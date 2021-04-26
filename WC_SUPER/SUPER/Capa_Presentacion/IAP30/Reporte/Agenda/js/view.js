var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Agenda = SUPER.IAP30.Agenda || {}


SUPER.IAP30.Agenda.view = (function () {

    var dom = {
        agenda: $('#calendar'),
        detalleAgenda: $('#detalleAgenda'),
        modalComentario: $('#modalComentario'),
        infoOcultable: $('.ocultable'),
        btnVolver: $('.btnVolver'),
        ayudaTarea: $(".fk_ayudaTarea"),
        idEventoCal: $('#idEventoCal'),
        asunto: $('#asunto'),
        dangerAsunto: $('#dangerAsunto'),
        linkTarea: $('#lblTarea'),
        dangerTarea: $('#dangerTarea'),
        tarea: $('#idTarea'),
        desTarea: $('#desTarea'),
        prof: $('#prof'),
        prom: $('#prom'),
        motivo: $('#motivo'),
        observaciones: $('#observaciones'),
        privado: $('#privado'),
        rangoFechas: $('#txtRangoFechas'),
        checkboxes0: $('#checkboxes-0'),
        checkboxes1: $('#checkboxes-1'),
        checkboxes2: $('#checkboxes-2'),
        checkboxes3: $('#checkboxes-3'),
        checkboxes4: $('#checkboxes-4'),
        checkboxes5: $('#checkboxes-5'),
        checkboxes6: $('#checkboxes-6'),
        checkRepeticion: $('input:checkbox[id^="checkbox"]'),
        bloqueProf: $('#datosAsignadoProf :input[type=text]'),
        btnSeleccionar: $('#btnSeleccionar'),
         btnEliminarSeleccionados: $("#btnEliminarSeleccionados"),
        btnDesmarcarEliminados: $("#btnDesmarcarEliminados"),
        btnMarcarDesmarcarOrigen: $("#btnMarcarDesmarcarOrigen"),
        btnMarcarDesmarcarDestino: $("#btnMarcarDesmarcarDestino"),
        tablaProfesionales: $("#tbodyProfesionales"),
        tablaProfesionalesSeleccionados: $("#tbodyProfesionalesSel"),
        Apellido1: $("#txtApellido1"),
        Apellido2: $("#txtApellido2"),
        Nombre: $("#txtNombre"),
        buscAsignados: $("#buscAsignados"),
        btnCancelar: $('#btnCancelar'),
        btnCierreModalEvento: $('#btnCierreModalEvento'),
        btnAceptar: $('#btnAceptar'),
        btnAceptarComentario: $('#btnAceptarComentario'),
        txtComentario: $('#txtComentario'),
        idEventoEliminar: $('#idEventoEliminar'),
        idEventoAgenda: $('#idEventoAgenda')

    }

    var selectores = {
        container: '.container',
        divProfesionales: "#lisProfesionales",
        divProfesionales2: "#lisProfesionales2",
        filasProfesionales: "#tbodyProfesionales > tr",
        filasProfesionalesSel: "#tbodyProfesionalesSel > tr",
        filasSeleccionadasProfesionales: "#tbodyProfesionales .selected",
        filasSeleccionadasProfesionalesSel: "#tbodyProfesionalesSel .selected",
        buscarProfesionales: ".criterios :input[type=text]",
        buscarCadenaProfesionalesSel: "#tbodyProfesionalesSel > tr > td:first-child"    ,
        sel_inputs: "input:not(.inputCriterios)",
        sel_textarea: "textarea"
    
    }

    var indicadores = {
        i_dispositivoTactil: false
    }

    var init = function () {
        //asignarControlDateRangePicker();
    };

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function aperturaModal(element) {
        element.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        element.modal('show');
        dom.infoOcultable.attr('area-hidden', true);
    }

    var controlCamposObligatorios = function (e) {
        if (dom.asunto.val() != "") dom.dangerTarea.hide();
        else {
            dom.dangerTarea.show();
            if (dom.tarea.val() != "") dom.dangerAsunto.hide();
            else dom.dangerAsunto.show();
        }
    }

    function cierreModal(element) {
        borrarCamposDetalle();
        dom.infoOcultable.attr('aria-hidden', 'false');
        $(dom.linkTarea).off('click');
        $(dom.tarea).off('keypress');
        $(dom.tarea).off('focusout');
        $(dom.btnCancelar).off('click');
        $(dom.btnCierreModalEvento).off('click');
        $(dom.btnAceptar).off('click');
        //Control de cambios//Evento de cambios en los campos
        $(selectores.sel_inputs).off("keypress");
        $(selectores.sel_inputs).off("change");
        $(selectores.sel_textarea).off("change");
        //Enlace eliminar seleccionados
        $(dom.btnEliminarSeleccionados).off("click");
        //Desmarcar eliminados
        $(dom.btnDesmarcarEliminados).off("click");
        //Marcar fila profesionales seleccionados
        $(selectores.filasProfesionalesSel).off("click");
        //Marcar fila profesionales a seleccionar
        $(selectores.filasProfesionales).off("click");
        //Marcar-Desmarcar todos los elementos Origen
        $(selectores.btnMarcarDesmarcarOrigen).off("click");
        //Marcar-Desmarcar todos los elementos Destino
        $(selectores.btnMarcarDesmarcarDestino).off("click");
        //Buscar profesionales por criterios de pantalla
        $(selectores.buscarProfesionales).off("keyup");
        //Buscar profesionales asignados
        $(selectores.buscAsignados).off("keyup");
        //Añadir elementos seleccionados a través de botón
        $(selectores.btnSeleccionar).off("click");
        //Añadir elementos seleccionados a través de doble click
        $(selectores.filasProfesionales).off("dblclick");
        $(dom.dangerAsunto).show();
        $(dom.dangerTarea).show();
    }

    darValor = function (elemento, value) {
        elemento.val(value);
    }

    pintarDatosTarea = function (objTarea) {
        dom.tarea.val(objTarea.t332_idtarea);
        dom.desTarea.val(objTarea.t332_destarea);
    }

    pintarDetalleEvento = function (objEvento) {
        dom.idEventoCal.val(objEvento.idEvento);
        dom.tarea.val(objEvento.IdTarea);
        dom.desTarea.val(objEvento.DesTarea);
        dom.prom.val(objEvento.Promotor);
        dom.prof.val(objEvento.Profesional);
        dom.asunto.val(objEvento.Asunto);
        dom.motivo.val(objEvento.Motivo);
        dom.observaciones.val(objEvento.Observaciones);
        dom.privado.val(objEvento.Privado);
        //asignarControlDateRangePicker(moment(objEvento.StartTime).format('DD/MM/YYYY h:mm A'), moment(moment(objEvento.EndTime)).format('DD/MM/YYYY h:mm A'));

    }

    borrarCamposDetalle = function () {
        dom.idEventoCal.val("");
        dom.tarea.val("");
        dom.desTarea.val("");
        dom.prom.val("");
        dom.prof.val("");
        dom.asunto.val("");
        dom.motivo.val("");
        dom.observaciones.val("");
        dom.privado.val("");
        dom.checkboxes0.attr('checked', false);
        dom.checkboxes1.attr('checked', false);
        dom.checkboxes2.attr('checked', false);
        dom.checkboxes3.attr('checked', false);
        dom.checkboxes4.attr('checked', false);
        dom.checkboxes5.attr('checked', false);
        dom.checkboxes6.attr('checked', false);
        dom.rangoFechas.off('.datepicker');
        //asignarValorDateRangePicker(moment().format('DD/MM/YYYY h:mm A'), moment().format('DD/MM/YYYY h:mm A'));
        $(selectores.filasProfesionales).remove();
        $(selectores.filasProfesionalesSel).remove();
        dom.Apellido1.val("");
        dom.Apellido2.val("");
        dom.Nombre.val("");
        dom.buscAsignados.val("");
    }   

    limpiarDatosTarea = function (e) {
        dom.desTarea.val("");
    }

    obtenerValor = function (elemento) {
        return elemento.val();
    }

    radioCheckeado = function (elemento) {
        return elemento.is(':checked')
    }

    var habilitarInput = function(elemento){
        elemento.removeAttr("disabled");
    }

    var desHabilitarInput = function (elemento) {
        elemento.attr('disabled', 'disabled');
    }

    var visualizarContenedor = function () {
        $(selectores.container).css("visibility", "visible");
    }

    $(window).resize(function () {
        controlarScroll();
    });

    var asignarControlDateRangePicker = function (fDesde, fHasta) {
        var fDesde, fHasta;

        if (fDesde == "" || fDesde == undefined) {
            fDesde = moment().format('DD/MM/YYYY h:mm A');
        } 

        if (fHasta == "" || fHasta == undefined) {
            fHasta = moment().format('DD/MM/YYYY h:mm A');
        }

        dom.rangoFechas.daterangepicker({
            locale: {
                format: 'DD/MM/YYYY h:mm A',
                applyLabel: 'Aceptar',
                cancelLabel: 'Cancelar',
            },
            startDate: fDesde,
            endDate: fHasta,
            linkedCalendars: false,
            timePicker: true,
            timePickerIncrement: 30,
            disableHoverDate: true
        });

        dom.rangoFechas.on('show.daterangepicker', function (event, picker) {
            let rightDate = picker.endDate || new Date();
            let rightMoment = moment(rightDate);

            picker.rightCalendar.month = rightMoment;
            picker.renderCalendar('right');
        });
    }

    var asignarValorDateRangePicker = function (fDesde, fHasta) {
        var fDesde, fHasta;

        if (fDesde == "" || fDesde == undefined) {
            fDesde = moment().format('DD/MM/YYYY h:mm A');
        }

        if (fHasta == "" || fHasta == undefined) {
            fHasta = moment().format('DD/MM/YYYY h:mm A');
        }

        dom.rangoFechas.data('daterangepicker').setStartDate(fDesde);
        dom.rangoFechas.data('daterangepicker').setEndDate(fHasta);
    }

    asignarFoco = function (elemento) {
        elemento.focus();
        elemento.select();
    }

    
    function pintarProfesionales(data) {
        var tblProfesionales = "";

        $.each(data, function (index, item) {
            tblProfesionales += "<tr class='linea' data-id='" + item.t314_idusuario + "' data-red='" + item.t001_idficepi + "//" + item.t001_codred + "' data-bd='' data-sexo='" + item.t001_sexo + "' data-tipo='" + item.tipo + "'>";
            tblProfesionales += "<td class='text-left'>" + item.PROFESIONAL + "</td>";
            tblProfesionales += "</tr>";
            //dom.tablaProfesionalesSeleccionados.append(tblDetalle);
        });

        ////Inyectar html en la página
        dom.tablaProfesionales.html(tblProfesionales);
        cebrear();
    }

    function cebrear() {
        $("#detalleAgenda >tr:visible:not(.bg-info)").removeClass("cebreada");
        $('#detalleAgenda >tr:visible:not(.bg-info):even').addClass('cebreada');
        controlarScroll();
    }
   
    function controlarScroll() {
        /*Controlamos si el contenedor tiene Scroll*/

        var div = document.getElementById('tbodyProfesionales');

        var scrollWidth = $('#tbodyProfesionales').width() - div.scrollWidth;

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;

        if (hasVerticalScrollbar) {
            $("#tabCabecera").css("width", "calc( " + $('#tblProfesionales').width() + "px - " + scrollWidth + "px )");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }
        else {
            $("#tabCabecera").css("width", "" + $('#tblProfesionales').width() + "px");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }

        div = document.getElementById('tbodyProfesionalesSel');

        scrollWidth = $('#tbodyProfesionalesSel').width() - div.scrollWidth;

        hasVerticalScrollbar = div.scrollHeight > div.clientHeight;

        if (hasVerticalScrollbar) {
            $("#tabCabeceraSel").css("width", "calc( " + $('#tblProfesionalesSel').width() + "px - " + scrollWidth + "px )");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }
        else {
            $("#tabCabeceraSel").css("width", "" + $('#tblProfesionalesSel').width() + "px");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }
        /*FIN Controlamos si el contenedor tiene Scroll*/
    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        aperturaModal: aperturaModal,
        cierreModal: cierreModal,
        visualizarContenedor: visualizarContenedor,
        asignarControlDateRangePicker: asignarControlDateRangePicker,
        asignarValorDateRangePicker: asignarValorDateRangePicker,
        pintarProfesionales: pintarProfesionales,
        cebrear: cebrear,
        habilitarInput: habilitarInput,
        desHabilitarInput: desHabilitarInput,
        asignarFoco: asignarFoco,
        pintarDetalleEvento: pintarDetalleEvento,
        borrarCamposDetalle: borrarCamposDetalle,
        limpiarDatosTarea: limpiarDatosTarea,
        obtenerValor: obtenerValor,
        pintarDatosTarea: pintarDatosTarea,
        radioCheckeado: radioCheckeado,
        darValor: darValor,
        controlCamposObligatorios: controlCamposObligatorios
    }
})();