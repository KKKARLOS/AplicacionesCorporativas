<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Calendario_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Calculadora/Calculadora.ascx" TagName="Calculadora" TagPrefix="uccalc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<label onclick="getCalculadora();">Calculadora</label><br />
<img border="0" src="<% =Session["GVT_strServer"].ToString() %>Capa_Presentacion/UserControls/Calculadora/images/imgCalculadora50.gif" width="33" height="50" onclick="getCalculadora();" style="cursor:pointer;" /><br />
<input type="text" id="txtCal0" class="txtNumM" value="" onfocus="fn(this);ic(this.id);" /><br />
<input type="text" id="txtCal1" class="txtNumM" value="" onfocus="fn(this);ic(this.id);" /><br />
<input type="text" id="txtCal2" class="txtNumM" value="" onfocus="fn(this);ic(this.id);" /><br />
<input type="text" id="txtCal3" class="txtNumM" value="" onfocus="fn(this);ic(this.id);" /><br />
<input type="text" id="txtCal4" class="txtNumM" value="" onfocus="fn(this);ic(this.id);" /><br />
    <div id="divPendientes" title="Datos pendientes de cumplimentar" style="display:none; width:40px; height:400px;">
        Hola
    </div>   

<uccalc:Calculadora ID="Calculadora" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

