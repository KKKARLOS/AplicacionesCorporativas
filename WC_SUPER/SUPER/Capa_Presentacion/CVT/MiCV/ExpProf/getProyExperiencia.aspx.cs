using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using SUPER.BLL;
using System.Text;
using System.Text.RegularExpressions;

public partial class getProyExperiencia : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
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

            string sIdExpProf="-1";

            if (Request.QueryString["ep"] != null)
            {
                sIdExpProf = Utilidades.decodpar(Request.QueryString["ep"].ToString());
            }
            ObtenerProyectosByExperiencia(int.Parse(sIdExpProf));
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerProyectosByExperiencia(int t808_idexpprof)
    {
        try
        {
            strTablaHTML = EXPPROFPROYECTO.ObtenerProyectosByExperiencia(t808_idexpprof);
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de validadores de la experiencia.", ex);
        }
    }

}
