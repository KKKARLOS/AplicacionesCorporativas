using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections;
using System.Data;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de Grafico
    /// </summary>
    public class Grafico
    {
        //private Chart m_chart;

        public Grafico()
        {
            //m_chart = new Chart();
        }

        public static Chart GenerarGraficoAnalisis()
        {
            Chart m_chart = new Chart();

            #region Chart setting 
            m_chart.Palette = ChartColorPalette.None;
            m_chart.Width = Unit.Pixel(500);
            m_chart.Height = Unit.Pixel(360);
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BackGradientStyle = GradientStyle.TopBottom;
            m_chart.BorderlineColor = ColorTranslator.FromHtml("#1A3B69");// Color.FromArgb(181, 64, 1);
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BorderlineWidth = 2;
            m_chart.BackColor = ColorTranslator.FromHtml("#D6D6D6");
            m_chart.SuppressExceptions = true;
            m_chart.BackImageTransparentColor = ColorTranslator.FromHtml("#DDE6E9");
            m_chart.ImageType = ChartImageType.Png;
            m_chart.IsSoftShadows = false;
            #endregion
            #region Título
            Title oTitulo = new Title();
            oTitulo.ShadowColor = Color.FromArgb(32,0,0,0);
            oTitulo.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            oTitulo.ShadowOffset = 3;
            oTitulo.Text = "Gráfico de análisis del Valor Ganado";
            oTitulo.ForeColor = Color.FromArgb(26, 59, 105);
            m_chart.Titles.Add(oTitulo);
            #endregion
            #region Legend
            Legend oLegend = new Legend("Default");
            oLegend.LegendStyle = LegendStyle.Table;
            oLegend.IsTextAutoFit = false;
            oLegend.Docking = Docking.Bottom;
            oLegend.BackColor = Color.Transparent;
            oLegend.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLegend.Alignment = StringAlignment.Center;
            m_chart.Legends.Add(oLegend);
            #endregion
            #region BorderSkin
            BorderSkin oBS = new BorderSkin();
            oBS.SkinStyle = BorderSkinStyle.Emboss;
            oBS.BackImageTransparentColor = ColorTranslator.FromHtml("#DDE6E9");
            oBS.BorderWidth = 0;
            oBS.BorderColor = ColorTranslator.FromHtml("#DDE6E9");
            oBS.PageColor = ColorTranslator.FromHtml("#DDE6E9");
            m_chart.BorderSkin = oBS;
            #endregion
            #region ChartArea
            ChartArea oCA = new ChartArea();
            oCA.Name = "ChartArea1";
            oCA.BorderColor = Color.FromArgb(64, 64, 64, 64);
            oCA.BackSecondaryColor = Color.White;
            oCA.BackColor = Color.OldLace;
            oCA.ShadowColor = Color.Transparent;
            oCA.BackGradientStyle = GradientStyle.TopBottom;
            //area3dstyle
            oCA.Area3DStyle.Rotation = 10;
            oCA.Area3DStyle.Perspective = 10;
            oCA.Area3DStyle.Inclination = 15;
            oCA.Area3DStyle.IsRightAngleAxes = false;
            oCA.Area3DStyle.WallWidth = 0;
            oCA.Area3DStyle.IsClustered = false;
            //axisy
            oCA.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            oCA.AxisY.LabelAutoFitMaxFontSize = 8;
            //oCA.AxisY.LabelStyle 
            LabelStyle oLS = new LabelStyle();
            oLS.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLS.Format = "N0";
            oCA.AxisY.LabelStyle = oLS;
            //MajorGrid
            oCA.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            //axisx
            oCA.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            oCA.AxisX.LabelAutoFitMaxFontSize = 8;
            //oCA.AxisY.LabelStyle 
            LabelStyle oLSX = new LabelStyle();
            oLSX.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLSX.IsEndLabelVisible = false;
            oLSX.Interval = 1.0;
            //oLSX.IntervalType = 
            //oLSX.IntervalType = DateTimeIntervalType.Auto;
            oLSX.IntervalOffset = 0.0;
            oLSX.IntervalOffsetType = DateTimeIntervalType.NotSet;
            //oLSX.IntervalOffset = 0;
            oLSX.TruncatedLabels = true;
            oCA.AxisX.LabelStyle = oLSX;
            //MajorGrid
            oCA.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

            m_chart.ChartAreas.Add(oCA);
            #endregion

        //<asp:CHART id="Chart1" oncustomizelegend="Chart1_CustomizeLegend" runat="server" Palette="None" Width="500px" Height="380px" BorderDashStyle="Solid" 
        //    BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" 
        //    SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" 
        //    ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
        //    <titles>
        //        <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14.25pt, style=Bold" ShadowOffset="3" Text="Gráfico de análisis del Valor Ganado" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
        //    </titles>
        //    <legends>
        //        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
        //    </legends>
        //    <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
        //    <chartareas>
        //        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
        //            <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
        //            <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
        //                <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N0" />
        //                <MajorGrid LineColor="64, 64, 64, 64" />
        //            </axisy>
        //            <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
        //                <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
        //                <MajorGrid LineColor="64, 64, 64, 64" />
        //            </axisx>
        //        </asp:ChartArea>
        //    </chartareas>
        //</asp:CHART>

            return m_chart;
        }

        public static Chart GenerarGraficoCPISPI()
        {
            Chart m_chart = new Chart();

            #region Chart setting 
            m_chart.Palette = ChartColorPalette.None;
            m_chart.Width = Unit.Pixel(450);
            m_chart.Height = Unit.Pixel(400);
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BackGradientStyle = GradientStyle.TopBottom;
            m_chart.BorderlineColor = ColorTranslator.FromHtml("#1A3B69");// Color.FromArgb(181, 64, 1);
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BorderlineWidth = 2;
            m_chart.BackColor = ColorTranslator.FromHtml("#D6D6D6");
            m_chart.SuppressExceptions = true;
            m_chart.BackImageTransparentColor = ColorTranslator.FromHtml("#DDE6E9");
            m_chart.ImageType = ChartImageType.Png;
            m_chart.IsSoftShadows = false;
            #endregion
            #region Título
            Title oTitulo = new Title();
            oTitulo.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            oTitulo.Font = new Font("Arial", 14F, FontStyle.Bold);
            oTitulo.ShadowOffset = 3;
            oTitulo.Text = "Evolución de CPI/SPI";
            oTitulo.ForeColor = Color.FromArgb(26, 59, 105);
            m_chart.Titles.Add(oTitulo);
            #endregion
            #region Legend
            Legend oLegend = new Legend("Default");
            oLegend.LegendStyle = LegendStyle.Table;
            oLegend.IsTextAutoFit = false;
            oLegend.Docking = Docking.Bottom;
            oLegend.BackColor = Color.Transparent;
            oLegend.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLegend.Alignment = StringAlignment.Center;
            m_chart.Legends.Add(oLegend);
            #endregion
            #region BorderSkin
            BorderSkin oBS = new BorderSkin();
            oBS.SkinStyle = BorderSkinStyle.Emboss;
            oBS.BackImageTransparentColor = ColorTranslator.FromHtml("#DDE6E9");
            oBS.BorderWidth = 0;
            oBS.BorderColor = ColorTranslator.FromHtml("#DDE6E9");
            oBS.PageColor = ColorTranslator.FromHtml("#DDE6E9");
            m_chart.BorderSkin = oBS;
            #endregion
            #region ChartArea
            ChartArea oCA = new ChartArea();
            oCA.Name = "ChartArea1";
            oCA.BorderColor = Color.FromArgb(64, 64, 64, 64);
            oCA.BackSecondaryColor = Color.White;
            oCA.BackColor = Color.OldLace;
            oCA.ShadowColor = Color.Transparent;
            oCA.BackGradientStyle = GradientStyle.TopBottom;
            //area3dstyle
            oCA.Area3DStyle.Rotation = 10;
            oCA.Area3DStyle.Perspective = 10;
            oCA.Area3DStyle.Inclination = 15;
            oCA.Area3DStyle.IsRightAngleAxes = false;
            oCA.Area3DStyle.WallWidth = 0;
            oCA.Area3DStyle.IsClustered = false;
            //axisy
            oCA.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            oCA.AxisY.LabelAutoFitMaxFontSize = 8;
            //oCA.AxisY.LabelStyle 
            LabelStyle oLS = new LabelStyle();
            oLS.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLS.Format = "N2";
            oCA.AxisY.LabelStyle = oLS;
            //MajorGrid
            oCA.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            //axisx
            oCA.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            oCA.AxisX.LabelAutoFitMaxFontSize = 8;
            //oCA.AxisY.LabelStyle 
            LabelStyle oLSX = new LabelStyle();
            oLSX.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLSX.IsEndLabelVisible = false;
            oLSX.Interval = 1.0;
            //oLSX.IntervalType = 
            //oLSX.IntervalType = DateTimeIntervalType.Auto;
            oLSX.IntervalOffset = 0.0;
            oLSX.IntervalOffsetType = DateTimeIntervalType.NotSet;
            //oLSX.IntervalOffset = 0;
            oLSX.TruncatedLabels = true;
            oCA.AxisX.LabelStyle = oLSX;
            //MajorGrid
            oCA.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            
            m_chart.ChartAreas.Add(oCA);
            #endregion

            //<asp:CHART id="ChartSPI1" runat="server" Palette="None" Width="450px" Height="400px" BorderDashStyle="Solid" 
            //    BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
            //    <titles>
            //        <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14pt, style=Bold" ShadowOffset="3" Text="Evolución de CPI/SPI" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
            //    </titles>
            //    <legends>
            //        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
            //    </legends>
            //    <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
            //    <chartareas>
            //        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
            //            <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
            //            <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
            //                <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N2" />
            //                <MajorGrid LineColor="64, 64, 64, 64" />
            //            </axisy>
            //            <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
            //                <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
            //                <MajorGrid LineColor="64, 64, 64, 64" />
            //            </axisx>
            //        </asp:ChartArea>
            //    </chartareas>
            //</asp:CHART>

            return m_chart;
        }

        public static Chart GenerarGraficoSituacion()
        {
            Chart m_chart = new Chart();

            #region Chart setting
            m_chart.Palette = ChartColorPalette.None;
            m_chart.Width = Unit.Pixel(450);
            m_chart.Height = Unit.Pixel(400);
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BackGradientStyle = GradientStyle.TopBottom;
            m_chart.BorderlineColor = ColorTranslator.FromHtml("#1A3B69");// Color.FromArgb(181, 64, 1);
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BorderlineWidth = 2;
            m_chart.BackColor = ColorTranslator.FromHtml("#D6D6D6");
            m_chart.SuppressExceptions = true;
            m_chart.BackImageTransparentColor = ColorTranslator.FromHtml("#DDE6E9");
            m_chart.ImageType = ChartImageType.Png;
            m_chart.IsSoftShadows = false;
            #endregion
            #region Título
            Title oTitulo = new Title();
            oTitulo.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            oTitulo.Font = new Font("Arial", 14F, FontStyle.Bold);
            oTitulo.ShadowOffset = 3;
            oTitulo.Text = "Situación del proyecto a mes de referencia";
            oTitulo.ForeColor = Color.FromArgb(26, 59, 105);
            m_chart.Titles.Add(oTitulo);
            #endregion
            #region Legend
            Legend oLegend = new Legend("Default");
            oLegend.LegendStyle = LegendStyle.Table;
            oLegend.IsTextAutoFit = false;
            oLegend.Docking = Docking.Bottom;
            oLegend.BackColor = Color.Transparent;
            oLegend.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLegend.Alignment = StringAlignment.Center;
            m_chart.Legends.Add(oLegend);
            #endregion
            #region BorderSkin
            BorderSkin oBS = new BorderSkin();
            oBS.SkinStyle = BorderSkinStyle.Emboss;
            oBS.BackImageTransparentColor = ColorTranslator.FromHtml("#DDE6E9");
            oBS.BorderWidth = 0;
            oBS.BorderColor = ColorTranslator.FromHtml("#DDE6E9");
            oBS.PageColor = ColorTranslator.FromHtml("#DDE6E9");
            m_chart.BorderSkin = oBS;
            #endregion
            #region ChartArea
            ChartArea oCA = new ChartArea();
            oCA.Name = "ChartArea1";
            oCA.BorderColor = Color.FromArgb(64, 64, 64, 64);
            oCA.BackSecondaryColor = Color.White;
            oCA.BackColor = Color.OldLace;
            oCA.ShadowColor = Color.Transparent;
            oCA.BackGradientStyle = GradientStyle.TopBottom;
            oCA.BorderDashStyle = ChartDashStyle.Solid;
            //area3dstyle
            oCA.Area3DStyle.Rotation = 10;
            oCA.Area3DStyle.Perspective = 10;
            oCA.Area3DStyle.Inclination = 15;
            oCA.Area3DStyle.IsRightAngleAxes = false;
            oCA.Area3DStyle.WallWidth = 0;
            oCA.Area3DStyle.IsClustered = false;
            //axisy
            oCA.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            oCA.AxisY.LabelAutoFitMaxFontSize = 8;
            //oCA.AxisY.LabelStyle 
            LabelStyle oLS = new LabelStyle();
            oLS.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLS.Format = "N0";
            oLS.Interval = 0.2;
            oCA.AxisY.LabelStyle = oLS;
            //MajorGrid
            oCA.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            //axisx
            oCA.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            oCA.AxisX.LabelAutoFitMaxFontSize = 8;
            //oCA.AxisY.LabelStyle 
            LabelStyle oLSX = new LabelStyle();
            oLSX.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            oLSX.Format = "N0";
            oLSX.Interval = 0.2;
//            oLSX.IsEndLabelVisible = false;
//            oLSX.IntervalOffset = 0.0;
//            oLSX.IntervalOffsetType = DateTimeIntervalType.NotSet;
            //oLSX.IntervalOffset = 0;
//            oLSX.TruncatedLabels = true;
            oCA.AxisX.LabelStyle = oLSX;
            //MajorGrid
            oCA.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            m_chart.ChartAreas.Add(oCA);
            //Serie
            Series oS = new Series("Proyecto");
            oS.YValuesPerPoint = 1;
            oS.ChartType = SeriesChartType.Bubble;
            oS.BorderColor = Color.FromArgb(180, 26, 59, 105);
            oS.Color = Color.FromArgb(220, 65, 140, 240);
            oS.Font = new Font("Arial", 8.25F);
            oS.IsValueShownAsLabel = false;
                //IsValueShownAsLabel="True" YValuesPerPoint="2" Name="Proyecto" ChartType="Bubble" 
            //BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Font="Trebuchet MS, 9pt">
            m_chart.Series.Add(oS);
            #endregion

            //<asp:CHART id="ChartSPI2" runat="server" Palette="None" Width="450px" Height="400px" BorderDashStyle="Solid" 
            //    BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
            //    <titles>
            //        <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14pt, style=Bold" ShadowOffset="3" Text="Situación del proyecto a mes de referencia" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
            //    </titles>
            //    <legends>
            //        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
            //    </legends>
            //    <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
            //    <series>
            //        <asp:Series IsValueShownAsLabel="True" YValuesPerPoint="2" Name="Proyecto" ChartType="Bubble" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Font="Trebuchet MS, 9pt">
            //            <points>
            //                <asp:DataPoint YValues="15,8" />
            //            </points>
            //        </asp:Series>
            //    </series>                            
            //    <chartareas>
            //        <asp:ChartArea Name="ChartArea1" BorderColor="Black" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="Solid">
            //            <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
            //            <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
            //                <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N0" Interval="0.2" />
            //                <MajorGrid LineColor="64, 64, 64, 64" />
            //            </axisy>
            //            <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
            //                <LabelStyle Font="Arial, 8.25pt, style=Bold"  Format="N0" Interval="0.2" />
            //                <MajorGrid LineColor="64, 64, 64, 64" />
            //            </axisx>
            //        </asp:ChartArea>
            //    </chartareas>
            //</asp:CHART>


            return m_chart;
        }

    }
}