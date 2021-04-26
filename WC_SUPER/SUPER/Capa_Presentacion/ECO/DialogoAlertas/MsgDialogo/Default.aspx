<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - <%= sTitulo%></title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" >
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        ocultarProcesando();
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var sUsuarioResponsable = "<%=sUsuarioResponsable%>";  //1 / 0 si el usuario es responsable del proyecto
    </script>    
<br />
<center>
<table id="tabla" cellspacing="0" cellpadding="0" style="text-align:center; width:590px;" class="texto">
    <tr>
        <td>
            <asp:TextBox ID="txtMsg" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="10" style="margin-left:5px; font-size:11pt; width:580px;"></asp:TextBox>
        </td>
    </tr>
</table>
<br /><br />
<table width="40%" border="0" class="texto" style="text-align:center;" >
	<tr> 
        <td style="text-align:center;">
            <button id="btnGrabarSalir" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                onclick="grabarSalir();" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgEnviarMsg.gif" /><span>Enviar</span>
            </button>
        </td>
		<td style="text-align:center;">
            <button id="btnSalir" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                onclick="cancelar();" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/Botones/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	  </tr>
</table>
<br />
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnPosicionMsg" id="hdnPosicionMsg" value="I" runat="server" />
<input type="hidden" name="hdnIdDialogo" id="hdnIdDialogo" value="-1" runat="server" />
<uc_mmoff:mmoff ID="mmoff" runat="server" />
</form>
<script type="text/javascript">
	<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        var theform;
        theform = document.forms[0];
//        if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
//            theform = document.forms[0];
//        }
//        else {
//            theform = document.forms["frmPrincipal"];
//        }

        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
        else {
            document.getElementById("Botonera").restablecer();
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
    	
    -->
</script>
</body>
</html>
