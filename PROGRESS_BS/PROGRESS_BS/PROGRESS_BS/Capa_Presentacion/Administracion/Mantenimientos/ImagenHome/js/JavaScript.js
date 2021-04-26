var bCambios = false;

$(document).ready(function () {


    $(window).on('beforeunload', function () {
        if (bCambios) return 'Hay cambios que no se han guardado.';
    });


    $('#btnSelectImage').on('click', function () {

        $('#imageupload').fileinput('clear');

        $('#modal-selectimage').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#modal-selectimage').modal('show');
    });

    $('#btnGrabar').on('click', grabarImagen);

    $("#imageupload").fileinput({
        uploadUrl: "UploadImage.ashx", // server upload action
        uploadAsync: true,
        maxFileCount: 1,
        language: "es",
        allowedFileExtensions: ["jpg", "png", "gif"]
    });
    
    $('#imageupload').on('fileuploaded', function (event, data, previewId, index) {
        //cerrar modal y actualizar imagen pantalla
        bCambios = true;
        $('#modal-selectimage').modal('hide');
        actualizarImagenPantalla()
    });
});

function comprobarCambiosSalir() {
    alert();
}

function actualizarImagenPantalla()
{
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getBase64ImageFromSession",   // Current Page, Method
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function(result){
            $("#imghome").attr("src", "data:image/jpeg;base64," + result.d)
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar actualizar.");
        }

    });
}

function grabarImagen() {

    if (!bCambios) {
        alertNew("warning", "No se ha seleccionado una nueva imagen para grabar");
        retrun;
    }

    actualizarSession();
    $.ajax({
        url: "Default.aspx/grabarImagen",   // Current Page, Method
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            bCambios = false;
            location.href = strServer + "Capa_Presentacion/Home/Home.aspx";

        },
        error: function (ex, status) {
            alertNew("danger", "Error al grabar la nueva imagen.");
        }

    });
}

