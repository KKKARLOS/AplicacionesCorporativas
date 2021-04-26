$(document).ready(function () {

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "300px");
        }
    }

    //Permitimos ordenación de columnas
    //$("#tablaProf").tablesorter();

    if ($('#tbdProf tr').length == 0) {
        $("button").css("display", "none");
        $("#divtablaProf").css("display", "none");
        $("#divSinDatos").removeClass("hide").addClass("show bsAlert col-xs-10 col-xs-offset-1 col-md-8 col-md-offset-2 alert alert-info cajaTexto pad10");
    }

    $("#tablaProf").trigger("destroy");

    //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
    $("#tablaProf").tablesorter({
        //dateFormat: "dd/mm/yy", // set the default date format
        // pass the headers argument and assing a object            
        headers: {
            // set "sorter : false" (no quotes) to disable the column
            0: {
                sorter : false
            },
            1: {
                sorter: "text"
            },
            2: {
                sorter: "shortDate", dateFormat: "ddmmyyyy"
            }
          
        }
    });

    comprobarerrores();

    $("#txtNombreEvaluador").text(nombre);
    
   
});

//Selección de profesionales
$('body').on('click', '#tbdProf tr', function (e) {
    $('#tbdProf tr').removeClass('active');
    $(this).addClass('active');
});


$('body').on('click', '.glyphicon.glyphicon-search', function (e) {

    $active = $(this).parent().parent().addClass("active");
    
    switch ($active.attr('idformulario')) {
        case "1":
            location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=completarabiertas";
            break;
        case "2":
            location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=completarabiertas";
            break;
    }    
});


$('div.container div.row button.btn-primary').on('click', function () {
    $active = $('#tbdProf tr.active');
    if ($active.length > 0) {        
        switch ($active.attr('idformulario')) {
            case "1":
                location.href = strServer +'Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=completarabiertas";                
                break;
            case "2":
                location.href = strServer +'Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=completarabiertas";
                break;
        }
    }else
        alertNew("warning", "Debes seleccionar la evaluación de algún profesional");
        
});


$("#btnEliminar").on("click", function () {

    $active = $('#tbdProf tr.active');

    $("#txtNombreEvaluado").text($active.attr("nombreprofesional"));

    if ($active.length > 0) {
        $("#modal-confirmacion-Eliminacion").modal("show");
    }

    else {
        alertNew("warning", "Debes seleccionar alguna evaluación");
    }
    
})


$("#btnNoEnvio").on("click", function () {
    $("#modal-confirmacion-Eliminacion").modal("hide");
})


$("#btnOkEnvio").on("click", function () {
    $active = $('#tbdProf tr.active');
    $.ajax({
        url: "Default.aspx/Delete",
        "data": JSON.stringify({ t930_idvaloracion: $active.attr("idvaloracion"), checkbox: $("#chkAvisar").prop("checked"), correoprofesional: $active.attr("correoprofesional"), nombreevaluado: $active.attr("nombreprofesional"), fechaapertura: $active.attr("fechaapertura") }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-confirmacion-Eliminacion").modal("hide");
            $active.remove();
        },
        error: function (ex, status) {
            alertNew("danger", "No se han podido eliminar la evaluación");
        }
    });
})




