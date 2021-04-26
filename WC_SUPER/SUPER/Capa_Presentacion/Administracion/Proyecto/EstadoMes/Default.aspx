<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table id="nombreProyecto" style="width:750px;text-align:left">
    <tr>
        <td>
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" cellpadding="5px" style="width:720px">
                        <colgroup><col style="width:80px" /><col style="width:640px" /></colgroup>
                        <tr>
                            <td><label id="lblProy" runat="server" title="Proyecto económico" style="width:55px;margin-top:5px" class="enlace" onclick="getPE()">Proyecto</label></td>
                            <td>
                                <asp:TextBox id="txtNumPE" runat="server" SkinID="Numero" MaxLength="6" Width="50px" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);}"></asp:TextBox>
                                &nbsp;&nbsp;<asp:TextBox ID="txtDesPE" style="width:500px;" runat="server" readonly="true" />&nbsp;&nbsp;
                                <asp:Image ID="imgCat" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" />
                                <asp:Image ID="imgCua" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" />
                                <asp:Image ID="imgEst" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td>Responsable</td>
                            <td><asp:TextBox ID="txtResp" style="width:560px;" runat="server" readonly="true" /><br /></td>
                        </tr>
                        <tr>
                            <td>Cliente</td>
                            <td><asp:TextBox ID="txtCliente" style="width:560px;" runat="server" readonly="true" /><br /></td>
                        </tr>
                        <tr>
                            <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
                            <td><asp:TextBox ID="txtNodo" style="width:560px;" runat="server" readonly="true" /><br /></td>
                        </tr>
                    </table>
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<table style="text-align:left;">
    <tr>
        <td>
            <table style="width: 260px; height: 17px;margin-top:5px;">
                <colgroup><col style='width:20px;' /><col style='width:180px;' /><col style='width:60px;' /></colgroup>
	            <tr class="TBLINI"><td></td><td>Mes</td><td style="text-align:center;">Estado</td></tr>
            </table>
            <div id="divCatalogo2" style="overflow: auto; width: 276px; height:320px;">
                <div id="divCatalogo" style='background-image:url(../../../../Images/imgFT20.gif); width:260px'>
                <%=strTablaHtml %>
                </div>
            </div>
            <table style="width: 260px; height: 17px">
	            <tr class="TBLFIN"><td></td></tr>
            </table>
        </td>
    </tr>
</table>  
</center>	
<div style="position:absolute; bottom:5; margin-left:15px">
<table width="700px" border="0" class="texto" >
    <colgroup>
        <col style="width:100px" />
        <col style="width:100px" />
        <col style="width:100px" />
        <col style="width:200px" />
        <col style="width:100px" />
        <col style="width:100px" />
    </colgroup>
	  <tr> 
	    <td><img class="ICO" src="../../../../Images/imgProducto.gif" />Producto</td>
        <td colspan=5><img class="ICO" src="../../../../Images/imgServicio.gif" />Servicio</td>
	  </tr>
	  <tr><td><img class="ICO" src="../../../../Images/imgIconoContratante.gif" />Contratante</td>
            <td><img class="ICO" src="../../../../Images/imgIconoRepJor.gif" />Replicado</td>
            <td colspan=4><img class="ICO" src="../../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
      </tr>
	  <tr><td style="vertical-align:top;"><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
            <td><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
            <td><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico</td>
            <td><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado</td>
            <td><img class="ICO" src="../../../../Images/imgMesAbierto.gif" title='Mes abierto' />Mes abierto</td>
            <td><img class="ICO" src="../../../../Images/imgMesCerrado.gif" title='Mes cerrado' />Mes cerrado</td>
      </tr>
</table>
</div>
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
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
    	
-->
</script>
</asp:Content>

