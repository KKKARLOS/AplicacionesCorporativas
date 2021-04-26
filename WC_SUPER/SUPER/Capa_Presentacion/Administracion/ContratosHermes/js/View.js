var SUPER = SUPER || {};
SUPER.Administracion = SUPER.Administracion || {};
SUPER.Administracion.ContratosHermes = SUPER.Administracion.ContratosHermes || {}

SUPER.Administracion.ContratosHermes.View = (function (e) {

    var dom = {

        txtDesde: $('#txtDesde'),
        txtHasta: $('#txtHasta'),
        btnObtener: $('#btnObtener'),
        tabla: $('#tablaON'),
        txtRango: $('#txtRango'),
        lblCR: $('#lblCR'),
        txtCR: $('#txtCR'),
        btnEliminarCR: $('#btnEliminarCR'),
        btnRegresar: $('#btnSalir'),
        btnGrabar: $('#btnGrabar'),
        //Profesional
        icoProfesional: $('#divProf'),
        divBuscadorPersonas: $('.buscadorUsuario'),
        imagenProf: $('#imagenProf'),
        spProfesional: $('#spProfesional'),
        tablaProyectosModal: $('#tablaProyectos'),
        modalProyecto: $('#modal-Proyecto')
    }

    var selectores = {

        contenedor: ".container",
        sel_filtros: ".fk_filtro",
        sel_chk: ".fk_chk",
        ayudaCatalogoBasico: ".fk_ayudaCatalogoBasico",
        sel_cualif: ".fk_Cualif",
        filasSeleccionadas: "#bodyTabla .selected",
    }

    var indicadores = {
        i_dispositivoTactil: false
    }

    function deAttachEvents(selector) {
        $(selector).off();
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function deAttachLiveEvents(event, selector, callback) {
        $(document).off(event, selector, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {

        $(lblCR).text(IB.vars.denNodoCorta);
        $('#spProfesional').html(IB.vars.denResponsable);

        if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
            indicadores.i_dispositivoTactil = true;
        }

        inicializarTabla();
        inicializarDatatablesProyectos();
        inicializarDatepickers();

        //Vaciado de tabla cuando se produce un cambio en los filtros
        $(selectores.sel_filtros).on("change", vaciarTabla);

        //validación de campos numéricos
        $(document).on('keypress', 'input.txtNum', function (e) {
            return validarTeclaNumerica(e, false);
        });

        //Para que cuando se muestr la modal se ajusten las cabeceras a los tamaños de las columnas
        $(document).on('shown.bs.modal', '#modal-Proyecto', function () {
            dom.tablaProyectosModal.DataTable().columns.adjust();
        });

    }

    //Funcionalidad pantalla principal

    //var exportarPDF = function () {

    //    IB.procesando.mostrar()

    //    if (!dom.tabla.DataTable().rows().count()) {
    //        IB.procesando.ocultar();
    //        IB.bsalert.toastdanger("No hay datos a exportar");
    //        return false;
    //    }

    //    if (!dom.tabla.DataTable().rows('.selected').count()) {
    //        IB.procesando.ocultar();
    //        IB.bsalert.toastdanger("No hay filas seleccionadas");
    //        return false;
    //    }

    //    if (dom.tabla.DataTable().rows('.selected').count() > 8000) {
    //        IB.procesando.ocultar();
    //        IB.bsalert.toastdanger("El volumen de imputaciones a exportar excede el máximo permitido. \n\n Acote el periodo temporal de la consulta o reduzca el número de tareas marcadas para la exportación.");
    //        return false;
    //    }

    //    var sb = new StringBuilder();

    //    dom.tabla.DataTable().rows('.selected').every(function () {
    //        sb.append(this.data().t314_idusuario + '/' + this.data().t332_idtarea + '/' + moment(this.data().t337_fecha).format("YYYYMMDD") + ',');
    //    });

    //    $("#hdnConsumos").val(sb.toString());

    //    if ($(selectores.modeloParte).val() == "0") {
    //        document.forms["frmDatos"].action = IB.vars["strserver"] + "Capa_Presentacion/IAP/ParteAct/ExportarModA/default.aspx";
    //    } else {
    //        document.forms["frmDatos"].action = IB.vars["strserver"] + "Capa_Presentacion/IAP/ParteAct/ExportarModB/default.aspx";
    //    }

    //    document.forms["frmDatos"].target = "_blank";
    //    document.forms["frmDatos"].submit();

    //    IB.procesando.ocultar();

    //}

    var inicializarTabla = function () {

        var dt = dom.tabla.DataTable({
            columns: [
                        { "data": null },
                        { "data": "t306_icontrato" }, // - t332_destarea .replace(/"/g, "'")                        
                        { "data": "t377_idextension" },                       
                        { "data": "t377_denominacion" },
                        //{ "data": "ta212_idorganizacioncomercial" },
                        { "data": "ta212_denominacion" },
                        //{ "data": "t302_codigoexterno" },
                        { "data": "cliente" },
                        //{ "data": "loginhermes_comercial" },
                        { "data": "comercial" },
                        //{ "data": "loginhermes_gestor" },
                        { "data": "gestor" },
                        { "data": "t377_fechacontratacion" },
                        { "data": "t377_importeser", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "t377_marpreser", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "t377_importepro", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "t377_marprepro", render: $.fn.dataTable.render.number('.', ',', 2, '') }
                        ,{ "data": "t195_denominacion" }
            ],
            data: [],
            'columnDefs': [
                {
                    'targets': 0,
                    'checkboxes': {
                        'selectRow': true
                    }
                },
                //{ //Descripción DE OPORTUNIDAD
                //    "targets": 3,
                //    "width": 100
                //    "render": function (data, type, row) {return accounting.formatNumber(data, 0) + ' - ' + row.descoport.replace(/"/g, "'");}
                //},
                { //fecha
                    "targets": 8,
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
                        }
                    }
                },
                {
                    'targets': 9,
                    "className": "numericCol"
                },
                {
                    'targets': 10,
                    "className": "numericCol"
                },
                {
                    'targets': 11,
                    "className": "numericCol"
                },
                {
                    'targets': 12,
                    "className": "numericCol"
                }

            ],
            'select': {
                'style': 'multi'
            },
            'order': [[1, 'asc']],
            bFilter: false,
            paging: false,
            bInfo: false,
            procesing: true,
            scrollCollapse: true,
            scrollY: "50vh",
            scrollX: true,
            //"autoWidth": false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            "createdRow": function (row, data, rowIndex) {                
                $(row).attr('id', data.t306_icontrato);
                $(row).attr('idOC', data.ta212_idorganizacioncomercial);
                $(row).attr('idCli', data.t302_idcliente_contrato);
                $(row).attr('idCo', data.t314_idusuario_comercialhermes);
                $(row).attr('idGe', data.t314_idusuario_gestorprod);
                $(row).attr('codredGe', data.codred_gestor_produccion);
                $(row).attr('mon', data.t422_idmoneda);
                $(row).attr('dur', data.duracion);
                $(row).attr('tip', data.tipocontrato);
                $(row).attr('idNLO', data.t195_idlineaoferta);
            },
            "initComplete": function (settings, json) {
                visualizarContenido();
            }
        });

    }

    var inicializarDatepickers = function () {

        //var hoy = moment();
        $('#txtRango').daterangepicker({
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aceptar',
                cancelLabel: 'Cancelar',
            },
            startDate: moment().subtract('months', 1),
            endDate: moment(),
            opens: "left",
            linkedCalendars: false
        });

    }

    var pintarTablaOportunidades = function (data) {

        dom.tabla.DataTable().clear().draw();
        dom.tabla.DataTable().rows.add(data).draw();
        dom.tabla.DataTable().draw();

    }

    var vaciarTabla = function () {

        dom.tabla.DataTable().clear().draw();

    }

    var visualizarContenido = function () {

        $(selectores.contenedor).css('visibility', 'visible');

    }

    var visualizarContenidoSR = function () {

        //Para que al cerrar la modal los elementos de la pantalla principal estén visibles al SR
        $('.ocultable').attr('aria-hidden', 'false');

    }

    //Fin funcionalidad pantalla principal

    //Funcionalidades generales pantalla

    var limpiarFiltro = function (filtro) {

        filtro.attr('value', "");
        filtro.val("").change();
    }

    var volcarTxt = function (target, valor) {

        target.val(valor).change();

    }

    var volcarTxtValue = function (target, valor, id) {

        target.attr('value', id);
        target.val(valor).change();

    }

    var mostrarModalProyecto = function (data) {

        //Se inicializan las datatables si no han sido inicializadas antes
        if (!$.fn.DataTable.isDataTable('#tablaProyectos')) {
            inicializarDatatablesProyectos();
        }
        else {
            //Se vacia la lista de proyectos
            //dom.tablaProyectosModal.DataTable().clear().draw();
            dom.tablaProyectosModal.DataTable().rows.add(data).draw();
        }
        //Se oculta el contenido inferior al lector de pantallas
        $('.ocultable').attr('aria-hidden', 'true');

        dom.modalProyecto.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        dom.modalProyecto.modal('show');
    }
    var inicializarDatatablesProyectos = function () {

        var dt = dom.tablaProyectosModal.DataTable({
            columns: [
                        {
                            "data": "cod_proyecto", 
                            render: function (data) {return accounting.formatNumber(data, 0);}
                        },
                        { "data": "nom_proyecto",  },
                        {
                            "data": "cod_contrato", 
                            render: function (data) {return accounting.formatNumber(data, 0);}
                        },
                        {
                            "data": "cod_extension", 
                            render: function (data) { return accounting.formatNumber(data, 0); }
                        }

                    ],
            data: [],
            //'order': [[1, 'asc']],
            bFilter: false,
            paging: false,
            bInfo: false,
            //procesing: true,
            //scrollCollapse: true,
            scrollY: "25vh",
            //scrollX: true,
            //searching: true,
            //dom: 'Bft',
            buttons: [],
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            responsive: false
        });

    }

    var vaciarTablaProyectos = function (data) {

        dom.tablaProyectosModal.DataTable().clear().draw();

    }

    var pintarTablaProyectos = function (data) {

        dom.tablaProyectosModal.DataTable().rows.add(data).draw();

    }


    //Fin funcionalidades generales pantalla

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        deAttachEvents: deAttachEvents,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        deAttachLiveEvents: deAttachLiveEvents,
        pintarTablaOportunidades: pintarTablaOportunidades,
        visualizarContenido: visualizarContenido,
        visualizarContenidoSR: visualizarContenidoSR,
        limpiarFiltro: limpiarFiltro,
        volcarTxt: volcarTxt,
        volcarTxtValue: volcarTxtValue,
        mostrarModalProyecto: mostrarModalProyecto,
        vaciarTablaProyectos: vaciarTablaProyectos
    }
})();