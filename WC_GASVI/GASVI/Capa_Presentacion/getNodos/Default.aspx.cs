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
using Microsoft.JScript;
using System.Text.RegularExpressions;
using System.Text;
using GASVI.BLL;
using EO.Web;
using System;


public partial class Default : System.Web.UI.Page
{
    public string strTablaHtml;
    public SqlConnection oConn;
    public SqlTransaction tr;
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
            strTablaHtml = ACUERDOGV.obtenerNodos();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos de los nodos", ex);
        }
    }
}