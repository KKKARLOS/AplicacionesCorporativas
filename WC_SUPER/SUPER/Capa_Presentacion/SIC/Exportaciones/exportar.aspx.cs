using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Shared = IB.SUPER.Shared;

public partial class Capa_Presentacion_SIC_Exportaciones_exportar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] bytearr = null;
        string sFilename = "";

        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;


        BLL.Exportaciones c = new BLL.Exportaciones();

        try
        {
            Hashtable ht = Shared.Utils.ParseQuerystring(Request.QueryString.ToString());

            sFilename = ht["exportid"].ToString() + ".xlsx";

            bytearr = c.ExportarExcel(ht["exportid"].ToString(), ht["origenmenu"].ToString(), ht["filters"].ToString());

            c.Dispose();

            if (bytearr == null)
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
                    Response.BinaryWrite(bytearr);
                    Response.Flush();
                    Response.Close();
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

        }
        catch (Exception ex)
        {
            Response.ContentType = "text/HTML";

            c.Dispose();
            Shared.LogError.LogearError("Error al realizar la exportación a excel", ex);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "scripterr", "parent.IB.bserror.mostrarErrorAplicacion('Error de aplicación','Se ha producido un error durante la generación del documento de exportación.')", true);
        }




    }
}