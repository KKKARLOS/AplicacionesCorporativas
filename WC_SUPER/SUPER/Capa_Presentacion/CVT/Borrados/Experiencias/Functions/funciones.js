var aFila;

function init() {
    try {
        getPendiente();
        //mmoff("InfPer", "Obteniendo las experiencias a borrar...", 300);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
oChk.setAttribute("style", "cursor:pointer; vertical-align:middle;");

var oImgInfo = document.createElement("img");
oImgInfo.setAttribute("src", location.href.substring(0, nPosCUR) + "images/info.gif");
oImgInfo.setAttribute("style", "cursor:pointer;vertical-align:middle; margin-left:2px; border: 0px; visibility:hidden; width:16px; height:16px;");

function getPendiente() {
    try {
        mostrarProcesando();
        var js_args = "getPendiente@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getPendiente":
                mmoff("hide");
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                scrollTabla();
                //deshabilitarCualificables();
                actualizarLupas("tblTitulo", "tblDatos");
                break;
            case "grabar":
                bCambios = false;
                desActivarGrabar();
                setTimeout("getPendiente();", 20);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

/* Mostrar detalle */
//function md(oFila, e) {
function md(oFila) {
    try {
        /*        
        try {
        if (!e) var e = window.event
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();
        } catch (e) { };
        */
        if (bCambios) {
            mmoff("War", "Para poder acceder al detalle, debe previamente pulsar el botón de grabación", 300);
            return;
        }
        //Acceso a la experiencia profesional desde proyecto SUPER
        var sParam = "?o=P";
        sParam += "&pr=" + codpar(oFila.getAttribute("idproyecto"));
        sParam += "&m=" + codpar("W");
        sParam += "&ep=" + codpar(oFila.getAttribute("expprof"));

        mostrarProcesando();
        ocultarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/CVT/MiCV/ExpProf/Default.aspx" + sParam, self, sSize(980, 630))
            .then(function(ret) {
                if (ret != null) {
                    if (ret.bDatosModificados) {
                        getPendiente();
                    }
                }
            });

        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al abrir detalle", e.message);
    }
}
function motivo(nFilaParam) {
    //var tblDatos = $I("tblDatos");
    activarGrabar();
    if (aFila[nFilaParam].cells[6].children[0].checked == true) {
        aFila[nFilaParam].setAttribute("idPlDest", "");
        if (aFila[nFilaParam].cells[7].children.length > 0) {
            aFila[nFilaParam].cells[7].children[0].onmouseover = "";
            aFila[nFilaParam].cells[7].children[0].onmouseout = "";
        }
        aFila[nFilaParam].cells[5].children[0].checked = false;
        $I("btnAceptarMotivo").className = "btnH25W95";
        $I("btnCancelarMotivo").className = "btnH25W95";
        //Si estamos tratando una línea de perfil, cambiamos el texto
        //if (aFila[nFilaParam].cells[3].innerHTML != "")
        if (getCelda(aFila[nFilaParam], 3) != "")
            $I("lblMotivo").innerHTML = "Indica el motivo por el que no debe borrarse el perfil de la experiencia profesional";
        else
            $I("lblMotivo").innerHTML = "Indica el motivo por el que no debe borrarse la experiencia profesional";
        $I("divFondoMotivo").style.visibility = "visible";
        $I("txtMotivoRT").value = Utilidades.unescape(aFila[nFilaParam].getAttribute("motivo"));
        $I("txtMotivoRT").focus();
    }
    else {
        $I("txtMotivoRT").value = "";
        aFila[nFilaParam].cells[6].children[1].style.visibility = "hidden";
        //aFila[nFilaParam].cells[6].children[1].setAttribute("title", "");
        aFila[nFilaParam].cells[6].children[1].onmouseover = null;
        aFila[nFilaParam].cells[6].children[1].onmouseout = null;
        aFila[nFilaParam].setAttribute("motivo", "");
    }
}
function AceptarMotivo() {
    try {
        if ($I("txtMotivoRT").value.Trim() == "") {
            mmoff("War", "Debes indicar un motivo.", 180);
            return;
        }
        aFila[iFila].setAttribute("motivo", Utilidades.escape($I("txtMotivoRT").value));
        aFila[iFila].cells[6].children[1].style.visibility = "visible";

        aFila[iFila].cells[6].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("motivo"), null, null, 350); }
        aFila[iFila].cells[6].children[1].onmouseout = function() { hideTTE(); }

        $I("divFondoMotivo").style.visibility = "hidden";
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar la obtención de motivo.", e.message);
    }
}

function CancelarMotivo() {
    try {
        if ($I("txtMotivoRT").value == "") {
            aFila[iFila].cells[6].children[0].checked = false;
            aFila[iFila].cells[6].children[1].style.visibility = "hidden";
            aFila[iFila].cells[6].children[1].onmouseover = null;
            aFila[iFila].cells[6].children[1].onmouseout = null;
            aFila[iFila].setAttribute("motivo", "");
        }
        $I("divFondoMotivo").style.visibility = "hidden";
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar la obtención de motivo.", e.message);
    }
}

function grabar() {
    try {

        if (!comprobarDatos()) return;
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        //var aFila = FilasDe("tblDatos");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (!aFila[i].getAttribute("sw")) continue;
                if (aFila[i].getAttribute("idPlDest") != "") {//Si hay plantilla destino procedo a su reasignación
                    sb.Append("R@dato@" + aFila[i].getAttribute("id") + "@dato@" + aFila[i].getAttribute("idPlDest") + "@fila@");
                }
                else {
                    if (aFila[i].cells[6].children[0].checked == true) {
                        sb.Append("N@dato@" + aFila[i].getAttribute("id") + "@dato@" + aFila[i].getAttribute("red"));
                        sb.Append("@dato@" + getCelda(aFila[i], 2) + "@dato@" + aFila[i].getAttribute("motivo"));
                        sb.Append("@dato@" + aFila[i].getAttribute("denCli") + "@dato@");
                        if (getCelda(aFila[i], 3) != "")
                            sb.Append("P@dato@" + getCelda(aFila[i], 3) + "@fila@");
                        else
                            sb.Append("E@dato@@fila@");
                    }
                    else {
                        if (aFila[i].cells[5].children[0].checked == true) {
                            sb.Append("B@dato@" + aFila[i].getAttribute("id") + "@dato@" + aFila[i].getAttribute("bP") + "@fila@");
                        }
                    }
                }
            }
            RealizarCallBack(sb.ToString(), "");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}
function comprobarDatos() {
    try {
        //var aFila = FilasDe("tblDatos");
        if (aFila != null) {
            for (i = 0; i <= aFila.length - 1; i++) {
                if (!aFila[i].getAttribute("sw")) continue;
                if ((aFila[i].cells[6].children[0].checked == true) && aFila[i].getAttribute("motivo") == "") {
                    mmoff("War", "Debe incluir el motivo de no aceptación de borrado de la experiencia " + aFila[i].cells[1].innerText + "-" + aFila[i].cells[2].innerText, 450, 2300);
                    return false;
                }
            }
            return true;
        }
        return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos.", e.message);
    }
}
var oImgFW = document.createElement("img");
oImgFW.setAttribute("src", "../../../../images/imgCatalogo.png");
oImgFW.className = "ICO";

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
        //var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);

        var oFila, sAux;
        var iCont = 0;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); };
                oFila.ondblclick = function() { md(this); };

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "I": oFila.cells[0].appendChild(oImgPV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "I": oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
                    }
                }

                var oChkInfo = oChk.cloneNode(true);
                oChkInfo.onclick = function() { borrar(this.parentNode.parentNode.rowIndex) };
                oFila.cells[5].appendChild(oChkInfo);
                
                var oChkInfo2 = oChk.cloneNode(true);
                oChkInfo2.onclick = function() { motivo(this.parentNode.parentNode.rowIndex) };
                oFila.cells[6].appendChild(oChkInfo2);

                var oInfo = oImgInfo.cloneNode(true);
                oInfo.onclick = function() { motivo(this.parentNode.parentNode.rowIndex) };
                oFila.cells[6].appendChild(oInfo);
                //Si tiene petición de borrado de plantilla añadimos botón de reasignar
                if (oFila.getAttribute("bP") == "S") {
                    oFila.cells[7].appendChild(oImgFW.cloneNode(true), null);
                    oFila.cells[7].children[0].onclick = function() { reasignarPlantilla(this.parentNode.parentNode); };
                    //oFila.cells[7].children[0].setAttribute("title", "Plantilla actual: " + oFila.getAttribute("denPl"));
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de diálogos.", e.message);
    }
}
function borrar(nFilaParam) {
    try {
        activarGrabar();
        aFila[nFilaParam].cells[6].children[0].checked = false;
        aFila[nFilaParam].cells[6].children[1].style.visibility = "hidden";
        aFila[nFilaParam].setAttribute("motivo", "");
        aFila[nFilaParam].setAttribute("idPlDest", "");
        if (aFila[nFilaParam].cells[7].children.length > 0) {
            aFila[nFilaParam].cells[7].children[0].onmouseover = "";
            aFila[nFilaParam].cells[7].children[0].onmouseout = "";
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar el motivo.", e.message);
    }
}
function reasignarPlantilla(oFila) {
    try {
        activarGrabar();
        oFila.cells[5].children[0].checked = false;
        oFila.cells[6].children[0].checked = false;
        oFila.cells[6].children[1].style.visibility = "hidden";
        oFila.setAttribute("motivo", "");
        
        mostrarProcesando();
        var t808_idexpprof = oFila.getAttribute("expprof")
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Plantilla/Catalogo/Default.aspx?ep=" + codpar(t808_idexpprof);
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(850, 400))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    if (aDatos[0] != "") {
                        if (aDatos[0] == oFila.getAttribute("idPlOri")) {
                            mmoff("War", "Has seleccionado el mismo perfil como origen y destino.", 300);
                        }
                        else {
                            oFila.setAttribute("idPlDest", aDatos[0]);
                            var sToolTip = "Perfil destino: " + aDatos[2];
                            if (oFila.cells[7].children.length > 0) {
                                oFila.cells[7].children[0].onmouseover = function() { showTTE(sToolTip, null, null, 350); }
                                oFila.cells[7].children[0].onmouseout = function() { hideTTE(); }
                            }
                        }
                    }
                }
            });
       ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar el motivo.", e.message);
    }
}