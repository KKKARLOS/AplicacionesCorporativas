var sValorNodo = "";
/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 260;
var nTopPestana = 124;
/* Fin de Valores necesarios para la pestaña retractil */

var js_Valores = new Array();

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

function init() {
    try {
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target;
        ToolTipBotonera("grabar", "Modifica el estado de los proyectos económicos seleccionados");

        mostrarOcultarPestVertical();
        if (es_administrador == "A" || es_administrador == "SA") {
           // if (sNodoFijo == "0") {
                $I("lblNodo").className = "enlace";
                $I("lblNodo").onclick = function () { getNodo() };
            //}
            sValorNodo = $I("hdnIdNodo").value;
        } else sValorNodo = $I("cboCR").value;

        if (sValorNodo != "") {
            setEnlace("lblCNP", "H");
            setEnlace("lblCSN1P", "H");
            setEnlace("lblCSN2P", "H");
            setEnlace("lblCSN3P", "H");
            setEnlace("lblCSN4P", "H");
        }
        $I("divCatalogo2").children[0].innerHTML = "<table id='tblDatos2' class='texto MM' style='WIDTH: 450px;table-layout:fixed' cellSpacing='0' border='0'><colgroup><col style='width:20px' /><col style='width:20px' /><col style='width:20px' /><col style='width:50x;' /><col style='width:340px;' /></colgroup></TABLE>";
        ocultarProcesando();
        
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function CargarProyectos() {
    try {
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='texto MM' style='WIDTH: 450px;'><colgroup><col style='width:20px' /><col style='width:20px' /><col style='width:20px' /><col style='width:50x;' /><col style='width:340px;' /></colgroup></table>";
        actualizarLupas("tblCatIni", "tblDatos");
        var js_args = "PROYECTOS@#@";
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error en la función CargarProyectos", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {

          
            case "grabar":
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                setTimeout("buscar();", 50);                
                break;

            default:
                nTopScrollProy = 0;
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblCatIni", "tblDatos");
                nIDTimeProy = setTimeout("scrollTablaProy();", 20);
                break;
        }

        //ActiDesCombo();
        ocultarProcesando();
    }
}

function getCliente() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0";
        modalDialog.Show(sPantalla, self, sSize(600, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function borrarCliente() {
    try {
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        borrarCatalogo();
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}


function getNodo() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx";
        modalDialog.Show(sPantalla, self, sSize(500, 470))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];

                    if (ie) $I("lblCNP").innerText = aDatos[2];
                    else $I("lblCNP").textContent = aDatos[2];

                    $I("lblCNP").title = aDatos[2];

                    if (ie) $I("lblCSN1P").innerText = aDatos[4];
                    else $I("lblCSN1P").textContent = aDatos[4];

                    $I("lblCSN1P").title = aDatos[4];

                    if (ie) $I("lblCSN2P").innerText = aDatos[6];
                    else $I("lblCSN2P").textContent = aDatos[6];

                    $I("lblCSN2P").title = aDatos[6];

                    if (ie) $I("lblCSN3P").innerText = aDatos[8];
                    else $I("lblCSN3P").textContent = aDatos[8];

                    $I("lblCSN3P").title = aDatos[8];

                    if (ie) $I("lblCSN4P").innerText = aDatos[10];
                    else $I("lblCSN4P").textContent = aDatos[10];

                    $I("lblCSN4P").title = aDatos[10];

                    $I("txtCNP").value = "";
                    $I("hdnCNP").value = "";
                    $I("txtCSN1P").value = "";
                    $I("hdnCSN1P").value = "";
                    $I("txtCSN2P").value = "";
                    $I("hdnCSN2P").value = "";
                    $I("txtCSN3P").value = "";
                    $I("hdnCSN3P").value = "";
                    $I("txtCSN4P").value = "";
                    $I("hdnCSN4P").value = "";

                    setEnlace("lblCNP", "H");
                    setEnlace("lblCSN1P", "H");
                    setEnlace("lblCSN2P", "H");
                    setEnlace("lblCSN3P", "H");
                    setEnlace("lblCSN4P", "H");

                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el " + strEstructuraNodo, e.message);
    }
}
function borrarNodo() {
    try {
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
        } else {
            $I("cboCR").value = "";
        }
        sValorNodo = "";

        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        setEnlace("lblCNP", "D");
        setEnlace("lblCSN1P", "D");
        setEnlace("lblCSN2P", "D");
        setEnlace("lblCSN3P", "D");
        setEnlace("lblCSN4P", "D");

        $I("divCatalogo").children[0].innerHTML = "";
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el " + strEstructuraNodo, e.message);
    }
}

