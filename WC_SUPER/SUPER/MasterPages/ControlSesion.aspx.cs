using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;

public partial class MasterPages_ControlSesion : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //1? Se indican (por este orden) la funci?n a la que se va a devolver el resultado
        //   y la funci?n que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2? Se "registra" la funci?n que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1? Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@";

        //2? Aqu? realizar?amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("setDiamante"):
                sResultado += setDiamante(aArgs[1]);
                break;
            case ("SalirSession"):
                sResultado += SalirSession();
                break;
                
        }

        //3? Damos contenido a la variable que se env?a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env?a el resultado al cliente.
        return _callbackResultado;
    }
    protected string setDiamante(string sDiamante)
    {
        try
        {
            USUARIO.UpdateDiamante((int)Session["NUM_EMPLEADO_ENTRADA"], (sDiamante == "1") ? true : false);
            Session["DIAMANTE"] = (sDiamante == "1") ? true : false;
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("No se han grabado los datos:", ex);
        }
    }
    protected string SalirSession()
    {
        try
        {
            //Utilidades.DeleteUsuario(Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\")[Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\").GetLength(0) - 1]);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al ir a abandonar la sesi?n C#.", ex);
        }
    }

}
