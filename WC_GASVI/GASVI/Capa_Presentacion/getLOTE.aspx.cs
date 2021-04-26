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
            int nLote = 0;
            if (Request.QueryString["nL"] != null)
                nLote = int.Parse(Utilidades.decodpar(Request.QueryString["nL"].ToString()));

            this.Title = "Lote nº " + nLote.ToString("#,###") + this.Title;
            this.strTablaHTML = CABECERAGV.ObtenerNotasDeUnLote(nLote);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener las solicitudes que conforman el lote.", ex);
        }
    }
}
