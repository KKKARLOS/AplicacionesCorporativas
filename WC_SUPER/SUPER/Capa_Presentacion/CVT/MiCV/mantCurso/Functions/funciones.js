var bHayCambios = false;
//var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml";
var extshtm = "htm|html";

//JQuery
var options, ac;
jQuery(function() {
    options = { serviceUrl: '../../../UserControls/AutocompleteData.ashx' };
    ac = $('#' + $I('query').id).autocomplete(options);
    ac.setOptions({ width: 310 });
    ac.setOptions({ minChars: 3 });
    ac.setOptions({ params: { opcion: 'proveedor'} });

});


function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();
        setOnline();
        
//        if ($I("ctl00$hdnRefreshPostback") == null) document.forms[0].appendChild(oRefreshPostback);
//        $I("ctl00$hdnRefreshPostback").value = "S";
//        window.focus();
        
        //$I("txtTitulo").focus();
//        if ($I("hdnOP").value == "1" || $I("hdnOP").value == "3") {
//            var returnValue = {
//                resultado: "OK",
//                bDatosModificados: true,
//                id: $I("hdnIdCurso").value
//                }//"OK";
//                modalDialog.Close(window, returnValue);
//        }

        swm($I("query"));
        if (es_DIS || sMOSTRAR_SOLODIS == "0") mostrarMensajesTareasPendientes(sTareasPendientes);
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
function UnloadValor() {
//    if (nName == 'chrome')
//        if ($I("hdnOP").value == "1")
//            window.returnValue = { resultado: "OK", bDatosModificados: true }//"OK";
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

            case "provinciasPais":
                var aDatos = aResul[2].split("///");
                var j = 1;
                $I("cboProvincia").length = 0;

                var opcion = new Option("", "");
                $I("cboProvincia").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboProvincia").options[j] = opcion;
                    j++;
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
    }
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    //Lo comento porque no se está activando bCambios en el aspx
    //if (bCambios && intSession > 0) {
    //    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    //        if (answer) bEnviar = grabar();
    //        else {
    //            bCambios = false;
    //            salirContinuar();
    //        }
    //    });

    //}
    //else
        salirContinuar();
}
function salirContinuar() {
    var returnValue = null;
    if (bHayCambios) 
        returnValue = $I("hdnIdCurso").value;
    modalDialog.Close(window, returnValue);
}

