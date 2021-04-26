<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_ArbolTabla_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language=javascript>
    var nNEAux = <%= nNE.ToString() %>;
</script>
<br />
&nbsp;&nbsp;
<img id="imgNE1" src='../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"><img id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);"><img id="imgNE4" src='../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);"><img id="imgNE5" src='../../../images/imgNE5off.gif' class="ne" onclick="setNE(5);">
<br /><br />
<DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 1000px; height:520px;" runat=server>

</DIV>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

