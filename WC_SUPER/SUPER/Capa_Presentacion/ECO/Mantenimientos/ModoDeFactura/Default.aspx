<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
</script>
<center>
<br /><br />
<table style="text-align:left;">
    <colgroup><col style='width:48%;'/><col style='width:4%;'/><col style='width:48%;'/></colgroup> 
    <tr>
        <td style="vertical-align:top;">
            <table cellpadding="5px">
                <tr>
                    <td>
                        <TABLE id="Table2" style="WIDTH: 400px; HEIGHT: 17px">
                            <TR class="TBLINI">
                                <td>&nbsp;Denominación</TD>
                            </TR>
                        </TABLE>
                        <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 416px; height:400px">
                            <div style='background-image:url(../../../../Images/imgFT20.gif); width:400px;'>
                                <%=strTablaHtml %>
                            </DIV>
                        </DIV>
                        <TABLE id="Table3" style="WIDTH: 400px; HEIGHT: 17px">
                            <TR class="TBLFIN">
                                <TD></TD>
                            </TR>
                        </TABLE>
                    </td>
                </tr>
            </table>
        </td>
        <td style="text-align:center;">
        </td>
        <td style="vertical-align:top;">
          <table cellpadding="5px">
                <tr>
                    <td colspan="3">
	                    <TABLE id="Table1" style="WIDTH: 400px; HEIGHT: 17px">
	                        <colgroup><col style="width:340px;"/><col style="width:60px;"/></colgroup>
		                    <TR class="TBLINI">
			                    <td>&nbsp;Denominación</TD>
			                    <td>Estado</TD>
		                    </TR>
	                    </TABLE>
	                    <DIV id="divValores" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 416px; height:400px">
	                        <div style="background-image:url(../../../../Images/imgFT20.gif); width:400px;">
	                            <%=strTablaHtmlModoFac %>
                            </DIV>
                        </DIV>
	                    <TABLE id="Table4" style="WIDTH: 400px; HEIGHT: 17px">
		                    <TR class="TBLFIN">
			                    <TD></TD>
		                    </TR>
	                    </TABLE>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<table style="width:300px; margin-left:40px;">
    <tr>
        <td>
            <button id="btnNuevo" type="button" onclick="nuevoModoFac()" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgAnadir.gif" /><span title="Salir">Añadir</span>
            </button>    
        </td>
        <td>
            <button id="btnEliminar" type="button" onclick="eliminarModoFac()" class="btnH25W90" style="margin-left:5px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgEliminar.gif" /><span title="Salir">Eliminar</span>
            </button>    
        </td>
    </tr>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<input type="hidden" id="sModoFac" value="" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "grabar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("grabar();", 20);
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

