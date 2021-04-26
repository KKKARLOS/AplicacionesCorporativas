using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SUPER.Capa_Negocio;
using System.ServiceModel;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.ComponentModel;
using System.Threading;
using System.Text.RegularExpressions;
//para generar archivos zip
using Ionic.Zip;
//para log
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//Para objetos SQL
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//using SUPER.DAL;

public partial class Capa_Presentacion_CVT_MiCV_formaExport_exportarCV : System.Web.UI.Page
{
    public string sErrores = "";
    private Dictionary<string, string> htCampos = new Dictionary<string, string>();
    private string sFiltros = "";
    private string strTipoFormato = "";
    private string nPlantilla = "", nPlantillaIberDok="3796";
    private string nDocs = "";
    private string strNombreProfesionales = "", strNombreFA = "", strNombreCurso = "", strNombreCert = "";
    private string strDestinatarios, strDestinatariosIdFicepi = "";
    private string trackingId = "";
    private string nombreDoc = "";
    private string pathDirectory = ConfigurationManager.AppSettings["pathGuardarCVT"].ToString();
    private string currentPath = HttpContext.Current.Request.Path;
    private string ipAdress = HttpContext.Current.Request.UserHostAddress;
    //private string usuario = HttpContext.Current.Session["APELLIDO1"].ToString() + " " + HttpContext.Current.Session["APELLIDO2"].ToString() + ", " + HttpContext.Current.Session["NOMBRE"].ToString();
    //private string codred = HttpContext.Current.Session["IDRED"].ToString();
    private string usuario = "";
    private string codred = "";
    private string idFicepiPeticionario = "";
    private string prefijo = "";
    //Exportación de documentos
    private string gslIdsFicepi = "";
    private string gslIdsFA = "";//formacion academica
    private string gslIdsCurso = "";//cursos
    private string gslIdsCertificado = "";//certificados
    private string gslDensTitIdioma = "";//denominaciones de titulos de idioma
    //Puede que por restringir los documentos a sacar, los profesionales que se generan no sean los mismos que los solicitados
    //En caso de que sean diferentes en el correo de aviso de lo que se va a enviar a PaqExpress deben figurar ambas listas
    //y la explicación de porqué son diferentes
    private ArrayList aProfSelec = new ArrayList();//Lista de profesionales seleccionados
    private ArrayList aProfEnviar = new ArrayList();//Lista de profesionales que se van a enviar por PaqExpress
    //Para ver qué tipos de documentos hay que exportar
    private bool bExportarFA = false, bExportarCurso = false, bExportarCert = false, bExportarTitidioma = false;
    string sTipoBusqueda = "Basica";
    string sCriteriosBusqueda = "", sCamposExportar="";


