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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";
            Master.TituloPagina = "Catálogo de Cuentas";
            Master.bFuncionesLocales = true;

            try
            {
                string sTabla = SUPER.Capa_Negocio.CUENTASCUR.obtenerCuentas("");
                string[] aTabla = Regex.Split(sTabla, "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...


        try
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += SUPER.Capa_Negocio.CUENTASCUR.Grabar(aArgs[1]);
                    break;
                case ("getDatos"):
                    sResultado += SUPER.Capa_Negocio.CUENTASCUR.obtenerCuentas(Utilidades.unescape(aArgs[1]));
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar las cuentas", ex);
                    break;
                case ("getDatos"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener las cuentas", ex);
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
