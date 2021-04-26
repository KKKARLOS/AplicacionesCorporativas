<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
   <center>
    <span style="position:absolute;top:140px;left:710px;cursor:pointer;">
        <label for="ctl00_CPHC_chkActivos" style="position:relative;top:-2px;">Mostrar únicamente activas&nbsp;&nbsp;</label><asp:CheckBox id="chkActivas" runat="server" Checked="true" style="vertical-align:middle" onClick="ObtenerDatos();"/>
    </span>

		 <table style="width:562px; text-align:left;margin-top:15px" cellpadding="0">
			<tbody>
				<tr>
					<td>
						<table id="tblTitulo" style="margin-top:10px;height:17;width:640px">
                        <colgroup>
                            <col style="width:530px;" />
                            <col style="width:110px;" />
                        </colgroup>
							<tr class="TBLINI">
								<td>&nbsp;
                                    <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgAFR1" border="0">
                                    <MAP name="imgAFR1">
				                        <AREA onclick="ot('tblDatos', 0, 0, '')" shape="RECT" coords="0,0,6,5">
				                        <AREA onclick="ot('tblDatos', 0, 1, '')" shape="RECT" coords="0,6,6,11">
			                        </MAP>
                                    Denominación&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
								</td>
								<td>
                                    <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgAFR2" border="0">
                                    <MAP name="imgAFR2">
				                        <AREA onclick="ot('tblDatos', 1, 0, '')" shape="RECT" coords="0,0,6,5">
				                        <AREA onclick="ot('tblDatos', 1, 1, '')" shape="RECT" coords="0,6,6,11">
			                        </MAP>
								    Código Externo
								</td>
							</tr>
						</table>
				    </td>
				</tr>
                <tr>
                    <td>
                        <div id="divCatalogo" style="overflow: auto; width: 656px; height: 495px" align="left">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:640px">
                                <%=strTablaHtml%> 
    		                </div>
		                </div>
		                <table id="tblResultado" style="height:17;width:640px">
			                <tr class="TBLFIN">
				                <td>&nbsp;</td>
			                </tr>
		                </table>
		            </td>
		        </tr>
		</tbody>
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
				case "nuevo": 
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

