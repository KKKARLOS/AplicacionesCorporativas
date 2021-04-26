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
using System.Text;
using SUPER.Capa_Negocio;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.sbotonesOpcionOn = "18";
                Master.sbotonesOpcionOff = "18";
                Master.bFuncionesLocales = true;
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.Modulo = "CVT";

                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                DateTime dHoy = DateTime.Now, dtAux;
                dtAux = dHoy.AddDays(-1);
                txtFechaFin.Text = dtAux.ToShortDateString();
                dtAux = dtAux.AddMonths(-1);
                txtFechaInicio.Text = dtAux.ToShortDateString();

                Master.TituloPagina = "Consulta de incumplimientos propios";
                Master.bFuncionesLocales = true;
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
     
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerResumen(DateTime.Parse(aArgs[1]), DateTime.Parse(aArgs[2]));
                break;        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    public string ObtenerResumen(DateTime dDesde, DateTime dHasta)
    {
        StringBuilder sb = new StringBuilder();
        int iTotRealizadas = 0;
        int iTotRealizadasFueraPlazo = 0;
        int iTotTareasFueraPlazoSinHacer = 0;
        string sFRealizacion = "";
        try
        {
            if (HttpContext.Current.Cache["Incumplimientos_Propios_" + Session["IDFICEPI_CVT_ACTUAL"].ToString()] != null)
                HttpContext.Current.Cache.Remove("Incumplimientos_Propios_" + Session["IDFICEPI_CVT_ACTUAL"].ToString());

            DataSet ds = Incumplimientos.Propios(null, (int)Session["IDFICEPI_CVT_ACTUAL"], dDesde, dHasta);

            HttpContext.Current.Cache.Insert("Incumplimientos_Propios_" + Session["IDFICEPI_CVT_ACTUAL"].ToString(), ds, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);

            sb.Append("<table id='resumenDet' style='background-image:url("+Session["strServer"]+"Images/imgFT16.gif);text-align:right;width:740px;cursor:pointer'>");
            sb.Append("<colgroup><col width='440px'/><col width='100px'/><col width='100px'/><col width='100px'/></colgroup>");
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if ((int)oFila["Realizadas"] == 0 && (int)oFila["RealizadasFueraPlazo"] == 0 && (int)oFila["TareasFueraPlazoSinHacer"] == 0) continue;

                sb.Append("<tr id=" + oFila["grupo"].ToString());
                sb.Append(" style='height:16px' onclick='ms(this);cargarTareas(this.id);'>");
                sb.Append("<td style='padding-left:5px;text-align:left'>" + oFila["Tarea"].ToString() + "</td>");
                sb.Append("<td style='padding-right:4px;'>" + int.Parse(oFila["Realizadas"].ToString()).ToString("###,###,##0") + "</td>");
                iTotRealizadas = iTotRealizadas + (int)oFila["Realizadas"];
                sb.Append("<td style='padding-right:4px;'>" + int.Parse(oFila["RealizadasFueraPlazo"].ToString()).ToString("###,###,##0") + "</td>");
                iTotRealizadasFueraPlazo = iTotRealizadasFueraPlazo + (int)oFila["RealizadasFueraPlazo"];
                sb.Append("<td style='text-align:right;padding-right:5px;'>" + int.Parse(oFila["TareasFueraPlazoSinHacer"].ToString()).ToString("###,###,##0") + "</td>");
                iTotTareasFueraPlazoSinHacer = iTotTareasFueraPlazoSinHacer + (int)oFila["TareasFueraPlazoSinHacer"];
                sb.Append("</tr>");
            }

            sb.Append("</table>@#@");
            sb.Append("<table id='Totales' style='text-align:right;width:740px' border='0'>");
            sb.Append("<colgroup><col width='440px'/><col width='100px'/><col width='100px'/><col width='100px'/></colgroup>");
            sb.Append("<tr class='TBLFIN'>");
            sb.Append("<td style='padding-left:5px;text-align:left'>TOTAL</td>");
            sb.Append("<td style='padding-right:4px'>" + iTotRealizadas.ToString("###,###,##0") + "</td>");
            sb.Append("<td style='padding-right:4px'>" + iTotRealizadasFueraPlazo.ToString("###,###,##0") + "</td>");
            sb.Append("<td style='padding-right:4px'>" + iTotTareasFueraPlazoSinHacer.ToString("###,###,##0") + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>@#@");
            sb.Append("<table id='detalleCab1' style='text-align:left;width: 930px' border='0'>");
            sb.Append("<colgroup><col width='350px'/><col width='380px'/><col width='100px'/><col width='100px'/></colgroup>");
      
            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                sb.Append("<tr id=" + oFila["grupo"].ToString());
                sb.Append(" style='height:16px'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W340' onmouseover='TTip(event)'>" + oFila["Apartado"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W370' onmouseover='TTip(event)'>" + oFila["Registro"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'>" + ((DateTime)oFila["flimite"]).ToShortDateString() + "</td>");
                sFRealizacion = (oFila["frealizacion"].ToString() != "") ? ((DateTime)oFila["frealizacion"]).ToShortDateString() : "";
                sb.Append("<td style='text-align:center'>" + sFRealizacion + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>@#@");
            sb.Append("<table id='detalleCab2' style='text-align:left;width: 930px' border='0'>");
            sb.Append("<colgroup><col width='250px'/><col width='200px'/><col width='280px'/><col width='100px'/><col width='100px'/></colgroup>");

            foreach (DataRow oFila in ds.Tables[2].Rows)
            {
                sb.Append("<tr id=" + oFila["grupo"].ToString());
                sb.Append(" style='height:16px'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W240' onmouseover='TTip(event)'>" + oFila["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190' onmouseover='TTip(event)'>" + oFila["Apartado"].ToString() + "</nobr></td>");
                //sb.Append("<td><nobr class='NBR W270' onmouseover='TTip(event)'>" + oFila["Registro"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W270' onmouseover='TTip(event)'>" + oFila["Registro"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'>" + ((DateTime)oFila["flimite"]).ToShortDateString() + "</td>");
                sFRealizacion = (oFila["frealizacion"].ToString() != "") ? ((DateTime)oFila["frealizacion"]).ToShortDateString() : "";
                sb.Append("<td style='text-align:center'>" + sFRealizacion + "</td>"); sb.Append("</tr>");
            }
            sb.Append("</table>@#@");
            sb.Append("<table id='detalleCab3' style='text-align:left;width: 930px' border='0'>");
            sb.Append("<colgroup><col width='350px'/><col width='375px'/><col width='100px'/><col width='100px'/></colgroup>");

            foreach (DataRow oFila in ds.Tables[3].Rows)
            {
                sb.Append("<tr id=" + oFila["grupo"].ToString());
                sb.Append(" style='height:16px'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W340' onmouseover='TTip(event)'>" + int.Parse(oFila["idProyecto"].ToString()).ToString("###,###") + " - " + oFila["Proyecto"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W365' onmouseover='TTip(event)'>" + oFila["Cliente"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'>" + ((DateTime)oFila["flimite"]).ToShortDateString() + "</td>");
                sFRealizacion = (oFila["frealizacion"].ToString() != "") ? ((DateTime)oFila["frealizacion"]).ToShortDateString() : "";
                sb.Append("<td style='text-align:center'>" + sFRealizacion + "</td>"); sb.Append("</tr>");
            }
            sb.Append("</table>@#@");
            sb.Append("<table id='detalleCab1' style='text-align:left;width: 930px' border='0'>");
            sb.Append("<colgroup><col width='350px'/><col width='380px'/><col width='100px'/><col width='100px'/></colgroup>");

            foreach (DataRow oFila in ds.Tables[4].Rows)
            {
                sb.Append("<tr id=" + oFila["grupo"].ToString());
                sb.Append(" style='height:16px'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W340' onmouseover='TTip(event)'>" + oFila["Apartado"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W370' onmouseover='TTip(event)'>" + oFila["Registro"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'>" + ((DateTime)oFila["flimite"]).ToShortDateString() + "</td>");
                sFRealizacion = (oFila["frealizacion"].ToString() != "") ? ((DateTime)oFila["frealizacion"]).ToShortDateString() : "";
                sb.Append("<td style='text-align:center'>" + sFRealizacion + "</td>"); sb.Append("</tr>");
            }

            sb.Append("</table>@#@");

            ds.Dispose();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de incumplimientos propios.", ex);
        }
    }
}
