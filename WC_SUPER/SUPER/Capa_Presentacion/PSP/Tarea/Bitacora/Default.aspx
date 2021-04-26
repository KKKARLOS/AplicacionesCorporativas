<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Bitácora de tarea</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload();" style="margin-left:15px;">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    var bSalir = false;
    var bLectura = false;
    mostrarProcesando();

    var num_proyecto = "<%=Session["NUM_PROYECTO"].ToString().Replace(".","") %>";
    
    var nIndicePT = -1;
    var aPT = new Array();
    var sAccesoBitacoraT = "<% =sAccesoBitacoraT %>";
</script>  
<br />  
<table id="tblCab" style="width:990px;">
    <colgroup><col style="width:80px;"/><col style="width:65px;"/><col style="width:845px;"/></colgroup>
    <tr>
        <td>
            <label id="lblProy" title="Proyecto económico" style="width:50px; height:16px;" class="enlace" onclick="obtenerProyectos()">Proyecto</label>
            <asp:Image ID="imgEstProy" runat="server" style="width:16px; height:16px;" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
        </td>
        <td>
            <asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:55px;" SkinID="Numero" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);setNumPE();setGomas();}" />
        </td>
        <td>
            <asp:TextBox ID="txtNomProy" runat="server" Text="" style="width:409px; vertical-align:top; margin-top:1px;" readonly="true" />
            <img id="gomPE" src="../../../../Images/Botones/imgBorrar.gif" border="0" onclick="borrarPE();" title="Borrar proyecto económico" style="cursor:pointer; margin-left:6px;" />
        </td>    
    </tr>
    <tr>
        <td>
            <label id="lblPT" class="enlace" style="width:80px; height:16px;" onclick="obtenerPTs()">P. técnico</label>
        </td>    
        <td colspan="2">
            <asp:TextBox ID="txtDesPT" runat="server" style="width:480px" readonly="true" />
            <asp:Image ID="gomPT" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarPT();" style="cursor:pointer; vertical-align:baseline; margin-left:5px;" />
            <asp:TextBox ID="hdnIdPT" runat="server" style="visibility:hidden; width:1px" readonly="true" />
        </td>  
    </tr>
    <tr>
        <td>
            <label id="lblFase" class="enlace" style="width:80px; height:16px;margin-top:3px;" onclick="obtenerFases()">Fase</label>
        </td>    
        <td colspan="2">
            <asp:TextBox ID="txtFase" runat="server" style="width:480px" readonly="true" />
            <asp:Image ID="gomF" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarFase();" style="cursor:pointer; vertical-align:baseline; margin-left:5px;" />
            <asp:TextBox ID="hdnIdFase" runat="server" style="visibility:hidden; width:1px" readonly="true" />
        </td>   
    </tr>
    <tr>
        <td>
            <label id="lblActividad" class="enlace" style="width:80px; height:16px; margin-top:3px;" onclick="obtenerActividades()">Actividad</label>
        </td>    
        <td colspan="2">
            <asp:TextBox ID="txtActividad" runat="server" style="width:480px" readonly="true" />
            <asp:Image ID="gomA" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarActividad();" style="cursor:pointer; vertical-align:baseline; margin-left:5px;" />
            <asp:TextBox ID="hdnIdActividad" runat="server" style="visibility:hidden; width:1px" readonly="true" />
        </td>  
    </tr>
    <tr>
        <td valign="top">
            <label id="lblTarea" class="enlace" style="width:70px; height:16px; margin-top:2px;" onclick="obtenerTareas()">Tarea</label>
        </td>    
        <td valign="top">
            <asp:TextBox ID="txtIdTarea" runat="server" MaxLength="7" SkinID="Numero" style="width:55px; margin-top:2px;" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarTarea();}else{vtn2(event);setGomas();}"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtDesTarea" runat="server" SkinID="multi" runat="server" TextMode="MultiLine" Rows="2" style="width:810px; margin-left:2px; vertical-align:top; margin-top:3px;" readonly="true"></asp:TextBox>
            <asp:Image ID="gomT" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarTarea();" style="cursor:pointer; vertical-align:baseline; margin-left:5px; margin-top:2px;" />
        </td>    
    </tr>
