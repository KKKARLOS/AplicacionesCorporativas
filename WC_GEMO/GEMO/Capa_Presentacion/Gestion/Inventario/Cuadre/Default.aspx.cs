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
using Microsoft.JScript;
using System.Text;
using GEMO.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtmlCaso1, strTablaHtmlCaso2, strTablaHtmlCaso3, strTablaHtmlCaso4;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 0;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Cuadre de inventario";
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            strTablaHtmlCaso1 = GEMO.BLL.LINEA.FacturadasNoInventariadas();
            strTablaHtmlCaso2 = GEMO.BLL.LINEA.ActivasSinFactura();
            strTablaHtmlCaso3 = GEMO.BLL.LINEA.InactivasYBloqueadasConFactura();
            strTablaHtmlCaso4 = GEMO.BLL.LINEA.DescuadresVarios(0,1);


            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
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
                case "FacturadasNoInventariadas":
                    GEMO.BLL.LINEA.ActualizarFacturadasNoInventariadas(aArgs[1]);
                    sResultado += "OK@#@";
                    break;
                case "ActivasSinFactura":
                    GEMO.BLL.LINEA.ActualizarActivasSinFactura(aArgs[1]);
                    sResultado += "OK@#@";
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("FacturadasNoInventariadas"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al registrar las lineas facturadas no inventariadas", ex);
                    break;
                case ("ActivasSinFactura"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al actualizar las líneas activas sin factura", ex);
                    break;

                //case ("generarExcel"):
                //    sResultado += "Error@#@" + Errores.mostrarError("Error al generar el fichero excel", ex);
                //    break;
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
