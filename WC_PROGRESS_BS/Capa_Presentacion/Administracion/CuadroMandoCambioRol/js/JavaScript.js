
$(document).ready(function () {
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "350px");
        }
    }

    //Llamada Ajax para obtener número de elementos de cada TILE
    getCountTiles();
})


function getCountTiles() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getCountTiles",        
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            //Hay que parsear el resultado JSON que devuelve el servidor
            pintarDatosPantalla(JSON.parse(result.d));
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido obtener los datos");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //Pintamos los números en los Tiles
        $("#numSolAprobadas").text(datos.n_solicitudes_APA);
        $("#numSolNoAprobadas").text(datos.n_solicitudes_DPA);
        $("#numSolPendAprobacion").text(datos.n_solicitudes_ASBY);
        $("#numSolPendNoAprobacion").text(datos.n_solicitudes_DSBY);   

    } catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }
}