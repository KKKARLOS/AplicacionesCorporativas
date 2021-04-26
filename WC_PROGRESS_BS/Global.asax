<%@ Application Language="C#" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.Web.Security" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse la aplicación

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta al cerrarse la aplicación

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        if (Session["strServer"] != null)
        {
            if (Context.Server.GetLastError().Message == "File does not exist."
            || Context.Server.GetLastError().Message == "El archivo no existe."
            || Request.Path.Contains("WebResource.axd"))
            {
                Context.Server.ClearError();
                return;
            }

            MailMessage objMail = new MailMessage();

            objMail.From = new MailAddress("EDA@ibermatica.com", "EDA desde PROGRESS 2.0");
            objMail.To.Add(new MailAddress("EDA@ibermatica.com", "EDA"));
            objMail.Subject = "Error en PROGRESS 2.0";

            //Gets the ASPX Page Name that caused the Error and 
            //the UserHost Address where the Error occured.
            objMail.Body = "Se ha producido un error en la página: " + Request.Path + "\n" +
                "en la dirección IP: " + Request.UserHostAddress + "\n\n";

            //Gets the Error Message
            objMail.Body += "Mensaje de error: " + Context.Server.GetLastError().Message + "\n\n";
            if (HttpContext.Current.Session != null
                && HttpContext.Current.Session["PROFESIONAL"] != null)
            {
                objMail.Body += "Profesional: " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + "\n\n";
                objMail.Body += "Código de Red: " + HttpContext.Current.Session["IDRED"].ToString() + "\n\n";
            }
            //Gets the Detailed Error Message
            objMail.Body += "El origen del error: " + Context.Server.GetLastError().InnerException + "\n\n";

            SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "E")
            {
                myClient.Send(objMail);
            }
        }


    }

    void Session_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse una nueva sesión

    }

    void Session_End(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: el evento Session_End se produce solamente con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer
        // o SQLServer, el evento no se produce.
        //Response.Redirect("~/reconexion.aspx");
        Session.Abandon();

    }
       
</script>
