function init() {
    try {
        //alert(DescToAnoMes($I("txtMesValor").value));
        ToolTipBotonera("aparcar", "Almacena la situación destino");
        ToolTipBotonera("recuperar", "Recupera la situación destino almacenada");
        ToolTipBotonera("replica", "Genera las réplicas necesarias en meses cerrados");
        $I("txtNumero").focus();

        if (bHayAparcadas) $I("imgCaution").style.display = "block";
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultar = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "proyectos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogoRep").children[0].innerHTML = "";
                $I("divCatalogoRep").scrollTop = 0;
                scrollTablaProy();
                var tblDatos = $I("tblDatos");
                var tblDatos2 = $I("tblDatos2");
                
                if (aResul[3] != "") {
                    //quitar los duplicados 
                    for (var i = tblDatos2.rows.length - 1; i >= 0; i--) {
                        for (var x = 0; x < tblDatos.rows.length; x++) {
                            if (tblDatos2.rows[i].idProy == tblDatos.rows[x].idProy) {
                                tblDatos2.deleteRow(tblDatos2.rows[i].rowIndex);
                                break;
                            } else if (parseInt(tblDatos2.rows[i].idProy, 10) > parseInt(tblDatos.rows[x].idProy, 10)) {
                                break;
                            }
                        }
                    }

                    insertarFilasEnTablaDOM("tblDatos2", aResul[3], tblDatos2.rows.length);

                    //poner nodo si existe arriba.
                    for (var i = tblDatos2.rows.length - 1; i >= 0; i--) {
                        for (var x = 0; x < tblDatos.rows.length; x++) {
                            if (tblDatos2.rows[i].idProy == tblDatos.rows[x].idProy
                                && tblDatos2.rows[i].getAttribute("nodo_destino") == ""
                                && $I("hdnIdNodoDestino").value != ""
                                ) {
                                /*
                                tblDatos2.rows[i].setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);
                                tblDatos2.rows[i].cells[4].children[0].innerText = $I("txtCRDestino").value;

                                var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
                                var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
                                var sTitle = tblDatos2.rows[i].cells[4].children[0].title;
                                if (sTitle != "") {
                                    tblDatos2.rows[i].cells[4].children[0].title = sTootTip; //span
                                } else {
                                    tblDatos2.rows[i].cells[4].children[0].boBDY = sTootTip; //span
                                }
                                */
                                tblDatos2.rows[i].setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);

                                tblDatos2.rows[i].cells[4].innerHTML = "";
                                var oNOBR = document.createElement("NOBR");
                                oNOBR.className = "NBR W180";
                                tblDatos2.rows[i].cells[4].appendChild(oNOBR);
                                tblDatos2.rows[i].cells[4].children[0].style.width = "180px";
                                tblDatos2.rows[i].cells[4].children[0].innerText = $I("txtCRDestino").value;//$I("txtCRDestino").value;

                                var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
                                var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

                                tblDatos2.rows[i].cells[4].children[0].title = sTootTip; //span
                                break;
                            } else if (parseInt(tblDatos2.rows[i].idProy, 10) > parseInt(tblDatos.rows[x].idProy, 10)) {
                                break;
                            }
                        }
                    }

                    scrollTablaProyDest();
                }
                break;
            case "aparcar":
                mmoff("Suc","Situación destino almacenada correctamente", 300);
                break;
            case "aparcardel":
                $I("imgCaution").style.display = "none";
                mmoff("Suc", "Cambios eliminados correctamente", 300);
                break;
            case "recuperar":
                $I("imgCaution").style.display = "none";
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                $I("divCatalogo2").scrollTop = 0;
                scrollTablaProyDest();
                break;
            case "procesar":
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                $I("divCatalogo2").scrollTop = 0;
                if (aResul[3] == "1" && nIntentosProcesoDeadLock < nLimiteIntentosProcesoDeadLock) {//Error de deadlock o timeout
                    bOcultar = false;
                    nIntentosProcesoDeadLock++;
                    mmoff("Inf", "Existen varios procesos ejecutándose simultáneamente. Disculpa la espera.", 500, 5000);
                    setTimeout("procesar(true);", nSetTimeoutProcesoDeadLock);
                }
                else {
                    nIntentosProcesoDeadLock = 0;
                    if ($I("chkGenerarReplicas").checked) {
                        bOcultar = false;
                        mmoff("Inf", "Generando réplicas en meses cerrados", 300, 5000);
                        setTimeout("replicasmeses();", 20);
                    }
                }
                scrollTablaProyDest();
                break;
            case "getreplicas":
                $I("divCatalogoRep").children[0].innerHTML = aResul[2];
                $I("divCatalogoRep").scrollTop = 0;
                scrollTablaProyRep();
                break;
            case "replicasmeses":
                if (aResul[2] == "0") {
                    mmoff("Suc", "Réplicas generadas en meses cerrados", 300);
                }
                else {
                    if (nIntentosProcesoDeadLock < nLimiteIntentosProcesoDeadLock) {// Error de deadlock o timeout
                        bOcultar = false;
                        nIntentosProcesoDeadLock++;
                        mmoff("Inf", "Existen varios procesos ejecutándose simultáneamente. Disculpa la espera.", 500, 5000);
                        setTimeout("replicasmeses();", nSetTimeoutProcesoDeadLock);
                    }
                    else {
                        nIntentosProcesoDeadLock = 0;
                        mmoff("Err", "Las operaciones de otros usuarios han impedido la réplica de meses cerrados.\nDeja pasar unos minutos y vuelve a intentarlo.", 500);
                    }
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultar) ocultarProcesando();
    }
}

