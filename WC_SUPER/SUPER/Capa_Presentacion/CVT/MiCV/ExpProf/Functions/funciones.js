var aFilaT, aACT, aACS;
var idNuevo = 1;
var bDatosModificados = false;

//var oCV = document.createElement("<select id ='cboCV' class='combo' onChange='fm(event);' ><option value=''>SIN VALOR</option><option value='S'>SI</option><option value='N'>NO</option></select>");
var oCV = document.createElement("select");
oCV.setAttribute("class", "combo");
//oCV.onchange = function() { fm(event); setCombo(event); };style.cursor = strCurMA

var oImgOpcional = document.createElement("img");
oImgOpcional.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOpcional.gif");
oImgOpcional.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border:0px; cursor:" + strCurMA);

var oImgProyTecOff = document.createElement("img");
oImgProyTecOff.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgProyTecOff.gif");
oImgProyTecOff.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;");

var oImgOrdenF = document.createElement("img");
oImgOrdenF.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOrdenF.gif");
oImgOrdenF.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border:0px; cursor:" + strCurMA);

//var oNoBr = document.createElement("<nobr class='NBR W110' style='vertical-align:top;padding-left:2px;width:105px; height:16px; padding-top:2px;' onmouseover='TTip(event)'></nobr>");

var oFAPE, oFBPE;

