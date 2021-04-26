<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Borrado de meses de escenario</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var sMeses = "<%=sMeses%>";
	-->
    </script>
    <style>
    #tblDatos td { padding: 0px 0px 0px 5px;}
    </style>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
	
    <table style="width:250px; margin-left:10px;">
    <tr>
        <td>
            <table style='WIDTH: 210px; table-layout:fixed; BORDER-COLLAPSE: collapse; HEIGHT: 17px; margin-top:5px;' cellSpacing='0' cellspacing='0' border='0'>
            <colgroup>
                <col style='width:40px;' />
                <col style='width:160px;' />
            </colgroup>
            <tr class='TBLINI'>
                <td style="text-align:center;">Borrar</td>
                <td style="text-align:center;">Mes</td>
            </tr>
            </table>
		    <div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 226px; height:380px" runat="server">
            <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:210px; height:auto;">
		    <%//=strTablaHTML%>
		        <table id="tblDatos" style="width:210px;">
                    <colgroup>
                       <col style="width:40px;" />
                       <col style="width:160px;" />
                    </colgroup>
                </table>
		    </div>
		    </div>
		    <table id="Table4" style="WIDTH: 210px; table-layout:fixed; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" border="0">
			    <tr class="TBLFIN">
				    <td>&nbsp;</td>
			    </tr>
		    </table>
        </td>
    </tr>
    </table>
    <table width="230px" align="center" style="margin-top:10px;">
        <tr>
        	<td align="center">
                <button id="btnProcesar" type="button" onclick="Aceptar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
        	<td align="center">
                <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgCancelar.gif" /><span>Cancelar</span>
                </button>
			</td>
        </tr>
    </table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
   <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <div class="clsDragWindow" id="DW" noWrap></div>
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
