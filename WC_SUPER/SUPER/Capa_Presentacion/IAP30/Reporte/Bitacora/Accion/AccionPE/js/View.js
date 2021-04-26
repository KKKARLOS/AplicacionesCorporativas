var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.BitacoraAccionPE = SUPER.IAP30.BitacoraAccionPE || {}

SUPER.IAP30.BitacoraAccionPE.View = (function () {
    //Booleano indicador MarcarDesmarcar

    var bMarcarDesmarcarProf = false;
    var bMarcarDesmarcarProfSelec = false;

    var dom = {
        title: $('.ibox-title_toggleable'),
        idAccion: $('#txtIdAccion'),
        desAccion: $('#txtDesAccion'),
        avance: $('#cboAvance'),
        idAsunto: $('#txtIdAsunto'),
        desAsunto: $('#txtDesAsunto'),
        fechaLimite: $('#txtFechaLimite'),
        fechaFinalizacion: $('#txtFechaFinalizacion'),
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
        btnNuevaTarea: $("#btnNuevaTarea"),
        btnEliminarTarea: $("#btnEliminarTarea"),
        btnDeshacerTarea: $("#btnDeshacerTarea"),
        icoNuevoDocu: $('#nuevoDocu'),
        modalFichero: $('#modal-subir'),
        tablaTareas: $('#tbodyTareas'),
        tablaProfesionales: $("#tbodyProfesionales"),
        tablaProfesionalesSeleccionados: $("#tbodyProfesionalesSel"),
        Apellido1: $("#txtApellido1"),
        Apellido2: $("#txtApellido2"),
        Nombre: $("#txtNombre"),
        buscAsignados: $("#buscAsignados"),
        linkTarea: $('#lblTarea'),
        ayudaTarea: $(".fk_ayudaTarea"),
        tablaTareasAyuda: $('#tbodyTareasAyuda'),
        modalTarea: $('#modalTarea'),
        ocultable: $("ocultable"),
        btnCancelarTareas: $("#btnCancelarTareas"),
        btnSeleccionarTareas: $("#btnSeleccionarTareas"),
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
        filasTareas: "#tbodyTareas > tr",
        filasTareasAyuda: "#tbodyTareasAyuda > tr",
        filasSeleccionadasProfesionales: "#tbodyProfesionales .activa",
        filasSeleccionadasProfesionalesSel: "#tbodyProfesionalesSel .activa",
        filasSeleccionadasTareas: "#tbodyTareas .activa",
        filasSeleccionadasTareasAyuda: "#tbodyTareasAyuda .activa",
        buscarProfesionales: ".criterios :input[type=text]",
        buscarCadenaProfesionalesSel: "#tbodyProfesionalesSel > tr > td:first-child"
    }

    var indicadores = {
        i_dispositivoTactil: false
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function deAttachLiveEvents(event, selector, callback) {
        $(document).off(event, selector, callback);
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
        if (!indicadores.i_dispositivoTactil) {
            $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true, animation: true });
        }

        //Si vengo de la consulta de bitácora de IAP contraigo la capa de profesionales asignados para dar más espacio a la descripción y observaciones
        if (IB.vars.origen == "IAP")
            aumentarObs();

        dom.desAccion.focus();
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
        $("tbody > tr:visible:not(.bg-info)").removeClass("cebreada");
        $('tbody > tr:visible:not(.bg-info):even').addClass('cebreada');
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

        div = document.getElementById('tbodyTareas');

        scrollWidth = $('#tbodyTareas').width() - div.scrollWidth;

        hasVerticalScrollbar = div.scrollHeight > div.clientHeight;

        if (hasVerticalScrollbar) {
            $("#tblCabeceraTareas").css("width", "calc( " + $('#tblDatosTarea').width() + "px - " + scrollWidth + "px )");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }
        else {
            $("#tblCabeceraTareas").css("width", "" + $('#tblDatosTarea').width() + "px");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }

/*      En pantallas modales no es necesario 
        
        div = document.getElementById('tbodyTareasAyuda');

        scrollWidth = $('#tbodyTareasAyuda').width() - div.scrollWidth;

        hasVerticalScrollbar = div.scrollHeight > div.clientHeight;

        if (hasVerticalScrollbar) {
            $("#tblCabeceraTareasAyuda").css("width", "calc( " + $('#tblTareasAyuda').width() + "px - " + scrollWidth + "px )");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }
        else {
            $("#tblCabeceraTareasAyuda").css("width", "" + $('#tblTareasAyuda').width() + "px");
            //$("#tabPie").css("width", "calc( " + $('#tblDatos').width() + "px - " + scrollWidth + "px )");
        }
*/
        /*FIN Controlamos si el contenedor tiene Scroll*/
    }
    pintarDatosAsunto = function (oAsunto) {
        dom.idAsunto.val(accounting.formatNumber(oAsunto.T382_idasunto,0));
        dom.desAsunto.val(oAsunto.T382_desasunto);
        if (IB.vars.idResponsable!="") IB.vars.idResponsable = oAsunto.T382_responsable;
        IB.vars.bCambios = 0;
    }

    pintarDatosAccion = function (oAccion) {
        dom.idAccion.val(accounting.formatNumber(oAccion.t383_idaccion,0));
        dom.desAccion.val(oAccion.t383_desaccion);
        dom.observaciones.val(oAccion.t383_obs);
        dom.descripcion.val(oAccion.t383_desaccionlong);
        dom.departamento.val(oAccion.t383_dpto);
        dom.alerta.val(oAccion.t383_alerta);
        if (oAccion.t383_flimite) dom.fechaLimite.val(moment(oAccion.t383_flimite).format("DD/MM/YYYY"));
        if (oAccion.t383_ffin) dom.fechaFinalizacion.val(moment(oAccion.t383_ffin).format("DD/MM/YYYY"));
        dom.avance.val(oAccion.t383_avance);
        dom.desAccion.focus();
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

    function pintarProfesionalesAccion(data) {
        var tblProfesionalesSel = "";
        var sNotificacion = "";

        $.each(data, function (index, item) {
            tblProfesionalesSel += "<tr class='linea' data-id='" + item.t314_idusuario + "' data-mail='" + item.mail + "' data-bd='' data-sexo='" + item.t001_sexo + "' data-baja='" + item.baja + "' data-tipo='" + item.tipo + "'>";
            tblProfesionalesSel += "<td class='text-left'>" + item.nomRecurso + "</td>";
            sNotificacion = (item.T389_notificar) ? "checked=true" : "";
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
  
    function pintarTablaTareaAyuda(data) {
        var tblDetalle = "";
        var sFecha = "";
        var fAvance = 0;
        var fPrev = 0;
        var fCons = 0;
        var sContent = "";

        $.each(data, function (index, item) {

            sContent = "<b>Proy. Eco.:</b> " + item.nom_proyecto.replace(/"/g, "") + "<br /> <b>Proy. Téc.:</b> " + item.t331_despt.replace(/"/g, "") + "<br />";
            if (item.t334_desfase != "") sContent += " <b>Fase:</b> " + item.t334_desfase.replace(/"/g, "") + "<br />";

            if (item.t335_desactividad != "") sContent += " <b>Actividad:</b> " + item.t335_desactividad.replace(/"/g, "") + "<br />";
            sContent += " <b>Tarea :</b> " + item.t332_idtarea + " - " + item.t332_destarea.replace(/"/g, "");

            tblDetalle += "<tr class='linea' data-id='" + item.t332_idtarea + "' data-bd='' data-destarea='" + item.t332_destarea + "' ";
            tblDetalle += "data-etpl='" + accounting.formatNumber(item.t332_etpl) + "' data-fipl='";
            if (item.t332_fipl) sFecha = moment(item.t332_fipl).format("DD/MM/YYYY");
            else sFecha = "";
            tblDetalle += sFecha + "' data-ffpl='";
            if (item.t332_ffpl) sFecha = moment(item.t332_ffpl).format("DD/MM/YYYY");
            else sFecha = "";
            tblDetalle += sFecha + "' data-ffpr='";
            if (item.t332_ffpr) sFecha = moment(item.t332_ffpr).format("DD/MM/YYYY");
            else sFecha = "";
            tblDetalle += sFecha + "' data-etpr='" + item.t332_etpr + "' data-consumo='" + accounting.formatNumber(item.consumo) + "' data-avance='";
            if (!item.t332_avanceauto) fAvance = item.t332_avance;
            else {
                fPrev = item.t332_etpr;
                fCons = item.consumo;
                if (fPrev == 0) fAvance = 0;
                else fAvance = (fCons * 100) / fPrev;
            }
            tblDetalle += accounting.formatNumber(fAvance) + "' ";
            tblDetalle += "data-placement='top' data-toggle='popover' data-content='" + sContent + "' title='<b>Información</b>'>";
            tblDetalle += "<td class='text-left'>" + accounting.formatNumber(item.t332_idtarea, 0) + "</td>";
            tblDetalle += "<td class='text-left'>" + item.t332_destarea + "</td>";
            tblDetalle += "</tr>";
        });

        //Inyectar html en la página
        dom.tablaTareasAyuda.html(tblDetalle);
        if (!indicadores.i_dispositivoTactil) {
            $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true, animation: true });
        }
        cebrear();
    }
    function pintarTablaTareas(data) {
        var tblDetalle = "";
        var sFecha = "";
        var fAvance = 0;
        var fPrev = 0;
        var fCons = 0;
        var sContent = "";
        $.each(data, function (index, item) {

            sContent = "<b>Proy. Eco.:</b> " + item.nom_proyecto.replace(/"/g, "") + "<br /> <b>Proy. Téc.:</b> " + item.t331_despt.replace(/"/g, "") + "<br />";
            if (item.t334_desfase != "") sContent += " <b>Fase:</b> " + item.t334_desfase.replace(/"/g, "") + "<br />";

            if (item.t335_desactividad != "") sContent += " <b>Actividad:</b> " + item.t335_desactividad.replace(/"/g, "") + "<br />";
            sContent += " <b>Tarea :</b> " + item.t332_idtarea + " - " + item.t332_destarea.replace(/"/g, "");

            tblDetalle += "<tr class='linea' data-id='" + item.t332_idtarea + "' data-bd='' data-placement='top' data-toggle='popover' data-content='" + sContent + "' title='<b>Información</b>'>";
            tblDetalle += "<td class='text-left'>" + accounting.formatNumber(item.t332_idtarea,0) + "</td>";
            tblDetalle += "<td class='text-left'>" + item.t332_destarea + "</td>";
            tblDetalle += "<td class='text-right'>" + accounting.formatNumber(item.t332_etpl) + "</td>";

            if (item.t332_fipl) sFecha = moment(item.t332_fipl).format("DD/MM/YYYY");
            else sFecha = "";

            tblDetalle += "<td class='text-center'>" + sFecha + "</td>";

            if (item.t332_ffpl) sFecha = moment(item.t332_ffpl).format("DD/MM/YYYY");
            else sFecha = "";

            tblDetalle += "<td class='text-center'>" + sFecha + "</td>";
            tblDetalle += "<td class='text-right'>" + item.t332_etpr + "</td>";

            if (item.t332_ffpr) sFecha = moment(item.t332_ffpr).format("DD/MM/YYYY");
            else sFecha = "";

            tblDetalle += "<td class='text-center'>" + sFecha + "</td>";
            tblDetalle += "<td class='text-right'>" + accounting.formatNumber(item.Consumo) + "</td>";

            if (!item.t332_avanceauto)  fAvance = item.t332_avance;
            else
            {
                fPrev = item.t332_etpr;
                fCons = item.Consumo;
                if (fPrev == 0) fAvance = 0;
                else fAvance = (fCons * 100) / fPrev;
            }

            tblDetalle += "<td class='text-right'>" + accounting.formatNumber(fAvance) + "</td>";
            tblDetalle += "<td class='text-center'></td>";
            tblDetalle += "</tr>";
            //dom.tablaProfesionalesSeleccionados.append(tblDetalle);
        });

        //Inyectar html en la página
        dom.tablaTareas.html(tblDetalle);
        if (!indicadores.i_dispositivoTactil) {
            $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true, animation: true });
        }
        cebrear();
    }

    function aperturaModal(element) {
        element.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        element.modal('show');
        dom.ocultable.attr('area-hidden', true);
    }

    var anadirFilaTarea = function (item) {
        var tblNuevaFila = "";
        var sFecha = "";
        tblNuevaFila += "<tr class='linea' data-id='" + $(item).attr("data-id") + "' data-bd='I' data-placement='top' data-toggle='popover' data-content='" + $(item).attr("data-content") + "' title='<b>Información</b>'>";
        tblNuevaFila += "<td class='text-left'>"  + accounting.formatNumber($(item).attr("data-id"), 0) + "</td>";
        tblNuevaFila += "<td class='text-left'>"  + $(item).attr("data-destarea") + "</td>";
        tblNuevaFila += "<td class='text-right'>" + $(item).attr("data-etpl") + "</td>";
        sFecha = ($(item).attr("data-fipl") == "01/01/0001") ? "" : $(item).attr("data-fipl");
        tblNuevaFila += "<td class='text-left'>" + sFecha + "</td>";
        sFecha = ($(item).attr("data-ffpl") == "01/01/0001") ? "" : $(item).attr("data-ffpl");
        tblNuevaFila += "<td class='text-left'>" + sFecha + "</td>";
        tblNuevaFila += "<td class='text-right'>" + $(item).attr("data-etpr") + "</td>";
        sFecha = ($(item).attr("data-ffpr") == "01/01/0001") ? "" : $(item).attr("data-ffpr");
        tblNuevaFila += "<td class='text-left'>" + sFecha + "</td>";
        tblNuevaFila += "<td class='text-right'>" + $(item).attr("data-consumo") + "</td>";
        tblNuevaFila += "<td class='text-right'>" + $(item).attr("data-avance") + "</td>";
        tblNuevaFila += "<td class='text-center'><i class='fa fa-plus fa-fw' aria-hidden='true' style='color: green'></td>";
        tblNuevaFila += "</tr>";
        $(dom.tablaTareas).append(tblNuevaFila);
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
    var validarNumerico = function (e) {
        return validarTeclaNumerica(e, true);
    }

    var validarFormatoDecimal = function (event) {
        $(this).val(accounting.formatNumber(getFloat($(this).val())));
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

    //Marcar para borrar en tareas seleccionadas las filas seleccionadas si existe o eliminar de la tabla si no existe
    var eliminarTarea = function (e) {
        $.each($(selectores.filasSeleccionadasTareas), function (e) {
            if ($(this).attr("data-bd") != "I") {
                $(this).attr("data-bd", "D");
                $(this).children().eq(9).html("<i class='fa fa-trash fa-fw' aria-hidden='true' style='color: red'></i>")
                //$(this).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(1).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(2).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(3).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(4).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(5).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(6).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(7).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
                $(this).children().eq(8).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
            }
            else $(this).closest('tr').remove();
        });
        //event.stopPropagation();
        return false;
    }

    // Deshacer cambios realizados en tareas de filas seleccionadas en caso de seleccionar o de todo
    var deshacerCambiosTarea = function (e) {
        if ($(selectores.filasSeleccionadasTareas).length == 0) {
            $.each($(selectores.filasTareas), function (e) {
                if ($(this).attr("data-bd") == "D") deshacerCambiosParaBorrar($(this))
                else if ($(this).attr("data-bd") == "I") $(this).remove();
            });
        }
        else {
            $.each($(selectores.filasSeleccionadasTareas), function (e) {
                if ($(this).attr("data-bd") == "D") deshacerCambiosParaBorrar($(this))
                else if ($(this).attr("data-bd") == "I") $(this).remove();
            });
        };
    }
    function deshacerCambiosParaBorrar(oFila) {
        oFila.attr("data-bd", "");
        oFila.children().eq(1).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(2).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(3).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(4).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(5).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(6).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(7).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(8).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
        oFila.children().eq(9).html("")
    }
    function marcarLeido(e) {
        e.removeClass("noLeido").addClass("leido");
    }
    var pulsarSeleccionarTareas = function (e) {
        var $selected = $(selectores.filasSeleccionadasTareasAyuda);
        //Si no hubiera ninguno se muestra el aviso
        if ($selected.length == 0) {
            IB.bsalert.toastwarning("No se ha seleccionado ninguna tarea");
            return false;
        }

        $.each($selected, function (i, item) {
            var bInsertar = true;
            var id = $(this).attr("data-id");
            $.each($(selectores.filasTareas), function (e) {
                if (id == $(this).attr("data-id")) bInsertar = false;
            })
            if (bInsertar) {
                hayCambios();
                anadirFilaTarea(item);
            }
        })

        cebrear();

        deAttachLiveEvents("click", selectores.filasTareasAyuda, marcarFila);
        dom.modalTarea.modal('hide');
    }

    var pulsarCancelarTareas = function (e) {
        deAttachLiveEvents("click", selectores.filasTareasAyuda, marcarFila);
        dom.modalTarea.modal('hide');
    }

    //Identificación de cambios en la pantalla
    var hayCambios = function () {
        if (IB.vars.bCambios != 1) {
            IB.vars.bCambios = 1;
        }
    }

    //Acciones que se ejecutan en los callback de los eventos
    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        deAttachLiveEvents: deAttachLiveEvents,
        pintarDatosAsunto: pintarDatosAsunto,
        pintarDatosAccion: pintarDatosAccion,
        pintarProfesionalesAccion: pintarProfesionalesAccion,
        pintarTablaTareas: pintarTablaTareas,
        pintarTablaTareaAyuda: pintarTablaTareaAyuda,
        visualizarContenedor: visualizarContenedor,
        pintarProfesionales: pintarProfesionales,
        cebrear: cebrear,
        collapse: collapse,
        controlarScroll: controlarScroll,
        aperturaModal: aperturaModal,
        anadirFilaTarea: anadirFilaTarea,
        validarNumerico: validarNumerico,
        validarFormatoDecimal: validarFormatoDecimal,
        marcarLeido: marcarLeido,
        //comprobarDatosGrabar: comprobarDatosGrabar,
        anadirProfesionalesDblClick: anadirProfesionalesDblClick,
        anadirProfesionalesBtn: anadirProfesionalesBtn,
        buscarProfesionalesAsignados: buscarProfesionalesAsignados,
        marcarDesmarcarOrigen: marcarDesmarcarOrigen,
        marcarDesmarcarDestino: marcarDesmarcarDestino,
        modificarBDFila: modificarBDFila,
        marcarFila: marcarFila,
        eliminarFilaProfesionalesSel: eliminarFilaProfesionalesSel,
        desmarcarFilaProfesionalesEliminados: desmarcarFilaProfesionalesEliminados,
        eliminarTarea: eliminarTarea,
        deshacerCambiosTarea: deshacerCambiosTarea,
        pulsarSeleccionarTareas: pulsarSeleccionarTareas,
        pulsarCancelarTareas: pulsarCancelarTareas
    }
})()