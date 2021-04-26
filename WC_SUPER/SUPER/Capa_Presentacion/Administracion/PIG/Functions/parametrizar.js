var bNaturalezasCreadas = false;
var nIdNodoActivo = null;
//var aNat = new Array(); // aNN --> Array de Naturalezas
//var aNN = new Array(); // aNN --> Array de Nodos Naturalezas
var bHayCambios = false;
var bCambios = false;
var bSalir = false;
var bSaliendo = false;
var returnValue;
var oNobr = document.createElement("nobr");
oNobr.className = "NBR";
//var oImgPlant = document.createElement("<img src='../../../images/imgIconoEmpresarial.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgPlant = document.createElement("img");
oImgPlant.setAttribute("src", "../../../images/imgIconoEmpresarial.gif");
oImgPlant.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

function init() {
    try {
        if (!mostrarErrores()) return;
        eval($I("hdnArrayNat").value);
        eval($I("hdnArrayNN").value);
        $I("btnAtnat").title = "Asignar todas las naturalezas a todos los " + strEstructuraNodo;
        $I("btnDtdt").title = "Desasignar todas las naturalezas de todos los " + strEstructuraNodo;
        setEstadistica();
        bHayCambios = false;
        bCambios = false;
        ocultarProcesando();
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload() {
    if (!bSaliendo) salir();
}
function grabarsalir() {
    bSalir = true;
    grabar();
}
function grabarAux() {
    bSalir = false;
    grabar();
}

function salir() {
    var strRetorno;
    bSalir = false;
    bSaliendo = true;
    if (bHayCambios) strRetorno = "T";
    else strRetorno = "F";

    returnValue = strRetorno;
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabarsalir();
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
                mmoff("Suc", "Grabación correcta", 160);

                bCambios = false;
                if (bSalir) setTimeout("salir();", 50);
                else {
                    var tblNatMant = $I("tblNatMant");
                    for (var i = 0; i < tblNatMant.rows.length - 1; i++);
                    tblNatMant.rows[i].setAttribute("bd", "N");
                }
                break;
            case "borrar":
                bCambios = false;
                bHayCambios = true;
                //salir();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function borrarParam() {
    try {
        jqConfirm("", "Si pulsas 'Aceptar' se eliminará la parametrización. <br><br>¿Quieres continuar?", "", "", "war", 380).then(function (answer) {
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

function grabar() {
    try {
        //var ret = setValoresNodoNaturalezas();

        var sb = new StringBuilder;
        var bCorrecto = true;
        var idIni=-1;
        var idFin=-1;
        sb.Append("grabar@#@");

        var aFilasNodo = FilasDe("tblNodos");
        var aFilasNat = FilasDe("tblNaturalezas");
        var aFilasNatMant = FilasDe("tblNatMant");

        //for (var x = 0; x < aFilasNodo.length; x++) {
        //    if (aFilasNodo[x].cells[0].children[0].checked && aFilasNodo[x].getAttribute("defectoPIG") == 0
        //        || !aFilasNodo[x].cells[0].children[0].checked && aFilasNodo[x].getAttribute("defectoPIG") == 1) {
        //        sb.Append(aFilasNodo[x].id + "##");
        //        sb.Append((aFilasNodo[x].cells[0].children[0].checked) ? "1///" : "0///"); //idNodo+si marcado o no
        //    }
        //}
        //sb.Append("@#@");
        for (var x = 0; x < aFilasNatMant.length; x++) {
            //if (aFilasNatMant[x].meses != aFilasNatMant[x].cells[2].children[0].value){
            if (aFilasNatMant[x].getAttribute("bd") != "N") {
                sb.Append(aFilasNatMant[x].id + "##");//iD Naturaleza
                sb.Append(aFilasNatMant[x].cells[2].children[0].value + "##");//meses vigencia
                sb.Append((aFilasNatMant[x].cells[3].children[0].checked) ? "1##" : "0##");//permite réplicas
                sb.Append((aFilasNatMant[x].cells[4].children[0].checked) ? "1##" : "0##");//hereda nodo
                sb.Append((aFilasNatMant[x].cells[5].children[0].checked) ? "1" : "0");//imputable GASVI
                sb.Append("///");
            }
        }
        sb.Append("@#@");

        for (var x = 0; x < aFilasNodo.length; x++) {//Recorre nodos marcados
            if (aFilasNodo[x].cells[0].children[0].checked) {
                sAux = buscarPrimerUltimoNodo(aFilasNodo[x].id);
                var aResul = sAux.split("@#@");
                idIni = parseInt(aResul[0]);
                idFin = parseInt(aResul[1]);
                if (aNN[idIni][12] != "1") {//No he cargado previamente ese nodo
                    if (tblNaturalezas.rows.length == 0) { crearTablaNaturalezas(); }
                    for (var j = 0; j < tblNaturalezas.rows.length; j++) {//Para cada una de las naturalezas del nodo
                        for (var z = idIni; z <= idFin; z++) {//Recorre las naturalezas en el array nodos-naturaleza
                            if (aNN[z][1] == tblNaturalezas.rows[j].id) {
                                aNN[z][2] = (tblNatMant.rows[j].cells[2].children[0].innerHTML);//meses vigencia
                                aNN[z][3] = "1";
                                aNN[z][4] = (tblNaturalezas.rows[j].cells[0].children[0].checked) ? "1" : "0";//generar
                                aNN[z][5] = (tblNatMant.rows[j].cells[3].children[0].checked) ? "1" : "0";//replica
                                aNN[z][6] = (tblNatMant.rows[j].cells[4].children[0].checked) ? "1" : "0";//hereda nodo
                                aNN[z][8] = tblNaturalezas.rows[j].getAttribute("idResp");//id Usuario responsable
                                aNN[z][9] = (tblNaturalezas.rows[j].cells[5].children[0].innerHTML);//responsable
                                aNN[z][7] = (tblNatMant.rows[j].cells[5].children[0].checked) ? "1" : "0";//imputable GASVI
                                aNN[z][10] = tblNaturalezas.rows[j].getAttribute("idValid");//Id ficepi validador GASVI
                                aNN[z][11] = tblNaturalezas.rows[j].cells[7].children[0].innerHTML;//validador GASVI

                                aNN[z][12] = "1";//Indica que el nodo ya ha sido cargado con los valores de parametrización
                            }
                        }
                    }
                }
                else {
                    for (var z = idIni; z <= idFin; z++) {//Recorre las naturalezas en el array nodos-naturaleza
                        aNN[z][3] = "1";
                    }
                }
            }
        }
        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][3] == "0" || aNN[i][4] == "0") continue;

            sb.Append(aNN[i][0] + "##"); //idNodo
            sb.Append(aNN[i][1] + "##"); //idNaturaleza
            sb.Append(aNN[i][5] + "##"); //replicaPIG
            sb.Append(aNN[i][6] + "##"); //hereda nodo
            sb.Append(aNN[i][7] + "##"); //imputable GASVI
            sb.Append(aNN[i][8] + "##"); //id usuario responsable
            sb.Append(aNN[i][10] + "///"); //id ficepi validador GASVI
        }

        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
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
function marcardesmarcarCalcular(nOpcion) {
    try {
        var tblNodos = $I("tblNodos");
        for (var i = 0; i < tblNodos.rows.length; i++) {
            tblNodos.rows[i].cells[0].children[0].checked = (nOpcion == 1) ? true : false;
        }
        setEstadistica();
        bHayCambios = true;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}

function marcardesmarcarNaturalezas(nOpcion) {
    try {
        var tblNodos = $I("tblNodos");
        var tblNaturalezas = $I("tblNaturalezas");

        for (var i = 0; i < tblNaturalezas.rows.length; i++) {
            tblNaturalezas.rows[i].cells[0].children[0].checked = (nOpcion == 1) ? true : false;
        }
        for (var i = 0; i < tblNodos.rows.length; i++) {
            if (tblNodos.rows[i].id == nIdNodoActivo) {
                if (nOpcion == 1) tblNodos.rows[i].cells[3].innerText = tblNaturalezas.rows.length;
                else tblNodos.rows[i].cells[3].innerText = 0;
                break;
            }
        }
        var sAux = buscarPrimerUltimoNodo(nIdNodoActivo);
        var aResul = sAux.split("@#@");
        var idIni = parseInt(aResul[0]);
        var idFin = parseInt(aResul[1]);
        for (var z = idIni; z <= idFin; z++) {//Recorre las naturalezas en el array nodos-naturaleza
            if (nOpcion == 1) 
                aNN[z][4] = "1";
            else 
                aNN[z][4] = "0";
        }

        bHayCambios = true;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}

function setEstadistica() {
    try {
        var nCount = 0;
        var nCountVigParam = 0;
        var tblNodos = $I("tblNodos");
        var tblNatMant = $I("tblNatMant");

        $I("cldEstNodo").innerText = tblNodos.rows.length;
        for (var i = 0; i < tblNodos.rows.length; i++) {
            if (tblNodos.rows[i].cells[0].children[0].checked) nCount++;
        }

        for (var i = 0; i < tblNatMant.rows.length; i++) {
            if (parseInt(tblNatMant.rows[i].cells[2].children[0].value, 10) == 0) tblNatMant.rows[i].cells[2].children[0].value = "1";
            if (parseInt(tblNatMant.rows[i].cells[2].children[0].value, 10) != 12) nCountVigParam++;
        }

        $I("cldNatImprod").innerText = aNat.length.ToString("N", 9, 0);
        $I("cldVigParam").innerText = nCountVigParam.ToString("N", 9, 0);
        $I("cldEstNodoSel").innerText = nCount.ToString("N", 9, 0);
        bHayCambios = true;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular las estadísticas.", e.message);
    }
}

function getNaturalezas(oFila) {
    try {
        //if (!bNaturalezasCreadas) crearTablaNaturalezas()
        //else var res = setValoresNodoNaturalezas();
        nIdNodoActivo = oFila.id;

        if (tblNaturalezas.rows.length == 0) {
            crearTablaNaturalezas();
        }
        //Pone en el array aNN los valores de la tabla Naturalezas y Nodo-Naturaleza
        //setValoresNodoNaturalezas();
        //Pone en la tabla tblNaturalezas los valores del array aNN
        getValorNaturalezas();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las fechas de vigencia.", e.message);
    }
}

function setValoresNodoNaturalezas() {
    try {
        var bNodoEncontrado = false;
        var dDesde;
        var dHasta;
        var tblNatMant = $I("tblNatMant");
        var tblNaturalezas = $I("tblNaturalezas");
        var iCountNat = 0;
        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                bNodoEncontrado = true;
                if (aNN[i][12] == "1") {//Ya he cargado previamente ese nodo
                    //No hago nada porque getValorNaturalezas() ya va a cargar el catálogo con los datos del array aNN
                }
                else {
                    if (tblNaturalezas.rows.length == 0) {
                        crearTablaNaturalezas();
                    }
                    iCountNat = 0;
                    for (var x = 0; x < tblNaturalezas.rows.length; x++) {
                        //if (tblNaturalezas.rows[x].className == "FS") ms(tblNaturalezas.rows[x]);
                        if (tblNaturalezas.rows[x].cells[0].children[0].checked) iCountNat++;
                        if (aNN[i][1] == tblNaturalezas.rows[x].id) {
                            aNN[i][3] = (tblNaturalezas.rows[x].cells[0].children[0].checked) ? "1" : "0";//generar
                            aNN[i][2] = (tblNatMant.rows[x].cells[2].children[0].innerHTML);//meses vigencia
                            aNN[i][5] = (tblNatMant.rows[x].cells[3].children[0].checked) ? "1" : "0";//replica
                            aNN[i][6] = (tblNatMant.rows[x].cells[4].children[0].checked) ? "1" : "0";//hereda nodo
                            aNN[i][8] = tblNaturalezas.rows[x].getAttribute("idResp");//id Usuario responsable
                            aNN[i][9] = (tblNaturalezas.rows[x].cells[5].children[0].innerHTML);//responsable
                            aNN[i][7] = (tblNatMant.rows[x].cells[5].children[0].checked) ? "1" : "0";//imputable GASVI
                            aNN[i][10] = tblNaturalezas.rows[x].getAttribute("idValid");//Id ficepi validador GASVI
                            aNN[i][11] = tblNaturalezas.rows[x].cells[7].children[0].innerHTML;//validador GASVI

                            aNN[i][12] = "1";//Indica que el nodo ya ha sido cargado con los valores de parametrización
                            break;
                        }
                    }
                    actualizarCountNaturalezas(nIdNodoActivo, iCountNat);
                }

            } else if (bNodoEncontrado) break;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los datos de nodos y naturalezas.", e.message);
    }
}

function getValorNaturalezas() {
    try {
        var bNodoEncontrado = false;
        var dDesde;
        var dHasta;
        var tblNaturalezas = $I("tblNaturalezas");
        var nCount = 0;
        var bNodoNatMarcado = false;
        var bActualizarContadorNodo = false;

        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                bNodoEncontrado = true;
                for (var x = 0; x < aNat.length; x++) {
                    if (aNN[i][1] == aNat[x][0]) {
                        if (aNN[i][12] != "1") {//Es la primera vez que se carga ese nodo->hay que coger la parametrización de la naturaleza
                            bActualizarContadorNodo = true;
                            bNodoNatMarcado = (tblNaturalezas.rows[x].cells[0].children[0].checked) ? true : false;
                            if (aNN[i][13] == "1") {//El nodo-naturaleza está parametrizado
                                tblNaturalezas.rows[x].cells[0].children[0].checked = true;
                                nCount++;
                            }
                            else {
                                if (bNodoNatMarcado) nCount++;
                                aNN[i][2] = (tblNatMant.rows[x].cells[2].children[0].innerHTML);//meses vigencia
                                //para mantener en el array los nodos-naturaleza marcados para generar
                                aNN[i][4] = (bNodoNatMarcado) ? "1" : "0";//Generar
                                aNN[i][5] = (tblNatMant.rows[x].cells[3].children[0].checked) ? "1" : "0";//replica
                                aNN[i][6] = (tblNatMant.rows[x].cells[4].children[0].checked) ? "1" : "0";//hereda nodo
                                aNN[i][7] = (tblNatMant.rows[x].cells[5].children[0].checked) ? "1" : "0";//imputable GASVI
                                aNN[i][12] = "1";//Indica que el nodo ya ha sido cargado con los valores de parametrización
                            }
                        }
                        else {
                            tblNaturalezas.rows[x].cells[0].children[0].checked = (aNN[i][4] == "1") ? true : false;//Generar
                        }
                        tblNaturalezas.rows[x].setAttribute("idResp", aNN[i][8]);
                        tblNaturalezas.rows[x].setAttribute("idValid", aNN[i][10]);
                        
                        tblNaturalezas.rows[x].cells[3].children[0].checked = (aNN[i][5] == "1") ? true : false;//Replica

                        tblNaturalezas.rows[x].cells[4].children[0].checked = (aNN[i][6] == "1") ? true : false;//Hereda nodo
                        tblNaturalezas.rows[x].cells[5].children[0].innerText = aNN[i][9];
                        tblNaturalezas.rows[x].cells[5].children[0].setAttribute("title", aNN[i][9]);

                        tblNaturalezas.rows[x].cells[6].children[0].checked = (aNN[i][7] == "1") ? true : false;//Imputable GASVI
                        tblNaturalezas.rows[x].cells[7].children[0].innerText = aNN[i][11];
                        tblNaturalezas.rows[x].cells[7].children[0].setAttribute("title", aNN[i][11]);
                        break;
                    }
                }
            }
            else if (bNodoEncontrado) break;
        }
        if (bActualizarContadorNodo)
            actualizarCountNaturalezas(nIdNodoActivo, nCount);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de nodos y naturalezas.", e.message);
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
            var oNF = $I("tblNaturalezas").insertRow(-1);
            oNF.style.height = "20px";
            oNF.id = aNat[i][0];

            oNF.setAttribute("plantilla", aNat[i][3]);
            oNF.attachEvent("onclick", mm);

            var oCtrl1 = document.createElement("input");
            oCtrl1.setAttribute("type", "checkbox");
            oCtrl1.setAttribute("style", "margin-left:5px;");
            oCtrl1.className = "check";
            oCtrl1.onclick = function () { setNatCount(this) };

            oNF.insertCell(-1).appendChild(oCtrl1);//generar -> 0

            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);//Denominación -> 1
            //oNF.cells[1].appendChild(oNobr.cloneNode(true), null);
            oNF.cells[1].children[0].className = "NBR";
            oNF.cells[1].children[0].setAttribute("style", "width:250px;");
            oNF.cells[1].children[0].attachEvent("onmouseover", TTip);
            oNF.cells[1].children[0].innerText = aNat[i][1];

            oNC3 = oNF.insertCell(-1);//Plantilla -> 2
            if (aNat[i][3] != 0) oNC3.appendChild(oImgPlant.cloneNode(true));

            var oCtrl2 = document.createElement("input");//Replica -> 3
            oCtrl2.setAttribute("type", "checkbox");
            oCtrl2.setAttribute("style", "margin-left:20px;");
            oCtrl2.className = "check";
            oCtrl2.onclick = function () { setElementoCheck(this, 'R') };
            oNF.insertCell(-1).appendChild(oCtrl2);

            var oCtrl3 = document.createElement("input");//hereda nodo -> 4
            oCtrl3.setAttribute("type", "checkbox");
            oCtrl3.setAttribute("style", "margin-left:20px;");
            oCtrl3.className = "check";
            oCtrl3.onclick = function () { setElementoCheck(this, 'H') };
            oNF.insertCell(-1).appendChild(oCtrl3);

            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);//Responsable proyecto -> 5
            oNF.cells[5].appendChild(oNobr.cloneNode(true), null);
            oNF.cells[5].children[0].className = "NBR";
            oNF.cells[5].children[0].setAttribute("style", "width:95px;");
            oNF.cells[5].children[0].attachEvent("onmouseover", TTip);

            var oCtrl4 = document.createElement("input");//imputable GASVI -> 6
            oCtrl4.setAttribute("type", "checkbox");
            oCtrl4.setAttribute("style", "margin-left:20px;");
            oCtrl4.className = "check";
            oCtrl4.onclick = function () { setElementoCheck(this, 'I') };
            oNF.insertCell(-1).appendChild(oCtrl4);

            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);//Validador GASVI -> 7
            oNF.cells[7].appendChild(oNobr.cloneNode(true), null);
            oNF.cells[7].children[0].className = "NBR";
            oNF.cells[7].children[0].setAttribute("style", "width:95px;");
            oNF.cells[7].children[0].attachEvent("onmouseover", TTip);
        }
        bNaturalezasCreadas = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla de naturalezas.", e.message);
    }
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
function setNatCount(oChk) {
    try {
        var tblNodos = $I("tblNodos");
        for (var i = 0; i < tblNodos.rows.length; i++) {
            if (tblNodos.rows[i].id == nIdNodoActivo) {
                var nCount = parseInt(tblNodos.rows[i].cells[3].innerText, 10);
                if (oChk.checked)
                    nCount++;
                else
                    nCount--;
                tblNodos.rows[i].cells[3].innerText = nCount;
                break;
            }
        }
        var idNat=oChk.parentElement.parentElement.getAttribute("id");
        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                if (aNN[i][1] == idNat)
                {
                    if (oChk.checked)
                        aNN[i][4] = "1";
                    else
                        aNN[i][4] = "0";
                    break;
                }
            }
            if (aNN[i][0] > nIdNodoActivo)
                break;
        }

        bHayCambios = true;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las naturalezas asociadas al " + strEstructuraNodo + ".", e.message);
    }
}
function setElementoCheck(oChk, tipo) {
    try {
        var idCol=0;
        switch (tipo) {
            case "R"://replica
                idCol = 5;
                break;
            case "H"://hereda nodo
                idCol = 6;
                break;
            case "I"://imputable GASVI
                idCol = 7;
                break;
            default:
                idCol = 0;
                break;
        }
        if (idCol == 0) return;
        var idNat = oChk.parentElement.parentElement.getAttribute("id");
        for (var i = 0; i < aNN.length; i++) {
            if (aNN[i][0] == nIdNodoActivo) {
                if (aNN[i][1] == idNat) {
                    if (oChk.checked)
                        aNN[i][idCol] = "1";
                    else
                        aNN[i][idCol] = "0";
                    break;
                }
            }
            if (aNN[i][0] > nIdNodoActivo)
                break;
        }

        bHayCambios = true;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al establece el atributo " + idCol + " a las naturalezas asociadas al " + strEstructuraNodo + ".", e.message);
    }
}

