﻿var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

var sListaContratos = "";
var oImgContratante = document.createElement("img");
oImgContratante.setAttribute("src", "../../../../images/imgIconoContratante.gif");
oImgContratante.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTrasOK = document.createElement("img");
oImgTrasOK.setAttribute("src", "../../../../images/imgTrasladoOK.gif");
oImgTrasOK.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTrasKO = document.createElement("img");
oImgTrasKO.setAttribute("src", "../../../../images/imgTrasladoKO.gif");
oImgTrasKO.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgSubnodo = document.createElement("img");
oImgSubnodo.setAttribute("src", "../../../../images/imgSubNodo.gif");
oImgSubnodo.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgGestor = document.createElement("img");
oImgGestor.setAttribute("src", "../../../../images/imgResponsable.gif");
oImgGestor.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgCliente = document.createElement("img");
oImgCliente.setAttribute("src", "../../../../images/imgCliente16.gif");
oImgCliente.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

function init() {
    try {
        $I("txtNumero").focus();
        AccionBotonera("procesar", "H");
    }
    catch (e) {
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
            case "contratos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogoRep").children[0].innerHTML = "";
                $I("divCatalogoRep").scrollTop = 0;
                var tblDatos = $I("tblDatos");
                var tblDatos2 = $I("tblDatos2");

                if (aResul[3] == "") {
                    if (tblDatos2) {
                        for (var i = tblDatos2.rows.length - 1; i >= 0; i--) {
                            tblDatos2.deleteRow(tblDatos2.rows[i].rowIndex);
                        }
                    }
                }
                else {
                    var tblDestino = $I("tblDatos2");
                    
                    //quitar los duplicados 
                    for (var i = tblDatos2.rows.length - 1; i >= 0; i--) {
                        for (var x = 0; x < tblDestino.rows.length; x++) {
                            if (tblDatos2.rows[i].id == tblDestino.rows[x].id) {
                                tblDatos2.deleteRow(tblDatos2.rows[i].rowIndex);
                                break;
                            }
                            //else if (parseInt(tblDatos2.rows[i].id, 10) < parseInt(tblDestino.rows[x].id, 10)) {
                            //    break;
                            //}
                        }
                    }

                    insertarFilasEnTablaDOM("tblDatos2", aResul[3], tblDatos2.rows.length);

                    //poner datos de arrastre
                    for (var i = tblDatos2.rows.length - 1; i >= 0; i--) {
                        //CR
                        if (tblDatos2.rows[i].getAttribute("nodo_destino") == "" && $I("hdnIdNodoDestino").value != "") {
                            tblDatos2.rows[i].setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);
                            tblDatos2.rows[i].cells[2].children[0].innerText = $I("txtCRDestino").value;
                        }
                        //Responsable
                        if ($I("hdnIdResponsableDestino").value != "") {
                            tblDatos2.rows[i].setAttribute("resp_destino", $I("hdnIdResponsableDestino").value);
                            tblDatos2.rows[i].setAttribute("nom_responsable", $I("txtDesResponsableDestino").value);
                        }
                        //Gestor de producción
                        if ($I("hdnIdResponsableDestino").value != "") {
                            tblDatos2.rows[i].setAttribute("gest_destino", $I("hdnIdGestorDestino").value);
                            tblDatos2.rows[i].cells[4].children[0].innerText = $I("txtDesGestorDestino").value;
                        }
                        if ($I("chkArrastrarGestor").checked)
                            tblDatos2.rows[i].cells[3].appendChild(oImgGestor.cloneNode(true));

                        //Cliente
                        if ($I("hdnIdClienteDestino").value != "") {
                            tblDatos2.rows[i].setAttribute("clie_destino", $I("hdnIdClienteDestino").value);
                            tblDatos2.rows[i].cells[6].children[0].innerText = $I("txtDesClienteDestino").value;
                        }
                        if ($I("chkArrastrarCliente").checked)
                            tblDatos2.rows[i].cells[5].appendChild(oImgCliente.cloneNode(true));

                        //Comercial
                        if ($I("hdnIdComercialDestino").value != "") {
                            tblDatos2.rows[i].setAttribute("come_destino", $I("hdnIdComercialDestino").value);
                            tblDatos2.rows[i].cells[7].children[0].innerText = $I("txtDesComercialDestino").value;
                        }

                        ponerToolTip(tblDatos2.rows[i]);
                        break;
                    }
                    //else if (parseInt(tblDatos2.rows[i].id, 10) < parseInt(tblDatos.rows[x].id, 10)) {
                    //    break;
                    //}
                }
                ocultarProcesando();
                break;
            case "procesar":
                //$I("divCatalogo2").children[0].innerHTML = aResul[2];
                //$I("divCatalogo2").scrollTop = 0;
                if (aResul[3] == "1" && nIntentosProcesoDeadLock < nLimiteIntentosProcesoDeadLock) {//Error de deadlock o timeout
                    bOcultar = false;
                    nIntentosProcesoDeadLock++;
                    mmoff("Inf", "Existen varios procesos ejecutándose simultáneamente. Disculpa la espera.", 500, 5000);
                    setTimeout("procesar(true);", nSetTimeoutProcesoDeadLock);
                }
                else {
                    nIntentosProcesoDeadLock = 0;
                }
                scrollTablaContDest(aResul[2]);
                ocultarProcesando();
                mmoff("Inf", "Proceso finalizado.", 200, 2000);
                break;
            case "getproyectos":
                $I("divCatalogoRep").children[0].innerHTML = aResul[2];
                $I("divCatalogoRep").scrollTop = 0;
                scrollTablaProyRep();
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultar)
            setTimeout("ocultarProcesando", 50);
    }
}

