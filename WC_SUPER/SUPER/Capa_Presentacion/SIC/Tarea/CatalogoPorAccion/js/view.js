var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view = (function () {
    var dom = {
        btnAddTarea: $("#btnAddTarea"),
        btnCerrar : $("#btnCerrar")
    }

    var selector = {
        edicion_accion: ".fk_edicion_accion"
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
        initDatatable()
    }

    function initDatatable() {

        var dt = $('#tblTareas').DataTable({
            "language": { "url": "../../../../plugins/datatables/Spanish.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": false,
            "autoWidth": true,            
            "order": [],
            "scrollY": "300px",            
            "ajax": {
                "url": "Default.aspx/obtenerTareasbyAccion",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ ta204_idaccionpreventa: IB.vars.ta204_idaccionpreventa }); },
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
                { "data": "ta207_estado" },         
                { "data": "ta207_fechacreacion" },
                { "data": "ta207_fechafinprevista"},                
                { "data": "ta207_fechafinreal" },
                
                
                
            ],

            "columnDefs": [
                {
                    //ta207_idtareapreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (row.accesoadetalle)
                                return "<a class='underline fk_edicion_accion' href='#' data-idaccion='" + IB.vars.ta204_idaccionpreventa + "' data-idtarea='" + row.ta207_idtareapreventa + "'>" + row.ta207_idtareapreventa + "</a>";
                            else
                                return "<span>" + row.ta207_idtareapreventa + "</span>";
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
                    //participantes
                    "targets": 2,
                    "className": "text-left",
                   "render": function (data, type, row, meta) {
                           if (row.participantes != null)
                               return row.participantes.split("|").join("<br />");
                           else
                               return "";
                                                       
                        }                    
                },

                {
                    //estado
                    "targets": 3,
                    "className": "text-center",
                    "render": function (data, type, row, met) {
                            return GetLiteralEstadoTarea(row.ta207_estado)
                        }                   
                },

                {
                    //ta207_fechafinprevista
                    "targets": 5,
                    "className": "text-center",
                    "render": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
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
                                    return "<span style='color:red;font-weight:bold'>" +moment(data).format('DD/MM/YYYY') + "</span>";
                                    else
                                    return "<span style='color:green;font-weight:bold'>" +moment(data).format('DD/MM/YYYY') + "</span>";

                                    else
                                        return "";
                        },
                        "filter": function (data, type, row, meta) {
                                    return moment(data).format('DD/MM/YYYY');
                                    }
            }
                    
                },

                 {
                     //ta207_fechacreación
                     "targets": 4,
                     "className": "text-center",
                     "render": function (data, type, row, meta) {
                             if (data != "0001-01-01T00:00:00")
                                 return moment(data).format('DD/MM/YYYY');
                             else
                                 return "";
                         }
                     
                 }

            ],

            "createdRow": function (row, data, index) {
                if (data.ta208_negrita)
                    $(row).css("font-weight", "bold").attr("data-negrita", "true");
                else
                    $(row).css("font-weight", "normal");

            },

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

    function pintaInfoCRM(data) {

        //todo poner los campos a mostrar
        switch (_ta206_itemorigen) {
            case "O":
                $("#lblOportunidadSolic").text("Oportunidad");
                $("#acc-txtOportunidadSolic").val(data.iditemorigen);
                $("#acc-txtDenominacionSolic").val(data.denominacion);
                $("#acc-txtClienteSolic").val(data.cuenta);


                $("#txtIditemorigen_cab").val(data.iditemorigen);
                $("#txtDenominacion_cab").val(data.denominacion);
                $("#txtCuenta_cab").val(data.cuenta);
                $("#div_txtCuenta_cab").css("display", "block");
                $("#txtOportExt").val(data.num_oportunidad);
                $("#txtdenOportExt_cab").val(data.den_oportunidad);
                $("#txtComercial_cab").val(data.comercial);
                $("#txtOrganizacionComercial_cab").val(data.organizacionComercial);
                $("#txtGestorProduccion_cab").val(data.gestorProduccion_nombre);
                $("#txtImporte_cab").val(data.importe);
                $("#txtRentabilidad_cab").val(data.rentabilidad);
                $("#txtProbabilidadExisto_cab").val(data.exito);
                $("#txtAreaConTecno_cab").val(data.areaConTecnologico);
                $("#txtAreaConSectorial_cab").val(data.areaConSectorial);
                $("#txtDuracionProyecto_cab").val(data.duracionProyecto);
                $("#txtFechaCierre_cab").val(data.fechaCierre);
                $("#txtEtapaVentas_cab").val(data.etapaVentas);
                $("#txtEstado_cab").val(data.estado);
                $("#txtCentroResponsabilidad_cab").val(data.centroResponsabilidad)




                break;
            case "E":
                $("#lblOportunidadSolic").text("Extensión");
                $("#acc-txtOportunidadSolic").val(data.iditemorigen);
                $("#acc-txtDenominacionSolic").val(data.denominacion);
                $("#acc-txtClienteSolic").val(data.cuenta);


                $("#txtIditemorigen_cab").val(data.iditemorigen);
                $("#txtDenominacion_cab").val(data.denominacion);
                $("#txtCuenta_cab").val(data.cuenta);
                $("#div_txtCuenta_cab").css("display", "block");
                $("#txtOportExt").val(data.num_oportunidad);
                $("#txtdenOportExt_cab").val(data.den_oportunidad);
                $("#txtComercial_cab").val(data.comercial);
                $("#txtOrganizacionComercial_cab").val(data.organizacionComercial);
                $("#txtGestorProduccion_cab").val(data.gestorProduccion_nombre);
                $("#txtImporte_cab").val(data.importe);
                $("#txtRentabilidad_cab").val(data.rentabilidad);
                $("#txtProbabilidadExisto_cab").val(data.exito);
                $("#txtAreaConTecno_cab").val(data.areaConTecnologico);
                $("#txtAreaConSectorial_cab").val(data.areaConSectorial);
                $("#txtDuracionProyecto_cab").val(data.duracionProyecto);
                $("#txtFechaCierre_cab").val(data.fechaCierre);
                $("#txtEtapaVentas_cab").val(data.etapaVentas);
                $("#txtEstado_cab").val(data.estado);
                $("#txtCentroResponsabilidad_cab").val(data.centroResponsabilidad)


                break;
            case "P":
                $("#lblOportunidadSolic").text("Objetivo");
                $("#acc-txtOportunidadSolic").val(data.iditemorigen);
                $("#acc-txtDenominacionSolic").val(data.denominacion);
                $("#acc-txtClienteSolic").val(data.cuenta);

                $("#txtIditemorigen_cab").val(data.iditemorigen);
                $("#txtDenominacion_cab").val(data.denominacion);

                $("#txtOferta_cab").val(data.oferta);
                $("#txtFechaInicio_cab").val(data.fechaInicio);
                $("#txtContataPrevista_cab").val(data.contratacionPrevista);
                $("#txtFechaFin_cab").val(data.fechaFin);
                $("#txtCoste_cab").val(data.costePrevisto);
                $("#txtResultado_cab").val(data.resultado);


               
                $("#txtEstado_cabObj").val(data.estado);
                $("#txtOrgComercial_Objetivo").val(data.organizacionComercial);
                $("#txtComercial_cabObjetivo").val(data.comercial);
                $("#txtDescObjetivo").val(data.desc_objetivo);

                $("#txtTipoNegocio").val(data.tipo_negocio);

                //dom.txtOferta_cab.val(data.oferta);
                //dom.txtFechaInicio_cab.val(data.fechaInicio);
                //dom.txtContataPrevista_cab.val(data.contratacionPrevista);
                //dom.txtFechaFin_cab.val(data.fechaFin);
                //dom.txtCoste_cab.val(data.costePrevisto);

                break;

            case "S":
                $("#acc-txtOportunidadSolic").val(data.ta206_idsolicitudpreventa);
                $("#acc-txtDenominacionSolic").val(data.ta206_denominacion);
                $("#divInformacionAdicional").css("display", "none");
                
                break;

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
        dom:dom,
        selector: selector,
        init: init,
        attachLiveEvents: attachLiveEvents,
        attachEvents: attachEvents,
        tieneNegrita: tieneNegrita,
        pintaInfoCRM:pintaInfoCRM

    }

})();