function comprobarDatos() {
    try 
    {
        //El título es obligatorio siempre
        if ($I("txtTitulo").value.Trim() == "") {
            ocultarProcesando();
            mmoff("War", "La denominación es dato obligatorio.", 300);
            return false;
        }
        //Si no es borrador ni tampoco encargado comprobar el resto de los campos obligatorios
        if ($I("hdnEstadoNuevo").value != "B" && $I("hdnEsEncargado").value == "0") {
            if ($I("txtFIni").value.Trim() == "") {
                ocultarProcesando();
                mmoff("War", "La fecha de inicio del curso es dato obligatorio.", 350);
                return false;
            }
            if ($I("txtFFin").value.Trim() == "") {
                mmoff("War", "La fecha de fin del curso es dato obligatorio.", 350);
                ocultarProcesando();
                return false;
            }
            if ($I("txtHoras").value.Trim() == "") {//Si el encargado de curriculums deja este campo vacio, se insertara un 0 en t574_horasfuera
                ocultarProcesando();
                mmoff("War", "La duracion del curso es dato obligatorio.", 300);
                return false;
            }
            if ($I("cboEntorno").value.Trim() == "") {
                ocultarProcesando();
                mmoff("War", "El entorno es dato obligatorio.", 250);
                return false;
            }
            if ($I("txtContenido").value.Trim() == "") {
                ocultarProcesando();
                mmoff("War", "El contenido del curso es dato obligatorio.", 300);
                return false;
            }

            if ($I("cboProvincia").value == "" && !$I("chkOnline").checked) {
                ocultarProcesando();
                mmoff("War", "Debes indicar la provincia o marcar Online.", 300);
                return false;
            }
            //if ($I("query").value == "" || $I("query").className == "WaterMark") {
            if ($I("query").value.Trim() == "" || $I("query").value == "Ej: MICROSOFT IBERICA,S.R.L.") {
                ocultarProcesando();
                mmoff("War", "El centro de obtención del curso es dato obligatorio.", 380, 1500);
                return false;
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
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
    }
}
function grabarVisibleCV() {
    try {
        mostrarProcesando();
        //$I("hdnOP").value = "3";
        //document.forms["formCurso"].submit();
        var js_args = "grabarVisibleCV@#@" + $I("hdnIdCurso").value + "@#@" + $I("hdnIdFicepi").value + "@#@";
        if ($I("chkVisibleCV").checked)
            js_args += "S";
        else
            js_args += "N";
        RealizarCallBack(js_args, ""); 
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer la visibilidad del curso en el CV", e.message);
    }
}

function grabar() {
    try {
        mostrarProcesando();
        if (!comprobarDatos()) return;
        if ($I("hdnEstadoNuevo").value != "B" && $I("hdnEsEncargado").value == "0") {
            if ($I("txtNombreDocumento").value == "") {//Si el combo esta deshabilitado, se trata de un titulo ya existente que ya posee documento y que sólo podrá modificarlo por otro, por lo que no nos tenemos que preocupar en mirar si el campo está vacio o no
                ocultarProcesando();
                if ($I("txtObservaciones").value.Trim() == "") {
                    mmoff("War", "El documento acreditativo de la titulación es dato obligatorio, a menos que se justifique su ausencia en el campo de \"Observaciones\".", 350, 5000);
                }
                else {
                    ocultarProcesando();
                    jqConfirm("", "El documento acreditativo de la titulación es dato obligatorio.<br><br>Si has justificado tu ausencia en el campo de \"Observaciones\" pulsa \"Aceptar\", en caso contrario pulsa \"Cancelar\" e indica el motivo.", "", "", "war", 450).then(function (answer) {
                        if (answer) LlamarGrabar();
                    });
                }
            }
            else LlamarGrabar();
        }
        else LlamarGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar-1", e.message);
    }
}
function LlamarGrabar() {
    try {
        //$I("hdnOP").value = "1";
        //document.forms["formCurso"].submit();
        var js_args = "grabar@#@" + $I("hdnIdFicepi").value + "@#@";
        js_args += $I("hdnIdCurso").value + "@#@";
        js_args += Utilidades.escape($I("txtTitulo").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombreDocumento").value) + "@#@";
        js_args += $I("hdnUsuTick").value + "@#@";
        js_args += $I("txtFIni").value + "@#@";
        js_args += $I("txtFFin").value + "@#@";
        js_args += $I("cboProvincia").value + "@#@";
        js_args += $I("txtHoras").value + "@#@";
        if ($I("query").className == "WaterMark")
            js_args += "@#@"; //Sin Proveedor
        else
            js_args += $I("query").value + "@#@"; //Proveedor
        js_args += $I("cboEntorno").value + "@#@";
        js_args += Utilidades.escape($I("txtContenido").value) + "@#@";
        js_args += Utilidades.escape($I("txtObservaciones").value) + "@#@";
        if ($I("rdbTecn").checked)
            js_args += "N@#@";
        else
            js_args += "S@#@";
        js_args += $I("hdnEstadoNuevo").value + "@#@";
        js_args += Utilidades.escape($I("txtMotivoRT").value) + "@#@";
        if ($I("chkOnline").checked)
            js_args += "S@#@";
        else
            js_args += "N@#@";
        if ($I("chkVisibleCV").checked)
            js_args += "S@#@";
        else
            js_args += "N@#@";
        js_args += $I("hdnEsMiCV").value + "@#@";
        js_args += $I("hdnCambioDoc").value + "@#@"; 
        js_args += $I("hdnEstadoInicial").value + "@#@";
        js_args += $I("hdnContentServer").value;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar-2", e.message);
    }
}

function verDOC() {

    try {
        var re = new RegExp("^.+\.(" + extshtm + ")$", "i");
        if (!re.test($I("hdnndoc").value)) {
            if ($I("hdnIdCurso").value == "-1")
                descargar('CVTCUR', $I("hdnUsuTick").value + "datos" + $I("hdnIdFicepi").value);
            else
                descargar('CVTCUR', $I("hdnIdCurso").value + "datos" + $I("hdnIdFicepi").value);
        }
        else
            descargarhtm('CVTCUR', $I("hdnIdCurso").value + "datos" + $I("hdnIdFicepi").value);
    } catch (e) {
        mostrarErrorAplicacion("Error en la función verDOC", e.message);

    }
}


//NUEVO
//function uploadDocumento(sTipo) {
//    try {
//        if ($I("hdnIdCurso").value == "-1") {
//            nuevoDoc("CURSOR", sIDDocuAux, $I("hdnIdFicepi").value);
//        } else {
//        if ($I("hdnContentServer").value == "")
//                nuevoDoc("CURSOR", $I("hdnIdCurso").value, $I("hdnIdFicepi").value);
//            else
//                modificarDoc("CURSOR", $I("hdnIdCurso").value, $I("hdnIdFicepi").value);
//        }

//    } catch (e) {
//        mostrarErrorAplicacion("Error al ejecutar la función uploadDocumento", e.message);
//    }
//}
function uploadDocumento(sTipo) {
    try {
        nuevoDoc("CURSOR", sIDDocuAux, $I("hdnIdFicepi").value);
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al ejecutar la función uploadDocumento", e.message);
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
    try {//Si el usuario es ECV, "Pseudovalidado (origen ECV)":"Pseudovalidado (origen Validador)"
        $I("hdnEstadoNuevo").value = ($I("hdnEsEncargado").value == "1") ? "Y" : "X"; 
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al pseudovalidar", e.message);
    }
}
function enviar() { //enviar a validar
    try {//Si el estado inicial era Pte. cumplimentar (origen ECV), "Pte. validar (origen ECV)":"Pte. validar (origen Validador)"
        $I("hdnEstadoNuevo").value = ($I("hdnEstadoInicial").value == "S") ? "O" : "P"; 
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

function getHistorial() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getCronologia/Default.aspx?t=" + codpar('ASICRONO') + "&k=" + codpar($I("hdnIdCurso").value + "@@" + $I("hdnIdFicepi").value);
        modalDialog.Show(strEnlace, self, sSize(640, 400));
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el historial de estados.", e.message);
    }
}
function setOnline() {
    try {
        //Si el origen no es CVT (3) todos los campos estarán deshabilitados por lo que no tiene sentido habilitar/deshabilitar
        //el combo de provincias en función de si está marcado el check OnLine o no
        if ($I("hdnOrigen").value == "3") {
            if ($I("chkOnline").checked) {
                $I("cboPais").value = "";
                $I("cboPais").disabled = true;
                $I("cboProvincia").value = "";
                $I("cboProvincia").disabled = true;
            }
            else {
                $I("cboPais").disabled = false;
                $I("cboProvincia").disabled = false;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer Online.", e.message);
    }
}
function setBotonGrabar() {
    try {
        if ($I("hdnOrigen").value != "3")
            $I("btnGrabarVisibleCV").setAttribute("style","display:inline-block");
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer visibilidad del botón Grabar.", e.message);
    }
}
function obtenerProvinciasPais(sPais) {
    try {
        if (sPais == "") {
            $I("cboProvincia").length = 1;
            return;
        }

        var js_args = "provinciasPais@#@";
        js_args += sPais;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenerProvinciasPais ", e.message);
    }
}