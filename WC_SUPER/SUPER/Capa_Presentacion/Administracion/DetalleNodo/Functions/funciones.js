
var bHayCambios = false;
var bCrearNuevo = false;
var nAnoMesActual, nAnoMesActualIAP;
var bDuplicando = false;

function init() {
    try {

        if (!mostrarErrores()) return;

        iniciarPestanas();
        if (sOrigen == "MantFiguras") {
            //$I("tsPestanas").pulPes(1);
            getDatos("1");
            $I("tblBotones").rows[0].deleteCell(0);
            initDragDropScript();
        }

        $I("lblSN4").innerText = sSN4;
        $I("lblSN3").innerText = sSN3;
        $I("lblSN2").innerText = sSN2;
        $I("lblSN1").innerText = sSN1;

        if ($I("txtDesSN4").value == "") {
            $I("lblSN3").className = "texto";
            $I("lblSN3").onclick = null;
        }
        if ($I("txtDesSN3").value == "") {
            $I("lblSN2").className = "texto";
            $I("lblSN2").onclick = null;
        }
        if ($I("txtDesSN2").value == "") {
            $I("lblSN1").className = "texto";
            $I("lblSN1").onclick = null;
        }

        if ($I("txtUltRecGF").value != "") $I("txtUltRecGF").value = AnoMesToMesAnoDesc($I("txtUltRecGF").value);

        if ($I("txtID").value != "") {
            nAnoMesActual = $I("hdnCierreECO").value;
            nAnoMesActualIAP = $I("hdnCierreIAP").value;
        } else {
            nAnoMesActual = nAnoMesAnteriorAlActual;
            nAnoMesActualIAP = nAnoMesAnteriorAlActual;
            $I("hdnCierreECO").value = nAnoMesActual;
            $I("hdnCierreIAP").value = nAnoMesActualIAP;
        }

        $I("txtCierreEco").value = AnoMesToMesAnoDescLong($I("hdnCierreECO").value);
        $I("txtCierreIAP").value = AnoMesToMesAnoDescLong($I("hdnCierreIAP").value);

        if (sOrigen == "MantFiguras") $I("txtApellido1").focus();
        else $I("txtDenominacion").focus();

        if ($I("cboHermes").value == 'N') $I("chkDesglose").disabled = "disabled";

        $I("hdnDenominacion").value = $I("txtDenominacion").value;
        aPestGenParam[1].bLeido = true;

        ocultarProcesando();

    }
    catch (e) {
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
    if (aPestGral[3].bModif) {
        //Control de las figuras virtuales
        for (var i = 0; i < $I("tblFiguras2V").rows.length; i++) {
            if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "" && $I("tblFiguras2V").rows[i].getAttribute("bd") != "D") {
                var aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI"); //2
                if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {

                    iCaso2 = 2;
                    iIndice2 = i;
                    //break;
                    $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                }
            }
        }
    }
    if ((iCaso1 == 1) || (iCaso2 == 2) || ($I("chkRepresentativo").checked == true && $I("hdnRepresentativo").value == "0")) {
        if (iCaso1 == 1) {
            tsPestanas.setSelectedIndex(1);
            ms($I("tblFiguras2").rows[iIndice1]);
        }
        else if (iCaso2 == 2) {
            tsPestanas.setSelectedIndex(3);
            ms($I("tblFiguras2V").rows[iIndice2])
        }
        if ((iCaso1 == 1) || (iCaso2 == 2))
            sMsg = "Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar con la grabación?";
        else {
            var reg = /{salto}/gi;
            var a = $I("hdnMensajeRepresentativo").value;
            a = a.replace(reg, "\n");
            if (a != "") {
                sMsg = "Las siguientes áreas de negocio tienen empresa asignada:<br><br>" + a + "<br><br> Se borrarán las empresas asignadas a dichas áreas de negocio. <br><br> ¿Deseas continuar?";
            }
            jqConfirm("", sMsg, "", "", "war", 320).then(function (answer) {
                if (answer) {
                    if (!comprobarDatos()) return;
                    bSalir = true;
                    if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                    if (iCaso2 == 2) $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                    grabar2();
                }
                else {
                    bCambios = false;
                    modalDialog.Close(window, returnValue);
                }
            });
        }
    }
    else if (bCambios) {
        if (!comprobarDatos()) return;
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
function recargarArrayFigurasV() {
    try {
        aFigIniV = new Array();
        for (var i = $I("tblFiguras2V").rows.length - 1; i >= 0; i--) {
            aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI");
            for (var x = 0; x < aLIs.length; x++) {
                insertarFiguraEnArrayV($I("tblFiguras2V").rows[i].id, aLIs[x].id)
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al recargarArrayFigurasV", e.message);
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
    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar una figura en el array de figuras.", e.message);
    }
}
function insertarFiguraEnArrayV(idUser, sFig) {
    try {
        oFIG = new objFigura(idUser, sFig);
        aFigIniV[aFigIniV.length] = oFIG;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar una figura en el array figuras virtuales.", e.message);
    }
}
function comprobarDatos() {
    try {
        if ($I("hdnIDSN1").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", sSN1 + " es dato obligatorio.", 350, 2500);
            return false;
        }
        if (fTrim($I("txtDenominacion").value) == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La denominación es dato obligatorio", 300, 2500);
            return false;
        }
        if (fTrim($I("txtAbreviatura").value) == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La abreviatura (denominación corta) es dato obligatorio", 350, 2500);
            return false;
        }
        if ($I("hdnIDResponsable").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "El responsable es dato obligatorio", 300, 2500);
            return false;
        }
        if ($I("hdnIDEmpresa").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La empresa es dato obligatorio", 300, 2500);
            return false;
        }
        if (parseInt($I("txtTolerancia").value, 10) < 0 || parseInt($I("txtTolerancia").value, 10) > 255) {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La tolerancia debe ser un dato entre 0 y 255", 300, 2500);
            return false;
        }
        if ($I("txtCualificador").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La denominación del \"Cualificador de proyecto\" es dato obligatorio", 400, 2500);;
            return false;
        }
        if ($I("cboOrgVtas").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La organización de ventas es un dato obligatorio", 350, 2500);;
            return false;
        }

        if (aPestGral[0].bModif) {
            //Control de soportes administrativos modificados                 
            var sNew = "0";
            for (var i = 0; i < tblSoporte.rows.length; i++) {
                if ($I("tblSoporte").rows[i].cells[1].children[0].checked) {
                    sNew = "1";
                    break;
                }
            }
            if (sNew == "0" && $I("chkSoporte").checked) {
                tsPestanas.setSelectedIndex(0);
                tsPestanasGenParam.setSelectedIndex(1);

                mmoff("War", "Es obligatorio indicar algún modelo de contratación", 350, 2000);
                return false;
            }
        }
        //Compruebo que solo exista un subnodo marcado como defecto para réplica
        var iContSn = 0;
        for (var i = 0; i < $I("tblSubNodomaniobra3").rows.length; i++) {
            if ($I("tblSubNodomaniobra3").rows[i].cells[1].children[0].checked) {
                iContSn++;
            }
        }
        if (iContSn > 1) {
            tsPestanas.setSelectedIndex(5);
            mmoff("WarPer", "Solo puede haber un elemento marcado como destino de réplica por defecto", 350, 2000);
            return false;
        }

        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar() {
    try {
        if (!comprobarDatos()) return;

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

        if (aPestGral[2].bModif && $I("tblDatos")!=null) {
            //Control de las extensiones
            for (var i = 0; i < $I("tblDatos").rows.length; i++) {
                if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                    if ($I("tblDatos").rows[i].cells[4].children[0].value == "")
                        $I("tblDatos").rows[i].cells[4].children[0].value = "0,00";
                    if ($I("tblDatos").rows[i].cells[5].children[0].value == "")
                        $I("tblDatos").rows[i].cells[5].children[0].value = "0,00";
                    if ($I("tblDatos").rows[i].cells[6].children[0].value == "")
                        $I("tblDatos").rows[i].cells[6].children[0].value = "0,00";
                    if ($I("tblDatos").rows[i].cells[7].children[0].value == "")
                        $I("tblDatos").rows[i].cells[7].children[0].value = "0,00";
                }
            }
        }

        if (aPestGral[3].bModif && iCaso1 == 0) {
            //Control de las figuras virtuales
            for (var i = 0; i < $I("tblFiguras2V").rows.length; i++) {
                if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "" && $I("tblFiguras2V").rows[i].getAttribute("bd") != "D") {
                    var aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI"); //2
                    if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {

                        iCaso2 = 2;
                        iIndice2 = i;
                        //break;
                        $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                    }
                }
            }
        }
        if ((iCaso1 == 1) || (iCaso2 == 2) || ($I("chkRepresentativo").checked == true && $I("hdnRepresentativo").value == "0")) {
            if (iCaso1 == 1) {
                tsPestanas.setSelectedIndex(1);
                ms($I("tblFiguras2").rows[iIndice1]);
            }
            if (iCaso2 == 2) {
                tsPestanas.setSelectedIndex(3);
                ms($I("tblFiguras2V").rows[iIndice2])
            }
            if ((iCaso1 == 1) || (iCaso2 == 2)) {
                jqConfirm("", "Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar?", "", "", "war").then(function (answer) {
                    if (answer) {
                        if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                        if (iCaso2 == 2) $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                        LLamadaGrabar();
                    } else LLamadaGrabar();
                });
            }
            else {
                //var reg = new RegExp(/<br>/g);
                var reg = /{salto}/gi;
                var a = $I("hdnMensajeRepresentativo").value;
                a = a.replace(reg, "\n");
                if (a != "") {
                    a = "Las siguientes áreas de negocio tienen empresa asignada:<br><br>" + a + "<br><br> Se borrarán las empresas asignadas a dichas áreas de negocio. <br><br> ¿Deseas continuar?";
                    jqConfirm("", a, "", "", "war", 400).then(function (answer) {
                        if (answer) LLamadaGrabar();
                    });
                }
            }
        } else LLamadaGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}

function grabar2() {
    LLamadaGrabar();
}
function LLamadaGrabar() {
    try {
        var js_args = "grabar@#@";
        js_args += $I("txtID").value.replace(".", "");
        js_args += "@#@";
        js_args += grabarP0_0(); //datos generales
        js_args += "@#@";
        js_args += grabarP0_1(); //Soporte administrativo
        js_args += "@#@";
        js_args += grabarP1(); //figuras
        js_args += "@#@";
        js_args += grabarP2(); //figuras virtuales
        js_args += "@#@";
        js_args += grabarP3(); //Alertas
        js_args += "@#@";
        js_args += grabarP4(); //Subnodos por defecto para réplica

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaGrabar", e.message);
    }
}
function grabarP0_0() {
    var sb = new StringBuilder;
    if (aPestGral[0].bModif) {
        //if ($I("txtID").value == "")
            bHayCambios = true;

        sb.Append($I("txtID").value.replace(".", "") + "##"); //0
        sb.Append(Utilidades.escape($I("txtDenominacion").value) + "##"); //1
        sb.Append($I("hdnIDResponsable").value + "##"); //2
        sb.Append($I("hdnIDSN1").value + "##"); //3
        sb.Append(($I("txtOrden").value == "") ? "0##" : $I("txtOrden").value + "##"); //4
        sb.Append($I("cboModoCoste").value + "##"); //5
        sb.Append($I("cboModoTarifa").value + "##"); //6
        sb.Append($I("cboHermes").value + "##"); //7
        sb.Append(($I("chkActivo").checked == true) ? "1##" : "0##"); //8
        sb.Append(($I("chkMasUnGF").checked == true) ? "1##" : "0##"); //9
        sb.Append(($I("chkGR").checked == true) ? "1##" : "0##"); //10
        sb.Append(($I("chkPGRCG").checked == true) ? "1##" : "0##"); //11
        sb.Append(($I("chkRepresentativo").checked == true) ? "1##" : "0##"); //12
        sb.Append(($I("chkMailCIA").checked == true) ? "1##" : "0##"); //13
        sb.Append(($I("chkCuadre").checked == true) ? "1##" : "0##"); //14
        sb.Append($I("txtTolerancia").value + "##"); //15
        sb.Append(($I("chkCalcular").checked == true) ? "1##" : "0##"); //16
        sb.Append($I("hdnIDEmpresa").value + "##"); //17
        sb.Append(($I("chkCierreIAPest").checked == true) ? "1##" : "0##"); //18
        sb.Append(($I("chkCierreECOest").checked == true) ? "1##" : "0##"); //19 
        sb.Append(($I("chkImprodGen").checked == true) ? "1##" : "0##"); //20
        sb.Append($I("hdnCierreIAP").value + "##"); //21
        sb.Append($I("hdnCierreECO").value + "##"); //22
        sb.Append(($I("txtMargenCesion").value == "") ? "0##" : $I("txtMargenCesion").value + "##"); //23
        sb.Append(($I("txtInteresGF").value == "") ? "0##" : $I("txtInteresGF").value + "##"); //24
        sb.Append(Utilidades.escape($I("txtCualificador").value) + "##"); //25
        sb.Append(($I("chkCualifObl").checked == true) ? "1##" : "0##"); //26
        sb.Append($I("cboPerfiles").value + "##"); //27
        sb.Append(($I("chkTipolInterna").checked == true) ? "1##" : "0##"); //28
        sb.Append(($I("chkTipolEspecial").checked == true) ? "1##" : "0##"); //29
        sb.Append(($I("chkTipolProdSC").checked == true) ? "1##" : "0##"); //30
        sb.Append(($I("chkDesglose").checked == true) ? "1##" : "0##"); //31
        sb.Append(($I("chkControlhuecos").checked == true) ? "1##" : "0##"); //32
        sb.Append(($I("chkPermitirPST").checked == true) ? "1##" : "0##"); //33
        sb.Append($I("cboOrgVtas").value + "##"); //34
        sb.Append(($I("chkSoporte").checked == true) ? "1##" : "0##"); //35
        sb.Append(Utilidades.escape($I("txtAbreviatura").value) + "##"); //36
        sb.Append(($I("chkAlertas").checked == true) ? "1##" : "0##"); //37
        sb.Append(($I("chkCualiCVT").checked == true) ? "1##" : "0##"); //38
        sb.Append($I("cboMoneda").value + "##"); //39   
        sb.Append($I("hdnIDCualificador").value + "##"); //40   
        sb.Append($I("txtIdOrgCom").value + "##"); //41
        sb.Append(($I("chkQEQ").checked == true) ? "1##" : "0##"); //42
        sb.Append($I("hdnInstrumentalIni").value + "##"); //43
        sb.Append(($I("chkInstrumental").checked == true) ? "1" : "0"); //44
    }
    return sb.ToString();
}
function grabarP0_1() {
    var sb = new StringBuilder;
    var sNew = "";
    if (aPestGral[0].bModif) {
        //Control de soportes administrativos modificados                 
        for (var i = 0; i < $I("tblSoporte").rows.length; i++) {
            sNew = ($I("tblSoporte").rows[i].cells[1].children[0].checked) ? "1" : "0";
            if ($I("tblSoporte").rows[i].getAttribute("old") != sNew) {
                if ($I("tblSoporte").rows[i].getAttribute("old") == "0" && sNew != "0") sb.Append("I");
                if ($I("tblSoporte").rows[i].getAttribute("old") != "0" && sNew == "0") sb.Append("D");
                //if ($I("tblSoporte").rows[i].getAttribute("old")!="" && sNew!="") sb.Append("U");
                sb.Append("@@");
                sb.Append($I("tblSoporte").rows[i].id);
                sb.Append("##");
            }
        }
    }
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
                sbFilaAct.Append($I("tblFiguras2").rows[i].getAttribute("bd") + "##"); //0
                sbFilaAct.Append($I("tblFiguras2").rows[i].id + "##"); //1
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
function grabarP2() {
    var sb = new StringBuilder;

    if (aPestGral[3].bModif) {
        //Control de las figuras virtuales
        for (var i = 0; i < $I("tblFiguras2V").rows.length; i++) {
            bGrabar = false;
            sbFilaAct = new StringBuilder;
            if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "") {
                sbFilaAct.Append($I("tblFiguras2V").rows[i].getAttribute("bd") + "##"); //0
                sbFilaAct.Append($I("tblFiguras2V").rows[i].id + "##"); //1
                if ($I("tblFiguras2V").rows[i].getAttribute("bd") == "D") {
                    //Si voy a borrar un profesional no tiene sentido hacer nada con sus figuras pues haremos delete por profesional
                    bGrabar = true;
                    //borrarUserDeArray($I("tblFiguras2").rows[i].id);
                    sbFilaAct.Append("D@");
                }
                else {
                    aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI"); //2
                    //Recorro la lista de figuras originales para ver que deletes hay que pasar
                    for (var nIndice = 0; nIndice < aFigIniV.length; nIndice++) {
                        if (aFigIniV[nIndice].idUser == $I("tblFiguras2V").rows[i].id) {
                            if (!estaEnLista(aFigIniV[nIndice].sFig, aLIs)) {
                                sbFilaAct.Append("D@" + aFigIniV[nIndice].sFig + ",");
                                bGrabar = true;
                            }
                        }
                    }
                    //Recorro la lista actual de figuras para ver que inserts hay que pasar
                    for (var x = 0; x < aLIs.length; x++) {
                        if (!estaEnLista2($I("tblFiguras2V").rows[i].id, aLIs[x].id, aFigIniV)) {
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
//Datos de las alertas
function grabarP3() {
    var sb = new StringBuilder;
    var bGrabar = false;
    if (aPestGral[4].bModif) {
        for (var i = 0; i < $I("tblAlertas").rows.length; i++) {
            if ($I("tblAlertas").rows[i].getAttribute("bd") != "N") {
                bGrabar = true;
                sb.Append($I("tblAlertas").rows[i].getAttribute("bd") + "##"); //0
                sb.Append($I("tblAlertas").rows[i].id + "##"); //1 t826_idnodoalertas
                sb.Append(getCelda($I("tblAlertas").rows[i], 3) + "##"); //Parametro 1
                sb.Append(getCelda($I("tblAlertas").rows[i], 5) + "##"); //Parametro 2
                sb.Append(getCelda($I("tblAlertas").rows[i], 7) + "##"); //Parametro 3
                sb.Append("///");
            }
        }
    }
    return sb.ToString();
}
//Datos de los Subnodos por defecto para réplica
function grabarP4() {
    var sb = new StringBuilder;
    var Estado = "0";

    if (aPestGral[5].bModif) {
        for (var i = 0; i < $I("tblSubNodomaniobra3").rows.length; i++) {
            if ($I("tblSubNodomaniobra3").rows[i].getAttribute("bd") != "N") {
                sb.Append($I("tblSubNodomaniobra3").rows[i].getAttribute("bd") + "##"); //0
                sb.Append($I("tblSubNodomaniobra3").rows[i].id + "##"); 
                Estado = "0";
                if ($I("tblSubNodomaniobra3").rows[i].cells[1].children[0].checked)
                    Estado = "1";
                sb.Append(Estado);
                sb.Append("///");
            }
        }
    }
    return sb.ToString();
}
//marcarSubnodoDefecto
function setSnD(e,idSn) {
    try {
        aG(5);
        fm(e);
        var oControl = (e.srcElement) ? e.srcElement : e.target;
        if (oControl.checked) {
            demarcarOtrosDefectos(oControl.parentNode.parentNode.id);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al marcar subnodo como defecto.", e.message);
    }
}
function demarcarOtrosDefectos(idSn) {
    try {
        for (var i = 0; i < $I("tblSubNodomaniobra3").rows.length; i++) {
            if ($I("tblSubNodomaniobra3").rows[i].getAttribute("id") != idSn) {
                if ($I("tblSubNodomaniobra3").rows[i].cells[1].children[0].checked) {
                    $I("tblSubNodomaniobra3").rows[i].setAttribute("bd", "U");
                    $I("tblSubNodomaniobra3").rows[i].cells[1].children[0].checked = false;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar si existen otros subnodos marcados como defecto.", e.message);
    }
}

function nuevo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    fOpener().insertarItem(5);
                    modalDialog.Close(window, null);
                }
            });
        }
        else
        {
            fOpener().insertarItem(5);
            modalDialog.Close(window, null);
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
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
                case "imgPapeleraFigurasV":
                    aG(3);
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

                            if (oRow.getAttribute("sexo") == "V") {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            }
                            else {
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
                case "divFiguras2V":
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

                            if (oRow.getAttribute("sexo") == "V") {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            }
                            else {
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
                            //oNC2.innerText = oRow.cells[1].innerText;

                            var oCtrl2 = document.createElement("div");
                            var oCtrl3 = document.createElement("ul");
                            oCtrl3.setAttribute("id", "box-" + oRow.id);
                            oCtrl2.appendChild(oCtrl3);
                            oNF.insertCell(-1).appendChild(oCtrl2);

                            aG(3);
                            initDragDropScriptV();
                        }
                    }
                    break;
            }
        }
        actualizarLupas("tblTituloFiguras2", "tblFiguras2");
        actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");
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
        }
        else {
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
    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura", e.message);
    }
}
function insertarFiguraV(oFila) {
    try {
        // Se inserta la fila
        for (var x = 0; x < $I("tblFiguras2V").rows.length; x++) {
            if ($I("tblFiguras2V").rows[x].cells[2].innerText == oFila.cells[1].innerText) {
                //alert("Profesional ya incluido");
                return;
            }
        }

        var oNF = $I("tblFiguras2V").insertRow(-1);
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

        //oNC2.innerText = oFila.cells[1].innerText;

        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNF.insertCell(-1).appendChild(oCtrl2);

        aG(3);
        initDragDropScriptV();
        actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");
        $I("divFiguras2V").scrollTop = $I("tblFiguras2V").rows[$I("tblFiguras2V").rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura virtual", e.message);
    }
}
function getCualificador() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCualificador.aspx", self, sSize(460, 510))
        .then(function (ret) {
            if (ret != null) {
                var aDatos = ret.split("@#@");
                $I("hdnIDCualificador").value = aDatos[0];
                $I("txtDesCualificador").value = aDatos[1].replace("&nbsp;", " ");;
                aG(0);
            }
        });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el cualificador.", e.message);
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
                $I("hdnIDResponsable").value = aDatos[0];
                $I("txtDesResponsable").value = aDatos[1].replace("&nbsp;", " ");;
                aG(0);
            }
        });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el responsable.", e.message);
    }
}

function getEmpresa() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getEmpresa.aspx", self, sSize(450, 520))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDEmpresa").value = aDatos[0];
                    $I("txtDesEmpresa").value = aDatos[1];
                    aG(0);
                }
            });

        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la empresa.", e.message);
    }
}

