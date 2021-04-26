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


public partial class getSolicitudPlazo : System.Web.UI.Page
{
    public string strComentario="";

    protected void Page_Load(object sender, EventArgs e)
    {
        //strComentario = Utilidades.unescape(Request.QueryString["strComentario"]);
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        this.txtSolicitud.Text = strComentario;
    }
}
