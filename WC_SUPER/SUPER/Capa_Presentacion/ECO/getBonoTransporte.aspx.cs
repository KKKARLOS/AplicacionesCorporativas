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


public partial class getBonoTransporte : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", strTablaHTML = "", sUsuario = "";

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

                if (Request.QueryString["iu"] != null)
                    sUsuario = Utilidades.decodpar(Request.QueryString["iu"].ToString());
                string[] aTablas = Regex.Split(ObtenerBonos(sUsuario, "1"), "@#@");
                if (aTablas[0] == "OK")
                    strTablaHTML = aTablas[1];
                else
                    sErrores = aTablas[1];
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscarBonos"):
                sResultado += ObtenerBonos(aArgs[1], aArgs[2]);
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

    protected string ObtenerBonos(string sUsuario, string sSoloVigentes)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px;'>");
            sb.Append("<colgroup>");
            sb.Append("  <col style='width:300px;' />");
            sb.Append(" <col style='width:100px;' />");
            sb.Append(" <col style='width:100px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = BONO_TRANSPORTE.ObtenerParaAsignacion(null, int.Parse(sUsuario), (sSoloVigentes == "1") ? true : false);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t655_idbono"].ToString() + "' ");
                sb.Append("fivb='" + dr["t657_desde"].ToString() + "' ");
                sb.Append("ffvb='" + dr["t657_hasta"].ToString() + "' ");
                sb.Append("ondblclick='aceptarClick(this.rowIndex)' style='height:16px'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t655_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + Fechas.AnnomesAFechaDescLarga((int)dr["t657_desde"]) + "</td>");
                sb.Append("<td>" + Fechas.AnnomesAFechaDescLarga((int)dr["t657_hasta"]) + "</td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de bonos de transporte.", ex);
        }
    }

}
