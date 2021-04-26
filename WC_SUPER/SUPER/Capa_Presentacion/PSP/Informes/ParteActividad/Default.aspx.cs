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

using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    public SqlConnection oConn;
    public SqlTransaction tr;
    private string _callbackResultado = null;
    protected ArrayList aProyecto;
    public string strTablaHtml;
    public int nEstructuraMinima = 0;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLProfesionales = "", strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "";
    public string strIDsProfesionales = "";
    public short nPantallaPreferencia = 26;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Parte de actividad";
        Master.bFuncionesLocales = true;
        Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
        Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

        if (!Page.IsCallback)
        {
            lblCDP.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
            lblCSN1P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
            lblCSN2P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
            lblCSN3P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
            lblCSN4P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));

            if (!Utilidades.EstructuraActiva("SN4")) fstCSN4P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN3")) fstCSN3P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN2")) fstCSN2P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN1")) fstCSN1P.Style.Add("visibility", "hidden");

            lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
            //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                divMonedaImportes.Style.Add("visibility", "visible");

            Utilidades.SetEventosFecha(this.txtFechaInicio);
            Utilidades.SetEventosFecha(this.txtFechaFin);

            DateTime dHoy = DateTime.Now, dtAux;
            int nDias = dHoy.Day;
            dtAux = dHoy.AddDays(-nDias + 1);
            txtFechaInicio.Text = dtAux.ToShortDateString();
            dtAux = dtAux.AddMonths(1).AddDays(-1);
            txtFechaFin.Text = dtAux.ToShortDateString();

            string[] aCriterios = Regex.Split(cargarCriterios(), "@#@");
            if (aCriterios[0] == "OK") 
                sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
            else Master.sErrores = aCriterios[1];

            if (Utilidades.EstructuraActiva("SN4")) nEstructuraMinima = 1;
            else if (Utilidades.EstructuraActiva("SN3")) nEstructuraMinima = 2;
            else if (Utilidades.EstructuraActiva("SN2")) nEstructuraMinima = 3;
            else if (Utilidades.EstructuraActiva("SN1")) nEstructuraMinima = 4;

            strTablaHtml = "<tr id='*' class='FA'><td>&lt; Todos &gt;</td></tr>";

            string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
            #region Lectura de preferencia

            if (bHayPreferencia && aDatosPref[0] == "OK")
            {
                sHayPreferencia = "true";
                cboCategoria.SelectedValue = aDatosPref[2];
                cboCualidad.SelectedValue = aDatosPref[3];

                if (aDatosPref[4] == "1") rdbOperador.Items[0].Selected = true;
                else rdbOperador.Items[1].Selected = true;

                chkFacturable.Checked = (aDatosPref[6] == "1") ? true : false;
                chkNoFacturable.Checked = (aDatosPref[7] == "1") ? true : false;

                sSubnodos = aDatosPref[14];
                //rdbFormato.SelectedValue = aDatosPref[45];
            }
            else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
            //else txtTolerancia.Attributes.Add("readonly", "readonly");

            #endregion

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor

            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
         switch (aArgs[0])
        {
            case ("getTablaCriterios"):
                sResultado += cargarCriterios();
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
                break;
            case ("generarExcel"):
                bool bError = false;
                if (!Utilidades.isDate(aArgs[3]))
                {
                    sResultado += "Error@#@La fecha desde no es correcta";
                    bError = true;
                }
                if (!bError && !Utilidades.isDate(aArgs[4]))
                {
                    sResultado += "Error@#@La fecha hasta no es correcta";
                    bError = true;
                }
                if (!bError)
                {
                    sResultado += generarExcel
                        (
                        aArgs[1],  //sCategoria
                        aArgs[2],  //sCualidad
                        DateTime.Parse(aArgs[3]),  //sFechaInicio 
                        DateTime.Parse(aArgs[4]),  //sFechaFin
                        aArgs[5],  //sProyectos
                        aArgs[6],  //sClientes
                        aArgs[7],  //sResponsables
                        aArgs[8],  //sNaturalezas
                        aArgs[9],  //sHorizontal
                        aArgs[10],  //sModeloContrato
                        aArgs[11],  //sContrato
                        aArgs[12], //sIDEstructura
                        aArgs[13], //sSectores
                        aArgs[14], //sSegmentos
                        aArgs[15], //sComparacionLogica
                        aArgs[16], //sCNP
                        aArgs[17], //sCSN1P
                        aArgs[18], //sCSN2P
                        aArgs[19], //sCSN3P
                        aArgs[20], //sCSN4P
                        aArgs[21], //Profesionales
                        aArgs[22] //Facturable
                        );
                }
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
    private string cargarCriterios()
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
            //if ( Session["IDRED_ENTRADA"].ToString()=="1341")
            //{
            //    string sLinea = Session["DES_EMPLEADO_ENTRADA"].ToString() + " InformesParteActividad UsuarioActual = " + Session["UsuarioActual"];
            //    SUPER.DAL.Log.Insertar(sLinea);
            //}
            SqlDataReader dr = ConsultasPSP.ObtenerParteActividadCriterios((int)Session["UsuarioActual"], Constantes.nNumElementosMaxCriterios);
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
    private string generarExcel(string sCategoria, string sCualidad, DateTime dtFechaInicio, DateTime dtFechaFin,
                                string sProyectos, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P,
                                string sProfesionales, string sFacturable)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool? bFacturable = null;

        ArrayList aSb = new ArrayList();
        aSb.Add(sb);
        StringBuilder sb_activo = (StringBuilder)aSb[aSb.Count-1];
        try
        {
            if (sFacturable != "") bFacturable = (sFacturable == "1") ? true : false;

            SqlDataReader dr = USUARIO.PARTE_ACTIVIDAD
                (
                (int)Session["UsuarioActual"],
                sCategoria,
                sCualidad,
                dtFechaInicio,
                dtFechaFin,
                sProyectos,
                sClientes,
                sResponsables,
                sNaturalezas,
                sHorizontal,
                sModeloContrato,
                sContrato,
                sIDEstructura,
                sSectores,
                sSegmentos,
                (sComparacionLogica == "1") ? true : false,
                sCNP,
                sCSN1P,
                sCSN2P,
                sCSN3P,
                sCSN4P,
                sProfesionales,
                bFacturable,
                Session["MONEDA_VDC"].ToString()
                );


            //strBuilder.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            //strBuilder.Append("</table>");

            sb_activo.Append(@"<TABLE style='font-family:Arial;font-size:8pt;' border=1>
                <tr align=center style='background-color: #BCD4DF;'>
                <td style='width:auto;'>Fecha Imputación</td>
                <td style='text-align:rigth;width:auto;'>Usuario</td>
                <td style='width:auto;'>Profesional</td>
                <td style='width:auto;'>Código-Externo</td>
                <td style='width:auto;'>Cliente</td>
                <td style='width:auto;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + @"</td>
                <td style='text-align:rigth;width:auto;'>Nº proyecto</td>
                <td style='width:auto;'>Proyecto económico</td>
                <td style='width:auto;'>Proyecto técnico</td>
                <td style='width:auto;'>Fase</td>
                <td style='width:auto;'>Actividad</td>
                <td style='text-align:rigth;width:auto;'>Nº tarea</td>
                <td style='width:auto;'>Tarea</td>
                <td style='text-align:center;width:auto;'>Facturable</td>

                <td style='width:auto;'>Modo de facturación</td>
                <td style='text-align:rigth;width:auto;'>Horas imputadas</td>
                <td style='text-align:rigth;width:auto;'>Jornadas imputadas</td>
                <td style='width:auto;'>Comentario</td>

                <td style='width:auto;'>Modelo de tarificación</td>
                <td style='width:auto;'>Perfil</td>
                <td style='width:auto;'>Tarifa</td>
                <td style='text-align:rigth;width:auto;'>Importe (Tarifa x unidades imputadas)</td>
                </tr>");

            while (dr.Read())
            {
                if (sb_activo.Length > 5000000)
                {
                    sb = new StringBuilder();
                    aSb.Add(sb);
                    sb_activo = (StringBuilder)aSb[aSb.Count - 1];
                }
                sb_activo.Append(@"<tr style='vertical-align:top;'>
                    <td style='width:auto;'>" + ((DateTime)dr["t337_fecha"]).ToShortDateString() + @"</td>
                    <td style='text-align:rigth;width:auto;'>" + ((int)dr["t314_idusuario"]).ToString("#,###") + @"</td>
                    <td style='width:auto;'>" + dr["profesional"].ToString() + @"</td>
                    <td style='width:auto;'>" + dr["codigoexterno"].ToString() + @"</td>
                    <td style='width:auto;'>" + dr["t302_denominacion"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + @"</td>
                    <td style='width:auto;'>" + dr["t303_denominacion"].ToString() + @"</td>
                    <td style='text-align:rigth;width:auto;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + @"</td>
                    <td style='width:auto;'>" + dr["t301_denominacion"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + @"</td>
                    <td style='width:auto;'>" + dr["t331_despt"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + @"</td>
                    <td style='width:auto;'>" + dr["t334_desfase"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + @"</td>
                    <td style='width:auto;'>" + dr["t335_desactividad"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + @"</td>
                    <td style='text-align:rigth;width:auto;'>" + ((int)dr["t332_idtarea"]).ToString("#,###") + @"</td>
                    <td style='width:auto;'>" + dr["t332_destarea"].ToString() + @"</td>
                    <td style='text-align:center;width:auto;'>"+ (((bool)dr["t332_facturable"])? "Sí":"No") + @"</td>
                    <td style='width:auto;'>" + dr["t324_denominacion"].ToString() + @"</td>
                    <td style='text-align:rigth;width:auto;'>" + dr["t337_esfuerzo"].ToString() + @"</td>
                    <td style='text-align:rigth;width:auto;'>" + decimal.Parse(dr["t337_esfuerzoenjor"].ToString()).ToString("N") + @"</td>
                    <td style='width:auto;'>" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + @"</td>
                    <td style='text-align:center;width:auto;'>"+ ((dr["t301_modelotarif"].ToString()=="H")?"Horas":"Jornadas") + @"</td>
                    <td style='width:auto;'>" + dr["t333_denominacion"].ToString() + @"</td>
                    <td style='text-align:rigth;width:auto;'>" + decimal.Parse(dr["t333_imptarifa"].ToString()).ToString("N") + @"</td>
                    <td style='text-align:rigth;width:auto;'>" + decimal.Parse(dr["ImporteUnidadesConsumidas"].ToString()).ToString("N") + @"</td>
                    </tr>" + (char)13);
            }

            sb_activo.Append(@"<tr style='vertical-align:top;'>
                <td style='font-weight:bold;width:auto;'>* Importes en " + Session["DENOMINACION_VDC"].ToString() + @"</td>");

            for (var j = 2; j <= 22; j++)
            {
                sb_activo.Append("<td></td>");
            }
            sb_activo.Append("</tr>");  
            dr.Close();
            dr.Dispose();

            sb_activo.Append("</table>");

            //sResul = "OK@#@" + sb_activo.ToString();
            //return "OK@#@" + ((aSb[0] != null) ? ((StringBuilder)aSb[0]).ToString() : "")
            //            + ((aSb[1] != null) ? ((StringBuilder)aSb[1]).ToString() : "")
            //            +((aSb[2] != null) ? ((StringBuilder)aSb[2]).ToString() : "")
            //            +((aSb[3] != null) ? ((StringBuilder)aSb[3]).ToString() : "")
            //            +((aSb[4] != null) ? ((StringBuilder)aSb[4]).ToString() : "")
            //            +((aSb[5] != null) ? ((StringBuilder)aSb[5]).ToString() : "")
            //            +((aSb[6] != null) ? ((StringBuilder)aSb[6]).ToString() : "")
            //            +((aSb[7] != null) ? ((StringBuilder)aSb[7]).ToString() : "")
            //            +((aSb[8] != null) ? ((StringBuilder)aSb[8]).ToString() : "")
            //            +((aSb[9] != null) ? ((StringBuilder)aSb[9]).ToString() : "");

            //return "OK@#@" + getStringFromStringBuilderArrayList(aSb);
            string sCadena = getStringFromStringBuilderArrayList(aSb);

            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sCadena;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sCadena;

            //sResul = "OK@#@";
            //foreach (StringBuilder oSb in aSb){
            //    sResul += oSb.ToString();
            //}
            //sb = null; //Para liberar memoria
            //sb_activo = null; //Para liberar memoria

            //return sResul;

        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de los partes de actividad", ex);
        }

        return sResul;

    }

    private string getStringFromStringBuilderArrayList(ArrayList aSb)
    {
        string sResul = "";
        foreach (StringBuilder oSb in aSb){
            sResul += oSb.ToString();
        }
        return sResul;
    }
    private string setPreferencia(         
        string sCategoria, string sCualidad, string sOperadorLogico, string sFormato, 
        string sFacturable, string sNoFacturable, string sValoresMultiples        
        )
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 
                                        26,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sOperadorLogico == "") ? null : sOperadorLogico, 
                                        (sFormato == "") ? null : sFormato,
                                        (sFacturable == "") ? null : sFacturable,
                                        (sNoFacturable == "") ? null : sNoFacturable,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null, null, null, null, null, null, null);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 26);
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
        int idPrefUsuario = 0; //, nConceptoEje = 0;
        int nOpcion = 1;
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 26);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["categoria"].ToString() + "@#@"); //2
                sb.Append(dr["cualidad"].ToString() + "@#@"); //3

                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //4
                sb.Append(dr["Formato"].ToString() + "@#@"); //5

                sb.Append(dr["Facturable"].ToString() + "@#@"); //6
                sb.Append(dr["NoFacturable"].ToString() + "@#@"); //7

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());

                //sFormato = dr["Formato"].ToString(); //51                
            }
            dr.Close();
            //dr.Dispose();
            sb.Append("0@#@");//8
            sb.Append("0@#@");//9
            sb.Append("0@#@");//10
            sb.Append("0@#@");//11
            sb.Append("0@#@");//12

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
                        strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:16px;' idAux='";
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
                    case 15:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsProfesionales != "") strIDsProfesionales += ",";
                        strIDsProfesionales += aID[0];

                        strHTMLProfesionales += "<tr id='" + aID[0] + "' ";
                        strHTMLProfesionales += "tipo='" + aID[1] + "' ";
                        strHTMLProfesionales += "sexo='" + aID[2] + "' ";
                        strHTMLProfesionales += "baja='" + aID[3] + "'>";

                        strHTMLProfesionales += "<td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 16:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsProyecto != "") strIDsProyecto += ",";
                        strIDsProyecto += aID[0];

                        strHTMLProyecto += "<tr id='" + aID[0] + "' style='height:16px;' ";
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

            sb.Append(nOpcion + "@#@"); //13
            sb.Append(sSubnodos + "@#@"); //14
            sb.Append(strHTMLAmbito + "@#@"); //15
            sb.Append(strIDsAmbito + "@#@"); //16
            sb.Append(strHTMLResponsable + "@#@"); //17
            sb.Append(strIDsResponsable + "@#@"); //18
            sb.Append(strHTMLNaturaleza + "@#@"); //19
            sb.Append(strIDsNaturaleza + "@#@"); //20
            sb.Append(strHTMLModeloCon + "@#@"); //21
            sb.Append(strIDsModeloCon + "@#@"); //22
            sb.Append(strHTMLHorizontal + "@#@"); //23
            sb.Append(strIDsHorizontal + "@#@"); //24
            sb.Append(strHTMLSector + "@#@"); //25
            sb.Append(strIDsSector + "@#@"); //26
            sb.Append(strHTMLSegmento + "@#@"); //27
            sb.Append(strIDsSegmento + "@#@"); //28
            sb.Append(strHTMLCliente + "@#@"); //29
            sb.Append(strIDsCliente + "@#@"); //30
            sb.Append(strHTMLContrato + "@#@"); //31
            sb.Append(strIDsContrato + "@#@"); //32
            sb.Append(strHTMLQn + "@#@"); //33
            sb.Append(strIDsQn + "@#@"); //34
            sb.Append(strHTMLQ1 + "@#@"); //35
            sb.Append(strIDsQ1 + "@#@"); //36
            sb.Append(strHTMLQ2 + "@#@"); //37
            sb.Append(strIDsQ2 + "@#@"); //38
            sb.Append(strHTMLQ3 + "@#@"); //39
            sb.Append(strIDsQ3 + "@#@"); //40
            sb.Append(strHTMLQ4 + "@#@"); //41
            sb.Append(strIDsQ4 + "@#@"); //42
            sb.Append(strHTMLProfesionales + "@#@"); //43
            sb.Append(strIDsProfesionales + "@#@"); //44
            sb.Append(strHTMLProyecto + "@#@"); //45
            sb.Append(strIDsProyecto + "@#@"); //46

            //sb.Append(sFormato + "@#@"); //47
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
}