function init() {
    try {
        if (!mostrarErrores()) return;
        oFAPE = document.createElement("input");
        oFAPE.setAttribute("type", "text");
        oFAPE.className = "txtL";
        oFAPE.id = "Fini";
        oFAPE.setAttribute("name", "txtFI");
        oFAPE.value = '';
        oFAPE.setAttribute("valAnt", "");
        oFAPE.setAttribute("style", "width:60px; cursor:pointer");
        oFAPE.setAttribute("Calendar", "oCal");
        oFAPE.setAttribute("goma", "0");
        //oFAPE.onchange = function() { mm(event); controlarFecha('I') };
        //oFAPE.attachEvent('onchange', contFechI);

        oFBPE = document.createElement("input");
        oFBPE.setAttribute("type", "text");
        oFBPE.className = "txtL";
        oFBPE.id = "Ffin";
        oFBPE.setAttribute("name", "txtFF");
        oFBPE.value = '';
        oFBPE.setAttribute("valAnt", "");
        oFBPE.setAttribute("style", "width:60px; cursor:pointer");
        oFBPE.setAttribute("Calendar", "oCal");
        //oFBPE.setAttribute("goma", "0");
        oFBPE.setAttribute("cRef", "txtFI");
        
        scrollTablaProf();

        aFilaT = FilasDe("tblProf");
        if ($I("hdnModo").value == "R") {
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
        }

        if ($I("hdnEP").value == "-1") {//Vamos a crear una experiencia profesional nueva
            (ie) ? $I("lblProyRef").className = "enlace" : $I("lblProyRef").setAttribute("class", "enlace");
            $I("lblProyRef").attachEvent('onclick', getProyecto);
            $I("hdnSelPR").value = "S";
        }
        else {
            if ($I("hdnSelPR").value == "S") {
                (ie) ? $I("lblProyRef").className = "enlace" : $I("lblProyRef").setAttribute("class", "enlace");
                $I("lblProyRef").attachEvent('onclick', getProyecto);
            }
        }

        ocultarProcesando();

        if (bHayExperiencias) {
            mmoff("WarPer", "Existen experiencias profesionales para este cliente. Si quieres relacionar este proyecto con una experiencia anterior selecciona la opción de \"Asociar\".", 500);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function setCombo(e) {
    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;
    var bFila = false;
    while (!bFila) {
        if (oElement.tagName.toUpperCase() == "TR") bFila = true;
        else {
            oElement = oElement.parentNode;
            if (oElement == null)
                return;
        }
    }
    oElement.setAttribute("cv", $I("cboCV-" + oElement.getAttribute("id") + "-" + oElement.rowIndex).value);
    oElement.cells[8].style.backgroundImage = "";
    lineaModificada(true);
}

function contFechI() {
    controlarFecha('I');
}
function contFechF() {
    controlarFecha('F');
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                var sDatos = aResul[2].split("#item#");
                bDatosModificados = true;
                desActivarGrabar();
                $I("hdnSelPR").value = "N";
                $I("lblProyRef").detachEvent("onclick", getProyecto);
                if (ie) {
                    $I("lblProyRef").className = "texto";
                }
                else {
                    $I("lblProyRef").setAttribute("class", "texto");
                }
                $I("nIdProy").value = "-1";
                $I("hdnEP").value = sDatos[0];
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) {
                    salir();
                } else {
                    var sId = sDatos[1].split("#dato#");
                    var x = 0;
                    for (var i = aFilaT.length - 1; i >= 0; i--) {
                        if (aFilaT[i].getAttribute("bd") != "")
                            mfa(aFilaT[i], "N");
                        if (aFilaT[i].getAttribute("id") == "-1") {
                            aFilaT[i].setAttribute("id", sId[x]);
                            x++;
                        }
                    }
                    for (var j = 0; i < aFilaT.length; i++) {
                        if (aFilaT[j].cells[8].children[0]) //Para que solo quite el combo si existe
                        {
                            aFilaT[j].cells[8].removeChild(aFilaT[j].cells[8].children[0]);
                            switch (aFilaT[j].getAttribute("cv")) {
                                case "S":
                                    aFilaT[j].cells[8].innerText = "Sí";
                                    break;
                                case "N":
                                    aFilaT[j].cells[8].innerText = "No";
                                    break;
                                case "P":
                                    aFilaT[j].cells[8].innerText = "Pdte.";
                                    break;
//                                case "":
//                                    aFilaT[i].cells[8].innerText = "Pdte.";
//                                    break;
                            }
                            aFilaT[j].cells[8].removeAttribute("title");
                        }
                    }
                    //actualizarLupas("tblTitulo", "tblProf");
                    if ($I("tblConTec") != null) {
                        aACT = FilasDe("tblConTec");
                        for (var x = aACT.length - 1; x >= 0; x--) {
                            if (aACT[x].getAttribute("bd") != "") {
                                if (aACT[x].getAttribute("bd") == "D")
                                    $I("tblConTec").deleteRow(x);
                                else
                                    mfa(aACT[x], "N");
                            }
                        }
                    }
                    if ($I("tblConSec") != null) {
                        aACS = FilasDe("tblConSec");
                        for (var k = aACS.length - 1; k >= 0; k--) {
                            if (aACS[k].getAttribute("bd") != "") {
                                if (aACS[k].getAttribute("bd") == "D")
                                    $I("tblConSec").deleteRow(k);
                                else
                                    mfa(aACS[k], "N");
                            }
                        }
                    }
                }
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

function grabar() {
    try {

        if (!comprobarDatos()) return;
        if (!comprobarCV()) {
            //mmoff("War", "Debes indicar a qué profesionales se les debe asignar o no la experiencia a su CV",400, 2000);
            return;
        }
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@" + $I("nIdProy").value + "@#@" + $I("hdnEP").value + "@#@" + $I("hdnPR").value + "@#@");
        var sAux = datosGenericos();
        sb.Append(sAux + "@#@");
        //Areas de conocimiento tecnologico
        sAux = datosACT();
        sb.Append(sAux + "@#@");
        //Areas de conocimiento sectorial
        sAux = datosACS();
        sb.Append(sAux + "@#@");
        //Profesionales
        sAux = datosProfesionales();
        sb.Append(sAux);

        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function datosGenericos() {
    var sb = new StringBuilder;
    sb.Append($I("txtDen").value + "##");
    sb.Append($I("txtDes").value + "##");
    sb.Append($I("hdnEnIb").value + "##");
    sb.Append($I("hdnCtaOri").value + "##");
    sb.Append($I("hdnCtaDes").value + "##");
    sb.Append($I("hdnCliProy").value + "##");
    sb.Append($I("hdnEmp").value);

    return sb.ToString();
}
function datosACT() {
    if ($I("tblConTec") == null) return;
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblConTec").rows.length; x++) {
        if ($I("tblConTec").rows[x].getAttribute("bd") == "I") {
            sb.Append("I##" + $I("tblConTec").rows[x].getAttribute("id") + "///");
        }
        else {
            if ($I("tblConTec").rows[x].getAttribute("bd") == "D")
                sb.Append("D##" + $I("tblConTec").rows[x].getAttribute("id") + "///");
        }
    }
    return sb.ToString();
}

function datosACS() {
    if ($I("tblConSec") == null) return;
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblConSec").rows.length; x++) {
        if ($I("tblConSec").rows[x].getAttribute("bd") == "I") {
            sb.Append("I##" + $I("tblConSec").rows[x].getAttribute("id") + "///");
        }
        else {
            if ($I("tblConSec").rows[x].getAttribute("bd") == "D")
                sb.Append("D##" + $I("tblConSec").rows[x].getAttribute("id") + "///");
        }
    }
    return sb.ToString();
}
function datosProfesionales() {
    var sb = new StringBuilder;
    var sIni, sFin, sId;

    for (var x = aFilaT.length-1; x >= 0; x--) {
        //if (aFilaT[x].getAttribute("bd") == "U" || aFilaT[x].getAttribute("bd") == "I") {
            sId = aFilaT[x].getAttribute("id");
            if (sId == "-1")
                sb.Append("I##");
            else
                sb.Append("U##");
            sb.Append(sId + "##");
            sb.Append(aFilaT[x].getAttribute("idF") + "##");
            sIni = getCelda(aFilaT[x], 6);
            sFin = getCelda(aFilaT[x], 7);
            sb.Append(sIni + "##");
            sb.Append(sFin + "##");
            //sb.Append($I("cboCV-" + sId).value + "##")
            sb.Append(aFilaT[x].getAttribute("cv") + "##");
            sb.Append(aFilaT[x].getAttribute("plantNew") + "##");
            sb.Append(aFilaT[x].getAttribute("validNew") + "##");
            sb.Append(getCelda(aFilaT[x], 2) + "##");
            sb.Append(aFilaT[x].getAttribute("esfJ") + "///");
        //}
    }
    return sb.ToString();
}
function hayAreaConTec() {
    bRes = false;
    for (var x = 0; x < $I("tblConTec").rows.length; x++) {
        if ($I("tblConTec").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}
function hayAreaConSec() {
    bRes = false;
    for (var x = 0; x < $I("tblConSec").rows.length; x++) {
        if ($I("tblConSec").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}

function comprobarDatos() {
    var sFecIni, sFecFin;

    if ($I("txtDen").value == "") {
        mmoff("War", "La denominación es obligatoria", 230, 2000);
        return false;
    }
    if ($I("txtDes").value == "") {
        mmoff("War", "La descripción es obligatoria", 230, 2000);
        return false;
    }
    if (!hayAreaConSec()) {
        mmoff("War", "La experiencia profesional debe tener asociado al menos un Area de conocimiento sectorial.", 450, 2000);
        return false;
    }
    if (!hayAreaConTec()) {
        mmoff("War", "La experiencia profesional debe tener asociado al menos un Area de conocimiento tecnologico.", 450, 2000);
        return false;
    }
    for (var i = 0; i < aFilaT.length; i++) {
        sFecIni = getCelda(aFilaT[i], 6);
        sFecFin = getCelda(aFilaT[i], 7);
        if (sFecIni == "") {
            //if (ie)
                aFilaT[i].click();
            //else {
            //    var clickEvent = window.document.createEvent("MouseEvent");
            //    clickEvent.initEvent("click", false, true);
            //    aFilaT[i].dispatchEvent(clickEvent);
            //}
            mmoff("War", "La fecha de alta es obligatoria", 230, 2000);
            return false;
        }
        //Fecha de inicio  > fecha de fin
        if (sFecFin != "" && !fechasCongruentes(sFecIni, sFecFin)) {
            //aFilaT[i].click();
            //if (ie)
                aFilaT[i].click();
            //else {
            //    var clickEvent2 = window.document.createEvent("MouseEvent");
            //    clickEvent2.initEvent("click", false, true);
            //    aFilaT[i].dispatchEvent(clickEvent2);
            //}
            mmoff("War", "La fecha de alta debe ser anterior a la fecha de baja", 350, 2000);
            return false;
        }
        //Si lleva curriculun hay que indicar plantilla o validador
        //if (aFilaT[i].getAttribute("cv") == "S" && aFilaT[i].getAttribute("validNew") == "" && aFilaT[i].getAttribute("plantNew") == "") {
        //    if (ie)
        //        aFilaT[i].click();
        //    else {
        //        var clickEven3t = window.document.createEvent("MouseEvent");
        //        clickEvent3.initEvent("click", false, true);
        //        aFilaT[i].dispatchEvent(clickEvent3);
        //    }
        //    mmoff("War", "Debes indicar validador o plantilla", 200, 3000);
        //    return false;
        //}
    }
    return true;
}

var nTopScrollAE = -1;
var nIDTimeAE = 0;
function scrollTablaProf() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollAE) {
            nTopScrollAE = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeAE);
            nIDTimeAE = setTimeout("scrollTablaProf()", 50);
            return;
        }
        var nFilaVisible = Math.floor(nTopScrollAE / 22);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 22 + 1, $I("tblProf").rows.length);
        var oFila;
        var sAux;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblProf").rows[i].getAttribute("sw")) {
                oFila = $I("tblProf").rows[i];
                oFila.setAttribute("sw", 1);
                if ($I("hdnModo").value != "R") {
                    //oFila.onclick = function() { profClick(event) };
                    oFila.attachEvent('onclick', profClick);
                }
                if (oFila.getAttribute("bd") != "I") {
                    oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);
                    oFila.cells[0].removeAttribute("title");
                }
                else {
                    oFila.cells[0].appendChild(oImgFI.cloneNode(true), null);
                    //oFila.cells[0].setAttribute("title", "Pendiente de indicar si se traslada la experiencia al profesional");
                }

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        //case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break; 
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                    }
                }
                else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        //case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break; 
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                    }
                }


                //Lacalle
                if (oFila.getAttribute("plant") == "") {
                    //oFila.cells[9].appendChild(oImgOpcional.cloneNode(true), null);
                    oFila.cells[9].insertBefore(oImgOpcional.cloneNode(true), oFila.cells[9].children[0]);
                }
                else {
                    if (oFila.getAttribute("plant") == "-1") {
                        //oFila.cells[9].children[0].style.backgroundImage = "url('../../../../images/imgProyTecOff.gif')";
                        //oFila.cells[9].children[0].style.backgroundRepeat = "no-repeat";
                        //oFila.cells[9].children[0].setAttribute("title", "Perfil de la experiencia completado desde el CV");

                        //oFila.cells[9].appendChild(oImgProyTecOff.cloneNode(true), null);
                        oFila.cells[9].insertBefore(oImgProyTecOff.cloneNode(true), oFila.cells[9].children[0]);
                    }
                    else if (oFila.getAttribute("plant") != "") {//Tiene plantilla
                        //oFila.cells[9].children[0].style.backgroundImage = "url('../../../../images/imgOrdenF.gif')";
                        //oFila.cells[9].children[0].style.backgroundRepeat = "no-repeat";
                        //oFila.cells[9].children[0].setAttribute("title", "Perfil de la experiencia completado desde una plantilla ");

                        //oFila.cells[9].appendChild(oImgOrdenF.cloneNode(true), null);
                        oFila.cells[9].insertBefore(oImgOrdenF.cloneNode(true), oFila.cells[9].children[0]);
                    }
                }
                //oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:290px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:110px'>Profesional:</label>" + oFila.cells[2].innerText + "<br><label style='width:110px'>Jornadas:</label>" + oFila.getAttribute("esfuerzoenjor") + "<br><label style='width:110px'>Primera imputación:</label>" + oFila.getAttribute("PrimerConsumo") + "<br><label style='width:110px'>Última imputación:</label>" + oFila.getAttribute("UltimoConsumo") + "] hideselects=[off]\" >" + oFila.cells[2].innerText + "</nobr>";
                oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:240px;' readOnly title=\"" + oFila.cells[2].innerText + "\" >" + oFila.cells[2].innerText + "</nobr>";
                
                if ($I("hdnModo").value != "R") {
                    //Columna CV
                    if (oFila.getAttribute("cv") == "" && oFila.cells[8].innerText=="") {
                        if (oFila.getAttribute("bd") == "I") {
                            oFila.cells[8].style.backgroundImage = "url('../../../../images/imgRequerido.gif')";
                            oFila.cells[8].style.backgroundRepeat = "no-repeat";
                            var dEsfuerzoenjor = parseFloat(oFila.getAttribute("esf"));
                            if (dEsfuerzoenjor >= 10)
                                oFila.cells[8].setAttribute("title", "Asignar (Sí/No) la experiencia al profesional");
                            else
                                oFila.cells[8].setAttribute("title", "Asignar (Sí/No/Pendiente) la experiencia al profesional");
                        }
                        else {
                            if (parseFloat(oFila.getAttribute("esf")) >= 10) {
                                oFila.cells[8].style.backgroundImage = "url('../../../../images/imgRequerido.gif')";
                                oFila.cells[8].style.backgroundRepeat = "no-repeat";
                                oFila.cells[8].setAttribute("title", "Asignar (Sí/No) la experiencia al profesional");
                            }
                        }
                    }
                    //columna Plantilla 
                    //if (oFila.getAttribute("plant") == "") {
                    //    //oFila.cells[9].children[0].style.backgroundImage = "url('../../../../images/imgOpcional.gif')";
                    //    //oFila.cells[9].children[0].style.backgroundRepeat = "no-repeat";
                    //    //oFila.cells[9].children[0].setAttribute("title", "Sin perfil en la experiencia de CVT");

                    //    //oFila.cells[9].src = location.href.substring(0, nPosCUR) + "images/imgOpcional.gif";
                    //    //oFila.cells[9].title = "Sin perfil en la experiencia de CVT";

                    //    oFila.cells[9].appendChild(oImgOpcional.cloneNode(true), null);
                    //}//Lacalle
                    //else if (oFila.getAttribute("plant") == "-1") {
                    //    //oFila.cells[9].children[0].style.backgroundImage = "url('../../../../images/imgProyTecOff.gif')";
                    //    //oFila.cells[9].children[0].style.backgroundRepeat = "no-repeat";
                    //    //oFila.cells[9].children[0].setAttribute("title", "El profesional tiene asignado un perfil");
                    //    oFila.cells[9].appendChild(oImgProyTecOff.cloneNode(true), null);
                    //}
                    //else {//Tiene plantilla
                    //    //oFila.cells[9].children[0].style.backgroundImage = "url('../../../../images/imgOrdenF.gif')";
                    //    //oFila.cells[9].children[0].style.backgroundRepeat = "no-repeat";
                    //    //oFila.cells[9].children[0].setAttribute("title", "Plantilla asignada");
                    //    oFila.cells[9].appendChild(oImgOrdenF.cloneNode(true), null);
                    //}

                    //Si no tiene perfil asociado, se le asocia el evento para ir al pop-up de plantillas
                    if (oFila.getAttribute("plant") != "-1") {
                        //oFila.cells[9].style.cursor = strCurMA;

                        oFila.cells[9].ondblclick = function () { getPlantilla(); };
                    }

                    //12-15 (Lacalle): Se elimina el concepto validador de esta pantalla
                    //columna Validador
                    //if (oFila.getAttribute("valid") == "") {
                    //    oFila.cells[10].style.backgroundImage = "url('../../../../images/imgOpcional.gif')";
                    //    oFila.cells[10].style.backgroundRepeat = "no-repeat";
                    //    oFila.cells[10].removeAttribute("title");
                    //}
                    //oFila.cells[10].style.cursor = strCurMA;
                    //oFila.cells[10].ondblclick = function() { getValidador(); };
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function nuevoForm() {
    bCambios = false;
    document.forms[0].submit();
}
function getCuenta(nTipo) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/getCuenta.aspx";
        modalDialog.Show(strEnlace, self, sSize(650, 600))
            .then(function(ret) {
            if (ret != null) {
                var aDatos = ret.split("@#@");
                if (aDatos[0] != "") {
                    switch (ntipo) {
                        case 1:
                            $I("hdnCtaOri").value = aDatos[0];
                            $I("txtOrigen").value = Utilidades.unescape(aDatos[1]);
                            break;
                        case 2:
                            $I("hdnCtaDes").value = aDatos[0];
                            $I("txtDestino").value = Utilidades.unescape(aDatos[1]);
                            break;
                    }
                }
            }
        });
        ocultarProcesando(); 
        window.focus();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al seleccionar cuenta", e.message);
    }
}
function getProyecto() {
    try {
        //if ($I("hdnSelPR").value == "N") return;
        if (bCambios) {
            jqConfirm("", "Esta acción sustituirá los datos asignados por los asociados a la experiencia profesional del proyecto de referencia.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
                if (answer) getProyectoContinuar()
            });
        } else getProyectoContinuar()

    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al seleccionar proyecto de referencia-1", e.message);
    }
}
function getProyectoContinuar() {
    try {
        mostrarProcesando();
        $I("hdnSelPR").value = "S";
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Proyecto/Default.aspx?cl=" + codpar($I("hdnCliProy").value);
        modalDialog.Show(strEnlace, self, sSize(950, 560))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    //$I("hdnDesdeM").value = aDatos[0];
                    if (aDatos[0] != "") {
                        //Response.Redirect("Default.aspx?pr=" + codpar(aDatos[0]) + "&ep=" + codpar(aDatos[1]) + "&o=P");
                        $I("nIdProy").value = aDatos[0];
                        $I("txtProyRef").value = Utilidades.unescape(aDatos[2]);
                        setTimeout("nuevoForm()", 1000);
                        //window.location.reload();
                        //location.href = "Default.aspx?pr=" + codpar(aDatos[0]) + "&ep=" + codpar(aDatos[1]) + "&o=P";
                    }
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al seleccionar proyecto de referencia-2", e.message);
    }
}
function getPlantillas() {
    try {
        if ($I("hdnEP").value == "" || $I("hdnEP").value == "-1") {
            mmoff("War", "Para el acceso a plantillas debe previamente grabar la experiencia profesional", 500);
            return;
        }

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Plantilla/Catalogo/Default.aspx?ep=" + codpar($I("hdnEP").value) + "&lp=" + codpar(lstPlantillas());
        modalDialog.Show(strEnlace, self, sSize(850, 400))
            .then(function(ret) {
            if (ret != null) {
                var aDatos = ret.split("@#@");
                //$I("hdnDesdeM").value = aDatos[0];
            }
        });
        window.focus();

         ocultarProcesando();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al acceder a plantillas", e.message);
    }
}
//Obtengo la lista de plantillas nuevas asignadas que están sin grabar
function lstPlantillas() {
    var sb = new StringBuilder;
    for (var x = 0; x < aFilaT.length; x++) {
        if (aFilaT[x].getAttribute("bd") == "U") {
            if (aFilaT[x].getAttribute("plant") != aFilaT[x].getAttribute("plantNew"))
                sb.Append(aFilaT[x].getAttribute("plantNew") + "///");
        }
    }
    return sb.ToString();
}
function grabarSalir() {
    if (getOp($I("btnGrabarSalir")) != 100) return;
    bSalir = true;
    grabar();
}
function grabarAux() {
    if (getOp($I("btnGrabar")) != 100) return;
    bSalir = false;
    grabar();
}
function aceptar() {
    bSalir = false;
    var returnValue = { resultado: "OK", bDatosModificados: bDatosModificados }//"OK";
    modalDialog.Close(window, returnValue);
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                continuarSalir()
            }
        });
    } else continuarSalir();
}
function continuarSalir() {
    var returnValue = { resultado: "OK", bDatosModificados: bDatosModificados }//"OK";
    //setTimeout("window.close();", 250);
    modalDialog.Close(window, returnValue);
}

