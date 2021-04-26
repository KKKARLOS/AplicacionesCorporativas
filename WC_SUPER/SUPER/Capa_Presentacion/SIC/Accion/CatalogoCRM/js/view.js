var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.viewCatAccion = (function () {

    var _ta206_itemorigen;
    var _ta206_iditemorigen;
    var hoy = moment().format("MM-DD-YYYY");
    
    var dom = {
        txtIditemorigen_cab: $("#txtIditemorigen_cab"),
        txtDenominacion_cab: $("#txtDenominacion_cab"),
        txtCuenta_cab: $("#txtCuenta_cab"),
        txtOportExt: $("#txtOportExt"),
        txtdenOportExt_cab: $("#txtdenOportExt_cab"),
        modalimputaciones: $("#modal-imputaciones"),
        jornadas: $("#jornadas"),
        euros: $("#euros"),
        titleAcciones: $("#h5Title"),
        btnInsert: $("#btnInsert")
    }

    var selector = {
        edicion_accion: ".fk_edicion_accion",
        linkImputaciones: "#linkImputaciones"
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function init(ta206_itemorigen, ta206_iditemorigen) {

        _ta206_itemorigen = ta206_itemorigen;
        _ta206_iditemorigen = ta206_iditemorigen;

        switch (_ta206_itemorigen) {
            case "O":
                dom.titleAcciones.text("Acciones preventa solicitadas para la oportunidad");
                break;
            case "E":
                dom.titleAcciones.text("Acciones preventa solicitadas para la extensión");
                break;
            case "P":
                dom.titleAcciones.text("Acciones preventa solicitadas para el objetivo");
                break;           
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
            "order": [],
            "scrollY": "40vh",
            "scrollX": true,
            "scrollCollapse": false,
            "fixedColumns": {
                "leftColumns": 2
            },


            "ajax": {
                "url": "Default.aspx/obtenerAccionesCRM",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ ta206_itemorigen: _ta206_itemorigen, ta206_iditemorigen: _ta206_iditemorigen }); },
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
                { "data": "unidadPreventa" },
                { "data": "areaPreventa" },
                { "data": "subareaPreventa" },
                { "data": "lider" },
                { "data": "promotor" },
                { "data": "estadoAccion" },
                { "data": "ta204_fechacreacion" },
                { "data": "ta204_fechafinestipulada" },
                { "data": "ta204_fechafinreal" }                                                      
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
                    //unidadPreventa
                    "targets": 2,
                    "className": "text-left",
                },
                {
                    //areaPreventa
                    "targets": 3,
                    "className": "text-left",
                },
                {
                    //subareaPreventa
                    "targets": 4,
                    "className": "text-left",
                },
                {
                    //ta204_fechafinestipulada
                    "targets": 9,
                    "type": "date",
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                if (moment(hoy).isAfter(data) && row.estadoAccion === "A")
                                    return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else if (moment(hoy).add(7, 'day').isAfter(data) && row.estadoAccion === "A")
                                    return "<span style='color:orange;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else
                                    return "<span>" + moment(data).format('DD/MM/YYYY') + "</span>";
                            else
                                return "";
                        },
                        "filter": function (data, type, row, meta) {
                            return moment(data).format('DD/MM/YYYY');
                        },
                    }

                },
                {
                    //ta204_fechacreacion
                    "targets": 8,
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
                        },
                    }
                },
                {
                    //estadoAccion
                    "targets": 7,
                    "className": "text-left",
                    "render": function (data, type, row, meta) {
                        return getLiteralEstado(data);

                    }
                },
                {
                    //lider
                    "targets": 5,
                    "className": "text-left",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != null)
                                return "<nobr>" + data + "</nobr>"
                            else
                                return "";
                        }
                    }
                },
                {
                    //promotor
                    "targets": 6,
                    "className": "text-left",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != null)
                                return "<nobr>" + data + "</nobr>"
                            else
                                return "";
                        }
                    }
                },
                {
                    //fecha fin real
                    "targets": 10,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                if (moment(data) > moment(row.ta204_fechafinestipulada))
                                    return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else
                                    return "<span style='color:green;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";

                            else
                                return "";
                        },
                        "filter": function (data, type, row, meta) {
                            return moment(data).format('DD/MM/YYYY');
                        },
                    }
                }

            ],

            "createdRow": function (row, data, index) {
                //$(row).attr("data-idaccion", data.ta204_idaccionpreventa) //Asignar id a la fila
            },
            "drawCallback": function (settings) {
                //$("#dtCatalogoPedidos a[data-toggle='tooltip']").tooltip();
            }

        })

        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

  
    function getLiteralEstado(estado) {
        switch (estado) {
            case "A": return "Abierta";
            case "F": return "Finalizada";
            case "X": return "Anulada";

                //heredados de la solicitud
            case "FS": return "Cerrada"; //anulada por finalización de solicitud
            case "XS": return "Cerrada"; //anulada por anulación de solicitud

            default: throw ("Estado no contemplado");
        }
    }

    function mostrarBoton(mostrar) {
        if (mostrar)
            $("#btnInsert").css("display", "inline-block");
        else
            $("#btnInsert").css("display", "none");
    }

    function pintaInfoCRM(data) {

        switch (_ta206_itemorigen) {
            case "O":
                dom.txtIditemorigen_cab.val(data.iditemorigen);
                dom.txtDenominacion_cab.val(data.denominacion);
                dom.txtCuenta_cab.val(data.cuenta);
                break;
            case "E":
                dom.txtIditemorigen_cab.val(data.iditemorigen);
                dom.txtDenominacion_cab.val(data.denominacion);
                dom.txtCuenta_cab.val(data.cuenta);
                dom.txtOportExt.val(data.num_oportunidad);
                dom.txtdenOportExt_cab.val(data.den_oportunidad);
                break;
            case "P":
                dom.txtIditemorigen_cab.val(data.iditemorigen);
                dom.txtDenominacion_cab.val(data.denominacion);
                dom.txtCuenta_cab.val(data.cuenta);
                break;
        }

        if (data.botonactivo) 
            $("#btnInsert").css("display", "inline-block");
        else
            $("#btnInsert").css("display", "none");
       
    }

    function rendermodalImputaciones(data) {
        $("#modal-imputaciones").modal("show");
        //data[0].jornadas = 2.333333;
        //data[0].euros = 45.555;
        dom.jornadas.text(data[0].jornadas.toFixed(2));
        dom.euros.text(data[0].euros.toFixed(2));
    }

    return {
        selector: selector,
        init: init,
        attachEvents:attachEvents,
        attachLiveEvents: attachLiveEvents,
        pintaInfoCRM: pintaInfoCRM,
        rendermodalImputaciones: rendermodalImputaciones,
        mostrarBoton: mostrarBoton
    }

})();






