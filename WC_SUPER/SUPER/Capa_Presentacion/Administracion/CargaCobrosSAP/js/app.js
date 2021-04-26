$(document).ready(function () {
    SUPER.APP.CobrosSAP.app.init();
})

var SUPER = SUPER || {};
SUPER.APP = SUPER.APP || {};
SUPER.APP.CobrosSAP = SUPER.APP.CobrosSAP || {}

SUPER.APP.CobrosSAP.app = (function (view) {

    var init = function () {
        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }
        view.init();
        view.attachEvents("click", view.dom.btnProcesar, procesar);
        IB.procesando.ocultar()
    }

    var procesar = function () {
        var defer = $.Deferred();
        if (!comprobarDatos())
            return;

        //view.resalta(1);
        IB.procesando.mostrar();
        var payload = {};
        IB.DAL.post(null, "borrarSaldos", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    //view.visualizarContenedor();
                    cargaSaldos();
                    defer.resolve();
                });
            },
            function (ex, status) {
                //view.resaltaError(1);
                IB.bserror.error$ajax(ex, status)
            },
            120000
        );
        return defer.promise();
    }

    var cargaSaldos = function () {
        var defer = $.Deferred();
        //view.resalta(2);
        IB.procesando.mostrar();
        var payload = { fecha: view.dom.txtFecha.val() };
        IB.DAL.post(null, "cargaSaldos", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    //view.visualizarContenedor();
                    generarCobros();
                    defer.resolve();
                });
            },
            function (ex, status) {
                //view.resaltaError(2);
                IB.bserror.error$ajax(ex, status)
            },
            1200000
        );
        return defer.promise();
    }

    var generarCobros = function () {
        var defer = $.Deferred();
        //view.resalta(3);
        IB.procesando.mostrar();
        var payload = { fecha: view.dom.txtFecha.val() };
        IB.DAL.post(null, "generarCobros", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    view.visualizarContenedor();
                    grabacionCorrecta(data);
                    defer.resolve();
                });
            },
            function (ex, status) {
                //view.resaltaError(3);
                IB.bserror.error$ajax(ex, status)
            },
            1200000
        );
        return defer.promise();
    }

    var grabacionCorrecta = function (sMsg) {
        //view.resalta(4);
        if (sMsg == 0) IB.bsalert.fixedToast("No se ha cargado ningún cobro.", "info");
        else IB.bsalert.fixedToast("Los cobros se han cargado correctamente.", "info");
        //desactivarGrabar();
    }

    function comprobarDatos() {
        var bOK = true;
        if (view.dom.txtFecha.val() == "") {
            bOK = false;
            IB.bsalert.toastdanger("Debes indicar una fecha.");
        }
        return bOK;
    }


    return {
        init: init
    }

})(SUPER.APP.CobrosSAP.View);
