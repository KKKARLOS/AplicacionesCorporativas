
$('#calendario1, #calendario2').on('click', function (event) {

    datepickerAccesibilidad();

});

function datepickerAccesibilidad() {

    /*Añadimos propiedades para mejorar la accesibilidad de datepicker*/

    /*Se añaden tabindex a las fechas de navegación de meses*/
    $('.ui-datepicker-prev.ui-corner-all').attr('tabindex', '0');
    $('.ui-datepicker-next.ui-corner-all').attr('tabindex', '0');

    /*Se añade información del mes/año actual al aria-label del contenedor del datepicker*/
    $('#calendario1').attr('aria-label', 'Calendario Inicio ' + $('#calendario1 .ui-datepicker-month').html() + ' ' + $('#calendario1 .ui-datepicker-year').html());
    $('#calendario2').attr('aria-label', 'Calendario Fin ' + $('#calendario2 .ui-datepicker-month').html() + ' ' + $('#calendario2 .ui-datepicker-year').html());

    /*Se añade a cada día el literal del día de la semana que le corresponde junto al número de día en la atributo aria-label*/
    $(".ui-datepicker-calendar > tbody > tr").each(function (index) {
        for (var i = 1; i <= 7; i++) {
            $('> td:nth-child(' + i + ') > a', this).attr('aria-label', $('.ui-datepicker-calendar > thead > tr > th:nth-child(' + i + ') > span').attr('title') + ' ' + $('> td:nth-child(' + i + ') > a', this).html());
        }
    })

    /*Se añade el literal Hoy al día actual*/
    $('.ui-datepicker-today > a').attr('aria-label', $('.ui-datepicker-today > a').attr('aria-label') + ' Hoy');

};