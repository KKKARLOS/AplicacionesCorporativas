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
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class getDestFact : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sIdCliente = "", sOVSAP = "", strTablaHTML = "";

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

                sIdCliente = Request.QueryString["sIdCliente"].ToString();
                sOVSAP = Request.QueryString["sOVSAP"].ToString();

                string[] aTabla = Regex.Split(ObtenerDestFact(sIdCliente, sOVSAP), "@#@");
                if (aTabla[0] == "OK")
                    this.strTablaHTML = aTabla[1];
                else 
                    sErrores += Errores.mostrarError(aTabla[1]);

            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los clientes", ex);
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
        //switch (aArgs[0])
        //{
        //    case ("cliente"):
        //        sResultado += ObtenerDestFact(aArgs[1], aArgs[2]);
        //        break;
        //}

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string ObtenerDestFact(string sIdCliente, string sOVSAP)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 900px;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:340px;'>");
            sb.Append("     <col style='width:200px;'>");
            sb.Append("     <col style='width:60px;'>");
            sb.Append("     <col style='width:200px;'>");
            sb.Append("     <col style='width:100px;'>");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = CLIENTE.ObtenerDestinatariosDeFactura(int.Parse(sIdCliente), sOVSAP);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' ");
                sb.Append(" nif='" + dr["NIF"].ToString() + "' ");
                sb.Append(" onclick=\"ms(this)\" ondblclick=\"aceptarClick(this.rowIndex)\" style='height:16px;' onmouseover='TTip(event)'>");

                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W330'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190'>" + dr["direccion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["cp"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W190'>" + dr["poblacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W90'>" + dr["pais"].ToString() + "</nobr></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los clientes destinatarios de factura", ex);
        }
    }
}
