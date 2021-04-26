var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
oChk.setAttribute("style", "cursor:pointer; vertical-align:middle;");

var oImgSeg = document.createElement("img");
oImgSeg.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgSeguimiento.png");
oImgSeg.setAttribute("style", "cursor:pointer;vertical-align:middle; margin-left:2px; border: 0px; visibility:hidden; width:16px; height:16px;");


function init() {
    try {
        $I("txtApellido1").focus();
        setTimeout("getProfPendientes();", 20);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

function abrirCurriculum() {
    try {
        if ($I("hdnProfesional").value!="")
            location.href = "../MiCV/Default.aspx?spr=" + codpar($I("hdnProfesional").value)+  "&origen=2";
        else
            mmoff("War", "Debes seleccionar el profesional", 300); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el currículum", e.message);
    }
}
function ponerId(id) {
    $I("hdnProfesional").value = id;
    abrirCurriculum();
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "buscar":
                $I("divCatalogoSC").children[0].innerHTML = aResul[2];
                scrollTablaProfSC();
                break;
            case "profesionales":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                break;
            case "grabar":
                //desActivarGrabar();
                for (var i = 0; i < tblDatosSC.length; i++) {
                    if (tblDatosSC[i].getAttribute("bd") == "U") {
                        mfa(tblDatosSC[i], "N");
                    }
                }
                /*mmoff("Suc", "Grabación correcta", 160);

                if (bBuscar) {
                    bBuscar = false;
                    setTimeout("buscar();", 20);
                }*/
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function mostrarProfesionales() {
    try {
        if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == "") {
            mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "profesionales@#@";
        js_args += Utilidades.escape($I("txtApellido1").value) + "@#@";
        js_args += Utilidades.escape($I("txtApellido2").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombre").value) + "@#@";
        js_args += (($I("chkSoloActivos").checked) ? "1" : "0");

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
    }
}

function getProfPendientes() {
    try {
        mostrarProcesando();

        var js_args = "buscar@#@";
        js_args += ((($I("chkNoCV").checked) ? "1" : "0")) + "@#@";
        js_args += (($I("chkNoExternos").checked) ? "1" : "0");
        
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
    }
}



var nTopScrollFICEPI = -1;
var nIDTimeFICEPI = 0;
function scrollTablaProf() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollFICEPI) {
            nTopScrollFICEPI = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeFICEPI);
            nIDTimeFICEPI = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollFICEPI / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            oFila = $I("tblDatos").rows[i];
            if (!oFila.getAttribute("sw")) {
                oFila.setAttribute("sw", 1);
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1") {
                    setOp(oFila.cells[0].children[0], 20);
                }

            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
    }
}

var nTopScrollFICEPISC = -1;
var nIDTimeFICEPISC = 0;
function scrollTablaProfSC() {
    try {
        if ($I("divCatalogoSC").scrollTop != nTopScrollFICEPISC) {
            nTopScrollFICEPISC = $I("divCatalogoSC").scrollTop;
            clearTimeout(nIDTimeFICEPISC);
            nIDTimeFICEPISC = setTimeout("scrollTablaProfSC()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollFICEPISC / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoSC").offsetHeight / 20 + 1, $I("tblDatosSC").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            oFila = $I("tblDatosSC").rows[i];
            if (!oFila.getAttribute("sw")) {
                oFila.setAttribute("sw", 1);
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1") {
                    setOp(oFila.cells[0].children[0], 20);
                }

                var oChkSeg = oChk.cloneNode(true);
                if (oFila.getAttribute("comentario") != "")
                    oChkSeg.setAttribute("checked", "true");
                oChkSeg.onclick = function(e) { setSeguimiento(e); };
                oFila.cells[4].appendChild(oChkSeg);

                var oFlag = oImgSeg.cloneNode(true);
                oFlag.onclick = function() { ModificarSeguimiento(this) };
                oFlag.onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("comentario")); }
                oFlag.onmouseout = function() { hideTTE(); }
                if (oFila.getAttribute("comentario") != "") {
                    oFlag.style.visibility = "visible";
                }
                oFila.cells[4].appendChild(oFlag);

            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales sin completar.", e.message);
    }
}

