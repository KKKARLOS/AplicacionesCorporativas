var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Tarea = SUPER.IAP30.Tarea || {}


SUPER.IAP30.Tarea.View = (function () {

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
        btnGrabar: $('#btnGrabar')
       
    };

    var init = function () {
        asignarControlDatepicker();
        controlPestaña();
        //inicializarTablaDocu();

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
    
    asignarControlDatepicker = function (e) {       
        dom.txtFEst.datepicker({
            changeMonth: true,
            changeYear: true,
            beforeShow: function (input, inst) {
                $(this).removeClass('calendar-off').addClass('calendar-on');
            },
            onClose: function () {
                $(this).removeClass('calendar-on').addClass('calendar-off');
            }
        });
    }
    
    inicializarTablaDocu = function (e) {
        $('#tablaDocu').DataTable({          
            "procesing": true,
            "paging": false,
            "responsive": false,            
            //"scrollY": "300px",
            "scrollCollapse": true,
            "language": { "url": IB.vars.strserver + 'plugins/datatables/Spanish.txt' },
            "info": false,
            "searching": false,
            "ajax": {
                "url": "Default.aspx/obtenerDocumentos",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ idTarea: IB.vars.idTarea }); },
                "dataSrc": function (json) {                    
                    return json.d;
                },               
                "error": function (ex, status) {
                    IB.bserror.error$ajax(ex, status);
                }
            },
           
            "columns": [
                    { "data": "t363_descripcion" },
                    { "data": "t363_nombrearchivo" },
                    { "data": "t363_weblink" },
                    { "data": "t314_idusuario_autor" }

            ],
            "columnDefs": [
                       
                        {
                            "targets": 2,
                            "render": {
                                "display": function (data, type, row, meta) {
                                    return '<a href="#">' + row.t363_weblink + '</a>';
                                    
                                }
                            }
                        }
            ]
            
            
        });

        $(document).on('click', '#tablaDocu tbody tr', function (e) {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                $('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });


        /*
        [t363_DOCUT].[t363_iddocut] AS t363_iddocut,
		[t363_DOCUT].[t332_idtarea] AS t332_idtarea,
		[t363_DOCUT].[t363_descripcion] AS t363_descripcion,
		[t363_DOCUT].[t363_weblink] AS t363_weblink,
		[t363_DOCUT].[t363_nombrearchivo] AS t363_nombrearchivo,
		--[t363_DOCUT].[t363_archivo] AS t363_archivo,
		[t363_DOCUT].[t363_privado] AS t363_privado,
		[t363_DOCUT].[t363_modolectura] AS t363_modolectura,
		[t363_DOCUT].[t363_tipogestion] AS t363_tipogestion,
		[t363_DOCUT].[t314_idusuario_autor] AS t314_idusuario_autor,*/

    }

    

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

    pantallaDeLectura = function (elemento) {
        dom.txtObsv.attr("disabled", "disabled");
        dom.txtTotEst.attr("disabled", "disabled");
        dom.txtFEst.attr("disabled", "disabled");
        dom.chkFinalizado.attr("disabled", "disabled");
        
    }

    pintarDatosTarea = function (objTarea) {
        dom.tarea.val(objTarea.t332_idtarea);
        dom.desTarea.val(objTarea.t301_denominacion);
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
        dom.txtTotEst.val(accounting.formatNumber(objTarea.t336_ete));
        if (objTarea.t336_ffe) dom.txtFEst.val(moment(objTarea.t336_ffe).format("DD/MM/YYYY"));
        if (objTarea.t336_completado) dom.chkFinalizado.attr('checked', 'checked');
        else dom.chkFinalizado.attr('checked', false);
        dom.txtObsv.val(objTarea.t336_comentario);
        
        //Notas
        dom.txtInv.val(objTarea.t332_notas1);
        dom.txtAcc.val(objTarea.t332_notas2);
        dom.txtPru.val(objTarea.t332_notas3);
        dom.txtPaso.val(objTarea.t332_notas4);

        if (!objTarea.t332_notasiap){
            $($("#tabs li")[2]).addClass("disabled");
        }
    }

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
        pantallaDeLectura: pantallaDeLectura,
    }

})();

