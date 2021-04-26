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
    public string strTablaHTML = "<table id='tblDatos'></table>";//, sLiteralCDPA = "Cualificador A", sLiteralCDPB = "Cualificador B";
    public SqlConnection oConn;
    public SqlTransaction tr;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 0;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto="";
    public string strMagnitudes = "";
    public short nPantallaPreferencia = 12;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (!(bool)Session["DATOSRES1024"])
                {
                    Master.nResolucion = 1280;
                }
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Resumen";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");

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

                lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
                lblMonedaImportes2.InnerText = Session["DENOMINACION_VDC"].ToString();

                //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                //{
                    divMonedaImportes.Style.Add("visibility", "visible");
                    divMonedaImportes2.Style.Add("visibility", "visible");
                //}

                if ((bool)Session["CALCULOONLINE"])
                {
                    rdbResultadoCalculo.Items[0].Selected = true;
                    rdbResultadoCalculo2.Items[0].Selected = true;
                }
                else
                {
                    rdbResultadoCalculo.Items[1].Selected = true;
                    rdbResultadoCalculo2.Items[1].Selected = true;
                }

                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                #region Lectura de preferencia
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    sHayPreferencia = "true";
                    cboConceptoEje.SelectedValue = aDatosPref[2];
                    cboCategoria.SelectedValue = aDatosPref[3];
                    cboCualidad.SelectedValue = aDatosPref[4];
                    chkCerrarAuto.Checked = (aDatosPref[5] == "1") ? true : false;
                    chkActuAuto.Checked = (aDatosPref[6] == "1") ? true : false;
                    //if (chkActuAuto.Checked) btnObtener.Disabled = true;
                    if (aDatosPref[7] == "1") rdbOperador.Items[0].Selected = true;
                    else rdbOperador.Items[1].Selected = true;

                    nUtilidadPeriodo = int.Parse(aDatosPref[8]);
                    hdnDesde.Text = aDatosPref[9];
                    txtDesde.Text = aDatosPref[10];
                    hdnHasta.Text = aDatosPref[11];
                    txtHasta.Text = aDatosPref[12];
                    sSubnodos = aDatosPref[14];

                    ContentPlaceHolder oCPHC = (ContentPlaceHolder)Master.FindControl("CPHC");
                    string[] aMagnitudes = Regex.Split(aDatosPref[43], "///");
                    foreach (string oMagnitud in aMagnitudes)
                    {
                        if (oMagnitud == "") continue;
                        string[] aDatos = Regex.Split(oMagnitud, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    if (chkActuAuto.Checked)
                    {
                        string strTabla = obtenerDatos(aDatosPref[13],
                                            hdnDesde.Text,
                                            hdnHasta.Text,
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
                                            aDatosPref[7],
                                            "1",
                                            (chkV1.Checked) ? "1" : "0",
                                            (chkV2.Checked) ? "1" : "0",
                                            (chkV3.Checked) ? "1" : "0",
                                            (chkV4.Checked) ? "1" : "0",
                                            (chkV5.Checked) ? "1" : "0",
                                            (chkV6.Checked) ? "1" : "0",
                                            (chkV7.Checked) ? "1" : "0",
                                            (chkV8.Checked) ? "1" : "0",
                                            (chkV9.Checked) ? "1" : "0",
                                            strIDsQn,
                                            strIDsQ1,
                                            strIDsQ2,
                                            strIDsQ3,
                                            strIDsQ4,
                                            strIDsProyecto);
                            
                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] == "OK")
                        {
                            this.strTablaHTML = aTabla[1];
                            this.totalV1.InnerText = decimal.Parse(aTabla[2]).ToString("N");
                            if (decimal.Parse(aTabla[2]) < 0) totalV1.Style.Add("color", "red");

                            this.totalV2.InnerText = decimal.Parse(aTabla[3]).ToString("N");
                            if (decimal.Parse(aTabla[3]) < 0) totalV2.Style.Add("color", "red");

                            this.totalV3.InnerText = decimal.Parse(aTabla[4]).ToString("N");
                            if (decimal.Parse(aTabla[4]) < 0) totalV3.Style.Add("color", "red");

                            this.totalV4.InnerText = decimal.Parse(aTabla[5]).ToString("N");
                            if (decimal.Parse(aTabla[5]) < 0) totalV4.Style.Add("color", "red");

                            this.totalV5.InnerText = decimal.Parse(aTabla[6]).ToString("N");
                            if (decimal.Parse(aTabla[6]) < 0) totalV5.Style.Add("color", "red");

                            this.totalV6.InnerText = decimal.Parse(aTabla[7]).ToString("N");
                            if (decimal.Parse(aTabla[7]) < 0) totalV6.Style.Add("color", "red");

                            this.totalV7.InnerText = decimal.Parse(aTabla[8]).ToString("N");
                            if (decimal.Parse(aTabla[8]) < 0) totalV7.Style.Add("color", "red");

                            this.totalV8.InnerText = decimal.Parse(aTabla[9]).ToString("N");
                            if (decimal.Parse(aTabla[9]) < 0) totalV8.Style.Add("color", "red");

                            this.totalV9.InnerText = decimal.Parse(aTabla[10]).ToString("N");
                            if (decimal.Parse(aTabla[10]) < 0) totalV9.Style.Add("color", "red");
                        }
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                }
                else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                #endregion

                string[] aCriterios = Regex.Split(cargarCriterios(int.Parse(hdnDesde.Text), int.Parse(hdnHasta.Text)), "@#@");
                if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
                else Master.sErrores = aCriterios[1];

                //sCriterios = "var js_cri = new Array();\n";

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
            case ("excel"):
                sResultado += excel(aArgs[1]);
                break;
            case ("buscar"):
            case ("buscar2"):
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21], aArgs[22], aArgs[23], aArgs[24], aArgs[25], aArgs[26], aArgs[27], aArgs[28], aArgs[29], aArgs[30], aArgs[31], aArgs[32]);
                break;
            case ("getTablaCriterios"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
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
            case ("setResultadoOnline"):
                Session["CALCULOONLINE"] = (aArgs[1] == "1") ? true : false;
                sResultado += "OK";
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

            return "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }

    private string obtenerDatos(string sConceptoEje, string sDesde, string sHasta, string sNivelEstructura,
                                string sCategoria, string sCualidad, string sClientes, string sResponsables, 
                                string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica, string sNivelIndentacion,
                                string sV1, string sV2, string sV3, string sV4, string sV5, 
                                string sV6, string sV7, string sV8, string sV9, string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P, string sPSN)
    {

        //decimal nIngNetos = 0, nMargen = 0, nCobros = 0, nGastos = 0, nIngresos = 0, nVolumen = 0;
        decimal nTotalV1 = 0, nTotalV2 = 0, nTotalV3 = 0, nTotalV4 = 0, nTotalV5 = 0, nTotalV6 = 0, nTotalV7 = 0, nTotalV8 = 0, nTotalV9 = 0;
        int nNivelIndentacion = int.Parse(sNivelIndentacion) + 1;
        int nWidthTabla = 370;
        int nColumnasSegunResolucion = ((bool)Session["DATOSRES1024"]) ? 6 : 8;
        int nColumnasVisibles = 0;
        int nColumnasACrear = 0;
        StringBuilder sb = new StringBuilder();
        string sColor = "";
        string sFormulas = "";
        DateTime? oDT1 = null, oDT2 = null, oDT3 = null;
        int nTiempoBD = 0;
        int nTiempoHTML = 0;
        
        try
        {
            if (sV6 == "1")
            {
                nColumnasVisibles++;
                sFormulas += "8,2,";
            }
            if (sV1 == "1")
            {
                nColumnasVisibles++;
                if (sV6 == "0") sFormulas += "8,";
            }
            if (sV2 == "1")
            {
                nColumnasVisibles++;
                sFormulas += "7,";
            }
            if (sV3 == "1")
            {
                nColumnasVisibles++;
                sFormulas += "1,";
            }
            if (sV4 == "1")
            {
                nColumnasVisibles++;
                sFormulas += "6,";
            }
            if (sV5 == "1")
            {
                nColumnasVisibles++;
                if (sV6 == "0") sFormulas += "2,";
            }
            if (sV7 == "1")
            {
                nColumnasVisibles++;
                sFormulas += "5,";
            }
            if (sV8 == "1")
            {
                nColumnasVisibles++;
                sFormulas += "9,";
            }
            if (sV9 == "1")
            {
                nColumnasVisibles++;
                sFormulas += "10";
            }

            if (nColumnasVisibles < nColumnasSegunResolucion) nColumnasACrear = nColumnasSegunResolucion - nColumnasVisibles;

            if (sNivelIndentacion == "1")
            {
                nWidthTabla = nWidthTabla + (nColumnasVisibles * 100);// +nColumnasACrear * 100;
                sb.Append("<table id='tblDatos' class='texto' style='width:" + nWidthTabla.ToString() + "px;'>");
                sb.Append("<colgroup>");
                sb.Append("<col style='width:370px;' />");
                if (sV1 == "1") sb.Append("<col style='width:100px;' />");
                if (sV2 == "1") sb.Append("<col style='width:100px;' />");
                if (sV3 == "1") sb.Append("<col style='width:100px;' />");
                if (sV4 == "1") sb.Append("<col style='width:100px;' />");
                if (sV5 == "1") sb.Append("<col style='width:100px;' />");
                if (sV6 == "1") sb.Append("<col style='width:100px;' />");
                if (sV7 == "1") sb.Append("<col style='width:100px;' />");
                if (sV8 == "1") sb.Append("<col style='width:100px;' />");
                if (sV9 == "1") sb.Append("<col style='width:100px;' />");

                //for (int i = 0; i < nColumnasACrear; i++)
                //{
                //    sb.Append("<col style='width:100px; text-align:right;' />");
                //}
                sb.Append("</colgroup>");
                sb.Append("<tbody>");
            }

            oDT1 = DateTime.Now;
            SqlDataReader dr = ConsultasPGE.ObtenerDatosResumidos(int.Parse(sConceptoEje),
                (int)Session["UsuarioActual"],
                int.Parse(sDesde), int.Parse(sHasta),
                (sNivelEstructura=="0")? null:(int?)int.Parse(sNivelEstructura),
                sCategoria,
                sCualidad,
                sClientes,
                sResponsables,
                sNaturalezas,
                sHorizontal,
                sModeloContrato,
                sContrato,
                sIDEstructura,
                sSectores,
                sSegmentos,
                (sComparacionLogica=="1")? true:false,
                sCNP,
                sCSN1P,
                sCSN2P,
                sCSN3P,
                sCSN4P,
                sPSN,
                sFormulas,
                Session["MONEDA_VDC"].ToString()
                );
            oDT2 = DateTime.Now;
            while (dr.Read())
            {
                if (dr["codigo"] == DBNull.Value)
                {
                    nTotalV1 = decimal.Parse(dr["Volumen_de_Negocio"].ToString());
                    nTotalV2 = decimal.Parse(dr["Total_Ingresos"].ToString());
                    nTotalV3 = decimal.Parse(dr["Ingresos_Netos"].ToString());
                    nTotalV4 = decimal.Parse(dr["Total_Gastos"].ToString());
                    nTotalV5 = decimal.Parse(dr["Margen"].ToString());
                    nTotalV6 = decimal.Parse(dr["Ratio"].ToString());
                    nTotalV7 = decimal.Parse(dr["Total_Cobros"].ToString());
                    nTotalV8 = decimal.Parse(dr["Otros_consumos"].ToString());
                    nTotalV9 = decimal.Parse(dr["Consumo_recursos"].ToString());
                }
                else
                {
                    sb.Append("<tr id='" + dr["codigo"].ToString() + "' ");
                    sb.Append("tipo=" + sConceptoEje + " ");
                    sb.Append("nivel=" + nNivelIndentacion.ToString() + " ");
                    sb.Append("desplegado=0 ");
                    sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                    sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                    sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                    sb.Append("tiporecurso='" + dr["tiporecurso"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("cr='" + dr["t303_denominacion"].ToString() + "' ");
                    sb.Append("responsable='" + dr["responsable"].ToString() + "' ");
                    sb.Append("clienteProy='" + dr["t302_denominacion"].ToString().Replace("'", "&#39;") + "' ");
                    sb.Append("style='height:20px;'>");

                    sb.Append("<td style='padding-left:2px;'>");
                    if (sConceptoEje != "11")
                    {
                        //sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' class='NSEG" + sNivelIndentacion + "' style='cursor:pointer; margin-right:22px;'>");
                        sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' class='NSEG" + sNivelIndentacion + "' style='cursor:pointer;'>");
                    }
                    else
                        sb.Append("<img src='../../../../Images/imgSeparador.gif' class='NSEG" + sNivelIndentacion + "' style='width:9px;height:9px;'>");

                    sb.Append("<nobr ");
                    //switch (sNivelIndentacion)
                    //{
                    //    case "1": sb.Append("class='NBR W320'>"); break;
                    //    case "2": sb.Append("class='NBR W310'>"); break;
                    //    case "3": sb.Append("class='NBR W280'>"); break;
                    //    case "4": sb.Append("class='NBR W260'>"); break;
                    //    case "5": sb.Append("class='NBR W240'>"); break;
                    //    case "6": sb.Append("class='NBR W220'>"); break;
                    //    case "7": sb.Append("class='NBR W200'>"); break;
                    //    case "8": sb.Append("class='NBR W180'>"); break;
                    //    case "9": sb.Append("class='NBR W180'>"); break;
                    //}
                    int nWidth = 320;
                    switch (sNivelIndentacion)
                    {
                        case "1": nWidth = 320; break;
                        case "2": nWidth = 310; break;
                        case "3": nWidth = 280; break;
                        case "4": nWidth = 260; break;
                        case "5": nWidth = 240; break;
                        case "6": nWidth = 220; break;
                        case "7": nWidth = 200; break;
                        case "8": nWidth = 180; break;
                        case "9": nWidth = 180; break;
                    }

                    if (sConceptoEje == "10") nWidth = nWidth - 40;
                    sb.Append("class='NBR W" + nWidth.ToString() + "' ");

                    if (sConceptoEje == "10")
                        sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    else
                        sb.Append(" onmouseover='TTip(event)' ");

                    sb.Append(">");

                    switch (sConceptoEje)
                    {
                        case "10": sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["denominacion"].ToString()); break;
                        case "11": sb.Append(Fechas.AnnomesAFechaDescLarga(int.Parse(dr["denominacion"].ToString()))); break;
                        default: sb.Append(dr["denominacion"].ToString()); break;
                    }

                    sb.Append("</nobr></td>");

                    if (sV1 == "1")
                    {
                        if (decimal.Parse(dr["Volumen_de_Negocio"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Volumen_de_Negocio"].ToString()).ToString("N") + "</td>");
                    }
                    if (sV2 == "1")
                    {
                        if (decimal.Parse(dr["Total_Ingresos"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Total_Ingresos"].ToString()).ToString("N") + "</td>");
                    }
                    if (sV3 == "1")
                    {
                        if (decimal.Parse(dr["Ingresos_Netos"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Ingresos_Netos"].ToString()).ToString("N") + "</td>");
                    }
                    if (sV4 == "1")
                    {
                        if (decimal.Parse(dr["Total_Gastos"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Total_Gastos"].ToString()).ToString("N") + "</td>");
                    }
                    if (sV5 == "1")
                    {
                        if (decimal.Parse(dr["Margen"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Margen"].ToString()).ToString("N") + "</td>");
                    }
                    if (sV6 == "1")
                    {
                        if (decimal.Parse(dr["Ratio"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Ratio"].ToString()).ToString("N") + " %</td>");
                    }
                    if (sV7 == "1")
                    {
                        if (decimal.Parse(dr["Total_Cobros"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Total_Cobros"].ToString()).ToString("N") + "</td>");
                    } 
                    if (sV8 == "1")
                    {
                        if (decimal.Parse(dr["Otros_consumos"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Otros_consumos"].ToString()).ToString("N") + "</td>");
                    }
                    if (sV9 == "1")
                    {
                        if (decimal.Parse(dr["Consumo_recursos"].ToString()) < 0) sColor = "textoR";
                        else sColor = "";
                        sb.Append("<td class='" + sColor + "' style='text-align:right;'>" + decimal.Parse(dr["Consumo_recursos"].ToString()).ToString("N") + "</td>");
                    }

                    sb.Append("</tr>");
                }
            }
            dr.Close();
            dr.Dispose();

            if (sNivelIndentacion == "1")
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
            }

            oDT3 = DateTime.Now;
            nTiempoBD = Fechas.DateDiff("mm", (DateTime)oDT1, (DateTime)oDT2);
            nTiempoHTML = Fechas.DateDiff("mm", (DateTime)oDT2, (DateTime)oDT3);
            return "OK@#@" + sb.ToString() + "@#@"
                            + nTotalV1.ToString() + "@#@"
                            + nTotalV2.ToString() + "@#@"
                            + nTotalV3.ToString() + "@#@"
                            + nTotalV4.ToString() + "@#@"
                            + nTotalV5.ToString() + "@#@"
                            + nTotalV6.ToString() + "@#@"
                            + nTotalV7.ToString() + "@#@"
                            + nTotalV8.ToString() + "@#@"
                            + nTotalV9.ToString() + "@#@"
                            + nTiempoBD.ToString() + "@#@"
                            + nTiempoHTML.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos económicos.", ex);
        }
    }

    private string setResolucion()
    {
        try
        {
            Session["DATOSRES1024"] = !(bool)Session["DATOSRES1024"];

            USUARIO.UpdateResolucion(4, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["DATOSRES1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }

    private string setPreferencia(string sConceptoEje, string sCategoria, string sCualidad, string sCerrarAuto, string sActuAuto, string sOpcionPeriodo, string sOperadorLogico, string sValoresMultiples)
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 12,
                                        (sConceptoEje=="")? null:sConceptoEje,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 12);
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
        int idPrefUsuario = 0, nConceptoEje=0;
        int nOpcion = 1;
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "")? null:(int?)int.Parse(sIdPrefUsuario),
                                                        (int)Session["IDFICEPI_PC_ACTUAL"], 12);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["ConceptoEje"].ToString() + "@#@"); //2
                sb.Append(dr["categoria"].ToString() + "@#@"); //3
                sb.Append(dr["cualidad"].ToString() + "@#@"); //4
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //5
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //6
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //7
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //8
                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nConceptoEje = (dr["ConceptoEje"].ToString()=="")? 0:int.Parse(dr["ConceptoEje"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());
            }
            dr.Close();
            //dr.Dispose();

            #region Fechas
            switch (nUtilidadPeriodo)
            {
                case 1:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + 12).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 12) + "@#@");//12
                    break;
                case 2:
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//12
                    break;
                case 3:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//12
                    break;
                case 4:
                    sb.Append("199001" + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//12
                    break;
                case 5:
                    sb.Append("199001" + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//10
                    sb.Append("207812" + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(207812) + "@#@");//12
                    break;
                default:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//9
                    sb.Append(mes[0] + " " + DateTime.Now.Year.ToString() + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString() + "@#@");//12
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
                        //strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:16px;'><td>";
                        strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:18px;' idAux='";
                        strHTMLAmbito += SUBNODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLAmbito += "'><td>";

                        switch (int.Parse(aID[0]))
                        {
                            case 1: strHTMLAmbito += "<img src='../../../../images/imgSN4.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 2: strHTMLAmbito += "<img src='../../../../images/imgSN3.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 3: strHTMLAmbito += "<img src='../../../../images/imgSN2.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 4: strHTMLAmbito += "<img src='../../../../images/imgSN1.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 5: strHTMLAmbito += "<img src='../../../../images/imgNodo.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
                            case 6: strHTMLAmbito += "<img src='../../../../images/imgSubNodo.gif' style='margin-left:2px; margin-right:4px; vertical-align:middle; border:0px; height:16px;'>"; break;
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
                        if (strMagnitudes != "") strMagnitudes += "///";
                        strMagnitudes += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
                        break;
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

            if (nConceptoEje >= 7)
            {
                nOpcion = nConceptoEje;
            }
            else
            {
                if (nNivelMinimo != 0) nOpcion = nNivelMinimo;
                else nOpcion = nEstructuraMinima;
            }

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
            sb.Append(strMagnitudes + "@#@"); //43
            sb.Append(strHTMLProyecto + "@#@"); //44
            sb.Append(strIDsProyecto + "@#@"); //45

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }

    private string excel(string sHtml)
    {
        try
        {
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sHtml;

            SUPER.DAL.Log.Insertar("ECO/Consultas/DatosResumidos->Excel. " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString());

            return "OK@#@" + sIdCache;
        }
        catch (Exception ex)
        {
            SUPER.DAL.Log.Insertar("ECO/Consultas/DatosResumidos->Excel. " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " Error " + ex.Message);
            return "Error@#@" + Errores.mostrarError("Error al generar el Excel.", ex);
        }
    }

}
