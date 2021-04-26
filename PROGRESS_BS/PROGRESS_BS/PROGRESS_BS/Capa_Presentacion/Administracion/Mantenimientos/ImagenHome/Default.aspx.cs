using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.Progress.BLL;

public partial class Capa_Presentacion_Administracion_Mantenimientos_ImagenHome_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BLL.ImagenHome cIH = new BLL.ImagenHome();
        try
        {
            HttpContext.Current.Session["nuevaimagenhome"] = null; //inicializar

            this.imghome.Src = "data:image/jpeg;base64," + Convert.ToBase64String(cIH.obtenerImagen());
        }
        catch (Exception)
        {
            PieMenu.sErrores = "msgerr = 'Ocurrió un error obteniendo la imagen de la HOME';";
        }
        finally
        {
            cIH.Dispose();
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getBase64ImageFromSession()
    {
        string base64img = "";

        if (HttpContext.Current.Session["nuevaimagenhome"] != null)
        {
            base64img = Convert.ToBase64String((byte[])HttpContext.Current.Session["nuevaimagenhome"]);
        }

        return base64img;

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void grabarImagen()
    {
        BLL.ImagenHome cImagenHome = new BLL.ImagenHome();

        try
        {
            if (HttpContext.Current.Session["nuevaimagenhome"] != null)
            {
                cImagenHome.subirImagen((byte[])HttpContext.Current.Session["nuevaimagenhome"]);                
                HttpContext.Current.Session["nuevaimagenhome"] = null;
            }
            else
                throw new Exception("No hay una nueva imagen para grabar.");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cImagenHome.Dispose();
        }
    }

}