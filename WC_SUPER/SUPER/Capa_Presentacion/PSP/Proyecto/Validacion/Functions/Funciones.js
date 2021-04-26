//var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml";
var exts = "xml";
//var exts = ".*"; //Acepta todas las extensiones.

function init() {
    //alert("EsPostBack=" + EsPostBack);
    setOp($I("btnImportar"), 30);
    if (hayConsumos) {
        //$I("fldImport").style.visibility = "visible";
        $I("chkEstr").checked = false;
        $I("chkEstr").disabled = true;
        //$I("chkEstr").style.visibility = "hidden";
    }
    if (EsPostBack) {
        aceptar();
        initMas();
    }
    else {
        if (!semaforo(false)) {
            setOp($I("btnImportar"), 30);
            ocultarProcesando();
            //alert("La estructura del fichero no es válida en SUPER.\nRevisa en OpenProj las filas marcadas.");
            jqConfirm("", "La estructura del fichero no es válida en SUPER.<br />Revisa en OpenProj las filas marcadas.<br /><br />¿Deseas ver un resumen de los errores?", "", "", "war", 450).then(function (answer) {
                if (answer) mmoff("Err", "Relación de errores:\n\n" + $I("hdnError").value, 400);
                initMas();
            });
        }
        else {
            setOp($I("btnImportar"), 100);
            initMas();
        }
    }

}
function initMas() {
    window.focus();
    ocultarProcesando();
    if ($I("hdnResul").value != "" && $I("hdnResul").value != "OK") {
        mmoff("WarPer", $I("hdnResul").value, 400);
        $I("hdnResul").value = "OK";
    }
}
function comprobarExt(value) {
    if (value == "") return true;
    var re = new RegExp("^.+\.(" + exts + ")$", "i");
    if (!re.test(value)) {
        mmoff("InfPer", "Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones prohibidas son: " + extsTexto + " \n\n", 550);//exts.replace(/\|/g, ',')
        //frmUpload.txtDescripcion.value="";
        frmUpload.txtArchivo.value = "";
        //setOp($I("tblAceptar"), 100);
        return false;
    }
    else
        validar();
    return true;
}

function validar() {
    $I("hdnAccion").value = "V";
    if (frmUpload.txtArchivo.value == "") {
        mmoff("inf","Seleccione un fichero",190);
        setOp($I("btnImportar"), 100);
        return;
    }
    //if (!comprobarExt(frmUpload.txtArchivo.value)) return;
    mostrarProcesando();
    //Si no estamos en ejecutando en local o extranet (y se va a subir un archivo), que muestre la barra de progreso.
    var strURL = location.href.toLowerCase();
    if (strURL.indexOf("localhost") == -1 && strURL.indexOf("https") == -1 && frmUpload.txtArchivo.value != "") uploadpop();
//    else if (frmUpload.txtArchivo.value != "") {
//        try {
//            var fso = new ActiveXObject("Scripting.FileSystemObject");
//            var nLength = fso.GetFile(frmUpload.txtArchivo.value).Size;
//            if (nLength > 26214400) {//25Mb, en bytes.
//                ocultarProcesando();
//                setOp($I("btnImportar"), 100);
//                alert("¡Denegado! Se ha seleccionado un archivo mayor del máximo establecido en 25Mb.");
//                return;
//            }
//        } catch (e) {
//            //Para el caso en que el usuario indique No a la ventana del sistema que solicita permiso para ejecutar ActiveX
//            ocultarProcesando();
//            setOp($I("btnImportar"), 100);
//            alert("Para poder exponer ficheros, su navegador en las políticas de seguridad debe permitir \n\"Inicializar y activar la secuencia de comandos de los\ncontroles de ActiveX no marcados como seguros\".");
//            return;
//        }
//    }
    frmUpload.submit();
}
function importar() {
    $I("hdnAccion").value = "I";
    if (getOp($I("btnImportar")) == 30) return;
    setOp($I("btnImportar"), 30);
    
    frmUpload.submit();
}
function aceptar() {
    var returnValue = $I("hdnResul").value;
    modalDialog.Close(window, returnValue);
}
function cerrarVentana() {
    var returnValue = $I("hdnResul").value; //null;
    modalDialog.Close(window, returnValue);
}

