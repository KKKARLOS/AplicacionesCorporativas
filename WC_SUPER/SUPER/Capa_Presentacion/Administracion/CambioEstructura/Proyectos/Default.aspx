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
</script>
<img id="imgCaution" src="../../../../Images/imgCaution.gif" border=0 style="display:none; position:absolute; top:130px; left:1130px;" title="Existen cambios aparcados" />
<center>
    <table style="width:1240px;text-align:left" cellpadding="5">
    <colgroup>
        <col style="width:590px;" />
        <col style="width:60px;" />
        <col style="width:590px;" />
    </colgroup>
        <tr>
            <td style="vertical-align:middle;">
                <asp:RadioButtonList Height="25px" ID="rdbAmbito" runat="server" SkinId="rbl" RepeatDirection="horizontal" onclick="seleccionAmbito(this.id)" style="display:inline-block">
                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_0').click();" Selected=true Value="P" Text="Número&nbsp;&nbsp;&nbsp;" />
                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_1').click();" Value="N" Text="C.R.&nbsp;&nbsp;&nbsp;" />
                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_2').click();" Value="R" Text="Responsable" />
                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_3').click();" Value="C" Text="Cliente" />
                </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <label id="lblLista" class="enlace" onclick="getLista()" style=" margin-left:30px;vertical-align:super">Lista</label>
            </td>
            <td>&nbsp;</td>
            <td><input type="checkbox" id="chkGenerarReplicas" class="check" style="vertical-align:middle;" checked /> <label id="Label1" title="Genera las réplicas necesarias a la finalización del proceso">Generar automáticamente réplicas en meses cerrados</label>
                <input type="checkbox" id="chkMantResp" class="check" style="vertical-align:middle; margin-left:30px;" checked /> <label id="Label2" title="Mantiene los responsables de proyecto originales o asigna al responsable del subnodo destino">Mantener responsabilidades</label>
            </td>
        </tr>
        <tr style="height:40px;">
            <td>
                <table style="width: 560px;">
                    <colgroup>
                        <col style="width: 500px;" />
                        <col style="width: 60px;" />
                    </colgroup>
                    <tr>
                        <td>
                            <span id="ambNumero" style="display:block; width:450px;" class="texto">&nbsp;Número<br /><asp:TextBox ID="txtNumero" runat="server" SkinID="Numero" style="width:60px" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;mostrarRelacionProyectos('P', this.value);} else {vtn2(event);}" MaxLength="7" /></span>
                            <span id="ambCR" style="display:none; width:450px;" class="texto">&nbsp;<label id="lblCROrigen" class="enlace" onclick="getNodoOrigen()"><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO)%> origen</label><br /><asp:TextBox ID="txtCROrigen" runat="server" Width="400px" readonly="true" /></span>
                            <span id="ambResponsable" style="display:none; width:450px;" class="texto">&nbsp;<label id="lblResponsable" class="enlace" onclick="getResponsable()">Responsable</label><br /><asp:TextBox ID="txtDesResponsable" runat="server" Width="400px" readonly="true" /><input type="hidden" id="hdnIdResponsable" value="" runat="server" style="width:1px;visibility:hidden" /></span>
                            <span id="ambCliente" style="display:none; width:450px;" class="texto">&nbsp;<label id="lblCliente" class="enlace" onclick="getCliente()">Cliente</label><br /><asp:TextBox ID="txtDesCliente" runat="server" Width="400px" readonly="true" /><input type="hidden" id="hdnIdCliente" value="" runat="server" style="width:1px;visibility:hidden" /></span>
                        </td>
                        <td style="vertical-align:bottom; text-align:right;" >
                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
            <td>
                <table style="width: 560px;">
                    <colgroup>
                        <col style="width:500px;" />
                        <col style="width:60px;" />
                    </colgroup>
                    <tr>
                    <td>&nbsp;<label id="lblCRDestino" class="enlace" onclick="getNodoDestino()"><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> destino</label></td>
                    <td></td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtCRDestino" runat="server" width="400px" readonly="true" /></td>
                    <td align="right"><img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="vertical-align:top;">
            <td>
                <table style="width: 560px;">
                    <colgroup>
                        <col style="width:60px;" />
                        <col style="width:300px;" />
                        <col style="width:200px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td></td>
                        <td>Proyecto</td>
                        <td><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; width: 576px; height:460px" onscroll="scrollTablaProy()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:560px">
                    </div>
                </div>
                <table style="width: 560px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
                <table style="width: 560px; margin-top:6px;">
                    <colgroup>
                        <col style="width:20px;" />
                        <col style="width:320px;" />
                        <col style="width:220px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td></td>
                        <td>Réplicas</td>
                        <td><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></td>
                    </tr>
                </table>
                <div id="divCatalogoRep" style="overflow: auto; width: 576px; height:150px" onscroll="scrollTablaProyRep()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:560px">
                    </div>
                </div>
                <table style="width: 560px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align:middle;">
                <asp:Image id="imgPapelera" style="cursor: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td>
                <table id="tblTituloAsignados" style="width: 560px; height:17px">
                    <colgroup>
                        <col style="width:60px;" />
                        <col style="width:290px;" />
                        <col style="width:185px;" />
                        <col style="width:25px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td></td>
                        <td style="padding-left:3px;">Proyecto</TD>
                        <td><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></TD>
                        <td></td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; width: 576px; height:650px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProyDest()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:560px">
                    <table id="tblDatos2" style="width: 560px;" class="texto MM">
                    <colgroup>
                        <col style="width: 20px;" />
                        <col style="width: 20px;" />
                        <col style="width: 20px;" />
                        <col style="width: 290px;" />
                        <col style="width: 190px;" />
                        <col style="width: 20px;" />
                    </colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 560px; height:17px">
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
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" id="hdnIdNodoOrigen" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoOrigen" value="" runat="server"/>
<input type="hidden" id="hdnIdNodoDestino" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoDestino" value="" runat="server"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
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
-->
</script>
</asp:Content>

