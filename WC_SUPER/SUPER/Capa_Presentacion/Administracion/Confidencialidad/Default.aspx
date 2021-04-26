<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <center>
    <br />
    <table style="width: 520px;">
    <tr>
        <td>
		    <table style="width: 500px; height: 17px; margin-top:5px;">
            <colgroup><col style='width:300px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>
			    <tr class="TBLINI">
				    <td width="300px" style="padding-left:15px;">Categoría</td>
				    <td width="100px" style="text-align:right;">&euro; hora&nbsp;</td>
				    <td width="100px" style="text-align:right;">&euro; jornada&nbsp;</td>
			    </tr>
		    </table>
		    <DIV id="divCatalogo" style="OVERFLOW: auto; width: 516px; height:500px" runat="server">
		    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
		    <%=strTablaHTML%>
		    </div>
		    </DIV>
		    <TABLE style="width: 500px;height: 17px">
			    <TR class="TBLFIN">
				    <TD></TD>
			    </TR>
		    </TABLE>
        </td>
    </tr>
    </table>
    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
   <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "nuevo": 
				{
                    bEnviar = false;
                    nuevo();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminar();
					break;
				}
				case "grabar": 
				{
                    bEnviar = false;
                    setTimeout("grabar();", 20);
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("TarifasNodo.pdf");
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
-->
</script>
</asp:Content>

