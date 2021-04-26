<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Detalle de datos económicos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<div id="divMonedaImportes" runat="server" style="position:absolute; top:5px; left:700px; width:300px; visibility:hidden;">
    <table style="width:300px; margin-top:15px;" cellpadding="3">
    <tr>
        <td>Moneda proyecto: <label id="lblMonedaProyecto" runat="server"></label></td>
    </tr>
    <tr>
        <td><label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server"></label></td>
    </tr>
    </table>
	<script type="text/javascript">

	    var sMonedaProyecto = "<%=sMonedaProyecto%>";
	    var sMonedaImportes = "<%=sMonedaImportes%>";

    </script>
</div>
    <form id="form1" runat="server">
	<script type="text/javascript">

        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var bLectura = <%=sLectura%>;

    </script>

    <center>
        <table style="width: 980px; text-align:left; margin-top:15px" cellpadding="3" >
            <colgroup>
                <col style="width:120px" />
                <col style="width:300px" />
                <col style="width:560px"/></colgroup>
            <tr>
                <td colspan="3">
                    <img id="imgPM" title="Primermes" src="../../../Images/btnPriRegOff.gif" onclick="cambiarMes('P')" style="cursor: pointer;vertical-align:bottom"  border="0" />
                    <img id="imgAM" title="Mes anterior" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer;vertical-align:bottom"  border="0" />
                    <asp:TextBox ID="txtMesBase" style="width:90px; text-align:center; vertical-align:super" readonly=true runat="server" Text=""></asp:TextBox>
                    <img id="imgSM" title="Siguiente mes" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer;vertical-align:bottom"  border="0" />
                    <img id="imgUM" title="Último mes" src="../../../Images/btnUltRegOff.gif" onclick="cambiarMes('U')" style="cursor: pointer;vertical-align:bottom" border="0" />
                </td>
            </tr>
        </table>
    <table style="width: 980px; text-align:left; margin-top:15px">
    <tr>
        <td>
            <table style="width: 960px; height: 17px; margin-top:15px;">
            <colgroup>
                <col style="width:425px;"/>
                <col style="width:430px;"/>
                <col style="width:100px;"/>                
            </colgroup>                  
            <tr class='TBLINI'>
                    <td style='padding-left:5px;'><img style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'><MAP name='img1'><AREA onclick="ot('tblDatos', 1, 0, '')" shape='RECT' coords='0,0,6,5'><AREA onclick="ot('tblDatos', 1, 1, '')" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Clase económica</td>
                    <td><img style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img2' border='0'><MAP name='img2'><AREA onclick="ot('tblDatos', 2, 0, '')" shape='RECT' coords='0,0,6,5'><AREA onclick="ot('tblDatos', 2, 1, '')" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Motivo</td>
                    <td style='text-align:right; padding-right:2px;'><img style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img3' border='0'><MAP name='img3'><AREA onclick="ot('tblDatos', 3, 0, 'num')" shape='RECT' coords='0,0,6,5'><AREA onclick="ot('tblDatos', 3, 1, 'num')" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Importe</td>
                </tr>
            </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 976px; height:425px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:960px">
		            <%=strTablaHTML%>
		        </div>
		    </div>
		    <table style="width: 960px; height: 17px;">
		    <colgroup>
                <col style="width:400px;"/>
                <col style="width:560px;"/>
            </colgroup>			    
			    <tr class="TBLFIN">
                    <td>&nbsp;Total</td>
                    <td align="right" style="padding-right:2px;"><label id="lblTotalImporte" class="texto">0,00</label></td>
			    </tr>
		    </table>
		    <center>
                <table width="100px" align="center" style="margin-top:5px;" border="0" cellpadding=0 cellspacing=0>
		            <tr>
                        <td align="center">
	                        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
		                         onmouseover="se(this, 25);mostrarCursor(this);">
		                        <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
	                        </button>	 
                        </td>
		            </tr>
            </table>
            </center>
        </td>
    </tr>
    </table>
    </center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />

   <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
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
