<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Ayuda_Contacto_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<div style="Z-INDEX: 1; VISIBILITY: visible; WIDTH: 125px; POSITION: absolute; TOP: 180px;  LEFT: 420px ;HEIGHT: 121px"><asp:Image ID="imgAqui" runat="server" Height="121" Width="125" ImageUrl="~/images/imgAqui.gif" /></DIV>
<br /><br /><br />
<center>
<div style="width:500px; text-align:left" class="texto" >
Desde esta opción, Ud. puede enviar un correo al administrador de GASVI<sup>.net</sup>. En caso de tratarse de un error de aplicación, le rogamos anexe un pantallazo del error producido.<br /><br />
<br /><br />Para abrir el correo, pinche <a href="mailto:cau-def@ibermatica.com" class="enlace">aquí</a>. 
</div>
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

