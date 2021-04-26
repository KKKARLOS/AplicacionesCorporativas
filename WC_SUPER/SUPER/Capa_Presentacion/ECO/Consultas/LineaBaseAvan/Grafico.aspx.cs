using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class Capa_Presentacion_ECO_Consultas_LineaBase_Grafico : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                string sAux = "";
                if (Request.QueryString["lp"] != null)
                {
                    sAux = Request.QueryString["lp"].ToString();
                    //this.hdnListaPE.Value = sAux;
                    Session["VALORGANADO_LISTA_PE"] = sAux;
                }
                //else
                //    Session["VALORGANADO_LISTA_PE"] = null;
                int i = 0;
                double dCSI = 0;
                List<SUPER.DAL.ProyectosValorGanado> lstProy = JsonConvert.DeserializeObject<List<SUPER.DAL.ProyectosValorGanado>>(sAux);
                ChartSPI2.Series["Proyecto"].Font = new Font("Arial", 8.25F);
                // Enable data points labels
                //ChartSPI2.Series["Proyecto"].IsValueShownAsLabel = true;
                ChartSPI2.Series["Proyecto"]["LabelStyle"] = "Top";
                ChartSPI2.Series["Proyecto"].MarkerStyle = MarkerStyle.Circle;
                ChartSPI2.Series["Proyecto"].MarkerSize = 3;

                //Para evitar que se monten los puntos
                ChartSPI2.Series["Proyecto"].SmartLabelStyle.Enabled = true;
                ChartSPI2.Series["Proyecto"].SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Partial;
                //ChartSPI2.Series["Proyecto"].SmartLabelStyle.CalloutLineAnchorCap = LineAnchorCapStyle.Diamond;
                ChartSPI2.Series["Proyecto"].SmartLabelStyle.CalloutLineColor = Color.Red;
                ChartSPI2.Series["Proyecto"].SmartLabelStyle.CalloutLineWidth = 2;
                ChartSPI2.Series["Proyecto"].SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box;


                foreach (SUPER.DAL.ProyectosValorGanado oProy in lstProy)
                {
                    dCSI = Math.Round(oProy.cpi * oProy.spi, 2);
                    //ChartSPI2.Series["Proyecto"].Points.InsertXY(0, oProy.cpi, oProy.spi);
                    ChartSPI2.Series["Proyecto"].Points.InsertXY(i, oProy.cpi, oProy.spi);
                    //ChartSPI2.Series["Proyecto"].Points[i].Label = oProy.idPe + " (" + oProy.idPsn + ")";
                    ChartSPI2.Series["Proyecto"].Points[i].Label = oProy.idPe;
                    //ChartSPI2.Series["Proyecto"].Points[i].SetCustomProperty("PSN", oProy.idPsn.ToString());

                    if (dCSI < 0.9) ChartSPI2.Series["Proyecto"].Points[i].Color = Color.Red;
                    else if (dCSI < 0.95) ChartSPI2.Series["Proyecto"].Points[i].Color = Color.Yellow;
                    else ChartSPI2.Series["Proyecto"].Points[i].Color = Color.Green;

                    ChartSPI2.Series["Proyecto"].Points[i].ToolTip = "Detalle proyecto " + oProy.idPe;
                    ChartSPI2.Series["Proyecto"].Points[i].LabelToolTip = "Detalle proyecto " + oProy.idPe;
                    i++;
                }
                foreach (Series series in ChartSPI2.Series)
                {
                    //series.MapAreaAttributes = "onclick=\"javascript:alert('Serie: #SER, Point Index: #INDEX, Valor: #LABEL');\"";
                    //series.LegendMapAreaAttributes = "onclick=\"javascript:alert('Mouse click event captured in the legend! Series: #SER, Total Values: #TOTAL{C}');\"";
                    series.MapAreaAttributes = "ondblclick=\"javascript:mostrar('#LABEL');\" href=\"#\" style=\"cursor:pointer;\"";//Click en el punto
                    series.LabelMapAreaAttributes = "ondblclick=\"javascript:mostrar('#LABEL');\" href=\"#\" style=\"cursor:pointer;\"";//Click en la label del punto (texto con el nº de proyecto)
                }
            }
        }
        catch (Exception ex)
        {
            //Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }
}