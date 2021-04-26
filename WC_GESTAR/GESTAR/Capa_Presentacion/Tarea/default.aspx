<%@ Page language="c#" Inherits="GESTAR.Capa_Presentacion.ASPX.DetalleTarea" EnableEventValidation="false" ValidateRequest="false" CodeFile="default.aspx.cs" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>
	<%=strTitulo%></title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<link href="../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" /> 
    <link rel="stylesheet" href="../../PopCalendar/CSS/Classic.css" type="text/css" />

    <script src="../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../JavaScript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../JavaScript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../JavaScript/documentos.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../JavaScript/modal.js" type="text/Javascript"></script>
</head>
	<body class="FondoBody" onload="init()">
    <style type="text/css">  
        #tsPestanas table { table-layout:auto; }
    </style>
    <ucproc:Procesando ID="Procesando" runat="server" />
		<form id="frmDatos" runat="server">
		<script type="text/javascript">
		    var strServer = "<%=Session["strServer"].ToString()%>";
	        var intSession = <%=Session.Timeout%>;
            var bNueva = <%=Request.QueryString["bNueva"] %>;
	</script>					
        <center>
        <table id="tabla" style="width:920px;text-align:left;margin-top:15px">
	        <tr>
		        <td>
			        <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
							        MultiPageID="mpContenido" 
							        ClientSideOnLoad="CrearPestanas" 
							        ClientSideOnItemClick="getPestana">
				        <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					        <Items>
							        <eo:TabItem Text-Html="General" ToolTip="" Width="100"></eo:TabItem>
							        <eo:TabItem Text-Html="Especialistas" ToolTip="" Width="120"></eo:TabItem>
							        <eo:TabItem Text-Html="Partes" ToolTip="" Width="100"></eo:TabItem>
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
				        <center>
	                    <table id="contenido" style="width:98%;text-align:left" >
		                    <tr>
		                        <td>
            			            <fieldset class="fld" style="width:84%;margin-left:65px">
			                            <legend class="Tooltip" title="Estructura">&nbsp;Estructura&nbsp;</legend>
			                            <table cellpadding="3px" width="100%">
				                            <tr>		
									            <td width="25%">&nbsp;&nbsp;&nbsp;Área&nbsp;</td>
									            <td width="75%">
									            <asp:TextBox onKeyUp="javascript:ActivarGrabar(0);" id="txtArea" runat="server" width="513px"
											            CssClass="textareatexto"></asp:TextBox>
									            </td>
				                            </tr>
				                            <tr>		
									            <td width="25%">&nbsp;&nbsp;&nbsp;Orden&nbsp;&nbsp;</td>
									            <td width="75%">
									            <asp:TextBox onKeyUp="javascript:ActivarGrabar(0);" id="txtDeficiencia" runat="server" width="513px"
											            CssClass="textareatexto"></asp:TextBox>
									            </td>
				                            </tr>
				                            <tr>		
									            <td width="25%">&nbsp;&nbsp;&nbsp;Tarea&nbsp;&nbsp;</td>
									            <td width="75%"><asp:TextBox id="txtIdTarea" runat="server" width="60px" align="right" CssClass="textareaNumero"></asp:TextBox>&nbsp;&nbsp;
									            <asp:TextBox onKeyUp="javascript:ActivarGrabar(0);" id="txtDenominacion" runat="server" width="440px"
											            CssClass="textareatexto"></asp:TextBox>
									            </td>
				                            </tr>				                                						                                				                                
			                            </table>
			                        </fieldset>
			                    </td>	
		                    </tr>
		                    <tr>
			                    <td><img style="height: 10px" alt="" title="" src="../../Images/imgSeparador.gif" align="left"/>
			                    <br /><br />
			                    </td>
		                    </tr>		
		                    <tr>
			                    <td>
									<table id="f1" width="85%;">
										<tr>
											<td valign="top" width="49%">									
												<fieldset class="fld" style="width:360px;margin-left:65px">
												<legend class="Tooltip" title="Previsión">&nbsp;Previsión&nbsp;</legend>																			
												<table width="96%" cellpadding="7" align="center">
													<tr>
														<td width="25%">Fecha inicio
														</td>
														<td width="75%">
                                                            <asp:textbox id="txtFechaInicioPrevista" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar(0)" runat="server" goma="1" lectura="0"></asp:textbox>														
														</td>
													</tr>
													<tr>
														<td width="25%">Fecha fin
														</td>
														<td width="75%">
                                                            <asp:textbox id="txtFechaFinPrevista" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar(0)" runat="server" goma="1" lectura="0"></asp:textbox>
														</td>
													</tr>
												</table>
												</fieldset>
											</td>
											<td width="2%"></td>
											<td valign="top" width="49%">
												<fieldset class="fld" style="width:360px;margin-left:65px">
												<legend class="Tooltip" title="Realización">&nbsp;Realización&nbsp;</legend>																			
												<table width="96%"  cellpadding="7">
													<tr>
														<td width="25%">Fecha inicio</td>
														<td width="75%">
                                                            <asp:textbox id="txtFechaInicioReal" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar(0)" runat="server" goma="1" lectura="0"></asp:textbox>														
														</td>
													</tr>
													<tr>
														<td width="25%">Fecha fin</td>
														<td width="75%">
                                                            <asp:textbox id="txtFechaFinReal" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar(0)" runat="server" goma="1" lectura="0"></asp:textbox>
														</td>
													</tr>
												</table>
                                                </fieldset>							
											</td>
										</tr>																						
								    </table>	
			                    </td>
		                    </tr>		
			                <tr>
			                <td align="center"><br />
			                    <table style="width:87%;text-align:left" cellPadding="7">
										<tr>
											<td with="100%">
												            &nbsp;&nbsp;Resultado&nbsp;&nbsp;
                                        <asp:dropdownlist id="cboRtado" runat="server" width="95px" CssClass="combo" onchange="javascript:ActivarGrabar(0);"
							                AutopostBack="false">
							                <asp:ListItem Value="1">Satisfactorio</asp:ListItem>
							                <asp:ListItem Value="2">No efectivo</asp:ListItem>
							                <asp:ListItem Value="3">Cancelado</asp:ListItem>
						                </asp:dropdownlist><br /><br /><br />
			                            &nbsp;&nbsp;Descripción&nbsp;<br>
						                <asp:textbox onKeyUp="javascript:ActivarGrabar(0);"
						                id="txtDescripcion" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="7">
						                </asp:textbox>									
						                </td>
						                </tr>
						        </table>
			                </td>	
			                </tr>	                                       									
	                    </table>	
                        </center>
                    </eo:PageView>
                    <eo:PageView CssClass="PageView" runat="server">
                            <!-- Pestaña 2 // Especialistas -->	
						    <table style="text-align:left" cellspacing="1" cellpadding="10px">
								<tr>
									<td colspan="4">
										<table style="width: 350px;visibility:visible">
											<tr>
												<td>&nbsp;&nbsp;Apellido1</td>
												<td>&nbsp;&nbsp;Apellido2</td>
												<td>&nbsp;&nbsp;Nombre</td>
											</tr>
											<tr>
												<td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px" CssClass="textareatexto" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" MaxLength="50" /></td>
												<td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" CssClass="textareatexto" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" MaxLength="50" /></td>
												<td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" CssClass="textareatexto" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" MaxLength="50" /></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td width="48%">
										<table id="tblTitulo" height="17" width="390px" border="0">
											<tr class="tituloColumnaTabla">
												<td width="90%"><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogo',0,'divCatalogo','imgLupa1',event)"
														height="11" src="../../Images/imgLupa.gif" width="20"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogo',0,'divCatalogo','imgLupa1')"
														height="11" src="../../Images/imgLupaMas.gif" width="20">&nbsp;&nbsp;Especialistas</td>
												<td width="10%"><img onmouseover="javascript:bMover=true;moverTablaUp()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
														height="8" src="../../Images/imgFleUp.gif" width="11"></td>
											</tr>
										</table>
										<div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 406px; height: 340px;">
                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">					                
					                            <%=strTablaHtmlCatalogo%>
					                        </div>
										</div>
										<table height="17" cellSpacing="0" cellPadding="0" width="390px" align="left" border="0">
											<tr class="textoResultadoTabla">
												<td colSpan="2"><img height="1" src="../../Images/imgSeparador.gif" width="90%" border="0">
													<img onmouseover="javascript:bMover=true;moverTablaDown()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
														height="8" src="../../Images/imgFleDown.gif" width="11"></td>
											</tr>
										</table>
									</td>
									<td width="4%">
										<asp:Image id="insertarProf" style="cursor: pointer" onclick="anadirSeleccionados()" runat="server" ImageUrl="../../Images/imgNextpg.gif" ToolTip="Añade el especialista seleccionado en el catálogo 'Especialistas' al catálogo 'Especialistas asignados'"></asp:Image><BR>
										<BR>
										&nbsp;<asp:Image id="quitar" style="cursor: pointer" onclick="quitarSeleccionados()" runat="server" ImageUrl="../../Images/botones/imgEliminar.gif" ToolTip="Elimina el especialista seleccionado del catálogo 'Especialistas asignados'"></asp:Image>
									</td>
									<td width="46%" align="left">
										<table id="tblTitulo2" height="17" cellSpacing="0" cellPadding="0" width="390px" border="0">
											<tr class="tituloColumnaTabla">
												<td width="90%"><img style="cursor: pointer" onclick="buscarDescripcion('tblSeleccionados',0,'divCatalogo2','imgLupa2',event)"
														height="11" src="../../Images/imgLupa.gif" width="20"> <img id="imgLupa2" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblSeleccionados',0,'divCatalogo2','imgLupa2')"
														height="11" src="../../Images/imgLupaMas.gif" width="20">&nbsp;&nbsp;Especialistas asignados</td>
												<td width="10%"><img onmouseover="javascript:bMover=true;moverTablaUp2()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
														height="8" src="../../Images/imgFleUp.gif" width="11"></td>
											</tr>
										</table>
										<div id="divCatalogo2" style="overflow-x: hidden; overflow: auto; width: 406px; height: 340px;">
										    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">			
										        <%=strTablaHtmlSeleccionados%>
										    </div>
										</div>
										<table id="tblResultado2" height="17" cellSpacing="0" cellPadding="0" width="390px" align="left" border="0">
											<tr class="textoResultadoTabla">
												<td colSpan="2"><img height="1" src="../../Images/imgSeparador.gif" width="90%" border="0">
													<img onmouseover="javascript:bMover=true;moverTablaDown2()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
														height="8" src="../../Images/imgFleDown.gif" width="11">
												</td>
											</tr>
										</table>
									</td>
									<td width="2%">
									</td>
								</tr>   
							</table>                            
                    </eo:PageView>
                    <eo:PageView CssClass="PageView" runat="server">
                            <!-- Pestaña 3 // Partes -->
                    <table class="texto" width="98%" cellSpacing=0 cellPadding=0 width="100%" align=center>
							<tr>
							    <td align="right">&nbsp;&nbsp;Avance&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:DropDownList id="cboAvance" runat="server" width="55px" AutopostBack="false" CssClass="combo" 
											onchange="javascript:ActivarGrabar(3);">
											<asp:ListItem Value="1">10 %</asp:ListItem>
											<asp:ListItem Value="2">20 %</asp:ListItem>
											<asp:ListItem Value="3">30 %</asp:ListItem>
											<asp:ListItem Value="4">40 %</asp:ListItem>
											<asp:ListItem Value="5">50 %</asp:ListItem>
											<asp:ListItem Value="6">60 %</asp:ListItem>
											<asp:ListItem Value="7">70 %</asp:ListItem>
											<asp:ListItem Value="8">80 %</asp:ListItem>
											<asp:ListItem Value="9">90 %</asp:ListItem>
											<asp:ListItem Value="10">100 %</asp:ListItem>
										</asp:DropDownList>
								</td>
							</tr>	
						    <tr>
			                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
			                    </td>
		                    </tr>	    										                    
						    <tr>
						    <td>
							    &nbsp;&nbsp;Causas&nbsp;<br />
									    <asp:textbox onKeyUp="javascript:ActivarGrabar(3);CorreoRespon();"
									    id="txtCausas" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="8"></asp:textbox>
						    </td>	
						    </tr>
						    <tr>
			                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
			                    </td>
		                    </tr>		                		
						    <tr>
						    <td>
                                &nbsp;&nbsp;Intervenciones&nbsp;<br />
									    <asp:textbox onKeyUp="javascript:ActivarGrabar(3);CorreoRespon();"
									    id="txtIntervenciones" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="8">	
									    </asp:textbox>
						    </td>	
						    </tr>
						    <tr>
			                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
			                    </td>
		                    </tr>		                								    	
						    <tr>
						    <td>
                                &nbsp;&nbsp;Consideraciones  / Comentarios&nbsp;<br />
									    <asp:textbox onKeyUp="javascript:ActivarGrabar(3);CorreoRespon();"
									    id="txtConsideraciones" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="8">
									    </asp:textbox>															    
						    </td>	
						    </tr>							
	                    </table>                        	
                    </eo:PageView>
                    </eo:MultiPage> 	
                </td>
              </tr>
            </table>	
            </center>
			<br />
			<center>
			    <table width="100%">
				    <tr id="Pie" style="visibility:visible">
					    <td width="20%">&nbsp;</td>
					    <td width="20%" align="center">
			                <button id="btnGrabar" type="button" onclick="grabar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../images/botones/imgGrabar.gif" /><span title="Graba la información">Grabar</span>
			                </button>	
					    </td>
					    <td width="20%" align="center">
			                <button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../images/botones/imgGrabarSalir.gif" /><span title="Graba la información y regresa a la pantalla">Grabar</span>
			                </button>				
					    </td>
					    <td width="20%" align="center">
			                <button id="btnSalir" type="button" onclick="salir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../images/botones/imgSalir.gif" /><span title="Regresa a la pantalla anterior">Salir</span>
			                </button>		
					    </td>	
					    <td width="20%">&nbsp;</td>
				    </tr>
                </table>	
            </center>   
            <asp:textbox id="hdnOrden" runat="server" style="visibility:hidden" >2</asp:textbox>
			<asp:textbox id="hdnAscDesc" runat="server" style="visibility:hidden" >0</asp:textbox>
			<asp:textbox id="hdnArea" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnIDTarea" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnIDDefi" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnFICEPI" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnNotificador" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnResponsable" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnEspecialistas" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnEsCoordinador" runat="server" style="visibility:hidden" >false</asp:textbox>
			<asp:textbox id="hdnEsTecnico" runat="server" style="visibility:hidden" >false</asp:textbox>
			<asp:textbox id="hdnEsControlador_o_Creador" runat="server" style="visibility:hidden" >false</asp:textbox>
			<asp:textbox id="hdnErrores" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnCorreoCreador" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnCorreoNotificador" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnCorreoControlador" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnEnviarCorreoCreador" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnCorreoResponsable" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnCorreoEspecialistaIn" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnCorreoEspecialistaOut" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnEnvCorreoCoordinador" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnEstado" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnMotivo" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnBusqueda" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnSalir" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnAdmin" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnAvanceIn" runat="server" style="visibility:hidden" >0</asp:textbox>
			<asp:TextBox ID="hdnModoLectura" runat="server" style="visibility:hidden" Text="" />
            <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
            <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
        <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    </form>	
	<script type="text/javascript">
			function __doPostBack(eventTarget, eventArgument) {
			    var bEnviar = true;
			    if (eventTarget.split("$")[2] == "Botonera") {
			        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
					switch (strBoton){
						case "salir": //Boton salir
						{
							bEnviar = false;
							cerrarVentana();
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
	</body>
	</html>
