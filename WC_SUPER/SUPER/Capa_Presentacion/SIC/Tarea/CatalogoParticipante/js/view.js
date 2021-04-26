var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view = (function () {

    var hoy = moment().format("MM-DD-YYYY");

    var selector = {
        edicion_accion: ".fk_edicion_accion"
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function init()
    {
        initDatatable()
        
    }

    function initDatatable() {

        var dt = $('#tblTareas').DataTable({
            "language": { "url": "../../plugins/datatables/literales-no-paginado.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,            
            "order": [], 
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
                //"data": function () { return JSON.stringify({ requestFilter: rf }); },
                "dataSrc": function (data) {
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
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
                { "data": "ta201_denominacion" },
                { "data": "ta200_denominacion" },
                { "data": "lider" },
                { "data": "ta207_fechacreacion" },
                { "data": "ta207_fechafinprevista" },                                
                //{ "data": "ta199_denominacion" },
                
            ],

            "columnDefs": [
                {
                    //ta207_idtareapreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<a class='underline fk_edicion_accion' href='#' data-idaccion='"+ row.ta204_idaccionpreventa +"' data-idtarea='" + row.ta207_idtareapreventa + "'>" + row.ta207_idtareapreventa + "</a>";
                        },
                        "filter": function (data, type, row, meta) {
                            return row.ta207_idtareapreventa + " " + accounting.formatNumber(row.ta207_idtareapreventa, 0, ".");
                        }
                    }
                },
                {
                    //ta207_denominacion
                    "targets": 1,
                    "className": "text-left",
                },
                {
                    //ta207_fechacreacion
                    "targets": 10,
                    "className": "text-center",
                    "render": 
                         function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
                        }
                    
                },
                {
                    //ta207_fechafinprevista
                    "targets": 11,
                    "className": "text-center",
                    "render": function (data, type, row, meta) {
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
                },
                {
                    //Tipo acción preventa
                    "targets": 3,
                    "className": "text-left",
                },

                {
                    //Subárea
                    "targets": 8,
                    "className": "text-left",
                },

                {
                    //Área
                    "targets": 7,
                    "className": "text-left",
                },
                
                {
                    //lider
                    "targets": 9,
                    "className": "text-left",
                },
                {
                    //den_cuenta
                    "targets": 5,
                    "className": "text-left",
                },
                {
                    //Solicitante
                    "targets": 6,
                    "className": "text-left",
                },
              {
                  //itemOrigen
                  "targets": 4,
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

            ],

            "createdRow": function (row, data, index) {
                if (data.ta208_negrita)
                    $(row).css("font-weight", "bold").attr("data-negrita", "true");
                else
                    $(row).css("font-weight", "normal");
            },

            "initComplete": function (row, data, index) {
                if ($("#tblTareas tr a[data-idtarea='" + IB.vars.ta207_idtareapreventa + "']").length > 0) {
                    $("#tblTareas tr a[data-idtarea='" + IB.vars.ta207_idtareapreventa + "']").trigger("click");
                }

            }           
        })


        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
        
    }

    function tieneNegrita(row) {
        if ($(row).parent().parent().attr("data-negrita") == "true") return true;
        else return false;
    }

    return {
        selector: selector,
        init: init,
        attachLiveEvents: attachLiveEvents,
        tieneNegrita: tieneNegrita
    }

})();