<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Detalle de orden de facturación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=10"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
    <script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?v=20180416" type="text/Javascript"></script>
    <script src="../../../../Javascript/modal.js?v=20180416" type="text/Javascript"></script>

</head>
<body onLoad="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
        .badge[data-badge]:after {
               content:attr(data-badge);
               font-size:.7em;
               background-color:#777;
               color:white;
               width:auto;
               padding: 2px;
               height:16px;
               text-align:center;
               line-height:16px;
               border-radius:50%;
               box-shadow:0 0 1px #333;
               /*margin-top: 7px;*/
               float:left;
            font-size:14px;
            }
    </style>
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
        var bNueva = <%= (nIdOrden == 0)? "true":"false" %>;
        var sNumEmpleado = "<% =Session["UsuarioActual"].ToString() %>";
        var sFechaHoy = "<% =DateTime.Today.ToShortDateString() %>";
        var sIDDocuAux = "<% =sIDDocuAux %>";
        var bEs_Administrador = <%=(Session["ADMINISTRADOR_PC_ACTUAL"].ToString()!="")? "true":"false" %>;
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    </script>

    <table style="width:920px;text-align:left;margin-left:20px;margin-top:7px">
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="530px">				
			    <eo:PageView id="PageView1" CssClass="PageView" runat="server" align="center">						    
				<!-- Pestaña 1 General-->
                <table id="tabla" style="width:880px; padding:4px; margin:10px; text-align:left">
                    <colgroup>
                        <col style="width:135px;" />
                        <col style="width:475px;" />
                        <col style="width:100px;" />
                        <col style="width:170px;" />
                    </colgroup>
                    <tr>
                        <td style="text-align:left;">
                            <label id="lblProy" class="texto" onclick="if (this.className=='enlace') getPE()" style=" width:50px; margin-left:10px;">Proyecto</label>
                            <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumPE" style="width:60px; margin-left:3px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;getPEByNum();}else{vtn2(event);setNumPE();}" />
                            <asp:TextBox ID="txtDesPE" style="width:368px;" Text="" runat="server" readonly="true" />
                        </td>
                        <td style="text-align:right;">
                            <label title="Nº de documentos asociados a la orden de facturación">Documentación:</label>
                        </td>
                        <td>
                            <span id="lblDocumentosCount" class="badge" runat="server" data-badge='0'></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            <fieldset style="width:590px;">
                                <legend>Cliente</legend>
                                <table class="texto" style="width:500px;" cellpadding="4" border="0">
                                    <colgroup>
                                        <col style="width: 130px;" />
                                        <col style="width: 450px;" />
                                    </colgroup>
                                     <tr>
                                        <td><label id="lblCliPago" onclick="if (this.className=='enlace') getCliente('R');" class="enlace" title="Cliente responsable del pago de la factura">NIF / Denominación</label> <span style="color:Red">*</span></td>
                                        <td><asp:TextBox ID="txtNIFCliPago" style="width:75px;" Text="" runat="server" readonly="true" />&nbsp;&nbsp;<asp:TextBox ID="txtDesCliRespPago" style="width:350px;" Text="" readonly="true" runat="server" /></td>
                                    </tr> 
                                    <tr>
                                        <td>
                                            <label id="lblComercial" onclick="getComercial()" class="enlace">Comercial</label><span style="color:Red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDenComercial" style="width:435px;" Text="" runat="server" readonly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset style="width:590px; margin-top:7px">
                                <legend>Destinatario de factura</legend>
                            <table border="0" cellpadding="4"  style="width: 500px;">
                                <colgroup>
                                    <col style="width: 130px;" />
                                    <col style="width: 450px;" />
                                </colgroup>
                                <tr>
                                    <td>NIF / <label id="lblCliFra" onclick="if (this.className=='enlace') getDestFact();"  class="enlace" title="Cliente destinatario de la factura">Razón social</label> <span style="color:Red">*</span></td>
                                    <td><asp:TextBox ID="txtNIFCliDestFac" style="width:75px;" Text="" runat="server" readonly="true" />&nbsp;&nbsp;<asp:TextBox ID="txtDesClienteDestFac" style="width:350px;" Text="" readonly="true" runat="server" /></td>
                                </tr> 
                                <tr style="height:50px; vertical-align:top;">
                                    <td>Dirección</td>
                                    <td id="cldDireccion" runat="server"></td>
                                </tr>
                                <tr>
                                    <td><label id="Label1" style="width:80px;" runat="server" class="texto">A la atención de</label></td>
                                    <td><asp:textbox id="txtComentarios" onkeyup="aG(0);" MaxLength="50" runat="server" style="width:435px" /></td>
                                </tr>
                            </table>
                            </fieldset>
                            <fieldset style="width: 590px;margin-top:7px">
                                <legend>Condiciones de pago</legend>
                            <table cellpadding="4" style="width: 500px;">
                                <colgroup>
                                    <col style="width: 130px;" />
                                    <col style="width: 450px;" />
                                </colgroup>
                                <tr>
                                    <td><label id="lblCondPago" runat="server" class="texto" >Plazo</label> <span style="color:Red">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="cboCondPago" runat="server" AppendDataBoundItems="true" onchange="aG(0);" Width="260px">
                                        </asp:DropDownList>	
                                    <label id="lblModPlazo" onclick="if (this.className=='enlace') getSolicitudModificacion();" class="enlace" style="margin-left:30px;" title="Solicita la modificación del plazo de pago al administrador, teniéndole que indicar el requerido.">Solicitar modificación</label>    
                                    </td>	            
                                </tr>
                                <tr>             
                                    <td><label id="lblViaPago" runat="server" title="Vía de pago para la factura" class="texto" >Vía</label> <span style="color:Red">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="cboViaPago" runat="server" AppendDataBoundItems="true" onchange="aG(0);" Width="200px"></asp:DropDownList>
                                        <label id="lblMoneda" runat="server" style="margin-left:10px;" class="texto">Moneda</label> <span style="color:Red">*</span>
                                        <asp:DropDownList ID="cboMoneda" runat="server" AppendDataBoundItems="true" onchange="aG(0);" Width="180px"></asp:DropDownList>	
                                    </td>	            
                                </tr> 
                            </table>
                            </fieldset>
                            <fieldset style="width: 590px; margin-top:7px; height: 68px">
                                <legend>Pedido</legend>
                            <table cellpadding="4" style="width: 500px;">
                                <colgroup>
                                    <col style="width: 130px;" />
                                    <col style="width: 450px;" />
                                </colgroup>
                             <tr>             
                                <td><label id="lblRefCli" runat="server" class="texto" title="Referencia del pedido en el cliente">Referencia en el cliente</label></td>
                                <td>
                                    <asp:TextBox ID="txtRefCli" style="width:260px;" onkeyup="aG(0);" Text="" runat="server" MaxLength="35"/>
                                    <label id="lblMaxRefCli"></label>
                                </td>
                             </tr>  
                             <tr>             
                                <td><label id="lblDocVentSAP" style="visibility:hidden;" runat="server" class="texto" title="Número de documento de venta asignado por SAP al pedido">Documento de venta</label></td>
                                <td><asp:TextBox ID="txtDocVentSAP" style="width:120px; visibility:hidden;" Text="" runat="server" readonly="true" /></td>
                             </tr>
                            </table>
                            </fieldset>
                            <table cellpadding="3" style="width: 610px;">
                                <colgroup><col style="width: 135px;" /><col style="width: 475px;" /></colgroup>
                                <tr>             
                                    <td><label id="lblOV" runat="server" title="Sociedad que factura" class="texto">Sociedad que factura</label> <span style="color:Red">*</span></td>
								    <td><br />
									    <asp:DropDownList ID="cboOV" runat="server" AppendDataBoundItems="true" onchange="getDestFactByOVmasVias();aG(0);" Width="260px">
									    </asp:DropDownList>	</td>
                                </tr>  
                                <tr>  
                                    <td valign="top">
                                        <label runat="server" class="texto">Indicaciones a Pool</label>
                                    </td>           
                                    <td valign="top">
                                        <asp:textbox id="txtObsPool" onkeyup="aG(0);" runat="server" SkinID="Multi" style="width:455px" TextMode="MultiLine" Rows="8"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="lblTramitadaPor1" runat="server"></label>
                                    </td>
                                    <td>
                                        <label id="lblTramitadaPor2" runat="server"></label>
                                    </td>
                                </tr>                        
                            </table>	                            
                        </td>
                        <td colspan="2" valign="top">
                            <fieldset style="width:260px;height:60px">
                            <legend>Orden</legend>
                            <table style="width:230px;" cellpadding="5">
                            <colgroup>
                                <col style="width: 100px;" />
                                <col style="width: 130px;" />
                            </colgroup>
                                <tr>
                                    <td>Número</td>
                                    <td><asp:TextBox ID="txtIdOrden" SkinID="numero" runat="server" style="width:60px;" Text="" readonly="true" /></td>
                                </tr>
                                <tr>
                                    <td>Estado</td>
                                    <td><asp:TextBox ID="txtEstado" runat="server" style="width:100px;" Text="" readonly="true" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            </fieldset>
                            <fieldset style="width:260px;height:85px;margin-top:7px">
                            <legend>Fechas</legend>
                            <table style="width:230px;" cellpadding="5">
                            <colgroup>
                                <col style="width: 100px;" />
                                <col style="width: 130px;" />
                            </colgroup>
                                <tr>
                                    <td><label id="lblFecPrevEmFac" runat="server" class="texto" title="Fecha factura">Factura</label> <span style="color:Red">*</span></td>
                                    <td><asp:TextBox ID="txtFecPrevEmFac" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="aG(0);" goma="0" lectura=0 /></td>
                                </tr>
                                <tr>
                                    <td><label id="lblDiferida" style="width:80px;" runat="server" class="texto" title="Fecha en la que la orden pasará a SAP para su proceso. Si no se indica, pasará inmediatamente tras su tramitación.">Proceso diferido</label></td>
                                    <td><asp:TextBox ID="txtFecDiferida" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="aG(0);" goma="1" lectura=0 /></td>
                                </tr>
                                <tr>
                                    <td id="cldCronologia" runat="server"><img src="../../../../images/info.gif" style="vertical-align:middle;"> Cronología</td>
                                    <td></td>
                                </tr>
                            </table>
                            </fieldset>
                            <fieldset style="width:260px; padding:5px ;margin-top:7px; height:170px;">
                                <legend>Texto cabecera de factura</legend>
                                <asp:textbox id="txtCabecera" runat="server" onkeyup="aG(0);" SkinID="Multi" style="width:250px" TextMode="MultiLine" Rows="11"></asp:textbox>
                            </fieldset>
                            <fieldset style="width:260px; padding:5px;" id="fstIndiPlan" runat="server">
                                <legend>Indicaciones derivadas de la plantilla</legend>
                                <asp:textbox id="txtObsPlantilla" runat="server" SkinID="Multi" style="width:250px; background-color:Transparent;" TextMode="MultiLine" Rows="4" readonly="true"></asp:textbox>
                            </fieldset>
							<table style="width: 270px; margin-top:7px">
                                <colgroup><col style="width:270px;" /></colgroup>
                                <tr>             
								    <td valign="top" align="right"><span id="spanAgrupacion" style="visibility:hidden;"><label id="lblAgrupacion" runat="server" class="enlace" style="margin-left:10px;" onclick="if (this.className=='enlace') getAgrupacion()" title="Clave de agrupación">Agrupación</label> 
								        <asp:TextBox ID="txtClaveAgru" style="width:60px;" SkinID="numero" Text="" runat="server" readonly="true" />
								        <img id="imgGomaClave" src="../../../../Images/Botones/imgBorrar.gif" title="Borra la agrupación" onclick="$I('txtClaveAgru').value='';$I('txtClaveAgru').title='';aG(0);this.style.visibility='hidden';" style="cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;"></span>
								    </td>
                                </tr>
                            </table>                         
                        </td>
                    </tr>               
                </table>
                </eo:PageView>
                <eo:PageView id="PageView2" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 2 Posiciones-->
                <br />
                <table style="width:900px" align="center">
                <colgroup>
		            <col style="width:900px;" />
		        </colgroup>
                    <tr>
	                    <td>
		                    <table style="width: 880px; height: 17px">
		                        <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
		                            <col style="width:20px;" />
	                                <col style="width:425px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:50px;" />
	                                <col style="width:70px;" />
		                            <col style="width:100px;" />
		                        </colgroup>
			                    <tr class="TBLINI">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;Concepto</td>
				                    <td style="padding-left:15px;" >Unidades</td>
				                    <td style="padding-left:25px;">Precio</td>
                                    <td style="padding-left:10px;" title="Porcentaje a descontar de la posición de la orden de facturación">Dto. %</td>
                                    <td style="text-align:right;padding-right:15px;" title="Importe absoluto a descontar de la posición, en la moneda de la orden de facturación">Dto. Imp.</td>
				                    <td style="text-align:right;padding-right:10px;">Importe</td>
			                    </tr>
		                    </table>
		                    <div id="divPosiciones" style="overflow: auto; overflow-x: hidden; width: 896px; height:370px" runat="server">
		                        <div style='background-image:url(../../../../Images/imgFT38.gif); width:880px'>
		                        <table id="tblPosiciones" class="MANO" style="width: 880px;" cellpadding="2" border="1">
		                            <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
		                            <col style="width:20px;" />
	                                <col style="width:425px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:50px;" />
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
		                            <col style="width:20px;" />
	                                <col style="width:425px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:50px;" />
	                                <col style="width:70px;" />
		                            <col style="width:100px;" />
		                            </colgroup>
	                            <tr class="TBLFIN">
	                                <td colspan="6"></td>
	                                <td>Subtotal</td>
	                                <td align="right" style="text-align:right; padding-right:3px;" ><label id='lblSubtotal'>0,00</label></td>
	                            </tr>
		                    </table>
		                    
		                    <table id="tblTotal" style="width: 880px;" cellpadding="3">
		                        <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
		                            <col style="width:20px;" />
	                                <col style="width:410px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:50px;" />
	                                <col style="width:75px;" />
		                            <col style="width:95px;" />
		                            </colgroup>
	                            <tr>
	                                <td colspan="4" rowspan="2">
                                        <table width="300px" align="center" style="margin-top:5px;" border="0" cellpadding="0" cellspacing="0">
		                                    <tr>
			                                    <td align="center">
													<button id="btnAddPosicion" type="button" onclick="addPosicion();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
														 onmouseover="se(this, 25);mostrarCursor(this);">
														<img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
													</button>													
			                                    </td>
			                                    <td align="center">
													<button id="btnDelPosicion" type="button" onclick="delPosicion();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
														 onmouseover="se(this, 25);mostrarCursor(this);">
														<img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar posición">Eliminar</span>
													</button>													
			                                    </td>
		                                    </tr>
                                        </table>
	                                </td>
	                                <td>Descuento</td>
	                                <td title="Porcentaje a descontar de la orden de facturación"><asp:TextBox ID="txtDtoPorc" runat="server" style="width:50px;" Text="" SkinID="numero" onfocus="fn(this)" onkeyup="if (bLectura) return;aG(0);$I('txtDtoImporte').value='';sit();" /> %</td>
	                                <td colspan="2" title="Importe absoluto a descontar en la moneda de la orden de facturación"><asp:TextBox ID="txtDtoImporte" runat="server" style="width:50px;" Text="" SkinID="numero" onfocus="fn(this)" onkeyup="if (bLectura) return;aG(0);$I('txtDtoPorc').value='';sit();" /> Imp.</td>
	                                <td align="right" style="text-align:right;"><label id='lblDto'>0,00</label></td>
	                            </tr>
	                            <tr>
	                                <td>IVA incluido</td>
	                                <td><asp:CheckBox id="chkIVA" runat="server" style="vertical-align:middle;cursor:pointer;margin-left:-4px;" onclick="if (bLectura) return;aG(0);" />
	                                </td>
	                                <td></td>
	                                <td align="right">Total</td>
	                                <td align="right" style="text-align:right;"><label id='lblTotal'>0,00</label></td>
	                            </tr>
		                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top:5px;"><br /><br />
                            <img class="ICO" src="../../../../Images/imgPosicionD.gif" title="Posición no procesada" />Sin procesar&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionP.gif" title="Posición procesada" />Procesada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionF.gif" title="Posición facturada" />Facturada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionC.gif" title="Posición contabilizada" />Contabilizada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionA.gif" title="Posición anulada" />Anulada&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgPosicionB.gif" title="Posición borrada en SAP" />Borrada
                        </td>
                    </tr>
                </table>
                </eo:PageView>

                <eo:PageView id="PageView3" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 3 Documentos -->
                <br>
                <table style="width:900px; margin-left:10px; text-align:left;">
                    <tr>
	                    <td>
		                    <table style="width: 850px; height:17px;">
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
		                    <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 866px; height:400px" runat="server">
		                        <div style='background-image:url(../../../../Images/imgFT20.gif); width:850px'>
		                        </div>
                            </div>
		                    <table id="Table3" style="width: 850px; height:17px;">
	                            <tr class="TBLFIN"><td></td></tr>
		                    </table>
                        </td>
                    </tr>
                </table>
                <center>
                    <table align="center" style="width:300px;margin-top:20px;">
                        <tr>
                            <td align="center">
							    <button id="btnAddDoc" type="button" onclick="addDoc();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
								     onmouseover="se(this, 25);mostrarCursor(this);">
								    <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
							    </button>													
                            </td>
                            <td align="center">
							    <button id="btnDelDoc" type="button" onclick="delDoc();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
								     onmouseover="se(this, 25);mostrarCursor(this);">
								    <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar documento">Eliminar</span>
							    </button>													
                            </td>
                        </tr>
                    </table>
                </center>                
                <iframe id="iFrmSubida" frameborder="0" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>

