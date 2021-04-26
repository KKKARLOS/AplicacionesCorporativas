<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Tarea_Default" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de tarea</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../Javascript/documentos.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?20180522" type="text/Javascript"></script>
	<script src="Functions/RecursoTarea.js" type="text/Javascript"></script>
    <script src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
  	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
  	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>    
</head>
<body onload="init()" onunload="unload()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">
        #tsPestanas table { table-layout:auto; }
        #tsPestanasProf  table { table-layout:auto; }
        #tsPestanasAvanza  table { table-layout:auto; }
    </style>
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
        var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
        var nCR = "<% =nCR %>";
        //Nos indica si hemos entrado para crear registro (C) o para buscar uno existente (B)
        var gsModo = "<%=gsModo%>";        
        //Variables a devolver a la estructura.
        var sDescripcion = "";
        var sFIPL = "";
        var sFFPL = "";
        var sETPL = "";
        var sPresupuesto = "";
        var sEstado = "";
        var sFIV = "";
        var sFFV = "";
        var sFacturable = "";
        //nuevas variables a devolver al gantt
        var sETPR = "";
        var sFFPR = "";
        var sAvanR = "";
        var sAutomatico = "";
        var aVAE_js = new Array();
        var aOrigen_js = new Array();
        //Para saber siadmite recursos desde PST
        var sRecPST = "<% =sRecPST %>";
        //Para el comportamiento de los calendarios
        var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
        //Para sacar el nº de empleaqdos del nodo en la subpestaña Pool de la pestaña Profesionales
        var gsNumEmpleadosNodo = "<% =gsNumEmpleadosNodo %>";

        //SSRS
        var servidorSSRS ="<%=Session["ServidorSSRS"]%>"; 
        var nodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>";
        var t314_idusuario_autor = "<%=Session["UsuarioActual"]%>";
        //SSRS

    </script>
    <script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
