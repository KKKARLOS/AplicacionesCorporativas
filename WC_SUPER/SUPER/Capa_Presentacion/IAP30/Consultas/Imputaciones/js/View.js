var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ConsuImputaciones = SUPER.IAP30.ConsuImputaciones || {}


SUPER.IAP30.ConsuImputaciones.View = (function () {


    var dom = {
                
        //fechaDesde: $('#txtDesde'),
        //fechaHasta: $('#txtHasta'),
        cboProyecto: $('#cboProyecto'),
        tablaContenido: $("#bodyTabla"),
        totalHorasReportadas: $("#txtHorasReportadas"),
        totalJornadasReportadas: $("#txtJornadasReportadas"),
        btnExportExcel: $('#btnExportExcel'),
        nivel1: $('#nivel1'),
        nivel2: $('#nivel2'),
        nivel3: $('#nivel3'),
        nivel4: $('#nivel4'),
        bomba: $('#icoBomb'),
        indicadorRedimension: $('#indicator'),
        txtRango: $('#txtRango'),
        bodyTabla: $("#bodyTabla"),
        tablaCabecera: $('#tablaCabecera'),
        tablaPie: $('#tablaPie'),
        tblDatos: $('#tblDatos'),
        ventana: $(window)

        }

    var selectores = {
            sel_lineasTabla: '#bodyTabla tr',
            sel_datepickers: "input.hasDatepicker",
            sel_nombreLinea: "span.nombreLinea",
            container: ".container"
    }

    var indicadores = {
        //i_dispositivoTactil: false
    }
        // cuando redimensionas si no cebreas la tabla se queda mal, le sale barra horizontal
        $(window).resize(function () {
            controlarScroll()
        });

        $(document).on('click', '#tblDatos tbody tr', function (e) {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                $('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });
        function attachEvents(event, selector, callback) {
            $(selector).on(event, callback);
        }

        function attachLiveEvents(event, selector, callback) {
            $(document).on(event, selector, callback);
        }
        var init = function () {
            redimensionPantalla();
            asignarControlDateRangePicker();
            //dom.fechaDesde.val(IB.vars.fechaDesde);
            //dom.fechaHasta.val(IB.vars.fechaHasta);
            //if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
            //    indicadores.i_dispositivoTactil = true;
            //}

        }
        function inicializarTabla() {

            //Contraer todas las ramas menos los proyectos económicos
            //Ocultamos todas las filas que tuvieran asignado un padre

            $("#bodyTabla tr[data-parent!='']").hide();

        }
        function cebrear() {
            $("tr:visible:not(.bg-info)").removeClass("cebreada");
            $('tr:visible:not(.bg-info):even').addClass('cebreada');

            $('tr[data-tipo="T"]').addClass('cebreaTareaHard');
            $('tr[data-tipo="T"]:even').addClass('cebreaTareaSoft');

            //$('tr[data-tipo="T"]:odd').addClass('cebreaTareaHard');
            //$('tr[data-tipo="T"]:even').addClass('cebreaTareSoft');

            $('tr[data-tipo="C"]').addClass('cebreaConsumoHard');
            $('tr[data-tipo="C"]:even').addClass('cebreaConsumoSoft');
            controlarScroll();
        }

        function redimensionPantalla() {
            if (dom.indicadorRedimension.is(':visible')) {                
                $("#ET").html('E.T. / Fec.Con. / Com.');
                //$("#H").html('H');
                $("#J").html('Jorn.');
            } else {
                $("#ET").html('Estructura técnica / Fecha consumo / Comentarios');
                //$("#H").html('Horas');
                $("#J").html('Jornadas');
            }
        }
        function controlarScroll() {
            /*Controlamos si el contenedor tiene Scroll*/

            var div = document.getElementById('bodyTabla');

            var scrollWidth = $('#bodyTabla').width() - div.scrollWidth;

            var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;

            if (hasVerticalScrollbar) {
                $("#tablaCabecera").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
                $("#tablaPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
            }
            else {
                $("#tablaCabecera").css("width", "" + $('#tblDatos').width() + "px");
                $("#tablaPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
            }

            /*FIN Controlamos si el contenedor tiene Scroll*/
        }

        var asignarControlDateRangePicker = function (e) {
            //dom.fechaDesde.datepicker();
            //dom.fechaHasta.datepicker();
            //$(document).on('focus', '#txtDesde:not(.hasDatepicker)', function (e) {
            //    $(this).datepicker({
            //        changeMonth: true,
            //        changeYear: true,
            //        beforeShow: function (input, inst) {
            //            $(this).removeClass('calendar-off').addClass('calendar-on');
            //        },
            //        onClose: function () {
            //            $(this).removeClass('calendar-on').addClass('calendar-off');
            //        }
            //    });
            //    $(this).change(function () {
            //        //....
            //    });
            //    $(this).focusout(function () {
            //        var input = $(this);
            //        window.setTimeout(function () {
            //            if ((!moment(input.val(), 'DD/MM/YYYY', 'es', true).isValid()) && (input.val() != '')) {
            //                IB.bsalert.toastdanger("Formato de fecha incorrecto: " + input.val());
            //                input.val('');
            //                input.focus();
            //            }
            //        }, 100);
            //    });
            //});

            //$(document).on('focus', '#txtHasta:not(.hasDatepicker)', function (e) {
            //    $(this).datepicker({
            //        changeMonth: true,
            //        changeYear: true,
            //        //defaultDate: $('#L').attr('data-date'),

            //        beforeShow: function (input, inst) {
            //            $(this).removeClass('calendar-off').addClass('calendar-on');
            //        },
            //        onClose: function () {
            //            $(this).removeClass('calendar-on').addClass('calendar-off');
            //        }
            //    });
            //    $(this).change(function () {
            //        //....
            //    });
            //    $(this).focusout(function () {
            //        var input = $(this);
            //        window.setTimeout(function () {
            //            if ((!moment(input.val(), 'DD/MM/YYYY', 'es', true).isValid()) && (input.val() != '')) {
            //                IB.bsalert.toastdanger("Formato de fecha incorrecto: " + input.val());
            //                input.val('');
            //                input.focus();
            //            }
            //        }, 100);
            //    });
            //});

            var fDesde, fHasta;

            if (IB.vars.fechaDesde != "") {
                fDesde = moment(IB.vars.fechaDesde, 'DD/MM/YYYY');
            } else {
                fDesde = moment();
            }

            if (IB.vars.fechaHasta != "") {
                fHasta = moment(IB.vars.fechaHasta, 'DD/MM/YYYY');
            } else {
                fHasta = moment();
            }

            $('#txtRango').daterangepicker({
                locale: {
                    format: 'DD/MM/YYYY',
                    applyLabel: 'Aceptar',
                    cancelLabel: 'Cancelar',
                },
                startDate: fDesde.format('DD/MM/YYYY'),
                endDate: fHasta.format('DD/MM/YYYY'),
                linkedCalendars: false,
                disableHoverDate: true
            });
        }

        function rellenarComboProyectos(data) {
            //Alimentar el combo de proyectos para ese rango temporal
            dom.cboProyecto.empty();
            dom.cboProyecto.append('<option value="">Todos</option>');
            $.each(data, function (i, item) {
                dom.cboProyecto.append($('<option>', {
                    value: item.t305_idproyectosubnodo,
                    text: item.t305_seudonimo
                }));
            });

            dom.cboProyecto.val(dom.cboProyecto.find('option:first').val()).change();
            IB.procesando.ocultar();
            //ObtenerDatosTabla();
        }

        //Pintado de la tabla de datos
        var pintarTablaDatos = function (data) {

            colorearNivel("1");
            var sHTML = new StringBuilder();
            var sb = new StringBuilder();
            var sfechaDia;
            var content;

            var nPE = 0, nPT = 0, nF = 0, nA = 0, nT = 0, nNivel = 0;
            var nFactual = 0, nAactual = 0;

            $.each(data, function (index, item) {
                sHTML = "";
                nFactual = item.t334_idfase;
                nAactual = item.t335_idactividad;

                if (nPE != item.t301_idproyecto) {
                    //Crear PE, PT, F, A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    nPE = item.t301_idproyecto;
                    sHTML = CrearProyEco(item);
                }
                else if (nPT != item.t331_idpt) {
                    //Crear PT, F, A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    sHTML = CrearProyTec(item);
                }
                else if ((nF != nFactual) && (nFactual != null)) {
                    //Crear F, A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    sHTML = CrearFase(item);
                }
                else if ((nA != nAactual) && (nAactual != null)) {
                    //Crear A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    if (nFactual == null)
                        nNivel = 3;
                    else nNivel = 4;
                    sHTML = CrearActividad(item, nNivel);
                }
                else if (nT != item.t332_idtarea) {
                    //Crear T y consumo
                    if (nFactual == null) {
                        if (nAactual == null) nNivel = 3;
                        else nNivel = 4;
                    }
                    else {
                        nNivel = 5;
                    }
                    nT = item.t332_idtarea;
                    sHTML = CrearTarea(item, nNivel);
                }
                else {
                    //Crear consumo
                    if (nFactual == null) {
                        if (nAactual == null) nNivel = 4;
                        else nNivel = 5;
                    }
                    else {
                        nNivel = 6;
                    }
                    sHTML = CrearConsumo(item, nNivel);
                }

                sb += sHTML.toString();
            });

            dom.tablaContenido.html(sb.toString());
            //if (!indicadores.i_dispositivoTactil) {
                $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true, animation: true });
            //}
            calcularAcumulados();
            inicializarTabla();
            cebrear();

            //var columnas = [
            //    { "data": "denT" },
            //    { "data": "denN" },
            //    { "data": "horasF", render: $.fn.dataTable.render.number('.', ',', 2, '') },
            //    { "data": "horasNF", render: $.fn.dataTable.render.number('.', ',', 2, '') }
            //];

            //var table = $("#tblDatos").DataTable({
            //    destroy: true,
            //    //"columns": columnas,
            //    //data: aNat,
            //    searching: false,
            //    scrollY: "55vh",
            //    scrollX: true,
            //    "bScrollCollapse": true,
            //    paging: false,
            //    language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            //    bInfo: false,
            //    //"footerCallback": function (row, data, start, end, display) {
            //    //    var api = this.api(), data;
            //    //    for (i = 2; i < 4; i++) {
            //    //        $(api.column(i).footer()).html(accounting.formatNumber(api.column(i).data().reduce(function (a, b) { return a + b; }, 0)));
            //    //    }
            //    //},
            //    //dom: 'f<"pull-right"B>t',
            //    //buttons: [
            //    //    {
            //    //        className: 'btnExportExcel2',
            //    //        text: 'EXCEL'
            //    //    }
            //    //]
            //});

            IB.procesando.ocultar();
        }

        function CrearFila(fila, nNivel, sTipo) {
            var sb = new StringBuilder();
            var sColor = "";
            var sContent = "";
            sb.append("<tr ");
            switch (sTipo) {
                case "PE":
                    sb.append("id='PSN" + fila.t301_idproyecto + "' data-tipo='PSN' class='linea' data-parent='' data-level='1' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                    sb.append("<td headers='PSN" + fila.t301_idproyecto + "' class='Nivel1'>");
                    sb.append("<span class='nombreLinea glyphicon-plus'></span>");
                    switch (fila.t301_estado) {
                        case "A":
                            sColor = "verde";
                            break;
                        case "H":
                            sColor = "gris";
                            break;
                        case "C":
                            sColor = "rojo";
                            break;
                        case "P":
                            sColor = "naranja";
                            break;
                    }

                    sContent = "<b>Proyecto:</b> " + fila.t301_denominacion.replace(/"/g, "") + "<br /> <b>Responsable:</b> " + fila.Responsable.replace(/"/g, "") + "<br /> <b>Cualidad:</b> " + fila.Cualidad.replace(/"/g, "") + "<br /> <b>Cliente:</b> " + fila.t302_denominacion.replace(/"/g, "") + "<br /> <b>C.R. :</b> " + fila.t303_idnodo + " - " + fila.t303_denominacion.replace(/"/g, "");

                    sb.append("<span aria-hidden='true' class='fa fa-diamond fa-diamond-" + sColor + "' data-placement='top' data-toggle='popover' data-content='" + sContent + "' title='<b>Información</b>'></span>");
                    
                    sb.append("<span aria-hidden='true' style='padding-left: 5px;'>" + accounting.formatNumber(fila.t301_idproyecto,0) + " - " + fila.t301_denominacion.replace(/"/g, "'") + "</span>");

                    sb.append("<span class='sr-only' role='button' aria-expanded='false'>Proyecto Económico - " + accounting.formatNumber(fila.t301_idproyecto,0) + " (Nivel 1)</span>");
                    sb.append("</td>");
                    break;

                case "PT":
                    sb.append("id='PT" + fila.t331_idpt + "' data-tipo='PT' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                    sb.append("<td headers='PT" + fila.t331_idpt + "' class='Nivel" + nNivel + "'>");
                    sb.append("<span class='nombreLinea glyphicon-plus'></span>");
                    sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>P</i></span>");
                    sb.append("<span aria-hidden='true'>" + fila.T331_despt.replace(/"/g, "'") + "</span>");
                    sb.append("<span class='sr-only' role='button' aria-expanded='false'>Proyecto Técnico - " + accounting.formatNumber(fila.t331_idpt,0) + fila.T331_despt.replace(/"/g, "'") + " (Nivel  " + nNivel + ")</span>");
                    sb.append("</td>");
                    break;

                case "F":
                    sb.append("id='F" + fila.t334_idfase + "' data-tipo='F' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                    sb.append("<td headers='F" + fila.t334_idfase + "' class='Nivel" + nNivel + "'>");
                    sb.append("<span class='nombreLinea glyphicon-plus'></span>");
                    sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>" + sTipo + "</i></span>");
                    sb.append("<span aria-hidden='true'>" + fila.t334_desfase.replace(/"/g, "'") + "</span>");
                    sb.append("<span class='sr-only' role='button' aria-expanded='false'>Fase - " + accounting.formatNumber(fila.t334_idfase,0) + fila.t334_desfase.replace(/"/g, "'") + " (Nivel  " + nNivel + ")</span>");
                    sb.append("</td>");
                    break;

                case "A":
                    sb.append("id='A" + fila.t335_idactividad + "' data-tipo='A' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                    sb.append("<td headers='A" + fila.t335_idactividad + "' class='Nivel" + nNivel + "'>");
                    sb.append("<span class='nombreLinea glyphicon-plus'></span>");
                    sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>" + sTipo + "</i></span>");
                    sb.append("<span aria-hidden='true'>" + fila.t335_desactividad.replace(/"/g, "'") + "</span>");
                    sb.append("<span class='sr-only' role='button' aria-expanded='false'>Actividad - " + accounting.formatNumber(fila.t335_idactividad,0) + fila.t335_desactividad.replace(/"/g, "'") + " (Nivel  " + nNivel + ")</span>");
                    sb.append("</td>");
                    break;

                case "T":
                    sb.append("id='T" + fila.t332_idtarea + "' data-tipo='T' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                    sb.append("<td headers='T" + fila.t332_idtarea + "' class='Nivel" + nNivel + "'>");
                    sb.append("<span class='nombreLinea glyphicon-plus'></span>");
                    sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>" + sTipo + "</i></span>");
                    sb.append("<span aria-hidden='true'>" + accounting.formatNumber(fila.t332_idtarea,0) + " - " + fila.t332_destarea.replace(/"/g, "'") + "</span>");
                    sb.append("<span class='sr-only' role='button' aria-expanded='false'>Tarea - " + accounting.formatNumber(fila.t332_idtarea,0) + fila.t332_destarea.replace(/"/g, "'") + " (Nivel  " + nNivel + ")</span>");
                    sb.append("</td>");
                    break;

                case "C":
                    sb.append("id='C" + fila.t332_idtarea + "-" + moment(fila.t337_fecha).format("DD/MM/YYYY") + "' data-tipo='C' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "' ");
                    if (fila.TotalHorasReportadas != null)
                        sb.append("nH='" + accounting.formatNumber(fila.TotalHorasReportadas) + "' ");
                    else
                        sb.append("nH='0' ");

                    if (fila.TotalJornadasReportadas != null)
                        //sb.append("nJ='" + accounting.formatNumber(fila.TotalJornadasReportadas) + "' ");
                        sb.append("nJ='" + fila.TotalJornadasReportadas + "' ");
                    else
                        sb.append("nJ='0' ");

                    sb.append(">");
                    sb.append("<td class='FC-C' headers='C" + fila.t332_idtarea + "' class='nombreLinea Nivel" + nNivel + "'>");
                    var sFecha = fila.t337_fecha;
                    if (sFecha != "") sFecha = moment(fila.t337_fecha).format("DD/MM/YYYY");
                    sb.append("<span>" + sFecha + "&nbsp;&nbsp;&nbsp;" + fila.Comentarios.replace(/"/g, "'") + "</span>");  //Fechas de Consumo y Comentarios
                    sb.append("</td>");
                    break;
            }

            if (sTipo == "C") {
                var nHR = 0;
                if (fila.TotalHorasReportadas != null) {
                    nHR = accounting.formatNumber(fila.TotalHorasReportadas);
                }
                sb.append("<td style='text-align:right;padding-right:5px;'>");
                sb.append(nHR);// TotalHorasReportadas
                sb.append("</td>");

                var nJR = 0;
                if (fila.TotalJornadasReportadas != null) {
                    nJR = accounting.formatNumber(fila.TotalJornadasReportadas);
                    //sb.append("<td title='" + nJR + "' style='text-align:right;padding-right:5px;'>");
                    sb.append("<td title='" + fila.TotalJornadasReportadas + "' style='text-align:right;padding-right:5px;'>");
                }
                else sb.append("<td title='0' style='text-align:right;padding-right:5px;'>");
                sb.append(nJR);// TotalJornadasReportadas
                sb.append("</td>");

            }
            else
                sb.append("<td style='text-align:right;padding-right:5px;'></td><td style='text-align:right;padding-right:5px;'>");

            sb.append("</tr>");
            return sb;
        }

        function CrearProyEco(fila) {
            var sResul = CrearFila(fila, 1, "PE");
            sResul += CrearFila(fila, 2, "PT");
            if (fila.t334_idfase != null) {
                sResul += CrearFila(fila, 3, "F");
                if (fila.t335_idactividad != null) {
                    sResul += CrearFila(fila, 4, "A");
                    sResul += CrearFila(fila, 5, "T");
                    sResul += CrearFila(fila, 6, "C");
                }
            }
            else {
                if (fila.t335_idactividad != null) {
                    sResul += CrearFila(fila, 3, "A");
                    sResul += CrearFila(fila, 4, "T");
                    sResul += CrearFila(fila, 5, "C");
                }
                else {
                    sResul += CrearFila(fila, 3, "T");
                    sResul += CrearFila(fila, 4, "C");
                }
            }
            return sResul;
        }


        function CrearProyTec(fila) {
            var sResul = CrearFila(fila, 2, "PT");
            if (fila.t334_idfase != null) {
                sResul += CrearFila(fila, 3, "F");
                if (fila.t335_idactividad != null) {
                    sResul += CrearFila(fila, 4, "A");
                    sResul += CrearFila(fila, 5, "T");
                    sResul += CrearFila(fila, 6, "C");
                }
            }
            else {
                if (fila.t335_idactividad != null) {
                    sResul += CrearFila(fila, 3, "A");
                    sResul += CrearFila(fila, 4, "T");
                    sResul += CrearFila(fila, 5, "C");
                }
                else {
                    sResul += CrearFila(fila, 3, "T");
                    sResul += CrearFila(fila, 4, "C");
                }
            }
            return sResul;
        }


        function CrearFase(fila) {
            var sResul = CrearFila(fila, 3, "F");
            sResul += CrearFila(fila, 4, "A");
            sResul += CrearFila(fila, 5, "T");
            sResul += CrearFila(fila, 6, "C");
            return sResul;
        }

        function CrearActividad(fila, nNivel) {
            var sResul = CrearFila(fila, nNivel, "A");
            sResul += CrearFila(fila, nNivel + 1, "T");
            sResul += CrearFila(fila, nNivel + 2, "C");
            return sResul;
        }

        function CrearTarea(fila, nNivel) {
            var sResul = CrearFila(fila, nNivel, "T");
            sResul += CrearFila(fila, nNivel + 1, "C");
            return sResul;
        }

        function CrearConsumo(fila, nNivel) {
            var sResul = CrearFila(fila, nNivel, "C");
            return sResul;
        }

    //Función de obtención del data-parent de un fila
        var obtenerPadre = function (tipo, fila) {

            var padre;

            switch (tipo) {
                case "PT":
                    padre = "PSN" + fila.t301_idproyecto;
                    break;
                case "F"://Activo
                    padre = "PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    break;
                case "A":
                    if (fila.t334_idfase == null) {
                        padre = "PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    } else {
                        padre = "F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    }
                    break;
                case "T":
                    if (fila.t335_idactividad == null) {
                        padre = "PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    } else {
                        if (fila.t334_idfase == null) {
                            padre = "A" + fila.t335_idactividad + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                        } else {
                            padre = "A" + fila.t335_idactividad + " F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                        }
                    }
                    break;

                case "C":
                    if (fila.t335_idactividad == null) {
                        padre = "T" + fila.t332_idtarea + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    } else {
                        if (fila.t334_idfase == null) {
                            padre = "T" + fila.t332_idtarea + " A" + fila.t335_idactividad + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                        } else {
                            padre = "T" + fila.t332_idtarea + " A" + fila.t335_idactividad + " F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                        }
                    }
                    break;
            }

            return padre;

        }

        function borrarCatalogo() {
            dom.tablaContenido.html("");

            dom.totalHorasReportadas.attr('title', '');
            dom.totalHorasReportadas.val("0,00");
            dom.totalJornadasReportadas.attr('title', '');
            dom.totalJornadasReportadas.val("0,00");
        }


        var lineas = {

            //Lineas de proyectos económicos
            PE: function () {
                //return $('#bodyTabla tr[data-level="1"]').find('span[aria-expanded="false"]').parent();
                return $('#bodyTabla tr[data-tipo="PSN"]').children();
            },

            //Lineas de proyectos técnicos
            PT: function () {
                return $('#bodyTabla tr[data-tipo="PT"]').children();

            },
            //Lineas que no sean Tarea, pie de tabla o línea de otros consumos
            padresTLineas: function () {

                //return $('#bodyTabla tr:not([data-tipo="T"], [data-level="0"], [data-loaded="1"])').find('span[aria-expanded="false"]').parent();;
                return $('#bodyTabla tr:not([data-tipo="T"], [data-level="0"])').children();

            },

            //Lineas que no sean pie de tabla o línea de otros consumos
            TLineas: function () {

                //return $('#bodyTabla tr:not([data-tipo="T"], [data-level="0"], [data-loaded="1"])').find('span[aria-expanded="false"]').parent();;
                return $('#bodyTabla tr:not([data-level="0"])').children();

            },

            //Linea activa y sus lineas descendientes
            padresDesLineaActiva: function () {
                return $("#bodyTabla tr.activa, #bodyTabla [data-parent*='" + $("#bodyTabla tr.activa").attr("id") + "']").children();
            },

            //Lineas descendientes de la línea activa
            lineaDesActiva: function () {

                return $("#bodyTabla [data-parent*='" + $("#bodyTabla tr.activa").attr("id") + "']").children();
                //return $("#bodyTabla tr.activa:not([data-tipo='T']").children();

            },

            //Línea activa
            lineaActiva: function () {

                return $("#bodyTabla tr.activa");

            }

        }
        var niveles = {
            //Lineas de proyectos económicos
            //N1: function () {
            //    return $('#bodyTabla tr').filter(function () {
            //        //return $('#bodyTabla tr[data-tipo="PSN"], #bodyTabla [data-tipo="PT"]');
            //        var tipo = $(this).attr("data-tipo");
            //        if (tipo == "PSN") return $(this);
            //    }).children();
            //},
            //Lineas de proyectos técnicos
            N2: function () {
                return $('#bodyTabla tr').filter(function () {
                    //return $('#bodyTabla tr[data-tipo="PSN"], #bodyTabla [data-tipo="PT"]');
                    var tipo = $(this).attr("data-tipo");
                    if (tipo == "PSN" || tipo == "PT") return $(this);
                }).children();
            },
            N3: function () {
                return $('#bodyTabla tr').filter(function () {

                    //return $('#bodyTabla tr[data-tipo="PT"], #bodyTabla tr[data-tipo="F"], #bodyTabla tr[data-tipo="A"], #bodyTabla tr[data-tipo="T"]');
                    var tipo = $(this).attr("data-tipo");
                    if (tipo == "PT"||tipo=="F"||tipo=="A"||tipo=="T")  return $(this);
                }).children();
            },
            N4: function () {
                //return $('#bodyTabla tr').filter(function () {
                //    var tipo = $(this).attr("data-tipo");
                //    if (tipo == "PSN" || tipo == "PT" || tipo == "F" || tipo == "A" || tipo == "T" || tipo == "C") return $(this);
                //}).children();
                return $('#bodyTabla tr').children();
            }
        }
    //Marcado de línea activa
        var marcarLinea = function (thisObj) {

            //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
            desmarcarLinea();
            $(thisObj).addClass('activa');

            //Eliminamos el texto ' - Seleccionado' del elemento seleccionado anterior. Las posiciones 0 y 1 contienen el tipo de linea y su descripción.
            $('span:contains("- Seleccionado")').each(function () {
                //$(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1]);
                if ($(this).text().split(" - ")[1]=="Seleccionado")  $(this).text($(this).text().split(" - ")[0]);
                else $(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1]);
            })

            //Añadimos el texo '- Seleccionado' al elemento seleccionado actualmente

            $(thisObj).children().children(":nth-child(4n)").text($(thisObj).children().children(":nth-child(4n)").text() + ' - Seleccionado');
        }

    //Desmarcado de línea activa
        var desmarcarLinea = function () {

            $("#bodyTabla tr.activa").removeClass('activa');
        }

    //Comprobación de apertura o cierre de linea. True para abrir y False para cerrar
        var abrir = function (thisObj) {

            if ($(thisObj).children().hasClass('glyphicon-plus')) return true;
            return false;

        }

    //Apertura de lineas
        var abrirLinea = function (thisObj, proceso) {

            switch (proceso) {
                case 1:
                    var id = $(thisObj).parent().attr('id')
                    //Abre los hijos del nodo pulsado
                    $("#bodyTabla tr[data-parent^='" + id + "']").show();
                    actualizarLineaAbierta(thisObj)
                    break;
                case 2:
                    $(thisObj.parent()).show();
                    actualizarLineaAbierta(lineas.PT());
                    break;
                case 3:
                    $(thisObj.parent()).show();
                    actualizarLineaAbierta(lineas.padresTLineas())
                    break;
                case 4:
                    $(thisObj.parent()).show();
                    actualizarLineaAbierta(lineas.padresDesLineaActiva())
                    break;
                case 5:
                    $(thisObj.parent()).show();
                    actualizarLineaAbierta(lineas.padresDesLineaActiva())
                    break;
            }

        }
        //Apertura de niveles
        var mostrarNivel = function (thisObj, nivel) {

            switch (nivel) {
                case 2:
                    $(thisObj.parent()).show();
                    break;
                case 3:
                    $(thisObj.parent()).show();
                    break;
                case 4:
                    $(thisObj.parent()).show();
                    break;
                case 5:
                    $(thisObj.parent()).show();
                    breakÇ
                case 6:
                    $(thisObj.parent()).show();
                    break
            }
            actualizarNivelExp(thisObj.parent(),nivel);
        }
    //Actualiza los valores del nivel

        var actualizarNivelExp = function (thisObj,nivel) {
            $(thisObj).each(function () {
                if ($(this).attr('data-level') < nivel)   
                {
                    $(this).find("span[aria-expanded]").attr('aria-expanded', 'true');
                    $(this).children().find(".glyphicon-plus").addClass("glyphicon-minus").removeClass("glyphicon-plus");
                }
            })
            cebrear();
        }

        //Actualiza los valores de la línea recién abierta
        var actualizarLineaAbierta = function (thisObj) {

            //Modifica el atributo de expandido de la línea pulsada
            $(thisObj).find("span[aria-expanded]").attr('aria-expanded', 'true');

            //Cambia el glyphicon
            $(thisObj).find(".glyphicon-plus").toggleClass("glyphicon-minus", true).removeClass("glyphicon-plus");

            cebrear();
        }

    //Cierre de lineas del arbol de la tabla
        var cerrarLinea = function (thisObj, proceso) {

            var id = $(thisObj).parent().attr('id')

            //Cierra los descendientes del nodo pulsado
            $("#bodyTabla tr[data-parent*='" + id + "']").hide();

            //Modifica el atributo de expandido de la línea pulsada
            $(thisObj).find("span[aria-expanded]").attr('aria-expanded', 'false');

            //Modifica el atributo de expandido de los descendientes del nodo pulsado
            $("#bodyTabla tr[data-parent*='" + id + "']").find("span[aria-expanded]").attr('aria-expanded', 'false');

            //Cambia el glyphicon de los descendientes del nodo pulsado y de sí mismo
            $("#bodyTabla tr[data-parent*='" + id + "']").find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
            $(thisObj).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");

            cebrear();
        }
    // Colorear niveles
        var colorearNiveles = function (e) {

            var srcobj;
            srcobj = e.target ? e.target : e.srcElement;

            //El elemento target no es el mismo si se pulsa el el botón o el span que hay dentro del botón, por lo que se comprueba y se coge siempre el botón
            if (!$(srcobj).is(":button")) srcobj = $(srcobj).parent();

            var nivel = $(srcobj).attr("data-level");
            if ($("#bodyTabla tr").length == 0) nivel = 1;
            colorearNivel(nivel);
        }
        function colorearNivel(nivel){
            switch(nivel){
                case "1":
                    $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra2").addClass("nivelGris").removeClass("nivelVerde");
                    $("#barra3").addClass("nivelGris").removeClass("nivelVerde");
                    $("#barra4").addClass("nivelGris").removeClass("nivelVerde");
                    break;
                case "2":
                    $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra2").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra3").addClass("nivelGris").removeClass("nivelVerde");
                    $("#barra4").addClass("nivelGris").removeClass("nivelVerde");
                    break;
                case "3":
                    $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra2").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra3").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra4").addClass("nivelGris").removeClass("nivelVerde");
                    break;
                case "4":
                    $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra2").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra3").addClass("nivelVerde").removeClass("nivelGris");
                    $("#barra4").addClass("nivelVerde").removeClass("nivelGris");
                    break;
            }
        }
    //Cierre del nivel (de momento igual)

        var cerrarNivel = function (e) {

            var srcobj;
            srcobj = e.target ? e.target : e.srcElement;

            //El elemento target no es el mismo si se pulsa el el botón o el span que hay dentro del botón, por lo que se comprueba y se coge siempre el botón
            if (!$(srcobj).is(":button")) srcobj = $(srcobj).parent();

            var nivel = $(srcobj).attr("data-level");

            $('#bodyTabla tr').filter(function () {
                return $(this).attr("data-level") > nivel;
            }).hide();

            //Modifica el atributo de expandido de los nodos cerrados
            $('#bodyTabla tr').filter(function () {
                return $(this).attr("data-level") >= nivel;
            }).find('span[aria-expanded]').attr('aria-expanded', 'false');

            //Cambia el glyphicon de los nodos cerrados
            $('#bodyTabla tr').filter(function () {
                return $(this).attr("data-level") >= nivel;
            }).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");

            desmarcarLinea();

            //Eliminamos el texto ' - Seleccionado' del elemento seleccionado anterior. Las posiciones 0 y 1 contienen el tipo de linea y su descripción.
            $('span:contains("- Seleccionado")').each(function () {
                $(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1]);
            });

            cebrear();

        }
        function calcularAcumulados() {
            try {
                var nTH = 0, nTJ = 0;
                var nAH = 0, nAJ = 0;
                var nFH = 0, nFJ = 0;
                var nPTH = 0, nPTJ = 0;
                var nPEH = 0, nPEJ = 0;
                var nTotH = 0;
                var nTotJ = 0;
                var nH = 0, nJ = 0;

                $($('#bodyTabla tr').get().reverse()).each(function () {
                    if ($(this).attr('data-tipo') == "C") { // Consumo

                        nH = parseFloat(accounting.unformat($(this).attr('nH'), ","));
                        nJ = parseFloat(accounting.unformat($(this).attr('nJ'), "."));

                        nTH += nH;
                        nTJ += nJ;
                        if ($(this).attr('data-level') >= 5) {
                            nAH += nH;
                            nAJ += nJ;
                        }
                        if ($(this).attr('data-level') == 6) {
                            nFH += nH;
                            nFJ += nJ;
                        }
                        nPTH += nH;
                        nPTJ += nJ;
                        nPEH += nH;
                        nPEJ += nJ;
                        nTotH += nH;
                        nTotJ += nJ;
                    }

                    else if ($(this).attr('data-tipo') == "T") {//Tarea
                        $(this).children().eq(1).attr("title", nTH);
                        $(this).children().eq(2).attr("title", nTJ);

                        $(this).children().eq(1).text(accounting.formatNumber(nTH));
                        $(this).children().eq(2).text(accounting.formatNumber(nTJ));

                        nTH = 0; nTJ = 0;

                    } else if ($(this).attr('data-tipo') == "A") {//ACTIVIDAD
                        $(this).children().eq(1).attr("title", nAH);
                        $(this).children().eq(2).attr("title", nAJ);

                        $(this).children().eq(1).text(accounting.formatNumber(nAH));
                        $(this).children().eq(2).text(accounting.formatNumber(nAJ));

                        nTH = 0; nTJ = 0;
                        nAH = 0; nAJ = 0;

                    } else if ($(this).attr('data-tipo') == "F") {//FASE
                        $(this).children().eq(1).text(nFH);
                        $(this).children().eq(2).text(nFJ);

                        $(this).children().eq(1).text(accounting.formatNumber(nFH));
                        $(this).children().eq(2).text(accounting.formatNumber(nFJ));

                        nTH = 0; nTJ = 0;
                        nAH = 0; nAJ = 0;
                        nFH = 0; nFJ = 0;

                    } else if ($(this).attr('data-tipo') == "PT") {//PT
                        $(this).children().eq(1).attr("title", nPTH);
                        $(this).children().eq(2).attr("title", nPTJ);

                        $(this).children().eq(1).text(accounting.formatNumber(nPTH));
                        $(this).children().eq(2).text(accounting.formatNumber(nPTJ));

                        nTH = 0; nTJ = 0;
                        nAH = 0; nAJ = 0;
                        nFH = 0; nFJ = 0;
                        nPTH = 0; nPTJ = 0;

                    } else if ($(this).attr('data-tipo') == "PSN") {//PE
                        $(this).children().eq(1).attr("title", nPEH);
                        $(this).children().eq(2).attr("title", nPEJ);

                        $(this).children().eq(1).text(accounting.formatNumber(nPEH));
                        $(this).children().eq(2).text(accounting.formatNumber(nPEJ));

                        nTH = 0; nTJ = 0;
                        nAH = 0; nAJ = 0;
                        nFH = 0; nFJ = 0;
                        nPTH = 0; nPTJ = 0;
                        nPEH = 0; nPEJ = 0;
                    }
                });

                $("#PieHoras").attr("title", nTotH);
                $("#PieHoras").text(accounting.formatNumber(nTotH));
                $("#PieJornadas").text(accounting.formatNumber(nTotJ));

            } catch (e) {
                IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error calculando acumulados");
            }

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
            rellenarComboProyectos: rellenarComboProyectos,
            pintarTablaDatos: pintarTablaDatos,
            borrarCatalogo: borrarCatalogo,
            lineas: lineas,
            niveles: niveles,
            abrir: abrir,
            marcarLinea: marcarLinea,
            desmarcarLinea: desmarcarLinea,
            abrirLinea: abrirLinea,
            mostrarNivel:mostrarNivel,
            cerrarLinea: cerrarLinea,
            cerrarNivel: cerrarNivel,
            colorearNiveles: colorearNiveles,
            redimensionPantalla: redimensionPantalla,
            visualizarContenedor: visualizarContenedor
        }
})();