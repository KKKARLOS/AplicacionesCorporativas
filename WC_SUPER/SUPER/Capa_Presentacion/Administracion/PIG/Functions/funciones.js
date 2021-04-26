var bNaturalezasCreadas = false;
var nIdNodoActivo = null;
//var oImgPlant = document.createElement("<img src='../../../images/imgIconoEmpresarial.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgPlant = document.createElement("img");
oImgPlant.setAttribute("src", "../../../images/imgIconoEmpresarial.gif");
oImgPlant.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

function init() {
    try {
        ToolTipBotonera("Eliminar", "Elimina la parametrización de " + strEstructuraNodo + " y naturalezas");
        //LiteralBotonera("Eliminar", "Exp.Mas.Bit.");
        setEstadistica();
        bCambios = false;
        setVigenciasAnno();
        //actualizarLupas("tblTitulo", "tblNodos");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    }
    else {
        switch (aResul[0]) {
            case "procesar":
                ocultarProcesando();
                mmoff("Suc", "Proceso realizado correctamente", 250);
                bCambios = false;
                //alert("Proceso realizado correctamente");
                break;
            case "borrar":
                ocultarProcesando();
                mmoff("Suc", "Parametrización eliminada", 250);
                bCambios = false;
                location.href = "Default.aspx";
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
                break;
        }
    }
}

function cambiarAnno(nAnno) {
    try {
        $I("txtAnnoVisible").value = parseInt($I("txtAnnoVisible").value, 10) + nAnno;
        setVigenciasAnno();
        setVigencias();
        //mIKEL 28/12/2010
        //bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar de año", e.message);
    }
}
function comprobarDatos() {
    try {
        for (var x = 0; x < $I("tblNaturalezas").rows.length; x++) {
            if ($I("tblNaturalezas").rows[x].cells[0].children[0].checked) {
                if (!esFecha($I("tblNaturalezas").rows[x].cells[3].children[0].value)) {
                    ms($I("tblNaturalezas").rows[x]);
                    mmoff("Inf", "La fecha de inicio de vigencia no es correcta",330);
                    return false;
                }
                else {
                    if (!esFecha($I("tblNaturalezas").rows[x].cells[4].children[0].value)) {
                        ms($I("tblNaturalezas").rows[x]);
                        mmoff("Inf", "La fecha de fin de vigencia no es correcta",330);
                        return false;
                    }
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
        return false;
    }
}
function procesar() {
    try {
        mostrarProcesando();
        if (!asignarValores()) {
            ocultarProcesando();
            mmoff("Err","Se ha producido un error al recoger los datos para su procesado", 400)
            return;
        }
        if (!comprobarDatos()) {
            ocultarProcesando();
            return;
        }
        var iCaso = 0;

        var aFila = FilasDe("tblNodos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].cells[0].children[0].checked && parseInt(aFila[i].cells[3].innerText, 10) == 0) {
                iCaso = 1;
                break;
            }
        }
        if (iCaso == 1) {
            ocultarProcesando();
            jqConfirm("", "Existen " + strEstructuraNodo + " marcados para generar, que no contemplan ninguna naturaleza de producción.<br><br>¿Deseas continuar?", "", "", "war").then(function (answer) {
                if (answer)                    
                    procesarContinuar();
            });
        }
        else procesarContinuar();

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}
function procesarContinuar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        var bCorrecto = true;
        sb.Append("procesar@#@");
        if ($I("chkForzar").checked)
            sb.Append("S@#@");
        else
            sb.Append("N@#@");
        sb.Append($I("txtAnnoVisible").value + "@#@");
        var aFilasNodo = FilasDe("tblNodos");
        var aFilasNat = FilasDe("tblNaturalezas");

        for (var x = 0; x < aFilasNodo.length; x++) {
            for (var i = 0; i < aNN.length; i++) {
                //if (parseInt(aNN[i][0], 10) > parseInt(tblNodos.rows[x].id)) break;
                //Mikel 28/12/2010. El punto de salida no puede ser el idNodo cuando tblNodos está ordenado por orden + denominación de nodo
                //if (aNN[i][0] > aFilasNodo[x].id) break;
                if (aFilasNodo[x].id == aNN[i][0]) {
                    if (aFilasNodo[x].cells[0].children[0].checked)
                        aNN[i][6] = "1";
                    else {
                        //aNN[i][3] = "0"; 
                        aNN[i][6] = "0";
                    }
                }
            }
        }

        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][3] == "0" || aNN[i][6] == "0") continue;

            sb.Append(aNN[i][0] + "##"); //idNodo
            sb.Append(aNN[i][1] + "##"); //idNaturaleza
            sb.Append(aNN[i][4] + "##"); //FIV
            sb.Append(aNN[i][5] + "##"); //FFV
            for (var x = 0; x < aNat.length; x++) {
                if (aNat[x][0] == aNN[i][1]) {
                    sb.Append(aNat[x][3] + "##"); //idPlantilla
                    sb.Append(Utilidades.escape(aNat[x][1]) + "##"); //Denominación Naturaleza
                    break;
                }
            }
            sb.Append(aNN[i][7] + "##"); //Esreplicable
            sb.Append(aNN[i][8] + "##"); //Hereda Nodo
            sb.Append(aNN[i][10] + "##"); //Id Usuario responsable
            sb.Append(aNN[i][9] + "##"); //Imputable GASVI
            sb.Append(aNN[i][12] + "///"); //Id Ficepi validador GASVI
        }

        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error en NuevoContinuar", e.message);
    }
}
function marcardesmarcarCalcular(nOpcion) {
    try {
        for (var i = 0; i < $I("tblNodos").rows.length; i++) {
            $I("tblNodos").rows[i].cells[0].children[0].checked = (nOpcion == 1) ? true : false;
        }
        setEstadistica();
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}

function marcardesmarcarNaturalezas(nOpcion) {
    try {
        for (var i = 0; i < $I("tblNaturalezas").rows.length; i++) {
            $I("tblNaturalezas").rows[i].cells[0].children[0].checked = (nOpcion == 1) ? true : false;
        }
        for (var i = 0; i < $I("tblNodos").rows.length; i++) {
            if ($I("tblNodos").rows[i].id == nIdNodoActivo) {
                if (nOpcion == 1) $I("tblNodos").rows[i].cells[3].innerText = $I("tblNaturalezas").rows.length;
                else $I("tblNodos").rows[i].cells[3].innerText = 0;
                break;
            }
        }
        actualizarArrayColMasivo(nOpcion, 0, nIdNodoActivo);
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar naturalezas.", e.message);
    }
}
function marcarDesmarcarCol(nOpcion, nCol) {
    try {
        for (var i = 0; i < $I("tblNaturalezas").rows.length; i++) {
            $I("tblNaturalezas").rows[i].cells[nCol].children[0].checked = (nOpcion == 1) ? true : false;
        }
        actualizarArrayColMasivo(nOpcion, nCol, nIdNodoActivo);

        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar columna.", e.message);
    }
}

var bCountVigParam = false;
function setEstadistica() {
    try {
        var nCount = 0;
        var nCountVigParam = 0;

        $I("cldEstNodo").innerText = $I("tblNodos").rows.length;
        for (var i = 0; i < $I("tblNodos").rows.length; i++) {
            if ($I("tblNodos").rows[i].cells[0].children[0].checked) nCount++;
        }
        if (!bCountVigParam) {
            for (var i = 0; i < aNat.length; i++) {
                if (aNat[i][2] > 12) nCountVigParam++;
                //if (parseInt(aNat[i][2], 10) > 12) nCountVigParam++;
            }
            bCountVigParam = true;
            $I("cldNatImprod").innerText = aNat.length.ToString("N", 9, 0);
            $I("cldVigParam").innerText = nCountVigParam.ToString("N", 9, 0);
        }
        $I("cldEstNodoSel").innerText = nCount.ToString("N", 9, 0);
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular las estadísticas.", e.message);
    }
}

function setVigenciasAnno() {
    try {
        var sFechaAux = $I("txtAnnoVisible").value;
        var dDesde = new Date(sFechaAux, 0, 1);
        var dHasta;
        var res = setValorNaturalezas();

        for (var i = 0; i < aNN.length; i++) {
            dHasta = new Date(sFechaAux, 0, 1).add("mo", parseInt(aNN[i][2], 10)).add("d", -1);
            aNN[i][4] = dDesde.ToShortDateString();
            aNN[i][5] = dHasta.ToShortDateString();
        }
        getValorNaturalezas();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las fechas de vigencia al modificar el año.", e.message);
    }
}
function setVigencias() {
    try {
        var dDesde;
        var dHasta;
        for (var i = 0; i < $I("tblNaturalezas").rows.length; i++) {
            dDesde = new Date($I("txtAnnoVisible").value, 0, 1);
            dHasta = new Date($I("txtAnnoVisible").value, 0, 1).add("mo", parseInt($I("tblNaturalezas").rows[i].getAttribute("meses"), 10)).add("d", -1);
            $I("tblNaturalezas").rows[i].cells[3].children[0].value = dDesde.ToShortDateString();
            $I("tblNaturalezas").rows[i].cells[4].children[0].value = dHasta.ToShortDateString();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las fechas de vigencia.", e.message);
    }
}

function getNaturalezas(oFila) {
    try {
        nIdNodoActivo = oFila.id;
        if (!bNaturalezasCreadas) {
            crearTablaNaturalezas();
            //setValorNaturalezas();
        }
        //else {
        //    var res = setValorNaturalezas();
        //}
        getValorNaturalezas();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer las fechas de vigencia.", e.message);
    }
}

function crearTablaNaturalezas() {
    try {
        var oNF;
        var oNC1;
        var oNC2;
        var oNC3;
        var oNC4;

        for (var i = 0; i < aNat.length; i++) {
            oNF = $I("tblNaturalezas").insertRow(-1);
            oNF.style.height = "20px";
            oNF.id = aNat[i][0];
            oNF.setAttribute("meses", aNat[i][2]);
            oNF.setAttribute("plantilla", aNat[i][3]);
            oNF.attachEvent("onclick", mm);

            //celda 0 FILA SELECCIONADA
            var oCtrl1 = document.createElement("input");
            oCtrl1.setAttribute("type", "checkbox");
            oCtrl1.setAttribute("style", "margin-left:5px;");
            oCtrl1.className="check";
            oCtrl1.onclick = function() { setNatCount(this); };

            oNF.insertCell(-1).appendChild(oCtrl1);
            //celda 1 DENOMINACION NATURALEZA

            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);//Denominación -> 1
            oNF.cells[1].children[0].className = "NBR";
            oNF.cells[1].children[0].setAttribute("style", "width:160px;");
            oNF.cells[1].children[0].attachEvent("onmouseover", TTip);
            oNF.cells[1].children[0].innerText = aNat[i][1];

            //celda 2 PLANTILLA
            oNC3 = oNF.insertCell(-1);
            if (aNat[i][3] != 0) oNC3.appendChild(oImgPlant.cloneNode(true));
            //celda 3 FIV

            var oCtrl3 = document.createElement("input");
            oCtrl3.setAttribute("type", "text");
            oCtrl3.className = "txtL";
            oCtrl3.id = "fDesde_" + i;
            oCtrl3.value='';
            oCtrl3.setAttribute("style", "width:60px; cursor:pointer");
            oCtrl3.setAttribute("Calendar", "oCal");
            oCtrl3.setAttribute("goma", "0");

            oCtrl3.onchange = function() { validarFecha('D', this); };
            
            if (btnCal == "I") {
                oCtrl3.setAttribute("readonly", "readonly");
                oCtrl3.onclick = function() { mc(this); };

                //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='fDesde_" + i + "' class='txtL' style='width:60px; cursor:pointer' value='' Calendar='oCal' onclick='mc(this);' onchange=\"validarFecha('D',this)\" goma='0' readonly />"));
            }
            else {
                oCtrl3.attachEvent("onfocus", focoFecha);
                oCtrl3.attachEvent("onmousedown", mc1);
            
                //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='fDesde_" + i + "' class='txtL' style='width:60px; cursor:pointer' value='' Calendar='oCal' onfocus='focoFecha(this);' onmousedown='mc1(this)' onchange=\"validarFecha('D',this)\" goma='0' />"));
            }
            oNF.insertCell(-1).appendChild(oCtrl3);
            //celda 4 FFV
            var oCtrl4 = document.createElement("input");
            oCtrl4.setAttribute("type", "text");
            oCtrl4.className = "txtL";
            oCtrl4.id = "fHasta_" + i;
            oCtrl4.value = '';
            oCtrl4.setAttribute("style", "width:60px; cursor:pointer");
            oCtrl4.setAttribute("Calendar", "oCal");
            oCtrl4.setAttribute("goma", "0");

            oCtrl4.onchange = function() { validarFecha('H', this); };

            if (btnCal == "I") {
                oCtrl4.setAttribute("readonly", "readonly");
                oCtrl4.onclick = function() { mc(this); };

                //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='fHasta_" + i + "' class='txtL' style='width:60px; cursor:pointer' value='' Calendar='oCal' onclick='mc(this);' onchange=\"validarFecha('H',this)\" goma='0' readonly />"));
            }
            else {
                //oCtrl4.onfocus = function() { focoFecha(this); };
                oCtrl4.attachEvent("onfocus", focoFecha);
                oCtrl4.onmousedown = function() { mc1(this); };

                //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='fHasta_" + i + "' class='txtL' style='width:60px; cursor:pointer' value='' Calendar='oCal' onfocus='focoFecha(this);' onmousedown='mc1(this)' onchange=\"validarFecha('H',this)\" goma='0' />"));
            }
            oNF.insertCell(-1).appendChild(oCtrl4);
            
            //celda 5 replica PIG
            //oNF.insertCell(-1).appendChild(document.createElement("<input type='checkbox' class='check'>"));
            var oCtrl5 = document.createElement("input");
            oCtrl5.setAttribute("type", "checkbox");
            oCtrl5.className = "check";
            oCtrl5.setAttribute("style", "margin-left:20px;");
            oCtrl5.onclick = function () { actualizarArrayCol(this, 5) };
            oNF.insertCell(-1).appendChild(oCtrl5);

            //celda 6 hereda Nodo
            var oCtrl6 = document.createElement("input");//hereda nodo -> 6
            oCtrl6.setAttribute("type", "checkbox");
            oCtrl6.setAttribute("style", "margin-left:20px;");
            oCtrl6.className = "check";
            oCtrl6.onclick = function () { actualizarArrayCol(this, 6) };
            oNF.insertCell(-1).appendChild(oCtrl6);

            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);//Responsable proyecto -> 7
            oNF.cells[7].appendChild(oNobr.cloneNode(true), null);
            oNF.cells[7].children[0].className = "NBR";
            oNF.cells[7].children[0].setAttribute("style", "width:95px;");
            oNF.cells[7].children[0].attachEvent("onmouseover", TTip);

            var oCtrl8 = document.createElement("input");//imputable GASVI -> 8
            oCtrl8.setAttribute("type", "checkbox");
            oCtrl8.setAttribute("style", "margin-left:20px;");
            oCtrl8.className = "check";
            oCtrl8.onclick = function () { actualizarArrayCol(this, 8) };
            oNF.insertCell(-1).appendChild(oCtrl8);

            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);//Validador GASVI -> 9
            oNF.cells[9].appendChild(oNobr.cloneNode(true), null);
            oNF.cells[9].children[0].className = "NBR";
            oNF.cells[9].children[0].setAttribute("style", "width:95px;");
            oNF.cells[9].children[0].attachEvent("onmouseover", TTip);

        }
        bNaturalezasCreadas = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla de naturalezas.", e.message);
    }
}

