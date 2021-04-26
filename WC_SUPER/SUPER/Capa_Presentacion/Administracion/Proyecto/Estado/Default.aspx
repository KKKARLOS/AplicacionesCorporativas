<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">      
    #ctl00_CPHC_rdbEstado tr { height: 30px; }
</style>
    
<center>
<br /><br />
<table id="nombreProyecto" style="width:710px;text-align:left">
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
                    <table id="tblDatos2" cellspacing="7" style="width:680px">
                        <colgroup><col style="width:80px" /><col style="width:600px" /></colgroup>
                        <tr>
                            <td><label id="lblProy" runat="server" title="Proyecto económico" style="width:55px;height:17px" class="enlace" onclick="getPE()">Proyecto</label></td>
                            <td>
                                <asp:TextBox id="txtNumPE" runat="server" SkinID="Numero" MaxLength="6" Width="50px" onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}else{vtn2(event);}"></asp:TextBox>
                                &nbsp;&nbsp;<asp:TextBox ID="txtDesPE" style="width:500px;" runat="server" readonly="true" />&nbsp;&nbsp;
                                <asp:Image ID="imgCat" runat="server" style="width:16px; height:16px; vertical-align:middle;" ImageUrl="~/images/imgSeparador.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td>Cliente</td>
                            <td><asp:TextBox ID="txtCliente" style="width:560px;" runat="server" readonly="true" /><br /></td>
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
            <br />
        </td>
    </tr>
</table>
<table align="center" cellpadding="10px">
<tr>
    <td>
        <FIELDSET id="fstEstado" style="width: 135px; height:150px; text-align:left; vertical-align:super; padding-top:10px;">
        <LEGEND>Estado</LEGEND><br />
            <asp:RadioButtonList id="rdbEstado" runat="server" RepeatDirection="Vertical" RepeatColumns="1" SkinId="rbl" onClick="activarGrabar();">
            <asp:ListItem Value="P">&nbsp;&nbsp;Presupuestado&nbsp;&nbsp;<img src="../../../../Images/imgIconoProyPresup.gif" style="float:right;margin-top:-3px;" title="Proyecto presupuestado"></asp:ListItem>
            <asp:ListItem Value="A">&nbsp;&nbsp;Abierto&nbsp;&nbsp;<img src="../../../../Images/imgIconoProyAbierto.gif" style="float:right;margin-top:-3px;" title="Proyecto abierto"></asp:ListItem>
            <asp:ListItem Value="C">&nbsp;&nbsp;Cerrado&nbsp;&nbsp;<img src="../../../../Images/imgIconoProyCerrado.gif" style="float:right;margin-top:-3px;" title="Proyecto cerrado"></asp:ListItem>
            <asp:ListItem Value="H">&nbsp;&nbsp;Histórico&nbsp;&nbsp;<img src="../../../../Images/imgIconoProyHistorico.gif" style="float:right;margin-top:-3px;" title="Proyecto histórico"></asp:ListItem>
            </asp:RadioButtonList>
        </FIELDSET> 
    </td>
</tr>  
</table>  
</center>	
<div style="position:absolute; bottom:5px; margin-left:15px">
<table width="200px" class="texto">
    <colgroup>
        <col style="width:100px" />
        <col style="width:100px" />
    </colgroup>
      <tr> 
        <td><img class="ICO" src="../../../../Images/imgProducto.gif" />&nbsp;Producto</td>
        <td><img class="ICO" src="../../../../Images/imgServicio.gif" />&nbsp;Servicio</td>
      </tr>
</table>
</div>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
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
</script>
</asp:Content>

