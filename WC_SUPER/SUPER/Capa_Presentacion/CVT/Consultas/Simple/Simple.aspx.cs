using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using SUPER.BLL;





public partial class Capa_Presentacion_CVT_Consultas_Simple : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sEstructura = "";
    //public string strHTMLUniNeg = "", strHTMLCenRes = "", strHTMLPerPro = "", strHTMLTitAca = "", strHTMLEntTecFor = "", strHTMLCertificado = "", strHTMLIdioma = "", strHTMLTitIdi = "", strHTMLConSec = "", strHTMLConTec = "", strHTMLEntTecExp = "", strHTMLPerExp = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strHTMLEntTecFor = "", strHTMLEntTecExp = "";
    //public string strIDsEntTecFor = "", strIDsEntTecExp = "";
    //Para la pestaña Avanzada
    public string strHTMLAvanProf = "", strHTMLAvanPerf = "", strHTMLAvanEntTecForObl = "",
                  strHTMLAvanEntTecForOpc = "", strHTMLAvanTitObl = "", strHTMLAvanTitOpc = "",
                  strHTMLAvanIdioObl = "", strHTMLAvanIdioOpc = "", strHTMLAvanCertObl = "", strHTMLAvanCertOpc="",
                  strHTMLAvanCertEntObl = "", strHTMLAvanCertEntOpc="", strHTMLAvanCursoObl = "", strHTMLAvanCursoOpc = "", 
                  strHTMLAvanExpPerfObl = "", strHTMLAvanExpPerfOpc = "", strHTMLAvanEntTecExpObl = "", strHTMLAvanEntTecExpOpc = "";
    //Para las pestañas de filtro por documentacion
    public string strTablaHTMLFiltroCert = "", strTablaHTMLFiltroFA = "", strTablaHTMLFiltroCurso = "", strTablaHTMLFiltroIdioma = "";

    public string sCriterios = "";//, sHayPreferencia = "false";
    //public short nPantallaPreferencia = 36;
    //public short nPantPrefBasica = 40, nPantPrefAvanzada = 41, nPantPrefCadena = 42;
    //public string sIdPreferenciaBasica = "-1", sIdPreferenciaAvanzada = "-1", sIdPreferenciaCadena = "-1";    

    public bool? cvConsultaExternos = null, cvConsultaBaja = null, cvConsultaCoste = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // All webpages displaying an ASP.NET menu control must inherit this class.
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

        if (!Page.IsCallback)
        {
            Master.nBotonera = 61;
            Master.TituloPagina = "Consulta de Currículums";
            Master.bFuncionesLocales = true;
            Master.bEstilosLocales = true;
            Master.bContienePestanas = true;
            //Master.FuncionesJavaScript.Add("Javascript/jquery.js");
            //Master.FuncionesJavaScript.Add("Javascript/jquery.autocomplete.js");

            //La exportación a IBERDOK de momento solo visible para DIS, Maria y Hugo
            //if (Session["ESDIS_ENTRADA"].ToString() == "S"
            //        || Session["IDRED_ENTRADA"].ToString() == "DOBEPAMA"
            //        || Session["IDRED_ENTRADA"].ToString() == "DOCRMAHU"
            //    )
            //    tsPestanas.Items[2].Visible = true;

            //Para el ToolTip extendido
            //Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
            Master.FuncionesJavaScript.Add("Javascript/documentos.js");

            //Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1/jquery-1.7.1.js");
            //Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js");
            Master.FuncionesJavaScript.Add("Javascript/jquery.autocomplete.js");
            Master.FicherosCSS.Add("App_Themes/Corporativo/jquery-ui-1.8.17.custom.css");
            Master.Modulo = "CVT";

            hdnNumExp.Value = ConfigurationManager.AppSettings["numExportaOnline"].ToString();
            segMediaCV.Value = ConfigurationManager.AppSettings["segMediaCV"].ToString();
            hdnDestinatarios.Value = Session["IDRED_ENTRADA"].ToString();
            hdnDestinatarioIdFicepi.Value = Session["IDFICEPI_ENTRADA"].ToString();
            if (Session["CVCONSULTAEXTERNOS"] == null || Session["IDFICEPI_CVT_ACTUAL"].ToString() != Session["IDFICEPI_ENTRADA"].ToString())
            {
                Curriculum.consultaFicepi((int)Session["IDFICEPI_CVT_ACTUAL"], out cvConsultaExternos, out cvConsultaBaja, out cvConsultaCoste);
                //cvConsultaExternos=true;
                //cvConsultaBaja=true;
                //cvConsultaCoste=true;
                Session["CVCONSULTAEXTERNOS"] = cvConsultaExternos;
                Session["CVCONSULTABAJA"] = cvConsultaBaja;
                Session["CVCONSULTACOSTE"] = cvConsultaCoste;
            }
            else
            {
                cvConsultaExternos = (bool)Session["CVCONSULTAEXTERNOS"];
                cvConsultaBaja = (bool)Session["CVCONSULTABAJA"];
                cvConsultaCoste = (bool)Session["CVCONSULTACOSTE"];
            }
            if (cvConsultaExternos == null || cvConsultaExternos == false)
            {
                divTipo.Attributes.Add("class", "ocultarcapa");
                divTipoC.Attributes.Add("class", "ocultarcapa");
                divTipoQ.Attributes.Add("class", "ocultarcapa");
                divAvanTipo.Attributes.Add("class", "ocultarcapa");
            }
            if (cvConsultaBaja == null || cvConsultaBaja == false)
            {
                divEstado.Attributes.Add("class", "ocultarcapa");
                divEstadoC.Attributes.Add("class", "ocultarcapa");
                divEstadoQ.Attributes.Add("class", "ocultarcapa");
                divAvanEstado.Attributes.Add("class", "ocultarcapa");
            }
            if (cvConsultaCoste == null || cvConsultaCoste == false)
            {
                divCoste.Attributes.Add("class", "ocultarcapa");
                divCosteC.Attributes.Add("class", "ocultarcapa");
                divCosteQ.Attributes.Add("class", "ocultarcapa");
                divAvanCoste.Attributes.Add("class", "ocultarcapa");
            }
            CargarCombos();
            
            try
            {
                sEstructura = "var js_estructura = new Array();\n" + Curriculum.cargarEstructuraConsulta();
                sCriterios = "var js_cri = new Array();\n";
                //ComprobarExistenPreferencias(Session["IDFICEPI_ENTRADA"].ToString());
                #region Lectura de preferencia
                /*
                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    sHayPreferencia = "true";
                    hdnProfesional.Value = aDatosPref[2];
                    txtProfesional.Text = aDatosPref[3];
                    cboPerfilPro.SelectedValue = aDatosPref[4];
                    cboCentro.SelectedValue = aDatosPref[5];
                    cboIntTrayInt.SelectedValue = aDatosPref[6];
                    cboMovilidad.SelectedValue = aDatosPref[7];
                    txtGradoDisp.Text = aDatosPref[8];
                    cboTipo.SelectedValue = aDatosPref[9];
                    hdnSN4.Value = aDatosPref[10];
                    hdnSN3.Value = aDatosPref[11];
                    hdnSN2.Value = aDatosPref[12];
                    hdnSN1.Value = aDatosPref[13];
                    hdnCR.Value = aDatosPref[14];
                    cboEstado.SelectedValue = aDatosPref[15];
                    txtLimCoste.Text = aDatosPref[16];
                    hdnTitulo.Value = aDatosPref[17];
                    txtTitulo.Text = aDatosPref[18];
                    cboTipologia.SelectedValue = aDatosPref[19];
                    cboModalidad.SelectedValue = aDatosPref[20];
                    cboTics.SelectedValue = aDatosPref[21];
                    hdnCertificacion.Value = aDatosPref[22];
                    txtCertificacion.Text = aDatosPref[23];
                    cboIdioma.SelectedValue = aDatosPref[24];
                    cboNivel.SelectedValue = aDatosPref[25];
                    hdnCuenta.Value = aDatosPref[26];
                    txtCuenta.Text = aDatosPref[27];
                    cboSector.SelectedValue = aDatosPref[28];
                    cboPerfilExp.SelectedValue = aDatosPref[29];
                    txtCanTiempo.Text = aDatosPref[30];
                    cboMedTiempo.SelectedValue = aDatosPref[31];
                    cboAnoInicio.SelectedValue = aDatosPref[32];
                    //cboFormatos.SelectedValue = aDatosPref[33];
                    chkRestringir.Checked = (aDatosPref[34] == "1") ? true : false;
                    chkTituloAcre.Checked = (aDatosPref[35] == "1") ? true : false;

                    rdlFormacionEntorno.SelectedValue = aDatosPref[36];
                    rdlExperienciaEntorno.SelectedValue = aDatosPref[37];

                    // Hay que utilizar objetos <asp:CheckBox en vez de input type=check actuales
                    
                    ContentPlaceHolder oCPHC = (ContentPlaceHolder)Master.FindControl("CPHC");

                    string[] aDatosPersonales = Regex.Split(aDatosPref[40], "///");
                    foreach (string oDatosPersonales in aDatosPersonales)
                    {
                        if (oDatosPersonales == "") continue;
                        string[] aDatos = Regex.Split(oDatosPersonales, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    string[] aDatosOrganiza = Regex.Split(aDatosPref[41], "///");
                    foreach (string oDatosOrganiza in aDatosOrganiza)
                    {
                        if (oDatosOrganiza == "") continue;
                        string[] aDatos = Regex.Split(oDatosOrganiza, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    string[] aFormaAcade = Regex.Split(aDatosPref[42], "///");
                    foreach (string oFormaAcade in aFormaAcade)
                    {
                        if (oFormaAcade == "") continue;
                        string[] aDatos = Regex.Split(oFormaAcade, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    string[] aAccionesFormaRecib = Regex.Split(aDatosPref[43], "///");
                    foreach (string oAccionesFormaRecib in aAccionesFormaRecib)
                    {
                        if (oAccionesFormaRecib == "") continue;
                        string[] aDatos = Regex.Split(oAccionesFormaRecib, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    string[] aAccionesFormaImp = Regex.Split(aDatosPref[44], "///");
                    foreach (string oAccionesFormaImp in aAccionesFormaImp)
                    {
                        if (oAccionesFormaImp == "") continue;
                        string[] aDatos = Regex.Split(oAccionesFormaImp, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    string[] aCertifiExame = Regex.Split(aDatosPref[45], "///");
                    foreach (string oCertifiExame in aCertifiExame)
                    {
                        if (oCertifiExame == "") continue;
                        string[] aDatos = Regex.Split(oCertifiExame, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }
   
                    string[] aIdiomas = Regex.Split(aDatosPref[46], "///");
                    foreach (string oIdiomas in aIdiomas)
                    {
                        if (oIdiomas == "") continue;
                        string[] aDatos = Regex.Split(oIdiomas, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    string[] aExperienciaFuera = Regex.Split(aDatosPref[47], "///");
                    foreach (string oExperienciaFuera in aExperienciaFuera)
                    {
                        if (oExperienciaFuera == "") continue;
                        string[] aDatos = Regex.Split(oExperienciaFuera, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                    string[] aExperienciaIbermatica = Regex.Split(aDatosPref[48], "///");
                    foreach (string oExperienciaIbermatica in aExperienciaIbermatica)
                    {
                        if (oExperienciaIbermatica == "") continue;
                        string[] aDatos = Regex.Split(oExperienciaIbermatica, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }
                    string[] aSinopsis = Regex.Split(aDatosPref[49], "///");
                    foreach (string oSinopsis in aSinopsis)
                    {
                        if (oSinopsis == "") continue;
                        string[] aDatos = Regex.Split(oSinopsis, "##");
                        ///aDatos[0] = id
                        ///aDatos[1] = checked
                        ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    }

                }
                else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                
                 */
                #endregion
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
        }

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                //case "correo":
                //    sResultado += Correo(aArgs[1], aArgs[2], aArgs[3]);
                //    break;

                //case ("verHTML"):
                //    Session["FILTROS_HTML"] = aArgs[1].ToString();
                //    sResultado += "OK";
                //    break;
                //case ("setPreferenciaBasica"):
                //    sResultado += setPreferenciaBasica(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5],
                //                                 aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10],
                //                                 aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15],
                //                                 aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20],
                //                                 aArgs[21], aArgs[22], aArgs[23], aArgs[24], aArgs[25],
                //                                 aArgs[26], aArgs[27], aArgs[28], aArgs[29], aArgs[30],
                //                                 aArgs[31], aArgs[32], aArgs[33], aArgs[34], aArgs[35],
                //                                 aArgs[36], aArgs[37]
                //                );
                //    break;
                //case ("setPreferenciaAvanzada"):
                //    sResultado += setPreferenciaAvanzada(aArgs[1], aArgs[2], aArgs[3]);
                //    break;
                //case ("setPreferenciaCadena"):
                //    sResultado += setPreferenciaCadena(aArgs[1], aArgs[2]);
                //    break;
                //case ("delPreferencia"):
                //    sResultado += delPreferencia(aArgs[1]);
                //    break;
                //case ("getPreferencia"):
                //case ("getPreferenciaTotal"):
                //    sResultado += getPreferencia(aArgs[1], aArgs[2]);
                //    break;
                case ("cargarPlantillas"):
                    sResultado += "OK@#@" + Curriculum.cargarPlantillas();
                    break;
                case ("cargarCriterio"):
                    string sTipoAux = aArgs[1];
                    switch (aArgs[1])
                    {
                        case "41":
                            sTipoAux = "4";
                            break;
                        case "51"://Entornos tecnológicos
                        case "171":
                            sTipoAux = "5";
                            break;
                        case "71":
                            sTipoAux = "7";
                            break;
                        case "151"://Entidades certificadoras
                            sTipoAux = "15";
                            break;
                        case "16":
                        case "161":
                            sTipoAux = "3";
                            break;
                    }
                    sResultado += "OK@#@" + aArgs[1] + "@#@" + sTipoAux + "@#@" + CargarCriterio(aArgs[1]);
                    break;
                case ("cargarPerfiles"):
                    sResultado += "OK@#@" + aArgs[1] + "@#@" + aArgs[2] + "@#@" + CargarCriterio(aArgs[2]);
                    break;
                //case "getIdCertificados":
                //    sResultado += "OK@#@" + Certificado.GetIds(aArgs[1], "{valor}");
                //    break;
                //case "getTablaCertificados":
                //    sResultado += "OK@#@" + Certificado.GetTablaCriterios(aArgs[1], aArgs[2].Replace(";", "##"));
                //    break;
                case "getTablaDocs":
                    sResultado += "OK@#@";
                    // sListaFicepis = aArgs[1]; sListaFACod = aArgs[2]; sListaFADen = aArgs[3]; Curso Cod = aArgs[4]; Curso Den = aArgs[5]; 
                    //Certificado Cod = aArgs[6]; Certificado Den = aArgs[7]; Idioma Cod = aArgs[8]; Titulo Idioma Den = aArgs[9];

                    //Titulos de la formación académica
                    if (aArgs[2] != "" || aArgs[3] != "")
                    {sResultado += MontarTablaDocs("FA", aArgs[1], aArgs[2], aArgs[3]);}
                    sResultado += "@#@";
                    //Cursos recibidos o impartidos
                    if (aArgs[4] != "" || aArgs[5] != "")
                    {sResultado += MontarTablaDocs("CURSO", aArgs[1], aArgs[4], aArgs[5]);}
                    //CERTIFICADOS
                    sResultado += "@#@";
                    if (aArgs[6] != "" || aArgs[7] != "")
                    {sResultado += MontarTablaDocs("CERT", aArgs[1], aArgs[6], aArgs[7]);}
                    //TITULOS DE IDIOMAS
                    sResultado += "@#@";
                    //Titulos de los idiomas de los profesionales seleccionados
                    if (aArgs[8] != "" || aArgs[9] != "" )
                    {sResultado += MontarTablaDocs("IDIOMA", aArgs[1], aArgs[8], aArgs[9]);}
                    sResultado += "@#@";
                    break;
                case "getTablaDocsCadena":
                case "getTablaDocsQuery":
                    #region Query
                    sResultado += "OK@#@";
                    string sListaFicepis = aArgs[1];
                    string sListaDen = "";
                    if (aArgs[2] != "")//Titulos de la formación académica
                    {
                        sListaDen = generarCadenaDocExport(aArgs[2]);
                        //sResultado += FormacionAcademica.GetTablaTitulos(sListaFicepis, sListaDen);
                        sResultado += MontarTablaDocs("FA", sListaFicepis, "", sListaDen);
                    }
                    sResultado += "@#@";
                    if (aArgs[3] != "")//Cursos impartidos y recibidos
                    {
                        sListaDen = generarCadenaDocExport(aArgs[3]);
                        if (sListaDen=="")
                            sListaDen = generarCadenaDocExport(aArgs[4]);
                        else
                            sListaDen += "##" + generarCadenaDocExport(aArgs[4]);
                        //sResultado += Curso.GetTablaTitulos("RECIBIDO", sListaFicepis, sListaDen);
                        sResultado += MontarTablaDocs("CURSO", sListaFicepis, "", sListaDen);
                    }
                    sResultado += "@#@";
                    if (aArgs[5] != "")//Certificados
                    {
                        sListaDen = generarCadenaDocExport(aArgs[5]);
                        //sResultado += Certificado.GetTablaCriterios("", sListaDen);
                        sResultado += MontarTablaDocs("CERT", sListaFicepis, "", sListaDen);
                    }
                    sResultado += "@#@";
                    if (aArgs[6] != "")//Titulos de Idiomas
                    {
                        sListaDen = generarCadenaDocExport(aArgs[6]);
                        //obtiene la lista de los titulos de los idiomas de los profesionales seleccionados
                        //sResultado += Idioma.GetTablaTitulos(sListaFicepis, sListaDen);
                        sResultado += MontarTablaDocs("IDIOMA", sListaFicepis, "", sListaDen);
                    }
                    sResultado += "@#@";
                    break;
                    #endregion
            }
        }
        catch (Exception ex)
        {
            sResultado += "Error@#@" + Errores.mostrarError("Error al guardar", ex);
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    public string CargarCriterio(string sTipo)
    {
        StringBuilder sb = new StringBuilder();
        List<ElementoLista> oLista=null;
        switch (sTipo)
        {
            case "3"://perfiles de mercado
            case "16":
            case "161":
                oLista = Curriculum.obtenerPerfil(true, true);
                break;
            case "7"://Idiomas
            case "71":
                oLista = Curriculum.obtenerIdioma(true, true);
                break;
            case "15"://Entidades certificadoras
            case "151":
                oLista = Curriculum.obtenerEntidadesCertificadoras();
                break;
        }
        if (oLista.Count > Constantes.nNumElementosMaxCriterios)
            sb.Append("S@#@");
        else
            sb.Append("N@#@");
        foreach (ElementoLista oElem in oLista)
        {
            sb.Append(oElem.sValor);
            sb.Append("##");
            sb.Append(oElem.sDenominacion);
            sb.Append("///");
        }
        return sb.ToString();
    }
    public void CargarCombos()
    {
        #region Cargar combos
        #region Estructura
        if (Utilidades.EstructuraActiva("SN4"))
        {
            lblSN4.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4);
            lblSN4.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));
            lblAvanSN4.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4);
            lblAvanSN4.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));
            lblSN4C.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4);
            lblSN4C.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));
            lblSN4Q.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4);
            lblSN4Q.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));

        }
        else
        {
            lblSN4.Style.Add("display", "none");
            cboSN4.Style.Add("display", "none");
            lblAvanSN4.Style.Add("display", "none");
            cboAvanSN4.Style.Add("display", "none");
            lblSN4C.Style.Add("display", "none");
            cboSNC4.Style.Add("display", "none");
            lblSN4Q.Style.Add("display", "none");
            cboSNQ4.Style.Add("display", "none");
        }

        if (Utilidades.EstructuraActiva("SN3"))
        {
            lblSN3.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3);
            lblSN3.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
            lblAvanSN3.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3);
            lblAvanSN3.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
            lblSN3C.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3);
            lblSN3C.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
            lblSN3Q.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3);
            lblSN3Q.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
        }
        else
        {
            lblSN3.Style.Add("display", "none");
            cboSN3.Style.Add("display", "none");
            lblAvanSN3.Style.Add("display", "none");
            cboAvanSN3.Style.Add("display", "none");
            lblSN3C.Style.Add("display", "none");
            cboSNC3.Style.Add("display", "none");
            lblSN3Q.Style.Add("display", "none");
            cboSNQ3.Style.Add("display", "none");
        }

        if (Utilidades.EstructuraActiva("SN2"))
        {
            lblSN2.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2);
            lblSN2.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
            lblAvanSN2.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2);
            lblAvanSN2.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
            lblSN2C.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2);
            lblSN2C.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
            lblSN2Q.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2);
            lblSN2Q.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
        }
        else
        {
            lblSN2.Style.Add("display", "none");
            cboSN2.Style.Add("display", "none");
            lblAvanSN2.Style.Add("display", "none");
            cboAvanSN2.Style.Add("display", "none");
            lblSN2C.Style.Add("display", "none");
            cboSNC2.Style.Add("display", "none");
            lblSN2Q.Style.Add("display", "none");
            cboSNQ2.Style.Add("display", "none");
        }

        if (Utilidades.EstructuraActiva("SN1"))
        {
            lblSN1.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1);
            lblSN1.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
            lblAvanSN1.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1);
            lblAvanSN1.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
            lblSN1C.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1);
            lblSN1C.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
            lblSN1Q.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1);
            lblSN1Q.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
        }
        else
        {
            lblSN1.Style.Add("display", "none");
            cboSN1.Style.Add("display", "none");
            lblAvanSN1.Style.Add("display", "none");
            cboAvanSN1.Style.Add("display", "none");
            lblSN1C.Style.Add("display", "none");
            cboSNC1.Style.Add("display", "none");
            lblSN1Q.Style.Add("display", "none");
            cboSNQ1.Style.Add("display", "none");
        }

        lblCR.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
        lblCR.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
        lblAvanCR.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
        lblAvanCR.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
        lblCRC.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
        lblCRC.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
        lblCRQ.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
        lblCRQ.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
        #endregion
        //Primero obtenemos la lista de objetos (centros, perfiles,...) y luego la vinculamos al combo de la pestaña básica, Query y Cadena. 
        #region Centro
        List<ElementoLista> oListaCentros = Curriculum.obtenerCentro(cvConsultaExternos, cvConsultaBaja);
        cboCentro.DataSource = oListaCentros;
        cboCentro.DataValueField = "sValor";
        cboCentro.DataTextField = "sDenominacion";
        cboCentro.DataBind();

        cboCentroQ.DataSource = oListaCentros;
        cboCentroQ.DataValueField = "sValor";
        cboCentroQ.DataTextField = "sDenominacion";
        cboCentroQ.DataBind();

        cboCentroC.DataSource = oListaCentros;
        cboCentroC.DataValueField = "sValor";
        cboCentroC.DataTextField = "sDenominacion";
        cboCentroC.DataBind();

        cboAvanCentro.DataSource = Curriculum.obtenerCentro(cvConsultaExternos, cvConsultaBaja);
        cboAvanCentro.DataValueField = "sValor";
        cboAvanCentro.DataTextField = "sDenominacion";
        cboAvanCentro.DataBind();
        #endregion
        #region Perfil
        //PPOO nos indica que no hay que mostrar perfiles de mercado
        //List<ElementoLista> oListaPerfiles = Curriculum.obtenerPerfil(cvConsultaExternos, cvConsultaBaja);
        //cboPerfilPro.DataSource = oListaPerfiles;
        //cboPerfilPro.DataValueField = "sValor";
        //cboPerfilPro.DataTextField = "sDenominacion";
        //cboPerfilPro.DataBind();

        //cboPerfilProC.DataSource = oListaPerfiles;
        //cboPerfilProC.DataValueField = "sValor";
        //cboPerfilProC.DataTextField = "sDenominacion";
        //cboPerfilProC.DataBind();

        //cboPerfilProQ.DataSource = oListaPerfiles;
        //cboPerfilProQ.DataValueField = "sValor";
        //cboPerfilProQ.DataTextField = "sDenominacion";
        //cboPerfilProQ.DataBind();
        #endregion
        #region Movilidad
        List<ElementoLista> oListaMovilidad = Curriculum.obtenerCboMovilidad();
        cboMovilidad.DataSource = oListaMovilidad;
        cboMovilidad.DataValueField = "sValor";
        cboMovilidad.DataTextField = "sDenominacion";
        cboMovilidad.DataBind();

        cboMovilidadQ.DataSource = oListaMovilidad;
        cboMovilidadQ.DataValueField = "sValor";
        cboMovilidadQ.DataTextField = "sDenominacion";
        cboMovilidadQ.DataBind();

        cboMovilidadC.DataSource = oListaMovilidad;
        cboMovilidadC.DataValueField = "sValor";
        cboMovilidadC.DataTextField = "sDenominacion";
        cboMovilidadC.DataBind();
        #endregion

        //Combos pestaña básica
        cboPerfilExp.DataSource = PerfilExper.GetPerfilesConsultas();
        cboPerfilExp.DataValueField = "sValor";
        cboPerfilExp.DataTextField = "sDenominacion";
        cboPerfilExp.DataBind();


        cboAvanMovilidad.DataSource = Curriculum.obtenerCboMovilidad();
        cboAvanMovilidad.DataValueField = "sValor";
        cboAvanMovilidad.DataTextField = "sDenominacion";
        cboAvanMovilidad.DataBind();

        cboModalidad.DataSource = TituloFicepi.obtenerModalidades();
        cboModalidad.DataValueField = "sValor";
        cboModalidad.DataTextField = "sDenominacion";
        cboModalidad.DataBind();

        cboAvanModalidad.DataSource = TituloFicepi.obtenerModalidades();
        cboAvanModalidad.DataValueField = "sValor";
        cboAvanModalidad.DataTextField = "sDenominacion";
        cboAvanModalidad.DataBind();

        cboTipologia.DataSource = TituloFicepi.obtenerTipos();
        cboTipologia.DataValueField = "sValor";
        cboTipologia.DataTextField = "sDenominacion";
        cboTipologia.DataBind();

        cboAvanTipologia.DataSource = TituloFicepi.obtenerTipos();
        cboAvanTipologia.DataValueField = "sValor";
        cboAvanTipologia.DataTextField = "sDenominacion";
        cboAvanTipologia.DataBind();

        cboIdioma.DataSource = Curriculum.obtenerIdioma(cvConsultaExternos, cvConsultaBaja);
        cboIdioma.DataValueField = "sValor";
        cboIdioma.DataTextField = "sDenominacion";
        cboIdioma.DataBind();

        cboNivel.DataSource = Idioma.obtenerCboNivelIdioma();
        cboNivel.DataValueField = "sValor";
        cboNivel.DataTextField = "sDenominacion";
        cboNivel.DataBind();

        cboSector.DataSource = Curriculum.obtenerSector();
        cboSector.DataValueField = "sValor";
        cboSector.DataTextField = "sDenominacion";
        cboSector.DataBind();

        cboAvanSector.DataSource = Curriculum.obtenerSector();
        cboAvanSector.DataValueField = "sValor";
        cboAvanSector.DataTextField = "sDenominacion";
        cboAvanSector.DataBind();

        cboMedTiempo.DataSource = Curriculum.obtenerMedidaTiempo();
        cboMedTiempo.DataValueField = "sValor";
        cboMedTiempo.DataTextField = "sDenominacion";
        cboMedTiempo.DataBind();

        //cboAvanMedTiempo.DataSource = Curriculum.obtenerMedidaTiempo();
        //cboAvanMedTiempo.DataValueField = "sValor";
        //cboAvanMedTiempo.DataTextField = "sDenominacion";
        //cboAvanMedTiempo.DataBind();

        cboAvanExpMedTiempo.DataSource = Curriculum.obtenerMedidaTiempo();
        cboAvanExpMedTiempo.DataValueField = "sValor";
        cboAvanExpMedTiempo.DataTextField = "sDenominacion";
        cboAvanExpMedTiempo.DataBind();

        cboAnoInicio.DataSource = TituloFicepi.obtenerAños();
        cboAnoInicio.DataValueField = "sValor";
        cboAnoInicio.DataTextField = "sDenominacion";
        cboAnoInicio.DataBind();

        //cboAvanAnoInicio.DataSource = TituloFicepi.obtenerAños();
        //cboAvanAnoInicio.DataValueField = "sValor";
        //cboAvanAnoInicio.DataTextField = "sDenominacion";
        //cboAvanAnoInicio.DataBind();

        cboAvanExpAnoInicio.DataSource = TituloFicepi.obtenerAños();
        cboAvanExpAnoInicio.DataValueField = "sValor";
        cboAvanExpAnoInicio.DataTextField = "sDenominacion";
        cboAvanExpAnoInicio.DataBind();

        cboAvanAnoCurso.DataSource = TituloFicepi.obtenerAños();
        cboAvanAnoCurso.DataValueField = "sValor";
        cboAvanAnoCurso.DataTextField = "sDenominacion";
        cboAvanAnoCurso.DataBind();

        //Contenido de Experiencias / Funciones
        cboAvanExpFunAnoInicio.DataSource = TituloFicepi.obtenerAños();
        cboAvanExpFunAnoInicio.DataValueField = "sValor";
        cboAvanExpFunAnoInicio.DataTextField = "sDenominacion";
        cboAvanExpFunAnoInicio.DataBind();

        cboAvanExpFunMedTiempo.DataSource = Curriculum.obtenerMedidaTiempo();
        cboAvanExpFunMedTiempo.DataValueField = "sValor";
        cboAvanExpFunMedTiempo.DataTextField = "sDenominacion";
        cboAvanExpFunMedTiempo.DataBind();

        #endregion
    }

    #region Preferencias
    //private string setPreferenciaBasica(
    //                              string sIdProfesional, string sProfesional, string sPerfilPro, string sCentro, string sIntTrayInt, 
    //                              string sMovilidad, string sGradoDisp, string sTipo, string sSN4,
    //                              string sSN3, string sSN2, string sSN1, string sCR, string sEstado, string sLimCoste,
    //                              string sIdTituloAcad, string sTituloAcad, string sTipologia, string sModalidad, string sTics,
    //                              string sIdCertificacion, string sCertificacion, string sIdioma, string sNivel, 
    //                              string sIdCuenta, string sCuenta, string sSector, string sPerfilExp, string sCanTiempo,
    //                              string sMedTiempo, string sAnoInicio, string sFormato, string sRestringir, string sTituloAcre,
    //                              string sForEntAndOr, string sExpEntAndOr, string sValoresMultiples                                
    //                            )
    //{


    //    string sResul = "";

    //    #region abrir conexión y transacción
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccionSerializable(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        int nPref = PREFERENCIAUSUARIO.InsertarCVT(tr,
    //                                    (int)Session["IDFICEPI_CVT_ACTUAL"], 
    //                                    40,
    //                                    (sIdProfesional=="")? null: sIdProfesional,
    //                                    (sProfesional == "") ? null : sProfesional,
    //                                    (sPerfilPro == "") ? null : sPerfilPro,
    //                                    (sCentro == "") ? null : sCentro,
    //                                    (sIntTrayInt == "") ? null : sIntTrayInt,
    //                                    (sMovilidad == "") ? null : sMovilidad,
    //                                    (sGradoDisp == "") ? null : sGradoDisp,
    //                                    (sTipo == "") ? null : sTipo,
    //                                    (sSN4 == "") ? null : sSN4,
    //                                    (sSN3 == "") ? null : sSN3,
    //                                    (sSN2 == "") ? null : sSN2,
    //                                    (sSN1 == "") ? null : sSN1,
    //                                    (sCR == "") ? null : sCR,
    //                                    (sEstado == "") ? null : sEstado,
    //                                    (sLimCoste == "") ? null : sLimCoste,
    //                                    (sIdTituloAcad == "") ? null : sIdTituloAcad,
    //                                    (sTituloAcad == "") ? null : sTituloAcad,
    //                                    (sTipologia == "") ? null : sTipologia,
    //                                    (sModalidad == "") ? null : sModalidad,
    //                                    (sTics == "") ? null : sTics,
    //                                    (sIdCertificacion == "") ? null : sIdCertificacion,
    //                                    (sCertificacion == "") ? null : sCertificacion,
    //                                    (sIdioma == "") ? null : sIdioma,
    //                                    (sNivel == "") ? null : sNivel,
    //                                    (sIdCuenta == "") ? null : sIdCuenta,
    //                                    (sCuenta == "") ? null : sCuenta,
    //                                    (sSector == "") ? null : sSector,
    //                                    (sPerfilExp == "") ? null : sPerfilExp,
    //                                    (sCanTiempo == "") ? null : sCanTiempo,
    //                                    (sMedTiempo == "") ? null : sMedTiempo,                                        
    //                                    (sAnoInicio == "") ? null : sAnoInicio,
    //                                    (sFormato == "") ? null : sFormato,
    //                                    (sRestringir == "") ? null : sRestringir,
    //                                    (sTituloAcre == "") ? null : sTituloAcre,
    //                                    (sForEntAndOr == "") ? null : sForEntAndOr,
    //                                    (sExpEntAndOr == "") ? null : sExpEntAndOr,                                        
    //                                    null, null, null);

    //        #region Valores Múltiples
    //        if (sValoresMultiples != "")
    //        {
    //            string[] aValores = Regex.Split(sValoresMultiples, "///");
    //            foreach (string oValor in aValores)
    //            {
    //                if (oValor == "") continue;
    //                string[] aDatos = Regex.Split(oValor, "##");
    //                ///aDatos[0] = concepto
    //                ///aDatos[1] = idValor
    //                ///aDatos[2] = denominacion

    //                PREFUSUMULTIVALOR.InsertarCVT(tr, nPref, byte.Parse(aDatos[0]), aDatos[1], Utilidades.unescape(aDatos[2]));
    //            }
    //        }

    //        #endregion

    //        Conexion.CommitTransaccion(tr);

    //        sResul = "OK@#@" + nPref.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}

    //private string setPreferenciaAvanzada(string sDatosSimples, string sDatosCajas, string sDatosTablas)
    //{
    //    string sResul = "";
    //    string[] aArgs = Regex.Split(sDatosSimples, "##");
    //    string sCentro=aArgs[0], sIntTrayInt=aArgs[1], sMovilidad=aArgs[2], sGradoDisp=aArgs[3], sTipo=aArgs[4], 
    //           sSN4=aArgs[5], sSN3=aArgs[6], sSN2=aArgs[7], sSN1=aArgs[8], sCR=aArgs[9], sEstado=aArgs[10], sLimCoste=aArgs[11],
    //           sTipologia=aArgs[12], sModalidad=aArgs[13], sTics=aArgs[14],
    //           EntAnoInicio = aArgs[15], CursoCanTiempo = aArgs[16], CursoAnoInicio = aArgs[17],
    //           sExpCanTiempo = aArgs[18], sExpMedTiempo = aArgs[19], sExpAnoInicio = aArgs[20],
    //           sIdCuenta = aArgs[21], sCuenta = aArgs[22], sSector = aArgs[23];

    //    #region abrir conexión y transacción
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccionSerializable(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        int nPref = PREFERENCIAUSUARIO.InsertarCVT(tr,
    //                                (int)Session["IDFICEPI_CVT_ACTUAL"],
    //                                41,
    //                                (sCentro == "") ? null : sCentro,
    //                                (sIntTrayInt == "") ? null : sIntTrayInt,
    //                                (sMovilidad == "") ? null : sMovilidad,
    //                                (sGradoDisp == "") ? null : sGradoDisp,
    //                                (sTipo == "") ? null : sTipo,
    //                                (sSN4 == "") ? null : sSN4,
    //                                (sSN3 == "") ? null : sSN3,
    //                                (sSN2 == "") ? null : sSN2,
    //                                (sSN1 == "") ? null : sSN1,
    //                                (sCR == "") ? null : sCR,
    //                                (sEstado == "") ? null : sEstado,
    //                                (sLimCoste == "") ? null : sLimCoste,
    //                                (sTipologia == "") ? null : sTipologia,
    //                                (sModalidad == "") ? null : sModalidad,
    //                                (sTics == "") ? null : sTics,
    //                                (EntAnoInicio == "") ? null : EntAnoInicio,
    //                                (CursoCanTiempo == "") ? null : CursoCanTiempo,
    //                                (CursoAnoInicio == "") ? null : CursoAnoInicio,
    //                                (sExpCanTiempo == "") ? null : sExpCanTiempo,
    //                                (sExpMedTiempo == "") ? null : sExpMedTiempo,
    //                                (sExpAnoInicio == "") ? null : sExpAnoInicio,
    //                                (sIdCuenta == "") ? null : sIdCuenta,
    //                                (sCuenta == "") ? null : sCuenta,
    //                                (sSector == "") ? null : sSector,
    //                                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

    //        #region Valores Múltiples en caja
    //        //Aunque estos valores son una única cadena, los guardo como multivalor porque no hay campos suficientes para guardarlos como 
    //        //criterios en la T462
    //        //Actualmente el mayor nº de concepto para guardar los datos de criterios en tablas es 18 así que dejo un hueco por si acaso
    //        //y empiezo a grabar este tipo de valores a partir del código 30
    //        if (sDatosCajas != "")
    //        {
    //            byte nConcepto = 29;
    //            string[] aValores = Regex.Split(sDatosCajas, "##");
    //            foreach (string oValor in aValores)
    //            {
    //                nConcepto++;
    //                if (oValor == "") continue;
    //                ///aDatos[0] = concepto
    //                ///aDatos[1] = idValor
    //                ///aDatos[2] = denominacion

    //                PREFUSUMULTIVALOR.InsertarCVT(tr, nPref, nConcepto, "", Utilidades.unescape(oValor));
    //            }
    //        }
    //        #endregion
    //        #region Valores Múltiples en tabla
    //        if (sDatosTablas != "")
    //        {
    //            string[] aValores = Regex.Split(sDatosTablas, "///");
    //            foreach (string oValor in aValores)
    //            {
    //                if (oValor == "") continue;
    //                string[] aDatos = Regex.Split(oValor, "##");
    //                ///aDatos[0] = concepto
    //                ///aDatos[1] = idValor
    //                ///aDatos[2] = denominacion

    //                PREFUSUMULTIVALOR.InsertarCVT(tr, nPref, byte.Parse(aDatos[0]), aDatos[1], Utilidades.unescape(aDatos[2]));
    //            }
    //        }
    //        #endregion

    //        Conexion.CommitTransaccion(tr);

    //        sResul = "OK@#@" + nPref.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia avanzada.", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}
    //private string setPreferenciaCadena(string sDatosSimples, string sDatosCajas)
    //{
    //    string sResul = "";
    //    string[] aArgs = Regex.Split(sDatosSimples, "##");
    //    string sPerfil = aArgs[0], sCentro = aArgs[1], sIntTrayInt = aArgs[2], sMovilidad = aArgs[3], sGradoDisp = aArgs[4], 
    //            sTipo = aArgs[5], sSN4 = aArgs[6], sSN3 = aArgs[7], sSN2 = aArgs[8], sSN1 = aArgs[9], sCR = aArgs[10], 
    //           sEstado = aArgs[11], sLimCoste = aArgs[12], sOperador = aArgs[13];
    //    string[] aBox = Regex.Split(sDatosCajas, "##");
    //    string sFA = aBox[0], sExp = aBox[1], sCursoR = aBox[2], sCursoI = aBox[3], sCert = aBox[4], sExamen = aBox[5], sIdioma = aBox[6];
    //    #region abrir conexión y transacción
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccionSerializable(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        int nPref = PREFERENCIAUSUARIO.InsertarCVT(tr,
    //                                (int)Session["IDFICEPI_CVT_ACTUAL"],
    //                                42,
    //                                (sPerfil == "") ? null : sPerfil,
    //                                (sCentro == "") ? null : sCentro,
    //                                (sIntTrayInt == "") ? null : sIntTrayInt,
    //                                (sMovilidad == "") ? null : sMovilidad,
    //                                (sGradoDisp == "") ? null : sGradoDisp,
    //                                (sTipo == "") ? null : sTipo,
    //                                (sSN4 == "") ? null : sSN4,
    //                                (sSN3 == "") ? null : sSN3,
    //                                (sSN2 == "") ? null : sSN2,
    //                                (sSN1 == "") ? null : sSN1,
    //                                (sCR == "") ? null : sCR,
    //                                (sEstado == "") ? null : sEstado,
    //                                (sLimCoste == "") ? null : sLimCoste,
    //                                (sOperador == "") ? null : sOperador,
    //                                (sFA == "") ? null : sFA,
    //                                (sExp == "") ? null : sExp,
    //                                (sCursoR == "") ? null : sCursoR,
    //                                (sCursoI == "") ? null : sCursoI,
    //                                (sCert == "") ? null : sCert,
    //                                (sExamen == "") ? null : sExamen,
    //                                (sIdioma == "") ? null : sIdioma,
    //                                null, null, null, null, null, null, null, null, null, null, null, null, null, null,   
    //                                null, null, null, null);


    //        Conexion.CommitTransaccion(tr);

    //        sResul = "OK@#@" + nPref.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia de cadena.", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}

    //private string delPreferencia(string sTipoPant)
    //{
    //    short nPantalla = -1;
    //    try
    //    {
    //        switch (sTipoPant)
    //        {
    //            case "B"://Borra las preferencias de la pestaña Basica
    //                nPantalla = 40;
    //                break;
    //            case "A"://Borra las preferencias de la pestaña Avanzada
    //                nPantalla = 41;
    //                break;
    //            case "C"://Borra las preferencias de la pestaña Cadena
    //                nPantalla = 42;
    //                break;
    //            case "T"://Borra las preferencias de todas las pestañas
    //                PREFERENCIAUSUARIO.DeleteAllCVT(tr, (int)Session["IDFICEPI_CVT_ACTUAL"], 40);
    //                PREFERENCIAUSUARIO.DeleteAllCVT(tr, (int)Session["IDFICEPI_CVT_ACTUAL"], 41);
    //                nPantalla = 42;
    //                break;
    //        }
    //        PREFERENCIAUSUARIO.DeleteAllCVT(tr, (int)Session["IDFICEPI_CVT_ACTUAL"], nPantalla);
    //        return "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al eliminar la preferencia", ex);
    //    }
    //}
    //private string getPreferencia(string sIdPantalla, string sIdPrefUsuario)
    //{
    //    string sRes = "";
    //    if (sIdPantalla == "-2")
    //    {//hemos ido a coger preferencia desde la consulta global. Necesito obtener el tipo de pantalla
    //        sIdPantalla = SUPER.Capa_Negocio.PREFERENCIAUSUARIO.GetTipoPantallaPreferencia(null, int.Parse(sIdPrefUsuario)).ToString();
    //    }
    //    switch (sIdPantalla)
    //    {
    //        case "40":
    //            sRes = getPreferenciaBasica(sIdPantalla, sIdPrefUsuario);
    //            break;
    //        case "41":
    //            sRes = getPreferenciaAvanzada(sIdPantalla, sIdPrefUsuario);
    //            break;
    //        case "42":
    //            sRes = getPreferenciaCadena(sIdPantalla, sIdPrefUsuario);
    //            break;
    //    }
    //    return sRes;
    //}
    //private string getPreferenciaBasica(string sIdPantalla, string sIdPrefUsuario)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    int idPrefUsuario = 0;
    //    try
    //    {
    //        SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
    //                                                        (int)Session["IDFICEPI_CVT_ACTUAL"], short.Parse(sIdPantalla));
    //        if (dr.Read())
    //        {
    //            #region Valores Simples

    //            sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
    //            sb.Append(dr["IdProfesional"].ToString() + "@#@"); //2 
    //            sb.Append(dr["Profesional"].ToString() + "@#@"); //3 
    //            sb.Append(dr["PerfilPro"].ToString() + "@#@"); //4
    //            sb.Append(dr["Centro"].ToString() + "@#@"); //5
    //            sb.Append(dr["IntTrayInt"].ToString() + "@#@"); //6
    //            sb.Append(dr["Movilidad"].ToString() + "@#@"); //7
    //            sb.Append(dr["GradoDisp"].ToString() + "@#@"); //8
    //            sb.Append(dr["Tipo"].ToString() + "@#@"); //9
    //            sb.Append(dr["SN4"].ToString() + "@#@"); //10
    //            sb.Append(dr["SN3"].ToString() + "@#@"); //11
    //            sb.Append(dr["SN2"].ToString() + "@#@"); //12
    //            sb.Append(dr["SN1"].ToString() + "@#@"); //13
    //            sb.Append(dr["CR"].ToString() + "@#@");  //14
    //            sb.Append(dr["Estado"].ToString() + "@#@");  //15
    //            sb.Append(dr["LimCoste"].ToString() + "@#@");  //16
    //            sb.Append(dr["IdTituloAcad"].ToString() + "@#@");  //17
    //            sb.Append(dr["TituloAcad"].ToString() + "@#@");  //18
    //            sb.Append(dr["Tipologia"].ToString() + "@#@");  //19
    //            sb.Append(dr["Modalidad"].ToString() + "@#@");  //20
    //            sb.Append(dr["Tics"].ToString() + "@#@");  //21
    //            sb.Append(dr["IdCertificacion"].ToString() + "@#@");  //22
    //            sb.Append(dr["Certificacion"].ToString() + "@#@");  //23
    //            sb.Append(dr["Idioma"].ToString() + "@#@");  //24
    //            sb.Append(dr["Nivel"].ToString() + "@#@");  //25
    //            sb.Append(dr["IdCuenta"].ToString() + "@#@");  //26
    //            sb.Append(dr["Cuenta"].ToString() + "@#@");  //27
    //            sb.Append(dr["Sector"].ToString() + "@#@");  //28
    //            sb.Append(dr["PerfilExp"].ToString() + "@#@");  //29
    //            sb.Append(dr["CanTiempo"].ToString() + "@#@");  //30
    //            sb.Append(dr["MedTiempo"].ToString() + "@#@");  //31
    //            sb.Append(dr["AnoInicio"].ToString() + "@#@");  //32
    //            sb.Append(dr["Formato"].ToString() + "@#@");  //33
    //            sb.Append(dr["Restringir"].ToString() + "@#@");  //34
    //            sb.Append(dr["IdiomaTituloAcre"].ToString() + "@#@");  //35

    //            sb.Append(dr["FormacionEntorno"].ToString() + "@#@");  //36
    //            sb.Append(dr["ExperienciaEntorno"].ToString() + "@#@");  //37
                
    //            idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
    //            #endregion
    //        }
    //        dr.Close();
    //        //dr.Dispose();

    //        #region HTML, IDs

    //        //string strDatosPersonales = "", strDatosOrganiza = "", strFormaAcade = "", 
    //        //    strAccionesFormaRecib = "", strAccionesFormaImp = "", strCertifiExame = "",
    //        //    strIdiomas = "", strExperienciaFuera = "", strExperienciaIbermatica = "", strSinopsis= "";

    //        dr = PREFUSUMULTIVALOR.ObtenerCVT(null, idPrefUsuario);

    //        while (dr.Read())
    //        {
    //            switch (int.Parse(dr["t441_concepto"].ToString()))
    //            {
    //                case 1:
    //                    //if (strIDsEntTecFor != "") strIDsEntTecFor += ",";
    //                    //strIDsEntTecFor += dr["t441_valor"].ToString();
    //                    strHTMLEntTecFor += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W290'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 2:
    //                    //if (strIDsEntTecExp != "") strIDsEntTecExp += ",";
    //                    //strIDsEntTecExp += dr["t441_valor"].ToString();
    //                    strHTMLEntTecExp += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W290'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                /*
    //                case 3:
    //                    if (strDatosPersonales != "") strDatosPersonales += "///";
    //                    strDatosPersonales += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 4:
    //                    if (strDatosOrganiza != "") strDatosOrganiza += "///";
    //                    strDatosOrganiza += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 5: 
    //                    if (strFormaAcade != "") strFormaAcade += "///";
    //                    strFormaAcade += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 6: 
    //                    if (strAccionesFormaRecib != "") strAccionesFormaRecib += "///";
    //                    strAccionesFormaRecib += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 7:
    //                    if (strAccionesFormaImp != "") strAccionesFormaImp += "///";
    //                    strAccionesFormaImp += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 8:
    //                    if (strCertifiExame != "") strCertifiExame += "///";
    //                    strCertifiExame += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 9:
    //                    if (strIdiomas != "") strIdiomas += "///";
    //                    strIdiomas += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 10:
    //                    if (strExperienciaFuera != "") strExperienciaFuera += "///";
    //                    strExperienciaFuera += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 11:
    //                    if (strExperienciaIbermatica != "") strExperienciaIbermatica += "///";
    //                    strExperienciaIbermatica += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
    //                    break;
    //                case 12:
    //                    if (strSinopsis != "") strSinopsis += "///";
    //                    strSinopsis += dr["t441_valor"].ToString() + "##" + dr["t441_denominacion"].ToString();
    //                    break;
    //                */
    //            }
    //        }
    //        dr.Close();
    //        dr.Dispose();
    //        #endregion

    //        sb.Append(strHTMLEntTecFor + "@#@"); //38
    //        sb.Append(strHTMLEntTecExp + "@#@"); //39
    //        //sb.Append(strDatosPersonales + "@#@"); //40
    //        //sb.Append(strDatosOrganiza + "@#@"); //41
    //        //sb.Append(strFormaAcade + "@#@"); //42
    //        //sb.Append(strAccionesFormaRecib + "@#@"); //43
    //        //sb.Append(strAccionesFormaImp + "@#@"); //44
    //        //sb.Append(strCertifiExame + "@#@"); //45
    //        //sb.Append(strIdiomas + "@#@"); //46
    //        //sb.Append(strExperienciaFuera + "@#@"); //47
    //        //sb.Append(strExperienciaIbermatica + "@#@"); //48
    //        //sb.Append(strSinopsis + "@#@"); //49

    //        return "OK@#@B@#@" + sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia de la pestaña básica.", ex, false);
    //    }
    //}
    //private string getPreferenciaAvanzada(string sIdPantalla, string sIdPrefUsuario)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    StringBuilder sbCaja = new StringBuilder();
    //    int idPrefUsuario = 0;
    //    try
    //    {
    //        SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
    //                                                        (int)Session["IDFICEPI_CVT_ACTUAL"], short.Parse(sIdPantalla));
    //        if (dr.Read())
    //        {
    //            #region Valores Simples

    //            sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
    //            sb.Append(dr["Centro"].ToString() + "@#@"); //2
    //            sb.Append(dr["IntTrayInt"].ToString() + "@#@"); //3
    //            sb.Append(dr["Movilidad"].ToString() + "@#@"); //4
    //            sb.Append(dr["GradoDisp"].ToString() + "@#@"); //5
    //            sb.Append(dr["Tipo"].ToString() + "@#@"); //6
    //            sb.Append(dr["SN4"].ToString() + "@#@"); //7
    //            sb.Append(dr["SN3"].ToString() + "@#@"); //8
    //            sb.Append(dr["SN2"].ToString() + "@#@"); //9
    //            sb.Append(dr["SN1"].ToString() + "@#@"); //10
    //            sb.Append(dr["CR"].ToString() + "@#@");  //11
    //            sb.Append(dr["Estado"].ToString() + "@#@");  //12
    //            sb.Append(dr["LimCoste"].ToString() + "@#@");  //13
    //            sb.Append(dr["Tipologia"].ToString() + "@#@");  //14
    //            sb.Append(dr["Modalidad"].ToString() + "@#@");  //15 
    //            sb.Append(dr["Tics"].ToString() + "@#@");  //16
    //            sb.Append(dr["EntAnoInicio"].ToString() + "@#@");  //
    //            sb.Append(dr["CursoCanTiempo"].ToString() + "@#@");  //
    //            sb.Append(dr["CursoAnoInicio"].ToString() + "@#@");  //
    //            sb.Append(dr["ExpCanTiempo"].ToString() + "@#@");  //
    //            sb.Append(dr["ExpMedTiempo"].ToString() + "@#@");  //
    //            sb.Append(dr["ExpAnoInicio"].ToString() + "@#@");  //
    //            sb.Append(dr["IdCuenta"].ToString() + "@#@");  //
    //            sb.Append(dr["Cuenta"].ToString() + "@#@");  //
    //            sb.Append(dr["Sector"].ToString() + "@#@");  //

    //            idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
    //            #endregion
    //        }
    //        dr.Close();
    //        //dr.Dispose();

    //        #region HTML, IDs
    //        dr = PREFUSUMULTIVALOR.ObtenerCVT(null, idPrefUsuario);
    //        while (dr.Read())
    //        {
    //            switch (int.Parse(dr["t441_concepto"].ToString()))
    //            {
    //                case 1:
    //                    strHTMLAvanProf += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 2:
    //                    strHTMLAvanPerf += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 3:
    //                    strHTMLAvanTitObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 4:
    //                    strHTMLAvanTitOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 5:
    //                    strHTMLAvanEntTecForObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 6:
    //                    strHTMLAvanEntTecForOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 7:
    //                    //En el campo denominación tenemos denominacion, nivel lectura, nivel escritura,nivel oral y titulo
    //                    string[] aIdioma = Regex.Split(dr["t441_denominacion"].ToString(), ",");
    //                    strHTMLAvanIdioObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'";
    //                    strHTMLAvanIdioObl += " nl=" + aIdioma[1] + " ne=" + aIdioma[2] + " no=" + aIdioma[3] + ">";
    //                    strHTMLAvanIdioObl += "<td><nobr class='NBR W140'>" + aIdioma[0] + "</nobr></td>";
    //                    strHTMLAvanIdioObl += "<td>" + HtmlComboNivelIdioma() + "</td>";
    //                    strHTMLAvanIdioObl += "<td>" + HtmlComboNivelIdioma() + "</td>";
    //                    strHTMLAvanIdioObl += "<td>" + HtmlComboNivelIdioma() + "</td>";
    //                    strHTMLAvanIdioObl += "<td>" + HtmlCheckTitulo(aIdioma[4]) + "</td>";
    //                    strHTMLAvanIdioObl += "</tr>";
    //                    break;
    //                case 8:
    //                    //En el campo denominación tenemos denominacion, nivel lectura, nivel escritura,nivel oral
    //                    aIdioma = Regex.Split(dr["t441_denominacion"].ToString(), ",");
    //                    strHTMLAvanIdioOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'";
    //                    strHTMLAvanIdioOpc += " nl=" + aIdioma[1] + " ne=" + aIdioma[2] + " no=" + aIdioma[3] + ">";
    //                    strHTMLAvanIdioOpc += "<td><nobr class='NBR W140'>" + aIdioma[0] + "</nobr></td>";
    //                    strHTMLAvanIdioOpc += "<td>" + HtmlComboNivelIdioma() + "</td>";
    //                    strHTMLAvanIdioOpc += "<td>" + HtmlComboNivelIdioma() + "</td>";
    //                    strHTMLAvanIdioOpc += "<td>" + HtmlComboNivelIdioma() + "</td>";
    //                    strHTMLAvanIdioOpc += "<td>" + HtmlCheckTitulo(aIdioma[4]) + "</td>";
    //                    strHTMLAvanIdioOpc += "</tr>";
    //                    break;

    //                case 9:
    //                    strHTMLAvanCertObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 10:
    //                    strHTMLAvanCertOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 11:
    //                    strHTMLAvanCertEntObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 12:
    //                    strHTMLAvanCertEntOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 13:
    //                    strHTMLAvanCursoObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 14:
    //                    strHTMLAvanCursoOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 15:
    //                    strHTMLAvanExpPerfObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 16:
    //                    strHTMLAvanExpPerfOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 17:
    //                    strHTMLAvanEntTecExpObl += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                case 18:
    //                    strHTMLAvanEntTecExpOpc += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W350'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
    //                    break;
    //                //A partir del código 30 son las cajas de texto
    //                default:
    //                    sbCaja.Append(dr["t441_concepto"].ToString() + "##" + dr["t441_denominacion"].ToString() + "///");
    //                    break;
    //            }
    //        }
    //        dr.Close();
    //        dr.Dispose();
    //        #endregion

    //        sb.Append(strHTMLAvanProf + "@#@");
    //        sb.Append(strHTMLAvanPerf + "@#@");
    //        sb.Append(strHTMLAvanTitObl + "@#@");
    //        sb.Append(strHTMLAvanTitOpc + "@#@");
    //        sb.Append(strHTMLAvanEntTecForObl + "@#@");
    //        sb.Append(strHTMLAvanEntTecForOpc + "@#@");
    //        sb.Append(strHTMLAvanIdioObl + "@#@");
    //        sb.Append(strHTMLAvanIdioOpc + "@#@");
    //        sb.Append(strHTMLAvanCertObl + "@#@");
    //        sb.Append(strHTMLAvanCertOpc + "@#@");
    //        sb.Append(strHTMLAvanCertEntObl + "@#@");
    //        sb.Append(strHTMLAvanCertEntOpc + "@#@");
    //        sb.Append(strHTMLAvanCursoObl + "@#@");
    //        sb.Append(strHTMLAvanCursoOpc + "@#@");
    //        sb.Append(strHTMLAvanExpPerfObl + "@#@");
    //        sb.Append(strHTMLAvanExpPerfOpc + "@#@");
    //        sb.Append(strHTMLAvanEntTecExpObl + "@#@");
    //        sb.Append(strHTMLAvanEntTecExpOpc + "@#@");

    //        return "OK@#@A@#@" + sb.ToString() + sbCaja.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia de la pestaña avanzada.", ex, false);
    //    }
    //}
    //private string getPreferenciaCadena(string sIdPantalla, string sIdPrefUsuario)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    int idPrefUsuario = 0;
    //    try
    //    {
    //        SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
    //                                                        (int)Session["IDFICEPI_CVT_ACTUAL"], short.Parse(sIdPantalla));
    //        if (dr.Read())
    //        {
    //            idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
    //            #region Valores Simples
    //            sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
    //            sb.Append(dr["PerfilPro"].ToString() + "@#@"); //
    //            sb.Append(dr["Centro"].ToString() + "@#@"); //
    //            sb.Append(dr["IntTrayInt"].ToString() + "@#@"); //
    //            sb.Append(dr["Movilidad"].ToString() + "@#@"); //
    //            sb.Append(dr["GradoDisp"].ToString() + "@#@"); //
    //            sb.Append(dr["Tipo"].ToString() + "@#@"); //
    //            sb.Append(dr["SN4"].ToString() + "@#@"); //
    //            sb.Append(dr["SN3"].ToString() + "@#@"); //
    //            sb.Append(dr["SN2"].ToString() + "@#@"); //
    //            sb.Append(dr["SN1"].ToString() + "@#@"); //
    //            sb.Append(dr["CR"].ToString() + "@#@");  //
    //            sb.Append(dr["Estado"].ToString() + "@#@");  //
    //            sb.Append(dr["LimCoste"].ToString() + "@#@");  //
    //            sb.Append(dr["Operador"].ToString() + "@#@");  //
    //            #endregion
    //            #region cadenas
    //            sb.Append(dr["FormAcad"].ToString() + "@#@");
    //            sb.Append(dr["ExpProf"].ToString() + "@#@");
    //            sb.Append(dr["CursoRec"].ToString() + "@#@");
    //            sb.Append(dr["CursoImp"].ToString() + "@#@");
    //            sb.Append(dr["Certificado"].ToString() + "@#@");
    //            sb.Append(dr["Examen"].ToString() + "@#@");
    //            sb.Append(dr["Idioma"].ToString());
    //            #endregion
    //        }
    //        dr.Close();
    //        dr.Dispose();

    //        return "OK@#@C@#@" + sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia de la pestaña cadena.", ex, false);
    //    }
    //}
    //private void ComprobarExistenPreferencias(string sIdFicepi)
    //{
    //    string sPant = "", sIdPreferencia="";
    //    ArrayList lstPant = SUPER.BLL.Curriculum.ListaPreferenciasDefecto(sIdFicepi);
    //    foreach (string sElem in lstPant)
    //    {
    //        if (sElem != "")
    //        {
    //            string[] aDat = Regex.Split(sElem, "#");
    //            sPant = aDat[0];
    //            sIdPreferencia = aDat[1];
    //            switch (sPant)
    //            {
    //                case "40":
    //                    sIdPreferenciaBasica = sIdPreferencia;
    //                    break;
    //                case "41":
    //                    sIdPreferenciaAvanzada = sIdPreferencia;
    //                    break;
    //                case "42":
    //                    sIdPreferenciaCadena = sIdPreferencia;
    //                    break;
    //            }
    //        }
    //    }
    //}
    #endregion

    private string HtmlComboNivelIdioma()
    {
        string sHtml =  @"<select class='combo'>
                            <option value='4'></option>
                            <option value='1'>Alto</option>
                            <option value='2'>Medio</option>
                            <option value='3'>Bajo</option>
                        </select>";       

        return sHtml;
    }
    private string HtmlCheckTitulo(string sMarcado)
    {
        string sHtml = "";
        if (sMarcado=="1")
            sHtml = @"<input type='checkbox' class'check' checked style='margin-left:10px' />";
        else
            sHtml = @"<input type='checkbox' class'check' style='margin-left:10px' />";

        return sHtml;
    }

    //private string Correo(string strFiltros, string sNombreProfesionales, string sDestinatarios)
    //{
    //    try
    //    {
    //        int int_pinicio;
    //        int int_pfin;
    //        string strServer;
    //        string strDataBase;
    //        string strUid;
    //        string strPwd;
    //        //				obtengo de la cadena de conexión los parámetros para luego
    //        //				modificar localizaciones 

    //        #region Conexion BBDD

    //        string strconexion = Utilidades.CadenaConexion;
    //        int_pfin = strconexion.IndexOf(";database=", 0);

    //        strServer = strconexion.Substring(7, int_pfin - 7);

    //        int_pinicio = int_pfin + 10;
    //        int_pfin = strconexion.IndexOf(";uid=", int_pinicio);
    //        strDataBase = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

    //        int_pinicio = int_pfin + 5;
    //        int_pfin = strconexion.IndexOf(";pwd=", int_pinicio);
    //        strUid = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

    //        int_pinicio = int_pfin + 5;
    //        int_pfin = strconexion.IndexOf(";Trusted_Connection=", int_pinicio);
    //        strPwd = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

    //        #endregion

    //        #region Objeto ReportDocument

    //        //creo un objeto ReportDocument
    //        ReportDocument rdCurriculum = new ReportDocument();

    //        try
    //        {
    //            rdCurriculum.Load(Server.MapPath(".") + @"\..\MiCV\Reports\cvt_curriculum.rpt");
    //        }
    //        catch (Exception ex)
    //        {
    //            rdCurriculum.Close();
    //            rdCurriculum.Dispose();
    //            Response.Write("Error al abrir el report: " + ex.Message);
    //        }

    //        try
    //        {
    //            rdCurriculum.SetDatabaseLogon(strUid, strPwd);
    //        }
    //        catch (Exception ex)
    //        {
    //            rdCurriculum.Close();
    //            rdCurriculum.Dispose();
    //            Response.Write("Error al logarse al report: " + ex.Message);
    //        }
    //        #endregion
    //        #region Objeto Logon
    //        //creo un objeto logon .

    //        CrystalDecisions.Shared.TableLogOnInfo tliCurrent;

    //        try
    //        {
    //            foreach (CrystalDecisions.CrystalReports.Engine.Table tbCurrent in rdCurriculum.Database.Tables)
    //            {

    //                //obtengo el logon por tabla
    //                tliCurrent = tbCurrent.LogOnInfo;

    //                tliCurrent.ConnectionInfo.DatabaseName = strDataBase;
    //                tliCurrent.ConnectionInfo.UserID = strUid;
    //                tliCurrent.ConnectionInfo.Password = strPwd;
    //                tliCurrent.ConnectionInfo.ServerName = strServer;

    //                //aplico los cambios hechos al objeto TableLogonInfo
    //                tbCurrent.ApplyLogOnInfo(tliCurrent);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            rdCurriculum.Close();
    //            rdCurriculum.Dispose();

    //            Response.Write("Error al actualizar la localización: " + ex.Message);
    //        }

    //        #endregion

    //        DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
    //        ExportOptions exportOpts = new ExportOptions();

    //        #region Parametros Report

    //        //string strFiltros = (Request.Form["hdnFiltros"] == null) ? Request.Form["ctl00$CPHC$hdnFiltros"] : Request.Form["hdnFiltros"];            
    //        //string[] sFiltros = Regex.Split(strFiltros,"{filtro}");

    //        string[] sFiltros = Regex.Split(strFiltros, "{filtro}");

    //        string[] aDesglose = Regex.Split(sFiltros[1], "{valor}");//Desglose (DO,FOR,FACAD,CUR,CURIMP,CERT,IDI,EXP,ENIBER,FUERA,SINOPSIS)

    //        string[] aDesgloseFACAD = Regex.Split(sFiltros[20], "{valor}");//Desglose FORMACION ACADEMICA (Tipo, Modalidad, Tic, FInicio, FFIn)

    //        string[] aDesgloseIdioma = Regex.Split(sFiltros[21], "{valor}");//Desglose IDIOMAS (Lectura, Escritura, Oral, Titulo, F.Obtencion)

    //        string[] aDesgloseDO = Regex.Split(sFiltros[22], "{valor}"); //Desglose Datos Organizativos (Empresa, Unidad de negocio, CR, Antifuedad, Rol, Perfil, Oficina, Provincia, Pais, Trayectoria, Movilidad, Observaciones)

    //        string[] aDesgloseCURREC = Regex.Split(sFiltros[23], "{valor}"); //Desglose CURSOS RECIBIDOS (Proveedor, Entorno tecnologico, Provincia, Horas, F.Inicio, F. Fin)

    //        string[] aDesgloseCERTEXAM = Regex.Split(sFiltros[24], "{valor}"); //Desglose Certificados (Proveedor, Entorno tecnologico, F.Obtencion, Tipo)

    //        string[] aDesgloseEXPFUERA = Regex.Split(sFiltros[25], "{valor}"); //Desglose EXPeriencia FUERA de Ibermatica (Empresa Origen, Cliente, Funcion, F Inicio, F Fin, Descripcion, ACS-ACT, Sector, Segmento, Perfil, Entorno, Nº mes) 

    //        string[] aDesgloseEXPIBER = Regex.Split(sFiltros[26], "{valor}"); //Desglose EXPeriencia en IBERmatica(Cliente, Funcion, F Inicio, F Fin, Descripcion, ACS-ACT, Sector, Segmento, Perfil, Entorno, Nº mes) 

    //        string[] aDesgloseDP = Regex.Split(sFiltros[27], "{valor}"); //Desglose Datos Personales(Foto, NIF, F Nacimiento, Nacionalidad, Sexo)

    //        string[] aDesgloseCURIMP = Regex.Split(sFiltros[28], "{valor}"); //Desglose CURSOS IMPARTIDOS (Proveedor, Entorno tecnologico, Provincia, Horas, F.Inicio, F. Fin)


    //        string strTipoFormato = sFiltros[18];//Tipo formato
    //        rdCurriculum.SetParameterValue("formato_expiber", strTipoFormato);//Tipo de formato
    //        rdCurriculum.SetParameterValue("formato_expfuera", strTipoFormato);//Tipo de formato


    //        try
    //        {
    //            rdCurriculum.SetParameterValue("@t001_idficepi", sFiltros[0]);
    //            //Si es un Encargado de CV o un Consultor de CV en el impreso figurará el Perfil de Mercado
    //            if (User.IsInRole("ECV") || User.IsInRole("EVAL") || User.IsInRole("VSN4") || User.IsInRole("VSN3") || User.IsInRole("VSN2") || User.IsInRole("VSN1") || User.IsInRole("VN"))
    //                rdCurriculum.SetParameterValue("EsSoloUsuario", "N");
    //            else
    //                rdCurriculum.SetParameterValue("EsSoloUsuario", "S");

    //            //FORMACION
    //            //FormacionAcad
    //            rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "FormacionAcad");//Indica si hay que filtrar o no
    //            rdCurriculum.SetParameterValue("@t019_idcodtitulo", (sFiltros[4] == "") ? 0 : int.Parse(sFiltros[4]), "FormacionAcad");//IdcodTitulo 
    //            rdCurriculum.SetParameterValue("@t019_descripcion", sFiltros[3], "FormacionAcad");//Titulacion
    //            rdCurriculum.SetParameterValue("@t019_tipo", (sFiltros[5] == "") ? 0 : int.Parse(sFiltros[5]), "FormacionAcad");//Tipologia
    //            rdCurriculum.SetParameterValue("@t019_modalidad", (sFiltros[6] == "") ? 0 : int.Parse(sFiltros[6]), "FormacionAcad");//Modalidad
    //            rdCurriculum.SetParameterValue("@t019_tic", (sFiltros[7] == "") ? 3 : int.Parse(sFiltros[7]), "FormacionAcad");//Tic

    //            //CurRecibidos
    //            rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "CurRecibidos");//Indica si hay que filtrar o no
    //            rdCurriculum.SetParameterValue("@lft036_idcodentorno", sFiltros[10], "CurRecibidos");//Lista EntornoTecnologicos Formacion

    //            //CurImpartidos
    //            rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "CurImpartidos");//Indica si hay que filtrar o no
    //            rdCurriculum.SetParameterValue("@lft036_idcodentorno", sFiltros[10], "CurImpartidos");//Lista EntornoTecnologicos Formacion

    //            //CertExam
    //            rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "CertExam");//Indica si hay que filtrar o no
    //            rdCurriculum.SetParameterValue("@t582_nombre", sFiltros[8], "CertExam");//certificado
    //            rdCurriculum.SetParameterValue("@t582_idcertificado", (sFiltros[9] == "") ? 0 : int.Parse(sFiltros[9]), "CertExam");//IdCertificado
    //            rdCurriculum.SetParameterValue("@lft036_idcodentorno", sFiltros[10], "CertExam");//Lista EntornoTecnologicos Formacion
    //            rdCurriculum.SetParameterValue("@origenConsulta", (sFiltros[19] == "") ? 1 : int.Parse(sFiltros[19]), "CertExam");//origenConsulta 0 MiCV 1 Consultas

    //            //Idiomas
    //            rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "Idiomas");//Indica si hay que filtrar o no
    //            rdCurriculum.SetParameterValue("@t020_idcodidioma", (sFiltros[11] == "") ? 0 : int.Parse(sFiltros[11]), "Idiomas");//IdIdioma
    //            rdCurriculum.SetParameterValue("@nivelidioma", (sFiltros[12] == "") ? 0 : int.Parse(sFiltros[12]), "Idiomas");//NivelIdioma


    //            //EXPERIENCIA PROFESIONAL
    //            //ExpFuera
    //            rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "ExpFuera");//Indica si hay que filtrar o no
    //            rdCurriculum.SetParameterValue("@nombrecuenta", sFiltros[13], "ExpFuera");//Clientes/Cuentas
    //            rdCurriculum.SetParameterValue("@idcuenta", (sFiltros[14] == "") ? 0 : int.Parse(sFiltros[14]), "ExpFuera");//Id Clientes/Cuentas
    //            rdCurriculum.SetParameterValue("@t483_idsector", (sFiltros[15] == "") ? 0 : int.Parse(sFiltros[15]), "ExpFuera");//IdSector
    //            rdCurriculum.SetParameterValue("@t035_codperfile", (sFiltros[16] == "") ? 0 : int.Parse(sFiltros[16]), "ExpFuera");//IdPerfil
    //            rdCurriculum.SetParameterValue("@let036_idcodentorno", sFiltros[17], "ExpFuera");//Lista EntornoTecnologicos Experiencia


    //            //ExpIber
    //            rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "ExpIber");//Indica si hay que filtrar o no
    //            rdCurriculum.SetParameterValue("@nombrecuenta", sFiltros[13], "ExpIber");//Clientes/Cuentas
    //            rdCurriculum.SetParameterValue("@idcuenta", (sFiltros[14] == "") ? 0 : int.Parse(sFiltros[14]), "ExpIber");//Id Clientes/Cuentas
    //            rdCurriculum.SetParameterValue("@t483_idsector", (sFiltros[15] == "") ? 0 : int.Parse(sFiltros[15]), "ExpIber");//IdSector
    //            rdCurriculum.SetParameterValue("@t035_codperfile", (sFiltros[16] == "") ? 0 : int.Parse(sFiltros[16]), "ExpIber");//IdPerfil
    //            rdCurriculum.SetParameterValue("@let036_idcodentorno", sFiltros[17], "ExpIber");//Lista EntornoTecnologicos Experiencia

    //            rdCurriculum.SetParameterValue("visible_dorganizativos", aDesglose[0]);
    //            rdCurriculum.SetParameterValue("visible_formacion", aDesglose[1]);//Formacion(Titulo)
    //            rdCurriculum.SetParameterValue("visible_formacad", aDesglose[2]);//Formacion Academica (SubReport)
    //            rdCurriculum.SetParameterValue("visible_currec", aDesglose[3]);//Acciones formativas recibidas (SubReport)
    //            rdCurriculum.SetParameterValue("visible_curimp", aDesglose[4]);//Acciones formativas impartidas (SubReport)
    //            rdCurriculum.SetParameterValue("visible_certexam", aDesglose[5]);//Certificados Examenes (SubReport)
    //            rdCurriculum.SetParameterValue("visible_idiomas", aDesglose[6]);//Idiomas (SubReport)
    //            rdCurriculum.SetParameterValue("visible_experiencia", aDesglose[7]);//Experiencia profesional (Titulo)
    //            rdCurriculum.SetParameterValue("visible_expfuera", aDesglose[8]);//Experiencia en Ibermatica (SubReport)
    //            rdCurriculum.SetParameterValue("visible_expiber", aDesglose[9]);//Experiencia fuera de Ibermatica (SubReport)

    //            //DESGLOSE FORMACION ACADEMICA
    //            rdCurriculum.SetParameterValue("visible_FACADTipo", aDesgloseFACAD[0]);//Formacion Academica Tipo (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_FACADModalidad", aDesgloseFACAD[1]);//Formacion Academica Modalidad (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_FACADTic", aDesgloseFACAD[2]);//Formacion Academica Tic (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_FACADFInicio", aDesgloseFACAD[3]);//Formacion Academica FInicio (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_FACADFFin", aDesgloseFACAD[4]);//Formacion Academica FFin (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_FACADEspecialidad", aDesgloseFACAD[5]);//Formacion Academica Especialidad (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_FACADCentro", aDesgloseFACAD[6]);//Formacion Academica Centro (Dato SubReport)

    //            //DESGLOSE IDIOMAS
    //            rdCurriculum.SetParameterValue("visible_IdiomaLectura", aDesgloseIdioma[0]);//Idioma Lectura (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_IdiomaEscritura", aDesgloseIdioma[1]);//Idioma Escritura (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_IdiomaOral", aDesgloseIdioma[2]);//Idioma Oral (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_IdiomaTitulo", aDesgloseIdioma[3]);//Idioma Titulo (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_IdiomaFObtencion", aDesgloseIdioma[4]);//Idioma FObtencion (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_IdiomaCentro", aDesgloseIdioma[5]);//Idioma Centro (Dato SubReport)

    //            //DESGLOSE Datos Organizativos
    //            rdCurriculum.SetParameterValue("visible_DOEmpresa", aDesgloseDO[0]);//Datos organizativos Empresa
    //            rdCurriculum.SetParameterValue("visible_DOUnidadNegocio", aDesgloseDO[1]);//Datos organizativos Unidad de negocio
    //            rdCurriculum.SetParameterValue("visible_DOCR", aDesgloseDO[2]);//Datos organizativos CR
    //            rdCurriculum.SetParameterValue("visible_DOFAntiguedad", aDesgloseDO[3]);//Datos organizativos F.Antiguedad
    //            rdCurriculum.SetParameterValue("visible_DORol", aDesgloseDO[4]);//Datos organizativos Rol
    //            rdCurriculum.SetParameterValue("visible_DOPerfil", aDesgloseDO[5]);//Datos organizativos Perfil
    //            rdCurriculum.SetParameterValue("visible_DOOficina", aDesgloseDO[6]);//Datos organizativos Oficina
    //            rdCurriculum.SetParameterValue("visible_DOProvincia", aDesgloseDO[7]);//Datos organizativos Provincia
    //            rdCurriculum.SetParameterValue("visible_DOPais", aDesgloseDO[8]);//Datos organizativos Pais
    //            rdCurriculum.SetParameterValue("visible_DOTrayectoria", aDesgloseDO[9]);//Datos organizativos Trayectoria
    //            rdCurriculum.SetParameterValue("visible_DOMovilidad", aDesgloseDO[10]);//Datos organizativos Movilidad
    //            rdCurriculum.SetParameterValue("visible_DOObservaciones", aDesgloseDO[11]);//Datos organizativos Observaciones


    //            //DESGLOSE CURsos RECibidos
    //            rdCurriculum.SetParameterValue("visible_CURRECProveedor", aDesgloseCURREC[0]);//Cursos Recibidos Proveedor (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECEntornoTecno", aDesgloseCURREC[1]);//Cursos Recibidos Entorno tecnologico (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECProvincia", aDesgloseCURREC[2]);//Cursos Recibidos Provincia (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECHoras", aDesgloseCURREC[3]);//Cursos Recibidos Horas (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECFInicio", aDesgloseCURREC[4]);//Cursos Recibidos F. Inicio(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECFFin", aDesgloseCURREC[5]);//Cursos Recibidos F. Fin (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECTipo", aDesgloseCURREC[6]);//Cursos Recibidos Tipo (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECModalidad", aDesgloseCURREC[7]);//Cursos Recibidos Modalidad (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURRECContenido", aDesgloseCURREC[8]);//Cursos Recibidos Contenido (Dato SubReport)

    //            //DESGLOSE Certificados
    //            rdCurriculum.SetParameterValue("visible_CERTEXAMProveedor", aDesgloseCERTEXAM[0]);//Certificados Proveedor (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CERTEXAMEntornoTecno", aDesgloseCERTEXAM[1]);//Certificados Entorno tecnologico (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CERTEXAMFObtencion", aDesgloseCERTEXAM[2]);//Certificados F. Obtencion(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CERTEXAMTipo", aDesgloseCERTEXAM[3]);//Certificados Tipo (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CERTEXAMFCaducidad", aDesgloseCERTEXAM[4]);//Certificados F. Caducidad (Dato SubReport)

    //            //DESGLOSE EXPERIENCIA FUERA
    //            rdCurriculum.SetParameterValue("visible_EXPFUERAEmpresaOri", aDesgloseEXPFUERA[0]);//Experiencia Fuera Empresa Origen (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERACliente", aDesgloseEXPFUERA[1]);//Experiencia Fuera Cliente(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERAFuncion", aDesgloseEXPFUERA[2]);//Experiencia Fuera Funcion(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERAFinicio", aDesgloseEXPFUERA[3]);//Experiencia Fuera Fecha Inicio(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERAFFin", aDesgloseEXPFUERA[4]);//Experiencia Fuera Fecha Fin(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERADescripcion", aDesgloseEXPFUERA[5]);//Experiencia Fuera Descripcion(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERAACSACT", aDesgloseEXPFUERA[6]);//Experiencia Fuera ACS-ACT(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERASector", aDesgloseEXPFUERA[7]);//Experiencia Fuera Sector(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERASegmento", aDesgloseEXPFUERA[8]);//Experiencia Fuera Segmento(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERAPerfil", aDesgloseEXPFUERA[9]);//Experiencia Fuera Perfil(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERAEntorno", aDesgloseEXPFUERA[10]);//Experiencia Fuera Entorno(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPFUERANmes", aDesgloseEXPFUERA[11]);//Experiencia Fuera Nº Mes(Dato SubReport)

    //            //SINOPSIS

    //            rdCurriculum.SetParameterValue("visible_sinopsis", aDesglose[10]);//Sinopsis(Titulo)

    //            //ACCIONES FORMATIVAS
    //            //rdCurriculum.SetParameterValue("visible_cursos", aDesglose[11]);//Acciones formativas

    //            //DESGLOSE EXPERIENCIA IBERMATICA
    //            rdCurriculum.SetParameterValue("visible_EXPIBERCliente", aDesgloseEXPIBER[0]);//Experiencia Ibermatica Cliente(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERFuncion", aDesgloseEXPIBER[1]);//Experiencia Ibermatica Funcion(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERFinicio", aDesgloseEXPIBER[2]);//Experiencia Ibermatica Fecha Inicio(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERFFin", aDesgloseEXPIBER[3]);//Experiencia Ibermatica Fecha Fin(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERDescripcion", aDesgloseEXPIBER[4]);//Experiencia Ibermatica Descripcion(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERACSACT", aDesgloseEXPIBER[5]);//Experiencia Ibermatica ACS-ACT(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERSector", aDesgloseEXPIBER[6]);//Experiencia Ibermatica Sector(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERSegmento", aDesgloseEXPIBER[7]);//Experiencia Ibermatica Segmento(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERPerfil", aDesgloseEXPIBER[8]);//Experiencia Ibermatica Perfil(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBEREntorno", aDesgloseEXPIBER[9]);//Experiencia Ibermatica Entorno(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_EXPIBERNmes", aDesgloseEXPIBER[10]);//Experiencia Ibermatica Nº Mes(Dato SubReport)

    //            //DESGLOSE DATOS PERSONALES
    //            rdCurriculum.SetParameterValue("visible_DPFoto", aDesgloseDP[0]);//Datos Personales FOTO
    //            rdCurriculum.SetParameterValue("visible_DPNIF", aDesgloseDP[1]);//Datos Personales NIF
    //            rdCurriculum.SetParameterValue("visible_DPFNacimiento", aDesgloseDP[2]);//Datos Personales F Nacimiento
    //            rdCurriculum.SetParameterValue("visible_DPNacionalidad", aDesgloseDP[3]);//Datos Personales Nacionalidad
    //            rdCurriculum.SetParameterValue("visible_DPSexo", aDesgloseDP[4]);//Datos Personales Sexo

    //            //DESGLOSE CURsos IMPartidos
    //            rdCurriculum.SetParameterValue("visible_CURIMPProveedor", aDesgloseCURIMP[0]);//Cursos Impartidos Proveedor (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPEntornoTecno", aDesgloseCURIMP[1]);//Cursos Impartidos Entorno tecnologico (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPProvincia", aDesgloseCURIMP[2]);//Cursos Impartidos Provincia (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPHoras", aDesgloseCURIMP[3]);//Cursos Impartidos Horas (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPFInicio", aDesgloseCURIMP[4]);//Cursos Impartidos F. Inicio(Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPFFin", aDesgloseCURIMP[5]);//Cursos Impartidos F. Fin (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPTipo", aDesgloseCURIMP[6]);//Cursos Impartidos Tipo (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPModalidad", aDesgloseCURIMP[7]);//Cursos Impartidos Modalidad (Dato SubReport)
    //            rdCurriculum.SetParameterValue("visible_CURIMPContenido", aDesgloseCURIMP[8]);//Cursos Impartidos Contenido (Dato SubReport)
    //        }
    //        catch (Exception ex)
    //        {
    //            rdCurriculum.Close();
    //            rdCurriculum.Dispose();
    //            return "Error@#@" + Errores.mostrarError("Error al actualizar los parámetros del report: ", ex);
    //        }

    //        #endregion

    //        #region Exportar a disco

    //        //string strFichero = aCIP[0];
    //        string strFichero = Session["IDFICEPI_CVT_ACTUAL"].ToString();
    //        strFichero = Server.MapPath(".") + @"\" + strFichero;
    //        if (strTipoFormato == "PDF")
    //        {
    //            exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
    //            strFichero = strFichero + ".pdf";
    //        }
    //        else
    //        {
    //            //       WORD
    //            exportOpts.ExportFormatType = ExportFormatType.EditableRTF;
    //            strFichero = strFichero + ".doc";
    //        }

    //        exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;

    //        // set the disk file options

    //        //diskOpts.DiskFileName = Server.MapPath(".") + @"\" + strFichero;
    //        diskOpts.DiskFileName = strFichero;
    //        exportOpts.ExportDestinationOptions = diskOpts;

    //        try
    //        {
    //            rdCurriculum.Export(exportOpts);
    //        }
    //        catch (Exception ex)
    //        {
    //            rdCurriculum.Close();
    //            rdCurriculum.Dispose();
    //            return "Error@#@" + Errores.mostrarError("Error al exportar el report: ", ex);
    //        }

    //        #endregion

    //        #region Enviar por correo

    //        string strMensaje = "";
    //        string strCabecera = "";
    //        string strDatos = "";
    //        string strAsunto = "";
    //        string strTO = "";

    //        strAsunto = "Aviso de CV's de profesionales";
    //        strCabecera = @" <LABEL class='TITULO'>Se adjuntan los curriculums de los profesionales que se muestran a continuación:</LABEL>";


    //        //string strNombreProfesionales = (Request.Form["hdnNombreProfesionales"] == null) ? Request.Form["ctl00$CPHC$hdnNombreProfesionales"] : Request.Form["hdnNombreProfesionales"];          
    //        //string[] aNombreProfesionales = Regex.Split(strNombreProfesionales, @"/");

    //        string[] aNombreProfesionales = Regex.Split(sNombreProfesionales, @"/");

    //        strDatos = "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
    //        for (int i = 0; i < aNombreProfesionales.Length; i++)
    //        {
    //            if (aNombreProfesionales[i] == "") continue;
    //            strDatos += "<tr>";
    //            strDatos += "<td width='100%'>-&nbsp;&nbsp;" + aNombreProfesionales[i].ToString() + "</td>";
    //            strDatos += "</tr>";
    //        }
    //        strDatos += "</table>";

    //        ArrayList aListCorreo = new ArrayList();

    //        //string strDestinatarios = (Request.Form["hdnDestinatarios"] == null) ? Request.Form["ctl00$CPHC$hdnDestinatarios"] : Request.Form["hdnDestinatarios"];
    //        //string[] aDestinatarios = Regex.Split(strDestinatarios, @",");

    //        string[] aDestinatarios = Regex.Split(sDestinatarios, @",");

    //        for (int i = 0; i < aDestinatarios.Length; i++)
    //        {
    //            try
    //            {
    //                strMensaje = strCabecera + strDatos;
    //                strTO = aDestinatarios[i].ToString();

    //                string[] aMail = { strAsunto, strMensaje, strTO, strFichero };
    //                aListCorreo.Add(aMail);
    //            }
    //            catch (System.Exception objError)
    //            {
    //                return "Error@#@" + Errores.mostrarError("Error al enviar el mail a evaluadores.", objError);
    //            }
    //        }

    //        if (aDestinatarios.Length != 0) SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);

    //        File.Delete(strFichero);
    //        #endregion

    //        return "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al generar el report y enviarlo por correo.", ex);
    //    }
    //}
    /// <summary>
    /// Devuelve la sql que hay ejecutar para conseguir la lista de documentos a mostrar para exportar
    /// </summary>
    /// <param name="cadenaCERT"></param>
    /// <returns></returns>
    private static string generarCadenaDocExport(string cadena)
    {
        string[] aCadena = Regex.Split(cadena, "");
        string sqlResult = "";
        for (var i = 1; i < aCadena.Length; i++)
        {
            if (aCadena[i] == " " || aCadena[i] == "") sqlResult += "";
            else
            {
                if (aCadena.Length - 1 > i + 3 && aCadena[i].ToUpper() == "A" && aCadena[i + 1].ToUpper() == "N" && aCadena[i + 2].ToUpper() == "D" && aCadena[i + 3] == " ")
                {
                    sqlResult += @"##";
                    i = i + 2;
                }
                else if (aCadena.Length - 1 > i + 2 && aCadena[i].ToUpper() == "O" && aCadena[i + 1].ToUpper() == "R" && aCadena[i + 2] == " ")
                {
                    sqlResult += @"##";
                    i = i + 1;
                }
                else if (caracterEspecial(aCadena[i]))
                {
                    //sqlResult += aCadena[i];
                }
                else
                {
                    if (aCadena[i] == "\"")
                    {
                        i++;
                        while (aCadena[i] != "\"")
                        {
                            sqlResult += aCadena[i];
                            i++;
                        }
                    }
                    else
                    {
                        while (aCadena.Length > i && aCadena[i] != " " && aCadena[i] != ")")
                        {
                            if (aCadena[i] == "?") sqlResult += "_";
                            else if (aCadena[i] == "*") sqlResult += "%";
                            else
                            {
                                sqlResult += aCadena[i];
                            }
                            i++;
                        }
                        i--;
                    }
                }
            }
        }
        return sqlResult;
    }

    /// <summary>
    /// Monta una tabla HTML con las denominaciones de los elementos de sListaCod + los elementos de sListaDen que tienen los profesionales de sListaFicepis
    /// </summary>
    /// <param name="sTipo">Tabla en la que hay que buscar los datos</param>
    /// <param name="sListaFicepis">lista de idFicepis separarada por comas</param>
    /// <param name="sListaCod">Lista de códigos de elementos separada por ##</param>
    /// <param name="sListaDen">Lista de denominaciones de elementos separada por ##</param>
    /// <returns></returns>
    private static string MontarTablaDocs(string sTipo, string sListaFicepis, string sListaCod, string sListaDen)
    {
        StringBuilder sb = new StringBuilder();

        switch (sTipo)
        {
            case "FA":
                List<Titulacion> oListaFA = SUPER.BLL.Titulacion.GetListaPorProfesional(sListaFicepis, sListaCod, sListaDen);
                sb.Append(@"<table id='tblFAExport' style='width:340px;' class='MANO'>
                        <colgroup>
                            <col style='width:20px;' />
                            <col style='width:320px;' />
                        </colgroup>");
                foreach (Titulacion oTit in oListaFA)
                {
                    sb.Append("<tr id=" + oTit.T019_IDCODTITULO.ToString() + " style='height:20px;'>");
                    sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='ponerBandera(this,1)'></td>");
                    sb.Append("<td><nobr class='NBR W320' onmouseover='TTip(event)'>" + oTit.T019_DESCRIPCION + "</nobr></td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                break;
            case "CURSO":
                List<Curso> oListaCurso = SUPER.BLL.Curso.GetListaPorProfesional(sListaFicepis, sListaCod, sListaDen);
                sb.Append(@"<table id='tblCursoExport' style='width:340px;' class='MANO'>
                        <colgroup>
                            <col style='width:20px;' />
                            <col style='width:320px;' />
                        </colgroup>");
                foreach (Curso oCurso in oListaCurso)
                {
                    sb.Append("<tr id=" + oCurso.T574_IDCURSO.ToString() + " style='height:20px;'>");
                    sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='ponerBandera(this,2)'></td>");
                    sb.Append("<td><nobr class='NBR W320' onmouseover='TTip(event)'>" + oCurso.T574_TITULO + "</nobr></td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                break;
            case "CERT":
                List<Certificado> oLista = SUPER.BLL.Certificado.GetListaPorProfesional(sListaFicepis, sListaCod, sListaDen);
                sb.Append(@"<table id='tblCertExport' style='width:340px;' class='MANO'>
                        <colgroup>
                            <col style='width:20px;' />
                            <col style='width:320px;' />
                        </colgroup>");
                foreach(Certificado oCert in oLista)
                {
                    sb.Append("<tr id=" + oCert.T582_IDCERTIFICADO.ToString() + " style='height:20px;'>");
                    sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='ponerBandera(this,3)'></td>");
                    sb.Append("<td><nobr class='NBR W320' onmouseover='TTip(event)'>" + oCert.T582_NOMBRE + "</nobr></td>");
                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                break;
            case "IDIOMA":
                //Los códigos son de idioma y las denominaciones de títulos
                List<Idioma> oListaTitIdioma = SUPER.BLL.Idioma.GetListaPorProfesional(sListaFicepis, sListaCod, sListaDen);
                sb.Append(@"<table id='tblIdiomaExport' style='width:340px;' class='MANO'>
                        <colgroup>
                            <col style='width:20px;' />
                            <col style='width:320px;' />
                        </colgroup>");
                foreach (Idioma oTitIdioma in oListaTitIdioma)
                {
                    sb.Append("<tr id=" + oTitIdioma.t020_idcodidioma.ToString() + " style='height:20px;'>");
                    sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='ponerBandera(this,4)'></td>");
                    sb.Append("<td><nobr class='NBR W320' onmouseover='TTip(event)'>" + oTitIdioma.t021_titulo + "</nobr></td>");
                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                break;
        }
        return sb.ToString();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ConsultaCadena(string sFA, string sID, string sEXP, string sCU, string sOT, string sCE,
                        string sCondicion, Nullable<char> tipoProf, Nullable<char> estadoProf,
                        Nullable<int> idNodo, Nullable<int> sn1, Nullable<int> sn2, Nullable<int> sn3, Nullable<int> sn4,
                        Nullable<short> idCentrab, Nullable<short> cvMovilidad, Nullable<bool> cvInternacional,
                        Nullable<int> idCodPerfil, Nullable<byte> disponibilidad, Nullable<decimal> costeJornada,
                        string sCadena)
    {
        bool bTitulaciones = false, bIdiomas = false, bExperiencia = false, bCursos = false, bOtros = false, 
             bCertExam=false, bCondicion = false;
        if (sFA == "1") bTitulaciones = true;
        if (sID == "1") bIdiomas = true;
        if (sEXP == "1") bExperiencia = true;
        if (sCU == "1") bCursos = true;
        if (sOT == "1") bOtros = true;
        if (sCE == "1") bCertExam = true;
        if (sCondicion == "1") bCondicion = true;

        //Quito caracteres que pueden dar problemas con el LIKE en BBDD
        sCadena = sCadena.Replace("[", " ");
        sCadena = sCadena.Replace("]", " ");
        sCadena = sCadena.Replace("%", " ");
        sCadena = sCadena.Replace("_", " ");
        sCadena = sCadena.Replace("|", " ");
        DataTable dt = Curriculum.getProfesionalesConsultaCadena(null, (int)HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"],
                                                                bTitulaciones, bIdiomas, bExperiencia, bCursos, bOtros, bCertExam, bCondicion,
                                                                tipoProf, estadoProf, idNodo, sn1, sn2, sn3, sn4,
                                                                idCentrab, cvMovilidad, cvInternacional,
                                                                idCodPerfil, disponibilidad, costeJornada, sCadena);

        StringBuilder sb = new StringBuilder();
        sb.Append(@"<table id='tblDatos' style='width:570px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                            <col style='width:30px;' />
                            <col style='width:20px;' />
                            <col style='width:280px;' />
                            <col style='width:200px;' />
                            <col style='width:20px;' />
                            <col style='width:20px;' />
                        </colgroup>");

        foreach (DataRow oFila in dt.Rows)
        {
            sb.Append("<tr id='" + oFila["T001_IDFICEPI"].ToString() + "' selected='false' style='height:20px;'");
            sb.Append("tipo='" + oFila["T001_TIPORECURSO"].ToString() + "' ");
            sb.Append("sexo='" + oFila["T001_SEXO"].ToString() + "' ");
            sb.Append("baja='" + oFila["BAJA"].ToString() + "' ");
            sb.Append("pdte=\"" + (((bool)oFila["Pendiente"]) ? "1" : "0") + "\" ");

            sb.Append("nodo=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
            sb.Append("perfil=\"" + Utilidades.escape(oFila["Perfil"].ToString()) + "\" ");
            sb.Append("disponible=\"" + oFila["disponible"].ToString() + "\" ");
            sb.Append("provincia=\"" + Utilidades.escape(oFila["provincia"].ToString()) + "\" ");
            sb.Append("pais=\"" + Utilidades.escape(oFila["pais"].ToString()) + "\" ");

            string sTooltip = "<label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style=width:70px;>Perfil:</label>" + oFila["Perfil"].ToString() + "<br><label style=width:70px;>Disponibilidad:</label>" + oFila["disponible"].ToString() + " %" + "<br><label style=width:70px;>Provincia:</label>" + oFila["provincia"].ToString() + "<br><label style=width:70px;>País:</label>" + oFila["pais"].ToString();
            sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");
            //Datos de alertas 
            sb.Append(">");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td>" + oFila["Profesional"].ToString() + "</td>");
            //20/11/2015 Mikel PPOO nos pide que no se muestre el perfil
            //sb.Append("<td>" + oFila["Perfil"].ToString() + "</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        dt.Dispose();

        //Se inserta en la tabla de log (3 -> Consulta por cadena)
        SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 3);

        return sb.ToString();
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ConsultaQuery(string cadenaFA, string cadenaEXP, string cadenaCR, string cadenaCI, string cadenaCERT,
                                        string cadenaEX, string cadenaID, string cadenaOT,
                                        string operador, string idPerfil, string idCentro, string trayInt, string movilidad, 
                                        string disponibilidad, string tipoProf, string SN4, string SN3, string SN2, string SN1,
                                        string nodo, string estado, string coste)
    {
        bool controlWhere = false; // variable para controlar si ya está el operador lógico en la consulta (false hay que añadir el or/and, true no hacer nada)
        string sTempFA = "", sTempEXP = "", sTempCR = "", sTempCI = "", sTempCERT = "", sTempEX = "", sTempID = "", sTempOT = "";
        string resultado = "";

        #region Select básica
        resultado = @"
;DECLARE @Ambito TABLEGENERICO 
INSERT INTO @Ambito SELECT distinct t001_idficepi
		from dbo.F061_SUP_CVT_PROF_AMBITO(" + HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString() + @")
select distinct T001.T001_IDFICEPI, 
dbo.[FRM_CONCATENAR](T001.T001_APELLIDO1, T001.T001_APELLIDO2, T001.T001_NOMBRE, T001.T001_ALIAS) as NOMBRE, 
case when isNull(T001.T001_FECBAJA, '20790606') >= dbo.f033_fsh(getdate()) then 0 else 1 end as BAJA, 
T001_TIPORECURSO, 
T001_SEXO, 
isNull(t303_denominacion, '') as t303_denominacion, 
isNull(T035_DESCRIPCION, '') as Perfil, 
isNull(T535_PORCENTAJE, 0) as Disponible, 
t173_denominacion as provincia, 
t172_denominacion as pais,
case when  AMB.T001_IDFICEPI IS null then 0 else 1 end as Estado 
FROM T001_FICEPI T001 with (nolock) 
left join dbo.F071_SUP_CVPENDIENTE(@Ambito) AMB 
on AMB.T001_IDFICEPI = T001.T001_IDFICEPI
left join T010_OFICINA T010 with (nolock) 
on T001.t010_idoficina = T010.t010_idoficina 
left join T009_CENTROTRAB T009 with (nolock) 
on T010.t009_idcentrab = T009.t009_idcentrab 
left join T173_PROVINCIA T173 with (nolock) 
on T009.t173_idprovincia = T173.t173_idprovincia 
left join T172_PAIS T172 with (nolock) 
on T173.t172_idpais = T172.t172_idpais 
left join T035_PERFILEXPER T035 with (nolock) 
on T001.T035_IDCODPERFIL = T035.T035_IDCODPERFIL 
left join T535_DISPONIBLE T535 with (nolock) 
on T535.T001_IDFICEPI = T001.T001_IDFICEPI 
and dbo.f033_fsh(getdate()) between T535_FDESDE and isNull(T535_FHASTA, '20790606')
";
        if (nodo != "" || SN1 != "")
        {
            resultado += @" inner join T303_NODO T303 with (nolock) on T001.t303_idnodo_profesional_aux = T303.t303_idnodo ";
            if (nodo != "")
            {
                resultado += @" and T001.t303_idnodo_profesional_aux = '" + nodo + @"' ";
            }
            if (SN1 != "")
            {
                resultado += @" and T303.T391_IDSUPERNODO1 = '" + SN1 + @"' ";
            }
        }
        else if (SN2 != "")
        {
            resultado += @" inner join T303_NODO T303 with (nolock)
on T001.t303_idnodo_profesional_aux = T303.t303_idnodo 
inner join T391_SUPERNODO1 T391 with (nolock)
on T303.T391_IDSUPERNODO1 = T391.T391_IDSUPERNODO1
and T391.T392_IDSUPERNODO2 = '" + SN2 + @"'
";
        }
        else if (SN3 != "")
        {
            resultado += @" inner join T303_NODO T303 with (nolock)
on T001.t303_idnodo_profesional_aux = T303.t303_idnodo 
inner join T391_SUPERNODO1 T391 with (nolock)
on T303.T391_IDSUPERNODO1 = T391.T391_IDSUPERNODO1
inner join T392_SUPERNODO2 T392 with (nolock)
on T391.T392_IDSUPERNODO2 = T392.T392_IDSUPERNODO2
and T392.T393_IDSUPERNODO3 = '" + SN3 + @"'
";
        }
        else if (SN4 != "")
        {
            resultado += @" inner join T303_NODO T303 with (nolock)
on T001.t303_idnodo_profesional_aux = T303.t303_idnodo 
inner join T391_SUPERNODO1 T391 with (nolock)
on T303.T391_IDSUPERNODO1 = T391.T391_IDSUPERNODO1
inner join T392_SUPERNODO2 T392 with (nolock)
on T391.T392_IDSUPERNODO2 = T392.T392_IDSUPERNODO2
inner join T393_SUPERNODO3 T393 with (nolock)
on T392.T393_IDSUPERNODO3 = T393.T393_IDSUPERNODO3
and T393.T394_IDSUPERNODO4 = '" + SN4 + @"'
";
        }
        else
        {
            resultado += @" left join T303_NODO T303 with (nolock) 
on T001.t303_idnodo_profesional_aux = T303.t303_idnodo 
";
        }

        resultado += @" where T001.T001_IDFICEPI IN (select  CODIGOINT FROM @Ambito)";
        if (idPerfil != "")
        {
            //resultado += " " + operador + " ";
            resultado += " AND T001.T035_IDCODPERFIL = " + idPerfil;
        }
        if (idCentro != "")
        {
            //resultado += " " + operador + " ";
            resultado += " AND T001.T010_IDOFICINA in (select T010_IDOFICINA T010 from T010_OFICINA T010 where T010.T009_IDCENTRAB = " + idCentro + ")";
        }
        if (trayInt != "")
        {
            //resultado += " " + operador + " ";
            resultado += " AND T001.T001_CVINTERNACIONAL = " + ((trayInt == "True") ? 1 : 0);
        }
        if (movilidad != "")
        {
            //resultado += " " + operador + " ";
            resultado += " AND T001.T001_CVMOVILIDAD = " + movilidad;
        }
        if (disponibilidad != "")
        {
            //resultado += " " + operador + " ";
            resultado += " AND T535.T535_PORCENTAJE >=" + disponibilidad;
        }
        if (tipoProf != "")
        {
            //resultado += " " + operador + " ";
            if (tipoProf == "I")
                resultado += " AND T001.T001_TIPORECURSO = 'I'";
            else
                resultado += " AND T001.T001_TIPORECURSO in ('T', 'E', 'B', 'G')";
        }

        switch (estado)
        {
            case "A":
                //resultado += " " + operador + " ";
                resultado += " AND isNull(T001.t001_fecbaja, '20790606') >= dbo.f033_fsh(getdate())";
                break;
            case "B":
                //resultado += " " + operador + " ";
                resultado += " AND isNull(T001.t001_fecbaja, '20790606') < dbo.f033_fsh(getdate())";
                break;
        }
        if (coste != "" && HttpContext.Current.Session["CVCONSULTACOSTE"].ToString() == "")
        {
            resultado += @" (" + coste + @" >= 
						(
						select  (T314.t314_costejornada * isnull(T422.T422_TIPOCAMBIO,1))
						from 	T314_USUARIO T314 with (nolock)
						inner join  T422_MONEDA T422 with (nolock) on T314.T422_IDMONEDA = T422.T422_IDMONEDA
						where 	T314.T001_IDFICEPI = T001.T001_IDFICEPI 
						and 	(T314.t314_costejornada * isnull(T422.T422_tipocambio,1)) > 0.1	
						and  (	(T314.t314_fbaja is null
							and not exists --otro usuario 'posterior' dado de alta
									(
									select 1 
									from T314_USUARIO T314B with (nolock)
									where T314.T001_IDFICEPI = T314B.T001_IDFICEPI 
									and T314B.T314_FBAJA is null			
									and T314B.T314_IDUSUARIO > T314.T314_IDUSUARIO
									) 
							)
							or
							(
							T314.t314_fbaja is not null
							and not exists	--otro usuario de alta o 'posterior'
									(
									select 1 
									from T314_USUARIO T314B with (nolock)
									where T314.T001_IDFICEPI = T314B.T001_IDFICEPI 
									and (T314B.T314_FBAJA is null			
										or T314B.T314_IDUSUARIO > T314.T314_IDUSUARIO)
									)
							)
						    )
						)
		)";
        }
        if (operador == "OR")
        {
            resultado += " AND (";
        }
        else
            controlWhere = true;

        #endregion

        #region Filtros de cadena
        //resultado += " "+ operador + " T001.T001_IDFICEPI ";
        if (cadenaFA.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            //resultado += generarCadenaFA(cadenaFA);
            string[] aQueryFA = Regex.Split(generarConsultaCadenaBusqueda(cadenaFA, "#TEMP_FA", "t019_descripcion", "F074_SUP_CVT_CADENA_FORMACIONACADEMICA"), "{separador}");
            sTempFA = aQueryFA[0];
            resultado += aQueryFA[1];
            resultado += ") ";
            if (!controlWhere) controlWhere = true;
        }

        if (cadenaEXP.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            //resultado += generarCadenaEXP(cadenaEXP);
            string[] aQueryEXP = Regex.Split(generarConsultaCadenaBusqueda(cadenaEXP, "#TEMP_EXP", "t808_denominacion", "F075_SUP_CVT_CADENA_EXPERIENCIAPROF"), "{separador}");
            sTempEXP = aQueryEXP[0];
            resultado += aQueryEXP[1];
            resultado += ") ";
            if (!controlWhere) controlWhere = true;
        }

        if (cadenaCR.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            //resultado += generarCadenaCR(cadenaCR);
            string[] aQueryCR = Regex.Split(generarConsultaCadenaBusqueda(cadenaCR, "#TEMP_CR", "t574_titulo", "F076_SUP_CVT_CADENA_CURSOSRECIBIDOS"), "{separador}");
            sTempCR = aQueryCR[0];
            resultado += aQueryCR[1];
            resultado += ") ";
            if (!controlWhere) controlWhere = true;
        }

        if (cadenaCI.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            //resultado += generarCadenaCI(cadenaCI);
            string[] aQueryCI = Regex.Split(generarConsultaCadenaBusqueda(cadenaCI, "#TEMP_CI", "t574_titulo", "F077_SUP_CVT_CADENA_CURSOSIMPARTIDOS"), "{separador}");
            sTempCI = aQueryCI[0];
            resultado += aQueryCI[1];
            resultado += ") ";
            if (!controlWhere) controlWhere = true;
        }

        if (cadenaCERT.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            //resultado += generarCadenaCERT(cadenaCERT);
            string[] aQueryCERT = Regex.Split(generarConsultaCadenaBusqueda(cadenaCERT, "#TEMP_CERT", "t582_nombre", "F073_SUP_CVT_CADENA_CERTIFICADOS"), "{separador}");
            sTempCERT = aQueryCERT[0];
            resultado += aQueryCERT[1];
            resultado += ") ";
            if (!controlWhere) controlWhere = true;
        }

        if (cadenaEX.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            //resultado += generarCadenaEX(cadenaEX);
            string[] aQueryEX = Regex.Split(generarConsultaCadenaBusqueda(cadenaEX, "#TEMP_EX", "TITULO", "F078_SUP_CVT_CADENA_EXAMENES"), "{separador}");
            sTempEX = aQueryEX[0];
            resultado += aQueryEX[1];
            resultado += ") ";
            if (!controlWhere) controlWhere = true;
        }

        if (cadenaID.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            //resultado += generarCadenaID(cadenaID);
            string[] aQueryID = Regex.Split(generarConsultaCadenaBusqueda(cadenaID, "#TEMP_ID", "t020_descripcion", "F079_SUP_CVT_CADENA_IDIOMAS"), "{separador}");
            sTempID = aQueryID[0];
            resultado += aQueryID[1];
            resultado += ")";
            if (!controlWhere) controlWhere = true;
        }
        if (cadenaOT.Length != 0)
        {
            if (controlWhere) resultado += operador;
            resultado += " T001.T001_IDFICEPI in (";
            string[] aQueryID =
                Regex.Split(generarConsultaCadenaBusqueda(cadenaOT, "#TEMP_OT", "T185_SINOPSIS",
                                                          "[F072_SUP_CVT_CADENA_OTROS]"), "{separador}");
            sTempOT = aQueryID[0];
            resultado += aQueryID[1];
            resultado += ")";
            if (!controlWhere) controlWhere = true;
        }
        #endregion

        if (operador == "OR")
            resultado += ") ";
        resultado += "order by 2";
        //return Curriculum.ConsultaCadena(resultado);

        //SUPER.DAL.Log.Insertar( HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + " ha hecho uso de la consulta por Query");

        //Se inserta en la tabla de log (4 -> Consulta por query)
        SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 4);

        return Curriculum.ConsultaQuery(sTempFA + sTempEXP + sTempCR + sTempCI + sTempCERT + sTempEX + sTempID + sTempOT + resultado);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string MostrarPendientes(int t001_idficepi)
    {
        return SUPER.BLL.Curriculum.CatalogoPendiente(t001_idficepi);
    }

    private static string generarConsultaCadenaBusqueda(string cadena, string sTablaTemporal, string sCampoDenominacion, string sFuncionSQL)
    {
        //Quito los caracteres que pueden dar problemas con el LIKE en BBDD
        cadena = cadena.Replace("[", " ");
        cadena = cadena.Replace("]", " ");
        cadena = cadena.Replace("%", " ");
        cadena = cadena.Replace("_", " ");
        cadena = cadena.Replace("|", " ");
        
        string[] aCadena = Regex.Split(cadena, "");
        string sElementos = "";
        int nJoin = 1;
        string sJoins = "";
        string sWhere = "WHERE ";
        //string sqlTableTemp = ";CREATE TABLE " + sTablaTemporal + " (t001_idficepi int not null, " + sCampoDenominacion + " varchar(200) not null) ";
        string sqlTableTemp = ";CREATE TABLE " + sTablaTemporal + " (t001_idficepi int not null, " + sCampoDenominacion + " varchar(max) not null) ";
        sqlTableTemp += ";INSERT INTO " + sTablaTemporal + " SELECT t001_idficepi, " + sCampoDenominacion + " ";

        string sqlSelect = "SELECT distinct TMP.t001_idficepi FROM " + sTablaTemporal + " TMP ";
        string sTermino = "";

        for (var i = 1; i < aCadena.Length; i++)
        {
            if (aCadena.Length - 1 > i + 4 && string.Join("", aCadena.Skip(i).Take(5).ToArray()).ToUpper() == " AND ")
            {
                sWhere += " and ";
                i = i + 4;
            }
            else if (aCadena.Length - 1 > i + 3 && string.Join("", aCadena.Skip(i).Take(4).ToArray()).ToUpper() == " OR ")
            {
                sWhere += " or ";
                i = i + 3;
            }
            else if (aCadena[i] == " " || aCadena[i] == "")
            {
                continue;
            }
            else if (bEsParentesis(aCadena[i]))
            {
                sWhere += aCadena[i];
            }
            else
            {
                sJoins += "LEFT JOIN " + sTablaTemporal + " TMP" + nJoin.ToString() + @" on TMP.t001_idficepi = TMP" + nJoin.ToString() + @".t001_idficepi and TMP" + nJoin.ToString() + @"." + sCampoDenominacion + " collate SQL_Latin1_General_CP1_CI_AI like ";
                string sElemento_aux = "";
                sTermino = "";
                bool bComillaDoble = (aCadena[i] == "\"");
                if (bComillaDoble)
                {
                    sTermino += "'";
                    i++;
                }
                else
                {
                    sTermino += "'%";
                }

                while ((bComillaDoble && aCadena[i] != "\"") || (!bComillaDoble && aCadena.Length > i && aCadena[i] != " " && aCadena[i] != ")"))
                {
                    if (aCadena[i] == "?")
                    {
                        sTermino += "_";
                        sElemento_aux += "_";
                    }
                    else if (aCadena[i] == "*")
                    {
                        sTermino += "%";
                        sElemento_aux += "%";
                    }
                    else
                    {
                        sTermino += aCadena[i];
                        sElemento_aux += aCadena[i];
                    }
                    i++;
                }

                if (bComillaDoble)
                {
                    sTermino += "'";
                }
                else
                {
                    i--;
                    sTermino += "%'";
                }

                sElementos += sElemento_aux + ",";
                sJoins += sTermino + " ";
                sWhere += @"TMP" + nJoin.ToString() + @".t001_idficepi is not null ";
                nJoin++;
            }
        }

        sqlTableTemp += @" FROM dbo." + sFuncionSQL + "(" + HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString() + @",'" + sElementos + @"') FUN ";
        sqlSelect += sJoins + sWhere;

        return sqlTableTemp + "{separador}" + sqlSelect;
    }

    private static bool bEsParentesis(string car)
    {
        if (car == "(" || car == ")")
            return true;
        else
            return false;
    }
    private static bool caracterEspecial(string car)
    {
        if (car == "(" || car == ")")
            return true;
        else
            return false;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getProfesionalesConsAvanzada(
                string tipoConsulta,
                //Datos personales - Organizativos
                string tipo,
                string estado,
                int? CR,
                int? SN1,
                int? SN2,
                int? SN3,
                int? SN4,
                short? centro,
                short? movilidad,
                bool? inttrayint,
                byte? gradodisp,
                decimal? limcoste,
                ArrayList profesionales,
                ArrayList perfiles,
                //Titulación
                byte? tipologia,
                bool? tics,
                byte? modalidad,
                ArrayList titulo_obl_cod,
                ArrayList titulo_obl_den,
                ArrayList titulo_opc_cod,
                ArrayList titulo_opc_den,
                //Idiomas
                ArrayList idioma_obl_cod,
                ArrayList idioma_obl_den,
                ArrayList idioma_opc_cod,
                ArrayList idioma_opc_den,
                //Formación
                int? num_horas,
                int? anno,
                ////Certificados
                ArrayList cert_obl_cod,
                ArrayList cert_obl_den,
                ArrayList cert_opc_cod,
                ArrayList cert_opc_den,
                ////Entidades certificadoras
                ArrayList entcert_obl_cod,
                ArrayList entcert_obl_den,
                ArrayList entcert_opc_cod,
                ArrayList entcert_opc_den,
                ////Entornos tecnológicos
                ArrayList entfor_obl_cod,
                ArrayList entfor_obl_den,
                ArrayList entfor_opc_cod,
                ArrayList entfor_opc_den,
                ////Cursos
                ArrayList curso_obl_cod,
                ArrayList curso_obl_den,
                ArrayList curso_opc_cod,
                ArrayList curso_opc_den,
                ////Experiencias profesionales
                //Cliente / Sector
                string cliente,
                int? sector,
                short? cantidad_expprof,
                byte? unidad_expprof,
                short? anno_expprof,
                //Contenido de Experiencias / Funciones
                ArrayList term_expfun,
                string op_logico,
                short? cantidad_expfun,
                byte? unidad_expfun,
                short? anno_expfun,
                //Experiencia profesional Perfil
                //string op_logico_perfil,
                ArrayList tbl_bus_perfil,
                //Experiencia profesional Perfil / Entorno tecnológico
                ArrayList tbl_bus_perfil_entorno,
                //Experiencia profesional Entorno tecnológico
                //string op_logico_entorno,
                ArrayList tbl_bus_entorno,
                //Experiencia profesional Entorno tecnológico / Perfil
                ArrayList tbl_bus_entorno_perfil)
    {
        DataTable dt = Curriculum.getProfesionalesConsultaAvanzada(
                (int)HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"], 
                //Datos personales - Organizativos
                tipo, estado, CR, SN1, SN2, SN3, SN4, centro, movilidad, inttrayint, gradodisp, limcoste, profesionales, perfiles,
                //Titulación
                tipologia, tics, modalidad, titulo_obl_cod, titulo_obl_den, titulo_opc_cod, titulo_opc_den,
                //Idiomas
                idioma_obl_cod, idioma_obl_den, idioma_opc_cod, idioma_opc_den,
                //Formación
                num_horas, anno,
                ////Certificados
                cert_obl_cod, cert_obl_den, cert_opc_cod, cert_opc_den,
                ////Entidades certificadoras
                entcert_obl_cod, entcert_obl_den, entcert_opc_cod, entcert_opc_den,
                ////Entornos tecnológicos
                entfor_obl_cod, entfor_obl_den, entfor_opc_cod, entfor_opc_den,
                ////Cursos
                curso_obl_cod, curso_obl_den, curso_opc_cod, curso_opc_den,
                ////Experiencias profesionales
                //Cliente / Sector
                cliente, sector, cantidad_expprof, unidad_expprof, anno_expprof,
                //Contenido de Experiencias / Funciones
                term_expfun, op_logico, cantidad_expfun, unidad_expfun, anno_expfun,
                //Experiencia profesional Perfil
                //op_logico_perfil,
                tbl_bus_perfil,
                //Experiencia profesional Perfil / Entorno tecnológico
                tbl_bus_perfil_entorno,
                //Experiencia profesional Entorno tecnológico
                //op_logico_entorno,
                tbl_bus_entorno,
                //Experiencia profesional Entorno tecnológico / Perfil
                tbl_bus_entorno_perfil);

        StringBuilder sb = new StringBuilder();
        sb.Append(@"<table id='tblDatos' style='width:570px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                            <col style='width:30px;' />
                            <col style='width:20px;' />
                            <col style='width:280px;' />
                            <col style='width:200px;' />
                            <col style='width:20px;' />
                            <col style='width:20px;' />
                        </colgroup>");

        foreach (DataRow oFila in dt.Rows)
        {
            sb.Append("<tr id='" + oFila["T001_IDFICEPI"].ToString() + "' selected='false' style='height:20px;'");
            sb.Append("tipo='" + oFila["T001_TIPORECURSO"].ToString() + "' ");
            sb.Append("sexo='" + oFila["T001_SEXO"].ToString() + "' ");
            sb.Append("baja='" + oFila["BAJA"].ToString() + "' ");
            sb.Append("pdte=\"" + (((bool)oFila["Pendiente"])?"1":"0") + "\" ");

            sb.Append("nodo=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
            sb.Append("perfil=\"" + Utilidades.escape(oFila["Perfil"].ToString()) + "\" ");
            sb.Append("disponible=\"" + oFila["disponible"].ToString() + "\" ");
            sb.Append("provincia=\"" + Utilidades.escape(oFila["provincia"].ToString()) + "\" ");
            sb.Append("pais=\"" + Utilidades.escape(oFila["pais"].ToString()) + "\" ");

            string sTooltip = "<label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style=width:70px;>Perfil:</label>" + oFila["Perfil"].ToString() + "<br><label style=width:70px;>Disponibilidad:</label>" + oFila["disponible"].ToString() + " %" + "<br><label style=width:70px;>Provincia:</label>" + oFila["provincia"].ToString() + "<br><label style=width:70px;>País:</label>" + oFila["pais"].ToString();
            sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");
            //Datos de alertas 
            sb.Append(">");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td>" + oFila["Profesional"].ToString() + "</td>");
            //20/11/2015 Mikel PPOO nos pide que no se muestre el perfil
            //sb.Append("<td>" + oFila["Perfil"].ToString() + "</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        dt.Dispose();

        //Se inserta en el log si se trata de una consulta básica (B -> 1) o avanzada (A -> 2)
        if (tipoConsulta == "B")
            SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 1);
        else
            SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 2);

        return sb.ToString();
    }


}
