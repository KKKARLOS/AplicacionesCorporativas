<%@ WebHandler Language="C#" Class="UploadImage" %>

using System;
using System.Web;

using BLL = IB.Progress.BLL;

public class UploadImage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        context.Response.ContentType = "text/json";

        BLL.ImagenHome cIH = new BLL.ImagenHome();
        
        try
        {
            HttpPostedFile file = context.Request.Files[0];
            byte[] buffer = new byte[file.ContentLength];
            file.InputStream.Read(buffer, 0, file.ContentLength);

            //guardar imagen en sessión           
            context.Session["nuevaimagenhome"] = buffer;

            context.Response.Write("{}");
            

        }
        catch (Exception ex)
        {

            context.Response.Write("{\"error\" : \"" + ex.Message + "\"}");

        }            
        finally
        {
            cIH.Dispose();
        }

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}