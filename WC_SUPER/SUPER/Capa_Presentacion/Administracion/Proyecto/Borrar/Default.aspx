<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table style="width:950px; text-align:left">
<colgroup><col style="width:340px"/><col style="width:610px"/></colgroup>
<tr>
<td><br />
    <table id="nombreProyecto" style="height:20px;width:270px">
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
    	                <table id="tblDatos2" class="texto" border="0" cellspacing="7" cellpadding="0" width="230px">
    	                    <tr>
    	                        <td>Teclee nº de proyecto a eliminar&nbsp;
    	                        <asp:TextBox id="txtNumPE" runat="server" SkinID="Numero" MaxLength="6" Width="50px" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscar();}else{vtn2(event);}"></asp:TextBox></td>
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
</td>
<td align="left">
    <table class="texto" width="680px">
        <colgroup><col style="width:80px;" /><col style="width:600px;" /></colgroup>
        <tr>
            <td>Denominación</td>
            <td>
                <asp:TextBox ID="txtDesPE" style="width:500px;" runat="server" readonly="true" />
                &nbsp;&nbsp;<asp:Image ID="imgEst" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" />
                &nbsp;&nbsp;<asp:Image ID="imgCat" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" />
            </td>
        </tr>
        <tr>
            <td>Cliente</td>
            <td><asp:TextBox ID="txtCliente" style="width:500px;" runat="server" readonly="true" /><br /></td>
        </tr>
    </table>
</td>
</tr>    
<tr>
<td>&nbsp;</td>
</tr>    
<tr>
<td colspan="2">
    <table style="width:940px" cellPadding="5px">
		<tr>
		    <td>
                <table id="tblTitulo" style="width:920px;">
                    <colgroup><col style="width:20px" /><col style="width:400px" /><col style="width:300px" /><col style="width:200px" /></colgroup>
	                <tr class="TBLINI">
	                    <td></td>
		                <td>&nbsp;Denominación</td>
		                <td style="padding-top:2px;"><label id="lblNodo2" class="TBLINI" style="height:14px;" runat="server">&nbsp;&nbsp;&nbsp;Nodo</label></td>
		                <td>&nbsp;Responsable</td>
	                </tr>
                </table>
			    <div id="divCatalogo" style="overflow: auto; width: 936px; height: 320px;">
				     <div style="background-image:url(../../../../Images/imgFT20.gif); width:920px">
				     <table id="tblDatos" style="WIDTH: 920px"></table>
				     </div>
			    </div>
			    <table id="tblResultado" style="width:920px;">
				    <tr class="TBLFIN"><td></td></tr>
			    </table>
			    <br /><br />
		    </td>
	    </tr>
	</table>
</td>
</tr>    
<tr>
<td colspan="2">
    <table style="width:940px">
        <colgroup>
            <col style="width:100px" />
            <col style="width:90px" />
            <col style="width:210px" />
            <col style="width:540px" />
        </colgroup>
	      <tr> 
	        <td><img border="0" src="../../../../Images/imgProducto.gif" />&nbsp;Producto</td>
            <td><img border="0" src="../../../../Images/imgServicio.gif" />&nbsp;Servicio</td>
            <td></td>
		    <td></td>
	      </tr>
	      <tr>
	        <td><img border="0" src="../../../../Images/imgIconoContratante.gif" />&nbsp;Contratante</td>
            <td><img border="0" src="../../../../Images/imgIconoRepJor.gif" />&nbsp;Replicado</td>
            <td><img border="0" src="../../../../Images/imgIconoRepPrecio.gif" />&nbsp;Replicado con gestión propia</td>
          </tr>
	      <tr>
	        <td style="vertical-align:top;"><img border="0" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />&nbsp;Abierto</td>
            <td><img border="0" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />&nbsp;Cerrado</td>
            <td><img border="0" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />&nbsp;Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />&nbsp;Presupuestado</td>
          </tr>
    </table>
</td>
</tr>    
</table>
</center>	
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

		    switch (strBoton) {
		        case "eliminar":
		        case "borrar":
	            {
	                bEnviar = false;
	                jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
	                    if (answer) {
	                        eliminarPE();
	                    }
	                    fSubmit(bEnviar, eventTarget, eventArgument);
	                    return;
	                });
	                break;
                }
            }
		    if (strBoton != "eliminar" && strBoton != "borrar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
	function fSubmit(bEnviar, eventTarget, eventArgument) {
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

