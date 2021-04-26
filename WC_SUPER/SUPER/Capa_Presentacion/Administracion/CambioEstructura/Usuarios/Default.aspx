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
        <tr >
            <td>
                <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinId="rbl" onclick="seleccionAmbito(this.id)" style="display:inline-block">
                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_0').click();" Selected="true" Value="A" Text="Nombre&nbsp;&nbsp;&nbsp;" />
                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_1').click();"  Value="C" Text="C.R.&nbsp;&nbsp;&nbsp;" />
                </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <label id="lblLista" class="enlace" onclick="getLista()" style=" margin-left:30px;vertical-align:super">Lista</label>
                <input type="checkbox" id="chkMostrarBajas" class="check" onclick="borrarCatalogo();repetirBusqueda();" style="vertical-align:middle; margin-left:100px; display:inline" /> <label id="lblMostrarBajas">Mostrar bajas</label>
            </td>
            <td>&nbsp;</td>
            <td><input type="checkbox" id="chkGenerarReplicas" class="check" style="vertical-align:middle;display:inline" checked /> <label id="Label1" title="Genera las réplicas necesarias a la finalización del proceso">Generar automáticamente réplicas en meses cerrados</label></td>
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
                        <span id="ambAp" style="display:block" class="texto">
                        <table style="WIDTH: 390px;">
                        <colgroup>
                            <col style="width:130px"/>
                            <col style="width:130px"/>
                            <col style="width:130px"/>
                        </colgroup>
                            <tr>
                            <td>&nbsp;Apellido1</td>
                            <td>&nbsp;Apellido2</td>
                            <td>&nbsp;Nombre</td>
                            </tr>
                            <tr>
                            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtNombre"    runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
                        </span>
                        <span id="ambCR" style="display:none" class="texto">&nbsp;<label id="lblCROrigen" class="enlace" onclick="getNodoOrigen()"><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO)%> origen</label><br /><asp:TextBox ID="txtCROrigen" runat="server" Width="316px" readonly="true" /></span>
                        </td>
                        <td style="text-align:right; vertical-align:bottom;">
                        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
            <td>
                <table style="width: 560px;">
                    <tr>
                    <td width="350px">&nbsp;<label id="lblCRDestino" class="enlace" onclick="getNodoDestino()"><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> destino</label></td>
                    <td width="100px"><label id="lblMesValor">&nbsp;Mes valor</label> </td>
                    <td></td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtCRDestino" runat="server" Width="316px" readonly="true" /></td>
                    <td><asp:TextBox ID="txtMesValor" runat="server" style="width:90px; text-align:center; cursor:pointer;" readonly="true" onclick="getMesValor()" /><asp:TextBox ID="hdnMesValor" runat="server" style="width:1px; visibility:hidden;" ReadOnly=true /></td>
                    <td style="vertical-align:bottom; text-align:right;">
                    <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 560px;">
                    <colgroup>
                        <col style="width:332px;" />
                        <col style="width:228px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="padding-left:23px">Usuario</TD>
                        <td><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></TD>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; width: 576px; height:650px" onscroll="scrollTablaProf()">
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
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td>
                <table id="tblTituloAsignados" style="width: 560px;">
                    <colgroup>
                        <col style="width:250px;" />
                        <col style="width:180px;" />
                        <col style="width:100px;" />
                        <col style="width:20px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="padding-left:22px;">Usuario</td>
                        <td><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></td>
                        <td>Mes valor</td>
                        <td></td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; width: 576px; height:650px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfDest()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:560px">
                    <table id="tblDatos2" style="width: 560px;" class="texto MM" border="0">
                    <colgroup>
                        <col style="width: 20px;" />
                        <col style="width:230px;" />
                        <col style="width:180px;" />
                        <col style="width:100px;" />
                        <col style="width: 20px;" />
                    </colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 560px;">
                    <tr class="TBLFIN">
                        <TD></TD>
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
                    bProcesado = true;
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
                    bProcesado = false;
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

