var aFila;
var tbody;
var oCombo = document.createElement("select");
oCombo.className = "combo";
oCombo.setAttribute("style", "width:75px; vertical-align:middle; margin-left:1px;");

function init() {
    try {
        oCombo.options[0] = new Option("Sin plazo", 0);
        oCombo.setAttribute("selected", "true");
        for (var i = 1; i < 100; i++) {
            oCombo.options[oCombo.options.length] = new Option(i.ToString(), i);
        }

        aFila = FilasDe("tblDatos");
        iFila = aFila.length - 1;
        tbody = document.getElementById('tBodyAcciones');
        tbody.onmousedown = startDragIMG;        
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
        /*if (iPos > 0) {
            //mostrarError("No se puede eliminar el tipo de acción '" + aResul[3] + "',\n ya que existen elementos que lo tienen asignado.");
            mostrarError("No se pueden eliminar tipos de acción que están en uso.");
        }
        else*/ mostrarError(sError.replace(reg, "\n"));
        
    } else {
        switch (aResul[0]) {
            case "grabar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                /*aFila = FilasDe("tblDatos");
                for (var i = aFila.length - 1; i >= 0 ; i--) {
                    if (aFila[i].getAttribute("bd") == "D")
                        $I("tblDatos").deleteRow(i);
                    else
                        mfa(aFila[i], "N");
                }*/
                desActivarGrabar();
                setTimeout('scrollTablaAE()',50);
                mmoff("Suc", "Grabación correcta", 160);
                break;
        }
        tbody = document.getElementById('tBodyAcciones');
        tbody.onmousedown = startDragIMG;
        ocultarProcesando();
    }
}
function mostrarAviso(strTitulo, sTipo) {
    var reg = /\n/g;
    if (strTitulo == "SESIONCADUCADA") {
        bCambios = false;
        location.href = strServer + "SesionCaducada.aspx"
        return;
    }
    if (typeof (mmoff) == "function") {
        mmoff(sTipo, strTitulo.replace(reg, "<br>"), 400);
    }
    else alert(strTitulo);
}

function comprobarDatos() {
    try {
        var aFila = FilasDe("tblDatos");
        var aNombresAccion = new Array();
        var index = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "D") {
                if (aFila[i].cells[2].children[0].value == "") {
                    mmoff("Inf", "Debes indicar la denominación del tipo de acción", 250);
                    ms(aFila[i]);
                    aFila[i].cells[2].children[0].focus();
                    return false;
                }

                if (aNombresAccion.length > 0) {
                    if (aNombresAccion.contains(aFila[i].cells[2].children[0].value.trim().normalize().toLowerCase())) {
                        mostrarAviso("No está permitido dos o más acciones con la misma denominación.", "WarPer");
                        return false;
                    }
                }
                aNombresAccion[index] = aFila[i].cells[2].children[0].value.trim().normalize().toLowerCase();
                index++;
                //if (aFila[i].cells[2].children[0].checked) nCR++;
                //if (aFila[i].cells[3].children[0].checked) nPIG++;
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] === obj) {
            return true;
        }
    }
    return false;
}

