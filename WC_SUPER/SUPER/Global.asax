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
<%@ Import Namespace="SUPER" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ Import Namespace="System.Reflection" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        System.Configuration.ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["LocalSqlServer"];
        System.Reflection.FieldInfo fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        fi.SetValue(settings, false);

        if (ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString == "E")
            ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServerE"].ConnectionString;
        else if (ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString == "D")
            ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServerD"].ConnectionString;
        else
            ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer" + ConfigurationManager.AppSettings["ENTORNO"]].ConnectionString;

        #region Licencia controles EO
        EO.Web.Runtime.AddLicense(
            "GbW4xd+ybKm2yNyxdabw+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn" +
            "9hnyntzCnrWfWZekzQzrpeb7z7iJWZekscufWZfA8g/jWev9ARC8W8Tp/yCh" +
            "We3pAx7oqOXBs9yvZ6emsdq9RoGkscufWZeksefgndukBSTvnrSm5BfondzR" +
            "9hn0W5f69h3youbyzs2waaW0s8uud4SOscufWZekscu7mtvosR/4qdzBs//g" +
            "m8r4AxTvW5f69h3youbyzs2waaW0s8uud4SOscufWZekscu7mtvosR/4qdzB" +
            "s//xntza+hD2W5f69h3youbyzs2waaW0s8uud4SOscufWZekscu7mtvosR/4" +
            "qdzBs/j0pevt4QzmnpmkBxDxrODz/+ihaqeywc2faLWRm8ufWZekscufddjo" +
            "9cvzsufpzs3CmuPp/w/gq5mkBxDxrODz/+ihaqeywc2faLWRm8ufWZekscuf" +
            "ddjo9cvzsufpzs3CmuPw8wzipJmkBxDxrODz/+ihaqeywc2faLWRm8ufWZek" +
            "scufddjo9cvzsufpzs3Ag7jc5hvrqNjo9h2hWe3pAx7oqOXBs9yvZ6emsdq9" +
            "RoGkscufWZeksefgndukBSTvnrSm1RTgpebrs8v1nun3+hrtdpm1wdmvW5ez" +
            "z7iJWZekscufWZfA8g/jWev9ARC8W8r0/RTzrdz2s8v1nun3+hrtdpm1wdmv" +
            "W5ezz7iJWZekscufWZfA8g/jWev9ARC8W8TlBBbknbzo+h+hWe3pAx7oqOXB" +
            "s9yvZ6emsdq9RoGkscufWZeksefgndukBSTvnrSm2B3onZmkBxDxrODz/+ih" +
            "aqeywc2faLWRm8ufWZekscufddjo9cvzsufpzs3CqOPzA/vonOLpA82fr9z2" +
            "BBTup7SmwtutaZmkwOmMQ5ekscufWZekzQzjnZf4ChvkdpnXARDrpbrs9g7q" +
            "nummsSHkq+rtABm8W6i0v9uhWabCnrWfWZekscufWbPl9Q+frfD09uihftvt" +
            "BRrxW5f69h3youbyzs2waaW0s8uud4SOscufWZekscu7mtvosR/4qdzBs/Ts" +
            "mt7p6xruppmkBxDxrODz/+ihaqeywc2faLWRm8ufWZekscufddjo9cvzsufp" +
            "zs3DqO7y/Rrgndz2s8v1nun3+hrtdpm1wdmvW5ezz7iJWZekscufWZfA8g/j" +
            "Wev9ARC8W73wAAzznummsSHkq+rtABm8W6i0v9uhWabCnrWfWZekscufWbPl" +
            "9Q+frfD09uihjOPt9RChWe3pAx7oqOXBs9yvZ6emsdq9RoGkscufWZeksefg" +
            "ndukBSTvnrSm1xf4qOz4s8v1nun3+hrtdpm1wdmvW5ezz7iJWZekscufWZfA" +
            "8g/jWev9ARC8W7zo+h/gm+Pp3QzhnuOmsSHkq+rtABm8W6i0v9uhWabCnrWf" +
            "WZekscufWbPl9Q+frfD09uihheD3Be3usZmkBxDxrODz/+ihaqeywc2faLWR" +
            "m8ufWZekscufddjo9cvzsufpzs3CqOTmAO3usZmkBxDxrODz/+ihaqeywc2f" +
            "aLWRm8ufWZekscufddjo9cvzsufpzs3Cmuf49BPgW5f69h3youbyzs2waaW0" +
            "s8uud4SOscufWZekscu7mtvosR/4qdzBs//uqOPY+huhWe3pAx7oqOXBs9yv" +
            "Z6emsdq9RoGkscufWZeksefgndukBSTvnrSm4wzzouXrs8v1nun3+hrtdpm1" +
            "wdmvW5ezz7iJWZekscufWZfA8g/jWev9ARC8W8rw+g/kq5mkBxDxrODz/+ih" +
            "aqeywc2faLWRm8ufWZfAwAzrpeb7z7iJWZeksefuq9vpA/Ttn+ak9QzznrSm" +
            "x9qwbaa2wdyxW5f69h3youbyzs2waZmkwOmMQ5ekscu7rODr/wzzrunpz/32" +
            "sanLw9zWr6nrxxPiprq5/vXPh8jBzueurODr/wzzrunpz7iJdabw+g7kp+rp" +
            "z7iJdePt9BDtrNzCnrWfWZekzRfonNzyBBDInQ==");
        SUPER.BLL.Log.InicializaLog();
        #endregion
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown6
    }

    void Application_Error(Object sender, EventArgs e)
    {
        if (Context.Server.GetLastError().Message == "File does not exist."
            || Context.Server.GetLastError().Message.Contains("eo_web.ashx")
            //|| Context.Server.GetLastError().Message.Contains("Thread was being aborted")
            || Context.Server.GetLastError().Message == "Thread was being aborted."
            || Context.Server.GetLastError().Message == "El archivo no existe."
            || Context.Server.GetLastError().Message == "Subproceso anulado."
            || Context.Server.GetLastError().Message == "La ruta de acceso 'OPTIONS' no está permitida."
            || Request.Path.Contains("WebResource.axd"))
        {
            Context.Server.ClearError();
            return;
        }

        //Si no hay sesión, no envío correo
        if (HttpContext.Current.Session["IDRED"] != null)
        {
            string sRteError = SUPER.Capa_Negocio.Errores.RteMailError();
            string sToError = SUPER.Capa_Negocio.Errores.DestMailError();
            
            MailMessage objMail = new MailMessage();
            objMail.From = new MailAddress(sRteError, "EDA desde SUPER");
            objMail.To.Add(new MailAddress(sToError, "EDA"));
            objMail.Subject = "Error en SUPER";

            if (HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"] != null)
            {
                objMail.Body += "Profesional: " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + "\n\n";
                objMail.Body += "Código de Red: " + HttpContext.Current.Session["IDRED"].ToString() + "\n\n";
            }
            //Gets the ASPX Page Name that caused the Error and 
            //the UserHost Address where the Error occured.
            objMail.Body = "Se ha producido un error en la página: " + Request.Path + "\n" +
                "en la dirección IP: " + Request.UserHostAddress + "\n\n";

            //Gets the Error Message
            objMail.Body += "Mensaje de error: " + Context.Server.GetLastError().Message + "\n\n";
            //Gets the Detailed Error Message
            objMail.Body += "El origen del error: " + Context.Server.GetLastError().InnerException + "\n\n";

            SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "E")
            {
                myClient.Send(objMail);
            }
        }
        Context.Server.ClearError();
        if (HttpContext.Current.Session != null && HttpContext.Current.Session["VERSIONAPP"].ToString() == "bs")
        {
            Context.Response.Redirect("~/bsError.aspx");
        }
        else
        {
            Context.Response.Redirect("~/Error.aspx");
        }        
    } 

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }
    
    void Session_End(Object sender, EventArgs e)
    {
        //Utilidades.DeleteUsuario(Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\")[Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\").GetLength(0) - 1]);
        try
        {
            Response.Redirect("~/SesionCaducada.aspx", true);
        }
        catch (System.Threading.ThreadAbortException) { }
        Session.Abandon();
    }
    // this event is executed for every page request
    // and is run before the PreInit event. Therefore we
    // can use it to set the theme and master for every page
/*    
    protected void Application_PreRequestHandlerExecute(object sender, System.EventArgs e)
    {
        // cast the current handler to a page object
        Page p = HttpContext.Current.Handler as Page;

        if (p != null)
        {
            // add an event handler for the PreInit event (the master page needs to be set there)
            p.PreInit += new EventHandler(p_PreInit);
        }
    }

    void p_PreInit(object sender, EventArgs e)
    {
        // cast the current handler to a page object
        Page p = HttpContext.Current.Handler as Page;

        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // All webpages displaying an ASP.NET menu control must inherit this class.
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            p.ClientTarget = "uplevel";        
    }
 */   
</script>