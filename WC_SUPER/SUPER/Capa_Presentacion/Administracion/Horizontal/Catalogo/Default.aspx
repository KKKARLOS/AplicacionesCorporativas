<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
   <center>
		 <table style="width:562px;" cellpadding="0">
			<TBODY>
				<tr>
					<td colspan="2">
						<table id="tblTitulo" align="left" style="margin-top:10px;height:17px;width:550px;text-align:left">
							<tr class="TBLINI">
								<td>&nbsp;Denominación&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
							</tr>
						</table>
				    </td>
				</tr>
                <tr>
                    <td colspan="2">
                        <div id="divCatalogo" style="overflow: auto; width: 566px; height: 490px" align="left">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:550px">
                                <%=strTablaHtml%> 
    		                </div>
		                </DIV>
		                <table id="tblResultado" style="height:17px;width:550px" align="left">
			                <tr class="TBLFIN">
				                <td>&nbsp;</td>
			                </tr>
		                </table>
		            </TD>
		        </TR>
		</TBODY>
		</TABLE>
    </center>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

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
</script>
</asp:Content>

