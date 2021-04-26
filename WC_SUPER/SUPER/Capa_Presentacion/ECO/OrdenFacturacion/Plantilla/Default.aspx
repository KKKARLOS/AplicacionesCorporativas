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
<head runat="server">
    <title> ::: SUPER ::: - Detalle de plantilla de orden de facturación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
    <script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bSalir = false;
        var sNumEmpleado = "<% =Session["UsuarioActual"].ToString() %>";
        var sFechaHoy = "<% =DateTime.Today.ToShortDateString() %>";
        var sIDDocuAux = "<% =sIDDocuAux %>";
        var bEs_Administrador = <%=(Session["ADMINISTRADOR_PC_ACTUAL"].ToString()!="")? "true":"false" %>;
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    -->
    </script>
<table style="width:920px;text-align:left;margin-left:20px;margin-top:0px">
<colgroup>
    <col style="width:520px;" />
    <col style="width:400px;" />
</colgroup>
<tr>
    <td>
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="480px">
            <tr>
                <td background="../../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <label id="Label2" style="width:80px; margin-left:10px;">Denominación</label>
                    <asp:TextBox ID="txtDenominacion" style="width:332px;" Text="" onkeyup="aG(0);" runat="server" />
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </td>
    <td>
        <fieldset id="fstEstado" style="width: 130px; text-align:left; height:55px;">
            <legend>Estado</legend>   
            <asp:RadioButtonList ID="rdbEstado" SkinId="rbl" runat="server" RepeatColumns="1" RepeatDirection=vertical onclick="aG(0);">
                <asp:ListItem Value="B" style="vertical-align:super;cursor:pointer" Selected="True"><label style="cursor:pointer;vertical-align:middle; margin-left:5px; margin-right: 5px;" onclick='this.parentNode.click()'>Borrador</label> <img src='../../../../Images/imgEstPlanB.gif' hidefocus=hidefocus style="vertical-align:middle;"></asp:ListItem>
                <asp:ListItem Value="P" style="vertical-align:super;cursor:pointer"><label style="cursor:pointer;vertical-align:middle; margin-left:5px; margin-right: 5px;" onclick='this.parentNode.click()'>Publicada</label> <img src='../../../../Images/imgEstPlanP.gif' hidefocus=hidefocus style="vertical-align:middle;"></asp:ListItem>
            </asp:RadioButtonList>
         </fieldset>
    </td>
