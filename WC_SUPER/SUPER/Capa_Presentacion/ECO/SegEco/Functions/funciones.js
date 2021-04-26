var aSegMesProy = new Array();
var nIndiceM2 = null;
var nIndiceM1 = null;
var nIndice0 = null;
var nIndiceP1 = null;
var nIndiceP2 = null;
var nIndiceIA = null;
var aImg = new Array();
var sUltCierreEcoNodo = null;
var bBuscarReplica = true;
var bNecesarioReplicar = false;
var nCosteNaturaleza = null;
var sEstado = "";
var sBorradoConfirmado = "0";
var nPrimerMesInsertado = 0;
var sHayGrupoC = "0", sHayGrupoP = "0", sHayGrupoI = "0", sHayGrupoO = "0";
var sUSA = "N";
var sProyUSA = "N";
var sProyExternalizable = "N";
var sResfreshNiveles = "N";
var bRefrescarDatosContrato = true;

var nDialogosAbiertos = 0;
var nDialogosLeerInterlocutor = 0;
var nDialogosResponderInterlocutor = 0;


function init() {
    try {
        if (bRes1024) setResolucion1024();

        setOp($I("imgME"), 30);

        setOp($I("imgPM"), 30);
        setOp($I("imgAM"), 30);
        setOp($I("imgSM"), 30);
        setOp($I("imgUM"), 30);

        //setDatosDialogos();

        if (id_proyectosubnodo_actual != "") {
            //alert("Antes de recuperarPSN");
            recuperarPSN();
        }
        setExcelImg("imgExcel", "divCatalogo");
        $I("txtNumPE").focus();
        setOpcionGusano("0,2,7,3,4,9,10,11,12,13");

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "getMesesProy":
                var aDatos = aResul[2].split("///");
                aSegMesProy.length = 0;
                var sw = 0;
                for (var i = 0; i < aDatos.length - 1; i++) {
                    var aValor = aDatos[i].split("##");
                    if (aValor[2] == "A") sw = 1;
                    aSegMesProy[i] = new Array(aValor[0], aValor[1], aValor[2]); //id, anomes, estado
                }
                if (sw == 0) setOp($I("imgME"), 30);
                else setOp($I("imgME"), 100);

                nIndiceM2 = null;
                nIndiceM1 = null;
                nIndice0 = null;
                nIndiceP1 = null;
                nIndiceP2 = null;

                if (nPrimerMesInsertado != 0) {
                    for (var i = 0; i < aSegMesProy.length; i++) {
                        if (nPrimerMesInsertado == aSegMesProy[i][1]) {
                            if (i > 1) nIndiceM2 = i - 2;
                            if (i > 0) nIndiceM1 = i - 1;
                            nIndice0 = i;
                            if (i < aSegMesProy.length - 1) nIndiceP1 = i + 1;
                            if (i < aSegMesProy.length - 2) nIndiceP2 = i + 2;

                            break;
                        }
                    }
                } else {
                    var sw = 0;
                    for (var i = 0; i < aSegMesProy.length; i++) {
                        if (aSegMesProy[i][2] == "A") {//estado abierto
                            if (i > 1) nIndiceM2 = i - 2;
                            if (i > 0) nIndiceM1 = i - 1;
                            nIndice0 = i;
                            if (i < aSegMesProy.length - 1) nIndiceP1 = i + 1;
                            if (i < aSegMesProy.length - 2) nIndiceP2 = i + 2;

                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0 && aSegMesProy.length > 0) {
                        if (aSegMesProy.length > 2) nIndiceM2 = aSegMesProy.length - 3;
                        if (aSegMesProy.length > 1) nIndiceM1 = aSegMesProy.length - 2;
                        nIndice0 = aSegMesProy.length - 1;
                        nIndiceP1 = null;
                        nIndiceP2 = null;
                    }
                }

                if (aSegMesProy.length > 0) {
                    setBotonCerrar();
                    if ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P") {
                        //alert("Antes de getCierre");
                        setTimeout("getCierre();", 50);
                    }
                    colorearMeses();
                    bOcultarProcesando = false; //si se van a obtener más datos que no se oculte el procesando
                    //alert("Antes de getResumen");
                    setTimeout("getResumen();", 50);
                } else {
                    nIndiceM2 = null;
                    nIndiceM1 = null;
                    nIndice0 = null;
                    nIndiceP1 = null;
                    nIndiceP2 = null;
                    nIndiceIA = null;

                    colorearMeses();
                    borrarCatalogo();
                }

                break;
            case "addMesesProy":
                //nNE = 1;
                nPrimerMesInsertado = parseInt(aResul[2], 10);
                bOcultarProcesando = false; //si se van a obtener más datos que no se oculte el procesando
                setTimeout("getSegMesProy();", 50);
                bAlgunCambio = true;
                break;
            case "getResumenArbol":
                $I("divCatalogo").children[0].innerHTML = aResul[2];

                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();

                $I("txtTotM2").innerText = aResul[3];
                if (parseFloat(dfn(aResul[3])) < 0) $I("txtTotM2").style.color = "#cc0000";
                else $I("txtTotM2").style.color = "black";
                if (nIndiceM2 == null) $I("txtTotM2").innerText = "";
                $I("txtTotM1").innerText = aResul[4];
                if (parseFloat(dfn(aResul[4])) < 0) $I("txtTotM1").style.color = "#cc0000";
                else $I("txtTotM1").style.color = "black";
                if (nIndiceM1 == null) $I("txtTotM1").innerText = "";
                $I("txtTot0").innerText = aResul[5];
                if (parseFloat(dfn(aResul[5])) < 0) $I("txtTot0").style.color = "#cc0000";
                else $I("txtTot0").style.color = "black";
                $I("txtTotP1").innerText = aResul[6];
                if (parseFloat(dfn(aResul[6])) < 0) $I("txtTotP1").style.color = "#cc0000";
                else $I("txtTotP1").style.color = "black";
                if (nIndiceP1 == null) $I("txtTotP1").innerText = "";
                $I("txtTotP2").innerText = aResul[7];
                if (parseFloat(dfn(aResul[7])) < 0) $I("txtTotP2").style.color = "#cc0000";
                else $I("txtTotP2").style.color = "black";
                if (nIndiceP2 == null) $I("txtTotP2").innerText = "";
                $I("txtTotIA").innerText = aResul[8];
                if (parseFloat(dfn(aResul[8])) < 0) $I("txtTotIA").style.color = "#cc0000";
                else $I("txtTotIA").style.color = "black";
                $I("txtTotIP").innerText = aResul[9];
                if (parseFloat(dfn(aResul[9])) < 0) $I("txtTotIP").style.color = "#cc0000";
                else $I("txtTotIP").style.color = "black";
                $I("txtTotTP").innerText = aResul[10];
                if (parseFloat(dfn(aResul[10])) < 0) $I("txtTotTP").style.color = "#cc0000";
                else $I("txtTotTP").style.color = "black";

                $I("txtINM2").innerText = aResul[11];
                if (parseFloat(dfn(aResul[11])) < 0) $I("txtINM2").style.color = "#cc0000";
                else $I("txtINM2").style.color = "black";
                if (nIndiceM2 == null) $I("txtINM2").innerText = "";
                $I("txtINM1").innerText = aResul[12];
                if (parseFloat(dfn(aResul[12])) < 0) $I("txtINM1").style.color = "#cc0000";
                else $I("txtINM1").style.color = "black";
                if (nIndiceM1 == null) $I("txtINM1").innerText = "";
                $I("txtIN0").innerText = aResul[13];
                if (parseFloat(dfn(aResul[13])) < 0) $I("txtIN0").style.color = "#cc0000";
                else $I("txtIN0").style.color = "black";
                $I("txtINP1").innerText = aResul[14];
                if (parseFloat(dfn(aResul[14])) < 0) $I("txtINP1").style.color = "#cc0000";
                else $I("txtINP1").style.color = "black";
                if (nIndiceP1 == null) $I("txtINP1").innerText = "";
                $I("txtINP2").innerText = aResul[15];
                if (parseFloat(dfn(aResul[15])) < 0) $I("txtINP2").style.color = "#cc0000";
                else $I("txtINP2").style.color = "black";
                if (nIndiceP2 == null) $I("txtINP2").innerText = "";
                $I("txtINIA").innerText = aResul[16];
                if (parseFloat(dfn(aResul[16])) < 0) $I("txtINIA").style.color = "#cc0000";
                else $I("txtINIA").style.color = "black";
                $I("txtINIP").innerText = aResul[17];
                if (parseFloat(dfn(aResul[17])) < 0) $I("txtINIP").style.color = "#cc0000";
                else $I("txtINIP").style.color = "black";
                $I("txtINTP").innerText = aResul[18];
                if (parseFloat(dfn(aResul[18])) < 0) $I("txtINTP").style.color = "#cc0000";
                else $I("txtINTP").style.color = "black";

                $I("txtRM2").innerText = aResul[19];
                if (parseFloat(dfn(aResul[19])) < 0) $I("txtRM2").style.color = "#cc0000";
                else $I("txtRM2").style.color = "black";
                if (nIndiceM2 == null) $I("txtRM2").innerText = "";
                $I("txtRM1").innerText = aResul[20];
                if (parseFloat(dfn(aResul[20])) < 0) $I("txtRM1").style.color = "#cc0000";
                else $I("txtRM1").style.color = "black";
                if (nIndiceM1 == null) $I("txtRM1").innerText = "";
                $I("txtR0").innerText = aResul[21];
                if (parseFloat(dfn(aResul[21])) < 0) $I("txtR0").style.color = "#cc0000";
                else $I("txtR0").style.color = "black";
                $I("txtRP1").innerText = aResul[22];
                if (parseFloat(dfn(aResul[22])) < 0) $I("txtRP1").style.color = "#cc0000";
                else $I("txtRP1").style.color = "black";
                if (nIndiceP1 == null) $I("txtRP1").innerText = "";
                $I("txtRP2").innerText = aResul[23];
                if (parseFloat(dfn(aResul[23])) < 0) $I("txtRP2").style.color = "#cc0000";
                else $I("txtRP2").style.color = "black";
                if (nIndiceP2 == null) $I("txtRP2").innerText = "";
                $I("txtRIA").innerText = aResul[24];
                if (parseFloat(dfn(aResul[24])) < 0) $I("txtRIA").style.color = "#cc0000";
                else $I("txtRIA").style.color = "black";
                $I("txtRIP").innerText = aResul[25];
                if (parseFloat(dfn(aResul[25])) < 0) $I("txtRIP").style.color = "#cc0000";
                else $I("txtRIP").style.color = "black";
                $I("txtRTP").innerText = aResul[26];
                if (parseFloat(dfn(aResul[26])) < 0) $I("txtRTP").style.color = "#cc0000";
                else $I("txtRTP").style.color = "black";

                $I("txtOCM2").innerText = aResul[27];
                if (parseFloat(dfn(aResul[27])) < 0) $I("txtOCM2").style.color = "#cc0000";
                else $I("txtOCM2").style.color = "black";
                if (nIndiceM2 == null) $I("txtOCM2").innerText = "";
                $I("txtOCM1").innerText = aResul[28];
                if (parseFloat(dfn(aResul[28])) < 0) $I("txtOCM1").style.color = "#cc0000";
                else $I("txtOCM1").style.color = "black";
                if (nIndiceM1 == null) $I("txtOCM1").innerText = "";
                $I("txtOC0").innerText = aResul[29];
                if (parseFloat(dfn(aResul[29])) < 0) $I("txtOC0").style.color = "#cc0000";
                else $I("txtOC0").style.color = "black";
                $I("txtOCP1").innerText = aResul[30];
                if (parseFloat(dfn(aResul[30])) < 0) $I("txtOCP1").style.color = "#cc0000";
                else $I("txtOCP1").style.color = "black";
                if (nIndiceP1 == null) $I("txtOCP1").innerText = "";
                $I("txtOCP2").innerText = aResul[31];
                if (parseFloat(dfn(aResul[31])) < 0) $I("txtOCP2").style.color = "#cc0000";
                else $I("txtOCP2").style.color = "black";
                if (nIndiceP2 == null) $I("txtOCP2").innerText = "";
                $I("txtOCIA").innerText = aResul[32];
                if (parseFloat(dfn(aResul[32])) < 0) $I("txtOCIA").style.color = "#cc0000";
                else $I("txtOCIA").style.color = "black";
                $I("txtOCIP").innerText = aResul[33];
                if (parseFloat(dfn(aResul[33])) < 0) $I("txtOCIP").style.color = "#cc0000";
                else $I("txtOCIP").style.color = "black";
                $I("txtOCTP").innerText = aResul[34];
                if (parseFloat(dfn(aResul[34])) < 0) $I("txtOCTP").style.color = "#cc0000";
                else $I("txtOCTP").style.color = "black";

                $I("txtSCM2").innerText = aResul[35];
                if (parseFloat(dfn(aResul[35])) < 0) $I("txtSCM2").style.color = "#cc0000";
                else $I("txtSCM2").style.color = "black";
                if (nIndiceM2 == null) $I("txtSCM2").innerText = "";
                $I("txtSCM1").innerText = aResul[36];
                if (parseFloat(dfn(aResul[36])) < 0) $I("txtSCM1").style.color = "#cc0000";
                else $I("txtSCM1").style.color = "black";
                if (nIndiceM1 == null) $I("txtSCM1").innerText = "";
                $I("txtSC0").innerText = aResul[37];
                if (parseFloat(dfn(aResul[37])) < 0) $I("txtSC0").style.color = "#cc0000";
                else $I("txtSC0").style.color = "black";
                $I("txtSCP1").innerText = aResul[38];
                if (parseFloat(dfn(aResul[38])) < 0) $I("txtSCP1").style.color = "#cc0000";
                else $I("txtSCP1").style.color = "black";
                if (nIndiceP1 == null) $I("txtSCP1").innerText = "";
                $I("txtSCP2").innerText = aResul[39];
                if (parseFloat(dfn(aResul[39])) < 0) $I("txtSCP2").style.color = "#cc0000";
                else $I("txtSCP2").style.color = "black";
                if (nIndiceP2 == null) $I("txtSCP2").innerText = "";
                $I("txtSCIA").innerText = aResul[40];
                if (parseFloat(dfn(aResul[40])) < 0) $I("txtSCIA").style.color = "#cc0000";
                else $I("txtSCIA").style.color = "black";
                $I("txtSCIP").innerText = aResul[41];
                if (parseFloat(dfn(aResul[41])) < 0) $I("txtSCIP").style.color = "#cc0000";
                else $I("txtSCIP").style.color = "black";
                $I("txtSCTP").innerText = aResul[42];
                if (parseFloat(dfn(aResul[42])) < 0) $I("txtSCTP").style.color = "#cc0000";
                else $I("txtSCTP").style.color = "black";

                sHayGrupoC = aResul[43];
                sHayGrupoP = aResul[44];
                sHayGrupoI = aResul[45];
                sHayGrupoO = aResul[46];

                if (aImg.length > 0) {
                    restaurarEstructura();
                }

                if ($I("tblDatos").rows.length == 0) {
                    nNE = 0;
                    colorearNE(nNE);
                }
                else if (nNE == 0) {
                    nNE = 1;
                    colorearNE(nNE);
                }

                if (bRefrescarDatosContrato) {
                    bOcultarProcesando = false;
                    setTimeout("getDatosContrato();", 20);
                } else {
                    bRefrescarDatosContrato = true;
                }

                getTotalesProyecto();
                setVisibilidadIconos();
                //ocultarProcesando();
                
                window.focus();
                break;
            case "getResumenClase":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila + 1);
                //tblDatos.insertarFilas(aResul[2], iFila+1);
                aFila = FilasDe("tblDatos");
                aFila[iFila].cells[0].children[0].src = strServer + "images/minus2.gif";
                aFila[iFila].setAttribute("desplegado", "1");
                sResfreshNiveles = "N";
                if (bMostrar) {
                    bOcultarProcesando = false;
                    setTimeout("MostrarTodo();", 50);
                }
                else if (bMostrarGrupoNivel) setTimeout("MostrarGrupoNivel('" + sGrupoExp + "'," + sNivelExp + ");", 50);
                else ocultarProcesando();

                break;
            case "getReplica":
                //alert(aResul[2]);
                if (aResul[2] == "1" && $I("hdnCualidadProyectoSubNodo").value != "J") {
                    if (sEstado == "A") {
                        if (!bLectura || (es_administrador == "SA" || es_administrador == "A"))
                            AccionBotonera("replica", "H");
                    }

                    $I("imgCaution").style.display = "block";
                    $I("imgCaution").title = "Necesario replicar";
                } else {
                    AccionBotonera("replica", "D");
                    $I("imgCaution").style.display = "none";
                }
                break;

            case "recuperarPSN":
                nPrimerMesInsertado = 0;
                //alert("Antes de ejecutar el codigo de la respuesta para recuperarPSN= " + aResul[2]);
                if (aResul[2] == "") {
                    if ($I("txtNumPE").value != "") mmoff("Inf", "El proyecto no existe o está fuera de su ámbito de visión.", 360); ;
                    break;
                }
                $I("txtNumPE").value = aResul[2].ToString("N", 9, 0);
                $I("txtDesPE").value = Utilidades.unescape(aResul[3]);
                $I("hdnIdProyectoSubNodo").value = aResul[4];
                $I("hdnIdNodo").value = aResul[5];
                $I("hdnModeloCoste").value = aResul[6];
                $I("hdnModeloTarificacion").value = aResul[38];
                $I("hdnCualidadProyectoSubNodo").value = aResul[7];

                switch (aResul[7]) {
                    case "C":
                        $I("fstContrato").style.visibility = "visible";
                        var sEstructura = "";
                        if (aResul[28] != "") sEstructura += aResul[28];
                        if (aResul[27] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[27];
                        }
                        if (aResul[26] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[26];
                        }
                        if (aResul[25] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[25];
                        }
                        sEstructura += "<br>" + aResul[24];
                        sEstructura += "<br>" + aResul[23];
                        sEstructura += "<br>" + aResul[29];

                        if (aResul[41] != "0") {
                            if (aResul[42] != "")
                                sEstructura += "<br><label style='width:200px'>Proyecto externalizado</label><br>";
                            else
                                sEstructura += "<br><label style='width:200px'>Proyecto externalizable</label><br>";
                            if (aResul[42] != "") sEstructura += "<label style='width:100px'>Soporte titular:</label>" + aResul[42] + "<br>";
                            if (aResul[43] != "") sEstructura += "<label style='width:100px'>Soporte alternativo:</label>" + aResul[43] + "<br>";
                        }
                        sEstructura += "<br><label style='width:70px'>Moneda:</label>" + aResul[46];
                        sEstructura += "<br><label style='width:70px'>Mod. contrat.:</label>" + aResul[53];

                        $I("divCualidadPSN").innerHTML = "<img id='imgCualidadPSN' src='" + strServer + "images/imgContratante.png' style='height:40px;width:120px;' />";
                        var sTooltipCualidad = sEstructura;
                        $I("divCualidadPSN").onmouseover = function() { showTTE(Utilidades.escape(sTooltipCualidad), "Instancia de proyecto contratante"); };
                        $I("divCualidadPSN").onmouseout = function() { hideTTE(); };


                        setOp($I("imgProduccionProf"), 100);
                        $I("imgProduccionProf").onclick = function() { accesoDirecto(6) };
                        $I("imgProduccionProf").style.cursor = "pointer";
                        setOp($I("imgConsumoNivel"), 100);
                        $I("imgConsumoNivel").onclick = function() { accesoDirecto(7) };
                        $I("imgConsumoNivel").style.cursor = "pointer";
                        setOp($I("imgProduccionPerfil"), 100);
                        $I("imgProduccionPerfil").onclick = function() { accesoDirecto(8) };
                        $I("imgProduccionPerfil").style.cursor = "pointer";
                        break;
                    case "J":
                        $I("fstContrato").style.visibility = "hidden";
                        var sEstructura = "";
                        if (aResul[28] != "") sEstructura += aResul[28];
                        if (aResul[27] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[27];
                        }
                        if (aResul[26] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[26];
                        }
                        if (aResul[25] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[25];
                        }
                        sEstructura += "<br>" + aResul[24];
                        sEstructura += "<br>" + aResul[23];
                        sEstructura += "<br>" + aResul[29];
                        var sExterna = "";
                        if (aResul[41] != "0") {
                            if (aResul[42] != "")
                                sExterna = "<label style='width:200px'>Proyecto externalizado</label><br>";
                            else
                                sExterna = "<label style='width:200px'>Proyecto externalizable</label><br>";
                            if (aResul[42] != "") sExterna += "<label style='width:100px'>Soporte titular:</label>" + aResul[42] + "<br>";
                            if (aResul[43] != "") sExterna += "<label style='width:100px'>Soporte alternativo:</label>" + aResul[43] + "<br>";
                        }
                        sEstructura += "<br><label style='width:70px'>Moneda:</label>" + aResul[46];

                        $I("divCualidadPSN").innerHTML = "<img id='imgCualidadPSN' src='" + strServer + "images/imgRepJornadas.png' style='height:40px;width:120px;' />";
                        var sTooltipCualidad = sEstructura + "<br><br><b><u>Información de la instancia de proyecto contratante</u></b><br>" + aResul[30] + "<br><label style='width:70px'>Responsable:</label>" + aResul[31] + "&nbsp;&nbsp;&#123;Ext.: " + aResul[32] + "&#125;<br>" + sExterna;
                        $I("divCualidadPSN").onmouseover = function() { showTTE(Utilidades.escape(sTooltipCualidad), "Instancia de proyecto replicada sin gestión"); };
                        $I("divCualidadPSN").onmouseout = function() { hideTTE(); };

                        setOp($I("imgProduccionProf"), 100);
                        $I("imgProduccionProf").onclick = function() { accesoDirecto(6) };
                        $I("imgProduccionProf").style.cursor = "pointer";
                        setOp($I("imgConsumoNivel"), 30);
                        $I("imgConsumoNivel").onclick = null;
                        $I("imgConsumoNivel").style.cursor = "not-allowed";
                        setOp($I("imgProduccionPerfil"), 30);
                        $I("imgProduccionPerfil").onclick = null;
                        $I("imgProduccionPerfil").style.cursor = "not-allowed";
                        break;
                    case "P":
                        $I("fstContrato").style.visibility = "hidden";
                        var sEstructura = "";
                        if (aResul[28] != "") sEstructura += aResul[28];
                        if (aResul[27] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[27];
                        }
                        if (aResul[26] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[26];
                        }
                        if (aResul[25] != "") {
                            if (sEstructura != "") sEstructura += "<br>";
                            sEstructura += aResul[25];
                        }
                        sEstructura += "<br>" + aResul[24];
                        sEstructura += "<br>" + aResul[23];
                        sEstructura += "<br>" + aResul[29];

                        var sExterna = "";
                        if (aResul[41] != "0") {
                            if (aResul[42] != "")
                                sExterna = "<label style='width:200px'>Proyecto externalizado</label><br>";
                            else
                                sExterna = "<label style='width:200px'>Proyecto externalizable</label><br>";
                            if (aResul[42] != "") sExterna += "<label style='width:100px'>Soporte titular:</label>" + aResul[42] + "<br>";
                            if (aResul[43] != "") sExterna += "<label style='width:100px'>Soporte alternativo:</label>" + aResul[43] + "<br>";
                        }
                        sEstructura += "<br><label style='width:70px'>Moneda:</label>" + aResul[46];

                        $I("divCualidadPSN").innerHTML = "<img id='imgCualidadPSN' src='" + strServer + "images/imgRepPrecio.png' style='height:40px;width:120px;' />";
                        var sTooltipCualidad = sEstructura + "<br><br><b><u>Información de la instancia de proyecto contratante</u></b><br>" + aResul[30] + "<br><label style='width:70px'>Responsable:</label>" + aResul[31] + "&nbsp;&nbsp;&#123;Ext.: " + aResul[32] + "&#125;" + sExterna;
                        $I("divCualidadPSN").onmouseover = function() { showTTE(Utilidades.escape(sTooltipCualidad), "Instancia de proyecto replicada con gestión"); };
                        $I("divCualidadPSN").onmouseout = function() { hideTTE(); };

                        setOp($I("imgProduccionProf"), 30);
                        $I("imgProduccionProf").onclick = null;
                        $I("imgProduccionProf").style.cursor = "not-allowed";
                        setOp($I("imgConsumoNivel"), 30);
                        $I("imgConsumoNivel").onclick = null;
                        $I("imgConsumoNivel").style.cursor = "not-allowed";
                        setOp($I("imgProduccionPerfil"), 30);
                        $I("imgProduccionPerfil").onclick = null;
                        $I("imgProduccionPerfil").style.cursor = "not-allowed";
                        break;
                }

                $I("divCualidadPSN").style.visibility = "visible";
                sUltCierreEcoNodo = aResul[8];

                $I("txtCliente").value = Utilidades.unescape(aResul[10]);

                var sTooltipCliente = "<label style='width:70px'>Cód. Cliente:</label>" + aResul[9] + "<br><label style='width:70px'>Sector:</label>" + aResul[33] + "<br><label style='width:70px'>Segmento:</label>" + aResul[34] + "<br><label style='width:70px'>Ambito:</label>" + aResul[35] + "<br><label style='width:70px'>Zona:</label>" + aResul[36];
                $I("txtCliente").onmouseover = function() { showTTE(Utilidades.escape(sTooltipCliente)); };
                $I("txtCliente").onmouseout = function() { hideTTE(); };


                $I("lgdContrato").innerText = "Contrato " + aResul[11];
                $I("txtTotContrato").value = aResul[12];
                $I("txtMrgContrato").value = aResul[13];
                $I("txtRentContrato").value = aResul[14];
                $I("txtProducido").value = aResul[47];
                if (getFloat($I("txtProducido").value) != 0) {
                    $I("cldTPPAC").title = "Total producción de todos los proyectos de categoría " + ((aResul[51] == "P") ? "producto" : "servicio") + " asociados al contrato";
                    $I("lblTPPAC").className = "enlace";
                    $I("lblTPPAC").onclick = function() { getTPPAC(); }
                } else {
                    $I("cldTPPAC").title = "Total producción de todos los proyectos asociados al contrato";
                    $I("lblTPPAC").className = "texto";
                    $I("lblTPPAC").onclick = null;
                }
                $I("txtResponsable").value = aResul[16];
                nCosteNaturaleza = aResul[18];
                $I("hdnAnnoPIG").value = aResul[19];

                if (aResul[21] == aResul[22] && aResul[20] == "0") $I("hdnAnotarProd").value = "0";
                else $I("hdnAnotarProd").value = "1";

                if (aResul[37] == "0") $I("hdnEsReplicable").value = "0";
                else $I("hdnEsReplicable").value = "1";

                $I("lblMonedaImportes").innerText = aResul[52];
                
                AccionBotonera("insertarmes", "D");
                AccionBotonera("borrarmes", "D");
                AccionBotonera("clonar", "D");
                AccionBotonera("replica", "D");
                AccionBotonera("cerrarmes", "D");
                AccionBotonera("icotras", "D");
                AccionBotonera("graficovg", "D");

                sUSA = "N";
                sProyUSA = "N";
                sProyExternalizable = "N";

                sEstado = aResul[17];
                switch (sEstado) {
                    case "A":
                        //$I("imgEstado").src = "../../../Images/imgProyAbierto.gif";
                        switch (aResul[48]) {
                            case "":
                                $I("imgEstado").src = "../../../Images/imgProyAbierto.gif";
                                break;
                            case "GA":
                                $I("imgEstado").src = "../../../Images/imgProyAbiertoGarantia.png";

                                var sTooltipGarantia = "El proyecto se encuentra en fase de garantía. El periodo que comprende dicha garantía es el siguiente:<br><br><label style='width:80px;'>Comienzo:</label>" + aResul[49] + "<br><label style='width:80px;'>Finalización:</label>" + aResul[50];
                                $I("imgEstado").onmouseover = function() { showTTE(Utilidades.escape(sTooltipGarantia), "Garantía", null, 200); };
                                $I("imgEstado").onmouseout = function() { hideTTE(); };
                                break;
                            case "GE":
                                $I("imgEstado").src = "../../../Images/imgProyAbiertoGarExp.png";

                                var sTooltip = "El periodo de garantía del proyecto ha expirado. La garantía comprendía el siguiente periodo:<br><br><label style='width:80px;'>Comienzo:</label>" + aResul[49] + "<br><label style='width:80px;'>Finalización:</label>" + aResul[50];
                                $I("imgEstado").onmouseover = function() { showTTE(Utilidades.escape(sTooltip), "Garantía", null, 200); };
                                $I("imgEstado").onmouseout = function() { hideTTE(); };
                                break;
                        }
                        if (!bLectura || (es_administrador == "SA" || es_administrador == "A")) {
                            AccionBotonera("insertarmes", "H");
                            AccionBotonera("borrarmes", "H");
                            AccionBotonera("clonar", "H");
                            if ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P") {
                                AccionBotonera("graficovg", "H");
                            }
                            if ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P") {
                                AccionBotonera("icotras", "H");
                            }
                            //AccionBotonera("Facturas","H");

                            if ($I("hdnCualidadProyectoSubNodo").value == "C") {
                                if (usu_actual == aResul[39] || usu_actual == aResul[40]) sUSA = "S";
                                if (aResul[39] != "0" || aResul[40] != "0") sProyUSA = "S";
                                if (aResul[41] != "0") sProyExternalizable = "S";
                            }
                        }
                        break;
                    case "C":
                        $I("imgEstado").src = "../../../Images/imgProyCerrado.gif";
                        break;
                    case "H":
                        $I("imgEstado").src = "../../../Images/imgProyHistorico.gif";
                        break;
                    case "P":
                        $I("imgEstado").src = "../../../Images/imgProyPresup.gif";
                        if (!bLectura || (es_administrador == "SA" || es_administrador == "A")) {
                            AccionBotonera("insertarmes", "H");
                            AccionBotonera("borrarmes", "H");
                            AccionBotonera("clonar", "H");
                            if ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P") {
                                AccionBotonera("graficovg", "H");
                            }
                        }
                        break;
                }
                $I("imgEstado").style.visibility = "visible";

                if (sMONEDA_VDP == "") {
                    sMONEDA_VDP = aResul[44]; //t422_idmoneda_proyecto
                    $I("lblMonedaImportes").innerText = aResul[45]; //t422_denominacionimportes
                }

                $I("divMonedaImportes").style.visibility = "visible";

                bBuscarReplica = true;
                bLimpiarDatos = true;
                bOcultarProcesando = false; //si se van a obtener más datos que no se oculte el procesando
                //alert("Antes de getSegMesProy");


                setTimeout("getDatosDialogos();", 20);
                //setTimeout("getSegMesProy();", 50);
                break;

            case "buscarPE":
            case "buscarDesPE":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto no existe o está fuera de su ámbito de visión.", 360); ;
                } else {
                    bOcultarProcesando = false; //si se van a obtener más datos que no se oculte el procesando
                    var aProy = aResul[2].split("///");
                    //alert(aProy.length);
                    if (aProy.length == 2) {
                        var aDatos = aProy[0].split("##")
                        limpiarPantalla();
                        $I("imgCaution").style.display = "none";
                        id_proyectosubnodo_actual = aDatos[0];
                        if (aDatos[1] == "1") {
                            bLectura = true;
                        } else {
                            bLectura = false;
                        }
                        setTimeout("recuperarPSN();", 50);
                    } else {
                        if (aResul[0] == "buscarPE")
                            setTimeout("getPEByNum();", 50);
                        else
                            setTimeout("getPEByDes();", 50);
                    }
                }
                break;
            case "setResolucion":
                location.reload(true);
                break;

            case "getDatosContrato":
                $I("txtTotContrato").value = aResul[2];
                $I("txtMrgContrato").value = aResul[3];
                $I("txtRentContrato").value = aResul[4];
                $I("txtProducido").value = aResul[7];
                if (getFloat($I("txtProducido").value) != 0) {
                    $I("cldTPPAC").title = "Total producción de todos los proyectos de categoría " + ((aResul[8] == "P") ? "producto" : "servicio") + " asociados al contrato";
                    $I("lblTPPAC").className = "enlace";
                    $I("lblTPPAC").onclick = function() { getTPPAC(); }
                } else {
                    $I("lblTPPAC").className = "texto";
                    $I("lblTPPAC").onclick = null;
                }

                if (sMONEDA_VDP == "") {
                    sMONEDA_VDP = aResul[5]; //t422_idmoneda_proyecto
                    $I("lblMonedaImportes").innerText = aResul[6]; //t422_denominacionimportes
                }
                $I("divMonedaImportes").style.visibility = "visible";

                if ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P") {
                    setTimeout("getCierre();", 50);
                }

                break;
            case "getDatosDialogos":
                if (aResul[2] == "1" && aResul[3]=="1") {//tiene alertas y el usuario es el interlocutor.
                    //                    sb.Append(((oDA.bUsuarioEsInterlocutor) ? "1" : "0") + "@#@");          //3
                    //                    sb.Append(oDA.nDialogosAbiertos.ToString() + "@#@");                //4
                    //                    sb.Append(oDA.nPendienteLeerInterlocutor.ToString() + "@#@");       //5
                    //                    sb.Append(oDA.nPendienteResponderInterlocutor.ToString() + "@#@");  //6
                    //                    sb.Append(oDA.nPendienteLeerGestor.ToString() + "@#@");             //7
                    //                    sb.Append(oDA.nPendienteResponderGestor.ToString() + "@#@");        //8
                    //
                    nDialogosAbiertos = parseInt(aResul[4], 10);
                    nDialogosLeerInterlocutor = parseInt(aResul[5], 10);
                    nDialogosResponderInterlocutor = parseInt(aResul[6], 10);

                    setDatosDialogos();
                } else {
                    $I("divDialogos").style.visibility = "hidden";
                }
                bOcultarProcesando = false;
                setTimeout("getSegMesProy();", 50);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        //if (!bMostrar || bOcultarProcesando)
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

