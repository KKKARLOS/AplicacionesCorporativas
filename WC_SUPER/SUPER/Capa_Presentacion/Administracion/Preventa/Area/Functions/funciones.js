var bHayCambios = false;
var bCrearNuevo = false;

function init() {
    try {
        if (!mostrarErrores()) return;

        iniciarPestanas();
        if (sOrigen == "MantFiguras") {
            //$I("tsPestanas").pulPes(1);
            getDatos("1");
            $I("tblBotones").rows[0].deleteCell(1);
            initDragDropScript();
        }
        if ($I("txtID").value != "") {
            $I("lblCodigo").style.visibility = "visible";
            $I("txtID").style.visibility = "visible";
        }
        else
            $I("chkActivo").checked = true;

        //switch (nNivel) {
        //    case 1: $I("imgNivel").src = "../../../../images/Unidad.gif"; break;
        //    case 2: $I("imgNivel").src = "../../../../images/Area.gif"; break;
        //    case 3: $I("imgNivel").src = "../../../../images/Subarea.gif"; break;
        //}

        if (sOrigen == "MantFiguras") $I("txtApellido1").focus();
        else $I("txtDenominacion").focus();

        sDesOld = $I("txtDenominacion").value;
        sEstOld = $I("chkActivo").checked;

        $I("hdnDenominacion").value = $I("txtDenominacion").value;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload() {

}
function grabarSalir() {
    bSalir = true;
    grabar();
}
function grabarAux() {
    bSalir = false;
    grabar();
}

function salir() {
    var returnValue = bHayCambios + "///" + $I("hdnDenominacion").value + "///" + $I("chkActivo").checked + "///" + $I("txtID").value;
    var sMsg = "Datos modificados. ¿Deseas grabarlos?";
    var iCaso1 = 0;
    var iIndice1 = 0;
    var iCaso2 = 0;
    var iIndice2 = 0;

    if (aPestGral[1].bModif) {
        //Control de las figuras
        for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {

                    iCaso1 = 1;
                    iIndice1 = i;
                    //break;
                    $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                }
            }
        }
    }
    if (iCaso1 == 1 || iCaso2 == 2) {
        sMsg = "Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar con la grabación?";

        if (iCaso1 == 1) {
            tsPestanas.setSelectedIndex(1);
            ms($I("tblFiguras2").rows[iIndice1]);
        }

        jqConfirm("", sMsg, "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                if (!comprobarDatos()) return;
                grabar2();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else if (bCambios) {
        jqConfirm("", sMsg, "", "", "war", 320).then(function (answer) {
            if (answer) {
                if (!comprobarDatos()) return;
                bSalir = true;
                grabar2();
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
        ocultarProcesando();
        var reg = /\\n/g;
        var sError = aResul[2];
        if (aResul[1] == "AVISO") {
            mmoff("Inf", "El proyecto técnico no existe o está fuera de tu ámbito de visión.", 390);
        }
        else if (aResul[1] == "AVISO2") {
            mmoff("War", sError, 390);
        }
        else {
            //mostrarError(sError.replace(reg, "\n"));
            mostrarErrorSQL(aResul[3], aResul[2]);
        }

    } else {
        switch (aResul[0]) {
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                ocultarProcesando();
                break;

            case "tecnicos":
                $I("divFiguras1").children[0].innerHTML = aResul[2];
                $I("divFiguras1").scrollTop = 0;
                nTopScroll = 0;
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                scrollTabla();
                actualizarLupas("tblTituloFiguras1", "tblFiguras1");
                ocultarProcesando();

                break;
            case "grabar":
                bCambios = false;
                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);

                $I("txtID").value = aResul[2];

                if (aPestGral[1].bModif == true) {
                    for (var i = $I("tblFiguras2").rows.length - 1; i >= 0; i--) {
                        if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D") {
                            $I("tblFiguras2").deleteRow(i);
                        } else if ($I("tblFiguras2").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblFiguras2").rows[i], "N");
                        }
                    }
                    recargarArrayFiguras();
                }

                $I("hdnDenominacion").value = $I("txtDenominacion").value;
                reIniciarPestanas();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");

                if (bCrearNuevo) {
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
                }

                if (bSalir) setTimeout("salir();");
                break;
            case "buscarPE":
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);
                }
                else {
                    limpiar();
                    $I("txtNumPE").value = $I("txtNumPE").value.ToString("N",7,0);
                    $I("hdnT305IdProy").value = aResul[2];
                    $I("txtPE").value = aResul[3];
                    
                }
                break;
            case "buscarPT":
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto técnico no existe o está fuera de tu ámbito de visión.", 360);
                }
                else {
                    limpiar();
                    $I("hdnIDPT").value = dfn($I("txtNumPT").value);
                    $I("txtNumPT").value = $I("txtNumPT").value.ToString("N", 7, 0);
                    $I("hdnT305IdProy").value = aResul[2];
                    $I("txtNumPE").value = aResul[3].ToString("N", 7, 0);
                    $I("txtPE").value = aResul[4];
                    $I("txtPT").value = aResul[5];
                }
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function recargarArrayFiguras() {
    try {
        aFigIni = new Array();
        for (var i = $I("tblFiguras2").rows.length - 1; i >= 0; i--) {
            aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI");
            for (var x = 0; x < aLIs.length; x++) {
                insertarFiguraEnArray($I("tblFiguras2").rows[i].id, aLIs[x].id)
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al recargarArrayFiguras", e.message);
    }
}
function objFigura(idUser, sFig) {
    this.idUser = idUser;
    this.sFig = sFig;
}
function insertarFiguraEnArray(idUser, sFig) {
    try {
        oFIG = new objFigura(idUser, sFig);
        aFigIni[aFigIni.length] = oFIG;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar un figura en el array.", e.message);
    }
}
function comprobarDatos() {
    try {
        if ($I("hdnIdUnidad").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La Unidad es dato obligatorio.", 320);
            return false;
        }
        //if ($I("hdnIdArea").value == "") {
        //    tsPestanas.setSelectedIndex(0);
        //    mmoff("War", "El área es dato obligatorio.", 320);
        //    return false;
        //}
        if ($I("txtDenominacion").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La denominación es dato obligatorio", 250);
            return false;
        }
        if ($I("hdnIDResponsable").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "El responsable es dato obligatorio", 250);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar() {
    try {
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var iCaso1 = 0;
        var iCaso2 = 0;
        var iIndice1 = 0;
        var iIndice2 = 0;
        var bAviso = false;

        if ($I("hdnT305IdProy").value != "" && $I("hdnIDPT").value == "") {
            //tsPestanas.setSelectedIndex(0);
            //mmoff("War", "Has indicado Proyecto Económico pero no Proyecto Técnico", 250);
            bAviso=true;
        }


        if (aPestGral[1].bModif) {
            //Control de las figuras
            for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                    var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                    if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                        iCaso1 = 1;
                        iIndice1 = i;
                        $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                    }
                }
            }
        }

        if (iCaso1 == 1 || iCaso2 == 2) {
            if (iCaso1 == 1) {
                tsPestanas.setSelectedIndex(1);
                ms($I("tblFiguras2").rows[iIndice1]);
            }
            if (iCaso1 == 1 || iCaso2 == 2) {
                jqConfirm("", "¡ Atención !<br><br>Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar?", "", "", "war").then(function (answer) {
                    if (answer) {
                        if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                        LLamadaGrabar();
                    }
                    else return;
                });
            }
        }
        else {
            if (bAviso) {
                jqConfirm("", "¡ Atención !<br><br>Has indicado Proyecto Económico pero no Proyecto Técnico.<br><br>¿Deseas continuar?", "", "", "war").then(function (answer) {
                    if (answer) LLamadaGrabar();
                    else return;
                });
            }
            else
                LLamadaGrabar();
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}
function grabar2() {
    try {
        LLamadaGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar(2).", e.message);
    }
}
function LLamadaGrabar() {
    try {
        var js_args = "grabar@#@";
        js_args += grabarP0();//datos generales
        js_args += "@#@";
        js_args += grabarP1();//figuras

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar(3).", e.message);
    }
}

function grabarP0() {
    var sb = new StringBuilder;
    bHayCambios = true;

    sb.Append($I("txtID").value + "##"); //0
    //sb.Append(Utilidades.escape($I("txtDenominacion").value) + "##"); //1
    sb.Append($I("txtDenominacion").value + "##"); //1
    sb.Append($I("hdnIDResponsable").value + "##"); //2
    sb.Append(($I("chkActivo").checked == true) ? "1##" : "0##"); //3
    sb.Append($I("hdnIdUnidad").value + "##");  //4
    sb.Append($I("hdnIDPT").value);  //5

    return sb.ToString();
}
function grabarP1() {
    var sb = new StringBuilder;
    if (aPestGral[1].bModif) {
        //Control de las figuras
        for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
            bGrabar = false;
            sbFilaAct = new StringBuilder;
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != "") {
                sbFilaAct.Append($I("tblFiguras2").rows[i].getAttribute("bd") + "##" + $I("tblFiguras2").rows[i].id + "##");
                if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D") {
                    //Si voy a borrar un profesional no tiene sentido hacer nada con sus figuras pues haremos delete por profesional
                    bGrabar = true;
                    //borrarUserDeArray($I("tblFiguras2").rows[i].id);
                    sbFilaAct.Append("D@");
                }
                else {
                    aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                    //Recorro la lista de figuras originales para ver que deletes hay que pasar
                    for (var nIndice = 0; nIndice < aFigIni.length; nIndice++) {
                        if (aFigIni[nIndice].idUser == $I("tblFiguras2").rows[i].id) {
                            if (!estaEnLista(aFigIni[nIndice].sFig, aLIs)) {
                                sbFilaAct.Append("D@" + aFigIni[nIndice].sFig + ",");
                                bGrabar = true;
                            }
                        }
                    }
                    //Recorro la lista actual de figuras para ver que inserts hay que pasar
                    for (var x = 0; x < aLIs.length; x++) {
                        if (!estaEnLista2($I("tblFiguras2").rows[i].id, aLIs[x].id, aFigIni)) {
                            sbFilaAct.Append("I@" + aLIs[x].id + ",");
                            bGrabar = true;
                        }
                    }
                }
                if (bGrabar) {
                    sbFilaAct.Append("///");
                    sb.Append(sbFilaAct.ToString());
                }
            }
        }
    }
    return sb.ToString();
}
function nuevo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                    return;
                }
                else bCambios = false;
                fOpener().insertarItem(nNivel);
                modalDialog.Close(window, null);
                return;
            });
        }
        else {
            fOpener().insertarItem(nNivel);
            modalDialog.Close(window, null);
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
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
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
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
                case "imgPapeleraFiguras":
                    aG(1);
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    }
                    break;
                case "divFiguras2":
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
                            // Se inserta la fila
                            var oNF;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;

                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "22px";
                            oNF.setAttribute("style", "height:22px");

                            oNF.id = oRow.id;

                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);

                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
                            //
                            if (oRow.getAttribute("sexo") == "V") {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            } else {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEM.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPM.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFM.cloneNode(true), null); break;
                                }
                            }

                            oNC2 = oNF.insertCell(-1).appendChild(oRow.cells[1].children[0].cloneNode(true), null);
                            oNC2.ondblclick = null;
                            oNC2.style.width = "275px";
                            oNC2.className = "NBR W275";
                            oNC2.style.verticalAlign = "bottom";
                            //oNC2.innerText=oRow.cells[1].innerText;

                            var oCtrl2 = document.createElement("div");
                            var oCtrl3 = document.createElement("ul");
                            oCtrl3.setAttribute("id", "box-" + oRow.id);
                            oCtrl2.appendChild(oCtrl3);
                            oNF.insertCell(-1).appendChild(oCtrl2);

                            aG(1);
                            initDragDropScript();
                        }
                    }
                    break;
            }
        }
        actualizarLupas("tblTituloFiguras2", "tblFiguras2");
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
    ocultarProcesando();
}

