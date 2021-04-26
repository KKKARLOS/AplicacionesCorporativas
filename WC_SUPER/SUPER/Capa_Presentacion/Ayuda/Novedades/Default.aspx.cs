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
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Ayuda_Novedades_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HAYNOVEDADES"].ToString() == "1" && Session["NOVEDADESLEIDAS"].ToString() == "0")
        {
            try
            {
                LECTURANOVEDAD.Insert(null, byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
                Session["NOVEDADESLEIDAS"] = "1";
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al establecer las novedades como leídas.", ex);
            }
        }
    }
}
