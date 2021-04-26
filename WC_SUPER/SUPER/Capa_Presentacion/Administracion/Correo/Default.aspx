<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
</script>
<center>
<div align="left" style="width: 250px;margin-top:150px">
    <div align="center" style="background-image: url('../../../Images/imgFondoCal100.gif');background-repeat:no-repeat; 
        width: 101px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
        &nbsp;Envío de correos
    </div>
    <table border="0" cellpadding="0" cellspacing="0" class="texto" width="250px">
        <tr>
            <td background="../../../Images/Tabla/7.gif" height="6" width="6">
            </td>
            <td background="../../../Images/Tabla/8.gif" height="6">
            </td>
            <td background="../../../Images/Tabla/9.gif" height="6" width="6">
            </td>
        </tr>
        <tr>
            <td background="../../../Images/Tabla/4.gif" width="6">
                &nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding: 5px" align="center">
                <!-- Inicio del contenido propio de la página -->
                <br /><br />
                <img id="imgEstadoCorreo" src="../../../Images/imgCorreoOn.png" />
                <br />
                <label id="lblEstado" class="texto" style="width:100px" >Activado</label>
                <br /><br /><br />
				<button id="btnCorreo" type="button" onclick="grabar();" class="btnH25W85" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<label id="lblAccion" class="texto" style="width:65px; padding-left:10px;" >Desactivar</label>
				</button>			
				  
                <br />
                <!-- Fin del contenido propio de la página -->
            </td>
            <td background="../../../Images/Tabla/6.gif" width="6">
                &nbsp;</td>
        </tr>
        <tr>
            <td background="../../../Images/Tabla/1.gif" height="6" width="6">
            </td>
            <td background="../../../Images/Tabla/2.gif" height="6">
            </td>
            <td background="../../../Images/Tabla/3.gif" height="6" width="6">
            </td>
        </tr>
    </table>
</div>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" id="hdnEstadoCorreo" value="" runat="server"/>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
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

