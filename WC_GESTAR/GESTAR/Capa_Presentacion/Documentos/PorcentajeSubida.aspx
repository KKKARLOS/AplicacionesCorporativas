<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PorcentajeSubida.aspx.cs" Inherits="GESTAR.PorcentajeSubida" %>
<%@ Register TagPrefix="upload" namespace="FileUpload" Assembly="FileUpload" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<META http-equiv="Refresh" content="1">
	<title>Transfiriendo...</title>
	<style>
    .texto
    {
        FONT-WEIGHT: normal;
        FONT-SIZE: 11px;
        COLOR: #000000;
        FONT-FAMILY: Arial, Helvetica, sans-serif;
        TEXT-DECORATION: none;
    }
	</style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="235px" class="texto" cellpadding="3" cellspacing="3">
			    <colgroup><col width="130px" /><col width="105px" /></colgroup>
				<tr>
					<td>Tamaño:</td>
					<td><asp:literal id="littotallen" Runat="server"></asp:literal></td>
				</tr>
				<tr>
					<td>Transferido:</td>
					<td><asp:literal id="litcontent" Runat="server"></asp:literal></td>
				</tr>
				<tr>
					<td>Velocidad:</td>
					<td><asp:literal id="littasatrans" Runat="server"></asp:literal></td>
				</tr>
				<tr>
					<td>Tiempo estimado:</td>
					<td><asp:literal id="littiempoest" Runat="server"></asp:literal></td>
				</tr>
				<tr>
					<td colspan="2">
					<fieldset style="margin-top:20px; width:225px;">
						<upload:progressbar id="objBarraProg" ForeColor="#50849f" width="225" height="20" runat="Server" />
				    </fieldset>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
