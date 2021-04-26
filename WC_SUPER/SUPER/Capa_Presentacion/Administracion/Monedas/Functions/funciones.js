var aFila;
var bBuscar = false;
var nOpcion = 0;

function init() {
    try {
        if (!mostrarErrores()) return;
        $I("chkEstado").style.verticalAlign = "middle";
        var aChild = $I("cldCheck").children[0].children;
        for (var i = 0; i < aChild.length; i++)
            aChild[i].style.cursor = "pointer";

        actualizarLupas("tblTitulo", "tblDatos");
        nOpcion = $I("cboActualizacionTCA").value;
        setExcelImg("imgExcel", "divCatalogo", "excel");
        ToolTipBotonera("historico", "Muestra mes a mes los diferentes tipos de cambio de la moneda");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function borrarFecha(oControl) {
    oControl.parentNode.parentNode.cells[4].children[0].value = "";
    oControl.parentNode.parentNode.setAttribute("fecha","");
    oControl.style.visibility = "hidden";
    mfa(oControl.parentNode.parentNode, "U");
    activarGrabar();
}

function getMesValor(oControl) {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getUnMes.aspx", self, sSize(270, 215))
            .then(function(ret) {
                if (ret != null) {
                    if (ret < $I('hdnFechaAnoMes').value && ret != "") {
                        ocultarProcesando();
                        mmoff("War", "¡ Atención !\n\n La fecha 'A partir de' especificada es anterior a la fecha actual.", 400);
                        return false;
                    }
                    mod(oControl);
                    oControl.parentNode.parentNode.setAttribute("fecha", ret);
                    oControl.value = AnoMesToMesAnoDescLong(ret);
                    oControl.parentNode.parentNode.cells[4].children[1].style.visibility = "visible";
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el valor del mes.", e.message);
    }
}
function mod(obj) {
    //    if (obj.parentNode != null) {
    if (obj.parentNode.parentNode.cells[2].children[0].value != "")
        if (parseFloat(dfn(obj.parentNode.parentNode.cells[2].children[0].value)) < 0 || obj.parentNode.parentNode.cells[2].children[0].value == "-") {
        mmoff("Inf","El tipo de cambio 'actual' no puede ser negativo.",330);
        obj.parentNode.parentNode.cells[2].children[0].value = 0;
        return;
    }
    if (obj.parentNode.parentNode.cells[3].children[0].value != "")
        if (parseFloat(dfn(obj.parentNode.parentNode.cells[3].children[0].value)) < 0 || obj.parentNode.parentNode.cells[3].children[0].value == "-") {
        mmoff("Inf", "El tipo de cambio 'siguiente' no puede ser negativo.",340);
        obj.parentNode.parentNode.cells[3].children[0].value = 0;
        return;
    }
    //    }     
    mfa(obj.parentNode.parentNode, "U");
    activarGrabar();
}
function cont(obj) {
    if (obj.parentNode.parentNode.cells[7].children[0].checked) obj.parentNode.parentNode.cells[8].children[0].checked = true;

    mod(obj);
}
//function unload() {
//    try {
//        if (bCambios) {
//            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
//                if (answer) {
//                    grabar();
//                }
//            });
//        }
//    }
//    catch (e) {
//        mostrarErrorAplicacion("Error en la función unload", e.message);
//    }
//}
//function salir() {
//    bSalir = false;
//    var returnValue = null;

//    if (bCambios) {
//        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
//            if (answer) {
//                bSalir = true;
//                grabar();
//            }
//            else {
//                bCambios = false;
//                modalDialog.Close(window, returnValue);
//            }
//        });
//    }
//    else modalDialog.Close(window, returnValue);
//}
function comprobarDatos() {
    try {
        aFila = FilasDe("tblDatos");
        var iTipoCambio;
        var iTipoCambioSiguiente;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                sNum = aFila[i].cells[2].children[0].value;
                if (sNum == "0" || sNum == "0,0" || sNum == "0,00" || sNum == "0,000" || sNum == "0,0000") sNum = "";
                iTipoCambio = (sNum == "") ? -1 : parseInt(sNum);

                sNum = aFila[i].cells[3].children[0].value;
                if (sNum == "0" || sNum == "0,0" || sNum == "0,00" || sNum == "0,000" || sNum == "0,0000") sNum = "";
                iTipoCambioSiguiente = (sNum == "") ? -1 : parseInt(sNum);

                if ((aFila[i].cells[8].children[0].checked == true || aFila[i].cells[7].children[0].checked == true) && iTipoCambio < 0) {
                    ms(aFila[i]);
                    mmoff("War", "¡ Atención !\n\nExiste alguna moneda sin el tipo de cambio asignado o asignado de forma incorrecta, teniendo el estado de gestión o visualización de la moneda activado.",400);
                    return false;
                }
                if (iTipoCambioSiguiente > 0 && aFila[i].cells[4].children[0].value == "") {
                    ms(aFila[i]);
                    mmoff("War", "¡ Atención !\n\nExiste alguna moneda con el tipo de cambio 'Siguiente' asignado correctamente pero sin especificar la fecha 'A partir de'.",400);
                    return false;
                }
                if (aFila[i].cells[3].children[0].value == "" && aFila[i].cells[4].children[0].value != "") {
                    ms(aFila[i]);
                    mmoff("War", "¡ Atención !\n\nExiste alguna moneda con el tipo de cambio 'Siguiente' no especificado pero con la fecha 'A partir de' especificada.",400);
                    return false;
                }
                if (aFila[i].cells[3].children[0].value != "" && aFila[i].cells[4].children[0].value == "") {
                    ms(aFila[i]);
                    mmoff("War", "¡ Atención !\n\nExiste alguna moneda con el tipo de cambio 'Siguiente' especificado pero con la fecha 'A partir de' sin especificar.",400);
                    return false;
                }
                if (aFila[i].cells[3].children[0].value == "" && aFila[i].cells[4].children[0].value != "") {
                    ms(aFila[i]);
                    mmoff("War", "¡ Atención !\n\nExiste alguna moneda con el tipo de cambio 'Siguiente' sin especificar pero con la fecha 'A partir de' especificada.",400);
                    return false;
                }

                if (aFila[i].getAttribute("fecha") <= $I('hdnFechaAnoMes').value && aFila[i].getAttribute("fecha") != "") {
                    ms(aFila[i]);
                    mmoff("War","¡ Atención !\n\n La fecha 'A partir de' especificada es anterior o igual al año/mes actual.",400);
                    return false;
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar() {
    try {
        if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis

        var sNum = "";
        sb.Append("grabar@#@");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "U"
                sb.Append(aFila[i].id + "##"); //ID Moneda
                sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) + "##"); //Denominación importes

                sNum = aFila[i].cells[2].children[0].value;
                if (sNum == "0" || sNum == "0,0" || sNum == "0,00" || sNum == "0,000" || sNum == "0,0000") sNum = "";

                sb.Append(sNum + "##"); //T.Cambio actual

                sNum = aFila[i].cells[3].children[0].value;
                if (sNum == "0" || sNum == "0,0" || sNum == "0,00" || sNum == "0,000" || sNum == "0,0000") sNum = "";

                sb.Append(sNum + "##"); //T.Cambio siguiente
                sb.Append(aFila[i].getAttribute("fecha") + "##"); //A partir de
                sb.Append((aFila[i].cells[7].children[0] && aFila[i].cells[7].children[0].checked == true) ? "1##" : "0##"); //Gestión
                sb.Append((aFila[i].cells[8].children[0] && aFila[i].cells[8].children[0].checked == true) ? "1##" : "0##"); //Visibilidad
                sb.Append("///");
                sw = 1;
            }
        }
        if (sw == 0) {
            desActivarGrabar();
            mmoff("War", "No se han modificado los datos.", 230);
            return false;
        }

        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
function buscar() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bResul = grabar();
                    bBuscar = true;
                }
                else {
                    bCambios = false;
                    LLamadaBuscar();
                }
            });
        }
        else LLamadaBuscar();

    } catch (e) {
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}
function LLamadaBuscar() {
    try {
        mostrarProcesando();

        var js_args = "monedas@#@";
        js_args += ($I('chkEstado').checked) ? "0" : "1";
        js_args += "@#@";
        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaBuscar", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].cells[8].children[0]) {
                        if (aFila[i].cells[8].children[0].checked == false) {
                            if ($I('chkEstado').checked == false) $I("tblDatos").deleteRow(i);
                            continue;
                        }
                    }
                    aFila[i].setAttribute("bd", "");
                }
                desActivarGrabar();
                //AccionBotonera("Grabar", "D");
                mmoff("Suc", "Grabación correcta", 160);
                bCambios = false;
                if (bBuscar) {
                    bBuscar = false;
                    setTimeout("buscar();", 100);
                }
                break;
            case "monedas":
                clearVarSel();
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                setExcelImg("imgExcel", "divCatalogo", "excel");
                window.focus();
                break;
            case "setTCA":
                clearVarSel();
                if ($I("cboActualizacionTCA").value == "2" || $I("cboActualizacionTCA").value == "3") {
                    desActivarGrabar();
                    bOcultarProcesando = false;
                    setTimeout("buscar();", 20);
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
function historico() {
    try {
        var sw = 0;
        aFila = FilasDe("tblDatos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS" || aFila[i].getAttribute("class")) {
                mostrarProcesando();
                var strEnlace = strServer + "Capa_Presentacion/Administracion/Monedas/Historico/Default.aspx?id=" + codpar(aFila[i].id) + "&Moneda=" + codpar(aFila[i].cells[0].innerText);
                //window.focus();
                modalDialog.Show(strEnlace, self, sSize(650, 390));
                
                ocultarProcesando();
                sw = 1;
                try { window.event.cancelBubble = true; } catch (e) { };
                break;
            }
        }
        if (sw == 0) {
            mmoff("Inf", "Para ver el histórico, es necesario seleccionar una moneda.", 390);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la función historico", e.message);
    }
}
function excel() {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 320);
            return;
        }
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr style='text-align:center;'>");
        sb.Append("        <td style='background-color: #E4EFF3;width:auto;' colspan='2'>Moneda</TD>");
        sb.Append("        <td style='background-color: #E4EFF3;width:auto;' colspan='5'>Tipo de cambio</TD>");
        sb.Append("        <td style='background-color: #E4EFF3;width:auto;' colspan='2'>Estado</TD>");

        sb.Append("	</tr>");
        sb.Append("	<tr style='text-align:center;'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Denominación</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Ver importes en</TD>");

        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Actual</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Siguiente</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>A partir de</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>TCOD</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>TCOMM</TD>");

        sb.Append("        <td style='width:auto;background-color: #BCD4DF;text-align:center;'>Gestión</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;text-align:center;'>Visibilidad</TD>");
        sb.Append("	</tr>");

        var tblDatos = $I("tblDatos");
        for (var i = 0; i < tblDatos.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < tblDatos.rows[i].cells.length; x++) {
                if (x < 7) {
                    sb.Append("<td>" + ((x==4)?"&nbsp;":"") + getCelda(tblDatos.rows[i], x) + "</td>");
                } else if (x == 7 || x == 8) {
                    sb.Append("<td style='text-align:center;'>");
                    if (tblDatos.rows[i].cells[x].children[0])
                        sb.Append((tblDatos.rows[i].cells[x].children[0].checked) ? "X" : "");
                    sb.Append("</td>");
                }
            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function setTCOD() {
    try {
        if (iFila == -1) {
            mmoff("Inf", "Para establecer un tipo de cambio es necesario seleccionar una moneda.", 470);
            return;
        }
        var oFila = $I("tblDatos").rows[iFila];
        oFila.cells[2].children[0].value = oFila.cells[5].innerText;
        mfa(oFila, "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el tipo de cambio actual con el TCOD", e.message);
    }
}

function setTCOMM() {
    try {
        if (iFila == -1) {
            mmoff("Inf", "Para establecer un tipo de cambio es necesario seleccionar una moneda.", 470);
            return;
        }
        var oFila = $I("tblDatos").rows[iFila];
        oFila.cells[2].children[0].value = oFila.cells[6].innerText;
        mfa(oFila, "U");
        activarGrabar();
    } catch (e) {
    mostrarErrorAplicacion("Error al actualizar el tipo de cambio actual con el TCOMM", e.message);
    }
}

function setTCA() {
    try {
        if ($I("cboActualizacionTCA").value == "2" || $I("cboActualizacionTCA").value == "3") {
            jqConfirm("", "La actualización del tipo de cambio seleccionada supone la modificación en base de datos de forma automática e instantánea de todas las monedas, sin opción de deshacer el cambio.<br><br>Si deseas que la modificación se realice en este instante, pulsa \"Aceptar\". En caso contrario, pulsa \"Cancelar\".", "", "", "war", 450).then(function (answer) {
                if (!answer) {
                    $I("cboActualizacionTCA").value = nOpcion;
                    window.focus();
                }
                else
                    setTCAContinuar();
            });
        }
        else setTCAContinuar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a establecer el sistema de actualización.", e.message);
    }
}
function setTCAContinuar() {
    try {
        nOpcion = $I("cboActualizacionTCA").value;
        window.focus();
        mostrarProcesando();
        var js_args = "setTCA@#@";
        js_args += $I("cboActualizacionTCA").value;

        RealizarCallBack(js_args, "");

    } catch (e) {
        mostrarErrorAplicacion("Error en setTCAContinuar", e.message);
    }
}