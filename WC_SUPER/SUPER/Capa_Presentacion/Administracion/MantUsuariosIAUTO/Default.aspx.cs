using System;
using System.Data;
//using System.Data.SqlClient;
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
    public string strTablaUsuariosIAUTO;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {            
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de profesionales con imputaciones automáticas";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

            try
            {
                //string strTabla0 = SUPER.Capa_Negocio.USUARIOSIAUTO.obtenerProfesionales();
                
                //string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                //if (aTabla0[0] != "Error") strTablaUsuariosIAUTO = aTabla0[1];
                //else Master.sErrores = aTabla0[1];
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los profesionales con imputaciones automáticas", ex);
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
                case ("grabar"):
                    sResultado += SUPER.Capa_Negocio.USUARIOSIAUTO.Grabar(aArgs[1]);
                    break;
                case ("getTarea"):
                    sResultado += ObtenerDatosTarea(aArgs[1], aArgs[2]);
                    break;
                case ("getProfesionales"):
                    sResultado += SUPER.Capa_Negocio.USUARIOSIAUTO.obtenerProfesionales();
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los profesionales con imputaciones automáticas", ex);
                    break;
                case ("getProfesionales"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los profesionales con imputaciones automáticas", ex);
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
    private string ObtenerDatosTarea(string sIdTarea, string sIDUsuario)
    {
        StringBuilder sb = new StringBuilder();
        try
        {

            TAREAPSP o = TAREAPSP.ObtenerDatosRecurso(null, int.Parse(sIdTarea), int.Parse(sIDUsuario));

            sb.Append(o.t324_idmodofact.ToString() + "@#@"); //2
            sb.Append(o.t324_denominacion.ToString() + "@#@"); //3
            sb.Append((o.dPrimerConsumo.HasValue) ? ((DateTime)o.dPrimerConsumo).ToShortDateString() + "@#@" : "@#@"); //4
            sb.Append((o.dUltimoConsumo.HasValue) ? ((DateTime)o.dUltimoConsumo).ToShortDateString() + "@#@" : "@#@"); //5
            sb.Append(o.nConsumidoHoras.ToString("N") + "@#@"); //6
            sb.Append(o.nConsumidoJornadas.ToString("N") + "@#@"); //7
            sb.Append(o.nPendienteEstimado.ToString("N") + "@#@"); //8
            sb.Append((o.nAvanceTeorico > -1) ? o.nAvanceTeorico.ToString("N") + "@#@" : "@#@"); //9
            sb.Append((o.t336_etp > 0) ? o.t336_etp.ToString("N") + "@#@" : "@#@"); //10
            sb.Append((o.t336_ffp.HasValue) ? ((DateTime)o.t336_ffp).ToShortDateString() + "@#@" : "@#@"); //11
            sb.Append(Utilidades.escape(o.t336_indicaciones.ToString()) + "@#@"); //12
            sb.Append(Utilidades.escape(o.t332_mensaje.ToString()) + "@#@"); //13
            sb.Append((o.t336_ete > 0) ? o.t336_ete.ToString("N") + "@#@" : "@#@"); //14
            sb.Append((o.t336_ffe.HasValue) ? ((DateTime)o.t336_ffe).ToShortDateString() + "@#@" : "@#@"); //15
            sb.Append(Utilidades.escape(o.t336_comentario.ToString()) + "@#@"); //16
            sb.Append((o.nCompletado == 1) ? "1@#@" : "0@#@"); //17
            sb.Append(o.num_proyecto.ToString("#,###") + " - " + o.t305_seudonimo + "@#@"); //18
            sb.Append(o.t331_despt + "@#@"); //19
            sb.Append(o.t334_desfase + "@#@"); //20
            sb.Append(o.t335_desactividad + "@#@"); //21


            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("No se han obtenido los datos de la tarea:", ex);
        }
    }
}
