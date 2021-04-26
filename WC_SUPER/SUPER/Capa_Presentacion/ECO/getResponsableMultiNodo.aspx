<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getResponsableMultiNodo.aspx.cs" Inherits="getResponsableMultiNodo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de responsable </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            if (FilasDe("tblDatos") != null){
                actualizarLupas("tblTitulo", "tblDatos");
                if ($I("tblDatos").rows.length == 0 && idNodo != ""){
                    mmoff("War", "No existen profesionales identificados como CRP\nen los CR's seleccionados" + "\".\n\nPuedes seleccionar al CRP por apellidos y/o nombre.", 400, 4000, 100);
                }
                for (var i = 0; i < $I("tblDatos").rows.length; i++) {
                    if ($I("tblDatos").rows[i].getAttribute("respon") == '0') setOp($I("tblDatos").rows[i].cells[0].children[0], 30);
                }                    
            }
        
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;

            var strDatos = $I("tblDatos").rows[indexFila].id + "@#@" +
                           $I("tblDatos").rows[indexFila].cells[1].innerText + "@#@" + 
                           $I("tblDatos").rows[indexFila].getAttribute("idficepi");

            var returnValue = strDatos;
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
                case "responsables":
                    $I("divCatalogo").scrollTop = 0;
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    for (var i = 0; i < $I("tblDatos").rows.length; i++) {
                        if ($I("tblDatos").rows[i].getAttribute("respon") == '0') setOp($I("tblDatos").rows[i].cells[0].children[0], 30);
                    }                     
                    actualizarLupas("tblTitulo", "tblDatos");
                    break;

                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                    break;
            }
            ocultarProcesando();
        }
    }

    function mostrarProfesionales(){
        try{
            if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
                mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
                $I("txtApellido1").focus();
                return;
            }
            var js_args = "responsables@#@";
            js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
            js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
            js_args += Utilidades.escape($I("txtNombre").value) +"@#@"; 
            js_args += ($I("chkBajas").checked) ? "1@#@":"0@#@";
            js_args += tiporesp;
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
            return;
            
        }catch(e){
	        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
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
	    var tiporesp = "<%=tiporesp%>";
	    var idNodo = "<%=idNodo%>";
	-->
    </script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table class="texto" style="margin-left:10px; width:520px;" cellpadding="5">
	    <tr>
	        <td width="410px">
                <table id="tblApellidos" class="texto" style="WIDTH: 350px;margin-bottom:5px;" >
                    <tr>
                    <td>&nbsp;Apellido1</td>
                    <td>&nbsp;Apellido2</td>
                    <td>&nbsp;Nombre</td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    </tr>
                </table>
	        </td>
	        <td width="110px" style="padding-bottom:10px; vertical-align:bottom;">
	        <input type="checkbox" id="chkBajas" class="check" runat="server" />&nbsp;Mostrar bajas
	        </td>
	    </tr>
        <tr>
            <td colspan="2">
                <TABLE id="tblTitulo" style="WIDTH: 500px; HEIGHT: 17px">
                    <colgroup>
                        <col style="width:20px;" />
                        <col style="width:480px;" />
                    </colgroup>
                    <TR class="TBLINI">
                        <td title="Es responsable">&nbsp;</td>
                        <td style="padding-left:3px;">
                            <IMG style="CURSOR: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <MAP name="img2">
						        <AREA onclick="ot('tblDatos', 2, 0, '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 2, 1, '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Profesional&nbsp;
					        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							        height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2', event)"
							        height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 516px; height:340px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:500px;">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 500px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
        <tr>
            <td style="padding-top:3px;">
                <img class="ICO" src="../../images/imgResponsable<%=sPrefijoCRP %>.gif" title="<%=sTTipResp %>" /><%=sResp %>&nbsp;&nbsp;&nbsp;
                <img id="NoRespon" class="ICO" src="../../images/imgResponsable<%=sPrefijoCRP %>.gif" title="<%=sTTipNoResp %>" /><%=sNoResp %>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <script type="text/javascript">
	<!--
        setOp($I("NoRespon"), 30);
	-->
    </script>    
    <center>  
        <table width="100px" style="margin-top:5px;">
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
