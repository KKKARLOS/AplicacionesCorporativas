var bSaliendo = false;

function init() {
    if (!mostrarErrores()) {
        return;
    }
    ocultarProcesando();
}
function unload() {
    if (!bSaliendo) salir();
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    var returnValue = null;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabarSalir();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function grabarSalir() {
    bSalir = true;
    grabar();
}
function grabar() {
    try {
        var js_args = "grabar@#@";

        var sb = new StringBuilder;
        for (var i = 0; i < $I("tblDatos2").rows.length; i++) {
            if ($I("tblDatos2").rows[i].getAttribute("bd") != "D") {
                if ($I("tblDatos2").rows[i].id != null) {
                    sb.Append($I("tblDatos2").rows[i].id);
                    sb.Append(",");
                }
            }
        }
        js_args += sb.ToString();

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de las figuras", e.message);
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
            case "grabar":
                aFila = FilasDe("tblDatos2");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos2").deleteRow(i);
                        continue;
                    }
                    mfa(aFila[i], "N");
                }
                bCambios = false;
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                setTimeout("salir();", 1000);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
        }
    }
}

function addItem(oNOBR) {
    try {
        insertarItem(oNOBR.parentNode.parentNode);
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la figura", e.message);
    }
}
function insertarItem(oFila) {
    try {
        var idItem = oFila.id;
        var bExiste = false;

        for (var i = 0; i < $I("tblDatos2").rows.length; i++) {
            if ($I("tblDatos2").rows[i].id == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            //alert("El item indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;


        var oTable = $I("tblDatos2");
        var sNuevo = (ie) ? oFila.innerText : oFila.textContent;
        for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = oTable.rows[iFilaNueva].innerText;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var oNF = $I("tblDatos2").insertRow(iFilaNueva);

        oNF.setAttribute("id", oFila.getAttribute("id"));
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:20px");

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true));

        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true), null);
        oNF.insertCell(-1).appendChild(oFila.cells[1].cloneNode(true), null);

        bCambios = true;

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una figura", e.message);
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

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
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
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        var oNF;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;

                        oNF.setAttribute("id", oRow.getAttribute("id"));
                        oNF.setAttribute("bd", "I");
                        oNF.setAttribute("style", "height:20px");

                        oNF.attachEvent('onclick', mm);
                        oNF.attachEvent('onmousedown', DD);

                        oNC1 = oNF.insertCell(-1);
                        oNC1.appendChild(oImgFI.cloneNode(true));

                        oNF.insertCell(-1).appendChild(oRow.cells[0].children[0].cloneNode(true), null);
                        oNF.insertCell(-1).appendChild(oRow.cells[1].cloneNode(true), null);

                        bCambios = true;
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
}
