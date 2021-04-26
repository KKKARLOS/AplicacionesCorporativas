$(document).ready(function () {


    $("#btnBuscar").on("click", buscar);
    $("#btnLimpiar").on("click", limpiar);
    $(document).on("change", "#contenido > tr > td > input", actualizar);
    //$("#contenido > tr > td > input").on("change", actualizar);

});

function buscar() {

    var filtro = $("#txtfiltro").val();


    $.ajax({
        url: "Default.aspx/buscar",
        data: JSON.stringify({filtro: filtro}),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 10000,
        success: function (result) {
            pintarTabla(result.d);     
        },
        error: function (ex, status) {
            alert("error");
        }
    });
}

function limpiar() {

    $("#txtfiltro").val("");
    $("#contenido").html("");

}

function pintarTabla(filas) {
    
    var h = "";

    for (var i = 0; i < filas.length; i++) {

        h += "<tr data-idficepi='" + filas[i].idficepi + "' >" + 
             "<td><input class='fk_nombre' type='text' value='" + filas[i].nombre + "' /></td>";
        h += "<td><input class='fk_apellido' type='text' value='" + filas[i].apellido + "' /></td></tr>";
    }
    $("#contenido").append(h);

    //$("#contenido > tr > td > input").off("change").on("change", actualizar);
    
   

}

function actualizar() {

   

    var datos = new Object();

    datos.idficepi = $(this).parent().parent().attr("data-idficepi");

    var tr = $("#contenido > tr[data-idficepi='" + datos.idficepi + "']");

    datos.nombre = $(tr).find(".fk_nombre").val();
    datos.apellido = $(tr).find(".fk_apellido").val();

    $.ajax({
        url: "Default.aspx/actualizar",
        data: JSON.stringify({ datos: datos}),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 10000,
        success: function (result) {
            alert(result.d);
        },
        error: function (ex, status) {
            alert("error");
        }
    });

}