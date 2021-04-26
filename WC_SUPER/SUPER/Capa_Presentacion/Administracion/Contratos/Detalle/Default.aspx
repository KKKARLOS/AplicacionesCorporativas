<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Detalle de Contratos</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css"/>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/ddfiguras.css" type="text/css"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />     
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>      
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="Functions/ddfiguras.js" type="text/Javascript"></script>
 	<script src="Functions/ddfigurasV.js" type="text/Javascript"></script> 	
	<script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script> 	
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
        #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        //var sOrigen = "<%= Request.QueryString["origen"] %>";
        //<!--<iewc:Tab Text="Extensiones" ToolTip=""></iewc:Tab>-->
    </script>  
<br />      
<center>
<table id="tabla" style="padding:10px;width:980px;text-align:left">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
							MultiPageID="mpContenido" 
							ClientSideOnLoad="CrearPestanas" 
							ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Figuras" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Extensiones" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="F.V.P" Width="100" ToolTip="Figuras virtuales de proyecto"></eo:TabItem>							
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="440px">
			<eo:PageView CssClass="PageView" runat="server" align="center">
				<!-- Pestaña 1 General--><br />
                    <table style="width:940px; text-align:left;" cellpadding="5px">
                        <colgroup>
                            <col style="width:120px;" />
                            <col style="width:820px;" />
                        </colgroup>
                        <tr>
	                        <td>Código</td>
	                        <td><asp:TextBox ID="txtCodigo" style="width:50px;" Text="" readonly="true" SkinID="Numero" runat="server" /></td>
                        </tr>
                        <tr style="vertical-align:middle;">
	                        <td>Denominación&nbsp;&nbsp;</td>
	                        <td><asp:TextBox ID="txtDenominacion" runat="server" style="width:500px;"  Text="" readonly="true"  /></td>
                        </tr>
                        <tr>
	                        <td>
                                <label id="lblCR" class="enlace" style="cursor:pointer;height:17px" onclick="getCR()">
                                    <%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>                                
                                </label>
	                        </td>
                            <td><asp:TextBox ID="txtNodo" style="width:500px;" Text="" readonly="true" runat="server" />                        
	                        </td>
                        </tr>                    
                        <tr>
	                        <td>
                                <label id="lblCliente" class="enlace" style="cursor:pointer;height:17px" onclick="getCliente()">
                                    Cliente
                                </label>
	                        </td>
                            <td><asp:TextBox ID="txtCliente" style="width:500px;" Text="" readonly="true" runat="server" />                        
	                        </td>
                        </tr>
                        <tr>
	                        <td>
                                <label id="lblResp" class="enlace" style="cursor:pointer;height:17px" onclick="getResp()">
                                    Responsable
                                </label>
	                        </td>
                            <td><asp:TextBox ID="txtResponsable" style="width:500px;" Text="" readonly="true" runat="server" />                        
	                        </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblGestorProdu" class="enlace" style="cursor:pointer;height:17px" onclick="getGestorProdu()">Gestor producción</label>
                            </td>
                            <td><asp:TextBox ID="txtGestorProdu" style="width:500px;" Text="" readonly="true" runat="server" />                        
	                        </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblComer" class="enlace" style="cursor:pointer;height:17px" onclick="getComer()">
                                    Comercial
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComercial" style="width:500px;" Text="" readonly="true" runat="server" />
                            </td>
                        </tr>
                        <tr>
	                        <td title="Permite al responsable, delegados e invitados ver proyectos replicados del proyecto contratante asociado al contrato">Visión sobre réplicas</td>
                            <td><asp:CheckBox ID="chkVisionReplicas" runat="server" style="cursor:pointer;" onclick="aG(0);" />                      
	                        </td>
                        </tr>      
                        <tr>
                            <td title="Organización comercial">Org. Comercial</td>
                            <td>
                                <asp:TextBox ID="txtCodOrgComer" style="width:50px;" Text="" readonly="true" SkinID="Numero" runat="server" />
                                <asp:TextBox ID="txtDenOrgComer" style="width:450px;" Text="" readonly="true" runat="server" />
                                <label style="margin-left:20px;" title="Código externo">Cód. Ext.</label>
                                <asp:TextBox ID="txtCodExtOrgComer" style="width:150px;" Text="" readonly="true" class="texto" runat="server" />
                            </td>
                        </tr>              
                        <tr>
                            <td title="Nueva línea de oferta">NLO</td>
                            <td>
                                <asp:TextBox ID="txtCodNLO" style="width:50px;" Text="" readonly="true" SkinID="Numero" runat="server" />
                                <asp:TextBox ID="txtDenNLO" style="width:450px;" Text="" readonly="true" runat="server" />
                                <label style="margin-left:20px;" title="Código externo">Cód. Ext.</label>
                                <asp:TextBox ID="txtCodExtNLO" style="width:150px;" Text="" readonly="true" SkinID="Numero" runat="server" />
                            </td>
                        </tr>              
                    </table>
	        </eo:PageView>
            <eo:PageView CssClass="PageView" runat="server">
                <!-- Pestaña 2 Figuras-->
                    <br />
			        <table id="dragDropContainer" width="940px">
			            <colgroup>
			            <col style="width:440px" /><col style="width:50px" /><col style="width:75px" /><col style="width:375px" />
			            </colgroup>
					    <tr>
						    <td style="vertical-align:bottom; margin-bottom:5px;">
							    <table style="width:400px;">
							    <colgroup>
							    <col style="width:120px" />
							    <col style="width:120px" />
							    <col style="width:120px" />
							    <col style="width:40px;" />
							    </colgroup>
								    <tr>
									    <td>Apellido1</td>
									    <td>Apellido2</td>
									    <td>Nombre</td>
								        <td></td>
								    </tr>
								    <tr>
									    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
									    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
									    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
									    <td style="text-align:right;" >
									        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1')" />&nbsp;
									        <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1')" />
									    </td>
								    </tr>
							    </table>
						    </td>
						    <td>&nbsp;</td>		
						    <td style="vertical-align:middle;">
						        <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();"><img src="../../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>
		                    </td>
		                    <td align="center" vertical-align:bottom;">
		                        <table style="width:345px;">
		                        <colgroup><col style="width:280px"/><col style="width:65px"/></colgroup>
		                        <tr>
		                            <td style="text-align:center;">		                        
		                                <fieldset class="fld" style="width:180px; height:34px; text-align:center">
								            <legend class="Tooltip" title="Pinchar y arrastrar">Selector de figuras</legend>
             				                    <div id="listOfItems" style="height:25px;">
                                                    <ul id="allItems"  style="width:180px;">
                                                        <li id="D" value="1" onmouseover="mcur(this)"><img src="../../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" /> Delegado</li>
                                                        <li id="I" value="2" onmouseover="mcur(this)"><img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" /> Invitado</li>
                                                    </ul>
                                                </div>
                                        </fieldset>
                                    </td>
                                    <td style="vertical-align:bottom;">
                                        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer;vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" />&nbsp;
                                        <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer;vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />
                                    </td>
                                  </tr>
                                  </table>
						    </td>									
					    </tr>
					    <tr>
						    <td style="vertical-align:top;">
							    <table id="tblTituloFiguras1" style="height:17px;width:400px">
							        <colgroup><col style="width:400px"/></colgroup>
								    <tr class="TBLINI">
									    <td>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1')"
				                        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1',event)"
				                        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                        </td>
								    </tr>
							    </table>
							    <div id="divFiguras1" style="overflow:auto; overflow-x:hidden; width:416px; height:300px;" align="left" onscroll="scrollTabla()">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:400px">
                                    </div>
							    </div>
							    <table style="height:30px; width:400px;">
								    <tr class="TBLFIN">
									    <td>&nbsp;</td>
								    </tr>
                                    <tr>
                                        <td class="texto"><br />
                                            <img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                                            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                                        </td>
                                    </tr>  								
							    </table>
						    </td>
						    <td>
							    <asp:Image id="imgPapeleraFiguras" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras'"></asp:Image>
						    </td>
						    <td colspan="2">
							    <table id="tblTituloFiguras2" style="height:17; width:420px;">
							        <colgroup><col style="width:20px"/><col style="width:300px"/><col style="width:100px"/></colgroup>
								    <tr class="TBLINI">
								        <td style="text-align:center;"></td>
									    <td><img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2" border="0">
							                <map name="imgFiguras2">
							                    <area onclick="ot('tblFiguras2', 2, 0, '')" shape="RECT" coords="0,0,6,5">
							                    <area onclick="ot('tblFiguras2', 2, 1, '')" shape="RECT" coords="0,6,6,11">
						                    </map>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2')"
				                        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2',event)"
				                        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                        </td>
								        <td>Figuras</td>
								    </tr>
							    </table>
							    <div id="mainContainer">
							        <div id="divFiguras2" style="overflow:auto; overflow-x:hidden; width:436px; height:300px;" align="left" target="true" onmouseover="setTarget(this);" caso="1">
                                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:420px; height:auto;">
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
							    <table id="tblResultado2" style="height:30px; width:420px;">
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
            <eo:PageView CssClass="PageView" runat="server">
                <!-- Pestaña 3 Extensiones-->
                <table width="890px" style="margin-left:30px;" cellpadding="5">
                    <tr>
                        <td>
                            <table id="tblTitulo" style="width:870px; height:17px;">
                                <colgroup>
                                    <col style="width:440px;" />
                                    <col style="width:55px" />
                                    <col style="width:55px" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                </colgroup>
	                            <tr class="texto" align="center" style="height:20px">
                                    <td colspan="3">&nbsp;</td>
                                    <td colspan="2" class="colTabla" style="text-align:center">Producto</td>
                                    <td colspan="2" class="colTabla1" style="text-align:center">Servicio</td>
	                            </tr>
                                <tr class="TBLINI">
                                    <td style="padding-left:10px;">Oportunidad / Extensión</td>
                                    <td align="left">Nº</td>
                                    <td style="text-align:center">Fecha</td>
                                    <td style="text-align:right">Importe</td>
                                    <td style="text-align:right">Margen</td>
                                    <td style="text-align:right">Importe</td>
                                    <td style="text-align:right">Margen</td>
                                </tr>
                            </table>
                            <div id="divExtensiones" style="overflow: auto; overflow-x:hidden; width: 886px; height:330px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:870px;height:auto;">
                                <%=strTablaHTML %>
                                </div>
                            </div>
                            <table style="width:870px; height:17px">
                                <tr class="TBLFIN">
                                    <td></td>
                                </tr>
                            </table>
                            <center>
		                        <table style="width:10%; margin-top:7px;">
		                            <tr>
									    <td>
										    <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
											     onmouseover="se(this, 25);mostrarCursor(this);">
											    <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
										    </button>	
			                            </td>
		                            </tr>
                                </table>   
                            </center>                              
                        </td>
                    </tr>
                </table>                
	         </eo:PageView> 
            <eo:PageView CssClass="PageView" runat="server">               
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
						        <td style="text-align:right;"><img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1V')" /><img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:5px" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1V')" /></td>
					        </tr>
				        </table>
			        </td>
					<td>
                        <div id="divBoxeoV" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidadesV();"><img src="../../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>						    
	                </td>				        
	                <td style="padding-bottom:3px;">
	                    <fieldset class="fld" style="width:305px; height:50px; margin-left:50px;">
						<legend class="Tooltip" title="Pinchar y arrastrar" unselectable=on>Selector de figuras</legend>
         				<div id="listOfItemsV" style="height:50px;">
                            <ul id="allItemsV"  style="width:300px;">
                                <li id="DV" value="1" onmouseover="mcur(this)"><img src="../../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" ondragstart="return false;" /> Delegado</li>
                                <li id="CV" value="2" onmouseover="mcur(this)"><img src="../../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" ondragstart="return false;" /> Colaborador</li>
                                <li id="JV" value="3" onmouseover="mcur(this)"><img src="../../../../Images/imgJefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Jefe" /> Jefe</li>
                                <li id="MV" value="4" onmouseover="mcur(this)"><img src="../../../../Images/imgSubjefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Responsable técnico de proyecto económico" /> RTPE</li>
                                <li id="BV" value="5" onmouseover="mcur(this)"><img src="../../../../Images/imgBitacorico.gif" onmouseover="mcur(this)" title="Bitacórico" ondragstart="return false;" /> Bitacórico</li>
                                <li id="SV" value="6" onmouseover="mcur(this)"><img src="../../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" ondragstart="return false;" /> Asistente</li>
                                <li id="IV" value="7" onmouseover="mcur(this)"><img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> Invitado</li>
                            </ul>
                        </div>
                        </fieldset>	
                    </td>
                    <td style="vertical-align:bottom; text-align:right; padding-right:20px;">
                        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2V')" />
                        <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:2px; margin-right:5px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2V')" />  									        																			
					</td>								
		        </tr>
		        <tr>
			        <td style="vertical-align:top;">
				        <table id="tblTituloFiguras1V" style="height:17px;width:400px">
					        <tr class="TBLINI">
						        <td>&nbsp;Profesionales&nbsp;
						            <IMG id="imgLupaFigurasV1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1V',1,'divFiguras1V','imgLupaFigurasV1')"
	                                    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
	                                <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1V',1,'divFiguras1V','imgLupaFigurasV1',event)"
	                                    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
	                            </td>
					        </tr>
				        </table>
				        <div id="divFiguras1V" style="overflow:auto; overflow-x:hidden; width: 416px; height:286px;" align="left" onscroll="scrollTablaProfFigurasV()">
                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:400px">
                            </div>
				        </div>
						<table style="height:30px;width:400px">
							<tr class="TBLFIN">
								<td>&nbsp;</td>
							</tr>
                            <tr>
                                <td class="texto"><br />
                                    <img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                                </td>
                            </tr>  								
						</table>	
			        </td>
			        <td>
				        <asp:Image id="imgPapeleraFigurasV" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras virtuales de proyecto'"></asp:Image>
			        </td>
			        <td colspan="2" style="vertical-align:top;">
				        <table id="tblTituloFiguras2V" style="height:17px; width:420px;">
					        <colgroup><col style="width:20px;"/><col style="width:300px;"/><col style="width:100px;"/></colgroup>
					        <tr class="TBLINI">
					            <td></td>
						        <td><img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2V" border="0">
				                    <map name="imgFiguras2V">
				                        <area onclick="ot('tblFiguras2V', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				                        <area onclick="ot('tblFiguras2V', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			                        </map>&nbsp;Profesionales&nbsp;
			                        <img id="imgLupaFigurasA2V" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2V',2,'divFiguras2V','imgLupaFigurasA2V')"
	                                    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
	                                <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2V',2,'divFiguras2V','imgLupaFigurasA2V',event)"
	                                    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
	                            </td>
					            <td>FVP</td>
					        </tr>
				        </table>
						<div id="mainContainerV">
						<div id="divFiguras2V" style="overflow:auto; overflow-x:hidden; width:436px; height:286px;" align="left" target="true" onmouseover="setTarget(this);" caso="1">
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
           </eo:MultiPage>
        </td>
    </tr>
</table>
<table id="tblBotones" style="margin-top:15px; width:50%;">
    <tr>			
	    <td>
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" disabled="disabled"
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td>
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" disabled="disabled"
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			</button>	
	    </td>		
	    <td id="cldAuditoria" runat="server">
			<button id="btnAuditoria" type="button" onclick="getAuditoriaAux();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgAuditoria.gif" /><span title="Auditoría">&nbsp;Auditoría</span>
			</button>	
	    </td>									
	    <td>
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>
</center>	
    <input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
    <asp:textbox id="hdnID" runat="server" style="visibility:hidden"></asp:textbox> 
    <asp:textbox id="hdnIDGestorProdu" runat="server" style="visibility:hidden"></asp:textbox> 
    <input type="hidden" id="hdnIdCR" value="" runat="server"/>
    <input type="hidden" id="hdnIdCli" value="" runat="server"/>
    <input type="hidden" id="hdnIdResp" value="" runat="server"/>
    <input type="hidden" id="hdnIdComer" value="" runat="server"/>
<div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:350px;
         top:250px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br>
        - Un profesional no puede ser simultáneamente:<br><br>
        1.- Delegado e Invitado.<br>
</div>
<div id="divIncompatibilidadesV" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy; left:350px; top:250px; 
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
<div id="dragDropIndicator"><img src="../../../../images/imgSeparador.gif"></div>
<div id="dragDropIndicatorV"><img src="../../../../images/imgSeparador.gif"></div>
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
