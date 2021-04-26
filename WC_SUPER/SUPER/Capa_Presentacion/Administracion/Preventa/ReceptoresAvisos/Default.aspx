<%@ Page  Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Preventa_ReceptoresAvisos_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
</script>
<center>
<table style="width:510px; text-align:left;">
    <tr>
        <td>
            <table id="tblTitulo" style="width:500px; height: 17px; margin-top:5px;" border="0">
                <colgroup>
                    <col style="width:20px"/>
                    <col style="width:430px"/>
                    <col style="width:50px"/>
                </colgroup>
                <tr>
                    <td colspan="2"></td>
                    <td  style="padding-left:12px;">
                        <img id="imgMarcar" src="../../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcar(1)" />
                        <img id="imgDesmarcar" src="../../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcar(0)" />
                    </td>
                </tr>
	            <tr class="TBLINI">
                    <td></td>
			        <td title="Administradores y superadministradores de producción">
			            Profesional&nbsp;
				        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
					        height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			            <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
					        height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            </td>
			        <td title="Habilita la recepción de avisos de preventa">Avisar</td>
	            </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width:516px; height:400px" onscroll="scrollTablaAE();">
                <div style='background-image:url(../../../../../Images/imgFT20.gif); width:500px'>
                    <%=strTablaHtml %>
                </div>
            </div>
            <table style="width:500px; height: 17px;">
	            <tr class="TBLFIN"><td></td></tr>
            </table>
            
        </td>
    </tr>
</table>
</center>
<asp:textbox id="hdnMensajeError" runat="server" style="visibility:hidden"></asp:textbox>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton){
                case "grabar": 
                    {
                        bEnviar = false;
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

</script>
</asp:Content>
