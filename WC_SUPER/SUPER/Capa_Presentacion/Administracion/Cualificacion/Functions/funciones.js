var aFila;
var oClase = document.createElement("span");
oClase.className = "NBR W390";

var oGomaPerfil = document.createElement("img");
oGomaPerfil.setAttribute("src", "../../../../../images/botones/imgBorrar.gif");
oGomaPerfil.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

function init() {
    try {
        $I("lblNodo").innerHTML = strNodoCorta;
        aFila = FilasDe("tblDatos");
        iFila = aFila.length - 1;
        scrollTablaAE();
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        var sError = aResul[2];
        var iPos = sError.indexOf("integridad referencial");
        if (iPos > 0) {
            mostrarError("No se puede eliminar el cualificador '" + aResul[3] + "',\n ya que existen elementos que lo tienen asignado.");
        }
        else mostrarError(sError.replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                //$I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                for (var i = aFila.length - 1; i >= 0 ; i--) {
                    if (aFila[i].getAttribute("bd") == "D")
                        $I("tblDatos").deleteRow(i);
                    else
                        mfa(aFila[i], "N");
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;
        }
        ocultarProcesando();
    }
}
function comprobarDatos() {
    try {
        var aFila = FilasDe("tblDatos");
        var nCR=0, nPIG=0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "D") {
                if (aFila[i].cells[1].children[0].value == "") {
                    mmoff("Inf", "Debes indicar la denominación del cualificador", 250);
                    ms(aFila[i]);
                    aFila[i].cells[1].children[0].focus();
                    return false;
                }
                if (aFila[i].cells[2].children[0].checked) nCR++;
                if (aFila[i].cells[3].children[0].checked) nPIG++;
            }
        }
        if (nCR != 1) {
            mmoff("WarPer", "No puede haber más de un cualificador como defecto para C.R.", 300);
            return false;
        }
        else {
            if (nPIG != 1) {
                mmoff("WarPer", "No puede haber más de un cualificador como defecto para PIG", 300);
                return false;
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function grabar() {
    try {
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@";

        var sw = 0;
        var aFila = FilasDe("tblDatos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                sw = 1;
                var nCR = 0;
                if (aFila[i].cells[2].children[0].checked) nCR = 1;
                var nPIG = 0;
                if (aFila[i].cells[3].children[0].checked) nPIG = 1;
                js_args += aFila[i].getAttribute("bd") + "##"; //Opcion BD. "I", "U", "D"
                js_args += aFila[i].id + "##"; //ID Origen
                js_args += Utilidades.escape(aFila[i].cells[1].children[0].value) + "##"; //Descripcion
                js_args += nCR + "##"; //Defecto CR
                js_args += nPIG + "##"; //Defecto PIG
                js_args += aFila[i].getAttribute("clase") + "///"; //E-Mail
            }
        }
        if (sw == 1) js_args = js_args.substring(0, js_args.length - 3);

        if (sw == 1) {
            //alert(js_args);
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        else desActivarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

function nuevo() {
    try {
            if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);

            oNF = $I("tblDatos").insertRow(-1);
            var iFila = oNF.rowIndex;

            oNF.id = iFila;
            oNF.setAttribute("bd", "I");
            oNF.attachEvent('onclick', mm);

            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

            var oCtrl1 = document.createElement("input");
            oCtrl1.type = "text";
            oCtrl1.id = "txtDen" + iFila;
            oCtrl1.className = "txtL";
            oCtrl1.setAttribute("class", "txtL");
            oCtrl1.setAttribute("style", "width:400px");
            oCtrl1.setAttribute("maxLength", "50");
            oCtrl1.onkeyup = function () { fm_mn(this) };
            oNF.insertCell(-1).appendChild(oCtrl1);

            var oCtrl2 = document.createElement("input");
            oCtrl2.type = "checkbox";
            oCtrl2.className = "checkTabla";
            oCtrl2.id = "chkCR" + iFila;
            oCtrl2.name = "chkCR" + iFila;
            oCtrl2.setAttribute("style", "width:15px");
            oCtrl2.onclick = function () { fm_mn(this) };
            oNF.insertCell(-1).appendChild(oCtrl2);

            var oCtrl3 = document.createElement("input");
            oCtrl3.type = "checkbox";
            oCtrl3.checked = true;
            oCtrl3.className = "checkTabla";
            oCtrl3.id = "chkPIG" + iFila;
            oCtrl3.name = "chkPIG" + iFila;
            oCtrl3.setAttribute("style", "width:15px");
            oCtrl3.onclick = function () { fm_mn(this) };
            oNF.insertCell(-1).appendChild(oCtrl3);

            var oCtrl4 = document.createElement("span");
            oCtrl4.className = "NBR W370";
            oNF.insertCell(-1).appendChild(oCtrl4);

            oNF.cells[4].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
            oNF.cells[4].style.backgroundRepeat = "no-repeat";
            oNF.cells[4].style.cursor = strCurMA;
            oNF.cells[4].ondblclick = function () { getClase(this.parentNode); };

            ms(oNF);
            oNF.cells[1].children[0].focus();
            activarGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al crear un nuevo registro", e.message);
    }
}

function eliminar() {
    try {
        var sw = 0;
        var sw2 = 0;
        aFila = FilasDe("tblDatos");
        for (var i = aFila.length - 1; i >= 0; i--) {
            if (aFila[i].className == "FS") {
                sw2 = 1;
                if (aFila[i].getAttribute("bd") == "I") {
                    //Si es una fila nueva, se elimina
                    $I("tblDatos").deleteRow(i);
                    iFila = -1;
                }
                else {
                    mfa(aFila[i], "D");
                    sw = 1;
                }
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("Inf", "Debes seleccionar la fila a eliminar", 250);
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}


var nTopScrollAE = -1;
var nIDTimeAE = 0;
function scrollTablaAE() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollAE) {
            nTopScrollAE = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeAE);
            nIDTimeAE = setTimeout("scrollTablaAE()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollAE / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        var sAux;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                oFila.attachEvent('onclick', mm);
                
                if (oFila.getAttribute("bd") != "I") oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgFI.cloneNode(true), null);
                
                if (oFila.getAttribute("clase") == "") {
                    oFila.cells[4].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
                    oFila.cells[4].style.backgroundRepeat = "no-repeat";
                }
                else {
                    var oGoma = oFila.cells[4].appendChild(oGomaPerfil.cloneNode(true), null);
                    oGoma.onclick = function () { borrarClase(this.parentNode.parentNode); };
                    oGoma.style.cursor = "pointer";
                }
                
                oFila.cells[4].style.cursor = strCurMA;
                oFila.cells[4].ondblclick = function () { getClase(this.parentNode); };
                
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}

function getClase(oFila) {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getClaseEco.aspx?tipo=1", self, sSize(505, 440))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("##");
                    oFila.setAttribute("clase", aDatos[0]);
                    oFila.cells[4].style.backgroundImage = "";
                    oFila.cells[4].children[0].innerText = aDatos[2];
                    if (oFila.cells[4].children[1] != null) oFila.cells[4].removeChild(oFila.cells[4].children[1]);

                    if (oFila.getAttribute("clase") == "") {
                        oFila.cells[4].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
                        oFila.cells[4].style.backgroundRepeat = "no-repeat";
                    } else {
                        var oGoma = oFila.cells[4].appendChild(oGomaPerfil.cloneNode(true), null);
                        oGoma.onclick = function () { borrarClase(this.parentNode.parentNode); };
                        oGoma.style.cursor = "pointer";
                    }

                    mfa(oFila, "U");
                    activarGrabar();
                }
            });
        window.focus();

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las clases", e.message);
    }
}
function borrarClase(oFila) {
    try {
        oFila.setAttribute("clase", "");
        oFila.cells[4].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
        oFila.cells[4].style.backgroundRepeat = "no-repeat";
        oFila.cells[4].children[0].innerText = "";
        if (oFila.cells[4].children[1] != null) oFila.cells[4].removeChild(oFila.cells[4].children[1]);
        mfa(oFila, "U");
        activarGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar la clase", e.message);
    }
}
