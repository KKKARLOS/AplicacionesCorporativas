<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="true" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">  
	#ctl00_CPHC_tsPestanas table { table-layout:auto; }
</style>
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var sMONEDA_VDP = "<%=(Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString() %>";
    bLectura = <%=((bool)Session["MODOLECTURA_PROYECTOSUBNODO"]) ? "true":"false" %>;
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
</script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<ucgus:UCGusano ID="UCGusano1" runat="server" />
<table id="tblSuperior" class="texto" style="width:990px; margin-left:10px;" cellpadding="3">
    <colgroup>
        <col style="width:80px;" />
        <col style="width:540px;" />
        <col style="width:370px;" />
    </colgroup>
    <tr>
        <td><label id="lblProy" class="enlace" onclick="getPE()" style="height:16px;">Proyecto</label></td>
        <td><asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" runat="server" onkeypress="f_onkeypress(event)" />
            <asp:TextBox ID="txtDesPE" style="width:437px;" Text="" runat="server" readonly="true" />
        </td>
        <td><div id="divMonedaImportes" runat="server" style="visibility:hidden">
                <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()" style="height:16px; width:75px;">Importes en </label><label id="lblMonedaImportes" style="height:16px;" runat="server"></label>
            </div>
        </td>
    </tr>
    <tr>
        <td><label id="lblLineaBase" runat="server" class="enlace" onclick="getLineaBase()" style="height:16px;">Línea base</label></td>
        <td><asp:TextBox ID="txtLineaBase" style="width:450px;" Text="" runat="server" readonly="true" /><asp:Label id="lblNumeroLineaBase" runat="server" style="margin-left:10px;" Text="" /></td>
        <td><label id="lblMesReferencia" class="enlace" style="width:155px; height:16px; cursor:pointer;" onclick="setMes();" >Mes cerrado de referencia</label><asp:TextBox ID="txtMesReferencia" runat="server" Text="" style="width:90px; text-align:center;" /></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-top:10px; padding-right:15px;">
            <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="970px" 
                            MultiPageID="mpContenido" 
                            ClientSideOnLoad="CrearPestanas" 
                            ClientSideOnItemClick="getPestana">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
		                    <eo:TabItem Text-Html="Análisis básico" Width="120"></eo:TabItem>
		                    <eo:TabItem Text-Html="Análisis avanzado" Width="120"></eo:TabItem>
		                    <eo:TabItem Text-Html="Reconocimiento" Width="120"></eo:TabItem>
		                    <eo:TabItem Text-Html="Evolución CPI/SPI" Width="120"></eo:TabItem>
		                    <eo:TabItem Text-Html="Evolución producción" Width="140"></eo:TabItem>
		                    <eo:TabItem Text-Html="Desglose" Width="120"></eo:TabItem>
		                    <eo:TabItem Text-Html="Observaciones" Width="120"></eo:TabItem>
		            </Items>
	            </TopGroup>
                <LookItems>
                    <eo:TabItem ItemID="_Default" 
                        LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
                        LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
                        LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
                        Image-Url="~/Images/Pestanas/normal_bg.gif"
                        Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
                        Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
                        RightIcon-Url="~/Images/Pestanas/normal_right.gif"
                        RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
                        RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
                        NormalStyle-CssClass="TabItemNormal"
                        HoverStyle-CssClass="TabItemHover"
                        SelectedStyle-CssClass="TabItemSelected"
                        DisabledStyle-CssClass="TabItemDisabled"
                        Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
                    </eo:TabItem>
                </LookItems>
            </eo:TabStrip>
            <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="970px" Height="480px">
                <eo:PageView id="PageView1" CssClass="PageView" runat="server">
				<!-- Pestaña 1 básico-->
				<div id="divVisilidadTabla" runat="server"></div>
				<table id="tblBasico" class="texto" style="visibility:hidden; width:950px;" border="0">
				<colgroup> 
				    <col style="width:450px;" />
				    <col style="width:160px;" />
				    <col style="width:340px;" />
				</colgroup>
				<tr>
				    <td colspan="2" style="padding-top:2px; padding-left:2px; font-size:11pt;">Comparativa con la línea base desde el inicio del proyecto</td>
				    <td rowspan="2">
						<fieldset id="fstFiltroSeleccion" style="width:250px;">
					        <legend>Filtros de selección</legend>
					        <table style="width:250px; margin-top: 5px;">
                                <colgroup><col style="width:80px;" /><col style="width:80px;" /><col style="width:90px;" /></colgroup>
					            <tr>
					                <td>
				                        <asp:CheckBox ID="chkIAP" runat="server" Text="IAP" ToolTip="Dedicaciones" Checked="true" onclick="gga()" />
                                    </td>
                                    <td>
				                        <asp:CheckBox ID="chkEXT" runat="server" Text="EXT" ToolTip="Compras / Subcontrataciones" Checked="true" onclick="gga()" />
                                    </td>
                                    <td>
				                        <asp:CheckBox ID="chkOCO" runat="server" Text="OCO" ToolTip="Otros consumos" Checked="true" onclick="gga()" />
					                </td>
					            </tr>
					        </table>
				        </fieldset>
				    </td>
				</tr>
				<tr>
				    <td colspan="2">
				        <table id="tblComparativa" style="width:580px;" cellpadding="3">
				        <colgroup>
				            <col style="width:100px;" />
				            <col style="width:120px;" />
				            <col style="width:120px;" />
				            <col style="width:120px;" />
				            <col style="width:120px;" />
				        </colgroup>
	                    <tr class="texto" align="center">
                            <td></td>
                            <td colspan="2" class="colTabla">TOTAL</td>
                            <td colspan="2" class="colTabla1">SELECCIÓN</td>
	                    </tr>
				        <tr id="tblTituloComparativa" class="TBLINI" align="center" style="padding-top:1px; height:17px;">
				            <td style="padding-left:3px;"></td>
				            <td style="text-align:center;">Planificado</td>
				            <td style="text-align:center;">Real</td>
				            <td style="text-align:center;">Planificado</td>
				            <td style="text-align:center;">Real</td>
				        </tr>
				        <tr class="FA">
				            <td style="padding-left:3px;">% Valor Ganado</td>
				            <td style="text-align:center;"><label id="lblCodigo19" style="font-weight:bold" runat="server"></label></td>
				            <td style="text-align:center;"><label id="lblCodigo20" style="font-weight:bold" runat="server"></label></td>
				            <td style="text-align:center;"><label id="lblCodigo19_bis" style="font-weight:bold" runat="server"></label></td>
				            <td style="text-align:center;"><label id="lblCodigo20_bis" style="font-weight:bold" runat="server"></label></td>
				        </tr>
				        <tr class="FB">
				            <td style="padding-left:3px;">Coste acumulado</td>
				            <td style="text-align:center;"><label id="lblCodigo21" style="font-weight:bold" runat="server"></label></td>
				            <td style="text-align:center;"><label id="lblCodigo22" style="font-weight:bold" runat="server"></label></td>
				            <td style="text-align:center;"><label id="lblCodigo21_bis" style="font-weight:bold" runat="server"></label></td>
				            <td style="text-align:center;"><label id="lblCodigo22_bis" style="font-weight:bold" runat="server"></label></td>
				        </tr>
				        </table>
				    </td>
				</tr>
				<tr>
				    <td style="padding-top:7px; padding-left:2px; font-size:11pt;">Análisis de situación y previsiones del proyecto</td>
				    <td colspan="2" rowspan="2" id="cldChart">
                        <asp:CHART id="Chart1" oncustomizelegend="Chart1_CustomizeLegend" runat="server" Palette="None" Width="500px" Height="360px" BorderDashStyle="Solid" 
                            BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
                            <titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14.25pt, style=Bold" ShadowOffset="3" Text="Gráfico de análisis del Valor Ganado" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                            </titles>
                            <legends>
		                        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
                            </legends>
                            <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                    <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                    <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N0" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisy>
                                    <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisx>
                                </asp:ChartArea>
                            </chartareas>
                        </asp:CHART>
				    </td>
				</tr>
				<tr>
				    <td style="vertical-align:top;">
				        <table id="tblAnalisis" style="width:424px;" cellpadding="3">
				        <colgroup>
				            <col style="width:80px;" />
				            <col style="width:164px;" />
				            <col style="width:180px;" />
				        </colgroup>
				        <tr id="tblTituloAnalisis" class="TBLINI" align="center" style="padding-top:1px;">
				            <td style="padding-left:3px;">Desviaciones</td>
				            <td style="padding-left:5px; padding-right:10px;">Situación actual</td>
				            <td style="padding-left:10px; padding-right:3px;">Previsiones</td>
				        </tr>
				        </table>
				        <div style="overflow:auto; overflow-x:hidden; width:440px; height:300px;">
				        <table id="tblBodyAnalisis" style="width:240px;" cellpadding="3">
				        <colgroup>
				            <col style="width:80px;" />
				            <col style="width:164px;" />
				            <col style="width:180px;" />
				        </colgroup>
				        <tr class="FA" style="vertical-align:top;">
				            <td style="padding-left:3px;">De coste</td>
				            <td style="padding-left:5px; padding-right:10px;">Se han gastado <nobr id="lblCodigo1" style="font-weight:bold" runat="server"></nobr>&nbsp;de los <nobr id="lblCodigo2" style="font-weight:bold" runat="server"></nobr>&nbsp;necesarios para el Valor Ganado Real, que es el <nobr id="lblCodigo23" style="font-weight:bold" runat="server"></nobr>.</td>
				            <td style="padding-left:10px; padding-right:3px;">Según la situación actual, el proyecto puede terminar en la horquilla <nobr id="lblCodigo9" style="font-weight:bold" runat="server"></nobr>&nbsp;y <nobr id="lblCodigo10" style="font-weight:bold" runat="server"></nobr>, frente a los <nobr id="lblCodigo11" style="font-weight:bold" runat="server"></nobr>&nbsp;planificados para la totalidad del proyecto.</td>
				        </tr>
				        <tr class="FB" style="vertical-align:top;">
				            <td style="padding-left:3px;">De calendario</td>
				            <td style="padding-left:5px; padding-right:10px;">El proyecto tiene un <label id="lblCodigo3" style="font-weight:bold" runat="server"></label>&nbsp;respecto del avance técnico planificado.</td>
				            <td style="padding-left:10px; padding-right:3px;">De seguir el ritmo actual, el proyecto finalizaría el <label id="lblCodigo12" style="font-weight:bold" runat="server"></label></td>
				        </tr>
				        <tr class="FA" style="vertical-align:top;">
				            <td style="padding-left:3px;">De margen</td>
				            <td style="padding-left:5px; padding-right:10px;">El margen acumulado hasta el momento es de <nobr id="lblCodigo4" style="font-weight:bold" runat="server"></nobr>&nbsp;<nobr id="lblCodigo5" style="font-weight:bold" runat="server"></nobr>&nbsp;frente al <nobr id="lblCodigo6" style="font-weight:bold" runat="server"></nobr>&nbsp;planificado al final del proyecto.</td>
				            <td style="padding-left:10px; padding-right:3px;">Según la situación actual, el proyecto puede terminar en la horquilla <nobr id="lblCodigo13" style="font-weight:bold" runat="server"></nobr>&nbsp;y <nobr id="lblCodigo14" style="font-weight:bold" runat="server"></nobr>, frente a los <nobr id="lblCodigo15" style="font-weight:bold" runat="server"></nobr>&nbsp;planificados para la totalidad del proyecto, es decir, con una rentabilidad entre el <nobr id="lblCodigo17" style="font-weight:bold" runat="server"></nobr>&nbsp;y el <nobr id="lblCodigo16" style="font-weight:bold" runat="server"></nobr>.</td>
				        </tr>
				        <tr class="FB" style="vertical-align:top;">
				            <td style="padding-left:3px;">De producción</td>
				            <td style="padding-left:5px; padding-right:10px;">La producción acumulada hasta el momento es <nobr id="lblCodigo7" style="font-weight:bold" runat="server"></nobr>&nbsp;a la producción sugerida por el Valor Ganado Real, que es el <nobr id="lblCodigo8" style="font-weight:bold" runat="server"></nobr>.</td>
				            <td style="padding-left:10px; padding-right:3px;">Se sugiere que la producción acumulada hasta el momento sea de <nobr id="lblCodigo18" style="font-weight:bold" runat="server"></nobr>.</td>
				        </tr>
				        <tr class="FA" style="vertical-align:top;">
				            <td style="padding-left:3px;">De productividad</td>
				            <td style="padding-left:5px; padding-right:10px;">La productividad del proyecto es de <nobr id="lblDP" style="font-weight:bold" runat="server"></nobr> %.</td>
				            <td style="padding-left:10px; padding-right:3px;">Para que el proyecto finalice en el coste planificado, la productividad debe <nobr id="lblAumenDismDP" style="font-weight:bold" runat="server"></nobr>&nbsp;un <nobr id="lblNNDP" style="font-weight:bold" runat="server"></nobr> %, hasta el <nobr id="lblVVDP" style="font-weight:bold" runat="server"></nobr> %.</td>
				        </tr>
				        <tr class="FB" style="vertical-align:top;">
				            <td style="padding-left:3px;">De velocidad</td>
				            <td style="padding-left:5px; padding-right:10px;">La velocidad del proyecto es de <nobr id="lblDV" style="font-weight:bold" runat="server"></nobr> % con respecto de la planificación.</td>
				            <td style="padding-left:10px; padding-right:3px;">Para que el proyecto finalice en el plazo planificado, la velocidad debe <nobr id="lblAumenDismDV" style="font-weight:bold" runat="server"></nobr>&nbsp;un <nobr id="lblYYDV" style="font-weight:bold" runat="server"></nobr> %, hasta el <nobr id="lblZZDV" style="font-weight:bold" runat="server"></nobr> %.</td>
				        </tr>
				        </table>
				        </div>
                        <table id="Table3" style="width:424px; height:17px;">
                            <tr class="TBLFIN">
                                <td>&nbsp;</td>
                            </tr>
	                    </table>
				    </td>
				</tr>
				</table>
                </eo:PageView>

                <eo:PageView id="PageView2" CssClass="PageView" runat="server">
                <!-- Pestaña 2 avanzado -->
				<table style="width:100%;margin-top:10px;">
				<tr>
				    <td>
				        <table id="tblTituloAvanzado" style="width:940px;" cellpadding="4">
				        <colgroup>
				            <col style="width:60px;" />
				            <col style="width:780px;" />
				            <col style="width:100px;" />
				        </colgroup>
				        <tr class="TBLINI" align="center" style="padding-top:1px; height:17px;">
				            <td style="padding-left:3px;">Indicador</td>
				            <td style="text-align:left; padding-left:5px;">Descripción</td>
				            <td style="text-align:right; padding-right:3px;">Valor</td>
				        </tr>
                        </table>
                        <div id="divCatalogoAvanzado" style="width:956px; height:420px; overflow:auto; overflow-x:hidden;" runat="server">
                            <div style="width:940px; height:auto;">
				            <table id="tblAvanzado" class="texto" style="width:940px;" border="0" cellpadding="4">
				            <colgroup>
				                <col style="width:60px;" />
				                <col style="width:780px;" />
				                <col style="width:100px;" />
				            </colgroup>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Actual Cost" style="padding-left:3px;">AC</td>
				                <td>Consumos reales del proyecto hasta un mes seleccionado.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoAC" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Budget At Completion" style="padding-left:3px;">AC-IAP</td>
				                <td>Consumos reales del proyecto hasta el mes de referencia seleccionado, vinculados a consumos IAP.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoAC_IAP" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Budget At Completion" style="padding-left:3px;">AC-EXT</td>
				                <td>Consumos reales del proyecto hasta el mes de referencia seleccionado, vinculados a compras y subcontrataciones a precio cerrado.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoAC_EXT" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Budget At Completion" style="padding-left:3px;">AC-OCO</td>
				                <td>Consumos reales del proyecto hasta el mes de referencia seleccionado, vinculados a otros consumos.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoAC_OCO" runat="server"></label></td>
				            </tr>
				            
				            
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Budget At Completion" style="padding-left:3px;">BAC</td>
				                <td>Coste Total Planificado para el proyecto (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoBAC" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Budget At Completion" style="padding-left:3px;">BAC-IAP</td>
				                <td>Coste Total Planificado para el proyecto vinculado al % avance técnico de PST (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoBAC_IAP" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Budget At Completion" style="padding-left:3px;">BAC-EXT</td>
				                <td>Coste Total Planificado para el proyecto vinculado a compras y subcontrataciones a precio cerrado (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoBAC_EXT" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Budget At Completion" style="padding-left:3px;">BAC-OCO</td>
				                <td>Coste Total Planificado para el proyecto vinculado a otros consumos (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoBAC_OCO" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Cost Performance Index" style="padding-left:3px;">CPI</td>
				                <td>Indice de Rendimiento de Coste. Un valor inferior a 1,00 alerta de un sobrecoste en el proyecto.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoCPI" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Cost Variance" style="padding-left:3px;">CV</td>
				                <td>Variación de Coste. Un valor negativo indica un coste normalmente no recuperable en el proyecto.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoCV" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Estimate at Completion" style="padding-left:3px;">EAC</td>
				                <td>Coste Estimado al final del proyecto</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEAC" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td style="padding-left:3px;">EAC1</td>
				                <td>Estimación obsoleta. Los consumos restantes se basan en los consumos planificados en una línea base, independientemente de lo sucedido hasta el momento.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEAC1" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td style="padding-left:3px;">EAC2</td>
				                <td>Estimación objetiva. Los consumos restantes se calculan en base al rendimiento de coste actual del proyecto (CPI).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEAC2" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td style="padding-left:3px;">EAC3</td>
				                <td>Estimación avanzada. Los consumos restantes se calculan teniendo en cuenta tanto el rendimiento de coste (CPI) como el rendimiento de calendario actual del proyecto (SPI).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEAC3" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Estimate at Completion (time)" style="padding-left:3px;">EACt</td>
				                <td>Fecha prevista de finalización del proyecto, al ritmo actual.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEACt" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Estimate To Complete" style="padding-left:3px;">ETC</td>
				                <td>Coste restante para completar el proyecto.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoETC" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Earned Value" style="padding-left:3px;">EV</td>
				                <td>Valor Ganado hasta un mes seleccionado, o valor del trabajo realizado, en base al grado de avance.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEV" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Earned Value" style="padding-left:3px;">EV-IAP</td>
				                <td>Valor Ganado hasta el mes de referencia seleccionado, o valor del trabajo realizado, en base al grado de avance técnico vinculado a imputaciones IAP.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEV_IAP" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Earned Value" style="padding-left:3px;">EV-EXT</td>
				                <td>Valor Ganado hasta el mes de referencia seleccionado, o valor del trabajo realizado, vinculado a compras y subcontrataciones a precio cerrado.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEV_EXT" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Earned Value" style="padding-left:3px;">EV-OCO</td>
				                <td>Valor Ganado hasta el mes de referencia seleccionado, o valor del trabajo realizado, vinculado a otros consumos y el grado de avance técnico PST.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoEV_OCO" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Profitability At Completion" style="padding-left:3px;">PAC</td>
				                <td>Margen estimado al final de proyecto.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPAC" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td style="padding-left:3px;">PAC1</td>
				                <td>Margen estimado al final de proyecto, basado en EAC1.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPAC1" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td style="padding-left:3px;">PAC2</td>
				                <td>Margen estimado al final de proyecto, basado en EAC2.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPAC2" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td style="padding-left:3px;">PAC3</td>
				                <td>Margen estimado al final de proyecto, basado en EAC3.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPAC3" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Planned Value" style="padding-left:3px;">PV</td>
				                <td>Consumos planificados hasta un mes seleccionado (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPV" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Planned Value" style="padding-left:3px;">PV_IAP</td>
				                <td>Consumos planificados para el proyecto hasta un mes seleccionado, vinculados al % avance técnico de PST (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPV_IAP" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Planned Value" style="padding-left:3px;">PV_EXT</td>
				                <td>Consumos planificados para el proyecto hasta un mes seleccionado, vinculados a compras y subcontrataciones a precio cerrado (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPV_EXT" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Planned Value" style="padding-left:3px;">PV_OCO</td>
				                <td>Consumos planificados para el proyecto hasta un mes seleccionado, vinculado a otros consumos (en una línea base).</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoPV_OCO" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Schedule Performance Index" style="padding-left:3px;">SPI</td>
				                <td>Indice de Rendimiento del Calendario. Un valor inferior a 1,00 alerta de un retraso en el proyecto.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoSPI" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="Schedule Variance" style="padding-left:3px;">SV</td>
				                <td>Variación del calendario. Un valor negativo alerta de un retraso en el proyecto.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoSV" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="Variance At Completion" style="padding-left:3px;">VAC</td>
				                <td>Variación de coste al final del proyecto. Un valor negativo indica el sobrecoste previsto para el proyecto.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoVAC" runat="server"></label></td>
				            </tr>
				            <tr class="FB" style="vertical-align:top;">
				                <td title="To-Complete Cost Performance Index" style="padding-left:3px;">TCPI</td>
				                <td>Índice de Rendimiento de Coste para Completar. Un valor superior a 1,00 indica que hay que aumentar la productividad.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoTCPI" runat="server"></label></td>
				            </tr>
				            <tr class="FA" style="vertical-align:top;">
				                <td title="To-Complete Schedule Performance Index" style="padding-left:3px;">TSPI</td>
				                <td>Índice de Rendimiento de Calendario para Completar. Un valor superior a 1,00 indica que hay que aumentar la velocidad.</td>
				                <td style="text-align:right; padding-right:3px;"><label id="lblCodigoTSPI" runat="server"></label></td>
				            </tr>
				            </table>
                            </div>
                        </div>
                        <table id="Table2" style="width:940px; height:17px;">
                            <tr class="TBLFIN">
                                <td>&nbsp;</td>
                            </tr>
	                    </table>
				    </td>
				</tr>
				</table>
                </eo:PageView>
                
                <eo:PageView id="PageView3" CssClass="PageView" runat="server">
                <!-- Pestaña 3 Reconocimiento -->
				<table style="width:100%;margin-top:10px;">
				<tr>
				    <td>
				        <table style="width:250px; margin-left:680px;">
				            <tr>
				                <td style="width:16px"><input type="checkbox" id="chkReconocimieto" class="check" style="cursor:pointer; vertical-align:middle;" onclick="getReconocimiento()" checked="checked" /></td>
				                <td style="width:234px"><label id="lblReconocimiento" style="cursor:pointer; vertical-align:middle; width:230px;" onclick="getReconocimientoAux();">Mostrar únicamente lo pendiente de reconocer</label></td>
				            </tr>
				        </table>
			        </td>
				</tr>
				<tr>
				    <td>
				        <table style="width:940px; margin-top:3px;">
				        <colgroup>
				            <col style="width:160px;" />
				            <col style="width:160px;" />
				            <col style="width:160px;" />
				            <col style="width:140px;" />
				            <col style="width:90px;" />
				            <col style="width:100px;" />
				            <col style="width:130px;" />
				        </colgroup>
				        <tr id="tblTituloReconocimiento" class="TBLINI" style="padding-top:1px; height:17px;">
				            <td>Concepto</td>
				            <td>Clase</td>
				            <td>Motivo</td>
				            <td><%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %> / Proveedor</td>
				            <td class="num">Importe</td>
				            <td style="padding-left:5px" title="Mes al que pertenecía el dato en el momento de crear la línea base">Mes original</td>
				            <td style="padding-left:14px" title="Mes de reconocimiento del consumo">Reconocimiento</td>
				        </tr>
				        </table>
                        <div id="divCatalogoReconocimiento" style="width:956px; height:400px; overflow:auto; overflow-x:hidden; " runat="server">
                            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:940px; height:auto;">
                            <%=strTablaHTMLReconocimiento%>
                            </div>
                        </div>
                        <table id="Table1" style="width:940px; height:17px;">
                            <tr class="TBLFIN">
                                <td>&nbsp;</td>
                            </tr>
	                    </table>
				    </td>
				</tr>
				</table>
                </eo:PageView>

                <eo:PageView id="PageView4" CssClass="PageView" runat="server">
                <!-- Pestaña 4 Evolución CPI/SPI -->
				<table style="width:940px; margin-left:10px;">
				<colgroup> 
				    <col style="width:490px;" />
				    <col style="width:450px;" />
				</colgroup>
				<tr>
				    <td colspan="2" style="padding-bottom:10px;">
						<fieldset id="fstFiltroSeleccionCPISPI" style="width:250px;">
					        <legend>Filtros de selección</legend>
					        <table style="width:250px; margin-top: 5px;">
                                <colgroup><col style="width:80px;" /><col style="width:80px;" /><col style="width:90px;" /></colgroup>
					            <tr>
					                <td>
				                        <asp:CheckBox ID="chkIAP_CPISPI" runat="server" Text="IAP" ToolTip="Dedicaciones" Checked="true" onclick="gcpispi()" Enabled=false />
                                    </td>
                                    <td>
				                        <asp:CheckBox ID="chkEXT_CPISPI" runat="server" Text="EXT" ToolTip="Compras / Subcontrataciones" Checked="true" onclick="gcpispi()" />
                                    </td>
                                    <td>
				                        <asp:CheckBox ID="chkOCO_CPISPI" runat="server" Text="OCO" ToolTip="Otros consumos" Checked="true" onclick="gcpispi()" />
					                </td>
					            </tr>
					        </table>
				        </fieldset>
				    </td>
				</tr>
				<tr>
				    <td id="cldChartSPI1">
                        <asp:CHART id="ChartSPI1" runat="server" Palette="None" Width="450px" Height="400px" BorderDashStyle="Solid" 
                            BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
                            <titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14pt, style=Bold" ShadowOffset="3" Text="Evolución de CPI/SPI" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                            </titles>
                            <legends>
		                        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
                            </legends>
                            <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                    <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                    <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N2" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisy>
                                    <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisx>
                                </asp:ChartArea>
                            </chartareas>
                        </asp:CHART>
				    </td>
				    <td id="cldChartSPI2">
                        <asp:CHART id="ChartSPI2" runat="server" Palette="None" Width="450px" Height="400px" BorderDashStyle="Solid" 
                            BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
                            <titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14pt, style=Bold" ShadowOffset="3" Text="Situación del proyecto a mes de referencia" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                            </titles>
                            <legends>
		                        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
                            </legends>
                            <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
							<series>
								<asp:Series IsValueShownAsLabel="False" YValuesPerPoint="1" Name="Proyecto" ChartType="Bubble" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Font="Trebuchet MS, 9pt">
									<points>
										<asp:DataPoint YValues="15,8" />
									</points>
								</asp:Series>
							</series>                            
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="Black" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="Solid">
                                    <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                    <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N0" Interval="0.2" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisy>
                                    <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold"  Format="N0" Interval="0.2" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisx>
                                </asp:ChartArea>
                            </chartareas>
                        </asp:CHART>
				    </td>
				</tr>
				</table>
                </eo:PageView>

                <eo:PageView id="PageView5" CssClass="PageView" runat="server">
                <!-- Pestaña 5 Evolución producción -->
				<table style="width:940px; margin-left:10px; margin-top:10px;">
				<colgroup> 
				    <col style="width:940px;" />
				</colgroup>
				<tr>
				    <td id="cldChartProduccion">
                        <asp:CHART id="ChartProduccion" runat="server" Palette="None" Width="890px" Height="460px" BorderDashStyle="Solid" 
                            BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
                            <titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14pt, style=Bold" ShadowOffset="3" Text="Evolución producción" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                            </titles>
                            <legends>
		                        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
                            </legends>
                            <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                    <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                    <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="N2" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisy>
                                    <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
	                                    <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
	                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisx>
                                </asp:ChartArea>
                            </chartareas>
                        </asp:CHART>
				    </td>
				</tr>
				</table>
                </eo:PageView>

                <eo:PageView id="PageView6" CssClass="PageView" runat="server">
                <!-- Pestaña 6 Desglose -->
                <table id="tblGeneral" style="width:951px; margin-top:10px;" runat="server" cellpadding="0" border="0">
				<colgroup> 
				    <col style="width:480px;" />
				    <col style="width:471px;" />
				</colgroup>
                    <tr>
                        <td style="width:480px;">
                        <div id="divTituloFijo" class="divTitulo" style="width:480px;" runat="server">
                        <table id='tblTituloFijo' style='width:480px;' cellpadding='0' cellspacing='0' border='0'>
                            <colgroup>
                               <col style='width:300px;' />
                               <col style='width:90px;' />
                               <col style='width:90px;' />
                            </colgroup>
                            <tr style='height:17px; vertical-align:middle;'>
                               <td rowspan="2">Grupo / Subgrupo / Concepto / Clase</td>
                               <td colspan="2">Total</td>
                            </tr>
                            <tr style='height:17px; vertical-align:top;'>
                                <td>Línea base</td>
                                <td>Real</td>
                            </tr>
                        </table>
                        </div>
                        </td>
                        <td style="width:471px;">
                        <div id="divTituloMovil" class="divTitulo" style="width:451px; overflow:hidden;" runat="server">
                        </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:480px;vertical-align:top;">
                            <div id="divBodyFijo" style="width:480px; height:400px; overflow:hidden;" runat="server"> <!--  onmousewheel="alert('sdfasdf');" -->
                                <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:480px; height:auto;">
                                <table id='tblBodyFijo' style='font-size:9pt; width:590px;' cellpadding='0' cellspacing='0' border='0'>
                                </table>
                                </div>
                            </div>
                        </td>
                        <td style="width:471px;vertical-align:top;">
                            <div id="divBodyMovil" style="width:467px; height:416px; overflow-x:auto; overflow-y:scroll;" runat="server" onscroll="setScroll()">
                                <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:1300px; height:auto;">
                                <table id='tblBodyMovil' style='font-size:9pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
                                </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                </eo:PageView>

                <eo:PageView id="PageView7" CssClass="PageView" runat="server">
                <!-- Pestaña 7 Observaciones -->
				<table style="width:940px; margin-left:10px; margin-top:10px;">
				<colgroup> 
				    <col style="width:940px;" />
				</colgroup>
				<tr>
				    <td>
                        <div id="divObservaciones" style="height: 400px; width:600px; margin-left: 170px; overflow-y:auto; overflow-x:hidden;">
	                    <ul id="ulObservaciones" class="chats">
		                    <!--<li class="in">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar_andoni.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <span class="name">Bob Nilson</span>
				                    <span class="datetime">
					                     25/07/2012 11:09
				                    </span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.<br /><br />
					                     Incluso es un texto largo que indicar importes varios: 145.550 &euro;<br /><br />
					                     Fin de la cita.
				                    </span>
			                    </div>
		                    </li>
		                    <li class="out">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar2.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <a href="#" class="name">Lisa Wong</a>
				                    <span class="datetime">at Jul 25, 2012 11:09</span><br />
				                    <a href="#" class="name">Bob Nilson</a>
				                    <span class="datetime">at Jul 27, 2012 16:25</span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
				                    </span>
			                    </div>
		                    </li>
		                    <li class="in">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar1.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <a href="#" class="name">Bob Nilson</a>
				                    <span class="datetime">
					                     at Jul 25, 2012 11:09
				                    </span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
				                    </span>
			                    </div>
		                    </li>
		                    <li class="out">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar3.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <a href="#" class="name">Richard Doe</a>
				                    <span class="datetime">
					                     at Jul 25, 2012 11:09
				                    </span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
				                    </span>
			                    </div>
		                    </li>
		                    <li class="in">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar3.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <a href="#" class="name">Richard Doe</a>
				                    <span class="datetime">
					                     at Jul 25, 2012 11:09
				                    </span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
				                    </span>
			                    </div>
		                    </li>
		                    <li class="out">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar1.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <a href="#" class="name">Bob Nilson</a>
				                    <span class="datetime">
					                     at Jul 25, 2012 11:09
				                    </span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
				                    </span>
			                    </div>
		                    </li>
		                    <li class="in">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar3.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <a href="#" class="name">Richard Doe</a>
				                    <span class="datetime">
					                     at Jul 25, 2012 11:09
				                    </span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
				                    </span>
			                    </div>
		                    </li>
		                    <li class="out">
			                    <img class="avatar img-responsive" alt="" src="../../../Images/avatar1.jpg">
			                    <div class="message">
				                    <span class="arrow">
				                    </span>
				                    <a href="#" class="name">Bob Nilson</a>
				                    <span class="datetime">
					                     at Jul 25, 2012 11:09
				                    </span>
				                    <span class="body">
					                     Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. sed diam nonummy nibh euismod tincidunt ut laoreet.
				                    </span>
			                    </div>
		                    </li>-->
	                    </ul>
                        </div>
				    </td>
				</tr>
				</table>
                <center>
                    <table id="tblBotones" style="margin-top:15px; text-align:left; width:100px; margin-left:10px;">
                        <colgroup>
                            <col style="width:110px;"/>
                        </colgroup>
                        <tr>
			                <td>
				                <button id="btnNuevo" type="button" onclick="addObservacion()" class="btnH25W85" runat="server" hidefocus="hidefocus" 
					                 onmouseover="se(this, 25);mostrarCursor(this);">
					                <img src="../../../images/imgComentario_add.png" /><span title="Nueva observación">Nueva</span>
				                </button>			
			                </td>
                        </tr>
                    </table>
                </center>
				
                </eo:PageView>
            </eo:MultiPage>
        </td>
    </tr>
</table>
<div id="divMeses" style="z-index: 9999; left: 18px; visibility: hidden; width: 190px; height: 500px; position: absolute; top: 190px; left: 420px">						
	<table class="tblborder" cellpadding="10" style="width:100%; text-align:center; background-color:#bcd4df" >
		<tr>
			<td align="center"><b><font size="3">Meses</font></b>
			</td>
		</tr>
	</table>
	<table class="texto tblborder" cellpadding="10" style="width:100%; text-align:center; background-color:#D8E5EB">
        <tr>
            <td>
                <div id="divCatalogoMeses" style="overflow: auto; overflow-x: hidden; width: 166px; height:240px">
                    <div style="background-image: url('../../../Images/imgFT16.gif'); background-repeat:repeat; width:150px; height:auto;">
                        <table id='tblMeses' style='width:150px; text-align:center;'>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">	
            <button id="btnCancelar" type="button" onclick="$I('divMeses').style.visibility = 'hidden';" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);" title="Cancelar sin seleccionar ningun mes">
                <img src="../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
            </td>
        </tr>	    
    </table>
</div>
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdLineaBase" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnFechaLineaBase" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnAutorLineaBase" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnMesReferencia" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnSWCambioLineaBase" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstado" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnPestanaAnterior" runat="server" style="visibility:hidden" Text="" />
<script type="text/javascript">
<%=strArrayMeses%>
</script>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<iframe id="iFrmDescarga" frameborder="0" name="iFrmDescarga" width="10px" height="10px" style="visibility:hidden" ></iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {

        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "excel":
                    {
                        bEnviar = false;
                        mostrarProcesando();
                        setTimeout("excel();", 20);
                        break;
                    }
                case "guia":
                    {
                        bEnviar = false;
                        mostrarGuia("ValorGanado.pdf");
                        break;
                    }
                case "eliminar":
                    {
                        bEnviar = false;
                        mostrarProcesando();
                        setTimeout("eliminar();", 20);
                        break;
                    }
            }
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) {
		    theform.submit();
		}
	}
	
    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
    	
-->
</script>
</asp:Content>

