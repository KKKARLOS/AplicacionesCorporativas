var bAlgunCambio = false;

function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function grabarSalir() {
    grabar();
}

function cancelar() {
    var returnValue = (bAlgunCambio) ? "T" : "F";
    modalDialog.Close(window, returnValue);	
}

function grabar() {
    try {
        if (getOp($I("btnGrabarSalir")) != 100) return;
        if (!comprobarDatos()) return;
        mostrarProcesando();
        var js_args = "grabar@#@";
        js_args += $I("hdnIdDialogo").value + "#/#"; //
        js_args += $I("hdnPosicionMsg").value + "#/#"; //
        js_args += Utilidades.escape($I("txtMsg").value) + "#/#"; //Mensaje
        js_args += sUsuarioResponsable;
        
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al enviar el mensaje", e.message);
    }
}

function comprobarDatos() {
    try {
        if ($I("txtMsg").value == "") {
            mmoff("War", "El mensaje es dato obligatorio.", 250);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar() {
    try {
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        setOp($I("btnGrabarSalir"), 30);
        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                bAlgunCambio = true;
                //mmoff("Suc", "Grabación correcta", 220);
                setTimeout("cancelar()", 20);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
