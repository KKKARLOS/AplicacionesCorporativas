<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Detalle de orden de facturación tramitada</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bSalir = false;
        var bLectura = true;
    -->
    </script>
    <table style="width:920px;text-align:left;margin-left:20px;margin-top:10px">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
				MultiPageID="mpContenido" 
				ClientSideOnLoad="CrearPestanas" 
				ClientSideOnItemClick="getPestana">
		        <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
			        <Items>
					        <eo:TabItem Text-Html="Cabecera" ToolTip="" Width="100"></eo:TabItem>
					        <eo:TabItem Text-Html="Posiciones" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Documentación" ToolTip="" Width="100"></eo:TabItem>
			        </Items>
		        </TopGroup>
	            <LookItems>
			            <eo:TabItem ItemID="_Default" 
			                LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
			                LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
			                LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
			                Image-Url="~/Images/Pestanas/normal_bg.gif"
			                Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
			                Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
				            RightIcon-Url="~/Images/Pestanas/normal_right.gif"
				            RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
				            RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
				            NormalStyle-CssClass="TabItemNormal"
				            HoverStyle-CssClass="TabItemHover"
				            SelectedStyle-CssClass="TabItemSelected"
				            DisabledStyle-CssClass="TabItemDisabled"
				            Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
			            </eo:TabItem>
	            </LookItems>
			</eo:TabStrip>
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="520px">				
			    <eo:PageView ID="PageView1" CssClass="PageView" runat="server" align="center">						    
				<!-- Pestaña 1 General-->
                <table id="tabla" style="width:880px; padding:5px; margin:10px; text-align:left">
                    <colgroup>
                        <col style="width:135px;" />
                        <col style="width:475px;" />
                        <col style="width:100px;" />
                        <col style="width:170px;" />
                    </colgroup>
                    <tr>
                        <td style="text-align:left;"><label id="lblProy" runat="server" title="Proyecto económico" style="width:95px;">Proyecto</label></td>
                        <td><asp:TextBox ID="txtNumPE" runat="server" Text="" SkinID="numero"  style="width:80px;"  readonly="true" />&nbsp;&nbsp;
                            <asp:TextBox ID="txtDesPE" runat="server" Text="" style="width:350px;" readonly="true" /></td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align:top;">
                            <fieldset style="width:590px;">
                            <legend>Cliente</legend>
                            <table class="texto" style="width:500px;" cellpadding="4">
                            <colgroup>
                                <col style="width: 126px;" />
                                <col style="width: 454px;" />
                            </colgroup>
                             <tr>
                                <td><label id="lblCliPago" title="Cliente responsable del pago de la factura">Responsable de pago</label></td>
                                <td><asp:TextBox ID="txtNIFCliPago" style="width:80px;" Text="" runat="server" readonly="true" />&nbsp;&nbsp;<asp:TextBox ID="txtDesCliRespPago" style="width:350px;" Text="" ReadOnly=true runat="server" /></td>
                            </tr> 
                           </table>
                            </fieldset>
                            <fieldset style="width: 590px;margin-top:10px">
                                <legend>Destinatario de factura</legend>
                                <table border="0" cellpadding="4" style="width: 500px;">
                                    <colgroup>
                                        <col style="width: 130px;" />
                                        <col style="width: 450px;" />
                                    </colgroup>
                                    <tr>
                                        <td><label  id="lblCliFra" title="Cliente destinatario de la factura">Destinatario de factura</label></td>
                                        <td><asp:TextBox ID="txtDesClienteDestFac" style="width:430px;" Text="" readonly="true" runat="server" />
                                        <asp:TextBox ID="txtNIFCliDestFac" style="width:0px; visibility:hidden;" Text="" runat="server" readonly="true" />
                                        </td>
                                    </tr> 
                                    <tr style="height:50px; vertical-align:top;">
                                        <td>Dirección</td>
                                        <td id="cldDireccion" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td><label id="Label2" style="width:80px;" runat="server" class="texto">A la atención de</label></td>
                                        <td><asp:textbox id="txtComentarios" onkeyup="aG(0);" MaxLength="50" runat="server" style="width:430px" /></td>
                                    </tr>
                                </table>
                            </fieldset>     
                            <fieldset style="width: 590px;margin-top:10px">
                                <legend>Condiciones de pago</legend>
                                <table border="0" cellpadding="4" style="width: 500px;">
                                    <colgroup>
                                        <col style="width: 130px;" />
                                        <col style="width: 450px;" />
                                    </colgroup>
                                    <tr>
                                        <td><label id="lblPlazo" runat="server" class="texto" >Plazo</label></td>
                                        <td><asp:TextBox ID="txtCondPago" style="width:260px;" Text="" runat="server" readonly="true" /></td>	            
                                    </tr>
                                    <tr>             
                                        <td><label id="lblViaPago" runat="server" title="Vía de pago para la factura" class="texto" >Vía</label></td>
                                        <td><asp:TextBox ID="txtViaPago" style="width:260px;" Text="" runat="server" readonly="true" /></td>	            
                                    </tr>  
                                    <tr>            
                                        <td><label id="lblMoneda" runat="server" class="texto" >Moneda</label></td>
                                        <td><asp:TextBox ID="txtMoneda" style="width:260px;" Text="" runat="server" readonly="true" /></td>	            
                                    </tr> 
                                </table>
                            </fieldset>
                            <fieldset style="width: 590px; height:67px; margin-top:10px">
                                <legend>Pedido</legend>
                            <table cellpadding="4" style="width: 500px;">
                                <colgroup>
                                    <col style="width: 130px;" />
                                    <col style="width: 450px;" />
                                </colgroup>
                             <tr>             
                                <td><label id="lblRefCli" runat="server" class="texto" title="Referencia del pedido en el cliente">Referencia en el cliente</label></td>
                                <td><asp:TextBox ID="txtRefCli" style="width:260px;" Text="" runat="server" ReadOnly=True /></td>
                             </tr>  
                            </table>
                            </fieldset>
                        </td>
                        <td colspan="2" rowspan="2" style="vertical-align:top;">
                            <fieldset style="width:250px;">
                            <legend>Orden</legend>
                            <table class="texto" style="width:230px;" cellpadding="5">
                            <colgroup>
                                <col style="width: 100px;" />
                                <col style="width: 130px;" />
                            </colgroup>
                                <tr>
                                    <td>Número</td>
                                    <td><asp:TextBox ID="txtIdOrden" SkinID="numero" runat="server" style="width:60px;" Text="" readonly /></td>
                                </tr>
                            </table>
                            </fieldset>
                            <br /><br />
                            <fieldset style="width:250px;margin-top:10px">
                            <legend>Fechas</legend>
                            <table style="width:230px;" cellPadding="5">
                                <colgroup>
                                    <col style="width: 100px;" />
                                    <col style="width: 130px;" />
                                </colgroup>
                                <tr>
                                    <td><label id="lblFecPrevEmFac" runat="server" class="texto" title="Fecha prevista de emisión de factura">Emisión factura</label></td>
                                    <td><asp:TextBox ID="txtFecPrevEmFac" runat="server" style="width:60px;" Text="" readonly /></td>
                                </tr>
                                <tr>
                                    <td><label id="lblDiferida" style="width:80px;" runat="server" class="texto" title="Fecha en la que la orden pasará a SAP para su proceso. Si no se indica, pasará inmediatamente tras su tramitación.">Proceso diferido</label></td>
                                    <td><asp:TextBox ID="txtFecDiferida" runat="server" style="width:60px;" Text="" readonly /></td>
                                </tr>
                            </table>
                            </fieldset><br /><br />
                            <fieldset style="width:250px; padding:5px; margin-top:10px">
                                <legend>Texto cabecera de factura</legend>
                                <asp:textbox id="txtCabecera" runat="server" SkinID="Multi" style="width:240px" TextMode="MultiLine" Rows="10"></asp:textbox>
                            </fieldset>
                        </td>
                        
                    </tr>
                    <tr>
                        <td><br /><label id="lblOV" runat="server" title="Sociedad que factura" class="texto">Sociedad que factura</label></td>
                        <td><br /><asp:TextBox ID="txtOV" style="width:260px;" Text="" runat="server" readonly="true" /></td>	            
                        <td colspan="2"></td>
                    </tr> 
                    <tr>  
                        <td style="vertical-align:top;"><br /><label id="Label1" runat="server" class="texto">Indicaciones a Pool</label></td>           
                        <td style="vertical-align:top;"><br /><asp:textbox id="txtObsPool" runat="server" SkinID="Multi" style="width:455px" TextMode="MultiLine" Rows="3" readonly="true"></asp:textbox></td>
                        <td>&nbsp;</td>
                        <td style="vertical-align:bottom;"><br /><span id="spanAgrupacion" style="visibility:hidden;"><label id="lblAgrupacion" runat="server" title="Clave de agrupación">Agrupación</label>
                        <asp:TextBox ID="txtClaveAgru" style="width:60px;" SkinID="numero" Text="" runat="server" readonly="true" /></span>
                        </td>
                    </tr>                
                    <tr>
                        <td>
                            <label id="lblTramitadaPor1" style="margin-top:5px;" runat="server"></label>
                        </td>
                        <td>
                            <label id="lblTramitadaPor2" style="margin-top:5px;" runat="server"></label>
                        </td>
                    </tr>                        
                </table>
                </eo:PageView>
                <eo:PageView ID="PageView2" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 2 Posiciones-->
                <br />
                <table style="width:900px" align="center">
                <colgroup>
		            <col style="width:900px;" />
		        </colgroup>
                    <tr>
	                    <td>
		                    <table id="Table4" style="width: 880px; height: 17px">
		                        <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
	                                <col style="width:425px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
		                            <col style="width:100px;" />
		                        </colgroup>
			                    <tr class="TBLINI">
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;Concepto / Observaciones</td>
				                    <td style="padding-left:7px;">Unidades</td>
				                    <td style="padding-left:25px;">Precio</td>
				                    <td style="padding-left:15px;">Dto. %</td>
				                    <td style="padding-left:15px;">Dto. &euro;</td>
				                    <td style="text-align:right; padding-right:15px;">Importe</td>
			                    </tr>
		                    </table>
		                    <div id="divPosiciones" style="overflow: auto; overflow-x: hidden; width: 896px; height:370px" runat="server">
		                        <div style='background-image:url(../../../../Images/imgFT38.gif); width:880px'>
		                        <table id="tblPosiciones" class="MANO" style="width: 880px;" cellpadding="2" border="1">
		                            <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
	                                <col style="width:425px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
		                            <col style="width:100px;" />
		                            </colgroup>
		                        </table>
		                        </div>
                            </div>
		                    <table id="tblSubtotal" style="width: 880px;">
		                            <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
	                                <col style="width:425px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
		                            <col style="width:100px;" />
		                            </colgroup>
	                            <tr class="TBLFIN">
	                                <td colspan="6"></td>
	                                <td>Subtotal</td>
	                                <td style="text-align:right; padding-right:3px;"><label id='lblSubtotal'>0,00</label></td>
	                            </tr>
		                    </table>
		                    <table id="tblTotal" class="texto" style="width: 880px;" cellpadding="3">
		                        <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
	                                <col style="width:425px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
		                            <col style="width:100px;" />
		                            </colgroup>
	                            <tr>
	                                <td colspan="3" rowspan="2">
	                                </td>
	                                <td>Descuento</td>
	                                <td><asp:TextBox ID="txtDtoPorc" runat="server" style="width:50px;" Text="" SkinID="numero" onfocus="fn(this)" onkeyup="if (bLectura) return;aG(0);$I('txtDtoImporte').value='';sit();" /> %</td>
	                                <td><asp:TextBox ID="txtDtoImporte" runat="server" style="width:55px;" Text="" SkinID="numero" onfocus="fn(this)" onkeyup="if (bLectura) return;aG(0);$I('txtDtoPorc').value='';sit();" /> &euro;</td>
	                                <td></td>
	                                <td style="text-align:right;"><label id='lblDto'>0,00</label></td>
	                            </tr>
	                            <tr>
	                                <td>IVA incluido</td>
	                                <td><asp:CheckBox id="chkIVA" runat="server" style="vertical-align:middle;cursor:pointer;margin-left:-4px;" Enabled=false />
	                                </td>
	                                <td></td>
	                                <td style="padding-left:5px;">Total</td>
	                                <td style="text-align:right;"><label id='lblTotal'>0,00</label></td>
	                            </tr>
		                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top:5px;"><br /><br />
                            <img class="ICO" src="../../../../Images/imgPosicionP.gif" title="Posición procesada" />Procesada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionF.gif" title="Posición facturada" />Facturada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionC.gif" title="Posición contabilizada" />Contabilizada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionA.gif" title="Posición anulada" />Anulada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionD.gif" title="Posición no procesada" />Sin procesar
                        </td>
                    </tr>
                </table>
                </eo:PageView>
                <eo:PageView ID="PageView3" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 3 Documentos -->
                <br>
                <table style="width:900px; margin-left:10px; text-align:left;">
                    <tr>
	                    <td>
		                    <table id="Table2" style="width: 850px; height:17px;">
		                        <colgroup>
		                            <col style="width:300px;" />
		                            <col style="width:300px;" />
		                            <col style="width:250px;" />
		                        </colgroup>
			                    <tr class="TBLINI">
				                    <td style="padding-left:3px;">Descripción</td>
				                    <td>Archivo</td>
				                    <td>Autor</td>
			                    </tr>
		                    </table>
		                    <div id="divCatalogoDoc" style="overflow:auto; overflow-x:hidden; width:866px; height:400px" runat="server">
		                        <div style='background-image:url(../../../../Images/imgFT20.gif); width:850px'>
		                        </div>
                            </div>
		                    <table id="Table3" style="width: 850px; height:17px;">
	                            <tr class="TBLFIN"><td></td></tr>
		                    </table>
                        </td>
                    </tr>
                </table>
                <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<center>
    <table id="tblBotonera" border="0" style="width:100px;margin-top:15px" align="center" class="texto">
        <colgroup>
            <col style="width:100px;" />
        </colgroup>
        <tr>
            <td>
                <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/Botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
            </td>
          </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnIdOrden" runat="server" style="visibility:hidden" Text="0" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</form>
<script type="text/javascript">
	<!--

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
</SCRIPT>
</body>
</html>
