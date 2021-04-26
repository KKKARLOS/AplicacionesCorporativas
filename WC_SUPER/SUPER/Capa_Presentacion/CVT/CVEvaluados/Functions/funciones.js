var aFila;
var strMsg = "Hasta que el profesional no complete su CV no es posible indicar que se ha revisado.";

function init() {
    try {
        mmoff("InfPer", "Obteniendo los evaluados...", 200);
        getEvaluados();
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
oChk.setAttribute("style", "cursor:pointer; vertical-align:middle;");

var oImgOK = document.createElement("img");
oImgOK.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOk.gif");
oImgOK.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgMostrarCV = document.createElement("img");
oImgMostrarCV.setAttribute("src", "../../../images/imgMostrarCV.png");
oImgMostrarCV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; cursor:pointer;");

function getEvaluados() {
    try {
        //mostrarProcesando();
        var js_args = "getEvaluados@#@";
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
            case "getEvaluados":
                mmoff("hide");
                iFila = -1;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                break;
            case "grabar":
                bCambios = false;
                desActivarGrabar();
                setTimeout("getEvaluados();", 20);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

function comprobarDatos() {
    try {
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos.", e.message);
    }
}

function grabar() {
    try {

        if (!comprobarDatos()) return;
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("grabar@#@");

        var aFila = FilasDe("tblDatos");
        if (aFila != null) {
            for (var i = 0; i < aFila.length; i++) {
                if (!aFila[i].getAttribute("sw")) continue;
                if (aFila[i].getAttribute("bd") == "U") {
                    sb.Append(aFila[i].getAttribute("id") + "{sep}" + ((aFila[i].cells[3].children[0].checked) ? "1" : "0") + "{sepreg}");
                }
            }
            RealizarCallBack(sb.ToString(), "");
        } else
            ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
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
                oFila.style.cursor = "pointer";

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

                if (oFila.getAttribute("comp") == "1") {
                    var oImgCVOK = oImgOK.cloneNode(true);
                    oFila.cells[2].appendChild(oImgCVOK);
                }

                var oChkRevi = oChk.cloneNode(true);
                oChkRevi.onclick = function() { setRevisado(this) };
                oFila.cells[3].appendChild(oChkRevi);
                
                if (oFila.getAttribute("revi") == "1") {
                    oFila.cells[3].children[0].checked = true;
                } else if (oFila.getAttribute("comp") == "0") {
                    oFila.cells[3].children[0].title = strMsg;
                    oFila.cells[3].children[0].disabled = true;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de diálogos.", e.message);
    }
}

function setRevisado(oInput) {
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
            mmoff("War", "No se ha podido determinar la fila.", 250);
            return;
        }

        mfa(oFila, "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer como revisado.", e.message);
    }
}

function exportar() {
    try {
        if (iFila == -1) {
            mmoff("War", "Debes seleccionar el profesional cuyo currículum desea exportar.", 400, 4000);
            return;
        }

        if ($I("tblDatos").rows[iFila].getAttribute("comp") == "0") {
            mmoff("War", "Hasta que el profesional no complete su CV no es posible exportarlo.", 450, 4000);
            return;
        }
        mostrarProcesando();
        var idFicepi = $I("tblDatos").rows[iFila].id;
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/formaExport/Default.aspx?idFicepi=" + codpar(idFicepi);
        modalDialog.Show(strEnlace, self, sSize(350, 120));
        window.focus();
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al exportar", e.message);
    }
}

