<%@ Page language="c#" Inherits="GESTAR.Capa_Presentacion.ASPX.MtoAreas" EnableEventValidation="false" CodeFile="default.aspx.cs" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="cabecera" runat="server">
<title>Detalle área</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
        <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
        <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../JavaScript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../JavaScript/documentos.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../JavaScript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../JavaScript/modal.js" type="text/Javascript"></script> 
	<link href="../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/>
</head>
<body class="FondoBody" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />	
    <style type="text/css">  
        #tsPestanas table { table-layout:auto; }
    </style>
	<form id="frmDatos" runat="server">
		<asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" >-1</asp:textbox>
		<asp:textbox id="hdnValidacion" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnFilaSeleccionada" runat="server" style="visibility:hidden" >-1</asp:textbox>
		<asp:textbox id="hdnErrores" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnModoLectura" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnIDFICEPI" runat="server" style="visibility:hidden" ></asp:textbox>
   		<asp:textbox id="hdnPromotorCorreo" runat="server" style="visibility:hidden" ></asp:textbox>		    
   		<asp:textbox id="hdnPromotorCorreoOld" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnPromotor" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnCoordinador" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnResponsables" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnResponsablesIn" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnCoordinadores" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnCoordinadoresIn" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnSolicitantes" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnSolicitantesIn" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnTecnicos" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnTecnicosIn" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnAdmin" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnActividadLibre" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnOrden" runat="server" style="visibility:hidden" >2</asp:textbox>
		<asp:textbox id="hdnAscDesc" runat="server" style="visibility:hidden" >0</asp:textbox>					
        <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
        <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
	<script type="text/javascript">
		mostrarProcesando();
	    var sNumEmpleado = "<% =Session["IDFICEPI"].ToString() %>";
		var strServer = "<%=Session["strServer"].ToString()%>";
		var intSession = <%=Session.Timeout%>; 
        var bNueva = <%=Request.QueryString["bNueva"] %>;
        var bAdministrador = <%=(Session["ADMIN"].ToString() == "A")? "true":"false" %>;
	</script>
