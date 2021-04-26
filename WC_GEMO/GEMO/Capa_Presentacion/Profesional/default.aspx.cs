using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;
using GEMO.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
        //   y la funci�n que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2� Se "registra" la funci�n que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@";

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        try
        {
            switch (aArgs[0])
            {
                case ("profesionales"):

                    sResultado += "OK@#@" + GEMO.BLL.PROFESIONALES.obtenerProfesionales2(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), int.Parse(Utilidades.unescape(aArgs[4])));
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("profesionales"):

                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los profesionales. ", ex);
                    break;
            }
        }


        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }

    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
}
