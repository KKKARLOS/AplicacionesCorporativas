<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getMonedaImportes.aspx.cs" Inherits="getMonedaImportes" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de moneda</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	    var nIndiceFila = -1;
	    
        function init(){
            try{
                if (!mostrarErrores()) return;

                ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }

        function setMonedaImportes(indexFila) {
            try {
                nIndiceFila = indexFila;
                
                var js_args = "setMonedaImportes@#@";
                js_args += $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[0].innerText + "@#@" + sTipoMoneda;
                
                RealizarCallBack(js_args, "");
            } catch (e) {
                mostrarErrorAplicacion("Error seleccionar la fila", e.message);
            }
        }


        function aceptarClick(indexFila) {
            try {
                if (bProcesando()) return;

                var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[0].innerText;
                modalDialog.Close(window, returnValue);	
            } catch (e) {
                mostrarErrorAplicacion("Error seleccionar la fila", e.message);
            }
        }
    	
        function cerrarVentana(){
	        try{
                if (bProcesando()) return;

                var returnValue = null;
                modalDialog.Close(window, returnValue);	
            }catch(e){
                mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
            }
        }

    /*
    El resultado se envía en el siguiente formato:
    "opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
    */
    function RespuestaCallBack(strResultado, context) {
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK") {
            ocultarProcesando();
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        } else {
            switch (aResul[0]) {
                case "setMonedaImportes":
                    aceptarClick(nIndiceFila);
                    break;
            }
            ocultarProcesando();
        }
    }
	-->
    </script>
    <style>
    #tblDatos tr {
        height: 16px;
        cursor:url('../../Images/imgManoAzul2.cur'),pointer;
    }
    </style>
</head>
<body style="overflow: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>;
    var strServer = "<%=Session["strServer"]%>";
    var sTipoMoneda = "<%=sTipoMoneda%>";
-->
</script>
<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
<table class="texto" style="margin-left:10px; width:320px;" cellpadding="5" >
    <tr></tr>
    <tr>
        <td>
            <table id="tblTitulo" style="width:300px; height:17px" >
                <tr class="TBLINI">
                    <td>&nbsp;Denominación&nbsp;</td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 316px; height:200px">
                <div style="background-image: url('../../Images/imgFT16.gif'); background-repeat:repeat; width:300px; height:auto;">
                <%=strTablaHTML %>
                </div>
            </div>
            <table style="width: 300px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<center>
<table style="margin-top:5px; width:100px;">
	<tr>
		<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../images/imgCancelar.gif" /><span>Cancelar</span>
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
