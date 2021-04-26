<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_EjecutarPA_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
	    var strServer = "<%=Session["strServer"]%>";
	    function init() {
	        try {
	            $I("txtID").focus();
	        } catch (e) {
	            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	        }
	    }

	    function Exportar() {
	        try {
	            document.forms["aspnetForm"].action = strServer + "Capa_Presentacion/PSP/Informes/ConsTecnico/ExportarOp/default.aspx";
	            document.forms["aspnetForm"].target = "_blank";
	            document.forms["aspnetForm"].submit();
	        } catch (e) {
	            mostrarErrorAplicacion("Error al exportar a PDF", e.message);
	        }
	    }
    </script>
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <br />
    <br />
    <table>
        <tr>
            <td>
                Código Ficepi&nbsp;<asp:TextBox ID="txtID" style="width:110px;" Text="" runat="server" />
            </td>
            <td>
                <button id="btnObtener" type="button" onclick="Exportar();" class="btnH25W110" runat="server" hidefocus="hidefocus" 
		                onmouseover="se(this, 25);mostrarCursor(this);">
	                <img src="/../images/imgObtener.gif" /><span title="Obtener CRYSTAL PDF COSTES DEL ARBOL">PDF Costes</span>
                </button>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
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

