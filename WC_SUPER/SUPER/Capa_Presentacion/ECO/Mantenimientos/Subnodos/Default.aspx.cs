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
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Administracion_EstructuraOrg_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nNE = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.nBotonera = 35;
        Master.bFuncionesLocales = true;
        Master.TituloPagina = "Estructura organizativa";

        try
        {
            if (!Page.IsCallback)
            {
                string strTabla = GenerarArbol(false, 1);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") divCatalogo.InnerHtml = aTabla[1];
                else Master.sErrores = aTabla[1];
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
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
                sResultado += GenerarArbol((aArgs[1] == "1") ? true : false, int.Parse(aArgs[2]));
                break;
            case "eliminar":
                sResultado += Eliminar(int.Parse(aArgs[1]));
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

    private string GenerarArbol(bool bMostrarInactivos, int nNivelExp)
    {
        try
        {
            SqlDataReader dr = Estructura.GetEstructuraMantNodo((int)Session["UsuarioActual"], bMostrarInactivos);
            StringBuilder sb = new StringBuilder();
            string sColor = "black";
            sb.Append("<div id='divCatalogo2' style='background-image:url(" + Session["strServer"] + "Images/imgFT20.gif); width:500px;' >");
            sb.Append("<TABLE class='texto MA' id=tblDatos style='width: 500px;'>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sColor = "black";
                if (!(bool)dr["ESTADO"]) sColor = "gray";
                sb.Append("<tr ");
                sb.Append("id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "' ");
                
                if ((int)dr["INDENTACION"] <= nNivelExp)
                {
                    sb.Append(" style='DISPLAY: table-row; HEIGHT: 20px; vertical-align:middle;' nivel=" + dr["INDENTACION"].ToString() + " ");
                    if ((int)dr["INDENTACION"] == 2) sb.Append("ondblclick='mdn(this)' ");
                    sb.Append(">");
                    if ((int)dr["INDENTACION"] == 2) sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' src='../../../../images/imgSeparador.gif'>");
                    else
                    {
                        if ((int)dr["INDENTACION"] < nNivelExp) sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../../images/minus.gif' style='cursor:pointer;'>");
                        else sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                    }
                }
                else
                {
                    sb.Append(" style='DISPLAY: none; HEIGHT: 20px; vertical-align:middle;' nivel=" + dr["INDENTACION"].ToString() + " ");
                    if ((int)dr["INDENTACION"] == 2) sb.Append("ondblclick='mdn(this)' ");
                    sb.Append(">");
                    if ((int)dr["INDENTACION"] < 2) sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                    else sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' src='../../../../images/imgSeparador.gif'>");
                }

                //if ((int)dr["INDENTACION"] < 5) sb.Append("<IMG src='../../../../images/imgSN" + dr["sufijoimg"].ToString() + ".gif' style='margin-left:3px;margin-right:3px;'>");
                //else sb.Append("<IMG src='../../../../images/imgNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                switch ((int)dr["INDENTACION"])
                {
                    case 1:
                        sb.Append("<IMG src='../../../../images/imgNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                        sb.Append("<label class='texto MANO' onclick='marcarLabel(this)' style='vertical-align:top; margin-top:3px; color:" + sColor + "'>" + dr["DENOMINACION"].ToString() + "</label>");
                        break;
                    case 2:
                        sb.Append("<IMG src='../../../../images/imgSubNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                        sb.Append("<label class='texto MA' onclick='marcarLabel(this)' style='vertical-align:top; margin-top:3px; color:" + sColor + "'>" + dr["DENOMINACION"].ToString() + "</label></td>");
                        break;
                }
                sb.Append("</td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la estructura organizativa", ex);
        }
    }
    private string Eliminar(int nIDItem)
    {
        try
        {
            SUBNODO.Delete(null, nIDItem);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar el subnodo", ex);
        }
    }

}
