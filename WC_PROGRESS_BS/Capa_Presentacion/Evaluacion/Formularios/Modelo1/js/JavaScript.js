var bCambios = false;

$(document).ready(function () {
    comprobarerrores();
    try {
        //Click en el botón confirmar equipo 
        if (estadoEvaluacion == 0) {
            $('.evaluador,.evaluado').prop("disabled", true).find('input').prop("disabled", true);
        } else if (estadoEvaluacion == 1) {
            $('.evaluado').prop("disabled", true);
        } else if (estadoEvaluacion == 2) {
            $('.evaluador').prop("disabled", true).find('input').prop("disabled", true);
        }
    } catch (e) {
        alertNew("danger", "Ocurrió un error al iniciar la página.");
    }
});

$("#rdbEvalProyecto").on("click", function () {
    if ($("#rdbEvalProyecto").is(":checked") == true) {
        $("#divProyecto").fadeIn("slow");
        $("#divProyecto input[type=text]").focus();
    }
});

$("#rdbEvalDesempeno").on("click", function () {
    $("#divProyecto").fadeOut("slow");
    bCambios = true;
});

$("input[name=rdbActividad]").on("click", function () {
    bCambios = true;    
});

$("input[name=habilidades], input[name=habilidades2], input[name=habilidades3], input[name=habilidades4]").on("click", function () {
    bCambios = true;
});

$("input[name=valores], input[name=valores2], input[name=valores3]").on("click", function () {
    bCambios = true;
});

$("input[name=rdbEvolucion]").on("click", function () {
    bCambios = true;    
});

$("#selMesIni, #selAnoIni, #selMesFin, #selAnoFin").on("change", function () {
    bCambios = true;    
});

$("textarea").on("keyup", function () {
    bCambios = true;    
});


$("#pieFormulario #sinfirma").on("click", function () {//Faltan los diálogos
    if (estadoEvaluacion == 1) {//Es evaluador
        if (!comprobarfechas(false)) return;
        guardar(false, false);
    } else if (estadoEvaluacion == 2) {//Es evaluado
        $('#modal-evaluado-sinfirma').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#modal-evaluado-sinfirma').modal('show');
    }
});

$("#pieFormulario #firmado").on("click", function () {
    if (estadoEvaluacion == 1) {//Es evaluador
        if (!comprobar_datos()) return;
        $('#modal-evaluador').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#modal-evaluador').modal('show');
    } else if (estadoEvaluacion == 2) {//Es evaluado
        $('#modal-evaluado-confirma').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#modal-evaluado-confirma').modal('show');
    }
});



//Botón regresar
$("#regresar, #cancelar").on("click", function () {

    navegar();

    //if (origen == "noConsultas") {
    //    if (estadoEvaluacion == 0) {
    //        if (acceso == "formacion") {
    //            location.href = strServer + "Capa_Presentacion/Administracion/FormacionDemandada/Default.aspx?pt=Formularios";
    //            return;
    //        }

    //        if (acceso == "demiequipo") {
    //            location.href = strServer +"Capa_Presentacion/Evaluacion/DeMiEquipo/Default.aspx?pt=Formularios";
    //            return;
    //        }

    //        else {
    //            location.href = strServer +"Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
    //            return;
    //        }
            
    //    }
    //    if (estadoEvaluacion == 1) {//Llamada a catálogo de evaluaciones abiertas (Es evaluador)
    //        location.href = strServer +"Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
    //        return;
    //    } else if (estadoEvaluacion == 2) {
    //        location.href = strServer +"Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
    //        return;
    //    }
    //}
    //else {
    //    if (menu.toUpperCase() == "EVA") {
    //        location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=EVA&pt=Estandar";
    //        return;
    //    }
    //    else {
    //        location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=ADM&pt=Estandar";
    //        return;
    //    } 
        
    //}



    //if (origen == "noConsultas") {
    //    if (acceso == "formacion") {
    //        location.href = strServer + "Capa_Presentacion/Administracion/FormacionDemandada/Default.aspx?pt=Formularios";
    //        return;
    //    }

    //    else if (acceso == "demiequipo") {
    //        location.href = strServer + "Capa_Presentacion/Evaluacion/DeMiEquipo/Default.aspx?pt=Formularios";
    //        return;
    //    }

    //    else if (acceso == "misevaluaciones") {
    //        location.href = strServer + "Capa_Presentacion/Evaluacion/misevaluaciones/Default.aspx?pt=Formularios";
    //        return;
    //    }

    //    else if (acceso == "completarabiertas") {
    //        location.href = strServer + "Capa_Presentacion/Evaluacion/completarabiertas/Default.aspx?pt=Formularios";
    //        return;
    //    }
    //}

    //else {
    //    if (menu.toUpperCase() == "ADM") {
    //        location.href = strServer + "Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=ADM&pt=Estandar";
    //        return;
    //    }

    //}




});


