var aFila;
var bCboNodo = false;

function init() {
    try {
        if ($I("hdnMensajeError").value != "") {
            var reg = /\\n/g;
            var strMsg = $I("hdnMensajeError").value;
            strMsg = strMsg.replace(reg, "\n");
            mmoff("Inf", strMsg, 400);
            $I("hdnMensajeError").value = "";
        }
        actualizarLupas("tblTitulo", "tblDatos");
        scrollTablaAE();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function obtenerProyectos() {
    try {
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge";
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    limpiar();
                    $I("hdnT305IdProy").value = aDatos[0];
                    $I("txtNumPE").value = aDatos[3];
                    $I("txtPE").value = aDatos[4];
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function obtenerPTs() {
    try {
        var aOpciones, idPE, sPE, idPT, strEnlace, sAncho, sAlto;

        idPE = $I("txtNumPE").value;
        sPE = $I("txtPE").value;
        var sTamano;

        if (idPE == "" || idPE == "0") {
            strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT2.aspx";
            sTamano = sSize(820, 650);
        }
        else {
            strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPSN=" + codpar($I("hdnT305IdProy").value) + "&nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE);
            sTamano = sSize(500, 580);
        }
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function (ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    idPT = aOpciones[0];
                    if ($I("hdnIDPT").value != idPT) {
                        if (idPE == "" || idPE == "0") {
                            $I("txtNumPE").value = aOpciones[2].ToString("N", 9, 0);
                            $I("txtPE").value = aOpciones[3];
                            $I("hdnT305IdProy").value = aOpciones[4];
                        }
                        $I("hdnIDPT").value = idPT;
                        $I("txtPT").value = aOpciones[1];
                        $I("txtNumPT").value = idPT.ToString("N", 9, 0);
                    }
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}
function buscarPE() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar el PE.", e.message);
    }
}
function limpiar() {
    $I("hdnT305IdProy").value = "";
    $I("txtPE").value = "";
    limpiarPT();
}
function buscarPT() {
    try {
        $I("txtNumPT").value = dfnTotal($I("txtNumPT").value).ToString("N", 9, 0);
        var js_args = "buscarPT@#@";
        js_args += dfn($I("txtNumPT").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar el PT.", e.message);
    }
}
function limpiarPT() {
    try {
        if ($I("txtPT").value != "") {
            $I("txtNumPT").value = "";
            $I("txtPT").value = "";
            $I("hdnIDPT").value = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el PT", e.message);
    }
}


function obtenerParam() {
    try {
        var js_args = "buscarParametrizaciones";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener todas las parametrizaciones", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        var sError = aResul[2];
        if (aResul[1] == "AVISO") {
             mmoff("Inf", "El proyecto técnico no existe o está fuera de tu ámbito de visión.", 390);
        }
        else{
            mostrarError(sError.replace(reg, "\n"));
        }
    } else {
        switch (aResul[0]) {
            case "grabar":
                actualizarLupas("tblTitulo", "tblDatos");
                desActivarGrabar();
                if (bRecargarCombo) {
                    bRecargarCombo = false;
                    recargarCombo();
                    setTimeout("buscar2()",200);
                }
                else {
                    if (bCboNodo) {
                        bCboNodo = false;
                        setTimeout("buscar2()", 200);
                    }
                    else {
                        var aFilas = $I("tblDatos");
                        for (var i = aFilas.rows.length - 1; i >= 0; i--) {
                            if (aFilas.rows[i].getAttribute("bd") != "") {
                                mfa(aFilas.rows[i], "N");
                                aFilas.rows[i].setAttribute("idPTOri", aFilas.rows[i].getAttribute("idPT"));
                            }
                        }
                    }
                }
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                break;
            case "oc":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                scrollTablaAE();
                ocultarProcesando();
                break;
            case "buscarPE":
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);
                }
                else {
                    limpiar();
                    $I("txtNumPE").value = $I("txtNumPE").value.ToString("N", 7, 0);
                    $I("hdnT305IdProy").value = aResul[2];
                    $I("txtPE").value = aResul[3];
                }
                ocultarProcesando();
                break;
            case "buscarPT":
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto técnico no existe o está fuera de tu ámbito de visión.", 360);
                }
                else {
                    limpiar();
                    $I("hdnIDPT").value = dfn($I("txtNumPT").value);
                    $I("txtNumPT").value = $I("txtNumPT").value.ToString("N", 7, 0);
                    $I("hdnT305IdProy").value = aResul[2];
                    $I("txtNumPE").value = aResul[3].ToString("N", 7, 0);
                    $I("txtPE").value = aResul[4];
                    $I("txtPT").value = aResul[5];
                }
                ocultarProcesando();
                break;
            case "buscarParametrizaciones":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                scrollTablaAE();
                ocultarProcesando();
                break;
                
        }
    }
}

function checkUpdated(obj) {
    activarGrabar();
   

    if ($(obj).parent().parent().attr("idpt") !== "") {
        mfa(obj.parentNode.parentNode, "U");
    }
    else
        mfa(obj.parentNode.parentNode, "I");
   
}


function asignarPT() {
    if ($I("hdnIDPT").value == "") {
        mmoff("Inf", "Debes seleccionar un Proyecto Técnico.", 300);
        return;
    }
    var sw = 0;
    aFila = FilasDe("tblDatos");
    for (var i = 0; i < aFila.length; i++) {
        if (aFila[i].cells[4].children[0].checked) {
            //aFila[i].cells[3].style.backgroundImage = "";
            aFila[i].setAttribute("idPT", $I("hdnIDPT").value);
            aFila[i].cells[3].innerHTML = $I("txtPT").value;
            aFila[i].cells[4].children[0].checked = false;
            aFila[i].cells[5].children[0].checked = false;

            //if (aFila[i].cells[5].children[0].checked) {

            //}
            aFila[i].cells[5].children[0].removeAttribute("disabled");

            if (aFila[i].getAttribute("idPTOri") == "") {
                mfa(aFila[i], "I");
            }
            else {
                mfa(aFila[i], "U");
            }
            sw = 1;
        }
    }
    if (sw == 0)
        mmoff("Inf", "Debes seleccionar alguna fila a asignar", 250);
    else
        activarGrabar();
}
function eliminar() {
    try {
        var sw = 0;
        //if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        for (var i = aFila.length - 1; i >= 0; i--) {
            if (aFila[i].cells[4].children[0].checked) {
                sw = 1;
                borrarPT(aFila[i]);
                if (aFila[i].getAttribute("idPTOri") == "") {
                    aFila[i].setAttribute("bd", "");
                }
                else
                    mfa(aFila[i], "D");
            }
        }
        if (sw == 0)
            mmoff("Inf", "Debes seleccionar alguna fila a borrar", 250);
        else
            activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}
function borrarPT(oFila) {
    try {
        oFila.setAttribute("idPT", "");
        //oFila.cells[3].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
        //oFila.cells[3].style.backgroundRepeat = "no-repeat";
        oFila.cells[3].innerText = "";
        oFila.cells[4].children[0].checked = false;
        oFila.cells[5].children[0].checked = false;
        oFila.cells[5].children[0].setAttribute("disabled", "disabled");
        mfa(oFila, "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el PT", e.message);
    }
}
var bRecargarCombo=false;
function mostrarActivas() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bRecargarCombo = true;
                    grabar();
                }
                else {
                    recargarCombo();
                    buscar2();
                    bCambios = false;
                }
            });
        }
        else {
            recargarCombo();
            buscar2();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar Organizaciones comerciales", e.message);
    }
}
function mostrarActivas2() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    grabar();
                }
                else {
                    buscar2();
                    bCambios = false;
                }
            });
        }
        else {
            buscar2();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar Organizaciones comerciales (2)", e.message);
    }
}

