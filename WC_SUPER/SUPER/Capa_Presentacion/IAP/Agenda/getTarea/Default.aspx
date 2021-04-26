<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Selección de tarea</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="navegador();init();" onunload="unload()" style="margin-left:15px;">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
    -->
    </script>
<center>
<table class="texto" id="table1" style="margin-top:15px; width:620px;">
  <tr>
    <td>
        <img id="imgNE1" src='../../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);">
        <img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);">
        <img id="imgNE3" src='../../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);">
    </td>
  </tr>
	<tr>
		<td>
			<table id="tblTitulo" style="width:600px; height:17px">
				<tr class="TBLINI" style="height:17px; text-align:left;"> 				
                    <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event);"
                     height="11px" src="../../../../Images/imgLupa.gif" width="20px" tipolupa="1"> 
                    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
                    height="11px" src="../../../../Images/imgLupaMas.gif" width="20px" tipolupa="2">&nbsp;Denominación
                    </td>
				</tr>
			</table>
			<div id="divCatalogo" style="OVERFLOW: auto; overflow-x:hidden; WIDTH: 616px; HEIGHT: 506px;" align="left">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT22.gif'); width:600px;">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:600px; height:17px">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
</center>
<table width="620px" class="texto" style="margin-top:5px; margin-left:5px;">
    <colgroup>
        <col style="width:70px" />
        <col style="width:180px" />
        <col style="width:370px" />
    </colgroup>
	<tr> 
        <td></td>
        <td></td>
		<td rowspan="2"><br />
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	  </tr>
    <tr>
        <td style="vertical-align:top;">
            <img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto
         </td>
        <td style="vertical-align:top;">
            <img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
        </td>
        <td></td>
    </tr>
</table>
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

