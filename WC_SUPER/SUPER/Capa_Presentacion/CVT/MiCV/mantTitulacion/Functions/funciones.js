//var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml";
var bHayCambios = false;
var options, acEspl, acCent;
$(function() {
    options = {
        serviceUrl: "../../../UserControls/AutocompleteData.ashx",
        width: 306,
        minChars: 3
    };

    acTit = $("#txtTitulo").autocomplete(options);
    acTit.setOptions({ params: { opcion: 'titulaciones', t001_idficepi: $I("hdnIdficepi").value } });

    acEspl = $('#' + $I("txtEspecialidad").id).autocomplete(options);
    acEspl.setOptions({ params: { opcion: 'tituloEspecialidad', titulo: $I("hdnIdtitulacion").value} });

    acCent = $('#' + $I("txtCentro").id).autocomplete(options);
    acCent.setOptions({ params: { opcion: 'tituloCentro', titulo: $I("hdnIdtitulacion").value} });
});

function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();
        cambiarFin();
        if ($I("hdnIdtitulacion").value != "") {
            acTit.suggestions[0] = $I("txtTitulo").value;
            acTit.data[0] = $I("hdnIdtitulacion").value;
            acTit.selectedIndex = 0;
            cambiarParamBusq(true);
        }
        acTit.onSelect = function() { onSelect(acTit); };
        acTit.onValueChange = function() { onValueChange(acTit); };

        swm($I("txtTitulo"));
        swm($I("txtEspecialidad"));
        swm($I("txtCentro"));

        if (es_DIS || sMOSTRAR_SOLODIS == "0") 
            mostrarMensajesTareasPendientes(sTareasPendientes);

        mostrarMsgCumplimentar($I("hdnMsgCumplimentar").value);

        window.focus();
        ocultarProcesando(); 
        
    } catch (e) {
        mostrarErrorAplicacion("Error en la función init", e.message);
    }
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