var oImgAux;
function mostrarCallBack() {
    mostrar(oImgAux);
}

function restaurarEstructura() {
    try {
        var j = 0;
        sResfreshNiveles = "N";
        var tblDatos = $I("tblDatos");
        for (var nEstr = 0; nEstr < tblDatos.rows.length; nEstr++) {
            //Guardo la información de lo que está contraído/expandido, para dejar el arbol igual.
            if (tblDatos.rows[nEstr].style.display == "none" || tblDatos.rows[nEstr].getAttribute("nivel") == 4) continue;

            if (aImg[j] == 0 && tblDatos.rows[nEstr].cells[0].children[0].src.indexOf("plus2.gif") > -1) {
                mostrar(tblDatos.rows[nEstr].cells[0].children[0]);
            }
            j++;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al restaurar la estructura", e.message);
    }
}

function fActualizarArbol() {
    try {
        aImg.length = 0;
        var tblDatos = $I("tblDatos");
        if (tblDatos != null) {
            var sw = 0;
            for (var i = 0; i < tblDatos.rows.length; i++) {
                //Guardo la información de lo que está contraído/expandido, para dejar el arbol igual.
                if (tblDatos.rows[i].style.display == "none" || tblDatos.rows[i].getAttribute("nivel") == 4) continue;

                if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus2.gif") > -1) aImg[aImg.length] = 1;
                else aImg[aImg.length] = 0;
            }
        } else {
            nNE = 1;
            colorearNE(nNE);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el estado del árbol", e.message);
    }
}

function cambiarMes(sValor) {
    try {
        if (aSegMesProy.length == 0) return;
        switch (sValor) {
            case "P": if (getOp($I("imgPM")) != 100) return; break;
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
            case "U": if (getOp($I("imgUM")) != 100) return; break;
        }
        switch (sValor) {
            case "A":
                if (nIndice0 > 0) nIndice0--;
                else return;

                if (nIndiceM1 != null && nIndiceM1 > 0) {
                    nIndiceM1--;
                } else nIndiceM1 = null;
                if (nIndiceM2 != null && nIndiceM2 > 0) {
                    nIndiceM2--;
                } else nIndiceM2 = null;

                if (nIndice0 < aSegMesProy.length - 1) {
                    if (nIndiceP1 == null) nIndiceP1 = aSegMesProy.length - 1;
                    else nIndiceP1--;
                } else nIndiceP1 = null;
                if (nIndice0 < aSegMesProy.length - 2) {
                    if (nIndiceP2 == null) nIndiceP2 = aSegMesProy.length - 1;
                    else nIndiceP2--;
                } else nIndiceP2 = null;
                break;

            case "S":
                if (nIndice0 < aSegMesProy.length - 1) nIndice0++;
                else return;

                if (nIndiceM1 < aSegMesProy.length - 1) {
                    if (nIndiceM1 == null) nIndiceM1 = 0;
                    else nIndiceM1++;
                }
                if (nIndiceM2 < aSegMesProy.length - 2) {
                    if (nIndiceM1 == 0) nIndiceM2 = null;
                    else if (nIndiceM2 == null) nIndiceM2 = 0;
                    else nIndiceM2++;
                }

                if (nIndiceP1 != null && nIndiceP1 < aSegMesProy.length - 1) {
                    nIndiceP1++;
                } else nIndiceP1 = null;
                if (nIndiceP2 != null && nIndiceP2 < aSegMesProy.length - 1) {
                    nIndiceP2++;
                } else nIndiceP2 = null;
                break;

            case "P":
                nIndiceM2 = null;
                nIndiceM1 = null;
                nIndice0 = 0;
                if (aSegMesProy.length > 1) nIndiceP1 = 1;
                else nIndiceP1 = null;
                if (aSegMesProy.length > 2) nIndiceP2 = 2;
                else nIndiceP2 = null;
                break;

            case "U":
                if (aSegMesProy.length > 2) nIndiceM2 = aSegMesProy.length - 3;
                else nIndiceM2 = null;
                if (aSegMesProy.length > 1) nIndiceM1 = aSegMesProy.length - 2;
                else nIndiceM1 = null;

                nIndice0 = aSegMesProy.length - 1;
                nIndiceP1 = null;
                nIndiceP2 = null;
                break;
        }

        sBorradoConfirmado = "0";
        bBuscarReplica = false;

        setBotonCerrar();

        colorearMeses();
        fActualizarArbol();

        bRefrescarDatosContrato = false;
        getResumen();
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}

function colorearMeses() {
    try {
        if (aSegMesProy.length == 0) {
            $I("lblMesM2").innerText = "";
            $I("lblMesM1").innerText = "";
            $I("lblMes0").innerText = "";
            $I("lblMesP1").innerText = "";
            $I("lblMesP2").innerText = "";
            $I("lblIA").innerText = "";
            $I("lblIP").innerText = "";
            $I("lblTP").innerText = "";
            return;
        }

        if (nIndiceM2 != null) {
            $I("lblMesM2").innerText = AnoMesToMesAnoDesc(aSegMesProy[nIndiceM2][1]);
            if (aSegMesProy[nIndiceM2][2] == "A") $I("lblMesM2").className = "textoV";
            else $I("lblMesM2").className = "textoR2";
        } else {
            $I("lblMesM2").innerText = "";
            $I("lblMesM2").className = "textoB";
        }
        if (nIndiceM1 != null) {
            $I("lblMesM1").innerText = AnoMesToMesAnoDesc(aSegMesProy[nIndiceM1][1]);
            if (aSegMesProy[nIndiceM1][2] == "A") $I("lblMesM1").className = "textoV";
            else $I("lblMesM1").className = "textoR2";
        } else {
            $I("lblMesM1").innerText = "";
            $I("lblMesM1").className = "textoB";
        }
        if (nIndice0 != null) {
            var nAnno = aSegMesProy[nIndice0][1].substring(0, 4);
            nIndiceIA = 0;
            for (nIndiceIA = 0; nIndiceIA < aSegMesProy.length; nIndiceIA++) {
                if (aSegMesProy[nIndiceIA][1].toString().substring(0, 4) == nAnno)
                    break;
            }
            $I("lblMes0").innerText = AnoMesToMesAnoDesc(aSegMesProy[nIndice0][1]);
            $I("lblMes0").style.cursor = strCurMA;
            $I("lblMes0").ondblclick = function() { mostrarProcesando(); setTimeout("setMes();", 50); };
            $I("lblIA").innerText = aMes[parseInt(aSegMesProy[nIndiceIA][1].toString().substring(4, 6), 10) - 1] + " - " + AnoMesToMesAnoDesc(aSegMesProy[nIndice0][1]);
            $I("lblIP").innerText = AnoMesToMesAnoDesc(aSegMesProy[0][1]) + " - " + AnoMesToMesAnoDesc(aSegMesProy[nIndice0][1]);
            $I("lblTP").innerText = AnoMesToMesAnoDesc(aSegMesProy[0][1]) + " - " + AnoMesToMesAnoDesc(aSegMesProy[aSegMesProy.length - 1][1]);
            if (aSegMesProy[nIndice0][2] == "A") $I("lblMes0").className = "textoV";
            else $I("lblMes0").className = "textoR2";
        } else {
            $I("txtMesBase").value = "";
            $I("lblMes0").innerText = "";
            $I("lblMes0").style.cursor = "default";
            $I("lblMes0").ondblclick = null;
            $I("lblIA").innerText = "";
            $I("lblIP").innerText = "";
            $I("lblTP").innerText = "";
            $I("lblMes0").className = "textoB";
        }
        if (nIndiceP1 != null) {
            $I("lblMesP1").innerText = AnoMesToMesAnoDesc(aSegMesProy[nIndiceP1][1]);
            if (aSegMesProy[nIndiceP1][2] == "A") $I("lblMesP1").className = "textoV";
            else $I("lblMesP1").className = "textoR2";
        } else {
            $I("lblMesP1").innerText = "";
            $I("lblMesP1").className = "textoB";
        }
        if (nIndiceP2 != null) {
            $I("lblMesP2").innerText = AnoMesToMesAnoDesc(aSegMesProy[nIndiceP2][1]);
            if (aSegMesProy[nIndiceP2][2] == "A") $I("lblMesP2").className = "textoV";
            else $I("lblMesP2").className = "textoR2";
        } else {
            $I("lblMesP2").innerText = "";
            $I("lblMesP2").className = "textoB";
        }

        if (nIndice0 == 0 && aSegMesProy.length == 1) {
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        } else if (nIndice0 == 0) {
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        } else if (nIndice0 == aSegMesProy.length - 1) {
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        } else {
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        }
        var nIndiceAbierto = getPMA();
        if (nIndiceAbierto == nIndice0) {
            setOp($I("imgME"), 30);
        } else {
            setOp($I("imgME"), 100);
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al indicar el estado de los meses", e.message);
    }
}

function getResumen() {
    try {
        if (aSegMesProy.length == 0) {
            //$I("divCatalogo").children[0].innerHTML = "";
            borrarCatalogo();
            ocultarProcesando();
            return;
        }

        var nAnno = aSegMesProy[nIndice0][1].substring(0, 4);
        nIndiceIA = 0;
        for (nIndiceIA = 0; nIndiceIA < aSegMesProy.length; nIndiceIA++) {
            if (aSegMesProy[nIndiceIA][1].toString().substring(0, 4) == nAnno)
                break;
        }

        var js_args = "getResumenArbol@#@";
        js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
        js_args += (nIndiceM2 == null) ? "0@#@" : aSegMesProy[nIndiceM2][1] + "@#@"; //anomes base - 2
        js_args += (nIndiceM1 == null) ? "0@#@" : aSegMesProy[nIndiceM1][1] + "@#@"; //anomes base - 1
        js_args += (nIndice0 == null) ? "0@#@" : aSegMesProy[nIndice0][1] + "@#@"; //anomes base
        js_args += (nIndiceP1 == null) ? "0@#@" : aSegMesProy[nIndiceP1][1] + "@#@"; //anomes base + 1
        js_args += (nIndiceP2 == null) ? "0@#@" : aSegMesProy[nIndiceP2][1] + "@#@"; //anomes base + 2
        js_args += aSegMesProy[nIndiceIA][1] + "@#@"; //anomes inicio año
        js_args += aSegMesProy[0][1] + "@#@"; //anomes inicio proyecto
        js_args += aSegMesProy[aSegMesProy.length - 1][1] + "@#@"; //anomes fin proyecto
        js_args += $I("hdnCualidadProyectoSubNodo").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar el resumen", e.message);
    }
}

function mostrar(oImg, bMos) {
    try {
        //if (event != null && event.srcElement == oImg)
        if (sResfreshNiveles == "N") {
            nNE = -1;
            colorearNE(nNE);
            //sResfreshNiveles = "N";
        }
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        var opcion = "M";
        if (!bMos) {
            if (oImg.src.indexOf("plus2.gif") == -1) opcion = "O"; //ocultar
            else opcion = "M"; //mostrar
        }
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);

        if (nDesplegado == "0") {
            var js_args = "getResumenClase@#@";
            js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
            js_args += (nIndiceM2 == null) ? "0@#@" : aSegMesProy[nIndiceM2][1] + "@#@"; //anomes base - 2
            js_args += (nIndiceM1 == null) ? "0@#@" : aSegMesProy[nIndiceM1][1] + "@#@"; //anomes base - 1
            js_args += (nIndice0 == null) ? "0@#@" : aSegMesProy[nIndice0][1] + "@#@"; //anomes base
            js_args += (nIndiceP1 == null) ? "0@#@" : aSegMesProy[nIndiceP1][1] + "@#@"; //anomes base + 1
            js_args += (nIndiceP2 == null) ? "0@#@" : aSegMesProy[nIndiceP2][1] + "@#@"; //anomes base + 2
            js_args += aSegMesProy[nIndiceIA][1] + "@#@"; //anomes inicio año
            js_args += aSegMesProy[0][1] + "@#@"; //anomes inicio proyecto
            js_args += aSegMesProy[aSegMesProy.length - 1][1] + "@#@"; //anomes fin proyecto

            switch (nNivel) {
                case "3": //Concepto Económico
                    js_args += oFila.getAttribute("G") + "@#@"; //Grupo Económico
                    js_args += oFila.getAttribute("S") + "@#@"; //Subgrupo Económico
                    js_args += oFila.getAttribute("C") + "@#@"; //Concepto Económico
                    //js_args += "0@#@"; //Clase Económica
                    js_args += $I("hdnCualidadProyectoSubNodo").value
                    break;
            }
            iFila = nIndexFila;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
            return;
        }

        //alert("nIndexFila: "+ nIndexFila);
        var tblDatos = $I("tblDatos");
        for (var i = nIndexFila + 1; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O") {
                    tblDatos.rows[i].style.display = "none";
                    if (tblDatos.rows[i].getAttribute("nivel") < 4)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../images/plus2.gif";
                }
                else if (tblDatos.rows[i].getAttribute("nivel") - 1 == nNivel) tblDatos.rows[i].style.display = "table-row";
            } else {
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../images/plus2.gif";
        else oImg.src = "../../../images/minus2.gif";

        if (bMostrar) MostrarTodo();
        else if (bMostrarGrupoNivel) MostrarGrupoNivel(sGrupoExp, sNivelExp);
        else ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar) {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            return;
        }
        var tblDatos = $I("tblDatos");
        if (nMostrar == 0) {//Contraer
            for (var i = 0; i < tblDatos.rows.length; i++) {
                if (tblDatos.rows[i].getAttribute("nivel") > 1) {
                    if (tblDatos.rows[i].getAttribute("nivel") < 4)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../images/plus2.gif";
                    tblDatos.rows[i].style.display = "none";
                }
                else {
                    tblDatos.rows[i].cells[0].children[0].src = "../../../images/plus2.gif";
                }
            }
            ocultarProcesando();
        } else { //Expandir
            MostrarTodo();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar = false;
var nIndiceTodo = -1;
function MostrarTodo() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
            ocultarProcesando();
            return;
        }

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i = nIndiceAux; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].getAttribute("nivel") < nNE) {
                if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus2.gif") > -1) {
                    bMostrar = true;
                    nIndiceTodo = i;
                    mostrar(tblDatos.rows[i].cells[0].children[0]);
                    return;
                }
            }
        }
        bMostrar = false;
        nIndiceTodo = -1;
        aFila = FilasDe("tblDatos");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

