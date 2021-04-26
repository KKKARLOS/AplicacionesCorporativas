
$(document).ready(function () {
    getTemporada();
})


function getTemporada() {
    $.ajax({
        url: "Default.aspx/getTemporada",   // Current Page, Method
        type: "POST",        
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#inputTemporada").val(result.d[0].Temporada);
            $("#inputPeriodo").val(result.d[0].periodoprogress);
        },
        error: function (ex, status) {
            alertNew("danger", "Error al obtener la temporada Progress.");
        }

    });
}

$("#btnGrabar").on("click", function () {
    var oDatos = new Object();
    oDatos.temporadaprogress = $("#inputTemporada").val();
    oDatos.periodoprogress = $("#inputPeriodo").val();

    grabar(oDatos);
})

function grabar(oDatos) {

    if (oDatos.temporadaProgress == "" || oDatos.periodoProgress == "") {
        alertNew("warning", "Los campos no pueden estar vacíos.");
        return;
    }

    $.ajax({
        url: "Default.aspx/updateTemporada",   // Current Page, Method
        type: "POST",
        "data": JSON.stringify({ oDatos: oDatos}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            alertNew("success", "Temporada y período grabados.");
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar actualizar.");
        }

    });
}


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

