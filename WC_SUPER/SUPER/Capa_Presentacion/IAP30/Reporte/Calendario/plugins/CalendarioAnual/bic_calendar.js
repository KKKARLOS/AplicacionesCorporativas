/*
 *
 * bic calendar
 * Autor: bichotll
 * Web-autor: bic.cat
 * Web script: http://bichotll.github.io/bic_calendar/
 * Llicència Apache
 *
 */

$.fn.bic_calendar = function(options) {

    var opts = $.extend({}, $.fn.bic_calendar.defaults, options);



    this.each(function() {


        /*** vars ***/


        //element called
        var elem = $(this);

        var calendar;
        var layoutMonth;
        var daysMonthsLayer;
        var textMonthCurrentLayer = $('<div class="visualmonth"></div>');
        var textYearCurrentLayer = $('<div class="visualyear"><span class="monthAndYear span6"></span></div>');

        var calendarId = "bic_calendar";

        var events = opts.events;

        var dayNames;
        if (typeof opts.dayNames != "undefined")
            dayNames = opts.dayNames;
        else
            dayNames = ["l", "m", "x", "j", "v", "s", "d"];

        var monthNames;
        if (typeof opts.monthNames != "undefined")
            monthNames = opts.monthNames;
        else
            monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];

        var showDays;
        if (typeof opts.showDays != "undefined")
            showDays = opts.showDays;
        else
            showDays = true;

        var popoverOptions;
        if (typeof opts.popoverOptions != "undefined")
            popoverOptions = opts.popoverOptions;
        else
            popoverOptions = {placement: 'bottom', html: true, trigger: 'hover'};

        var tooltipOptions;
        if (typeof opts.tooltipOptions != "undefined")
            tooltipOptions = opts.tooltipOptions;
        else
            tooltipOptions = {placement: 'bottom', trigger: 'hover'};

        var reqAjax;
        if (typeof opts.reqAjax != "undefined")
            reqAjax = opts.reqAjax;
        else
            reqAjax = false;

        var enableSelect = false;
        if (typeof opts.enableSelect != 'undefined')
            enableSelect = opts.enableSelect;

        var multiSelect = false;
        if (typeof opts.multiSelect != 'undefined')
            multiSelect = opts.multiSelect;

        var firstDaySelected = '';
        var lastDaySelected = '';
        var daySelected = '';

        /*** --vars-- ***/


        //Pintar los festivos según la semana laboral
        var aSemanaLab = IB.vars.aSemLab.split(",");
        var aFestivosSemana = new Array;
        var x = 0;
        for (var i = 0; i < aSemanaLab.length; i++) {
            if (aSemanaLab[i] == "0") {
                aFestivosSemana[x] = i;
                x++;
            }
        }


        /*** functions ***/

        /**
         * init n print calendar
         */
        function showCalendar() {

            //layer with the days of the month (literals)
            daysMonthsLayer = $('<div id="monthsLayer" class="row"></div>');

            //Date obj to calc the day
            var objFecha = new Date();
            objFecha.setMonth(0);

            //current year
            var year = objFecha.getFullYear();

            //show the days of the month n year configured
            showMonths(year);

            //next-previous year controllers
            var nextYearButton = $('<span role="link" class="button-year-next" aria-label="Año siguiente" tabindex="0"></span>');
            //event
            nextYearButton.click(function(e) {
                e.preventDefault();
                year++;
                changeDate(year, objFecha.getFullYear());
            })
            var previousYearButton = $('<span role="link" class="button-year-previous" aria-label="Año anterior" tabindex="0"></span>');
            //event
            previousYearButton.click(function(e) {
                e.preventDefault();
                year--;
                changeDate(year, objFecha.getFullYear());
            })


            //show the current year n current month text layer
            var headerLayer = $('<div class="table header"></div>');
            /*var yearControlTextLayer = $('<span class="monthAndYear span6"></span>');*/

            headerLayer.append(previousYearButton);
            headerLayer.append(textYearCurrentLayer);
            headerLayer.append(nextYearButton);
            /*yearControlTextLayer.append(textYearCurrentLayer);*/

            
            //calendar n border
            calendar = $('<div class="bic_calendar" id="' + calendarId + '" ></div>');
            calendar.prepend(headerLayer);

            calendar.append(daysMonthsLayer);

            //insert calendar in the document
            elem.append(calendar);

            //check and add events
            checkEvents(year);
            //if enable select
            checkIfEnableMark();
        }

        /**
        * Obtiene el detalle horario de un calendario en un determinado año y vuelca la información a un Map. 
        * Despues llama al pintado de meses pasándole el map
        */

        function cargarDatosCalendario(year) {
            ocultarCalendario();
            var opt = {
                hide:1
            }
            IB.procesando.opciones(opt);
            IB.procesando.mostrar();
            var payload = { idCal: IB.vars.codCal, iAnno: year };
            try {
                IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Reporte/Calendario/Default.aspx", "getHorasCalendario", payload, null,
                    function (data) {                        
                        $.when(crearTablaDiasAnno(year, data))
                            .then(function () {
                                IB.procesando.ocultar();
                                visualizarCalendario();
                            });
                    },
                    function (ex, status) {
                        IB.bserror.error$ajax(ex, status)
                    }
             );
            } catch (e) {
                IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
            }
        }
        
        //Vuelca la información del detalle horario a un map <(string) diaMes, (objeto) infoDia> 
        function crearTablaDiasAnno(year, data) {
            var defer = $.Deferred();
            var tablaDiasAnno = new Map();
            var dateDia = null;
            if (data != null && data.length > 0) {
                $(data).each(function () {
                    dateDia = new Date(moment(this.t067_dia));
                    tablaDiasAnno.set(dateDia.toString(), this);

                });
            }
            pintarMonths(year, tablaDiasAnno);
            return defer.resolve();
        }

        function visualizarCalendario() {
            $('#bic_calendar').css("visibility", "visible");
        }

        function ocultarCalendario() {
            $('#bic_calendar').css("visibility", "hidden");
        }
        
        function obtenerDatosDia(tablaDiasAnno, fechaMes) {
            var objDia = null;
            
            if (tablaDiasAnno != null) {
                objDia = tablaDiasAnno.get(fechaMes.toString());
            }
            return objDia;
        }
        

        /**
         * indeed, change month or year
         */
        function changeDate(year, currentYear) {
            var data = null;
            daysMonthsLayer.empty();
            showMonths(year);
            checkEvents(year);
            markSelectedDays();            
        }

        /**
         * show literals of the week
         */
        function listListeralsWeek() {
            if (showDays != false) {
                var capaDiasSemana = $('<tr class="days-month" >');
                var codigoInsertar = '';
                $(dayNames).each(function(indice, valor) {
                    codigoInsertar += '<th tabindex="0"';
                    if (indice == 0) {
                        codigoInsertar += ' class="primero" title="Lunes"';
                    } else if (indice == 1) {
                        codigoInsertar += ' title="Martes"';
                    } else if (indice == 2) {
                        codigoInsertar += ' title="Miércoles"';
                    } else if (indice == 3) {
                        codigoInsertar += ' title="Jueves"';
                    } else if (indice == 4) {
                        codigoInsertar += ' title="Viernes"';
                    } else if (indice == 5) {
                        codigoInsertar += ' title="Sábado"';
                    }else if (indice == 6) {
                        codigoInsertar += ' class="domingo ultimo" title="Domingo"';
                    }
                    codigoInsertar += ">" + valor + '</th>';
                });
                codigoInsertar += '</tr>';
                capaDiasSemana.append(codigoInsertar);

                layoutMonth.append(capaDiasSemana);
            }
        }

        /**
         * print months
         */
        function showMonths(year) {
            var objFecha = new Date();
            var currentYear = objFecha.getFullYear();
            //if (currentYear >= year || (year-1 == currentYear)) cargarDatosCalendario(year);
            //else pintarMonths(year, null);
            cargarDatosCalendario(year);
            
        }

        function pintarMonths(year,tablaDiasAnno) {            

            for (i = 0; i != 12; i++) {
                if (i % 4 == false)
                    daysMonthsLayer.append($('</div><div class="row">'));
                showMonthDays(i, year, tablaDiasAnno);
            }
        }

        /**
         * show the days of the month
         */
        function showMonthDays(month, year, tablaDiasAnno) {
            
            //layoutMonth
            layoutMonth = $('<table tabindex="0" class="table" title="'+monthNames[month]+'"></table>');

            //print year n month in layers
            textMonthCurrentLayer.text(monthNames[month]);
            textYearCurrentLayer.text(year);

            //show days of the month
            var daysCounter = 1;

            //calc the date of the first day of this month
            var firstDay = calcNumberDayWeek(1, month, year);

            //calc the last day of this month
            var lastDayMonth = lastDay(month, year);

            var nMonth = month + 1;

            var daysMonthLayerString = "";
            var diaMes = null;
            var dateDiaMes = null

            
            //print the first row of the week
            var horasEsfuerzo;
            for (var i = 0; i < 7; i++) {
                if (i < firstDay) {
                    var dayCode = "";
                    if (i == 0)
                        dayCode += "<tr>";
                    //add weekDay
                    dayCode += '<td  class="invalid-day week-day-' + i + '"';
                    dayCode += '"></td>';
                } else {
                    var dayCode = "";
                    if (i == 0)
                        dayCode += '<tr>';

                    //dayCode += '<td tabindex="0" title="Día: ' + daysCounter + "/" + nMonth + "/" + year + '. Horas:8,5 (festivo)" id="' + calendarId + '_' + daysCounter + "_" + nMonth + "_" + year + '" data-date="' + nMonth + "/" + daysCounter + "/" + year + '" ';
                    dateDiaMes = new Date(year, nMonth-1, daysCounter);
                    diaMes = obtenerDatosDia(tablaDiasAnno, dateDiaMes);
                    if (diaMes != null) {

                        horasEsfuerzo = diaMes.t067_horasD;
                        if ((aFestivosSemana.indexOf(i) != -1) || diaMes.t067_festivo == 1) {
                            dayCode += '<td tabindex="0" title="D&iacute;a: ' + daysCounter + "/" + nMonth + "/" + year + ' (festivo). Horas: ' + horasEsfuerzo + '." id="' + calendarId + '_' + daysCounter + "_" + nMonth + "_" + year + '"';
                            //add weekDay
                            dayCode += ' class="week-day-' + i + '"';

                            dayCode += '><div class="diaFestivo"><a>' + daysCounter + '</a></div><div class="monthly-time-number"><span>' + horasEsfuerzo + '</span></div></span>';

                        } else {
                            dayCode += '<td tabindex="0"';
                            //add weekDay
                            dayCode += ' class="week-day-' + i + '"';
                            dayCode += '><div><a>' + daysCounter + '</a></div><div class="monthly-time-number"><span>' + horasEsfuerzo + '</span></div></span>';
                        }

                    } else {
                        if (aFestivosSemana.indexOf(i) != -1) {
                            dayCode += '<td tabindex="0" title="D&iacute;a: ' + daysCounter + "/" + nMonth + "/" + year + '. (festivo)"';
                            //add weekDay
                            dayCode += ' class="week-day-' + i + '"';

                            dayCode += '><div class="diaFestivo"><a>' + daysCounter + '</a></div>';

                        } else {
                            dayCode += '<td tabindex="0" title="D&iacute;a: ' + daysCounter + "/" + nMonth + "/" + year + '."';
                            //add weekDay
                            dayCode += ' class="week-day-' + i + '"';
                            dayCode += '><div><a>' + daysCounter + '</a></div>';
                        }
                    }                   
                    if (i == 6)
                        dayCode += '</tr>';
                    daysCounter++;
                }
                daysMonthLayerString += dayCode
            }
			
			

            //check all the other days until end of the month
            var currentWeekDay = 1;
            while (daysCounter <= lastDayMonth) {
                var dayCode = "";
                if (currentWeekDay % 7 == 1)
                    dayCode += "<tr>";

                dateDiaMes = new Date(year, nMonth - 1, daysCounter);
                diaMes = obtenerDatosDia(tablaDiasAnno, dateDiaMes);
                if (diaMes != null) {

                    horasEsfuerzo = diaMes.t067_horasD;
                    if (((aFestivosSemana.indexOf(((currentWeekDay - 1) % 7)) != -1)) || diaMes.t067_festivo == 1) {
                        dayCode += '<td tabindex="0" title="D&iacute;a: ' + daysCounter + '/' + nMonth + '/' + year + ' (festivo). Horas: ' + horasEsfuerzo + '." id="' + calendarId + '_' + daysCounter + '_' + nMonth + '_' + year + '"';
                        //add weekDay
                        dayCode += ' class="week-day-' + ((currentWeekDay - 1) % 7) + '"';

                        dayCode += '><div class="diaFestivo"><a>' + daysCounter + '</a></div><div class="monthly-time-number"><span>' + horasEsfuerzo + '</span></div></span>';
                    } else {
                        dayCode += '<td tabindex="0" title="D&iacute;a: ' + daysCounter + '/' + nMonth + '/' + year + '. Horas: ' + horasEsfuerzo + '." id="' + calendarId + '_' + daysCounter + '_' + nMonth + '_' + year + '"';
                        //add weekDay
                        dayCode += ' class="week-day-' + ((currentWeekDay - 1) % 7) + '"';

                        dayCode += '><div><a>' + daysCounter + '</a></div><div class="monthly-time-number"><span>' + horasEsfuerzo + '</span></div></span>';
                    }
                } else {

                    if ((aFestivosSemana.indexOf(((currentWeekDay - 1) % 7)) != -1)) {
                        dayCode += '<td tabindex="0" title="D&iacute;a: ' + daysCounter + '/' + nMonth + '/' + year + ' (festivo)."';
                        //add weekDay
                        dayCode += ' class="week-day-' + ((currentWeekDay - 1) % 7) + '"';
                    
                        dayCode += '><div class="diaFestivo"><a>' + daysCounter + '</a></div>';
                    } else {
                        dayCode += '<td tabindex="0" title="D&iacute;a: ' + daysCounter + '/' + nMonth + '/' + year + '."';
                        //add weekDay
                        dayCode += ' class="week-day-' + ((currentWeekDay - 1) % 7) + '"';
                    
                        dayCode += '><div><a>' + daysCounter + '</a></div></td>';
                    
                    }

                }
                
                if (currentWeekDay % 7 == 0)
                    dayCode += "</tr>";
                daysCounter++;
                currentWeekDay++;
                daysMonthLayerString += dayCode
            }

            //check if the empty cells it have yet to write of the last week of the month
            currentWeekDay--;
            if (currentWeekDay % 7 != 0) {
                dayCode = "";
                for (var i = (currentWeekDay % 7) + 1; i <= 7; i++) {
                    var dayCode = "";
                    dayCode += '<td ';
                    //add weekDay
                    dayCode += ' class="invalid-day week-day-'+ (i-1) +'"';
                    dayCode += '"></td>';
                    if (i == 7)
                        dayCode += '</tr>'
                    daysMonthLayerString += dayCode
                }
            }

            listListeralsWeek();

            layoutMonth.append(daysMonthLayerString);

            layoutMonth = $('<div class="monthDisplayed" ></div>').append(layoutMonth);
            layoutMonth.prepend($('<h2 class="sr-only" >' + monthNames[month] + '</h2>'));
            layoutMonth.prepend($('<div class="month" >' + monthNames[month] + '</div>'));

            layoutMonth = $('<div class="col-md-3" ></div>').append(layoutMonth);


            daysMonthsLayer.append(layoutMonth);
        }

        /**
         * calc the number of the week day
         */
        function calcNumberDayWeek(day, month, year) {
            var objFecha = new Date(year, month, day);
            var numDia = objFecha.getDay();
            if (numDia == 0)
                numDia = 6;
            else
                numDia--;
            return numDia;
        }

        /**
         * check if a date is correct
         * 
         * @thanks http://kevin.vanzonneveld.net
         * @thanks http://www.desarrolloweb.com/manuales/manual-librerias-phpjs.html
         */
        function checkDate(m, d, y) {
            return m > 0 && m < 13 && y > 0 && y < 32768 && d > 0 && d <= (new Date(y, m, 0)).getDate();
        }

        /**
         * return last day of a date (month n year)
         */
        function lastDay(month, year) {
            var lastDayValue = 28;
            while (checkDate(month + 1, lastDayValue + 1, year)) {
                lastDayValue++;
            }
            return lastDayValue;
        }

        function validateWritedDate(fecha) {
            var arrayFecha = fecha.split("/");
            if (arrayFecha.length != 3)
                return false;
            return checkDate(arrayFecha[1], arrayFecha[0], arrayFecha[2]);
        }

        /**
         * check if there are ajax events
         */
        function checkEvents(year) {
            if (reqAjax != false) {
                //peticio ajax
                $.ajax({
                    type: reqAjax.type,
                    url: reqAjax.url,
                    data: {ano: year},
                    dataType: 'json'
                }).done(function(data) {

                    if (typeof events == 'undefined')
                        events = [];

                    $.each(data, function(k, v) {
                        events.push(data[k]);
                    });

                    markEvents(year);

                });
            } else {
                markEvents(year);
            }
        }

        /**
         * mark all the events n create logic for them
         */
        function markEvents(year) {

            for (var i = 0; i < events.length; i++) {

                if (events[i].date.split('/')[2] == year) {

                    var loopDayTd = $('#' + calendarId + '_' + events[i].date.replace(/\//g, "_"));
                    var loopDayA = $('#' + calendarId + '_' + events[i].date.replace(/\//g, "_") + ' a');

                    loopDayTd.addClass('event');

                    loopDayA.attr('data-original-title', events[i].title);

                    //bg color
                    if (events[i].color)
                        loopDayTd.css('background', events[i].color);

                    //link
                    if (typeof events[i].link != 'undefined') {
                        loopDayA.attr('href', events[i].link);
                    }

                    //class
                    if (events[i].class)
                        loopDayTd.addClass(events[i].class);

                    //tooltip vs popover
                    if (events[i].content) {
                        loopDayTd.addClass('event_popover');
                        loopDayA.attr('rel', 'popover');
                        loopDayA.attr('data-content', events[i].content);
                    } else {
                        loopDayTd.addClass('event_tooltip');
                        loopDayA.attr('rel', 'tooltip');
                    }
                }
            }

            $('#' + calendarId + ' ' + '.event_tooltip a').tooltip(tooltipOptions);
            $('#' + calendarId + ' ' + '.event_popover a').popover(popoverOptions);

            $('.manual_popover').click(function() {
                $(this).popover('toggle');
            });
        }

        /**
         * check if the user can mark days
         */
        function checkIfEnableMark() {
            if (enableSelect == true) {

                var eventBicCalendarSelect;

                elem.on('click', '#monthsLayer td', function() {
                    //if multiSelect
                    if (multiSelect == true) {
                        if (daySelected == '') {
                            daySelected = $(this).data('date');
                            markSelectedDays();
                        } else {
                            if (lastDaySelected == '') {
                                //set firstDaySelected
                                firstDaySelected = daySelected;
                                lastDaySelected = $(this).data('date');
                                
                                markSelectedDays();

                                //create n fire event
                                //to change
                                var eventBicCalendarSelect = new CustomEvent("bicCalendarSelect", {
                                    detail: {
                                        dateFirst: firstDaySelected,
                                        dateLast: lastDaySelected
                                    }
                                });
                                document.dispatchEvent(eventBicCalendarSelect);
                            } else {
                                firstDaySelected = '';
                                lastDaySelected = '';
                                daySelected = '';
                                elem.find('.selection').removeClass('middle-selection selection first-selection last-selection');
                            }
                        }
                    } else {
                        //remove the class selection of the others a
                        elem.find('td div').removeClass('selection');
                        //add class selection
                        $(this).find('div').addClass('selection');
                        //create n fire event
                        var eventBicCalendarSelect = new CustomEvent("bicCalendarSelect", {
                            detail: {
                                date: $(this).data('date')
                            }
                        });
                        document.dispatchEvent(eventBicCalendarSelect);
                    }
                })
            }
        }

        /**
         * to mark selected dates
         */
        function markSelectedDays() {
            if (daySelected != '' && firstDaySelected == '') {
                var arrayDate = daySelected.split('/');
                $('#bic_calendar_' + arrayDate[1] + '_' + arrayDate[0] + '_' + arrayDate[2] + ' div').addClass('selection');
            } else if (firstDaySelected != '') {
                //create array from dates
                var arrayFirstDay = firstDaySelected.split('/');
                var arrayLastDay = lastDaySelected.split('/');

                //remove all selected classes
                elem.find('td.selection').removeClass('selection');

                //create date object from dates
                var oldSelectedDate = new Date(firstDaySelected);
                var newSelectedDate = new Date(lastDaySelected);

                //create a loop adding day per loop to set days
                //turn dates if >
                if (oldSelectedDate > newSelectedDate) {
                    //turn vars date
                    var tempSelectedDate = oldSelectedDate;
                    oldSelectedDate = newSelectedDate;
                    newSelectedDate = tempSelectedDate;
                }
                //set first selection
                $('#bic_calendar_' + oldSelectedDate.getDate() + '_' + (parseInt(oldSelectedDate.getMonth()) + 1) + '_' + oldSelectedDate.getFullYear() + ' div').addClass('selection first-selection');
                while (oldSelectedDate < newSelectedDate) {
                    oldSelectedDate.setDate(oldSelectedDate.getDate() + 1);
                    //set middle-selection
                    $('#bic_calendar_' + oldSelectedDate.getDate() + '_' + (parseInt(oldSelectedDate.getMonth()) + 1) + '_' + oldSelectedDate.getFullYear() + ' div').addClass('selection middle-selection');
                }
                //set last selection
                $('#bic_calendar_' + oldSelectedDate.getDate() + '_' + (parseInt(oldSelectedDate.getMonth()) + 1) + '_' + oldSelectedDate.getFullYear() + ' div').removeClass('middle-selection').addClass('selection last-selection');
            }
        }

        /*** --functions-- ***/



        //fire calendar!
        showCalendar();


    });
    return this;
};
