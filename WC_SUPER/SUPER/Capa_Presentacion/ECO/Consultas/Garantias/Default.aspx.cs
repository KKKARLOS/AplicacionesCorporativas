using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;  
    public int nEstructuraMinima = 0;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLCliente = "", strHTMLContrato = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsCliente = "", strIDsContrato = "";
    public short nPantallaPreferencia = 12;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                //Master.nBotonera = 58;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Catálogo de garantías";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");

                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                #region Lectura de preferencia
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    sHayPreferencia = "true";

                    chkCerrarAuto.Checked = (aDatosPref[5] == "1") ? true : false;
                    chkActuAuto.Checked = (aDatosPref[6] == "1") ? true : false;
                    sSubnodos = aDatosPref[14];

                    if (chkActuAuto.Checked)
                    {
/*
                        string strTabla = obtenerDatos(aDatosPref[13],
                                            "7",
                                            strIDsCliente,
                                            strIDsResponsable,
                                            strIDsNaturaleza,
                                            strIDsModeloCon,
                                            strIDsContrato,
                                            sSubnodos,
                                            aDatosPref[7],
                        );

                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] != "OK") Master.sErrores += Errores.mostrarError(aTabla[1]);
 */
                    }
                }
                else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                #endregion

                hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                hdnHasta.Text = (DateTime.Now.Year * 100 + 12).ToString();

                string[] aCriterios = Regex.Split(cargarCriterios(int.Parse(hdnDesde.Text), int.Parse(hdnHasta.Text)), "@#@");
                if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
                else Master.sErrores = aCriterios[1];

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9]);
                break;
            case ("getTablaCriterios"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string setPreferencia(string sConceptoEje, string sCategoria, string sEstado, string sCualidad, string sCerrarAuto, string sActuAuto, string sOpcionPeriodo, string sOperadorLogico, string sObraCurso, string sFactAnti, string sValoresMultiples)
    {
        string sResul = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            int nPref = PREFERENCIAUSUARIO.Insertar(tr,
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 20,
                                        (sConceptoEje == "") ? null : sConceptoEje,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        (sEstado == "") ? null : sEstado,
                                        (sObraCurso == "") ? null : sObraCurso,
                                        (sFactAnti == "") ? null : sFactAnti,
                                         null, null, null, null, null, null, null, null, null, null, null);

            #region Valores Múltiples
            if (sValoresMultiples != "")
            {
                string[] aValores = Regex.Split(sValoresMultiples, "///");
                foreach (string oValor in aValores)
                {
                    if (oValor == "") continue;
                    string[] aDatos = Regex.Split(oValor, "##");
                    ///aDatos[0] = concepto
                    ///aDatos[1] = idValor
                    ///aDatos[2] = denominacion

                    PREFUSUMULTIVALOR.Insertar(tr, nPref, byte.Parse(aDatos[0]), aDatos[1], Utilidades.unescape(aDatos[2]));
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + nPref.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string delPreferencia()
    {
        try
        {
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 20);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar la preferencia", ex);
        }
    }
    private string getPreferencia(string sIdPrefUsuario)
    {
        StringBuilder sb = new StringBuilder();
        int idPrefUsuario = 0;
        //int nOpcion = 1;
        //string sEstado = "";
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 20);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //5
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //6
                //sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //7
                //sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //8

                //sEstado = dr["estado"].ToString();
                //sObraCurso = (dr["ObraCurso"].ToString() == "") ? "1" : dr["ObraCurso"].ToString();
                //sFactuAnti = (dr["FactAnti"].ToString() == "") ? "1" : dr["FactAnti"].ToString();

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                //nConceptoEje = (dr["ConceptoEje"].ToString() == "") ? 0 : int.Parse(dr["ConceptoEje"].ToString());
                //nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());
            }
            dr.Close();
            //dr.Dispose();

            #region HTML, IDs
            int nNivelMinimo = 0;
            bool bAmbito = false;
            string[] aID = null;
            dr = PREFUSUMULTIVALOR.Obtener(null, idPrefUsuario);
            while (dr.Read())
            {
                switch (int.Parse(dr["t441_concepto"].ToString()))
                {
                    case 1:
                        if (!bAmbito)
                        {
                            bAmbito = true;
                            nNivelMinimo = 6;
                        }
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (int.Parse(aID[0]) < nNivelMinimo) nNivelMinimo = int.Parse(aID[0]);

                        if (strIDsAmbito != "") strIDsAmbito += ",";
                        strIDsAmbito += aID[1];

                        aSubnodos = PREFUSUMULTIVALOR.SelectSubnodosAmbito(null, aSubnodos, int.Parse(aID[0]), int.Parse(aID[1]));
                        strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:18px;' idAux='";
                        strHTMLAmbito += SUBNODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLAmbito += "'><td>";

                        switch (int.Parse(aID[0]))
                        {
                            case 1: strHTMLAmbito += "<img src='../../../../images/imgSN4.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 2: strHTMLAmbito += "<img src='../../../../images/imgSN3.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 3: strHTMLAmbito += "<img src='../../../../images/imgSN2.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 4: strHTMLAmbito += "<img src='../../../../images/imgSN1.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 5: strHTMLAmbito += "<img src='../../../../images/imgNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 6: strHTMLAmbito += "<img src='../../../../images/imgSubNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLAmbito += "<nobr class='NBR W230'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 2:
                        if (strIDsResponsable != "") strIDsResponsable += ",";
                        strIDsResponsable += dr["t441_valor"].ToString();
                        strHTMLResponsable += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 3:
                        if (strIDsNaturaleza != "") strIDsNaturaleza += ",";
                        strIDsNaturaleza += dr["t441_valor"].ToString();
                        strHTMLNaturaleza += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 4:
                        if (strIDsModeloCon != "") strIDsModeloCon += ",";
                        strIDsModeloCon += dr["t441_valor"].ToString();
                        strHTMLModeloCon += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;

                    case 8:
                        if (strIDsCliente != "") strIDsCliente += ",";
                        strIDsCliente += dr["t441_valor"].ToString();
                        strHTMLCliente += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 9:
                        if (strIDsContrato != "") strIDsContrato += ",";
                        strIDsContrato += dr["t441_valor"].ToString();
                        strHTMLContrato += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion

            //if (nConceptoEje >= 7)
            //{
            //    nOpcion = nConceptoEje;
            //}
            //else
            //{
            //    if (nNivelMinimo != 0) nOpcion = nNivelMinimo;
            //    else nOpcion = nEstructuraMinima;
            //}

            for (int i = 0; i < aSubnodos.Count; i++)
            {
                if (i > 0) sSubnodos += ",";
                sSubnodos += aSubnodos[i];
            }

            //sb.Append(nOpcion + "@#@"); //13
            sb.Append(sSubnodos + "@#@"); //14
            sb.Append(strHTMLAmbito + "@#@"); //15
            sb.Append(strIDsAmbito + "@#@"); //16
            sb.Append(strHTMLResponsable + "@#@"); //17
            sb.Append(strIDsResponsable + "@#@"); //18
            sb.Append(strHTMLNaturaleza + "@#@"); //19
            sb.Append(strIDsNaturaleza + "@#@"); //20
            sb.Append(strHTMLModeloCon + "@#@"); //21
            sb.Append(strIDsModeloCon + "@#@"); //22
            //sb.Append(strHTMLHorizontal + "@#@"); //23
            //sb.Append(strIDsHorizontal + "@#@"); //24
            //sb.Append(strHTMLSector + "@#@"); //25
            //sb.Append(strIDsSector + "@#@"); //26
            //sb.Append(strHTMLSegmento + "@#@"); //27
            //sb.Append(strIDsSegmento + "@#@"); //28
            sb.Append(strHTMLCliente + "@#@"); //29
            sb.Append(strIDsCliente + "@#@"); //30
            sb.Append(strHTMLContrato + "@#@"); //31
            sb.Append(strIDsContrato + "@#@"); //32
            //sb.Append(strHTMLQn + "@#@"); //33
            //sb.Append(strIDsQn + "@#@"); //34
            //sb.Append(strHTMLQ1 + "@#@"); //35
            //sb.Append(strIDsQ1 + "@#@"); //36
            //sb.Append(strHTMLQ2 + "@#@"); //37
            //sb.Append(strIDsQ2 + "@#@"); //38
            //sb.Append(strHTMLQ3 + "@#@"); //39
            //sb.Append(strIDsQ3 + "@#@"); //40
            //sb.Append(strHTMLQ4 + "@#@"); //41
            //sb.Append(strIDsQ4 + "@#@"); //42
            //sb.Append(strHTMLProyecto + "@#@"); //43
            //sb.Append(strIDsProyecto + "@#@"); //44
            //sb.Append(sEstado + "@#@"); //45
            //sb.Append(sObraCurso + "@#@"); //46
            //sb.Append(sFactuAnti + "@#@"); //47


            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
    private string cargarCriterios(int nDesde, int nHasta)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        try
        {
            /*
             * t -> tipo
             * c -> codigo
             * d -> denominacion
             * ///datos auxiliares para el catálogo de proyecto (16)
             * a -> categoria
             * u -> cualidad
             * e -> estado
             * l -> cliente
             * n -> nodo
             * r -> responsable
             * */
            SqlDataReader dr = ConsultasPGE.ObtenerGarantiasCriterios((int)Session["UsuarioActual"], nDesde, nHasta, Constantes.nNumElementosMaxCriterios);
            while (dr.Read())
            {
                if ((int)dr["codigo"] == -1)
                    sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"excede\":1};\n");
                else
                {
                    sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\"};\n");
                }
                i++;
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }
    private string obtenerDatos(string sNivelEstructura,
                                    string sSituacionGarantia, string sNDiasGarantia,
                                    string sClientes, string sResponsables, string sNaturalezas, string sModeloContrato, string sContrato,
                                    string sIDEstructura
                                    )
    {
        StringBuilder sb = new StringBuilder();

        #region Cabecera tabla HTML
        sb.Append(@"<table id='tblDatos' class='MA' style='width: 960px;'>
                    <colgroup>
                        <col style='width:20px;' />
                        <col style='width:60px;' />
	                    <col style='width:200px;' />
	                    <col style='width:200px;' />
	                    <col style='width:200px;' />
	                    <col style='width:90px;' />
	                    <col style='width:90px;' />
                    </colgroup>");

        #endregion

        SqlDataReader dr = ConsultasPGE.ObtenerGarantias(
                                                            (int)Session["UsuarioActual"],
                                                            (sNivelEstructura == "0") ? null : (int?)int.Parse(sNivelEstructura),
                                                            sSituacionGarantia,
                                                            int.Parse(sNDiasGarantia),
                                                            sClientes,
                                                            sResponsables,
                                                            sNaturalezas,
                                                            sModeloContrato,
                                                            sContrato,
                                                            sIDEstructura
                                                            );

        while (dr.Read())
        {
            sb.Append("<tr ");
            sb.Append("id='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
            sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
            //string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + dr["Responsable"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
            //sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");
            sb.Append(">");

            sb.Append("<td></td>");
            sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
            sb.Append("<td><nobr class='NBR W190'>" + dr["t301_denominacion"].ToString() + "</nobr></td>");
            sb.Append("<td><nobr class='NBR W190'>" + dr["responsable_proyecto"].ToString() + "</nobr></td>");
            sb.Append("<td><nobr class='NBR W190'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
            sb.Append("<td>" + ((dr["t301_iniciogar"] == DBNull.Value) ? "" : ((DateTime)dr["t301_iniciogar"]).ToShortDateString()) + "</td>");
            sb.Append("<td>" + ((dr["t301_fingar"] == DBNull.Value) ? "" : ((DateTime)dr["t301_fingar"]).ToShortDateString()) + "</td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();

        sb.Append("</table>");
        return "OK@#@" + sb.ToString();
    }
}
