$(document).ready(function () {

    controlarScroll();
    $("#tabla tbody tr td span:empty").addClass("fk-elementosvacios");
    cebrear();

    $(window).resize(function () {
        cebrear();
    });    

});

$('#divProf').on('click', function (event) {
    $('buscador-profesionales').attr('titulo', 'Selección de profesional');
    $('buscador-profesionales').removeAttr('chkbajas');
    $('buscador-profesionales').attr('chktodos', '');

    abrirBuscadorprofesionales();

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable').attr('aria-hidden', 'true');
});

function controlarScroll() {
    /*Controlamos si el contenedor tiene Scroll*/
    var div = document.getElementById('bodyTabla');

    var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
    if (hasVerticalScrollbar) {
        $("#tabla thead").css("width", "calc( 100% - 1em )")
    }
    else { $("#tabla thead").css("width", "100%") }
    /*FIN Controlamos si el contenedor tiene Scroll*/
}

/*Cebrea las líneas visualizadas*/
function cebrear() {

    $('#bodyTabla > tr:visible, #bodyTablaC > tr:visible').removeClass("cebreada");
    $('#bodyTabla > tr:visible:even, #bodyTablaC > tr:visible:even').addClass('cebreada');
}

$('#btnProyecto').on('click', function (event) {

    $('#modal-Proyecto').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-Proyecto').modal('show');

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable').attr('aria-hidden', 'true');
});

$('.lblCliente').on('click', function (event) {

    $('#modal-Cliente-Proyecto').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-Cliente-Proyecto').modal('show');
    $('#modal-Cliente-Proyecto').attr('data-origen', $(this).attr('data-origen'))

    //Ocultamos el contenido inferior al lector de pantallas
    if ($(this).attr('data-origen') === 'P') {
        $('.ocultable2').attr('aria-hidden', 'true');
    }
    else {
        $('.ocultable3').attr('aria-hidden', 'true');
    }       
});

$('#lblCR').on('click', function (event) {

    $('#modal-CR').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-CR').modal('show');

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable2').attr('aria-hidden', 'true');
});

$('#lblQn').on('click', function (event) {
    $('#tituloQn').html(':::SUPER::: Selección de Cualificador Qn')
    abrirModalQ();
});

$('#lblQ1').on('click', function (event) {
    $('#tituloQn').html(':::SUPER::: Selección de Cualificador Q1')
    abrirModalQ();
});

$('#lblQ2').on('click', function (event) {
    $('#tituloQn').html(':::SUPER::: Selección de Cualificador Q2')
    abrirModalQ();
});

function abrirModalQ() {
    $('#modal-Qn').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-Qn').modal('show');

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable2').attr('aria-hidden', 'true');
}


$('#lblResp').on('click', function (event) {

    $('buscador-profesionales').attr('titulo', 'Selección de responsable de proyecto');
    $('buscador-profesionales').removeAttr('chktodos');
    $('buscador-profesionales').attr('chkbajas', '');
    abrirBuscadorprofesionales();

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable2').attr('aria-hidden', 'true');
});

$('#lblContrato').on('click', function (event) {

    $('#modal-Contrato-Proyecto').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-Contrato-Proyecto').modal('show');

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable2').attr('aria-hidden', 'true');
});

$('#modal-Contrato-Proyecto').on('show.bs.modal', function (e) {
    $("[class*=treegrid-parent]").hide();
    setTimeout(function () {
        cebrear();
    }, 300);
});



$('#lblHor').on('click', function (event) {

    $('#modal-Horizontal-Proyecto').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-Horizontal-Proyecto').modal('show');

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable2').attr('aria-hidden', 'true');
});

$('#btnCliente').on('click', function (event) {

    $('#modal-Cliente').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-Cliente').modal('show');

    //Ocultamos el contenido inferior al lector de pantallas
    $('.ocultable').attr('aria-hidden', 'true');
});


$(document).on('hidden.bs.modal', '.modal', function () {
    $('.modal:visible').length && $(document.body).addClass('modal-open');
});

