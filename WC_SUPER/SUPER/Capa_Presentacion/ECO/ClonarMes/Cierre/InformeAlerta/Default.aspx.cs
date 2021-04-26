using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;

public partial class Capa_Presentacion_ECO_ValorGanado_CreacionLB_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sMoneda = "", strHTMLTabla = "";
    public int nIdSegMes;
    public byte nAlerta;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                if (Request.QueryString["nM"] != null)
                    nIdSegMes = int.Parse(Utilidades.decodpar(Request.QueryString["nM"].ToString()));
                if (Request.QueryString["nA"] != null)
                    nAlerta = byte.Parse(Utilidades.decodpar(Request.QueryString["nA"].ToString()));
                if (Request.QueryString["sM"] != null)
                    sMoneda = Utilidades.decodpar(Request.QueryString["sM"].ToString());

                string[] aDatos = Regex.Split(SEGMESPROYECTOSUBNODO.ObtenerInformeDeAlerta(nIdSegMes, sMoneda, nAlerta), "@#@");
                if (aDatos[0] == "OK")
                {
                    strHTMLTabla = aDatos[1];
                    cldResultadoM1.InnerText = aDatos[2];
                    cldResultadoM2.InnerText = aDatos[3];
                    cldAcumuladoM1.InnerText = aDatos[4];
                    cldAcumuladoM2.InnerText = aDatos[5];
                    txtMensaje.Text = aDatos[6];
                }
                else
                {
                    sErrores = Errores.mostrarError(aDatos[1]);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("getAlertas"):
                sResultado += SEGMESPROYECTOSUBNODO.ObtenerDialogosDeAlertas(aArgs[1], true);
                break;
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
