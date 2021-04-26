var selectedId = -1;
var bTieneSinopsis = false;
//var sMapa="";
var bCambiosOb = false;
var bCambiosS = false;
function init() {
    try {
        setOp($I("btnGuiaExam"), 30);
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarS"), 30);
        $I("rdlInternacional_0").style.verticalAlign = "middle";
        $I("rdlInternacional_1").style.verticalAlign = "middle";
//        if ($I("hdnEsEncargado").value == "1") {
//            $I("imgProf").style.visibility = "visible";
//        }

        if (origen == "1")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; CVT &gt; Mi Currículum ";
        else
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; CVT &gt; Mantenimiento de Currículum ";

        iniciarPestanas();
        cargarPendientes(sDatosPendientes);
        if (es_DIS || sMOSTRAR_SOLODIS == "0") mostrarMensajesTareasPendientes(sTareasPendientes);
        
        //$I("btnAddIdioma").onclick = function () { mantIdioma(sIdFicepi, -1); }
        $I("btnAddIdioma").onclick = function () { mantIdioma($I("hdnIdficepi").value, -1); }

//        var js_args = "delFoto@#@"; //PARA LIBERAR ESPACIO
        //        RealizarCallBack(js_args, "");
        strAction = document.forms["aspnetForm"].action; //Guardo el original
        strTarget = document.forms["aspnetForm"].target; //Guardo el original
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}
function getDocumentos() {
    try {
        mostrarProcesando();

        var sParam = "?ID=" + codpar($I("hdnIdficepi").value);
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/Documentos/Default.aspx" + sParam;
        modalDialog.Show(strEnlace, self, sSize(780, 300))
            .then(function(ret) {
                if (ret != null) {
                    //            var aDatos = ret.split("@#@");
                    //            $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    //            $I("hdnDesde").value = aDatos[0];
                    //            $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    //            $I("hdnHasta").value = aDatos[1];
                }
            });
        ocultarProcesando();
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    try {
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK") {
            if (aResul[0] == "borrarCertif") {
                switch (aResul[2]) {
                    case "S": //Si el certificado tiene examenes validados
                        PedirBorrado("tblDatosExamenCert", oFilaPetBorr);
                        break;
                    case "CONF_BORR": //Si el certificado tiene examenes que están asociados a otro certificado de ese profesional
                        //PedirBorrado("tblDatosExamenCert", oFilaPetBorr);
                        //confBorrarCertificados
                        var sBorrarExamen = "BORR_CERT";
                        ocultarProcesando();
                        jqConfirm("", "El certificado contiene exámenes asociados a otros certificados.<br><br>¿Deseas borrar también esos exámenes?", "", "", "war", 450).then(function (answer) {
                            if (answer) sBorrarExamen = "BORR_TODO";
                            setTimeout("confBorrarCertificados('" + sBorrarExamen + "')", 100);
                        });
                        break;
                    default:
                        ocultarProcesando();
                        var reg = /\\n/g;
                        alert(aResul[2].replace(reg, "\n"));
                        break;
                }
            }
            else {
                ocultarProcesando();
                var reg = /\\n/g;
                alert(aResul[2].replace(reg, "\n"));
            }
        } else {
        switch (aResul[0]) {
//            case "delFoto":
//                break;

            case "cargarFormacionAcad":
                $I("divCatalogoAcademica").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosTitulacion"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestGral[1].bLeido = true;
                aPestFor[0].bLeido = true;
                if ($I("chkFor_For").checked) mostrarPdtes($I("chkFor_For"));
                break;
            case "cargarCursos":
                $I("divCatalogoAcciones").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosFormacionRecibida"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestFor[1].bLeido = true;
                aPestAcc[0].bLeido = true;
                $I("divCatalogoAcciones").focus();
                if ($I("chkFor_Acc_Rec").checked) mostrarPdtes($I("chkFor_Acc_Rec"));
                break;
            case "cargarCursosImpartidos":
                $I("divCatalogoAccionesImpartidas").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosFormacionImpartida"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestAcc[1].bLeido = true;
                if ($I("chkFor_Acc_Imp").checked) mostrarPdtes($I("chkFor_Acc_Imp"));                
                break;
            case "cargarCertExam":
                $I("divCatalogoCertificados").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosExamenCert"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestFor[2].bLeido = true;
                aPestCertExam[0].bLeido = true;
                mostrarCertificadosSugeridos($I("chkFor_CertSug"));
                if ($I("chkFor_Cert").checked) mostrarPdtes($I("chkFor_Cert"));
                RefrescarCatalogo();
                break;
            case "reCargarCertExam":
                $I("divCatalogoCertificados").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosExamenCert"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestFor[2].bLeido = true;
                aPestCertExam[0].bLeido = true;
                mostrarCertificadosSugeridos($I("chkFor_CertSug"));
                if ($I("chkFor_Cert").checked) mostrarPdtes($I("chkFor_Cert"));
                RefrescarCatalogo();
                //Si la pestaña de exámenes está leída la actualizamos
                if (aPestCertExam[1].bLeido)
                    setTimeout("cargar('cargarExam')", 200);
                break;
            case "cargarExam":
                $I("divCatalogoExamenes").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosExamen"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestCertExam[1].bLeido = true;
                if ($I("chkFor_Exam").checked) mostrarPdtes($I("chkFor_Exam"));
                //setTimeout("mostrarPdtes($I('chkFor_Cert'))", 100);
                break;
            case "reCargarExam":
                $I("divCatalogoExamenes").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosExamen"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestCertExam[1].bLeido = true;
                if ($I("chkFor_Exam").checked) mostrarPdtes($I("chkFor_Exam"));
                //setTimeout("mostrarPdtes($I('chkFor_Cert'))", 100);
                //Actualizo la pestaña de certificados
                setTimeout("cargar('cargarCertExam')", 200);
                break;
            case "cargarIdiomas":
                $I("divCatalogoIdiomas").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblDatosIdiomas"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestFor[3].bLeido = true;
                if ($I("chkFor_Idi").checked) mostrarPdtes($I("chkFor_Idi"));
                break;
                
            case "cargarExpIb":
                $I("divExpIber").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblExpProfIber"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestGral[2].bLeido = true;
                aPestExp[0].bLeido = true;
                if ($I("chkExpDentro").checked) mostrarPdtes($I("chkExpDentro"));
                break;
            case "cargarExpNoIb":
                $I("divExpNoIber").children[0].innerHTML = aResul[2];
                seleccionarFila($I("tblExpProfNoIber"));
                sDatosPendientes = aResul[3];
                cargarPendientes(sDatosPendientes);
                aPestExp[1].bLeido = true;
                if ($I("chkExpFuera").checked) mostrarPdtes($I("chkExpFuera"));
                break;
                
            case "cargarTitulos":
                $I(aResul[3].ToString()).innerHTML = aResul[2];
                sDatosPendientes = aResul[4];
                cargarPendientes(sDatosPendientes);
                break;
            case "grabar":
                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200);
                bCambiosOb = false;
                if (!bCambiosS && bCambios)
                    bCambios = false;
                break;
            case "borrarForAcad":
                desActivarGrabar();
                eliminarFilasTabla("tblDatosTitulacion");
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "borrarForRec":
                desActivarGrabar();
                eliminarFilasTabla("tblDatosFormacionRecibida");
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "borrarForImp":
                desActivarGrabar();
                eliminarFilasTabla("tblDatosFormacionImpartida");
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "borrarIdioma":
                desActivarGrabar();
                eliminarFilasTabla("tblDatosIdiomas");
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "borrarCertif":
                desActivarGrabar();
                //eliminarFilasTabla("tblDatosExamenCert");
                setTimeout("cargar('reCargarCertExam')", 200);
                mostrarCertificadosSugeridos($I("chkFor_CertSug"));
                mostrarPdtes($I("chkFor_Cert"));
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "borrarExamen":
                desActivarGrabar();
                eliminarFilasTabla("tblDatosExamen");
                setTimeout("cargar('reCargarExam')", 200);
                mostrarPdtes($I("chkFor_Exam"));
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "borrarExpIber":
                desActivarGrabar();
                eliminarExpIber();
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "borrarExpNoIber":
                desActivarGrabar();
                eliminarExpNoIber();
                mmoff("Suc", "Eliminación correcta", 170);
                break;
            case "cargarPerfiles":
                $I(aResul[3].ToString()).innerHTML = aResul[2];
                sDatosPendientes = aResul[4];
                cargarPendientes(sDatosPendientes);
                mostrarPerfiles(aResul[3]);
                break;
            case "reCargarPerfiles":
                $I(aResul[3].ToString()).innerHTML = aResul[2];
                sDatosPendientes = aResul[4];
                cargarPendientes(sDatosPendientes);
                break;
            case "FinCV":
                if ($I("chkCorreo").checked)
                    mmoff("Inf", "Alta de CV procesada y correo enviado al profesional correctamente", 300);
                else
                    mmoff("Inf", "Alta de CV procesada correctamente", 250);
                $I("btnFinCv").style.display = "none";
                setTimeout("irAlMto();", 500);
                break;
            case "setCompletadoProf":
                $I("btnCVCompletadoProf").style.display = "none";
                break;
            case "setRevisadoActualizadoCV":
                if (aResul[2] == "NOVALIDADO") {
                    mmoff("Suc", "Existe información del currículum pendiente de cumplimentar", 360);
                }
                else {
                    setOp($I("btnCVCompletadoProf"), 30);
                    mmoff("Suc", "Validación del currículum realizada correctamente", 330);
                }            
            
                $I("MensajeFijo").style.display = "none"; 
                break;                
            case ("cargarSinopsis"):
                $I("txtSinopsis").value = aResul[2];
                aPestGral[3].bLeido = true;
                break;
            case ("grabarS"):
                mmoff("Suc", "Se ha grabado la sinopsis del curriculum correctamente", 360, 4000);
                setOp($I("btnGrabarS"), 30);
                bCambiosS = false;
                if (!bCambiosOb && bCambios)
                    bCambios = false;
                break;
            default: 
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la respuesta callback (" + aResul[0] + ")", e.message);
    }
}
function irAlMto() {
    location.href = "../MantCV/Default.aspx";
}
function mostrarPerfiles(strDiv) {
    try {
        if ($I(strDiv).className == "ocultarcapa" || $I(strDiv).className == "titulo3 ocultarcapa") {
            if ($I(strDiv).className == "titulo3 ocultarcapa")
                $I(strDiv).className = "titulo3 mostrarcapa";
            else
                $I(strDiv).className = "mostrarcapa";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar perfiles", e.message);
    }
}

function cargarPendientes(strDatos){
    var aResul = strDatos.split("@##@");
    var color = "";
    var Seccion = "";
    var estadoSeccion = "";
    var estado = "";
    for (var i = 0; i <= aResul.length - 1; i++) {
        if (aResul[i] != "") {
            var aArgs = aResul[i].split("#dato#");
            if (i == 0) {
                Seccion = aArgs[0];
                estadoSeccion += aArgs[2] + "estadoSeccion";
            }
            else {
                if (aArgs[0] != Seccion) {
                    estado = devolverEstado(estadoSeccion);
                    darEstiloSecSubsec(estado, Seccion, "Sec");
                    Seccion = aArgs[0];
                    //darEstiloSecSubsec(estado, aArgs[0], "Sec");
                    estadoSeccion = "";
                } 
                estadoSeccion += aArgs[2] + "estadoSeccion";
            }
            darEstiloSecSubsec(aArgs[2], aArgs[1],"Sub");
        }
    }
    
    estado = devolverEstado(estadoSeccion);
    darEstiloSecSubsec(estado, aArgs[0],"Sec");

}
function mostrarMensajesTareasPendientes(strDatos) {
    var aResul = strDatos.split("@##@");
    var color = "";
    var Seccion = "";
    var estadoSeccion = "";
    var estado = "";
    for (var i = 0; i <= aResul.length - 1; i++) {
        if (aResul[i] != "") {
            var aArgs = aResul[i].split("#dato#");
            if ($I("hdnEsMiCV").value == "S" || es_DIS) 
            {
                if (aArgs[2] == "0" && aArgs[3] != "")   // Caso:1 (Actualización masiva)
                {
                    mmoff("warper", "¡Atención!\n\nDebes revisar y actualizar tu CV para el " + cadenaAfecha(aArgs[3]).ToShortDateString() + " y una vez hayas finalizado, pulsa el botón 'CV revisado y actualizado'.", 690);
                        $I("MensajeFijo").style.display = "block";
                    $I("btnCVCompletadoProf").onclick = null;
                    $I("btnCVCompletadoProf").onclick = function() { setRevisadoActualizadoCV(); }; 
                     	 
                    $I("lblFechaLimite").innerText = cadenaAfecha(aArgs[3]).ToShortDateString();
                    
                    var fechaHoy = new Date();
                                          
                    if (fechaHoy>cadenaAfecha(aArgs[3]))
                        $I("imgEnPlazoyFuera").src = strServer + "images/imgCVFueraPlazo.png";
                    else
                        $I("imgEnPlazoyFuera").src = strServer + "images/imgCVEnPlazo.png";
                }
            }
        }
    }
}
function devolverEstado(estadoSeccion) {

    if (estadoSeccion.indexOf("T") > -1)
        return "T";
    else if (estadoSeccion.indexOf("P") > -1)
        return "P";
    else
        return "";
}

function darEstiloSecSubsec(estado, label, subsec) {
    try {
        var sImg = "";
        if (subsec == "Sec") {
            switch (label) {
                case "SFormacion": sImg = "imgWarningFormacion"; break;
                case "SExperiencia": sImg = "imgWarningExperiencia"; break;
                case "SAcciones": sImg = "imgWarningAcciones"; break;
                case "SCertExam": sImg = "imgWarningCertExam"; break;
            }
        } else { //Sub
            switch (label) {
                case "lblFormacionAcad": sImg = "imgWarningAcademica"; break;
                case "lblCursosReci": sImg = "imgWarningAccionesRecibidas"; break;
                case "lblCursosImp": sImg = "imgWarningAccionesImpartidas"; break;
                case "lblCertificado": sImg = "imgWarningCertificados"; break;
                case "lblExamen": sImg = "imgWarningExamenes"; break;
                case "lblIdiomas": sImg = "imgWarningIdiomas"; break;
                case "lblExpIber": sImg = "imgWarningEnIbermatica"; break;
                case "lblExpNoIber": sImg = "imgWarningFueraIbermatica"; break;
            }
        }
        //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
        switch (estado) {
            case "T": 
                //if (sIdFicepi == sIDFicepiEntrada) $I(sImg).src = strServer + "images/imgPenCumplimentarAnim.gif";
                if ($I("hdnIdficepi").value == sIDFicepiEntrada) $I(sImg).src = strServer + "images/imgPenCumplimentarAnim.gif";
                else $I(sImg).src = strServer + "images/imgPenCumplimentar.png";
                $I(sImg).title = "Datos que tienes pendiente de completar, actualizar o modificar";
                break;
            //case "P":
            //    $I(sImg).src = strServer + "images/imgPenValidar.png";
            //    $I(sImg).title = "Datos pendientes de validar por la organización";
            //    break;
            default:
                $I(sImg).src = strServer + "images/imgSeparador.gif"; 
                $I(sImg).title = "";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al dar estilos pendientes", e.message);
    }
}

////desplegar catalogo
function cargar(strOpcion) {
    try {
        mostrarProcesando();
        selectedId = -1;
        RealizarCallBack(strOpcion, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar datos", e.message);
    }  
}
/*CERTIFICADOS*/
var oFilaPetBorr;
function borrarCertificados() {
    if (borradoYaSolicitado("tblDatosExamenCert")) {
        ocultarProcesando();
        mmoff("Inf", "El elemento ya tiene una solicitud de borrado realizada.", 400);
        return;
    }
    var aFilas = FilasDe("tblDatosExamenCert");
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarCertif@#@PREGUNTAR@#@" + $I("hdnIdficepi").value + "@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            //if ($I("tblDatosExamenCert").rows[x].getAttribute("ori") == "1" && $I("tblDatosExamenCert").rows[x].getAttribute("est") != "V") {
            //No se puede borrar un certificado sugerido
            if (aFilas[x].getAttribute("idFicCert") == "0") {
                mmoff("War", "No puedes eliminar un certificado sugerido", 300);
                return;
            }
            //Solo se puede borrar si el origen es CVT
            if (aFilas[x].getAttribute("ori") == "1") {
                //Si está validado hay que solicitar su borrado, sino se puede borrar
                if (aFilas[x].getAttribute("est") != "V") {
                    bHayBorrado = true;
                    oFilaPetBorr = aFilas[x];
                    sb.Append(aFilas[x].getAttribute("id") + "##");
                }
                else {
                    PedirBorrado("tblDatosExamenCert", aFilas[x]);
                    return;
                }
            }
            else {
                //mmoff("War", "No puedes eliminar un certificado cuyo origen es EN FORMA", 400);
                PedirBorrado("tblDatosExamenCert", aFilas[x]);
                return;
            }
        }
    }

    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado del elemento seleccionado.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);
}
function confBorrarCertificados(sBorrExamen) {
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarCertif@#@" + sBorrExamen + "@#@" + $I("hdnIdficepi").value + "@#@");
    
    for (var x = $I("tblDatosExamenCert").rows.length - 1; x >= 0; x--) {
        if ($I("tblDatosExamenCert").rows[x].className == "FS") {
            if ($I("tblDatosExamenCert").rows[x].getAttribute("ori") == "1" && $I("tblDatosExamenCert").rows[x].getAttribute("est") != "V") {
                bHayBorrado = true;
                oFilaPetBorr = $I("tblDatosExamenCert").rows[x];
                sb.Append($I("tblDatosExamenCert").rows[x].getAttribute("id") + "##");
            }
        }
    }
    if (bHayBorrado) {
        RealizarCallBack(sb.ToString(), "");
    }
    else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);

}

function eliminarCertificados() {
    try {
        var idExpProf = -1;
        for (var x = $I("tblDatosExamenCert").rows.length - 1; x >= 0; x--) {
            if ($I("tblDatosExamenCert").rows[x].className == "FS") {
                $I("tblDatosExamenCert").deleteRow(x);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el certificado", e.message);
    }
}

//Solicitar certificado
function solicitarCert() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/mantCertificado/Solicitud/Default.aspx?t=" + codpar("C");
        modalDialog.Show(sPantalla, self, sSize(700, 600))
            .then(function (ret) {
               
                    window.focus();
                
                
            });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al solicitar nuevo certificado", e.message);
    }
}

//Solicitar examen
function solicitarExamen() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/mantCertificado/SolicitudExamen/Default.aspx?t=" + codpar("E");
        modalDialog.Show(sPantalla, self, sSize(700, 500))
            .then(function (ret) {
               
                    window.focus();
                
            });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al solicitar nuevo examen", e.message);
    }
}

