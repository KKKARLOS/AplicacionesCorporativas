using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
//Pantalla genérica para mostrar un catálogo de elementos y permitir seleccionar uno de ellos
public partial class Capa_Presentacion_getLista : System.Web.UI.Page
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
            string sTipo = "";
            if (Request.QueryString["t"] != null)
            {
                sTipo = Utilidades.decodpar(Request.QueryString["t"].ToString());
                switch(sTipo)
                {
                    case "ORGCOM":
                        strTablaHTML = SUPER.BLL.OrganizacionComercial.getHtmlNoLigadasNodo();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
}