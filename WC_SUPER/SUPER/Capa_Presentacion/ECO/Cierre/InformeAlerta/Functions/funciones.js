function init() {
    try {
        if (!mostrarErrores()) return;

        //obtenerAlertas();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana() {
    try {
        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getAlertas":
                mmoff("hide");
                $I("divCatalogo").children[0].innerHTML = aResul[2];
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

function obtenerAlertas() {
    try {
        //alert(sIdSegMes);
        if (sIdSegMes == "") {
            mmoff("War", "No hay proyectos procesables para comprobar alertas.", 350, 2500);
            return;
        }
        mostrarProcesando();
        mmoff("InfPer", "Realizando comprobaciones de alertas.", 280);

        var js_args = "getAlertas@#@";
        js_args += sIdSegMes;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las alertas.", e.message);
    }
}

function getInforme(oControl){
    try {
        var oFila = null;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }
        if (oFila == null) {
            mmoff("Err", "No se ha podido determinar la fila", 250);
            return;
        }
        mmoff("WarPer", "idSegMes: " + oFila.getAttribute("idSegMes") + "\nidAlerta: " + oFila.getAttribute("idAlerta"),400); 

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar el informe económico.", e.message);
    }
}

