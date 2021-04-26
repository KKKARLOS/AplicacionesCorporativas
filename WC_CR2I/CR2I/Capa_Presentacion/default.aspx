<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
var strServer = "<%=Session["strServer"]%>";
function init(){}
function unload(){}
function mostrarGuia(){
    try{
        window.open(strServer +"Capa_Presentacion/Ayuda/Trabajo colaborativo.pdf","", "resizable=yes,status=no,scrollbars=no,menubar=no,top="+ eval(screen.availHeight/2-384)+"px,left="+ eval(screen.availWidth/2-512) +"px,width=1010px,height=705px");	
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el archivo guía", e.message);
    }
}
</script>
<br /><br /><br />
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <colgroup>
        <col style="width:15%" />
        <col style="width:30%" />
        <col style="width:10%" />
        <col style="width:30%" />
        <col style="width:15%" />
    </colgroup>
	<tr>
		<td colspan="5">&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>
			<asp:ImageButton id="imgSala" runat="server" ImageUrl="../Images/imgSalaReunion.gif"></asp:ImageButton>
		</td>
		<td>&nbsp;</td>
		<td>
			<asp:ImageButton id="imgVideo" runat="server" ImageUrl="../Images/imgVideoconferencia2.gif"></asp:ImageButton>
        </td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td colspan="5">&nbsp;<br /><br /></td>
	</tr>
		<td>&nbsp;</td>
		<td colspan="2" style="vertical-align:bottom;">
			<asp:ImageButton id="imgTelerreunion" runat="server" ImageUrl="../Images/imgWebex.gif" style="vertical-align:bottom;"></asp:ImageButton>
			<div id="divWebex" runat="server">
                <img src="../Images/imgInterrogacion.gif" style="cursor:pointer;vertical-align:bottom;" onclick="mostrarGuia()" />&nbsp;
                <span class="enlace" onclick="mostrarGuia()" style="vertical-align:bottom;">Introducción</span>
            </div>
		</td>
		<td style="vertical-align:bottom;">
		    <asp:ImageButton id="imgWifi" runat="server" ImageUrl="../Images/imgWifi.gif" style="vertical-align:bottom;"></asp:ImageButton></td>
	    <td>&nbsp;</td>
</table>
<input id="hdnErrores" type="hidden" value="<%=sErrores%>" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
		function __doPostBack(eventTarget, eventArgument) {
			var bEnviar = true;
			if (eventTarget.split("$")[2] == "Botonera") {
			    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
				switch (strBoton){
					case "salir": 
					{
						bEnviar = false;
						window.close();
						break;
					}
				}
			}
			var theform = document.forms[0];
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.__EVENTARGUMENT.value = eventArgument;
			if (bEnviar) theform.submit();
        }
	</script>
</asp:Content>
