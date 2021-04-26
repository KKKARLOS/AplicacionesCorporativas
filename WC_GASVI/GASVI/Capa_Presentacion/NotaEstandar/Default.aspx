<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="false" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_NotaEstandar_Default" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Calculadora/Calculadora.ascx" TagName="Calculadora" TagPrefix="uccalc" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodoCorta = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var sMsgRecuperada = "<%=sMsgRecuperada %>";
    var bLectura = <%=(bLectura)? "true":"false" %>;
    var sOrigen = "<%=sOrigen %>";
    var sDiaLimiteContAnoAnterior = <%=sDiaLimiteContAnoAnterior %>;
    var sDiaLimiteContMesAnterior = <%=sDiaLimiteContMesAnterior %>;
    var bSeleccionBeneficiario = <%=((bool)Session["GVT_MULTIUSUARIO"] || User.IsInRole("T") || User.IsInRole("S") || User.IsInRole("A"))? "true":"false" %>;
    var sNodoUsuario = "<%=sNodoUsuario %>";
    var bAdministrador = <%=(User.IsInRole("A"))? "true":"false" %>;
    var nMinimoKmsECO = <%=Constantes.nNumeroMinimoKmsECO %>;
</script>
<style type="text/css">
#tblGastos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblTotalesGastos TD{border-right: solid 1px #A6C3D2; padding-right:2px;}
#tblTerritorio TD{border-right: solid 1px #A6C3D2; padding-right:2px;} 
#tblAnticipos TD{border-right: solid 1px #A6C3D2; padding-right:2px;}
#tblPagado TD{border-right: solid 1px #A6C3D2; padding-right:2px;}
.TooltipGasto{color:#f2ec50;}
.TBLINIGASTO
{
    FONT-WEIGHT: bold;
    FONT-SIZE: 12px;
    background-color: #77b2c8;/* #6ca5ba;*/
    COLOR: #ffffff;
    FONT-FAMILY: Arial, Helvetica, sans-serif;
    height:17px;
}
#ctl00_CPHC_tsPestanas table { table-layout:auto; }
</style>
<asp:Image ID="imgCalculadora" ImageUrl="~/Capa_Presentacion/UserControls/Calculadora/Images/imgCalculadora50.gif" style="position:absolute; top:285px; left:950px; cursor:pointer; width:33px; height:50px;" onclick="getCalculadora(845, 122);" runat="server" />
<asp:Image ID="imgManoVisador" ImageUrl="~/Images/imgManoVisador.gif" style="position:absolute; top:290px; left:835px; cursor:pointer; width:50px; height:50px; visibility:hidden;" onclick="getVisador();" runat="server" ToolTip="" />
<asp:Image ID="imgJustificantes" ImageUrl="~/Images/imgSeparador.gif" style="position:absolute; top:266px; left:355px; width:40px; height:40px;" runat="server" ToolTip="¿Existen justificantes?" />
<table style="width:990px;" cellpadding="4px" cellspacing="0" border="0">
    <colgroup>
        <col style="width:70px;" />
        <col style="width:350px;" />
        <col style="width:100px;" />
        <col style="width:300px;" />
        <col style="width:170px;" />
    </colgroup>
    <tr>
        <td><label id="lblBeneficiario" class="texto" runat="server">Beneficiario</label></td>
        <td><asp:TextBox ID="txtInteresado" class="txtM" style="width:300px;" Text="" runat="server" ReadOnly="true" /></td>
        <td>Referencia</td>
        <td><asp:TextBox ID="txtReferencia" style="width:60px;" SkinID="numero" Text="" runat="server" ReadOnly="true" /></td>
        <td rowspan="4">
            <asp:Image ID="imgEstado" ImageUrl="~/Images/imgEstado.gif" style="width:160px; height:80px;" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Concepto <span style="color:Red">*</span></td>
        <td><asp:TextBox ID="txtConcepto" style="width:300px;" Text="" runat="server" MaxLength="50" /></td>
        <td>Empresa</td>
        <td><asp:TextBox ID="txtEmpresa" style="width:200px;" Text="" runat="server" ReadOnly="true" />
        <asp:DropDownList ID="cboEmpresa" runat="server" Width="200px" onchange="setEmpresaAux();aG(0);" AppendDataBoundItems="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Motivo</td>
        <td>
            <asp:DropDownList ID="cboMotivo" runat="server" Width="140px" onchange="aG(0);setOblProy();getCC();" AppendDataBoundItems="true"></asp:DropDownList>
        </td>
        <td title="Destino de los justificantes">Dest. justificantes</td>
        <td><asp:TextBox ID="txtOficinaLiq" style="width:200px;" Text="" ReadOnly="true" runat="server" /></td>
    </tr>
    <tr>
        <td>
            <label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label>
            <span id="spanOblProy" style="color:Red;display:none;">*</span>
            <label id="lblNodo" runat="server" style="display:none;"><%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></label>
        </td>
        <td><asp:TextBox ID="txtProyecto" style="width:300px;" Text="" runat="server" ReadOnly="true" />
            <asp:TextBox ID="txtDesNodo" style="width:300px;display:none;" Text="" runat="server" ReadOnly="true" />
        </td>
        <td>Moneda</td>
        <td >
            <asp:DropDownList ID="cboMoneda" runat="server" Width="200px" onchange="aG(0)" AppendDataBoundItems="true"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="vertical-align: text-top;">
            <fieldset style="width: 170px; height:60px; float:left">
                <legend>A cobrar</legend>
                <table style="width:160px;margin: 4px 0px 0px 7px;">
                    <colgroup>
                        <col style="width:80px;" />
                        <col style="width:80px;" />
                    </colgroup>
                    <tr>
                        <td>Sin retención<br /><asp:TextBox ID="txtACobrarDevolver" SkinID="numero" style="width:65px;" Text="" runat="server" ReadOnly="true" /></td><!-- onclick="getCalendarioRango()" -->
                        <td>En nómina<br /><asp:TextBox ID="txtNomina" SkinID="numero" style="width:65px;" Text="0,00" runat="server" ReadOnly="true" /></td><!-- onfocus="fn(this);ic(this.id);" -->
                    </tr>
                </table>
             </fieldset>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <fieldset style="width: 80px; height:60px;float:left; margin: 0px 5px 0px 5px;" >
                <legend>Total viaje</legend>   
                <table style="width:70px; margin-left:3px; margin-top:14px;">
                    <tr>
                        <td><asp:TextBox ID="txtTotalViaje" SkinID="numero" style="width:65px;" Text="" runat="server" ReadOnly="true" /></td>
                    </tr>
                </table>
            </fieldset>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <fieldset style="width: 85px; height:60px; margin-top:-11px">
                <legend>Justificantes <span style="color:Red">*</span></legend>   
                <asp:RadioButtonList ID="rdbJustificantes" CssClass="texto" style="margin: 3px 0px 0px 3px;" runat="server" RepeatColumns="1" onclick="if(!$I('rdbJustificantes_0').disabled){setImagenJustificantes();aG(0);}">
                    <asp:ListItem Value="1" style="cursor:pointer">Sí&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="0" style="cursor:pointer">No</asp:ListItem>
                </asp:RadioButtonList>
             </fieldset>
        </td>
        <td style="vertical-align:top">Observaciones</td>
        <td><asp:TextBox TextMode="multiLine" ID="txtObservacionesNota" style="width:290px; height:70px;" Rows="3" Text="" runat="server" /></td>
        <td style="vertical-align:top">
            <asp:Image ID="imgMail" ImageUrl="~/Images/imgEmailEdit32.png" style="cursor:pointer; display:none;" onclick="mdMail();" runat="server" />
            <div id="divDisposiciones" runat="server" >
                <asp:Image ID="imgDisposiciones" ImageUrl="~/Images/imgDispGen.gif" style="vertical-align:middle;" runat="server" ToolTip="" /> Disposiciones generales
                <br />
            </div>
            <div id="divAnotaciones" style="cursor:pointer; margin-top:3px;;" onclick="getAnotaciones();" runat="server">
                <asp:Image ID="imgAnotaciones" ImageUrl="~/Images/imgAnotacionesPer.gif" style="vertical-align:middle;" runat="server" />
                <nobr id="lblAnotaciones">Anotaciones personales</nobr>
            </div>
            <br />
            <asp:Image ID="imgLote" ImageUrl="~/Images/imgLote.gif" style="cursor:pointer; display:none;" onclick="mdlote();" runat="server" />
        </td>
    </tr>
</table>
<table cellpadding="0" style="width:1000px">
	<tr>
		<td>
		
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="990px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
		                    <eo:TabItem Text-Html="Gastos" Width="100"></eo:TabItem>
		                    <eo:TabItem Text-Html="Anticipos" Width="100"></eo:TabItem>
		                    <eo:TabItem Text-Html="Otros datos" Width="100" ToolTip=""></eo:TabItem>
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" style="height:340px; width:990px" SelectedIndex="1" EnableViewState="False">
                <eo:PageView CssClass="PageView"  style="margin:5px;" runat="server">
				<!-- Pestaña 1 Gastos-->
				<table id="tblTituloGastos" style="width:970px; margin-left:-6px; border:solid 1px #e9e9e9;">
		            <colgroup>
			            <col style="width:130px" />
			            <col style="width:165px" />
			            <col style="width:20px" />
			            <col style="width:30px" />
			            <col style="width:30px" />
			            <col style="width:30px" />
			            <col style="width:30px" />
			            <col style="width:65px" />
			            <col style="width:40px" />
			            <col style="width:65px" />
			            <col style="width:30px" />
			            <col style="width:65px" />
			            <col style="width:65px" />
			            <col style="width:65px" />
			            <col style="width:65px" />
			            <col style="width:75px" />
		            </colgroup>
                    <tr style="vertical-align: text-top;text-align:center" class="TBLINIGASTO" > 
                        <td rowspan="2" style="border-right:solid 1px #e9e9e9;">FECHA</td>
                        <td rowspan="2"></td>
                        <td rowspan="2" style="border-right:solid 1px #e9e9e9;"></td>
                        <td colspan="8" style="border-right:solid 1px #e9e9e9;">SIN JUSTIFICANTE</td>
                        <td rowspan="2" colspan="4" style="border-right:solid 1px #e9e9e9;">CON JUSTIFICANTE</td>
                        <td rowspan="2"></td>
                    </tr>
                    <tr class="TBLINIGASTO" style="height:18px;vertical-align: text-bottom;"> 
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9; text-align:center" colspan="5" >Dietas</td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;" colspan="3">
                            <img id="imgKMSEstandares" src="../../Images/imgSeparador.gif" style="width:16px; height:16px;" border="0" runat="server" title="" />
                            <span style="width:auto; text-align:center; vertical-align: top; margin-left:7px">Vehículo propio</span>
                        </td>
                    </tr>
                    <tr class="TBLINIGASTO" style="text-align:center">
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Inicio&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fin&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td style="border-top:solid 1px #e9e9e9;">Destino</td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;" title="Comentario al gasto"><img src="../../Images/imgComGastoOn.gif" border="0" /></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;" id="cldDC"><nobr id="nbCompleta" runat="server" style="noWrap:true;" class="TooltipGasto" title="">C</nobr></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;" id="cldDM"><nobr id="nbMedia" runat="server" style="noWrap:true;" class="TooltipGasto" title="">M</nobr></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;" id="cldDA"><nobr id="nbEspecial" runat="server" style="noWrap:true;" class="TooltipGasto" title="">E</nobr></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;" id="cldDE"><nobr id="nbAlojamiento" runat="server" style="noWrap:true;" class="TooltipGasto" title="">A</nobr></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Importe</td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Kms.</td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Importe</td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">ECO</td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;"><label id="lblPeajes" runat="server" class="TooltipGasto" title="">Peajes</label></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;"><label id="lblComidas" runat="server" class="TooltipGasto" title="">Comidas</label></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;"><label id="lblTransporte" runat="server" class="TooltipGasto" title="">Transp.</label></td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Hoteles</td>
                        <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">TOTAL</td>
                    </tr>
                </table>
                <div id="divCatalogoGastos" style="margin-left:-5px; overflow-x:hidden; overflow-y:auto; width: 986px; height:200px;">
                    <div id="divFondoCatalogoGastos" runat="server" style="background-image:url('../../Images/imgFT20.gif'); WIDTH: 970px;">
 				        <table id="tblGastos" cellpadding="0" class="MANO" style="width:970px;" border="0" mantenimiento="1">
		                    <colgroup>
			                    <col style="width:130px" />
			                    <col style="width:165px" />
			                    <col style="width:20px" />
			                    <col style="width:30px" />
			                    <col style="width:30px" />
			                    <col style="width:30px" />
			                    <col style="width:30px" />
			                    <col style="width:65px" />
			                    <col style="width:40px" />
			                    <col style="width:65px" />
			                    <col style="width:30px" />
			                    <col style="width:65px" />
			                    <col style="width:65px" />
			                    <col style="width:65px" />
			                    <col style="width:65px" />
			                    <col style="width:75px" />
		                    </colgroup>  
                        </table>
                    </div>
                </div>
				<table id="tblTotalesGastos" style="width:970px; margin-left:-5px;">
		            <colgroup>
			            <col style="width:130px" />
			            <col style="width:165px" />
			            <col style="width:20px" />
			            <col style="width:30px" />
			            <col style="width:30px" />
			            <col style="width:30px" />
			            <col style="width:30px" />
			            <col style="width:65px" />
			            <col style="width:40px" />
			            <col style="width:65px" />
			            <col style="width:30px" />
			            <col style="width:65px" />
			            <col style="width:65px" />
			            <col style="width:65px" />
			            <col style="width:65px" />
			            <col style="width:75px" />
		            </colgroup>
		            <tr class="TBLFIN" style="text-align:right"> 
                        <td colspan="3" style="text-align:center"> 
                            <div class="texto">TOTAL</div>
                        </td>
                        <td><label id="txtGSTDC">0</label></td>
                        <td><label id="txtGSTMD">0</label></td>
                        <td><label id="txtGSTDE">0</label></td>
                        <td><label id="txtGSTAL">0</label></td>
                        <td><label id="txtGSTIDI">0,00</label></td>
                        <td><label id="txtGSTKM">0</label></td>
                        <td><label id="txtGSTIKM">0,00</label></td>
                        <td></td>
                        <td><label id="txtGSTPE">0,00</label></td>
                        <td><label id="txtGSTCO">0,00</label></td>
                        <td><label id="txtGSTTR">0,00</label></td>
                        <td><label id="txtGSTHO">0,00</label></td>
                        <td><label id="txtGSTOTAL">0,00</label></td>
                    </tr>
                </table>
                <div id="divIconosGastos" style="text-align:right;margin-right:50px; margin-top:2px; visibility:hidden">
                    <img src="../../Images/imgNuevoGasto.gif" class="ICO MANO" onclick="addGasto(true)" title="Añade nueva fila de gasto" />
                    <img src="../../Images/imgEliminarGasto.gif" class="ICO MANO" onclick="delGasto()" title="Elimina la fila de gasto seleccionada" />
                    <img src="../../Images/imgDuplicarGasto.gif" class="ICO MANO" onclick="dupGasto()" title="Duplica fila de gasto seleccionada" />
                </div>
                </eo:PageView>
                
                <eo:PageView ID="PageView1" CssClass="PageView" style="margin:5px;" runat="server">
				<!-- Pestaña 2 Anticipos y Empresa-->
				<table id="" style="width: 970px; margin-top:10px;">
				    <tr>
				        <td>
                            <fieldset id="Fieldset1" style="width: 960px; height:140px;">
				                <legend>LIQUIDACIÓN DE ANTICIPOS (Sólo si hay anticipos a liquidar)</legend>
				                <table id="tblAnticipos" style="width: 940px; margin:5px; margin-top:10px;" border="0" cellpadding="5">
				                    <colgroup>
				                        <col style="width:50px; padding-left:3px;" />
				                        <col style="width:220px;" />
				                        <col style="width:50px;" />
				                        <col style="width:230px;" />
				                        <col style="width:390px;" />
				                    </colgroup>
				                    <tr class="TBLINIGASTO" style="height:24px; text-align:center;">
				                        <td colspan="2" style=" border: solid 1px #e9e9e9;">Anticipado</td>
				                        <td colspan="2" style=" border: solid 1px #e9e9e9;">Devuelto</td>
				                        <td style=" border: solid 1px #e9e9e9;">Aclaraciones</td>
				                    </tr>
				                    <tr class="FA">
				                        <td>Fecha</td>
				                        <td><asp:TextBox ID="txtFecAnticipo" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(this);" onchange="aG(0);" ReadOnly="true" runat="server" goma="1" /></td>
				                        <td>Fecha</td>
				                        <td><asp:TextBox ID="txtFecDevolucion" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(this);" onchange="aG(0);" ReadOnly="true" runat="server" goma="1" /></td>
				                        <td rowspan="3">
                                            <asp:TextBox TextMode="multiLine" ID="txtAclaracionesAnticipos" style="width:380px; height:70px;" Rows="3" Text="" runat="server" />
				                        </td>
				                    </tr>
				                    <tr class="FB">
				                        <td>Importe</td>
				                        <td><asp:TextBox ID="txtImpAnticipo" style="width:60px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setTotales();aG(1);" /></td>
				                        <td>Importe</td>
				                        <td><asp:TextBox ID="txtImpDevolucion" style="width:60px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setTotales();aG(1);" /></td>
				                    </tr>
				                    <tr class="FA">
				                        <td>Oficina</td>
				                        <td><asp:TextBox ID="txtOficinaAnticipo" style="width:200px;" MaxLength="50" Text="" runat="server" /></td>
				                        <td>Oficina</td>
				                        <td><asp:TextBox ID="txtOficinaDevolucion" style="width:200px;" MaxLength="50" Text="" runat="server" /></td>
				                    </tr>
				                </table>
				            </fieldset><br /><br />
				    	</td>
				    </tr>
				    <tr>
				        <td>
                            <fieldset id="fldGastos" style="width: 960px; height:140px;">
				                <legend>GASTOS PAGADOS POR LA EMPRESA (Sólo si se va a facturar al cliente. Importes sin IVA) </legend>
				                <table id="tblPagado" style="width: 940px; margin:5px; margin-top:10px;" border="0" cellpadding="5">
				                <colgroup>
				                    <col style="width:150px; padding-left:3px;" />
				                    <col style="width:100px; text-align:right; padding-right:2px;" />
				                    <col style="width:100px; text-align:right; padding-right:2px;" />
				                    <col style="width:100px; text-align:right; padding-right:2px;" />
				                    <col style="width:100px; text-align:right; padding-right:2px;" />
				                    <col style="width:390px;" />
				                </colgroup>
				                <tr class="TBLINIGASTO" style="height:24px;">
				                    <td rowspan="2" style="border: solid 1px #e9e9e9; text-align:center;">Billetes de agencia</td>
				                    <td style=" border: solid 1px #e9e9e9; text-align:center;">Transporte</td>
				                    <td style=" border: solid 1px #e9e9e9; text-align:center;">Hotel</td>
				                    <td style=" border: solid 1px #e9e9e9; text-align:center;">Otros</td>
				                    <td style=" border: solid 1px #e9e9e9; text-align:center;">Total &euro;</td>
				                    <td style=" border: solid 1px #e9e9e9; text-align:center;">Aclaraciones</td>
				                </tr>
				                <tr class="FA" style=" vertical-align:top;">
				                    <td><asp:TextBox ID="txtPagadoTransporte" style="width:90px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setPagadoEmpresa();aG(1);" /></td>
				                    <td><asp:TextBox ID="txtPagadoHotel" style="width:90px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setPagadoEmpresa();aG(1);" /></td>
				                    <td><asp:TextBox ID="txtPagadoOtros" style="width:90px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setPagadoEmpresa();aG(1);" /></td>
				                    <td><asp:TextBox ID="txtPagadoTotal" style="width:90px;" SkinID="numero" Text="" runat="server" ReadOnly="true" /></td>
				                    <td><asp:TextBox TextMode="multiLine" ID="txtAclaracionesPagado" style="width:380px; height:70px;" Rows="3" Text="" runat="server" /></td>
				                </tr>
				                </table>
                                        
				            </fieldset>
				        </td>
				    </tr>
				</table>
                </eo:PageView>
                
                <eo:PageView ID="PageView2" CssClass="PageView" style="margin:5px;" runat="server">
				<!-- Pestaña 3 Otros datos-->
				<table style="width:990px">
				    <colgroup>
				        <col style="width: 330px;" />
				        <col style="width: 660px;" />
				    </colgroup>
				    <tr style="vertical-align: text-top;">
				        <td>
                        <div style="text-align:center;background-image: url('../../Images/imgFondo240.gif'); background-repeat:no-repeat;
                            width:240px; height:23px; position:relative; top:12px; left:20px; padding-top:5px;text-align:center;
                            font:bold 12px Arial; color:#5894ae;">Territorio fiscal: 
                            <asp:Label ID="lblTerritorio" runat="server" style="font:bold 12px Arial; color:#5894ae;"></asp:Label>
                        </div>
                        <table style="width:320px;" cellpadding="0">
                            <tr>
                                <td style="background-image: url('../../Images/Tabla/7.gif'); height:6px; width:6px"></td>
                                <td style="background-image: url('../../Images/Tabla/8.gif'); height:6px;"></td>
                                <td style="background-image: url('../../Images/Tabla/9.gif'); height:6px; width:6px"></td>
                            </tr>
                            <tr>
                                <td style="background-image: url('../../Images/Tabla/4.gif'); width:6px">&nbsp;</td>
                                <td style="background-image: url('../../Images/Tabla/5.gif'); padding: 5px; vertical-align:top;">
                                    <!-- Inicio del contenido propio de la página -->
				                    <table id="tblTerritorio" style="width: 300px; margin-top:5px;">
				                        <colgroup>
				                            <col style="width:100px;" />
				                            <col style="width:80px; " />
				                            <col style="width:120px; " />
				                        </colgroup>
				                        <tr class="TBLINIGASTO">
				                            <td rowspan="2" style="padding-left:3px;border-left:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;border-top:solid 1px #e9e9e9; text-align:center;">Conceptos</td>
				                            <td colspan="2" style="text-align:center;border-bottom:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;border-top:solid 1px #e9e9e9;">Importes &euro;</td>
				                        </tr>
				                        <tr class="TBLINIGASTO">
				                            <td style="text-align:center;border-right:solid 1px #e9e9e9;">Convenio</td>
				                            <td style="text-align:center;border-right:solid 1px #e9e9e9;">Exento de retención</td>
				                        </tr>
				                        <tr class="FA">
				                            <td style="padding-left:3px;">Kilometraje</td>
				                            <td style="text-align:center;" id="cldKMCO" runat="server" >0,00</td>
				                            <td id="cldKMEX" runat="server" style="border-right:0px;text-align:center;"></td>
				                        </tr>
				                        <tr class="FB">
				                            <td style="padding-left:3px;">Dieta completa</td>
				                            <td style="text-align:center;" id="cldDCCO" runat="server">0,00</td>
				                            <td id="cldDCEX" runat="server" style="border-right:0px;text-align:center;">0,00</td>
				                        </tr>
				                        <tr class="FA">
				                            <td style="padding-left:3px;">Media dieta</td>
				                            <td style="text-align:center;" id="cldMDCO" runat="server">0,00</td>
				                            <td id="cldMDEX" runat="server" style="border-right:0px;text-align:center;">0,00</td>
				                        </tr>
				                        <tr class="FB">
				                            <td style="padding-left:3px;">Dieta especial</td>
				                            <td style="text-align:center;" id="cldDECO" runat="server">0,00</td>
				                            <td id="cldDEEX" runat="server" style="border-right:0px;text-align:center;">0,00</td>
				                        </tr>
				                        <tr class="FA">
				                            <td style="padding-left:3px;">Dieta alojamiento</td>
				                            <td style="text-align:center;" id="cldDACO" runat="server">0,00</td>
				                            <td id="cldDAEX" runat="server" style="border-right:0px;text-align:center;">0,00</td>
				                        </tr>
				                        <tr class="TBLFIN">
				                            <td colspan="3" style="border-right:0px;">&nbsp;</td>
				                        </tr>
				                    </table>
                                    <!-- Fin del contenido propio de la página -->
                                </td>
                                <td style="background-image: url('../../Images/Tabla/6.gif'); width:6px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="background-image: url('../../Images/Tabla/1.gif'); height:6px; width:6px"></td>
                                <td style="background-image: url('../../Images/Tabla/2.gif'); height:6px;"></td>
                                <td style="background-image: url('../../Images/Tabla/3.gif'); height:6px; width:6px"></td>
                            </tr>
                        </table>
				        </td>
				        <td rowspan="2">
                        <div style="text-align:center;background-image: url('../../Images/imgFondo100.gif'); background-repeat:no-repeat;
                            width: 100px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
                            font:bold 12px Arial; color:#5894ae;">Historial</div>
                        <table style="width:647px;" cellpadding="0">
                            <tr>
                                <td style="background-image: url('../../Images/Tabla/7.gif'); height:6px; width:6px">
                                </td>
                                <td style="background-image: url('../../Images/Tabla/8.gif'); height:6px;">
                                </td>
                                <td style="background-image: url('../../Images/Tabla/9.gif'); height:6px; width:6px">
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('../../Images/Tabla/4.gif'); width:6px">
                                    &nbsp;</td>
                                <td style="background-image: url('../../Images/Tabla/5.gif'); padding: 5px; vertical-align:top;">
                                    <!-- Inicio del contenido propio de la página -->
                                    <table id="tblTituloHistorial" style="width:610px; height:17px; margin-top:10px;">
	                                    <colgroup>					
		                                    <col style="width:105px; padding-left:3px;" />
		                                    <col style="width:105px;" />
		                                    <col style="width:400px;" />
	                                    </colgroup>
	                                    <tr class="TBLINI">				    
		                                    <td>Estado</td>
		                                    <td>Fecha</td>
		                                    <td>Profesional / Causa</td>
	                                    </tr>
                                    </table>
                                    <div id="divCatalogoHistorial" style="overflow-x:hidden; overflow:auto; width:626px; height:225px" runat="server">
	                                    <div style="width:0%; height:0%"></div>
                                    </div>
                                    <table id="tblPieHistorial" style="width:610px; height:17px;">
	                                    <tr class="TBLFIN">
		                                    <td>&nbsp;</td>
	                                    </tr>
                                    </table>
                                    <!-- Fin del contenido propio de la página -->
                                </td>
                                <td style="background-image: url('../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="background-image: url('../../Images/Tabla/1.gif'); height:6px; width:6px;">
                                </td>
                                <td style="background-image: url('../../Images/Tabla/2.gif'); height:6px;">
                                </td>
                                <td style="background-image: url('../../Images/Tabla/3.gif'); height:6px; width:6px;">
                                </td>
                            </tr>
                        </table>
				    </td>
				    </tr>
				    <tr>
				        <td style="vertical-align: text-bottom">
                            <div id="divContabilizacion" style="text-align:center;background-image: url('../../Images/imgFondo185.gif'); background-repeat:no-repeat;
                                width: 185px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
                                font:bold 12px Arial; visibility:hidden; color:#5894ae;">Datos para contabilización</div>
                            <table id="tblContabilizacion" cellpadding="0" style="width:320px; height:109px; visibility:hidden;">                        
                                <tr style="height:6px;"  >
                                    <td style="background-image: url('../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/8.gif'); height:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                                </tr>
                                <tr style="height:97px;">
                                    <td style="background-image: url('../../Images/Tabla/4.gif'); height:6px;">&nbsp;</td>
                                    <td style="background-image: url('../../Images/Tabla/5.gif'); padding: 5px; vertical-align:top;">
                                        <!-- Inicio del contenido propio de la página -->
				                        <table id="tblTC" style="width: 300px; margin-top:5px; padding:5px;">
				                            <colgroup>
				                                <col style="padding-left:3px;" />
				                            </colgroup>
				                            <tr>
				                                <td style="padding-left:5px;">Fecha <asp:TextBox ID="txtFecContabilizacion" style="width:60px; margin-left:5px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(this);" onchange="aG(0);" ReadOnly="true" runat="server" goma="0" /></td>
				                            </tr>
				                            <tr>
				                                <td>
				                                    <fieldset id="flsTipoCambio" style="width:292px; padding:5px; visibility:hidden;">
				                                        <legend>Tipo de cambio</legend>
				                                        <label>1 &euro; equivale a</label><br />
				                                        <asp:TextBox ID="txtTipoCambio" style="width:60px; margin-top:3px;" SkinID="numero" Text="" runat="server" onfocus="fn(this, 3, 4)" />
				                                        <label id="lblLiteralMoneda"></label>
				                                    </fieldset>
				                                </td>
				                            </tr>
				                        </table>
                                        <!-- Fin del contenido propio de la página -->
                                    </td>
                                    <td style="background-image: url('../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                                </tr>
                                <tr style="height:6px;">
                                    <td style="background-image: url('../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/2.gif'); height:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                                </tr>
                            </table>
				        </td>
				    </tr>
				</table>
                </eo:PageView>
           </eo:multipage>
        </td>
    </tr>
</table>
<div id="divExportar" runat="server" 
    style="position: absolute;
            top:250px;
            left:300px;
            width:350px;
            z-index:10;
            display:none"
    class="texto">
	<table style="border:1px solid #5894ae;width:350px; height:30px; background-color:#bcd4df;text-align:center">
		<tr>
			<td style="padding-left:5px;"><b><font size="3">Modelo de exportación</font></b></td>
		</tr>
	</table>
	<table style="border:1px solid #5894ae; width:350px; background-color:#D8E5EB;text-align:center">
	    <tr><td><br /></td></tr>
	    <tr>
	        <td>
	            <center>
	                <table>
	                    <tr>
	                        <td>
	                    	    <table style="text-align:left;width:150px;">
	                                <tr>
	                                    <td>
			                                <asp:radiobuttonlist id="rdbMasivo" runat="server" CssClass="texto" width="200px" CellSpacing="0" CellPadding="0" RepeatDirection="vertical" RepeatLayout="Flow">
				                                <asp:ListItem Selected="True" Value="1">Solicitud individual</asp:ListItem>
				                                <asp:ListItem Value="2">Lote completo</asp:ListItem>
			                                </asp:radiobuttonlist> 
			                            </td>
			                        </tr>
			                    </table>
	                        </td>
	                    </tr>
	                    <tr>
						    <td><br /></td>
					    </tr>
                        <tr>
                            <td>	
						        <table style="text-align:left;margin-top:5px; width:220px">
								        <tr>
									        <td style="text-align:center">
									            <button id="btnAceptar" type="button" onclick="aceptarExportacion();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>
									        </td>
									        <td style="text-align:center">
									            <button id="btnSalir" type="button" onclick="$I('divExportar').style.display='none'" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>
									        </td>
								        </tr>
						        </table> 
                            </td>
                       </tr>
                       <tr><td><br /></td></tr>
                    </table>
                </center>
             </td> 
        </tr>			
    </table>
</div> 	
<asp:TextBox ID="hdnReferencia" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnInteresado" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstado" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstadoAnterior" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnOficinaLiquidadora" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnOficinaBase" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnotacionesPersonales" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDEmpresa" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDTerritorio" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAutorresponsable" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDLote" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnToolTipKmEstan" runat="server" style="visibility:hidden" TextMode="MultiLine" Text="" />

<asp:TextBox ID="hdnCentroCoste" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnNodoCentroCoste" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnNodoBeneficiario" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCCIberper" runat="server" style="visibility:hidden" Text="" />

<input type="hidden" name="hdnMsg" id="hdnMsg" value="" runat="server" />

<uccalc:Calculadora ID="Calculadora" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">
    function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
		    switch (strBoton){
			    case "tramitar": 
			    {
                    bEnviar = false;
                    //alert("tramitar");
                    tramitar();
				    break;
			    }
			    case "aparcar": 
			    {
                    bEnviar = false;
                    //alert("aparcar");
                    aparcar();
				    break;
			    }
			    case "cancelar":
			    {
			        bEnviar = false;
			        bCambios = false;
			        location.href = "../Inicio/Default.aspx";
			        break;
			    }
			    case "aprobar": 
			    {
                    bEnviar = false;
                    aprobar();
				    break;
			    }
			    case "noaprobar": 
			    {
                    bEnviar = false;
                    noaprobar();
				    break;
			    }
			    case "aceptarnota": 
			    {
                    bEnviar = false;
                    aceptar();
				    break;
			    }
			    case "noaceptar": 
			    {
                    bEnviar = false;
                    noaceptar();
				    break;
			    }
			    case "anular": 
			    {
                    bEnviar = false;
                    anular();
				    break;
			    }
				case "pdf": //Boton exportar pdf
				{
					bEnviar = false;
					Exportar(false);
					break;
				}
                case "eliminar":
                    {
                        bEnviar = false;
                        mostrarProcesando();
                        setTimeout("eliminar();", 20);
                        break;
                    }
		    }
	    }
	    var theform;
	    theform = document.forms[0];
	    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	    theform.__EVENTARGUMENT.value = eventArgument;
	    if (bEnviar){
		    theform.submit();
	    }
	   /* else{
		    //Si se ha "cortado" el submit, se restablece el estado original de la botonera.
		    $I("Botonera").restablecer();
	    }*/
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

