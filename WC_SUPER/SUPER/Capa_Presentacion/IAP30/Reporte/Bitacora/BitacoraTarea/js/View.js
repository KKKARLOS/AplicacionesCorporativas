var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.BitacoraT = SUPER.IAP30.BitacoraT || {}

SUPER.IAP30.BitacoraT.View = (function (e) {
    var dom = {
        btnRegresar: $('#btnSalir'),
        btnDelAsunto: $('#eliminarAsunto'),
        btnDelAccion: $('#eliminarAccion'),
        icoNuevoAsunto: $('#nuevoAsunto'),
        icoNuevaAccion: $('#nuevaAccion'),
        btnGoAsunto: $('#goAsunto'),
        btnGoAccion: $('#goAccion'),
        cboTipoAsunto: $('#cboTipoAsunto'),
        cboEstado: $('#cboEstado'),
        idProyecto: $('#idProyecto'),
        desProyecto: $('#desProyecto'),
        idPT: $('#idPT'),
        desProyectoT: $('#desProyectoT'),
        idTarea: $('#idTarea'),
        desTarea: $('#tareaDes'),
        fase: $('#fase'),
        actividad: $('#actividad'),
        bodyAsunto: $('#bodyAsuntos'),
        bodyAccion: $('#bodyAcciones'),
        tablaAsunto: $("#tablaAsunto"),
        tablaAccion: $("#tablaAccion"),
        colTipoAsunto: 1,
        colEstado: 6
    };
    var selectores = {
        //sel_inputs: "input",
        sel_TipoAsunto: "#cboTipoAsunto option:selected",
        sel_Estado: "#cboEstado option:selected",
        filasAsunto: "#tablaAsunto tr",
        filaAsuntoSel: "#tablaAsunto tr.activa",
        filasAccion: "#tablaAccion tr",
        filaAccionSel: "#tablaAccion tr.activa",
        container: ".container",
        btnExcelAsunto: ".btnExcelAsunto",
        btnExcelAccion: ".btnExcelAccion",
        btnModif: ".btnModif"
        //,sel_IdAsunto: "td:eq(7)"
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }
    //para elementos que no existen de inicio
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {}

    var crearAsuntos = function (dataSource) {
        var columnas = [
                { "data": "desAsunto" },
                { "data": "desTipo" },
                { "data": "severidad" },
                { "data": "prioridad" },
                {
                    "data": "fLimite",
                    "type": "date ",
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                {
                    "data": "fNotificacion",
                    "type": "date ",
                    //"render": function (value) { if (value === null) return ""; return moment(value).format('DD/MM/YYYY'); }
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data === null)
                                return "";
                            else {
                                if (data != "0001-01-01T00:00:00")
                                    return moment(data).format('DD/MM/YYYY');
                                else
                                    return "";
                            }
                        }
                    }
                },
                { "data": "estado" }
        ];


        dom.tblAsunto = $("#tablaAsunto").DataTable({
            "columns": columnas,
            data: dataSource,
            scrollY: "15vh",
            scrollCollapse: true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            "order": [[5, "asc"]],
            createdRow: function (row, data, dataIndex) {
                $(row).attr('id', data.idAsunto);
                $(row).attr('idR', data.idUserResponsable);
                if (IB.vars.idAsunto != "" && IB.vars.idAsunto != undefined) {
                    if (data.idAsunto == IB.vars.idAsunto)
                        marcarLinea($(row));
                }
            },
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExcelAsunto',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'EXCEL'
                }
            ]
        });
        dom.tblAsunto.on('search.dt', function () {
            $(selectores.filaAsuntoSel).removeClass('activa');
            if (dom.tblAccion)
                dom.tblAccion.clear().draw();
        });
    }
    var crearAcciones = function (dataSource) {
        if (!dom.tblAccion)
            crearDtAcciones(dataSource);
        else
            dom.tblAccion.clear().rows.add(dataSource).draw();
    }
    var crearDtAcciones = function (dataSource) {
        var columnDefs = [
            {
                "targets": [2],
                "className": "numericCol"
            }
        ];
        var columnas = [
                { "data": "T601_desaccion" },
                {
                    "data": "T601_flimite",
                    "type": "date ",
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                { "data": "T601_avance" },
                {
                    "data": "T601_ffin",
                    "type": "date ",
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                }
        ];


        dom.tblAccion = $("#tablaAccion").DataTable({
            destroy: true,
            "columns": columnas,
            "columnDefs": columnDefs,
            data: dataSource,
            scrollY: "11vh",
            scrollCollapse: true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            createdRow: function (row, data, dataIndex) {
                $(row).attr('id', data.T601_idaccion)
                if (IB.vars.idAccion != "" && IB.vars.idAccion != undefined) {
                    if (data.T601_idaccion == IB.vars.idAccion)
                        marcarLinea($(row));
                }
            },
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExcelAccion',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'EXCEL'
                }
            ]
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

    function rellenarComboTipoAsunto(data) {
        IB.procesando.ocultar();
        dom.cboTipoAsunto.empty();
        //Añado una fila vacía
        dom.cboTipoAsunto.append($('<option></option>').val(-1).html('TODOS'));
        $.each(data, function (index, item) {
            dom.cboTipoAsunto.append($('<option></option>').val(item.t384_idtipo).html(item.t384_destipo));
        });
        dom.cboTipoAsunto.val(dom.cboTipoAsunto.find('option:first').val());
    }

    var visualizarContenedor = function () {

        $(selectores.container).css("visibility", "visible");

    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        rellenarComboTipoAsunto: rellenarComboTipoAsunto,
        cebrear: cebrear,
        marcarLinea: marcarLinea,
        crearAsuntos: crearAsuntos,
        crearAcciones: crearAcciones,
        visualizarContenedor: visualizarContenedor
    }

}());

