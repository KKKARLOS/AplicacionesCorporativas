<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Preventa_Tareas_Default" %>
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
<table style="width: 521px;text-align:center;">
    <tr>
    <td>
	    <table style="width: 510px; height: 17px; margin-top:5px;" border="0">
	        <colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:400px;' /><col style='width:70px;' /></colgroup>
		    <TR class="TBLINI">
			    <td></td>
                <td></td>
			    <td>Denominación</td>                
			     <td title="">Activa</td>
		    </TR>
	    </table>
	    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 521px; height:440px" >
	        <div style='background-image:url(../../../../Images/imgFT20.gif); width:510px' runat="server">
	            <%=strTablaHTML%>
	        </div>
        </div>
	    <table  style="width: 510px; height: 17px;">
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
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
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
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
</asp:Content>

