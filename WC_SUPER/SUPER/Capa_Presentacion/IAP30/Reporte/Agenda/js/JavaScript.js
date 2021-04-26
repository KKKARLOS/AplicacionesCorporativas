$(document).ready(function () {      

    $('#calendar').fullCalendar({
        lang: 'es',
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
                title: "nuevo evento",
                start: start,
                end: end
            };

            
            $("#detalleAgenda #fechaInicio").val($.fullCalendar.moment(start).format("L"));
            $("#detalleAgenda #fechaFin").val($.fullCalendar.moment(start).format("L"));
            $("#detalleAgenda #horaInicio").val($.fullCalendar.moment(start).format("LT"));
            $("#detalleAgenda #horaFin").val($.fullCalendar.moment(end).format("LT"));
           
            $('#detalleAgenda').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#detalleAgenda').modal('show');
            $('.ocultable').attr('aria-hidden', 'true');
            $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
            $('#calendar').fullCalendar('unselect');
           
        },
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        events: [
            {
                title: 'Evento para todo el día',
                start: '2016-03-31'
            },
            {
                title: 'Presentación prototipos',
                start: '2016-01-07',
                end: '2016-01-10'
            },
            {
                id: 1,
                title: 'Reunión toma requisitos',
                start: '2016-04-04T11:30:00',
                end: '2016-04-04T13:00:00'
            },
            {
                id: 2,
                title: 'Reunión Seguimiento',
                start: '2016-04-04T08:30:00',
                end: '2016-04-04T10:00:00'
            },
            {
                id: 3,
                title: 'Evento repetido',
                start: '2016-04-05T16:00:00'
            },
            {
                title: 'Conferencia',
                start: '2016-04-06',
                end: '2016-04-06'
            }
          
        ],
        
        eventClick: function (event, element) {
            $("#detalleAgenda #fechaInicio").val($.fullCalendar.moment(event.start).format("L"));
            $("#detalleAgenda #fechaFin").val($.fullCalendar.moment(event.start).format("L"));
            $("#detalleAgenda #horaInicio").val($.fullCalendar.moment(event.start).format("LT"));
            $("#detalleAgenda #horaFin").val($.fullCalendar.moment(event.end).format("LT"));
            $("#detalleAgenda #asunto").val(event.title);

            $('#detalleAgenda').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#detalleAgenda').modal('show');
            $('.ocultable').attr('aria-hidden', 'true');
            $(document).on('hide.bs.modal', '#detalleAgenda', function () {
                event.title = $("#detalleAgenda #asunto").val();
                 $('#calendar').fullCalendar('updateEvent', event);

            });
        }        
    });

    //Para poder abrir un modal dentro de otro modal
    $(document).on('hidden.bs.modal', '.modal', function () {
        $('.modal:visible').length && $(document.body).addClass('modal-open');

    });

    //para que al cerrar el modal los elementos de la pantalla principal estén visibles
    $(document).on('hidden.bs.modal', '#detalleAgenda', function () {
        $('.ocultable').attr('aria-hidden', 'false');
    });

    $('#btnSalir').on('click', function () {
        location.href = '../Calendario/Default.aspx';
    });


    //Evento de abrir el modal de búsqueda de tareas
    $('#lblTarea').on('click', function () {
        $('#buscTarea').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#buscTarea').modal('show');
        $('.ocultable2').attr('aria-hidden', 'true');
    });

    $('#lblTarea').on('keypress', function (e) {
        if (e.which == 13) {
            $('#buscTarea').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#buscTarea').modal('show');
            $('.ocultable2').attr('aria-hidden', 'true');
        }
    });

    //para que al cerrar el modal los elementos de la pantalla principal estén visibles
    $(document).on('hidden.bs.modal', '#buscTarea', function () {
        $('.ocultable2').attr('aria-hidden', 'false');
    });

  
});

