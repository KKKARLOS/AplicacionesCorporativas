var bHayCambios = false;
var bSaliendo = false;
function init() {
    bCambios = false;
    $I("txtCip").focus();
    ocultarProcesando();
}
function ponerFoco(campo) {
    if (event.keyCode == 9 || event.keyCode == 13) {
        event.keyCode = 0;
        $I(campo).focus();
    }
    else
        activarGrabar(); 
}
function unload() {
    if (!bSaliendo)
        salir();
}

function salir() {
    bSaliendo = true;
    if (bCambios && intSession > 1) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                bEnviar = grabar();
            }
            else {
                bCambios = false;
                CerrarVentana();
            }
        });
    }
    else CerrarVentana();
}
function CerrarVentana() {
    var strRetorno;
    if (bHayCambios) strRetorno = "T@#@";
    else strRetorno = "F@#@";
    strRetorno += $I("hdnIdSuper").value + "@#@" + $I("hdnNombre").value + "@#@" + $I("hdnIdCalendario").value + "@#@" +
                  $I("txtCalendario").value + "@#@" + getRadioButtonSelectedValue("rdbSexo", true);
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var sMensError = "";
        switch (aResul[2]) {
            case "E1":
                sMensError = "El profesional existe en FICEPI pero no en SUPER\n\nPóngase en contacto con el CAU-DEF";
                break;
            case "E2":
                sMensError = "El profesional ya existe. No se puede tramitar el alta.";
                break;
            case "E3":
                sMensError = "El profesional existe en FICEPI pero el usuario SUPER va a ser dado de baja\n\nPóngase en contacto con el CAU-DEF";
                break;
            case "E4":
                sMensError = "El profesional existe en FICEPI pero el usuario SUPER está de baja\n\nPóngase en contacto con el CAU-DEF";
                break;
            case "E5":
                //sMensError = "Existe un profesional dado de alta con ese CIP, pero su nombre es\n\n" + aResul[3];
                sMensError = "Alta denegada.\nExiste un profesional en el sistema con el mismo CIP.\n\nSu nombre responde a:\n\n" + aResul[3];
                break;
            case "E6":
                sMensError = "Existe un profesional no foráneo dado de alta con ese CIP, cuyo nombre es\n\n" + aResul[3];
                break;
            default:
                sMensError = aResul[2];
                break;
        }
        var reg = /\\n/g;
        mostrarError(sMensError.replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                bCambios = false;
                bHayCambios = true;
                setOp($I("btnGrabar"), 30);
                //setOp($I("btnGrabarSalir"), 30);
                $I("hdnIdSuper").value = aResul[2];
                $I("hdnNombre").value = aResul[3];
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                setTimeout("salir();", 500);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
    }
}
function grabarAux() {
    //bSalir = false;
    grabar();
}
function grabar() {
    var sOPcionBD, sAux, js_args = "";
    try {
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        js_args = "grabar@#@I##";
        js_args += Utilidades.escape($I("txtCip").value) + "##"; //1 NIF
        js_args += Utilidades.escape($I("txtApe1").value) + "##"; //2
        js_args += Utilidades.escape($I("txtApe2").value) + "##"; //3
        js_args += Utilidades.escape($I("txtNombre").value) + "##"; //4
        js_args += getRadioButtonSelectedValue("rdbSexo", true) + "##"; //5 sexo
        js_args += Utilidades.escape($I("txtEmail").value) + "##"; //6
        //js_args += Utilidades.escape($I("txtTfno").value) + "##"; //7
        js_args += $I("hdnIdCalendario").value + "##"; //8
        js_args += $I("txtFAlta").value
        //js_args = "///";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del foráneo", e.message);
    }
}
function comprobarDatos() {
    try {
        if ($I("txtCip").value == "") {
            mmoff("War", "Debes indicar el CIP", 230);
            return false;
        }
        if ($I("txtFAlta").value == "") {
            mmoff("War", "Debes indicar la fecha de alta", 230);
            return false;
        }
        //Victor 08/05/2014 La fecha de alta no puede ser inferior al día 1 del mes actual
        var anio, mes;
        var Mi_Fecha = new Date();
        anio = Mi_Fecha.getFullYear();
        mes = Mi_Fecha.getMonth() + 1;
        var sFechaIni = "1/" + mes + "/" + anio;
        if (DiffDiasFechas(sFechaIni, $I("txtFAlta").value) < 0) {
            mmoff("War", "La fecha de alta debe ser posterior al primer día del mes actual.", 300, 4000);
            return false;
        }
        
        if ($I("txtNombre").value == "") {
            mmoff("War", "Debes indicar el Nombre", 230);
            return false;
        }
        if ($I("txtApe1").value == "") {
            mmoff("War", "Debes indicar el Apellido 1", 230);
            return false;
        }
        if ($I("txtEmail").value == "") {
            mmoff("War", "Debes indicar el E-mail", 230);
            return false;
        }
        if (!validarEmail(fTrim($I("txtEmail").value))) {
            mmoff("War", "El E-mail no tiene un formato correcto", 300);
            return false;
        }
        if ($I("hdnIdCalendario").value == "") {
            mmoff("War", "Debes indicar el Calendario", 230);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function getCalendario() {
    try {

        mostrarProcesando();

        //var strEnlace = "../../getCalendario.aspx?idficepi=" + oFila.getAttribute("idficepi");
        var strEnlace = strServer + "Capa_Presentacion/PSP/getCalendario.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sSize(470, 450));
        modalDialog.Show(strEnlace, self, sSize(470, 450))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCalendario").value = aDatos[0];
                    $I("txtCalendario").value = aDatos[1];
                    activarGrabar();
                }
                $I("txtCip").focus();
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el calendario", e.message);
    }
}
