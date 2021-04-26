using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;


public partial class Capa_Presentacion_eco_informes_RExternos_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected ArrayList aProyecto;
    public string strTablaHtml;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Informe de colaboradores externos por proveedor";
        Master.bFuncionesLocales = true;

        if (!Page.IsCallback)
        {
            strTablaHtml = "<tr id='*' class='FA'><td>&lt; Todos &gt;</td></tr>";
            lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
            //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                divMonedaImportes.Style.Add("visibility", "visible");

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

            case ("generarExcel"):
                sResultado += generarExcel(aArgs[1], aArgs[2], aArgs[3]);
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

    protected string generarExcel(string sCodigos, string sEstado, string sURL)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PROVEEDOR.Excel(sCodigos, sEstado, Session["MONEDA_VDC"].ToString());

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<colgroup><col/><col style='width:315px;' /><col /><col /><col /><col  /><col  /><col  /></colgroup>");

            //sb.Append("<tbody>");
            //int i = 0;
            sb.Append("<tr style='height:16px;noWrap:true;'>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proveedor</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Profesional</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Coste (jornada)</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Coste (hora)</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Baja</td>");
            sb.Append("</tr>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;noWrap:true;'>");

                sb.Append("<td>" + dr["t315_denominacion"].ToString() + "</td>");

                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");
                sb.Append("<td>" + dr["t314_costejornada"].ToString() + "</td>");
                sb.Append("<td>" + dr["t314_costehora"].ToString() + "</td>");
                sb.Append("<td>" + dr["T001_FECBAJA"].ToString() + "</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            //sb.Append("<tr><td colspan='5' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td></tr>");
            sb.Append("<tr style='vertical-align:top;'>");
            sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td>");

            for (var j = 2; j <= 5; j++)
            {
                sb.Append("<td></td>");
            }
            sb.Append("</tr>" + (char)13);
            //sb.Append("</tbody>");
            sb.Append("</table>");
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al generar el Excel.", ex);
        }
    }

}