<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="CVObras.aspx.cs" Inherits="CVObras" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<br />
<br />
<br />
<div align="center">
  <center>
  <table border="0" cellpadding="5" cellspacing="0" width="50%" style="border: 1 solid #83afc3">
    <tr>
      <td width="35%"><img border="0" src="<% =Session["strServer"].ToString() %>images/imgObras.gif" width="189" height="185"></td>
      <td width="65%" align="center"><b><font face="Verdana" color="#cc6699">Curriculum en construcción.</font></b>
      </td>
    </tr>
  </table>
  </center>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

