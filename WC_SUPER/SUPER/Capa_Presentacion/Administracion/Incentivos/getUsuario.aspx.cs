using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class getUsuario : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML="";
    public int nIberper = 0;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            if (Request.QueryString["nF"] != null)
            {
                nIberper = int.Parse(Utilidades.decodpar(Request.QueryString["nF"].ToString()));
                lblProfesional.Text = Utilidades.decodpar(Request.QueryString["sP"].ToString());
            }

            string[] aDatos = Regex.Split(INCENTIVOSPRODUCTIVIDAD.ObtenerUsuarios(nIberper), "@#@");
            strTablaHTML = aDatos[0];
            lblProfesional.Text = aDatos[2];
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

}
