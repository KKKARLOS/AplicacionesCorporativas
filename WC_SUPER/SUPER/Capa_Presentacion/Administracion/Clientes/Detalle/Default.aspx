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
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de Clientes</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="Functions/ddfiguras.js" type="text/Javascript"></script>
 	<script src="Functions/nodos.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<style type="text/css">  
    #tsPestanas table { table-layout:auto; }
</style>
<script type="text/javascript">

    var strServer = "<% =Session["strServer"].ToString() %>";
    var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
    var bCambios = false;
    var bLectura = false;
    var bSalir = false;
    //var sOrigen = "<%= Request.QueryString["origen"] %>";
    //document.onselectstart = function () { return false; }; // prevent IE from trying to drag an image 

</script>    
<TABLE id="tabla" style="margin-left:15px;width:980px">
	<TR>
		<TD>
		    <br />
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
							MultiPageID="mpContenido" 
							ClientSideOnLoad="CrearPestanas" 
							ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Figuras" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Invitados" ToolTip="" Width="100"></eo:TabItem>
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
			    <eo:PageView CssClass="PageView" runat="server">
				    <!-- Pestaña 1 General--><br />
				    <table class="texto" width="940px">
                            <colgroup>
                                <col style="width:940px;" />
                            </colgroup>
						<tr>
							<td>
								<fieldset style="width:920px;height:50px;margin-left:15px;">
								    <legend>Cliente</legend>
									<table class="texto" style="width:900px;margin-top:10px;" cellpadding="5px" >
										<colgroup>
											<col style="width:240px;" />
											<col style="width:560px;" />
										</colgroup>
										<tr>
											<td><label style="width:90px;">Código Externo</label>
											    <asp:TextBox ID="txtCodigoExterno" style="width:70px;" Text="" readonly="true" SkinID="Numero" runat="server" />
										    </td>
											<td><label style="width:80px;">Denominación</label>
											    <asp:TextBox ID="txtDenominacion" runat="server" style="width:500px;" MaxLength="50" Text="" readonly="true"  />
									        </td>
										</tr>
									</table>
								</fieldset>									
							</td>
						</tr>
						<tr>
							<td>						
								<fieldset style="width:920px;height:70px;margin-left:15px;margin-top:10px">
								    <legend>Datos Gestión</legend>
									<table class="texto" width="940px;margin-top:15px;" cellpadding="5px" >
										<colgroup>
											<col style="width:470px;" />
											<col style="width:470px;" />
										</colgroup>
						                    <tr>
						                        <td>
						                            <label id="lblPaisGes" style="width:50px;">País</label>
                                                    <asp:DropDownList ID="cboPaisGes" runat="server" width="150px" onchange="javascript:aG(0);obtenerProvinciasGtonPais(this.value);"></asp:DropDownList>
						                        </td>
						                        <td>
						                            <label id="lblProvGes" style="width:50px;">Provincia</label>
							                        <asp:DropDownList id="cboProvGes" runat="server" Width="150px" onchange="javascript:aG(0);obtenerAmbitoZona();"></asp:DropDownList>	
						                        </td>
						                    </tr>												
											<tr style="vertical-align:middle;">
												<td><label id="lblAmbito" style="width:50px;">Ámbito</label>
												     <asp:TextBox ID="txtAmbito" runat="server" style="width:146px;" MaxLength="50" Text="" readonly="true"  />
										        </td>
												<td><label id="lblZona" style="width:50px;">Zona</label>
												    <asp:TextBox ID="txtZona" runat="server" style="width:146px;" MaxLength="50" Text="" readonly="true"  /></td>
											</tr>  
									</table>
								</fieldset>								
							</td>
						</tr>
						<tr>
							<td>						
								<fieldset style="width:920px;height:40px;margin-left:15px;margin-top:10px">
								    <legend>Datos Fiscales</legend>
									<table class="texto" width="940px;margin-top:15px;" cellpadding="5px" >
										<colgroup>
											<col style="width:470px;" />
											<col style="width:470px;" />
										</colgroup>
						                    <tr>
						                        <td>
						                            <label id="lblPaisFis" style="width:50px;">País</label>
                                                    <asp:DropDownList ID="cboPaisFis" runat="server" width="150px" onchange="javascript:aG(0);obtenerProvinciasFisPais(this.value);"></asp:DropDownList>
						                        </td>
						                        <td>
						                            <label id="lblProvFis" style="width:50px;">Provincia</label>
							                        <asp:DropDownList id="cboProvFis" runat="server" Width="150px" onchange="javascript:aG(0);"></asp:DropDownList>	
						                        </td>
						                    </tr>												
									</table>
								</fieldset>								
							</td>
						</tr>						
						<tr>
							<td>
								<fieldset style="width:920px;height:160px;margin-left:15px;margin-top:10px">
								    <legend>Otros Datos</legend>							
										<table class="texto" style="width:900px;margin-top:10px;" cellpadding="5px" >
											<colgroup>
												<col style="width:100px;" />
												<col style="width:800px;" />
										</colgroup>
											<tr>
												<td><label id="lblResponsable" class="enlace" style="cursor:pointer" onclick="getResponsable()">Responsable</label></td>
												<td><asp:TextBox ID="txtDesResponsable" style="width:500px;" Text="" readonly="true" runat="server" />
												&nbsp;&nbsp;<asp:TextBox ID="hdnIDResponsable" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
												</td>
											</tr>
											<tr style="vertical-align:middle;">
												<td>Sector</td>
												<td><asp:DropDownList ID="cboSector" runat="server" width="290px" onchange="javascript:aG(0);obtenerSegmentosSector(this.value);"></asp:DropDownList>
											</tr>    
											<tr style="vertical-align:middle;">
												<td>Segmento</td>
												<td><asp:DropDownList ID="cboSegmento" runat="server" width="290px" onchange="javascript:aG(0);" ></asp:DropDownList>												
											</tr>   

											<tr style="vertical-align:middle;">
												<td>Excluido Alertas</td>
												<td><asp:CheckBox ID="chkAlertas" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();aG(0);" checked="false" /></td>
											</tr>  
											<tr style="vertical-align:middle;">
												<td title="Cualificación de proyectos obligatorios">Cualificación CVT</td>
												<td><asp:CheckBox ID="chkCualiCVT" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();aG(0);" checked="false" /></td>
											</tr> 
										</table>
							</fieldset>								
							</td>
						</tr>
						</table>
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">
                    <!-- Pestaña 2 Figuras-->
                    <br />
			        <TABLE id="dragDropContainer" style="width:940px; margin-left:15px;">
			            <colgroup>
			            <col style="width:440px" /><col style="width:50px" /><col style="width:75px" /><col style="width:375px" /></colgroup>
					    <TR>
						    <TD>
						        <br />
							    <table style="WIDTH: 400px; margin-top:10px;">
							    <colgroup>
							        <col style="width:120px" /><col style="width:120px" /><col style="width:120px" /><col style="width:40px;" />
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
									    <td style="text-align:right;"><img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1')" /></td>
								    </tr>
							    </table>
						    </TD>
						    <TD>&nbsp;</TD>		
						    <TD style="vertical-align:middle;">
						        <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();"><img src="../../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>
		                    </TD>
		                    <TD style="text-align:right; vertical-align:bottom;">
		                        <table style="width:345px;">
		                            <colgroup><col style="width:280px"/><col style="width:65px"/></colgroup>
		                            <tr>
		                                <td align="center">		                        
		                                    <FIELDSET class="fld" style="width:180px; height:34px; text-align:center">
								                <LEGEND class="Tooltip" title="Pinchar y arrastrar">Selector de figuras</LEGEND>
             				                        <div id="listOfItems" style="height:25px;" onselectstart="return false;">
                                                        <ul id="allItems"  style="width:180px;">
                                                            <li id="D" value="1" onmouseover="mcur(this)" ondragstart="return false;"><img src="../../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" /> Delegado</li>
                                                            <li id="I" value="2" onmouseover="mcur(this)" ondragstart="return false;"><img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" /> Invitado</li>
                                                        </ul>
                                                    </div>
                                            </FIELDSET>
                                        </td>
                                        <td style="vertical-align:bottom;">
                                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" />
                                            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />
                                        </td>
                                      </tr>
                                  </table>
						    </TD>										
					    </TR>
					    <TR>
						    <TD>
							    <TABLE id="tblTituloFiguras1" style="height:17px;width:400px">
								    <TR class="TBLINI">
									    <TD>&nbsp;Profesionales&nbsp;
									        <IMG id="imgLupaFigurasA1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1')"
				                                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				                            <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1',event)"
				                                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                        </TD>
								    </TR>
							    </TABLE>
							    <DIV id="divFiguras1" style="overflow:auto; overflow-x:hidden; WIDTH: 416px; HEIGHT: 285px;" onscroll="scrollTabla()">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif);width:400px">
                                       <TABLE id='tblFiguras1' style='WIDTH: 400px; CURSOR: url(../../../../images/imgManoAzul2Move.cur),pointer'>
                                        <colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>
                                       </TABLE>         
                                    </div>
							    </DIV>
							    <TABLE style="height:17px; width:400px">
								    <TR class="TBLFIN">
									    <TD>&nbsp;</TD>
								    </TR>
                                    <tr>
                                        <td class="texto"><br />
                                            <img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                                            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                                        </td>
                                    </tr>  								
							    </TABLE>
						    </TD>
						    <TD>
							    <asp:Image id="imgPapeleraFiguras" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" caso="3" onmouseover="setTarget(this);" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras'"></asp:Image>
						    </TD>
						    <TD colspan="2">						    
							    <TABLE id="tblTituloFiguras2" style="height:17px;width:420px">
							        <colgroup><col style="width:20px"/><col style="width:295px"/><col style="width:105px"/></colgroup>
								    <TR class="TBLINI">
								        <TD width="20px" align="center"></TD>
									    <TD width="295px"><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2" border="0">
							                <MAP name="imgFiguras2">
							                    <AREA onclick="ot('tblFiguras2', 2, 0, '')" shape="RECT" coords="0,0,6,5">
							                    <AREA onclick="ot('tblFiguras2', 2, 1, '')" shape="RECT" coords="0,6,6,11">
						                    </MAP>&nbsp;Profesionales&nbsp;
						                    <IMG id="imgLupaFigurasA2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2')"
				                                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				                            <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2',event)"
				                                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                        </TD>
								        <TD width="105px">Figuras</TD>
								    </TR>
							    </TABLE>
							    <div id="mainContainer">
							        <div id="divFiguras2" style="OVERFLOW: auto; WIDTH: 436px; height: 285px;" target="true" caso="1" onmouseover="setTarget(this);" >
                                        <div style="background-image:url('<%=Session["strServer"]%>Images/imgFT22.gif');width: 420px;height:auto;">
                                            <table id='tblFiguras2' class='MM' style='width:420px;' mantenimiento='1'>
                                            <colgroup><col style='width:10px;' /><col style='width: 20px;' /><col style='width: 280px;' /><col style='width: 110px;' /></colgroup>                                                                                
                                            </table>
                                        </div>
							        </div>
							    </div>
							    <TABLE id="tblResultado2" style="height:17px; width:420px">
								    <TR class="TBLFIN">
									    <TD>&nbsp;</TD>
								    </tr>
								    <TR>
									    <TD><br />&nbsp;</TD>
								    </tr>
							    </TABLE>
							    <br />						
						    </TD>
					    </TR>   
				    </TABLE>                                
	            </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">
                    <!-- Pestaña 3 Nodos de los invitados-->
                    <br />
                    <table style="width:860px; margin-left:55px;">
                        <tr>
                            <td style="width:440px; vertical-align:top;">
							    <table style="width:400px; height:17px">
								    <TR class="TBLINI">
									    <TD>&nbsp;Profesionales&nbsp;
				                        </TD>
								    </TR>
							    </table>
							    <DIV id="divInvitados" style="overflow:auto; width:416px; height:320px; text-align:left;" onscroll="scrollTablaInvitados()">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif);width:400px">                                    
                                    </div>
							    </DIV>
							    <table style="width:400px;">
								    <TR class="TBLFIN" style="height:17px;" >
									    <TD>&nbsp;</TD>
								    </TR>
                                    <tr>
                                        <td><br />
                                            <img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                                            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                                        </td>
                                    </tr>  								
							    </table>
                            </td>
                            <td style="width:420px; vertical-align:top;">
							    <table style="width:400px;height:17px">
								    <TR class="TBLINI">
									    <td>&nbsp;<%=sNodo%>&nbsp;
				                        </td>
								    </TR>
							    </table>
							    <div id="divNodos" style="overflow:auto; width:416px; height:320px; text-align:left;">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif);width: 400px;">
                                    </div>
							    </div>
								<table style="width:400px;">
								    <TR class="TBLFIN" style="height:17px;" >
									    <td>&nbsp;</td>
								    </TR>	
						            <TR>
									    <td><br />					    
							        <center>
							            <table style="width:250px;">
							            <tr>
								            <td width="45%">
									            <button id="btnNuevo" type="button" onclick="nuevoNodo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										             onmouseover="se(this, 25);mostrarCursor(this);">
										            <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
									            </button>	
								            </td>
								            <td width="10%"></td>
								            <td width="45%">
									            <button id="btnEliminar" type="button" onclick="EliminarNodo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										             onmouseover="se(this, 25);mostrarCursor(this);">
										            <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
									            </button>	
								            </td>
							            </tr>
							            </table>							        
							        </center>											    
									    </td>
								    </TR>							
							    </table>	
                            </td>
                        </tr>
                    </table>
	            </eo:PageView>
           </eo:MultiPage>
        </TD>
    </TR>
</TABLE>
<center>
    <table id="tblBotones" style="margin-top:15px; width:360px;">
        <tr>
	        <td>
			    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" disabled="disabled" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			    </button>	
	        </td>
	        <td>
			    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled="disabled" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
	        </td>						
	        <td>
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnOrigen" value="" runat="server" />
<input type="hidden" id="hdnProvGton" value="" runat="server" />  
<input type="hidden" id="hdnProvFis" value="" runat="server" />  
<input type="hidden" id="hdnSegmento" value="" runat="server" />  


<asp:textbox id="hdnID" runat="server" style="visibility:hidden"></asp:textbox> 
<div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy; left:400px; top:30px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br>
        - Un profesional no puede ser simultáneamente:<br><br>
        1.- Delegado e Invitado.<br>
</div>
<ul id="dragContent" class="texto"></ul>
<div id="dragDropIndicator"><img src="../../../../images/imgSeparador.gif"></div>
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
