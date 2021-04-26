$(document).ready(function () {
    SUPER.APP.MotivoOCFA.app.init();
})

var SUPER = SUPER || {};
SUPER.APP = SUPER.APP || {};
SUPER.APP.MotivoOCFA = SUPER.APP.MotivoOCFA || {}

SUPER.APP.MotivoOCFA.app = (function (view) {

    var init = function () {
        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }
        view.init();

        //SELECCIONAR FILA DEL CATALOGO
        view.attachLiveEvents("click", view.selectores.filas, marcarFila);
        //Añadir al catálogo 
        //view.attachEvents("click", view.dom.icoNuevo, addElemento);
        view.attachEvents("click", view.dom.btnAdd, addElemento);
        //Modificar fila seleccionada del catálogo
        view.attachEvents("click", view.dom.btnMod, goDetalle);
        //Eliminar fila seleccionada del catálogo
        view.attachEvents("click", view.dom.btnDel, eliminar);
        //Acceso a detalle 
        //view.attachEvents("click", view.dom.btnGoDetalle, goDetalle);
        //editar al detalle 
        view.attachLiveEvents("dblclick", view.selectores.filaSel, goDetalle);
        //Botones exportación a excel
        view.attachLiveEvents("click", view.selectores.btnExcel, excelCatalogo);

        //Aceptar del modal de detalle
        view.attachEvents("keypress click", view.dom.btnAceptar, aceptarDetalle);

        consultar();

    }

    var modo_modal;
    var idElemento_editing;

    var marcarFila = function (event) {
        view.marcarLinea(this);
    }

    var consultar = function () {
        var defer = $.Deferred();

        //var payload = { nTarea: nTarea, tipoAsunto: tipoAsunto, estado: estado };
        var payload = { };
        IB.DAL.post(null, "getCatalogo", payload, null,
            function (data) {
                //if (data.length != 0) {
                view.crear(data);
                //}
                $.when(IB.procesando.ocultar()).then(function () {
                    view.visualizarContenedor();
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }


    var eliminar = function (event) {
        IB.procesando.ocultar();
        $.when(IB.bsconfirm.confirm("warning", "Borrado de motivo", "¿Deseas borrar el motivo?.</br></br>")).then(function () {
            var aLineas = [];
            var sIdElem = $(view.selectores.filaSel).attr("id");
            var linea = { t840_idmotivo: sIdElem };
            aLineas.push(linea);
            borrar(aLineas);
        });
    }
    var borrar = function (lineas) {
        IB.procesando.mostrar();
        var payload = { lineas: lineas };
        IB.DAL.post(null, "borrar", payload, null,
            function (data) {
                view.dom.tbl.row('.activa').remove().draw(false);
                IB.procesando.ocultar();
                grabacionCorrecta();
            }
        );
    }
    var grabacionCorrecta = function () {
        IB.bsalert.toast("Grabación correcta.", "info");
        //desactivarGrabar();
    }

    //Acceso a otras pantallas
    var addElemento = function (e) {
        modo_modal = "A";
        view.modalDetalle.init("A");
        //$.when(loadUnidades()).then(view.modalDetalle.mostrar);
        view.modalDetalle.mostrar();
    }
    var goDetalle = function () {
        IB.procesando.mostrar();
        idElemento_editing = $(view.selectores.filaSel).attr("id");
        //var el = event.target ? event.target : event.srcElement;
        //idElemento_editing = $(el).attr("id");
        if (!idElemento_editing) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar una fila");
            return;
        }

        modo_modal = "E"
        view.modalDetalle.init("E");

        //Obtener la denominación de la fila del datatable y pintarla en la modal
        var table = $('#tabla').DataTable();
        var row = table.row('#' + idElemento_editing);
        var o = { t840_descripcion: row.data().t840_descripcion, t820_tipo: row.data().t820_tipo }
        view.modalDetalle.setValues(o)
        view.modalDetalle.mostrar();
        IB.procesando.ocultar();
    }
    //Pulsado del botón de aceptar del modal de detalle
    var aceptarDetalle = function (event) {
        //Hay que grabar y a la vuelta refrescar el catalogo
        //validar obligatorios
        if (!view.modalDetalle.requiredValidation()) return;

        IB.procesando.mostrar();
        if (modo_modal == "A") {//Alta
            //grabar y en el callback refrescar datatable y seleccionar fila.
            var payload = view.modalDetalle.getValues()
            IB.DAL.post(null, "Alta", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        view.modalDetalle.ocultar();
                        IB.bsalert.toast("Grabación correcta.");
                        //var filter = view.getFilterValues();
                        //view.refreshDatatable(filter, data);
                        consultar();
                    });
                }
            );
        }
        else { //Edición
            //grabar y en el callback refrescar datatable y seleccionar fila.
            var payload = view.modalDetalle.getValues()
            $.extend(payload, { t840_idmotivo: idElemento_editing })
            IB.DAL.post(null, "Edicion", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        view.modalDetalle.ocultar();
                        IB.bsalert.toast("Grabación correcta.");

                        //actualizar celda en el datatable
                        var table = $('#tabla').DataTable();
                        var row = table.row('#' + idElemento_editing);
                        var oData = row.data();
                        oData.t840_descripcion = payload.denominacion;
                        //falta asignar el valor del combo
                        row.data(oData).draw();
                    });
                }
            );
        }
    }

    function excelCatalogo() {
        if (!view.dom.tbl.rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }
        IB.Exportaciones.exportarDataTableExcel(view.dom.tabla, "Motivos", "Motivos de OC y FA", false);
    }


    return {
        init: init
    }

})(SUPER.APP.MotivoOCFA.View);
