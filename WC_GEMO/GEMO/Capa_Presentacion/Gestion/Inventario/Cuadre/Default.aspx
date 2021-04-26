<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<table style="width:970px;" align="center">
    <colgroup>
        <col style="width:470px;" />
        <col style="width:30px;" />
        <col style="width:470px;" />
    </colgroup>
	<tr>
	    <td>
				<table id="tblTitulo1" style="WIDTH: 430px; height:17px; margin-top:2px;">
                    <colgroup>
                        <col style='width:230px;' />
                        <col style='width:200px;text-align:right' />
                    </colgroup>            				
				    <tr>
				        <td colspan="2">&nbsp;Líneas facturadas que no están inventariadas&nbsp;</td>
				    </tr>
					<tr class="TBLINI">
						<td >&nbsp;Línea&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatosCaso1',0,'divCatalogoCaso1','imgLupa1')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatosCaso1',0,'divCatalogoCaso1','imgLupa1',event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
                        <td>&nbsp;Importe facturado</td>						
					</tr>
				</table>
				<div id="divCatalogoCaso1" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 446px; HEIGHT: 220px;">
                    <div style='background-image:url(../../../../Images/imgFT20.gif); width:430px'>
                    <%=strTablaHtmlCaso1%> 
					</div>
                </DIV>
                <table style="WIDTH: 430px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
                <table style="WIDTH: 430px; HEIGHT: 17px">
                    <tr>
                        <TD>
                            <button id="btnInventariar" type="button" onclick="FacturadasNoInventariadas()" class="btnH25W115" hidefocus=hidefocus oonmouseover="se(this, 25);mostrarCursor(this);" runat="server">
                                <img src="../../../../Images/botones/imggrabar.gif" /><span title="Pre-Activas">&nbsp;Pre-Activas</span>
                            </button>                   
                        </TD>
                    </tr>
                </TABLE>

	    </td>
	    <td></td>
	    <td>
				<table id="tblTitulo2" style="WIDTH: 430px; height:17px; margin-top:2px;">
                    <colgroup>
                        <col style='width:430px;' />
                    </colgroup>   				
				    <tr>
				        <td>&nbsp;Líneas del inventario en estado 'activas' de las que no hemos recibido factura&nbsp;
				        </td>
					</tr>
					<tr class="TBLINI">
						<td >&nbsp;Línea&nbsp;
							<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatosCaso2',0,'divCatalogoCaso2','imgLupa2')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatosCaso2',0,'divCatalogoCaso2','imgLupa2',event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
					</tr>
				</table>
				<div id="divCatalogoCaso2" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 446px; HEIGHT: 220px;">
                    <div style='background-image:url(../../../../Images/imgFT20.gif); width:430px'>
                    <%=strTablaHtmlCaso2%> 
					</div>
                </DIV>
                <table style="WIDTH: 430px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
                <table style="WIDTH: 430px; HEIGHT: 17px">
                    <tr>
                        <TD>
							<button id="btnActivasSinFactura" type="button" onclick="ActivasSinFactura()" class="btnH25W115" hidefocus=hidefocus onmouseover="se(this, 25);mostrarCursor(this);" runat="server">
							  <img src="../../../../Images/botones/imggrabar.gif" /><span title="Pre-inactivas">&nbsp;Pre-inactivas</span>								
                            </button>                             
                        </TD>
                    </tr>                    
                </TABLE>
	    </td>
	</tr>

	<tr>
	    <td>
			<table id="tblTitulo3" style="WIDTH: 430px; height:17px; margin-top:2px;">
                    <colgroup>
                        <col style='width:230px;' />
                        <col style='width:200px;text-align:right' />
                    </colgroup>            				
				    <tr>
				        <td colspan="2">&nbsp;Líneas en estado 'inactiva' o 'bloqueada' de las que hemos recibido factura</td>
				    </tr>
					<tr class="TBLINI">
						<td >&nbsp;Línea&nbsp;			
						<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatosCaso3',0,'divCatalogoCaso3','imgLupa3')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatosCaso3',0,'divCatalogoCaso3','imgLupa3',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
                    <td>&nbsp;Importe facturado</td>					
				</tr>
			</table>
			<div id="divCatalogoCaso3" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 446px; HEIGHT: 220px;">
                <div style='background-image:url(../../../../Images/imgFT20.gif); width:430px'>
                <%=strTablaHtmlCaso3%> 
				</div>
            </DIV>
            <table style="WIDTH: 430px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>

	    </td>
	    <td></td>
	    <td>
			<table id="tblTitulo4" style="WIDTH: 430px; height:17px; margin-top:2px;">
                    <colgroup>
                        <col style='width:230px;' />
                        <col style='width:200px;text-align:right' />
                    </colgroup>            				
				    <tr>
				        <td colspan="2">&nbsp;Incidencias a revisar sobre líneas de voz, datos, voz-datos</td>
				    </tr>
					<tr class="TBLINI">
					<td >&nbsp;Línea&nbsp;					
						<IMG id="imgLupa4" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatosCaso4',0,'divCatalogoCaso4','imgLupa4')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatosCaso4',0,'divCatalogoCaso4','imgLupa4',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
					<td>&nbsp;Importe facturado</td>
				</tr>
			</table>
			<div id="divCatalogoCaso4" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 446px; HEIGHT: 220px;">
                <div style='background-image:url(../../../../Images/imgFT20.gif); width:430px'>
                <%=strTablaHtmlCaso4%> 
				</div>
            </DIV>
            <table style="WIDTH: 430px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>

	    </td>
	</tr>
</TABLE>
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript">
        function __doPostBack(eventTarget, eventArgument) {
            var bEnviar = true;
            if (eventTarget.split("$")[2] == "Botonera") {
                var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			    switch (strBoton){
				    case "eliminar": 
				    {
                        bEnviar = false;
                        eliminar();
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

