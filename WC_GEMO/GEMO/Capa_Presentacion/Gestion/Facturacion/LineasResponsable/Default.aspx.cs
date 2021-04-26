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
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "", sErrores="";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                //Master.nBotonera = 43;
                //string strTabla0 = GEMO.BLL.LINEA.Responsables();
                //string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                //strTablaHTML = aTabla0[0];
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar los datos", ex);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@";
        try
        {
            switch (aArgs[0])
            {
                case ("lineas"):
                    sResultado += "OK@#@" + GEMO.BLL.LINEA.ResponsablesLineasFactura(int.Parse(aArgs[1]));
                    break;
                case ("buscar"):
                    sResultado += "OK@#@" + GEMO.BLL.LINEA.Responsables(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), (aArgs[4] == "1") ? true : false);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("lineas"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener las líneas", ex);
                    break;
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los responsables", ex);
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
