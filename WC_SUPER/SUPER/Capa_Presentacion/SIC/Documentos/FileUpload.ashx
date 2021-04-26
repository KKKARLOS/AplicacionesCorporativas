<%@ WebHandler Language="C#" Class="FileUploadHandler" %>
 
using System;
using System.Web;
using System.Text;
using System.Collections.Generic;

public class FileUploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //System.Threading.Thread.Sleep(4000);
        
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;

        if (context.Request.Files.Count > 0)
        {
            try
            {
                HttpPostedFile file = context.Request.Files[0];

                byte[] content = new byte[file.ContentLength];
                file.InputStream.Read(content, 0, file.ContentLength);

                long t2_iddocumento = IB.Conserva.ConservaHelper.SubirDocumento(System.IO.Path.GetFileName(file.FileName.Trim()), content);

                int size = content.Length / 1024;
                string name = file.FileName;

                if (name.Contains("\\")) {
                    name = name.Substring(name.LastIndexOf("\\") + 1);
                }

                context.Response.Write("{\"t2_iddocumento\": " + t2_iddocumento +
                                        ",\"name\": \"" + name + "\"" + 
                                        ",\"size\": " + size + "}");
            }
            catch (ConservaException cex)
            {
                string msg = cex.InnerException != null ? cex.InnerException.Message : cex.Message;
                context.Response.Write("{\"jquery-upload-file-error\":\"Ocurrió un error subiendo el fichero: " + msg + "\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"jquery-upload-file-error\":\"Ocurrió un error subiendo el fichero: " + ex.Message + "\"}");
            }
        }
        else
            context.Response.Write("{\"jquery-upload-file-error\":\"No se ha detectado ningún fichero\"}");

        

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}