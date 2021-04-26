var bAlgunCambio = false;
/*
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
*/
var oImgOK = null;
var oFecha = null; 
var oGoma = null; 
var oChk = null; 

function init() {
    try {
        if (!mostrarErrores()) return;

        oFecha = document.createElement("input");
        oFecha.setAttribute("type", "text");
        oFecha.className = "txtM";
        oFecha.setAttribute("style", "width:90px; text-align:center;");

        oChk = document.createElement("input");
        oChk.setAttribute("type", "checkbox");
        oChk.setAttribute("className", "checkTabla");
        oChk.setAttribute("style", "width:20px; cursor:pointer;");

        oGoma = document.createElement("img");
        oGoma.setAttribute("src", location.href.substring(0, nPosCUR) + "images/botones/imgBorrar.gif");
        oGoma.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; cursor:pointer;");

        oImgOK = document.createElement("img");
        oImgOK.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOK.gif");
        oImgOK.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

        if ($I("hdnGrupoOC").value == "0") {
            //$I("chkObraCurso").disabled = true;
            $I("txtCausa").readOnly = true;
            $I("txtAcciones").readOnly = true;
            setOp($I("fstObraCurso"), 30);
        } else {
            $I("rdbOC_0").style.verticalAlign = "middle";
            $I("rdbOC_0").style.cursor = "pointer";
            $I("rdbOC_1").style.verticalAlign = "middle";
            $I("rdbOC_1").style.cursor = "pointer";
        }

        $I("spanProyecto").onmouseover = function() { showTTE(sTooltipInfoProy); }
        $I("spanProyecto").onmouseout = function() { hideTTE(); }

        var oGomaActual = oGoma.cloneNode(true);
        oGomaActual.id = "imgGomaAlertaActual";
        oGomaActual.style.visibility = "hidden";
        oGomaActual.onclick = function() { delPeriodo(this, true); };
        $I("lgdStandby").appendChild(oGomaActual);

        

        if ($I("trAlertaActual").getAttribute("idAlerta") == "0"){
            $I("chkHabilitada").disabled = true;
        }else{
            $I("fstStandby").style.visibility = "visible";
            if ($I("chkHabilitada").checked) {
                $I("txtIniStby").style.cursor = "pointer";
                $I("txtIniStby").onclick = function() { getPeriodo(this, true); };
                $I("txtFinStby").style.cursor = "pointer";
                $I("txtFinStby").onclick = function() { getPeriodo(this, true); };
            }
        }
        
        
        var oFila = $I("trAlertaActual");
        if (oFila.getAttribute("inistandby") != "") {
            oGomaActual.style.visibility = "visible";
        }

        scrollTabla();
        setTooltipSeguimiento();
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "cerrarDialogo":
                bCambios = false;
                var returnValue = "OK";
                modalDialog.Close(window, returnValue);		
                return;
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function cancelar() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);	
}

var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogoAlertas").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogoAlertas").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }

        var sAux = "";
        //var nFilaVisible = Math.floor(nTopScroll / 20);
        var tblOtrasAlertas = $I("tblOtrasAlertas");
        //var nUltFila = Math.min(nFilaVisible + $I("divCatalogoAlertas").offsetHeight / 20 + 1, tblOtrasAlertas.rows.length);
        var oFila;
        //for (var i = nFilaVisible; i < nUltFila; i++) {
        for (var i = 0; i < tblOtrasAlertas.rows.length; i++) {
            if (!tblOtrasAlertas.rows[i].getAttribute("sw")) {
                oFila = tblOtrasAlertas.rows[i];
                oFila.setAttribute("sw", 1);
                //oFila.attachEvent("onclick", mm);

//                //Solo inserto check si el usuario es administrador y el proyecto está abierto
//                var oChkAux = oChk.cloneNode(true);
//                if (oFila.getAttribute("a") == "1")//Miro si está activo o no
//                    oChkAux.setAttribute("checked", "true");
//                oChkAux.onclick = function() { setHabilitada(this) };
//                oFila.cells[1].appendChild(oChkAux);

                var oFISB = oFecha.cloneNode(true);
                if (oFila.getAttribute("a") == "1") {
                    oFISB.onclick = function() { getPeriodo(this); };
                    oFISB.style.cursor = "pointer";
                }
                oFISB.id = "FISB_" + oFila.id;
                oFISB.setAttribute("readonly", "readonly");
                if (oFila.getAttribute("inistandby") != "")
                    oFISB.value = AnoMesToMesAnoDescLong(oFila.getAttribute("inistandby"));
                oFila.cells[2].appendChild(oFISB);

                var oFFSB = oFecha.cloneNode(true);
                if (oFila.getAttribute("a") == "1") {
                    oFFSB.onclick = function() { getPeriodo(this); };
                    oFISB.style.cursor = "pointer";
                }
                oFFSB.id = "FFSB_" + oFila.id;
                oFFSB.setAttribute("readonly", "readonly");
                if (oFila.getAttribute("finstandby") != "")
                    oFFSB.value = AnoMesToMesAnoDescLong(oFila.getAttribute("finstandby"));
                oFila.cells[3].appendChild(oFFSB);
                if (oFila.getAttribute("finstandby") != "") {
                    oFila.cells[3].appendChild(oGoma.cloneNode(true));
                    oFila.cells[3].children[1].onclick = function() { delPeriodo(this); };
                }
                if (oFila.getAttribute("seg") != "") {
                    oFila.cells[4].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("seg")); }
                    oFila.cells[4].children[1].onmouseout = function() { hideTTE(); }
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de alertas.", e.message);
    }
}