/* Función para establecer el nivel de expansión */
var nNE = 0;
function setNE(nValor) {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        sResfreshNiveles = "S";
        nNE = nValor;
        colorearNE(nNE);
        MostrarOcultar(0);
        if (nNE > 1) MostrarOcultar(1);

    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

var bMostrarGrupoNivel = false;
var nIndiceGrupoNivel = -1;
var sGrupoExp = "";
var sNivelExp = "";
function MostrarGrupoNivel(sGrupo, sNivel) {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
            ocultarProcesando();
            return;
        }

        sResfreshNiveles = "N";
        sGrupoExp = sGrupo;
        sNivelExp = sNivel;

        var nIndiceAux = 0;
        if (nIndiceGrupoNivel > -1) nIndiceAux = nIndiceGrupoNivel;
        for (var i = nIndiceAux; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].getAttribute("T") != sGrupo) continue;
            if (tblDatos.rows[i].getAttribute("nivel") <= sNivel - 1) {
                if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus2.gif") > -1) {
                    bMostrarGrupoNivel = true;
                    nIndiceGrupoNivel = i;
                    mostrar(tblDatos.rows[i].cells[0].children[0]);
                    return;
                }
            }
        }
        bMostrarGrupoNivel = false;
        nIndiceGrupoNivel = -1;
        aFila = FilasDe("tblDatos");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

