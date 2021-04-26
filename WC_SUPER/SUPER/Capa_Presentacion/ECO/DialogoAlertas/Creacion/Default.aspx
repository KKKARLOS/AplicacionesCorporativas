<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Creación de diálogo</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="CSS/estilos.css" type="text/css"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
    var idPSN = "<%=sPSN %>";
    var bInterlocutor = <%=sEsInterlocutor %>;
    var bGestor = <%=sEsGestor %>;
    var sToday = "<%=DateTime.Today.ToShortDateString() %>";
-->
</script>    
<table id="tblDatos" style="width:500px; margin-left:10px; margin-top:10px;" cellpadding="3" cellspacing="0" border="0">
    <colgroup>
        <col style='width:80px;' />
        <col style='width:420px;' />
    </colgroup>
	<tr>
	    <td>Asunto<asp:Label ID="lblReqAsunto" runat="server" ForeColor="Red" style="margin-left:3px" Text="*" /></td>
	    <td><asp:DropDownList ID="cboAsunto" runat="server" style="width:400px;" AppendDataBoundItems="true" />
	        <asp:Label ID="lblAsunto" runat="server" Visible="false" Text=""></asp:Label> </td>
	</tr>
	<tr>
	    <td><label id="lblMes" style="width:30px;" title="Mes de cierre">Mes</label></td>
	    <td><asp:TextBox ID="txtMes" runat="server" style="width:90px; text-align:center; cursor:pointer;" onclick="getMes()" ReadOnly="true" /><asp:TextBox ID="hdnMes" runat="server" style="width:1px; visibility:hidden;" ReadOnly="true" /><img id="imgGoma" src='../../../../Images/Botones/imgBorrar.gif' title="Borra el mes" onclick="delMes()" style="cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;">
	        <label id="lblFLR" style="width:30px; margin-left:30px; visibility:hidden;" title="Fecha límite de respuesta">FLR</label><asp:TextBox ID="txtFLR" runat="server" style="width:60px;visibility:hidden;" ReadOnly="true" Calendar="oCal" goma="0" onchange="setFLR()" /><img id="imgGomaFLR" src='../../../../Images/Botones/imgBorrar.gif' title="Borra el mes" onclick="delFLR()" style="cursor:pointer; margin-left:5px; vertical-align:middle; border:0px; visibility:hidden;">
	    </td>
	</tr>
	<tr>
	    <td style="vertical-align:top;">Mensaje<asp:Label ID="Label1" runat="server" ForeColor="Red" style="margin-left:3px" Text="*" /></td>
	    <td><asp:TextBox ID="txtMensaje" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="10" Width="395px"></asp:TextBox></td>
	</tr>
</table>
<center>
<table style="width:220px; margin-top:10px;" align="center">
    <colgroup>
        <col style="width:100px" />
        <col style="width:100px" />
    </colgroup>
	<tr> 
		<td align="center">
            <button id="btnNuevo" type="button" onclick="tramitar();" class="btnH25W100" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgTramitar.gif" /><span">Tramitar</span>
            </button>				   
		</td>
		<td align="center">
            <button id="btnSalir" type="button" onclick="salir();" class="btnH25W100" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgCancelar.gif" /><span>Cancelar</span>
            </button>				   
		</td>
	  </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff" runat="server" />
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

