<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Exportar estructura en formato XML</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    var bSalir = false;
    var bLectura = false;
</script>  
<br /><br />  
<center>
<table border="0" cellspacing="0" cellpadding="0" width="580px">
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
    </tr>
    <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding:15px;text-align:left;" class="texto">
            <br />
            <lu>
                <li>
                    Openproj no permite proyectos en los que la fecha de fin planificada en la línea base sea superior a la fecha de fin prevista.
                    <br /><br />
                    Si alguna de las tareas a exportar se encuentra en esta situación, la fecha de fin prevista se exportará con el valor de la fecha de fin planificada.
                    <br /><br />
                    Si selecciona exportar incluyendo línea base, el proceso le notificará si existe alguna tarea en esa situación, permitiéndole cancelar la exportación o continuar con ella.
                    <br /><br />
                </li>
                <li>
                    Openproj no permite avance superior al 100%. Por ello en caso de que lo consumido en una tarea sea superior al esfuerzo previsto, se tomara como esfuerzo el valor de lo consumido.
                    <br /><br />
                </li>
                <li>
                    Openproj no permite tareas sin fecha de inicio, fecha de fin o duración. Para aquellas tareas que no cumplan esta premisa 
                    se establecerá como fecha la del día actual y duración 8 horas.
                </li>
            </lu>
            <br /><br />
	    </td>
        <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
    </tr>
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
        <td height="6" background="../../../../Images/Tabla/2.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
    </tr>
</table>
<br /><br />
<table align="center" style="margin-top:15px;" border=0 cellpadding=0 cellspacing=0 width="60%" class=texto>
<tr>
    <td colspan="3" align="center">
        <input hideFocus id="chkLineaBase" class="check" onclick="" type="checkbox"  checked runat="server" />&nbsp;&nbsp;Incluir línea base
        <br /><br />
    </td>
</tr>
</table>	
<center>
    <table style="margin-top:15px;" width="70%">
        <tr>
	        <td align="center">
			    <button id="btnProcesar" type="button" onclick="procesar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgProcesar.gif" /><span title="Procesar">&nbsp;&nbsp;Procesar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnAyuda" type="button" onclick="mostrarGuia('ExportaraOpenProj.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGuia.gif" /><span title="Guía">Guía</span>
			    </button>	
	        </td>				
        </tr>
    </table>
</center>
</center>

<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" id="hdnPSN" value="" />
<input type="hidden" runat="server" id="hdnAvisar" value="N" />
<input type="hidden" runat="server" id="hdnRTPT" value="0" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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