function colorearNE(nValor) {
    try {
        switch (nValor) {
            case -1:
            case 0:
                $I("imgNE1").src = "../../../images/imgNE1off.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 1:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 4:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function getPE() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge";

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");

                    limpiarPantalla();
                    $I("imgCaution").style.display = "none";
                    id_proyectosubnodo_actual = aDatos[0];
                    if (aDatos[1] == "1") {
                        bLectura = true;
                    } else {
                        bLectura = false;
                    }

                    recuperarPSN();
                } else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function getPEByNum() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value);

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");

                    limpiarPantalla();
                    $I("imgCaution").style.display = "none";
                    id_proyectosubnodo_actual = aDatos[0];
                    if (aDatos[1] == "1") {
                        bLectura = true;
                    } else {
                        bLectura = false;
                    }

                    recuperarPSN();
                } else ocultarProcesando();
	        });


    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function getPEByDes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&desPE=" + codpar($I("txtDesPE").value);

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");

                    limpiarPantalla();
                    $I("imgCaution").style.display = "none";
                    id_proyectosubnodo_actual = aDatos[0];
                    if (aDatos[1] == "1") {
                        bLectura = true;
                    } else {
                        bLectura = false;
                    }

                    recuperarPSN();
                } else ocultarProcesando();
	        });


    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos por denominación", e.message);
    }
}

