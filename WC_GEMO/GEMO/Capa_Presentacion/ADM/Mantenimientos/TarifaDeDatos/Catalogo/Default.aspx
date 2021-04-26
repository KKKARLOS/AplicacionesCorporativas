<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="../../../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
   <center>
		 <table style="width:562px;text-align:left">
			<TBODY>
				<tr>
					<td colspan="2">
						<table id="tblTitulo" style="margin-top:10px;width:600px;height:17px;text-align:left">
						    <colgroup><col width="310px"/><col width="290px"/></colgroup>
							<tr class="TBLINI">
								<td style="padding-left:15px">Denominación&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
										height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
										height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">
								</td>
								<td>Proveedor</td>								
							</tr>
						</table>
				    </td>
				</tr>
                <tr>
                    <td colspan="2">
                        <div id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 616px; HEIGHT: 460px" align="left" name="divCatalogo">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:600px">
                                <%=strTablaHtml%> 
    		                </div>
		                </DIV>
		                <table id="tblResultado" height="17" width="600px" align="left">
			                <tr class="TBLFIN">
				                <td>&nbsp;</td>
			                </tr>
		                </table>
		            </TD>
		        </TR>
		</TBODY>
		</TABLE>
        <table style="margin-top:20px; width:300px;">
            <tr>
                <td>
                    <button id="btnAnadir" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                     onmouseover="se(this, 25);mostrarCursor(this);">
	                    <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añade un elemento nuevo" >Añadir</span>
                    </button>	
                </td>
                <td>
                    <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                     onmouseover="se(this, 25);mostrarCursor(this);">
	                    <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Borra el elemento seleccionado">Eliminar</span>
                    </button>	
                </td>
            </tr>
        </table> 		
    </center>
   <uc_mmoff:mmoff ID="mmoff1" runat="server" />
   <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />    
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "nuevo": //Boton Nuevo
				{
				    bEnviar = false;
					Nuevo();
					break;
				}
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
-->
</script>
</asp:Content>

