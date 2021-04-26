var SUPER = SUPER || {};
SUPER.APP = SUPER.APP || {};
SUPER.APP.MotivoOCFA = SUPER.APP.MotivoOCFA || {}

SUPER.APP.MotivoOCFA.View = (function (e) {
    var dom = {
        btnDel: $('#eliminar'),
        //icoNuevo: $('#nuevo'),
        btnAdd: $('#btnAdd'),
        btnMod: $('#btnMod'),
        btnDel: $('#btnDel'),
        bodyDatos: $('#bodyDatos'),
        tabla: $("#tabla"),
        btnAceptar: $('#btnAceptar'),//Botón de la modal de detalle
        modalDetalle: $("#modalDetalle"),
        txtDen: $("#txtDen"),
        cboTipo: $("#cboTipo"),
        colTipo: 0
    };
    var selectores = {
        filas: "#tabla tr",
        filaSel: "#tabla tr.activa",
        container: ".container",
        btnExcel: ".btnExcel",
        btnModif: ".btnModif"
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }
    //para elementos que no existen de inicio
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () { }

    var crear = function (dataSource) {
        var columnas = [
                { "data": "desTipo" },
                { "data": "t840_descripcion" },
        ];


        dom.tbl = $("#tabla").DataTable({
            "columns": columnas,
            "columnDefs": [{ width: "20%", targets: 0 }],
            destroy: true,
            data: dataSource,
            scrollY: "50vh",
            //scrollX: true,
            scrollCollapse: true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            //"order": [[1, "asc"], [2, "asc"]],
            createdRow: function (row, data, dataIndex) {
                $(row).attr('id', data.t840_idmotivo);
                $(row).attr('tipo', data.t820_tipo);
                //if (IB.vars.idElemento != "" && IB.vars.idElemento != undefined) {
                //    if (data.idElemento == IB.vars.idElemento)
                //        marcarLinea($(row));
                //}
            },
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExcel',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'EXCEL'
                }
            ]
        });
        dom.tbl.on('search.dt', function () {
            $(selectores.filaSel).removeClass('activa');
        });
    }

    function cebrear() {
        $("tr:visible:not(.bg-info)").removeClass("cebreada");
        $('tr:visible:not(.bg-info):even').addClass('cebreada');
    }

    var marcarLinea = function (thisObj) {
        desmarcarLinea($(thisObj).parent());
        $(thisObj).addClass('activa');
    }
    var desmarcarLinea = function (tabla) {
        $(tabla).find('tr.activa').removeClass('activa');
    }

    var visualizarContenedor = function () {

        $(selectores.container).css("visibility", "visible");
    }

    //function clearDatatable() {

    //    var oDataTable = $('#tabla').DataTable();
    //    oDataTable.clear().draw();
    //}

    var modalDetalle = (function () {

        function init(modo) {
            if (modo == "A") { //Alta
                dom.modalDetalle.find(".modal-title").html("Alta de motivo");
                //dom.row_estructura_AS.removeClass("hide").addClass("show");
                //desbloquearEstructura()
            }
            else { //EDición
                dom.modalDetalle.find(".modal-title").html("Edición de motivo");
                //dom.row_estructura_AS.removeClass("show").addClass("hide");
                //bloquearEstructura();
            }
            dom.txtDen.val("");
            dom.txtDen.removeClass("requerido");
            //dom.cboTipo.removeClass("requerido");
            //clearComboOptions(dom.cboTipo);
        }
        function mostrar() {
            dom.modalDetalle.modal("show");
        }
        function ocultar() {
            dom.modalDetalle.modal("hide");
        }
        function getValues() {

            return {
                denominacion: dom.txtDen.val(),
                tipo: dom.cboTipo.find("option:selected").val(),
            }
        }
        function setValues(o) {
            dom.txtDen.val(o.t840_descripcion);
            dom.cboTipo.val(o.t820_tipo);
        }
        function requiredValidation(oAmbito) {

            var valid = true;
            if (typeof oAmbito == "undefined" || oAmbito == null) oAmbito = dom.modalDetalle;

            oAmbito.find(":required").each(function () {

                if (($(this).val() == null || $(this).val().length == 0) && !$(this).hasClass("hide")) {
                    $(this).addClass("requerido");
                    valid = false;
                }
            });

            if (!valid)
                IB.bsalert.toastdanger("Debes cumplimentar los campos obligatorios.");

            return valid;
        }

        return {
            init: init,
            mostrar: mostrar,
            ocultar: ocultar,
            getValues: getValues,
            setValues: setValues,
            requiredValidation: requiredValidation
        }
    })();


    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        cebrear: cebrear,
        marcarLinea: marcarLinea,
        crear: crear,
        visualizarContenedor: visualizarContenedor,
        modalDetalle: modalDetalle
    }

}());