</table>
<fieldset style="width:970px;">
<legend>Asunto</legend>
    <table style="width:100%; margin-top:5px;">
        <tr>
            <td style="text-align:left;">
                <label id="Label1" class="texto" style="width:50px;">Tipo</label>
                <asp:DropDownList ID="cboTipo" runat="server" AppendDataBoundItems="true" onchange="buscar();">
                    <asp:ListItem Selected="True" Value="0" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label id="Label2" class="texto" style="margin-left:30px" >Estado</label>
                <asp:DropDownList ID="cboEstado" runat="server" onchange="buscar();">
                    <asp:ListItem Value="0" Text="" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Registrado"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Asignado"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Resuelto"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Verificado"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Aprobado"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Reabierto"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table1" style="width: 940px; height: 17px; margin-top:8px; text-align:left;">
                    <colgroup>
                        <col style="width:300px;" /><col style="width:190px;" /><col style="width:87px;" /><col style="width:90px;" />
                        <col style="width:88px;" /><col style="width:90px;" /><col style="width:95px;" />
                    </colgroup>
	                <tr class="TBLINI">
		                <td style="padding-left:3px;">
		                    <img style="cursor: pointer; width:6px; height:11px;" src="../../../../Images/imgFlechas.gif" useMap="#imgAsunto" border="0" /> 
					        <map id="imgAsunto" name="imgAsunto">
						        <area onclick="ordenarTablaAsuntos(4,0)" shape="rect" coords="0,0,6,5" />
						        <area onclick="ordenarTablaAsuntos(4,1)" shape="rect" coords="0,6,6,11" />
					        </map>&nbsp;Denominación&nbsp;
					        <img id="imgLupa1" style="height:11px; display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos1',0,'divAsunto','imgLupa1',event);" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
					        <img id="img2" style="height:11px; display:none; cursor: pointer" onclick="buscarDescripcion('tblDatos1',0,'divAsunto','imgLupa1',event);" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" />
					    </td>
		                <td>
		                    <img style="cursor: pointer;height:11px; width:6px;" src="../../../../Images/imgFlechas.gif" useMap="#imgTipo" border="0" /> 
					        <map id="imgTipo" name="imgTipo">
						        <area onclick="ordenarTablaAsuntos(1,0)" shape="rect" coords="0,0,6,5" />
						        <area onclick="ordenarTablaAsuntos(1,1)" shape="rect" coords="0,6,6,11" />
					        </map>Tipo
					    </td>
		                <td>
		                    <img style="cursor: pointer; height:11px; width:6px;" src="../../../../Images/imgFlechas.gif" useMap="#imgSeveridad" border="0" /> 
					        <map id="imgSeveridad" name="imgSeveridad">
						        <area onclick="ordenarTablaAsuntos(17,0)" shape="rect" coords="0,0,6,5" />
						        <area onclick="ordenarTablaAsuntos(17,1)" shape="rect" coords="0,6,6,11" />
					        </map>Severidad
					    </td>
		                <td>
		                    <img style="cursor: pointer;height:11px; width:6px;" src="../../../../Images/imgFlechas.gif" useMap="#imgPrioridad" border="0" /> 
					        <map id="imgPrioridad" name="imgPrioridad">
						        <area onclick="ordenarTablaAsuntos(13,0)" shape="rect" coords="0,0,6,5" />
						        <area onclick="ordenarTablaAsuntos(13,1)" shape="rect" coords="0,6,6,11" />
					        </map>Prioridad
					    </td>
		                <td>
		                    <img style="cursor: pointer;height:11px; width:6px;" src="../../../../Images/imgFlechas.gif" useMap="#imgFLim" border="0" /> 
					        <map id="imgFLim" name="imgFLim">
						        <area onclick="ordenarTablaAsuntos(10,0)" shape="rect" coords="0,0,6,5" />
						        <area onclick="ordenarTablaAsuntos(10,1)" shape="rect" coords="0,6,6,11" />
					        </map>Límite
					    </td>
		                <td>
		                    <img style="cursor: pointer; height:11px; width:6px;" src="../../../../Images/imgFlechas.gif" useMap="#imgFNot" border="0" /> 
					        <map id="imgFNot" name="imgFNot">
						        <area onclick="ordenarTablaAsuntos(11,0)" shape="rect" coords="0,0,6,5" />
						        <area onclick="ordenarTablaAsuntos(11,1)" shape="rect" coords="0,6,6,11" />
					        </map>Notificación
					    </td>
		                <td>
		                    <img style="cursor: pointer; height:11px; width:6px;" src="../../../../Images/imgFlechas.gif" useMap="#imgEstado" border="0" /> 
					        <map id="imgEstado" name="imgEstado">
						        <area onclick="ordenarTablaAsuntos(5,0)" shape="rect" coords="0,0,6,5" />
						        <area onclick="ordenarTablaAsuntos(5,1)" shape="rect" coords="0,6,6,11" />
					        </map>Estado
					    </td>
	                </tr>
                </table>
                <div id="divAsunto" style="overflow: auto; width: 956px; height:200px">
                    <div style='background-image:url(../../../../Images/imgFT16.gif); width:940px;'>
                    <%=strTablaHtmlAsunto %>
                    </div>
                </div>
                 <table style="width:940px; height: 17px; margin-bottom:5px;">
                    <tr class="TBLFIN"><td>&nbsp;</td></tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <center>
                <button id="btnAAs" type="button" onclick="nuevoAsunto()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                </button>    
                <button id="btnBAs" type="button" onclick="eliminarAsunto()" class="btnH25W90" style="display:inline; margin-left:30px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../Images/Botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                </button> 
                </center>   
            </td>
        </tr>    
