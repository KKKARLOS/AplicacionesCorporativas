<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_FuncionesTecnico_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script language=Javascript>
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
    var nIDFun = 0;
    var nIDTec = 0;
    <%=strArrayTecFun %>
</script>
    <br />
    <table style="width: 500px; margin-left:5px">
        <tr>
            <td>
                <label id="lblNodo" style="width:399px" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
                <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList id="cboCR" runat="server" Width="400px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems="true"></asp:DropDownList>
                <asp:TextBox ID="txtDesNodo" style="width:400px;" Text="" readonly="true" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <center>
    <table style="width: 990px;text-align:left">
    <colgroup><col style="width:330px" /><col style="width:330px" /><col style="width:330px" /></colgroup>
    <tr>
    <td>
		<table style="width: 275px; height: 17px">
			<tr class="TBLINI" align="center">
				<td>Función</td>
			</tr>
		</table>
		<div style="overflow:auto; overflow-x:hidden; width:286px; height:450px">
		    <div id="divCatalogo" style='background-image:url(../../../../Images/imgFT16.gif); width:275px'>
		    <%=strTablaHtml %>
		    </div>
        </div>
		<table style="width: 275px; height: 17px">
			<tr class="TBLFIN">
				<td></td>
			</tr>
		</table>
        
    </td>
    <td>
		<table style="width: 275px; height: 17px">
			<tr class="TBLINI" align="center">
				<td>Profesionales por función</td>
			</tr>
		</table>
		<div style="overflow: auto; overflow-x: hidden; width: 286px; height:450px">
            <div style='background-image:url(../../../../Images/imgFT16.gif); width:275px'>
                <table id="tblTec" class='texto MANO' style='width: 275px;'>
                </table>
            </div>
        </div>
		<table style="width: 275px; height: 17px">
			<tr class="TBLFIN">
				<td></td>
			</tr>
		</table>
        
    </td>
    <td>
		<table style="width: 275px; height: 17px">
			<tr class="TBLINI" align="center">
				<td>Funciones por profesional</td>
			</tr>
		</table>
		<div style="overflow: auto; overflow-x: hidden; width: 286px; height:450px">
            <div style='background-image:url(../../../../Images/imgFT16.gif); width:275px'>
            <table id="tblFunTec" class='texto' style='width: 275px;'>
            </table>
            </div>
        </div>
		<table style="width: 275px; height: 17px">
			<tr class="TBLFIN">
				<td></td>
			</tr>
		</table>        
    </td>
    </tr>
    </table>
    </center>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        switch (strBoton) {
				case "nuevo": 
				{
                    bEnviar = false;
                    nuevo();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;
//					if (confirm("¿Estás conforme?")){
                        eliminar();
//                    }
					break;
				}
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
			}
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) theform.submit();
	}
	
    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
</script>
</asp:Content>

