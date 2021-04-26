/// <reference path="../../../../../scripts/IB.js" />

$(document).ready(function () { SUPER.SIC.appCatAccion.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appCatAccion = (function (view) {

    var _ta206_itemorigen = IB.vars.ta206_itemorigen;
    var _ta206_iditemorigen = IB.vars.ta206_iditemorigen;

    function init() {

        console.log("appCatAccion.init");

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        view.init(_ta206_itemorigen, _ta206_iditemorigen);

        view.attachLiveEvents("click", view.selector.linkImputaciones, showModal);
        view.attachLiveEvents("click", view.selector.edicion_accion, function () {
            var qs = IB.uri.encode("modo=E&id=" + $(this).attr("data-idaccion") + "&itemorigen=" + _ta206_itemorigen + "&iditemorigen=" + _ta206_iditemorigen + "&origenpantalla=CRM");
            window.location.href = "../Detalle/Default.aspx?" + qs;
        });
        
        $("#btnInsert").on("click", function () {
            var qs = IB.uri.encode("modo=A&itemorigen=" + _ta206_itemorigen + "&iditemorigen=" + _ta206_iditemorigen + "&origenpantalla=CRM");
            window.location.href = "../Detalle/Default.aspx?" + qs;
        })

        obtenerInfoCRM();                
    }

    function obtenerInfoCRM() {

        var payload = { ta206_itemorigen: _ta206_itemorigen, ta206_iditemorigen: _ta206_iditemorigen }
        return IB.DAL.post(IB.vars.strserver + "Capa_Presentacion/SIC/Services/CRM.asmx", "ObtenerDatosCRM", payload, null, view.pintaInfoCRM);
    }

    

    function showModal() {
        IB.procesando.mostrar();

        var payload = { ta206_itemorigen: _ta206_itemorigen, ta206_iditemorigen: _ta206_iditemorigen }
        IB.DAL.post(null, "imputaciones", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    view.rendermodalImputaciones(JSON.parse(data));                    
                })
            }
        );        
    }

    return {
        init: init
    }

})(SUPER.SIC.viewCatAccion);