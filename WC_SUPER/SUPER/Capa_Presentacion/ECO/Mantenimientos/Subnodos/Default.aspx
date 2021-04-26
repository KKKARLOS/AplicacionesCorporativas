<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_EstructuraOrg_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script  type="text/jscript">
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>";
</script>
<center>
<table style="width:520px; height:17px; text-align:left;">
    <colgroup>
        <col style="width:50px;" />
        <col style="width:320px;" />
        <col style="width:150px;" />
    </colgroup>
    <tr style="height:35px;">
        <td style="padding-bottom:2px; vertical-align:bottom;">
            <img id="imgNE1" src='../../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);">
            <img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);">
        </td>
        <td>
		    <button id="Button1" type="button" onclick="insertarItem()" class="btnH25W25" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" title="Añade nuevo elemento">
			    <img src="../../../../images/imgSubNodo.gif" />
		    </button>	        
	    </td>
        <td style='text-align:right; padding-right:20px;'><asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar inactivos" /> 
            <input type="checkbox" id="chkMostrarInactivos" class="check" onclick="MostrarInactivos();" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <TABLE id="tblTitulo" style="width: 500px; HEIGHT: 17px;">
                <TR class="TBLINI">
                    <td style="padding-left:35px;">Denominación</td>
                </TR>
            </TABLE>
            <DIV id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:516px; height:460px;" runat="server">
            </DIV>
            <TABLE id="tblResultado" style="width:500px; height:17px;">
                <TR class="TBLFIN">
                    <TD>&nbsp;</TD>
                </TR>
            </TABLE>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <img border="0" src="../../../../images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;
            <img border="0" src="../../../../images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>
        </td>
    </tr>
</table>
</center>
<input type="hidden" runat="server" name="hdnNE" id="hdnNE" value="1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "eliminar": 
				{
                    bEnviar = false;
					jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
					    if (answer) {
					        eliminar();
					    }
					    fSubmit(bEnviar, eventTarget, eventArgument);
					});
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("MantenimientoSubnodos.pdf");
					break;
				}
			}
			if (strBoton != "eliminar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
    function fSubmit(bEnviar, eventTarget, eventArgument)
    {
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