function insertarFigura(oFila) {
    try {
        // Se inserta la fila
        for (var x = 0; x < $I("tblFiguras2").rows.length; x++) {
            if ($I("tblFiguras2").rows[x].cells[2].innerText == oFila.cells[1].innerText) {
                //alert("Profesional ya incluido");
                return;
            }
        }

        var oNF = $I("tblFiguras2").insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:22px");
        oNF.style.height = "22px";
        oNF.id = oFila.id;

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        if (oFila.getAttribute("sexo") == "V") {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
            }
        } else {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNF.insertCell(-1).appendChild(oImgEM.cloneNode(true), null); break;
                case "P": oNF.insertCell(-1).appendChild(oImgPM.cloneNode(true), null); break;
                case "F": oNF.insertCell(-1).appendChild(oImgFM.cloneNode(true), null); break;
            }
        }

        oNC2 = oNF.insertCell(-1).appendChild(oFila.cells[1].children[0].cloneNode(true), null);
        oNC2.ondblclick = null;
        oNC2.style.width = "275px";
        oNC2.className = "NBR W275";
        oNC2.style.verticalAlign = "bottom";
        //oNC2.innerText=oFila.cells[1].innerText;

        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNF.insertCell(-1).appendChild(oCtrl2);

        aG(1);
        initDragDropScript();

        actualizarLupas("tblTituloFiguras2", "tblFiguras2");
        $I("divFiguras2").scrollTop = $I("tblFiguras2").rows[$I("tblFiguras2").rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura", e.message);
    }
}
function getResponsable() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDResponsable").value = aDatos[2];//Cojo el IdFicepi y no el IdSuper
                    $I("txtDesResponsable").value = aDatos[1];
                    aG(0);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function getItemEstructura() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getItemEstructura.aspx?nNivel=21&nIDPadre=-1";
        modalDialog.Show(strEnlace, self, sSize(450, 480))
            .then(function (ret) {
                if (ret != null) {
                    bHayCambios = true;
                    var aDatos = ret.split("@#@");
                    $I("hdnIdUnidad").value = aDatos[0];
                    $I("txtUnidad").value = aDatos[1];
                    aG(0);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los elementos de estructura", e.message);
    }
}

//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
var aPestGral = new Array();
function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        ocultarIncompatibilidades();

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount() ; i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount() ; i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}

