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
using GASVI.BLL;

namespace GASVI
{
    public partial class Subir : System.Web.UI.Page
    {
        protected byte[] binaryImage;
        protected MemoryStream msFichero;
        public string strAutor = "Autor";
        public string EsPostBack = "false";

        private void Page_Load(object sender, System.EventArgs e)
        {
            txtArchivoOld.Attributes.Add("readonly", "readonly");
            txtAutor.Attributes.Add("readonly", "readonly");
            Session["GVT_bSubido"] = false;
            chkGestion.Attributes.Add("style", "visibility:hidden;");

            try
            {
                if (Request.QueryString["sTipo"].ToString() == "OF")
                {
                    rowLink.Style.Add("display", "none");
                    rowCaracteristicas.Style.Add("display", "none");
                }
                if (!Page.IsPostBack)
                {
                    hdnsTipo.Text = Request.QueryString["sTipo"].ToString();
                    hdnnItem.Text = Request.QueryString["nItem"].ToString();
                    hdnsAccion.Text = Request.QueryString["sAccion"].ToString();
                    if (hdnsTipo.Text == "AS" || hdnsTipo.Text == "AC") this.chkGestion.Visible = false;

                    if (hdnsAccion.Text == "I")
                    {
                        if (Session["GVT_AdminActual"].ToString() == "A")
                            strAutor = "<label name='lblAutor' class='enlace' onclick='obtenerAutor()'>Autor</label>";

                        //txtNumEmpleado.Text = Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString();
                        //txtAutor.Text = Session["GVT_DES_EMPLEADO_ENTRADA"].ToString();
                        txtNumEmpleado.Text = Session["GVT_UsuarioActual"].ToString();
                        //txtAutor.Text = Session["GVT_DES_UsuarioActual"].ToString();
                        txtAutor.Text = Session["GVT_PROFESIONAL"].ToString();
                        if (hdnsTipo.Text == "T") chkGestion.Attributes.Add("style", "visibility:visible;");
                    }
                    else if (hdnsAccion.Text == "U")
                    {
                        #region Cargar Datos
                        //switch (hdnsTipo.Text)
                        //{
                        //    case "AS": //Documentación de asuntos de bitácora de proyecto economico
                        //    case "AS_PE": //Documentación de asuntos de bitácora de proyecto economico
                        //        DOCASU oDocAS = DOCASU.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocAS.t386_iddocasu.ToString();
                        //        txtDescripcion.Text = oDocAS.t386_descripcion;
                        //        txtLink.Text = oDocAS.t386_weblink;
                        //        txtArchivoOld.Text = oDocAS.t386_nombrearchivo;

                        //        if (oDocAS.t386_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocAS.t386_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocAS.t386_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocAS.t386_autor.ToString();
                        //        txtAutor.Text = oDocAS.DesAutor.Replace("&nbsp;", " ") +"   (" + oDocAS.t386_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocAS.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAS.t386_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocAS.t386_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "AC": //Documentación de acciones de bitácora de proyecto economico
                        //    case "AC_PE": //Documentación de acciones de bitácora de proyecto economico
                        //        DOCACC oDocAC = DOCACC.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocAC.t387_iddocacc.ToString();
                        //        txtDescripcion.Text = oDocAC.t387_descripcion;
                        //        txtLink.Text = oDocAC.t387_weblink;
                        //        txtArchivoOld.Text = oDocAC.t387_nombrearchivo;

                        //        if (oDocAC.t387_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocAC.t387_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocAC.t387_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocAC.t387_autor.ToString();
                        //        txtAutor.Text = oDocAC.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAC.t387_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocAC.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAC.t387_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocAC.t387_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "AS_PT": //Documentación de asuntos de bitácora de proyecto economico
                        //        DOCASU_PT oDocAS_PT = DOCASU_PT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocAS_PT.t411_iddocasu.ToString();
                        //        txtDescripcion.Text = oDocAS_PT.t411_descripcion;
                        //        txtLink.Text = oDocAS_PT.t411_weblink;
                        //        txtArchivoOld.Text = oDocAS_PT.t411_nombrearchivo;

                        //        if (oDocAS_PT.t411_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocAS_PT.t411_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocAS_PT.t411_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocAS_PT.t411_autor.ToString();
                        //        txtAutor.Text = oDocAS_PT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAS_PT.t411_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocAS_PT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAS_PT.t411_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocAS_PT.t411_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "AC_PT": //Documentación de acciones de bitácora de proyecto economico
                        //        DOCACC_PT oDocAC_PT = DOCACC_PT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocAC_PT.t412_iddocacc.ToString();
                        //        txtDescripcion.Text = oDocAC_PT.t412_descripcion;
                        //        txtLink.Text = oDocAC_PT.t412_weblink;
                        //        txtArchivoOld.Text = oDocAC_PT.t412_nombrearchivo;

                        //        if (oDocAC_PT.t412_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocAC_PT.t412_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocAC_PT.t412_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocAC_PT.t412_autor.ToString();
                        //        txtAutor.Text = oDocAC_PT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAC_PT.t412_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocAC_PT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAC_PT.t412_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocAC_PT.t412_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "AS_T": //Documentación de asuntos de bitácora de tarea
                        //        DOCASU_T oDocAS_T = DOCASU_T.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocAS_T.t602_iddocasu.ToString();
                        //        txtDescripcion.Text = oDocAS_T.t602_descripcion;
                        //        txtLink.Text = oDocAS_T.t602_weblink;
                        //        txtArchivoOld.Text = oDocAS_T.t602_nombrearchivo;

                        //        if (oDocAS_T.t602_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocAS_T.t602_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocAS_T.t602_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocAS_T.t602_autor.ToString();
                        //        txtAutor.Text = oDocAS_T.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAS_T.t602_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocAS_T.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAS_T.t602_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocAS_T.t602_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "AC_T": //Documentación de acciones de bitácora de proyecto economico
                        //        DOCACC_T oDocAC_T = DOCACC_T.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocAC_T.t603_iddocacc.ToString();
                        //        txtDescripcion.Text = oDocAC_T.t603_descripcion;
                        //        txtLink.Text = oDocAC_T.t603_weblink;
                        //        txtArchivoOld.Text = oDocAC_T.t603_nombrearchivo;

                        //        if (oDocAC_T.t603_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocAC_T.t603_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocAC_T.t603_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocAC_T.t603_autor.ToString();
                        //        txtAutor.Text = oDocAC_T.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocAC_T.t603_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocAC_T.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocAC_T.t603_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocAC_T.t603_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "IAP_T":
                        //    case "T":
                        //        DOCUT oDocT = DOCUT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocT.t363_iddocut.ToString();
                        //        txtDescripcion.Text = oDocT.t363_descripcion;
                        //        txtLink.Text = oDocT.t363_weblink;
                        //        txtArchivoOld.Text = oDocT.t363_nombrearchivo;
                        //        chkGestion.Attributes.Add("style", "visibility:visible;");

                        //        if (oDocT.t363_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocT.t363_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocT.t363_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocT.t314_idusuario_autor.ToString();
                        //        txtAutor.Text = oDocT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocT.t363_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocT.t363_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocT.t314_idusuario_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "A":
                        //        DOCUA oDocA = DOCUA.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocA.t365_iddocua.ToString();
                        //        txtDescripcion.Text = oDocA.t365_descripcion;
                        //        txtLink.Text = oDocA.t365_weblink;
                        //        txtArchivoOld.Text = oDocA.t365_nombrearchivo;

                        //        if (oDocA.t365_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocA.t365_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocA.t365_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocA.t365_autor.ToString();
                        //        txtAutor.Text = oDocA.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocA.t365_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocA.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocA.t365_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocA.t365_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "F":
                        //        DOCUF oDocF = DOCUF.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocF.t364_iddocuf.ToString();
                        //        txtDescripcion.Text = oDocF.t364_descripcion;
                        //        txtLink.Text = oDocF.t364_weblink;
                        //        txtArchivoOld.Text = oDocF.t364_nombrearchivo;

                        //        if (oDocF.t364_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocF.t364_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocF.t364_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocF.t314_idusuario_autor.ToString();
                        //        txtAutor.Text = oDocF.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocF.t364_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocF.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocF.t364_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocF.t314_idusuario_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "PT":
                        //        DOCUPT oDocPT = DOCUPT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocPT.t362_iddocupt.ToString();
                        //        txtDescripcion.Text = oDocPT.t362_descripcion;
                        //        txtLink.Text = oDocPT.t362_weblink;
                        //        txtArchivoOld.Text = oDocPT.t362_nombrearchivo;

                        //        if (oDocPT.t362_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocPT.t362_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocPT.t362_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocPT.t314_idusuario_autor.ToString();
                        //        txtAutor.Text = oDocPT.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocPT.t362_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocPT.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocPT.t362_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocPT.t314_idusuario_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "PE":
                        //    case "PSN":
                        //        DOCUPE oDocPE = DOCUPE.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocPE.t368_iddocupe.ToString();
                        //        txtDescripcion.Text = oDocPE.t368_descripcion;
                        //        txtLink.Text = oDocPE.t368_weblink;
                        //        txtArchivoOld.Text = oDocPE.t368_nombrearchivo;

                        //        if (oDocPE.t368_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocPE.t368_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocPE.t368_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocPE.t314_idusuario_autor.ToString();
                        //        txtAutor.Text = oDocPE.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocPE.t368_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocPE.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocPE.t368_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocPE.t314_idusuario_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "HT": //Hito lineal
                        //    case "HM": //Hito discontinuo
                        //        DOCUH oDocH = DOCUH.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocH.t366_iddocuh.ToString();
                        //        txtDescripcion.Text = oDocH.t366_descripcion;
                        //        txtLink.Text = oDocH.t366_weblink;
                        //        txtArchivoOld.Text = oDocH.t366_nombrearchivo;

                        //        if (oDocH.t366_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocH.t366_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocH.t366_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocH.t314_idusuario_autor.ToString();
                        //        txtAutor.Text = oDocH.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocH.t366_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocH.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocH.t366_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocH.t314_idusuario_autor.ToString() != Session["GVT_UsuarioActual"].ToString() && 
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "HF":
                        //        DOCUHE oDocHE = DOCUHE.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocHE.t367_iddocuhe.ToString();
                        //        txtDescripcion.Text = oDocHE.t367_descripcion;
                        //        txtLink.Text = oDocHE.t367_weblink;
                        //        txtArchivoOld.Text = oDocHE.t367_nombrearchivo;

                        //        if (oDocHE.t367_privado) chkPrivado.Checked = true;
                        //        else chkPrivado.Checked = false;
                        //        if (oDocHE.t367_modolectura) chkLectura.Checked = true;
                        //        else chkLectura.Checked = false;
                        //        if (oDocHE.t367_tipogestion) chkGestion.Checked = true;
                        //        else chkGestion.Checked = false;

                        //        txtNumEmpleado.Text = oDocHE.t314_idusuario_autor.ToString();
                        //        txtAutor.Text = oDocHE.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocHE.t367_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocHE.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocHE.t367_fechamodif.ToString() + ")";

                        //        //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                        //        if (oDocHE.t314_idusuario_autor.ToString() != Session["GVT_UsuarioActual"].ToString() &&
                        //            Session["GVT_AdminActual"].ToString() != "A")
                        //        {
                        //            chkPrivado.Enabled = false;
                        //            chkLectura.Enabled = false;
                        //            chkGestion.Enabled = false;
                        //        }
                        //        break;
                        //    case "OF":
                        //        DOCUOF oDocOF = DOCUOF.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                        //        hdnnIDDOC.Text = oDocOF.t624_iddocuof.ToString();
                        //        txtDescripcion.Text = oDocOF.t624_descripcion;
                        //        //txtLink.Text = oDocHE.t367_weblink;
                        //        txtArchivoOld.Text = oDocOF.t624_nombrearchivo;
                        //        txtNumEmpleado.Text = oDocOF.t314_idusuario_autor.ToString();
                        //        txtAutor.Text = oDocOF.DesAutor.Replace("&nbsp;", " ") + "   (" + oDocOF.t624_fecha.ToString() + ")";
                        //        txtAutorModif.Text = oDocOF.DesAutorModif.Replace("&nbsp;", " ") + "   (" + oDocOF.t624_fechamodif.ToString() + ")";

                        //        chkPrivado.Enabled = false;
                        //        chkLectura.Enabled = false;
                        //        chkGestion.Enabled = false;
                        //        break;
                        //}
                        #endregion
                    }
                    if (Request.QueryString["sOrigen"] != null && Request.QueryString["sOrigen"].ToString() == "IAP")
                    {
                        chkGestion.Checked = true;
                        chkGestion.Attributes.Add("style", "visibility:hidden;");
                    }
                }
                else
                {
                    Session["GVT_bSubido"] = true;
                    Grabar();
                    EsPostBack = "true";
                }
            }
            catch (System.OutOfMemoryException)
            {
                //Si el archivo a subir es demasiado grande, se produce un error por
                //falta de memoria. La ventana de la barra de progreso ya avisa al usuario de
                //esta situación y cierre esta ventana.
            }
        }

