<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getNaturaleza.aspx.cs" Inherits="getNaturaleza" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de naturaleza de producción</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            if ($I("tblDatos1").rows.length == 1) $I("tblDatos1").rows[0].click();
            
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;

            var returnValue = $I("tblDatos3").rows[indexFila].id + "@#@" +
	                             $I("tblDatos3").rows[indexFila].cells[0].innerText + "@#@" +
	                             $I("tblDatos3").rows[indexFila].getAttribute("idPlant") + "@#@" +
	                             $I("tblDatos3").rows[indexFila].getAttribute("desPlant");
            modalDialog.Close(window, returnValue);	
        }catch(e){
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
    function RespuestaCallBack(strResultado, context){
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            ocultarProcesando();
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        }else{
            switch (aResul[0]){
                case "getSubgrupoNat":
                    $I("divCatalogo2").children[0].innerHTML = aResul[2];
                    setTimeout("if ($I('tblDatos2').rows.length == 1) $I('tblDatos2').rows[0].click();", 20);
                    break;
                case "getNaturaleza":
                    $I("divCatalogo3").children[0].innerHTML = aResul[2];
                    break;

                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                    break;
            }
            ocultarProcesando();
        }
    }

    function getSubgrupoNat(nIDGrupoNat){
        try{
            var js_args = "getSubgrupoNat@#@";
            js_args += nIDGrupoNat; 
            
            mostrarProcesando();
            $I("divCatalogo2").children[0].innerHTML = "";
            $I("divCatalogo3").children[0].innerHTML = "";

            RealizarCallBack(js_args, ""); 
        }catch(e){
	        mostrarErrorAplicacion("Error al ir a obtener los subgrupos de naturaleza.", e.message);
        }
    }

    function getNaturalezaProd(nIDSubGrupoNat){
        try{
            var js_args = "getNaturaleza@#@";
            js_args += nIDSubGrupoNat; 
            
            mostrarProcesando();
            $I("divCatalogo3").children[0].innerHTML = "";

            RealizarCallBack(js_args, ""); 
        }catch(e){
	        mostrarErrorAplicacion("Error al ir a obtener las naturalezas de producción.", e.message);
        }
    }
    
    
	-->
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="15px" />
    <table border="0" class="texto" width="520px" style="margin-left:15px;">
        <tr>
            <td>
                <TABLE id="tblTitulo" style="WIDTH: 500px; HEIGHT: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:5px">Grupo</td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo1" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 516px; height:120px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:500px;">
                    <%=strTablaHTML%>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 500px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <TABLE id="TABLE1" style="WIDTH: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:5px">Subgrupo</td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 516px; height:120px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:500px;">
                    </div>
                </DIV>
                <TABLE style="WIDTH: 500px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <TABLE id="TABLE2" style="WIDTH: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:5px">Naturaleza</td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo3" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 516px; height:120px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:500px;">
                    </div>
                </DIV>
                <TABLE style="WIDTH: 500px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    <center>
    <table style="margin-top:5px; width:100px;">
		<tr>
			<td>
                <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
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
