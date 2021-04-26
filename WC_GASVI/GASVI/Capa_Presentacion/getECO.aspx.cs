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

public partial class Capa_Presentacion_getECO : System.Web.UI.Page
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
            this.strTablaHTML = DESPLAZAMIENTO.ObtenerDesplazamientosRangoFechas(
                                        int.Parse(Utilidades.unescape(Request.QueryString["in"].ToString())), //Interesado-Beneficiario
                                        DateTime.Parse(Utilidades.unescape(Request.QueryString["ini"].ToString())), //desde
                                        DateTime.Parse(Utilidades.unescape(Request.QueryString["fin"].ToString())), //hasta
                                        int.Parse(Utilidades.unescape(Request.QueryString["ref"].ToString())) //referencia GASVI
                                        );
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos.", ex);
        }
    }
}
