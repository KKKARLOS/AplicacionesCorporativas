var idUser;
var aFigIni = new Array(); ;
var idInvAct = "";
function init() {
    try {
        if (!mostrarErrores()) return;
       
        iniciarPestanas();
        if ($I("hdnOrigen").value =="MantFiguras")
        {
            getDatos("1");
            initDragDropScript();
        }
        
        //        if ($I("hdnFigIni").value != ""){
        //            eval($I("hdnFigIni").value);
        //            $I("hdnFigIni").value="";
        //        }
        //            $I("imgPapeleraFiguras").onmouseover = function() { setTarget(this); };

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload() 
{
    //salir();
}
function grabarSalir()
{
    bSalir = true;
    grabar();
}
function grabarAux() {
    bSalir = false;
    grabar();
}
function salir() {
    var returnValue = $I("txtDenominacion").value;
    var sMsg = "Datos modificados. ¿Deseas grabarlos?";

    if (bCambios) {
        if (aPestGral[1].bModif) {
            var iSinFigura = 0;
            var iFila = 0;
            //Control de las figuras
            for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                    var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                    if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                        tsPestanas.setSelectedIndex(1);
                        ms($I("tblFiguras2").rows[i]);
                        iSinFigura = 1;
                        iFila = i;
                        break;
                    }
                }
            }
            if (iSinFigura == 1) {
                sMsg = "Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar con la grabación?";
            }
        }
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
        mostrarErrorSQL(aResul[3], aResul[2]);
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

                if (aPestGral[1].bModif == true) {
                    for (var i = $I("tblFiguras2").rows.length - 1; i >= 0; i--) {
                        if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D") {
                            $I("tblFiguras2").deleteRow(i);
                        } else if ($I("tblFiguras2").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblFiguras2").rows[i], "N");
                        }
                    }
                }
                if (aPestGral[2].bModif == true) {
                    for (var i = $I("tblDatosNodo").rows.length - 1; i >= 0; i--) {
                        if ($I("tblDatosNodo").rows[i].getAttribute("bd") == "D") {
                           borrarNODODeArray(idUser, $I("tblDatosNodo").rows[i].id);
                           $I("tblDatosNodo").deleteRow(i);                            
                        } else if ($I("tblDatosNodo").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblDatosNodo").rows[i], "N");
                            oNODOActivo = buscarNODOEnArray(idUser, $I("tblDatosNodo").rows[i].id);
                            if (oNODOActivo != null)
                                oNODOActivo.bd = "";
                        }
                    }
                }
                                
                reIniciarPestanas();                
                aPestGral[2].bLeido = false;
                /*                
                //Vuelvo a traer los invitados
                $I("divInvitados").children[0].innerHTML = aResul[3];
                scrollTablaInvitados();
                eval(aResul[4]);               
                //scrollTablaInvitados();
                bCambios = false;
                if (aResul[5]==""){
                //Vacío la tabla de nodos
                RefrescarNodos("*");
                }
                else{
                //Cargo los nodos del invitado que tenía seleccionado
                //RefrescarNodos(aResul[5]);
                idInvAct= aResul[5];
                setTimeout("invitadoActivo();",50);
                }    
                */
                ocultarProcesando();
                $I("divBoxeo").style.visibility = "hidden";
                ocultarIncompatibilidades();
                mmoff("Suc", "Grabación correcta", 160);
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                if (bSalir)
                    setTimeout("salir()", 50);
                break;
            case "provinciasGtonPais":
                var aDatos = aResul[2].split("///");
                var j = 1;
                $I("cboProvGes").length = 0;

                var opcion = new Option("", "");
                $I("cboProvGes").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    opcion.setAttribute("zona",aValor[2],0);
                    opcion.setAttribute("ambito",aValor[3],0);

                    $I("cboProvGes").options[j] = opcion;
                    j++;
                }
                $I("txtAmbito").value = "";
                $I("txtZona").value = "";
                
                break;

            case "provinciasFisPais":
                var aDatos = aResul[2].split("///");
                var j = 1;
                $I("cboProvFis").length = 0;

                var opcion = new Option("", "");
                $I("cboProvFis").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboProvFis").options[j] = opcion;
                    j++;
                }
                break;

            case "segmentosSector":
                var aDatos = aResul[2].split("///");
                var j = 1;
                $I("cboSegmento").length = 0;

                var opcion = new Option("", "");
                $I("cboSegmento").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboSegmento").options[j] = opcion;
                    j++;
                }
                break;                  
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function obtenerProvinciasGtonPais(sPais) {
    try {
        if (sPais == "") {
            $I("cboProvGes").length = 1;
            return;
        }

        var js_args = "provinciasGtonPais@#@";
        js_args += sPais;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenerProvinciasGtonPais ", e.message);
    }
}