//Click sobre tabla de selección de contrato
$(document).on("keypress click", '.nombreLineaC', function (e) {

    if ($(this).children().hasClass('glyphicon-plus')) {

        var clase = $(this).parent().attr('class');

        //separamos por - y depués por espacios y en res[0] siempre se nos quedará el nodo pulsado
        var res = clase.split("-");
        var res = res[1].split(" ");

        //Cambia el glyphicon
        $(this).children(':not(.fk-label)').toggleClass("glyphicon-minus", true).removeClass("glyphicon-plus");        

        //Abre los hijos del nodo pulsado
        $("#bodyTablaC .treegrid-parent-" + res[0] + "").show(500, function () {
            cebrear();
        });

        //dejamos todos los iconos en +
        $(".treegrid-parent-" + res[0] + "").find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");

    }
    else {

        var clase = $(this).parent().attr('class');

        //separamos por - y depués por espacios y en res[0] siempre se nos quedará el nodo pulsado
        var res = clase.split("-");
        var res = res[1].split(" ");

        //cierra los descendientes del nodo pulsado
        $(".treegrid-parent-" + res[0] + "").hide("slow", function () {
            cebrear();
        });

        //Cambia el glyphicon en las dos tablas
        $(this).children(':not(.fk-label)').toggleClass("glyphicon-plus", true).removeClass("glyphicon-minus");        

    }

    e.preventDefault();
    e.stopPropagation();

});

$("#buscador").keyup(function () {

    //separar el contenido del input
    var data = this.value;
    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //var data = this.value.split(" ");

    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //quitamos la búsqueda de espacios
    //var removeItem = "";

    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //data = jQuery.grep(data, function (value) {
    //    return value != removeItem;
    //});

    //creamos el objeto línea
    var linea = $("#bodyTablaC").find("tr.linea");

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
        if (normalize($t.text()).toUpperCase().indexOf(normalize(data.toUpperCase())) > -1) {
            return true;
        }
        //Sustituye a las lineas superiores en caso de querer una búsqueda OR
        //for (var d = 0; d < data.length; ++d) {
        //    if (normalize($t.text()).toUpperCase().indexOf(normalize(data[d].toUpperCase())) > -1) {
        //        return true;
        //    }        
        return false;
    })
    //Mostrar las lineas coincidentes.    
    .show();
    
    //Mostrar la jerarquia superior de las lineas coincidentes
    $("#bodyTablaC").find("tr.linea:visible").each(function (index) {
        var clase = $(this).attr('class');
        var res = clase.split(" ");

        //Si no es nodo de mayor nivel
        if (typeof res[2] !== "undefined") {
            var res = res[2].split("-");
            if (!$(".treegrid-" + res[2] + "").is(":visible")) {
                $(".treegrid-" + res[2] + "").show();

                if (typeof res[2] !== "undefined") {
                    var clase2 = $($(".treegrid-" + res[2] + "")).attr('class');
                    var res2 = clase2.split(" ");
                    var res2 = res2[2].split("-");
                    $(".treegrid-" + res2[2] + "").show();

                    }
                }
            }
    })
    cebrear();
    cambiarIconos();
});

/*Función que cambia los glyphicon a las líneas que estén abiertas*/
function cambiarIconos() {
    $("#bodyTablaC").find("tr.linea:visible").each(function (index) {
        var clase = $(this).attr('class');
        var res = clase.split(" ");
        var res = res[1].split("-");

        /*Si su hijo está abierto y él tiene icono lo cambiamos a abierto*/
        if ($(".treegrid-parent-" + res[1] + "").is(":visible") && $(this).children().children().is('[class^=glyphicon]')) {
            $(this).children().children('span:first').removeClass('glyphicon-plus').addClass('glyphicon-minus');
        }

        /*Si su hijo está cerrado y él tiene icono lo cambiamos a cerrado*/
        if (!$(".treegrid-parent-" + res[1] + "").is(":visible") && $(this).children().children().is('[class^=glyphicon]')) {
            $(this).children().children('span:first').removeClass('glyphicon-minus').addClass('glyphicon-plus');
        }
    });
}

