<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Recuperar contraseña&nbsp;&nbsp;&nbsp;</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../App_Themes/Corporativo/Corporativo.css" type="text/css"/>
    <link rel="stylesheet" href="CSS/Estilos.css" type="text/css"/>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init()" >
<form id="Form1" name="frmPrincipal" runat="server">
<div id="divRecuperarPass" style="width:500px; margin-top:5px;" runat="server">
<center>
<table border="0" width="480px" cellspacing="0" cellpadding="0" style="border:1px solid #B5C7DE">
    <tr>
        <td width="6" height="6"></td>
        <td height="6"></td>
        <td width="6" height="6"></td>
    </tr>
    <tr>
        <td width="6">&nbsp;</td>
        <td style="padding:5px">
        <!-- Inicio del contenido propio de la página -->
            <table style="width:455px; height:30px;">
            <tr>             
                <td align="center" style="color:White;background-color:#507CD1; font-weight:bold; font-size:large;">
                   <h1> Recuperar contraseña</h1>
                </td>
            </tr>
            </table>
            <asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="Large" ></asp:Label>
            <div id="divUser" style="visibility:hidden; margin-left:110px; margin-top:80px;">
            <table id="tblDatos" cellpadding="5px" style="width:455px; text-align:left; font-size:medium;">
                <tr>
                    <td><label style="font-weight:bold;margin-right:5px;color:#666;text-decoration:none;font: normal 14px Verdana, Helvetica;">Introduzca su código de usuario</label></td>                    
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" MaxLength="15" Width="190px" 
                            onkeypress="if(event.keyCode==13){event.keyCode=0;grabar();}">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
            </div>
            <div id="divPregunta" style="visibility:hidden;">
            <table style="width:455px; text-align:left;" cellpadding="1px" border="0">
                <colgroup><col style="width:130px;"/><col style="width:325px;"/></colgroup>
                <tr>
                    <td colspan="2">
                        <label id="lblPregunta" class="info" style="margin-left:30px;">
                            Responda a la siguiente pregunta para visualizar su contraseña.
                        </label>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td></td>
                    <td>
                        <span>¿</span><label id="txtPregunta" style="margin-left:4px;font-weight:bold;margin-right:5px;color:#666;text-decoration:none;font: normal 14px Verdana, Helvetica;" runat="server" /><span>?</span>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td>
                        <label id="lblRespuesta" style="font-weight:bold;margin-left:30px;color:#666;text-decoration:none;font: normal 12px Verdana, Helvetica;">Respuesta: </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRespuesta" Text="" MaxLength="50" style="width:252px; " 
                                onkeypress="if(event.keyCode==13){event.keyCode=0;grabar();}" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divPassw" style="visibility:hidden; margin-left:130px; margin-top:20px; font-size:medium;">
                            <label style="font-size:14px" class="label">Su contraseña es:</label> <label id="lblPassw" style="font-size:14px;font-weight:bold;" class="label" runat="server"></label>
                        </div>
                    </td>
                </tr>
            </table>
            </div>
            <table style="width:500px; margin-top:30px; margin-left:65px;">
	            <tr> 
                    <td>
		                <button id="btnGrabar" type="button" onclick="grabar();" runat="server" hidefocus="hidefocus" style="display:inline;">
			                <span>&nbsp;Aceptar</span>
		                </button>
		                <button id="btnSalir" type="button" onclick="salir();"  runat="server" hidefocus="hidefocus" style="display:inline; margin-left:30px;">
			                <span title="Salir">&nbsp;Salir</span>
		                </button>
		            </td>
	              </tr>
            </table>
        <!-- Fin del contenido propio de la página -->
        </td>
        <td width="6">&nbsp;</td>
    </tr>
    <tr>
        <td width="6" height="6"></td>
        <td height="6" ></td>
        <td width="6" height="6"></td>
    </tr>
</table>
</center>
</div>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdFicepi" id="hdnIdFicepi" value="" />
<input type="hidden" name="hdnRespuesta" id="hdnRespuesta" value="" runat="server" />
</form>
<script type="text/javascript">
	<!--
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
    	
    -->
</script>
</body>
</html>
