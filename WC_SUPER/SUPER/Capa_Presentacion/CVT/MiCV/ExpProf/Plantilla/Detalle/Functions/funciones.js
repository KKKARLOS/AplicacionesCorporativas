var bHayCambios = false;
var bSaliendo = false;

function init() {
    try {
        if (!mostrarErrores()) return;

        $I("txtDescripcion").focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload() {
    if (!bSaliendo) salir();
}
function grabarSalir() {
    bSalir = true;
    grabar();
}

function salir() {
    var sRes = "";
    bSaliendo = true;
    bCambios = false;
    if (bHayCambios) {
        sRes = $I("hdnId").value + "@#@" + Utilidades.escape($I("txtDescripcion").value) + "@#@" + Utilidades.escape($I("txtFun").value) + "@#@" + Utilidades.escape($I("cboPerfil").options[$I("cboPerfil").selectedIndex].innerText);
    }
    var returnValue = sRes;
    modalDialog.Close(window, returnValue);
}
function grabar() {
    try {
        if (getOp($I("btnGrabarSalir")) != 100) return;
        if (!comprobarDatos()) return;
        
        mostrarProcesando();
        var js_args = "grabar@#@";
        js_args += $I("hdnEP").value + "#/#"; //codigo de la experiencia profesional
        js_args += $I("hdnId").value + "#/#"; //codigo de la plantilla (-1 si es nuevo)
        js_args += Utilidades.escape($I("txtDescripcion").value) + "#/#"; //Descripcion
        js_args += Utilidades.escape($I("txtFun").value) + "#/#"; //funciones
        js_args += Utilidades.escape($I("txtObs").value) + "#/#"; //Observaciones
        js_args += $I("cboPerfil").value + "#/#";
        js_args += $I("cboIdioma").value + "@#@";
        js_args += entornos();

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la plantilla", e.message);
    }
}
function entornos() {
    var sb = new StringBuilder;
    for (var x = 0; x < $I("tblEnt").rows.length; x++) {
        if ($I("tblEnt").rows[x].getAttribute("bd") == "I") {
            sb.Append("I##" + $I("tblEnt").rows[x].getAttribute("id") + "///");
        }
        else {
            if ($I("tblEnt").rows[x].getAttribute("bd") == "D")
                sb.Append("D##" + $I("tblEnt").rows[x].getAttribute("id") + "///");
        }
    }
    return sb.ToString();
}
function comprobarDatos() {
    try {
        if ($I("txtDescripcion").value == "") {
            //alert("hola");
            mmoff("War", "La denominación es obligatoria.", 240, 2000);
            $I("txtDescripcion").focus();
            return false;
        }
        if ($I("cboPerfil").value == "") {
            mmoff("War", "El perfil es obligatorio.", 240, 2000);
            return false;
        }
        if ($I("txtFun").value == "") {
            mmoff("War", "Las funciones son obligatorias.", 240, 2000);
            $I("txtFun").focus();
            return false;
        }
        if ($I("tblEnt").rows.length == 0) {
            mmoff("War", "Los entornos tecnológicos/funcionales son obligatorios.", 350, 2000);
            return false;
        }
        if (!hayEnt()) {
            mmoff("War", "El perfil debe tener asociado al menos un Entorno tecnologico/funcional.", 450, 2000);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}




function hayEnt() {
    bRes = false;
    for (var x = 0; x < $I("tblEnt").rows.length; x++) {
        if ($I("tblEnt").rows[x].getAttribute("bd") != "D") {
            bRes = true;
            break;
        }
    }
    return bRes;
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        alert(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                bCambios = false;
                //if (bNuevo)
                $I("hdnId").value = aResul[2];
                bHayCambios = true;
                salir();
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando) ocultarProcesando();
    }
}

function nuevoEnt() {
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
                            for (var x = 0; x < $I("tblEnt").rows.length; x++) {
                                if ($I("tblEnt").rows[x].getAttribute("id") == aDatos[0]) {
                                    //alert("Area ya incluida");
                                    bPonerFila = false;
                                    break;
                                }
                            }
                            //ponerFila
                            if (bPonerFila) {
                                var oNF = $I("tblEnt").insertRow(-1);
                                oNF.setAttribute("bd", "I");
                                oNF.setAttribute("style", "height:20px");
                                oNF.attachEvent('onclick', mm);
                                oNF.setAttribute("id", aDatos[0]);

                                var oImgFI = document.createElement("img");
                                oImgFI.setAttribute("src", "../../../../../../images/imgFI.gif");
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
        mostrarErrorAplicacion("Error al añadir un entorno tecnológico", e.message);
    }
}
function EliminarEnt() {
    try {
        //mostrarProcesando();
        //recorrer filas seleccionadas y marcar para borrado
        for (var x = 0; x < $I("tblEnt").rows.length; x++) {
            if ($I("tblEnt").rows[x].className.toUpperCase() == "FS") {
                //if ($I("tblConTec").rows[x].getAttribute("class").toUpperCase() == "FS") {
                if ($I("tblEnt").rows[x].getAttribute("bd") == "I")
                    $I("tblEnt").deleteRow(x);
                else {
                    mfa($I("tblEnt").rows[x], "D");
                    activarGrabar();
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar un entorno tecnológico", e.message);
    }
}
