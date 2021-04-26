//Vbles globales para el control de cambio de padres de una tarea
var gsIdPE, gsIdPT, gsIdFase, gsIdActividad;
var bConsumosDeRecursos = false;
var bHayCambios = false;
var bNotificarApertura = false;
var bNotificarCierre = false;
var bEnvMensGral = false;
var sEstadoInicial;
var bSaliendo = false;
//var bClickMostrarBajas = false;
var sPSNAux = "";
var strAction; //Submitea el formulario Report
var strTarget; //Submitea el formulario Report
var js_Valores = new Array();
var bDuplicar = false;
var bExportar = false;
var bLimpiar = false;
var bGetRecurso = false;
var bBitacora = false;
var lstCamposPT="";

function init() {
    try {
        
        if (!mostrarErrores()) {
            iniciarPestanas();
            LlamarLimpiar();
            setOp($I("btnDuplicar"), 30);
            setOp($I("btnBorrar"), 30);
            setOp($I("btnPDF"), 30);
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
            setCamposEstadoBusq();
            $I("txtIdTarea").readOnly = false;
            $I("lblDesTarea").className = "enlace";
            $I("lblDesTarea").style.width = "80px";
            $I("btnNuevo").children[1].innerText = "Limpiar";
            $I("btnNuevo").children[1].title = "Limpia datos de la pantalla";
            $I("txtIdTarea").value = "";            
            setEstadoDatosAsignacion(false);
            return;
        } 
        iniciarPestanas();
        //Acciones en función del modo de entrada Crear o Buscar
        $I("txtIdTarea").readOnly = true;
        if (gsModo == "C") {
            //            $I("lblTarea").className="";
            //            $I("lblTarea").onclick = null;
            $I("lblDesTarea").className = "";
            $I("lblDesTarea").style.width = "70px";
            $I("lblDesTarea").onclick = null;
            $I("btnNuevo").children[1].innerText = "Nueva";
            $I("btnNuevo").children[1].title = "Nueva tarea";
        }
        else {
            if (gsModo == "B") {
                //$I("lblTarea").className="enlace";
                $I("txtIdTarea").readOnly = false;
                $I("lblDesTarea").className = "enlace";
                $I("lblDesTarea").style.width = "80px";
                $I("btnNuevo").children[1].innerText = "Limpiar";
                $I("btnNuevo").children[1].title = "Limpia datos de la pantalla";
            }
        }
        if ($I("hdnAcceso").value != "R") setCamposEstadoBusq();

        if ($I("hdnEstr").value == "S") {
            setOp($I("btnNuevo"), 30);
            $I("lblProy").className = "";
            $I("lblProy").onclick = null;
            //$I("lblTarea").className="";
            //$I("lblTarea").onclick = null;
            $I("lblDesTarea").className = "";
            $I("lblDesTarea").style.width = "70px";
            $I("lblDesTarea").onclick = null;
        }
        //Guardo en vbles globales los valores de inicio para Proyecto Económico, Proyecto Técnico, Fase y Actividad
        gsIdPE = $I("txtNumPE").value;
        gsIdPT = $I("hdnIDPT").value;
        gsIdFase = $I("hdnIDFase").value;
        gsIdActividad = $I("hdnIDAct").value;

        //Acciones si entro a una tarea que no existe todavía
        if (($I("txtIdTarea").value == "") || ($I("txtIdTarea").value == "0")) {
            sEstado = "1"; //Activa
            $I("cboEstado").value = sEstado;
            $I("hdnEstado").value = sEstado;
            var anio, mes, dia;
            var Mi_Fecha = new Date();
            anio = Mi_Fecha.getFullYear();
            mes = Mi_Fecha.getMonth() + 1;
            if (mes.toString().length == 1) mes = "0" + mes;
            dia = Mi_Fecha.getDate();
            if (dia.toString().length == 1) dia = "0" + dia;
            var sFecha = dia + "/" + mes + "/" + anio;
            $I("txtValIni").value = sFecha;
            setOp($I("btnDuplicar"), 30);
            setOp($I("btnBorrar"), 30);
            setOp($I("btnPDF"), 30);
            setInformativos();
        }
        else {
            $I("txtIdTarea").readOnly = true;
            $I("lblProy").className = "";
            $I("lblPT").className = "";
            $I("lblFase").className = "";
            $I("lblActividad").className = "";
            $I("Image8").style.visibility = "hidden";
            $I("Image9").style.visibility = "hidden";

            setOp($I("btnDuplicar"), 100);
            setOp($I("btnPDF"), 100);
            //Sino hay consumos habilito el botón de borrado
            if (($I("txtConHor").value == "") || ($I("txtConHor").value == "0") || ($I("txtConHor").value == "0,00"))
                setOp($I("btnBorrar"), 100);
            else
                setOp($I("btnBorrar"), 30);
            if (sRecPST == "T")
                $I("lblInsPST").style.visibility = "hidden";
            else
                $I("lblInsPST").style.visibility = "visible";

            setIconoBitacora();
        }       

        if ($I("hdnNivelPresupuesto").value == "T") {
            $(".ocultarcapa").removeClass();
            $I("idFieldsetIAP").style.height = "154px";
            $I("idFieldsetSituacion").style.height = "";

            if ($I("chkAvanceAuto").checked) clickAvanceAutomatico();
            $I("txtPresupuesto").onkeyup = function () { calcularProducido(); aG(0); };
            $I("txtAvanReal").onkeyup = function () { calcularProducido(); aG(0); };


        } else {
            $I("txtPresupuesto").readOnly = true;
            $I("txtAvanReal").readOnly = true;
            $I("chkAvanceAuto").disabled = true;
        }

       
        //comprobarAEObligatorios();

        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);

        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesTarea").value;
        sFIPL = $I("txtPLIni").value;
        sFFPL = $I("txtPLFin").value;
        sETPL = $I("txtPLEst").value;
        sPresupuesto = $I("txtPresupuesto").value;
        sEstado = $I("hdnEstado").value;
        sFIV = $I("txtValIni").value;
        sFFV = $I("txtValFin").value;
        if ($I("chkFacturable").checked) sFacturable = "1";
        else sFacturable = "0";
        //nuevas variables a devolver al gantt
        sETPR = $I("txtPREst").value;
        sFFPR = $I("txtPRFin").value;
        sAvanR = $I("txtAvanReal").value;

        //Variable para el control de estado.
        sEstadoOrig = sEstado;
        //Control del avance real
        if ($I("chkAvanceAuto").checked) {
            $I("txtAvanReal").value = $I("txtAvanTeo").value;
            $I("txtAvanReal").readOnly = true;
        }
        //Carga del combo de estado en función del estado de la tarea
        cargarComboEstado(sEstado);
        calcularProducido();

        bCambios = false;
        bHayCambios = false;

        sEstadoInicial = $I("cboEstado").value;
        if ($I("chkNotificable").checked)
            bNotificarApertura = true;
        //Si Cerrada o Finalizada o Anulada
        if ($I("cboEstado").value == "3" || $I("cboEstado").value == "4" || $I("cboEstado").value == "5") {
            bNotificarCierre = true;
        }
        //Si Cerrada o Finalizada
        if ($I("cboEstado").value == "3" || $I("cboEstado").value == "4") {
            $I("spanImpIAP").style.visibility = "visible";
        }
        else
            $I("chkImpIAP").checked = false;
        //if ($I("chkFinalizada").checked) bNotificarCierre = true;
        calcularPorcentajes();
        calcularDesvPlazo2();
        //activación de campos en función de si la OTC es heredada o no
        if ($I("hdnOtcH").value == "T") {
            activarOTC(false);
        }
        else {
            activarOTC(true);
        }

        if ($I("hdnOrigen").value == "gantt") {
            setOp($I("btnNuevo"), 30);
            setOp($I("btnDuplicar"), 30);
            $I("lblProy").className = "";
            $I("lblPT").className = "";
            $I("lblFase").className = "";
            $I("lblActividad").className = "";
            //$I("lblTarea").className="";
            $I("lblDesTarea").className = "";
            $I("lblDesTarea").style.width = "70px";
        }
        if ($I("hdnAcceso").value != "W") {
            setOp($I("btnBorrar"), 30);
            //setOp($I("Button1"), 30);
            //setOp($I("Button2"), 30);
            setOp($I("btnAGF"), 30);
            setOp($I("btnBGF"), 30);
        }
        else {
            //if ($I("hdnEstProy").value=="C" || $I("hdnEstProy").value=="C"){
            if ($I("hdnEstProy").value == "C") {
                setOp($I("btnBorrar"), 30);
                //setOp($I("Button2"), 30);
                setOp($I("btnAGF"), 30);
                setOp($I("btnBGF"), 30);
            }
        }
        setEstadoDatosAsignacion(false);
        if ($I("hdnAcceso").value == "R") bLectura = true;
        ocultarProcesando();
        strAction = document.forms[0].action;
        strTarget = document.forms[0].target;
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function aceptar() {
    var strRetorno = "F";
    bSalir = false;
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T";
    }
    strRetorno += "@#@T@#@" + sDescripcion + "@#@";
    strRetorno += sFIPL + "@#@"; //3
    strRetorno += sFFPL + "@#@"; //4
    strRetorno += sETPL + "@#@"; //5
    strRetorno += sPresupuesto + "@#@"; //6
    strRetorno += sEstado + "@#@"; //7
    strRetorno += sFIV + "@#@"; //8
    strRetorno += sFFV + "@#@"; //9
    strRetorno += sFacturable + "@#@"; //10
    strRetorno += $I("hdnRecargar").value + "@#@"; //11
    //nuevas variables a devolver al gantt
    strRetorno += sETPR + "@#@"; //12
    strRetorno += sFFPR + "@#@"; //13
    strRetorno += sAvanR + "@#@"; //14
    strRetorno += sAutomatico; //15

    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function unload() {
    
}

function salir() {
    bSalir = false;
    bSaliendo = true;

    if ($I("hdnAcceso").value != "R") {
        if (bCambios && intSession > 0) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    salirCerrarVentana();
                }
            });
        } else salirCerrarVentana();
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
    var strRetorno = "F";
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T";
    }

    strRetorno += "@#@T@#@" + sDescripcion + "@#@" + sFIPL + "@#@";
    strRetorno += sFFPL + "@#@";
    strRetorno += sETPL + "@#@";
    strRetorno += sPresupuesto + "@#@";
    strRetorno += sEstado + "@#@";
    strRetorno += sFIV + "@#@";
    strRetorno += sFFV + "@#@";

    strRetorno += sFacturable + "@#@";
    strRetorno += $I("hdnRecargar").value + "@#@";
    //nuevas variables a devolver al gantt
    strRetorno += sETPR + "@#@";
    strRetorno += sFFPR + "@#@";
    strRetorno += sAvanR + "@#@";
    strRetorno += sAutomatico;

    var returnValue = strRetorno;
    //setTimeout("window.close();", 250); //para que de tiempo a grabar y actualizar "bCambios";
    modalDialog.Close(window, returnValue);
}
//function tratarTecnicosDeBaja(){
//try{
//    var aFila = FilasDe("tblRelacion");
//    for (var i=aFila.length-1;i>=0;i--){
//        if (aFila[i].Estado=="0") aFila[i].style.color="Red";
//    }
//	}catch(e){
//		mostrarErrorAplicacion("Error al comprobar profesionales de baja", e.message);
//    }
//}
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
        mostrarError(aResul[2].replace(reg, "\n"));
        if (aResul[3] != null) mostrarDeNuevoRecurso(aResul[3]);
    } else {
        switch (aResul[0]) {
            case "getDatosProy":
                if (aResul[2] == "0")
                    $I("chkFacturable").checked = false;
                else
                    $I("chkFacturable").checked = true;
                $I("txtNumPE").value = $I("txtNumPE").value.ToString("N", 9, 0);
                $I("hdnIDPT").value = "";
                $I("txtPT").value = "";
                $I("hdnIDFase").value = "";
                $I("txtFase").value = "";
                $I("hdnIDAct").value = "";
                $I("txtActividad").value = "";
                //aG(0);
                ocultarProcesando();
                break;
            case "getDatosProy2":
                if (aResul[2] == "0")
                    $I("chkFacturable").checked = false;
                else
                    $I("chkFacturable").checked = true;
                ocultarProcesando();
                break;
            case "tecnicos":
                $I("divRelacion").children[0].innerHTML = aResul[2];
                $I("divRelacion").scrollTop = 0;
                scrollTablaProf();
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                //tratarTecnicosDeBaja();
                actualizarLupas("tblTitRec", "tblRelacion");
                ocultarProcesando();
                break;
            case "grabar":
                bCambios = false;
                setIconoBitacora();
                for (var i = 0; i < aRecursos.length; i++) aRecursos[i].opcionBD = "";
                var bProfBorrados = false;
                if (aPestGral[1].bModif == true) {
                    if (aPestProf[0].bModif == true) {
                        var aRecur = FilasDe("tblAsignados");
                        for (var i = aRecur.length - 1; i >= 0; i--) {
                            if (aRecur[i].getAttribute("bd") == "D") {
                                $I("tblAsignados").deleteRow(i);
                                bProfBorrados = true;
                            } else {
                                mfa(aRecur[i], "N");
                            }
                        }
                    }
                    if (aPestProf[1].bModif == true) {
                        var aRecur = FilasDe("tblPoolGF");
                        for (var i = aRecur.length - 1; i >= 0; i--) {
                            if (aRecur[i].getAttribute("bd") == "D") {
                                $I("tblPoolGF").deleteRow(i);
                            } else {
                                mfa(aRecur[i], "N");
                            }
                        }
                    }
                    if (bProfBorrados) scrollTablaProfAsig();
                }
                if (aPestGral[2].bModif) {
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

                $I("txtFecModif").value = aResul[2];
                $I("txtIdUsuModif").value = aResul[3];
                $I("txtDesUsuModif").value = aResul[4];
                if (bConsumosDeRecursos) {
                    mmoff("War", "No se han podido borrar algunos recursos de la tarea por tener consumos realizados.", 400);
                    bConsumosDeRecursos = false;
                }

                if (aResul[5] != "") {
                    //Error al comunicar apertura/cierre tarea
                    var reg = /\\n/g;
                    mmoff("WarPer", aResul[5].replace(reg, "\n"), 400);
                }
                if (aResul[6] != "") {
                    $I("txtIdTarea").value = aResul[6].ToString("N", 9, 0);
                    $I('hdnIdTarea').value = aResul[6];
                }
                if (aResul[7] == "T") {//hay que recargar la estructura
                    $I("hdnRecargar").value = "T";
                }

                if (aResul[8] != "" || aResul[9] != "") {
                    $I("txtPREst").value = aResul[8];
                    $I("txtPRFin").value = aResul[9];
                }
                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);
                setOp($I("btnPDF"), 100);
                setOp($I("btnDuplicar"), 100);
                //Sino hay consumos habilito el botón de borrado
                if (($I("txtConHor").value == "") || ($I("txtConHor").value == "0") || ($I("txtConHor").value == "0,00"))
                    setOp($I("btnBorrar"), 100);
                else
                    setOp($I("btnBorrar"), 30);

                sEstadoInicial = $I("cboEstado").value;

                //if (bClickMostrarBajas) {bClickMostrarBajas = false;}

                //Pongo las variables de pestaña modificada a false
                reIniciarPestanas();

                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);

                if (bSalir) salir();
                else{
                    if (bDuplicar) {
                        bDuplicar = false;
                        setTimeout("llamarCargarTarea();", 50);
                    }
                    else {
                        if (bExportar) {
                            bExportar = false;
                            setTimeout("LlamarExportar1();", 50);
                        }
                        else {
                            if (bLimpiar) {
                                bLimpiar = false;
                                setTimeout("LlamarLimpiar();", 50);
                            }
                            else {
                                if (bGetRecurso) {
                                    bGetRecurso = false;
                                    setTimeout("getRecursos2();", 50);
                                }
                                else {
                                    if (bBitacora) {
                                        bBitacora = false;
                                        setTimeout("LLamarMostrarBitacora1();", 50);
                                    }
                                }
                            }
                        }
                    }
                }

                break;
            case "tarearecurso":
                eval(aResul[3]);
                ocultarProcesando();
                mostrarDatosAsignacion(aResul[2]);
                if (bDesasignar) setTimeout("desasignar();", 50);
                break;

            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                setEstadoBotonesDoc(aResul[3], aResul[4]);
                ocultarProcesando();
                nfs = 0;
                break;
            case "aept":
                ponerPST(aResul[3]);
                ocultarProcesando();
                break;
            case "aept2":
                $I("divAECR").children[0].innerHTML = aResul[4];
                $I("divAEPT").children[0].innerHTML = aResul[2];
                eval(aResul[5]);
                if (!comprobarAEObligatorios()) {
                    //                    bCambios=false;
                    //                    window.close();
                    //                    return;
                }
                eliminarAERepetidos();
                ponerPST(aResul[3]);
                setTimeout("getDatosProy2();", 100);
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
            case "tarifas":
                var aTarifas = aResul[2].split("@#@");
                var aTarifa, codTarifa, desTarifa;
                $I("cboTarifa").length = 0;
                for (var i = 0; i < aTarifas.length; i++) {
                    aTarifa = aTarifas[i].split("##");
                    codTarifa = aTarifa[0];
                    desTarifa = aTarifa[1];
                    var op = new Option(desTarifa, codTarifa);
                    $I("cboTarifa").options[i] = op;
                }
            case "borrar":
                ocultarProcesando();
                if ($I("hdnEstr").value == "S") {
                    var returnValue = "borrar@#@";
                    //setTimeout("window.close();", 250); //para que de tiempo a grabar y actualizar "bCambios";
                    modalDialog.Close(window, returnValue);
                }
                else {
                    limpiar();
                }
                break;
            case "getRecursos":
                $I("divAsignados").children[0].innerHTML = aResul[2];
                $I("divAsignados").scrollTop = 0;
                borrarDatosAsignacion();
                scrollTablaProfAsig();
                actualizarLupas("tblTitRecAsig", "tblAsignados");
                ocultarProcesando();
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
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[3] == "") {
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto no existe o está fuera de su ámbito de visión.", 360); ;
                    break;
                }
                $I("txtNumPE").value = aResul[3].ToString("N", 9, 0);
                $I("txtPE").value = aResul[4];
                $I("hdnCRActual").value = aResul[5];
                $I("hdnDesCRActual").value = aResul[6];
                if (aResul[8] == "T") {//AdmiteRecursosPST
                    $I("chkHeredaCR").disabled = false;
                    if ($I("imgDelBajaPE") != null) $I("imgDelBajaPE").style.visibility = "visible";
                    $I("lblInsPST").style.visibility = "hidden";
                }
                else {
                    $I("chkHeredaCR").disabled = true;
                    if ($I("imgDelBajaPE") != null) $I("imgDelBajaPE").style.visibility = "hidden";
                    $I("lblInsPST").style.visibility = "visible";
                }
                sPSNAux = aResul[2];
                setTimeout("getModosFacturacion()", 20);
                break;
            case "getMF":
                //alert(aResul[2]);
                setTimeout("getDatosProy(" + sPSNAux + ");", 20);
                var aDatos = aResul[2].split("///");
                $I("cboModoFacturacion").length = 0;
                var opcion = new Option("", "0");
                $I("cboModoFacturacion").options[0] = opcion;
                
                var j = 1;                
                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboModoFacturacion").options[j] = opcion;
                    j++;
                }
                break;
            case "getValPrevision":

                $I("txtPLIni").value = aResul[2];
                $I("txtPLFin").value = aResul[3];
                $I("txtPLEst").value = aResul[4];
                $I("txtPRFin").value = aResul[5];
                $I("txtPREst").value = aResul[6];
                $I("txtUltCon").value = aResul[7];
                $I("txtConHor").value = aResul[8];

                if (parseFloat($I("txtConHor").value) > parseFloat($I("txtPREst").value))
                    $I("txtConHor").className = "txtNumMR";
                else
                    $I("txtConHor").className = "txtNumM";

                if (parseFloat($I("txtTotEst").value) > parseFloat($I("txtPREst").value)) {
                    $I("txtPREst").className = "txtNumMR";
                    $I("txtTotEst").className = "txtNumMR";
                }
                else {
                    $I("txtPREst").className = "txtNumM";
                    $I("txtTotEst").className = "txtNumM";
                }

                controlarFinalizada($I("cboEstado").value);

                //Si cambio el estado y no tengo leida la pestaña Avanzado debo leerla para obtener los datos
                //que se enviarán en la notificación de correo
                if (!aPestGral[2].bLeido) {
                    setTimeout("getDatos(2);", 500);
                }
                break;
            case "cargarCamposPorAmbito":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
    }
}