function getValorNaturalezas() {
    try {
        if (nIdNodoActivo == null) return;
        //var bNodoEncontrado = false;
        var dDesde;
        var dHasta;
        var iCountNat = 0;
        var sAux = buscarPrimerUltimoNodo(nIdNodoActivo);
        var aResul = sAux.split("@#@");
        var idIni = parseInt(aResul[0]);
        var idFin = parseInt(aResul[1]);

        for (var i = idIni; i <= idFin; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                //bNodoEncontrado = true;
                for (var x = 0; x < aNat.length; x++) {
                    if (aNN[i][1] == aNat[x][0]) {
                        //Si el nodo no estaba cargado previamente mantengo el valor del check salvo que este parametrizado
                        if (aNN[i][14] == "1" || aNN[i][15] == "1")
                            $I("tblNaturalezas").rows[x].cells[0].children[0].checked = (aNN[i][3] == "1") ? true : false;//Grabar
                        else {
                            if ($I("tblNaturalezas").rows[x].cells[0].children[0].checked)
                                aNN[i][3] = "1";
                            else
                                aNN[i][3] = "0";
                        }
                        aNN[i][14] = "1";//Indica que el nodo ya ha sido cargado

                        if (aNN[i][4] != "") {
                            $I("tblNaturalezas").rows[x].cells[3].children[0].value = aNN[i][4];
                        }
                        else {
                            dDesde = new Date($I("txtAnnoVisible").value, 0, 1);
                            $I("tblNaturalezas").rows[x].cells[3].children[0].value = dDesde.ToShortDateString();
                        }
                        if (aNN[i][5] != "") {
                            $I("tblNaturalezas").rows[x].cells[4].children[0].value = aNN[i][5];
                        }
                        else {
                            dHasta = new Date($I("txtAnnoVisible").value, 0, 1).add("mo", parseInt(aNat[x][2], 10)).add("d", -1);
                            $I("tblNaturalezas").rows[x].cells[4].children[0].value = dHasta.ToShortDateString();
                        }
                        $I("tblNaturalezas").rows[x].cells[5].children[0].checked = (aNN[i][7] == "1") ? true : false;//Replicar
                        $I("tblNaturalezas").rows[x].cells[6].children[0].checked = (aNN[i][8] == "1") ? true : false;//Hereda nodo
                        $I("tblNaturalezas").rows[x].cells[7].children[0].innerText = aNN[i][11];//Responsable
                        $I("tblNaturalezas").rows[x].cells[8].children[0].checked = (aNN[i][9] == "1") ? true : false;//Imputable GASVI
                        $I("tblNaturalezas").rows[x].cells[9].children[0].innerText = aNN[i][13];//Validador GASVI

                        $I("tblNaturalezas").rows[x].setAttribute("idResp", aNN[i][10]);
                        $I("tblNaturalezas").rows[x].setAttribute("idValid", aNN[i][12]);

                        if (tblNaturalezas.rows[x].cells[0].children[0].checked) {
                            iCountNat++;
                        }
                        
                        break;
                    }
                } 
            } //else if (bNodoEncontrado) break;
        }
        actualizarCountNaturalezas(nIdNodoActivo, iCountNat);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de nodos y naturalezas.", e.message);
    }
}

