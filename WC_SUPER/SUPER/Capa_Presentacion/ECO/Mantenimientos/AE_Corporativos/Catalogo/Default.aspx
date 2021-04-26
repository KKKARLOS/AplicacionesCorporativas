<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
</script>
<center>
<table style="text-align:left">
    <colgroup><col style='width:47%;'/><col style='width:6%;'/><col style='width:47%;'/></colgroup>
    <tr>
        <td colspan="2" align="right" valign="top">
            <label id="lblNodo" runat="server" class="enlace" onclick="getNodo();" style="margin-right:5px;position:relative;top:-5px">Nodo</label>
			<span title="Visualizar toda la información registrada sobre CEEC" style="cursor:pointer;display:inline-block;margin-right:20px;" onclick="getInformacion(1);"><img src="../../../../../images/info.gif" /></span>
        </td>
        <td>
            <table style="width: 435px;height:17px">
                <tr class="TBLINI">
                    <td width="435px">&nbsp;Denominación</td>
                </tr>
            </table>
            <div id="divNodos" style="overflow: auto; width: 453px; height:48px">
                <div style='background-image:url(../../../../../Images/imgFT16.gif); width:437px'>
                </div>
            </div>
            <table style="width: 435px;height:17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">
            <fieldset style="height:450px; width:430px; margin-left:4px;">
            <legend>Criterios estadísticos económicos corporativos</legend>
            <table cellpadding="5" style="width: 430px;">
                <tr>
                    <td colspan="3" valign="top">
                        <span title="Visualizar toda la información sobre un CEEC registrada" style="cursor:pointer;display:inline-block;float:right;" onclick="getInformacion(2);"><img src="../../../../../images/info.gif" /></span>
                        <table style="width: 400px;height:17px;position:relative;">
                            <colgroup><col style='width:35px;' /><col style='width:305px;' /><col style='width:60px' /></colgroup>
                            <tr class="TBLINI">
                                <td></td>
                                <td>&nbsp;Denominación</td>
                                <td>Activo</td>
                            </tr>
                        </table>
                          <div id="divCatalogo" style="overflow: auto; width: 416px; height:160px">
                            <div style='background-image:url(../../../../../Images/imgFT20.gif); width:400px'>
                                <%=strTablaHtml %>
                            </div>
                        </div>
                        <table style="width: 400px;height:17px">
                            <tr class="TBLFIN">
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" valign="top">
                        <span title="Visualizar el uso de un valor asignado a un CEEC" style="display:inline-block;float:right;" onclick="getInformacion(3);"><img src="../../../../../images/info.gif" /></span>
                        <table style="width: 400px;height:17px">
                            <colgroup><col style='width:35px;' /><col style='width:305px;' /><col style='width:60px' /></colgroup>
		                    <tr class="TBLINI">
			                    <td width="35px"></td>
			                    <td width="305px">Valores</td>
			                    <td width="60px">Activo</td>
		                    </tr>
	                    </table>
	                    <div id="divValores" style="OVERFLOW: auto; width: 416px; height:150px">
	                        <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif); width: 400px;">
	                            <%=strTablaHtmlVAE%>
                            </div>
                        </div>
	                    <table style="width: 400px;height:17px">
		                    <tr class="TBLFIN">
			                    <td></td>
		                    </tr>
	                    </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <center>
						    <table style="width:250px">
						    <tr>
							    <td width="45%">
								    <button id="btnNuevo" type="button" onclick="nuevoVAE()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
									    onmouseover="se(this, 25);mostrarCursor(this);">
									    <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
								    </button>	
							    </td>
							    <td width="10%"></td>
							    <td width="45%">
								    <button id="btnEliminar" type="button" onclick="eliminarVAE()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
									     onmouseover="se(this, 25);mostrarCursor(this);">
									    <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
								    </button>	
							    </td>
						    </tr>
						    </table>
				        </center>
                    </td>
                </tr>
            </table>
            </fieldset>
        </td>
        <td align="center">
            &nbsp;
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="3"></asp:Image>
            &nbsp;
        </td>
        <td style="vertical-align:top;">
            <fieldset style="height:450px; width:420px;">
            <legend><label id="lblNodo1" runat="server">Asociados al nodo</label></legend>
            <table cellpadding="5">
                <tr>
                    <td>
                       <table style="width: 400px;height:17px">
                            <colgroup><col style='width:15px;' /><col style='width:305px;' /><col style='width:80px' /></colgroup>
                            <tr class="TBLINI">
                                <td></td>
                                <td>Denominación</td>
                                <td>Obligatorio</td>
                            </tr>
                        </table>
                        <div id="divCENODO" style="overflow: auto; width: 416px; height:160px" target="true" onmouseover="setTarget(this)" caso="1">
                            <div style='background-image:url(../../../../../Images/imgFT20.gif); width:400px'>
                                <table id='tblCECNodo' class='texto MM' style='WIDTH: 400px;' mantenimiento='1'>
                                    <colgroup><col style='width:10px;' /><col style='width:330px;' /><col style='width:60px' /></colgroup>
                                </table>
                            </div>
                        </div>
                        <table style="width: 400px;height:17px">
                            <tr class="TBLFIN">
                                <TD></TD>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 400px;height:17px">
                            <colgroup><col style='width:10px;' /><col style='width:390px;' /></colgroup>
                            <tr class="TBLINI">
                                <td></td>
                                <td>&nbsp;Valores</td>
                            </tr>
                        </table>
                        <div id="divVCECNODO" style="OVERFLOW: auto; width: 416px; height:150px" target="true" onmouseover="setTarget(this)" caso="1">
                            <div style='background-image:url(../../../../../Images/imgFT20.gif); width:400px'>
                                <table id='tblDatosVAENodo' class='texto MM' style='width: 400px;' mantenimiento='1'>
                                    <colgroup><col style='width:10px;' /><col style='width:330px;' /><col style='width:60px' /></colgroup>                                
                                </table>
                            </div>
                        </div>
                        <table style="width: 400px;height:17px">
                            <tr class="TBLFIN">
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </fieldset>
        </td>
    </tr>
</table>
</center>
<div class="clsDragWindow" id="DW" noWrap></div>
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<input type="hidden" name="sVAE" id="sVAE" value="" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("grabar();", 20);
					break;
				}
				case "nuevo": 
				{
                    bEnviar = false;
                    nuevoAE();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;
					//if (confirm("¿Estás conforme?")){
                        preEliminarAE();
                    //}
					break;
				}
			    case "valores": 
			        {
			            bEnviar = false;
			            Valores();
			            break;
			        }
				
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("CEEC.pdf");
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

