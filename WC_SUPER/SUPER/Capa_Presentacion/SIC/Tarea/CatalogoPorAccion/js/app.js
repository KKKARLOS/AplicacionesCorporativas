$(document).ready(function () { SUPER.SIC.app.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app = (function (view, dal) {
    var bcambios = false;
    var _ta206_itemorigen = IB.vars.ta206_itemorigen;
    var _ta206_iditemorigen = IB.vars.ta206_iditemorigen;

    function init() {

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación.", IB.vars.error);
            return;
        }

        view.init(_ta206_itemorigen, _ta206_iditemorigen);

        //Obtiene datos del CRM   
        if (_ta206_itemorigen === "S")
            obtenerInfoSuper();
        else
            obtenerInfoCRM();

        view.attachLiveEvents("click", view.selector.edicion_accion, function () {

            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                //Quitamos la negrita 
                var payload = { idtarea: $(this).attr("data-idtarea") }
                dal.post(null, "quitarNegritasTareasPendientes", payload, null, function (data) {
                }
            );
            }

            var qs = IB.uri.encode("idAccion=" + $(this).attr("data-idaccion") + "&idTarea=" + $(this).attr("data-idtarea") + "&modoPantalla=E");
            window.location.href = "../DetalleTarea/Default.aspx?" + qs;
        });


        view.attachEvents("click", view.dom.btnAddTarea, btnAddTarea_onClick);
        view.attachEvents("click", view.dom.btnCerrar, btnCerrar_onClick);

        
    }

    function btnAddTarea_onClick() {
        var qs = IB.uri.encode("idAccion=" + IB.vars.ta204_idaccionpreventa + "&modoPantalla=A");
        window.location.href = "../DetalleTarea/Default.aspx?" + qs;
    }

    function btnCerrar_onClick() {
        //redirigir donde sea pertinente        
        IB.vars.bcambios = false;
        regresar();
    }

    function regresar() {

        var payload = {
            urlActual: location.href
        }
        if (bcambios) {
            $.when(IB.bsconfirm.confirmCambios())
                .then(function () {
                    bcambios = false;
                    IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Leer", payload, null,
                        function (data) {
                            if (data != "") window.location.href = data;
                        });
                })
        }
        else {
            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Leer", payload, null,
                function (data) {
                    if (data != "") window.location.href = data;
                });
        }


    }

    function obtenerInfoSuper() {
        var payload = { ta206_iditemorigen: _ta206_iditemorigen }
        IB.DAL.post(null, "ObtenerDatosSUPER", payload, null, view.pintaInfoCRM);
    }

    function obtenerInfoCRM() {
        _ta206_iditemorigen
        var payload = { ta206_itemorigen: _ta206_itemorigen, ta206_iditemorigen: _ta206_iditemorigen }
        IB.DAL.post(IB.vars.strserver + "Capa_Presentacion/SIC/Services/CRM.asmx", "ObtenerDatosCRM", payload, null, view.pintaInfoCRM);
    }

    return {
        init: init
    }

})(SUPER.SIC.view, IB.DAL);