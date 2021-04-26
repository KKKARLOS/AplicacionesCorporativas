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
using System.Text;
using System.Text.RegularExpressions;

public partial class getValidador : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sIdPE = "-1", sIdExpProf="-1";
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

            if (Request.QueryString["ep"] != null)
            {
                sIdExpProf = Utilidades.decodpar(Request.QueryString["ep"].ToString());
                ObtenerValidadoresExperiencia(int.Parse(sIdExpProf));
            }
            else
            {
                if (Request.QueryString["pr"] != null)
                    sIdPE = Utilidades.decodpar(Request.QueryString["pr"].ToString());
                ObtenerValidadores(int.Parse(sIdPE));
            }
            
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerValidadores(int t301_idproyecto)
    {
        try
        {
            strTablaHTML = PROYECTO.GetValidadores(null, t301_idproyecto);
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de validadores.", ex);
        }
    }
    protected void ObtenerValidadoresExperiencia(int t808_idexpprof)
    {
        try
        {
            strTablaHTML = PROYECTO.GetValidadoresExperiencia(null, t808_idexpprof);
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de validadores de la experiencia.", ex);
        }
    }

}
