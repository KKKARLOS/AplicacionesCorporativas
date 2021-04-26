<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_CONCEP_ESTRUC_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de elementos de la estructura</title>
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
-->
</script>
<center>
<table class="texto" width="100%" style="margin-left:10px; text-align:left;" cellpadding="5px">
    <colgroup><col style="width:370px;"/><col style="width:60px;"/><col style="width:370px;"/></colgroup>
    <tr style="height:30px;">
        <td>Nivel de Estructura&nbsp;&nbsp;
	        <asp:dropdownlist id="cboNivelEstru" runat="server" Width="200px" CssClass="combo" onChange="cargarElementosTipo(this.value)" AppendDataBoundItems=true>
	            <asp:ListItem Value="" Text=""></asp:ListItem>
            </asp:dropdownlist>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="padding-bottom:2px; padding-right:42px; vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
        </td>
        <td>&nbsp;</td>
        <td style="padding-bottom:2px; padding-right:42px; vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
        </td>
    </tr>
    <tr>
        <td><!-- Relación de Items -->
            <TABLE id="tblCatIni" style="WIDTH: 350px; HEIGHT: 17px;">
                <TR class="TBLINI">
                    <td style="padding-left:3px">
                        Denominación
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" />
				    </td>                    
                </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 366px; height:270px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px;; height:auto">
                <TABLE id="tblDatos" style="WIDTH: 350px;" class="texto MAM" >
                    <colgroup><col style="width:350px;" /></colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 350px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
        <td style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <TABLE id="tblAsignados" style="WIDTH: 350px; HEIGHT: 17px">
                <TR class="TBLINI">
                    <td style="padding-left:3px">
                        Elementos seleccionados
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" />
					</TD>
                </TR>
            </TABLE>
            <DIV id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 366px; height:270px" target="true" onmouseover="setTarget(this)" caso="2">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:350px; height:auto">
                <TABLE id="tblDatos2" style="WIDTH: 350px;" class="texto MM">
                    <colgroup><col style="width:350px;" /></colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 350px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
    </tr>
</table>
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
<input type="hidden" id="hdnNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnCualidad" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
