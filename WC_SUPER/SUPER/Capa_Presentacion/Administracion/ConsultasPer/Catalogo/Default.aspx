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
	    var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;
    <%=strParametros %>
	    
    </script>
    <center>
    <table style="width:960px;text-align:left" align="center">
        <tr>
            <td align="right" style="padding-right:18px">
                <asp:CheckBox ID="chkActivas" runat="server" style="cursor:pointer;width:300px;" Text="Mostrar inactivas" onclick="buscar();" TextAlign=Left ToolTip="Permite visualizar consultas desactivadas." />
            </td>
        </tr>
        <tr>
            <td>
		        <table style="width: 940px; height: 17px">
		            <colgroup>
		                <col style="width:10px"/><col style="width:300px"/><col style="width:285px"/>
		                <col style="width:30px"/><col style="width:200px"/><col style="width:115px"/>
		            </colgroup>
			        <tr class="TBLINI">
				        <td></td>
				        <td>Denominación</td>
				        <td>Procedimiento almacenado</td>
				        <td title="Estado">Est.</td>
				        <td>Comentario</td>
				        <td title="Clave para acceso a la consulta mediante servicio web">Clave WS</td>
			        </tr>
		        </table>
		        <div id="divCatalogo" style="OVERFLOW: auto; WIDTH: 956px; height:200px">
		            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:940px">
		                <%=strTablaHTML%>
                    </div>
                </div>
		        <table style="width: 940px; height: 17px">
			        <tr class="TBLFIN">
				        <td> </td>
			        </tr>
		        </table>
            </td>
        </tr>
    </table>
	<table style="width:250px;margin-top:10px;" align="center">
	<tr>
		<td width="45%">
			<button id="btnNuevo" type="button" onclick="nuevoCons()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			</button>	
		</td>
		<td width="10%"></td>
		<td width="45%">
			<button id="btnCancelar" type="button" onclick="EliminarConsulta()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			</button>	
		</td>
	</tr>
	</table>    
    <br />
    <fieldset style="height:240px; width:925px; margin-left:4px;">
    <legend>Parámetros de la consulta</legend>
    <table cellspacing="5" style="width: 923px; text-align:left">
    <tr>
        <td colspan="3">
		    <table style="width: 905px; height: 17px">
	            <colgroup><col style="width:40px"/><col style="width:125px"/>
	            <col style="width:125px"/><col style="width:75px"/><col style="width:240px"/>
                <col style="width:180px"/><col style="width:90px"/><col style="width:30px"/>	            
	            </colgroup>		    
			    <tr class="TBLINI">
				    <td></td>
				    <td>Nombre</td>
				    <td>Denominación</td>
				    <td>Tipo</td>
				    <td>Comentario</td>
				    <td>Valor defecto</td>
				    <td>Característica</td>
				    <td title="Opcional">Op.</td>
			    </tr>
		    </table>
		    <div id="divValores" style="OVERFLOW: auto; width: 923px; height:160px">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:905px">
		            <table id='tblDatosParam' border='1'></table>
                </div>
            </div>
		    <table style="width: 905px; height: 17px">
			    <tr class="TBLFIN">
				    <td></td>
			    </tr>
		    </table>
        </td>
    </tr>
    </table>
	<table style="width:250px;margin-top:5px;">
	<tr>
		<td width="45%">
			<button id="Button1" type="button" onclick="nuevoParam()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			</button>	
		</td>
		<td width="10%"></td>
		<td width="45%">
			<button id="Button2" type="button" onclick="eliminarParam()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			</button>	
		</td>
	</tr>
	</table>
    </fieldset>
    </center>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <div style="display:none">
        <asp:DropDownList id="cboTipoParam" onchange="actualizarDatos('U','tipo',this);" runat="server" Width="70px" AutoPostBack="false" CssClass="combo">        		                
            <asp:ListItem Value="I">Entero</asp:ListItem>		                            	
            <asp:ListItem Value="V">Varchar</asp:ListItem>		                            	                            	
            <asp:ListItem Value="M">Money</asp:ListItem>		                            	
            <asp:ListItem Value="D">Date</asp:ListItem>		                            	                            	
            <asp:ListItem Value="B">Boolean</asp:ListItem>		                            	                            	
            <asp:ListItem Value="A">Añomes</asp:ListItem>		                            	                            	
        </asp:DropDownList>
        <asp:DropDownList id="cboVisible" onchange="actualizarDatos('U','visible',this);" runat="server" Width="85px" AutoPostBack="false" CssClass="combo">        		                
            <asp:ListItem Value="M">Modificable</asp:ListItem>		                            	
            <asp:ListItem Value="N">No visible</asp:ListItem>		                            	
            <asp:ListItem Value="V">Visible</asp:ListItem>		                            	                            	
        </asp:DropDownList>
        <input type="hidden" name="hdnIdCons" id="hdnIdCons" value="" runat="server" />
        <input type="hidden" name="hdnParams" id="hdnParams" value="" runat="server" />
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
				case "comentariobot": 
				{
					bEnviar = false;
					mostrarComentario();
					break;
				}
				case "grabar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("grabar();", 20);
					break;
				}
				case "nuevo": 
				{
                    bEnviar = false;
                    nuevoCons();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;
					//if (confirm("¿Estás conforme?")){
                        EliminarConsulta();
                    //}
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    //mostrarGuia("CEE.pdf");
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
