//cambioDoc = false;
//var exts = "doc|docx|txt|pdf|html|htm|pps|odt|jpg|gif|xls"; //extension de los archivos permitidos
var primeraVez = true;
var bHayCambios = false;
//JQuery
var options, acDenom;
$(function() {
    options = {
        serviceUrl: strServer + "Capa_Presentacion/UserControls/AutocompleteData.ashx",
        minChars: 3
    };

    acDenom = $('#' + $I('txtDenom').id).autocomplete(options);
    acDenom.setOptions({ width: 330 });
    acDenom.setOptions({ params: { opcion: 'examen', datos: $I("hdnEntCert").value + "@#@" + $I("hdnEntorno").value + "@#@" + $I("hdnIdCertificado").value, idFicepi: $I("hdnIdFicepi").value, valido: true} });
 
});

function init() {
    if (!mostrarErrores()) return;
    ocultarProcesando();
//    if ($I("hdnOP").value == "1") {
//        var returnValue = {
//            resultado: "OK",
//            idExamen: $I("hdnIdExamen").value,
//            nombre: $I("txtDenom").value,
//            fObtencion: $I("txtFechaO").value,
//            fCaducidad: $I("txtFechaC").value,
//            url: $I("hdnNomDoc").value,
//            estado: $I("hdnAccion").value,
//            motivo: Utilidades.escape($I("hdnMotivo").value),
//            cambioDoc: ($I("hdnCambioDoc").value) ? "1" : "0"
//        }
//        modalDialog.Close(window, returnValue);
//        return;
//    }
//    else if ($I("hdnOP").value == "2") {
//        alert("¡Denegado! Se ha seleccionado un archivo mayor del máximo establecido en 25Mb.");
    //    }

    //Inicializar el objeto JQuery
    if ($I("txtDenom").value != "") {
        //acDenom.getSuggestions($I("txtDenom").value);
        //acDenom.activate(0);
        //acDenom.select(0);
        $I(acDenom.mainContainerId).style.display = "none";
    }

    acDenom.onSelect = function() { onSelect(acDenom); };
    acDenom.onValueChange = function() { onValueChange(acDenom); };

//    acDenom.suggest = function() { suggest(); };
//    if ($I("txtDenom").value != "") {
//        acDenom.getSuggestions($I("txtDenom").value);
//        acDenom.activate(0);
//        acDenom.select(0);
//    } 
//    else
//        primeraVez = false;
//    acDenom.setOptions({ params: { opcion: 'examen', datos: $I("hdnEntCert").value + "@#@" + $I("hdnEntorno").value + "@#@" + $I("hdnIdCertificado").value, idFicepi: $I("hdnIdFicepi").value, valido: true} });


//    if ($I("ctl00$hdnRefreshPostback") == null) document.forms[0].appendChild(oRefreshPostback);
//    $I("ctl00$hdnRefreshPostback").value = "S";
    window.focus();
    swm($I("txtDenom"));
    if (es_DIS || sMOSTRAR_SOLODIS == "0")
        mostrarMensajesTareasPendientes(sTareasPendientes);

    mostrarMsgCumplimentar($I("hdnMsgCumplimentar").value);

    if ($I("txtDenom").disabled == false)
        $I("txtDenom").focus();
}
function mostrarMsgCumplimentar(sMsg) {
    try {
        if (sMsg != "") {
            mmoff("warper", sMsg, 445);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el mensaje de pendiente de cumplimentar", e.message);
    }
}

function onSelect(object) {
    var me, fn, s, d;
    me = object;
    i = me.selectedIndex;
    fn = me.options.onSelect;
    s = me.suggestions[i];
    d = me.data[i];
    me.el.val(me.getValue(s));
    if ($.isFunction(fn)) { fn(s, d, me.el); }
    getDatosExamen();
}

function onValueChange(object) {
    $I(acDenom.mainContainerId).style.display = "block";
    clearInterval(object.onChangeInterval);
    object.currentValue = object.el.val().Trim();
    var q = object.getQuery(object.currentValue);
    object.selectedIndex = -1;
    if (object.ignoreValueChange) {
        object.ignoreValueChange = false;
        return;
    }
    if (q === '' || q.length < object.options.minChars || q.indexOf('%') != -1) {
        object.hide();
    } else {
        object.getSuggestions(q);
    }
    getDatosExamen();
}
function getDatosExamen() {
    var sAux = "";
    try {
        if (acDenom.selectedIndex != -1) {
            $I("hdnIdExamen").value = acDenom.data[acDenom.selectedIndex];
        } else {
            $I("hdnIdExamen").value = "-1";
        }
        //cambiarParamBusq();
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al verificar el examen", e.message);
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
                bCambios = false;
                bHayCambios = true;
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                //if (bSalir) 
                setTimeout("salir()", 50); ;
                break;
            case "documentos":
                $I("txtNombreDocumento").value = aResul[2];
                if (aResul[3] == "S") {//Documento grabado en Atenea
                    $I("hdnUsuTick").value = "";
                    $I("hdnContentServer").value = aResul[4];
                }
                else {
                    $I("hdnContentServer").value = "";
                    $I("hdnUsuTick").value = aResul[4];
                }
                $I("imgUploadDoc").style.display = "inline-block";
                $I("imgDownloadDoc").style.display = "inline-block";
                $I("imgBorrarDoc").style.display = "inline-block";
                $I("hdnCambioDoc").value = true;
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
    }
}

function mostrarMensajesTareasPendientes(strDatos) {
    var aResul = strDatos.split("@##@");
    var sMensaje = "";
    for (var i = 0; i <= aResul.length - 1; i++) {
        if (aResul[i] != "") {
            var aArgs = aResul[i].split("#dato#");
            if ($I("hdnEsMiCV").value == "S" || es_DIS) 
            {            
                if (aArgs[2] == "5" && aArgs[3] != "")    // Caso:5 (Si el CV  ya ha sido dado de alta en CVT y tiene información pendiente de cumplimentar)
                {
                    sMensaje += "\n\n- Debes corregir/modificar el registro, y enviarlo a validar para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                    sMensaje += "\n\n  Motivo por el que está pendiente de cumplimentar:";
                    sMensaje += "\n\n" + Utilidades.unescape(aArgs[4]);
                }
            }
        }
    }
    if (sMensaje != "") mmoff("warper", sMensaje, 445);

}
function getHistorial() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getCronologia/Default.aspx?t=" + codpar('T595_FICEPIEXAMENCRONO') + "&k=" + codpar($I("hdnIdExamen").value + "@@" + $I("hdnIdFicepi").value);
        modalDialog.Show(strEnlace, self, sSize(640, 400));
        window.focus();
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la cronología", e.message);
    }
}
function UnloadValor() {
    if (nName == 'chrome')
        if ($I("hdnOP").value == "1")
            window.returnValue = {resultado: "OK",bDatosModificados: true}//"OK";
}
function cargarDocumento(sTipo) {
    try {
        nuevoDoc("EXAM", sIDDocuAux, $I("hdnIdFicepi").value);
//        if ($I("hdnIdExamenOld").value == "-1") {
//            nuevoDoc("EXAM", sIDDocuAux, $I("hdnIdFicepi").value);
//        } else {
//        if ($I("hdnIdExamenOld").value == "")
//                nuevoDoc("EXAM", $I("hdnIdExamen").value, $I("hdnIdFicepi").value);  
//            else
//                modificarDoc("EXAM", $I("hdnIdExamen").value, $I("hdnIdFicepi").value);
//        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ejecutar la función cargarDocumento", e.message);
    }
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer)
                bEnviar = grabar("P", "");
            else {
                bCambios = false;
                continuarSalir()
            }
        });
    } else continuarSalir();
}
function continuarSalir() {
    var returnValue = "F";
    if (bHayCambios) returnValue = "T";
    modalDialog.Close(window, returnValue);
}