function ordenarTabla(nOrden, nAscDesc) {
    if ($I("chkAct").checked)
        buscar(nOrden, nAscDesc, "T");//Organizaciones comerciales activas e inactivas
    else
        buscar(nOrden, nAscDesc, "A");//solo activas
}
function buscar2() {
    ordenarTabla('3', '0');
}
function buscar(nOrden, nAscDesc, sTipo) {
    try {
        var js_args = "oc@#@" + sTipo + "@#@" + $I("cboCR").value + "@#@" + $I("hdnIdProf").value;
        if ($I("chkProf").checked)
            js_args += "@#@S";
        else
            js_args += "@#@N";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al ordenar el catálogo de OTCs", e.message);
    }
}
function getCatalogo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCboNodo = true;
                    grabar();
                }
                else {
                    LLamarGetCatalogo();
                    bCambios = false;
                }
            });
        } else LLamarGetCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function LLamarGetCatalogo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
        if ($I("chkAct").checked)
            buscar(3, 0, "T");
        else
            buscar(3, 0, "A");
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}



function grabar() {
    try {
        //if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        //if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].getAttribute("idOC") + "##"); //Organizacion Comercial
                sb.Append(aFila[i].getAttribute("idP") + "##"); //Profesional
                sb.Append(aFila[i].getAttribute("idPT") + "##"); //Proyecto tecnico
                if (aFila[i].cells[5].children[0].checked) {
                    sb.Append("true##"); //Bloquear
                }
                else
                    sb.Append("false##"); //Bloquear
                
                sb.Append("///");
                sw = 1;
            }
        }
        if (sw == 0) {
            mmoff("War", "No se han modificado los datos.", 230);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}


