$(document).ready(function () { SUPER.SIC.app.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app = (function (view, dal) {

    function init() {

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación.", IB.vars.error);
            return;
        }
        //IB.procesando.mostrar();
        view.init();

        view.attachLiveEvents("click", view.selector.edicion_accion, function () {
            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                //Quitamos la negrita 
                var payload = { idtarea: $(this).attr("data-idtarea") }
                dal.post(null, "quitarNegritasTareasPendientes", payload, null, function (data) {
                }
            );
            }

            var qs = IB.uri.encode("idAccion=" + $(this).attr("data-idaccion")  + "&idTarea=" + $(this).attr("data-idtarea") + "&modoPantalla=E");
            window.location.href = "../DetalleTarea/Default.aspx?" + qs;
        });
    }

    return {
        init: init
    }

})(SUPER.SIC.view, IB.DAL);