function getDatos(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana + "@#@";
        js_args += nNivel + "@#@";
        js_args += ($I("txtID").value == "") ? "-1" : $I("txtID").value;

        RealizarCallBack(js_args, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}
function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0":
                //no hago nada
                break;
            case "1": //Figuras
                $I("divFiguras2").children[0].innerHTML = aResul[0];
                initDragDropScript();
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                eval(aResul[1]);
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function aG(iPestana) {//Sustituye a activarGrabar
    try {
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);

        aPestGral[iPestana].bModif = true;

        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
    }
}

function comprobarIncompatibilidades(oNuevo, aLista) {
    try {
        //1º Comprueba las incompatibilidades
        for (var i = 0; i < aLista.length; i++) {
            if ((oNuevo.id == "D" || oNuevo.id == "I" || oNuevo.id == "C")
                    &&
                 (aLista[i].id == "D" || aLista[i].id == "I" || aLista[i].id == "C")) {

                //mmoff("War","Figura no insertada por incompatibilidad.", 260, null, null, 550, 200);
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000);
                $I("divBoxeo").style.visibility = "visible";
                return false;
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras de área de preventa.", e.message);
    }
}


function mostrarIncompatibilidades() {
    try {
        $I("divBoxeo").style.visibility = "hidden";
        $I("divIncompatibilidades").style.visibility = "visible";
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las incompatibilidades.", e.message);
    }
}
function ocultarIncompatibilidades() {
    try {
        $I("divIncompatibilidades").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar las incompatibilidades.", e.message);
    }
}

function getProfesionalesFigura() {
    try {
        //alert(strInicial);
        if (bLectura) return;
        var sAp1 = Utilidades.escape($I("txtApellido1").value);
        var sAp2 = Utilidades.escape($I("txtApellido2").value);
        var sNombre = Utilidades.escape($I("txtNombre").value);

        if (sAp1 == "" && sAp2 == "" && sNombre == "") return;
        mostrarProcesando();

        var js_args = "tecnicos@#@";
        js_args += sAp1 + "@#@";
        js_args += sAp2 + "@#@";
        js_args += sNombre;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales", e.message);
    }
}

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divFiguras1").scrollTop != nTopScroll) {
            nTopScroll = $I("divFiguras1").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divFiguras1").offsetHeight / 20 + 1, $I("tblFiguras1").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblFiguras1").rows[i].getAttribute("sw")) {
                oFila = $I("tblFiguras1").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.ondblclick = function () { insertarFigura(this) };
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function estaEnLista(sElem, slLista) {
    try {
        var bRes = false;
        for (var i = 0; i < slLista.length; i++) {
            if (sElem == slLista[i].id) {
                bRes = true;
                break;
            }
        }
        return bRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
}
function estaEnLista2(sUser, sFig, slLista) {
    try {
        var bRes = false;
        for (var i = 0; i < slLista.length; i++) {
            if (sUser == slLista[i].idUser && sFig == slLista[i].sFig) {
                bRes = true;
                break;
            }
        }
        return bRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
}

function getAuditoriaAux() {
    try {
        if ($I("txtID").value == "") return;
        switch (nNivel) {
            case 1: getAuditoria(15, $I("txtID").value); break;
            case 2: getAuditoria(14, $I("txtID").value); break;
            case 3: getAuditoria(13, $I("txtID").value); break;
            case 4: getAuditoria(12, $I("txtID").value); break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}

function obtenerProyectos() {
    try {
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge"; 
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    limpiar();
                    $I("hdnT305IdProy").value = aDatos[0];
                    $I("txtNumPE").value = aDatos[3];
                    $I("txtPE").value = aDatos[4];
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function obtenerPTs() {
    try {
        var aOpciones, idPE, sPE, idPT, strEnlace, sAncho, sAlto;

        idPE = $I("txtNumPE").value;
        sPE = $I("txtPE").value;
        var sTamano;

        if (idPE == "" || idPE == "0") {
            strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT2.aspx";
            sTamano = sSize(820, 650);
        }
        else {
            strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPSN=" + codpar($I("hdnT305IdProy").value) + "&nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE);
            sTamano = sSize(500, 580);
        }
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function (ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    idPT = aOpciones[0];
                    if ($I("hdnIDPT").value != idPT) {
                        if (idPE == "" || idPE == "0") {
                            $I("txtNumPE").value = aOpciones[2].ToString("N", 9, 0);
                            $I("txtPE").value = aOpciones[3];
                            $I("hdnT305IdProy").value = aOpciones[4];
                        }
                        $I("hdnIDPT").value = idPT;
                        $I("txtPT").value = aOpciones[1];
                        $I("txtNumPT").value = idPT.ToString("N", 9, 0);
                        aG(0);
                    }
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}
function borrarPT() {
    try {
        if ($I("txtPT").value != "") {
            $I("txtPT").value = "";
            $I("hdnIDPT").value = "";
            $I("txtNumPT").value = "";
            aG(0);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el PT", e.message);
    }
}
function buscarPE() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
function limpiar() {
    $I("hdnT305IdProy").value = "";
    $I("txtPE").value = "";
    borrarPT();
}
function buscarPT() {
    try {
        $I("txtNumPT").value = dfnTotal($I("txtNumPT").value).ToString("N", 9, 0);
        var js_args = "buscarPT@#@";
        js_args += dfn($I("txtNumPT").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar el PT.", e.message);
    }
}
