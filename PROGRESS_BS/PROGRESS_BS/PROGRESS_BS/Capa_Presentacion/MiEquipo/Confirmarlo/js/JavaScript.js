$(document).ready(function () {
    comprobarerrores();

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "350px");
        }
    }

    $("a[data-toggle=tooltip]").tooltip();
    try {
        //Click en el botón confirmar equipo 
        $('#divSalTramite button').on("click", function () { grabar(this); });
    } catch (e) {
        alertNew("danger", "Ocurrió un error al iniciar la página.");
    }

    /*Controlamos si el contenedor tiene Scroll*/
    var div = document.getElementById('idtbody');

    var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
    if (hasVerticalScrollbar) {
        $("#tablaConfirmarlo th:eq(0)").css("width", "49%");
        
    }
    else {

        $("#tablaConfirmarlo th:eq(0)").css("width", "50%");
        
    }
    /*FIN Controlamos si el contenedor tiene Scroll*/

    $("#spanNumero").text($("#idtbody tr").length);


});

function grabar(button) {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/confirmarEquipo",   // Current Page, Method
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#dateConfirm").val(result.d.toString());
            var but = $(button).prop("disabled", true);
            but.removeClass("btn-primary").addClass("btn-default");
            alertNew("success", "Equipo confirmado. Muchas gracias.");
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar confirmar mi equipo");
        }
    });
}
