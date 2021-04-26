function init() {
    try {
        ToolTipBotonera("eliminar", "Elimina el Proyecto Económico seleccionado");
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
    }
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
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                if (aResul[3] == "") {
                    mmoff("Inf","Proyecto inexistente", 160);
                    var x = $I("txtNumPE").value;
                    limpiar();
                    $I("txtNumPE").value = x;
                }
                else {
                    AccionBotonera("eliminar", "H");
                    var aDatos = aResul[3].split("##");
                    $I("txtDesPE").value = Utilidades.unescape(aDatos[0]);
                    $I("txtCliente").value = Utilidades.unescape(aDatos[1]);
                    switch (aDatos[2]) {
                        case "A":
                            $I("imgEst").src = "../../../../images/imgIconoProyAbierto.gif";
                            $I("imgEst").title = "Proyecto abierto";
                            break;
                        case "C":
                            $I("imgEst").src = "../../../../images/imgIconoProyCerrado.gif";
                            $I("imgEst").title = "Proyecto cerrado";
                            break;
                        case "P":
                            $I("imgEst").src = "../../../../images/imgIconoProyPresup.gif";
                            $I("imgEst").title = "Proyecto presupuestado";
                            break;
                        case "H":
                            $I("imgEst").src = "../../../../images/imgIconoProyHistorico.gif";
                            $I("imgEst").title = "Proyecto histórico";
                            break;
                    }
                    switch (aDatos[3]) {
                        case "P": $I("imgCat").src = "../../../../images/imgProducto.gif"; break;
                        case "S": $I("imgCat").src = "../../../../images/imgServicio.gif"; break;
                    }
                    mmoff("Inf", "Pulsa el botón Eliminar para borrar de forma definitiva el proyecto", 410);
                }
                break;
            case "eliminar":
                limpiar();
                mmoff("Suc","Proyecto eliminado", 160);
                break;
        }
        ocultarProcesando();
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
    }
}
function buscar() {
    try {
        $I("txtNumPE").value = $I("txtNumPE").value.ToString("N", 6, 0);

        var js_args = "buscar@#@" + dfn($I("txtNumPE").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}
function eliminarPE() {
    try {
        var tblDatos = $I("tblDatos");
        var js_args = "eliminar@#@";
        js_args += dfn($I("txtNumPE").value) + "@#@";
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (i == 0) js_args += tblDatos.rows[i].id;
            else js_args += "," + tblDatos.rows[i].id;
        }
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar el Proyecto Económico.", e.message);
    }
}
function limpiar() {
    try {
        $I("txtNumPE").value = "";
        $I("txtDesPE").value = "";
        $I("txtCliente").value = "";
        $I("imgEst").src = "../../../../Images/imgSeparador.gif";
        $I("imgCat").src = "../../../../Images/imgSeparador.gif";
        var aFila = FilasDe("tblDatos");
        for (var i = aFila.length - 1; i >= 0; i--) {
            $I("tblDatos").deleteRow(i);
        }
        AccionBotonera("eliminar", "D");
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar el Proyecto Económico.", e.message);
    }
}
