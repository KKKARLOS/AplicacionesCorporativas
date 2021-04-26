<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Cambio de contraseña&nbsp;&nbsp;&nbsp;</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../App_Themes/Corporativo/Corporativo.css" type="text/css"/>
    <link rel="Stylesheet" href="CSS/Estilos.css" type="text/css"/>

	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init()" >
<form id="Form1" name="frmPrincipal" runat="server">
<div id="divCambioPass" style="width:500px; margin-top:30px;" runat="server">
<center>
    <table border="0" cellspacing="0" cellpadding="0" style="border:1px solid #B5C7DE">     
      <tr>
        <td width="6">&nbsp;</td>
        <td style="padding:5px">
	    <!-- Inicio del contenido propio de la página -->
    	    <table id="tblDatos" cellpadding="5px" style="width:500px;text-align:left; margin-left:0px;">
    	        <colgroup>
    	            <col style="width:150px" />
    	            <col style="width:250px"/>
    	        </colgroup>
                <tr style="height:30px;">
                    <td align="center" colspan="2" style="color:White;background-color:#507CD1; font-weight:bold; font-size:large;">
                        <h1>Cambio de contraseña</h1>
                    </td>
                </tr>
				<tr style="height:30px;"><td colspan="2"></td></tr>
    	        <tr>
    	            <td><label style="font-weight:bold;margin-right:5px;color:#666;text-decoration:none;font: normal 14px Verdana, Helvetica;">Usuario:</label></td>
    	            <td><asp:TextBox ID="txtUser" runat="server"  Text="" ReadOnly="true"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td><label style="font-weight:bold;margin-right:5px;color:#666;text-decoration:none;font: normal 14px Verdana, Helvetica;">Contraseña:</label></td>
    	            <td><asp:TextBox ID="txtPasswAnt" runat="server" MaxLength="10" TextMode="Password" onkeypress="if(event.keyCode==13){event.keyCode=0;ponerFoco('txtPasswAct');}"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td><label style="font-weight:bold;margin-right:5px;color:#666;text-decoration:none;font: normal 14px Verdana, Helvetica;">Nueva contraseña:</label></td>
    	            <td>
    	                <asp:TextBox ID="txtPasswAct" runat="server" MaxLength="10" TextMode="Password" 
    	                    onkeypress="if(event.keyCode==13){event.keyCode=0; verfificarPasswActual();}"
    	                    onkeydown="if(event.keyCode==9){verificarPasswActual();}">
    	                </asp:TextBox>
    	            </td>
    	        </tr>
    	        <tr>
    	            <td><label style="font-weight:bold;margin-right:5px;color:#666;text-decoration:none;font: normal 14px Verdana, Helvetica;">Repetir contraseña:</label></td>
    	            <td>
    	                <asp:TextBox ID="txtRepPassw" runat="server" MaxLength="10"  TextMode="Password" onkeypress="if(event.keyCode==13){event.keyCode=0;grabar();}">
    	                </asp:TextBox>
    	            </td>
    	        </tr>
    	        <tr style="height:30px;"><td colspan="2"></td></tr>
	            <tr> 
                    <td colspan="2">
                        <button id="btnGrabar" type="button" onclick="grabar();" style="margin-left:50px" runat="server" hidefocus="hidefocus"">
                            <span>&nbsp;Aceptar</span>
                        </button>
                        <button id="btnSalir" type="button" onclick="salir();" style="margin-left:75px" runat="server" hidefocus="hidefocus">
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

<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdFicepi" id="hdnIdFicepi" value="" runat="server" />
<input type="hidden" name="hdnPassw" id="hdnPassw" value="" runat="server" />
</div>
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
