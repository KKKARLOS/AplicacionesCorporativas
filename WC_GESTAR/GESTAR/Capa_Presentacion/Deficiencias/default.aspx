<%@ Page language="c#" Inherits="GESTAR.Capa_Presentacion.ASPX.DetalleDeficiencia" ValidateRequest="false" EnableEventValidation="false" CodeFile="default.aspx.cs" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="cabecera" runat="server">
	<title>
	<%=strTitulo%></title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<link href="../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/> 
	<link rel="stylesheet" href="../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../JavaScript/funciones.js" type="text/Javascript"></script>	
    <script language="JavaScript" src="../../JavaScript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../JavaScript/documentos.js" type="text/Javascript"></script>	
    <script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	    var strServer = "<%=Session["strServer"].ToString()%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    </script>
</head>
	<body class="FondoBody" style="overflow:hidden" onload="init()">
        <ucproc:Procesando ID="Procesando" runat="server" />       
        <style type="text/css">  
            #tsPestanas table { table-layout:auto; }
        </style>

<form id="frmDatos" runat="server">
	<script type="text/javascript">
		mostrarProcesando();
	    var sNumEmpleado = "<%=Session["IDFICEPI"].ToString()%>";
		var strServer = "<%=Session["strServer"].ToString()%>";
		var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.	
        var bNueva = <%=Request.QueryString["bNueva"] %>;
	</script>
    <div style="display:none">
	    <asp:textbox id="hdnFilaSeleccionada" runat="server" style="visibility:hidden" >-1</asp:textbox>                
        <asp:textbox id="hdnEntrada" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnAlcance" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnTipo" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnProducto" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnProceso" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnRequisito" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnCausa" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnSolicitante" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnCoordinador" runat="server" style="visibility:hidden" >0</asp:textbox>	
        <asp:textbox id="hdnCoordinadorOld" runat="server" style="visibility:hidden" >0</asp:textbox>	    					
        <asp:textbox id="hdnOrden" runat="server" style="visibility:hidden" >2</asp:textbox>
        <asp:textbox id="hdnAscDesc" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnIDDefi" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnIDTarea" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnFICEPI" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnTecnicos" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnEsSolicitante" runat="server" style="visibility:hidden" >false</asp:textbox>
        <asp:textbox id="hdnEsCoordinador" runat="server" style="visibility:hidden" >false</asp:textbox>
        <asp:textbox id="hdnEsTecnico" runat="server" style="visibility:hidden" >false</asp:textbox>
        <asp:textbox id="hdnEsPromotor_o_Responsable" runat="server" style="visibility:hidden" >false</asp:textbox>
        <asp:textbox id="hdnErrores" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnCorreoPromotor" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnCorreoSolicitante" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnEnvCorreoSolicitante" runat="server" style="visibility:hidden" ></asp:textbox>    
        <asp:textbox id="hdnCorreoResponsable" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnCorreoResponsables" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnEnviarCorreoPromotor" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnCorreoCoordinador" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnCorreoCoordinadores" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnCorreoEspecialistas" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnEnvCorreoCoordinador" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnEnvCorreoResponsable" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnIdEstado" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnDenEstado" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnMotivo" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnAdmin" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnModoLectura" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnFechaAlta" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnResuelta" runat="server" style="visibility:hidden" ></asp:textbox>    
    </div>
    <center>
    <br />
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
							    <eo:TabItem Text-Html="Avanzado" ToolTip="" Width="100"></eo:TabItem>
							    <eo:TabItem Text-Html="Tareas" ToolTip="" Width="100"></eo:TabItem>
							    <eo:TabItem Text-Html="Documentación" ToolTip="" Width="120"></eo:TabItem>
							    <eo:TabItem Text-Html="Planif./Cronología" ToolTip="" Width="120"></eo:TabItem>
							    <eo:TabItem Text-Html="Comentarios" ToolTip="" Width="110"></eo:TabItem>
							    <eo:TabItem Text-Html="Pruebas" ToolTip="" Width="100"></eo:TabItem>
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
    			        <table cellpadding="3px" style="text-align:left;margin-top:15px;margin-left:5px;width:99%">
		                    <tr>
			                    <td>&nbsp;Área&nbsp;&nbsp;&nbsp;
				                    <asp:textbox id="txtArea" runat="server" width="330px" MaxLength="62" CssClass="textareatexto" ReadOnly=true></asp:textbox>
				                    &nbsp;&nbsp;&nbsp;&nbsp;                   
				                
				                 </td>
		                    </tr>
		                    <tr>
			                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
			                    </td>
		                    </tr>
		                    <tr>
		                    <td>
            			    <fieldset class="fld" style="width:96%">
			                    <legend class="Tooltip" title="Información">&nbsp;Orden&nbsp;</legend>
			                    <table  width="100%" align="center">
				                    <tr>		
				                    <td width="9%">&nbsp;&nbsp;Nº&nbsp;
					                    <asp:TextBox id="txtIdDeficiencia" runat="server" width="40px" align="right" CssClass="textareaNumero" ReadOnly=true></asp:TextBox></td>
				                    <td align="center" width="72%">Denominación&nbsp;(Asunto)&nbsp;
					                    <asp:TextBox onKeyUp="javascript:if ($I('hdnIdEstado').value==0) ActivarGrabar();" id="txtDenominacion" runat="server" width="360px"
						                    CssClass="textareatexto"></asp:TextBox>
                                            &nbsp;Creada el&nbsp;<asp:textbox id="txtFechaAlta" runat="server" width="60px" CssClass="textareatexto" MaxLength="15" ReadOnly=true></asp:textbox>						                
						                    </td>
				                    <td align="right" width="19%">
					                    <asp:label id="lblEstado" runat="server" CssClass="label" Visible="True">Estado&nbsp;</asp:label>
					                    <asp:DropDownList id="cboEstado" runat="server" width="120px" AutopostBack="false" CssClass="combo"
						                    onchange="javascript:ActivarGrabar();">
						                </asp:DropDownList></td>
				                    </tr>
			                    </table>
			                </fieldset>
			                </td>	
		                    </tr>
		                    <tr>
			                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
			                    </td>
		                    </tr>		                
			                <tr>
			                <td>
			                <fieldset class="fld" style="width:96%">
				                <legend class="Tooltip" title="Integrantes">&nbsp;Integrantes&nbsp;</legend>
				                <table width="100%" align="center">
					                <tr>	
					                <td width="50%" valign="middle">&nbsp;&nbsp;Solicitante&nbsp;
						                <asp:textbox id="txtSolicitante" runat="server" width="340px" CssClass="textareatexto"
							                MaxLength="70" ReadOnly=true></asp:textbox></td>
					                <td width="50%" valign="middle"><asp:label id="lblCoordinador" ToolTip="Permite la selección de un coordinador" onclick="javascript:CargarDatos('Coordinador');" runat="server" SkinID="enlace" Visible="true">Coordinador</asp:label>&nbsp;
						                <asp:textbox id="txtCoordinador" runat="server" width="300px" CssClass="textareatexto"
							                MaxLength="70" ReadOnly=true></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCoordinador" style="cursor: pointer" onclick="javascript:$I('txtCoordinador').value='';$I('hdnCoordinador').value='0';$I('hdnCorreoCoordinador').value='';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
					                </tr>
				                </table>
			                </fieldset>
			                </td>	
			                </tr>
		                    <tr>
			                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
			                    </td>
		                    </tr>		                
						    <tr>
						    <td>
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:50%">
						                    <fieldset class="fld" style="width:420px">
							                    <legend class="Tooltip" title="Fechas">&nbsp;Fechas&nbsp;</legend>
							                    <table width="97%">
								                    <tr>	
								                    <td>
									                    Notificación&nbsp;
                                                        <asp:textbox id="txtFechaNotificacion" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura=0></asp:textbox>
									                    &nbsp;Límite&nbsp;
                                                        <asp:textbox id="txtFechaLimite" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura=0></asp:textbox>
									                    &nbsp;Pactada&nbsp;
                                                        <asp:textbox id="txtFechaPactada" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura=0></asp:textbox>
								                    </td>
								                    </tr>
		                                            <tr>
			                                            <td><img style="height: 3px" src="../../Images/imgSeparador.gif" align="left">
			                                            </td>
		                                            </tr>		 								
							                    </table>
						                    </fieldset>					                
                                        </td>
                                        <td style="width:50%" align="center">
                                            <fieldset class="fld" style="width:380px;height:29px;margin-top:4px">
							                    <table  style="width:100%;margin-top:8px">
								                    <tr valign="middle">	
								                    <td>&nbsp;Importancia&nbsp;
						                            <asp:dropdownlist id="cboImportancia" runat="server" width="60px" CssClass="combo" onchange="javascript:ActivarGrabar();"
							                            AutopostBack="false">
							                            <asp:ListItem Value="1">Crítica</asp:ListItem>
							                            <asp:ListItem Value="2">Alta</asp:ListItem>
							                            <asp:ListItem Value="3" Selected="True">Media</asp:ListItem>
							                            <asp:ListItem Value="4">Baja</asp:ListItem>
						                            </asp:dropdownlist>							
								                    &nbsp;Prioridad&nbsp;&nbsp;
                                                    <asp:dropdownlist id="cboPrioridad" runat="server" width="60px" CssClass="combo" onchange="javascript:ActivarGrabar();"
							                            AutopostBack="false">
							                            <asp:ListItem Value="1">Máxima</asp:ListItem>
							                            <asp:ListItem Value="2" Selected="True">Normal</asp:ListItem>
							                            <asp:ListItem Value="3">Mínima</asp:ListItem>
						                            </asp:dropdownlist>
                                                    &nbsp;Avance&nbsp;
									                    <asp:DropDownList id="cboAvance" runat="server" width="55   px" AutopostBack="false" CssClass="combo"
										                    onchange="javascript:ActivarGrabar();">										
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
							                    </table>
						                    </fieldset>						
                                        </td>
                                    </tr>
                                </table>
						    </td>	

						    </tr>	
		                    <tr>
			                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
			                    </td>
		                    </tr>		                
						    <tr>
						    <td>&nbsp;&nbsp;Descripción&nbsp;<br/>
									    <asp:textbox onKeyUp="javascript:if ($I('hdnIdEstado').value==0||$I('hdnIdEstado').value==2||$I('hdnIdEstado').value==4||$I('hdnIdEstado').value==7) ActivarGrabar();"
									    id="txtDescripcion" runat="server" SkinID="Multi" width="98%" TextMode="MultiLine" Rows="15">
									    </asp:textbox>									
						    </td>	
						    </tr>																						            		                
	                    </table>
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">                
                    <br />
                    <!-- Pestaña 2 // Avanzado -->	
                   <table class="texto" width="98%;text-align:left">
				        <tr>
					        <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
					        </td>
				        </tr>
				        <tr>
				        <td>
					        <fieldset class="fld">
						        <table cellpadding="3" width="98%">
							        <tr>	
							        <td width="8%" valign="bottom"><asp:label id="lblEntrada" ToolTip="Permite la selección de una entrada" onclick="javascript:CargarDatos('Entrada');" runat="server" SkinID="enlace" Visible="true">Entrada</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtEntrada" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnEntrada" style="cursor: pointer" onclick="javascript:$I('txtEntrada').value='';$I('hdnEntrada').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        <td width="8%" valign="middle"><asp:label id="lblAlcance" ToolTip="Permite la selección de un alcance" onclick="javascript:CargarDatos('Alcance');" runat="server" SkinID="enlace" Visible="true">Alcance</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtAlcance" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnAlcance" style="cursor: pointer" onclick="javascript:$I('txtAlcance').value='';$I('hdnAlcance').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        </tr>
							        <tr>	
							        <td width="8%"valign="bottom"><asp:label id="lblTipo" ToolTip="Permite la selección de un tipo" onclick="javascript:CargarDatos('Tipo');" runat="server" SkinID="enlace" Visible="true">Tipo</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtTipo" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnTipo" style="cursor: pointer" onclick="javascript:$I('txtTipo').value='';$I('hdnTipo').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        <td width="8%" valign="middle"><asp:label id="lblProducto" ToolTip="Permite la selección de un producto" onclick="javascript:CargarDatos('Producto');" runat="server" SkinID="enlace" Visible="true">Producto</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtProducto" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnProducto" style="cursor: pointer" onclick="javascript:$I('txtProducto').value='';$I('hdnProducto').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        </tr>	
							        <tr>	
							        <td width="8%" valign="bottom"><asp:label id="lblProceso" ToolTip="Permite la selección de un proceso" onclick="javascript:CargarDatos('Proceso');" runat="server" SkinID="enlace" Visible="true">Proceso</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtProceso" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnProceso" style="cursor: pointer" onclick="javascript:$I('txtProceso').value='';$I('hdnProceso').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        <td width="8%" valign="middle"><asp:label id="lblRequisito" ToolTip="Permite la selección de un requisito" onclick="javascript:CargarDatos('Requisito');" runat="server" SkinID="enlace" Visible="true">Requisito</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtRequisito" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnRequisito" style="cursor: pointer" onclick="javascript:$I('txtRequisito').value='';$I('hdnRequisito').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        </tr>							
							        <tr>	
							        <td width="8%" valign="bottom"><asp:label id="lblCausa" ToolTip="Permite la selección de una causa" onclick="javascript:CargarDatos('Causa');" runat="server" SkinID="enlace" Visible="true">Causa</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtCausa" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCausa" style="cursor: pointer" onclick="javascript:$I('txtCausa').value='';$I('hdnCausa').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        <td width="8%" valign="middle"><asp:label id="lblCR" ToolTip="Permite la selección de un CR" onclick="javascript:CargarDatos('CR');" runat="server" SkinID="enlace" Visible="true">C.R.</asp:label>&nbsp;</td>
							        <td width="42%"><asp:textbox id="txtCR" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCR" style="cursor: pointer" onclick="javascript:$I('txtCR').value='';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        </tr>	
							        <tr>	
							        <td width="7%" valign="bottom"><asp:label id="lblProveedor" ToolTip="Permite la selección de un proveedor" onclick="javascript:CargarDatos('Proveedor');" runat="server" SkinID="enlace" Visible="true">Proveedor</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtProveedor" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnProveedor" style="cursor: pointer" onclick="javascript:$I('txtProveedor').value='';$I('hdnProveedor').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        <td width="7%" valign="middle"><asp:label id="lblCliente" ToolTip="Permite la selección de un cliente" onclick="javascript:CargarDatos('Cliente');" runat="server" SkinID="enlace" Visible="true">Cliente</asp:label>&nbsp;</td>
							        <td width="42%">
								        <asp:textbox id="txtCliente" runat="server" width="320px" CssClass="textareatexto"
									        MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCliente" style="cursor: pointer" onclick="javascript:$I('txtCliente').value='';$I('hdnCliente').value='0';ActivarGrabar();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image></td>
							        </tr>								        							
						        </table>
					        </fieldset>
				        </td>	
				        </tr>
	                    <tr>
		                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
		                    </td>
	                    </tr>					    
				        <tr>
						    <td>&nbsp;&nbsp;Causa / Beneficio&nbsp;<br />
									    <asp:textbox onKeyUp="javascript:if ($I('txtCausaBfcio').readOnly==false) ActivarGrabar();"
									    id="txtCausaBfcio" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="8">
									    </asp:textbox>									
						    </td>	
					    </tr>	
	                    <tr>
		                    <td><img style="height: 8px" src="../../Images/imgSeparador.gif" align="left">
		                    </td>
	                    </tr>					    
				        <tr>
						    <td>&nbsp;&nbsp;Resultado&nbsp;&nbsp;
                                    <asp:dropdownlist id="cboRtado" runat="server" width="95px" CssClass="combo" onchange="javascript:ActivarGrabar();"
							            AutopostBack="false">
							            <asp:ListItem Value="1">Satisfactorio</asp:ListItem>
							            <asp:ListItem Value="2">No efectivo</asp:ListItem>
							            <asp:ListItem Value="3">Cancelado</asp:ListItem>
						            </asp:dropdownlist><br /><br />
									    <asp:textbox onKeyUp="javascript:if ($I('txtResultado').readOnly==false) ActivarGrabar();"
									    id="txtResultado" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="7">
									    </asp:textbox>									
						    </td>	
					    </tr>
			        </table>	
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">   
                    <!-- Pestaña 3 // Tareas -->	
                    <br>
	                    <br>
                        <center>
	                        <table style="width:90%;text-align:left">
		                        <tr>
			                        <td>
				                        <table id="tblTitulo" style="width: 830px; height:17px">
					                        <tr class="TBLINI">
						                        <td width="15%">&nbsp;&nbsp;<img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgCodigo"
								                        border="0"> <map name="imgCodigo">
								                        <area onclick="ordenarTabla(1,0)" shape="RECT" coords="0,0,6,5">
								                        <area onclick="ordenarTabla(1,1)" shape="RECT" coords="0,6,6,11">
							                        </map><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogoTarea',0,'divCatalogoTarea','imgLupa2',event)"
								                        height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> <img id="imgLupa2" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogoTarea',0,'divCatalogoTarea','imgLupa2')"
								                        height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">&nbsp;Código</td>
						                        <td width="55%">&nbsp;&nbsp;<img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgDescripcion"
								                        border="0"> <map name="imgDescripcion">
								                        <area onclick="ordenarTabla(2,0)" shape="RECT" coords="0,0,6,5">
								                        <area onclick="ordenarTabla(2,1)" shape="RECT" coords="0,6,6,11">
							                        </map><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogoTarea',1,'divCatalogoTarea','imgLupa1',event)"
								                        height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogoTarea',1,'divCatalogoTarea','imgLupa1')"
								                        height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> &nbsp;Descripción</td>
						                        <td width="15%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgRtado"
								                        border="0"> <map name="imgRtado">
								                        <area onclick="ordenarTabla(3,0)" shape="RECT" coords="0,0,6,5">
								                        <area onclick="ordenarTabla(3,1)" shape="RECT" coords="0,6,6,11">
							                        </map>Resultado</td>
						                        <td width="15%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgFecha"
								                        border="0"> <map name="imgFecha">
								                        <area onclick="ordenarTabla(4,0)" shape="RECT" coords="0,0,6,5">
								                        <area onclick="ordenarTabla(4,1)" shape="RECT" coords="0,6,6,11">
							                        </map>Avance
						                        </td>
					                        </tr>
				                        </table>
				                        <div id="divCatalogoTarea" style="overflow-x: hidden; overflow-y: auto; width: 846px; height: 320px" align="left" runat="server">
    <%--                                        <div id="divCatalogoTarea2" style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:830px">--%>
                                            <div id="divCatalogo" style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:830px">
                                                <%=strTablaCatalogo%>
                                            </div>					                    
				                        </div>
				                        <table id="TBLFIN" style="width: 830px;height:17px">
					                        <tr class="textoResultadoTabla">
						                        <td>&nbsp;</td>
					                        </tr>
				                        </table>
			                        </td>
		                        </tr>
	                            <tr>
	                                <td>
	                                    <img style="height: 6px" src="../../Images/imgSeparador.gif" align="left">
	                                </td>
	                            </tr>		                
				                <tr>
					                <td>
					                    <center>
			                                <table style="width:350px; margin-top:10px;">
				                                <tr>
					                                <td align="center">
						                                <button id="btnNuevaTarea" type="button" onclick="nuevaTarea();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							                                 onmouseover="se(this, 25);mostrarCursor(this);" style="margin-left:10px; margin-top:5px;">
							                                <img src="../../Images/Botones/imgNuevo.gif" /><span title='Añade una nueva tarea'>Añadir</span>
						                                </button>
					                                </td>
					                                <td align="center">
						                                <button id="btnEliminarTarea" type="button" onclick="eliminarTarea();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							                                 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
							                                <img src="../../Images/botones/imgEliminar.gif" /><span title="Elimina la tarea seleccionada">Eliminar</span>
						                                </button>
					                                </td>
				                                </tr>
			                                </table>
			                            </center>	
					                </td>
				                </tr>					  
	                        </table>
                        </center>
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">   
                    <!-- Pestaña 4 // Documentación-->	
                    <br>
                     <table width="100%">
	                    <tr>
	                        <td colspan="6">
	                            &nbsp;<bold>Documentos relacionados con el área</bold>
	                        </td>
	                    </tr>                 
				 
                        <tr>
	                        <td colspan="6">
		                    <table style="width: 850px; height: 17px">
                                <colgroup>
                                    <col style="width:305px" />
                                    <col style="width:200px" />
                                    <col style="width:210px" />
                                    <col style="width:135px" />
                                </colgroup>			                
                                <tr class="TBLINI">
				                    <td>&nbsp;Descripción</td>
				                    <td>Archivo</td>
				                    <td>Link</td>
				                    <td>Autor</td>
			                    </tr>
		                    </table>
		                    <div id="divCatalogoDocArea" style="overflow-x: hidden; overflow-y: auto; width: 866px; height:120px" runat="server">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:850px">
		                        <%=strTablaCatalogoDocArea%>
		                        </div>    
                            </div>
		                    <table style="width: 850px; height: 17px">
			                    <tr class="TBLFIN">
				                    <td></td>
			                    </tr>
		                    </table>
		                    <br />
                            </td>
                    </tr>
	                <tr>
	                    <td colspan="6">
	                        <img style="height: 6px" src="../../Images/imgSeparador.gif" align="left">
	                    </td>
	                </tr>		                
                    </table>
                    <table width="100%">
	                    <tr>
	                        <td colspan="6">
	                            &nbsp;<bold>Documentos relacionados con la orden</bold>
	                        </td>
	                    </tr>                 
                        <tr>
	                        <td colspan="6">
		                    <table style="width: 850px; height: 17px">
                                <colgroup>
                                    <col style="width:305px" />
                                    <col style="width:200px" />
                                    <col style="width:210px" />
                                    <col style="width:135px" />
                                </colgroup>			                
                                <tr class="TBLINI">
                                    <td>&nbsp;Descripción</td>
				                    <td>Archivo</td>
				                    <td>Link</td>
				                    <td>Autor</td>
			                    </tr>
		                    </table>
		                    <div id="divCatalogoDoc" style="overflow-x: hidden; overflow-y: auto; width: 866px; height:120px" runat="server">
                                <div id="divCatalogoDoc2" style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:850px">
		                        <%=strTablaCatalogoDoc%>
		                        </div>    		                
                            </div>
		                    <table style="width: 850px; height: 17px">
			                    <tr class="textoResultadoTabla">
				                    <td></td>
			                    </tr>
		                    </table>
		                    <br />
                            </td>
                    </tr>
	                <tr>
	                    <td colspan="6">
	                        <img style="height: 6px" src="../../Images/imgSeparador.gif" align="left">
	                    </td>
	                </tr>		                
				    <tr id="PieDocumentacion" style="visibility:visible">
					    <td width="10%">&nbsp;</td>
					    <td width="20%">&nbsp;
					    </td>
                        <td width="20%">
						    <button id="btnNuevoDocumentacion" type="button" onclick="nDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							     onmouseover="se(this, 25);mostrarCursor(this);" style="margin-left:10px; margin-top:5px;">
							    <img src="../../Images/Botones/imgNuevo.gif" /><span title='Añade un nuevo elemento'>Añadir</span>
						    </button>					
                        </td>
                        <td width="20%">
						    <button id="btnEliminarDocumentacion" type="button" onclick="eDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
							    <img src="../../Images/botones/imgEliminar.gif" /><span title="Elimina eDoc">Eliminar</span>
						    </button>					
                        </td>					
					    <td width="20%">&nbsp;</td>
					    <td width="10%">&nbsp;</td>
				    </tr>
                    </table>
                    <iframe id="iFrmSubida" name="iFrmSubida" frameborder="no" width="10px" height="1px" style="visibility:hidden" ></iframe>
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">   
                     <!-- Pestaña 5 // Planificación/Cronología -->	
                    <br /><br />
                    <center>
                    <table id="tbCrono" style="width:100%;text-align:left" runat="server">
					    <tr>
						    <td valign="top" width="35%">									
							    <fieldset class="fld" style="width:290px">
							    <legend class="Tooltip" title="Previsión">&nbsp;Previsión&nbsp;</legend>																			
							    <table width="90%" cellPadding="7">
								    <tr>
									    <td width="30%">Fecha inicio
									    </td>
									    <td width="70%">
                                            <asp:textbox id="txtFechaInicioPrevista" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura=0></asp:textbox>									
									    </td>
								    </tr>
								    <tr>
									    <td width="30%">Fecha fin
									    </td>
									    <td width="70%">
                                            <asp:textbox id="txtFechaFinPrevista" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura=0></asp:textbox>									
									    </td>
									
								    </tr>
								    <tr>
									    <td width="30%">Esfuerzo
									    </td>
									    <td width="70%">
										    <asp:textbox onkeypress="javascript:if ($I('txtTiempoEstimado').readOnly==false){ActivarGrabar();validarNumero(this,6,2)}" onblur="formatearNumeroSalir(this,2)"
											    onkeyup="formatearNumero(this,5,2)" id="txtTiempoEstimado" runat="server" Width="60px" CssClass="textareaNumero"
											    MaxLength="9"></asp:textbox>
                                        </td>
								    </tr>
								    <tr>
									    <td width="30%">Unidad
									    </td>
									    <td width="70%">
										    <asp:DropDownList id="cboUnidadEstimacion" runat="server" Width="80px" AutoPostBack="false" CssClass="combo"
											    onchange="javascript:ActivarGrabar();">
											    <asp:ListItem Value="1" Selected="True">Jornadas</asp:ListItem>
											    <asp:ListItem Value="2">Horas</asp:ListItem>
										    </asp:DropDownList>
									    </td>
								    </tr>
							    </table>
							    </fieldset>
							    <br /><br />
						        <fieldset class="fld" style="width:290px">
							    <legend class="Tooltip" title="Realización">&nbsp;Realización&nbsp;</legend>																			
							    <table width="90%" cellpadding="7">
								    <tr>
									    <td width="30%">Fecha inicio</td>
									    <td width="70%">
                                            <asp:textbox id="txtFechaInicioReal" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura=0></asp:textbox>									
									    </td>
								    </tr>
								    <tr>
									    <td width="30%">Fecha fin</td>
									    <td width="70%">
                                            <asp:textbox id="txtFechaFinReal" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura=0></asp:textbox>									
									    </td>
								    </tr>
								    <tr>
									    <td width="30%">Esfuerzo</td>
									    <td width="70%">
										    <asp:textbox onkeypress="if ($I('txtTiempoInvertido').readOnly==false){ActivarGrabar();validarNumero(this,6,2)}" onblur="formatearNumeroSalir(this,2)"
											    onkeyup="formatearNumero(this,5,2)" id="txtTiempoInvertido" runat="server" Width="60px" CssClass="textareaNumero"
											    MaxLength="9"></asp:textbox>
									    </td>
								    </tr>
							    </table>	
							    </fieldset>	
							    <br /><br />
							    <fieldset class="fld" style="width:290px">
								    <legend class="Tooltip" title="Última modificación">&nbsp;Última modificación&nbsp;</legend>	
									    <table class="texto" width="100%" border="0" cellPadding="7" align="left">
										    <tr id='usu_modif' class='label' align="left">
											    <td width='100%'>&nbsp;Usuario:&nbsp;
												    <asp:label id="lblUsuario" runat="server" CssClass="label" Visible="True"></asp:label></td>
										    </tr>
										    <tr id='fecha_modif' class='label' align="left">
											    <td width='100%'>&nbsp;Fecha:&nbsp;
												    <asp:label id="lblFUM" runat="server" CssClass="label" Visible="True"></asp:label></td>
										    </tr>
									    </table>
							    </fieldset>																			
						    </td>
						
						    <td valign="top" width="65%">																									
							    <fieldset class="fld" style="width:560px" >
								    <legend class="Tooltip" title="Cronología">&nbsp;Cronología&nbsp;</legend>	
					
								    <table style="width:540px">
                                    <colgroup><col style="width:120px"/><col style="width:130px" /><col style="width:290px" /></colgroup>
									    <tr class="TBLINI">
										    <td>&nbsp;Estado
										    </td>
										    <td>
											    Fecha
										    </td>
										    <td>  
											    Usuario
										    </td>										
									    </tr>
								    </table>
	
				                    <div id="divCronologia" style="overflow-x: hidden; overflow-y: auto; width: 556px; height: 314px"
					                    align="left"  runat="server">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:540px">
					                        <%=strTablaHtmlCronologia%>
                                        </div>
				                    </div>
						
								    <table id="pieTabla" style="width:540px">
									    <tr style="height:17px" class="TBLFIN">
										    <td>
										    </td>
									    </tr>
								    </table>  
							    </fieldset>		
						    </td>
					    </tr>
 				    </table>
                   </center>
                </eo:PageView>      
                <eo:PageView CssClass="PageView" runat="server">   
				    <!-- Pestaña 6 // Observaciones-->
	                    <table style="width:98%;text-align:left" cellpadding="3px">
						    <tr>
						    <td>&nbsp;&nbsp;Observaciones&nbsp;<br />
							    <asp:textbox onKeyUp="javascript:if ($I('txtObservaciones').readOnly==false) ActivarGrabar();"
							    id="txtObservaciones" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="9">
							    </asp:textbox>									
						    </td>	
						    </tr>																						            		                
						    <tr>
						    <td>&nbsp;&nbsp;Solicitud de aclaraciones&nbsp;<br />
							    <asp:textbox onKeyUp="javascript:if ($I('txtSolAclar').readOnly==false) ActivarGrabar();"
							    id="txtSolAclar" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="9">
							    </asp:textbox>									
						    </td>	
						    </tr>	
						    <tr>
						    <td>&nbsp;&nbsp;Aclaraciones&nbsp;<br />
							    <asp:textbox onKeyUp="javascript:if ($I('txtAclara').readOnly==false) ActivarGrabar();"
							    id="txtAclara" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="9">
							    </asp:textbox>									
						    </td>	
						    </tr>													
	                    </table>
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">   
								
				    <!-- Pestaña 7 // Pruebas -->
				
	                    <table class="texto" width="98%" cellSpacing=0 cellPadding=0 width="100%" align=center>
						    <tr>
						    <td>&nbsp;&nbsp;Descripción&nbsp;<br />
							    <asp:textbox onKeyUp="javascript:if ($I('txtPruebas').readOnly==false) ActivarGrabar();"
							    id="txtPruebas" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="30">
							    </asp:textbox>									
						    </td>	
						    </tr>																						            		                											
	                    </table>
                </eo:PageView>					
               </eo:MultiPage>             
                 </td>
            </tr>
        </table>	
    <br />
    </center>
    <center>
		    <table width="100%" cellSpacing="0" border="0">
		    <tr id="Pie" style="visibility:visible">
			    <td width="13%" align="center">&nbsp;</td>
			    <td width="15%" align="center">
				    <div id="divbtnAnular" style="display:none">
					    <button id="btnAnular" type="button" onclick="Anular();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
						    <img src="../../Images/botones/imgAnular.gif" /><span title="Pasa a estado anulada la orden">Anular</span>
					    </button>
				    </div>				
				    <div id="divbtnEliminar" style="display:none">								
					    <button id="btnEliminar" type="button" onclick="Eliminar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
						    <img src="../../Images/botones/imgEliminar.gif" /><span title="Elimina la orden">Eliminar</span>
					    </button>
				    </div>	
				    <div id="divbtnAbrir" style="display:none">							    
					    <button id="btnAbrir" type="button" onclick="Abrir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
						    <img src="../../Images/botones/imgAbrir.gif" /><span title="Abre una orden ya cerrada">Abrir</span>
					    </button>	
				    </div>								  												  
			    </td>
			    <td width="15%" align="center">
				    <div id="divbtnPDF" style="display:block">	
					    <button id="btnPDF" type="button" onclick="Exportar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgPDF.gif" /><span title="Genera un documento PDF con los datos de la orden">Exportar</span>
					    </button>				    										   
					    </div>	
			    </td>					
			    <td width="15%" align="center">
				    <div id="divbtnGrabar" style="display:block">	
					    <button id="btnGrabar" type="button" onclick="grabar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgGrabar.gif" /><span title="Graba la información">Grabar</span>
					    </button>	
				    </div>
				    <div id="divbtnAprobar" style="display:none">	
					    <button id="btnAprobar" type="button" onclick="Aprobar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgAprobar.gif" /><span title="Aprueba la resolución de la orden">Aprobar</span>
					    </button>	
				    </div>	
				    <div id="divbtnPropuestaOK" style="display:none">						    						
					    <button id="btnPropuestaOK" type="button" onclick="Propuesta()" class="btnH25W90" runat="server" hidefocus="hidefocus"
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgPropuestaOK.gif" /><span title="Acepta la propuesta presentada">Aceptar</span>
					    </button>
				    </div>							    
                    <div id="divbtnAparcar" style="display:none">									    
					    <button id="btnAparcar" type="button" onclick="Aparcar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgAparcar.gif" /><span title="Aparca la orden">Aparcar</span>
					    </button>							  
				    </div>							    
			    </td>
			    <td width="15%" align="center">
				    <div id="divbtnGrabarSalir" style="display:block">	
					    <button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgGrabarSalir.gif" /><span title="Graba la información y regresa a la pantalla">Grabar</span>
					    </button>
				    </div>
				    <div id="divbtnTramitar" style="display:none">
					    <button id="btnTramitar" type="button" onclick="Tramitar()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgTramitar.gif" /><span title="Tramita la orden y regresa a la pantalla anterior">Tramitar</span>
					    </button>
                    </div>	
                    <div id="divbtnRechazar" style="display:none">					    						
					    <button id="btnRechazar" type="button" onclick="Rechazar()" class="btnH25W110" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgRechazar.gif" /><span title="Rechaza como resuelta la orden">No aprobar</span>
					    </button>
				    </div>
				    <div id="divbtnPropuestaNO" style="display:none">	
					    <button id="btnPropuestaNO" type="button" onclick="Anular()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							    onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../images/botones/imgAnular.gif" /><span title="Rechaza la propuesta presentada">Anular</span>
					    </button>
                    </div>						    							  
			    </td>
			    <td width="15%" align="center">
				    <button id="btnSalir" type="button" onclick="salir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
						    onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../images/botones/imgSalir.gif" /><span title="Regresa a la pantalla anterior">Salir</span>
				    </button>				
			    </td>
			    <td width="12%" align="center">&nbsp;</td>					
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
