$(document).ready(function () { SUPER.IAP30.Agenda.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Agenda = SUPER.IAP30.Agenda || {}

SUPER.IAP30.Agenda.app = (function (view) {

    //Booleano indicador MarcarDesmarcar
    var bMarcarDesmarcarProf = false;
    var bMarcarDesmarcarProfSelec = false;

    //Booleano indicador de cambios en el detalle de tarea
    var bCambiosEvento = false;

    /*Lógica de carga y visualización de datos*/
    window.onbeforeunload = function () {
        if (bCambiosEvento) 
            return "Si continúas perderás los cambios que no has guardado.";
    }

    var init = function () {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

        //Inicializa formateo
        accounting.settings = {
            number: {
                precision: 2,
                thousand: ".",
                decimal: ","
            }
        }

        //Manejador de fechas
        moment.locale('es');
        //Capturar la fecha enviada por parámetro
        //var date = moment(IB.vars.fechaCal, "MMYYYY");
        view.init();
        view.dom.agenda.fullCalendar({
            lang: 'es',
            locale: 'es',
            timezone: 'local',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            defaultView: 'agendaWeek',
            selectable: true,
            selectHelper: true,
            select: function (start, end) {

                var eventData = {
                    title: "",
                    start: start,
                    end: end
                };
                //Al ser un nuevo evento, si no se guarda en BBDD se perderá el evento en el gráfico de la agenda
                //bCambiosEvento = true;
                aperturaModalDetalleAgenda(eventData);
            },

            editable: false,
            eventLimit: true,
            events: function (start, end, timezone, callback) {

                var payload = {
                    fechaIni: start.format(),
                    fechaFin: end.format()
                }
                payload = JSON.stringify(payload);
                $.ajax({
                    url: IB.vars.strserver + "Capa_Presentacion/IAP30/Reporte/Agenda/Default.aspx/getCatalogoEventos",
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: payload,
                    success: function (doc) {
                        var events = [];
                        for (var i = 0; i < $(doc)[0].d.length; i++) {
                            events.push({
                                ID: $(doc)[0].d[i].ID,
                                title: $(doc)[0].d[i].Asunto,
                                start: $(doc)[0].d[i].StartTime, // will be parsed
                                end: $(doc)[0].d[i].EndTime
                            });
                        }

                        callback(events);
                        IB.procesando.ocultar();
                    },
                    error: function (ex, status) {
                        alert(ex.message + " status : " + status);
                        IB.bserror.error$ajax(ex, status);
                    }
                });
            },

            eventClick: function (event, element) {
                if ($(element.target).hasClass("btnDeleteEvent")) return;
                aperturaModalDetalleAgenda(event);
            },

            eventRender: function (event, element) {
                element.find(".fc-bg").css("pointer-events", "none");
                element.append("<div style='position:absolute;bottom:0px;right:0px;z-index:1000'><button type='button' class='btn btn-block btn-danger btn-flat btnDeleteEvent'>X</button></div>");
                element.find(".btnDeleteEvent").click(function () {

                    if (event.ID != undefined) {
                        view.attachEvents("click", view.dom.btnAceptarComentario, controlPrevioEliminarEvento);
                        view.aperturaModal(view.dom.modalComentario);
                        view.dom.idEventoEliminar.val(event.ID);
                        view.dom.idEventoAgenda.val(event._id);

                    }
                    //No se da el caso, siempre que se dibuje un evento en la agenda, será porque ya existe en BBDD
                    /*else {
                        $.when(IB.bsconfirm.confirm("warning", "Eliminar evento", "¿Estás seguro de querer eliminar el evento de tu agenda?"))
                       .then(function () {
                           iview.dom.agenda.fullCalendar('removeEvents', event._id);
                           
                       })
                    }*/

                });
            }

        });

        //Para cargar la agenda en una fecha determinada
        //view.dom.agenda.fullCalendar('gotoDate', date);

        view.visualizarContenedor();

        //Ayuda tareas
        var options = {
            titulo: "Selección de tarea",
            modulo: "IAP30",
            autoSearch: true,
            autoShow: false,
            searchParams: {
            },
            onSeleccionar: function (data) {
                darValor(view.dom.tarea, data.idTarea);
                darValor(view.dom.desTarea, data.desTarea);
                view.controlCamposObligatorios();
            },
            onCancelar: function () {
            }
        };

        view.dom.ayudaTarea.buscatarea(options);       
    }

    //Se atachan los eventos 
    view.attachEvents("hidden.bs.modal", view.dom.detalleAgenda, view.cierreModal);
    view.attachEvents("click", view.dom.btnVolver, regresoCalendario);
    

    var controlPrevioEliminarEvento = function () {
        if (view.dom.idEventoEliminar.val() != "") {
            if (view.dom.txtComentario.val() == "") IB.bsalert.toastdanger("Para eliminar un evento, es necesario indicar un motivo.");
            else {
                view.dom.modalComentario.modal("hide");
                $.when(IB.bsconfirm.confirm("warning", "Eliminar evento", "¿Estás seguro de querer eliminar el evento de tu agenda?"))
               .then(function () {                   
                   $.when(eliminarEvento(view.dom.idEventoEliminar.val(), view.dom.txtComentario.val())).then(function () {
                       view.dom.agenda.fullCalendar('removeEvents', view.dom.idEventoAgenda.val());
                       darValor(view.dom.txtComentario, "");
                    });                  

               })
            }
        }         
    }

    var initDetalleTarea = function (modoAcceso, accion) {

        
        //Si el modo acceso es de solo lectura, se deshabilitan todos los campos de la pantalla
        if (modoAcceso == "R") {
            view.desHabilitarInput(view.dom.detalleAgenda.find('input, textarea, button, select'));
            view.dom.btnSeleccionar.hide();
            view.dom.btnDesmarcarEliminados.hide();
            view.dom.btnEliminarSeleccionados.hide();
        }

        //Se atachan los eventos segun si estoy en modo grabacion o lectura
        if (modoAcceso == "W") // Si estoy en modo escritura atacho los eventos
        {
            view.habilitarInput(view.dom.detalleAgenda.find('input, textarea, button, select'));
            //Si es una actualización del evento, se deshbailitan los check de repetición
            if (accion == "U") {
                view.desHabilitarInput(view.dom.checkRepeticion);
                view.desHabilitarInput(view.dom.bloqueProf);
            }
            view.desHabilitarInput($('#prof'));
            view.desHabilitarInput($('#prom'));
            view.desHabilitarInput($('#desTarea'));
            view.dom.btnSeleccionar.show();
            view.dom.btnDesmarcarEliminados.show();
            view.dom.btnEliminarSeleccionados.show();
            //En caso de que el asunto tenga valor se quita el asterísco del campo de la tarea
            view.attachEvents("focusout", view.dom.asunto, view.controlCamposObligatorios);
            //Buscador de tareas
            view.attachEvents("click", view.dom.linkTarea, buscarTarea);
            view.attachEvents("keypress", view.dom.tarea, validarCampoNumerico);
            view.attachEvents("focusout", view.dom.tarea, validarTarea);

            //Control de cambios//Evento de cambios en los campos
            view.attachLiveEvents("keypress", view.selectores.sel_inputs, hayCambios);
            view.attachLiveEvents("change", view.selectores.sel_inputs, hayCambios);
            view.attachLiveEvents("change", view.selectores.sel_textarea, hayCambios);
            
            //Enlace eliminar seleccionados
            view.attachEvents("click", view.dom.btnEliminarSeleccionados, eliminarFilaProfesionalesSel);

            //Desmarcar eliminados
            view.attachEvents("click", view.dom.btnDesmarcarEliminados, desmarcarFilaProfesionalesEliminados);

            //Marcar fila profesionales seleccionados
            view.attachLiveEvents('click', view.selectores.filasProfesionalesSel, marcarFila);

            //Marcar fila profesionales a seleccionar
            view.attachLiveEvents('click', view.selectores.filasProfesionales, marcarFila);

            //Marcar-Desmarcar todos los elementos Origen
            view.attachEvents("click", view.dom.btnMarcarDesmarcarOrigen, marcarDesmarcarOrigen);

            //Marcar-Desmarcar todos los elementos Destino
            view.attachEvents("click", view.dom.btnMarcarDesmarcarDestino, marcarDesmarcarDestino);

            //Buscar profesionales por criterios de pantalla
            view.attachLiveEvents('keypress', view.selectores.buscarProfesionales, buscarProfesionales);

            //Buscar profesionales asignados
            view.attachEvents('keyup', view.dom.buscAsignados, buscarProfesionalesAsignados);

            //Añadir elementos seleccionados a través de botón
            view.attachEvents("click", view.dom.btnSeleccionar, anadirProfesionalesBtn);

            //Añadir elementos seleccionados a través de doble click
            view.attachLiveEvents("dblclick", view.selectores.filasProfesionales, anadirProfesionalesDblClick);            
        }

        view.habilitarInput(view.dom.btnCancelar);
        view.habilitarInput(view.dom.btnCierreModalEvento);
        //Click en el botón Cancelar de la modal de "Detalle de evento"
        view.attachEvents("click", view.dom.btnCancelar, controlarCambios);
        view.attachEvents("click", view.dom.btnCierreModalEvento, controlarCambios);       
        //Click en el botón Aceptar de la modal de "Detalle de evento"
        view.attachEvents("click", view.dom.btnAceptar, guardarDatosEvento);

    }
    var validarCampoNumerico = function (e) {
        if (e.which == 13) validarTarea(e);
        else if (e.which != 13 && e.which != 37 && e.which != 38 && e.which != 39 && e.which != 40) {
            if (validarTeclaNumerica(e, false)) {
                view.limpiarDatosTarea();
            } else return false;
        }        
    }
    
    validarTarea = function (e) {
        if (view.obtenerValor(view.dom.tarea) != "") {

            var filtro = { idFicepiProf: 0, idTarea: view.obtenerValor(view.dom.tarea) };
            IB.procesando.mostrar();
            IB.DAL.post(null, "validarTarea", filtro, null,
                function (data) {
                    if (data != null) {
                        objTarea = data;
                        view.pintarDatosTarea(objTarea);
                    } else {
                         $.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", "La tarea indicada no existe, su estado no lo permite o no está asociada al profesional."))
                        .then(function () { view.asignarFoco(view.dom.tarea); return false; });
                    }
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    $.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener la tarea (" + view.obtenerValor(view.dom.tarea) + ")."))
                    .then(function () { view.asignarFoco(view.dom.tarea) });
                    IB.procesando.ocultar();
                    return false;
                }
            );
        } else {
            view.limpiarDatosTarea();
        }

        view.controlCamposObligatorios();
    }
   
    var aperturaModalDetalleAgenda = function (evento) {
        var modoAcceso = "W";
        view.asignarControlDateRangePicker($.fullCalendar.moment(evento.start).format('DD/MM/YYYY h:mm A'), $.fullCalendar.moment(evento.end).format('DD/MM/YYYY h:mm A'));

        //Se trata de un evento existente, por lo que hace falta obtener el detalle
        if (evento.ID != "" && evento.ID != undefined) {

            $.when(obtenerDatosEvento(evento)).then(function () {
                bCambiosEvento = false;
                view.aperturaModal(view.dom.detalleAgenda);
            });
            if (evento.start < moment()) modoAcceso = "R";
            initDetalleTarea(modoAcceso, "U");
        } else {
            bCambiosEvento = false;
            view.darValor(view.dom.prof, IB.vars.desProfesional);
            view.darValor(view.dom.prom, IB.vars.desPromotor);
            view.aperturaModal(view.dom.detalleAgenda);
            initDetalleTarea(modoAcceso, "I");
        }
            
    }

    obtenerDatosEvento = function (evento) {
        var defer = $.Deferred();
        if (evento.ID != "") {

            var filtro = { idEvento: evento.ID };
            IB.procesando.mostrar();
            IB.DAL.post(null, "getDetalleEvento", filtro, null,
                function (data) {
                    if (data != null) {
                        objEvento = data;
                        objEvento.idEvento = evento._id;
                        view.pintarDetalleEvento(objEvento);
                    } else IB.bsalert.fixedAlert("danger", "Error de aplicación", "No se han obtenido datos del evento (" + objEvento.Motivo + ").");
                    defer.resolve();
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener el evento.");
                    defer.reject();
                    IB.procesando.ocultar();
                }
            );
        }
        return defer.promise();
    }


    var guardarDatosEvento = function (e) {
        var opt = {
            delay: 1,
            hide: 1
        }

        if (!validarCampos()) return false;

        IB.procesando.opciones(opt);
        IB.procesando.mostrar();

        var diasSemana = new Array();
        diasSemana[0] = view.radioCheckeado(view.dom.checkboxes0) ? true : false;
        diasSemana[1] = view.radioCheckeado(view.dom.checkboxes1) ? true : false;
        diasSemana[2] = view.radioCheckeado(view.dom.checkboxes2) ? true : false;
        diasSemana[3] = view.radioCheckeado(view.dom.checkboxes3) ? true : false;
        diasSemana[4] = view.radioCheckeado(view.dom.checkboxes4) ? true : false;
        diasSemana[5] = view.radioCheckeado(view.dom.checkboxes5) ? true : false;
        diasSemana[6] = view.radioCheckeado(view.dom.checkboxes6) ? true : false;
        var eventoBBDD = {
            title: view.dom.asunto.val(),
            start: view.dom.rangoFechas.data('daterangepicker').startDate,
            end: view.dom.rangoFechas.data('daterangepicker').endDate,
            ASUNTO: view.dom.asunto.val(),
            FECHAMOD: moment(),
            STARTTIME : view.dom.rangoFechas.data('daterangepicker').startDate,
            ENDTIME : view.dom.rangoFechas.data('daterangepicker').endDate,
            PROMOTOR : view.obtenerValor(view.dom.prom),
            PROFESIONAL : view.obtenerValor(view.dom.prof),
            ASUNTO : view.obtenerValor(view.dom.asunto),
            MOTIVO : view.obtenerValor(view.dom.motivo),
            OBSERVACIONES : view.obtenerValor(view.dom.observaciones),
            PRIVADO : view.obtenerValor(view.dom.privado),
            IDTAREA: (view.obtenerValor(view.dom.tarea) == "") ? null : view.obtenerValor(view.dom.tarea),
            DESTAREA: view.obtenerValor(view.dom.desTarea),
            DIASSEMANA: diasSemana,
        }

        //Si el evento tiene asignado un idEvento en el control fullcalendar querra decir que el evento existe y
        //se están haciendo modificaciones sobre el
        if (view.dom.idEventoCal.val() != "" && view.dom.idEventoCal.val() != undefined) {
            var evento = view.dom.agenda.fullCalendar('clientEvents', view.dom.idEventoCal.val())[0];           
            eventoBBDD.ID = evento.ID;
            eventoBBDD.IDTAREA = (view.obtenerValor(view.dom.tarea) == "") ? -1 : view.obtenerValor(view.dom.tarea);

            var filtro = { evento: eventoBBDD, otrosProfesionales: null,  confirmarBorrado: false};
            var promise1 = IB.DAL.post(null, "grabarEvento", filtro, null);

            $.when(promise1).then(function () {
                evento.title = eventoBBDD.title;
                evento.start = eventoBBDD.start;
                evento.end = eventoBBDD.end;

                view.dom.agenda.fullCalendar('updateEvent', evento);
                view.dom.detalleAgenda.modal("hide");
                IB.bsalert.toast('La actualización del evento se ha realizado correctamente.');

            });
           
        //Si no, será un nuevo evento que habrá que insertar en BBDD y añadir en el fullcalendar
        } else {
            var aOtrosProfesionales = new Array();
            aOtrosProfesionales = getProfesionalesAsignados();
            var filtro = { evento: eventoBBDD, otrosProfesionales: aOtrosProfesionales, confirmarBorrado: false };
            var promise1 = IB.DAL.post(null, "grabarEvento", filtro, null);
           
            $.when(promise1).then(function (data) {
                var aDatos = data.split("//");

                //La grabación no ha devuelto error, pero el evento no se ha insertado porque hay eventos que lo solapan
                if (data == -1) {
                    $.when(IB.bsconfirm.confirm("warning", "Grabación de evento", "¡¡ Atención !! Se han detectado solapamientos de citas creadas por responsables.¿ Deseas borrarlas ?"))
                       .then(function () {
                           var filtro2 = { evento: eventoBBDD, otrosProfesionales: aOtrosProfesionales, confirmarBorrado: true };
                           var promise2 = IB.DAL.post(null, "grabarEvento", filtro2, null);
                           $.when(promise2).then(function (data2) {
                               eventoBBDD.ID = data2;
                               view.dom.agenda.fullCalendar('refetchEvents');
                               view.dom.detalleAgenda.modal("hide");
                               IB.bsalert.toast('La grabación del evento se ha realizado correctamente.');
                           });
                       });
               
                }else if (aDatos[0] == "0"){
                    //La grabación para el profesional se ha realizado correctamente, aunque en este caso ha habido profesionales asignados que tenían eventos que solapaban a este
                    var msg; 
                    if (eventoBBDD.IDTAREA == "") msg = "La planificación para el profesional ha sido realizada con éxito. Sin embargo, ha existido solapamiento en alguna de las planificaciones para otros profesionles, lo que ha impedido su realización. Dichos profesionales se encuentran marcados en rojo.";
                    else msg = "La planificación para el profesional ha sido realizada con éxito. Sin embargo, ha existido solapamiento en alguna de las planificaciones para otros profesionles, o no están asociados a la tarea, lo que ha impedido su realización. Dichos profesionales se encuentran marcados en rojo.";

                    IB.bsalert.fixedAlert("info", "Resultado de guardar el evento", msg);
                    $(view.selectores.filasProfesionalesSel).each(function () {
                        if (aDatos[1].indexOf(($(this).attr('data-red').split("//")[0])) >= 0) $(this).addClass("aviso");

                    });

                    bCambiosEvento = false;
                    view.dom.agenda.fullCalendar('refetchEvents');
                    view.desHabilitarInput(view.dom.btnAceptar);
                
                } else{//La grabación se harealizado correctamente
                    eventoBBDD.ID = data;                    
                    view.dom.agenda.fullCalendar('refetchEvents');
                    view.dom.detalleAgenda.modal("hide");
                    IB.bsalert.toast('La grabación del evento se ha realizado correctamente.');
                }               

                IB.procesando.ocultar();
                
            });           
        }        
       
        IB.procesando.ocultar();
        
    }

    var getProfesionalesAsignados = function () {
        var listaCodRedAsignados = new Array();

        $(view.selectores.filasProfesionalesSel).each(function () {
            listaCodRedAsignados.push($(this).attr('data-red'));
        });

        return listaCodRedAsignados;
    }

    var validarCampos = function (e) {

        if (view.dom.asunto.val() == "" && view.dom.tarea.val() == "") {
            IB.bsalert.toastdanger("Es obligatorio indicar un asunto o una tarea");
            if(view.dom.asunto.val() == "") view.asignarFoco(view.dom.asunto);
            else view.asignarFoco(view.dom.idTarea);
            return false;
        } else if (view.dom.tarea.val() != "" && view.dom.desTarea.val() == "") {
            IB.bsalert.toastdanger("Es obligatorio introducir una tarea válida");
            return false;
        }

        var fechaDesde = view.dom.rangoFechas.data('daterangepicker').startDate;
        var fechaHasta = view.dom.rangoFechas.data('daterangepicker').endDate;

        var horaDesde = view.dom.rangoFechas.data('daterangepicker').startTime;
        var horaHasta = view.dom.rangoFechas.data('daterangepicker').endTime;

        if(fechaDesde <= moment()){
            IB.bsalert.toastdanger("La fecha de inicio debe ser posterior al día actual.");
            view.asignarFoco(view.dom.rangoFechas);
            return false;
        }
        if ((fechaDesde.format('DD/MM/YYYY') != fechaHasta.format('DD/MM/YYYY')) && diasRepeticion() <0) {
            IB.bsalert.toastdanger("No se permiten citas con diferente fecha de inicio y fin sin indicar días de repetición.");
            view.asignarFoco(view.dom.rangoFechas);
            return false;
        }

        if (fechaHasta <= fechaDesde) {
            IB.bsalert.toastdanger("El fin del rango temporal debe ser posterior al inicio.");
            view.asignarFoco(view.dom.rangoFechas);
            return false;
        }

        return true;
    }

    var diasRepeticion = function () {
        var diasSemana = new Array();
        diasSemana[0] = view.radioCheckeado(view.dom.checkboxes0) ? true : false;
        diasSemana[1] = view.radioCheckeado(view.dom.checkboxes1) ? true : false;
        diasSemana[2] = view.radioCheckeado(view.dom.checkboxes2) ? true : false;
        diasSemana[3] = view.radioCheckeado(view.dom.checkboxes3) ? true : false;
        diasSemana[4] = view.radioCheckeado(view.dom.checkboxes4) ? true : false;
        diasSemana[5] = view.radioCheckeado(view.dom.checkboxes5) ? true : false;
        diasSemana[6] = view.radioCheckeado(view.dom.checkboxes6) ? true : false;

        return jQuery.inArray(true, diasSemana);
    }


    
    //Control de cambios en la pantalla antes de cierre
    var controlarCambios = function (e) {
        IB.procesando.mostrar();
        var defer = $.Deferred();

        if (bCambiosEvento) {
            IB.procesando.ocultar();
            $.when(IB.bsconfirm.confirmCambios())
                .then(function () {
                    defer.resolve();
                    bCambiosEvento = false;                    
                    view.dom.detalleAgenda.modal("hide");
                })

        } else {
            defer.resolve();            
            view.dom.detalleAgenda.modal("hide");
            IB.procesando.ocultar();
        }

        return defer.promise();
    }

    //Identificación de cambios en la pantalla
    var hayCambios = function () {
        if (!bCambiosEvento) {
            bCambiosEvento = true;
            //view.dom.btnAceptar.removeAttr("disabled");
        }
    }

    function regresoCalendario() {
        location.href = '../Calendario/Default.aspx?or=bWVudQ==';
    }

    var buscarTarea = function (e) {
        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();
        var o = {
            tipoBusqueda: "tareasAgendaEnBloque",
            fechaInicio: null,
            fechaFin: null
        }
        $(".fk_ayudaTarea").buscatarea("option", "searchParams", o);
        $(".fk_ayudaTarea").buscatarea("show");

    }

    //Para evitar el submit del formulario que por defecto se hace al dar intro dentro un html input

    /*$('input[type="text"]').keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });*/

    var buscarProfesionalesAsignados = function (e) {
        $.each($(view.selectores.buscarCadenaProfesionalesSel), function (e) {
            if ($(this).text().toLowerCase().replace(/\s+/g, '').indexOf(view.dom.buscAsignados.val().replace(/\s+/g, '').toLowerCase()) == -1)
                $(this).parent().hide();
            else
                $(this).parent().show();
        });
    }
    var marcarDesmarcarOrigen = function (e) {
       
        if ($(view.selectores.filasProfesionales).length > 0) {
            if ($(view.selectores.divProfesionales + ' .glyphicon').hasClass('glyphicon-unchecked')) {
                $(view.selectores.divProfesionales + ' .glyphicon').removeClass('glyphicon-unchecked');
                $(view.selectores.divProfesionales + ' .glyphicon').addClass('glyphicon-check');
            } else {
                $(view.selectores.divProfesionales + ' .glyphicon').removeClass('glyphicon-check');
                $(view.selectores.divProfesionales + ' .glyphicon').addClass('glyphicon-unchecked');
            }
        }
                

        $.each($(view.selectores.filasProfesionales), function (e) {
            if (bMarcarDesmarcarProf) {
                $(this).removeClass('selected');
            }
            else {
                $(this).removeClass('selected').addClass('selected');
            }
        });
        if (bMarcarDesmarcarProf) bMarcarDesmarcarProf = false;
        else bMarcarDesmarcarProf = true;
    }

    var marcarDesmarcarDestino = function (e) {

        if ($(view.selectores.filasProfesionalesSel).length > 0) {
            if ($(view.selectores.divProfesionales2 + ' .glyphicon').hasClass('glyphicon-unchecked')) {
                $(view.selectores.divProfesionales2 + ' .glyphicon').removeClass('glyphicon-unchecked');
                $(view.selectores.divProfesionales2 + ' .glyphicon').addClass('glyphicon-check');
            } else {
                $(view.selectores.divProfesionales2 + ' .glyphicon').removeClass('glyphicon-check');
                $(view.selectores.divProfesionales2 + ' .glyphicon').addClass('glyphicon-unchecked');
            }
        }

        $.each($(view.selectores.filasProfesionalesSel), function (e) {
            if (bMarcarDesmarcarProfSelec) {
                $(this).removeClass('selected');
            }
            else {
                $(this).removeClass('selected').addClass('selected');
            }
        });
        if (bMarcarDesmarcarProfSelec) bMarcarDesmarcarProfSelec = false;
        else bMarcarDesmarcarProfSelec = true;
    }


    var anadirProfesionalesDblClick = function (e) {
        var bInsertar = true;
        var sProfesional = $(this).text().toLowerCase();
        $.each($(view.selectores.buscarCadenaProfesionalesSel), function (i, item) {
            if (sProfesional.replace(/\s+/g, '').indexOf($(item).text().replace(/\s+/g, '').toLowerCase()) != -1)
                bInsertar = false;
        })

        if (bInsertar) {
            var tr = $(this).closest("tr").clone();
            tr.removeClass("selected");
            tr.attr("data-bd", "I");
            //tr.children().eq(0).after("<td class='text-center'><input type='checkbox' class='notificable' style='width:20px; margin-top:0px;'></td>")
            $(view.dom.tablaProfesionalesSeleccionados).append(tr);
        }
        view.cebrear();
    };

    var anadirProfesionalesBtn = function (e) {
        $.each($(view.selectores.filasSeleccionadasProfesionales), function (i, item) {
            //controlar si ya existe en la tabla destino antes de insertarlo
            var bInsertar = true;
            $.each($(view.selectores.buscarCadenaProfesionalesSel), function (e) {
                if ($(this).text().toLowerCase().replace(/\s+/g, '').indexOf($(item).children().eq(0).text().replace(/\s+/g, '').toLowerCase()) != -1)
                    bInsertar = false;
            })

            if (bInsertar) {
                var tr = $(this).closest("tr").clone();
                tr.removeClass("selected");
                tr.attr("data-bd", "I");
                //tr.children().eq(0).after("<td class='text-center'><input type='checkbox' class='notificable' style='width:20px; margin-top:0px;'></td>")
                $(view.dom.tablaProfesionalesSeleccionados).append(tr);
            }
        });
        view.cebrear();
        //event.stopPropagation();
        return false;
    }
    var modificarBDFila = function (e) {
        if (IB.vars.permiso == "L") return false;
        if ($(this).parent().parent().attr("data-bd") == "") $(this).parent().parent().attr("data-bd", "U");
    }

    //Marcar para borrar en profesionales seleccionados las filas seleccionadas si existe o eliminar de la tabla si no existe
    var eliminarFilaProfesionalesSel = function (e) {
        if ($(view.selectores.divProfesionales2 + ' .glyphicon').hasClass('glyphicon-check')) {
            $(view.selectores.divProfesionales2 + ' .glyphicon').removeClass('glyphicon-check');
            $(view.selectores.divProfesionales2 + ' .glyphicon').addClass('glyphicon-unchecked');
        } 

        $.each($(view.selectores.filasSeleccionadasProfesionalesSel), function (e) {
            if ($(this).attr("data-bd") != "I") {
                $(this).attr("data-bd", "D");
                $(this).children().eq(0).removeClass("desmarcarParaBorrar").addClass("marcadoParaBorrar");
            }
            else $(this).closest('tr').remove();
        });
        //event.stopPropagation();
        return false;
    }

    var desmarcarFilaProfesionalesEliminados = function (e) {
        $.each($(view.selectores.filasProfesionalesSel), function (e) {
            if ($(this).attr("data-bd") == "D") {
                $(this).attr("data-bd", "");
                $(this).children().eq(0).removeClass("marcadoParaBorrar").addClass("desmarcarParaBorrar");
            }
        });
        //event.stopPropagation();
        return false;
    }

    var marcarFila = function (e) {
        var oElement = e.srcElement ? e.srcElement : e.target;
        oFila = $(oElement).closest("tr");

        if (oFila.hasClass('selected')) {
            oFila.removeClass('selected');
        }
        else {
            oFila.removeClass('selected');
            oFila.addClass('selected');
        }
    }

    buscarProfesionales = function (event) {
        if (event.keyCode != 13) return;
        if (view.dom.Apellido1.val() == "" && view.dom.Apellido2.val() == "" && view.dom.Nombre.val() == "") {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No se permite realizar búsquedas con los filtros vacíos");
            return false;
        }
        
        //event.stopPropagation();

        var defer = $.Deferred();
        var payload = { tipoBusqueda: "PROFESIONALES_AGENDA", nombre: view.dom.Nombre.val(), apellido1: view.dom.Apellido1.val(), apellido2: view.dom.Apellido2.val(), bajas: false, nodo: "" };

        try {
            IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Services/BuscadorPersonas.asmx", "ObtenerProfesionales", payload, null,
         
                function (data) {
                    view.pintarProfesionales(data);
                    IB.procesando.ocultar();
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error", "Error al obtener los datos de profesionales.");
                    defer.fail();
                }
            );
        } catch (e) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
        }
        return defer.promise();
    }

    function eliminarEvento(idEvent, motivoEliminacion) {
        var defer = $.Deferred();
        IB.procesando.mostrar();
        
        var filtro = { idEvento: idEvent };
        var promise1 = IB.DAL.post(null, "getDetalleEvento", filtro, null);
           
        $.when(promise1).then(function (data) {
            var eventoBBDD = {
                ID:data.ID,
                ASUNTO: data.Asunto,
                FECHAMOD: moment(data.FechaMod),
                STARTTIME: moment(data.StartTime),
                ENDTIME: moment(data.EndTime),
                PROMOTOR: data.Promotor,
                PROFESIONAL: data.Profesional,
                MOTIVO: data.Motivo,
                DESTAREA: data.DesTarea,
                OBSERVACIONES: data.Observaciones,
                MOTIVOELIMINACION: motivoEliminacion
            }
            var payload = { evento: eventoBBDD, enviarMail: true, solapamiento: false };
            IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Reporte/Agenda/Default.aspx", "eliminarEvento", payload, null,
                function () {
                    defer.resolve();
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bserror.error$ajax(ex, status);
                    defer.fail();
                }
            );
        });
        return defer;
        
    }

    return {
        init: init
    }


})(SUPER.IAP30.Agenda.view);