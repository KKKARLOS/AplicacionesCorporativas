<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de proyecto técnico</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../Javascript/documentos.js" type="text/Javascript"></script>
    <script src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
  	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?20180531" type="text/Javascript"></script>
	<script src="Functions/RecursoPT.js" type="text/Javascript"></script>
  	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
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
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var bCambios = false;
    var bLectura = <%=sLectura%>;
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var bRTPT = false;
    //Nos indica si hemos entrado para crear registro (C) o para buscar uno existente (B)
    var gsModo = "<%=gsModo%>";
    var bSalir = false;
    //Para la selección de proyectos económicos
    var nIndiceProy = -1;
    var aProy = new Array();
    //Variables a devolver a la estructura.
    var sDescripcion = "";
    var sEstado = "";
    var sFIV = "";
    var sFFV = "";
    var aVAE_js = new Array();
    //Para el comportamiento de los calendarios
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
</script>  
<table id="tabla" style="width:920px; margin-top:10px; margin-left:10px;">
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
							<eo:TabItem Text-Html="Responsables" Width="110" ToolTip="Responsables"></eo:TabItem>
							<eo:TabItem Text-Html="Avanzado" Width="120"></eo:TabItem>
							<eo:TabItem Text-Html="Tareas" Width="100"></eo:TabItem>
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
                <br />
                <table style="width:800px; text-align:left; margin-left:40px;">
                <colgroup>
                    <col style="width:460px"/>
                    <col style="width:340px"/>
                </colgroup>
                    <tr>
	                    <td colspan="2">
							<fieldset style="width:790px; margin-left:4px;">
								<legend>Estructura</legend><br />
                                <table style="width:100%;" cellpadding="5px">
                                    <tr>
                                        <td>
                                            <label id="lblProy" class="enlace" style="width:60px;height:17px" onclick="obtenerProyectos()">Proyecto</label>
					                        <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" style="vertical-align:middle;" />
                                            <asp:TextBox ID="hdnIdPE" runat="server" SkinID="Numero" style="width:50px"  maxlength="8" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);limpiarPE2();}"/>
					                        <asp:TextBox ID="txtPE" runat="server" style="width:530px" readonly="true" />
                                            <asp:TextBox ID="txtEstado" runat="server" Text="" style="width:2px;visibility:hidden;" readonly="true" />
                                            <asp:TextBox ID="txtCualidad" runat="server" Text="" style="width:2px;visibility:hidden;" readonly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
	                    </td>
                    </tr>
                    <tr>
	                    <td colspan="2">
							<fieldset style="width:790px; margin-left:4px; vertical-align:bottom;">
							<legend>Identificación de proyecto técnico</legend>
                                <table style="width:790px" border="0">
                                <colgroup>
                                    <col style="width:760px"/>
                                    <col style="width:30px"/>
                                </colgroup>
                                    <tr>
                                        <td style="vertical-align:bottom;">
                                            <label id="lblCodPT" class="texto" style="width:40px;height:17px; margin-left:4px;" >Número</label>
                                            <asp:TextBox ID="txtIdPT" runat="server" SkinID="Numero" style="width:60px" maxlength="8" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPT();}else{vtn2(event);limpiarPT2();}"/>                                            
                                            <label id="lblDesPT" class="enlace" style="width:80px;height:17px; margin-left:25px;" onclick="obtenerPTs()">Denominación</label>
                                            <asp:TextBox ID="txtDesPT" runat="server" Style="width: 520px" MaxLength="100" onKeyPress="teclaDenominacion()" onKeyUp="aG(0);"></asp:TextBox>
                                            <!--<asp:TextBox ID="txtDesPT2" MaxSize = "100" SkinID="multi" runat="server" TextMode="MultiLine" Rows="2" style="width:450px;height:35px;display:inline-block;float:right;margin-top: -15px;" onKeyPress="teclaDenominacion()" onKeyUp="aG(0);" ></asp:TextBox>-->
                                        </td>
                                        <td>
                                            <img id="btnBitacora" src="../../../Images/imgBTPTN.gif" style="width:30px; height:30px;" border="0" title="Sin acceso a la bitácora de proyecto técnico" onclick="mostrarBitacora();" /> 
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                    <tr>
	                    <td colspan="2">
                            <br />
                            &nbsp;&nbsp;Descripción<br />
                            <asp:TextBox ID="txtDescripcion" style="margin-left:5px" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="3" Width="800px" onKeyUp="aG(0);"></asp:TextBox>
	                    </td>
                    </tr>
                    <tr>
	                    <td>
	                        <table style="width:460px;margin-top:10px">
	                        <colgroup>
	                            <col style="width:290px;" />
	                            <col style="width:170px;" />
	                        </colgroup>
	                        <tr>
	                            <td>
							        <fieldset style="width:270px; height:40px; margin-left:4px;">
								        <legend>Vigencia</legend>
                                        <table style="width:250px" cellpadding="5">
                                            <tr>
                                                <td>
                                                    &nbsp;Inicio
                                                    <asp:TextBox ID="txtValIni" runat="server" style="width:60px;cursor:pointer; margin-right:23px;" Calendar="oCal" onchange="modificarVigencia();" ></asp:TextBox>
                                                    Fin
                                                    <asp:TextBox ID="txtValFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" cRef="txtValIni" onchange="modificarVigencia();" ></asp:TextBox>
                                               </td>
                                            </tr>
                                        </table>
                                    </fieldset>
	                            </td>
	                            <td>                                                                
                                    <fieldset style="width:155px; height:40px; margin-left:4px;">
								        <legend>Estado</legend>
                                        <table style="width:130px" cellpadding="5">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="cboEstado" runat="server" style="width:70px;" onchange="controlEstado(this.value);aG(0);">
                                                        <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                     </fieldset>
	                            </td>
                            </tr> 
                            </table>                                    
                        </td>
                        <td style="vertical-align:top;">
							<fieldset style="width:325px; height:40px; margin-left:10px;margin-top:10px">
								<legend>Acceso Bitácora desde IAP</legend>
                                <table width="100%" cellpadding="5">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cboAccesoIAP" runat="server" style="width:120px;" onchange="aG(0);">
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
                    <tr>
	                    <td>
							<fieldset style="width:445px; height:40px; margin-left:4px;margin-top:10px">
							<legend>Planificación</legend>
                                <table align="center" width="430px" cellpadding="5">
                                    <colgroup>
                                        <col style="width:217px"/>
                                        <col style="width:213px"/>
                                    </colgroup>
                                    <tr>
                                        <td>
                                            Inicio
                                            <asp:TextBox ID="txtPLIni" runat="server" style="width:60px; margin-right:23px;" readonly="true"></asp:TextBox>
                                            Fin
                                            <asp:TextBox ID="txtPLFin" runat="server" style="width:60px" readonly="true"></asp:TextBox>
                                        </td>
                                        <td>Horas <asp:TextBox ID="txtPLEst" runat="server" SkinID="numero" style="width:70px" readonly="true"></asp:TextBox>
                                       </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
							<fieldset style="width:325px; height:40px; margin-left:10px;margin-top:10px">
							<legend>Previsión</legend>
                                <table style="width:100%;" cellpadding="5px">
                                    <tr>
                                        <td style="padding-left:75px;">
                                            Fin
                                            <asp:TextBox ID="txtPRFin" runat="server" style="width:60px" readonly="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            Horas
                                            <asp:TextBox ID="txtPREst" runat="server" SkinID="numero" style="width:70px" readonly="true" />
                                        </td>
                                    </tr>
                                </table>
                        </fieldset>
	                    </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;">
							<fieldset id="idFieldsetIAP" style="width:445px; margin-left:4px; height:120px;margin-top:10px">
							<legend>IAP</legend>
                                <table style="width:440px" cellpadding="5px" cellspacing="0">
                                    <colgroup>
                                        <col style="width:125px" /><col style="width:75px;" /><col style="width:20px;" />
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
                                        <td><asp:TextBox ID="txtTotEst" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Consumido (horas)</td>
                                        <td><asp:TextBox ID="txtConHor" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td>Pte. estimado (horas)</td>
                                        <td><asp:TextBox ID="txtPteEst" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Consumido (jornadas)</td>
                                        <td><asp:TextBox ID="txtConJor" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td>Avance estimado</td>
                                        <td><asp:TextBox ID="txtAvanPrev" style="width:50px; margin-left:20px;" runat="server" SkinID="Numero" readonly="true" /></td>
                                        <td style="padding-left:0px;">%</td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="vertical-align:top;">
							<fieldset id="idFieldsetSituacion" style="width:325px; margin-left:10px; height:55px; margin-top:10px">
							<legend>Situación</legend>
                                <table align="center" width="100%" cellpadding="5">
                                    <tr>
                                        <td width="110px">Avance teórico</td>
                                        <td><asp:TextBox ID="txtAvanTeo" SkinID="Numero" style="width:60px" runat="server" readonly="true" />%
                                        </td>                                                
                                    </tr>
                                    <tr class="ocultarcapa">
                                        <td title="Calcula de forma automática el avance real, igualándolo al avance teórico.">Cálculo automático</td>
                                        <td><asp:CheckBox ID="chkAvanceAuto" checked="true" runat="server" style="width:15px;cursor:pointer" onClick="clickAvanceAutomatico();" /></td>
                                    </tr>
                                    <tr class="ocultarcapa">
                                        <td>Avance real</td>
                                        <td><asp:TextBox ID="txtAvanReal" SkinID="numero" style="width:60px" MaxLength="6" runat="server" onFocus="fn(this,5,2)" onKeyPress="pulsarTeclaAvanceReal(event);" onKeyUp="soltarTeclaAvanceReal();calcularProducido();" />%</td>
                                    </tr>
                                </table>
                            </fieldset>
							<fieldset style="width:325px; margin-left:10px; margin-top:13px; height:40px;">
								<legend>Importes (&euro;)</legend>
                                <table align="center" width="100%" cellpadding="5">
                                    <tr>
                                        <td>Presupuesto
                                            <asp:TextBox ID="txtPresupuesto" SkinID="Numero" style="width:70px" runat="server" onFocus="fn(this,7,2);"/>
                                        </td>
                                        <td class="ocultarcapa">Producido
                                            <asp:TextBox ID="txtProducido" runat="server" style="width:70px" SkinID="numero" readonly="true" />
                                        </td>

                                    </tr>
                                </table>
                            </fieldset>
                      </td>
                    </tr>
                </table>
                </eo:PageView>

                <eo:PageView ID="PageView2" CssClass="PageView" runat="server">	
                <!-- Pestaña 2 Técnicos-->
					<eo:TabStrip runat="server" id="tsPestanasProf" ControlSkinID="None" Width="904px" 
						MultiPageID="mpContenidoProf" 
						ClientSideOnLoad="CrearPestanasProf" 
						ClientSideOnItemClick="getPestana">
						<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							<Items>
									<eo:TabItem Text-Html="Asignación" ToolTip="Asignación de profesionales al proyecto" Width="100"></eo:TabItem>
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
                        <eo:PageView ID="PageView3" CssClass="PageView" runat="server">					        
			            <!-- Pestaña 2.1 Asignación-->	
                        <table style="text-align:left; width:870px; margin-left:10px;">
                            <tr>
                                <td>
						            <fieldset style="width:850px; height:330px; padding-left:10px;">
							            <legend>Selección de profesionales</legend>
                                        <table style="width:100%;" cellpadding="5px" >
                                            <colgroup><col style="width:43%;"/><col style="width:6%;"/><col style="width:51%;"/></colgroup>
                                            <tr style="height:42px;">
                                                <td>
                                                    <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionAmbito(this.id)">
                                                        <asp:ListItem Selected="True" Value="A" Text="Nombre&nbsp;&nbsp;" />
                                                        <asp:ListItem Value="C" Text="C.R.&nbsp;&nbsp;" />
                                                        <asp:ListItem Value="G" Text="Grupo funcional&nbsp;&nbsp;" />
                                                        <asp:ListItem Value="P" Text="Proy. económico" />
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <span id="ambAp" style="display:block" class="texto">
                                                        <table style="width:390px;">
                                                            <colgroup><col style="width:130px;"/><col style="width:130px;"/><col style="width:130px;"/></colgroup>
                                                            <tr>
                                                                <td>Apellido1</td>
                                                                <td>Apellido2</td>
                                                                <td>Nombre</td>
                                                            </tr>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtApellido" runat="server" style="width:123px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:123px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                                <td><asp:TextBox ID="txtNombre" runat="server" style="width:123px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                            </tr>
                                                        </table>
                                                    </span>
                                                    <span id="ambCR" style="display:none;">
                                                        <label id="lblCR" class="enlace" style="width:28px;height:14px;" onclick="obtenerCR()" runat="server"></label> 
                                                        <asp:TextBox ID="txtCR" runat="server" style="width:355px" />
                                                    </span>
                                                    <span id="ambGF" style="display:none;">
                                                        <label id="lblGF" class="enlace" style="width:94px;height:14px;" onclick="obtenerGF()">Grupo funcional</label> 
                                                        <asp:TextBox ID="txtGF" runat="server" style="width:290px" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;">
                                                    <table id="tblTitRec" style="width:350px; height:17px; text-align:center;">
		                                                <tr class="TBLINI">
				                                            <td>Profesionales&nbsp;
					                                            <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblRelacion',1,'divRelacion','imgLupa1')"
						                                            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				                                                <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblRelacion',1,'divRelacion','imgLupa1',event)"
						                                            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					                                        </td>
		                                                </tr>
	                                                </table>
	                                                <div id="divRelacion" style="overflow: auto; overflow-x: hidden; width: 367px; height:200px" onscroll="scrollTablaProf()">
	                                                    <div style='background-image:url(../../../Images/imgFT20.gif); width:350px'>
	                                                    <table id="tblRelacion" style="width: 350px;">
	                                                    </table>
	                                                    </div>
                                                    </div>
	                                                <table style="width: 350px; height: 17px; margin-bottom:7px;">
		                                                <tr class="TBLFIN"><td></td></tr>
	                                                </table>
                                                    <img class="ICO" src="../../../Images/imgUsuPVM.gif" title="Del <%=sNodo%> del proyecto" />&nbsp;Del <%=sNodo%>&nbsp;&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;
                                                    <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                                                    <label id="lblForaneo" runat="server">Foráneo</label>
                                                </td>
                                                <td style="vertical-align:middle; text-align:center;">
						                            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="3"></asp:Image>
                                                </td>
                                                <td><!-- Técnicos asignados -->
	                                                <table id="tblTitRecAsig" style="width:390px; height:17px;">
	                                                    <colgroup><col style="width:15%;"/><col style="width:70%;"/><col style="width:15%;"/></colgroup>
		                                                <tr class="TBLINI">
		                                                    <td></td>
			                                                <td>Profesionales asignados&nbsp;
					                                            <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblAsignados',3,'divAsignados','imgLupa3')"
						                                            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				                                                <img id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblAsignados',3,'divAsignados','imgLupa3',event)"
						                                            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					                                        </td>
			                                                <td style="text-align:right;" title="Número de tareas asignadas al profesional en las que esta activo">Nº&nbsp;</td>
		                                                </tr>
	                                                </table>
	                                                <div id="divAsignados" style="overflow: auto; overflow-x: hidden; width: 406px; height:200px" target="true" onmouseover="setTarget(this)" caso="1"  onscroll="scrollTablaProfAsig()">
	                                                    <div style='background-image:url(../../../Images/imgFT20.gif); width:390px'>
	                                                    <%=strTablaRecursos %>
	                                                    </div>
                                                    </div>
	                                                <table  style="width: 390px; height: 17px; margin-bottom:3px;">
		                                                <tr class="TBLFIN">
		                                                    <td></td>
		                                                 </tr>
	                                                </table>
	                                                <asp:Image id="Image5" style="CURSOR: pointer" ToolTip="Elimina las marcas de 'Asignar completo' o 'Desasignar completo'." onclick="reestablecer()" runat="server" ImageUrl="../../../Images/Botones/imgLimpiar.gif"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                                                <asp:Image id="imgAdjudicar" style="CURSOR: pointer" ToolTip="Asignar completo. Asocia al profesional seleccionado a todas las tareas dependientes de este nivel. Si ya se encuentra asociado, lo activa." onclick="asignarCompleto()" runat="server" ImageUrl="../../../Images/imgAdjudicar.gif"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                                                <asp:Image id="imgDesactivar" style="CURSOR: pointer" ToolTip="Desasignar completo. Desactiva al profesional seleccionado de todas las tareas dependientes de este nivel, en las que se encuentre asociado." onclick="desAsignarCompleto()" runat="server" ImageUrl="../../../Images/imgDesactivar.gif"></asp:Image>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label id="lblMostrarBajas" title="Muestra u oculta los profesionales de baja en la empresa o usuarios de baja en SUPER, identificándolos en rojo, o de baja en el proyecto.">Mostrar bajas</label>
                                                    <input type="checkbox" id="chkVerBajas" class="check" onclick="getRecursos();" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image id="imgDelBajaPE" runat="server" style="CURSOR: pointer" ToolTip="Borra fecha de baja en el proyecto económico a los usuarios seleccionados." onclick="reAsignar()" ImageUrl="../../../Images/imgVolverTrabajo.gif"></asp:Image>
                                                    &nbsp;&nbsp;&nbsp;<asp:Image id="imgCorreo" ToolTip="SUPER notifica la asignación de profesionales a la tarea, por parametrización a nivel de proyecto económico." runat="server" ImageUrl="../../../Images/imgCorreoHabilitado.gif"></asp:Image>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                             <tr>
                                <td>
						            <fieldset style="width:850px; height:120px; padding-left:10px;">
							            <legend>Indicaciones del responsable al profesional asignado seleccionado</legend>
                                        <table class="texto" style="width:100%; margin-top:10px;" cellpadding="5px" cellspacing="0" >
                                            <tr>
                                                <td>Fecha inicio prevista 
                                                    <asp:TextBox ID="txtFIPRes" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="oRecActualizar('U', 'fip', this);" />
                                                    &nbsp;&nbsp;Fecha fin prevista
                                                    <asp:TextBox ID="txtFFPRes" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="oRecActualizar('U', 'ffe', this);" />
                                                    &nbsp;&nbsp;&nbsp;Perfil 
                                                    <asp:DropDownList id="cboTarifa" runat="server" style="width:180px;" AppendDataBoundItems="true" onchange="oRecActualizar('U', 'idTarifa', this);">
                                                    <asp:ListItem Selected="True" Value="0" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:TextBox ID="txtIndicaciones" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="3" Width="815px" style="margin-top:10px;" onKeyUp="oRecActualizar('U', 'indicaciones', this);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                       </table>
                        <div id="divRecurso" style="z-index:0; position:absolute; left:33px; top:430px; width:840px; height:100px; background-image: url(../../../Images/imgFondoPixelado2.gif); background-repeat:no-repeat;"></div>
                        </eo:PageView>
                        <eo:PageView ID="PageView4" CssClass="PageView" runat="server">	
                        <!-- Pestaña 2.2 Pool de Grupos Funcionales y Técnicos-->
                        <table style="width:860px; text-align:left; margin-left:10px;" class="texto">
                            <colgroup>
                                <col style="width:460px" />
                                <col style="width:400px" />
                            </colgroup>
                            <tr>
                                <td>
                                    <fieldset style="width:90%; height: 115px;"><legend>Asignación automática de profesionales</legend>
                                        <br />
                                        <asp:CheckBox ID="chkHeredaPE" runat="server" Text=" Proyecto económico" Width="100%" onClick="aG(0);" ToolTip="Asigna de forma automática a todos los profesionales presentes y futuros que estén asignados al proyecto económico."/>
                                        <asp:CheckBox ID="chkHeredaCR" runat="server" Text=" Centro de responsabilidad " Width="100%" onClick="aG(0);" ToolTip="Asigna de forma automática a todos los profesionales presentes y futuros del <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>" />
                                        <br /><br /><span style="color:Red; font-size:9pt;">¡Atención! La activación de esta segunda opción, asignará de forma automática a las tareas dependientes de este Proyecto Técnico a todos los profesionales pertenecientes al <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> del proyecto, que son <label id="lblNumEmp" style="width:40px;color:Blue; font-size:12pt;" ></label></span>
                                    </fieldset>
                                </td>
                                <td>
                                    <table style="width:370px; height:17px">
                                        <tr class="TBLINI">
                                            <td style="text-align:center;">Grupos funcionales</td>
                                        </tr>
                                    </table>
                                    <div id="divPoolGF" style="overflow:auto; overflow-x:hidden; width:386px; height:100px">
                                        <div style='background-image:url(../../../Images/imgFT16.gif); width:370px'>
                                        <%=strTablaPoolGF %>
                                        </div>
                                    </div>
                                    <table style="width: 370px; height: 17px">
                                        <tr class="TBLFIN"><td></td></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <center>
                                        <table style="width:250px;margin-top:10px">
                                        <tr>
                                            <td width="45%">
		                                        <button id="btnAGF" type="button" onclick="obtenerGF3()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                                         onmouseover="se(this, 25);mostrarCursor(this);">
			                                        <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
		                                        </button>	
                                            </td>
                                            <td width="10%"></td>
                                            <td width="45%">
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
                            <tr>
	                            <td colspan="2">
							        <fieldset style="width:850px; height: 310px;">
								        <legend>Profesionales</legend>
                                        <table style="text-align:left; width:830px;" cellpadding="0px">
                                            <colgroup><col style="width:47%;"/><col style="width:6%;"/><col style="width:47%;"/></colgroup>
                                            <tr style="height:40px;">
                                                <td>
                                                    <table id="Span1" style="text-align:left; width:375px;">
                                                        <colgroup><col style="width:125px;"/><col style="width:125px;"/><col style="width:125px;"/></colgroup>
                                                        <tr>
                                                            <td>Apellido1</td>
                                                            <td>&nbsp;Apellido2</td>
                                                            <td>&nbsp;Nombre</td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtApe1Pool" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos3('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                            <td><asp:TextBox ID="txtApe2Pool" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos3('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                            <td><asp:TextBox ID="txtNomPool" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos3('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td> 
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td> <!-- Relación de técnicos -->
		                                            <table id="tblTitPoolAsig" style="width:370px; height:17px">
			                                            <tr class="TBLINI">
				                                            <td style="text-align:center;">Relación de profesionales</td>
			                                            </tr>
		                                            </table>
		                                            <div id="divRelacionPool" style="overflow: auto; overflow-x: hidden; width: 386px; height:180px" onscroll="scrollTablaPool()">
		                                                <div style='background-image:url(../../../Images/imgFT20.gif); width:370px'>
		                                                <table id="TABLE8" style="width: 370px;">
		                                                </table>
		                                                </div>
                                                    </div>
		                                            <table style="width: 370px; height: 17px; margin-bottom:8px;">
			                                            <tr class="TBLFIN"><td></td></tr>
		                                            </table>
                                                    <img class="ICO" src="../../../Images/imgUsuPVM.gif" title="Del <%=sNodo%> del proyecto"/>&nbsp;Del <%=sNodo%>&nbsp;&nbsp;&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                                                    <img id="imgForaneo2" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                                                    <label id="lblForaneo2" runat="server">Foráneo</label>
                                                </td>
                                                <td style="vertical-align:middle;">
							                        <asp:Image id="imgPapPoolProf" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                                </td>
                                                <td style="vertical-align:top;"><!-- Técnicos asignados -->
		                                            <table style="width:370px; height:17px;">
			                                            <tr class="TBLINI">
				                                            <td style="text-align:center;">Profesionales asignados&nbsp;
						                                        <img id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblAsignados3',0,'divPoolProf','imgLupa5')"
							                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					                                            <img id="imgLupa6" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblAsignados3',0,'divPoolProf','imgLupa5',event)"
							                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						                                    </td>
			                                            </tr>
		                                            </table>
		                                            <div id="divPoolProf" style="overflow: auto; overflow-x: hidden; width: 386px; height:180px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaPoolAsig()">
		                                                <div style='background-image:url(../../../Images/imgFT20.gif); width:370px'>
		                                                <%=strTablaPoolProf %>
		                                                </div>
                                                    </div>
		                                            <table style="width: 370px; height: 17px; margin-bottom:8px;">
			                                            <tr class="TBLFIN"><td></td></tr>
		                                            </table>
                                                    <label id="Label10" title="Muestra u oculta los profesionales de baja en la empresa o usuarios de baja en SUPER, identificándolos en rojo, o de baja en el proyecto.">Mostrar bajas</label>
                                                    <input type="checkbox" id="chkVerBajasPool" class="check" onclick="getRecursosPool();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
	                            </td>
                            </tr>
                        </table>
                        </eo:PageView>
                    </eo:MultiPage>
                </eo:PageView>

                <eo:PageView ID="PageView5" CssClass="PageView" runat="server">	
                <!-- Pestaña 3 Responsables-->
                <br />
                <center>                
                <table width="100%" style="text-align:left;">
                    <tr>
	                    <td align="center">
							<fieldset style="width:805px; height: 500px;">
								<legend>Responsables Técnicos de Proyecto Técnico</legend>
                                <table cellpadding="5" style="text-align:left; width:100%;">
                                    <colgroup><col style="width:47%;"/><col style="width:6%;"/><col style="width:47%;"/></colgroup>
                                    <tr style="height:40px;">
                                        <td colspan="2">
                                            <asp:RadioButtonList Height="25px" ID="rdbAmbito2" runat="server" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionAmbito2(this.id)">
                                                <asp:ListItem Selected="True" Value="A" Text="Nombre&nbsp;" />
                                                <asp:ListItem Value="C" Text="C.R.&nbsp;" />
                                                <asp:ListItem Value="G" Text="G.Funcional&nbsp;" />
                                                <asp:ListItem Value="P" Text="P.Económico&nbsp;" />
                                                <asp:ListItem Value="T" Text="P.Técnico" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <span id="ambAp2" style="display:block" class="texto">
                                                &nbsp;Apellido1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;Apellido2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;Nombre<br />
                                                <asp:TextBox ID="txtApe1" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos2('N','','','');event.keyCode=0;}" MaxLength="50" />
                                                <asp:TextBox ID="txtApe2" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos2('N','','','');event.keyCode=0;}" MaxLength="50" />
                                                <asp:TextBox ID="txtNom" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos2('N','','','');event.keyCode=0;}" MaxLength="50" />
                                            </span>
                                            <span id="ambCR2" style="display:none" class="texto"><label id="lblCR2" class="enlace" style="width:28px;height:17px" onclick="obtenerCR2()">C.R.</label> <asp:TextBox ID="txtCR2" runat="server" Width="316px" /></span>
                                            <span id="ambGF2" style="display:none" class="texto"><label id="lblGF2" class="enlace" style="width:94px;height:17px" onclick="obtenerGF2()">Grupo funcional</label> <asp:TextBox ID="txtGF2" runat="server" Width="250px" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><!-- Relación de técnicos -->
		                                    <table style="width:350px; height:17px">
			                                    <tr class="TBLINI">
				                                    <td style="text-align:center;">Relación de profesionales</td>
			                                    </tr>
		                                    </table>
		                                    <div id="divRTPTcandidato" style="overflow: auto; overflow-x: hidden; width: 367px; height:360px;text-align:left;" onscroll="scrollTablaRTPT()">
		                                        <div style='background-image:url(../../../Images/imgFT20.gif); width:350px'>
		                                        <table id="tblRelacion2" style="width: 350px;">
		                                        </table>
		                                        </div>
                                            </div>
		                                    <table style="width: 350px; height: 17px">
			                                    <tr class="TBLFIN">
				                                    <td></td>
			                                    </tr>
		                                    </table>
                                        </td>
                                        <td style="vertical-align:middle;">
							                <asp:Image id="imgPapRtpt" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                        </td>
                                        <td><!-- Técnicos asignados -->
		                                    <table style="width: 350px; height: 17px">
			                                    <tr class="TBLINI">
				                                    <td style="text-align:center;">RTPT&#39;s asignados</td>
			                                    </tr>
		                                    </table>
		                                    <div id="divRTPTAsignados" style="overflow: auto; overflow-x: hidden; width: 366px; height:360px; text-align:left;" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaRTPTAsig()">
		                                        <div style='background-image:url(../../../Images/imgFT20.gif); width:350px'>
		                                        <%=strTablaRTPTs %>
		                                        </div>
                                            </div>
		                                    <table  style="width: 350px; height: 17px">
			                                    <tr class="TBLFIN">
				                                    <td></td>
			                                    </tr>
		                                    </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top:4px;" colspan="3">
                                            <img class="ICO" src="../../../Images/imgUsuPVM.gif" title="Del <%=sNodo%> del proyecto"/>&nbsp;Del <%=sNodo%>&nbsp;&nbsp;&nbsp;
                                            <img class="ICO" src="../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
                                            <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                                            <img id="imgForaneo3" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                                            <label id="lblForaneo3" runat="server">Foráneo</label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                </table>
                </center>
                </eo:PageView>

                <eo:PageView ID="PageView6" CssClass="PageView" runat="server">	
                <!-- Pestaña 4 Avanzado-->
					<eo:TabStrip runat="server" id="tsPestanasAvanza" ControlSkinID="None" Width="904px" 
						MultiPageID="mpContenidoAvanza" 
						ClientSideOnLoad="CrearPestanasAvanza" 
						ClientSideOnItemClick="getPestana">
						<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							<Items>
								<eo:TabItem Text-Html="General" Width="100"></eo:TabItem>
								<eo:TabItem Text-Html="Campos" Width="100"></eo:TabItem>
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
                        <eo:PageView ID="PageView7" CssClass="PageView" runat="server">					        
			            <!-- Pestaña 4.1 General-->	
							<center>
                                <table style="text-align:left; width:840px; margin-left:20px; margin-top:15px;">
                                    <tr>
                                        <td>
					                        <fieldset style="width:820px; margin-left:4px; height:370px;">
						                        <legend>Criterios estadísticos técnicos</legend>
                                                <table width="800px" cellpadding="5">
                                                <colgroup>
                                                    <col style="width:200px;"/>
                                                    <col style="width:50px;"/>
                                                    <col style="width:350px;"/>
                                                    <col style="width:200px;"/>
                                                </colgroup>                                
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:CheckBox ID="chkCliente" runat="server" Text="Restringidos al cliente " onclick="restringir();" Width="800" style="cursor:pointer" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 180px; height: 17px">
	                                                            <tr class="TBLINI">
                                                                    <td style="padding-left:20px">
                                                                        <nobr class='NBR' style='width:160px' onmouseover="TTip(event)">Definidos en el <%=sNodo%></nobr>
                                                                     </td>
	                                                            </tr>
                                                            </table>
                                                            <div id="divAECR" style="overflow: auto; overflow-x: hidden; width: 198px; height:292px">
                                                                    <%=strTablaAECR %>
                                                            </div>
                                                            <table style="width: 180px; height: 17px">
	                                                            <tr class="TBLFIN"> <td></td> </tr>
                                                            </table>
                                                        </td>
                                                        <td style="vertical-align:middle; text-align:center;">
					                                        <asp:Image id="imgPapeleraAE" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                                        </td>
                                                        <td>
                                                            <table style="width:330px;height:17px">
                                                                <colgroup>
                                                                    <col style="width:230px"/>
                                                                    <col style="width:100px"/>
                                                                </colgroup>
	                                                            <tr class="TBLINI">
		                                                            <td align="center">Asociados al proyecto técnico</td>
		                                                            <td align="left">Valor</td>
	                                                            </tr>
                                                            </table>
                                                            <div id="divAET" style="overflow: auto; overflow-x: hidden; width: 348px; height:292px" target="true" onmouseover="setTarget(this);" caso="1">
                                                                    <%=strTablaAET%>
                                                            </div>
                                                            <table style="width: 330px;height: 17px">
	                                                            <tr class="TBLFIN"><td></td></tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table style="width: 180px; height: 17px">
	                                                            <tr class="TBLINI"><td align="center">Valores definidos</td></tr>
                                                            </table>
                                                            <div id="divAEVD" style="overflow: auto; overflow-x: hidden; width: 198px; height:292px">
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
                                        <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
						                    <fieldset style="width:820px; margin-left:4px; height:90px;">
							                    <legend>Otra información</legend>
                                                <table style="width:100%;" cellpadding="5px">
                                                    <tr>
                                                        <td><asp:Label ID="lblOTC" style="width:40px" SkinID="enlace" onclick="mostrarOTC();" runat="server" Text="OTC" ToolTip="Orden de trabajo codificada" />
                                                            <asp:TextBox ID="txtCodPST" runat="server" style="width:200px" readonly="true" />
                                                            <asp:TextBox ID="txtDesPST" runat="server" Width="400px" readonly="true" />
                                                            &nbsp;
                                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="limpiarPST();" style="cursor:pointer; vertical-align:bottom" />
	                                        
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkObligaEst" runat="server" style="cursor:pointer;width:200px;" onClick="aG(0);" Text="Estimación Obligatoria" TextAlign=Left />
                                                            <asp:TextBox ID="txtIdPST" runat="server" style="width:2px;visibility:hidden;" readonly="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                        </fieldset>
                                        </td>
                                    </tr>
                                </table>
						    </center>
                        </eo:PageView>
                        <eo:PageView ID="PageView8" CssClass="PageView" runat="server">	
                        <!-- Pestaña 4.2 Campos-->
                            <center>
                                <table style="text-align:left; width:840px; margin-left:20px; margin-top:15px;">
                                    <tr>
                                        <td>
					                        <!--<fieldset style="width:820px; margin-left:4px; height:370px;">
						                        <legend>Criterios estadísticos técnicos</legend>-->
                                                <table width="800px" cellpadding="5">
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
                                                            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:180px; height:350px">
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
		                                                    <div id="divCatalogoValores" style="overflow: auto; overflow-x: hidden; width: 600px; height:350px" target="true" caso="1"  runat="server">
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
                                
                                
                                       <button id="btnValores" type="button" onclick="MtoValores()" style="float: left;margin-left: 17px;" class="btnH25W200" runat="server" hidefocus="hidefocus" 
				                                onmouseover="se(this, 25);mostrarCursor(this);">
				                            <img src="../../../../../images/botones/imgEntrada.gif" /><span title="Accede al mantenimiento de campos a asociar a las tareas">Mantenimiento de campos</span>
			                            </button>	
                                   
                            </center>  
                        </eo:PageView>
                    </eo:MultiPage>
                </eo:PageView>

                <eo:PageView ID="PageView9" CssClass="PageView" runat="server">	
                <!-- Pestaña 5 tareas-->
                <table style="text-align:left; width:870px; margin-top:10px; margin-left:20px;">
                    <tr>
	                    <td>
                            <table style="width: 850px; height: 17px;">
                                <colgroup>
                                    <col style="width:285px"/>
                                    <col style="width:55px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                    <col style="width:55px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                </colgroup>
                                <tr class="TBLINI" align="center">
		                            <td>&nbsp;Denominación</td>
		                            <td title='Esfuerzo total planificado' style="text-align:right;">ETPL</td>
		                            <td title='Fecha inicio planificada'>FIPL</td>
		                            <td title='Fecha fin planificada'>FFPL</td>
		                            <td title='Esfuerzo total previsto' style="text-align:right; padding-right:5px;">ETPR</td>
		                            <td title='Fecha fin prevista'>FFPR</td>
		                            <td title='Fecha inicio vigencia'>FIV</td>
		                            <td title='Fecha fin vigencia'>FFV</td>
		                            <td title='Consumo en horas' style="text-align:right;">Consumo</td>
		                            <td title='Orden Trabajo Codificada'>OTC</td>
                                </tr>
                            </table>
                            <div id="divTareas" style="overflow: auto; overflow-x: hidden; width: 866px; height:450px">
                                <div style='background-image:url(../../../Images/imgFT20.gif); width:850px;'>
                                <%=strTablaTareas %>
                                </div>
                            </div>
                            <table style="width: 850px; height: 17px;">
                                <tr class="TBLFIN">
                                    <td></td>
                                </tr>
                            </table>
	                    </td>
                    </tr>
                </table>
                </eo:PageView>

                <eo:PageView ID="PageView10" CssClass="PageView" runat="server">	
                 <!-- Pestaña 6 Control-->
                <table style="width:860px;text-align:left;margin-left:20px">
                    <tr>
	                    <td>							
							<fieldset style="width:850px;">
								<legend>Observaciones</legend>
                                <table style="width:100%;" cellpadding="5px">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtObservaciones" SkinID="multi" runat="server" TextMode="MultiLine" Rows="20" Width="830px" onKeyUp="aG(0);" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                </table>
                </eo:PageView>
                
                <eo:PageView ID="PageView11" CssClass="PageView" runat="server">	
                <!-- Pestaña 7 Documentación -->
                <table style="text-align:left; width:870px; margin-top:15px; margin-left:20px;">
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
		                    <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 866px; height:190px" runat="server">
		                        <div id="div1" style='background-image:url(../../../Images/imgFT20.gif); width:850px' runat="server">
		                        </div>
                            </div>
	                        <table id="Table1" style="width: 850px; height: 17px">
                                <tr class="TBLFIN"><td></td></tr>
	                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:250px;margin-top:10px;margin-left:315px">
                            <tr>
                                <td width="45%">
		                            <button id="Button1" type="button" onclick="nuevoDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                             onmouseover="se(this, 25);mostrarCursor(this);">
			                            <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
		                            </button>	
                                </td>
                                <td width="10%"></td>
                                <td width="45%">
		                            <button id="Button2" type="button" onclick="eliminarDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                             onmouseover="se(this, 25);mostrarCursor(this);">
			                            <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
		                            </button>	
	                            </td>
                            </tr>
                            </table>	
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:bottom; height:30px;">
                            <div>Documentación dependiente</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
		                    <table id="Table6" style="width: 850px; height: 17px">
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
		                    <div id="divCatalogoDoc2" style="overflow: auto; overflow-x: hidden; width: 866px; height:155px" runat="server">
		                        <div id="div2" style='background-image:url(../../../Images/imgFT20.gif); width:850px' runat="server">
		                        </div>
                            </div>
		                    <table id="Table7" style="width: 850px; height: 17px">
			                    <tr class="TBLFIN">
				                    <td></td>
			                    </tr>
		                    </table>                        
		                </td>
                    </tr>
                </table>
                <iframe id="iFrmSubida" frameborder="0" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<center>
    <table id="tblBotones" align="center" style="margin-top:15px;" width="70%">
        <tr>
			<td align="center">
				<button id="btnNuevo" type="button" onclick="limpiar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgAnadir.gif" /><span title="Nuevo proyecto técnico">Nuevo</span>
				</button>			
			</td>
	        <td align="center">
			    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
	        </td>
			<td align="center">
				<button id="btnEliminar" type="button" onclick="borrar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgEliminar.gif" /><span title="Elimina el proyecto técnico">Eliminar</span>
				</button>	
			</td>
	        <td align="center">
			    <button id="btnGuia" type="button" onclick="mostrarGuia('DetallePT.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGuia.gif" /><span title="Guía">Guía</span>
			    </button>	
	        </td>				
	        <td align="center">
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
<input type="hidden" name="hdnIDPT" id="hdnIDPT" value="<%=nIdPT %>" />
<input type="hidden" name="hdnCRActual" runat="server" id="hdnCRActual" value="" />
<input type="hidden" name="hdnDesCRActual" runat="server" id="hdnDesCRActual" value="" />
<input id="hdnIdCliente" name="hdnIdCliente" type="hidden" value="<%=strIdCliente %>" />
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnEstr" name="hdnEstr" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnRecargar" name="hdnRecargar" runat="server" style="visibility:hidden" Text="F"></asp:TextBox>
<input type="hidden" name="nIdPT" id="nIdPT" value="" />
<input type="hidden" name="Permiso" id="Permiso" value="" />
<input type="hidden" name="nCR" id="nCR" value="" />
<input type="hidden" name="hdnT305IdProy" id="hdnT305IdProy" runat="server" value="" />
<input type="hidden" name="hdnAccBitacora" id="hdnAccBitacora" runat="server" value="X" />
<input type="hidden" name="hdnModoAcceso" id="hdnModoAcceso" value="" runat="server"/>
<input type="hidden" name="hdnEsReplicable" id="hdnEsReplicable" value="" runat="server"/>
<input type="hidden" name="hdnNivelPresupuesto" id="hdnNivelPresupuesto" value="" runat="server" />
<input type="hidden" name="hdnLstAEsTareas" id="hdnLstAEsTareas" value="" runat="server"/>
<input type="hidden" name="hdnLstCamposTareas" id="hdnLstCamposTareas" value="" runat="server"/>

<script type="text/javascript">
    <%=strArrayVAE %>
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
