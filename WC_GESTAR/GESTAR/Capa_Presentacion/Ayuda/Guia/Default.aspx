<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Ayuda_Guia_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<div style="Z-INDEX: -1; VISIBILITY: visible; position: absolute; width: 60px; top: 150px;  left: 30px ;height: 57px">
<asp:Image ID="imgAyuda" runat="server" Height="57" width="60" ImageUrl="~/images/imgAyudaContenido.gif" />
</div>

<div style="z-index: -1; visibility: visible; position: absolute; top: 280px; left: 400px; width: 109px; height: 160px">
<asp:Image ID="Image2" runat="server" ImageUrl="images/imgGestar21.gif" width="150" Height="175" onclick="mostrarAyuda(1);" style="cursor:pointer;" AlternateText="" />
</div>


</asp:Content>
<asp:Content ID="CPHDoPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

