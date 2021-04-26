

$(document).ready(function () {


    // //Se obtiene la fecha actual
    var currentDate = new Date()
    var day = currentDate.getDate();
    var month = currentDate.getMonth() + 1;
    var year = currentDate.getFullYear();


    $("#txtFantiguedad").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        yearRange: '1973:' + year,
        

        beforeShow: function (input, inst) {
            $(inst.dpDiv).removeClass('calendar-off');
        }
    });

    //Por defecto, la fecha de antigüedad es un año menos de la fecha actual
    //$("#txtFantiguedad").datepicker({
    //    dateFormat: 'dd/mm/yy'
    //}).datepicker('setDate', defectoAntiguedad)

    ////Traducción al español del datepicker
    $(function ($) {
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '<Ant',
            nextText: 'Sig>',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
    });
    // //FIN DATEPICKER


    $("#txtFantiguedad").datepicker({
        dateFormat: 'dd/mm/yy'
    }).datepicker('setDate', defectoAntiguedad)


})




$("#txtFantiguedad").on("change", function () {
   
    // actualizarSession();

    var sfecha = $('input#txtFantiguedad').val();
    var sd = sfecha.substring(0, 2);
    var sm = sfecha.substring(3, 5);
    var sa = sfecha.substring(6, 10);

    var fAntiguedad = new Date(sa, sm - 1, sd);

    $.ajax({
        url: "Default.aspx/updateFecha",   // Current Page, Method
        type: "POST",
        "data": JSON.stringify({ fecha: fAntiguedad }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            alertNew("success", "Modificación grabada.");
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar actualizar.");
        }

    });
  
})