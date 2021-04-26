<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Pregunta de recordatorio de contraseña de acceso&nbsp;&nbsp;&nbsp;</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../App_Themes/Corporativo/Corporativo.css" type="text/css"/>
     <link rel="stylesheet" href="CSS/Estilos.css" type="text/css"/>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<script type="text/javascript">
<!--
    var bCambios = false;
    var bSalir = false;
    var esPostBack=<%=esPostBack%>;
-->
</script>    
<div id="divPadre" runat="server">
<center>
    <table border="0" cellspacing="0" cellpadding="0"  style="border:1px solid #B5C7DE">
      <tr>
        <td width="6" height="6"></td>
        <td height="6"></td>
        <td width="6" height="6"></td>
      </tr>
      <tr>
        <td width="6">&nbsp;</td>
        <td style="padding:5px">
	        <!-- Inicio del contenido propio de la página -->
            <table style="width:480px;">
                <tr style="height:30px;">
                    <td align="center" style="color:White;background-color:#507CD1; font-weight:bold; font-size:large;">
                       <h1 style="font-size:11px; font-weight:bold;">Establecimiento de pregunta y respuesta</h1>
                    </td>
                </tr>
                <tr>
                    <td>
			            <center>
                            <label class="info">Para acceder a SUPER, debes definir una pregunta y su respuesta para el caso de que olvides tu contraseña.</label>
                        </center>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="border:0">
                            <label style="text-align:center;display:block; width:465px; border :1px solid #f2f2f2;background: #67a2c0;color: #ffffff;padding:5px;"><h1>Aviso legal</h1></label>
                            <div id="divCondicionesLegales" runat="server" style="text-align:right; margin-right:60px; color:#3f7abc;display:none;">
                                <label class="label" style="color:#3f7abc">Condiciones aceptadas el: </label>
                                <span id="fechaAceptacion" class="label" style="color:#3f7abc" runat="server" ></span>
                            </div>
                            <table style="width:500px; text-align:left;" cellpadding="5px" border="0">
                                <tr>
                                    <td colspan="2">
                                            <asp:TextBox ID="txtlegal" SkinID="Multi" style="text-align:left; margin-left:1px; font-weight:bold; text-decoration:none;font: normal 12px Verdana, Helvetica; resize:none;" Text="" TextMode="MultiLine" Columns="74" Rows="8" ReadOnly="true" runat="server">
PROTECCIÓN DE DATOS

De conformidad con lo establecido en el Art. 5 de la Ley Orgánica 15/1999 de diciembre de Protección de Datos de Carácter Personal, por el que se regula el derecho de información en la recogida de datos le informamos de los siguientes extremos:

- Los datos de carácter personal que nos ha suministrado en esta y otras comunicaciones mantenidas con usted serán objeto de tratamiento en los ficheros responsabilidad de IBERMÁTICA S.A.

- La finalidad del tratamiento es la de gestionar de forma adecuada la prestación del servicio que nos ha requerido. Asimismo estos datos no serán cedidos a terceros, salvo las cesiones legalmente permitidas. 

- Los datos solicitados a través de esta y otras comunicaciones son de suministro obligatorio para la prestación del servicio. Estos son adecuados, pertinentes y no excesivos. En ningún caso se utilizarán con motivos publicitarios.

- Su negativa a suministrar y aceptar los datos solicitados implica la imposibilidad prestarle el servicio.

- Aceptar esta información, supone estar de acuerdo con la mensajería que el sistema SUPER utiliza y envía de forma estándar.

- Asimismo, le informamos de la posibilidad de ejercitar los correspondiente derechos de acceso, rectificación, cancelación y oposición de conformidad con lo establecido en la Ley 15/1999 ante IBERMÁTICA S.A como responsable del fichero. Los derechos mencionados los puede ejercitar a través del teléfono 943 413 500.
                                            </asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="checkbox" id="chkAceptar" class="check" onclick="aceptarLegal()" style="" runat="server" />
                                        &nbsp;<label class="label">Acepto</label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset>               
                            <table style="width:500px; text-align:left;" cellpadding="5px" cellspacing="5px" border="0">
                                <tr>
                                    <td>
                                        <label class="label" style="font-weight:bold;" id="lblPregunta">Pregunta</label>
                                        <label style="color:Red;font-weight:bold;">*</label><br /><br />
                                        <asp:TextBox ID="txtPregunta" SkinID="Multi" Text="" TextMode="MultiLine" Columns="60" style="width:425px; overflow:hidden" Rows="1" MaxLength="100" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="label" style="font-weight:bold;" id="lblRespuesta">Respuesta</label>
                                        <label style="color:Red;font-weight:bold;">*</label><br /><br />
                                        <asp:TextBox ID="txtRespuesta" Text="" MaxLength="50" style="width:425px;" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
	            <tr> 
                    <td>
		                <button id="btnGrabar" type="button" onclick="grabarAux();"  runat="server" hidefocus="hidefocus" style="display:inline; margin-top:20px; margin-left:60px;">
			                <span>Aceptar</span>
		                </button>
		                <button id="btnSalir" type="button" onclick="irMenu();" style="margin-left:75px; margin-top:20px;" runat="server" hidefocus="hidefocus">
                            <span title="Salir">&nbsp;Salir</span>
                        </button>
                        <button id="btnAbandonar" type="button" onclick="cancelar();"  runat="server" hidefocus="hidefocus" style="display:none; margin-top:20px; margin-left:60px;">
			                <span>Abandonar</span>
		                </button>
		                <button id="btnSiguiente" type="button" onclick="grabarAux();" style="display:none;margin-left:75px; margin-top:20px;" runat="server" hidefocus="hidefocus">
                            <span title="Siguiente">&nbsp;Siguiente</span>
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
        <td height="6"></td>
        <td width="6" height="6"></td>
      </tr>
    </table>
</center>
</div>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnIdFicepi" id="hdnIdFicepi" value="" />
<input type="hidden" runat="server" name="hdnVez" id="hdnVez" value="" />
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
