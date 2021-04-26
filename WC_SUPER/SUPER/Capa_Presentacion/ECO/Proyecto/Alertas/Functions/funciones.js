var bLectura = true;
var oImgOK = document.createElement("img");
oImgOK.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOK.gif");
oImgOK.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

var oFecha = document.createElement("input");
oFecha.setAttribute("type", "text");
oFecha.className = "txtM";
oFecha.setAttribute("style", "width:90px; text-align:center;");

var oGoma = document.createElement("img");
oGoma.setAttribute("src", "../../../../images/botones/imgBorrar.gif");
oGoma.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; cursor:pointer;");

var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
oChk.setAttribute("className", "checkTabla");
oChk.setAttribute("style", "width:20px; cursor:pointer;");

function init() {
    try {
        if (es_administrador != "" && $I("hdnEstado").value=="A")
            bLectura = false;
        else
            bLectura = true;
        scrollTabla();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);	
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                bCambios = false;
                salir();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
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

        var sAux = "";
        var nFilaVisible = Math.floor(nTopScroll / 20);
        var tblDatos = $I("tblDatos");
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                //oFila.attachEvent("onclick", mm);
                if (!bLectura) {
                    //Solo inserto check si el usuario es administrador y el proyecto está abierto
                    var oChkAux = oChk.cloneNode(true);
                    if (oFila.getAttribute("a")=="1")//Miro si está activo o no
                        oChkAux.setAttribute("checked", "true");
                    oChkAux.onclick = function() { setHabilitada(this) };
                    oFila.cells[2].appendChild(oChkAux);

                    var oFISB = oFecha.cloneNode(true);
                    if (oFila.getAttribute("a") == "1") {
                        oFISB.onclick = function() { getPeriodo(this); };
                        oFISB.style.cursor = "pointer";
                    }
                    oFISB.id = "FISB_" + oFila.id;
                    oFISB.setAttribute("readonly", "readonly");
                    if (oFila.getAttribute("inistandby") != "")
                        oFISB.value = AnoMesToMesAnoDescLong(oFila.getAttribute("inistandby"));
                    oFila.cells[3].appendChild(oFISB);

                    var oFFSB = oFecha.cloneNode(true);
                    if (oFila.getAttribute("a") == "1") {
                        oFFSB.onclick = function() { getPeriodo(this); };
                        oFISB.style.cursor = "pointer";
                    }
                    oFFSB.id = "FFSB_" + oFila.id;
                    oFFSB.setAttribute("readonly", "readonly");
                    if (oFila.getAttribute("finstandby") != "")
                        oFFSB.value = AnoMesToMesAnoDescLong(oFila.getAttribute("finstandby"));
                    oFila.cells[4].appendChild(oFFSB);
                    if (oFila.getAttribute("finstandby") != "") {
                        oFila.cells[4].appendChild(oGoma.cloneNode(true));
                        oFila.cells[4].children[1].onclick = function() { delPeriodo(this); };
                    }
                    if (oFila.getAttribute("seg") != "") {
                        oFila.cells[5].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("seg")); }
                        oFila.cells[5].children[1].onmouseout = function() { hideTTE(); }
                    }
                } 
                else {
                    if (oFila.getAttribute("a") == "1")//Miro si está activo o no
                        oFila.cells[2].appendChild(oImgOK.cloneNode(false));

                    if (oFila.getAttribute("inistandby") != "") {
                        oFila.cells[3].style.textAlign = "center";
                        oFila.cells[3].innerText = AnoMesToMesAnoDescLong(oFila.getAttribute("inistandby"));
                    }
                    if (oFila.getAttribute("finstandby") != "") {
                        oFila.cells[4].style.textAlign = "center";
                        oFila.cells[4].innerText = AnoMesToMesAnoDescLong(oFila.getAttribute("finstandby"));
                    }
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de alertas.", e.message);
    }
}

function cancelar() {
    try {
        bCambios = false;
        salir();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cancelar", e.message);
    }
}

