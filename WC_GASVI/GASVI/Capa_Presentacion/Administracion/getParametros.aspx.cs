using System;
using GASVI.BLL;

public partial class getParametros : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            strTablaHTML = Administracion.ObtenerParametros(int.Parse(Utilidades.decodpar(Request.QueryString["nIdConsulta"].ToString())));
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los parámetros", ex);
        }

    }
}
