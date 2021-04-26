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
<%@ Import Namespace="GESTAR" %>
<%@ Import Namespace="System.Reflection" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        System.Configuration.ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["LocalSqlServer"];
        System.Reflection.FieldInfo fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        fi.SetValue(settings, false);

        if (ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString.ToString() == "E")
            ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServerE"].ConnectionString;
        else
            ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServerD"].ConnectionString;
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
            || Request.Path.Contains("WebResource.axd"))
        {
            Context.Server.ClearError();
            return;
        }
        string sRteError = RteMailError();
        string sToError = DestMailError();
        
        MailMessage objMail = new MailMessage();

        //objMail.From = new MailAddress("EDA@ibermatica.com", "EDA desde GESTAR 2.0");
        //objMail.To.Add(new MailAddress("EDA@ibermatica.com", "EDA"));
        objMail.From = new MailAddress(sRteError, "EDA desde GESTAR");
        objMail.To.Add(new MailAddress(sToError, "EDA"));

        objMail.Subject = "Error en GESTAR";

        //Gets the ASPX Page Name that caused the Error and 
        //the UserHost Address where the Error occured.
        objMail.Body = "Se ha producido un error en la página: " + Request.Path + "\n" +
            "en la dirección IP: " + Request.UserHostAddress + "\n\n";

        //Gets the Error Message
        objMail.Body += "Mensaje de error: " + Context.Server.GetLastError().Message + "\n\n";
        if (HttpContext.Current.Session != null)
        {
            objMail.Body += "Usuario: " + HttpContext.Current.Session["NOMBRE"].ToString() + " ";
            //objMail.Body += "Usuario: " + HttpContext.Current.Session["APELLIDO1"].ToString() + " ";
            //objMail.Body += HttpContext.Current.Session["APELLIDO2"].ToString() + " ";
            //objMail.Body += HttpContext.Current.Session["NOMBRE"].ToString() + "\n\n";
            objMail.Body += "Código de Red: " + HttpContext.Current.Session["IDRED"].ToString() + "\n\n";
        }
        //Gets the Detailed Error Message
        objMail.Body += "El origen del error: " + Context.Server.GetLastError().InnerException + "\n\n";

        //SmtpMail.SmtpServer = "IBMBXNORTE";//"localhost";
        //SmtpMail.Send(objMail);


        SmtpClient myClient = new SmtpClient("OUTLOOK.INTRANET.IBERMATICA");
        try
        {
            // UNA VEZ QUE TENEMOS PERMISOS DE ACCESO AL SERVIDOR, PUEDE QUE SIGAMOS
            // EN LA PRÁCTICA SIN PODER CONECTARNOS POR EL ANTIVIRUS QUE ESTÉ EN LA
            // MÁQUINA. TENDRIAMOS QUE DECIRLE QUE SI ES  (WEBDEV.WEBSERVER.EXE)
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString.ToUpper() == "E")
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

	//Ensure SessionID in order to prevent the folloing exception
	//when the Application Pool Recycles
	//[HttpException]: Session state has created a session id, but cannot 
	//				   save it because the response was already flushed by 
	    string sessionId = Session.SessionID;
    }

    protected void Session_End(Object sender, EventArgs e)
    {
        Session.Abandon();
    }
    /// <summary>
    /// Obtiene el buzón destinatario de correos de error
    /// </summary>
    /// <returns></returns>
    public static string DestMailError()
    {
        string sToError = "AMS-DIS-ERRAPP@ibermatica.com";
        try
        {
            sToError = ConfigurationManager.AppSettings["SMTP_to"].ToString();
            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
                sToError = ConfigurationManager.AppSettings["SMTP_to_DES"].ToString();

            if (sToError == "") sToError = "AMS-DIS-ERRAPP@ibermatica.com";
        }
        catch
        {
            sToError = "AMS-DIS-ERRAPP@ibermatica.com";
        }
        return sToError;
    }
    /// <summary>
    /// Obtiene el buzón remitente de correos de error
    /// </summary>
    /// <returns></returns>
    public static string RteMailError()
    {
        string sRte = "EDA@ibermatica.com";
        try
        {
            sRte = ConfigurationManager.AppSettings["SMTP_from"].ToString();
            if (sRte == "") sRte = "EDA@ibermatica.com";
        }
        catch
        {
            sRte = "EDA@ibermatica.com";
        }
        return sRte;
    }
       
</script>