var sAmb = "";
function seleccionAmbito(strRblist) {
    try {
        if ($I("txtNumPE").value == "") {
            mmoff("Inf", "Debes indicar proyecto económico", 230);
            return;
        }
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else {
            //acción a realizar
            $I("divRelacion").children[0].innerHTML = "";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambAp").style.display = "none";
            $I("txtGF").value = "";

            switch (sOp) {
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    $I("txtCR").value = $I("hdnDesCRActual").value;
                    mostrarRelacionTecnicos("C", $I("hdnCRActual").value);
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
                case "P":
                    mostrarRelacionTecnicos("P", $I("hdnIdTarea").value);
                    break;
            }
            sAmb = sOp;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function borrar() {
    try {
        if (getOp($I("btnBorrar")) != 100) return;

        if ($I("hdnIdTarea").value == "") return;
        jqConfirm("", "Si pulsas 'Aceptar' se eliminará la tarea actual. <br><br>¿Deseas hacerlo?", "", "", "war", 400).then(function (answer) {
            if (answer) 
                LlamarBorrar();
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la tarea-1", e.message);
    }
}
function LlamarBorrar() {
    try {
        bCambios = false;
        //Si le hemos llamado desde la estructura hay que borrar esa línea
        $I("hdnRecargar").value = "T";
        var js_args = "borrar@#@";
        js_args += $I("hdnIdTarea").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la tarea-2", e.message);
    }
}
function actualizarFFPR() {
    if ($I("txtPRFin").value == "") {
        $I("txtPRFin").value = $I("txtPLFin").value;
    }
}
function actualizarEFPR() {
    if ($I("txtPREst").value == "") {
        $I("txtPREst").value = $I("txtPLEst").value;
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
function grabar() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        if (getOp($I("btnGrabar")) != 100) return;
        //Si estoy en modo Busqueda debo tener codigo de Tarea
        if (gsModo == "B") {
            if ($I("txtIdTarea").value == "" || $I("txtIdTarea").value == "0" || $I("txtIdTarea").value == "-1") {
                mmoff("Inf", "Debes seleccionar una tarea", 210);
                return;
            }
        }

        if (!comprobarDatos()) return;

        //        if ($I("txtPREst").value != 0){
        //            var bMuchoEsfuerzo=muchoEsfuerzo();
        //            if (bMuchoEsfuerzo){
        //                if (confirm("La suma de los esfuerzos estimados para los recursos excede\ndel esfuerzo estimado para la tarea.\n\n¿Deseas continuar?"))
        //                    bSeguir=true;
        //                else
        //                    bSeguir=false;
        //            }
        //            if (!bSeguir){
        //                bSalir=false;
        //                return;
        //            }
        //        }
        grabarTarea(false);
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la tarea", e.message);
    }
}
function grabarTarea(bDuplicar) {
    var vEtpl, vEtpr, sCodTarea;
    var bCambioEstado = false, bSeguir = true;
    try {
        if (bDuplicar)
            sCodTarea = "";
        else
            sCodTarea = $I("hdnIdTarea").value;
        var js_args = "grabar@#@" + sCodTarea + "##" + $I("hdnCRActual").value + "##" + $I("hdnT305IdProy").value +
                      "##" + $I("hdnIDPT").value + "##" + $I("hdnOrden").value + "##";
        if (bDuplicar)
            js_args += $I("txtIdTarea").value; // codigo de tarea actual para duplicación de recursos
        if ($I("chkNotificable").checked) js_args += "##1"; //Notificar por correo a origen 
        else js_args += "##0";
        if ($I("chkNotifProf").checked) js_args += "##1"; //Notificar por correo a profesionales asociados
        else js_args += "##0"; // 1
        js_args += "@#@";
        js_args += grabarP0(bDuplicar); //datos generales 2
        js_args += "@#@";
        js_args += grabarP1(bDuplicar); //profesionales 3,4,5
        js_args += "@#@";
        js_args += grabarP2(bDuplicar); //atributos estadísticos(avanzado) 6,7

        js_args += "@#@";
        js_args += grabarP3(bDuplicar); //notas 8
        js_args += "@#@";
        js_args += grabarP4(bDuplicar); //control 9
        js_args += "@#@";
        if (!bDuplicar) js_args += grabarNotificacion(); //apertura y cierre de tareas 10,11
        else js_args += "@#@";

        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesTarea").value;
        sFIPL = $I("txtPLIni").value;
        sFFPL = $I("txtPLFin").value;
        sETPL = $I("txtPLEst").value;
        if (sETPL == "") sETPL = "0";
        sPresupuesto = $I("txtPresupuesto").value;
        sEstado = $I("cboEstado").value;
        sFIV = $I("txtValIni").value;
        sFFV = $I("txtValFin").value;
        if ($I("chkFacturable").checked) sFacturable = "1";
        else sFacturable = "0";
        //nuevas variables a devolver al gantt
        sETPR = $I("txtPREst").value;
        if (sETPR == "") sETPR = "0";
        sFFPR = $I("txtPRFin").value;
        //if (sFFPR=="")sFFPR="0";
        sAvanR = $I("txtAvanReal").value;
        if (sAvanR == "") sAvanR = "0";
        if ($I("chkAvanceAuto").checked) sAutomatico = "1";
        else sAutomatico = "0";

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la tarea", e.message);
    }
}
function grabarP0(bDuplicar) {
    var js_args = "";
    var bCambioPadre = false;
    if (aPestGral[0].bModif || aPestProf[0].bModif || bDuplicar) {
        if (bDuplicar) js_args += Utilidades.escape("Copia de " + $I("txtDesTarea").value) + "##"; //0
        else js_args += Utilidades.escape($I("txtDesTarea").value) + "##"; //0
        js_args += Utilidades.escape($I("txtDescripcion").value) + "##"; //1
        if ($I("chkFacturable").checked) js_args += "1##"; //2
        else js_args += "0##";
        js_args += $I("txtValIni").value + "##"; //3
        js_args += $I("txtValFin").value + "##"; //4
        if (bDuplicar) js_args += "1##"; //5 cboEstado Grabamos la tarea como activa
        else js_args += $I("cboEstado").value + "##"; //5
        js_args += $I("txtCLE").value + "##"; //6
        js_args += getCLE() + "##"; //7
        js_args += $I("txtPLIni").value + "##"; //8
        js_args += $I("txtPLFin").value + "##"; //9
        vEtpl = fts(dfn($I("txtPLEst").value));
        js_args += vEtpl + "##"; //10
        js_args += $I("txtPRFin").value + "##"; //11
        vEtpr = fts(dfn($I("txtPREst").value));
        js_args += vEtpr + "##"; //12
        vPresu = fts(dfn($I("txtPresupuesto").value));
        js_args += vPresu + "##"; //13
        if (($I("hdnIDAct").value == "") || ($I("hdnIDAct").value == "0")) js_args += "-1##"; //14
        else js_args += $I("hdnIDAct").value + "##";
        //Si el avance es automatico el avance real se grabará como null
        if ($I("chkAvanceAuto").checked) {
            js_args += "##"; //15
            js_args += "1##"; //16
        }
        else {
            js_args += $I("txtAvanReal").value + "##"; //15
            js_args += "0##"; //16
        }
        js_args += $I("txtNumPE").value.replace(".", "") + "##"; //17
        js_args += nCR + "##"; //18

        if (bDuplicar) js_args += "0##0##"; //19 y 20
        else {
            if (!bNotificarApertura && $I("chkNotificable").checked) js_args += "1##"; //19 Comunicar apertura tarea
            else js_args += "0##";

            if (!bNotificarCierre && ($I("cboEstado").value == "3" || ($I("cboEstado").value == "4") || ($I("cboEstado").value == "5")))
                js_args += "1##"; //20 Comunicar cierre tarea
            else
                js_args += "0##";
        }
        js_args += $I("hdnIDFase").value + "##"; //21
        //Compruebo si hay cambios en los valores de inicio para Proyecto Económico, Proyecto Técnico, Fase y Actividad
        if (gsIdPE != $I("txtNumPE").value) bCambioPadre = true;
        if (gsIdPT != $I("hdnIDPT").value) bCambioPadre = true;
        if (gsIdFase != $I("hdnIDFase").value) bCambioPadre = true;
        if (gsIdActividad != $I("hdnIDAct").value) bCambioPadre = true;
        if (bDuplicar) {
            js_args += "T##"; //22
            $I("hdnRecargar").value = "T";
        }
        else {
            if (bCambioPadre) {
                js_args += "T##"; //22
                $I("hdnRecargar").value = "T";
            }
            else js_args += "F##"; //22
        }
        if (bDuplicar) js_args += "0##1##1##";
        else {
            if (sEstadoInicial != $I("cboEstado").value && $I("chkNotificable").checked) {
                js_args += "1##"; //23 Comunicar cambio de estado
                bCambioEstado = true;
            }
            else js_args += "0##"; //23
            js_args += sEstadoInicial + "##"//24
            js_args += $I("cboEstado").value + "##" //25
        }

        if ($I("chkImpIAP").checked) js_args += "1##"; //26
        else js_args += "0##";
        //Hereda los recursos del CR
        if ($I("chkHeredaCR").checked) js_args += "1##"; //27
        else js_args += "0##";
        //Hereda los recursos del PE
        if ($I("chkHeredaPE").checked) js_args += "1##"; //28
        else js_args += "0##";
        //Acceso a bitacora desde IAP
        if ($I("cboAccesoIAP").value == "")
            $I("cboAccesoIAP").value = "X";
        js_args += $I("cboAccesoIAP").value + "##"; //29
        if ($I("cboModoFacturacion").value == "0") $I("cboModoFacturacion").value = "";
        js_args += $I("cboModoFacturacion").value; //30
        js_args += "##";
        //Horas complementarias (para contratos a tiempo parcial)
        if ($I("chkHorasComplementarias").checked) js_args += "1"; //31
        else js_args += "0";

    }
    return js_args;
}
function grabarP1(bDuplicar) {
    var js_args = "", sAux;
    if (aPestGral[1].bModif || bDuplicar) {
        if (aPestProf[0].bModif) {
            //Control de recursos asociados a la tarea//
            var sw = 0;
            //for (var i = 0; i < aRecursos.length; i++){
            var aFila = FilasDe("tblAsignados");
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("bd") != "") {
                    sw = 1;
                    sAux = Utilidades.escape($I("txtDesTarea").value) + "##"; //10
                    sAux += $I("txtNumPE").value + "##"; //11
                    sAux += Utilidades.escape($I("txtPE").value) + "##"; //12
                    sAux += Utilidades.escape($I("txtPT").value) + "##"; //13
                    sAux += Utilidades.escape($I("txtFase").value) + "##"; //14
                    sAux += Utilidades.escape($I("txtActividad").value) + "##"; //15
                    sAux += Utilidades.escape($I("txtCodPST").value) + "##"; //16
                    sAux += Utilidades.escape($I("txtDesPST").value) + "##"; //17
                    sAux += Utilidades.escape($I("txtOTL").value) + "##"; //18
                    sAux += Utilidades.escape($I("txtIncidencia").value) + "##"; //19 

                    var objRec = buscarRecursoEnArray(aFila[i].id);
                    if (objRec == null) {
                        js_args += aFila[i].getAttribute("bd") + "##"; //0 
                        js_args += "##"; //1
                        js_args += aFila[i].id + "##"; //2 
                        js_args += "##"; //3
                        js_args += "##"; //4
                        js_args += "##"; //5
                        js_args += "1##"; //6 estado
                        js_args += "##"; //7
                        //js_args += Utilidades.escape(aFila[i].cells[2].innerText) + "##"; //8 nombre
                        js_args += Utilidades.escape(getCelda(aFila[i], 2)) + "##"; //8 nombre
                        js_args += "##"; //9

                        js_args += sAux + "///"; //20
                    }
                    else {
                        //if (aRecursos[i].opcionBD != ""){
                        //js_args +=objRec.opcionBD +"##";
                        js_args += aFila[i].getAttribute("bd") + "##"; //0 
                        js_args += objRec.idTarea + "##"; //1
                        js_args += objRec.idRecurso + "##"; //2 
                        js_args += objRec.etp + "##"; //3
                        js_args += objRec.ffp + "##"; //4
                        //Si la tarifa es cero quiere decir que no tiene tarifa por lo que pasamos cadena vacía
                        //para que en base de datos lo grabe como NULL
                        if (objRec.idTarifa == "0") js_args += "##"; //5
                        else js_args += objRec.idTarifa + "##"; //5
                        js_args += objRec.estado + "##"; //6
                        js_args += objRec.indicaciones + "##"; //7
                        js_args += objRec.nombre + "##"; //8
                        js_args += objRec.fip + "##"; //9

                        js_args += sAux + objRec.bNotifExceso + "///"; //20
                    }
                }
            }
            if (sw == 1) js_args = js_args.substring(0, js_args.length - 3);
        }
        js_args += "@#@";
        if (aPestProf[1].bModif) {
            //Control de los Grupos Funcionales asociados al pool de la tarea
            var aPoolGF = FilasDe("tblPoolGF");
            for (var i = 0; i < aPoolGF.length; i++) {
                if (aPoolGF[i].getAttribute("bd") != "N") {
                    js_args += aPoolGF[i].getAttribute("bd") + "##" + aPoolGF[i].id + "///";
                }
            }
        }
        js_args += "@#@"; //Para el mensaje generico a todos los profesionales asignados y activos en la tarea
        if (bEnvMensGral) {
            js_args += "T##"; //0
            js_args += Utilidades.escape($I("txtIndGen").value) + "##"; //1
            js_args += $I("txtIdTarea").value + "##"; //2
            js_args += Utilidades.escape($I("txtDesTarea").value) + "##"; //3
            js_args += $I("txtNumPE").value + "##"; //4
            js_args += Utilidades.escape($I("txtPE").value) + "##"; //5
            js_args += Utilidades.escape($I("txtPT").value) + "##"; //6
            js_args += Utilidades.escape($I("txtFase").value) + "##"; //7
            js_args += Utilidades.escape($I("txtActividad").value) + "##"; //8
        }
        else js_args += "F##################";
    }
    else {//No se ha tocado nada en la pestaña o estamos duplicando tarea en cuyo caso no se pasan profesionales
        // y los grupos funcionales se asociarán en codigo de servidor
        js_args += "@#@@#@";
    }
    return js_args;
}
function grabarP2(bDuplicar) {
    var js_args = "";
    if (aPestGral[2].bModif || bDuplicar)
    {
        if (aPestAvanza[0].bModif) {
            if (bDuplicar) {
                js_args += "##0######";
            }
            else {
                js_args += $I("txtIdPST").value + "##"; //0
                js_args += $I("cboOrigen").value + "##"; //1
                js_args += Utilidades.escape($I("txtOTL").value) + "##"; //2
                js_args += Utilidades.escape($I("txtIncidencia").value) + "##"; //3
            }
            js_args += "@#@";
            //Control de atributos estadísticos//
            if (!bDuplicar) {
                var sw = 0;
                var aFila = FilasDe("tblAET");
                for (var i = 0; i < aFila.length; i++) {
                    if (aFila[i].getAttribute("bd") != "") {
                        sw = 1;
                        js_args += aFila[i].getAttribute("bd") + "##";
                        js_args += $I("hdnIdTarea").value + "##"; //nºtarea
                        js_args += aFila[i].id + "##";
                        js_args += aFila[i].getAttribute("vae") + "///";
                    }
                }
                if (sw == 1) js_args = js_args.substring(0, js_args.length - 3);
            }
        }

        if (!aPestAvanza[0].bModif) js_args += "@#@";

        js_args += "@#@";
        if (aPestAvanza[1].bModif) {
            //Control de los campos asociados a la tarea
            var aCampos = FilasDe("tblCampos");
            for (var i = 0; i < aCampos.length; i++) {
                if (aCampos[i].getAttribute("bd") != "") {
                    js_args += aCampos[i].getAttribute("bd") + "##" + aCampos[i].id + "##" + aCampos[i].cells[3].children[0].value + "##";
                    js_args += aCampos[i].getAttribute("tipodato");
                    if (aCampos[i].getAttribute("tipodato") == "H") if (aCampos[i].getAttribute("tipodato") == "H") js_args += "##" + aCampos[i].cells[4].children[0].value + "##" + aCampos[i].cells[5].children[0].value + "##" + aCampos[i].cells[6].children[0].value + "##";
                    js_args += "///";
                }
            }
        }
    }
    else
    //js_args += "##0######@#@";
        js_args += "@#@@#@";
    return js_args;
}
function grabarP3(bDuplicar) {
    var js_args = "";
    if (aPestGral[3].bModif || bDuplicar) {
        if ($I("chkNotasIAP").checked) js_args += "1##"; //0
        else js_args += "0##";
        if (bDuplicar) js_args += "########"
        else {
            js_args += Utilidades.escape($I("txtNotas1").value) + "##"; //1
            js_args += Utilidades.escape($I("txtNotas2").value) + "##"; //2
            js_args += Utilidades.escape($I("txtNotas3").value) + "##"; //3
            js_args += Utilidades.escape($I("txtNotas4").value) + "##"; //4
        }
    }
    return js_args;
}
function grabarP4(bDuplicar) {
    var js_args = "";
    if (aPestGral[4].bModif) {
        js_args += "T##" + Utilidades.escape($I("txtObservaciones").value); // +"##"; //0
        //            if (bDuplicar)js_args += "####";
        //            else{
        //                js_args += $I("txtIdUsuAlta").value.replace(".","") +"##"; //1
        //                js_args += $I("txtFecAlta").value +"##"; //2
        //            }
    }
    return js_args;
}
function grabarNotificacion() {
    try {
        var js_args = "";
        if ($I("txtIdTarea").value == "") {
            //Notificación de apertura de tarea//
            if (!bNotificarApertura && $I("chkNotificable").checked) {
                js_args += $I("txtIdTarea").value + "##"; //0
                js_args += Utilidades.escape($I("txtDesTarea").value) + "##"; //1
                js_args += $I("txtNumPE").value + "##"; //2
                js_args += Utilidades.escape($I("txtPE").value) + "##"; //3
                js_args += Utilidades.escape($I("txtPT").value) + "##"; //4
                js_args += Utilidades.escape($I("txtFase").value) + "##"; //5
                js_args += Utilidades.escape($I("txtActividad").value) + "##"; //6

                var strMail = "";
                for (var i = 0; i < aOrigen_js.length; i++) {
                    if (aOrigen_js[i][0] == $I("cboOrigen").value) {
                        strMail = aOrigen_js[i][2];
                        break;
                    }
                }
                js_args += Utilidades.escape($I("txtCodPST").value) + "##"; //7
                js_args += Utilidades.escape($I("txtDesPST").value) + "##"; //8
                js_args += Utilidades.escape($I("txtOTL").value) + "##"; //9
                js_args += Utilidades.escape($I("txtIncidencia").value) + "##"; //10
                js_args += Utilidades.escape(strMail) + "##"; //11
            }
        }
        js_args += "@#@";
        //Notificación de cambio de estado de tarea//
        if (sEstadoInicial != $I("cboEstado").value && $I("chkNotificable").checked) {
            //Además envío correo a los RTPT´s de la tarea para que se enteren de que ha cambiado de estado 
            js_args += $I("txtIdTarea").value + "##"; //0
            js_args += Utilidades.escape($I("txtDesTarea").value) + "##"; //1
            js_args += $I("txtNumPE").value + "##"; //2
            js_args += Utilidades.escape($I("txtPE").value) + "##"; //3
            js_args += Utilidades.escape($I("txtPT").value) + "##"; //4
            js_args += Utilidades.escape($I("txtFase").value) + "##"; //5
            js_args += Utilidades.escape($I("txtActividad").value) + "##"; //6
            js_args += $I("txtPLEst").value + "##"; //7
            js_args += $I("txtPLIni").value + "##"; //8
            js_args += $I("txtPLFin").value + "##"; //9
            js_args += $I("txtPriCon").value + "##"; //10
            js_args += $I("txtPREst").value + "##"; //11
            js_args += $I("txtPRFin").value + "##"; //12
            js_args += $I("txtUltCon").value + "##"; //13
            js_args += $I("txtConHor").value + "##"; //14
            js_args += $I("txtAvanReal").value + "##"; //15
            js_args += $I("hdnIDPT").value + "##"; //16
            if ($I("chkNotificable").checked) js_args += "1##"; //17 ->Notificar al origen
            else js_args += "0##"; //17

            //Si hay que notificar al origen de la tarea.
            if ($I("chkNotificable").checked) {
                var strMail = "";
                for (var i = 0; i < aOrigen_js.length; i++) {
                    if (aOrigen_js[i][0] == $I("cboOrigen").value) {
                        strMail = aOrigen_js[i][2];
                        break;
                    }
                }
                js_args += Utilidades.escape($I("txtCodPST").value) + "##"; //18
                js_args += Utilidades.escape($I("txtDesPST").value) + "##"; //19
                js_args += Utilidades.escape($I("txtOTL").value) + "##"; //20
                js_args += Utilidades.escape($I("txtIncidencia").value) + "##"; //21
                js_args += Utilidades.escape(strMail) + "##"; //22
            }
        }
        return js_args;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la pestaña Avanzado del proyecto técnico", e.message);
    }
}

function duplicarGrabar() {
    var vEtpl, vEtpr;
    try {
        if ($I("hdnAcceso").value == "R") return;
        if (getOp($I("btnDuplicar")) != 100) return;
        if (!comprobarDatos()) return;
        jqConfirm("", "Si pulsas 'Aceptar' se generará una copia de la tarea actual.<br><br>¿Deseas hacerlo?", "", "", "war", 350).then(function (answer) {
            if (answer) llamarDuplicarGrabar();
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al duplicar los datos de la tarea-1", e.message);
    }
}
function llamarDuplicarGrabar() {
    try {
        bDuplicar = true; 
        grabarTarea(true);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al duplicar los datos de la tarea-2", e.message);
    }
}
function llamarCargarTarea() {
    try {
        //grabarTarea(true);
        //Cargo la nueva tarea
        setTimeout("cargarTarea()", 1000);
        $I("hdnRecargar").value = "T";
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al duplicar los datos de la tarea-3", e.message);
    }
}

function comprobarDatos() {
    try {
        if (($I("txtNumPE").value == "") || ($I("txtNumPE").value == "0") || ($I("txtNumPE").value == "-1")) {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar proyecto económico", 230);
            return false;
        }
        if (($I("hdnIDPT").value == "") || ($I("hdnIDPT").value == "0") || ($I("hdnIDPT").value == "-1")) {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar proyecto técnico", 210);
            return false;
        }
        //No puede haber tareas con fase y sin actividad
        if (($I("hdnIDFase").value != "") && ($I("hdnIDFase").value != "0")) {
            if (($I("hdnIDAct").value == "") || ($I("hdnIDAct").value == "0")) {
                tsPestanas.setSelectedIndex(0);
                mmoff("War", "PSP no permite a una tarea depender directamente de una fase", 390);
                return false;
            }
        }
        if ($I("txtDesTarea").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre de la tarea", 240);
            return false;
        }
        if ($I("txtValIni").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La fecha de inicio de vigencia no puede ser vacía", 240);
            return false;
        }
        //La fecha de fin de vigencia no puede ser anterior a la de inicio
        if (!fechasCongruentes($I("txtValIni").value, $I("txtValFin").value)) {
            tsPestanas.setSelectedIndex(0);
            $I("txtValFin").select();
            //alert("La fecha de fin de vigencia debe ser posterior a la de inicio");
            mmoff("War", "La fecha de inicio de vigencia: " + sFechaAct + "\ndebe ser anterior a la fecha de fin de vigencia: " + $I("txtValFin").value, 350);
            $I("txtValIni").focus();
            return false;
        }
        //La fecha de fin planificada no puede ser anterior a la de inicio
        if (!fechasCongruentes($I("txtPLIni").value, $I("txtPLFin").value)) {
            tsPestanas.setSelectedIndex(0);
            $I("txtPLFin").select();
            //alert("La fecha de fin planificada debe ser posterior a la de inicio");
            mmoff("infPer", "La fecha de inicio planificada: " + sFechaAct + "\ndebe ser anterior a la fecha de fin planificada: " + $I("txtPLFin").value, 380);
            $I("txtPLIni").focus();
            return false;
        }
        //28/02/2013: A petición de Víctor (mail 9:28h), se elimina este control.
        //        //La fecha de inicio planificada (si no es vacía) no puede ser anterior a la fecha de fin prevista
        //        if ($I("txtPLIni").value != ""){
        //            if (!fechasCongruentes($I("txtPLIni").value, $I("txtPRFin").value)){
        //                tsPestanas.setSelectedIndex(0);
        //                $I("txtPLIni").select();
        //                alert("La fecha de inicio planificada: " + $I("txtPLIni").value + " debe ser anterior a la fecha de fin prevista: " + $I("txtPRFin").value);
        //                return false;
        //            }
        //        }
        if ($I("txtAvanReal").value == "" && $I("chkAvanceAuto").checked == false) $I("txtAvanReal").value = "0";
        if ($I("chkAvanceAuto").checked == false) {
            //Si el avance es automatico no merece la pena controlar el valor del avance real pues se va a grbar en baase de datos como NULL
            if ($I("txtAvanReal").value != "") {
                if (isNaN(parseInt($I("txtAvanReal").value, 10))) {
                    tsPestanas.setSelectedIndex(0);
                    mmoff("War", "El avance real debe ser un valor numérico", 380);
                    $I("txtAvanReal").select();
                    return false;
                } else {
                    var nAR = parseFloat($I("txtAvanReal").value);
                    if (nAR < 0) {
                        $I("txtAvanReal").value = "0,00";
                    }
                    if (nAR > 100) {
                        $I("txtAvanReal").value = "100,00";
                    }
                }
            }
        }
        //Compruebo que el esfuerzo total planificado es <32.000
        if ($I("txtPLEst").value != "") {
            if (isNaN(parseInt($I("txtPLEst").value, 10))) {
                tsPestanas.setSelectedIndex(0);
                mmoff("War", "El esfuerzo total planificado debe ser un valor numérico", 380);
                $I("txtPLEst").select();
                return false;
            }
        }
        //Compruebo que el esfuerzo total previsto es <32.000
        if ($I("txtPREst").value != "") {
            if (isNaN(parseInt($I("txtPREst").value, 10))) {
                tsPestanas.setSelectedIndex(0);
                mmoff("War", "El esfuerzo total previsto debe ser un valor numérico", 380);
                $I("txtPREst").select();
                return false;
            }
        }
        //Validaciones de los datos de los recursos.
        if (aPestGral[1].bModif) {
            for (var i = 0; i < aRecursos.length; i++) {
                if (aRecursos[i].opcionBD == "D") {
                    //Comprobar que no tenga consumos, en caso contrario,
                    //no borrar y avisar al usuario.
                    var strParam = "nTarea=" + $I("hdnIdTarea").value;
                    strParam += "&nRecurso=" + aRecursos[i].idRecurso;

                    strUrl = document.location.toString();
                    intPos = strUrl.indexOf("Default.aspx");
                    strUrlPag = strUrl.substring(0, intPos) + "../obtenerDatos.aspx?nOpcion=1&" + strParam;
                    ret = Utilidades.unescape(sendHttp(strUrlPag));
                    var nPos = ret.indexOf("<html>");
                    if (nPos > -1) {
                        ret = ret.substring(0, nPos - 3); //-3 que son: un espacio, chr(10) y chr(13)
                    }
                    if (ret != "Error") {
                        if (ret == "1") { //Tiene consumos
                            bConsumosDeRecursos = true;
                            aRecursos[i].opcionBD = ""; //Se deja sin cambios para que no se intente borrar del servidor.
                            mostrarDeNuevoRecurso(aRecursos[i].idRecurso); //Se muestra
                        }
                    } else {
                        mmoff("War", "No se han podido comprobar la existencia de consumos", 360);
                    }
                }
                else {
                    //Comprobar que la fecha de fin prevista no sea anterior a la de inicio
                    if (!fechasCongruentes(aRecursos[i].fip, aRecursos[i].ffp)) {
                        tsPestanas.setSelectedIndex(1);
                        var aFilaAux = FilasDe("tblAsignados");
                        for (var x = 0; x < aFilaAux.length; x++) {
                            if (aFilaAux[x].id == aRecursos[i].idRecurso) {
                                ms(aFilaAux[x]);
                                break;
                            }
                        }
                        mmoff("War", "Profesional asignado: " + aRecursos[i].nombre + ".\nLa fecha de fin prevista no puede ser anterior a la de inicio.", 400);
                        //alert("Empleado: "+aRecursos[i].nombre+". La fecha de fin prevista no puede ser anterior a la de inicio.");
                        return false;
                    }
                    //Comprobar que la fecha de inicio prevista del profesional no sea anterior a la de inicio planificada
                    if ($I("txtPLIni").value != "" && !fechasCongruentes($I("txtPLIni").value, aRecursos[i].fip)) {
                        tsPestanas.setSelectedIndex(1);
                        var aFilaAux = FilasDe("tblAsignados");
                        for (var x = 0; x < aFilaAux.length; x++) {
                            if (aFilaAux[x].id == aRecursos[i].idRecurso) {
                                ms(aFilaAux[x]);
                                break;
                            }
                        }
                        mmoff("War", "Profesional asignado: " + aRecursos[i].nombre + ".\nLa fecha de inicio indicada al profesional no puede ser anterior a la de inicio planificada de la tarea.", 400);
                        return false;
                    }
                    //Comprobar que la fecha de fin prevista del profesional no sea anterior a la de inicio planificada
                    if ($I("txtPLIni").value != "" && !fechasCongruentes($I("txtPLIni").value, aRecursos[i].ffp)) {
                        tsPestanas.setSelectedIndex(1);
                        var aFilaAux = FilasDe("tblAsignados");
                        for (var x = 0; x < aFilaAux.length; x++) {
                            if (aFilaAux[x].id == aRecursos[i].idRecurso) {
                                ms(aFilaAux[x]);
                                break;
                            }
                        }
                        mmoff("War", "Profesional asignado: " + aRecursos[i].nombre + ".\nLa fecha de finalización indicada al profesional no puede ser anterior a la de inicio planificada de la tarea.", 400);
                        return false;
                    }
                }
            }
        }
        //Validaciones de los atributos estadísticos.
        if (aPestGral[2].bModif) {
            var aFila = FilasDe("tblAET");
            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].style.display == "none") continue;
                if (aFila[i].cells[3].innerText == "" && aFila[i].getAttribute("bd") != "D") {
                    tsPestanas.setSelectedIndex(2);
                    ms(aFila[i]);
                    mmoff("War", "Debes asignar un valor al atributo estadístico '" + aFila[i].cells[2].innerText + "'", 390);
                    return false;
                }
            }
            var aFilaPT = FilasDe("tblAEPT");
            for (var i = 0; i < aFilaPT.length; i++) {
                if (aFilaPT[i].cells[2].innerText == "") {
                    tsPestanas.setSelectedIndex(2);
                    tsPestanasAvanza.setSelectedIndex(0);
                    mmoff("War", "Debes asignar un valor al atributo estadístico '" + aFilaPT[i].cells[1].innerText + "'\ndesde la pantalla de detalle de Proyecto Técnico", 380);
                    return false;
                }
            }
            //var aCampos = FilasDe("tblCampos");
            //for (var i = 0; i < aCampos.length; i++) {
            //    if (aCampos[i].cells[3].children[0].value == "") {
            //        tsPestanas.setSelectedIndex(2);
            //        tsPestanasAvanza.setSelectedIndex(1);
            //        ms(aCampos[i]);
            //        mmoff("War", "Debe asignar un valor al campo '" + aCampos[i].cells[1].innerText, 380);
            //        return false;
            //    }
            //}
        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function eliminarRecurso(oFila) {
    try {
        var idRecurso = oFila.id;
        var strRecurso = oFila.cells[0].innerText;

        var objRec = buscarRecursoEnArray(idRecurso);
        if (objRec == null) {
            //alert("No se han podido obtener los datos del profesional '"+ strRecurso +"'");
            //return;
            bDesasignar = true;
            mostrarDatosAsignacion(idRecurso);
            return false;
        }
        if (oFila.getAttribute("bd") == "I")
            $I("tblAsignados").deleteRow(oFila.rowIndex);
        else
            mfa(oFila, "D");

        if (objRec.opcionBD != "I")
            objRec.opcionBD = "D";
        else
            borrarRecursoDeArray(idRecurso);

        borrarDatosAsignacion();
        aGProf(0);
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar al profesional.", e.message);
    }
}
function insertarRecurso(oFila) {
    try {
        var idRecurso = oFila.id;
        //1º buscar si existe en el array de recursos y su "opcionBD"
        var objRec = buscarRecursoEnArray(idRecurso);
        var bExiste = false;
        var aFila = FilasDe("tblAsignados");
        if (objRec == null) { //No existe en el array
            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].id == idRecurso) {
                    aFila[i].className = "FS";
                    //alert("El técnico indicado ya se encuentra asignado a la tarea");
                    bExiste = true;
                    break;
                }
            }
            if (bExiste) {
                mostrarDatosAsignacion(idRecurso);
                return;
            }
            else {
                insertarRecursoEnArray("I", $I("hdnIdTarea").value, idRecurso, oFila.innerText, "", "", "", "", "",
                                       oFila.getAttribute("idTarifa"), "1", "", "", "", "", "", "", "", "0");
                if (oFila.getAttribute("idTarifa") == "-1") $I("cboTarifa").value = "";
                else $I("cboTarifa").value = oFila.getAttribute("idTarifa");
            }
        } else {
            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].id == idRecurso) {
                    bExiste = true;
                    break;
                }
            }
        }
        if (bExiste) {
            //alert("El profesional indicado ya se encuentra asignado a la tarea");
            return;
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        sNombreNuevo = oFila.innerText;
        if (aFila != null) {
            for (var iFilaNueva = 0; iFilaNueva < aFila.length; iFilaNueva++) {
                //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                sNombreAct = aFila[iFilaNueva].innerText;
                if (sNombreAct > sNombreNuevo) break;
            }
        }
        //Para que detecte change al poner fechas
        $I("txtFIPRes").value = "";
        $I("txtFFPRes").value = "";
        $I("txtFIPRes").oValue = "";
        $I("txtFFPRes").oValue = "";

        var oNF = $I("tblAsignados").insertRow(iFilaNueva);
        oNF.id = idRecurso;
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.setAttribute("sw", 1);

        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.onclick = function() { mostrarDatosAsignacion(this.id) };

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNC2 = oNF.insertCell(-1);

        oNC2.appendChild(oFila.cells[0].children[0].cloneNode(true));
        //oNC2.children[0].style.cursor = "../../../images/imgManoAzul2.cur";
        oNC2.children[0].ondblclick = function() { setEstado(this); };
        oNC2.children[0].title = "Profesional activo en la tarea (le figura en su IAP)";

        oNC3 = oNF.insertCell(-1);
        oNC3.innerText = oFila.cells[1].innerText;
        oNF.children[1].children[0].style.cursor = strCurMA;    
        ms(oNF);
        actualizarLupas("tblTitRecAsig", "tblAsignados");
        aGProf(0);

        //$I("divAsignados").scrollTop = $I("tblAsignados").rows.length * 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
    }
}
function mostrarDeNuevoRecurso(idRecurso) {
    try {
        var objRec = buscarRecursoEnArray(idRecurso);
        if (objRec != null)
            objRec.opcionBD = "U";
//        var oNF = $I("tblAsignados").insertRow(-1);
//        oNF.style.height = "20px";
//        oNF.id = idRecurso;
//        oNF.attachEvent("onclick", mm);
//        oNF.onclick = function() { mostrarDatosAsignacion(this.id); };
//        oNF.ondblclick = function() { eliminarRecurso(this); };
//        oNC0 = oNF.insertCell(-1);
//        oNC1 = oNF.insertCell(-1);
//        oNC1.innerText = objRec.nombre;
//        oNC2 = oNF.insertCell(-1);
//        if (objRec.estado == "1")
//            oNC2.innerHTML = "<img src='../../../images/imgActivoTareaOn.gif' title='Profesional activo en la tarea (le figura en su IAP)' ondblclick='setEstado(this)' style='CURSOR: url(../../../images/imgManoAzul2.cur),pointer;'>";
//        else
//            oNC2.innerHTML = "<img src='../../../images/imgActivoTareaOff.gif' title='Profesional inactivo en la tarea (no le figura en su IAP)' ondblclick='setEstado(this)' style='CURSOR: url(../../../images/imgManoAzul2.cur),pointer;'>";
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idRecurso) {
                //aFila[i].className = "FS";
                mfa(aFila[i], "U");
                break;
            }
        }
        aGProf(0);
        //ms(oNF);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar de nuevo al profesional.", e.message);
    }
}
function mostrarDatosAsignacion(idRecurso) {
    try {
        var objRec = buscarRecursoEnArray(idRecurso);
        if (objRec == null) {

            var js_args = "tarearecurso@#@";
            js_args += $I("hdnIdTarea").value + "@#@";
            js_args += idRecurso;

            //alert(js_args);
            mostrarProcesando();
            RealizarCallBack(js_args, "");
            return;
        }
        //Compruebo que solo haya uno seleccionado
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id != idRecurso && aFila[i].className == "FS") {
                borrarDatosAsignacion();
                return;
            }
        }

        $I("txtETPRes").value = objRec.etp.ToString("N");
        $I("txtConTec").value = objRec.acumulado.ToString("N");
        $I("txtFIPRes").value = objRec.fip;
        $I("txtFFPRes").value = objRec.ffp;
        $I("txtIndicaciones").value = Utilidades.unescape(objRec.indicaciones);
        $I("txtETPTec").value = objRec.ete.ToString("N");
        $I("txtFFPTec").value = objRec.ffe;
        $I("txtComentarios").value = Utilidades.unescape(objRec.comentario);

        if (objRec.bNotifExceso == 1) $I("chkNotifExceso").checked = true;
        else $I("chkNotifExceso").checked = false;
        $I("txtEP").value = objRec.pendiente;
        $I("cboTarifa").value = objRec.idTarifa;
        if (objRec.completado == 1) {
            //$I("chkCompletado").checked = true;
            $I("lblCompletado").innerText = "Finalización comunicada en IAP";
            $I("lblCompletado").style.color = "DimGray";
        }
        else {
            //$I("chkCompletado").checked = false;
            $I("lblCompletado").innerText = "Sin comunicar finalización en IAP";
            $I("lblCompletado").style.color = "Green";
        }
        if (objRec.sPriCons == null) $I("txtPriCons").value = "";
        else $I("txtPriCons").value = objRec.sPriCons;
        if (objRec.sUltCons == null) $I("txtUltCons").value = "";
        else $I("txtUltCons").value = objRec.sUltCons;

        setEstadoDatosAsignacion(true);
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de la asignación.", e.message);
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
function borrarDatosAsignacion() {
    try {
        $I("txtETPRes").value = "";
        $I("txtConTec").value = "";
        $I("txtFIPRes").value = "";
        $I("txtFIPRes").oValue = "";
        $I("txtFFPRes").value = "";
        $I("txtFFPRes").oValue = "";
        $I("txtIndicaciones").value = "";
        $I("txtETPTec").value = "";
        $I("txtFFPTec").value = "";
        $I("txtComentarios").value = "";
        $I("txtEP").value = "";
        $I("cboTarifa").value = "";
        $I("lblCompletado").value = "";
        $I("txtPriCons").value = "";
        $I("txtUltCons").value = "";
        setEstadoDatosAsignacion(false);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los datos de la asignación.", e.message);
    }
}