function mostrarMensajesTareasPendientes(strDatos) {
    var aResul = strDatos.split("@##@");
    var sMensaje = "";
    for (var i = 0; i <= aResul.length - 1; i++) {
        if (aResul[i] != "") {
            var aArgs = aResul[i].split("#dato#");
            if ($I("hdnEsMiCV").value == "S" || es_DIS) 
            {            
                if (aArgs[2] == "5" && aArgs[3]!="")   // Caso:5 (Si el CV  ya ha sido dado de alta en CVT y tiene información pendiente de cumplimentar)
                {
                    sMensaje += "\n\n- Debes corregir/modificar el registro, y enviarlo a validar para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                    sMensaje += "\n\n  Motivo por el que está pendiente de cumplimentar:";
                    sMensaje += "\n\n" +  Utilidades.unescape(aArgs[4]);
                }
            }
        }
    }
    if (sMensaje != "") mmoff("warper", sMensaje, 445);

}
function cerrarVentana() {
    try {
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    //Lo comento porque no se está activando bCambios en el aspx
    //if (bCambios && intSession > 0) {
    //    var sMsg = "Datos modificados. ¿Deseas grabarlos?";
    //    if ($I("chkFinalizado").checked) {
    //        if ($I("txtNombreDocumento").value.Trim() == "") //Si el combo esta deshabilitado, se trata de un titulo ya existente que ya posee documento y que sólo podrá modificarlo por otro, por lo que no nos tenemos que preocupar en mirar si el campo está vacio o no
    //        {
    //            if ($I("txtObservaciones").value.Trim() != "") {
    //                sMsg="El documento acreditativo de la titulación es dato obligatorio.<br><br>Si has justificado tu ausencia en el campo de \"Observaciones\" pulsa \"Aceptar\", en caso contrario pulsa \"Cancelar\" e indica el motivo.";
    //            }
    //        } 
    //    } 
    //    jqConfirm("", sMsg, "", "", "war", 320).then(function (answer) {
    //        if (answer) {
    //            bEnviar = LlamarGrabar();
    //        }
    //        else {
    //            bCambios = false;
    //            continuarSalir()
    //        }
    //    });
    //}
    //else
        continuarSalir();
}
function continuarSalir() {
    var returnValue = null;
    if (bHayCambios) returnValue = $I("hdnIdTituloficepi").value;
    modalDialog.Close(window, returnValue);
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getDatosTitulo":
                $I("cboTipo").value = aResul[2];
                $I("cboTipo").disabled = true;
                $I("cboModalidad").value = aResul[3];
                $I("cboModalidad").disabled = true;
                $I("chkTIC").checked = (aResul[4]=="1")?true:false;
                $I("chkTIC").disabled = true;
                break;
            case "grabar":
                bCambios = false;
                bHayCambios = true;
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                setTimeout("salir()", 50); ;
                break;
            case "documentos":
                if (aResul[5] == "TAD") {
                    $I("txtNombreDocumento").value = aResul[2];
                    if (aResul[3] == "S") {//Documento grabado en Atenea
                        $I("hdnUsuTickTit").value = "";
                        $I("hdnContentServer").value = aResul[4];
                    }
                    else {
                        $I("hdnContentServer").value = "";
                        $I("hdnUsuTickTit").value = aResul[4];
                    }
                    $I("imgUploadDoc").style.display = "inline-block";
                    $I("imgDownloadDoc").style.display = "inline-block";
                    $I("imgBorrarDoc").style.display = "inline-block";
                    $I("hdnCambioDoc").value = true;
                }
                else {//Es el documento del expediente
                    $I("txtNombreExpediente").value = aResul[2];
                    if (aResul[3] == "S") {//Documento grabado en Atenea
                        $I("hdnUsuTickExpte").value = "";
                        $I("hdnContentServerExpte").value = aResul[4];
                    }
                    else {
                        $I("hdnContentServerExpte").value = "";
                        $I("hdnUsuTickExpte").value = aResul[4];
                    }
                    $I("imgUploadExp").style.display = "inline-block";
                    $I("imgDownloadExp").style.display = "inline-block";
                    $I("imgBorrarExp").style.display = "inline-block";
                    $I("hdnCambioExp").value = true;
                }
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
function verDOC(tipo) {
    try {
        var sTipo = "TAD";
        switch(tipo){
            case "doc":
                if ($I("hdnIdTituloficepi").value == "")
                    id = $I("hdnUsuTickTit").value + "datos" + $I("hdnIdficepi").value;
                else {
                    if ($I("hdnCambioDoc").value == "false")
                        id = $I("hdnIdTituloficepi").value + "datos" + $I("hdnIdficepi").value;
                    else
                        id = $I("hdnUsuTickTit").value + "datos" + $I("hdnIdficepi").value;
                }
                break;
            case "expte":
                sTipo = "TAE";
                if ($I("hdnIdTituloficepi").value == "")
                    id = $I("hdnUsuTickExpte").value + "datos" + $I("hdnIdficepi").value;
                else {
                    if ($I("hdnCambioExp").value == "false")
                        id = $I("hdnIdTituloficepi").value + "datos" + $I("hdnIdficepi").value;
                    else
                        id = $I("hdnUsuTickExpte").value + "datos" + $I("hdnIdficepi").value;
                }
                break;
        }
        descargar(sTipo, id);
    } catch (e) {
        mostrarErrorAplicacion("Error en la función ver documento", e.message);
    }
}

function UnloadValor() {
//    if (nName == 'chrome') {
//        if ($I("hdnOP").value == "1") { 
//            window.returnValue = { resultado: "OK", bDatosModificados: true }//"OK";
//        }
//    }
}

function cambiarParamBusq(bInit) {
    try {
        if (!bInit) {
            if ($I("txtEspecialidad").className != "WaterMark") {
                $I("txtEspecialidad").value = "";
                if (document.all) {
                    $I("txtEspecialidad").fireEvent("onblur");
                } else {
                    var changeEvent = document.createEvent("MouseEvent");
                    changeEvent.initEvent("blur", false, true);
                    $I("txtEspecialidad").dispatchEvent(changeEvent);
                }
            }
            if ($I("txtCentro").className != "WaterMark") {
                $I("txtCentro").value = "";
                if (document.all) {
                    $I("txtCentro").fireEvent("onblur");
                } else {
                    var changeEvent = document.createEvent("MouseEvent");
                    changeEvent.initEvent("blur", false, true);
                    $I("txtCentro").dispatchEvent(changeEvent);
                }
            }
        }
        acEspl.clearCache();
        acCent.clearCache();
        var aux;
        if (acTit.selectedIndex != -1) {
            aux = acTit.data[acTit.selectedIndex];
            deshabilitarDatTit(true.toString());
        }
        else {
            aux = "";
            deshabilitarDatTit(false.toString());
        }
        acEspl.setOptions({ params: { opcion: 'tituloEspecialidad', titulo: aux} });
        acCent.setOptions({ params: { opcion: 'tituloCentro', titulo: aux} });
    } catch (e) {
        mostrarErrorAplicacion("Error en cambiar los parámetros de busqueda", e.message);
    }
}

function deshabilitarDatTit(bool) {
    try {
        $I("cboTipo").setAttribute("disabled", bool);
        $I("cboModalidad").setAttribute("disabled", bool);
        $I("chkTIC").setAttribute("disabled", bool);
    
    } catch (e) {
        mostrarErrorAplicacion("Error al deshabilitar campos propios de la titulación", e.message);
    }
}

function deleteDocumento(sTipo) {
    try {
        switch (sTipo) {
            case "doc":
                $I("hdnCambioDoc").value = true;
                $I("txtNombreDocumento").value = "";
                $I("hdnContentServer").value = "";
                $I("imgUploadDoc").style.display = "inline-block";
                $I("imgDownloadDoc").style.display = "none";
                $I("imgBorrarDoc").style.display = "none";
                break;
            case "exp":
                $I("hdnCambioExp").value = true;
                $I("txtNombreExpediente").value = "";
                $I("hdnContentServerExpte").value = "";
                $I("imgUploadExp").style.display = "inline-block";
                $I("imgDownloadExp").style.display = "none";
                $I("imgBorrarExp").style.display = "none";
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar el archivo", e.message);
    }
}

function cambiarFin() {
    try {
        if ($I("cboInicio").selectedIndex >= $I("cboFin").length) {
            for (var i = $I("cboFin").length; i <= $I("cboInicio").selectedIndex && i > 0; i++) {
                $I("cboFin").appendChild($I("cboInicio").children[i].cloneNode(true));
            }
        } else if ($I("cboInicio").selectedIndex + 1 < $I("cboFin").length) {
            for (var i = $I("cboFin").length - 1; i > $I("cboInicio").selectedIndex; i--) {
                $I("cboFin").remove(i);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función cambiarFin", e.message);
    }
}
function comprobarDatos() {
    try 
    {
        //El título es obligatorio siempre
        //if ($I("txtTitulo").className == "WaterMark" && $I("txtTitulo").value == "Ej: Licenciatura en Informática") {
        if ($I("txtTitulo").value.Trim() == "" || $I("txtTitulo").value == "Ej: Licenciatura en Informática") {
            ocultarProcesando();
            mmoff("War", "El título es dato obligatorio.", 200, 1500);
            return false;
        //Si insertamos titulación nueva, es obligatorio especificar el tipo y la modalidad    
        } else if (acTit.selectedIndex == -1) {
            if ($I("cboTipo").value == "") {
                ocultarProcesando();
                mmoff("War", "El tipo de la titulación es dato obligatorio.", 300, 1500);
                return false;
            }
            if ($I("cboTipo").value == "1" && $I("cboModalidad").value == "") {
                ocultarProcesando();
                mmoff("War", "La modalidad de la titulación es dato obligatorio si el tipo es universitaria.", 350, 1500);
                return false;
            }
        }
        //Si no es borrador ni tampoco encargado comprobar el resto de los campos obligatorios
        if ($I("hdnEstadoNuevo").value != "B" && $I("hdnEsEncargado").value == "0") {

            //if ($I("txtEspecialidad").className == "WaterMark") {
            if ($I("txtEspecialidad").value.Trim() == "" || $I("txtEspecialidad").value == "Ej: Análisis de aplicaciones") {
                ocultarProcesando();
                mmoff("War", "La especialidad es dato obligatorio.", 300, 1500);
                return false;
            }

            //if ($I("txtCentro").className == "WaterMark") {
            if ($I("txtCentro").value.Trim() == "" || $I("txtCentro").value == "Ej: Universidad del País Vasco (UPV) /Euskal Herriko Unibertsitatea (EHU)") {
                ocultarProcesando();
                mmoff("War", "El centro de obtención de la titulación es dato obligatorio.", 380, 1500);
                return false;
            }
            
            if ($I("cboInicio").value == "") {
                ocultarProcesando();
                mmoff("War", "El año de inicio de la titulación es dato obligatorio.", 380, 1500);
                return false;
            }
            if ($I("chkFinalizado").checked) {
                if ($I("cboFin").value == "") {
                    ocultarProcesando();
                    mmoff("War", "El año de fin de la titulación es dato obligatorio.", 380, 1500);
                    return false;
                }
                if ($I("txtNombreDocumento").value.Trim() == "") //Si el combo esta deshabilitado, se trata de un titulo ya existente que ya posee documento y que sólo podrá modificarlo por otro, por lo que no nos tenemos que preocupar en mirar si el campo está vacio o no
                {
                    ocultarProcesando();
                    if ($I("txtObservaciones").value.Trim() == "") {
                        mmoff("War", "El documento acreditativo de la titulación es dato obligatorio, a menos que justifique su ausencia en el campo de \"Observaciones\".", 350, 5000);
                        return false;
                    }
                }
            }
        }
        //Si se envía a cumplimentar, rellenar el motivo si anteriormente no lo tuviese
        if ($I("txtMotivoRT").value.Trim() == "" && ($I("hdnEstadoNuevo").value == "S" || $I("hdnEstadoNuevo").value == "T")) {
            $I("btnAceptarMotivo").className = "btnH25W95";
            $I("btnCancelarMotivo").className = "btnH25W95";
            $I("divFondoMotivo").style.visibility = "visible";
            $I("txtMotivoRT").focus();
            ocultarProcesando();
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos-1", e.message);
    }
}
function grabar() {
    try {
        mostrarProcesando();
        if (!comprobarDatos()) return;
        LlamarGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar-1", e.message);
    }
}
function LlamarGrabar() {
    try {
        //$I("hdnOP").value = "1";
        if (acTit.selectedIndex != -1) {//Titulacion Validada
            if (acTit.data[acTit.selectedIndex] == $I("hdnIdtitulacion").value)
            //No Cambiada. Update de tituloficepi sin cambiar codtitulo
                $I("hdnOPTit").value = "1";
            else {
                //Cambiada por otra validada. Update de tituloficepi cambiando codtitulo
                if ($I("hdnTituloEstadoIni").value == "V")
                    $I("hdnOPTit").value = "2";
                else {//Cambio de No Validada a Validada. Además de updatear borra la titulación sino está en uso
                    $I("hdnOPTit").value = "5";
                }
            }
        }
        else {//Titulacion No Validada
            if ($I("hdnTituloEstadoIni").value == "V")
                //Si la titulacion inicial estaba Validada -> Inserto nuevo Título y le asigno el profesional
                $I("hdnOPTit").value = "3";
            else//Si la titulación inicial NO estaba Validada -> updateo el Titulo
                $I("hdnOPTit").value = "4";
        }
        if (acTit.data[acTit.selectedIndex]!=undefined)
            $I("hdnIdtitulacion").value = acTit.data[acTit.selectedIndex];

        var js_args = "grabar@#@" + $I("hdnIdTituloficepi").value + "@#@";
        js_args += $I("hdnIdficepi").value + "@#@";
        js_args += $I("hdnIdtitulacion").value + "@#@";
        js_args += Utilidades.escape($I("txtTitulo").value) + "@#@";
        js_args += $I("cboTipo").value + "@#@";
        js_args += $I("cboModalidad").value + "@#@";
        if ($I("chkTIC").checked)
            js_args += "S@#@";
        else
            js_args += "N@#@";
        if ($I("chkFinalizado").checked)
            js_args += "S@#@";
        else
            js_args += "N@#@";
        if ($I("txtEspecialidad").className == "WaterMark")
            js_args += "@#@";
        else
            js_args += $I("txtEspecialidad").value + "@#@";
        if ($I("txtCentro").className == "WaterMark")
            js_args += "@#@";
        else
            js_args += $I("txtCentro").value + "@#@";
        js_args += $I("cboInicio").value + "@#@";
        js_args += $I("cboFin").value + "@#@";
        js_args += Utilidades.escape($I("txtNombreDocumento").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombreExpediente").value) + "@#@";
        js_args += Utilidades.escape($I("txtObservaciones").value) + "@#@";
        js_args += $I("hdnEstadoNuevo").value + "@#@";
        js_args += Utilidades.escape($I("txtMotivoRT").value) + "@#@";
        js_args += $I("hdnCambioDoc").value + "@#@";
        js_args += $I("hdnCambioExp").value + "@#@";
        js_args += $I("hdnEstadoInicial").value + "@#@";
        js_args += $I("hdnOPTit").value + "@#@";
        js_args += $I("hdnIdtitulacionIni").value + "@#@";
        js_args += $I("hdnUsuTickTit").value + "@#@";
        js_args += $I("hdnUsuTickExpte").value + "@#@";
        js_args += $I("hdnContentServer").value + "@#@";
        js_args += $I("hdnContentServerExpte").value + "@#@";
        js_args += $I("hdnEsMiCV").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar-2", e.message);
    }
}


function onValueChange(object) {
    clearInterval(object.onChangeInterval);
    object.currentValue = object.el.val();
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
//    cambiarParamBusq(false);
    getDatosTitulo();
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
//    cambiarParamBusq(false);
    getDatosTitulo();
}

function aparcar() {
    try {
        $I("hdnEstadoNuevo").value = "B";
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al aparcar", e.message);
    }
}

function validar() {
    try {
        if ($I("chkFinalizado").checked)
            $I("hdnEstadoNuevo").value = "V";
        else
            $I("hdnEstadoNuevo").value = "B";
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al validar", e.message);
    }
}

function pseudovalidar() {
    try {
        //Si el usuario es ECV, "Pseudovalidado (origen ECV)":"Pseudovalidado (origen Validador)"
        if ($I("chkFinalizado").checked)
            $I("hdnEstadoNuevo").value = ($I("hdnEsEncargado").value == "1") ? "Y" : "X";
        else
            $I("hdnEstadoNuevo").value = "B";
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al pseudovalidar", e.message);
    }
}

function enviar() { //enviar a validar
    try {
        //Si el estado inicial era Pte. cumplimentar (origen ECV), "Pte. validar (origen ECV)":"Pte. validar (origen Validador)"
        if ($I("chkFinalizado").checked)
            $I("hdnEstadoNuevo").value = ($I("hdnEstadoInicial").value == "S") ? "O" : "P";
        else
            $I("hdnEstadoNuevo").value = "B";
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al enviar", e.message);
    }
}

function cumplimentar() { //poner pendiente de cumplimentar
    try {
        //Si el usuario es ECV, "Pte. cumplimentar (origen ECV)":"Pte. cumplimentar (origen Validador)"
        //if ($I("chkFinalizado").checked)
            $I("hdnEstadoNuevo").value = ($I("hdnEsEncargado").value == "1") ? "S" : "T";
        //else
            //$I("hdnEstadoNuevo").value = "B";
        grabar();
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
        cumplimentar();
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

function rechazar() { //poner como no interesante
    try {
        $I("hdnEstadoNuevo").value = "R";
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar que no es interesante", e.message);
    }
}

//function uploadDocumento(sTipo){
//    try {
//        
//        switch (sTipo) {
//            case "doc":
//                if ($I("hdnIdTituloficepi").value == "") {
//                    nuevoDoc("TAD", sIDDocuAuxTit, $I("hdnIdficepi").value);
//                } else {
//                    if ($I("hdnContentServer").value == "")
//                        nuevoDoc("TAD", $I("hdnIdTituloficepi").value, $I("hdnIdficepi").value);
//                    else
//                        modificarDoc("TAD", $I("hdnIdTituloficepi").value, $I("hdnIdficepi").value);
//                }
//                break;
//            case "exp":
//                if ($I("hdnIdTituloficepi").value == "") {
//                    nuevoDoc("TAE", sIDDocuAuxExpte, $I("hdnIdficepi").value);
//                } else {
//                    if ($I("hdnContentServerExpte").value == "")
//                        nuevoDoc("TAE", $I("hdnIdTituloficepi").value, $I("hdnIdficepi").value);
//                    else
//                        modificarDoc("TAE", $I("hdnIdTituloficepi").value, $I("hdnIdficepi").value);
//                }
//                break;
//        }
//    } catch (e) {
//        mostrarErrorAplicacion("Error al ejecutar la función uploadDocumento", e.message);
//    }
//}
function uploadDocumento(sTipo) {
    try {

        switch (sTipo) {
            case "doc":
                nuevoDoc("TAD", sIDDocuAuxTit, $I("hdnIdficepi").value);
                break;
            case "exp":
                nuevoDoc("TAE", sIDDocuAuxExpte, $I("hdnIdficepi").value);
                break;
        }
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al ejecutar la función uploadDocumento", e.message);
    }
}

function getDatosTitulo() {
    try {
        if (acTit.selectedIndex == -1) {
            $I("cboTipo").value = "0";
            $I("cboTipo").disabled = false;
            $I("cboModalidad").value = "0";
            $I("cboModalidad").disabled = false;
            $I("chkTIC").checked = false;
            $I("chkTIC").disabled = false;
        } else {
            var js_args = "getDatosTitulo@#@";
            js_args += acTit.data[acTit.selectedIndex];
            RealizarCallBack(js_args, "");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos del título.", e.message);
    }
}

function getHistorial() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getCronologia/Default.aspx?t=" + codpar('TITULOFICEPICRONO') + "&k=" + codpar($I("hdnIdTituloficepi").value);
        modalDialog.Show(strEnlace, self, sSize(640, 400));
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el historial de estados.", e.message);
    }
}
//23/10/2013 Dice María que sino está marcado como finalizado no se puede enviar a validar
//10/09/2015 Dice María que como solo se muestra el botón Grabar, no hay que hacer nada
function setFinalizado(){
    //if ($I("chkFinalizado").checked) {
    //    if ($I("hdnPermiteEnviarValidar").value=="S")
    //        $I("btnEnviar").style.display = "inline-block";
    //}
    //else {
    //    $I("btnEnviar").style.display = "none";
    //}
}