function navegar() {
    if (origen == "noConsultas") {
        if (acceso == "formacion") {
            location.href = strServer + "Capa_Presentacion/Administracion/FormacionDemandada/Default.aspx?pt=Formularios";           
        }

        else if (acceso == "demiequipo") {
            location.href = strServer + "Capa_Presentacion/Evaluacion/DeMiEquipo/Default.aspx?pt=Formularios";
        }

        else if (acceso == "misevaluaciones") {
            location.href = strServer + "Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx?pt=Formularios";
        }

        else if (acceso == "completarabiertas") {
            location.href = strServer + "Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx?pt=Formularios";
           
        }
    }

    else {
        if (menu.toUpperCase() == "ADM") {
            location.href = strServer + "Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=ADM&pt=Estandar";
          
        }

    }
}



$("#modal-evaluador button#evaluadorms").on("click", function () { guardar(true, false); });
$("#modal-evaluado-sinfirma button#evaluadomc").on("click", function () { guardar(false, false); });
$("#modal-evaluado-confirma button#evaluadoms").on("click", function () { guardar(true, false); });

function comprobarfechas(firma) {
    try {
        $anoIni = $('#selAnoIni'); $mesIni = $('#selMesIni'); $anoFin = $('#selAnoFin'); $mesFin = $('#selMesFin');
        if (!firma) {//Comprobación previa a guardar sin firmar
            if ($anoIni.val() != '' && $mesIni.val() != '' && $anoFin.val() != '' && $mesFin.val() != '') {
                fecIni = parseInt($anoIni.val()) * 100 + parseInt($mesIni.val());
                fecFin = parseInt($anoFin.val()) * 100 + parseInt($mesFin.val());
                if ((fecIni > fecFin)) {
                    alertNew('warning', 'Periodo de evaluación no lógico.');
                    return false;
                } else {
                    return true;
                }
            } else if ($anoIni.val() != '' || $mesIni.val() != '' || $anoFin.val() != '' || $mesFin.val() != '') {
                alertNew('warning', 'Periodo de evaluación no lógico. O lo completas entero o lo dejas vacío.');
                return false;
            } else {
                return true;
            }
        } else {//Comprobación previa a guardar con firmar
            if ($anoIni.val() == '' || $mesIni.val() == '' || $anoFin.val() == '' || $mesFin.val() == '') {
                alertNew('warning', 'El periodo de evaluación es obligatorio.');
                return false;
            } else {
                fecIni = parseInt($anoIni.val()) * 100 + parseInt($mesIni.val());
                fecFin = parseInt($anoFin.val()) * 100 + parseInt($mesFin.val());
                fecNow = new Date();
                fecHoy = parseInt(fecNow.getFullYear()) * 100 + parseInt(fecNow.getMonth() + 1);
                if ((fecIni > fecFin)) {
                    alertNew('warning', 'Periodo de evaluación no lógico.');
                    return false;
                } else if ((fecHoy < fecFin)) {
                    alertNew('warning', 'Periodo de evaluación incorrecto. El periodo no puede exceder a la fecha de firma.', null, 3000, null);
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar las fechas.");
    }
};

function comprobar_datos() {
    try {
        var actividad = $('.evaluador input[name=rdbActividad]:checked').val();
        if (actividad == null || actividad == undefined) {
            alertNew('warning', 'Debes indicar el tipo de actividad del evaluado.');
            return false;
        }
        var objeval = $('.evaluador input[name=evaluacion]:checked').val();
        if (objeval == null || objeval == undefined) {
            alertNew('warning', 'Debes indicar el objeto de la evaluación.');
            return false;
        } else if (objeval == 1 && $('#sProyecto').val() == '') {
            alertNew('warning', 'Debes indicar el proyecto asociado a la evaluación.');
            return false;
        }
        if (!comprobarfechas(true)) return false;

        //Modificación pedida por RRHH [23/02/2017] (textarea 1 y 2 obligatorios ==> mínimo 125 caracteres)
        var aspectosAReconocer = $(".evaluador[name=sAreconocer]").val();
        if (aspectosAReconocer.trim().length <= 125) {
            alertNew('warning', 'Amplía la información sobre los aspectos a reconocer, será útil para ti y para el profesional.', null, 4000, null);
            alert(aspectosAReconocer.trim().length);
            return false;
        }

        var aspectosAMejorar = $(".evaluador[name=sAmejorar]").val();
        if (aspectosAMejorar.trim().length <= 125) {
            alertNew('warning', 'Amplía la información sobre los aspectos a mejorar, será útil para ti y para el profesional.', null, 4000, null);
            return false;
        }

        var habilidades = $('.evaluador input[name=habilidades]:checked').val();
        if (habilidades == null || habilidades == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la gestión de relación de clientes.');
            return false;
        }
        var habilidades2 = $('.evaluador input[name=habilidades2]:checked').val();
        if (habilidades2 == null || habilidades2 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre el liderazgo/gestión de equipo.');
            return false;
        }
        var habilidades3 = $('.evaluador input[name=habilidades3]:checked').val();
        if (habilidades3 == null || habilidades3 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la planificación/organización.');
            return false;
        }
        var habilidades4 = $('.evaluador input[name=habilidades4]:checked').val();
        if (habilidades4 == null || habilidades4 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre el expertise/técnico.');
            return false;
        }
        var valores = $('.evaluador input[name=valores]:checked').val();
        if (valores == null || valores == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la cooperación.');
            return false;
        }
        var valores2 = $('.evaluador input[name=valores2]:checked').val();
        if (valores2 == null || valores2 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la iniciativa.');
            return false;
        }
        var valores3 = $('.evaluador input[name=valores3]:checked').val();
        if (valores3 == null || valores3 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la perseverancia.');
            return false;
        }
        return true;
    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar los datos.");
    }
};

function guardar(firma, desdeImprimir) {
    try {
        var form_1 = new formulario_id1();
        var sMetodo = "firmarFinalizarEvaluador";
        //Rellenar campos propios de formulario
        form_1.t930_idvaloracion = idValoracion;
        form_1.t934_idmodeloformulario = 1;//El ID del formulario estandar es 1
        if (estadoEvaluacion == 1) {//Update del evaluador
            //Si ha firmado cargamos el campo firma con la fecha actual
            if (firma)
                form_1.t930_fecfirmaevaluador = new Date();
            //Rellenar campos propios de formulario estandar
            form_1.t930_denominacionROL = $('#sRol').val();
            form_1.t930_denominacionCR = $('#sCR').val();
            form_1.t930_objetoevaluacion = $('.evaluador input[name=evaluacion]:checked').val();
            form_1.t930_denominacionPROYECTO = $('#sProyecto').val();
            form_1.t930_anomes_ini = ($('#selAnoIni').val() != '') ? parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val()) : null;
            form_1.t930_anomes_fin = ($('#selAnoFin').val() != '') ? parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val()) : null;
            form_1.t930_actividad = $('.evaluador input[name=rdbActividad]:checked').val();
            form_1.t930_areconocer = $('#sAreconocer').val().trim();
            form_1.t930_amejorar = $('#sAmejorar').val().trim();
            form_1.t930_gescli = $('.evaluador input[name=habilidades]:checked').val();
            form_1.t930_liderazgo = $('.evaluador input[name=habilidades2]:checked').val();
            form_1.t930_planorga = $('.evaluador input[name=habilidades3]:checked').val();
            form_1.t930_exptecnico = $('.evaluador input[name=habilidades4]:checked').val();
            form_1.t930_cooperacion = $('.evaluador input[name=valores]:checked').val();
            form_1.t930_iniciativa = $('.evaluador input[name=valores2]:checked').val();
            form_1.t930_perseverancia = $('.evaluador input[name=valores3]:checked').val();
            form_1.t930_interesescar = $('.evaluador input[name=rdbEvolucion]:checked').val();
            form_1.t930_formacion = $('#sFormacion').val();
        } else if (estadoEvaluacion == 2) {//Update del evaluado
            //Si ha firmado cargamos el campo firma con la fecha actual
            if (firma)
                form_1.t930_fecfirmaevaluado = new Date();
            //Rellenar campos propios de formulario estandar
            form_1.t930_autoevaluacion = $('#sAutoevaluacion').val();;
            sMetodo = "firmarFinalizarEvaluado";
        }
    } catch (e) {
        alertNew("danger", "Ocurrió un error al capturar los datos para grabar.");
    }
    actualizarSession();
    $.ajax({
        url: "Default.aspx/" + sMetodo,   // Current Page, Method
        data: JSON.stringify({ form1: form_1 }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function () {

            if (!desdeImprimir) {

                navegar();

                //if (acceso == "completarabiertas") {
                //    location.href = strServer + "Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
                //}

                //else if (acceso == "demiequipo") {
                //    location.href = strServer + "Capa_Presentacion/Evaluacion/DeMiEquipo/Default.aspx?pt=Formularios";
                //}

                //else if (acceso == "misevaluaciones") {
                //    location.href = strServer + "Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx?pt=Formularios";
                //}

                //if (origen == "noConsultas") {
                //    if (estadoEvaluacion == 1) {//Llamada a catálogo de evaluaciones abiertas
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
                //    }

                //    else if (estadoEvaluacion == 2) {
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
                //    }
                //}

                //else {
                //    if (menu == "EVA") {
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=EVA";
                //    }
                //    else {
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=ADM";
                //    }

                //}
            }

            
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar modificar una evaluación");
        }
    });
};

//Modal selección de proyecto
$('#lblProyecto').on('click', function () {
    if (!comprobarfechas(true)) return;
    var anmsdesde = parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val());
    var anmshasta = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());
    $lisProyectos = $('#lisProyectos');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getProyectos",   // Current Page, Method
        data: JSON.stringify({ idficepi: idficepiEvaluado, anomesdesde: anmsdesde, anomeshasta: anmshasta }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item'>" + this + "</li>").appendTo($lisProyectos); });
                $('#modal-proyectos').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-proyectos').modal('show');
            } else {
                alertNew("warning", "En el periodo indicado, no consta que el evaluado haya colaborado en ningún proyecto");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los proyectos");
        }
    });
});