function mostrarRelacionContratosLista() {
    try {
        var js_args = "contratos@#@0@#@";
        js_args += "@#@";//$I("hdnIdNodoOrigen").value + 
        js_args += "@#@";//$I("hdnIdResponsable").value + 
        js_args += "@#@";//$I("hdnIdGestor").value + 
        js_args += "@#@";//$I("hdnIdCliente").value + 
        js_args += "@#@";//$I("hdnIdComercial").value + 
        js_args += sListaContratos;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de contratos (mostrarRelacionContratosLista)", e.message);
    }
}
function buscar() {
    //var sNumContrato = "";
    try {
        if ($I("txtNumero").value == "" && $I("hdnIdNodoOrigen").value == "" && $I("hdnIdResponsable").value == ""
            && $I("hdnIdGestor").value == "" && $I("hdnIdCliente").value == "" && $I("hdnIdComercial").value == "") {
            //&& sListaContratos == "") {
            mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
            return;
        }
        if ($I("txtNumero").value != "") {
            if (isNaN(dfn($I("txtNumero").value))) {
                mmoff("War", "El valor introducido no es numérico", 250);
                $I("txtNumero").value = "";
                $I("txtNumero").focus();
                return;
            }
            //else {
            //    sNumContrato = $I("txtNumero").value.ToString("N", 9, 0);
            //}
        }
        var js_args = "contratos@#@";
        if ($I("txtNumero").value == "") js_args += "@#@";
        else js_args += dfn($I("txtNumero").value) + "@#@";
        js_args += $I("hdnIdNodoOrigen").value + "@#@";
        js_args += $I("hdnIdResponsable").value + "@#@";
        js_args += $I("hdnIdGestor").value + "@#@";
        js_args += $I("hdnIdCliente").value + "@#@";
        js_args += $I("hdnIdComercial").value + "@#@";
        //js_args += sListaContratos;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de contratos (buscar)", e.message);
    }
}
function getLista() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/CambioEstructura/Importar.aspx?nodo=N", self, sSize(400, 400))
            .then(function (ret) {
                if (ret != null) {
                    var sb = new StringBuilder;
                    var aLineas = ret.split(getSaltoLinea());
                    var bPrimero = true;
                    var bElementoNoNumerico = false;
                    for (var i = 0; i < aLineas.length; i++) {
                        if (aLineas[i] == "") continue;
                        if (isNaN(aLineas[i])) {
                            bElementoNoNumerico = true;
                            continue;
                        }

                        if (!bPrimero) sb.Append(";" + dfn(aLineas[i]));
                        else {
                            bPrimero = false;
                            sb.Append(dfn(aLineas[i]));
                        }
                    }
                    if (sb.ToString().length > 8000 ) {
                        mmoff("Inf", "La longitud máxima de la lista no debe sobrepasar los 7000 caracteres.", 450, 3000);
                    }
                    else {
                        sListaContratos = sb.ToString();
                        mostrarRelacionContratosLista();
                        if (bElementoNoNumerico) {
                            mmoff("Inf", "Se ha detectado que hay elementos de la lista que no tienen formato numérico.\n\nDichos elementos han sido obviados el la búsqueda de resultados.", 360);
                        }
                    }
                }
            });

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a generar las réplicas y los meses.", e.message);
    }
}
function getProyectos(oFila) {
    try {
        var js_args = "getproyectos@#@" + oFila.id;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos del contrato.", e.message);
    }
}