function getItemEstructura(nNivel) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getItemEstructura.aspx?nNivel=" + nNivel;
        switch (nNivel) {
            case 1:
                strEnlace += "&nIDPadre=0";
                break;
            case 2:
                strEnlace += "&nIDPadre=" + $I("hdnIDSN4").value;
                break;
            case 3:
                strEnlace += "&nIDPadre=" + $I("hdnIDSN3").value;
                break;
            case 4:
                strEnlace += "&nIDPadre=" + $I("hdnIDSN2").value;
                break;
        }

        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(450, 480))
        .then(function (ret) {
            if (ret != null) {
                bHayCambios = true;
                var aDatos = ret.split("@#@");
                switch (nNivel) {
                    case 1:
                        $I("txtDesSN4").value = aDatos[1];
                        $I("hdnIDSN4").value = aDatos[0];
                        $I("lblSN3").className = "enlace";
                        $I("lblSN3").onclick = function () { getItemEstructura(2) };
                        $I("txtDesSN3").value = "";
                        $I("hdnIDSN3").value = "";
                        $I("lblSN2").className = "texto";
                        $I("lblSN2").onclick = null;
                        $I("txtDesSN2").value = "";
                        $I("hdnIDSN2").value = "";
                        $I("lblSN1").className = "texto";
                        $I("lblSN1").onclick = null;
                        $I("txtDesSN1").value = "";
                        $I("hdnIDSN1").value = "";
                        break;
                    case 2:
                        $I("txtDesSN3").value = aDatos[1];
                        $I("hdnIDSN3").value = aDatos[0];
                        $I("lblSN2").className = "enlace";
                        $I("lblSN2").onclick = function () { getItemEstructura(3) };
                        $I("txtDesSN2").value = "";
                        $I("hdnIDSN2").value = "";
                        $I("lblSN1").className = "texto";
                        $I("lblSN1").onclick = null;
                        $I("txtDesSN1").value = "";
                        $I("hdnIDSN1").value = "";
                        break;
                    case 3:
                        $I("txtDesSN2").value = aDatos[1];
                        $I("hdnIDSN2").value = aDatos[0];
                        $I("lblSN1").className = "enlace";
                        $I("lblSN1").onclick = function () { getItemEstructura(4) };
                        $I("txtDesSN1").value = "";
                        $I("hdnIDSN1").value = "";
                        break;
                    case 4:
                        $I("txtDesSN1").value = aDatos[1];
                        $I("hdnIDSN1").value = aDatos[0];
                        break;
                }
                aG(0);
            }
        });

        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los elementos de estructura", e.message);
    }
}

