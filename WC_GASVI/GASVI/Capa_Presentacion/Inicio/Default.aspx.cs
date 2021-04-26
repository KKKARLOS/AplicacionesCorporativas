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
using GASVI.BLL;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    protected bool bMostrarMMOFF = false;
    public bool bNotasPendientes = false;
    public bool bBono = false;
    public bool bPago = false;
    private string _callbackResultado = null;
    public string strTablaHTML = "", sMensajeMMOFF = "";
    public int nNotasPendientes = 0;
    public int nNotasVisadas = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sPaso = "1";
        if (!Page.IsCallback && Session["GVT_IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        sPaso = "2";
        try
        {
            if (!Page.IsCallback)
            {
                Master.FuncionesJavaScript.Add("Javascript/jquery.min.js");
                Master.FuncionesJavaScript.Add("Javascript/imgbubbles.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FicherosCSS.Add("Capa_Presentacion/Inicio/css/Inicio.css");
                Master.bFuncionesLocales = true;
                sPaso = "3";
                nNotasPendientes = Profesional.nNotasPendientes((int)Session["GVT_IDFICEPI"]);
                sPaso = "4";
                nNotasVisadas = Profesional.nNotasVisadas((int)Session["GVT_IDFICEPI"]);
                sPaso = "5";
                strTablaHTML = CABECERAGV.ObtenerNotasAbiertasYRecientes((int)Session["GVT_IDFICEPI"]);
                sPaso = "6";
                bBono = Profesional.bPermiteBono((int)Session["GVT_IDFICEPI"]);
                sPaso = "7";
                bPago = Profesional.bPermitePago((int)Session["GVT_IDFICEPI"]);
                sPaso = "8";

                if (Session["GVT_HAYAVISOS"].ToString() == "1")
                {
                    sPaso = "9";
                    this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/Avisos.ascx"));
                    Session["GVT_HAYAVISOS"] = "0";
                }
                sPaso = "10";
                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                sPaso = "11";
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar la página.(sPaso=" + sPaso + ")", ex);
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
