

//$(document).ready(function () {
    
//    $("#btnEnviar").on("click", function () {

//        if ($("#txtComentario").val().length == 0) {
//            alertNew("warning", "No se permite enviar un correo vacío.");
//            return;
//        };

//        $("#btnEnviar").prop("disabled", true);

//        $.ajax({
//            url: "Home.aspx/enviarCorreo",
//            data: JSON.stringify({ texto: $("#txtComentario").val() }),  // parameter map as JSON
//            type: "POST",
//            async: true,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            timeout: 20000,
//            success: function (result) {                
//                $("#txtComentario").val("");
//                $("#modal-correo").modal("hide");
//                $("#btnEnviar").prop("disabled", false);
//            },
//            error: function (ex, status) {
//                mostrarErrorAplicacion("Ocurrió un error al enviar el correo.", e.message)
//            }
//        });
//    })


//    $("#btnCancelar").on("click", function () {
//        $("#txtComentario").val("");        
//    })

//})

////Ponemos foco al campo comentario de la modal
//$(window).on('shown.bs.modal', function () {    
//    $("#txtComentario").focus();
//});





















//$(document).ready(function () {

   
    
//    //$("#divDecalogo").on("click", function () {

//    //    if ($("#contentDecalogo").get(0).style.display =="none") {            
//    //        $("#contentDecalogo").slideDown("slow");
//    //    }
//    //    else {            
//    //        $("#contentDecalogo").slideUp("slow");
//    //    }
        
//    //})

//    //$("#divProcedimiento").on("click", function () {

//    //    if ($("#contentProcedimiento").get(0).style.display == "none") {            
//    //        $("#contentProcedimiento").slideDown("slow");
//    //    }
//    //    else {            
//    //        $("#contentProcedimiento").slideUp("slow");
//    //    }

//    //})

//    //$("#divEjemplos").on("click", function () {

//    //    if ($("#contentEjemplos").get(0).style.display == "none") {            
//    //        $("#contentEjemplos").slideDown("slow");
//    //    }
//    //    else {            
//    //        $("#contentEjemplos").slideUp("slow");
//    //    }

//    //})
    
//    //var $active = true;

//    //$('.panel-title > a').click(function (e) {
//    //    e.preventDefault();
//    //});

  

//})


