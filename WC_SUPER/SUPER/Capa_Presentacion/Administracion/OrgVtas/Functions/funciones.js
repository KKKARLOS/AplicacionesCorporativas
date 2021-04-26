function init() {
    actualizarLupas("tblCatIni", "tblDatos");
    actualizarLupas("tblAsignados", "tblDatos2");
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
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                aFila = FilasDe("tblDatos2");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos2").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].id = aEI[nIndiceEI];
                        nIndiceEI++;
                    }
                    mfa(aFila[i], "N");
                }
                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar() {
    try {
        var js_args = "grabar@#@";

        var sb = new StringBuilder;

        for (var i = 0; i < $I("tblDatos2").rows.length; i++) {
            if ($I("tblDatos2").rows[i].getAttribute("bd") != "") {
                sb.Append($I("tblDatos2").rows[i].getAttribute("bd") + "##"); //0
                sb.Append($I("tblDatos2").rows[i].id + "##"); //1
                sb.Append("///");
            }
        }
        js_args += sb.ToString();

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la organización de ventas", e.message);
    }
}

function addItem(oFila) {
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

//        var oTable = $I("tblDatos2");
//        var sNuevo = (ie) ? oFila.innerText : oFila.textContent;
//        for (iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
//            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
//            var sActual = (ie) ? oTable.rows[iFilaNueva].innerText : oTable.rows[iFilaNueva].textContent;
//            if (sActual > sNuevo) break;
//        }
        
        var oTable = $I("tblDatos2");
        var sNuevo = oFila.cells[1].innerText.Trim().toUpperCase();
        for (iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = oTable.rows[iFilaNueva].children[2].innerText.toUpperCase();
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var oNF = $I("tblDatos2").insertRow(iFilaNueva);
        //var oNF = tblDatos2.insertRow();
        oNF.setAttribute("bd","I");
        oNF.style.height = "20px";
        oNF.id = oFila.id;
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
       
        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true));
        //oNC1.appendChild(oImgFI.cloneNode(true), null);
//        oNC3 = oNF.insertCell();
//        oNC3.innerHTML = "<nobr class='NBR' style='width:435px'>" + oFila.cells[0].innerText + "</nobr>";
        oNC2 = oNF.insertCell(-1);
        oNC2.innerText = oFila.cells[0].innerText.Trim();

        oNC3 = oNF.insertCell(-1);
        //oNF.cells[1].appendChild(oFila.cells[0].cloneNode(true), null);
        
        var oCtrl1 = document.createElement("div");
        oCtrl1.setAttribute("style", "width:355px");
        oCtrl1.className="NBR";
        oCtrl1.appendChild(document.createTextNode(oFila.cells[1].innerText.Trim()));
        oNC3.appendChild(oCtrl1);
                        
        activarGrabar();
        actualizarLupas("tblAsignados", "tblDatos2");

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar un profesional", e.message);
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
//                        var NewRow;
//                        if (nIndiceInsert == null) {
//                            nIndiceInsert = oTable.rows.length;
//                            oNF = oTable.insertRow(nIndiceInsert);
//                        }
//                        else {
//                            if (nIndiceInsert > oTable.rows.length)
//                                nIndiceInsert = oTable.rows.length;
//                            oNF = oTable.insertRow(nIndiceInsert);
//                        }
//                        nIndiceInsert++;

                        var iFilaNueva = 0;
                        var sNombreNuevo, sNombreAct;

                        var oTable = $I("tblDatos2");
                        var sNuevo = oRow.cells[1].innerText.Trim().toLowerCase();
                        for (iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
                            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                            var sActual = oTable.rows[iFilaNueva].children[2].innerText.toLowerCase();
                            if (sActual > sNuevo) break;
                        }

                        var oNF = oTable.insertRow(iFilaNueva);

                        oNF.setAttribute("bd", "I");
                        oNF.style.height = "20px";
                        oNF.id = oRow.id;
                        oNF.attachEvent('onclick', mm);
                        oNF.attachEvent('onmousedown', DD);

                        oNC1 = oNF.insertCell(-1);
                        oNC1.appendChild(oImgFI.cloneNode(true));

                        oNC2 = oNF.insertCell(-1);
                        oNC2.innerText = oRow.cells[0].innerText.Trim();
                        
                        oNC3 = oNF.insertCell(-1);
                        //oNF.cells[1].appendChild(oRow.cells[0].cloneNode(true), null);

                        //oNC3.innerHTML = "<nobr class='NBR' style='width:435px'>" + oRow.cells[0].innerText + "</nobr>";
                        
                        var oCtrl1 = document.createElement("div");
                        oCtrl1.setAttribute("style", "width:365px");
                        oCtrl1.setAttribute("className", "NBR");
                        //oCtrl1.setAttribute("class", "NBR");
                        oCtrl1.appendChild(document.createTextNode(oRow.cells[1].innerText.Trim()));
                        oNC3.appendChild(oCtrl1);                        
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        activarGrabar();
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