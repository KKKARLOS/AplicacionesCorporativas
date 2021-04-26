function init() {
    try {
        if (!mostrarErrores()) return;
        $I("txtApellido1").focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        alert(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "profesionales":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                break;
        }
        ocultarProcesando();
    }
}

function mostrarProfesionales() {
    try {
        if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == "") {
            alert("Debe introducir algún criterio de búsqueda");
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "";
        //if (sTipo==1) js_args = "profesionales@#@";
        //else if (sTipo==2) js_args = "responsables@#@";

        js_args = "profesionales@#@";
        js_args += Utilidades.escape($I("txtApellido1").value) + "@#@";
        js_args += Utilidades.escape($I("txtApellido2").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombre").value) + "@#@";
        js_args += sTipo;
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;

    } catch (e) {
        if (sTipo == 1) mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
        //else if (sTipo==2) mostrarErrorAplicacion("Error al obtener la relación de responsables", e.message);
    }
}

function aceptarClick(indexFila) {
    try {
        if (bProcesando()) return;
//        window.returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].cells[0].innerText;
//        window.close();
        var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].cells[0].innerText;
        modalDialog.Close(window, returnValue);
        
    } catch (e) {
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function cerrarVentana() {
    try {
        if (bProcesando()) return;
        var returnValue = null;
        modalDialog.Close(window, returnValue);

//        window.returnValue = null;
//        window.close();
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}