//Selección simple de proyectos
$('body').on('click', '#lisProyectos li', function (e) {
    $('#lisProyectos li').removeClass('active');
    $(this).addClass('active');
});

//Selección de proyecto
$('#modal-proyectos button').on('click', function () {
    $proyecto = $('#lisProyectos li.active');
    if ($proyecto.length > 0) {
        $('#sProyecto').val($proyecto.text());
        $('#modal-proyectos').modal('hide');

    } else {
        alertNew("warning", "Debes seleccionar un proyecto");
    }

});

/* MODALES */
$("#txtCualificacion").text("Pulsa “Confirmar” para cualificar la evaluación como válida.");
$("#txtCualificacionNoValida").text("Pulsa “Confirmar” para cualificar la evaluación como no válida.");
$("#txtDescualificar").text("Pulsa “Confirmar” para quitar la cualificación a la evaluación.");


$("#Cvalida").on("click", function () {
    $("#modal-ConfirmCualificacion").modal("show");
});

$("#CnoValida").on("click", function () {
    $("#modal-NOValida").modal("show");
});

$("#Descualificar").on("click", function () {
    $("#modal-Descualificar").modal("show");
});

$("#btnConfirmar").on("click", function () {
    Cualificar(true);
});

$("#btnConfirmarNoValida").on("click", function () {
    Cualificar(false);
});

