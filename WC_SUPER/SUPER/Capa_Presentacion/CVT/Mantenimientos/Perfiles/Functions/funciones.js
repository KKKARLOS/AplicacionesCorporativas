var fSA = null;
var numPerfil = { img: 0, denom: 1, rh: 2, abrev: 3, nivel: 4 };
var oImg;
var oPerfil;
var oCheck;
var oAbrev;


function init() {
    try {
        AccionBotonera("grabar", "H");
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

        //Celda 1 Perfil
        oPerfil = document.createElement("input");
        oPerfil.setAttribute("type", "text");
        oPerfil.setAttribute("style", "width:380px; text-transform:uppercase;");
        oPerfil.setAttribute("class", "txtL");
        oPerfil.setAttribute("value", "");

        //Celda 2 RH
        oCheck = document.createElement("input");
        oCheck.setAttribute("type", "checkbox");

        //Celda 3 Abrev
        oAbrev = document.createElement("input");
        oAbrev.setAttribute("type", "text");
        oAbrev.setAttribute("style", "width:90px; text-transform:uppercase;");
        oAbrev.setAttribute("class", "txtL");
        oAbrev.setAttribute("value", "");

    } catch (e) {
        mostrarErrorAplicacion("Error en objetosTablas", e.message);
    }
}

function activarCombo(id) {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila[id].cells[numPerfil.nivel].children[0] == undefined) {
            var comboA;
            var aux = Utilidades.unescape(strCombo);
            var comboA = aux.replace(">" + aFila[id].cells[numPerfil.nivel].innerText, " selected='selected'>" + aFila[id].cells[numPerfil.nivel].innerText);
            comboA = comboA.replace(/cboNivel/g, "cboNivel" + id);

            aFila[id].cells[numPerfil.nivel].innerHTML = comboA;
        }

        if (fSA != null && id != fSA) {
            if (aFila[fSA] != null) {
                aFila[fSA].setAttribute("nivel", $I("cboNivel" + (fSA)).options[$I("cboNivel" + (fSA)).selectedIndex].value);
                aFila[fSA].cells[numPerfil.nivel].innerText = $I("cboNivel" + (fSA)).options[$I("cboNivel" + (fSA)).selectedIndex].text;
            }
        }
        fSA = id;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar el combo", e.message);
    }
}


function nuevo() {
    try {
        var iFila;
        var oNF = $I("tblCatalogo").insertRow(-1);
        iFila = oNF.rowIndex;
        oNF.id = -$I("tblCatalogo").rows.length;
        oNF.nF = iFila;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("height", "22px");
        oNF.setAttribute("sw", "1");

        oNF.attachEvent("onclick", mm);
        oNF.onclick = function() { activarCombo(this.rowIndex); };

        //Celda 0 Imagen
        var oImgFI = oNF.insertCell(-1).appendChild(document.createElement("img"));
        oImgFI.setAttribute("src", "../../../../Images/imgFI.gif");

        //Celda 1 Perfil
        oNF.insertCell(-1);

        var oImput1 = document.createElement("input");
        oImput1.setAttribute("type", "text");
        oImput1.setAttribute("id", "Denominacion" + iFila);
        oImput1.setAttribute("style", "width:380px; text-transform:uppercase;");
        oImput1.setAttribute("class", "txtM");
        oImput1.setAttribute("value", "");
        oImput1.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[numPerfil.denom].appendChild(oImput1);

        //Celda 2 rh
        oNF.insertCell(-1);
        oNF.cells[numPerfil.rh].style.textAlign = "center";
        var oChk = document.createElement("input");
        oChk.setAttribute("type", "checkbox");
        oChk.setAttribute("id", "chkEstado" + iFila);
        oChk.setAttribute("checked", "checked");
        oNF.cells[numPerfil.rh].appendChild(oChk);
        oNF.insertCell(-1);

        //Celda 3 Abrev
        oNF.insertCell(-1);
        var oImput2 = document.createElement("input");
        oImput2.setAttribute("type", "text");
        oImput2.setAttribute("id", "Denominacion" + iFila);
        oImput2.setAttribute("style", "width:90px; text-transform:uppercase;");
        oImput2.setAttribute("class", "txtM");
        oImput2.setAttribute("value", "");
        oImput2.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[numPerfil.abrev].appendChild(oImput2);

        //Celda 4 Nivel
        oNF.insertCell(-1);

        oChk.onclick = function() { mfa(this.parentNode.parentNode, 'U') };
        ms(oNF);
        activarCombo(oNF.rowIndex);
        oNF.cells[numPerfil.denom].children[0].focus();

    } catch (e) {
        mostrarErrorAplicacion("Error al crear una nuevo perfil", e.message);
    }
}

