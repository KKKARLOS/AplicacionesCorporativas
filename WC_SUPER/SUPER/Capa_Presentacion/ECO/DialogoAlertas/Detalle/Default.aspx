<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Diálogo de alerta</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    var bSalir = false;
    var bEsGestorDialogo = <%=sEsGestorDialogo %>;
    var bEsAdm = <%=gsEsAdm %>;
    var bEsInterlocutorProyecto = <%=sEsInterlocutorProyecto %>;
    var sToday = "<%=DateTime.Today.ToShortDateString() %>";
    var nIDFicepi = <%=Session["IDFICEPI_PC_ACTUAL"].ToString() %>;
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
    mostrarProcesando();
-->
</script>    
<br />
<table id="tabla" style="width:900px; margin-left:10px;" cellpadding="5" border="0">
<colgroup>
    <col style="width:450px;" />
    <col style="width:450px;" />
</colgroup>
    <tr>
        <td><label id="lblDialogo" style="width:50px;">Diálogo</label>
            <asp:TextBox ID="txtIdDialogo" runat="server" SkinID="Numero" style="width:60px" ReadOnly="true" />
            <label id="lblMes" style="width:30px; margin-left:36px;" title="Mes de cierre">Mes</label>
            <asp:TextBox ID="txtMes" runat="server" style="width:90px; text-align:center;" ReadOnly="true" />
            <label id="lblFLR" style="width:30px; margin-left:30px;" title="Fecha límite de respuesta">FLR</label>
            <asp:TextBox ID="txtFLR" runat="server" style="width:60px" ReadOnly="true" Calendar="oCal" onchange="grabar();" /></td>
        <td>
            <label id="lblProyecto" style="width:50px;">Proyecto</label>
            <asp:TextBox ID="txtProyecto" runat="server" style="width:350px" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td><label id="lblAsunto" style="width:50px;">Asunto</label>
            <asp:TextBox ID="txtAsunto" runat="server" style="width:350px" ReadOnly="true" />
            <asp:DropDownList ID="cboAsunto" runat="server" style="width:353px; display:none;" onchange="$I('hdnIdDAlerta').value=this.value;grabar();" AppendDataBoundItems="true" />
        </td>
        <td><label id="lblEstado" style="width:50px;">Estado</label><asp:TextBox ID="txtEstado" runat="server" style="width:350px" ReadOnly="true" /></td>
    </tr>
</table>
<table id="TABLE2" style="width:900px; margin-top:5px; margin-left:10px;" cellpadding="0" border="0">
    <tr>
        <td>
        <table  class="texto" style="width:880px; height:17px;">
            <tr class="TBLINI">
                <td style="text-align:center;">Conversación</td>
            </tr>
        </table>
        <div id="divDialogo" style="overflow: auto; overflow-x: hidden; width: 896px; height: 520px;">
            <div style='width:880px; height:520px; background-color:#ece4de;'>
                <%=strHTMLCaso%>
            </div>
        </div>
        </td>
    </tr>
</table>

<table style="width:80%; margin-top:10px; margin-left:110px;">
	<tr> 
		<td align="center">
            <button id="btnAddMensaje" type="button" class="btnH30W145" onmouseover="se(this, 30)" style="margin-top:3px;" disabled="disabled"
                    onclick="addMensaje();" runat="server" hidefocus="hidefocus">
                <img src="../../../../images/botones/imgAddMensaje.png" /><span>Añadir mensaje</span>
            </button>
		</td>
		<td align="center">
            <button id="btnDocumentacion" type="button" class="btnH30W145" runat="server" hidefocus="hidefocus" disabled="disabled"
                onclick="mostrarDocumentos();" onmouseover="se(this, 30);mostrarCursor(this);">
                <img id="imgDocFact" src="../../../../images/imgCarpeta32.png" runat="server" /><span>Documentación</span>
            </button>
		</td>
		<td align="center">
            <button id="btnCerrarDialogo" type="button" class="btnH30W145" runat="server" hidefocus="hidefocus" disabled="disabled"
                onclick="cerrarDialogo();" onmouseover="se(this, 30);mostrarCursor(this);">
                <img id="imgBtnEstado" src="../../../../images/botones/imgCerrarDialogo.png" runat="server" /><span id="spanBtnEstado">Cerrar diálogo</span>
            </button>
		</td>
		<td align="center">
            <button id="btnSalir" type="button" class="btnH30W140" runat="server" hidefocus="hidefocus"
                onclick="salir();" onmouseover="se(this, 30);mostrarCursor(this);">
                <img src="../../../../images/Botones/imgSalir.png" /><span>Salir</span>
            </button>
		</td>
	  </tr>
</table>
<br />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdDialogo" id="hdnIdDialogo" value="-1" runat="server" />
<input type="hidden" name="hdnIdDAlerta" id="hdnIdDAlerta" value="-1" runat="server" />
<input type="hidden" name="hdnIdEstado" id="hdnIdEstado" value="-1" runat="server" />
<input type="hidden" name="hdnIdResponsableProy" id="hdnIdResponsableProy" value="-1" runat="server" />
<input type="hidden" name="hdnEntePromotor" id="hdnEntePromotor" value="-1" runat="server" />
<input type="hidden" name="hdnHayDocs" id="hdnHayDocs" value="N" runat="server" />
<uc_mmoff:mmoff ID="mmoff" runat="server" />
</form>
<script type="text/javascript">
	<!--
    setOp($I("btnAddMensaje"), 30);
    setOp($I("btnDocumentacion"), 30);
    setOp($I("btnCerrarDialogo"), 30);
	
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