$("#btnConfirmarDescualificar").on("click", function () {
    Cualificar(null);
});


function Cualificar(accion) {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/Cualificar",   // Current Page, Method
        data: JSON.stringify({ t930_idvaloracion: idValoracion, t930_puntuacion : accion }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-ConfirmCualificacion").modal("hide");

            if (menu.toUpperCase() == "EVA") location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=EVA&pt=Estandar"
            else location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=ADM&pt=Estandar";
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar cualificar la evaluación");
        }
    });
};

//Botón Imprimir
$("#Imprimir").on("click", function () {
    Imprimir();
});

function Imprimir() {
    if (bCambios) {
        $('#modal-datosmodificados').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $("#modal-datosmodificados").modal('show');
    }
    else { imprimirInforme(); }
};


$("#btnconfirm").on("click", function () {
    //grabar e imprimir
    guardar(false, true);
    imprimirInforme();
    $("#modal-datosmodificados").modal('hide');
});

$("#btnCancelar").on("click", function () {
    //sólo imprimir
    imprimirInforme();
    $("#modal-datosmodificados").modal('hide');
});

function imprimirInforme() {
    if (IdEvaluacion == "") return;

    var strUrlPag = "Informe/Default.aspx?IdEvaluacion=" + codpar(IdEvaluacion) + "&modelo=1";
    var bScroll = "no";
    var bMenu = "no";
    if (screen.width == 800) bScroll = "yes";
    bMenu = "yes";
    window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
};





//$("#cancelar").on("click", function () {

//    //if (origen == "noConsultas") {
//    //    if (estadoEvaluacion == 1) {//Llamada a catálogo de evaluaciones abiertas (Es evaluador)
//    //        location.href = strServer +"Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
//    //    } else if (estadoEvaluacion == 2) {
//    //        location.href = strServer +"Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
//    //    }
//    //}
//    //else {
//    //    location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=EVA";
//    //}
//    if (acceso == "formacion") {
//        location.href = strServer + "Capa_Presentacion/Administracion/FormacionDemandada/Default.aspx?pt=Formularios";
//    }

//    else if (acceso == "completarabiertas") {
//        location.href = strServer + "Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
//    }

//    else if (acceso == "demiequipo") {
//        location.href = strServer + "Capa_Presentacion/Evaluacion/DeMiEquipo/Default.aspx?pt=Formularios";
//    }

//    else if (acceso=="misevaluaciones") {
//        location.href = strServer + "Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx?pt=Formularios";
//    }

//});
