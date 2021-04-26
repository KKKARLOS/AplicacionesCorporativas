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
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;
using GEMO.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHtml;
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.sbotonesOpcionOn = "4";
        Master.sbotonesOpcionOff = "4";
        Master.TituloPagina = "Mantenimiento de la tarifa de datos";
        Master.bFuncionesLocales = true;

        try
        {
            if (!Page.IsCallback)
            {
                strTablaHtml = GEMO.BLL.TARIFADATOS.Catalogo();
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }

        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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

        try
        {
            switch (aArgs[0])
            {
                case "getDatos":
                    sResultado += "OK@#@" + GEMO.BLL.TARIFADATOS.Catalogo();
                    break;
                case ("grabar"):
                    string sInsert = GEMO.BLL.TARIFADATOS.Grabar(aArgs[1]);
                    sResultado += "OK@#@" + sInsert;
                    break;		
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("getDatos"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener las tarifas de datos", ex);
                    break;
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los medios", ex);
                    break;		
            }
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;

    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
}