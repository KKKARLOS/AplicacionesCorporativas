var bAlgunCambio = false;
var bEsGestorDialogoOriginal = false;
var bEsInterlocutorProyectoOriginal = false;

function init() {
    try {
        if (!mostrarErrores()) return;
//        setOp($I("btnAddMensaje"), 30);
//        setOp($I("btnDocumentacion"), 30);
//        setOp($I("btnCerrarDialogo"), 30);

        if (nIDFicepi == nIDFicepiEntrada) {
            //Aceptada DEF y No conforme DEF
            if (parseInt($I("hdnIdEstado").value, 10) == 4 || parseInt($I("hdnIdEstado").value, 10) == 5) {
                //setOp($I("btnDocumentacion"), 100);
                //            setOp($I("btnAddMensaje"), 30);
                //            setOp($I("btnCerrarDialogo"), 30);
            } else {
                if (bEsGestorDialogo || bEsInterlocutorProyecto) {
                    setOp($I("btnAddMensaje"), 100);
                    setOp($I("btnDocumentacion"), 100);
                }
                if (bEsGestorDialogo) {
                    setOp($I("btnCerrarDialogo"), 100);
                }
                if (bEsAdm || bEsGestorDialogo) {
                    $I("lblFLR").className = "enlace";
                    $I("lblFLR").onclick = function () { mc($I("txtFLR")); };
                }
            }
        }
        //alert(sToday);
        if ($I("hdnHayDocs").value == "S") {
            $I("imgDocFact").setAttribute("src", strServer + "Images/imgCarpetaDoc32.png");
            $I("imgDocFact").title = "Existen documentos asociados";
            setOp($I("btnDocumentacion"), 100);
        }
        else {
            $I("imgDocFact").setAttribute("src", strServer + "Images/imgCarpeta32.png");
            $I("imgDocFact").title = "No existen documentos asociados";
            if (nIDFicepi == nIDFicepiEntrada
                && parseInt($I("hdnIdEstado").value, 10) != 4
                && parseInt($I("hdnIdEstado").value, 10) != 5
                && (bEsInterlocutorProyecto || bEsGestorDialogo)
                ) {
                setOp($I("btnDocumentacion"), 100);
            }
        }
        $I("divDialogo").scrollTop = $I("divDialogo").scrollHeight;

        bEsGestorDialogoOriginal = bEsGestorDialogo;
        bEsInterlocutorProyectoOriginal = bEsInterlocutorProyecto;

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getDialogo":
                bOcultarProcesando = false;
                $I("divDialogo").children[0].innerHTML = aResul[2];
                $I("divDialogo").scrollTop = $I("divDialogo").scrollHeight;
                setTimeout("getDatosEstado();", 20);
                break;
            case "getDatosEstado":
                $I("hdnIdEstado").value = aResul[2];
                $I("txtEstado").value = Utilidades.unescape(aResul[3]);
                $I("txtEstado").title = Utilidades.unescape(aResul[4]);
                break;
            case "grabar":
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function cerrarDialogo() {
    try {
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/DialogoAlertas/CerrarDialogo/Default.aspx?id=" + codpar($I("hdnIdDialogo").value);
        //var ret = window.showModalDialog(strEnlace, self, sSize(960, 600));
        modalDialog.Show(strEnlace, self, sSize(960, 600))
	        .then(function(ret) {
                if (ret != null) {
                    if (ret == "OK") {
                        setOp($I("btnAddMensaje"), 30);
                        setOp($I("btnDocumentacion"), 30);
                        setOp($I("btnCerrarDialogo"), 30);
                        getDatosEstado();
                    }
                } else
                    ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a cerrar el diálogo.", e.message);
    }
}

function getDatosEstado() {
    try {
        var js_args = "getDatosEstado@#@";
        js_args += $I("hdnIdDialogo").value;
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a actualizar el estado.", e.message);
    }
}

function salir() {
    var returnValue = "OK";
    modalDialog.Close(window, returnValue);		
}

function addMensajeAux(sLadoMsg) {
    try {
        //var sLadoMsg = getLadoMsg();
        //if (sLadoMsg == false) return;
        mostrarProcesando();

        var sPantalla = strServer + "Capa_Presentacion/ECO/DialogoAlertas/MsgDialogo/Default.aspx?id=" + codpar($I("hdnIdDialogo").value) + "&pos=" + codpar(sLadoMsg) + "&resp=" + codpar((bEsInterlocutorProyecto) ? "1" : "0");
        modalDialog.Show(sPantalla, self, sSize(630, 240))
	        .then(function (ret) {
                if (ret != null && ret == "T") {
                    bAlgunCambio = true;
                    getDialogo();
                } else {
                    ocultarProcesando();
                }
                bEsGestorDialogo = bEsGestorDialogoOriginal;
                bEsInterlocutorProyecto = bEsInterlocutorProyectoOriginal;
	        }); 
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir nuevo mensaje.", e.message);
    }
}

function getDialogo() {
    try {
        var js_args = "getDialogo@#@";
        js_args += $I("hdnIdDialogo").value;

        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a actualizar el diálogo.", e.message);
    }
}

function mostrarDocumentos() {
    try {
        if ($I("hdnIdDialogo").value == "" || $I("hdnIdDialogo").value == "-1") {
            //alert("Debe indicar número de proyecto");
            return false;
        }
        mostrarProcesando();
        var sModoAcceso = "W";
        if (nIDFicepi != nIDFicepiEntrada || $I("hdnIdEstado").value == "4" || $I("hdnIdEstado").value == "5")
            sModoAcceso = "R";
            
        var sPantalla = strServer + "Capa_Presentacion/ECO/DialogoAlertas/DetalleDocs/Default.aspx?nID=" + codpar($I("hdnIdDialogo").value) + "&sMA=" + codpar(sModoAcceso);
        //var ret = window.showModalDialog(sPantalla, self, sSize(940, 630));
        modalDialog.Show(sPantalla, self, sSize(940, 630))
	        .then(function(ret) {
                if (ret == "0") {
                    //$I("imgDocFact").src = strServer + "Images/imgCarpeta32.png";
                    $I("imgDocFact").setAttribute("src", strServer + "Images/imgCarpeta32.png");
                    $I("hdnHayDocs").value = "N";
                    $I("imgDocFact").title = "No existen documentos asociados";
                }
                else {
                    //$I("imgDocFact").src = strServer + "Images/imgCarpetaDoc32.png";
                    $I("imgDocFact").setAttribute("src", strServer + "Images/imgCarpetaDoc32.png");
                    $I("hdnHayDocs").value = "S";
                    $I("imgDocFact").title = "Existen documentos asociados";
                }
                ocultarProcesando();
	        }); 
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la ventana de la documentación", e.message);
    }
}

function addMensaje() {//getLadoMsg()
    try {
        var sLadoMsg = "";
        //Si el usuario es tanto gestor como interlocutor, preguntaremos en calidad de qué quiere escribir.
        if (bEsGestorDialogo && bEsInterlocutorProyecto) {
            var sPantalla = strServer + "Capa_Presentacion/ECO/DialogoAlertas/Detalle/getRol.aspx";
            //var ret = window.showModalDialog (sPantalla, self, sSize(290, 140));
            modalDialog.Show(sPantalla, self, sSize(290, 140))
	        .then(function(ret) {
	            if (ret == null || typeof (ret) == "undefined") {
	                sLadoMsg = "";
	                mmoff("War", "Debes seleccionar un rol", 200);
	            }
	            else {
	                if (ret == "G") {
	                    bEsInterlocutorProyecto = false;
	                } else {
	                    bEsGestorDialogo = false;
	                }
	                if (
                        (($I("hdnEntePromotor").value == "S" || $I("hdnEntePromotor").value == "D") && bEsInterlocutorProyecto)
                        ||
                        (($I("hdnEntePromotor").value != "S" && $I("hdnEntePromotor").value != "D") && !bEsInterlocutorProyecto)
                        )
	                    sLadoMsg = "D";
	                else
	                    sLadoMsg = "I";
	                addMensajeAux(sLadoMsg);
	            }
	        }); 
        } 
        //alert("Promotor: " + $I("hdnEntePromotor").value + "\nResp. Proy: " + $I("hdnIdResponsableProy").value + "\nUsuario: " + sUsuarioEmpleado);
        else if (
                (($I("hdnEntePromotor").value == "S" || $I("hdnEntePromotor").value == "D") && bEsInterlocutorProyecto)
                ||
                (($I("hdnEntePromotor").value != "S" && $I("hdnEntePromotor").value != "D") && !bEsInterlocutorProyecto)
                ) {
            sLadoMsg = "D";
            addMensajeAux(sLadoMsg);
        }
        else {
            sLadoMsg = "I";
            addMensajeAux(sLadoMsg);
        }
            
    }
    catch (e) {
        mostrarErrorAplicacion("Error al determinar en qué lado se añadirá el mensaje.", e.message);
    }
}

function grabar() {
    try {
        if ($I("txtFLR").value != "" && cadenaAfecha(sToday).getTime() > cadenaAfecha($I("txtFLR").value).getTime()) {
            mmoff("War", "La fecha límite de respuesta no puede ser anterior a hoy.", 380, null, null, null, 30);
            $I("txtFLR").value = $I("txtFLR").getAttribute("oValue");
            return;
        }

        var js_args = "grabar@#@";
        js_args += $I("hdnIdDialogo").value + "@#@";
        js_args += $I("hdnIdDAlerta").value + "@#@";
        js_args += $I("txtFLR").value;

        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a actualizar los datos.", e.message);
    }
}