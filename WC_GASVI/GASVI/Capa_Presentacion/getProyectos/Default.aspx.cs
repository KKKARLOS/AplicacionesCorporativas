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

public partial class Default : System.Web.UI.Page
{
    public string strTablaHTML = "";
    public string sErrores = "";
    public string sOpcion = "";
    public int nUsuario = 0;

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
            if (Request.QueryString["sop"] != null)
                sOpcion = Request.QueryString["sop"].ToString();
            if (Request.QueryString["su"] != null)
                nUsuario = int.Parse(Utilidades.decodpar(Request.QueryString["su"].ToString()));


            switch (sOpcion)
            {
                case "con":
                    this.strTablaHTML = PROYECTO.ObtenerProyectoConsulta((int)Session["GVT_IDFICEPI"]);
                    break;
                default:
                    this.strTablaHTML = PROYECTO.ObtenerProyectoNuevaNota((nUsuario==0)?(int)Session["GVT_USUARIOSUPER"]:nUsuario);
                    break;
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos.", ex);
        }
    }
}
