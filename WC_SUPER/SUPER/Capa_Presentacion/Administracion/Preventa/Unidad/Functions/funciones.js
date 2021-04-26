var returnValue = "";
var bHayCambios = false;

function init() {
    try {
        var idCodigo = -1;
        if (!mostrarErrores()) return;
        if ($I("txtCodigo").value != "") {
            $I("lblCodigo").style.visibility = "visible";
            $I("txtCodigo").style.visibility = "visible";
            idCodigo = parseInt($I("txtCodigo").value);
        }
        else
            $I("chkActiva").checked = true;

        if (idCodigo != -1) {
            $I("chkActiva").focus();
            $I("txtDenominacion").onkeyup = null;
        }
        else
            $I("txtDenominacion").focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload() {
    //window.returnValue = $I("txtDenominacion").value;
}
function grabarSalir() {
    bSalir = true;
    grabar();
}
function salir() {
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabarSalir();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else {
        //returnValue = bHayCambios + "///" + $I("txtDenominacion").value + "///" + $I("chkActiva").checked + "///" + $I("txtCodigo").value;
        returnValue = bHayCambios + "///" + $I("txtDenominacion").value + "///" + $I("txtCodigo").value;
        modalDialog.Close(window, returnValue);
    }
}
function aG() {//Sustituye a activarGrabar
    try {
        if (!bCambios) {
            bCambios = true;
            bHayCambios = true;
            setOp($I("btnGrabarSalir"), 100);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        if (aResul[1] == "AVISO") {
            mmoff("War", aResul[2], 390);
        } else {            
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        }
        

    } else {
        switch (aResul[0]) {
            case "grabar":
                bCambios = false;
                $I("txtCodigo").value = aResul[2];
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                setTimeout("salir();", 50);

                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function comprobarDatos() {
    try {
        if ($I("txtDenominacion").value == "") {
            $I("txtDenominacion").focus();
            mmoff("War", "Se debe indicar la denominación", 320);
            return false;
        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar() {
    try {
        if (getOp($I("btnGrabarSalir")) != 100) return;
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@";
        js_args += $I("txtCodigo").value + "@#@";  //1 Código       
        js_args += Utilidades.escape($I("txtDenominacion").value) + "@#@";   //2 Denominanción 
        js_args += ($I("chkActiva").checked) ? "1" : "0";        //3 Activa

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la unidad de preventa", e.message);
    }
}