var sAmb = "";
function seleccionAmbito(strRblist) {
    try {
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else {
            sAmb = sOp;
            $I("divCatalogo").children[0].innerHTML = "";
            $I("divCatalogoRep").children[0].innerHTML = "";
            $I("ambCR").style.display = "none";
            $I("ambResponsable").style.display = "none";
            $I("ambNumero").style.display = "none";
            $I("ambCliente").style.display = "none";

            switch (sOp) {
                case "R":
                    $I("ambResponsable").style.display = "block";
                    mostrarRelacionProyectos("R", $I("hdnIdResponsable").value);
                    break;
                case "N":
                    $I("ambCR").style.display = "block";
                    if ($I("hdnIdNodoOrigen").value == "") return;
                    $I("txtCROrigen").value = $I("hdnDesNodoOrigen").value;
                    mostrarRelacionProyectos("N", $I("hdnIdNodoOrigen").value);
                    break;
                case "P":
                    $I("ambNumero").style.display = "block";
                    break;
                case "C":
                    mostrarRelacionProyectos("C", $I("hdnIdCliente").value);
                    $I("ambCliente").style.display = "block";
                    break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}

function mostrarRelacionProyectos(sOpcion, sValor, sParesDatos) {
    try {
        //alert("Proyectos("+ sOpcion +","+ sValor +")");
        if (sOpcion == "P") {
            if (sValor == "") return;
            if (isNaN(dfn(sValor))) {
                mmoff("War","El valor introducido no es numérico",180);
                $I("txtNumero").value = "";
                $I("txtNumero").focus();
                return;
            } else $I("txtNumero").value = sValor.ToString("N", 9, 0);
        }

        var js_args = "proyectos@#@";
        js_args += sOpcion + "@#@";
        if (sOpcion == "L") js_args += sValor;
        else js_args += dfn(sValor);
        //alert(js_args); ocultarProcesando(); return;
        js_args += "@#@";
        if (sOpcion == "L") js_args += sParesDatos;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de proyectos", e.message);
    }
}

function getReplicas(oFila) {
    try {
        var js_args = "getreplicas@#@";
        js_args += oFila.getAttribute("idProy");

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las réplicas.", e.message);
    }
}

function getNodoOrigen() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNodoOrigen").value = aDatos[0];
                    $I("txtCROrigen").value = aDatos[1];
                    $I("hdnDesNodoOrigen").value = aDatos[1];
                    mostrarRelacionProyectos("N", aDatos[0]);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el nodo origen.", e.message);
    }
}

function getCliente() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    mostrarRelacionProyectos("C", aDatos[0]);
                }
            });

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function getResponsable() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto", self, sSize(550, 540))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtDesResponsable").value = aDatos[1];
                    mostrarRelacionProyectos("R", aDatos[0]);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}


