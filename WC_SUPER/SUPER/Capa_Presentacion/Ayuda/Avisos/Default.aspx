<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administradores" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	<!--
	var sMensajeMMOFF = "<%=sMensajeMMOFF %>"; 	
	-->
	</script>
<center>

</center>
    
<div class="clsDragWindow" id="DW" noWrap></div>
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
-->
</script>
</asp:Content>

