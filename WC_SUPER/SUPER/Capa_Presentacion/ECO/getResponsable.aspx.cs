using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class getResponsable : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", tiporesp = "proyecto", tiporeplica = "", sPrefijoCRP = "", sResp = "Responsable", sNoResp = "No responsable", sTTipResp = "", sTTipNoResp = "", idNodo = "", sNodo = "";
    public string strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            if (Request.QueryString["tiporesp"] != null)
                tiporesp = Request.QueryString["tiporesp"].ToString();
            if (Request.QueryString["idNodo"] != null)
            {
                idNodo = Request.QueryString["idNodo"].ToString();
                sNodo = Request.QueryString["sNodo"].ToString();
            }

            switch (tiporesp)
            {
                case "proyecto": this.Title = "Selección de responsable de proyecto"; break;
                case "contrato": this.Title = "Selección de responsable de contrato"; break;
                case "crp":
                    this.Title = "Selección de responsable de proyecto";
                    sResp = "CRP";
                    sNoResp = "No CRP";
                    sTTipResp = "Candidato a responsable de proyecto";
                    sTTipNoResp = "No es candidato a responsable de proyecto";
                    sPrefijoCRP = "CRP";
                    if (!SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        chkBajas.Disabled = true;
                    if (Request.QueryString["tiporeplica"] != null)
                        tiporeplica = Request.QueryString["tiporeplica"].ToString();
                    if (tiporeplica == "P"){
                    txtApellido1.Enabled = false;
                    txtApellido2.Enabled = false;
                    txtNombre.Enabled = false;
                    }
                    break;
            }

            if (tiporesp == "crp" && idNodo != "")
            {
                string strTabla = getResponsables("", "", "", "0", tiporesp, idNodo);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else sErrores += Errores.mostrarError(aTabla[1]);
            }
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

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("responsables"):
                sResultado += getResponsables(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], "");
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

    private string getResponsables(string sAp1, string sAp2, string sNombre, string sMostrarBajas, string sTipoResp, string idNodo)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;

        sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 500px;'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:480px;' /></colgroup>");
        sb.Append("<tbody>");
        try
        {
            switch (sTipoResp){
                case "proyecto": dr = USUARIO.ObtenerProfesionalesResponsablesProyecto(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), (sMostrarBajas == "1")? true:false); break;
                case "contrato": dr = USUARIO.ObtenerProfesionalesResponsablesContrato(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), (sMostrarBajas == "1") ? true : false); break;
                case "crp": dr = USUARIO.ObtenerProfesionalesCRP(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), (sMostrarBajas == "1") ? true : false, (idNodo == "") ? null : ((int?)int.Parse(idNodo))); break;
            }

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["idusuario"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' ");

                if ((int)dr["es_responsable"] == 0)
                {
                    if (sTipoResp == "crp")
                    {
                        //sb.Append(" class='MA' ondblclick=\"aceptarClick(this.rowIndex)\"><td><img src='../../images/imgResponsable" + sPrefijoCRP + ".gif' style='filter:progid:DXImageTransform.Microsoft.Alpha(opacity=30)' width='16px' height='16px' /></td>");
                        sb.Append(" respon='0' class='MA' style='height:20px;cursor:url(../../images/imgManoAzul2.cur),pointer;' ondblclick=\"aceptarClick(this.rowIndex)\">");
                        sb.Append("<td><img src='../../images/imgResponsable" + sPrefijoCRP + ".gif' width='16px' height='16px' /></td>");
                        //sb.Append("<td style='noWrap:true;' ondblclick=\"aceptarClick(this.parentNode.rowIndex)\" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                        sb.Append("<td style='noWrap:true; padding-left:3px;' ondblclick=\"aceptarClick(this.parentNode.rowIndex)\" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                    }
                    else
                    {
                        //sb.Append("><td><img src='../../images/imgResponsable" + sPrefijoCRP + ".gif' style='filter:progid:DXImageTransform.Microsoft.Alpha(opacity=30)' width='16px' height='16px' /></td>");
                        sb.Append(" respon='0'  style='height:20px;'>");
                        sb.Append("<td><img src='../../images/imgResponsable" + sPrefijoCRP + ".gif' width='16px' height='16px' /></td>");
                        //sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                        sb.Append("<td style='noWrap:true; padding-left:3px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                    }
                }
                else
                {
                    sb.Append(" respon='1' class='MA'  style='height:20px;cursor:url(../../images/imgManoAzul2.cur),pointer;' ondblclick=\"aceptarClick(this.rowIndex)\">");
                    sb.Append("<td><img src='../../images/imgResponsable" + sPrefijoCRP + ".gif' width='16px' height='16px' /></td>");
                    //sb.Append("<td style='noWrap:true;' ondblclick=\"aceptarClick(this.parentNode.rowIndex)\" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                    sb.Append("<td style='noWrap:true; padding-left:3px;' ondblclick=\"aceptarClick(this.parentNode.rowIndex)\" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                }

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los responsables.", ex);
        }
        return sResul;
    }
}
