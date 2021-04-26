var gsFile = "";

function init() {
    try {
        if (ie)
            $I("btnVisualizar").style.visibility = "visible";
        ToolTipBotonera("procesar", "Procesa las filas correctas del fichero cargado");
        ocultarProcesando();

        if ($I("hdnErrores").value != "") {
            AccionBotonera("procesar", "D");
            return;
        }

        if ($I("hdnIniciado").value == "T") {
            if ($I("cldLinProc").innerText == $I("cldLinOK").innerText && $I("cldLinProc").innerText != "0") {
                AccionBotonera("procesar", "H");
                mmoff("SucPer","Información del fichero de entrada correcta\n\nPulsa el botón procesar", 350);
            }
            else {
                AccionBotonera("procesar", "D");
                mmoff("Err", "Se han detectado errores en el proceso verificación", 340);
            }
        }

        setExcelImg("imgExcel", "divErrores");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function HabCarga() {
    try {
        AccionBotonera("procesar", "D");
        $I("cldLinProc").innerText = "0";
        $I("cldLinOK").innerText = "0";
        $I("cldLinErr").innerText = "0";
        mmoff("Inf", "Se necesita analizar el fichero de entrada antes de procesarlo.", 390);
        setOp($I("btnVisualizar"), 100);
        setOp($I("btnCargar"), 100);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al habilitar los botones", e.message);
    }
}

function Visualizar() {
    try {
        if (ie) {
            var shell = new ActiveXObject("WScript.Shell");
            shell.run("notepad.exe " + $I("uplTheFile").value, 1, false);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a visualizar el fichero", e.message);
    }
}

function procesar() {
    var js_args = "";
    try {
        if (tblErrores.rows.length != 0) return;
        js_args = "procesar@#@";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos", e.message);
        return false;
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
                $I("cldLinProc").innerText = "0";
                $I("cldLinOK").innerText = "0";
                $I("cldLinErr").innerText = "0";
                ocultarProcesando();

                mmoff("SucPer", "Proceso finalizado correctamente", 230);

                break;
            default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function LeerFichero() {
    try {
        //var theform;
        //		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
        //theform = document.forms[0];
        //		}
        //		else {
        //			theform = document.forms["frmDatos"];
        //		}
        document.forms[0].submit();
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
        var tblErrores = $I("tblErrores");
        if (tblErrores == null) {
            ocultarProcesando();
            mmoff("War","No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:auto'>" + sNodo + "</TD>");
        sb.Append("        <td style='width:auto'>Proyecto</TD>");
        sb.Append("        <td style='width:auto'>Año/Mes</TD>");
        sb.Append("        <td style='width:auto'>Clase</TD>");
        sb.Append("        <td style='width:auto'>Motivo</TD>");
        sb.Append("        <td style='width:auto'>Proveedor</TD>");
        sb.Append("        <td style='width:auto'><LABEL title='" + sNodo + " Destino'>" + sNodo + "</LABEL></TD>");
        sb.Append("        <td style='width:auto'>Importe</TD>");
        sb.Append("        <td style='width:auto'>Error</TD>");
        sb.Append("	</TR>");
        for (var i = 0; i < tblErrores.rows.length; i++) {
           sb.Append(tblErrores.rows[i].outerHTML);
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

