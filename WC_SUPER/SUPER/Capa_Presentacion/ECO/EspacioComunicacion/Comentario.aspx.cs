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



public partial class Default : System.Web.UI.Page
{
    public string strComentario;
    public string strTitulo;
    protected string sEstado;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        //strComentario = Utilidades.unescape(Request.QueryString["strComentario"]).Replace("</br>","\n");
        strTitulo = Utilidades.unescape(Request.QueryString["strTitulo"]);
        sEstado = Request.QueryString["estado"];
        this.txtComentario.Text = strComentario;
        
        bool bEstadoLectura = false;

        if (sEstado == "L") bEstadoLectura = true;

        if (bEstadoLectura) ModoLectura.Poner(this.Controls);
    }
}