function obtenerCR() {
    try {
        var aOpciones;
        if ($I("txtNumPE").value == "") {
            mmoff("Inf", "Debes indicar proyecto económico", 230);
            return;
        }
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
                mostrarRelacionTecnicos("C", aOpciones[0]);
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
        if ($I("txtNumPE").value == "") {
            mmoff("Inf", "Debes indicar proyecto económico", 230);
            return;
        }
        if ($I("hdnAcceso").value == "R") return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnCRActual").value;
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
            if (ret != null) {
                //alert(ret);
                aOpciones = ret.split("@#@");
                $I("txtGF").value = aOpciones[1];
                mostrarRelacionTecnicos("G", aOpciones[0]);
            }
        });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}

function mostrarRelacionTecnicos(sOpcion, sValor) {
    var sCodUne, sNumPE;
    try {
        if ($I("txtNumPE").value == "") {
            mmoff("Inf", "Debes indicar proyecto económico", 230);
            return;
        }
        if (sOpcion == "C") sCodUne = sValor;
        else sCodUne = $I("hdnCRActual").value;
        if ($I("txtNumPE").value != "") sNumPE = dfn($I("txtNumPE").value);
        else sNumPE = "";

        var sValor1 = "";
        var sValor2 = "";
        var sValor3 = "";
        if (sOpcion == "N") {
            sValor1 = Utilidades.escape($I("txtApellido1").value);
            sValor2 = Utilidades.escape($I("txtApellido2").value);
            sValor3 = Utilidades.escape($I("txtNombre").value);
            if (sValor1 == "" && sValor2 == "" && sValor3 == "") {
                mmoff("Inf", "Debes indicar algún criterio para la búsqueda por apellidos/nombre", 410);
                return;
            }
        } else {
            sValor1 = sValor;
        }
        var js_args = "tecnicos@#@" + sOpcion + "@#@" + sValor1 + "@#@" + sValor2 + "@#@" + sValor3 + "@#@" + sCodUne + "@#@"
                      + $I("hdnT305IdProy").value + "@#@" + $I("txtCualidad").value + "@#@" + $I("txtIdTarea").value.replace('.', '');

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}

function asignar() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        var aFila = FilasDe("tblRelacion");
        if (aFila.length == 0) return;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                insertarRecurso(aFila[i]);
            }
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al asignar a un profesional", e.message);
    }
}
var bDesasignar = false;
function desasignar() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        var aFila = FilasDe("tblAsignados");
        if (aFila.length == 0) return;
        var sw = 0;
        //for (var i=0; i<aFila.length; i++){
        for (var i = aFila.length - 1; i >= 0; i--) {
            if (aFila[i].className == "FS") {
                var bResul = eliminarRecurso(aFila[i]);
                if (!bResul) {
                    sw = 1;
                    break;
                }
            }
        }
        if (sw == 0) {
            bDesasignar = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desasignar a un profesional", e.message);
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

function asociarAEPT(oFila, bMsg) {
    try {
        oNF = $I("tblAEPT").insertRow(-1);
        oNF.id = oFila.id;
        oNF.setAttribute("style", "height:16px")
        oNF.setAttribute("vae", "");
        oNF.setAttribute("obl", oFila.getAttribute("obl"));
        oNF.setAttribute("bd", "I");

        var iFila = oNF.rowIndex;
        if ($I("hdnAcceso").value != "R") {
            oNC1 = oNF.insertCell(-1);
            if (oNF.getAttribute("obl") == "1")
                oNC1.innerHTML = "<img src='../../../images/imgIconoObl.gif' title='Obligatorio'>";
            else
                oNC1.innerHTML = "<img src='../../../images/imgSeparador.gif'>";

            oNC2 = oNF.insertCell(-1);
            oNC2.innerText = oFila.innerText;

            oNF.insertCell(-1);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al asociar el atributo estadístico al proyecto técnico", e.message);
    }
}

function asociarAE(oFila, bMsg) {
    try {
        //1º Mirar si el AE seleccionado está en la tabla tblAET (visible u oculto)
        //if ($I("hdnAcceso").value=="R")return;
        var aFila = FilasDe("tblAET");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].id == oFila.id) { //Si existe
                sw = 1;
                if (aFila[i].getAttribute("bd") == "D") { //Si está pdte de borrar
                    mfa(aFila[i], "U");
                } else {
                    if (bMsg) mmoff("Inf", "El atributo estadístico seleccionado ya está asociado a la tarea.", 390);
                    break;
                }
            }
        }
        //2º Mirar si el AE seleccionado está en la tabla tblAEPT, es decir es un AE heredado del PT
        if (sw == 0) {
            var aFila2 = FilasDe("tblAEPT");
            var sw = 0;
            for (var i = 0; i < aFila2.length; i++) {
                if (aFila2[i].id == oFila.id) { //Si existe
                    sw = 1;
                    if (bMsg) mmoff("Inf", "El atributo estadístico seleccionado se hereda del proyecto técnico.", 400);
                    break;
                }
            }
        }
        if (sw == 0) {
            oNF = $I("tblAET").insertRow(-1);
            oNF.id = oFila.id;
            oNF.setAttribute("style", "height:16px")
            oNF.setAttribute("vae", "");
            oNF.setAttribute("obl", oFila.getAttribute("obl"));
            oNF.setAttribute("bd", "I");

            var iFila = oNF.rowIndex;
            if ($I("hdnAcceso").value != "R") {

                oNF.attachEvent("onclick", mm);
                oNF.attachEvent("onmousedown", DD);

                oNF.onclick = function() { mostrarValoresAE(this); };

                oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

                oNC2 = oNF.insertCell(-1);
                if (oNF.getAttribute("obl") == "1")
                    oNC2.innerHTML = "<img src='../../../images/imgIconoObl.gif' title='Obligatorio'>";
                else
                    oNC2.innerHTML = "<img src='../../../images/imgSeparador.gif'>";

                oNF.insertCell(-1).innerText = oFila.innerText;

                var oNOBR = document.createElement("NOBR");
                oNOBR.className = "NBR W90";

                oNF.insertCell(-1).appendChild(oNOBR);
            }
            if (($I("txtIdTarea").value == "") || ($I("txtIdTarea").value == "0")) {
                //Para no activar grabar cuando estamos en modo busqueda
            }
            else aGAvanza(0);
            //ms(oNF);
            mostrarValoresAE(oNF);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al asociar el atributo estadístico a la tarea", e.message);
    }
}

