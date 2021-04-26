<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";  
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
        var sAmbito = <%=sAmbito%>;
    </script>
    <center>
    <table style="width: 918px; text-align:left">
        <colgroup><col style="width:438px"/><col style="width:480px"/></colgroup>
        <tr>
            <td>
                <label id="lblNodo" style="width:438px" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
                <asp:DropDownList id="cboCR" runat="server" Width="350px" onChange="setCombo()" AppendDataBoundItems=true>
                <asp:ListItem Value="" Text=""></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDesNodo" style="width:390px;" Text="" readonly="true" runat="server" />
                <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" />
                <img id="gomaNodo" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle;visibility:hidden;" runat="server"></td>
            <td>
                <label id="lblCliente" style="width:60px" runat="server" class="texto" onclick="getCliente();">Cliente</label>
                <asp:TextBox ID="txtDesCliente" runat="server" Width="440px" readonly="true" />
                <input type="hidden" id="hdnIdCliente" value="" runat="server" style="width:1px;visibility:hidden" />
                <img id="gomaCli" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarCli()" style="cursor:pointer; vertical-align:middle;visibility:hidden;" title="Borra el cliente">
            </td>
        </tr>
        <tr>
            <td colspan="2">
		        <table style="width: 900px; height: 17px; margin-top:5px;">
		            <colgroup><col style="width:30px"/><col style="width:300px"/><col style="width:75px"/><col style="width:75px"/><col style="width:420px"/></colgroup>
			        <tr class="TBLINI">
				        <td></td>
				        <td>Denominación</td>
				        <td style="text-align:center">Activo</td>
				        <td>Obligatorio</td>
				        <td>Cliente</td>
			        </tr>
		        </table>
		        <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 916px; height:200px" onscroll="scrollTablaAE()">
		            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:900px">
		                <%=strTablaHtml %>
                    </div>
                </div>
		        <table style="width: 900px; height: 17px;">
			        <tr class="TBLFIN">
				        <td> </td>
			        </tr>
		        </table>
            </td>
        </tr>
    </table>
    <table style="width:240px; margin-top:5px; margin-left:20px;">
	<tr>
		<td>
			<button id="btnAnadir" type="button" onclick="nuevoAE()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			</button>	
		</td>
		<td>
			<button id="btnEliminar" type="button" onclick="preEliminarAE()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../../images/botones/imgEliminar.gif" /><span title="Borrar">Borrar</span>
			</button>	
		</td>
	</tr>
	</table>
    <table style="margin-left:15px; width: 338px; text-align:left">
    <tr>
        <td colspan="2"><br />
		    <table style="width: 320px; height: 17px">
		        <colgroup><col style="width:35px;"/><col style="width:235px;"/><col style="width:50px;"/></colgroup>
			    <tr class="TBLINI">
				    <td></td>
				    <td>Valores</td>
				    <td style="text-align:right;">Activo&nbsp;</td>
			    </tr>
		    </table>
		    <div id="divValores" style="overflow: auto; overflow-x: hidden; width: 336px; height:180px">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:320px">
		            <%=strTablaHtmlVAE %>
                </div>
            </div>
		    <table style="width: 320px; height: 17px">
			    <tr class="TBLFIN">
				    <td></td>
			    </tr>
		    </table>
        </td>
    </tr>
    </table>
    <table style="width:240px; margin-top:5px; margin-left:20px;">
	<tr>
		<td>
			<button id="btnAddVAE" type="button" onclick="nuevoVAE()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			</button>	
		</td>
		<td>
			<button id="btnDelVAE" type="button" onclick="eliminarVAE()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../../images/botones/imgEliminar.gif" /><span title="Borrar">Borrar</span>
			</button>	
		</td>
	</tr>
	</table>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    </center>
<input type="hidden" runat="server" name="hdnVAE" id="hdnVAE" value="" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        switch (strBoton) {
				case "grabar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("grabar();", 20);
					break;
				}
//				case "nuevo": 
//				{
//                    bEnviar = false;
//                    nuevoAE();
//					break;
//				}
//				case "eliminar": 
//				{
//                    bEnviar = false;
//					if (confirm("¿Estás conforme?")){
//                        preEliminarAE();
//                    }
//					break;
//				}
				case "guia": 
				{
                    bEnviar = false;
                    if (sAmbito=="T") mostrarGuia("CET.pdf");
                    else mostrarGuia("CEE.pdf");
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

