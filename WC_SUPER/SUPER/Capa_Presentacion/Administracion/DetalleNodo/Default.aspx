<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo2" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?20180326" type="text/Javascript"></script>
 	<script src="Functions/ddfiguras.js" type="text/Javascript"></script>
 	<script src="Functions/ddfigurasV.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
        .tdcol{border-right: solid 1px #569BBD; padding-right:2px;}
    </style>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
	    #tsPestanasGenParam  table { table-layout:auto; }
    </style>
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var sSN1 = "<%= Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) %>";
        var sSN2 = "<%= Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) %>";
        var sSN3 = "<%= Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) %>";
        var sSN4 = "<%= Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) %>";
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        var sOrigen = "<%= Utilidades.decodpar(Request.QueryString["origen"]) %>";
        var nAnoMesAnteriorAlActual = <%=DateTime.Now.AddMonths(-1).Year * 100 + DateTime.Now.AddMonths(-1).Month %>;
    </script>    
<center>
<table id="tabla" style="width:980px; text-align:left; margin-top:10px; margin-left:6px;">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="970px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Figuras" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="C. Coste" Width="100" ToolTip="Centros de Coste relacionados"></eo:TabItem>
							<eo:TabItem Text-Html="F.V.P" Width="100" ToolTip="Figuras virtuales de proyecto"></eo:TabItem>
							<eo:TabItem Text-Html="Alertas" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Réplicas" Width="100" ToolTip="Definición de áreas de negocio por defecto para réplica"></eo:TabItem>
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="970px" Height="490px">
				<eo:PageView ID="PageView1" CssClass="PageView" runat="server" align="center" Height="490px" >
					<eo2:TabStrip runat="server" id="tsPestanasGenParam" ControlSkinID="None" Width="950px" 
						MultiPageID="mpContenido2" 
						ClientSideOnLoad="CrearPestanasGenParam" 
						ClientSideOnItemClick="getPestana">
						<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							<Items>
									<eo2:TabItem Text-Html="General 1" Width="100"></eo2:TabItem>
									<eo2:TabItem Text-Html="General 2" Width="100"></eo2:TabItem>
							</Items>
						</TopGroup>
						<LookItems>
							<eo2:TabItem ItemID="_Default" 
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
							</eo2:TabItem>
						</LookItems>
					</eo2:TabStrip>	
					<eo2:MultiPage runat="server" id="mpContenido2" CssClass="FMP" Width="950px" Height="455px">
						<eo2:PageView ID="PageView2" CssClass="PageView" runat="server" align="center">
				        <!-- Pestaña 1 General 1-->
						<table class="texto" style="width:940px; text-align:left;" cellpadding="3px">
							<colgroup>
								<col style="width:100px;" />
								<col style="width:175px;" />
								<col style="width:335px;" />
								<col style="width:165px;" />
								<col style="width:165px;" />
							</colgroup>
							<tr>
								<td colspan="3" rowspan="6" style="width:610px;">
								<fieldset style="width:590px;">
									<legend>Superestructura</legend>  
									<table class="texto" style="width: 590px; margin-top:7px;" cellpadding="2px">
									<colgroup>
										<col style="width:90px;" />
										<col style="width:500px;" />
									</colgroup>
									<tr>
										<td><asp:Label ID="lblSN4" runat="server" SkinID="enlace" onclick="getItemEstructura(1)" Text="" /></td>
										<td><asp:TextBox ID="txtDesSN4" style="width:480px;" Text="" readonly="true" runat="server" />
									<asp:TextBox ID="hdnIDSN4" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
									</tr>
									<tr>
										<td><asp:Label ID="lblSN3" runat="server" SkinID="enlace" onclick="getItemEstructura(2)" Text="" /></td>
										<td><asp:TextBox ID="txtDesSN3" style="width:480px;" Text="" readonly="true" runat="server" />
											<asp:TextBox ID="hdnIDSN3" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
									</tr>
									<tr>
										<td><asp:Label ID="lblSN2" runat="server" SkinID="enlace" onclick="getItemEstructura(3)" Text="" /></td>
										<td><asp:TextBox ID="txtDesSN2" style="width:480px;" Text="" readonly="true" runat="server" />
											<asp:TextBox ID="hdnIDSN2" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
									</tr>
									<tr>
										<td><asp:Label ID="lblSN1" runat="server" SkinID="enlace" onclick="getItemEstructura(4)" Text="" /></td>
										<td><asp:TextBox ID="txtDesSN1" style="width:480px;" Text="" readonly="true" runat="server" />
											<asp:TextBox ID="hdnIDSN1" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
									</tr>
									</table>
								 </fieldset> 
								</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>
								    <asp:CheckBox ID="chkActivo" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Activo
								</td>
								<td title="Indica si los profesionales del nodo pueden estar en más de un Grupo Funcional de ese nodo">
									<asp:CheckBox ID="chkMasUnGF" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;+1GF
						        </td>
							</tr>
							<tr>
								<td title="Indica si los proyectos del nodo se tienen en cuenta en el proceso de réplicas">
									<asp:CheckBox ID="chkGR" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Dispara réplica</td>
								<td title="Indica el defecto para permitir a los proyectos generar réplicas con gestión">
									<asp:CheckBox ID="chkPGRCG" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" />&nbsp;PGRCG</td>
							</tr>
							<tr>
								<td title="Indica si el nodo es representativo para mostrar los centros de coste asociados al mismo en GASVI, o si por el contrario hay que mostrar los asociados al subnodo">
									<asp:CheckBox ID="chkRepresentativo" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Representativo</td>
								<td title="Indica si este nodo se tiene en cuenta en los avisos del control de informe de actividad a las figuras RIA del nodo">
									<asp:CheckBox ID="chkMailCIA" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Aviso RIA</td>
							</tr>
							<tr>
								<td title="Indica si el nodo está sujeto al cierre mensual estándar de IAP">
									<asp:CheckBox ID="chkCierreIAPest" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Cierre IAP estándar</td>
								<td title="Indica si el nodo está sujeto al cierre económico mensual estándar">
									<asp:CheckBox ID="chkCierreECOest" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Cierre ECO estándar</td>
							</tr>
							<tr>
								<td title="Indica si el nodo se encuentra seleccionado por defecto en la creación de proyectos improductivos genéricos">
									<asp:CheckBox ID="chkImprodGen" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;PIG estándar</td>
								<td title="En función del valor del interface Hermes, genera un proyecto (sin marcar) o dos (marcado)">
									<asp:CheckBox ID="chkDesglose" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Desglose Hermes
								</td>
							</tr>
							<tr>
								<td>Id</td>
								<td colspan="2"><asp:TextBox ID="txtID" style="width:50px; margin-right:52px;" Text="" readonly="true" SkinID="Numero" runat="server" />
								Organización de ventas SAP&nbsp;&nbsp;<asp:DropDownList ID="cboOrgVtas" runat="server" Width="230px" onchange="aG(0);">
									</asp:DropDownList> </td>
								<td title="">
									<asp:CheckBox ID="chkControlhuecos" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Control de huecos
								</td>
								<td title="Indica si se permite por defecto asignar profesionales desde PST, ajenos al proyecto económico.">
									<asp:CheckBox ID="chkPermitirPST" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Permitir PST
								</td>
							</tr>
							<tr style="vertical-align:middle;">
								<td><label style="vertical-align:super">Denominación</label>&nbsp;&nbsp;<img id="imgNivel" border="0" src="../../../Images/imgNodo.gif" /></td>
								<td colspan="2"><asp:TextBox ID="txtDenominacion" runat="server" style="width:300px;" MaxLength="50" Text="" onKeyUp="aG(0);" />
								<label class="texto" id="lblAbreviatura" title="Denominación corta" style="margin-left:10px">Abrev.</label> <asp:TextBox ID="txtAbreviatura" runat="server" style="width:128px;" MaxLength="15" Text="" onKeyUp="aG(0);" />
								</td>
							    <td title="Excluido de alertas">
									<asp:CheckBox ID="chkAlertas" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();aG(0);" checked="false" />&nbsp;Exc.Alertas								 
								</td>
								<td title="Cualificación de proyectos obligatorios para CURVIT">
									<asp:CheckBox ID="chkCualiCVT" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();aG(0);" Checked />&nbsp;Cualificación CVT
								</td>
							</tr>
							<tr>
								<td><label id="lblResponsable" class="enlace" style="cursor:pointer;height:17px" onclick="getResponsable()">Responsable</label></td>
								<td colspan="2"><asp:TextBox ID="txtDesResponsable" style="width:480px;" Text="" readonly="true" runat="server" />
									<asp:TextBox ID="hdnIDResponsable" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
								</td>
                                <td title="Visible desde la aplicación ¿Quién es quién?">
                                    <asp:CheckBox ID="chkQEQ" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();aG(0);" checked="true" />&nbsp;Visible QEQ
                                </td>
                                <td title="Impide la asignación de proyectos y profesionales">
                                    <asp:CheckBox ID="chkInstrumental" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();aG(0);" checked="false" />&nbsp;Instrumental
                                </td>
							</tr>
							<tr>
								<td><label id="lblEmpresa" class="enlace" style="cursor:pointer;height:17px" onclick="getEmpresa()">Empresa</label></td>
								<td colspan="2"><asp:TextBox ID="txtDesEmpresa" style="width:480px;" Text="" readonly="true" runat="server" />
									<asp:TextBox ID="hdnIDEmpresa" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
							</tr>
							<tr>
								<td>Modelo coste</td>
								<td><asp:DropDownList ID="cboModoCoste" runat="server" Width="100px" onchange="aG(0);">
										<asp:ListItem Value="J" Text="Jornadas" Selected></asp:ListItem>
										<asp:ListItem Value="H" Text="Horas"></asp:ListItem>
										<asp:ListItem Value="X" Text="Mixto"></asp:ListItem>
										</asp:DropDownList></td>
								<td rowspan="2" style="vertical-align:top;">
								<fieldset style="width:310px; padding: 7px;">
									<legend>Cualificador de proyecto</legend>  
									<table class="texto" style="width: 300px; margin-top:7px;" cellpadding="3px">
									<colgroup>
										<col style="width:75px;" />
										<col style="width:140px;" />
										<col style="width:85px;" />
									</colgroup>
									<tr>
										<td>Denominación</td>
										<td><asp:TextBox ID="txtCualificador" runat="server" style="width:120px;" MaxLength="15" Text="Cualificador Qn" onKeyUp="aG(0);" /></td>
										<td><asp:CheckBox ID="chkCualifObl" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" />&nbsp;Obligatorio</td>
									</tr>
									</table>
								 </fieldset> 
								</td>
								<td colspan="2" rowspan="2" style="vertical-align:top;">
								<fieldset style="width: 308px;">
									<legend>Gastos financieros</legend>  
									<table class="texto" style="width:305px; margin-top:7px;" border="0">
									<colgroup>
										<col style="width:70px;" />
										<col style="width:125px;" />
										<col style="width:110px;" />
									</colgroup>
									<tr>
										<td title="Indica si los proyectos del nodo se tienen en cuenta en el proceso de cálculo de gastos financieros" style="vertical-align:top;">
											<asp:CheckBox ID="chkCalcular" runat="server" Text="" style="cursor:pointer;" onclick="aG(0);" Checked="true" />
                                            <label style="vertical-align:top; margin-top:3px;">Calcular</label>
									    </td>
										<td>Últ. cálculo&nbsp;<asp:TextBox ID="txtUltRecGF" style="width:55px;" Text="" readonly="true" runat="server" /></td>
										<td>Interés&nbsp;<asp:TextBox ID="txtInteresGF" style="width:40px;" SkinID="Numero" Text="0,0000" onfocus="fn(this,3,4)" runat="server" onkeyup="aG(0);" /> %</td>
									</tr>
									</table>
								 </fieldset> 
								 </td>
							</tr>
							<tr>
								<td>Modelo tarifa</td>
								<td><asp:DropDownList ID="cboModoTarifa" runat="server" Width="100px" onchange="aG(0);">
										<asp:ListItem Value="J" Text="Jornadas" Selected="True"></asp:ListItem>
										<asp:ListItem Value="H" Text="Horas"></asp:ListItem>
										<asp:ListItem Value="X" Text="Mixto"></asp:ListItem>
										</asp:DropDownList></td>
							</tr>
							<tr>
								<td>Interface Hermes</td>
								<td><asp:DropDownList ID="cboHermes" runat="server" Width="100px" onchange="cambioInterface();">
										<asp:ListItem Value="N" Text="No interviene" Selected="True"></asp:ListItem>
										<asp:ListItem Value="S" Text="SUPER"></asp:ListItem>
										<asp:ListItem Value="P" Text="SUPER & SAP"></asp:ListItem>
										</asp:DropDownList></td>
								<td rowspan="2" style="vertical-align:top;">
								<fieldset style="width:308px; padding:7px;">
									<legend title="Parametrización aplicable a la creación manual de proyectos">Tipologías de proyecto parametrizables</legend>  
									<table class=texto style="width: 290px;" border=0 style="margin-top:7px; table-layout:fixed;" cellpadding=3 cellspacing=0>
									<tr>
										<td style="text-align:center;">
										<asp:CheckBox ID="chkTipolInterna" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />Interna
										<asp:CheckBox ID="chkTipolEspecial" runat="server" Text="" style="cursor:pointer; vertical-align:middle; margin-left:30px;" onclick="aG(0);" Checked />Especial
										<asp:CheckBox ID="chkTipolProdSC" runat="server" Text="" style="cursor:pointer; vertical-align:middle; margin-left:30px;" onclick="aG(0);" Checked />Productiva SC
										</td>
									</tr>
									</table>
								 </fieldset> 
								</td>
								<td colspan="2" rowspan="3" style="vertical-align:top;">
								<fieldset style="width: 308px; padding: 7px;">
									<legend>Último cierre</legend>  
									<table class=texto style="width: 260px; margin-top:7px" cellpadding="5px">
									<tr>
										<td>Económico&nbsp;
											<img id="imgAM" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer; vertical-align:bottom;" border=0 />
											<asp:TextBox ID="txtCierreEco" style="width:90px; text-align:center;" Text="" readonly="true" runat="server" />
											<img id="imgSM" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer; vertical-align:bottom;" border=0 />
										</td>
									</tr>
									<tr>
										<td>IAP&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<img id="imgAMiap" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMesIAP('A')" style="cursor: pointer; vertical-align:bottom;" border=0 />
											<asp:TextBox ID="txtCierreIAP" style="width:90px; text-align:center;" Text="" readonly="true" runat="server" />
											<img id="imgSMiap" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMesIAP('S')" style="cursor: pointer; vertical-align:bottom;" border=0 />
										</td>
									</tr>
									</table>
								 </fieldset> 
								 </td>
							</tr>
							<tr>
								<td>Margen de cesión</td>
								<td>
								    <asp:TextBox ID="txtMargenCesion" style="width:40px;" Text="0,00" SkinID="Numero" onfocus="fn(this, 5, 2);" runat="server" onchange="aG(0);" /> %
								</td>
							</tr>
							<tr>
								<td>Orden</td>
								<td><asp:TextBox ID="txtOrden" style="width:40px;" Text="0" SkinID="Numero" onfocus="fn(this, 5, 0);" runat="server" onchange="aG(0);" /></td>
								<td rowspan="3" style="vertical-align:top;">
								    <fieldset style="width: 308px; padding: 7px;">
									    <legend>Cuadre&nbsp;&nbsp;"Contratación - Producción"</legend>  
									    <table class="texto" style="width: 260px; margin-top:7px" cellpadding="5px">
									    <colgroup>
										    <col style="width:133px;" />
										    <col style="width:113px;" />
									    </colgroup>
									    <tr>
										    <td title="Indica si los proyectos del nodo se tienen en cuenta en el proceso de cuadre de contratación">
											    <asp:CheckBox ID="chkCuadre" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" Checked />&nbsp;Comprobar</td>
										    <td>Tolerancia (%)&nbsp;<asp:TextBox ID="txtTolerancia" style="width:30px;" onKeyUp="aG(0);" Text="" runat="server" SkinID="Numero" onfocus="fn(this, 3, 0);" /></td>
									    </tr>
									    </table>
								     </fieldset> 
								</td>
							</tr>
						    <tr>
								<td>Asignar perfiles</td>	
							    <td><asp:DropDownList ID="cboPerfiles" runat="server" style="width:160px; vertical-align:middle;" onchange="aG(0);" AppendDataBoundItems=True>
										<asp:ListItem Value="" Text="" Selected></asp:ListItem>
										</asp:DropDownList></td>
                            </tr>											
						</table>
                        </eo2:PageView>
                        
                        <eo2:PageView CssClass="PageView" runat="server">
                            <!-- Pestaña 2 General 2-->
                            <table class="texto" style="width:930px; text-align:left;" border="0">
                                <colgroup><col style="width:330px;" /><col style="width:600px;" /></colgroup>		
                                <tr>
                                    <td style="vertical-align:top;">                    
			                            <fieldset style="width: 300px;">
			                                <legend>Soporte administrativo&nbsp;&nbsp;<asp:CheckBox id="chkSoporte" runat="server" onclick="dTabla();" style="vertical-align:middle;"/></legend>                      
                                            <table style="WIDTH:260px; HEIGHT:17px; margin-top:8px; margin-left:20px;">
                                                <tr class="TBLINI">
                                                    <td style="padding-left:5px">Modelo de contratación</td>
                                                </tr>
                                            </table>                
                                            <div id="divSoporte" style="overflow: auto; width: 276px; height:144px; margin-left:20px;">                                
                                                <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT18.gif); width:260px;">                                                                      
                                                    <%=strHTMLSoporteAdminis%>
                                                </div>
                                            </div> 
                                            <table style="WIDTH: 260px; HEIGHT: 17px; margin-bottom:10px;margin-left:20px;">
                                                <tr class="TBLFIN">
                                                    <td></td>
                                                </tr>
                                            </table>  
                                        </fieldset>                                  
                                    </td>
                                    <td style="vertical-align:top; padding-top:10px;">
                                        <table class="texto" style="width:595px; text-align:left;" cellspacing="5px" cellpadding="5px" border="0">
                                            <tr>
                                                <td>
                                                    <label title="Moneda por defecto para los proyectos que se creen bajo este elemento">Moneda</label>
                                                    <asp:DropDownList ID="cboMoneda" runat="server" AppendDataBoundItems="true" onchange="aG(1);" style="margin-left:5px;"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label id="lblCualificador" class="enlace" style="cursor:pointer;height:17px; width:140px;" onclick="getCualificador()">Cualificador ventas</label>
                                                    <asp:TextBox ID="txtDesCualificador" style="width:400px;" Text="" readonly="true" runat="server" />
                                                    <asp:TextBox ID="hdnIDCualificador" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label id="lblOrgComer" class="enlace" style="cursor:pointer;height:17px; width:140px;" onclick="getOrgCom()">Organización comercial</label>
                                                    <asp:TextBox ID="txtDesOrgCom" style="width:400px;" Text="" readonly="true" runat="server" />
                                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="limpiarOrgCom();" style="cursor:pointer; vertical-align:middle;" />
                                                    <asp:TextBox ID="txtIdOrgCom" style="width:1px; visibility:hidden;" Text="" readonly="true" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>       
                        </eo2:PageView>
                   </eo2:MultiPage>
                </eo:PageView>
     
                <eo:PageView CssClass="PageView" Height="490px" runat="server">               
                <!-- Pestaña 2 Figuras-->
			    <table id="dragDropContainer" class="texto" style="width:940px; margin-left:15px;">
			        <colgroup><col style="width:420px" /><col style="width:70px" /><col style="width:95px" /><col style="width:355px" /></colgroup>
					<tr>
						<td style="vertical-align:bottom;">
							<table style="width:400px; visibility:visible;">
							    <colgroup><col style="width:120px"/><col style="width:120px"/><col style="width:120px"/><col style="width:40px"/></colgroup>
								<tr>
									<td>Apellido1</td>
									<td>Apellido2</td>
									<td>Nombre</td>
								    <td></td>
								</tr>
								<tr>
									<td><asp:TextBox id="txtApellido1" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
									<td><asp:TextBox id="txtApellido2" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
									<td><asp:TextBox id="txtNombre" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
									<td style="text-align:right;"><img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1')" /><img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; padding-left:5px" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1')" /></td>
								</tr>
							</table>
						</td>
						<td>&nbsp;</td>		
						<td>
						    <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();"><img src="../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>
		                </td>
		                <td>
		                    <table style="width:325px;">
		                    <colgroup><col style="width:280px;"/><col style="width:45px;"/></colgroup>
		                        <tr>
		                            <td>
		                                <fieldset class="fld" style="width:260px; height:50px;">
							            <legend class="Tooltip" title="Pinchar y arrastrar">Selector de figuras</legend>
             				                <div id="listOfItems" style="height:50px;">
                                                <ul id="allItems" style="width:255px;">
                                                    <li id="D" value="1" onmouseover="mcur(this)"><img src="../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" /> Delegado</li>
                                                    <li id="C" value="2" onmouseover="mcur(this)"><img src="../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" /> Colaborador</li>
                                                    <li id="I" value="3" onmouseover="mcur(this)"><img src="../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" /> Invitado</li>
                                                    <li id="G" value="4" onmouseover="mcur(this)"><img src="../../../Images/imgGestor.gif" onmouseover="mcur(this)" title="Gestor" /> Gestor</li>
                                                    <li id="S" value="5" onmouseover="mcur(this)"><img src="../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" /> Asistente</li>
                                                    <li id="P" value="6" onmouseover="mcur(this)"><img src="../../../Images/imgPerseguidor.gif" onmouseover="mcur(this)" title="Receptor de Informes de Actividad" /> RIA</li>
                                                </ul>
                                            </div>
            		                    </fieldset>
	                                </td>
	                                <td style="text-align:right; vertical-align:bottom;">
                                        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" /><img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:5px" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />  									        								                        
		                            </td>                                    
		                        </tr>
		                    </table>            		                    
						</td>									
					</tr>
					<tr>
						<td>
							<table id="tblTituloFiguras1" style="height:17px;width:400px" >
								<tr class="TBLINI">
									<td>&nbsp;Profesionales&nbsp;
									    <img id="imgLupaFigurasA1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1')"
				                            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				                        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1',event)"
				                            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                    </td>
								</tr>
							</table>
							<div id="divFiguras1" style="overflow: auto; width: 416px; height:352px;" onscroll="scrollTabla()">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:400px">
                                </div>
							</div>
							<table style="height:30px;width:400px">
								<tr class="TBLFIN">
									<td>&nbsp;</td>
								</tr>
                                <tr>
                                    <td class="texto"><br />
                                        <img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                                        <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                                    </td>
                                </tr>  								
							</table>	
						</td>
						<td style="vertical-align:middle; text-align:center;">
							<asp:Image id="imgPapeleraFiguras" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras'"></asp:Image>
						</td>
						<td colspan="2" style="vertical-align:top;">
							<table id="tblTituloFiguras2" style="height:17px;width:420px">
							    <colgroup><col style="width:20px"/><col style="width:300px"/><col style="width:100px"/></colgroup>
								<tr class="TBLINI">
								    <td align="center"></td>
									<td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2" border="0">
							            <MAP name="imgFiguras2">
							                <AREA onclick="ot('tblFiguras2', 2, 0, '')" shape="RECT" coords="0,0,6,5">
							                <AREA onclick="ot('tblFiguras2', 2, 1, '')" shape="RECT" coords="0,6,6,11">
						                </MAP>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2')"
				                    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2',event)"
				                    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                    </td>
								    <td>Figuras</td>
								</tr>
							</table>
							<div id="mainContainer">
							<div id="divFiguras2" style="OVERFLOW: auto; width: 436px; height:352px;" align="left" target="true" onmouseover="setTarget(this);" caso="1">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px; height:auto">
							        <table id="tblFiguras2" class="texto MANO" width="420px">							    
							            <colgroup>
							            <col style="width:20px;" />
							            <col style="width:20px;" />
							            <col style="width:280px;" />
							            <col style="width:100px;" />
							            </colgroup>
							            <tbody id='tbodyFiguras2'>
							            </tbody>
							        </table>
							    </div>
							</div>
							</div>
							<table id="tblResultado2" style="height:30px;width:420px">
								<tr class="TBLFIN">
									<td>&nbsp;</td>
								</tr>
								<tr>
								    <td class="texto"><br />&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>   
				</table>                                
                </eo:PageView>
                
                <eo:PageView CssClass="PageView" Height="490px" runat="server">               
				<!-- Pestaña 3 C. Coste-->
				<br />
				<center>
                <table style="width:820px; text-align:left; margin-left:80px;">
                    <tr>
                        <td>
                            <table id="tblTitulo" style="width: 800px; height: 17px;">
                                <colgroup>
                                <col style="width:50px;" />
                                <col style="width:350px;" />
                                <col style="width:360px;" />
                                <col style="width:40px;" />
                                </colgroup>
                                <tr class="TBLINI">
                                    <td style="padding-left:5px;">Cód.</td>
                                    <td>Denominación</td>
                                    <td><%= Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %></td>
                                    <td>GASVI</td>
                                </tr>
                            </table>
                            <div id="divCatalogo" style="overflow: auto; width: 816px; height:420px;" runat="server">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:800px">
                                </div>
                            </div>
                            <table id="tblResultado" style="width: 800px; height: 17px;">
                                <tr class="TBLFIN">
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </center>
                <br />
                </eo:PageView>
                
                <eo:PageView CssClass="PageView" Height="490px" runat="server">               
				<!-- Pestaña 4 Figuras virtuales de proyecto-->
	            <table id="dragDropContainerV" style="margin-left:15px; width:940px; text-align:left;">
	                <colgroup><col style="width:420px" /><col style="width:80px" /><col style="width:370px" /><col style="width:75px" /></colgroup>
			        <tr>
				        <td style="vertical-align:bottom;">
					        <table style="width: 400px; margin-top:10px;visibility:visible">
					            <colgroup><col style="width:120px"/><col style="width:120px"/><col style="width:120px"/><col style="width:40px"/></colgroup>
						        <tr>
							        <td>Apellido1</td>
							        <td>Apellido2</td>
							        <td>Nombre</td>
							        <td></td>
						        </tr>
						        <tr>
							        <td><asp:TextBox ID="txtApellido1V" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFiguraV();event.keyCode=0;}" MaxLength="50" /></td>
							        <td><asp:TextBox ID="txtApellido2V" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFiguraV();event.keyCode=0;}" MaxLength="50" /></td>
							        <td><asp:TextBox ID="txtNombreV" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFiguraV();event.keyCode=0;}" MaxLength="50" /></td>
							        <td style="text-align:right;"><img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1V')" /><img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:5px" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1V')" /></td>
						        </tr>
					        </table>
				        </td>
						<td>
                            <div id="divBoxeoV" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidadesV();"><img src="../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>						    
		                </td>				        
		                <td style="padding-bottom:3px;">
		                    <fieldset class="fld" style="width:305px; height:50px; margin-left:50px;">
							<legend class="Tooltip" title="Pinchar y arrastrar" unselectable=on>Selector de figuras</legend>
             				<div id="listOfItemsV" style="height:50px;">
                                <ul id="allItemsV"  style="width:300px;">
                                    <li id="DV" value="1" onmouseover="mcur(this)"><img src="../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" ondragstart="return false;" /> Delegado</li>
                                    <li id="CV" value="2" onmouseover="mcur(this)"><img src="../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" ondragstart="return false;" /> Colaborador</li>
                                    <li id="JV" value="3" onmouseover="mcur(this)"><img src="../../../Images/imgJefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Jefe" /> Jefe</li>
                                    <li id="MV" value="4" onmouseover="mcur(this)"><img src="../../../Images/imgSubjefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Responsable técnico de proyecto económico" /> RTPE</li>
                                    <li id="BV" value="5" onmouseover="mcur(this)"><img src="../../../Images/imgBitacorico.gif" onmouseover="mcur(this)" title="Bitacórico" ondragstart="return false;" /> Bitacórico</li>
                                    <li id="SV" value="6" onmouseover="mcur(this)"><img src="../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" ondragstart="return false;" /> Asistente</li>
                                    <li id="IV" value="7" onmouseover="mcur(this)"><img src="../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> Invitado</li>
                                </ul>
                            </div>
                            </fieldset>	
                        </td>
                        <td style="vertical-align:bottom; text-align:right; padding-right:20px;">
                            <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2V')" />
                            <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:2px; margin-right:5px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2V')" />  									        																			
						</td>								
			        </tr>
			        <tr>
				        <td style="vertical-align:top;">
					        <table id="tblTituloFiguras1V" style="height:17px;width:400px">
						        <tr class="TBLINI">
							        <td>&nbsp;Profesionales&nbsp;
							            <IMG id="imgLupaFigurasV1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1V',1,'divFiguras1V','imgLupaFigurasV1')"
		                                    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
		                                <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1V',1,'divFiguras1V','imgLupaFigurasV1',event)"
		                                    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
		                            </td>
						        </tr>
					        </table>
					        <div id="divFiguras1V" style="overflow:auto; overflow-x:hidden; width: 416px; height:352px;" align="left" onscroll="scrollTablaProfFigurasV()">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:400px">
                                </div>
					        </div>
							<table style="height:30px;width:400px">
								<tr class="TBLFIN">
									<td>&nbsp;</td>
								</tr>
                                <tr>
                                    <td class="texto"><br />
                                        <img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                                        <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                                    </td>
                                </tr>  								
							</table>	
				        </td>
				        <td>
					        <asp:Image id="imgPapeleraFigurasV" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras virtuales de proyecto'"></asp:Image>
				        </td>
				        <td colspan="2" style="vertical-align:top;">
					        <table id="tblTituloFiguras2V" style="height:17px; width:420px;">
						        <colgroup><col style="width:20px;"/><col style="width:300px;"/><col style="width:100px;"/></colgroup>
						        <tr class="TBLINI">
						            <td></td>
							        <td><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2V" border="0">
					                    <map name="imgFiguras2V">
					                        <area onclick="ot('tblFiguras2V', 2, 0, '')" shape="RECT" coords="0,0,6,5">
					                        <area onclick="ot('tblFiguras2V', 2, 1, '')" shape="RECT" coords="0,6,6,11">
				                        </map>&nbsp;Profesionales&nbsp;
				                        <img id="imgLupaFigurasA2V" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2V',2,'divFiguras2V','imgLupaFigurasA2V')"
		                                    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
		                                <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2V',2,'divFiguras2V','imgLupaFigurasA2V',event)"
		                                    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
		                            </td>
						            <td>FVP</td>
						        </tr>
					        </table>
							<div id="mainContainerV">
							<div id="divFiguras2V" style="overflow:auto; overflow-x:hidden; width:436px; height:352px;" align="left" target="true" onmouseover="setTarget(this);" caso="1">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px; height:auto">
							        <table id="tblFiguras2V" class="texto MANO" width="420px">							    
							            <colgroup>
							            <col style="width:20px;" />
							            <col style="width:20px;" />
							            <col style="width:280px;" />
							            <col style="width:100px;" />
							            </colgroup>
							            <tbody id='tbodyFiguras2V'>
							            </tbody>
							    </table>
							    </div>
							</div>
							</div>
							<table id="tblResultado2V" style="height:17px; width:420px;">
								<tr class="TBLFIN">
									<td>&nbsp;</td>
								</tr>
	    					</table>							
				        </td>
			        </tr>   
		        </table>                                
				</eo:PageView>  
				
                <eo:PageView CssClass="PageView" Height="490px" runat="server">               
				<!-- Pestaña 5 Alertas-->
				<table id="tblGralAlerta" style="margin-left:10px; width:940px; text-align:left;">
				    <tr>
				        <td>
				            <table id="tblCabAlerta" style="width:930px; height:34px;">
				                <colgroup>
				                    <col style="width:30px"/>
				                    <col style="width:210px"/>
				                    <col style="width:165px"/><col style="width:65px"/>
				                    <col style="width:165px"/><col style="width:65px"/>
				                    <col style="width:165px"/><col style="width:65px"/>
				                </colgroup>
	                            <tr class="texto" align="center">
	                                <td></td>	
                                    <td></td>
                                    <td colspan="2" class="colTabla">Parámetro 1</td>
                                    <td colspan="2" class="colTabla">Parámetro 2</td>
                                    <td colspan="2" class="colTabla1">Parámetro 3</td>
	                            </tr>
				                <tr class="TBLINI">
				                    <td align="center">Id</td>	
				                    <td style="padding-left:3px;">Asunto</td>
				                    <td>&nbsp;Literal</td><td style="text-align:right;">Valor&nbsp;</td>
				                    <td>&nbsp;Literal</td><td style="text-align:right;">Valor&nbsp;</td>
				                    <td>&nbsp;Literal</td><td style="text-align:right;">Valor&nbsp;</td>
				                </tr>
				            </table>
                            <div id="divAlertas" style="overflow:auto; width:946px; height:420px;" runat="server">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:930px">
                                </div>
                            </div>
                            <table id="tblPieAlerta" style="width:930px; height:17px;">
                                <tr class="TBLFIN">
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
				        </td>
				    </tr>
				</table> 
				</eo:PageView>
                <eo:PageView CssClass="PageView" Height="490px" runat="server">
                <!-- Pestaña 6 Réplicas-->
                    <div style="width:510px; margin-top:40px; margin-left:190px;">
                        <table style="width:500px; height:17px; ">
                            <colgroup><col style='width:400px;' /><col style='width: 100px;' /></colgroup>
                            <tr>
                                <td colspan="2" style="text-align:right;">
                                    <label style="font-size:12px;color:Navy;">
                                        Seleccione el <%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%> destino por defecto para réplicas.
                                    </label>
                                </td>
                            </tr>
                            <tr class="TBLINI">
                                <td style="width:400px; padding-left:5px;"><%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%></td>
                                <td style="width:100px; text-align:center;">Destino réplica</td>
                            </tr>
                        </table>                
                        <div id="divSubNodoManiobra" style="overflow: auto; width: 516px; height:380px;">                                
                            <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif); width:500px;">                                                                      
                                <%=strHTMLSubNodomaniobra3%>
                            </div>
                        </div> 
                        <table style="width:500px; height:17px; margin-bottom:10px;">
                            <tr class="TBLFIN">
                                <td></td>
                            </tr>
                        </table>  
                    </div>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<table id="tblBotones" style="margin-top:15px; width:80%;">
    <tr>
	    <td>
			<button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgNuevo.gif" /><span title="Nuevo">&nbsp;&nbsp;Nuevo</span>
			</button>	
	    </td>						
	    <td>
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" disabled hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td>
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			</button>	
	    </td>	
	    <td>
			<button id="btnDuplicar" type="button" onclick="duplicar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgDuplicar.gif" /><span title="duplicar">Duplicar</span>
			</button>	
	    </td>								
		<td id="cldAuditoria" runat="server">
			<button id="btnAuditoria" type="button" onclick="getAuditoriaAux();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgAuditoria.gif" /><span title="Auditoría">&nbsp;Auditoría</span>
			</button>	
		</td>								
	    <td>
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>
</center>
<asp:TextBox ID="hdnCierreIAP" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnCierreECO" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnRepresentativo" runat="server" style="visibility:hidden" Text="1"></asp:TextBox>
<asp:TextBox ID="hdnMensajeRepresentativo" runat="server" style="visibility:hidden" Text=""></asp:TextBox>

<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnDenominacion" value="" />
<input type="hidden" id="hdnInstrumentalIni" value="" runat="server"/>

<div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy; left:400px; top:30px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br />
        - Un profesional no puede ser simultáneamente:<br /><br />
        1.- Delegado y Colaborador.<br />
        2.- Delegado e Invitado.<br />
        3.- Colaborador e Invitado.<br />
</div>
<div id="divIncompatibilidadesV" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy; left:400px; top:270px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidadesV()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br />
        - Un profesional no puede ser simultáneamente:<br><br />
        1.- Delegado y Colaborador.<br />
        2.- Delegado e Invitado.<br />
        3.- Delegado y RTPE.<br />
        4.- Colaborador e Invitado.<br />
        5.- Jefe y RTPE.<br />
        6.- Colaborador y RTPE.<br />
</div>
<ul id="dragContent" class="texto"></ul>
<ul id="dragContentV" class="texto"></ul>
<div id="dragDropIndicator"><img src="../../../images/imgSeparador.gif"></div>
<div id="dragDropIndicatorV"><img src="../../../images/imgSeparador.gif"></div>
<div class="clsDragWindow" id="DW" noWrap></div>
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
