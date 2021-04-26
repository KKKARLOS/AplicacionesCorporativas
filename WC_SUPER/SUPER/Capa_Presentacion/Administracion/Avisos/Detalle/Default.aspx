<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de aviso</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
    <script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
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
        var sNumEmpleado = "<%=sNumEmpleado%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        //var sCRcorto = <%=sLectura%>;
        var bSalir = false;
        //Variables a devolver a la estructura.
        var sDescripcion = "";
        var sNumUsers = "";
        var sTexto="";
    </script>    
<table id="tabla" width="920px" style="padding:10px;margin-left:8px">
	<tr>
		<td>
		    <br />
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
							MultiPageID="mpContenido" 
							ClientSideOnLoad="CrearPestanas" 
							ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Profesionales" ToolTip="" Width="100"></eo:TabItem>
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" width="100%" height="520px">
			    <eo:PageView ID="PageView1" CssClass="PageView" runat="server">
				<!-- Pestaña 1 -->
				<br />
                <center>
                    <table style="width:820px;text-align:left;margin-left:80px">
                    <colgroup><col style="width:70px" /><col style="width:500px" /><col style="width:250px" /></colgroup>
                        <tr>
	                        <td>Denominación</td>
	                        <td>
	                            <asp:TextBox ID="txtDen" runat="server" style="width: 450px" MaxLength="50" onKeyUp="aG(0);"></asp:TextBox>
	                        </td>
	                        <td>&nbsp;<br /><br /></td>
                        </tr>
                        <tr>
	                        <td>Título</td>
	                        <td colspan="2">
                                <asp:TextBox ID="txtTit" runat="server" Style="width: 450px" MaxLength="50" onKeyUp="aG(0);"></asp:TextBox>
	                        </td>
                        </tr>                        
                        <tr>
	                        <td colspan="2">
							    <fieldset style="width:250px;float:left;margin-top:10px;height:40px">
								    <legend>Vigencia</legend>
                                    <table align="center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inicio
                                                <asp:TextBox ID="txtValIni" runat="server" style="width:60px;cursor:pointer; margin-right:23px;" Calendar="oCal" onchange="aG(0);" ></asp:TextBox>
                                                Fin
                                                <asp:TextBox ID="txtValFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG(0);"></asp:TextBox>
                                           </td>
                                        </tr>
                                    </table>
                                </fieldset>
							    <fieldset style="width:226px;margin-left:25px;margin-top:10px;float:left;height:40px">
								    <legend>Módulos afectados</legend>
                                    <table align="center" width="100%" cellpadding="5">
                                        <tr>
                                            <td>&nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chkIAP" runat="server" Text="IAP" onClick="aG(0);" ToolTip="" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chkPGE" runat="server" Text="PGE" onClick="aG(0);" ToolTip="" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chkPST" runat="server" Text="PST" onClick="aG(0);" ToolTip="" />
                                           </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td>
                                <div id="divTodosProf" style="visibility:hidden; width:240px;">
                                    <asp:CheckBox ID="chkTodosProf" Width="230px" runat="server" Text="Asignar todos los profesionales" onClick="selTodosProf();" ToolTip="" />
                                </div>
                            </td>
                        </tr>
                        <tr>
	                        <td colspan="3">
                                <br />
                                &nbsp;&nbsp;Texto<br />
                            <asp:TextBox ID="txtDescripcion" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="25" Width="800px" onKeyUp="aG(0);"></asp:TextBox>
	                        </td>
                        </tr>
                    </table>
                </center>
 	            </eo:PageView>

                <eo:PageView ID="PageView2" CssClass="PageView" runat="server">
                <!-- Pestaña 2 Técnicos-->
				<fieldset style="width:870px; margin-left:10px; margin-top:15px;">
				<legend>Selección de profesionales</legend>
                    <table align="center" width="840px" border="0">
                        <colgroup><col style="width:395px;"/><col style="width:40px;"/><col style="width:405px;"/></colgroup>
                        <tr style="height:80px;">
                            <td>
                                <fieldset style="width:351px;">
                                    <legend>Tipo de selección</legend>
                                    <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" RepeatColumns="4" SkinId="rbl" onclick="seleccionAmbito(this.id)">
                                    <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_0').click();" Selected="True" Value="A" Text="Nombre &nbsp;&nbsp;" />
                                    <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_1').click();" Value="C" Text="C.R. &nbsp;&nbsp;" />
                                    <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_2').click();" Value="O" Text="Oficina &nbsp;&nbsp;" />
                                    <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_3').click();" Value="R" Text="Responsable proyecto " />
                                    <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_4').click();" Value="F" Text="Figuras" />
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                            <td>&nbsp;</td>
                            <td rowspan="2">
                                <span id="ambAp" style="display:block" class="texto">
                                    <table style="width: 385px; height: 40px;">
                                        <colgroup><col style="width:130px;" /><col style="width:130px;" /><col style="width:125px;" /></colgroup>
                                        <tr>
                                            <td>Apellido1</td>
                                            <td>Apellido2</td>
                                            <td>Nombre</td>
                                        </tr>
                                        <tr>
                                            <td><asp:TextBox ID="txtApellido" runat="server" Width="115px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                            <td><asp:TextBox ID="txtApellido2" runat="server" Width="115px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                            <td><asp:TextBox ID="txtNombre" runat="server" Width="120px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                        </tr>
                                    </table>
                                </span>
                                <span id="ambCR" style="display:none" class="texto">
                                    <label id="lblNodo" style="width:370px;height:17px" runat="server" class="enlace" onclick="getNodo();">C.R.</label>                                            
                                    <asp:TextBox ID="txtCodCR" runat="server" Text="" Width="1px" style="text-align:right; visibility:hidden" readonly="true" />
                                    <asp:TextBox ID="txtCR" runat="server" Width="370px" readonly="true" />
                                </span>
                                <span id="ambOfi" style="display:none" class="texto">
                                    <label id="Label1" style="width:40px;height:17px" runat="server" class="enlace" onclick="getOfi();">Oficina</label>
                                    <asp:TextBox ID="txtCodOfi" runat="server" Text="" Width="1px" style="visibility:hidden" readonly="true" />
                                    <asp:TextBox ID="txtDenOfi" runat="server" Width="370px" readonly="true" />
                                </span>
                                <span id="ambFig" style="display:none" class="texto">
                                    <fieldset style="width:366px; height:75px;">
                                        <legend>
                                            <label id="lblFiguras" class="enlace" onclick="if (this.className=='enlace') getFiguras()" runat="server">Figuras</label>
                                            <img id="Img20" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarFiguras()" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                                        </legend>
                                        <div id="divFiguras" style="overflow:auto; width:356px; height:64px;">
                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:340px">
                                                <table id="tblFiguras" style="width:340px;">
                                                </table>
                                            </div>
                                        </div>
                                    </fieldset>	
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-left:330px;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblRelacion')" />&nbsp;
                                <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblRelacion')" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top;">
                                <table id="tblTitRec" style="WIDTH: 365px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" border="0">
                                    <tr class="TBLINI">
		                                <td>&nbsp;Profesionales&nbsp;
			                                <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblRelacion',1,'divRelacion','imgLupa1')"
				                                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
		                                    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblRelacion',1,'divRelacion','imgLupa1',event)"
				                                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
			                            </td>
                                    </tr>
                                </table>
                                <div id="divRelacion" style="OVERFLOW: auto; WIDTH: 381px; height:300px" onscroll="scrollTablaProf()">
                                    <div style='background-image:url(../../../../Images/imgFT20.gif); width:365px'>
                                        <table id="tblRelacion" style="WIDTH: 365px;">
                                            <colgroup><col style='width:20px;' /><col style='width:345px;' /></colgroup>
                                        </table>
                                    </div>
                                </div>
                                <table style="WIDTH: 365px; HEIGHT: 17px; margin-bottom:7px;" cellSpacing="0" border="0">
                                    <tr class="TBLFIN"><td></td></tr>
                                </table>
                                <img border="0" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
                                <img border="0" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                                <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
                                <label id="lblForaneo" runat="server">Foráneo</label>
                            </td>
                            <td style="vertical-align:middle; text-align:left;">
				                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                            </td>
                            <td style="vertical-align:top;"><!-- Técnicos asignados -->
                                <table id="tblTitRecAsig" style="width: 380px; height: 17px">
                                    <tr class="TBLINI">
	                                    <td>&nbsp;Profesionales asignados&nbsp;
			                                <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblAsignados',2,'divAsignados','imgLupa3')"
				                                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
		                                    <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblAsignados',2,'divAsignados','imgLupa3',event)"
				                                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
			                            </td>
                                    </tr>
                                </table>
                                <div id="divAsignados" style="OVERFLOW: auto; width: 396px; height:300px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
                                    <div style='background-image:url(../../../../Images/imgFT20.gif); width:380px;'>
                                    <%=strTablaRecursos %>
                                    </div>
                                </div>
                                <table style="width: 380px; height: 17px; margin-bottom:3px;">
                                    <TR class="TBLFIN"><TD></TD></TR>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<center>
    <table id="tblBotones" align="center" style="margin-top:15px;"  width="70%">
        <tr>
	        <td align="center">
			    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnGuia" type="button" onclick="mostrarGuia('DetalleAviso.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGuia.gif" /><span title="Guía">Guía</span>
			    </button>	
	        </td>				
	        <td align="center">
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>

    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnIdAviso" id="hdnIdAviso" value="<%=nIdAv %>" />

    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <div class="clsDragWindow" id="DW" noWrap></div>
</form>
<script type="text/javascript">
		function __doPostBack(eventTarget, eventArgument) {
			var bEnviar = true;

			var theform;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.forms[0];
			}
			else {
				theform = document.forms["frmPrincipal"];
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