<table id="tblBotonera" border="0" style="width:520px; margin-left:150px; margin-top:7px" class="texto">
    <colgroup>
        <col style="width:130px;" />
        <col style="width:130px;" />
        <col style="width:130px;" />
        <col style="width:130px;" />
    </colgroup>
    <tr>
        <td>
			<button id="btnTramitar" type="button" onclick="Tramitar();" class="btnH25W95" runat="server" hidefocus="hidefocus" style="display:none;"
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgTramitar.gif" /><span title="Tramitar">Tramitar</span>
			</button>			
 
        </td>
        <td> 
			<button id="btnAparcar" type="button" onclick="Aparcar();" class="btnH25W95" runat="server" hidefocus="hidefocus" style="display:none;"
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgAparcar.gif" /><span title="Aparcar">Aparcar</span>
			</button>	
        </td>		        
        <td>
			<button id="btnPrevisualizar" type="button" onclick="PrevisualizaFactura();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgFacturas.gif" /><span title="Visualiza la orden en formato factura">&nbsp;Visualizar</span>
			</button>		   
        </td>        
        <td>
			<button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" style="display:none;" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/Botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
			<button id="btnCerrar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" style="display:none;" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/Botones/imgSalir.gif" /><span title="Cerrar">Cerrar</span></button>				
        </td>
      </tr>
</table>

<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnT305IdProy" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdOrden" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnIdCliSolicitante" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdCliPago" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdCliDestFac" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdRespComercial" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstado" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnLectura" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" name="hdnEfactur" id="hdnEfactur" runat="server" value="N" />
<input type="hidden" name="hdnNumDocs" id="hdnNumDocs" runat="server" value="0" />
<!--<asp:TextBox ID="hdnEsClienteSolicitante" runat="server" style="visibility:hidden" Text="0" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />-->
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
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
</body>
</html>
