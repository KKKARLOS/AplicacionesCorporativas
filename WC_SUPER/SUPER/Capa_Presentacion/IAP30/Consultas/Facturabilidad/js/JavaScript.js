//$(document).ready(function () {

//    controlarScroll();
//    $("#tabla tbody tr td span:empty").addClass("fk-elementosvacios");
//    cebrear();
//});
//function controlarScroll() {
//    /*Controlamos si el contenedor tiene Scroll*/
//    var div = document.getElementById('bodyTabla');

//    var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
//    if (hasVerticalScrollbar) {
//        $("#tabla thead").css("width", "calc( 100% - 1.3em )")
//    }
//    else { $("#tabla thead").css("width", "100%") }
//    /*FIN Controlamos si el contenedor tiene Scroll*/
//}

/*Cebrea las líneas visualizadas*/
//function cebrear() {

//    $("#bodyTabla > tr:visible").removeClass("cebreada");
//    $('#bodyTabla > tr:visible:even').addClass('cebreada');
//}
/*
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
    var linea = $("#bodyTabla").find("tr.linea");

    //Si el input está vacio mostramos solo las líneas de los proyectos
    if (this.value == "") {
        linea.show();
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
    cebrear();
});
*/