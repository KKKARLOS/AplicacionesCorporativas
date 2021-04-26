   
function init() {
    try {
        ocultarProcesando();
        if (!mostrarErrores()) return;
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptar() {
    try {
        if ($I("cboFechaFra").value == "") return;

        if ($I("txtFecEnvio").value != "" && $I("cboHoraEnvio").value == "") {
            alert("Se ha especificado la fecha de envío pero no la hora de envío");
            return;
        }

        if ($I("txtFecEnvio").value == "" && $I("cboHoraEnvio").value != "") {
            alert("Se ha especificado la hora de envío pero no la fecha de envío");
            return;
        }
        jqConfirm("", "Este proceso va realizar un envío masivo del consumo del mes y año seleccionado a todos los usuarios de líneas.<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
            if (answer) {
                aceptar2();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}
function aceptar2() {
    try {

        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("mail@#@");
        //sb.Append(Utilidades.escape($I("txtAnno").value + "/" + $I("cboMes").value + "/01") + "@#@");    // 1 Año/mes/01
        sb.Append(Utilidades.escape($I("cboFechaFra").value) + "@#@");    // 1 Año/mes/01
        sb.Append(Utilidades.escape($I("txtFecEnvio").value + " " + $I("cboHoraEnvio").value) + "@#@");
        //sb.Append(Utilidades.escape($I("cboHoraEnvio").value) + "@#@");
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar2", e.message);
    }
}
function setAnno(sOpcion) {
    try {
        if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
        else $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el año", e.message);
    }
}

function cerrarVentana() {
    try {
        var returnValue = null;
        modalDialog.Close(window, returnValue);	   
//        window.returnValue = null;
//        window.close();
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        alert(aResul[1]);      
    }else{
        switch (aResul[0]){
            case "mail":
                ocultarProcesando();
                //mmoff("Correo enviado correctamente", 200);
                alert("Correo enviado correctamente");
                cerrarVentana();
                //window.focus();
                break;
   
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
    }
}
