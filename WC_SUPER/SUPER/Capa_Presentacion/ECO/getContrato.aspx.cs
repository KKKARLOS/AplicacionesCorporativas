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


public partial class getContrato : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sErrores = "", sOrigen="";

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
//                string sMostrarTodos = "0";
                sOrigen = Request.QueryString["origen"].ToString();
                this.hdnIdNodo.Value = Request.QueryString["nNodo"].ToString();

                if (sOrigen == "busqueda")
                {
                    //sMostrarTodos = "1";
                    chkTodos.Checked = true;
                }
                //string strTabla = ObtenerContratos(Request.QueryString["nNodo"].ToString(), sMostrarTodos, sOrigen,null,"","I",null);
                //string[] aTabla = Regex.Split(strTabla, "@#@");
                //if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                //else sErrores = aTabla[1];
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los proyectos", ex);
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
            case ("contrato"):
                sResultado += ObtenerContratos(aArgs[1], aArgs[2], aArgs[3], (aArgs[4] == "") ? null : (int?)int.Parse(aArgs[4]), Utilidades.unescape(aArgs[5]), aArgs[6], (aArgs[7] == "") ? null : (int?)int.Parse(aArgs[7]));
                break;
            case ("contratoID"):
                sResultado += ObtenerContratoID(int.Parse(aArgs[1]));
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

    private string ObtenerContratoID(int idContrato)
    {
        try
        {
            string sValor = "";
            SqlDataReader dr = CONTRATO.ObtenerExtensionPadre(idContrato);
            if (dr.Read()) sValor = Utilidades.escape(dr["t377_denominacion"].ToString());

            dr.Close();
            dr.Dispose();

            return "OK@#@" + sValor;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el Contrato.", ex);
        }
    }

    private string ObtenerContratos(string sNodo, string sTodos, string sOrigen, Nullable<int> iIdContrato, string sDesContrato, string sTipoBusq, Nullable<int> iIdCliente )
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 970px;'>");
            sb.Append("<colgroup><col style='width:360px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:220px;' /></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = null;

            if (sOrigen == "proyecto")
                dr = CONTRATO.ObtenerContratos((sNodo == "") ? null : (int?)int.Parse(sNodo), (sTodos == "1") ? true : false, (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) ? null :(int?)Session["UsuarioActual"]  , iIdContrato, sDesContrato, sTipoBusq, iIdCliente);
            else
                dr = CONTRATO.ObtenerContratosVisionProyectos((sNodo == "") ? null : (int?)int.Parse(sNodo), (sTodos == "1") ? true : false, (int)Session["UsuarioActual"], iIdContrato, sDesContrato, sTipoBusq, iIdCliente);

            string sDenominacion = "", sPendienteProducto = "", sPendienteServicio = "", sIDCliente = "", sDesCliente = "";
            while (dr.Read())
            {
                if ((int)dr["t377_idextension"] == 0)
                {
                    sDenominacion = Utilidades.escape(dr["t377_denominacion"].ToString());
                    sPendienteProducto = double.Parse(dr["pendiente_producto"].ToString()).ToString("N");
                    sPendienteServicio = double.Parse(dr["pendiente_servicio"].ToString()).ToString("N");
                    sIDCliente = dr["t302_idcliente_contrato"].ToString();
                    sDesCliente = Utilidades.escape(dr["t302_denominacion"].ToString());

                    sb.Append("<tr id=\"" + dr["t306_idcontrato"].ToString() + "///" + sDenominacion + "///" + sIDCliente + "///" + sDesCliente + "///" + sPendienteProducto + "///" + sPendienteServicio + "///" + dr["t195_idlineaoferta"].ToString() + "///" + dr["t195_denominacion"].ToString() + "\"");
                    sb.Append(" ondblclick=\"aceptarClick(this.rowIndex)\" ");
                    sb.Append(" nNivel=1 style='height:16px;display:table-row;' >");
                    sb.Append("<td><img src='../../images/plus.gif' onclick=\"me(this)\" style='margin-left:2px;cursor:pointer;margin-right:10px;'><nobr class='NBR W320'>" + HttpUtility.HtmlEncode(dr["t377_denominacion"].ToString()) + "</nobr></td>");
                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###") + "</td>");

                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + double.Parse(dr["importe_producto"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + double.Parse(dr["pendiente_producto"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + double.Parse(dr["importe_servicio"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + double.Parse(dr["pendiente_servicio"].ToString()).ToString("N") + "</td>");

                    sb.Append("<td style='padding-left:5px;'><nobr class='NBR W210'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                }
                else
                {
                    sb.Append("<tr id='" + dr["t306_idcontrato"].ToString() + "///" + sDenominacion + "///" + sIDCliente + "///" + sDesCliente + "///" + sPendienteProducto + "///" + sPendienteServicio + "'");
                    sb.Append(" ondblclick=\"aceptarClick(this.rowIndex)\" ");
                    sb.Append(" nNivel=2 style='height:16px;display:none;' >");
                    sb.Append("<td><nobr style='margin-left:40px;' class='NBR W300'>" + HttpUtility.HtmlEncode(dr["t377_denominacion"].ToString()) + "</nobr></td>");
                    sb.Append("<td></td><td></td><td></td><td></td><td></td><td></td>");
                }
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los contratos", ex);
        }
    }
}
