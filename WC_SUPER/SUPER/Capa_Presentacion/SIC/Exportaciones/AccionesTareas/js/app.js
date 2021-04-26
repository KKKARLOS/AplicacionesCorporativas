/// <reference path="../../../../../scripts/IB.js" />

$(document).ready(function () { SUPER.SIC.app.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app = (function (view) {


    function init() {


        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        view.init();

    }



    return {
        init: init
    }

})(SUPER.SIC.view);