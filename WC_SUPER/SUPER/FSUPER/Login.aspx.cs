using System;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.Security;

public partial class Login : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //this.txtUser.Text = Utilidades.decodpar(Request.QueryString["u"].ToString());//usuario
            //this.txtPasswAnt.Text = Utilidades.decodpar(Request.QueryString["p"].ToString());//password
            if (Request.QueryString["u"] != null)
            {
                //this.txtUser.Text = Request.QueryString["u"].ToString();//usuario
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@";

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("UserTest"):
                sResultado += Autenticacion(aArgs[1], aArgs[2], int.Parse(aArgs[3]));
                break;
            case ("UserAuth"):
                sResultado += Autenticacion(aArgs[1], aArgs[2], int.Parse(aArgs[3]));
                break;
            case ("Login"):
                sResultado += Autenticacion(aArgs[1], aArgs[2], int.Parse(aArgs[3]));
                break;
            case ("Remember"):
                sResultado += btnRecordar(aArgs[1]);
                break;
            case ("ChangePass"):
                sResultado += cambiarPass(aArgs[1], aArgs[2]);
                break;
            case ("ChangeQuestion"):
                sResultado += cambiarPregunta(aArgs[1], aArgs[2]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }


    private string Autenticacion(string UserName, string Password, int numIntentos)
    {
        string sResul = "";
        string sIntentos = "";
        //SUPER.Capa_Negocio.cLog miLog = new SUPER.Capa_Negocio.cLog();
        Session["MULTIUSUARIO"] = false;
        Session["MostrarMensajeBienvenida"] = false;
        Session["MSGBIENVENIDA"] = "";
        Session["BIENVENIDAMOSTRADA"] = true;
        Session["TiempoMensajeBienvenida"] = 0;

        PonerMensaje("");
        //miLog.put("Entro en Login1_Authenticate");
        SUPER.DAL.Log.Insertar("Entro en Login.aspx Autenticacion, UserName=" + UserName + " Password=" + Password + " numIntentos=" + numIntentos.ToString());
        string sUsuario = "FOR_" + UserName.ToUpper();
        string sPassword = Password.ToUpper();
        //if (ValidateUser(Login1.UserName, sPassword))
        if (sUsuario != "" && sPassword == "")
        {
            bool bValidado = false;
            SUPER.BLL.FORANEO oFor = SUPER.BLL.FORANEO.GetByNif(null, sUsuario);
            if (oFor.t001_idficepi != -1)
            {
                bValidado = true;
            }

            sResul = "ComprobarUsuario@#@" + oFor.t001_idficepi + "@#@" + bValidado + "@#@" + sIntentos + "@#@" + oFor.t080_facep;
        }
        else if (ValidateUser(sUsuario, sPassword))
        {
            //miLog.put("Login1_Authenticate. Usuario validado");
            SUPER.BLL.FORANEO oFor = SUPER.BLL.FORANEO.GetByNif(null, sUsuario);
            if (oFor.t001_idficepi != -1)
            {
                //miLog.put("Usuario encontrado en FICEPI. oFor.t001_idficepi=" + oFor.t001_idficepi.ToString());
                if (!oFor.t314_accesohabilitado)
                {
                    //miLog.put("Usuario bloqueado");
                    //this.lblPregunta.Text = "Su usuario se encuentra bloqueado. Póngase en contacto con su responsable.";
                    //this.lblPregunta.Visible = true;
                    //FormsAuthentication.RedirectFromLoginPage(UserName, true);
                    //Response.Redirect("../Error.aspx?t=1", true);

                    string url = @"Error.aspx?t=1";
                    sResul = "Bloqueado@#@" + url;
                }
                //Fecha aceptación aviso legal
                else if (oFor.t080_facep == null || oFor.t080_pregunta=="" || oFor.t080_respuesta=="")
                {
                    int vez = 1;
                    Session["ua"] = oFor.t001_idficepi.ToString();
                    //string url = @"~/../Inicio/Pregunta/Default.aspx?f=" + oFor.t001_idficepi.ToString() + "&" + "VEZ=" + vez;
                    // Create a custom FormsAuthenticationTicket containing application specific data for the user.
                    string username = oFor.t001_idficepi.ToString();
                    bool isPersistent = false;
                    string userData = "ApplicationSpecific data for this user.";
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30),
                                                                                    isPersistent, userData, FormsAuthentication.FormsCookiePath);
                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    
                    string url = @"../Forastero/Pregunta/Default.aspx?f=" + oFor.t001_idficepi.ToString() + "&" + "VEZ=" + vez;
                    sResul = "PrimeraVez@#@" + url;
                }
                else
                {
                    bool bValidadoPass = true;
                    Session["ua"] = oFor.t001_idficepi.ToString();
                    //En explotacion
                    string url = @"/Capa_Presentacion/default.aspx";
                    //En desarrollo
                    //string url = @"../../default.aspx";
                    //string url = @"/FORASTERO/Capa_Presentacion/default.aspx";
                    if (SUPER.BLL.Log.logger.IsDebugEnabled) SUPER.BLL.Log.logger.Debug("Login.aspx.cs->Autenticacion. url=" + url);

                    // Create a custom FormsAuthenticationTicket containing application specific data for the user.
                    string username = oFor.t001_idficepi.ToString();
                    bool isPersistent = false;
                    string userData = "ApplicationSpecific data for this user.";
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30),
                                                                                    isPersistent, userData, FormsAuthentication.FormsCookiePath);
                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    //Response.Redirect("./Default.aspx");
                    
                    sResul = "IniciarSesion@#@" + url + "@#@" + bValidadoPass + "@#@+" + sIntentos;
                    //FormsAuthentication.RedirectFromLoginPage(username, true);
                }
            }
        }
        else { return "IniciarSesion@#@" + sIntentos; }

        return sResul;
    }

    private bool ValidateUser(string sUsuario, string sPassword)
    {
        bool bValidado = false;

        #region Comprobación de si es miembro
        //if (Membership.ValidateUser(sUsuario, sPassword))
        int idFicepi = SUPER.BLL.FORANEO.Validar(sUsuario, sPassword);
        if (idFicepi > -1)
            bValidado = true;
        //MembershipUser u = Membership.GetUser(sUsuario, true);
        //u.ChangePasswordQuestionAndAnswer(sPassword, sPregunta, sRespuesta);
        #endregion

        return bValidado;
    }

    private int GetIdFicepi(string sUsuario, string sPassword)
    {
        return SUPER.BLL.FORANEO.Validar(sUsuario, sPassword); ;
    }

    protected string btnRecordar(string username)
    {
        PonerMensaje("");
        //string sForm = @"~/../Inicio/RecuperarPassw/Default.aspx?u=" + username;
        string sForm = @"../Forastero/RecuperarPassw/Default.aspx?u=" + username;
        //Response.Redirect(sForm, true);       
        return "Recordar@#@" + sForm;
    }

    protected string cambiarPass(string username, string sPassword)
    {
        string respuesta = "";
        string sForm = @"../Forastero/CambioPassw/Default.aspx?u=" + username + "&p=" + Encriptar(sPassword);
        PonerMensaje("");
        //Response.Redirect(sForm, true);
        //string sForm = @"~/../Inicio/CambioPassw/Default.aspx?u=" + username;
        if (username != "")
        {
            string sUsuario = "FOR_" + username;
            SUPER.BLL.FORANEO oFor = SUPER.BLL.FORANEO.GetByNif(null, sUsuario);
            if (oFor.t001_idficepi != -1 && oFor.t314_accesohabilitado)
                //Response.Redirect(sForm, true);
                respuesta = sForm + "@#@OK";
            else
            {
                if (oFor.t001_idficepi != -1 && !oFor.t314_accesohabilitado)
                    //PonerMensaje("Su usuario se encuentra bloqueado. Póngase en contacto con su supervisor");
                    respuesta = "Tu usuario se encuentra bloqueado. Ponte en contacto con tu responsable.";
                else
                    //PonerMensaje("Usuario no válido");
                    respuesta = "Usuario no válido.";
            }
        }
        else
            //PonerMensaje("Debe indicar usuario");
            respuesta = "Debes indicar un usuario";


        return "cambiarPass@#@" + respuesta;
    }

    private string cambiarPregunta(string username, string password)
    {
        string sForm = "";
        PonerMensaje("");
        string respuesta = "";
        if (username != "" && password != "")
        {
            string sUsuario = "FOR_" + username;
            string sPassword = password.ToUpper();
            //int idFicepi = GetIdFicepi(sUsuario, sPassword);
            SUPER.BLL.FORANEO oFor = SUPER.BLL.FORANEO.GetByNif(null, sUsuario);
            if (oFor.t001_idficepi != -1 && oFor.t314_accesohabilitado && oFor.t080_passw == password)
            {
                //sForm = @"~/../Inicio/Pregunta/Default.aspx?f=" + oFor.t001_idficepi.ToString();
                sForm = @"../Forastero/Pregunta/Default.aspx?f=" + oFor.t001_idficepi.ToString();
                //Response.Redirect(sForm, true);
                respuesta = sForm + "@#@OK";
            }
            else
            {
                if (oFor.t001_idficepi != -1 && !oFor.t314_accesohabilitado)
                    respuesta = "Tu usuario se encuentra bloqueado. Ponte en contacto con tu responsable.";

                if (oFor.t080_passw != password)
                    respuesta = "Contraseña incorrecta";
                //else
                //    respuesta = "Usuario no válido";
            }
        }
        else
            respuesta = "Debes indicar usuario y contraseña.";

        return "cambiarPregunta@#@" + respuesta;
    }

    protected void PonerMensaje(string sMens)
    {
        this.divMsg.InnerText = sMens;
    }

    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    /// Encripta una cadena
    public static string Encriptar(string cadenaAencriptar)
    {
        string result = string.Empty;
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
        result = Convert.ToBase64String(encryted);
        return result;
    }

   
}
