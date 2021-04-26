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
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using GASVI.BLL;
using System.Text;

public partial class Default : System.Web.UI.Page
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
            this.strTablaHTML = ACUERDOGV.obtenerAcuerdosParaAsignacion(Utilidades.decodpar(Request.QueryString.Get("su").ToString()),
                                Utilidades.decodpar(Request.QueryString.Get("sp").ToString()));
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos.", ex);
        }

    }
}
