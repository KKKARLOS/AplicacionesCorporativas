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
using GASVI.BLL;

public partial class Capa_Presentacion_Motivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["sop"] != "")
        {
            switch (Request.QueryString["sop"].ToString()){
                case "ac":
                    lblMotivo.InnerText = "Motivo de la no aceptación";
                    imgNO.ImageUrl = "~/images/imgNoAceptar40.gif";
                    break;
                case "ap":
                    lblMotivo.InnerText = "Motivo de la no aprobación";
                    imgNO.ImageUrl = "~/images/imgNoAprobar40.gif";
                    break;
                case "an":
                    lblMotivo.InnerText = "Motivo de la anulación";
                    imgNO.ImageUrl = "~/images/imgAnular40.gif";
                    break;
            }
        }
    }
}
