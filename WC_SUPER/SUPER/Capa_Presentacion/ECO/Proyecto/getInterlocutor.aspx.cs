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

public partial class getInterlocutor : System.Web.UI.Page
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

            if (Request.QueryString["idpsn"] != null)
            {
                if (Request.QueryString["ocfa"] != null)
                    ObtenerInterlocutoresOCyFA();
                else
                    ObtenerInterlocutores(int.Parse(Utilidades.decodpar(Request.QueryString["idpsn"].ToString())));
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerInterlocutores(int t305_idproyectosubnodo)
    {
        try
        {
            strTablaHTML = PROYECTOSUBNODO.ObtenerInterlocutores(t305_idproyectosubnodo);
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de interlocutores.", ex);
        }
    }
    protected void ObtenerInterlocutoresOCyFA()
    {
        try
        {
            strTablaHTML = PROYECTOSUBNODO.ObtenerInterlocutoresOCyFA();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de interlocutores.", ex);
        }
    }

}
