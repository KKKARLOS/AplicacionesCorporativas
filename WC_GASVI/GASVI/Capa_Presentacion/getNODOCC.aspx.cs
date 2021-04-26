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

public partial class Capa_Presentacion_getNODOCC : System.Web.UI.Page
{
    public string strTablaHTML = "";
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
            this.strTablaHTML = CentrosCoste.ObtenerNodosCCIberper(Utilidades.decodpar(Request.QueryString["sb"].ToString()));
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los centros de responsabilidad.", ex);
        }
    }
}