<table id="tabla" style="width:920px; text-align:left; margin-left:9px; margin-top:5px;">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="918px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Profesionales" Width="110"></eo:TabItem>
							<eo:TabItem Text-Html="Avanzado" Width="120"></eo:TabItem>							
							<eo:TabItem Text-Html="Notas" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Control" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Documentación" Width="110"></eo:TabItem>
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="560px">
				<eo:PageView ID="PageView1" CssClass="PageView" runat="server">	
				<!-- Pestaña 1 General-->
                <table style="width:850px; text-align:left; margin-left:15px;">
                <colgroup>
                    <col style="width:470px"/>
                    <col style="width:380px"/>
                </colgroup>
                    <tr>
	                    <td colspan="2">	
							<fieldset style="width:845px; margin-left:4px;">
								<legend>Estructura</legend>
                                <table class="texto" style="width:100%;" cellpadding="5px" cellspacing="4px">
                                    <tr>
                                        <td>
                                            <label id="lblProy" class="enlace" style="width:85px;height:17px" onclick="obtenerProyectos()">Proyecto</label>
					                        <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" style="vertical-align:middle;" />
                                            <asp:TextBox ID="txtNumPE" runat="server" SkinID="numero" style="width:50px" readonly="true" onchange="obtenerTarifas()"/>
                                            <asp:TextBox ID="txtPE" runat="server" style="width:252px" readonly="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <label id="lblFase" class="enlace" style="width:60px;height:17px" onclick="obtenerFases()">Fase</label>
                                            <asp:TextBox ID="txtFase" runat="server" style="width:300px" readonly="true" />
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarFase();" style="cursor:pointer; vertical-align:text-bottom;" /><br />
                                            <label id="lblPT" class="enlace" style="width:105px;height:17px" onclick="obtenerPTs()">Proyecto técnico</label>
                                            <asp:TextBox ID="txtPT" runat="server" style="width:308px" readonly="true" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <label id="lblActividad" class="enlace" style="width:60px;height:17px" onclick="obtenerActividades()">Actividad</label>
                                            <asp:TextBox ID="txtActividad" runat="server" style="width:300px" readonly="true" />
                                            <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarActividad();" style="cursor:pointer; vertical-align:text-bottom;" />
                                        </td>
                                    </tr>
                                </table>
                        </fieldset>
                        <br />
	                    </td>
                    </tr>
                    <tr>
	                    <td colspan="2" style="vertical-align:top;">
	                    <table style="width:850px">
	                        <colgroup>
                                <col style="width:680px"/>
                                <col style="width:170px"/>
                            </colgroup>
                            <tr>
                                <td>
						            <fieldset style="width:670px; margin-left:4px; height:50px; vertical-align:bottom;">
							            <legend>Identificación de tarea</legend>
                                            <table style="width:670px" border="0">
                                            <colgroup><col style="width:220px"/><col style="width:405px"/><col style="width:45px"/></colgroup>
                                                <tr>
                                                    <td valign="top">
                                                            <label id="lblTarea" class="texto" style="width:45px; margin-left:4px;">Número</label>
                                                            <asp:TextBox ID="txtIdTarea" runat="server" MaxLength="8" SkinID="numero" style="width:60px;" onkeypress="if(event.keyCode==13){event.keyCode=0;obtenerTareas2();}else{vtn2(event);}"></asp:TextBox>
                                                            <label id="lblDesTarea" class="enlace" style="width:75px; height:17px; margin-left:10px;" onclick="obtenerTareas()">Denominación</label>
                                                        <br />
                                                        <label title="Indica si los consumos de la tarea serán considerados como horas complementarias" style="margin-left:4px;">Horas complementarias</label>
                                                        <asp:CheckBox ID="chkHorasComplementarias" runat="server" style="cursor:pointer; vertical-align:middle;" onClick="aG(0);" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDesTarea" MaxSize="100" SkinID="multi" runat="server" TextMode="MultiLine" Rows="2" style="width:400px;height:35px;display:inline-block;float:right;margin-top:-11px;" onKeyUp="aG(0);" onKeyPress="teclaDenominacion();Count(this,100);" onChange="javascript:Count(this,100);" ></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img id="btnBitacora" style="margin-left:10px" src="../../../Images/imgBTTN.gif" border="0"  title="Sin acceso a la bitácora de tarea" onclick="mostrarBitacora();" />
                                                    </td>
                                                </tr>
                                            </table>
                                    </fieldset>
                                </td>
                                <td>
							        <fieldset style="width:152px; margin-left:15px; height:53px; vertical-align:top;">
								        <legend>Facturable <asp:CheckBox ID="chkFacturable" runat="server" style="cursor:pointer; vertical-align:middle;" onClick="if (!this.checked){$I('cboModoFacturacion').value=''};aG(0);" /></legend>
								            <table>
								                <tr>
								                    <td>
								                        <asp:DropDownList ID="cboModoFacturacion" runat="server" onchange="if (this.value!=''){$I('chkFacturable').checked=true;};aG(0);" style="width:130px; margin-left:5px; margin-top:5px;" AppendDataBoundItems=true>
								                        <asp:ListItem Value="0" Text="" Selected="True"></asp:ListItem>
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
	                    <td colspan="2">
                            <br />
                            &nbsp;&nbsp;Descripción<br />
                            <asp:TextBox ID="txtDescripcion" SkinID="multi" runat="server" TextMode="MultiLine" Rows="4" style="width:850px; margin-left:5px;" onKeyUp="aG(0);"></asp:TextBox>
	                    </td>
                    </tr>
                    <tr>
                        <td style="padding-top:5px;">
	                        <table style="width:470px;">
	                        <colgroup>
	                            <col style="width:260px;" />
	                            <col style="width:210px;" />
	                        </colgroup>
	                        <tr>
	                            <td>
							        <fieldset style="width:235px; margin-left:4px; height:35px; vertical-align:top;">
								        <legend>Vigencia</legend>
                                        <table style="width:100%" cellpadding="5px">
                                            <tr>
                                                <td>
                                                    Inicio
                                                    <asp:TextBox ID="txtValIni" runat="server" style="width:60px;cursor:pointer; margin-right:23px;" Calendar="oCal" onchange="aG(0);controlarFecha('VI');" goma="0" ></asp:TextBox>
                                                    Fin
                                                    <asp:TextBox ID="txtValFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" cRef="txtValIni" onchange="aG(0);controlarFecha('VF');" ></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
	                            </td>
	                            <td>                                     
						            <fieldset style="width:190px; vertical-align:top; height:35px;">
							            <legend>Estado</legend>
                                        <table style="width:100%" cellpadding="5px">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="cboEstado" runat="server" onchange="controlEstado(this.value);aG(0);" style="width:80px">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;<span id="spanImpIAP" style="visibility:hidden">
                                                    <asp:CheckBox ID="chkImpIAP" runat="server" style="cursor:pointer" onClick="aG(0);" Text="Imputar IAP" Width="80" TextAlign=Left ToolTip="Permite realizar imputaciones en IAP aunque la tarea esté finalizada o cerrada, dependiendo siempre del periodo de vigencia." /></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
	                            </td>
                            </tr> 
                            </table>                                    
                        </td>
                        <td style="padding-top:5px;">
						    <fieldset style="width:380px; vertical-align:top; height:35px">
							    <legend>Control límite de esfuerzos</legend>
                                <table style="width:370px;" cellpadding="5px">
                                    <colgroup><col style="width:170px;"/><col style="width:100px;"/><col style="width:100px;"/></colgroup>
                                    <tr>
                                        <td>
                                            Límite (horas)
                                            <asp:TextBox ID="txtCLE" runat="server" CssClass="txtNumM" SkinID="numero"  style="width:60px" onfocus="fn(this,7,2);" onkeyup="aG(0);"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdbCLE" runat="server" RepeatDirection="Horizontal" SkinID="rbl" Width="100px" onclick="setInformativos();aG(0);">
                                                <asp:ListItem Selected="True" Value="B" Text="Bloq."></asp:ListItem>
                                                <asp:ListItem Value="I" Text="Inf."></asp:ListItem>                                                
                                            </asp:RadioButtonList>       
                                        </td>
                                        <td>                                 
                                            <asp:DropDownList id="cboInformativo" runat="server" Width="100px" onchange="aG(0);">
                                                <asp:ListItem Selected="True" Value="I" Text="Diferido"></asp:ListItem>
                                                <asp:ListItem Value="X" Text="Inmediato"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                    <tr>
	                    <td style="padding-top:5px;">
							<fieldset style="width:447px; margin-left:4px; vertical-align:top; height:35px;">
								<legend>Planificación</legend>
                                <table style="width:420px" cellpadding="5px">
                                    <tr>
                                        <td>
                                            Inicio
                                            <asp:TextBox ID="txtPLIni" runat="server" style="width:60px;cursor:pointer; margin-right:23px;" Calendar="oCal" onchange="calcularDesvPlazo();aG(0);controlarFecha('I');" ></asp:TextBox>
                                            Fin
                                            <asp:TextBox ID="txtPLFin" runat="server" style="width:60px;cursor:pointer; margin-right:33px;" Calendar="oCal" cRef="txtPLIni" onchange="calcularDesvPlazo();aG(0);controlarFecha('F');" ></asp:TextBox>
                                            Horas
                                            <asp:TextBox ID="txtPLEst" runat="server" CssClass="txtNumM" SkinID="numero" style="width:70px" onfocus="fn(this,7,2);" onkeyup="calcularPorcentajes();aG(0);" onchange="actuPrev();"></asp:TextBox>
                                       </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="padding-top:5px;">
	                            <table style="width:370px; vertical-align:top; height:35px;">
	                            <colgroup>
	                                <col style="width:240px;" />
	                                <col style="width:130px;" />
	                            </colgroup>
	                            <tr>
	                                <td>                        
							            <fieldset style="width:220px; vertical-align:top; height:37px;">
								            <legend>Previsión <img id="imgHistorial" src="../../../images/imgHistorial.gif" style="cursor:pointer; vertical-align:middle;" runat="server" onclick="getHistoriaPR()" title="Historial de previsiones" /></legend>
                                            <table style="width:100%" cellpadding="5px">
                                                <tr>
                                                    <td>
                                                        Fin
                                                        <asp:TextBox ID="txtPRFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG(0);calcularDesvPlazo2();"  />
                                                        &nbsp;&nbsp;&nbsp;Horas
                                                        <asp:TextBox ID="txtPREst" runat="server" MaxLength="8" SkinID="numero" style="width:70px" onFocus="fn(this,7,2);" onKeyUp="aG(0);calcAvanPrev();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td>
							            <fieldset style="width:139px; vertical-align:top; height:35px;">
								            <legend>Acceso Bitácora IAP</legend>
                                            <table style="width:100%" cellpadding="5px">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="cboAccesoIAP" runat="server" style="width:100px;" onchange="aG(0);">
                                                            <asp:ListItem Value="X" Text="Sin acceso" Selected></asp:ListItem>
                                                            <asp:ListItem Value="E" Text="Escritura"></asp:ListItem>
                                                            <asp:ListItem Value="L" Text="Lectura"></asp:ListItem>
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
                        <td style="padding-top:5px; vertical-align:top;" rowspan="2">
							<fieldset id="idFieldsetIAP" style="width:447px; margin-left:4px;">
								<legend>IAP</legend>
                                <table class="texto" style="width:440px" cellpadding="5px" cellspacing="0px">
                                    <colgroup>
                                        <col style="width:125px"/><col style="width:75px; text-align:right;" /><col style="width:20px" />
                                        <col style="width:130px" /><col style="width:80px;" /><col style="width:10px;" />
                                    </colgroup>
                                    <tr>
                                        <td>Primer consumo</td>
                                        <td><asp:TextBox ID="txtPriCon" style="width:60px; margin-left:10px;" runat="server" readonly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td>Fin estimado</td>
                                        <td><asp:TextBox ID="txtFinEst" style="width:60px; margin-left:10px;" runat="server" readonly="true" /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Último consumo</td>
                                        <td><asp:TextBox ID="txtUltCon" style="width:60px; margin-left:10px;" runat="server" readonly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td>Total estimado (horas)</td>
                                        <td><asp:TextBox ID="txtTotEst" SkinID="numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Consumido (horas)</td>
                                        <td><asp:TextBox ID="txtConHor" SkinID="numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td>Pte. estimado (horas)</td>
                                        <td><asp:TextBox ID="txtPteEst" SkinID="numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Consumido (jornadas)</td>
                                        <td><asp:TextBox ID="txtConJor" SkinID="numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td>Avance estimado</td>
                                        <td style="text-align:right;">
                                            <asp:TextBox ID="txtAvanEst" style="width:50px; margin-left:20px;" runat="server" SkinID="Numero" readonly="true" />
                                        </td>
                                        <td style="padding-left:0px;">%</td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="padding-top:5px; vertical-align:top;">
							<fieldset id="idFieldsetSituacion" style="width:380px;">
								<legend>Situación</legend>
                                <table style="width:365px" cellpadding="2px">
                                <colgroup>
                                    <col style="width: 110px" />
                                    <col style="width: 105px" />
                                    <col style="width: 80px"  />
                                    <col style="width: 70px"  />
                                </colgroup>
                                    <tr>
                                        <td title="Relación entre las horas consumidas y las horas totales previstas">Avance teórico</td>
                                        <td ><asp:TextBox ID="txtAvanTeo"  style="width:50px" runat="server" SkinID="numero" readonly="true" />%</td>
                                        <td title="Relación entre las horas consumidas y las horas totales planificadas">Consumido</td>
                                        <td><asp:TextBox ID="txtPorCon" style="width:50px" runat="server" SkinID="numero" readonly="true" />%</td>
                                    </tr>
                                    <tr>
                                        <td class="ocultarcapa" title="Calcula de forma automática el avance real, igualándolo al avance teórico.">Cálculo automático</td>
                                        <td class="ocultarcapa"><asp:CheckBox ID="chkAvanceAuto" runat="server" style="width:15px;cursor:pointer" onClick="clickAvanceAutomatico();" /></td>
                                        <td title="Relación entre las horas totales previstas y las horas totales planificadas">Desv. Esfuerzo</td>
                                        <td><asp:TextBox ID="txtPorDes" style="width:50px" runat="server" SkinID="numero" readonly="true" />%</td>
                                    </tr>
                                    <tr>
                                        <td class="ocultarcapa">Avance real</td>
                                        <td class="ocultarcapa"><asp:TextBox ID="txtAvanReal" SkinID="numero" style="width:50px" MaxLength="6" runat="server" onFocus="fn(this,5,2)" onKeyPress="pulsarTeclaAvanceReal(event);" onKeyUp="soltarTeclaAvanceReal();calcularProducido();" />%</td>
                                        <td title="Relación entre el fin de plazo planificado y el previsto">Desv. Plazo</td>
                                        <td><asp:TextBox ID="txtDesvPlazo" style="width:50px" runat="server" SkinID="numero" readonly="true" />%</td>
                                    </tr>
                                    <tr>
                                        <td class="ocultarcapa" title="Horas previstas menos horas consumidas">Pte. teórico (horas)</td>
                                        <td class="ocultarcapa" colspan="3">
                                            <asp:TextBox ID="txtPdteTeo" style="width:60px" runat="server" SkinID="numero" readonly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td class="ocultarcapa" colspan="2" style="vertical-align:top;">    
							<fieldset style="width:380px; margin-top:10px;">
								<legend>Importes (&euro;)</legend>
                                <table class="texto" cellpadding="3px" cellspacing="0px" style="margin-top:5px; width:325px;">
                                    <colgroup><col style="width:170px;"/><col style="width:210px;"/></colgroup>
                                    <tr>
                                        <td>Presupuesto
                                            <asp:TextBox ID="txtPresupuesto" runat="server" style="width:70px" SkinID="numero" onFocus="fn(this,7,2);"/>
                                        </td>
                                        <td>Producido
                                            <asp:TextBox ID="txtProducido" runat="server" style="width:70px" SkinID="numero" readonly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                </eo:PageView>

                <eo:PageView ID="PageView2" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 2 Técnicos-->
					<eo:TabStrip runat="server" id="tsPestanasProf" ControlSkinID="None" Width="904px" 
						MultiPageID="mpContenidoProf" 
						ClientSideOnLoad="CrearPestanasProf" 
						ClientSideOnItemClick="getPestana">
						<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							<Items>
									<eo:TabItem Text-Html="Asignación" ToolTip="Asignación de profesionales a la tarea" Width="100"></eo:TabItem>
									<eo:TabItem Text-Html="Pool" ToolTip="Profesionales que se asociarán a todas las tareas dependientes, actuales y futuras." Width="100"></eo:TabItem>
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
			        <eo:MultiPage runat="server" id="mpContenidoProf" CssClass="FMP" Width="100%" Height="520px">
                        <eo:PageView ID="PageView3" CssClass="PageView" runat="server" align="center">					        
			            <!-- Pestaña 2.1 Asignación-->	
			            <center>
                        <table style="text-align:left;margin-left:10px; width:100%;">
                            <tr>
                                <td>
						            <fieldset style="width:850px; height: 240px;">
							            <legend>Selección de profesionales</legend>
                                        <table style="width:100%" cellpadding="5px" > 
                                            <colgroup><col style="width:43%;"/><col style="width:6%;"/><col style="width:51%;"/></colgroup>                                            
                                            <tr style="height:45px">
                                                <td>
                                                    <asp:RadioButtonList Height="25px" ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionAmbito(this.id)">
                                                        <asp:ListItem Selected="True" Value="A" Text="Nombre&nbsp;&nbsp;&nbsp;" />
                                                        <asp:ListItem Value="C" Text="C.R.&nbsp;&nbsp;" />
                                                        <asp:ListItem Value="G" Text="Grupo funcional&nbsp;&nbsp;&nbsp;" />
                                                        <asp:ListItem Value="P" Text="Proyecto económico" />
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <span id="ambAp" style="display:block" class="texto">
                                                    <table style="width:345px;">
                                                        <colgroup><col style="width:115px;"/><col style="width:115px;"/><col style="width:115px;"/></colgroup>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;Apellido1</td>
                                                            <td>&nbsp;&nbsp;Apellido2</td>
                                                            <td>&nbsp;&nbsp;Nombre</td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:105px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                                                            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:105px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                                                            <td><asp:TextBox ID="txtNombre" runat="server" style="width:105px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                                                        </tr>
                                                    </table>
                                                    </span>
                                                    <span id="ambCR" style="display:none" class="texto">
                                                        <label id="lblCR" class="enlace" style="width:28px;height:17px" onclick="obtenerCR()">C.R.</label> 
                                                        <asp:TextBox ID="txtCR" runat="server" Width="325px" />
                                                    </span>
                                                    <span id="ambGF" style="display:none" class="texto">
                                                        <label id="lblGF" class="enlace" style="width:94px; height:17px" onclick="obtenerGF()">Grupo funcional</label> 
                                                        <asp:TextBox ID="txtGF" runat="server" Width="260px" readonly="true" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><!-- Relación de técnicos -->
                                                    <table id="tblTitRec" style="width:340px; height:17px">
		                                                <tr class="TBLINI">
				                                            <td style="text-align:left;padding-left:7px">Profesionales&nbsp;
					                                            <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblRelacion',1,'divRelacion','imgLupa1')"
						                                            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"/>
				                                                <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblRelacion',1,'divRelacion','imgLupa1',event)"
						                                            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"/> 
					                                        </td>
		                                                </tr>
	                                                </table>
		                                            <div id="divRelacion" style="overflow: auto; overflow-x: hidden; width:356px; height:100px" onscroll="scrollTablaProf()">
	                                                    <div style='background-image:url(../../../Images/imgFT20.gif); width:340px'>
	                                                    <table id="tblRelacion" style="width: 340px;">
	                                                    </table>
	                                                    </div>
                                                    </div>
	                                                <table style="width: 340px; height: 17px; margin-bottom:7px;">
		                                                <tr class="TBLFIN"><td></td></tr>
	                                                </table>
                                                </td>
                                                <td style="vertical-align:middle; text-align:center;">
						                            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="3"></asp:Image>
                                                </td>
                                                <td style="vertical-align:top;"><!-- Técnicos asignados -->
		                                            <table id="tblTitRecAsig" style="width:360px; height:17px">
		                                                <colgroup>
		                                                    <col style="width:340px"/>
		                                                    <col style="width:20px"/>
		                                                </colgroup>
			                                            <tr class="TBLINI">
				                                            <td style="text-align:left;padding-left:7px">Profesionales asignados&nbsp;
						                                        <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblAsignados',2,'divAsignados','imgLupa3')"
							                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					                                            <img id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblAsignados',2,'divAsignados','imgLupa3',event)"
							                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				                                            </td>
				                                            <td title="Finalización comunicada en IAP">FC</td>
			                                            </tr>
		                                            </table>
		                                            <div id="divAsignados" style="overflow:auto; overflow-x:hidden; width:376px; height:100px" target="true"  onmouseover="setTarget(this)" caso="1"  onscroll="scrollTablaProfAsig()">
		                                                <div style='background-image:url(../../../Images/imgFT20.gif); width:360px'>
		                                                <%=strTablaRecursos %>
		                                                </div>
                                                    </div>
	                                                <table style="width:360px; height:17px; margin-bottom:3px;">
		                                                <tr class="TBLFIN">
		                                                    <td></td>
		                                                 </tr>
	                                                </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top:4px;">
                                                    <img class="ICO" src="../../../Images/imgUsuPVM.gif" title="Del <%=sNodo%> del proyecto"/>Del <%=sNodo%>&nbsp;&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuNVM.gif" />De otro <%=sNodo %>&nbsp;&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuEVM.gif" />Externo&nbsp;&nbsp;
                                                    <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                                                    <label id="lblForaneo" runat="server">Foráneo</label>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label id="lblMostrarBajas" title="Muestra u oculta los profesionales de baja en la empresa o usuarios de baja en SUPER, identificándolos en rojo, o de baja en el proyecto.">Mostrar bajas</label>
                                                    <input type="checkbox" id="chkVerBajas" class="check" onclick="getRecursos();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Image id="imgDelBajaPE" style="CURSOR: pointer" ToolTip="Borra fecha de baja en el proyecto económico a los usuarios seleccionados." onclick="reAsignar()" runat="server" ImageUrl="../../../Images/imgVolverTrabajo.gif"></asp:Image>
                                                    &nbsp;&nbsp;&nbsp;<asp:Image id="imgCorreo" ToolTip="SUPER notifica la asignación de profesionales a la tarea, por parametrización a nivel de proyecto económico." runat="server" ImageUrl="../../../Images/imgCorreoHabilitado.gif"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <label id="lblInsPST" runat="server" style="color:Red; visibility:hidden;" title="No permite asignar profesionales desde PST, ajenos al proyecto económico.">No permite ajenos al P.E.</label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
	                            </td>
                            </tr>
                            <tr>
	                            <td>
							        <table style="width:100%">
                                        <tr>
	                                        <td style="width:440px; vertical-align:top; padding-top:4px;">
							                    <fieldset style="width:430px; height: 230px; ">
							                        <legend style="vertical-align:middle;">Información visible desde IAP.&nbsp;&nbsp;&nbsp;&nbsp;Email<input type=checkbox id="chkNotifProf" title="Notifica a profesionales su asignación a tarea y las indicaciones colectivas" runat="server" style="cursor:pointer; vertical-align:middle;" onClick="aG(0);" /></legend>
                                                    <table style="width:100%" cellpadding="5px">
                                                        <tr>
                                                            <td style="vertical-align:middle;">Perfil&nbsp;
                                                                <asp:DropDownList ID="cboTarifa" runat="server" style="vertical-align:middle;width:160px;" AppendDataBoundItems=True onchange="oRecActualizar('U', 'idTarifa', this);">
                                                                <asp:ListItem Selected Value="0" Text=""></asp:ListItem>
                                                                </asp:DropDownList><label style="vertical-align:middle;margin-left:23px;">Alerta sobreesfuerzo</label>
                                                                <input type=checkbox ID="chkNotifExceso" style="vertical-align:middle;" title="Notificar a responsables si el recurso imputa en IAP más horas de las previstas" runat="server" onclick="oRecActualizar('U', 'bNotifExceso', this);" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align:middle;">
                                                            Inicio <asp:TextBox ID="txtFIPRes" runat="server" style="width:60px;cursor:pointer; margin-right:20px; margin-left:3px" Calendar="oCal" onchange="oRecActualizar('U', 'fip', this);" />
                                                            Fin <asp:TextBox ID="txtFFPRes" runat="server" style="width:60px;cursor:pointer; margin-right:20px;" Calendar="oCal" onchange="oRecActualizar('U', 'ffp', this);" />
                                                            Horas <asp:TextBox ID="txtETPRes" runat="server" SkinID="numero" style="width:60px" onFocus="fn(this,7,2);" onKeyUp="oRecActualizar('U', 'etp', this);aG(0);" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Indicaciones individuales<br />
                                                                <asp:TextBox ID="txtIndicaciones" SkinID="multi" runat="server" TextMode="MultiLine" Rows="3" Width="420px" onKeyUp="oRecActualizar('U', 'indicaciones', this);" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Indicaciones colectivas<br />
                                                                <asp:TextBox ID="txtIndGen" SkinID="multi" runat="server" TextMode="MultiLine" Rows="3" Width="420px" onKeyUp="activarMensaje();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
						                    </td>    
						                    <td style="padding-left:10px; vertical-align:top; padding-top:4px; height: 165px;">
						                        <fieldset style="width:400px; height: 230px;">
							                        <legend>Información introducida en IAP</legend>
                                                        <table style="width:390px" cellpadding="5px">
                                                            <colgroup>
                                                                <col style="width:125px" />
                                                                <col style="width:70px" />
                                                                <col style="width:125px" />
                                                                <col style="width:70px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td>Total estimado (horas)</td>
                                                                <td><asp:TextBox ID="txtETPTec" SkinID="numero" runat="server" style="width:60px" readonly="true" /></td>
                                                                <td>Consumido (horas)</td>
                                                                <td><asp:TextBox ID="txtConTec" SkinID="numero" runat="server" style="width:60px" readonly="true" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Finalización estimada</td>
                                                                <td><asp:TextBox ID="txtFFPTec" runat="server" style="width:60px" readonly="true" /></td>
                                                                <td title="Pendiente estimado (horas)">Pte. estimado (horas)</td>
                                                                <td><asp:TextBox ID="txtEP" runat="server" SkinID="numero" style="width:60px" readonly="true" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Primera imputación</td>
                                                                <td><asp:TextBox ID="txtPriCons" runat="server" style="width:60px" readonly="true" /></td>
                                                                <td>Última imputación</td>
                                                                <td><asp:TextBox ID="txtUltCons" runat="server" style="width:60px" readonly="true" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td colspan="2"><asp:Label ID="lblCompletado" Text="" runat="server" />&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4"> 
                                                                    Comentario<br />
                                                                    <asp:TextBox ID="txtComentarios" SkinID="multi" runat="server" width="380px" TextMode="MultiLine" Rows="5" readonly="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </fieldset>
	                                        </td>
                                        </tr>
                                    </table>
	                            </td>
                            </tr>
                        </table>
                        <div id="divRecurso" style="z-index:0; position:absolute; left:33px; top:343px; width:435px; height:200px; background-image: url(../../../Images/imgFondoPixelado2.gif); background-repeat:no-repeat;">    
                        </div>
                        </center>
                        </eo:PageView>
                        <eo:PageView ID="PageView4" CssClass="PageView" runat="server" align="center">	
                        <!-- Pestaña 2.2 Pool de Grupos Funcionales y Técnicos-->
                        
                        <center>
                        <table style="width:860px;text-align:left;margin-top:20px">
                            <colgroup>
                                <col style="width:460px" />
                                <col style="width:400px" />
                            </colgroup>
                            <tr>
                                <td style="vertical-align:top;">
                                    <fieldset style="width:90%; height: 100px;"><legend>Asignación automática de profesionales</legend>
                                        <br />
                                        <asp:CheckBox ID="chkHeredaPE" runat="server" Text=" Proyecto económico" Width="100%" onClick="aG(0);" ToolTip="Asigna de forma automática a todos los profesionales presentes y futuros que estén asignados al proyecto económico."/>
                                        <br />
                                        <asp:CheckBox ID="chkHeredaCR" runat="server" Text=" Centro de responsabilidad " Width="100%" onClick="aG(0);" ToolTip="Asigna de forma automática a todos los profesionales presentes y futuros del <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>" />
                                        <br /><br /><span style="color:Red; font-size:9pt;">¡Atención! La activación de esta segunda opción, asignará de forma automática a la tarea a todos los profesionales pertenecientes al <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> del proyecto, que son <label id="lblNumEmp" style="width:40px;color:Blue; font-size:12pt;" ></label></span>
                                    </fieldset>
                                </td>
                                <td>
                                    <table style="width:370px; height:17px">
                                        <tr class="TBLINI">
                                            <td style="text-align:center;">Grupos funcionales</td>
                                        </tr>
                                    </table>
                                    <div id="divPoolGF" style="overflow: auto; overflow-x: hidden; width:386px; height:100px">
                                        <div style='background-image:url(../../../Images/imgFT16.gif); width:370px'>
                                        <%=strTablaPoolGF %>
                                        </div>
                                    </div>
                                    <table style="width:370px; height:17px">
                                        <tr class="TBLFIN"><td></td></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <center>
                                        <table style="width:250px;margin-top:10px; text-align:left;">
                                        <tr>
                                            <td>
		                                        <button id="btnAGF" type="button" onclick="obtenerGF3()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                                         onmouseover="se(this, 25);mostrarCursor(this);">
			                                        <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
		                                        </button>	
                                            </td>
                                            <td>
		                                        <button id="btnBGF" type="button" onclick="desasignarGF()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                                         onmouseover="se(this, 25);mostrarCursor(this);">
			                                        <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
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
                    </eo:MultiPage>
                </eo:PageView>
                
                <eo:PageView ID="PageView5" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 3 Avanzado (Atributos estadísticos, OTC...)-->
					<eo:TabStrip runat="server" id="tsPestanasAvanza" ControlSkinID="None" Width="904px" 
						MultiPageID="mpContenidoAvanza" 
						ClientSideOnLoad="CrearPestanasAvanza" 
						ClientSideOnItemClick="getPestana">
						<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							<Items>
									<eo:TabItem Text-Html="General" ToolTip="Asignación de atributos estadísticos." Width="100"></eo:TabItem>
									<eo:TabItem Text-Html="Campos" ToolTip="" Width="100"></eo:TabItem>
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
			        <eo:MultiPage runat="server" id="mpContenidoAvanza" CssClass="FMP" Width="100%" Height="520px">
                        <eo:PageView ID="PageView9" CssClass="PageView" runat="server" align="center">					        
			            <!-- Pestaña 3.1 Atributos estadísticos, OTC-->	
							<center>
							<table style="text-align:left; margin-left:10px; margin-top:5px; width:880px;">
								 <tr>
									<td>
										<fieldset style="width:850px; margin-left:4px; height:370px;">
											<legend>Criterios estadísticos técnicos</legend>
											<table style="width:800px" cellpadding="5">
											<colgroup>
												<col style="width:200px;"/>
												<col style="width:50px;"/>
												<col style="width:350px;"/>
												<col style="width:200px;"/>
											</colgroup>  
												<tr>
													<td colspan="4"><asp:CheckBox ID="chkCliente" runat="server" Text="Restringidos al cliente " onclick="restringir();" Width="800" style="cursor:pointer" Checked="true" />
													</td>
												</tr>
												<tr>
													<td style="vertical-align:top;">
														<table style="width: 180px; height: 17px">
															<tr class="TBLINI">
																<td style="padding-left:20px">
																	<nobr class='NBR' style='width:160px' onmouseover="TTip(event)">Definidos en el <%=sNodo%></nobr>
																 </td>
															</tr>
														</table>
														<div id="divAECR" style="overflow:auto; overflow-x:hidden; width:198px; height:288px">
															<div style='background-image:url(../../../Images/imgFT16.gif); width:180px'>
																<%=strTablaAECR %>
															</div>
														</div>
														<table style="width:180px; height:17px">
															<tr class="TBLFIN"> <td></td> </tr>
														</table>
													</td>
													<td style="vertical-align:middle; text-align:center;">
														<asp:Image id="imgPapeleraAE" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
													</td>
													<td style="vertical-align:top;">
														<table style="width:330px; height:17px">
															<colgroup>
																<col style="width:230px"/>
																<col style="width:100px"/>
															</colgroup>
															<tr class="TBLINI">
																<td align="center">Asociados a la tarea</td>
																<td align="left">Valor</td>
															</tr>
														</table>
														<div id="divAET" style="overflow: auto; overflow-x: hidden; width:348px; height:184px" target="true" onmouseover="setTarget(this);" caso="1">
															<div style='background-image:url(../../../Images/imgFT16.gif); width:330px'>
																<%=strTablaAET%>
															</div>
														</div>
														<table style="width: 330px;height: 17px">
															<tr class="TBLFIN"><td></td></tr>
														</table>

														<table style="width: 330px;height: 17px;margin-top:10px">
															<colgroup>
																<col style="width:230px"/>
																<col style="width:100px"/>
															</colgroup>		                                    
															<tr class="TBLINI">
																<td align="center">Heredados del proyecto técnico</td>
																<td align="left">Valor</td>
															</tr>
														</table>
														<div id="divAEPT" style="overflow: auto; overflow-x: hidden; width: 348px; height:60px">
															<div style='background-image:url(../../../Images/imgFT16.gif); width:330px'>
																  <%=strTablaAEPT%>
															</div>
														</div>
														<table style="width: 330px;height: 17px">
															<tr class="TBLFIN"><td></td></tr>
														</table>
													</td>
													<td style="vertical-align:top;">
														<table style="width: 180px; height: 17px">
															<tr class="TBLINI"><td align="center">Valores definidos</td></tr>
														</table>
														<div id="divAEVD" style="overflow: auto; overflow-x: hidden; width: 198px; height:288px">
															<div style="background-image: url(<%=Session["strServer"] %>Images/imgFT16.gif); width: 180px">
																<table id='tblAEVD' class='texto MA' style='width: 180px;'>
																</table>
															</div>
														</div>
														<table style="width: 180px; height: 17px">
															<tr class="TBLFIN"><td></td></tr>
														</table>
													</td>
												</tr>
											</table>
										</fieldset>
									</td>
								</tr>
								<tr>
									<td>
										<table style="width:880px;">
										<colgroup>
											<col style="width:445px;"/>
											<col style="width:435px;"/>
										</colgroup>
										<tr>
											<td>
												<fieldset style="width:420px; margin-left:4px; height:115px;">
													<legend>Orden de trabajo</legend>
													<table style="width:420px; text-align:left;" cellpadding="2px">
														<colgroup><col style="width:25px;" /><col style="width:395px;" /></colgroup>
														<tr>
															<td>
																<asp:Label ID="lblOTC" SkinID=enlace onclick="mostrarOTC();aGAvanza(0);" runat="server" Text="OTC" ToolTip="Orden de trabajo codificada" /></td>
															<td>
																<asp:TextBox ID="txtCodPST" runat="server" style="width:160px" readonly="true" title=""/>
																<asp:TextBox ID="txtDesPST" runat="server" style="width:195px" readonly="true" title=""/>
																<asp:Image ID="Image7" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="limpiarPST();aGAvanza(0);" style="cursor:pointer; vertical-align:middle;" />
															</td> 
														</tr>  
														<tr>
															<td colspan="2"> 
																<asp:Label ID="lblOtcH" Text="OTC heredada" runat="server" />
																<asp:TextBox ID="txtIdPST" runat="server" style="visibility:hidden" readonly="true" />
															</td>
														</tr>
														<tr>
															<td>
																<asp:Label ID="lbl" runat="server" Text="OTL" ToolTip="Orden de trabajo libre" />
															</td>
															<td>
																<asp:TextBox ID="txtOTL" MaxLength=25 runat="server" Width="358px" onKeyUp="aGAvanza(0);" />
															</td>
														</tr>
													</table>
											    </fieldset>
											</td>
											<td>
												<fieldset style="width:410px; height:115px;">
												<legend>Origen</legend>
													<table style="width:410px" cellpadding="5px">
														<colgroup>
															<col style="width:90px;" />
															<col style="width:320px;" />	                                        
														</colgroup>
														<tr>
															<td>Denominación</td>
															<td>
																<asp:DropDownList ID="cboOrigen" runat="server" Width="310px" AppendDataBoundItems=True onChange="aGAvanza(0);controlNotificable(this.value);">
																<asp:ListItem Selected Value="0" Text=""></asp:ListItem>
																</asp:DropDownList>
															</td>
														</tr>
														<tr>
															<td title="Incidencia/Petición">Inc./Petición</td>
															<td>
																<asp:TextBox ID="txtIncidencia" Width="310px" runat="server" onKeyUp="aGAvanza(0);" />
															</td>
														</tr>
														<tr>
															<td colspan="2">Notificar apertura y cierre <asp:CheckBox ID="chkNotificable" runat="server" Width="20px" style="cursor:pointer; vertical-align:middle;" onClick="aG(0);" />
															 </td>
														</tr>
													</table>
												</fieldset>
											</td>	                        
										</tr>
										</table>
									</td>
								</tr>
							</table>
						</center>
                        </eo:PageView>
                        <eo:PageView ID="PageView10" CssClass="PageView" runat="server" align="center">	
                        <!-- Pestaña 3.2 -->
                            <center>
                                <table style="text-align:left; width:870px; margin-top:15px;">
                                    <tr>
                                        <td>
					                        <!--<fieldset style="width:820px; margin-left:4px; height:370px;">
						                        <legend>Criterios estadísticos técnicos</legend>-->
                                                <table width="870px" cellpadding="5">
                                                <colgroup>
                                                    <col style="width:200px;"/>
                                                    <col style="width:50px;"/>
                                                    <col style="width:550px;"/>
                                                </colgroup>                                
                                                    <tr>
                                                        <td colspan="3"style="vertical-align:bottom;">
                                                            Ámbito&nbsp;
				                                                <asp:DropDownList ID="cboAmbito" runat="server" onchange="cargarCamposPorAmbito(this.value);" style="width:95px;" AppendDataBoundItems=true>
					                                                <asp:ListItem Value="0" Text="Empresarial"></asp:ListItem>
					                                                <asp:ListItem Value="1" Text="Privado"></asp:ListItem>
					                                                <asp:ListItem Value="2" Text="Proyecto"></asp:ListItem>
					                                                <asp:ListItem Value="3" Text="Cliente"></asp:ListItem>
					                                                <asp:ListItem Value="4" Text="C.R."></asp:ListItem>
					                                                <asp:ListItem Value="5" Text="Equipo"></asp:ListItem>						
					                                                <asp:ListItem Value="99" Text="Todos" Selected="True"></asp:ListItem>                            
				                                                </asp:DropDownList>			
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 180px; height: 17px">
	                                                            <tr class="TBLINI">
                                                                    <td style="padding-left:20px">
                                                                        <nobr class='NBR' style='width:160px' onmouseover="TTip(event)">Definidos</nobr>
                                                                     </td>
	                                                            </tr>
                                                            </table>
                                                            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:180px; height:200px">
                                                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:180px;">
                                                                <%=strTablaHTML%>
                                                                </div>
                                                            </div>
                                                                                                                        
                                                            <table style="width: 180px; height: 17px">
	                                                            <tr class="TBLFIN"> <td></td> </tr>                                                                
                                                            </table>

                                                        </td>
                                                        <td style="vertical-align:middle; text-align:center;">
					                                        <asp:Image id="imgPapeleraCampos" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                                        </td>
                                                        <td>                                                          
		                                                    <table style="width: 600px; height: 17px">
                                                            <colgroup>
                                                                <col style="width:20px" />
                                                                <col style="width:150px" />
                                                                <col style="width:60px" />
                                                                <col style="width:80px" />
                                                                <col style="width:290px" />
                                                            </colgroup>		                
			                                                    <tr class="TBLINI">
                                                                    <td></td>
				                                                    <td>&nbsp;Denominación</td>
                                                                    <td>Tipo</td>
				                                                    <td align="left">&nbsp;Valor</td>
                                                                    <td align="left">HH/MM/SS (sólo para campos de tipo Fecha/Hora)</td>
			                                                    </tr>
		                                                    </table>
		                                                    <div id="divCatalogoValores" style="overflow: auto; overflow-x: hidden; width: 600px; height:200px" target="true" caso="1"  runat="server">
		                                                        <div style='background-image:url(../../../Images/imgFT20.gif); width:600px' runat="server">
                                                                    <%=strTablaCampoValor%>
		                                                        </div>
                                                            </div>
	                                                        <table style="width:600px; height:17px">
                                                                <tr class="TBLFIN"><td></td></tr>
	                                                        </table>                                                               
                                                        </td>
                                                    </tr>
                                                </table>
                                        <!--</fieldset>-->
                                        <br />
                                        </td>
                                    </tr>
                                </table>
                                
                                
                                <button id="btnValores" type="button" onclick="MtoValores()" style="float: left;margin-left: 17px;margin-bottom:10px;" class="btnH25W200" runat="server" hidefocus="hidefocus" 
				                        onmouseover="se(this, 25);mostrarCursor(this);">
				                    <img src="../../../../../images/botones/imgEntrada.gif" /><span title="Accede al mantenimiento de campos a asociar a las tareas">Mantenimiento de campos</span>
			                    </button>	
                                 <table style="width:870px;text-align:left;margin-left:10px; margin-top:15px;">
                                    <tr>
                                        <td>
		                                    <table style="width: 870px; height: 17px">
                                            <colgroup>
                                                <col style="width:340px" />
                                                <col style="width:60px" />
                                                <col style="width:80px" />
                                                <col style="width:370px" />
                                            </colgroup>		                
			                                    <tr class="TBLINI">
				                                    <td>&nbsp;Heredados del proyecto técnico</td>
                                                    <td>Tipo</td>
				                                    <td align="left">&nbsp;Valor</td>
                                                    <td align="left">HH/MM/SS (sólo para campos de tipo Fecha/Hora)</td>
			                                    </tr>
		                                    </table>
		                                    <div id="divCamposPT" style="overflow: auto; overflow-x: hidden; width:870px; height:150px" runat="server">
		                                        <div style='background-image:url(../../../Images/imgFT20.gif); width:870px' runat="server">
                                                    <%=strTablaCamposPT%>
		                                        </div>
                                            </div>
	                                        <table style="width:870px; height:17px">
                                                <tr class="TBLFIN"><td></td></tr>
	                                        </table>
                                        </td>
                                    </tr>
                                </table>
                                   
                            </center>  

                                                   </eo:PageView>
                    </eo:MultiPage>
                </eo:PageView>
                
                <eo:PageView ID="PageView6" CssClass="PageView" runat="server">	
                <!-- Pestaña 4 Notas-->
                <table style="width:860px; text-align:left; margin-left:20px; margin-top:15px;">
                    <tr>
	                    <td style="text-align:right;">
	                        <asp:CheckBox ID="chkNotasIAP" runat="server" style="cursor:pointer" onClick="aG(3);" Text="Mostrar notas en IAP" Width="150" TextAlign=Left ToolTip="Muestra las notas desde el detalle de tarea de IAP, para que las puedan completar los técnicos." />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
	                    <td>
	                    &nbsp;&nbsp;Investigación / Detección
                            <asp:TextBox ID="txtNotas1" SkinID="multi" runat="server" TextMode="MultiLine" Rows="6" Width="850px" onKeyUp="aG(3);" />
                            <br /><br />
	                    &nbsp;&nbsp;Acciones / Modificaciones
                            <asp:TextBox ID="txtNotas2" SkinID="multi" runat="server" TextMode="MultiLine" Rows="6" Width="850px" onKeyUp="aG(3);" />
                            <br /><br />
	                    &nbsp;&nbsp;Pruebas
                            <asp:TextBox ID="txtNotas3" SkinID="multi" runat="server" TextMode="MultiLine" Rows="6" Width="850px" onKeyUp="aG(3);" />
                            <br /><br />
	                    &nbsp;&nbsp;Pasos a producción
                            <asp:TextBox ID="txtNotas4" SkinID="multi" runat="server" TextMode="MultiLine" Rows="6" Width="850px" onKeyUp="aG(3);" />
	                    </td>
                    </tr>
                </table>
                </eo:PageView>

                <eo:PageView ID="PageView7" CssClass="PageView" runat="server">	
                <!-- Pestaña 5 Control-->
                <table style="width:860px;text-align:left;margin-left:20px">
                    <tr>
	                    <td>
							<fieldset style="width:850px;">
								<legend>Datos de control</legend>
                                <table style="width:845px" cellpadding="5px">
                                    <colgroup>
                                        <col style="width:110px;"/>
                                        <col style="width:140px;"/>
                                        <col style="width:40px;"/>
                                        <col style="width:555px;"/>
                                    </colgroup>
                                    <tr>
	                                    <td>Apertura</td>
	                                    <td><asp:TextBox ID="txtFecAlta" runat="server" Width="110px" readonly="true" /></td>
	                                    <td>Usuario</td>
	                                    <td>
	                                        <asp:TextBox ID="txtIdUsuAlta" runat="server" Width="50px" SkinID="numero" readonly="true" />
	                                        <asp:TextBox ID="txtDesUsuAlta" runat="server" Width="480px" readonly="true" />
	                                    </td>
                                    </tr>
                                    <tr>
	                                    <td>Finalización</td>
	                                    <td><asp:TextBox ID="txtFecFin" runat="server" Width="110px" readonly="true" /></td>
	                                    <td>Usuario</td>
	                                    <td>
	                                        <asp:TextBox ID="txtIdUsuFin" runat="server" Width="50px" SkinID="numero" readonly="true" />
	                                        <asp:TextBox ID="txtDesUsuFin" runat="server" Width="480px" readonly="true" />
	                                    </td>
                                    </tr>
                                    <tr>
	                                    <td>Cierre</td>
	                                    <td><asp:TextBox ID="txtFecCierre" runat="server" Width="110px" readonly="true" /></td>
	                                    <td>Usuario</td>
	                                    <td><asp:TextBox ID="txtIdUsuCierre" runat="server" Width="50px" SkinID="numero" readonly="true" />
	                                    <asp:TextBox ID="txtDesUsuCierre" runat="server" Width="480px" readonly="true" /></td>
                                    </tr>
                                    <tr id="trUM" runat="server">
	                                    <td>Última modificación</td>
	                                    <td><asp:TextBox ID="txtFecModif" runat="server" Width="110px" readonly="true" /></td>
	                                    <td>Usuario</td>
	                                    <td><asp:TextBox ID="txtIdUsuModif" runat="server" Width="50px" SkinID="numero" readonly="true" />
	                                    <asp:TextBox ID="txtDesUsuModif" runat="server" Width="480px" readonly="true" /></td>
                                    </tr>
                                    <tr>
                                         <td colspan="3"></td>
                                         <td style="padding-left:70px;">
											<button id="btnAuditoria" type="button" onclick="getAuditoriaAux('PDF')" class="btnH25W100" runat="server" hidefocus="hidefocus" 
												 onmouseover="se(this, 25);mostrarCursor(this);">
												<img src="../../../images/botones/imgAuditoria.gif" /><span title="Auditoría">Auditoría</span>
											</button>	
                                        </td>
                                    </tr>
                                </table>
                            </fieldset><br /><br />
							<fieldset style="width:850px;">
								<legend>Observaciones</legend>
                                <table style="width:100%;" cellpadding="5px">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtObservaciones" SkinID="multi" runat="server" TextMode="MultiLine" Rows="20" Width="830px" onKeyUp="aG(4);" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                </table>
                </eo:PageView>

                <eo:PageView ID="PageView8" CssClass="PageView" runat="server">	
                <!-- Pestaña 6 Documentos-->
                <table style="width:870px;text-align:left;margin-left:20px; margin-top:15px;">
                    <tr>
	                    <td>
		                    <table id="Table2" style="width: 850px; height: 17px">
                            <colgroup>
                                <col style="width:312px" />
                                <col style="width:213px" />
                                <col style="width:225px" />
                                <col style="width:100px" />
                            </colgroup>		                
			                    <tr class="TBLINI">
				                    <td>&nbsp;Descripción</td>
				                    <td>Archivo</td>
				                    <td>Link</td>
				                    <td>Autor</td>
			                    </tr>
		                    </table>
		                    <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 866px; height:430px" runat="server">
		                        <div id="div1" style='background-image:url(../../../Images/imgFT20.gif); width:850px' runat="server">
		                        </div>
                            </div>
	                        <table id="Table1" style="width:850px; height:17px">
                                <tr class="TBLFIN"><td></td></tr>
	                        </table>
                        </td>
                    </tr>
                </table>   
                <center>
                    <table style="width:250px; margin-top:10px">
                    <tr>
                        <td>
                            <button id="Button1" type="button" onclick="nuevoDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                             onmouseover="se(this, 25);mostrarCursor(this);">
	                            <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                            </button>	
                        </td>
                        <td>
                            <button id="Button2" type="button" onclick="eliminarDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                             onmouseover="se(this, 25);mostrarCursor(this);">
	                            <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                            </button>	
                        </td>
                    </tr>
                    </table>	
                </center>               
                <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<center>
    <table id="tblBotones" style="margin-top:15px; text-align:left; width:880px; margin-left:10px;">
        <colgroup>
            <col style="width:110px;"/><col style="width:110px;"/><col style="width:110px;"/><col style="width:110px;"/>
            <col style="width:110px;"/><col style="width:110px;"/><col style="width:110px;"/><col style="width:110px;"/>
        </colgroup>
        <tr>
			<td>
				<button id="btnPDF" type="button" onclick="Exportar('PDF')" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgPDF.gif" /><span title="Genera un documento PDF con los datos de la tarea">Exportar</span>
				</button>			
			</td>			
			<td>
				<button id="btnNuevo" type="button" onclick="limpiar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgAnadir.gif" /><span title="Nueva tarea">Nueva</span>
				</button>			
			</td>
	        <td>
			    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
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
				<button id="btnDuplicar" type="button" onclick="duplicar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgDuplicar.gif" /><span title="Crear copia de la tarea actual">Duplicar</span>
				</button>			
			</td>				
			<td>
				<button id="btnBorrar" type="button" onclick="borrar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgEliminar.gif" /><span title="Elimina la tarea">Eliminar</span>
				</button>	
			</td>
	        <td>
			    <button id="btnGuia" type="button" onclick="mostrarGuia('DetalleTarea.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGuia.gif" /><span title="Guía">Guía</span>
			    </button>	
	        </td>				
	        <td>
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />
<input type="hidden" name="hdnIdTarea" id="hdnIdTarea" value="<%=nIdTarea %>" />
<input type="hidden" name="hdnCRActual" id="hdnCRActual" value="" runat="server" />
<input type="hidden" name="hdnDesCRActual" id="hdnDesCRActual" value="" runat="server" />
<input id="hdnIdCliente" name="hdnIdCliente" type="hidden" value="<%=strIdCliente %>" />
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDPT" name="hdnIDPT" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDAct" name="hdnIDAct" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDFase" name="hdnIDFase" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnEstado" name="hdnEstado" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnRecargar" name="hdnRecargar" runat="server" style="visibility:hidden" Text="F"></asp:TextBox>
<asp:TextBox ID="hdnUnicaEnActividad" name="hdnUnicaEnActividad" runat="server" style="visibility:hidden" Text="F"></asp:TextBox>
<asp:TextBox ID="hdnEstr" name="hdnEstr" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnOtcH" name="hdnOtcH" runat="server" style="visibility:hidden" Text="F"></asp:TextBox>
<asp:TextBox ID="txtCualidad" runat="server" Text="" style="width:12px;visibility:hidden;" readonly="true" />
<input type="hidden" name="nIdTarea" id="nIdTarea" value="" />
<input type="hidden" name="Permiso" id="Permiso" value="" />
<input type="hidden" name="nCR" id="nCR" value="" />
<input type="hidden" name="hdnT305IdProy" id="hdnT305IdProy" value="" runat="server"/>
<input type="hidden" name="hdnEstProy" id="hdnEstProy" value="" runat="server"/>
<input type="hidden" name="hdnModoAcceso" id="hdnModoAcceso" value="" runat="server"/>
<input type="hidden" name="hdnEsReplicable" id="hdnEsReplicable" value="" runat="server"/>
<input type="hidden" name="hdnNivelPresupuesto" id="hdnNivelPresupuesto" value="" runat="server" />
<asp:TextBox ID="hdnOrigen" runat="server" name="hdnOrigen" Style="visibility: hidden" Text=""></asp:TextBox>
<asp:TextBox ID="FORMATO" runat="server" Style="visibility: hidden" Text="PDF"></asp:TextBox>
    <script type="text/javascript">
    <%=strArrayVAE %>   
    <%=strArrayOrigenes %>
</script>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <div class="clsDragWindow" id="DW" noWrap></div>
</form>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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
