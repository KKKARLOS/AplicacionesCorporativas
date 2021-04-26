using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;

public partial class Capa_Presentacion_ECO_Consultas_Auditoria_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 0;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "";
    public short nPantallaPreferencia = 39;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Auditoría económica";
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                lblCDP.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                lblCSN1P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
                lblCSN2P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
                lblCSN3P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
                lblCSN4P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));

                if (!Utilidades.EstructuraActiva("SN4")) fstCSN4P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN3")) fstCSN3P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN2")) fstCSN2P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN1")) fstCSN1P.Style.Add("visibility", "hidden");

                if (Utilidades.EstructuraActiva("SN4")) nEstructuraMinima = 1;
                else if (Utilidades.EstructuraActiva("SN3")) nEstructuraMinima = 2;
                else if (Utilidades.EstructuraActiva("SN2")) nEstructuraMinima = 3;
                else if (Utilidades.EstructuraActiva("SN1")) nEstructuraMinima = 4;

                hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();
                hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

                Utilidades.SetEventosFecha(this.txtDesdeAct);
                Utilidades.SetEventosFecha(this.txtHastaAct);

                DateTime dHoy = DateTime.Now, dtAux;
                int nDias = dHoy.Day;
                dtAux = dHoy.AddDays(-nDias + 1);
                txtDesdeAct.Text = dtAux.ToShortDateString();
                dtAux = dtAux.AddMonths(1).AddDays(-1);
                txtHastaAct.Text = dtAux.ToShortDateString();

                lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
                lblMonedaImportes2.InnerText = Session["DENOMINACION_VDC"].ToString();

                //divMonedaImportes.Style.Add("visibility", "visible");
                //divMonedaImportes2.Style.Add("visibility", "visible");

                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                #region Lectura de preferencia
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    sHayPreferencia = "true";
                    cboCategoria.SelectedValue = aDatosPref[2];
                    cboCualidad.SelectedValue = aDatosPref[3];
                    chkCerrarAuto.Checked = (aDatosPref[4] == "1") ? true : false;
                    chkActuAuto.Checked = (aDatosPref[5] == "1") ? true : false;
                    //if (chkActuAuto.Checked) btnObtener.Disabled = true;
                    if (aDatosPref[6] == "1") rdbOperador.Items[0].Selected = true;
                    else rdbOperador.Items[1].Selected = true;

                    nUtilidadPeriodo = int.Parse(aDatosPref[7]);
                    hdnDesde.Text = aDatosPref[8];
                    txtDesde.Text = aDatosPref[9];
                    hdnHasta.Text = aDatosPref[10];
                    txtHasta.Text = aDatosPref[11];

                    chkIns.Checked = (aDatosPref[12] == "1") ? true : false;
                    chkDel.Checked = (aDatosPref[13] == "1") ? true : false;
                    chkMod.Checked = (aDatosPref[14] == "1") ? true : false;
                    string sTipo = "";
                    if (aDatosPref[12] == "1" && aDatosPref[13] == "1" && aDatosPref[14] == "1")
                        sTipo = "";
                    else
                    {
                        if (aDatosPref[12] == "1") sTipo += "I";
                        if (aDatosPref[13] == "1") 
                        {
                            if (sTipo == "")
                                sTipo += "B";
                            else
                                sTipo += ",B";
                        }
                        if (aDatosPref[14] == "1") sTipo += "M,";
                        {
                            if (sTipo == "")
                                sTipo += "M";
                            else
                                sTipo += ",M";
                        }
                    }
                    sSubnodos = aDatosPref[15];

                    if (chkActuAuto.Checked)
                    {
                        string strTabla = obtenerDatos(sTipo, hdnDesde.Text, hdnHasta.Text,
                                            this.txtDesdeAct.Text, this.txtHastaAct.Text,
                                            "7",
                                            cboCategoria.SelectedValue,
                                            cboCualidad.SelectedValue,
                                            strIDsCliente,
                                            strIDsResponsable,
                                            strIDsNaturaleza,
                                            strIDsHorizontal,
                                            strIDsModeloCon,
                                            strIDsContrato,
                                            sSubnodos,
                                            strIDsSector,
                                            strIDsSegmento,
                                            aDatosPref[6],
                                            strIDsQn,
                                            strIDsQ1,
                                            strIDsQ2,
                                            strIDsQ3,
                                            strIDsQ4,
                                            strIDsProyecto);

                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] == "OK")
                        {
                            strTablaHTML = aTabla[1];
                        }
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                }
                else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                #endregion

                string[] aCriterios = Regex.Split(cargarCriterios(hdnDesde.Text, hdnHasta.Text), "@#@");
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
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21], aArgs[22], aArgs[23], aArgs[24]);
                break;
            case ("getTablaCriterios"):
                sResultado += cargarCriterios(aArgs[1], aArgs[2]);
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10]);
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

    private string cargarCriterios(string sDesde, string sHasta)
    {
        StringBuilder sb = new StringBuilder();
        int nDesde = 0, nHasta = 0;
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
            if (sDesde != "") nDesde = int.Parse(sDesde);
            if (sHasta != "") nHasta = int.Parse(sDesde);
            SqlDataReader dr = ConsultasPGE.ObtenerCombosDatosResumidosCriterios((int)Session["UsuarioActual"], nDesde, nHasta, Constantes.nNumElementosMaxCriterios);
            while (dr.Read())
            {
                if ((int)dr["codigo"] == -1)
                    sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"excede\":1};\n");
                else
                {
                    if ((int)dr["tipo"] == 16)
                        sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\",\"p\":\"" + dr["t301_idproyecto"].ToString() + "\",\"a\":\"" + dr["t301_categoria"].ToString() + "\",\"u\":\"" + dr["t305_cualidad"].ToString() + "\",\"e\":\"" + dr["t301_estado"].ToString() + "\",\"l\":\"" + dr["t302_denominacion"].ToString() + "\",\"n\":\"" + dr["t303_denominacion"].ToString() + "\",\"r\":\"" + dr["Responsable"].ToString() + "\"};\n");
                    else
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

    private string obtenerDatos(string sTipo, string sDesde, string sHasta, string sDesdeAct, string sHastaAct, string sNivelEstructura,
                                string sCategoria, string sCualidad, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P, string sPSN)
    {
        StringBuilder sb = new StringBuilder();
        int? iDesde = null;
        int? iHasta = null;
        string sAux = "", sPrimer = "";
        try
        {
            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<tbody>");
            //sb.Append("<tr><td>Tabla</td><td>Fila</td><td>t499_id1</td><td>t499_id2</td><td>t499_id3</td>");
            //sb.Append("<td>Línea de Negocio</td><td>Unidad de Negocio</td><td>Centro de responsabilidad</td>");
            //sb.Append("<td>Nº proyecto</td><td>Denominación proyecto</td><td>Responsable del proyecto</td><td>Cualidad</td>");
            //sb.Append("<td>Modalidad</td><td>Naturaleza</td><td>Oportunidad</td><td>Cliente</td>");
            //sb.Append("<td>Grupo económico</td><td>Subgrupo económico</td><td>Concepto económico</td>");
            //sb.Append("<td>Clase económica</td><td>Usuario</td><td>Qué</td><td>Acción</td><td>Autor acción</td>");
            //sb.Append("<td>Cuando</td><td>Fecha cierre</td>");
            //sb.Append("<td>Importe antiguo</td><td>Unidades antiguas</td><td>Coste contratante antiguo</td><td>Perfil antiguo</td>");
            //sb.Append("<td>Importe nuevo</td><td>Unidades nuevas</td><td>Coste contratante nuevo</td><td>Perfil nuevo</td>");
            //sb.Append("</tr>");
            bool bTitulos = false;
            if (sDesde != "" && sDesde != "0") iDesde = int.Parse(sDesde);
            if (sHasta != "" && sHasta!="0") iHasta = int.Parse(sHasta);
            DataSet ds = ConsultasPGE.getDatosAuditoria((int)Session["IDFICEPI_ENTRADA"],
                            (int)Session["UsuarioActual"], sTipo,
                            iDesde, iHasta, DateTime.Parse(sDesdeAct),  DateTime.Parse(sHastaAct),
                            (sNivelEstructura == "0") ? null : (int?)int.Parse(sNivelEstructura), "",//ESTADO
                            sCategoria.Trim(), sCualidad.Trim(), (sComparacionLogica == "1") ? true : false,
                            sClientes, sResponsables, sNaturalezas, sHorizontal, sModeloContrato, sContrato, sIDEstructura,
                            sSectores, sSegmentos, sCNP, sCSN1P, sCSN2P, sCSN3P, sCSN4P, sPSN
                            );
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                #region Old
                //sb.Append("<td>" + oFila["t300_tabla"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["idfila"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["t499_id1"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["t499_id2"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["t499_id3"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Línea de Negocio"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Unidad de Negocio"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["C. Resp."].ToString() + "</td>");
                //sb.Append("<td>" + int.Parse(oFila["Pyto"].ToString()).ToString("#,###") + "</td>");
                //sb.Append("<td>" + oFila["Proyecto"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Responsable del Pyto"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Cual"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Modalidad"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Naturaleza"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Oport"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Cliente"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Grupo eco"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Subgrupo eco"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Concepto eco"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Clase eco"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Usuario"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Qué"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Accion"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Autor acción"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Cuando"].ToString() + "</td>");
                //sb.Append("<td>" + oFila["Fec.cierre"].ToString() + "</td>");

                //sb.Append("<td>" + double.Parse(oFila["Importe antiguo"].ToString()).ToString("#,###.##") + "</td>");
                //sb.Append("<td>" + double.Parse(oFila["Unidades antiguas"].ToString()).ToString("#,###.##") + "</td>");
                //sb.Append("<td>" + double.Parse(oFila["Cte.contrat.antiguo"].ToString()).ToString("#,###.##") + "</td>");
                //sb.Append("<td>" + oFila["Perfil antiguo"].ToString() + "</td>");

                //sb.Append("<td>" + double.Parse(oFila["Importe nuevo"].ToString()).ToString("#,###.##") + "</td>");
                //sb.Append("<td>" + double.Parse(oFila["Unidades nuevas"].ToString()).ToString("#,###.##") + "</td>");
                //sb.Append("<td>" + double.Parse(oFila["Cte.contrat.nuevo"].ToString()).ToString("#,###.##") + "</td>");
                //sb.Append("<td>" + oFila["Perfil nuevo"].ToString() + "</td>");

                //sb.Append("</tr>");
                #endregion
                if (!bTitulos)
                {
                    sb.Append("<tr align='center'>");
                    for (int x = 0; x < ds.Tables[0].Columns.Count; x++)
                    {
                        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + ds.Tables[0].Columns[x].ColumnName + "</td>");
                    }
                    sb.Append("</tr>");
                    bTitulos = true;
                }
                sb.Append("<tr>");
                for (int x = 0; x < ds.Tables[0].Columns.Count; x++)
                {
                    sAux = oFila[x].ToString();
                    if (ds.Tables[0].Columns[x].DataType.Name == "String" && sAux.Trim() != "")
                    {//Para el contenido de campos de tipo Text hacemos transformaciones para que no falle la exportación a Excel
                        sAux = sAux.Replace("<", " < ");
                        sAux = sAux.Replace(">", " > ");
                        sAux = sAux.Trim();
                        sPrimer = sAux.Substring(0, 1);
                        switch (sPrimer)
                        {
                            case "-":
                            case "+":
                            case "=":
                                sAux = "(" + sPrimer + ")" + sAux.Substring(1);
                                break;
                        }
                    }
                    sb.Append("<td>" + sAux + "</td>");
                }
                sb.Append("</tr>");
            }
            ds.Dispose();

            sb.Append("</tbody></table>");

            //return "OK@#@" + sb.ToString();
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos económicos.", ex);
        }
    }

    private string setPreferencia(string sIns, string sDel, string sMod, string sCategoria, string sCualidad, string sCerrarAuto, 
                                  string sActuAuto, string sOpcionPeriodo, string sOperadorLogico, string sValoresMultiples)
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 39,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        (sIns == "") ? null : sIns,
                                        (sDel == "") ? null : sDel,
                                        (sMod == "") ? null : sMod,
                                        null, null, null, null, null, null, null, null, null, null, null, null);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 39);
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

        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 39);
            if (dr.Read())
            {
                bHayPreferencia = true;

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["categoria"].ToString() + "@#@"); //2
                sb.Append(dr["cualidad"].ToString() + "@#@"); //3
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //4
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //5
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //6
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //7
            }

            #region Fechas
            switch (nUtilidadPeriodo)
            {
                case -1:
                    sb.Append("0@#@");//8
                    sb.Append("@#@");//9
                    sb.Append("0@#@");//10
                    sb.Append("@#@");//11
                    break;
                case 1:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + 12).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 12) + "@#@");//11
                    break;
                case 2:
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//11
                    break;
                case 3:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//11
                    break;
                case 4:
                    sb.Append("199001" + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//11
                    break;
                case 5:
                    sb.Append("199001" + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//9
                    sb.Append("207812" + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(207812) + "@#@");//11
                    break;
                default:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//8
                    sb.Append(mes[0] + " " + DateTime.Now.Year.ToString() + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString() + "@#@");//11
                    break;
            }
            #endregion

            if (bHayPreferencia)
            {
                sb.Append(dr["Insercion"].ToString() + "@#@"); //12
                sb.Append(dr["Borrado"].ToString() + "@#@"); //13
                sb.Append(dr["Modificacion"].ToString() + "@#@"); //14
            }
            else
            {
                sb.Append("1@#@1@#@1@#@");
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
                    case 5:
                        if (strIDsHorizontal != "") strIDsHorizontal += ",";
                        strIDsHorizontal += dr["t441_valor"].ToString();
                        strHTMLHorizontal += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 6: if (strIDsSector != "") strIDsSector += ",";
                        strIDsSector += dr["t441_valor"].ToString();
                        strHTMLSector += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 7: if (strIDsSegmento != "") strIDsSegmento += ",";
                        strIDsSegmento += dr["t441_valor"].ToString();
                        strHTMLSegmento += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
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
                    case 10:
                        if (strIDsQn != "") strIDsQn += ",";
                        strIDsQn += dr["t441_valor"].ToString();
                        strHTMLQn += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 11:
                        if (strIDsQ1 != "") strIDsQ1 += ",";
                        strIDsQ1 += dr["t441_valor"].ToString();
                        strHTMLQ1 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 12:
                        if (strIDsQ2 != "") strIDsQ2 += ",";
                        strIDsQ2 += dr["t441_valor"].ToString();
                        strHTMLQ2 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 13:
                        if (strIDsQ3 != "") strIDsQ3 += ",";
                        strIDsQ3 += dr["t441_valor"].ToString();
                        strHTMLQ3 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 14:
                        if (strIDsQ4 != "") strIDsQ4 += ",";
                        strIDsQ4 += dr["t441_valor"].ToString();
                        strHTMLQ4 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    //case 15: //strMagnitudes  break;
                    case 16:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsProyecto != "") strIDsProyecto += ",";
                        strIDsProyecto += aID[0];

                        strHTMLProyecto += "<tr id='" + aID[0] + "' style='height:18px;' ";
                        strHTMLProyecto += "categoria='" + aID[1] + "' ";
                        strHTMLProyecto += "cualidad='" + aID[2] + "' ";
                        strHTMLProyecto += "estado='" + aID[3] + "'><td>";

                        if (aID[1] == "P") strHTMLProyecto += "<img src='../../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";
                        else strHTMLProyecto += "<img src='../../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";

                        switch (aID[2])
                        {
                            case "C": strHTMLProyecto += "<img src='../../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "J": strHTMLProyecto += "<img src='../../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        switch (aID[3])
                        {
                            case "A": strHTMLProyecto += "<img src='../../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "C": strHTMLProyecto += "<img src='../../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "H": strHTMLProyecto += "<img src='../../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLProyecto += "<nobr class='NBR W190' style='margin-left:10px;' onmouseover='TTip(event)'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion

            for (int i = 0; i < aSubnodos.Count; i++)
            {
                if (i > 0) sSubnodos += ",";
                sSubnodos += aSubnodos[i];
            }

            sb.Append(sSubnodos + "@#@"); //15
            sb.Append(strHTMLAmbito + "@#@"); //16
            sb.Append(strIDsAmbito + "@#@"); //17
            sb.Append(strHTMLResponsable + "@#@"); //18
            sb.Append(strIDsResponsable + "@#@"); //19
            sb.Append(strHTMLNaturaleza + "@#@"); //20
            sb.Append(strIDsNaturaleza + "@#@"); //21
            sb.Append(strHTMLModeloCon + "@#@"); //22
            sb.Append(strIDsModeloCon + "@#@"); //23
            sb.Append(strHTMLHorizontal + "@#@"); //24
            sb.Append(strIDsHorizontal + "@#@"); //25
            sb.Append(strHTMLSector + "@#@"); //26
            sb.Append(strIDsSector + "@#@"); //27
            sb.Append(strHTMLSegmento + "@#@"); //28
            sb.Append(strIDsSegmento + "@#@"); //29
            sb.Append(strHTMLCliente + "@#@"); //30
            sb.Append(strIDsCliente + "@#@"); //31
            sb.Append(strHTMLContrato + "@#@"); //32
            sb.Append(strIDsContrato + "@#@"); //33
            sb.Append(strHTMLQn + "@#@"); //34
            sb.Append(strIDsQn + "@#@"); //35
            sb.Append(strHTMLQ1 + "@#@"); //36
            sb.Append(strIDsQ1 + "@#@"); //37
            sb.Append(strHTMLQ2 + "@#@"); //38
            sb.Append(strIDsQ2 + "@#@"); //39
            sb.Append(strHTMLQ3 + "@#@"); //40
            sb.Append(strIDsQ3 + "@#@"); //41
            sb.Append(strHTMLQ4 + "@#@"); //42
            sb.Append(strIDsQ4 + "@#@"); //43
            sb.Append(strHTMLProyecto + "@#@"); //44
            sb.Append(strIDsProyecto + "@#@"); //45

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
}