function marcarTodo() {
    try {
        var tblNaturalezas = $I("tblNaturalezas");
        var tblNodos = $I("tblNodos");
        var nNaturalezas = tblNatMant.rows.length;

        for (var i = 0; i < tblNodos.rows.length; i++) {
            tblNodos.rows[i].cells[3].innerText = nNaturalezas;
        }
        for (var i = 0; i < tblNaturalezas.rows.length; i++) {
            tblNaturalezas.rows[i].cells[0].children[0].checked = true;
        }
        for (var i = 0; i < aNN.length; i++) {
            aNN[i][3] = 1;
        }
        bHayCambios = true;
        bCambios = true;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar todas las naturalezas de todos los " + strEstructuraNodo + ".", e.message);
    }
}
function desmarcarTodo() {
    try {
        var tblNaturalezas = $I("tblNaturalezas");
        var tblNodos = $I("tblNodos");

        for (var i = 0; i < tblNodos.rows.length; i++) {
            tblNodos.rows[i].cells[3].innerText = 0;
        }
        for (var i = 0; i < tblNaturalezas.rows.length; i++) {
            tblNaturalezas.rows[i].cells[0].children[0].checked = false;
        }
        for (var i = 0; i < aNN.length; i++) {
            aNN[i][3] = 0;
        }
        bHayCambios = true;
        bCambios = true;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al desmarcar todas las naturalezas de todos los " + strEstructuraNodo + ".", e.message);
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
                    $I("hdnIdValidador").value = aDatos[2];//Recojo el IdFicepi
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
                tblNaturalezas.rows[x].cells[5].children[0].innerHTML = $I("txtResponsable").value;
                tblNaturalezas.rows[x].cells[5].children[0].setAttribute("title", $I("txtResponsable").value);
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
        //if ($I("hdnIdValidador").value == "") {
        //    mmoff("War", "Debes seleccionar validador para asignarlo a las naturalezas",420)
        //    return;
        //}
        var tblNaturalezas = $I("tblNaturalezas");
        var sw = 0;
        for (var x = 0; x < tblNaturalezas.rows.length; x++) {
            if (tblNaturalezas.rows[x].className == "FS") {
                sw = 1;
                //Id ficepi validador GASVI
                tblNaturalezas.rows[x].cells[7].setAttribute("idValid", $I("hdnIdValidador").value);
                //validador GASVI
                tblNaturalezas.rows[x].cells[7].children[0].innerHTML = $I("txtValidador").value;
                tblNaturalezas.rows[x].cells[5].children[0].setAttribute("title", $I("txtValidador").value);
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
                aNN[i][8] = $I("hdnIdResponsable").value;
                aNN[i][9] = $I("txtResponsable").value;
                break;
            }
        }
    }
}
function actualizarArrayValidador(nIdNodoActivo, idNaturaleza) {
    for (var i = 0; i < aNN.length; i++) {
        if (aNN[i][0] == nIdNodoActivo) {
            if (aNN[i][1] == idNaturaleza) {
                aNN[i][10] = $I("hdnIdValidador").value;
                aNN[i][11] = $I("txtValidador").value;
                break;
            }
        }
    }
}
//function delResponsable() {
//    $I("hdnIdResponsable").value = "";
//    $I("txtResponsable").value = "";
//}
function delValidador() {
    $I("hdnIdValidador").value = "";
    $I("txtValidador").value = "";
}
function marcarDesmarcarCol(nOpcion, nCol) {
    try {
        for (var i = 0; i < $I("tblNatMant").rows.length; i++) {
            $I("tblNatMant").rows[i].cells[nCol].children[0].checked = (nOpcion == 1) ? true : false;
        }
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar columna.", e.message);
    }
}
