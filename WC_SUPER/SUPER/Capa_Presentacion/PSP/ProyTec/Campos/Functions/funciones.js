
var aFila;
var bSalir = false;
var bCambios = false;

function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();
        //setOp($I("btnGrabar"), 30);
        //setOp($I("btnGrabarSalir"), 30);
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        var sError = aResul[2];
        mostrarError(sError.replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                var sElementosInsertados = aResul[2];
                var aValores = sElementosInsertados.split("//");
                aValores.reverse();
                var nIndiceEI = 0;
                aFila = FilasDe("tblDatos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].id = aValores[nIndiceEI];
                        aFila[i].cells[1].innerHTML = "<nobr class='NBR W400' onmouseover='TTip(event)'>" + aFila[i].cells[1].children[0].value + "</nobr>";
                        aFila[i].cells[2].innerHTML = aFila[i].cells[2].children[0].options[aFila[i].cells[2].children[0].selectedIndex].text;
                        nIndiceEI++;
                    }
                    mfa(aFila[i], "N");
                }
                //setOp($I("btnGrabar"), 30);
                //setOp($I("btnGrabarSalir"), 30);
                bCambios = false;
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) salir();
                break;
            case "getDatos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                //$I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
                //$I("divCatalogo2").children[0].children[0].innerHTML = aResul[2];
                //$I("divCatalogo2").children[0].children[0].style.backgroundImage = "url(../../../Images/imgFT20.gif)";
                //eval(aResul[3]);
                //initDragDropScript();
                //scrollTablaProfAsig();
                desActivarGrabar();
                break;
        }
        ocultarProcesando();
    }
}
function eliminar() {
    try {
        if ($I("tblDatos") == null) return;
        if ($I("tblDatos").rows.length == 0) return;

        aFilas = $I("tblDatos").getElementsByTagName("TR");
        var strID = "";
        for (i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className == "FS") {
                if (aFilas[i].getAttribute("owner") != $I("hdn_ficepi_actual").value && aFilas[i].getAttribute("owner") != $I("hdn_ficepi_actual").value) {
                    mmoff("Inf", "No tiene permiso para borrar el campo ", 320);
                    return;
                }
                aFilas[i].setAttribute("bd", "D");
                strID += "D##";
                strID += aFilas[i].id;
                strID += "///";
            }
        }

        if (strID == "") {
            mmoff("Inf", "No hay ninguna fila seleccionada.", 230);
            return;
        }

        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "grabar@#@" + strID;
                js_args = js_args.substring(0, js_args.length - 3);
                RealizarCallBack(js_args, "");  //con argumentos
            }
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Eliminar", e.message);
    }
}
function grabar() {
    try {

        aFila = FilasDe("tblDatos");
        if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "D"
                sb.Append(aFila[i].id + "##"); //ID Campo
                if (aFila[i].getAttribute("bd") == "I") {
                    sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) + "##"); //Descripcion
                    sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) + "##"); //Id de Tipo de dato
                }
                else sb.Append("##D##");
                sb.Append("///");
                sw = 1;
            }
        }
        if (sw == 0) {
            //setOp($I("btnGrabar"), 30);
            //setOp($I("btnGrabarSalir"), 30);
            mmoff("Inf", "No se han modificado los datos.", 230);
            if (bSalir) salir();
            return;
        }

        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos() {
    try {
        var nOrden = 0;
        var js_denominaciones = new Array();

        for (var i = 0; i < aFila.length; i++) {
            if (js_denominaciones.isInArray(aFila[i].cells[1].children[0].value) == null) {
                js_denominaciones[js_denominaciones.length] = (aFila[i].cells[1].children[0].value != null) ? aFila[i].cells[1].children[0].value : aFila[i].cells[1].children[0].innerText;
            } else {
                ms(aFila[i]);
                mmoff("War", "No se permiten denominaciones repetidas.", 320);
                aFila[i].cells[1].children[0].focus();
                return false;
            }
            if (aFila[i].getAttribute("bd") == "") continue;
            if (aFila[i].getAttribute("bd") == "D") {
                var strElement = aFila[i].id;
                var tblDatos = fOpener().$I("tblDatos2");
                for (var z = 0; z < tblDatos.rows.length; z++) {
                    if (tblDatos.rows[z].id == strElement) {
                        //fOpener().BorrarFiltro(tblDatos.rows[z].id);
                        tblDatos.deleteRow(z);
                        break;
                    }
                }

                continue;
            }
            if (aFila[i].cells[1].children[0].value == "") {
                ms(aFila[i]);
                mmoff("War", "Debes indicar la denominación del campo", 270);
                aFila[i].cells[1].children[0].focus();
                return false;
            }
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function grabarSalir() {
    bSalir = true;
    grabar();
}
function grabarAux() {
    bSalir = false;
    grabar();
}
function salir() {
    bSalir = false;
    var returnValue = null;
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function Detalle(oFila) {
    try {
        var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/Detalle/Default.aspx?ID=" + oFila.id + "&bNueva=false&t305_idproyectosubnodo=" + $I("hdnT305_idproyectosubnodo").value;
        modalDialog.Show(strEnlace, self, sSize(630, 200))
	        .then(function (ret) {
	            if (ret != null) ObtenerDatos();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}
function ObtenerDatos() {
    try {
        mostrarProcesando();
        var js_args = "getDatos@#@";
        js_args += ($I("cboAmbito").value == "") ? "99@#@" : $I("cboAmbito").value + "@#@";
        js_args += ($I("cboTipoDato").value == "") ? "9@#@" : $I("cboTipoDato").value + "@#@";
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function nuevo() {
    try {
        var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/Detalle/Default.aspx?bNueva=true&t305_idproyectosubnodo=" + $I("hdnT305_idproyectosubnodo").value;
        modalDialog.Show(strEnlace, self, sSize(630, 200))
	        .then(function (ret) {
	            if (ret != null) ObtenerDatos();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función nuevo", e.message);
    }
}
function cargarAmbitoTipo() {
    try {
        if ($I("cboAmbito").value == "") {
            $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
            return;
        }
        var js_args = "getDatos@#@";
        //js_args += ($I("cboAmbito").value == "") ? "99@#@" : $I("cboAmbito").value + "@#@";
        //js_args += ($I("cboTipoDato").value == "") ? "9@#@" : $I("cboTipoDato").value + "@#@";
        js_args += $I("cboAmbito").value + "@#@";
        js_args += $I("cboTipoDato").value + "@#@";
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la tabla", e.message);
    }
}

