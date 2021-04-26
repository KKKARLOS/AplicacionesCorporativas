using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_SIC_Error : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string sH3 = "HTTP 500";
        string sH4 = "Error interno de servidor.";

        try
        {
            if (Request.QueryString["error"] != null) {

                string err = Request.QueryString["error"].ToString();

                switch (err) {
                    case "400":
                        sH3 = "HTTP " + err;
                        sH4 = "Los parámetros que se han recibido de HERMES no son correctos.";
                        break;
                    case "401":
                        sH3 = "HTTP " + err;
                        sH4 = "Usuario no autorizado";
                        break;
                    case "402":
                        sH3 = "HTTP " + err;
                        sH4 = "Acceso no permitido";
                        break;
                }
            }
        }
        catch (Exception)
        {
        }
        finally {
            this.ltrH3.Text = sH3;
            this.ltrH4.Text = sH4;
        }





    }
}