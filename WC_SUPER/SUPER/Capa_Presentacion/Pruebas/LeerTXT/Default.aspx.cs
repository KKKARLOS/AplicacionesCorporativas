using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.IO;

public partial class Capa_Presentacion_Pruebas_LeerTXT_Default : System.Web.UI.Page
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
        Master.nBotonera = 14;

        if (Page.IsPostBack)
        {
            string a = "";
			HttpPostedFile selectedFile = File1.PostedFile;
            if (selectedFile.ContentLength != 0)
            {
                string sFichero = selectedFile.FileName;
                StreamReader r = new StreamReader(selectedFile.InputStream);
                int i = 0;
                while (r.Peek() > -1)
                {
                    a = r.ReadLine();
                    i++;
                }

            }

        }
    }
}
