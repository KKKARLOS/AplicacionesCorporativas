var roles = null;

$(document).ready(function () {
    comprobarerrores();

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "400px");
        }
    }

    $('[data-toggle="popover"]').popover({        
        trigger: 'hover',
        html: true
    });
 

    /*Controlamos si el contenedor tiene Scroll*/
    var div = document.getElementById('idtbody');

    var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
    if (hasVerticalScrollbar) {
        $("#tablaSolicitudRol th:eq(1)").css("width", "29%");
        
    }
    else {
        $("#tablaSolicitudRol th:eq(1)").css("width", "30%");
    }
    /*FIN Controlamos si el contenedor tiene Scroll*/

});

$('#btnModalTramitar').on("click", function () {
    $fila = $('#idtbody tr.active');
    if ($fila.length > 0) {
        if ($fila.not('[id]').length == 0)
        {
            alertNew("warning", "El profesional seleccionado ya tiene una solicitud de cambio de rol.", null, 4000, null);
        }

        else if ($fila.find('.glyphicon-new-window').length > 0) {
            alertNew("warning", "El profesional seleccionado tiene una salida en trámite. No puedes solicitarle cambio de rol.", null, 4000, null);
        }

        else if ($fila.find('.fa.fa-compress').length > 0)
        {
            alertNew("warning", "El profesional seleccionado tiene una salida en estado rechazada. No puedes solicitarle cambio de rol.", null, 4000, null);
        }
        else if ($fila.find('.glyphicon-user').length > 0) {
            alertNew("warning", "El profesional seleccionado tiene una salida de tu equipo en trámite, para la cual has solicitado mediación. Hasta que se resuelva, no se puede solicitar un cambio de rol.", null, 4000, null );
        }

        else
        {
            actualizarSession();
            $.ajax({
                url: "Default.aspx/obtenerRoles",   // Current Page, Method
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {
                    pintarModal(result.d, $fila);
                },
                error: function (ex, status) {
                    alertNew("danger", "Error al cargar la lista de roles");
                }
            });
        }
    } else {
        alertNew("warning", "Debes seleccionar algún profesional");
    }
});

//Selección de profesionales
$('body').on('click', '#idtbody tr', function (e) {
    $('#idtbody tr').removeClass('active');
    $(this).addClass('active');

    if ($(this).attr("data-t940_resolucion") != undefined)
        $("#btnModalAnular").attr("disabled", "disabled");
    else
        $("#btnModalAnular").removeAttr("disabled");
});

function pintarModal(lisRoles, $fila) {
    $('#modal-solicitar input:eq(0)').val($fila.find('td:eq(0) span:last-child').text());
    $('#modal-solicitar input:eq(1)').val($fila.find('td:eq(1)').text());
    $('#modal-solicitar textarea').val('');
    ////Contabiliza el número de caracteres escritos en un textarea
    $('#numCaracteres').text('750 caracteres disponibles');
    $('#modal-solicitar select').find('option').remove().end().append('<option value=""></option>');
    $(lisRoles).each(function () {
        if ($fila.find('td:eq(1)').text() != this.t004_desrol)
            $('<option value=' + this.t004_idrol + '>' + this.t004_desrol + '</option>').appendTo('#modal-solicitar select');
    });
    $('#modal-solicitar').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-solicitar').modal('show');
}

$('#btnTramitar').on('click', function () {

    if ($("#txtMotivo").val().length == 0) {
        alertNew("warning", "Tienes que indicar un motivo")
        $("#txtMotivo").focus();
        return;
    }


    if ($("#txtMotivo").val().length < 10) {
        alertNew("warning", "Tienes que ampliar la descripción del motivo", null, 4000, null);
        $("#txtMotivo").focus();
        return;
    }

    $fila = $('#idtbody tr.active');
    $modal = $('#modal-solicitar');
    if ($modal.find('select').val() != "") {
        if ($modal.find('textarea').val() != '') {
            var camrol = new tramitacioncambiorol();
            camrol.t001_idficepi_interesado = $fila.attr('idficepi');
            camrol.t004_idrol_propuesto = $modal.find('select').val();
            camrol.t940_motivopropuesto = $modal.find('textarea').val();
            actualizarSession();
            $.ajax({
                url: "Default.aspx/insert",   // Current Page, Method

                data: JSON.stringify({ tcr_i: camrol, nombreapellidosinteresado: $fila.attr("data-nombreapellidosprofesional"), rolactual: $("#inputRolActual").val(), rolpropuesto: $("#selRolPropuesto option:selected").text(), motivo: $modal.find('textarea').val(), nombreaprobador: $fila.attr("data-nombreaprobador"), correoaprobador: $fila.attr("data-correoaprobador"), nombreapellidosaprobador: $fila.attr("data-nombreapellidosaprobador") }),  // parameter map as JSON
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {
                    if (result.d != "") {
                        $fila.attr('id', result.d);
                        $fila.find('td:eq(2)').text($modal.find('select option:selected').text());
                    } else {
                        $fila.find('td:eq(1)').text($modal.find('select option:selected').text());
                        alertNew('success', 'Rol cambiado correctamente');
                    }
                    $('#modal-solicitar').modal('hide');
                    
                    //PINTAMOS EL ICONO DE MOTIVO
                    var icono = "<a data-placement='left' data-toggle ='popover' href='#' title='' data-content='" + camrol.t940_motivopropuesto + "'><i class='glyphicon glyphicon-comment text-primary'></i></a>";
                    if (camrol.t940_motivopropuesto != "") $fila.find('td:last').html(icono);

                    $('[data-toggle="popover"]').popover({                        
                        trigger: 'hover',
                        html: true
                    });
                    
                },
                error: function (ex, status) {
                    alertNew("danger", "Error al solicitar un cambio de rol");
                }
            });
        } else {
            $('#modal-no-motivo').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#modal-no-motivo').modal('show');
        }
    } else {
        alertNew("warning", "Debes seleccionar el rol propuesto.");
    }
});

