using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class Capa_Presentacion_ECO_ValorGanado_CreacionLB_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", strTablaHTMLLineas = "";
    public int nPSN = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (Session["ID_PROYECTOSUBNODO"] != null)
                {
                    strTablaHTMLLineas = LINEABASE.ObtenerCatalogoByPSNCreacionLB(int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()));
                    ObtenerAnalisisConsistencia();
                    ObtenerDesgloseGrafico();
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
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("crearLB"):
                try
                {
                    sResultado += "OK@#@" + LINEABASE.CrearLineaBase(int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()), int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), aArgs[1], int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al crear la línea base.", ex);
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

    private void ObtenerAnalisisConsistencia()
    {
        int nCountR = 0;
        int nCountA = 0;
        CONSISTENCIALB oCLB = CONSISTENCIALB.Obtener(int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()));
        cldJornadasPST.InnerText = oCLB.jornadas_PST.ToString("N");
        cldJornadasPST.Attributes.Add("jornadas", oCLB.jornadas_PST.ToString());
        lblJornadasPGE.InnerText = oCLB.jornadas_PGE.ToString("N");
        lblJornadasPGE.Attributes.Add("jornadas", oCLB.jornadas_PGE.ToString());
        lblJornadasESTPGE.InnerText = oCLB.jornadas_ESTPGE.ToString("N");
        lblJornadasESTPGE.Attributes.Add("jornadas", oCLB.jornadas_ESTPGE.ToString());
        lblJornadasPGESJI.InnerText = oCLB.jornadas_PGESJI.ToString("N");
        lblJornadasPGESJI.Attributes.Add("jornadas", oCLB.jornadas_PGESJI.ToString());
        cldJornadasIndi.InnerText = oCLB.Indicador_Jornadas.ToString("N") + "%";

        if (Math.Abs(100 - Math.Abs(oCLB.Indicador_Jornadas)) <= 5) imgSemJornadas.Src = "../../../../Images/imgSemaforoVF.gif";
        else if (Math.Abs(100 - Math.Abs(oCLB.Indicador_Jornadas)) <= 10)
        {
            imgSemJornadas.Src = "../../../../Images/imgSemaforoA.gif";
            nCountA++;
        }
        else
        {
            imgSemJornadas.Src = "../../../../Images/imgSemaforoR.gif";
            nCountR++;
        }
        cldTareasPST.InnerText = oCLB.num_tareas_prevision.ToString() + " / " + oCLB.num_tareas.ToString();
        cldTareasIndi.InnerText = oCLB.Indicador_Tareas.ToString("N") + "%";
        if (Math.Abs(oCLB.Indicador_Tareas) >= 90) imgSemTareas.Src = "../../../../Images/imgSemaforoVF.gif";
        else if (Math.Abs(oCLB.Indicador_Tareas) >= 80)
        {
            imgSemTareas.Src = "../../../../Images/imgSemaforoA.gif";
            nCountA++;
        }
        else
        {
            imgSemTareas.Src = "../../../../Images/imgSemaforoR.gif";
            nCountR++;
        }
        if (nCountR > 0 || nCountA > 1)
        {
            lblRecomendacion.InnerText = "Corregir datos antes de crear la línea base.";
            lblRecomendacion.Style.Add("color", "red");
        }
        else if (nCountA > 0)
        {
            lblRecomendacion.InnerText = "Revisar datos antes de crear la línea base.";
            lblRecomendacion.Style.Add("color", "red");
        }
        else
            lblRecomendacion.InnerText = "Datos consistentes. Se puede crear la línea base.";
    }
    private void ObtenerDesgloseGrafico()
    {
        DataSet ds = SUPER.Capa_Datos.LINEABASE.ObtenerDatosCreacionLineaBAse(null, int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());
        
        #region Desglose de consumos
        lblImportes.InnerText = MONEDA.getDenominacionImportes((Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());

        DataRow oFilaDesglose = ds.Tables[0].Rows[0];
        txtConsumoIAP.Text = decimal.Parse(oFilaDesglose["consumo_iap"].ToString()).ToString("N");
        txtConsumoOCO.Text = decimal.Parse(oFilaDesglose["consumo_oco"].ToString()).ToString("N");
        txtSubcontratacion.Text = decimal.Parse(oFilaDesglose["consumo_subcontratacion"].ToString()).ToString("N");
        txtCompras.Text = decimal.Parse(oFilaDesglose["consumo_compras"].ToString()).ToString("N");
        txtTotal.Text = decimal.Parse(oFilaDesglose["consumo_total"].ToString()).ToString("N");
        #endregion

        #region Grafico

        #region Origen datos
        DataView dv;
        int nMesesMax = 17;
        int nDivisor = 1;

        if (ds.Tables[1].Rows.Count > nMesesMax)
        {
            DataTable dt = ds.Tables[1].Clone();
            //int nFilas = dt.Rows.Count;
            //for (int nFila = nFilas-1; nFila >= 0; nFila--)
            //    dt.Rows.RemoveAt(nFila);

            int nFilas = ds.Tables[1].Rows.Count;
            int nIndice = 0;

            while (nFilas / nDivisor > nMesesMax)
            {
                nDivisor++;
            }
            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                if (nIndice == 0 || nIndice == nFilas - 1)
                {
                    dt.ImportRow(oFila);
                }
                else
                {
                    if (nIndice % nDivisor == 0)
                        dt.ImportRow(oFila);
                }
                nIndice++;

            }

            dv = dt.DefaultView;
        }
        else
            dv = ds.Tables[1].DefaultView;
        #endregion

        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;

        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.TruncatedLabels = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;

        // Creo una serie y sus atributos visuales para el grafico de TOTAL
        Series serieBAC = new Series("Total del proyecto - BAC");
        //Añado desde BBDD los puntos que conforman la serie
        serieBAC.Points.DataBind(dv, "mes", "BAC", "");
        serieBAC.Color = Color.Brown;
        //Añado la serie al gráfico
        Chart1.Series.Add(serieBAC);

        Series seriePV = new Series("Acumulado planificado - PV");
        seriePV.Points.DataBind(dv, "mes", "PV", "");
        seriePV.Color = Color.Cyan;
        Chart1.Series.Add(seriePV);

        for (int i = 0; i < Chart1.Series.Count; i++)
        {
            if (dv.Count == 1)
            {
                Chart1.Series[i].ChartType = SeriesChartType.Column;
                Chart1.Series[i]["PointWidth"] = "0.2";
                Chart1.Series[i]["DrawingStyle"] = "Default";
            }
            else
            {
                Chart1.Series[i].ChartType = SeriesChartType.Line;
                Chart1.Series[i].BorderWidth = 2;
                Chart1.Series[i].ShadowOffset = 1;
            }
            Chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
            Chart1.Series[i].MarkerColor = Color.Navy;
            Chart1.Series[i].MarkerSize = 4;
            Chart1.Series[i].ToolTip = "#VALY{N0}";
        }

        #region Labels Personalizados
        Chart1.Legends["Default"].Title = "COSTES";
        Chart1.Legends["Default"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
        Chart1.Legends["Default"].TitleSeparatorColor = Color.Black;

        // Disable legend item for the first series
        //Chart1.Series[0].IsVisibleInLegend = false;

        //// Add custom legend item with line style
        //LegendItem legendItem = new LegendItem();
        //legendItem.Name = "BACBAC";
        //legendItem.ToolTip = "ToolTip de BACBAC";
        //legendItem.ImageStyle = LegendImageStyle.Line;
        //legendItem.ShadowOffset = 2;
        //legendItem.Color = Color.LightBlue;
        //legendItem.MarkerStyle = MarkerStyle.Circle;
        //Chart1.Legends["Default"].CustomItems.Count; .CustomItems.Add(legendItem);
        #endregion
        #endregion
    }


}