var oFilaSeguimiento;
var sAccionSeguimiento = "";
var sOrigenModificacion = "";
function setSeguimiento(e) {
    try {
        sOrigenModificacion = "CHK";
        var oFila = null;
        if (!e) e = event;
        var oControl = (typeof e.srcElement != 'undefined') ? e.srcElement : e.target;
        var oCheck = oControl;
        while (oControl != document.body) {
            if (oControl.tagName != undefined) {
                if (oControl.tagName.toUpperCase() == "TR") {
                    oFila = oControl;
                    break;
                }
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila.", 300);
            return;
        }
        //alert(oFila.getAttribute("idr") + ": " + ((oCheck.checked) ? "activar" : "desactivar"));
        oFilaSeguimiento = oFila;
        if (oCheck.checked)
            ActivarSeguimiento();
        else
            DesactivarSeguimiento();
    } catch (e) {
        mostrarErrorAplicacion("Error en la función setSeguimiento.", e.message);
    }
}

function ActivarSeguimiento() {
    try {
        sAccionSeguimiento = "Activar";
        $I("lblTextoSeguimiento").innerText = "Para establecer que no se va a dar de alta un CV, es preciso indicar el motivo del mismo.";
        $I("txtSeguimiento").ReadOnly = false;
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("comentario"));

        $I("lblBoton").innerText = "Activar";
        $I("imgBotonActivar").src = "../../../images/imgSegAdd.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelarSeg").className = "btnH25W100";
        $I("divTotal").style.display = "block";
        $I("btnActivarDesactivar").onclick = function() { Activar(); };
        $I("txtSeguimiento").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el seguimiento.", e.message);
    }
}
function DesactivarSeguimiento() {
    try {
        sAccionSeguimiento = "Desactivar";
        $I("lblTextoSeguimiento").innerText = "Para confirmar la DESACTIVACIÓN y poder dar de alta un CV, pulsa el botón \"Desactivar\". En caso contrario pulse \"Cancelar\".";
        $I("txtSeguimiento").ReadOnly = true;
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("comentario"));

        $I("lblBoton").innerText = "Desactivar";
        $I("imgBotonActivar").src = "../../../images/imgSegDel.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelarSeg").className = "btnH25W100";
        $I("divTotal").style.display = "block";
        $I("btnActivarDesactivar").onclick = function() { Desactivar(); };
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el seguimiento.", e.message);
    }
}
function CancelarSeguimiento() {
    try {
        if (sOrigenModificacion == "CHK")
            oFilaSeguimiento.cells[4].children[0].checked = !oFilaSeguimiento.cells[4].children[0].checked;
        sOrigenModificacion = "";
        $I("divTotal").style.display = "none";
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar el seguimiento.", e.message);
    }
}
function ModificarSeguimiento(oImagen) {
    try {
        sAccionSeguimiento = "Modificar";
        var oFila = null;
        sOrigenModificacion = "IMG";
        var oControl = oImagen;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila.", 300);
            return;
        }

        oFilaSeguimiento = oFila;
        $I("lblTextoSeguimiento").innerText = "";
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("comentario"));
        $I("txtSeguimiento").ReadOnly = false;

        $I("lblBoton").innerText = "Modificar";
        $I("imgBotonActivar").src = "../../../images/imgSegAdd.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelarSeg").className = "btnH25W100";
        $I("divTotal").style.display = "block";
        $I("btnActivarDesactivar").onclick = function() { Activar(); };
        $I("txtSeguimiento").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el seguimiento.", e.message);
    }
}

function Activar() {
    try {
        if ($I("txtSeguimiento").value == "") {
            mmoff("War", "El motivo es dato obligatorio.", 230);
            return;
        }

        oFilaSeguimiento.setAttribute("comentario", Utilidades.escape($I("txtSeguimiento").value));
        oFilaSeguimiento.cells[4].children[1].style.visibility = "visible";
        oFilaSeguimiento.cells[4].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("comentario")); }
        oFilaSeguimiento.cells[4].children[1].onmouseout = function() { hideTTE(); }
        $I("divTotal").style.display = "none";
        mfa(oFilaSeguimiento, "U");
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el seguimiento.", e.message);
    }
}

function Desactivar() {
    try {
        oFilaSeguimiento.setAttribute("comentario", "");
        oFilaSeguimiento.cells[4].children[1].style.visibility = "hidden";
        oFilaSeguimiento.cells[4].children[1].onmouseover = null;
        oFilaSeguimiento.cells[4].children[1].onmouseout = null;
        $I("divTotal").style.display = "none";
        mfa(oFilaSeguimiento, "U");
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el seguimiento.", e.message);
    }
}

function grabar() {
    try {
        var tblDatosSC = FilasDe("tblDatosSC");

        var sb = new StringBuilder; //sin paréntesis
        sb.Append("grabar@#@");

        for (var i = 0; i < tblDatosSC.length; i++) {
            if (tblDatosSC[i].getAttribute("bd") == "U") {
                sb.Append(tblDatosSC[i].id + "{sep}");
                sb.Append(((tblDatosSC[i].cells[4].children[0].checked) ? "1" : "0") + "{sep}");
                sb.Append(tblDatosSC[i].getAttribute("comentario"));
                sb.Append("{sepreg}");
            }
        }
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}