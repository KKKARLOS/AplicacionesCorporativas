var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.viewCatAccion = (function () {

    var hoy = moment().format("MM-DD-YYYY");
    var _dtFilter = null;

    var selector = {
        edicion_accion: ".fk_edicion_accion"
    }

    var dom = {
        cmbTipoAsignacion: $("#cmbTipoAsignacion")
    }


    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function init() {

        if (IB.vars.filters) {
            dom.cmbTipoAsignacion.val(IB.vars.filters.cmbTipoAsignacion);
            _dtFilter = IB.vars.filters.cmbTipoAsignacion == -1 ? null : IB.vars.filters.cmbTipoAsignacion;
        }

        initDatatable();
    }

    function initDatatable() {

        var dt = $('#tblAcciones').DataTable({
            "language": { "url": "../../plugins/datatables/literales-no-paginado.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,
            //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
            //"pageLength": 2,
            "order": [], //ordenado por fecha fin requerida ascendente
            "scrollY": "55vh",
            "scrollX": true,
            "scrollCollapse": false,
            "fixedColumns": {
                "leftColumns": 2
            },

            "ajax": {
                "url": "Default.aspx/Catalogo",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ filter: _dtFilter }); },
                "dataSrc": function (data) {
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
                }
            },

            "columns": [
                { "data": "ta204_idaccionpreventa" },
                { "data": "tipoAccion" },
                { "data": "ta206_itemorigen" },
                { "data": "importe" },
                { "data": "den_cuenta" },
                { "data": "promotor" },
                { "data": "den_unidadcomercial" },
                { "data": "areaPreventa" },
                { "data": "subareaPreventa" },                               
                { "data": "ta204_fechacreacion" },
                { "data": "ta204_fechafinestipulada" },
                { "data": "ta201_permitirautoasignacionlider" },
                
            ],

            "columnDefs": [
                {
                    //ta204_idaccionpreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<a class='underline fk_edicion_accion' href='#' data-idaccion='" + row.ta204_idaccionpreventa + "'>" + accounting.formatNumber(row.ta204_idaccionpreventa, 0, ".") + "</a>";
                        },
                        "filter": function (data, type, row, meta) {
                            return row.ta204_idaccionpreventa + " " + accounting.formatNumber(row.ta204_idaccionpreventa, 0, ".");
                        }
                    }
                },
                {
                    //tipoAccion
                    "targets": 1,
                    "className": "text-left",
                    "render": {
                        "display": function (data, type, row, meta) {
                            var style = ""
                            if (row.estadoAccion == "A") { //en rojo las que estando abiertas no cumplen el PMR
                                var fc = moment(row.ta204_fechacreacion).format("DD/MM/YYYY"); //quitar las horas
                                if (moment(row.ta204_fechafinestipulada).isBefore(moment(fc, "DD/MM/YYYY").add(row.ta205_plazominreq, "days")))
                                    style = "style='color:red;font-weight:bold'";
                            }

                            return "<span " + style + "><nobr>" + data + "</nobr></span>";
                        },
                        "filter": function (data, type, row, meta) {
                            return data;
                        }
                    }
                },

                {
                    //itemOrigen
                    "targets": 2,
                    "className": "text-left",
                    "render": function (data, type, row, meta) {
                            var s = "<nobr>"
                            switch (data) {
                                case "O": s += "ON"; break;
                                case "E": s += "EXT"; break;
                                case "P": s += "OBJ"; break;
                                case "S": s += "SUP"; break;
                            }
                            s += " " + accounting.formatNumber(row.ta206_iditemorigen, 0, ".") + "</nobr><br />"
                            if (data == "S")
                                s += row.ta206_denominacion;
                            else
                                s += row.den_item;

                            return s;

                    }

                },

                {
                    //importe
                    "targets": 3,
                    "className": "text-right",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (row.moneda == "") return "";
                            return "<nobr>" + accounting.formatNumber(data, 2, ".", ",") + " " + row.moneda + "</nobr>";

                        },
                        "filter": function (data, type, row, meta) {
                            if (row.moneda == "") return "";
                            return data + " " + accounting.formatNumber(data, 2, ".", ",") + " " + row.moneda;
                        }
                    }

                },
                {
                    //areaPreventa
                    "targets": 7,
                    "className": "text-left",
                },
                {
                    //subareaPreventa
                    "targets": 8,
                    "className": "text-left",
                },
                {
                    //ta201_permitirautoasignacionlider
                    "targets": 11,
                    "className": "text-left",
                    "render": function (data, type, row, meta) {
                            if (data)
                                return "Permite autoasignación";
                            else
                                return "Asignado por el responsable";

                        
                    }

                },
                {
                    //promotor
                    "targets": 5,
                    "className": "text-left",
                   
                },
                {
                    //ta204_fechacreacion
                    "targets": 9,
                    "className": "text-center",
                    "type": "date",
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
                        },
                        "filter": function (data, type, row, meta) {
                            return moment(data).format('DD/MM/YYYY');
                        }
                    }

                },
                {
                    //ta204_fechafinestipulada
                    "targets": 10,
                    "type": "date",
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                if (moment(hoy).isAfter(data))
                                    return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else if (moment(hoy).add(7, 'day').isAfter(data))
                                    return "<span style='color:orange;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else
                                    return "<span>" + moment(data).format('DD/MM/YYYY') + "</span>";
                            else
                                return "";

                        },
                        "filter": function (data, type, row, meta) {
                            return moment(data).format('DD/MM/YYYY');
                        }
                    }
                },
                {
                    //den_cuenta
                    "targets": 4,
                    "className": "text-left",
                },
                {
                    //den_unidadcomercial
                    "targets": 6,
                    "className": "text-left",
                },

            ],

            "createdRow": function (row, data, index) {
                if (data.ta208_negrita)
                    $(row).css("font-weight", "bold").attr("data-negrita", "true");
                else
                    $(row).css("font-weight", "normal");
                //$(row).attr("data-idaccion", data.ta204_idaccionpreventa) //Asignar id a la fila
            },
            "drawCallback": function (settings) {
                //$("#dtCatalogoPedidos a[data-toggle='tooltip']").tooltip();
            },

            "initComplete": function (row, data, index) {
                if ($("#tblAcciones tr a[data-idaccion='" + IB.vars.ta204_idaccionpreventa + "']").length > 0) {
                    $("#tblAcciones tr a[data-idaccion='" + IB.vars.ta204_idaccionpreventa + "']").trigger("click");
                }

            }


        })

        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

    function refreshDatatable(filter) {

        _dtFilter = filter;

        var oDataTable = $('#tblAcciones').DataTable();
        //oDataTable.clear().draw();

        oDataTable.ajax.reload(null, true);
    }

    function getcmbTipoAsignacion_value() {
        return dom.cmbTipoAsignacion.find("option:selected").val();
    }

    function tieneNegrita(row) {
        if ($(row).parent().parent().attr("data-negrita") == "true") return true;
        else return false;
    }

    return {
        dom: dom,
        selector: selector,
        init: init,
        attachLiveEvents: attachLiveEvents,
        attachEvents: attachEvents,
        getcmbTipoAsignacion_value: getcmbTipoAsignacion_value,
        refreshDatatable: refreshDatatable,
        tieneNegrita: tieneNegrita
    }

})();






