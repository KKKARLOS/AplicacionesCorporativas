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

        view.attachEvents("change", view.dom.cmbTipoAsignacion, cmbTipoAsignacion_onClick)

        view.attachLiveEvents("click", view.selector.edicion_accion, function () {

            var el = this;

            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                //Quitamos la negrita 
                var payload = { idaccion: $(this).attr("data-idaccion") }
                dal.post(null, "quitarNegritasTareasPendientes", payload, null, function (data) {
                }
            );
            }

            //Guardar filtros en el historial y navegar al detalle
            var params = "filters=";
            params += "cmbTipoAsignacion:" + view.getcmbTipoAsignacion_value();
            //params += "|filtro2:valorfiltro21;valorfiltro22;valorfiltro23";
            params = IB.uri.encode(params);
            var newurl = location.href.substr(0, location.href.indexOf("?") != -1 ? location.href.indexOf("?") : location.href.length) + "?" + params;

            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
                function () {
                    var qs = IB.uri.encode("modo=E&id=" + $(el).attr("data-idaccion") + "&origenpantalla=SUPER&caller=catalogopdtelider");
                    window.location.href = "../Detalle/Default.aspx?" + qs;
            });

        });


    }

    function cmbTipoAsignacion_onClick() {

        var filter = view.getcmbTipoAsignacion_value();
        if (filter == -1) filter = null;
        view.refreshDatatable(filter);

    }

    return {
        init: init
    }

})(SUPER.SIC.viewCatAccion, IB.DAL);