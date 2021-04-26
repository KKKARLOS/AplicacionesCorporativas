<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GESTAR.Capa_Presentacion.ASPX.Catalogos" Title="Catálogo" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
        <br />
<style>
    .Catalogo {overflow-x: hidden; overflow: auto}
</style>
		<table id="tblFiltros" style="width: 880px;position: absolute; left: 10px;" height="17" >
		<tr>
			<td>
				<asp:RadioButtonList  ID="rdlInforme"  runat="server" CssClass="texto"  RepeatDirection="Horizontal" RepeatLayout="Flow">
					<asp:ListItem Selected="True" Value="0">Relación</asp:ListItem>
					<asp:ListItem  Value="1">Detalle</asp:ListItem>
				</asp:RadioButtonList>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla(1);" title="Marca todas" />
				&nbsp;&nbsp;
				<img src="../../../../images/botones/imgdesmarcar.gif" onclick="mTabla(2);" title="Desmarca todas"/>
						
			</td>
		</tr>
		</table>	
        <br /><br /><br />
		<table id="tblTituloDefic" style="width: 980px;position: absolute; left: 10px; height:17px">
            <colgroup>
                <col style="width:352px"/>
                <col style="width:130px"/>
                <col style="width:197px"/>
                <col style="width:106px"/>
                <col style="width:60px"/>
                <col style="width:64px"/>
                <col style="width:45px"/>
                <col style="width:16px"/>
            </colgroup>
			<tr class="TBLINI">
				<td >&nbsp;&nbsp;<img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogo',1,'divCatalogoDefi','imgLupa1',event)"
						height="11" src="../../../../images/imgLupa.gif" width="20"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogo',1,'divCatalogoDefi','imgLupa1')"
						height="11" src="../../../../images/imgLupaMas.gif" width="20"> 
						<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgDescri" border="0">
								<map name="imgDescri">
								<area onclick="Cargar(1, 0)" shape="RECT" coords="0,0,6,5">
								<area onclick="Cargar(1, 1)" shape="RECT" coords="0,6,6,11">
							</map>								
						&nbsp;Orden
				</td>
				<td>
						<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgArea" border="0">
								<map name="imgArea">
								<area onclick="Cargar(2, 0)" shape="RECT" coords="0,0,6,5">
								<area onclick="Cargar(2, 1)" shape="RECT" coords="0,6,6,11">
							</map>														
				Área</td>	
				<td>
						<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgSolic" border="0">
								<map name="imgSolic">
								<area onclick="Cargar(3, 0)" shape="RECT" coords="0,0,6,5">
								<area onclick="Cargar(3, 1)" shape="RECT" coords="0,6,6,11">
							</map>																				
				Solicitante</td>																						
				<td>
						<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgEstado" border="0">
								<map name="imgEstado">
								<area onclick="Cargar(4, 0)" shape="RECT" coords="0,0,6,5">
								<area onclick="Cargar(4, 1)" shape="RECT" coords="0,6,6,11">
							</map>																										
				Estado actual</td>
				<td>
						<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFNotif" border="0">
								<map name="imgFNotif">
								<area onclick="Cargar(5, 0)" shape="RECT" coords="0,0,6,5">
								<area onclick="Cargar(5, 1)" shape="RECT" coords="0,6,6,11">
							</map>
                <label id='lblFNot' title='Fecha de notificación'>F.&nbsp;Notifi.</label></td>																																									
				<td>
						<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFLim" border="0">
								<map name="imgFLim">
								<area onclick="Cargar(6, 0)" shape="RECT" coords="0,0,6,5">
								<area onclick="Cargar(6, 1)" shape="RECT" coords="0,6,6,11">
							</map>																																						
				<label id='lblfecha' title='Fecha límite o pactada'><%=strFecha%></label></td>
				<td >
						<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgImpor" border="0">
							<map name="imgImpor">
								<area onclick="Cargar(7, 0)" shape="RECT" coords="0,0,6,5">
								<area onclick="Cargar(7, 1)" shape="RECT" coords="0,6,6,11">
							</map>																				
				<label style="display:inline-block;width:5px" id='lblImpor' title='Importancia'>Impor.</label></td>	
				<td><img onmouseover="javascript:bMover=true;moverTablaUp()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
						height="8" src="../../../../images/imgFleUp.gif" width="11"></td>
			</tr>
		</table><br />
        <table id="tblGeneral" align="center">
        <tr>
	        <td>				
			<div id="divCatalogoDefi" align="left" style="overflow-x: hidden; overflow: auto; width: 996px; height: 484px">
                <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT22.gif);width: 980px;">
					<%=strTablaHtmlCatalogo%>
				</div>
			</div> 
			</td>
		</tr>
		</table>
		<table id="tblResultadoDefi" style="width: 980px;position: absolute; left: 10px;" height="17">
			<tr class="textoResultadoTabla">
				<td>&nbsp;<img height="1" src="../../../../images/imgSeparador.gif" width="955"> <img onmouseover="javascript:bMover=true;moverTablaDown()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
						height="8" src="../../../../images/imgFleDown.gif" width="11"></td>
			</tr>
			<tr height="5">
				<td><img onmouseover="javascript:bMover=true;moverTablaDown()" onmouseout="javascript:bMover=false;"
						height="7" src="../../../../images/imgSeparador.gif"></td>
			</tr>
		</table>
	    <div style="display:none">
		<asp:textbox id="hdnOpcion" runat="server" style="visibility:hidden" ></asp:textbox>
        </div>
				
</asp:Content>
<asp:Content ID="CPHDoPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "pdf": //Boton exportar pdf
				{
					bEnviar = false;
					Exportar('PDF');
					break;
				}
		        case "excel": 
		        {
			        bEnviar = false;
			        Excel();
			        break;
		        }		
		        case "volcar": 
		        {
			        bEnviar = false;
			        Volcar();
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
