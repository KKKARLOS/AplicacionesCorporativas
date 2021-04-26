<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">  
	#ctl00_CPHC_tsPestanasGen table { table-layout:auto;
</style>
<script type="text/javascript">
    var nAnoMesActual = <%=nAnoMes %>;
    var bEs_Administrador = <%=(Session["ADMINISTRADOR_PC_ACTUAL"].ToString()!="")? "true":"false" %>;
</script>
<table id="tabla" style="width:990px;text-align:left">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanasGen" ControlSkinID="None" Width="100%" 
						MultiPageID="mpContenido" 
						ClientSideOnLoad="CrearPestanas" 
						ClientSideOnItemClick="getPestana">
				        <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					        <Items>
							        <eo:TabItem Text-Html="Órdenes" ToolTip="Órdenes de facturación" Width="100"></eo:TabItem>
							        <eo:TabItem Text-Html="Plantillas" ToolTip="" Width="100"></eo:TabItem>
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="540px">				
			    <eo:PageView ID="PageView1" CssClass="PageView" runat="server" align="center">						    
				<!-- Pestaña 1 Órdenes de facturación-->
                <table id="tblPestOrdenes" style="width:960px;text-align:left;">
                    <tr>
                        <td>
                        <fieldset style="width:958px; padding:5px;">
                        <legend>Filtros</legend>
		                <table id="Table4" style="width:945px;">
   		                <colgroup>
		                    <col style="width:225px;" />
		                    <col style="width:200px;" />
		                    <col style="width:515px;" />
		                </colgroup>
                            <tr>
                                <td rowspan="3" style="padding-top:2px; vertical-align:top;">
                                    <fieldset style="width:210px; height:72px; padding:5px;">
                                    <legend>Estado&nbsp;
                                    <img id="img5" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer; vertical-align:middle;" runat="server" onclick="marcardesmarcarEstados(1)" />
                                    <img id="img6" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer; vertical-align:middle;" runat="server" onclick="marcardesmarcarEstados(0)" />
                                    </legend>
		                            <table id="tblEstados" style="width:200px; margin-top:5px;">
                                        <tr>
                                            <td>
                                                <img class="ICO" src="../../../Images/imgOrdenA.gif" title="Orden aparcada" style="cursor:pointer" onclick="seleccionar(getNextElementSibling(this))" /><input type="checkbox" id="chkAparcada" value="A" style="vertical-align:middle;cursor:pointer; margin-right:20px;" checked onclick="cambiarMes(0);" />
                                                <img class="ICO" src="../../../Images/imgOrdenT.gif" title="Orden tramitada" style="cursor:pointer" onclick="seleccionar(getNextElementSibling(this))" /><input type="checkbox" id="chkTramitada" value="T" style="vertical-align:middle;cursor:pointer; margin-right:20px;" checked onclick="cambiarMes(0);" />
                                                <img class="ICO" src="../../../Images/imgOrdenE.gif" title="Orden enviada desde SUPER al Pool de facturación de SAP" style="cursor:pointer" onclick="seleccionar(getNextElementSibling(this))" /><input type="checkbox" id="chkEnviada" value="E" style="vertical-align:middle;cursor:pointer; margin-right:20px;" checked  onclick="cambiarMes(0);" />
                                                <img class="ICO" src="../../../Images/imgOrdenR.gif" title="Orden recogida por el Pool de facturación de SAP" style="cursor:pointer" onclick="seleccionar(getNextElementSibling(this))" /><input type="checkbox" id="chkRecogida" value="R" style="vertical-align:middle;cursor:pointer; margin-right:20px;" checked  onclick="cambiarMes(0);" />
                                                <img class="ICO" src="../../../Images/imgOrdenF.gif" title="Orden finalizada" style="cursor:pointer" onclick="seleccionar(getNextElementSibling(this))" /><input type="checkbox" id="chkFinalizada" value="F" style="vertical-align:middle;cursor:pointer; margin-right:20px;" onclick="cambiarMes(0);" />
                                                <img class="ICO" src="../../../Images/imgOrdenX.gif" title="Orden errónea" style="cursor:pointer" onclick="seleccionar(getNextElementSibling(this))" /><input type="checkbox" id="chkErronea" value="X" style="vertical-align:middle;cursor:pointer; margin-right:20px;" onclick="cambiarMes(0);" checked  />
                                            </td>
                                        </tr>
                                    </table>
                                    </fieldset>
                                </td>
                                <td rowspan="3" style="padding-top:2px; vertical-align:top;">
                                    <fieldset style="width:180px; height:72px; padding:5px;">
                                    <legend>Restringir a <input type="checkbox" id="chkRestringir" style="vertical-align:middle;cursor:pointer;" onclick="cambiarMes(0);" /></legend>
                                        <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer; margin-left:10px; margin-top:10px;" />
                                        <asp:TextBox ID="txtMesVisible" style="width:90px; margin-bottom:5px; text-align:center;vertical-align:super;" readonly="true" runat="server" Text=""></asp:TextBox>
                                        <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
                                    </fieldset>
                                </td>
                                <td>
                                    <label id="lblCliente" class="enlace" style="width:74px;margin-top:8px;height:17px" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label>
                                    <asp:TextBox ID="txtNIFCliente" style="width:75px;margin-top:8px" Text="" runat="server" readonly="true" />&nbsp;&nbsp;<asp:TextBox ID="txtDesCliente" style="width:315px;margin-top:8px" Text="" ReadOnly runat="server" />
                                    <img src="../../../Images/Botones/imgBorrar.gif" border="0" title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label>
                                <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
                                <asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE(1);}else{vtn2(event);setNumPE();}" />
                                <asp:TextBox ID="txtDesPE" style="width:334px;" Text="" runat="server" readonly="true" />
                                <img src="../../../Images/Botones/imgBorrar.gif" border="0" title="Borra el proyecto" onclick="borrarProyecto()" style="cursor:pointer; vertical-align:middle;">
                                </td>
                            </tr>
                            <tr>
                                <td><label class="texto" style="width:77px">Nº Orden:</label><asp:TextBox ID="txtNumeroOrden" style="width:60px;" SkinID="Numero" runat="server" Text="" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;borrarCliente();borrarProyecto();getOrdenes();}else{vtn2(event);}"/></td>
                            </tr>
                        </table>
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <fieldset style="width:958px; padding:5px; padding-top:0px;">
                        <legend>Cabeceras</legend>
		                <table id="Table1" style="width:930px; table-layout:fixed; HEIGHT: 17px; margin-top:0px;" cellpadding="0" cellspacing="0" border="0">
		                    <colgroup>
		                        <col style="width:25px;" />
		                        <col style="width:65px;" />
		                        <col style="width:80px;" />
		                        <col style="width:360px;" />
		                        <col style="width:360px;" />
                                <col style="width:40px;" />
                            </colgroup>
                            <tr align="center">
                                <td colspan="6" align="right">
                                <img id="img3" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarOrdenes(1)" />
                                <img id="img4" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer; margin-right:4px;" runat="server" onclick="marcardesmarcarOrdenes(0)" />
                                </td>
                            </tr>
			                <tr id="tblTitulo" class="TBLINI">
				                <td title="Estado" style="padding-left: 2px;" >Est.</td>
				                <td style="text-align:right; padding-right:3px;" ><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgo1" border="0" />
			                        <map name="imgo1">
					                    <area onclick="ot('tblOrdenes', 1, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblOrdenes', 1, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
				                    </map>Nº orden</td>
				                <td style="padding-left:2px;" title="Fecha factura"><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgo2" border="0" />
			                        <map name="imgo2">
					                    <area onclick="ot('tblOrdenes', 2, 0, 'fec', '')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblOrdenes', 2, 1, 'fec', '')" shape="RECT" coords="0,6,6,11">
				                    </map>F. Factura</td>
				                <td>Cliente (NIF) / Denominación&nbsp;
				                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOrdenes',3,'divCatalogoOrdenes','imgLupa2')"
							            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" /> 
							        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblOrdenes',3,'divCatalogoOrdenes','imgLupa2', event)"
							            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1" />
				                </td>
				                <td>Proyecto&nbsp;
				                    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOrdenes',4,'divCatalogoOrdenes','imgLupa3')"
							            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" /> 
							        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblOrdenes',4,'divCatalogoOrdenes','imgLupa3', event)"
							            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1" />
							    </td>
			                    <td align="center" title="Marca para la generación de remesa">MGR</td>
			                </tr>
		                </table>
		                <div id="divCatalogoOrdenes" style="overflow-y: auto; overflow-x: hidden; width: 946px; height:130px" runat="server">
		                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:930px">
		                    <%=strTablaHTML %>
		                    </div>
		                </div>
		                <table id="tblTotal" style="width:930px;">
			                <tr class="TBLFIN">
				                <td >&nbsp;</td>
			                </tr>
		                </table>
						<table id="tblBotones" align="center" style="margin-top:10px;"  width="100%">
							<tr>
								<td align="center">
									<button id="btnNuevaOrden" type="button" onclick="addOrden();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">&nbsp;&nbsp;Añadir</span>
									</button>	
								</td>						
								<td align="center">
									<button id="btnEliminarOrden" type="button" onclick="delOrden();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">&nbsp;&nbsp;Eliminar</span>
									</button>	
								</td>
								<td align="center">
									<button id="btnRecuperar" type="button" onclick="recuperarOrden();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/imgRecuperar.gif" /><span title="Recuperar">Recuperar</span>
									</button>	
								</td>	
								<td align="center">
									<button id="btnRemesa" type="button" onclick="crearRemesaOF();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/imgRemesaOF.gif" /><span title="Remesa">&nbsp;Remesa</span>
									</button>	
								</td>								 
								<td align="center">
									<button id="btnRemesaPL" type="button" onclick="crearRemesaPLDesdeOF();" class="btnH25W105" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/botones/imgPlantillaOF.gif" /><span title="Remesa PL">&nbsp;Remesa PL</span>
									</button>	
								</td>								
								<td align="center">
									<button id="btnPrevisualizar" type="button" onclick="PrevisualizaFactura();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/botones/imgFacturas.gif" /><span title="Visualizar">&nbsp;Visualizar</span>
									</button>	 
								</td>
								<td align="center">
									<button id="btnCPE" type="button" onclick="cambiarPE();" class="btnH25W105" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/imgIconoProyAzul.gif" /><span title="Cambiar el proyecto a la orden de factuación">&nbsp;Cambiar PE</span>
									</button>	
								</td>	
							</tr>
						</table>	
						<table class="texto" style="WIDTH: 930px; table-layout:fixed;" cellSpacing="0" border="0">
                            <tr>
                                <td style="padding-top:5px;">
                                    <img class="ICO" src="../../../Images/imgOrdenA.gif" title="Orden aparcada" />Aparcada&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgOrdenT.gif" title="Orden tramitada" />Tramitada&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgOrdenE.gif" title="Orden enviada desde SUPER al Pool de facturación de SAP" />Enviada a Pool&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgOrdenR.gif" title="Orden recogida por el Pool de facturación de SAP" />Recogida por el Pool&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgOrdenF.gif" title="Orden finalizada" />Finalizada&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgOrdenX.gif" title="Orden errónea" />Errónea
                                </td>
                            </tr>
                        </table>            
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <fieldset style="width:958px; padding:5px;">
                        <legend>Posiciones</legend>
		                <table id="Table2" style="WIDTH: 930px; table-layout:fixed; HEIGHT: 17px; margin-top:5px;" cellspacing="0" border="0">
		                    <colgroup>
		                        <col style="width:25px;" />
		                        <col style="width:60px;" />
		                        <col style="width:60px;" />
		                        <col style="width:405px;" />
		                        <col style="width:70px; " />
		                        <col style="width:70px;" />
		                        <col style="width:70px;" />
		                        <col style="width:70px;" />
		                        <col style="width:100px;" />
		                    </colgroup>
			                <tr class="TBLINI">
				                <td style="padding-left: 2px;" title="Estado">Est.</td>
				                <td title="Serie de factura">Serie</td>
				                <td title="Número de factura">Número</td>
                                <td>Concepto</td>
                                <td style='text-align:right; padding-right: 2px;'>Unidades</td>
                                <td style='text-align:right; padding-right: 2px;'>Precio</td>
                                <td style='text-align:right; padding-right: 2px;' title="Porcentaje a descontar de la posición de la orden de facturación">Dto. %</td>
                                <td style='text-align:right; padding-right: 2px;' title="Importe absoluto a descontar de la posición, en la moneda de la orden de facturación">Dto. Imp.</td>
                                <td style='text-align:right; padding-right: 25px;'>Importe</td>
			                </tr>
		                </table>
		                <div id="divCatalogoPosiciones" style=" overflow: auto; overflow-x: hidden; WIDTH: 946px; height:60px" runat="server">
		                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:930px">
		                    </div>
		                </div>
		                <table id="Table3" class="texto" style="WIDTH: 930px; table-layout:fixed; HEIGHT: 17px" cellpadding="0" cellspacing="0" border="0">
		                    <colgroup>
		                        <col style="width:25px;" />
		                        <col style="width:60px;" />
		                        <col style="width:60px;" />
		                        <col style="width:390px;" />
		                        <col style="width:70px;" />
		                        <col style="width:70px;" />
		                        <col style="width:85px;" />
		                        <col style="width:70px;" />
		                        <col style="width:100px;" />
		                    </colgroup>
			                <tr class="TBLFIN">
                                <td colspan="7"></td>
                                <td>Subtotal</td>
                                <td style="text-align:right; padding-right:25px;"><label id='lblSubtotal'>0,00</label></td>
			                </tr>
                            <tr style="padding-top:5px;">
                                <td colspan="4"></td>
                                <td style="text-align:right; padding-top:2px; padding-right: 2px;">Descuento</td>
                                <td title="Porcentaje a descontar de la orden de facturación" style=" padding-top:2px; text-align:right; padding-right: 2px;">
                                    <asp:TextBox ID="txtDtoPorc" runat="server" style="width:50px;" Text="" SkinID="Numero" readonly="true" /> %
                                </td>
                                <td title="Importe absoluto a descontar en la moneda de la orden de facturación" style=" padding-top:2px;text-align:right; padding-right: 2px;">
                                    <asp:TextBox ID="txtDtoImporte" runat="server" style="width:55px;" Text="" SkinID="Numero" readonly="true" />
                                     Imp.
                                </td>
                                <td></td>
                                <td style=" padding-top:2px; text-align:right; padding-right:25px;"><label id='lblDto'>0,00</label></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <img class="ICO" src="../../../Images/imgPosicionD.gif" title="Posición no procesada" />Sin procesar&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgPosicionP.gif" title="Posición procesada en SAP" />Procesada&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgPosicionF.gif" title="Posición facturada en SAP" />Facturada&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgPosicionC.gif" title="Posición contabilizada en SAP" />Contabilizada&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgPosicionA.gif" title="Posición anulada en SAP" />Anulada&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../Images/imgPosicionB.gif" title="Posición borrada en SAP" />Borrada
                                </td>
                                <td style='text-align:right; padding-right: 2px;'><label id="lblIVA"></label></td>
                                <td style='text-align:right; padding-right: 2px;'></td>
                                <td></td>
                                <td style='text-align:right; padding-right: 2px;'>Total</td>
                                <td style='text-align:right; padding-right: 25px;'><label id='lblTotal'>0,00</label></td>
                            </tr>
		                </table>
                        </fieldset>
                        </td>
                    </tr>
                </table>
                </eo:PageView>
                
				<eo:PageView ID="PageView2" CssClass="PageView" runat="server" align="center">
				<!-- Pestaña 2 Plantillas-->
                <table id="tblPestPlantillas" style="width:970px;text-align:left">
                    <tr>
                        <td align="center">
                        <fieldset style="width:540px; padding:5px;">
                        <legend>Filtros</legend>
		                <table id="Table11" style="width:520px;text-align:left" cellpadding="3">
                            <tr>
                                <td>
                                    <label id="lblClientePlan" class="enlace" style="width:74px;height:17px" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label>
                                    <asp:TextBox ID="txtNIFClientePlan" style="width:75px;" Text="" runat="server" readonly="true" />&nbsp;&nbsp;<asp:TextBox ID="txtDesClientePlan" style="width:315px;" Text="" ReadOnly runat="server" />
                                    <img src="../../../Images/Botones/imgBorrar.gif" border="0" title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <label id="Label2" class="enlace" onclick="getPE()">Proyecto</label>
                                <asp:Image ID="imgEstProyPlan" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
                                <asp:TextBox ID="txtNumPEPlan" style="width:60px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE(2);}else{vtn2(event);setNumPE();}" />
                                <asp:TextBox ID="txtDesPEPlan" style="width:332px;" Text="" runat="server" readonly="true" />
                                <img src="../../../Images/Botones/imgBorrar.gif" border="0" title="Borra el proyecto" onclick="borrarProyecto()" style="cursor:pointer; vertical-align:middle;">
                                <input type="hidden" runat="server" id="hdnT305IdProyPlan" value="" />
                                </td>
                            </tr>
                            <tr>
                                <td><label class="texto" style="width:77px">Nº Plantilla:</label><asp:TextBox ID="txtNumeroPlantilla" style="width:60px;" SkinID="Numero" runat="server" Text="" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;borrarCliente();borrarProyecto();bGetPlantillas = true;getDatos(1);}else{vtn2(event);}"/></td>
                            </tr>
                        </table>
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;">
                            <fieldset style="width:960px; padding:5px;">
                            <legend>Plantillas</legend>
		                    <table id="Table5" style="width:940px; height: 17px; margin-top:5px;">
		                        <colgroup>
		                            <col style="width:25px;" />
		                            <col style="width:65px;" />
		                            <col style="width:210px;" />
                                    <col style="width:200px;" />
                                    <col style="width:200px;" />
                                    <col style="width:200px;" />
                                    <col style="width:40px;" />
                                </colgroup>
			                    <tr id="Tr1" class="TBLINI">
				                <td style="padding-left: 2px;" title="Estado">Est.</td>
				                <td style="text-align:right; padding-right:3px;"><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imga0" border="0" />
			                        <map name="imga0">
					                    <area onclick="ot('tblPlantillas', 1, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblPlantillas', 1, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
				                    </map>Nº plant.</td>
				                    <td><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imga1" border="0" />
				                        <map name="imga1">
						                    <area onclick="ot('tblPlantillas', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
						                    <area onclick="ot('tblPlantillas', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
					                    </map>Denominación</td>
				                    <td><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imga2" border="0" />
				                        <map name="imga2">
						                    <area onclick="ot('tblPlantillas', 3, 0, '', '')" shape="RECT" coords="0,0,6,5">
						                    <area onclick="ot('tblPlantillas', 3, 1, '', '')" shape="RECT" coords="0,6,6,11">
					                    </map>Cliente</td>
				                    <td><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imga3" border="0" />
				                        <map name="imga3">
						                    <area onclick="ot('tblPlantillas', 4, 0, '', '')" shape="RECT" coords="0,0,6,5">
						                    <area onclick="ot('tblPlantillas', 4, 1, '', '')" shape="RECT" coords="0,6,6,11">
					                    </map>Proyecto</td>
				                    <td><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imga4" border="0" />
				                        <map name="imga4">
						                    <area onclick="ot('tblPlantillas', 5, 0, '', '')" shape="RECT" coords="0,0,6,5">
						                    <area onclick="ot('tblPlantillas', 5, 1, '', '')" shape="RECT" coords="0,6,6,11">
					                    </map>Autor</td>
				                    <td align="center" title="Marca para la generación de remesa">MGR</td>
			                    </tr>
		                    </table>
		                    <div id="divCatalogoPlantillas" style="overflow: auto; overflow-x: hidden;  width: 956px; height:300px" runat="server">
		                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:940px">
		                        </div>
		                    </div>
		                    <table id="Table6" style="width:940px; height: 17px">
			                    <tr class="TBLFIN">
				                    <td >&nbsp;</td>
			                    </tr>
		                    </table>
						    <table id="Table7" align="center" style="margin-top:10px;"  width="100%">
							    <tr>
								    <td align="center">
									    <button id="btnAddPlantilla" type="button" onclick="addPlantilla();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										     onmouseover="se(this, 25);mostrarCursor(this);">
										    <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">&nbsp;&nbsp;Añadir</span>
									    </button>	
								    </td>						
								    <td align="center">
									    <button id="btnDelPlantilla" type="button" onclick="delPlantilla();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										     onmouseover="se(this, 25);mostrarCursor(this);">
										    <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">&nbsp;&nbsp;Eliminar</span>
									    </button>	
								    </td>
								    <td align="center">
									    <button id="Button1" type="button" onclick="PrevisualizaPlantilla();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
										     onmouseover="se(this, 25);mostrarCursor(this);">
										    <img src="../../../images/botones/imgFacturas.gif" /><span title="Visualizar">Visualizar</span>
									    </button>	
								    </td>	
								    <td align="center">
									    <button id="Button5" type="button" onclick="crearRemesaPL();" class="btnH25W105" runat="server" hidefocus="hidefocus" 
										     onmouseover="se(this, 25);mostrarCursor(this);">
										    <img src="../../../images/imgRemesaOF.gif" /><span title="Remesa">&nbsp;Remesa OF</span>
									    </button>	
								    </td>								
								    <td align="center">
									    <button id="btnDuplicar" type="button" onclick="duplicarPlantilla();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
										     onmouseover="se(this, 25);mostrarCursor(this);">
										    <img src="../../../images/botones/imgDuplicar.gif" /><span title="Duplicar">&nbsp;Duplicar</span>
									    </button>	
								    </td>								
							    </tr>
						    </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top:5px;">
                            <img class="ICO" src="../../../Images/imgEstPlanB.gif" title="Plantilla en estado borrador" />Borrador&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../Images/imgEstPlanP.gif" title="Plantilla en estado publicada" />Publicada
                        </td>
                    </tr>
                </table>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<div class="clsDragWindow" id="DW" noWrap></div>
<asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
<input type="hidden" runat="server" id="hdnT305IdProy" value="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "ordenoriginal": //Boton instantanea
				{   
				    bEnviar = false;
				    instantaneaTramitacion();							
					break;
				}
				case "regresar": //Boton regresar
				{
				    //bEnviar = Regresar();							
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