function getNodoOrigen() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNodoOrigen").value = aDatos[0];
                    $I("txtCROrigen").value = aDatos[1];
                    $I("hdnDesNodoOrigen").value = aDatos[1];
                    //mostrarRelacionContratos("N", aDatos[0]);
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
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0", self, sSize(600, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    //mostrarRelacionContratos("C", aDatos[0]);
                }
            });

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function getClienteDestino() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0", self, sSize(600, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdClienteDestino").value = aDatos[0];
                    $I("txtDesClienteDestino").value = aDatos[1];
                    ponerDestino("cliente");
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
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=contrato", self, sSize(550, 540))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtDesResponsable").value = aDatos[1];
                    //mostrarRelacionContratos("R", aDatos[0]);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}
function getResponsableDestino() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsableDestino").value = aDatos[0];
                    $I("txtDesResponsableDestino").value = aDatos[1];
                    ponerDestino("responsable");
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function getGestor() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdGestor").value = aDatos[0];
                    $I("txtDesGestor").value = aDatos[1];
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los gestores de producción", e.message);
    }
}
function getGestorDestino() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdGestorDestino").value = aDatos[0];
                    $I("txtDesGestorDestino").value = aDatos[1];
                    ponerDestino("gestor");
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los gestores de producción", e.message);
    }
}

function getComercial() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getComercial.aspx", self, sSize(530, 540))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdComercial").value = aDatos[0];
                    $I("txtDesComercial").value = aDatos[1];
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los comerciales", e.message);
    }
}
function getComercialDestino() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getComercial.aspx", self, sSize(530, 540))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdComercialDestino").value = aDatos[0];
                    $I("txtDesComercialDestino").value = aDatos[1];
                    ponerDestino("comercial");
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los comerciales", e.message);
    }
}

