var IB = IB || {};
IB.SUPER = IB.SUPER || {};

//Vista Avisos Home
IB.SUPER.AvisosView = (function () {

    var dom = {
        divmodal: $("#modal-avisos"),
        modal_title: $("#modal-avisos .modal-title"),        
        opcionIAP: $("#spanIAP"),
        opcionPST: $("#spanPST"),
        opcionPGE: $("#spanPGE"),
        contenidoAviso: $("#modal-avisos .modal-body #txtContenidoAviso"),
        totalAvisos: $("#lblTotalAvisos"),
        numAviso: $("#lblNumAviso"),
        btnConservar: $("#btnConservar"),
        btnDestruir: $("#btnDestruir")    
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    var init = function (totalAvisos) {

        dom.divmodal.modal("show");
        dom.totalAvisos.html(totalAvisos);
    }
 
    var render = function (datos, numAviso) {

        //Habilitamos y deshabilitamos las imágenes de IAP, PST y PGE
        if (datos.t448_IAP) dom.opcionIAP.removeClass("disabled").addClass("enabled");
        if (datos.t448_PST) dom.opcionPST.removeClass("disabled").addClass("enabled");
        if (datos.t448_PGE) dom.opcionPGE.removeClass("disabled").addClass("enabled");

        //Avisos totales y aviso actual
        dom.numAviso.html(numAviso)

        //Datos a presentar en la modal
        dom.modal_title.html(datos.t448_titulo);
        dom.modal_title.attr("data-t448_idaviso", datos.t448_idaviso);        
        dom.contenidoAviso.html(datos.t448_texto);

    }

    var ocultar = function () {
        dom.divmodal.modal("hide");
    }

    return {
        dom: dom,
        init: init,
        render: render,
        ocultar: ocultar,
        attachEvents: attachEvents
    }

})()