//Renovación de examen
function renovarExamen() {    
    try {

        var bHayBorrado = false;
        var fila;
        for (var i = $I("tblDatosExamen").rows.length - 1; i >= 0; i--) {
            if ($I("tblDatosExamen").rows[i].className == "FS") {
                bHayBorrado = true;
                fila = $I("tblDatosExamen").rows[i];
            }
        }
        if (!bHayBorrado) {
            mmoff("War", "Debes seleccionar algún elemento para renovar", 330);
            return;
        }
                
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantCertificado/RenovarExamen/Default.aspx?iE=" + codpar(fila.getAttribute("id"))//t583_idexamen
                                    + "&nm=" + codpar(fila.cells[1].children[0].innerText)
                                    + "&iF=" + codpar(fila.getAttribute("f"))
                                    + "&eE=" + codpar(fila.getAttribute("estado"));

        modalDialog.Show(strEnlace, self, sSize(500, 260))
        .then(function (ret) {
            if (ret == "OK") {
                aceptar();
            }
            else {
                window.focus();
            }
        });        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al renovar el examen.", e.message);
    }
    
}

function renovarCert() {
    try {
        var fila;
        var bHayBorrado = false;
        for (var i = $I("tblDatosExamenCert").rows.length - 1; i >= 0; i--) {
            if ($I("tblDatosExamenCert").rows[i].className == "FS") {
                fila = $I("tblDatosExamenCert").rows[i];
                bHayBorrado = true;
            }
        }
        if (!bHayBorrado) {
            mmoff("War", "Debes seleccionar algún elemento para renovar", 330);
            return;
        }
       
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantCertificado/RenovarCert/Default.aspx?iE=" + codpar(fila.getAttribute("id"))//t583_idexamen
                                    + "&nm=" + codpar(fila.cells[2].children[0].innerText)
                                    + "&iF=" + codpar($I("hdnIdficepi").value)
                                    + "&eE=" + codpar(fila.getAttribute("estado"));

        modalDialog.Show(strEnlace, self, sSize(500, 260))
        .then(function (ret) {
            if (ret == "OK") {
                aceptar();
            }
            else {
                window.focus();
            }
        });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al renovar el certificado.", e.message);
    }

}


