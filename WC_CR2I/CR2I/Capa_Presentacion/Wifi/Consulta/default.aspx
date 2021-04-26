<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Wifi.Consulta.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
	var strServer = "<%=Session["strServer"]%>";

//	var objPanel = $I("AreaTrabajo");
//	objPanel.style.backgroundImage = "url(../../../Images/imgWifiAlpha.gif)";
//	objPanel.style.backgroundRepeat= "no-repeat";
//	objPanel.style.backgroundPosition= "top left";
//	objPanel = null;
-->
	</script>
<div id="AreaTrabajo" style="height:100%;width:100%; padding: 0px;">
<br /><br />
<table id="tblFiltros" class="texto" style="width:870px; table-layout:fixed; margin-left:75px;" >
    <tbody>
        <tr>
            <td style='text-align:right;'><asp:Label ID="lblMostrarCerradas" runat="server" Text="Mostrar cerradas" /> <input type="checkbox" id="chkMostrarCerradas" class="check" onclick="MostrarInactivos();" style="margin-right:17px;" runat=server /></td>
        </tr>
        <tr>
            <td>
				<table style="width:850px; table-layout:fixed;" cellspacing="0px" border="0">
				    <colgroup>
				        <col style="width:295px" />
				        <col style="width:295px" />
				        <col style="width:100px" />
				        <col style="width:100px" />
				        <col style="width:60px" />
				    </colgroup>
					<tr class="TBLINI">
						<td style='padding-left:2px;'>Solicitante</td>
						<td>Interesado</td>
						<td>Desde</td>
						<td>Hasta</td>
						<td>Estado</td>
					</tr>
				</table>
				<div id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 866px; height:500px">
					<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:850px; ">
					<%=strTablaHTML%>
					</div>
				</div>
				<table style="width:850px; height:17px" cellspacing="0" border="0">
					<tr class="TBLFIN">
						<td></td>
					</tr>
				</table>
            </td>
        </tr>
    </tbody>
</table>
<asp:TextBox style="VISIBILITY: hidden" id="hdnNuevo" runat="server" Text=""></asp:TextBox>
<asp:TextBox id="hdnReserva" style="VISIBILITY: hidden" runat="server" Text=""></asp:TextBox>
<asp:TextBox id="hdnFecha" style="VISIBILITY: hidden" runat="server" Text=""></asp:TextBox>
<asp:TextBox id="hdnHora" style="VISIBILITY: hidden" runat="server" Text=""></asp:TextBox>
<input id="hdnErrores" type="hidden" value="<%=sErrores%>" />
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
		<!--
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	            //alert("strBoton: "+ strBoton);
	            switch (strBoton) {
						case "inicio": 
						{
						    bEnviar = false;
						    window.open("../../Default.aspx", "CR2I", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 365) + "px,left=" + eval(screen.availWidth / 2 - 510) + "px,width=1010px,height=705px");
						    break;
						}
						case "nuevo": 
						{
                            bEnviar = false;
                            nuevo();
						}
					}
				}
				var theform = document.forms[0];
				theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
				theform.__EVENTARGUMENT.value = eventArgument;
				if (bEnviar) theform.submit();
}
		-->
	</script>
</asp:Content>
