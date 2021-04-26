<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Consultas_getCriterio_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD runat=Server>
	<title>Selección de criterio</title>
	<meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<LINK href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</HEAD>
<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var nTipo = "<%=Request.QueryString["nTipo"]%>";
	-->
    </script>
    <table align="center" border="0" class="texto" width="100%" style="margin-left:10px;" cellpadding="5" cellspacing="0">
        <tr height="25px">
            <td width="47%" valign=bottom align=right style="padding-bottom:2px; padding-right:42px;">
                <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>
            <td width="6%">&nbsp;</td>
            <td width="47%" valign=bottom align=right style="padding-bottom:2px; padding-right:42px;">
                <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
            </td>
        </tr>
        <tr>
            <td width="47%">
                <TABLE id="tblTitulo" style="WIDTH: 350px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0"
                    border="0">
                    <TR class="TBLINI">
                        <td style="padding-left:3px">Denominación
                            &nbsp;
                            <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                        </TD>                    
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 366px; height:320px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:350px; height:auto;">
                    <TABLE id="tblDatos" style="WIDTH: 350px;" class="texto MAM" cellSpacing="0" cellspacing="0" border="0">
                        <colgroup><col style="width:347px;" /></colgroup>
                    </TABLE>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 350px; HEIGHT: 17px" cellSpacing="0"
                    border="0">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
            <td width="6%" valign=middle>
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td width="47%"><!-- Items asignados -->
                <table id="tblAsignados" style="WIDTH: 350px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" border="0">
                    <TR class="TBLINI">
                        <td style="padding-left:3px">
                            Elementos seleccionados
                            &nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
						</TD>
                    </TR>
                </table>
                <DIV id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 366px; height:320px" onscroll="scrollTablaProyAsig()" target="true" onmouseover="setTarget(this);" caso="2">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:350px; height:auto;">
                    <table id="tblDatos2" style="WIDTH: 350px;" class="texto MM" cellSpacing="0" border="0">
                        <colgroup><col style="width:347px;" /></colgroup>
                    </table>
                    </div>
                </DIV>
                <table style="WIDTH: 350px; HEIGHT: 17px" cellSpacing="0" border="0">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </table>
            </td>
        </tr>
    </table>
    <center>
    <table style="width:300px; margin-top:10px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td align="center">
                <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
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
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
</html>