<center>
<table id="tabla" style="width:900px">
<tr>
	<td>
		<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
						MultiPageID="mpContenido" 
						ClientSideOnLoad="CrearPestanas" 
						ClientSideOnItemClick="getPestana">
			<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
				<Items>
						<eo:TabItem Text-Html="General" ToolTip="" Width="100"></eo:TabItem>
						<eo:TabItem Text-Html="Integrantes" ToolTip="" Width="100"></eo:TabItem>
						<eo:TabItem Text-Html="Mantenimientos" ToolTip="" Width="120"></eo:TabItem>
						<eo:TabItem Text-Html="Documentación" ToolTip="" Width="120"></eo:TabItem>
						<eo:TabItem Text-Html="Cambios" ToolTip="" Width="100"></eo:TabItem>
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
			<br /><br />
            <center>
    			<table cellpadding="3px" style="text-align:left;width:80%">
                <colgroup><col style="width:15%"/><col style="width:85%" /></colgroup>
				    <tr>
					    <td><asp:label id="lblPromotor" ToolTip="Permite la selección de un promotor" onclick="javascript:Cargar('Promotor');" runat="server" SkinID="enlace" Visible="true">Promotor</asp:label></td>
					    <td valign="middle">
						    <table class="texto" border="0" style="width:560px;">
                                <colgroup><col style="width:380px;"/><col style="width:180px;"/></colgroup>
							    <tr>
								    <td>
                                        <asp:textbox id="txtPromotor" runat="server" MaxLength="62" CssClass="textareatexto" width="280px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input class="check" id="chkCorreo" checked hidefocus onclick="javascript:ActivarGrabar(1);" type="checkbox" name="chkCorreo" runat="server">&nbsp;&nbsp;
                                        <label style="cursor:pointer" title='El promotor se suscribe a recibir alertas por correo'>Avisos</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								    </td>
								    <td align="left">
			                            <fieldset class="fld" style="width:150px;">
									    <legend>Estado</legend>
									    <table class="texto" id="tblEstado">
										    <tr>
											    <td>
                                                    <asp:RadioButtonList ID="rdlEstado" SkinId="rbli" style="margin-top:5px; width:170px;" runat="server" RepeatColumns="2" onclick="javascript:ActivarGrabar(1);">
	                                                    <asp:ListItem Value="0" Selected="True">Activa</asp:ListItem>
	                                                    <asp:ListItem Value="1">Inactiva</asp:ListItem>
                                                    </asp:RadioButtonList>	
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
					    <td>Denominación</td>
					    <td valign="middle">
						    <table class="texto" border="0" style="width:560px;">
                                <colgroup><col style="width:380px;"/><col style="width:180px;"/></colgroup>
							    <tr>
								    <td><asp:textbox onKeyUp="javascript:ActivarGrabar(1);"
										    id="txtNombreArea" runat="server" MaxLength="50" CssClass="textareaTexto" width="350px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								    </td>
								    <td align="left">
			                            <fieldset class="fld"  style="width:150px;">
									    <legend>Categoría</legend>
									    <table class="texto" id="tblCategoria">
									        <tr>
											    <td>
													<asp:RadioButtonList ID="rdlCategoria" SkinId="rbli" style="width:170px;" runat="server" RepeatColumns="2" onclick="javascript:ActivarGrabar(1);BotonesSolici();">
													    <asp:ListItem Value="0" Selected="True">Restringida</asp:ListItem>
													    <asp:ListItem Value="1">Libre</asp:ListItem>
													</asp:RadioButtonList>	
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
					    <td>Creada el</td>
					    <td valign="middle" >				
						    <asp:textbox id="txtFechaAlta" runat="server" width="60px" CssClass="textareatexto" MaxLength="15"></asp:textbox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input class="check" id="chkSelCoord" hidefocus onclick="javascript:ActivarGrabar(1);"
							    type="checkbox" name="chkSelCoord" runat="server">&nbsp;&nbsp;Permitir seleccionar coordinador             
    &nbsp;&nbsp;&nbsp;<input class="check" id="chkResuelta" hidefocus onclick="javascript:ActivarGrabar(1);"
							    type="checkbox" name="chkResuelta" runat="server">&nbsp;&nbsp;Hacer validación al pasar la orden a estado 'Resuelta'					
					    </td>
				    </tr>										
				    <tr>
					    <td valign="top"><BR />Descripción</td>
					    <td><BR />
                            <asp:textbox onKeyUp="javascript:ActivarGrabar(1);"
							    id="txtDescripcion" runat="server" SkinID="Multi" width="545px" TextMode="MultiLine" Rows="12"></asp:textbox>
					    </td>
				    </tr>
				    <tr>
				        <td>&nbsp;</td>
				        <td><input class="check" id="chkAutoaprobacion" hidefocus="hidefocus" onclick="javascript:ActivarGrabar(1);"
							    type="checkbox" name="chkAutoaprobacion" runat="server" style="vertical-align:middle; cursor:pointer;" />&nbsp;<label onclick="$I('chkAutoaprobacion').click()" title="Con el switch activado, las órdenes que
    lleven más de 60 días resueltas y cuyo estado no haya sido modificado posteriormente, pasarán automáticamente el estado 'Aprobada'.">Autoaprobación</label>
        			    </td>
				    </tr>
				    <tr>
				        <td>&nbsp;</td>
				        <td id="tdPermitirCambio" runat="server" style="visibility:hidden">
                            <input class="check" id="chkPermitirCambio" hidefocus="hidefocus" onclick="javascript:ActivarGrabar(1);"
							    type="checkbox" name="chkPermitirCambio" runat="server" style="vertical-align:middle; cursor:pointer;" />&nbsp;Permitir al promotor modificar datos en pestaña "Cambios"
        			    </td>
				    </tr>
			    </table>			
            </center>
        </eo:PageView>
        <eo:PageView CssClass="PageView" runat="server">
            <!-- Pestaña 2 // Integrantes-->	
	        <table class="texto" id="tblfiltros" width="100%;text-align:left">
			    <tr>
				    <td width="100%">
                        <table style="width:385px; margin-left:20px; height: 40px;">
                            <colgroup><col style="width:130px;" /><col style="width:130px;" /><col style="width:125px;" /></colgroup>
                            <tr>
                                <td>Apellido1</td>
                                <td>Apellido2</td>
                                <td>Nombre</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtApellido1" runat="server" Width="115px" onkeypress="if(event.keyCode==13){CargarDatos();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtApellido2" runat="server" Width="115px" onkeypress="if(event.keyCode==13){CargarDatos();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtNombre" runat="server"    Width="120px" onkeypress="if(event.keyCode==13){CargarDatos();event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
				    </td>
			    </tr>
		    </table>
		    <table id="general" style="width:100%;text-align:left">
			    <tr>
				    <td vAlign="top" align="center" width="49%">
					    <table id="tblTitulo" style="width: 390px;height:17px;text-align:left">
						    <tr class="TBLINI">
							    <td width="90%">&nbsp;&nbsp;<img style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgDescripcion"
									    border="0"> <map name="imgDescripcion">
									    <area onclick="ordenarTabla(2,0)" shape="RECT" coords="0,0,6,5">
									    <area onclick="ordenarTabla(2,1)" shape="RECT" coords="0,6,6,11">
								    </map><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogo',0,'divCatalogo','imgLupa1',event)"
									    height="11" src="../../../Images/imgLupa.gif" width="20"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogo',0,'divCatalogo','imgLupa1')"
									    height="11" src="../../../Images/imgLupaMas.gif" width="20"> &nbsp;Profesionales</td>
							    <td width="10%"><img onmouseover="javascript:bMover=true;moverTablaUp()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
									    height="8" src="../../../Images/imgFleUp.gif" width="11"></td>
						    </tr>
					    </table>
					    <div id="divCatalogo" style="overflow-x: hidden; overflow: auto; width: 406px; height: 360px"   >
						    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">
						        <table id="tblCatalogo2">
							        <tr>
								        <td><img style="width:2px" src="../../../Images/imgSeparador.gif" align="left"></td>
								        <td>
									        <table id="tblCatalogo" style="width: 390px">
									        </table>
								        </td>
							        </tr>
						        </table>
                            </div>						    
					    </div>
					    <table id="tblResultado" style="width:390px;height:17px">
						    <tr class="TBLFIN">
							    <td width="90%">&nbsp;<img style="height: 1px" src="../../../Images/imgSeparador.gif" align="left">
							    </td>
							    <td width="10%"><img onmouseover="javascript:bMover=true;moverTablaDown()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
									    height="8" src="../../../Images/imgFleDown.gif" width="11"></td>
						    </tr>
					    </table>
				    </td>
				    <td valign="top" align="center" width="3%">&nbsp;
					    <table id="general2" width="100%" align="right">
						    <tr>
			                    <td>
			                        <img style="height: 10px" src="../../../Images/imgSeparador.gif" align="left">
			                    </td>
			                </tr>	
						    <tr>
							    <td>						
								    <img id="btn_anadirResponsables" style="cursor: pointer" onclick="anadirResponsables();" height="20" src="../../../Images/imgnextpg.gif"
									    width="20"> 
                                    <img id="btn_eliminarResponsables" style="cursor: pointer" onclick="eliminarResponsables();" height="20" src="../../../Images/imgprevpg.gif"
									    width="20">									
								    <BR>
								    <BR>
								    <BR>
							    </td>
						    </tr>
			                <tr>
			                    <td>
			                        <img style="height: 40px" src="../../../Images/imgSeparador.gif" align="left">
			                    </td>
			                </tr>						
						    <tr>
							    <td>															
								    <img id="btn_anadirCoordinadores" style="cursor: pointer" onclick="anadirCoordinadores();" height="20" src="../../../Images/imgnextpg.gif"
									    width="20"> 
                                    <img id="btn_eliminarCoordinadores" style="cursor: pointer" onclick="eliminarCoordinadores();" height="20" src="../../../Images/imgprevpg.gif"
									    width="20">
								    <BR>
								    <BR>
								    <BR>
							    </td>
						    </tr>
			                <tr>
			                    <td>
			                        <img style="height: 35px" src="../../../Images/imgSeparador.gif" align="left">
			                    </td>
			                </tr>											
						    <tr>
							    <td>																
								    <img id="btn_anadirSolicitantes" style="cursor: pointer" onclick="anadirSolicitantes();"
									    height="20" src="../../../Images/imgnextpg.gif" width="20"> 
                                    <img id="btn_eliminarSolicitantes" style="cursor: pointer" onclick="eliminarSolicitantes();"
									    height="20" src="../../../Images/imgprevpg.gif" width="20">					
								    <BR>
								    <BR>
								    <BR>
			                    </td>
			                </tr>
			                <tr>
			                    <td>
			                        <img style="height: 30px" src="../../../Images/imgSeparador.gif" align="left">
			                    </td>
			                </tr>											
						    <tr>
							    <td>				            	
								    <img id="btn_anadirTecnicos" style="cursor: pointer" onclick="anadirTecnicos();" height="20" src="../../../Images/imgnextpg.gif"
									    width="20"> 
                                    <img id="btn_eliminarTecnicos" style="cursor: pointer" onclick="eliminarTecnicos();" height="20" src="../../../Images/imgprevpg.gif"
									    width="20">
							    </td>
						    </tr>
					    </table>
				    </td>
				    <td valign="top" width="47%">
					    <table id="general3" width="48%" align="left">
						    <tr>
							    <td>
								    <table id="tblTitulo4" style="width: 390px;height:17px">
									    <tr class="TBLINI">
										    <td width="100%">&nbsp;&nbsp;&nbsp;Responsables
										    </td>
									    </tr>
								    </table>
								    <div id="divCatalogo4" style="overflow-x: hidden; overflow: auto; width: 406px; height: 53px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">
									        <%=strTablaHtmlResponsable%>
                                        </div>
								    </div>
								    <table id="tblResultado4" style="width: 390px;height: 17px">
									    <tr class="TBLFIN">
										    <td>&nbsp;<img height="1" src="../../../Images/imgSeparador.gif">
										    </td>
									    </tr>
								    </table>
							    </td>
						    </tr>
						    <tr>
							    <td><BR>
							    </td>
						    </tr>
						    <tr>
							    <td>
								    <table id="tblTitulo3" style="width: 390px;height:17px">
									    <tr class="TBLINI">
										    <td width="100%">&nbsp;&nbsp;&nbsp;Coordinadores
										    </td>
									    </tr>
								    </table>
								    <div id="divCatalogo3" style="overflow-x: hidden; overflow: auto; width: 406px; height: 53px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">
									        <%=strTablaHtmlCoordinador%>
                                        </div>
								    </div>
								    <table id="tblResultado3" style="width: 390px;height:17px">
									    <tr class="TBLFIN">
										    <td>&nbsp;<img height="1" src="../../../Images/imgSeparador.gif">
										    </td>
									    </tr>
								    </table>
							    </td>
						    </tr>
						    <tr>
							    <td><BR>
							    </td>
						    </tr>
						    <tr>
							    <td>
								    <table id="tblTitulo2" style="width: 390px;height:17px">
									    <tr class="TBLINI">
										    <td width="100%">&nbsp;&nbsp;&nbsp;Solicitantes
										    </td>
									    </tr>
								    </table>
								    <div id="divCatalogo2" style="overflow-x: hidden; overflow: auto; width: 406px; height: 53px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">
									        <%=strTablaHtmlSolicitante%>
                                        </div>
								    </div>
								    <table id="tblResultado2" style="width: 390px;height:17px">
									    <tr class="TBLFIN">
										    <td>&nbsp;<img height="1" src="../../../Images/imgSeparador.gif">
										    </td>
									    </tr>
								    </table>
							    </td>
						    </tr>
						    <tr>
							    <td><BR>
							    </td>
						    </tr>
						    <tr>
							    <td>
								    <table id="tblTitulo5" style="width: 390px;height:17px">
									    <tr class="TBLINI">
										    <td width="100%">&nbsp;&nbsp;&nbsp;Especialistas
										    </td>
									    </tr>
								    </table>
								    <div id="divCatalogo5" style="overflow-x: hidden; overflow: auto; width: 406px; height: 55px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">
									        <%=strTablaHtmlTecnico%>
                                        </div>
								    </div>
								    <table id="tblResultado5" style="width: 390px;height:17px">
									    <tr class="TBLFIN">
										    <td>&nbsp;<img height="1" src="../../../Images/imgSeparador.gif">
										    </td>
									    </tr>
								    </table>
							    </td>
						    </tr>
					    </table>
				    </td>
			    </tr>
		    </table>
        </eo:PageView>
        <eo:PageView CssClass="PageView" runat="server">
        <!-- Pestaña 3 // Mto de conceptos-->
        <br><br>
		    <table id="conceptos" style="text-align:left;width:100%">
                <colgroup><col style="width:30%"/><col style="width:70%"/></colgroup>
			    <tr>
				    <td valign="top" align="center">
				        <table id="tblTituloConceptos" style="width: 190px;height:17px" >
						    <tr class="TBLINI">
							    <td width="10%" align="center">&nbsp;Conceptos
								</td>
						    </tr>
				        </table>
				        <br />		
                        <table id="tblCptos" style="width: 190px;text-align:left;margin-left:80px" cellpadding="5px">
	                            <tr onclick="concepto(1);">
	                                <td>
	   		                            <input type="radio" id="rdlCpto1" class="texto radio" />&nbsp;&nbsp;Tipo
		                            </td>
	                            </tr>
	                            <tr onclick="concepto(2);">	  
	                                <td>
	   		                            <input type="radio" id="rdlCpto2" class="texto radio" />&nbsp;&nbsp;Alcance
	                                </td>
	                            </tr>
	                            <tr onclick="concepto(3);">	     
	                                <td>
	   		                            <input type="radio" id="rdlCpto3" class="texto radio"/>&nbsp;&nbsp;Proceso
	                                </td>
	                            </tr>
	                            <tr onclick="concepto(4);">		     
	                                <td>
	   		                            <input type="radio" id="rdlCpto4" class="texto radio"/>&nbsp;&nbsp;Producto
	                                </td>
	                            </tr>
	                            <tr onclick="concepto(5);">		     
	                                <td>
	   		                            <input type="radio" id="rdlCpto5" class="texto radio"/>&nbsp;&nbsp;Requisito
	                                </td>
	                            </tr>
	                            <tr onclick="concepto(6);">		     
	                                <td>
	   		                            <input type="radio" id="rdlCpto6" class="texto radio"/>&nbsp;&nbsp;Causa
	                                </td>
	                            </tr>
	                            <tr onclick="concepto(7);">		     
	                                <td>
	   		                            <input type="radio" id="rdlCpto7" class="texto radio"/>&nbsp;&nbsp;Origen
	                                </td>
	                            </tr>
 	                            <tr onclick="concepto(8);">		     
	                                <td>
	   		                            <input type="radio" id="rdlCpto8" class="texto radio"/>&nbsp;&nbsp;Entrada
	                                </td>
	                            </tr>                           	
                        </table>			  			                
				    </td>
				    <td valign="top">
                        <table style="width: 517px;">
                        <tr>
                            <td colspan="6">
		                        <table id="tblTituloConcep" style="width: 500px; height: 17px">
			                        <tr class="TBLINI">
				                        <td width="425px">&nbsp;Valores</td>
				                        <td width="75px">Orden</td>
			                        </tr>
		                        </table>
                        	</td>
                        </tr>
                        <tr>
                        	<td colspan="6">   
		                        <div id="divCatValores" style="overflow-x: hidden; overflow: auto; width: 516px; height:320px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT22.gif'); width:500px">
                                        <table id='tblCatValores' style='width: 500px'><colgroup><col style='width:425px;' /><col style='width:75px;' /></colgroup>
                                        </table>
                                    </div>
                                </div>
                        			
		                        <table id="tblPieConcep" style="width: 500px; height: 17px">
			                        <tr class="TBLFIN">
				                        <td></td>
			                        </tr>
		                        </table>
                        			
                                <br />
                            </td>
                        </tr>
	                    <tr id="PieConcepto" style="visibility:visible">
		                    <td colspan="6">
                                <table style="margin-left:30px;width:450px; margin-top:10px;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnNuevo" type="button" onclick="nuevoConcepto();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                                                onmouseover="se(this, 25);mostrarCursor(this);" style="margin-left:10px; margin-top:5px;">
	                                            <img src="../../../Images/Botones/imgNuevo.gif" /><span title='Añade un nuevo elemento'>Añadir</span>
                                            </button>
                                        </td>
                                        <td align="center">
                                            <button id="btnEliminar" type="button" onclick="eliminarConcepto();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                                                onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
	                                            <img src="../../../Images/botones/imgEliminar.gif" /><span title="Elimina el elemento seleccionado">Eliminar</span>
                                            </button>
                                        </td>
                                    </tr>
                                </table>		                       
		                    </td>
	                    </tr>		
                        </table>	
				    </td>
			    </tr>
		    </table>                
        </eo:PageView>
        <eo:PageView CssClass="PageView" runat="server">
            <!-- Pestaña 4 // Documentación-->	
            <br>
                <table class="texto" width="100%" align="center">
                <tr>
	                <td colspan="6">
		            <table style="width: 870px; height: 17px" >
		                        <colgroup>
		                            <col width="280px"/>
		                            <col width="225px"/>
		                            <col width="215px"/>
		                            <col width="150px"/>
		                        </colgroup>
			                        <tr class="TBLINI">
				                        <td>&nbsp;Descripción</td>
				                        <td>Archivo</td>
				                        <td>Link</td>
				                        <td>Autor</td>
			                        </tr>
		            </table>
		            <div id="divCatalogoDoc" style="overflow-x: hidden; overflow: auto; width: 886px; height:330px" runat="server">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:870px">
                            <%=strTablaHTMLDocum%>
                        </div>
                    </div>
		            <table style="width: 870px; BORDER-COLLAPSE: collapse; height: 17px">
			            <tr class="TBLFIN">
				            <td></td>
			            </tr>
		            </table>
		            <br />
                    </td>
            </tr>
	        <tr>
	            <td colspan="6">
	                <img style="height: 6px" src="../../../Images/imgSeparador.gif" align="left">
	            </td>
	        </tr>		                
            <tr id="PieDocumentacion" style="visibility:visible">
                <td colspan="6">
					<table style="margin-left:190px;width:450px; margin-top:10px;">
						<tr>
							<td align="center">
								<button id="btnNuevoDocumentacion" type="button" onclick="nDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										onmouseover="se(this, 25);mostrarCursor(this);" style="margin-left:10px; margin-top:5px;">
									<img src="../../../Images/Botones/imgNuevo.gif" /><span title='Añade un nuevo elemento'>Añadir</span>
								</button>
							</td>
							<td align="center">
								<button id="btnEliminarDocumentacion" type="button" onclick="eDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
										onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
									<img src="../../../Images/botones/imgEliminar.gif" /><span title="Elimina el elemento seleccionado">Eliminar</span>
								</button>
							</td>
						</tr>
					</table>								
				</td>
            </tr>	
            </table>
            <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
        </eo:PageView>
        <eo:PageView CssClass="PageView" runat="server">
            <!-- Pestaña 5 // Cambios fechas, etc,...-->	
                <table style="width:885px; margin-top:3px;text-align:left" align="left">
                <colgroup>
                    <col style="width: 580px;"/>
                    <col style="width: 305px;"/>
                </colgroup>
                <tr>
	                <td colspan="2">
	                <label id="lblOrden">Órdenes</label><br />
                    <div id="divTablaTitulo" style=" overflow-x:hidden; width: 860px; height:17px;" runat="server">
                    <table style="width: 1140px; height: 17px; z-index:5;">
                        <colgroup>
                            <col style="width:70px;"/>
                            <col style="width:200px;"/>
                            <col style="width:120px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:120px;"/>
                        </colgroup>
	                    <tr class="tituloColumnaTabla" align="center">
	                        <td style="text-align: right; padding-right: 10px;">Orden</td>
	                        <td style="text-align:left;">Denominación</td>
	                        <td>F.Alta</td>
	                        <td>F.Notificada</td>
	                        <td>F.Límite</td>
	                        <td>F.Pactada</td>
	                        <td>F.Ini.Prev.</td>
	                        <td>F.Fin.Prev.</td>
	                        <td>Esf.Prev.</td>
	                        <td>F.Ini.Real</td>
	                        <td>F.Fin.Real</td>
	                        <td align="right">Esf.Real</td>
	                        <td>F.Ult.Modif.</td>
	                    </tr>
                    </table>
                    </div>
                    <div id="div1" style="overflow-x:scroll; overflow-y:scroll; width: 876px; height:190px;" onscroll="moverScroll();">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:1140px">
                        <%=strTablaHTMLOrdenes%>
                        </div>
                    </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 580px; padding-top:15px;">
	                <label id="lblTareas">Tareas</label><br />
                    <table style="width: 550px; height: 17px; z-index:5;">
                        <colgroup>
                            <col style="width:70px;"/>
                            <col style="width:200px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                            <col style="width:70px;"/>
                        </colgroup>
	                    <tr class="tituloColumnaTabla" align="center">
	                        <td style="text-align: right; padding-right: 10px;">Tarea</td>
	                        <td style="text-align:left;">Denominación</td>
	                        <td>F. Ini. Prev.</td>
	                        <td>F. Fin. Prev.</td>
	                        <td>F. Ini. Real</td>
	                        <td>F. Fin. Real</td>
	                    </tr>
                    </table>
                    <div id="divCatalogoTareas" style="overflow-x:hidden; overflow-y:auto; width: 566px; height:160px;">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:550px">
                        <%//=strTablaHTMLOrdenes%>
                        </div>
                    </div>
                    <table style="width: 550px; height: 17px; z-index:5;">
	                    <tr class="textoResultadoTabla">
	                        <td>&nbsp;</td>
	                    </tr>
                    </table>
                    </td>
                    <td style="width: 305px; padding-top:15px;">
	                <label id="lblCronologia">Cronología</label><br />
                    <table style="width: 280px; height: 17px; z-index:5;">
	                    <tr class="tituloColumnaTabla" align="center">
	                        <td style="width:160px; text-align:left; padding-left:2px;">Estado</td>
	                        <td style="width:120px; text-align:left">Fecha</td>
	                    </tr>
                    </table>
                    <div id="divCatalogoCronologia" style="overflow-x:hidden; overflow-y:auto; width: 296px; height:160px;">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:280px">
                        <%//=strTablaHTMLOrdenes%>
                        </div>
                    </div>
                    <table style="width: 280px; BORDER-COLLAPSE: collapse; table-layout:fixed; height: 17px; z-index:5;">
	                    <tr class="textoResultadoTabla">
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
</center>    	
<br />
<center>
<table id="tblBotones" style="width:400px; margin-top:25px;" class="texto">
	<tr> 
<%--		<td> 
			<button id="btnEntrada" type="button" onclick="entrada()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgEntrada.gif" /><span title="Permite el mantenimiento de entradas asociadas al área">Entrada</span>
			</button>	
		</td>--%>
		<td> 
			<button id="btnGrabar" type="button" onclick="grabar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">Grabar</span>
			</button>	
		</td>
		<td> 
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar</span>
			</button>	
		</td>
		<td>
			<button id="btnSalir" type="button" onclick="salir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
			</button>	
		</td>
	</tr>
</table>	
</center>    						
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
