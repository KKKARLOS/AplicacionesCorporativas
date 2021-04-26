<%@ Page Language="c#" CodeFile="obtenerFuncion.aspx.cs" Inherits="SUPER.Capa_Presentacion.obtenerFuncion" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		    <title> ::: SUPER ::: - Seleccione una función</title>
            <meta http-equiv="X-UA-Compatible" content="IE=8"/>
			<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
			<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
         	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
			<script type="text/javascript">
			<!--
				function init(){
		            try{
					    if (strErrores != ""){
						    mostrarError(strErrores);
					    }
					    actualizarLupas("tblTitulo", "tblDatos");
					    ocultarProcesando();
                    }catch(e){
	                    mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
                    }
				}

           	
	            function aceptarClick(indexFila){
		            try{
                        if (bProcesando()) return;
                        
	                    strOpciones = "";
	                    var tblDatos = $I("tblDatos");
	                    strOpciones = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText + "@#@";
		                strOpciones = strOpciones.substring(0,strOpciones.length-3);

		                var returnValue = strOpciones;
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
                function getNodo(){
                    try{
                        var js_args = "getNodo@#@"+ $I("cboCR").value;
                        mostrarProcesando();
                        RealizarCallBack(js_args, "");
                        return;
	                }catch(e){
		                mostrarErrorAplicacion("Error al ir a obtener funciones del nodo", e.message);
                    }
                }
                function RespuestaCallBack(strResultado, context){
                    actualizarSession();
                    var aResul = strResultado.split("@#@");
                    if (aResul[1] != "OK"){
                        ocultarProcesando();
                        var reg = /\\n/g;
		                mostrarError(aResul[2].replace(reg, "\n"));
                    }else{
                        switch (aResul[0]){
                            case "getNodo":
                                $I("divCatalogo").children[0].innerHTML = aResul[2];
                                break;
                        }
                        ocultarProcesando();
                    }
                }
			-->
            </script>
           <style type="text/css">      
	            #tblDatos td { padding-left: 3px; }
	            #tblDatos tr { height: 18px; }
            </style>                
    </head>
	<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
		<form id="Form1" method="post" runat="server">
		<script type="text/javascript">
		<!--
		    var strErrores = "<%=strErrores%>";
            var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
		    var strServer = "<%=Session["strServer"]%>";
		-->
		</script>
			<br />
			<center>
			<table style="width:412px;text-align:left">			
					<tr>
                        <td>
                            <label id="lblNodo" runat="server" class="texto">Nodo </label>
                            <asp:DropDownList id="cboCR" runat="server" Width="350px" onChange="getNodo();" AppendDataBoundItems=true >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
						<td>
							<table id="tblTitulo" style="width: 396px; height: 17px">
								<tr class="TBLINI">
									<td >&nbsp;Función&nbsp;
										<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
											height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
									    <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
											height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
									</td>
								</tr>
							</table>
                            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 412px; height:337px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:396px;">
                                    <%=strTablaHtml %>
                                </div>
                            </div>
                            <table style="width: 396px; height: 17px">
                                <tr class="TBLFIN">
                                    <td></td>
                                </tr>
                            </table>
            			</td>
			        </tr>
			</table>
			<br />
            <table width="300px" align="center" style="margin-top:5px;">
	            <tr>
		            <td align="center">
		                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
		            </td>
	            </tr>
            </table>
        </center>  
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
