<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de estructura organizativa</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
	<link href="css/estilos.css" type="text/css" rel="stylesheet" />
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow:hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    var sSnds = opener.sSubnodos;
</script>
<center>
<table style="width: 920px; margin-top:10px; margin-left:10px; text-align:left">
<colgroup><col style="width:440px;" /><col style="width:80px;" /><col style="width:400px;" /></colgroup>
<tr>
    <td style="text-align:right;">
        <asp:Label ID="lblMostrarInactivos" AssociatedControlID="chkMostrarInactivos" style="cursor:pointer" runat="server" Text="Mostrar inactivos" /> 
        <input type="checkbox" id="chkMostrarInactivos" class="check" onclick="MostrarInactivos();" style="margin-right:15px;" runat="server" />
    </td>
    <td colspan="2"></td>
</tr>
<tr>
    <td>
        <table id="tblTitulo" style="WIDTH: 430px; HEIGHT: 17px; margin-top:3px;">
            <colgroup>
                <col style="width:430px;" />
            </colgroup>
            <tr class="TBLINI">
                <td style="padding-left:35px;">Denominación</td>
            </tr>
        </table>
        <div style="OVERFLOW:auto; overflow-x:hidden; width: 446px; height:340px;" runat="server">
            <div id="divCatalogo" runat="server" style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:430px; height:auto;">
            </div>
        </div>
        <table id="tblResultado" style="width: 430px; height: 17px;" >
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
    </td>
    <td style="vertical-align:middle; text-align:center;">
        <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
    </td>
    <td>
        <table id="tblAsignados" style="width: 350px; height: 17px">
            <tr class="TBLINI">
                <td style="padding-left:3px">
                    Elementos seleccionados
                    &nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
				</td>
            </tr>
        </table>
        <div id="divCatalogo2" style="OVERFLOW: auto; overflow-x:hidden; width: 366px; height:340px" target="true" onmouseover="setTarget(this);" caso="2">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:350px; height:auto;">
            <table id="tblDatos2" style="WIDTH: 350px; table-layout:fixed;" class="texto MM" >
                <colgroup><col style="width:350px;" /></colgroup>
            </table>
            </div>
        </div>
        <table style="width: 350px; height: 17px">
            <tr class="TBLFIN">
                <td></td>
            </tr>
        </table>
    </td>
    </tr>
</table>

<table style="width:300px; margin-top:10px;">
	<tr>
		<td>
            <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
            </button>
		</td>
		<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	</tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnExcede" id="hdnExcede" value="F" runat="server" />
<input type="hidden" name="hdnOrigen" id="hdnOrigen" value="ECO" runat="server" />
<div class="clsDragWindow" id="DW" noWrap></div>
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
