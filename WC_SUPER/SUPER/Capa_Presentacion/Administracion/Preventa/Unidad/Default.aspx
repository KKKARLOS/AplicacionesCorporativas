<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de la unidad de preventa</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?20170921" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
    </script>    
<br /><br />
<center>
<table style="margin-top:20px;width:90%;text-align:left;">
	<tr>
		<td>
            <table width="940px" align="center" cellpadding="5px" >
                <colgroup>
                    <col style="width:100px;" />
                    <col style="width:840px;" />
                </colgroup>
                <tr>
                    <td>
                        <label id="lblCodigo" style="visibility:hidden;">Código</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigo" SkinID="numero" style="width:60px; visibility:hidden" Text="" runat="server" ReadOnly />
                    </td>
                </tr>
                <tr style="vertical-align:middle;">
                    <td>
                        Denominación&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtDenominacion" runat="server" style="width:450px;" MaxLength="50" Text="" onChange="aG();"/>
                    </td>
                </tr>
                <tr style="vertical-align:middle;">
                    <td>
                        Activa&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:CheckBox id="chkActiva" runat="server" onclick="aG();" style="vertical-align:middle;"/>
                    </td>
                </tr>    
            </table>
        </td>
    </tr>
</table>
    <table id="tblBotones" align="center" style="margin-top:30px;" width="50%">
	    <tr>
		    <td align="center">
				<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
				</button>	
		    </td>						
		    <td align="center">
				<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
				</button>	 
		    </td>
	    </tr>
    </table>
</center>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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
