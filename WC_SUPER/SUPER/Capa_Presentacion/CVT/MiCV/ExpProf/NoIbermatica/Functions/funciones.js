var aACT, aACS, aET;
//var idNuevo = 1;
var bHayCambios = false;
var bExpProfModificable = true;
var bAnadiendoPerfil = false;
var bSalir = false;
//Vbles para acceder al detalle de un perfil cuando la experiencia profesional está pdte de grabar
var bDetalle = false;
var sCaso = "";
var oImgFN = document.createElement("img");
oImgFN.setAttribute("src", "../../../../../images/imgFN.gif");
oImgFN.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; height:16px; width:16px;");

var oImgFN2 = document.createElement("img");
oImgFN2.setAttribute("src", "../../../../../images/imgFN.gif");
oImgFN2.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; height:16px; width:16px;");

var oCV = document.createElement("select");
oCV.setAttribute("class", "combo");

function init() {
    try {
        if (!mostrarErrores()) return;
        acOri.onSelect = function() { onSelect(acOri); };
        acOri.onValueChange = function() { onValueChange(acOri); };
        
        acDes.onSelect = function() { onSelect(acDes); };
        acDes.onValueChange = function() { onValueChange(acDes); };
               
        if ($I("hdnModo").value == "R") {
            setOp($I("btnGrabar"), 30);
            setOp($I("btnNewACS"), 30);
            setOp($I("btnDelACS"), 30);
            setOp($I("btnNewACT"), 30);
            setOp($I("btnDelACT"), 30);
            setOp($I("btnNewPerf"), 30);
        }
        //Si el registro es nuevo, es mi propio Curriculum o soy el Encargado de CVs -> Puedo manejar perfiles
        if ($I("hdnEP").value == "-1" || $I("hdnEsMiCV").value == "S" || $I("hdnEsEncargado").value == "S") {
            setOp($I("btnNewPerf"), 100);
        }
        swm($I("txtEmpresaC"));
        swm($I("txtEmpresaD"));


        desActivarGrabar();
        if ($I("hdnEP").value == "-1") $I("txtDen").focus();
        if (es_DIS || sMOSTRAR_SOLODIS == "0") mostrarMensajesTareasPendientes(Utilidades.unescape(sTareasPendientes));

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
//function saludar(event, ui) {
//    alert("Hola");
//    //event.preventDefault();
//}
function mostrarMensajesTareasPendientes(strDatos) {
    var aResul = strDatos.split("@##@");
    var sMensaje = "";
    for (var i = 0; i <= aResul.length - 1; i++) {
        if (aResul[i] != "") {
            var aArgs = aResul[i].split("#dato#");

            if (i == 0) sCaso = sCaso + aArgs[2];
            else sCaso = sCaso + "," + aArgs[2];

            if ($I("hdnEsMiCV").value == "S" || es_DIS) {
                if (aArgs[2] == "2" && aArgs[3] != "")         // Caso:2 (Asignación de una experiencia al curriculum de un profesional )
                {
                    sMensaje += "¡Atención!\n\n- Debes añadir y enviar a validar un perfil en la experiencia para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                }
                else if (aArgs[2] == "3" && aArgs[3] != "")    // Caso:3 (Asignación de la fecha de baja a un profesional en una experiencia)
                {
                    sMensaje += " \n\n- Al haber finalizado tu participación en la experiencia, debes revisar el contenido de los perfil/es, y enviar a validarlo/s para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                    $I("btnExpRevisada").style.display = "block";
                    $I("tblBotones").style.marginLeft = "300px";
                }
                else if (aArgs[2] == "4" && aArgs[3] != "")   // Caso:4 (Borrar la fecha de baja a un profesional en una experiencia)
                {
                    sMensaje += "\n\n- Al haber sido dado de alta de nuevo en la experiencia, debes modificar la fecha fin del perfil que estimes oportuno o añadir un nuevo perfil, y enviarlo a validar para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                }
                else if (aArgs[2] == "5" && aArgs[3] != "")   // Caso:5,6 (Cuando un tercero (validador, encargado de CV) establece al registro el estado ‘Pdte de cumplimentar’.)
                {
                    sMensaje += "\n\n- Debes corregir/modificar el registro, y enviarlo a validar para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                }
            }
        }
    }
    if (sMensaje != "") mmoff("warper", sMensaje, 690);

}
function activarGrabar1() {
    try {
        if ($I("hdnModo").value == "R") return;
        if (!bExpProfModificable) return;
        //if (!bCambios) {
            setOp($I("btnGrabar"), 100);
            bCambios = true;
        //}
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function activarGrabar() {
    try {
        if ($I("hdnModo").value == "R") return;
        //if (!bCambios) {
            setOp($I("btnGrabar"), 100);
            bCambios = true;
        //}
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function activarGrabar2() {
    try {
        //if (!bCambios) {
            setOp($I("btnGrabar"), 100);
            bCambios = true;
        //}
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        setOp($I("btnGrabar"), 30);
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
        case "delPerfil":
            if ($I("tblDatosPerfiles") != null) {
                aPerf = FilasDe("tblDatosPerfiles");
                for (var j = aPerf.length - 1; j >= 0; j--) {
                    if (aPerf[j].getAttribute("bd") == "D")
                        $I("tblDatosPerfiles").deleteRow(j);
                }
                mmoff("Suc", "Eliminación correcta", 160);
            }
            break;
        case "borrarEP":
            //mmoff("Suc", "Grabación correcta", 160);
            bSalir = true;
            var returnValue = { resultado: "OK", id: $I("hdnEP").value }
            //setTimeout("window.close();", 250);
            modalDialog.Close(window, returnValue);
            break;
        case "getperfil":
            $I("divEnt").children[0].innerHTML = aResul[2];
            mmoff("Suc", "Grabación correcta", 160);
            break;
        case "RevisadaPerfilExper":
            if (aResul[2] == "NOVALIDADO") {
                mmoff("Suc", "Existen perfiles pendientes de revisar", 260);
            }
            else {
                setOp($I("btnExpRevisada"), 30);
                mmoff("Suc", "Validación de la experiencia realizada correctamente", 340);
            }
            break;             
        case "grabar":
                bHayCambios = true;
                desActivarGrabar();
                if ($I("hdnEP").value == "-1") {//Hemos creado una experiencia profesional nueva
                    $I("hdnEP").value = aResul[2];
                    if (aResul[3] != "0") {
                        $I("hdnidExpFicepi").value = aResul[3];
                    }
                    bCambios = false;
                    if (bDetalle) {
                        bDetalle = false;
                        setTimeout("nuevoPerf()", 1000);
                    }
                }
                mmoff("Suc", "Grabación correcta", 170, 2000);
                if (bSalir) {
                    salir();
                    return;
                }
                if ($I("tblConTec") != null) {
                    aACT = FilasDe("tblConTec");
                    for (var j = aACT.length - 1; j >= 0; j--) {
                        if (aACT[j].getAttribute("bd") != "") {
                            if (aACT[j].getAttribute("bd") == "D")
                                $I("tblConTec").deleteRow(j);
                            else
                                mfa(aACT[j], "N");
                        }
                    }
                }
                if ($I("tblConSec") != null) {
                    aACS = FilasDe("tblConSec");
                    for (var k = aACS.length - 1; k >= 0; k--) {
                        if (aACS[k].getAttribute("bd") != "") {
                            if (aACS[k].getAttribute("bd") == "D")
                                $I("tblConSec").deleteRow(k);
                            else
                                mfa(aACS[k], "N");
                        }
                    }
                }
                if ($I("tblET") != null) {
                    aET = FilasDe("tblET");
                    for (var m = aET.length - 1; m >= 0; m--) {
                        if (aET[m].getAttribute("bd") != "") {
                            if (aET[m].getAttribute("bd") == "D")
                                $I("tblET").deleteRow(m);
                            else
                                mfa(aET[m], "N");
                        }
                    }
                }
                if ($I("tblDatosPerfiles") != null) {
                    aPERF = FilasDe("tblDatosPerfiles");
                    for (var m = aPERF.length - 1; m >= 0; m--) {
                        if (aPERF[m].getAttribute("bd") != "") {
                            if (aPERF[m].getAttribute("bd") == "D") $I("tblDatosPerfiles").deleteRow(m);
                            else mfa(aPERF[m], "N");
                        }
                    }
                }
                //Los códigos de cliente los ponemos en positivo y los códigos de cuentasCVT en negativo
                //Si cuenta Ori > 0 se ha insertado en el cuentas y hay que cambiar el id del hidden
                if (parseInt(aResul[4]) > 0) {
                    $I("hdnCtaOri").value = "-" + aResul[4];
                }
                //Si cuenta Des > 0 se ha insertado en el cuentas y hay que cambiar el id del hidden
                if (parseInt(aResul[5]) > 0) {
                    $I("hdnCtaDes").value = "-" + aResul[5];
                }
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

function comprobarDatos() {
    if ($I("txtDen").value.Trim() == "") {
        mmoff("War", "La denominación es obligatoria", 230, 2000);
        return false;
    }
    if ($I("txtDes").value.Trim() == "" && $I("hdnEsEncargado").value != "S") {
        mmoff("War", "La descripción es obligatoria", 230, 2000);
        return false;
    }
    if (!hayAreaConSec() && $I("hdnEsEncargado").value != "S") {
        mmoff("War", "La experiencia profesional debe tener asociado al menos un Area de conocimiento sectorial.", 450, 2000);
        return false;
    }
    if (!hayAreaConTec() && $I("hdnEsEncargado").value != "S") {
        mmoff("War", "La experiencia profesional debe tener asociado al menos un Area de conocimiento tecnologico.", 450, 2000);
        return false;
    }

    if ($I("txtEmpresaC").value.Trim() == "" || $I("txtEmpresaC").value == "Ej: Arcelor Mittal") {
        mmoff("War", "La empresa contratante es obligatoria", 270, 2000);
        return false;
    }
    else
        if (!bECV &&
                (
                  $I("cboSectorC").value == "" ||
                  ($I("cboSectorC").value == "-1" && $I("txtEmpresaC").value.Trim() != "" && $I("txtEmpresaC").value != "Ej: Arcelor Mittal")
                 )
            ) {
        var sTexto = $I("txtEmpresaC").value.toUpperCase();
        var bAsignado = false;
        for (i = 0; i < acOri.data.length; i++) {
            if (acOri.suggestions[i].toUpperCase() == sTexto) {
                $I("hdnCtaOri").value = acOri.data[i];
                $I("cboSectorC").value = acOri.sector[i];
                setSegmentoOri();
                $I("cboSegmentoC").value = acOri.segmento[i];
                $I("cboSegmentoC").disabled = true;
                $I("cboSectorC").disabled = true;
                acOri.selectedIndex = i;
                bAsignado = true;
                break;
            }
        }
        if (!bAsignado) {
            mmoff("War", "El sector de la empresa contratante es obligatorio", 230, 2000);
            return false;
        }
    }
    else if (!bECV && $I("cboSegmentoC").value == "") {
        mmoff("War", "El segmento de la empresa contratante es obligatorio", 230, 2000);
        return false;
    }

    if ($I("txtEmpresaD").value.Trim() != "" && $I("txtEmpresaD").value != "Ej: Arcelor Mittal") {
        if ($I("cboSectorD").value == "" || $I("cboSectorD").value == "-1") {
            var sTexto = $I("txtEmpresaD").value.toUpperCase();
            var bAsignado = false;
            for (i = 0; i < acDes.data.length; i++) {
                if (acDes.suggestions[i].toUpperCase() == sTexto) {
                    $I("hdnCtaDes").value = acDes.data[i];
                    $I("cboSectorD").value = acDes.sector[i];
                    setSegmentoDes();
                    $I("cboSegmentoD").value = acDes.segmento[i];
                    $I("cboSegmentoD").disabled = true;
                    $I("cboSectorD").disabled = true;
                    acDes.selectedIndex = i;
                    bAsignado = true;
                    break;
                }
            }
            if (!bAsignado) {
                mmoff("War", "Si indicas la empresa destino, su sector es dato obligatorio", 320, 2500);
                return false;
            }
        }
        if ($I("cboSegmentoD").value == "") {
            mmoff("War", "Si indicas la empresa destino, su segmento es dato obligatorio", 320, 2500);
            return false;
        }
    }
    return true;
}
function setRevisadaExper() {
    try {
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("RevisadaPerfilExper@#@");
//        sb.Append(sCaso);
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function grabar() {
    try {

        if (!comprobarDatos()) return;
        if (!bAnadiendoPerfil && !hayPerfiles()) {
            jqConfirm("", "La experiencia profesional debe tener asociado al menos un perfil, de lo contrario se perderán los datos de la experiencia profesional.<br><br>Pulsa Aceptar para continuar en la pantalla o Cancelar para salir", "", "", "war", 450).then(function (answer) {
                if (answer) return;
                else {
                    bCambios = false;
                    mostrarProcesando();
                    var sb = new StringBuilder; //sin paréntesis
                    sb.Append("borrarEP@#@" + $I("hdnProf").value + "@#@" + $I("hdnEP").value);
                    RealizarCallBack(sb.ToString(), "");
                }
            });
        } else LLamadaGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar-1.", e.message);
    }
}
function LLamadaGrabar() {
    try {
        var sb = new StringBuilder;
        sb.Append("grabar@#@" + $I("hdnEP").value + "@#@");
        var sAux = datosGenericos();
        sb.Append(sAux + "@#@");
        //Areas de conocimiento tecnologico
        sAux = datosACT();
        sb.Append(sAux + "@#@");
        //Areas de conocimiento sectorial
        sAux = datosACS();
        sb.Append(sAux + "@#@");
        //Perfiles (para borrar)
        sAux = datosPerf();
        sb.Append(sAux);
        sb.Append("@#@" + $I("hdnProf").value);

        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar-2.", e.message);
    }
}
function datosGenericos() {
    var sb = new StringBuilder;
    sb.Append($I("txtDen").value + "##");
    sb.Append($I("txtDes").value + "##");
    //Recojo (si existe) el código del autocomplete
    if ($I("hdnCtaOri").value == "" || $I("hdnCtaOri").value == "-1" || $I("hdnCtaOri").value == "null")
        $I("hdnCtaOri").value = (acOri.selectedIndex != -1) ? acOri.data[acOri.selectedIndex] : "null";
    if ($I("hdnCtaDes").value == "" || $I("hdnCtaDes").value == "-1" || $I("hdnCtaDes").value == "null")
        $I("hdnCtaDes").value = (acDes.selectedIndex != -1) ? acDes.data[acDes.selectedIndex] : "null";
    sb.Append($I("hdnCtaOri").value + "##");
    sb.Append($I("hdnCtaDes").value + "##");
    sb.Append($I("cboSegmentoC").value + "##");
    sb.Append($I("cboSegmentoD").value + "##");

    //if ($I("txtEmpresaC").getAttribute("class") == "WaterMark")
    if ($I("txtEmpresaC").value == "Ej: Arcelor Mittal")
        sb.Append("##");
    else
        sb.Append($I("txtEmpresaC").value + "##");
    //if ($I("txtEmpresaD").getAttribute("class") == "WaterMark")
    if ($I("txtEmpresaD").value == "Ej: Arcelor Mittal")
        sb.Append("##");
    else
        sb.Append($I("txtEmpresaD").value + "##");

    return sb.ToString();
}
function datosACT() {
    if ($I("tblConTec") == null) return;
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblConTec").rows.length; x++) {
        if ($I("tblConTec").rows[x].getAttribute("bd") == "I") {
            sb.Append("I##" + $I("tblConTec").rows[x].getAttribute("id") + "///");
        }
        else {
            if ($I("tblConTec").rows[x].getAttribute("bd") == "D")
                sb.Append("D##" + $I("tblConTec").rows[x].getAttribute("id") + "///");
        }
    }
    return sb.ToString();
}
function datosACS() {
    if ($I("tblConSec") == null) return;
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblConSec").rows.length; x++) {
        if ($I("tblConSec").rows[x].getAttribute("bd") == "I") {
            sb.Append("I##" + $I("tblConSec").rows[x].getAttribute("id") + "///");
        }
        else {
            if ($I("tblConSec").rows[x].getAttribute("bd") == "D")
                sb.Append("D##" + $I("tblConSec").rows[x].getAttribute("id") + "///");
        }
    }
    return sb.ToString();
}
function datosPerf() {
    if ($I("tblDatosPerfiles") == null) return;
    var bBorrarTodos = true;
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblDatosPerfiles").rows.length; x++) {
        if ($I("tblDatosPerfiles").rows[x].getAttribute("bd") == "D")
            sb.Append("D##" + $I("tblDatosPerfiles").rows[x].getAttribute("id") + "///");
        else
            bBorrarTodos = false;
    }
    return sb.ToString();
}


function grabarSalir() {
    if (getOp($I("btnGrabarSalir")) != 100) return;
    bSalir = true;
    grabar();
}
function grabarAux() {
    if (getOp($I("btnGrabar")) != 100) return;
    bSalir = false;
    grabar();
}
function aceptar() {
    bSalir = false;
    modalDialog.Close(window, null);
}

window.onbeforeunload = unload;
function unload() {
    var mensaje = "";
    if (bSalir == false)
    {
        if (bCambios)
            mensaje = "Los datos han sido modificados. Al salir perdera los cambios. ";
        if (!hayPerfiles2()) 
        {
            mensaje += "La experiencia profesional debe tener asociado al menos un perfil, de lo contrario la experiencia profesional no sera visible en tu CV.";
        }
        if (mensaje!="")
            alert(mensaje);
    }
}

function salir() {
    bSalir = false;
    bSaliendo = true;
    var mensaje = "";
    //Datos Modificados
    if (bCambios && intSession > 0) {
        mensaje = "Los datos han sido modificados.<br>"
        if (bCambios && !hayPerfiles2()) {
            mensaje += "La experiencia profesional debe tener asociado al menos un perfil, de lo contrario ésta no sera visible en tu CV.<br>";
        }
        mensaje += "<br>¿Realmente desea salir sin grabar?";
        jqConfirm("", mensaje, "", "", "war", 400).then(function (answer) {
            if (answer) {
                bSalir = true;
                bCambios = false;
            }
            else {
                bCambios = true;
                bSalir = false;
            }
            salirContinuar();
        });
    }
    else {//Datos No modificados
        if (!hayPerfiles2() &&
            ($I("txtDen").value != "" || $I("txtDes").value != "" ||($I("txtEmpresaC").value != "" && $I("txtEmpresaC").className != "WaterMark") ||(hayAreaConTec()) || (hayAreaConSec()))) {
            mensaje = "La experiencia profesional debe tener asociado al menos un perfil,<br>de lo contrario la experiencia profesional no sera visible en tu CV. ";

            mensaje += "<br><br>¿Realmente desea salir sin grabar?";
            jqConfirm("", mensaje, "", "", "war", 400).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    bCambios = false;
                }
                else {
                    bCambios = true;
                    bSalir = false;
                }
                salirContinuar();
            });
        }
        else {
            bSalir = true;
            salirContinuar();
        }
    }
}
function salirContinuar()
{
    var returnValue;
    if (bHayCambios)
        returnValue = "R";
    if (bSalir) {
        returnValue = { resultado: "OK", id: $I("hdnEP").value }
        //setTimeout("window.close();", 250);
        modalDialog.Close(window, returnValue);
    }
    ocultarProcesando();
}

function hayAreaConTec() {
    bRes = false;
    for (var x = 0; x < $I("tblConTec").rows.length; x++) {
        if ($I("tblConTec").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}
function hayAreaConSec() {
    bRes = false;
    for (var x = 0; x < $I("tblConSec").rows.length; x++) {
        if ($I("tblConSec").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}

function hayPerfiles() {
    bRes = false;
    for (var x = 0; x < $I("tblDatosPerfiles").rows.length; x++) {
        if ($I("tblDatosPerfiles").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}
function hayPerfiles2() {
    bRes = false;
    if ($I("tblDatosPerfiles").rows.length > 0)
        bRes = true;
    return bRes;
}
function nuevoACT() {
    try {
        if (!bExpProfModificable) return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/ACT/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 500))
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    for (var i = 0; i < aElementos.length; i++) {
                        if (aElementos[i] == "") continue;
                        bPonerFila = true;
                        var aDatos = aElementos[i].split("@#@");
                        if (aDatos[0] != "") {
                            for (var x = 0; x < $I("tblConTec").rows.length; x++) {
                                if ($I("tblConTec").rows[x].getAttribute("id") == aDatos[0]) {
                                    //alert("Area ya incluida");
                                    bPonerFila = false;
                                    break;
                                }
                            }
                            //ponerFila
                            if (bPonerFila) {
                                var oNF = $I("tblConTec").insertRow(-1);
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
        mostrarErrorAplicacion("Error al añadir un área de conocimiento tecnológico", e.message);
    }
}
function EliminarACT() {
    try {
        if (!bExpProfModificable) return;
        //mostrarProcesando();
        //recorrer filas seleccionadas y marcar para borrado
        for (var x = 0; x < $I("tblConTec").rows.length; x++) {
            if ($I("tblConTec").rows[x].className.toUpperCase() == "FS") {
                if ($I("tblConTec").rows[x].getAttribute("bd") == "I")
                    $I("tblConTec").deleteRow(x);
                else {
                    mfa($I("tblConTec").rows[x], "D");
                    activarGrabar();
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un área de conocimiento tecnológico", e.message);
    }
}
function nuevoACS() {
    try {
        if (!bExpProfModificable) return;
        mostrarProcesando();
        //var strEnlace = "../ExpProf/ACS/Default.aspx";
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/ACS/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 500))
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    for (var i = 0; i < aElementos.length; i++) {
                        if (aElementos[i] == "") continue;
                        bPonerFila = true;
                        var aDatos = aElementos[i].split("@#@");
                        if (aDatos[0] != "") {
                            for (var x = 0; x < $I("tblConSec").rows.length; x++) {
                                if ($I("tblConSec").rows[x].getAttribute("id") == aDatos[0]) {
                                    //alert("Area ya incluida");
                                    bPonerFila = false;
                                    break;
                                }
                            }
                            //ponerFila
                            if (bPonerFila) {
                                var oNF = $I("tblConSec").insertRow(-1);
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
        mostrarErrorAplicacion("Error al añadir un área de conocimiento sectorial", e.message);
    }
}
function EliminarACS() {
    try {
        if (!bExpProfModificable) return;
        //mostrarProcesando();
        //recorrer filas seleccionadas y marcar para borrado
        for (var x = 0; x < $I("tblConSec").rows.length; x++) {
            if ($I("tblConSec").rows[x].className.toUpperCase() == "FS") {
                if ($I("tblConSec").rows[x].getAttribute("bd") == "I")
                    $I("tblConSec").deleteRow(x);
                else {
                    mfa($I("tblConSec").rows[x], "D");
                    activarGrabar();
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un área de conocimiento sectorial", e.message);
    }
}
//Al seleccionar un valor en el combo de Sector origen hay que recargar el combo de Segmento origen
function setSegmentoOri() {
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
        setSegmentoO();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los segmentos del sector", e.message);
    }
}
//Al seleccionar un valor en el combo de Sector destino hay que recargar el combo de Segmento destino
function setSegmentoDes() {
    try {
        var idSector = $I("cboSectorD").value
        var iNum = 0;
        $I("cboSegmentoD").length = 0;
        for (var i = 0; i < aSEG_js.length; i++) {
            if (idSector == aSEG_js[i][0]) {
                var op1 = new Option(aSEG_js[i][2], aSEG_js[i][1]);
                $I("cboSegmentoD").options[iNum++] = op1;
            }
        }
        setSegmentoD();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los segmentos del sector", e.message);
    }
}
function setSegmentoO() {
    try {
        activarGrabar();
        acOri.setOptions({ params: { opcion: 'cuentaCVT', origen: ''} });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el segmento", e.message);
    }
}
function setSegmentoD() {
    try {
        activarGrabar();
        acDes.setOptions({ params: { opcion: 'cuentaCVT', origen: ''} });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el segmento", e.message);
    }
}
function nuevoPerf() {
    bAnadiendoPerfil = true;
    if ($I("hdnEP").value == "-1") {
        ocultarProcesando();
        bDetalle = true;
        grabar();
        return;
    }
    if ($I("hdnEsEncargado").value == "S")
        mdPerf($I("hdnEP").value, $I("hdnidExpFicepi").value, "-1", $I("txtDen").value, "V");
    else
        mdPerf($I("hdnEP").value, $I("hdnidExpFicepi").value, "-1", $I("txtDen").value, "B");

    bAnadiendoPerfil = false;
}
function mostrarPerfil(oFila) {
    mdPerf($I("hdnEP").value, oFila.getAttribute("idEPF"), oFila.getAttribute("id"), $I("txtDen").value, oFila.getAttribute("est"));
}
function mdPerf(idExpProf, idExpProfFicepi, idPerfil, sDenExpProf, sEstado) {
    try {
        //bECV = false;
        //No se puede acceder a perfiles sin tener los datos obligatorios grabados
        if (!comprobarDatos())
            return;
        
        mostrarProcesando();
        var sNewEstado = "";
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Perfil/Default.aspx";
        strEnlace += "?iE=" + codpar(idExpProf) + "&iF=" + codpar($I("hdnProf").value);
        strEnlace += "&iEF=" + codpar(idExpProfFicepi) + "&iP=" + codpar(idPerfil);
        strEnlace += "&eA=" + codpar($I("hdnEsEncargado").value);
        //strEnlace += "&e=" + sEstado;
        strEnlace += "&dE=" + codpar(sDenExpProf);

        modalDialog.Show(strEnlace, self, sSize(550, 610))
            .then(function(ret) {
                if (ret == null) {
                    ocultarProcesando();
                    if (!hayPerfiles()) {
                        mmoff("War", "La experiencia profesional debe tener asociado al menos un perfil,\nde lo contrario se perderán los datos de la experienci profesional.", 450, 2000);
                    }
                }
                else {
                    var sb = new StringBuilder; //sin paréntesis
                    sb.Append("getperfil@#@" + $I("hdnProf").value + "@#@" + $I("hdnEP").value);
                    RealizarCallBack(sb.ToString(), "");
                }
            });
        window.focus();
        
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir un perfil", e.message);
    }
}
function actualizarEstadoPerfil(oFila, sEstado) {
    try {
        //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
        switch (sEstado) {
            case "X": //Pseudovalidado por Encargado de CVs
            case "Y": //Pseudovalidado por RRHH
                oFila.cells[1].children[0].src = strServer + "images/imgPseudovalidado.png";
                oFila.cells[1].children[0].title = "Datos pendientes de validar";
                break;
            //case "O": //Pdte validar por Encargado de CVs
            //case "P": //Pdte validar por RRHH
            //    oFila.cells[1].children[0].src = strServer + "images/imgPenValidar.png";
            //    oFila.cells[1].children[0].title = "Datos pendientes de validar";
            //    break;
            //case "R": //Rechazado
            //    oFila.cells[1].children[0].src = strServer + "images/imgRechazar.gif";
            //    oFila.cells[1].children[0].title = "Datos no interesantes";
            //    break;
            case "S": //Pdte cumplimentar por el técnico con origen ECV
            case "T": //Pdte cumplimentar por el técnico con origen Validador
                oFila.cells[1].children[0].src = strServer + "images/imgPenCumplimentar.png";
                oFila.cells[1].children[0].title = "Datos pendientes de cumplimentar";
                break;
            case "V": //Validado
                oFila.cells[1].children[0].src = strServer + "images/imgFN.gif";
                oFila.cells[1].children[0].title = "";
                break;
            case "B": //borrador
                oFila.cells[1].children[0].src = strServer + "images/imgBorrador.png";
                oFila.cells[1].children[0].title = "Borrador";
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al actualizar el estado de un perfil", e.message);
    }
}
function EliminarPerf() {
    try {

        jqConfirm("", "¿Deseas eliminar el perfil seleccionado?", "", "", "war", 350).then(function (answer) {
            if (answer)
                EliminarPerfContinuar()
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un perfil-1", e.message);
    }
}

function EliminarPerfContinuar() {
    try {
        //mostrarProcesando();
        var bHayBorrado = false;
        var sb = new StringBuilder;
        //recorrer filas seleccionadas y marcar para borrado
        for (var x = 0; x < $I("tblDatosPerfiles").rows.length; x++) {
            if ($I("tblDatosPerfiles").rows[x].className.toUpperCase() == "FS") {
                if ($I("tblDatosPerfiles").rows[x].getAttribute("bd") == "I") {
                    $I("tblDatosPerfiles").deleteRow(x);
                    bHayBorrado = true;
                }
                else {
                    //mfa($I("tblDatosPerfiles").rows[x], "D");
                    //activarGrabar2();
                    //bHayCambios = true;
                    bHayBorrado = true;
                    $I("tblDatosPerfiles").rows[x].setAttribute("bd", "D");
                    sb.Append($I("tblDatosPerfiles").rows[x].getAttribute("id") + "##");
                }
            }
        }
        if (!bHayBorrado)
            mmoff("War", "Debes seleccionar algún elemento para borrar", 330);
        else {
            //if (!hayPerfiles())
            //    mmoff("War", "La experiencia profesional debe tener asociado al menos un perfil,\nde lo contrario ésta no sera visible en su CV. ", 450, 2500);
            if (sb.ToString() != "")
                RealizarCallBack("delPerfil@#@" + sb.ToString(), "");
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un perfil-2", e.message);
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
        if (object.el.context.id == "txtEmpresaC") {
            if (acOri.selectedIndex != -1) {
                $I("hdnCtaOri").value = acOri.data[acOri.selectedIndex];
                $I("cboSectorC").value = acOri.sector[acOri.selectedIndex];
                setSegmentoOri();
                $I("cboSegmentoC").value = acOri.segmento[acOri.selectedIndex];
                $I("cboSegmentoC").disabled = true;
                $I("cboSectorC").disabled = true;
            } else {
                $I("hdnCtaOri").value = "null";
                if ($I("cboSegmentoC").disabled == true) {
                    $I("cboSegmentoC").value = "";
                    $I("cboSectorC").value = "";
                    $I("cboSegmentoC").disabled = false;
                    $I("cboSectorC").disabled = false;
                }
            }
        }
        else if (object.el.context.id == "txtEmpresaD") {
            if (acDes.selectedIndex != -1) {
                $I("hdnCtaDes").value = acDes.data[acDes.selectedIndex];
                $I("cboSectorD").value = acDes.sector[acDes.selectedIndex];
                setSegmentoDes();
                $I("cboSegmentoD").value = acDes.segmento[acDes.selectedIndex];
                $I("cboSegmentoD").disabled = true;
                $I("cboSectorD").disabled = true;
            } else {
                $I("hdnCtaDes").value = "null";
                if ($I("cboSegmentoD").disabled == true) {
                    $I("cboSegmentoD").value = "";
                    $I("cboSectorD").value = "";
                    $I("cboSegmentoD").disabled = false;
                    $I("cboSectorD").disabled = false;
                }
            }
        }
        //cambiarParamExamen();
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al verificar cliente contratante", e.message);
    }
}
//Al salir del autocomplete, compruebo que si hay texto escrito si se ha asignado un valor al campo clave oculto
//Si no hay clave busco esa denominación en la cache del autocomplete y si la encuentro, la asigno
function verificarAutoComplete(object) {
    try {
        if (object.el.context.id == "txtEmpresaC") {
            if (acOri.selectedIndex == -1 && $I("txtEmpresaC").value != "" && $I("txtEmpresaC").className != "WaterMark") {
                if ($I("txtEmpresaC").value.length > 2) {
                    var sTexto = $I("txtEmpresaC").value.toUpperCase() ;
                    for (i = 0; i < acOri.data.length; i++) {
                        if (acOri.suggestions[i].toUpperCase() == sTexto) {
                            $I("hdnCtaOri").value = acOri.data[i];
                            $I("cboSectorC").value = acOri.sector[i];
                            setSegmentoOri();
                            $I("cboSegmentoC").value = acOri.segmento[i];
                            $I("cboSegmentoC").disabled = true;
                            $I("cboSectorC").disabled = true;
                            acOri.selectedIndex = i;
                            activarGrabar1();
                            break;
                        }
                    }
                }
            } 
        }
        else if (object.el.context.id == "txtEmpresaD") {
            if (acDes.selectedIndex == -1 && $I("txtEmpresaD").value != "" && $I("txtEmpresaD").className != "WaterMark") {
                if ($I("txtEmpresaD").value.length > 2) {
                    var sTexto = $I("txtEmpresaD").value.toUpperCase();
                    for (i = 0; i < acDes.data.length; i++) {
                        if (acDes.suggestions[i].toUpperCase() == sTexto) {
                            $I("hdnCtaDes").value = acDes.data[i];
                            $I("cboSectorD").value = acDes.sector[i];
                            setSegmentoDes();
                            $I("cboSegmentoD").value = acDes.segmento[i];
                            $I("cboSegmentoD").disabled = true;
                            $I("cboSectorD").disabled = true;
                            acDes.selectedIndex = i;
                            activarGrabar1();
                            break;
                        }
                    }
                }
            } 
        }
        //cambiarParamExamen();
        //bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al verificar cuenta", e.message);
    }
}

function borrarSegSec(){
    if ($I("txtEmpresaD").value == "" || $I("txtEmpresaD").value == $I("txtEmpresaD").getAttribute("watermarktext")){
        $I("cboSectorD").value = "";
        $I("cboSegmentoD").value = "";
    }        
}