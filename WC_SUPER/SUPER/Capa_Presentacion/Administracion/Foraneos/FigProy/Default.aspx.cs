using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
//using System.Web.UI.WebControls;
using EO.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaFigAsig, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                string strTabla0 = obtenerFigurasAsig();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strTablaFigAsig = aTabla0[1];
                else //Master.sErrores = aTabla0[1];
                    sErrores += Errores.mostrarError("Error al obtener las figuras");
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener figuras", ex);
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
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string obtenerFigurasAsig()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            string sFiguras = Session["FIGURASFORANEOS"].ToString();
            sb.Append("<TABLE id='tblDatos2' style='width: 350px;' class='texto MM' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width: 20px' /><col style='width: 315px;' /></colgroup>");
            sb.Append("<tbody>");

            if (sFiguras != "")
            {
                string[] aFig = Regex.Split(sFiguras, ",");
                for(int i=0; i<aFig.Length; i++)
                {
                    if (aFig[i] != "")
                    {
                        sb.Append("<tr id='" + aFig[i] + "' bd='' style='height:20px;' ");
                        sb.Append("onmousedown='DD(event)' onclick='mm(event)' >");
                        sb.Append("<td style='padding-left:1px'><img src='../../../../images/imgFN.gif'></td>");
                        switch (aFig[i])
                        {
                            case "D":
                                sb.Append("<td><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></td>");
                                sb.Append("<td>Delegado</td>");
                                break;
                            case "C":
                                sb.Append("<td><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></td>");
                                sb.Append("<td>Colaborador</td>");
                                break;
                            case "I":
                                sb.Append("<td><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></td>");
                                sb.Append("<td>Invitado</td>");
                                break;
                            case "J":
                                sb.Append("<td><img src='../../../../Images/imgJefeProyecto.gif' title='Jefe de proyecto económico' /></td>");
                                sb.Append("<td>Jefe</td>");
                                break;
                            case "M":
                                sb.Append("<td><img src='../../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></td>");
                                sb.Append("<td>RTPE</td>");
                                break;
                            case "B":
                                sb.Append("<td><img src='../../../../Images/imgBitacorico.gif' title='Bitacórico' /></td>");
                                sb.Append("<td>Bitacórico</td>");
                                break;
                            case "S":
                                sb.Append("<td><img src='../../../../Images/imgSecretaria.gif' title='Asistente' /></td>");
                                sb.Append("<td>Asistente</td>");
                                break;
                            case "W":
                                sb.Append("<td><img src='../../../../Images/imgRTPT.gif' title='Responsable técnico de proyecto técnico' /></td>");
                                sb.Append("<td>RTPT</td>");
                                break;
                        }
                        sb.Append("</tr>" + (char)10);
                    }
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras asignadas.", ex);
        }
    }
    private string Grabar(string strFiguras)
    {
        string sResul = "";
        try
        {
            SUPER.Capa_Negocio.PARAMETRIZACIONSUPER.UpdateFigProyForaneo(null, strFiguras);
            Session["FIGURASFORANEOS"] = strFiguras;
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de las figuras", ex);
        }
        return sResul;
    }
}
