var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.BitacoraPT = SUPER.IAP30.BitacoraPT || {}

SUPER.IAP30.BitacoraPT.View = (function (e) {
    var dom = {
        btnRegresar: $('#btnSalir'),
        btnDelAsunto: $('#eliminarAsunto'),
        btnDelAccion: $('#eliminarAccion'),
        icoNuevoAsunto: $('#nuevoAsunto'),
        icoNuevaAccion: $('#nuevaAccion'),
        btnGoAsunto: $('#goAsunto'),
        btnGoAccion: $('#goAccion'),
        btnAccesoT: $('#accesoT'),
        cboTipoAsunto: $('#cboTipoAsunto'),
        cboEstado: $('#cboEstado'),
        idProyecto: $('#idProyecto'),
        desProyecto: $('#desProyecto'),
        idPT: $('#idPT'),
        desProyectoT: $('#desProyectoT'),
        bodyAsunto: $('#bodyAsuntos'),
        bodyAccion: $('#bodyAcciones'),
        bodyPT: $('#bodyT'),
        tablaAsunto: $("#tablaAsunto"),
        tablaAccion: $("#tablaAccion"),
        tablaT: $('#tablaT'),
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
        filasT: "#tablaT tr",
        filaTSel: "#tablaT tr.activa",
        container: ".container",
        btnExcelAsunto: ".btnExcelAsunto",
        btnExcelAccion: ".btnExcelAccion",
        btnExcelT: ".btnExcelT",
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

    var init = function () { }

    var crearAsuntos = function (dataSource) {
        //var columnDefs = [
        //    {
        //        "targets": [7],
        //        "visible": false,
        //        "searchable": false
        //    }
        //];
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
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                { "data": "estado" }
                //,{ "data": "idAsunto" }
        ];


        dom.tblAsunto = $("#tablaAsunto").DataTable({
            "columns": columnas,
            //DT_RowId: 'idAsunto',
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
        //$('#cboTipoAsunto').change(function () {
        //    var sTipoAsunto = $("#cboTipoAsunto option:selected").text();
        //    tblAsunto.column(1).search(sTipoAsunto).draw();
        //    //$("#tablaAsunto").column(1).search(sTipoAsunto).draw();
        //})
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
                { "data": "T410_desaccion" },
                {
                    "data": "T410_flimite",
                    "type": "date ",
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                { "data": "T410_avance" },
                {
                    "data": "T410_ffin",
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
            //"bScrollCollapse": true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            createdRow: function (row, data, dataIndex) {
                $(row).attr('id', data.T410_idaccion)
                if (IB.vars.idAccion != "" && IB.vars.idAccion != undefined) {
                    if (data.T410_idaccion == IB.vars.idAccion)
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

    var crearBitacoraT = function (dataSource) {
        var columnas = [{ "data": "nom_tarea" }];
        dom.tblTarea = $("#tablaT").DataTable({
            "columns": columnas,
            data: dataSource,
            scrollY: "11vh",
            scrollCollapse: true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            createdRow: function (row, data, dataIndex) {
                $(row).attr('id', data.cod_tarea);
                $(row).attr('acceso', data.t332_acceso_iap);
            },
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExcelT',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'EXCEL'
                }
            ]
        });
    }
    function cebrear() {
        $("tr:visible:not(.bg-info)").removeClass("cebreada");
        $('tr:visible:not(.bg-info):even').addClass('cebreada');
        //controlarScroll();
    }
    //Marcado de línea activa
    var marcarLinea = function (thisObj) {
        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        desmarcarLinea($(thisObj).parent());
        $(thisObj).addClass('activa');
    }
    //Desmarcado de línea activa
    var desmarcarLinea = function (tabla) {
        //$("tr.activa").removeClass('activa');
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
        crearBitacoraT: crearBitacoraT,
        visualizarContenedor: visualizarContenedor
    }

}());

