
$(document).ready(function () {
    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "300px");
        }
    }


    //manejador de fechas
    moment.locale('es');

})


function historicoRoles() {

   
    //Se comprueba que los combos Desde y Hasta seleccionados tienen un periodo lógico
    if (!comprobarfechas()) return;

    var Desde = parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val());
    var Hasta = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());

    actualizarSession();

  
    //Vacíamos tabla
    $("#tbdEval").html("");

    $.ajax({
        url: "Default.aspx/historicoRoles",
        "data": JSON.stringify({ t001_apellido1: $("#txtApellido1").val(), t001_apellido2: $("#txtApellido2").val(), t001_nombre: $("#txtNombre").val(), desde: Desde, hasta: Hasta }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {

            pintarDatosPantalla(JSON.parse(result.d));



        },
        error: function (ex, status) {
            alertNew("danger", "No se han podido obtener los históricos de roles");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  

        var tbdHistorico = "";
        var FechaCambio = "";
        for (var i = 0; i < datos.length; i++) {

            tbdHistorico += "<tr data-id='"+ datos[i].t001_idficepi +"'>";
           
            tbdHistorico += "<td>" + datos[i].Profesional + "</td>";
            tbdHistorico += "<td>" + datos[i].RolNuevo + "</td>";            
            tbdHistorico += "<td>" + moment(datos[i].FechaCambio).format('DD/MM/YYYY') + "</td>";

            //if (datos[i].FechaCambio == null) FechaCambio = "";
            //else FechaCambio = moment(datos[i].FechaCambio).format('DD/MM/YYYY');

            //tbdHistorico += "<td>" + FechaCambio + "</td>";
            tbdHistorico += "</tr>";
        };

        //Inyectar html en la página
        $("#tbdHistorico").html(tbdHistorico);


        
        $item = $("#tbdHistorico tr[data-id]");

        //Mapea los idficepi y excluye los repetidos.
        var UniqueNames = $.unique($item.map(function (d) {
            return $(this).attr("data-id");
        }));

        $("#spanTotal").text(UniqueNames.length);

        /*Controlamos si el contenedor tiene Scroll*/
        var div = document.getElementById('tbdHistorico');

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
        if (hasVerticalScrollbar) {
            $("thead").css("width", "calc( 100% - 1em )")
        }
        else { $("thead").css("width", "100%") }
        ///*FIN Controlamos si el contenedor tiene Scroll*/


        $("#tblHistorico").trigger("destroy");

        //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
        $("#tblHistorico").tablesorter({
            //dateFormat: "dd/mm/yy", // set the default date format
            // pass the headers argument and assing a object            
            headers: {
                // set "sorter : false" (no quotes) to disable the column
                0: {
                    sorter: "text"
                },

                1: {
                    sorter: "text"
                },
                2: {
                   sorter: "shortDate", dateFormat: "ddmmyyyy"
                }

            }
        });




    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}

function comprobarfechas() {
    try {
        $anoIni = $('#selAnoIni'); $mesIni = $('#selMesIni'); $anoFin = $('#selAnoFin'); $mesFin = $('#selMesFin');
        fecIni = parseInt($anoIni.val()) * 100 + parseInt($mesIni.val());
        fecFin = parseInt($anoFin.val()) * 100 + parseInt($mesFin.val());

        if (fecIni > fecFin) {
            alertNew('warning', 'Periodo seleccionado no lógico.');
            $("#tbdEval").html("");
            return false;
        } else
            return true;

    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar las fechas.");
    }
}



//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('input[type=text]').on('keyup', function (event) {

   

    //if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "") {
    //    $("#btnObtener").removeClass("show").addClass("hide");
    //    return;
    //}

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#tbdHistorico").length > 0) {
        $("#tbdHistorico").html("");
    }

    if (event.keyCode == 13) {

        if ($("#txtApellido1").val() == "" && $("#txtApellido2").val() == "" && $("#txtNombre").val() == "" && $("#tblgetFicepi li").length == 0) {
            alertNew("warning", "Tienes que seleccionar algún dato de profesional");
            return;
        }

        historicoRoles();
    }
    

});


$("select").on("change", function () {
    if ($("#txtApellido1").val() == "" && $("#txtApellido2").val() == "" && $("#txtNombre").val() == "") {
        alertNew("warning", "Tienes que seleccionar algún dato de profesional");
        return;
    }
    historicoRoles();
})


$("#btnRestablecer").on("click", restablecerFiltros);

function restablecerFiltros() {
    location.href = "Default.aspx";
}
