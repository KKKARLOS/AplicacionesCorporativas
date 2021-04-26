using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;
using SUPER.Capa_Negocio;
using System.Collections;
using System.ServiceModel;
using System.Threading;
using System.Text;

public partial class Capa_Presentacion_CVT_MiCV_formaExport_exportar : System.Web.UI.Page
{
    public string sErrores = "";
    public string sFileName = "";
    ReportDocument rdCurriculum = null;
    string prefijo = Constantes.sPrefijo;
    private string strTipoFormato = "";
    private string strNombreProfesionales = "";
    private string trackingId = "";
    private string strDestinatarios, strDestinatariosIdFicepi = "";
    private string currentPath = HttpContext.Current.Request.Path;
    private string ipAdress = HttpContext.Current.Request.UserHostAddress;
    private string usuario = "";
    private string codred = "";
    private string nombreDoc = "";
    private string pathDirectory = ConfigurationManager.AppSettings["pathGuardarCVT"].ToString();


    private void Page_Load(object sender, System.EventArgs e)
    {
        usuario = HttpContext.Current.Session["APELLIDO1"].ToString() + " " + HttpContext.Current.Session["APELLIDO2"].ToString() + ", " + HttpContext.Current.Session["NOMBRE"].ToString();
        codred = HttpContext.Current.Session["IDRED"].ToString();

        // Put user code to initialize the page here
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

    }

