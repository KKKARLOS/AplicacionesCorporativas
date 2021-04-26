var aACT, aACS, aPERF;
//var idNuevo = 1;
var bSalir = false;
var bHayCambios = false;
var bExpProfModificable = true;
var bAnadiendoPerfil = false;
//Vbles para acceder al detalle de un perfil cuando la experiencia profesional está pdte de grabar
var bDetalle = false;
var sCaso = "";
var oImgAux = document.createElement("img");
oImgAux.setAttribute("src", "../../../../../images/imgFN.gif");
oImgAux.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; height:16px; width:16px;");

function init() {
    try {
        if (!mostrarErrores()) return;
        if ($I("hdnModo").value == "R") {
            setOp($I("btnGrabar"), 30);
            setOp($I("btnNewACT"), 30);
            setOp($I("btnDelACT"), 30);
            setOp($I("btnNewACS"), 30);
            setOp($I("btnDelACS"), 30);
            setOp($I("btnNewPerf"), 30);
            $I("lblCliente").setAttribute("class", "texto");
        }
        //Si el registro es nuevo, es mi propio Curriculum o soy el Encargado de CVs -> Puedo manejar perfiles
        if ($I("hdnEP").value == "-1" || $I("hdnEsMiCV").value == "S" || $I("hdnEsEncargado").value == "S") {
            //01-16 (Lacalle): Se permite insertar nuevos perfiles independientemente de que la experiencia provenga de una plantilla
            //Si el perfil tiene su origen en plantilla no se pueden añadir perfiles
            //if ($I("hdnEsPlant").value == "S")
            //    setOp($I("btnNewPerf"), 30);
            //else
                setOp($I("btnNewPerf"), 100);
        }
        else
            setOp($I("btnNewPerf"), 30);
                
        if (es_DIS || sMOSTRAR_SOLODIS == "0")
        {
            //alert("entra:" + sTareasPendientes)
            mostrarMensajesTareasPendientes(sTareasPendientes);
        }
        //else
        //{
        //    //alert("sale:" + sTareasPendientes)
        //    mostrarMensajesTareasPendientes(sTareasPendientes);
        //}

        if ($I("hdnEP").value == "-1") $I("txtDen").focus(); 
        desActivarGrabar();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarMensajesTareasPendientes(strDatos) {
    var aResul = strDatos.split("@##@");
    var sMensaje = "";
    for (var i = 0; i <= aResul.length - 1; i++) 
    {
        if (aResul[i] != "") {
            var aArgs = aResul[i].split("#dato#");
            
            if (i == 0) sCaso = sCaso + aArgs[2];
            else sCaso = sCaso + "," + aArgs[2];

            if ($I("hdnEsMiCV").value == "S" || es_DIS) 
            {
                if (aArgs[2] == "2" && aArgs[3] != "")        // Caso:2 (Asignación de una experiencia al curriculum de un profesional )
                {
                    sMensaje += "¡Atención!\n\n- Debes añadir y enviar a validar un perfil en la experiencia para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                }
                else if (aArgs[2] == "3" && aArgs[3] != "")   // Caso:3 (Asignación de la fecha de baja a un profesional en una experiencia)
                {
                    sMensaje += " \n\n- Al haber finalizado tu participación en la experiencia, debes revisar el contenido de los perfil/es, y enviar a validarlo/s para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                    $I("btnExpRevisada").style.display = "block";
                    $I("tblBotones").style.marginLeft = "300px"; 
                }
                // Caso:4 (Borrar la fecha de baja a un profesional en una experiencia)
                // Caso:6 Al reasignar una experiencia que tiene algún perfil creado
                else if ((aArgs[2] == "4" || aArgs[2] == "6") && aArgs[3] != "")                                                                                        
                {
                    sMensaje += "\n\n- Al haber sido dado de alta de nuevo en la experiencia, debes modificar la fecha fin del perfil que estimes oportuno o añadir un nuevo perfil, y enviarlo a validar para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                    $I("btnExpRevisada").style.display = "block";
                    $I("tblBotones").style.marginLeft = "300px";
                }
                else if (aArgs[2] == "5"  && aArgs[3] != "")    // Caso:5 (Cuando un tercero (validador, encargado de CV) establece al registro el estado ‘Pdte de cumplimentar’.)
                {
                    sMensaje += "\n\n- Debes corregir/modificar el registro, y enviarlo a validar para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + ".";
                }
            }
        }
    }
    if (sMensaje!="") mmoff("warper", sMensaje, 690);            

}
function activarGrabar1() {
    try {
        if ($I("hdnModo").value == "R") return;
        if (!bExpProfModificable) return;
        if (!bCambios) {
            setOp($I("btnGrabar"), 100);
            bCambios = true;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function activarGrabar() {
    try {
        if ($I("hdnModo").value == "R") return;
        setOp($I("btnGrabar"), 100);
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function activarGrabar2() {
    try {
        if (!bCambios) {
            setOp($I("btnGrabar"), 100);
            bCambios = true;
        }
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
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK")  mostrarErrorSQL(aResul[3], aResul[2]);
    else {
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
                    mmoff("Inf", "No existen perfiles o estos tienen estado 'Borrador' o 'Pendiente de cumplimentar'", 500);
                }
                else {
                    setOp($I("btnExpRevisada"), 30);
                    mmoff("Inf", "Validación de la experiencia realizada correctamente", 340);
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
                            if (aACT[j].getAttribute("bd") == "D") $I("tblConTec").deleteRow(j);
                            else mfa(aACT[j], "N");
                        }
                    }
                }
                if ($I("tblConSec") != null) {
                    aACS = FilasDe("tblConSec");
                    for (var k = aACS.length - 1; k >= 0; k--) {
                        if (aACS[k].getAttribute("bd") != "") {
                            if (aACS[k].getAttribute("bd") == "D") $I("tblConSec").deleteRow(k);
                            else mfa(aACS[k], "N");
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
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}
function setRevisadaExper() {
    try 
    {
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("RevisadaPerfilExper@#@");
        //sb.Append(sCaso);
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function grabar() {
    try {
        if (!comprobarDatos()) return;
        
        if (!bAnadiendoPerfil && !hayPerfiles()) {
            jqConfirm("", "La experiencia profesional debe tener asociado al menos un perfil,<br>de lo contrario se perderán los datos de la experiencia profesional.<br><br>Pulsa Aceptar para continuar en la pantalla o Cancelar para salir.", "", "", "war", 470).then(function (answer) {
                if (!answer) {
                    bCambios = false;
                    mostrarProcesando();
                    RealizarCallBack("borrarEP@#@" + $I("hdnProf").value + "@#@" + $I("hdnEP").value, "");
                }
            });
        } else LLamadaGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar-1.", e.message);
    }
}
function LLamadaGrabar() {
    try {
        var sb = new StringBuilder; //sin paréntesis
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
        sb.Append(sAux + "@#@" + $I("hdnProf").value);
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
    sb.Append($I("hdnCli").value);
        
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
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblDatosPerfiles").rows.length; x++) {
        if ($I("tblDatosPerfiles").rows[x].getAttribute("bd") == "D")
            sb.Append("D##" + $I("tblDatosPerfiles").rows[x].getAttribute("id") + "///");
    }
    return sb.ToString();
}
function comprobarDatos() {
    if ($I("txtDen").value == "") {
        mmoff("War", "La denominación es obligatoria", 230, 1500);
        return false;
    }
    if ($I("txtDes").value == "" && $I("hdnEsEncargado").value != "S") {
        mmoff("War", "La descripción es obligatoria", 230, 1500);
        return false;
    }
    if ($I("txtCliente").value == "" && $I("hdnEsEncargado").value != "S") {
        mmoff("War", "El cliente es obligatorio", 230, 1500);
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
    return true;
}

function grabarSalir() {
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
    if (bSalir == false) {
        if (bCambios)
            mensaje = "Los datos han sido modificados.";
        if (!hayPerfiles2()) {
            if (mensaje!="") mensaje += "\n";
            mensaje += "La experiencia profesional debe tener asociado al menos un perfil, de lo contrario ésta no sera visible en su CV. ";
        }
        if (mensaje != "")
            alert(mensaje + "\nAl salir perdera los cambios.");
    }
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    var mensaje = "";
    //Datos Modificados
    if (bCambios && intSession > 0) {
        mensaje = "Los datos han sido modificados.<br />"
        if (bCambios && !hayPerfiles2()) {
            mensaje += "La experiencia profesional debe tener asociado al menos un perfil, de lo contrario ésta no sera visible en tu CV.<br /><br />";
        }
        mensaje += "¿Realmente deseas salir sin grabar?";
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
        if (!hayPerfiles2() && ($I("txtDen").value != "" || $I("txtDes").value != "" || $I("txtCliente").value != "" || (hayAreaConTec()) || (hayAreaConSec()))) {
            mensaje = "La experiencia profesional debe tener asociado al menos un perfil, de lo contrario ésta no sera visible en tu CV.<br /><br />";
            mensaje += "¿Realmente deseas salir?";
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
        else
        {
            bSalir = true;
            salirContinuar()
        }
    }
}

function salirContinuar() {
    var returnValue;
    if (bHayCambios) returnValue = "R";
    if (bSalir) {
        returnValue = { resultado: "OK", id: $I("hdnEP").value }
        //setTimeout("window.close();", 250);
        modalDialog.Close(window, returnValue);
    }
    ocultarProcesando();
}
function hayPerfiles() {
    if ($I("hdnEsPlant").value == "S" || $I("hdnTieneProy").value == "S")
        return true;
    var bRes = false;
    for (var x = 0; x < $I("tblDatosPerfiles").rows.length; x++) {
        if ($I("tblDatosPerfiles").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}
function hayPerfiles2() {
    if ($I("hdnEsPlant").value == "S" || $I("hdnTieneProy").value == "S")
        return true;
    var bRes = false;
    if ($I("tblDatosPerfiles").rows.length > 0) 
        bRes = true;
    return bRes;
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
    } 
    catch (e) {
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
                //if ($I("tblConTec").rows[x].getAttribute("class").toUpperCase() == "FS") {
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
function nuevoPerf() {
    bAnadiendoPerfil = true;
    if ($I("hdnEP").value == "-1") {
        ocultarProcesando();
        bDetalle = true;
        grabar();
        return;
    }
    if ($I("hdnEsEncargado").value == "S")
        mdPerf($I("hdnEP").value, $I("hdnidExpFicepi").value, "-1", $I("txtDen").value, "V", 'M');
    else
        mdPerf($I("hdnEP").value, $I("hdnidExpFicepi").value, "-1", $I("txtDen").value, "B", 'M');
    
    bAnadiendoPerfil = false;
}
function mostrarPerfil(oFila) {
    mdPerf($I("hdnEP").value, oFila.getAttribute("idEPF"), oFila.getAttribute("id"), $I("txtDen").value, oFila.getAttribute("est"), oFila.getAttribute("tipo"));
}
function mdPerf(idExpProf, idExpProfFicepi, idPerfil, sDenExpProf, sEstado, plantilla) {
    try {
        mostrarProcesando();
        var sNewEstado = "";
        //Tipo P(Plantilla) M (Perfil)
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Perfil/Default.aspx";
        strEnlace += "?iE=" + codpar(idExpProf) + "&iF=" + codpar($I("hdnProf").value);
        strEnlace += "&iEF=" + codpar(idExpProfFicepi) + "&iP=" + codpar(idPerfil);
        strEnlace += "&eA=" + codpar($I("hdnEsEncargado").value);
        //strEnlace += "&e=" + sEstado;
        strEnlace += "&dE=" + codpar(sDenExpProf);
        strEnlace += "&tipo=" + codpar(plantilla);
        strEnlace += "&o=expIber";
        if ($I("hdnModo").value == "R")
            strEnlace += "&LSuper=" + codpar("LS");
        

        modalDialog.Show(strEnlace, self, sSize(550, 610))
            .then(function(ret) {
                if (ret == null) {
                    ocultarProcesando();
                    if (!hayPerfiles()) {
                        mmoff("War", "La experiencia profesional debe tener asociado al menos un perfil,\nde lo contrario ésta no sera visible en su CV. ", 450, 2500);
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
            //    oFila.cells[1].children[0].src = strServer + "images/imgRechazar.png";
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
            if (answer) {
                //mostrarProcesando();
                var bHayBorrado = false;
                var sb = new StringBuilder;
                //recorrer filas seleccionadas y marcar para borrado
                for (var x = 0; x < $I("tblDatosPerfiles").rows.length; x++) {
                    if ($I("tblDatosPerfiles").rows[x].className.toUpperCase() == "FS") {
                        bHayBorrado = true;
                        if ($I("tblDatosPerfiles").rows[x].getAttribute("bd") == "I") {
                            $I("tblDatosPerfiles").deleteRow(x);
                        }
                        else {
                            //if ($I("tblDatosPerfiles").rows[x].getAttribute("tipo") == "P") {
                            //    //mmoff("War","No puede eliminar un perfil cuyo origen es una plantilla",350);
                            //    PedirBorrado("tblDatosPerfiles", $I("tblDatosPerfiles").rows[x]);
                            //    return;
                            //}
                            //else {
                            //    //mfa($I("tblDatosPerfiles").rows[x], "D");
                            //    //activarGrabar2();
                            //    //bHayCambios = true;
                            //    $I("tblDatosPerfiles").rows[x].setAttribute("bd", "D");
                            //    sb.Append($I("tblDatosPerfiles").rows[x].getAttribute("id") + "##");
                            //}

                            //01-16(Lacalle): Se pueden borrar los perfiles independientemente de su origen
                            $I("tblDatosPerfiles").rows[x].setAttribute("bd", "D");

                            if ($I("tblDatosPerfiles").rows[x].getAttribute("tipo") == "P")
                                sb.Append($I("tblDatosPerfiles").rows[x].getAttribute("tipo") + "@#@" + $I("tblDatosPerfiles").rows[x].getAttribute("idEPF") + "##");
                            else
                                sb.Append($I("tblDatosPerfiles").rows[x].getAttribute("tipo") + "@#@" + $I("tblDatosPerfiles").rows[x].getAttribute("id") + "##");
                        }
                    }
                }
                if (!bHayBorrado)
                    mmoff("War", "Debes seleccionar algún elemento para borrar", 330);
                else {
                    if (sb.ToString() != "")
                        RealizarCallBack("delPerfil@#@" + sb.ToString(), "");
                }
            }
        });
     }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un perfil", e.message);
    }
}
function PedirBorrado(tabla, oFila) {
    try {
        var sApartado = "", sElemento = "", sKey = "", sTipo = "";
        switch (tabla) {
            case "tblDatosPerfiles":
                sTipo = "PE";
                sApartado = "Perfil de la Experiencia profesional en IBERMÁTICA: " + $I("txtDen").value;
                sElemento = getCelda(oFila, 2);
                //Al ser un perfil de plantilla cuando el usuario está pidiendo borrar el perfil, en realidad se rtansforma en una petición
                //de borrado de la experiencia del profesional (T812_EXPPROFFICEPI)
                sKey = oFila.getAttribute("idEPF");
                break;
        }
        //"&eA=" + codpar($I("hdnEsAdministrador").value  
        if (sTipo != "") {
            var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/PetBorrado/Default.aspx?t=" + sTipo +
                        "&n=" + codpar($I("hdnNomProf").value) +
                        "&a=" + codpar(sApartado) +
                        "&e=" + codpar(sElemento) +
                        "&p=" + codpar($I("hdnUserAct").value) + //Usuario que pide el borrado
                        "&k=" + codpar(sKey); //Código del registro que se quiere borrar

            modalDialog.Show(strEnlace, self, sSize(700, 540))
            .then(function(ret) {
                    if (ret != null) {
                        //Pongo el icono de pdte de borrado a la fila
                        ponerFilaPdteBorrar(oFila);
                    }
                });
            window.focus();
        }
        else
            mmoff("Err", "No se ha encontrado el tipo de elemento para la solicitud de borrado", 400);
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al solicitar borrado", e.message);
    }
}
function ponerFilaPdteBorrar(oFila) {
    //alert("Falta el icono que indica fila pendiente de borrado");
    if (oFila.cells[0].getElementsByTagName("img").length == 0)
        oFila.cells[0].appendChild(oImgPdteBor.cloneNode(), null);
    else {
        oFila.cells[0].children[0].setAttribute("src", "../../../../../Images/imgPetBorrado.png");
        oFila.cells[0].children[0].setAttribute("title", "Pdte de eliminar");
    }
}

function getCliente() {
    try {
        if ($I("hdnModo").value == "R") return;
        if (!bExpProfModificable) return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getCliente.aspx";
        modalDialog.Show(strEnlace, self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnCli").value = aDatos[0];
                    $I("txtCliente").value = aDatos[1];
                }
            });
        window.focus();
        ocultarProcesando();
    } 
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al seleccionar cliente", e.message);
    }
}
function borrarCliente() {
    if ($I("hdnModo").value == "R") return;
    if (!bExpProfModificable) return;
    $I("hdnCli").value = "-1";
    $I("txtCliente").value = "";
    activarGrabar();
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