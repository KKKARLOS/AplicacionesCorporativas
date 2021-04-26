function salir() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);
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
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        insertarItem(oRow);
                        if (nIndiceInsert != null) {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            //oTable.moveRow(oTable.rows.length, nIndiceInsert);
                        }
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

function insertarItem(oFila) {

    try {
        var idItem = oFila.id;
        var bExiste = false;
        var tblDatos = $I("tblDatos");
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].id == idItem) {
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

        var sNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < tblDatos.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = tblDatos.rows[iFilaNueva].innerText;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var NewRow = tblDatos.insertRow(iFilaNueva);

        var oCloneNode = oFila.cloneNode(true);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        NewRow.swapNode(oCloneNode);
//        scrollTablaConoAsig();
//        actualizarLupas("tblAsignados", "tblDatos");

        return iFilaNueva;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}

function aceptar() {
    try{
        var aFilas = FilasDe("tblDatos");
        if (aFilas.length != 0) {
            var idAreas = "";
            var descAreas = "";
            for (var i = 0; i < aFilas.length; i++) {
                idAreas += aFilas[i].id + ",";
                descAreas += aFilas[i].cells[1].innerText + ", ";
            }
            var returnValue = {
                desc: descAreas.substring(0, descAreas.length - 2),
                id: idAreas.substring(0, idAreas.length - 1)
            };
            //setTimeout("window.close();", 250);
            modalDialog.Close(window, returnValue);
        }
        else
            mmoff("Err", "La experiencia profesional debe de tener asociado al menos un Area de Conocimiento", 400);
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar conocimientos", e.message);
    }
}