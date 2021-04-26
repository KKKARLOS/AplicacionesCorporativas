function init() {
    try {
        nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
        $I("txtAnnoNes").value = AnoMesToMesAnoDescLong(nAnoMesActual);

        if (nAnoMesGenDialogos != 0) {
            $I("txtMesGen").value = AnoMesToMesAnoDescLong(nAnoMesGenDialogos);
        } else {
            setOp($I("btnBorrar"), 30);
            setOp($I("btnEjecutar"), 30);
        }
        getHistorial();
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function cambiarMes(nMes) {
    try {
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtAnnoNes").value = AnoMesToMesAnoDescLong(nAnoMesActual);
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar de mes", e.message);
    }
}
function asignar() {
    try {
        $I("txtMesGen").value = $I("txtAnnoNes").value;
        nAnoMesGenDialogos = nAnoMesActual;

        grabar("activar");
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar el año/mes", e.message);
    }
}
function borrarAsig() {
    try {
        $I("txtMesGen").value = "";
        nAnoMesGenDialogos = 0;

        grabar("desactivar");
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el año/mes", e.message);
    }
}
function ejecutar() {
    try {
        var strMsg = "Este proceso está diseñado para ser ejecutado de forma desatendida y por la noche.<br /><br />Si deseas ejecutarlo ON-LINE, debes saber que puede durar varios minutos durante los cuales el servidor de base de datos de la compañía quedará colapsado y puede afectar al trabajo del resto de profesionales.<br /><br />Siendo consciente de lo dicho, pulsa ACEPTAR para ejecutarlo ON-LINE; pulsa CANCELAR para que se ejecute automáticamente a la noche.";
        jqConfirm("", strMsg, "", "", "war", 600).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "ejecutar@#@";
                RealizarCallBack(js_args, "");
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar.", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    try {
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK") {
            mostrarErrorSQL(aResul[3], aResul[2]);
        } else {
            switch (aResul[0]) {
                case "activar":
                    mmoff("Suc", "Proceso nocturno activado.", 200, null, null, null, 400);
                    setOp($I("btnBorrar"), 100);
                    setOp($I("btnEjecutar"), 100);
                    break;
                case "desactivar":
                    mmoff("Suc", "Proceso nocturno desactivado.", 230, null, null, null, 400);
                    setOp($I("btnBorrar"), 30);
                    setOp($I("btnEjecutar"), 30);
                    break;
                case "ejecutar":
                    $I("txtMesGen").value = "";
                    ocultarProcesando();
                    mmoff("Suc", "Proceso ejecutado correctamente.", 230, null, null, null, 400);
                    setOp($I("btnBorrar"), 30);
                    setOp($I("btnEjecutar"), 30);
                    setTimeout("getHistorial();", 20);
                    break;
                case "getHistorial":
                    $I("divCatalogo").scrollTop = 0;
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    break;

                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;

            }
            ocultarProcesando();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la respuesta del callback.", e.message);
    }
    window.focus();
}

function grabar(sOpcion) {
    try {
        var js_args = sOpcion + "@#@";
        js_args += nAnoMesGenDialogos + "@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar el año/mes genaración de dialogo.", e.message);
    }
}

function getHistorial() {
    try {
        var js_args = "getHistorial@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el historial de ejecuciones..", e.message);
    }
}

