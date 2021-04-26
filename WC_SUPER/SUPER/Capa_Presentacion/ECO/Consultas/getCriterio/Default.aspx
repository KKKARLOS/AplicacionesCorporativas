<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Consultas_getCriterio_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
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
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">

    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    var nTipo = "<%=Request.QueryString["nTipo"]%>";

</script>
<table class="texto" style="margin-left:10px; width:790px;">
    <colgroup><col style="width:370px;" /><col style="width:50px;" /><col style="width:370px;" /></colgroup>
    <tr style="height:25px;">
        <td style="padding-bottom:2px; padding-right:20px; vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
        </td>
        <td>&nbsp;</td>
        <td style="padding-bottom:2px; padding-right:20px; vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
        </td>
    </tr>
    <tr>
        <td>
            <TABLE id="tblTitulo" style="WIDTH: 350px; HEIGHT: 17px;">
                <TR class="TBLINI">
                    <td style="padding-left:3px">Denominación
                        &nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                    </TD>                    
                </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 366px; height:320px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:350px; height:auto;">
                <TABLE id="tblDatos" style="WIDTH: 350px;" class="texto MAM">
                    <colgroup><col style="width:347px;" /></colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 350px; HEIGHT: 17px;">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
        <td  style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <table id="tblAsignados" style="WIDTH: 350px; HEIGHT: 17px;">
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
                <table id="tblDatos2" style="WIDTH: 350px;" class="texto MM">
                    <colgroup><col style="width:347px;" /></colgroup>
                </table>
                </div>
            </DIV>
            <table style="WIDTH: 350px; HEIGHT: 17px;">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </table>
        </td>
    </tr>
</table>
<center>
<table style="width:320px; margin-top:10px; text-align:center;">
	<tr>
		<td>
            <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" style="margin-left:12px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
            </button>
		</td>
		<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" style="margin-left:12px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
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
