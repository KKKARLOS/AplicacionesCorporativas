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

using System.Text;
//using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page
{
    public string sErrores = "", strHTML="";

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
            strHTML = SUPER.BLL.ACONSECT.GetAreas(null, "", 2, 0);
        }
        catch (Exception ex)
        {
            sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
}
