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


public partial class getMonedaImportesProyecto : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", strTablaHTML = "", sTipoMoneda = "VDC";

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

            if (Request.QueryString["tm"] != null)
                sTipoMoneda = Request.QueryString["tm"].ToString();

            if (sTipoMoneda == "MG")
                ObtenerMonedasParaGestion();
            else
                ObtenerMonedasParaVisualizacion(sTipoMoneda);

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
            case ("setMonedaImportes"):
                //Grabar la moneda del proyecto
                try
                {
                    SUPER.Capa_Negocio.PROYECTOSUBNODO.UpdateMoneda(int.Parse(aArgs[1]), aArgs[2]);
                    Session["MONEDA_PROYECTOSUBNODO"] = aArgs[2];

                    sResultado += "OK";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al actualizar la moneda.", ex);
                }
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

    protected void ObtenerMonedasParaVisualizacion(string sTipo)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = null;

            if (sTipo == "VDP")
                dr = MONEDA.ObtenerMonedasVDP();
            else
                dr = MONEDA.ObtenerMonedasVDC();

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 300px;'>");
            sb.Append("<colgroup><col style='width:300px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t422_idmoneda"].ToString() + "' onclick='ms(this)' ondblclick='setMonedaImportes(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t422_denominacionimportes"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de monedas.", ex);
        }
    }
    protected void ObtenerMonedasParaGestion()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = MONEDA.ObtenerMonedasGestionarProyectos();

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 300px;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:300px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t422_idmoneda"].ToString() + "' onclick='ms(this)' ondblclick='setMonedaImportes(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t422_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de monedas.", ex);
        }
    }


}