function getNodoDestino() {
    try {
        mostrarProcesando();
        var sTitle = "";
        var sTootTip = "";
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNodoDestino").value = aDatos[0];
                    $I("txtCRDestino").value = aDatos[1];

                    sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
                    sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

                    var aFila = FilasDe("tblDatos2");
                    for (var i = 0; i < aFila.length; i++) {
                        if (aFila[i].className == "FS") {
                            aFila[i].className = "";
                            aFila[i].setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);
                            aFila[i].cells[4].innerHTML = "";
                            var oNOBR = document.createElement("NOBR");
                            oNOBR.className = "NBR W180";
                            aFila[i].cells[4].appendChild(oNOBR);
                            aFila[i].cells[4].children[0].style.width = "180px";
                            aFila[i].cells[4].children[0].innerText = $I("txtCRDestino").value;//$I("txtCRDestino").value;
                            aFila[i].cells[4].children[0].title = sTootTip; //span

                            aFila[i].cells[5].innerHTML = "";
                        }
                    }
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el nodo destino.", e.message);
    }
}
/*
var oImgProducto = document.createElement("img");
oImgProducto.setAttribute("src", "../../../../images/imgProducto.gif");
oImgProducto.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
var oImgServicio = document.createElement("img");
oImgServicio.setAttribute("src", "../../../../images/imgServicio.gif");
oImgServicio.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgContratante = document.createElement("img");
oImgContratante.setAttribute("src", "../../../../images/imgIconoContratante.gif");
oImgContratante.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
var oImgRepJor = document.createElement("img");
oImgRepJor.setAttribute("src", "../../../../images/imgIconoRepJor.gif");
oImgRepJor.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
var oImgRepPrecio = document.createElement("img");
oImgRepPrecio.setAttribute("src", "../../../../images/imgIconoRepPrecio.gif");
oImgRepPrecio.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgAbierto = document.createElement("img");
oImgAbierto.setAttribute("src", "../../../../images/imgIconoProyAbierto.gif");
oImgAbierto.setAttribute("title", "Proyecto abierto");
oImgAbierto.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
var oImgCerrado = document.createElement("img");
oImgCerrado.setAttribute("src", "../../../../images/imgIconoProyCerrado.gif");
oImgCerrado.setAttribute("title", "Proyecto cerrado");
oImgCerrado.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
var oImgHistorico = document.createElement("img");
oImgHistorico.setAttribute("src", "../../../../images/imgIconoProyHistorico.gif");
oImgHistorico.setAttribute("title", "Proyecto histórico");
oImgHistorico.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
var oImgPresup = document.createElement("img");
oImgPresup.setAttribute("src", "../../../../images/imgIconoProyPresup.gif");
oImgPresup.setAttribute("title", "Proyecto presupuestado");
oImgPresup.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
*/
var oImgTrasOK = document.createElement("img");
oImgTrasOK.setAttribute("src", "../../../../images/imgTrasladoOK.gif");
oImgTrasOK.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTrasKO = document.createElement("img");
oImgTrasKO.setAttribute("src", "../../../../images/imgTrasladoKO.gif");
oImgTrasKO.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(), null); break;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollProyRep = 0;
var nIDTimeProyRep = 0;
function scrollTablaProyRep() {
    try {
        if ($I("divCatalogoRep").scrollTop != nTopScrollProyRep) {
            nTopScrollProyRep = $I("divCatalogoRep").scrollTop;
            clearTimeout(nIDTimeProyRep);
            nIDTimeProyRep = setTimeout("scrollTablaProyRep()", 50);
            return;
        }

        var tblDatosRep = $I("tblDatosRep");
        var nFilaVisible = Math.floor(nTopScrollProyRep / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoRep").offsetHeight / 20 + 1, tblDatosRep.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatosRep.rows[i].getAttribute("sw")) {
                oFila = tblDatosRep.rows[i];
                oFila.setAttribute("sw", 1);
                
                switch (oFila.getAttribute("cualidad")) {
                    case "J": oFila.cells[0].appendChild(oImgRepJor.cloneNode(), null); break;
                    case "P": oFila.cells[0].appendChild(oImgRepPrecio.cloneNode(), null); break;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollProyDest = 0;
var nIDTimeProyDest = 0;
function scrollTablaProyDest() {
    try {
        if ($I("divCatalogo2").scrollTop != nTopScrollProyDest) {
            nTopScrollProyDest = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProyDest);
            nIDTimeProyDest = setTimeout("scrollTablaProyDest()", 50);
            return;
        }
        
        var tblDatos2 = $I("tblDatos2");        
        var nFilaVisible = Math.floor(nTopScrollProyDest / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, tblDatos2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos2.rows[i].getAttribute("sw")) {
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(), null); break;
                }

                if (oFila.getAttribute("procesado") == "1") oFila.cells[5].appendChild(oImgTrasOK.cloneNode(), null);
                else if (oFila.getAttribute("procesado") == "0") oFila.cells[5].appendChild(oImgTrasKO.cloneNode(), null);
                if (typeof (oFila.getAttribute("excepcion")) != "undefined" && oFila.getAttribute("excepcion") != "") oFila.cells[5].children[0].title = Utilidades.unescape(oFila.getAttribute("excepcion"));
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos asignados.", e.message);
    }
}


function insertarProyecto(oFila) {
    try {
        //var idPSN = oFila.idPSN;
        var bExiste = false;
        //1º buscar si existe en el array de recursos y su "opcionBD"
        var aFila = FilasDe("tblDatos2");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("idPSN") == oFila.getAttribute("idPSN")) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            //alert("El profesional indicado ya se encuentra asignado a la tarea");
            return;
        }

        var oNF = $I("tblDatos2").insertRow(-1);
        oNF.style.height = "20px";

        oNF.setAttribute("idPSN", oFila.getAttribute("idPSN"));
        oNF.setAttribute("sw", oFila.getAttribute("sw"));
        oNF.setAttribute("categoria", oFila.getAttribute("categoria"));
        oNF.setAttribute("cualidad", oFila.getAttribute("cualidad"));
        oNF.setAttribute("estado", oFila.getAttribute("estado"));
        oNF.setAttribute("procesado", "");
        oNF.setAttribute("codigo_excepcion", "")        
        oNF.setAttribute("nodo_origen", oFila.getAttribute("nodo_origen"));
        oNF.setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[1].children[0].cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[2].children[0].cloneNode(true));

        var oNC4 = oNF.insertCell(-1);
        oNC4.appendChild(oFila.cells[3].children[0].cloneNode(true));
        oNC4.children[0].className = "NBR W280";
        /*
        var oNC5 = oNF.insertCell(-1);
        oNC5.appendChild(oFila.cells[4].children[0].cloneNode(true));
        oNC5.children[0].className = "NBR W180";
        oNC5.children[0].innerText = $I("txtCRDestino").value;

        var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        sTitle = oNC5.children[0].title;
        if (sTitle != "") {
            oNC5.children[0].title = sTootTip; //span
        } else {
            oNC5.children[0].boBDY = sTootTip; //span
        }
        */
        oNC5 = oNF.insertCell(-1);
        var oNOBR = document.createElement("NOBR");
        oNOBR.className = "NBR W180";
        oNC5.appendChild(oNOBR);
        //oNC5.appendChild(oRow.cells[2].children[0].cloneNode(false));
        oNC5.children[0].style.width = "180px";
        oNC5.children[0].innerText = $I("txtCRDestino").value;//$I("txtCRDestino").value;
        var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

        oNC5.children[0].title = sTootTip; //span

        oNF.insertCell(-1);
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el proyecto.", e.message);
    }
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

    var obj = $I("DW");
    var oTable = null;
    var oNF = null;
    var oNC1 = null;
    var oNC2 = null;
    var oNC3 = null;
    var oNC4 = null;
    var oNC5 = null;
    var oNC6 = null;
    var aProyAsig = new Array();
    var nND = $I("hdnIdNodoDestino").value;
    var sND = $I("txtCRDestino").value;

    var nIndiceInsert = null;
    
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }		

        var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        //oNC3.children[0].title = sTootTip; //span
        var sTitle = "";

        switch (oTarget.id) {
            case "divCatalogo2":
            case "ctl00_CPHC_divCatalogo2":	
                oTable = oTarget.getElementsByTagName("table")[0];
                for (var i = 0; i < oTable.rows.length; i++) {
                    aProyAsig[aProyAsig.length] = oTable.rows[i].getAttribute("idPSN");
                }
                break;
        }

        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":	
                    if (FromTable == null || ToTable == null) continue;
                    if (nOpcionDD == 1) {
                        var sw = 0;
                        for (var i = 0; i < aProyAsig.length; i++) {
                            if (aProyAsig[i] == oRow.getAttribute("idPSN")) {
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 0) {
                            oNF = oTable.insertRow(-1);
                            oNF.style.height = "20px";
                            oNF.setAttribute("idPSN", oRow.getAttribute("idPSN"));

                            oNF.setAttribute("sw", oRow.getAttribute("sw"));
                            oNF.setAttribute("categoria", oRow.getAttribute("categoria"));
                            oNF.setAttribute("cualidad", oRow.getAttribute("cualidad"));
                            oNF.setAttribute("estado", oRow.getAttribute("estado"));
                            oNF.setAttribute("procesado", "");
                            oNF.setAttribute("codigo_excepcion", "")
                            oNF.setAttribute("nodo_origen", oRow.getAttribute("nodo_origen"));
                            oNF.setAttribute("nodo_destino", nND);

                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);                            
                            
                            oNC1 = oNF.insertCell(-1);
                            if (oRow.cells[0].children.length > 0)
                                oNC1.appendChild(oRow.cells[0].children[0].cloneNode(true));
                            oNC2 = oNF.insertCell(-1);
                            if (oRow.cells[1].children.length > 0)
                                oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true));
                            oNC3 = oNF.insertCell(-1);
                            if (oRow.cells[2].children.length > 0)
                                oNC3.appendChild(oRow.cells[2].children[0].cloneNode(true));

                            oNC4 = oNF.insertCell(-1);
                            oNC4.appendChild(oRow.cells[3].children[0].cloneNode(true));
                            oNC4.children[0].style.width = "280px";

                           /* oNC5 = oNF.insertCell(-1);
                            oNC5.appendChild(oRow.cells[4].children[0].cloneNode(true));
                            oNC5.children[0].style.width = "180px";
                            oNC5.children[0].innerText = sND; //$I("txtCRDestino").value;
                            sTitle = oNC5.children[0].title;
                            if (sTitle != "") {
                                oNC5.children[0].title = sTootTip; //span
                            } else {
                                oNC5.children[0].boBDY = sTootTip; //span
                            }
*/
                            oNC5 = oNF.insertCell(-1);
                            var oNOBR = document.createElement("NOBR");
                            oNOBR.className = "NBR W180";
                            oNC5.appendChild(oNOBR);
                            //oNC3.appendChild(oRow.cells[2].children[0].cloneNode(false));
                            oNC5.children[0].style.width = "180px";
                            oNC5.children[0].innerText = sND;//$I("txtCRDestino").value;
                            oNC5.children[0].title = sTootTip; //span

                            oNF.insertCell(-1);
                        }
                    }
                    break;
            }
        }

        switch (oTarget.id) {
            case "divCatalogo2":
            case "ctl00_CPHC_divCatalogo2":	
                scrollTablaProyDest();
                break;
        }
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
    FromTable = null;
    ToTable = null;
}

