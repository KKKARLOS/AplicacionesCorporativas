<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table style="width:566px; text-align:left;">
	<tr>	
		<td>
            <fieldset style="width: 550px; height:40px;">
            	<legend>Criterios de búsqueda</legend>
                <table style='margin-top:5px;width:550px'>
                    <colgroup><col style="width:360px"/><col style="width:190px"/></colgroup>
                    <tr>
                        <td><label style="width:40px;">Contrato</label>&nbsp;&nbsp;<asp:TextBox ID="txtIdContrato" style="width:50px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;NumContrato(this);}else{vtn2(event);$I('txtCadena').value='';}"/>&nbsp;&nbsp;<asp:TextBox ID="txtCadena" runat="server" Width="240px" onKeyPress="javascript:if(event.keyCode==13){buscar(this.value);event.keyCode=0;}else{$I('txtIdContrato').value='';}"></asp:TextBox>
						</td>
						<td onclick="buscar($I('txtCadena').value);">
							<asp:RadioButtonList ID="rdbTipo"  SkinID="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda">
								<asp:ListItem Value="I"><img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$I('rdbTipo_0').checked=true;buscar($I('txtCadena').value);"></asp:ListItem>
								<asp:ListItem Selected="true" Value="C"><img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$I('rdbTipo_1').checked=true;buscar($I('txtCadena').value);"></asp:ListItem>
							</asp:RadioButtonList>
                    	</td>
                    </tr> 
                </table>   				
			</fieldset>					
        </td>
    </tr>
 <tr>
 <td>	
 <table style="width:566px;">
	<tbody>
		<tr>
			<td colspan="2">
				<table id="tblTitulo"  style="margin-top:10px;height:17px;width:550px">
					<tr class="TBLINI">
						<td>&nbsp;Denominación&nbsp;
						    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							<IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
					</tr>
				</table>
		    </td>
		</tr>
        <tr>
            <td colspan="2">
                <div id="divCatalogo" style="overflow: auto; width: 566px; height: 450px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:550px">
					    <table id="tblDatos" style="WIDTH: 546px;">
	                    </table>
	                </div>
                </div>
                <table id="tblResultado" style="height:17px;width:550px">
	                <tr class="TBLFIN">
		                <td>&nbsp;</td>
	                </tr>
                </table>
            </td>
        </tr>
</tbody>
</table>
</td>
</tr>	
</table>
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
	        //alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminar();
					break;
				}
			    case "poolfvp":
			        {
			            bEnviar = false;
			            PoolFVP();
			            break;
			        }
			    case "duplicar":
			        {
			            bEnviar = false;
			            generarContratos();
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