function semaforo(bCalcTotales) {
    var sTipo, sError="", sDesc="";
    var bResultado = true, bPrimer=true;
    var swVacio = 0;
    var aFilaT = FilasDe("tblDatos");

    try {
        for (var i = 0; i < aFilaT.length; i++) {
        
            //empadrarFilas(i);
            //buscarIDPadres(i);
            bLineaOk = true;
            sDesc = aFilaT[i].cells[0].innerText;
            aFilaT[i].setAttribute("sColor","Black");
            if (aFilaT[i].getAttribute("sc") == 1) {
                aFilaT[i].cells[0].children[1].style.color = "Black";
            }
            sTipo = aFilaT[i].getAttribute("tipo");
            bHayInfo = true;

            sMargen = aFilaT[i].getAttribute("mar");
            nMargen = Number(sMargen);
            switch (sTipo) {
                case "P":
                    if (nMargen != 0){
                        bLineaOk = false;
                        sError+="El proyecto técnico " + sDesc + " no tiene una identación correcta.\n";
                    }
                    break;
                case "F":
                    if (i == 0){
                        bLineaOk = false;
                        sError+="La fase " + sDesc + " no puede ser el primer elemento de la estructura.\n";
                    }
                    if (nMargen != 20) {
                        bLineaOk = false;
                        sError+="La fase " + sDesc + " no tiene la identación correcta.\n";
                    }
                    if (bLineaOk) {//Una fase nunca puede ser la última linea de un desglose
                        if (i == aFilaT.length - 1){
                            bLineaOk = false;
                            sError+="El último elemento de la estructura no puede ser una fase.\n";
                        }
                        else {//De una fase debe colgar al menos una actividad
                            iFilaSig = i + 1;
                            if (iFilaSig >= aFilaT.length) { 
                                bLineaOk = false; 
                                sError+="De la fase " + sDesc + " debe colgar al menos una actividad.\n";
                            }
                            else {
                                sMargenAux = aFilaT[iFilaSig].getAttribute("mar");
                                nMargenAux = Number(sMargenAux);

                                if ((aFilaT[iFilaSig].getAttribute("tipo") != "A") || (nMargenAux != nMargen + 20)) {
                                    bLineaOk = false;
                                    sError+="De la fase " + sDesc + " debe colgar al menos una actividad.\n";
                                }
                            }
                        }
                    }
//                    if (bLineaOk) {//Una fase debe colgar de un Proy. Técnico
//                        iPadre = i - 1;
//                        if (iPadre < 0) { 
//                            bLineaOk = false; 
//                            sError+="La fase " + sDesc + " debe colgar de un proyecto técnico.\n";
//                        }
//                        else {
//                            if (aFilaT[iPadre].tipo != "P") {
//                                bLineaOk = false;
//                                sError+="La fase " + sDesc + " debe colgar de un proyecto técnico.\n";
//                            }
//                        }
//                    }
                    break;
                case "A":
                    if (i == 0){
                        bLineaOk = false;
                        sError+="La actividad " + sDesc + " no puede ser el primer elemento de la estructura.\n";
                    }
                    if (nMargen != 20 && nMargen != 40) {//Las actividades deben estar identadas a 20 o 40 
                        bLineaOk = false;
                        sError+="La actividad " + sDesc + " no tiene una identación correcta.\n";
                    }
                    if (bLineaOk) {//Una actividad nunca puede ser la última linea de un desglose
                        if (i == aFilaT.length - 1) {
                            if (bHayInfo) {
                                bLineaOk = false;
                                sError+="El último elemento de la estructura no puede ser una actividad.\n";
                            }
                        }
                        else {//De una actividad debe colgar al menos una tarea
                            if (bHayInfo) {
                                iFilaSig = i + 1;
                                if (iFilaSig >= aFilaT.length) {
                                    bLineaOk = false;
                                    sError+="De la actividad " + sDesc + " debe colgar al menos una tarea.\n";
                                }
                                else {
                                    sMargenAux = aFilaT[iFilaSig].getAttribute("mar");
                                    nMargenAux = Number(sMargenAux);
                                    if ((aFilaT[iFilaSig].getAttribute("tipo") != "T") || (nMargenAux != nMargen + 20)) {
                                        bLineaOk = false;
                                        sError+="De la actividad " + sDesc + " debe colgar al menos una tarea.\n";
                                    }
                                }
                            }
                        }
                    }
//                    if (bLineaOk) {//Una actividad debe colgar de un Proy. Técnico o de una Fase
//                        iPadre = i - 1;
//                        if (iPadre < 0) { 
//                            bLineaOk = false; 
//                            sError+="La actividad " + sDesc + " debe colgar de un proyecto técnico o de una fase.\n";
//                        }
//                        else {
//                            if (aFilaT[iPadre].tipo != "P" && aFilaT[iPadre].tipo != "F"){
//                                bLineaOk = false;
//                                sError+="La actividad " + sDesc + " debe colgar de un proyecto técnico o de una fase.\n";
//                            }
//                        }
//                    }
                    if (bLineaOk) {//La anterior linea de una ACTIVIDAD no puede ser una tarea con identación menor
                        iFilaAnt = i - 1;
                        if (aFilaT[iFilaAnt].getAttribute("tipo") == "T") {
                            sMargenAux = aFilaT[iFilaAnt].getAttribute("mar");
                            nMargenAux = Number(sMargenAux);
                            if (nMargenAux < nMargen) {
                                bLineaOk = false;
                                sError+="La línea anterior a la actividad " + sDesc + " no puede ser una tarea con identación menor.\n";
                            }
                        }
                    }
                    break;
                case "T":
                    if (i == 0){
                        bLineaOk = false;
                        sError+="La tarea " + sDesc + " no puede ser el primer elemento de la estructura.\n";
                    }
                    else//no es la primera fila
                    {
                        if (nMargen != 20 && nMargen != 40 && nMargen != 60){
                            bLineaOk = false;
                            sError+="La tarea " + sDesc + " no tiene una identación correcta.\n";
                        }
                        else {
                            iFilaAnt = i - 1;
                            if (iFilaAnt < 0) {
                                bLineaOk = false;
                                sError+="La tarea " + sDesc + " debe colgar de un proyecto técnico o de una actividad.\n";
                            }
                            else {//El padre de una tarea no puede ser una fase
                                if (aFilaT[iFilaAnt].getAttribute("tipo") == "F") {
                                    bLineaOk = false;
                                    sError+="La actividad " + sDesc + " debe colgar de un proyecto técnico o de una fase.\n";
                                }
                                else {
                                    sMargenAux = aFilaT[iFilaAnt].getAttribute("mar");
                                    nMargenAux = Number(sMargenAux);
                                    if (((aFilaT[iFilaAnt].getAttribute("tipo") == "P") || (aFilaT[iFilaAnt].getAttribute("tipo") == "A")) &&
                                     (nMargenAux < nMargen - 20)) {//La identación de su padre debe ser un punto superior
                                        bLineaOk = false;
                                        sError+="La identación del padre de la tarea " + sDesc + " debe ser un punto superior.\n";
                                    }
                                    else if (aFilaT[iFilaAnt].getAttribute("tipo") == "A" && nMargenAux == nMargen) {//La identación de su padre debe ser un punto superior
                                        bLineaOk = false;
                                        sError+="La identación del padre de la tarea " + sDesc + " debe ser un punto superior.\n";
                                    }
                                    else {
                                        if ((aFilaT[iFilaAnt].getAttribute("tipo") == "T") && (nMargenAux < nMargen)) {//Una tarea no puede colgar de otra tarea
                                            bLineaOk = false;
                                            sError+="La tarea " + sDesc + " no puede colgar de otra tarea.\n";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (bLineaOk) {//Una tarea debe colgar de un Proy. Técnico o de una Actividad o estar debajo de otra tarea
                        iPadre = i - 1;
                        if (iPadre < 0){
                            bLineaOk = false;
                            sError+="La tarea " + sDesc + " debe colgar de un proyecto técnico o de una actividad o estar debajo de otra tarea.\n";
                        }
                        else if (aFilaT[iPadre].getAttribute("tipo") != "P" && aFilaT[iPadre].getAttribute("tipo") != "A" && aFilaT[iPadre].getAttribute("tipo") != "T") {
                            bLineaOk = false;
                            sError+="La tarea " + sDesc + " debe colgar de un proyecto técnico o de una actividad o estar debajo de otra tarea.\n";
                        }
                    }
                    break;
                default:
                    bLineaOk = false;
                    sError += "El elemento " + sDesc + " no tiene especificado un valor válido en el campo WBS.\n";
            } //switch 
            swVacio = 0;
            if (aFilaT[i].cells[0].innerText == ""){
                bLineaOk = false;
                if (bPrimer){
                    bPrimer=false;
                    sError+="Existe algún elemento sin denominación.\n";
                }
            }
            if (bLineaOk)
                $I("hdnError").value = "";
            else {
                bHayError = true;
                bResultado = false;
                $I("hdnError").value = sError;
                //                if (swVacio == 0) {
                //                    aFilaT[i].sColor = "Red";
                //                    if (aFilaT[i].sc) aFilaT[i].cells[0].children[2].style.color = "Red";
                //                }
                aFilaT[i].className = "FR"; //Marco las filas erróneas en rojo
            }
           
        }
        //if (bCalcTotales) calcularTotales();
        $I("hdnError").value = sError;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar el semáforo.", e.message);
        bResultado = false;
    }
//    if (bResultado && bHayMorado) bResultado = false;
    return bResultado;
}
