using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
public partial class Capa_Presentacion_Pruebas_ImageData_Default : System.Web.UI.Page
{
    public string sImageData = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sImageData = "data:image/png;base64," + Utilidades.ImageToBase64(Request.PhysicalApplicationPath + "Images\\imgComentarioAzul.png", System.Drawing.Imaging.ImageFormat.Png);
                    //    string b = Utilidades.ImageToBase64(a1, System.Drawing.Imaging.ImageFormat.Png);

            //    //sImage = sTabla.Replace(sImage.ToString(), "src='data:image/gif;base64," + b + "'");

    }
}
