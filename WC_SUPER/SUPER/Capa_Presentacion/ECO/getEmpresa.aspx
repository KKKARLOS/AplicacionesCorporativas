<%@ Page Language="c#" CodeFile="getEmpresa.aspx.cs" Inherits="getEmpresa" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de empresa</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">

		function init(){
            try{
                if (!mostrarErrores()) return;
    			ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}

        
        function aceptarClick(indexFila){
            try{
                if (bProcesando()) return;

                var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[0].innerText; ;
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

    </script>
    
    <style type="text/css">      
	    #tblDatos td { padding: 3px; }
	    #tblDatos tr { height: 18px; }
    </style>
</head>
<body style="OVERFLOW: hidden; margin-left:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">

	    var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer   = "<%=Session["strServer"]%>";

	    function ObtenerDatos(){
	        try{
	            mostrarProcesando();        

	            var js_args = "ObtenerDatos@#@" + $I("chkActivas").checked;
	                        
	            RealizarCallBack(js_args, "");
	            return;
            }catch(e){
                mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
            }
	    }

	    function RespuestaCallBack(strResultado, context) {
	        actualizarSession();

	        var aResul = strResultado.split("@#@");

	        var aResul = strResultado.split("@#@");
	        if (aResul[0] != "OK") {
	            ocultarProcesando();
	            var reg = /\\n/g;
	            mostrarError(aResul[2].replace(reg, "\n"));
	        } else {
	            $I("divCatalogo").children[0].innerHTML = aResul[1];
	            ocultarProcesando();
	        }
	    }

    </script>
        <center>

    <span style="position:absolute;top:10px;left:270px;cursor:pointer;">
        <label for="ctl00_CPHC_chkActivos" style="position:relative;top:-2px;font-size:11px">Mostrar únicamente activas</label><asp:CheckBox id="chkActivas" runat="server" Checked="true" style="vertical-align:middle" onClick="ObtenerDatos();"/>
    </span>


		<table style="margin-top:30px;width:412px">
			<tr>
		        <td>
			        <table id="tblTitulo" style="height:17px; width:396px;" >
				        <tr class="TBLINI">
					        <td>&nbsp;Denominación&nbsp;
						        <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
							        height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					            <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
							        height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					        </td>
				        </tr>
			        </table>
			        <div id="divCatalogo" style="OVERFLOW: auto; width: 412px; height: 397px" align="left" runat="server">                            
                        <table id='tblDatos' style='width: 396px;'>
                            <colgroup><col style='width:396px;' /></colgroup>
				        </table>
    					
                    </div>
                    <table style="width: 396px; height: 17px">
                        <tr class="TBLFIN">
                            <td></td>
                        </tr>
                    </table>
                    <br />
                    <center>
                        <table style="margin-top:5px; width:100px;">
                            <tr>
                                <td align="center">
                                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
                                </td>
                            </tr>
                        </table>
                    </center>
		        </td>
		   </tr>
        </table>
        </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
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
