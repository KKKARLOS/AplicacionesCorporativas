
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

$('input[name=intereses]').on('click', function () {
    if ($(this).attr('id') == 'rdbIntereses3') {
        $('#divOtrosIntereses').fadeIn("slow");
    }

    else {
        $('#divOtrosIntereses').fadeOut("slow");
    }
});

$('#divNecesidadFormacion input[type=checkbox]').on('click', function () {
    $this = $(this);
    if ($this.prop('checked'))
        $this.parent().parent().find('input[type=text]').prop("disabled", false);
    else
        $this.parent().parent().find('input[type=text]').prop("disabled", true).val('');
});

function comprobar_datos() {
    try {
        var habilidades = $('.evaluador input[name=desemProfesional]:checked').val();
        if (habilidades == null || habilidades == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la atención al cliente interno/externo.');
            return false;
        }
        var habilidades2 = $('.evaluador input[name=plazoResp]:checked').val();
        if (habilidades2 == null || habilidades2 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre el plazo de respuesta.');
            return false;
        }
        var habilidades3 = $('.evaluador input[name=calidadResp]:checked').val();
        if (habilidades3 == null || habilidades3 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la calidad de respuesta.');
            return false;
        }
        var habilidades4 = $('.evaluador input[name=dificilesResp]:checked').val();
        if (habilidades4 == null || habilidades4 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la respuesta ante situaciones difíciles.');
            return false;
        }
        var habilidades5 = $('.evaluador input[name=valoraActividad]:checked').val();
        if (habilidades5 == null || habilidades5 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre los indicadores de la actividad.');
            return false;
        }
        var valores = $('.evaluador input[name=orientaCliente]:checked').val();
        if (valores == null || valores == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la orientación al cliente.');
            return false;
        }
        var valores2 = $('.evaluador input[name=orientaResul]:checked').val();
        if (valores2 == null || valores2 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la orientación a resultados.');
            return false;
        }
        var valores3 = $('.evaluador input[name=comunicacion]:checked').val();
        if (valores3 == null || valores3 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre la comunicacfión.');
            return false;
        }
        var valores4 = $('.evaluador input[name=compromiso]:checked').val();
        if (valores4 == null || valores4 == undefined) {
            alertNew('warning', 'Debes indicar la evaluación sobre el compromiso.');
            return false;
        }
        if ($('.evaluador input[name=intereses]:checked').val() == 3 && $('#otrosIntereses').val() == '') {
            alertNew('warning', 'Debes especificar los intereses profesionales.');
            return false;
        }
        return true;
    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar los datos.");
    }
}