function abrirBuscadorprofesionales() {
    $('#buscProfesional').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#buscProfesional').modal('show');
};

$(document).on('hidden.bs.modal', '#modal-Proyecto, #modal-Cliente', function () {
    $('.ocultable').attr('aria-hidden', 'false');
});

$(document).on('hidden.bs.modal', '#modal-CR, #modal-Contrato-Proyecto, #modal-Horizontal-Proyecto, #modal-Qn', function () {
    $('.ocultable2').attr('aria-hidden', 'false');
});

$(document).on('hidden.bs.modal', '#modal-Cliente-Proyecto', function () {
    if ($(this).attr('data-origen') === 'P') {
        $('.ocultable2').attr('aria-hidden', 'false');
    }
    else {
        $('.ocultable3').attr('aria-hidden', 'false');
    }
});

//El buscador de profesionales puede abrirse desde la pantalla principal como desde el modal de proyectos por lo que 
//al cerrarlo hay que comprobar que contenido oculto al lector de pantallas hay que poner visible de nuevo
$(document).on('hidden.bs.modal', '#buscProfesional', function () {
    if ($('buscador-profesionales').attr('titulo') === 'Selección de profesional') {
        $('.ocultable').attr('aria-hidden', 'false');
    }
    else {
        $('.ocultable2').attr('aria-hidden', 'false');
    }    
});

$("#buscadorHorizontal").keyup(function () {

    //separar el contenido del input
    var data = this.value;
    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //var data = this.value.split(" ");

    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //quitamos la búsqueda de espacios
    //var removeItem = "";

    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //data = jQuery.grep(data, function (value) {
    //    return value != removeItem;
    //});

    //creamos el objeto línea
    var linea = $("#ulHorizontalProyectos").find(".list-group-item");

    //Si el input está vacio mostramos solo las líneas de los proyectos
    if (this.value == "") {
        linea.show();
        return;
    }
    //ocultamos todas las líneas
    linea.hide();

    //Filtramos recursivamente las lineas para obtener el resultado    
    linea.filter(function (i, v) {
        var $t = $(this);
        if (normalize($t.text()).toUpperCase().indexOf(normalize(data.toUpperCase())) > -1) {
            return true;
        }
        //Sustituye a las lineas superiores en caso de querer una búsqueda OR
        //for (var d = 0; d < data.length; ++d) {
        //    if (normalize($t.text()).toUpperCase().indexOf(normalize(data[d].toUpperCase())) > -1) {
        //        return true;
        //    }        
        return false;
    })
    //Mostrar las lineas coincidentes.    
    .show();
});

$("#buscadorCR").keyup(function () {

    //separar el contenido del input
    var data = this.value;
    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //var data = this.value.split(" ");

    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //quitamos la búsqueda de espacios
    //var removeItem = "";

    //Sustituye a las lineas superiores en caso de querer una búsqueda OR
    //data = jQuery.grep(data, function (value) {
    //    return value != removeItem;
    //});

    //creamos el objeto línea
    var linea = $("#ulCR").find(".list-group-item");

    //Si el input está vacio mostramos solo las líneas de los proyectos
    if (this.value == "") {
        linea.show();
        return;
    }
    //ocultamos todas las líneas
    linea.hide();

    //Filtramos recursivamente las lineas para obtener el resultado    
    linea.filter(function (i, v) {
        var $t = $(this);
        if (normalize($t.text()).toUpperCase().indexOf(normalize(data.toUpperCase())) > -1) {
            return true;
        }
        //Sustituye a las lineas superiores en caso de querer una búsqueda OR
        //for (var d = 0; d < data.length; ++d) {
        //    if (normalize($t.text()).toUpperCase().indexOf(normalize(data[d].toUpperCase())) > -1) {
        //        return true;
        //    }        
        return false;
    })
    //Mostrar las lineas coincidentes.    
    .show();
});



