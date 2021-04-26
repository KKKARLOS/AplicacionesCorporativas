<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_SelNodo_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de C.R.</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
</script>
<br />
<br />
<table style="margin-left:10px;text-align:left; width:800px;">
    <colgroup><col style="width:370px;" /><col style="width:60px;" /><col style="width:370px;" /></colgroup>
    <tr>
        <td><!-- Relación de nodos activos -->
            <table style="width: 350px; height: 17px">
                <TR class="TBLINI">
                    <td style="padding-left:3px">Denominación</td>
                </TR>
            </table>
            <div id="divCatalogo" style="overflow: auto; width: 366px; height:320px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px">
                    <%=strTablaHtml%>                    
                </div>
            </div>
            <table style="width: 350px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:middle; padding-left:10px;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
        </td>
        <td><!-- Nodos asignados -->
            <table id="tblTituloAsignados" style="width: 350px; height: 17px">
                <tr class="TBLINI">
                    <td style="padding-left:3px">
					    <label id="lblNodo" runat="server" > Nodos a asignar</label>
					</td>
                </tr>
            </table>
            <div id="divCatalogo2" style="OVERFLOW: auto; width: 366px; height:320px" target="true" onmouseover="setTarget(this);" caso="2">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:350px">
                    <table id="tblDatos2" style="width: 350px;" class="texto MM">
                        <colgroup><col style="width:350px;" /></colgroup>
                    </table>
                </div>
            </div>
            <table style="width: 350px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<center>
<table style="margin-top:15px; width:220px;">
	<tr>
		<td>
			<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span>
			</button>								
		</td>
		<td>
			<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
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