//function abrirDetalleCertificado(id, caso) {
function abrirDetalleCertificado(id) {
    try {
        mostrarProcesando();

        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/mantCertificado/Default.aspx?iC=" + codpar(id) + 
                                "&eA=" + codpar($I("hdnEsEncargado").value) +
                                "&iF=" + codpar($I("hdnIdficepi").value) +
                                "&n=" + codpar($I("nombre").innerText);

        modalDialog.Show(sPantalla, self, sSize(500, 500))
            .then(function(ret) {
                if (ret != null) {
                    selectedId = ret.id;
                    RealizarCallBack("reCargarCertExam", "");
                }
            });
        window.focus();
        
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el detalle del certificado", e.message);
    }

}

function verDOC(tipo,id,ficepi) {
    try {
        if (tipo == "CVTCERT" || tipo == "CVTEXAMEN") {
            id = id + "datos" + ficepi;
            descargar(tipo, id);
        }
        
    } catch (e) {
        mostrarErrorAplicacion("Error en la función ver documento", e.message);
    }
}
/*Experiencia profesional en Ibermatica*/
function borrarExpIber() {
    if (borradoYaSolicitado("tblExpProfIber")) {
        ocultarProcesando();
        mmoff("Inf", "El elemento ya tiene una solicitud de borrado realizada.", 400);
        return;
    }
    var aFilas = FilasDe("tblExpProfIber");
    
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarExpIber@#@" + $I("hdnIdficepi").value + "@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            if (aFilas[x].getAttribute("tipo") == "NL") {
                bHayBorrado = true;
                sb.Append(aFilas[x].getAttribute("id") + "##");
            }
            else{
                //mmoff("War", "No puedes eliminar una experiencia ligada a un proyecto SUPER", 350);
                PedirBorrado("tblExpProfIber", aFilas[x]);
                return;
            }
        }
    }
    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);

}
function eliminarExpIber() {
    try {
        var idExpProf = -1;
        for (var x = $I("tblExpProfIber").rows.length - 1; x >= 0; x--) {
            if ($I("tblExpProfIber").rows[x].className == "FS") {
//                idExpProf = $I("tblExpProfIber").rows[x].getAttribute("id");
//                for (var j = $I("tblExpProfIber").rows.length - 1; j > x; j--) {
//                    if ($I("tblExpProfIber").rows[j].getAttribute("id") == idExpProf) {
//                        $I("tblExpProfIber").deleteRow(j);
//                    }
//                }
                $I("tblExpProfIber").deleteRow(x);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la experiencia profesional", e.message);
    }
}
function anadirExpIber() {
    //mostrarDetExpIber("-1", $I("hdnIdficepi").value, "-1", "-1", $I("hdnEsEncargado").value, "P");
    //mostrarDetExpIber("-1", $I("hdnIdficepi").value, $I("hdnEsEncargado").value, "B");
    mostrarDetExpIber("-1", $I("hdnIdficepi").value, $I("hdnEsEncargado").value, "B", "NL", null, null);
}
function mdExpIber(oFila) {
//    var ret = mostrarDetExpIber(oFila.getAttribute("id"), $I("hdnIdficepi").value, oFila.getAttribute("idPF"), oFila.getAttribute("idFP"),
//                                $I("hdnEsEncargado").value, oFila.getAttribute("est"));
    var ret = mostrarDetExpIber(oFila.getAttribute("id"), $I("hdnIdficepi").value, $I("hdnEsEncargado").value, oFila.getAttribute("est"), oFila.getAttribute("tipo"), oFila.getAttribute("pr"), oFila.getAttribute("idPF"));
}
//function mostrarDetExpIber(idExpProf, idProf, idExpProfFicepi, idExpFicepiPerfil, sAdmin, sEstado) {
function mostrarDetExpIber(idExpProf, idProf, sAdmin, sEstado, sTipo, iProye, idExpProfFicepi) {
    try {
        mostrarProcesando();
        var strEnlace;
        if (sTipo == "NL")
            strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/EnIbermatica/Default.aspx?iE=" + codpar(idExpProf) + "&iF=" + codpar(idProf) + "&eA=" + sAdmin + "&e=" + sEstado + "&iEF=" + codpar(idExpProfFicepi);
        else
            strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/EnIbermatica/Default.aspx?iE=" + codpar(idExpProf) + "&iF=" + codpar(idProf) + "&eA=" + sAdmin + "&e=" + sEstado + "&pr=" + codpar(iProye) + "&iEF=" + codpar(idExpProfFicepi) + "&n=" + codpar($I("nombre").innerText);

        modalDialog.Show(strEnlace, self, sSize(970, 678))
            .then(function(ret) {
                if (ret != null) {
                    //Refresco con las posibles modificaciones
                    selectedId = ret.id;
                    RealizarCallBack("cargarExpIb", "");
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir/modificar la experiencia profesional", e.message);
    }
}
function borrarFormacionAcademica() {
    var aFilas = FilasDe("tblDatosTitulacion");
  
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarForAcad@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            bHayBorrado = true;
            sb.Append(aFilas[x].getAttribute("id") + "##");
        }
    }
    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);
}


function borrarIdiomas() {
    var aFilas = FilasDe("tblDatosIdiomas");
    if (aFilas.length == 0) 
    {
        mmoff("War", "No hay filas para borrar", 300); 
        return;
    }
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarIdioma@#@" + $I("hdnIdficepi").value + "@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            bHayBorrado = true;
            var aux = aFilas[x].getAttribute("id").split("//");
            sb.Append(aFilas[x].getAttribute("t") + "//" + aux[0] + "##");
        }
    }
    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);
}

