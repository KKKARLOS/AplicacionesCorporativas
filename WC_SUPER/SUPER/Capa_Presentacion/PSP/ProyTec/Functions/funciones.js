var bHayCambios = false;
var bFechaModificada = false;
var bSaliendo = false;
var aFilaT;
var bGetPE = false;
var bGetPT = false;
var bLimpiar = false;
var bBitacora = false;
var aAEsTareas;
var aCamposTareas;

function init() {
    try {
        if (!mostrarErrores()) {
            iniciarPestanas();
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
            setCamposEstadoBusq();
            setEstadoDatosAsignacion(false);
            return;
        }
        iniciarPestanas();
        setCamposEstadoBusq();
        setOp($I("btnNuevo"), 30);

        if ($I("hdnIDPT").value != "0")
            setIconoBitacora();

        //Acciones en función del modo de entrada Crear o Buscar
        if (gsModo == "C") {
            $I("lblDesPT").className = "";
            $I("lblDesPT").onclick = null;

            $I("btnNuevo").children[1].innerText = "Nuevo";
            $I("btnNuevo").children[1].title = "Nuevo proyecto técnico";
            //$I("hdnAcceso").value="W";
        }
        else {
            if (gsModo == "B") {
                $I("lblDesPT").className = "enlace";
                $I("btnNuevo").children[1].innerText = "Limpiar";
                $I("btnNuevo").children[1].title = "Limpia datos de la pantalla";
                setOp($I("btnNuevo"), 100);
            }
        }
        if ($I("hdnEstr").value == "S") {
            $I("lblProy").className = "";
            $I("lblProy").onclick = null;
            $I("lblDesPT").className = "";
            $I("lblDesPT").onclick = null;
        }
        //Acciones si entro a un PT que no existe todavía
        if (($I("txtIdPT").value == "") || ($I("txtIdPT").value == "0")) {
            setOp($I("btnEliminar"), 30);
        }
        else {
            //Cargo la lista de AEs utilizados en las tareas del PT
            aAEsTareas = $I("hdnLstAEsTareas").value.split(",");
            //Cargo la lista de Campos utilizados en las tareas del PT
            aCamposTareas = $I("hdnLstCamposTareas").value.split(",");
            //Sino hay consumos habilito el botón de borrado
            if (($I("txtConHor").value == "") || ($I("txtConHor").value == "0") || ($I("txtConHor").value == "0,00"))
                setOp($I("btnEliminar"), 100);
            else
                setOp($I("btnEliminar"), 30);
        }
        //$I("cboTarifa").disabled=true;
        //$I("txtIndicaciones").disabled=true;
        if ($I("hdnAcceso").value == "V") {
            $I("txtDesPT").onkeyup = function() { };
            $I("txtDescripcion").onkeyup = function() { };
        }

        comprobarAEObligatorios();

        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesPT").value;
        sEstado = $I("cboEstado").value;
        sFIV = $I("txtValIni").value;
        sFFV = $I("txtValFin").value;
        sPresupuesto = $I("txtPresupuesto").value;

        //Variable para el control de estado.
        sEstadoOrig = sEstado;
        //Carga del combo de estado en función del estado del proyecto
        cargarComboEstado(sEstado);
        if ($I("cboAccesoIAP").value == "")
            $I("cboAccesoIAP").value = "X";

        if ($I("hdnAcceso").value != "W") {
            setOp($I("btnEliminar"), 30);
        }
        $I("txtFIPRes").onclick = null;
        $I("txtFFPRes").onclick = null;

        
        if ($I("hdnNivelPresupuesto").value == "P") {
            $(".ocultarcapa").removeClass();
            $I("idFieldsetIAP").style.height = "155px";
            $I("idFieldsetSituacion").style.height = "";

            if ($I("chkAvanceAuto").checked) clickAvanceAutomatico();
            $I("txtPresupuesto").onkeyup = function () { calcularProducido(); aG(0); };
            $I("txtAvanReal").onkeyup = function () { calcularProducido(); aG(0); };

        } else {
            $I("txtPresupuesto").readOnly = true;
            $I("txtAvanReal").readOnly = true;
            $I("chkAvanceAuto").disabled = true;
        }

        setEstadoDatosAsignacion(false);
        bCambios = false;
        bHayCambios = false;
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
    var strRetorno = "F";
    bSalir = false;
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T";
    }
    strRetorno += "@#@P@#@" + sDescripcion + "@#@" + sEstado + "@#@" + sFIV + "@#@" + sFFV + "@#@" + bFechaModificada + "@#@" + $I("hdnRecargar").value + "@#@" + sPresupuesto;

    var returnValue = strRetorno; //Devuelvo la descripción del P.T. y su estado
    modalDialog.Close(window, returnValue);
}
function unload() {
    if (!bSaliendo) 
        salir();
}
function salir() {
    bSalir = false;
    bSaliendo = true;

    if ($I("hdnAcceso").value != "R") {
        if (bCambios && intSession > 0) {
            var sMsg = vigenciaModificada();
            var iWidth = 500;
            if (sMsg == "") {
                iWidth = 320;
                sMsg = "Datos modificados. ¿Deseas grabarlos?";
            }
            jqConfirm("", sMsg, "", "", "war", iWidth).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    LLamarGrabar();
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
    var strRetorno = "F";
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T";
    }
    strRetorno += "@#@P@#@" + sDescripcion + "@#@" + sEstado + "@#@" + sFIV + "@#@" + sFFV + "@#@" + bFechaModificada + "@#@" + $I("hdnRecargar").value + "@#@" + sPresupuesto;

    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
        if (aResul[3] != null)
            mostrarDeNuevoRecurso(aResul[3]);
    } else {
        switch (aResul[0]) {           
            case "ponerRTPT":
                $I("divRTPTAsignados").children[0].innerHTML = aResul[2];
                $I("divRTPTAsignados").scrollTop = 0;
                bCambios = false;
                scrollTablaRTPTAsig();
                ocultarProcesando();
                break;
            case "rtpt":
                $I("divRTPTcandidato").children[0].innerHTML = aResul[2];
                $I("divRTPTcandidato").scrollTop = 0;
                scrollTablaRTPT();
                ocultarProcesando();
                break;
            case "tecnicos":
                $I("divRelacion").children[0].innerHTML = aResul[2];
                $I("divRelacion").scrollTop = 0;
                //tratarTecnicosDeBaja();
                scrollTablaProf();
                $I("txtApellido").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                actualizarLupas("tblTitRec", "tblRelacion");
                ocultarProcesando();
                break;
            case "tecnicosPool":
                $I("divRelacionPool").children[0].innerHTML = aResul[2];
                $I("divRelacionPool").scrollTop = 0;
                //tratarTecnicosDeBaja();
                scrollTablaPool();
                $I("txtApe1Pool").value = "";
                $I("txtApe2Pool").value = "";
                $I("txtNomPool").value = "";
                ocultarProcesando();
                break;
            case "grabar":
                bCambios = false;
                setIconoBitacora();
                //Inicializo los campos de estado de las listas de recursos asociados  y RTPTs
                for (var i = 0; i < aRecursos.length; i++) aRecursos[i].opcionBD = "";

                //if ((aPestGral[1].bModif==true) && (aPestProf[0].bModif==true)){
                if (aPestGral[1].bModif == true) {
                    if (aPestProf[0].bModif == true) {
                        if ($I("tblAsignados") != null) {
                            var aRecur = FilasDe("tblAsignados");
                            for (var i = aRecur.length - 1; i >= 0; i--) {
                                if (aRecur[i].getAttribute("bd") == "D") {
                                    $I("tblAsignados").deleteRow(i);
                                } else {
                                    mfa(aRecur[i], "N");
                                    aRecur[i].style.cursor = "pointer";
                                }
                            }
                        }
                        //Estblece el nº de tareas en las que está asignado cada profesional
                        var aRecTareas = aResul[5].split("##");
                        for (var i = 0; i < aRecTareas.length; i++) {
                            var aDatos = aRecTareas[i].split("//");
                            for (var x = 0; x < aRecur.length; x++) {
                                if (aDatos[0] == aRecur[x].id) {
                                    aRecur[x].cells[4].innerText = aDatos[1];
                                    break;
                                }
                            }
                        }
                    }
                    var bProfBorrados = false;
                    if (aPestProf[1].bModif == true) {
                        if ($I("tblAsignados3") != null) {
                            var aRecur = FilasDe("tblAsignados3");
                            for (var i = aRecur.length - 1; i >= 0; i--) {
                                if (aRecur[i].getAttribute("bd") == "D") {
                                    $I("tblAsignados3").deleteRow(i);
                                    bProfBorrados = true;
                                } else {
                                    mfa(aRecur[i], "N");
                                }
                            }
                        }
                        if ($I("tblPoolGF") != null) {
                            var aRecur = FilasDe("tblPoolGF");
                            for (var i = aRecur.length - 1; i >= 0; i--) {
                                if (aRecur[i].getAttribute("bd") == "D") {
                                    $I("tblPoolGF").deleteRow(i);
                                } else {
                                    mfa(aRecur[i], "N");
                                }
                            }
                        }
                    }
                    if (bProfBorrados) scrollTablaPoolAsig();
                }
                if (aPestGral[2].bModif == true) {
                    var aRTPT = FilasDe("tblAsignados2");
                    for (var i = aRTPT.length - 1; i >= 0; i--) {
                        if (aRTPT[i].getAttribute("bd") == "D") {
                            $I("tblAsignados2").deleteRow(i);
                        } else {
                            mfa(aRTPT[i], "N");
                        }
                    }
                }
                if (aPestGral[3].bModif == true) {
                    if (aPestAvanza[0].bModif == true) {
                        var aFila = FilasDe("tblAET");
                        for (var i = aFila.length - 1; i >= 0; i--) {
                            if (aFila[i].getAttribute("bd") == "D") {
                                $I("tblAET").deleteRow(i);
                            } else {
                                mfa(aFila[i], "N");
                            }
                        }
                    }
                    if (aPestAvanza[1].bModif == true) {
                        var aFila = FilasDe("tblCampos");
                        for (var i = aFila.length - 1; i >= 0; i--) {
                            if (aFila[i].getAttribute("bd") == "D") {
                                $I("tblCampos").deleteRow(i);
                            } else {
                                mfa(aFila[i], "N");
                            }
                        }
                    }
                }

                BorrarFilasDe("tblAEVD");

                if (aResul[6] != "") {//viene el código de PT
                    $I("txtIdPT").value = aResul[6].ToString("N",7,0);
                    $I("hdnIDPT").value = aResul[6];
                }
                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);
                //Pongo las variables de pestaña modificada a false
                reIniciarPestanas();
                //Sino hay consumos habilito el botón de borrado
                if (($I("txtConHor").value == "") || ($I("txtConHor").value == "0") || ($I("txtConHor").value == "0,00"))
                    setOp($I("btnEliminar"), 100);
                else
                    setOp($I("btnEliminar"), 30);
                setTimeout("ocultarProcesando();", 250); //para que de tiempo a grabar y actualizar "bCambios";
                mmoff("Suc", "Grabación correcta", 160);
                reestablecer();

                if (bSalir)
                    setTimeout("salir();", 50);
                else {
                    bFechaModificada = false;
                    if (bGetPE) {
                        bGetPE = false;
                        setTimeout("LLamarObtenerProyectos();", 50);
                    } 
                    else {
                        if (bGetPT) {
                            bGetPT = false;
                            setTimeout("LLamarObtenerPTs();", 50);
                        }
                        else {
                            if (bLimpiar) {
                                bLimpiar = false;
                                setTimeout("llamarLimpiar();", 50);
                            }
                            else {
                                if (bBitacora) {
                                    bBitacora = false;
                                    setTimeout("LlamarMostrarBitacora();", 50);
                                }
                            }
                        }
                    }
                }
                break;
            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                setEstadoBotonesDoc(aResul[3], aResul[4]);
                ocultarProcesando();
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
            case "borrar":
                bCambios = false;
                ocultarProcesando();
                if ($I("hdnEstr").value == "S") {
                    var returnValue = "borrar@#@";
                    //setTimeout("window.close();", 250); //para que de tiempo a grabar y actualizar "bCambios";
                    modalDialog.Close(window, returnValue);
                }
                else {
                    setTimeout("limpiar();", 250); //para que de tiempo a grabar y actualizar "bCambios";
                }
                break;
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                ocultarProcesando();
                break;
            case "getDatosPestanaProf":
                RespuestaCallBackPestanaProf(aResul[2], aResul[3], aResul[4]);
                ocultarProcesando();
                break;
            case "getDatosPestanaAvan":
                RespuestaCallBackPestanaAvan(aResul[2], aResul[3], aResul[4]);
                ocultarProcesando();
                break;
            case "getFase":
            case "getActiv":
                insertarFilasEnTablaDOM("tblTareas", aResul[2], iFila + 1);

                aFilaT = FilasDe("tblTareas");
                $I("tblTareas").rows[iFila].cells[0].getElementsByTagName("img")[0].src = strServer + "images/minus.gif";
                $I("tblTareas").rows[iFila].setAttribute("desplegado", "1");
                //if (bMostrar) setTimeout("MostrarTodo();", 10);
                //else 
                ocultarProcesando();
                break;
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360); ;
                    break;
                }
                $I("hdnT305IdProy").value = aResul[2];
                $I("hdnIdPE").value = aResul[3].ToString("N", 9, 0);
                $I("txtPE").value = aResul[4];
                $I("hdnCRActual").value = aResul[5];
                $I("hdnDesCRActual").value = aResul[6];
                $I("txtEstado").value = aResul[7];
                switch ($I("txtEstado").value) {
                    case "A":
                        $I("imgEstProy").src = "../../../images/imgIconoProyAbierto.gif";
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C":
                        $I("imgEstProy").src = "../../../images/imgIconoProyCerrado.gif";
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P":
                        $I("imgEstProy").src = "../../../images/imgIconoProyPresup.gif";
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H":
                        $I("imgEstProy").src = "../../../images/imgIconoProyHistorico.gif";
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }
                if (aResul[8] == "S") {//AdmiteRecursosPST
                    $I("chkHeredaCR").disabled = false;
                    if ($I("imgDelBajaPE") != null) $I("imgDelBajaPE").style.visibility = "visible";
                }
                else {
                    $I("chkHeredaCR").disabled = true;
                    if ($I("imgDelBajaPE") != null) $I("imgDelBajaPE").style.visibility = "hidden";
                }
                $I("txtIdPT").value = "";
                $I("hdnAcceso").value = "W";
                //Compruebo el estado del proyecto
                if ($I("txtEstado").value == "C" || $I("txtEstado").value == "H") {
                    $I("hdnAcceso").value = "R";  //modoLectura
                    bLectura = true;
                    if (gsModo == "C") {
                        mmoff("War", "El estado del proyecto económico no permite modificaciones", 400);
                        bCambios = false;
                        break;
                    }
                }
                else {
                    if (bLectura) {
                        $I("hdnAcceso").value = "R"; //modoLectura
                    }
                    else {
                        if (bRTPT) $I("hdnAcceso").value = "V"; //es solo RTPT
                        else setOp($I("btnNuevo"), 100);
                    }
                    if (gsModo == "C") {//Si queremos crear un nuevo PT 
                        if (bLectura) {//y el proyecto solo tiene acceso en modo lectura
                            mmoff("War", "No dispones de permisos para modificar el proyecto económico", 400);
                            bCambios = false;
                            break;
                        }
                        else {
                            //el usuario es solo RTPT
                            if (bRTPT == "1") {
                                $I("hdnAcceso").value = "R";
                                mmoff("War", "Un RTPT no puede crear proyecto técnicos", 330);
                                bCambios = false;
                                break;
                            }
                        }
                    }
                    if (gsModo == "C") {
                        //Pongo el pool de RTPT´s del PE o (si es vacío) el usuario actual como RTPT por defecto
                        setTimeout("ponerRTPT();", 20);
                        aPestGral[2].bModif = true;
                    }
                }
                ocultarProcesando();
                break;
            case "getRecursos":
                $I("divAsignados").children[0].innerHTML = aResul[2];
                $I("divAsignados").scrollTop = 0;
                scrollTablaProfAsig();
                actualizarLupas("tblTitRecAsig", "tblAsignados");
                ocultarProcesando();
                break;
            case "getRecursosPool":
                $I("divPoolProf").children[0].innerHTML = aResul[2];
                $I("divPoolProf").scrollTop = 0;
                scrollTablaPoolAsig();
                ocultarProcesando();
                break;
            case "buscarPE":
                if (aResul[2] == "") {
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);
                    setTimeout("limpiar()", 20);
                } else {
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2) {
                        var aDatos = aProy[0].split("##")
                        $I("hdnT305IdProy").value = aDatos[0];
                        if (aDatos[1] == "1") {
                            bLectura = true;
                        } else {
                            bLectura = false;
                        }
                        if (es_administrador == "SA" || es_administrador == "A") {
                            bRTPT = false;
                        }
                        else {
                            if (aDatos[2] == "1") {
                                bRTPT = true;
                            } else {
                                bRTPT = false;
                            }
                        }
                        setTimeout("recuperarDatosPSN();", 20);
                    } else {
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "buscarPT":
                if (aResul[2] == "") {
                    limpiar();
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto técnico no existe o está fuera de tu ámbito de visión.", 360);
                }
                else {
                    limpiarPT();
                    $I("hdnIDPT").value = dfn($I("txtIdPT").value);
                    $I("txtIdPT").value = $I("txtIdPT").value.ToString("N", 7, 0);
                    $I("hdnT305IdProy").value = aResul[2];
                    $I("hdnIdPE").value = aResul[3].ToString("N", 7, 0);
                    $I("txtPE").value = aResul[4];
                    $I("txtDesPT").value = aResul[5];
                    setTimeout("cargarPT();", 20);
                }
                ocultarProcesando();
                break;
            case "mostrarTareasConAE":
                ocultarProcesando();
                mmoff("InfPer", "Relación de tareas y su valor en el atributo estadístico:\n\n" + aResul[2], 600);
                break;
            case "cargarCamposPorAmbito":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                break;
            case "mostrarTareasConCampo":
                ocultarProcesando();
                mmoff("InfPer", "Relación de tareas y su valor en el campo libre:\n\n" + aResul[2], 600);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
    }
}

