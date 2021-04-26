<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getDetalle.aspx.cs" Inherits="getDetalle" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Detalle de dato modificado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function salir(){
	    try{
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
	-->
    </script>
</head>
<body style="OVERFLOW: hidden" class=texto leftMargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="10px" />
        <table border="0" cellpadding="0" cellspacing="0" style="width:600px; margin-left:10px;" align="center">
            <tr>
                <td background="../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                        <table style='WIDTH: 570px;'>
                        <colgroup>
                            <col style="width:110px; font-weight:bold;" />
                            <col />
                        </colgroup>
                        <tr style="height:20px;">
                            <td>Tabla:</td>
                            <td id="cldTabla" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Campo:</td>
                            <td id="cldCampo" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Acción:</td>
                            <td id="cldAccion" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Qué:</td>
                            <td id="cldQue" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Quién:</td>
                            <td id="cldQuien" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Cuándo:</td>
                            <td id="cldCuando" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Valor antiguo:</td>
                            <td id="cldAntiguo" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Valor nuevo:</td>
                            <td id="cldNuevo" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Usuario sistema:</td>
                            <td id="cldUsuSis" runat="server"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td>Equipo:</td>
                            <td id="cldEquipo" runat="server"></td>
                        </tr>
                        </table>
                    <!--
                    </div>
                    </div> -->
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
<center>
<table style="width:100px; margin-top:15px;" border="0" class="texto" align="center">
    <colgroup>
        <col style="width:100px" />
    </colgroup>
	  <tr> 
		<td>
			<button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
		</td>
	  </tr>
</table>
</center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
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
