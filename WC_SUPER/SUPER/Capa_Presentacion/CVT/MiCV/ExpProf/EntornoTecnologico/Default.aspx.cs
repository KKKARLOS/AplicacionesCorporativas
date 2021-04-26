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

using System.Text;
using SUPER.Capa_Negocio;
using SUPER.BLL;
using System.Text.RegularExpressions;
//using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    public string sErrores = "", strHTML = "";
    protected string strInicial;
    private string _callbackResultado = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            strHTML = SUPER.BLL.EntornoTecno.Catalogo2();
            strInicial = "";
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        

        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("grabar"):
                    string strDatos="";
                    string[] aEntornos = Regex.Split(aArgs[1], "##@@");
                    foreach (string sEntorno in aEntornos)
                    {
                        if (sEntorno!="")
                            strDatos += "I@dato@null@dato@" + sEntorno + "@dato@false@entorno@";
                    }
                    sResultado += EntornoTecno.Grabar(strDatos);
                    break;

            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):

                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "ErrorControlado@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los entornos tecnologicos", ex);
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