function obtenerAmbitoZona() {
    try {
        $I("txtAmbito").value = $I("cboProvGes").options[$I("cboProvGes").selectedIndex].getAttribute("ambito");
        $I("txtZona").value = $I("cboProvGes").options[$I("cboProvGes").selectedIndex].getAttribute("zona");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenerProvinciasGtonPais ", e.message);
    }
}
function obtenerProvinciasFisPais(sPais) {
    try {
        if (sPais == "") {
            $I("cboProvFis").length = 1;
            return;
        }

        var js_args = "provinciasFisPais@#@";
        js_args += sPais;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenerProvinciasFisPais ", e.message);
    }
}
function obtenerSegmentosSector(sSector) {
    try {
        if (sSector == "") {
            $I("cboProvGes").length = 1;
            return;
        }

        var js_args = "segmentosSector@#@";
        js_args += sSector;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenersegmentosSector ", e.message);
    }
}
function invitadoActivo() {
    try {
        for (var i = $I("tblInvitados").rows.length - 1; i >= 0; i--) {
            if ($I("tblInvitados").rows[i].id == idInvAct) {

                ms($I("tblInvitados").rows[i]);
             
                break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al recargar el invitado activo", e.message);
    }
}
function comprobarDatos() {
    try {
        if ($I("hdnIDResponsable").value == "0") {
            mmoff("War","Se debe indicar el responsable del cliente",300);
            return false;
        }
        if ($I("cboSegmento").value == "") {
            mmoff("War", "Se debe indicar el segmento", 210);
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
        var iSinFigura = 0;

        if (aPestGral[1].bModif) {
            var iFila = 0;
            //Control de las figuras
            for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                    var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                    if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                        tsPestanas.setSelectedIndex(1);
                        ms($I("tblFiguras2").rows[i]);
                        iSinFigura = 1;
                        iFila = i;
                        break;
                    }
                }
            }
            if (iSinFigura == 1)
            {
                jqConfirm("", "¡ Atención !<br><br>Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                    if (answer) {
                        $I("tblFiguras2").rows[iFila].setAttribute("bd", "D");
                        LLamarGrabar();
                    }
                    else return false;
                });
            } else LLamarGrabar();

        } else LLamarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}
