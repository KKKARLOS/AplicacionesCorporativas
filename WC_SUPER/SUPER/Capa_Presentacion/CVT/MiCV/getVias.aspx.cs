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
    public partial class getVias : System.Web.UI.Page
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
                    string sIdCert = Utilidades.decodpar(Request.QueryString["c"].ToString());
                    int idCert = -1;
                    if (sIdCert != "")
                    {
                        idCert = int.Parse(sIdCert);
                        this.txtCertificado.Text = SUPER.BLL.Certificado.GetNombre(null, idCert);

                    }
                    strTablaHtml = GetVias(idCert, int.Parse(Utilidades.decodpar(Request.QueryString["f"].ToString())));
                }
                catch (Exception ex)
                {
                    strErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
        }

        private string GetVias(int IdCert, int idFicepi)
        {
            return SUPER.BLL.Certificado.GetVias(null, IdCert, idFicepi);
        }
	}
}
