$(document).ready(function () { });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app = (function (view, view_ModalArea, view_ModalSubArea, dal) {

    var _ta200_idareapreventa = null;
    var _ta201_idsubareapreventa = null;
    var bcambios = false;

    if (IB.vars.error) {
        IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
        return;
    }
    
    window.onbeforeunload = function () {
        if (bcambios) return 'Hay cambios sin guardar. Si continúas perderás dichos cambios.';
    };


    view.init();
    view_ModalArea.init();
    view_ModalSubArea.init();

    //Eventos
    view.attachLiveEvents("click", view.selectores.filaArea, seleccionarFilaArea);
    view.attachLiveEvents("click", view.selectores.fk_ta200_idareapreventa, fk_ta200_idareapreventa_onClick);
    view.attachLiveEvents("click", view.selectores.fk_ta201_idsubareapreventa, fk_ta201_idsubareapreventa_onClick);    
    view.attachLiveEvents("click", ".fk_grabar", btnGrabar_onClick);
    view.attachLiveEvents("click", ".fk_grabarSubArea", btnGrabarSubArea_onClick);
    

    //Detalle área preventa
    function fk_ta200_idareapreventa_onClick(e) {                
        _ta200_idareapreventa = view.getIdArea($(this));
        obtenerDetalleArea(_ta200_idareapreventa);
        e.stopPropagation();        
    }

    //Detalla subárea preventa
    function fk_ta201_idsubareapreventa_onClick(e) {
        _ta201_idsubareapreventa = view.getIdSubArea($(this));        
        obtenerDetalleSubArea(_ta201_idsubareapreventa);
        e.stopPropagation();
    }

    function btnGrabar_onClick() {

        if (!view_ModalArea.requiredValidation()) return;

        var oArea = view_ModalArea.getViewValues();
        var lstFigurasArea = view_ModalArea.obtenerTablaFiguras();

        var payload = { oArea: oArea, lstFigurasArea: lstFigurasArea }

        IB.DAL.post(null, "grabarArea", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    bcambios = false;
                    
                    seleccionarFila = view.estaFilaSeleccionada(_ta200_idareapreventa);

                    view_ModalArea.ocultarModal();
                    view.refreshDatatableArea(_ta200_idareapreventa, seleccionarFila, _ta200_idareapreventa);
                    
                })
            }

        );        
    }

    function btnGrabarSubArea_onClick() {

        if (!view_ModalSubArea.requiredValidation()) return;

        var oSubArea = view_ModalSubArea.getViewValues();
        var lstFigurasSubArea = view_ModalSubArea.obtenerTablaFiguras();

        var payload = { oSubArea: oSubArea, lstFigurasSubArea: lstFigurasSubArea }

        IB.DAL.post(null, "grabarSubArea", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    bcambios = false;
                    view_ModalSubArea.ocultarModal();                    
                    view.refreshDatatable(_ta200_idareapreventa);

                })
            }

        );
    }

    function seleccionarFilaArea() {

        //Refrescamos el datatable de subareas y marcamos la fila como seleccionada
        _ta200_idareapreventa = view.addSelected($(this));
        view.refreshDatatable(_ta200_idareapreventa);
    }

    function obtenerDetalleArea(ta200_idareapreventa) {
        
        try {
            var payload = { ta200_idareapreventa: ta200_idareapreventa }

            dal.post(null, "getAreaSel", payload, null,
                function (data) {
                    if (data.length > 0) {
                        view_ModalArea.mostrarModal(); //Inicializar la modal
                        //pintar campos en modal
                        view_ModalArea.renderModal(data);
                        obtenerFigurasArea(ta200_idareapreventa);
                    }
                }
            );

        } catch (e) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
        }
    }

    function obtenerDetalleSubArea(ta201_idsubareapreventa) {

        try {
            var payload = { ta201_idsubareapreventa: ta201_idsubareapreventa }

            dal.post(null, "getSubAreaSel", payload, null,
                function (data) {
                    if (data.length > 0) {
                        view_ModalSubArea.mostrarModal(); //Inicializar la modal
                        //pintar campos en modal
                        view_ModalSubArea.renderModal(data);
                        obtenerFigurasSubArea(ta201_idsubareapreventa);
                    }
                }
            );

        } catch (e) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
        }
    }

    function obtenerFigurasArea(ta200_idareapreventa) {
        var payload = { ta200_idareapreventa: ta200_idareapreventa }
        dal.post(null, "getFiguras_Area", payload, null,
               function (data) {
                   if (data.length > 0) {
                       //render figuras
                       view_ModalArea.renderListaFiguras(data);                       
                   }
               }
           );
    }

    function obtenerFigurasSubArea(ta201_idsubareapreventa) {
        var payload = { ta201_idsubareapreventa: ta201_idsubareapreventa }
        dal.post(null, "getFiguras_SubArea", payload, null,
               function (data) {
                   if (data.length > 0) {
                       //render figuras
                       view_ModalSubArea.renderListaFigurasSubArea(data);
                   }
               }
           );
    }

    view.attachLiveEvents("click", "#btnCerrar", function () {        
        if (bcambios) {
            $.when(IB.bsconfirm.confirmCambios())
                .then(function () {
                    bcambios = false;
                    view_ModalArea.ocultarModal();
                })
        }
        else view_ModalArea.ocultarModal();        
    });

    view.attachLiveEvents("click", "#btnCerrarSubArea", function () {
        if (bcambios) {
            $.when(IB.bsconfirm.confirmCambios())
                .then(function () {
                    bcambios = false;
                    view_ModalSubArea.ocultarModal();
                })
        }
        else view_ModalSubArea.ocultarModal();
    });

    var camposEditables = $("#modal-area input[type='text'], #modal-subarea input[type='text']");
    view.attachLiveEvents("change", camposEditables, function () {
        bcambios = true;
    });

})(SUPER.SIC.view, SUPER.SIC.view_ModalArea, SUPER.SIC.view_ModalSubArea, IB.DAL);