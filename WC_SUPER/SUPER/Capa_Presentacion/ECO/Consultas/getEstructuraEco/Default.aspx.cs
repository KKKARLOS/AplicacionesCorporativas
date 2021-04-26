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
    public string sErrores = "", sGrupoEco;

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

                if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "")
                {
                    lblMostrarInactivos.Style.Add("visibility", "hidden");
                    chkMostrarInactivos.Style.Add("visibility", "hidden");
                }
                sGrupoEco = Request.QueryString["nGrupo"].ToString();
                if (sGrupoEco == "") sGrupoEco = "1";
                switch (sGrupoEco)
                {
                    case "1":
                        this.lblGrupoEco.Text = "Grupo Consumos";
                        break;
                    case "2":
                        this.lblGrupoEco.Text = "Grupo Producción";
                        break;
                    case "3":
                        this.lblGrupoEco.Text = "Grupo Ingresos";
                        break;
                }
                string strTabla = GenerarArbol(sGrupoEco, true);

                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") divCatalogo.InnerHtml = aTabla[1];
                else sErrores = aTabla[1];
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
                sResultado += GenerarArbol(aArgs[1], (aArgs[2] == "1") ? true : false);
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

    private string GenerarArbol(string sIdGrupo, bool bMostrarInactivos)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            string sColor = "black", sDenGrupo="";
            SqlDataReader dr = null;

            switch (sIdGrupo)
            {
                case "1":
                    sDenGrupo = "Consumo";
                    break;
                case "2":
                    sDenGrupo = "Producción";
                    break;
                case "3":
                case "4":
                    sDenGrupo = "Ingresos";
                    break;
            }
            dr = GRUPOECO.GetEstructuraEconomica(byte.Parse(sIdGrupo), bMostrarInactivos);
            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width:430px'>");
            sb.Append("<table id=tblDatos class='texto' style='width: 430px; margin-top:3px;'>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sColor = "black";
                if (dr["ESTADO"].ToString() != "1") sColor = "gray";
                sb.Append("<tr id='" + dr["CLASE"].ToString() + "' ");
                if ((int)dr["INDENTACION"] == 4)
                    sb.Append(" onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' cl='S'");
                else
                    sb.Append(" cl='N'");

                sb.Append(" style='display:table-row; height:16px; vertical-align:middle;' nivel=" + dr["INDENTACION"].ToString());

                if ((int)dr["INDENTACION"] < 4)
                {
                    sb.Append("><td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../../images/minus.gif' style='cursor:pointer;'>");
                    sb.Append("<label class='texto' style='margin-left:3px;color:" + sColor + "'>" + dr["DENOMINACION"].ToString() + "</label></td>");
                }
                else
                {
                    sb.Append(" grupo='" + sDenGrupo + "' subgrupo='" + dr["DenSubgrupo"] + "' concepto='" + dr["DenConcepto"]);
                    sb.Append("'>");
                    sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' src='../../../../images/imgSeparador.gif'>");
                    sb.Append("<label class='texto MAM' style='margin-left:3px;color:" + sColor + "' ondblclick='insertarItem(this.parentNode.parentNode)'");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' ");
                    sb.Append(" style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Grupo:</label>" + sDenGrupo);
                    sb.Append("<br><label style='width:70px;'>Subgrupo:</label>" + dr["DenSubgrupo"].ToString().Replace((char)34, (char)39));
                    sb.Append("<br><label style='width:70px;'>Concepto:</label>" + dr["DenConcepto"].ToString().Replace((char)34, (char)39));
                    sb.Append("] hideselects=[off]\" >" + dr["DENOMINACION"].ToString() + "</label></td>");
                }
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
}