function recuperarPSN() {
    try {
        mostrarProcesando();
        setOp($I("imgME"), 30);
        setOp($I("imgPM"), 30);
        setOp($I("imgAM"), 30);
        setOp($I("imgSM"), 30);
        setOp($I("imgUM"), 30);
        $I("lblMesM2").innerText = "";
        $I("lblMesM1").innerText = "";
        $I("lblMes0").innerText = "";
        $I("lblMes0").style.cursor = "default";
        $I("lblMes0").ondblclick = null;
        $I("lblMesP1").innerText = "";
        $I("lblMesP2").innerText = "";
        $I("lblIA").innerText = "";
        $I("lblIP").innerText = "";
        $I("lblTP").innerText = "";

        //alert("Antes de Hay que recuperar el proyecto: " + id_proyectosubnodo_actual);
        var js_args = "recuperarPSN@#@" + id_proyectosubnodo_actual;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function limpiarPantalla() {
    try {
        document.forms[0].reset();
        borrarCatalogo();
        ocultarDialogos();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al limpiar la pantalla", e.message);
    }
}

function ocultarDialogos() {
    try {
        $I("lblCountDialogosAbiertos").style.visibility = "hidden";
        $I("divCountLeer").style.visibility = "hidden";
        $I("divCountResponder").style.visibility = "hidden";
        $I("divDialogos").style.visibility = "hidden";
        $I("divDialogos").onmouseover = null;
        $I("divDialogos").onmouseout = null;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al limpiar la pantalla", e.message);
    }
}

var nIDTimeGM = 0;
var nCountGM = 0;
function getSegMesProy() {
    try {
        if (nCountGM > 3) {
            clearTimeout(nIDTimeGM);
            nCountGM = 0;
            mmoff("Inf", "No se ha podido determinar el proyecto para la obtención de los meses", 400);
            return;
        }
        if ($I("hdnIdProyectoSubNodo").value == "") {
            nCountGM++;
            clearTimeout(nIDTimeGM);
            //alert("Antes de getSegMesProy - 2");
            nIDTimeProy = setTimeout("getSegMesProy()", 100);
            return;
        }
        nCountGM = 0;

        var js_args = "getMesesProy@#@";
        js_args += $I("hdnIdProyectoSubNodo").value;

        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los meses", e.message);
    }
}

var nColumnaCarrusel;
/* mostrarDetalle */
function md(oFila, nColumnaAux) {
    try {
        //alert("Grupo: "+ oFila.getAttribute("G")+"\nSubgrupo: "+ oFila.getAttribute("S")+"\nConcepto:" + oFila.getAttribute("C")+"\nClase:" + oFila.getAttribute("CL")+"\nTipo:" + oFila.getAttribute("T") +"\nnSegMesProy: "+ aSegMesProy[nIndice0][0]);
        mostrarProcesando();
        var sModal = sSize(1000, 575);

        switch (nColumnaAux) {
            case -2: nColumnaCarrusel = nIndiceM2; break;
            case -1: nColumnaCarrusel = nIndiceM1; break;
            case 0: nColumnaCarrusel = nIndice0; break;
            case 1: nColumnaCarrusel = nIndiceP1; break;
            case 2: nColumnaCarrusel = nIndiceP2; break;
        }

        switch (oFila.getAttribute("T")) {
            case "C":
                switch (oFila.getAttribute("CL")) {
                    case "-1": //Esfuerzos de profesionales.
                    case "-2": //Esfuerzos de profesionales.
                    case "-3": //Esfuerzos de profesionales.
                    case "-4": //Esfuerzos de profesionales.
                    case "-15": //Cesión de profesionales.
                        var sDisponibilidad = 0;
                        if (
                            ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P")
                            && $I("hdnModeloCoste").value == "J"
                            ) sDisponibilidad = 1;

                        var strEnlace = strServer + "Capa_Presentacion/ECO/setEsfuerzos/default.aspx?C=" + oFila.getAttribute("C") + "&CL=" + oFila.getAttribute("CL") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value + "&sDisponibilidad=" + sDisponibilidad + "&sUltCierreEcoNodo=" + sUltCierreEcoNodo;
                        break;
                    case "-11": //Jornadas por nivel.
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setNivel/default.aspx?nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sModeloCoste=" + $I("hdnModeloCoste").value + "&nIdNodo=" + $I("hdnIdNodo").value + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-14": //Producción de Avance. Se muestra en las pantallas de Prod. por prof y perfil
                        sModal = sSize(510, 140);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setGastosFinancieros/default.aspx?nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-13": //Consumos por periodificación
                        sModal = sSize(510, 150);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setConsPeriod/default.aspx?nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    default:  //Incluye el -4 (cesión de consumos DatoEco)
                        sModal = sSize(1000, 575);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=" + oFila.getAttribute("G") + "&S=" + oFila.getAttribute("S") + "&C=" + oFila.getAttribute("C") + "&CL=" + oFila.getAttribute("CL") + "&T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                }
                break;
            case "P":
                switch (oFila.getAttribute("CL")) {
                    case "-17":
                        //Producción de Profesionales.
                        sModal = sSize(1020, 680);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setProdProf/default.aspx?nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-26":
                        //Producción por transferencia
                        var strEnlace = strServer + "Capa_Presentacion/ECO/getProdIngProfRep/default.aspx?T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-18": //Producción de Perfil
                        sModal = sSize(760, 575);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setProdPerfil/default.aspx?nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-19": //Producción de Avance. Se muestra en las pantallas de Prod. por prof y perfil
                        if ($I("hdnAnotarProd").value == "0") {
                            ocultarProcesando();
                            mmoff("Inf", "Salvo excepciones, no se permite anotar producción en\nproyectos cuyo cliente se corresponde con una empresa del Grupo.", 500, 5000, 50);
                            return;
                        }
                        sModal = sSize(510, 150);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setAvance/default.aspx?nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-20": //Producción por periodificación
                        if ($I("hdnAnotarProd").value == "0") {
                            ocultarProcesando();
                            mmoff("Inf", "Salvo excepciones, no se permite anotar producción en\nproyectos cuyo cliente se corresponde con una empresa del Grupo.", 500, 5000, 50);
                            return;
                        }
                        sModal = sSize(510, 150);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setProdPeriod/default.aspx?nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-21": //Otra Producción por transferencia
                        var strEnlace = strServer + "Capa_Presentacion/ECO/getProdIngTransRep/default.aspx?T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    default:
                        sModal = sSize(1000, 575);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=" + oFila.getAttribute("G") + "&S=" + oFila.getAttribute("S") + "&C=" + oFila.getAttribute("C") + "&CL=" + oFila.getAttribute("CL") + "&T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                }
                break;
            case "I":
                switch (oFila.getAttribute("CL")) {
                    case "-22": //Ingresos por dedicaciones (esfuerzos)
                    case "-24": //Ingresos empresas grupo por dedicaciones (esfuerzos)
                        var strEnlace = strServer + "Capa_Presentacion/ECO/getProdIngProfRep/default.aspx?T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    case "-23": //Ingresos por dedicaciones (esfuerzos)
                    case "-25": //Ingresos empresas grupo por dedicaciones (esfuerzos)
                        var strEnlace = strServer + "Capa_Presentacion/ECO/getProdIngTransRep/default.aspx?T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                        break;
                    default:
                        sModal = sSize(1000, 575);
                        var strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=" + oFila.getAttribute("G") + "&S=" + oFila.getAttribute("S") + "&C=" + oFila.getAttribute("C") + "&CL=" + oFila.getAttribute("CL") + "&T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                }
                break;
            case "O":
                sModal = sSize(1000, 575);
                var strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=" + oFila.getAttribute("G") + "&S=" + oFila.getAttribute("S") + "&C=" + oFila.getAttribute("C") + "&CL=" + oFila.getAttribute("CL") + "&T=" + oFila.getAttribute("T") + "&nSegMesProy=" + aSegMesProy[nColumnaCarrusel][0] + "&sEstadoMes=" + aSegMesProy[nColumnaCarrusel][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
        }
        //var ret = window.showModalDialog(strEnlace, self, sModal);
        modalDialog.Show(strEnlace, self, sModal)
	        .then(function(ret) {
                if (ret != null) {
                    if (ret == "OK") {
                        bBuscarReplica = true;
                        colorearMeses();
                        fActualizarArbol();
                        getResumen();
                        ocultarProcesando();
                    }
                    else ocultarProcesando();
                }
                else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el detalle del concepto económico", e.message);
    }
}

function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Grupo / Subgrupo / Concepto / Clase </TD>"); //&nbsp;&nbsp;("+ $I("lblMes0").innerText +")

        for (var i = 1; i < 8; i++) {
            if (i == 3 || i == 5) continue;
            switch (tblTitulo.rows[0].cells[i].getElementsByTagName("label")[0].className) {
                case "textoR2": sb.Append("        <td style='width:auto;color:#cc0000;background-color: #BCD4DF;'>&nbsp;" + tblTitulo.rows[0].cells[i].innerText + "</td>"); break;
                case "textoV": sb.Append("        <td style='width:auto;color:#009900;background-color: #BCD4DF;'>&nbsp;" + tblTitulo.rows[0].cells[i].innerText + "</td>"); break;
                case "textoB": sb.Append("        <td style='width:auto;color:White;background-color: #BCD4DF;'>&nbsp;" + tblTitulo.rows[0].cells[i].innerText + "</td>"); break;
            }
        }
        for (var i = 8; i < 11; i++)
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>&nbsp;" + tblTitulo.rows[0].cells[i].innerText + "</td>");

        sb.Append("	</TR>");
        var bNegrita = false;
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].style.display == "none") continue;
            bNegrita = false;
            sb.Append("<tr>");
            sb.Append("<td>");

            switch (tblDatos.rows[i].getAttribute("nivel")) {
                case "1": sb.Append("&nbsp;"); bNegrita = true; break;
                case "2": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                case "3": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                case "4": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
            }
            sb.Append(tblDatos.rows[i].cells[0].innerText);
            sb.Append("</td>");
            for (var x = 1; x < 9; x++) {
                if (tblDatos.rows[i].cells[x].className.indexOf("textoR2") > -1) sb.Append("<td style='color:#cc0000'>");
                else sb.Append("<td>");

                if (bNegrita) sb.Append("<b>");
                sb.Append(tblDatos.rows[i].cells[x].innerText);
                if (bNegrita) sb.Append("</b>");
                sb.Append("</td>");
            }

            sb.Append("</tr>");
        }

        var aFilaRes = FilasDe("tblResultado");
        for (var i = 0; i < aFilaRes.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;Margen de contribución</td>");
            for (var x = 1; x <= 8; x++) {

                var aInput = aFilaRes[i].cells[x].getElementsByTagName("INPUT");
                sb.Append("<td style='text-align:right;background-color: #BCD4DF;");
                if (aFilaRes[i].cells[x].children[0].style.color == "#cc0000")
                    sb.Append("color:#cc0000;");
                sb.Append("'>");

                switch (x) {
                    case 1: if (nIndiceM2 != null) sb.Append(aFilaRes[i].cells[x].children[0].innerText); break;
                    case 2: if (nIndiceM1 != null) sb.Append(aFilaRes[i].cells[x].children[0].innerText); break;
                    case 4: if (nIndiceP1 != null) sb.Append(aFilaRes[i].cells[x].children[0].innerText); break;
                    case 5: if (nIndiceP2 != null) sb.Append(aFilaRes[i].cells[x].children[0].innerText); break;
                    default: sb.Append(aFilaRes[i].cells[x].children[0].innerText); break;
                }

                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        var aFilaRent = FilasDe("tblRentabilidad");
        for (var i = 0; i < aFilaRent.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;Rentabilidad</td>");
            for (var x = 1; x <= 8; x++) {

                var aInput = aFilaRent[i].cells[x].getElementsByTagName("INPUT");
                sb.Append("<td style='text-align:right;background-color: #BCD4DF;");
                if (aFilaRent[i].cells[x].children[0].style.color == "#cc0000")
                    sb.Append("color:#cc0000;");
                sb.Append("'>");

                switch (x) {
                    case 1: if (nIndiceM2 != null) sb.Append(aFilaRent[i].cells[x].children[0].innerText); break;
                    case 2: if (nIndiceM1 != null) sb.Append(aFilaRent[i].cells[x].children[0].innerText); break;
                    case 4: if (nIndiceP1 != null) sb.Append(aFilaRent[i].cells[x].children[0].innerText); break;
                    case 5: if (nIndiceP2 != null) sb.Append(aFilaRent[i].cells[x].children[0].innerText); break;
                    default: sb.Append(aFilaRent[i].cells[x].children[0].innerText); break;
                }

                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        var aFilaIN = FilasDe("tblIngNetos");
        for (var i = 0; i < aFilaIN.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;Ingresos netos</td>");
            for (var x = 1; x <= 8; x++) {

                var aInput = aFilaIN[i].cells[x].getElementsByTagName("INPUT");
                sb.Append("<td style='text-align:right;background-color: #BCD4DF;");
                if (aFilaIN[i].cells[x].children[0].style.color == "#cc0000")
                    sb.Append("color:#cc0000;");
                sb.Append("'>");

                switch (x) {
                    case 1: if (nIndiceM2 != null) sb.Append(aFilaIN[i].cells[x].children[0].innerText); break;
                    case 2: if (nIndiceM1 != null) sb.Append(aFilaIN[i].cells[x].children[0].innerText); break;
                    case 4: if (nIndiceP1 != null) sb.Append(aFilaIN[i].cells[x].children[0].innerText); break;
                    case 5: if (nIndiceP2 != null) sb.Append(aFilaIN[i].cells[x].children[0].innerText); break;
                    default: sb.Append(aFilaIN[i].cells[x].children[0].innerText); break;
                }

                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        var aFilaOC = FilasDe("tblObraCurso");
        for (var i = 0; i < aFilaOC.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;Obra en curso</td>");
            for (var x = 1; x <= 8; x++) {

                var aInput = aFilaOC[i].cells[x].getElementsByTagName("INPUT");
                sb.Append("<td style='text-align:right;background-color: #BCD4DF;");
                if (aFilaOC[i].cells[x].children[0].style.color == "#cc0000")
                    sb.Append("color:#cc0000;");
                sb.Append("'>");

                switch (x) {
                    case 1: if (nIndiceM2 != null) sb.Append(aFilaOC[i].cells[x].children[0].innerText); break;
                    case 2: if (nIndiceM1 != null) sb.Append(aFilaOC[i].cells[x].children[0].innerText); break;
                    case 4: if (nIndiceP1 != null) sb.Append(aFilaOC[i].cells[x].children[0].innerText); break;
                    case 5: if (nIndiceP2 != null) sb.Append(aFilaOC[i].cells[x].children[0].innerText); break;
                    default: sb.Append(aFilaOC[i].cells[x].children[0].innerText); break;
                }

                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        var aFilaSC = FilasDe("tblSaldoClientes");
        for (var i = 0; i < aFilaSC.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;Saldo de clientes</td>");
            for (var x = 1; x <= 8; x++) {

                var aInput = aFilaSC[i].cells[x].getElementsByTagName("INPUT");
                sb.Append("<td style='text-align:right;background-color: #BCD4DF;");
                if (aFilaSC[i].cells[x].children[0].style.color == "#cc0000")
                    sb.Append("color:#cc0000;");
                sb.Append("'>");

                switch (x) {
                    case 1: if (nIndiceM2 != null) sb.Append(aFilaSC[i].cells[x].children[0].innerText); break;
                    case 2: if (nIndiceM1 != null) sb.Append(aFilaSC[i].cells[x].children[0].innerText); break;
                    case 4: if (nIndiceP1 != null) sb.Append(aFilaSC[i].cells[x].children[0].innerText); break;
                    case 5: if (nIndiceP2 != null) sb.Append(aFilaSC[i].cells[x].children[0].innerText); break;
                    default: sb.Append(aFilaSC[i].cells[x].children[0].innerText); break;
                }

                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        //sb.Append("<tr><td colspan='" + tblDatos.rows[0].cells.length + "' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");
        sb.Append("<tr style='vertical-align:top;'>");
        sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");
        for (var j = 2; j <= tblDatos.rows[0].cells.length; j++) {
            sb.Append("<td></td>");
        }
        sb.Append("</tr>");
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function insertarmes() {
    try {
        mostrarProcesando();
        var nMesACrear = getPMACrear();
        var nAnomesMinimo = FechaAAnnomes(AnnomesAFecha(parseInt(sUltCierreEcoNodo, 10)).add("mo", 1));

        var strEnlace = strServer + "Capa_Presentacion/ECO/getPeriodo.aspx?sDesde=" + nMesACrear + "&sHasta=" + nMesACrear + "&sAnomesMinimo=" + nAnomesMinimo + "&sOpValidacion=1";
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    bBuscarReplica = false;
                    var aDatos = ret.split("@#@");
                    var js_args = "addMesesProy@#@";
                    js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                    js_args += aDatos[0] + "@#@";
                    js_args += aDatos[1];

                    RealizarCallBack(js_args, "");
                } else ocultarProcesando();
	        });

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes", e.message);
    }
}

/* posicionarse mes estrella */
function setME() {
    try {
        if (getOp($I("imgME")) != 100) return;
        if (aSegMesProy.length == 0) return;

        var nIndiceAbierto = getPMA();
        if (nIndiceAbierto == -1) return;
        if (nIndiceAbierto == nIndice0) return;

        mostrarProcesando();
        nIndice0 = nIndiceAbierto;

        if (nIndiceAbierto > 0) nIndiceM1 = nIndiceAbierto - 1;
        else nIndiceM1 = null;
        if (nIndiceAbierto > 1) nIndiceM2 = nIndiceAbierto - 2;
        else nIndiceM2 = null;

        if (nIndiceAbierto < aSegMesProy.length - 1) nIndiceP1 = nIndiceAbierto + 1;
        else nIndiceP1 = null;
        if (nIndiceAbierto < aSegMesProy.length - 2) nIndiceP2 = nIndiceAbierto + 2;
        else nIndiceP2 = null;

        //setBotonBorrar();

        colorearMeses();
        fActualizarArbol();

        bRefrescarDatosContrato = false;
        getResumen();

    } catch (e) {
        mostrarErrorAplicacion("Error al posicionar el primer mes abierto", e.message);
    }
}

function getReplica() {
    try {
        if (aSegMesProy.length == 0 || typeof (aSegMesProy[nIndice0]) == "undefined") {
            return;
        }
        var js_args = "getReplica@#@";
        js_args += aSegMesProy[nIndice0][1] + "@#@";
        js_args += dfn($I("txtNumPE").value);

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la necesidad de replicar el proyecto", e.message);
    }
}


function replica() {
    try {
        location.href = "../Replica/Default.aspx?nProy=" + dfn($I("txtNumPE").value) + "&sProy=" + codpar($I("txtDesPE").value) + "&sAnomes=" + aSegMesProy[nIndice0][1] + "&nPSN=" + $I("hdnIdProyectoSubNodo").value + "&origen=carrusel&opcion=replicar";
    } catch (e) {
        mostrarErrorAplicacion("Error al pulsar el botón de \"Replicar\"", e.message);
    }
}

/* Obtener el índice del primer mes abierto */
function getPMA() {
    try {
        var nIndice = -1;
        for (var i = 0; i < aSegMesProy.length; i++) {
            if (aSegMesProy[i][2] == "A") {
                nIndice = i;
                break;
            }
        }
        return nIndice;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el índice del primer mes abierto.", e.message);
    }
}

/* Obtener el añomes del primer mes que no exista, que sea posterior al último mes económico cerrado del nodo */
function getPMACrear() {
    try {
        var oFecha = new Date();
        var nAnnoMesActual = oFecha.getFullYear() * 100 + oFecha.getMonth() + 1;
        var nMesInsertarAux = FechaAAnnomes(AnnomesAFecha(parseInt(sUltCierreEcoNodo, 10)).add("mo", 1));

        nMesInsertarAux = (nAnnoMesActual > nMesInsertarAux) ? nAnnoMesActual : nMesInsertarAux;

        for (var i = 0; i < aSegMesProy.length; i++) {
            if (nMesInsertarAux < parseInt(aSegMesProy[i][1], 10)) {
                break;
            }
            if (nMesInsertarAux > parseInt(aSegMesProy[i][1], 10)) {
                continue;
            }
            if (parseInt(aSegMesProy[i][1], 10) >= nMesInsertarAux) {
                nMesInsertarAux = FechaAAnnomes(AnnomesAFecha(nMesInsertarAux).add("mo", 1));
            }
        }
        return nMesInsertarAux;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el mes a insertar.", e.message);
    }
}

function cerrarmes() {
    try {
        //location.href = "../Replica/Default.aspx?nProy=" + dfn($I("txtNumPE").value) + "&sProy=" + $I("txtDesPE").value + "&sAnomes=" + aSegMesProy[nIndice0][1] + "&nPSN=" + $I("hdnIdProyectoSubNodo").value + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&origen=carrusel&opcion=cerrarmes";
        location.href = "../Replica/Default.aspx?nProy=" + dfn($I("txtNumPE").value)
            + "&sAnomes=" + aSegMesProy[nIndice0][1]
            + "&nPSN=" + $I("hdnIdProyectoSubNodo").value
            + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value
            + "&origen=carrusel&opcion=cerrarmes"
            + "&sProy=" + codpar($I("txtDesPE").value);

    } catch (e) {
        mostrarErrorAplicacion("Error al pulsar el botón de \"Replicar\"", e.message);
    }
}

function getCierre() {
    try {
        var nIndiceMes = getPMA();
        if (nIndiceMes == -1) {
            $I("imgCaution").style.display = "none";
            return;
        }
        if (parseInt(aSegMesProy[nIndiceMes][1], 10) < nAnoMesActual) {
            $I("imgCaution").style.display = "block";
            $I("imgCaution").title = "Necesario cerrar mes";
        } else {
            if (bBuscarReplica == true && $I("hdnCualidadProyectoSubNodo").value == "C") {
                getReplica();
            } //else $I("imgCaution").style.display = "none";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar si es necesario cerrar el mes", e.message);
    }
}

function getTotalesProyecto() {
    try {
        var tblDatos = $I("tblDatos");
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].getAttribute("G") == "2") {
                $I("txtMrgProyecto").value = $I("txtTotTP").innerText;
                if (tblDatos.rows[i].cells[8].innerText != "0,00") {
                    $I("txtTotProyecto").value = tblDatos.rows[i].cells[8].innerText;
                    $I("txtRentProyecto").value = (parseFloat(dfn($I("txtMrgProyecto").value)) * 100 / parseFloat(dfn($I("txtTotProyecto").value))).ToString("N");
                } else {
                    $I("txtTotProyecto").value = "0,00";
                    $I("txtRentProyecto").value = "0,00";
                }
                break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los totales del proyecto", e.message);
    }
}

function setVisibilidadIconos() {
    try {
        setOp($I("imgConsumo3x"), 100);
        $I("imgConsumo3x").onclick = function() { mostrarNivelAux('C', 3) };
        $I("imgConsumo3x").style.cursor = "pointer";
        setOp($I("imgProduccion3x"), 100);
        $I("imgProduccion3x").onclick = function() { mostrarNivelAux('P', 3) };
        $I("imgProduccion3x").style.cursor = "pointer";
        setOp($I("imgIngresos3x"), 100);
        $I("imgIngresos3x").onclick = function() { mostrarNivelAux('I', 3) };
        $I("imgIngresos3x").style.cursor = "pointer";
        setOp($I("imgCobros3x"), 100);
        $I("imgCobros3x").onclick = function() { mostrarNivelAux('O', 3) };
        $I("imgCobros3x").style.cursor = "pointer";
        setOp($I("imgConsumo"), 100);
        $I("imgConsumo").onclick = function() { accesoDirecto(1) };
        $I("imgConsumo").style.cursor = "pointer";
        setOp($I("imgProduccion"), 100);
        $I("imgProduccion").onclick = function() { accesoDirecto(2) };
        $I("imgProduccion").style.cursor = "pointer";
        setOp($I("imgIngresos"), 100);
        $I("imgIngresos").onclick = function() { accesoDirecto(3) };
        $I("imgIngresos").style.cursor = "pointer";
        setOp($I("imgCobros"), 100);
        $I("imgCobros").onclick = function() { accesoDirecto(4) };
        $I("imgCobros").style.cursor = "pointer";
        setOp($I("imgConsumo4x"), 100);
        $I("imgConsumo4x").onclick = function() { mostrarNivelAux('C', 4) };
        $I("imgConsumo4x").style.cursor = "pointer";
        setOp($I("imgProduccion4x"), 100);
        $I("imgProduccion4x").onclick = function() { mostrarNivelAux('P', 4) };
        $I("imgProduccion4x").style.cursor = "pointer";
        setOp($I("imgIngresos4x"), 100);
        $I("imgIngresos4x").onclick = function() { mostrarNivelAux('I', 4) };
        $I("imgIngresos4x").style.cursor = "pointer";
        setOp($I("imgCobros4x"), 100);
        $I("imgCobros4x").onclick = function() { mostrarNivelAux('O', 4) };
        $I("imgCobros4x").style.cursor = "pointer";
        setOp($I("imgConsumoProf"), 100);
        $I("imgConsumoProf").onclick = function() { accesoDirecto(5) };
        $I("imgConsumoProf").style.cursor = "pointer";
        setOp($I("imgProduccionProf"), 100);
        $I("imgProduccionProf").onclick = function() { accesoDirecto(6) };
        $I("imgProduccionProf").style.cursor = "pointer";
        setOp($I("imgConsumoNivel"), 100);
        $I("imgConsumoNivel").onclick = function() { accesoDirecto(7) };
        $I("imgConsumoNivel").style.cursor = "pointer";
        setOp($I("imgProduccionPerfil"), 100);
        $I("imgProduccionPerfil").onclick = function() { accesoDirecto(8) };
        $I("imgProduccionPerfil").style.cursor = "pointer";
        setOp($I("imgImpGasvi"), 100);
        $I("imgImpGasvi").onclick = function() { accesoDirecto(9) };
        $I("imgImpGasvi").style.cursor = "pointer";

        switch ($I("hdnCualidadProyectoSubNodo").value) {
            case "J":
                setOp($I("imgConsumoNivel"), 30);
                $I("imgConsumoNivel").onclick = null;
                $I("imgConsumoNivel").style.cursor = "not-allowed";
                setOp($I("imgProduccionPerfil"), 30);
                $I("imgProduccionPerfil").onclick = null;
                $I("imgProduccionPerfil").style.cursor = "not-allowed";
                break;
            case "P":
                setOp($I("imgProduccionProf"), 30);
                $I("imgProduccionProf").onclick = null;
                $I("imgProduccionProf").style.cursor = "not-allowed";
                setOp($I("imgConsumoNivel"), 30);
                $I("imgConsumoNivel").onclick = null;
                $I("imgConsumoNivel").style.cursor = "not-allowed";
                setOp($I("imgProduccionPerfil"), 30);
                $I("imgProduccionPerfil").onclick = null;
                $I("imgProduccionPerfil").style.cursor = "not-allowed";
                break;
        }

        if (sHayGrupoC == "0") {
            setOp($I("imgConsumo3x"), 30);
            $I("imgConsumo3x").onclick = null;
            $I("imgConsumo3x").style.cursor = "not-allowed";
            setOp($I("imgConsumo"), 30);
            $I("imgConsumo").onclick = null;
            $I("imgConsumo").style.cursor = "not-allowed";
            setOp($I("imgConsumo4x"), 30);
            $I("imgConsumo4x").onclick = null;
            $I("imgConsumo4x").style.cursor = "not-allowed";
            setOp($I("imgConsumoProf"), 30);
            $I("imgConsumoProf").onclick = null;
            $I("imgConsumoProf").style.cursor = "not-allowed";
        }
        if (sHayGrupoP == "0") {
            setOp($I("imgProduccion3x"), 30);
            $I("imgProduccion3x").onclick = null;
            $I("imgProduccion3x").style.cursor = "not-allowed";
            setOp($I("imgProduccion"), 30);
            $I("imgProduccion").onclick = null;
            $I("imgProduccion").style.cursor = "not-allowed";
            setOp($I("imgProduccion4x"), 30);
            $I("imgProduccion4x").onclick = null;
            $I("imgProduccion4x").style.cursor = "not-allowed";
            setOp($I("imgProduccionProf"), 30);
            $I("imgProduccionProf").onclick = null;
            $I("imgProduccionProf").style.cursor = "not-allowed";
            setOp($I("imgProduccionPerfil"), 30);
            $I("imgProduccionPerfil").onclick = null;
            $I("imgProduccionPerfil").style.cursor = "not-allowed";
            setOp($I("imgImpGasvi"), 30);
            $I("imgImpGasvi").onclick = null;
            $I("imgImpGasvi").style.cursor = "not-allowed";
        }
        if (sHayGrupoI == "0") {
            setOp($I("imgIngresos3x"), 30);
            $I("imgIngresos3x").onclick = null;
            $I("imgIngresos3x").style.cursor = "not-allowed";
            setOp($I("imgIngresos"), 30);
            $I("imgIngresos").onclick = null;
            $I("imgIngresos").style.cursor = "not-allowed";
            setOp($I("imgIngresos4x"), 30);
            $I("imgIngresos4x").onclick = null;
            $I("imgIngresos4x").style.cursor = "not-allowed";
        }
        if (sHayGrupoO == "0") {
            setOp($I("imgCobros3x"), 30);
            $I("imgCobros3x").onclick = null;
            $I("imgCobros3x").style.cursor = "not-allowed";
            setOp($I("imgCobros"), 30);
            $I("imgCobros").onclick = null;
            $I("imgCobros").style.cursor = "not-allowed";
            setOp($I("imgCobros4x"), 30);
            $I("imgCobros4x").onclick = null;
            $I("imgCobros4x").style.cursor = "not-allowed";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la visibilidad de los iconos", e.message);
    }
}

function borrarmes() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto.", 250);
            return;
        }
        if (aSegMesProy.length == 0) {
            mmoff("Inf", "No existe mes para borrar.", 200);
            return;
        }
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/BorrarMes/default.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sSize(650, 520));
        modalDialog.Show(strEnlace, self,sSize(650, 520))
	        .then(function(ret) {
                if (ret != null) {
                    nPrimerMesInsertado = 0;
                    getSegMesProy();
                }
                else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a borrar el mes.", e.message);
    }
}

function buscarPE() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        setNumPE(1);

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
function buscarDesPE() {
    try {
        var js_args = "buscarDesPE@#@";
        js_args += Utilidades.escape($I("txtDesPE").value);
        setNumPE(2);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
function setDesPE() {
    try {
        borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir la denominación de proyecto", e.message);
    }
}

var bLimpiarDatos = true;
function setNumPE(iTipo) {
    try {
        if (bLimpiarDatos) {
            borrarCatalogo();

            sUltCierreEcoNodo = null;
            nCosteNaturaleza = null;
            bBuscarReplica = true;
            nIndiceM2 = null;
            nIndiceM1 = null;
            nIndice0 = null;
            nIndiceP1 = null;
            nIndiceP2 = null;
            nIndiceIA = null;

            if (iTipo == 1)//Estoy tecleando un mº de proyecto
                $I("txtDesPE").value = "";
            else//Estpy teclenado una denominación de proyecto
                $I("txtNumPE").value = "";
            $I("fstContrato").style.visibility = "hidden";
            $I("divCualidadPSN").style.visibility = "hidden";
            $I("divCualidadPSN").onmouseover = null;
            $I("divCualidadPSN").onmouseout = null;
            $I("imgEstado").src = "../../../Images/imgSeparador.gif";
            $I("imgEstado").onmouseover = null;
            $I("imgEstado").onmouseout = null;
            $I("imgCaution").style.display = "none";
            $I("txtCliente").value = "";
            $I("txtCliente").onmouseover = null;
            $I("txtCliente").onmouseout = null;
            ocultarDialogos();
            $I("lgdContrato").innerText = "";
            $I("txtTotContrato").value = "";
            $I("txtMrgContrato").value = "";
            $I("txtRentContrato").value = "";
            $I("txtProducido").value = "";
            $I("lblTPPAC").className = "texto";
            $I("lblTPPAC").onclick = null;
            $I("txtResponsable").value = "";
            $I("txtMrgProyecto").value = "";
            $I("txtTotProyecto").value = "";
            $I("txtRentProyecto").value = "";

            $I("lblMesM2").innerText = "";
            $I("lblMesM1").innerText = "";
            $I("lblMes0").innerText = "";
            $I("lblMes0").style.cursor = "default";
            $I("lblMes0").ondblclick = null;
            $I("lblMesP1").innerText = "";
            $I("lblMesP2").innerText = "";
            $I("lblIA").innerText = "";
            $I("lblIP").innerText = "";
            $I("lblTP").innerText = "";

            $I("txtTotM2").value = "0,00";
            $I("txtTotM2").style.color = "black";
            $I("txtTotM1").value = "0,00";
            $I("txtTotM1").style.color = "black";
            $I("txtTot0").value = "0,00";
            $I("txtTot0").style.color = "black";
            $I("txtTotP1").value = "0,00";
            $I("txtTotP1").style.color = "black";
            $I("txtTotP2").value = "0,00";
            $I("txtTotP2").style.color = "black";
            $I("txtTotIA").value = "0,00";
            $I("txtTotIA").style.color = "black";
            $I("txtTotIP").value = "0,00";
            $I("txtTotIP").style.color = "black";
            $I("txtTotTP").value = "0,00";
            $I("txtTotTP").style.color = "black";

            $I("txtINM2").value = "0,00";
            $I("txtINM2").style.color = "black";
            $I("txtINM1").value = "0,00";
            $I("txtINM1").style.color = "black";
            $I("txtIN0").value = "0,00";
            $I("txtIN0").style.color = "black";
            $I("txtINP1").value = "0,00";
            $I("txtINP1").style.color = "black";
            $I("txtINP2").value = "0,00";
            $I("txtINP2").style.color = "black";
            $I("txtINIA").value = "0,00";
            $I("txtINIA").style.color = "black";
            $I("txtINIP").value = "0,00";
            $I("txtINIP").style.color = "black";
            $I("txtINTP").value = "0,00";
            $I("txtINTP").style.color = "black";

            $I("txtRM2").value = "0,00";
            $I("txtRM2").style.color = "black";
            $I("txtRM1").value = "0,00";
            $I("txtRM1").style.color = "black";
            $I("txtR0").value = "0,00";
            $I("txtR0").style.color = "black";
            $I("txtRP1").value = "0,00";
            $I("txtRP1").style.color = "black";
            $I("txtRP2").value = "0,00";
            $I("txtRP2").style.color = "black";
            $I("txtRIA").value = "0,00";
            $I("txtRIA").style.color = "black";
            $I("txtRIP").value = "0,00";
            $I("txtRIP").style.color = "black";
            $I("txtRTP").value = "0,00";
            $I("txtRTP").style.color = "black";

            $I("txtOCM2").value = "0,00";
            $I("txtOCM2").style.color = "black";
            $I("txtOCM1").value = "0,00";
            $I("txtOCM1").style.color = "black";
            $I("txtOC0").value = "0,00";
            $I("txtOC0").style.color = "black";
            $I("txtOCP1").value = "0,00";
            $I("txtOCP1").style.color = "black";
            $I("txtOCP2").value = "0,00";
            $I("txtOCP2").style.color = "black";
            $I("txtOCIA").value = "0,00";
            $I("txtOCIA").style.color = "black";
            $I("txtOCIP").value = "0,00";
            $I("txtOCIP").style.color = "black";
            $I("txtOCTP").value = "0,00";
            $I("txtOCTP").style.color = "black";

            $I("txtSCM2").value = "0,00";
            $I("txtSCM2").style.color = "black";
            $I("txtSCM1").value = "0,00";
            $I("txtSCM1").style.color = "black";
            $I("txtSC0").value = "0,00";
            $I("txtSC0").style.color = "black";
            $I("txtSCP1").value = "0,00";
            $I("txtSCP1").style.color = "black";
            $I("txtSCP2").value = "0,00";
            $I("txtSCP2").style.color = "black";
            $I("txtSCIA").value = "0,00";
            $I("txtSCIA").style.color = "black";
            $I("txtSCIP").value = "0,00";
            $I("txtSCIP").style.color = "black";
            $I("txtSCTP").value = "0,00";
            $I("txtSCTP").style.color = "black";

            $I("lblMonedaImportes").innerText = "";

            bLimpiarDatos = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}
function borrarCatalogo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
        $I("txtTotM2").innerText = "";
        $I("txtTotM1").innerText = "";
        $I("txtTot0").innerText = "";
        $I("txtTotP1").innerText = "";
        $I("txtTotP2").innerText = "";
        $I("txtTotIA").innerText = "";
        $I("txtTotIP").innerText = "";
        $I("txtTotTP").innerText = "";

        $I("txtINM2").innerText = "";
        $I("txtINM1").innerText = "";
        $I("txtIN0").innerText = "";
        $I("txtINP1").innerText = "";
        $I("txtINP2").innerText = "";
        $I("txtINIA").innerText = "";
        $I("txtINIP").innerText = "";
        $I("txtINTP").innerText = "";

        $I("txtRM2").innerText = "";
        $I("txtRM1").innerText = "";
        $I("txtR0").innerText = "";
        $I("txtRP1").innerText = "";
        $I("txtRP2").innerText = "";
        $I("txtRIA").innerText = "";
        $I("txtRIP").innerText = "";
        $I("txtRTP").innerText = "";

        $I("txtOCM2").innerText = "";
        $I("txtOCM1").innerText = "";
        $I("txtOC0").innerText = "";
        $I("txtOCP1").innerText = "";
        $I("txtOCP2").innerText = "";
        $I("txtOCIA").innerText = "";
        $I("txtOCIP").innerText = "";
        $I("txtOCTP").innerText = "";

        $I("txtSCM2").innerText = "";
        $I("txtSCM1").innerText = "";
        $I("txtSC0").innerText = "";
        $I("txtSCP1").innerText = "";
        $I("txtSCP2").innerText = "";
        $I("txtSCIA").innerText = "";
        $I("txtSCIP").innerText = "";
        $I("txtSCTP").innerText = "";

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function setMes() {
    try {
        var oNF, oNC;
        var tblMeses = $I("tblMeses");
        for (var i = tblMeses.rows.length - 1; i >= 0; i--)
            tblMeses.deleteRow(i);
        for (var i = 0; i < aSegMesProy.length; i++) {
            oNF = tblMeses.insertRow(-1);
            oNF.style.height = "16px";
            oNF.className = "MA";
            oNF.ondblclick = function() { seleccionarMes(this.rowIndex) };
            oNC = oNF.insertCell(-1);
            oNC.style.color = (aSegMesProy[i][2] == "A") ? "#009900" : "#cc0000";
            oNC.innerText = AnoMesToMesAnoDescLong(aSegMesProy[i][1]);
        }
        $I("divCatalogoMeses").scrollTop = nIndice0 * 16;
        $I("divMeses").style.visibility = "visible";
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el mes.", e.message);
    }
}

function seleccionarMes(nIndiceFila) {
    try {
        //alert(nIndiceFila);
        for (var i = 0; i < aSegMesProy.length; i++) {
            if (i == nIndiceFila) {
                if (i > 1) nIndiceM2 = i - 2;
                else nIndiceM2 = null;
                if (i > 0) nIndiceM1 = i - 1;
                else nIndiceM1 = null;
                nIndice0 = i;
                if (i < aSegMesProy.length - 1) nIndiceP1 = i + 1;
                else nIndiceP1 = null;
                if (i < aSegMesProy.length - 2) nIndiceP2 = i + 2;
                else nIndiceP2 = null;

                sw = 1;
                break;
            }
        }
        sBorradoConfirmado = "0";

        //setBotonBorrar();
        setBotonCerrar();

        colorearMeses();
        fActualizarArbol();

        bRefrescarDatosContrato = false;
        getResumen();
        $I("divMeses").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function accesoDirecto(nOpcion) {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto", 250);
            return;
        }
        if (nIndice0 == null) {
            mmoff("Inf", "Mes central vacío", 200);
            return;
        }
        mostrarProcesando();
        var sModal = "";
        var strEnlace = "";
        nColumnaCarrusel = nIndice0;
        switch (nOpcion) {
            case 1:
                sModal = sSize(1000, 575);
                strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=1&S=0&C=0&CL=0&T=C&nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
            case 2:
                sModal = sSize(1000, 575);
                strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=2&S=0&C=0&CL=0&T=P&nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
            case 3:
                sModal = sSize(1000, 575);
                strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=3&S=0&C=0&CL=0&T=I&nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
            case 4:
                sModal = sSize(1000, 575);
                strEnlace = strServer + "Capa_Presentacion/ECO/setDatosEco/default.aspx?G=4&S=0&C=0&CL=0&T=O&nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + $I("hdnAnnoPIG").value + "&sEsReplicable=" + $I("hdnEsReplicable").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
            case 5:
                sModal = sSize(1000, 575);
                var sDisponibilidad = 0;
                if (
                            ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P")
                            && $I("hdnModeloCoste").value == "J"
                            ) sDisponibilidad = 1;
                strEnlace = strServer + "Capa_Presentacion/ECO/setEsfuerzos/default.aspx?C=0&nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value + "&sDisponibilidad=" + sDisponibilidad + "&sUltCierreEcoNodo=" + sUltCierreEcoNodo;
                break;
            case 6:
                if ($I("hdnCualidadProyectoSubNodo").value == "J") {
                    sModal = sSize(1000, 575);
                    strEnlace = strServer + "Capa_Presentacion/ECO/getProdIngProfRep/default.aspx?T=P&nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sCualidad=" + $I("hdnCualidadProyectoSubNodo").value + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                } else {
                    sModal = sSize(1020, 680);
                    strEnlace = strServer + "Capa_Presentacion/ECO/setProdProf/default.aspx?nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                }
                break;
            case 7:
                sModal = sSize(1000, 575);
                strEnlace = strServer + "Capa_Presentacion/ECO/setNivel/default.aspx?nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sModeloCoste=" + $I("hdnModeloCoste").value + "&nIdNodo=" + $I("hdnIdNodo").value + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
            case 8:
                sModal = sSize(760, 575);
                strEnlace = strServer + "Capa_Presentacion/ECO/setProdPerfil/default.aspx?nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
            case 9:
                sModal = sSize(1000, 575);
                strEnlace = strServer + "Capa_Presentacion/ECO/setImpGasvi/default.aspx?nSegMesProy=" + aSegMesProy[nIndice0][0] + "&sEstadoMes=" + aSegMesProy[nIndice0][2] + "&sEstadoProy=" + sEstado + "&nPSN=" + $I("hdnIdProyectoSubNodo").value;
                break;
            case 10:
                strEnlace = strServer + "Capa_Presentacion/ECO/AvanceDetalle/Default.aspx?";
                strEnlace += "nPSN=" + $I("hdnIdProyectoSubNodo").value;
                strEnlace += "&nPE=" + dfn($I("txtNumPE").value);
                strEnlace += "&sPE=" + codpar($I("txtDesPE").value);
                if (!bLectura || (es_administrador == "SA" || es_administrador == "A"))
                    strEnlace += "&ML=0";
                else
                    strEnlace += "&ML=1";
                strEnlace += "&idNodo=" + $I("hdnIdNodo").value;
                strEnlace += "&sAnoMes=" + aSegMesProy[nIndice0][1];
                strEnlace += "&estado=" + sEstado;
                strEnlace += "&origen=C"; //Carrusel

                var sModal = sSize(1280, 990);
                if (bRes1024) sModal = sSize(1020, 735);

                break;
        }
        //var ret = window.showModalDialog(strEnlace, self, sModal);
        modalDialog.Show(strEnlace, self, sModal)
	        .then(function(ret) {
	            if (ret != null) {
	                ocultarProcesando();
                    if (nOpcion == 10) {
                        var aDatos = ret.split("@#@");
                        //if (aDatos[0] != aSegMesProy[nIndice0][1] || aDatos[1] == "T"){//si se ha cambiado de mes o datos del mes.
                        if (aDatos[1] == "T") {//si se ha cambiado de mes o datos del mes.
                            colorearMeses();
                            fActualizarArbol();
                            getResumen();
                        } else ocultarProcesando();
                    } else if (ret == "OK") {
                        colorearMeses();
                        fActualizarArbol();
                        getResumen();
                    }
                }
                else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al pulsar el acceso directo", e.message);
    }
}

function mostrarNivelAux(sGrupo, sNivel) {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto", 250);
            return;
        }
        if (nIndice0 == null) {
            mmoff("Inf", "Mes central vacío", 200);
            return;
        }
        mostrarProcesando();
        nNE = 0;
        setTimeout("mostrarNivel('" + sGrupo + "', " + sNivel + ");", 50);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar un nivel determinado", e.message);
    }
}
function mostrarNivel(sGrupo, sNivel) {
    try {
        sResfreshNiveles = "N";
        colorearNE(0);
        MostrarOcultar(0);
        MostrarGrupoNivel(sGrupo, sNivel);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar un nivel determinado", e.message);
    }
}

function setBotonCerrar() {
    try {
        AccionBotonera("cerrarmes", "D");
        if (getPMA() != -1 && sEstado == "A") {
            if (!bLectura || (es_administrador == "SA" || es_administrador == "A")) {
                if ($I("hdnCualidadProyectoSubNodo").value == "C" || $I("hdnCualidadProyectoSubNodo").value == "P") {
                    AccionBotonera("cerrarmes", "H");
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el botón cerrar mes", e.message);
    }
}

function setResolucionPantalla() {
    try {
        mostrarProcesando();
        var js_args = "setResolucion@#@";

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}

function setResolucion1024() {
    try {
        $I("procesando").style.top = ($I("procesando").offsetTop + 30) + "px";

        var oColgroup = $I("tblIconos").children[0];
        $I("tblIconos").style.width = "985px";
        oColgroup.children[0].style.width = "80px"; //-40
        oColgroup.children[1].style.width = "373px";    //-100
        oColgroup.children[2].style.width = "172px";
        oColgroup.children[3].style.width = "295px"; //-90
        oColgroup.children[4].style.width = "65px"; //-10

        $I("divCatalogo").style.width = "1001px";
        $I("divCatalogo").style.height = nAltura + "px";
        $I("divCatalogo").children[0].style.width = "985px";

        oColgroup = $I("tblTitulo").children[0];
        $I("tblTitulo").style.width = "985px";
        oColgroup.children[0].style.width = "320px"; //-70
        oColgroup.children[1].style.width = "0px";
        oColgroup.children[1].style.display = "none";
        oColgroup.children[7].style.width = "0px";
        oColgroup.children[7].style.display = "none";

        var oFila = $I("tblTitulo").rows[0];
        //$I("tblTitulo").style.width = "985px";
        oFila.cells[0].style.width = "320px"; //-70
        oFila.cells[1].style.width = "0px";
        oFila.cells[1].style.display = "none";
        oFila.cells[7].style.width = "0px";
        oFila.cells[7].style.display = "none";

        oColgroup = $I("tblResultado").children[0];
        $I("tblResultado").style.width = "985px";
        oColgroup.children[0].style.width = "320px"; //-70
        oColgroup.children[1].style.width = "0px";
        oColgroup.children[1].style.display = "none";
        oColgroup.children[5].style.width = "0px";
        oColgroup.children[5].style.display = "none";

        oFila = $I("tblResultado").rows[0];
        oFila.cells[0].style.width = "320px"; //-70
        oFila.cells[1].style.width = "0px";
        oFila.cells[1].style.display = "none";
        oFila.cells[5].style.width = "0px";
        oFila.cells[5].style.display = "none";

        oColgroup = $I("tblRentabilidad").children[0];
        $I("tblRentabilidad").style.width = "985px";
        oColgroup.children[0].style.width = "320px"; //-70
        oColgroup.children[1].style.width = "0px";
        oColgroup.children[1].style.display = "none";
        oColgroup.children[5].style.width = "0px";
        oColgroup.children[5].style.display = "none";

        oFila = $I("tblRentabilidad").rows[0];
        oFila.cells[0].style.width = "320px"; //-70
        oFila.cells[1].style.width = "0px";
        oFila.cells[1].style.display = "none";
        oFila.cells[5].style.width = "0px";
        oFila.cells[5].style.display = "none";

        oColgroup = $I("tblIngNetos").children[0];
        $I("tblIngNetos").style.width = "985px";
        oColgroup.children[0].style.width = "320px"; //-70
        oColgroup.children[1].style.width = "0px";
        oColgroup.children[1].style.display = "none";
        oColgroup.children[5].style.width = "0px";
        oColgroup.children[5].style.display = "none";

        oFila = $I("tblIngNetos").rows[0];
        oFila.cells[0].style.width = "320px"; //-70
        oFila.cells[1].style.width = "0px";
        oFila.cells[1].style.display = "none";
        oFila.cells[5].style.width = "0px";
        oFila.cells[5].style.display = "none";

        oColgroup = $I("tblObraCurso").children[0];
        $I("tblObraCurso").style.width = "985px";
        oColgroup.children[0].style.width = "320px"; //-70
        oColgroup.children[1].style.width = "0px";
        oColgroup.children[1].style.display = "none";
        oColgroup.children[5].style.width = "0px";
        oColgroup.children[5].style.display = "none";

        oFila = $I("tblObraCurso").rows[0];
        oFila.cells[0].style.width = "320px"; //-70
        oFila.cells[1].style.width = "0px";
        oFila.cells[1].style.display = "none";
        oFila.cells[5].style.width = "0px";
        oFila.cells[5].style.display = "none";

        oColgroup = $I("tblSaldoClientes").children[0];
        $I("tblSaldoClientes").style.width = "985px";
        oColgroup.children[0].style.width = "320px"; //-70
        oColgroup.children[1].style.width = "0px";
        oColgroup.children[1].style.display = "none";
        oColgroup.children[5].style.width = "0px";
        oColgroup.children[5].style.display = "none";

        oFila = $I("tblSaldoClientes").rows[0];
        oFila.cells[0].style.width = "320px"; //-70
        oFila.cells[1].style.width = "0px";
        oFila.cells[1].style.display = "none";
        oFila.cells[5].style.width = "0px";
        oFila.cells[5].style.display = "none";

        oColgroup = $I("tblSuperior").children[0];
        $I("tblSuperior").style.width = "985px";
        oColgroup.children[0].style.width = "460px"; //
        oColgroup.children[1].style.width = "200px"; //
        oColgroup.children[2].style.width = "190px"; //
        oColgroup.children[3].style.width = "130px"; //


        $I("flsIdentificacion").style.width = "440px";
        $I("tblIdentificacion").style.width = "430px";
        $I("tblIdentificacion").children[0].children[1].style.width = "350px";
        $I("txtDesPE").style.width = "270px";
        $I("txtResponsable").style.width = "333px";
        $I("txtCliente").style.width = "333px";

    } catch (e) {
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}

function clonarmes() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto.", 250);
            return;
        }
        if (aSegMesProy.length == 0) {
            mmoff("Inf", "No existe mes para clonar.", 220);
            return;
        }

        mostrarProcesando();
        nColumnaCarrusel = nIndice0;
        var sModal = sSize(680, 565);
        var strEnlace = strServer + "Capa_Presentacion/ECO/ClonarMes/default.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sModal);
        modalDialog.Show(strEnlace, self, sModal)
	        .then(function(ret) {
                if (ret != null) {
                    getSegMesProy();
                }
                else ocultarProcesando();
	        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a borrar el mes.", e.message);
    }
}

function goToResumenEco() {
    try {
        document.forms["aspnetForm"].method = "POST";
        document.forms["aspnetForm"].action = "../ResumenEcoProy/Default.aspx";
        document.forms["aspnetForm"].submit();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la pantalla de resumen económico.", e.message);
    }
}

function traspasoiap() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto.", 250);
            return;
        }
        if (aSegMesProy.length == 0) {
            mmoff("Inf", "No existen meses para traspasar.", 250);
            return;
        }
        location.href = "../TraspasoIAP/Default.aspx?nPSN=" + codpar($I("hdnIdProyectoSubNodo").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a borrar el mes.", e.message);
    }
}
//function FacturasOrdenes(){
//    try{
//        if ($I("hdnIdProyectoSubNodo").value=="") return;
//        location.href = "../OrdenFacturacion/Catalogo/Default.aspx?nPSN="+ codpar($I("hdnIdProyectoSubNodo").value);
//	}catch(e){
//		mostrarErrorAplicacion("Error al ir a las ordenes de facturación.", e.message);
//    }
//}


function EspacioComunicacion() {
    try {
        location.href = "../EspacioComunicacion/Default.aspx";
    } catch (e) {
        mostrarErrorAplicacion("Error al ir al espacio de comunicación.", e.message);
    }
}

function AgendaUSA() {
    try {
        location.href = "../AgendaUSA/Default.aspx";
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la agenda USA.", e.message);
    }
}
function accesoEspaComu() {
    try {
        if ($I("hdnIdProyectoSubNodo").value != "0" && sProyExternalizable == "N") {
            mmoff("War", "Denegado. El proyecto debe ser externalizable", 330);
            return;
        }
        if ($I("hdnIdProyectoSubNodo").value != "0" && sProyUSA == "N") {
            mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400);
            return;
        }

        // Acceso a los usuarios que tengan acceso en modo grabación y que el proyecto tenga asignado algun usuario USA
        if (
	        (bLectura == false || es_administrador != "")
	        && ($I("hdnIdProyectoSubNodo").value == ""
	             || ($I("hdnIdProyectoSubNodo").value != "0" && sProyUSA == "S")
	             )
           )
            location.href = "../EspacioComunicacion/Default.aspx";
        else mmoff("War", "Denegado. Acceso no permitido", 200);

    } catch (e) {
        mostrarErrorAplicacion("Error al ir al espacio de comunicación.", e.message);
    }
}
function accesoAgendaUSA() {
    try {
        // Acceso a los usuarios USA y administradores
        if ($I("hdnIdProyectoSubNodo").value != "0" && sProyExternalizable == "N") {
            mmoff("War", "Denegado. El proyecto debe ser externalizable", 330);
            return;
        }
        if ($I("hdnIdProyectoSubNodo").value != "0" && sProyUSA == "N") {
            mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400);
            return;
        }

        if (sUSA == "N" && es_administrador == "") {
            mmoff("War", "Denegado. Acceso permitido a usuarios de soporte administrativo o administradores", 400); //
            return;
        }

        location.href = "../AgendaUSA/Default.aspx";

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la agenda USA.", e.message);
    }
}

function accesoProyectosUSA() {
    try {
        // Acceso a los usuarios USA y administradores
        if (sUSA == "N" && es_administrador == "") {
            mmoff("Inf", "Denegado. Acceso permitido a usuarios de soporte administrativo o administradores", 400); //
            return;
        }

        location.href = "../ProyectosUSA/Default.aspx";

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la relación de proyectos USA.", e.message);
    }
}

//var bMonedaImportesModificada = false;
function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDP";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    //bMonedaImportesModificada = true;
                    var aDatos = ret.split("@#@");
                    //$I("hdnMonedaImportes").value = aDatos[0];
                    sMONEDA_VDP = aDatos[0];
                    $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];
                    fActualizarArbol();
                    getResumen();
                } else
                    ocultarProcesando();
	        });

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}
function getDatosContrato() {
    try {
        mostrarProcesando();

        var js_args = "getDatosContrato@#@";
        js_args += id_proyectosubnodo_actual;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos del contrato", e.message);
    }
}

function crearlineabase() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto.", 250);
            return;
        }
        if (aSegMesProy.length == 0) {
            mmoff("Inf", "No existe mes para crear línea base.", 270);
            return;
        }
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/ValorGanado/CreacionLB/Default.aspx"; //?ipsn="+ codpar($I("hdnIdProyectoSubNodo").value);
        //var ret = window.showModalDialog(strEnlace, self, sSize(1000, 580));
        modalDialog.Show(strEnlace, self, sSize(1000, 580))
	        .then(function(ret) {
                if (ret != null) {
                    //            var js_args = "crearLB@#@";
                    //            js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                    //            js_args += Utilidades.escape(ret);

                    //            RealizarCallBack(js_args, "");
                    ocultarProcesando();
                } else
                    ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear la línea base.", e.message);
    }
}


function getDialogosProy() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto", 250);
            return;
        }
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/DialogoAlertas/CatalogoPSN/Default.aspx";
        //var ret = window.showModalDialog(sPantalla, self, sSize(1010, 540));
        modalDialog.Show(sPantalla, self, sSize(1010, 540))
	        .then(function(ret) {
                if (ret == "1") {
                    getDatosDialogos();
                }
                ocultarProcesando();
	        });        
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar los diálogos.", e.message);
    }
}

function setDatosDialogos() {
    try {
        if (bAlertasActivadas) {
            var strMsgDialogos = "";
            var oDivCountLeer = $I("divCountLeer");
            var oDivCountResponder = $I("divCountResponder");

            if (nDialogosAbiertos == 0) strMsgDialogos = "No tiene diálogos abiertos.";
            else {
                strMsgDialogos = "Tiene " + nDialogosAbiertos + " diálogos abiertos";
                if (nDialogosLeerInterlocutor > 0 || nDialogosResponderInterlocutor > 0) strMsgDialogos += " de los cuales, por su parte, ";
                if (nDialogosLeerInterlocutor > 0) strMsgDialogos += nDialogosLeerInterlocutor + " está" + ((nDialogosLeerInterlocutor == 1) ? "" : "n") + " pendiente" + ((nDialogosLeerInterlocutor == 1) ? "" : "s") + " de leer";
                if (nDialogosLeerInterlocutor > 0 && nDialogosResponderInterlocutor > 0) strMsgDialogos += " y ";
                if (nDialogosResponderInterlocutor > 0) strMsgDialogos += nDialogosResponderInterlocutor + " está" + ((nDialogosResponderInterlocutor == 1) ? "" : "n") + " pendiente" + ((nDialogosResponderInterlocutor == 1) ? "" : "s") + " de responder";
                strMsgDialogos += ".";
            }

            $I("lblCountDialogosAbiertos").innerText = nDialogosAbiertos;
            $I("lblCountDialogosAbiertos").style.visibility = "visible";

            oDivCountLeer.innerText = nDialogosLeerInterlocutor;
            switch (nDialogosLeerInterlocutor.toString().length) {
                case 1: oDivCountLeer.style.width = 17; break;
                case 2: oDivCountLeer.style.width = 22; break;
                case 3: oDivCountLeer.style.width = 27; break;
            }
            oDivCountLeer.style.backgroundImage = "url(../../../images/imgCountMsg" + nDialogosLeerInterlocutor.toString().length + ".png)";
            oDivCountLeer.style.visibility = (nDialogosLeerInterlocutor > 0) ? "visible" : "hidden";

            oDivCountResponder.innerText = nDialogosResponderInterlocutor;
            switch (nDialogosResponderInterlocutor.toString().length) {
                case 1: oDivCountResponder.style.width = 17; break;
                case 2: oDivCountResponder.style.width = 22; break;
                case 3: oDivCountResponder.style.width = 27; break;
            }
            oDivCountResponder.style.backgroundImage = "url(../../../images/imgCountMsg" + nDialogosResponderInterlocutor.toString().length + ".png)";
            oDivCountResponder.style.visibility = (nDialogosResponderInterlocutor > 0) ? "visible" : "hidden";

            $I("divDialogos").onmouseover = function() { showTTE(Utilidades.escape(strMsgDialogos), "Acceso a diálogos", null, 250); }
            $I("divDialogos").onmouseout = function() { hideTTE(); }
            $I("divDialogos").style.visibility = "visible";
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer los datos de los diálogos.", e.message);
    }
}

function getTPPAC() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto", 250);
            return;
        }
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/SegEco/getProyectosProduccion/Default.aspx";
        //var ret = window.showModalDialog(sPantalla, self, sSize(630, 390));
        modalDialog.Show(sPantalla, self, sSize(630, 390))
	        .then(function(ret) {
                if (ret == "OK") {
                    location.reload(true);
                } else
                    ocultarProcesando();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar los proyectos asociados al contrato.", e.message);
    }
}

function getDatosDialogos() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto", 250);
            return;
        }
        mostrarProcesando();

        var js_args = "getDatosDialogos@#@";
        js_args += $I("hdnIdProyectoSubNodo").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos de los diálogos", e.message);
    }
}
