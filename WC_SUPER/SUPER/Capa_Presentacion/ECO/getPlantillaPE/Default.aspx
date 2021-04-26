<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Selección de plantilla</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="Functions/Plantilla.js" type="text/Javascript"></script>
   	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()" style="margin-top:10px; margin-left:10px;">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">

    var strServer = "<%=Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.         
    var gsTipo = "<%=gsTipo%>";
    var gsDesglose = "<%=gsDesglose%>";

</script>    
<center>
<table style="width:920px; text-align:left" cellpadding="5px">
    <colgroup><col style="width:440px;"/><col style="width:20px;"/><col style="width:460px;"/></colgroup>
    <tr>
        <td>
		    <table id="tblTitulo" style="height: 17px; width:420px">
			    <tr class="TBLINI">
				    <td style="padding-left:30px;">Denominación</td>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width: 436px; height:460px">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width: 420px">
	    	    <%=strTablaHTMLPlantilla%>
                </div>
            </div>
		    <table style="height: 17px; width:420px">
			    <tr class="TBLFIN">
				    <td></td>
			    </tr>
		    </table>
        </td>
        <td>&nbsp;</td>
        <td>
	        <table style="height: 17px; width:420px">
		        <tr class="TBLINI">
			        <td>&nbsp;Estructura</td>
		        </tr>
	        </table>
	        <div id="divTareas" style="overflow:auto; overflow-x:hidden; width: 436px; height:360px">
                <div style='background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:420px'>
	            <%=strTablaHTMLTarea%>
                </div>
            </div>
	        <table style="height: 17px; width:420px">
		        <tr class="TBLFIN">
			        <td></td>
		        </tr>
	        </table>
	        <table style="height: 17px; width:420px">
		        <tr class="TBLINI">
			        <td>&nbsp;Hitos de cumplimiento discontinuo</td>
		        </tr>
	        </table>
	        <div id="divHitos" style="overflow:auto; overflow-x:hidden; width: 436px; height:70px">
                <div style='background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:420px'>
	            <%=strTablaHTMLHitos%>
                </div>
            </div>
	        <table style="height: 17px; width:420px">
		        <tr class="TBLFIN">
			        <td></td>
		        </tr>
	        </table>
        </td>
    </tr>
    </table>
<table style="width:920px; margin-left:10px; text-align:left;" class="texto">
    <tr> 
        <td>
            <img border="0" src="../../../Images/imgIconoEmpresarial.gif" />&nbsp;Empresarial&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../Images/imgIconoDepartamental.gif" />&nbsp;Departamental&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../Images/imgIconoPersonal.gif" />&nbsp;Personal
        </td>
    </tr>
</table>
<table style="width:100px;" class="texto">
    <tr>
        <td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
            </button>				    
        </td>
    </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
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

