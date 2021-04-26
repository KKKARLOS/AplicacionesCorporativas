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

public partial class getCliente : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sInterno = "0", sSoloActivos = "0", sTipo = "S", strTablaHTML="";

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

                if (Request.QueryString["interno"] != null)
                    sInterno = Request.QueryString["interno"].ToString();
                if (Request.QueryString["sSoloActivos"] != null)
                    sSoloActivos = Request.QueryString["sSoloActivos"].ToString();

                sTipo = Request.QueryString["sTipo"].ToString();
                 
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
        switch (aArgs[0])
        {
            case ("cliente"):
                sResultado += ObtenerClientes(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
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

    private string ObtenerClientes(string sTipoBusqueda, string strCli, string sInterno, string sSoloActivos, string sTipoCliente)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 550px;'>");
            sb.Append("<colgroup><col style='width:450px;'><col style='width:100px;'></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = CLIENTE.SelectByNombreSAP(strCli, sTipoBusqueda, (sSoloActivos == "1") ? true : false, (sInterno == "1") ? true : false, sTipoCliente);

            int i = 0;
            bool bExcede = false;
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' ");
                sb.Append(" nif='" + dr["NIF"].ToString() + "' ");
                if ((bool)dr["t302_efactur"]) sb.Append(" ef='S' ");//cliente de facturación electrónica
                else sb.Append(" ef='N' ");

                if ((int)dr["t302_estado"] == 1) sb.Append(" onclick=\"ms(this)\" ondblclick=\"aceptarClick(this.rowIndex)\" style='height:16px;'");
                else sb.Append("style='height:16px;cursor:default;color:gray;'");

                sb.Append("><td style='padding-left:5px;'><img src='../../../../images/img" + dr["tipo"].ToString() + ".gif' ");
                if (dr["tipo"].ToString() == "M") sb.Append("style='margin-right:5px;'");
                else sb.Append("style='margin-left:15px;margin-right:5px;'");
                sb.Append("><nobr class='NBR W400'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["NIF"].ToString() + "</td>");
                sb.Append("</tr>");
                i++;
                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();
            if (!bExcede)
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
            }else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los clientes", ex);
        }
    }
}
