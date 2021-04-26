<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <center>
        <table style="width:562px;text-align:left;">
        <tr>
         <td>
		    <fieldset style="width:546px;">
		        <legend>Criterios de búsqueda</legend>
		        <table align="center">
			        <tr>
			            <td><label style="width:50px;">Proveedor</label>&nbsp;<asp:TextBox ID="txtCodExterno" style="width:60px;" Text="" SkinID="Numero"  runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscar();}else{vtn2(event);$I('txtCadena').value='';}"/>&nbsp;&nbsp;<asp:TextBox ID="txtCadena" runat="server" Width="270px" onKeyPress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}else{$I('txtCodExterno').value='';}" onfocus="this.select();"></asp:TextBox>
                        </td>
                        <td>
                        <asp:RadioButtonList ID="rdbTipo" onclick="Mostrar();" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda">
                            <asp:ListItem Value="I"><img src='../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" onclick="seleccionar($I('rdbTipo_0'));" style="cursor:pointer"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="C"><img src='../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="seleccionar($I('rdbTipo_1'));"></asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
		    </fieldset>
         </td>
        </tr>	
        <tr>
         <td>	
		 <table width="562px">
			<tbody>
			    <tr>
                    <td align="right">
                        <br />
                        <asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar inactivos" />&nbsp;
                        <input type="checkbox" id="chkMostrarInactivos" class="check" onclick="Mostrar();" style="margin-right:38px;" />
                    </td>
                </tr>
				<tr>
					<td>
						<table id="tblTitulo" height="17px" width="546px" align="left" name="tblTitulo" style="margin-top:10px;">
						    <colgroup><col style="width:80px"/><col style="width:400px"/><col style="width:66px"/></colgroup>
							<tr class="TBLINI">
					            <td title="Código externo"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event)"
							        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2')"
							        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
							        &nbsp;Cód.
					            </td>
					            <td>Denominación&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
										height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
										height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
								<td align="center"><label title="Activa control de huecos sobre los colaboradores de ese Proveedor">CH&nbsp;</label></td>
							</tr>
						</table>
				    </td>
				</tr>
                <tr>
                    <td>
                        <div id="divCatalogo" style="OVERFLOW: auto; WIDTH: 562px; HEIGHT: 447px" align="left" name="divCatalogo">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:546px">
							<table id="tblDatos" style="WIDTH: 546px;">
    		                </table>
    		                </div>
		                </div>
		                <table id="tblResultado" height="17" width="546px" align="left"
			                border="0" name="tblResultado">
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
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    setTimeout("grabar();", 20);
					break;
				}					
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("Proveedores.pdf");
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

