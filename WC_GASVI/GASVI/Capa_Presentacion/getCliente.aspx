<%@ Page Language="c#" CodeFile="getCliente.aspx.cs" Inherits="getCliente" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de cliente</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	    function init(){
	        try{
	            if (!mostrarErrores()) return;
	            $I("txtCliente").focus();
	            ocultarProcesando();
	        }catch(e){
	            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	        }
	    }
		
	    var bMsg = false;
	    function buscarClientes(strCli){
	        try{
	            if (strCli == ""){
	                bMsg = !bMsg;
	                if (bMsg) mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
	                return;
	            }
	            var js_args = "cliente@#@";
	            var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
	            js_args += sAccion + "@#@";
	            js_args += Utilidades.escape(strCli);

	            mostrarProcesando();
	            RealizarCallBack(js_args, "");
	        }catch(e){
	            mostrarErrorAplicacion("Error al obtener los clientes", e.message);
	        }
	    }
	    function RespuestaCallBack(strResultado, context) {
	        strResultado = Utilidades.unescape(strResultado);
	        actualizarSession();
	        var aResul = strResultado.split("@#@");
	        if (aResul[1] != "OK"){
	            ocultarProcesando();
	            var reg = /\\n/g;
	            alert(aResul[2].replace(reg,"\n"));
	        }else{
	            switch (aResul[0]){
	                case "cliente":
	                    $I("divCatalogo").scrollTop = 0;
	                    if (aResul[2] == "EXCEDE"){
	                        $I("divCatalogo").children[0].innerHTML = "";
	                        mmoff("War", "La selección realizada excede un límite razonable. Por favor, acote más su consulta.", 500, 2500);
	                    }else{
	                        $I("divCatalogo").children[0].innerHTML = aResul[2];
	                        $I("txtCliente").value = "";
	                        actualizarLupas("tblTitulo", "tblDatos");
	                    }
	                    break;
	            }
	            ocultarProcesando();
	        }
	    }
        
	    function aceptarClick(oFila){
	        try{
	            if (bProcesando()) return;

	            //var strDatos = $I("tblDatos").rows[indexFila].getAttribute("id") + "@#@" + $I("tblDatos").rows[indexFila].innerText; 
	            var strDatos = oFila.getAttribute("id") + "@#@" + oFila.innerText; 

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
    </script>
</head>
<body style="overflow: hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
<fieldset style="width:540px; height:40px;">
    <legend>Búsqueda por denominación</legend>
    <table class="texto" style="width:540px;">
	    <tr>
	        <td style="width:390px;">Cadena de búsqueda
                <asp:TextBox ID="txtCliente" runat="server" style="width:260px;" onkeypress="javascript:if(event.keyCode==13){bMsg = false;buscarClientes(this.value);event.keyCode=0;return false;}" />
            </td>
            <td style="width:150px;">
                <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" style="width:149px;" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda" onclick="buscarClientes($I('txtCliente').value);">
                    <asp:ListItem Value="I"><img src='../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$I('rdbTipo_0').click();"></asp:ListItem>
                    <asp:ListItem Selected="True" Value="C"><img src='../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$I('rdbTipo_1').click();"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</fieldset>
<table style="width:566px;" >
		<tr>
			<td colspan="2">
				<table id="tblTitulo" style="margin-top:10px; width:550px; height:17px; text-align:left;">
					<tr class="TBLINI">
						<td>&nbsp;Denominación&nbsp;
						    <img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2" /> 
							<img style="display: none; cursor: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)"
								height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1" />
					    </td>
					</tr>
				</table>
		    </td>
		</tr>
        <tr>
            <td colspan="2">
                <div id="divCatalogo" style="overflow:auto; width:566px; height:300px;">
                    <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT16.gif'); width:550px;">
					    <table id="tblDatos" style="width:550px; text-align:left;">
	                    </table>
	                </div>
                </div>
            </td>
        </tr>
        <tr>
        <td colspan="2">
            <table id="tblResultado" style="width:550px;height:17px; text-align:left;">
                <tr class="TBLFIN">
	                <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="padding-top:3px;">
                        <img class="ICO" src="../../images/imgM.gif" />Matriz&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../images/imgF.gif" />Filial
                    </td>
                </tr>
            </table>		        
        </td>
        </tr>
</table>
<center>
<table style="margin-top:5px; width:100px;">
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
