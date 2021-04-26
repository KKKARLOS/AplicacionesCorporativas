var bReasignacionRealizada = false;

function init() {
    try {
        if (!mostrarErrores()) return;
        
        switch ($I("hdnTipo").value) {
            case "E":
                $I("lblOrigen").innerText = "Entorno tecnológico origen";
                $I("lblDestino").innerText = "Busque y seleccione entorno tecnológico destino";
                $I("lblDestino2").innerText = "Entornos tecnológicos";
                break;
            case "T":
                $I("lblOrigen").innerText = "Titulación origen";
                $I("lblDestino").innerText = "Busque y seleccione titulación destino";
                $I("lblDestino2").innerText = "Titulaciones";
                break;
            case "C":
                $I("lblOrigen").innerText = "Cuenta origen";
                $I("lblDestino").innerText = "Busque y seleccione cuenta destino";
                $I("lblDestino2").innerText = "Cuentas"; //Clientes no Ibermática
                break;
        }

        if ($I("tblElementosAsociado") == null || $I("tblElementosAsociado").rows.length == 0) {
            setOp($I("btnProcesar"), 30);
            mmoff("War", "No se han detectado elementos a reasignar", 300);
        }
        $I("txtDen").focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function cerrarVentana() {
    try {
        if (bProcesando()) return;

        var returnValue = (bReasignacionRealizada)? "OK": null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function borrarCatalogo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}
function buscar() {
    try {
        borrarCatalogo();
        mostrarElementos();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function mostrarElementos() {
    try {
//        if ($I("txtDen").value == "") {
//            mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
//            $I("txtDen").focus();
//            return;
//        }

        var js_args = "buscar@#@" + $I("hdnTipo").value + "@#@" + Utilidades.escape($I("txtDen").value) + "@#@";
        js_args += getRadioButtonSelectedValue("rdbTipo", true);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        var reg = /\\n/g;
        setOp($I("btnProcesar"), 100);
        mostrarErrorSQL(aResul[3], aResul[2]);
//        ocultarProcesando();
//        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                //actualizarLupas("tblTitulo", "tblDatos");
                //scrollTabla();
                var aFila = FilasDe("tblDatos");
                if (aFila.length == 0) {
                    mmoff("War", "No existen elemento validados que cumplan el criterio indicado", 400);
                    $I("txtDen").focus();
                }
                else
                    window.focus();
                break;
            case "procesar":
                bCambios = false;
                bReasignacionRealizada = true;
                ocultarProcesando();
                setOp($I("btnProcesar"), 30);
                //mmoff("Proceso finalizado correctamente", 400, null, null, null, 100);
                mmoff("SucPer", "Proceso finalizado correctamente", 240);
                setTimeout("cerrarVentana();", 1000);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        ocultarProcesando();
    }
}
function procesar() {
    try {
        if (bProcesando()) return;

        if ($I("hdnOrigen").value == "") {
            mmoff("War", "Falta elemento origen", 200);
            return;
        }

        var aFila = ($I("hdnTipo").value == "E") ? FilasDe("tblDatos2") : FilasDe("tblDatos");
        if (aFila == null) {
            mmoff("War", "Falta elemento destino.", 200);
            return;
        }

        var sElementos = "";
        var bExit = false;
        for (var i = 0; i < aFila.length; i++) {
            switch ($I("hdnTipo").value) {
                case "C":
                    if (aFila[i].className != "FS") continue;
                    sElementos += aFila[i].id; //idDestino
                    sElementos += "{sep}" + ((aFila[i].hasAttribute("idSeg")) ? aFila[i].getAttribute("idSeg") : "-1"); //idSegmento para Cuentas CVT
                    sElementos += "{sep}" + Utilidades.escape(aFila[i].cells[0].children[0].innerHTML);  //Denominacion
                    bExit = true;
                    break;
                case "T":
                    if (aFila[i].className != "FS") continue;
                    sElementos += aFila[i].id; //idDestino
                    bExit = true;
                    break;
                default:
                    sElementos += aFila[i].id; //idDestino
                    sElementos += "{sepreg}";
                    break;
            }

            if (bExit) break;
        }

        if (sElementos == "") {
            mmoff("War", "Falta elemento destino a reasignar.", 280);
            return;
        }
        mostrarProcesando();
        var js_args = "procesar@#@" + $I("hdnTipo").value + "@#@" + $I("hdnOrigen").value + "@#@" + sElementos;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a procesar la reasignación", e.message);
    }
}

function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = $I("DW");
    //	var nIndiceInsert = null;
    var oTable, oNF;
    var js_ids;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        if (oTarget.id == "divCatalogo2"
	           || oTarget.id == "ctl00_CPHC_divCatalogo2") {
            oTable = oTarget.getElementsByTagName("TABLE")[0];
            js_ids = getIDsFromTable(oTable);
        }
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    if (js_ids.isInArray(oRow.id) == null) {
                        oRow.setAttribute("class", "");
                        oNF = oTable.insertRow(-1);
                        var oCloneNode = oRow.cloneNode(true);
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        oNF.swapNode(oCloneNode);
                    }
                    break;
            }
        }
    }
    oTable = null;
    killTimer();
    CancelDrag();

    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;

    setOp($I("btnProcesar"), 100);
}