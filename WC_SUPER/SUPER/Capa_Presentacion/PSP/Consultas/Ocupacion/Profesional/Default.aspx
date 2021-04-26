<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_Ocupacion_Profesional_Default" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de ocupación del profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var opener = window.dialogArguments;
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
        var bCambios = false;
        var bLectura = true;
        var bSalir = false;
        var nIndiceProy = -1;
        var aProy = new Array();
    -->
    </script>    
<br />
<center>
<table style="width:1000px;text-align:left;">
    <tr>
        <td>
        <fieldset style="width: 966px; margin-left:15px;">
        <legend>&nbsp;Criterios de selección&nbsp;</legend>
        <table name='tblCatalogo' id='tblCatalogo' align='left' style="width:950px" cellspacing='5' cellpadding='5'>
            <tr>
                <td width="600px">
                    <label id="lblProy" class="enlace" onclick="obtenerTecnicos()" style="width: 70px;height:17px">Profesional</label>
                    <asp:TextBox ID="txtNombreTecnico" runat="server" Text="" Width="500px" readonly="true" />
                    <asp:TextBox ID="txtCodTecnico" runat="server" Text="" Width="1px" style="visibility:hidden;" readonly="true" />
                </td>
                <td width="366px">    
                    Desde
                    <asp:textbox id="txtFechaInicio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('D');"></asp:textbox>
                    &nbsp;&nbsp;Hasta
                    <asp:textbox id="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('H');"></asp:textbox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="chkBusqAutomatica" hidefocus="true"  class="check" type="checkbox" runat="server" onclick="compBusAuto()" checked="checked">&nbsp;Búsq. automática
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label style="width: 70px;">Escala</label>
                    <asp:DropDownList ID="cboVista" runat="server" onchange="vista();">
                    <asp:ListItem Value="M" Text="Mensual"></asp:ListItem>
                    <asp:ListItem Value="D" Text="Diaria" selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        </fieldset>
        </td>
    </tr>
    <tr>
	    <td>
        <fieldset style="width: 966px; margin-left:15px;">
        <legend>&nbsp;Porcentajes de ocupación&nbsp;</legend>
           <table style="width: 950px;">
                <tr>
                    <td>
		                <div id="divCatalogo" style="overflow: auto; overflow-x:hidden; width: 950px; height:484px;" runat="server">
 		                    <table id='tblDatos' style='width: 950px;'></table>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        </td>
    </tr>	
</table>
<br />
<table width="500px" >
	<tr> 
		<td>      
		    <button id="btnBuscar" type="button" onclick="buscar2();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../../images/botones/imgSalir.gif" /><span title="Buscar">&nbsp;&nbsp;Buscar</span>
		    </button>		      
		</td>
		<td>
            <button id="btnExcel" type="button" onclick="excel();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../images/botones/imgExcel.gif" /><span title="Excel">&nbsp;&nbsp;Excel</span>
            </button>      
		</td>
		<td>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		    </button>	
		</td>
	</tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:textbox id="hdnFechaDesde" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnFechaHasta" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnListaProf" runat="server" style="visibility:hidden"></asp:textbox>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>

<script type="text/javascript">
	<!--
		function __doPostBack(eventTarget, eventArgument) {
		    var bEnviar = true;
		    if (eventTarget.split("$")[2] == "Botonera") {
		        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
				switch (strBoton){
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
