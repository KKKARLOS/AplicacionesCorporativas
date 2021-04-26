<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_DialogoAlertas_CatalogoPendientes_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language="javascript" type="text/javascript">
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
</script>
<button id="btnCarrusel" style="margin-left:5px; display:inline-block; position:absolute; top: 102px; left:815px;" type="button" onclick="goCarrusel();" class="btnH25W150" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../../Images/botones/imgCarrusel.gif" /><span>Acceso al Carrusel</span>
</button>
<table id="Table1" style="width:960px; margin-top:20px;">
	<tr>
		<td>
			<table id="tblTitulo" style="width:960px; height:17px;" cellpadding='0' cellspacing='0' border='0'>
			    <colgroup>
	                <col style='width:60px;' />
	                <col style='width:240px;' />
	                <col style='width:210px;' />
	                <col style='width:120px;' />
	                <col style='width:250px;' />
	                <col style='width:80px;' />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td style='text-align:right; padding-right:5px;'><img id="imgFA1" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA1">
                        <map name="imgEA1">
		                    <area onclick="ot('tblDatos', 0, 0, 'num', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 0, 1, 'num', '');" shape="rect" coords="0,6,6,11">
	                    </map>Nº</td>
					<td><img id="imgFA2" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA2">
                        <map name="imgEA2">
		                    <area onclick="ot('tblDatos', 1, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 1, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Proyecto</td>
					<td><img id="imgFA3" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                        <map name="imgEA3">
		                    <area onclick="ot('tblDatos', 2, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 2, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Asunto</td>
					<td><img id="imgFA4" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA4">
                        <map name="imgEA4">
		                    <area onclick="ot('tblDatos', 3, 0, 'mes', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 3, 1, 'mes', '');" shape="rect" coords="0,6,6,11">
	                    </map>Mes de cierre</td>
					<td><img id="imgFA5" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA5">
                        <map name="imgEA5">
		                    <area onclick="ot('tblDatos', 4, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 4, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Estado</td>
					<td title="Fecha límite de respuesta"><img id="imgFA6" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA6">
                        <map name="imgEA6">
		                    <area onclick="ot('tblDatos', 5, 0, 'fec', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 5, 1, 'fec', '');" shape="rect" coords="0,6,6,11">
	                    </map>F.L.R.</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 976px; height: 520px;">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px">
                    <%//=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:960px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