function guardar(firma, desdeImprimir) {
    try {
        var form_2 = new formulario_id2();
        var sMetodo = "updatearEvaluador";
        //Rellenar campos propios de formulario
        form_2.t930_idvaloracion = idValoracion;
        form_2.t934_idmodeloformulario = 2;//El ID del formulario estandar es 2
        if (estadoEvaluacion == 1) {//Update del evaluador
            //Si ha firmado cargamos el campo firma con la fecha actual
            if (firma)
                form_2.t930_fecfirmaevaluador = new Date();
            //Rellenar campos propios de formulario estandar
            form_2.t930_denominacionROL = $('#sRol').val();
            form_2.t930_denominacionCR = $('#sCR').val();
            form_2.t930_atclientes = $('.evaluador input[name=desemProfesional]:checked').val();
            form_2.t930_prespuesta = $('.evaluador input[name=plazoResp]:checked').val();
            form_2.t930_crespuesta = $('.evaluador input[name=calidadResp]:checked').val();
            form_2.t930_respdificil = $('.evaluador input[name=dificilesResp]:checked').val();
            form_2.t930_valactividad = $('.evaluador input[name=valoraActividad]:checked').val();
            form_2.t930_amejorar = $('#sAmejorar').val();
            form_2.t930_gescli = $('.evaluador input[name=orientaCliente]:checked').val();
            form_2.t930_liderazgo = $('.evaluador input[name=orientaResul]:checked').val();
            form_2.t930_planorga = $('.evaluador input[name=comunicacion]:checked').val();
            form_2.t930_exptecnico = $('.evaluador input[name=compromiso]:checked').val();
            form_2.t930_cooperacion = $('.evaluador input[name=cooperacion]:checked').val();
            form_2.t930_iniciativa = $('.evaluador input[name=iniciativa]:checked').val();
            form_2.t930_perseverancia = $('.evaluador input[name=perseverancia]:checked').val();
            form_2.t930_interesescar = $('.evaluador input[name=intereses]:checked').val();
            form_2.t930_especificar = $('#otrosIntereses').val();
            form_2.t930_forofichk = $('#chkOfimatica').prop('checked');
            form_2.t930_forofitxt = $('#lblOfimatica').val();
            form_2.t930_fortecchk = $('#chkTecnologias').prop('checked');
            form_2.t930_fortectxt = $('#lblTecnologias').val();
            form_2.t930_foratcchk = $('#chkCliente').prop('checked');
            form_2.t930_foratctxt = $('#lblCliente').val();
            form_2.t930_forcomchk = $('#chkComunicacion').prop('checked');
            form_2.t930_forcomtxt = $('#lblComunicacion').val();
            form_2.t930_forvenchk = $('#chkIdeas').prop('checked');
            form_2.t930_forventxt = $('#lblIdeas').val();
            form_2.t930_forespchk = $('#chkConocimientos').prop('checked');
            form_2.t930_foresptxt = $('#lblConocimientos').val();
            
        } else if (estadoEvaluacion == 2) {//Update del evaluado
            //Si ha firmado cargamos el campo firma con la fecha actual
            if (firma)
                form_2.t930_fecfirmaevaluado = new Date();
            //Rellenar campos propios de formulario estandar
            form_2.t930_autoevaluacion = $('#sAutoevaluacion').val();
            //Estas las relleno pq el objeto de servidor no es nullable 
            form_2.t930_forofichk = $('#chkOfimatica').prop('checked');
            form_2.t930_fortecchk = $('#chkTecnologias').prop('checked');
            form_2.t930_foratcchk = $('#chkCliente').prop('checked');
            form_2.t930_forcomchk = $('#chkComunicacion').prop('checked');
            form_2.t930_forvenchk = $('#chkIdeas').prop('checked');
            form_2.t930_forespchk = $('#chkConocimientos').prop('checked');
            sMetodo = "updatearEvaluado";
        }
    } catch (e) {
        alertNew("danger", "Ocurrió un error al capturar los datos para grabar.");
    }

    actualizarSession();
    $.ajax({
        url: "Default.aspx/" + sMetodo,   // Current Page, Method
        data: JSON.stringify({ form2: form_2 }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function () {
            //if (estadoEvaluacion == 1) {//Llamada a catálogo de evaluaciones abiertas
            //    location.href = "/Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
            //} else if (estadoEvaluacion == 2) {
            //    location.href = "/Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
            //}

            if (!desdeImprimir) {
                navegar();

                //if (origen == "noConsultas") {
                //    if (estadoEvaluacion == 1) {//Llamada a catálogo de evaluaciones abiertas
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
                //    } else if (estadoEvaluacion == 2) {
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
                //    }
                //}

                //else {
                //    if (menu == "Eva") {
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=Eva";
                //    }
                //    else {
                //        location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=Adm";
                //    }

                //}
            }

        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar guardar una evaluación");
        }
    });
}

$("#pieFormulario #sinfirma").on("click", function () {//Faltan los diálogos
    if (estadoEvaluacion == 1) {//Es evaluador
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


$("#modal-evaluador button#evaluadorms").on("click", function () { guardar(true, false); });
$("#modal-evaluado-sinfirma button#evaluadomc").on("click", function () { guardar(false, false); });
$("#modal-evaluado-confirma button#evaluadoms").on("click", function () { guardar(true, false); });



//Botón regresar
//$("#regresar").on("click", function () {
//    if (origen == "noConsultas") {
//        if (estadoEvaluacion == 0) {
//            location.href = "/Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
//        }
//        if (estadoEvaluacion == 1) {//Llamada a catálogo de evaluaciones abiertas (Es evaluador)
//            location.href = "/Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
//        } else if (estadoEvaluacion == 2) {
//            location.href = "/Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
//        }
//    }
//    else {
//        if (menu.toUpperCase() == "EVA") location.href = "/Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=Eva&pt=Estandar";
//        else location.href = "/Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=ADM&pt=Estandar";

//    }
//});


$("#regresar, #cancelar").on("click", function () {

    navegar();
    //if (origen == "noConsultas") {
    //    if (estadoEvaluacion == 0) {
    //        if (acceso == "formacion") {
    //            location.href = strServer + "Capa_Presentacion/Administracion/FormacionDemandada/Default.aspx?pt=Formularios";
    //            return;
    //        }

    //        if (acceso == "demiequipo") {
    //            location.href = strServer + "Capa_Presentacion/Evaluacion/DeMiEquipo/Default.aspx?pt=Formularios";
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


//Función navegar (botón regresar y botón cancelar de los formularios)
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



$("input[name=desemProfesional], input[name=plazoResp], input[name=calidadResp], input[name=dificilesResp]").on("click", function () {
    bCambios = true;
});

$("input[name=valoraActividad], input[name=orientaCliente], input[name=orientaResul], input[name=comunicacion]").on("click", function () {
    bCambios = true;
});

$("input[name=compromiso], input[name=cooperacion], input[name=iniciativa], input[name=perseverancia], input[name=intereses] ").on("click", function () {
    bCambios = true;
});

$("textarea").on("keyup", function () {
    bCambios = true;
})


$("input[name=chkOfimatica], input[name=chkTecnologias], input[name=chkCliente], input[name=chkComunicacion], input[name=chkIdeas], input[name=chkConocimientos]").on("click", function () {
    bCambios= true;
})


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
}


$("#btnconfirm").on("click", function () {
    //Grabar e imprimir
    guardar(false, true);
    imprimirInforme();
    $("#modal-datosmodificados").modal('hide');
});

$("#btnCancelar").on("click", function () {
    //Sólo imprimir
    imprimirInforme();
    $("#modal-datosmodificados").modal('hide');
});

function imprimirInforme() {
    if (IdEvaluacion == "") return;
    
    var strUrlPag = "../Modelo1/Informe/Default.aspx?IdEvaluacion=" + codpar(IdEvaluacion) + "&modelo=2";
    var bScroll = "no";
    var bMenu = "no";
    if (screen.width == 800) bScroll = "yes";
    bMenu = "yes";
    window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
}


//$("#cancelar").on("click", function () {

//    navegar();

//    //if (origen == "noConsultas") {
//    //    if (estadoEvaluacion == 1) {//Llamada a catálogo de evaluaciones abiertas (Es evaluador)
//    //        location.href = strServer +"Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
//    //    }

//    //    else if (estadoEvaluacion == 2) {
//    //        location.href = strServer +"Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx";
//    //    }
//    //}
//    //else {
//    //    location.href = strServer +"Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=Eva";
//    //}


//    //if (acceso == "completarabiertas") {
//    //    location.href = strServer + "Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
//    //}

//    //else if (acceso == "demiequipo") {
//    //    location.href = strServer + "Capa_Presentacion/Evaluacion/DeMiEquipo/Default.aspx?pt=Formularios";
//    //}

//    //else if (acceso == "misevaluaciones") {
//    //    location.href = strServer + "Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx?pt=Formularios";
//    //}



//});







