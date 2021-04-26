<%@ Page Language="c#" CodeFile="getDestFact.aspx.cs" Inherits="getDestFact" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selección de dirección de facturación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
		    try{
                if (!mostrarErrores()) return;
 
			    ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}
		
//		var bMsg = false;
//        function buscarClientes(strCli){
//            try{
//                //alert(strCli);
//                if (strCli == ""){
//                    bMsg = !bMsg;
//                    if (bMsg) alert("Introduzca algún criterio de búsqueda");
//                    return;
//                }
//                var js_args = "cliente@#@";
//                var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
//                js_args += sAccion + "@#@";
//                js_args += strCli + "@#@";
//                js_args += sInterno + "@#@";
//                js_args += sSoloActivos + "@#@";
//                js_args += sTipo
//                //alert(js_args);
//                mostrarProcesando();
//                RealizarCallBack(js_args, "");
//	        }catch(e){
//		        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
//            }
//		}
        /*
        El resultado se envía en el siguiente formato:
        "opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
        */
//        function RespuestaCallBack(strResultado, context){
//            actualizarSession();
//            var aResul = strResultado.split("@#@");
//            if (aResul[1] != "OK"){
//                ocultarProcesando();
//                var reg = /\\n/g;
//                mostrarError(aResul[2].replace(reg, "\n"));
//            }else{
//                switch (aResul[0]){
//                    case "cliente":
//                        $I("divCatalogo").scrollTop = 0;
//                        if (aResul[2] == "EXCEDE"){
//		                    $I("divCatalogo").children[0].innerHTML = "";
//		                    mmoff("War", "La selección realizada excede un límite razonable. Por favor, acote más su consulta.", 500, 2500);
//                        }else{
//		                    $I("divCatalogo").children[0].innerHTML = aResul[2];
//                            $I("txtCliente").value = "";
//                            actualizarLupas("tblTitulo", "tblDatos");
//                        }
//                        break;
//                }
//                ocultarProcesando();
//            }
//        }
        
	    function aceptarClick(indexFila){
		    try{
                if (bProcesando()) return;
                var tblDatos = $I("tblDatos");
                
	            strDatos = "";
	            strDatos = tblDatos.rows[indexFila].id + "@#@" 
	                        + tblDatos.rows[indexFila].cells[0].innerText + "@#@" 
	                        + tblDatos.rows[indexFila].getAttribute("nif") + "@#@" 
	                        + tblDatos.rows[indexFila].cells[1].innerText + "<br>"
	                        + tblDatos.rows[indexFila].cells[2].innerText + " "
	                        + tblDatos.rows[indexFila].cells[3].innerText + "<br>"
	                        + tblDatos.rows[indexFila].cells[4].innerText;

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
		
	-->
    </script>
</head>
<body style="overflow: hidden" leftmargin="15" topmargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	-->
    </script>
		<br />
		<center>
		<table style="width:920px;text-align:left">
			<tr>
				<td>
					<table id="tblTitulo" style="width:900px; height:17px; margin-top:10px;">
					    <colgroup>
					        <col style="width:340px;" />
					        <col style="width:200px;" />
					        <col style="width:60px;" />
					        <col style="width:200px;" />
					        <col style="width:100px;" />
					    </colgroup>
						<tr class="TBLINI">
							<td>&nbsp;Razón social&nbsp;<IMG id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
									height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="display: none; cursor: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
									height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
							<td>Dirección</td>
							<td title="Código postal">C.P.</td>
							<td>Población</td>
							<td>País</td>
						</tr>
					</table>
			    </td>
			</tr>
            <tr>
                <td>
                    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 916px; height: 340px" align="left" name="divCatalogo">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:900px">
						<%=strTablaHTML%>
		                </div>
	                </div>
	                <table style="width:900px; height:17px;"> 
		                <tr class="TBLFIN">
			                <td>&nbsp;</td>
		                </tr>
		                <tr>
			                <td align="center" style="padding-top:15px;">
                                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
			                </td>
		                </tr>
	                </table>
	            </td>
	        </tr>
		</table>
    </center>		
		
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