function aparcar() {
    try {
        grabar("B", "");
    } catch (e) {
        mostrarErrorAplicacion("Error al aparcar", e.message);
    }
}

function enviar() { //enviar a validar
    try {
        grabar("P", "");
    } catch (e) {
        mostrarErrorAplicacion("Error al enviar", e.message);
    }
}
function cumplimentar() { //poner pendiente de cumplimentar
    try {
        grabar("T", $I("txtMotivoRT").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al ", e.message);
    }
}
function AceptarMotivo() {
    try {
        if ($I("txtMotivoRT").value.Trim() == "") {
            mmoff("War", "Debes indicar un motivo.", 180);
            return;
        }

        $I("divFondoMotivo").style.visibility = "hidden";
        cumplimentar("T", $I("txtMotivoRT").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar la obtención de motivo.", e.message);
    }
}
function CancelarMotivo() {
    try {
        $I("txtMotivoRT").value = "";
        $I("divFondoMotivo").style.visibility = "hidden";
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar la obtención de motivo.", e.message);
    }
}


function grabar(accion, mot) {
    try {
        mostrarProcesando();
        if (!comprobarDatos(accion)) return false;
        var js_args = "grabar@#@" + $I("hdnIdFicepi").value + "@#@";
        js_args += $I("hdnIdExamenOld").value + "@#@"; //Id examen Old
        js_args += $I("hdnIdExamen").value + "@#@"; //Id examen New
        js_args += Utilidades.escape($I("txtNombreDocumento").value) + "@#@";
        js_args += $I("hdnUsuTick").value + "@#@";
        js_args += $I("txtFechaO").value + "@#@";
        js_args += $I("txtFechaC").value + "@#@";
        js_args += accion + "@#@";
        js_args += Utilidades.escape(mot);

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return true;
        
//        var sUrl = "";
//        if ($I("txtFileDocumento").value == "" && $I("txtNombreDocumento").value != "")
//            sUrl = $I("txtNombreDocumento").value;
//        else
//            sUrl = $I("txtFileDocumento").value;
//        window.returnValue = {
//            resultado: "OK",
//            idExamen: $I("hdnIdExamen").value,
//            nombre: $I("txtDenom").value,
//            fObtencion: $I("txtFechaO").value,
//            fCaducidad: $I("txtFechaC").value,
//            url: sUrl,//$I("txtFileDocumento").value,
//            estado: accion,
//            motivo: Utilidades.escape(mot),
//            cambioDoc: (cambioDoc) ? "1" : "0"
//        }
        //document.forms["formDetExamen"].submit();
//        $I("hdnOP").value = "1";
//        $I("hdnAccion").value = accion;
//        $I("hdnMotivo").value = mot;
//        var theform = document.forms[0];
//        theform.submit();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}

function comprobarDatos(estado) {
    try {
        if ($I("hdnIdExamen").value == "-1") {
            if (acDenom.selectedIndex != -1) {
                if (acDenom.data[acDenom.selectedIndex] != null) {
                    $I("hdnIdExamen").value = acDenom.data[acDenom.selectedIndex];
                }
            }
        }
        //No permitimos generar examenes nuevos. Debe seleccionar uno existente
        if ($I("hdnIdExamen").value == "-1") {
            ocultarProcesando();
            mmoff("War", "Debes elegir un nombre de examen registrado en el sistema.", 400, 2500);
            return false;
        }
        //El título y el documento son obligatorios siempre
        //if ($I("txtDenom").className == "WaterMark") {
        if ($I("txtDenom").value.Trim() == "" || $I("txtDenom").value == "Ej: SQL FOR BEGGINERS") {
            ocultarProcesando();
            mmoff("War", "Debes elegir un nombre de examen.", 340, 2500);
            return false;
        }
        //Como el autocomplete busca a partir del tercer caracter, obligamos a que la denominación tenga al menos 3 caracteres
        if ($I("txtDenom").value.Trim().length < 3) {
            ocultarProcesando();
            mmoff("War", "El nombre del examen debe tener al menos tres caracteres.", 400, 2500);
            return false;
        }
        if (estado != "B" && estado != "T" && $I("txtNombreDocumento").value == "") {
            ocultarProcesando();
            mmoff("War", "El documento acreditativo del examen es dato obligatorio", 420, 2500);
            return false;
        }
        if ((acDenom.selectedIndex != -1)) {//Si hemos desplegado el autocomplete
            if (acDenom.suggestions[acDenom.selectedIndex] != null && 
                acDenom.suggestions[acDenom.selectedIndex] != $I("txtDenom").value) {//Pero no hemos seleccionado un valor
                ocultarProcesando();
                mmoff("War", "Debes elegir un nombre de examen.", 340, 2500);
                return false;
            }
        }
        //Si no es borrador ni tampoco encargado comprobar la fecha de obtención del examen
        //if (estado != "B" && estado != "T" && $I("hdnEsEncargado").value == "0") {
        if (estado != "B" && estado != "T") {

            if ($I("txtFechaO").value == "") {
                ocultarProcesando();
                mmoff("War", "Debes elegir una fecha de obtención", 380, 2500);
                return false;
            }
        }
        //Si se envía a cumplimentar, rellenar el motivo si anteriormente no lo tuviese
        if ($I("txtMotivoRT").value.Trim() == "" && (estado == "S" || estado == "T")) {
            $I("btnAceptarMotivo").className = "btnH25W95";
            $I("btnCancelarMotivo").className = "btnH25W95";
            $I("divFondoMotivo").style.visibility = "visible";
            $I("txtMotivoRT").focus();
            ocultarProcesando();
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
    }
}

function verDOC() {
    try {
        tipo = 'CVTEXAMEN';
        if ($I("hdnIdExamenOld").value == "-1")
            id = $I("hdnUsuTick").value + "datos" + $I("hdnIdFicepi").value;
        else {
            if ($I("hdnCambioDoc").value=="false")
                id = $I("hdnIdExamenOld").value + "datos" + $I("hdnIdFicepi").value;
            else
                id = $I("hdnUsuTick").value + "datos" + $I("hdnIdFicepi").value;
        }
        descargar(tipo, id);
    } catch (e) {
        mostrarErrorAplicacion("Error en la función ver documento", e.message);
    }
}

function verDOC2(url) {
    try {
        id = url + "datos" + $I("txtNombreDocumento").value;
        tipo = 'CVTEXAMEN2';
        descargar(tipo, id);
    } catch (e) {
        mostrarErrorAplicacion("Error en la función ver documento", e.message);
    }
}

function suggest() {
    if (acDenom.suggestions.length === 0) {
        acDenom.hide();
        return;
    }

    var me, len, div, f, v, i, s, mOver, mClick;
    me = acDenom;
    len = acDenom.suggestions.length;
    f = acDenom.options.fnFormatResult;
    v = acDenom.getQuery(acDenom.currentValue);
    mOver = function(xi) { return function() { me.activate(xi); }; };
    mClick = function(xi) { return function() { me.select(xi); }; };
    acDenom.container.hide().empty();
    for (i = 0; i < len; i++) {
        s = acDenom.suggestions[i];
        div = $((me.selectedIndex === i ? '<div class="selected"' : '<div') + ' title="' + s + '">' + f(s, acDenom.data[i], v) + '</div>');
        div.mouseover(mOver(i));
        div.click(mClick(i));
        acDenom.container.append(div);
    }
    acDenom.enabled = true;
    if(!primeraVez)
        acDenom.container.show();
    primeraVez = false;
}
function buscarExamen() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/mantCertificado/SolicitudExamen/Default.aspx?t=" + codpar("E") + 
                        "&c=" + codpar($I("hdnIdCertificado").value) +
                        "&f=" + codpar($I("hdnIdFicepi").value) +
                        "&te=" + codpar($I("hdnHayExamenes").value);
        modalDialog.Show(sPantalla, self, sSize(700, 430))
            .then(function(ret) {
                if (ret != null) {
                    if (ret == "OK") {
                        modalDialog.Close(window, null);
                    }
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de solicitud de examen", e.message);
    }
}
function deleteDocumento(sTipo) {
    try {
        switch (sTipo) {
            case "doc":
                $I("hdnCambioDoc").value = true;
                $I("txtNombreDocumento").value = "";
                $I("hdnContentServer").value = "";
                //$I("txtFileDocumento").parentNode.innerHTML = $I("txtFileDocumento").parentNode.innerHTML;
                $I("imgUploadDoc").style.display = "inline-block";
                $I("imgDownloadDoc").style.display = "none";
                $I("imgBorrarDoc").style.display = "none";
                break;
        }
        //cambioDoc = true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar el archivo", e.message);
    }
}
