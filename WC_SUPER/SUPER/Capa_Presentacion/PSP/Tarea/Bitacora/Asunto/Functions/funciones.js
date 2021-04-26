//var bFechaModificada=false;
var sFFVAnt;
var bHayCambios = false;
var bSaliendo = false;

function init() {
    try {
        if (!mostrarErrores()) return;

        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        if ($I("hdnAcceso").value == "R") {
            setOp($I("Button1"), 30);
            setOp($I("Button2"), 30);
            $I("lblProy").className = "texto";
            $I("lblProy").onclick = null;
            if (btnCal == "I") {
                //$I("txtValNotif").readOnly=true;
                $I("txtValNotif").onclick = "";
                $I("txtValNotif").onchange = "";
                //$I("txtValLim").readOnly=true;
                $I("txtValLim").onclick = "";
                $I("txtValLim").onchange = "";

                //$I("txtValFin").readOnly=true;
                $I("txtValFin").onclick = "";
                $I("txtValFin").onchange = "";
            }
            else {
                $I("txtValNotif").onmousedown = "";
                $I("txtValNotif").onfocus = "";
                $I("txtValLim").onmousedown = "";
                $I("txtValLim").onfocus = "";
                $I("txtValFin").onmousedown = "";
                $I("txtValFin").onfocus = "";
            }
        }
//        else {
//            $I("lblProy").className = "enlace";
//            $I("lblProy").onclick = function() { obtenerResponsable(); };

//            if (btnCal == "I") {
//                //$I("txtValNotif").readOnly=false;
//                $I("txtValNotif").onclick = function() { mc(this); };
//                $I("txtValNotif").onchange = function() { activarGrabar(); focoRef(); };

//                //$I("txtValLim").readOnly=false;
//                $I("txtValLim").onclick = function() { mc(this); };
//                $I("txtValLim").onchange = function() { activarGrabar(); focoRef(); };

//                //$I("txtValFin").readOnly=false;
//                $I("txtValFin").onclick = function() { mc(this); };
//                $I("txtValFin").onchange = function() { activarGrabar(); focoRef(); };
//            }
//            else {
//                $I("txtValNotif").onmousedown = function() { mc1(this); };
//                $I("txtValNotif").onfocus = function() { focoFecha(this); };
//                $I("txtValLim").onmousedown = function() { mc1(this); };
//                $I("txtValLim").onfocus = function() { focoFecha(this); };
//                $I("txtValFin").onmousedown = function() { mc1(this); };
//                $I("txtValFin").onfocus = function() { focoFecha(this); };
//            }
//        }
        $I("txtDesAsunto").select();
        //Variables a devolver a la estructura.
        sIdAsunto = $I("txtIdAsunto").value;
        sDescripcion = $I("txtDesAsunto").value;
        sFLim = $I("txtValLim").value;
        sFNotif = $I("txtValNotif").value;
        if ($I("cboTipo").selectedIndex != -1)
            sTipo = $I("cboTipo").options[$I("cboTipo").selectedIndex].innerText;
        sSeveridad = $I("cboSeveridad").options[$I("cboSeveridad").selectedIndex].innerText;
        sPrioridad = $I("cboPrioridad").options[$I("cboPrioridad").selectedIndex].innerText;
        sEstado = $I("cboEstado").options[$I("cboEstado").selectedIndex].innerText;
        sIdResponsable = $I("txtIdResponsable").value;
        //Vble que controla si se ha modificado la fecha de finalización para enviar alerta
        sFFVAnt = $I("txtValFin").value;

        scrollTablaProfAsig();
        //bHayCambios = false;
        bCambios = false;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload() {
    if (!bSaliendo) salir();
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function grabarSalir() {
    bSalir = true;
    grabar();
}
function grabarAux() {
    bSalir = false;
    grabar();
}
function aceptar() {
    var strRetorno;
    bSalir = false;
    if ($I("hdnAcceso").value == "R") {
        strRetorno = "F@#@";
    }
    else {
        if (bHayCambios) strRetorno = "T@#@";
        else strRetorno = "F@#@";
    }
    strRetorno += $I("txtIdAsunto").value + "@#@";
    strRetorno += sDescripcion + "@#@";
    strRetorno += sTipo + "@#@";
    strRetorno += sSeveridad + "@#@";
    strRetorno += sPrioridad + "@#@";
    strRetorno += sFLim + "@#@";
    strRetorno += sFNotif + "@#@";
    strRetorno += sEstado + "@#@";
    strRetorno += sIdResponsable;

    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function salir() {
    bSalir = false;
    bSaliendo = true;

    if ($I("hdnAcceso").value != "R") {
        if (bCambios && intSession > 0) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    grabarSalir();
                }
                else {
                    bCambios = false;
                    salirCerrarVentana();
                }
            });
        }
        else salirCerrarVentana();
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
    var strRetorno = "F@#@";
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T@#@";
    }
    strRetorno += $I("txtIdAsunto").value + "@#@";
    strRetorno += sDescripcion + "@#@";
    strRetorno += sTipo + "@#@";
    strRetorno += sSeveridad + "@#@";
    strRetorno += sPrioridad + "@#@";
    strRetorno += sFLim + "@#@";
    strRetorno += sFNotif + "@#@";
    strRetorno += sEstado + "@#@";
    strRetorno += sIdResponsable;

    var returnValue = strRetorno;
    //setTimeout("window.close();", 250); //para que de tiempo a grabar y actualizar "bCambios";
    modalDialog.Close(window, returnValue);
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
                bCambios = false;
                bHayCambios = true;
                $I("txtIdAsunto").value = aResul[2];
                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);
                //Dejo como grabadas las filas para no volver a grabarlas
                var aRecursos = $I("tblOpciones2").getElementsByTagName("TR");
                for (var i = aRecursos.length - 1; i >= 0; i--) {
                    if (aRecursos[i].getAttribute("bd") == "D") {
                        $I("tblOpciones2").deleteRow(i);
                    } else {
                        mfa(aRecursos[i], "N");
                    }
                }

                if (bSalir) {
                    salir();
                }
                else {//Recargo la cronología
                    $I("txtEstadoAnt").value = $I("cboEstado").value;
                    setTimeout("historial(" + $I("txtIdAsunto").value + ");", 250);
                }
                scrollTablaProfAsig();
                actualizarLupas("tblTitulo2", "tblOpciones2");
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                break;
            case "historial":
                $I("divTareas").children[0].innerHTML = aResul[2];
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblOpciones");
                break;
            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                setEstadoBotonesDoc(aResul[3], aResul[4]);
                nfs = 0;
                break;
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
                }
                aFila = null;
                nfs = 0;
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function historial(sIdAsunto) {
    try {
        var js_args = "historial@#@" + sIdAsunto;
        RealizarCallBack(js_args, "");  //con argumentos

    } catch (e) {
        mostrarErrorAplicacion("Error al recargar el historial del asunto", e.message);
    }
}
function grabar() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;
        // 0 -> id asunto (si -1 es un alta)
        // 1 -> descripcion corta
        // 2 -> descripcion larga
        // 3 -> referencia externa
        // 4 -> f/notificación
        // 5 -> f/fin
        // 6 -> f/limite
        // 7 -> alerta
        // 8 -> departamento
        // 9 -> tipo
        // 10-> estado
        // 11-> severidad
        // 12-> prioridad
        // 13-> sistema
        // 14-> esfuerzo planificado
        // 15-> esfuerzo real
        // 16 -> observaciones
        // 17 -> cod une
        // 18 -> nº proyecto
        // 19 -> notificador
        // 20 -> id responsable
        // 21 -> estado anterior
        // 22 -> nombre responsable
        // 23 -> desc tipo
        // 24-> desc estado
        // 25-> desc severidad
        // 26-> desc prioridad
        // 27 -> Cod Tarea
        var bEnviarAlerta = false;
        var sIdAsunto = $I("txtIdAsunto").value;

        if (sIdAsunto == "" || sIdAsunto == "-1") {
            bEnviarAlerta = true;
            if ($I("cboEstado").value == "0") $I("cboEstado").value = 1;
        }
        if (sFFVAnt != $I("txtValFin").value) bEnviarAlerta = true;

        var js_args = "grabar@#@";
        js_args += sIdAsunto + "##"; //nº asunto  //0
        js_args += Utilidades.escape($I("txtDesAsunto").value) + "##"; //1
        js_args += Utilidades.escape($I("txtDescripcion").value) + "##"; //2
        js_args += Utilidades.escape($I("txtRefExt").value) + "##"; //3
        js_args += $I("txtValNotif").value + "##"; //4
        js_args += $I("txtValFin").value + "##"; //5
        js_args += $I("txtValLim").value + "##"; //6
        js_args += Utilidades.escape($I("txtAlerta").value) + "##"; //7
        js_args += Utilidades.escape($I("txtDpto").value) + "##"; //8
        js_args += $I("cboTipo").value + "##"; //9
        js_args += $I("cboEstado").value + "##"; //10
        js_args += $I("cboSeveridad").value + "##"; //11
        js_args += $I("cboPrioridad").value + "##"; //12
        js_args += Utilidades.escape($I("txtSistema").value) + "##"; //13
        var vAux = fts(dfn($I("txtEtp").value));
        js_args += vAux + "##"; //14
        vAux = fts(dfn($I("txtEtr").value));
        js_args += vAux + "##"; //15

        js_args += Utilidades.escape($I("txtObs").value) + "##"; //16
        js_args += "##"; //17 escape($I("hdnCRActual").value) +
        //js_args += dfn($I("hdnIdPE").value) +"##"; //18
        js_args += $I("hdnIdT").value + "##"; //18
        js_args += Utilidades.escape($I("txtNotificador").value) + "##"; //19
        js_args += $I("txtIdResponsable").value + "##"; //20
        js_args += $I("txtEstadoAnt").value + "##"; //21
        js_args += Utilidades.escape($I("txtResponsable").value) + "##"; //22
        if ($I("cboTipo").selectedIndex != -1)
            js_args += Utilidades.escape($I("cboTipo").options[$I("cboTipo").selectedIndex].innerText) + "##"; //23
        else
            js_args += "##"; //23
        js_args += Utilidades.escape($I("cboEstado").options[$I("cboEstado").selectedIndex].innerText) + "##"; //24
        js_args += Utilidades.escape($I("cboSeveridad").options[$I("cboSeveridad").selectedIndex].innerText) + "##"; //25
        js_args += Utilidades.escape($I("cboPrioridad").options[$I("cboPrioridad").selectedIndex].innerText) + "@#@"; //26 

        js_args += flGetIntegrantes(); //lista de integrantes

        //Variables a devolver a la estructura.
        sIdAsunto = $I("txtIdAsunto").value;
        sDescripcion = $I("txtDesAsunto").value;
        sFLim = $I("txtValLim").value;
        sFNotif = $I("txtValNotif").value;
        if ($I("cboTipo").selectedIndex != -1)
            sTipo = $I("cboTipo").options[$I("cboTipo").selectedIndex].innerText;
        else
            sTipo = "";
        sSeveridad = $I("cboSeveridad").options[$I("cboSeveridad").selectedIndex].innerText;
        sPrioridad = $I("cboPrioridad").options[$I("cboPrioridad").selectedIndex].innerText;
        sEstado = $I("cboEstado").options[$I("cboEstado").selectedIndex].innerText;
        sIdResponsable = $I("txtIdResponsable").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del asunto", e.message);
    }
}
function comprobarDatos() {
    try {
        if ($I("txtDesAsunto").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre del asunto",240);
            return false;
        }
        if ($I("txtIdResponsable").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el responsable del asunto",270);
            return false;
        }
        if ($I("txtValNotif").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar la fecha de notificación",240);
            return false;
        }
        //Salvo en insert el estado debe ser obligatorio
        if ($I("cboEstado").value == "0") {
            if (sIdAsunto != "" && sIdAsunto != "-1") {
                tsPestanas.setSelectedIndex(0);
                mmoff("War", "Debes indicar el estado del asunto",240);
                return false;
            }
        }
        if ($I("cboTipo").selectedIndex == -1) {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el tipo de asunto",240);
            return false;
        }
        //Alertas por e-mail
        if ($I("txtAlerta").value != "") {
            var aResul = $I("txtAlerta").value.split(";");
            for (i = 0; i < aResul.length; i++) {
                if (aResul[i] != "") {
                    if (!validarEmail(fTrim(aResul[i]))) {
                        tsPestanas.setSelectedIndex(0);
                        mmoff("War", "La dirección de correo indicada en el campo Alertas no es válida (" + aResul[i] + ")",400);
                        return false;
                    }
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar() {
    try {
        //if (event.keyCode == 9) return;
        if ($I("hdnAcceso").value != "R") {
            if (!bCambios) {
                setOp($I("btnGrabar"), 100);
                setOp($I("btnGrabarSalir"), 100);
                bCambios = true;
                //bHayCambios = true;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function girar() {
    $I("txtDesAsunto").select();
}
function mostrarProfesional() {
    var strInicial;
    try {
        //if (bLectura) return;
        strInicial = Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) +
	              "@#@" + Utilidades.escape($I("txtNombre").value) + "@#@" + $I("hdnNodo").value;
        if (strInicial == "@#@@#@") return;

        var js_args = "buscar@#@" + strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function anadirConvocados() {
    try {
        var aFilas = $I("tblOpciones").rows;
        if (aFilas.length > 0) {
            for (x = 0; x < aFilas.length; x++) {
                if (aFilas[x].className == "FS") {
                    convocar(aFilas[x].id, aFilas[x].cells[1].innerText, aFilas[x].getAttribute("mail"));
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir componentes", e.message);
    }
}
function convocarAux(oFila) {
    try {
        convocar(oFila.id, oFila.cells[1].innerText, oFila.getAttribute("mail"));
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir componentes (convocarAux)", e.message);
    }
}
function convocar(idUsuario, strUsuario, sMail) {
    try {
        if (bLectura) return;
        var aFilas = $I("tblOpciones2").rows;
        if (aFilas.length > 0) {
            for (var i = 0; i < aFilas.length; i++) {
                if (aFilas[i].id == idUsuario) {
                    if (aFilas[i].style.display == "none") {
                        aFilas[i].setAttribute("bd", "U");
                        aFilas[y - 1].style.display = "";
                    }
                    return;
                }
            }
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        sNombreNuevo = strUsuario;
        for (var iFilaNueva = 0; iFilaNueva < aFilas.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFilas[iFilaNueva].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
        oNF.id = idUsuario;
        oNF.setAttribute("mail", sMail);
        oNF.setAttribute("bd", "I");
        //oNF.style.cursor = "pointer";
        //oNF.style.height = "20px";
        oNF.setAttribute("style", "height:20px; cursor:pointer;");
        oNF.setAttribute("sw", "1");
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNC1 = oNF.insertCell(-1);
        oNC1.style.width = "10px";
        oNC1.appendChild(oImgFI.cloneNode(true));
        
        oNC2 = oNF.insertCell(-1);
        oNC2.style.width = "20px";
        oNC2.appendChild($I("tblOpciones").rows[iFila].children[0].cloneNode(true));
        oNC3 = oNF.insertCell(-1);
        oNC3.style.width = "365px";
        oNC3.innerText = strUsuario;
        oNC4 = oNF.insertCell(-1);
        oNC4.setAttribute("style", "width:40px;");
        var oCtrl2 = document.createElement("input");
        oCtrl2.setAttribute("type", "checkbox");
        oCtrl2.setAttribute("className", "checkTabla");
        oCtrl2.setAttribute("checked", "true");
        oCtrl2.setAttribute("id", "chkEst" + oNF.rowIndex);
        oCtrl2.setAttribute("style", "width:20px; height:14px; vertical-align:top; margin-top:2px; padding-top:0px; margin-bottom:0px; padding-bottom:0px;");
        oCtrl2.onclick = function() { actualizarDatos(this) };
        oNC4.appendChild(oCtrl2);

        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
function actualizarDatos(objInput) {
    try {
        var oFila = objInput.parentNode.parentNode;
        if (oFila.getAttribute("bd") != "I") oFila.setAttribute("bd", "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar la fila como modificada.", e.message);
    }
}
function flGetIntegrantes() {
    /*Recorre la tabla de Integrantes para obtener una cadena que se pasará como parámetro
    al procedimiento de grabación. Pasa trios de valores: indicador acción BD ## mail ## notificar ## nombre completo
    */
    var sRes = "";
    try {
        aFila = $I("tblOpciones2").getElementsByTagName("TR");
        var nEstado = 0;

        for (i = 0; i < aFila.length; i++) {
            if (aFila[i].cells[3].children[0].checked) nEstado = 1;
            else nEstado = 0;
            sRes += aFila[i].getAttribute("bd") + "##" + aFila[i].id + "##" + nEstado + "##" + Utilidades.escape(aFila[i].cells[2].innerText) + "##" + aFila[i].getAttribute("mail") + "///";
        } //for
        return sRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function nuevoDoc1() {
    if ($I("hdnAcceso").value == "R") return;
    var sIdAsunto = $I('txtIdAsunto').value;
    if ((sIdAsunto == "") || (sIdAsunto == "0")) {
        mmoff("Inf","El asunto debe estar grabado para poder asociarle documentación",410);
    }
    else {
        nuevoDoc('AS_T', sIdAsunto);
    }
}
function eliminarDoc1() {
    if ($I("hdnAcceso").value == "R") return;
    var sIdAsunto = $I('txtIdAsunto').value;
    if ((sIdAsunto == "") || (sIdAsunto == "0")) {
        mmoff("Inf","El asunto debe estar grabado para poder borrar documentación",400);
    }
    else {
        eliminarDoc();
    }
}
function obtenerResponsable() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    activarGrabar();
                }
            });
        window.focus();    
        $I("txtNotificador").select();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el responsable", e.message);
    }
}
function focoRef() {
    $I("txtRefExt").select();
}
function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = $I("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (oRow.getAttribute("bd") == "I") {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    else mfa(oRow, "D");
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        var NewRow;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;
                        var oCloneNode = oRow.cloneNode(true);
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);
                        oCloneNode.setAttribute("class", "");

                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(false));

                        oCloneNode.cells[1].setAttribute("style", "width:365px;");

                        oCloneNode.insertCell(-1);
                        oCloneNode.cells[1].setAttribute("style", "width:40px;");
                        var oChk = document.createElement("input");
                        oChk.setAttribute("type", "checkbox");
                        oChk.setAttribute("className", "checkTabla");
                        oChk.setAttribute("checked", "true");
                        oChk.setAttribute("id", "chkNot" + x);
                        oChk.setAttribute("style", "width:20px;");
                        oChk.onclick = function() { actualizarDatos(this) };
                        oCloneNode.cells[3].appendChild(oChk.cloneNode(false));

                        mfa(oCloneNode, "I");
                    }
                    break;
            }
        }
        actualizarLupas("tblTitulo2", "tblOpciones2");

        activarGrabar();
    }
    oTable = null;
    killTimer();
    CancelDrag();

    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProf) {
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProf / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblOpciones").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblOpciones.rows.length; i++){
            if (!$I("tblOpciones").rows[i].getAttribute("sw")) {
                oFila = $I("tblOpciones").rows[i];
                oFila.setAttribute("sw", "1");

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
            }
            //            nContador++;
            //            if (nContador > $I("divCatalogo").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig() {
    try {
        if ($I("divCatalogo2").scrollTop != nTopScrollProfAsig) {
            nTopScrollProfAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProfAsig / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, $I("tblOpciones2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblOpciones2").rows[i].getAttribute("sw")) {
                oFila = $I("tblOpciones2").rows[i];
                oFila.setAttribute("sw", "1");
                oFila.attachEvent('onclick', mm);
                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(true), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(true), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
