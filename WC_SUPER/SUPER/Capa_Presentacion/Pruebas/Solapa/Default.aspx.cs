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

public partial class Capa_Presentacion_Pruebas_Solapa_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // All webpages displaying an ASP.NET menu control must inherit this class.
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.bFuncionesLocales = true;
        Master.TituloPagina = "Solapa";
    }
}