function grabar() {
    try {
        var tblDatos = FilasDe("tblDatos");

        var sb = new StringBuilder; //sin paréntesis
        sb.Append("grabar@#@");
        
        for (var i = 0; i < tblDatos.length; i++) {
            if (tblDatos[i].getAttribute("bd") == "U") {
                sb.Append($I("hdnPSN").value + "{sep}");
                sb.Append(tblDatos[i].id + "{sep}");
                sb.Append(((tblDatos[i].cells[2].children[0].checked) ? "1" : "0") + "{sep}");
                sb.Append(tblDatos[i].getAttribute("inistandby") + "{sep}");
                sb.Append(tblDatos[i].getAttribute("finstandby") + "{sep}");
                sb.Append(tblDatos[i].getAttribute("seg") + "{sep}");
                sb.Append("{sepreg}");
            }
        }

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function getPeriodo(oInput) {
    try {
        var oFila = null;
        var oControl = oInput;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila a de las fechas", 300);
            return;
        }
    
        mostrarProcesando();

        var sDesde = oFila.getAttribute("inistandby");
        var sHasta = oFila.getAttribute("finstandby");

        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar(sDesde) + "&sH=" + codpar(sHasta);
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 230));
        modalDialog.Show(strEnlace, self, sSize(550, 230))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("inistandby", aDatos[0]);
                    oFila.cells[3].children[0].value = AnoMesToMesAnoDescLong(aDatos[0]);
                    oFila.setAttribute("finstandby", aDatos[1]);
                    oFila.cells[4].children[0].value = AnoMesToMesAnoDescLong(aDatos[1]);
                    if (oFila.cells[4].children.length == 1)
                        oFila.cells[4].appendChild(oGoma.cloneNode(true));
                    oFila.cells[4].children[1].onclick = function() { delPeriodo(this); };
                    mfa(oFila, "U");
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function delPeriodo(oImgGoma) {
    try {
        var oFila = null;
        var oControl = oImgGoma;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila a de las fechas", 300);
            return;
        }

        oFila.setAttribute("inistandby", "");
        oFila.setAttribute("finstandby", "");
        oFila.cells[3].children[0].value = "";
        oFila.cells[4].children[0].value = "";
        oFila.cells[4].children[1].removeNode();
        mfa(oFila, "U");
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function setHabilitada(oCheck) {
    try {
        var oFila = null;
        var oControl = oCheck;
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

        //var oFila = oCheck.parentNode.parentNode;
        if (!oCheck.checked) {
            oFila.setAttribute("inistandby", "");
            oFila.cells[3].children[0].value = "";
            oFila.setAttribute("finstandby", "");
            oFila.cells[4].children[0].value = "";
            oFila.cells[3].children[0].style.cursor = "default";
            oFila.cells[3].children[0].onclick = null;
            oFila.cells[4].children[0].style.cursor = "default";
            oFila.cells[4].children[0].onclick = null;
            if (oFila.cells[4].children[1]) oFila.cells[4].children[1].removeNode();
        } else {
            oFila.cells[3].children[0].style.cursor = "pointer";
            oFila.cells[3].children[0].onclick = function() { getPeriodo(this) };
            oFila.cells[4].children[0].style.cursor = "pointer";
            oFila.cells[4].children[0].onclick = function() { getPeriodo(this) };
        }
        mfa(oFila, "U");
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar/deshabilitar la alerta", e.message);
    }
}

var oFilaSeguimiento;
var sAccionSeguimiento = "";
var sOrigenModificacion = "";
//function setSeguimiento(oControl) {
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
        mostrarErrorAplicacion("Error al activar/desactivar el seguimiento.", e.message);
    }
}

function ActivarSeguimiento() {
    try {
        sAccionSeguimiento = "Activar";
        $I("lblTextoSeguimiento").innerText = "Para ACTIVAR un seguimiento, es preciso indicar el motivo del mismo.";
        $I("txtSeguimiento").ReadOnly = false;
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));

        $I("lblBoton").innerText = "Activar";
        $I("imgBotonActivar").src = "../../../../images/imgSegAdd.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelarSeg").className = "btnH25W100";
        $I("divTotal").style.display = "block";
        $I("btnActivarDesactivar").onclick = function() { Activar(); };
        $I("txtSeguimiento").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el seguimiento.", e.message);
    }
}
function DesactivarSeguimiento() {
    try {
        sAccionSeguimiento = "Desactivar";
        $I("lblTextoSeguimiento").innerText = "Para confirmar la DESACTIVACIÓN, pulsa el botón \"Desactivar\". En caso contrario pulsa \"Cancelar\".";
        $I("txtSeguimiento").ReadOnly = true;
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));

        $I("lblBoton").innerText = "Desactivar";
        $I("imgBotonActivar").src = "../../../../images/imgSegDel.png";
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
            oFilaSeguimiento.cells[5].children[0].checked = !oFilaSeguimiento.cells[5].children[0].checked;
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
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));
        $I("txtSeguimiento").ReadOnly = false;

        $I("lblBoton").innerText = "Modificar";
        $I("imgBotonActivar").src = "../../../../images/imgSegAdd.png";
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

        oFilaSeguimiento.setAttribute("seg", Utilidades.escape($I("txtSeguimiento").value));
        oFilaSeguimiento.cells[5].children[1].style.visibility = "visible";
        oFilaSeguimiento.cells[5].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("seg")); }
        oFilaSeguimiento.cells[5].children[1].onmouseout = function() { hideTTE(); }
        $I("divTotal").style.display = "none";
        mfa(oFilaSeguimiento, "U");
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el seguimiento.", e.message);
    }
}

function Desactivar() {
    try {
        oFilaSeguimiento.setAttribute("seg", "");
        oFilaSeguimiento.cells[5].children[1].style.visibility = "hidden";
        oFilaSeguimiento.cells[5].children[1].onmouseover = null;
        oFilaSeguimiento.cells[5].children[1].onmouseout = null;
        $I("divTotal").style.display = "none";
        mfa(oFilaSeguimiento, "U");
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el seguimiento.", e.message);
    }
}