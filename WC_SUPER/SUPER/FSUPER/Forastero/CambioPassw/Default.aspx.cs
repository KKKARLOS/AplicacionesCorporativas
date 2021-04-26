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
    public string sErrores = "", sLectura = "false", strTablaHTMLIntegrantes, sNodo = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //this.txtUser.Text = Utilidades.decodpar(Request.QueryString["u"].ToString());//usuario
            if (Request.QueryString["u"] != null)
            {
                this.txtUser.Text = Request.QueryString["u"].ToString();//usuario
            }
            if (Request.QueryString["p"] != null)
            {
                //Al tener formato password me obliga a cargarlo desde javascript
                //this.txtPasswAnt.Text = DesEncriptar(Request.QueryString["p"].ToString());//password
                this.hdnPassw.Value = DesEncriptar(Request.QueryString["p"].ToString());//password
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
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
    protected string Grabar(string sNif, string sPasswAnt, string sPassNew)
    {
        string sResul = "OK@#@";
        try
        {
            //byte iApp = byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"].ToString());
            string sRes = ComprobarUsuario(sNif, sPasswAnt);
            string[] aDatos = Regex.Split(sRes, "@#@");
            //1º compruebo que el usuario existe
            if (aDatos[0] == "OK")
            {
                ActualizarPassword(int.Parse(aDatos[1]), sPassNew);
            }
            else
                sResul = "Error@#@Usuario no autorizado\n\nNo ha sido posible grabar la nueva contraseña";
        }
        catch (Exception e)
        {
            sResul = "Error@#@" + e.Message;
        }
        return sResul;
    }

    protected string ComprobarUsuario(string sNif, string sPasswAnt)
    {
        string sResul = "OK@#@";
        string sUsuario = "FOR_" + sNif;
        try
        {
            byte iApp = byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"].ToString());
            string sIdFicepi = ValidarUsuario(sUsuario, sPasswAnt);

            //1º compruebo que el usuario con ese NIF existe
            if (sIdFicepi != "")
            {
                sResul = "OK@#@" + sIdFicepi;
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
    protected string ValidarUsuario(string sNif, string sPasswAnt)
    {
        string sRes = "";
        SqlDataReader dr = GetUsuario(sNif, sPasswAnt);
        if (dr.Read())
        {
            sRes = dr["t001_idficepi"].ToString();
        }
        dr.Close();
        dr.Dispose();

        return sRes;
    }
    protected SqlDataReader GetUsuario(string t001_cip, string t080_passw)
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

        cmd = new SqlCommand("SUP_FORANEO_VALIDAR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@t001_cip", SqlDbType.Text, 15);
        cmd.Parameters[0].Value = t001_cip;
        cmd.Parameters.Add("@t080_passw", SqlDbType.Text, 50);
        cmd.Parameters[1].Value = Encriptar(t080_passw.ToUpper());

        return cmd.ExecuteReader();
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

    protected void ActualizarPassword(int t001_idficepi, string t080_passw)
    {
        SqlConnection con = null;
        SqlCommand cmd = null;

        string ConnectionString = obtenerCadenaConexion();
        con = new SqlConnection(ConnectionString);
        con.Open();

        cmd = new SqlCommand("SUP_FICEPIFORANEO_UPD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@t001_idficepi", SqlDbType.Int, 4);
        cmd.Parameters[0].Value = t001_idficepi;
        cmd.Parameters.Add("@t001_idficepi_promotor", SqlDbType.Int, 4);
        cmd.Parameters[1].Value = null;
        cmd.Parameters.Add("@t080_facep", SqlDbType.SmallDateTime, 10);
        cmd.Parameters[2].Value = null;
        cmd.Parameters.Add("@t080_fultacc", SqlDbType.SmallDateTime, 10);
        cmd.Parameters[3].Value = null;
        cmd.Parameters.Add("@t080_passw", SqlDbType.Text, 50);
        cmd.Parameters[4].Value = Encriptar(t080_passw.ToUpper());
        cmd.Parameters.Add("@t080_pregunta", SqlDbType.Text, 100);
        cmd.Parameters[5].Value = "";
        cmd.Parameters.Add("@t080_respuesta", SqlDbType.Text, 50);
        cmd.Parameters[6].Value = "";

        cmd.ExecuteReader();

    }
    /// Encripta una cadena
    public static string Encriptar(string cadenaAencriptar)
    {
        string result = string.Empty;
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
        result = Convert.ToBase64String(encryted);
        return result;
    }
    /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
    public static string DesEncriptar(string cadenaAdesencriptar)
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }


}