var nFilaAESel = -1;
//function mostrarValoresAE(oFila) {
//    try {
//        //if ($I("hdnAcceso").value=="R")return;
//        nFilaAESel = oFila.rowIndex;
//        //alert("mostrar valores del AE cuyo id: "+ oFila.id);
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
        //2º Insertar los valores del AE asociado.
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
        //if ($I("hdnAcceso").value=="R")return;
        if (nFilaAESel == -1) return;
        var oFilaAET = $I("tblAET").rows[nFilaAESel];
        oFilaAET.setAttribute("vae", oFila.id);
        //oFilaAET.cells[3].innerText = oFila.innerText;
        oFilaAET.cells[3].children[0].innerHTML = oFila.cells[0].innerText;
        if (oFilaAET.getAttribute("bd") != "I") oFilaAET.setAttribute("bd", "U");

        //Compruebo si el valor del combo de estado es compatible con la situación de los AE obligatorios
        verificarEstado();

        aGAvanza(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar valor a un atributo estadístico", e.message);
    }
}

function eliminarAERepetidos() {
    try {
        //si un AE está en la lista de heredados del PT y en la lista de tareas lo borro del de tareas
        if ($I("hdnIDPT").value == "") return;
        var aAeT = FilasDe("tblAET");
        if (aAeT == null) return;
        var aAePt = FilasDe("tblAEPT");
        for (var i = aAeT.length - 1; i >= 0; i--) {
            if (estaAEenPT2(aAeT[i].id, aAePt)) {
                $I("tblAET").deleteRow(i);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los atributos estadísticos obligatorios", e.message);
    }
}
function comprobarAEObligatorios() {
    try {
        var res = true;
        //si son obligatorios en el CR y no están en los AE del PT los añado
        if ($I("hdnIDPT").value == "") return;
        var aFila = FilasDe("tblAECR");
        //var aAePt = FilasDe("tblAEPT");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("obl") == "1") {
                if (!estaAEenPT(aFila[i].id)) {
                    //alert("Los atributos estadísticos obligatorios deben tener\nvalor asignado a nivel de Proyecto técnico");
                    //asociarAEPT(aFila[i], false);
                    asociarAE(aFila[i], false);
                    res = false;
                    //break;
                }
            }
        }
        return res;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los atributos estadísticos obligatorios", e.message);
    }
}
function estaAEenPT(idAE) {
    try {
        var aAePt = FilasDe("tblAEPT");
        for (var i = 0; i < aAePt.length; i++) {
            if (aAePt[i].id == idAE) {
                return true;
            }
        }
        return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al recorrer los atributos estadísticos heredados del PT", e.message);
    }
}
function estaAEenPT2(idAE, aAePt) {
    try {
        for (var i = 0; i < aAePt.length; i++) {
            if (aAePt[i].id == idAE) {
                return true;
            }
        }
        return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al recorrer los atributos estadísticos heredados del PT", e.message);
    }
}

