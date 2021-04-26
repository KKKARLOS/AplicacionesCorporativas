using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;

public partial class ObtenerFoto : System.Web.UI.Page
{
	protected void Page_Load(object sender, System.EventArgs e)
	{
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

		//byte[] bytes = (byte[])dr_resultado["T001_FOTO"];
		//Response.ContentType = "image/gif";
        if (Session["FOTOUSUARIO"] != null)
        {
            Response.BinaryWrite((byte[])Session["FOTOUSUARIO"]);
            //Session["FOTOUSUARIO"] = null;
        }
		// Introducir aquí el código de usuario para inicializar la página
	}

}