function delOrigen() {
    $I("txtNumero").value = "";
    $I("hdnIdNodoOrigen").value = "";
    $I("hdnDesNodoOrigen").value = "";
    $I("txtCROrigen").value = "";
    $I("hdnIdResponsable").value = "";
    $I("txtDesResponsable").value = "";
    $I("hdnIdGestor").value = "";
    $I("txtDesGestor").value = "";
    $I("hdnIdCliente").value = "";
    $I("txtDesCliente").value = "";
    $I("hdnIdComercial").value = "";
    $I("txtDesComercial").value = "";

    borrarCatalogo();
}
function delDestino() {
    $I("hdnIdNodoDestino").value = "";
    $I("hdnDesNodoDestino").value = "";
    $I("txtCRDestino").value = "";
    $I("hdnIdResponsableDestino").value = "";
    $I("txtDesResponsableDestino").value = "";
    $I("hdnIdGestorDestino").value = "";
    $I("txtDesGestorDestino").value = "";
    $I("hdnIdClienteDestino").value = "";
    $I("txtDesClienteDestino").value = "";
    $I("hdnIdComercialDestino").value = "";
    $I("txtDesComercialDestino").value = "";

    $I("chkArrastrarPropio").checked = false;
    $I("chkArrastrarTodos").checked = false;
    $I("chkActualizarGestor").checked = false;
    $I("chkArrastrarGestor").checked = false;
    $I("chkArrastrarCliente").checked = false;
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

                oFila.cells[0].appendChild(oImgContratante.cloneNode(true), null);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollContDest = 0;
var nIDTimeContDest = 0;
function scrollTablaContDest(lstContratos) {
    try {
        if ($I("divCatalogo2").scrollTop != nTopScrollContDest) {
            nTopScrollContDest = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeContDest);
            nIDTimeContDest = setTimeout("scrollTablaContDest()", 50);
            return;
        }
        //Recojo la lista de idContrato#procesado
        var aContratos = lstContratos.split(",");
        var sProcesado = "0";

        var tblDatos2 = $I("tblDatos2");
        var nFilaVisible = Math.floor(nTopScrollContDest / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, tblDatos2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos2.rows[i].getAttribute("sw") || tblDatos2.rows[i].getAttribute("sw")=="null") {
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);
                //GESTOR
                if (oFila.getAttribute("arrastra_gestor") == "N")
                    oFila.cells[3].appendChild(oImgSubnodo.cloneNode(true), null);
                else if (oFila.getAttribute("arrastra_gestor") == "G")
                    oFila.cells[3].appendChild(oImgGestor.cloneNode(true), null);
                //Cliente
                if (oFila.getAttribute("arrastra_cliente") == "S")
                    oFila.cells[5].appendChild(oImgCliente.cloneNode(true), null);
                //Compruebo si se ha procesado
                sProcesado = contratoProcesado(oFila.getAttribute("id"), aContratos, oFila);
                oFila.setAttribute("procesado", sProcesado);
                if (oFila.getAttribute("procesado") == "1")
                    oFila.cells[8].appendChild(oImgTrasOK.cloneNode(true), null);
                else{
                    oFila.cells[8].appendChild(oImgTrasKO.cloneNode(true), null);
                    if (oFila.getAttribute("excepcion") == null) {
                        oFila.setAttribute("excepcion", "Los datos origen y destino son iguales");
                        oFila.cells[8].title = Utilidades.unescape(oFila.getAttribute("excepcion"));
                    }
                    else {
                        if (typeof (oFila.getAttribute("excepcion")) != "undefined" && oFila.getAttribute("excepcion") != "")
                            oFila.cells[8].title = Utilidades.unescape(oFila.getAttribute("excepcion"));
                    }
                }
                ponerToolTip(oFila);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de contratos asignados.", e.message);
    }
}
function contratoProcesado(idContrato, aContratos, oFila) {
    var sProcesado = "0";
    var aCont="";
    try {
        for (var i = 0; i < aContratos.length; i++) {
            if (aContratos[i] != "") {
                aCont = aContratos[i].split("#");
                if (aCont[0] == idContrato) {
                    sProcesado = aCont[1];
                    if (sProcesado == "0")
                        oFila.setAttribute("excepcion", aCont[2]);
                    else
                        oFila.setAttribute("excepcion", "");
                    break;
                }
            }
        }
        return sProcesado;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar si el contrato se ha procesado.", e.message);
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
    var aContAsig = new Array();
    var nND = $I("hdnIdNodoDestino").value;
    //var sND = $I("txtCRDestino").value;
    var sTitle = "";
    var sTootTip = "";

    var nIndiceInsert = null;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }

        //sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + sND;
        //sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        //oNC3.children[0].title = sTootTip; //span
        var sTitle = "", sResponsable = "", sCR = "", sGestor = "";

        switch (oTarget.id) {
            case "divCatalogo2":
            case "ctl00_CPHC_divCatalogo2":
                oTable = oTarget.getElementsByTagName("TABLE")[0];
                for (var i = 0; i < oTable.rows.length; i++) {
                    aContAsig[aContAsig.length] = oTable.rows[i].id;
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
                        for (var i = 0; i < aContAsig.length; i++) {
                            if (aContAsig[i] == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 0) {
                            oNF = oTable.insertRow(-1);
                            oNF.style.height = "20px";
                            oNF.id = oRow.id;

                            oNF.setAttribute("sw", oRow.getAttribute("sw"));
                            oNF.setAttribute("procesado", "");
                            oNF.setAttribute("codigo_excepcion", "")
                            oNF.setAttribute("nodo_origen", oRow.getAttribute("nodo_origen"));
                            oNF.setAttribute("cliente_origen", oRow.getAttribute("cliente_origen"));//cliente
                            oNF.setAttribute("responsable_origen", oRow.getAttribute("responsable_origen"));//responsable
                            oNF.setAttribute("gestor_origen", oRow.getAttribute("gestor_origen"));//gestor
                            oNF.setAttribute("comercial_origen", oRow.getAttribute("comercial_origen"));//comercial

                            if ($I("hdnIdResponsableDestino").value == "") {
                                oNF.setAttribute("resp_destino", oRow.getAttribute("responsable_origen"));//responsable
                                oNF.setAttribute("nom_responsable", oRow.getAttribute("nom_responsable"));//nombre responsable
                                sResponsable = oRow.getAttribute("responsable_origen") + "-" + oRow.getAttribute("nom_responsable");
                            }
                            else {
                                oNF.setAttribute("resp_destino", $I("hdnIdResponsableDestino").value);//responsable
                                oNF.setAttribute("nom_responsable", $I("txtDesResponsable").value);//responsable
                                sResponsable = $I("hdnIdResponsableDestino").value + " - " + $I("txtDesResponsable").value;
                            }
                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);
                            //Contrato
                            oNC1 = oNF.insertCell(-1);
                            //oNC1.appendChild(oRow.cells[0].children[0].cloneNode(true));
                            oNC1.appendChild(oNobr.cloneNode(true), null);
                            oNC1.children[0].className = "NBR";
                            oNC1.children[0].setAttribute("style", "width:160px;");
                            //oNC1.children[0].attachEvent("onmouseover", TTip);
                            oNC1.children[0].innerText = oRow.cells[0].children[0].innerText;

                            //Arrastre nodo
                            oNC2 = oNF.insertCell(-1);
                            oNC2.style.width = "20px";
                            //Nodo
                            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
                            oNF.cells[2].children[0].className = "NBR";
                            oNF.cells[2].children[0].setAttribute("style", "width:80px;");
                            //oNF.cells[2].children[0].attachEvent("onmouseover", TTip);

                            if ($I("hdnIdNodoDestino").value == "") {
                                oNF.setAttribute("nodo_destino", oRow.getAttribute("nodo_origen"));
                                oNF.cells[2].children[0].innerText = oRow.cells[1].children[0].innerText;
                                sCR = oRow.getAttribute("nodo_origen") + "-" + oRow.cells[1].children[0].innerText;
                            }
                            else {
                                oNF.setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);
                                //oNF.cells[2].children[0].innerText = $I("txtCRDestino").value;
                                //sCR = $I("hdnIdNodoDestino").value + " - " + $I("txtCRDestino").value;
                            }

                            //sTitle = oNC3.children[0].title;
                            //if (sTitle != ""){
                            //    oNC3.children[0].title = sTootTip; //span
                            //}else{
                            //    oNC3.children[0].boBDY = sTootTip; //span
                            //}

                            //Arrastrar Gestor
                            var oNC3 = oNF.insertCell(-1);
                            oNC3.style.width = "20px";
                            if ($I("chkArrastrarGestor").checked) oNC3.appendChild(oImgGestor.cloneNode(true));

                            //Gestor
                            var oNC4 = oNF.insertCell(-1);
                            oNC4.appendChild(oNobr.cloneNode(true), null);
                            oNC4.children[0].className = "NBR";
                            oNC4.children[0].setAttribute("style", "width:80px;");
                            //oNC4.children[0].attachEvent("onmouseover", TTip);
                            if ($I("hdnIdGestorDestino").value == "") {
                                oNF.setAttribute("gest_destino", oRow.getAttribute("gestor_origen"));//gestor de producción
                                oNC4.children[0].innerText = oRow.cells[2].children[0].innerText;
                                sGestor = oRow.getAttribute("gestor_origen") + "-" + oRow.cells[2].children[0].innerText;
                            }
                            else {
                                oNF.setAttribute("gest_destino", $I("hdnIdGestorDestino").value);//gestor de producción
                                oNC4.children[0].innerText = $I("txtDesGestorDestino").value;
                                sGestor = $I("hdnIdGestorDestino").value + " - " + $I("txtDesGestorDestino").value;
                            }
                            //Arrastrar cliente
                            var oNC5 = oNF.insertCell(-1);
                            if ($I("chkArrastrarCliente").checked) oNC5.appendChild(oImgCliente.cloneNode(true));
                            //Cliente
                            var oNC6 = oNF.insertCell(-1);
                            oNC6.appendChild(oNobr.cloneNode(true), null);
                            oNC6.children[0].className = "NBR";
                            oNC6.children[0].setAttribute("style", "width:80px;");
                            //oNC6.children[0].attachEvent("onmouseover", TTip);
                            //oNC6.children[0].innerText = $I("txtDesClienteDestino").value;
                            if ($I("hdnIdClienteDestino").value == "") {
                                oNF.setAttribute("clie_destino", oRow.getAttribute("cliente_origen"));//cliente
                                oNC6.children[0].innerText = oRow.cells[3].children[0].innerText;
                            }
                            else {
                                oNF.setAttribute("clie_destino", $I("hdnIdClienteDestino").value);//cliente
                                oNC6.children[0].innerText = $I("txtDesClienteDestino").value;
                            }

                            //Comercial
                            var oNC7 = oNF.insertCell(-1);
                            oNC7.appendChild(oNobr.cloneNode(true), null);
                            oNC7.children[0].className = "NBR";
                            oNC7.children[0].setAttribute("style", "width:80px;");
                            //oNC7.children[0].attachEvent("onmouseover", TTip);
                            //oNC7.children[0].innerText = $I("txtDesComercialDestino").value;
                            if ($I("hdnIdComercialDestino").value == "") {
                                oNF.setAttribute("come_destino", oRow.getAttribute("comercial_origen"));//comercial Hermes
                                oNC7.children[0].innerText = oRow.cells[4].children[0].innerText;
                            }
                            else {
                                oNF.setAttribute("come_destino", $I("hdnIdComercialDestino").value);//comercial Hermes
                                oNC7.children[0].innerText = $I("txtDesComercialDestino").value;
                            }
                            //Resultado
                            oNF.insertCell(-1);

                            //Tooltip extendido
                            //sTitle = "<label style='width:70px;'>Contrato:</label>" + oNF.cells[0].children[0].innerHTML + "<br>";
                            //sTitle += "<label style='width:70px;'>Responsable:</label>" + sResponsable + "<br>";
                            //sTitle += "<label style='width:70px;'>" + strEstructuraNodo + ":</label>" + sCR + "<br>";
                            //sTitle += "<label style='width:70px;'>Gestor Prod.:</label>" + sGestor + "<br>";
                            //sTitle += "<label style='width:70px;'>Cliente:</label>" + oNF.cells[6].children[0].innerHTML + "<br>";
                            //sTitle += "<label style='width:70px;'>Comercial:</label>" + oNF.cells[7].children[0].innerHTML + "<br>";
                            //sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
                            //if (oNC1.children[0].title=="") {
                            //    oNC1.children[0].title = sTootTip; //span
                            //}
                            //else {
                            //    oNC1.children[0].boBDY = sTootTip; //span
                            //}
                            ponerToolTip(oNF);
                        }
                    }
                    break;
            }
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


