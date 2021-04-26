<%@ Page Language="c#" CodeFile="obtenerFotos.aspx.cs" Inherits="SUPER.Capa_Presentacion.obtenerFotos" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
	<title> ::: SUPER ::: - Selección de instantánea</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">

	    function init() {
	        if (strErrores != "") {
	            mostrarError(strErrores);
	        }
	        ocultarProcesando();
	    }
	    function eliminar() {
	        try {
	            if (iFila == -1) {
	                mmoff("Inf", "Debes seleccionar alguna instantánea",240);
	                return;
	            }
	            if ($I("tblOpciones").rows[iFila].getAttribute("usuario") != num_empleado) {
	                mmoff("Inf", "Solo se pueden eliminar las instantáneas propias",400);
	                return;
	            }
	            jqConfirm("", "Esta acción elimina la instantánea de la base de datos.<br><br>¿Deseas continuar?", "", "", "war", 350).then(function (answer) {
	                if (answer) LLamarEliminar();
	            });
	        } catch (e) {
	            mostrarErrorAplicacion("Error al eliminar la foto-1", e.message);
	        }
	    }
	    function LLamarEliminar() {
	        try {
	            var js_args = "eliminar@#@";
	            js_args += $I("tblOpciones").rows[iFila].id;
	            mostrarProcesando();
	            RealizarCallBack(js_args, "");
	            return;
	        } catch (e) {
	            mostrarErrorAplicacion("Error al eliminar la foto-2", e.message);
	        }
	    }
	    function RespuestaCallBack(strResultado, context) {
	        actualizarSession();
	        var aResul = strResultado.split("@#@");
	        if (aResul[1] != "OK") {
	            ocultarProcesando();
	            var reg = /\\n/g;
	            mostrarError(aResul[2].replace(reg, "\n"));
	        } else {
	            switch (aResul[0]) {
	                case "eliminar":
	                    $I("tblOpciones").deleteRow(iFila);
	                    iFila = -1;
	                    ocultarProcesando();
	                    break;
	                default:
	                    ocultarProcesando();
	                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
	            }
	            ocultarProcesando();
	        }
	    }

    </script>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">

    var strErrores = "<%=strErrores%>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
    num_empleado= "<%=Session["UsuarioActual"]%>";

</script>
<center>
<table style="width:520px; margin-top:20px; text-align:left;">		
<tr>
    <td>
        <table style="width:520px">
        <tr>
            <td>			    
				<table id="tblTitulo" style="height:17px; width:500px;">
				    <colgroup><col style="width:120px;" /><col style="width:200px;" /><col style="width:180px;" /></colgroup>
					<tr class="TBLINI">
						<td style="padding-left:3px;">F. Creación</td>
						<td>
							<img id="imgLupa2" style="display: none; cursor: pointer; height:11px; width:20px;" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa2')" src="../../../../Images/imgLupaMas.gif"/>
						    <img style="cursor: pointer; height:11px; width:20px;" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa2', event)" src="../../../../Images/imgLupa.gif" /> 
							&nbsp;Autor
				        </td>
				        <td style="text-align:right; padding-right:5px;">Consumos IAP a:</td>
					</tr>
				</table>
            </td>
        </tr>
        <tr>
            <td>
	            <div id="divCatalogo" style="overflow: auto; width: 516px; height:336px">
		            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:500px;">
			            <%=strTablaHtml %>
		            </div>
	            </div>					
	            <table style="width: 500px; height: 17px">
		            <tr class="TBLFIN">
			            <td></td>
		            </tr>
	            </table>
            </td>
        </tr>
        </table>
    </td>
</tr>            
</table>
<table style="margin-top:15px; width:210px;">
	<tr>
		<td>
            <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" style="display:inline;" 
                runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../Images/Botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
            </button>    
            <button id="btnSalir" type="button" onclick="cerrarVentana()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                style="margin-left:20px; display:inline;" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
            </button>    
		</td>
	</tr>
</table>	
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">

    function aceptarClick(indexFila) {
        strOpciones = "";

        for (i = 0; i < $I("tblOpciones").rows.length; i++) {
            strOpciones += $I("tblOpciones").rows[i].id + "///" +
		                   $I("tblOpciones").rows[i].cells[0].innerText + "///" +
		                   $I("tblOpciones").rows[i].cells[2].innerText + "@#@";
        }
        strOpciones = indexFila + "@#@" + strOpciones;

        strOpciones = strOpciones.substring(0, strOpciones.length - 3);
        var returnValue = strOpciones;
        modalDialog.Close(window, returnValue);
    }

    function cerrarVentana() {
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }

    </script>
</body>
</html>
