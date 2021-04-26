var aFila;

function init() {
    try {
        if ($I("hdnMensajeError").value != "") {
            var reg = /\\n/g;
            var strMsg = $I("hdnMensajeError").value;
            strMsg = strMsg.replace(reg, "\n");
            mmoff("Inf", strMsg, 400);
            $I("hdnMensajeError").value = "";
        }
        aFila = FilasDe("tblDatos");
        actualizarLupas("tblTitulo", "tblDatos");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        var sError = aResul[2];
        mostrarError(sError.replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                actualizarLupas("tblTitulo", "tblDatos");
                desActivarGrabar();
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") != "") {
                        mfa(aFila[i], "N");
                    }
                }
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                break;
            case "catalogo":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                ocultarProcesando();
                break;
        }
    }
}

function grabar() {
    try {
        //if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        //if (!comprobarDatos()) return;
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                //sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].getAttribute("id") + "##"); //Profesional
                if (aFila[i].cells[2].children[0].checked) 
                    sb.Append("1"); 
                else
                    sb.Append("0");
                sb.Append("///");
                sw = 1;
            }
        }
        if (sw == 0) {
            mmoff("War", "No se han modificado los datos.", 230);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function marcardesmarcar(nOpcion) {
    try {
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].cells[2].children[0].checked && nOpcion == 0) {
                aFila[i].cells[2].children[0].checked = false;
                mfa(aFila[i], "U");
            }
            else {
                if (!aFila[i].cells[2].children[0].checked && nOpcion == 1) {
                    aFila[i].cells[2].children[0].checked = true;
                    mfa(aFila[i], "U");
                }
            }
        }
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}
function setFila(oFila) {
    mfa(oFila, "U");
}