</table>
</fieldset>
<center>
<fieldset style="width:680px; margin-top:10px; text-align:left;">
<legend>Acción</legend>
<table style="width:666px; margin-top:5px; text-align:left">
    <tr>
        <td>
            <table id="Table2" style="width: 650px; height: 17px">
                <colgroup><col style="width:420px;" /><col style="width:70px;" /><col style="width:70px;" /><col style="width:90px;" /></colgroup>
	            <tr class="TBLINI">
                    <td style="padding-left:3px;">
                        <img style="cursor: pointer; width:6px; height:11px;" src="../../../../Images/imgFlechas.gif" useMap="#imgAccion" border="0" /> 
                        <map id="imgAccion" name="imgAccion">
                            <area onclick="ordenarTablaAcciones(5,0)" shape="rect" coords="0,0,6,5" />
                            <area onclick="ordenarTablaAcciones(5,1)" shape="rect" coords="0,6,6,11" />
                        </map>&nbsp;Denominación&nbsp;
                        <img id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2',event);"
		                                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
                        <img id="imgLupa5" style="display: none; cursor: pointer" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2',event);"
		                                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" />
                    </td>
                    <td style="padding-left:5px;">
                        <img style="cursor: pointer; width:6px; height:11px;" src="../../../../Images/imgFlechas.gif" useMap="#imgAcFLi" border="0" /> 
                            <map id="imgAcFLi" name="imgAcFLi">
                                <area onclick="ordenarTablaAcciones(7,0)" shape="rect" coords="0,0,6,5" />
                                <area onclick="ordenarTablaAcciones(7,1)" shape="rect" coords="0,6,6,11" />
                            </map>&nbsp;Límite
                    </td>
                    <td style='text-align:right; padding-right:5px;'>
                        <img style="cursor: pointer; width:6px; height:11px;" src="../../../../Images/imgFlechas.gif" useMap="#imgAcAva" border="0" /> 
                            <map id="imgAcAva" name="imgAcAva">
                                <area onclick="ordenarTablaAcciones(4,0)" shape="rect" coords="0,0,6,5" />
                                <area onclick="ordenarTablaAcciones(4,1)" shape="rect" coords="0,6,6,11" />
                            </map>&nbsp;Avance
                    </td>
                    <td style="padding-left:5px";>
                        <img style="cursor: pointer; width:6px; height:11px;" src="../../../../Images/imgFlechas.gif" useMap="#imgAcFin" border="0" /> 
                            <map id="imgAcFin" name="imgAcFin">
                                <area onclick="ordenarTablaAcciones(6,0)" shape="rect" coords="0,0,6,5" />
                                <area onclick="ordenarTablaAcciones(6,1)" shape="rect" coords="0,6,6,11" />
                            </map>&nbsp;Finalización
                    </td>
	            </tr>
            </table>
            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:666px; height:60px">
                <div style='background-image:url(../../../../Images/imgFT16.gif); width:650px;'>
                <%=strTablaHtmlAccion%>
                </div>
            </div>
            <table style="width:650px; height:17px">
                <tr class="TBLFIN"><td></td></tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <center>
                <button id="btnAAc" type="button" onclick="nuevoAccion()" class="btnH25W90" style="display:inline; margin-top:10px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                </button>    
                <button id="btnBAc" type="button" onclick="eliminarAccion()" class="btnH25W90" style="display:inline; margin-left:25px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../Images/Botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                </button>    
            </center>
        </td>
    </tr>
</table>
</fieldset>
<br />
<table style="width:120px;">
	<tr> 
		<td>
            <button id="btnSalir" type="button" onclick="salir()" class="btnH25W90" style="margin-left:10px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
            </button>    
		</td>
	  </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<input type="hidden" runat="server" name="hdnOrigen" id="hdnOrigen" value="" />
<asp:TextBox ID="txtUne" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<asp:TextBox ID="txtEstado" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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
			
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.__EVENTARGUMENT.value = eventArgument;
			if (bEnviar){
				theform.submit();
			}
			else{
				$I("Botonera").restablecer();
			}
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

