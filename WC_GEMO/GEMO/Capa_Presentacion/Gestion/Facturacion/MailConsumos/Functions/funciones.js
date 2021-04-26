   
function init() {
    try {
        ocultarProcesando();
        if (!mostrarErrores()) return;
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

function aceptar() {
    try {
        if ($I("cboFechaFra").value == "") return;

        if ($I("txtFecEnvio").value != "" && $I("cboHoraEnvio").value == "") {
            alert("Se ha especificado la fecha de env�o pero no la hora de env�o");
            return;
        }

        if ($I("txtFecEnvio").value == "" && $I("cboHoraEnvio").value != "") {
            alert("Se ha especificado la hora de env�o pero no la fecha de env�o");
            return;
        }
        jqConfirm("", "Este proceso va realizar un env�o masivo del consumo del mes y a�o seleccionado a todos los usuarios de l�neas.<br /><br />�Deseas continuar?", "", "", "war", 400).then(function (answer) {
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

        var sb = new StringBuilder; //sin par�ntesis 

        sb.Append("mail@#@");
        //sb.Append(Utilidades.escape($I("txtAnno").value + "/" + $I("cboMes").value + "/01") + "@#@");    // 1 A�o/mes/01
        sb.Append(Utilidades.escape($I("cboFechaFra").value) + "@#@");    // 1 A�o/mes/01
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
        mostrarErrorAplicacion("Error al seleccionar el a�o", e.message);
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
                alert("Opci�n de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
    }
}
