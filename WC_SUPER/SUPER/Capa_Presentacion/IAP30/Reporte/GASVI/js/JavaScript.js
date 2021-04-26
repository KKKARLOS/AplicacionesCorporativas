var prevFocus;
var returnElement;

$(document).ready(function () {

});

//cambio de icono de FA
$(document).on('change', 'input:radio[id^="radio"]', function (event) {
    if (this.value == "0") {
        $("#icoJustificante").removeClass("fa-question-circle").removeClass("fa-check-circle").addClass("fa-times-circle");
    }
    else {
        $("#icoJustificante").removeClass("fa-question-circle").removeClass("fa-times-circle").addClass("fa-check-circle");
    }
});

//Una vez mostrado el modal de calculadora enviamos el foco al input de resultado
$('#calculadora').on('shown.bs.modal', function (event) {
    $('#idCalculadoratxtResultado').focus();
});

//Damos funcionalidad al enter en el modal de calculadora con el input central enfocado
$(document).on('keyup', '#idCalculadoratxtResultado', function (e) {
    var code = e.keyCode || e.which;
    if (code == 13) {
        $('#idCalculadoratxtResultado').val(eval($('#idCalculadoratxtResultado').val()));
    }
});

//Enviamos al input el valor calculado en la calculadora
$('#btnLlevarValor').on('click', function (event) {
    $(returnElement).val($('#idCalculadoratxtResultado').val());
    $('#calculadora').modal('hide');
});

$(document).on('hidden.bs.modal', '.modal', function () {
    $('.ocultable').attr('aria-hidden', 'false');
});

$('.lineaTablaProy').on('dblclick', function (event) {
    $('#txtPro').val('42.404 - IAP 3.0')
    $('#proyecto').modal('hide');
});

/*Comprobamos si cuando se reduce el tamaño la calculadora está visualizandose para ocultarla*/
$(window).resize(function () {
    var width = $(window).width();
    if (width < 992 && ($("#calculadora").data('bs.modal') || {}).isShown) {
        $('#calculadora').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#calculadora').modal('hide');
    }
});

//Función  de cebreo
function cebrear() {
    $("#tablaProyectos tr").removeClass("cebreada");
    $('#tablaProyectos tr:even').addClass('cebreada');
}
