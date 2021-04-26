<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getPreferenciaMant.aspx.cs" Inherits="getPreferenciaMant" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Edición de preferencias </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../Javascript/draganddrop.js" type="text/Javascript"></script>
 	<script src="../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">
        var aFila;
        var tbody;
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        var bSaliendo = false;
        var bHayCambios = false;

        function init() {
            try {
                if (!mostrarErrores()) return;
                ocultarProcesando();

                tbody = document.getElementById('tbodyDatos');
                tbody.onmousedown = startDragIMG;

                window.focus();

                aFila = FilasDe("tblDatos");
                var oFila = null;
                if (aFila != null) {
                    if (nIDPreferencia == 0) {
                        oFila = aFila[0];
                    } else {
                        for (var i = 0; i < aFila.length; i++) {
                            if (aFila[i].id == nIDPreferencia) {
                                oFila = aFila[i];
                                break;
                            }
                        }
                    }
                }
                if (oFila != null) {
                    ms(oFila);
                    oFila.cells[2].focus();
                    oFila.cells[2].children[0].select();
                }
            } catch (e) {
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
        function RespuestaCallBack(strResultado, context) {
            actualizarSession();
            var aResul = strResultado.split("@#@");
            if (aResul[1] != "OK") {
                mostrarErrorSQL(aResul[3], aResul[2]);
            } else {
                switch (aResul[0]) {
                    case "setDefecto":
                        mmoff("Inf", "Defecto modificado.", 200);
                        break;
                    case "grabar":
                        bHayCambios = true;
                        for (var i = aFila.length - 1; i >= 0; i--) {
                            if (aFila[i].getAttribute("bd") == "D") {
                                $I("tblDatos").deleteRow(i);
                                continue;
                            }
                            mfa(aFila[i], "N");
                        }
                        nFilaDesde = -1;
                        nFilaHasta = -1;
                        desActivarGrabar();
                        mmoff("Suc", "Grabación correcta.", 160);
                        if (bSalir) setTimeout("salir();", 50);
                        break;
                    default:
                        ocultarProcesando();
                        mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                        break;
                }
                ocultarProcesando();
            }
        }
        function setDefecto(objChk) {
            try {
                var nIdPrefUsuario = 0;
                var tblDatos = $I("tblDatos");
                for (var i = 0; i < tblDatos.rows.length; i++) {
                    if (tblDatos.rows[i].cells[3].children[0] == objChk) {
                        if (tblDatos.rows[i].cells[3].children[0].checked == false) {
                            tblDatos.rows[i].cells[3].children[0].checked = true;
                            window.focus();
                            return;
                        }
                        tblDatos.rows[i].cells[3].children[0].checked = true;
                        nIdPrefUsuario = tblDatos.rows[i].getAttribute("id");
                    } else {
                        if (tblDatos.rows[i].cells[3].children[0].checked == true)
                            fm(tblDatos.rows[i]);
                        tblDatos.rows[i].cells[3].children[0].checked = false;
                    }
                }
                window.focus();
            } catch (e) {
                mostrarErrorAplicacion("Error al establecer la preferencia por defecto.", e.message);
            }
        }
        function eliminar() {
            try {
                if (iFila != -1) modoControles(tblDatos.rows[iFila], false);

                aFila = FilasDe("tblDatos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("class") == "FS") {
                        mfa(aFila[i], "D");
                    }
                }
                activarGrabar();
            } catch (e) {
                mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
            }
        }
        function comprobarDatos() {
            try {
                var nOrden = 0;
                for (var i = 0; i < aFila.length; i++) {
                    if (aFila[i].getAttribute("bd") == "D") continue;

                    if (parseInt(aFila[i].getAttribute("orden"), 10) != nOrden) {
                        if (aFila[i].getAttribute("bd") != "I") aFila[i].setAttribute("bd", "U");
                        aFila[i].setAttribute("orden", nOrden);
                    }
                    nOrden++;
                }
                return true;
            } catch (e) {
                mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
                return false;
            }
        }
        function grabar() {
            try {
                mostrarProcesando();

                aFila = FilasDe("tblDatos");

                if (iFila != -1) modoControles(aFila[iFila], false);

                if (!comprobarDatos()) {
                    ocultarProcesando();
                    return;
                }

                var sb = new StringBuilder; //sin paréntesis

                sb.Append("grabar@#@" + $I("hdnEsCV").value + "@#@");
                var sw = 0;
                for (var i = 0; i < aFila.length; i++) {
                    if (aFila[i].getAttribute("bd") != "") {
                        sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                        sb.Append(aFila[i].getAttribute("id") + "##"); //ID preferenccia
                        sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) + "##"); //Descripcion
                        sb.Append((aFila[i].cells[3].children[0].checked == true) ? "1##" : "0##"); //Defecto
                        sb.Append((parseInt(aFila[i].getAttribute("orden"), 10) > 255) ? "255///" : aFila[i].getAttribute("orden") + "///"); //Orden
                        sw = 1;
                    }
                }
                if (sw == 0) {
                    mmoff("War", "No se han modificado los datos.", 230);
                    ocultarProcesando();
                    return;
                }

                RealizarCallBack(sb.ToString(), "");
            } catch (e) {
                mostrarErrorAplicacion("Error al ir a grabar", e.message);
            }
        }
        function grabarsalir() {
            bSalir = true;
            grabar();
        }
        function unload() {
            if (!bSaliendo) salir();
        }
        function salir() {
            bSalir = false;
            bSaliendo = true;
            if (bCambios && intSession > 0) {
                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                    if (answer)         {
                        bSalir = true;
                        bEnviar = grabar();
                    } 
                    bCambios = false;
                    salirContinuar();
                });
            }else salirContinuar();
        }
        function salirContinuar() {
            var sDenominacion = "";
            if (nIDPreferencia != 0) {
                aFila = FilasDe("tblDatos");
                if (aFila != null) {
                    for (var i = 0; i < aFila.length; i++) {
                        if (parseInt(aFila[i].id, 10) == nIDPreferencia) {
                            sDenominacion = aFila[i].cells[2].children[0].value;
                            break;
                        }
                    }
                }
            }
            var returnValue = {
                resultado: ((bHayCambios) ? "OK" : null),
                denominacion: Utilidades.escape(((bHayCambios) ? sDenominacion : null))  //Si venimos por la edición de una pref. recien creada, devolvemos su denominación para refrescar la pantalla.
            }
            //setTimeout("window.close();", 250); //para que de tiempo a grabar y actualizar "bCambios";
            modalDialog.Close(window, returnValue);
        }
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var nIDPreferencia = <%=nIDPreferencia%>;
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table style="width:420px; margin-left:10px;" cellpadding="5px">
        <tr>
            <td>
                <table style="WIDTH: 400px; HEIGHT: 17px;">
                    <colgroup>
                        <col style='width:375px;' />
                        <col style='width:25px;' />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="padding-left:40px;">Denominación&nbsp;
                            <IMG id="imgLupa" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
		                        height="11" src="../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				            <IMG style="CURSOR: pointer; DISPLAY:none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa', event)"
			                    height="11" src="../Images/imgLupa.gif" width="20" tipolupa="1">
				        </td>
				        <td title="Preferencia por defecto" style='text-align:center;'>Def.</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 416px; height:380px">
                    <div style="background-image: url('../Images/imgFT20.gif'); background-repeat:repeat; width:400px; height:auto;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="WIDTH:400px; HEIGHT:17px;" cellpadding="0">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
    <table style="width:400px;margin-top:5px; margin-left:15px;">
		<tr>
			<td>
                <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../images/botones/imgEliminar.gif" /><span>Eliminar</span>
                </button>
			</td>
			<td>
                <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../images/botones/imgGrabar.gif" /><span>Grabar</span>
                </button>
			</td>
			<td>
                <button id="btnGrabarSalir" type="button" onclick="grabarsalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../images/botones/imgGrabarSalir.gif" /><span>Grabar...</span>
                </button>
			</td>
			<td>
                <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../images/botones/imgSalir.gif" /><span>Salir</span>
                </button>
			</td>
		</tr>
    </table>
    </center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnEsCV" id="hdnEsCV" value="N" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
</body>
</html>
