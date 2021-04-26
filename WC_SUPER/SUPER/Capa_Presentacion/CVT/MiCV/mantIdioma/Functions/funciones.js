var numTitulo = { doc: 0, titulo: 1, centro: 2, fecha: 3, estado: 4 };
var bDatosModificados = false;
var bSaliendo = false;
var bHayCambios = false;
var bSalir = false;
function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();
        if ($I("ctl00$hdnRefreshPostback") == null) document.forms[0].appendChild(oRefreshPostback);
        $I("ctl00$hdnRefreshPostback").value = "S";
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload() {
    if (!bSaliendo) salir();
}
function salir() {
    bSaliendo = true;
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                bEnviar = grabar();
            }
            else {
                bCambios = false;
                continuarSalir(bHayCambios)
            }
        });
    } else continuarSalir(bHayCambios);
}
function continuarSalir(bHayCambios) {
    var sRes;
    //if (bDatosModificados) bHayCambios = true;
    if (bHayCambios) {
        var sId;
        if ($I("hdnIdCodIdioma").value != "")
            sId = $I("hdnIdCodIdioma").value;
        else
            sId = $I("cboIdioma").value;
        sRes = { resultado: "OK", bHayCambios: true, id: sId };
    }
    else {
        sRes = null;
    }
    var returnValue = sRes;
    //setTimeout("window.close();", 250); //para que de tiempo a grabar y actualizar "bCambios";
    modalDialog.Close(window, returnValue);
}
function UnloadValor() {
//    if (nName == 'chrome') {
//        if ($I("hdnOP").value == "1") {
//            window.returnValue = { resultado: "OK",
//                bDatosModificados: true,
//                id: ($I("hdnIdCodIdioma").value != "") ? $I("hdnIdCodIdioma").value : $I("cboIdioma").value
//            }
//        }
//    }                              
//    unload();
}
function cerrarVentana() {
    try {
        if (bProcesando()) return;
        var returnValue = (bDatosModificados) ? { resultado: "OK", bDatosModificados: true, id: ($I("hdnIdCodIdioma").value != "") ? $I("hdnIdCodIdioma").value : $I("cboIdioma").value} : null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function comprobarDatos() {
    try {
        
        if ($I("cboIdioma").value == "" && $I("txtIdioma").value == "") {
            ocultarProcesando();
            mmoff("War", "Debes seleccionar el idioma.", 200);
            return false;
        }
        var sLectura = getRadioButtonSelectedValue("rdbLectura", true);
        if (sLectura == "" && $I("hdnEsEncargado").value == "0") {
            ocultarProcesando();
            mmoff("War", "Debes seleccionar el nivel de lectura.", 300);
            return false;
        }
        var sEscritura = getRadioButtonSelectedValue("rdbEscritura", true);
        if (sEscritura == "" && $I("hdnEsEncargado").value == "0") {
            ocultarProcesando();
            mmoff("War", "Debes seleccionar el nivel de escritura.", 300);
            return false;
        }
        var sConversacion = getRadioButtonSelectedValue("rdbConversacion", true);
        if (sConversacion == "" && $I("hdnEsEncargado").value == "0") {
            ocultarProcesando();
            mmoff("War", "Debes seleccionar el nivel de conversación.", 300);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
    }
}
function setIdioma() {
    $I("hdnIdCodIdioma").value = $I("cboIdioma").value;
}
var anadirTitulo = false;
var idioma = "";
var estado = "";
var idtitulo = "";
function addTitulo() {
    idioma = $I("hdnIdCodIdioma").value;
    estado = "";
    idtitulo = "";

    mostrarProcesando();

    if (!comprobarDatos()) return false;
    else if (bCambios) {
        anadirTitulo = true;
        grabar();
    } else
        abrirDetTitulo();
}

function AnadirTitulo(strIdioma, strEstado, strIdtitulo) 
{
    idioma=strIdioma;
    estado=strEstado;
    idtitulo=strIdtitulo;
    
    mostrarProcesando();

    if (!comprobarDatos()) return false;
    else if(bCambios){
            anadirTitulo = true;
            grabar();
         }else
            abrirDetTitulo();
}

function EliminarTitulo() {
    try {
        if ($I("tblDatos") == null) return;
        if ($I("tblDatos").rows.length == 0) return;
        var bHayBorrado = false;
        var sb = new StringBuilder;
        sb.Append("eliminartitulo@#@");

        aFilas = FilasDe("tblDatos");
        if (aFilas != null) {
            for (i = aFilas.length - 1; i >= 0; i--) {
                if (aFilas[i].className == "FS") {
                    sb.Append(aFilas[i].id + "@titulo@");
                    bHayBorrado = true;
                }
            }
        }
        if (bHayBorrado) {
            jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    RealizarCallBack(sb.ToString(), "");
                }
                else {
                    ocultarProcesando();
                    return;
                }
            });
        } else
            mmoff("War", "Debes seleccionar algún elemento para borrar", 330);
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Eliminar", e.message);
    }
}