function procesar(bPorDeadLockTimeout) {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        //var bCorrecto = true;
        sb.Append("procesar@#@");
        sb.Append(((bPorDeadLockTimeout) ? "1" : "0") + "@#@");

        var aFila = FilasDe("tblDatos2");
        for (var i = 0; i < aFila.length; i++) {
            //if (aFila[i].getAttribute("nodo_destino") == "") {
            //    bCorrecto = false;
            //    ocultarProcesando();
            //    mmoff("War","Debe seleccionar el "+ strEstructuraNodo +" destino de todos los contratos.",380);
            //    return;
            //}
            //Si los datos de origen y destino son iguales, no hago nada
            if (aFila[i].getAttribute("nodo_origen") == aFila[i].getAttribute("nodo_destino") &&
                aFila[i].getAttribute("cliente_origen") == aFila[i].getAttribute("clie_destino") &&
                aFila[i].getAttribute("gestor_origen") == aFila[i].getAttribute("gest_destino") &&
                aFila[i].getAttribute("comercial_origen") == aFila[i].getAttribute("come_destino") &&
                aFila[i].getAttribute("responsable_origen") == aFila[i].getAttribute("resp_destino"))
                continue
            sb.Append(aFila[i].id + "##"); //0
            sb.Append(aFila[i].getAttribute("nodo_origen") + "##"); //1
            sb.Append(aFila[i].getAttribute("nodo_destino") + "##"); //2

            if (aFila[i].cells[1].innerHTML == "") sb.Append("##"); //3
            else if (aFila[i].cells[1].innerHTML.indexOf("imgImanPropio.gif") != -1) sb.Append("P##"); //3
            else sb.Append("T##"); //3

            //Gestor de producción
            sb.Append(aFila[i].getAttribute("gestor_origen") + "##"); //4
            sb.Append(aFila[i].getAttribute("gest_destino") + "##"); //5
            if (aFila[i].cells[3].innerHTML == "") sb.Append("##"); //6
            else if (aFila[i].cells[3].innerHTML.indexOf("imgSubNodo.gif") != -1) sb.Append("P##"); //6
            else sb.Append("T##"); //6

            //Cliente HERMES
            sb.Append(aFila[i].getAttribute("cliente_origen") + "##"); //7
            sb.Append(aFila[i].getAttribute("clie_destino") + "##"); //8
            if (aFila[i].cells[5].innerHTML == "") sb.Append("##"); //9
            else sb.Append("T##"); //9

            //Responsable
            sb.Append(aFila[i].getAttribute("responsable_origen") + "##"); //10
            sb.Append(aFila[i].getAttribute("resp_destino") + "##"); //11

            //Comercial HERMES
            sb.Append(aFila[i].getAttribute("comercial_origen") + "##"); //12
            sb.Append(aFila[i].getAttribute("come_destino") + "##"); //13

            if (aFila[i].cells[8].innerHTML == "") sb.Append(""); //14
            else if (aFila[i].cells[8].innerHTML.indexOf("imgTrasladoOK.gif") != -1) sb.Append("1"); //14
            else sb.Append("0"); //14
            sb.Append("##");
            sb.Append(aFila[i].getAttribute("codigo_excepcion") + "##"); //15
            //Miro si es un contrato recuperado, es decir, estaba aparcado
            sb.Append(aFila[i].getAttribute("recuperada") + "///");
        }
        //sb.Append("@#@");
        //sb.Append(($I("chkMantResp").checked) ? "1" : "0");
        //sb.Append(aFila[i].getAttribute("acc_responsable"));//Indica la acción a realizar sobre los responsables de proyecto
        //alert(sb.ToString());return;
        //if (bCorrecto)
        RealizarCallBack(sb.ToString(), "");

        //$I("divCatalogo").children[0].innerHTML = "";

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}