function setValorNaturalezas() {
    try {
        var bCargarNodo = true;
        var dDesde;
        var dHasta;
        var iCountNat = 0;

        if (!nIdNodoActivo) return;

        var sAux = buscarPrimerUltimoNodo(nIdNodoActivo);
        var aResul = sAux.split("@#@");
        var idIni = parseInt(aResul[0]);
        var idFin = parseInt(aResul[1]);
        //Cuento el nº de naturalezas marcadas
        if (aNN[idIni][14] == "0") {
            if (aNN[idIni][15] == "0") {
                for (var x = 0; x < $I("tblNaturalezas").rows.length; x++) {
                    if (tblNaturalezas.rows[x].cells[0].children[0].checked) {
                        iCountNat++;
                    }
                    //if ($I("tblNaturalezas").rows[x].className == "FS" || $I("tblNaturalezas").rows[x].getAttribute("class") == "FS") {
                    //    ms($I("tblNaturalezas").rows[x]);
                    //}
                }
                actualizarCountNaturalezas(nIdNodoActivo, iCountNat);
            }
            else
                bCargarNodo = false;
        }
        else 
            bCargarNodo = false;
        
        for (var i = idIni; i <= idFin; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                bNodoEncontrado = true;
                for (var x = 0; x < $I("tblNaturalezas").rows.length; x++) {
                    if (aNN[i][1] == $I("tblNaturalezas").rows[x].id) {
                        if (bCargarNodo) {
                            aNN[i][3] = ($I("tblNaturalezas").rows[x].cells[0].children[0].checked) ? "1" : "0";
                            aNN[i][4] = $I("tblNaturalezas").rows[x].cells[3].children[0].value;
                            aNN[i][5] = $I("tblNaturalezas").rows[x].cells[4].children[0].value;
                            aNN[i][7] = ($I("tblNaturalezas").rows[x].cells[5].children[0].checked) ? "1" : "0";//replica
                            aNN[i][8] = ($I("tblNaturalezas").rows[x].cells[6].children[0].checked) ? "1" : "0";//hereda nodo
                            aNN[i][9] = ($I("tblNaturalezas").rows[x].cells[8].children[0].checked) ? "1" : "0";//imputable GASVI
                            aNN[i][10] = $I("tblNaturalezas").rows[x].getAttribute("idResp");//Id Usuario responsable proyecto
                            aNN[i][11] = $I("tblNaturalezas").rows[x].cells[7].children[0].innerText;
                            aNN[i][12] = $I("tblNaturalezas").rows[x].getAttribute("idValid");//Id Ficepi validador GASVI
                            aNN[i][13] = $I("tblNaturalezas").rows[x].cells[9].children[0].innerText;
                            aNN[i][14] = "1";//Indica que el nodo ya ha sido cargado
                        }
                        break;
                    }
                }
            }
        }
        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer los datos de nodos y naturalezas.", e.message);
        return false;
    }
}

