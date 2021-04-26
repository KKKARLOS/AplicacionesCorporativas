using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos' class='texto MA' style='width: 1520px;text-align:right;' ></table>";
    public string strTblBodyFijoHTML = "<table id='tblBodyFijo' style='width:375px;'></table>";
    public string strTblBodyMovilHTML = "<table id='tblBodyMovil' style='width:1135px;'></table>";

    public SqlConnection oConn;
    public SqlTransaction tr;
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 0;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "";
    public short nPantallaPreferencia = 3;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 48;
                //Master.nResolucion = 1280;
                if (!(bool)Session["AVANTEC1024"])
                {
                    Master.nResolucion = 1280;
                }
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Resumen de avance";
                Master.FicherosCSS.Add("Capa_Presentacion/ECO/Avance/Avance.css");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");

                try
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

                    if (Utilidades.EstructuraActiva("SN4")) nEstructuraMinima = 1;
                    else if (Utilidades.EstructuraActiva("SN3")) nEstructuraMinima = 2;
                    else if (Utilidades.EstructuraActiva("SN2")) nEstructuraMinima = 3;
                    else if (Utilidades.EstructuraActiva("SN1")) nEstructuraMinima = 4;

                    //hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                    //txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();
                    //hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                    //txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

                    //Al estar oculto el rango temporal, hay que asignar valores globales para que se carguen los criterios.
                    hdnDesde.Text = "199001";
                    txtDesde.Text = mes[0] + " " + "1990";
                    hdnHasta.Text = "207812";
                    txtHasta.Text = mes[11] + " " + "2078";

                    lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
                    lblMonedaImportes2.InnerText = Session["DENOMINACION_VDC"].ToString();
                    //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    //{
                        divMonedaImportes.Style.Add("visibility", "visible");
                        divMonedaImportes2.Style.Add("visibility", "visible");
                    //}

                    string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");

                    if (bHayPreferencia && aDatosPref[0] == "OK")
                    {
                        sHayPreferencia = "true";
                        cboEstado.SelectedValue = aDatosPref[43];
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
                        sSubnodos = aDatosPref[12];

                        if (chkActuAuto.Checked)
                        {
                            string strTabla = obtenerDatos((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString(),
                                                (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString(),
                                                "7",
                                                cboEstado.SelectedValue,
                                                cboCategoria.SelectedValue,
                                                cboCualidad.SelectedValue,
                                                strIDsCliente,
                                                strIDsResponsable,
                                                strIDsNaturaleza,
                                                strIDsHorizontal,
                                                strIDsModeloCon,
                                                strIDsContrato,
                                                sSubnodos,
                                                strIDsSector, strIDsSegmento,
                                                aDatosPref[6],
                                                strIDsQn,
                                                strIDsQ1, strIDsQ2, strIDsQ3, strIDsQ4,
                                                strIDsProyecto);

                            string[] aTabla = Regex.Split(strTabla, "@#@");
                            if (aTabla[0] == "OK")
                            {
                                this.strTblBodyFijoHTML = aTabla[1];
                                this.strTblBodyMovilHTML = aTabla[2];
                            }
                            else Master.sErrores += Errores.mostrarError(aTabla[1]);
                        }
                    }
                    else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                }
                catch (Exception ex)
                {
                    Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
                }
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
                //sResultado += obtenerPE(Session["UsuarioActual"].ToString(), aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21], aArgs[22]);
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
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

    private string obtenerDatos(string sDesde, string sHasta, string sNivelEstructura,
                                string sEstado, string sCategoria, string sCualidad, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P, string sPSN)
    {
        string sFecha;
        bool bHayDatos = false;
        double dDesviacion = 0;

        var sComun = "";
        StringBuilder sbA = new StringBuilder();
        StringBuilder sbB = new StringBuilder();
        DateTime? oDT1 = null, oDT2 = null, oDT3 = null;
        int nTiempoBD = 0;
        int nTiempoHTML = 0;

        try
        {
            sbA.Append("<table id='tblBodyFijo' class='texto MA' style='width:375px; text-align:right;'>");
            sbA.Append("<colgroup>");
            sbA.Append("<col style='width:20px' />");
            sbA.Append("<col style='width:20px' />");
            sbA.Append("<col style='width:20px' />");
            sbA.Append("<col style='width:60px;' />");//Nº Proyecto
            sbA.Append("<col style='width:255px;' />");//Denominacion
            sbA.Append("</colgroup>");
            sbA.Append("<tbody>");

            sbB.Append("<table id='tblBodyMovil' class='texto MA' style='width:1135px; text-align:right;'>");
            sbB.Append("<colgroup>");
            sbB.Append("<col style='width:70px;' />");//Planificado.Total
            sbB.Append("<col style='width:60px;' />");//Planificado.Inicio
            sbB.Append("<col style='width:60px;' />");//Planificado.Fin
            sbB.Append("<col style='width:100px;' />");//Planificado.presupuesto

            sbB.Append("<col style='width:60px;' />");//IAP.Mes
            sbB.Append("<col style='width:65px;' />");//IAP.Acumulado
            sbB.Append("<col style='width:65px;' />");//IAP.Pendiente Estimado
            sbB.Append("<col style='width:65px;' />");//IAP.Total estimado
            sbB.Append("<col style='width:60px;' />");//IAP.Fin estimado

            sbB.Append("<col style='width:70px;' />");//Previsto.Total
            sbB.Append("<col style='width:70px;' />");//Pendiente.Total
            sbB.Append("<col style='width:60px;' />");//Previsto.Fin
            sbB.Append("<col style='width:40px;' />");//Previsto %

            sbB.Append("<col style='width:40px;' />");//Avance %
            sbB.Append("<col style='width:100px;' />");//Avance. Producido

            sbB.Append("<col style='width:50px;' />");// % CONSUMIDO
            sbB.Append("<col style='width:50px;' />");// % DESVIACION DE ESFUERZO
            sbB.Append("<col style='width:50px;' />");// % DESVIACION DE PLAZO

            sbB.Append("</colgroup>");

            sbB.Append("<tbody>");

            oDT1 = DateTime.Now;
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerAvanceTecnico((int)Session["UsuarioActual"],
                                                                      int.Parse(sDesde), int.Parse(sHasta),
                                                                    (sNivelEstructura == "0") ? null : (int?)int.Parse(sNivelEstructura),
                                                                    sEstado, sCategoria, sCualidad, sClientes, sResponsables, sNaturalezas,
                                                                    sHorizontal, sModeloContrato, sContrato, sIDEstructura, sSectores,
                                                                    sSegmentos, (sComparacionLogica == "1") ? true : false,
                                                                    sCNP, sCSN1P, sCSN2P, sCSN3P, sCSN4P, sPSN, Session["MONEDA_VDC"].ToString());
            oDT2 = DateTime.Now;
            while (dr.Read())
            {
                sComun = "<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' ";
                sComun += "PE='" + dr["t301_idproyecto"].ToString() + "' ";
                sComun += "desPE='" + dr["t301_denominacion"].ToString() + "' ";
                sComun += "ML='" + dr["modo_lectura"].ToString() + "' ";
                sComun += "categoria='" + dr["t301_categoria"].ToString() + "' ";
                sComun += "cualidad='" + dr["t305_cualidad"].ToString() + "' ";
                sComun += "estado='" + dr["t301_estado"].ToString() + "' ";
                sComun += "idNodo='" + dr["t303_idnodo"].ToString() + "' ";
                sComun += "moneda_proyecto='" + dr["t422_idmoneda_proyecto"].ToString() + "' ";
                string sRtpt = ((bool)dr["rtpt"]) ? "1" : "0";
                sComun += "rtpt='" + sRtpt + "' ";
                sComun += "style='height:20px;' ondblclick='mdpsn(this)' ";
                sbA.Append(sComun);
/*
                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("PE='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("ML='" + dr["modo_lectura"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("idNodo='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("moneda_proyecto='" + dr["t422_idmoneda_proyecto"].ToString() + "' ");
                //sb.Append("rtpt='" + ((bool)dr["rtpt"]) ? "1" : "0" + "' ");
                string sRtpt = ((bool)dr["rtpt"]) ? "1" : "0";
                sb.Append("rtpt='" + sRtpt + "' ");
                //sb.Append("sNodo='" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "' ");
                sb.Append("style='height:20px;' onclick='ms(this)' ondblclick='mdpsn(this)'>");
*/
                sbA.Append("onclick='setFilaFija(this)'>");

                sbA.Append("<td></td>");// Categoría (producto o servicio)
                sbA.Append("<td></td>");//Cualidad (contratante, replicado,...)
                sbA.Append("<td></td>");//Estado (abiero, cerrado,...

                sbA.Append("<td style='padding-right:5px;' class='tdbr'");
                if (ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
                    sbA.Append(" title=\"" + dr["desmotivo"].ToString() + "\"");
                sbA.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sbA.Append("<td style='padding-left:3px; text-align:left;'><nobr class='NBR W240' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Moneda:</label>" + dr["t422_denominacion_proyecto"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sbA.Append("</tr>");

                sbB.Append(sComun);
                sbB.Append("onclick='setFilaMovil(this)'>");

                sbB.Append("<td class='tdbrl'>");
                if (double.Parse(dr["TotalPlanificado"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalPlanificado"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sFecha = dr["FechaInicioPlanificado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaInicioPlanificado"].ToString()).ToShortDateString();
                else sFecha = "&nbsp;";
                sbB.Append("<td class='tdbr'>" + sFecha + "</td>");

                sFecha = dr["FechaFinPlanificado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaFinPlanificado"].ToString()).ToShortDateString();
                else sFecha = "&nbsp;";
                sbB.Append("<td class='tdbr'>" + sFecha + "</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["TotalPresupuesto"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalPresupuesto"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["EsfuerzoMes"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoMes"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
//                    sbB.Append("<td style='background-color:#F58D8D;' class='tdbr'>");
                    sbB.Append("<td class='tdbr SR'>");
                else
                    sbB.Append("<td class='tdbr'>");

                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["PendienteEstimado"].ToString()) > 0) sbB.Append(double.Parse(dr["PendienteEstimado"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                else sFecha = "&nbsp;";
                sbB.Append("<td class='tdbr'>" + sFecha + "</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalPrevisto"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0) sbB.Append((double.Parse(dr["TotalPrevisto"].ToString()) - double.Parse(dr["EsfuerzoTotalAcumulado"].ToString())).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sFecha = dr["FinPrevisto"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinPrevisto"].ToString()).ToShortDateString();
                else sFecha = "&nbsp;";
                sbB.Append("<td class='tdbr'>" + sFecha + "</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["PorcPrevisto"].ToString()) > 0) sbB.Append(double.Parse(dr["PorcPrevisto"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["PorcAvance"].ToString()) > 0) sbB.Append(double.Parse(dr["PorcAvance"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");

                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["Producido"].ToString()) > 0) sbB.Append(double.Parse(dr["Producido"].ToString()).ToString("N"));
                else sbB.Append("&nbsp;");
                sbB.Append("</td>");


                //CONJUNTO DE INDICADORES
                sbB.Append("<td class='tdbr'>");
                if (double.Parse(dr["PorcConsumido"].ToString()) > 0)
                    sbB.Append(double.Parse(dr["PorcConsumido"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");
                bHayDatos = false;
                //Tot. previsto y Tot. planificado != 0
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPlanificado"].ToString()) > 0) 
                {
                    bHayDatos = true;
                }
                //% Desviación esfuerzos
                sbB.Append("<td ");
                if (bHayDatos)
                {
                    dDesviacion = double.Parse(dr["PorcDesviacion"].ToString());
                    if (dDesviacion <= 5) sbB.Append(" class='tdbr SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sbB.Append(" class='tdbr SA'");
                    else if (dDesviacion > 20) sbB.Append(" class='tdbr SR'");
                    sbB.Append(">" + double.Parse(dr["PorcDesviacion"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sbB.Append("class='tdbr'></td>");
                //% Desviación plazos
                sbB.Append("<td ");
                if (dr["PorcPlazo"].ToString() != "")
                {
                    dDesviacion = double.Parse(dr["PorcPlazo"].ToString());
                    if (dDesviacion <= 5) sbB.Append(" class='tdbr SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sbB.Append(" class='tdbr SA'");
                    else if (dDesviacion > 20) sbB.Append(" class='tdbr SR'");
                    sbB.Append(">" + double.Parse(dr["PorcPlazo"].ToString()).ToString("N") + "</td>");//% Desviación plazos
                }
                else
                    sbB.Append(" class='tdbr'></td>");//% Desviación plazos

                //sb.Append("<td>");
                //if (double.Parse(dr["ProducidoMes"].ToString()) > 0) sb.Append(double.Parse(dr["ProducidoMes"].ToString()).ToString("N"));
                //else sb.Append("&nbsp;");
                //sb.Append("</td>");

                //sb.Append("<td>");
                //if (double.Parse(dr["TotalProducido"].ToString()) > 0) sb.Append(double.Parse(dr["TotalProducido"].ToString()).ToString("N"));
                //else sb.Append("&nbsp;");
                //sb.Append("</td>");

                //sb.Append("<td style=\"border-right:''\">");
                //if (double.Parse(dr["PorcProducido"].ToString()) > 0) sb.Append(double.Parse(dr["PorcProducido"].ToString()).ToString("N"));
                //else sb.Append("&nbsp;");
                //sb.Append("</td>");

                sbB.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sbA.Append("</tbody>");
            sbA.Append("</table>");

            sbB.Append("</tbody>");
            sbB.Append("</table>");

            oDT3 = DateTime.Now;
            nTiempoBD = Fechas.DateDiff("mm", (DateTime)oDT1, (DateTime)oDT2);
            nTiempoHTML = Fechas.DateDiff("mm", (DateTime)oDT2, (DateTime)oDT3);

            return "OK@#@" + sbA.ToString() + "@#@" + sbB.ToString() + "@#@"
                            + nTiempoBD.ToString() + "@#@"
                            + nTiempoHTML.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos económicos", ex);
        }
    }

    private string setPreferencia(string sEstado, string sCategoria, string sCualidad, string sCerrarAuto, string sActuAuto, string sOpcionPeriodo, string sOperadorLogico, string sValoresMultiples)
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 3,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        (sEstado == "") ? null : sEstado,
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 3);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar las preferencias", ex);
        }
    }
    private string getPreferencia(string sIdPrefUsuario)
    {
        StringBuilder sb = new StringBuilder();
        string sEstado = "";
        int idPrefUsuario = 0;
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 3);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["categoria"].ToString() + "@#@"); //4
                sb.Append(dr["cualidad"].ToString() + "@#@"); //5
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //6
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //7
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //6
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //7
                sEstado = dr["estado"].ToString();
                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());
            }
            dr.Close();
            dr.Dispose();
            #region Fechas
            switch (nUtilidadPeriodo)
            {
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
                    //case 15: //strMagnitudes  break;
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

            sb.Append(sSubnodos + "@#@"); //12
            sb.Append(strHTMLAmbito + "@#@"); //13
            sb.Append(strIDsAmbito + "@#@"); //14
            sb.Append(strHTMLResponsable + "@#@"); //15
            sb.Append(strIDsResponsable + "@#@"); //16
            sb.Append(strHTMLNaturaleza + "@#@"); //17
            sb.Append(strIDsNaturaleza + "@#@"); //18
            sb.Append(strHTMLModeloCon + "@#@"); //19
            sb.Append(strIDsModeloCon + "@#@"); //20
            sb.Append(strHTMLHorizontal + "@#@"); //21
            sb.Append(strIDsHorizontal + "@#@"); //22
            sb.Append(strHTMLSector + "@#@"); //23
            sb.Append(strIDsSector + "@#@"); //24
            sb.Append(strHTMLSegmento + "@#@"); //25
            sb.Append(strIDsSegmento + "@#@"); //26
            sb.Append(strHTMLCliente + "@#@"); //27
            sb.Append(strIDsCliente + "@#@"); //28
            sb.Append(strHTMLContrato + "@#@"); //29
            sb.Append(strIDsContrato + "@#@"); //30
            sb.Append(strHTMLQn + "@#@"); //31
            sb.Append(strIDsQn + "@#@"); //32
            sb.Append(strHTMLQ1 + "@#@"); //33
            sb.Append(strIDsQ1 + "@#@"); //34
            sb.Append(strHTMLQ2 + "@#@"); //35
            sb.Append(strIDsQ2 + "@#@"); //36
            sb.Append(strHTMLQ3 + "@#@"); //37
            sb.Append(strIDsQ3 + "@#@"); //38
            sb.Append(strHTMLQ4 + "@#@"); //39
            sb.Append(strIDsQ4 + "@#@"); //40
            sb.Append(strHTMLProyecto + "@#@"); //41
            sb.Append(strIDsProyecto + "@#@"); //42
            sb.Append(sEstado); //43

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
    private string setResolucion()
    {
        try
        {
            Session["AVANTEC1024"] = !(bool)Session["AVANTEC1024"];

            USUARIO.UpdateResolucion(7, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["AVANTEC1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
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
            // SqlDataReader dr = ConsultasPGE.ObtenerCombosDatosResumidosCriterios((int)Session["UsuarioActual"], nDesde, nHasta);
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
}
