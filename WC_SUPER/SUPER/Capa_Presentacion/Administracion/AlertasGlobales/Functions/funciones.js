
function init() {
    try {
        if (!mostrarErrores()) return;
        $I("hdnTabla").value = $I("divCatalogo").children[0].innerHTML;
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir() {
    bSalir = false;
    var returnValue = null;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas procesarlos?", "", "", "war",330).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);           
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                bCambios = false;
                aFila = FilasDe("tblDatos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "U") {
                        mfa(aFila[i], "N");
                        continue;
                    }
                }
                ocultarProcesando();
                desActivarGrabar();
                $I("divCatalogo").children[0].innerHTML = $I("hdnTabla").value;
                
                mmoff("Suc", "Proceso finalizado correctamente.", 230);
                if (bSalir) setTimeout("salir();", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function grabar() {
    try {
        if (getOp($I("btnGrabar")) != 100) return;    
        
        aFila = FilasDe("tblDatos");
      
        var sb = new StringBuilder; //sin paréntesis 
        sb.Append("grabar@#@");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                //sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id + "##");
                sb.Append(aFila[i].cells[2].children[0].value + "##");
                sb.Append(aFila[i].cells[3].children[0].value + "##");
                sb.Append("///");
            }
        }

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
function aG(obj) {
    mfa(obj.parentNode.parentNode, 'U');
    setOp($I('btnGrabar'), 100);
    bCambios = true;
}