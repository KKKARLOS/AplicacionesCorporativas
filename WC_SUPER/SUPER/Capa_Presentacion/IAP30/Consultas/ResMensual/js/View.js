var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ResMensual = SUPER.IAP30.ResMensual || {}


SUPER.IAP30.ResMensual.View = (function () {


    var dom = {
        fechaDesde: $('#txtInicio'),
        fechaHasta: $('#txtFin'),
        ano: $('#txtAno'),
        anoMes: $('#txtAnoMes'),
        mes: $('#txtMes'),
        grpTxtInicio: $('#grpTxtInicio'),
        grpTxtFin: $('#grpTxtFin'),
        grpAno: $('#grpAno'),
        grpAnoMes: $('#grpAnoMes'),
        grpMes: $('#grpMes'),
        opcionSel: $('#selOpc'),
        divFlechas: $('#divFlechas'),
        flechaIzda: $('#flechaIzda'),
        flechaDcha: $('#flechaDcha'),
        tablaTotal: $("#bodyTablaTotal"),
        tablaDetalle: $("#bodyTablaDetalle"),
        Imprimir: $("#Imprimir"),
        indicadorRedimension: $('#indicator'),
        ventana: $(window)
    }

    var selectores = {
        sel_datepickers: "input.hasDatepicker",
        container: ".container"
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {

        asignarControlDatepicker();

    }    

    asignarControlDatepicker = function () {

        destruirOcultarDatepickers();

        var months = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];

        switch (parseInt(dom.opcionSel.val(), 10)) {
            case 0:

                dom.grpTxtInicio.show();
                dom.grpTxtFin.show();
                
                dom.fechaDesde.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'MM yy',
                    showButtonPanel: true,
                    closeText: "Aceptar",
                    monthNames: months,
                    onClose: function () {
                        var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                        var sMonth = "";
                        var iMes = parseInt(iMonth, 10) + 1;
                        if (iMes < 10) sMonth = '0' + iMes.toString();
                        else sMonth = iMes.toString();

                        var iYear = $("#ui-datepicker-div .ui-datepicker-year :selected").val();

                        var sFinicioOld = dom.fechaDesde.val();
                        IB.vars.fechaInicioOld = sFinicioOld;

                        $(this).datepicker('setDate', new Date(iYear, iMonth, 1));
                        IB.vars.fechaDesde = iYear.toString() + sMonth;
                        if (sFinicioOld != dom.fechaDesde.val()) $(this).change(); //fuerzo la modificación del input para que salte el evento onchange sino no lo hace
                        //$('#txtInicio').removeClass('calendar-on').addClass('calendar-off');
                        //obtenerDatos(); //Se enlaza el evento onchange en app
                        $(this).removeClass('calendar-on').addClass('calendar-off');
                    },

                    beforeShow: function () {
                        if ((selDate = $(this).val()).length > 0) {
                            iYear = selDate.substring(selDate.length - 4, selDate.length);
                            iMonth = jQuery.inArray(selDate.substring(0, selDate.length - 5),
                                     $(this).datepicker('option', 'monthNames'));
                            $(this).datepicker('option', 'defaultDate', new Date(iYear, iMonth, 1));
                            $(this).datepicker('setDate', new Date(iYear, iMonth, 1));
                        }
                        //$('#txtInicio').removeClass('calendar-off').addClass('calendar-on');
                        $(this).removeClass('calendar-off').addClass('calendar-on');
                    }
                });

                dom.fechaHasta.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'MM yy',
                    monthNames: months,
                    showButtonPanel: true,
                    closeText : "Aceptar",
                    onClose: function () {
                        var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                        var iYear = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                        var sFFinOld = dom.fechaHasta.val();
                        IB.vars.fechaFinOld = sFFinOld;

                        $(this).datepicker('setDate', new Date(iYear, iMonth, 1));

                        var sMonth = "";
                        var iMes = parseInt(iMonth, 10) + 1;
                        if (iMes < 10) sMonth = '0' + iMes.toString();
                        else sMonth = iMes.toString();

                        IB.vars.fechaHasta = iYear.toString() + sMonth;

                        if (sFFinOld != dom.fechaHasta.val()) $(this).change(); //fuerzo la modificación del input para que salte el evento onchange sino no lo hace
                        $(this).removeClass('calendar-on').addClass('calendar-off');
                    },

                    beforeShow: function () {
                        if ((selDate = $(this).val()).length > 0) {
                            iYear = selDate.substring(selDate.length - 4, selDate.length);
                            iMonth = jQuery.inArray(selDate.substring(0, selDate.length - 5),
                                     $(this).datepicker('option', 'monthNames'));
                            $(this).datepicker('option', 'defaultDate', new Date(iYear, iMonth, 1));
                            $(this).datepicker('setDate', new Date(iYear, iMonth, 1));
                        }
                        $(this).removeClass('calendar-off').addClass('calendar-on');
                    }
                });

                break;
            case 1:

                dom.grpAno.show();                

                dom.ano.datepicker({
                    changeMonth: false,
                    changeYear: true,
                    showButtonPanel: true,
                    yearRange: '1990:2078',
                    dateFormat: 'yy',
                    closeText: "Aceptar",
                    stepMonths: 12,
                    monthNames: ["", "", "", "", "", "", "", "", "", "", "", ""],
                    onClose: function (dateText, inst) {
                        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                        var sAnoOld = dom.ano.val();

                        $(this).datepicker('setDate', new Date(year, 0, 1));
                        IB.vars.fechaDesde = year.toString() + "01";
                        IB.vars.fechaHasta = year.toString() + "12";

                        if (sAnoOld != dom.ano.val()) $(this).change(); //fuerzo la modificación del input para que salte el evento onchange sino no lo hace
                        $(this).removeClass('calendar-on').addClass('calendar-off');

                    },
                    beforeShow: function () {
                        var year = parseInt($(this).val());
                        $(this).removeClass('calendar-off').addClass('calendar-on');
                        $(this).datepicker('option', 'defaultDate', new Date(year, 0, 1));
                        $(this).datepicker('setDate', new Date(year, 0, 1));
                    }
                });

                break;

            case 2:

                dom.grpAnoMes.show();

                dom.anoMes.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'MM yy',
                    monthNames: months,
                    showButtonPanel: true,
                    closeText : "Aceptar",
                    onClose: function () {
                        var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                        var iYear = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                        var sFOld = dom.anoMes.val();

                        $(this).datepicker('setDate', new Date(iYear, iMonth, 1));

                        var sMonth = "";
                        var iMes = parseInt(iMonth, 10) + 1;
                        if (iMes < 10) sMonth = '0' + iMes.toString();
                        else sMonth = iMes.toString();

                        IB.vars.fechaDesde = iYear.toString() + sMonth;
                        IB.vars.fechaHasta = iYear.toString() + sMonth;

                        if (sFOld != dom.anoMes.val()) $(this).change(); //fuerzo la modificación del input para que salte el evento onchange sino no lo hace
                        $(this).removeClass('calendar-on').addClass('calendar-off');
                    },

                    beforeShow: function () {
                        if ((selDate = $(this).val()).length > 0) {
                            iYear = selDate.substring(selDate.length - 4, selDate.length);
                            iMonth = jQuery.inArray(selDate.substring(0, selDate.length - 5),
                                     $(this).datepicker('option', 'monthNames'));
                            $(this).datepicker('option', 'defaultDate', new Date(iYear, iMonth, 1));
                            $(this).datepicker('setDate', new Date(iYear, iMonth, 1));
                        }
                        $(this).removeClass('calendar-off').addClass('calendar-on');
                    }
                });

                break;

            case 3:

                dom.grpMes.show();                

                dom.mes.datepicker({
                    changeMonth: true,
                    changeYear: false,
                    showButtonPanel: true,
                    yearRange: IB.vars.ano + ':' + IB.vars.ano,
                    dateFormat: 'mm',
                    closeText: "Aceptar",
                    stepMonths: 1,
                    monthNames: months,
                    onClose: function (dateText, inst) {
                        var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                        var year = IB.vars.ano;
                        var sMesOld = dom.mes.val();

                        var sMonth = "";
                        var iMes = parseInt(iMonth, 10) + 1;
                        if (iMes < 10) sMonth = '0' + iMes.toString();
                        else sMonth = iMes.toString();

                        //$(this).datepicker('setDate', new Date(year, iMonth, 1));
                        $(this).val(months[parseInt($("#ui-datepicker-div .ui-datepicker-month :selected").val())]);
                        IB.vars.fechaHasta = IB.vars.ano + sMonth;

                        if (sMesOld != dom.mes.val()) $(this).change(); //fuerzo la modificación del input para que salte el evento onchange sino no lo hace
                        $(this).removeClass('calendar-on').addClass('calendar-off');

                    },
                    beforeShow: function () {
                        var month = $(this).val();
                        var iMonth = jQuery.inArray(month, $(this).datepicker('option', 'monthNames'));
                        $(this).removeClass('calendar-off').addClass('calendar-on');                        
                        $(this).datepicker('setDate', new Date(IB.vars.ano, iMonth, 1));
                        $(this).datepicker('option', 'defaultDate', new Date(IB.vars.ano, iMonth, 1));
                        $(this).val(months[parseInt(iMonth)]);
                    }
                });

                break;
        }
        

    }

    var destruirOcultarDatepickers = function () {

        //Se destruyen los datepickers
        dom.fechaDesde.datepicker("destroy");
        dom.fechaDesde.removeClass("hasDatepicker");

        dom.fechaHasta.datepicker("destroy");
        dom.fechaHasta.removeClass("hasDatepicker");

        dom.ano.datepicker("destroy");
        dom.ano.removeClass("hasDatepicker");

        dom.anoMes.datepicker("destroy");
        dom.anoMes.removeClass("hasDatepicker");

        dom.mes.datepicker("destroy");
        dom.mes.removeClass("hasDatepicker");

        dom.grpTxtInicio.hide();
        dom.grpTxtFin.hide();
        dom.grpAno.hide();
        dom.grpAnoMes.hide();
        dom.grpMes.hide();

    }

    function pintarTablaDatos(data) {
        var tblTotal = "";
        var sImporte = "";

        var columnas = [
                { "data": "AnnoMesText", "width": "50%" },
                { "data": "Horas", "sClass": "text-right", "width": "50%" }
        ];

        var table = $('#tablaTotal').DataTable({
            destroy: true,
            "columns": columnas,
            data: data,
            searching: false,
            scrollY: "59vh",
            scrollX: false,
            bScrollCollapse: true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: false,
            columnDefs: [
                            {
                                "targets": 1,
                                "render": {
                                    "display": function (data, type, row, meta) {
                                            if (row.Horas == 0) {
                                                return '';
                                            } else {
                                                return accounting.formatNumber(row.Horas);
                                            }
                                     }
                                }
                            }
                        ],
        });
    }


    function pintarTablaDatosDetalle(data) {
        var tblDetalle = "";
        var sImporte = "";

        var columnas = [
                {
                    "data": "Fecha", "width": "33%", render: function (data) {
                        return moment(data).format('DD/MM/YYYY')
                    }
                },
                { "data": "DiaSemana", "width": "33%" },
                { "data": "Horas", "width": "33%", "sClass": "text-right" }
        ];

        var table = $('#tablaDetalle').DataTable({
            destroy: true,
            "columns": columnas,
            data: data,
            searching: false,
            scrollY: "59vh",
            scrollX: true,
            bScrollCollapse: true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: false,
            columnDefs: [
                            {
                                 "targets": 2,
                                 "render": {
                                     "display": function (data, type, row, meta) {
                                         if (row.Horas == 0) {
                                             return '';
                                         } else {
                                             return accounting.formatNumber(row.Horas);
                                         }
                                     }
                                 }
                             }
                        ],
        });

    }

    Imprimir = function (e) {
        $("#hdnIDS").val(IB.vars.codUsu);
        $("#hdnAnoMesDesde").val(IB.vars.fechaDesde);
        $("#hdnAnoMesHasta").val(IB.vars.fechaHasta);
        $("#FORMATO").val("PDF");

        //*SSRS

        var params = {
            reportName: "/SUPER/sup_impu_men",
            tipo: "PDF",
            Profesionales: IB.vars.codUsu,
            nDesde: IB.vars.fechaDesde,
            nHasta: IB.vars.fechaHasta
        }

        PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR
        document.forms["frmDatos"].action = "../../../Administracion/ControlUsuCATP/Exportar/default.aspx";
        document.forms["frmDatos"].target = "_blank";
        document.forms["frmDatos"].submit();
        //CR*/
    }

    var visualizarContainer = function () {

        $(selectores.container).css("visibility", "visible");

    }    

    var ocultarContainer = function () {

        $(selectores.container).css("visibility", "hidden");

    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        pintarTablaDatos: pintarTablaDatos,
        pintarTablaDatosDetalle: pintarTablaDatosDetalle,
        asignarControlDatepicker: asignarControlDatepicker,
        visualizarContainer: visualizarContainer,
        ocultarContainer: ocultarContainer
    }
})();