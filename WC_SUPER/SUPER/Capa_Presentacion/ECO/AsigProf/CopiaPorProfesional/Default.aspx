<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table id="tabla" class="texto" style="text-align:left; width:980px;">
    <colgroup><col style="width:490px;" /><col style="width:490px;" /></colgroup>
    <tr>
        <td>
			<FIELDSET style="width:470px;">
				<LEGEND>Origen</LEGEND>
                <table class="texto" style="width:465px;" cellpadding="5px" >
                    <tr>
                        <td>
                            <label id="lblProy" class="enlace" style="width:65px" onclick="obtenerProyectos(1)" title="Proyecto económico">Proy. eco.</label>
                            <asp:TextBox ID="txtNumPE" style="width:50px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE(1);}else{vtn2(event);limpiarEstructura(1);}" />
                            <asp:TextBox ID="txtPE" runat="server" style="width:325px" readonly="true" />
                        </td>
                    </tr>
                </table>
            </FIELDSET>
        </td>
        <td>
			<FIELDSET style="width:475px;">
				<LEGEND>Destino</LEGEND>
                <table class="texto" style="width:470px;" cellpadding="5px" >
                    <tr>
                        <td>
                            <label id="lblProy2" class="enlace" style="width:65px" onclick="obtenerProyectos(2)" title="Proyecto económico">Proy. eco.</label>
                            <asp:TextBox ID="txtNumPE2" style="width:50px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE(2);}else{vtn2(event);limpiarEstructura(2);}" />
                            <asp:TextBox ID="txtPE2" runat="server" style="width:330px" readonly="true" />
                        </td>
                    </tr>
                </table>
            </FIELDSET>
        </td>
    </tr>
</table>
<table id="TABLE2" class="texto" style="text-align:left; width:480px;">
    <tr>
        <td style="vertical-align:bottom; height:20px; text-align:right; padding-right:20px;">
            <img id="imgMarcar" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar2(1)" />
            <img id="imgDesmarcar" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar2(0)" />
        </td>
    </tr>
    <tr>
        <td>
	        <TABLE id="TABLE1" style="height:17px; width:460px;">
		        <TR class="TBLINI">
		            <TD>&nbsp;Usuarios a asignar en el proyecto destino
                        <IMG id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones3',1,'divCatalogo3','imgLupa5')"
                            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <IMG id="imgLupa6" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones3',1,'divCatalogo3','imgLupa5', event)"
                            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            </TD>
		        </TR>
	        </TABLE>
            <DIV id="divCatalogo3" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 476px; HEIGHT: 400px;" align="left" onscroll="scrollTablaProfAsig()">
		        <div style='background-image:url(../../../../Images/imgFT20.gif); width:460px; height:auto'>
		            <TABLE class="texto" id="tblOpciones3" style="WIDTH: 460px">
		            </TABLE>
                </DIV>
            </DIV>
            <TABLE id="TABLE3" style="height:17px; width:460px;">
		        <tr class="TBLFIN"><td></td></tr>
	        </TABLE>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:bottom; height:23px;">
            <img border="0" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
</center>
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnEstado" name="hdnEstado" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" name="hdnNodo" id="hdnNodo" value="" />
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<input type="hidden" runat="server" name="hdnT305IdProy2" id="hdnT305IdProy2" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<div class="clsDragWindow" id="DW" noWrap></div>
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
                    grabar();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("CopiaPorProfesional.pdf");
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


