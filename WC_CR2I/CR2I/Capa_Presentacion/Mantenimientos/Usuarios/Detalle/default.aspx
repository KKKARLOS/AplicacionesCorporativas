<%@ Reference Page="~/capa_presentacion/default.aspx" %>
<%@ Page language="c#" Inherits="CR2I.Capa_Presentacion.Mantenimientos.Usuarios.Detalle.Default" CodeFile="Default.aspx.cs" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle de usuario</title>
    <script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />		
</head>
<body class="FondoBody" style=" overflow: auto" onbeforeunload="end();" onload="init();">
<form id="Form1" method="post" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
	var strServer = "<%=Session["strServer"]%>";
</script>
<br /><br /><br />
<table class="texto" style=" z-index:101; margin-left:8px; width:480px; border-collapse:collapse; height:136px" cellpadding="5" border="0">
    <colgroup><col style="width:90px;" /><col style="width:140px;" /><col style="width:120px;" /><col style="width:130px;" /></colgroup>
	<tbody>
		<tr>
			<td>Usuario</td>
			<td colspan="3">
				<asp:TextBox id="txtProfesional" runat="server" Height="16px" Width="280px" ReadOnly="True"></asp:TextBox>
				&nbsp;
				<asp:Label id="lblQEQ" SkinID="enlace" runat="server" Visible="True" onclick="mostrarQEQ()">QEQ</asp:Label>
		    </td>
		</tr>
		<tr>
			<td>Acceso CR2I</td>
			<td colspan="3">
				<asp:DropDownList ID="cboCR2I" Runat="server" style="width:100px;" CssClass="combo" >
					<asp:ListItem Value="">Nulo</asp:ListItem>
					<asp:ListItem Value="B">Básico</asp:ListItem>
					<asp:ListItem Value="A">Administrador</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>Sala de reuniones</td>
			<td>
				<asp:DropDownList ID="cboReunion" Runat="server" style="width:100px;" CssClass="combo">
					<asp:ListItem Value="">Nulo</asp:ListItem>
					<asp:ListItem Value="B">Básico</asp:ListItem>
					<asp:ListItem Value="E">Extendido</asp:ListItem>
				</asp:DropDownList>
			</td>
            <td>Sala telerreuniones</td>
			<td>
				<asp:DropDownList ID="cboWebex" Runat="server" style="width:100px;" CssClass="combo">
					<asp:ListItem Value="">Nulo</asp:ListItem>
					<asp:ListItem Value="B">Básico</asp:ListItem>
					<asp:ListItem Value="E">Extendido</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>Sala de video</td>
			<td>
				<asp:DropDownList ID="cboVideo" Runat="server" style="width:100px;" CssClass="combo" >
					<asp:ListItem Value="">Nulo</asp:ListItem>
					<asp:ListItem Value="B">Básico</asp:ListItem>
					<asp:ListItem Value="E">Extendido</asp:ListItem>
				</asp:DropDownList>
			</td>
            <td>Conexiones WIFI</td>
			<td>
				<asp:DropDownList ID="cboWifi" Runat="server" style="width:100px;" CssClass="combo" >
					<asp:ListItem Value="">Nulo</asp:ListItem>
					<asp:ListItem Value="B">Básico</asp:ListItem>
					<asp:ListItem Value="E">Extendido</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr style="height:20px;">
			<td colspan="4"></td>
		</tr>
		<tr>
			<td colspan="4">
				<table id="tblResultado" style="width:60%; margin-left:25%;" border="0">
					<tr>
						<td>
                            <button id="btnGrabar" type="button" onclick="grabar()" style="width:85px;">
                                <span><img src="../../../../images/Botones/imgGrabar.gif" />&nbsp;Grabar</span>
                            </button>    
						</td>
						<td>
                            <button id="btnCancelar" type="button" onclick="cerrarVentana()" style="width:85px;">
                                <span><img src="../../../../images/Botones/imgCancelar.gif" />&nbsp;Cancelar</span>
                            </button>    
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</tbody>
</table>
<input id="hdnErrores" type="hidden" value="" runat="server" />
<asp:TextBox id="txtCIP" runat="server" style=" visibility:hidden"></asp:TextBox>
</form>
</body>
</html>