function getResponsable() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto";
        modalDialog.Show(sPantalla, self, sSize(550, 540))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function borrarResponsable() {
    try {
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        borrarCatalogo();
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}

function setNumPE() {
    try {
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
        } else {
            $I("cboCR").value = "";
        }
        sValorNodo = "";

        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        setEnlace("lblCNP", "D");
        setEnlace("lblCSN1P", "D");
        setEnlace("lblCSN2P", "D");
        setEnlace("lblCSN3P", "D");
        setEnlace("lblCSN4P", "D");

        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        //$I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("cboCualidad").value = "";
        $I("txtDesPE").value = "";
        borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function setDesPE() {
    try {
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
        } else {
            $I("cboCR").value = "";
        }
        sValorNodo = "";
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        //$I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("cboCualidad").value = "";
        $I("txtNumPE").value = "";
        borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir la denominación de proyecto", e.message);
    }
}

function getHorizontal() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getHorizontal.aspx";
        modalDialog.Show(sPantalla, self, sSize(400, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdHorizontal").value = aDatos[0];
                    $I("txtDesHorizontal").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener Horizontal", e.message);
    }
}

function borrarHorizontal() {
    try {
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}

function getContrato() {
    try {

        //        if (sValorNodo == ""){
        //            alert("Para poder obtener el contrato, antes debe seleccionar el "+ strEstructuraNodo);
        //            return;
        //        }

        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getContrato.aspx?nNodo=" + sValorNodo + "&origen=busqueda";
        modalDialog.Show(sPantalla, self, sSize(1020, 550))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtIDContrato").value = aDatos[0];
                    $I("txtDesContrato").value = Utilidades.unescape(aDatos[1]);
                    $I("hdnIdCliente").value = aDatos[2];
                    $I("txtDesCliente").value = Utilidades.unescape(aDatos[3]);
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el contrato", e.message);
    }
}

function borrarContrato() {
    try {
        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el contrato", e.message);
    }
}
function getCualificador(sOpcion) {
    try {
        mostrarProcesando();
        var strEnlace = "";

        switch (sOpcion) {
            case "Qn": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + sValorNodo + "&sTitulo=" + $I("lblCNP").innerText; break;
            case "Q1": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + sValorNodo + "&sTitulo=" + $I("lblCSN1P").innerText; break;
            case "Q2": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + sValorNodo + "&sTitulo=" + $I("lblCSN2P").innerText; break;
            case "Q3": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + sValorNodo + "&sTitulo=" + $I("lblCSN3P").innerText; break;
            case "Q4": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + sValorNodo + "&sTitulo=" + $I("lblCSN4P").innerText; break;
        }
        modalDialog.Show(strEnlace, self, sSize(400, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    switch (sOpcion) {
                        case "Qn":
                            $I("hdnCNP").value = aDatos[0];
                            $I("txtCNP").value = aDatos[1];
                            break;
                        case "Q1":
                            $I("hdnCSN1P").value = aDatos[0];
                            $I("txtCSN1P").value = aDatos[1];
                            break;
                        case "Q2":
                            $I("hdnCSN2P").value = aDatos[0];
                            $I("txtCSN2P").value = aDatos[1];
                            break;
                        case "Q3":
                            $I("hdnCSN3P").value = aDatos[0];
                            $I("txtCSN3P").value = aDatos[1];
                            break;
                        case "Q4":
                            $I("hdnCSN4P").value = aDatos[0];
                            $I("txtCSN4P").value = aDatos[1];
                            break;
                    }
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los cualificadores", e.message);
    }
}
function borrarCualificador(sOpcion) {
    try {
        switch (sOpcion) {
            case "Qn":
                $I("hdnCNP").value = "";
                $I("txtCNP").value = "";
                break;
            case "Q1":
                $I("hdnCSN1P").value = "";
                $I("txtCSN1P").value = "";
                break;
            case "Q2":
                $I("hdnCSN2P").value = "";
                $I("txtCSN2P").value = "";
                break;
            case "Q3":
                $I("hdnCSN3P").value = "";
                $I("txtCSN3P").value = "";
                break;
            case "Q4":
                $I("hdnCSN4P").value = "";
                $I("txtCSN4P").value = "";
                break;
        }
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el cualificador.", e.message);
    }
}
function setEnlace(sOpcion, sAccion) {
    try {
        switch (sOpcion) {
            case "lblCNP":
                if (sAccion == "H") {
                    $I("lblCNP").className = "enlace";
                    $I("lblCNP").onclick = function () { getCualificador("Qn"); };
                    $I("lblCNP").onmouseover = function () { mostrarCursor(this) };
                    $I("lblCNP").attachEvent("onmouseover", TTip);

                } else {
                    $I("lblCNP").className = "texto";
                    $I("lblCNP").innerText = "Cualificador Qn";

                    $I("lblCNP").style.cursor = "default";
                    $I("lblCNP").title = "Cualificador Qn";
                    $I("lblCNP").attachEvent("onmouseover", TTip);
                    $I("lblCNP").onclick = null;
                }
                break;
            case "lblCSN1P":
                if (sAccion == "H") {

                    $I("lblCSN1P").className = "enlace";
                    $I("lblCSN1P").onclick = function () { getCualificador("Q1"); };
                    $I("lblCSN1P").onmouseover = function () { mostrarCursor(this) };
                    $I("lblCSN1P").attachEvent("onmouseover", TTip);

                } else {

                    $I("lblCSN1P").className = "texto";
                    $I("lblCSN1P").innerText = "Cualificador Q1";

                    $I("lblCSN1P").style.cursor = "default";
                    $I("lblCSN1P").title = "Cualificador Q1";
                    $I("lblCSN1P").attachEvent("onmouseover", TTip);
                    $I("lblCSN1P").onclick = null;
                }
                break;
            case "lblCSN2P":

                if (sAccion == "H") {

                    $I("lblCSN2P").className = "enlace";
                    $I("lblCSN2P").onclick = function () { getCualificador("Q2"); };
                    $I("lblCSN2P").onmouseover = function () { mostrarCursor(this) };
                    $I("lblCSN2P").attachEvent("onmouseover", TTip);

                } else {

                    $I("lblCSN2P").className = "texto";
                    $I("lblCSN2P").innerText = "Cualificador Q2";

                    $I("lblCSN2P").style.cursor = "default";
                    $I("lblCSN2P").title = "Cualificador Q1";
                    $I("lblCSN2P").attachEvent("onmouseover", TTip);
                    $I("lblCSN2P").onclick = null;
                }

                break;
            case "lblCSN3P":
                if (sAccion == "H") {
                    $I("lblCSN3P").className = "enlace";
                    $I("lblCSN3P").onclick = function () { getCualificador("Q3"); };
                    $I("lblCSN3P").onmouseover = function () { mostrarCursor(this) };
                    $I("lblCSN3P").attachEvent("onmouseover", TTip);

                } else {

                    $I("lblCSN3P").className = "texto";
                    $I("lblCSN3P").innerText = "Cualificador Q3";

                    $I("lblCSN3P").style.cursor = "default";
                    $I("lblCSN3P").title = "Cualificador Q3";
                    $I("lblCSN3P").attachEvent("onmouseover", TTip);
                    $I("lblCSN3P").onclick = null;
                }

                break;
            case "lblCSN4P":
                if (sAccion == "H") {
                    with ($I("lblCSN4P")) {
                        className = "enlace";
                        onclick = function () { getCualificador("Q4"); };
                        onmouseover = function () { mostrarCursor(this) };
                    }
                    $I("lblCSN4P").attachEvent("onmouseover", TTip);
                } else {
                    with ($I("lblCSN4P")) {
                        innerText = "Cualificador Q4";
                        title = "Cualificador Q4";
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN4P").attachEvent("onmouseover", TTip);
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar/deshabilitar enlaces", e.message);
    }
}

function setNodo(oNodo) {
    try {
        sValorNodo = oNodo.value;

        if (sValorNodo != "") {

            if (ie) {
                $I("lblCNP").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CNP);
                $I("lblCSN1P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN1P);
                $I("lblCSN2P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN2P);
                $I("lblCSN3P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN3P);
                $I("lblCSN4P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN4P);
            }
            else {
                $I("lblCNP").textContent = Utilidades.unescape(oNodo[oNodo.selectedIndex].getAttribute("CNP"));
                $I("lblCSN1P").textContent = Utilidades.unescape(oNodo[oNodo.selectedIndex].getAttribute("CSN1P"));
                $I("lblCSN2P").textContent = Utilidades.unescape(oNodo[oNodo.selectedIndex].getAttribute("CSN2P"));
                $I("lblCSN3P").textContent = Utilidades.unescape(oNodo[oNodo.selectedIndex].getAttribute("CSN3P"));
                $I("lblCSN4P").textContent = Utilidades.unescape(oNodo[oNodo.selectedIndex].getAttribute("CSN4P"));
            }

            setEnlace("lblCNP", "H");
            setEnlace("lblCSN1P", "H");
            setEnlace("lblCSN2P", "H");
            setEnlace("lblCSN3P", "H");
            setEnlace("lblCSN4P", "H");
        } else {
            setEnlace("lblCNP", "D");
            setEnlace("lblCSN1P", "D");
            setEnlace("lblCSN2P", "D");
            setEnlace("lblCSN3P", "D");
            setEnlace("lblCSN4P", "D");
        }

        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        borrarCatalogo();
        if ($I('chkActuAuto').checked) {
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el " + strEstructuraNodo + ".", e.message);
    }
}
function getNaturaleza() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getNaturalezaSimple.aspx";
        modalDialog.Show(sPantalla, self, sSize(400, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNaturaleza").value = aDatos[0];
                    $I("txtDesNaturaleza").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener Naturaleza", e.message);
    }
}

function borrarNaturaleza() {
    try {
        $I("hdnIdNaturaleza").value = "";
        $I("txtDesNaturaleza").value = "";
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la Naturaleza", e.message);
    }
}

function borrarCatalogo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}


function insertarTabla(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "16px";
            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
            oNF.cells[0].children[0].className = "NBR";
            oNF.cells[0].children[0].setAttribute("style", "width:260px;");
            oNF.cells[0].children[0].attachEvent("onmouseover", TTip);
            oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

function buscar() {
    try {

        /*if ($I("cboAnnoPIG").value == "" && $I("tblProyecto").rows.length == 0) {
            mmoff("War", "Debes indicar algún filtro de búsqueda además del estado del proyecto.", 450);
            return;
        }*/
        if ($I("cboEstado").value == "") {
            mmoff("War", "Debes indicar el estado.", 190);
            return;
        }

        var smod = "pste";        

        var js_args = "PROYECTOS@#@";
        js_args += smod + "@#@";
        js_args += sValorNodo + "@#@";
        js_args += $I("cboEstado").value + "@#@";
        js_args += $I("cboCategoria").value + "@#@";
        js_args += $I("hdnIdCliente").value + "@#@";
        js_args += $I("hdnIdResponsable").value + "@#@";
        js_args += dfn($I("txtNumPE").value) + "@#@";
        js_args += Utilidades.escape($I("txtDesPE").value) + "@#@";
        js_args += getRadioButtonSelectedValue("rdbTipoBusqueda", true) + "@#@";
        js_args += $I("cboCualidad").value + "@#@";//10
        js_args += $I("txtIDContrato").value.replace(".", "") + "@#@";//11
        js_args += $I("hdnIdHorizontal").value + "@#@";//12
        js_args += 0 + "@#@";//13
        js_args += $I("hdnCNP").value + "@#@";//14
        js_args += $I("hdnCSN1P").value + "@#@";//15
        js_args += $I("hdnCSN2P").value + "@#@";//16
        js_args += $I("hdnCSN3P").value + "@#@";//17
        js_args += $I("hdnCSN4P").value + "@#@";//18
        js_args += $I("cboModContratacion").value + "@#@";//19
        js_args += $I("hdnIdNaturaleza").value + "@#@";//20
        js_args += $I("cboAnnoPIG").value;//21

        //alert(js_args);     
        mostrarProcesando();
        RealizarCallBack(js_args, "");

        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}



function borrarCatalogo() {
    try {
        if ($I("divCatalogo").children[0].innerHTML != "") {
            $I("divCatalogo").children[0].innerHTML = "<TABLE style='WIDTH: 450px' id='tblDatos' class='texto MM' cellSpacing=0><COLGROUP><COL style='width: 20px'><COL style='width: 20px'><COL style='width: 20px'><COL style='width: 50px'><COL style='width: 320px'></COLGROUP></TABLE>";
        }
        if ($I("divCatalogo2").children[0].innerHTML != "") {
            $I("divCatalogo2").children[0].innerHTML = "<TABLE style='WIDTH: 450px' id='tblDatos2' class='texto MM' cellSpacing=0><COLGROUP><COL style='width: 20px'><COL style='width: 20px'><COL style='width: 20px'><COL style='width: 50px'><COL style='width: 320px'></COLGROUP></TABLE>";
        }        
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}
function getDatosTabla(nTipo) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo) {
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }

        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (i > 0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }

        if (sb.ToString().length > 8000) {
            ocultarProcesando();
            switch (nTipo) {
                //case 1: break;  
                case 3: mmoff("Inf", "Has seleccionado un número excesivo de naturalezas.", 450); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos.", 450); break;
            }
            return;
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}

function grabar() {
    try {
        if ($I("cboEstado").value == $I("cboEstadoFin").value) 
        {
            mmoff("Inf", "No ha modificado el nuevo estado", 300);
            return;
        }
        if ($I("tblDatos2").rows.length == 0) {
            mmoff("Inf", "No hay proyectos a grabar", 210);
            return;
        }        
        
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("grabar@#@");
        sb.Append($I("cboEstadoFin").value + "@#@");
        
        aFila = FilasDe("tblDatos2");        
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                //sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id + "##");
                sb.Append("///");
            }
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
function ActiDesCombo()
{
    try {
        if (!bPestRetrMostrada);
        {
            $I("dEstado2").style.visibility = "visible";
            if ($I("dEstado2").style.zIndex == "-1") $I("dEstado2").style.zIndex = "";
            else $I("dEstado2").style.zIndex = "-1";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el estado del proyecto", e.message);
    }
}    
function estado(sEstado) {
    try {

        $I("cboEstadoFin").length = 0;
         
        switch (sEstado) {
            case "P":

                var op1 = new Option("Presupuestado", "P");
                $I("cboEstadoFin").options[0] = op1;

                var op2 = new Option("Abierto", "A");
                $I("cboEstadoFin").options[1] = op2;

                $I("cboEstadoFin").value = "A";
                break;
            case "A":

                var op1 = new Option("Abierto", "A");
                $I("cboEstadoFin").options[0] = op1;

                var op2 = new Option("Cerrado", "C");
                $I("cboEstadoFin").options[1] = op2;

                $I("cboEstadoFin").value = "C";
                break;
            case "C":

                var op1 = new Option("Abierto", "A");
                $I("cboEstadoFin").options[0] = op1;

                var op2 = new Option("Cerrado", "C");
                $I("cboEstadoFin").options[1] = op2;

                $I("cboEstadoFin").value = "A";
                break;
            case "H":

                var op1 = new Option("Abierto", "A");
                $I("cboEstadoFin").options[0] = op1;

                var op2 = new Option("Histórico", "H");
                $I("cboEstadoFin").options[1] = op2;

                $I("cboEstadoFin").value = "A";
                break;
        }
        borrarCatalogo();
        
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el estado del proyecto", e.message);
    }
}

function setCombo() {
    try {
        borrarCatalogo();
        if ($I('chkActuAuto').checked) {
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}

function mdpsn(oNOBR) {
    try {
        insertarItem(oNOBR.parentNode.parentNode);
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar proyecto", e.message);
    }
}
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
        clearTimeout(nIDTimeProy);

        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);
                //oFila.style.cursor = strCurMAM;

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                oFila.ondblclick = function() { insertarItem(this) };


                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}
function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        var oNF;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;

                        oNF.setAttribute("style", "height:20px");
                        oNF.setAttribute("id", oRow.getAttribute("id"));
                        oNF.setAttribute("categoria", oRow.getAttribute("categoria"));
                        oNF.setAttribute("cualidad", oRow.getAttribute("cualidad"));
                        oNF.setAttribute("estado", oRow.getAttribute("estado"));
                        oNF.style.cursor = strCurMM;

                        oNF.attachEvent('onclick', mm);
                        oNF.attachEvent('onmousedown', DD);

                        oNC1 = oNF.insertCell(-1);

                        if (oRow.getAttribute("categoria") == "P") oNC1.appendChild(oImgProducto.cloneNode(true), null);
                        else oNC1.appendChild(oImgServicio.cloneNode(true), null);

                        oNC2 = oNF.insertCell(-1);

                        switch (oRow.getAttribute("cualidad")) {
                            case "C": oNC2.appendChild(oImgContratante.cloneNode(true), null); break;
                            case "J": oNC2.appendChild(oImgRepJor.cloneNode(true), null); break;
                            case "P": oNC2.appendChild(oImgRepPrecio.cloneNode(true), null); break;
                        }

                        oNC3 = oNF.insertCell(-1);

                        switch (oRow.getAttribute("estado")) {
                            case "A": oNC3.appendChild(oImgAbierto.cloneNode(true), null); break;
                            case "C": oNC3.appendChild(oImgCerrado.cloneNode(true), null); break;
                            case "H": oNC3.appendChild(oImgHistorico.cloneNode(true), null); break;
                            case "P": oNC3.appendChild(oImgPresup.cloneNode(true), null); break;
                        }

                        oNC4 = oNF.insertCell(-1);
                        oNC4.setAttribute("style", "text-align:right; padding-right:5px;");
                        oNF.cells[3].innerText = oRow.cells[3].innerText;

                        oNC5 = oNF.insertCell(-1);
                        oNF.cells[4].innerText = oRow.cells[4].innerText;
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        activarGrabar();
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
function insertarItem(oFila) {

    try {
        var idItem = oFila.id;
        var bExiste = false;

        for (var i = 0; i < $I("tblDatos2").rows.length; i++) {
            if ($I("tblDatos2").rows[i].id == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            //alert("El item indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        var oTable = $I("tblDatos2");
        //var sNuevo = oFila.innerText;
        var sNuevo = oFila.cells[4].innerText;

        for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            //var sActual=oTable.rows[iFilaNueva].innerText;
            var sActual = (ie) ? oTable.rows[iFilaNueva].cells[4].innerText : oTable.rows[iFilaNueva].cells[4].textContent;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var oNF = $I("tblDatos2").insertRow(iFilaNueva);

        oNF.setAttribute("style", "height:20px");
        oNF.setAttribute("id", oFila.getAttribute("id"));
        oNF.setAttribute("categoria", oFila.getAttribute("categoria"));
        oNF.setAttribute("cualidad", oFila.getAttribute("cualidad"));
        oNF.setAttribute("estado", oFila.getAttribute("estado"));
        oNF.style.cursor = strCurMM;

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNC1 = oNF.insertCell(-1);

        if (oFila.getAttribute("categoria") == "P") oNC1.appendChild(oImgProducto.cloneNode(true), null);
        else oNC1.appendChild(oImgServicio.cloneNode(true), null);

        oNC2 = oNF.insertCell(-1);

        switch (oFila.getAttribute("cualidad")) {
            case "C": oNC2.appendChild(oImgContratante.cloneNode(true), null); break;
            case "J": oNC2.appendChild(oImgRepJor.cloneNode(true), null); break;
            case "P": oNC2.appendChild(oImgRepPrecio.cloneNode(true), null); break;
        }

        oNC3 = oNF.insertCell(-1);

        switch (oFila.getAttribute("estado")) {
            case "A": oNC3.appendChild(oImgAbierto.cloneNode(true), null); break;
            case "C": oNC3.appendChild(oImgCerrado.cloneNode(true), null); break;
            case "H": oNC3.appendChild(oImgHistorico.cloneNode(true), null); break;
            case "P": oNC3.appendChild(oImgPresup.cloneNode(true), null); break;
        }

        oNC4 = oNF.insertCell(-1);
        oNC4.setAttribute("style", "text-align:right; padding-right:5px;");
        oNF.cells[3].innerText = oFila.cells[3].innerText;

        oNC5 = oNF.insertCell(-1);
        oNF.cells[4].innerText = oFila.cells[4].innerText;
        
        actualizarLupas("tblAsignados", "tblDatos2");
        activarGrabar();
        return iFilaNueva;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
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
                            mmoff("Inf", "Se ha detectado que hay elementos de la lista que no tienen formato numérico.\n\nDichos elementos han sido obviados el la búsqueda de resultados.", 380);
                        }
                    }
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a generar las réplicas y los meses.", e.message);
    }
}

function mostrarRelacionProyectos(sOpcion, sValor, sParesDatos) {
    try {
        if (sParesDatos == "") return;
        //alert("Proyectos("+ sOpcion +","+ sValor +")");
        if (sOpcion == "P") {
            if (sValor == "") return;
            if (isNaN(dfn(sValor))) {
                mmoff("War", "El valor introducido no es numérico", 180);
                $I("txtNumero").value = "";
                $I("txtNumero").focus();
                return;
            } else $I("txtNumero").value = sValor.ToString("N", 9, 0);
        }

        var js_args = "buscar@#@A@#@0@#@";        
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