function grabar2() {
    try {
        LLamarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}
function LLamarGrabar() {
    try {
        var js_args = "grabar@#@";
        js_args += grabarP0(); //datos generales
        js_args += "@#@";
        js_args += grabarP1(); //figuras
        js_args += "@#@";
        js_args += grabarP2(); //nodos de invitados 
        js_args += "@#@" + idInvAct; //Paso el invitado seleccionado para a la vuelta cargar sus nodos

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}

function grabarP0() {
    var sb = new StringBuilder;
    if (aPestGral[0].bModif) {
        sb.Append($I("hdnIDResponsable").value + "##"); //0
        sb.Append(($I("chkAlertas").checked == true) ? "1##" : "0##"); //1
        sb.Append(($I("chkCualiCVT").checked == true) ? "1##" : "0##"); //2
        sb.Append($I("cboProvGes").value + "##"); //3
        sb.Append($I("cboProvFis").value + "##"); //4
        sb.Append($I("cboSegmento").value + "##"); //5
    }
    return sb.ToString();
}
function grabarP1() {
    var sb = new StringBuilder;
    var sbFilaAct = new StringBuilder;
    var bGrabar = false;
    if (aPestGral[1].bModif) {
        //Control de las figuras
        for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
            bGrabar = false;
            sbFilaAct = new StringBuilder;
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != "") {
                sbFilaAct.Append($I("tblFiguras2").rows[i].getAttribute("bd") + "##"); //0
                sbFilaAct.Append($I("tblFiguras2").rows[i].id + "##"); //1
                if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D") {
                    //Si voy a borrar una figura no tiene sentido hacer nada con sus nodos
                    bGrabar = true;
                    borrarUserDeArray($I("tblFiguras2").rows[i].id);
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
    if (aPestGral[2].bModif) {
        //Control de los nodos de los invitados
        for (var nIndice = 0; nIndice < aNODOS.length; nIndice++) {
            if (aNODOS[nIndice].bd != "") {
                sb.Append(aNODOS[nIndice].bd + "#");
                sb.Append(aNODOS[nIndice].idUser + "#");
                sb.Append(aNODOS[nIndice].idNODO + "/");
            }
        }
    }
    return sb.ToString();
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
    /*
    if (nName == 'firefox') {
    //alert('Lo mando al setTarget'+oElement.outerHTML);
    setTarget($I("imgPapeleraFiguras"));
    }
    */
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
                    if (nOpcionDD == 3) {
                        aG(1);
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

                            oNF.setAttribute("id", oRow.getAttribute("id"));
                            oNF.setAttribute("bd", "I");
                            oNF.setAttribute("style", "height:22px");
                            oNF.id = oRow.getAttribute("id");
                            oNF.setAttribute("bd","I");
                            oNF.style.height = "20px";

                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);

                            oNC1 = oNF.insertCell(-1);
                            oNC1.appendChild(oImgFI.cloneNode(true), null);

                            oNC2 = oNF.insertCell(-1);

                            //if (oRow.sexo=="V"){
                            if (oRow.getAttribute("sexo") == "V") {
                                //switch (oRow.tipo) {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNC2.appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNC2.appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            } else {
                                //switch (oRow.tipo){
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                                    case "P": oNC2.appendChild(oImgPM.cloneNode(true), null); break;
                                    case "F": oNC2.appendChild(oImgFM.cloneNode(true), null); break;
                                }
                            }
                            oNC3 = oNF.insertCell(-1);

                            var oCtrl1 = document.createElement("nobr");
                            //oCtrl1.setAttribute("style", "width:295px");

                            //oCtrl1.setAttribute((ie) ? "className": "class", "NBR W270");                            
                            oCtrl1.appendChild(document.createTextNode((ie) ? oRow.cells[1].innerText : oRow.cells[1].textContent));
                            oNC3.appendChild(oCtrl1);

                            oNC4 = oNF.insertCell(-1);
                            var oCtrl2 = document.createElement("div");
                            var oCtrl3 = document.createElement("ul");
                            oCtrl3.setAttribute("id", "box-" + oRow.id);
                            oCtrl2.appendChild(oCtrl3);
                            oNC4.appendChild(oCtrl2);
                            initDragDropScript();

                            aG(1);
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
            if (ie) {
                if ($I("tblFiguras2").rows[x].cells[2].innerText == oFila.cells[1].innerText) {
                    //alert("Profesional ya incluido");
                    return;
                }
            }
            else {
                if ($I("tblFiguras2").rows[x].cells[2].textContent == oFila.cells[1].textContent) {
                    //alert("Profesional ya incluido");
                    return;
                }
            }
        }

        var oNF = $I("tblFiguras2").insertRow(-1);

        oNF.id = oFila.getAttribute("id");
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";

        //oNF.setAttribute("id", oFila.getAttribute("id"));
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:22px");

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        //(typeof oNF.attachEvent != 'undefined') ? oNF.attachEvent('onclick', mm) : oNF.addEventListener('click', mm, false);
        //(typeof oNF.attachEvent != 'undefined') ? oNF.attachEvent('onmousedown', DD) : oNF.addEventListener('mousedown', DD, false);

        //oNC1 = oNF.insertCell().appendChild(document.createElement("<img src='../../../../images/imgFI.gif'>"));

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true), null);

        oNC2 = oNF.insertCell(-1);

        //if (oRow.sexo=="V"){
        if (oFila.getAttribute("sexo") == "V") {
            //switch (oRow.tipo) {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                case "P": oNC2.appendChild(oImgPV.cloneNode(true), null); break;
                case "F": oNC2.appendChild(oImgFV.cloneNode(true), null); break;
            }
        } else {
            //switch (oRow.tipo){
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                case "P": oNC2.appendChild(oImgPM.cloneNode(true), null); break;
                case "F": oNC2.appendChild(oImgFM.cloneNode(true), null); break;
            }
        }

        oNC3 = oNF.insertCell(-1);

        var oCtrl1 = document.createElement("nobr");
        //oCtrl1.setAttribute("style", "width:295px");
        //oCtrl1.setAttribute((ie) ? "className" : "class", "NBR W270");
        oCtrl1.className="NBR W270";
        oCtrl1.appendChild(document.createTextNode(oFila.cells[1].innerText));
        oNC3.appendChild(oCtrl1);

        oNC4 = oNF.insertCell(-1);

        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNC4.appendChild(oCtrl2);

        initDragDropScript();

        aG(1);

        $I("divFiguras2").scrollTop = $I("tblFiguras2").rows[$I("tblFiguras2").rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura", e.message);
    }
}


