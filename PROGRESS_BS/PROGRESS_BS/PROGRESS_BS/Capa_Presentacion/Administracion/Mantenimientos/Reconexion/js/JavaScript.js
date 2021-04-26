
$(document).ready(function () {
    $('#inputApellido1').focus();
})


$("#btnObtener").on("click", function () {
    obtener();
})


//Botón cancelar de evaluador
$('#modal-evaluadores #btnCancelar').on('click', function () {
    $('#modal-evaluadores').modal('hide');
});


//Selección simple de evaluador
$('body').on('click', '#lisEvaluadores li', function (e) {
    $('#lisEvaluadores li').removeClass('active');
    $(this).addClass('active');

});

function obtener() {

    var $lisEvaluadores = $('#lisEvaluadores');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getProfesionales",   // Current Page, Method
        data: JSON.stringify({ t001_apellido1:$('#inputApellido1').val(), t001_apellido2:$('#inputApellido2').val(), t001_nombre:$('#txtNombre').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                limpiarFiltros();
                $("#btnObtener").removeClass("show").addClass("hide");
                $("#btnSeleccionar").css("display", "inline-block");
                $(result.d).each(function () { $("<li class='list-group-item' data-codred='"+ this.Codred +"' data-sexo='" + this.Sexo + "' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });                

            } else {
                alertNew("warning", "Ningún evaluador responde a la búsqueda por los filtros establecidos", null, 2000, null);
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluadores");
        }
    });
}


//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('input[type=text]').on('keyup', function (event) {

    $("#btnSeleccionar").css("display", "none");

    if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "") {
        $("#btnObtener").removeClass("show").addClass("hide");
        return;
    }

    $("#btnObtener").removeClass("hide").addClass("show");

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#lisEvaluadores").length > 0) {
        $("#lisEvaluadores").html("");
    }

    if (event.keyCode == 13) {

        if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "" && $("#tblgetFicepi li").length == 0) {
            alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
            return;
        }

        obtener();

    }
});

function limpiarFiltros() {
    $("#inputApellido1").val("");
    $("#inputApellido2").val("");
    $("#inputNombre").val("");
}

$("#btnAceptar").on("click", function () {

    if ($('#lisEvaluadores li.active').length == 0) {
        alertNew("warning", "No has seleccionado ningún profesional");
        return;
    } 
    
    var objetoSession = [$('#lisEvaluadores li.active').text(), $('#lisEvaluadores li.active').attr("data-codred")];

    $.ajax({
        url: "Default.aspx/establecerVariableSesion",   // Current Page, Method
        data: JSON.stringify({ datos: objetoSession}),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            location.href = strServer + "Capa_Presentacion/Home/home.aspx";
        },
        error: function (ex, status) {
            alertNew("danger", ex.message);
        }
    });
})