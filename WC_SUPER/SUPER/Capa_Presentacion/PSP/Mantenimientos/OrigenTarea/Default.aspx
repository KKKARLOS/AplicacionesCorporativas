<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_OrigenTarea_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
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
<style type="text/css">  
#tblDatos tr { height: 20px; }
</style>     
<center>
<table style="width: 916px;text-align:left;">
        <tr>
            <td>
                <label id="lblNodo" style="width:500px;height:17px" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
                <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList id="cboCR" runat="server" Width="500px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems="true"></asp:DropDownList>
                <asp:TextBox ID="txtDesNodo" style="width:500px;" Text="" readonly="true" runat="server" />
            </td>
        </tr>
    <tr>
    <td>
	    <table  style="width: 900px; height: 17px; margin-top:5px;">
	        <colgroup><col style='width:200px;' /><col style='width:100px;' /><col style='width:545px;' /><col style='width:55px;' /></colgroup>
		    <TR class="TBLINI">
			    <td style='padding-left:20px;'>Denominación</td>
			    <td>Notificable</td>
			    <td>E-mail</td>
			    <td>Activo</td>
		    </TR>
	    </TABLE>
	    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 916px; height:440px" >
	        <div style='background-image:url(../../../../Images/imgFT20.gif); width:900px' runat="server">
	            <%=strTablaHTML%>
	        </div>
        </div>
	    <table  style="width: 900px; height: 17px;">
		    <tr class="TBLFIN">
			    <td></td>
		    </tr>
	    </table>
    </td>
    </tr>
    </table>
    <table style="width:250px;margin-top:15px">
    <tr>
        <td width="45%">
            <button id="btnAnadir" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
            </button>	
        </td>
        <td width="10%"></td>
        <td width="45%">
            <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
            </button>	
        </td>
    </tr>
    </table>
</center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
//				case "nuevo": 
//				{
//                    bEnviar = false;
//                    nuevo();
//					break;
//				}
//				case "eliminar": 
//				{
//                    bEnviar = false;
//                    eliminar();
//					break;
//				}
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("OrigenTarea.pdf");
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