        private void Grabar()
        {
            byte[] ArchivoEnBinario = new Byte[0];
            string sNombre = "";
            //bool bPrivado = false;
            //bool bLectura = false;
            //bool bGestion = false;

            //Create el PostedFile
            HttpPostedFile Archivo = txtArchivo.PostedFile;

            if (Archivo.ContentLength > 0)
            {
                ArchivoEnBinario = new Byte[Archivo.ContentLength]; //Crear el array de bytes con la longitud del archivo
                Archivo.InputStream.Read(ArchivoEnBinario, 0, Archivo.ContentLength); //Forzar al control del archivo a cargar los datos en el array
            }

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

            //if (chkPrivado.Checked) bPrivado = true;
            //if (chkLectura.Checked) bLectura = true;
            //if (chkGestion.Checked) bGestion = true;
            //controlar que no se haya subido archivo: e.g. solo link
        //    switch (hdnsTipo.Text)
        //    {
        //        case "AS": //Asunto de bitácora
        //        case "AS_PE": //Asunto de bitácora
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCASU.Insert(null, int.Parse(hdnnItem.Text), ArchivoEnBinario,
        //                                int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
        //                                bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //                case "U":
        //                    DOCASU.Update(null, int.Parse(hdnnItem.Text), ArchivoEnBinario, int.Parse(txtNumEmpleado.Text),
        //                                txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //            }
        //            break;
        //        case "AC": //Ación de bitácora
        //        case "AC_PE": //Ación de bitácora
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCACC.Insert(null, int.Parse(hdnnItem.Text), ArchivoEnBinario,
        //                                int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
        //                                bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //                case "U":
        //                    DOCACC.Update(null, int.Parse(hdnnItem.Text), ArchivoEnBinario, int.Parse(txtNumEmpleado.Text),
        //                                txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //            }
        //            break;
        //        case "AS_PT": //Asunto de bitácora de proyecto técnico
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCASU_PT.Insert(null, int.Parse(hdnnItem.Text), ArchivoEnBinario,
        //                                int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
        //                                bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //                case "U":
        //                    DOCASU_PT.Update(null, int.Parse(hdnnItem.Text), ArchivoEnBinario, int.Parse(txtNumEmpleado.Text),
        //                                txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //            }
        //            break;
        //        case "AC_PT": //Ación de bitácora de proyecto técnico
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCACC_PT.Insert(null, int.Parse(hdnnItem.Text), ArchivoEnBinario,
        //                                int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
        //                                bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //                case "U":
        //                    DOCACC_PT.Update(null, int.Parse(hdnnItem.Text), ArchivoEnBinario, int.Parse(txtNumEmpleado.Text),
        //                                txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //            }
        //            break;
        //        case "AS_T": //Asunto de bitácora de tarea
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCASU_T.Insert(null, int.Parse(hdnnItem.Text), ArchivoEnBinario,
        //                                int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
        //                                bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //                case "U":
        //                    DOCASU_T.Update(null, int.Parse(hdnnItem.Text), ArchivoEnBinario, int.Parse(txtNumEmpleado.Text),
        //                                txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //            }
        //            break;
        //        case "AC_T": //Acción de bitácora de tarea
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCACC_T.Insert(null, int.Parse(hdnnItem.Text), ArchivoEnBinario,
        //                                int.Parse(txtNumEmpleado.Text), txtDescripcion.Text,
        //                                bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //                case "U":
        //                    DOCACC_T.Update(null, int.Parse(hdnnItem.Text), ArchivoEnBinario, int.Parse(txtNumEmpleado.Text),
        //                                txtDescripcion.Text, int.Parse(hdnnIDDOC.Text), bLectura, sNombre, bPrivado, bGestion, txtLink.Text);
        //                    break;
        //            }
        //            break;
        //        case "T": //Tarea
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUT.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUT.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //        case "A": //Actividad
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUA.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUA.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //        case "F": //Fase
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUF.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUF.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //        case "PT": //Proyecto Técnico
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUPT.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUPT.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //        case "PE": //Proyecto Económico
        //        case "PSN": //Proyecto Económico
        //            int nPSN = int.Parse(hdnnItem.Text);
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUPE.Insert(null, nPSN, txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUPE.Update(null, int.Parse(hdnnIDDOC.Text), nPSN, txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //        case "HT": //Hito lineal
        //        case "HM": //Hito discontinuo
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUH.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUH.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //        case "HF": //Hito de fecha
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUHE.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUHE.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, ArchivoEnBinario, bPrivado, bLectura, bGestion, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //        case "OF": //Orden de facturación
        //            switch (hdnsAccion.Text)
        //            {
        //                case "I":
        //                    DOCUOF.Insert(null, int.Parse(hdnnItem.Text), txtDescripcion.Text, sNombre, ArchivoEnBinario, int.Parse(txtNumEmpleado.Text));
        //                    break;
        //                case "U":
        //                    DOCUOF.Update(null, int.Parse(hdnnIDDOC.Text), int.Parse(hdnnItem.Text), txtDescripcion.Text, sNombre, ArchivoEnBinario, int.Parse(Session["GVT_NUM_EMPLEADO_ENTRADA"].ToString()));
        //                    break;
        //            }
        //            break;
        //    }
        }
    }
}