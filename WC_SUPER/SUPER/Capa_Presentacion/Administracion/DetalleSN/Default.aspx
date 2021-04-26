<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
   <title> ::: SUPER ::: - Detalle de estructura organizativa</title>
   <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="Functions/ddfiguras.js" type="text/Javascript"></script>
 	<script src="Functions/ddfigurasV.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
<style type="text/css">
#tblFiguras2 tr { height: 22px; }
#tblFiguras2 td { padding: 0px 0px 0px 0px; }
#tsPestanas table { table-layout:auto; }
</style> 	
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
    var sSN4 = "<%= Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) %>";
    var sSN3 = "<%= Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) %>";
    var sSN2 = "<%= Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) %>";
    //var sFiguraActiva = "R";
    var nNivel = <%=nNivel%>;
    var bCambios = false;
    var bLectura = false;
    var bSalir = false;
    var sOrigen = "<%= Utilidades.decodpar(Request.QueryString["origen"]) %>";
</script>    
<center>
<table style="width:980px; padding:10px; margin-top:10px; text-align:left">
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
						            <eo:TabItem Text-Html="F.V.P" ToolTip="Figuras virtuales de proyecto" Width="100"></eo:TabItem>
						            <eo:TabItem Text-Html="Alertas" Width="100"></eo:TabItem>
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
		    <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="450px">
		        <!-- Pestaña 1 General--><br />
		        <eo:PageView CssClass="PageView" runat="server" align="center">
                    <fieldset style="width: 620px; margin-top:10px; margin-left:10px;">
                        <legend>Superestructura</legend>  
                        <table style="width:610px; margin-top:15px" cellpadding="5px">
                        <colgroup>
                            <col style="width:100px;" />
                            <col style="width:510px;" />
                        </colgroup>
                        <tr id="fSN4" style="visibility:hidden">
                            <td><asp:Label ID="lblSN4" runat="server" SkinID="enlace" onclick="getItemEstructura(1)" Text="" /></td>
                            <td><asp:TextBox ID="txtDesSN4" style="width:490px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
                        </tr>
                        <tr id="fSN3" style="visibility:hidden">
                            <td><asp:Label ID="lblSN3" runat="server" SkinID="enlace" onclick="getItemEstructura(2)" Text="" /></td>
                            <td><asp:TextBox ID="txtDesSN3" style="width:490px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
                        </tr>
                        <tr id="fSN2" style="visibility:hidden">
                            <td><asp:Label ID="lblSN2" runat="server" SkinID="enlace" onclick="getItemEstructura(3)" Text="" /></td>
                            <td><asp:TextBox ID="txtDesSN2" style="width:490px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
                        </tr>
                        </table>
                     </fieldset> 
                    <table style="width:940px; margin-left:10px; margin-top:20px;" cellpadding="4px">
                        <colgroup>
                            <col style="width:100px;" />
                            <col style="width:840px;" />
                        </colgroup>
                        <tr>
                            <td>Id</td>
                            <td><asp:TextBox ID="txtID" style="width:50px;" Text="" readonly="true" SkinID="Numero" runat="server" /></td>
                        </tr>
                        <tr>
                        </tr>
                        <tr style="vertical-align:middle;">
                            <td>Denominación&nbsp;&nbsp;<img id="imgNivel" border="0" src="../../../Images/imgSeparador.gif" /></td>
                            <td><asp:TextBox ID="txtDenominacion" runat="server" style="width:500px;" MaxLength="50" Text="" onKeyUp="aG(0);" /></td>
                        </tr>
                        <tr>
	                        <td><label id="lblResponsable" class="enlace" style="cursor:pointer;height:17px" onclick="getResponsable()">Responsable</label></td>
                            <td><asp:TextBox ID="txtDesResponsable" style="width:500px;" Text="" readonly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Activo</td>
                            <td>
                                <asp:CheckBox ID="chkActivo" runat="server" Text="" style="cursor:pointer" onclick="aG(0);" checked="true" />
                                <label title="Visible desde la aplicación ¿Quién es quién?" style="margin-left:50px; vertical-align:top; margin-top:5px;">Visible QEQ</label>
                                <asp:CheckBox ID="chkQEQ" runat="server" Text="" style="cursor:pointer; margin-left:5px;" onclick="aG(0);" checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>Orden</td>
                            <td><asp:TextBox ID="txtOrden" style="width:40px;" Text="0" SkinID="Numero" onfocus="fn(this, 5, 0);" runat="server" onchange="aG(0);" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                            <fieldset style="width:320px; padding:7px; margin-top:10px;">
		                        <legend>Cualificador de proyecto</legend>  
		                        <table style="width: 300px; margin-top:7px;" cellpadding="3px">
                                <colgroup>
                                    <col style="width:75px;" />
                                    <col style="width:140px;" />
                                    <col style="width:85px;" />
                                </colgroup>
                                <tr>
                                    <td>Denominación</td>
                                    <td><asp:TextBox ID="txtCualificador" runat="server" style="width:120px;" MaxLength="15" Text="" onKeyUp="aG(0);" /></td>
                                    <td><asp:CheckBox ID="chkCualifObl"   runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="aG(0);" />&nbsp;Obligatorio</td>
                                </tr>
                                </table>
                             </fieldset> 
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:TextBox ID="hdnIDResponsable" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
                    <br />
                    <asp:TextBox ID="hdnIDSN2" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
                    <br />
                    <asp:TextBox ID="hdnIDSN3" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
                    <br />
                    <asp:TextBox ID="hdnIDSN4" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
                    <br />
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">
                <!-- Pestaña 2 Figuras-->
		        <table id="dragDropContainer" class="texto" style="width:940px; margin-left:15px;">
		            <colgroup>
                    <col style="width:420px" /><col style="width:75px" /><col style="width:445px" />
		            </colgroup>
				    <tr>
					    <td style="vertical-align:bottom;">
						    <table style="width:400px; visibility:visible">
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
								    <td style="text-align:right;">
								        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1')" />
								        <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; padding-left:3px" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1')" />
								    </td>
							    </tr>
						    </table>
					    </td>
					    <td>
					        <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();"><img src="../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>
	                    </td>
	                    <td>
	                        <table style="width:420px; text-align:left;">
	                        <colgroup><col style="width:280px;"/><col style="width:62px;"/></colgroup>
	                        <tr>
	                            <td>
	                                <fieldset class="fld" style="width:265px; height:50px; margin-left:60px;">
						            <legend class="Tooltip" title="Pinchar y arrastrar">Selector de figuras</legend>
         				            <div id="listOfItems" style="height:50px;">
                                        <ul id="allItems" style="width:265px;">
                                            <li id="D" value="1" onmouseover="mcur(this)" ><img src="../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" ondragstart="return false;" /> Delegado</li>
                                            <!--<li id="C" value="2" onmouseover="mcur(this)" ><img src="../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" ondragstart="return false;" /> Colaborador</li>-->
                                            <li id="I" value="3" onmouseover="mcur(this)" ><img src="../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> Invitado</li>
                                            <li id="G" value="4" onmouseover="mcur(this)" ><img src="../../../Images/imgGestor.gif" onmouseover="mcur(this)" title="Gestor" ondragstart="return false;" /> Gestor</li>
                                            <li id="S" value="5" onmouseover="mcur(this)" ><img src="../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" ondragstart="return false;" /> Asistente</li>
                                        </ul>
                                    </div>
                                    </fieldset>
	                            </td>
	                            <td style="vertical-align:bottom; text-align:right;">
                                    <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" />
                                    <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; padding-left:3px" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />  									        								                        
	                            </td>
	                        </tr>
	                        </table>
					    </td>									
				    </tr>
				    <tr>
					    <td>
						    <table id="tblTituloFiguras1" style="height:17px;width:400px">
							    <tr class="TBLINI">
								    <td>&nbsp;Profesionales&nbsp;
								        <img id="imgLupaFigurasA1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1')"
			                                height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
			                            <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1',event)"
			                                height="11" src="../../../Images/imgLupa.gif" width="20px" tipolupa="1">
			                        </td>
							    </tr>
						    </table>
						    <div id="divFiguras1" style="overflow: auto; width: 416px; height: 308px;" onscroll="scrollTabla()">
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
					    <td style="vertical-align:middle;">
						    <asp:Image id="imgPapeleraFiguras" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras'"></asp:Image>
					    </td>
					    <td style="vertical-align:top;">
						    <table id="tblTituloFiguras2" style="height:17px; width:420px;">
						        <colgroup><col style="width:20px"/><col style="width:300px"/><col style="width:100px"/></colgroup>
							    <tr class="TBLINI">
							        <td style="text-align:center;"></td>
								    <td>
								        <img style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" usemap="#imgFiguras2" border="0">
						                <map name="imgFiguras2">
						                    <area onclick="ot('tblFiguras2', 2, 0, '')" shape="rect" coords="0,0,6,5">
						                    <area onclick="ot('tblFiguras2', 2, 1, '')" shape="rect" coords="0,6,6,11">
					                    </map>&nbsp;Profesionales&nbsp;seleccionados
					                    <img id="imgLupaFigurasA2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2')"
			                                height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
			                            <img style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2',event)"
			                                height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
			                        </td>
							        <td>Figuras</td>
							    </tr>
						    </table>
						    <div id="mainContainer">
						        <div id="divFiguras2" style="overflow: auto; width: 436px; height:308px;" target="true" onmouseover="setTarget(this)" caso="1">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px; height:auto">
						                <table id="tblFiguras2" class="texto MM" style="width:420px">
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
						    <table id="tblResultado2" style="height:17px;width:420px">
							    <tr class="TBLFIN">
								    <td>&nbsp;</td>
							    </tr>
						    </table>							
					    </td>
				    </tr>   
			    </table>                                
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">
			    <!-- Pestaña 3 Figuras virtuales de proyecto-->
                <table id="dragDropContainerV" style="margin-left:15px; width:940px">
                    <colgroup>
                    <col style="width:420px" /><col style="width:75px" /><col style="width:370px" /><col style="width:75px" />
                    </colgroup>
		            <tr>
			            <td style="vertical-align:bottom;">
				            <table style="width:400px; visibility:visible">
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
						            <td style="text-align:right;">
						                <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1V')" />
						                <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:3px" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1V')" />
						            </td>
					            </tr>
				            </table>
			            </td>
					    <td style="vertical-align:middle; text-align:center;">
                            <div id="divBoxeoV" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidadesV();"><img src="../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>						    
	                    </td>				        
	                    <td style="padding-bottom:3px;">
	                        <fieldset class="fld" style="width:305px; height:50px; margin-left:50px;">
						    <legend class="Tooltip" title="Pinchar y arrastrar" unselectable="on">Selector de figuras</legend>
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
                        <td style="vertical-align:bottom; text-align:right; padding-right:25px; padding-bottom:2px;">
                            <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2V')" />
                            <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:3px; " title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2V')" />									        																			
					    </td>								
		            </tr>
		            <tr>
			            <td style="vertical-align:top;">
				            <table id="tblTituloFiguras1V" style="height:17px;width:400px" >
					            <tr class="TBLINI">
						            <td>&nbsp;Profesionales&nbsp;
						                <img id="imgLupaFigurasV1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1V',1,'divFiguras1V','imgLupaFigurasV1')"
	                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
	                                    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1V',1,'divFiguras1V','imgLupaFigurasV1',event)"
	                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
	                                </td>
					            </tr>
				            </table>
				            <div id="divFiguras1V" style="OVERFLOW: auto; width: 416px; height: 308px;" align="left" onscroll="scrollTablaV()">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:400px">
                                </div>
				            </div>
						    <table style="height:30px;width:400px" >
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
				            <asp:Image id="imgPapeleraFigurasV" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras virtuales de proyecto'"></asp:Image>
			            </td>
			            <td colspan="2" style="vertical-align:top;">
				            <table id="tblTituloFiguras2V" style="width: 420px; height: 17px;">
				                <colgroup><col style="width:20px"/><col style="width:300px"/><col style="width:100px"/></colgroup>
					            <tr class="TBLINI">
					                <td align="center"></td>
						            <td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2V" border="0">
				                        <MAP name="imgFiguras2V">
				                            <AREA onclick="ot('tblFiguras2V', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				                            <AREA onclick="ot('tblFiguras2V', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			                            </MAP>&nbsp;Profesionales&nbsp;
			                            <IMG id="imgLupaFigurasA2V" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2V',2,'divFiguras2V','imgLupaFigurasA2V')"
	                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
	                                    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2V',2,'divFiguras2V','imgLupaFigurasA2V',event)"
	                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
	                                </td>
					                <td>FVP</td>
					            </tr>
				            </table>
						    <div id="mainContainerV">
						        <div id="divFiguras2V" style="overflow: auto; width: 436px; height: 308px;" align="left" target="true" onmouseover="setTarget(this)" caso="1">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px;height:auto">
						                <table id="tblFiguras2V" class="texto MANO"  width="420px">							    
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
						    <table id="tblResultado2V" style="width: 420px; height: 30px;">
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
			<!-- Pestaña 4 Alertas-->
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
                        <div id="divAlertas" style="overflow:auto; width:946px; height:340px;" runat="server">
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
			<center>
            <table id="tblBotonesAlertas" style="margin-top:10px; width:350px;">
            <tr>
                <td>
		            <button id="btnTrasladar" type="button" onclick="setOpcionAlertas(1);" class="btnH25W110" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25);mostrarCursor(this);">
			            <img src="../../../images/imgTrasAlertas01.png" /><span title="Traslada la configuración de las alertas del item actual, a los elementos del siguiente nivel de la estructura">&nbsp;&nbsp;Trasladar</span>
		            </button>	
                </td>						
                <td>
		            <button id="btnTrasladarPlus" type="button" onclick="setOpcionAlertas(2);" class="btnH25W120" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25);mostrarCursor(this);">
			            <img src="../../../images/imgTrasAlertas02.png" /><span title="Traslada la configuración de las alertas del item actual, a los elementos de todos los niveles inferiores de la estructura">&nbsp;&nbsp;Trasladar +</span>
		            </button>	
                </td>
            </tr>
            </table>
            </center>		
			</eo:PageView>            
           </eo:MultiPage>
        </td>
    </tr>
</table>
<table id="tblBotones" style="margin-top:15px; width:80%">
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
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnDenominacion" value="" />
<div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:400px;
         top:30px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br>
        - Un profesional no puede ser simultáneamente:<br><br>
        1.- Delegado y Colaborador.<br>
        2.- Delegado e Invitado.<br>
        3.- Colaborador e Invitado.<br>
</div>
<div id="divIncompatibilidadesV" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:400px;
         top:270px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidadesV()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br>
        - Un profesional no puede ser simultáneamente:<br><br>
        1.- Delegado y Colaborador.<br>
        2.- Delegado e Invitado.<br>
        3.- Delegado y RTPE.<br>
        4.- Colaborador e Invitado.<br>
        5.- Jefe y RTPE.<br>
        6.- Colaborador y RTPE.<br>
</div>
<ul id="dragContent" class="texto"></ul>
<ul id="dragContentV" class="texto"></ul>
<div id="dragDropIndicator"><img src="../../../images/imgSeparador.gif"/></div>
<div id="dragDropIndicatorV"><img src="../../../images/imgSeparador.gif"/></div>
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
