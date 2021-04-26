using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class Contacto_Mensaje : System.Web.UI.Page//, ICallbackEventHandler
    {
        public string EsPostBack = "false";

        private void Page_Load(object sender, System.EventArgs e)
        {
            Session["bSubido"] = false;
            try
            {
                if (Session["IDRED"] == null)
                {
                    try { Response.Redirect("~/SesionCaducadaModal.aspx", true); }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (!Page.IsCallback)
                {
                    if (Page.IsPostBack)
                    {
                        Session["bSubido"] = true;
                        Enviar();
                        EsPostBack = "true";
                    }
                }
            }
            catch (System.OutOfMemoryException)
            {
                //Si el archivo a subir es demasiado grande, se produce un error por
                //falta de memoria. La ventana de la barra de progreso ya avisa al usuario de
                //esta situación y cierre esta ventana.
            }
        }
        private void Enviar()
        {
            string strFileNameOnServer = "";
            ArrayList aListCorreo = new ArrayList();

            try
            {
                string sTO = ConfigurationManager.AppSettings["CorreoDEF"].ToString();

                HttpPostedFile selectedFile = txtArchivo.PostedFile;
                if (selectedFile.ContentLength > 0)
                {
                    string sArchivo = "";
                    string sAux = selectedFile.FileName;
                    int nPos = sAux.LastIndexOf("\\");
                    sArchivo = sAux.Substring(nPos + 1, sAux.Length - nPos - 1);

                    strFileNameOnServer = Request.PhysicalApplicationPath + "Upload\\" + sArchivo;
                    selectedFile.SaveAs(strFileNameOnServer);
                }
                string sTexto= this.txtComentario.Value.Replace(((char)10).ToString(), "<br />");
                string[] aMail = { Session["DES_EMPLEADO_ENTRADA"].ToString(), sTexto, sTO, strFileNameOnServer };
                aListCorreo.Add(aMail);

                SUPER.Capa_Negocio.Correo.EnviarCorreosCAUDEF(aListCorreo);
                //this.hdnResul.Value = "Error provocado";
            }
            catch (Exception e)
            {
                this.hdnResul.Value = e.Message;
            }
            //finally
            //{
            //El método de envío de correo se encarga del borrado del fichero
            //if (strFileNameOnServer != "") File.Delete(strFileNameOnServer);
            //}
        }
    }
}