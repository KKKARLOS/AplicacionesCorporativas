<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Contratos_Proyectos" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<center>
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
    </tr>
    <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
        <!-- Inicio del contenido propio de la página -->
            <table id='tblCatalogo' class="texto" style="width:615px; text-align:left;">
            <tr>
                <td>
                    <FIELDSET style="width: 600px; height:60px; padding:5px;">
                    <LEGEND>
                        <label id="Label1" class="enlace" onclick="getCriterios(0)" runat="server">Proyecto</label>
                        <img id="Img1"  src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(0)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                    </LEGEND>
                        <table class="texto" style="text-align:left;" cellpadding="3px">
                            <tr>
                                <td>
                                    <label style="width:70px;">Proyecto</label>
                                    <asp:TextBox ID="txtNumPE" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;getCriterios(1);}else{vtn2(event);}"/>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtDesPE" readonly="true" style="width:400px;" Text=""  runat="server" />
								</td>
                            </tr> 
                            <tr>
                                <td>
					                <label style="width:71px;">Estado</label>
					                <asp:DropDownList id="cboEstado" runat="server" Width="100px" CssClass="combo" >
					                    <asp:ListItem Value="" Text=""></asp:ListItem>
					                    <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
					                    <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
					                    <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
					                    <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
					                </asp:DropDownList>
					                &nbsp;&nbsp;&nbsp;Categoría&nbsp;
                                    <asp:DropDownList id="cboCategoria" runat="server" Width="80px" CssClass="combo">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                        <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                    </asp:DropDownList>      
                                </td>
                            </tr>
                        </table>   				
					</FIELDSET>	
              	</td>
			</tr>
			<tr>	
				<td>
                    <FIELDSET style="width: 600px; height:40px; padding:5px; margin-top:5px;">
                    <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Contrato</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <table class="texto" style="text-align:left;">
                            <tr>
                                <td>
                                    <label style="width:70px;">Contrato</label>
                                    <asp:TextBox ID="txtIdContrato" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;NumContrato(this);}else{vtn2(event);}"/>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtDesContrato" style="width:400px;" Text="" readonly="true" runat="server" />
								</td>
                            </tr> 
                        </table>   				
					</FIELDSET>					
                </td>
            </tr>
            <tr>
                <td>
                    <FIELDSET style="width:600px; height:40px; padding:5px; text-align:left;  margin-top:5px;">
                    <LEGEND>
                        <label id="Label6" class="enlace" onclick="getCriterios(3)" runat="server">Cliente</label>
                        <img id="Img6"  src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                    </LEGEND>
                    	<label style="width:70px;">Denominación</label>
                    	<asp:TextBox ID="txtCliente" style="width:475px;" Text="" readonly="true" runat="server" /> 				
					</FIELDSET>	
              	</td>
			</tr>	            			
            <tr>
                <td>
                    <FIELDSET style="width: 600px; height:55px; padding:5px; margin-top:5px;">
                    <LEGEND>Responsables</LEGEND>
                        <table class="texto" style="text-align:left;" cellpadding="3px">
                        <tr>
                            <td>                            
                    	        <label id="lblProy" style="width:70px;" runat="server" onclick="getCriterios(4)" class="enlace">Proyecto</label>
                    	        <asp:TextBox ID="txtDesResponPE" style="width:475px;" Text="" readonly="true" runat="server" />
                    	        <img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"> 				
                            </td>
                        </tr>   
                         <tr>
                            <td>                            
                    	        <label id="lblContra" style="width:70px;" runat="server" onclick="getCriterios(5)" class="enlace">Contrato</label>
                    	        <asp:TextBox ID="txtDesResponCO" style="width:475px;" Text="" readonly="true" runat="server" />
                    	        <img id="Img4"  src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">				
                            </td>
                        </tr>   
                        </table>                    	
					</FIELDSET>	
              	</td>
			</tr>	
            <tr>
                <td>
                    <FIELDSET style="width: 600px; height:80px; padding:5px;  margin-top:5px;">
                    <LEGEND class="Tooltip" title="Número de pedido">&nbsp;Otros</LEGEND>     
                        <table class="texto" style="text-align:left;" cellpadding="3px">
                            <tr>
                                <td><label style="width:110px;">Nº Pedido Cliente</label><asp:TextBox ID="txtPedidoCliente" style="width:150px;"  Text=""  MaxLength="15" runat="server" />
                                </td>
							</tr> 
                            <tr>
								<td><label style="width:110px;">Nº Pedido Ibermática</label><asp:TextBox ID="txtPedidoIbermatica" style="width:150px;"  Text="" MaxLength="15" runat="server" />
								</td>
							</tr> 
                            <tr>
 								<td>
				                <label id="lblNodo" style="width:107px;" runat="server" class="texto">Nodo</label>
				                <asp:DropDownList ID="cboCR" runat="server" AppendDataBoundItems="true">
					                <asp:ListItem Value="" Text=""></asp:ListItem>
				                </asp:DropDownList>								
								</td>                           
							</tr> 
                        </table>         
                    </FIELDSET>	
                </td>
            </tr>
			<tr>
				<td>
				    <center>
					<FIELDSET id="fldRtdo" class="fld" style="width:270px;text-align:left; margin-top:5px;" runat="server">
						<LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
							<table class="texto" style="width:260px; text-align:left;">
							    <colgroup><col style="width:100px;"/><col style="width:160px;"/></colgroup>
	                            <tr>
	                                <td colspan="2">
                                        <div id="divMonedaImportes" runat="server" style="visibility:hidden">
                                            <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">
                                            Importes</label> en <label id="lblMonedaImportes" style="width:215px; display:inline;" runat="server">Dólares americanos</label>
                                        </div>
	                                </td>
	                            </tr>									
								<tr>
									<td style="text-align:left;">
										<img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
									</td>
									<td style="vertical-align:top;">
										<FIELDSET id="FIELDSET1" class="fld" style="height:30px; width:50px; text-align:left; margin-left:40px;" runat="server"> 
										    <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
										    <img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer; margin-left:20px; margin-top:2px;" title="Excel" />
										</FIELDSET>
										<br /> 							
                                        <button id="btnObtener" type="button" onclick="obtenerDatosExcel()" class="btnH25W85" style="margin-left:33px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                            <img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                                        </button>    
									</td>
								</tr> 
							</table> 						
					</FIELDSET>	
					</center>  
				</td>				
			</tr>	
        </table>
	    <!-- Fin del contenido propio de la página -->
	    </td>
        <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
    </tr>
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
        <td height="6" background="../../../../Images/Tabla/2.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
    </tr>
</table>
</center>

<asp:textbox id="hdnIdPE" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnIdCliente" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnIdResponPE" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnIdResponCO" runat="server" style="visibility:hidden;"></asp:textbox>
</asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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