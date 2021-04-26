var $encurso = $('#tbdEval tr[estado=CUR]');
var $cerradas = $('#tbdEval tr[estado!=CUR]');
var $todas = $('#tbdEval tr');
var $cboestado = $('#cboEstado');

$(document).ready(function () {

    //Permitimos ordenación de columnas
    //$("#tableEval").tablesorter();

    $("#tableEval").trigger("destroy");

    //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
    $("#tableEval").tablesorter({
        //dateFormat: "dd/mm/yy", // set the default date format
        // pass the headers argument and assing a object            
        headers: {
            // set "sorter : false" (no quotes) to disable the column
            0: {
                sorter: false
            },
            1: {
                sorter: "text"
            },
            2: {
                sorter: "text"
            },
            3:
            {
                sorter: "shortDate", dateFormat: "ddmmyyyy"
            },
            4:
            {
                 sorter: "shortDate", dateFormat: "ddmmyyyy"
            }
          
        }
    });

    comprobarerrores();
    try {

        if (filtros != "") {
            if (filtros == "0") {
                $cboestado.val('0');
                $encurso.removeClass('hide');
            }
            else if (filtros == "1") {
                $cboestado.val('1');
                $cerradas.removeClass('hide');
            }
            else {
                $cboestado.val('2');
                $encurso.removeClass('hide');
                $cerradas.removeClass('hide');
            }
        }
        else {
            if ($encurso.length > 0) {
                $cboestado.val('0');
                $encurso.removeClass('hide');
            } else if ($cerradas.length > 0) {
                $cboestado.val('1');
                $cerradas.removeClass('hide');
            }
        }

       
    } catch (e) {
        alertNew("danger", "Ocurrió un error al iniciar la página.");
    }

    $("#spanNumeroEvaluaciones").text($("#tbdEval tr:not(.hide)").length)

    if ($("#tbdEval tr:not(.hide)").length == 1) $("#spanTextoResultado").text("resultado");
    else $("#spanTextoResultado").text("resultados");


    /*Controlamos si el contenedor tiene Scroll*/
    var div = document.getElementById('tbdEval');

    var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
    if (hasVerticalScrollbar) {
        $("thead").css("width", "calc( 100% - 1em )")
    }
    else { $("thead").css("width", "100%") }
    /*FIN Controlamos si el contenedor tiene Scroll*/


    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "300px");
        }
    }

});

$cboestado.on('change', function () {    
    $this = $(this);
    $("#divSinDatos").removeClass("show").addClass("hide");
    $("#btnAcceder").css("display", "block");
    if ($this.val() == '0') {//En curso
        $encurso.removeClass('hide');
        $cerradas.addClass('hide');
        if ($encurso.length == 0) {
            $("#btnAcceder").css("display", "none");
            $("#divSinDatos").removeClass("hide").addClass("show bsAlert col-md-5 col-md-offset-4 col-lg-5 col-lg-offset-3 alert alert-info cajaTexto pad10");
            $("#txtSinEvaluaciones").text("No tienes evaluaciones en curso")
        }
    } else if ($this.val() == '1') {//Cerradas
        $encurso.addClass('hide');
        $cerradas.removeClass('hide');
        if ($cerradas.length == 0) {
            $("#btnAcceder").css("display", "none");
            $("#divSinDatos").removeClass("hide").addClass("show bsAlert col-md-5 col-md-offset-4 col-lg-5 col-lg-offset-3 alert alert-info cajaTexto pad10");
            $("#txtSinEvaluaciones").text("No tienes evaluaciones cerradas")

        }
    }
    else {
        $encurso.removeClass('hide');
        $cerradas.removeClass('hide');
        if ($todas.length == 0) {
            $("#btnAcceder").css("display", "none");
            $("#divSinDatos").removeClass("hide").addClass("show bsAlert col-md-5 col-md-offset-4 col-lg-5 col-lg-offset-3 alert alert-info cajaTexto pad10");
            $("#txtSinEvaluaciones").text("No tienes evaluaciones")

        }
    }

    $("#spanNumeroEvaluaciones").text($("#tbdEval tr:not(.hide)").length)
    if ($("#tbdEval tr:not(.hide)").length == 1) $("#spanTextoResultado").text("resultado");
    else $("#spanTextoResultado").text("resultados");

});

//Selección de profesionales
$('body').on('click', '#tbdEval tr', function (e) {
    $('#tbdEval tr').removeClass('active');
    $(this).addClass('active');
});


$(".glyphicon.glyphicon-search").on("click", function () {
    guardarFiltros();
    $(this).parent().parent().addClass("active");
    irDetallefromLupa();    
})

$('div.container div.row button.btn-primary').on('click', function () {    
    guardarFiltros();
    irDetalle();   
});


function guardarFiltros() {
    $.ajax({
        url: "Default.aspx/GuardarFiltros",
        "data": JSON.stringify({ estado: $cboestado.val() }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {

        },
        error: function (ex, status) {
            alertNew("danger", "No se han podido guardar los filtros de búsqueda");
        }
    });
}


function irDetalle() {
    $active = $('#tbdEval tr.active');

    if ($active.length > 0) {
        switch ($active.attr('idformulario')) {
            case "1":
                location.href = strServer + "Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=" + codpar($active.attr('idvaloracion')) + "&acceso=misevaluaciones";
                break;
            case "2":
                location.href = strServer + "Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=" + codpar($active.attr('idvaloracion')) + "&acceso=misevaluaciones";
                break;
        }
    } else
        alertNew("warning", "Tienes que seleccionar alguna evaluación");
}

function irDetallefromLupa() {
    $active = $('#tbdEval tr.active');

    switch ($active.attr('idformulario')) {
        case "1":
            location.href = strServer + "Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=" + codpar($active.attr('idvaloracion')) + "&acceso=misevaluaciones";
            break;
        case "2":
            location.href = strServer + "Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=" + codpar($active.attr('idvaloracion')) + "&acceso=misevaluaciones";
            break;
    }
}

//$(window).on("orientationchange", function (event) {
//    $('meta[name=viewport]').remove();
//    $('head').append('<meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1">');
    
//});


/*
// Change the selector if needed
var $table = $('div.panel-body table'),
    $bodyCells = $table.find('thead tr:first').children(),
    colWidth;

// Adjust the width of thead cells when window resizes
$(window).resize(function () {
    // Get the tbody columns width array
    colWidth = $bodyCells.map(function () {
        return $(this).width();
    }).get();

    // Set the width of thead columns
    $table.find('tbody tr').children().each(function (i, v) {
        $(v).width(colWidth[i]);
    });
}).resize(); // Trigger resize handler





*/

