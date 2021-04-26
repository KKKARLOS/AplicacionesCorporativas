<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="GEMO.BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comunicación de consumos</title>
	<link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	-->
    </script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
    <form id="form1" runat="server">
        <ucproc:Procesando ID="Procesando" runat="server" />    
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table border="0" class="texto" width="250px" style="margin-left:10px;margin-top:20px;" cellpadding="0" cellspacing="0">
        <colgroup><col style="width:250px;" /></colgroup>
        <tr style="height:20px;">
            <td align="center">
	            Fecha&nbsp;factura
	            <asp:DropDownList ID="cboFechaFra" runat="server" style="width:80px;" AppendDataBoundItems=true>
	            </asp:DropDownList>            
            </td>
        </tr>
    </table>
	    <table width="300px" align="center" style="margin-top:105px;">
		        <tr>
			        <td align="center">
				        <button id="btnAceptar" type="button" onclick="aceptar()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				         onmouseover="se(this, 25);mostrarCursor(this);">
					        <img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">&nbsp;&nbsp;Aceptar</span>
				        </button>    
			        </td>	

			        <td align=center>
				        <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				         onmouseover="se(this, 25);mostrarCursor(this);">
					        <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
				        </button>    
			        </td>
		        </tr>
	    </table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
<script type="text/javascript">
	<!--
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
