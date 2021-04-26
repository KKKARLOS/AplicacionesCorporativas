<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando1" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
-->
</script>
<br />
<table style="width:766px; margin-left:10px;" class="texto">
	<tr>
		<td>
			<table id="tblTitulo" style="height:17px; width:750px;">
			    <colgroup>
                    <col style="width:20px;" />
                    <col style="width:60px;" />
                    <col style="width:420px;" />
                    <col style="width:250px;" />
			    </colgroup>
				<tr class="TBLINI">
					<td colspan="2" style="text-align:right; padding-left:2px;">
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblDatos', 1, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 1, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td style="padding-right:2px; text-align: right;">
					    <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					    <MAP name="img2">
					        <AREA onclick="ot('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Proyecto&nbsp;
				        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa2')"
						    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa2', event)"
						    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
					<td>
					    <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
					    <MAP name="img3">
					        <AREA onclick="ot('tblDatos', 3, 0, '', '')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 3, 1, '', '')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Cliente&nbsp;
				        <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa3')"
						    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa3', event)"
						    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
			<div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 766px; HEIGHT: 480px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:750px; height:auto">
                <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:750px;">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table style="width:700px; margin-top:10px;" class="texto">
    <tr> 
        <td>
            <img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
        </td>
    </tr>
</table>
<center>
<table style="width:100px; margin-top:10px;" class="texto">
    <tr> 
        <td>
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
        </td>
    </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
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
</script>
</body>
</html>

