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

public partial class Capa_Presentacion_Pruebas_ROBOMOVILWS_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        //Response.ContentType = "text/html; charset=utf-8";
        //ROBOMOVILWS.SMS oWS = new ROBOMOVILWS.SMS();
        //Response.Write(oWS.ConsultarEnvios("CATU", "CLAVECATU", "01/03/2010", "11/05/2011"));
        Response.Flush();
        Response.End();
    }
}
