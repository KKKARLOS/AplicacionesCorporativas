//cambioDoc = false;
//var exts = "doc|docx|txt|pdf|html|htm|pps|odt|jpg|gif|xls"; //extension de los archivos permitidos
var primeraVez = true;
var bHayCambios = false;


function init() {
        
}


function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {       
        var reg = /\\n/g;
        ocultarProcesando();
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                bCambios = false;
                bHayCambios = true;
                $I("hdnIdSolicitud").value = aResul[2];                
                mmoff("Inf", "Renovación enviada correctamente.", 250);
                setTimeout("cerrarVentana();", 2000);                
                break;
            case "documentos":
                $I("txtNombreDocumento").value = aResul[2];
                break;
            case "borrarDoc":
                setTimeout("addDoc();", 100);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function cerrarVentana() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}


function salir() {
    bSalir = false;
    bSaliendo = true;
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer)
                bEnviar = grabar("P", "");
            else {
                bCambios = false;
                continuarSalir()
            }
        });
    } else continuarSalir();
}
function continuarSalir() {
    var returnValue = "F";
    if (bHayCambios) returnValue = "T";
    modalDialog.Close(window, returnValue);
}

function enviar() {
    try {
        if (getOp($I("btnEnviar")) != 100) return;
        grabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Tramitar", e.message);
    }
}

function grabar() {
    try {
        if (!comprobarDatos()) {
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        var js_args = "grabar@#@" + $I("hdnTipo").value + "##" + Utilidades.escape($I("txtDenom").value) + "##" + sIDDocuAux + "##" +
                                    $I("txtFechaO").value + "##" + $I("hdnIdFicepiExamen").value;
        RealizarCallBack(js_args, "");
        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer los datos a grabar", e.message);
        return false;
    }
}
function addDoc() {
    if ($I("hdnIdSolicitud").value == "-1") {
        nuevoDoc("SC", sIDDocuAux);
    }
    else {
        nuevoDoc("SC", $I("hdnIdSolicitud").value);
    }
}

function comprobarDatos() {
    try {
        
        if ($I("txtFechaO").value == "") {
            ocultarProcesando();
            mmoff("War", "Debes elegir una fecha de obtención", 380, 2500);
            return false;
        }
        if ($I("txtNombreDocumento").value == "") {
            ocultarProcesando();
            mmoff("War", "El documento acreditativo del examen es dato obligatorio", 420, 2500);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

