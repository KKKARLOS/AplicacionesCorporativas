function init() {
    try {
        if (!mostrarErrores()) return;
        $I("cboTipoItem").focus();
        cargarCriteriosSeleccionados();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarAux() {
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar() {
    try {
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            sb.Append(tblDatos2.rows[i].id + "@#@");
            sb.Append(tblDatos2.rows[i].cells[0].children[0].innerHTML);
            sb.Append("///");
            sw = 1;
        }

        if (sw == 0) {
            ocultarProcesando();
            mmoff("Inf","Debes seleccionar algún item",210);
            return;
        }
        bCambios = false;
        var returnValue = sb.ToString().substring(0, sb.ToString().length - 3);
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana() {
    try {
        if (bProcesando()) return;
        bCambios = false;
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function insertarItem(oFila) {

    try {
        var idItem = oFila.id;
        var bExiste = false;

        var oTable = $I("tblDatos2");

        for (var i = 0; i < oTable.rows.length; i++) {
            if (oTable.rows[i].id == idItem) {
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
        for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = oTable.rows[iFilaNueva].innerText;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var NewRow;

        oNF = $I("tblDatos2").insertRow(iFilaNueva);

        oNF.id = oFila.id;
        oNF.style.height = "16px";
        oNF.insertCell(-1);

        var oCtrl1 = document.createElement("nobr");
        oCtrl1.className = "NBR";        
        oNF.cells[0].appendChild(oCtrl1);
        
        oNF.cells[0].children[0].innerHTML = Utilidades.unescape(oFila.getAttribute("des"));

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
        actualizarLupas("tblAsignados", "tblDatos2");

        return iFilaNueva;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}

function InsertaFila(id, sTexto) {
    var NewRow;
    oNF = $I("tblDatos").insertRow($I("tblDatos").length);
    oNF.id = id;
    oNF.setAttribute("des", sTexto + " de " + $I("cboTipoItem").options[$I("cboTipoItem").selectedIndex].innerText);
    oNF.style.height = "16px";
    oNF.insertCell(-1);
    var oCtrl1 = document.createElement("nobr");
    oCtrl1.className = "NBR";
    oNF.cells[0].appendChild(oCtrl1);
        
    oNF.cells[0].children[0].innerHTML = Utilidades.unescape(sTexto);
    oNF.attachEvent('onclick', mm);
    oNF.attachEvent('onmousedown', DD);
    oNF.ondblclick = function() { insertarItem(this) };

}
function setFigura(sItem) {
    try {

        BorrarFilasDe("tblDatos");
        var NewRow;

        switch (sItem) {
            case "1":
            case "2":
            case "3":
            case "4":
            case "6":
                InsertaFila('S#' + sItem, 'Asistente');
                InsertaFila('I#' + sItem, 'Invitado');
                InsertaFila('G#' + sItem, 'Gestor');
                InsertaFila('D#' + sItem, 'Delegado');
                InsertaFila('R#' + sItem, 'Responsable');
                break;

            case "5":
                InsertaFila('P#' + sItem, "RIA");
                InsertaFila('S#' + sItem, 'Asistente');
                InsertaFila('I#' + sItem, 'Invitado');
                InsertaFila('G#' + sItem, 'Gestor');
                InsertaFila('C#' + sItem, 'Colaborador');
                InsertaFila('D#' + sItem, 'Delegado');
                InsertaFila('R#' + sItem, 'Responsable');
                break;

            case "7":
                InsertaFila('K#' + sItem, 'RTPT');
                InsertaFila('B#' + sItem, 'Bitacórico');
                InsertaFila('S#' + sItem, 'Asistente');
                InsertaFila('M#' + sItem, 'RTPE');
                InsertaFila('J#' + sItem, 'Jefe');
                InsertaFila('I#' + sItem, 'Invitado');
                InsertaFila('C#' + sItem, 'Colaborador');
                InsertaFila('D#' + sItem, 'Delegado');
                InsertaFila('T#' + sItem, 'SAA');
                InsertaFila('L#' + sItem, 'SAT');
                InsertaFila('R#' + sItem, 'Responsable');
                break;

            case "8":
            case "9":
            case "10":
            case "13":
            case "14":
            case "15":
            case "16":
            case "17":
                InsertaFila('I#' + sItem, 'Invitado');
                InsertaFila('D#' + sItem, 'Delegado');
                InsertaFila('R#' + sItem, 'Responsable');
                break;

            case "11":
                InsertaFila('OT#' + sItem, 'Miembro');
                break;

            case "12":
                InsertaFila('RG#' + sItem, 'Responsable');
                break;
        }
        //setCombo(1);

    } catch (e) {
        mostrarErrorAplicacion("Error en la función setFigura ", e.message);
    }
}
function fnRelease(e) {
    //alert('entra fnRelease');
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;
   
    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

    var obj = $I("DW");

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
                        aG(1);
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    }
                    else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                     break;

                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
                            if (oTable.rows[i].id == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            var NewRow;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                NewRow = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length)
                                    nIndiceInsert = oTable.rows.length;
                                NewRow = oTable.insertRow(nIndiceInsert);
                            }

                            nIndiceInsert++;
                            var oCloneNode = oRow.cloneNode(true);
                            oCloneNode.className = "";
                            NewRow.swapNode(oCloneNode);

                            //Se marca la fila como insertada
                            oCloneNode.insertCell(0);
                            oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true), null);
                            mfa(oCloneNode, "I");

                            activarGrabar();
                        }
                    } else if (nOpcionDD == 2) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            if (oTable.rows[i].id == oRow.id) {
                                //alert("Persona ya incluida");
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 0) {
                            //Cambio por indicación de victor 10/03/2008. Cuando drag&drop, se insertan donde se se han soltado.
                            //Si queremos reordenar, poner flechas
                            var NewRow;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                NewRow = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length)
                                    nIndiceInsert = oTable.rows.length;
                                NewRow = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;
                            var oCloneNode = oRow.cloneNode(true);
                            oCloneNode.className = "MM";
                            NewRow.swapNode(oCloneNode);
                            oCloneNode.cells[0].children[0].innerText = oRow.getAttribute("des");
                            oCloneNode.attachEvent('onclick', mm);
                            oCloneNode.attachEvent('onmousedown', DD);

                            activarGrabar();
                        }
                    }
                    break;
            }
        }
        switch (oTarget.id) {
            case "divAsignados":
            case "ctl00_CPHC_divAsignados":
                actualizarLupas("tblAsignados", "tblDatos2");
                break;
            case "imgPapelera":
            case "ctl00_CPHC_imgPapelera":
                if (nOpcionDD == 3) {
                    if (oRow.getAttribute("bd") == "I") {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                    }
                } else if (nOpcionDD == 4) {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                }
                break;
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
function cargarCriteriosSeleccionados() {
    try {
        var sb = new StringBuilder;
        var aDatos;
        var sw = 0;
        for (var i = 0; i < fOpener().js_Valores.length; i++) {
            aDatos = fOpener().js_Valores[i].split("##");
            if (aDatos[0] != "") {
                sb.Append("<tr id='" + aDatos[0] + "' style='height:16px;' onmouseover='TTip(event)' ");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)'>");
                sb.Append("<td><nobr class='NBR W340'>" + Utilidades.unescape(aDatos[1]) + "</nobr></td></tr>");
                sw = 1;
            }
        }
        if (sw == 1) {
            insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
            actualizarLupas("tblAsignados", "tblDatos2");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
 	