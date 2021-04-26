using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.IO;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

namespace SUPER
{
    public partial class Subir : System.Web.UI.Page
    {
        protected byte[] binaryImage;
        protected MemoryStream msFichero;
        public string strAutor = "Autor";
        public string EsPostBack = "false";

        private void Page_Load(object sender, System.EventArgs e)
        {
            #region Compruebo la sesión
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            #endregion
            txtArchivoOld.Attributes.Add("readonly", "readonly");
            txtAutor.Attributes.Add("readonly", "readonly");
            Session["bSubido"] = false;
            chkGestion.Attributes.Add("style", "visibility:hidden;");

            try
            {//Ordenes de facturación, Solicitudes de certificado no llevan características ni link
                if (Request.QueryString["sTipo"].ToString() == "OF" || Request.QueryString["sTipo"].ToString() == "SC")
                {
                    this.rowCab.Style.Add("display", "block");
                    rowLink.Style.Add("display", "none");
                    rowCaracteristicas.Style.Add("display", "none");
                    this.rowComodin.Style.Add("display", "block");
                }
                //CVT (Certificados, Exámenes, Cursos impartidos y recibidos) no llevan descripción, características ni link
                if (Request.QueryString["sTipo"].ToString() == "EXAM" || Request.QueryString["sTipo"].ToString() == "CERT" ||
                    Request.QueryString["sTipo"].ToString() == "CURSOR" || Request.QueryString["sTipo"].ToString() == "CURSOI" ||
                    Request.QueryString["sTipo"].ToString() == "TIF" ||
                    Request.QueryString["sTipo"].ToString() == "TAD" || Request.QueryString["sTipo"].ToString() == "TAE")
                {
                    this.rowCab.Style.Add("display", "block");
                    this.rowDesc.Style.Add("display", "none");
                    rowLink.Style.Add("display", "none");
                    rowCaracteristicas.Style.Add("display", "none");
                    this.rowComodin.Style.Add("display", "block");
                }
                if (Request.QueryString["sTipo"].ToString() == "EC" || Request.QueryString["sTipo"].ToString() == "DI")
                {
                    rowCaracteristicas.Style.Add("display", "none");
                    this.rowComodin.Style.Add("display", "block");
                }
                if (!Page.IsPostBack)
                {
                    #region cargo datos y establezco visibilidad de controles
                    hdnsTipo.Text = Request.QueryString["sTipo"].ToString();
                    hdnnItem.Text = Request.QueryString["nItem"].ToString();
                    hdnsAccion.Text = Request.QueryString["sAccion"].ToString();
                    
                    if (hdnsAccion.Text == "I")
                    {
                        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                            strAutor = "<label name='lblAutor' class='enlace' onclick='obtenerAutor()'>Autor</label>";

                        //txtNumEmpleado.Text = Session["NUM_EMPLEADO_ENTRADA"].ToString();
                        //txtAutor.Text = Session["DES_EMPLEADO_ENTRADA"].ToString();
                        //if (hdnsTipo.Text == "DI") txtNumEmpleado.Text = Session["IDFICEPI"].ToString();
                        //else txtNumEmpleado.Text = Session["UsuarioActual"].ToString();
                        switch (hdnsTipo.Text)
                        {
                            case "DI"://dialogos de alerta
                            case "SC"://solicitud de certificados
                                txtNumEmpleado.Text = Session["IDFICEPI_CVT_ACTUAL"].ToString();
                                break;
                            case "EXAM"://exámenes
                            case "CERT"://certificados
                            case "CURSOR"://Curso recibido
                            case "CURSOI"://Curso impartido
                            case "TIF"://Titulo de idioma
                            case "TAD"://Titulo academico
                            case "TAE"://Expediente de Titulo academico
                                txtNumEmpleado.Text = Request.QueryString["sOrigen"].ToString();
                                break;
                            default:
                                txtNumEmpleado.Text = Session["UsuarioActual"].ToString();
                                break;
                        }

                        //txtAutor.Text = Session["DES_UsuarioActual"].ToString();
                        txtAutor.Text = Session["NOMBRE"].ToString() + " " + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString();
                        switch (hdnsTipo.Text)
                        {
                            case "T":
                                this.chkGestion.Attributes.Add("style", "visibility:visible;");
                                break;
                            case "PSN":
                                this.chkGestion.Checked = true;
                                this.chkGestion.Visible = false;
                                break;
                            case "PE":
                                this.chkGestion.Attributes.Add("style", "visibility:visible;");
                                this.chkGestion.ToolTip = "Indica si el documento es accesible desde PST";
                                this.chkGestion.Text = "Visible desde PST";
                                this.chkGestion.Checked = true;
                                break;
                            default:
                                this.chkGestion.Visible = false;
                                break;
                        }
                    }
                    else if (hdnsAccion.Text == "U")
                    {
                        #region Cargar Datos
                        bool bAdmin = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();
                        switch (hdnsTipo.Text)
                        {
                            case "AS": //Documentación de asuntos de bitácora de proyecto economico
                            case "AS_PE": //Documentación de asuntos de bitácora de proyecto economico
                                #region
                                DOCASU oDocAS = DOCASU.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocAS.t386_iddocasu.ToString();
                                txtDescripcion.Text = oDocAS.t386_descripcion;
                                txtLink.Text = oDocAS.t386_weblink;
                                txtArchivoOld.Text = oDocAS.t386_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocAS.t2_iddocumento.ToString();

                                if (oDocAS.t386_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocAS.t386_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocAS.t386_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocAS.t386_autor.ToString();
                                txtAutor.Text = oDocAS.DesAutor.Replace("&nbsp;", " ") +"   (" + oDocAS.t386_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocAS.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAS.t386_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocAS.t386_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "AC": //Documentación de acciones de bitácora de proyecto economico
                            case "AC_PE": //Documentación de acciones de bitácora de proyecto economico
                                #region
                                DOCACC oDocAC = DOCACC.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocAC.t387_iddocacc.ToString();
                                txtDescripcion.Text = oDocAC.t387_descripcion;
                                txtLink.Text = oDocAC.t387_weblink;
                                txtArchivoOld.Text = oDocAC.t387_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocAC.t2_iddocumento.ToString();

                                if (oDocAC.t387_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocAC.t387_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocAC.t387_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocAC.t387_autor.ToString();
                                txtAutor.Text = oDocAC.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAC.t387_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocAC.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAC.t387_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocAC.t387_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "AS_PT": //Documentación de asuntos de bitácora de proyecto economico
                                #region
                                DOCASU_PT oDocAS_PT = DOCASU_PT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocAS_PT.t411_iddocasu.ToString();
                                txtDescripcion.Text = oDocAS_PT.t411_descripcion;
                                txtLink.Text = oDocAS_PT.t411_weblink;
                                txtArchivoOld.Text = oDocAS_PT.t411_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocAS_PT.t2_iddocumento.ToString();

                                if (oDocAS_PT.t411_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocAS_PT.t411_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocAS_PT.t411_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocAS_PT.t411_autor.ToString();
                                txtAutor.Text = oDocAS_PT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAS_PT.t411_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocAS_PT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAS_PT.t411_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocAS_PT.t411_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "AC_PT": //Documentación de acciones de bitácora de proyecto economico
                                #region
                                DOCACC_PT oDocAC_PT = DOCACC_PT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocAC_PT.t412_iddocacc.ToString();
                                txtDescripcion.Text = oDocAC_PT.t412_descripcion;
                                txtLink.Text = oDocAC_PT.t412_weblink;
                                txtArchivoOld.Text = oDocAC_PT.t412_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocAC_PT.t2_iddocumento.ToString();

                                if (oDocAC_PT.t412_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocAC_PT.t412_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocAC_PT.t412_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocAC_PT.t412_autor.ToString();
                                txtAutor.Text = oDocAC_PT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAC_PT.t412_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocAC_PT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAC_PT.t412_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocAC_PT.t412_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "AS_T": //Documentación de asuntos de bitácora de tarea
                                #region
                                DOCASU_T oDocAS_T = DOCASU_T.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocAS_T.t602_iddocasu.ToString();
                                txtDescripcion.Text = oDocAS_T.t602_descripcion;
                                txtLink.Text = oDocAS_T.t602_weblink;
                                txtArchivoOld.Text = oDocAS_T.t602_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocAS_T.t2_iddocumento.ToString();

                                if (oDocAS_T.t602_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocAS_T.t602_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocAS_T.t602_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocAS_T.t602_autor.ToString();
                                txtAutor.Text = oDocAS_T.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAS_T.t602_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocAS_T.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAS_T.t602_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocAS_T.t602_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "AC_T": //Documentación de acciones de bitácora de proyecto economico
                                #region
                                DOCACC_T oDocAC_T = DOCACC_T.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocAC_T.t603_iddocacc.ToString();
                                txtDescripcion.Text = oDocAC_T.t603_descripcion;
                                txtLink.Text = oDocAC_T.t603_weblink;
                                txtArchivoOld.Text = oDocAC_T.t603_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocAC_T.t2_iddocumento.ToString();

                                if (oDocAC_T.t603_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocAC_T.t603_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocAC_T.t603_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocAC_T.t603_autor.ToString();
                                txtAutor.Text = oDocAC_T.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAC_T.t603_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocAC_T.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAC_T.t603_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocAC_T.t603_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "IAP_T": 
                            case "T":
                                #region
                                DOCUT oDocT = DOCUT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocT.t363_iddocut.ToString();
                                txtDescripcion.Text = oDocT.t363_descripcion;
                                txtLink.Text = oDocT.t363_weblink;
                                txtArchivoOld.Text = oDocT.t363_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocT.t2_iddocumento.ToString();
                                

                                if (oDocT.t363_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocT.t363_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocT.t363_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocT.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocT.t363_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocT.t363_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocT.t314_idusuario_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                if (hdnsTipo.Text=="IAP_T")
                                    chkGestion.Attributes.Add("style", "visibility:hidden;");
                                else
                                    chkGestion.Attributes.Add("style", "visibility:visible;");
                                break;
                                #endregion
                            case "A":
                                #region
                                DOCUA oDocA = DOCUA.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocA.t365_iddocua.ToString();
                                txtDescripcion.Text = oDocA.t365_descripcion;
                                txtLink.Text = oDocA.t365_weblink;
                                txtArchivoOld.Text = oDocA.t365_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocA.t2_iddocumento.ToString();

                                if (oDocA.t365_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocA.t365_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocA.t365_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocA.t365_autor.ToString();
                                txtAutor.Text = oDocA.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocA.t365_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocA.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocA.t365_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocA.t365_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "F":
                                #region
                                DOCUF oDocF = DOCUF.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocF.t364_iddocuf.ToString();
                                txtDescripcion.Text = oDocF.t364_descripcion;
                                txtLink.Text = oDocF.t364_weblink;
                                txtArchivoOld.Text = oDocF.t364_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocF.t2_iddocumento.ToString();

                                if (oDocF.t364_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocF.t364_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocF.t364_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocF.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocF.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocF.t364_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocF.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocF.t364_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocF.t314_idusuario_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "PT":
                                #region
                                DOCUPT oDocPT = DOCUPT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocPT.t362_iddocupt.ToString();
                                txtDescripcion.Text = oDocPT.t362_descripcion;
                                txtLink.Text = oDocPT.t362_weblink;
                                txtArchivoOld.Text = oDocPT.t362_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocPT.t2_iddocumento.ToString();

                                if (oDocPT.t362_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocPT.t362_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocPT.t362_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocPT.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocPT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocPT.t362_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocPT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocPT.t362_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocPT.t314_idusuario_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "PE":
                            case "PSN":
                                #region
                                DOCUPE oDocPE = DOCUPE.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocPE.t368_iddocupe.ToString();
                                txtDescripcion.Text = oDocPE.t368_descripcion;
                                txtLink.Text = oDocPE.t368_weblink;
                                txtArchivoOld.Text = oDocPE.t368_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocPE.t2_iddocumento.ToString();

                                if (oDocPE.t368_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocPE.t368_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocPE.t368_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocPE.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocPE.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocPE.t368_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocPE.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocPE.t368_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocPE.t314_idusuario_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                if (hdnsTipo.Text == "PSN")
                                    chkGestion.Attributes.Add("style", "visibility:hidden;");
                                else
                                {
                                    chkGestion.Attributes.Add("style", "visibility:visible;");
                                    this.chkGestion.ToolTip = "Indica si el documento es accesible desde PST";
                                    this.chkGestion.Text = "Visible desde PST";
                                }
                                break;
                                #endregion
                            case "PEF":
                                #region
                                DOC_ACUERDO_PROY oDocPEF = DOC_ACUERDO_PROY.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocPEF.t640_iddocfact.ToString();
                                txtDescripcion.Text = oDocPEF.t640_descripcion;
                                txtLink.Text = oDocPEF.t640_weblink;
                                txtArchivoOld.Text = oDocPEF.t640_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocPEF.t2_iddocumento.ToString();

                                if (oDocPEF.t640_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocPEF.t640_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocPEF.t640_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocPEF.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocPEF.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocPEF.t640_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocPEF.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocPEF.t640_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocPEF.t314_idusuario_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "HT": //Hito lineal
                            case "HM": //Hito discontinuo
                                #region
                                DOCUH oDocH = DOCUH.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocH.t366_iddocuh.ToString();
                                txtDescripcion.Text = oDocH.t366_descripcion;
                                txtLink.Text = oDocH.t366_weblink;
                                txtArchivoOld.Text = oDocH.t366_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocH.t2_iddocumento.ToString();

                                if (oDocH.t366_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocH.t366_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocH.t366_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocH.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocH.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocH.t366_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocH.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocH.t366_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocH.t314_idusuario_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "HF":
                                #region
                                DOCUHE oDocHE = DOCUHE.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocHE.t367_iddocuhe.ToString();
                                txtDescripcion.Text = oDocHE.t367_descripcion;
                                txtLink.Text = oDocHE.t367_weblink;
                                txtArchivoOld.Text = oDocHE.t367_nombrearchivo;
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocHE.t2_iddocumento.ToString();

                                if (oDocHE.t367_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocHE.t367_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocHE.t367_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocHE.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocHE.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocHE.t367_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocHE.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocHE.t367_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocHE.t314_idusuario_autor.ToString() != Session["UsuarioActual"].ToString() && !bAdmin)
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                break;
                                #endregion
                            case "OF":
                                #region
                                DOCUOF oDocOF = DOCUOF.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                                hdnnIDDOC.Text = oDocOF.t624_iddocuof.ToString();
                                txtDescripcion.Text = oDocOF.t624_descripcion;
                                //txtLink.Text = oDocHE.t367_weblink;
                                txtArchivoOld.Text = oDocOF.t624_nombrearchivo;
                                txtNumEmpleado.Text = oDocOF.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocOF.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocOF.t624_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocOF.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocOF.t624_fechamodif.ToString() + ")";
                                //Cargo el identificador de ATENEA
                                if (oDocOF.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocOF.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "PL_OF":
                                #region
                                PLANTILLADOCUOF oDocPLOF = PLANTILLADOCUOF.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocPLOF.t631_iddocuplanof.ToString();
                                txtDescripcion.Text = oDocPLOF.t631_descripcion;
                                //txtLink.Text = oDocHE.t367_weblink;
                                txtArchivoOld.Text = oDocPLOF.t631_nombrearchivo;
                                txtNumEmpleado.Text = oDocPLOF.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocPLOF.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocPLOF.t631_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocPLOF.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocPLOF.t631_fechamodif.ToString() + ")";
                                //Cargo el identificador de ATENEA
                                if (oDocPLOF.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocPLOF.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "EC":
                                #region
                                DOCUEC oDocEC = DOCUEC.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocEC.t658_iddocuec.ToString();
                                txtDescripcion.Text = oDocEC.t658_descripcion;
                                txtLink.Text = oDocEC.t658_weblink;
                                txtArchivoOld.Text = oDocEC.t658_nombrearchivo;
                                txtNumEmpleado.Text = oDocEC.t314_idusuario_autor.ToString();
                                txtAutor.Text = oDocEC.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocEC.t658_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocEC.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocEC.t658_fechamodif.ToString() + ")";
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocEC.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "DI":
                                #region
                                SUPER.Capa_Datos.DOCDIALOGO oDocDI = 
                                    SUPER.Capa_Datos.DOCDIALOGO.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocDI.t837_id.ToString();
                                txtDescripcion.Text = oDocDI.t837_descripcion;
                                txtLink.Text = oDocDI.t837_weblink;
                                txtArchivoOld.Text = oDocDI.t837_nombrearchivo;
                                txtNumEmpleado.Text = oDocDI.t001_idficepi_autor.ToString();
                                txtAutor.Text = oDocDI.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocDI.t837_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocDI.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocDI.t837_fechamodif.ToString() + ")";
                                //Cargo el identificador de ATENEA
                                this.hdnContentServer.Value = oDocDI.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "SC"://Solicitud de certificado
                                #region
                                SUPER.BLL.DOCSOLICITUD oDocSC = 
                                    SUPER.BLL.DOCSOLICITUD.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()));//, false);
                                hdnnIDDOC.Text = oDocSC.t697_iddoc.ToString();
                                txtDescripcion.Text = oDocSC.t697_descripcion;
                                //txtLink.Text = oDocHE.t367_weblink;
                                txtArchivoOld.Text = oDocSC.t697_nombrearchivo;
                                txtNumEmpleado.Text = oDocSC.t001_idficepi_autor.ToString();
                                txtAutor.Text = oDocSC.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocSC.t697_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocSC.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocSC.t697_fechamodif.ToString() + ")";
                                //Cargo el identificador de ATENEA
                                if (oDocSC.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocSC.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "EXAM"://Documento acreditativo de examen
                                #region
                                int idFicepi1 = int.Parse(Request.QueryString["sOrigen"].ToString());
                                int idExamen = int.Parse(Request.QueryString["nIDDOC"].ToString());
                                SUPER.BLL.Examen oDocExam =
                                    SUPER.BLL.Examen.SelectDoc(null, idExamen, idFicepi1);//, false);
                                hdnnIDDOC.Text = idExamen.ToString();
                                txtDescripcion.Text = oDocExam.T591_NDOC;
                                //txtLink.Text = oDocHE.t367_weblink;
                                txtArchivoOld.Text = oDocExam.T591_NDOC;
                                txtNumEmpleado.Text = idFicepi1.ToString();
                                //txtAutor.Text = oDocExam.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocExam.t697_fecha.ToString() + ")";
                                //txtAutorModif.Text = oDocExam.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocExam.t697_fechamodif.ToString() + ")";
                                //Cargo el identificador de ATENEA
                                if (oDocExam.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocExam.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "CERT"://Documento acreditativo de certificado
                                #region
                                int idFicepi2 = int.Parse(Request.QueryString["sOrigen"].ToString());
                                int idCertificado = int.Parse(Request.QueryString["nIDDOC"].ToString());
                                SUPER.BLL.Certificado oDocCert =
                                    SUPER.BLL.Certificado.SelectDoc(null, idCertificado, idFicepi2);
                                hdnnIDDOC.Text = idCertificado.ToString();
                                txtDescripcion.Text = oDocCert.T593_NDOC;
                                txtArchivoOld.Text = oDocCert.T593_NDOC;
                                txtNumEmpleado.Text = idFicepi2.ToString();
                                //txtAutor.Text = oDocCert.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocCert.t697_fecha.ToString() + ")";
                                //txtAutorModif.Text = oDocCert.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocCert.t697_fechamodif.ToString() + ")";
                                //Cargo el identificador de ATENEA
                                if (oDocCert.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocCert.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "CURSOR"://Documento acreditativo de curso recibido
                                #region
                                int idFicepiCursoR = int.Parse(Request.QueryString["sOrigen"].ToString());
                                int idCurso = int.Parse(Request.QueryString["nIDDOC"].ToString());
                                SUPER.BLL.Curso oDocCursoRec =
                                    SUPER.BLL.Curso.SelectDoc(null, idCurso, idFicepiCursoR);
                                hdnnIDDOC.Text = idCurso.ToString();
                                txtDescripcion.Text = oDocCursoRec.T575_NDOC;
                                txtArchivoOld.Text = oDocCursoRec.T575_NDOC;
                                txtNumEmpleado.Text = idFicepiCursoR.ToString();
                                //Cargo el identificador de ATENEA
                                if (oDocCursoRec.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocCursoRec.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "CURSOI"://Documento acreditativo de curso impartido
                                #region
                                int idFicepiCursoI = int.Parse(Request.QueryString["sOrigen"].ToString());
                                int idCursoI = int.Parse(Request.QueryString["nIDDOC"].ToString());
                                //SUPER.BLL.Curso oDocCursoImp = SUPER.BLL.Curso.SelectDoc2(null, idCursoI, idFicepiCursoI);
                                SUPER.BLL.Curso oDocCursoImp = SUPER.BLL.Curso.DetalleMonitor(idCursoI);
                                hdnnIDDOC.Text = idCursoI.ToString();
                                txtDescripcion.Text = oDocCursoImp.T580_NDOC;
                                txtArchivoOld.Text = oDocCursoImp.T580_NDOC;
                                txtNumEmpleado.Text = idFicepiCursoI.ToString();
                                //Cargo el identificador de ATENEA
                                if (oDocCursoImp.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocCursoImp.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "TIF"://Documento acreditativo de titulo de idioma
                                #region
                                int idFicepiTitIdio = int.Parse(Request.QueryString["sOrigen"].ToString());
                                int idTitIdio = int.Parse(Request.QueryString["nIDDOC"].ToString());
                                SUPER.BLL.TituloIdiomaFic oDocTitIdio = SUPER.BLL.TituloIdiomaFic.Detalle(idTitIdio);
                                hdnnIDDOC.Text = idTitIdio.ToString();
                                txtDescripcion.Text = oDocTitIdio.T021_NDOC;
                                txtArchivoOld.Text = oDocTitIdio.T021_NDOC;
                                txtNumEmpleado.Text = idFicepiTitIdio.ToString();
                                //Cargo el identificador de ATENEA
                                if (oDocTitIdio.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocTitIdio.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "TAD"://Documento acreditativo de titulo academico
                                #region
                                int idFicepiTitDoc = int.Parse(Request.QueryString["sOrigen"].ToString());
                                int idTit = int.Parse(Request.QueryString["nIDDOC"].ToString());
                                SUPER.BLL.TituloFicepi oDocTitDoc = SUPER.BLL.TituloFicepi.Select(idTit);
                                hdnnIDDOC.Text = idTit.ToString();
                                txtDescripcion.Text = oDocTitDoc.T012_NDOCTITULO;
                                txtArchivoOld.Text = oDocTitDoc.T012_NDOCTITULO;
                                txtNumEmpleado.Text = idFicepiTitDoc.ToString();
                                //Cargo el identificador de ATENEA
                                if (oDocTitDoc.t2_iddocumento == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocTitDoc.t2_iddocumento.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                            case "TAE"://Documento acreditativo de expediente de titulo academico
                                #region
                                int idFicepiTitExpte = int.Parse(Request.QueryString["sOrigen"].ToString());
                                int idTitExpte = int.Parse(Request.QueryString["nIDDOC"].ToString());
                                SUPER.BLL.TituloFicepi oDocTitExpte = SUPER.BLL.TituloFicepi.Select(idTitExpte);
                                hdnnIDDOC.Text = idTitExpte.ToString();
                                txtDescripcion.Text = oDocTitExpte.T012_NDOCEXPDTE;
                                txtArchivoOld.Text = oDocTitExpte.T012_NDOCEXPDTE;
                                txtNumEmpleado.Text = idFicepiTitExpte.ToString();
                                //Cargo el identificador de ATENEA
                                if (oDocTitExpte.t2_iddocumentoExpte == null)
                                    this.hdnContentServer.Value = "";
                                else
                                    this.hdnContentServer.Value = oDocTitExpte.t2_iddocumentoExpte.ToString();

                                chkPrivado.Enabled = false;
                                chkLectura.Enabled = false;
                                chkGestion.Enabled = false;
                                break;
                                #endregion
                        }
                        #endregion
                    }
                    if (Request.QueryString["sOrigen"] != null && Request.QueryString["sOrigen"].ToString() == "IAP")
                    {
                        chkGestion.Checked = true;
                        chkGestion.Attributes.Add("style", "visibility:hidden;");
                    }
                    #endregion
                }
                else
                {
                    Session["bSubido"] = true;
                    Grabar();
                    EsPostBack = "true";
                }
            }
            catch (System.OutOfMemoryException)
            {
                //Si el archivo a subir es demasiado grande, se produce un error por falta de memoria.
                //La ventana de la barra de progreso ya avisa al usuario de esta situación y cierra esta ventana.
            }
            catch (ConservaException cex)
            {
                this.hdnError.Value = Utilidades.MsgErrorConserva("W", cex);
            }
            catch (Exception ex)
            {
                this.hdnError.Value = "Error: " + ex.Message;
                if (ex.InnerException != null)
                    this.hdnError.Value += "<br />Detalle error: " + ex.InnerException.Message;
            }
        }

        private void Grabar()
        {
            byte[] ArchivoEnBinario = new Byte[0];
            string sNombre = "";
            bool bPrivado = false;
            bool bLectura = false;
            bool bGestion = false;
            long? idContentServer = null;

            //Create el PostedFile
            HttpPostedFile Archivo = txtArchivo.PostedFile;

            #region Obtengo el nombre del archivo
            if (hdnsAccion.Text == "I")
            {
                if (Archivo.ContentLength > 0)
                {
                    string sArchivo = Archivo.FileName;
                    int nPos = sArchivo.LastIndexOf("\\");
                    sNombre = sArchivo.Substring(nPos + 1, sArchivo.Length - nPos - 1);
                }
            }
            else //U
            {
                if (Archivo.FileName != "")
                {
                    string sArchivo = Archivo.FileName;
                    int nPos = sArchivo.LastIndexOf("\\");
                    sNombre = sArchivo.Substring(nPos + 1, sArchivo.Length - nPos - 1);
                }
                else sNombre = txtArchivoOld.Text;
            }
            #endregion
            #region Obtengo el id del archivo en Atenea.
            //Si no hay nombre de archivo lo dejamos en NULL porque puede ser
            //que el registro no tuviera documento o que lo tuviera pero ahora el usuario lo está borrando
            if (this.hdnContentServer.Value != "" && sNombre != "")
                idContentServer = long.Parse(this.hdnContentServer.Value);
            #endregion
            #region Obtengo el contenido del archivo y lo guardo en ATENEA
            if (Archivo.ContentLength > 0)
            {
                ArchivoEnBinario = new Byte[Archivo.ContentLength]; //Crear el array de bytes con la longitud del archivo
                Archivo.InputStream.Read(ArchivoEnBinario, 0, Archivo.ContentLength); //Forzar al control del archivo a cargar los datos en el array

                //Si he seleccionado un archivo, cargo el archivo en el ContenteServer y obtengo su identificador
                //Excepto para documentos de orden de facturación y de plantilla de orden de facturacion
                //if (hdnsTipo.Text != "OF" && hdnsTipo.Text != "PL_OF")
                //{
                    if (this.hdnContentServer.Value != "")
                    {//Cuando hemos cargado la página ya teníamos un Id de documento en ATENEA
                        idContentServer = long.Parse(this.hdnContentServer.Value);
                        if (sNombre == txtArchivoOld.Text)
                        {//Si el nombre del nuevo archivo es el mismo que el inicial
                            IB.Conserva.ConservaHelper.ActualizarContenidoDocumento((long)idContentServer, ArchivoEnBinario);
                        }
                        else
                        {//El archivo a cargar es dierente
                            IB.Conserva.ConservaHelper.ActualizarDocumento((long)idContentServer, ArchivoEnBinario, sNombre);
                        }
                    }
                    else
                        idContentServer = IB.Conserva.ConservaHelper.SubirDocumento(sNombre, ArchivoEnBinario);
                //}
            }
            #endregion

            if (chkPrivado.Checked) bPrivado = true;
            if (chkLectura.Checked) bLectura = true;
            if (chkGestion.Checked) bGestion = true;
            //controlar que no se haya subido archivo: e.g. solo link

            #region Insertar o Updatear en la tabla de documentos correspondiente
            switch (hdnsTipo.Text)
            {
                case "AS": //Asunto de bitácora
                case "AS_PE": //Asunto de bitácora
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCASU.Insert(null, int.Parse(hdnnItem.Text), idContentServer, 
                                        int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
                                        bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                        case "U":
                            DOCASU.Update(null, int.Parse(hdnnItem.Text), idContentServer, int.Parse(txtNumEmpleado.Text),
                                        txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                    }
                    break;
                case "AC": //Ación de bitácora
                case "AC_PE": //Ación de bitácora
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCACC.Insert(null, int.Parse(hdnnItem.Text), idContentServer, 
                                        int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
                                        bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                        case "U":
                            DOCACC.Update(null, int.Parse(hdnnItem.Text), idContentServer, int.Parse(txtNumEmpleado.Text),
                                        txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                    }
                    break;
                case "AS_PT": //Asunto de bitácora de proyecto técnico
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCASU_PT.Insert(null, int.Parse(hdnnItem.Text), idContentServer, 
                                        int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
                                        bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                        case "U":
                            DOCASU_PT.Update(null, int.Parse(hdnnItem.Text), idContentServer, int.Parse(txtNumEmpleado.Text),
                                        txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                    }
                    break;
                case "AC_PT": //Ación de bitácora de proyecto técnico
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCACC_PT.Insert(null, int.Parse(hdnnItem.Text), idContentServer, 
                                        int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
                                        bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                        case "U":
                            DOCACC_PT.Update(null, int.Parse(hdnnItem.Text), idContentServer, int.Parse(txtNumEmpleado.Text),
                                        txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                    }
                    break;
                case "AS_T": //Asunto de bitácora de tarea
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCASU_T.Insert(null, int.Parse(hdnnItem.Text), idContentServer, 
                                        int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
                                        bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                        case "U":
                            DOCASU_T.Update(null, int.Parse(hdnnItem.Text), idContentServer, int.Parse(txtNumEmpleado.Text),
                                        txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                    }
                    break;
                case "AC_T": //Acción de bitácora de tarea
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCACC_T.Insert(null, int.Parse(hdnnItem.Text), idContentServer, 
                                        int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
                                        bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                        case "U":
                            DOCACC_T.Update(null, int.Parse(hdnnItem.Text), idContentServer, int.Parse(txtNumEmpleado.Text),
                                        txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
                            break;
                    }
                    break;
                case "T": //Tarea
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCUT.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                                         idContentServer, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCUT.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text,
                                         sNombre, idContentServer, bPrivado, bLectura, bGestion,
                                         int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "A": //Actividad
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            //DOCUA.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario,
                            //             bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            DOCUA.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, idContentServer,
                                         bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            //DOCUA.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                            //             ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            DOCUA.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                                         idContentServer, bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "F": //Fase
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCUF.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, idContentServer,
                                         bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCUF.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                                         idContentServer, bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "PT": //Proyecto Técnico
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCUPT.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, idContentServer, 
                                          bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCUPT.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                                          idContentServer, bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "PE": //Proyecto Económico
                case "PSN": //Proyecto Económico
                    int nPSN = int.Parse(hdnnItem.Text);
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCUPE.Insert(null, nPSN, txtDescripcion.Text, txtLink.Text, sNombre, idContentServer, bPrivado, bLectura,
                                            bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCUPE.Update(null, int.Parse(hdnnIDDOC.Text), nPSN, txtDescripcion.Text, txtLink.Text, sNombre, idContentServer,
                                          bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "PEF": //Documentos de facturación del espacio de acuerdo del Proyecto Económico
                    int nPE = int.Parse(hdnnItem.Text);
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOC_ACUERDO_PROY.Insert(null, nPE, txtDescripcion.Text, txtLink.Text, sNombre, idContentServer, bPrivado,
                                                    bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOC_ACUERDO_PROY.Update(null, int.Parse(hdnnIDDOC.Text), nPE, txtDescripcion.Text, txtLink.Text, sNombre,
                                        idContentServer, bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "HT": //Hito lineal
                case "HM": //Hito discontinuo
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCUH.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, idContentServer, 
                                         bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCUH.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                                         idContentServer, bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "HF": //Hito de fecha
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCUHE.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, idContentServer, 
                                          bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCUHE.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                                          idContentServer, bPrivado, bLectura, bGestion, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "OF": //Orden de facturación
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            if (Utilidades.isNumeric(hdnnItem.Text))
                                DOCUOF.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, sNombre, idContentServer, ArchivoEnBinario,
                                             int.Parse(txtNumEmpleado.Text), null);
                            else
                                DOCUOF.Insert(null, null, txtDescripcion.Text, sNombre, idContentServer, ArchivoEnBinario, int.Parse(txtNumEmpleado.Text),
                                              hdnnItem.Text);
                            break;
                        case "U":
                            if (Utilidades.isNumeric(hdnnItem.Text))
                                DOCUOF.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, sNombre,
                                              idContentServer, ArchivoEnBinario, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            else
                                DOCUOF.Update(null, int.Parse(hdnnIDDOC.Text), 0, txtDescripcion.Text, sNombre,
                                              idContentServer, ArchivoEnBinario, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));

                            break;
                    }
                    break;
                case "PL_OF": // Plantilla de orden de facturación
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            if (Utilidades.isNumeric(hdnnItem.Text))
                                PLANTILLADOCUOF.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, sNombre, idContentServer, 
                                                       int.Parse(txtNumEmpleado.Text), null);
                            else
                                PLANTILLADOCUOF.Insert(null, null, txtDescripcion.Text, sNombre, idContentServer, 
                                                       int.Parse(txtNumEmpleado.Text), hdnnItem.Text);
                            break;
                        case "U":
                            PLANTILLADOCUOF.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, sNombre,
                                                   idContentServer, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "EC": //ESPACIO DE COMUNICACION
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            if (Utilidades.isNumeric(hdnnItem.Text))
                                DOCUEC.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, idContentServer, 
                                              int.Parse(txtNumEmpleado.Text), null);
                            else
                                DOCUEC.Insert(null, null, txtDescripcion.Text, txtLink.Text, sNombre, idContentServer, 
                                              int.Parse(txtNumEmpleado.Text), hdnnItem.Text);
                            break;
                        case "U":
                            DOCUEC.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text,
                                          sNombre, idContentServer, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "DI": //Dialogo de alerta
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            SUPER.Capa_Datos.DOCDIALOGO.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,
                                                               idContentServer, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            SUPER.Capa_Datos.DOCDIALOGO.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text,
                                                               txtLink.Text, sNombre, idContentServer, 
                                                               int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                            break;
                    }
                    break;
                case "SC": //Solicitud de certificado
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            if (Utilidades.isNumeric(hdnnItem.Text))
                                SUPER.BLL.DOCSOLICITUD.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, sNombre,
                                                               idContentServer, int.Parse(txtNumEmpleado.Text),"");
                            else
                                SUPER.BLL.DOCSOLICITUD.Insert(null, null, txtDescripcion.Text, sNombre, idContentServer,
                                                              int.Parse(txtNumEmpleado.Text), hdnnItem.Text);
                            break;
                        case "U":
                            SUPER.BLL.DOCSOLICITUD.Update(null, int.Parse(hdnnItem.Text), int.Parse(hdnnIDDOC.Text), txtDescripcion.Text,
                                                          sNombre, idContentServer, int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                            break;
                    }
                    //throw (new NullReferenceException("Error provocado"));
                    break;
                case "EXAM": // Examen de CVT
                    if (Utilidades.isNumeric(hdnnItem.Text))//Actualizo el documento en el examen del profesional
                        SUPER.BLL.Examen.PonerDocumento(null, int.Parse(txtNumEmpleado.Text), int.Parse(hdnnItem.Text),
                                                        (long)idContentServer, sNombre);
                    else//Inserto el documento en la tabla auxiliar
                        SUPER.BLL.DocuAux.Insert(null, int.Parse(txtNumEmpleado.Text), "E", sNombre, hdnnItem.Text,
                                                (long)idContentServer, false);
                    break;
                case "CERT": // Certificado de CVT
                    if (Utilidades.isNumeric(hdnnItem.Text))//Actualizo el documento en el certificado del profesional
                        SUPER.BLL.Certificado.PonerDocumento(null, int.Parse(txtNumEmpleado.Text), int.Parse(hdnnItem.Text),
                                                        (long)idContentServer, sNombre);
                    else//Inserto el documento en la tabla auxiliar
                        SUPER.BLL.DocuAux.Insert(null, int.Parse(txtNumEmpleado.Text), "C", sNombre, hdnnItem.Text,
                                                (long)idContentServer, false);
                    break;
                case "CURSOR": // Curso recibido
                    if (Utilidades.isNumeric(hdnnItem.Text))//Actualizo el documento en el curso del profesional
                        SUPER.BLL.Curso.PonerDocumento(null, "R", int.Parse(hdnnItem.Text), int.Parse(txtNumEmpleado.Text),
                                                        sNombre, (long)idContentServer);
                    else//Inserto el documento en la tabla auxiliar
                        SUPER.BLL.DocuAux.Insert(null, int.Parse(txtNumEmpleado.Text), "R", sNombre, hdnnItem.Text,
                                                (long)idContentServer, false);
                    break;
                case "CURSOI": // Curso impartido
                    if (Utilidades.isNumeric(hdnnItem.Text))//Actualizo el documento en el curso del profesional
                    {
                        SUPER.BLL.Curso.PonerDocumento(null, "I", int.Parse(hdnnItem.Text), int.Parse(txtNumEmpleado.Text),
                                                        sNombre, (long)idContentServer);
                    }
                    else//Inserto el documento en la tabla auxiliar
                        SUPER.BLL.DocuAux.Insert(null, int.Parse(txtNumEmpleado.Text), "I", sNombre, hdnnItem.Text,
                                                (long)idContentServer, false);
                    break;
                case "TIF": // Titulo de idioma
                    if (Utilidades.isNumeric(hdnnItem.Text))//Actualizo el documento 
                    {
                        SUPER.BLL.TituloIdiomaFic.PonerDocumento(null, int.Parse(hdnnItem.Text), int.Parse(txtNumEmpleado.Text),
                                                        sNombre, (long)idContentServer);
                    }
                    else//Inserto el documento en la tabla auxiliar
                        SUPER.BLL.DocuAux.Insert(null, int.Parse(txtNumEmpleado.Text), "T", sNombre, hdnnItem.Text,
                                                (long)idContentServer, false);
                    break;
                case "TAD": // Titulo academico
                    if (Utilidades.isNumeric(hdnnItem.Text))//Actualizo el documento 
                    {
                        SUPER.BLL.TituloFicepi.PonerDocumento(null, "TIT", int.Parse(hdnnItem.Text), int.Parse(txtNumEmpleado.Text),
                                                        sNombre, (long)idContentServer);
                    }
                    else//Inserto el documento en la tabla auxiliar
                        SUPER.BLL.DocuAux.Insert(null, int.Parse(txtNumEmpleado.Text), "A", sNombre, hdnnItem.Text,
                                                (long)idContentServer, false);
                    break;
                case "TAE": // Expediente de Titulo academico
                    if (Utilidades.isNumeric(hdnnItem.Text))//Actualizo el documento 
                    {
                        SUPER.BLL.TituloFicepi.PonerDocumento(null, "EXPTE", int.Parse(hdnnItem.Text), int.Parse(txtNumEmpleado.Text),
                                                        sNombre, (long)idContentServer);
                    }
                    else//Inserto el documento en la tabla auxiliar
                        SUPER.BLL.DocuAux.Insert(null, int.Parse(txtNumEmpleado.Text), "B", sNombre, hdnnItem.Text,
                                                (long)idContentServer, false);
                    break;
            }
            #endregion
        }
    }
}