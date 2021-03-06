<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getPreferencia.aspx.cs" Inherits="getPreferencia" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Selección de preferencia</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	    function init() {
	        try {
	            if (!mostrarErrores()) return;
	            actualizarLupas("tblTitulo", "tblDatos");
	            setOp($I("btnMant"), 30);
	            if ($I("tblDatos").rows.length > 0) setOp($I("btnMant"), 100);
	            ocultarProcesando();
	            window.focus();
	        } catch (e) {
	            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	        }
	    }

	    function aceptarClick(oCelda) {
	        try {
	            if (bProcesando()) return;

	            var returnValue = oCelda.parentNode.getAttribute("id");
	            modalDialog.Close(window, returnValue);
	        } catch (e) {
	            mostrarErrorAplicacion("Error seleccionar la fila", e.message);
	        }
	    }

	    function salir() {
	        try {
	            if (bProcesando()) return;

	            var returnValue = null;
	            modalDialog.Close(window, returnValue);
	        } catch (e) {
	            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
	        }
	    }

	    function setDefecto(objChk) {
	        try {
	            var nIdPrefUsuario = 0;
	            var tblDatos = $I("tblDatos");
	            for (var i = 0; i < tblDatos.rows.length; i++) {
	                if (tblDatos.rows[i].cells[1].children[0] == objChk) {
	                    if (tblDatos.rows[i].cells[1].children[0].checked == false) {
	                        tblDatos.rows[i].cells[1].children[0].checked = true;
	                        window.focus();
	                        return;
	                    }
	                    tblDatos.rows[i].cells[1].children[0].checked = true;
	                    nIdPrefUsuario = tblDatos.rows[i].getAttribute("id");
	                } else {
	                    tblDatos.rows[i].cells[1].children[0].checked = false;
	                }
	            }
	            window.focus();

	            var js_args = "setDefecto@#@";
	            js_args += nIdPrefUsuario

	            RealizarCallBack(js_args, "");

	        } catch (e) {
	            mostrarErrorAplicacion("Error al establecer la preferencia por defecto.", e.message);
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
	                case "setDefecto":
	                    mmoff("Suc", "Defecto modificado.", 200);
	                    break;
	                case "getPreferencia":
	                    $I("divCatalogo").scrollTop = 0;
	                    $I("divCatalogo").children[0].innerHTML = aResul[2];
	                    break;

	                default:
	                    ocultarProcesando();
	                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
	                    break;
	            }
	            ocultarProcesando();
	        }
	    }

	    function mostrarMantenimiento() {
	        try {
	            mostrarProcesando();
	            var strEnlace = strServer + "Capa_Presentacion/getPreferenciaMant.aspx?nP=" + codpar(nPantalla);
	            //window.focus();
	            modalDialog.Show(strEnlace, self, sSize(450, 470))
                    .then(function(ret) {
                        if (ret != null){
                            if (ret.resultado == "OK") {
                                var js_args = "getPreferencia@#@";
                                js_args += nPantalla;
                                RealizarCallBack(js_args, "");
                                borrarCatalogo();
                            }
                        }
	                });

	            ocultarProcesando();
	        } catch (e) {
	            mostrarErrorAplicacion("Error al ir a mantener las preferencias.", e.message);
	        }
	    }

	    function borrarCatalogo() {
	        try {
	            $I("divCatalogo").children[0].innerHTML = "";
	        } catch (e) {
	            mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	        }
	    }
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">

    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
	var nPantalla = "<%=sPantalla %>";

</script>
<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
<table style="margin-left:10px; width:420px;" cellpadding="5px">
    <tr>
        <td>
            <table id="tblTitulo" style="WIDTH:400px; HEIGHT:17px;">
                <colgroup>
                    <col style='width:375px;' />
                    <col style='width:25px;' />
                </colgroup>
                <tr class="TBLINI">
                    <td style='padding-left:3px;'>Denominación&nbsp;
                        <IMG id="imgLupa" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
			                    height="11" src="../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
			            <IMG style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa', event)"
			                    height="11" src="../Images/imgLupa.gif" width="20" tipolupa="1">
			        </td>
			        <td title="Preferencia por defecto" style='text-align:center;'>Def.</td>
                </tr>
            </table>
            <div id="divCatalogo" style="OVERFLOW:auto; OVERFLOW-X: hidden; width:416px; height:380px">
                <div style="background-image: url('../Images/imgFT20.gif'); background-repeat:repeat; width:400px; height:auto;">
                <%=strTablaHTML %>
                </div>
            </div>
            <table style="WIDTH: 400px; HEIGHT: 17px;">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<center>
<table style="margin-top:5px; width:220px;">
	<tr>
		<td>
            <button id="btnMant" type="button" onclick="mostrarMantenimiento();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <span><img src="../images/imgEdicion.gif" />&nbsp;Editar</span>
            </button>
		</td>
		<td style="padding-left:10px;">
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <span><img src="../images/botones/imgSalir.gif" />&nbsp;Salir</span>
            </button>
		</td>
	</tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