function nuevoACT() {
    try {
        mostrarProcesando();
        //var strEnlace = "../ExpProf/ACT/Default.aspx";
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/ACT/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 500))
            .then(function(ret) {
            if (ret != null) {
                var aElementos = ret.split("///");
                for (var i = 0; i < aElementos.length; i++) {
                    if (aElementos[i] == "") continue;
                    bPonerFila = true;
                    var aDatos = aElementos[i].split("@#@");
                    if (aDatos[0] != "") {
                        for (var x = 0; x < $I("tblConTec").rows.length; x++) {
                            if ($I("tblConTec").rows[x].getAttribute("id") == aDatos[0]) {
                                //alert("Area ya incluida");
                                bPonerFila = false;
                                break;
                            }
                        }
                        //ponerFila
                        if (bPonerFila) {
                            var oNF = $I("tblConTec").insertRow(-1);
                            oNF.setAttribute("bd", "I");
                            oNF.setAttribute("style", "height:16px");
                            oNF.attachEvent('onclick', mm);
                            oNF.setAttribute("id", aDatos[0]);

                            var oImgFI = document.createElement("img");
                            oImgFI.setAttribute("src", "../../../../images/imgFI.gif");
                            oNC1 = oNF.insertCell(-1);
                            oNC1.appendChild(oImgFI);

                            oNF.insertCell(-1);
                            oNF.cells[1].innerText = Utilidades.unescape(aDatos[1]);
                        }
                    }
                }
            }
        });
        window.focus();
        
        ocultarProcesando();
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir un área de conocimiento tecnológico", e.message);
    }
}
function EliminarACT() {
    try {
        //mostrarProcesando();
        //recorrer filas seleccionadas y marcar para borrado
        for (var x = 0; x < $I("tblConTec").rows.length; x++) {
            if ($I("tblConTec").rows[x].className.toUpperCase() == "FS") {
                //if ($I("tblConTec").rows[x].getAttribute("class").toUpperCase() == "FS") {
                if ($I("tblConTec").rows[x].getAttribute("bd") == "I")
                    $I("tblConTec").deleteRow(x);
                else {
                    mfa($I("tblConTec").rows[x], "D");
                    activarGrabar();
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un área de conocimiento tecnológico", e.message);
    }
}
function nuevoACS() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/ACS/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 500))
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    for (var i = 0; i < aElementos.length; i++) {
                        if (aElementos[i] == "") continue;
                        bPonerFila = true;
                        var aDatos = aElementos[i].split("@#@");
                        if (aDatos[0] != "") {
                            for (var x = 0; x < $I("tblConSec").rows.length; x++) {
                                if ($I("tblConSec").rows[x].getAttribute("id") == aDatos[0]) {
                                    //alert("Area ya incluida");
                                    bPonerFila = false;
                                    break;
                                }
                            }
                            //ponerFila
                            if (bPonerFila) {
                                var oNF = $I("tblConSec").insertRow(-1);
                                oNF.setAttribute("bd", "I");
                                oNF.setAttribute("style", "height:16px");
                                oNF.attachEvent('onclick', mm);
                                oNF.setAttribute("id", aDatos[0]);

                                var oImgFI = document.createElement("img");
                                oImgFI.setAttribute("src", "../../../../images/imgFI.gif");
                                oNC1 = oNF.insertCell(-1);
                                oNC1.appendChild(oImgFI);

                                oNF.insertCell(-1);
                                oNF.cells[1].innerText = Utilidades.unescape(aDatos[1]);
                            }
                        }
                    }
                }
            });
        window.focus();
        ocultarProcesando();
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir un área de conocimiento sectorial", e.message);
    }
}
function EliminarACS() {
    try {
        //mostrarProcesando();
        //recorrer filas seleccionadas y marcar para borrado
        for (var x = 0; x < $I("tblConSec").rows.length; x++) {
            if ($I("tblConSec").rows[x].className.toUpperCase() == "FS") {
                //if ($I("tblConSec").rows[x].getAttribute("class").toUpperCase() == "FS") {
                if ($I("tblConSec").rows[x].getAttribute("bd") == "I")
                    $I("tblConSec").deleteRow(x);
                else {
                    mfa($I("tblConSec").rows[x], "D");
                    activarGrabar();
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un área de conocimiento sectorial", e.message);
    }
}
function ii(e) {
    try {
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;

        //var oFila = oElement.parentNode.parentNode;

        var bFila = false;
        while (!bFila) {
            if (oElement.tagName.toUpperCase() == "TR") bFila = true;
            else {
                oElement = oElement.parentNode;
                if (oElement == null)
                    return;
            }
        }

        var oFila = oElement;

        if ($I("hdnModo").value == "R") return;
        if (oFila.getAttribute("ii") == "1" && oFila.rowIndex == iFila)
            return;

        var sAux;
        var sCadenaCal = "-" + oFila.getAttribute("id") + "-" + oFila.rowIndex;
        //Control del combo CV
        var dEsfuerzoenjor = parseFloat(oFila.getAttribute("esf"));
        sAux = oFila.cells[8].innerText;
        oFila.cells[8].innerText = "";
        oFila.cells[8].appendChild(oCV.cloneNode(true), null);
        oFila.cells[8].children[0].id = "cboCV" + sCadenaCal;
        //        var op1 = new Option("SIN VALOR", "");
        //        oFila.cells[5].children[0].options[0] = op1;
        var op2 = new Option("Sí", "S");
        oFila.cells[8].children[0].options[0] = op2;
        var op3 = new Option("No", "N");
        oFila.cells[8].children[0].options[1] = op3;
        if (dEsfuerzoenjor <= 10) {
            var op4 = new Option("Pdte", "P");
            oFila.cells[8].children[0].options[2] = op4;
        }
        switch (sAux) {
            //            case "SIN VALOR":  
            //                oFila.cells[5].children[0].selectedIndex = 0;  
            //                break;  
            case "Sí":
                oFila.cells[8].children[0].selectedIndex = 0;
                break;
            case "No":
                oFila.cells[8].children[0].selectedIndex = 1;
                break;
            //case "":
            case "Pdte.":
                oFila.cells[8].children[0].selectedIndex = 2;
                break;
            default:
                oFila.cells[8].children[0].selectedIndex = -1;
                break;
        }
        oFila.cells[8].children[0].attachEvent('onchange', setCombo);

        if (oFila.getAttribute("ii") == "1") return;

        //Control de fechas
        sAux = oFila.cells[6].innerText;
        oFila.cells[6].innerText = "";
        oFila.cells[6].appendChild(oFAPE.cloneNode(true), null);
        oFila.cells[6].children[0].name = "txtFI" + sCadenaCal;
        oFila.cells[6].children[0].id = "Fini" + sCadenaCal;
        oFila.cells[6].children[0].setAttribute("cRef", "Fini" + sCadenaCal);
        oFila.cells[6].children[0].value = sAux;
        oFila.cells[6].children[0].setAttribute("valAnt", sAux);

        //Eventos de los objetos calendario
        oFila.cells[6].children[0].attachEvent('onchange', contFechI);
        //alert("btnCal= " + btnCal)
        if (btnCal == "I") {
            oFila.cells[6].children[0].attachEvent('onclick', mc);
        }
        else {
            oFila.cells[6].children[0].attachEvent('onmousedwown', mc1);
            oFila.cells[6].children[0].attachEvent('onfocus', focoFecha);
        }

        {
            sAux = oFila.cells[7].innerText;
            oFila.cells[7].innerText = "";
            oFila.cells[7].appendChild(oFBPE.cloneNode(true), null);
            oFila.cells[7].children[0].name = "txtFF" + sCadenaCal;
            oFila.cells[7].children[0].id = "Ffin" + sCadenaCal;
            oFila.cells[7].children[0].setAttribute("cRef", "Ffin" + sCadenaCal);
            oFila.cells[7].children[0].value = sAux;
            oFila.cells[7].children[0].setAttribute("valAnt", sAux);
            oFila.cells[7].children[0].attachEvent('onchange', contFechF);
            if (btnCal == "I") {
                oFila.cells[7].children[0].attachEvent('onclick', mc);
            }
            else {
                oFila.cells[7].children[0].attachEvent('onmousedwown', mc1);
                oFila.cells[7].children[0].attachEvent('onfocus', focoFecha);
            }
       }

        oFila.setAttribute("ii", "1");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir los inputs en la fila", e.message);
    }
}
function controlarFecha(sTipo) {
    //A esta función se le llama desde el onclick de las cajas de texto que llevan calendario (FAPE, FBPE)
    //Comprueba si el valor actual es el mismo que el valor anterior para marcar la linea como modificada
    try {
        if (iFila == -1) return;
        switch (sTipo) {
            case "I": //FAPE
                sFechaAnt = aFilaT[iFila].cells[6].children[0].getAttribute("valAnt");
                sFechaAct = aFilaT[iFila].cells[6].children[0].value;

                if (sFechaAnt != sFechaAct) {
                    if (!fechasCongruentes(sFechaAct, getCelda(aFilaT[iFila], 7))) {
                        mmoff("War", "La fecha de alta del profesional: " + sFechaAct + "\ndebe ser anterior a la fecha de baja: " + aFilaT[iFila].cells[7].children[0].value, 350, 2000);
                        aFilaT[iFila].cells[6].children[0].value = sFechaAnt;
                        return;
                    }
                    else {
                        aFilaT[iFila].cells[6].children[0].setAttribute("valAnt", sFechaAct);
                        lineaModificada(true);
                    }
                }
                break;
            case "F": //FBPE
                //sFechaAct = aFilaT[iFila].cells[7].children[0].value;
                sFechaAct = getCelda(aFilaT[iFila], 7);
                if (aFilaT[iFila].cells[7].getElementsByTagName("INPUT").length == 0) {
                    sFechaAnt = sFechaAct;
                }
                else
                    sFechaAnt = aFilaT[iFila].cells[7].children[0].getAttribute("valAnt");
                
                
                if (sFechaAnt != sFechaAct) {
                    if (!fechasCongruentes(aFilaT[iFila].cells[6].children[0].value, sFechaAct)) {
                        mmoff("War", "La fecha de baja del profesional: " + sFechaAct + "\ndebe ser posterior a la fecha de alta: " + aFilaT[iFila].cells[6].children[0].value, 350, 2000);
                        if (aFilaT[iFila].cells[7].getElementsByTagName("INPUT").length > 0)
                            aFilaT[iFila].cells[7].children[0].value = sFechaAnt;
                        return;
                    }
                    else {
                        if (aFilaT[iFila].cells[7].getElementsByTagName("INPUT").length > 0)
                            aFilaT[iFila].cells[7].children[0].setAttribute("valAnt", sFechaAct);
                        lineaModificada();
                    }
                }
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar fechas de asignación de profesionales", e.message);
    }
}
function lineaModificada() {
    try {
        if (iFila == -1) return;
        //Compruebo si la linea es modificable
        //if (aFilaT[iFila].mod != "W") {
        //    return;
        //}
        activarGrabar();
        sEstado = aFilaT[iFila].getAttribute("bd");
        if (sEstado == "N" || sEstado == "") {
            //aFilaT[iFila].setAttribute("bd", "U");
            mfa(aFilaT[iFila], "U");
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al marcar una línea como modificada", e.message);
    }
}
function profClick(e) {
    //alert("hola");
    //mm(event);
    ii(e);
    mm(e);
    modoCombos(e);
}
function modoCombos(e) {
    try {
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;
        var bFila = false;
        while (!bFila) {
            if (oElement.tagName.toUpperCase() == "TR") bFila = true;
            else {
                oElement = oElement.parentNode;
                if (oElement == null)
                    return;
            }
        }
        var oFila = oElement;
        if ($I("hdnModo").value == "R") return;
        for (var i = 0; i < aFilaT.length; i++) {
            if ((aFilaT[i].getAttribute("idF") != oFila.getAttribute("idF")) || nfs > 1) {
                if (aFilaT[i].cells[8].children[0]) {//Para que solo quite el combo si existe
                    if (aFilaT[i].className != "FS") {
                        aFilaT[i].cells[8].removeChild(aFilaT[i].cells[8].children[0]);
                        switch (aFilaT[i].getAttribute("cv")) {
                            case "S":
                                aFilaT[i].cells[8].innerText = "Sí";
                                break;
                            case "N":
                                aFilaT[i].cells[8].innerText = "No";
                                break;
                            case "P":
                                //if (aFilaT[i].getAttribute("id") != "-1")
                                    aFilaT[i].cells[8].innerText = "Pdte.";
                                break;
                        }
                        aFilaT[i].cells[8].removeAttribute("title");
                    }
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer los combos de las filas", e.message);
    }
}
//function getPlantilla(e) {
function getPlantilla() {
    try {
        if ($I("hdnModo").value == "R") return;
        if ($I("hdnEP").value == "" || $I("hdnEP").value == "-1") {
            mmoff("War", "Para el acceso a plantillas debes previamente grabar la experiencia profesional", 500);
            return;
        }
        if (!comprobarSeleccion()) {
            mmoff("War", "Antes de seleccionar la plantilla, debes determinar a qué profesionales vas a asignársela. Recuerda que usando ctrl o mayús/shift puedes hacer una selección múltiple de profesionales", 500, 3000);
            return;
        }

        //01-16 (Lacalle): No se permite asignar plantillas si algún profesional tiene perfil
        var hayPerfiles = false;
        var sb = new StringBuilder; //sin paréntesis

//        sb.Append("grabar@#@" + $I("nIdProy").value + "@#@" + $I("hdnEP").value + "@#@" + $I("hdnPR").value + "@#@");

        for (var x = 0; x < aFilaT.length; x++) {

            if (aFilaT[x].getAttribute("class") == "FS" && aFilaT[x].getAttribute("plant") == "-1")
            {
                hayPerfiles = true;
                sb.Append(aFilaT[x].children[2].innerText + "\n");
            }

        }

        if (hayPerfiles == true) {
            mmoff("WarPer", "Los siguientes profesionales tienen asignados un perfil propio, por lo que no se les puede asignar una plantilla para su perfil. \n"
                + "Debes seleccionar únicamente profesionales sin perfil asociado o con perfil asociado a plantilla: \n\n"
                + sb.ToString(), 500);
            

            return;
        }

        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Plantilla/Catalogo/Default.aspx?ep=" + codpar($I("hdnEP").value);
        modalDialog.Show(strEnlace, self, sSize(850, 400))
            .then(function(ret) {
            if (ret != null) {
                var aDatos = ret.split("@#@");
                var oFila;
                for (var i = 0; i < aFilaT.length; i++) {
                    if ((aFilaT[i].getAttribute("class") == "FS") && (aFilaT[i].getAttribute("plant") != "-1")) {
                        oFila = aFilaT[i];
                        mfa(oFila, "U");
                        oFila.setAttribute("plantNew", aDatos[0]);
                        //oFila.cells[9].style.backgroundImage = "";
                        //oFila.cells[9].children[0].innerText = Utilidades.unescape(aDatos[1]);
                        //oFila.cells[9].removeAttribute("title");

                        //hay que poner la imagen de plantilla
                        //oFila.cells[9].children[0].style.backgroundImage = "url('../../../../images/imgOrdenF.gif')";
                        oFila.cells[9].children[0].setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOrdenF.gif");
                        oFila.cells[9].children[0].style.backgroundRepeat = "no-repeat";
                        oFila.cells[9].children[0].setAttribute("title", "Perfil de la experiencia completado desde una plantilla ");

                        oFila.cells[9].children[1].innerText = Utilidades.unescape(aDatos[1]);
                        //oFila.cells[9].removeAttribute("title");
                        //Quito validador
                        oFila.setAttribute("validNew", "");
                        //12-15 (Lacalle): se elimina el validador de esta pantalla
                        //oFila.cells[10].children[0].innerText = "";
                        //oFila.cells[10].style.backgroundImage = "url('../../../../images/imgOpcional.gif')";
                        //oFila.cells[10].style.backgroundRepeat = "no-repeat";
                    }
                    else {
                        oFila = aFilaT[i];
                        if (oFila.getAttribute("plantNew") == aDatos[0]) {
////                            oFila.cells[9].children[0].innerText = Utilidades.unescape(aDatos[1]);
                            //oFila.cells[9].children[0].style.backgroundImage = "url('../../../../images/imgOrdenF.gif')";
                            oFila.cells[9].children[0].setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOrdenF.gif");
                            oFila.cells[9].children[0].style.backgroundRepeat = "no-repeat";
                            oFila.cells[9].children[0].setAttribute("title", "Perfil de la experiencia completado desde una plantilla ");

                            oFila.cells[9].children[1].innerText = Utilidades.unescape(aDatos[1]);
                        }

                    }
                }
                activarGrabar();
            }
        });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar plantilla.", e.message);
    }
}
//function delPlantilla(e) {
//    try {
//        mostrarProcesando();
//        if (!e) e = event;
//        var oElement = e.srcElement ? e.srcElement : e.target;
//        var bFila = false;
//        while (!bFila) {
//            if (oElement.tagName.toUpperCase() == "TR") bFila = true;
//            else oElement = oElement.parentNode;
//        }
//        var oFila = oElement;

//        oFila.setAttribute("plantNew", "");
//        oFila.cells[6].children[0].innerText = "";
//        oFila.cells[6].style.backgroundImage = "url('../../../../images/imgOpcional.gif')";
//        oFila.cells[6].style.backgroundRepeat = "no-repeat";
//        
//        if (oFila.cells[6].children[1] != null) {
//            oFila.cells[6].removeChild(oFila.cells[6].children[1]);
//        }
//        //oFila.cells[6].onclick = null;
//        mfa(oFila, "U");
//        activarGrabar();

//        ocultarProcesando();
//    }
//    catch (e) {
//        mostrarErrorAplicacion("Error al desasignar la plantilla.", e.message);
//    }
//}

function getValidador() {
    try {
        if ($I("hdnModo").value == "R") return;
        if (!comprobarSeleccion()) {
            //mmoff("War", "Antes de seleccionar el responsable de validar la información que introduzca el profesional en su CV relacionado con la experiencia profesional de este proyecto, debe determinar a qué profesionales se lo va asignar. Recuerda que usando ctrl o mayús/shift puede hacer una selección múltiple de profesionales", 500, 7000);
            mmoff("War", "Debes seleccionar el profesional antes de indicar el responsable. Recuerda que usando ctrl o mayús/shift puedes hacer una selección múltiple de profesionales.", 500, 7000);
            return;
        }
        if ($I("hdnModo").value == "R") return;
        mostrarProcesando();
        //Si tenemos código de experiencia profesional, los candidatos a validadores deben ser los de cada uno de los proyectos
        //englobados por la experiencia profesional
        if ($I("hdnEP").value == "-1" || $I("hdnEP").value == "")
            var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/getValidador.aspx?pr=" + codpar($I("hdnPR").value);
        else
            var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/getValidador.aspx?ep=" + codpar($I("hdnEP").value);

        modalDialog.Show(strEnlace, self, sSize(550, 500))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    var oFila;
                    for (var i = 0; i < aFilaT.length; i++) {
                        if (aFilaT[i].className == "FS") {
                            oFila = aFilaT[i];
                            mfa(oFila, "U");
                            oFila.setAttribute("validNew", aDatos[0]);
                            //                    if (oFila.cells[7].children[0] != null)
                            //                        oFila.cells[7].removeChild(oFila.cells[7].children[0]);
                            oFila.cells[10].style.backgroundImage = "";
                            oFila.cells[10].children[0].innerText = Utilidades.unescape(aDatos[1]);
                            //Quito plantilla
                            oFila.setAttribute("plantNew", "");
                            oFila.cells[9].children[0].innerText = "";
                            oFila.cells[9].style.backgroundImage = "url('../../../../images/imgOpcional.gif')";
                            oFila.cells[9].style.backgroundRepeat = "no-repeat";
                            oFila.cells[9].setAttribute("title", "Sin perfil en la experiencia de CVT");
                        }
                    }
                    activarGrabar();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar plantilla.", e.message);
    }
}
function getCV() {
    try {
        if ($I("hdnModo").value == "R") return;
        if (!comprobarSeleccion()){
            mmoff("War", "Previamente tienes que indicar a qué profesionales se les va a asignar o no, esta experiencia. Recuerda que usando ctrl o mayús/shift puedes hacer una selección múltiple de profesionales", 500, 3000);
            return;
        }
        mostrarProcesando();
        var dEsfuerzoenjor = 0;
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/getCV.aspx";
        modalDialog.Show(strEnlace, self, sSize(250, 130))
            .then(function(ret) {
                if (ret != null) {
                    var oFila;
                    for (var i = 0; i < aFilaT.length; i++) {
                        if (aFilaT[i].getAttribute("class") == "FS") {
                            oFila = aFilaT[i];
                            //Si un profesional tiene asignadas mas de 10 jornadas económicas, no se podrá asignar su CV como pendiente
                            dEsfuerzoenjor = parseFloat(oFila.getAttribute("esf"));
                            if (ret == "P") {
                                if (dEsfuerzoenjor >= 10) {
                                    mmoff("War", "No se puede asignar el valor Pdte. al profesional\n" +
                                                    oFila.cells[2].innerText + "\nporque tiene más de 10 jornadas imputadas",
                                                    400, 4000);
                                    ocultarProcesando();
                                    return;
                                }
                            }
                            mfa(oFila, "U");
                            oFila.setAttribute("cv", ret);
                            oFila.cells[8].style.backgroundImage = "";
                            if (oFila.cells[8].children[0] != null) {
                                //oFila.cells[8].removeChild(oFila.cells[8].children[0]);
                                switch (ret) {
                                    case "S":
                                        oFila.cells[8].children[0].selectedIndex = 0;
                                        break;
                                    case "N":
                                        oFila.cells[8].children[0].selectedIndex = 1;
                                        break;
                                    case "P":
                                        oFila.cells[8].children[0].selectedIndex = 2;
                                        break;
                                }
                            }
                            else {
                                switch (ret) {
                                    case "S":
                                        oFila.cells[8].innerText = "Sí";
                                        break;
                                    case "N":
                                        oFila.cells[8].innerText = "No";
                                        break;
                                    case "P":
                                        oFila.cells[8].innerText = "Pdte.";
                                        break;
                                }
                            }
                        }
                    }
                    activarGrabar();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar CV.", e.message);
    }
}



function comprobarCV(){
    try{
        var dEsfuerzoenjor = 0;
         for (var x = 0; x < aFilaT.length; x++) {
             if (aFilaT[x].getAttribute("cv") == "") {
                 mmoff("War", "Debes indicar a qué profesionales se les debe asignar o no la experiencia a su CV", 400, 2000);
                 return false;
             }
             else {
                 if (aFilaT[x].getAttribute("cv") == "P") {
                     dEsfuerzoenjor = parseFloat(aFilaT[x].getAttribute("esf"));
                     if (dEsfuerzoenjor >= 10) {
                         mmoff("War", "No se puede asignar el valor Pdte. al profesional\n" +
                                            aFilaT[x].cells[2].innerText + "\nporque tiene más de 10 jornadas imputadas",
                                            400, 4000);
                         return false;
                     }
                 }
             }
         }
         return true;
    }
    catch (e){
        mostrarErrorAplicacion("Error al comprobar visibilidad CV.", e.message);
    }
}

function comprobarSeleccion() {
    try {
        var sw = 0;
        for (var x = 0; x < aFilaT.length; x++) {
            if (aFilaT[x].getAttribute("class") == "FS") sw=1;
        }
        if (sw == 0)
            return false;
        else
            return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar fila seleccionada.", e.message);
    }
}

function getProyExp() {
    try {
//        alert($I("hdnEP").value);
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/getProyExperiencia.aspx?ep=" + codpar($I("hdnEP").value);
        modalDialog.Show(strEnlace, self, sSize(700, 510));
        window.focus();
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar los proyectos asociados a la experiencia profesional.", e.message);
    }
}