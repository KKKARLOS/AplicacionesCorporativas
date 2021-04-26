var gsFile = "";

function init() {
    try {
        //alert($I("hdnNumfacts").value);//Nº de facturas en T445_INTERFACTSAP
        //$I("lblNumOK").value="";//Nº  de facturas correctas validadas
        AccionBotonera("procesar", "D");
        if ($I("hdnIniciado").value == "T") {
            if ($I("cldFacOK").innerText != "0")
                AccionBotonera("procesar", "H");
            $I("txtMesBase").value = AnoMesToMesAnoDescLong($I("txtAnioMes").value);
        }
        else {
            $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMesActual);
            $I("txtAnioMes").value = nAnoMesActual;
        }
        if ($I("cldFilasIFS").innerText != "0") {
            $I("lblIFS").className = "enlace";
            $I("lblIFS").onclick = function () { mostrarIFS() };
        }

        ToolTipBotonera("procesar", "Procesa las facturas correctas en T445_INTERFACTSAP");

        setExcelImg("imgExcel", "divErrores");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function HabCarga() {
    try {
        mmoff("Inf", "Se necesita analizar el fichero de entrada antes de procesarlo.", 390);

        $I("divErrores").children[0].innerHTML = "";
        $I("cldFacOK").innerText = "0";
        $I("cldFacProc").innerText = "0";
        $I("cldFacErr").innerText = "0";
        AccionBotonera("procesar", "D");
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

    try {
        if ($I("hdnNumfacts").value != "0") {
            ocultarProcesando();
            jqConfirm("", "La tabla intermedia T445_INTERFACTSAP contiene registros que si continúas se eliminarán.<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    procesar2();
                }
            });
        }
        else {
            procesar2();
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos", e.message);
        return false;
    }
}
function procesar2() {
    var js_args = "procesar@#@";
    js_args += $I("txtAnioMes").value + "@#@";
    js_args += flGetCadenaDesglose();
    mostrarProcesando();
    RealizarCallBack(js_args, "");  //con argumentos
}
function flGetCadenaDesglose() {
    /*Recorre la tabla de facturas erróneas para obtener una cadena que se pasará como parámetro
      al procedimiento de grabación. En la cadena figurarán los nº de linea del fichero que no son correctos
    */
    try {
        var sRes = "";
        var aF = FilasDe("tblErrores");
        var sw = 0;
        var sb = new StringBuilder;

        for (var i = 0; i < aF.length; i++) {
            //sRes += aF[i].id+"##";
            sb.Append(aF[i].id + "##");
            sw = 1;
        }//for
        //if (sRes != "") sRes="##"+ sRes;
        if (sw == 1) sRes = "##" + sb.ToString();
        return sRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
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
                $I("cldFilasIFS").innerText = aResul[2];
                if ($I("cldFilasIFS").innerText != "0") {
                    $I("lblIFS").className = "enlace";
                    $I("lblIFS").onclick = function () { mostrarIFS() };
                } else {
                    $I("lblIFS").className = "texto";
                    $I("lblIFS").onclick = null;
                }
                AccionBotonera("procesar", "D");
                mmoff("Suc", "Proceso correcto", 160);
                break;
            case "mostrarIFS":
                if (aResul[2] == "cacheado") {
                    var xls;
                    try {
                        xls = new ActiveXObject("Excel.Application");
                        crearExcel(aResul[4]);
                    } catch (e) {
                        crearExcelSimpleServerCache(aResul[3]);
                    }
                }
                else
                    crearExcel(aResul[2]);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function LeerFichero() {
    try {
        //		var theform;
        //		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
        //			theform = document.forms[0];
        //		}
        //		else {
        //			theform = document.forms["frmDatos"];
        //		}
        /*
                if ($I("uplTheFile").value != "") 
                {
                    try {
                        if (ie) {
                            var fso = new ActiveXObject("Scripting.FileSystemObject");
                            var nLength = fso.GetFile($I("uplTheFile").value).Size;
                            //alert(nLength);
                            if (nLength > 26214400) {//25Mb, en bytes.
                                ocultarProcesando();
                                setOp($I("btnCargar"), 100);
                                alert("¡Denegado! Se ha seleccionado un archivo mayor del máximo establecido en 25Mb.");
                                return;
                            }
                        }
                    } catch (e) {
                        //Para el caso en que el usuario indique No a la ventana del sistema
                        //que solicita permiso para ejecutar ActiveX
                        ocultarProcesando();
                        setOp($I("btnCargar"), 100);
                        alert("Para poder exponer ficheros, su navegador en las políticas de seguridad debe permitir \n\"Inicializar y activar la secuencia de comandos de los\ncontroles de ActiveX no marcados como seguros\".");
                        return;
                    }
                }
        */
        document.forms[0].submit();
    } catch (e) {
        mostrarErrorAplicacion("Error al validar el fichero de facturas de SAP", e.message);
    }
}

function mostrarIFS() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("mostrarIFS@#@");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar INTERFACTSAP", e.message);
    }
}

function excel() {
    try {
        var tblErrores = $I("tblErrores");
        if (tblErrores == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:auto'>Fecha</TD>");
        sb.Append("        <td style='width:auto'>Serie</TD>");
        sb.Append("        <td style='width:auto'>Factura</TD>");
        sb.Append("        <td style='width:auto'>Motivo</TD>");
        sb.Append("	</TR>");
        //		sb.Append("</TABLE>");
        //        
        //        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
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
function cambiarMes(sValor) {
    try {
        switch (sValor) {
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
        }
        switch (sValor) {
            case "A":
                nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
                break;

            case "S":
                nAnoMesActual = AddAnnomes(nAnoMesActual, 1);
                break;
        }
        $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        $I("txtAnioMes").value = nAnoMesActual;
        $I("divErrores").children[0].innerHTML = "";
        $I("cldFacOK").innerText = "0";
        $I("cldFacProc").innerText = "0";
        $I("cldFacErr").innerText = "0";
        AccionBotonera("procesar", "D");
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}