function ponerRTPT() {
    try {
        mostrarProcesando();
        var js_args = "ponerRTPT@#@" + $I("hdnT305IdProy").value + "@#@" + $I("hdnCRActual").value; //aProy[nIndiceProy][0];
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
var sAmb = "";
function seleccionAmbito(strRblist) {
    try {
        if ($I("hdnIdPE").value == "") {
            mmoff("Inf", "Debes indicar proyecto económico", 230);
            return;
        }
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else {
            //acción a realizar
            $I("divRelacion").children[0].innerHTML = "<table></table>";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambAp").style.display = "none";

            switch (sOp) {
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    $I("txtCR").value = $I("hdnDesCRActual").value;
                    mostrarRelacionTecnicos("C", $I("hdnCRActual").value, "", "");
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
                case "P":
                    //mostrarRelacionTecnicos("P", $I("hdnIDPT").value,"","");
                    mostrarRelacionTecnicos("P", dfn($I("hdnIdPE").value), "", "");
                    break;
            }
            sAmb = sOp;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
var sAmb2 = "";
function seleccionAmbito2(rdbAmbito2) {
    try {
        if ($I("hdnIdPE").value == "") {
            mmoff("Inf", "Debes indicar proyecto económico", 230);
            return;
        }
        var sOp = getRadioButtonSelectedValue(rdbAmbito2, true);
        if (sOp == sAmb2) return;
        else {
            //acción a realizar
            $I("divRTPTcandidato").children[0].innerHTML = "";
            $I("ambCR2").style.display = "none";
            $I("ambGF2").style.display = "none";
            $I("ambAp2").style.display = "none";

            switch (sOp) {
                case "A":
                    $I("ambAp2").style.display = "block";
                    break;
                case "C":
                    $I("ambCR2").style.display = "block";
                    $I("txtCR2").value = $I("hdnDesCRActual").value;
                    mostrarRelacionTecnicos2("C", $I("hdnCRActual").value, "", "");
                    break;
                case "G":
                    $I("ambGF2").style.display = "block";
                    break;
                case "P":
                    //mostrarRelacionTecnicos2("P", $I("hdnIDPT").value,"","");
                    mostrarRelacionTecnicos2("P", dfn($I("hdnIdPE").value), "", "");
                    break;
                case "T":
                    mostrarRelacionTecnicos2("T", $I("hdnIDPT").value, "", "");
                    break;
            }
            sAmb2 = sOp;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function vigenciaModificada() {
    var sRes = "";
    if ($I("txtValIni").value != "" || $I("txtValFin").value != "") {
        if (bFechaModificada && $I("txtIdPT").value != "") {
            sRes = "Se han modificado las fechas de vigencia.<br />Esta acción desencadena la actualización de las fechas de vigencia de ";
            sRes += "las tareas que dependen de este proyecto técnico.<br /><br />";
            sRes += "¿Deseas continuar?";
        }
    }
    return sRes;
}

function clickAvanceAutomatico() {
    try {
        if ($I("chkAvanceAuto").checked) {
            $I("txtAvanReal").value = $I("txtAvanTeo").value;
            $I("txtAvanReal").readOnly = true;
            calcularProducido();
        }
        else {
            $I("txtAvanReal").readOnly = false;
        }
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el Cálculo automatico", e.message);
    }
}

function pulsarTeclaAvanceReal(e) {
    try {
        if ($I("chkAvanceAuto").checked) {
            if (e.keyCode) e.keyCode = 0;
            else e.which = 0;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el avance real", e.message);
    }
}

function soltarTeclaAvanceReal() {
    try {
        if ($I("chkAvanceAuto").checked) return;
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el avance real", e.message);
    }
}

function calcularProducido() {
    try {     

        if ($I("chkAvanceAuto").checked) {
            if ($I("txtConHor").value != "" && $I("txtPresupuesto").value != "" && $I("txtPREst").value != "") {
                var nHorasIAP = parseFloat(dfn($I("txtConHor").value));
                var nHorasPrev = parseFloat(dfn($I("txtPREst").value));
                var nPR = parseFloat(dfn($I("txtPresupuesto").value));
                if (nHorasIAP != 0 && nPR != 0 && nHorasPrev != 0) {
                    $I("txtProducido").value = (nHorasIAP * nPR / nHorasPrev).ToString("N");
                } else {
                    $I("txtProducido").value = "0,00";
                }
            } else {
                $I("txtProducido").value = "0,00";
            }
        } else {
            if ($I("txtAvanReal").value != "" && $I("txtPresupuesto").value != "") {
                var nAR = parseFloat(dfn($I("txtAvanReal").value));
                var nPR = parseFloat(dfn($I("txtPresupuesto").value));
                if (nAR != 0 && nPR != 0) {
                    $I("txtProducido").value = (nAR * nPR / 100).ToString("N");
                } else {
                    $I("txtProducido").value = "0,00";
                }
            } else {
                $I("txtProducido").value = "0,00";
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular el importe producido", e.message);
    }
}

function grabar() {
    try {
        var sMsg = vigenciaModificada();
        if (sMsg != "") {
            jqConfirm("", sMsg, "", "", "war", 500).then(function (answer) {
                if (answer)
                    LLamarGrabar();
            });
        }
        else LLamarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la fase", e.message);
    }
}
function LLamarGrabar() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        if (getOp($I("btnGrabar")) != 100) return;
        //Si estoy en modo Busqueda debo tener codigo de PT
        if (gsModo == "B") {
            if ($I("hdnIDPT").value == "" || $I("hdnIDPT").value == "0" || $I("hdnIDPT").value == "-1") {
                mmoff("War", "Debes seleccionar proyecto técnico", 230);
                return;
            }
        }
        if (!comprobarDatos()) return;
        //Desactivo los campos de asignación a técnico 
        //        $I("cboTarifa").disabled=true;
        //        $I("txtFIPRes").disabled=true;
        //        $I("txtFFPRes").disabled=true;
        //        $I("txtIndicaciones").disabled=true;

        var js_args = "grabar@#@" + $I("hdnIDPT").value + "##" + $I("hdnCRActual").value + "##" + $I("hdnT305IdProy").value + "@#@";
        js_args += grabarP0(); //datos generales
        js_args += "@#@";
        js_args += grabarP1(); //profesionales
        js_args += "@#@";
        js_args += grabarP2(); //responsables
        js_args += "@#@";
        js_args += grabarP3(); //avanzado

        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesPT").value;
        sEstado = $I("cboEstado").value;
        sFIV = $I("txtValIni").value;
        sFFV = $I("txtValFin").value;
        sPresupuesto = $I("txtPresupuesto").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del proyecto técnico-2", e.message);
    }
}
function grabarP0() {
    var js_args = "";
    if (aPestGral[0].bModif) {
        js_args += Utilidades.escape($I("txtDesPT").value) + "##";  //0
        js_args += $I("cboEstado").value + "##"; //1

        if ($I("chkObligaEst").checked) js_args += "1##"; //2
        else js_args += "0##"; //5

        if ($I("hdnOrden").value == "") $I("hdnOrden").value = 1000;
        js_args += $I("hdnOrden").value + "##"; //3
        js_args += $I("txtIdPST").value + "##"; //4
        js_args += Utilidades.escape($I("txtDescripcion").value) + "##"; //5
        if (bFechaModificada) js_args += "1##"; //6
        else js_args += "0##"; //6
        js_args += $I("txtValIni").value + "##"; //7
        js_args += $I("txtValFin").value + "##"; //8
        if ($I("chkHeredaCR").checked) js_args += "1##"; //9
        else js_args += "0##"; //9
        if ($I("chkHeredaPE").checked) js_args += "1##"; //10
        else js_args += "0##"; //10
        if ($I("cboAccesoIAP").value == "")
            $I("cboAccesoIAP").value = "X";
        js_args += $I("cboAccesoIAP").value + "##"; //11
        if ($I("hdnNivelPresupuesto").value == "P") {
            js_args += $I("txtPresupuesto").value + "##"; //12
            //Si el avance es automatico el avance real se grabará como null
            if ($I("chkAvanceAuto").checked) {
                js_args += "##"; //13
                js_args += "1##"; //14
            }
            else {
                js_args += $I("txtAvanReal").value + "##"; //13
                js_args += "0##"; //14
            }
        }
        else {
            js_args += "####0##"; //12,13,14
        }
        js_args += Utilidades.escape($I("txtObservaciones").value); //15

    }
    return js_args;
}
function grabarP1() {
    var js_args = "";
    if (aPestGral[1].bModif) {
        if (aPestProf[0].bModif) {
            //Control de los tecnicos asociados al proyecto técnico
            //Primero recojo los que ya pertencían al PT y ahora se Asignan/Desasignan completamente
            if ($I("tblAsignados") != null) {
                var aRecur = FilasDe("tblAsignados");
                for (var i = 0; i < aRecur.length; i++) {
                    if (aRecur[i].getAttribute("ac") != "") {
                        bCambioRecursos = true;
                        js_args += aRecur[i].getAttribute("ac") + "##" + aRecur[i].id + "##########///";
                    }
                }
            }
            //Luego recojo los nuevos tecnicos que los tengo en la lista de objetos oculta 
            for (var i = 0; i < aRecursos.length; i++) {
                if (aRecursos[i].opcionBD != "") {
                    bCambioRecursos = true;
                    js_args += aRecursos[i].opcionBD + "##" + aRecursos[i].idRecurso + "##";
                    js_args += aRecursos[i].ffe + "##";
                    js_args += aRecursos[i].idTarifa + "##";
                    js_args += aRecursos[i].indicaciones + "##";
                    js_args += aRecursos[i].fip + "##";
                    js_args += aRecursos[i].bNotifExceso + "///";
                }
            }
        }
        js_args += "@#@";
        if (aPestProf[1].bModif) {
            //Control de los Grupos Funcionales asociados al pool del proyecto técnico
            var aPoolGF = FilasDe("tblPoolGF");
            for (var i = 0; i < aPoolGF.length; i++) {
                if (aPoolGF[i].getAttribute("bd") != "") {
                    js_args += aPoolGF[i].getAttribute("bd") + "##" + aPoolGF[i].id + "///";
                }
            }
            js_args += "@#@";
            //Control de los profesionales asociados al pool del proyecto técnico
            var aPoolProf = FilasDe("tblAsignados3");
            for (var i = 0; i < aPoolProf.length; i++) {
                if (aPoolProf[i].getAttribute("bd") != "") {
                    js_args += aPoolProf[i].getAttribute("bd") + "##" + aPoolProf[i].id + "///";
                }
            }
        }
        else {//No se ha tocado nada en la subpestaña Pool
            js_args += "@#@";
        }
    }
    else {//No se ha tocado nada en la pestaña
        js_args += "@#@@#@";
    }
    return js_args;
}
function grabarP2() {
    var js_args = "";
    if (aPestGral[2].bModif) {
        //Control de los RTPT´s asociados al proyecto técnico
        var aRTPT = FilasDe("tblAsignados2");
        for (var i = 0; i < aRTPT.length; i++) {
            if (aRTPT[i].getAttribute("bd") != "") {
                //bCambioRTPT=true;
                js_args += aRTPT[i].getAttribute("bd") + "##" + aRTPT[i].id + "///";
            }
        }
    }
    return js_args;
}
function grabarP3() {
    var js_args = "";
    if (aPestGral[3].bModif) {
        if (aPestAvanza[0].bModif) {
            //Control de atributos estadísticos
            if ($I("tblAET") != null) {
                var aFila = FilasDe("tblAET");
                for (var i = 0; i < aFila.length; i++) {
                    if (aFila[i].getAttribute("bd") != "") {
                        js_args += aFila[i].getAttribute("bd") + "##";
                        js_args += $I("hdnIDPT").value + "##"; //nº proyecto técnico
                        js_args += aFila[i].id + "##";
                        js_args += aFila[i].getAttribute("vae") + "///";
                    }
                }
            }
        }
        js_args += "@#@";
        if (aPestAvanza[1].bModif) {
            //Control de los campos asociados a la tarea
            var aCampos = FilasDe("tblCampos");
            for (var i = 0; i < aCampos.length; i++) {
                if (aCampos[i].getAttribute("bd") != "") {
                    js_args += aCampos[i].getAttribute("bd") + "##" + aCampos[i].id + "##" + aCampos[i].cells[3].children[0].value + "##";
                    js_args += aCampos[i].getAttribute("tipodato");
                    if (aCampos[i].getAttribute("tipodato") == "H") js_args += "##" + aCampos[i].cells[4].children[0].value + "##" + aCampos[i].cells[5].children[0].value + "##" + aCampos[i].cells[6].children[0].value + "##";
                    js_args += "///";
                }
            }
        }
    }
    else
        js_args += "@#@";

    return js_args;
}

function comprobarDatos() {
    try {
        if (($I("hdnT305IdProy").value == "") || ($I("hdnT305IdProy").value == "0") || ($I("hdnT305IdProy").value == "-1")) {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar proyecto económico", 230);
            return false;
        }
        if (($I("hdnIdPE").value == "") || ($I("hdnIdPE").value == "0") || ($I("hdnIdPE").value == "-1")) {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar proyecto económico", 230);
            return false;
        }
        if ($I("txtDesPT").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre del proyecto técnico", 290);
            return false;
        }
        //La fecha de fin de vigencia no puede ser anterior a la de inicio
        if (!fechasCongruentes($I("txtValIni").value, $I("txtValFin").value)) {
            tsPestanas.setSelectedIndex(0);
            $I("txtValFin").select();
            mmoff("War", "La fecha de fin de vigencia debe ser posterior a la de inicio", 380);
            return false;
        }

        //Validaciones de los datos de los recursos.
        for (var i = 0; i < aRecursos.length; i++) {
            if (aRecursos[i].opcionBD != "D") {
                //Comprobar que la fecha de fin prevista no sea anterior a la de inicio
                if (!fechasCongruentes(aRecursos[i].fip, aRecursos[i].ffe)) {
                    tsPestanas.setSelectedIndex(1);
                    var aFilaAux = FilasDe("tblAsignados");
                    for (var x = 0; x < aFilaAux.length; x++) {
                        if (aFilaAux[x].id == aRecursos[i].idRecurso) {
                            ms(aFilaAux[x]);
                            break;
                        }
                    }
                    mmoff("War", "Profesional asignado: " + aRecursos[i].nombre + ".\nLa fecha de fin prevista no puede ser anterior a la de inicio.", 350);
                    return false;
                }
            }
        }
        //Validaciones de los RTPT´s.
        if (aPestGral[2].bModif) {
            var iNumRtpts = 0;
            if ($I("tblAsignados2") != null) {
                var aFila = FilasDe("tblAsignados2");
                for (var i = 0; i < aFila.length; i++) {
                    if ((aFila[i].style.display != "none") && (aFila[i].getAttribute("bd") != "D")) {
                        iNumRtpts++;
                        break;
                    }
                }
            }
            //            if (iNumRtpts==0 && $I("txtIdPT").value !=""){
            //                alert("Debe existir al menos un responsable técnico del proyecto técnico");
            //                return false;
            //            }
        }
        //Validaciones de los atributos estadísticos.
        if (aPestGral[3].bModif) {
            if (aPestAvanza[0].bModif) {
                var aFila = FilasDe("tblAET");
                for (var i = 0; i < aFila.length; i++) {
                    if (aFila[i].style.display == "none") continue;
                    if (aFila[i].cells[3].innerText == "" && aFila[i].getAttribute("bd") != "D") {
                        tsPestanas.setSelectedIndex(3);
                        ms(aFila[i]);
                        mmoff("War", "Debe asignar un valor al atributo estadístico '" + aFila[i].cells[2].innerText + "'", 320);
                        return false;
                    }
                }
            }
        }
        //Validación de las OTC´s de las tareas
        //Lo quito porque se va a controlar por trigger
        //        var sOtcTarea,sOtcPT,sDesOtcTarea;
        //        sOtcPT=$I("txtIdPST").value;
        //        if (sOtcPT != ""){
        //            var aTareas = FilasDe("tblTareas");
        //            for (var i=0;i<aTareas.length;i++){
        //                if (aTareas[i].style.display == "none") continue;
        //                sOtcTarea=aTareas[i].otc;
        //                if (sOtcTarea != "0"){
        //                    if (sOtcPT != sOtcTarea){
        //                        sDesOtcTarea=aTareas[i].cells[13].innerText;
        //                        alert("Existe al menos una tarea con OTC="+sDesOtcTarea+ "\ndiferente a la seleccionada para el proyecto técnico.\nNo se permite la grabación.");
        //                        return false;
        //                    }
        //                }
        //            }
        //        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function obtenerCR() {
    try {
        var aOpciones;

        if ($I("hdnEsReplicable").value == "0") {
            mmoff("Inf", "El proyecto no permite seleccionar profesionales pertenecientes a otro " + strEstructuraNodo, 500, 2500);
            return;
        }

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=T";
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtCR").value = aOpciones[1];
                    mostrarRelacionTecnicos("C", aOpciones[0], "", "");
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}
function obtenerCR2() {
    try {
        var aOpciones;
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=T";
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtCR2").value = aOpciones[1];
                    mostrarRelacionTecnicos2("C", aOpciones[0], "", "");
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}
function obtenerGF() {
    try {
        var aOpciones;
        if ($I("hdnAcceso").value == "R") return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnCRActual").value;
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtGF").value = aOpciones[1];
                    mostrarRelacionTecnicos("G", aOpciones[0], "", "");
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function obtenerGF2() {
    try {
        var aOpciones;
        if ($I("hdnAcceso").value == "R") return;
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnCRActual").value;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtGF2").value = aOpciones[1];
                    mostrarRelacionTecnicos2("G", aOpciones[0], "", "");
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function mostrarRelacionTecnicos(sOpcion, sValor1, sValor2, sValor3) {
    var sCodUne, sNumPE;
    try {
        if (sOpcion == "C") sCodUne = sValor1;
        else sCodUne = $I("hdnCRActual").value;
        sNumPE = dfn($I("hdnT305IdProy").value);

        if (sNumPE == "") {
            mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
            return;
        }
        if (sOpcion == "N") {
            sValor1 = Utilidades.escape($I("txtApellido").value);
            sValor2 = Utilidades.escape($I("txtApellido2").value);
            sValor3 = Utilidades.escape($I("txtNombre").value);
            if (sValor1 == "" && sValor2 == "" && sValor3 == "") {
                mmoff("Inf", "Debes indicar algún criterio para la búsqueda por apellidos/nombre", 410);
                return;
            }
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion + "@#@" + sValor1 + "@#@" + sValor2 + "@#@" + sValor3 + "@#@" + sCodUne + "@#@" + sNumPE + "@#@" + $I("txtCualidad").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function mostrarRelacionTecnicos2(sOpcion, sValor1, sValor2, sValor3) {
    var sNumPE;
    try {
        if ($I("hdnIdPE").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
            return;
        }
        if (sOpcion == "N") {
            sValor1 = Utilidades.escape($I("txtApe1").value);
            sValor2 = Utilidades.escape($I("txtApe2").value);
            sValor3 = Utilidades.escape($I("txtNom").value);
            if (sValor1 == "" && sValor2 == "" && sValor3 == "") {
                mmoff("Inf", "Debes indicar algún criterio para la búsqueda por apellidos/nombre", 410);
                return;
            }
        }
        var js_args = "rtpt@#@";
        js_args += sOpcion + "@#@" + sValor1 + "@#@" + sValor2 + "@#@" + sValor3 + "@#@" + $I("hdnCRActual").value
                    + "@#@" + dfn($I("hdnT305IdProy").value) + "@#@" + $I("txtCualidad").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function insertarRecurso(oFila) {
    var iFilaNueva = 0;
    var sNombreNuevo, sNombreAct;
    try {
        var idRecurso = oFila.id;
        //1º buscar si existe en el array de recursos 
        var objRec = buscarRecursoEnArray(idRecurso);
        var bExiste = false;
        if (objRec == null) { //No existe en el array
            var aFila = FilasDe("tblAsignados");
            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].id == idRecurso) {
                    aFila[i].className = "FS";
                    bExiste = true;
                    break;
                }
            }
            if (bExiste) {
                mostrarDatosAsignacion(idRecurso);
                return;
            }
            //Si no tiene tarifa asignada compruebo si la tiene como recurso asociado al proyecto 
            else {
                insertarRecursoEnArray("I", $I("hdnIDPT").value, idRecurso, oFila.innerText, "", "", oFila.getAttribute("idTarifa"), "1", "", "0");
                $I("cboTarifa").value = oFila.getAttribute("idTarifa");
            }
        } else {
            var aFila = FilasDe("tblAsignados");
            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].id == idRecurso) {
                    bExiste = true;
                    break;
                }
            }
        }
        if (bExiste) {
            //alert("El profesional indicado ya se encuentra asignado al proyecto técnico");
            return;
        }
        if (iFila >= 0) modoControles($I("tblAsignados").rows[iFila], false);
        sNombreNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < aFila.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFila[iFilaNueva].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        var oNF = $I("tblAsignados").insertRow(iFilaNueva);
        oNF.style.height = "20px";
        oNF.id = idRecurso;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("ac", "");
        oNF.setAttribute("sw", "1");

        iFila = oNF.rowIndex;
        oNF.className = "FS";
        oNF.style.cursor = "../../../images/imgManoMove.cur";

        if ($I("hdnAcceso").value == "V") oNF.attachEvent("onclick", mm_rtpt);
        else oNF.attachEvent("onclick", mm);

        oNF.attachEvent("onmousedown", DD);

        oNF.onclick = function() { mostrarDatosAsignacion(this.id) };

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1);

        oNC3 = oNF.insertCell(-1);
        oNC3.appendChild(oFila.cells[0].children[0].cloneNode(true));

        var oNOBR = document.createElement("nobr");
        oNOBR.className = "'NBR W310";
        oNOBR.appendChild(document.createTextNode(oFila.innerText));
        oNC4 = oNF.insertCell(-1).appendChild(oNOBR);

        //        oNC4 = oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W310'>"+oFila.innerText+"</nobr>"));
        //        oNC4.innerText = oFila.innerText;

        oNC5 = oNF.insertCell(-1);
        oNC5.align = "right";
        oNC5.ondblclick = function() { mostrarTareasRecurso(idRecurso) };
        oNC5.innerText = "0";

        ms(oNF);

        aGProf(0);

        $I("divAsignados").scrollTop = iFilaNueva * 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
    }
}
function mostrarDeNuevoRecurso(idRecurso) {
    try {
        var objRec = buscarRecursoEnArray(idRecurso);
        objRec.opcionBD = "U";
        aGProf(0);

        oNF = $I("tblAsignados").insertRow(-1);
        oNF.id = idRecurso;
        oNF.setAttribute("bd", "I");

        var iFila = oNF.rowIndex;

        oNF.attachEvent("onclick", mm);
        oNF.onclick = function() { mostrarDatosAsignacion(this.id); };

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1);

        oNC3 = oNF.insertCell(-1);
        oNC3.innerText = objRec.nombre;

        oNC4 = oNF.insertCell(-1);
        oNC4.align = "right";
        oNC4.ondblclick = function() { mostrarTareasRecurso(idRecurso) };
        oNC4.innerText = "0";

        ms(oNF);
        aGProf(0);
        $I("divAsignados").scrollTop = $I("tblAsignados").rows.length * 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar de nuevo al profesional.", e.message);
    }
}
function setEstadoDatosAsignacion(bEstado) {
    try {
        if (bEstado)
            $I('divRecurso').style.display = 'none';
        else
            $I('divRecurso').style.display = 'block';
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el estado de los datos de la asignación.", e.message);
    }
}
function mostrarDatosAsignacion(idRecurso) {
    var sEstado = "", sTipo;
    var bFilaMarcada = false, bMostrar = false;
    var iNumFilasMarcadas = 0;
    try {
        var aFila = FilasDe("tblAsignados");
        for (i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idRecurso) {
                //if (aFila[i].className == "FS"){
                sEstado = aFila[i].getAttribute("bd");
                if (sEstado == "I") bMostrar = true;
                break;
                //}
            }
        }
        var objRec = buscarRecursoEnArray(idRecurso);
        if (objRec == null) {
            borrarDatosAsignacion();
        }
        else {
            $I("txtFIPRes").value = objRec.fip;
            $I("txtFFPRes").value = objRec.ffe;
            $I("txtIndicaciones").value = Utilidades.unescape(objRec.indicaciones);
            $I("cboTarifa").value = objRec.idTarifa;
        }
        if (bMostrar) {
            if (btnCal == "I") {
                $I("txtFIPRes").onclick = function() { mc(this); };
                $I("txtFFPRes").onclick = function() { mc(this); };
            }
            else {
                $I("txtFIPRes").onmousedown = function() { mc1(this); };
                //$I("txtFIPRes").onfocus=function(){focoFecha(this);};
                $I("txtFIPRes").attachEvent("onfocus", focoFecha);
                $I("txtFFPRes").onmousedown = function() { mc1(this); };
                //$I("txtFFPRes").onfocus=function(){focoFecha(this);};
                $I("txtFFPRes").attachEvent("onfocus", focoFecha);
            }
            //$I("cboTarifa").disabled=false;
            //$I("txtIndicaciones").disabled=false;
            setOp($I("imgAdjudicar"), 20);
            setOp($I("imgDesactivar"), 20);
        }
        else {
            if (btnCal == "I") {
                $I("txtFIPRes").onclick = null;
                $I("txtFFPRes").onclick = null;
            }
            else {
                $I("txtFIPRes").onmousedown = "";
                $I("txtFIPRes").onfocus = "";
                $I("txtFFPRes").onmousedown = "";
                $I("txtFFPRes").onfocus = "";
            }
            //$I("cboTarifa").disabled=true;
            //$I("txtIndicaciones").disabled=true;
            setOp($I("imgAdjudicar"), 100);
            setOp($I("imgDesactivar"), 100);
        }
        setEstadoDatosAsignacion(bMostrar);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de la asignación.", e.message);
    }
}
function borrarDatosAsignacion() {
    try {
        $I("txtFIPRes").value = "";
        $I("txtFFPRes").value = "";
        $I("txtIndicaciones").value = "";
        $I("cboTarifa").value = "";
        setEstadoDatosAsignacion(false);
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar al profesional.", e.message);
    }
}

function insertarRecurso2(oFila) {
    //Añade un RTPT a la lista de RTPT´s del proyecto tecnico
    var iFilaNueva = 0;
    var sNombreNuevo, sNombreAct;
    try {
        var idRecurso = oFila.id;
        var bExiste = false;
        var aFila = FilasDe("tblAsignados2");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idRecurso) {
                if (aFila[i].getAttribute("bd") == "D") {//Si el recurso ya estaba y lo habíamos borrado, lo reactivamos
                    aFila[i].setAttribute("bd", "");
                    aFila[i].style.display = "";
                    aFila[i].className = "FS";
                    return;
                }
                else {//Si el recurso ya estaba sacamos mensaje indicativo
                    aFila[i].className = "FS";
                    bExiste = true;
                }
                break;
            }
        }
        if (bExiste) {
            //alert("El técnico indicado ya se encuentra asignado al proyecto técnico como responsable técnico");
            return;
        }
        sNombreNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < aFila.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFila[iFilaNueva].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = $I("tblAsignados2").insertRow(iFilaNueva);
        oNF.style.height = "20px";
        oNF.id = idRecurso;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", "1");

        var iFila = oNF.rowIndex;
        //oNF.className = "FS";
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[1].children[0].cloneNode(true));
        oNF.cells[2].children[0].className = "NBR W310";
        //        oNC3 = oNF.insertCell(-1);
        //        oNC3.innerText = oFila.innerText;

        aG(2);
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el responsable técnico.", e.message);
    }
}
function desasignar() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        var aFilas = FilasDe("tblAsignados");
        if (aFilas.length == 0) return;
        for (var i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className.toUpperCase() == "FS") {
                eliminarRecurso(aFilas[i]);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desasignar a un profesional", e.message);
    }
}
function eliminarRecurso(oFila) {
    try {
        var idRecurso = oFila.id;
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idRecurso) {
                if (aFila[i].getAttribute("bd") == "I") {
                    borrarRecursoDeArray(idRecurso);
                    $I("tblAsignados").deleteRow(oFila.rowIndex);
                }
                else {
                    //No dejo eliminar recursos que ya están asignados
                    if (aFila[i].getAttribute("bd") == "")
                        mmoff("War", "El profesional " + aFila[i].cells[2].innerText + " no puede ser eliminado pues ya tiene asignaciones realizadas", 400);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar al profesional.", e.message);
    }
}
function desasignar2() {
    try {
        if ($I("hdnAcceso").value != "W") return;
        var nFilas = 0
        var aResp = FilasDe("tblAsignados2");
        nFilas = aResp.length;
        if (nFilas == 0) return;
        for (var i = nFilas - 1; i >= 0; i--) {
            if (aResp[i].className == "FS") {
                eliminarRecurso2(aResp[i]);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desasignar a un profesional", e.message);
    }
}
function eliminarRecurso2(oFila) {
    try {
        var idRecurso = oFila.id;
        var aFila = FilasDe("tblAsignados2");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idRecurso) {
                if (aFila[i].getAttribute("bd") == "I") $I("tblAsignados2").deleteRow(oFila.rowIndex);
                else {
                    aFila[i].setAttribute("bd", "D");
                    aFila[i].style.display = "none";
                    aG(2);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar al profesional.", e.message);
    }
}
function mostrarOTC() {
    try {
        if ($I("hdnAcceso").value != "W") return;
        var aOpciones;
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerPST.aspx?sIdCli=" + $I("hdnIdCliente").value + "&nCR=" + $I("hdnCRActual").value;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(900, 500))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I('txtIdPST').value = aOpciones[0];
                    $I('txtCodPST').value = aOpciones[1];
                    $I('txtDesPST').value = aOpciones[2];
                    aG(0);
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al mostrar las órdenes de trabajo codificadas", e.message);
    }
}
function limpiarPST() {
    try {
        if ($I("hdnAcceso").value != "W") return;
        $I('txtIdPST').value = "";
        $I('txtCodPST').value = "";
        $I('txtDesPST').value = "";
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar las órdenes de trabajo codificadas", e.message);
    }
}
function reestablecer() {//Pone a vacío todas las celdas de la columna Acción
    try {
        if ($I("hdnAcceso").value == "R") return;
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            aFila[i].setAttribute("ac", "");
            //aFila[i].cells[1].innerText="";
            if (aFila[i].cells[1].children.length > 0)
                aFila[i].cells[1].removeChild(aFila[i].cells[1].children[0]);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al reestablecer estado de los profesionales asignados", e.message);
    }
}
function asignarCompleto() {//Marca para asignar el recurso a todas las tareas que cuelgan del proyecto técnico
    //En las que ya estuviera pasa a estado activo
    //En las que no estuviera se inserta (logicamente con estado activo)
    try {
        if ($I("hdnAcceso").value == "R") return;

        var sw = 0;
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                if (aFila[i].getAttribute("bd") == "") {
                    aFila[i].setAttribute("ac", "A");
                    //aFila[i].cells[1].innerText="A";
                    if (aFila[i].cells[1].children.length > 0)
                        aFila[i].cells[1].removeChild(aFila[i].cells[1].children[0]);
                    aFila[i].cells[1].appendChild($I("imgAdjudicar").cloneNode(true));
                    aFila[i].cells[1].children[0].title = "";
                    aGProf(0);
                } else sw = 1;
            }
        }
        if (sw == 1) mmoff("Inf", "Esta acción no es aplicable a profesionales recién asignados y no grabados.", 450);
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar el profesional para asociarlo a todas las tareas", e.message);
    }
}
function desAsignarCompleto() {//Marca el recurso como inactivo a todas las tareas que cuelgan del proyecto técnico
    try {
        if ($I("hdnAcceso").value == "R") return;

        var sw = 0;
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                if (aFila[i].getAttribute("bd") == "") {
                    aFila[i].setAttribute("ac", "D");
                    //aFila[i].cells[1].innerText="D";
                    if (aFila[i].cells[1].children.length > 0)
                        aFila[i].cells[1].removeChild(aFila[i].cells[1].children[0]);
                    aFila[i].cells[1].appendChild($I("imgDesactivar").cloneNode(true));
                    aFila[i].cells[1].children[0].title = "";
                    aGProf(0);
                } else sw = 1;
            }
        }
        if (sw == 1) mmoff("Inf", "Esta acción no es aplicable a profesionales recién asignados y no grabados.", 450);
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar el profesional para desactivarlo en todas las tareas", e.message);
    }
}
var sEstadoOrig = "";
function controlEstado(sValor) {
    try {
        if (sValor == "2") {
            //            alert("No se puede poner el proyecto técnico en estado \"Pendiente\" de forma manual.");
            //            $I("cboEstado").value = sEstadoOrig;
        } else sEstadoOrig = $I("cboEstado").value;
    } catch (e) {
        mostrarErrorAplicacion("Error al controlar el valor del estado", e.message);
    }
}
function restringir() {
    try {
        var bRestringir = $I("chkCliente").checked;
        var aFila = FilasDe("tblAECR");
        if (aFila.length == 0) return;
        for (var i = 0; i < aFila.length; i++) {
            if (!bRestringir) aFila[i].style.display = "";
            else {
                if (aFila[i].getAttribute("cliente") == $I("hdnIdCliente").value) {
                    aFila[i].style.display = "";
                    //Comprobar si está y pasar a tabla tblAET
                } else aFila[i].style.display = "none";
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al restringir los atributos estadísticos", e.message);
    }
}

function asociarAE(oFila, bMsg) {
    try {
        //1º Mirar si el AE seleccionado está en la tabla tblAET (visible u oculto)
        var aFila = FilasDe("tblAET");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == oFila.id) { //Si existe
                sw = 1;
                if (aFila[i].getAttribute("bd") == "D") { //Si está pdte de borrar
                    mfa(aFila[i], "U");
                } else {
                    if (bMsg) mmoff("Inf", "El atributo estadístico seleccionado ya está asociado al proyecto técnico.", 400);
                    break;
                }
            }
        }
        if (sw == 0) {
            oNF = $I("tblAET").insertRow(-1);
            oNF.setAttribute("style", "height:16px")
            oNF.id = oFila.id;
            oNF.setAttribute("vae", "");
            oNF.setAttribute("obl", oFila.getAttribute("obl"));
            oNF.setAttribute("bd", "I");

            var iFila = oNF.rowIndex;

            oNF.attachEvent("onclick", mm);
            oNF.attachEvent("onmousedown", DD);

            oNF.onclick = function() { mostrarValoresAE(this); };

            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

            oNC2 = oNF.insertCell(-1);
            if (oNF.getAttribute("obl") == "1")
                oNC2.innerHTML = "<img src='../../../images/imgIconoObl.gif' alt='Obligatorio'>";
            else
                oNC2.innerHTML = "<img src='../../../images/imgSeparador.gif'>";

            oNF.insertCell(-1).innerText = oFila.innerText;

            var oNOBR = document.createElement("nobr");
            oNOBR.className = "NBR W90";

            oNF.insertCell(-1).appendChild(oNOBR);

            aG(3);
            //ms(oNF);
            mostrarValoresAE(oNF);
            //Compruebo si el AE está usado en alguna tarea para avisar al usuario
            var IdAE;
            for (var i = 0; i < aAEsTareas.length; i++) {
                if (aAEsTareas[i] == oFila.id) { //Si existe
                    IdAE = aAEsTareas[i];
                    //mmoff("InfPer", "El atributo estadístico seleccionado ya está asociado a alguna tarea del PT.\nSi lo asocias, perderás los valores que están en sus tareas.", 400);
                    var msg = "El atributo estadístico seleccionado ya está asociado a una o varias tareas del proyecto técnico<br />Si lo asocias, perderás los valores que están en esas tareas.<br /><br />¿Deseas mostrar las tareas que tienen el atributo estadístico?";
                        jqConfirm("", msg, "", "", "war", 500).then(function (answer) {
                            if (answer)
                                mostrarTareasConAE(IdAE);
                        });
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al asociar el atributo estadístico al proyecto técnico", e.message);
    }
}
function mostrarTareasConAE(idAE) {
    try {
        var js_args = "mostrarTareasConAE@#@" + $I("hdnIDPT").value + "@#@" + idAE;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las tareas con un AE", e.message);
    }
}

var nFilaAESel = -1;
//function mostrarValoresAE(oFila) {
//    try {
        
//        nFilaAESel = oFila.rowIndex;
//        //1º Borrar los valores que hubiera.
//        var aFila = FilasDe("tblAEVD");
//        for (var i = aFila.length - 1; i >= 0; i--) $I("tblAEVD").deleteRow(i);
//        //2º Insertar los valores del AE asociado.
//        aFila = FilasDe("tblAET");
//        var nFilaSel;
//        var sw = 0;
//        for (var i = 0; i < aFila.length; i++) {
//            if (aFila[i].className == "FS") {
//                nFilaSel = i;
//                sw = 1;
//                break;
//            }
//        }
//        if (sw == 1) {
//            var idAE = aFila[i].id;
//            for (var i = 0; i < aVAE_js.length; i++) {
//                if (idAE == aVAE_js[i][0]) {
//                    oNF = $I("tblAEVD").insertRow(-1);
//                    oNF.setAttribute("style", "height:16px")
//                    oNF.id = aVAE_js[i][1];
//                    oNF.attachEvent("onclick", mm);
//                    oNF.ondblclick = function() { asignarValorAE(this); };

//                    oNF.insertCell(-1).innerText = aVAE_js[i][2];
//                }
//            }
//        }
//        ms(oFila);
//    } catch (e) {
//        mostrarErrorAplicacion("Error al mostrar los valores del atributo estadístico.", e.message);
//    }
//}
function mostrarValoresAE(oFila) {
    try {
        nFilaAESel = oFila.rowIndex;
        //1º Borrar los valores que hubiera.
        var aFila = FilasDe("tblAEVD");
        for (var i = aFila.length - 1; i >= 0; i--) $I("tblAEVD").deleteRow(i);
        //2º cargar la lista de valores del atributo seleccionado
        var idAE = oFila.id;
        for (var i = 0; i < aVAE_js.length; i++) {
            if (idAE == aVAE_js[i][0]) {
                oNF = $I("tblAEVD").insertRow(-1);
                oNF.setAttribute("style", "height:16px")
                oNF.id = aVAE_js[i][1];
                oNF.attachEvent("onclick", mm);
                oNF.ondblclick = function () { asignarValorAE(this); };
                oNF.insertCell(-1).innerText = aVAE_js[i][2];
            }
        }
        ms(oFila);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los valores del atributo estadístico.", e.message);
    }
}

function asignarValorAE(oFila) {
    try {
        if (nFilaAESel == -1) return;
        var oFilaAET = $I("tblAET").rows[nFilaAESel];
        oFilaAET.setAttribute("vae", oFila.id);
        //oFilaAET.cells[3].innerText = oFila.innerText;
        oFilaAET.cells[3].children[0].innerHTML = oFila.cells[0].innerText;

        if (oFilaAET.getAttribute("bd") != "I") mfa(oFilaAET, "U");
        $I("hdnRecargar").value = "T";
        //Compruebo si el valor del combo de estado es compatible con la situación de los AE obligatorios
        verificarEstado();
        aG(3);
        aGAvanza(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar valor a un atributo estadístico", e.message);
    }
}

function comprobarAEObligatorios() {
    try {
        //        var aFila = FilasDe("tblAECR");
        //        for (var i=0;i<aFila.length;i++){
        //            if (aFila[i].getAttribute("obl") == "1") asociarAE(aFila[i], false);
        //        }
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los atributos estadísticos obligatorios", e.message);
    }
}
function cargarComboEstado(sEstado) {
    //Carga del combo de estado en función del estado de la tarea
    try {
        $I("cboEstado").length = 0;
        //        if (sEstado=="2"){
        //            var op1=new Option("Pendiente",2);
        //            $I("cboEstado").options[0]= op1;
        //        }
        //        else{
        var op2 = new Option("Activo", 1);
        $I("cboEstado").options[0] = op2;
        var op1 = new Option("Inactivo", 0);
        $I("cboEstado").options[1] = op1;
        //        }
        switch (sEstado) {
            case "0": $I("cboEstado").value = 0; break;
            case "1": $I("cboEstado").value = 1; break;
            case "2": $I("cboEstado").value = 1; break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el estado", e.message);
    }
}

function bAE_Obligatorios() {
    //Comprueba que todos los atributos estadísticos obligatorios tengan valor asignado
    var bRes = true;
    try {
        var aFila = FilasDe("tblAET");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("obl") == "1") {
                if (aFila[i].cells[3].innerText == "") {
                    bRes = false;
                    break;
                }
            }
        }
        return bRes;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los valores de los AE obligatorios", e.message);
    }
}

function verificarEstado() {
    //Se le llama al quitar/poner valores en los atributos estadísticos
    //Si hay algún AE obligatario sin valor pone el estado en Pendiente y recarga el combo
    //Sino pone el estado en Activa y recarga el combo
    try {
        if (bAE_Obligatorios()) {
            //alert("NO esta pendiente");
            cargarComboEstado("1"); //pongo Activa
        }
        else {
            cargarComboEstado("2"); //pongo Pendiente

        }
    } catch (e) {
        mostrarErrorAplicacion("Error al verificar los valores de los AE obligatorios", e.message);
    }
}

function modificarVigencia() {
    try {
        if (bLectura) return;
        aG(0);
        bFechaModificada = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al verificar los valores de los AE obligatorios", e.message);
    }
}
function mostrarTareasRecurso(idRecurso) {
    try {
        var strEnlace = strServer + "Capa_Presentacion/PSP/mostrarTareasRec.aspx?";
        strEnlace += "sTipo=P";
        strEnlace += "&nItem=" + $I("hdnIDPT").value;
        strEnlace += "&nRecurso=" + idRecurso;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 460));
        window.focus();
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar las tareas asignadas al profesional", e.message);
    }
}
function obtenerProyectos(){
    try{
        var idPT, sAux;
        if ($I("hdnEstr").value == "S") return;
        idPT = $I("txtIdPT").value;
        //En PT grabadas no permitimos cambiar de Proyecto Económico
        if (idPT != "" && idPT != "0" && idPT != "-1") return;

        if (bCambios) {
            if ($I("hdnIdPE").value != "") {
                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                    if (answer) {
                        bGetPE = true;
                        bEnviar = LLamarGrabar();
                    }
                    else {
                        bCambios = false;
                        LLamarObtenerProyectos();
                    }
                });
            } else LLamarObtenerProyectos();
        } else LLamarObtenerProyectos();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos-1", e.message);
    }
}
function LLamarObtenerProyectos(){
    try{   
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sSoloAbiertos=1&sNoVerPIG=1"; //Solo proyectos abiertos
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    if (aDatos[1] == "1") {
                        bLectura = true;
                    } else {
                        bLectura = false;
                    }
                    if (es_administrador == "SA" || es_administrador == "A") {
                        bRTPT = false;
                    }
                    else {
                        if (aDatos[2] == "1") {
                            bRTPT = true;
                        } else {
                            bRTPT = false;
                        }
                    }
                    recuperarDatosPSN();
                }
            });
        
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function recuperarDatosPSN() {
    try {
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        js_args += $I("hdnT305IdProy").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
function obtenerPTs(){
    try{
        //Si estoy en modo Crear no dejo acceso a la selección de PT
        if (gsModo == "C") return;

        //if (bCambios && intSession > 1){
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetPT = true;
                    bEnviar = LLamarGrabar();
                }
                else {
                    bCambios = false;
                    LLamarObtenerPTs();
                }
            });
        } else LLamarObtenerPTs();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos técnicos-1", e.message);
    }
}
function LLamarObtenerPTs(){
    try{   
        var aOpciones, idPE, sPE, idPT, strEnlace, sAncho, sAlto;
        idPE = $I("hdnIdPE").value;
        sPE = $I("txtPE").value;
        idPT = $I("txtIdPT").value.replace('.', '');
        var sTamano = "";

        if (idPE == "" || idPE == "0") {
            strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT2.aspx";
            sTamano = sSize(850, 640);
            //sAncho="850px";
            //sAlto="650px";
        }
        else {
            strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPSN=" + codpar($I("hdnT305IdProy").value) + "&nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE);
            sTamano = sSize(500, 560);
            //sAncho="500px";
            //sAlto="580px";
        }
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    idPT = aOpciones[0];
                    $I("txtIdPT").value = idPT.ToString("N",7,0);
                    $I("txtDesPT").value = aOpciones[1];
                    cargarPT();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos técnicos-2", e.message);
    }
}
function limpiarPE2() {
    if (gsModo == "") return;
    if ($I("txtPE").value != "") {
        var sIdPE = $I("hdnIdPE").value;
        llamarLimpiar();
        $I("hdnIdPE").value = sIdPE;
    }
}
function limpiarPT2() {
    if (gsModo == "") return;
    if ($I("txtDesPT").value != "") {
        var sIdPT = $I("txtIdPT").value;
        llamarLimpiar();
        $I("txtIdPT").value = sIdPT;
    }
}
function limpiar() {
    try {
        if (getOp($I("btnNuevo")) == 30) return;
        if ($I("hdnAcceso").value != "R") {
            if (bCambios && intSession > 0) {
                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                    if (answer) {
                        bLimpiar = true;
                        bEnviar = LLamarGrabar();
                    }
                    else {
                        bCambios = false;
                        llamarLimpiar();
                    }
                });
            } else llamarLimpiar();
        }
        else llamarLimpiar();
    } catch (e) {
        mostrarErrorAplicacion("Error al poner nuevo proyecto técnico-1.", e.message);
    }
}
function llamarLimpiar() {
    var sCodCR, sCodPE, sDesPE, sPermiso, sAcceso, sT305IdProy;
    try {
        sCodCR = $I("hdnCRActual").value;
        sAcceso = $I("hdnAcceso").value;
        if (gsModo == "B" || gsModo == "C") {
            sT305IdProy = "";
            sCodPE = "";
            sDesPE = "";
        }
        else {
            sT305IdProy = $I("hdnT305IdProy").value;
            sCodPE = $I("hdnIdPE").value;
            sDesPE = $I("txtPE").value;
        }
        sPermiso = $I("Permiso").value;

        clearAll(document.forms[0]);

        $I("hdnT305IdProy").value = sT305IdProy;
        $I("hdnIdPE").value = sCodPE;
        $I("txtPE").value = sDesPE;
        $I("hdnCRActual").value = sCodCR;
        $I("nCR").value = sCodCR;
        $I("Permiso").value = sPermiso;
        $I("hdnAcceso").value = sAcceso;

        //Pongo valores por defecto
        var sEstado = "1"; //Activa
        $I("cboEstado").value = sEstado;
        //$I("hdnEstado").value=sEstado;
        var anio, mes, dia;
        var Mi_Fecha = new Date();
        anio = Mi_Fecha.getFullYear();
        mes = Mi_Fecha.getMonth() + 1;
        if (mes.toString().length == 1) mes = "0" + mes;
        dia = Mi_Fecha.getDate();
        if (dia.toString().length == 1) dia = "0" + dia;
        var sFecha = dia + "/" + mes + "/" + anio;
        $I("txtValIni").value = sFecha;

        //Borro el contenido de las tablas 
        //Profesioneales
        BorrarFilasDe("tblRelacion");
        BorrarFilasDe("tblAsignados");
        //RTPT´s
        BorrarFilasDe("tblRelacion2");
        BorrarFilasDe("tblAsignados2");
        //Atributos Estadísticos
        //BorrarFilasDe("tblAECR");
        BorrarFilasDe("tblAET");
        //        var aFilas = $I("tblAET").getElementsByTagName("tr");
        //        for (var i=aFilas.length-1;i>=0;i--){
        //            if (aFilas[i].getAttribute("obl")=="0")
        //                $I("tblAET").deleteRow(i);
        //        }
        BorrarFilasDe("tblAEVD");

        if ($I("tblTareas") != null)
            BorrarFilasDe("tblTareas");
        if ($I("tblDocumentos") != null)
            BorrarFilasDe("tblDocumentos"); //documentos 
        if ($I("tblDocumentos2") != null)
            BorrarFilasDe("tblDocumentos2"); //documentos dependientes
        if ($I("tblAsignados3") != null)
            BorrarFilasDe("tblAsignados3"); //Pool de profesionales
        if ($I("tblPoolGF") != null)
            BorrarFilasDe("tblPoolGF"); //Pool de Grupos Funcionales

        $I("nIdPT").value = "";
        $I("Permiso").value = "";
        $I("nCR").value = $I("hdnCRActual").value;
        reIniciarPestanas();
        bCambios = false;
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        setOp($I("btnEliminar"), 30);

        setCamposEstadoBusq();
        tsPestanas.setSelectedIndex(0);
        //if (gsModo == "C") $I("txtDesPT").focus();

        $I("btnBitacora").src = "../../../images/imgBTPTN.gif";
        $I("btnBitacora").style.cursor = "default";
        $I("btnBitacora").onclick = mostrarBitacora;
        $I("btnBitacora").title = "Sin acceso a la bitácora de proyecto técnico.";


        //Pongo el pool de RTPT´s del PE o (si es vacío) el usuario actual como RTPT por defecto 
        var js_args = "ponerRTPT@#@" + $I("hdnT305IdProy").value + "@#@" + $I("hdnCRActual").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos

    } catch (e) {
        mostrarErrorAplicacion("Error al poner nueva proyecto técnico-2.", e.message);
    }
}
function nuevoForm() {
    bCambios = false;
    bHayCambios = false;
    reIniciarPestanas();
    //document.forms[0].submit();
    bSaliendo = true;
    var theform = document.forms[0];
    theform.submit();    
    
}
function cargarPT() {
    try {
        $I("nIdPT").value = $I("txtIdPT").value.replace('.', '');

        //if (gsModo == "B")
        //    $I("Permiso").value = $I("hdnModoAcceso").value;
        //else
            $I("Permiso").value = "";

        $I("nCR").value = $I("hdnCRActual").value;
        setTimeout("nuevoForm()", 1000);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el proyecto técnico.", e.message);
    }
}
function clearAll(form) {
    var controls = form.elements;

    for (var i = 0, n = controls.length; i < n; i++) {
        var current = controls[i];
        //alert("Tipo="+current.type+ " Nombre="+current.name);
        switch (current.type) {
            case 'text':
            case 'textarea':
            case 'select-one':
                current.value = "";
                break;
            case 'checkbox':
                current.checked = false;
                break;
            case 'hidden':
                if (current.name.substring(0, 3) == "hdn")
                    current.value = "";
                break;
        }
    }
}
function borrar() {
    try {
        if (getOp($I("btnEliminar")) != 100) return;
        if ($I("txtIdPT").value == "") return;
        //Si le hemos llamado desde la estructura hay que borrar esa línea
        jqConfirm("", "Si pulsas 'Aceptar' se eliminará el proyecto técnico actual y todos lo elementos que cuelguen de él. <br><br>¿Deseas hacerlo?", "", "", "war", 400).then(function (answer) {
            if (answer)
            {
                $I("hdnRecargar").value = "T";
                var js_args = "borrar@#@";
                js_args += $I("txtIdPT").value.replace('.', '');
                mostrarProcesando();
                RealizarCallBack(js_args, "");  //con argumentos
            }
            else return;
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el proyecto técnico.", e.message);
    }
}
function nuevoDoc1() {
    var sIdPT = $I("hdnIDPT").value;

    if ((sIdPT == "") || (sIdPT == "0")) {
        mmoff("Inf", "El proyecto técnico debe estar grabado para poder asociarle documentación", 380);
    }
    else {
        nuevoDoc('PT', sIdPT);
    }
}
function eliminarDoc1() {
    if ($I("hdnModoAcceso").value == "R") return;
    var sIdPT = $I("hdnIDPT").value;

    if ((sIdPT == "") || (sIdPT == "0")) {
        mmoff("Inf", "El proyecto técnico debe estar grabado para poder asociarle documentación", 380);
    }
    else {
        eliminarDoc();
    }
}
function teclaDenominacion(e) {

    if (gsModo == "B") {
        var sIdPT = $I('hdnIDPT').value;
        if ((sIdPT == "") || (sIdPT == "0")) {
            if (!e) e = event;
            var oElement = e.srcElement ? e.srcElement : e.target;
            oElement.keyCode = 0;
        }
    }
}
//***************************************************************************************
//Funciones asociadas al Pool
//***************************************************************************************
function mostrarRelacionTecnicos3(sOpcion, sValor1, sValor2, sValor3) {
    var sNumPE;
    try {
        if ($I("hdnIdPE").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
            return;
        }
        if (sOpcion == "N") {
            sValor1 = Utilidades.escape($I("txtApe1Pool").value);
            sValor2 = Utilidades.escape($I("txtApe2Pool").value);
            sValor3 = Utilidades.escape($I("txtNomPool").value);
            if (sValor1 == "" && sValor2 == "" && sValor3 == "") {
                mmoff("Inf", "Debes indicar algún criterio para la búsqueda por apellidos/nombre", 410);
                return;
            }
        }
        var js_args = "tecnicosPool@#@";
        js_args += sOpcion + "@#@" + sValor1 + "@#@" + sValor2 + "@#@" + sValor3 + "@#@" + $I("txtCualidad").value + "@#@" + $I("hdnCRActual").value + "@#@" + dfn($I("hdnT305IdProy").value);

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}

function insertarRecurso3(oFila) {
    //Añade un profesional a la lista de profesionales del Pool del proyecto tecnico
    var iFilaNueva = 0;
    var sNombreNuevo, sNombreAct;
    try {
        var idRecurso = oFila.id;
        var bExiste = false;
        var aFila = FilasDe("tblAsignados3");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idRecurso) {
                if (aFila[i].getAttribute("bd") == "D") {//Si el recurso ya estaba y lo habíamos borrado, lo reactivamos
                    aFila[i].setAttribute("bd", "");
                    aFila[i].style.display = "";
                    aFila[i].className = "FS";
                    return;
                }
                else {//Si el recurso ya estaba sacamos mensaje indicativo
                    aFila[i].className = "FS";
                    bExiste = true;
                }
                break;
            }
        }
        if (bExiste) {
            //alert("El técnico indicado ya se encuentra asignado al proyecto técnico como integrante del pool");
            return;
        }
        sNombreNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < aFila.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFila[iFilaNueva].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = $I("tblAsignados3").insertRow(iFilaNueva);
        oNF.id = idRecurso;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", "1");
        oNF.style.height = "20px";

        var iFila = oNF.rowIndex;

        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true));

        oNC3 = oNF.insertCell(-1);
        oNC3.innerText = oFila.innerText;

        aGProf(1);
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el profesional en el pool.", e.message);
    }
}
function desasignar3() {
    try {
        if ($I("hdnAcceso").value != "W") return;
        var nFilas = 0
        var aResp = FilasDe("tblAsignados3");
        nFilas = aResp.length;
        if (nFilas == 0) return;
        for (var i = nFilas - 1; i >= 0; i--) {
            if (aResp[i].className == "FS") {
                eliminarRecurso3(aResp[i]);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desasignar a un profesional del pool", e.message);
    }
}
function eliminarRecurso3(oFila) {
    try {
        var idRecurso = oFila.id;
        var aFila = FilasDe("tblAsignados3");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idRecurso) {
                if (aFila[i].getAttribute("bd") == "I") $I("tblAsignados3").deleteRow(oFila.rowIndex);
                else {
                    //aFila[i].bd="D";
                    //aFila[i].style.display="none";
                    mfa(aFila[i], "D");
                    aGProf(1);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar al profesional del pool.", e.message);
    }
}
function obtenerGF3() {
    try {
        var aOpciones, aGF;
        var sw = 0;
        if ($I("hdnAcceso").value == "R") return;
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF_Mult.aspx?nCR=" + $I("hdnCRActual").value;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("##");
                    for (i = 0; i < aOpciones.length; i++) {
                        if (aOpciones[i] != "") {
                            aGF = aOpciones[i].split("@#@");
                            if (aGF[0] != "") {
                                insertarGF(aGF[0], aGF[1]);
                                sw = 1;
                            }
                        }
                    }
                    if (sw == 1) aGProf(1);
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function insertarGF(sIdGF, sDesGF) {
    var iFilaNueva = 0;
    try {
        var bExiste = false;
        if (!bExiste) {// buscar si existe en el array de GF de la fase
            var aFila = FilasDe("tblPoolGF");
            for (var i = 0; i < aFila.length; i++) {
                if ((aFila[i].id == sIdGF) && (aFila[i].getAttribute("bd") != "D")) {
                    bExiste = true;
                    break;
                }
            }
        }
        if (bExiste) {
            //alert("El GF indicado ya se encuentra asignado al pool del proyecto técnico");
            return;
        }
        for (var iFilaNueva = 0; iFilaNueva < aFila.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            if (aFila[iFilaNueva].cells[1].innerText > sDesGF) break;
        }
        oNF = $I("tblPoolGF").insertRow(iFilaNueva);
        oNF.id = sIdGF;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("h", "N");
        oNF.style.height = "16px";

        var iFila = oNF.rowIndex;
        oNF.className = "FS";

        oNF.attachEvent("onclick", mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);
        oNC2.innerText = sDesGF;

        ms(oNF);
        $I("divPoolGF").scrollTop = $I("tblPoolGF").rows.length * 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el Grupo Funcional.", e.message);
    }
}
function desasignarGF() {
    try {
        var sw = 0;
        if ($I("hdnAcceso").value == "R") return;
        var aFilas = FilasDe("tblPoolGF");
        if (aFilas.length == 0) return;
        for (var i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className.toUpperCase() == "FS") {
                if (aFilas[i].getAttribute("bd") == "I") $I("tblPoolGF").deleteRow(i);
                else {
                    mfa(aFilas[i], "D");
                }
                sw = 1;
            }
        }
        if (sw == 1) {
            aGProf(1);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desasignar un Grupo Funcional del pool.", e.message);
    }
}
function mostrarBitacora() {
    try {
        var sCodproy = $I("txtIdPT").value;
        if (sCodproy == "") return;

        //Si la estructura no está grabada solicito grabacion
        if (bCambios) {
            ocultarProcesando();
            jqConfirm("", "Datos modificados.<br />Para acceder a Bitácora es preciso grabarlos.<br /><br />¿Deseas hacerlo?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bBitacora = true;
                    LLamarGrabar();
                }
            });
        } else LlamarMostrarBitacora();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar Bitácora-1", e.message);
    }
}
function LlamarMostrarBitacora() {
    try {
        var sParam = "?e=" + codpar($I("txtEstado").value);
        sParam += "&psn=" + codpar($I("hdnT305IdProy").value);
        sParam += "&cr=" + codpar($I("hdnCRActual").value);
        sParam += "&ncr=" + codpar($I("hdnDesCRActual").value);
        sParam += "&p=" + codpar($I("hdnIdPE").value);
        sParam += "&np=" + codpar($I("txtPE").value);
        //location.href="../../Proyecto/Bitacora/Default.aspx"+ sParam; 
        sParam += "&pt=" + codpar($I("txtIdPT").value.replace('.', ''));
        sParam += "&npt=" + codpar($I("txtDesPT").value);
        if ($I("hdnAcceso").value == "V" || $I("hdnAcceso").value == "W") {
            sParam += "&b=" + codpar("E");
        }
        else {
            sParam += "&b=" + codpar("L"); ;
        }

        var sPantalla = strServer + "Capa_Presentacion/PSP/ProyTec/Bitacora/Default.aspx" + sParam; ;

        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(1014, 675));
        window.focus();
        ocultarProcesando();

    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar Bitácora-2", e.message);
    }
}

//////////////////////////////////////////////////////////////////////////////////
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
var aPestGral = new Array();
var aPestProf = new Array();
var aPestAvanza = new Array();

var bValidacionPestanas = true;
//validar pestana pulsada

function vpp(e, eventInfo) {
    try {
        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        if (!aPestGral[nPestanaPulsada]) {
            //mmoff("La pantalla se está cargando.\nPor favor, espere unos segundos y vuelva a intentarlo.", 500);
            eventInfo.cancel();
            return false;
        }
        if (sSistemaPestanas == "tsPestanas" || sSistemaPestanas == "ctl00_CPHC_tsPestanas") {
            if (nPestanaPulsada > 0) {
                //Evaluar lo que proceda, y si no se cumple la validación
                if ($I("txtIdPT").value == "0" || $I("txtIdPT").value == "") {
                    mmoff("Inf", "El acceso a la pestaña seleccionada, requiere seleccionar un proyecto técnico.", 500);
                    eventInfo.cancel();
                    return false;
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la pestaña pulsada.", e.message);
    }
}
function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function insertarPestanaEnArrayProf(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestProf[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña de profesionales en el array.", e.message);
    }
}
function insertarPestanaEnArrayAvanza(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestAvanza[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña de profesionales en el array.", e.message);
    }
}

function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);

        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayProf(i, false, false);

        for (var i = 0; i < tsPestanasAvanza.bbd.bba.getItemCount() ; i++)
            insertarPestanaEnArrayAvanza(i, false, false);

        //Para seleccionar una subpestaña, primero hay que seleccionar su pestaña padre.
        //        tsPestanas.setSelectedIndex(1);
        //        tsPestanasProf.setSelectedIndex(0);

        //Posicionarnos en la general
        tsPestanas.setSelectedIndex(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;

        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++)
            aPestProf[i].bModif = false;

        for (var i = 0; i < tsPestanasAvanza.bbd.bba.getItemCount() ; i++)
            aPestAvanza[i].bModif = false;

    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las pestañas de primer nivel.", e.message);
    }
}

function CrearPestanasProf() {
    try {
        tsPestanasProf = EO1021.r._o_tsPestanasProf;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña de clientes.", e.message);
    }
}
function CrearPestanasAvanza() {
    try {
        tsPestanasAvanza = EO1021.r._o_tsPestanasAvanza;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña de avanzada.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }

        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        switch (sSistemaPestanas) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
            case "ctl00_CPHC_tsPestanasProf":
            case "tsPestanasProf":
                if (!aPestProf[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosProf(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
            case "ctl00_CPHC_tsPestanasAvanza":
            case "tsPestanasAvanza":
                if (!aPestAvanza[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosAvanza(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function getDatos(iPestana) {
    try {
        //sI ES LA PESTAÑA DE cONTROL no hacemos nada porque el campo ya se carga al traer los datos del PT
        if (iPestana == 5) return;
        //Si estamos creando PT ya tenemos cargado el catálogo de RTPTs
        if ($I("txtIdPT").value == "" && iPestana == 2) return;
        if ($I("txtIdPT").value == "" && iPestana == 4) {
            //No tiene sentido obtener tareas si el PT es nuevo
        }
        else {
            mostrarProcesando();
            var js_args = "getDatosPestana@#@" + iPestana + "@#@" + $I("txtIdPT").value.replace('.', '') + "@#@@#@" + $I("hdnCRActual").value;
            if (iPestana == 4) {
                RealizarCallBack(js_args, "");
            }
            else {
                if (iPestana == 6) {//Pestaña de documentos
                    //modo de acceso a la pantalla y estado del proyecto
                    gsDocModAcc = $I("hdnModoAcceso").value;
                    gsDocEstPry = $I("txtEstado").value;
                    setEstadoBotonesDoc(gsDocModAcc, gsDocEstPry);
                    js_args += "@#@" + gsDocModAcc + "@#@" + gsDocEstPry;
                }
            }
            RealizarCallBack(js_args, "");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}
function getDatosProf(iPestana) {
    try {
        mostrarProcesando();
        RealizarCallBack("getDatosPestanaProf@#@" + iPestana + "@#@" + $I("txtIdPT").value.replace('.', '') + "@#@" + $I("hdnCRActual").value, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales " + iPestana, e.message);
    }
}
function getDatosAvanza(iPestana) {
    try {

        mostrarProcesando();

        RealizarCallBack("getDatosPestanaAvan@#@" + iPestana + "@#@" + $I("txtIdPT").value.replace('.', ''), ""); // + "@#@" +
        //$I("hdnIDPT").value + "@#@" + $I("hdnIDFase").value + "@#@" + $I("hdnIDAct").value + "@#@" + $I("hdnCRActual").value, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de datos avanzados " + iPestana, e.message);
    }
}

function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0":
            case "5":
                //no hago nada
                break;
            case "1": //Profesionales
                RespuestaCallBackPestanaProf("0", strResultado, "");
                actualizarLupas("tblTitRecAsig", "tblAsignados");
                break;
            case "2": //Responsables
                $I("divRTPTAsignados").children[0].innerHTML = aResul[0];
                $I("divRTPTAsignados").scrollTop = 0;
                scrollTablaRTPTAsig();
                break;
            //case "3": //Avanzado
            //    $I("divAECR").innerHTML = aResul[0];
            //    $I("divAECR").scrollTop = 0;
            //    $I("divAET").innerHTML = aResul[1];
            //    $I("divAET").scrollTop = 0;
            //    eval(aResul[2]);
            //    break;
            case "3": //Avanzado
                RespuestaCallBackPestanaAvan("0", strResultado);
                break;
            case "4": //Tareas
                $I("divTareas").children[0].innerHTML = aResul[0];
                $I("divTareas").scrollTop = 0;
                aFilaT = FilasDe("tblTareas");
                break;
            case "6": //Documentación
                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
                $I("divCatalogoDoc").scrollTop = 0;
                $I("divCatalogoDoc2").children[0].innerHTML = aResul[1];
                $I("divCatalogoDoc2").scrollTop = 0;
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function RespuestaCallBackPestanaProf(iPestana, strResultado, sNumEmpleados) {
    try {
        var aResul = strResultado.split("///");
        aPestProf[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0": //Profesionales
                $I("divAsignados").children[0].innerHTML = aResul[0];
                $I("divAsignados").scrollTop = 0;
                scrollTablaProfAsig();
                break;
            case "1": //Pool´s
                $I("divPoolGF").children[0].innerHTML = aResul[1];
                $I("divPoolGF").scrollTop = 0;
                $I("divPoolProf").children[0].innerHTML = aResul[0];
                $I("divPoolProf").scrollTop = 0;
                $I("lblNumEmp").innerHTML = sNumEmpleados;
                scrollTablaPoolAsig();
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales", e.message);
    }
}
function RespuestaCallBackPestanaAvan(iPestana, strResultado, strResultadoCampos) {
    try {
        var aResul = strResultado.split("///");
        aPestAvanza[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0": //Criterios estadísticos
                $I("divAECR").innerHTML = aResul[0];
                $I("divAECR").scrollTop = 0;
                $I("divAET").innerHTML = aResul[1];
                $I("divAET").scrollTop = 0;

                comprobarAEObligatorios();
                //eliminarAERepetidos();
                restringir();

                eval(aResul[2]);

                break;
            case "1": // Campos de PT
                $I("divCatalogoValores").children[0].innerHTML = aResul[0];
                $I("divCatalogoValores").children[0].scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = strResultadoCampos;
                $I("divCatalogo").scrollTop = 0;
                break;            
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña avanzado", e.message);
    }
}

function aG(iPestana) {//Sustituye a activarGrabar
    try {
        //if ($I("txtDesPT").value=="")return;
        if ($I("hdnAcceso").value != "R") {
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
            aPestGral[iPestana].bModif = true;

            bCambios = true;
            bHayCambios = true;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
    }
}
function aGProf(iSubPestana) {
    try {
        aPestProf[iSubPestana].bModif = true; //Marco como modificada la subpestaña
        aG(1);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
function aGAvanza(iSubPestana) {
    try {
        aPestAvanza[iSubPestana].bModif = true; //Marco como modificada la subpestaña
        aG(3);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}

//Funciones para contraer/expandir los elementos de la pestaña de tareas
function mostrar(oImg) {
    //Contrae o expande un elemento
    try {
        var opcion, nMargen, nMargenAct, sEstado, sTipo, sTipoAct;

        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        var idFila = oFila.id;
        if (oImg.src.indexOf("plus.gif") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar
        sTipoAct = oFila.getAttribute("tipo");

        if (nDesplegado == "0") {
            switch (nNivel) {
                //                case "1": //PT  
                //                    var js_args = "getPT@#@"+ $I("txtIdPT").value;   
                //                    break;  
                case "1": //Fase o Actividad
                    if (sTipoAct == "F") var js_args = "getFase@#@";
                    else var js_args = "getActiv@#@";
                    js_args += idFila + "@#@";
                    //                    if ($I("chkEstr").checked) js_args+="S";
                    //                    else js_args+="N";
                    break;
            }
            iFila = nIndexFila;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
            return;
        }
        var iF = oImg.parentNode.parentNode.rowIndex;
        //        if (oImg.src.indexOf("plus.gif") == -1){
        //            if (!flRamaContraible(iF, false)){
        //                ocultarProcesando();
        //                msjIncorrecto();
        //                return;
        //            }
        //        }
        var tblTareas = $I("tblTareas");

        //Recojo el margen actual y lo transformo a numerico
        var sMargen = String(tblTareas.rows[iF].cells[0].children[0].style.marginLeft);

        //Si pulso sobre la imagen en un elemento que no sea F o A no hago nada
        if ((sTipoAct != "F") && (sTipoAct != "A")) {
            ocultarProcesando();
            aFila = null;
            return;
        }

        nMargenAct = getMargenAct(sMargen);
        var tblTareas = $I("tblTareas");
        for (var i = iF + 1; i < tblTareas.rows.length; i++) {
            sTipo = tblTareas.rows[i].getAttribute("tipo");
            //Recojo el estado actual para no tratar las filas marcadas para borrado
            sEstado = tblTareas.rows[iF].getAttribute("bd");
            if (sEstado != "D") {
                sMargen = String(tblTareas.rows[i].cells[0].children[0].style.marginLeft);
                nMargen = getMargenAct(sMargen);
                if (nMargenAct >= nMargen) break;
                else {
                    if (opcion == "O") {//Al ocultar contraemos todos los hijos independientemente de su nivel
                        if ((sTipo == "F") || (sTipo == "A")) {
                            if (tblTareas.rows[i].cells[0].children[0].tagName == "IMG")
                                tblTareas.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                        }
                        tblTareas.rows[i].style.display = "none";
                    }
                    else {//Al desplegar, para P,F y A solo desplegamos los del siguiente nivel al actual 
                        if ((sTipoAct == "P") || (sTipoAct == "F")) {
                            if (nMargenAct == nMargen - 20) {//Actúo solo sobre el siguiente nivel o sobre los hitos del siguiente nivel.
                                tblTareas.rows[i].style.display = "table-row";
                            }
                        }
                        else {
                            if (sTipoAct == "A") {
                                tblTareas.rows[i].style.display = "table-row";
                            }
                        }
                    }
                }
            }
        }
        if (opcion == "O") {
            oImg.src = "../../../images/plus.gif";
        }
        else oImg.src = "../../../images/minus.gif";

        if (bMostrar) MostrarTodo();
        else ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

var bMostrar = false;
var nIndiceTodo = -1;
function MostrarTodo() {
    try {
        if (aFilaT == null) return;

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        var tblTareas = $I("tblTareas");
        for (var i = nIndiceAux; i < tblTareas.rows.length; i++) {
            if (tblTareas.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1) {
                bMostrar = true;
                nIndiceTodo = i;
                mostrar(tblTareas.rows[i].cells[0].children[0]);
                return;
            }
        }
        bMostrar = false;
        nIndiceTodo = -1;
        aFilaT = FilasDe("tblTareas");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}
function getMargenAct(sMargen) {
    var intPos;
    var sAux;

    try {
        intPos = sMargen.indexOf("p");
        if (intPos <= 0) sAux = 0;
        else sAux = parseInt(sMargen.substring(0, intPos), 10);
        return sAux;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener el margen de la línea", e.message);
    }
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
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

    var obj = document.getElementById("DW");
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
                case "imgPapRtpt":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else {
                            mfa(oRow, "D");
                            aG(2);
                        }
                    }
                    break;
                case "imgPapPoolProf":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else {
                            mfa(oRow, "D");
                            aGProf(1);
                        }
                    }
                    break;
                case "imgPapeleraAE":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("obl") == "1") {
                            if (oRow.cells[1].children[0].src != null && oRow.cells[1].children[0].src.indexOf("imgIconoObl.gif") > -1) {
                                mmoff("Inf", "No se permite eliminar criterios estadísticos obligatorios", 350);
                            }
                        } else {
                            if (oRow.getAttribute("bd") == "I") {
                                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                            }
                            else {
                                mfa(oRow, "D");
                                //aG(3);
                                aGAvanza(0);
                            }
                        }
                        BorrarFilasDe("tblAEVD");
                    }
                    break;

                case "imgPapeleraCampos":
                    if (nOpcionDD == 3) {
                       
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else {
                            mfa(oRow, "D");
                        //aG(3);
                            aGAvanza(1);
                            }                                
                       
                    }
                    break;

                case "imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            borrarRecursoDeArray(oRow.id);
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                            borrarDatosAsignacion();
                            $I("txtFIPRes").onclick = null;
                            $I("txtFFPRes").onclick = null;
                            //$I("cboTarifa").disabled = true;
                            //$I("txtIndicaciones").disabled = true;
                            setOp($I("imgAdjudicar"), 100);
                            setOp($I("imgDesactivar"), 100);
                        }
                        else if (oRow.getAttribute("bd") != "") {
                            mfa(oRow, "D");
                            aGProf(0);
                        }
                    }
                    break;
                case "divRTPTAsignados":
                case "divPoolProf":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("table")[0];
                    var sw = 0;
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

                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));

                        if (oTarget.id == "divRTPTAsignados") oCloneNode.cells[2].children[0].className = "NBR W270";
                        mfa(oCloneNode, "I");
                        if (oTarget.id == "divPoolProf") aGProf(1);
                        else aG(2);
                    }
                    break;

                case "divAsignados":
                    if (FromTable == null || ToTable == null) continue;
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
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

                            insertarRecursoEnArray("I", $I("hdnIDPT").value, oRow.id, oRow.innerText, "", "", oRow.getAttribute("idTarifa"), "1", "", "0");

                            nIndiceInsert++;
                            var oCloneNode = oRow.cloneNode(true);
                            oCloneNode.attachEvent("onclick", mm);
                            oCloneNode.onclick = function() { mostrarDatosAsignacion(this.id) };
                            oCloneNode.className = "";
                            oCloneNode.setAttribute("bd", "I");
                            oCloneNode.setAttribute("ac", "");
                            oCloneNode.style.cursor = "../../../images/imgManoMove.cur";

                            NewRow.swapNode(oCloneNode);

                            NewRow.attachEvent("onclick", mm);
                            NewRow.onclick = function() { mostrarDatosAsignacion(this.id) };
                            NewRow.style.cursor = "../../../images/imgManoMove.cur";

                            oCloneNode.insertCell(0);
                            oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));

                            oCloneNode.insertCell(1);

                            var idRecurso = oRow.id;
                            oNC4 = oCloneNode.insertCell(-1);
                            oNC4.align = "right";
                            oNC4.ondblclick = function() { mostrarTareasRecurso(idRecurso) };
                            oNC4.innerText = "0";

                            oCloneNode.cells[3].children[0].className = "NBR W310";

                            mfa(oCloneNode, "I");
                            //seleccionar(oCloneNode);
                            aGProf(0);
                        }
                    }
                    break;
                case "divAET":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        if (sw == 0) {
                            nIndiceInsert++;
                            asociarAE(oRow, true);
                        }
                    }
                    break;
                case "divCatalogoValores":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        if (sw == 0) {
                            nIndiceInsert++;
                            asociarCampo(oRow, true);
                        }
                    }
                    break;
            }
        }
        if (oTarget != null) {
            switch (oTarget.id) {
                case "divAsignados":
                    //actualizarLupas(event.srcElement.previousSibling.id, event.srcElement.children[0].children[0].id);
                    actualizarLupas("tblTitRecAsig", "tblAsignados");
                    break;
                case "imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            var oElem = getNextElementSibling(oElement.parentNode);
                            actualizarLupas(oElem.getElementsByTagName("table")[0].id, oElem.getElementsByTagName("table")[1].id);
                        }
                    } else if (nOpcionDD == 4) {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas(oElem.getElementsByTagName("table")[0].id, oElem.getElementsByTagName("table")[1].id);
                    }
                    break;

                case "divPoolProf":
                    actualizarLupas("tblTitPoolAsig", "tblAsignados3");
                    break;
                case "imgPapPoolProf":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            var oElem = getNextElementSibling(oElement.parentNode);
                            actualizarLupas("tblTitPoolAsig", "tblAsignados3");
                        }
                    } else if (nOpcionDD == 4) {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas("tblTitPoolAsig", "tblAsignados3");
                    }
                    break;
            }
        }
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
        if ($I("tblRelacion") == null) return;
        if ($I("divRelacion").scrollTop != nTopScrollProf) {
            nTopScrollProf = $I("divRelacion").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        var tblRelacion = $I("tblRelacion");
        var nFilaVisible = Math.floor(nTopScrollProf / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacion").offsetHeight / 20 + 1, tblRelacion.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblRelacion.rows.length; i++){
            if (!tblRelacion.rows[i].getAttribute("sw")) {
                oFila = tblRelacion.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }

                if ($I("hdnAcceso").value != "R") {
                    oFila.attachEvent("onclick", mm);
                    oFila.attachEvent("onmousedown", DD);
                    oFila.ondblclick = function() { insertarRecurso(this); };
                }

                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[1].style.color = "maroon";
                }
            }
            //            nContador++;
            //            if (nContador > $I("divRelacion").offsetHeight/20 +1) break;
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

        if ($I("divAsignados").scrollTop != nTopScrollProfAsig) {
            nTopScrollProfAsig = $I("divAsignados").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }

        var tblAsignados = $I("tblAsignados");
        var nFilaVisible = Math.floor(nTopScrollProfAsig / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divAsignados").offsetHeight / 20 + 1, tblAsignados.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblAsignados.rows.length; i++){
            if (!tblAsignados.rows[i].getAttribute("sw")) {
                oFila = tblAsignados.rows[i];
                oFila.setAttribute("sw", 1);


                if ($I("hdnAcceso").value == "V") oFila.attachEvent("onclick", mm_rtpt);
                else oFila.attachEvent("onclick", mm);

                oFila.onclick = function() { mostrarDatosAsignacion(this.id); };

                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[2].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[2].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[2].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[2].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[2].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[2].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[2].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[2].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[3].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[3].style.color = "maroon";
                }
            }
            //            nContador++;
            //            if (nContador > $I("divAsignados").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollRTPT = -1;
var nIDTimeRTPT = 0;
function scrollTablaRTPT() {
    try {
        if ($I("tblRelacion2") == null) return;
        if ($I("divRTPTcandidato").scrollTop != nTopScrollRTPT) {
            nTopScrollRTPT = $I("divRTPTcandidato").scrollTop;
            clearTimeout(nIDTimeRTPT);
            nIDTimeRTPT = setTimeout("scrollTablaRTPT()", 50);
            return;
        }

        var tblRelacion2 = $I("tblRelacion2");
        var nFilaVisible = Math.floor(nTopScrollRTPT / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divRTPTcandidato").offsetHeight / 20 + 1, tblRelacion2.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblRelacion2.rows.length; i++){
            if (!tblRelacion2.rows[i].getAttribute("sw")) {
                oFila = tblRelacion2.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
            }
            //            nContador++;
            //            if (nContador > $I("divRTPTcandidato").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales RTPT.", e.message);
    }
}

var nTopScrollRTPTAsig = -1;
var nIDTimeRTPTAsig = 0;
function scrollTablaRTPTAsig() {
    try {
        if ($I("divRTPTAsignados").scrollTop != nTopScrollRTPTAsig) {
            nTopScrollRTPTAsig = $I("divRTPTAsignados").scrollTop;
            clearTimeout(nIDTimeRTPTAsig);
            nIDTimeRTPTAsig = setTimeout("scrollTablaRTPTAsig()", 50);
            return;
        }

        var tblAsignados2 = $I("tblAsignados2");
        var nFilaVisible = Math.floor(nTopScrollRTPTAsig / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divRTPTAsignados").offsetHeight / 20 + 1, tblAsignados2.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblAsignados2.rows.length; i++){
            if (!tblAsignados2.rows[i].getAttribute("sw")) {
                oFila = tblAsignados2.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent("onclick", mm);
                oFila.attachEvent("onmousedown", DD);

                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
            }
            //            nContador++;
            //            if (nContador > $I("divRTPTAsignados").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados RTPT.", e.message);
    }
}

var nTopScrollPool = -1;
var nIDTimePool = 0;
function scrollTablaPool() {
    try {
        if ($I("divRelacionPool").scrollTop != nTopScrollPool) {
            nTopScrollPool = $I("divRelacionPool").scrollTop;
            clearTimeout(nIDTimePool);
            nIDTimePool = setTimeout("scrollTablaPool()", 50);
            return;
        }

        var tblRelacion3 = $I("tblRelacion3");
        var nFilaVisible = Math.floor(nTopScrollPool / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacionPool").offsetHeight / 20 + 1, tblRelacion3.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblRelacion3.rows.length; i++){
            if (!tblRelacion3.rows[i].getAttribute("sw")) {
                oFila = tblRelacion3.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[1].style.color = "maroon";
                }
            }
            //            nContador++;
            //            if (nContador > $I("divRelacionPool").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de pool.", e.message);
    }
}

var nTopScrollPoolAsig = -1;
var nIDTimePoolAsig = 0;
function scrollTablaPoolAsig() {
    try {
        if ($I("divPoolProf").scrollTop != nTopScrollPoolAsig) {
            nTopScrollPoolAsig = $I("divPoolProf").scrollTop;
            clearTimeout(nIDTimePoolAsig);
            nIDTimePoolAsig = setTimeout("scrollTablaPoolAsig()", 50);
            return;
        }
        var tblAsignados3 = $I("tblAsignados3");
        var nFilaVisible = Math.floor(nTopScrollPoolAsig / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divPoolProf").offsetHeight / 20 + 1, tblAsignados3.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblAsignados3.rows.length; i++){
            if (!tblAsignados3.rows[i].getAttribute("sw")) {
                oFila = tblAsignados3.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent("onclick", mm);

                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[2].style.color = "maroon";
                }
            }
            //            nContador++;
            //            if (nContador > $I("divPoolProf").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function getRecursos() {
    try {
        mostrarProcesando();
        var sVer;
        if ($I("chkVerBajas").checked) sVer = "S";
        else sVer = "N";
        RealizarCallBack("getRecursos@#@" + $I("txtIdPT").value.replace('.', '') + "@#@" + $I("hdnCRActual").value + "@#@" + sVer, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de profesionales ", e.message);
    }
}
//Reasigna los profesionales seleccionados si estaban asociados al proyecto con fecha de baja
function reAsignar() {
    try {
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                if (aFila[i].getAttribute("baja") == "1") {
                    aFila[i].cells[3].style.color = "";
                    mfa(aFila[i], "U");
                    aGProf(0);
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reasignar usuarios.", e.message);
    }
}
function getRecursosPool() {
    try {
        mostrarProcesando();
        var sVer;
        if ($I("chkVerBajasPool").checked) sVer = "S";
        else sVer = "N";
        RealizarCallBack("getRecursosPool@#@" + $I("hdnIDPT").value + "@#@" + $I("hdnCRActual").value + "@#@" + sVer, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de pool de profesionales ", e.message);
    }
}
function setCamposEstadoBusq() {
    try {
        if (gsModo == "B") {
            $I("hdnIdPE").readOnly = false;
            $I("txtIdPT").readOnly = false;

            if ($I("txtIdPT").value == "") {
                $I("txtDesPT").readOnly = true;
                $I("txtDesPT").onkeyup = "";
                $I("txtDesPT").onkeypress = "";

                $I("txtDescripcion").readOnly = true;
                $I("txtDescripcion").onkeyup = "";

                $I("txtValIni").readOnly = true;
                $I("txtValFin").readOnly = true;
                if (btnCal == "I") {
                    $I("txtValIni").onclick = "";
                    $I("txtValIni").onchange = "";
                    $I("txtValFin").onclick = "";
                    $I("txtValFin").onchange = "";
                }
                else {
                    $I("txtValIni").onmousedown = "";
                    $I("txtValIni").onfocus = "";
                    $I("txtValFin").onmousedown = "";
                    $I("txtValFin").onfocus = "";
                }
                $I("cboEstado").disabled = true;
                $I("cboAccesoIAP").disabled = true;

            }
            else {
                if ($I("hdnAcceso").value != "V") {
                    $I("txtDesPT").readOnly = false;
                    $I("txtDesPT").onkeyup = function () { aG(0); };
                    $I("txtDesPT").attachEvent("onkeypress", teclaDenominacion);
                    //$I("txtDesPT").onkeypress=function (){teclaDenominacion();};

                    $I("txtDescripcion").readOnly = false;
                    $I("txtDescripcion").onkeyup = function () { aG(0); };

                    $I("txtValIni").onchange = function () { modificarVigencia(); };
                    $I("txtValFin").onchange = function () { modificarVigencia(); };
                    if (btnCal == "I") {
                        $I("txtValIni").onclick = function () { mc(this); };
                        $I("txtValFin").onclick = function () { mc(this); };
                        $I("txtValIni").readOnly = true;
                        $I("txtValFin").readOnly = true;
                    }
                    else {
                        $I("txtValIni").readOnly = false;
                        $I("txtValFin").readOnly = false;
                        $I("txtValIni").onmousedown = function () { mc1(this); };
                        //$I("txtValIni").onfocus=function (){focoFecha(this);};
                        $I("txtValIni").attachEvent("onfocus", focoFecha);
                        $I("txtValFin").onmousedown = function () { mc1(this); };
                        //$I("txtValFin").onfocus=function (){focoFecha(this);};
                        $I("txtValFin").attachEvent("onfocus", focoFecha);
                    }
                    $I("cboEstado").disabled = false;
                    $I("cboAccesoIAP").disabled = false;
                }
            }
        }
        else {
            
            $I("txtIdPT").readOnly = true;

            if (gsModo == "C") {
                $I("txtValIni").readOnly = true;
                $I("txtValFin").readOnly = true;
                if (btnCal == "I") {
                    $I("txtValIni").onclick = "";
                    $I("txtValIni").onchange = "";
                    $I("txtValFin").onclick = "";
                    $I("txtValFin").onchange = "";
                }
                else {
                    $I("txtValIni").onmousedown = "";
                    $I("txtValIni").onfocus = "";
                    $I("txtValFin").onmousedown = "";
                    $I("txtValFin").onfocus = "";
                }
            }
            else {
                $I("hdnIdPE").readOnly = true;
                if (!bLectura && $I("hdnAcceso").value != "V") {
                    $I("txtDesPT").readOnly = false;
                    $I("txtDesPT").onkeyup = function() { aG(0); };
                    $I("txtDesPT").attachEvent("onkeypress", teclaDenominacion);

                    $I("txtDescripcion").readOnly = false;
                    $I("txtDescripcion").onkeyup = function() { aG(0); };
                    if (btnCal == "I") {
                        $I("txtValIni").readOnly = true;
                        $I("txtValFin").readOnly = true;
                        $I("txtValIni").onclick = function() { mc(this); };
                        $I("txtValFin").onclick = function() { mc(this); };
                    }
                    else {
                        $I("txtValIni").readOnly = false;
                        $I("txtValFin").readOnly = false;
                        $I("txtValIni").onmousedown = function() { mc1(this); };
                        //$I("txtValIni").onfocus=function (){focoFecha(this);};
                        $I("txtValIni").attachEvent("onfocus", focoFecha);
                        $I("txtValFin").onmousedown = function() { mc1(this); };
                        //$I("txtValFin").onfocus=function (){focoFecha(this);};
                        $I("txtValFin").attachEvent("onfocus", focoFecha);
                    }
                    $I("txtValIni").onchange = function() { modificarVigencia(); };
                    $I("txtValFin").onchange = function() { modificarVigencia(); };

                    $I("cboEstado").disabled = false;
                    $I("cboAccesoIAP").disabled = false;
                } 
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reasignar usuarios.", e.message);
    }
}

function setIconoBitacora() {
    try {
        if ($I("hdnAcceso").value == "V" || $I("hdnAcceso").value == "W") {
            $I("btnBitacora").src = "../../../images/imgBTPTW.gif";
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo esritura a la bitácora de proyecto técnico.";
        }
        else {
            $I("btnBitacora").src = "../../../images/imgBTPTR.gif";
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de proyecto técnico.";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el icono de la bitácora.", e.message);
    }
}
function mm_rtpt(e) {
    try { //alert("Entra mm");
        if (bLectura && $I("hdnAcceso").value != "V") return;
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;

        //var oFila = oElement.parentNode.parentNode;

        var bFila = false;
        while (!bFila) {
            if (oElement.tagName.toUpperCase() == "TR") bFila = true;
            else {
                oElement = oElement.parentNode;
                if (oElement == null)
                    return;
            }
        }

        var oFila = oElement;

        var oTabla = oFila.parentNode.parentNode;
        if (sTabla != oTabla.id) {
            clearVarSel();
            sTabla = oTabla.id;
        }

        var nFila = oFila.rowIndex;

        var nMantenimiento = 0;
        try { nMantenimiento = oTabla.getAttribute("mantenimiento") } catch (e) { }
        //alert(event.ctrlKey);
        //alert(event.shiftKey);
        if (e.ctrlKey) {  //Tecla control pulsada
            //document.selection.empty();
            try {
                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();
            } catch (e) { };

            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].style.display == "none") continue;
                if (i == nFila) break;
            }

            if (oFila.className == "FS") {
                if (nfs > 1) {
                    nfs--;
                    //oFila.setAttribute("class","");
                    oFila.className = "";
                }
            } else {
                nfs++;
                //oFila.setAttribute("class","FS");
                oFila.className = "FS";
                iFila = oFila.rowIndex;
            }

            if (nMantenimiento == 1) modoControles(oFila, false);
        } else if (e.shiftKey) {	//Tecla shift pulsada
            //if (nfs > 0) document.selection.empty();
            if (nfs > 0) {
                try {
                    if (window.getSelection) window.getSelection().removeAllRanges();
                    else if (document.selection && document.selection.empty) document.selection.empty();
                } catch (e) { };
            }
            var nff = nFila;
            if (nfo > nff) {
                nff = nfo;
                nfi = nFila;
            }
            if (nfi < nfo) nff = nfo;
            if (nFila > nfo) {
                nfi = nfo
                nff = nFila;
            }

            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].style.display == "none") continue;

                if (i >= nfi && i <= nff) {
                    if (oTabla.rows[i].className != "FS") {
                        nfs++;
                        iFila = i;
                        oTabla.rows[i].className = "FS";
                    }
                } else {
                    if (oTabla.rows[i].className == "FS") {
                        nfs--;
                        oTabla.rows[i].className = "";
                    }
                }

                if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
            }
            //alert("Control:\n\nnfo: "+ nfo +" nfi: "+ nfi +" nff: "+ nff);
        }
        else {  //teclas ni control ni shift pulsadas.
            var j = 0;
            for (var i = 0; i < oTabla.rows.length; i++) {
                //		        alert("display: "+oTabla.rows[i].style.display);
                if (oTabla.rows[i].style.display == "none") continue;
                if (i == nFila) {
                    nfo = i;
                    nfi = i;
                    nff = i;

                    if (oFila.className != "FS") {
                        nfs++;
                        iFila = i;
                        oFila.className = "FS";
                    }

                    if (nMantenimiento == 1) {
                        modoControles(oFila, true);
                        iFila = i;
                    }
                } else {

                    if (oTabla.rows[i].className == "FS") {
                        nfs--;
                        oTabla.rows[i].className = "";
                    }

                    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
                }
            }
            //alert("nfo: "+ nfo +" nfi: "+ nfi +" nff: "+ nff);
        }
        //	    var r = document.body.createTextRange();
        //	    r.findText(" ");
        //	    r.select();	    
    } catch (e) {
        mostrarErrorAplicacion("Error en la selección múltiple (mm)", e.message);
    }
}
function buscarPE() {
    try {
        $I("hdnIdPE").value = dfnTotal($I("hdnIdPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("hdnIdPE").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar el PE.", e.message);
    }
}
function limpiarPE() {
    $I("hdnT305IdProy").value = "";
    $I("txtPE").value = "";
    limpiarPT();
}
function buscarPT() {
    try {
        var nIdPt = dfnTotal($I("txtIdPT").value);
        $I("txtIdPT").value = nIdPt.ToString("N", 9, 0);
        var js_args = "buscarPT@#@" + nIdPt;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar el PT.", e.message);
    }
}
function limpiarPT() {
    try {
        if ($I("txtDesPT").value != "") {
            //$I("txtIdPT").value = "";
            $I("txtDesPT").value = "";
            $I("hdnIDPT").value = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el PT", e.message);
    }
}
function getPEByNum() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&nPE=" + dfn($I("hdnIdPE").value);
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    if (aDatos[1] == "1") {
                        bLectura = true;
                    } else {
                        bLectura = false;
                    }
                    if (es_administrador == "SA" || es_administrador == "A") {
                        bRTPT = false;
                    }
                    else {
                        if (aDatos[2] == "1") {
                            bRTPT = true;
                        } else {
                            bRTPT = false;
                        }
                    }
                    recuperarDatosPSN();
                } else {
                    limpiarPE();
                    ocultarProcesando();
                }
            });
        window.focus();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}
//Inicio campos de PT

function cargarCamposPorAmbito(codAmbito) {
    try {

        var js_args = "cargarCamposPorAmbito@#@";
        js_args += (codAmbito == "") ? $I("cboAmbito").value : codAmbito + "@#@";

    //alert(js_args);
    //mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la tabla", e.message);
    }
}

function MtoValores() {
    try {
        mostrarProcesando();
        ventanaPadre = fOpener();
        var strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/Campos/Default.aspx?nPT=" + $I("hdnIDPT").value + "&t305_idproyectosubnodo=" + $I("hdnT305IdProy").value;
        ocultarProcesando();
        modalDialog.Show(strEnlace, self, sSize(950, 560))
            .then(function (ret) {
                
                mostrarProcesando();
                cargarCamposPorAmbito("");
                ocultarProcesando();
            });

            } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el mantenimiento de valores.", e.message);
   }
}

function asociarCampo(oFila, bMsg) {
    try {

        var idItem = oFila.id;
        var bExiste = false;
        var tblCampos = $I("tblCampos");
        var sw = 0;
        for (var i = 0; i < tblCampos.rows.length; i++) {
            if (tblCampos.rows[i].id == idItem) {//Si existe
                sw = 1;
                bExiste = true;
                if (tblCampos.rows[i].getAttribute("bd") == "D") { //Si está pdte de borrar
                    mfa(tblCampos.rows[i], "U");
                    aGAvanza(1);
                } else {
                    if (bMsg) mmoff("Inf", "El campo seleccionado ya está asociado al proyecto técnico.", 400);
                    break;
                }
                break;
            }
        }
       
        if (sw == 0) {

            var iFilaNueva = 0;
            var sNombreNuevo, sNombreAct;

            var sNuevo = oFila.innerText;
            for (var iFilaNueva = 0; iFilaNueva < tblCampos.rows.length; iFilaNueva++) {
                //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                var sActual = tblCampos.rows[iFilaNueva].innerText;
                if (sActual > sNuevo) break;
            }

            // Se inserta la fila         
            var NewRow = $I("tblCampos").insertRow(iFilaNueva);
            var oCloneNode = oFila.cloneNode(true);
            //oCloneNode.style.cursor = strCurMM;
            oCloneNode.className = "";
            oCloneNode.setAttribute("bd", "I");
            oCloneNode.removeAttribute("ondblclick");
            NewRow.swapNode(oCloneNode);

           
            oCloneNode.insertCell(0);
            oCloneNode.cells[0].setAttribute("style", "padding-left:2px;");
            oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));

            var oCtrl2 = document.createElement("nobr");
            oCtrl2.className = "NBR W60";
            oCtrl2.innerHTML = oCloneNode.getAttribute("descdato");
            oCloneNode.insertCell(-1).appendChild(oCtrl2);

            var oCtrl1 = document.createElement("input");
            oCtrl1.type = "text";
            oCtrl1.style.width = "95px";

            switch (oCloneNode.getAttribute("tipodato")) {
                case "I":
                    oCtrl1.className = "txtNumM";
                    oCtrl1.value = "0,00";
                    oCtrl1.style.width = "70px";
                    oCtrl1.onkeyup = function () {
                        fm(this)
                    };
                    oCtrl1.onfocus = function () { fn(this, 7, 2) };
                    oCtrl1.onchange = function () {
                        aGAvanza(1);
                    };
                    break;
                case "H":
                case "F":
                    if (btnCal == "I") {
                        oCtrl1.id = "fecha_" + idItem;
                        oCtrl1.readOnly = true;
                        oCtrl1.className = "txtFecM";
                        oCtrl1.setAttribute("class", "txtFecM");
                        oCtrl1.setAttribute("style", "width:70px;cursor:pointer");
                        oCtrl1.setAttribute("Calendar", "oCal");
                        oCtrl1.setAttribute("goma", "1");
                        oCtrl1.onchange = function () {
                            aGAvanza(1);
                            fm(this);
                        };
                        oCtrl1.onclick = function () { mc(this) };                        
                    }
                    else {
                        oCtrl1.id = "fecha_" + idItem;
                        oCtrl1.className = "txtFecM";
                        oCtrl1.setAttribute("class", "txtFecM");
                        oCtrl1.setAttribute("style", "width:70px;cursor:pointer");
                        oCtrl1.setAttribute("Calendar", "oCal");
                        oCtrl1.setAttribute("goma", "1");
                        oCtrl1.onchange = function () { aGAvanza(1); fm(this); };
                        //oCtrl1.attachEvent("onmousedown", mc1);
                        oCtrl1.onmousedown = function () {
                            mc1(this);
                        };
                       
                        oCtrl1.attachEvent("onfocus", focoFecha);
                    }
                    break;
                case "T":
                    oCtrl1.className = "txtM";
                    oCtrl1.style.width = "360px";
                    oCtrl1.maxLength = "50";
                    oCtrl1.onchange = function () { aGAvanza(1);}
                    break;
            }
            oCloneNode.insertCell(-1).appendChild(oCtrl1);
            if (oCloneNode.getAttribute("tipodato") != "H") oCloneNode.cells[3].setAttribute("colspan", "5");
            else {
                var oCtrl3 = document.createElement("select");
                oCtrl3.style.width = "39px";
                oCtrl3.setAttribute("class", "combo");
                oCtrl3.onchange = function () {
                    aGAvanza(1);
                    mfa(this.parentNode.parentNode, 'U');
                };

                var valor;
                for (var i = 0; i <= 23; i++) {
                    if (i < 10) valor = "0" + i;
                    else valor = i;
                    oCtrl3.options[i] = new Option(valor, valor);
                }

                oCloneNode.insertCell(-1).appendChild(oCtrl3);

                var oCtrl4 = document.createElement("select");
                oCtrl4.style.width = "39px";
                oCtrl4.setAttribute("class", "combo");
                oCtrl4.onchange = function () {
                    aGAvanza(1);
                    mfa(this.parentNode.parentNode, 'U');
                };

                var valor;
                for (var i = 0; i <= 59; i++) {
                    if (i < 10) valor = "0" + i;
                    else valor = i;
                    oCtrl4.options[i] = new Option(valor, valor);
                }
                oCloneNode.insertCell(-1).appendChild(oCtrl4);

                var oCtrl5 = document.createElement("select");
                oCtrl5.style.width = "39px";
                oCtrl5.setAttribute("class", "combo");
                oCtrl5.onchange = function () {
                    aGAvanza(1);
                    mfa(this.parentNode.parentNode, 'U');
                };

                var valor;
                for (var i = 0; i <= 59; i++) {
                    if (i < 10) valor = "0" + i;
                    else valor = i;
                    oCtrl5.options[i] = new Option(valor, valor);
                }
                oCloneNode.insertCell(-1).appendChild(oCtrl5);
                oCloneNode.insertCell(-1);
            }
            //actualizarLupas("tblAsignados", "tblCampos");
            //ot('tblDatos2', 0, 0, '', '');        

            //Compruebo si el Campo está usado en alguna tarea para avisar al usuario
            var IdCampo;
            for (var i = 0; i < aCamposTareas.length; i++) {
                if (aCamposTareas[i] == idItem) { //Si existe
                    IdCampo = aCamposTareas[i];
                    var msg = "El campo seleccionado ya está asociado a una o varias tareas del proyecto técnico<br />Si lo asocias, perderás los valores que están en esas tareas.<br /><br />¿Deseas mostrar las tareas que tienen el campo?";
                    jqConfirm("", msg, "", "", "war", 500).then(function (answer) {
                        if (answer)
                            mostrarTareasConCampo(IdCampo);
                    });
                }
            }

            aG(3);
            aGAvanza(1);
        }
        return iFilaNueva;

    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}

