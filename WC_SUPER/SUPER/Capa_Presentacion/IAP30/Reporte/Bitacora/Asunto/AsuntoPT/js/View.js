var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.BitacoraAsuntoPT = SUPER.IAP30.BitacoraAsuntoPT || {}

SUPER.IAP30.BitacoraAsuntoPT.View = (function () {
    //Booleano indicador MarcarDesmarcar

    var bMarcarDesmarcarProf = false;
    var bMarcarDesmarcarProfSelec = false;

    var dom = {
        title: $('.ibox-title_toggleable'),
        idAsunto: $('#txtIdAsunto'),
        desAsunto: $('#txtDesAsunto'),
        registrador: $('#txtRegistrador'),
        responsable: $('#txtResponsable'),
        notificador: $('#txtNotificador'),
        fechaCreacion: $('#txtFechaCreacion'),
        fechaNotificacion: $('#txtFechaNotificacion'),
        fechaLimite: $('#txtFechaLimite'),
        fechaFinalizacion: $('#txtFechaFinalizacion'),
        refExterma: $('#txtRefExt'),
        esfuerzoPlanificado: $('#txtEtp'),
        esfuerzoReal: $('#txtEtr'),
        severidad: $('#cboSeveridad'),
        prioridad: $('#cboPrioridad'),
        tipo: $('#cboTipo'),
        estado: $('#cboEstado'),
        sistema: $('#txtSistema'),
        observaciones: $('#txtObservaciones'),
        descripcion: $('#txtDescripcion'),
        departamento: $('#txtDpto'),
        alerta: $('#txtAlerta'),
        ventana: $(window),
        btnGrabar: $('#btnGrabar'),
        btnRegresar: $('#btnSalir'),
        btnSeleccionar: $('#btnSeleccionar'),
        btnEliminarSeleccionados: $("#btnEliminarSeleccionados"),
        btnDesmarcarEliminados: $("#btnDesmarcarEliminados"),
        btnMarcarDesmarcarOrigen: $("#btnMarcarDesmarcarOrigen"),
        btnMarcarDesmarcarDestino: $("#btnMarcarDesmarcarDestino"),
        icoNuevoDocu: $('#nuevoDocu'),
        modalFichero: $('#modal-subir'),
        enlaceBuscadorProfesional: $('#lblResponsable'),
        modalBuscadorProfesional: $('#buscProfesional'),
        tablaCrono: $('#bodyTablaCrono'),
        lblNumero: $('#lblNumero'),
        lblCreacion: $('#lblCreacion'),
        divBuscadorPersonas: $('.buscadorUsuario'),
        lblResponsable: $('#lblResponsable'),
        tablaProfesionales: $("#tbodyProfesionales"),
        tablaProfesionalesSeleccionados: $("#tbodyProfesionalesSel"),
        Apellido1: $("#txtApellido1"),
        Apellido2: $("#txtApellido2"),
        Nombre: $("#txtNombre"),
        buscAsignados: $("#buscAsignados"),
        capaProfesionales: $("#capaProfesionales")
    }

    var selectores = {
        sel_inputs: "#general input",
        sel_selects: "#general select",
        sel_textarea: "#general textarea",
        sel_numeroDecimal: "input.numeroDecimal",
        tab: $("#tabs > li"),
        sel_datepickers: "input.hasDatepicker",
        sel_notificables: "input.notificable",
        container: ".container",
        filasProfesionales: "#tbodyProfesionales > tr",
        filasProfesionalesSel: "#tbodyProfesionalesSel > tr",
        filasSeleccionadasProfesionales: "#tbodyProfesionales .activa",
        filasSeleccionadasProfesionalesSel: "#tbodyProfesionalesSel .activa",
        buscarProfesionales: ".criterios :input[type=text]",
        buscarCadenaProfesionalesSel: "#tbodyProfesionalesSel > tr > td:first-child"
    }

    var indicadores = {
        i_dispositivoTactil: false
    }

    var validarNumerico = function (e) {
        return validarTeclaNumerica(e, true);
    }

    var validarFormatoDecimal = function (event) {
        $(this).val(accounting.formatNumber(getFloat($(this).val())));
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {
        if (IB.vars.permiso == "L") {
            $(selectores.sel_inputs).prop('disabled', true);
            $(selectores.sel_selects).prop('disabled', true);
            $(selectores.sel_textarea).prop('disabled', true);

            //dom.btnMarcarDesmarcarOrigen.hide();
            //dom.btnMarcarDesmarcarDestino.hide();
            dom.btnSeleccionar.hide();
            dom.btnDesmarcarEliminados.hide();
            dom.btnEliminarSeleccionados.hide();
            dom.btnGrabar.hide();
        }
        else {
            asignarControlDatepicker();
        }
        controlPestaña();

        if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
            indicadores.i_dispositivoTactil = true;
        }
        //if (!indicadores.i_dispositivoTactil) {
        //    $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true, animation: true });
        //}

        //Si vengo de la consulta de bitácora de IAP contraigo la capa de profesionales asignados para dar más espacio a la descripción y observaciones
        if (IB.vars.origen == "IAP")
            aumentarObs();

        dom.desAsunto.focus();
    }

    var aumentarObs = function () {
        dom.capaProfesionales.click();
        dom.descripcion[0].rows = 10;
        dom.observaciones[0].rows = 10;
    }

    controlPestaña = function (e) {
        $("#tabs > li").click(function () {
            if ($(this).hasClass("disabled"))
                return false;
        });
    }
    $(window).resize(function () {
        controlarScroll();
    });

    collapse = function (e) {
        //$(this).toggleClass('fa-chevron-down fa-chevron-up');
        $(this).find('.fa').toggleClass('fa-chevron-down fa-chevron-up');
    }

    asignarControlDatepicker = function (e) {
        //dom.fechaDesde.datepicker();
        //dom.fechaHasta.datepicker();
        $(document).on('focus', '#txtFechaNotificacion:not(.hasDatepicker)', function (e) {
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

        $(document).on('focus', '#txtFechaLimite:not(.hasDatepicker)', function (e) {
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
        $(document).on('focus', '#txtFechaFinalizacion:not(.hasDatepicker)', function (e) {
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
    }
    function cebrear() {
        $("tr:visible:not(.bg-info)").removeClass("cebreada");
        $('tr:visible:not(.bg-info):even').addClass('cebreada');
        controlarScroll();
    }
    var visualizarContenedor = function () {
        $(selectores.container).css("visibility", "visible");
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

    function rellenarComboTiposAsunto(data) {
        //Alimentar el combo de proyectos para ese rango temporal
        dom.tipo.empty();
        $.each(data, function (i, item) {
            dom.tipo.append('<option value=' + item.T384_idtipo + '>' + item.T384_destipo + '</option>');
        });
        if (IB.vars.idAsunto == "") dom.tipo.val(dom.tipo.find('option:first').val()).change();
        //IB.procesando.ocultar();
        //ObtenerDatosTabla();
    }

    pintarDatosAsunto = function (oAsunto) {
        dom.idAsunto.val(accounting.formatNumber(oAsunto.T409_idasunto,0));
        dom.desAsunto.val(oAsunto.T409_desasunto);
        dom.descripcion.val(oAsunto.T409_desasuntolong);
        dom.esfuerzoPlanificado.val(accounting.formatNumber(oAsunto.T409_etp));
        dom.esfuerzoReal.val(accounting.formatNumber(oAsunto.T409_etr));
       
        if (oAsunto.T409_fcreacion != null) dom.fechaCreacion.val(moment(oAsunto.T409_fcreacion).format("DD/MM/YYYY"));
        if (oAsunto.T409_fnotificacion != null) dom.fechaNotificacion.val(moment(oAsunto.T409_fnotificacion).format("DD/MM/YYYY"));
        if (oAsunto.T409_flimite != null) dom.fechaLimite.val(moment(oAsunto.T409_flimite).format("DD/MM/YYYY"));
        if (oAsunto.T409_ffin != null) dom.fechaFinalizacion.val(moment(oAsunto.T409_ffin).format("DD/MM/YYYY"));

        dom.departamento.val(oAsunto.T409_dpto);
        dom.alerta.val(oAsunto.T409_alerta);
        dom.observaciones.val(oAsunto.T409_obs);
        dom.refExterma.val(oAsunto.T409_refexterna);
        dom.sistema.val(oAsunto.T409_sistema);
        dom.estado.val(oAsunto.T409_estado);
        IB.vars.coEstadoAnterior = oAsunto.T409_estado;
        dom.prioridad.val(oAsunto.T409_prioridad);
        dom.severidad.val(oAsunto.T409_severidad);
        dom.tipo.val(oAsunto.t384_idtipo);
        dom.registrador.val(oAsunto.Registrador);
        dom.notificador.val(oAsunto.T409_notificador);
        dom.responsable.val(oAsunto.Responsable);
        IB.vars.idResponsable = oAsunto.T409_responsable;
        IB.vars.bCambios = 0;
        dom.desAsunto.focus();
    }

    pintarDatosAltaAsunto = function (e) {
        dom.fechaCreacion.val(IB.vars.fechaDia);
        dom.fechaNotificacion.val(IB.vars.fechaDia);
        dom.responsable.val(IB.vars.nombreEmpleadoEntrada);
        dom.registrador.val(IB.vars.nombreEmpleadoEntrada);
        dom.desAsunto.focus();
    }

    function pintarProfesionales(data) {
        var tblProfesionales = "";

        $.each(data, function (index, item) {
            tblProfesionales += "<tr class='linea' data-id='" + item.t314_idusuario + "' data-mail='" + item.MAIL + "' data-bd='' data-sexo='" + item.t001_sexo + "' data-baja='" + item.baja + "' data-tipo='" + item.tipo + "'>";
            tblProfesionales += "<td class='text-left'>" + item.Profesional + "</td>";
            tblProfesionales += "</tr>";
            //dom.tablaProfesionalesSeleccionados.append(tblDetalle);
        });

        ////Inyectar html en la página
        dom.tablaProfesionales.html(tblProfesionales);
        cebrear();
    }

    function pintarProfesionalesAsunto(data) {
        var tblProfesionalesSel = "";
        var sNotificacion = "";

        $.each(data, function (index, item) {
            tblProfesionalesSel += "<tr class='linea' data-id='" + item.t314_idusuario + "' data-mail='" + item.MAIL + "' data-bd='' data-sexo='" + item.t001_sexo + "' data-baja='" + item.baja + "' data-tipo='" + item.tipo + "'>";
            tblProfesionalesSel += "<td class='text-left'>" + item.nomRecurso + "</td>";
            sNotificacion = (item.t413_notificar) ? "checked=true" : "";
            tblProfesionalesSel += "<td class='text-center'><input type='checkbox' ";
            if (IB.vars.permiso == "L") tblProfesionalesSel += "disabled ";
            tblProfesionalesSel += "class='notificable' style='width:20px; margin-top:0px;' " + sNotificacion + "></td>";
            tblProfesionalesSel += "</tr>";
            //dom.tablaProfesionalesSeleccionados.append(tblDetalle);
        });

        //Inyectar html en la página
        dom.tablaProfesionalesSeleccionados.html(tblProfesionalesSel);
        cebrear();
    }

    function pintarDatosPSN_OPD(oPSN) {
        if (oPSN.t305_opd) {
            $("#lblNumero").css("display", "none");
            dom.idAsunto.hide();
            $("#lblCreacion").css("display", "none");
            dom.fechaCreacion.hide();
            $($("#tabs li")[1]).addClass("disabled");
        }
    }
    function pintarTablaCronologia(data) {
        var tblDetalle = "";
        var sFecha = "";

        $.each(data, function (index, item) {
            tblDetalle += "<tr class='linea'>";
            tblDetalle += "<td class='text-left'>" + item.Estado + "</td>";
            sFecha = item.t416_fecha;
            if (sFecha != "") sFecha = moment(item.t416_fecha).format("DD/MM/YYYY");
            tblDetalle += "<td class='text-left'>" + sFecha + "</td>";
            tblDetalle += "<td class='text-left'>" + item.nomRecurso + "</td>";
            tblDetalle += "</tr>";
            //dom.tablaCrono.append(tblDetalle);
        });

        //Inyectar html en la página
        dom.tablaCrono.html(tblDetalle);
        cebrear();
    }
    function marcarLeido(e) {
        e.removeClass("noLeido").addClass("leido");
    }
    function marcarNoLeidoCrono() {
        $("#tabs > li:nth-child(2) a").removeClass("leido").addClass("noLeido");
        IB.vars.coEstadoAnterior = dom.estado.val();
    }

    var anadirProfesionalesDblClick = function (e) {
        var bInsertar = true;
        var sProfesional = $(this).text().toLowerCase();
        $.each($(selectores.buscarCadenaProfesionalesSel), function (i, item) {
            if (sProfesional.replace(/\s+/g, '').indexOf($(item).text().replace(/\s+/g, '').toLowerCase()) != -1)
                bInsertar = false;
        })

        if (bInsertar) {
            var tr = $(this).closest("tr").clone();
            tr.attr("data-bd", "I");
            tr.children().eq(0).after("<td class='text-center'><input type='checkbox' class='notificable' style='width:20px; margin-top:0px;'></td>")
            $(dom.tablaProfesionalesSeleccionados).append(tr);
            //$(this).closest("tr").remove(); // no lo eliminamos
        }
        cebrear();
    };

    var anadirProfesionalesBtn = function (e) {
        $.each($(selectores.filasSeleccionadasProfesionales), function (i, item) {
            //controlar si ya existe en la tabla destino antes de insertarlo
            var bInsertar = true;
            $.each($(selectores.buscarCadenaProfesionalesSel), function (e) {
                if ($(this).text().toLowerCase().replace(/\s+/g, '').indexOf($(item).children().eq(0).text().replace(/\s+/g, '').toLowerCase()) != -1)
                    bInsertar = false;
            })

            if (bInsertar) {
                var tr = $(this).closest("tr").clone();
                tr.attr("data-bd", "I");
                tr.children().eq(0).after("<td class='text-center'><input type='checkbox' class='notificable' style='width:20px; margin-top:0px;'></td>")
                $(dom.tablaProfesionalesSeleccionados).append(tr);
                //$(this).closest("tr").remove(); // no lo eliminamos
            }
        });
        $(selectores.filasSeleccionadasProfesionales).removeClass('activa');
        $(selectores.filasSeleccionadasProfesionalesSel).removeClass('activa');
        cebrear();
        //event.stopPropagation();
        return false;
    }
    var buscarProfesionalesAsignados = function (e) {
        $.each($(selectores.buscarCadenaProfesionalesSel), function (e) {
            if ($(this).text().toLowerCase().replace(/\s+/g, '').indexOf(dom.buscAsignados.val().replace(/\s+/g, '').toLowerCase()) == -1)
                $(this).parent().hide();
            else
                $(this).parent().show();
        });
    }

    var marcarDesmarcarOrigen = function (e) {
        if (bMarcarDesmarcarProf) $(this).children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
        else $(this).children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');

        $.each($(selectores.filasProfesionales), function (e) {
            if (bMarcarDesmarcarProf) {
                $(this).removeClass('activa');
                $(this).children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
            }
            else {
                $(this).removeClass('activa').addClass('activa');
                $(this).children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
            }
        });
        if (bMarcarDesmarcarProf) bMarcarDesmarcarProf = false;
        else bMarcarDesmarcarProf = true;
    }

    var marcarDesmarcarDestino = function (e) {
        if (bMarcarDesmarcarProf) $(this).children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
        else $(this).children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');

        $.each($(selectores.filasProfesionalesSel), function (e) {
            if (bMarcarDesmarcarProf) {
                $(this).removeClass('activa');
            }
            else {
                $(this).removeClass('activa').addClass('activa');
            }
        });
        if (bMarcarDesmarcarProf) bMarcarDesmarcarProf = false;
        else bMarcarDesmarcarProf = true;
    }
    var modificarBDFila = function (e) {
        if (IB.vars.permiso == "L") return false;
        if ($(this).parent().parent().attr("data-bd") == "") $(this).parent().parent().attr("data-bd", "U");
    }
    var marcarFila = function (e) {
        var oElement = e.srcElement ? e.srcElement : e.target;
        oFila = $(oElement).closest("tr");

        if (oFila.hasClass('activa')) {
            oFila.removeClass('activa');
        }
        else {
            oFila.removeClass('activa');
            oFila.addClass('activa');
        }
    }

    //Marcar para borrar en profesionales seleccionados las filas seleccionadas si existe o eliminar de la tabla si no existe
    var eliminarFilaProfesionalesSel = function (e) {
        $.each($(selectores.filasSeleccionadasProfesionalesSel), function (e) {
            if ($(this).attr("data-bd") != "I") {
                $(this).attr("data-bd", "D");
                $(this).children().eq(0).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
            }
            else $(this).closest('tr').remove();
        });
        $(selectores.filasSeleccionadasProfesionalesSel).removeClass('activa');
        //event.stopPropagation();
        return false;
    }

    var desmarcarFilaProfesionalesEliminados = function (e) {
        $.each($(selectores.filasProfesionalesSel), function (e) {
            if ($(this).attr("data-bd") == "D") {
                $(this).attr("data-bd", "");
                $(this).children().eq(0).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
            }
        });
        $(selectores.filasSeleccionadasProfesionalesSel).removeClass('activa');
        //event.stopPropagation();
        return false;
    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        rellenarComboTiposAsunto: rellenarComboTiposAsunto,
        pintarDatosAsunto: pintarDatosAsunto,
        pintarProfesionalesAsunto: pintarProfesionalesAsunto,
        pintarDatosPSN_OPD: pintarDatosPSN_OPD,
        pintarTablaCronologia: pintarTablaCronologia,
        visualizarContenedor: visualizarContenedor,
        pintarProfesionales: pintarProfesionales,
        cebrear: cebrear,
        collapse: collapse,
        controlarScroll: controlarScroll,
        validarNumerico: validarNumerico,
        validarFormatoDecimal: validarFormatoDecimal,
        marcarLeido: marcarLeido,
        anadirProfesionalesDblClick: anadirProfesionalesDblClick,
        anadirProfesionalesBtn: anadirProfesionalesBtn,
        buscarProfesionalesAsignados: buscarProfesionalesAsignados,
        marcarDesmarcarOrigen: marcarDesmarcarOrigen,
        marcarDesmarcarDestino: marcarDesmarcarDestino,
        modificarBDFila: modificarBDFila,
        marcarFila: marcarFila,
        eliminarFilaProfesionalesSel: eliminarFilaProfesionalesSel,
        desmarcarFilaProfesionalesEliminados: desmarcarFilaProfesionalesEliminados,
        pintarDatosAltaAsunto: pintarDatosAltaAsunto,
        marcarNoLeidoCrono: marcarNoLeidoCrono
    }
})()