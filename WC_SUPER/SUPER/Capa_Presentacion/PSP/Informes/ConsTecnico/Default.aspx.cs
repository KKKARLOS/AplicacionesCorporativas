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



public partial class Capa_Presentacion_psp_informes_ConsTecnico_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected ArrayList aProyecto;
    public string strTablaHtml;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Informe de consumos por profesional (agregados/desglosados)";
        Master.bFuncionesLocales = true;
        Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
        Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            //Master.nBotonera = 22;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
            try
            {
                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                //Obtener los datos necesarios
                DateTime dDesde = DateTime.Parse("01/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                txtFechaInicio.Text = dDesde.ToShortDateString();
                txtFechaFin.Text = dDesde.AddMonths(1).AddDays(-1).ToShortDateString();

                hdnEmpleado.Text = Session["UsuarioActual"].ToString();

                cboConcepto.Items.Add(new ListItem("", "0"));
                cboConcepto.Items.Add(new ListItem("Estructura", "1"));
                cboConcepto.Items.Add(new ListItem("Grupo funcional", "2"));
                cboConcepto.Items.Add(new ListItem("Profesional", "3"));

            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("excel"):
                sResultado += ObtenerConsumos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }
    protected string ObtenerConsumos(string sConcepto, String sNivel, string sListaCodigos, string sDesde, string sHasta, string sInt, string sExt)
    {
        string sResul = ""; 
        StringBuilder sb = new StringBuilder();
        StringBuilder sbPSN = new StringBuilder();
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
                DataSet ds = Consumo.ObtenerInformeConsProfAEDS(short.Parse(sConcepto), sListaCodigos, int.Parse(Session["UsuarioActual"].ToString()),
                                                                sDesde, sHasta, sInt, sExt,sNivel);


                sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellpadding='0' cellSpacing='0' border='1'>");
                sb.Append("<tr>");
                sb.Append("<td style='background-color: #BCD4DF;'>Nº Proyecto</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>Denominación Proyecto</td>");
                //sb.Append("<td style='background-color: #BCD4DF;'>Nº Proy. Técnico</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>Denominación Proy. Técnico</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>Nº Tarea</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>Denominación Tarea</td>");

                sb.Append("<td style='background-color: #BCD4DF;'>OTC</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>OTL</td>");

                sb.Append("<td style='background-color: #BCD4DF;'>Nº Usuario</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>Profesional</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>Horas</td>");
                sb.Append("<td style='background-color: #BCD4DF;'>Jornadas</td>");

                foreach (DataRow oFila in ds.Tables[1].Rows)
                {
                    sb.Append("<td style='background-color: #BCD4DF;'>" + oFila["denominacion"] + "</td>");
                }
                sb.Append("<td style='background-color: #BCD4DF;'>Observaciones</td>");
                sb.Append("</tr>");

                int i = 0;
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + oFila.ItemArray[0].ToString() + "</td>");//Nº Proyecto
                    sb.Append("<td>" + oFila.ItemArray[1].ToString() + "</td>");//Denominación Proyecto
                    //sb.Append("<td>" + oFila.ItemArray[2].ToString() + "</td>");//Nº Proy. Técnico
                    sb.Append("<td>" + oFila.ItemArray[2].ToString() + "</td>");//Denominación Proy. Técnico
                    sb.Append("<td>" + oFila.ItemArray[3].ToString() + "</td>");//Nº Tarea
                    sb.Append("<td>" + oFila.ItemArray[4].ToString() + "</td>");//Denominación Tarea

                    sb.Append("<td>" + oFila.ItemArray[11].ToString() + "</td>");//OTC
                    sb.Append("<td>" + oFila.ItemArray[12].ToString() + "</td>");//OTL


                    sb.Append("<td>" + oFila.ItemArray[5].ToString() + "</td>");//Nº Usuario
                    sb.Append("<td>" + oFila.ItemArray[6].ToString() + "</td>");//Profesional
                    sb.Append("<td>" + oFila.ItemArray[9].ToString() + "</td>");//Horas
                    sb.Append("<td>" + oFila.ItemArray[10].ToString() + "</td>");//Jornadas

                    i = 14;
                    while (i < oFila.ItemArray.Length)
                    {
                        sb.Append("<td>" + oFila.ItemArray[i].ToString() + "</td>");
                        i++;
                    }
                    sb.Append("<td>" + oFila.ItemArray[13].ToString() + "</td>");//Observaciones
                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                ds.Dispose();

                //sResul = "OK@#@" + sb.ToString();
                string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
                Session[sIdCache] = sb.ToString(); ;

                sResul = "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); ;

            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los consumos.", ex);
        }

        return sResul;
    }
}