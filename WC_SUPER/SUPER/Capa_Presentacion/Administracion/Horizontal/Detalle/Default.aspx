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
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de Divis?n horizontal</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="Functions/ddfiguras.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
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
        var sOrigen = "<%= Request.QueryString["origen"] %>";

    </script> 
<br />      
<center>   
<table id="tabla" style="width:980px; text-align:left">
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
				<!-- Pesta?a 1 General--><br />
                <table class="texto" width="940px" cellpadding="5px" cellspacing="0px">
                    <colgroup>
                        <col style="width:100px;" />
                        <col style="width:840px;" />
                    </colgroup>
                    <tr style="vertical-align:middle;">
	                    <td>Denominaci?n&nbsp;&nbsp;</td>
	                    <td>
	                        <asp:TextBox ID="txtDenominacion" runat="server" style="width:500px;" MaxLength="50" Text="" onkeyup="aG(0);"/>
	                    </td>
                    </tr>
                    <tr>
	                    <td><label id="lblResponsable" class="enlace" style="cursor:pointer;height:17px" onclick="getResponsable()">Responsable</label></td>
                        <td>
                            <asp:TextBox ID="txtDesResponsable" style="width:500px;" Text="" readonly="true" runat="server" />
                            <asp:TextBox ID="hdnIDResponsable" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
	                    </td>
                    </tr>
                </table>
			    </eo:PageView>
			
			    <eo:PageView CssClass="PageView" runat="server" style="text-align:center;">
                <!-- Pesta?a 2 Figuras-->
			    <table id="dragDropContainer" class="texto" style="width:940px; margin-left:15px; text-align:left;">
			        <colgroup>
			            <col style="width:440px" /><col style="width:50px" /><col style="width:75px" /><col style="width:375px" />
			        </colgroup>
				    <tr>
					    <td>
					        <br />
						    <table style="WIDTH: 400px; margin-top:5px;">
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
								    <td style="text-align:right;"><img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selecci?n a todas las filas" onclick="DesmarcarTodo('tblFiguras1')" /></td>
							    </tr>
						    </table>
					    </td>
					    <td>&nbsp;</td>		
					    <td style="vertical-align:middle;">
					        <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();">
					            <img src="../../../../Images/imgBoxeo.gif" style="width:73px; height:24px;" border="0" /><br /><u>Incompatibilidades</u>
					        </div>
	                    </td>
	                    <td style="vertical-align:top;">
	                        <table style="width:325px; text-align:left; margin-left:20px;">
	                            <colgroup><col style="width:260px"/><col style="width:65px"/></colgroup>
	                            <tr>
	                                <td style="text-align:center;">		                        
	                                    <FIELDSET class="fld" style="width:180px; height:34px; text-align:center">
							                <LEGEND class="Tooltip" title="Pinchar y arrastrar">Selector de figuras</LEGEND>
         				                        <div id="listOfItems" style="height:25px;">
                                                    <ul id="allItems"  style="width:180px;">
                                                        <li id="D" value="1" onmouseover="mcur(this)"><img src="../../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" /> Delegado</li>
                                                        <li id="I" value="2" onmouseover="mcur(this)"><img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" /> Invitado</li>
                                                    </ul>
                                                </div>
                                        </FIELDSET>
                                    </td>
                                    <td style="text-align:right; vertical-align:bottom;">
                                        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" />
                                        <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer;" title="Quitar selecci?n a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />
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
									    <IMG id="imgLupaFigurasA1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1',0,'divFiguras1','imgLupaFigurasA1')"
				                            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				                        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1',0,'divFiguras1','imgLupaFigurasA1', event)"
				                            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                    </td>
								</tr>
							</table>
							<div id="divFiguras1" style="overflow:auto; overflow-x:hidden; width: 416px; height:300px; text-align:left;" onscroll="scrollTabla()">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:400px">
                                </div>
							</div>
							<table style="height:30px; width:400px; text-align:left;">
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
							<asp:Image id="imgPapeleraFiguras" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3" ToolTip="Elimina el profesional seleccionado del cat?logo 'Profesionales/Figuras'"></asp:Image>
						</td>
						<td colspan="2" style="vertical-align:top;">
							<table id="tblTituloFiguras2" style="height:17px; width:420px;">
							    <colgroup><col style="width:20px;"/><col style="width:300px;"/><col style="width:100px;"/></colgroup>
								<tr class="TBLINI">
								    <td></td>
									<td>
									    <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2" border="0">
							            <MAP name="imgFiguras2">
							                <AREA onclick="ot('tblFiguras2', 2, 0, '')" shape="RECT" coords="0,0,6,5">
							                <AREA onclick="ot('tblFiguras2', 2, 1, '')" shape="RECT" coords="0,6,6,11">
						                </MAP>&nbsp;Profesionales&nbsp;
						                <IMG id="imgLupaFigurasA2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2',1,'divFiguras2','imgLupaFigurasA2')"
				                            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				                        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2',1,'divFiguras2','imgLupaFigurasA2', event)"
				                            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				                    </td>
								    <td>Figuras</td>
								</tr>
							</table>
							<div id="mainContainer">
							    <div id="divFiguras2" style="overflow:auto; overflow-x:hidden; width:436px; height:300px; text-align:left;" target="true" onmouseover="setTarget(this);" caso="1">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px; height:auto;">
							            <table id="tblFiguras2" class="texto MANO" style="width:420px;">
							                <colgroup>
							                    <col style="width:20px;" /><col style="width:20px;" /><col style="width:280px;" /><col style="width:100px;" />
							                </colgroup>
							                <tbody id='tbodyFiguras2'>
							                </tbody>
							            </table>
							        </div>
							    </div>
							</div>
							<table id="tblResultado2" style="height:17px; width:420px;">
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
<table id="tblBotones" style="margin-top:15px;" width="500px;">
    <tr>
		<td>
			<button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgNuevo.gif" /><span title="Nuevo">&nbsp;Nuevo</span>
			</button>	
	    </td>	
	    <td>
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td>
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
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
<input type="hidden" id="hdnDenominacion" value="" />
<asp:textbox id="hdnNueva" runat="server" style="visibility:hidden"></asp:textbox> 
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:400px;
         top:30px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br>
        - Un profesional no puede ser simult?neamente:<br><br>
        1.- Delegado e Invitado.<br>
</div>
<ul id="dragContent" class="texto"></ul>
<div id="dragDropIndicator"><img src="../../../../images/imgSeparador.gif"></div>
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
