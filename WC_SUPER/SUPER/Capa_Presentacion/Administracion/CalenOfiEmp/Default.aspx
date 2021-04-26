<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_CalenOfiEmp_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
     <center>
    <table style="width: 950px;text-align:left;">
        <tr>
            <td width="315px">
                &nbsp;Empresa<br />
                <asp:DropDownList ID="cboEmpresa" style="width:300px;" onChange="buscar();" runat="server">
                <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                </asp:DropDownList>
                </td>
            <td width="315px">
                &nbsp;Oficina<br />
                <asp:DropDownList ID="cboOficina" style="width:300px;" onChange="buscar();" runat="server">
                <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="315px">
                &nbsp;Empleados<br />
                <asp:DropDownList ID="cboEmpleados" style="width:200px;" onChange="buscar();" runat="server">
                <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                <asp:ListItem Value="1" Text="Empresa oficina con empleados" Selected="true"></asp:ListItem>
                <asp:ListItem Value="2" Text="Empresa oficina sin empleados"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table style="width:966px;text-align:left;">
    <tr>
    <td>
		<table style="width: 950px; height: 17px">
		    <colgroup><col style='width:300px;' /><col style='width:300px' /><col style='width:300px' /><col style='width:50px;' /></colgroup>
			<tr class="TBLINI">
				<td>&nbsp;Empresa</td>
				<td>Oficina</td>
				<td>Calendario</td>
				<td><label title="Número de empleados en esta empresa y oficina">Nº emp.</label></td>
			</tr>
		</table>
		<div id="divCatalogo" style="OVERFLOW: auto; width: 966px; height:464px">
		    <div style='background-image:url(../../../Images/imgFT16.gif); width:950px;'>
                <%=strTablaHtml %>
            </div>
        </div>
		<table style="width: 950px; height: 17px">
			<tr class="TBLFIN">
				<td></td>
			</tr>
		</table>
        
    </td>
    </tr>
    </table>
    </center>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
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