function comprobarDatos() {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "D")) {
                    if (aFila[i].cells[numPerfil.denom].children[0] != undefined) {
                        if (aFila[i].cells[numPerfil.denom].children[0].value.Trim() == "") {
                            mmoff("War", "Debes escribir una descripción para el perfil.", 350);
                            ocultarProcesando();
                            ms(aFila[i]);
                            aFila[i].cells[numPerfil.denom].children[0].focus();
                            return false;
                        } 
                    }
                    if (aFila[i].cells[numPerfil.nivel].children[0] != undefined) {
                        if (aFila[i].cells[numPerfil.nivel].innerHTML.indexOf("<OPTION selected value=\"\">") != -1) {
                            mmoff("War", "Debes seleccionar un nivel.", 350);
                            ocultarProcesando();
                            ms(aFila[i]);
                            aFila[i].cells[numPerfil.nivel].children[0].focus();
                            return false;
                        }
                    }
                    if (aFila[i].cells[numPerfil.nivel].innerText == "") {
                        mmoff("War", "Debes seleccionar un perfil.", 350);
                        ocultarProcesando();
                        ms(aFila[i]);
                        if (aFila[i].cells[numPerfil.nivel].children[0] != undefined)
                            aFila[i].cells[numPerfil.nivel].children[0].focus();
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
        if (!comprobarDatos()) return false
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "")) {
                    var nivel= "";
                    if (aFila[i].cells[numPerfil.nivel].children[0] != undefined) {
                        nivel = aFila[i].cells[numPerfil.nivel].children[0].options[aFila[i].cells[numPerfil.nivel].children[0].selectedIndex].value;
                    }
                    else {
                        nivel = aFila[i].getAttribute("nivel");
                    }

                    sb.Append(aFila[i].getAttribute("bd") + "@dato@" + aFila[i].id + "@dato@" + aFila[i].cells[numPerfil.denom].children[0].value + "@dato@" + aFila[i].cells[numPerfil.abrev].children[0].value + "@dato@" + aFila[i].cells[numPerfil.rh].children[0].checked + "@dato@" + nivel + "@perfil@");
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
function eliminar() {
    try {
        if ($I("tblCatalogo") == null) return;
        if ($I("tblCatalogo").rows.length == 0) return;

        var sw = 0;
        var sw2 = 0;
        var aux = 0;
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            intFila = -1;
            var intID = "";
            for (i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("class") == "FS") {
                    sw2 = 1;
                    if (aFila[i].getAttribute("bd") == "I") {
                        $I("tblCatalogo").deleteRow(i);
                    }
                    else {
                        if (aFila[i].getAttribute("bd")!="-1")
                            mfa(aFila[i], "D");
                        else
                            mmoff("War", "La fila " + aFila[i].cells[numPerfil.denom].innerText + " no se puede eliminar por ser el perfil por defecto", 450);
                    }
                }
            }
        }
        if (sw2 == 0) mmoff("War", "Debes seleccionar la fila a eliminar", 250);
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Eliminar", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        if (aResul[2].indexOf("##EC##") > -1) {
            ocultarProcesando();
            var aDatos = aResul[2].split("##EC##");
            mmoff("Err", Utilidades.unescape(aDatos[1]), 700, 4000);
        }
        else if (aResul[3] == "2627") {
            ocultarProcesando();
            var aFila = FilasDe("tblCatalogo");
            mmoff("Err", "No es posible grabar. Alguno de los elementos que desea grabar ya existe", 550, 2300);
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("bd") == "D") {
                    mfa(aFila[i], "N");
                }
            }
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
                        if (aFila[i].getAttribute("bd") == "D") {
                            $I("tblCatalogo").deleteRow(i);
                            continue;
                        }
                        else if (aFila[i].getAttribute("bd") == "I") {
                            aFila[i].id = aEI[nIndiceEI];
                            nIndiceEI++;
                        }
                        if (aFila[i].getAttribute("bd") != "D") {
                            if (aFila[i].cells[numPerfil.nivel].children[0] != undefined) {
                                aFila[i].cells[numPerfil.nivel].innerText = $I("cboNivel" + (i)).options[$I("cboNivel" + (i)).selectedIndex].text;
                            }
                        }
                        mfa(aFila[i], "N");
                    }
                }
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 170, 2300);
                
                //bCambios = false;
                fSA = null;
                AccionBotonera("grabar", "D");
                desActivarGrabar();
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        
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
                oFila.cells[numPerfil.img].innerText = "";
                oFila.cells[numPerfil.img].appendChild(oImg.cloneNode(true), null);

                //Perfil
                var sPerfil= oFila.cells[numPerfil.denom].innerText;
                oFila.cells[numPerfil.denom].innerText = "";
                oFila.cells[numPerfil.denom].appendChild(oPerfil.cloneNode(true), null);
                oFila.cells[numPerfil.denom].children[0].setAttribute("value", sPerfil);
                oFila.cells[numPerfil.denom].onkeyup = function() { mfa(this.parentNode, 'U'); };


                //Check rh
                var sCheck = oFila.getAttribute("chk");
                oFila.cells[numPerfil.rh].innerText = "";
                oFila.cells[numPerfil.rh].appendChild(oCheck.cloneNode(true), null);
                if (sCheck == "True") {
                    oFila.cells[numPerfil.rh].children[0].setAttribute("checked", "checked");
                }
                oFila.cells[numPerfil.rh].children[0].setAttribute("id", "chkRH" + oFila.rowIndex.toString());
                oFila.cells[numPerfil.rh].onclick = function() { mfa(this.parentNode, 'U'); };

                //Abreviatura
                var sAbrev = oFila.cells[numPerfil.abrev].innerText;
                oFila.cells[numPerfil.abrev].innerText = "";
                oFila.cells[numPerfil.abrev].appendChild(oAbrev.cloneNode(true), null);
                oFila.cells[numPerfil.abrev].children[0].setAttribute("value", sAbrev);
                oFila.cells[numPerfil.abrev].onkeyup = function() { mfa(this.parentNode, 'U'); };

                //Nivel

                
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de Perfiles.", e.message);
    }
}