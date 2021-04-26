<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
     <center>
    <table style="width: 600px;text-align:left">
    <colgroup>
        <col style="width:200px;" />
        <col style="width:200px;" />
        <col style="width:200px;"/>
    </colgroup>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td>Apellido1</td>
            <td>Apellido2</td>
            <td>Nombre</td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="25" /></td>
            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="25" /></td>
            <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="20" /></td>
        </tr>
    </table>
    <br />
    <table style="width:620px;text-align:left">
        <tr>
            <td>
		        <table style="width:600px; height: 17px">
		            <colgroup><col style='width:300px;' /><col style='width:300px;' /></colgroup>
			        <tr class="TBLINI">
				        <td>&nbsp;Profesional</td>
				        <td>Calendario</td>
			        </tr>
		        </table>
		        <div id="divCatalogo" style="overflow:auto; width: 616px; height:464px">
		            <div style='background-image:url(../../../../Images/imgFT16.gif); width:600px;'>
		                <%=strTablaHtml %>
                    </div>
                </div>
		        <table style="width:600px; height: 17px">
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
				case "buscar": 
				{
                    bEnviar = false;
                    mostrarProfesional();
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

