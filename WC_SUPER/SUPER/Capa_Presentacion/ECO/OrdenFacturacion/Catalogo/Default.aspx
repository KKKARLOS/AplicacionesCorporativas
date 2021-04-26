<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var modolectura_proyectosubnodo_actual = <%=(bLecturaProyectoOF)? "true":"false" %>;
    var bEs_Administrador = <%=(Session["ADMINISTRADOR_PC_ACTUAL"].ToString()!="")? "true":"false" %>;
    </script>
<center>
<table id="tabla" style="width:990px; text-align:left" cellpadding="1" border="0">
    <colgroup>
        <col style="width:60px;" />
        <col style="width:20px;" />
        <col style="width:60px;" />
        <col style="width:590px;" />
        <col style="width:138px;" />
        <col style="width:122px;" />
    </colgroup>
    <tr>
        <td><label id="lblProy" runat="server" title="Proyecto económico" style="width:55px; height:17px; padding-left:10px; margin-top:5px" class="enlace" onclick="if (this.className=='enlace') getPE();">Proyecto</label></td>
        <td align="left"><asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" /></td>
        <td align="center"><asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:50px;" SkinID="Numero" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);setNumPE();}" /></td>
        <td><asp:TextBox ID="txtNomProy" runat="server" Text="" style="width:580px;" readonly="true" /></td>
        <td align='left'>            
            <div id="divCualidadPSN" style="width:130px; height:35px;">
                <asp:Image ID="imgCualidadPSN" runat="server" Height="35" ImageUrl="~/images/imgSeparador.gif" />        
            </div>
             <div align="right" title="Mostrar ordenes erróneas" style="padding-right:10px;margin-bottom:12px;">
                <asp:CheckBox ID="chkErroneas" runat="server" Text="Mostrar OF erróneas" Width="130px" TextAlign=left CssClass="check texto" onclick="getOrdenes();" />     
            </div>
            
        </td>
        <td>
            <fieldset style="width:120px; height:58px; padding:1px; display:inline;">
                <legend><label id="Label1">Fecha factura</label></legend>
                    <table style="width:135px; margin-left:7px;" cellpadding="3px" >
                        <colgroup><col style="width:35px;"/><col style="width:100px;"/></colgroup>
                        <tr>
                            <td>Desde</td>
                            <td>
                                <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('D');" goma="1" />
                            </td>
                        </tr>
                        <tr>
                            <td>Hasta</td>
                            <td>
                                <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('H');" goma="1" />
                            </td>
                        </tr>
                    </table>
            </fieldset>
        </td>
    </tr>
	<tr>
		<td colspan="6">
            <fieldset style="width:980px; padding:5px;">
            <legend>Cabeceras</legend>
		    <table id="Table1" style="width: 960px; height: 17px; margin-top:5px;">
		        <colgroup>
		            <col style="width:20px;" />
		            <col style="width:80px;" />
		            <col style="width:70px;" />
		            <col style="width:80px;" />
		            <col style="width:300px;" />
		            <col style="width:190px;" />
                    <col style="width:130px;" />
                    <col style="width:90px;" />
                </colgroup>
			    <tr id="tblTitulo" class="TBLINI">
				    <td style="padding-left: 2px;" title="Estado">Est.</td>
				    <td style="text-align:right; padding-right:3px;">
                        &nbsp;
                        <IMG style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgPlant" border="0"> 
                        <MAP name="imgPlant">
		                    <AREA onclick="ot('tblOrdenes', 1, 0, '', '')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblOrdenes', 1, 1, '', '')" shape="RECT" coords="0,6,6,11">
	                    </MAP>Nº orden&nbsp;                                  
                    </td>
				    <td style="padding-left:2px;" title="Fecha factura">F. Factura</td>
				    <td>Doc. venta</td>
				    <td>Cliente (NIF) / Denominación&nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOrdenes',4,'divCatalogoOrdenes','imgLupa2')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" /> <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblOrdenes',4,'divCatalogoOrdenes','imgLupa2', event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" />
				    </td>
				    <td>Plazo de pago</td>
				    <td>Sociedad que factura</td>
				    <td title="Referencia en el cliente">Pedido
                        <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOrdenes',7,'divCatalogoOrdenes','imgLupa3')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" /> <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblOrdenes',7,'divCatalogoOrdenes','imgLupa3', event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" />
				    </td>
			    </tr>
		    </table>
		    <div id="divCatalogoOrdenes" style="overflow: auto; overflow-x: hidden; width: 976px; height:160px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:960px">
		        </div>
		    </div>
		    <table id="tblTotal" style="width: 960px; height: 17px;">
			    <tr class="TBLFIN">
				    <td >&nbsp;</td>
			    </tr>
		    </table>
		    <center>
            <table style="width:800px; margin-top:5px;">
		        <tr>
					<td align="center">
						<button id="btnNuevaOrden" type="button" onclick="addOrden();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							 onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">&nbsp;&nbsp;Añadir</span>
						</button>	
					</td>						
					<td align="center">
						<button id="btnEliminarOrden" type="button" onclick="delOrden();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							 onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">&nbsp;&nbsp;Eliminar</span>
						</button>	
					</td>
					<td align="center">
						<button id="btnRecuperar" type="button" onclick="recuperarOrden();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
							 onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../../images/imgRecuperar.gif" /><span title="Recuperar">Recuperar</span>
						</button>	
					</td>	
					<td align="center">
						<button id="btnDuplicar" type="button" onclick="duplicarOrden();" class="btnH25W95" runat="server" hidefocus="hidefocus" disabled
							 onmouseover="se(this, 25);mostrarCursor(this);">
							 <img src="../../../../images/botones/imgDuplicar.gif" /><span title="Duplica la orden de la fila seleccionada">&nbsp;Duplicar</span>
						</button>	 
					</td>  
					<td align="center">
						<button id="btnPrevisualizar" type="button" onclick="PrevisualizaFactura();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
							 onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../../images/botones/imgFacturas.gif" /><span title="Visualiza la orden en formato factura">&nbsp;Visualizar</span>
						</button>	 
					</td>					
					<td align="center">
						<button id="btnCPE" type="button" onclick="cambiarPE();" class="btnH25W105" runat="server" hidefocus="hidefocus" 
								onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../../images/imgIconoProyAzul.gif" /><span title="Cambiar el proyecto a la orden de factuación">&nbsp;Cambiar PE</span>
						</button>	
					</td>	
		        </tr>
            </table>
            </center>
		    <table style="width: 960px;">
                <tr>
                    <td style="padding-top:5px;">
                        <img class="ICO" src="../../../../Images/imgOrdenA.gif" title="Orden aparcada" />Aparcada&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgOrdenT.gif" title="Orden tramitada" />Tramitada&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgOrdenE.gif" title="Orden enviada desde SUPER al Pool de facturación de SAP" />Enviada a Pool&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgOrdenR.gif" title="Orden recogida por el Pool de facturación de SAP" />Recogida por el Pool&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgOrdenF.gif" title="Orden finalizada" />Finalizada&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgOrdenX.gif" title="Orden errónea" />Errónea
                    </td>
                </tr>
            </table>            
            </fieldset>
		</td>
    </tr>
	<tr>
		<td colspan="6">
            <fieldset style="width:980px; padding:5px;">
            <legend>Posiciones</legend>
		    <table style="WIDTH: 960px; height: 17px; margin-top:5px;">
		        <colgroup>
		            <col style="width:25px;" />
		            <col style="width:60px;" />
		            <col style="width:60px;" />
		            <col style="width:435px;" />
		            <col style="width:70px;" />
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
                    <td style="text-align:right; padding-right: 2px;" >Unidades</td>
                    <td style="text-align:right; padding-right: 2px;">Precio</td>
                    <td style="text-align:right; padding-right: 2px;" title="Porcentaje a descontar de la posición de la orden de facturación">Dto. %</td>
                    <td style="text-align:right; padding-right: 2px;" title="Importe absoluto a descontar de la posición, en la moneda de la orden de facturación">Dto. Imp.</td>
                    <td style="text-align:right; padding-right: 25px;" >Importe</td>
			    </tr>
		    </table>
		    <div id="divCatalogoPosiciones" style="overflow: auto; overflow-x: hidden; width: 976px; height:120px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:960px;">
		        </div>
		    </div>
		    <table style="width: 960px; height: 17px">
		        <colgroup>
		            <col style="width:25px;" />
		            <col style="width:60px;" />
		            <col style="width:60px;" />
		            <col style="width:420px;" />
		            <col style="width:70px;" />
		            <col style="width:70px;" />
		            <col style="width:85px;" />
		            <col style="width:70px;" />
		            <col style="width:100px;" />
		        </colgroup>
			    <tr class="TBLFIN">
                    <td colspan="7" style="padding-right:2px; padding-left: 2px;"></td>
                    <td>Subtotal</td>
                    <td style="text-align:right; padding-right: 25px;"><label id='lblSubtotal'>0,00</label></td>
			    </tr>
                <tr style="padding-top:5px;">
                    <td colspan="4" style="padding-right: 2px;"></td>
                    <td style="text-align:right; padding-right: 2px;">Descuento</td>
                    <td style="text-align:right; padding-right: 2px;" title="Porcentaje a descontar de la orden de facturación">
                        <asp:TextBox ID="txtDtoPorc" runat="server" style="width:50px;" Text="" CssClass="txtL" SkinID="Numero" readonly="true" /> %</td>
                    <td style="text-align:right; padding-right: 2px;" title="Importe absoluto a descontar en la moneda de la orden de facturación">
                        <asp:TextBox ID="txtDtoImporte" runat="server" style="width:70px;" Text="" CssClass="txtL" SkinID="Numero" readonly="true" /></td>
                    <td>Imp.</td>
                    <td style="text-align:right; padding-right: 25px;"><label id='lblDto'>0,00</label></td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-right: 2px;">
                        <img class="ICO" src="../../../../Images/imgPosicionD.gif" title="Posición no procesada" />Sin procesar&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgPosicionP.gif" title="Posición procesada en SAP" />Procesada&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgPosicionF.gif" title="Posición facturada en SAP" />Facturada&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgPosicionC.gif" title="Posición contabilizada en SAP" />Contabilizada&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgPosicionA.gif" title="Posición anulada en SAP" />Anulada&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgPosicionB.gif" title="Posición borrada en SAP" />Borrada
                    </td>
                    <td style="text-align:right; padding-right: 2px;"><label id="lblIVA"></label></td>
                    <td style="text-align:right; padding-right: 2px;"></td>
                    <td style="text-align:right; padding-right: 2px;"></td>
                    <td>Total</td>
                    <td style='text-align:right; padding-right: 25px;'><label id='lblTotal'>0,00</label></td>
                </tr>
		    </table>
            </fieldset>
		</td>
    </tr>
</table>
</center>
<asp:TextBox ID="hdnT305IdProy" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnRespContrato" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnDenRespContrato" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCliente" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdCliente" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnNifRespPago" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnOrigen" runat="server" style="visibility:hidden" Text="C" />
<asp:TextBox ID="hdnOVSAP" runat="server" style="visibility:hidden" Text="C" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<ucgus:UCGusano ID="UCGusano1" runat="server" />
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

