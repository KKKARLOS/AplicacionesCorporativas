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
using EO.Web;
using GASVI.BLL;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.JScript;

public partial class Administradores : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sMensajeMMOFF = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    protected bool bMostrarMMOFF = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 0;
            Master.TituloPagina = "Detalle de comunicados";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                Session["GVT_HAYAVISOS"] = "0";
                //Mostrar avisos únicamente al usuario real, no los de reconexión.
                if ((int)Session["GVT_IDFICEPI"] == (int)Session["GVT_IDFICEPI_ENTRADA"])
                {
                    //if (TEXTOAVISOSGASVI.VerSiHay()) Session["GVT_HAYAVISOS"] = "1";
                    if (FICEPIAVISOS.VerSiHay((int)Session["GVT_IDFICEPI_ENTRADA"])) Session["GVT_HAYAVISOS"] = "1";
                }

                if (Session["GVT_HAYAVISOS"].ToString() == "1")
                {
                    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/AvisosNav.ascx"));
                    Session["GVT_HAYAVISOS"] = "0";
                }
                else
                {   //Si no hay avisos de Admon y hemos accedido desde menú para ver los que hay
                    sMensajeMMOFF = "No existen comunicados de Administración para Ud.";
                    bMostrarMMOFF = true;
                }

                if (bMostrarMMOFF)
                    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/mensajeOff.ascx"));
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al crear la página", ex);
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

        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case "eliminar":
                    //TEXTOAVISOSGASVI.EliminarAviso(aArgs[1]);
                    FICEPIAVISOS.EliminarAviso(aArgs[1], int.Parse(Session["GVT_IDFICEPI_ENTRADA"].ToString()));
                    sResultado += "OK@#@";
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("eliminar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al eliminar los avisos", ex);
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
