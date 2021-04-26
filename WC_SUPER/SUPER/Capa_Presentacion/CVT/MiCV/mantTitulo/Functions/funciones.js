//var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml";
var extshtm = "htm|html";
var bHayCambios = false;

//JQuery
var options, acCent, acIdioma;
jQuery(function() {
    options = { serviceUrl: '../../../UserControls/AutocompleteData.ashx' };
    acCent = $('#' + $I('query').id).autocomplete(options);
    acCent.setOptions({ width: 305 });
    acCent.setOptions({ minChars: 3 });
    acCent.setOptions({ params: { opcion: 'tituloIdiomaCentro'} });

    acIdioma = $('#' + $I('txtTitulo').id).autocomplete(options);
    acIdioma.setOptions({ width: 305 });
    acIdioma.setOptions({ minChars: 3 });
    acIdioma.setOptions({ params: { opcion: 'tituloIdioma', idcodidioma: $I("hdnIdCodIdioma").value} });
    
});

function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();

        //if (!$I("txtTitulo").disabled)
        //$I("txtTitulo").focus();
        
//        var returnValue = null;
//        if ($I("hdnOP").value == "1") {
//            returnValue = {
//                iT: $I("hdnIdTitulo").value,
//                nDoc: $I("hdnndoc").value,
//                dT: $I("txtTitulo").value,
//                dC: $I("query").value,
//                fecha: $I("txtFecha").value,
//                E: $I("hdnEstadoNuevo").value,
//                resultado: "OK",
//                bDatosModificados: true,
//                id: $I("hdnIdTitulo").value
//            }
//            modalDialog.Close(window, returnValue);
//            return;
//        }
        swm($I("query"));
        swm($I("txtTitulo"));
        if (es_DIS || sMOSTRAR_SOLODIS == "0") 
            mostrarMensajesTareasPendientes(sTareasPendientes);
        mostrarMsgCumplimentar($I("hdnMsgCumplimentar").value);
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
                if (aArgs[2] == "5" && aArgs[3] != "")   // Caso:5 (Si el CV  ya ha sido dado de alta en CVT y tiene información pendiente de cumplimentar)
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
function salir() {
    bSalir = false;
    bSaliendo = true;
    //Lo comento porque no se está activando bCambios en el aspx
    //if (bCambios && intSession > 0) {
    //    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    //        if (answer)
    //            bEnviar = grabar();

    //        bCambios = false;
    //        continuarSalir()
    //    });
    //}
    //else
        continuarSalir();
}
function continuarSalir() {
    var returnValue = null;
    if (bHayCambios) returnValue = $I("hdnIdTitulo").value;
    modalDialog.Close(window, returnValue);
}
function cerrarVentana() {
    try {
        if (bProcesando()) return;

//        var returnValue = "No";
//        if (opener != undefined) {
//            if ($I("hdnPantalla").value == "frmDatos")//Viene de MiCV
//                opener.document.forms[0][4].value = "No";
//            else//Viene de MantIdiomas
//                opener.document.form1.hdnReturnValue.value = "No";
//        }
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function comprobarDatos() {
    try {
        //El idioma es obligatorio
        if ($I("hdnIdCodIdioma").value == "") {
            ocultarProcesando();
            mmoff("War", "El idioma es dato obligatorio.", 300);
            return false;
        }
        //El título es obligatorio siempre
        //if ($I("txtTitulo").className == "WaterMark") {
        if ($I("txtTitulo").value.Trim() == "" || $I("txtTitulo").value == "Ej: First Certificate") {
            ocultarProcesando();
            mmoff("War", "La denominación es dato obligatorio.", 300);
            return false;
        }
        //Si no es borrador ni tampoco encargado comprobar el resto de los campos obligatorios
        if ($I("hdnEstadoNuevo").value != "B" && $I("hdnEsEncargado").value == "0") {
            if ($I("txtFecha").value.Trim() == "") {
                ocultarProcesando();
                mmoff("War", "La fecha de obtención del titulo es dato obligatorio.", 350, 2000);
                return false;
            }
            //El título es obligatorio (María 16/10/2013)
            //if ($I("query").className == "WaterMark") {
            if ($I("query").value.Trim() == "" || $I("query").value == "Ej: Cambridge Schools Centre") {
                ocultarProcesando();
                mmoff("War", "El centro es dato obligatorio.", 300);
                return false;
            }
        }
        if ($I("txtMotivoRT").value.Trim() == "" && ($I("hdnEstadoNuevo").value == "S" || $I("hdnEstadoNuevo").value == "T")) {
            $I("btnAceptarMotivo").className = "btnH25W95";
            $I("btnCancelarMotivo").className = "btnH25W95";
            $I("divFondoMotivo").style.visibility = "visible";
            $I("txtMotivoRT").focus();
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
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
            case "grabarVisibleCV":
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
function grabar() {
    try {
        if (!comprobarDatos()) return;

        if ($I("txtNombreDocumento").value.Trim() == "" && $I("hdnEsEncargado").value != "1") {
            if ($I("txtObservaciones").value.Trim() == "") {
                ocultarProcesando();
                mmoff("War", "El documento acreditativo de la titulación es dato obligatorio, a menos que justifique su ausencia en el campo de \"Observaciones\".", 350, 5000);
                return;
            } else {
                    jqConfirm("", "¡ Atención !<br><br>El documento acreditativo de la titulación es dato obligatorio.<br><br>Si has justificado tu ausencia en el campo de \"Observaciones\" pulsa \"Aceptar\", en caso contrario pulsa \"Cancelar\" e indica el motivo.", "", "", "war", 450).then(function (answer) {
                        if (answer) LlamarGrabar();
                    });
            }
        } else LlamarGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar-1", e.message);
    }
}
function LlamarGrabar() {
    try {
            var js_args = "grabar@#@" + $I("hdnIdTitulo").value + "@#@";
            js_args += $I("hdnIdFicepi").value + "@#@";
            js_args += $I("hdnIdCodIdioma").value + "@#@";
            js_args += Utilidades.escape($I("txtTitulo").value) + "@#@";
            js_args += $I("txtFecha").value + "@#@";
            js_args += Utilidades.escape($I("txtObservaciones").value) + "@#@";
            if ($I("query").className == "WaterMark")//Centro
                js_args += "@#@"; 
            else
                js_args += $I("query").value + "@#@";
            js_args += Utilidades.escape($I("txtNombreDocumento").value) + "@#@";
            js_args += $I("hdnCambioDoc").value + "@#@";
            js_args += $I("hdnUsuTick").value + "@#@";
            js_args += $I("hdnEstadoNuevo").value + "@#@";
            js_args += Utilidades.escape($I("txtMotivoRT").value) + "@#@";
            js_args += $I("hdnEstadoInicial").value + "@#@";
            js_args += $I("hdnContentServer").value + "@#@";
            js_args += $I("hdnEsMiCV").value;

            mostrarProcesando();
            RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}

function verDOC() {

    try {
        var re = new RegExp("^.+\.(" + extshtm + ")$", "i");
        if (!re.test($I("hdnndoc").value)) {
            //descargar('TIF', $I("hdnIdTitulo").value);
            if ($I("hdnIdTitulo").value == "-1")
                descargar('TIF', $I("hdnUsuTick").value);
            else
                descargar('TIF', $I("hdnIdTitulo").value);
        }
        else
            descargarhtm('TIF', $I("hdnIdTitulo").value);
    } catch (e) {
        mostrarErrorAplicacion("Error en la función verDOC", e.message);

    }
}
//Nuevo
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
        $I("hdnEstadoNuevo").value = "V";
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al validar", e.message);
    }
}
function pseudovalidar() {
    try {
        $I("hdnEstadoNuevo").value = ($I("hdnEsEncargado").value == "1") ? "Y" : "X"; //Si el usuario es ECV, "Pseudovalidado (origen ECV)":"Pseudovalidado (origen Validador)"
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al pseudovalidar", e.message);
    }
}
function enviar() { //enviar a validar
    try {
        $I("hdnEstadoNuevo").value = ($I("hdnEstadoInicial").value == "S") ? "O" : "P"; //Si el estado inicial era Pte. cumplimentar (origen ECV), "Pte. validar (origen ECV)":"Pte. validar (origen Validador)"
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al enviar", e.message);
    }
}
function cumplimentar() { //poner pendiente de cumplimentar
    try {
        $I("hdnEstadoNuevo").value = ($I("hdnEsEncargado").value == "1") ? "S" : "T"; //Si el usuario es ECV, "Pte. cumplimentar (origen ECV)":"Pte. cumplimentar (origen Validador)"
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

function uploadDocumento(sTipo) {
    try {
        if ($I("hdnIdTitulo").value == "-1") {
            nuevoDoc("TIF", sIDDocuAux, $I("hdnIdFicepi").value);
        } else {
            if ($I("hdnContentServer").value == "")
                nuevoDoc("TIF", $I("hdnIdTitulo").value, $I("hdnIdFicepi").value);
            else
                modificarDoc("TIF", $I("hdnIdTitulo").value, $I("hdnIdFicepi").value);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ejecutar la función uploadDocumento", e.message);
    }
}



function getHistorial() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getCronologia/Default.aspx?t=" + codpar('TITIDIOMAFICCRONO') + "&k=" + codpar($I("hdnIdTitulo").value);
        modalDialog.Show(strEnlace, self, sSize(640, 400));
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el historial de estados.", e.message);
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
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar el archivo", e.message);
    }
}

function UnloadValor() {
//    if (nName == 'chrome') {
//        if ($I("hdnOP").value == "1") {
//            window.returnValue = { resultado: "OK",
//                iT: $I("hdnIdTitulo").value,
//                nDoc: $I("hdnndoc").value,
//                dT: $I("txtTitulo").value,
//                dC: $I("query").value,
//                fecha: $I("txtFecha").value,
//                E: $I("hdnEstadoNuevo").value,
//                resultado: "OK",
//                bDatosModificados: true,
//                id: $I("hdnIdTitulo").value            
//                }
//        }
//    }                              
}