<%@ Page Language="c#" CodeFile="obtenerProfesional.aspx.cs" Inherits="GESTAR.Capa_Presentacion.obtenerProfesional" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<html>
<head runat="server">
	<title>Seleccione un profesional</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<link href="../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/>
	<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
			ocultarProcesando();			
			    try{
			        $I("txtApellido1").focus();
			    }catch(e){}
		}

        /*
        El resultado se envía en el siguiente formato:
        "opcion@@OK@@valor si hiciera falta, html,..." ó "ERROR@@Descripción del error"
        */
        function RespuestaCallBack(strResultado, context){
            actualizarSession();
            var aResul = strResultado.split("@@");
            if (aResul[1] != "OK"){
                ocultarProcesando();
                var reg = /\\n/g;
                mostrarError(aResul[2].replace(reg, "\n"));
            }else{
                switch (aResul[0]){
                    case "profesionales":
                        //$I("divCatalogo").innerHTML = aResul[2];
                        $I("divCatalogo").children[0].innerHTML = aResul[2];
                        break;
                }
                ocultarProcesando();
            }
        }

        function mostrarProfesionales(){
            try{
                if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
                    mmoff("War", "Debe introducir algún criterio de búsqueda", 300);
                    $I("txtApellido1").focus();
                    return;
                }
                var js_args = "profesionales@@";
                js_args += escape($I("txtApellido1").value) +"@@"; 
                js_args += escape($I("txtApellido2").value) +"@@"; 
                js_args += escape($I("txtNombre").value); 
                
                //alert(js_args);
                mostrarProcesando();
                RealizarCallBack(js_args, ""); 
                return;
                
	        }catch(e){
	            var strTitulo = "Error al obtener la relación de técnicos";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
        }
	-->
    </script>
</head>
<body style="overflow: hidden" leftMargin="15" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />	
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
	    var intSession  = <%=Session.Timeout%>; 
	    var strServer   = "<%=Session["strServer"]%>";
	-->
    </script>
		<table width="412px" align="center" style="margin-top:10px;">
			<TBODY>
			    <tr>
			        <td>
                        <table id="tblApellidos" style="width: 350px;margin-bottom:5px;">
                            <tr>
                            <td>&nbsp;&nbsp;Apellido1</td>
                            <td>&nbsp;&nbsp;Apellido2</td>
                            <td>&nbsp;&nbsp;Nombre</td>
                            </tr>
                            <tr>
                            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
			        </td>
			    </tr>
				<tr>
					<td>
						<table id="tblTitulo" style="height:17px;width:396px">
							<tr class="tituloColumnaTabla">
							    <td width="56px" align="center">Nº</td>
								<td width="300px"><img style="cursor: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
										height="11" src="../Images/imgLupa.gif" width="20"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										height="11" src="../Images/imgLupaMas.gif" width="20">&nbsp;Profesional</td>
								<td width="40px"><img onmouseover="javascript:bMover=true;moverTablaUp()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
										height="8" src="../Images/imgFleUp.gif" width="11"></td>
							</tr>
						</table>
						<div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width:412px; height: 267px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:396px">					                
                                <table id='tblDatos' style='width: 396px;'>
                                    <colgroup><col style='padding-left:5px;' /></colgroup>
							    </table>
					        </div>
		                </div>
		                <table id="tblResultado" style="height:17px;width:396px">
			                <tr class="textoResultadoTabla">
				                <td colSpan="2"><img height="1" src="../Images/imgSeparador.gif" width="356px" border="0">
					                <img onmouseover="javascript:bMover=true;moverTablaDown()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
						                height="8" src="../Images/imgFleDown.gif" width="11"></td>
			                </tr>
			                <tr height="5">
				                <td colSpan="2"><img onmouseover="javascript:bMover=true;moverTablaDown()" onmouseout="javascript:bMover=false;"
						                height="7" src="../Images/imgSeparador.gif"></td>
			                </tr>
			                <tr>
				                <td>
					                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
						                <img src="../images/botones/imgAceptar.gif" /><span title="Aceptamos el elemento seleccionado">Aceptar</span>
					                </button>					
				                </td>
				                <td>
					                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
						                <img src="../images/botones/imgCancelar.gif" /><span title="Salir sin selección de ningun elemento">Cancelar</span>
					                </button>				
				                </td>
			                </tr>
		                </table>
		</td></tr></TBODY></table>
	</form>
	<script type="text/javascript">
        <!--
        function aceptar(){
	        var strOpciones = "";
	        var sw = 0;
        	
	        var aFila = FilasDe("tblDatos");
	        for (var i=0; i<aFila.length;i++){
	            if (aFila[i].className == "FS"){
	                strOpciones = aFila[i].id + "@@" + aFila[i].getAttribute("codred") + "@@" + aFila[i].cells[1].innerText + "@@";
		            sw = 1;
		            break;
		        }
	        }
	        if (sw == 0){
	            mmoff("Inf", "Debe seleccionar algún elemento del catálogo", 330);
	            return;
	        }
        	
	        strOpciones = strOpciones.substring(0,strOpciones.length-2);
	        var returnValue = strOpciones;
	        modalDialog.Close(window, returnValue);		 	        
        }

        function aceptarClick(indexFila){
            strOpciones = "";
            var tblDatos = $I("tblDatos");

            strOpciones = tblDatos.rows[indexFila].id + "@@" + tblDatos.rows[indexFila].getAttribute("codred") + "@@" + tblDatos.rows[indexFila].cells[1].innerText + "@@";
	        strOpciones = strOpciones.substring(0,strOpciones.length-2);

	        var returnValue = strOpciones;
	        modalDialog.Close(window, returnValue);		 
        }

        function cerrarVentana(){

            var returnValue = null;
            modalDialog.Close(window, returnValue);		 
        }
        -->
    </script>
</body>
</html>
