<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table style="width:960px; margin-left:5px; margin-top:10px;text-align:left">
	<tr>
		<td>
			<table id="tblTitulo" style="width:960px;height:17px">
			    <colgroup>
			        <col style="width:40px;" />
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:65px" />
			        <col style="width:355px" />
			        <col style="width:220px" />
			        <col style="width:220px" />
			    </colgroup>
				<tr class="TBLINI">
				    <td>&nbsp;Borrar</td>
				    <td></td>
				    <td></td>
					<td colspan="2" align=right>
					    Nº&nbsp;&nbsp;
					</td>
					<td>
					    Proyecto
					</td>
					<td>
					    Cliente
					</td>
					<td>
					    <label id="lblNodo2" class="TBLINI" style="height:14px; margin-top:3px;" runat="server">Nodo</label>
					</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:976px; height:500px;" onscroll="scrollTablaProy();">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px;">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado"style="width:960px;height:17px">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table width="940px" border="0" class="texto" align="center">
    <colgroup>
        <col style="width:100px" />
        <col style="width:100px" />
        <col style="width:100px" />
        <col style="width:640px" />
    </colgroup>
	  <tr> 
	    <td><img class="ICO" src="../../../../Images/imgProducto.gif" />Producto</td>
        <td><img class="ICO" src="../../../../Images/imgServicio.gif" />Servicio</td>
	    <td><img class="ICO" src="../../../../Images/imgIconoContratante.gif" />Contratante</td>
	    <td><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
      </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</center>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    eliminarProyectos();
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

