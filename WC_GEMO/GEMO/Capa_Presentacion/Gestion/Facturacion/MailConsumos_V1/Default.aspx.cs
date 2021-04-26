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
using GEMO.BLL;
using Microsoft.JScript;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    public string sErrores= "";
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                //cboMes.Value = DateTime.Now.Month.ToString();
                //txtAnno.Text = DateTime.Now.Year.ToString();
                cboFechaFra.DataValueField = "ID";
                cboFechaFra.DataTextField = "DENOMINACION";
                cboFechaFra.DataSource = GEMO.DAL.FACTURACION.Fechas();
                cboFechaFra.DataBind();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
                case ("mail"):
                    sResultado += "OK@#@" + ComunicacionesMail(DateTime.Parse(Utilidades.unescape(aArgs[1]).ToString()));
                    break;//
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("mail"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al enviar los mails", ex);
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
    private string ComunicacionesMail(DateTime dFecha)
    {
        try
        {
            sErrores = GEMO.BLL.FACTURACION.ComunicacionesMail(dFecha);
            if (sErrores == "") Actualizar_T714_CRONOFICHFRA(dFecha);
        }
        catch (Exception x)
        {
            sErrores = "Error producido al intentar actualizar las tablas en base de datos. " + x.Message;
        }
        return sErrores;
    }
    private string Actualizar_T714_CRONOFICHFRA(DateTime dFecha)
    {
        try
        {
            GEMO.BLL.FACTURACION.Mail(dFecha);
            return sErrores;
        }
        catch (Exception x)
        {
            sErrores = "Error producido al intentar actualizar la tabla T714_CRONOFICHFRA (. " + x.Message;
            return sErrores;
        }
    }
}
