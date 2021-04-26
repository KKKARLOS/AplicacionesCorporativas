<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_ValorGanado_CreacionLB_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title> ::: SUPER ::: - Creación de línea base</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="css/estilos.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()">
<form id="Form1" method="post" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<table style="width:990px; margin-left:10px; margin-top:10px; padding-top:3px; padding-right:3px; padding-bottom:3px; padding-left:3px;">
	<colgroup>
	    <col style="width:570px;" />
	    <col style="width:420px;" />
	</colgroup>
	    <tr>
	        <td>
            <fieldset id="flsLineas" style="width: 550px;">
			    <legend>Otras líneas base ya generadas</legend>   
                <table id="tblTituloLineas" style="width:530px; height:17px; margin-top:5px; margin-left:3px;">
                    <colgroup>
                        <col style="width:260px;" />
                        <col style="width:70px;" />
                        <col style="width:200px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td>Denominación</td>
                        <td>Creación</td>
                        <td>Autor</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="width:546px; height:50px; overflow:auto; overflow-x:hidden; margin-left:3px;" runat="server">
                    <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:530px; height:auto;">
                    <%=strTablaHTMLLineas%>
                    </div>
                </div>
                <table id="Table1" style="width:530px; height:17px; margin-left:3px; margin-bottom:3px;">
                    <tr class="TBLFIN">
                        <td>&nbsp;</td>
                    </tr>
	            </table>
			</fieldset>     
	        </td>
	        <td rowspan="3" style="vertical-align:middle; text-align:center; padding-top:20px;">
                <asp:CHART id="Chart1" runat="server" Palette="None" Width="400px" Height="350px" BorderDashStyle="Solid" 
                    BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Transparent" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation" BorderSkin-BorderWidth="0">
                    <titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14.25pt, style=Bold" ShadowOffset="3" Text="Evolución de costes" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                    </titles>
                    <legends>
                        <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
                    </legends>
                    <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="Transparent" BorderWidth="0" BorderColor="Transparent" PageColor="Transparent" BackColor="Transparent" />
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderWidth="0">
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
	        <td>
            <fieldset id="Fieldset1" style="width: 550px; margin-top:5px;">
			    <legend>Análisis de consistencia</legend>   
                <table id="tblConsistencia" style="width:530px; height:17px; margin-top:5px; margin-left:3px; margin-bottom:5px;" border="0">
                    <colgroup>
                        <col style="width:110px;" />
                        <col style="width:60px;" />
                        <col style="width:80px;" />
                        <col style="width:80px;" />
                        <col style="width:80px;" />
                        <col style="width:60px;" />
                        <col style="width:60px;" />
                    </colgroup>
                    <tr class="TBLINI" align="center" style="text-align:center;">
                        <td></td>
                        <td style="text-align:center;">PST</td>
                        <td style="text-align:center;" colspan="3">PGE</td>
                        <td style="text-align:center;">Indicador</td>
                        <td></td>
                    </tr>
                    <tr style="height:26px; background-color:#E6EEF2;">
                        <td style="text-align:left;">Jornadas</td>
                        <td id="cldJornadasPST" runat="server"></td>
                        <td id="cldJornadasPGE" runat="server" style="vertical-align:middle;"><label id="lblJornadasPGE" runat="server" style="vertical-align:middle; cursor:pointer;" for="rdbPGE_0" title="Dedicaciones de profesionales asignadas para la totalidad del proyecto (para todos los asignados al proyecto)"></label><input type="radio" id="rdbPGE_0" name="rdbPGE" value="1" style="vertical-align:middle; cursor:pointer;" checked="checked" onclick="setAnalisisJornadas()"></td>
                        <td id="cldJornadasESTPGE" runat="server" style="vertical-align:middle;"><label id="lblJornadasESTPGE" runat="server" style="vertical-align:middle; cursor:pointer;" for="rdbPGE_1" title="Estimación de jornadas PGE en base a consumos presupuestados (BAC_IAP) y coste medio de profesionales asignados al proyecto"></label><input type="radio" id="rdbPGE_1" name="rdbPGE" value="2" style="vertical-align:middle; cursor:pointer;" onclick="setAnalisisJornadas()"></td>
                        <td id="cldJornadasPGESJI" runat="server" style="vertical-align:middle;"><label id="lblJornadasPGESJI" runat="server" style="vertical-align:middle; cursor:pointer;" for="rdbPGE_2" title="Estimación de jornadas PGE en base a consumos presupuestados (BAC_IAP) y coste medio de jornadas incurridas en el proyecto"></label><input type="radio" id="rdbPGE_2" name="rdbPGE" value="3" style="vertical-align:middle; cursor:pointer;" onclick="setAnalisisJornadas()"></td>
                        <td id="cldJornadasIndi" runat="server"></td>
                        <td style="text-align:center"><img id="imgSemJornadas" src="../../../../Images/imgSemaforo.gif" style="height:20px; width:53px;" runat="server" /></td>
                    </tr>
                    <tr style="height:26px; background-color:#FFFFFF;">
                        <td style="text-align:left;">Tareas con previsión</td>
                        <td id="cldTareasPST" runat="server"></td>
                        <td colspan="3"></td>
                        <td id="cldTareasIndi" runat="server"></td>
                        <td style="text-align:center"><img id="imgSemTareas" src="../../../../Images/imgSemaforo.gif" style="height:20px; width:53px;" runat="server" /></td>
                    </tr>
                    <tr class="TBLFIN">
                        <td colspan="7">&nbsp;</td>
                    </tr>
	            </table>
	            <label style="font-weight:bold; margin-left:4px; width:110px; font-size:10pt;">Recomendación: </label><label id="lblRecomendacion" style="width:405px; font-size:10pt;" runat="server"></label>
			</fieldset>     
	        </td>
	    </tr>
	    <tr>
	        <td>
            <fieldset id="Fieldset2" style="width: 550px; margin-top:5px;">
			    <legend>Desglose de consumos planificados - Valor ganado</legend>   
                <table id="tblDesglose" style="width:530px; margin-top:5px; margin-left:3px; margin-bottom:5px;">
                <colgroup>
                    <col style="width:210px;" />
                    <col style="width:100px;" />
                    <col style="width:220px;" />
                </colgroup>
                    <tr>
                        <td colspan="3">Vinculado al avance técnico en PST:</td>
                    </tr>
                    <tr>
                        <td style="padding-left:30px;">Por consumos IAP</td>
                        <td colspan="2"><asp:TextBox id="txtConsumoIAP" runat="server" style="width:90px;" SkinID="Numero" readonly="true" Text="" /></td>
                    </tr>
                    <tr>
                        <td style="padding-left:30px;">Por otros consumos (viajes, etc)</td>
                        <td colspan="2"><asp:TextBox id="txtConsumoOCO" runat="server" style="width:90px;" SkinID="Numero" readonly="true" Text="" /></td>
                    </tr>
                    <tr>
                        <td colspan="3">No vinculado al avance técnico en PST: </td>
                    </tr>
                    <tr>
                        <td style="padding-left:30px;">Subcontratación:</td>
                        <td colspan="2"><asp:TextBox id="txtSubcontratacion" style="width:90px;" runat="server" SkinID="Numero" readonly="true" Text="" /></td>
                    </tr>
                    <tr>
                        <td style="padding-left:30px;">Compras:</td>
                        <td colspan="2"><asp:TextBox id="txtCompras" style="width:90px;" runat="server" SkinID="Numero" readonly="true" Text="" /></td>
                    </tr>
                    <tr>
                        <td><b>TOTAL</b></td>
                        <td><asp:TextBox id="txtTotal" style="width:90px;" runat="server" SkinID="Numero" readonly="true" Text="" /></td>
                        <td style="text-align:right;"><span style="vertical-align:bottom;">* Importes en <label id="lblImportes" runat="server"></label></span></td>
                    </tr>
	            </table>
			</fieldset>     
	        </td>
	    </tr>
		<tr>
			<td colspan="2" align="center">
                <fieldset id="Fieldset3" style="width: 340px; margin-top:15px;">
			        <legend>Denominación línea base a crear</legend>   
					<input type="text" class="txtM" id="txtString" style="width:300px; margin-top:5px; margin-bottom: 5px;" maxlength="50" onKeyPress="javascript:if(event.keyCode==13){aceptar();event.keyCode=0;}" />
				</fieldset>
			</td>
		</tr>
	</table>
	<center>
    <table style="width:250px;margin-top:10px;">
	    <tr>
        	<td align="center">
                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
        	<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
			</td>
	    </tr>
    </table>
    </center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
</form>
</body>
</html>

