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
using System.Text;
using System.Text.RegularExpressions;
//Para los gráficos
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;


public partial class Capa_Presentacion_Archivo_Conexiones_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLGrafico = "", strTablaHTMLPropias = "", strTablaHTMLAjenas = "";
    //public string strXML = "";
    public int nPropias = 0, nAjenas = 0;

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        InitializeComponent();
        base.OnInit(e);
    }
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Chart1.Customize += new EventHandler(this.Chart1_Customize);
    }
    protected void Chart1_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
    {
        // Loop through all default legend items
        try
        {
            if (e.LegendName == "Default")
            {
                //e.LegendItems[7].ToolTip = "Bonos de transporte";
                //e.LegendItems[8].ToolTip = "Pagos concertados";
            }
        }
        catch { }
    }
    private void Chart1_Customize(Object sender, EventArgs args)
    {
        // Get X and Y axis labels collections
        CustomLabelsCollection xAxisLabels = Chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels;
        // Change Y axis labels
        for (int labelIndex = 0; labelIndex < xAxisLabels.Count; labelIndex++)
        {
            //xAxisLabels[labelIndex].Text = AnnomesAFechaDescCorta(xAxisLabels[labelIndex].Text);
        }

    }
    //protected void Chart1_Cursor(object sender, ChartPaintEventArgs e)
    //{
    //    if (e.ChartElement is Series)
    //    {
    //        Series series = (Series)e.ChartElement;
    //        System.Drawing.PointF position = System.Drawing.PointF.Empty;
    //        series.MapAreaAttributes = "onmouseover=\"this.style.cursor='pointer'\" onmouseout=\"this.style.cursor='default'\"";
    //    }
    //}
    #endregion
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.FuncionesJavaScript.Add("Javascript/FusionCharts.js");
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Registro de actividad";

                try
                {
                    string sAnoMes = Fechas.FechaAAnnomes(DateTime.Now).ToString();
                    //obtenerDatosXML(int.Parse(sAnoMes));
                    obtenerDatos(int.Parse(sAnoMes));
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
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sAnoMes="";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getmes"):
                sAnoMes = FechaDescCortaAnnomes(aArgs[1]);
                DateTime dtPri = Fechas.crearDateTime("01/" + sAnoMes.Substring(4, 2) + "/" + sAnoMes.Substring(0, 4));
                DateTime dtUlt = Fechas.getSigDiaUltMesCerrado(int.Parse(sAnoMes));
                sResultado += obtenerDatosPropios(dtPri, dtUlt) + obtenerDatosAjenos(dtPri, dtUlt);
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
    private string obtenerDatosPropios(DateTime dtPrimer, DateTime dtUltimo)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "", sFecAux;
        int iNumConex = 0;
        try
        {
            sb.Append("<table id='tblPropios' style='width: 160px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:100px;' />");//Fecha conexión
            sb.Append("<col style='width:60px;' />");//Usuario
            sb.Append("</colgroup>");
            //SqlDataReader dr = CONEXIONES.SelectPropias(null, int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
            SqlDataReader dr = CONEXIONES.SelectPropias(null, int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), dtPrimer, dtUltimo);

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;'>");
                sFecAux = dr["t459_fecconect"].ToString().Substring(0, 16);
                if (sFecAux.Substring(15, 1) == ":")
                    sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);
                sb.Append("<td  style='padding-left:2px;'>" + sFecAux + "</td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t314_idusuario_conect"].ToString()).ToString("#,###") + "</td></tr>");
                //sb.Append("<td><nobr class='NBR W260'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
                iNumConex++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            strTablaHTMLPropias = sb.ToString();
            sResul = "OK@#@" + iNumConex + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener el registro de actividad propio.", ex);
        }
        return sResul;
    }
    private string obtenerDatosAjenos(DateTime dtPrimer, DateTime dtUltimo)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "", sFecAux;
        int iNumConex = 0;
        try
        {
            sb.Append("<table id='tblAjenos' style='width: 695px;' >");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:100px;' />");//Fecha de conexión
            sb.Append("<col style='width:595px;' />");//Profesional
            sb.Append("</colgroup>");
            SqlDataReader dr = CONEXIONES.SelectAjenas(null, (int)Session["UsuarioActual"], dtPrimer, dtUltimo);

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;'>");
                sFecAux = dr["t459_fecconect"].ToString().Substring(0, 16);
                if (sFecAux.Substring(15, 1) == ":")
                    sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);

                sb.Append("<td style='padding-left:2px;'>" + sFecAux + "</td>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td></tr>");
                iNumConex++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            strTablaHTMLAjenas = sb.ToString();
            sResul = "@#@" + iNumConex + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener el registro de actividad ajeno.", ex);
        }
        return sResul;
    }
    private string obtenerDatos(int iAnoMes)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            string sAnoMesIni, sAnoMesFin; 
            int iAnoMesIni = Fechas.AddAnnomes(iAnoMes, -12), iAnoMesFin;

            sAnoMesIni = iAnoMesIni.ToString();
            this.hdnMesAct.Value = Fechas.AnnomesAFechaDescCorta(iAnoMes);

            #region grafico 1
            DateTime dtPrimer = Fechas.crearDateTime("01/" + sAnoMesIni.Substring(4, 2) + "/" + sAnoMesIni.Substring(0, 4));
            iAnoMesFin = Fechas.AddAnnomes(iAnoMes, 1);
            sAnoMesFin = iAnoMesFin.ToString();
            DateTime dtUltimo = Fechas.crearDateTime("01/" + sAnoMesFin.Substring(4, 2) + "/" + sAnoMesFin.Substring(0, 4));

            SqlDataReader dr = CONEXIONES.SelectGraficoMes(null, int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), 
                                                            (int)Session["UsuarioActual"], dtPrimer, dtUltimo);

            DataTable table = new DataTable();
            table.Load(dr);
            DataView dv = table.DefaultView;
            dr.Close();
            dr.Dispose();

            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;
            //Creo las series para el gráfico con cada uno de los conceptos
            //Creo una serie para las conexiones propias
            Series seriePropias = new Series("Conexiones propias");
            //seriePropias.Points.DataBind(dv, "anomes", "npropias", "");
            seriePropias.Points.DataBind(dv, "mes", "npropias", "");
            Chart1.Series.Add(seriePropias);

            //Creo una serie para las conexiones en mi nombre
            Series serieAjenas = new Series("Conexiones en su nombre");
            //serieAjenas.Points.DataBind(dv2, "anomes", "najenas", "");
            serieAjenas.Points.DataBind(dv, "mes", "najenas", "");
            Chart1.Series.Add(serieAjenas);

            for (int i = 0; i < Chart1.Series.Count; i++)
            {
                Chart1.Series[i].ChartType = SeriesChartType.Bar; 
                Chart1.Series[i]["PointWidth"] = "1.0";
                Chart1.Series[i]["DrawingStyle"] = "Default";
                //if (dv.Count == 1)
                //{
                //    Chart1.Series[i].ChartType = SeriesChartType.Column;
                //    Chart1.Series[i]["PointWidth"] = "0.2";
                //    Chart1.Series[i]["DrawingStyle"] = "Default";
                //}
                //else
                //{
                //    Chart1.Series[i].ChartType = SeriesChartType.Spline;
                //    Chart1.Series[i].BorderWidth = 2;
                //    Chart1.Series[i].ShadowOffset = 1;
                //}
                //Chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
                //Chart1.Series[i].MarkerColor = Color.Navy;
                //Chart1.Series[i].MarkerSize = 6;
                Chart1.Series[i].ToolTip = "#VALY{N0}";
                //Asigno evento click
                //Chart1.Series[i].MapAreaAttributes = "onclick=\"javascript:alert('Event captured! Series Name: #SER, Point Index: #INDEX, Valor: #VALX');\"";
                //Chart1.Series[i].MapAreaAttributes = "onclick=\"javascript:getMes('#VALX');\" onmouseover=\"document.body.style.cursor='pointer'\" onmouseout=\"document.body.style.cursor='default'\"";
                Chart1.Series[i].MapAreaAttributes = "onclick=\"javascript:getMes('#VALX');\" onmouseover=\"ponerMano();\" onmouseout=\"quitarMano();\"";
            }

            Chart1.Visible = true;

            table.Dispose();
            dv.Dispose();
            #endregion

            return "OK@#@";// +sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
        }
    }
    private string AnnomesAFechaDescCorta(string nAnnoMes)
    {
        string[] aMeses = new string[12] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
        int iMes = (int.Parse(nAnnoMes.ToString().Substring(4, 2))) % 12;
        if (iMes == 0) iMes = 12;
        return aMeses[iMes - 1];
    }
    private string FechaDescCortaAnnomes(string sFechaCorta)
    {
        string sRes = "";
        int iMes = 0;
        string sMesCorto = sFechaCorta.Substring(0, 3);
        switch (sMesCorto.ToUpper())
        {
            case "ENE":
                iMes = 1;
                break;
            case "FEB":
                iMes = 2;
                break;
            case "MAR":
                iMes = 3;
                break;
            case "ABR":
                iMes = 4;
                break;
            case "MAY":
                iMes = 5;
                break;
            case "JUN":
                iMes = 6;
                break;
            case "JUL":
                iMes = 7;
                break;
            case "AGO":
                iMes = 8;
                break;
            case "SEP":
                iMes = 9;
                break;
            case "OCT":
                iMes = 10;
                break;
            case "NOV":
                iMes = 11;
                break;
            case "DIC":
                iMes = 12;
                break;
        }
        int iAnio = int.Parse(sFechaCorta.Substring(4, 4));
        sRes = ((iAnio * 100) + iMes).ToString();
        return sRes;
    }

}