function setTooltipSeguimiento() {
    try {
        var tblAlertaActual = $I("tblAlertaActual");
        if (tblAlertaActual.rows[0].getAttribute("seg") != "") {
            $I("imgSegAlertaActual").onmouseover = function() { showTTE($I("tblAlertaActual").rows[0].getAttribute("seg")); }
            $I("imgSegAlertaActual").onmouseout = function() { hideTTE(); }
        }
        
        var tblDatosAlertas = $I("tblOtrasAlertas");
        for (var i = 0; i < tblDatosAlertas.rows.length; i++) {
            if (tblDatosAlertas.rows[i].getAttribute("seg") != "") {
                tblDatosAlertas.rows[i].cells[4].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("seg")); }
                tblDatosAlertas.rows[i].cells[4].children[1].onmouseout = function() { hideTTE(); }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los tooltip de seguimiento.", e.message);
    }
}

var oFilaSeguimiento;
var sAccionSeguimiento = "";
function setSeguimiento(e) {
    try {
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
        
        //alert(oFila.getAttribute("idr") + ": " + ((oCheck.checked) ? "activar" : "desactivar"));
        oFilaSeguimiento = (oControl.parentNode.parentNode.id == "tblAlertaActual") ? $I("tblAlertaActual").rows[0] : oControl;
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
        $I("btnCancelar").className = "btnH25W100";
        $I("divTotal").style.display = "block";
//        $I("btnActivarDesactivar").detachEvent('onclick', Desactivar);
//        $I("btnActivarDesactivar").attachEvent('onclick', Activar);
        $I("btnActivarDesactivar").onclick = function() { Activar(); };
        $I("txtSeguimiento").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el seguimiento.", e.message);
    }
}
function DesactivarSeguimiento() {
    try {
        sAccionSeguimiento = "Desactivar";
        $I("lblTextoSeguimiento").innerText = "Para confirmar la DESACTIVACIÓN, pulsa el botón \"Desactivar\". En caso contrario pulse \"Cancelar\".";
        $I("txtSeguimiento").ReadOnly = true;
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));

        $I("lblBoton").innerText = "Desactivar";
        $I("imgBotonActivar").src = "../../../../images/imgSegDel.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelar").className = "btnH25W100";
        $I("divTotal").style.display = "block";
