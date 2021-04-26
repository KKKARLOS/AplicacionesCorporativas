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

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos' class='texto' style='WIDTH: 980px; table-layout:fixed; BORDER-COLLAPSE: collapse;' cellSpacing='0' cellPadding='0' border='0' ></table>";
    public int iAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "38,18";
            Master.sbotonesOpcionOff = "";
            Master.TituloPagina = "Información mensual";
            Master.bFuncionesLocales = true;

            try
            {
                AGENDAUSA.CrearAgendaUSAAuto(null, (int)Session["UsuarioActual"], DateTime.Today.Year * 100 + DateTime.Today.Month);

                string[] aTabla = Regex.Split(ObtenerDatos(), "@#@");
                if (aTabla[0] == "OK") strTablaHTML = aTabla[1];
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("getDatos"):
                sResultado += ObtenerDatos();
                break;
            case ("ExportarExcel"):
                sResultado += ExportarExcel(aArgs[1], aArgs[2]);
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

    private string ObtenerDatos()
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.InformeUSA((int)Session["UsuarioActual"]);
            sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 970px;'>");
            sb.Append("<colgroup><col style='width:70px;' /><col style='width:300px;' /><col style='width:300px;' /><col style='width:300px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' style=' height:20px' onclick='mm(event)' onmouseover='TTip(event)'>");
                sb.Append("<td style='padding-left:10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("###,###") + "</td>");
                sb.Append("<td><nobr class='NBR W300'>" + dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W300'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W300'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
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
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos mensuales de la agenda", ex);
        }

        return sResul;

    }
    private string ExportarExcel(string sProyectos, string sAnomes)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {

            SqlDataReader dr = PROYECTOSUBNODO.ObtenerInformeUSAExcel(sProyectos, int.Parse(sAnomes));
            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("<tr align='center'>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº proyecto</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Denominación de proyecto</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>"+ Estructura.getDefLarga(Estructura.sTipoElem.NODO) +"</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Responsable de proyecto</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Cliente</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Tipo de facturación</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Mes</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Consumos</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Producción</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Facturación</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Otros</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Partida</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Comunicado</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>USA / Responsable comunicado</TD>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº documentos</TD>");
            sb.Append("</TR>");

            while (dr.Read())
            {
                sb.Append("<tr style='vertical-align:top;' >");
                if (dr["tipo"].ToString() == "A") sb.Append("<td>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                else sb.Append("<td></td>");
                sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["ResponsableProyecto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["TipoFacturacion"].ToString() + "</td>");
                sb.Append("<td>&nbsp;" + dr["Mes"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_consumos"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_produccion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_facturacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t641_otros"].ToString() + "</td>");

                sb.Append("<td>" + dr["partidas_comunicado"].ToString() + "</td>");
                sb.Append("<td>" + dr["desc_comunicado"].ToString() + "</td>");

                if (dr["tipo"].ToString() == "A")
                {
                    sb.Append("<td>USA: " + dr["SAT"].ToString());
                    if (dr["SAA"].ToString() != "")
                        sb.Append(" / " + dr["SAA"].ToString());
                    sb.Append("</td>");
                }
                else sb.Append("<td>RC: " + dr["usu_comunicado"].ToString() + "</td>");

                sb.Append("<td>" + dr["num_doc"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos mensuales de la agenda", ex);
        }

        return sResul;

    }

}
