using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BLL = IB.SUPER.APP.BLL;
using Models = IB.SUPER.APP.Models;
using Shared = IB.SUPER.Shared;

public partial class getNLO : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sErrores = "";
    public int nNE = 0;

    private void Page_Load(object sender, System.EventArgs e)
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

                string strTabla = ObtenerNLO(false);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                else sErrores = aTabla[1];
            }
            catch (Exception ex)
            {
                sErrores = SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener las líneas de oferta", ex);
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("lineas"):
                bool bMostrarInactivos = false;
                if (aArgs[1] == "1") bMostrarInactivos = true;
                 sResultado += ObtenerNLO(bMostrarInactivos);
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

    private string ObtenerNLO(bool bMostrarInactivos)
    {
        BLL.LineaOferta bLinea = new BLL.LineaOferta();
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MANO' style='width: 500px;'>");
            //sb.Append("<colgroup><col style='width:500px;' /></colgroup>");
            sb.Append("<tbody id='tbody'>");

            
            List<Models.LineaOferta> lLineas = bLinea.Catalogo(bMostrarInactivos);

            string sDenArea = "", sDenNLO="";
            foreach(Models.LineaOferta oLinea in lLineas)
            {
                if (oLinea.indentacion == 1)
                {
                    sDenArea = SUPER.Capa_Negocio.Utilidades.escape(oLinea.ta212_denominacion);
                    sb.Append("<tr id=\"" + oLinea.ta212_idorganizacioncomercial.ToString() + "\" nNivel=1 style='height:16px;display:table-row;' >");
                    sb.Append("<td><img src='../../images/plus.gif' onclick=\"me(this)\" style='margin-left:2px;cursor:pointer;margin-right:10px;'>"); 
                    
                    if (oLinea.activo)
                        sb.Append("<nobr class='NBR W475'>" + HttpUtility.HtmlEncode(oLinea.ta212_denominacion) + "</nobr></td>");
                    else
                        sb.Append("<nobr class='NBR W475' style ='color:Red;'>" + HttpUtility.HtmlEncode(oLinea.ta212_denominacion) + "</nobr></td>");
                }
                else
                {
                    sDenNLO = SUPER.Capa_Negocio.Utilidades.escape(oLinea.t195_denominacion);
                    sb.Append("<tr idL='" + oLinea.t195_idlineaoferta.ToString() + "///" + sDenNLO + "' ondblclick=\"aceptarClick(this.rowIndex)\" ");
                    sb.Append(" id=\"" + oLinea.ta212_idorganizacioncomercial.ToString() + "\" ");
                    sb.Append(" nNivel=2 style='height:16px;display:none;' >");
                    if (oLinea.activo)
                        sb.Append("<td><nobr style='margin-left:40px;' class='NBR W440 MA'>" + HttpUtility.HtmlEncode(oLinea.t195_denominacion) + "</nobr></td>");
                    else
                        sb.Append("<td><nobr style='margin-left:40px;color:Red;' class='NBR W440 MA'>" + HttpUtility.HtmlEncode(oLinea.t195_denominacion) + "</nobr></td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener las nuevas líneas de oferta", ex);
        }
        finally
        {
            bLinea.Dispose();
        }
    }
}