function borrarCatalogo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
        $I("divCatalogoRep").children[0].innerHTML = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function aparcar() {
    try {
        var sb = new StringBuilder;
        sb.Append("aparcar@#@");

        var aFila = FilasDe("tblDatos2");
        if (aFila.length == 0) return;

        mostrarProcesando();
        for (var i = 0; i < aFila.length; i++) {
            sb.Append(aFila[i].getAttribute("idPSN") + "##"); //0
            sb.Append(aFila[i].getAttribute("nodo_destino") + "##"); //1

            if (aFila[i].cells[5].innerHTML == "") sb.Append(""); //2
            else if (aFila[i].cells[5].innerHTML.indexOf("imgTrasladoOK.gif") != -1) sb.Append("1"); //2
            else sb.Append("0"); //2
            sb.Append("///");
        }

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a aparcar la situación destino.", e.message);
    }
}
function aparcardel() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("aparcardel@#@");

        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a eliminar la situación aparcada.", e.message);
    }
}
function recuperar() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("recuperar@#@");

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar la situación destino.", e.message);
    }
}

function procesar(bPorDeadLockTimeout) {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        var bCorrecto = true;
        sb.Append("procesar@#@");
        sb.Append(((bPorDeadLockTimeout) ? "1" : "0") + "@#@");

        var aFila = FilasDe("tblDatos2");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("nodo_destino") == "") {
                bCorrecto = false;
                //alert("aFila[i].nodo_destino: "+ aFila[i].nodo_destino);
                ocultarProcesando();
                mmoff("War", "Debes seleccionar el " + strEstructuraNodo + " destino de todos los proyectos.", 380);
                return;
            }
            sb.Append(aFila[i].getAttribute("idPSN") + "##"); //0
            sb.Append(aFila[i].getAttribute("nodo_origen") + "##"); //1
            sb.Append(aFila[i].getAttribute("nodo_destino") + "##"); //2

            if (aFila[i].cells[5].innerHTML == "") sb.Append(""); //3
            else if (aFila[i].cells[5].innerHTML.indexOf("imgTrasladoOK.gif") != -1) sb.Append("1"); //3
            else sb.Append("0"); //3
            sb.Append("##");
            sb.Append(aFila[i].getAttribute("codigo_excepcion") + "///"); //4
        }
        sb.Append("@#@");
        sb.Append(($I("chkMantResp").checked) ? "1" : "0");
        //alert(sb.ToString());return;
        if (bCorrecto) RealizarCallBack(sb.ToString(), "");

        $I("divCatalogo").children[0].innerHTML = "";
        $I("divCatalogoRep").children[0].innerHTML = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}

