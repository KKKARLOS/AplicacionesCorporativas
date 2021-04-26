<%@ WebHandler Language="C#" Class="FileDownload" %>

using System;
using System.Web;

public class FileDownload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {

            byte[] oArchivo = null;
            IB.Conserva.svcConserva.CSVDocument oDoc = new IB.Conserva.svcConserva.CSVDocument();

            System.Collections.Hashtable ht = IB.SUPER.Shared.Utils.ParseQuerystring(context.Request.QueryString.ToString());
            long t2_iddocumento = long.Parse(ht["t2_iddocumento"].ToString());


            oDoc = IB.Conserva.ConservaHelper.ObtenerDocumento((long)t2_iddocumento);
            oArchivo = oDoc.content;

            if (oArchivo != null)
            {
                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.Buffer = true;
                context.Response.ContentType = ContentType(oDoc.docName);
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDoc.docName + "\"");
                context.Response.BinaryWrite(oArchivo);

                context.Response.Flush();
            }


        }
        catch (ConservaException cex)
        {
            string msg = IB.SUPER.Shared.Utils.MsgErrorConserva("R", cex);

            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.Buffer = true;
            context.Response.ContentType = "text/html";
            context.Response.Write("<script>window.parent.IB.bserror.mostrarErrorAplicacion('Descarga de fichero','Ocurrió un error en la descarga de fichero:<br/><br/>" + msg + "');</script>");
        }
        catch (Exception ex)  //Javi 18.05.2015 Controlar excepcion general
        {
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.Buffer = true;
            context.Response.ContentType = "text/html";
            context.Response.Write("<script>window.parent.IB.bserror.mostrarErrorAplicacion('Descarga de fichero','Ocurrió un error en la descarga de fichero:<br/><br/>" + ex.Message + "');</script>");
        }
    }

    private string ContentType(string fileName)
    {
        int pos = 0;
        string ext;
        pos = fileName.LastIndexOf(".");
        ext = fileName.Substring(pos + 1, fileName.Length - pos - 1);

        switch (ext)
        {
            case "txt":
                return "text/plain";
            case "doc":
                return "application/ms-word";
            case "xls":
                return "application/vnd.ms-excel";
            case "gif":
                return "image/gif";
            case "jpg":
            case "jpeg":
                return "image/jpeg";
            case "bmp":
                return "image/bmp";
            case "wav":
                return "audio/wav";
            case "ppt":
                return "application/mspowerpoint";
            case "dwg":
                return "image/vnd.dwg";
            case "pdf":
                return "application/pdf";
            default:
                return "application/octet-stream";
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}