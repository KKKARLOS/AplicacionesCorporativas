<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
</script>
    <center>
    <table class="texto" style="width:870px; margin-top:5px;text-align:left">
    <colgroup>
        <col style="width:870px;" />
    </colgroup>
        <tr>
		    <td>						
			    <table class="texto" width="840px;margin-top:5px;" cellpadding="5px" >
				    <colgroup>
					    <col style="width:420px;" />
					    <col style="width:420px;" />
				    </colgroup>
                        <tr>
                            <td>
		                        <fieldset style="width:400px;height:70px;margin-top:10px;">
		                            <legend>Origen</legend>
								    <table class="texto" style="width:400px;margin-top:20px;margin-left:10px;">
									    <colgroup>
										    <col style="width:400px;" />
									    </colgroup>
					                        <tr>
					                            <td>			                            
	                                                <label id="lblPaisGes" style="width:50px;">País</label>
                                                    <asp:DropDownList ID="cboPaisGes" runat="server" width="200px" onchange="javascript:obtenerProvinciasGesPais(this.value);" onkeydown = "javascript:obtenerProvinciasGesPais(this.value);">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                    </table>
                   			    </fieldset>								                                            
                            </td>
                            <td>
		                        <fieldset style="width:390px;height:70px;margin-left:10px;margin-top:10px;">
		                            <legend>Asignación</legend>	 
								    <table class="texto" style="width:400px;margin-top:5px;margin-left:5px;" cellpadding="3px">
									    <colgroup>
										    <col style="width:270px;" />
										    <col style="width:130px;" />
									    </colgroup>
					                        <tr style="height:20px">
					                            <td colspan="2">			                            
	                                                <label id="lblAmbito" style="width:50px;">Ámbito</label>
                                                    <asp:DropDownList ID="cboAmbito" runat="server" width="200px" onchange="javascript:obtenerZonasAmbito(this.value);">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
					                        <tr>
					                            <td>			                            
                                                    <label id="lblzona" style="width:50px;">Zona</label>
	                                                    <asp:DropDownList id="cboZona" runat="server" Width="200px">
	                                                    </asp:DropDownList>	
                                                </td>
                                                <td align="center">
                                                    <button id="btnReasignar" type="button" onclick="asignar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                                                         onmouseover="se(this, 25);mostrarCursor(this);">
                                                        <img src="../../../Images/botones/imgReasignar.gif" /><span title="Permite asignar a las provincias seleccionadas la zona que hemos especificado">Asignar</span>
                                                    </button>
                                                </td>
                                            </tr>                                                
                                    </table>			                                                       
	                            </fieldset>		
                            </td>
                        </tr>												
			    </table>
		    </td>
	    </tr>
	    <tr style="height:25px">   
            <td style="vertical-align:bottom; text-align:right; padding-right:30px">
                <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>
        </tr>            	     
        <tr>
            <td>
                <table id="tblTitulo" style="width: 840px; height: 17px ;margin-top:10px;">
                    <colgroup>
                        <col style="width:20px;" />
                        <col style="width:410px;" />
                        <col style="width:410px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td></td>
					    <td style="padding-left:3px;">
					        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					        <MAP name="img1">
					            <AREA onclick="ot('tblDatos', 1, 0, '')" shape="RECT" coords="0,0,6,5">
					            <AREA onclick="ot('tblDatos', 1, 1, '')" shape="RECT" coords="0,6,6,11">
				            </MAP>&nbsp;Provincia&nbsp;
				            <IMG id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
						            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						    <IMG style="CURSOR: pointer; display: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)"
						            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </td>
					    <td style="padding-left:3px;"><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						        <MAP name="img2">
						            <AREA onclick="ot('tblDatos', 2, 0, '')" shape="RECT" coords="0,0,6,5">
						            <AREA onclick="ot('tblDatos', 2, 1, '')" shape="RECT" coords="0,6,6,11">
					            </MAP>&nbsp;Zona&nbsp;
					            <IMG id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa2')"
							            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							    <IMG style="CURSOR: pointer; display: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa2', event)"
							            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow:auto; overflow-x: hidden; width: 856px; height:340px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT18.gif); width:840px;">
                    <table id="tblDatos" style="width: 840px; table-layout:fixed;" class="texto">
                    <colgroup>
                        <col style="width:20px;" />
                        <col style="width:410px;" />
                        <col style="width:410px;" />
                    </colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 840px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</center>    
<input type="hidden" id="hdnIdNodo" value="" runat="server"/>
<input type="hidden" id="hdnDesNodo" value="" runat="server"/>
<input type="hidden" id="hdnIdSubnodo" value="" runat="server"/>
<input type="hidden" id="hdnResponsableDestino" value="" runat="server"/>
<input type="hidden" id="hdnResponsableOrigen" value="" runat="server"/>
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

