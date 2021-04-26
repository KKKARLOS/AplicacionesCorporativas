<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
   <center>
    <table style="width: 520px;margin-top:10px;text-align:left">
    <tr>
        <td>
		    <table id="tblTitulo" style="width: 500px; height: 17px; margin-top:10px;">
            <colgroup><col style='width:300px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>
			    <tr class="TBLINI">
				    <td style="padding-left:15px;">Cuenta
		    		    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" tipolupa="2" width="20">
					    <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../../Images/imgLupa.gif" tipolupa="1" width="20">
				    </td>
				    <td style="text-align:right;padding-left:5px" title="Valor de Negocio">V.N.</td>
				    <td align="center">Es cliente</td>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow-y: auto; overflow-x: hidden;width: 516px; height:441px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
		        <%=strTablaHtml%>
		        </div>
		    </div>
		    <table style="width: 500px;height: 17px">
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
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "nuevo": //Boton Nuevo
				{
				    bEnviar = false;
					Nuevo();
					break;
				}				
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminar();
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

