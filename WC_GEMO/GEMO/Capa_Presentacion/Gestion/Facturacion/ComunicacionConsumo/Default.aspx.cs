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
using GEMO.BLL;
using Microsoft.JScript;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //int a = 0;
            //int b = 1;
            //int c = b / a;
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
                //   y la funci�n que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2� Se "registra" la funci�n que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Error al mostrar la previsi�n metereol�gica.") Master.sErrores = Errores.mostrarError("Error al cargar los datos: ", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@";
        switch (aArgs[0])
        {
            case ("eliminar"):
                break;
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
