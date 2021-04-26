
$(document).ready(function () {
   
    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "250px");
        }
    }

    //Boton exportación a excel
    $('#btnExportExcel').on('click', exportarExcel)

    cargarFormacionDemandada();
})

function cargarFormacionDemandada() {

    actualizarSession();


    if (filtrosFormacionDemandada != "") {
        var $filtrosFormacionDemandada = JSON.parse(filtrosFormacionDemandada);
        
        //FECHAS        
        Desde = parseInt($filtrosFormacionDemandada[0].toString().substring(0, 4)) * 100 + parseInt($filtrosFormacionDemandada[0].toString().substring(4, 6));
        Hasta = parseInt($filtrosFormacionDemandada[1].toString().substring(0, 4)) * 100 + parseInt($filtrosFormacionDemandada[1].toString().substring(4, 6));

        $('#selAnoIni').val($filtrosFormacionDemandada[0].toString().substring(0, 4));
        $('#selMesIni').val($filtrosFormacionDemandada[0].toString().substring(4, 6));

        $('#selAnoFin').val($filtrosFormacionDemandada[1].toString().substring(0, 4));
        $('#selMesFin').val($filtrosFormacionDemandada[1].toString().substring(4, 6));

        $("#cboColectivo").val($filtrosFormacionDemandada[2].toString());
        
        filtrosFormacionDemandada = "";
    }

    procesando.mostrar();
    
    $.ajax({
        url: "Default.aspx/catFormacionDemandada",
        "data": JSON.stringify({ desde: parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val()), hasta: parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val()), t941_idcolectivo : $("#cboColectivo").val() }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            procesando.ocultar();
            //Si hay demasiados resultados 
            if (result.d.Nevaluciones > 5000) {
                alertNew("warning", "El volumen del resultado obtenido excede el límite máximo permitido (" + result.d.Nevaluciones + " frente a 5000 máximo). Acota más la búsqueda y vuelve a intentarlo", null, 6000, null);
                return;
            }
            pintarDatosPantalla(result.d);

            
        },
        error: function (ex, status) {
            procesando.ocultar();
            alertNew("danger", "No se ha podido obtener el catálogo de la formación demandada");            
        }
    });
}



function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  

        var tblFormacionDemandada = "";

        for (var i = 0; i < datos.FormacionDemandadaS1.length; i++) {

            tblFormacionDemandada += "<tr idformulario=" + datos.FormacionDemandadaS1[i].idformulario + " idvaloracion='" + datos.FormacionDemandadaS1[i].T930_idvaloracion + "'>";
            tblFormacionDemandada += "<td><i class='glyphicon glyphicon-search'</td>";
            tblFormacionDemandada += "<td>" + datos.FormacionDemandadaS1[i].Evaluador + "</td>";
            tblFormacionDemandada += "<td>" + datos.FormacionDemandadaS1[i].Evaluado + "</td>";
            tblFormacionDemandada += "<td>" + datos.FormacionDemandadaS1[i].Formacion + "</td>";
            tblFormacionDemandada += "</tr>";
        };


        //Todo metemos número de evaluacioens
        $("#numEvaluaciones").text(datos.Nevaluciones);

        //Inyectar html en la página
        $("#tbdFormacion").html(tblFormacionDemandada);

        /*Controlamos si el contenedor tiene Scroll*/
        var div = document.getElementById('tbdFormacion');

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
        if (hasVerticalScrollbar) {
            $("thead").css("width", "calc( 100% - 1em )")
        }
        else { $("thead").css("width", "100%") }
        /*FIN Controlamos si el contenedor tiene Scroll*/

        //Ordenación de columnas
        $("#tablaFormacion").trigger("destroy");
        
        $("#tablaFormacion").tablesorter({
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

                3: {
                    sorter: false
                }
               
            }
        });


    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}


$('body').on('click', '#tbdFormacion tr', function (e) {
    $('#tbdFormacion tr').removeClass('active');
    $(this).addClass('active');
});


$('body').on('click', '.glyphicon.glyphicon-search', function (e) {

    $active = $(this).parent().parent().addClass("active");
    if ($active.length > 0) {
        switch ($active.attr('idformulario')) {
            case "1":
                location.href = '/Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + '&acceso=formacion';
                break;
            case "2":
                location.href = '/Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + '&acceso=formacion';
                break;
        }
    } else
        alertNew("warning", "Tienes que seleccionar alguna fila para acceder a la evaluación", null, 2000, null);
});



//Acceder a la evaluación
$('#btnAcceder, .glyphicon.glyphicon-search').on('click', function () {
    $active = $('#tbdFormacion tr.active');
    if ($active.length > 0) {
        switch ($active.attr('idformulario')) {
            case "1":
                location.href = '/Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) +'&acceso=formacion';
                break;
            case "2":
                location.href = '/Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + '&acceso=formacion';
                break;
        }
    } else
        alertNew("warning", "Tienes que seleccionar alguna fila para acceder a la evaluación", null, 2000, null);

});


function exportarExcel() {

    //validaciones
    if ($("#tbdFormacion tr").length == 0) {
        alertNew("warning", "No hay datos para exportar");
        return;
    }
    
    //Cargar en el iframe la página de exportación.
    var qs = "pantalla=formaciondemandada&idficepi=" + idficepi + "&desde=" + parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val()) + "&mesinitext=" + $('#selMesIni option:selected').text() + "&hasta=" + parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val()) + "&anoinitext=" + $('#selAnoIni option:selected').text() + "&mesfintext=" + $('#selMesFin option:selected').text() + "&anofintext=" + $('#selAnoFin option:selected').text() + "&colectivo=" + $("#cboColectivo").val() + "&colectivotext=" + $("#cboColectivo option:selected").text() + "&numevaluaciones=" + $("#tbdFormacion tr").length;
        
    $("#ifrmExportExcel").prop("src", strServer + "Capa_Presentacion/Utilidades/ExportarExcel.aspx?" + qs);

}