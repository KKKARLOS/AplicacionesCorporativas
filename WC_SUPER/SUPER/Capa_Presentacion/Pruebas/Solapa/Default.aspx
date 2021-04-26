<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Solapa_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<div id="divGlobal" style=" width:700px; height:500px; position:absolute; top:95px; left:0px; clip:rect(auto 73 76 auto); background-color: #D8E5EB;">

	<div id="divImg" style="position:absolute; top:0px; left:0px; z-index:100;" onclick="mostrar()" title="Mostrar/ocultar información">
	<img id="imgSol" src="../../../Images/imgSolapa2.gif" border="0" style="cursor:pointer; "/>
	</div>
</div>
<!--
	<center>
	<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>
	<table class="texto" align="center" cellpadding="3" cellspacing="0" border="0" width="350px">
	<tr>
		<td align="center"><input type="button" class="boton" id="btnMostrar" name="btnMostrar" value="Mostrar" style="width:60px; " onclick="mostrarDIV();"></td>
		<td align="center"><input type="button" class="boton" id="btnOcultar" name="btnOcultar" value="Ocultar" style="width:60px; " onclick="ocultarDIV();"></td>
	</tr>
	</table>
	</center>
-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

