<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_NotaEstandar_Default" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Calculadora/Calculadora.ascx" TagName="Calculadora" TagPrefix="uccalc" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodoCorta = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var bSeleccionBeneficiario = <%=((bool)Session["GVT_MULTIUSUARIO"] || User.IsInRole("T") || User.IsInRole("S") || User.IsInRole("A"))? "true":"false" %>;
    var sToolTipProyectoPorDefecto = "<%=sToolTipProyectoPorDefecto %>";
    var sNodoUsuario = "<%=sNodoUsuario %>";
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
<asp:Image ID="imgCalculadora" ImageUrl="~/Capa_Presentacion/UserControls/Calculadora/Images/imgCalculadora50.gif" style="position:absolute; top:300px; left:950px; cursor:pointer; width:33px; height:50px;" onclick="getCalculadora(845, 122);" runat="server" />
<asp:Image ID="imgJustificantes" ImageUrl="~/Images/imgJustREQ.gif" style="position:absolute; top:273px; left:350px; width:40px; height:40px;" runat="server" ToolTip="¿Existen justificantes?" />
<table style="width:990px;" cellpadding="4">
    <colgroup>
        <col style="width:70px;" />
        <col style="width:350px;" />
        <col style="width:100px;" />
        <col style="width:300px;" />
        <col style="width:170px;" />
    </colgroup>
    <tr>
        <td><label id="lblBeneficiario" class="texto" runat="server">Beneficiario</label></td>
        <td><asp:TextBox ID="txtInteresado" style="width:330px;" Text="" runat="server" ReadOnly="true" /></td>
        <td></td>
        <td></td>
        <td rowspan="3">
            <asp:Image ID="imgEstado" ImageUrl="~/Images/imgEstado.gif" style="width:160px; height:80px;" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Concepto <span style="color:Red;">*</span></td>
        <td><asp:TextBox ID="txtConcepto" style="width:330px;" Text="" runat="server" MaxLength="50" /></td>
        <td>Empresa</td>
        <td><asp:TextBox ID="txtEmpresa" style="width:200px;" Text="" runat="server" ReadOnly="true" />
        <asp:DropDownList ID="cboEmpresa" runat="server" Width="200px" onchange="aG(0);setEmpresaAux();" AppendDataBoundItems="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td rowspan="2" colspan="2">
            <label id="lblProy" class="enlace" onclick="getPE()" style=" margin-left:2px;">Proyecto defecto</label><br />
            <asp:TextBox ID="txtProyecto" style="width:380px; margin-top:3px;" Text="" runat="server" ReadOnly="true" />
            <img id="imgGoma" src='../../Images/Botones/imgBorrar.gif' title="Borra el proyecto por defecto" onclick="delPEDefecto()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
        </td>
        <td title="Destino de los justificantes">Dest. justificantes</td>
        <td><asp:TextBox ID="txtOficinaLiq" style="width:200px;" Text="" ReadOnly="true" runat="server" /></td>
    </tr>
    <tr>
        <td>Moneda</td>
        <td colspan="2">
            <asp:DropDownList ID="cboMoneda" runat="server" Width="200px" onchange="aG(0)" AppendDataBoundItems="true"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="vertical-align: text-top;">
            <fieldset style="width:170px; height:55px;float:left">
                <legend>A cobrar</legend>
                <table style="width:160px; margin-left:7px;">
                    <colgroup>
                        <col style="width:80px;" />
                        <col style="width:80px;" />
                    </colgroup>
                    <tr>
                        <td>Sin retención<br />
                            <asp:TextBox ID="txtACobrarDevolver" SkinID="numero" style="width:65px;" Text="" runat="server" ReadOnly="true" />
                        </td><!-- onclick="getCalendarioRango()" -->
                        <td>En nómina<br />
                            <asp:TextBox ID="txtNomina" SkinID="numero" style="width:65px;" Text="0,00" runat="server" ReadOnly="true" />
                        </td><!-- onfocus="fn(this);ic(this.id);" -->
                    </tr>
                </table>
             </fieldset>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <fieldset style="width:80px; height:55px;float:left; margin: 0px 5px 0px 5px;">
                <legend>Total viaje</legend>   
                <table style="width:70px; margin-left:3px; margin-top:14px;">
                    <tr>
                        <td><asp:TextBox ID="txtTotalViaje" SkinID="numero" style="width:65px;" Text="" runat="server" ReadOnly="true" /></td>
                    </tr>
                </table>
             </fieldset>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <fieldset style="width:85px; height:55px;margin-top:-11px">
                <legend>Justificantes <span style="color:Red">*</span></legend>   
                <asp:RadioButtonList ID="rdbJustificantes" CssClass="texto" style="margin-left:3px;" runat="server" RepeatColumns="1" onclick="if(!$I('rdbJustificantes_0').disabled){setImagenJustificantes();aG(0);}">
                    <asp:ListItem Value="1" style="cursor:pointer">Sí&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="0" style="cursor:pointer">No</asp:ListItem>
                </asp:RadioButtonList>
             </fieldset>
        </td>
        <td>Observaciones</td>
        <td><asp:TextBox TextMode="multiLine" ID="txtObservacionesNota" style="width:290px; height:70px;" Rows="3" Text="" runat="server" /></td>
        <td>
            <div id="divDisposiciones" runat="server" style="margin-top:-35px">
                <asp:Image ID="imgDisposiciones" ImageUrl="~/Images/imgDispGen.gif" style="vertical-align:middle;" runat="server" ToolTip="" /> Disposiciones generales<br />
            </div>
            <div id="divAnotaciones" style="cursor:pointer; margin-top:3px; vertical-align:bottom;" onclick="getAnotaciones();" runat="server">
                <asp:Image ID="imgAnotaciones" ImageUrl="~/Images/imgAnotacionesPer.gif" style="vertical-align:middle;" runat="server" /> Anotaciones personales
            </div>
        </td>
    </tr>
