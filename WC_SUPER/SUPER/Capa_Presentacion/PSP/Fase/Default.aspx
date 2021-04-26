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
<head  runat="server">
    <title> ::: SUPER ::: - Detalle de fase</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?20180208" type="text/Javascript"></script>
	<script src="Functions/RecursoFase.js" type="text/Javascript"></script>
	<script src="../../../Javascript/documentos.js" type="text/Javascript"></script>
    <script src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
  	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script> 
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
<style type="text/css">
#tsPestanas table { table-layout:auto; }
#tsPestanasProf  table { table-layout:auto; }
#tblTareas TD{border-right: solid 1px #569BBD; padding-left:2px; padding-right:2px;}
</style>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
        var bOTCHeredada = <%=sOTCHeredada%>;
        var bSalir = false;
        mostrarProcesando();
        //Variables a devolver a la estructura.
        var sDescripcion = "";
        var sFIV = "";
        var sFFV = "";
        //Para el comportamiento de los calendarios
        var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
    </script>    
<center>
<table id="tabla" style="width:920px; margin-left:5px; margin-top:10px;">
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
                    <table style="width:830px; text-align:left; margin-top:10px; margin-left:30px;" cellpadding="3px">
                        <tr>
	                        <td>
							    <fieldset style="width:800px; margin-left:4px;">
								    <legend>Estructura</legend>
                                    <table style="width:100%" cellpadding="4px">
                                        <tr>
                                            <td>
                                                <label style="width:85px">Proyecto</label>
					                            <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" style="vertical-align:middle;" />
					                            <asp:TextBox ID="hndIdPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" />
					                            <asp:TextBox ID="txtPE" runat="server" style="width:593px" readonly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
					                            <label style="width:105px">Proyecto técnico</label>
					                            <asp:TextBox ID="txtDesPT" runat="server" style="width:650px" readonly="true" />
					                            <asp:TextBox ID="txtIdPT" runat="server" SkinID="Numero" style="width:2px;visibility:hidden;" readonly="true"></asp:TextBox>
                                                <asp:TextBox ID="txtCualidad" runat="server" Text="" style="width:2px;visibility:hidden;" readonly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
	                        </td>
                        </tr>
                        <tr>
	                        <td>
							    <fieldset style="width:800px; margin-left:4px; margin-top:5px;">
								    <legend>Identificación de fase</legend>
                                    <table style="width:100%" cellpadding="5px">
                                        <tr>
                                            <td>
                                                <label style="width:105px" ondblclick="verCodigo()">Denominación</label>
                                                <asp:TextBox ID="txtDesFase" runat="server" Style="width:650px" MaxLength="100" onKeyUp="aG(0);"></asp:TextBox>                                               
                                            </td>
                                        </tr>
                                    </table>
                            </fieldset>
	                        </td>
                        </tr>
                        <tr>
	                        <td>
                                &nbsp;&nbsp;Descripción<br />
                                <asp:TextBox ID="txtDescripcion" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="4" style="width:810px; margin-left:4px;" onKeyUp="aG(0);"></asp:TextBox>
	                        </td>
                        </tr>
                        <tr>
	                        <td>
	                            <table style="width:800px; margin-top:10px">
	                            <colgroup>
	                                <col style="width:240px;" />
	                                <col style="width:560px;" />
	                            </colgroup>	           
	                                <tr>
	                                    <td>
							                <fieldset style="width:215px; margin-left:4px;height:40px;">
								                <legend>Vigencia</legend>
                                                <table style="width:100%;margin-top:5px" cellpadding="4">
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
			                                <fieldset style="width:560px; margin-left:4px;height:40px;">
			                                <legend>Orden de trabajo codificada</legend>
                                                <table style="width:100%" cellpadding="5px">
                                                    <tr>
                                                        <td><asp:Label ID="lblOTC" style="width:40px;height:14px;" SkinID="enlace" onclick="mostrarOTC();" runat="server" Text="OTC" ToolTip="Orden de trabajo codificada" />
                                                            <asp:TextBox ID="txtCodPST" runat="server" style="width:150px" readonly="true" />
	                                                        <asp:TextBox ID="txtDesPST" runat="server" Width="320px" readonly="true" />
	                                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="limpiarPST();" style="cursor:pointer; vertical-align:bottom;" />
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
	                        <td>
	                            <table style="width:820px;margin-top:10px">
	                            <colgroup>
	                                <col style="width:490px;" />
	                                <col style="width:330px;" />
	                            </colgroup>	           
	                                <tr>
	                                    <td>     	                    
						                    <fieldset style="width:460px; margin-left:4px;"><legend>Planificación</legend>
                                                <table style="width:390px;" cellpadding="5px">
                                          	    <colgroup><col style="width:230px;" /><col style="width:160px;" /></colgroup>	     
                                                    <tr>
                                                        <td>
                                                            Inicio
                                                            <asp:TextBox ID="txtPLIni" runat="server" style="width:60px; margin-right:25px;" readonly="true"></asp:TextBox>
                                                            Fin
                                                            <asp:TextBox ID="txtPLFin" runat="server" style="width:60px;" readonly="true"></asp:TextBox>
                                                        </td>
                                                        <td>Horas 
                                                            <asp:TextBox ID="txtPLEst" runat="server" SkinID="Numero" style="width:70px" readonly="true"></asp:TextBox>
                                                       </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
	                                    </td>    
                                        <td>                            
							                <fieldset style="width:315px;"><legend>Previsión</legend>
                                                <table style="width:233px; margin-left:55px;" cellpadding="5px">
                                                    <tr>
                                                        <td>
                                                            Fin
                                                            <asp:TextBox ID="txtPRFin" runat="server" style="width:60px" readonly="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            Horas
                                                            <asp:TextBox ID="txtPREst" runat="server" SkinID="Numero" style="width:70px;" readonly="true" />
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
                            <td>
	                            <table style="width:820px;margin-top:10px">
	                            <colgroup>
	                                <col style="width:490px;" />
	                                <col style="width:330px;" />
	                            </colgroup>	     
                                <tr>
                                    <td>  	                                             
							            <fieldset id="idFieldsetIAP" style="width:460px; margin-left:4px;"><legend>IAP</legend>
                                            <table style="width:450px; text-align:left;" cellpadding="5px">
                                                <colgroup>
                                                    <col style="width:120px" /><col style="width:80px;" /><col style="width:20px" />
                                                    <col style="width:140px" /><col style="width:80px;" /><col style="width:10px;" />
                                                </colgroup>
                                                <tr>
                                                    <td>Primer consumo</td>
                                                    <td style="text-align:right;"><asp:TextBox ID="txtPriCon" style="width:60px; margin-left:10px;" runat="server" readonly="true" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>Fin estimado</td>
                                                    <td style="text-align:right;"><asp:TextBox ID="txtFinEst" style="width:60px; margin-left:10px;" runat="server" readonly="true" /></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Último consumo</td>
                                                    <td style="text-align:right;"><asp:TextBox ID="txtUltCon" style="width:60px; margin-left:10px;" runat="server" readonly="true" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>Total estimado (horas)</td>
                                                    <td style="text-align:right;"><asp:TextBox ID="txtTotEst" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Consumido (horas)</td>
                                                    <td style="text-align:right;"><asp:TextBox ID="txtConHor" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>Pte. estimado (horas)</td>
                                                    <td style="text-align:right;"><asp:TextBox ID="txtPteEst" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Consumido (jornadas)</td>
                                                    <td style="text-align:right;"><asp:TextBox ID="txtConJor" SkinID="Numero" style="width:70px" runat="server" readonly="true" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>Avance estimado</td>
                                                    <td>
                                                        <asp:TextBox ID="txtAvanPrev" style="width:40px; margin-left:30px;" runat="server" SkinID="Numero" readonly="true"/>
                                                    </td>
                                                    <td style="padding-left:0px;">%</td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>    
                                   <td style="padding-top:5px; vertical-align:top;">                                          
							        <fieldset id="idFieldsetSituacion" style="width:315px; vertical-align:top;"><legend>Situación</legend>
                                        <table style="width:300px" cellpadding="2px">
                                            <tr>
                                                <td title="Relación entre las horas consumidas y las horas totales previstas" width="110px">Avance teórico</td>
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
                                    <br />
                                    <fieldset style="width:315px;">
								        <legend>Importes (&euro;)</legend>
                                        <table align="center" style="width:300px" cellpadding="2px">
                                            <colgroup><col style="width:150px;"/><col style="width:150px;"/></colgroup>
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
									<eo:TabItem Text-Html="Asignación" ToolTip="" Width="100"></eo:TabItem>
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
                        <table style="text-align:left; width:100%; margin-left:10px;">		
                            <tr style="height:45px">
	                            <td>
							        <fieldset style="width:850px; height: 330px;">
								        <legend>Selección de profesionales</legend>
                                        <table style="width:100%" cellpadding="5px">
                                            <colgroup><col style="width:41%;"/><col style="width:10%;"/><col style="width:49%;"/></colgroup>
                                            <tr style="height:45px">
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
                                                    <table style="width: 380px;">
                                                        <tr>
                                                            <td>
                                                                &nbsp;&nbsp;Apellido1
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;Apellido2
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;Nombre
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtApellido" runat="server" Width="120px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtApellido2" runat="server" Width="120px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNombre" runat="server" Width="120px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </span>
                                                <span id="ambCR" style="display:none" class="texto"><label id="lblCR" class="enlace" style="width:28px;height:17px" onclick="obtenerCR()">C.R.</label> <asp:TextBox ID="txtCR" runat="server" Width="346px" /></span>
                                                <span id="ambGF" style="display:none" class="texto"><label id="lblGF" class="enlace" style="width:94px;height:17px" onclick="obtenerGF()">Grupo funcional</label> <asp:TextBox ID="txtGF" runat="server" Width="280px" /></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;">
                                                    <table id="tblTitRec" style="width: 350px; height: 17px">
			                                            <tr class="TBLINI">
					                                        <td style="text-align:center;">Profesionales&nbsp;
						                                        <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblRelacion',1,'divRelacion','imgLupa1')"
							                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					                                            <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblRelacion',1,'divRelacion','imgLupa1',event)"
							                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						                                    </td>
			                                            </tr>
		                                            </table>
		                                            <div id="divRelacion" style="overflow:auto; overflow-x:hidden; width:367px; height:200px" onscroll="scrollTablaProf()">
	                                                    <div style='background-image:url(../../../Images/imgFT20.gif); width:350px'>
	                                                    <table id="tblRelacion" style="width: 350px;">
	                                                    </table>
	                                                    </div>
                                                    </div>
	                                                <table style="width: 350px; height: 17px; margin-bottom:8px;">
		                                                <tr class="TBLFIN"><td></td></tr>
	                                                </table>
                                                    <img class="ICO" src="../../../Images/imgUsuPVM.gif" title="Del <%=sNodo%> del proyecto"/>Del <%=sNodo%>&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuNVM.gif" />De otro <%=sNodo %>&nbsp;
                                                    <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;
                                                    <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                                                    <label id="lblForaneo" runat="server">Foráneo</label>
                                                </td>
                                                <td style="vertical-align:middle; text-align:center;">
							                        <asp:Image id="imgPapelera" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                                </td>
                                                <td>
                                                    <table id="tblTitRecAsig" style="width: 380px; height: 17px">
			                                            <colgroup><col style="width:15%;"/><col style="width:70%;"/><col style="width:15%;"/></colgroup>
			                                            <tr class="TBLINI">
			                                                <td></td>
				                                            <td>Profesionales asignados&nbsp;
						                                        <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblAsignados',3,'divAsignados','imgLupa3')"
							                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					                                            <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblAsignados',3,'divAsignados','imgLupa3',event)"
							                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						                                    </td>
				                                            <td style="text-align:right;" title="Número de tareas asignadas al profesional en las que esta activo">Nº&nbsp;</td>
			                                            </tr>
		                                            </table>
	                                                <div id="divAsignados" style="overflow:auto; overflow-x:hidden; width:396px; height:200px" target="true" onmouseover="setTarget(this)" caso="1"  onscroll="scrollTablaProfAsig()" >
	                                                    <div style='background-image:url(../../../Images/imgFT20.gif); width:380px'>
	                                                    <%=strTablaRecursos %>
	                                                    </div>
                                                    </div>
		                                            <table  style="width: 380px; height: 17px; margin-bottom:3px;">
			                                            <tr class="TBLFIN"><td></td></tr>
		                                            </table>
		                                            <asp:Image id="Image5" style="CURSOR: pointer" ToolTip="Elimina las marcas de 'Asignar completo' o 'Desasignar completo'." onclick="reestablecer()" runat="server" ImageUrl="../../../Images/Botones/imgLimpiar.gif"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                                            <asp:Image id="imgAdjudicar" style="CURSOR: pointer" ToolTip="Asignar completo. Asocia al profesional seleccionado a todas las tareas dependientes de este nivel. Si ya se encuentra asociado, lo activa." onclick="asignarCompleto()" runat="server" ImageUrl="../../../Images/imgAdjudicar.gif"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                                            <asp:Image id="imgDesactivar" style="CURSOR: pointer" ToolTip="Desasignar completo. Desactiva al profesional seleccionado de todas las tareas dependientes de este nivel, en las que se encuentre asociado." onclick="desAsignarCompleto()" runat="server" ImageUrl="../../../Images/imgDesactivar.gif"></asp:Image>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <label id="lblMostrarBajas" title="Muestra u oculta los profesionales de baja en la empresa o usuarios de baja en SUPER, identificándolos en rojo, o de baja en el proyecto.">Mostrar bajas</label>
                                                    <input type="checkbox" id="chkVerBajas" class="check" onclick="getRecursos();" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Image id="imgDelBajaPE" style="CURSOR: pointer" ToolTip="Borra fecha de baja en el proyecto económico a los usuarios seleccionados." onclick="reAsignar()" runat="server" ImageUrl="../../../Images/imgVolverTrabajo.gif"></asp:Image>
                                                    &nbsp;&nbsp;&nbsp;<asp:Image id="imgCorreo" ToolTip="SUPER notifica la asignación de profesionales a la tarea, por parametrización a nivel de proyecto económico." runat="server" ImageUrl="../../../Images/imgCorreoHabilitado.gif"></asp:Image>                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>&nbsp;&nbsp;&nbsp;&nbsp;
	                            </td>
                            </tr>
                            <tr>
	                            <td>
							        <fieldset style="width:850px; height: 120px;">
								        <legend>Indicaciones del responsable al profesional asignado seleccionado</legend>
                                            <table class="texto" style="width:100%; margin-top:10px;" cellpadding="5px" cellspacing="0" >
                                                <tr>
                                                    <td>Fecha inicio prevista
                                                        <asp:TextBox ID="txtFIPRes" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="oRecActualizar('U', 'fip', this);" />
                                                        &nbsp;&nbsp;Fecha fin prevista 
                                                        <asp:TextBox ID="txtFFPRes" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="oRecActualizar('U', 'ffe', this);" />
                                                        &nbsp;&nbsp;&nbsp;Perfil 
                                                        <asp:DropDownList ID="cboTarifa" runat="server" Width="180px" AppendDataBoundItems="true" onchange="oRecActualizar('U', 'idTarifa', this);">
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
                        <div id="divRecurso" style="z-index:0; position:absolute; left:40px; top:440px; width:840px; height:100px; background-image: url(../../../Images/imgFondoPixelado2.gif); background-repeat:no-repeat;"></div>
                        </eo:PageView>
                        <eo:PageView ID="PageView4" CssClass="PageView" runat="server">	
                        <!-- Pestaña 2.2 Pool de Grupos Funcionales y Técnicos-->
                        <table style="width:860px; text-align:left; margin-left:10px;"> 
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
                                        <br /><br /><span style="color:Red; font-size:9pt;">¡Atención! La activación de esta segunda opción, asignará de forma automática a las tareas dependientes de esta Fase a todos los profesionales pertenecientes al <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> del proyecto, que son <label id="lblNumEmp" style="width:40px;color:Blue; font-size:12pt;" ></label></span>
                                    </fieldset>
                                </td>
                                <td>
                                    <table style="width: 370px; height:17px">
                                        <tr class="TBLINI">
                                            <td style="text-align:center;">Grupos funcionales</td>
                                        </tr>
                                    </table>
                                    <div id="divPoolGF" style="overflow:auto; overflow-x:hidden; width:386px; height:90px">
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
                                <td></td>
                                <td>
                                    <center>
                                        <table style="width:250px; margin-top:10px">
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
                            <tr>
	                            <td colspan="2">
							        <fieldset style="width:850px; height: 310px;">
								        <legend>Profesionales</legend>
                                        <table style="width:830px;" cellpadding="5px">
                                            <colgroup><col style="width:47%;"/><col style="width:6%;"/><col style="width:47%;"/></colgroup>
                                            <tr>
                                                <td>
                                                    <table id="Span1" style="width:375px;" class="texto">
                                                    <colgroup><col style="width:125px;"/><col style="width:125px;"/><col style="width:125px;"/></colgroup>
                                                        <tr>
                                                            <td>Apellido1</td>
                                                            <td>Apellido2</td>
                                                            <td>Nombre</td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtApe1Pool" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos3('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                            <td><asp:TextBox ID="txtApe2Pool" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos3('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                            <td><asp:TextBox ID="txtNomPool" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos3('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td> 
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td><!-- Relación de técnicos -->
		                                            <table id="tblTitPoolAsig" style="width: 370px; height: 17px">
			                                            <tr class="TBLINI">
				                                            <td style="text-align:center;">Relación de profesionales</td>
			                                            </tr>
		                                            </table>
		                                            <div id="divRelacionPool" style="overflow:auto; overflow-x:hidden; width:386px; height:180px" onscroll="scrollTablaPool()">
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
		                                            <table style="width:370px; height:17px">
			                                            <tr class="TBLINI">
				                                            <td style="text-align:center;">Profesionales asignados&nbsp;
						                                        <img id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblAsignados3',0,'divPoolProf','imgLupa5')"
							                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					                                            <img id="imgLupa6" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblAsignados3',0,'divPoolProf','imgLupa5',event)"
							                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						                                    </td>
			                                            </tr>
		                                            </table>
		                                            <div id="divPoolProf" style="overflow: auto; overflow-x:hidden; width:386px; height:180px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaPoolAsig()">
		                                                <div style='background-image:url(../../../Images/imgFT20.gif); width:370px'>
		                                                <%=strTablaPoolProf %>
		                                                </div>
                                                    </div>
		                                            <table style="width: 370px; height: 17px; margin-bottom:8px;">
			                                            <tr class="TBLFIN"><td></td></tr>
		                                            </table>
                                                    <label id="Label9" title="Muestra u oculta los profesionales de baja en la empresa o usuarios de baja en SUPER, identificándolos en rojo, o de baja en el proyecto.">Mostrar bajas</label>
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
               <eo:PageView ID="PageView5" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 3 tareas-->
                <table style="text-align:left; width:100%; margin-left:20px; margin-top:10px;">
                    <tr>
	                    <td>
                            <table style="width: 850px; height: 17px;">
                                <colgroup>
                                    <col style="width:350px"/>
                                    <col style="width:55px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                    <col style="width:55px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                    <col style="width:65px"/>
                                </colgroup>
                                <tr class="TBLINI" align="center">
		                            <td>&nbsp;Denominación</td>
		                            <td title='Esfuerzo total planificado'>ETPL</td>
		                            <td title='Fecha inicio planificada'>FIPL</td>
		                            <td title='Fecha fin planificada'>FFPL</td>
		                            <td title='Esfuerzo total previsto'>ETPR</td>
		                            <td title='Fecha fin prevista'>FFPR</td>
		                            <td title='Fecha inicio vigencia'>FIV</td>
		                            <td title='Fecha fin vigencia'>FFV</td>
		                            <td title='Consumo en horas'>Consumo</td>
                                </tr>
                            </table>
                            <div id="divTareas" style=" overflow: auto; overflow-x: hidden;width: 866px; height:450px">
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
                <eo:PageView ID="PageView7" CssClass="PageView" runat="server">	
                <!-- Pestaña 4 Control-->
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
                <eo:PageView ID="PageView6" CssClass="PageView" runat="server" align="center">	
                <!-- Pestaña 5 Documentación -->
                <br />
                <table style="text-align:left;width:950px;margin-left:25px">
                    <tr>
	                    <td>
		                    <table id="Table2" style="width: 850px; height: 17px">
                            <colgroup>
                                <col style="width:312px"/>
                                <col style="width:213px"/>
                                <col style="width:225px"/>
                                <col style="width:100px"/>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br /><div>Documentación dependiente</div>
		                    <table id="Table6" style="width:850px; height:17px; margin-top:2px;">
                            <colgroup>
                                <col style="width:312px"/>
                                <col style="width:213px"/>
                                <col style="width:225px"/>
                                <col style="width:100px"/>
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
    <table id="tblBotones" style="margin-top:15px;" width="500px">
        <tr>
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
			    <button id="btnGuia" type="button" onclick="mostrarGuia('DetalleFase.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
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
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />
<input type="hidden" name="hdnIDPT" id="hdnIDPT" value="<%=nIdPT %>" />
<input type="hidden" name="hdnIDFase" id="hdnIDFase" value="<%=nIdFase %>" />
<input type="hidden" name="hdnCRActual" runat="server" id="hdnCRActual" value="" />
<input type="hidden" name="hdnDesCRActual" runat="server" id="hdnDesCRActual" value="" />
<input type="hidden" name="hdnIdCliente" id="hdnIdCliente"   value="<%=strIdCliente %>" />
<input type="hidden" name="hdnT305IdProy" id="hdnT305IdProy" runat="server" value="" />
<input type="hidden" name="hdnEstProy" id="hdnEstProy" value="" runat="server"/>
<input type="hidden" name="hdnModoAcceso" id="hdnModoAcceso" value="" runat="server"/>
<input type="hidden" name="hdnEsReplicable" id="hdnEsReplicable" value="" runat="server"/>
<input type="hidden" name="hdnNivelPresupuesto" id="hdnNivelPresupuesto" value="" runat="server"/>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="txtIdPST" runat="server" style="width:2px;visibility:hidden;" readonly="true"></asp:TextBox>
<asp:TextBox ID="txtIdFase" runat="server" SkinID="Numero" style="width:60px;visibility:hidden;" readonly="true"></asp:TextBox>
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
