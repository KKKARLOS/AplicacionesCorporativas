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
using GASVI.BLL;
//using Microsoft.JScript;

public partial class Default : System.Web.UI.Page
{
    public string sErrores = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["GVT_IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            if (Request.QueryString["sDesde"] != null || Request.QueryString["sHasta"] != null)
            {
                if (Request.QueryString["sDesde"] != null)
                {
                    cboDesde.Value = int.Parse(Request.QueryString["sDesde"].Substring(4, 2)).ToString();
                    txtDesde.Text = Request.QueryString["sDesde"].Substring(0, 4);
                }
                if (Request.QueryString["sHasta"] != null)
                {
                    cboHasta.Value = int.Parse(Request.QueryString["sHasta"].Substring(4, 2)).ToString();
                    txtHasta.Text = Request.QueryString["sHasta"].Substring(0, 4);
                }
            }
            else
            {
                txtDesde.Text = DateTime.Now.Year.ToString();
                txtHasta.Text = DateTime.Now.Year.ToString();
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
}