function eliminarFilasTabla(sTabla) {
    try {
        for (var x = $I(sTabla).rows.length - 1; x >= 0; x--) {
            if ($I(sTabla).rows[x].className == "FS") {
                if (sTabla == "tblDatosIdiomas") {
                    if ($I("tblDatosIdiomas").rows[x].getAttribute("t")=="I") {
                        //Si estamos borrando un idioma, hay que borrar sus títulos
                        var aux = $I("tblDatosIdiomas").rows[x].getAttribute("id").split("//");
                        var sIdioma = aux[0];
                        for (var j = $I(sTabla).rows.length - 1; j > x; j--) {
                            if ($I("tblDatosIdiomas").rows[j].getAttribute("t") == "T") {
                                if ($I("tblDatosIdiomas").rows[j].getAttribute("id2") == sIdioma) {
                                    $I(sTabla).deleteRow(j);
                                }
                            }
                        }
                    }
                }
                $I(sTabla).deleteRow(x);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar fila del catálogo", e.message);
    }
}

/*Experiencia profesional fuera Ibermatica*/
function borrarExpNoIber() {
    var aFilas = FilasDe("tblExpProfNoIber");
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarExpNoIber@#@" + $I("hdnIdficepi").value + "@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            bHayBorrado = true;
            sb.Append(aFilas[x].getAttribute("id") + "##");
        }
    }
    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 430).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    }else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);

}
function eliminarExpNoIber() {
    try {
        var idExpProf = -1;
        for (var x = $I("tblExpProfNoIber").rows.length - 1; x >= 0; x--) {
            if ($I("tblExpProfNoIber").rows[x].className == "FS") {
                idExpProf = $I("tblExpProfNoIber").rows[x].getAttribute("id");
                for (var j = $I("tblExpProfNoIber").rows.length - 1; j > x; j--) {
                    if ($I("tblExpProfNoIber").rows[j].getAttribute("id") == idExpProf) {
                        $I("tblExpProfNoIber").deleteRow(j);
                    }
                }
                $I("tblExpProfNoIber").deleteRow(x);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la experiencia profesional", e.message);
    }
}
function anadirExpNoIber() {
    mostrarDetExpNoIber("-1", $I("hdnIdficepi").value, $I("hdnEsEncargado").value, "B","-1");
}
function mdExpNoIber(oFila) {
    var ret = mostrarDetExpNoIber(oFila.getAttribute("id"), $I("hdnIdficepi").value, $I("hdnEsEncargado").value, oFila.getAttribute("est"), oFila.getAttribute("idPF"));
}
function mostrarDetExpNoIber(idExpProf, idProf, sAdmin, sEstado, idExpProfFicepi) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/NoIbermatica/Default.aspx";
        strEnlace += "?ep=" + codpar(idExpProf) + "&idf=" + codpar(idProf);
        strEnlace += "&a=" + sAdmin;
        strEnlace += "&e=" + sEstado;
        strEnlace += "&iEF=" + codpar(idExpProfFicepi);
        modalDialog.Show(strEnlace, self, sSize(990, 670))
            .then(function(ret) {
                if (ret != null) {
                    //Refresco con las posibles modificaciones
                    selectedId = ret.id;
                    RealizarCallBack("cargarExpNoIb", "");
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir/modificar la experiencia profesional", e.message);
    }
}
/*Experiencia profesional fuera de Ibermatica*/

/*TITULACION*/
function AnadirTitulacion(idTitulacion, estado) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantTitulacion/Default.aspx?iT=" + codpar(idTitulacion) + "&iF=" + codpar($I("hdnIdficepi").value) + "&eA=" + codpar($I("hdnEsEncargado").value);
        modalDialog.Show(strEnlace, self, sSize(460, 490))
            .then(function(ret) {
                if (ret != null) {
                    //Refresco con las posibles modificaciones
                    //selectedId = ret.id;
                    selectedId = ret;
                    RealizarCallBack("cargarFormacionAcad", "");
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir titulación", e.message);
    }
}
/*TITULACION*/

/*CURSO*/
function AnadirCurso(idCurso, estado) {
    try {
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantCurso/Default.aspx?iC=" + codpar(idCurso) + "&iF=" + codpar($I("hdnIdficepi").value) + "&eA=" + codpar($I("hdnEsEncargado").value);
        modalDialog.Show(strEnlace, self, sSize(480, 540))
            .then(function(ret) {
                if (ret != null) {
                    //Refresco con las posibles modificaciones
                    selectedId = ret;
                    RealizarCallBack("cargarCursos", "");
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir curso", e.message);
    }
}
function PedirBorrado(tabla, oFila) {
    try {
        var sApartado = "", sElemento="", sKey="", sTipo="";
        switch (tabla) {
            case "tblDatosFormacionRecibida":
                sTipo = "FR";
                sApartado = "Formación recibida";
                sElemento = getCelda(oFila, 2);
                sKey = $I("hdnIdficepi").value + "##" + oFila.getAttribute("id");
                break;
            case "tblDatosFormacionImpartida":
                sTipo = "FI";
                sApartado = "Formación impartida";
                sElemento = getCelda(oFila, 2);
                sKey = oFila.getAttribute("id");
                break;
            case "tblExpProfIber":
                sTipo = "EI";
                sApartado = "Experiencia profesional en IBERMÁTICA";
                sElemento = getCelda(oFila, 1);
                sKey = oFila.getAttribute("idPF");
                break;
            case "tblDatosExamenCert":
                sTipo = "CE";
                sApartado = "Certificados";
                sElemento = getCelda(oFila, 2);
                sKey = $I("hdnIdficepi").value + "##" + oFila.getAttribute("id");
                break;
            case "tblDatosExamen":
                sTipo = "EX";
                sApartado = "Examen ";
                sElemento = getCelda(oFila, 1);
                sKey = oFila.getAttribute("f") + "##" + oFila.getAttribute("id");
                break;
        }
        //"&eA=" + codpar($I("hdnEsEncargado").value  
        if (sTipo != "") {
            var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/PetBorrado/Default.aspx?t=" + sTipo +
                        "&n=" + codpar($I("nombre").innerText) +
                        "&a=" + codpar(sApartado) +
                        "&e=" + codpar(sElemento) +
                        "&p=" + codpar(sIDFicepiEntrada) + //Usuario que pide el borrado
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
        oFila.cells[0].children[0].setAttribute("src", "../../../Images/imgPetBorrado.png");
        oFila.cells[0].children[0].setAttribute("title", "Pdte de eliminar");
    }
}
function borradoYaSolicitado(sTabla) {
    var bRes = false;
    for (var x = $I(sTabla).rows.length - 1; x >= 0; x--) {
        if ($I(sTabla).rows[x].className == "FS") {
            if ($I(sTabla).rows[x].cells[0].getElementsByTagName("img").length != 0) {
                var cadena = $I(sTabla).rows[x].cells[0].children[0].getAttribute("src");
                intPos = cadena.indexOf("imgPetBorrado.png");
                if (intPos >= 0) {
                    bRes = true;
                }
            }
            break;
        }
    }
    return bRes;
}
function borrarFormacionRecibida() {
    if (borradoYaSolicitado("tblDatosFormacionRecibida")) {
        ocultarProcesando();
        mmoff("Inf", "El elemento ya tiene una solicitud de borrado realizada.", 400);
        return;
    }
    var aFilas = FilasDe("tblDatosFormacionRecibida");
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarForRec@#@" + $I("hdnIdficepi").value + "@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            if (aFilas[x].getAttribute("ori") == "3") {
                bHayBorrado = true;
                sb.Append(aFilas[x].getAttribute("id") + "##");
            }
            else {
                //mmoff("War", "No puedes eliminar el registro directamente del CV al consistir en una acción formativa proveniente de En Forma.\n\nSi dese aliminarla de su CV y de su cuenta de En Forma, solicítelo a RRHH.", 350);
                PedirBorrado("tblDatosFormacionRecibida", aFilas[x]);
                return;
            }
        }
    }
    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);

}
//function eliminarFormacionRecibida() {
//    try {
//        for (var x = $I("tblDatosFormacionRecibida").rows.length - 1; x >= 0; x--) {
//            if ($I("tblDatosFormacionRecibida").rows[x].className == "FS") {
//                $I("tblDatosFormacionRecibida").deleteRow(x);
//            }
//        }
//    } catch (e) {
//        mostrarErrorAplicacion("Error al borrar la formación recibida", e.message);
//    }
//}
function borrarExamenes() {
    if (borradoYaSolicitado("tblDatosExamen")) {
        ocultarProcesando();
        mmoff("Inf", "El elemento ya tiene una solicitud de borrado realizada.", 400);
        return;
    }
    var aFilas = FilasDe("tblDatosExamen");
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarExamen@#@" + $I("hdnIdficepi").value + "@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            //Solo permito borrar examenes cuyo origen sea CVT (lo ha metido el usuario) y no está Validado
            if (aFilas[x].getAttribute("ori") == "1") {
                if (aFilas[x].getAttribute("est") == "V") {
                    PedirBorrado("tblDatosExamen", aFilas[x]);
                    return;
                }
                else {
                    bHayBorrado = true;
                    sb.Append(aFilas[x].getAttribute("id") + "##");
                }
            }
            else {
                //mmoff("War", "No puedes eliminar el registro directamente del CV al consistir en una acción formativa proveniente de En Forma.\n\nSi dese aliminarla de su CV y de su cuenta de En Forma, solicítelo a RRHH.", 350);
                PedirBorrado("tblDatosExamen", aFilas[x]);
                return;
            }
        }
    }
    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);

}

function AnadirCursoImpartido(idCurso, estado) {
    try {
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantCursoImp/Default.aspx?iC=" + codpar(idCurso) + "&iF=" + codpar($I("hdnIdficepi").value) + "&eA=" + codpar($I("hdnEsEncargado").value);
        modalDialog.Show(strEnlace, self, sSize(480, 540))
            .then(function(ret) {
                if (ret != null) {
                    //Refresco con las posibles modificaciones
                    //selectedId = ret.id;
                    selectedId = ret;
                    RealizarCallBack("cargarCursosImpartidos", "");
                }
            });
        window.focus();
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir curso impartido", e.message);
    }
}
function borrarFormacionImpartida() {
    if (borradoYaSolicitado("tblDatosFormacionImpartida")) {
        ocultarProcesando();
        mmoff("Inf", "El elemento ya tiene una solicitud de borrado realizada.", 400);
        return;
    }
    var aFilas = FilasDe("tblDatosFormacionImpartida");    
    var sb = new StringBuilder;
    var bHayBorrado = false;
    sb.Append("borrarForImp@#@");
    for (var x = aFilas.length - 1; x >= 0; x--) {
        if (aFilas[x].className == "FS" && aFilas[x].style.display != "none") {
            if (aFilas[x].getAttribute("ori") == "3") {
                bHayBorrado = true;
                sb.Append(aFilas[x].getAttribute("id") + "##");
            }
            else {
                //mmoff("War", "No puedes eliminar el registro directamente del CV al consistir en una acción formativa proveniente de En Forma.\n\nSi dese aliminarla de su CV y de su cuenta de En Forma, solicítelo a RRHH.", 350);
                PedirBorrado("tblDatosFormacionImpartida", aFilas[x]);
                return;
            }
        }
    }
    if (bHayBorrado) {
        ocultarProcesando();
        jqConfirm("", "Se procederá al borrado de los elementos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } else
        mmoff("War", "Debes seleccionar algún elemento para borrar", 330);

}
function eliminarFormacionImpartida() {
    try {
        for (var x = $I("tblDatosFormacionImpartida").rows.length - 1; x >= 0; x--) {
            if ($I("tblDatosFormacionImpartida").rows[x].className == "FS") {
                $I("tblDatosFormacionImpartida").deleteRow(x);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la formación impartida", e.message);
    }
}
/*CURSO*/



/*IDIOMAS*/
function mantIdioma(ficepi, idioma) {
try{
    mostrarProcesando();

    var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantIdioma/Default.aspx?ficepi=" + codpar(ficepi) + "&idioma=" + codpar(idioma) + "&esEnc=" + codpar($I("hdnEsEncargado").value);
    modalDialog.Show(strEnlace, self, sSize(560, 560))
            .then(function(ret) {
            if (ret != null) {
                //idioma ya grabado.RRHH tendra que confirmar el Idioma en enForma
                selectedId = ret.id;
                RealizarCallBack("cargarIdiomas", "");
            }
        });
    window.focus();
    
    ocultarProcesando(); 
}
catch (e) {
        mostrarErrorAplicacion("Error en la función mantIdioma", e.message);
    }
}

function AnadirTitulo(idioma, estado, idtitulo) {
    try{
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantTitulo/Default.aspx?iI=" + codpar(idioma) + "&iT=" + codpar(idtitulo) + "&pantalla=" + codpar("frmDatos") + "&eA=" + codpar($I("hdnEsEncargado").value) + "&iF=" + codpar($I("hdnIdficepi").value);
        modalDialog.Show(strEnlace, self, sSize(460, 400))
            .then(function(ret) {
                if (ret != null) {
                    //idioma ya grabado.RRHH tendra que confirmar el Idioma en enForma
                    selectedId = ret;
                    RealizarCallBack("cargarIdiomas", "");
                }
            });
        window.focus();
        
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir idioma", e.message);
    }
}

/*IDIOMAS*/
function grabar() {
    try {
        mostrarProcesando();          
        var sb = new StringBuilder; //sin paréntesis por ser versión JavaScript
        sb.Append("grabar@#@");
        sb.Append("U@#@");
        sb.Append($I("cboMovilidad").value + "@#@");
        if ($I('rdlInternacional_0').checked == true)
            sb.Append("true@#@");
        else if ($I('rdlInternacional_1').checked == true)
            sb.Append("false@#@");
        else
            sb.Append("@#@");
        sb.Append($I("txtObserva").value + "@#@");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}
var oCriterios = {};
var aDatosVacio = [];
function exportar() {
    try {
            mostrarProcesando();
            var sPantalla = strServer + "Capa_Presentacion/CVT/MiCV/formaExport/Default.aspx";
            modalDialog.Show(sPantalla, self, sSize(350, 120))
            .then(function(ret) {
                if (ret != null) {
                    oCriterios.bfiltros = false;
                    oCriterios.tipo = null;
                    oCriterios.estado = null;

                    oCriterios.idFicepis = $I("hdnIdficepi").value.split(",");

                    oCriterios.bSinopsis = true;
                    oCriterios.bFA = true; oCriterios.bEPI = true; oCriterios.bEPF = true; oCriterios.bCR = true;
                    oCriterios.bCI = true; oCriterios.bCERT = true; oCriterios.bID = true; oCriterios.bEX = true;

                    oCriterios.CR = null;
                    oCriterios.SN1 = null;
                    oCriterios.SN2 = null;
                    oCriterios.SN3 = null;
                    oCriterios.SN4 = null;
                    oCriterios.centro = null;
                    oCriterios.movilidad = null;
                    oCriterios.inttrayint = null;
                    oCriterios.gradodisp = null;
                    oCriterios.limcoste = null;

                    //oCriterios.profesionales = getDenominacionesInput($I("hdnProfesional").value);
                    oCriterios.perfiles = aDatosVacio;

                    //Titulación
                    oCriterios.tipologia = null;
                    oCriterios.tics = null;
                    oCriterios.modalidad = null;
                    oCriterios.titulo_obl_cod = aDatosVacio;
                    oCriterios.titulo_obl_den = aDatosVacio;
                    oCriterios.titulo_opc_cod = aDatosVacio;
                    oCriterios.titulo_opc_den = aDatosVacio;
                    //Idiomas
                    oCriterios.idioma_obl_cod = aDatosVacio;
                    oCriterios.idioma_obl_den = aDatosVacio
                    oCriterios.idioma_opc_cod = aDatosVacio;
                    oCriterios.idioma_opc_den = aDatosVacio
                    //Formación
                    oCriterios.num_horas = null;
                    oCriterios.anno = null;
                    ////Certificados
                    oCriterios.cert_obl_cod = aDatosVacio;
                    oCriterios.cert_obl_den = aDatosVacio;
                    oCriterios.cert_opc_cod = aDatosVacio;
                    oCriterios.cert_opc_den = aDatosVacio;
                    ////Entidades certificadoras
                    oCriterios.entcert_obl_cod = aDatosVacio;
                    oCriterios.entcert_obl_den = aDatosVacio;
                    oCriterios.entcert_opc_cod = aDatosVacio;
                    oCriterios.entcert_opc_den = aDatosVacio;
                    ////Entornos tecnológicos de la formación
                    oCriterios.entfor_obl_cod = aDatosVacio;
                    oCriterios.entfor_obl_den = aDatosVacio;
                    oCriterios.entfor_opc_cod = aDatosVacio;
                    oCriterios.entfor_opc_den = aDatosVacio;
                    ////Cursos
                    oCriterios.curso_obl_cod = aDatosVacio;
                    oCriterios.curso_obl_den = aDatosVacio;
                    oCriterios.curso_opc_cod = aDatosVacio;
                    oCriterios.curso_opc_den = aDatosVacio;

                    ////Experiencias profesionales
                    //Cliente / Sector
                    // Comentado 09/10/2013 por Andoni. No hay que enviar idcuenta, ya que puede ser cliente o cuentacvt (Ej: ISBAN).
                    //Hay que buscar por la denominación
                    oCriterios.cliente = "";
                    oCriterios.sector = null;
                    oCriterios.cantidad_expprof = null;
                    oCriterios.unidad_expprof = null;
                    oCriterios.anno_expprof = null;
                    //Contenido de Experiencias / Funciones
                    oCriterios.term_expfun = aDatosVacio;
                    oCriterios.op_logico = "A";
                    oCriterios.unidad_expfun = null;
                    oCriterios.cantidad_expfun = null;
                    oCriterios.anno_expfun = null;
                    //Experiencia profesional Perfil
                    oCriterios.tbl_bus_perfil = aDatosVacio;
                    //Experiencia profesional Perfil / Entorno tecnológico
                    oCriterios.tbl_bus_perfil_entorno = aDatosVacio;
                    //Experiencia profesional Entorno tecnológico
                    oCriterios.tbl_bus_entorno = aDatosVacio;
                    //Experiencia profesional Entorno tecnológico / Perfil
                    oCriterios.tbl_bus_entorno_perfil = aDatosVacio;

                    $I("hdnCriterios").value = JSON.stringify(oCriterios);

                    token = new Date().getTime();   //use the current timestamp as the token value
                    //var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/formaExport/exportarCVBasica.aspx";
                    var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/formaExport/exportarCV.aspx";
                    strEnlace += "?peticion=MICV";
                    $I("rdbFormato").value = ret;
                    $I("hdnIdficepis").value = $I("hdnIdficepi").value;
                    $I("hdnNombreProfesionales").value = $I("nombre").innerText;
                    strEnlace += "&descargaToken=" + token;
                    initDownload();

                    document.forms["aspnetForm"].action = strEnlace;
                    document.forms["aspnetForm"].target = "iFrmSubida";
                    document.forms["aspnetForm"].submit();
                    document.forms["aspnetForm"].action = strAction;
                    document.forms["aspnetForm"].target = strTarget;
                }
                else
                    ocultarProcesando();
            });
            window.focus();
            //setTimeout("ocultarProcesando()",2000);
        } catch (e) {
            mostrarErrorAplicacion("Error al exportar", e.message);
    }
}

function finishDownload() {
    window.clearInterval(fileDownloadCheckTimer);
    expireCookie('fileDownloadToken');
    ocultarProcesando();
    if ($I("hdnErrores").value != "") {
        mostrarErrores();
        $I("hdnErrores").value = "";
    }
}


var tsPestanas = null;
var tsPestanasFor = null;
var tsPestanasExp = null;
var tsPestanasAcciones = null;
var tsPestanasCertExam = null;

function CrearPestanas() {
    try {
        //alert("entra CrearPestanasCliente");
        var aTablas = $I("tsPestanas").getElementsByTagName("table");
        for (var i = 0; i < aTablas.length; i++)
            aTablas[i].style.tableLayout = "auto";

        if (typeof (setMenu) == "function") { //maestra
            tsPestanas = EO1021.r._o_ctl00_CPHC_tsPestanas;
        } else {//modal
            tsPestanas = EO1021.r._o_tsPestanas;
        }

        var oImg = document.createElement("img");
        oImg.id = "imgWarningFormacion";
        oImg.className = "warning";
        oImg.src = strServer + "images/imgSeparador.gif";
        oImg.title = "";
        aTablas[3].getElementsByTagName("TD")[1].id = "tdFormacion";
        $I("tdFormacion").style.paddingTop = "3px";
        $I("tdFormacion").appendChild(oImg);

        var oImg2 = document.createElement("img");
        oImg2.id = "imgWarningExperiencia";
        oImg2.className = "warning";
        oImg2.src = strServer + "images/imgSeparador.gif";
        oImg2.title = "";
        aTablas[4].getElementsByTagName("TD")[1].id = "tdExperiencia";
        $I("tdExperiencia").style.paddingTop = "3px";
        $I("tdExperiencia").appendChild(oImg2);

    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}

function CrearPestanasFor() {
    try {
        //alert("entra CrearPestanasCliente");
        var aTablas = $I("tsPestanasFor").getElementsByTagName("table");
        for (var i = 0; i < aTablas.length; i++)
            aTablas[i].style.tableLayout = "auto";

        if (typeof (setMenu) == "function") { //maestra
            tsPestanasFor = EO1021.r._o_ctl00_CPHC_tsPestanasFor;
        } else {//modal
            tsPestanasFor = EO1021.r._o_tsPestanasFor;
        }

        var oImg = document.createElement("img");
        oImg.id = "imgWarningAcademica";
        oImg.className = "warning";
        oImg.src = strServer + "images/imgSeparador.gif";
        aTablas[2].getElementsByTagName("TD")[1].id = "tdAcademica";
        $I("tdAcademica").style.paddingTop = "3px";
        $I("tdAcademica").appendChild(oImg);
        
        var oImg2 = document.createElement("img");
        oImg2.id = "imgWarningAcciones";
        oImg2.className = "warning";
        oImg2.src = strServer + "images/imgSeparador.gif";
        aTablas[3].getElementsByTagName("TD")[1].id = "tdAcciones";
        $I("tdAcciones").style.paddingTop = "3px";
        $I("tdAcciones").appendChild(oImg2);

        var oImg3 = document.createElement("img");
        oImg3.id = "imgWarningCertExam";
        oImg3.className = "warning";
        oImg3.src = strServer + "images/imgSeparador.gif";
        aTablas[4].getElementsByTagName("TD")[1].id = "tdCertExam";
        $I("tdCertExam").style.paddingTop = "3px";
        $I("tdCertExam").appendChild(oImg3);

        var oImg4 = document.createElement("img");
        oImg4.id = "imgWarningIdiomas";
        oImg4.className = "warning";
        oImg4.src = strServer + "images/imgSeparador.gif";
        aTablas[5].getElementsByTagName("TD")[1].id = "tdIdiomas";
        $I("tdIdiomas").style.paddingTop = "3px";
        $I("tdIdiomas").appendChild(oImg4);

        

    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas de Formación.", e.message);
    }
}

function CrearPestanasAcciones() {
    try {
        //alert("entra CrearPestanasCliente");
        var aTablas = $I("tsPestanasAcciones").getElementsByTagName("table");
        for (var i = 0; i < aTablas.length; i++)
            aTablas[i].style.tableLayout = "auto";

        if (typeof (setMenu) == "function") { //maestra
            tsPestanasAcciones = EO1021.r._o_ctl00_CPHC_tsPestanasAcciones;
        } else {//modal
            tsPestanasAcciones = EO1021.r._o_tsPestanasAcciones;
        }

        var oImg = document.createElement("img");
        oImg.id = "imgWarningAccionesRecibidas";
        oImg.className = "warning";
        oImg.src = strServer + "images/imgSeparador.gif";
        oImg.title = "";
        aTablas[2].getElementsByTagName("TD")[1].id = "tdAccionesRecibidas";
        $I("tdAccionesRecibidas").style.paddingTop = "3px";
        $I("tdAccionesRecibidas").appendChild(oImg);

        var oImg2 = document.createElement("img");
        oImg2.id = "imgWarningAccionesImpartidas";
        oImg2.className = "warning";
        oImg2.src = strServer + "images/imgSeparador.gif";
        oImg2.title = "";
        aTablas[3].getElementsByTagName("TD")[1].id = "tdAccionesImpartidas";
        $I("tdAccionesImpartidas").style.paddingTop = "3px";
        $I("tdAccionesImpartidas").appendChild(oImg2);
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}

function CrearPestanasExp() {
    try {
        //alert("entra CrearPestanasCliente");
        var aTablas = $I("tsPestanasExp").getElementsByTagName("table");
        for (var i = 0; i < aTablas.length; i++)
            aTablas[i].style.tableLayout = "auto";

        if (typeof (setMenu) == "function") { //maestra
            tsPestanasExp = EO1021.r._o_ctl00_CPHC_tsPestanasExp;
        } else {//modal
            tsPestanasExp = EO1021.r._o_tsPestanasExp;
        }

        var oImg = document.createElement("img");
        oImg.id = "imgWarningEnIbermatica";
        oImg.className = "warning";
        oImg.src = strServer + "images/imgSeparador.gif";
        aTablas[2].getElementsByTagName("TD")[1].id = "tdEnIbermatica";
        $I("tdEnIbermatica").style.paddingTop = "3px";
        $I("tdEnIbermatica").appendChild(oImg);

        var oImg2 = document.createElement("img");
        oImg2.id = "imgWarningFueraIbermatica";
        oImg2.className = "warning";
        oImg2.src = strServer + "images/imgSeparador.gif";
        aTablas[3].getElementsByTagName("TD")[1].id = "tdFueraIbermatica";
        $I("tdFueraIbermatica").style.paddingTop = "3px";
        $I("tdFueraIbermatica").appendChild(oImg2);
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas de Experiencia profesional.", e.message);
    }
}
function CrearPestanasCertExam() {
    try {
        //alert("entra CrearPestanasCertExam");
        var aTablas = $I("tsPestanaCertExam").getElementsByTagName("table");
        for (var i = 0; i < aTablas.length; i++)
            aTablas[i].style.tableLayout = "auto";

        if (typeof (setMenu) == "function") { //maestra
            tsPestanaCertExam = EO1021.r._o_ctl00_CPHC_tsPestanaCertExam;
        } else {//modal
        tsPestanaCertExam = EO1021.r._o_tsPestanaCertExam;
        }

        var oImg = document.createElement("img");
        oImg.id = "imgWarningCertificados";
        oImg.className = "warning";
        oImg.src = strServer + "images/imgSeparador.gif";
        oImg.title = "";
        aTablas[2].getElementsByTagName("TD")[1].id = "tdCertificados";
        $I("tdCertificados").style.paddingTop = "3px";
        $I("tdCertificados").appendChild(oImg);

        var oImg2 = document.createElement("img");
        oImg2.id = "imgWarningExamenes";
        oImg2.className = "warning";
        oImg2.src = strServer + "images/imgSeparador.gif";
        oImg2.title = "";
        aTablas[3].getElementsByTagName("TD")[1].id = "tdExamenes";
        $I("tdExamenes").style.paddingTop = "3px";
        $I("tdExamenes").appendChild(oImg2);
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestaña Certificados y Exámenes.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    switch (eventInfo.getItem().getIndex()) {
                        case 1: cargar('cargarFormacionAcad'); break; //Formación
                        case 2: cargar('cargarExpIb'); break; //Experiencia
                        case 3: cargar('cargarSinopsis'); break; //sinopsis
                    }
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
            case "ctl00_CPHC_tsPestanasFor":
            case "tsPestanasFor":
                if (!aPestFor[eventInfo.getItem().getIndex()].bLeido) {
                    switch (eventInfo.getItem().getIndex()) {
                        case 1: cargar('cargarCursos'); break; //Acciones
                        case 2: cargar('cargarCertExam'); break; //Certificados
                        case 3: cargar('cargarIdiomas'); break; //Idiomas
                        
                    }
                }
                break;
            case "ctl00_CPHC_tsPestanasAcciones":
            case "tsPestanasAcciones":
                if (!aPestAcc[eventInfo.getItem().getIndex()].bLeido) {
                    switch (eventInfo.getItem().getIndex()) {
                        case 1: cargar('cargarCursosImpartidos'); break; //Acciones impartidas 
                    }
                }
                break;
            case "ctl00_CPHC_tsPestanasExp":
            case "tsPestanasExp":
                if (!aPestExp[eventInfo.getItem().getIndex()].bLeido) {
                    switch (eventInfo.getItem().getIndex()) {
                        case 1: cargar('cargarExpNoIb'); break; //Fuera de Ibermática
                    }
                }
                break;
            case "ctl00_CPHC_tsPestanaCertExam":
            case "tsPestanaCertExam":
                if (!aPestCertExam[eventInfo.getItem().getIndex()].bLeido) {
                    switch (eventInfo.getItem().getIndex()) {
                        case 1: cargar('cargarExam'); break; //Exámenes
                    }
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

var aPestGral = new Array();
var aPestFor = new Array();
var aPestExp = new Array();
var aPestAcc = new Array();
var aPestCertExam = new Array();

function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function insertarPestanaEnArrayFor(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestFor[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array de Formación.", e.message);
    }
}
function insertarPestanaEnArrayExp(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestExp[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array de Experiencia.", e.message);
    }
}

function insertarPestanaEnArrayAcc(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestAcc[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array de Acciones.", e.message);
    }
}
function insertarPestanaEnArrayCertExam(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestCertExam[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array de Certificados y Exámenes.", e.message);
    }
}

function iniciarPestanas(){
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);

        for (var i = 0; i < tsPestanasFor.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayFor(i, false, false);

        for (var i = 0; i < tsPestanasExp.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayExp(i, false, false);

        for (var i = 0; i < tsPestanasAcciones.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayAcc(i, false, false);
            
        for (var i = 0; i < tsPestanaCertExam.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayCertExam(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}

function activarGrabar() {
    try {
        setOp($I("btnGrabar"), 100);
        bCambiosOb = true;
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        setOp($I("btnGrabar"), 30);
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
    }
}

function FinalizarCv() {
    try {
        $I("divFondoFinCv").style.visibility = "visible";
        $I("txtComentario").focus();
    } catch (e) {
    mostrarErrorAplicacion("Error al comunicar Finalización del Cv", e.message);
    }
}

function AceptarFinCv() {
    try {
        var js_args = "FinCV@#@" + $I("hdnIdficepi").value + "@#@" + Utilidades.escape($I("txtComentario").value);
        if ($I("chkCorreo").checked) {
            js_args += "@#@S";
            if ($I("txtComentario").value.Trim() == "") {
                mmoff("Inf", "Para enviar el correo es obligatorio indicar un comentario.", 400);
                return;
            }
        }
        else
            js_args += "@#@N";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar la obtención de motivo.", e.message);
    }
}
function CancelarFinCv() {
    try {
        $I("txtComentario").value = "";
        $I("divFondoFinCv").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar la obtención de motivo.", e.message);
    }
}

function clearSelection() {
    if (document.selection) {
        document.selection.empty();
    } else if (window.getSelection) {
        window.getSelection().removeAllRanges();
    }
}

function seleccionarFila(tabla) {
    try {
        maxIdTF = -1;
        idFila = -1;
        if (selectedId != -1) {
            clearSelection();
            if (tabla.getAttribute("id") == "tblDatosIdiomas") {
                for (i = 0; i < tabla.rows.length; i++) {
                    var aux = tabla.rows[i].id.split("//");
                    if (aux[0] == selectedId) {
                        ms(tabla.rows[i]);
                        return;
                    } //Si es nuevo, se seleccionará la fila que el id más nuevo, más alto(Idiomas)
                    else if (parseInt(aux[0]) > parseInt(maxIdTF)) {
                        maxIdTF = aux[0];
                        idFila = i;
                    }
                }
            }
            else {
                for (i = 0; i < tabla.rows.length; i++) {
                    if (tabla.rows[i].getAttribute("id") == selectedId) {
                        idFila = i;
                        ms(tabla.rows[i]);
                        return;
                    } //Si es nuevo, se seleccionará la fila que el id más nuevo, más alto
                    else {
                        try {//Este try es una prueba para ver si averguamos porque a veces casca esta función
                            if (parseInt(tabla.rows[i].getAttribute("id")) > parseInt(maxIdTF)) {
                                maxIdTF = tabla.rows[i].getAttribute("id");
                                idFila = i;
                            }
                        }
                        catch (e) {
                            maxIdTF = tabla.rows.length - 1;
                            idFila = maxIdTF;
                        }
                    }
                }
            }
            if (idFila != -1) 
                ms(tabla.rows[idFila]);
        }
    } catch (e) {
    mostrarErrorAplicacion("Error al seleccionar fila " + idFila + " en la tabla " + tabla + " de MiCV.", e.message);
    }
}
function mostrarPdtes(objChk){
    var sTabla = "";
    try {
        switch (objChk.id) {
            case "chkFor_For":
            case "ctl00_CPHC_chkFor_For":
                sTabla = "tblDatosTitulacion";
                break;
            case "chkFor_Acc_Rec":
            case "ctl00_CPHC_chkFor_Acc_Rec":
                sTabla = "tblDatosFormacionRecibida";
                break;
            case "chkFor_Acc_Imp":
            case "ctl00_CPHC_chkFor_Acc_Imp":
                sTabla = "tblDatosFormacionImpartida";
                break;
            case "chkExpDentro":
            case "ctl00_CPHC_chkExpDentro":
                sTabla = "tblExpProfIber";
                break;
            case "chkExpFuera":
            case "ctl00_CPHC_chkExpFuera":
                sTabla = "tblExpProfNoIber";
                break;
            case "chkFor_Cert":
            case "ctl00_CPHC_chkFor_Cert":
                sTabla = "tblDatosExamenCert";
                break;
            case "chkFor_Exam":
            case "ctl00_CPHC_chkFor_Exam":
                sTabla = "tblDatosExamen";
                break;
            case "chkFor_Idi":
            case "ctl00_CPHC_chkFor_Idi":
                sTabla = "tblDatosIdiomas";
                break;
        }
        if (sTabla != "") {
            mostrarFilasPdtes(sTabla, objChk.checked);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar pendientes.", e.message);
    }
}
function mostrarFilasPdtes(sTabla, bSoloPdtes) {
    var bMostrar = false;
    if (sTabla != "") {
        var aFilas = FilasDe(sTabla);
        if (aFilas == null) return;
        for (var i = 0; i < aFilas.length; i++) {
            if (bSoloPdtes) {
                if (aFilas[i].getAttribute("est") == "S" || aFilas[i].getAttribute("est") == "T" || aFilas[i].getAttribute("est") == null) {
                    bMostrar = true;
                    if (aFilas[i].getAttribute("idFicCert") == "0" && (!$I("chkFor_CertSug").checked))
                        bMostrar = false;
                    else bMostrar = true;
                } 
                else
                    bMostrar = false;
            }
            else 
            {
                    if (aFilas[i].getAttribute("idFicCert") == "0" && (!$I("chkFor_CertSug").checked)) 
                           bMostrar = false;
                    else bMostrar = true;

            }
                
            if (bMostrar)
                aFilas[i].style.display = "table-row";
            else
                aFilas[i].style.display = "none";
        }
    }
}
function mostrarCertificadosSugeridos(objChk) {
    var aFilas = FilasDe("tblDatosExamenCert");
    if (aFilas == null) return;
    for (var i = 0; i < aFilas.length; i++) {
        //if (aFilas[i].getAttribute("ori") == "0"){
        if (aFilas[i].getAttribute("idFicCert") == "0") {
            if (objChk.checked) {
                if ($I("chkFor_Cert").checked) {
                    if (aFilas[i].getAttribute("est") == "S" || aFilas[i].getAttribute("est") == "T" || aFilas[i].getAttribute("est") == null)
                        bMostrar = true;
                    else
                        bMostrar = false;
                }
                else
                    bMostrar = true;

                if (bMostrar)
                    aFilas[i].style.display = "table-row";
                else
                    aFilas[i].style.display = "none";

                //aFilas[i].style.display = "table-row";
            }
            else
                aFilas[i].style.display = "none";
        }
    }
    //mostrarPdtes($I('chkFor_Cert'));
}

function mostrarProfesionales() {
    try {
        location.href = "../MantCV/Default.aspx";
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar profesionales.", e.message);
    }
}
function RefrescarCatalogo() {
    try {
        setTimeout("mostrarCertificadosSugeridos($I('chkFor_CertSug'))", 100);
        setTimeout("mostrarPdtes($I('chkFor_Cert'))", 100);
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al refrecar el Catalogo.", e.message);
    }
}


function setCompletadoProf() {
    try {
        jqConfirm("", "Confirma por favor si has completado y enviado a validar todos los apartados de tu CV, y por tanto lo das por finalizado.", "", "", "war", 450).then(function (answer) {
            if (answer) {
                var sb = new StringBuilder;
                sb.Append("setCompletadoProf@#@" + $I("hdnIdficepi").value);
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al dar el CV por completado.", e.message);
    }
}

function setRevisadoActualizadoCV() {
    try {
        jqConfirm("", "Confirma por favor si has revisado y actualizado todos apartados de tu CV, y por tanto das por finalizada la revisión.", "", "", "war", 450).then(function (answer) {
            if (answer) {
                if (comprobarHayPdtesCumpli(sDatosPendientes)) {
                    mmoff("Inf", "Existen en el CV registros pendientes de cumplimentar.", 350);
                    return;
                }
                var sb = new StringBuilder;
                sb.Append("setRevisadoActualizadoCV@#@" + $I("hdnIdficepi").value);
                RealizarCallBack(sb.ToString(), "");
            }
            else return;
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al dar el CV por revisado y actualizado.", e.message);
    }
}
function comprobarHayPdtesCumpli(strDatos) {
    try {
        var aResul = strDatos.split("@##@");
        var bPendientes = false;
        for (var i = 0; i <= aResul.length - 1; i++) {
            if (aResul[i] != "") {
                var aArgs = aResul[i].split("#dato#");
                if (aArgs[2] == 'T') {
                    bPendientes = true;
                    break;
                }
            }
        }
        return bPendientes;  
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar el CV dado por revisado y actualizado.", e.message);
    }
}

////reescribimos la funcion mm a mmIdioma para no seleccionar las filas de la titulación

var nfi = 0; //número fila inicio
var nff = 0; //número fila fin
var nfo = 0; //número fila original
var nfs = 0; //número filas seleccionadas
/* marcar multiple sin estilos FA, FB */
var sTabla = "";
/* mm antes mmse_MN es la nueva función para marcado múltiple de filas*/
function mmIdioma(e)
{	
    try{ //alert("Entra mm");
        if (bLectura) return;
        if (!e) e = event;    
        var oElement = e.srcElement ? e.srcElement : e.target;
        
	    //var oFila = oElement.parentNode.parentNode;
	    
	    var bFila = false;
	    while (!bFila)
	    {
	        if (oElement.tagName.toUpperCase()=="TR") bFila = true;
	        else{
	            oElement = oElement.parentNode;
	            if (oElement==null)
	                return;
	        }
	    }
	    
	    var oFila = oElement;
	    	            
	    var oTabla = oFila.parentNode.parentNode;
	    if (sTabla != oTabla.id){
	        clearVarSel();
	        sTabla = oTabla.id;
	    }
	    
	    var nFila  = oFila.rowIndex;

	    var nMantenimiento = 0;
	    try{ nMantenimiento = oTabla.getAttribute("mantenimiento")}catch(e){}
	    //alert(event.ctrlKey);
	    //alert(event.shiftKey);
	    if (e.ctrlKey){  //Tecla control pulsada
            //document.selection.empty();
            try{
                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();
	        }catch(e){};
	        
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        if (i == nFila) break;
		    }
            
            if (oFila.className == "FS"){
                if (nfs > 1){
                    nfs--;
                    //oFila.setAttribute("class","");
                    oFila.className = "";
                }
            }else if (oFila.getAttribute("tipo") == "I"){
                    oFila.className = "FS";
                    nfs++;
                    iFila = oFila.rowIndex;
            }            
            
	        if (nMantenimiento == 1) modoControles(oFila, false);
	    }else if (e.shiftKey){	//Tecla shift pulsada
	        //if (nfs > 0) document.selection.empty();
	        if (nfs > 0)
	        {
	            try{
	                if (window.getSelection) window.getSelection().removeAllRanges();
                    else if (document.selection && document.selection.empty) document.selection.empty();
                }catch(e){};
	        }
		    var nff = nFila;
		    if (nfo > nff){
			    nff = nfo;
			    nfi = nFila;
		    }
		    if (nfi < nfo) nff = nfo;
		    if (nFila > nfo){
		        nfi = nfo
		        nff = nFila;
		    }

		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        
	            if (i >= nfi && i <= nff){	
			        if (oTabla.rows[i].className != "FS" && oTabla.rows[i].getAttribute("t") == "I"){
			            nfs++;
			            iFila = i;
				        oTabla.rows[i].className = "FS";		
			        }
		        }else{
		            if (oTabla.rows[i].className == "FS"){
		                nfs--;
                        oTabla.rows[i].className = "";	
                    }
		        }                
   			    
			    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
		    }
		    //alert("Control:\n\nnfo: "+ nfo +" nfi: "+ nfi +" nff: "+ nff);
	    }
	    else{  //teclas ni control ni shift pulsadas.
	        var j=0;
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
                if (i == nFila){
                    nfo = i;
                    nfi = i;
                    nff = i;

    	            if (oFila.className != "FS"){
                        nfs++;
                        iFila = i;
                        oFila.className = "FS";
                    }                             
	                
                    if (nMantenimiento == 1){
                        modoControles(oFila, true);
                        iFila = i;
                    }
			    }else{

	                if (oTabla.rows[i].className == "FS"){
	                    nfs--;
                        oTabla.rows[i].className = "";
                    }                    

                    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
                }
		    }
	    }
    
	}catch(e){
		mostrarErrorAplicacion("Error en la selección múltiple (mm)", e.message);
    }
}
function getVias(idCert, idFicepi) {
    try {
        mostrarProcesando();
        //paso idCertificado e idFicepi
        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/getVias.aspx?c=" + codpar(idCert) + "&f=" + codpar(idFicepi);
        modalDialog.Show(sPantalla, self, sSize(700, 600));
        window.focus();

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las vías del certificado", e.message);
    }

}
function verCert(idExam, idFicepi) {
    try {
        mostrarProcesando();
        //paso idCertificado e idFicepi
        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/getCertExam.aspx?e=" + codpar(idExam) + "&f=" + codpar(idFicepi);
        modalDialog.Show(sPantalla, self, sSize(700, 600));
        window.focus();

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las vías del certificado", e.message);
    }

}

//pestaña sinopsis
function grabarSinopsis(){
try{
    var sb = new StringBuilder;
    sb.Append("grabarS@#@"); 
//    if (bTieneSinopsis)  
//        sb.Append("U@#@");
//    else
//        sb.Append("I@#@");
    sb.Append($I("txtSinopsis").value);    
    RealizarCallBack(sb.ToString(), "");
    
}catch(e){
		mostrarErrorAplicacion("Error al grabar la sinpsis", e.message);
    }
}

function activarGrabarS(){
try{
    if (getOp($I("btnGrabarS")) != 100)
        setOp($I("btnGrabarS"), 100);
    bCambiosS = true;
    bCambios = true;
}catch(e){
		mostrarErrorAplicacion("Error al intentar activar el botón de grabar en la pestaña de sinopsis", e.message);
    }
}
function setObligatoriedadMotivo() {
    if ($I("chkCorreo").checked) {
        $I("lblCorreo").style.visibility = "visible";
    }
    else {
        $I("lblCorreo").style.visibility = "hidden";
    }
}