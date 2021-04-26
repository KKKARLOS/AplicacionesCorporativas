function init() {
    try {
        if (!mostrarErrores()) return;

        $I("rdbPGE_0").setAttribute("title", $I("lblJornadasPGE").getAttribute("title"));
        $I("rdbPGE_1").setAttribute("title", $I("lblJornadasESTPGE").getAttribute("title"));
        $I("rdbPGE_2").setAttribute("title", $I("lblJornadasPGESJI").getAttribute("title"));
        
        ocultarProcesando();
        $I("txtString").focus();

        var sTooltipJornadas = "<label style='width:190px;'>- Desvío absoluto respecto del 100%:</label><br><label style='width:50px;'><= 5%:</label>Verde<br><label style='width:50px;'><= 10%:</label>Amarillo<br><label style='width:44px;padding-left:6px;'>> 10%:</label>Rojo";
        $I("imgSemJornadas").onmouseover = function() { showTTE(Utilidades.escape(sTooltipJornadas), "Criterio establecido", null, 200); }
        $I("imgSemJornadas").onmouseout = function() { hideTTE(); }

        var sTooltipTareas = "<label style='width:190px;'>- Indicador absoluto:</label><br><label style='width:50px;'>>= 90%:</label>Verde<br><label style='width:50px;'>>= 80%:</label>Amarillo<br><label style='width:44px;padding-left:6px;'>< 80%:</label>Rojo";
        $I("imgSemTareas").onmouseover = function() { showTTE(Utilidades.escape(sTooltipTareas), "Criterio establecido", null, 200); }
        $I("imgSemTareas").onmouseout = function() { hideTTE(); }
        
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptar() {
    try {
        var strString = $I("txtString").value;
        if (strString == "") {
            mmoff("War", "La denominacion es obligatoria.", 230);
            $I("txtString").focus();
            return;
        }
        setOp($I("btnAceptar"), 30);
        setOp($I("btnCancelar"), 30);
        var js_args = "crearLB@#@";
        js_args += Utilidades.escape(strString);

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana() {
    try {
        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "crearLB":
                mmoff("Suc", "Línea base creada correctamente", 240, 2000);
                setTimeout("cerrarVentana();", 2000);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function setAnalisisJornadas() {
    try {
        //alert(getRadioButtonSelectedValue("rdbPGE", true));
        //alert($I("cldJornadasPST").title);
        var nJornadasPST = getFloat($I("cldJornadasPST").getAttribute("jornadas"));
        var nJornadasPGE = 0;
        //alert(getFloat($I("lblJornadasPGE").getAttribute("jornadas")));
        switch (getRadioButtonSelectedValue("rdbPGE", true)) {
            case "1": nJornadasPGE = getFloat($I("lblJornadasPGE").getAttribute("jornadas")); break;
            case "2": nJornadasPGE = getFloat($I("lblJornadasESTPGE").getAttribute("jornadas")); break;
            case "3": nJornadasPGE = getFloat($I("lblJornadasPGESJI").getAttribute("jornadas")); break;
        }
        var nIndicadorJornadas = 0;
        if (nJornadasPST != 0) nIndicadorJornadas = nJornadasPGE * 100 / nJornadasPST;
        $I("cldJornadasIndi").innerText = nIndicadorJornadas.ToString("N") + "%";
        
        var nCountR = 0;
        var nCountA = 0;

        if (Math.abs(100 - Math.abs(nIndicadorJornadas)) <= 5) {
            $I("imgSemJornadas").setAttribute("src", "../../../../Images/imgSemaforoVF.gif");
        }
        else if (Math.abs(100 - Math.abs(nIndicadorJornadas)) <= 10) {
            $I("imgSemJornadas").setAttribute("src", "../../../../Images/imgSemaforoA.gif");
            nCountA++;
        }
        else {
            $I("imgSemJornadas").setAttribute("src", "../../../../Images/imgSemaforoR.gif");
            nCountR++;
        }

        var nIndicadorTareas = getFloat($I("cldTareasIndi").innerText.substring(0, $I("cldTareasIndi").innerText.length - 1));
        //alert(nIndicadorTareas);
        if (Math.abs(nIndicadorTareas) >= 90){
            $I("imgSemTareas").setAttribute("src", "../../../../Images/imgSemaforoVF.gif");
        }
        else if (Math.abs(nIndicadorTareas) >= 80) {
            $I("imgSemTareas").setAttribute("src", "../../../../Images/imgSemaforoA.gif");
            nCountA++;
        }
        else {
            $I("imgSemTareas").setAttribute("src", "../../../../Images/imgSemaforoR.gif");
            nCountR++;
        }

        $I("lblRecomendacion").style.color = "black";
        if (nCountR > 0 || nCountA > 1) {
            $I("lblRecomendacion").innerText = "Corregir datos antes de crear la línea base.";
            $I("lblRecomendacion").style.color = "red";
        }
        else if (nCountA > 0) {
            $I("lblRecomendacion").innerText = "Revisar datos antes de crear la línea base.";
            $I("lblRecomendacion").style.color = "red";
        }
        else
            $I("lblRecomendacion").innerText = "Datos consistentes. Se puede crear la línea base.";

    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el análisis de jornadas", e.message);
    }
}