var oEstadoD = document.createElement("input");
oEstadoD.type = "checkbox";
oEstadoD.className = "check";
oEstadoD.setAttribute("style", "width:15px;margin-left:15px;");

//var oGomaPerfil = document.createElement("img");
//oGomaPerfil.setAttribute("src", "../../../../../images/botones/imgBorrar.gif");
//oGomaPerfil.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var nTopScrollAE = -1;
var nIDTimeAE = 0;
function scrollTablaAE() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollAE) {
            nTopScrollAE = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeAE);
            nIDTimeAE = setTimeout("scrollTablaAE()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;

        var nFilaVisible = Math.floor(nTopScrollAE / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        var sAux;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                //oFila.attachEvent('onclick', mm);

                if (oFila.getAttribute("bd") != "I") oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgFI.cloneNode(true), null);

                //if (oFila.getAttribute("idPT") == "") {
                //    oFila.cells[3].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
                //    oFila.cells[3].style.backgroundRepeat = "no-repeat";
                //}
                //else {
                //    var oGoma = oFila.cells[3].appendChild(oGomaPerfil.cloneNode(true), null);
                //    oGoma.onclick = function () { borrarPT(this.parentNode.parentNode); };
                //    oGoma.style.cursor = "pointer";
                //}
                //oFila.cells[3].style.cursor = strCurMA;
                //oFila.cells[3].ondblclick = function () { getClienteAE(this.parentNode); };

                //oFila.cells[4].appendChild(oEstadoD.cloneNode(true), null);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function marcardesmarcar(nOpcion) {
    try {
        for (var i = 0; i < tblDatos.rows.length; i++) {
            tblDatos.rows[i].cells[4].children[0].checked = (nOpcion == 1) ? true : false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}
function getProfesional() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx?T=CP", self, sSize(460, 535))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("txtProfesional").value = aDatos[1];
	                $I("hdnIdProf").value = aDatos[2];
                    //Si en el combo de Organizaciones comerciales teníamos vacío, lo cambiamos a TODOS
	                if ($I("cboCR").value == "-1")
	                    $I("cboCR").value = "";
	                getCatalogo();
	            }
	            ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar mensaje de GASVI.", e.message);
    }
}
function limpiarProf() {
    if ($I("hdnIdProf").value != "") {
        $I("txtProfesional").value = "";
        $I("hdnIdProf").value = "";
        //Si en el combo de Organizaciones comerciales teníamos TODOS, lo cambiamos a vacío
        if ($I("cboCR").value == "")
            $I("cboCR").value = "-1";
        buscar2();
    }
}

function recargarCombo() {
    var aMC;
    var sel = $I("cboCR");
    sel.options.length = 0;
    if ($I("chkAct").checked)
        aMC = $I("hdnOCtodas").value.split("///");
    else
        aMC = $I("hdnOCactivas").value.split("///");

    for (var i = 0; i < aMC.length; i++) {
        if (aMC[i] != "") {
            aDatos = aMC[i].split("@#@");
            var option = document.createElement('option');
            option.text = aDatos[0];
            option.value = aDatos[1];
            sel.add(option);
        }
    }

}
