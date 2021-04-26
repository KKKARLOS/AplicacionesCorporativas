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


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sSnds = "";
    public string sErrores = "";//, sExcede = "F",sOrigen="ECO";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                //if (!SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                //{
                //    lblMostrarInactivos.Style.Add("visibility", "hidden");
                //    chkMostrarInactivos.Style.Add("visibility", "hidden");
                //}
                if (Request.QueryString["sExcede"] != null)
                    this.hdnExcede.Value = Request.QueryString["sExcede"].ToString();
                //Indica si la pantalla llamante es de ECO (defecto) o de PST
                if (Request.QueryString["sOrigen"] != null)
                    this.hdnOrigen.Value = Request.QueryString["sOrigen"].ToString();

                //bool bExcede = (sExcede == "T") ? true : false;
                //string strTabla = GenerarArbol(Utilidades.decodpar(Request.QueryString["sSnds"].ToString()), false, bExcede);
                //string strTabla = GenerarArbol(sSnds, false, bExcede);
                //string[] aTabla = Regex.Split(strTabla, "@#@");
                //if (aTabla[0] != "Error") divCatalogo.InnerHtml = aTabla[1];
                //else sErrores = aTabla[1];
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
            case "getEstructura":
                sResultado += GenerarArbol(aArgs[1], (aArgs[2] == "1") ? true : false, (aArgs[3] == "T") ? true : false, aArgs[4]);
                break;
            //case "setEstructura":
            //    sResultado += setArbol(aArgs[1]);
            //    break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string GenerarArbol(string sSubnodos, bool bMostrarInactivos, bool bExcede, string sOrigen)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            string sColor = "black";
            int nNivelExp = 1;
            SqlDataReader dr = null;
            //Si es administrador o vengo de la pantalla de consulta de experiencias profesionales, muestro todo el árbol
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion() || sOrigen=="cvt_exp")
                dr = Estructura.GetEstructuraOrganizativa(bMostrarInactivos);
            else
            {
                if (bExcede || sSubnodos == "")
                {
                    //dr = Estructura.GetEstructuraOrganizativa(bMostrarInactivos);
                    dr = Estructura.GetEstructuraOrganizativa((int)Session["UsuarioActual"], bMostrarInactivos, sOrigen);
                }
                else
                    dr = Estructura.GetEstructuraOrganizativaSubnodos(sSubnodos, bMostrarInactivos);
            }

            if (Utilidades.EstructuraActiva("SN4")) nNivelExp = 1;
            else if (Utilidades.EstructuraActiva("SN3")) nNivelExp = 2;
            else if (Utilidades.EstructuraActiva("SN2")) nNivelExp = 3;
            else if (Utilidades.EstructuraActiva("SN1")) nNivelExp = 4;


            sb.Append("<table class='texto MAM' id='tblDatos' style='width:430px;'>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                //if (
                //    (Utilidades.EstructuraActiva("SN4") == false && (int)dr["INDENTACION"] == 1)
                //    || (Utilidades.EstructuraActiva("SN3") == false && (int)dr["INDENTACION"] == 2)
                //    || (Utilidades.EstructuraActiva("SN2") == false && (int)dr["INDENTACION"] == 3)
                //    || (Utilidades.EstructuraActiva("SN1") == false && (int)dr["INDENTACION"] == 4)
                //    ) continue;
                if ((int)dr["INDENTACION"] < nNivelExp)
                    continue;

                sColor = "black";
                if (!(bool)dr["ESTADO"]) sColor = "gray";

                if ((int)dr["INDENTACION"] <= nNivelExp)
                {
                    sb.Append("<tr id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "' ");
                    sb.Append(" ondblclick='insertarItem(this)' onmousedown='DD(event);' ");
                    sb.Append(" style='display:table-row; height:20px; vertical-align:middle;' nivel=" + dr["INDENTACION"].ToString() + " >");

                    if ((int)dr["INDENTACION"] < 6)
                    {
                        if ((int)dr["INDENTACION"] < nNivelExp) sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../../images/minus.gif' style='cursor:pointer;'>");
                        else sb.Append("<td><IMG class='N" + ((int)dr["INDENTACION"] - nNivelExp+1).ToString() + "' onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                    }
                    else sb.Append("<td><IMG class='N" + ((int)dr["INDENTACION"] - nNivelExp+1).ToString() + "' src='../../../../images/imgSeparador.gif'>");
                }
                else
                {
                    sb.Append("<tr id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "' ");
                    sb.Append(" ondblclick='insertarItem(this)' onmousedown='DD(event)' ");
                    sb.Append(" style='display:none; height:20px; vertical-align:middle;' nivel=" + dr["INDENTACION"].ToString() + " >");
                    sb.Append("<td>");
                    if ((int)dr["INDENTACION"] < 6) sb.Append("<IMG class='N" + ((int)dr["INDENTACION"] - nNivelExp+1).ToString() + "' onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                    else sb.Append("<IMG class='N" + ((int)dr["INDENTACION"] - nNivelExp+1).ToString() + "' src='../../../../images/imgSeparador.gif'>");
                }

                switch ((int)dr["INDENTACION"])
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        sb.Append("<IMG src='../../../../images/imgSN" + dr["sufijoimg"].ToString() + ".gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case 5:
                        sb.Append("<IMG src='../../../../images/imgNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case 6:
                        sb.Append("<IMG src='../../../../images/imgSubNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                }

                sb.Append("<label class='texto MAM' style='color:" + sColor + "'>" + dr["DENOMINACION"].ToString() + "</label></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la estructura organizativa", ex);
        }
    }
}
