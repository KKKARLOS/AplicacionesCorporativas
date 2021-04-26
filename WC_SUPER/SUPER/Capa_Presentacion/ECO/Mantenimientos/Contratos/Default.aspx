<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <center>
		 <table class="texto" cellSpacing="0" cellPadding="0" style="width:562px; text-align:left;">
			<TBODY>
				<tr>
					<td colspan="2">
						<table id="tblTitulo" cellSpacing="0" cellPadding="0" style="margin-top:10px; height:17px; width:550px;">
							<tr class="TBLINI">
								<td>&nbsp;Denominación&nbsp;
								    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
									<IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
							</tr>
						</table>
				    </td>
				</tr>
                <tr>
                    <td colspan="2">
                        <div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 566px; HEIGHT: 540px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:550px;">
                                <%=strTablaHtml%> 
    		                </div>
		                </DIV>
		                <table id="tblResultado"style="height:17px; width:550px;">
			                <tr class="TBLFIN">
				                <td>&nbsp;</td>
			                </tr>
		                </table>
		            </TD>
		        </TR>
		</TBODY>
		</TABLE>
    </center>
    <asp:textbox id="hdnErrores" runat="server" style="visibility:hidden"></asp:textbox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
//			switch (strBoton){
//				case "guia": 
//				{
//                    bEnviar = false;
//                    mostrarGuia("TarifasCliente.pdf");
//					break;
//				}
//			}
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
</SCRIPT>
</asp:Content>