function getResponsable() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getProfesional.aspx";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDResponsable").value = aDatos[0];
                    $I("txtDesResponsable").value = aDatos[1];
                    aG(0);
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}


//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
var tsPestanas = null;
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

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
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
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}

/*
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (i = 1; i < 4; i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (i = 1; i < 4; i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}


function getPestana() {
    try {
        if ($I('procesando').style.visibility == 'visible') return;
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        switch (event.srcElement.id) {
            case "tsPestanas":
                if (!aPestGral[event.srcElement.selectedIndex].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(event.srcElement.selectedIndex);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}
*/
function getDatos(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana + "@#@";
        js_args += $I("hdnID").value;
        
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
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
                //alert("respuesta:" + aResul[0]);
                
                $I("divFiguras2").children[0].innerHTML = aResul[0];
                $I("divFiguras2").children[0].children[0].style.backgroundImage = "url('../../../../Images/imgFT22.gif')";
                initDragDropScript();
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                eval(aResul[1]);
                break;
            case "2": //Nodos de invitados
                $I("divInvitados").children[0].innerHTML = aResul[0];
                scrollTablaInvitados();
                //initDragDropScript();
                //actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                eval(aResul[1]);
                RefrescarNodos("*");
                //Si no está cargada la primera pestaña, la cargo
                if (!aPestGral[1].bLeido) {
                    setTimeout("getDatos1();", 100);
                }
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function getDatos1() {
    getDatos(1);
}
function aG(iPestana) {//Sustituye a activarGrabar
    try {
        if (!bCambios) {
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
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
            if ((oNuevo.id == "D" || oNuevo.id == "C" || oNuevo.id == "I")
                    &&
                 (aLista[i].id == "D" || aLista[i].id == "C" || aLista[i].id == "I")) {
                /*                 
                $I("popupWin_content").parentNode.style.left = "550px";
                $I("popupWin_content").parentNode.style.top = "200px";
                $I("popupWin_content").parentNode.style.width = "266px";
                $I("popupWin_content").style.width = "260px";
                if (ie)
                $I("popupWin_content").innerText = "Figura no insertada por incompatibilidad.";
                else
                $I("popupWin_content").TextContent = "Figura no insertada por incompatibilidad.";

                popupWinespopup_winLoad();
                */
                mmoff("War","Figura no insertada por incompatibilidad.", 260, null, null, 550, 200);
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
        var nUltFila;
        
        if ($I("divFiguras1").offsetHeight != 'undefined')
            nUltFila = Math.min(nFilaVisible + $I("divFiguras1").offsetHeight / 20 + 1, $I("tblFiguras1").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divFiguras1").innerHeight / 20 + 1, $I("tblFiguras1").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblFiguras1").rows[i].getAttribute("sw")) {
                oFila = $I("tblFiguras1").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
                //(typeof oFila.attachEvent != 'undefined') ? oFila.attachEvent('onclick', mm) : oFila.addEventListener('click', mm, false);
                //(typeof oFila.attachEvent != 'undefined') ? oFila.attachEvent('onmousedown', DD) : oFila.addEventListener('mousedown', DD, false);
                oFila.ondblclick = function() { insertarFigura(this) };


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
//////////////////////////////////////
// FUNCIONES PARA LA TABLA DE NODOS //
//////////////////////////////////////
var nTopScrollInv = 0;
var nIDTimeInv = 0;
function scrollTablaInvitados() {
    try {
        if ($I("divInvitados").scrollTop != nTopScroll) {
            nTopScrollInv = $I("divInvitados").scrollTop;
            clearTimeout(nIDTimeInv);
            nIDTimeInv = setTimeout("scrollTablaInvitados()", 50);
            return;
        }
        clearTimeout(nIDTimeInv);

        var nFilaVisible = Math.floor(nTopScrollInv / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divInvitados").offsetHeight / 20 + 1, $I("tblInvitados").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblInvitados").rows[i].getAttribute("sw")) {
                oFila = $I("tblInvitados").rows[i];
                oFila.setAttribute("sw", 1);

                //oFila.onclick = function(){mmse(this)};
                //                oFila.onclick = function(){mostrarNodos(this)};
                //                oFila.onmousedown = function(){DD(this)};

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(false), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(false), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(false), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(false), null); break;
                    }
                }

                //                if (oFila.baja=="1")
                //                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de invitados.", e.message);
    }
}
function nuevoNodo() {
    try {
        if (idUser == "" || idUser == null) {
            mmoff("War","Para insertar valores, debe seleccionar el profesional", 400);
            return;
        }
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");

                    oNF = document.getElementById("tblDatosNodo").insertRow(-1);
                    var iFila = oNF.rowIndex;

                    oNF.id = aDatos[0]; //idNodo
                    oNF.setAttribute("bd", "I");
                    oNF.style.height = "20px";
                    oNF.setAttribute("orden", oNF.rowIndex);
                    oNF.attachEvent('onclick', mm);

                    oNC1 = oNF.insertCell(-1);
                    oNC1.appendChild(oImgFI.cloneNode(true), null);

                    oNC2 = oNF.insertCell(-1);

                    var oCtrl1 = document.createElement("nobr");
                    oCtrl1.className = "NBR W380";
                    oCtrl1.appendChild(document.createTextNode(aDatos[1]));
                    oNC2.appendChild(oCtrl1);

                    ms(oNF);

                    insertarNODOEnArray("I", idUser, aDatos[0], aDatos[1]);
                    aG(2);
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function EliminarNodo() {
    try {
        var sw = 0;
        aFilaNODO = FilasDe("tblDatosNodo");
        if (aFilaNODO == null) {
            mmoff("War","Selecciona la fila a eliminar", 200);
            return;
        }
        if (aFilaNODO.length == 0) {
            mmoff("War","Selecciona la fila a eliminar", 200);
            return;
        }
        for (var i = aFilaNODO.length - 1; i >= 0; i--) {
            if (aFilaNODO[i].className == "FS" || aFilaNODO[i].getAttribute("class") == "FS") {
                sw = 1;
                if (aFilaNODO[i].getAttribute("bd") == "I") {
                    //Si es una fila nueva, se elimina
                    borrarNODODeArray(idUser, aFilaNODO[i].id);
                    document.getElementById("tblDatosNodo").deleteRow(i);
                }
                else {
                    mfa(aFilaNODO[i], "D");
                    oNODOActivo = buscarNODOEnArray(idUser, aFilaNODO[i].id);
                    if (oNODOActivo.bd != "I")
                        oNODOActivo.bd = "D";
                    else
                        borrarNODODeArray(idUser, aFilaNODO[i].id);
                }
            }
        }
        if (sw == 0) mmoff("War","Selecciona la fila a eliminar", 200);
        else {
            bCambios = false;
            aG(2);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
/*
function actualizarDatos(accion, clave, obj) {
    try {
        var oFila = obj.parentNode.parentNode;
        //if (oFila.bd != "I") oFila.bd = "U";
        fm(oFila);
        activarGrabar();
        oNODOActivo = buscarNODOEnArray(oFila.id);
        oNODOActualizar(accion, clave, obj)
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los datos", e.message);
    }
}
*/
function RefrescarNodos(id) {
    try {
        if (id == "*") {
            document.getElementById("divNodos").children[0].innerHTML = "<table id='tblDatosNodo'></table>";
            return;
        }
        idInvAct = id;
        idUser = id;
        var sb = new StringBuilder;
        var sImagen = "imgFN.gif";
        sb.Append("<table id='tblDatosNodo' class='texto MANO' style='width: 400px;' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:380px;' /></colgroup>");
        //sb.Append("<tbody id='tbodyDatosNodo'>");
        for (var nIndice = 0; nIndice < aNODOS.length; nIndice++) {
            if (aNODOS[nIndice].idUser == id) {
                sb.Append("<tr id='" + aNODOS[nIndice].idNODO + "' style='height:20px' bd='" + aNODOS[nIndice].bd + "' onclick='mm(event)' >");
                switch (aNODOS[nIndice].bd) {
                    case "I":
                        sImagen = "imgFI.gif";
                        break;
                    case "U":
                        sImagen = "imgFU.gif";
                        break;
                    case "D":
                        sImagen = "imgFD.gif";
                        break;
                    default:
                        sImagen = "imgFN.gif";
                        break;
                }
                sb.Append("<td><img src='../../../../images/" + sImagen + "'></td>");
                //sb.Append("<td><input type='text' id='txtNodo' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:310px' value='" + Utilidades.unescape(aNODOS[nIndice].nombre) + "' MaxLength='50'></td>");
                sb.Append("<td>" + Utilidades.unescape(aNODOS[nIndice].nombre) + "</td>");
                sb.Append("</tr>");
            }
        }
        sb.Append("</tbody>");
        sb.Append("</table>");

        document.getElementById("divNodos").children[0].innerHTML = sb.ToString();
        //tbodyNODO = document.getElementById('tbodyDatosNodo'); 
        //tbodyNODO.onmousedown = startDragIMGvae; 
    } catch (e) {
        mostrarErrorAplicacion("Error al refrescar los valores", e.message);
    }
}
function restaurarFila2() {
    try {
        //Miro si estoy restaurando una fila de figura o de nodo
        var sAux = oFilaARestaurar.innerHTML;
        var intPos = sAux.indexOf("W280");
        if (intPos >= 0) { return; } //estoy restaurando una figura luego no hago nada con el array
        else {
            oNODOActivo = buscarNODOEnArray(idUser, oFilaARestaurar.id);
            if (oNODOActivo != null)
                oNODOActivo.bd = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al restaurar la fila", e.message);
    }
}
function getIdUser(idOld, sCadena) {
    try {
        var sElem, sRes = "";
        var aLista = sCadena.split("@@");
        for (var i = 0; i < aLista.length; i++) {
            sElem = aLista[i];
            if (sElem != "") {
                var aUser = sElem.split("##");
                if (aUser[0] == idOld) {
                    sRes = aAE[1];
                    break;
                }
            }
        }
        return sRes;
    } catch (e) {
        mostrarErrorAplicacion("Error al buscar el código del profesional insertado", e.message);
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


/*
function eventos(oFila) {
    (typeof oFila.attachEvent != 'undefined') ? oFila.attachEvent('onclick', mm) : oFila.addEventListener('click', mm, false);

    if (oFila.onclick == null) oFila.onclick = function() { RefrescarNodos(this.id); };
}

function eventos2(oFila) {
if (typeof document.attachEvent != 'undefined') {
if (oFila.onclick == null) {
oFila.attachEvent('onclick', mm);
oFila.attachEvent('onmousedown', DD);
}
} else {
if (oFila.onclick == null) {
oFila.addEventListener('click', mm, false);
oFila.addEventListener('mousedown', DD, false);
}
}
}
*/


