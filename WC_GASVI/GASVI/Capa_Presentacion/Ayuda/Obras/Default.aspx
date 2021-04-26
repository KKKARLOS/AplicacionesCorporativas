<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Ayuda_Requisitos_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<br />
<br />
<br />
<div style="text-align:center">
  <center>
  <table border="0" cellpadding="5" cellspacing="0" width="50%" style="border: 1 solid #83afc3">
    <tr>
      <td width="35%"><img border="0" src="<% =Session["GVT_strServer"].ToString() %>images/imgObras.gif" width="189" height="185"></td>
      <td width="65%" style="text-align:center"><b><font face="Verdana" color="#cc6699">Página
        en construcción</font></b><b><font face="Verdana" color="#cc6699">.</font></b><p style="text-align:center"><A HREF="javascript:history.back()"><B><FONT SIZE="-2"
       face="Verdana">Regresar</FONT></B></A>
</p>
      </td>
    </tr>
  </table>
  </center>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