</table>
<table style="width:1000px">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="993px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
	                    <eo:TabItem Text-Html="Gastos" Width="100"></eo:TabItem>
	                    <eo:TabItem Text-Html="Otros datos" Width="100"></eo:TabItem>
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

			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Height="340px" SelectedIndex="1" EnableViewState="False">
                <eo:PageView CssClass="PageView" style="margin:5px; width:981px" runat="server">

				<!-- Pestaña 1 Gastos-->
				    <table id="tblTituloGastos" style="width:970px; margin-left:-6px; border:solid 1px #e9e9e9;">
		                <colgroup>
			                <col style="width:130px" />
			                <col style="width:165px" />
			                <col style="width:70px" />
			                <col style="width:20px" />
			                <col style="width:25px" />
			                <col style="width:25px" />
			                <col style="width:25px" />
			                <col style="width:25px" />
			                <col style="width:65px" />
			                <col style="width:40px" />
			                <col style="width:65px" />
			                <col style="width:30px" />
			                <col style="width:55px" />
			                <col style="width:55px" />
			                <col style="width:55px" />
			                <col style="width:55px" />
			                <col style="width:65px" />
		                </colgroup>
                        <tr style="vertical-align: text-top;text-align:center" class="TBLINIGASTO" > 
                            <td rowspan="2" style="border-right:solid 1px #e9e9e9;">FECHA</td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2" style="border-right:solid 1px #e9e9e9;"></td>
                            <td colspan="8" style="border-right:solid 1px #e9e9e9;">SIN JUSTIFICANTE</td>
                            <td rowspan="2" colspan="4" style="border-right:solid 1px #e9e9e9;">CON JUSTIFICANTE</td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr class="TBLINIGASTO" style="height:18px;vertical-align: text-bottom"> 
                            <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;text-align:center" colspan="5">Dietas</td>
                            <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" colspan="3">
                                <img id="imgKMSEstandares" src="../../Images/imgSeparador.gif" style="width:16px; height:16px;" border="0" runat="server" title="" />
                                <span style="width:auto; text-align:center; vertical-align: top; margin-left:7px">Vehículo propio</span>
                            </td>
                        </tr>
                        <tr class="TBLINIGASTO" style="text-align:center">
                            <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Inicio&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fin&nbsp;</td>
                            <td style="border-top:solid 1px #e9e9e9;">Destino</td>
                            <td style="border-top:solid 1px #e9e9e9;">Proyecto</td>
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
                    <div id="divCatalogoGastos" style="margin-left:-5px; overflow-x:hidden; overflow-y:auto; WIDTH: 986px; height:240px;">
                        <div id="divFondoCatalogoGastos" runat="server" style="background-image:url('../../Images/imgFT20.gif'); WIDTH: 970px;">
 				            <table id="tblGastos" class="MANO" style="width:970px;" border="0" mantenimiento="1">
                                <colgroup>
                                    <col style="width:130px" />
                                    <col style="width:165px" />
                                    <col style="width:70px" />
                                    <col style="width:20px" />
                                    <col style="width:25px" />
                                    <col style="width:25px" />
                                    <col style="width:25px" />
                                    <col style="width:25px" />
                                    <col style="width:65px" />
                                    <col style="width:40px" />
                                    <col style="width:65px" />
                                    <col style="width:30px" />
                                    <col style="width:55px" />
                                    <col style="width:55px" />
                                    <col style="width:55px" />
                                    <col style="width:55px" />
                                    <col style="width:65px" />
                                </colgroup>  
                            </table>
                        </div>
                    </div>
	                <table id="tblTotalesGastos" style="width:970px; margin-left:-5px;">
                        <colgroup>
	                        <col style="width:130px" />
	                        <col style="width:165px" />
	                        <col style="width:70px" />
	                        <col style="width:20px" />
	                        <col style="width:25px" />
	                        <col style="width:25px" />
	                        <col style="width:25px" />
	                        <col style="width:25px" />
	                        <col style="width:65px" />
	                        <col style="width:40px" />
	                        <col style="width:65px" />
	                        <col style="width:30px" />
	                        <col style="width:55px" />
	                        <col style="width:55px" />
	                        <col style="width:55px" />
	                        <col style="width:55px" />
	                        <col style="width:65px" />
                        </colgroup>
                        <tr class="TBLFIN" style="text-align:right"> 
                            <td colspan="4" style="text-align:center"> 
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
                    <div style="text-align:right;margin-right:50px; margin-top:2px;">
                    <img src="../../Images/imgNuevoGasto.gif" class="ICO MANO" onclick="addGasto(true)" title="Añade nueva fila de gasto" />
                    <img src="../../Images/imgEliminarGasto.gif" class="ICO MANO" onclick="delGasto()" title="Elimina la fila de gasto seleccionada" />
                    <img src="../../Images/imgDuplicarGasto.gif" class="ICO MANO" onclick="dupGasto()" title="Duplica fila de gasto seleccionada" />
                </div>
                </eo:PageView>
                
				<eo:PageView ID="PageView1" CssClass="PageView" style="margin:5px; width:981px" runat="server">
				<!-- Pestaña 2 Otros datos-->
				    <table style="width:990px">
				    <colgroup>
				        <col style="width: 330px;" />
				        <col style="width: 660px;" />
				    </colgroup>
				    <tr style="vertical-align: text-top;">
				        <td>
                            <div style="text-align:center; background-image: url('../../Images/imgFondo240.gif'); background-repeat:no-repeat;
                                width: 240px; height: 23px; position: relative; top: 12px; left: 20px; padding-top:5px; text-align:center;
                                font:bold 12px Arial; color:#5894ae;">Territorio fiscal: <asp:Label ID="lblTerritorio" runat="server" style="font:bold 12px Arial; color:#5894ae;"></asp:Label></div>
                            <table style="width:320px;" cellpadding="0">
                                <tr>
                                    <td style="background-image: url('../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/8.gif'); height:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                                </tr>
                                <tr>
                                    <td style="background-image: url('../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                                    <td style="background-image: url('../../Images/Tabla/5.gif'); padding:5px; vertical-align:top;">
                                        <!-- Inicio del contenido propio de la página -->
				                        <table id="tblTerritorio" style="width: 300px; margin-top:5px;">
				                            <colgroup>
				                                <col style="width:100px;" />
				                                <col style="width:80px; text-align:center;" />
				                                <col style="width:120px; text-align:center;" />
				                            </colgroup>
				                            <tr class="TBLINIGASTO">
				                                <td rowspan="2" style="padding-left:3px;border-left:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9; border-top:solid 1px #e9e9e9;">Conceptos</td>
				                                <td colspan="2" style="text-align:center;border-bottom:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9; border-top:solid 1px #e9e9e9;">Importes &euro;</td>
				                            </tr>
				                            <tr class="TBLINIGASTO">
				                                <td style="border-right:solid 1px #e9e9e9;text-align:center;">Convenio</td>
				                                <td style="border-right:solid 1px #e9e9e9;text-align:center;">Exento de retención</td>
				                            </tr>
				                            <tr class="FA">
				                                <td style="padding-left:3px;">Kilometraje</td>
				                                <td id="cldKMCO" style="text-align:center;" runat="server">0,00</td>
				                                <td id="cldKMEX" style="text-align:center;border-right:0px;" runat="server"></td>
				                            </tr>
				                            <tr class="FB">
				                                <td style="padding-left:3px;">Dieta completa</td>
				                                <td id="cldDCCO" style="text-align:center;" runat="server">0,00</td>
				                                <td id="cldDCEX" style="text-align:center;border-right:0px;" runat="server">0,00</td>
				                            </tr>
				                            <tr class="FA">
				                                <td style="padding-left:3px;">Media dieta</td>
				                                <td id="cldMDCO" style="text-align:center;" runat="server">0,00</td>
				                                <td id="cldMDEX" style="text-align:center;border-right:0px;" runat="server">0,00</td>
				                            </tr>
				                            <tr class="FB">
				                                <td style="padding-left:3px;">Dieta especial</td>
				                                <td id="cldDECO" style="text-align:center;" runat="server">0,00</td>
				                                <td id="cldDEEX" style="text-align:center;border-right:0px;" runat="server">0,00</td>
				                            </tr>
				                            <tr class="FA">
				                                <td style="padding-left:3px;">Dieta alojamiento</td>
				                                <td id="cldDACO" style="text-align:center;" runat="server">0,00</td>
				                                <td id="cldDAEX"  runat="server" style="border-right:0px; text-align:center;">0,00</td>
				                            </tr>
				                            <tr class="TBLFIN">
				                                <td colspan="3" style="border-right:0px;padding-left:3px;">&nbsp;</td>
				                            </tr>
				                        </table>
                                        <!-- Fin del contenido propio de la página -->
                                    </td>
                                    <td style="background-image: url('../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="background-image: url('../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/2.gif'); height:6px;"></td>
                                    <td style="background-image: url('../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                                </tr>
                            </table>
				        </td>
				        <td></td>
				    </tr>
				</table>
                </eo:PageView>
           </eo:multipage>
        </td>
    </tr>
</table>

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
<asp:TextBox ID="hdnToolTipKmEstan" runat="server" style="visibility:hidden" TextMode="MultiLine" Text="" />
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
			    case "eliminar": 
			    {
                    bEnviar = false;
                    eliminar();
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
	    /*else{
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

