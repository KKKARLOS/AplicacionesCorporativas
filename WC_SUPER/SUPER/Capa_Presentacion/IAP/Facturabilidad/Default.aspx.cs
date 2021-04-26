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
//Para el grafico
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class Capa_Presentacion_IAP_Consulta_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "", strTablaHTML2 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sPantallaCompleta = "F";
        
        if (!Page.IsCallback)
        {

            
            Master.sbotonesOpcionOn = "71";
            if (!(bool)Session["IAPFACT1024"])
            {
                Master.nResolucion = 1280;
                sPantallaCompleta = "T";
            }
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Consulta de facturabilidad";
            //Master.FuncionesJavaScript.Add("Javascript/FusionCharts.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            
           DateTime dDesde;
           DateTime dHasta;

           try
           {
               
               if (!Page.IsPostBack)
               {
                   Utilidades.SetEventosFecha(this.txtDesde);
                   Utilidades.SetEventosFecha(this.txtHasta);

                   int nMes = 0;
                   if (Session["UMC_IAP"] != null) nMes = (int)Session["UMC_IAP"];
                   else nMes = DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;

                   DateTime dt = Fechas.AnnomesAFecha(nMes).AddMonths(1);
                   dDesde = dt;
                   dHasta = dt.AddMonths(1).AddDays(-1);

                   txtDesde.Text = dt.ToShortDateString();
                   txtHasta.Text = dHasta.ToShortDateString();
               }
                
               string strTabla = obtenerDatosFact(txtDesde.Text, txtHasta.Text, sPantallaCompleta);
               string[] aTabla = Regex.Split(strTabla, "@#@");
               if (aTabla[0] == "OK")
               {
                   this.strTablaHTML = aTabla[1];
                   this.strTablaHTML2 = aTabla[2];
               }
               else Master.sErrores += aTabla[1];
                
           }
           catch (Exception ex)
           {
               Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
           }
           try
           {
               obtenerOpcionReconexion();
           }
           catch (Exception ex)
           {
               Master.sErrores = Errores.mostrarError("Error al establecer la opción de reconexión", ex);
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
            //case ("buscar"):
            //    sResultado += obtenerDatosFact(aArgs[1], aArgs[2], aArgs[3]);
            //    break;
            case ("setResolucion"):
                sResultado += setResolucion();
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

    private string obtenerDatosFact(string sDesde, string sHasta, string sPantallaCompleta)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "", sToolTip = "";
        SqlDataReader dr;
        bool bError = false;
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
                if (sPantallaCompleta == "T")
                {
                    sb.Append("<table id='tblDatos' style='width:1220px; text-align:right;'>");
                    sb.Append("<colgroup>");
                    sb.Append("<col style='width:295px;' />");
                    sb.Append("<col style='width:300px;' />");

                    sb.Append("<col style='width:25px;' />");
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
                    dr = Consumo.ObtenerConsumosFacturabilidad((int)Session["IDFICEPI_IAP"], DateTime.Parse(sDesde), DateTime.Parse(sHasta));
                    #region imputaciones
                    while (dr.Read())
                    {
                        sToolTip = "<label style='width:60px'>Proy. Tec.:</label>" + dr["t331_despt"].ToString();
                        if (dr["t334_desfase"].ToString() != "") sToolTip += "<br><label style='width:60px'>Fase:</label>" + dr["t334_desfase"].ToString();
                        if (dr["t335_desactividad"].ToString() != "") sToolTip += "<br><label style='width:60px'>Actividad:</label>" + dr["t335_desactividad"].ToString();
                        sToolTip += "<br><label style='width:60px'>Tarea:</label>" + dr["t332_destarea"].ToString().Replace((char)34, (char)39);

                        sb.Append("<tr style='height:20px;'>");
                        sb.Append("<td style='text-align:left;'><nobr class='NBR W280' onmouseover='TTip(event)'>" + double.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t305_seudonimo"].ToString() + "</nobr></td>");
                        sb.Append("<td style='border-right:0px;text-align:left'><nobr class='NBR W280 MANO' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sToolTip + "] hideselects=[off]\">" + double.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["t332_destarea"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                        sb.Append("<td>");
                        if ((bool)dr["t332_facturable"]) sb.Append("<img src='" + Session["strServer"].ToString() + "images/imgIcoMonedas.gif' width='16' height='16' class='ICO'>");
                        else sb.Append("<img src='" + Session["strServer"].ToString() + "images/imgIcoMonedasOff.gif' width='16' height='16' class='ICO'>");
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["t332_etpl"].ToString()) > 0) sb.Append(double.Parse(dr["t332_etpl"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["t336_etp"].ToString()) > 0) sb.Append(double.Parse(dr["t336_etp"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_planificadas_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_tecnico_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_tecnico_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_otros_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_otros_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_total_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_total_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_planificadas_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_tecnico_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_tecnico_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_otros_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_otros_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_total_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_total_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("</tr>");
                    }
                    dr.Close();
                    dr.Dispose();
                    #endregion
                    sb.Append("</tbody>");
                    sb.Append("</table>");

                }
                else
                {//Pantalla a 1024
                    sb.Append("<table id='tblDatos' style='width:980px; text-align:right;'>");
                    sb.Append("<colgroup>");
                    sb.Append("<col style='width:175px;' />");
                    sb.Append("<col style='width:185px;' />");

                    sb.Append("<col style='width:20px;' />");
                    sb.Append("<col style='width:60px;;' />");//
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
                    dr = Consumo.ObtenerConsumosFacturabilidad((int)Session["IDFICEPI_IAP"], DateTime.Parse(sDesde), DateTime.Parse(sHasta));
                    #region imputaciones
                    while (dr.Read())
                    {
                        sToolTip = "<label style='width:60px'>Proy. Tec.:</label>" + dr["t331_despt"].ToString();
                        if (dr["t334_desfase"].ToString() != "") sToolTip += "<br><label style='width:60px'>Fase:</label>" + dr["t334_desfase"].ToString();
                        if (dr["t335_desactividad"].ToString() != "") sToolTip += "<br><label style='width:60px'>Actividad:</label>" + dr["t335_desactividad"].ToString();
                        sToolTip += "<br><label style='width:60px'>Tarea:</label>" + dr["t332_destarea"].ToString().Replace((char)34, (char)39);

                        sb.Append("<tr style='height:20px;'>");

                        sb.Append("<td style='text-align:left;'><nobr class='NBR W170' onmouseover='TTip(event)'>" + double.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t305_seudonimo"].ToString() + "</nobr></td>");
                        sb.Append("<td style='border-right:0px;text-align:left'><nobr class='NBR W170 MANO' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sToolTip + "] hideselects=[off]\">" + double.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["t332_destarea"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                        sb.Append("<td>");
                        if ((bool)dr["t332_facturable"]) sb.Append("<img src='" + Session["strServer"].ToString() + "images/imgIcoMonedas.gif' width='16' height='16' class='ICO'>");
                        else sb.Append("<img src='" + Session["strServer"].ToString() + "images/imgIcoMonedasOff.gif' width='16' height='16' class='ICO'>");
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["t332_etpl"].ToString()) > 0) sb.Append(double.Parse(dr["t332_etpl"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["t336_etp"].ToString()) > 0) sb.Append(double.Parse(dr["t336_etp"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_planificadas_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_tecnico_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_tecnico_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_otros_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_otros_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_total_periodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_total_periodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_planificadas_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_planificadas_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_tecnico_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_tecnico_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_otros_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_otros_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("<td>");
                        if (double.Parse(dr["horas_total_finperiodo"].ToString()) > 0) sb.Append(double.Parse(dr["horas_total_finperiodo"].ToString()).ToString("N"));
                        sb.Append("</td>");

                        sb.Append("</tr>");
                    }
                    dr.Close();
                    dr.Dispose();
                    #endregion
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                }
                dr = Consumo.ObtenerIndicadoresFacturabilidad((int)Session["IDFICEPI_IAP"], DateTime.Parse(sDesde), DateTime.Parse(sHasta));
                #region indicadores
                double dFact = 0, dNoFact = 0;
                sb.Append("@#@<table id='tblDatos2' class='texto' style='width:700px;text-align:right;' cellpadding='0' cellspacing='0'>");
                sb.Append("<colgroup>");
                sb.Append("<col style='width:195px;' />");
                sb.Append("<col style='width:300px;' />");

                sb.Append("<col style='width:100px;' />");//
                sb.Append("<col style='width:100px;' />");//
                sb.Append("</colgroup>");
                sb.Append("<tbody>");
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px;'>");
                    sb.Append("<td style='text-align:left;padding-left:2px;'><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["t320_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td style='text-align:left;padding-left:2px;'><nobr class='NBR W280' onmouseover='TTip(event)'>" + dr["t323_denominacion"].ToString() + "</nobr></td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_facturables"].ToString()) > 0)
                    {
                        sb.Append(double.Parse(dr["horas_facturables"].ToString()).ToString("N"));
                        dFact += double.Parse(dr["horas_facturables"].ToString());
                    }
                    sb.Append("</td>");

                    sb.Append("<td>");
                    if (double.Parse(dr["horas_no_facturables"].ToString()) > 0)
                    {
                        sb.Append(double.Parse(dr["horas_no_facturables"].ToString()).ToString("N"));
                        dNoFact += double.Parse(dr["horas_no_facturables"].ToString());
                    }
                    sb.Append("</td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                GenerarGrafico(dFact, dNoFact);
                #endregion
                sResul = "OK@#@" + sb.ToString();
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de facturabililidad", ex);
        }
        return sResul;
    }
    private void obtenerOpcionReconexion()
    {
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion() || User.IsInRole("S"))//SECRETARIA --  PENDIENTE DE DETERMINAR QUÉ HARÁN LAS SECRETARIAS
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "A";
        }
        else if (Session["reconectar_iap"].ToString() == "" && User.IsInRole("RG"))
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "RG";
        }
        else
        {
            //Contemplar que la persona pueda tener dos usuario con los que imputar porque hace menos de 35 días que se le ha dado de baja
            //Ej: externo que pasa a interno
            if (Recurso.ObtenerCountUsuarios(Session["IDRED"].ToString()) >= 1)
            {
                Session["reconectar_iap"] = "1";
                Session["perfil_iap"] = "P";  //Personal
            }
        }
    }
    private string setResolucion()
    {
        try
        {
            Session["IAPFACT1024"] = !(bool)Session["IAPFACT1024"];

            USUARIO.UpdateResolucion(12, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["IAPFACT1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }
    protected void GenerarGrafico(double dHorasFact, double dHorasNoFact)
    {
        // Establezco el tipo de grafico 
        //Chart1.Series["Default"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Doughnut", true);
        // Relleno los datos
        double dPorFact = (dHorasFact * 100) / (dHorasFact + dHorasNoFact);
        double dPorNoFact = 100 - dPorFact;
        double[] yValues = { dPorFact, dPorNoFact };
        string[] xValues = { dPorFact.ToString("#,###.##") + "%", dPorNoFact.ToString("#,###.##") + "%" };
        Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);
        // Remove supplemental series and chart area if they already exsist
        if (Chart1.Series.Count > 1)
        {
            Chart1.Series.RemoveAt(1);
            Chart1.ChartAreas.RemoveAt(1);
            // Reset automatic position for the default chart area
            Chart1.ChartAreas["ChartArea1"].Position.Auto = true;
        }
        Chart1.Series[0].Points[0].Color = Color.FromArgb(248, 209, 76);
        Chart1.Series[0].Points[1].Color = Color.FromArgb(213, 213, 213);
    }

}
