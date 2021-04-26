using System;
using System.Collections.Generic;
using System.Web.UI;
using IB.SUPER.Shared;
using Shared = IB.SUPER.Shared;
using System.Web.Script.Serialization;

public partial class Capa_Presentacion_APP_Exportaciones_ExportarExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] myArray = null;
        string sFilename = "", stituloExcel = "";
        List<Dictionary<string, string>> schema, datos;        

        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;

        try
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            sFilename = Request.Form["ifrmExportarFilename"].ToString() + ".xlsx";
            stituloExcel = Request.Form["ifrmExportarTitulo"].ToString();
            schema = serializer.Deserialize<List<Dictionary<string, string>>>(Request.Form["ifrmExportarSchema"]);
            datos = serializer.Deserialize<List<Dictionary<string, string>>>(Request.Form["ifrmExportarDatos"]);

            myArray = Exportaciones.exportarExcel(schema, datos, sFilename, stituloExcel);

            if (myArray == null)
            {
                Response.ContentType = "text/HTML";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "scripterr", "parent.IB.bserror.mostrarAvisoAplicacion('Exportación a Excel','La exportación a excel no ha producido ningún resultado.')", true);
            }
            else
                if (Response.IsClientConnected)
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sFilename + "\"");
                    Response.ContentType = "application/vnd.ms-excel";
                    if (System.Web.HttpContext.Current.Request.Browser.Browser.ToString() == "Chrome") Response.AddHeader("Content-Length", "999999999999");
                    Response.BinaryWrite(myArray);
                    Response.Flush();
                    Response.Close();
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

        }
        catch (Exception ex)
        {
            Response.ContentType = "text/HTML";
            Shared.LogError.LogearError("Error al realizar la exportación a excel", ex);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "scripterr", "parent.IB.bserror.mostrarErrorAplicacion('Error de aplicación','Se ha producido un error durante la generación del documento de exportación.')", true);
        }
    }
}