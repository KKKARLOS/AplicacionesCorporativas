/// <reference path="../../../../../scripts/IB.js" />

$(document).ready(function () { SUPER.SIC.appCatAccion.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appCatAccion = (function (view, dal) {

    var _ta206_itemorigen = IB.vars.ta206_itemorigen;
    var _ta206_iditemorigen = IB.vars.ta206_iditemorigen;

    function init() {

        console.log("appCatAccion.init");

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        view.init();

        view.attachLiveEvents("click", view.selector.edicion_accion, function () {

            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                    //Quitamos la negrita de la acción seleccionada
                    var payload = { idaccion: $(this).attr("data-idaccion") }
                    dal.post(null, "quitarNegritasTareasPendientes", payload, null,function (data) {
                    }                    
                );
            }
        
            var qs = IB.uri.encode("modo=E&id=" + $(this).attr("data-idaccion") + "&origenpantalla=SUPER&caller=autoasignacion");
            window.location.href = "../Detalle/Default.aspx?" + qs;
        });


    }


    return {
        init: init
    }

})(SUPER.SIC.viewCatAccion, IB.DAL);