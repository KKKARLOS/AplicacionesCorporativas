<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_TipoAsunto_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <br />
    <center>
    <table style="width: 500px;text-align:left;">
    <tr>
    <td>
		<table id="Table2" style="width: 450px; height: 17px">
			<tr class="TBLINI">
				<td>&nbsp;
				    <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgDes" border="0"> 
					    <map name="imgDes">
						    <area onclick="ordenarTabla(1,0)" shape="RECT" coords="0,0,6,5">
						    <area onclick="ordenarTabla(1,1)" shape="RECT" coords="0,6,6,11">
					    </map>Tipo de asunto
			    </td>
			</tr>
		</table>
		<div id="divCatalogo" style="OVERFLOW: auto; width: 466px; height:480px">
		    <div style='background-image:url(../../../../Images/imgFT20.gif); width:450px'>
		    <%=strTablaHtml %>
		    </div>
        </div>
		<table id="Table3" style="width: 450px; height: 17px">
			<tr class="TBLFIN">
				<td></td>
			</tr>
		</table>
        
    </td>
    </tr>
    </table>
    <table style="width:250px;margin-top:10px;">
    <tr>
        <td width="45%">
            <button id="btnAnadir" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
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
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
	        switch (strBoton) {
//				case "nuevo": 
//				{
//                    bEnviar = false;
//                    nuevo();
//					break;
//				}
//				case "eliminar": 
//				case "borrar": 
//				{
//                    bEnviar = false;
//					//if (confirm("¿Estás conforme?")){
//                        eliminar();
//                    //}
//					break;
//				}
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