    #region Web Form Designer generated code
 
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);

        try
        {
            int int_pinicio;
            int int_pfin;
            string strServer;
            string strDataBase;
            string strUid;
            string strPwd;
            //				obtengo de la cadena de conexión los parámetros para luego
            //				modificar localizaciones 

            #region Conexion BBDD

            string strconexion = Utilidades.CadenaConexion;
            int_pfin = strconexion.IndexOf(";database=", 0);

            strServer = strconexion.Substring(7, int_pfin - 7);

            int_pinicio = int_pfin + 10;
            int_pfin = strconexion.IndexOf(";uid=", int_pinicio);
            strDataBase = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";pwd=", int_pinicio);
            strUid = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";Trusted_Connection=", int_pinicio);
            strPwd = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            #endregion

            #region Objeto ReportDocument

            //creo un objeto ReportDocument
            //ReportDocument rdCurriculum = new ReportDocument();
            rdCurriculum = new ReportDocument();

            try
            {
                    rdCurriculum.Load(Server.MapPath(".") + @"\..\Reports\cvt_curriculum.rpt");
            }
            catch (Exception ex)
            {
                rdCurriculum.Close();
                rdCurriculum.Dispose();
                Response.Write("Error al abrir el report: " + ex.Message);
            }

            try
            {
                rdCurriculum.SetDatabaseLogon(strUid, strPwd);
            }
            catch (Exception ex)
            {
                rdCurriculum.Close();
                rdCurriculum.Dispose();
                Response.Write("Error al logarse al report: " + ex.Message);
            }
            #endregion

            #region Objeto Logon
            //creo un objeto logon .

            CrystalDecisions.Shared.TableLogOnInfo tliCurrent;

            try
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table tbCurrent in rdCurriculum.Database.Tables)
                {

                    //obtengo el logon por tabla
                    tliCurrent = tbCurrent.LogOnInfo;

                    tliCurrent.ConnectionInfo.DatabaseName = strDataBase;
                    tliCurrent.ConnectionInfo.UserID = strUid;
                    tliCurrent.ConnectionInfo.Password = strPwd;
                    tliCurrent.ConnectionInfo.ServerName = strServer;

                    //aplico los cambios hechos al objeto TableLogonInfo
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                }
            }
            catch (Exception ex)
            {
                rdCurriculum.Close();
                rdCurriculum.Dispose();

                Response.Write("Error al actualizar la localización: " + ex.Message);
            }

            #endregion

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();

            #region Parametros Report

            string strFiltros = (Request.Form["hdnFiltros"] == null) ? Request.Form["ctl00$CPHC$hdnFiltros"] : Request.Form["hdnFiltros"];
            
            string[] sFiltros = Regex.Split(strFiltros,"{filtro}");

            string[] aDesglose = Regex.Split(sFiltros[1], "{valor}");//Desglose (DO,FOR,FACAD,CUR,CURIMP,CERT,IDI,EXP,ENIBER,FUERA)

            string[] aDesgloseFACAD = Regex.Split(sFiltros[20], "{valor}");//Desglose FORMACION ACADEMICA (Tipo, Modalidad, Tic, FInicio, FFIn)

            string[] aDesgloseIdioma = Regex.Split(sFiltros[21], "{valor}");//Desglose IDIOMAS (Lectura, Escritura, Oral, Titulo, F.Obtencion)

            string[] aDesgloseDO = Regex.Split(sFiltros[22], "{valor}"); //Desglose Datos Organizativos (Empresa, Unidad de negocio, CR, Antifuedad, Rol, Perfil, Oficina, Provincia, Pais, Trayectoria, Movilidad, Observaciones)

            string[] aDesgloseCURREC = Regex.Split(sFiltros[23], "{valor}"); //Desglose CURSOS RECIBIDOS (Proveedor, Entorno tecnologico, Provincia, Horas, F.Inicio, F. Fin)

            string[] aDesgloseCERTEXAM = Regex.Split(sFiltros[24], "{valor}"); //Desglose Certificados (Proveedor, Entorno tecnologico, F.Obtencion, Tipo)

            string[] aDesgloseEXPFUERA = Regex.Split(sFiltros[25], "{valor}"); //Desglose EXPeriencia FUERA de Ibermatica (Empresa Origen, Cliente, Funcion, F Inicio, F Fin, Descripcion, ACS-ACT, Sector, Segmento, Perfil, Entorno, Nº mes) 

            string[] aDesgloseEXPIBER = Regex.Split(sFiltros[26], "{valor}"); //Desglose EXPeriencia en IBERmatica(Cliente, Funcion, F Inicio, F Fin, Descripcion, ACS-ACT, Sector, Segmento, Perfil, Entorno, Nº mes) 

            string[] aDesgloseDP = Regex.Split(sFiltros[27], "{valor}"); //Desglose Datos Personales(Foto, NIF, F Nacimiento, Nacionalidad, Sexo)

            string[] aDesgloseCURIMP = Regex.Split(sFiltros[28], "{valor}"); //Desglose CURSOS IMPARTIDOS (Proveedor, Entorno tecnologico, Provincia, Horas, F.Inicio, F. Fin)


            strTipoFormato = sFiltros[18];//Tipo formato
            rdCurriculum.SetParameterValue("formato_expiber", strTipoFormato);//Tipo de formato
            rdCurriculum.SetParameterValue("formato_expfuera", strTipoFormato);//Tipo de formato

            try
            {
                rdCurriculum.SetParameterValue("@t001_idficepi", sFiltros[0]);

                //Si es un Encargado de CV o un Consultor de CV en el impreso figurará el Perfil de Mercado
                if (User.IsInRole("ECV") || User.IsInRole("EVAL") || User.IsInRole("VSN4") || User.IsInRole("VSN3") || User.IsInRole("VSN2") || User.IsInRole("VSN1") || User.IsInRole("VN"))
                    rdCurriculum.SetParameterValue("EsSoloUsuario", "N");
                else
                    rdCurriculum.SetParameterValue("EsSoloUsuario", "S");


                //FORMACION

                //FormacionAcad
                rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "FormacionAcad");//Indica si hay que filtrar o no
                rdCurriculum.SetParameterValue("@t019_idcodtitulo", (sFiltros[4] == "") ? 0 : int.Parse(sFiltros[4]), "FormacionAcad");//IdcodTitulo 
                rdCurriculum.SetParameterValue("@t019_descripcion", sFiltros[3], "FormacionAcad");//Titulacion
                rdCurriculum.SetParameterValue("@t019_tipo", (sFiltros[5] == "") ? 0 : int.Parse(sFiltros[5]), "FormacionAcad");//Tipologia
                rdCurriculum.SetParameterValue("@t019_modalidad", (sFiltros[6] == "") ? 0 : int.Parse(sFiltros[6]), "FormacionAcad");//Modalidad
                rdCurriculum.SetParameterValue("@t019_tic", (sFiltros[7] == "") ? 3 : int.Parse(sFiltros[7]), "FormacionAcad");//Tic

                //CurRecibidos
                rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "CurRecibidos");//Indica si hay que filtrar o no
                rdCurriculum.SetParameterValue("@lft036_idcodentorno", sFiltros[10], "CurRecibidos");//Lista EntornoTecnologicos Formacion

                //CurImpartidos
                rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "CurImpartidos");//Indica si hay que filtrar o no
                rdCurriculum.SetParameterValue("@lft036_idcodentorno", sFiltros[10], "CurImpartidos");//Lista EntornoTecnologicos Formacion

                //CertExam
                rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "CertExam");//Indica si hay que filtrar o no
                rdCurriculum.SetParameterValue("@t582_nombre", sFiltros[8], "CertExam");//certificado
                rdCurriculum.SetParameterValue("@t582_idcertificado", (sFiltros[9] == "") ? 0 : int.Parse(sFiltros[9]), "CertExam");//IdCertificado
                rdCurriculum.SetParameterValue("@lft036_idcodentorno", sFiltros[10], "CertExam");//Lista EntornoTecnologicos Formacion
                rdCurriculum.SetParameterValue("@origenConsulta", (sFiltros[19] == "") ? 1 : int.Parse(sFiltros[19]), "CertExam");//origenConsulta 0 MiCV 1 Consultas

                //Idiomas
                rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "Idiomas");//Indica si hay que filtrar o no
                rdCurriculum.SetParameterValue("@t020_idcodidioma", (sFiltros[11] == "") ? 0 : int.Parse(sFiltros[11]), "Idiomas");//IdIdioma
                rdCurriculum.SetParameterValue("@nivelidioma", (sFiltros[12] == "") ? 0 : int.Parse(sFiltros[12]), "Idiomas");//NivelIdioma


                //EXPERIENCIA PROFESIONAL
                //ExpFuera
                rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "ExpFuera");//Indica si hay que filtrar o no
                rdCurriculum.SetParameterValue("@nombrecuenta", sFiltros[13], "ExpFuera");//Clientes/Cuentas
                rdCurriculum.SetParameterValue("@idcuenta", (sFiltros[14] == "") ? 0 : int.Parse(sFiltros[14]), "ExpFuera");//Id Clientes/Cuentas
                rdCurriculum.SetParameterValue("@t483_idsector", (sFiltros[15] == "") ? 0 : int.Parse(sFiltros[15]), "ExpFuera");//IdSector
                rdCurriculum.SetParameterValue("@t035_codperfile", (sFiltros[16] == "") ? 0 : int.Parse(sFiltros[16]), "ExpFuera");//IdPerfil
                rdCurriculum.SetParameterValue("@let036_idcodentorno", sFiltros[17], "ExpFuera");//Lista EntornoTecnologicos Experiencia


                //ExpIber
                rdCurriculum.SetParameterValue("@bfiltros", int.Parse(sFiltros[2]), "ExpIber");//Indica si hay que filtrar o no
                rdCurriculum.SetParameterValue("@nombrecuenta", sFiltros[13], "ExpIber");//Clientes/Cuentas
                rdCurriculum.SetParameterValue("@idcuenta", (sFiltros[14] == "") ? 0 : int.Parse(sFiltros[14]), "ExpIber");//Id Clientes/Cuentas
                rdCurriculum.SetParameterValue("@t483_idsector", (sFiltros[15] == "") ? 0 : int.Parse(sFiltros[15]), "ExpIber");//IdSector
                rdCurriculum.SetParameterValue("@t035_codperfile", (sFiltros[16] == "") ? 0 : int.Parse(sFiltros[16]), "ExpIber");//IdPerfil
                rdCurriculum.SetParameterValue("@let036_idcodentorno", sFiltros[17], "ExpIber");//Lista EntornoTecnologicos Experiencia

                rdCurriculum.SetParameterValue("visible_dorganizativos", aDesglose[0]);
                rdCurriculum.SetParameterValue("visible_formacion", aDesglose[1]);//Formacion(Titulo)
                rdCurriculum.SetParameterValue("visible_formacad", aDesglose[2]);//Formacion Academica (SubReport)
                rdCurriculum.SetParameterValue("visible_currec", aDesglose[3]);//Acciones formativas recibidas (SubReport)
                rdCurriculum.SetParameterValue("visible_curimp", aDesglose[4]);//Acciones formativas impartidas (SubReport)
                rdCurriculum.SetParameterValue("visible_certexam", aDesglose[5]);//Certificados Examenes (SubReport)
                rdCurriculum.SetParameterValue("visible_idiomas", aDesglose[6]);//Idiomas (SubReport)
                rdCurriculum.SetParameterValue("visible_experiencia", aDesglose[7]);//Experiencia profesional (Titulo)
                rdCurriculum.SetParameterValue("visible_expfuera", aDesglose[8]);//Experiencia en Ibermatica (SubReport)
                rdCurriculum.SetParameterValue("visible_expiber", aDesglose[9]);//Experiencia fuera de Ibermatica (SubReport)
                
                //SINOPSIS
                
                rdCurriculum.SetParameterValue("visible_sinopsis", aDesglose[10]);//Sinopsis(Titulo)

                //DESGLOSE FORMACION ACADEMICA
                rdCurriculum.SetParameterValue("visible_FACADTipo", aDesgloseFACAD[0]);//Formacion Academica Tipo (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_FACADModalidad", aDesgloseFACAD[1]);//Formacion Academica Modalidad (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_FACADTic", aDesgloseFACAD[2]);//Formacion Academica Tic (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_FACADFInicio", aDesgloseFACAD[3]);//Formacion Academica FInicio (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_FACADFFin", aDesgloseFACAD[4]);//Formacion Academica FFin (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_FACADEspecialidad", aDesgloseFACAD[5]);//Formacion Academica Especialidad (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_FACADCentro", aDesgloseFACAD[6]);//Formacion Academica Centro (Dato SubReport)

                //DESGLOSE IDIOMAS
                rdCurriculum.SetParameterValue("visible_IdiomaLectura", aDesgloseIdioma[0]);//Idioma Lectura (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_IdiomaEscritura", aDesgloseIdioma[1]);//Idioma Escritura (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_IdiomaOral", aDesgloseIdioma[2]);//Idioma Oral (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_IdiomaTitulo", aDesgloseIdioma[3]);//Idioma Titulo (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_IdiomaFObtencion", aDesgloseIdioma[4]);//Idioma FObtencion (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_IdiomaCentro", aDesgloseIdioma[5]);//Idioma Centro (Dato SubReport)

                //DESGLOSE Datos Organizativos
                rdCurriculum.SetParameterValue("visible_DOEmpresa", aDesgloseDO[0]);//Datos organizativos Empresa
                rdCurriculum.SetParameterValue("visible_DOUnidadNegocio", aDesgloseDO[1]);//Datos organizativos Unidad de negocio
                rdCurriculum.SetParameterValue("visible_DOCR", aDesgloseDO[2]);//Datos organizativos CR
                rdCurriculum.SetParameterValue("visible_DOFAntiguedad", aDesgloseDO[3]);//Datos organizativos F.Antiguedad
                rdCurriculum.SetParameterValue("visible_DORol", aDesgloseDO[4]);//Datos organizativos Rol
                rdCurriculum.SetParameterValue("visible_DOPerfil", aDesgloseDO[5]);//Datos organizativos Perfil
                rdCurriculum.SetParameterValue("visible_DOOficina", aDesgloseDO[6]);//Datos organizativos Oficina
                rdCurriculum.SetParameterValue("visible_DOProvincia", aDesgloseDO[7]);//Datos organizativos Provincia
                rdCurriculum.SetParameterValue("visible_DOPais", aDesgloseDO[8]);//Datos organizativos Pais
                rdCurriculum.SetParameterValue("visible_DOTrayectoria", aDesgloseDO[9]);//Datos organizativos Trayectoria
                rdCurriculum.SetParameterValue("visible_DOMovilidad", aDesgloseDO[10]);//Datos organizativos Movilidad
                rdCurriculum.SetParameterValue("visible_DOObservaciones", aDesgloseDO[11]);//Datos organizativos Observaciones

                //DESGLOSE CURsos RECibidos
                rdCurriculum.SetParameterValue("visible_CURRECProveedor", aDesgloseCURREC[0]);//Cursos Recibidos Proveedor (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECEntornoTecno", aDesgloseCURREC[1]);//Cursos Recibidos Entorno tecnologico (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECProvincia", aDesgloseCURREC[2]);//Cursos Recibidos Provincia (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECHoras", aDesgloseCURREC[3]);//Cursos Recibidos Horas (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECFInicio", aDesgloseCURREC[4]);//Cursos Recibidos F. Inicio(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECFFin", aDesgloseCURREC[5]);//Cursos Recibidos F. Fin (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECTipo", aDesgloseCURREC[6]);//Cursos Recibidos Tipo (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECModalidad", aDesgloseCURREC[7]);//Cursos Recibidos Modalidad (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURRECContenido", aDesgloseCURREC[8]);//Cursos Recibidos Contenido (Dato SubReport)

                //DESGLOSE Certificados
                rdCurriculum.SetParameterValue("visible_CERTEXAMProveedor", aDesgloseCERTEXAM[0]);//Certificados Proveedor (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CERTEXAMEntornoTecno", aDesgloseCERTEXAM[1]);//Certificados Entorno tecnologico (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CERTEXAMFObtencion", aDesgloseCERTEXAM[2]);//Certificados F. Obtencion(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CERTEXAMTipo", aDesgloseCERTEXAM[3]);//Certificados Tipo (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CERTEXAMFCaducidad", aDesgloseCERTEXAM[4]);//Certificados F. Caducidad (Dato SubReport)

                //DESGLOSE EXPERIENCIA FUERA
                rdCurriculum.SetParameterValue("visible_EXPFUERAEmpresaOri", aDesgloseEXPFUERA[0]);//Experiencia Fuera Empresa Origen (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERACliente", aDesgloseEXPFUERA[1]);//Experiencia Fuera Cliente(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERAFuncion", aDesgloseEXPFUERA[2]);//Experiencia Fuera Funcion(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERAFinicio", aDesgloseEXPFUERA[3]);//Experiencia Fuera Fecha Inicio(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERAFFin", aDesgloseEXPFUERA[4]);//Experiencia Fuera Fecha Fin(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERADescripcion", aDesgloseEXPFUERA[5]);//Experiencia Fuera Descripcion(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERAACSACT", aDesgloseEXPFUERA[6]);//Experiencia Fuera ACS-ACT(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERASector", aDesgloseEXPFUERA[7]);//Experiencia Fuera Sector(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERASegmento", aDesgloseEXPFUERA[8]);//Experiencia Fuera Segmento(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERAPerfil", aDesgloseEXPFUERA[9]);//Experiencia Fuera Perfil(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERAEntorno", aDesgloseEXPFUERA[10]);//Experiencia Fuera Entorno(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPFUERANmes", aDesgloseEXPFUERA[11]);//Experiencia Fuera Nº Mes(Dato SubReport)

                //DESGLOSE EXPERIENCIA IBERMATICA
                rdCurriculum.SetParameterValue("visible_EXPIBERCliente", aDesgloseEXPIBER[0]);//Experiencia Ibermatica Cliente(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERFuncion", aDesgloseEXPIBER[1]);//Experiencia Ibermatica Funcion(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERFinicio", aDesgloseEXPIBER[2]);//Experiencia Ibermatica Fecha Inicio(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERFFin", aDesgloseEXPIBER[3]);//Experiencia Ibermatica Fecha Fin(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERDescripcion", aDesgloseEXPIBER[4]);//Experiencia Ibermatica Descripcion(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERACSACT", aDesgloseEXPIBER[5]);//Experiencia Ibermatica ACS-ACT(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERSector", aDesgloseEXPIBER[6]);//Experiencia Ibermatica Sector(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERSegmento", aDesgloseEXPIBER[7]);//Experiencia Ibermatica Segmento(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERPerfil", aDesgloseEXPIBER[8]);//Experiencia Ibermatica Perfil(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBEREntorno", aDesgloseEXPIBER[9]);//Experiencia Ibermatica Entorno(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_EXPIBERNmes", aDesgloseEXPIBER[10]);//Experiencia Ibermatica Nº Mes(Dato SubReport)

                //DESGLOSE DATOS PERSONALES
                rdCurriculum.SetParameterValue("visible_DPFoto", aDesgloseDP[0]);//Datos Personales FOTO
                rdCurriculum.SetParameterValue("visible_DPNIF", aDesgloseDP[1]);//Datos Personales NIF
                rdCurriculum.SetParameterValue("visible_DPFNacimiento", aDesgloseDP[2]);//Datos Personales F Nacimiento
                rdCurriculum.SetParameterValue("visible_DPNacionalidad", aDesgloseDP[3]);//Datos Personales Nacionalidad
                rdCurriculum.SetParameterValue("visible_DPSexo", aDesgloseDP[4]);//Datos Personales Sexo

                //SINOPSIS


                //DESGLOSE CURsos IMPartidos
                rdCurriculum.SetParameterValue("visible_CURIMPProveedor", aDesgloseCURIMP[0]);//Cursos Impartidos Proveedor (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPEntornoTecno", aDesgloseCURIMP[1]);//Cursos Impartidos Entorno tecnologico (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPProvincia", aDesgloseCURIMP[2]);//Cursos Impartidos Provincia (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPHoras", aDesgloseCURIMP[3]);//Cursos Impartidos Horas (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPFInicio", aDesgloseCURIMP[4]);//Cursos Impartidos F. Inicio(Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPFFin", aDesgloseCURIMP[5]);//Cursos Impartidos F. Fin (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPTipo", aDesgloseCURIMP[6]);//Cursos Impartidos Tipo (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPModalidad", aDesgloseCURIMP[7]);//Cursos Impartidos Modalidad (Dato SubReport)
                rdCurriculum.SetParameterValue("visible_CURIMPContenido", aDesgloseCURIMP[8]);//Cursos Impartidos Contenido (Dato SubReport)
            }
            catch (Exception ex)
            {
                rdCurriculum.Close();
                rdCurriculum.Dispose();
                Response.Write("Error al actualizar los parámetros del report: " + ex.Message);
            }

            #endregion

            strNombreProfesionales = Request.Form[prefijo + "hdnNombreProfesionales"];
            trackingId = Request.Form[prefijo + "hdnTrackingId"];
            nombreDoc = trackingId;
            strDestinatarios = Request.Form[prefijo + "hdnDestinatarios"];
            strDestinatariosIdFicepi = Request.Form[prefijo + "hdnDestinatarioIdFicepi"];

            if (Request.Form[prefijo + "rdbTipoExp"] == "0") //Petición On line
            {
                #region Exportacion

                try
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    System.IO.Stream oStream;
                    byte[] byteArray = null;

                    switch (strTipoFormato)
                    {
                        case "PDF":
                            Response.AddHeader("content-type", "application/pdf; charset=utf-8");
                            Response.AddHeader("Content-Disposition", "attachment;filename=\"Curriculum.pdf\"");
                            //			PDF			
                            oStream = rdCurriculum.ExportToStream(ExportFormatType.PortableDocFormat);
                            byteArray = new byte[oStream.Length];
                            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                            break;
                        case "WORD":
                            Response.AddHeader("content-type", "application/msword; charset=utf-8");
                            Response.AddHeader("Content-Disposition", "attachment; filename=\"Curriculum.doc\"");
                            //			WORD (Para que el fichero sea editable y los textos se recoloquen usamos el RTF)
                            //rdCurriculum.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, false, "Exportacion");

                            //rdCurriculum.ExportToHttpResponse(ExportFormatType.EditableRTF, Response, false, "Exportacion");
                            //Response.End();

                            //string sFileName = @"D:\tmp\wordresult\" + Guid.NewGuid().ToString() + ".doc";
                            sFileName = Request.PhysicalApplicationPath + "TempImagesGraficos\\" + Guid.NewGuid().ToString() + ".doc";
                            rdCurriculum.ExportToDisk(ExportFormatType.EditableRTF, sFileName);
                            byteArray = System.IO.File.ReadAllBytes(sFileName.ToString());
                            break;
                        case "EXC":
                            rdCurriculum.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Exportacion");
                            break;
                    }

                    if (Request.QueryString["descargaToken"] != null)
                        Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field
                    Response.BinaryWrite(byteArray);

                    Response.Flush();
                    //Response.Close();
                    //Response.End();
                }
                catch (Exception ex)
                {
                    rdCurriculum.Close();
                    rdCurriculum.Dispose();
                    Response.Write("Error al exportar el report: " + ex.Message);
                }
                finally
                {
                    rdCurriculum.Close();
                    rdCurriculum.Dispose();
                }

                #endregion
            }
            else // Correo
            {
                enviarCorreoConfPedido();
                ThreadStart ts = new ThreadStart(GenerarCorreoCV);
                Thread workerThread = new Thread(ts);
                workerThread.Start();
            }
        }
        //handle any exceptions
        catch (Exception ex)
        {

            Response.Write("No se puede crear el report: " + ex.Message);
        }
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Load += new System.EventHandler(this.Page_Load);
    }
    
    private void GenerarCorreoCV()
    {
        try
        {
            string extension = "";

            byte[] byteArray = null;
            sFileName = Request.PhysicalApplicationPath + "TempImagesGraficos\\" + nombreDoc + ((strTipoFormato=="PDF")? ".pdf":".doc");
            switch (strTipoFormato)
            {
                case "PDF":
                    extension = ".pdf";
                    rdCurriculum.ExportToDisk(ExportFormatType.PortableDocFormat, sFileName);
                    byteArray = System.IO.File.ReadAllBytes(sFileName.ToString());
                    break;
                case "WORD":
                    extension = ".doc";
                    rdCurriculum.ExportToDisk(ExportFormatType.EditableRTF, sFileName);
                    byteArray = System.IO.File.ReadAllBytes(sFileName.ToString());
                    break;
            }

            if ((File.OpenRead(sFileName).Length / 1048576) > 10) //miramos si el archivo es mayor que 10MB
                enviarPackExpress(extension);
            else
                enviarCorreo(extension);
        }
        catch (Exception ex)
        {
            enviarCorreoErrorEDA("Error al exportar", ex);
            enviarCorreoErrorUsuario();
            sErrores += Errores.mostrarError("Error al exportar", ex);
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
            
            //oPaq.CrearPaqueteCV(new FileInfo(nombreDoc + extension).Name, strb.ToString(), File.OpenRead(pathDirectory + trackingId + @"\" + nombreDoc + extension));
            oPaq.CrearPaqueteCV(new FileInfo(sFileName).Name, strb.ToString(), File.OpenRead(sFileName));
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
        strCabecera = @"<br><br><br> <u class='TITULO'>Relación de profesionales</u>";
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
                
                //string[] aMail = { strAsunto, strMensaje, strTO, pathDirectory + trackingId + @"\" + nombreDoc + extension };
                string[] aMail = { strAsunto, strMensaje, strTO, sFileName };
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

    #endregion

    ~Capa_Presentacion_CVT_MiCV_formaExport_exportar()
    {
        // Simply call Dispose(false).
        this.Dispose();
    }
}
