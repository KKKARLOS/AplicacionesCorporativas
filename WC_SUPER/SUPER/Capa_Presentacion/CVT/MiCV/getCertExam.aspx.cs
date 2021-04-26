using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

namespace SUPER.Capa_Presentacion
{
    public partial class getCertExam : System.Web.UI.Page
    {
        public string strErrores;
        public string strTablaHtml;

		private void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsCallback)
            {
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
                    string sIdExam = Utilidades.decodpar(Request.QueryString["e"].ToString());
                    int idExam = -1;
                    if (sIdExam != "")
                    {
                        idExam = int.Parse(sIdExam);
                        //this.txtExamen.Text = SUPER.BLL.Certificado.GetNombre(null, idExam);
                        this.txtExamen.Text = SUPER.BLL.Examen.GetNombre(null, idExam);
                    }
                    strTablaHtml = GetCertificados(idExam, int.Parse(Utilidades.decodpar(Request.QueryString["f"].ToString())));
                }
                catch (Exception ex)
                {
                    strErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
        }

        private string GetCertificados(int idExam, int idFicepi)
        {
            return SUPER.BLL.Examen.GetCertificados(null, idExam, idFicepi);
        }
	}
}
