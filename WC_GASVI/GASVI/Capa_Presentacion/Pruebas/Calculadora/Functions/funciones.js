$(document).ready(function () {
    //Dialogo para mostrar los elementos no validados del CV de un profesional
    $("#divPendientes").dialog({
        autoOpen: false,
        resizable: false,
        width: 400,
        modal: true,
        open: function (event, ui) {
            // Hide close button
            //$(this).parent().children().children(".ui-dialog-titlebar-close").hide();
            //Prueba para cambiarle la imagen
            //$(this).parent().children().children(".ui-dialog-titlebar-close").css("background-image: url(../../../../images/Botones/imgCancelar.gif)");
        }
    });
    //$("#divPendientes").dialog("open");
    jqConfirm("", "La fecha de contabilización de las siguientes solicitudes<br />pertenece a un mes que tal vez se encuentre cerrado.<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
        if (answer) {
            alert("OK");
        }
        else {
            alert("KO");
        }
    });

});