function mostrarOTC() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        if ($I("hdnOtcH").value == "T") return;
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
                    aGAvanza(0);
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al mostrar las órdenes de trabajo codificadas", e.message);
    }
}
var sEstadoOrig = "";
function controlEstado(sValor) {
    try {
        if (sValor == "2") {
            mmoff("Inf", "No se puede poner la tarea en estado \"Pendiente\" de forma manual.", 260);
            $I("cboEstado").value = sEstadoOrig;
        } else sEstadoOrig = $I("cboEstado").value;

        //controlarFinalizada(sValor);

        var js_args = "getValPrevision@#@";
        js_args += dfn($I("txtIdTarea").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
    } catch (e) {
        mostrarErrorAplicacion("Error al controlar el valor del estado", e.message);
    }
}

function calcAvanPrev() {
    try {
        if ($I("txtConHor").value == "0,00" || $I("txtPREst").value == "0,00" || parseFloat(dfn($I("txtPREst").value), 10) == 0 ||
            $I("txtConHor").value == "" || $I("txtPREst").value == "") {
            $I("txtAvanTeo").value = "";
        } else {
            var nAvanTeo = parseFloat(dfn($I("txtConHor").value), 10) * 100 / parseFloat(dfn($I("txtPREst").value), 10);
            $I("txtAvanTeo").value = nAvanTeo.ToString("N");
        }
        clickAvanceAutomatico();
        calcularPorcentajes();
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular el avance previsto", e.message);
    }
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
function actuPrev() {
    try {
        if ($I("txtPLEst").value != "") {
            if ($I("txtPREst").value == "") $I("txtPREst").value = $I("txtPLEst").value;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al modificar las horas de la planificación", e.message);
    }
}
function calcularPorcentajes() {
    try {
        var fConsumido = 0;
        var fTotPlan = 0;
        var fTotPrev = 0;
        var fAvanTeo = 0;
        var fPorConsuido = 0;
        var fPorDesviacion = 0;

        if ($I("txtConHor").value != "") fConsumido = parseFloat(dfn($I("txtConHor").value));
        if ($I("txtPLEst").value != "") fTotPlan = parseFloat(dfn($I("txtPLEst").value));
        if ($I("txtPREst").value != "") fTotPrev = parseFloat(dfn($I("txtPREst").value));
        if ($I("txtAvanTeo").value != "") fAvanTeo = parseFloat(dfn($I("txtAvanTeo").value));
        //alert("fConsumido: "+ fConsumido + "\nfTotPlan: "+ fTotPlan + "\nfTotPrev: "+ fTotPrev + "\nfAvanTeo: "+ fAvanTeo);
        if (fConsumido > fTotPrev) {
            $I("txtConHor").className = "txtNumMR";
            //$I("txtPREst").className = "txtNumMR";
        }
        else
        {
            $I("txtConHor").className = "txtNumM";
            //$I("txtPREst").className = "txtNumM";
        }
        if (fConsumido != 0 && fTotPlan != 0) {
            fPorConsuido = fConsumido * 100 / fTotPlan;
            $I("txtPorCon").value = fPorConsuido.ToString("N");
        } else {
            $I("txtPorCon").value = "";
        }
        if (fTotPrev != 0 && fTotPlan != 0) {
            fPorDesviacion = (fTotPrev * 100 / fTotPlan) - 100;
            $I("txtPorDes").value = fPorDesviacion.ToString("N");
        } else {
            $I("txtPorDes").value = "";
        }
        $I("txtPdteTeo").value = (fTotPrev - fConsumido).ToString("N");

        //Si hay datos aplicamos el color (clase) correspondiente a la caja de texto (Desviación de esfuerzo)
        if ($I("txtPorDes").value != "")
        {
            if (fPorDesviacion <= 5)
                $I("txtPorDes").className = "txtNumMV";
            else if (fPorDesviacion <= 20)
                $I("txtPorDes").className = "txtNumMA";
            else
                $I("txtPorDes").className = "txtNumMR";
        }
        else
            $I("txtPorDes").className = "txtNumM";
        
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular los porcentajes", e.message);
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

function cargarComboEstado(sEstado) {
    //Carga del combo de estado en función del estado de la tarea
    try {
        $I("cboEstado").length = 0;
        if (sEstado == "2") {
            var op1 = new Option("Pendiente", 2);
            $I("cboEstado").options[0] = op1;
        }
        else {
            var op1 = new Option("Paralizada", 0);
            $I("cboEstado").options[0] = op1;
            var op2 = new Option("Activa", 1);
            $I("cboEstado").options[1] = op2;
            var op3 = new Option("Finalizada", 3);
            $I("cboEstado").options[2] = op3;
            var op4 = new Option("Cerrada", 4);
            $I("cboEstado").options[3] = op4;
            var op5 = new Option("Anulada", 5);
            $I("cboEstado").options[4] = op5;
        }
        $I("cboEstado").value = sEstado;
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
            //alert("Fila: "+ i +" obligatorio: "+ aFila[i].obl);
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
            if ($I("cboEstado").value == "2") {
                cargarComboEstado("1"); //pongo Activa si está pendiente
                aG(0);
            }
        }
        else {
            if ($I("cboEstado").value == "1") {
                cargarComboEstado("2"); //pongo Pendiente si está activa
                aG(0);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al verificar los valores de los AE obligatorios", e.message);
    }
}

function limpiarPST() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        $I('txtCodPST').value = "";
        $I('txtDesPST').value = "";
        $I('txtIdPST').value = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar los valores de la PST", e.message);
    }
}

function controlNotificable(nValue) {
    try {
        //0. id origen
        //1. notificable
        var sw = 0;
        var bValAnt = $I("chkNotificable").checked;
        for (var i = 0; i < aOrigen_js.length; i++) {
            if (aOrigen_js[i][0] == nValue) {
                if (aOrigen_js[i][1] == "1") $I("chkNotificable").checked = true;
                else $I("chkNotificable").checked = false;
                sw = 1;
                break;
            }
        }
        if (sw == 0) $I("chkNotificable").checked = false;
        //Si cambia el check de notificable hay que grabar la primera pestaña
        if ($I("chkNotificable").checked != bValAnt)
            aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar si el origen es notificable", e.message);
    }
}

function controlarFinalizada(sValor) {
    try {
        //if (!$I('chkFinalizada').checked){
        //        if (!sValor=="3"){
        //            $I('txtValFin').value = "";
        //        }else{

        if (sValor == "3" || sValor == "4")
            $I("spanImpIAP").style.visibility = "visible";
        else {
            $I("spanImpIAP").style.visibility = "hidden";
            $I("chkImpIAP").checked = false;
        }
        if (sValor == "3" || sValor == "4" || sValor == "5") {
            var objValFin;
            if ($I('txtValFin').value == "") {
                objValFin = new Date(9999, 11, 31);
            } else {
                var aValFin = $I('txtValFin').value.split("/");
                objValFin = new Date(aValFin[2], eval(aValFin[1] - 1), aValFin[0]);
            }
            var objHoy = new Date();
            var dFecha;
            if (objValFin > objHoy) dFecha = objHoy;
            else dFecha = objValFin;

            var strDia = dFecha.getDate();
            if (strDia < 10) strDia = "0" + strDia;
            var strMes = eval(dFecha.getMonth() + 1);
            if (strMes < 10) strMes = "0" + strMes;
            var strAnno = dFecha.getFullYear();
            var strFecha = strDia + "/" + strMes + "/" + strAnno;
            $I('txtValFin').value = strFecha//;

            // actualizar t332_etpr

            var iConHoras = 0;
            var iETPL = 0;
            var iETPR = 0;

            if ($I("txtConHor").value != "" && $I("txtConHor").value != 0) iConHoras = parseFloat(dfn($I("txtConHor").value));
            if ($I("txtPLEst").value!="" && $I("txtPLEst").value!=0) iETPL= parseFloat(dfn($I("txtPLEst").value));
            if ($I("txtPREst").value != "" && $I("txtPREst").value != 0) iETPR = parseFloat(dfn($I("txtPREst").value));

            if (iConHoras == 0 && iETPL != 0) $I("txtPREst").value = "";
            else 
            {
                //if (iETPR != 0) $I("txtPREst").value = iConHoras.ToString("N", 7, 2);
                $I("txtPREst").value = iConHoras.ToString("N", 7, 2);
            }

            // actualizar t332_ffpr

            if (iConHoras == 0 && $I("txtPLFin").value != "")
                $I("txtPRFin").value = $I("txtPLFin").value;
            else 
            {
//                if ($I("txtPLFin").value != "") 
//                {
                    //if ($I("txtUltCon").value < $I("txtPLIni").value)
                    if (DiffDiasFechas($I("txtUltCon").value, $I("txtPLIni").value) > 0)
                        $I("txtPRFin").value = $I("txtPLIni").value;
                    else
                        $I("txtPRFin").value = $I("txtUltCon").value;
//                }
            }
            // refrescar Situación
            calcAvanPrev();
            ocultarProcesando();           
        }
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el estado de finalizada", e.message);
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

function Exportar(strFormato) {
    try {
        //mmoff("Acceso no permitido.", 225);
        //return;
        if (getOp($I("btnPDF")) == 30) return;
        if ($I("hdnIdTarea").value == "") return;
        if ($I("hdnAcceso").value != "R") {
            if (bCambios && intSession > 0) {
                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                    if (answer) {
                        bExportar = true;
                        bEnviar = grabar();
                    }
                    else {
                        bCambios = false;
                        LlamarExportar(strFormato);
                    }
                });
            } else LlamarExportar(strFormato);
        } else LlamarExportar(strFormato);
    } catch (e) {
        mostrarErrorAplicacion("Error al exportar " + strFormato, e.message);
    }
}
function LlamarExportar1() {
    LlamarExportar('PDF');
}
function LlamarExportar(strFormato) {
    try {
        
        //*SSRS

        var params = {
            reportName: "/SUPER/sup_tarea",
            tipo: "PDF",
            nIdTarea: $I("hdnIdTarea").value,
            nodo: nodo,
            t314_idusuario_autor: t314_idusuario_autor
        }

        PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR
        strUrlPag = "Informe/default.aspx";
        strUrlPag += "?fm=" + codpar(strFormato);
        strUrlPag += "&it=" + codpar($I("hdnIdTarea").value);
        //strUrlPag += "&ip="+ codpar($I("hdnIDPT").value);

        if (strFormato == "PDF")
            if (screen.width == 800)
            window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=yes,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
        else
            window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=no,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
        else
            if (screen.width == 800)
            window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=yes,menubar=yes,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
        else
            window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=no,menubar=yes,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
        //CR*/

    } catch (e) {
        mostrarErrorAplicacion("Error al exportar " + strFormato, e.message);
    }
}
function obtenerProyectos() {
    try {
        var idTarea, sAux;
        if ($I("hdnEstr").value == "S") return;
        if ($I("hdnOrigen").value == "gantt") return;

        idTarea = $I("txtIdTarea").value;
        //En tareas grabadas no permitimos cambiar de Proyecto Económico
        if (idTarea != "" && idTarea != "0" && idTarea != "-1") return;
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sSoloAbiertos=1&sNoVerPIG=1"; //Solo proyectos abiertos
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
            if (ret != null) {
                var aDatos = ret.split("///");
                $I("hdnT305IdProy").value = aDatos[0];
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
function getModosFacturacion() {
    try {
        //alert("Hay que recuperar los modos de facturación: ");
        var js_args = "getMF@#@";
        js_args += $I("hdnT305IdProy").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
function getDatosProy(sPSN) {
    try {
        mostrarProcesando();
        var js_args = "getDatosProy@#@" + sPSN;
        //setTimeout("getDatosCEE(0);", 1000);
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos del ProyectoSubNodo " + sPSN, e.message);
    }
}
function getDatosProy2() {
    try {
        mostrarProcesando();
        RealizarCallBack("getDatosProy2@#@" + $I("hdnT305IdProy").value, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos del ProyectoSubNodo " + sPSN, e.message);
    }
}

function obtenerPTs() {
    try {
        if ($I("hdnOrigen").value == "gantt") return;

        var aOpciones, idPE, sPE, idPT, strEnlace, sAncho, sAlto;
        var idTarea = $I("txtIdTarea").value;
        //En tareas grabadas no permitimos cambiar de Proyecto técnico
        if (idTarea != "" && idTarea != "0" && idTarea != "-1") return;
        idPE = $I("txtNumPE").value;
        sPE = $I("txtPE").value;
        //En tareas grabadas no permitimos cambiar de Proyecto Económico
        var sTamano = "";
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
            .then(function(ret) {
            if (ret != null) {
                aOpciones = ret.split("@#@");
                idPT = aOpciones[0];
                if ($I("hdnIDPT").value != idPT) {
                    if (idPE == "" || idPE == "0") {
                        $I("txtNumPE").value = aOpciones[2];
                        $I("txtPE").value = aOpciones[3];
                        $I("hdnT305IdProy").value = aOpciones[4];
                        $I("hdnCRActual").value = aOpciones[6];
                        if (aOpciones[7] == "S") {//AdmiteRecursosPST
                            $I("chkHeredaCR").disabled = false;
                            if ($I("imgDelBajaPE") != null) $I("imgDelBajaPE").style.visibility = "visible";
                            $I("lblInsPST").style.visibility = "hidden";
                        }
                        else {
                            $I("chkHeredaCR").disabled = true;
                            if ($I("imgDelBajaPE") != null) $I("imgDelBajaPE").style.visibility = "hidden";
                            $I("lblInsPST").style.visibility = "visible";
                        }
                    }
                    $I("hdnIDPT").value = idPT;
                    $I("hdnIDFase").value = "";
                    $I("txtFase").value = "";
                    $I("hdnIDAct").value = "";
                    $I("txtActividad").value = "";
                    //Cargo los AE de PT que debe heredar la tarea y también la OTC si el PT lo tiene
                    var js_args = "aept2@#@" + idPT + "@#@" + $I("hdnCRActual").value;
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");  //con argumentos
                }
                $I("txtPT").value = aOpciones[1];
                //aG(0);
            }
        });
        
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}

function obtenerFases() {
    try {
        if ($I("hdnOrigen").value == "gantt") return;

        var aOpciones, idPE, sPE, idPT, sPT, idFase;
        var idTarea = $I("txtIdTarea").value;
        //En tareas grabadas no permitimos cambiar de Fase
        if (idTarea != "" && idTarea != "0" && idTarea != "-1") return;

        idPT = $I("hdnIDPT").value;
        if (gsIdPT == idPT) {
            if ($I("hdnUnicaEnActividad").value == "T") {
                mmoff("War", "No se puede modificar la fase por ser tarea única.\nSi deseas que la tarea no cuelgue de esta fase\ndebes eliminar la fase.", 340);
                return;
            }
        }
        idPE = $I("txtNumPE").value;
        sPE = $I("txtPE").value;
        sPT = $I("txtPT").value;
        //En tareas grabadas no permitimos cambiar de Proyecto Económico
        //	    if (idPE=="" || idPE=="0"){
        //	        alert("Para seleccionar una fase debe seleccionar\npreviamente un proyecto económico");
        //	        return;
        //	    }
        if (idPT == "" || idPT == "0") {
            mmoff("Inf", "Para seleccionar una fase debes seleccionar\npreviamente un proyecto técnico", 310);
            return;
        }
        var strEnlace = strServer + "Capa_Presentacion/PSP/Fase/obtenerFase.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(500, 540))
            .then(function(ret) {
            if (ret != null) {
                aOpciones = ret.split("@#@");
                idFase = aOpciones[0];
                if ($I("hdnIDFase").value != idFase) {
                    $I("hdnIDFase").value = idFase;
                    $I("hdnIDAct").value = "";
                    $I("txtActividad").value = "";
                }
                $I("txtFase").value = aOpciones[1];
                //aG(0);
            }
        });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las fases", e.message);
    }
}

function obtenerActividades() {
    try {
        if ($I("hdnOrigen").value == "gantt") return;

        var aOpciones, idPE, sPE, idPT, sPT, idFase, sFase, idActividad;
        var idTarea = $I("txtIdTarea").value;
        //En tareas grabadas no permitimos cambiar de Actividad
        if (idTarea != "" && idTarea != "0" && idTarea != "-1") return;

        idPT = $I("hdnIDPT").value;
        if (gsIdPT == idPT) {
            if ($I("hdnUnicaEnActividad").value == "T") {
                mmoff("War", "No se puede modificar la actividad por ser tarea única.\nSi deseas que la tarea no cuelgue de esta actividad\ndebes eliminar la actividad.", 340);
                return;
            }
        }
        idPE = $I("txtNumPE").value;
        sPE = $I("txtPE").value;
        sPT = $I("txtPT").value;
        idFase = $I("hdnIDFase").value;
        sFase = $I("txtFase").value;
        //En tareas grabadas no permitimos cambiar de Proyecto Económico
        //	    if (idPE=="" || idPE=="0"){
        //	        alert("Para seleccionar una actividad debe seleccionar\npreviamente un proyecto económico");
        //	        return;
        //	    }
        if (idPT == "" || idPT == "0") {
            mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto técnico", 320);
            return;
        }
        var strEnlace = strServer + "Capa_Presentacion/PSP/Actividad/obtenerActividad.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(500, 560))
            .then(function(ret) {
            if (ret != null) {
                aOpciones = ret.split("@#@");
                idActividad = aOpciones[0];
                $I("hdnIDAct").value = idActividad;
                $I("txtActividad").value = aOpciones[1];
                $I("hdnIDFase").value = aOpciones[2];
                $I("txtFase").value = aOpciones[3];
                //aG(0);
            }
        });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las actividades", e.message);
    }
}
function obtenerTareas() {
    try {
        if ($I("hdnOrigen").value == "gantt") return;
        //Si estoy en modo Crear no dejo acceso a la selección de Tarea
        if (gsModo == "C") return;

        var aOpciones, t305IdProy, idPE, sPE, idPT, sPT, idFase, sFase, idActividad, sActividad, idTarea, sTarea, strEnlace;
        t305IdProy = $I("hdnT305IdProy").value;
        idPE = $I("txtNumPE").value;
        sPE = $I("txtPE").value;
        idPT = $I("hdnIDPT").value;
        sPT = $I("txtPT").value;
        idFase = $I("hdnIDFase").value;
        sFase = $I("txtFase").value;
        idActividad = $I("hdnIDAct").value;
        sActividad = $I("txtActividad").value;
        sTarea = $I("txtDesTarea").value;
        if (idPE == "" || idPE == "0") {
            strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea2.aspx?nIdPE=" + t305IdProy + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
        }
        else {
            strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea.aspx?nIdPE=" + t305IdProy + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
        }
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(500, 580))
            .then(function(ret) {
            if (ret != null) {
                aOpciones = ret.split("@#@");
                idTarea = aOpciones[0];
                $I("txtIdTarea").value = idTarea;
                $I("txtDesTarea").value = Utilidades.unescape(aOpciones[1]);
                cargarTarea();
            }
        });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las tareas", e.message);
    }
}
function obtenerTareas2() {
    try {
        if ($I("hdnOrigen").value == "gantt") return;
        cargarTarea();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las tareas por código", e.message);
    }
}
function duplicar() {
    try {
        if (getOp($I("btnDuplicar")) == 30) return;
        if ($I("txtIdTarea").value == "") return;

        duplicarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al duplicar la tarea.", e.message);
    }
}
function obtenerTarifas() {
    try {
        var js_args = "tarifas@#@" + $I("txtNumPE").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de la tarifa.", e.message);
    }
}
function limpiar() {
    try{
        if (getOp($I("btnNuevo")) == 30) return;
        if ($I("hdnAcceso").value != "R") {
            if (bCambios && intSession > 0){
                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                    if (answer) {
                        bLimpiar = true;
                        bEnviar = grabar();
                    }
                    else {
                        bCambios = false;
                        LlamarLimpiar();
                    }
                });
            } else LlamarLimpiar();
        }
        else LlamarLimpiar();
    }catch(e){
        mostrarErrorAplicacion("Error al limpiar tarea-1.", e.message);
    }
}
function LlamarLimpiar() {
    try{
        var sCodPE, sDesPE, sCodPT, sDesPT, sCodFase, sDesFase, sCodAct, sDesAct;
        var sCodPST, sIdPST, sDesPST, sCodCR, sIdPSN;        

        $I("lblProy").className = "enlace";
        $I("lblPT").className = "enlace";
        $I("lblFase").className = "enlace";
        $I("lblActividad").className = "enlace";
        $I("Image8").style.visibility = "visible";
        $I("Image9").style.visibility = "visible";

        sCodPST = "";
        sDesPST = "";
        sIdPST = "";

        if (gsModo == "C") {
            sCodCR = $I("hdnCRActual").value;
            sIdPSN = $I("hdnT305IdProy").value;
            sCodPE = $I("txtNumPE").value;
            sDesPE = $I("txtPE").value;
            sCodPT = $I("hdnIDPT").value;
            sDesPT = $I("txtPT").value;
            sCodFase = $I("hdnIDFase").value;
            sDesFase = $I("txtFase").value;
            sCodAct = $I("hdnIDAct").value;
            sDesAct = $I("txtActividad").value;
            //Si la OTC es hereda la guardo para volver a ponerla
            if ($I("lblOtcH").style.visibility != "hidden") {
                sCodPST = $I('txtCodPST').value;
                sDesPST = $I('txtDesPST').value;
                sIdPST = $I('txtIdPST').value;
            }
        }
        else {
            sCodPE = "";
            sDesPE = "";
            sCodPT = "";
            sDesPT = "";
            sCodFase = "";
            sDesFase = "";
            sCodAct = "";
            sDesAct = "";
            activarOTC(true);
        }

        clearAll(document.forms[0]);
        $I("hdnCRActual").value = sCodCR;
        $I("hdnT305IdProy").value = sIdPSN;
        $I("txtNumPE").value = sCodPE;
        $I("txtPE").value = sDesPE;
        $I("hdnIDPT").value = sCodPT;
        $I("txtPT").value = sDesPT;
        $I("hdnIDFase").value = sCodFase;
        $I("txtFase").value = sDesFase;
        $I("hdnIDAct").value = sCodAct;
        $I("txtActividad").value = sDesAct;

        $I('txtCodPST').value = sCodPST;
        $I('txtDesPST').value = sDesPST;
        $I('txtIdPST').value = sIdPST;
        //if (gsModo=="B"){
        //$I("txtIdTarea").enabled=true;
        $I("txtIdTarea").readOnly = false;
        setCamposEstadoBusq();
        //}
        //Pongo valores por defecto
        var sEstado = "1"; //Activa
        $I("cboEstado").value = sEstado;
        $I("hdnEstado").value = sEstado;
        var anio, mes, dia;
        var Mi_Fecha = new Date();
        anio = Mi_Fecha.getFullYear();
        mes = Mi_Fecha.getMonth() + 1;
        if (mes.toString().length == 1) mes = "0" + mes;
        dia = Mi_Fecha.getDate();
        if (dia.toString().length == 1) dia = "0" + dia;
        var sFecha = dia + "/" + mes + "/" + anio;
        $I("txtValIni").value = sFecha;

        //Borro el contenido de las tabls
        //Profesioneales
        if (aPestGral[1].bLeido) {
            BorrarFilasDe("tblRelacion");
            BorrarFilasDe("tblAsignados");
        }
        //LIMPIEZA DE ATRIBUTOS ESTADÍSTICOS
        //BorrarFilasDe("tblAECR");
        if (gsModo == "C") {
            var aFilas = $I("tblAET").getElementsByTagName("TR");
            for (var i = aFilas.length - 1; i >= 0; i--) {
                $I("tblAET").rows[i].cells[3].innerText = "";
            }
            for (var i = aFilas.length - 1; i >= 0; i--) {
                if (aFilas[i].getAttribute("obl") == "0") {
                    $I("tblAET").deleteRow(i);
                }
            }
        }
        else {
            BorrarFilasDe("tblAET");
            BorrarFilasDe("tblAEPT");
        }

        BorrarFilasDe("tblAEVD");

        if ($I("tblDocumentos") != null)
            BorrarFilasDe("tblDocumentos"); //documentos 

        $I("nIdTarea").value = "";
        $I("Permiso").value = "";
        $I("nCR").value = $I("hdnCRActual").value;
        setTimeout("limpiarCodTarea()", 1000);
        reIniciarPestanas();
        tsPestanas.setSelectedIndex(0);
        bCambios = false;
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        setOp($I("btnBorrar"), 30);
        setOp($I("btnDuplicar"), 30);
        setOp($I("btnPDF"), 30);

        $I("btnBitacora").src = "../../../images/imgBTTN.gif";
        $I("btnBitacora").style.cursor = "default";
        $I("btnBitacora").onclick = mostrarBitacora;
        $I("btnBitacora").title = "Sin acceso a la bitácora de tarea.";
        if (gsModo == "C") $I("txtDesTarea").focus();        

    } catch (e) {
        mostrarErrorAplicacion("Error al poner nueva tarea.", e.message);
    }
}
function limpiarCodTarea() {
    $I("txtIdTarea").value = "";
    $I("txtFecModif").value = "";
    $I("txtIdUsuModif").value = "";
    $I("txtDesUsuModif").value = "";
}
function nuevoForm() {
    bCambios = false;
    bHayCambios = false;
    reIniciarPestanas();
    bSaliendo = true;
    
    //var theform = document.forms[0];
    //theform.submit();
    document.forms[0].submit();
    document.forms[0].action = strAction;
    document.forms[0].target = strTarget;

}
function cargarTarea() {
    try {
        mostrarProcesando();
        $I("nIdTarea").value = $I("txtIdTarea").value.replace('.', '');
        $I("Permiso").value = "";
        $I("nCR").value = $I("hdnCRActual").value;
        setTimeout("nuevoForm()", 1000);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la tarea.", e.message);
    }
}
function borrarFase() {
    try {
        var idPT;
        var idTarea = $I("txtIdTarea").value;
        //En tareas grabadas no permitimos cambiar de fase
        if (idTarea != "" && idTarea != "0" && idTarea != "-1") return;

        idPT = $I("hdnIDPT").value;
        if (gsIdPT == idPT) {
            if ($I("hdnUnicaEnActividad").value == "T") {
                mmoff("War", "No se puede modificar la fase por ser tarea única.\nSi deseas que la tarea no cuelgue de esta fase\ndebes eliminar la fase.", 340);
                return;
            }
        }
        if (($I("txtIdTarea").value == "") || ($I("txtIdTarea").value == "0")) {
            //Para no activar grabar cuando estamos en modo busqueda
        }
        else aG(0);
        $I("txtFase").value = "";
        $I("hdnIDFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIDAct").value = "";

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}
function borrarActividad() {
    try {
        var idPT;
        var idTarea = $I("txtIdTarea").value;
        //En tareas grabadas no permitimos cambiar de Actividad
        if (idTarea != "" && idTarea != "0" && idTarea != "-1") return;

        idPT = $I("hdnIDPT").value;
        if (gsIdPT == idPT) {
            if ($I("hdnUnicaEnActividad").value == "T") {
                mmoff("War", "No se puede modificar la actividad por ser tarea única.\nSi deseas que la tarea no cuelgue de esta actividad\ndebes eliminar la actividad.", 340);
                return;
            }
        }
        if (($I("txtIdTarea").value == "") || ($I("txtIdTarea").value == "0")) {
            //Para no activar grabar cuando estamos en modo busqueda
        }
        else aG(0);
        $I("txtActividad").value = "";
        $I("hdnIDAct").value = "";

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la actividad", e.message);
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
function nuevoDoc1() {
    //if ($I("hdnAcceso").value=="R")return;
    var sIdtarea = $I('hdnIdTarea').value;

    if ((sIdtarea == "") || (sIdtarea == "0")) {
        mmoff("Inf", "La tarea debe estar grabada para poder asociarle documentación", 400);
    }
    else {
        nuevoDoc('T', $I('hdnIdTarea').value);
    }
}
function eliminarDoc1() {
    if ($I("hdnModoAcceso").value == "R") return;
    eliminarDoc();
}

function ponerPST(sCadena) {
    //Cuando se elige un PT si tiene OTC la tarea debe heredarlo y dejarlo intocable
    try {
        var aElems = sCadena.split("##");
        if ((aElems[0] != "") && (aElems[0] != "0")) {
            $I('txtIdPST').value = aElems[0];
            $I('txtCodPST').value = aElems[1];
            $I('txtDesPST').value = aElems[2];
            activarOTC(false);
        }
        else {
            $I('txtIdPST').value = "";
            $I('txtCodPST').value = "";
            $I('txtDesPST').value = "";
            activarOTC(true);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la OTC del proyecto técnico", e.message);
    }
}
function activarOTC(bActivar) {
    try {
        //activación de campos en función de si la OTC es heredada o no
        if (!bActivar) {
            $I("lblOtcH").style.visibility = "";
            $I("lblOTC").className = "";
            $I("lblOTC").onclick = null;
            $I("Image7").style.visibility = "hidden";
        }
        else {
            $I("lblOtcH").style.visibility = "hidden";
            $I("Image7").style.visibility = "";
            $I("lblOTC").className = "enlace";
            $I("lblOTC").onclick = function () { mostrarOTC(); aGAvanza(0); };
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar la OTC del proyecto técnico", e.message);
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
function muchoEsfuerzo() {
    var bRes = false;
    try {
        if ($I("txtPREst").value == 0) return bRes;
        //Validaciones de los datos de los recursos.
        var total = 0, dAux = 0;
        for (var i = 0; i < aRecursos.length; i++) {
            if (aRecursos[i].opcionBD != "D") {
                if (aRecursos[i].etp != "") {
                    dAux = parseFloat(aRecursos[i].etp);
                    total += dAux;
                }
            }
        }
        if (total > $I("txtPREst").value) bRes = true;
        return bRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar el esfuerzo asignado a los recursos", e.message);
    }
}
//***************************************************************************************
//Funciones asociadas al Pool
//***************************************************************************************
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
                if (sw == 1) {
                    aGProf(1);
                }
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
                if (aFilas[i].getAttribute("h") == "N") {//Si es una fila no heredada permitimos borrado
                    if (aFilas[i].getAttribute("bd") == "I") $I("tblPoolGF").deleteRow(i);
                    else {
                        mfa(aFilas[i], "D");
                    }
                    sw = 1;
                }
            }
        }
        if (sw == 1) {
            aGProf(1);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desasignar un Grupo Funcional del pool.", e.message);
    }
}
function activarMensaje() {
    try {
        bEnvMensGral = true;
        aGProf(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al activar mensaje para todos los profesionales.", e.message);
    }
}
/* en base de datos
case when min(T332.t332_fipl) is not null and max(T332.t332_ffpl) is not null 
          and max(T332.t332_ffpr) is not null then
	convert(float, DATEDIFF (day , max(T332.t332_ffpl)  , max(T332.t332_ffpr))) * 100 / (  case when DATEDIFF ( day , min(T332.t332_fipl) , max(T332.t332_ffpl)) = 0 then 1 else convert(float, DATEDIFF ( day , min(T332.t332_fipl) , max(T332.t332_ffpl) ) + 1) end  )
	else null end as PorcPlazo,
*/			
function calcularDesvPlazo() {
    try {
        var ffpr = "", fipl = "", ffpl = "";
        var fPorPlazo = 0;
        var iDiasPlanificados = 1;

        if ($I("txtPLIni").value != "") fipl = $I("txtPLIni").value;
        if ($I("txtPLFin").value != "") {
            ffpl = $I("txtPLFin").value;
            if ($I("txtPRFin").value == "" && ffpl.length == 10) $I("txtPRFin").value = $I("txtPLFin").value;
        }
        if ($I("txtPRFin").value != "") ffpr = $I("txtPRFin").value;

        if (fipl != "" && ffpl != "" && ffpr != "") {
            if (fipl != ffpl)
                iDiasPlanificados = DiffDiasFechas(fipl, ffpl) + 1; 
            fPorPlazo = (DiffDiasFechas(ffpl, ffpr) * 100) / iDiasPlanificados;
            $I("txtDesvPlazo").value = fPorPlazo.ToString("N");
        } else {
            $I("txtDesvPlazo").value = "";
        }

        //Si hay datos aplicamos el color (clase) correspondiente a la caja de texto (Desviación de plazo)
        if ($I("txtDesvPlazo").value != "") {
            if (fPorPlazo <= 5)
                $I("txtDesvPlazo").className = "txtNumMR";
            else if (fPorPlazo <= 20)
                $I("txtDesvPlazo").className = "txtNumMA";
            else
                $I("txtDesvPlazo").className = "txtNumMR";
        }
        else
            $I("txtDesvPlazo").className = "txtNumM";

        if (DiffDiasFechas($I("txtFinEst").value, $I("txtPRFin").value) < 0) {
            $I("txtFinEst").style.backgroundColor = "#F45C5C";
            $I("txtPRFin").style.backgroundColor = "#F45C5C";
        }
        else {
            $I("txtFinEst").style.backgroundColor = "";
            $I("txtPRFin").style.backgroundColor = "";
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al calcular los porcentajes de desviación de plazo", e.message);
    }
}
function calcularDesvPlazo2() {
    try {
        var ffpr = "", fipl = "", ffpl = "";
        var fPorPlazo = 0;
        var iDiasPlanificados = 1;

        if ($I("txtPLIni").value != "") fipl = $I("txtPLIni").value;
        if ($I("txtPLFin").value != "") {
            //if  ($I("txtPRFin").value == "") $I("txtPRFin").value = $I("txtPLFin").value;
            ffpl = $I("txtPLFin").value;
        }
        if ($I("txtPRFin").value != "") ffpr = $I("txtPRFin").value;

        if (fipl != "" && ffpl != "" && ffpr != "") {
            if (fipl != ffpl)
                iDiasPlanificados = DiffDiasFechas(fipl, ffpl) + 1; 
            fPorPlazo = (DiffDiasFechas(ffpl, ffpr) * 100) / iDiasPlanificados;
            $I("txtDesvPlazo").value = fPorPlazo.ToString("N");
        } else {
            $I("txtDesvPlazo").value = "";
        }

        //Si hay datos aplicamos el color (clase) correspondiente a la caja de texto (Desviación de plazo)
        if ($I("txtDesvPlazo").value != "") {
            if (fPorPlazo <= 5)
                $I("txtDesvPlazo").className = "txtNumMV";
            else if (fPorPlazo <= 20)
                $I("txtDesvPlazo").className = "txtNumMA";
            else
                $I("txtDesvPlazo").className = "txtNumMR";
        }
        else
            $I("txtDesvPlazo").className = "txtNumM";

        if (DiffDiasFechas($I("txtFinEst").value, $I("txtPRFin").value) < 0) {
            $I("txtFinEst").style.backgroundColor = "#F45C5C";
            $I("txtPRFin").style.backgroundColor = "#F45C5C";
        }
        else {
            $I("txtFinEst").style.backgroundColor = "";
            $I("txtPRFin").style.backgroundColor = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular los porcentajes de desviación de plazo", e.message);
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
                if ($I("hdnIdTarea").value == "0" || $I("hdnIdTarea").value == "") {

                    if (gsModo == "C")
                        mmoff("Inf", "El acceso a la pestaña seleccionada, requiere grabar la tarea.", 420);
                    else
                        mmoff("Inf", "El acceso a la pestaña seleccionada, requiere seleccionar una tarea.", 430);

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
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña clientes.", e.message);
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
        mostrarProcesando();
        var js_args = "getDatosPestana@#@" + iPestana + "@#@" + $I("hdnIdTarea").value + "@#@" + $I("hdnIDPT").value + "@#@" + $I("hdnCRActual").value;
        if (iPestana == 5) {//Pestaña de documentos
            //modo de acceso a la pantalla y estado del proyecto
            gsDocModAcc = $I("hdnModoAcceso").value;
            gsDocEstPry = $I("hdnEstProy").value;
            setEstadoBotonesDoc(gsDocModAcc, gsDocEstPry);
            js_args += "@#@" + gsDocModAcc + "@#@" + gsDocEstPry;
        }

        RealizarCallBack(js_args, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}
function getDatosProf(iPestana) {
    try {
        mostrarProcesando();

        RealizarCallBack("getDatosPestanaProf@#@" + iPestana + "@#@" + $I("txtIdTarea").value + "@#@" +
                         $I("hdnIDPT").value + "@#@" + $I("hdnIDFase").value + "@#@" + $I("hdnIDAct").value + "@#@" + $I("hdnCRActual").value, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales " + iPestana, e.message);
    }
}
function getDatosAvanza(iPestana) {
    try {

        mostrarProcesando();
        var sAux="getDatosPestanaAvan@#@" + iPestana + "@#@" + $I("txtIdTarea").value + "@#@" + $I("hdnIDPT").value;
        RealizarCallBack(sAux);
        //$I("hdnIDPT").value + "@#@" + $I("hdnIDFase").value + "@#@" + $I("hdnIDAct").value + "@#@" + $I("hdnCRActual").value, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de datos avanzados " + iPestana, e.message);
    }
}
function getRecursos() {
    try {
        if ($I("hdnAcceso").value != "R") {
            if (bCambios && intSession > 1) {
                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                    if (answer) {
                        //bClickMostrarBajas = true;
                        bGetRecurso = true;
                        bEnviar = grabar();
                    } //else $I("chkVerBajas").checked = !$I("chkVerBajas").checked;
                });
            } else setTimeout("getRecursos2()", 50);
        }
        else setTimeout("getRecursos2()", 50);
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales " + iPestana, e.message);
    }
}
function getRecursos2() {
    try {
        mostrarProcesando();
        var sVer;
        if ($I("chkVerBajas").checked) sVer = "S";
        else sVer = "N";
        RealizarCallBack("getRecursos@#@" + dfn($I("txtIdTarea").value) + "@#@" + $I("hdnCRActual").value + "@#@" + sVer, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales " + iPestana, e.message);
    }
}
function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0":
                //no hago nada
                break;
            case "1": //Profesionales
                RespuestaCallBackPestanaProf("0", strResultado, "");
                break;
            case "2": //Avanzado
                RespuestaCallBackPestanaAvan("0", strResultado);
                break;
            case "3": //Notas 
                var aR2 = aResul[0].split("##");
                if (aR2[0] == "S") $I("chkNotasIAP").checked = true;
                else $I("chkNotasIAP").checked = false;
                $I("txtNotas1").value = Utilidades.unescape(aR2[1]);
                $I("txtNotas2").value = Utilidades.unescape(aR2[2]);
                $I("txtNotas3").value = Utilidades.unescape(aR2[3]);
                $I("txtNotas4").value = Utilidades.unescape(aR2[4]);
                break;
            case "4": //Control
                var aR2 = aResul[0].split("##");
                $I("txtObservaciones").value = aR2[0];

                $I("txtIdUsuAlta").value = aR2[1];
                $I("txtDesUsuAlta").value = aR2[2];
                $I("txtFecAlta").value = aR2[3];

                $I("txtIdUsuModif").value = aR2[4];
                $I("txtDesUsuModif").value = aR2[5];
                $I("txtFecModif").value = aR2[6];

                $I("txtIdUsuFin").value = aR2[7];
                $I("txtDesUsuFin").value = aR2[8];
                $I("txtFecFin").value = aR2[9];

                $I("txtIdUsuCierre").value = aR2[10];
                $I("txtDesUsuCierre").value = aR2[11];
                $I("txtFecCierre").value = aR2[12];
                break;
            case "5": //Documentación
                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
                $I("divCatalogoDoc").scrollTop = 0;
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
                borrarDatosAsignacion();
                scrollTablaProfAsig();
                actualizarLupas("tblTitRecAsig", "tblAsignados");
                break;
            case "1": //Pool´s
                $I("divPoolGF").children[0].innerHTML = aResul[0];
                $I("divPoolGF").children[0].scrollTop = 0;
                $I("lblNumEmp").innerHTML = sNumEmpleados;
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
                $I("divAECR").children[0].innerHTML = aResul[0];
                $I("divAECR").scrollTop = 0;
                $I("divAEPT").children[0].innerHTML = aResul[1];
                $I("divAEPT").scrollTop = 0;

                $I("divAET").children[0].innerHTML = aResul[2];
                $I("divAET").scrollTop = 0;

                comprobarAEObligatorios();
                eliminarAERepetidos();
                restringir();

                eval(aResul[4]);

                if (aResul[3] != "") {
                    var aR2 = aResul[3].split("##");
                    $I("txtOTL").value = aR2[0];
                    $I("txtIdPST").value = aR2[1];
                    $I("txtCodPST").value = aR2[2];
                    $I("txtDesPST").value = aR2[3];
                    $I("hdnOtcH").value = aR2[4];
                    //activación de campos en función de si la OTC es heredada o no
                    if ($I("hdnOtcH").value == "T") {
                        activarOTC(false);
                    }
                    else {
                        activarOTC(true);
                    }
                    if (aR2[5] != "")
                        mmoff("InfPer", aR2[5], 350);
                }
                else {
                    $I("txtOTL").value = "";
                    $I("txtIdPST").value = "";
                    $I("txtCodPST").value = "";
                    $I("txtDesPST").value = "";
                    $I("hdnOtcH").value = "";
                    activarOTC(true);
                }
                break;
            case "1": // Nuevo
                $I("divCatalogoValores").children[0].innerHTML = aResul[0];
                $I("divCatalogoValores").children[0].scrollTop = 0;

                //Heredados del PT
                $I("divCamposPT").children[0].innerHTML = aResul[1];
                $I("divCamposPT").children[0].scrollTop = 0;

                //Carga una lista con los códigos de Campo heredados del PT para no mostrarlos como candidatos
                //cuando se cargan los campos por ámbito
                lstCamposPT = aResul[2];

                //Todos los campos posibles
                $I("divCatalogo").children[0].innerHTML = strResultadoCampos;
                $I("divCatalogo").children[0].scrollTop = 0;
                
                
                
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
        aG(2);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
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
                case "imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            borrarRecursoDeArray(oRow.id);
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                            borrarDatosAsignacion();
                        }
                        else {
                            var objRec = buscarRecursoEnArray(oRow.id);
                            if (objRec != null) objRec.opcionBD = "D";
                            mfa(oRow, "D");
                            aGProf(0);
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
                                aGAvanza(0);
                            }
                            //BorrarFilasDe("tblAEVD");
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
                case "divAsignados":
                    if (FromTable == null || ToTable == null) continue;
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                        var sw = 0;
                        for (var i = 0; i < oTable.rows.length; i++) {
                            if (oTable.rows[i].id == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 0) {

                            var iFilaNueva = 0;
                            var sNombreNuevo, sNombreAct;

                            sNombreNuevo = oRow.innerText;
                            if (oTable != null) {
                                for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
                                    //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                                    sNombreAct = oTable.rows[iFilaNueva].innerText;
                                    if (sNombreAct > sNombreNuevo) break;
                                }
                            }
                            var NewRow = $I("tblAsignados").insertRow(iFilaNueva);
                            //			                var NewRow;
                            //			                if (nIndiceInsert == null) {
                            //			                    nIndiceInsert = oTable.rows.length;
                            //			                    NewRow = oTable.insertRow(nIndiceInsert);
                            //			                }
                            //			                else {
                            //			                    if (nIndiceInsert > oTable.rows.length)
                            //			                        nIndiceInsert = oTable.rows.length;
                            //			                    NewRow = oTable.insertRow(nIndiceInsert);
                            //			                }

                            //insertarRecursoEnArray("I", $I("hdnIdPT").value, oRow.id, oRow.innerText, "", "", oRow.idTarifa, "1", "","0");
                            insertarRecursoEnArray("I", $I("hdnIdTarea").value, oRow.id, oRow.innerText, "", "", "", "", "",
                                                   oRow.getAttribute("idTarifa"), "1", "", "", "", "", "", "", "", "0");
                            if (oRow.getAttribute("idTarifa") == "-1") $I("cboTarifa").value = "";
                            else $I("cboTarifa").value = oRow.getAttribute("idTarifa");

                            //nIndiceInsert++;
                            var oCloneNode = oRow.cloneNode(true);
                            oCloneNode.className = "";
                            NewRow.swapNode(oCloneNode);

                            oCloneNode.attachEvent("onclick", mm);

                            oCloneNode.onclick = function() { mostrarDatosAsignacion(this.id) };
                            oCloneNode.ondblclick = function() { eliminarRecurso(this); };
                            oCloneNode.insertCell(0);
                            oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));

                            //oCloneNode.cells[1].children[0].style.cursor = "../../../images/imgManoAzul2.cur";
                            if (oCloneNode.cells[1].children[0]) {
                                oCloneNode.cells[1].children[0].ondblclick = function() { setEstado(this); };
                                oCloneNode.cells[1].children[0].title = "Profesional activo en la tarea (le figura en su IAP)";
                            }
                            oCloneNode.children[1].children[0].style.cursor = strCurMA;    
                            mfa(oCloneNode, "I");
                            aGProf(0);
                        }
                    }
                    break;
                case "divAET":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
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
                    actualizarLupas("tblTitRecAsig", "tblAsignados");
                    break;
                case "imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            var oElem = getNextElementSibling(oElement.parentNode);
                            actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                        }
                    } else if (nOpcionDD == 4) {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                    }
                    break;
            }
        }
    }
    oTable = null;
    oTarget = null;
    killTimer();
    CancelDrag();
    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;
}
function setEstado(oImg) {
    try {
        if (getOp(oImg) == 100) {
            setOp(oImg, 20);
            oImg.title = "Profesional inactivo en la tarea (no le figura en su IAP)";
            oRecActualizar('U', 'estado', 0);
        } else {
            setOp(oImg, 100);
            oImg.title = "Profesional activo en la tarea (le figura en su IAP)";
            oRecActualizar('U', 'estado', 1);
        }
        oImg.style.cursor = strCurMA; //"../../../images/imgManoAzul2.cur";
        mfa(oImg.parentNode.parentNode, "U");
        aGProf(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer el estado de un profesional", e.message);
    }
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

                if ($I("hdnAcceso").value == "R") {
                    oFila.attachEvent("onclick", mm);
                    oFila.attachEvent("onmousedown", DD);
                    oFila.onclick = function() { mostrarDatosAsignacion(this.id); };
                }

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

                oFila.cells[1].children[0].ondblclick = function() { setEstado(this); };

                if (oFila.getAttribute("estado") == "0") {
                    setOp(oFila.cells[1].children[0], 20);
                    oFila.cells[1].children[0].title = "Profesional inactivo en la tarea (no le figura en su IAP)";
                } else {
                    oFila.cells[1].children[0].title = "Profesional activo en la tarea (le figura en su IAP)";
                }
                oFila.children[1].children[0].style.cursor = strCurMA;
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[2].style.color = "maroon";
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
//Reasigna los profesionales seleccionados si estaban asociados al proyecto con fecha de baja
function reAsignar() {
    try {
        var aFila = FilasDe("tblAsignados");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                if (aFila[i].getAttribute("baja") == "2") {
                    aFila[i].cells[2].style.color = "";
                    //                    aFila[i].cells[0].src="../../../images/imgFU.gif";
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
function setCamposEstadoBusq() {
    try {
        if (gsModo == "B") {
            if ($I("txtIdTarea").value == "") {
                $I("txtDesTarea").readOnly = true;
                $I("txtDesTarea").onkeyup = "";
                $I("txtDesTarea").onkeypress = "";

                $I("chkFacturable").disabled = true;
                $I("txtDescripcion").readOnly = true;
                $I("txtDescripcion").onkeyup = "";
                if (btnCal == "I") {
                    //$I("txtValIni").readOnly=true;
                    $I("txtValIni").onclick = "";
                    $I("txtValIni").onchange = "";
                    //$I("txtValFin").readOnly=true;
                    $I("txtValFin").onclick = "";
                    $I("txtValFin").onchange = "";

                    //$I("txtPLIni").readOnly=true;
                    $I("txtPLIni").onclick = "";
                    $I("txtPLIni").onchange = "";
                    //$I("txtPLFin").readOnly=true;
                    $I("txtPLFin").onclick = "";
                    $I("txtPLFin").onchange = "";

                    //$I("txtPRFin").readOnly=true;
                    $I("txtPRFin").onclick = "";
                    $I("txtPRFin").onchange = "";
                }
                else {
                    $I("txtValIni").onmousedown = "";
                    $I("txtValIni").onfocus = "";
                    $I("txtValFin").onmousedown = "";
                    $I("txtValFin").onfocus = "";
                    $I("txtPLIni").onmousedown = "";
                    $I("txtPLIni").onfocus = "";
                    $I("txtPLFin").onmousedown = "";
                    $I("txtPLFin").onfocus = "";
                    $I("txtPRFin").onmousedown = "";
                    $I("txtPRFin").onfocus = "";
                }
                $I("txtCLE").readOnly = true;
                $I("txtCLE").onkeyup = "";
                $I("txtCLE").onchange = "";
                $I("cboEstado").disabled = true;
                $I("rdbCLE").disabled = true;
                $I("rdbCLE").onclick = "";

                $I("txtPLEst").readOnly = true;
                $I("txtPLEst").onkeypress = "";
                $I("txtPLEst").onchange = "";

                $I("txtPREst").readOnly = true;
                $I("txtPREst").onkeypress = "";
                $I("txtPREst").onchange = "";

                $I("chkAvanceAuto").disabled = true;
                $I("txtAvanReal").readOnly = true;
                //                $I("txtAvanReal").onkeypress="";
                //                $I("txtAvanReal").onkeyup="";

                $I("txtPresupuesto").readOnly = true;
                $I("txtPresupuesto").onkeyup = "";
            }
            else {
                $I("txtDesTarea").readOnly = false;
                $I("txtDesTarea").onkeyup = function() { aG(0); };

                $I("txtDesTarea").attachEvent("onkeypress", teclaDenominacion);
                
                $I("chkFacturable").disabled = false;
                $I("txtDescripcion").readOnly = false;
                $I("txtDescripcion").onkeyup = function() { aG(0); };
                $I("txtCLE").readOnly = false;
                $I("txtCLE").onkeyup = function() { aG(0); };
                $I("txtCLE").onchange = function() { aG(0); };
                $I("cboEstado").disabled = false;
                $I("rdbCLE").disabled = false;
                $I("rdbCLE").onclick = function () { setInformativos(); aG(0); };

                if (btnCal == "I") {
                    //$I("txtValIni").readOnly=false;
                    $I("txtValIni").onclick = function() { mc(this); };
                    $I("txtValIni").onchange = function() { aG(0); };
                    //$I("txtValFin").readOnly=false;
                    $I("txtValFin").onclick = function() { mc(this); };
                    $I("txtValFin").onchange = function() { aG(0); };
                    //$I("txtPLIni").readOnly=false;
                    $I("txtPLIni").onclick = function() { mc(this); };
                    $I("txtPLIni").onchange = function() { calcularDesvPlazo(); aG(0); };
                    //$I("txtPLFin").readOnly=false;
                    $I("txtPLFin").onclick = function() { mc(this); };
                    $I("txtPLFin").onchange = function() { calcularDesvPlazo(); aG(0); };

                    //$I("txtPRFin").readOnly=false;
                    $I("txtPRFin").onclick = function() { mc(this); };
                    $I("txtPRFin").onchange = function() { aG(0); calcularDesvPlazo2(); };
                }
                else {
                    $I("txtValIni").onmousedown = function() { mc1(this); };
                    //$I("txtValIni").onfocus=function(){focoFecha(this);};
                    $I("txtValIni").attachEvent("onfocus", focoFecha);
                    $I("txtValFin").onmousedown = function() { mc1(this); };
                    //$I("txtValFin").onfocus=function(){focoFecha(this);};
                    $I("txtValFin").attachEvent("onfocus", focoFecha);
                    $I("txtPLIni").onmousedown = function() { mc1(this); };
                    //$I("txtPLIni").onfocus=function(){focoFecha(this);};
                    $I("txtPLIni").attachEvent("onfocus", focoFecha);
                    $I("txtPLFin").onmousedown = function() { mc1(this); };
                    //$I("txtPLFin").onfocus=function(){focoFecha(this);};
                    $I("txtPLFin").attachEvent("onfocus", focoFecha);
                    $I("txtPRFin").onmousedown = function() { mc1(this); };
                    //$I("txtPRFin").onfocus=function(){focoFecha(this);};
                    $I("txtPRFin").attachEvent("onfocus", focoFecha);
                }
                $I("txtPLEst").readOnly = false;
                $I("txtPLEst").onkeypress = function() { aG(0); };
                $I("txtPLEst").onkeyup = function() { calcularPorcentajes(); aG(0); };

                $I("txtPREst").readOnly = false;
                $I("txtPREst").onkeypress = function() { aG(0); };
                $I("txtPREst").onkeyup = function() { aG(0); calcAvanPrev(); };

                $I("chkAvanceAuto").disabled = false;
                $I("txtAvanReal").readOnly = false;
                //                $I("txtAvanReal").onkeypress=function (){pulsarTeclaAvanceReal();};
                //                $I("txtAvanReal").onkeyup=function (){soltarTeclaAvanceReal();calcularProducido();};
               
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reasignar usuarios.", e.message);
    }
}
//function setEstadoContenedor(sNombre, bDisabled){
//    try{
//        // Get a fieldset
//        //var f = document.getElementById('fieldsetID');
//        var f = document.getElementById(sNombre);
//        // Get all inputs in fieldset
//        var c = f.getElementsByTagName('input');
//        var l = c.length;
//        // Disable all inputs
//        for(var i=0; i<l; i++)
//          c[i].disabled = bDisabled;
//    }
//    catch(e){
//	    mostrarErrorAplicacion("Error al reasignar usuarios.", e.message);
//    }
//}
    function mostrarBitacora() {
        try {
            //var sCodTarea=$I("txtIdTarea").value.replace(".","");
            var sCodTarea = $I("txtIdTarea").value;
            if (sCodTarea == "") return;

            //Si la estructura no está grabada solicito grabacion
            if (bCambios) {
                ocultarProcesando();
                jqConfirm("", "Datos modificados.<br />Para acceder a Bitácora es preciso grabarlos. <br><br>¿Deseas hacerlo?", "", "", "war", 390).then(function (answer) {
                    if (answer) {
                        bBitacora = true;
                        grabar();
                    }
                });
            } else LLamarMostrarBitacora(sCodTarea);
        }
        catch (e) {
            mostrarErrorAplicacion("Error al mostrar Bitácora-1", e.message);
        }
    }
    function LLamarMostrarBitacora1() {
        LLamarMostrarBitacora($I("txtIdTarea").value);
    }
    function LLamarMostrarBitacora(sCodTarea) {
        try {
            //var sParam = "?sEstado=" + codpar($I("hdnEstado").value);
            //sCodT
            var sParam = "?t=" + codpar(sCodTarea);
            //sAccesoBitacoraPE
            if ($I("hdnAcceso").value == "W")
                sParam += "&a=" + codpar("E");
            else
                sParam += "&a=" + codpar("L");

            var sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/Default.aspx" + sParam; ;
            mostrarProcesando();
            modalDialog.Show(sPantalla, self, sSize(1014, 675))
                .then(function(ret) {
                    ocultarProcesando();
                });        
        }
        catch (e) {
            mostrarErrorAplicacion("Error al mostrar Bitácora-2", e.message);
        }
    }

    function setIconoBitacora() {
        try {
            if ($I("hdnAcceso").value == "W") {
                $I("btnBitacora").src = "../../../images/imgBTTW.gif";
                $I("btnBitacora").style.cursor = "pointer";
                $I("btnBitacora").onclick = mostrarBitacora;
                $I("btnBitacora").title = "Acceso en modo esritura a la bitácora de tarea.";
            }
            else {
                $I("btnBitacora").src = "../../../images/imgBTTR.gif";
                $I("btnBitacora").style.cursor = "pointer";
                $I("btnBitacora").onclick = mostrarBitacora;
                $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de tarea.";
            }
        } catch (e) {
            mostrarErrorAplicacion("Error al establecer el icono de la bitácora.", e.message);
        }
    }

    function getHistoriaPR() {
        try {
            mostrarProcesando();
            var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/getAuditoriaTarea/Default.aspx?nT=" + $I("hdnIdTarea").value;
            ocultarProcesando();
            modalDialog.Show(strEnlace, self, sSize(820, 580))
                .then(function(ret) {
                    ocultarProcesando();
                });         
        
        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar la pantalla de historial de previsión.", e.message);
        }
    }
    function getAuditoriaAux() {
        try {
            getAuditoria(9, $I("hdnIdTarea").value);
        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
        }
    }
    function controlarFecha(sTipo) {
        //A esta función se le llama desde el onchange de las cajas de texto que llevan calendario (FIV, FFV, FIPL, FFPL)
        //Comprueba si el valor actual es correcto
        try {
            var sFechaAct = "";
            switch (sTipo) {
                case "I": //FIPL
                    sFechaAct = $I("txtPLIni").value;
                    if (sFechaAct != "" && $I("txtPRFin").value != "") { //FIPL y FFPR
                        if (!fechasCongruentes(sFechaAct, $I("txtPRFin").value)) {
                            mmoff("infPer", "La fecha de inicio planificada: " + sFechaAct + "\ndebe ser anterior a la fecha de fin prevista: " + $I("txtPRFin").value + ".", 370);
                            $I("txtPLIni").focus();
                            return;
                        }
                    }
                    if (!fechasCongruentes(sFechaAct, $I("txtPLFin").value)) {
                        mmoff("infPer", "La fecha de inicio planificada: " + sFechaAct + "\ndebe ser anterior a la fecha de fin planificada: " + $I("txtPLFin").value, 380);
                        $I("txtPLIni").focus();
                        return;
                    }
                    break;
                case "F": //FFPL
                    sFechaAct = $I("txtPLFin").value;
                    if (!fechasCongruentes($I("txtPLIni").value, sFechaAct)) {
                        mmoff("InfPer", "La fecha de fin planificada: " + sFechaAct + "\ndebe ser posterior a la fecha de inicio planificada: " + $I("txtPLIni").value, 380);
                        $I("txtPLFin").focus();
                        return;
                    }
                    break;
                case "VI": //FIV
                    sFechaAct = $I("txtValIni").value;
                    //NO DEJO VACIAR LA FECHA DE INICIO DE VIGENCIA
                    if (sFechaAct == "") {
                        //Por indicación de Víctor el 14/04/2015 la fecha de inicio de vigencia no puede ser vacía
                        //alert("La fecha de inicio de vigencia no puede ser vacía");
                        mmoff("infPer", "La fecha de inicio de vigencia no puede ser vacía", 350);
                    }
                    else {
                        if (!fechasCongruentes(sFechaAct, $I("txtValFin").value)) {
                            mmoff("War", "La fecha de inicio de vigencia: " + sFechaAct + "\ndebe ser anterior a la fecha de fin de vigencia: " + $I("txtValFin").value, 350);
                            $I("txtValIni").focus();
                            return;
                        }
                    }
                    break;
                case "VF": //FFV
                    sFechaAct = $I("txtValFin").value;
                    if (!fechasCongruentes($I("txtValIni").value, sFechaAct)) {
                        mmoff("War", "La fecha de fin de vigencia: " + sFechaAct + "\ndebe ser posterior a la fecha de inicio de vigencia: " + $I("txtValIni").value, 350);
                        $I("txtValFin").focus();
                        return;
                    }
                    break;
            }
        }
        catch (e) {
            mostrarErrorAplicacion("Error al controlar fechas", e.message);
        }
    }
    /*
    function MtoDeCampos() {
        try {
            mostrarProcesando();
            var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/Default.aspx?nT=" + $I("hdnIdTarea").value;
            ocultarProcesando();
            modalDialog.Show(strEnlace, self, sSize(950, 560))
                .then(function (ret) {
                    ocultarProcesando();
                });
    
        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar la pantalla de historial de previsión.", e.message);
        }
    }
    function getCampos(nTipo) {
        try {
            mostrarProcesando();
            var sTamano = sSize(850, 420);
            var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
    
            //Paso los elementos que ya tengo seleccionados
    
            oTabla = $I("tblCampos");
    
            slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
    
    
            modalDialog.Show(strEnlace, self, sTamano)
                .then(function (ret) {                
                                    if (ret != null) {
                                        var aElementos = ret.split("///");
                                        insertarTabla(aElementos, "tblCampos"); 
                                        ocultarProcesando();
                                    } else ocultarProcesando();                           
                });
    
            //window.focus();
    
        } catch (e) {
            mostrarErrorAplicacion("Error al obtener los campos", e.message);
        }
    }
    */

    function cargarCamposPorAmbito(codAmbito) {
        try {

            var js_args = "cargarCamposPorAmbito@#@";
            js_args += ((codAmbito == "") ? $I("cboAmbito").value : codAmbito) + "@#@" + lstCamposPT;

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
            var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/Default.aspx?nTarea=" + $I("nIdTarea").value + "&t305_idproyectosubnodo=" + $I("hdnT305IdProy").value;
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


    function MtoCamposTarea() {
        try {

            if ($I("hdnT305IdProy").value == "") {
                mmoff("infPer", "La tarea debe estar creada", 350);
                return;
            }
            mostrarProcesando();
            var sTamano = sSize(850, 420);
            var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/MtoCamposTarea/default.aspx?nTarea=" + $I("txtIdTarea").value +
                                        "&t305_idproyectosubnodo=" + $I("hdnT305IdProy").value + 
                                        "&lstCamposPT=" + lstCamposPT;
            //Paso los elementos que ya tengo seleccionados

            oTabla = $I("tblCampos");

            slValores = fgGetCriteriosSeleccionados(40, oTabla);
            js_Valores = slValores.split("///");

            modalDialog.Show(strEnlace, self, sTamano)
                .then(function (ret) {
                    if (ret != null) {
                        //var aElementos = ret.split("///");
                        //insertarTabla(aElementos, "tblCampos");
                        getDatosAvanza(1);
                        ocultarProcesando();
                    } else ocultarProcesando();
                });
        } catch (e) {
            mostrarErrorAplicacion("Error al añadir filas en la tabla", e.message);
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
                        if (bMsg) mmoff("Inf", "El campo seleccionado ya está asociado a la tarea.", 400);
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
                        oCtrl1.onchange = function () { aGAvanza(1); }
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
               
                aG(3);
                aGAvanza(1);
            }
            return iFilaNueva;

        }
        catch (e) {
            mostrarErrorAplicacion("Error al insertar el item.", e.message);
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
                    if (aDatos[0] == $I("tblCampos").rows[j].id)
                    {
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
                            oCtrl1.onchange = function () { fm(this);};
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

    function eliminarValor()
    {
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
    function modoControles(oFila, bMod) {
        try {
            if (bLectura) return;
            if (oFila == null) return;
            var aControl = oFila.getElementsByTagName("INPUT");
            for (var i = 0; i < aControl.length; i++) {
                switch (aControl[i].type) {
                    case "text":
                        if (bMod) {
                            if (aControl[i].className != "txtV" && aControl[i].className != "txtNumV" && aControl[i].className != "txtFecV") {
                                if (aControl[i].className == "txtNumL") aControl[i].className = "txtNumM";
                                else if (aControl[i].className == "txtL") aControl[i].className = "txtM";
                                else if (aControl[i].className == "txtFecL") aControl[i].className = "txtFecM";
                            }
                        } else {
                            if (aControl[i].className != "txtV" && aControl[i].className != "txtNumV" && aControl[i].className != "txtFecV") {
                                if (aControl[i].className == "txtNumM") aControl[i].className = "txtNumL";
                                else if (aControl[i].className == "txtM") aControl[i].className = "txtL";
                                else if (aControl[i].className == "txtFecM") aControl[i].className = "txtFecL";
                            }
                        }
                        break;
                        /*
                        case "checkbox":
                            if (bMod) aControl[i].className = "";
                            else aControl[i].className = "";
                            break;
                        */
                }
            }
            aControl = oFila.getElementsByTagName("SELECT");
            for (var i = 0; i < aControl.length; i++) {
                if (bMod) aControl[i].disabled = false;
                else aControl[i].disabled = true;
            }
        } catch (e) {
            mostrarErrorAplicacion("Error al cambiar el estilo de los controles", e.message);
        }
    }

    function setInformativos(){
        try {
            if (getRadioButtonSelectedValue("rdbCLE", true) == "B") {
                $I("cboInformativo").disabled = true;
            }else{
                $I("cboInformativo").disabled = false;
            }
	    }catch(e){
		    mostrarErrorAplicacion("Error al establecer el filtro.", e.message);
        }
    }

    function getCLE() {
        try {

            var result = "B";
            if (getRadioButtonSelectedValue("rdbCLE", true) == "I") {
                result = $I("cboInformativo").value;
            }
            return result;

        } catch (e) {
            mostrarErrorAplicacion("Error al establecer e tipo de control de límite de esfuerzo.", e.message);
        }
    }