<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Consultas_getCriterio_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de criterio</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    var nTipo = "<%=Request.QueryString["nTipo"]%>";
    var sNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO)%>";
-->
</script>
<table class="texto" style="width:980px;" cellpadding="5px">
<colgroup><col style="width:560px;" /><col style="width:40px;" /><col style="width:380px;" /></colgroup>
    <tr style="height:25px;">
        <td style="padding-bottom:2px; padding-right:25px; vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
        </td>
        <td>&nbsp;</td>
        <td style="padding-bottom:2px; padding-right:25px; vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
        </td>
    </tr>
    <tr>
        <td>
            <TABLE id="tblTitulo" style="WIDTH: 530px; HEIGHT: 17px;">
		    <colgroup>
		        <col style="width:20px" />
		        <col style="width:20px" />
		        <col style="width:20px" />
		        <col style="width:65px;" />
		        <col style="width:225px" />
		        <col style="width:180px" />
		    </colgroup>
                <TR class="TBLINI">
			        <TD></TD>
			        <TD></TD>
				    <TD colspan="2" align=right>
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
						    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1', event)"
						    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
				        <MAP name="img1">
				            <AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;Nº&nbsp;&nbsp;
				    </TD>
				    <td style="text-align:right; padding-right:10px;">
				        <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
				        <MAP name="img2">
				            <AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;Proyecto&nbsp;
			            <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
					        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2', event)"
					        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				    </td>
				    <td><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					        <MAP name="img3">
					            <AREA onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <AREA onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </MAP>&nbsp;Cliente&nbsp;
				            <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
						        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa3', event)"
						        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				    </td>
                </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 546px; height:430px" onscroll="scrollTablaProy()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:530px; height:auto;">
                <TABLE id="tblDatos" style="WIDTH: 530px; table-layout:fixed;" class="texto MAM" >
		        <colgroup>
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:65px" />
		            <col style="width:225px" />
			        <col style="width:180px" />
		        </colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 530px; HEIGHT: 17px;">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
        <td style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
        </td>
        <td>
            <TABLE id="tblAsignados" style="WIDTH: 350px; HEIGHT: 17px;">
                <TR class="TBLINI">
                    <td style="padding-left:3px">
                        Elementos seleccionados
                        &nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
					</TD>
                </TR>
            </TABLE>
            <DIV id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 366px; height:430px" onscroll="scrollTablaProyAsig()" target="true" onmouseover="setTarget(this);" caso="2">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:350px; height:auto;">
                <TABLE id="tblDatos2" style="WIDTH: 350px;" class="texto MM">
		        <colgroup>
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:65px" />
		            <col style="width:225px" />
		        </colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 350px; HEIGHT: 17px;">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
    </tr>
</table>
<center>
<table style="width:300px; margin-top:10px; text-align:center;">
	<tr>
		<td>
            <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
            </button>
		</td>
		<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	</tr>
</table>
</center>
<div class="clsDragWindow" id="DW" noWrap></div>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