function setNatCount(oChk) {
    try {

        for (var i = 0; i < $I("tblNodos").rows.length; i++) {
            if ($I("tblNodos").rows[i].id == nIdNodoActivo) {
                var nCount = parseInt($I("tblNodos").rows[i].cells[3].innerText, 10);
                if (oChk.checked)
                    nCount++;
                else
                    nCount--;
                $I("tblNodos").rows[i].cells[3].innerText = nCount;
                break;
            }
        }
        var idNat = oChk.parentElement.parentElement.getAttribute("id");
        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                if (aNN[i][1] == idNat) {
                    if (oChk.checked)
                        aNN[i][3] = "1";
                    else
                        aNN[i][3] = "0";
                    break;
                }
            }
            if (aNN[i][0] > nIdNodoActivo)
                break;
        }
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las naturalezas asociadas al " + strEstructuraNodo + ".", e.message);
    }
}

function validarFecha(sOpcion, oControl) {
    try {
        if (oControl.parentNode.parentNode.cells[3].children[0].value.length != 0 &&
            oControl.parentNode.parentNode.cells[3].children[0].value.length != 10)
            return;
        if (oControl.parentNode.parentNode.cells[4].children[0].value.length != 0 &&
            oControl.parentNode.parentNode.cells[4].children[0].value.length != 10)
            return;
        var iDiffDias = DiffDiasFechas(oControl.parentNode.parentNode.cells[3].children[0].value, oControl.parentNode.parentNode.cells[4].children[0].value);
        if (iDiffDias < 0) {
            if (sOpcion == "D")
                oControl.parentNode.parentNode.cells[4].children[0].value = oControl.parentNode.parentNode.cells[3].children[0].value;
            else
                oControl.parentNode.parentNode.cells[3].children[0].value = oControl.parentNode.parentNode.cells[4].children[0].value;
        }

        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                aNN[i][4] = oControl.parentNode.parentNode.cells[3].children[0].value; //FIV
                aNN[i][5] = oControl.parentNode.parentNode.cells[4].children[0].value; //FFV
                break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la fecha.", e.message);
    }
}

