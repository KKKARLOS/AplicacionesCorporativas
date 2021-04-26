using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SUPER.BLL;
using System.ServiceModel;
using SUPER.Capa_Negocio;
using System.Xml;
using System.Collections;
using System.Threading;
using System.Text;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using log4net;
//Para pruebas
using SUPER.Capa_Datos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class Capa_Presentacion_CVT_MiCV_formaExport_exportarCVExcel : System.Web.UI.Page
{
    public string sErrores = "";
    public string sFileName = "";
    string prefijo = Constantes.sPrefijo;
    private string strNombreProfesionales = "";
    private string trackingId = "";
    private string strDestinatarios, strDestinatariosIdFicepi = "";
    private string currentPath = HttpContext.Current.Request.Path;
    private string ipAdress = HttpContext.Current.Request.UserHostAddress;
    private string usuario = "", codred = "", nombreDoc = "";
    private string pathDirectory = ConfigurationManager.AppSettings["pathGuardarCVT"].ToString();
    private string sListaFiltros = "";
    private string sListaProfSeleccionados = "";
    private string gsTipoPantalla = "";
    Dictionary<string, string> htFiltros = null;
    Dictionary<string, string> htCampos = null;
    private string sExtension = ".xlsx";
    //Indica si la consulta se ha realizado con la búsqueda Avanzada o Básica
    private bool bBusAvan = false;
    private string gsFiltros = "", gsCampos="";

    protected void Page_Load(object sender, EventArgs e)
    {
        //SUPER.DAL.Log.Insertar("Entro en exportarCVExcel");
        ILog miLog = LogManager.GetLogger("SUP");
        log4net.Config.XmlConfigurator.Configure();

        //Si la exportación es on-line llamo directamente al servicio en IBServiOffice
        //Si es en diferido, hay que pasar por IBServices para que el hilo no muera al caducar la sesión

        //Acceso a IBServiOffice
        svcCVT.IsvcCVTClient osvcExcel = null;
        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            usuario = HttpContext.Current.Session["APELLIDO1"].ToString() + " " + HttpContext.Current.Session["APELLIDO2"].ToString() + ", " + HttpContext.Current.Session["NOMBRE"].ToString();
            codred = HttpContext.Current.Session["IDRED_ENTRADA"].ToString();

            miLog.Debug(codred + " -> Inicio exportación Excel.");
            //SUPER.DAL.Log.Insertar("Inicio exportación Excel.");

            htFiltros = new Dictionary<string, string>();
            htCampos = new Dictionary<string, string>();

            //Recojo la pestña con la que se ha generado la lista de profesionales
            if (Request.QueryString["pest"] != null)
            {
                switch (Request.QueryString["pest"].ToString()) 
                {
                    case "1":
                        gsTipoPantalla = "BASICA";
                        break;
                    case "2":
                        gsTipoPantalla = "AVANZADA";
                        break;
                    case "3":
                        gsTipoPantalla = "CADENA";
                        break;
                    case "4":
                        gsTipoPantalla = "QUERY";
                        break;
                }
            }
            //Cargo los filtros en función de la pestaña desde la que se ha realizado la consulta
            if (Request.QueryString["Avan"] != null)
            {
                if (Request.QueryString["Avan"].ToString() == "AVAN")
                    bBusAvan = true;
            }
            if (bBusAvan)
            {
                #region Filtros búsqueda avanzada
                /*
                htFiltros.Add("idFicepi", Request.Form[prefijo + "hdnIdFicepis"]);
                htFiltros.Add("bFiltros", ((Request.Form[prefijo + "chkExFS"] == "on") ? "1" : "0"));

                htFiltros.Add("t019_descripcion", (Request.Form[prefijo + "hdnTitulo"]));
                htFiltros.Add("t019_idcodtitulo", (Request.Form[prefijo + "hdnIdTitulo"]));
                htFiltros.Add("t019_tipo", (Request.Form[prefijo + "cboTipologia"]));
                htFiltros.Add("t019_tic", (Request.Form[prefijo + "cboTics"]));
                htFiltros.Add("t019_modalidad", (Request.Form[prefijo + "cboModalidad"]));
                htFiltros.Add("t582_idcertificado", (Request.Form[prefijo + "hdnIdCertificado"]));

                htFiltros.Add("t582_nombre", (Request.Form[prefijo + "hdnCertificacion"]));
                htFiltros.Add("lft036_idcodentorno", (Request.Form[prefijo + "hdnIdEntornoFormacion"]));
                htFiltros.Add("nombrecuenta", (Request.Form[prefijo + "hdnCuenta"]));
                htFiltros.Add("idcuenta", (Request.Form[prefijo + "hdnIdCliente"]));
                htFiltros.Add("t483_idsector", (Request.Form[prefijo + "cboSector"]));
                htFiltros.Add("t035_codperfile", (Request.Form[prefijo + "cboPerfilExp"]));
                htFiltros.Add("let036_idcodentorno", (Request.Form[prefijo + "hdnIdEntornoExp"]));
                htFiltros.Add("t020_idcodidioma", (Request.Form[prefijo + "cboIdioma"]));
                htFiltros.Add("nivelidioma", (Request.Form[prefijo + "cboNivel"]));
                htFiltros.Add("bTitulo", ((Request.Form[prefijo + "chkTituloAcre"] == "on") ? "1" : "0"));
                 * */
                sListaProfSeleccionados = Request.Form[prefijo + "hdnIdFicepis"];
                //sListaFiltros = Request.Form[prefijo + "hdnCriteriosBusquedaAvanzada"];
                sListaFiltros = Request.Form[prefijo + "hdnCriterios"];
                //JObject oCriterios = JObject.Parse(sListParam);

                #endregion
            }
            else
            {
                #region Filtros búsqueda básica
                //htFiltros.Add("idFicepi", Request.Form[prefijo + "hdnIdFicepis"]);
                //htFiltros.Add("bFiltros", ((Request.Form[prefijo + "chkExFS"] == "on") ? "1" : "0"));
                //if (gsTipoPantalla == "BASICA")
                //{
                //    htFiltros.Add("t019_descripcion", (Request.Form[prefijo + "hdnTitulo"]));
                //    htFiltros.Add("t019_idcodtitulo", (Request.Form[prefijo + "hdnIdTitulo"]));
                //    htFiltros.Add("t019_tipo", (Request.Form[prefijo + "cboTipologia"]));
                //    htFiltros.Add("t019_tic", (Request.Form[prefijo + "cboTics"]));
                //    htFiltros.Add("t019_modalidad", (Request.Form[prefijo + "cboModalidad"]));
                //    htFiltros.Add("t582_idcertificado", (Request.Form[prefijo + "hdnIdCertificado"]));

                //    htFiltros.Add("t582_nombre", (Request.Form[prefijo + "hdnCertificacion"]));
                //    htFiltros.Add("lft036_idcodentorno", (Request.Form[prefijo + "hdnIdEntornoFormacion"]));
                //    htFiltros.Add("nombrecuenta", (Request.Form[prefijo + "hdnCuenta"]));
                //    htFiltros.Add("idcuenta", (Request.Form[prefijo + "hdnIdCliente"]));
                //    htFiltros.Add("t483_idsector", (Request.Form[prefijo + "cboSector"]));
                //    htFiltros.Add("t035_codperfile", (Request.Form[prefijo + "cboPerfilExp"]));
                //    htFiltros.Add("let036_idcodentorno", (Request.Form[prefijo + "hdnIdEntornoExp"]));
                //    htFiltros.Add("t020_idcodidioma", (Request.Form[prefijo + "cboIdioma"]));
                //    htFiltros.Add("nivelidioma", (Request.Form[prefijo + "cboNivel"]));
                //    htFiltros.Add("bTitulo", ((Request.Form[prefijo + "chkTituloAcre"] == "on") ? "1" : "0"));
                //}
                sListaProfSeleccionados = Request.Form[prefijo + "hdnIdFicepis"];
                sListaFiltros = Request.Form[prefijo + "hdnCriterios"];
                #endregion
            }
            htCampos.Add("bDP", ((Request.Form[prefijo + "chkExInfo"] == "on") ? "1" : "0"));
            htCampos.Add("bFA", ((Request.Form[prefijo + "chkExFA"] == "on") ? "1" : "0"));
            htCampos.Add("bCR", ((Request.Form[prefijo + "chkExCR"] == "on") ? "1" : "0"));
            htCampos.Add("bCI", ((Request.Form[prefijo + "chkExCI"] == "on") ? "1" : "0"));
            htCampos.Add("bCERT", ((Request.Form[prefijo + "chkExCertExam"] == "on") ? "1" : "0"));
            htCampos.Add("bID", ((Request.Form[prefijo + "chkExID"] == "on") ? "1" : "0"));
            htCampos.Add("bEXPCLI", ((Request.Form[prefijo + "chkExEXPCLI"] == "on") ? "1" : "0"));
            htCampos.Add("bEXPCLIPERF", ((Request.Form[prefijo + "chkExEXPCLIPERF"] == "on") ? "1" : "0"));
            htCampos.Add("bPERF", ((Request.Form[prefijo + "chkExPERF"] == "on") ? "1" : "0"));
            htCampos.Add("bENT", ((Request.Form[prefijo + "chkExENT"] == "on") ? "1" : "0"));
            htCampos.Add("bENTPERF", ((Request.Form[prefijo + "chkExENTPERF"] == "on") ? "1" : "0"));
            htCampos.Add("bENTEXP", ((Request.Form[prefijo + "chkExENTEXP"] == "on") ? "1" : "0"));

            strNombreProfesionales = Request.Form[prefijo + "hdnNombreProfesionales"];
            trackingId = Request.Form[prefijo + "hdnTrackingId"];
            nombreDoc = trackingId;
            strDestinatarios = Request.Form[prefijo + "hdnDestinatarios"];
            strDestinatariosIdFicepi = Request.Form[prefijo + "hdnDestinatarioIdFicepi"];

            miLog.Debug(codred + "->" + trackingId + " Filtros busqueda basica");

            if (Request.Form[prefijo + "rdbTipoExp"] == "0") //Petición On line. Llama directamente a IbServiOffice
            {
                #region Online
                DataSet ds = Curriculum.exportarExcelAvanzada(sListaProfSeleccionados, sListaFiltros, htCampos);
                miLog.Debug(codred + "->" + trackingId + " Dataset generado");
                string sXML = @"<Params><trackingid>" + trackingId + "</trackingid>" +
                              @"<codred>" + codred + @"</codred></Params>";
                //Acceso a IBServiOffice
                osvcExcel = new svcCVT.IsvcCVTClient();
                byte[] result = osvcExcel.getExcelCVT(ds, sExtension, sXML);

                miLog.Debug(codred + "->" + trackingId + " El metodo getExcelCVT ha devuelto datos");

                Response.ClearContent();
                Response.Buffer = true;

                Response.AddHeader("content-type", "application/excel; charset=utf-8");
                Response.AddHeader("Content-Disposition", "attachment;filename=\"Curriculum" + sExtension + "\"");

                Response.Clear();

                if (Request.QueryString["descargaToken"] != null)
                    Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field

                Response.BinaryWrite(result);
                Response.Flush();
                Response.Close();
                miLog.Debug(codred + "->" + trackingId + " Fin exportación Excel.");
                #endregion
            }
            else // Correo. Para que no muera, el cálculo del dataset se hace en IbServices y éste llama a IbServiOffice
            {
                #region Correo
                //SUPER.DAL.Log.Insertar("exportarCVExcel->Peticion en diferido->Inicio");
                enviarCorreoConfPedido();
                sFileName = Request.PhysicalApplicationPath + "TempImagesGraficos\\" + nombreDoc + sExtension;
                //SUPER.DAL.Log.Insertar("exportarCVExcel->sFileName = " + sFileName);

                gsFiltros = sListaFiltros;
                #region Filtros
                /*
                if (bBusAvan)
                {
                    gsFiltros = Request.Form[prefijo + "hdnCriterios"];
                }
                else
                {
                    #region agregar filtros
                    ArrayList aParametros = new ArrayList();
                    aParametros.Add(new ParametrosWS("IdFicepi", (Request.Form[prefijo + "hdnIdFicepis"])));
                    aParametros.Add(new ParametrosWS("TieneFiltros", ((Request.Form[prefijo + "chkRestringir"] == "on") ? "1" : "0")));
                    aParametros.Add(new ParametrosWS("DesTitulacion", (Request.Form[prefijo + "hdnTitulo"])));
                    aParametros.Add(new ParametrosWS("IdTitulacion", (Request.Form[prefijo + "hdnIdTitulo"])));
                    aParametros.Add(new ParametrosWS("TipoTitulacion", (Request.Form[prefijo + "cboTipologia"])));
                    aParametros.Add(new ParametrosWS("Tic", (Request.Form[prefijo + "cboTics"])));
                    aParametros.Add(new ParametrosWS("Modalidad", (Request.Form[prefijo + "cboModalidad"])));
                    aParametros.Add(new ParametrosWS("IdCertificado", (Request.Form[prefijo + "hdnIdCertificado"])));
                    aParametros.Add(new ParametrosWS("DesCertificado", (Request.Form[prefijo + "hdnCertificacion"])));
                    aParametros.Add(new ParametrosWS("EntornoFormacion", (Request.Form[prefijo + "hdnIdEntornoFormacion"])));
                    aParametros.Add(new ParametrosWS("DesCliente", (Request.Form[prefijo + "hdnCuenta"])));
                    aParametros.Add(new ParametrosWS("IdCuenta", (Request.Form[prefijo + "hdnIdCliente"])));
                    aParametros.Add(new ParametrosWS("IdSectorCliente", (Request.Form[prefijo + "cboSector"])));
                    aParametros.Add(new ParametrosWS("CodPerfil", (Request.Form[prefijo + "cboPerfilExp"])));
                    aParametros.Add(new ParametrosWS("EntornoExperiencia", (Request.Form[prefijo + "hdnIdEntornoExp"])));
                    aParametros.Add(new ParametrosWS("IdIdioma", (Request.Form[prefijo + "cboIdioma"])));
                    aParametros.Add(new ParametrosWS("NivelIdioma", (Request.Form[prefijo + "cboNivel"])));
                    aParametros.Add(new ParametrosWS("SoloIdiomaTitulo", ((Request.Form[prefijo + "chkTituloAcre"] == "on") ? "1" : "0")));
                    aParametros.Add(new ParametrosWS("EPIPnummes", ((Request.Form[prefijo + "chkEXPIBENmes"] == "on") ? "1" : "0")));
                    aParametros.Add(new ParametrosWS("EPFPnummes", ((Request.Form[prefijo + "chkEXPFUENmes"] == "on") ? "1" : "0")));

                    gsFiltros = SerializeArrayList(aParametros);

                    #endregion
                }
                */
                #endregion
                #region Agregar campos a mostrar en el CV
                //ArrayList aCampos = new ArrayList();
                //aCampos.Add(new ParametrosWS("bDP", ((Request.Form[prefijo + "chkExInfo"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bFA", ((Request.Form[prefijo + "chkExFA"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bCR", ((Request.Form[prefijo + "chkExCR"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bCI", ((Request.Form[prefijo + "chkExCI"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bCERT", ((Request.Form[prefijo + "chkExCertExam"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bID", ((Request.Form[prefijo + "chkExID"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bEXPCLI", ((Request.Form[prefijo + "chkExEXPCLI"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bEXPCLIPERF", ((Request.Form[prefijo + "chkExEXPCLIPERF"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bPERF", ((Request.Form[prefijo + "chkExPERF"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bENT", ((Request.Form[prefijo + "chkExENT"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bENTPERF", ((Request.Form[prefijo + "chkExENTPERF"] == "on") ? "1" : "0")));
                //aCampos.Add(new ParametrosWS("bENTEXP", ((Request.Form[prefijo + "chkExENTEXP"] == "on") ? "1" : "0")));
                //gsCampos = SerializeArrayList(aCampos);
                gsCampos = Request.Form[prefijo + "hdnCamposExcel"];
                #endregion

                //SUPER.DAL.Log.Insertar("exportarCVExcel-> Antes de llamar al hilo con GenerarCorreoCV.");
                ThreadStart ts = new ThreadStart(GenerarCorreoCV);
                Thread workerThread = new Thread(ts);
                workerThread.Start();
                #endregion
            }

            //Se inserta en la tabla de log (6 -> Exportación a Excel)
            SUPER.DAL.Log.Insertar(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()), 6);

        }
        #region Finalización
        //catch (FaultException<svcEXCEL.IBOfficeException> cex)
        catch (FaultException<svcSUPERIBOffice.SUPERException> cex)
        {
            miLog.Debug(codred + "->" + trackingId + " Error: " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            sErrores += Errores.mostrarError(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
        }
        catch (Exception ex)
        {
            miLog.Debug(codred + "->" + trackingId + " Error: " + ex.Message);
            sErrores += Errores.mostrarError("Error al exportar", ex);
        }
        finally
        {
            if (osvcExcel != null && osvcExcel.State != System.ServiceModel.CommunicationState.Closed)
            {
                try
                {
                    if (osvcExcel.State != System.ServiceModel.CommunicationState.Faulted) osvcExcel.Close();
                    else if (osvcExcel.State != System.ServiceModel.CommunicationState.Closed) osvcExcel.Abort();
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al exportar", ex);
                }
            }
        }
        #endregion
    }

    private void GenerarCorreoCV()
    {
        ILog miLog = LogManager.GetLogger("SUP");
        log4net.Config.XmlConfigurator.Configure();
        ArrayList aParametros = new ArrayList();
        //svcSUPER.IsvcSUPERClient osvcExcel = null;
        svcSUPERIBOffice.IsvcSUPERClient osvcExcel = null;
        try
        {
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> Inicio");
            //SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> Inicio petición de CODRED = " + codred);
            osvcExcel = new svcSUPERIBOffice.IsvcSUPERClient();

            string sXML = @"<Params><trackingid>" + trackingId + "</trackingid>" +
                          @"<nombreProfesionales>" + strNombreProfesionales + @"</nombreProfesionales>" +
                          @"<codred>" + codred + @"</codred>" +
                          @"<idFicepiRte>" + strDestinatariosIdFicepi + @"</idFicepiRte>" +
                          @"<extension>" + sExtension + "</extension></Params>";
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> petición enviada");
            //SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> Antes de llamar al servicio CrearEnviarExcelCVT");
            byte[] byteArray = osvcExcel.CrearEnviarExcelCVT(sXML, sListaProfSeleccionados, gsFiltros, gsCampos);
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> el metodo CrearEnviarExcelCVT se ha ejecutado correctamente");
            //SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> Despues de llamar al servicio CrearEnviarExcelCVT");

            System.IO.File.WriteAllBytes(sFileName, byteArray);
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> Despues de grabar el archivo");
            //SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> Despues de grabar el archivo");
            if ((File.OpenRead(sFileName).Length / 1048576) > 10) //miramos si el archivo es mayor que 10MB
            {
                miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> Antes de enviarAvisoPackExpress");
                //SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> enviarPackExpress");
                enviarAvisoPackExpress(sExtension);
            }
            else
            {
                miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> Antes de enviarCorreo");
                //SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> enviarCorreo");
                enviarCorreo(sExtension);
            }
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> enviado correo de aviso de llegada");
        }
        catch (FaultException<svcSUPER.IBOfficeException> cex)
        {
            sErrores += Errores.mostrarError(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> Error (servicio): " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> Error (servicio): " + cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage);
            enviarCorreoErrorEDA(cex.Detail.Message + "\n\n" + cex.Detail.InnerMessage, cex);
            enviarCorreoErrorUsuario();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al exportar", ex);
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> Error (general): " + ex.Message);
            SUPER.DAL.Log.Insertar(trackingId + " - exportarCVExcel.GenerarCorreoCV -> Error (general): " + ex.Message);
            enviarCorreoErrorEDA("Error al exportar", ex);
            enviarCorreoErrorUsuario();
        }
        finally
        {
            miLog.Debug(codred + "->" + trackingId + " exportarCVExcel.GenerarCorreoCV -> Fin");
            if (osvcExcel != null && osvcExcel.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (osvcExcel.State != System.ServiceModel.CommunicationState.Faulted) osvcExcel.Close();
                else if (osvcExcel.State != System.ServiceModel.CommunicationState.Closed) osvcExcel.Abort();
            }
        }
    }

    private void enviarAvisoPackExpress(string extension)
    {
        string sTexto = "";
        svcSendPack.SendPackClient oPaq = null;
        System.Text.StringBuilder strb = new System.Text.StringBuilder();

        try
        {
            string strMensaje = "";
            string strCabecera = "";
            string strDatos = "";
            string strAsunto = "";
            string strTO = "";
            string[] aNombreProfesionales = Regex.Split(strNombreProfesionales, @"/");

            strAsunto = "Pedido CVT (Localizador: " + trackingId + ")";
            //strMensaje = "El pedido que has realizado para obtener los curriculums de los profesionales que se muestran al final de este correo, ha generado un documento que excede el tamaño máximo para ser enviado por correo (10Mb), por lo que se te enviará en breve a través de PAQEXPRESS.<br><br>";
            strMensaje = "El pedido que has realizado para obtener los curriculums de los profesionales que se muestran al final de este correo, ha generado un documento que se te enviará en breve a través de PAQEXPRESS.<br><br>";
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
            oPaq.CrearPaqueteCV(new FileInfo(nombreDoc + extension).Name, strb.ToString(), File.OpenRead(sFileName));
            /********** Fin de envío a PaqExpress **************/
        }
        catch (Exception ex)
        {
            enviarCorreoErrorEDA(ex.Message + "\r\n", ex);
            enviarCorreoErrorUsuario();
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
            strCabecera = @" <LABEL class='TITULO'>Se adjunta la información de los curriculums de los profesionales que se muestran a continuación:</LABEL>";
        else
            strCabecera = @" <LABEL class='TITULO'>Se adjunta la información del curriculum del profesional que se muestra a continuación:</LABEL>";


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

    private DataTable GetDataTableIdiomaFromList(JArray aLista)
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

}
