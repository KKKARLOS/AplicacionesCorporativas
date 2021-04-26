<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var bHayAparcadas = <%=sHayAparcadas %>;
    window.onselectstart = function(){return false}
</script>
<img id="imgCaution" src="../../../../Images/imgCaution.gif" border="0" style="display:none; position:absolute; top:130px; left:595px;" title="Existen cambios aparcados" />
<center>
    <table style="width:1240px;text-align:left" cellpadding="3px">
    <colgroup>
        <col style="width:590px;" />
        <col style="width:50px;" />
        <col style="width:600px;" />
    </colgroup>
        <tr>
            <td>
                <fieldset style="width:560px;">
                    <legend>
                        Selección de contratos
                        <img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delOrigen()" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                    </legend>
                    <table style="width:558px;" cellpadding="3px;" border="0">
                    <colgroup><col style="width:115px;" /><col style="width:443px;" /></colgroup>
                        <tr>
                            <td>Nº contrato</td>
                            <td>
                                <asp:TextBox ID="txtNumero" runat="server" SkinID="Numero" style="width:60px" 
                                    onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscar();} else {vtn2(event);}" 
                                    MaxLength="7" />
                                <label id="lblLista" class="enlace" onclick="getLista()" style="margin-left:30px; height:17px">Lista</label>
                                <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W85" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" style="display:inline; margin-left:220px;" title="Obtiene una lista de contratos con los que cumplan las condiciones marcadas.">
                                    <span><img src="../../../../images/imgObtener.gif" alt="" />Obtener</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblCROrigen" class="enlace" onclick="getNodoOrigen()"><%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCROrigen" runat="server" Width="432px" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblResponsable" class="enlace" onclick="getResponsable()" title="Responsable contrato">Resp. Contrato</label></td>
                            <td>
                                <asp:TextBox ID="txtDesResponsable" runat="server" Width="432px" readonly="true" />
                                <input type="hidden" id="hdnIdResponsable" value="" runat="server" style="width:1px;visibility:hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblGestor" class="enlace" onclick="getGestor()" title="Responsable de proyecto">Gestor producción</label></td>
                            <td>
                                <asp:TextBox ID="txtDesGestor" runat="server" Width="432px" readonly="true" />
                                <input type="hidden" id="hdnIdGestor" value="" runat="server" style="width:1px;visibility:hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblCliente" class="enlace" onclick="getCliente()">Cliente</label></td>
                            <td>
                                <asp:TextBox ID="txtDesCliente" runat="server" Width="432px" readonly="true" />
                                <input type="hidden" id="hdnIdCliente" value="" runat="server" style="width:1px;visibility:hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblComercialOrigen" class="enlace" onclick="getComercial()">Comercial HERMES</label></td>
                            <td>
                                <asp:TextBox ID="txtDesComercial" runat="server" Width="432px" readonly="true" />
                                <input type="hidden" id="hdnIdComercial" value="" runat="server" style="width:1px;" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:right;">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td>&nbsp;</td>
            <td>
                <input type="checkbox" id="chkGenerarReplicas" class="check" style="vertical-align:middle;" /> 
                <label id="Label1" title="Genera las réplicas necesarias a la finalización del proceso">Generar automáticamente réplicas en meses cerrados</label>
                <br /><br />
                <fieldset>
                    <legend>
                        Destino
                        <img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delDestino()" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                    </legend>
                    <table style="width:575px; margin-top:5px;" cellpadding="3px;" border="0">
                    <colgroup><col style="width:115px;" /><col style="width:470px;" /></colgroup>
                        <tr>
                            <td>
                                <label id="lblCRDestino" class="enlace" onclick="getNodoDestino()"><%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCRDestino" runat="server" Width="360px" readonly="true" />
                                <img id="imgImanPropio" runat="server" src="../../../../Images/imgImanPropio.gif" style="cursor:pointer; vertical-align:bottom;" title="" />&nbsp;
                                <input type="checkbox" id="chkArrastrarPropio" class="check" onclick="setArrastrar('P')" style="vertical-align:middle;" />
                                &nbsp;&nbsp;&nbsp;
                                <img id="imgImanPropioOtros" runat="server" src="../../../../Images/imgImanPropioOtros.gif" style="cursor:pointer; vertical-align:bottom;" title="" />&nbsp;
                                <input type="checkbox" id="chkArrastrarTodos" class="check" onclick="setArrastrar('T')" style="vertical-align:middle;"  />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblResponsableDestino" class="enlace" onclick="getResponsableDestino()" title="Responsable contrato">Resp. Contrato</label></td>
                            <td>
                                <asp:TextBox ID="txtDesResponsableDestino" runat="server" Width="360px" readonly="true" />
                                <input type="hidden" id="hdnIdResponsableDestino" value="" runat="server" style="width:1px;visibility:hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblGestorDestino" class="enlace" onclick="getGestorDestino()" title="Responsable de proyecto">Gestor producción</label></td>
                            <td>
                                <asp:TextBox ID="txtDesGestorDestino" runat="server" Width="360px" readonly="true" />
                                <input type="hidden" id="hdnIdGestorDestino" value="" runat="server" style="width:1px;visibility:hidden" />
                                <img id="img3" runat="server" src="../../../../Images/imgResponsable.gif" style="cursor:pointer; vertical-align:bottom;" title="Pone al gestor de producción del contrato como responsable de los proyectos del contrato" />&nbsp;
                                <input type="checkbox" id="chkArrastrarGestor" class="check" onclick="setArrastrarGestor()" style="vertical-align:middle;"  />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblClienteDestino" class="enlace" onclick="getClienteDestino()">Cliente</label></td>
                            <td>
                                <asp:TextBox ID="txtDesClienteDestino" runat="server" Width="360px" readonly="true" />
                                <input type="hidden" id="hdnIdClienteDestino" value="" runat="server" style="width:1px;visibility:hidden" />
                                <img id="imgImanCliente" runat="server" src="../../../../Images/imgCliente16.gif" 
                                    style="cursor:pointer; vertical-align:bottom;" title="Pone al cliente del contrato como cliente de los proyectos del contrato" />&nbsp;
                                <input type="checkbox" id="chkArrastrarCliente" class="check" onclick="setArrastrarCliente()" style="vertical-align:middle;"  />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="lblComercialDestino" class="enlace" onclick="getComercialDestino()">Comercial HERMES</label></td>
                            <td>
                                <asp:TextBox ID="txtDesComercialDestino" runat="server" Width="360px" readonly="true" />
                                <input type="hidden" id="hdnIdComercialDestino" value="" runat="server" style="width:1px;" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:bottom; text-align:right;">
                <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;
                <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:16px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>
            <td></td>
            <td style="text-align:right;">
                <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
                <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;  margin-right:26px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
            </td>
        </tr>
        <tr style="vertical-align:top;">
            <td>
                <table style="width:570px;">
                    <colgroup>
                       <col style="width:170px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td>Denominación</td>
                        <td><%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></td>
                        <td>Gestor Prod.</td>
                        <td>Cliente</td>
                        <td>Comercial</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; width: 586px; height:340px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:570px">
                        <table id="tblDatos"></table>
                    </div>
                </div>
                <table style="width: 570px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
                <table style="width: 570px; height:17px; margin-top:6px;">
                    <colgroup>
                        <col style="width:20px;" />
                       <col style="width:60px;" />
                       <col style="width:190px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td></td>
                        <td colspan="2">Proyectos relacionados</td>
                        <td><%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></td>
                        <td>Gestor Prod.</td>
                        <td>Cliente</td>
                    </tr>
                </table>
                <div id="divCatalogoRep" style="overflow: auto; width: 586px; height:140px" onscroll="scrollTablaProyRep()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:570px">

                    </div>
                </div>
                <table style="width: 570px; height:17px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align:middle;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td>
                <table id="tblTituloAsignados" style="width: 580px; height:17px;" border="0">
                    <colgroup>
                       <col style="width:160px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                       <col style="width:100px;" />
                       <col style="width:20px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td>Denominación</td>
                        <td><%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></td>
                        <td>Gestor Prod.</td>
                        <td>Cliente</td>
                        <td>Comercial</td>
                        <td></td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; width: 596px; height:520px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaContDest()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:580px">
                        <table id="tblDatos2" style="width:580px;" class="texto MM"  border="0">
                        <colgroup>
                           <col style="width:160px;" />
                           <col style="width:20px;" />
                           <col style="width:80px;" />
                           <col style="width:20px;" />
                           <col style="width:80px;" />
                           <col style="width:20px;" />
                           <col style="width:80px;" />
                           <col style="width:100px;" />
                           <col style="width:20px;" />
                        </colgroup>
                        </table>
                    </div>
                </div>
                <table style="width: 580px; height:17px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
	    <tr>
	        <td></td>
	        <td></td>
	        <td style="padding-top: 5px;">
                &nbsp;<img class="ICO" src="../../../../Images/imgTrasladoOK.gif" />&nbsp;Traslado correcto&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../../Images/imgTrasladoKO.gif" />&nbsp;Traslado no realizado
	        </td>
	    </tr>
    </table>
</center>    
<div class="clsDragWindow" id="DW" noWrap></div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" id="hdnIdNodoOrigen" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoOrigen" value="" runat="server"/>
<input type="hidden" id="hdnIdNodoDestino" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoDestino" value="" runat="server"/>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    nIntentosProcesoDeadLock = 0;
                    procesar(false);
                    break;
				}
				case "aparcar": 
				{
                    bEnviar = false;
                    aparcar();
					break;
				}
				case "recuperar": 
				{
                    bEnviar = false;
                    recuperar();
					break;
				}
				case "replica": 
				{
                    bEnviar = false;
                    replicasmeses();
					break;
				}
                case "aparcardel":
                {
                    bEnviar = false;
                    aparcardel();
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

