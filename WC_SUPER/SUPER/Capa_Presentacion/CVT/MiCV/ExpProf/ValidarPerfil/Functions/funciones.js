var aACT, aACS, aET;
//var idNuevo = 1;
var bHayCambios = false;
var bExpProfModificable = true;
/*

var oImgFN = document.createElement("img");
oImgFN.setAttribute("src", "../../../../images/imgFN.gif");
oImgFN.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

var oImgFI = document.createElement("img");
oImgFI.setAttribute("src", "../../../../images/imgFI.gif");
oImgFI.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

*/
var oCV = document.createElement("select");
oCV.setAttribute("class", "combo");

function init() {
    try {
        if (!mostrarErrores()) return;
        if ($I("hdnModo").value == "R") {
            setOp($I("btnNewET"), 30);
            setOp($I("btnDelET"), 30);
        }
        $I("txtFI").setAttribute("valAnt", $I("txtFI").value);
        $I("txtFF").setAttribute("valAnt", $I("txtFF").value);
        if ($I("hdnEstadoInicial").value == "R") {
            $I("txtFI").onclick = "";
            $I("txtFF").onclick = "";
        }
        if ($I("txtCliente") != null) {
            acCli.onSelect = function() { onSelect(acCli); };
            acCli.onValueChange = function() { onValueChange(acCli); };
            if ($I("filCliente").style.display != "none")
                swm($I("txtCliente"));
            
        }
        if ($I("txtEmpresaC") != null) {
            acEmpC.onSelect = function() { onSelect(acEmpC); };
            acEmpC.onValueChange = function() { onValueChange(acEmpC); };
            if ($I("filEmpresa").style.display!="none")
                swm($I("txtEmpresaC"));
        }

        if ($I("txtClienteP") != null) {
            acCliP.onSelect = function() { onSelect(acCliP); };
            acCliP.onValueChange = function() { onValueChange(acCliP); };
            if ($I("filClienteP").style.display != "none")
                swm($I("txtClienteP"));
        }

        //if ($I("hdnOrigen").value == "expIber") 
        //{
        //    if (es_DIS || sMOSTRAR_SOLODIS == "0") mostrarMensajesTareasPendientes(sTareasPendientes);
        //}
        //mostrarMsgCumplimentar(sMsgCumplimentar);
        mostrarMsgCumplimentar($I("hdnMsgCumplimentar").value);

        desActivarGrabar();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarMsgCumplimentar(sMsg) {
    try {
        if (sMsg != "") {
            //if ($I("hdnEsMiCV").value == "S") {
                mmoff("warper", sMsg, 445);
            //}
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el mensaje de pendiente de cumplimentar", e.message);
    }
}
//function mostrarMensajesTareasPendientes(strDatos) {
//    var aResul = strDatos.split("@##@");
//    var sMensaje = "";
//    for (var i = 0; i <= aResul.length - 1; i++) {
//        if (aResul[i] != "") {
//            var aArgs = aResul[i].split("#dato#");
//            if ($I("hdnEsMiCV").value == "S" || es_DIS) 
//            {
//                if (aArgs[2] == "5" && aArgs[3] != "")    // Caso:5 (Si el CV  ya ha sido dado de alta en CVT y tiene información pendiente de cumplimentar)
//                {
//                    sMensaje += "\n\n- Debes corregir/modificar el registro, y enviarlo a validar para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
//                    sMensaje += "\n\n  Motivo por el que está pendiente de cumplimentar:";
//                    sMensaje += "\n\n" + Utilidades.unescape(aArgs[4]);
//                }
//            }
//        }
//    }
//    if (sMensaje != "") mmoff("warper", sMensaje, 445);

//}
function activarGrabar1() {
    try {
        if ($I("hdnModo").value == "R") return;
        if (!bExpProfModificable) return;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function activarGrabar() {
    try {
        if ($I("hdnModo").value == "R") return;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
    }
}
function contFechI() {
    controlarFecha('I')
}
function contFechF() {
    controlarFecha('F')
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                bHayCambios = true;
                desActivarGrabar();
                if ($I("hdnEPF").value == "-1") {//Hemos creado un profesional nuevo en una experiencia profesional
                    $I("hdnEPF").value = aResul[2];
                }
                if ($I("hdnEFP").value == "-1") {//Hemos creado un perfil nuevo en un profesional de una experiencia profesional
                    $I("hdnEFP").value = aResul[3];
                }
                salirValidacion();
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

function grabar() {
    try {

        if (!comprobarDatos()) return;
        if ($I("hdnTipo") != null) {
            if (!comprobarDatosEP()) return;
        }
        
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        var sAux = datosGenericos();
        sb.Append(sAux + "@#@");
        //Entornos tecnologicos
        sAux = datosET();
        sb.Append(sAux);
        if ($I("hdnTipo")!=null){ //Validar Perfil (Grabar Datos Experiencia Profesional
            var sEP = datosExperienciaProfesional();
            sb.Append("@#@");
            sb.Append(sEP + "@#@");
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function datosGenericos() {
    var sb = new StringBuilder;
    sb.Append($I("hdnEP").value + "##");
    sb.Append($I("hdnEPF").value + "##");
    sb.Append($I("hdnEFP").value + "##");
    sb.Append($I("hdnProf").value + "##");
    sb.Append("1##"); //sb.Append(($I("chkVisibleCV").checked) ? "1##" : "0##"); 
    sb.Append($I("txtFI").value + "##");
    sb.Append($I("txtFF").value + "##");
    sb.Append($I("txtFun").value + "##");
    sb.Append($I("txtObs").value + "##");
    sb.Append($I("hdnEstadoNuevo").value + "##");
    sb.Append($I("txtMotivoRT").value + "##");
    sb.Append($I("cboPerfil").value + "##");
    sb.Append($I("hdnEstadoInicial").value + "##");
    sb.Append($I("cboIdioma").value);
    return sb.ToString();
}
function datosET() {
    if ($I("tblET") == null) return;
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblET").rows.length; x++) {
        if ($I("tblET").rows[x].getAttribute("bd") == "I") {
            sb.Append("I##" + $I("tblET").rows[x].getAttribute("id") + "///");
        }
        else {
            if ($I("tblET").rows[x].getAttribute("bd") == "D")
                sb.Append("D##" + $I("tblET").rows[x].getAttribute("id") + "///");
        }
    }
    return sb.ToString();
}
function comprobarDatos() {
    var sFecIni = $I("txtFI").value;
    var sFecFin = $I("txtFF").value;
    if ($I("txtDen").value == "") {
        mmoff("War", "La denominación es obligatoria", 230, 2000);
        return false;
    }
    if (sFecIni == "" && $I("hdnEsAdmin").value == "N" && $I("hdnEstadoNuevo").value != "B") {
        mmoff("War", "La fecha de inicio es obligatoria", 230, 2000);
        return false;
    }
    //La experiencia profesional tiene fechas si viene de SUPER, sino estarán vacías
    if ($I("hdnFechaIni").value != "") {
        if (sFecIni != "" && DiffDiasFechas(sFecIni, $I("hdnFechaIni").value) > 0) {
            mmoff("War", "La fecha de inicio debe ser posterior a la fecha de inicio de la experiencia profesional (" + $I("hdnFechaIni").value + ")", 450, 2300);
            return false;
        }
        if (sFecFin != "" && $I("hdnFechaFin").value != "" && DiffDiasFechas($I("hdnFechaFin").value, sFecIni) > 0) {
            mmoff("War", "La fecha de inicio debe ser anterior a la fecha de fin de la experiencia profesional (" + $I("hdnFechaFin").value + ")", 450, 2300);
            return false;
        }
    }
    if ($I("hdnFechaFin").value != "") {
        if (sFecFin == "") {
            mmoff("War", "La fecha de fin debe ser anterior a la fecha de fin de la experiencia profesional (" + $I("hdnFechaFin").value + ")", 450, 2300);
            return false;
        }
        else {
            if (sFecFin != "" && (DiffDiasFechas(sFecFin, $I("hdnFechaFin").value) < 0)) {
                mmoff("War", "La fecha de fin debe ser anterior a la fecha de fin de la experiencia profesional (" + $I("hdnFechaFin").value + ")", 450, 2300);
                return false;
            }
        }
    }
    //Fecha de inicio  > fecha de fin
    if (sFecFin != "" && !fechasCongruentes(sFecIni, sFecFin) && $I("hdnEstadoNuevo").value != "B") {
        mmoff("War", "La fecha de inicio debe ser anterior a la fecha de fin", 350, 2000);
        return false;
    }
    if ($I("lblFFin") != null) {
        if ($I("lblFFin").style.visibility == "visible") {
            if (sFecFin == "" && $I("hdnEsAdmin").value == "N" && $I("hdnEstadoNuevo").value != "B") {
                mmoff("War", "La fecha de fin es obligatoria, al existir fecha de baja en la experiencia profesional.", 400, 2000);
                return false;
            }
        }
    }
    if ($I("txtFun").value == "" && $I("hdnEsAdmin").value == "N" && $I("hdnEstadoNuevo").value != "B") {
        mmoff("War", "Debes indicar las funciones desempeñadas", 300, 2000);
        return false;
    }
    if ($I("tblET").rows.length == 0 && $I("hdnEsAdmin").value == "N" && $I("hdnEstadoNuevo").value != "B") {
        mmoff("War", "Los entornos tecnológicos/funcionales son obligatorios.", 350, 2000);
        return false;
    }
    if (!hayEnt() && $I("hdnEsAdmin").value == "N" && $I("hdnEstadoNuevo").value != "B") {
        mmoff("War", "El perfil debe tener asociado al menos un Entorno tecnológico/funcional.", 450, 2000);
        return false;
    }
    return true;
}
function hayEnt() {
    bRes = false;
    for (var x = 0; x < $I("tblET").rows.length; x++) {
        if ($I("tblET").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}

function comprobarDatosEP() {

    if ($I("txtDescripcion").value == "") {
        mmoff("War", "La descripcion es obligatoria", 230, 2000);
        return false;
    }
    //Si estoy grabando porque quiero dejar Pdte de Cumplimentar. No exijo algunos campos
    if ($I("hdnEstadoNuevo").value == "S" || $I("hdnEstadoNuevo").value == "T") {
    }
    else {
        if ($I("nbrACS").innerText == "") {
            mmoff("War", "Las áreas de conocimientos sectoriales son obligatorias", 230, 2000);
            return false;
        }
        if ($I("nbrACT").innerText == "") {
            mmoff("War", "Las áreas de conocimientos tecnológicos son obligatorias", 230, 2000);
            return false;
        }
        if ($I("hdnCli").value == "null" && $I("hdnTipo").value == "I") {
            mmoff("War", "El cliente debe de ser uno de los sugeridos", 230, 2000);
            return false;
        }
        if (($I("txtEmpresaC").value == "" || $I("txtEmpresaC").className == "WaterMark") && $I("hdnTipo").value != "I") {
            mmoff("War", "La empresa contratante es obligatoria", 230, 2000);
            return false;
        }
        else {
            if (($I("cboSectorEC").value == "" || $I("cboSectorEC").value == "-1") && $I("hdnTipo").value != "I") {
                mmoff("War", "El sector de la empresa contratante es obligatorio", 230, 2000);
                return false;
            }
            if ($I("cboSegmentoEC").value == "" && $I("hdnTipo").value != "I") {
                mmoff("War", "El segmento de la empresa contratante es obligatorio", 230, 2000);
                return false;
            }
        }
        if (!hayEnt()) {
            mmoff("War", "El perfil debe tener asociado al menos un Entorno tecnológico/funcional.", 450, 2000);
            return false;
        }
    }
    return true;
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
        if ($I("hdnEnIb").value != "S") {
            if ($I("hdnSegmentoC").value == "") {
                mmoff("War", "Debes indicar el segmento de la empresa contratante en la experiencia profesional", 350, 2000);
                return false;
            }
        }
        if ($I("cboPerfil").value == "-1") {
            mmoff("War", "Debes indicar un perfil válido para validar", 300, 2000);
            return false;
        }
        $I("hdnEstadoNuevo").value = "V";
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al validar", e.message);
    }
}
//function pseudovalidar() {
//    try {
//        if ($I("cboPerfil").value == "-1") {
//            mmoff("War", "Debes indicar un perfil válido para pseudovalidar", 300, 2000);
//            return false;
//        }
//        $I("hdnEstadoNuevo").value = ($I("hdnEsAdmin").value == "S") ? "Y" : "X"; //Si el usuario es ECV, "Pseudovalidado (origen ECV)":"Pseudovalidado (origen Validador)"
//        grabar();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al pseudovalidar", e.message);
//    }
//}
function enviar() { //enviar a validar
    try {
        if ($I("hdnEnIb").value != "S") {
            if ($I("hdnSegmentoC").value == "") {
                mmoff("War", "Debes indicar el segmento de la empresa contratante en la experiencia profesional", 400, 2000);
                return false;
            }
        }
        if ($I("cboPerfil").value == "-1") {
            mmoff("War", "Debes indicar un perfil válido para grabar la información", 300, 2000);
            return false;
        }
        $I("hdnEstadoNuevo").value = ($I("hdnEstadoInicial").value == "S") ? "O" : "P"; //Si el estado inicial era Pte. cumplimentar (origen ECV), "Pte. validar (origen ECV)":"Pte. validar (origen Validador)"
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al enviar", e.message);
    }
}
function cumplimentar() { //poner pendiente de cumplimentar
    try {
        //$I("hdnEstadoNuevo").value = ($I("hdnEsAdmin").value == "S") ? "S" : "T"; //Si el usuario es ECV, "Pte. cumplimentar (origen ECV)":"Pte. cumplimentar (origen Validador)"
        /* 05/02/2014: Si el estado inicial es "Pte. validar por el validador", pasa a estado Pte. cumplimentar (origen validador).
        En caso contrario, pasa a Pte. Cumplimentar (origen ECV). */
        $I("hdnEstadoNuevo").value = ($I("hdnEstadoInicial").value == "P") ? "T" : "S"; //Si el usuario es ECV, "Pte. cumplimentar (origen ECV)":"Pte. cumplimentar (origen Validador)"
        if ($I("txtMotivoRT").value.Trim() == "") {
            $I("btnAceptarMotivo").className = "btnH25W95";
            $I("btnCancelarMotivo").className = "btnH25W95";
            $I("divFondoMotivo").style.visibility = "visible";
            $I("txtMotivoRT").focus();
            return;
        }
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
function cancelar() {
    bCambios = false;
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

function salirValidacion() {
    bSalir = false;
    bSaliendo = true;
    //Plantilla. No debe de hacer nada y debe de salir
    if ($I("hdnPlantilla").value != "M") {
        bCambios = false;
        bHayCambios = false;
    }
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            } else {
                bCambios = false;
                salirValidacionContinuar();
            }
        });
    } else salirValidacionContinuar();
}
function salirValidacionContinuar() {
    if (bHayCambios)
        var returnValue = {
            resultado: "OK",
            bDatosModificados: true,
            EFP: $I("hdnEFP").value,
            EPF: $I("hdnEPF").value,
            E: $I("hdnEstadoNuevo").value,
            P: $I("cboPerfil").options[$I("cboPerfil").selectedIndex].innerText,
            FI: $I("txtFI").value,
            FF: $I("txtFF").value
        } //"OK";
    //setTimeout("window.close();", 250);
    modalDialog.Close(window, returnValue);
}
function pedirMotivo(sTipo) {
    try {
        var strEnlace = strServer + "Capa_Presentacion/ECO/Foraneo/MensAdm/Comentario.aspx";
        switch (sTipo) {
            case "C":
                strEnlace += "?strTitulo=Motivo para dejar pendiente&estado=M"
                break;
            case "R":
                strEnlace += "?strTitulo=Motivo de rechazo&estado=M"
                break;
            default:
                strEnlace += "?strTitulo=Motivo&estado=M"
                break;
        }
        modalDialog.Show(sPantalla, self, sSize(450, 250))
            .then(function(ret) {
                if (ret != null) {
                    $I("hdnMotivo").value = Utilidades.unescape(ret);
                }
            });
        window.focus();
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al establecer el motivo", e.message);
    }
}
function getHistorial() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getCronologia/Default.aspx?t=" + codpar('T838_EXPFICEPIPERFILCRONO') + "&k=" + codpar($I("hdnEFP").value);
        modalDialog.Show(strEnlace, self, sSize(640, 400));
        window.focus();
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la cronología", e.message);
    }
}
function nuevoET() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/EntornoTecnologico/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 500))
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    for (var i = 0; i < aElementos.length; i++) {
                        if (aElementos[i] == "") continue;
                        bPonerFila = true;
                        var aDatos = aElementos[i].split("@#@");
                        if (aDatos[0] != "") {
                            for (var x = 0; x < $I("tblET").rows.length; x++) {
                                if ($I("tblET").rows[x].getAttribute("id") == aDatos[0]) {
                                    //alert("Entorno ya incluido");
                                    bPonerFila = false;
                                    break;
                                }
                            }
                            //ponerFila
                            if (bPonerFila) {
                                var oNF = $I("tblET").insertRow(-1);
                                oNF.setAttribute("bd", "I");
                                oNF.setAttribute("style", "height:16px");
                                oNF.attachEvent('onclick', mm);
                                oNF.setAttribute("id", aDatos[0]);

                                var oImgFI = document.createElement("img");
                                oImgFI.setAttribute("src", "../../../../../images/imgFI.gif");
                                oNC1 = oNF.insertCell(-1);
                                oNC1.appendChild(oImgFI);

                                oNF.insertCell(-1);
                                oNF.cells[1].innerText = Utilidades.unescape(aDatos[1]);
                            }
                        }
                    }
                }
            });
        window.focus();
        ocultarProcesando();
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir un entorno", e.message);
    }
}
function eliminar() {
    try {
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        for (var i = aFila.length - 1; i >= 0; i--) {
            if (aFila[i].className == "FS") {
                if (aFila[i].getAttribute("bd") == "I") {
                    $I("tblDatos").deleteRow(i);
                } else {
                    mfa(aFila[i], "D");
                }
            }
        }
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}
function EliminarET() {
    try {
        //mostrarProcesando();
        //recorrer filas seleccionadas y marcar para borrado
        if ($I("tblET") == null) return;
        if ($I("tblET").rows.length == 0) return;
        var sw = 0;
        for (var x = $I("tblET").rows.length-1; x >=0 ; x--) {
            if ($I("tblET").rows[x].className.toUpperCase() == "FS") {
                var sw = 1;
                if ($I("tblET").rows[x].getAttribute("bd") == "I")
                    $I("tblET").deleteRow(x);
                else {
                    mfa($I("tblET").rows[x], "D");
                }
            }
        }
        if (sw == 0) {
            mmoff("Inf", "Selecciona la fila a eliminar", 220);
            return;
        }
        activarGrabar();        
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un entorno", e.message);
    }
}
function controlarFecha(sTipo) {
    //A esta función se le llama desde el onchange de las cajas de texto que llevan calendario 
    //Comprueba si el valor actual es correcto
    try {
        var sFechaAct = "";
        switch (sTipo) {
            case "I":
                sFechaAnt = $I("txtFI").getAttribute("valAnt");
                sFechaAct = $I("txtFI").value;
                //NO DEJO VACIAR LA FECHA DE INICIO 
                if (sFechaAct == "") {
                    mmoff("War", "La fecha de inicio no puede ser vacía", 230, 2000);
                    $I("txtFI").value = sFechaAnt;
                }
                else {
                    if (!fechasCongruentes(sFechaAct, $I("txtFF").value)) {
                        mmoff("War", "La fecha de inicio: " + sFechaAct + "\ndebe ser anterior a la fecha de fin: " + $I("txtFF").value, 230, 2000);
                        $I("txtFI").value = sFechaAnt;
                        $I("txtFI").focus();
                        return;
                    }
                    else {
                        $I("txtFI").setAttribute("valAnt", sFechaAct);
                    }
                }
                break;
            case "F":
                sFechaAnt = $I("txtFF").getAttribute("valAnt");
                sFechaAct = $I("txtFF").value;
                if (!fechasCongruentes($I("txtFI").value, sFechaAct)) {
                    mmoff("War", "La fecha de fin: " + sFechaAct + "\ndebe ser posterior a la fecha de inicio: " + $I("txtFI").value, 230, 2000);
                    $I("txtFF").focus();
                    $I("txtFF").value = sFechaAnt;
                    return;
                }
                else {
                    $I("txtFF").setAttribute("valAnt", sFechaAct);
                }
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar fechas", e.message);
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
    verificarCuentasCVT(object);
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
    verificarCuentasCVT(object);
}

function verificarCuentasCVT(object) {
    try {
        if (object.el.context.id == "txtCliente") {
            if (acCli.selectedIndex != -1) {
                $I("hdnCli").value = acCli.data[acCli.selectedIndex];
                $I("cboSectorC").value = acCli.sector[acCli.selectedIndex];
                setSegmentoCli();
                $I("cboSegmentoC").value = acCli.segmento[acCli.selectedIndex];
                $I("cboSegmentoC").disabled = true;
                $I("cboSectorC").disabled = true;
            } else {
                $I("hdnCli").value = "null";
                if ($I("cboSegmentoC").disabled == true) {
                    $I("cboSegmentoC").value = "";
                    $I("cboSectorC").value = "";
                    $I("cboSegmentoC").disabled = false;
                    $I("cboSectorC").disabled = false;
                }
            }
        }
        else if (object.el.context.id == "txtEmpresaC") {
            if (acEmpC.selectedIndex != -1) {
                $I("hdnEC").value = acEmpC.data[acEmpC.selectedIndex];
                $I("cboSectorEC").value = acEmpC.sector[acEmpC.selectedIndex];
                setSegmentoEC();
                $I("cboSegmentoEC").value = acEmpC.segmento[acEmpC.selectedIndex];
                $I("cboSegmentoEC").disabled = true;
                $I("cboSectorEC").disabled = true;
            } else {
                $I("hdnEC").value = "null";
                if ($I("cboSegmentoEC").disabled == true) {
                    $I("cboSegmentoEC").value = "";
                    $I("cboSectorEC").value = "";
                    $I("cboSegmentoEC").disabled = false;
                    $I("cboSectorEC").disabled = false;
                }
            }
        }
        else if (object.el.context.id == "txtClienteP") {
            if (acCliP.selectedIndex != -1) {
                $I("hdnCliP").value = acCliP.data[acCliP.selectedIndex];
                $I("cboSectorClienteP").value = acCliP.sector[acCliP.selectedIndex];
                setSegmentoCliP();
                $I("cboSegmentoClienteP").value = acCliP.segmento[acCliP.selectedIndex];
                $I("cboSegmentoClienteP").disabled = true;
                $I("cboSectorClienteP").disabled = true;
            } else {
                $I("hdnCliP").value = "null";
                if ($I("cboSegmentoClienteP").disabled == true) {
                    $I("cboSegmentoClienteP").value = "";
                    $I("cboSectorClienteP").value = "";
                    $I("cboSegmentoClienteP").disabled = false;
                    $I("cboSectorClienteP").disabled = false;
                }
            }
        }
        //cambiarParamExamen();
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al verificar cliente contratante", e.message);
    }
}


//Al seleccionar un valor en el combo de Sector cliente hay que recargar el combo de Segmento cliente
function setSegmentoCli() {
    try {
        var idSector = $I("cboSectorC").value
        var iNum = 0;
        $I("cboSegmentoC").length = 0;
        for (var i = 0; i < aSEG_js.length; i++) {
            if (idSector == aSEG_js[i][0]) {
                var op1 = new Option(aSEG_js[i][2], aSEG_js[i][1]);
                $I("cboSegmentoC").options[iNum++] = op1;
            }
        }
        setSegmentoOCli();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los segmentos del sector", e.message);
    }
}
function setSegmentoOCli() {
    try {
        acCli.setOptions({ params: { opcion: 'cuentaCVT', origen: '1'} });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el segmento", e.message);
    }
}

//Al seleccionar un valor en el combo de Sector empresa C hay que recargar el combo de Segmento empresa C
function setSegmentoEC() {
    try {
        var idSector = $I("cboSectorEC").value
        var iNum = 0;
        $I("cboSegmentoEC").length = 0;
        for (var i = 0; i < aSEG_js.length; i++) {
            if (idSector == aSEG_js[i][0]) {
                var op1 = new Option(aSEG_js[i][2], aSEG_js[i][1]);
                $I("cboSegmentoEC").options[iNum++] = op1;
            }
        }
        setSegmentoOEC();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los segmentos del sector", e.message);
    }
}
function setSegmentoOEC() {
    try {
        acEmpC.setOptions({ params: { opcion: 'cuentaCVT', origen: ''} });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el segmento", e.message);
    }
}

//Al seleccionar un valor en el combo de Sector cliente P hay que recargar el combo de Segmento cliente P
function setSegmentoCliP() {
    try {
        var idSector = $I("cboSectorClienteP").value
        var iNum = 0;
        $I("cboSegmentoClienteP").length = 0;
        for (var i = 0; i < aSEG_js.length; i++) {
            if (idSector == aSEG_js[i][0]) {
                var op1 = new Option(aSEG_js[i][2], aSEG_js[i][1]);
                $I("cboSegmentoClienteP").options[iNum++] = op1;
            }
        }
        setSegmentoOCliP();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los segmentos del sector", e.message);
    }
}
function setSegmentoOCliP() {
    try {
        acCliP.setOptions({ params: { opcion: 'cuentaCVT', origen: ''} });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el segmento", e.message);
    }
}

function anadirCON(tipo){
    try {
        mostrarProcesando();
        var cono = "";
        if (tipo == "ACS")
            cono = $I("hdnACS").value;
        else
            cono = $I("hdnACT").value;
            
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/ExpProf/Perfil/getConocimientos.aspx?tipo=" + tipo + "&cono=" + cono;
        modalDialog.Show(strEnlace, self, sSize(760, 530))
            .then(function(ret) {
                if (ret != null) {
                    if (tipo == "ACS") {
                        $I("nbrACS").innerText = ret.desc;
                        $I("hdnACS").value = ret.id;
                    }
                    else {
                        $I("nbrACT").innerText = ret.desc;
                        $I("hdnACT").value = ret.id;
                    }
                    bCambios = true;
                }
            });
        window.focus();
        ocultarProcesando();
        
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir conocimientos", e.message);
    }
}

function datosExperienciaProfesional() {
    try{
        var sb = new StringBuilder;
        sb.Append($I("hdnEP").value + "##");
        sb.Append($I("hdnACS").value + "##");
        sb.Append($I("hdnACT").value + "##");
        sb.Append($I("hdnTipo").value + "##");
        if ($I("hdnTipo").value == "I") {
            sb.Append($I("hdnCli").value + "##");
            sb.Append("null##"); //hdnEC
            sb.Append("null##"); //hdnCliP
            sb.Append("##"); //txtEC
            sb.Append("null##"); //idSegEC
            sb.Append("##"); //txtCliP
            sb.Append("null##"); //idSegCliEP
        }
        else {
            sb.Append("null##"); //hdnCli
            sb.Append($I("hdnEC").value + "##"); //hdnEC
            sb.Append($I("hdnCliP").value + "##"); //hdnCliP
            //txtEC
            if ($I("txtEmpresaC").getAttribute("class") == "WaterMark")
                sb.Append("##");
            else
                sb.Append($I("txtEmpresaC").value + "##");
            sb.Append($I("cboSegmentoEC").value + "##"); //idSegEC

            //txtCliP
            if ($I("txtClienteP").getAttribute("class") == "WaterMark")
                sb.Append("##");
            else
                sb.Append($I("txtClienteP").value + "##");
            sb.Append($I("cboSegmentoClienteP").value + "##"); //idSegCliEP
        }

        sb.Append($I("txtDescripcion").value + "##");
        sb.Append($I("txtDen").value);
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al coger datos Exp Profesional", e.message);
    }
}

function getProyExp() {
    try {
        //        alert($I("hdnEP").value);
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/getProyExperiencia.aspx?ep=" + codpar($I("hdnEP").value);
        modalDialog.Show(strEnlace, self, sSize(700, 510));
        window.focus();
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar los proyectos asociados a la experiencia profesional.", e.message);
    }
}