function replicasmeses() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("replicasmeses@#@");

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a generar las réplicas y los meses.", e.message);
    }
}

function getLista() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/CambioEstructura/Importar.aspx", self, sSize(400, 400))
            .then(function(ret) {
                if (ret != null) {
                    var sb = new StringBuilder;
                    var sb2 = new StringBuilder;
                    var aLineas = ret.split(getSaltoLinea());
                    var bPrimero = true;
                    var bElementoNoNumerico = false;
                    for (var i = 0; i < aLineas.length; i++) {
                        if (aLineas[i] == "") continue;
                        var aParDatos = aLineas[i].split(";");
                        aParDatos[0] = fTrim(aParDatos[0]);
                        if (aParDatos.length == 2)
                            aParDatos[1] = fTrim(aParDatos[1]);


                        if (isNaN(aParDatos[0])) {
                            bElementoNoNumerico = true;
                            continue;
                        }
                        if (aParDatos.length == 2) {
                            if (isNaN(aParDatos[1])) {
                                bElementoNoNumerico = true;
                                continue;
                            }
                        }

                        sb2.Append(aParDatos[0] + "/" + ((aParDatos.length == 2) ? aParDatos[1] : "") + ";");

                        if (!bPrimero) sb.Append("," + dfn(aParDatos[0]));
                        else {
                            bPrimero = false;
                            sb.Append(dfn(aParDatos[0]));
                        }
                    }
                    if (sb.ToString().length > 8000 || sb2.ToString().length > 8000) {
                        mmoff("Inf", "La longitud máxima de la lista no debe sobrepasar los 7000 caracteres.", 450, 3000);
                    } else {
                        //alert(sb2.ToString());
                        mostrarRelacionProyectos("L", sb.ToString(), sb2.ToString());
                        if (bElementoNoNumerico) {
                            mmoff("Inf", "Se ha detectado que hay elementos de la lista que no tienen formato numérico.\n\nDichos elementos han sido obviados el la búsqueda de resultados.",380);
                        }
                    }
                } 
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a generar las réplicas y los meses.", e.message);
    }
}