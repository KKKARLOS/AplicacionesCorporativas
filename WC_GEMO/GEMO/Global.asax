<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Security" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="GEMO" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)  
    {
        // Code that runs on application startup
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
    }
        
    void Application_Error(Object sender, EventArgs e)
    {
        if (Context.Server.GetLastError().Message == "File does not exist."
            || Context.Server.GetLastError().Message == "El archivo no existe."
            || Context.Server.GetLastError().Message == "Subproceso anulado."
            || Context.Server.GetLastError().Message == "La ruta de acceso 'OPTIONS' no está permitida."
            || Request.Path.Contains("WebResource.axd"))
        {
            Context.Server.ClearError();
            return;
        }
        string sRteError = GEMO.BLL.Correo.RteMailError();
        string sToError = GEMO.BLL.Correo.DestMailError();
                
        MailMessage objMail = new MailMessage();

        //objMail.From = new MailAddress("EDA@ibermatica.com", "EDA desde GEMO.net");
        //objMail.To.Add(new MailAddress("EDA@ibermatica.com", "EDA"));
        objMail.From = new MailAddress(sRteError, "EDA desde GEMO");
        objMail.To.Add(new MailAddress(sToError, "EDA"));

        objMail.Subject = "Error en GEMO.net";

        //Gets the ASPX Page Name that caused the Error and 
        //the UserHost Address where the Error occured.
        objMail.Body = "Se ha producido un error en la página: " + Request.Path + "\n" +
            "en la dirección IP: " + Request.UserHostAddress + "\n\n";

        //Gets the Error Message
        objMail.Body += "Mensaje de error: " + Context.Server.GetLastError().Message + "\n\n";
        if (HttpContext.Current.Session != null)
        {
            objMail.Body += "Usuario: " + HttpContext.Current.Session["APELLIDO1"].ToString() + " ";
            objMail.Body += HttpContext.Current.Session["APELLIDO2"].ToString() + ", ";
            objMail.Body += HttpContext.Current.Session["NOMBRE"].ToString() + "\n\n";
            objMail.Body += "Código de Red: " + HttpContext.Current.Session["IDRED"].ToString() + "\n\n";
        }
        //Gets the Detailed Error Message
        objMail.Body += "El origen del error: " + Context.Server.GetLastError().InnerException + "\n\n";

        SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
        try
        {
            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "E")
            {
                myClient.Send(objMail);
            }
        }
        catch (SmtpFailedRecipientsException ex)
        {
            for (int i = 0; i < ex.InnerExceptions.Length; i++)
            {
                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                if (status == SmtpStatusCode.MailboxBusy || status == SmtpStatusCode.MailboxUnavailable)
                {
                    //Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                    System.Threading.Thread.Sleep(5000);
                    myClient.Send(objMail);
                }
                else
                {
                    //Console.WriteLine("Failed to deliver message to {0}", ex.FailedRecipient[i]);
                }
            }
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    protected void Session_End(Object sender, EventArgs e)
    {
        Session.Abandon();
    }
       
</script>
