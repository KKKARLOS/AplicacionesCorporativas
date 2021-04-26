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

using SUPER.Capa_Negocio;

using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;

public partial class Capa_Presentacion_IAP_Consulta_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "", strTablaHTML2 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Consulta de facturabilidad / disponibilidad";
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

            DateTime dDesde;
            DateTime dHasta;

            try
            {
                Utilidades.SetEventosFecha(this.txtDesde);
                Utilidades.SetEventosFecha(this.txtHasta);

                int nMes = 0;
                if (Session["UMC_IAP"] != null) nMes = (int)Session["UMC_IAP"];
                else nMes = DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;

                DateTime dt = Fechas.AnnomesAFecha(nMes);
                dDesde = dt;
                dHasta = dt.AddMonths(1);

                txtDesde.Text = dt.ToShortDateString();
                txtHasta.Text = dt.AddMonths(1).ToShortDateString();

                string strTabla = obtenerDatosFact(txtDesde.Text, txtHasta.Text);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK")
                {
                    this.strTablaHTML = aTabla[1];
                }
                else Master.sErrores += aTabla[1];
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

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, @"##");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += obtenerDatosFact(aArgs[1], aArgs[2]);
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

    private string obtenerDatosFact(string sDesde, string sHasta)
    {
        StringBuilder sb = new StringBuilder();
        bool bError = false;
        string sResul = "";
        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                sb.Append("<table id='tblDatos' style='width: 970px; text-align:right;'>");
                sb.Append("<colgroup>");
                sb.Append("<col style='width:368px;' />");

                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//
                sb.Append("<col style='width:60px;' />");//

                sb.Append("</colgroup>");
                sb.Append("<tbody>");
                //SqlDataReader dr = Consumo.ObtenerConsumosFacturabilidad((int)Session["IDFICEPI_IAP"], DateTime.Parse(sDesde), DateTime.Parse(sHasta));
                SqlDataReader dr = PLANIFAGENDA.ObtenerFacturabilidadDisponibilidad((int)Session["IDFICEPI_IAP"], DateTime.Parse(sDesde), DateTime.Parse(sHasta));
                #region imputaciones
                string sTooltip = "";
                while (dr.Read())
                {
                    if (dr["t303_idnodo"].ToString() != "")
                    {
                        //sTooltip = Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["empresa"].ToString().Replace((char)34, (char)39);
                        sTooltip = Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                    }
                    else
                    {
                        sTooltip = "Proveedor:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["empresa"].ToString();
                    }
                    sb.Append("<tr style='height:20px;'>");
                    sb.Append("<td style='text-align:left;'><nobr class='NBR W350' onmouseover='TTip(event)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTooltip + "] hideselects=[off]\">" + dr["profesional"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                    sb.Append("<td>");
                    sb.Append(double.Parse(dr["horas_calendario"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    sb.Append(double.Parse(dr["horas_disponibles"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_planificadas_facturables"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_facturables"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_planificadas_no_facturables"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_no_facturables"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_planificadas_no_tarea"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_no_tarea"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_planificadas_totales"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_totales"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_reales_facturables"].ToString()) > 0) sb.Append(double.Parse(dr["horas_reales_facturables"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_reales_no_facturables"].ToString()) > 0) sb.Append(double.Parse(dr["horas_reales_no_facturables"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_reales_totales"].ToString()) > 0) sb.Append(double.Parse(dr["horas_reales_totales"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["facturabilidad_teorica"].ToString()) > 0) sb.Append(double.Parse(dr["facturabilidad_teorica"].ToString()).ToString("N"));
                    sb.Append("</td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
                #endregion
                sb.Append("</tbody>");
                sb.Append("</table>");

                sResul = "OK@#@" + sb.ToString();
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los profesionales", ex);
        }
        return sResul;
    }

}
