<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Archivo_Conexiones_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">

    <div id="chartdiv" align="center" style="position: absolute; top: 115px;"></div>
    <script type="text/javascript">
        var nPropias = <%=nPropias %>;
        var nAjenas = <%=nAjenas %>;
    </script> 
<center>    
<table id="tblGlobal" style="width:960px; text-align:left" align="center" cellspacing="3" cellpadding="3">
<colgroup>
    <col style="width:200px"/>
    <col style="width:20px"/>
    <col style="width:740px"/>
</colgroup>
    <tr>
        <td colspan="3">
            <label class="texto">(Pulsar sobre las barras del gráfico para obtener el detalle mensual)</label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:CHART id="Chart1" runat="server" Palette="BrightPastel" Visible="false" 
                BackColor="243, 223, 193" Width="960px" Height="320px" BorderDashStyle="Solid" 
                BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" 
                BorderlineWidth="2" ImageStorageMode="UseImageLocation" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)">
                <legends>
                    <asp:legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1" Docking="Bottom" 
                        IsDockedInsideChartArea="False" Name="Default" BackColor="Transparent" Font="Arial, 8pt, style=Bold" Alignment="Center">
                    </asp:legend>
                </legends>
                <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="Transparent" BorderWidth="0" PageColor="236, 240, 238" />
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" 
                        ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                        <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N0" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisy>
                        <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" IsStaggered="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisx>
                    </asp:ChartArea>
                </chartareas>
            </asp:CHART>
        </td>
    </tr>
    <tr>
        <td>
            <fieldset style="width:190px">
            <legend><label id="lblPropias">Detalle</label> <img id="imgCorP" src='<%=Session["strServer"] %>Images/imgCorazonR1.gif' style='vertical-align:middle;margin:0px;border:0px;' /></legend>
            <table style="width: 160px; height: 17px; margin-top:5px;">
                <colgroup>
                <col style='width:100px;' />
                <col style='width:60px;' />
                </colgroup>
	            <tr id="tblTitulo" class="TBLINI">
	                <td>&nbsp;Fecha</td>
					<td style="text-align:right; padding-right:5px;">Usuario</td>
	            </tr>
            </table>
            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width: 176px; height:150px;" >
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:160px">
                    <%=strTablaHTMLPropias%>
                </div>
            </div>
            <table id="tblTotales" style="width: 160px; height: 17px; margin-bottom:3px; text-align: right;">
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
	            </tr>
            </table>
            </fieldset>
        </td>
        <td></td>
        <td>
            <fieldset style="width:720px;">
            <legend><label id="lblAjenas">Detalle</label> <img id="imgCorA" src='<%=Session["strServer"] %>Images/imgCorazonA1.gif' style='vertical-align:middle;margin:0px;border:0px;' /></legend>
            <table style="width: 695px; height: 17px; margin-top:5px;">
                <colgroup>
                    <col style='width:100px;' />
                    <col style='width:595px;' />
                </colgroup>
	            <tr id="TR1" class="TBLINI">
	                <td>&nbsp;Fecha</td>
					<td>Profesional</td>
	            </tr>
            </table>
            <div id="divCatalogo2" style="overflow:auto; overflow-x:hidden; width: 711px; height:150px;" >
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:695px">
                <%=strTablaHTMLAjenas%>
                </div>
            </div>
            <table style="width: 695px; height: 17px; margin-bottom:3px; text-align: right;">
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
	            </tr>
            </table>
            </fieldset>
        </td>
    </tr>
</table>
</center>  
<input id="hdnMesAct" type="hidden" runat="server" value="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

