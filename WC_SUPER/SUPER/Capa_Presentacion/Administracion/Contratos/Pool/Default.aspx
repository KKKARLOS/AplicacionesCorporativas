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
<head runat="server">
    <title> ::: SUPER ::: - Detalle de Pool de FVP</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css"/>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/ddfiguras.css" type="text/css"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />       
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>      
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="Functions/ddfiguras.js" type="text/Javascript"></script>
	<script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script> 	
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
        #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        //var sOrigen = "<%= Request.QueryString["origen"] %>";
        //<!--<iewc:Tab Text="Extensiones" ToolTip=""></iewc:Tab>-->
    -->
    </script>  
<br />      
<center>
<table id="tabla" style="padding:10px;width:980px;text-align:left">
	<tr>
		<td>
        <!-- Figuras-->
            <br />
            <table id="dragDropContainer" style="margin-left:15px; width:940px; text-align:left;">
                <colgroup><col style="width:420px" /><col style="width:80px" /><col style="width:370px" /><col style="width:75px" /></colgroup>
			    <tr>
				    <td style="vertical-align:bottom; margin-bottom:5px;">
					    <table style="width:400px;">
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
							    <td style="text-align:right;" >
							        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras1')" />&nbsp;
							        <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras1')" />
							    </td>
						    </tr>
					    </table>
				    </td>
				    <td style="vertical-align:middle;">
				        <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();"><img src="../../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>
                    </td>
                    <td style="padding-bottom:3px;">                      
                        <fieldset class="fld" style="width:305px; height:50px; margin-left:50px;">
				        <legend class="Tooltip" title="Pinchar y arrastrar" unselectable=on>Selector de figuras</legend>
 				        <div id="listOfItems" style="height:50px;">
                            <ul id="allItems"  style="width:300px;">
                                <li id="D" value="1" onmouseover="mcur(this)"><img src="../../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" ondragstart="return false;" /> Delegado</li>
                                <li id="C" value="2" onmouseover="mcur(this)"><img src="../../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" ondragstart="return false;" /> Colaborador</li>
                                <li id="J" value="3" onmouseover="mcur(this)"><img src="../../../../Images/imgJefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Jefe" /> Jefe</li>
                                <li id="M" value="4" onmouseover="mcur(this)"><img src="../../../../Images/imgSubjefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Responsable técnico de proyecto económico" /> RTPE</li>
                                <li id="B" value="5" onmouseover="mcur(this)"><img src="../../../../Images/imgBitacorico.gif" onmouseover="mcur(this)" title="Bitacórico" ondragstart="return false;" /> Bitacórico</li>
                                <li id="S" value="6" onmouseover="mcur(this)"><img src="../../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" ondragstart="return false;" /> Asistente</li>
                                <li id="I" value="7" onmouseover="mcur(this)"><img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> Invitado</li>
                            </ul>
                        </div>
                        </fieldset>	
				    </td>	
                    <td style="vertical-align:bottom; text-align:right; padding-right:20px;">
                        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" />
                        <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;padding-left:2px; margin-right:5px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />  									        																			
					</td>			    								
			    </tr>
			    <tr>
				    <td style="vertical-align:top;">
					    <table id="tblTituloFiguras1" style="height:17px;width:400px">
					        <colgroup><col style="width:400px"/></colgroup>
						    <tr class="TBLINI">
							    <td>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1')"
		                        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras1',1,'divFiguras1','imgLupaFigurasA1',event)"
		                        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
		                        </td>
						    </tr>
					    </table>
					    <div id="divFiguras1" style="overflow:auto; overflow-x:hidden; width:416px; height:300px;" align="left" onscroll="scrollTabla()">
                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:400px">
                            </div>
					    </div>
					    <table style="height:30px; width:400px;">
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
				    <td align="center">
					    <asp:Image id="imgPapeleraFiguras" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras'"></asp:Image>
				    </td>
				    <td colspan="2">
					    <table id="tblTituloFiguras2" style="height:17; width:420px;">
					        <colgroup><col style="width:20px"/><col style="width:300px"/><col style="width:100px"/></colgroup>
						    <tr class="TBLINI">
						        <td style="text-align:center;"></td>
							    <td><img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2" border="0">
					                <map name="imgFiguras2">
					                    <area onclick="ot('tblFiguras2', 2, 0, '')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblFiguras2', 2, 1, '')" shape="RECT" coords="0,6,6,11">
				                    </map>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2')"
		                        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2',event)"
		                        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
		                        </td>
						        <td>Figuras</td>
						    </tr>
					    </table>
					    <div id="mainContainer">
					        <div id="divFiguras2" style="overflow:auto; overflow-x:hidden; width:436px; height:300px;" align="left" target="true" onmouseover="setTarget(this);" caso="1">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:420px; height:auto;">
					                <table id="tblFiguras2" class="texto MANO" width="420px">
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
					    <table id="tblResultado2" style="height:30px; width:420px;">
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
        </td>
    </tr>
</table>
<table id="tblBotones" style="margin-top:15px; width:60%;">
    <tr>			
	    <td>
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" disabled="disabled"
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td>
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" disabled="disabled"
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
<asp:textbox id="hdnID" runat="server" style="visibility:hidden"></asp:textbox> 

<div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy; left:350px; top:250px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br />
        - Un profesional no puede ser simultáneamente:<br><br />
        1.- Delegado y Colaborador.<br />
        2.- Delegado e Invitado.<br />
        3.- Delegado y RTPE.<br />
        4.- Colaborador e Invitado.<br />
        5.- Jefe y RTPE.<br />
        6.- Colaborador y RTPE.<br />
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