function setArrastrarGestor() {
    try {
        var aFila = FilasDe("tblDatos2");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                //aFila[i].className = "";

                if (aFila[i].cells[3].children.length > 0)
                    aFila[i].cells[3].children[0].removeNode();

                if ($I("chkArrastrarGestor").checked) aFila[i].cells[3].appendChild(oImgGestor.cloneNode(true));

                aFila[i].cells[8].innerHTML = "";
            }
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al establecer si se arrastran los gestores de producción.", e.message);
    }
}
function setArrastrarCliente() {
    try {
        var aFila = FilasDe("tblDatos2");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                //aFila[i].className = "";

                if (aFila[i].cells[5].children.length > 0)
                    aFila[i].cells[5].children[0].removeNode();

                if ($I("chkArrastrarCliente").checked) aFila[i].cells[5].appendChild(oImgCliente.cloneNode(true));

                aFila[i].cells[8].innerHTML = "";
            }
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al establecer si se arrastran los clientes.", e.message);
    }
}

function ponerDestino(tipo) {
    var aFila = FilasDe("tblDatos2");
    for (var i = 0; i < aFila.length; i++) {
        if (aFila[i].className == "FS") {
            //aFila[i].className = "";
            switch (tipo) {
                //case "nodo":
                //    aFila[i].nodo_destino = $I("hdnIdNodoDestino").value;
                //    aFila[i].cells[2].children[0].innerText = $I("txtCRDestino").value;
                //    break;
                case "responsable":
                    aFila[i].setAttribute("resp_destino", $I("hdnIdResponsableDestino").value);
                    aFila[i].setAttribute("nom_responsable", $I("txtDesResponsableDestino").value);
                    break;
                case "gestor":
                    aFila[i].setAttribute("gest_destino", $I("hdnIdGestorDestino").value);
                    aFila[i].cells[4].children[0].innerText = $I("txtDesGestorDestino").value;
                    break;
                case "cliente":
                    aFila[i].setAttribute("clie_destino", $I("hdnIdClienteDestino").value);
                    aFila[i].cells[6].children[0].innerText = $I("txtDesClienteDestino").value;
                    break;
                case "comercial":
                    aFila[i].setAttribute("come_destino", $I("hdnIdComercialDestino").value);
                    aFila[i].cells[7].children[0].innerText = $I("txtDesComercialDestino").value;
                    break;
            }
            ponerToolTip(aFila[i]);
            aFila[i].cells[8].innerHTML = "";
        }
    }
}
function ponerToolTip(oF) {
    //El código de más abajo asigna los valores correctamente pero se sigue mostrando el tooltip con los datos originales
    var sTootTip = "", sTitle = "", sResponsable = "", sCR = "", sGestor = "";

    sResponsable = oF.getAttribute("resp_destino") + " - " + oF.getAttribute("nom_responsable");
    sCR = oF.getAttribute("nodo_destino") + " - " + oF.cells[2].children[0].innerText;
    sGestor = oF.getAttribute("gest_destino") + " - " + oF.cells[4].children[0].innerText;

    sTitle = "<label style='width:70px;'>Contrato:</label>" + oF.cells[0].children[0].innerHTML + "<br>";
    sTitle += "<label style='width:70px;'>Responsable:</label>" + sResponsable + "<br>";
    sTitle += "<label style='width:70px;'>" + strEstructuraNodo + ":</label>" + sCR + "<br>";
    sTitle += "<label style='width:70px;'>Gestor Prod.:</label>" + sGestor + "<br>";
    sTitle += "<label style='width:70px;'>Cliente:</label>" + oF.cells[6].children[0].innerHTML + "<br>";
    sTitle += "<label style='width:70px;'>Comercial:</label>" + oF.cells[7].children[0].innerHTML + "<br>";
    sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

    if (ie) {
        if (oF.children[0].getAttribute("boBDY") == null) {
            oF.children[0].setAttribute("title", sTootTip);
        }
        else {
            oF.children[0].setAttribute("boBDY", sTitle);
        }
    }
    else {
        if (oF.children[0].getAttribute("title") == null) {
            oF.children[0].setAttribute("title", sTootTip);
        }
        else {//Falta que en Chrome se actualice el Tooltip cuando la fila ya está construida y se cambia algún dato
            oF.children[0].removeAttribute("title");
            oF.children[0].removeAttribute("boBDY");
            if (oF.getAttribute("title") == null) {
                oF.setAttribute('title', "Contrato: " + oF.cells[0].children[0].innerHTML +
                                         "       Responsable: " + sResponsable +
                                         "       " + strEstructuraNodo + ": " + sCR +
                                         "       Gestor Producción: " + sGestor +
                                         "       Cliente: " + oF.cells[6].children[0].innerHTML +
                                         "       Comercial: " + oF.cells[7].children[0].innerHTML);
            }
            else {
                oF.removeAttribute("title");
            }
        }
    }
}