using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;
//using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sLectura = "false";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //this.txtNif.Text = Utilidades.decodpar(Request.QueryString["u"].ToString());//usuario
            if (Request.QueryString["u"] != null)
            {
                this.txtUser.Text = Request.QueryString["u"].ToString();//usuario
                if (this.txtUser.Text != "")
                {
                    string sRes = ComprobarUsuario(this.txtUser.Text);
                    string[] aDatos = Regex.Split(sRes, "@#@");
                    if (aDatos[0] == "OK")
                    {
                        this.txtPregunta.InnerText = aDatos[3];
                        this.hdnRespuesta.Value = aDatos[4];
                        this.lblPassw.InnerText = aDatos[5];
                    }
                    else//El NIF no existe como foráneo
                    {
                        this.txtUser.Text = "";
                        this.lblError.Text = aDatos[1];
                        this.btnGrabar.Visible = false;
                    }
                }
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
            case ("getUser"):
                sResultado += ComprobarUsuario(aArgs[1]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    protected string ComprobarUsuario(string sNif)
    {
        string sResul = "OK@#@";
        string sUsuario = "FOR_" + sNif;
        try
        {
            //byte iApp = byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"].ToString());
            //byte iApp = byte.Parse("14");
            string sDatosUser = ValidarUsuario(sUsuario);

            //1º compruebo que el usuario con ese NIF existe
            if (sDatosUser != "")
            {
                string[] aUser = Regex.Split(sDatosUser, "@#@");
                int idFicepi= int.Parse(aUser[0]);
                //Miro si tiene pregunta definida
                if (aUser[1] != "" && aUser[2] != "")
                    sResul = "OK@#@PREGUNTAR@#@" + aUser[0] + "@#@" + aUser[1] + "@#@" + aUser[2].ToUpper() + "@#@" + aUser[3];
                else
                    sResul = "Error@#@Usuario no autorizado\n\nNo dispone de pregunta recordatorio registrada.";
            }
            else
                sResul = "Error@#@Usuario no autorizado\n\nNo ha sido posible recuperar la contraseña.";
        }
        catch (Exception e)
        {
            sResul = "Error@#@" + e.Message;
        }
        return sResul;
    }
    //protected void EncolarCorreo(string sNif, string sEmail, string sPassw)
    //{
    //    ArrayList aListCorreo = null;
    //    string sAsunto = "IBERMATICA.SUPER - Comunicación de contraseña";
    //    string sTexto = "<br />SUPER le informa que su contraseña vigente es :" + sPassw;

    //    string[] aMail = { sAsunto, sTexto, sEmail };
    //    aListCorreo.Add(aMail);

    //    //SUPER.Capa_Negocio.Correo.EnviarCorreos(aListCorreo);
    //}
    protected string ValidarUsuario(string sNif)
    {
        string sRes = "";
        SqlDataReader dr = GetByNif(sNif);
        if (dr.Read())
        {
            //if (dr["t001_email"].ToString().ToUpper() == sMail.ToUpper())
                //idFicepi = int.Parse(dr["t001_idficepi"].ToString());
            sRes = dr["t001_idficepi"].ToString() + "@#@" + 
                   DesEncriptar(dr["t080_pregunta"].ToString()) + "@#@" +
                   DesEncriptar(dr["t080_respuesta"].ToString()) + "@#@" +
                   DesEncriptar(dr["t080_passw"].ToString());
        }
        dr.Close();
        dr.Dispose();

        return sRes;
    }
    protected string obtenerCadenaConexion()
    {
        string sConn = "";
        if (HttpContext.Current.Cache.Get("CadenaConexion") == null)
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
            else
                sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();

            HttpContext.Current.Cache.Insert("CadenaConexion", sConn, null, DateTime.Now.AddHours(24), TimeSpan.Zero);
        }
        else
        {
            sConn = (string)HttpContext.Current.Cache.Get("CadenaConexion");
        }

        return sConn;
    }

    protected SqlDataReader GetByNif(string t001_cip)
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        //SqlDataReader dr = null;
        //SqlParameter[] aParam = new SqlParameter[1];
        //aParam[0] = new SqlParameter("@t001_cip", SqlDbType.Text, 15);
        //aParam[0].Value = t001_cip;

        string ConnectionString = obtenerCadenaConexion();
        con = new SqlConnection(ConnectionString);
        con.Open();

        cmd = new SqlCommand("SUP_FORANEO_NIF_S", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@t001_cip", SqlDbType.Text, 15);
        cmd.Parameters[0].Value = t001_cip;

        //return SqlHelper.ExecuteSqlDataReader("SUP_FORANEO_NIF_S", aParam);
        return cmd.ExecuteReader();
    }
    //protected bool ValidarPregunta(int t001_idficepi)
    //{
    //    bool bRes = false;
    //    SqlDataReader dr = GetPregunta(t001_idficepi);
    //    if (dr.Read())
    //    {
    //        if (dr["t080_pregunta"].ToString() != "" && dr["t080_respuesta"].ToString() != "")
    //            bRes = true;
    //    }
    //    dr.Close();
    //    dr.Dispose();

    //    return bRes;
    //}
    //protected SqlDataReader GetPregunta(int t001_idficepi)
    //{
    //    SqlConnection con = null;
    //    SqlCommand cmd = null;
    //    string ConnectionString = obtenerCadenaConexion();
    //    con = new SqlConnection(ConnectionString);
    //    con.Open();

    //    cmd = new SqlCommand("SUP_FORANEO_PREGUNTA_S", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@t001_idficepi", SqlDbType.Int, 4);
    //    cmd.Parameters[0].Value = t001_idficepi;

    //    return cmd.ExecuteReader();
    //}

    /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
    protected string DesEncriptar(string cadenaAdesencriptar)
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }

}
