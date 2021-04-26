var fSA = null;
var numProfesional = { img: 0, prof: 1, exclusion: 2};
var oImg;
var oProf;

function init() {
    try {        
        objetosTablas();
        if ($I("tblCatalogo") != null) {
            scrollTabla();
        }
        actualizarLupas("tblTitulo", "tblCatalogo");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function objetosTablas() {
    try {
        //Pendiente de ver si es la mejor opcion
        oImg = document.createElement("img");
        oImg.setAttribute("src", "../../../../Images/imgFN.gif");

        //Celda 1 Profesional
        oProf = document.createElement("input");
        oProf.setAttribute("type", "text");
        oProf.setAttribute("style", "width:280px;");
        oProf.setAttribute("class", "txtL");
        oProf.setAttribute("value", "");
        oProf.setAttribute("readonly", "readonly");

    } catch (e) {
        mostrarErrorAplicacion("Error en objetosTablas", e.message);
    }
}

function activarCombo(id) {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila[id].cells[numProfesional.exclusion].children[0] == undefined) {
            var comboA;
            var aux = Utilidades.unescape(strCombo);
            var comboA = aux.replace(">" + aFila[id].cells[numProfesional.exclusion].innerText, " selected='selected'>" + aFila[id].cells[numProfesional.exclusion].innerText);
            comboA = comboA.replace(/cboExclusion/g, "cboExclusion" + id);

            aFila[id].cells[numProfesional.exclusion].innerHTML = comboA;
        }

        if (fSA != null && id != fSA) {
            if (aFila[fSA] != null) {
                aFila[fSA].setAttribute("exclusion", $I("cboExclusion" + (fSA)).options[$I("cboExclusion" + (fSA)).selectedIndex].value);
                aFila[fSA].cells[numProfesional.exclusion].innerText = $I("cboExclusion" + (fSA)).options[$I("cboExclusion" + (fSA)).selectedIndex].text;
            }
        }
        AccionBotonera("grabar", "H");
        fSA = id;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar el combo", e.message);
    }
}

function comprobarDatos() {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "D")) {

                    if (aFila[i].cells[numProfesional.exclusion].children[0] != undefined) {
                        if (aFila[i].cells[numProfesional.exclusion].innerHTML.indexOf("<OPTION selected value=\"\">") != -1) {
                            mmoff("War", "Debes seleccionar un exclusion.", 350);
                            ocultarProcesando();
                            ms(aFila[i]);
                            aFila[i].cells[numProfesional.segmento].children[0].focus();
                            return false;
                        }
                    }
                    if (aFila[i].cells[numProfesional.exclusion].innerText == "") {
                        mmoff("War", "Debes seleccionar un nivel de exclusion.", 350);
                        ocultarProcesando();
                        ms(aFila[i]);
                        if (aFila[i].cells[numProfesional.exclusion].children[0] != undefined)
                            aFila[i].cells[numProfesional.exclusion].children[0].focus();
                        return false;
                    }
                }
            }
        } else {
            mmoff("War", "No existen datos.", 350);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
    }
}
function grabar() {
    try {
        mostrarProcesando();
        //if (!comprobarDatos()) return false
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("bd") == "U") {
                    var idExclusion = "";
                    if (aFila[i].cells[numProfesional.exclusion].children[0] != undefined) {
                        idExclusion = aFila[i].cells[numProfesional.exclusion].children[0].options[aFila[i].cells[numProfesional.exclusion].children[0].selectedIndex].value;
                    }
                    else {
                        idExclusion = aFila[i].getAttribute("exclusion");
                    }

                    sb.Append(aFila[i].getAttribute("bd") + "@dato@" + aFila[i].getAttribute("idficepi") + "@dato@"  + idExclusion + "@profesional@");
                }
            }
            RealizarCallBack(sb.ToString(), "");
        }
        else {
            return false;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        if (aResul[2].indexOf("##EC##") > -1) {
            ocultarProcesando();
            var aDatos = aResul[2].split("##EC##");
            mmoff("Err", aDatos[1], 400);
        }
        else
            mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                //aEI.reverse();
                var nIndiceEI = 0;
                var aFila = FilasDe("tblCatalogo");
                if (aFila != null) {
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") != "D") {
                            if (aFila[i].cells[numProfesional.exclusion].children[0] != undefined) {
                                aFila[i].cells[numProfesional.exclusion].innerText = $I("cboExclusion" + (i)).options[$I("cboExclusion" + (i)).selectedIndex].text;
                            }
                        }
                        if (aFila[i].getAttribute("bd") != "N")
                            mfa(aFila[i], "N");
                    }
                }
                mmoff("Suc", "Grabación correcta", 170, 2300);
                bCambios = false;
                fSA = null;
                bOcultarProcesando = true;
                setTimeout("buscar();", 2300);
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                fSA = null;
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScroll / 22);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 22 + 1, $I("tblCatalogo").rows.length);
        var oFila;

        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblCatalogo").rows[i].getAttribute("sw")) {
                oFila = $I("tblCatalogo").rows[i];
                oFila.setAttribute("sw", 1);
                oFila.attachEvent("onclick", mm);
                oFila.onclick = function() { activarCombo(this.rowIndex); };

                //Imagen
                oFila.cells[numProfesional.img].innerText = "";
                oFila.cells[numProfesional.img].appendChild(oImg.cloneNode(true), null);

                //Profesional
                var sCuenta = oFila.cells[numProfesional.prof].innerText;
                oFila.cells[numProfesional.prof].innerText = "";
                oFila.cells[numProfesional.prof].appendChild(oProf.cloneNode(true), null);
                oFila.cells[numProfesional.prof].children[0].setAttribute("value", sCuenta);
                oFila.cells[numProfesional.prof].onkeyup = function() { mfa(this.parentNode, 'U'); };
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de Profesionales.", e.message);
    }
}
function buscar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder; //sin paréntesis por ser versión JavaScript
        sb.Append("buscar@#@");
        sb.Append($I("txtNombre").value + "@#@" + $I("txtApellido1").value + "@#@" + $I("txtApellido2").value + "@#@" + $I("cbonivelExclusion").value);
        RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}