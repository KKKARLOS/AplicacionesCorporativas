using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
using SUPER.BLL;

public partial class Capa_Presentacion_CVT_Mantenimientos_Idiomas_Default : System.Web.UI.Page,ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.nBotonera = 9;
        Master.bFuncionesLocales = true;
        Master.TituloPagina = "Mantenimiento de Idiomas";
        Master.Modulo = "CVT";
        if (!IsCallback) 
        {
            try
            {
                this.strTablaHTML = Idioma.Catalogo();
            }
            catch (Exception ex)
            {                
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
        }

        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";

        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "OK@#@" + Idioma.Grabar(aArgs[1]);
                    break;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.IndexOf("ErrorControlado") > -1)
            {
                sResultado += "Error@#@" + ex.Message;
            }
            else
                sResultado += "Error@#@" + Errores.mostrarError("Error al guardar", ex);
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


