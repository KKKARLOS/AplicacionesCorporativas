<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Calendario de profesionales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
    <script type="text/javascript" language="javascript">
	    function cerrarVentana(){
		    var returnValue = 0;
		    modalDialog.Close(window, returnValue);	
	    }
	</script>
</head>
<body onload="init()" onunload="unload()" style="margin-top:10px;">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">

    var strServer = "<% =Session["strServer"].ToString() %>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    var bSalir = false;
    var bLectura = <%=sLectura%>;
    var sIdFicepiEmpleado = "<%=Session["IDFICEPI_PC_ACTUAL"].ToString() %>";

</script>  
<center>  
<table style="width:950px; text-align:left" cellpadding="2px">
<colgroup>
    <col style="width:395px;" />
    <col style="width:55px;" />
    <col style="width:50px;" />
    <col style="width:395px;" />
    <col style="width:55px;" />
</colgroup>
    <tr>
        <td style="vertical-align:bottom;">
            <input type="checkbox" id="chkMostrarBajas" class="check" onclick="getUsuarios();" style="vertical-align:middle;" />&nbsp;&nbsp;
            <label id="lblMostrarBajas">Mostrar bajas</label>
        </td>
        <td style="vertical-align:middle;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
        </td>
        <td></td>
        <td style="vertical-align:bottom;">
            <label id="lblCalDestino" class="enlace" onclick="getCalendario()">Calendario</label>
            <asp:TextBox ID="txtCalDestino" runat="server" Width="316px" readonly="true" />
        </td>
        <td style="vertical-align:middle;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width: 430px; height: 17px">
                <colgroup>
                    <col style="width:230px;" />
                    <col style="width:200px;" />
                </colgroup>
                <tr class="TBLINI">
                    <td style="padding-left:20px">Usuario</td>
                    <td style='padding-left:5px;'>Calendario origen</td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow-y: auto; overflow-x: hidden; width: 446px; height:540px" onscroll="scrollTablaProf()" runat="server">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:430px">
                    <%=strTablaHTMLIntegrantes%>
                </div>
            </div>
            <table style="width: 430px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:middle; text-align:center;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
        </td>
        <td  colspan="2">
            <table id="tblTituloAsignados" style="width: 430px; height: 17px">
                <colgroup>
                    <col style="width:220px;" />
                    <col style="width:210px;" />
                </colgroup>
                <tr class="TBLINI">
                    <td style="padding-left:20px">Usuario</td>
                    <td style='padding-left:10px;'>Calendario destino</td>
                </tr>
            </table>
            <div id="divCatalogo2" style="overflow: auto; overflow-x:hidden; width: 446px; height:540px" target="true" onmouseover="setTarget(this)" caso="1" onscroll="scrollTablaProfDest()">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:430px">
                <table id="tblDatos2" style="width: 430px;" class="texto MM" >
                <colgroup>
                    <col style="width:20px;" />
                    <col style="width:210px;" />
                    <col style="width:200px;" />
                </colgroup>
                </table>
                </div>
            </div>
            <table style="width: 430px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table style="width: 940px; text-align:left;">
    <tr>
        <td>
            <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
<table style="margin-top:15px; width:260px;">
	<tr> 
		<td>
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">&nbsp;Grabar...</span>
			</button>	  
		</td>
		<td>
			<button id="btnSalir" type="button" onclick="salir()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="">&nbsp;&nbsp;Salir</span>   
			</button>  		
		</td>
	  </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnPSN" id="hdnPSN" value=""  runat="server"/>
<input type="hidden" name="hdnIdCalIni" id="hdnIdCalIni" value=""  runat="server"/>
<input type="hidden" name="hdnIdCal" id="hdnIdCal" value=""  runat="server"/>
<input type="hidden" name="hdnCualidad" id="hdnCualidad" value=""  runat="server"/>
<input type="hidden" name="hdnIdNodo" id="hdnIdNodo" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
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
