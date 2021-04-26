<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Consumos de GASVI</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<div id="divMonedaImportes" runat="server" style="position:absolute; top:5px; left:270px; width:300px; visibility:hidden;">
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
	    var EsForaneo = <%= (Session["ua"] != null)? "1":"0" %>;
	    var bLectura = <%=sLectura%>;
	    var bLecturaInsMes = <%=sLecturaInsMes%>;
    	var sUltCierreEcoNodo = opener.sUltCierreEcoNodo;
	    var sEstado = "<%=Request.QueryString["sEstadoProy"]%>";
	    var sModeloImputacionGasvi = <%=sModeloImputacionGasvi%>;
    </script>
    <center>
    <table cellpadding="3" style="width: 980px; text-align:left; margin-top:15px;">
        <colgroup>
            <col style="width: 470px;" />
            <col style="width: 40px;" />
            <col style="width: 470px;" />
        </colgroup>
        <tr style="height:32px;">
            <td style="vertical-align:bottom;">
                <img id="imgPM" title="Primermes" src="../../../Images/btnPriRegOff.gif" onclick="cambiarMes('P')" style="cursor: pointer;vertical-align:bottom" border=0 />
                <img id="imgAM" title="Mes anterior" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer;vertical-align:bottom" border=0 />
                <asp:TextBox ID="txtMesBase" style="width:90px; text-align:center; vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
                <img id="imgSM" title="Siguiente mes" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer;vertical-align:bottom" border=0 />
                <img id="imgUM" title="Último mes" src="../../../Images/btnUltRegOff.gif" onclick="cambiarMes('U')" style="cursor: pointer;vertical-align:bottom" border=0 />
            </td>
            <td></td>
            <td>Sistema de importación: <img id="imgSIG" src='../../../Images/imgSeparador.gif' border='0' title="" style="vertical-align:middle; width:32px; height:32px;" /><label style="margin-left:60px;">(doble click sobre la nota para ver su detalle)</label></td>
        </tr>
    </table>
    <table style="width: 980px; text-align:left">
    <colgroup>
        <col style="width:470px;" />
        <col style="width:40px;" />
        <col style="width:470px;" />
    </colgroup>
    <tr>
    <td align="right" style="padding-right:18px;">
        <nobr style="margin-right:170px; font-weight:bold; font-size:12pt; vertical-align:bottom; color:#5894ae;">GASVI</nobr>
        <img id="imgMT1" src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img id="imgDMT1" src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
    </td>
    <td></td>
    <td align="right" style="padding-right:18px;">
        <nobr style="margin-right:170px; font-weight:bold; font-size:12pt; vertical-align:bottom; color:#5894ae;">SUPER</nobr>
        <img id="imgMT2" src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img id="imgDMT2" src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
    </td>
    </tr>
    <tr>
        <td>
            <table style="width: 450px; height: 17px; margin-top:5px;">
            <colgroup>
            <col style='width:195px;' />
            <col style='width:195px;' />
            <col style='width:60px;' />
            </colgroup>
            <tr class='TBLINI'>
                <td style='padding-left:5px;'>Nota / Profesional</td>
                <td>Concepto</td>
                <td style='text-align:right; padding-right:2px;' >Importe</td>
            </tr>
            </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 466px; height:380px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px">
		            <%=strTablaHTML%>
		        </div>
		    </div>
		    <table style="width: 450px; height: 17px;">
			    <tr class="TBLFIN">
				    <td>&nbsp;</td>
			    </tr>
		    </table>
        </td>
        <td style="vertical-align:middle; text-align:center;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);"  caso="3"></asp:Image>
        </td>
        <td>
            <table style='width: 450px; height: 17px; margin-top:5px;'>
            <colgroup>
            <col style='width:10px;' />
            <col style='width:185px;' />
            <col style='width:195px;' />
            <col style='width:60px;' />
            </colgroup>
            <tr class='TBLINI'>
                <td></td>
                <td style='padding-left:5px;'>Nota / Profesional</td>
                <td>Concepto</td>
                <td style='text-align:right; padding-right:2px;'>Importe</td>
            </tr>
            </table>
		    <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 466px; height:380px" runat="server" target="true" onmouseover="setTarget(this);" caso="3">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px">
		            <%=strTablaHTML2%>
		        </div>
		    </div>
		    <table style='width: 450px; height: 17px;'>
			    <tr class="TBLFIN">
				    <td>&nbsp;</td>
			    </tr>
		    </table>
        </td>
    </tr>
    </table>
    <center>
        <table id="tblBotones" align="center" style="margin-top:15px;" width="90%">
            <tr>
	            <td align="center">
		            <button id="btnInsertarMes" type="button" onclick="insertarmes()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25);mostrarCursor(this);">
			            <img src="../../../images/botones/imgInsertarMes.gif" /><span title="Insertar mes">Ins. mes</span>
		            </button>			
	            </td>			
                <td align="center">
	                <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
		                 onmouseover="se(this, 25);mostrarCursor(this);">
		                <img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;Grabar</span>
	                </button>	
                </td>
                <td align="center">
	                <button id="btnGrabarSalir" type="button" onclick="grabarsalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
		                 onmouseover="se(this, 25);mostrarCursor(this);">
		                <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
	                </button>	
                </td>			
 
                <td align="center">
	                <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
		                 onmouseover="se(this, 25);mostrarCursor(this);">
		                <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
	                </button>	 
                </td>
            </tr>
        </table>
    </center>
    
    </center>
    <asp:TextBox ID="hdnReferencia" runat="server" style="visibility:hidden" Text="" />
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