//        $I("btnActivarDesactivar").detachEvent('onclick', Activar);
//        $I("btnActivarDesactivar").attachEvent('onclick', Desactivar);
        $I("btnActivarDesactivar").onclick = function() { Desactivar(); };
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el seguimiento.", e.message);
    }
}
function CancelarSeguimiento() {
    try {
        if (sAccionSeguimiento != "Modificar") {
            if (oFilaSeguimiento.parentNode.parentNode.id == "tblAlertaActual") {
                //$I("chkHabilitada").checked = !$I("chkHabilitada").checked;
                $I("chkSeguimiento").checked = !$I("chkSeguimiento").checked;
            } else {
                oFilaSeguimiento.cells[4].children[0].checked = !oFilaSeguimiento.cells[4].children[0].checked;
            }
        }
        $I("divTotal").style.display = "none";
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar el seguimiento.", e.message);
    }
}
function ModificarSeguimiento(oControl) {
    try {
        sAccionSeguimiento = "Modificar";
        var oFila;
        var oCheck = oControl;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }
        oFilaSeguimiento = (oControl.parentNode.parentNode.id == "tblAlertaActual") ? $I("tblAlertaActual").rows[0] : oControl;
        $I("lblTextoSeguimiento").innerText = "";
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));
        $I("txtSeguimiento").ReadOnly = false;

        //$I("lblBoton").innerText = "Grabar";
        //$I("imgBotonActivar").src = "../../../../images/botones/imgGrabarSalir.gif";
        $I("lblBoton").innerText = "Modificar";
        $I("imgBotonActivar").src = "../../../../images/imgSegAdd.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelar").className = "btnH25W100";
        $I("divTotal").style.display = "block";
//        $I("btnActivarDesactivar").detachEvent('onclick', Desactivar);
//        $I("btnActivarDesactivar").attachEvent('onclick', Activar);
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

        if (oFilaSeguimiento.parentNode.parentNode.id == "tblAlertaActual") {
            $I("tblAlertaActual").rows[0].setAttribute("seg", Utilidades.escape($I("txtSeguimiento").value));
            $I("imgSegAlertaActual").style.visibility = "visible";
            $I("imgSegAlertaActual").onmouseover = function() { showTTE($I("tblAlertaActual").rows[0].getAttribute("seg")); }
            $I("imgSegAlertaActual").onmouseout = function() { hideTTE(); }
        } else {
            oFilaSeguimiento.setAttribute("seg", Utilidades.escape($I("txtSeguimiento").value));
            oFilaSeguimiento.cells[4].children[1].style.visibility = "visible";
            oFilaSeguimiento.cells[4].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("seg")); }
            oFilaSeguimiento.cells[4].children[1].onmouseout = function() { hideTTE(); }
        }
        $I("divTotal").style.display = "none";
        mfa(oFilaSeguimiento, "U");
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el seguimiento.", e.message);
    }
}

function Desactivar() {
    try {
        if (oFilaSeguimiento.parentNode.parentNode.id == "tblAlertaActual") {
            $I("tblAlertaActual").rows[0].setAttribute("seg", "");
            $I("imgSegAlertaActual").style.visibility = "hidden";
            $I("imgSegAlertaActual").onmouseover = null;
            $I("imgSegAlertaActual").onmouseout = null;
        } else {
            oFilaSeguimiento.setAttribute("seg", "");
            oFilaSeguimiento.cells[4].children[1].style.visibility = "hidden";
            oFilaSeguimiento.cells[4].children[1].onmouseover = null;
            oFilaSeguimiento.cells[4].children[1].onmouseout = null;
        }
        mfa(oFilaSeguimiento, "U");
        $I("divTotal").style.display = "none";
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el seguimiento.", e.message);
    }
}

