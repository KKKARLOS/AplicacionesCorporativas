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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;



public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos'></table>", sHayPreferencia = "false";
    public string sErrores = "", sModulo = "", sMostrarBitacoricos = "0", sNoVerPIG = "0", sNodoFijo = "0";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public short nPantallaPreferencia = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
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

                lblNodo2.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

                int? nPE = null;
                if (Request.QueryString["nPE"] != null)
                    nPE = (int?)int.Parse(Request.QueryString["nPE"].ToString());
                string[] aTabla = Regex.Split(ObtenerProyectos(nPE), "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                else sErrores = aTabla[1];
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
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                //sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19]);
                sResultado += ObtenerProyectos(null);
                break;
            case ("setPSN"):
                Session["ID_PROYECTOSUBNODO"] = aArgs[1];
                Session["MODOLECTURA_PROYECTOSUBNODO"] = (aArgs[2] == "1") ? true : false;
                Session["RTPT_PROYECTOSUBNODO"] = false;
                Session["MONEDA_PROYECTOSUBNODO"] = aArgs[3];
                sResultado += "OK";
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string ObtenerProyectos(Nullable<int> nPE)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 960px;'>");
            sb.Append("<colgroup>");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:65px;' />");
			sb.Append("<col style='width:355px' />");
            sb.Append("<col style='width:220px' />");
            sb.Append("<col style='width:260px' />");//
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = PROYECTO.ObtenerProyectosParaFacturar((int)Session["UsuarioActual"], nPE);

            while (dr.Read()) 
            {
                sb.Append("<tr style='height:20px' id=\"");
                sb.Append(dr["t305_idproyectosubnodo"].ToString());
                sb.Append("///");
                sb.Append(dr["modo_lectura"].ToString());
                sb.Append("///");
                sb.Append(dr["rtpt"].ToString());
                sb.Append("\" ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("moneda_proyecto='" + dr["t422_idmoneda_proyecto"].ToString() + "' ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;'");
                if (ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
                    sb.Append(" title=\"" + dr["desmotivo"].ToString() + "\"");
                sb.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                
                sb.Append("<td><nobr class='NBR W350' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W210'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W250'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close(); 
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@"+sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos económicos para facturar", ex);
        }
    }
}