//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
var bValidacionPestanas = true;
//validar pestana pulsada
function vpp(e, eventInfo) {
    try {
        if (eventInfo.getItem().getIndex() > 0) {
            //Evaluar lo que proceda, y si no se cumple la validación
            if ($I("txtID").value == "") {
                mmoff("Inf", "El acceso a la pestaña seleccionada, requiere grabar el nodo.", 420, null, null, null, 350);
                eventInfo.cancel();
                return false;
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la pestaña pulsada.", e.message);
    }
}
var aPestGral = new Array();
var aPestGenParam = new Array();

function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}

//function CrearMetodosPestanas() {
//    try {
//        EO1021.getSistemaPestanas = function() {
//            return this.aej.aaf.toString();
//        }
//        EO1021.getIndicePestanaPulsada = function() {
//            return this.getItem().getIndex();
//        }
//    } catch (e) {
//        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
//    }
//}


function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function CrearPestanasGenParam() {
    try {
        tsPestanasGenParam = EO1021.r._o_tsPestanasGenParam;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        ocultarIncompatibilidades();
        ocultarIncompatibilidadesV();

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }

        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
            case "ctl00_CPHC_tsPestanasGenParam":
            case "tsPestanasGenParam":
                if (!aPestGenParam[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosGenParam(eventInfo.getItem().getIndex());
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
function insertarPestanaEnArrayGenParam(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGenParam[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}

function iniciarPestanas() {
    try {

        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount() ; i++) {
            if (i == 5)//La pestaña de subnodo por defecto para réplica (maniobra=3) se carga en el Page_Load
                insertarPestanaEnArray(i, true, false);
            else
                insertarPestanaEnArray(i, false, false);
        }

        for (var i = 0; i < tsPestanasGenParam.bbd.bba.getItemCount() ; i++)
            insertarPestanaEnArrayGenParam(i, false, false);

        //Para seleccionar una subpestaña, primero hay que seleccionar su pestaña padre.
        if (sOrigen != "MantFiguras") {
            tsPestanas.setSelectedIndex(0);
            tsPestanasGenParam.setSelectedIndex(0);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount() ; i++)
            aPestGral[i].bModif = false;

        for (var i = 0; i < tsPestanasGenParam.bbd.bba.getItemCount() ; i++)
            aPestGenParam[i].bModif = false;
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
        js_args += ($I("txtID").value == "") ? "0" : $I("txtID").value;

        switch (parseInt(iPestana, 10)) {
            case 0:
                setTimeout("getDatosGenParam(0);", 500);
                return;
                break;
        }

        RealizarCallBack(js_args, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}

function getDatosGenParam(iPestana) {
    try {
        if (iPestana == 0) return;
        if ($I("txtID").value == "") {
            mmoff("Inf", "Tienes que grabar para poder acceder a la pestaña", 330);
            return;
        }
        mostrarProcesando();
        var js_args = "getDatosPestanaGenParam@#@";
        js_args += iPestana + "@#@";

        switch (parseInt(iPestana, 10)) {
            case 1:
                js_args += $I("txtID").value + "@#@";
                break;
        }

        //alert(js_args);//return;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de parámetros general " + iPestana, e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                ocultarProcesando();
                break;
            case "getDatosPestanaGenParam":
                aPestGral[0].bLeido = true;
                aPestGenParam[1].bLeido = true;
                RespuestaCallBackPestanaGenParam(aResul[2], aResul[3]);
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
            case "tecnicosV":
                $I("divFiguras1V").children[0].innerHTML = aResul[2];
                $I("divFiguras1V").scrollTop = 0;
                nTopScrollV = 0;
                $I("txtApellido1V").value = "";
                $I("txtApellido2V").value = "";
                $I("txtNombreV").value = "";
                scrollTablaV();
                actualizarLupas("tblTituloFiguras1V", "tblFiguras1V");
                ocultarProcesando();
                break;
            case "duplicar":
                $I("txtID").value = aResul[2];
                bHayCambios = true;
                //activarGrabar();
                $I("txtDenominacion").value = "Copia de " + $I("hdnDenominacion").value;
                $I("hdnDenominacion").value = $I("txtDenominacion").value;

                tsPestanas.setSelectedIndex(0);
                tsPestanasGenParam.setSelectedIndex(0);
                $I("txtDenominacion").focus();
                $I("txtDenominacion").select();
                bDuplicando = false;
                ocultarProcesando();
                mmoff("Suc", "Duplicación realizada", 180);
                break;
            case "grabar":
                desActivarGrabar();

                $I("txtID").value = aResul[2];

                var sNew = "";
                if (aPestGral[0].bModif) {
                    //Control de soportes administrativos modificados                 
                    for (var i = 0; i < $I("tblSoporte").rows.length; i++) {
                        sNew = ($I("tblSoporte").rows[i].cells[1].children[0].checked) ? "1" : "0";
                        if ($I("tblSoporte").rows[i].getAttribute("old") != sNew) $I("tblSoporte").rows[i].setAttribute("old", sNew)
                    }
                }

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

                if (aPestGral[3].bModif == true) {
                    for (var i = $I("tblFiguras2V").rows.length - 1; i >= 0; i--) {
                        if ($I("tblFiguras2V").rows[i].getAttribute("bd") == "D") {
                            $I("tblFiguras2V").deleteRow(i);
                        } else if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblFiguras2V").rows[i], "N");
                        }
                    }
                    recargarArrayFigurasV();
                }
                if (aPestGral[4].bModif == true) {
                    for (var i = $I("tblAlertas").rows.length - 1; i >= 0; i--) {
                        if ($I("tblAlertas").rows[i].getAttribute("bd") != "N")
                            mfa($I("tblAlertas").rows[i], "N");
                    }
                }
                $I("hdnDenominacion").value = $I("txtDenominacion").value;
                reIniciarPestanas();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");

                $I("hdnRepresentativo").value = ($I("chkRepresentativo").checked == true) ? "1" : "0";
                $I("hdnMensajeRepresentativo").value = "";

                if (bCrearNuevo) {
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
                }
                else {
                    if (bDuplicando) {
                        setTimeout("duplicar2();", 50);
                    }
                }
                if (bSalir) setTimeout("salir();",50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
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
            case "2": //Centros de coste
                $I("divCatalogo").children[0].innerHTML = aResul[0];
                //actualizarLupas("tblTitulo", "tblDatos");
                break;
                //            case "3"://Soporte administrativo 
                //                $I("divSoporte").children[0].innerHTML = aResul[0]; 
                //                //actualizarLupas("tblTitulo", "tblDatos"); 
                //                break;
            case "3": //Figuras Virtuales
                $I("divFiguras2V").children[0].innerHTML = aResul[0];
                initDragDropScriptV();
                actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");
                eval(aResul[1]);
                break;
            case "4": //Alertas
                $I("divAlertas").children[0].innerHTML = aResul[0];
                break;
            case "5": //Subnodos para replica
                $I("divSubNodoManiobra").children[0].innerHTML = aResul[0];
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function RespuestaCallBackPestanaGenParam(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestGenParam[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0": //
                break;
            case "1": //Soporte administrativo
                $I("divSoporte").children[0].innerHTML = aResul[0];
                break;
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de parámetros general", e.message);
    }
}
function aG(iPestana) {//Sustituye a activarGrabar
    try {
        aPestGral[iPestana].bModif = true;
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
    }
}
function aGGenParam(iSubPestana, obj) {
    try {

        if (obj != null && obj.checked) $I("chkSoporte").checked = true;
        aPestGenParam[iSubPestana].bModif = true; //Marco como modificada la subpestaña
        aG(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
function comprobarIncompatibilidades(oNuevo, aLista) {
    try {
        //1º Comprueba las incompatibilidades
        for (var i = 0; i < aLista.length; i++) {
            if ((oNuevo.id == "D" || oNuevo.id == "C" || oNuevo.id == "I")
            &&
            (aLista[i].id == "D" || aLista[i].id == "C" || aLista[i].id == "I")) {

                //mmoff("Figura no insertada por incompatibilidad.", 260, null, null, 550, 200);
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000);
                $I("divBoxeo").style.visibility = "visible";
                return false;
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras de proyecto.", e.message);
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

function comprobarIncompatibilidadesV(oNuevo, aLista) {
    try {
        //1º Comprueba las incompatibilidades
        for (var i = 0; i < aLista.length; i++) {
            if (
            (oNuevo.id == "DV" && aLista[i].id == "CV") || (oNuevo.id == "CV" && aLista[i].id == "DV") ||
            (oNuevo.id == "DV" && aLista[i].id == "IV") || (oNuevo.id == "IV" && aLista[i].id == "DV") ||
            (oNuevo.id == "DV" && aLista[i].id == "MV") || (oNuevo.id == "MV" && aLista[i].id == "DV") ||
            (oNuevo.id == "CV" && aLista[i].id == "IV") || (oNuevo.id == "IV" && aLista[i].id == "CV") ||
            (oNuevo.id == "CV" && aLista[i].id == "MV") || (oNuevo.id == "MV" && aLista[i].id == "CV") ||
            (oNuevo.id == "JV" && aLista[i].id == "MV") || (oNuevo.id == "MV" && aLista[i].id == "JV")
            ) {

                //                $I("popupWin_content").parentNode.style.left = "600px";
                //                $I("popupWin_content").parentNode.style.top = "500px";
                //                $I("popupWin_content").parentNode.style.width = "266px";
                //                $I("popupWin_content").style.width = "260px";
                //                $I("popupWin_content").innerText = "Figura no insertada por incompatibilidad.";
                //                popupWinespopup_winLoad();
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000);
                $I("divBoxeoV").style.visibility = "visible";
                return false;
            }

        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras virtuales de proyecto.", e.message);
    }
}

function mostrarIncompatibilidadesV() {
    try {
        $I("divBoxeoV").style.visibility = "hidden";
        $I("divIncompatibilidadesV").style.visibility = "visible";
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las incompatibilidades de las figuras virtuales.", e.message);
    }
}
function ocultarIncompatibilidadesV() {
    try {
        $I("divIncompatibilidadesV").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar las incompatibilidades de las figuras virtuales.", e.message);
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
function getProfesionalesFiguraV() {
    try {
        //alert(strInicial);
        if (bLectura) return;
        var sAp1 = Utilidades.escape($I("txtApellido1V").value);
        var sAp2 = Utilidades.escape($I("txtApellido2V").value);
        var sNombre = Utilidades.escape($I("txtNombreV").value);

        if (sAp1 == "" && sAp2 == "" && sNombre == "") return;
        mostrarProcesando();

        var js_args = "tecnicosV@#@";
        js_args += sAp1 + "@#@";
        js_args += sAp2 + "@#@";
        js_args += sNombre;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales en figuras virtuales", e.message);
    }
}
function cambiarMes(sValor) {
    try {
        switch (sValor) {
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
        }
        switch (sValor) {
            case "A":
                nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
                break;
            case "S":
                nAnoMesActual = AddAnnomes(nAnoMesActual, 1);
                break;
        }
        $I("hdnCierreECO").value = nAnoMesActual;
        //if ($I("hdnCierreECO").value != "") 
        $I("txtCierreEco").value = AnoMesToMesAnoDescLong($I("hdnCierreECO").value);
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}
function cambiarMesIAP(sValor) {
    try {
        switch (sValor) {
            case "A": if (getOp($I("imgAMiap")) != 100) return; break;
            case "S": if (getOp($I("imgSMiap")) != 100) return; break;
        }
        switch (sValor) {
            case "A":
                nAnoMesActualIAP = AddAnnomes(nAnoMesActualIAP, -1);
                break;
            case "S":
                nAnoMesActualIAP = AddAnnomes(nAnoMesActualIAP, 1);
                break;
        }
        $I("hdnCierreIAP").value = nAnoMesActualIAP;
        //if ($I("hdnCierreIAP").value != "") 
        $I("txtCierreIAP").value = AnoMesToMesAnoDescLong($I("hdnCierreIAP").value);
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes", e.message);
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
var nTopScrollV = 0;
var nIDTimeV = 0;
function scrollTablaV() {
    try {
        if ($I("divFiguras1V").scrollTop != nTopScrollV) {
            nTopScrollV = $I("divFiguras1V").scrollTop;
            clearTimeout(nIDTimeV);
            nIDTimeV = setTimeout("scrollTablaV()", 50);
            return;
        }
        clearTimeout(nIDTimeV);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divFiguras1V").offsetHeight / 20 + 1, $I("tblFiguras1V").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblFiguras1V").rows[i].getAttribute("sw")) {
                oFila = $I("tblFiguras1V").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.ondblclick = function () { insertarFiguraV(this) };
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
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales para asignar figuras virtuales.", e.message);
    }
}

function getAuditoriaAux() {
    try {
        if ($I("txtID").value == "") return;
        getAuditoria(2, $I("txtID").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
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
} function estaEnLista2(sUser, sFig, slLista) {
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
function dTabla() {
    try {
        var aInput = $I('tblSoporte').getElementsByTagName("INPUT");
        for (i = 0; i < aInput.length; i++) {
            if (aInput[i].type != "checkbox") continue;
            aInput[i].checked = false;
        }
        aGGenParam(1, null);
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función dTabla", e.message);
    }
}
function activarGrabar() {
    try {
        if (!bCambios) {
            bCambios = true;
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
    }
}
function duplicar() {
    
    try {
        if ($I("txtID").value == "") {
            mmoff("Inf", "Debes seleccionar un elemento para poder duplicarlo.", 270);
            return;
        }
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bDuplicando = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    duplicar2();
                }
            });
        }
        else
            duplicar2();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al duplicar el elemento", e.message);
    }
}
function duplicar2() {
    try {
        mostrarProcesando();
        var js_args = "duplicar@#@" + $I("txtID").value.replace(".", "");
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al duplicar(2)", e.message);
    }
}
function limpiarOrgCom() {
    try {
        $I('txtIdOrgCom').value = "";
        $I('txtDesOrgCom').value = "";
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar los valores de la Organización Comercial", e.message);
    }
}
//Obtiene las organizaciones comerciales activas que no estén vinculadas con ningún CR.
function getOrgCom() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/getLista.aspx?t=" + codpar('ORGCOM'), self, sSize(460, 510))
        .then(function (ret) {
            if (ret != null) {
                var aDatos = ret.split("@#@");
                $I("txtIdOrgCom").value = aDatos[0];
                $I("txtDesOrgCom").value = aDatos[1].replace("&nbsp;", " ");;
                aG(0);
            }
        });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la organización comercial.", e.message);
    }
}

function cambioInterface() {
   
    if ($I("cboHermes").value == 'N') $I("chkDesglose").disabled = "disabled";
    else $I("chkDesglose").disabled = "";
    aG(0);
}

