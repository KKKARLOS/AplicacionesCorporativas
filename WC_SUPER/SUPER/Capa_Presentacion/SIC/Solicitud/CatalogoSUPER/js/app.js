/// <reference path="../../../../../scripts/IB.js" />

$(document).ready(function () { SUPER.SIC.appCatAccion.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appCatAccion = (function (view) {
    var _estadoSolicitud;
    
    function init() {

        console.log("appCatAccion.init");
        console.log("origenMenu=" + IB.vars.origenMenu);

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        view.init();

        //editar solicitud
        view.attachLiveEvents("click", view.selector.fk_edicion_solicitud, editarSolicitud)
        view.attachLiveEvents("click", view.selector.fk_lnkEstadoSolicitud, fk_lnkEstadoSolicitud_onClick)
        

        //agregar nueva acción
        view.attachEvents("click", view.selector.btnAddAccion, agregarAccion);
        view.attachEvents("click", view.selector.btnAddSolicitud, agregarSolicitud);

        //Modal agregar solicitud 
        view.attachEvents("change", view.dom.cmbUnidad_AS, loadAreas);
        view.attachEvents("click", view.dom.btnGrabar_AS, function(){
            _estadoSolicitud = "A";
            btnGrabar_AS_onClick();
        })

        //Modal finalizar solicitud
        view.attachLiveEvents("click", view.selector.btnFinalizar, function () {            
            btnFinalizarAnular("F")
        });
        
        //Modal anular solicitud
        view.attachLiveEvents("click", view.selector.btnAnular, function () {            
            btnFinalizarAnular("X")
        });

        //Botón aceptar Finalización/Anulación Solicitud
        view.attachLiveEvents("click", view.selector.btnFinAnul, function () {
            //btnGrabar_AS_onClick();
            finAnul(_estadoSolicitud);
        })

        view.attachLiveEvents("click", view.selector.btnCancelFinAnul, function () {
            limpiarAnulacion();
        })

        view.attachEvents("click", view.dom.btnDelSolicitud, function () {
            view.showModalEliminarSolicitud();            
        })

        view.attachEvents("keyup", view.dom.txtMotivo, contabilizarCaracteresMotivoAnulacion_onKeyUp);

        view.attachEvents("click", view.dom.btnEliminarSolicitud, function () {                        
            IB.procesando.mostrar();
           
            IB.DAL.post(null, "EliminarSolicitud", { ta206_idsolicitudpreventa: view.getidSelectedRow() }, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        view.hideModalEliminarSolicitud();                        
                        var filter = view.getFilterValues();
                        view.refreshDatatable(filter, data);
                    });
                }
            );            
        })

        view.attachLiveEvents("click", view.selector.edicion_accion, function () {
            
            var el = this;

            //Guardar filtros en el historial y navegar al detalle
            var params = "filters=";
            var filter = view.getFilterValuesWithDesc();

            //Guardar filtros en el historial y navegar al detalle
            var params = "filters=ejecutar:true";

            if (filter.estado != null) params += "|estado:" + filter.estado;
            if (filter.estadoSolicitud != null) params += "|estadoSolicitud:" + filter.estadoSolicitud;
            if (filter.itemorigen != null) params += "|itemorigen:" + filter.itemorigen;
            if (filter.iditemorigen != null) params += "|iditemorigen:" + filter.iditemorigen;
            if (filter.importeDesde != null) params += "|importeDesde:" + filter.importeDesde;
            if (filter.importeHasta != null) params += "|importeHasta:" + filter.importeHasta;
            if (filter.ffinDesde != null) params += "|ffinDesde:" + filter.ffinDesde;
            if (filter.ffinHasta != null) params += "|ffinHasta:" + filter.ffinHasta;
            if (filter.promotor != null) params += "|promotor:" + filter.promotor;
            if (filter.comercial != null) params += "|comercial:" + filter.comercial;
            if (filter.lideres != null) params += "|lideres:" + filter.lideres.join(";");
            if (filter.clientes != null) params += "|clientes:" + filter.clientes.join(";");
            if (filter.acciones != null) params += "|acciones:" + filter.acciones.join(";");
            if (filter.unidades != null) params += "|unidades:" + filter.unidades.join(";");
            if (filter.areas != null) params += "|areas:" + filter.areas.join(";");
            if (filter.subareas != null) params += "|subareas:" + filter.subareas.join(";");

            params += "|idaccion:" + $(this).attr("data-idaccion");
            params += "|idsolicitud:" + view.getidSelectedRow();
            
            params += "&origenmenu=" + IB.vars.origenMenu;

            params = IB.uri.encode(params);
            var newurl = location.href.substr(0, location.href.indexOf("?") != -1 ? location.href.indexOf("?") : location.href.length) + "?" + params;

            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
                function () {
                    var qs = IB.uri.encode("modo=E&id=" + $(el).attr("data-idaccion") + "&origenpantalla=SUPER");
                    window.location.href = "../../Accion/Detalle/Default.aspx?" + qs;
                });

        });

    }

    var modo_modalSolicitud;
    var idSolicitud_editing;

    //Añadir nueva solicitud (mostrar modal)
    function agregarSolicitud() {
        modo_modalSolicitud = "A";
        view.modalSolicitud.init("A");
        view.ocultarBotonesAccionSolicitud();
        $.when(loadUnidades()).then(view.modalSolicitud.mostrar);
    }

    //Editar solicitud (Sólo las de tipoorigen="S", se permite editar la denominación)
    function editarSolicitud() {

        view.mostrarBotonesAccionSolicitud();
        var el = event.target ? event.target : event.srcElement;

        idSolicitud_editing = $(el).attr("data-idsolicitud");
        modo_modalSolicitud = "E"
        view.modalSolicitud.init("E");

        //Obtener la denominación de la fila del datatable y pintarla en la modal
        var table = $('#tblSolicitudes').DataTable();
        var row = table.row('#ta206_id_' + idSolicitud_editing);
        var o = { ta206_denominacion: row.data().ta206_denominacion }
        view.modalSolicitud.setValues(o)
        view.modalSolicitud.mostrar();
    }

    function finAnul(accion) {
        switch (accion) {
            case "F":
                btnGrabar_AS_onClick();
                break;

            case "X":
                if ($("#txtMotivo").val().trim().length === 0)
                {
                    IB.bsalert.toastwarning("Debes introducir un motivo de anulación.");
                    return;
                }
                btnGrabar_AS_onClick();
                break;
           
                limpiarAnulacion();
        }
    }

    function limpiarAnulacion() {
        view.dom.txtMotivo.val("");
        view.dom.numCaracteres.text('250 caracteres disponibles');
    }

    //Grabar solicitud
    function btnGrabar_AS_onClick() {

        //validar obligatorios
        if (!view.modalSolicitud.requiredValidation()) return;

        if (modo_modalSolicitud == "A") {

            IB.procesando.mostrar();

            //grabar y en el callback refrescar datatable y seleccionar fila.
            var payload = view.modalSolicitud.getValues()
            IB.DAL.post(null, "AltaSolicitud", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        view.modalSolicitud.ocultar();
                        IB.bsalert.toast("Grabación correcta.");
                        var filter = view.getFilterValues();
                        view.refreshDatatable(filter, data);
                    });
                }
            );
        }
        else { //Edición

            IB.procesando.mostrar();
            
            //Grabar y en el callback refrescar datatable y seleccionar fila.
            var payload = view.modalSolicitud.getValues();
            
            $.extend(payload, { ta206_idsolicitudpreventa: idSolicitud_editing, ta206_estado: _estadoSolicitud, motivoAnulacion: view.dom.txtMotivo.val() })
            IB.DAL.post(null, "EdicionSolicitud", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        view.modalSolicitud.ocultar();
                        view.Ocultar_modalfinAnul();
                        limpiarAnulacion();
                        IB.bsalert.toast("Grabación correcta.");

                        //actualizar celda en el datatable
                        var table = $('#tblSolicitudes').DataTable();
                        var row = table.row('#ta206_id_' + idSolicitud_editing);
                        var oData = row.data();
                        oData.ta206_denominacion = payload.denominacion;
                        oData.ta206_estado = payload.ta206_estado;
                        row.data(oData).draw();
                    });
                }
            );
        }


    }

    //Finalizar y Anular solicitud
    function btnFinalizarAnular(accion) {
        
        switch (accion) {
            case "F":
                _estadoSolicitud = view.openFinalizarAnular(accion);
                break;

            case "X":
                _estadoSolicitud = view.openFinalizarAnular(accion);
                break;        
        }                
    }

    //* COMBOS *//
    var loadUnidades = function () {

        console.log("loadUnidades");

        view.clearComboOptions(view.dom.cmbArea_AS);

        var cmbEstructBloqueados = view.modalSolicitud.estadoBloqueoEstructura();

        var method = "ObtenerListaEstructura";
        if (cmbEstructBloqueados.unidad) method = "ObtenerLista";

        var payload = { tipo: "unidad_preventa", filtrarPor: null, origenMenu: IB.vars.origenMenu }

        return IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/SIC/Services/Listas.asmx", method,
                    payload, null,
                    function (data) { view.renderCombo(data, view.dom.cmbUnidad_AS) });
    };

    var loadAreas = function () {

        console.log("loadAreas");

        var cmbEstructBloqueados = view.modalSolicitud.estadoBloqueoEstructura();

        var filtrarPor = view.getComboSelectedOption(view.dom.cmbUnidad_AS);
        if (filtrarPor == null || filtrarPor == "") return;

        var method = "ObtenerListaEstructura";
        if (cmbEstructBloqueados.area) method = "ObtenerLista";

        var payload = { tipo: "area_preventa", filtrarPor: filtrarPor, origenMenu: IB.vars.origenMenu }
        return IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/SIC/Services/Listas.asmx", method,
                    payload, null,
                    function (data) {
                        view.renderCombo(data, view.dom.cmbArea_AS)
                    });
    };
    // * COMBOS * //

    function agregarAccion() {

        var o = view.getSolicitudSelected();

        if (o == null) {
            IB.bsalert.toastwarning("Para agregar una nueva acción primero debes seleccionar una solicitud en el catálogo");
            return;
        }

        if (o.ta206_estado != "A") {
            IB.bsalert.toastwarning("Sólo se permite agregar nuevas acciones a solicitudes abiertas.");
            return;
        }


        var el = this;
        var filter = view.getFilterValuesWithDesc();

        //Guardar filtros en el historial y navegar al detalle
        var params = "filters=ejecutar:true";
        params += "|idsolicitud:" + o.ta206_idsolicitudpreventa

        if (filter.estado != null) params += "|estado:" + filter.estado;
        if (filter.estadoSolicitud != null) params += "|estadoSolicitud:" + filter.estadoSolicitud;
        if (filter.itemorigen != null) params += "|itemorigen:" + filter.itemorigen;
        if (filter.iditemorigen != null) params += "|iditemorigen:" + filter.iditemorigen;
        if (filter.importeDesde != null) params += "|importeDesde:" + filter.importeDesde;
        if (filter.importeHasta != null) params += "|importeHasta:" + filter.importeHasta;
        if (filter.ffinDesde != null) params += "|ffinDesde:" + filter.ffinDesde;
        if (filter.ffinHasta != null) params += "|ffinHasta:" + filter.ffinHasta;
        if (filter.solicitud != null) params += "|solicitud:" + filter.solicitud;
        if (filter.promotor != null) params += "|promotor:" + filter.promotor;
        if (filter.comercial != null) params += "|comercial:" + filter.comercial;
        if (filter.lideres != null) params += "|lideres:" + filter.lideres.join(";");
        if (filter.clientes != null) params += "|clientes:" + filter.clientes.join(";");
        if (filter.acciones != null) params += "|acciones:" + filter.acciones.join(";");
        if (filter.unidades != null) params += "|unidades:" + filter.unidades.join(";");
        if (filter.areas != null) params += "|areas:" + filter.areas.join(";");
        if (filter.subareas != null) params += "|subareas:" + filter.subareas.join(";");

        params += "&origenmenu=" + IB.vars.origenMenu;
        params = IB.uri.encode(params);

        var newurl = location.href.substr(0, location.href.indexOf("#") != -1 ? location.href.indexOf("#") : location.href.length);
        newurl = newurl.substr(0, newurl.indexOf("?") != -1 ? newurl.indexOf("?") : newurl.length) + "?" + params;

        IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
            function () {
                var qs = IB.uri.encode("modo=A&itemorigen=" + o.ta206_itemorigen + "&iditemorigen=" + o.ta206_iditemorigen + "&origenpantalla=SUPER&caller=Catalogosuper");
                window.location.href = "../../Accion/Detalle/Default.aspx?" + qs;
            });
    }

    //Contabiliza el número de caracteres escritos en el textarea motivo de anulación
    function contabilizarCaracteresMotivoAnulacion_onKeyUp() {
        var _max = 250;
        view.dom.numCaracteres.text(_max + ' caracteres disponibles');

        var len = $(this).val().length;
        if (len >= _max) {
            view.dom.numCaracteres.text('Límite alcanzado');
            view.dom.numCaracteres.addClass('text-danger');

            /*Una vez alcanzado el límite, sólo permitimos el backspace*/
            if (e.keyCode !== 8) {
                e.preventDefault();
            }

        }
        else {
            var ch = _max - len;
            view.dom.numCaracteres.text(ch + ' caracteres disponibles');
            view.dom.numCaracteres.removeClass('disabled');
            view.dom.numCaracteres.removeClass('text-danger');
        }
    }

    function fk_lnkEstadoSolicitud_onClick() {
        view.abrirModalEstadoSolicitud($(this).parent().parent());
    }

    return {
        init: init
    }

})(SUPER.SIC.viewCatAccion);