
function init() {
    try {
        ToolTipBotonera("procesar", "Procesa las filas correctas del fichero cargado");
        ocultarProcesando();
        if ($I("hdnErrores").value != "") {
            AccionBotonera("procesar", "D");
            return;
        }

        if ($I("hdnIniciado").value == "T") {
            //var scldLinProc = (ie) ? $I("cldLinProc").innerText : $I("cldLinProc").textContent;
            //var scldLinOK = (ie) ? $I("cldLinOK").innerText : $I("cldLinOK").textContent;
            var scldLinProc = $I("cldLinProc").innerText;
            var scldLinOK = $I("cldLinOK").innerText;

            if (scldLinProc == scldLinOK && scldLinProc != "0") {
                $I("rdbImputacion_0").disabled = true;
                $I("rdbImputacion_1").disabled = true;
                //$I("cboFormato").disabled = true;
                AccionBotonera("procesar", "H");
                mmoff("Inf","Información del fichero de entrada correcta\n\nPulsa el botón procesar",380);
            }
            else {
                $I("fls").innerHTML = $I("sFLS").value;
                AccionBotonera("procesar", "D");
                mmoff("Err", "Se han detectado errores en el proceso verificación", 340);
            }
        }

        setExcelImg("imgExcel", "divErrores", "excel");
        setExcelImg("imgExcel2", "divErroresGrab", "excel2");

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function HabCarga() {
    try {
        AccionBotonera("procesar", "D");
        //if (ie) {
            $I("cldLinProc").innerText = "0";
            $I("cldLinOK").innerText = "0";
            $I("cldLinErr").innerText = "0";
            $I("lblFileName").innerText = "";
        /*}
        else {
            $I("cldLinProc").textContent = "0";
            $I("cldLinOK").textContent = "0";
            $I("cldLinErr").textContent = "0";
            $I("lblFileName").textContent = "";        
        }
        */
        $I("rdbImputacion_0").disabled = false;
        $I("rdbImputacion_1").disabled = false;
        //$I("cboFormato").disabled = false;
        setOp($I("btnVisualizar"), 100);
        setOp($I("btnCargar"), 100);        
        mmoff("Inf", "Se necesita analizar el fichero de entrada antes de procesarlo.", 390);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al habilitar los botones", e.message);
    }
}

function Visualizar() {
    try {
        var shell = new ActiveXObject("WScript.Shell");
        shell.run("notepad.exe " + $I("uplTheFile").value, 1, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a visualizar el fichero", e.message);
    }
}

function procesar() {
    var js_args = "";
    try {
        if (tblErrores.rows.length != 0) return;
        //if (ie) {
            $I('valproc').innerText = "procesadas";
            $I("cldLinProc").innerText = "0";
            $I("cldLinOK").innerText = "0";
            $I("cldLinErr").innerText = "0";
        /*}
        else {
            $I('valproc').textContent = "procesadas";
            $I("cldLinProc").textContent = "0";
            $I("cldLinOK").textContent = "0";
            $I("cldLinErr").textContent = "0";        
        }*/        
        js_args = "procesar@#@";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        // return false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos", e.message);
        //return false;
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
            case "procesar":
                AccionBotonera("procesar", "D");
                if (aResul[4] != "0") {
                    $I("divErroresGrab").children[0].innerHTML = aResul[5];
                    $I("divErroresGrab").scrollTop = 0;
                    nTopScroll = 0;
                }

                //if (ie) {
                    $I("cldLinProc").innerText = aResul[2];
                    $I("cldLinOK").innerText = aResul[3];
                    $I("cldLinErr").innerText = aResul[4];
                /*}
                else {
                    $I("cldLinProc").textContent = aResul[2];
                    $I("cldLinOK").textContent = aResul[3];
                    $I("cldLinErr").textContent = aResul[4];               
                }*/
                $I("rdbImputacion_0").disabled = false;
                $I("rdbImputacion_1").disabled = false;
                ocultarProcesando();

                if (aResul[4] != "0") mmoff("SucPer", "Proceso finalizado con errores", 210);
                else mmoff("SucPer", "Proceso finalizado correctamente", 210);

                break;
            default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function LeerFichero() {
    try {

        var sOL = getRadioButtonSelectedValue("rdbImputacion", false);
        if (sOL == "D")
            $I("fls").innerHTML = $I('sCab1').value + $I('sPie1').value;
        else
            $I("fls").innerHTML = $I('sCab2').value + $I('sPie2').value;

        $I("tblErroresGrab").outerHTML = "<TABLE style='WIDTH: 930px; BORDER-COLLAPSE: collapse' id=tblErroresGrab class=texto border=0 cellSpacing=0 cellPadding=0><COLGROUP><COL style='WIDTH: 70px'><COL style='WIDTH: 860px'></COLGROUP></TABLE>";

        //if (ie) {
            $I('valproc').innerText = "analizadas";
            $I("cldLinProc").innerText = "0";
            $I("cldLinOK").innerText = "0";
            $I("cldLinErr").innerText = "0";
        /*}
        else {
            $I('valproc').textContent = "analizadas";
            $I("cldLinProc").textContent = "0";
            $I("cldLinOK").textContent = "0";
            $I("cldLinErr").textContent = "0";
        } 
        */       
       
        $I("rdbImputacion_0").disabled = false;
        $I("rdbImputacion_1").disabled = false;
        
        var theform;
        theform = document.forms[0];

        theform.method = "POST";
        theform.enctype = "multipart/form-data";

        theform.submit();
    } catch (e) {
        mostrarErrorAplicacion("Error al validar el fichero de carga", e.message);
    }
}

function excel1() {
    try {
        mostrarProcesando();
        setTimeout("excel();", 20);
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function excel() {
    try {
        if ($I("tblErrores") == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;

        var sOL = getRadioButtonSelectedValue("rdbImputacion", false);
        if (sOL == "D") {
            sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
            sb.Append("     <td style='width:235px'>Usuario</TD>");
            sb.Append("     <td style='width:90px'>Fecha</TD>");
            sb.Append("     <td style='width:235px'>Tarea</TD>");
            sb.Append("     <td style='width:90px'>Esfuerzo</TD>");
            sb.Append("     <td style='width:280px'>Error</TD>");
            sb.Append(" </tr>");
        }
        else {
            sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
            sb.Append("     <td style='width:200px'>Usuario</TD>");
            sb.Append("     <td style='width:100px'>F.Desde</TD>");
            sb.Append("     <td style='width:100px'>F.Hasta</TD>");
            sb.Append("     <td style='width:200px'>Tarea</TD>");
            sb.Append("     <td style='width:90px'>Esfuerzo</TD>");
            sb.Append("     <td style='width:30px'>Fes.</TD>");
            sb.Append("     <td style='width:280px'>Error</TD>");
            sb.Append(" </tr>");
        }

        for (var i = 0; i < $I("tblErrores").rows.length; i++) {
            sb.Append($I("tblErrores").rows[i].outerHTML);
        }
/*
        var sTexto = "";
        for (var i = 0; i < $I("tblErrores").rows.length; i++) {
            sb.Append("<tr>");
            if (sOL == "D") {
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[0].innerText : $I("tblErrores").rows[i].cells[0].textContent;
                if (sTexto != "") sTexto += "-";
                sTexto += (ie) ? $I("tblErrores").rows[i].cells[1].innerText : $I("tblErrores").rows[i].cells[1].textContent;
                sb.Append("<td style='width:235px'>" + sTexto + "</td>");
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[2].innerText : $I("tblErrores").rows[i].cells[2].textContent;
                sb.Append("<td style='width:90px'>" + sTexto + "</td>");
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[3].innerText : $I("tblErrores").rows[i].cells[3].textContent;
                if (sTexto != "") sTexto += "-";
                sTexto += (ie) ? $I("tblErrores").rows[i].cells[4].innerText : $I("tblErrores").rows[i].cells[4].textContent;
                sb.Append("<td style='width:235px'>" + sTexto + "</td>");
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[5].innerText : $I("tblErrores").rows[i].cells[5].textContent;
                sb.Append("<td style='width:90px'>" + sTexto + "</td>");
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[6].innerText : $I("tblErrores").rows[i].cells[6].textContent;
                sb.Append("<td style='width:280px'>" + sTexto + "</td>");
            }
            else 
            {
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[0].innerText : $I("tblErrores").rows[i].cells[0].textContent;
                if (sTexto != "") sTexto += "-";
                sTexto += (ie) ? $I("tblErrores").rows[i].cells[1].innerText : $I("tblErrores").rows[i].cells[1].textContent;
                sb.Append("<td style='width:235px'>" + sTexto + "</td>");
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[2].innerText : $I("tblErrores").rows[i].cells[2].textContent;
                sb.Append("<td style='width:100px'>" + sTexto + "</td>");
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[3].innerText : $I("tblErrores").rows[i].cells[3].textContent;
                sb.Append("<td style='width:100px'>" + sTexto + "</td>");

                sTexto = (ie) ? $I("tblErrores").rows[i].cells[4].innerText : $I("tblErrores").rows[i].cells[4].textContent;
                if (sTexto != "") sTexto += "-";
                sTexto += (ie) ? $I("tblErrores").rows[i].cells[5].innerText : $I("tblErrores").rows[i].cells[5].textContent;
                sb.Append("<td style='width:235px'>" + sTexto + "</td>");

                sTexto = (ie) ? $I("tblErrores").rows[i].cells[6].innerText : $I("tblErrores").rows[i].cells[6].textContent;
                sb.Append("<td style='width:90px'>" + sTexto + "</td>");

                sTexto = (ie) ? $I("tblErrores").rows[i].cells[7].innerText : $I("tblErrores").rows[i].cells[7].textContent;
                sb.Append("<td style='width:30px'>" + sTexto + "</td>");
                sTexto = (ie) ? $I("tblErrores").rows[i].cells[8].innerText : $I("tblErrores").rows[i].cells[8].textContent;
                sb.Append("<td style='width:220px'>" + sTexto + "</td>");
            }

            sb.Append("</tr>");
        } 
*/        
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function excel2() {
    try {
        if ($I("tblErroresGrab") == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;

        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
        sb.Append("     <td style='width:70px'>Nº Línea</TD>");
        sb.Append("     <td style='width:860px'>Error</TD>");
        sb.Append(" </tr>");

        for (var i = 0; i < $I("tblErroresGrab").rows.length; i++) {
            sb.Append($I("tblErroresGrab").rows[i].outerHTML);
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel2", e.message);
    }
}
function setImputacion() {
    try {
        var sOL = getRadioButtonSelectedValue("rdbImputacion", false);
        if (sOL == "D")
            $I("fls").innerHTML = $I('sCab1').value + $I('sPie1').value;
        else
            $I("fls").innerHTML = $I('sCab2').value + $I('sPie2').value;

        setExcelImg("imgExcel", "divErrores");
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el tipo de imputación.", e.message);
    }
}
function mostrar() {
    try {
        var sOL = getRadioButtonSelectedValue("rdbImputacion", false);
        if (sOL == "D")
            $I('estructura_dia').style.visibility = 'visible';
        else
            $I('estructura_masiva').style.visibility = 'visible';
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la estructura del fichero de entrada.", e.message);
    }
}
function ocultar() {
    try {
        var sOL = getRadioButtonSelectedValue("rdbImputacion", false);
        if (sOL == "D")
            $I('estructura_dia').style.visibility = 'hidden';
        else
            $I('estructura_masiva').style.visibility = 'hidden';

    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar la estructura del fichero de entrada.", e.message);
    }
}