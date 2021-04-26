var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.viewCatAccion = (function () {

    var _dtFilter = null;
    var _ta204_idaccionpreventa = null;
    var hoy = moment().format("MM-DD-YYYY");

    var dom = {
        radios : $("input[type='radio']")
    }
    var selector = {
        edicion_accion: ".fk_edicion_accion",
        edicion_tarea: ".fk_edicion_tarea",
        edicion_tareaParticipante: ".fk_edicion_tareaParticipante",
        radios: "input[type='radio']",
        tabTareasActivasParticipante: "#tabTareasActivasParticipante",
        tabs: "#ulTabs li"
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function init() {

        _dtFilter = -1;

        if (IB.vars.filters) {            
            _dtFilter = IB.vars.filters.idaccion == -1 ? null : IB.vars.filters.idaccion; 
            
        }
        
        initDatatableAccion();        
        initDatatableTarea(_dtFilter);
        initDatatableParticipantes();


        if (IB.vars.filters) {
            
            //Datatable tareas activas como participante
            if (IB.vars.filters.radioSeleccionado == "2") {                                
                $("#tabTareasActivasParticipante").trigger("click");                
                $("#divTareas").css("display", "none");
                $("#divTareasParticipante").css("display", "block");                
            }
        }

    }

    function initDatatableAccion() {

        var dt = $('#tblAcciones').DataTable({
            "language": { "url": "../../plugins/datatables/literales-no-paginado.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,            
            "order": [], 
            "scrollY": "20vh",
            "scrollX": true,
            "scrollCollapse": false,
            "fixedColumns": {
                "leftColumns": 2
            },

            "ajax": {
                "url": "Default.aspx/CatalogoMisAcciones",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "dataSrc": function (data) {
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status == "abort") return;
                    IB.bserror.error$ajax(ex, status);
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
                
                
            ],

            "columnDefs": [
                {
                    //ta204_idaccionpreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<a class='underline fk_edicion_accion' href='#' data-idaccion='" + row.ta204_idaccionpreventa + "'>" + accounting.formatNumber(row.ta204_idaccionpreventa, 0, ".") + "</a>";
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
                    "render": {
                        "display": function (data, type, row, meta) {
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
                    }

                },

                {
                    //importe
                    "targets": 3,
                    "className": "text-right",
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<nobr>" + accounting.formatNumber(data, 2, ".", ",") + " " + row.moneda + "</nobr>";

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
                    //promotor
                    "targets": 5,
                    "className": "text-left"                   
                },
                {
                    //ta204_fechacreacion
                    "targets": 9,
                    "className": "text-center",
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
                    //ta204_fechafinestipulada
                    "targets": 10,
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

                $(row).attr("data-ta204_idaccionpreventa", data.ta204_idaccionpreventa) //Asignar id a la fila
               
            },

            "drawCallback": function (settings) {
                //marcar la fila que estaba seleccionada                
                if (IB.vars.filters && IB.vars.filters.radioSeleccionado != "2") {
                    $("#tblAcciones_wrapper tbody tr[data-ta204_idaccionpreventa='" + IB.vars.filters.idaccion + "']").addClass("selected");                   
                }

            },

            "initComplete": function (row, data, index) {                
                if ($("#tblAcciones tr[data-ta204_idaccionpreventa='" + IB.vars.ta204_idaccionpreventa + "']").length > 0) {
                    $("#tblAcciones tr[data-ta204_idaccionpreventa='" + IB.vars.ta204_idaccionpreventa + "'] a").trigger("click");
                }

                if ($("#tblAcciones_wrapper tbody tr.selected").length === 0) {
                    refreshDatatableTarea(-1);
                }
            }


        })

        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

    function initDatatableTarea(ta204_idaccionpreventa) {

        var dt = $('#tblTareas').DataTable({
            "language": { "url": "../../plugins/datatables/literales-no-paginado.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,
            "order": [], 
            "scrollY": "20vh",
            "scrollX": true,
            "scrollCollapse": false,
            "fixedColumns": {
                "leftColumns": 2
            },


            "ajax": {
                "url": "Default.aspx/obtenerTareasbyAccion",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ ta204_idaccionpreventa: _dtFilter }); },
                "dataSrc": function (data) {
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status == "abort") return;
                    IB.bserror.error$ajax(ex, status);
                }

            },

            "columns": [
                { "data": "ta207_idtareapreventa" },
                { "data": "ta207_denominacion" },
                { "data": "participantes" },
                { "data": "ta207_estado" },
                { "data": "ta207_fechacreacion" },
                { "data": "ta207_fechafinprevista" },
                { "data": "ta207_fechafinreal" }
                


            ],

            "columnDefs": [
                {
                    //ta207_idtareapreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<a class='underline fk_edicion_tarea' href='#' data-idaccion='" + _dtFilter + "' data-idtarea='" + row.ta207_idtareapreventa + "'>" + row.ta207_idtareapreventa + "</a>";
                        }
                    }
                },

                {
                    //ta207_denominacion
                    "targets": 1,
                    "className": "text-left",
                },

                {
                    //participantes
                    "targets": 2,
                    "className": "text-left",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (row.participantes != null)
                                return row.participantes.split("|").join("<br />");
                            else
                                return "";

                        }
                    }
                },

                {
                    //estado
                    "targets": 3,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, met) {
                            return GetLiteralEstadoTarea(row.ta207_estado)
                        }
                    }

                },

                {
                    //ta207_fechafinprevista
                    "targets": 5,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                if (moment(hoy).isAfter(data) && row.ta207_estado === "A")
                                    return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else if (moment(hoy).add(7, 'day').isAfter(data) && row.ta207_estado === "A")
                                    return "<span style='color:orange;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else
                                    return "<span>" + moment(data).format('DD/MM/YYYY') + "</span>";
                            else
                                return "";
                        }
                    }
                },

                {
                    //ta207_fecha fin real
                    "targets": 6,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                if (moment(data) > moment(row.ta207_fechafinprevista))
                                    return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else
                                    return "<span style='color:green;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";

                            else
                                return "";
                        }
                    }
                },

                 {
                     //ta207_fechacreación
                     "targets": 4,
                     "className": "text-center",
                     "render": {
                         "display": function (data, type, row, meta) {
                             if (data != "0001-01-01T00:00:00")
                                 return moment(data).format('DD/MM/YYYY');
                             else
                                 return "";
                         }
                     }
                 }

            ],

            "createdRow": function (row, data, index) {
                //fila creada
                if (data.ta208_negrita)
                    $(row).css("font-weight", "bold").attr("data-negrita", "true");
                else
                    $(row).css("font-weight", "normal");

                $(row).attr("data-ta207_idtareapreventa", data.ta207_idtareapreventa);

            },

        })

        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

    function initDatatableParticipantes() {
        //aquí
        var dt = $('#tblTareasParticipante').DataTable({
            "language": { "url": "../../../../plugins/datatables/Spanish.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,
            "order": [],
            "scrollY": "50vh",
            "scrollX": true,
            "scrollCollapse": false,
            "fixedColumns": {
                "leftColumns": 2
            },
            "ajax": {
                "url": "Default.aspx/misTareasComoParticipante",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",                
                "dataSrc": function (data) {

                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status == "abort") return;
                    IB.bserror.error$ajax(ex, status);
                }

            },

            "columns": [
                { "data": "ta207_idtareapreventa" },
                { "data": "ta207_denominacion" },
                { "data": "participantes" },



                { "data": "ta205_denominacion" },
                { "data": "ta206_itemorigen" },
                
                { "data": "den_cuenta" },
                { "data": "solicitante" },
                { "data": "ta200_denominacion" },
                { "data": "ta201_denominacion" },
                { "data": "lider" },
                { "data": "ta207_fechacreacion" },                
                { "data": "ta207_fechafinprevista" },
                
               


            ],

            "columnDefs": [
                {
                    //ta207_idtareapreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<a class='underline fk_edicion_tareaParticipante' href='#' data-idaccion='" + _dtFilter + "' data-idtarea='" + row.ta207_idtareapreventa + "'>" + row.ta207_idtareapreventa + "</a>";
                        }
                    }
                },

                {
                    //ta207_denominacion
                    "targets": 1,
                    "className": "text-left",
                },

                {
                    //participantes
                    "targets": 2,
                    "className": "text-left",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (row.participantes != null)
                                return row.participantes.split("|").join("<br />");
                            else
                                return "";

                        }
                    }
                },

                {
                    //Acción preventa
                    "targets": 3,
                    "className": "text-center",
                },

                {
                    //itemOrigen
                    "targets": 4,
                    "className": "text-left",
                    "render": {
                        "display": function (data, type, row, meta) {
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
                    }

                },

                {
                    //Cuenta
                    "targets": 5,
                    "className": "text-center"
                },

                  {
                      //Solicitante
                      "targets": 6,
                      "className": "text-center"
                  },

                {
                    //Área
                    "targets": 7,
                    "className": "text-center"
                },

                  {
                      //Subárea
                      "targets": 8,
                      "className": "text-center"
                  },

                    {
                        //Líder
                        "targets": 9,
                        "className": "text-center"
                    },

                {
                    //ta207_fecha fin prevista
                    "targets": 11,
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
                        }
                    }
                },

                 {
                     //ta207_fechacreación
                     "targets": 10,
                     "className": "text-center",
                     "render": {
                         "display": function (data, type, row, meta) {
                             if (data != "0001-01-01T00:00:00")
                                 return moment(data).format('DD/MM/YYYY');
                             else
                                 return "";
                         }
                     }
                 }

            ],

            "createdRow": function (row, data, index) {
                //fila creada
                if (data.ta208_negrita)
                    $(row).css("font-weight", "bold").attr("data-negrita", "true");
                else
                    $(row).css("font-weight", "normal");

                $(row).attr("data-ta207_idtareapreventa", data.ta207_idtareapreventa).attr("data-idaccion", data.ta204_idaccionpreventa);

            },
            
        })

        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

    //Marcar fila seleccionada en datatable
    $(document).on("click", "#tblAcciones_wrapper tbody tr", function () {
        var id = $(this).attr("data-ta204_idaccionpreventa")
        addSelected(id);        
    });

    function addSelected(id) {

        var checkSeleccionado = obtenerRadioSeleccionado();
        
        //Tareas asociadas a una acción
        if (checkSeleccionado == "1") {
            $("#tblAcciones_wrapper table tbody tr").removeClass("selected");
            $("#tblAcciones_wrapper tbody tr[data-ta204_idaccionpreventa='" + id + "']").addClass("selected");
            _ta204_idaccionpreventa = $("#tblAcciones_wrapper table tbody tr.selected").attr("data-ta204_idaccionpreventa");
            
            if (typeof _ta204_idaccionpreventa === "undefined") return;

            refreshDatatableTarea(_ta204_idaccionpreventa);
        }
        
    }


    function refreshDatatableAccion(filter) {

        _dtFilter = filter;

        var oDataTable = $('#tblAcciones').DataTable();

        oDataTable.clear().draw();
        oDataTable.ajax.reload(null, true);
        
    }

    function refreshDatatableTarea(filter) {

        _dtFilter = filter;
        
        var oDataTable = $('#tblTareas').DataTable();

        oDataTable.clear().draw();
        oDataTable.ajax.reload(null, true);
    }

    function refreshDatatableTareaParticipante() {

        var oDataTable = $('#tblTareasParticipante').DataTable();

        oDataTable.clear().draw();
        oDataTable.ajax.reload(null, true);              
    }

    //Solución al problema de recalcular los anchos de columnas del Datatable
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $($.fn.dataTable.tables(true)).DataTable()
           .columns.adjust();
    });

    function tieneNegrita(row) {
        if ($(row).parent().parent().attr("data-negrita") == "true") return true;
        else return false;
    }


    function obtenerRadioSeleccionado() {
        return $("input[type='radio']:checked").val();
    }

    function tabSeleccionado() {
       
        if ($("#liTareasActivasParticipante").hasClass("active")) {
            _ta204_idaccionpreventa = $("#tblAcciones_wrapper table tbody tr.selected").attr("data-ta204_idaccionpreventa");
            //$("#tblAcciones_wrapper table tbody tr.selected").removeClass("selected");            
            $("#divTareas").css("display", "none");
            $("#divTareasParticipante").css("display", "block");
            refreshDatatableTareaParticipante();
            
        }
        else {
            
            $("#divTareas").css("display", "block");
            $("#divTareasParticipante").css("display", "none");

            if (_ta204_idaccionpreventa == null) {
                $("#tblAcciones_wrapper tbody tr:first").addClass("selected");
                $("#tblAcciones_wrapper tbody tr:first").trigger("click");
            }
            else
                $("#tblAcciones_wrapper tbody tr[data-ta204_idaccionpreventa='" + _ta204_idaccionpreventa + "']").addClass("selected");
            
        }

    }



    //Obtiene el literal del estado pasado por parámetro
    function GetLiteralEstadoTarea(estado) {
        switch (estado) {
            case "A": return "En curso";
            case "F": return "Finalizada";
            case "FS": return "Cerrada";
            case "FA": return "Cerrada";
            case "X": return "Anulada";
            case "XS": return "Cerrada";
            case "XA": return "Cerrada";
            default: return "";
        }
    }

    return {
        selector: selector,
        init: init,
        attachLiveEvents: attachLiveEvents,
        tieneNegrita: tieneNegrita,
        addSelected: addSelected,
        refreshDatatableTarea: refreshDatatableTarea,
        tabSeleccionado: tabSeleccionado,
        obtenerRadioSeleccionado: obtenerRadioSeleccionado
        

    }

})();






