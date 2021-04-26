function init() {
    try {
        $I("procesando").style.top = "420px";
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/* El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error" */
function RespuestaCallBack(strResultado, context) {
    try {
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK") {
            mostrarErrorSQL(aResul[3], aResul[2]);
        } else {
            switch (aResul[0]) {
                case "grabar":
                    desActivarGrabar();
                    if (fTrim($I("txtTolerancia").value) == "") $I("txtTolerancia").value = "0,00";
                    if (fTrim($I("txtProduccionCVT").value) == "") $I("txtProduccionCVT").value = "0,00";
                    var aFilas = FilasDe("tblTablas");
                    for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
                        if (aFilas[i].getAttribute("bd") != "") {
                            mfa(aFilas[i], "N");
                        }
                    }
                    mmoff("Suc", "Grabación correcta", 160);
                    break;
                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;

            }
            ocultarProcesando();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la respuesta del callback.", e.message);
    }
    window.focus();
}

function comprobarDatos() {
    if (fTrim($I("txtHistorico").value) == "") {
        mmoff("War", "Los meses de paso históricos no puede estar vacio.", 300, 2000);
        $I("txtHistorico").focus();
        return false;
    }
    if (parseInt(fTrim($I("txtHistorico").value)) < 1) {
        mmoff("War", "Los meses de paso históricos no puede ser menor de 1.", 330, 2000);
        $I("txtHistorico").focus();
        return false;
    }

    if (parseInt(dfn($I("txtDiasAvisoVto").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txtDiasAvisoVto").focus();
        return false;
    }
    return true;
}

function grabar() {
    try {
        if (!comprobarDatos()) return;
        mostrarProcesando();
        var sCad = "";
        var js_args = "grabar@#@";
        js_args += $I("txtTolerancia").value + "#sCad#";
        js_args += parseInt($I("txtHistorico").value) + "#sCad#";
        js_args += (($I("chkAccesoAu").checked) ? "1" : "0") + "#sCad#";
        //($I("chkGeneral").checked) ? js_args += "1@#@" : js_args += "0@#@";
        js_args += getRadioButtonSelectedValue("rdbEstado", true) + "#sCad#";
        js_args += $I("txtProduccionCVT").value + "#sCad#";
        js_args += (($I("chkAlertas").checked) ? "1" : "0") + "#sCad#";
        js_args += (($I("chkMailCIA").checked) ? "1" : "0") + "#sCad#";
        js_args += (($I("chkForaneo").checked) ? "1" : "0") + "#sCad#";
        js_args += parseInt(dfn($I("txtDiasAvisoVto").value)) + "@#@";

        var aFilas = FilasDe("tblTablas");
        for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
            if (aFilas[i].getAttribute("bd") != "") {
                if (sCad == "") sCad = aFilas[i].id + "#sCad#";
                else sCad += "#sFin#" + aFilas[i].id + "#sCad#";
                sCad += ((aFilas[i].cells[2].children[0].checked) ? "1" : "0");
            }
        }

        RealizarCallBack(js_args + sCad, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de parametrización.", e.message);
    }
}

function setFiguras() {
    try {

        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/Administracion/Foraneos/FigProy/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(810, 550));
        //window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el " + strEstructuraNodo, e.message);
    }
}
function cambiarMes(sValor, sModulo) {
    try {
        switch (sValor) {
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
        }
        var nAnoMesActual = (sModulo == "ECO") ? $I("hdnCierreEmpresa").value : $I("hdnCierreIAPEmpresa").value;
        switch (sValor) {
            case "A":
                nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
                break;
            case "S":
                nAnoMesActual = AddAnnomes(nAnoMesActual, 1);
                break;
        }
        if (sModulo == "ECO") {
            $I("hdnCierreEmpresa").value = nAnoMesActual;
            $I("txtCierreEmpresa").value = AnoMesToMesAnoDescLong($I("hdnCierreEmpresa").value);
        } else {
            $I("hdnCierreIAPEmpresa").value = nAnoMesActual;
            $I("txtCierreIAPEmpresa").value = AnoMesToMesAnoDescLong($I("hdnCierreIAPEmpresa").value);
        }
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}
