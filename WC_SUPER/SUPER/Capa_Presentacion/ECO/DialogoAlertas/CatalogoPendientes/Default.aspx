<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_DialogoAlertas_CatalogoPendientes_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var bEsInterlocutor = <%=sEsInterlocutor %>; 
    var bEsGestor = <%=sEsGestor %>; 
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
</script>
<button id="btnCarruselInt" style="margin-left:5px; display:inline-block; position:absolute; visibility:hidden; top: 104px; left:815px;" type="button" onclick="goCarrusel(0);" class="btnH25W150" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../../Images/botones/imgCarrusel.gif" /><span>Acceso al Carrusel</span>
</button>
<button id="btnCarruselGes" style="margin-left:5px; display:inline-block; position:absolute; visibility:hidden; top: 404px; left:815px;" type="button" onclick="goCarrusel(1);" class="btnH25W150" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../../Images/botones/imgCarrusel.gif" /><span>Acceso al Carrusel</span>
</button>
<div id="divUsuario" style="width:990px; height:280px; display:block; margin-bottom:20px; margin-top:10px;" runat="server">
    <label id="lblUsuario" class="texto" style="margin-left:3px; font-size:9pt; font-weight:bold; width:964px; border-bottom: solid 1px #acabab; padding-bottom:3px; margin-bottom:3px;">Mis diálogos pendientes como interlocutor<%=(Session["SEXOUSUARIO"].ToString() == "M")?"a":""%></label>
    <table id="tblTitulo" style="width: 970px; height: 17px; margin-top:3px;">
        <colgroup>
        <col style="width:60px;" />
        <col style="width:240px" />
        <col style="width:200px;" />
        <col style="width:200px;" />
        <col style="width:200px;" />
        <col style="width:70px;" />
        </colgroup>
        <tr class="TBLINI">
            <td style="text-align:right; padding-right:5px;"><img id="imgFA1" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA1">
                <map name="imgEA1">
		            <area onclick="ot('tblDatosUsuario', 0, 0, 'num', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosUsuario', 0, 1, 'num', '');" shape="rect" coords="0,6,6,11">
	            </map>Nº</td>
            <td><img id="imgFA2" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA2">
                <map name="imgEA2">
		            <area onclick="ot('tblDatosUsuario', 1, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosUsuario', 1, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Proyecto</td>
            <td><img id="imgFA3" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                <map name="imgEA3">
		            <area onclick="ot('tblDatosUsuario', 2, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosUsuario', 2, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Cliente</td>
            <td><img id="imgFA4" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA4">
                <map name="imgEA4">
		            <area onclick="ot('tblDatosUsuario', 3, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosUsuario', 3, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Asunto diálogo</td>
            <td><img id="imgFA5" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA5">
                <map name="imgEA5">
		            <area onclick="ot('tblDatosUsuario', 4, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosUsuario', 4, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Estado</td>
            <td title="Fecha límite de respuesta"><img id="imgFA6" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA6">
                <map name="imgEA6">
		            <area onclick="ot('tblDatosUsuario', 5, 0, 'fec', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosUsuario', 5, 1, 'fec', '');" shape="rect" coords="0,6,6,11">
	            </map>F.L.R.</td>
        </tr>
    </table>
    <div id="divCatalogoUsuario" style="overflow:auto; overflow-x:hidden; width: 986px; height:220px" runat="server">
        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:970px">
            <%=strHTMLTablaUsuario %>
        </div>
    </div>
    <table id="tblResultado" style="width: 970px; height: 17px; margin-bottom: 3px;">
        <tr class="TBLFIN">
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
<div id="divGestor" style="width:990px; height:280px; display:block; margin-top:10px;" runat="server">
    <label id="Label1" class="texto" style="margin-left:3px; font-size:9pt; font-weight:bold; width:964px; border-bottom: solid 1px #acabab; padding-bottom:3px; margin-bottom:3px;">
        Mis diálogos pendientes como gestor<%=(Session["SEXOUSUARIO"].ToString() == "M")?"a":""%>
    </label>
    <table id="Table1" style="width: 970px; height: 17px; margin-top:3px;">
        <colgroup>
        <col style="width:60px;" />
        <col style="width:240px" />
        <col style="width:160px;" />
        <col style="width:160px;" />
        <col style="width:160px;" />
        <col style="width:120px;" />
        <col style="width:70px;" />
        </colgroup>
        <tr class="TBLINI">
            <td style="text-align:right; padding-right:5px;"><img id="imgFB1" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEB1">
                <map name="imgEB1">
		            <area onclick="ot('tblDatosGestor', 0, 0, 'num', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosGestor', 0, 1, 'num', '');" shape="rect" coords="0,6,6,11">
	            </map>Nº</td>
            <td><img id="imgFB2" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEB2">
                <map name="imgEB2">
		            <area onclick="ot('tblDatosGestor', 1, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosGestor', 1, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Proyecto</td>
            <td><img id="imgFB3" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEB3">
                <map name="imgEB3">
		            <area onclick="ot('tblDatosGestor', 2, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosGestor', 2, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Responsable</td>
            <td><img id="imgFB4" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEB4">
                <map name="imgEB4">
		            <area onclick="ot('tblDatosGestor', 3, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosGestor', 3, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Cliente</td>
            <td><img id="imgFB5" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEB5">
                <map name="imgEB5">
		            <area onclick="ot('tblDatosGestor', 4, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosGestor', 4, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Asunto diálogo</td>
            <td><img id="imgFB6" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEB6">
                <map name="imgEB6">
		            <area onclick="ot('tblDatosGestor', 5, 0, '', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosGestor', 5, 1, '', '');" shape="rect" coords="0,6,6,11">
	            </map>Estado</td>
            <td title="Fecha de última respuesta"><img id="imgFB7" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEB7">
                <map name="imgEB7">
		            <area onclick="ot('tblDatosGestor', 6, 0, 'fec', '');" shape="rect" coords="0,0,6,5">
		            <area onclick="ot('tblDatosGestor', 6, 1, 'fec', '');" shape="rect" coords="0,6,6,11">
	            </map>F.U.R.</td>
        </tr>
    </table>
    <div id="divCatalogoGestor" style="overflow:auto; overflow-x:hidden; width: 986px; height:220px" runat="server">
        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:970px">
            <%=strHTMLTablaGestor %>
        </div>
    </div>
    <table id="Table2" style="width: 970px; height: 17px; margin-bottom: 3px;">
        <tr class="TBLFIN">
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

