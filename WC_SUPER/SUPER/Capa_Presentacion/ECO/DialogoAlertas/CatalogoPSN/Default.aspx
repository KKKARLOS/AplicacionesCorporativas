<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Diálogos por proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="CSS/estilos.css" type="text/css"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
    var bNuevo = <% =sNuevo %>;
    var idPSN = "<%=idPSN %>";
    var sRestringirOCyFACerrados = "<%=sRestringirOCyFACerrados %>";
-->
</script>    
<table id="Table1" style="width:960px; margin-left:15px; margin-top:15px;">
    <tr>
        <td style="text-align:right; margin-right:20px;"><input type="checkbox" id="chkSoloAbiertos" class="check" onclick="getDialogos();" style="margin-right:3px; cursor:pointer; vertical-align:middle;" runat="server" checked="checked" /><asp:Label ID="lblMostrarSoloAbiertos" runat="server" Text="Mostrar sólo abiertos" style="cursor:pointer; vertical-align:middle;" onclick="this.previousSibling.click();" /></td>
    </tr>
	<TR>
		<TD>
			<table id="tblTitulo" style="width:960px; height:17px;" cellpadding='0' cellspacing='0' border='0'>
			    <colgroup>
	                <col style='width:350px;' />
	                <col style='width:120px;' />
	                <col style='width:250px;' />
	                <col style='width:80px;' />
	                <col style='width:80px;' />
	                <col style='width:80px;' />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td><img id="imgFA1" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA1">
                        <map name="imgEA1">
		                    <area onclick="ot('tblDatos', 0, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 0, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Asunto</td>
					<td><img id="imgFA2" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA2">
                        <map name="imgEA2">
		                    <area onclick="ot('tblDatos', 1, 0, 'mes', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 1, 1, 'mes', '');" shape="rect" coords="0,6,6,11">
	                    </map>Mes de cierre</td>
					<td><img id="imgFA3" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                        <map name="imgEA3">
		                    <area onclick="ot('tblDatos', 2, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 2, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Estado</td>
					<td><img id="imgFA4" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                        <map name="imgEA4">
		                    <area onclick="ot('tblDatos', 3, 0, 'fec', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 3, 1, 'fec', '');" shape="rect" coords="0,6,6,11">
	                    </map>Creación</td>
					<td title="Fecha límite de respuesta"><img id="imgFA5" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA5">
                        <map name="imgEA5">
		                    <area onclick="ot('tblDatos', 4, 0, 'fec', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 4, 1, 'fec', '');" shape="rect" coords="0,6,6,11">
	                    </map>F.L.R.</td>
					<td title="Fecha de último mensaje"><img id="imgFA6" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA6">
                        <map name="imgEA6">
		                    <area onclick="ot('tblDatos', 5, 0, 'fec', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 5, 1, 'fec', '');" shape="rect" coords="0,6,6,11">
	                    </map>F.U.M.</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 976px; height: 420px;">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:960px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
		</TD>
    </TR>
</table>
<center>
<table style="width:220px; margin-top:10px;" align="center">
    <colgroup>
        <col style="width:100px" />
        <col style="width:100px" />
    </colgroup>
	<tr> 
		<td align="center">
            <button id="btnNuevo" type="button" onclick="addDialogo();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgAnadir.gif" /><span title="Crear nuevo diálogo">Añadir</span>
            </button>				   
		</td>
		<td align="center">
            <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgSalir.gif" /><span>Salir</span>
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