</tr>
</table>    
<table style="width:920px; text-align:left; padding:10px; position:absolute; left:20px; top: 60px;">
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="475px">				
			    <eo:PageView ID="PageView1" CssClass="PageView1" runat="server" align="center">						    
				<!-- Pestaña 1 General-->
                <table id="tabla" style="width:880px; padding:5px; margin:10px; text-align:left">
                    <colgroup>
                        <col style="width:135px;" />
                        <col style="width:475px;" />
                        <col style="width:100px;" />
                        <col style="width:170px;" />
                    </colgroup>
                    <tr>
                        <td style="text-align:left;"><label id="lblProy" class="enlace" onclick="getPE()" style=" width:50px; margin-left:10px;">Proyecto</label>
                        <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" /></td>
                        <td><asp:TextBox ID="txtNumPE" style="width:60px; margin-left:3px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;getPEByNum();}else{vtn2(event);setNumPE();}" />
                            <asp:TextBox ID="txtDesPE" style="width:368px;" Text="" runat="server" readonly="true" />
                        </td>
                        <td colspan="2">
                            <label id="lblFecPrevEmFac" runat="server" class="texto">Fecha factura</label> 
                            &nbsp;&nbsp;<asp:TextBox ID="txtFecPrevEmFac" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="aG(0);" lectura=0 />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align:top;">
                            <fieldset style="width:590px;">
                            <legend>Cliente</legend>
                            <table class="texto" style="width:500px; table-layout:fixed;"  cellspacing="0" cellpadding="4" border="0">
                            <colgroup>
                                <col style="width: 130px;" />
                                <col style="width: 450px;" />
                            </colgroup>
                             <tr>
                                <td><label id="lblCliPago" onclick="if (this.className=='enlace') getCliente('R');" class="enlace" title="Cliente responsable del pago de la factura">NIF / Denominación</label> <span style="color:Red">*</span></td>
                                <td><asp:TextBox ID="txtNIFCliPago" style="width:75px;" Text="" runat="server" readonly="true" />&nbsp;&nbsp;<asp:TextBox ID="txtDesCliRespPago" style="width:350px;" Text="" readonly="true" runat="server" /></td>
                            </tr> 
                           </table>
                            </fieldset>
                            <fieldset style="width: 590px;margin-top:10px">
                                <legend>Destinatario de factura</legend>
                            <table border="0" cellpadding="4" cellspacing="0" class="texto" style="width: 500px; table-layout: fixed;">
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
                                    <td><asp:textbox id="txtComentarios" onkeyup="aG(0);" MaxLength="50" runat="server" style="width:430px" /></td>
                                </tr>
                            </table>
                            </fieldset>
                            <fieldset style="width: 590px;margin-top:10px">
                                <legend>Condiciones de pago</legend>
                            <table border="0" cellpadding="4" cellspacing="0" class="texto" style="width: 500px; table-layout: fixed;">
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
                                    <td><asp:DropDownList ID="cboViaPago" runat="server" AppendDataBoundItems="true" onchange="aG(0);" Width="260px">
                                        </asp:DropDownList>	</td>	            
                                </tr>  
                                <tr>            
                                    <td><label id="lblMoneda" runat="server" class="texto" >Moneda</label> <span style="color:Red">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="cboMoneda" runat="server" AppendDataBoundItems="true" onchange="aG(0);" Width="260px">
                                        </asp:DropDownList>	</td>	            
                                </tr> 
                            </table>
                            </fieldset>
                            <fieldset style="width: 590px;margin-top:10px">
                                <legend>Pedido</legend>
                            <table border="0" cellpadding="4" cellspacing="0" class="texto" style="width: 500px; table-layout: fixed;">
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
                            </table>
                            </fieldset>
                        </td>
                        <td colspan="2" rowspan="2" style="vertical-align:top;">
                            <fieldset style="width:260px; padding:5px;">
                                <legend>Indicaciones a Pool</legend>
                                <asp:textbox id="txtObsPool" runat="server" onkeyup="aG(0);" SkinID="Multi" style="width:250px" TextMode="MultiLine" Rows="6"></asp:textbox>
                            </fieldset>
                            <br />
                            <fieldset style="width:260px; padding:5px;">
                                <legend>Indicaciones responsable OF</legend>
                                <asp:textbox id="txtObsPlantilla" runat="server" onkeyup="aG(0);" SkinID="Multi" style="width:250px" TextMode="MultiLine" Rows="6"></asp:textbox>
                            </fieldset>
                            <br />
                            <fieldset style="width:260px; padding:5px;">
                                <legend>Texto cabecera de factura</legend>
                                <asp:textbox id="txtCabFact" runat="server" onkeyup="aG(0);" SkinID="Multi" style="width:250px" TextMode="MultiLine" Rows="6"></asp:textbox>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblOV" runat="server" title="Organización de ventas" class="texto">Sociedad que factura</label> 
                            <span style="color:Red">*</span>
                        </td>
                        <td colspan=2>
                            <asp:DropDownList ID="cboOV" runat="server" AppendDataBoundItems="true" onchange="getDestFactByOV();aG(0);" Width="260px">
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;	
                            <span id="spanAgrupacion" style="visibility:visible;">
                                <label id="lblAgrupacion" runat="server" class="enlace" style="margin-left:10px;" onclick="if (this.className=='enlace') getAgrupacion()" title="Clave de agrupación">Agrupación</label> 
                                <asp:TextBox ID="txtClaveAgru" style="width:60px;" SkinID="Numero" Text="" runat="server" readonly="true" />
                                <img id="imgGomaClave" src="../../../../Images/Botones/imgBorrar.gif" title="Borra la agrupación" onclick="$I('txtClaveAgru').value='';$I('txtClaveAgru').title='';aG(0);this.style.visibility='hidden';" style="cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;">
                            </span>
                        </td>	            
                        <td>
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
		                    <table style="width: 880px; height: 17px">
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
                                    <td>&nbsp;Concepto</td>
				                    <td style="padding-left:7px;" >Unidades</td>
				                    <td style="padding-left:25px;">Precio</td>
                                    <td style="padding-left:20px;" title="Porcentaje a descontar de la posición de la orden de facturación">Dto. %</td>
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
	                                <td align="right" style="text-align:right; padding-right:3px;" ><label id='lblSubtotal'>0,00</label></td>
	                            </tr>
		                    </table>
		                    <table id="tblTotal" style="width: 880px;" cellpadding="3">
		                        <colgroup>
		                            <col style="width:15px;" />
		                            <col style="width:20px;" />
	                                <col style="width:410px;" />
	                                <col style="width:70px;" />
	                                <col style="width:70px;" />
	                                <col style="width:85px;" />
	                                <col style="width:70px;" />
		                            <col style="width:100px;" />
		                            </colgroup>
	                            <tr>
	                                <td colspan="3" rowspan="2">
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
	                                <td title="Porcentaje a descontar de la orden de facturación"><asp:TextBox ID="txtDtoPorc" runat="server" style="width:50px;" Text="" SkinID="Numero" onfocus="fn(this)" onkeyup="if (bLectura) return;aG(0);$I('txtDtoImporte').value='';sit();" /> %</td>
	                                <td title="Importe absoluto a descontar en la moneda de la orden de facturación"><asp:TextBox ID="txtDtoImporte" runat="server" style="width:55px;" Text="" SkinID="Numero" onfocus="fn(this)" onkeyup="if (bLectura) return;aG(0);$I('txtDtoPorc').value='';sit();" /> Imp.</td>
	                                <td></td>
	                                <td align="right" style="text-align:right;"><label id='lblDto'>0,00</label></td>
	                            </tr>
	                            <tr>
	                                <td>IVA incluido</td>
	                                <td><asp:CheckBox id="chkIVA" runat="server" style="vertical-align:middle;cursor:pointer;margin-left:-4px;" onclick="if (bLectura) return;aG(0);" />
	                                </td>
	                                <td></td>
	                                <td style="padding-left:5px;">Total</td>
	                                <td align="right" style="text-align:right;"><label id='lblTotal'>0,00</label></td>
	                            </tr>
		                    </table>
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
		                    <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 866px; height:360px" runat="server">
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
<center>
	<table id="tblBotonera" style="width:390px; position:absolute; left:295px; top: 567px;">
		<colgroup>
			<col style="width:130px;" />
			<col style="width:130px;" />
			<col style="width:130px;" />
		</colgroup>
		<tr>
			<td> 
				<button id="btnGrabar" type="button" onclick="Grabar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
				</button>			
			</td>		        
			<td>
				<button id="btnPrevisualizar" type="button" onclick="PrevisualizaPlantilla();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../../images/botones/imgFacturas.gif" /><span title="Visualiza la orden en formato factura">&nbsp;Visualizar</span>
				</button>				
			</td>        
			<td>
				<button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus"  onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/Botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
				<button id="btnCerrar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" style="display:none;" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/Botones/imgSalir.gif" /><span title="Cerrar">Cerrar</span></button>				
			</td>
		  </tr>
	</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnT305IdProy" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdPlantilla" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnIdCliSolicitante" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdCliPago" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdCliDestFac" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdRespComercial" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstado" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" name="hdnEfactur" id="hdnEfactur" runat="server" value="N" />
<!--<asp:TextBox ID="hdnEsClienteSolicitante" runat="server" style="visibility:hidden" Text="0" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />-->
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
