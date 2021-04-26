<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Calendarios_Default"  %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">

Ejemplos que "tiran" de la carpeta <u>PopCalendar2</u>, ya se ha modificado a PopCalendar
<br /><br />
Calendario en modo edición (Control Web): <asp:TextBox ID="txtFec1" style="width:60px; cursor:pointer" Text="01/12/2007" Calendar="oCal" onclick="mc(this);" onchange="activarGrabar();" readonly runat=server />
<br />
<br />
Calendario en modo lectura (Control Web): <asp:TextBox ID="txtFec2" style="width:60px;" Text="01/12/2007" Calendar="oCal" onclick="mc(this);" onchange="activarGrabar();" lectura=1 readonly runat=server />
<br />
<br />
Calendario sin goma de borrar (Control HTML): <input id='txtFec3' type='text' class='txtM' style='width:60px; cursor:pointer' value='01/12/2007' Calendar='oCal' onclick='mc(this);' onchange='activarGrabar();' goma=0 readonly />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

