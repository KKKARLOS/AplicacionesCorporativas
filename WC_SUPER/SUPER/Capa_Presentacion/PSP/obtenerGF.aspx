<%@ Page Language="c#" CodeFile="obtenerGF.aspx.cs" Inherits="SUPER.Capa_Presentacion.obtenerGF" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		    <title> ::: SUPER ::: - Selección de grupo funcional</title>
            <meta http-equiv="X-UA-Compatible" content="IE=8"/>
            <script language="JavaScript" src="../../Javascript/jquery-1.7.1.min.js" type="text/Javascript"></script>
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
			-->
            </script>
            <style type="text/css">      
	            #tblDatos td { padding-left: 3px; }
	            #tblDatos tr { height: 18px; }
            </style>              
    </head>
	<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
		<form id="Form1" runat="server">
		<script type="text/javascript">
		<!--
		    var strErrores = "<%=strErrores%>";
            var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
		    var strServer = "<%=Session["strServer"]%>";
		-->
		</script>
		<br />
	    <center>
            <table style="width:412px;text-align:left;">		                	
                <tr>
                    <td>
                        <table id="tblDatos2" cellspacing="7" style="width:396px;text-align:left;">
                            <colgroup>
	                            <col style="width:60px" />
	                            <col style="width:336px"/>
	                        </colgroup>
                            <tr>
                                <td>
                                    <label id="lblNodo" runat="server" class="enlace" onclick="getNodo();" visible ="false">Nodo</label>
                                    
                                <td align="center">                                     
                                    <asp:TextBox ID="txtDesNodo" style="width:310px;" onChange="getGF()"  Text="" visible ="false" readonly="true" runat="server"/>       
                                    <img id="gomaNodo" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" visible ="false" style="cursor:pointer; vertical-align:middle;" runat="server">
                                </td>
                                </td>    
                            </tr>   
                        </table> 
                        <br />
                        <table id="tblTitulo" style="width: 396px; height: 17px">      
                            <tr class="TBLINI">
                                <td colspan="2" style="padding-left:3px;">Denominación&nbsp;<IMG id="imgLupa" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
				                            height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa',event)"
				                            height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
				                </td>
                            </tr>
                        </table>
                        <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 412px; height:334px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:396px;">
                                <%=strTablaHTML %>
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
            <table width="300px" align="center">
	            <tr>
		            <td align="center">
		                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
		            </td>
	            </tr>
            </table>
        </center>    
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
        <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
    </form>
	</body>
</html>

<script type="text/javascript">

    function getNodo(){
        try{
            if ($I("lblNodo").className == "texto") return;
            mostrarProcesando();
            modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 460))
               .then(function(ret) {
                   if (ret != null){
                       var aDatos = ret.split("@#@");
                       $I("hdnIdNodo").value = aDatos[0];
                       $I("txtDesNodo").value = aDatos[1];
                       obtenerGFPorCR(aDatos[0]);
                   }else ocultarProcesando();
               }); 
            window.focus();
            ocultarProcesando();
	    }catch(e){
		    mostrarErrorAplicacion("Error al obtener el nodo ", e.message);
        }
    }

    function borrarNodo(){
        try{

            if($I("txtDesNodo").value != ""){
                $I("hdnIdNodo").value = "";
                $I("txtDesNodo").value = "";
                obtenerGFPorCR("");
            }
            
        }catch(e){
            mostrarErrorAplicacion("Error al borrar el nodo", e.message);
        }
    }

    function obtenerGFPorCR(sOpcion){
       
        try{    
            
            mostrarProcesando();

            $.ajax({
                url: "obtenerGF.aspx/listaGF",
                data: JSON.stringify({ sCR: sOpcion }),
                async: true,
                type: "POST", // data has to be POSTed
                contentType: "application/json; charset=utf-8", // posting JSON content    
                dataType: "json",  // type of data is JSON (must be upper case!)
                timeout: 10000,    // AJAX timeout. Se comentariza para debugar
                success: function (result) {
                    //$("#divresultado").html(result.d.nombre + " " + result.d.apellido + " " + result.d.dni);
                    //$("#divresultado").html(result.d);
                    var aResul = result.d.split("@#@");
                    if (aResul[0] != "OK") {
                        ocultarProcesando();
                        mmoff("Err", aResul[1], 540);
                    }

                    $I("divCatalogo").children[0].innerHTML = aResul[1];
                    $I("divCatalogo").scrollTop = 0;
                    ocultarProcesando();
                },
                error: function (ex, status) {
                    error$ajax("Ocurrió un error en la aplicación", ex, status)
                }
            });

        }catch(e){
            mostrarErrorAplicacion("Error al obtener la relación de grupos funcionales", e.message);
        }
    }

    function error$ajax(msg, ex, status) {
        ocultarProcesando();
        if (status == "timeout") {
            mmoff("Err", "Se ha sobrepasado el tiempo límite de espera para procesar la petición en servidor.\n\nVuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU.\n\nDisculpa las molestias.",400);
            return;
        }
        bCambios = false;
        var desc = "";
        var reg = /\\n/g;
        if (ex.responseText != "undefined") {
            desc = Utilidades.unescape($.parseJSON(ex.responseText).Message);
            desc = desc.replace(reg, "\n");
        }
        mostrarError(msg + "\n\n" + desc);
    }

</script>