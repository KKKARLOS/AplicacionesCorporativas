$(document).ready(function () { });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app_ModalArea = (function (view) {

    if (IB.vars.error) {
        IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
        return;
    }

    view.init();

    function detalleAreaPreventa() {

    }



})(SUPER.SIC.view_ModalArea);