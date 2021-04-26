<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
-->
</script>
<style>
#tblDatosx TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<br /><br />
<center>
<table id="tabla" class="texto" cellpadding="2" style="width:916px;">
	<tr>
		<td>
            <table id="tblTitulo" style="width:900px; height:17px;  text-align:left;">
                <colgroup>
                    <col style="width:60px;" />
                    <col style="width:440px;" />
                    <col style="width:200px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                </colgroup>
                <tr class="TBLINI">
                    <td style="text-align:right;">Número</td>
                    <td style="padding-left:8px;">&nbsp;Proyecto</td>
                    <td>Cliente</td>
                    <td style="text-align:center;" title="Proyecto externalizado">Externalizado</td>
                    <td style="text-align:center;" title="Factura el soporte administrativo">Factura USA</td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 916px; height:500px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:900px">
                    <%=strTablaHTML %>
                </div>
            </div>
            <table id="tblResultado" style="width: 900px; height: 17px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
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
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("Cierre.pdf");
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

