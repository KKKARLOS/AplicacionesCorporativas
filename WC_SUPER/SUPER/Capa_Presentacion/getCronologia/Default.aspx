<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Cronología de estado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
  	<style type="text/css">
  	    #tblDatosHistorial tr { min-height: 18px; }
  	    #tblDatosHistorial td { padding: 3px 2px 2px 2px; line-height:14px; }
  	</style>
</head>
<body onload="init();" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
</script>
<table cellspacing="0" class="texto" style="width: 620px; table-layout:fixed; margin-left:10px; margin-top:10px;" border="0">
    <tr>
        <td>
            <table id="tblTituloHistorial" style="width:600px; height:17px;">
                <colgroup>					
                    <col style="width:150px;" />
                    <col style="width:100px;" />
                    <col style="width:350px;" />
                </colgroup>
                <tr class="TBLINI">				    
                    <td>Estado</td>
                    <td>Fecha</td>
                    <td>Profesional / Motivo</td>
                </tr>
            </table>
            <div id="divCatalogoHistorial" style="overflow:auto; overflow-x:hidden; width:616px; height:300px" runat="server">
                <div style="width:600px; ">
                    <%=strTablaHTML %>
                </div>
            </div>
            <table id="tblPieHistorial" style="width:600px; height:17px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br /><br />
<center>
    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	     onmouseover="se(this, 25);mostrarCursor(this);">
	    <img src="../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
    </button>	
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnTabla" id="hdnTabla" value="" />
<input type="hidden" runat="server" name="hdnKey" id="hdnKey" value="-1" />
<input type="hidden" runat="server" name="hdnKey2" id="hdnKey2" value="-1" />

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
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

