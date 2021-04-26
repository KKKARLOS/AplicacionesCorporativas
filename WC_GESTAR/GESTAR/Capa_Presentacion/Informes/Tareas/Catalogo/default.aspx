<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GESTAR.Capa_Presentacion.ASPX.CatalogoProp" Title="Catálogo" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<style>.Catalogo {
overflow-x: hidden; overflow: auto
}
</style>
	<table id="tblFiltros" style="width: 880px; height:17px" >
	<tr>
		<td>
			<asp:RadioButtonList  ID="rdlInforme"  runat="server" CssClass="texto"  RepeatDirection="Horizontal" RepeatLayout="Flow">
				<asp:ListItem Selected="True" Value="0">Relación</asp:ListItem>
				<asp:ListItem  Value="1">Detalle</asp:ListItem>
			</asp:RadioButtonList>
		</td>
		<td>
			<button id="btnMarcar" type="button" onclick="mTabla(1)" class="btnH25W110" runat="server" hidefocus="hidefocus" 
					onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgmarcar.gif" /><span title="Marcamos los checks de todas las filas">Marcar todos</span>
			</button>	
		</td>
		<td>
			<button id="btnDesmarcar" type="button" onclick="mTabla(2)" class="btnH25W140" runat="server" hidefocus="hidefocus" 
					onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgdesmarcar.gif" /><span title="Desmarcamos los checks de todas las filas">Desmarcar todos</span>
			</button>	
		</td>
	</tr>
	</table>
    <br />
	<table id="tblTitulo" style="width: 880px; height:17px" border="0">
        <colgroup>
            <col style='width:25px;' />
            <col style='width:215px;' />
            <col style='width:215px;' />
            <col style='width:160px;' />
            <col style='width:105px;' />
            <col style='width:80px;' />
            <col style='width:80px;' />
        </colgroup>
		<tr class="TBLINI">
            <td></td>
			<td>&nbsp;&nbsp;<img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogo',1,'divCatalogo','imgLupa1',event)"
					height="11" src="../../../../images/imgLupa.gif" width="20"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogo',1,'divCatalogo','imgLupa1')"
					height="11" src="../../../../images/imgLupaMas.gif" width="20">
					<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgTarea" border="0">
							<map name="imgTarea">
							<area onclick="Cargar(1, 0)" shape="RECT" coords="0,0,6,5">
							<area onclick="Cargar(1, 1)" shape="RECT" coords="0,6,6,11">
						</map>																	
					&nbsp;Tarea</td>
			<td><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogo',2,'divCatalogo','imgLupa2',event)"
					height="11" src="../../../../images/imgLupa.gif" width="20"> <img id="imgLupa2" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogo',2,'divCatalogo','imgLupa2')"
					height="11" src="../../../../images/imgLupaMas.gif" width="20">
					<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgOrden" border="0">
							<map name="imgOrden">
							<area onclick="Cargar(2, 0)" shape="RECT" coords="0,0,6,5">
							<area onclick="Cargar(2, 1)" shape="RECT" coords="0,6,6,11">
						</map>									
					&nbsp;Orden</td>								
			<td>
					<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgArea" border="0">
							<map name="imgArea">
							<area onclick="Cargar(3, 0)" shape="RECT" coords="0,0,6,5">
							<area onclick="Cargar(3, 1)" shape="RECT" coords="0,6,6,11">
						</map>																					
					Área</td>																						
			<td>
					<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgAvance" border="0">
							<map name="imgAvance">
							<area onclick="Cargar(4, 0)" shape="RECT" coords="0,0,6,5">
							<area onclick="Cargar(4, 1)" shape="RECT" coords="0,6,6,11">
						</map>																											
					Avance</td>
			<td>
					<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgResultado" border="0">
							<map name="imgResultado">
							<area onclick="Cargar(5, 0)" shape="RECT" coords="0,0,6,5">
							<area onclick="Cargar(5, 1)" shape="RECT" coords="0,6,6,11">
						</map>										
					Resultado</td>	
			<td>
					<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgffinprev" border="0">
							<map name="imgffinprev">
							<area onclick="Cargar(6, 0)" shape="RECT" coords="0,0,6,5">
							<area onclick="Cargar(6, 1)" shape="RECT" coords="0,6,6,11">
						</map>							
					<label title='Fecha fin prevista' id='lblfecha' runat="server"><%=strFecha%></label></td>
		</tr>
	</table>
	<div id="divCatalogo" align="left" style="overflow-x: hidden; overflow: auto; width: 896px; height: 458px;">
		<div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:880px;">
			<%=strTablaHtmlCatalogo%>
		</div>
	</div> 
	<table id="tblResultadoDefi" style="width: 880px; height:17px">
		<tr class="TBLFIN">
			<td><img height="1" src="../../../../images/imgSeparador.gif" ></td>
		</tr>
	</table>
	<div style="display:none">
	    <asp:textbox id="hdnOpcion" runat="server" style="visibility:hidden" ></asp:textbox>
    </div>
</asp:Content>
<asp:Content ID="CPHDoPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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
</script>
</asp:Content>
