var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Bitacora = SUPER.IAP30.Bitacora || {}
SUPER.IAP30.Bitacora.PE = SUPER.IAP30.Bitacora.PE || {}


SUPER.IAP30.Bitacora.PE.View = (function (e) {

    var dom = {
        txtDenominacion: $('#txtDenominacion'),
        txtNotif: $('#txtNotif'),
        txtLimite: $('#txtLimite'),
        txtFin: $('#txtFin'),
        btnObtener: $('#btnObtener'),
        btnExportar: $('#btnExportar'),
        tabla: $('#tablaResultados'),
        chkAutomatica: $('#chkAutomatica'),
        chkConAcciones: $('#chkConAcciones'),
        selTipoAsunto: $('#cboTipo'),
        selEstado: $('#cboEstado'),
        selSeveridad: $('#cboSeveridad'),
        selPrioridad: $('#cboPrioridad'),
        btnGoAsunto: $('#btngoAsunto'),
        //Proyecto
        linkProy: $('#lblProy'),
        ayudaProyecto: $(".fk_ayudaProyecto"),
        IdProyectoSubnodo: $('#hdnIdProyectoSubNodo'),
        txtPro: $("#txtPro"),
        //proyecto: $("#proyecto")
        colTipoAsunto: 2,
        colEstado: 9
    }

    var selectores = {

        contenedor: ".container",
        link_proy: ".txtLinkU",
        sel_filtros: ".fk_filtro",
        sel_fechas: ".fk_fecha",
        sel_chk: ".fk_chk",
        sel_Proy: ".fk_ayudaProyecto",
        DenProy: ".fk_Proyecto",
        sel_TipoAsunto: "#cboTipo option:selected",
        sel_Estado: "#cboEstado option:selected",
        filas: "#tablaResultados tr",
        filaSel: "#tablaResultados tr.activa"
    }

    var indicadores = {
        //i_dispositivoTactil: false
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

        //if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
        //    indicadores.i_dispositivoTactil = true;
        //}

        inicializarTabla();

        inicializarDatepickers();

        //Vaciado de tabla cuando se produce un cambio en los filtros
        $(selectores.sel_filtros).on("change", cambioFiltros);
        //$(selectores.DenProy).on("keypress", cambioFiltros);
        $(selectores.link_proy).on("click", vaciarTabla);

        //validación de campos numéricos
        $(document).on('keypress', 'input.txtNum', function (e) {
            return validarTeclaNumerica(e, false);
        });

        //Para permitir borrar el rango de fechas
        $(selectores.sel_fechas).on("apply.daterangepicker", function (ev, picker) {
            $(this).val(picker.startDate.format('DD/MM/YYYY') + ' - ' + picker.endDate.format('DD/MM/YYYY'));
            cambioFechas();
        });
        $(selectores.sel_fechas).on("cancel.daterangepicker", function (ev, picker) {
            $(this).val('');
            cambioFechas();
        });


    }

    //Funcionalidad pantalla principal
    var cambioFechas = function () {
        if (dom.chkAutomatica.is(":checked")) {
            dom.btnObtener.click();
        }
        else {
            vaciarTabla();
        }
    }
    var cambioFiltros = function () {
        if (!dom.chkAutomatica.is(":checked")) {
            vaciarTabla();
        }
    }

    var inicializarTabla = function () {

        var columnas = [
                {
                    "data": "fNotificacion",
                    "type": "date ",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
                        }
                    }
                },
                { "data": "desTipo" },
                { "data": "denominacion" },
                { "data": "severidad" },
                { "data": "prioridad" },
                {
                    "data": "fLimite",
                    "type": "date ",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data === null)
                                return "";
                            else{
                                if (data != "0001-01-01T00:00:00")
                                    return moment(data).format('DD/MM/YYYY');
                                else
                                    return "";
                            }
                        }
                    }
                },
                {
                    "data": "fFin",
                    "type": "date ",
                    //"render": function (value) {
                    //    if (value === null) return "";
                    //    return moment(value).format('DD/MM/YYYY');
                    //}
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
                { "data": "avance" },
                { "data": "estado" },
                { "data": "descripcion" }
        ];

        var dt = dom.tabla.DataTable({
        //    columns: [
        //                { "data": null },
        //                { "data": "fNotificacion" }, // - t332_destarea .replace(/"/g, "'")                        
        //                { "data": "desTipo" },
        //                { "data": "denominacion" },
        //                //{ "data": "t337_esfuerzo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
        //                { "data": "severidad" },
        //                { "data": "prioridad" },
        //                { "data": "fLimite" },
        //                { "data": "fFin" },
        //                { "data": "avance" },
        //                { "data": "estado" },
        //                { "data": "descripcion" }
        //],
            "columns": columnas,
            data: [],
            //'columnDefs': [
            //    {
            //        'targets': 0,
            //        'checkboxes': {
            //            'selectRow': true
            //        }
            //    },
            //    //{ //t332_idtarea
            //    //    "targets": 1,
            //    //    "render": function (data, type, row) {
            //    //        return accounting.formatNumber(data, 0) + ' - ' + row.t332_destarea.replace(/"/g, "'");
            //    //    }
            //    //},
            //    { //t382_fnotificacion
            //        "targets": 1,
            //        "render": {
            //            "display": function (data, type, row, meta) {
            //                if (data != "0001-01-01T00:00:00")
            //                    return moment(data).format('DD/MM/YYYY');
            //                else
            //                    return "";
            //            }
            //        }
            //    },
            //    { //t382_flimite
            //        "targets": 6,
            //        "render": {
            //            "display": function (data, type, row, meta) {
            //                if (data && data != "0001-01-01T00:00:00")
            //                    return moment(data).format('DD/MM/YYYY');
            //                else
            //                    return "";
            //            }
            //        }
            //    },
            //    { //t382_ffin
            //        "targets": 7,
            //        "render": {
            //            "display": function (data, type, row, meta) {
            //                if (data && data != "0001-01-01T00:00:00")
            //                    return moment(data).format('DD/MM/YYYY');
            //                else
            //                    return "";
            //            }
            //        }
            //    }
            //    //{ //t332_facturable
            //    //    "targets": 6,
            //    //    "render": {
            //    //        "display": function (data, type, row, meta) {
            //    //            if (data == true)
            //    //                return '<i class="fa fa-check"></i>';
            //    //            else
            //    //                return "";
            //    //        }
            //    //    }
            //    //}
            //],
            'select': {
                'style': 'single'
            },
            'order': [[0, 'desc']],
            bFilter: false,
            paging: false,
            bInfo: false,
            procesing: true,
            scrollCollapse: true,
            scrollY: "50vh",
            scrollX: true,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            //"createdRow": function (row, data, rowIndex) {                
            //    $(row).attr('data-tarea', data.t332_idtarea);
            //},            
            createdRow: function (row, data, dataIndex) {
                $(row).attr('id', data.codigo);
                $(row).attr('ASoACC', data.ASoACC);
                //$(row).attr('idR', data.idUserResponsable);
                //if (IB.vars.idAsunto != "" && IB.vars.idAsunto != undefined) {
                //    if (data.codigo == IB.vars.idAsunto)
                //        marcarLinea($(row));
                //}

                if (data.ASoACC == "AC") {
                    $(row).addClass('fk_rojo');
                    if (IB.vars.idAccion != "" && IB.vars.idAccion != undefined) {
                        if (data.codigo == IB.vars.idAccion)
                            marcarLinea($(row));
                    }
                }
                else {
                    if (IB.vars.idAsunto != "" && IB.vars.idAsunto != undefined) {
                        if (data.codigo == IB.vars.idAsunto)
                            marcarLinea($(row));
                    }
                }
            },
            "initComplete": function (settings, json) {
                visualizarContenido();
            }
        });
        dom.tabla.on('search.dt', function () {
            $(selectores.filaSel).removeClass('activa');
        });
    }

    var inicializarDatepickers = function () {
        var bPonerNotif = false, bPonerLimite = false, bPonerFin = false;
        var notifD = moment();
        var notifH = moment();
        var limiteD = moment();
        var limiteH = moment();
        var finD = moment();
        var finH = moment();

        if (IB.vars.notifD != "") notifD = IB.vars.notifD;
        else  $('#txtNotif').val('');
        if (IB.vars.notifH != "") {
            notifH = IB.vars.notifH;
            bPonerNotif = true;
        }

        if (IB.vars.limiteD != "") limiteD = IB.vars.limiteD;
        else $('#txtLimite').val('');
        if (IB.vars.limiteH != "") {
            limiteH = IB.vars.limiteH;
            bPonerLimite = true;
        }
        if (IB.vars.finD != "") finD = IB.vars.finD;
        else $('#txtFin').val('');
        if (IB.vars.finH != "") {
            finH = IB.vars.finH;
            bPonerFin = true;
        }

        $('#txtNotif').daterangepicker({
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aceptar',
                cancelLabel: 'Cancelar',
            },
            startDate: notifD,
            endDate: notifH,
            opens: "left",
            linkedCalendars: false,
            disableHoverDate: true,
            autoUpdateInput: false
            //,forceUpdate:true
            //, function (startDate, endDate, period) {
            //    $(this).val(startDate.format('L') + ' – ' + endDate.format('L'))
        });
        //$('#txtNotif').val('');

        $('#txtLimite').daterangepicker({
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aceptar',
                cancelLabel: 'Cancelar',
            },
            startDate: limiteD,
            endDate: limiteH,
            opens: "left",
            linkedCalendars: false,
            disableHoverDate: true,
            autoUpdateInput: false
        //},
        //    function setFechas(desde, hasta) {
        //        $("#txtLimite").data('daterangepicker').setStartDate(desde);
        //        $("#txtLimite").data('daterangepicker').setEndDate(hasta);
        //        $('#txtLimite').val(desde + ' - ' + hasta);
        });
        //$('#txtLimite').val('');
        
        $('#txtFin').daterangepicker({
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aceptar',
                cancelLabel: 'Cancelar',
            },
            startDate: finD,
            endDate: finH,
            opens: "left",
            linkedCalendars: false,
            disableHoverDate: true,
            autoUpdateInput: false
        });
        //$('#txtFin').val('');moment(testDate).format('MM/DD/YYYY');
        
        if (bPonerNotif) {
        //    $(".fk_fecha").trigger("click");
        //    $("#txtNotif").daterangepicker("setStartDate", notifD);
        //    $("#txtNotif").daterangepicker("setEndDate", notifH);
        //    $("#txtNotif").daterangepicker("option", "currentText", notifD + ' - ' + notifH);
        //    $("#txtNotif").daterangepicker("setRange", { start: notifD, end: notifH });
            //dom.txtNotif.data('daterangepicker').element[0].value = "01/12/2017 - 31/12/2017";
            //dom.txtNotif.data('daterangepicker').element[0].value = notifD + " - " + moment(notifH).format('MM/DD/YYYY');
            dom.txtNotif.data('daterangepicker').element[0].value = notifD + " - " + notifH;
        }
        if (bPonerLimite) {
            dom.txtLimite.data('daterangepicker').element[0].value = limiteD + " - " + limiteH;
        }
        if (bPonerFin){
            dom.txtFin.data('daterangepicker').element[0].value = finD + " - " + finH;
        }
    }

    var pintarTablaBitacora = function (data) {

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

    var llenarTipoAsunto = function (data) {
        /*
        dom.selTipoAsunto.empty();
        dom.selTipoAsunto.append(
                $("<option></option>")
                    .text("Todos")
                    .val("0")
            );

        $(data).each(function (index, item) {
            alert(item.T384_idtipo + " " +item.T384_destipo);
            dom.selTipoAsunto.append(
                $("<option></option>")
                    .text(item.T384_destipo)
                    .val(item.T384_idtipo)
            );
        });
        */
        dom.selTipoAsunto.empty();
        var listItems = '<option selected="selected" value="0">Todos</option>';
        $(data).each(function (index, item) {
            listItems += "<option value='" + item.T384_idtipo + "'>" + item.T384_destipo + "</option>";
        });

        dom.selTipoAsunto.html(listItems);
        if (IB.vars.tipo != "")
            dom.selTipoAsunto.val(IB.vars.tipo);
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

    obtenerValor = function (elemento) {
        return elemento.val();
    }

    darValor = function (elemento, value) {
        elemento.val(value);
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

    //Fin funcionalidades generales pantalla

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        deAttachEvents: deAttachEvents,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        deAttachLiveEvents: deAttachLiveEvents,
        pintarTablaBitacora: pintarTablaBitacora,
        llenarTipoAsunto: llenarTipoAsunto,
        visualizarContenido: visualizarContenido,
        visualizarContenidoSR: visualizarContenidoSR,
        limpiarFiltro: limpiarFiltro,
        volcarTxt: volcarTxt,
        volcarTxtValue: volcarTxtValue,
        obtenerValor: obtenerValor,
        darValor: darValor,
        marcarLinea: marcarLinea,
    }
})();