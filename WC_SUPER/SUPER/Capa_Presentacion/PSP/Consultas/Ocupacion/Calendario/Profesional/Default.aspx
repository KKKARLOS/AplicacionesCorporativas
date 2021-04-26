<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_Ocupacion_Calendario_Profesional_Default" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de ocupación por tareas del profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var opener = window.dialogArguments;
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
//        var bCambios = false;
//        var bLectura = true;
//        var bSalir = false;
    -->
    </script>    
<br />
<center>
<table style="width:800px; text-align:left">
    <tr>
        <td>
        <fieldset style="width: 730px; margin-left:15px">
        <legend>&nbsp;Profesional&nbsp;</legend>
        <table name='tblCatalogo' class="texto" id='tblCatalogo' align='left' width='700px' border='0' cellspacing='0' cellpadding='0'>
            <tr>
                <td width="60px">&nbsp;</td>
                <td width="640px">
                    <asp:TextBox ID="txtNombreTecnico" runat="server" Text="" Width="600px" readonly="true" />
                    <asp:TextBox ID="txtCodTecnico" runat="server" Text="" Width="1px" style="visibility:hidden;" readonly="true" />
                </td>
            </tr>
        </table>
        </fieldset>
        </td>
    </tr>
    <tr>
	    <td>
           <table style="width: 750px;">
                <tr>
                    <td>
                        <table id="tblTitulo" style="width: 730px; height: 17px; margin-left:15px">
                            <colgroup>
                                <col style="width: 70px;" />
                                <col style="width: 400px;" />
                                <col style="width: 60px;" />
                                <col style="width: 60px;" />
                                <col style="width: 50px;" />
                            </colgroup>
	                        <tr class="TBLINI">
	                            <td align="center">&nbsp;&nbsp;Tarea</td>
	                            <td>Denominación</td>
	                            <td align="right">Previsto</td>
	                            <td align="right">Consumido</td>
	                            <td align="right">Pendiente</td>
		                    </tr>
		                </table>
		                <div id="divCatalogo" style="overflow: auto; width: 746px; height:448px; margin-left:15px" runat="server">
 		                    <table id='tblDatos' style='width: 730px;'></table>
                        </div>
                        <table id="tblResultado" style="width: 730px; height: 17px; margin-left:15px" >
	                        <tr class="TBLFIN">
                                <td>&nbsp;</td>
			                </tr>
		                </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>	
</table>
<br />
<table style="width:200px">
	<tr> 
		<td>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
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
		function __doPostBack(eventTarget, eventArgument) {
		    var bEnviar = true;
		    if (eventTarget.split("$")[2] == "Botonera") {
		        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

		        switch (strBoton) {
//					case "regresar": 
//					{
//					    comprobarGrabarOtrosDatos();
//						bEnviar = true;
//						break;
//					}
				}
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
    	
    -->
</script>
</body>
</html>
