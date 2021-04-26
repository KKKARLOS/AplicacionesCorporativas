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

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaUsuariosCATP;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "38";
            //Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Consulta de profesionales con contrato a tiempo parcial que exceden algún límite establecido";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
            //Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                /*
                string sMes = (DateTime.Now.Month.ToString().Length == 1) ? "0" : ""; 
                sMes += DateTime.Now.Month.ToString();
                string sFecha = DateTime.Now.Year.ToString() + sMes + "01";
                string strTabla0 = SUPER.Capa_Negocio.USUARIOSCATP.LimiteExcedido(sFecha);
                
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error")
                {
                    strTablaUsuariosCATP = aTabla0[1];
                    if (aTabla0[2] == "S") imgCaution.Style.Add("display", "block");
                    else imgCaution.Style.Add("display", "none");
                }
                else Master.sErrores = aTabla0[1];
                */
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los profesionales con contrato a tiempo parcial que hayan excedido algún límite establecido", ex);
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

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += SUPER.Capa_Negocio.USUARIOSCATP.LimiteExcedido(aArgs[1]);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al cargar los profesionales con contrato temporal para ese mes", ex);
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