    protected void Page_Load(object sender, EventArgs e)
    {
        svcSUPERIBOffice.IsvcSUPERClient osvcCVT = null;
        prefijo = Constantes.sPrefijo;
        htCampos = new Dictionary<string, string>();
        usuario = HttpContext.Current.Session["APELLIDO1"].ToString() + " " + HttpContext.Current.Session["APELLIDO2"].ToString() + ", " + HttpContext.Current.Session["NOMBRE"].ToString();
        codred = HttpContext.Current.Session["IDRED_ENTRADA"].ToString();
        //Si me he reconectado y exporto CV´s prefiero que el correo me llegue a mi y no al usuario al que me he reconectado
        idFicepiPeticionario = HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString();
        ILog miLog = LogManager.GetLogger("SUP");
        log4net.Config.XmlConfigurator.Configure();

        try
        {
            #region Iniciar
            //if (Session["IDRED"] == null)
            //{
            //    try
            //    {
            //        Response.Redirect("~/SesionCaducadaModal.aspx", true);
            //    }
            //    catch (System.Threading.ThreadAbortException) { return; }
            //}
            #endregion
            miLog.Debug(codred + "-> exportarCV -> Inicio");
            //SUPER.DAL.Log.Insertar("exportarCV -> Inicio");
            osvcCVT = new svcSUPERIBOffice.IsvcSUPERClient();
            ArrayList aParametros = new ArrayList();
            aParametros.Add(new ParametrosWS("IdFicepi", (Request.Form[prefijo + "hdnIdFicepis"])));
            strNombreProfesionales = Request.Form[prefijo + "hdnNombreProfesionales"];
            trackingId = Request.Form[prefijo + "hdnTrackingId"];

            if (Request.QueryString["sTipoBusqueda"] != null)
                sTipoBusqueda = Request.QueryString["sTipoBusqueda"].ToString();

            //Cargo en una lista la relación de nombres de profesionales original
            string[] aProf = Regex.Split(strNombreProfesionales, "/");
            aProfSelec.AddRange(aProf);
            
            if (Request.QueryString["docs"] != null)
            {
                #region Exportar documentos
                miLog.Debug(codred + "->" + trackingId + " exportarCV -> exportación de documentos");
                gslIdsFicepi = Request.Form[prefijo + "hdnIdFicepis"];
                //gslIdsFicepi = Request.Form[prefijo + "hdnIdFicepisTotal"];
                strDestinatarios = Session["IDRED_ENTRADA"].ToString();
                strDestinatariosIdFicepi = Session["IDFICEPI_ENTRADA"].ToString();

                gslIdsFA = Request.Form[prefijo + "hdnListaFAExport"];
                gslIdsCurso = Request.Form[prefijo + "hdnListaCursosExport"];
                gslIdsCertificado = Request.Form[prefijo + "hdnListaCertificadosExport"];
                gslDensTitIdioma = Request.Form[prefijo + "hdnListaIdiomasExport"];

                strNombreFA = Request.Form[prefijo + "hdnNombreFA"];
                strNombreCurso = Request.Form[prefijo + "hdnNombreCurso"];
                strNombreCert = Request.Form[prefijo + "hdnNombreCert"];

                //Compruebo qué tipos de documentos hay que exportar
                
                if (Request.Form[prefijo + "hdnExportarFA"] == "S") bExportarFA = true;
                if (Request.Form[prefijo + "hdnExportarCurso"] == "S") bExportarCurso = true;
                if (Request.Form[prefijo + "hdnExportarCert"] == "S") bExportarCert = true;
                if (Request.Form[prefijo + "hdnExportarTitIdioma"] == "S") bExportarTitidioma = true;
                
                //En caso de tener que exportar, compruebo si son todos o una selección
                bool bTodosFA = true;
                if (gslIdsFA != "") bTodosFA = false;
                bool bTodosCurso = true;
                if (gslIdsCurso != "") bTodosCurso = false;
                bool bTodosCert = true;
                if (gslIdsCertificado != "") bTodosCert = false;
                bool bTodosTiIdioma = true;
                if (gslDensTitIdioma != "") bTodosTiIdioma = false;

                enviarCorreoConfPedidoDoc(1, bExportarFA , bExportarCurso, bExportarCert, bExportarTitidioma, bTodosFA, bTodosCurso, bTodosCert, bTodosTiIdioma);

                ThreadStart ts = new ThreadStart(GenerarCorreoDocumentos);
                Thread workerThread = new Thread(ts);
                workerThread.Start();

                //Se inserta en la tabla de log sólo si viene de la pantalla de consultas (8 -> Exportar docs)
                SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 8);

                #endregion
            }
            else
            {
                #region Exportar CVs
                miLog.Debug(codred + "->" + trackingId + " exportarCV -> exportacion de CVs");
                sCamposExportar = Request.Form[prefijo + "hdnCamposWord"];
                //if (Request.Form[prefijo + "chkRestringir"] == "on")
                //{
                //    if (sTipoBusqueda == "Basica")
                //    {
                //        //Estos parámetros son de la consulta simple.
                //        #region agregar parámetros
                //        aParametros.Add(new ParametrosWS("TieneFiltros", ((Request.Form[prefijo + "chkRestringir"] == "on") ? "1" : "0")));
                //        aParametros.Add(new ParametrosWS("DesTitulacion", (Request.Form[prefijo + "hdnTitulo"])));
                //        aParametros.Add(new ParametrosWS("IdTitulacion", (Request.Form[prefijo + "hdnIdTitulo"])));
                //        aParametros.Add(new ParametrosWS("TipoTitulacion", (Request.Form[prefijo + "cboTipologia"])));
                //        aParametros.Add(new ParametrosWS("Tic", (Request.Form[prefijo + "cboTics"])));
                //        aParametros.Add(new ParametrosWS("Modalidad", (Request.Form[prefijo + "cboModalidad"])));
                //        aParametros.Add(new ParametrosWS("IdCertificado", (Request.Form[prefijo + "hdnIdCertificado"])));
                //        aParametros.Add(new ParametrosWS("DesCertificado", (Request.Form[prefijo + "hdnCertificacion"])));
                //        aParametros.Add(new ParametrosWS("EntornoFormacion", (Request.Form[prefijo + "hdnIdEntornoFormacion"])));
                //        aParametros.Add(new ParametrosWS("DesCliente", (Request.Form[prefijo + "hdnCuenta"])));
                //        aParametros.Add(new ParametrosWS("IdCuenta", (Request.Form[prefijo + "hdnIdCliente"])));
                //        aParametros.Add(new ParametrosWS("IdSectorCliente", (Request.Form[prefijo + "cboSector"])));
                //        aParametros.Add(new ParametrosWS("CodPerfil", (Request.Form[prefijo + "cboPerfilExp"])));
                //        aParametros.Add(new ParametrosWS("EntornoExperiencia", (Request.Form[prefijo + "hdnIdEntornoExp"])));
                //        aParametros.Add(new ParametrosWS("IdIdioma", (Request.Form[prefijo + "cboIdioma"])));
                //        aParametros.Add(new ParametrosWS("NivelIdioma", (Request.Form[prefijo + "cboNivel"])));
                //        aParametros.Add(new ParametrosWS("SoloIdiomaTitulo", ((Request.Form[prefijo + "chkTituloAcre"] == "on") ? "1" : "0")));
                //        aParametros.Add(new ParametrosWS("EPIPnummes", ((Request.Form[prefijo + "chkEXPIBENmes"] == "on") ? "1" : "0")));
                //        aParametros.Add(new ParametrosWS("EPFPnummes", ((Request.Form[prefijo + "chkEXPFUENmes"] == "on") ? "1" : "0")));
                //        #endregion
                //    }
                // }

                //sFiltros = SerializeArrayList(aParametros);

                //if (sTipoBusqueda == "Avanzada")
                //{
                //    sCriteriosBusquedaAvanzada = Request.Form[prefijo + "hdnCriterios"];
                //}
                sCriteriosBusqueda = Request.Form[prefijo + "hdnCriterios"];
                //campos a excluir
                if (Request.QueryString["peticion"].ToString() == "Consultas")
                {
                    #region Campos a exportar OLD
                    /*
                    if (Request.Form[prefijo + "hdnPlantilla"] == "1") //Si se trata de la plantilla "CV Completo"
                    {
                        #region Plantilla
                        //datos personales
                        htCampos.Add("nif", ((Request.Form[prefijo + "chkNIF"] == "on") ? "1" : "0"));
                        htCampos.Add("perfil", ((Request.Form[prefijo + "chkPerfil"] == "on") ? "1" : "0"));
                        htCampos.Add("fnacim", ((Request.Form[prefijo + "chkFNacimiento"] == "on") ? "1" : "0"));
                        htCampos.Add("nacionalidad", ((Request.Form[prefijo + "chkNacionalidad"] == "on") ? "1" : "0"));
                        htCampos.Add("sexo", ((Request.Form[prefijo + "chkSexo"] == "on") ? "1" : "0"));
                        htCampos.Add("empresa", ((Request.Form[prefijo + "chkEmpresa"] == "on") ? "1" : "0"));
                        htCampos.Add("sn2", ((Request.Form[prefijo + "chkUnidNegocio"] == "on") ? "1" : "0"));
                        htCampos.Add("nodo", ((Request.Form[prefijo + "chkCR"] == "on") ? "1" : "0"));
                        htCampos.Add("fantiguedad", ((Request.Form[prefijo + "chkAntiguedad"] == "on") ? "1" : "0"));
                        htCampos.Add("rol", ((Request.Form[prefijo + "chkRol"] == "on") ? "1" : "0"));
                        htCampos.Add("oficina", ((Request.Form[prefijo + "chkOficina"] == "on") ? "1" : "0"));
                        htCampos.Add("provincia", ((Request.Form[prefijo + "chkProvincia"] == "on") ? "1" : "0"));
                        htCampos.Add("pais", ((Request.Form[prefijo + "chkPais"] == "on") ? "1" : "0"));
                        htCampos.Add("trayinter", ((Request.Form[prefijo + "chkTrayectoria"] == "on") ? "1" : "0"));
                        htCampos.Add("dispmovilidad", ((Request.Form[prefijo + "chkMovilidad"] == "on") ? "1" : "0"));
                        htCampos.Add("observa", ((Request.Form[prefijo + "chkObservacion"] == "on") ? "1" : "0"));
                        htCampos.Add("foto", ((Request.Form[prefijo + "chkFoto"] == "on") ? "1" : "0"));

                        //sinopsis
                        htCampos.Add("sinopsis", ((Request.Form[prefijo + "chkSinopsis"] == "on") ? "1" : "0"));

                        //formacion academica
                        htCampos.Add("FA", ((Request.Form[prefijo + "chkFORACA"] == "on") ? "1" : "0"));
                        htCampos.Add("FAmodalidad", ((Request.Form[prefijo + "chkModalidad"] == "on") ? "1" : "0"));
                        htCampos.Add("FAespecialidad", ((Request.Form[prefijo + "chkEspecialidad"] == "on") ? "1" : "0"));
                        htCampos.Add("FAtipo", ((Request.Form[prefijo + "chkTipo"] == "on") ? "1" : "0"));
                        htCampos.Add("FAtic", ((Request.Form[prefijo + "chkTic"] == "on") ? "1" : "0"));
                        htCampos.Add("FAcentro", ((Request.Form[prefijo + "chkCentroFORACA"] == "on") ? "1" : "0"));
                        htCampos.Add("FAfinicio", ((Request.Form[prefijo + "chkFInicio"] == "on") ? "1" : "0"));
                        htCampos.Add("FAffin", ((Request.Form[prefijo + "chkFFin"] == "on") ? "1" : "0"));



                        //certificaciones
                        htCampos.Add("CERT", ((Request.Form[prefijo + "chkCERT"] == "on") ? "1" : "0"));
                        htCampos.Add("CTentidad", ((Request.Form[prefijo + "chkCertProv"] == "on") ? "1" : "0"));
                        htCampos.Add("CTentorno", ((Request.Form[prefijo + "chkCertEntTec"] == "on") ? "1" : "0"));
                        htCampos.Add("CTfobtencion", ((Request.Form[prefijo + "chkCertFObten"] == "on") ? "1" : "0"));
                        htCampos.Add("CTfcaducidad", ((Request.Form[prefijo + "chkCertFCadu"] == "on") ? "1" : "0"));


                        //formacion complementaria
                        htCampos.Add("FC", ((Request.Form[prefijo + "chkFORM"] == "on") ? "1" : "0"));
                        //Cursos recibidos
                        htCampos.Add("CR", ((Request.Form[prefijo + "chkCURREC"] == "on") ? "1" : "0"));
                        htCampos.Add("CRtipo", ((Request.Form[prefijo + "chkTipoCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRhoras", ((Request.Form[prefijo + "chkHorasCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRfinicio", ((Request.Form[prefijo + "chkFIniCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRffin", ((Request.Form[prefijo + "chkFFinCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRcentro", ((Request.Form[prefijo + "chkProvedCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRentorno", ((Request.Form[prefijo + "chkEntTecCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRcontenido", ((Request.Form[prefijo + "chkConteCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRprovincia", ((Request.Form[prefijo + "chkProvCur"] == "on") ? "1" : "0"));
                        htCampos.Add("CRmodalidad", ((Request.Form[prefijo + "chkModalCur"] == "on") ? "1" : "0"));

                        //Cursos impartidos

                        htCampos.Add("CI", ((Request.Form[prefijo + "chkCURIMP"] == "on") ? "1" : "0"));
                        htCampos.Add("CItipo", ((Request.Form[prefijo + "chkTipoCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CIhoras", ((Request.Form[prefijo + "chkHorasCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CIfinicio", ((Request.Form[prefijo + "chkFIniCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CIffin", ((Request.Form[prefijo + "chkFFinCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CIcentro", ((Request.Form[prefijo + "chkProvedCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CIentorno", ((Request.Form[prefijo + "chkEntTecCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CIcontenido", ((Request.Form[prefijo + "chkConteCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CIprovincia", ((Request.Form[prefijo + "chkProvCurImp"] == "on") ? "1" : "0"));
                        htCampos.Add("CImodalidad", ((Request.Form[prefijo + "chkModalCurImp"] == "on") ? "1" : "0"));


                        //exámenes

                        htCampos.Add("EXAM", ((Request.Form[prefijo + "chkEXAM"] == "on") ? "1" : "0"));
                        htCampos.Add("EXentidad", ((Request.Form[prefijo + "chkExamProv"] == "on") ? "1" : "0"));
                        htCampos.Add("EXentorno", ((Request.Form[prefijo + "chkExamEntTec"] == "on") ? "1" : "0"));
                        htCampos.Add("EXfobtencion", ((Request.Form[prefijo + "chkExamFObten"] == "on") ? "1" : "0"));
                        htCampos.Add("EXfcaducidad", ((Request.Form[prefijo + "chkExamFCadu"] == "on") ? "1" : "0"));

                        //EXPERIENCIAS
                        htCampos.Add("EP", ((Request.Form[prefijo + "chkEXP"] == "on") ? "1" : "0"));
                        //en iber
                        htCampos.Add("EPI", ((Request.Form[prefijo + "chkEXPIBE"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIfinicio", ((Request.Form[prefijo + "chkEXPIBEFIni"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIffin", ((Request.Form[prefijo + "chkEXPIBEFFin"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIdescripcion", ((Request.Form[prefijo + "chkEXPIBEDescri"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIareacono", ((Request.Form[prefijo + "chkEXPIBEACSACT"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIcliente", ((Request.Form[prefijo + "chkEXPIBECli"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIsectorc", ((Request.Form[prefijo + "chkEXPIBESector"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIsegmentoc", ((Request.Form[prefijo + "chkEXPIBESegmen"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIempresa", "1");//no está el campo, dejamos preparado por si hay que incluir
                        htCampos.Add("EPIsectore", ((Request.Form[prefijo + "chkEXPIBESector"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIsegmentoe", ((Request.Form[prefijo + "chkEXPIBESegmen"] == "on") ? "1" : "0"));
                        //perfil en iber
                        htCampos.Add("EPIperfil", ((Request.Form[prefijo + "chkEXPIBEPerfil"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIareatec", ((Request.Form[prefijo + "chkEXPIBEEntor"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIfuncion", ((Request.Form[prefijo + "chkEXPIBEFunci"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIPfinicio", ((Request.Form[prefijo + "chkEXPIBEFIni"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIPffi", ((Request.Form[prefijo + "chkEXPIBEFFin"] == "on") ? "1" : "0"));
                        htCampos.Add("EPIPnummes", ((Request.Form[prefijo + "chkEXPIBENmes"] == "on") ? "1" : "0"));

                        //fuera de iber
                        htCampos.Add("EPF", ((Request.Form[prefijo + "chkEXPFUE"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFfinicio", ((Request.Form[prefijo + "chkEXPFUEFIni"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFffin", ((Request.Form[prefijo + "chkEXPFUEFFin"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFdescripcion", ((Request.Form[prefijo + "chkEXPFUEDescri"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFareacono", ((Request.Form[prefijo + "chkEXPFUEACSACT"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFcliente", ((Request.Form[prefijo + "chkEXPFUECli"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFsectorc", ((Request.Form[prefijo + "chkEXPFUESector"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFsegmentoc", ((Request.Form[prefijo + "chkEXPFUESegmen"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFempresa", ((Request.Form[prefijo + "chkEXPFUEEmpOri"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFsectore", ((Request.Form[prefijo + "chkEXPFUESector"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFsegmentoe", ((Request.Form[prefijo + "chkEXPFUESegmen"] == "on") ? "1" : "0"));
                        //perfil fuera
                        htCampos.Add("EPFperfil", ((Request.Form[prefijo + "chkEXPFUEPerfil"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFareatec", ((Request.Form[prefijo + "chkEXPFUEEntor"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFfuncion", ((Request.Form[prefijo + "chkEXPFUEFunci"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFPfinicio", ((Request.Form[prefijo + "chkEXPFUEFIni"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFPffi", ((Request.Form[prefijo + "chkEXPFUEFFin"] == "on") ? "1" : "0"));
                        htCampos.Add("EPFPnummes", ((Request.Form[prefijo + "chkEXPFUENmes"] == "on") ? "1" : "0"));

                        //Idiomas
                        htCampos.Add("ID", ((Request.Form[prefijo + "chkIdiomas"] == "on") ? "1" : "0"));
                        htCampos.Add("IDcentro", ((Request.Form[prefijo + "chkTitCentro"] == "on") ? "1" : "0"));
                        htCampos.Add("IDfecha", ((Request.Form[prefijo + "chkTitIdiomaObt"] == "on") ? "1" : "0"));
                        htCampos.Add("IDtitulo", ((Request.Form[prefijo + "chkTitIdioma"] == "on") ? "1" : "0"));
                        htCampos.Add("IDescrito", ((Request.Form[prefijo + "chkEscritura"] == "on") ? "1" : "0"));
                        htCampos.Add("IDoral", ((Request.Form[prefijo + "chkOral"] == "on") ? "1" : "0"));
                        htCampos.Add("IDcomprension", ((Request.Form[prefijo + "chkLectura"] == "on") ? "1" : "0"));
                        #endregion
                    }
                    else
                    {
                        #region Campos a mostrar
                        //sinopsis
                        htCampos.Add("sinopsis", ((Request.Form[prefijo + "chkSinopsis"] == "on") ? "1" : "0"));

                        //formacion academica
                        htCampos.Add("FA", ((Request.Form[prefijo + "chkFORACA"] == "on") ? "1" : "0"));

                        //certificaciones
                        htCampos.Add("CERT", ((Request.Form[prefijo + "chkCERT"] == "on") ? "1" : "0"));

                        //formacion complementaria
                        htCampos.Add("FC", ((Request.Form[prefijo + "chkFORM"] == "on") ? "1" : "0"));
                        //Cursos recibidos
                        htCampos.Add("CR", ((Request.Form[prefijo + "chkCURREC"] == "on") ? "1" : "0"));
                        //Cursos impartidos
                        htCampos.Add("CI", ((Request.Form[prefijo + "chkCURIMP"] == "on") ? "1" : "0"));

                        //exámenes
                        htCampos.Add("EXAM", ((Request.Form[prefijo + "chkEXAM"] == "on") ? "1" : "0"));

                        //EXPERIENCIAS
                        htCampos.Add("EP", ((Request.Form[prefijo + "chkEXP"] == "on") ? "1" : "0"));
                        //en iber
                        htCampos.Add("EPI", ((Request.Form[prefijo + "chkEXPIBE"] == "on") ? "1" : "0"));
                        //fuera de iber
                        htCampos.Add("EPF", ((Request.Form[prefijo + "chkEXPFUE"] == "on") ? "1" : "0"));

                        //Idiomas
                        htCampos.Add("ID", ((Request.Form[prefijo + "chkIdiomas"] == "on") ? "1" : "0"));
                        #endregion
                    }
                    */
                    #endregion
                    strDestinatarios = Request.Form[prefijo + "hdnDestinatarios"];
                    strDestinatariosIdFicepi = Request.Form[prefijo + "hdnDestinatarioIdFicepi"];
                    //trackingId = Request.Form[prefijo + "hdnTrackingId"];
                    nDocs = Request.Form[prefijo + "rdbDoc"];
                }
                else //MICV
                {
                    trackingId = Guid.NewGuid().ToString();
                    nDocs = "0";
                }
                string extension = "";
                strTipoFormato = Request.Form[prefijo + "rdbFormato"];
                //strNombreProfesionales = Request.Form[prefijo + "hdnNombreProfesionales"];
                switch (strTipoFormato)
                {
                    case "PDF":
                        extension = ".pdf";
                        break;
                    case "WORD":
                        extension = ".doc";
                        break;
                }
                nPlantilla = Request.Form[prefijo + "hdnPlantilla"];
                nombreDoc = trackingId;
                if (Regex.Split(Request.Form[prefijo + "hdnIdFicepis"], ",").Length == 1)
                    nombreDoc = strNombreProfesionales.Substring(0, strNombreProfesionales.Length - 1);

                //if (Request.Form[prefijo + "rdbTipoExp"] == null || Request.Form[prefijo + "rdbTipoExp"] == "0") //Para la pantalla de consultas exportación rdbTipo = 0, rdbTipo = null es la llamada desde MICV
                //Para la pantalla de consultas exportación rdbTipo = 0, rdbTipo = null es la llamada desde MICV
                if (Request.Form[prefijo + "hdnTipoExp"] == null || Request.Form[prefijo + "hdnTipoExp"] == "0")
                {
                    #region generación on-line
                    if (Request.QueryString["descargaToken"] != null)
                        Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field
                    Stream oStream = new MemoryStream();

                    //svcSUPERIBOffice.ArrayOfKeyValueOfstringstringKeyValueOfstringstring[] arrCampos =
                    //  htCampos.Select(pair =>
                    //     new svcSUPERIBOffice.ArrayOfKeyValueOfstringstringKeyValueOfstringstring()
                    //     {
                    //         Key = pair.Key,
                    //         Value = pair.Value
                    //     }).ToArray();
                    //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //Dictionary<string, string> htCamposAux = serializer.Deserialize<Dictionary<string, string>>(sCamposExportar);
                    
                    //if (sTipoBusqueda == "Basica")
                    //{
                    //    miLog.Debug("exportarCV -> Antes de osvcCVT.obtenerPlantilla");
                    //    //oStream = osvcCVT.obtenerPlantilla(sFiltros, extension, int.Parse(Request.Form[prefijo + "hdnPlantilla"]),
                    //    //                                   htCampos, //,arrCampos
                    //    //                                   byte.Parse(nDocs), trackingId);
                    //    oStream = osvcCVT.getPlantilla(sFiltros, extension, int.Parse(Request.Form[prefijo + "hdnPlantilla"]),
                    //                                       sCamposExportar, byte.Parse(nDocs), trackingId);
                    //    miLog.Debug("exportarCV -> Después de osvcCVT.obtenerPlantilla");
                    //}
                    //else if (sTipoBusqueda == "Avanzada")
                    //{
                    //    miLog.Debug("exportarCV -> Antes de osvcCVT.obtenerPlantilla_avanzada");
                    //    //oStream = osvcCVT.obtenerPlantilla_avanzada(sTipoBusqueda, sCriteriosBusquedaAvanzada, extension,
                    //    //                                            int.Parse(Request.Form[prefijo + "hdnPlantilla"]),
                    //    //                                            htCampos, //arrCampos
                    //    //                                            byte.Parse(nDocs), trackingId);
                    //    oStream = osvcCVT.getPlantilla(sCriteriosBusquedaAvanzada, extension,
                    //                                    int.Parse(Request.Form[prefijo + "hdnPlantilla"]),
                    //                                    sCamposExportar, byte.Parse(nDocs), trackingId);
                    //    miLog.Debug("exportarCV -> Después de osvcCVT.obtenerPlantilla_avanzada");
                    //}
                    miLog.Debug(codred + "->" + trackingId + " exportarCV -> Antes de osvcCVT.getPlantilla");
                    //SUPER.DAL.Log.Insertar("exportarCV -> Antes de osvcCVT.getPlantilla");
                    oStream = osvcCVT.getPlantilla(sCriteriosBusqueda, extension,
                                                    int.Parse(Request.Form[prefijo + "hdnPlantilla"]),
                                                    sCamposExportar, byte.Parse(nDocs), trackingId);
                    //Para pruebas sin pasar por los servicios
                    //oStream = kkgetPlantilla(sCriteriosBusqueda, extension,
                    //                                int.Parse(Request.Form[prefijo + "hdnPlantilla"]),
                    //                                sCamposExportar, byte.Parse(nDocs), trackingId);

                    //SUPER.DAL.Log.Insertar("exportarCV -> Después de osvcCVT.getPlantilla");
                    miLog.Debug(codred + "->" + trackingId + " exportarCV -> Después de osvcCVT.getPlantilla");
                    
                    if (nDocs == "1")
                        extension = ".zip";
                    Directory.CreateDirectory(pathDirectory + trackingId);
                    miLog.Debug(codred + "->" + trackingId + " exportarCV -> Creado el directorio " + pathDirectory + trackingId);
                    //SUPER.DAL.Log.Insertar(trackingId + " - exportarCV -> Creado el directorio " + pathDirectory + trackingId);
                    GrabarStream(pathDirectory + trackingId + @"\" + nombreDoc + extension, oStream);
                    miLog.Debug(codred + "->" + trackingId + " exportarCV -> Stream grabado");
                    byte[] result = File.ReadAllBytes(pathDirectory + trackingId + @"\" + nombreDoc + extension);
                    Response.ClearContent();
                    Response.Buffer = true;
                    if (nDocs == "1") strTipoFormato = "ZIP";
                    String nav = HttpContext.Current.Request.Browser.Browser.ToString();
                    if (nav.IndexOf("IE") != -1)
                    {
                        switch (strTipoFormato)
                        {
                            case "PDF":
                                Response.AddHeader("content-type", "application/pdf; charset=utf-8");
                                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Uri.EscapeDataString(nombreDoc) + ".pdf\"");
                                break;
                            case "WORD":
                                Response.AddHeader("content-type", "application/msword; charset=utf-8");
                                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Uri.EscapeDataString(nombreDoc) + ".doc\"");
                                break;
                            case "ZIP":
                                Response.AddHeader("content-type", "application/zip; charset=utf-8");
                                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + trackingId + ".zip\"");
                                break;
                        }
                    }
                    else
                    {
                        switch (strTipoFormato)
                        {
                            case "PDF":
                                Response.AddHeader("content-type", "application/pdf; charset=utf-8");
                                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreDoc + ".pdf\"");
                                break;
                            case "WORD":
                                Response.AddHeader("content-type", "application/msword;charset=utf-8");
                                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + nombreDoc + ".doc\"");
                                break;
                            case "ZIP":
                                Response.AddHeader("content-type", "application/zip;charset=utf-8");
                                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + trackingId + ".zip\"");
                                break;
                        }
                    }

                    Response.Clear();
                    Response.BinaryWrite(result);
                    if (Response.IsClientConnected)
                        Response.Flush();
                    miLog.Debug(codred + "->" + trackingId + " exportarCV -> Fin");


                    //Se inserta en la tabla de log si viene de consultas (5 -> Exportar Word/PDF), o Mi CV (9 -> Exportar Mi CV) o Mi CV distinto al usuario conectado (10 -> Exportación Mi CV (ajeno))
                    if (Request.QueryString["peticion"].ToString() == "Consultas")
                        SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 5);
                    else if (Request.Form[prefijo + "hdnIdFicepis"] == HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString())
                        SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 9);
                    else
                        SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 10);

                    #endregion
                }
                else if (Request.Form[prefijo + "hdnTipoExp"] == "2")//IBERDOK 
                {
                    //svcSUPERIBOffice.IsvcSUPERClient osvcSuper = new svcSUPERIBOffice.IsvcSUPERClient();
                    //string sPlant= Request.Form[prefijo + "hdnPlantilla"];
                    //string sXML = @"<Params><trackingid>" + trackingId + "</trackingid>" +
                    //              @"<codred>" + codred + @"</codred>" +
                    //              @"<plantilla>" + sPlant + @"</plantilla></Params>";
                    //string sIdPedido = osvcSuper.CrearDatosIberDok(sXML, sCriteriosBusqueda);
                    //Con el identificador de pedido llamaremos al servicio IBERDOK para que abra el navegador con los documentos
                    //strTipoFormato = Request.Form[prefijo + "rdbFormatoIB"];
                    if (Request.Form[prefijo + "rdbTipoExpFOIB"].ToString() == "1")
                        strTipoFormato = "PDF";
                    else
                        strTipoFormato = "HTML";
                    nDocs = Request.Form[prefijo + "rdbDocIB"];

                    miLog.Debug(codred + "->" + trackingId + " exportarCV -> Se va a llamar al hilo para IberDok");
                    enviarCorreoConfPedido();

                    nPlantillaIberDok = SUPER.BLL.IberDok.GetModelo(nPlantilla);
                    ThreadStart ts = new ThreadStart(HiloIberDok);
                    Thread workerThread = new Thread(ts);
                    workerThread.Start();

                    //Se inserta en la tabla de log sólo si viene de la pantalla de consultas (7 -> Iberdok)
                    if (Request.QueryString["peticion"].ToString() == "Consultas")
                        SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 7);

                }
                else //correo
                {
                    miLog.Debug(codred + "->" + trackingId + " exportarCV -> Inicio petición por correo");
                    //SUPER.DAL.Log.Insertar(trackingId + " - exportarCV -> Inicio petición por correo");
                    enviarCorreoConfPedido();
                    ThreadStart ts = new ThreadStart(GenerarCorreoCV);
                    Thread workerThread = new Thread(ts);
                    workerThread.Start();

                    //Se inserta en la tabla de log sólo si viene de la pantalla de consultas (5 -> Exportar Word/PDF)
                    if (Request.QueryString["peticion"].ToString() == "Consultas")
                        SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 5);

                }
                #endregion
            }
        }
        catch (FaultException<svcSUPERIBOffice.SUPERException> cex)
        {
            miLog.Debug(codred + "->" + trackingId + " exportarCV.Page_Load -> Error en llamada al servicio: " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            //SUPER.DAL.Log.Insertar(trackingId + " - exportarCV.Page_Load -> Error en llamada al servicio: " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            sErrores += Errores.mostrarError(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage, cex);
        }
        catch (Exception ex)
        {
            miLog.Debug(codred + "->" + trackingId + " exportarCV.Page_Load -> Error genérico: " + ex.Message);
            //SUPER.DAL.Log.Insertar(trackingId + " - exportarCV.Page_Load -> Error genérico: " + ex.Message);
            sErrores += Errores.mostrarError("Error al exportar", ex);
        }
        finally
        {
            #region finalización
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> Fin");
            if (osvcCVT != null && osvcCVT.State != System.ServiceModel.CommunicationState.Closed)
            {
                try
                {
                    if (osvcCVT.State != System.ServiceModel.CommunicationState.Faulted) osvcCVT.Close();
                    else if (osvcCVT.State != System.ServiceModel.CommunicationState.Closed) osvcCVT.Abort();
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al cerrar el canal de comunicación con el servicio", ex);
                }
            }
            //Las carpetas serán borradas por un proceso nocturno
            //if (Directory.Exists(pathDirectory + trackingId))
            //    Directory.Delete(pathDirectory + trackingId, true);
            #endregion
        }
    }
    private void HiloIberDok()
    {
        string sModelo = nPlantillaIberDok;// "3796";
        ILog miLog = LogManager.GetLogger("SUP");
        log4net.Config.XmlConfigurator.Configure();
        miLog.Debug(codred + "->" + trackingId + " exportarCV.HiloIberDok -> Inicio");
        //Saco la traducción del modelo SUPER al modelo IBERDOK fuera del hilo porque dentro de él no hay contexto Http
        //sModelo = SUPER.BLL.IberDok.GetModelo(nPlantilla);

        //strTipoFormato ->(PDF, IBERDOK) Indica si se tiene que generar el PDF o dejar los documentos en la bandeja de IBERDOK para su edición
        //nDocs -> (1, 0) Indica si tiene que generar un documento por profesional(1) o un documento con todos los profesionales(0)
        svcSUPERIBOffice.IsvcSUPERClient osvcSuper = new svcSUPERIBOffice.IsvcSUPERClient();
        try 
        {
            string sXML = @"<Params><trackingid>" + trackingId + "</trackingid>" +
                          @"<idficepipet>" + idFicepiPeticionario + @"</idficepipet>" +
                          @"<codred>" + codred + @"</codred>" +
                          @"<plantilla>" + nPlantilla + @"</plantilla>" +
                          @"<modelo>" + sModelo + @"</modelo>" +
                          @"<formato>" + strTipoFormato + @"</formato>" +
                          @"<ndocs>" + nDocs + @"</ndocs>" +
                          @"<campos>" + sCamposExportar + @"</campos>" +
                          @"</Params>";
            miLog.Debug("exportarCV.HiloIberDok -> Antes de llamar al servicio CrearDatosIberDok. XML = " + sXML);
            string sIdPedido = osvcSuper.CrearDatosIberDok(sXML, sCriteriosBusqueda);

            //Con el identificador de pedido llamaremos al servicio IBERDOK y nos olvidamos
            //Cuando IBERDOK haya preparado lo que tenga que preparar, hará una llamada a un servicio SUPER
            //que implementaremos nosotros que se encargará de recoger la respuesta de IBERDOK y enviar un correo al usuario
            //con el archivo adjunto (por PaqExpress si es grande) o el link a la bandeja de IBERDOK
            if (sIdPedido == "-1")
            {
                miLog.Error("La llamada osvcSuper.CrearDatosIberDok ha devuelto -1\n\nNo se han creado datos en la BBDD IBERDOK");
                enviarCorreoErrorEDA("La llamada osvcSuper.CrearDatosIberDok ha devuelto -1\n\nNo se han creado datos en la BBDD IBERDOK");
                enviarCorreoErrorUsuario();
            }
            else
            {
                miLog.Debug(codred + "->" + trackingId + " exportarCV.HiloIberDok -> Antes de llamar al servicio REST para lanzar la generación de documentos");
                //El servicio de IBERDOK es un servicio REST
                string sUsuario = "admin";
                string sRespuesta = (strTipoFormato == "PDF") ? "PDF" : "HTML";
                string sAgrupacion = (nDocs == "0") ? "C" : "I";
                //string sXML_IBERDOK = @"<Params>" +
                //              @"<idUsuario>" + sUsuario + @"</idUsuario>" +
                //              @"<uidPedido>" + sIdPedido + @"</uidPedido>" +
                //              @"<modelo>" + nPlantilla + @"</modelo>" +
                //              @"<tipo>" + strTipoFormato=="PDF" ? "PDF" : "HTML" + @"</tipo>" +
                //              @"<clase>" + nDocs=="0" ? "C" : "I" + @"</clase>" +
                //              @"</Params>";
                SUPER.BLL.IberDok.CrearPedido(sUsuario, sIdPedido, sModelo, sRespuesta, sAgrupacion);
            }
            miLog.Debug(codred + "->" + trackingId + " exportarCV.HiloIberDok -> Fin pedido");
        }
        catch (FaultException<svcSUPERIBOffice.SUPERException> cex)
        {
            sErrores += Errores.mostrarError(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            miLog.Debug(codred + "->" + trackingId + " exportarCV.HiloIberDok -> Error: " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            enviarCorreoErrorEDA(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage, cex);
            enviarCorreoErrorUsuario();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al exportar", ex);
            miLog.Debug(codred + "->" + trackingId + " exportarCV.HiloIberDok -> Error: " + ex.Message);
            //SUPER.DAL.Log.Insertar("exportarCV.GenerarCorreoCV -> Error: " + ex.Message);
            enviarCorreoErrorEDA("Error al exportar", ex);
            enviarCorreoErrorUsuario();
        }
        finally
        {
            miLog.Debug(codred + "->" + trackingId + " exportarCV.HiloIberDok -> Fin");
            if (osvcSuper != null && osvcSuper.State != System.ServiceModel.CommunicationState.Closed)
            {
                try
                {
                    if (osvcSuper.State != System.ServiceModel.CommunicationState.Faulted) osvcSuper.Close();
                    else if (osvcSuper.State != System.ServiceModel.CommunicationState.Closed) osvcSuper.Abort();
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al exportar a IberDok", ex);
                }
            }
        }
    }
    private void GenerarCorreoCV()
    {
        ILog miLog = LogManager.GetLogger("SUP");
        log4net.Config.XmlConfigurator.Configure();
        miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Inicio");

        svcSUPERIBOffice.IsvcSUPERClient osvcCVT = null;
        try
        {
            osvcCVT = new svcSUPERIBOffice.IsvcSUPERClient();
            string extension = "";
            switch (strTipoFormato)
            {
                case "PDF":
                    extension = ".pdf";
                    break;
                case "WORD":
                    extension = ".doc";
                    break;
            }
            Stream oStream = new MemoryStream();
            //svcSUPERIBOffice.ArrayOfKeyValueOfstringstringKeyValueOfstringstring[] arrCampos =
            //  htCampos.Select(pair =>
            //     new svcSUPERIBOffice.ArrayOfKeyValueOfstringstringKeyValueOfstringstring()
            //     {
            //         Key = pair.Key,
            //         Value = pair.Value
            //     }).ToArray();
            
            //oStream = osvcCVT.obtenerPlantilla(sFiltros, extension, int.Parse(nPlantilla), htCampos, byte.Parse(nDocs), trackingId);
            //if (sTipoBusqueda == "Basica")
            //{
            //    miLog.Debug("exportarCV.GenerarCorreoCV -> Antes de osvcCVT.obtenerPlantilla");
            //    //oStream = osvcCVT.obtenerPlantilla(sFiltros, extension, int.Parse(nPlantilla),
            //    //                                    htCampos,//,arrCampos
            //    //                                    byte.Parse(nDocs), trackingId);
            //    oStream = osvcCVT.getPlantilla(sFiltros, extension, int.Parse(nPlantilla),
            //                                        sCamposExportar, byte.Parse(nDocs), trackingId);
            //    miLog.Debug("exportarCV.GenerarCorreoCV -> Después de osvcCVT.obtenerPlantilla");
            //}
            //else if (sTipoBusqueda == "Avanzada")
            //{
            //    miLog.Debug("exportarCV.GenerarCorreoCV -> Antes de osvcCVT.obtenerPlantilla_avanzada");
            //    //oStream = osvcCVT.obtenerPlantilla_avanzada(sTipoBusqueda, sCriteriosBusquedaAvanzada, extension,
            //    //                                            int.Parse(nPlantilla),
            //    //                                           htCampos,//,arrCampos
            //    //                                            byte.Parse(nDocs), trackingId);
            //    oStream = osvcCVT.getPlantilla(sCriteriosBusqueda, extension, int.Parse(nPlantilla),
            //                                    sCamposExportar, byte.Parse(nDocs), trackingId);
            //    miLog.Debug("exportarCV.GenerarCorreoCV -> Después de osvcCVT.obtenerPlantilla_avanzada");
            //}
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Antes de osvcCVT.getPlantilla");
            //SUPER.DAL.Log.Insertar("exportarCV.GenerarCorreoCV -> Antes de osvcCVT.getPlantilla");
            oStream = osvcCVT.getPlantilla(sCriteriosBusqueda, extension, int.Parse(nPlantilla),
                                            sCamposExportar, byte.Parse(nDocs), trackingId);
            //SUPER.DAL.Log.Insertar("exportarCV.GenerarCorreoCV -> Después de osvcCVT.getPlantilla");
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Después de osvcCVT.getPlantilla");

            if (nDocs == "1")
                extension = ".zip";
            Directory.CreateDirectory(pathDirectory + trackingId);
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Creado el directorio " + pathDirectory + trackingId);
            //SUPER.DAL.Log.Insertar("exportarCV.GenerarCorreoCV -> Creado el directorio " + pathDirectory + trackingId);
            GrabarStream(pathDirectory + trackingId + @"\" + nombreDoc + extension, oStream);
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Stream grabado");
            if ((File.OpenRead(pathDirectory + trackingId + @"\" + nombreDoc + extension).Length / 1048576) > 10) //miramos si el archivo es mayor que 10MB
                enviarPackExpress(extension);
            else
                enviarCorreo(extension);
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Fin");
            //SUPER.DAL.Log.Insertar("exportarCV.GenerarCorreoCV ->  Fin");
        }
        catch (FaultException<svcSUPERIBOffice.SUPERException> cex)
        {
            sErrores += Errores.mostrarError(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Error: " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            //SUPER.DAL.Log.Insertar("exportarCV.GenerarCorreoCV -> Error: " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            enviarCorreoErrorEDA(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage, cex);
            enviarCorreoErrorUsuario();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al exportar", ex);
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Error: " + ex.Message);
            //SUPER.DAL.Log.Insertar("exportarCV.GenerarCorreoCV -> Error: " + ex.Message);
            enviarCorreoErrorEDA("Error al exportar", ex);
            enviarCorreoErrorUsuario();
        }
        finally
        {
            miLog.Debug(codred + "->" + trackingId + " exportarCV.GenerarCorreoCV -> Fin");
            if (osvcCVT != null && osvcCVT.State != System.ServiceModel.CommunicationState.Closed)
            {
                try
                {
                    if (osvcCVT.State != System.ServiceModel.CommunicationState.Faulted) osvcCVT.Close();
                    else if (osvcCVT.State != System.ServiceModel.CommunicationState.Closed) osvcCVT.Abort();
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al exportar", ex);
                }
            }
        }
    }

    private void enviarPackExpress(string extension)
    {
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
        string sTexto = "";
        svcSendPack.SendPackClient oPaq = null;

        try
        {
            string strMensaje = "";
            string strCabecera = "";
            string strDatos = "";
            string strAsunto = "";
            string strTO = "";
            string[] aNombreProfesionales = Regex.Split(strNombreProfesionales, @"/");

            strAsunto = "Pedido CVT (Localizador: " + trackingId + ")";
            strMensaje = "El pedido que has realizado para obtener los curriculums de los profesionales que se muestran al final de este correo, ha generado un documento que excede el tamaño máximo para ser enviado por correo (10Mb), por lo que se te enviará en breve a través de PAQEXPRESS.<br><br>";
            strMensaje += "Lozalizador asignado: " + trackingId;

            strDatos = "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            //for (int i = aNombreProfesionales.Length - 1; i >= 0; i--)
            for (int i = 0; i < aNombreProfesionales.Length; i++)
            {
                if (aNombreProfesionales[i] == "") continue;
                strDatos += "<tr>";
                strDatos += "<td width='100%'>" + aNombreProfesionales[i].ToString() + "</td>";
                strDatos += "</tr>";
            }
            strDatos += "</table>";

            ArrayList aListCorreo = new ArrayList();

            string[] aDestinatarios = Regex.Split(strDestinatarios, @",");

            for (int i = 0; i < aDestinatarios.Length; i++)
            {
                try
                {
                    strMensaje += strCabecera + strDatos;
                    strTO = aDestinatarios[i].ToString();

                    string[] aMail = { strAsunto, strMensaje, strTO, "" };
                    aListCorreo.Add(aMail);
                }
                catch (System.Exception objError)
                {
                    enviarCorreoErrorEDA("Error al enviar el mail.", objError);
                    enviarCorreoErrorUsuario();

                }
            }

            if (aDestinatarios.Length != 0) SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);

            /********** Inicio de envío a PaqExpress **************/
            oPaq = new svcSendPack.SendPackClient();

            DateTime dtIni = DateTime.Now;
            sTexto = dtIni.ToString() + "\r\n";

            strb.Append("<Pack>");
            strb.Append("<User>PAQEXPRESS</User>");
            strb.Append("<Clave>XRJ001-WCF-SUPER-CV.</Clave>");
            strb.Append("<IdFicepi>" + strDestinatariosIdFicepi + "</IdFicepi>");
            strb.Append("<FPedido>" + DateTime.Now.ToString() + "</FPedido>");
            strb.Append("<Profesionales>" + strNombreProfesionales.Replace("/", ";") + "</Profesionales>");
            strb.Append("<Ref>" + trackingId + "</Ref>");//Nº de referencia para tracking
            strb.Append("</Pack>");

            oPaq.CrearPaqueteCV(new FileInfo(nombreDoc + extension).Name, strb.ToString(), File.OpenRead(pathDirectory + trackingId + @"\" + nombreDoc + extension));
            /********** Fin de envío a PaqExpress **************/
        }
        catch (FaultException<svcSendPack.PackException> cex)
        {
            string sError = "Error: Código:" + cex.Detail.ErrorCode + ". Descripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;

            if (cex.Detail.InnerMessage != "")
                sError += "\r\nInnerMessage: " + cex.Detail.InnerMessage;
            sTexto += sError + "\r\n";
            enviarCorreoErrorEDA(sTexto, cex);
            enviarCorreoErrorUsuario();
        }
        catch (Exception ex)
        {
            enviarCorreoErrorEDA(ex.Message + "\r\n", ex);
            enviarCorreoErrorUsuario();
        }
        finally
        {
            //Cierre del canal
            if (oPaq != null && oPaq.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (oPaq.State != System.ServiceModel.CommunicationState.Faulted) oPaq.Close();
                else if (oPaq.State != System.ServiceModel.CommunicationState.Closed) oPaq.Abort();
            }
        }
    }

    private void enviarCorreoConfPedido()
    {
        string strMensaje = "";
        string strCabecera = "";
        string strDatos = "";
        string strAsunto = "";
        string strTO = "";
        string[] aNombreProfesionales = Regex.Split(strNombreProfesionales, @"/");

        strAsunto = "Pedido CVT. Confirmación de pedido.";
        strCabecera = @"<br><br><br> <u class='TITULO'>Relación de profesionales seleccionados</u>";
        strMensaje = "El pedido que has realizado para obtener los curriculums de los profesionales que se muestran al final de este correo, se está procesando y te será entregado a esta misma cuenta en cuanto esté preparado.<br><br>";
        strMensaje += "Lozalizador asignado: " + trackingId;

        strDatos = "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
        //for (int i = aNombreProfesionales.Length - 1; i >= 0; i--)
        for (int i = 0; i < aNombreProfesionales.Length; i++)
        {
            if (aNombreProfesionales[i] == "") continue;
            strDatos += "<tr>";
            strDatos += "<td width='100%'>" + aNombreProfesionales[i].ToString() + "</td>";
            strDatos += "</tr>";
        }
        strDatos += "</table>";

        ArrayList aListCorreo = new ArrayList();

        string[] aDestinatarios = Regex.Split(strDestinatarios, @",");

        for (int i = 0; i < aDestinatarios.Length; i++)
        {
            try
            {
                strMensaje += strCabecera + strDatos;
                strTO = aDestinatarios[i].ToString();

                string[] aMail = { strAsunto, strMensaje, strTO, ""};
                aListCorreo.Add(aMail);
            }
            catch (System.Exception objError)
            {
                enviarCorreoErrorEDA("Error al enviar el mail.", objError);
                enviarCorreoErrorUsuario();

            }
        }

        if (aDestinatarios.Length != 0) SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);
    }

    private void enviarCorreo(string extension)
    {
        string strMensaje = "";
        string strCabecera = "";
        string strDatos = "";
        string strAsunto = "";
        string strTO = "";
        string[] aNombreProfesionales = Regex.Split(strNombreProfesionales, @"/");

        strAsunto = "Pedido CVT (Localizador: " + trackingId + ")";
        if (aNombreProfesionales.Length > 2)
            strCabecera = @" <LABEL class='TITULO'>Se adjuntan los curriculums de los profesionales que se muestran a continuación:</LABEL>";
        else
            strCabecera = @" <LABEL class='TITULO'>Se adjunta el curriculum del profesional que se muestra a continuación:</LABEL>";



        strDatos = "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
        //for (int i = aNombreProfesionales.Length - 1; i >= 0; i--)
        for (int i = 0; i < aNombreProfesionales.Length; i++)
        {
            if (aNombreProfesionales[i] == "") continue;
            strDatos += "<tr>";
            strDatos += "<td width='100%'>" + aNombreProfesionales[i].ToString() + "</td>";
            strDatos += "</tr>";
        }
        strDatos += "</table>";

        ArrayList aListCorreo = new ArrayList();

        string[] aDestinatarios = Regex.Split(strDestinatarios, @",");

        for (int i = 0; i < aDestinatarios.Length; i++)
        {
            try
            {
                strMensaje = strCabecera + strDatos;
                strTO = aDestinatarios[i].ToString();

                string[] aMail = { strAsunto, strMensaje, strTO, pathDirectory + trackingId + @"\" + nombreDoc + extension };
                aListCorreo.Add(aMail);
            }
            catch (System.Exception objError)
            {
                enviarCorreoErrorEDA("Error al enviar el mail.", objError);
                enviarCorreoErrorUsuario();

            }
        }

        if (aDestinatarios.Length != 0) SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);
    }
    
    private string SerializeArrayList(ArrayList obj)
    {

        System.Xml.XmlDocument doc = new XmlDocument();
        Type[] extraTypes = new Type[1];
        extraTypes[0] = typeof(ParametrosWS);
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ArrayList), extraTypes);
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        try
        {
            serializer.Serialize(stream, obj);
            stream.Position = 0;
            doc.Load(stream);
            return doc.InnerXml;
        }
        catch { throw; }
        finally
        {
            stream.Close();
            stream.Dispose();
        }
    }

    private string SerializeHashtable(Dictionary<object, object> obj)
    {
        //string serializedDictionary;

        //Serialize
        using (MemoryStream stream = new MemoryStream())
        {
            System.Runtime.Serialization.DataContractSerializer s1 = new System.Runtime.Serialization.DataContractSerializer(typeof(Dictionary<object, object>));
            s1.WriteObject(stream, obj);

            //serializedDictionary = Encoding.UTF8.GetString(stream.ToArray());
            return Encoding.UTF8.GetString(stream.ToArray());
            //take a look it's been serialized
            // Console.WriteLine(serializedDictionary);
        }

    }

    private static long GrabarStream(string sFich, System.IO.Stream mistream)
    {
        long totalBytesRead = 0;
        try
        {
            FileStream targetStream = null;
            using (targetStream = new FileStream(sFich, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 4K chunks and save to output stream
                const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = mistream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                    totalBytesRead += count;
                }
                targetStream.Close();
                mistream.Close();
            }

        }
        catch (Exception)
        {
            //string msg = ex.Message;
            //msg = ex.InnerException == null ? msg += ". No innerexception" : msg += ". " + ex.InnerException;
            //return msg;
            totalBytesRead = -1;
        }

        return totalBytesRead;
    }
    
    private void enviarCorreoErrorEDA(string strDescripcion, System.Exception objError)
    {
        string strCuerpo = "";
        string strAsunto = "";
        string strTO = "EDA@ibermatica.com;";

        strAsunto = "Error en SUPER (exportación de curriculums)";
        strCuerpo += "No se ha podido realizar el pedido de curvit con localizador " + trackingId + ".<br><br>";
        strCuerpo += "Se ha producido un error en la página: " + currentPath + "<br>" +
            "en la dirección IP: " + ipAdress + "<br><br>";

        strCuerpo += "Usuario: " + usuario + "<br><br>";
        strCuerpo += "Código de Red: " + codred + "<br><br>";

        strCuerpo += "Mensaje de error: " + objError.Message + "<br><br>";
        strCuerpo += "Descri. de error: " + strDescripcion + "<br><br>";
        //Gets the Detailed Error Message
        strCuerpo += "<b>El detalle del error:</b>" + objError.InnerException + "<br><br>";
        strCuerpo += "<b>El origen del error:</b>" + objError.Source + "<br><br>";
        strCuerpo += "<b>El lugar del error:</b>" + objError.StackTrace.Replace(((char)10).ToString(), "<br>") + "<br><br>";
        strCuerpo += "<b>El método del error:</b>" + objError.TargetSite + "<br><br>";

        ArrayList aListCorreo = new ArrayList();

        string[] aMailEDA = { strAsunto, strCuerpo, strTO, "" };
        aListCorreo.Add(aMailEDA);

        SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);
    }
    private void enviarCorreoErrorUsuario()
    {
        string strCuerpo = "";
        string strAsunto = "";

        strAsunto = "Error en SUPER (exportación de curriculums)";
        strCuerpo += "Se ha producido un error en la generación del pedido con localizador " + trackingId + ".<br><br><br><br>";
        strCuerpo += "Disculpa las molestias.";

        ArrayList aListCorreo = new ArrayList();

        string[] aMail = { strAsunto, strCuerpo, codred, "" };
        aListCorreo.Add(aMail);

        SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);
    }
    private void enviarCorreoErrorEDA(string strDescripcion)
    {
        string strCuerpo = "";
        string strAsunto = "";
        string strTO = "EDA@ibermatica.com;";

        strAsunto = "Error en SUPER (exportación de curriculums)";
        strCuerpo += "No se ha podido realizar el pedido de curvit con localizador " + trackingId + ".<br><br>";
        strCuerpo += "Se ha producido un error en la página: " + currentPath + "<br>" +
            "en la dirección IP: " + ipAdress + "<br><br>";
        strCuerpo += "Usuario: " + usuario + "<br><br>";
        strCuerpo += "Código de Red: " + codred + "<br><br>";
        strCuerpo += "Descri. de error: " + strDescripcion + "<br><br>";

        ArrayList aListCorreo = new ArrayList();

        string[] aMailEDA = { strAsunto, strCuerpo, strTO, "" };
        aListCorreo.Add(aMailEDA);

        SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);
    }


    #region Generación de certificados
    /// <summary>
    /// Crea dos archivos y los envía por PaqExpress.
    /// Uno de los archivos es un zip que contiene todos los documentos acreditativos de certificados
    /// El otro archivo es un Excel que contiene la relación de los documentos contenidos en el zip
    /// </summary>
    private void GenerarCorreoDocumentos()
    {
        svcSUPERIBOffice.IsvcSUPERClient osvcCVT = null;
        try
        {
            ILog miLog = LogManager.GetLogger("SUP");
            log4net.Config.XmlConfigurator.Configure();

            miLog.Debug(codred + "->" + trackingId + " exportarCV -> GenerarCorreoDocumentos. Inicio exportación documentos.");
            //osvcCVT = new svcSUPERIBOffice.IsvcSUPERClient();
            //Stream oStream = new MemoryStream();
            //oStream = osvcCVT.obtenerPlantilla(sFiltros, extension, int.Parse(nPlantilla), htCampos, byte.Parse(nDocs), trackingId);
            string sDirectorio = pathDirectory + Guid.NewGuid().ToString();// trackingId;
            Directory.CreateDirectory(sDirectorio);
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> GenerarCorreoDocumentos. Se ha creado el directorio " + sDirectorio);
            //GrabarStream(pathDirectory + trackingId + @"\" + nombreDoc + extension, oStream);

            //Genero un zip con todos lo documentos y un fichero Excel que relaciona el contido del zip anterior
            string sNomFicheros = GenerarFicherosDocumentos(sDirectorio, bExportarFA, bExportarCurso, bExportarCert, bExportarTitidioma, 
                                                gslIdsFicepi, gslIdsFA, gslIdsCurso, gslIdsCertificado, gslDensTitIdioma, miLog);
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> GenerarCorreoDocumentos. Se han generado todos archivos.");
            string[] aFichs = Regex.Split(sNomFicheros, "@#@");
            string sNomZipDocs = aFichs[0];
            string sNomExcelDocs = aFichs[1];
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> GenerarCorreoDocumentos. sNomZipDocs=" + sNomZipDocs);
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> GenerarCorreoDocumentos. sNomExcelDocs=" + sNomExcelDocs);
            //Compruebo que hay algo que enviar
            bool bTodosFA = true;
            if (gslIdsFA != "") bTodosFA = false;
            bool bTodosCurso = true;
            if (gslIdsCurso != "") bTodosCurso = false;
            bool bTodosCert = true;
            if (gslIdsCertificado != "") bTodosCert = false;
            bool bTodosTitIdio = true;
            if (gslDensTitIdioma != "") bTodosTitIdio = false;

            if (sNomZipDocs == "TAMANO_EXCEDIDO")
            {
                EnviarCorreoTamanoExcedido(trackingId, sNomExcelDocs, strDestinatarios, aProfSelec);
                //throw new FileSizeMessageException("Fichero adjuntos: Se ha superado el tamaño máximo permitido (" + TamMaxPermitido.ToString() + " bytes.");
            }
            else
            {
                if (aProfEnviar.Count > 0)
                {
                    //Envio correo indicando que la documentación se ha generado y será enviada por paqExpress
                    //Si la lista de profesionales resultante es diferente a la lista de profesionales original hay que sacar las dos listas
                    enviarCorreoConfPedidoDoc(2, bExportarFA, bExportarCurso, bExportarCert, bExportarTitidioma, bTodosFA, bTodosCurso, bTodosCert, bTodosTitIdio);
                    //Subo los archivos a PaqExpress
                    miLog.Debug(codred + "->" + trackingId + " Se envía paquete a PaqExpress.");

                    enviarPackExpress(sNomExcelDocs, sNomZipDocs);
                    miLog.Debug(codred + "->" + trackingId + " Paquete enviado a PaqExpress.");

                }
                else
                    enviarCorreoPedidoVacio(bExportarFA, bExportarCurso, bExportarCert, bExportarTitidioma, bTodosFA, bTodosCurso, bTodosCert, bTodosTitIdio);
            }
        }
        catch (FaultException<svcSUPERIBOffice.SUPERException> cex)
        {
            enviarCorreoErrorEDA(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage, cex);
            enviarCorreoErrorUsuario();
        }
        catch (Exception ex)
        {
            enviarCorreoErrorEDA("Error al exportar", ex);
            enviarCorreoErrorUsuario();
        }
        finally
        {
            #region finalización
            if (osvcCVT != null && osvcCVT.State != System.ServiceModel.CommunicationState.Closed)
            {
                try
                {
                    if (osvcCVT.State != System.ServiceModel.CommunicationState.Faulted) osvcCVT.Close();
                    else if (osvcCVT.State != System.ServiceModel.CommunicationState.Closed) osvcCVT.Abort();
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al exportar", ex);
                }
            }
            #endregion
        }
    }
    private void EnviarCorreoTamanoExcedido(string sLocalizador, string sTamEnBytes, string sTO, ArrayList lstProf)
    {
        System.Text.StringBuilder sB = new System.Text.StringBuilder();
        string sAsunto = "Error en la generación del archivo de documentos";
        string sTamMaxPack = System.Configuration.ConfigurationManager.AppSettings["TamMaxPack"];
        if (sTamMaxPack != "")
        {
            double lMax = double.Parse(sTamMaxPack);
            if (lMax > 1024)
            {
                lMax = lMax / 1024;
                sTamMaxPack = lMax.ToString("#.##") + " Gb.";
            }
            else
                sTamMaxPack=lMax.ToString("#,###") + " Mb.";
        }
        else
            sTamMaxPack = "1,9 Gb";
        //Convierto el perso del archivo
        string sTamFich = "";
        double lTamFich = double.Parse(sTamEnBytes) / 1024;//Paso a Kb
        lTamFich = lTamFich / 1024;//Paso a Mb
        if (lTamFich > 1024)
        {
            lTamFich = lTamFich / 1024;//Paso a Gb
            sTamFich = lTamFich.ToString("#.##") + " Gb.";
        }
        else
        {
            sTamFich = lTamFich.ToString("#,###") + " Mb.";
        }
        //sB.Append("PAQEXPRESS no puede hacerte entrega del paquete generado por CVT con los documentos acreditativos de los certificados");
        //sB.Append(" de los profesionales que se muestran a continuación, al exceder el volumen máximo permitido, que está limitado a ");
        sB.Append("El pedido con localizador " + sLocalizador + ", ha sido generado.");
        sB.Append(" Su volumen, de " + sTamFich + " excede del máximo permitido por PAQEXPRESS,");
        sB.Append(" que es de " + sTamMaxPack + ", por lo que no puede ser enviado.");
        sB.Append(" Por favor, acota más el filtro de búsqueda para que su resultado se adecúe a los parámetros máximos establecidos.");
        sB.Append("<br /><br />Disculpa las molestias.");
        //24/11/2014 Víctor dice que no hay que pasar la lista de profesionales
        //if (lstProf.Count > 0)
        //{
        //    sB.Append("<br /><br /><u><b>Relación de profesionales</u></b><br /><br />");
        //    foreach (string sProf in lstProf)
        //    {
        //        sB.Append(sProf);
        //        sB.Append("<br />");
        //    }
        //}
        ArrayList aListCorreo = new ArrayList();
        string[] aMail = { sAsunto, sB.ToString(), sTO, "" };
        aListCorreo.Add(aMail);
        Correo.EnviarCorreosCVT(aListCorreo);
    }

    private void enviarPackExpress(string sNomExcelDocs, string sNomZipDocs)
    {
        int idPack = -1;
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
        string sTexto = "";
        svcSendPack.SendPackClient oPaq = null;

        try
        {
            ILog miLog = LogManager.GetLogger("SUP");
            log4net.Config.XmlConfigurator.Configure();
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> enviarPackExpress");

            #region Inicio de envío a PaqExpress **************/
            oPaq = new svcSendPack.SendPackClient();

            DateTime dtIni = DateTime.Now;
            sTexto = dtIni.ToString() + "\r\n";

            strb.Append("<Pack>");
            strb.Append("<User>PAQEXPRESS</User>");
            strb.Append("<Clave>XRJ001-WCF-SUPER-CV.</Clave>");
            strb.Append("<IdFicepi>" + strDestinatariosIdFicepi + "</IdFicepi>");
            strb.Append("<FPedido>" + DateTime.Now.ToString() + "</FPedido>");
            //strb.Append("<Profesionales>" + strNombreProfesionales.Replace("/", ";") + "</Profesionales>");
            StringBuilder sbAux = new StringBuilder();
            foreach (string sP in aProfEnviar)
            {
                sbAux.Append(sP);
                sbAux.Append(";");
            }
            strb.Append("<Profesionales>" + sbAux.ToString() + "</Profesionales>");
            strb.Append("<Ref>" + trackingId + "</Ref>");//Nº de referencia para tracking
            strb.Append("<Tipo>CERTIFICADOS</Tipo>");//Tipo de paquete
            strb.Append("</Pack>");

            //oPaq.CrearPaqueteCV(new FileInfo(nombreDoc + extension).Name, strb.ToString(), File.OpenRead(pathDirectory + trackingId + @"\" + nombreDoc + extension));
            //Creo el paquete
            idPack = oPaq.CrearPaqueteMulti(strb.ToString());
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> enviarPackExpress. Creado paquete " + idPack.ToString());

            //Le añado el fichero Excel
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> enviarPackExpress. Se va a agregar el fichero cabecera. sNomExcelDocs=" + sNomExcelDocs);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<Pack>");
            sb.Append("<User>PAQEXPRESS</User>");
            sb.Append("<Clave>XRJ001-WCF-SUPER-CV.</Clave>");
            sb.Append("<IdFicepi>" + strDestinatariosIdFicepi + "</IdFicepi>");
            sb.Append("<FinPack>N</FinPack>");//Indica que NO es el último fichero del paquete
            sb.Append("<IdPack>" + idPack.ToString() + "</IdPack>");//identificador del paquete
            sb.Append("</Pack>");
            oPaq.AgregarFichero(new FileInfo(sNomExcelDocs).Name, sb.ToString(), File.OpenRead(sNomExcelDocs));
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> enviarPackExpress. Agregado fichero cabecera. sNomExcelDocs=" + sNomExcelDocs);

            //Le añado el zip con todos los documentos
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> enviarPackExpress. Se va a agregar fichero zip. sNomZipDocs=" + sNomZipDocs);
            System.Text.StringBuilder sbFin = new System.Text.StringBuilder();
            sbFin.Append("<Pack>");
            sbFin.Append("<User>PAQEXPRESS</User>");
            sbFin.Append("<Clave>XRJ001-WCF-SUPER-CV.</Clave>");
            sbFin.Append("<IdFicepi>" + strDestinatariosIdFicepi + "</IdFicepi>");
            sbFin.Append("<FinPack>S</FinPack>");//Indica que SI es el último fichero del paquete
            sbFin.Append("<IdPack>" + idPack.ToString() + "</IdPack>");//identificador del paquete
            sbFin.Append("</Pack>");
            oPaq.AgregarFichero(new FileInfo(sNomZipDocs).Name, sbFin.ToString(), File.OpenRead(sNomZipDocs));
            miLog.Debug(codred + "->" + trackingId + " exportarCV -> enviarPackExpress. Agregado fichero zip. sNomZipDocs=" + sNomZipDocs);

            #endregion
        }
        catch (FaultException<svcSendPack.PackException> cex)
        {
            string sError = "Error: Código:" + cex.Detail.ErrorCode + ". Descripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;

            if (cex.Detail.InnerMessage != "")
                sError += "\r\nInnerMessage: " + cex.Detail.InnerMessage;
            sTexto += sError + "\r\n";
            enviarCorreoErrorEDA(sTexto, cex);
            enviarCorreoErrorUsuario();
        }
        catch (Exception ex)
        {
            enviarCorreoErrorEDA(ex.Message + "\r\n", ex);
            enviarCorreoErrorUsuario();
        }
        finally
        {
            //Cierre del canal
            if (oPaq != null && oPaq.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (oPaq.State != System.ServiceModel.CommunicationState.Faulted) oPaq.Close();
                else if (oPaq.State != System.ServiceModel.CommunicationState.Closed) oPaq.Abort();
            }
        }
    }
    private void enviarCorreoConfPedidoDoc(int idPaso, bool bExportarFA, bool bExportarCurso, bool bExportarCert, bool bExportarTitidioma, 
                                           bool bTodosFA, bool bTodosCurso, bool bTodosCert, bool bTodosTitIdioma)
    {
        string strMensaje = "";
        string strProfs = "";
        string strAsunto = "";
        string strTO = "";
        bool bListasProfDiferentes = false;
        //string[] aNombreProfesionales = Regex.Split(strNombreProfesionales, @"/");
        string strFAs = "";
        string[] aNombreFA = Regex.Split(strNombreFA, @"##");
        string strCursos = "";
        string[] aNombreCurso = Regex.Split(strNombreCurso, @"##");
        string strCerts = "";
        string[] aNombreCert = Regex.Split(strNombreCert, @"##");
        string strNombreTitIdiomas = "";
        string[] aNombreTitIdioma = Regex.Split(gslDensTitIdioma, @"##");

        //Puede que los profesionales a enviar no sean todos los seleccionados porque
        //  1.- No he pedido todos los documentos
        //  2.- Alguno de los profesionales de la lista no tienen documento
        if (idPaso == 2)
        {
            if (!ListasIguales(aProfSelec, aProfEnviar))
                bListasProfDiferentes = true;
        }
        if (bListasProfDiferentes)
        {
            strProfs = @"<br><br><br><u class='TITULO'>Relación de profesionales seleccionados</u>";
            strProfs += "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            foreach (string sProfesional in aProfSelec)
            {
                strProfs += "<tr><td width='100%'>" + sProfesional + "</td></tr>";
            }
            strProfs += "</table>";

            strProfs += @"<br><br><br> <u class='TITULO'>Relación de profesionales con acreditación</u>";
            strProfs += "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            foreach (string sProfesional in aProfEnviar)
            {
                strProfs += "<tr><td width='100%'>" + sProfesional + "</td></tr>";
            }
            strProfs += "</table>";
        }
        else
        {
            strProfs = @"<br><br><br> <u class='TITULO'>Relación de profesionales seleccionados</u>";
            strProfs += "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            foreach (string sProfesional in aProfSelec)
            {
                strProfs += "<tr><td width='100%'>" + sProfesional + "</td></tr>";
            }
            strProfs += "</table>";
        }
        if (idPaso == 1)
        {
            strAsunto = "Pedido CVT. Confirmación de pedido.";
            if (bTodosCert && bTodosFA && bTodosCurso && bTodosTitIdioma)
            {//Si está marcado que se exporten todos los certificados
                strMensaje = @"El pedido que has realizado para obtener los documentos acreditativos que se dispongan de los profesionales 
                          que se relacionan más abajo, se está procesando. Se te avisará a esta misma cuenta en cuanto esté preparado.<br><br>";
            }
            else
            {
                strMensaje = @"El pedido que has realizado para obtener los documentos acreditativos que has seleccionado 
                           de los profesionales que se relacionan más abajo, se está procesando. 
                           Se te avisará a esta misma cuenta en cuanto esté preparado.<br><br>";
            }
        }
        else
        {
            strAsunto = "Pedido CVT (Localizador: " + trackingId + ")";
            if (bListasProfDiferentes)
            {
                if (bTodosCert && bTodosFA && bTodosCurso && bTodosTitIdioma)
                    strMensaje = @"El pedido que has realizado para obtener los documentos acreditativos de los 
                                       profesionales que se relacionan más abajo, se ha completado y te será entregado en breve vía PAQEXPRESS a 
                                       esta misma cuenta. 
                                       Observa que los documentos pertenecen a un subconjunto de los profesionales seleccionados; 
                                       ello es debido a que no todos ellos disponían de acreditación.<br><br>";
                else
                    strMensaje = @"El pedido que has realizado para obtener los documentos acreditativos que has seleccionado de los 
                                       profesionales que se relacionan más abajo, se ha completado y te será entregado en breve vía PAQEXPRESS a 
                                       esta misma cuenta. 
                                       Observa que los documentos pertenecen a un subconjunto de los profesionales seleccionados; 
                                       ello es debido a que no todos ellos disponían de acreditación.<br><br>";
            }
            else
            {
                if (bTodosCert && bTodosFA && bTodosCurso && bTodosTitIdioma)
                    strMensaje = @"El pedido que has realizado para obtener los documentos acreditativos de los profesionales que se relacionan 
                                   más abajo, ha generado los archivos correspondientes y te será entregado vía PAQEXPRESS a esta misma 
                                   cuenta en cuanto esté preparado.<br><br>";
                else
                    strMensaje = @"El pedido que has realizado para obtener los documentos acreditativos que has seleccionado 
                                   de los profesionales que se relacionan más abajo, ha generado los archivos correspondientes 
                                   y te será entregado vía PAQEXPRESS a esta misma cuenta en cuanto esté preparado.<br><br>";
            }
        }
        if (idPaso == 1)
            strMensaje += "Lozalizador asignado: " + trackingId;
        else
            strMensaje += "Lozalizador: " + trackingId;

        #region Mostrar listas de documentos si no se han seleccionado todos
        //Si no se ha marcado Todos los titulos academicos sacamos la lista de los seleccionados
        if (bExportarFA && !bTodosFA)
        {
            strFAs = @"<br /><br /><u class='TITULO'>Relación de titulaciones académicas</u>";
            strFAs += "<br /><br /><table id='tblFA' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            for (int i = 0; i < aNombreFA.Length; i++)
            {
                if (aNombreFA[i] == "") continue;
                strFAs += "<tr>";
                strFAs += "<td width='100%'>" + aNombreFA[i].ToString() + "</td>";
                strFAs += "</tr>";
            }
            strFAs += "</table>";
        }
        //Si no se ha marcado Todos los cursos sacamos la lista de los seleccionados
        if (bExportarCurso && !bTodosCurso)
        {
            strCursos = @"<br /><br /><u class='TITULO'>Relación de cursos</u>";
            strCursos += "<br /><br /><table id='tblCurso' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            for (int i = 0; i < aNombreCurso.Length; i++)
            {
                if (aNombreCurso[i] == "") continue;
                strCursos += "<tr>";
                strCursos += "<td width='100%'>" + aNombreCurso[i].ToString() + "</td>";
                strCursos += "</tr>";
            }
            strCursos += "</table>";
        }
        //Si no se ha marcado Todos los certificados sacamos la lista de los seleccionados
        if (bExportarCert && !bTodosCert)
        {
            strCerts = @"<br /><br /><u class='TITULO'>Relación de certificados</u>";
            strCerts += "<br /><br /><table id='tblCert' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            //for (int i = aNombreProfesionales.Length - 1; i >= 0; i--)
            for (int i = 0; i < aNombreCert.Length; i++)
            {
                if (aNombreCert[i] == "") continue;
                strCerts += "<tr>";
                strCerts += "<td width='100%'>" + aNombreCert[i].ToString() + "</td>";
                strCerts += "</tr>";
            }
            strCerts += "</table>";
        }
        //Si no se ha marcado Todos los titulos de idiomas sacamos la lista de los seleccionados
        if (bExportarTitidioma && !bTodosTitIdioma)
        {
            strNombreTitIdiomas = @"<br /><br /><u class='TITULO'>Relación de títulos de idiomas</u>";
            strNombreTitIdiomas += "<br /><br /><table id='tblTitIdioma' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            for (int i = 0; i < aNombreTitIdioma.Length; i++)
            {
                if (aNombreTitIdioma[i] == "") continue;
                strNombreTitIdiomas += "<tr>";
                strNombreTitIdiomas += "<td width='100%'>" + aNombreTitIdioma[i].ToString() + "</td>";
                strNombreTitIdiomas += "</tr>";
            }
            strNombreTitIdiomas += "</table>";
        }
        #endregion
        ArrayList aListCorreo = new ArrayList();

        string[] aDestinatarios = Regex.Split(strDestinatarios, @",");

        for (int i = 0; i < aDestinatarios.Length; i++)
        {
            try
            {
                strMensaje += strProfs + strFAs + strCursos + strCerts + strNombreTitIdiomas;
                strTO = aDestinatarios[i].ToString();

                string[] aMail = { strAsunto, strMensaje, strTO, "" };
                aListCorreo.Add(aMail);
            }
            catch (System.Exception objError)
            {
                enviarCorreoErrorEDA("Error al enviar el mail.", objError);
                enviarCorreoErrorUsuario();

            }
        }

        if (aDestinatarios.Length != 0) SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);
    }
    private void enviarCorreoPedidoVacio(bool bExportarFA, bool bExportarCurso, bool bExportarCert, bool bExportarTitidioma,
                                         bool bTodosFA, bool bTodosCurso, bool bTodosCert, bool bTodosTitIdio)
    {
        string strMensaje = "";
        string strCabecera = "";
        string strDatos = "";
        string strAsunto = "";
        string strTO = "";

        strAsunto = "Pedido CVT (Localizador: " + trackingId + ")";
        strMensaje = @"No existe ningún documento acreditativo que cumpla las condiciones especificadas para los profesionales 
                        que se relacionan más abajo. El pedido queda anulado.<br><br>";
        strMensaje += "Lozalizador: " + trackingId;

        strCabecera = @"<br><br><br> <u class='TITULO'>Relación de profesionales seleccionados</u>";
        strCabecera += "<br><br><table id='tblTitulo' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>"; 
        foreach (string sProfesional in aProfSelec)
        {
            strCabecera += "<tr><td width='100%'>" + sProfesional + "</td></tr>";
        }
        strCabecera += "</table>";

        //Si no se ha marcado Todos los titulos academicos sacamos la lista de los seleccionados
        if (bExportarFA && !bTodosFA)
        {
            string[] aNombreFA = Regex.Split(strNombreFA, @"##");
            strDatos += @"<br /><br /><u class='TITULO'>Relación de titulaciones acacémicas</u>";
            strDatos += "<br /><br /><table id='tblFA' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            for (int i = 0; i < aNombreFA.Length; i++)
            {
                if (aNombreFA[i] == "") continue;
                strDatos += "<tr>";
                strDatos += "<td width='100%'>" + aNombreFA[i].ToString() + "</td>";
                strDatos += "</tr>";
            }
            strDatos += "</table>";
        }
        //Si no se ha marcado Todos los cursos sacamos la lista de los seleccionados
        if (bExportarCurso && !bTodosCurso)
        {
            string[] aNombreCurso = Regex.Split(strNombreCurso, @"##");
            strDatos += @"<br /><br /><u class='TITULO'>Relación de cursos</u>";
            strDatos += "<br /><br /><table id='tblCurso' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            for (int i = 0; i < aNombreCurso.Length; i++)
            {
                if (aNombreCurso[i] == "") continue;
                strDatos += "<tr>";
                strDatos += "<td width='100%'>" + aNombreCurso[i].ToString() + "</td>";
                strDatos += "</tr>";
            }
            strDatos += "</table>";
        }
        //Si no se ha marcado Todos los certificados sacamos la lista de los seleccionados
        if (bExportarCert && !bTodosCert)
        {
            string[] aNombreCert = Regex.Split(strNombreCert, @"##");
            strDatos += @"<br /><br /><u class='TITULO'>Relación de certificados</u>";
            strDatos += "<br /><br /><table id='tblCert' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            //for (int i = aNombreProfesionales.Length - 1; i >= 0; i--)
            for (int i = 0; i < aNombreCert.Length; i++)
            {
                if (aNombreCert[i] == "") continue;
                strDatos += "<tr>";
                strDatos += "<td width='100%'>" + aNombreCert[i].ToString() + "</td>";
                strDatos += "</tr>";
            }
            strDatos += "</table>";
        }
        //Si no se ha marcado Todos los titulos de idiomas sacamos la lista de los seleccionados
        if (bExportarTitidioma && !bTodosTitIdio)
        {
            string[] aNombreTitIdio = Regex.Split(gslDensTitIdioma, @"##");
            strDatos += @"<br /><br /><u class='TITULO'>Relación de titulaciones de idiomas</u>";
            strDatos += "<br /><br /><table id='tblIdio' width='100%' class='texto' border='0' cellspacing='0' cellpadding='0'>";
            for (int i = 0; i < aNombreTitIdio.Length; i++)
            {
                if (aNombreTitIdio[i] == "") continue;
                strDatos += "<tr>";
                strDatos += "<td width='100%'>" + aNombreTitIdio[i].ToString() + "</td>";
                strDatos += "</tr>";
            }
            strDatos += "</table>";
        }
        ArrayList aListCorreo = new ArrayList();

        string[] aDestinatarios = Regex.Split(strDestinatarios, @",");

        for (int i = 0; i < aDestinatarios.Length; i++)
        {
            try
            {
                strMensaje += strCabecera + strDatos;
                strTO = aDestinatarios[i].ToString();

                string[] aMail = { strAsunto, strMensaje, strTO, "" };
                aListCorreo.Add(aMail);
            }
            catch (System.Exception objError)
            {
                enviarCorreoErrorEDA("Error al enviar el mail.", objError);
                enviarCorreoErrorUsuario();

            }
        }

        if (aDestinatarios.Length != 0) SUPER.Capa_Negocio.Correo.EnviarCorreosCVT(aListCorreo);
    }
    /// <summary>
    /// Trae de Atenea los documentos acreditativos de los certificados de los profesionales y genera un zip con ellos
    /// Ademas genera un archivo Excel con el contenido del archivo zip
    /// </summary>
    /// <param name="sDirectorio"></param>
    /// <param name="sListaFicepis"></param>
    /// <param name="sListacertificados"></param>
    /// <returns></returns>
    private string GenerarFicherosDocumentos(string sDirectorio, bool bExpFA, bool bExpCurso, bool bExpCert, bool bExpTitidioma, 
                                             string sListaFicepis, string sListaFAs,
                                             string sListaCursos, string sListaCertificados, string sListaTitIdiomas, ILog miLog)
    {
        string sNomArchivoZip = "", sNomArchivoExcel="", sNomAux="";
        miLog.Debug(codred + "->" + trackingId + " GenerarFicherosDocumentos");
        List<SUPER.BLL.Titulacion> oListaFA=null;
        List<SUPER.BLL.Curso> oListaCurso = null;
        List<SUPER.BLL.Certificado> oListaCert = null;
        List<SUPER.BLL.Idioma> oListaTitIdioma = null;

        //Obtengo los datos necesarios: Nombre profesional, nombre certificado, nombre archivo, documento en Atenea
        //Solo traigo elementos que tengan documento en Atenea

        if (bExpFA)
        {
            oListaFA = SUPER.BLL.Titulacion.GetDocsExportacion(sListaFicepis, sListaFAs);
        }
        if (bExpCurso)
        {
            oListaCurso = SUPER.BLL.Curso.GetDocsExportacion(sListaFicepis, sListaCursos);
        }
        if (bExpCert)
        {
            oListaCert = SUPER.BLL.Certificado.GetCertificadosExportacion(sListaFicepis, sListaCertificados);
        }
        if (bExpTitidioma)
        {
            oListaTitIdioma = SUPER.BLL.Idioma.GetDocsExportacion(sListaFicepis, sListaTitIdiomas);
        }
         
        //miLog.Debug("Se van a traer " + oListaCert.Count.ToString("#,###") + " archivos desde ATENEA.");

        #region Genero un zip con todos los documentos acreditativos
        #region Titulación academica
        if (bExpFA)
        {

            foreach (SUPER.BLL.Titulacion oElem in oListaFA)
            {
                //Le ponemos como prefijo el t2_iddocumento para que no haya archivos iguales
                sNomAux = sDirectorio + "\\" + oElem.t2_iddocumento.ToString() + "_" + oElem.NDOC;
                //Traigo el contenido del archivo de Atenea
                try
                {
                    //miLog.Debug("Se va a traer desde ATENEA el archivo: " + oCert.t2_iddocumento.ToString() + "_" + oCert.T593_NDOC);
                    byte[] Archivo = IB.Conserva.ConservaHelper.ObtenerDocumento((long)oElem.t2_iddocumento).content;
                    //Grabo el documento en la carpeta. 
                    System.IO.File.WriteAllBytes(sNomAux, Archivo);
                    //Marco que el documento se ha recuperado correctamente (para que en el Excel solo se relacionen los que tienen documento)
                    oElem.BDOC = true;
                    //Si el profesional no está en la lista de profesionales con certificado, lo añado
                    if (!aProfEnviar.Contains(oElem.Profesional))
                        aProfEnviar.Add(oElem.Profesional);
                }
                catch
                {
                    oElem.Estado = "No disponible en repositorio. Avisar al CAU.";
                    miLog.Debug(codred + "->" + trackingId + " Error al traer archivo desde ATENEA. Profesional: " + oElem.Profesional + " Documento: " + oElem.NDOC);
                }
            }
        }
        #endregion

        #region Curso
        if (bExpCurso)
        {
            foreach (SUPER.BLL.Curso oElem in oListaCurso)
            {
                //Le ponemos como prefijo el t2_iddocumento para que no haya archivos iguales
                sNomAux = sDirectorio + "\\" + oElem.t2_iddocumento.ToString() + "_" + oElem.T575_NDOC;
                //Traigo el contenido del archivo de Atenea
                try
                {
                    //miLog.Debug("Se va a traer desde ATENEA el archivo: " + oCert.t2_iddocumento.ToString() + "_" + oCert.T593_NDOC);
                    byte[] Archivo = IB.Conserva.ConservaHelper.ObtenerDocumento((long)oElem.t2_iddocumento).content;
                    //Grabo el documento en la carpeta. 
                    System.IO.File.WriteAllBytes(sNomAux, Archivo);
                    //Marco que el documento se ha recuperado correctamente (para que en el Excel solo se relacionen los que tienen documento)
                    oElem.BDOC = true;
                    //Si el profesional no está en la lista de profesionales con certificado, lo añado
                    if (!aProfEnviar.Contains(oElem.Profesional))
                        aProfEnviar.Add(oElem.Profesional);
                }
                catch
                {
                    oElem.Estado = "No disponible en repositorio. Avisar al CAU.";
                    miLog.Debug(codred + "->" + trackingId + " Error al traer archivo desde ATENEA. Profesional: " + oElem.Profesional + " Documento: " + sNomAux);
                }
            }
        }
        #endregion

        #region Certificado
        if (bExpCert)
        {
            foreach (SUPER.BLL.Certificado oCert in oListaCert)
            {
                //Le ponemos como prefijo el t2_iddocumento para que no haya archivos iguales
                sNomAux = sDirectorio + "\\" + oCert.t2_iddocumento.ToString() + "_" + oCert.T593_NDOC;
                //Traigo el contenido del archivo de Atenea
                try
                {
                    //miLog.Debug("Se va a traer desde ATENEA el archivo: " + oCert.t2_iddocumento.ToString() + "_" + oCert.T593_NDOC);
                    byte[] Archivo = IB.Conserva.ConservaHelper.ObtenerDocumento((long)oCert.t2_iddocumento).content;
                    //Grabo el documento en la carpeta. 
                    System.IO.File.WriteAllBytes(sNomAux, Archivo);
                    //Marco que el documento se ha recuperado correctamente (para que en el Excel solo se relacionen los que tienen documento)
                    oCert.BDOC = true;
                    //Si el profesional no está en la lista de profesionales con certificado, lo añado
                    if (!aProfEnviar.Contains(oCert.Profesional))
                        aProfEnviar.Add(oCert.Profesional);
                }
                catch
                {
                    oCert.EstadoCertificado = "No disponible en repositorio. Avisar al CAU.";
                    miLog.Debug(codred + "->" + trackingId + " Error al traer archivo desde ATENEA. Profesional: " + oCert.Profesional + " Certificado: " + oCert.T593_NDOC);
                }
            }
        }
        #endregion

        #region Titulación idioma
        if (bExpTitidioma)
        {
            foreach (SUPER.BLL.Idioma oElem in oListaTitIdioma)
            {
                //Le ponemos como prefijo el t2_iddocumento para que no haya archivos iguales
                sNomAux = sDirectorio + "\\" + oElem.t2_iddocumento.ToString() + "_" + oElem.NDOC;
                //Traigo el contenido del archivo de Atenea
                try
                {
                    //miLog.Debug("Se va a traer desde ATENEA el archivo: " + oCert.t2_iddocumento.ToString() + "_" + oCert.T593_NDOC);
                    byte[] Archivo = IB.Conserva.ConservaHelper.ObtenerDocumento((long)oElem.t2_iddocumento).content;
                    //Grabo el documento en la carpeta. 
                    System.IO.File.WriteAllBytes(sNomAux, Archivo);
                    //Marco que el documento se ha recuperado correctamente (para que en el Excel solo se relacionen los que tienen documento)
                    oElem.BDOC = true;
                    //Si el profesional no está en la lista de profesionales con certificado, lo añado
                    if (!aProfEnviar.Contains(oElem.Profesional))
                        aProfEnviar.Add(oElem.Profesional);
                }
                catch
                {
                    oElem.Estado = "No disponible en repositorio. Avisar al CAU.";
                    miLog.Debug(codred + "->" + trackingId + " Error al traer archivo desde ATENEA. Profesional: " + oElem.Profesional + " Documento: " + oElem.NDOC);
                }
            }
        }
        #endregion

        miLog.Debug(codred + "->" + trackingId + " Se han traido todos los archivos desde ATENEA.");
        //Genero un zip con todos los archivos de la carpeta
        sNomArchivoZip = CompressFile(sDirectorio, trackingId);
        FileInfo fInfo = new FileInfo(sNomArchivoZip);
        long lTamFich = fInfo.Length;
        long iTamEnBytes = fInfo.Length, TamMaxPermitido = 104857600;//100Mb        
        string sTamMax = ConfigurationManager.AppSettings["TamMaxPack"];
        if (sTamMax != "")
            TamMaxPermitido = long.Parse(sTamMax) * 1024 * 1024;//Paso de Mb a bytes
        if (iTamEnBytes > TamMaxPermitido)
        {
            return "TAMANO_EXCEDIDO@#@" + iTamEnBytes.ToString();
        }
        
        #endregion

        #region Genero el archivo Excel que indica el contenido del zip
        svcEXCEL.IsvcEXCELClient osvcExcel =  new svcEXCEL.IsvcEXCELClient();
        #region Prueba para llamar al metodo de Excel sin dataset
        /*
        //Añado un elemento mas que será la cabecera con los titulos de cada columna del archivo Excel
        int iNumElem = oListaFA.Count + oListaCurso.Count + oListaCert.Count + oListaTitIdioma.Count + 1;
        object[] aDatosExcel = new object[iNumElem];
        int i=0;
        //Pongo el titulo de la columna de Excel
        object[] aTit = new object[4];
        aTit[0] = "Profesional";
        aTit[1] = "Documento acreditativo";
        aTit[2] = "Archivo";
        aTit[3] = "Error";
        aDatosExcel[i++] = aTit;

        foreach (SUPER.BLL.Titulacion oElem in oListaFA)
        {
            if (oElem.BDOC)
            {//Si se ha podido recuperar el documento meto fila en el Excel
                object[] arr_aux = new object[4];
                arr_aux[0] = oElem.Profesional;
                arr_aux[1] = oElem.T019_DESCRIPCION;
                arr_aux[2] = oElem.t2_iddocumento.ToString() + "_" + oElem.NDOC;
                arr_aux[3] = oElem.Estado;
                aDatosExcel[i++] = arr_aux;
            }
        }
        foreach (SUPER.BLL.Curso oElem in oListaCurso)
        {
            if (oElem.BDOC)
            {//Si se ha podido recuperar el documento meto fila en el Excel
                object[] arr_aux = new object[4];
                arr_aux[0] = oElem.Profesional;
                arr_aux[1] = oElem.T574_TITULO;
                if (oElem.T580_NDOC != "")
                    arr_aux[2] = oElem.t2_iddocumento.ToString() + "_" + oElem.T580_NDOC;
                else
                    arr_aux[2] = oElem.t2_iddocumento.ToString() + "_" + oElem.T575_NDOC;
                arr_aux[3] = oElem.Estado;
                aDatosExcel[i++] = arr_aux;
            }
        }
        foreach (SUPER.BLL.Certificado oCert in oListaCert)
        {
            if (oCert.BDOC)
            {//Si se ha podido recuperar el documento meto fila en el Excel
                object[] arr_aux = new object[4];
                arr_aux[0] = oCert.Profesional;
                arr_aux[1] = oCert.T582_NOMBRE;
                arr_aux[2] = oCert.t2_iddocumento.ToString() + "_" + oCert.T593_NDOC;
                arr_aux[3] = oCert.EstadoCertificado;
                aDatosExcel[i++] = arr_aux;
            }
        }
        foreach (SUPER.BLL.Idioma oElem in oListaTitIdioma)
        {
            if (oElem.BDOC)
            {//Si se ha podido recuperar el documento meto fila en el Excel
                object[] arr_aux = new object[4];
                arr_aux[0] = oElem.Profesional;
                arr_aux[1] = oElem.t021_titulo;
                arr_aux[2] = oElem.t2_iddocumento.ToString() + "_" + oElem.NDOC;
                arr_aux[3] = oElem.Estado;
                aDatosExcel[i++] = arr_aux;
            }
        }
         * */
        #endregion
        //PRUEBA DE CREACION DE DATASET
        //DataColumn colString = new DataColumn("StringCol");
        //colString.DataType = System.Type.GetType("System.String");

        DataTable dt = new DataTable("DOCUMENTOS");
        dt.Columns.Add("Profesional", System.Type.GetType("System.String"));
        dt.Columns.Add("Tipo documento", System.Type.GetType("System.String"));
        dt.Columns.Add("Documento", System.Type.GetType("System.String"));
        dt.Columns.Add("Archivo", System.Type.GetType("System.String"));
        dt.Columns.Add("Error al obtener documento", System.Type.GetType("System.String"));
        if (bExpFA)
        {

            foreach (SUPER.BLL.Titulacion oElem in oListaFA)
            {
                if (oElem.BDOC)
                {//Si se ha podido recuperar el documento meto fila en el Excel
                    DataRow dr = dt.NewRow();
                    dr[0] = oElem.Profesional;
                    dr[1] = "Titulación académica";
                    dr[2] = oElem.T019_DESCRIPCION;
                    dr[3] = oElem.t2_iddocumento.ToString() + "_" + oElem.NDOC;
                    dr[4] = oElem.Estado;
                    dt.Rows.Add(dr);
                }
            }
        }
        if (bExpCurso)
        {
            foreach (SUPER.BLL.Curso oElem in oListaCurso)
            {
                if (oElem.BDOC)
                {//Si se ha podido recuperar el documento meto fila en el Excel
                    DataRow dr = dt.NewRow();
                    dr[0] = oElem.Profesional;
                    dr[1] = "Curso";
                    dr[2] = oElem.T574_TITULO;
                    dr[3] = oElem.t2_iddocumento.ToString() + "_" + oElem.T575_NDOC;
                    dr[4] = oElem.Estado;
                    dt.Rows.Add(dr);
                }
            }
        }
        if (bExpCert)
        {
            foreach (SUPER.BLL.Certificado oCert in oListaCert)
            {
                if (oCert.BDOC)
                {//Si se ha podido recuperar el documento meto fila en el Excel
                    DataRow dr = dt.NewRow();
                    dr[0] = oCert.Profesional;
                    dr[1] = "Certificado";
                    dr[2] = oCert.T582_NOMBRE;
                    dr[3] = oCert.t2_iddocumento.ToString() + "_" + oCert.T593_NDOC;
                    dr[4] = oCert.EstadoCertificado;
                    dt.Rows.Add(dr);
                }
            }
        }
        if (bExpTitidioma)
        {
            foreach (SUPER.BLL.Idioma oElem in oListaTitIdioma)
            {
                if (oElem.BDOC)
                {//Si se ha podido recuperar el documento meto fila en el Excel
                    DataRow dr = dt.NewRow();
                    dr[0] = oElem.Profesional;
                    dr[1] = "Título de idioma";
                    dr[2] = oElem.t021_titulo;
                    dr[3] = oElem.t2_iddocumento.ToString() + "_" + oElem.NDOC;
                    dr[4] = oElem.Estado;
                    dt.Rows.Add(dr);
                }
            }
        }
        DataSet ds = new DataSet();
        dt.DefaultView.Sort = "Profesional, [Tipo documento], Documento";
        dt = dt.DefaultView.ToTable();
        ds.Tables.Add(dt);
        ////////////////////////////
        int nIntentosTotal = 50, nIntentos=0;
        bool bOk=false;
        while(!bOk)
        {
            try
            {
               // byte[] aFichExcel = osvcExcel.getExcelFrom2DimObjectArray(aDatosExcel, ".xls");
                byte[] aFichExcel = osvcExcel.getExcelFromDataSet(ds, ".xls");
                sNomArchivoExcel = sDirectorio + "\\Cabecera.xls";
                System.IO.File.WriteAllBytes(sNomArchivoExcel, aFichExcel);
                bOk=true;
            }
            catch (Exception ex)
            {
                nIntentos++;
                string msg = ex.Message; if (ex.InnerException != null) msg += ". " + ex.InnerException.Message;
                miLog.Error("Error al intentar crear el archivo Excel el trackingId: " + trackingId + ". Intento nº: " + nIntentos.ToString() + ". Detalle: " + msg);

                if (nIntentos >= nIntentosTotal)
                {
                    miLog.Error("Se ha superado el número máximo de intentos para generar archivo Excel para el trackingId: " + trackingId + ". Detalle: " + msg);
                    throw new Exception("Error al generar archivo Excel."+ msg);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        #endregion

        return sNomArchivoZip + "@#@" + sNomArchivoExcel;
    }
    private static string CompressFile(string sDirectorio, string trackingId)
    {
        string finalPath = sDirectorio + "\\Documentos.zip";
        //try
        //{
            //DirectoryInfo directorySelected = new DirectoryInfo(sDirectorio);
            ZipFile zip = new ZipFile(finalPath);
            zip.AddDirectory(sDirectorio);
            zip.Save();

            return finalPath;
        //}
        //catch (Exception ex)
        //{
        //    throw new FaultException<IBOfficeException>(new IBOfficeException(107, "Error al comprimir la carpeta.", ex.Message));
        //}
    }
    private bool ListasIguales(ArrayList ListaGrande, ArrayList ListaReducida)
    {
        bool bIguales = true;

        foreach (string sElem in ListaGrande)
        {
            if (sElem != "")
            {
                if (!ListaReducida.Contains(sElem))
                {
                    bIguales = false;
                    break;
                }
            }
        }
        return bIguales;
    }
    #endregion

    /*
    #region Para pruebas sin pasar por los servicios
    private static DataTable GetDataTableFromListCod(JArray aLista)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("CODIGOINT", typeof(int)));

        foreach (JValue oValue in aLista)
        //foreach (Newtonsoft.Json.Linq.JObject oValue in aLista)
        {
            DataRow row = dt.NewRow();
            row["CODIGOINT"] = (int)oValue;
            dt.Rows.Add(row);
        }

        return dt;
    }
    private static DataTable GetDataTableFromListDen(JArray aLista)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("DENOMINACION", typeof(string)));

        foreach (JValue oValue in aLista)
        //foreach (Newtonsoft.Json.Linq.JObject oValue in aLista)
        {
            DataRow row = dt.NewRow();
            row["DENOMINACION"] = (string)oValue;
            dt.Rows.Add(row);
        }

        return dt;
    }
    private static DataTable GetDataTableIdiomaFromList(JArray aLista)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("t020_idcodidioma", typeof(int)));
        dt.Columns.Add(new DataColumn("t013_lectura", typeof(byte)));
        dt.Columns.Add(new DataColumn("t013_escritura", typeof(byte)));
        dt.Columns.Add(new DataColumn("t013_oral", typeof(byte)));
        dt.Columns.Add(new DataColumn("tieneTitulo", typeof(bool)));

        //foreach (JValue o in aLista)
        foreach (Newtonsoft.Json.Linq.JObject o in aLista)
        {
            DataRow row = dt.NewRow();
            if (o["id"].ToString() != "") row["t020_idcodidioma"] = (int)o["id"];
            if (o["lectura"].ToString() != "") row["t013_lectura"] = (byte)o["lectura"];
            if (o["escritura"].ToString() != "") row["t013_escritura"] = (byte)o["escritura"];
            if (o["oral"].ToString() != "") row["t013_oral"] = (byte)o["oral"];
            row["tieneTitulo"] = ((int)o["titulo"] == 1) ? true : false;

            dt.Rows.Add(row);
        }

        return dt;
    }
    private static DataTable GetDataTablePerfilEntornoFromList(JArray aLista)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("tipo", typeof(string)));
        dt.Columns.Add(new DataColumn("id_pri", typeof(int)));
        dt.Columns.Add(new DataColumn("id_sec", typeof(int)));
        //dt.Columns.Add(new DataColumn("unidad_tm", typeof(short)));
        //dt.Columns.Add(new DataColumn("tipo_tm", typeof(byte)));
        dt.Columns.Add(new DataColumn("unidad", typeof(short)));
        dt.Columns.Add(new DataColumn("cantidad", typeof(byte)));
        dt.Columns.Add(new DataColumn("anno", typeof(short)));
        dt.Columns.Add(new DataColumn("obligatorio", typeof(bool)));

        //foreach (JValue o in aLista)
        foreach (Newtonsoft.Json.Linq.JObject o in aLista)
        {
            DataRow row = dt.NewRow();
            row["tipo"] = o["tipo"];
            row["id_pri"] = o["id_pri"];
            if (o["id_sec"].ToString() != "") row["id_sec"] = (int)o["id_sec"];
            row["cantidad"] = (o["cantidad"].ToString() != "") ? (short)o["cantidad"] : 0;  //0
            row["unidad"] = (o["unidad"].ToString() != "") ? (byte)o["unidad"] : 3;  //1->años, 2->Meses, 3->días
            if (o["anno"].ToString() != "") row["anno"] = (short)o["anno"];
            row["obligatorio"] = ((int)o["obl"] == 1) ? true : false;

            dt.Rows.Add(row);
        }

        return dt;
    }
    private static DataSet ObtenerPlantillaCVC_Avanzada(SqlParameter[] aParam)
    {
        DataSet ds = null;
        ds = SqlHelper.ExecuteDataset("SUP_CVT_CONSAVANZADA_EXPORTARPLANT_CVC", aParam);
        int length = aParam.Length;
        int indice = 0;
        ds.Tables[indice++].TableName = "DatosPersonales";

        if ((bool)aParam[length - 8].Value)
            ds.Tables[indice++].TableName = "FormacionAcademica";
        if ((bool)aParam[length - 3].Value)
            ds.Tables[indice++].TableName = "Certificados";
        if ((bool)aParam[length - 6].Value || (bool)aParam[length - 7].Value)
        {
            ds.Tables[indice++].TableName = "Experiencia";
            ds.Tables[indice++].TableName = "PerfilExperiencia";
        }
        if ((bool)aParam[length - 2].Value)
            ds.Tables[indice++].TableName = "Idiomas";
        if ((bool)aParam[length - 5].Value)
            ds.Tables[indice++].TableName = "CursosRecibidos";
        if ((bool)aParam[length - 4].Value)
            ds.Tables[indice++].TableName = "CursosImpartidos";
        if ((bool)aParam[length - 1].Value)
            ds.Tables[indice++].TableName = "Examenes";

        return ds;
    }
    private void DistribuidorPlantilla(DataSet ds, string sExtension, int idPlantilla, Dictionary<string, string> htCampos,
                                    string trackingId, int nDocs)
    {
        bool bPeticionOnLine = true; //true: online, false: batch.
        Stream streamResultado = null;
        int nIntentos = 0;
        try
        {
            if (ds == null)
            {
                throw new Exception("No hay datos para generar la exportación para el trackingId: " + trackingId);
            }
            #region Creación de documentos en función de la plantilla seleccionada
            switch (idPlantilla)
            {
                case 1:
                    if (htCampos == null || htCampos.Count == 0)
                        //streamResultado = cvc.CargarPlantillaCVCompleto(ds, trackingId, sExtension, (nDocs == 0) ? true : false);
                        streamResultado = null;
                    else
                        CargarPlantillaCVC_CE(ds, htCampos, trackingId, sExtension, (nDocs == 0) ? true : false);
                    break;
                //case 2:
                //    PlantillaAT at = new PlantillaAT();
                //    streamResultado = at.CargarPlantillaAT(ds, wordApplication, sPathPlantillas, sPathDocumentos, trackingId, sExtension, (nDocs == 0) ? true : false);
                //    break;
                //case 3:
                //    PlantillaPPTR pptr = new PlantillaPPTR();
                //    streamResultado = pptr.CargarPlantillaPPTR(ds, wordApplication, sPathPlantillas, sPathDocumentos, trackingId, sExtension, (nDocs == 0) ? true : false);
                //    break;
                //case 4:
                //    PlantillaPPTC pptc = new PlantillaPPTC();
                //    streamResultado = pptc.CargarPlantillaPPTC(ds, wordApplication, sPathPlantillas, sPathDocumentos, trackingId, sExtension, (nDocs == 0) ? true : false);
                //    break;
                //case 5:
                //    PlantillaEP ep = new PlantillaEP();
                //    streamResultado = ep.CargarPlantillaEP(ds, wordApplication, sPathPlantillas, sPathDocumentos, trackingId, sExtension, (nDocs == 0) ? true : false);
                //    break;
            }
            #endregion
        }
        catch (Exception ex)
        {
            string msg = "TrackingId = " + trackingId + " - Error: " + ex.Message;
            if (ex.InnerException != null) msg += ". " + ex.InnerException.Message;
        }
        finally
        {
            //Liberar memoria del dataset.
            if (ds != null) ds.Dispose();
        }
    }

    public Stream kkgetPlantilla(string sListParam, string extension, int idPlantilla, string sCampos, byte bNDocs, string trackingId)
    {
        Stream result = null;
        DataSet data = null;
        Dictionary<string, string> htCampos = new Dictionary<string, string>();

        #region Cargar sqlParameter y hacer llamada a base de datos
        try
        {
            SqlParameter[] aParam = null;
            #region Consulta Avanzada
            //Se crea un objeto JSON con los parámetros recibidos en una cadena.
            JObject oCriterios = JObject.Parse(sListParam);

            #region Parámetros Consulta Avanzada
            aParam = new SqlParameter[]{
                    ParametroSql.add("@tbl_idficepi", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["idFicepis"])),
                    ParametroSql.add("@bfiltros", SqlDbType.Int, 4, (int)oCriterios["bfiltros"]),
                    //Titulación  
                    //(oCriterios[""].ToString() == "")? null: (?)oCriterios[""])),
                    ParametroSql.add("@t019_tipo", SqlDbType.TinyInt, 1, (oCriterios["tipologia"].ToString() == "")? null: (byte?)oCriterios["tipologia"]),
                    ParametroSql.add("@t019_tic", SqlDbType.Bit, 1, (oCriterios["tics"].ToString() == "")? null: (bool?)oCriterios["tics"]),
                    ParametroSql.add("@t019_modalidad", SqlDbType.TinyInt, 1, (oCriterios["modalidad"].ToString() == "")? null: (byte?)oCriterios["modalidad"]),
                    ParametroSql.add("@TMP_TITULO_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["titulo_obl_cod"])),
                    ParametroSql.add("@TMP_TITULO_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["titulo_obl_den"])),
                    ParametroSql.add("@TMP_TITULO_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["titulo_opc_cod"])),
                    ParametroSql.add("@TMP_TITULO_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["titulo_opc_den"])),
                    //Idiomas
                    ParametroSql.add("@TMP_IDIOMA_OBL_COD", SqlDbType.Structured, GetDataTableIdiomaFromList((JArray)oCriterios["idioma_obl_cod"])),
                    ParametroSql.add("@TMP_IDIOMA_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["idioma_obl_den"])),
                    ParametroSql.add("@TMP_IDIOMA_OPC_COD", SqlDbType.Structured, GetDataTableIdiomaFromList((JArray)oCriterios["idioma_opc_cod"])),
                    ParametroSql.add("@TMP_IDIOMA_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["idioma_opc_den"])),
                    //Formación
                    ParametroSql.add("@num_horas", SqlDbType.Int, 4, (oCriterios["num_horas"].ToString() == "")? null: (int?)oCriterios["num_horas"]),
                    ParametroSql.add("@anno", SqlDbType.SmallInt, 2, (oCriterios["anno"].ToString() == "")? null: (short?)oCriterios["anno"]),
                    ////Certificados
                    ParametroSql.add("@TMP_CERT_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["cert_obl_cod"])),
                    ParametroSql.add("@TMP_CERT_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["cert_obl_den"])),
                    ParametroSql.add("@TMP_CERT_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["cert_opc_cod"])),
                    ParametroSql.add("@TMP_CERT_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["cert_opc_den"])),
                    //Entidades Certificadoras
                    ParametroSql.add("@TMP_ENTCERT_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entcert_obl_cod"])),
                    ParametroSql.add("@TMP_ENTCERT_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entcert_obl_den"])),
                    ParametroSql.add("@TMP_ENTCERT_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entcert_opc_cod"])),
                    ParametroSql.add("@TMP_ENTCERT_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entcert_opc_den"])),
                    //Entornos tecnológicos
                    ParametroSql.add("@TMP_ENTFOR_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entfor_obl_cod"])),
                    ParametroSql.add("@TMP_ENTFOR_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entfor_obl_den"])),
                    ParametroSql.add("@TMP_ENTFOR_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["entfor_opc_cod"])),
                    ParametroSql.add("@TMP_ENTFOR_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["entfor_opc_den"])),
                    //Cursos
                    ParametroSql.add("@TMP_CURSO_OBL_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["curso_obl_cod"])),
                    ParametroSql.add("@TMP_CURSO_OBL_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["curso_obl_den"])),
                    ParametroSql.add("@TMP_CURSO_OPC_COD", SqlDbType.Structured, GetDataTableFromListCod((JArray)oCriterios["curso_opc_cod"])),
                    ParametroSql.add("@TMP_CURSO_OPC_DEN", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["curso_opc_den"])),
                    ////Experiencias profesionales
                    //Cliente / Sector
                    ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, oCriterios["cliente"].ToString()),
                    ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, (oCriterios["sector"].ToString() == "")? null: (int?)oCriterios["sector"]),
                    ParametroSql.add("@cantidad_expprof", SqlDbType.SmallInt, 2, (oCriterios["cantidad_expprof"].ToString() == "")? null: (short?)oCriterios["cantidad_expprof"]),
                    ParametroSql.add("@unidad_expprof", SqlDbType.TinyInt, 1, (oCriterios["unidad_expprof"].ToString() == "")? null: (byte?)oCriterios["unidad_expprof"]),
                    ParametroSql.add("@anno_expprof", SqlDbType.SmallInt, 2, (oCriterios["anno_expprof"].ToString() == "")? null: (short?)oCriterios["anno_expprof"]),
                    //Contenido de Experiencias / Funciones
                    ParametroSql.add("@TMP_EXPFUNCION", SqlDbType.Structured, GetDataTableFromListDen((JArray)oCriterios["term_expfun"])),
                    ParametroSql.add("@bOperadorLogico", SqlDbType.Char, 1, oCriterios["op_logico"].ToString()),
                    ParametroSql.add("@cantidad_expfun", SqlDbType.SmallInt, 2, (oCriterios["cantidad_expfun"].ToString() == "")? null: (short?)oCriterios["cantidad_expfun"]),
                    ParametroSql.add("@unidad_expfun", SqlDbType.TinyInt, 1, (oCriterios["unidad_expfun"].ToString() == "")? null: (byte?)oCriterios["unidad_expfun"]),
                    ParametroSql.add("@anno_expfun", SqlDbType.SmallInt, 2, (oCriterios["anno_expfun"].ToString() == "")? null: (short?)oCriterios["anno_expfun"]),
                    //Experiencia profesional Perfil
                    ParametroSql.add("@tbl_bus_perfil", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_perfil"])),
                    //Experiencia profesional Perfil / Entorno tecnológico
                    ParametroSql.add("@tbl_bus_perfil_entorno", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_perfil_entorno"])),
                    //Experiencia profesional Entorno tecnológico
                    ParametroSql.add("@tbl_bus_entorno", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_entorno"])),
                    //Experiencia profesional Entorno tecnológico / Perfil
                    ParametroSql.add("@tbl_bus_entorno_perfil", SqlDbType.Structured, GetDataTablePerfilEntornoFromList((JArray)oCriterios["tbl_bus_entorno_perfil"])),
                    //Checks
                    ParametroSql.add("@bSinopsis", SqlDbType.Bit, 1, (oCriterios["bSinopsis"].ToString() == "")? false: (bool?)oCriterios["bSinopsis"]),
                    ParametroSql.add("@bFA", SqlDbType.Bit, 1, (oCriterios["bFA"].ToString() == "") ? false : (bool?)oCriterios["bFA"]),
                    ParametroSql.add("@bEPI", SqlDbType.Bit, 1, (oCriterios["bEPI"].ToString() == "") ? false : (bool?)oCriterios["bEPI"]),
                    ParametroSql.add("@bEPF", SqlDbType.Bit, 1, (oCriterios["bEPF"].ToString() == "") ? false : (bool?)oCriterios["bEPF"]),
                    ParametroSql.add("@bCR", SqlDbType.Bit, 1, (oCriterios["bCR"].ToString() == "") ? false : (bool?)oCriterios["bCR"]),
                    ParametroSql.add("@bCI", SqlDbType.Bit, 1, (oCriterios["bCI"].ToString() == "") ? false : (bool?)oCriterios["bCI"]),
                    ParametroSql.add("@bCERT", SqlDbType.Bit, 1, (oCriterios["bCERT"].ToString() == "") ? false : (bool?)oCriterios["bCERT"]),
                    ParametroSql.add("@bID", SqlDbType.Bit, 1, (oCriterios["bID"].ToString() == "") ? false : (bool?)oCriterios["bID"]),
                    ParametroSql.add("@bEX", SqlDbType.Bit, 1, (oCriterios["bEX"].ToString() == "") ? false : (bool?)oCriterios["bEX"])
                };
            #endregion

            #region Llamada al procedimiento en función de la plantilla
            switch (idPlantilla)
            {
                case 1:
                    if (sCampos == "") //Micv
                    {
                        //data = Curriculum.ObtenerPlantillaCVCSF_Avanzada(aParam);
                    }
                    else//consultas
                    {
                        data = ObtenerPlantillaCVC_Avanzada(aParam);
                    }
                    break;
                //case 2:
                //    data = Curriculum.ObtenerPlantillaAT_Avanzada(aParam);
                //    break;
                //case 3:
                //    data = Curriculum.ObtenerPlantillaPPTR_Avanzada(aParam);
                //    if (isDebugEnabled) log.Debug(trackingId + " - Se han obtenido los datos de la plantilla PPTR en la consulta avanzada.");
                //    break;
                //case 4:
                //    data = Curriculum.ObtenerPlantillaPPTC_Avanzada(aParam);
                //    break;
                //case 5:
                //    data = Curriculum.ObtenerPlantillaEP_Avanzada(null, aParam);
                //    break;
            }
            #endregion
            #endregion
        }
        catch (Exception ex)
        {
            string msg = trackingId + " - " + ex.Message;
            if (ex.InnerException != null) msg += ". " + ex.InnerException.Message;

        }

        #endregion

        #region Simulación Llamada al servicio de office
        //result = osvcCVT.DistribuidorPlantilla(data, extension, idPlantilla, ((idPlantilla != 1) ? null : arrCampos), trackingId, int.Parse(bNDocs.ToString()));
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        if (sCampos != "")
            htCampos = serializer.Deserialize<Dictionary<string, string>>(sCampos);

        DistribuidorPlantilla(data, extension, idPlantilla, htCampos, trackingId, int.Parse(bNDocs.ToString()));

        #endregion

        //liberamos el dataset
        if (data != null)
            data.Dispose();

        return result;
    }

    #region CV Completo CON campos a excluir
    internal void CargarPlantillaCVC_CE(DataSet ds, Dictionary<string, string> htCampos, string trackingId, string sExtension, bool bDocUnico)
    {
        string profesional = "";
        ArrayList aDocs = new ArrayList();

        //Al guardar el de maniobra, si se han pedido múltiples archivos, hay que guardarlos con la 
        //extensión que se ha solicitado (.doc o .pdf), ya que al final se zipean y se envían.
        //Si se ha pedido un archivo único, se guardan en .docx para la creación del documento final.
        string sExtensionManiobra = (bDocUnico) ? ".docx" : sExtension;
        for (int i = 0; i < ds.Tables["DatosPersonales"].Rows.Count; i++)
        {
            #region Obtener el nombre del profesional
            try
            {
                profesional = ds.Tables["DatosPersonales"].Rows[i]["Profesional"].ToString();
            }
            catch (Exception ex)
            {
                string msg = ex.Message; if (ex.InnerException != null) msg += ". " + ex.InnerException.Message;
            }
            #endregion

            //Cargamos los datos de los diferentes bloques de la Plantilla CVC_CE
            CargarDatosCVCCE(ds, htCampos, i, trackingId, profesional, bDocUnico);
        }
        //liberar memoria en cuanto sea posible.
        if (ds != null) ds.Dispose();

    }

    private void CargarDatosCVCCE(DataSet ds, Dictionary<string, string> htCampos, 
                        int i, string trackingId, string profesional, bool bDocUnico)
    {
        #region Experiencias
        try
        {
            DataView dvEXP = new DataView();
            DataView dvEXPPER = new DataView();
            if (ds.Tables["Experiencia"] != null)
            {
                dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
                dvEXPPER = new DataView(ds.Tables["PerfilExperiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            }
            CargarExperienciasCVCCE(dvEXP, dvEXPPER, htCampos, trackingId);
            dvEXP.Dispose();
            dvEXPPER.Dispose();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            if (ex.InnerException != null) msg += ". " + ex.InnerException.Message;
        }
        #endregion
    }


    #region Experiencias profesionales CON campos a excluir
    /// <summary>
    /// dv-> Experiencias, dvP-> perfiles de las experiencias
    /// </summary>
    /// <param name="dv"></param>
    /// <param name="dvP"></param>
    /// <param name="htCampos"></param>
    /// <param name="wordApplication"></param>
    /// <param name="sPathDocumentos"></param>
    /// <param name="trackingId"></param>
    private void CargarExperienciasCVCCE(DataView dv, DataView dvP, Dictionary<string, string> htCampos,string trackingId)
    {
        //Si no hay registros o no está marcado el check de exportar Experiencias
        if (dv.Count == 0 || htCampos["EP"].ToString() == "0")
        {
            //rngE.Delete();
        }
        else
        {
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                DataView dvPerfil = new DataView(dvP.ToTable(), "t808_idexpprof= " + oFila["T808_IDEXPPROF"].ToString(), "FFIN, FINICIO", DataViewRowState.CurrentRows);

                if (oFila["TIPOEXP"].ToString() == "0")
                {
                    if (htCampos["EPF"].ToString() == "1")
                    {
                        ReemplazarDatosExperienciasFueraCVCCE(oFila, dvPerfil, i + 1, htCampos, trackingId);
                        i++;
                    }
                }
                else if (htCampos["EPI"].ToString() == "1")
                {
                    ReemplazarDatosExperienciasIberCVCCE(oFila, dvPerfil, i + 1, htCampos, trackingId);
                    i++;
                }
            }
        }
    }

    private void ReemplazarDatosExperienciasIberCVCCE(DataRowView oFila, DataView perfil, int nExp, Dictionary<string, string> htCampos,
                                                      string trackingId)
    {
        int iAux=0;
        if (htCampos["EPIfinicio"].ToString() == "0" && htCampos["EPIffin"].ToString() == "0")
        {
            //
        }
        else if (oFila["FINICIO"].ToString() != "" || oFila["FFIN"].ToString() != "")
        {
            if (htCampos["EPIfinicio"].ToString() == "0")
                iAux = 0;
                
        }
        if (htCampos["EPIdescripcion"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["T808_DESCRIPCION"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPIareacono"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["SECTTEC"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPIcliente"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["CLIENTE"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPIsectorc"].ToString() == "0" && htCampos["EPIsegmentoc"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["SECTORCLI"].ToString() != "" || oFila["SEGMENTOCLI"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPIempresa"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["EMPRESACON"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPIsectore"].ToString() == "0" && htCampos["EPIsegmentoe"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["SECTOREMP"].ToString() != "" || oFila["SEGMENTOEMP"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPIperfil"].ToString() == "1")
            cumplimentarPerfilesExperienciasIberCVCCE(perfil, nExp, htCampos, trackingId);
        else
        {
            iAux = 0;
        }
    }

    private void ReemplazarDatosExperienciasFueraCVCCE(DataRowView oFila, DataView perfil, int nExp, Dictionary<string, string> htCampos,
                                                       string trackingId)
    {
        int iAux = 0;
        if (htCampos["EPFfinicio"].ToString() == "0" && htCampos["EPFffin"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["FINICIO"].ToString() != "" || oFila["FFIN"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPFdescripcion"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["T808_DESCRIPCION"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPFareacono"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["SECTTEC"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPFcliente"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["CLIENTE"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPFsectorc"].ToString() == "0" && htCampos["EPFsegmentoc"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["SEGMENTOCLI"].ToString() != "" || oFila["SECTORCLI"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPFempresa"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["EMPRESACON"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPFsectore"].ToString() == "0" && htCampos["EPFsegmentoe"].ToString() == "0")
        {
            iAux = 0;
        }
        else if (oFila["SECTOREMP"].ToString() != "" || oFila["SEGMENTOEMP"].ToString() != "")
        {
            iAux = 0;
        }
        else
            iAux = 0;

        if (htCampos["EPFperfil"].ToString() == "1")
            cumplimentarPerfilesExperienciasFueraCVCCE(perfil, nExp, htCampos, trackingId);
        else
        {
            iAux = 0;
        }
    }

    private void cumplimentarPerfilesExperienciasIberCVCCE(DataView perfiles, int nExp, Dictionary<string, string> htCampos,
                                                           string trackingId)
    {
        int iAux = 0;
        if (perfiles.Count == 0)
        {
            iAux = 0;
        }
        else
        {
            int fila = 0;// rng_datos.Tables[nExp].Rows.Count;
            foreach (DataRowView oFila in perfiles)
            {
                //fila perfil
                fila++;

                //fila fechas
                if (htCampos["EPIPfinicio"].ToString() == "1" || htCampos["EPIPffi"].ToString() == "1")
                {
                    if (htCampos["EPIPfinicio"].ToString() == "1" && htCampos["EPIPffi"].ToString() == "1")
                    {
                        iAux = 0;
                    }
                    else if (htCampos["EPIPfinicio"].ToString() == "1")
                    {
                        iAux = 0;
                    }
                    else if (htCampos["EPIPffi"].ToString() == "1")
                    {
                        iAux = 0;
                    }
                    fila++;
                }
                //fila funciones
                if (htCampos["EPIfuncion"].ToString() == "1")
                {
                    fila++;
                }
                //fila áreas y tecnologías
                if (htCampos["EPIareatec"].ToString() == "1")
                {
                    fila++;
                }
            }
        }
    }

    private void cumplimentarPerfilesExperienciasFueraCVCCE(DataView perfiles, int nExp, Dictionary<string, string> htCampos,
                                                            string trackingId)
    {
        int iAux = 0;
        if (perfiles.Count == 0)
        {
            iAux = 0;
        }
        else
        {
            int fila = 0;// rng_datos.Tables[nExp].Rows.Count;
            //int nExp = 1;
            foreach (DataRowView oFila in perfiles)
            {
                //fila perfil
                fila++;

                //fila fechas
                if (htCampos["EPFPfinicio"].ToString() == "1" || htCampos["EPFPffi"].ToString() == "1")
                {
                    if (htCampos["EPFPfinicio"].ToString() == "1" && htCampos["EPFPffi"].ToString() == "1")
                    {
                        iAux = 0;
                    }
                    else if (htCampos["EPFPfinicio"].ToString() == "1")
                    {
                        iAux = 0;

                    }
                    else if (htCampos["EPFPffi"].ToString() == "1")
                    {
                        iAux = 0;

                    }
                    fila++;
                }
                //fila funciones
                if (htCampos["EPFfuncion"].ToString() == "1")
                {
                    fila++;
                }
                //fila áreas y tecnologías
                if (htCampos["EPFareatec"].ToString() == "1")
                {
                    fila++;
                }
            }
        }
    }

    #endregion

    #endregion
    #endregion
    */
}
