var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ImpMasiva = SUPER.IAP30.ImpMasiva || {}


SUPER.IAP30.ImpMasiva.view = (function () {
    var dom = {
        //chevron: $('.chevron_toggleable'),
        title: $('.ibox-title_toggleable'),
        tarea: $('#idTarea'),
        linkTarea: $('#lblTarea'),
        desTarea: $('#desTarea'),
        txtProy: $('#txtProy'),
        txtProyTec: $('#txtProyTec'),
        txtFase: $('#txtFase'),
        txtAct: $('#txtAct'),
        btnGuia: $('#btnGuia'),
        cmbModo: $('#SelModo'),
        chkFestivos: $('#chkFestivos'),
        chkFacturable: $('#chkFacturable'),
        chkFinalizado: $('#chkFinalizado'),
        RegJorNoCompleta: $('#hdnRegJorNoCompleta'),
        ImputarFestivos: $('#hdnImputarFestivos'),
        fechaInicioImpPermitida: $('#hdnfechaInicioImpPermitida'),
        fechaFinImpPermitida: $('#hdnfechaFinImpPermitida'),
        txtHoras: $('#txtHoras'),
        txtHorasDanger: $('#txtHorasDanger'),
        txtComent: $('#txtComent'),
        txtUltRep: $('#txtUltRep'),
        txtFIni: $('#txtFIni'),
        txtFIniH: $('#txtFIniH'),
        txtFIniDanger: $('#txtFIniDanger'),
        txtFFin: $('#txtFFin'),
        txtFEst: $('#txtFEst'),
        txtModoFact: $('#txtModoFact'),
        radioImputaciones: $('input:radio[id^="radio"]'),
        radioImputacionesCheckeado: $('input:radio[id^="radio"]:checked'),
        radio1: $('#radio1'),
        radio2: $('#radio2'),
        radio3: $('#radio3'),
        txtObsv: $('#txtObsv'),
        txtTotEst: $('#txtTotEst'),
        btnGrabar: $('#btnGrabar'),
        PConsumo: $('#PConsumo'),
        PConsumido: $('#PConsumido'),
        pEstimado: $('#pEstimado'),
        UConsumo: $('#UConsumo'),
        UConsumido: $('#UConsumido'),
        ATeorico: $('#ATeorico'),
        txtTotPrev: $('#txtTotPrev'),
        txtFFinPrev: $('#txtFFinPrev'),
        txtParti: $('#txtParti'),
        txtColec: $('#txtColec'),
        ayudaTarea: $(".fk_ayudaTarea"),
        irFieldset: $("#irFieldset"),
        ctFieldset: $("#ctFieldset"),
        container: $('.container')
    }

    var init = function () {
        dom.txtUltRep.val(IB.vars.fechaUltImp);
        dom.container.css("visibility", "visible");
        dom.ctFieldset.height(dom.irFieldset.height());
        IB.procesando.ocultar();
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    $(window).resize(function () {
        dom.ctFieldset.height(dom.irFieldset.height());
    });

    
    collapse = function (e){
        //$(this).toggleClass('fa-chevron-down fa-chevron-up');
        $(this).find('.fa').toggleClass('fa-chevron-down fa-chevron-up');
        $(this).attr('aria-expanded') == "true" ? $(this).attr("aria-expanded","false") : $(this).attr("aria-expanded","true");
    }

    abrirGuia = function (e) {
        //TODO: abrir guia en nueva ventana
    }

    abrirDatePicker = function (e) {
        if ($(this).attr("disabled") == "disabled") $(this).datepicker('disable');
        else $(this).datepicker('enable');
    }   

    selTipoImputacion = function (e) {
        if (this.value == "1") {
            tipoImputacion1();
        } else if (this.value == "2") {
            tipoImputacion2();
        } else tipoImputacion3();
    }


    tipoImputacion1 = function (e) {
        dom.radio1.attr('checked', 'checked');
        dom.radio2.attr('checked', false);
        dom.radio3.attr('checked', false);
        dom.cmbModo.attr('disabled', 'disabled');
        dom.chkFestivos.attr('checked',false);
        dom.chkFestivos.attr('disabled', 'disabled');
        dom.txtHoras.attr('disabled', 'disabled');
        dom.txtHoras.attr('aria-readonly', 'readonly');
        dom.txtHorasDanger.addClass('hidden');
        dom.txtFIni.attr('disabled', 'disabled');
        dom.txtFIni.attr('aria-readonly', 'readonly');
        dom.txtFIni.attr('aria-required', 'false');
        dom.txtFIni.val(dom.txtFIniH.val());
        dom.txtFIniDanger.addClass('hidden');
        dom.txtFIniDanger.attr('aria-hidden', true);
        

    }

    tipoImputacion2 = function (e) {
        dom.cmbModo.attr('disabled', 'disabled');
        dom.chkFestivos.attr('checked', false);
        dom.chkFestivos.attr('disabled', 'disabled');
        dom.txtHoras.attr('disabled', 'disabled');
        dom.txtHoras.attr('aria-readonly', 'readonly');
        dom.txtHorasDanger.addClass('hidden');
        dom.txtFIni.removeAttr('disabled');
        dom.txtFIni.attr('aria-required', 'true');
        dom.txtFIniDanger.removeClass('hidden');
        dom.txtFIniDanger.attr('aria-hidden', false);
        dom.txtFIni.attr('aria-readonly', false);
        dom.radio2.attr('checked', 'checked');
        dom.radio3.attr('checked', false);
        dom.radio1.attr('checked', false);
    }

    tipoImputacion3 = function(e){
        dom.cmbModo.removeAttr('disabled');
        dom.chkFestivos.attr('checked', false);
        dom.chkFestivos.removeAttr('disabled');
        dom.txtHoras.removeAttr('disabled');
        dom.txtHoras.attr('aria-readonly', false);
        dom.txtHorasDanger.removeClass('hidden');
        dom.txtFIni.removeAttr('disabled');
        dom.txtFIni.attr('aria-required', 'true');
        dom.txtFIni.attr('aria-readonly', false);
        dom.txtFIniDanger.removeClass('hidden');
        dom.txtFIniDanger.attr('aria-hidden', false);
        dom.radio3.attr('checked', 'checked');
        dom.radio1.attr('checked', false);
        dom.radio2.attr('checked', false);
    }

    radioCheckeado = function (elemento) {
        //return (elemento.attr("checked") == "checked");
        return elemento.is(':checked')
    }

    obtenerValor = function (elemento) {
        return elemento.val();
    }

    darValor = function (elemento, value) {
        elemento.val(value);
    }

    asignarFoco = function (elemento) {
        elemento.focus(scrollGo);
        elemento.select();     
    }

    function scrollGo() {
        var x = $(this).offset().top - 200; 
        $('html,body').animate({ scrollTop: x });
    }

    //accounting.formatNumber(oPrecolab.t867_salario, oPrecolab.mondedadec)
    pintarDatosTarea = function (objTarea) {
        dom.tarea.val(objTarea.t332_idtarea);
        dom.desTarea.val(objTarea.denominacion);
        dom.txtProy.val(objTarea.t301_idproyecto + " - " + objTarea.t305_seudonimo);
        dom.txtProyTec.val(objTarea.t331_despt);
        dom.txtFase.val(objTarea.t334_desfase);
        dom.txtAct.val(objTarea.t335_desactividad);
        if (objTarea.dPrimerConsumo) dom.PConsumo.val(moment(objTarea.dPrimerConsumo).format("DD/MM/YYYY"));
        if (objTarea.dUltimoConsumo) dom.UConsumo.val(moment(objTarea.dUltimoConsumo).format("DD/MM/YYYY"));
        dom.PConsumido.val(accounting.formatNumber(objTarea.esfuerzo));
        dom.UConsumido.val(accounting.formatNumber(objTarea.esfuerzoenjor));
        dom.pEstimado.val(accounting.formatNumber(objTarea.nPendienteEstimado));
        dom.ATeorico.val(accounting.formatNumber(objTarea.nAvanceTeorico));

        //Indicaciones del responsable
        //dom.txtTotPrev.val(parseFloat(objTarea.t336_etp).toFixed(2));
        dom.txtTotPrev.val(accounting.formatNumber(objTarea.t336_etp));
        if (objTarea.t336_ffp) dom.txtFFinPrev.val(moment(objTarea.t336_ffp).format("DD/MM/YYYY"));
        dom.txtParti.val(objTarea.t336_indicaciones); 
        dom.txtColec.val(objTarea.t332_mensaje);
        //Indicaciones del técnico
        dom.txtTotEst.val(accounting.formatNumber(objTarea.t336_ete));
        if (objTarea.t336_ffe) dom.txtFEst.val(moment(objTarea.t336_ffe).format("DD/MM/YYYY"));
        if (objTarea.t336_completado) dom.chkFinalizado.attr('checked', 'checked');
        else dom.chkFinalizado.attr('checked', false);
        dom.txtObsv.val(objTarea.t336_comentario);
        if (objTarea.t324_idmodofact) dom.chkFacturable.attr('checked', 'checked');
        else dom.chkFacturable.attr('checked', false);
        dom.txtModoFact.val(objTarea.t324_denominacion);
        dom.RegJorNoCompleta.val(objTarea.t323_regjornocompleta);
        dom.ImputarFestivos.val(objTarea.t323_regfes);
        dom.fechaInicioImpPermitida.val(objTarea.fechaInicioImpPermitida);
        dom.fechaFinImpPermitida.val(objTarea.fechaFinImpPermitida);

    }

    limpiarDatosTarea = function (e) {
        //dom.tarea.val("");
        dom.desTarea.val("");
        dom.txtProy.val("");
        dom.txtProyTec.val("");
        dom.txtFase.val("");
        dom.txtAct.val("");
        dom.PConsumo.val("");
        dom.UConsumo.val("");
        dom.PConsumido.val("");
        dom.UConsumido.val("");
        dom.pEstimado.val("");
        dom.ATeorico.val("");
        dom.txtTotPrev.val("");
        dom.txtFFinPrev.val("");
        dom.txtParti.val("");
        dom.txtColec.val("");
        dom.fechaInicioImpPermitida.val("");
        dom.fechaFinImpPermitida.val("");
    }

    return {
        init: init,
        dom: dom,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        collapse: collapse,
        abrirGuia: abrirGuia,
        selTipoImputacion: selTipoImputacion,
        abrirDatePicker: abrirDatePicker,
        radioCheckeado: radioCheckeado,
        pintarDatosTarea: pintarDatosTarea,
        obtenerValor: obtenerValor,
        darValor: darValor,
        asignarFoco: asignarFoco,
        limpiarDatosTarea: limpiarDatosTarea
        
    }

})();