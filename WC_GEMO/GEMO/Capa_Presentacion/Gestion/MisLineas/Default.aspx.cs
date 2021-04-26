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
    public string strTablaHtml;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.nBotonera = 0;
        Master.TituloPagina = "Mis l�neas";
        Master.bFuncionesLocales = true;

        try
        {
            if (!Page.IsCallback)
            {
                string strTabla0 = GEMO.BLL.LINEA.Usuario("X,A,Y,B","1","1");
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                strTablaHtml = aTabla0[0];
                lblNumLineas.Text = int.Parse(aTabla0[1].ToString()).ToString("###,###,###");

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
            Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
                    sResultado += "OK@#@" + GEMO.BLL.LINEA.Usuario(Utilidades.unescape(aArgs[1]), aArgs[2], aArgs[3]);
                    break;
                //case ("generarExcel"):
                //    sResultado += "OK@#@" + GEMO.BLL.LINEA.generarExcel();
                //    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener mis l�neas", ex);
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
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
}