function getPeriodo(oInput, bAlertaActual) {
    try {
        var oFila = null;
        if (!bAlertaActual) {
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
        } else {
            oFila = $I("trAlertaActual");
        }

        mostrarProcesando();
        var sTabla = oFila.parentNode.parentNode.id;
        var sDesde = "", sHasta = "";
//        var oTabla;
//        if (sTabla == "tblOtrasAlertas") {
            sDesde = oFila.getAttribute("inistandby");
            sHasta = oFila.getAttribute("finstandby");
//        } else {
//            oTabla = $I("tblAlertaActual");
//            sDesde = oTabla.rows[0].getAttribute("inistandby");
//            sHasta = oTabla.rows[0].getAttribute("finstandby");
//        }

        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD="+ codpar(sDesde) + "&sH=" + codpar(sHasta);
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 230));
        modalDialog.Show(strEnlace, self, sSize(550, 230))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    if (sTabla == "tblOtrasAlertas") {
                        oFila.setAttribute("inistandby", aDatos[0]);
                        oFila.cells[2].children[0].value = AnoMesToMesAnoDescLong(aDatos[0]);
                        oFila.setAttribute("finstandby", aDatos[1]);
                        oFila.cells[3].children[0].value = AnoMesToMesAnoDescLong(aDatos[1]);
                        if (oFila.cells[3].children.length == 1)
                            oFila.cells[3].appendChild(oGoma.cloneNode(true));
                        oFila.cells[3].children[1].onclick = function() { delPeriodo(this); };
                        mfa(oFila, "U");
                    } else {
                        oFila.setAttribute("inistandby", aDatos[0]);
                        $I("txtIniStby").value = AnoMesToMesAnoDescLong(aDatos[0]);
                        oFila.setAttribute("finstandby", aDatos[1]);
                        $I("txtFinStby").value = AnoMesToMesAnoDescLong(aDatos[1]);
                        $I("imgGomaAlertaActual").style.visibility = "visible";
                        mfa(oFila, "U");
                    }
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function delPeriodo(oImgGoma, bAlertaActual) {
    try {
        var oFila = null;
        if (!bAlertaActual) {
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
        } else {
            oFila = $I("trAlertaActual");
        }

        oFila.setAttribute("inistandby", "");
        oFila.setAttribute("finstandby", "");
        if (!bAlertaActual) {
            oFila.cells[2].children[0].value = "";
            oFila.cells[3].children[0].value = "";
            oFila.cells[3].children[1].removeNode();
        } else {
            $I("txtIniStby").value = "";
            $I("txtFinStby").value = "";
            $I("imgGomaAlertaActual").style.visibility = "hidden";
        }
        mfa(oFila, "U");
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function setHabilitada(oCheck, bAlertaActual) {
    try {
        var oFila = null;
        var sTabla =  null;
        if (!bAlertaActual) {
            oFila = oCheck.parentNode.parentNode;
        } else {
            oFila = $I("trAlertaActual");
        }
        sTabla = oFila.parentNode.parentNode.id;
        //alert(sTabla);
        if (sTabla == "tblOtrasAlertas") {
            if (!oCheck.checked) {
                oFila.setAttribute("inistandby", "");
                oFila.cells[2].children[0].value = "";
                oFila.setAttribute("finstandby", "");
                oFila.cells[3].children[0].value = "";
                if (oFila.cells[3].children[1]) oFila.cells[3].children[1].removeNode();
                mfa(oFila, "U");
                oFila.cells[2].children[0].style.cursor = "default";
                oFila.cells[2].children[0].onclick = null;
                oFila.cells[3].children[0].style.cursor = "default";
                oFila.cells[3].children[0].onclick = null;
            } else {
                oFila.cells[2].style.cursor = "pointer";
                oFila.cells[2].onclick = function() { getPeriodo(this.parentNode) };
                oFila.cells[3].style.cursor = "pointer";
                oFila.cells[3].onclick = function() { getPeriodo(this.parentNode) };
            }
        } else {//tblAlertaActual
            var oTabla = $I("tblAlertaActual");
            oFila = $I("trAlertaActual");
            if (!oCheck.checked) {
                oTabla.rows[0].setAttribute("inistandby", "");
                $I("txtIniStby").value = "";
                oTabla.rows[0].setAttribute("finstandby", "");
                $I("txtFinStby").value = "";
                mfa(oTabla.rows[0], "U");
                $I("txtIniStby").style.cursor = "default";
                $I("txtIniStby").onclick = null;
                $I("txtFinStby").style.cursor = "default";
                $I("txtFinStby").onclick = null;
                $I("imgGomaAlertaActual").style.visibility = "hidden";
            } else {
                $I("txtIniStby").style.cursor = "pointer";
                $I("txtIniStby").onclick = function() { getPeriodo(this, true) };
                $I("txtFinStby").style.cursor = "pointer";
                $I("txtFinStby").onclick = function() { getPeriodo(this, true) };
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar/deshabilitar la alerta", e.message);
    }
}

function setCierre(oCheck, nOpcion) {
    try {
        var oFila = oCheck.parentNode.parentNode;
        if (nOpcion == 1) {//cerrar conforme
            if (oCheck.checked) oFila.cells[4].children[0].checked = false;
        } else {
            if (oCheck.checked) oFila.cells[3].children[0].checked = false;
        }
        mfa(oFila, "U");
    } catch (e) {
        mostrarErrorAplicacion("Error al actuar sobre otros diálogos abiertos.", e.message);
    }
}

function cerrarDialogo(nEstado) {
    try {
        jqConfirm("", "Has elegido cerrar el diálogo " + ((nEstado == 4) ? "aprobando" : "sin aprobar") + " su contenido.<br><br>Pulsa \"Aceptar\" para confirmarlo.", "", "", "war", 450).then(function (answer) {
            if (answer) {
                var sValorOC = "";
                if ($I("hdnGrupoOC").value == "1") {
                    sValorOC = getRadioButtonSelectedValue("rdbOC", true);
                    if (sValorOC == "") {
                        mmoff("WarPer", "Debes indicar si se justifica la existencia", 270);
                        return;
                    }
                }
                //Si es un diálogo de OC o FA, hago obligatorio el motivo codificado de cierre
                if ($I("hdnGrupoOC").value != "0") {
                    if ($I("cboMotivo").value == "") {
                        mmoff("WarPer", "Debes indicar el motivo", 220);
                        return;
                    }
                }
                mostrarProcesando();

                $I("hdnIdEstado").value = nEstado;

                var sb = new StringBuilder;
                sb.Append("cerrarDialogo@#@");

                //Datos alerta actual
                sb.Append($I("trAlertaActual").getAttribute("idPSN") + "{sep}");
                sb.Append($I("trAlertaActual").getAttribute("idAlerta") + "{sep}");
                sb.Append((($I("chkHabilitada").checked) ? "1" : "0") + "{sep}");
                sb.Append($I("trAlertaActual").getAttribute("inistandby") + "{sep}");
                sb.Append($I("trAlertaActual").getAttribute("finstandby") + "{sep}");
                sb.Append($I("trAlertaActual").getAttribute("seg"));

                sb.Append("@#@");

                //Datos otras alertas bajo mi gestión.
                var tblOtrasAlertas = $I("tblOtrasAlertas");
                for (var i = 0; i < tblOtrasAlertas.rows.length; i++) {
                    if (tblOtrasAlertas.rows[i].getAttribute("bd") == "U") {
                        sb.Append(tblOtrasAlertas.rows[i].getAttribute("idPSN") + "{sep}");
                        sb.Append(tblOtrasAlertas.rows[i].getAttribute("idAlerta") + "{sep}");
                        sb.Append(((tblOtrasAlertas.rows[i].cells[1].children[0].checked) ? "1" : "0") + "{sep}");
                        sb.Append(tblOtrasAlertas.rows[i].getAttribute("inistandby") + "{sep}");
                        sb.Append(tblOtrasAlertas.rows[i].getAttribute("finstandby") + "{sep}");
                        sb.Append(tblOtrasAlertas.rows[i].getAttribute("seg") + "{sepReg}");
                    }
                }

                sb.Append("@#@");

                //Datos dialogo actual
                sb.Append($I("hdnIdDialogo").value + "{sep}");
                sb.Append($I("hdnIdEstado").value + "{sep}");
                sb.Append(sValorOC + "{sep}");
                sb.Append(Utilidades.escape($I("txtCausa").value) + "{sep}");
                sb.Append(Utilidades.escape($I("txtAcciones").value) + "{sep}");
                sb.Append($I("cboMotivo").value);

                sb.Append("@#@");

                //Datos otros dialogos abiertos bajo mi gestión.
                var tblOtrosDialogos = $I("tblOtrosDialogos");
                for (var i = 0; i < tblOtrosDialogos.rows.length; i++) {
                    if (tblOtrosDialogos.rows[i].getAttribute("bd") == "U"
                        &&
                        (
                            tblOtrosDialogos.rows[i].cells[3].children[0].checked
                            || tblOtrosDialogos.rows[i].cells[4].children[0].checked
                        )
                        ) {
                        sb.Append(tblOtrosDialogos.rows[i].getAttribute("idDialogo") + "{sep}");
                        sb.Append(((tblOtrosDialogos.rows[i].cells[3].children[0].checked) ? "1" : "0") + "{sep}");
                        sb.Append(((tblOtrosDialogos.rows[i].cells[4].children[0].checked) ? "1" : "0") + "{sepReg}");
                    }
                }

                //ocultarProcesando();
                //alert(sb.ToString());
                RealizarCallBack(sb.ToString(), "");
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a cerrar el diálogo.", e.message);
    }
}