function grabar() {
    try {
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@";

        var sw = 0;
        var aFila = FilasDe("tblDatos");
        for (var i = 0; i < aFila.length; i++) {
            //if (aFila[i].getAttribute("bd") != "") {
            sw = 1;
                var nON = 0;
                if (aFila[i].cells[3].children[0].checked) nON = 1;
                 var nPartida = 0;
                 if (aFila[i].cells[4].children[0].checked) nPartida = 1;
                var nSP = 0;
                if (aFila[i].cells[5].children[0].checked) nSP = 1;
                var nUA = 0;
                if (aFila[i].cells[6].children[0].checked) nUA = 1;
                var nActiva = 0;
                if (aFila[i].cells[7].children[0].checked) nActiva = 1;
                //var nPMR = aFila[i].cells[8].children[0].options[aFila[i].cells[8].children[0].selectedIndex].innerText;
                var nPMR = aFila[i].cells[8].children[0].selectedIndex;
                if (nPMR == 0)
                    nPMR = '';
                js_args += aFila[i].getAttribute("bd") + "##"; //Opcion BD. "I", "U", "D"
                js_args += aFila[i].id + "##"; 
                //js_args += Utilidades.escape(aFila[i].cells[1].children[0].value) + "##"; //Descripcion
                js_args += aFila[i].cells[2].children[0].value + "##"; //Descripcion
                js_args += nPartida + "##";
                js_args += nON + "##"; 
                js_args += nSP + "##";
                js_args += nUA + "##";
                js_args += nActiva + "##";
                js_args += nPMR + "///";

               
            //}
        }
        if (sw == 1) js_args = js_args.substring(0, js_args.length - 3);

        if (sw == 1) {
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        else desActivarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

var oMoveRow = document.createElement("img");
oMoveRow.setAttribute("src", "../../../../images/imgMoveRow.gif");
oMoveRow.title = "Pinchar y arrastrar para ordenar";
oMoveRow.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;cursor:row-resize");
oMoveRow.ondragstart = function () { return false; };

function nuevo() {
    try {
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);

        oNF = $I("tblDatos").insertRow(-1);
        var iFila = oNF.rowIndex;

        //oNF.id = iFila;
        oNF.id = 0;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", "1");
        oNF.setAttribute("pmr", "0");
        oNF.attachEvent('onclick', mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        var oMovRow = oMoveRow.cloneNode(true);
        oMovRow.ondragstart = function () { return false; };
        oNF.insertCell(-1).appendChild(oMovRow);

        var oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.id = "txtDen" + iFila;
        oCtrl1.className = "txtL";
        oCtrl1.setAttribute("class", "txtL");
        oCtrl1.setAttribute("style", "width:360px;margin-left:4px;");
        oCtrl1.setAttribute("maxLength", "50");
        oCtrl1.onkeyup = function () { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl1);

        var oCtrl2 = document.createElement("input");
        oCtrl2.type = "checkbox";
        oCtrl2.className = "checkTabla";
        oCtrl2.id = "chkON" + iFila;
        oCtrl2.name = "chkON" + iFila;
        oCtrl2.setAttribute("style", "width:15px;");
        oCtrl2.onclick = function () { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl2);

        var oCtrl3 = document.createElement("input");
        oCtrl3.type = "checkbox";
        //oCtrl3.checked = true;
        oCtrl3.className = "checkTabla";
        oCtrl3.id = "chkPartida" + iFila;
        oCtrl3.name = "chkPartida" + iFila;
        oCtrl3.setAttribute("style", "width:15px;");
        oCtrl3.onclick = function () { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl3);

        var oCtrl4 = document.createElement("input");
        oCtrl4.type = "checkbox";
        //oCtrl4.checked = true;
        oCtrl4.className = "checkTabla";
        oCtrl4.id = "chkSP" + iFila;
        oCtrl4.name = "chkSP" + iFila;
        oCtrl4.setAttribute("style", "width:15px");
        oCtrl4.onclick = function () { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl4);

        var oCtrl5 = document.createElement("input");
        oCtrl5.type = "checkbox";
        //oCtrl4.checked = true;
        oCtrl5.className = "checkTabla";
        oCtrl5.id = "chkUA" + iFila;
        oCtrl5.name = "chkUA" + iFila;
        oCtrl5.setAttribute("style", "width:15px;margin-left:29px;");
        oCtrl5.onclick = function () { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl5);

        var oCtrl6 = document.createElement("input");
        oCtrl6.type = "checkbox";
        oCtrl6.checked = true;
        oCtrl6.className = "checkTabla";
        oCtrl6.id = "chkActiva" + iFila;
        oCtrl6.name = "chkActiva" + iFila;
        oCtrl6.setAttribute("style", "width:15px;margin-left:9px;");
        oCtrl6.onclick = function () { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl6);

        //Plazo mínimo requerido
        var oComboLimFac = oCombo.cloneNode(true);
        oComboLimFac.onchange = function () { fm_mn(this); };
        oNF.insertCell(-1).appendChild(oComboLimFac);


        ms(oNF);
        oNF.cells[2].children[0].focus();
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

                var oComboLimFac = oCombo.cloneNode(true);
                oComboLimFac.onchange = function () { fm_mn(this); };
                oFila.cells[8].appendChild(oComboLimFac);
                if (tblDatos.rows[i].getAttribute("pmr") == "")
                    oFila.cells[8].children[0].selectedIndex = 0;
                else
                    oFila.cells[8].children[0].selectedIndex = tblDatos.rows[i].getAttribute("pmr");

            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
