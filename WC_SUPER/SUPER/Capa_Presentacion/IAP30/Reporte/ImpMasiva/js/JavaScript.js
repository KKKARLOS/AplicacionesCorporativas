$('#lblTarea').on('click', function (event) {
    $('#buscTarea').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#buscTarea').modal('show');
    $('.ocultable').attr('aria-hidden', 'true');
});

$(document).on('hidden.bs.modal', '.modal', function () {
    $('.ocultable').attr('aria-hidden', 'false');
});

/*$(document).ready(function () {

    //Contraer todas las ramas menos los proyectos económicos
    $("[class*=treegrid-parent]").hide();
    cebrear();

    $(window).resize(function () {
        cebrear();
    });

    $('#buscTarea').on('shown.bs.modal', function () {
        cebrear();
        cambiarIconos();
    })    

});

/*Click sobre la línea de la tabla horizontal
$(document).on("click", '.nombreLinea', function (e) {
    if ($(this).children().hasClass('glyphicon-plus')) {
        var clase = $(this).parent().attr('class');

        //separamos por - y depués por espacios y en res[0] siempre se nos quedará el nodo pulsado
        var res = clase.split("-");
        var res = res[1].split(" ");

        $(this).children().toggleClass("glyphicon-minus", true).removeClass("glyphicon-plus");

        //Abre los hijos del nodo pulsado
        $(".treegrid-parent-" + res[0] + "").show(500);

        //dejamos todos los iconos en +
        $(".treegrid-parent-" + res[0] + "").find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
    }
    else {
        var clase = $(this).parent().attr('class');

        //separamos por - y depués por espacios y en res[0] siempre se nos quedará el nodo pulsado
        var res = clase.split("-");
        var res = res[1].split(" ");

        //cierra los descendientes del nodo pulsado
        $(".treegrid-parent-" + res[0] + ":visible").each(function () {
            var clase2 = $(this).attr('class');
            var res2 = clase2.split("-");
            var res2 = res2[1].split(" ");
            $(".treegrid-parent-" + res2[0] + ":visible").each(function () {
                var clase3 = $(this).attr('class');
                var res3 = clase3.split("-");
                var res3 = res3[1].split(" ");
                $(".treegrid-parent-" + res3[0] + ":visible").each(function () {
                    var clase4 = $(this).attr('class');
                    var res4 = clase4.split("-");
                    var res4 = res4[1].split(" ");
                    $(".treegrid-parent-" + res4[0] + ":visible").hide(500);

                    //cierra a si mismo                     
                    $(this).hide("slow", function () {
                        cebrear();
                    });
                })

                //cierra a si mismo
                $(this).hide("slow", function () {
                    cebrear();
                });
            })

            //cierra a si mismo
            $(this).hide("slow", function () {
                cebrear();
            });
        })


        $(this).children().toggleClass("glyphicon-plus", true).removeClass("glyphicon-minus");
    }

    cebrear();

    e.preventDefault();
    e.stopPropagation();
});
*/
/*Cebrea las líneas visualizadas
function cebrear() {

    $("tr:visible").removeClass("cebreada");
    $('tr:visible:even').addClass('cebreada');
}*/

/*Click de apertura de modal
$('#lblTarea').on('click', function (event) {
    $("[class*=treegrid-parent]").hide();
    $('#buscTarea').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#buscTarea').modal('show');
});*/


/*Click de selección de tarea
$(document).on("click", '.tarea', function (e) {
    $('#buscTarea').modal('hide');
});*/
/*
$("#buscaTareas").keyup(function () {
    //Quitamos el crebreado
    $("tr:visible").removeClass("cebreada");

    //separar el contenido del input
    var data = this.value.split(" ");
    //var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();    


    //quitamos la búsqueda de espacios
    var removeItem = "";

    data = jQuery.grep(data, function (value) {
        return value != removeItem;
    });

    //creamos el objeto línea
    var linea = $("#tbodyProyectos").find("tr.linea");

    //Si el input está vacio mostramos solo las líneas de los proyectos
    if (this.value == "") {
        linea.show();
        $("[class*=treegrid-parent]").hide();
        cambiarIconos();
        cebrear();
        return;
    }
    //ocultamos todas las líneas
    linea.hide();

    //Filtramos recursivamente las lineas para obtener el resultado    
    linea.filter(function (i, v) {
        var $t = $(this);
        for (var d = 0; d < data.length; ++d) {
            if (normalize($t.text()).toUpperCase().indexOf(normalize(data[d].toUpperCase())) > -1) {
                return true;
            }
        }
        return false;
    })
    //Mostrar las lineas coincidentes.    
    .show();

    //Mostrar la jerarquia superior de las lineas coincidentes
    $("#tbodyProyectos").find("tr.linea:visible").each(function (index) {
        var clase = $(this).attr('class');
        var res = clase.split(" ");

        //Si no es nodo de mayor nivel
        if (typeof res[2] !== "undefined") {
            var res = res[2].split("-");
            if (!$(".treegrid-" + res[2] + "").is(":visible")) {
                $(".treegrid-" + res[2] + "").show();

                var clase2 = $($(".treegrid-" + res[2] + "")).attr('class');
                var res2 = clase2.split(" ");
                var res2 = res2[2].split("-");
                $(".treegrid-" + res2[2] + "").show();

                var clase3 = $($(".treegrid-" + res2[2] + "")).attr('class');
                var res3 = clase3.split(" ");
                if (typeof res3[2] !== "undefined") {
                    var res3 = res3[2].split("-");
                    $(".treegrid-" + res3[2] + "").show();

                    var clase4 = $($(".treegrid-" + res3[2] + "")).attr('class');
                    var res4 = clase4.split(" ");
                    if (typeof res4[2] !== "undefined") {
                        var res4 = res4[2].split("-");
                        $(".treegrid-" + res4[2] + "").show();
                    }
                }
            }
        }
    })
    cebrear();
    cambiarIconos();
});
*/
/*Función que cambia los glyphicon a las líneas que estén abiertas*/
/*function cambiarIconos() {
    $("#tbodyProyectos").find("tr.linea:visible").each(function (index) {
        var clase = $(this).attr('class');
        var res = clase.split(" ");
        var res = res[1].split("-");
       
        //Si su hijo está abierto y él tiene icono lo cambiamos a abierto
        if ($(".treegrid-parent-" + res[1] + "").is(":visible") && $(this).children().children().is('[class^=glyphicon]')) {
            $(this).children().children().removeClass('glyphicon-plus').addClass('glyphicon-minus');
        }

        //Si su hijo está cerrado y él tiene icono lo cambiamos a cerrado
        if (!$(".treegrid-parent-" + res[1] + "").is(":visible") && $(this).children().children().is('[class^=glyphicon]')) {
            $(this).children().children().removeClass('glyphicon-minus').addClass('glyphicon-plus');
        }
    });
}*/