function parametrizar() {
    try {
        if (bCambios) {
            jqConfirm("", "Se han producido cambios que se perderán si grabas algún dato de la parametrización.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    parametrizarContinuar();
                }
            });
        }
        else parametrizarContinuar();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la ventana de parametrización.", e.message);
    }
}
function parametrizarContinuar() {
    try {

        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/PIG/parametrizarPIG.aspx", self, sSize(1180, 900))
            .then(function (ret) {
                if (ret != null) {
                    if (ret == "T") {
                        bCambios = false;
                        location.href = "Default.aspx";
                        //return; //El return detiene el location.href en Chrome.
                    }
                }
            });

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error en parametrizarContinuar", e.message);
    }
}
function unload() {
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Desea procesarlos?", "", "", "war", 350).then(function (answer) {
            if (answer) {
                procesar(); 
            }
            else bCambios = false;
        });
    }
}
function getResponsable() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener un responsable", e.message);
    }
}
function getValidador() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdValidador").value = aDatos[2];//recojo el IdFicepi
                    $I("txtValidador").value = aDatos[1];
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener un validador", e.message);
    }
}
function setResponsable() {
    try {
        if ($I("hdnIdResponsable").value == "") {
            mmoff("War", "Debes seleccionar responsable para asignarlo a las naturalezas", 420);
            return;
        }
        var tblNaturalezas = $I("tblNaturalezas");
        var sw = 0;
        for (var x = 0; x < tblNaturalezas.rows.length; x++) {
            if (tblNaturalezas.rows[x].className == "FS") {
                sw = 1;
                //id Usuario responsable
                tblNaturalezas.rows[x].setAttribute("idResp", $I("hdnIdResponsable").value);
                tblNaturalezas.rows[x].cells[7].children[0].innerHTML = $I("txtResponsable").value;
                tblNaturalezas.rows[x].cells[7].children[0].setAttribute("title", $I("txtResponsable").value);
                //Actualizo el array
                actualizarArrayResponsable(nIdNodoActivo, tblNaturalezas.rows[x].id)
            }
        }
        if (sw == 0)
            mmoff("War", "Debes seleccionar alguna fila para asignarle responsable", 400);

        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer responsable.", e.message);
    }
}
function setValidador() {
    try {
        var tblNaturalezas = $I("tblNaturalezas");
        var sw = 0;
        for (var x = 0; x < tblNaturalezas.rows.length; x++) {
            if (tblNaturalezas.rows[x].className == "FS") {
                sw = 1;
                //Id ficepi validador GASVI
                tblNaturalezas.rows[x].setAttribute("idValid", $I("hdnIdValidador").value);
                //validador GASVI
                tblNaturalezas.rows[x].cells[9].children[0].innerHTML = $I("txtValidador").value;
                tblNaturalezas.rows[x].cells[9].children[0].setAttribute("title", $I("txtValidador").value);
                //Actualizo el array
                actualizarArrayValidador(nIdNodoActivo, tblNaturalezas.rows[x].id)
            }
        }
        if (sw == 0)
            mmoff("War", "Debes seleccionar alguna fila para asignarle validador", 400);

        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer responsable.", e.message);
    }
}
function actualizarArrayResponsable(nIdNodoActivo, idNaturaleza) {
    for (var i = 0; i < aNN.length; i++) {
        if (aNN[i][0] == nIdNodoActivo) {
            if (aNN[i][1] == idNaturaleza) {
                aNN[i][10] = $I("hdnIdResponsable").value;
                aNN[i][11] = $I("txtResponsable").value;
                break;
            }
        }
    }
}
function actualizarArrayValidador(nIdNodoActivo, idNaturaleza) {
    for (var i = 0; i < aNN.length; i++) {
        if (aNN[i][0] == nIdNodoActivo) {
            if (aNN[i][1] == idNaturaleza) {
                aNN[i][12] = $I("hdnIdValidador").value;
                aNN[i][13] = $I("txtValidador").value;
                break;
            }
        }
    }
}
function delValidador() {
    $I("hdnIdValidador").value = "";
    $I("txtValidador").value = "";
}
function asignarValores() {
    var iCountNat = 0;
    var idIni = -1;
    var idFin = -1;
    var sAux = "";
    try {
        var ret = setValorNaturalezas();

        var aNodos = FilasDe("tblNodos");
        for (var i = 0; i < aNodos.length; i++) {
            //alert("aFila[i].nodo_destino: "+ aFila[i].nodo_destino);
            if (aNodos[i].cells[0].children[0].checked){// && parseInt(aNodos[i].cells[3].innerText, 10) == 0) {
                sAux = buscarPrimerUltimoNodo(aNodos[i].id);
                var aResul = sAux.split("@#@");
                idIni = parseInt(aResul[0]);
                idFin = parseInt(aResul[1]);
                if (aNN[idIni][14] != "1" && aNN[idIni][15] != "1") {//No he cargado previamente ese nodo ni está parametrizado
                    if (tblNaturalezas.rows.length == 0) { crearTablaNaturalezas(); }

                    for (var j = 0; j < tblNaturalezas.rows.length; j++) {//Para cada una de las naturalezas del nodo
                        if (tblNaturalezas.rows[j].cells[0].children[0].checked) {
                            iCountNat++;
                            for (var z = idIni; z <= idFin; z++) {//Recorre las naturalezas en el array nodos-naturaleza
                                if (aNN[z][1] == tblNaturalezas.rows[j].id) {
                                    aNN[z][2] = tblNaturalezas.rows[j].getAttribute("meses");//meses vigencia
                                    aNN[z][3] = "1";
                                    aNN[z][6] = (tblNaturalezas.rows[j].cells[0].children[0].checked) ? "1" : "0";//grabar
                                    aNN[z][7] = (tblNaturalezas.rows[j].cells[5].children[0].checked) ? "1" : "0";//replica
                                    aNN[z][8] = (tblNaturalezas.rows[j].cells[6].children[0].checked) ? "1" : "0";//hereda nodo
                                    aNN[z][10] = tblNaturalezas.rows[j].getAttribute("idResp");//id Usuario responsable
                                    aNN[z][11] = (tblNaturalezas.rows[j].cells[7].children[0].innerHTML);//responsable
                                    aNN[z][9] = (tblNaturalezas.rows[j].cells[8].children[0].checked) ? "1" : "0";//imputable GASVI
                                    aNN[z][12] = tblNaturalezas.rows[j].getAttribute("idValid");//Id ficepi validador GASVI
                                    aNN[z][13] = tblNaturalezas.rows[j].cells[9].children[0].innerHTML;//validador GASVI

                                    aNN[z][14] = "1";//Indica que el nodo ya ha sido cargado 

                                    break;
                                }
                            }
                        }
                    }
                    actualizarCountNaturalezas(aNodos[i].id, iCountNat);
                }
            }
        }
        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al asignar valores.", e.message);
        return false;
    }
}
//Obtiene los indices menor y mayor de las naturalezas para un nodo
function buscarPrimerUltimoNodo(idNodo) {
    var idIni = -1;
    var idFin = -1;
    for (var i = 0; i < aNN.length; i++) {
        if (aNN[i][0] > idNodo) break;
        if (aNN[i][0] == idNodo && idIni == -1) idIni = i;
        if (aNN[i][0] == idNodo) idFin = i;
    }
    return idIni + "@#@" + idFin;
}
function actualizarCountNaturalezas(nIdNodoActivo, iCountNat) {
    var tblNodos = $I("tblNodos");
    for (var i = 0; i < tblNodos.rows.length; i++) {
        if (tblNodos.rows[i].id == nIdNodoActivo) {
            tblNodos.rows[i].cells[3].innerText = iCountNat;
            break;
        }
    }
}
function borrarParam() {
    try {
        jqConfirm("", "Si pulsas 'Aceptar' se eliminará la parametrización. <br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                RealizarCallBack("borrar@#@", "");  //con argumentos
            }
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar la parametrización.", e.message);
    }
}

function actualizarArrayCol(oChk, nCol) {
    var idNat = oChk.parentElement.parentElement.getAttribute("id");
    var oCol = -1;
    switch (nCol) {
        case 5://Replica
            oCol = 7;
            break;
        case 6://Hereda
            oCol = 8;
            break;
        case 8://Imputable
            oCol = 9;
            break;
    }
    if (oCol != -1) {
        var oAct = 0;
        if (oChk.checked) oAct = 1;
        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                if (aNN[i][1] == idNat) {
                    aNN[i][oCol] = oAct;
                    break;
                }
            }
        }
    }
}
function actualizarArrayColMasivo(oAct, nCol, nIdNodoActivo) {
    var oCol = -1;
    switch (nCol) {
        case 0://Marcado para grabar
            oCol = 3;
            break;
        case 5://Replica
            oCol = 7;
            break;
        case 6://Hereda
            oCol = 8;
            break;
        case 8://Imputable
            oCol = 9;
            break;
    }
    if (oCol != -1) {
        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                aNN[i][oCol] = oAct;
            }
        }
    }
}

