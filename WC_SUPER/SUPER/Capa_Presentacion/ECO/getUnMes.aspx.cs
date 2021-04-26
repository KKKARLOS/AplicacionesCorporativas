using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;


public partial class getUnMes : System.Web.UI.Page
{
    public string sErrores= "";

    protected void Page_Load(object sender, EventArgs e)
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

            if (Request.QueryString["nm"] == null)
            {
                cboMes.Value = DateTime.Now.Month.ToString();
                txtAnno.Text = DateTime.Now.Year.ToString();
            }
            else
            {
                int nAnomes = int.Parse(Utilidades.decodpar(Request.QueryString["nm"].ToString()));
                cboMes.Value = Fechas.AnnomesAFecha(nAnomes).Month.ToString();
                txtAnno.Text = Fechas.AnnomesAFecha(nAnomes).Year.ToString();
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
}
