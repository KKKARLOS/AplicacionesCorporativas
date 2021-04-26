<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_Agenda_Consulta_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
<!--
	var aFechas = new Array();
	var nRecurso = "<%=nRecurso%>";
	var strHora = "<%=strHora%>";
	var strSalas = "<%=strSalas%>";
	var strFechaHoy = "<%=System.DateTime.Today.ToShortDateString()%>";
	var sFechaAux = "<%=Request.QueryString["sFechaAux"]%>";
	var nScrollTop = "<%=Request.QueryString["nScrollTop"]%>";
	var aSemLab = "<%=Session["aSemLab"]%>".split(",");
	var aDiaFes = "<%=strFestivos%>".split(",");
	//var aDiaFes = "<%//=Request.QueryString["aDiaFes"]%>".split(",");
-->
    </script>
<center>
<table class="texto" style="OVERFLOW: auto; WIDTH: 55px; POSITION: absolute; TOP: 132px; LEFT: 5px;" border="0" cellspacing="0" cellpadding="3px">
  <tr>
	<td id="tblAnterior" width="24px"><img name="btnAntReg" onclick="semanaAnterior()" style="cursor:pointer;" border="0" src="../../../../images/btnAntRegOn.gif" width="24px" height="20px" title="Semana anterior"/></td>
	<td id="tblSiguiente" width="24px"><img name="btnSigReg" onclick="semanaSiguiente()" style="cursor:pointer;" border="0" src="../../../../images/btnSigRegOn.gif" width="24px" height="20px" title="Siguiente semana"/></td>
  </tr>
</table>
</center>
<table id="tblLiterales" style="OVERFLOW: auto; WIDTH: 889px; POSITION: absolute; TOP: 130px; LEFT: 66px;">
  <tr style="font-weight:bold; height:17px; text-align:center;"> 
    <td style="width:127px;">Lunes</td>
    <td style="width:127px;">Martes</td>
    <td style="width:127px;">Miércoles</td>
    <td style="width:127px;">Jueves</td>
    <td style="width:127px;">Viernes</td>
    <td style="width:127px;">Sábado</td>
    <td style="width:127px;">Domingo</td>
  </tr>
</table>
<table id="tblTitulo" class="title2" style="OVERFLOW: auto; WIDTH: 889px; POSITION: absolute; TOP: 147px; LEFT: 72px; table-layout:fixed; border-collapse: collapse;">
  <tr style="height:17px;"> 
    <td id="cldL" style="width:127px; border-right:solid 1px gray;"></td>
    <td id="cldM" style="width:127px; border-right:solid 1px gray;"></td>
    <td id="cldX" style="width:127px; border-right:solid 1px gray;"></td>
    <td id="cldJ" style="width:127px; border-right:solid 1px gray;"></td>
    <td id="cldV" style="width:127px; border-right:solid 1px gray;"></td>
    <td id="cldS" style="width:127px; border-right:solid 1px gray;"></td>
    <td id="cldD" style="width:127px;"></td>
  </tr>
</table>
	<div id="divContenido" style="OVERFLOW: auto; WIDTH: 963px; POSITION: absolute; TOP: 165px; LEFT: 35px; height: 505px;">
        <div id="ZZ">
		    <asp:Table id="tblCal" runat="server" EnableViewState="False"></asp:Table>
		</div>
    </div>
<table id="tblResultado" class="title2" style="OVERFLOW: auto; BORDER-COLLAPSE: collapse; WIDTH: 889px; POSITION: absolute; TOP: 670px; LEFT: 72px;  table-layout:fixed; border-collapse: collapse;">
  <tr style="height:17px;"> 
    <td id="cldTotL" style="width:127px; border-right:solid 1px gray;" runat="server">0</td>
    <td id="cldTotM" style="width:127px; border-right:solid 1px gray;" runat="server">0</td>
    <td id="cldTotX" style="width:127px; border-right:solid 1px gray;" runat="server">0</td>
    <td id="cldTotJ" style="width:127px; border-right:solid 1px gray;" runat="server">0</td>
    <td id="cldTotV" style="width:127px; border-right:solid 1px gray;" runat="server">0</td>
    <td id="cldTotS" style="width:127px; border-right:solid 1px gray;" runat="server">0</td>
    <td id="cldTotD" style="width:127px;" runat="server">0</td>
  </tr>
</table>
	<asp:TextBox id="hdnFecha" style="VISIBILITY: hidden" runat="server" Text=""></asp:TextBox>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

