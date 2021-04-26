var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ImpDiaria = SUPER.IAP30.ImpDiaria || {}


SUPER.IAP30.ImpDiaria.viewTarea = (function () {

    var dom = {
        chevron: $('.chevron_toggleable'),
        tarea: $('#idTarea'),
        desTarea: $('#desTarea'),
        txtProy: $('#txtProy'),
        txtProyTec: $('#txtProyTec'),
        txtFase: $('#txtFase'),
        txtAct: $('#txtAct'),
        txtDesc: $('#txtDesc'),
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
        txtObsv: $('#txtObsv'),
        txtTotEst: $('#txtTotEst'),
        txtFEst: $('#txtFEst'),
        chkFinalizado: $('#chkFinalizado'),
        //Notas
        txtInv: $('#txtInv'),
        txtAcc: $('#txtAcc'),
        txtPru: $('#txtPru'),
        txtPaso: $('#txtPaso'),
        tab: $("#tabs > li"),
        pestanaDocu: $('#pestanaDocu'),
        modalDetalleTarea: $('#modalDetalleTarea'),
        irFieldset: $("#irFieldset"),
        ctFieldset: $("#ctFieldset"),
        infoOcultable: $('#ocultable'),
        btnGrabar: $('#btnGrabarTarea'),
        btnCancelar: $('#btnCancelarTarea'),
        btnCierreModalTarea: $('#btnCierreModalTarea')

    };

    var selectores = {
        sel_inputs: "#general input, #notas input",
        sel_textarea: "#general textarea, #notas textarea"
    }

    var init = function () {
        controlPestaña();        
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    

    collapse = function (e) {
        $(this).toggleClass('fa-chevron-down fa-chevron-up');
    }

    asignarFoco = function (elemento) {
        elemento.focus(scrollGo);
        elemento.select();
    }

    function scrollGo() {
        var x = $(this).offset().top - 200;
        $('html,body').animate({ scrollTop: x });
    }
   
    //Validación de inputación manual en datepickers    
    $(document).on('focus', '.txtFechaTarea:not(.hasDatepicker)', function (e) {
        $(this).datepicker({
            changeMonth: true,
            changeYear: true,
            //defaultDate: $('#L').attr('data-date'),

            beforeShow: function (input, inst) {
                $(this).removeClass('calendar-off').addClass('calendar-on');
            },
            onClose: function () {
                $(this).removeClass('calendar-on').addClass('calendar-off');
            }
        });
        $(this).change(function () {
            //....
        });
        $(this).focusout(function () {
            var input = $(this);
            window.setTimeout(function () {
                if ((!moment(input.val(), 'DD/MM/YYYY', 'es', true).isValid()) && (input.val() != '')) {
                    IB.bsalert.toastdanger("Formato de fecha incorrecto: " + input.val());
                    input.val('');
                    input.focus();
                }
            }, 100);
        });
    });


    //No se podrá acceder a las pestañas que están disabled
    controlPestaña = function (e) {
        $("#tabs > li").click(function () {
            if ($(this).hasClass("disabled"))
                return false;
        });
    }

    obtenerValor = function (elemento) {
        return elemento.val();
    }

    darValor = function (elemento, value) {
        elemento.val(value);
    }

    radioCheckeado = function (elemento) {
        return elemento.is(':checked')
    }

    modDatosFinalizacion = function (elemento) {
        if (radioCheckeado($(this))) {
            darValor(dom.pEstimado, "0");
            darValor(dom.ATeorico, "100");
            if (obtenerValor(dom.PConsumido) != "") darValor(dom.txtTotEst, obtenerValor(dom.PConsumido));
            if (obtenerValor(dom.UConsumo) != "") darValor(dom.txtFEst, obtenerValor(dom.UConsumo));
        }
    }

    descheckear = function (elemento) {
       dom.chkFinalizado.removeAttr('checked');
    }

    pantallaDeLectura = function (elemento) {
        dom.txtObsv.attr("disabled", "disabled");
        dom.txtTotEst.attr("disabled", "disabled");
        dom.txtFEst.attr("disabled", "disabled");
        dom.chkFinalizado.attr("disabled", "disabled");

    }

    pantallaDeEscritura = function (elemento) {
        dom.txtObsv.removeAttr("disabled");
        dom.txtTotEst.removeAttr("disabled");
        dom.txtFEst.removeAttr("disabled");
        dom.chkFinalizado.removeAttr("disabled");

    }

    pintarDatosTarea = function (objTarea) {
        dom.tarea.val(objTarea.t332_idtarea);
        dom.desTarea.val(objTarea.t332_destarea);
        dom.txtProy.val(objTarea.t301_idproyecto + " - " + objTarea.t305_seudonimo);
        dom.txtProyTec.val(objTarea.t331_despt);
        dom.txtFase.val(objTarea.t334_desfase);
        dom.txtAct.val(objTarea.t335_desactividad);
        dom.txtDesc.val(objTarea.t332_destarealong);

        if (objTarea.dPrimerConsumo) dom.PConsumo.val(moment(objTarea.dPrimerConsumo).format("DD/MM/YYYY"));
        if (objTarea.dUltimoConsumo) dom.UConsumo.val(moment(objTarea.dUltimoConsumo).format("DD/MM/YYYY"));
        dom.PConsumido.val(accounting.formatNumber(objTarea.esfuerzo));
        dom.UConsumido.val(accounting.formatNumber(objTarea.esfuerzoenjor));
        dom.pEstimado.val(accounting.formatNumber(objTarea.nPendienteEstimado));
        dom.ATeorico.val(accounting.formatNumber(objTarea.nAvanceTeorico));


        //Indicaciones del responsable
        dom.txtTotPrev.val(accounting.formatNumber(objTarea.t336_etp));
        if (objTarea.t336_ffp) dom.txtFFinPrev.val(moment(objTarea.t336_ffp).format("DD/MM/YYYY"));
        dom.txtParti.val(objTarea.t336_indicaciones);
        dom.txtColec.val(objTarea.t332_mensaje);

        //Indicaciones del técnico
        if (objTarea.t336_ete) dom.txtTotEst.val(accounting.formatNumber(objTarea.t336_ete));
        else dom.txtTotEst.val("");
        if (objTarea.t336_ffe) dom.txtFEst.val(moment(objTarea.t336_ffe).format("DD/MM/YYYY"));
        else dom.txtFEst.val("");
        if (objTarea.t336_completado) dom.chkFinalizado.prop('checked', 'checked');
        else dom.chkFinalizado.prop('checked', false);
        if (objTarea.t336_comentario) dom.txtObsv.val(objTarea.t336_comentario);
        else dom.txtObsv.val("");

        //Notas
        dom.txtInv.val(objTarea.t332_notas1);
        dom.txtAcc.val(objTarea.t332_notas2);
        dom.txtPru.val(objTarea.t332_notas3);
        dom.txtPaso.val(objTarea.t332_notas4);

        if (!objTarea.t332_notasiap) {
            $($("#tabs li")[2]).addClass("disabled");
        } else $($("#tabs li")[2]).removeClass("disabled");

    }

    //Modal detalle tarea
    function aperturaModal(element) {        
        element.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        element.modal('show');
        dom.infoOcultable.attr('area-hidden', true);
    }

    function cierreModal() {
        dom.infoOcultable.attr('aria-hidden', 'false');
    }

    function ajustarCampos() {
        dom.ctFieldset.height(dom.irFieldset.height());
    }

    $(window).resize(function () {
        dom.ctFieldset.height(dom.irFieldset.height());
    });
   

    return {
        init: init,
        dom: dom,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        collapse: collapse,
        pintarDatosTarea: pintarDatosTarea,
        obtenerValor: obtenerValor,
        darValor: darValor,
        controlPestaña: controlPestaña,
        radioCheckeado: radioCheckeado,
        modDatosFinalizacion: modDatosFinalizacion,
        descheckear: descheckear,
        pantallaDeLectura: pantallaDeLectura,
        pantallaDeEscritura: pantallaDeEscritura,
        aperturaModal: aperturaModal,
        ajustarCampos: ajustarCampos,
        cierreModal: cierreModal,
        selectores: selectores,
        asignarFoco: asignarFoco
    }

})();
