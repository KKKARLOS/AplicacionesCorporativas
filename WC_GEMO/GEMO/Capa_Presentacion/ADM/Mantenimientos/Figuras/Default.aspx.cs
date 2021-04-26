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

using System.Text.RegularExpressions;
using EO.Web;
using GEMO.BLL;
using System.Text;
//Para usar el unescape
using Microsoft.JScript;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial = "";
    public string strTablaHTMLIntegrantes;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Figuras a nivel de aplicación";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Capa_Presentacion/ADM/Mantenimientos/Figuras/Functions/ddfiguras.js");
            Master.FicherosCSS.Add("App_Themes/Corporativo/ddfiguras.css");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    string strTabla0 = GEMO.BLL.FIGURAS_PROFESI.ObtenerIntegrantes();
                    string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                    strTablaHTMLIntegrantes = aTabla0[0];
                    this.hdnAux.Value = aTabla0[1];
                    txtApellido1.Focus();
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
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@";
        try
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "OK@#@" + GEMO.BLL.PROFESIONALES.obtenerProfesionales(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]),0);
                    break;
                case ("grabar"):
                    GEMO.BLL.FIGURAS_PROFESI.Grabar(aArgs[1]);
                    sResultado += "OK@#@"; 
                    break;					
                case ("getFiguras"):
                    sResultado += "OK@#@" + GEMO.BLL.FIGURAS_PROFESI.ObtenerIntegrantes();
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los profesionales", ex);
                    break;
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar las figuras", ex);
                    break;
                case ("getFiguras"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los profesionales con sus figuras", ex);
                    break;					
            }
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }  
}