$('#btnAceptar').on('click', function () {
    $fila = $('#idtbody tr.active');
    $modal = $('#modal-solicitar');
    var camrol = new tramitacioncambiorol();
    camrol.t001_idficepi_interesado = $fila.attr('idficepi');
    camrol.t004_idrol_propuesto = $modal.find('select').val();
    camrol.t940_motivopropuesto = $modal.find('textarea').val();
    actualizarSession();
    $.ajax({
        url: "Default.aspx/insert",   // Current Page, Method
        data: JSON.stringify({ tcr_i: camrol, nombreapellidosinteresado: $fila.attr("data-nombreapellidosprofesional"), rolactual: $("#inputRolActual").val(), rolpropuesto: $("#selRolPropuesto option:selected").text(), motivo: $modal.find('textarea').val() }),  // parameter map as JSON })
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            if (result.d != "") {
                $fila.attr('id', result.d);
                $fila.find('td:eq(2)').text($modal.find('select option:selected').text());
            } else {
                $fila.find('td:eq(1)').text($modal.find('select option:selected').text());
                alertNew('success', 'Rol cambiado correctamente');
            }
            $('#modal-no-motivo').modal('hide');
            $('#modal-solicitar').modal('hide');
        },
        error: function (ex, status) {
            alertNew("danger", "Error al solicitar un cambio de rol");
        }
    });
});

$('#btnModalAnular').on("click", function () {
    $fila = $('#idtbody tr.active');
    if ($fila.length > 0) {
        if ($fila.not('[id]').length > 0) {
            alertNew("warning", "El profesional no tiene ninguna solicitud de cambio de rol.");
        } else {
            $('#modal-anular textarea').val('');
            $('#modal-anular').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#modal-anular').modal('show');
        }
    } else {
        alertNew("warning", "Debes seleccionar algún profesional");
    }

    
});

$('#btnAnular').on('click', function () {
    if ($('#modal-anular textarea').val() != '') {
        actualizarSession();
        $.ajax({
            url: "Default.aspx/delete",   // Current Page, Method
            data: JSON.stringify({ t940_idtramitacambiorol: $('#idtbody tr.active').attr('id'), nombreapellidosinteresado: $('#idtbody tr.active').attr("data-nombreapellidosprofesional"), rolactual: $("#idtbody tr.active").find("td:eq(1)").text(), rolpropuesto: $("#idtbody tr.active").find("td:eq(2)").text(), motivo: $("#modal-anular").find('textarea').val(), nombreaprobador: $('#idtbody tr.active').attr('data-nombreaprobador'), correoaprobador: $('#idtbody tr.active').attr('data-correoaprobador') }),  // parameter map as JSON
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                $fila.removeAttr('id');
                $fila.find('td:eq(2)').text('');
                $fila.find('td:eq(3)').text('');
                $('#modal-anular').modal('hide');
            },
            error: function (ex, status) {
                alertNew("danger", "Error al solicitar un cambio de rol.");
            }
        });
    } else {
        alertNew("warning", "Debes indicar el motivo de anulación.");
    }
});

$('#txtMotivo').keyup(function (e) {
    var max = 750;
    var len = $(this).val().length;
    if (len >= max) {
        $('#numCaracteres').text('Límite alcanzado');
        $('#numCaracteres').addClass('text-danger');

        /*Una vez alcanzado el límite, sólo permitimos el backspace*/
        if (e.keyCode !== 8) {
            e.preventDefault();
        }

    }
    else {
        var ch = max - len;
        $('#numCaracteres').text(ch + ' caracteres disponibles');
        $('#numCaracteres').removeClass('disabled');
        $('#numCaracteres').removeClass('text-danger');
    }
});

//Ponemos foco a campos de la modal
$(window).on('shown.bs.modal', function () {
    $("#txtMotivoAnulacion").focus();        
});