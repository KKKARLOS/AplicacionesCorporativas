<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Borrado de meses</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/javascript"></script>
	<script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" leftmargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
    </script>
    <center>
    <table style="width: 620px; text-align:left; margin-top:25px">
    <tr>
        <td>
            <table style='width: 600px; height: 17px;'>
            <colgroup>
                <col style='width:50px;' />
                <col style='width:150px;' />
                <col style='width:100px;' />
                <col style='width:100px;' />
                <col style='width:100px;' />
                <col style='width:100px;' />
            </colgroup>
            <tr class='TBLINI'>
                <td style='padding-left:5px;'>
                    <img src="../../../images/botones/imgmarcar.gif" onclick="mTabla()" title="Marca todas los meses para ser procesadas" style="cursor:pointer;" />
                    <img src="../../../images/botones/imgdesmarcar.gif" onclick="dTabla()" title="Desmarca todas los meses" style="cursor:pointer;margin-right:5px" />                       
                </td>
                <td style='padding-left:5px;'>Mes</td>
                <td style='text-align:right;'>Consumos</td>
                <td style='text-align:right;'>Producción</td>
                <td style='text-align:right;'>Ingresos</td>
                <td style='text-align:right;padding-right:2px;'>Cobros</td>
            </tr>
            </table>
		    <div id="divCatalogo" style="overflow:auto; overlow-x: hidden; width:616px; height:380px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:600px">
		        <%=strTablaHTML%>
		        </div>
		    </div>
		    <table id="Table4" style="width: 600px; height: 17px" >
			    <tr class="TBLFIN">
				    <td>&nbsp;</td>
			    </tr>
		    </table>
        </td>
    </tr>
    </table>
    <table width="290px" style="margin-top:25px;">
		<tr>
			<td>
                <button id="btnProcesar" type="button" onclick="Procesar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/botones/imgProcesar.gif" /><span>Procesar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/botones/imgSalir.gif" /><span>Salir</span>
                </button>
			</td>
		</tr>
    </table>
    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <div class="clsDragWindow" id="DW" noWrap></div>
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
