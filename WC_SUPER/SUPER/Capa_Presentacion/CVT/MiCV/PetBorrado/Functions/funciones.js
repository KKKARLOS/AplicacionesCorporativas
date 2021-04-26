var bSaliendo = false;
var bEnviar = false;
function init() {
    try {
        //alert("Peticionario: " + sIdPeticionario);
        if ($I("hdnErrores").value != "") {
            var reg = /\\n/g;
            var strMsg = $I("hdnErrores").value;
            strMsg = strMsg.replace(reg, "\n");
            mostrarError(strMsg);
        }
        ocultarProcesando();
        desActivarGrabar();
        $I("txtMotivo").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload() {
    if (!bSaliendo) 
        salir();
}
function salir() {
    bSaliendo = true;
    //if (bCambios && intSession > 0) {
    //    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    //        if (answer)
    //            bEnviar = grabar();
    //        bCambios = false;
    //        continuarSalir()
    //    });
    //} else continuarSalir();
    continuarSalir();
}
function continuarSalir() {
    var strRetorno = null;
    bCambios = false;
    if (bEnviar) strRetorno = "T";
    modalDialog.Close(window, strRetorno);
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
            case "grabar":
                bEnviar = true;
                desActivarGrabar();
                ocultarProcesando();
                mmoff("Suc", "Envío correcto", 160);
                //if (bSalir) 
                    salir();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
    }
}
function enviarsalir() {
    if (getOp($I("btnEnviar")) != 100) return;
    if (!comprobarDatos()) return;
    setOp($I("btnEnviar"), 30);
    enviar();
}
function enviar() {
    try {
        var sDatosCorreo = Utilidades.escape($I("txtSolic").value)
                             + "#/#" + Utilidades.escape($I("txtProf").value)
                             + "#/#" + Utilidades.escape($I("txtAptdo").value)
                             + "#/#" + Utilidades.escape($I("txtElem").value)
                             + "@#@";
        var js_args = "grabar@#@";
        js_args += $I("nIdPeticionario").value + "@#@"; 
        js_args += $I("hdnTipo").value + "@#@";
        js_args += $I("hdnKey").value + "@#@";
        js_args += Utilidades.escape($I("txtMotivo").value) + "@#@";
        js_args += sDatosCorreo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    }
    catch (e) {
        mostrarErrorAplicacion("Error al enviar la petición de borrado", e.message);
    }
}
function comprobarDatos() {
    try {
        if ($I("txtMotivo").value == "") {
            mmoff("War", "Debes indicar el motivo", 230);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a enviar", e.message);
    }
}
function activarGrabar() {
    try {
        setOp($I("btnEnviar"), 100);
        //bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de enviar", e.message);
    }
}
function desActivarGrabar() {
    try {
        setOp($I("btnEnviar"), 30);

        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}

function maximaLongitud(oControl, maxlong) {
    var in_value, out_value;

    if (oControl.value.length > maxlong) {
        /*con estas 3 sentencias se consigue que el texto se reduzca
        al tamaño maximo permitido, sustituyendo lo que se haya
        introducido, por los primeros caracteres hasta dicho limite*/
        in_value = oControl.value;
        out_value = in_value.substring(0, maxlong);
        oControl.value = out_value;
        $I("lblCaracteres").innerText = 0;
        mmoff("Inf", "La longitud máxima del comentario es de " + maxlong + " caracteres.", 370);
        return false;
    }
    $I("lblCaracteres").innerText = (maxlong - oControl.value.length).ToString("N", 5, 0);
    return true;
}