function mostrarTareasConCampo(IdCampo) {
    try {
        var js_args = "mostrarTareasConCampo@#@" + $I("hdnIDPT").value + "@#@" + IdCampo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las tareas con un campo libre", e.message);
        }
}

/*function insertarTabla(aElementos, strName) {
    try {
        //BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var bYaEsta = false;
            for (var j = 0; j < $I("tblCampos").rows.length; j++) {
                if (aDatos[0] == $I("tblCampos").rows[j].id) {
                    bYaEsta = true;
                    break;
                }
            }
            if (bYaEsta) continue;

            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.setAttribute("bd", "I");
            oNF.setAttribute("tipodato", aDatos[2]);
            oNF.style.height = "20px";
            oNF.attachEvent('onclick', mm);

            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true), null);

            var oNobr = document.createElement("NOBR");
            oNobr.className = "NBR W360";
            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
            oNF.cells[1].children[0].className = "NBR";
            oNF.cells[1].children[0].setAttribute("style", "width:360px;");
            oNF.cells[1].children[0].attachEvent("onmouseover", TTip);
            oNF.cells[1].children[0].innerHTML = Utilidades.unescape(aDatos[1]);

            oNC2 = oNF.insertCell(-1);
            var oCtrl1 = document.createElement("input");
            oCtrl1.type = "text";
            oCtrl1.style.width = "95px";

            switch (aDatos[2]) {
                case "I":
                    oCtrl1.className = "txtNumM";
                    oCtrl1.value = "0,00";
                    oCtrl1.onkeyup = function () { fm(this) };
                    oCtrl1.onfocus = function () { fn(this, 7, 2) };

                    break;
                case "F":
                    if (btnCal == "I") {
                        oCtrl1.id = "fecha_" + aDatos[0];
                        oCtrl1.readOnly = true;
                        oCtrl1.className = "txtL";
                        oCtrl1.setAttribute("class", "txtL");
                        oCtrl1.setAttribute("style", "width:60px;cursor:pointer");
                        oCtrl1.setAttribute("Calendar", "oCal");
                        oCtrl1.setAttribute("goma", "1");
                        oCtrl1.onchange = function () { fm(this); };
                        oCtrl1.onclick = function () { mc(this) };
                    }
                    else {
                        oCtrl1.id = "fecha_" + aDatos[0];
                        oCtrl1.className = "txtL";
                        oCtrl1.setAttribute("class", "txtL");
                        oCtrl1.setAttribute("style", "width:60px;cursor:pointer");
                        oCtrl1.setAttribute("Calendar", "oCal");
                        oCtrl1.setAttribute("goma", "1");
                        oCtrl1.onchange = function () { fm(this); };
                        //oCtrl1.attachEvent("onmousedown", mc1);
                        oCtrl1.onmousedown = function () { mc1(this); };
                        oCtrl1.attachEvent("onfocus", focoFecha);
                    }
                    break;
                case "T":
                    oCtrl1.className = "txtM";
                    oCtrl1.style.width = "440px";
                    break;
            }
            oNC2.appendChild(oCtrl1);
            aGAvanza(1)
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

function eliminarValor() {
    try {
        aFila = FilasDe("tblCampos");
        if (aFila == null) return;
        for (var i = aFila.length - 1; i >= 0; i--) {
            if (aFila[i].className == "FS") {
                if (aFila[i].getAttribute("bd") == "I") {
                    $I("tblCampos").deleteRow(i);
                } else {
                    mfa(aFila[i], "D");
                }
            }
        }
        aGAvanza(1);
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}*/


function MtoGP() {
    try {
        mostrarProcesando();
        //window.focus();
        var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/GP/Catalogo/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(950, 560))
            .then(function (ret) {
                if (ret != null) {
                    //var aDatos = ret.split("@#@");
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir al mantenimiento de grupos de profesionales", e.message);
    }
}

//Fin campos de PT