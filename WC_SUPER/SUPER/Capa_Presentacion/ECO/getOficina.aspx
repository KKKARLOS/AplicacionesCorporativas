<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getOficina.aspx.cs" Inherits="getOficina" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Selección de oficina </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            actualizarLupas("tblTitulo", "tblDatos");

            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;

            var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[1].innerText;
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
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
-->
</script>
<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
<table class="texto" style="margin-left:10px; width:570px;" cellpadding="5">
    <tr></tr>
    <tr>
        <td>
            <TABLE id="tblTitulo" style="WIDTH: 550px; HEIGHT: 17px" >
                <TR class="TBLINI">
                    <td width="275px;" style="padding-left:5px;">
						<IMG style="CURSOR: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblDatos', 0, 0, '')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 0, 1, '')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Centro&nbsp;
				        <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"	height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                        <IMG style="CURSOR: pointer;DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
                    </TD>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					    <MAP name="img2">
					        <AREA onclick="ot('tblDatos', 1, 0, '')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 1, 1, '')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Denominación&nbsp;
				        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"	height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"></TD>
                </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 566px; height:352px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:550px">
                <%=strTablaHTML %>
                </div>
            </DIV>
            <TABLE style="WIDTH: 550px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
    </tr>
</table>
    <br />
    <center>
    <table width="300px" align="center" style="margin-top:5px;">
	    <tr>
		    <td align="center">
		    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
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