function CargarTitulos() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("CargarTitulos@#@");
        sb.Append($I("hdnIdFicepi").value + "@#@");
        sb.Append($I("hdnIdCodIdioma").value);

        RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a cargar los títulos", e.message);
    }
}

function grabar() {
    try {
        mostrarProcesando();
        if (!comprobarDatos()) return false;
            
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        sb.Append($I("hdnIdCodIdiomaEntrada").value + "@#@");
        sb.Append((($I("cboIdioma").value =="")? $I("hdnIdCodIdioma").value : $I("cboIdioma").value) + "@#@");
        sb.Append(getRadioButtonSelectedValue("rdbLectura", true) + "@#@");
        sb.Append(getRadioButtonSelectedValue("rdbEscritura", true) + "@#@");
        sb.Append(getRadioButtonSelectedValue("rdbConversacion", true) + "@#@");
        
        RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Grabar", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                ocultarProcesando();
                bDatosModificados = true;
                bHayCambios = true;
                $I("hdnOP").value = "2";
                $I("hdnIdCodIdioma").value = ($I("cboIdioma").value == "") ? $I("hdnIdCodIdioma").value : $I("cboIdioma").value;
                $I("hdnIdCodIdiomaEntrada").value = ($I("cboIdioma").value == "") ? $I("hdnIdCodIdioma").value : $I("cboIdioma").value;
                if (anadirTitulo) {
                    anadirTitulo = false;
                    abrirDetTitulo();
                }
                else mmoff("Suc", "Grabación correcta", 220, 2300);
                bCambios = false;
                if (bSalir) setTimeout("salir();", 50);
                break;
            case "CargarTitulos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                break;
            case "eliminartitulo":
                bDatosModificados = true;
                bHayCambios = true;
                var aFilas = FilasDe("tblDatos");
                if (aFilas != null) {
                    for (var i = aFilas.length - 1; i >= 0; i--) {
                        if (aFilas[i].className == "FS") {
                            $I("tblDatos").deleteRow(i);
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
function aG() {
    bCambios = true;
}

function abrirDetTitulo(){

    var descIdioma;
    if ($I("cboIdioma").options[$I("cboIdioma").selectedIndex].text != "")
        descIdioma = $I("cboIdioma").options[$I("cboIdioma").selectedIndex].text;
    else
        descIdioma = $I("txtIdioma").value;
    var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantTitulo/Default.aspx?iI=" + codpar($I("hdnIdCodIdioma").value) +
                    "&iT=" + codpar(idtitulo) + "&pantalla=" + codpar("form1") + "&dI=" + codpar(descIdioma) + 
                    "&eA=" + codpar($I("hdnEsEncargado").value) + "&iF=" + codpar($I("hdnIdFicepi").value);

    modalDialog.Show(strEnlace, self, sSize(460, 400))
        .then(function(ret) {
        if (ret != null) {
            bDatosModificados = true;
            bHayCambios = true;
            setTimeout("CargarTitulos();", 250);
        }
    });
    window.focus();
    ocultarProcesando();
}