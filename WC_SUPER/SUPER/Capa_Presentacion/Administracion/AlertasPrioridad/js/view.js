var SUPER = SUPER || {};
SUPER.APP = SUPER.APP || {};
SUPER.APP.PrioAlertas = SUPER.APP.PrioAlertas || {}

SUPER.APP.PrioAlertas.View = (function (e) {
    var dom = {
        //btnDel: $('#eliminar'),
        btnAdd: $('#btnAdd'),
        btnMod: $('#btnMod'),
        bodyDatos: $('#bodyDatos'),
        tabla: $("#tabla"),
        btnAceptar: $('#btnAceptar'),//Botón de la modal de detalle
        modalDetalle: $("#modalDetalle"),
        cboAlert1: $("#cboAlert1"),
        cboAlert2: $("#cboAlert2"),
        cboAlertG: $("#cboAlertG"),
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

    var init = function () {

    }

    var crear = function (dataSource) {
        var columnas = [
                { "data": "denGrupo1" },
                { "data": "denAlert1" },
                { "data": "denAlert2" },
                { "data": "denAlertG" }
        ];


        dom.tbl = $("#tabla").DataTable({
            "columns": columnas,
            //"columnDefs": [{ width: "20%", targets: 0 }],
            destroy: true,
            data: dataSource,
            scrollY: "50vh",
            //scrollX: true,
            scrollCollapse: true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            "order": [[1, "asc"], [2, "asc"]],
            createdRow: function (row, data, dataIndex) {
                $(row).attr('id', data.t820_idalerta_1 + '-' + data.t820_idalerta_2);
                $(row).attr('a1', data.t820_idalerta_1);
                $(row).attr('a2', data.t820_idalerta_2);
                $(row).attr('ag', data.t820_idalerta_g);
                $(row).attr('g1', data.grupo1);
                $(row).attr('g2', data.grupo2);
                $(row).attr('gg', data.grupoG);
                //$(row).attr('title', data.t820_idalerta_1);
                //$('[data-toggle="tooltip"]', row).tooltip();
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

    var modalDetalle = (function () {

        function init(modo) {
            if (modo == "A") { //Alta
                dom.modalDetalle.find(".modal-title").html("Alta de prioridad");
                dom.cboAlert1.prop("disabled", false);
                dom.cboAlert2.prop("disabled", false);
            }
            else { //EDición
                dom.modalDetalle.find(".modal-title").html("Edición de prioridad");
                dom.cboAlert1.prop("disabled", true);
                dom.cboAlert2.prop("disabled", true);
            }
        }
        function mostrar() {
            dom.modalDetalle.modal("show");
        }
        function ocultar() {
            dom.modalDetalle.modal("hide");
        }
        function getValues() {

            //return {
            //    //denominacion: dom.txtDen.val(),
            //    t820_idalerta_1: dom.cboAlert1.find("option:selected").val(),
            //    t820_idalerta_2: dom.cboAlert2.find("option:selected").val(),
            //    t820_idalerta_g: dom.cboAlertG.find("option:selected").val(),
            //    denAlert1: dom.cboAlert1.find("option:selected").text(),
            //    denAlert2: dom.cboAlert2.find("option:selected").text(),
            //    denAlertG: dom.cboAlertG.find("option:selected").text(),
            //    grupo1: 0,
            //    grupo2: 0,
            //    grupoG: 0,
            //}
            oPrioridad = new Object();
            //propiedades de campos.
            oPrioridad.t820_idalerta_1 = dom.cboAlert1.find("option:selected").val();
            oPrioridad.t820_idalerta_2 = dom.cboAlert2.find("option:selected").val();
            oPrioridad.t820_idalerta_g = dom.cboAlertG.find("option:selected").val();
            return oPrioridad;

        }
        //function setValues(o) {
        //    //dom.txtDen.val(o.t840_descripcion);
        //    dom.cboAlert1.val(o.t820_tipo);
        //}
        //function requiredValidation(oAmbito) {

        //    var valid = true;
        //    if (typeof oAmbito == "undefined" || oAmbito == null) oAmbito = dom.modalDetalle;

        //    oAmbito.find(":required").each(function () {

        //        if (($(this).val() == null || $(this).val().length == 0) && !$(this).hasClass("hide")) {
        //            $(this).addClass("requerido");
        //            valid = false;
        //        }
        //    });

        //    if (!valid)
        //        IB.bsalert.toastdanger("Debes cumplimentar los campos obligatorios.");

        //    return valid;
        //}

        return {
            init: init,
            mostrar: mostrar,
            ocultar: ocultar,
            getValues: getValues
            //setValues: setValues,
            //requiredValidation: requiredValidation
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

