/*
Monthly 2.0.5 by Kevin Thornbloom is licensed under a Creative Commons Attribution-ShareAlike 4.0 International License.
*/
(function($) {
    $.fn.extend({        
	    monthly: function (options) {
			// These are overridden by options declared in footer
	        var defaults = {
                reset: false,
				weekStart: 'Lunes',
				mode: '',
				xmlUrl: '',
				target: '',
				eventList: true,
				maxWidth: false,
				setWidth: false,
				startHidden: false,
				showTrigger: '',
				stylePast: false,
				disablePast: false,
				month: '',
				year: '',
                regreso: false
			}

            
			var options = $.extend(defaults, options),
				that = this,
				uniqueId = $(this).attr('id'),
				d = new Date(),               
				currentMonth = d.getMonth() + 1,
				currentYear = d.getFullYear(),
				currentDay = d.getDate(),
				monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                dayNames = ['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo'];
        
			

		if (options.maxWidth != false){
			$('#'+uniqueId).css('maxWidth',options.maxWidth);
		}
		if (options.setWidth != false){
			$('#'+uniqueId).css('width',options.setWidth);
		}

		if (options.startHidden == true){
			$('#'+uniqueId).addClass('monthly-pop').css({
				'position' : 'absolute',
				'display' : 'none'
			});
			$(document).on('focus', ''+options.showTrigger+'', function (e) {
				$('#'+uniqueId).show();
				e.preventDefault();
			});
			$(document).on('click', ''+options.showTrigger+', .monthly-pop', function (e) {
				e.stopPropagation();
				e.preventDefault();
			});
			$(document).on('click', function (e) {
				$('#'+uniqueId).hide();
			});
		}

        //Para la recarga del calendario desde la reconexión
		if (options.reset) {		    
		    obtenerCatalogoJornadas(currentMonth, currentYear);
		    return;
		}
	   
		// Add Day Of Week Titles
		if (options.weekStart == 'Lunes') {
		    $('#' + uniqueId).append('<thead><div class="monthly-day-title-wrap"><tr><th scope="col" id="L">Lunes</th><th scope="col" id="M">Martes</th><th scope="col" id="X">Miércoles</th><th scope="col" id="J">Jueves</th><th scope="col" id="V">Viernes</th><th scope="col" id="S">Sábado</th><th scope="col" id="D">Domingo</th></tr></div></thead>');
		} else {
			console.log('Incorrect entry for weekStart variable.')
		}

		// Add Header & event list markup
		$('#' + uniqueId).prepend('<caption><div class="monthly-header"><span class="sr-only">Puede pulsar en cualquier momento dentro de la tabla calendario para ir a la pantalla de imputaci&oacute;n semanal</span><div class="monthly-header-title"></div><a href="#" class="monthly-prev oculto" title="Mes anterior" tabindex="0">Mes anterior</a><a href="#" class="monthly-next oculto" title="Mes siguiente" tabindex="0">Mes siguiente</a></div></caption>').append('<tbody class="monthly-day-wrap"></tbody>');
		
		// How many days are in this month?
		function daysInMonth(m, y){
			return m===2?y&3||!(y%25)&&y&15?28:29:30+(m+(m>>3)&1);
		}

		// Massive function to build the month
		function setMonthly(m, y){
		    var defer = $.Deferred();

		    $('#' + uniqueId).data('setMonth', m).data('setYear', y);

			// Get number of days
			var dayQty = daysInMonth(m, y),
				// Get day of the week the first day is
				mZeroed = m -1,
				firstDay = new Date(y, mZeroed, 1, 0, 0, 0, 0).getDay();
			// Remove old days
			$('#' + uniqueId + ' .monthly-day, #' + uniqueId + ' .monthly-day-blank').remove();
			$('#'+uniqueId+' .monthly-week').remove();
			$('#'+uniqueId+' .monthly-event-list').empty();
			
			for(var i = 0; i < dayQty; i++) {
				// Fix 0 indexed days
				var day = i + 1;
					
				// Check if it's a day in the past
				if(((day < currentDay && m === currentMonth) || y < currentYear || (m < currentMonth && y == currentYear)) && options.stylePast == true){
				    $('#' + uniqueId + ' .monthly-day-wrap').append('<td class="m-d monthly-day"><div class="monthly-day-pick monthly-past-day" data-number="' + day + '"><div class="monthly-day-number">' + day + '</div></td>');
				} else {							
				    $('#' + uniqueId + ' .monthly-day-wrap').append('<td class="m-d monthly-day" tabindex="0"><span class="sr-only diasImputables"></span><div class="monthly-day-pick" aria-hidden="true"  data-number="' + day + '"><div class="monthly-day-number">' + day + '</div><div class="monthly-time-number"><span id="planificadas" class="hidden-xs horas diaVacaciones"/><span id="imputadas" class="hidden-xs horas diaRegistrado"></span><span id="estandar" class="hidden-xs horas diaEstandar"></span></div></div></td>');
				}
			}
			


			// Set Today
			var setMonth = $('#' + uniqueId).data('setMonth'),
				setYear = $('#' + uniqueId).data('setYear');
			if (setMonth == currentMonth && setYear == currentYear) {
			    $('#' + uniqueId + ' *[data-number="' + currentDay + '"]').parent().addClass('monthly-today');
			    $('#' + uniqueId + ' *[data-number="' + currentDay + '"]').parent().attr('id', 'today');
			}		       

		    //Lista años
			if (setMonth == currentMonth && setYear == currentYear) {
			    $('#' + uniqueId + ' .monthly-header-title').html('<select class="selectMonth" title="Selección de mes" data-handler="selectMonth" data-event="change"><option value="1">Enero</option><option value="2">Febrero</option><option value="3">Marzo</option><option value="4">Abril</option><option value="5">Mayo</option><option value="6">Junio</option><option value="7">Julio</option><option value="8">Agosto</option><option value="9">Septiembre</option><option value="10">Octubre</option><option value="11">Noviembre</option><option value="12">Diciembre</option></select>&nbsp;&nbsp;<select class="selectYear" title="Selección de año" data-handler="selectYear" data-event="change"></select>') + '<a href="#today" class="sr-only">Día actual</a>';
			} else {
			    $('#' + uniqueId + ' .monthly-header-title').html('<select class="selectMonth" title="Selección de mes" data-handler="selectMonth" data-event="change"><option value="1">Enero</option><option value="2">Febrero</option><option value="3">Marzo</option><option value="4">Abril</option><option value="5">Mayo</option><option value="6">Junio</option><option value="7">Julio</option><option value="8">Agosto</option><option value="9">Septiembre</option><option value="10">Octubre</option><option value="11">Noviembre</option><option value="12">Diciembre</option></select>&nbsp;&nbsp;<select class="selectYear" title="Selección de año" data-handler="selectYear" data-event="change"></select>');
			}
			$('#' + uniqueId + ' .selectMonth option[value = "' + m + '"]').attr("selected", "selected");
            
            
			for (var anno = 2007; anno <= currentYear + 1; anno++) {
			    $('#' + uniqueId + ' .selectYear').append('<option value="'+anno+'">'+anno+'</option>');
			}

			$('#' + uniqueId + ' .selectYear option[value = "' + y + '"]').attr("selected", "selected");
            

			// Account for empty days at start
			if(options.weekStart == 'Domingo' && firstDay != 7) {
				for(var i = 0; i < firstDay; i++) {
				    $('#' + uniqueId + ' .monthly-day-wrap').prepend('<td class="m-d monthly-day-blank"><span class="sr-only">Día en blanco</span><div class="monthly-day-number"></div></td>');
				}
			} else if (options.weekStart == 'Lunes' && firstDay == 0) {
				for(var i = 0; i < 6; i++) {
				    $('#' + uniqueId + ' .monthly-day-wrap').prepend('<td class="m-d monthly-day-blank"><span class="sr-only">Día en blanco</span><div class="monthly-day-number"></div></td>');
				}
			} else if (options.weekStart == 'Lunes' && firstDay != 1) {
				for(var i = 0; i < (firstDay - 1); i++) {
				    $('#' + uniqueId + ' .monthly-day-wrap').prepend('<td class="m-d monthly-day-blank"><span class="sr-only">Día en blanco</span><div class="monthly-day-number"></div></td>');
				}				
			}
			//Account for empty days at end
			var numdays = $('#' + uniqueId + ' .monthly-day').length,
				numempty = $('#' + uniqueId + ' .monthly-day-blank').length,
				totaldays = numdays + numempty,
				roundup = Math.ceil(totaldays/7) * 7,
				daysdiff = roundup - totaldays;
			if(totaldays % 7 != 0) {
				for(var i = 0; i < daysdiff; i++) {
				    $('#' + uniqueId + ' .monthly-day-wrap').append('<td class="m-d monthly-day-blank"><span class="sr-only">Día en blanco</span><div class="monthly-day-number"></div></td>');
				}
			}
			
			//Para colorear los días del mes
			var divs = $("#"+uniqueId+" .m-d");
			var divDiaActual = 0;

		    //Para separar los días del mes en semanas
			semanas = 0;
			var mesCerrado = MesCerrado(setMonth, setYear);
			/*var annoPosterior = false;
			if (currentYear < setYear) annoPosterior = true;*/

			var contenedorSemana = "";
			var diasSemana;
			listasSemanas = new Array();

            //String de parámetros para la redirección a imputación diaria
			var qs;
			var strPrimerDiaSemana; var strUltimoDiaSemana; var intPrimerDia; var intDiasEnSemana; var strRango;
			for(var i = 0; i < divs.length; i+=7) {		
			    qs = "";
			    strPrimerDiaSemana = "";
			    strUltimoDiaSemana = "";
			    intPrimerDia = 0;
			    intDiasEnSemana = 0;
			    strRango = "";
			    strRangoMini = "";

			    divs.slice(i, i + 7).wrapAll("<tr class='monthly-week' tabindex='0' data-href='' data-week='" + (semanas + 1) + "' title='Imputaci&oacute;n diaria de la semana " + (semanas + 1) + "'></tr>");
                
			    if (mesCerrado) {
			        //Para inhabilitar las semanas de un mes cerrado			        
			        $(divs[i]).parent().addClass('diaCerrado');
			        $(divs[i]).parent().attr('title', 'Semana ' + (semanas + 1) + ' deshabilitada');
			        $(divs[i].children[1]).addClass('diaCerrado');

			    } else {

			        contenedorSemana = $('#cal').find('[data-week= "' + (semanas + 1) + '"]');
			        contenedorDiasSemana = $(contenedorSemana).find('.monthly-day-number');
			        diasSemana = new Array();
			        for (var z = 0; z < contenedorDiasSemana.length; z++) {
			            diasSemana[z] = $(contenedorDiasSemana[z]).text();
			            //Primer día imputable de la semana en el mes actual
			            if (diasSemana[z] != "" && strPrimerDiaSemana == "") {
			                //Valor del primer día de la semana
			                strPrimerDiaSemana = diasSemana[z];
			                //Indice del primer día de la semana
			                intPrimerDia = z;
			               
			            }
			            //Último día imputable de la semana en el mes actual
			            if (diasSemana[z] != "") {
			                strUltimoDiaSemana = diasSemana[z];
			                //Número de días de la semana
			               intDiasEnSemana++;
			            }	         
			        }
			        listasSemanas[semanas] = diasSemana;

			        strRangoMini = strPrimerDiaSemana + " - " + strUltimoDiaSemana + " " + monthNames[m - 1] + " - " + y;
			        strRango = "Semana del " + strPrimerDiaSemana + " al " + strUltimoDiaSemana + " de " + monthNames[m - 1] + " - " + y;
			        qs = IB.uri.encode("ipd=" + strPrimerDiaSemana + "&iud=" + strUltimoDiaSemana + "&ipds=" + intPrimerDia + "&ides=" + intDiasEnSemana +
                         "&im=" + (m - 1) + "&ia=" + y + "&sr=" + strRango + "&srm=" + strRangoMini);

			        $(contenedorSemana).attr('data-href', '../ImpDiaria/Default.aspx?' + qs);
			    }

			    semanas++;

			}
			defer.resolve();
		}

		// Set the calendar the first time
		//El mes y el anno se obtienen por parámetro, así se podrá cargar el calendario en fechas determinadas
		//setMonthly(parseInt(options.month), parseInt(options.year));
		obtenerCatalogoJornadas(parseInt(options.month), parseInt(options.year));
	    /*Función que obtiene los datos sobre las jornadas del mes de un profesional*/ 

		function obtenerCatalogoJornadas(mesSel, annoSel) {
		    var mesCerrado = MesCerrado(mesSel, annoSel);
		    IB.procesando.mostrar();
		    var payload = { codUsu: IB.vars.codUsu, idficepi: IB.vars.idficepi, codCal: IB.vars.codCal, mes: mesSel, anno: annoSel };

		    try {
		        IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Reporte/Calendario/Default.aspx", "getJornadasCalendario", payload, null,
                 function (data) {
                     if (data.length > 0) {
                         $.when(getFestivosRango(annoSel, mesSel, mesCerrado))
                         .then(function (lstFestivos) {
                             IB.vars.aFestivos = lstFestivos;
                             $.when(setMonthly(parseInt(mesSel), parseInt(annoSel)))
                                 .then(visualizarCalendario(), pintarDatosCalendario(data, mesSel, annoSel, mesCerrado));
                         });
                     } else {
                         $.when(setMonthly(parseInt(mesSel), parseInt(annoSel)))
                            .then(function () {
                                visualizarCalendarioSinDatos(mesCerrado);
                            });
                     }
                     
                 },
                 function (ex, status) {
                     $.when(setMonthly(parseInt(mesSel), parseInt(annoSel)))
                        .then(function () {
                            visualizarCalendarioSinDatos(mesCerrado);
                            IB.bserror.error$ajax(ex, status);
                        });
                 }
             );
		    } catch (e) {
		        IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
		    }	    
            
		}

		function visualizarCalendarioSinDatos(mesCerrado) {
		    visualizarCalendario();
		    //Se deshabilitan todas las semanas del calendario
		    var contenedorSemana;
		    for (x = 0; x < semanas; x++) {
		        contenedorSemana = $('#cal').find('[data-week= "' + (x + 1) + '"]');
		        $(contenedorSemana).addClass("diaCerrado");
		        $(contenedorSemana).attr('title', 'Semana ' + (x + 1) + ' deshabilitada');
		    }
            //Se muestra el resumen de tareas vacío
		    SUPER.IAP30.Calendario.View.pintarTabla(new Array(), 0, 0, mesCerrado);
		    IB.procesando.ocultar();
        }

		function getFestivosRango(annoSel, mesSel, mesCerrado) {

		    var defer = $.Deferred();

		    var mesAntCerrado, mesAnt, anno;
		    if (mesSel == 1){
		        mesAnt = 12;
		        anno = annoSel-1;
		    }else{
		        mesAnt = mesSel-1;
		        anno = annoSel;
		    }

		    mesAntCerrado = MesCerrado(mesAnt, anno);

		    if (mesCerrado || mesAntCerrado) defer.resolve(new Array());
		    else {
		        var payload = { anno: anno, mes: mesAnt};
		        IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Reporte/Calendario/Default.aspx", "getFestivosRango", payload, null,
                    function (data) {
                        defer.resolve(data);
                    },
		            function (ex, status) {		                
		                IB.procesando.ocultar();
		                IB.bserror.error$ajax(ex, status);		                
		                defer.resolve();
		            }
                );
		    }
		    return defer.promise();
		}
        

		function visualizarCalendario() {
		    $('.container').css("visibility", "visible");
		}

		function pintarDatosCalendario(datos, mesSel, annoSel, mesCerrado) {

		    var contenedor;
		    var contenedorInfo;
		    var contenedorDia;
		    var contenedorHoras;
		    var contenedorHorasImputadas;
		    var contenedorHorasEstandar;
		    var contenedorHorasPlanificadas;

		    var aFestivos = IB.vars.aFestivos;
		    var diaActual = $('#cal #today .monthly-day-number').text();
		    var mesAnterior;
		    var mesSiguiente;
		    //Pintar los literales de la semana según la semana laboral
		    var aSemanaLab = IB.vars.aSemLab.split(",");
		    var dia;
		    for (var i = 0; i < aSemanaLab.length; i++) {
		        if (aSemanaLab[i] == "0") {
		            switch (i) {
		                case 0:
		                    dia = "L";
		                    break;
		                case 1:
		                    dia = "M";
		                    break;
		                case 2:
		                    dia = "X";
		                    break;
		                case 3:
		                    dia = "J";
		                    break;
		                case 4:
		                    dia = "V";
		                    break;
		                case 5:
		                    dia = "S";
		                    break;
		                case 6:
		                    dia = "D";
		            }

		            $('#' + dia).addClass("diaFestivo");
		        }
		    }

		    //No estamos en el mes actual dentro del calendario
		    if (diaActual == "") {

		    }
		    var diaCal;
		    var dateCal;
		    var horasImputar = 0;
		    var horasImputadas = 0;
		    for (var i = 0; i < datos.length; i++) {
		        diaCal = datos[i];
		        dateCal = new Date(annoSel, eval(mesSel - 1), diaCal.dia);
		        contenedor = $('#cal .monthly-day').find('[data-number= "' + diaCal.dia + '"]');
		        contenedorInfo = $(contenedor).parent().find('.diasImputables');
		        contenedorDia = $(contenedor).find('.monthly-day-number');
		        if (diaCal.dia_festivo == 1 || diaCal.estilo_festivo == 1) {
		            contenedorDia.addClass("diaFestivo");
		            contenedorInfo.html('D&iacute;a: ' + diaCal.dia + '. Horas registradas: ' + diaCal.esfuerzo + '. Horas disponibles: ' + diaCal.horas_estandar + '. Estado: Festivo');
		        } else if (diaCal.dia_vacaciones == 1) {
		            contenedorDia.addClass("diaVacaciones");
		            contenedorInfo.html('D&iacute;a: ' + diaCal.dia + '. Horas registradas: ' + diaCal.esfuerzo + '. Horas disponibles: ' + diaCal.horas_estandar + '. Estado: Vacaciones');
		        } else if (diaCal.esfuerzo > 0) {
		            contenedorDia.addClass("diaRegistrado");
		            contenedorInfo.html('D&iacute;a: ' + diaCal.dia + '. Horas registradas: ' + diaCal.esfuerzo + '. Horas disponibles: ' + diaCal.horas_estandar + '. Estado: Registrado');
		        } else {
		            if (diaCal.esfuerzo == 0) {
		                //Si estamos en el mes actual
		                if ((diaActual != "" && diaCal.dia > diaActual) || (dateCal > new Date)) {
		                    contenedorDia.addClass("diaFuturo");
		                    contenedorInfo.html('D&iacute;a: ' + diaCal.dia + '. Horas registradas: ' + diaCal.esfuerzo + '. Horas disponibles: ' + diaCal.horas_estandar + '. Estado: Futuro');

		                } else { //Si nos hemos movido de mes en el calendario                            
		                    contenedorDia.addClass("diaSinRegistrar");
		                    contenedorInfo.html('D&iacute;a: ' + diaCal.dia + '. Horas registradas: ' + diaCal.esfuerzo + '. Horas disponibles: ' + diaCal.horas_estandar + '. Estado: Sin registrar');
		                }
		            }
		        }

		        contenedorHorasImputadas = $(contenedor).find('#imputadas');
		        contenedorHorasEstandar = $(contenedor).find('#estandar');
		        if (diaCal.esfuerzo > 0) contenedorHorasImputadas.html(diaCal.esfuerzo);
		        else contenedorHorasImputadas.html(diaCal.esfuerzo);
		        if (mesCerrado) contenedorHorasEstandar.html("");
		        else contenedorHorasEstandar.html(" / " + diaCal.horas_estandar);
		        
		        if (diaCal.horas_planificadas != null && diaCal.horas_planificadas > 0) {
		            contenedorHorasPlanificadas = $(contenedor).find('#planificadas');
		            contenedorHorasPlanificadas.html(diaCal.horas_planificadas + " / ");
		        }

		        if (diaCal.dia_festivo != 1 && diaCal.estilo_festivo != 1) horasImputar = horasImputar + diaCal.horas_estandar;
		        horasImputadas = horasImputadas + diaCal.esfuerzo;
		    }


		    //Control de huecos
		    var controlHuecos = IB.vars.controlhuecos == "True" ? true : false;
		    var fechaUltimaImputacion = IB.vars.FechaUltimaImputacion;
		    var mesUltimaImputacion;
		    var annoUltimaImputacion;
		    var diaUltimaImputacion;
		    var dateUltimaImputacion;

		    if (fechaUltimaImputacion != "") {
		        var aUltimoDia = fechaUltimaImputacion.split("/");
		        annoUltimaImputacion = parseInt(aUltimoDia[2], 10);
		        mesUltimaImputacion = parseInt(aUltimoDia[1], 10);
		        diaUltimaImputacion = parseInt(aUltimoDia[0], 10);
		        dateUltimaImputacion = new Date(annoUltimaImputacion, mesUltimaImputacion - 1, diaUltimaImputacion);
		        if (dateUltimaImputacion < dateUltimoMesCerrado) dateUltimaImputacion = dateUltimoMesCerrado;

		        diaUltimaImputacion = dateUltimaImputacion.getDate();
		        annoUltimaImputacion = dateUltimaImputacion.getFullYear();
		        mesUltimaImputacion = dateUltimaImputacion.getMonth()+1;

		    }
		    
		    

		    var diaSemanaUltimaImputacion;
		    var semanaUltimaImputacion;
		    var numSemanasCal = listasSemanas.length;
		    var contenedorSemana;
		    var restoDiasSemanaFestivo = false;

		    if (controlHuecos) {
		        //Se mira si la última imputación se ha hecho en el mes actual o en el anterior
		        if (annoSel == annoUltimaImputacion && mesSel == mesUltimaImputacion) { //Mes actual
		            //alert("ultima imputacion este mes");
		            //Se obtiene la semana y el dia de semana del últmo dia reportado
		            for (var i = 0; i < listasSemanas.length; i++) {
		                for (var x = 0; x < listasSemanas[i].length; x++) {
		                    if (parseInt(listasSemanas[i][x]) == diaUltimaImputacion) {
		                        semanaUltimaImputacion = i + 1;
		                        diaSemanaUltimaImputacion = x;
		                        break;
		                    }
		                }
		            }

		            //Si quedan más semanas por imputar en el mes actual y la semana de imputación está completada,
		            //se deshabilitan las siguientes semanas a la siguiente de la actual
		            if (semanaUltimaImputacion < (numSemanasCal)) {
		                //Si el último día imputado no completa la semana de imputación, se comprueba si los días de la semana que faltan son festivos.
		                //Si es así, se considera la semana completa y se habilita la siguiente semana y se deshabilitan las demás. Si no, se deshabilitan todas las demás
		                if (diaSemanaUltimaImputacion < 6) {
		                    for (var x = diaSemanaUltimaImputacion; x <= 6 ; x++) {
		                        if (datos[listasSemanas[semanaUltimaImputacion - 1][diaSemanaUltimaImputacion]].dia_festivo == 1 ||
                                    datos[listasSemanas[semanaUltimaImputacion - 1][diaSemanaUltimaImputacion]].estilo_festivo == 1) {
		                            restoDiasSemanaFestivo = true;
		                        } else {
		                            restoDiasSemanaFestivo = false;
		                            break;
		                        }
		                    }
		                } else restoDiasSemanaFestivo = true;

		                if (restoDiasSemanaFestivo) {//Se cierran las semanas posteriores a la siguiente del actual
		                    for (var y = semanaUltimaImputacion + 1; y < (numSemanasCal) ; y++) {
		                        contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                        $(contenedorSemana).addClass("diaCerrado");
		                        $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
		                    }
		                } else {//Se cierran todas las semanas posteriores al actual
		                    for (var y = semanaUltimaImputacion; y < (numSemanasCal) ; y++) {
		                        contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                        $(contenedorSemana).addClass("diaCerrado");
		                        $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
		                    }
		                }

		            }

		            //Miro si el mes de la última imputación es justo el anterior al mes actual
		        } else if (((parseInt(mesUltimaImputacion, 10) + 1) == parseInt(mesSel, 10) && parseInt(annoUltimaImputacion, 10) == parseInt(annoSel, 10)) ||
                        (parseInt(mesUltimaImputacion, 10) == 12 && parseInt(mesSel, 10) == 1 && parseInt(annoUltimaImputacion, 10) == (parseInt(annoSel, 10) - 1))) {

		            //alert("ultima imputacion mes anterior");
		            //Si el último día imputado es menor o igual al último del mes anterior
		            objFechaMesAnt = new Date(annoSel, mesSel - 1, 0);
		            var intUltimoDiaMesAnt = objFechaMesAnt.getDate();
		            if (diaUltimaImputacion <= intUltimoDiaMesAnt) {
		                var restoDiasMesFestivo = true;
		                var fechaFestivo;
		                var annoFechaFestivo;
		                var mesFechaFestivo;
		                var diaFechaFestivo;
		                var dateFechaFestivo;
		                var dateCalendario;
		                var nuevaSemana = false;

		                if (diaUltimaImputacion < intUltimoDiaMesAnt) {
		                    //Hay que controlar si los días que faltan para acabar el mes son festivos
		                    for (var x = diaUltimaImputacion + 1; x <= intUltimoDiaMesAnt; x++) {
		                        dateCalendario = new Date(annoUltimaImputacion, mesUltimaImputacion - 1, x);
		                        restoDiasMesFestivo = false;
		                        for (var i = 0; i < aFestivos.length; i++) {		                            
		                            if (new Date(moment(aFestivos[i].t067_dia)).getTime() === dateCalendario.getTime()) {
		                                restoDiasMesFestivo = true;
		                                break;
		                            }
		                        }
		                        if (!restoDiasMesFestivo) break;
		                    }
		                }

		                //Se comprueba si el mes actual inicia una nueva semana
		                if (listasSemanas[0][0] != "") nuevaSemana = true;

		                if (restoDiasMesFestivo) {
		                    //Se comprueba si los primeros días del mes que completan la semana son festivos o no
		                    for (var x = 0; x < listasSemanas.length; x++) {
		                        if (listasSemanas[0][x] == "" || (datos[(listasSemanas[0][x]) - 1].dia_festivo == 1 ||
                                    datos[(listasSemanas[0][x]) - 1].estilo_festivo == 1)) {
		                            restoDiasSemanaFestivo = true;
		                        } else {
		                            restoDiasSemanaFestivo = false;
		                            break;
		                        }
		                    }
		                }

		                //Se deshabilitan las semanas según el caso
		                if (nuevaSemana) {
		                    if (restoDiasMesFestivo) {
		                        for (var y = 1; y < (numSemanasCal) ; y++) {
		                            contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                            $(contenedorSemana).addClass("diaCerrado");
		                            $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
		                        }
		                    } else {
		                        for (var y = 0; y < (numSemanasCal) ; y++) {
		                            contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                            $(contenedorSemana).addClass("diaCerrado");
		                            $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
		                        }
		                    }
		                } else {
		                    if (restoDiasMesFestivo && restoDiasSemanaFestivo) {
		                        for (var y = 2; y < (numSemanasCal) ; y++) {
		                            contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                            $(contenedorSemana).addClass("diaCerrado");
		                            $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
		                        }
		                    } else if (restoDiasMesFestivo && !restoDiasSemanaFestivo) {
		                        for (var y = 1; y < (numSemanasCal) ; y++) {
		                            contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                            $(contenedorSemana).addClass("diaCerrado");
		                            $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
                                }
		                    } else {
		                        for (var y = 0; y < (numSemanasCal) ; y++) {
		                            contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                            $(contenedorSemana).addClass("diaCerrado");
		                            $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
		                        }
		                    }
		                }

		            }
		            //Si estamos en un mes de calendario 1 o mas meses posterior al mes de imputación, se deshabilitan todas las semanas
		        } else if (((parseInt(mesUltimaImputacion, 10) + 1) < parseInt(mesSel, 10) && parseInt(annoUltimaImputacion, 10) == parseInt(annoSel, 10)) ||
                    (parseInt(annoUltimaImputacion, 10) == (parseInt(annoSel, 10) - 1))) {
		            for (var y = 0; y < (numSemanasCal) ; y++) {
		                contenedorSemana = $('#cal').find('[data-week= "' + (y + 1) + '"]');
		                $(contenedorSemana).addClass("diaCerrado");
		                $(contenedorSemana).attr('title', 'Semana ' + (y + 1) + ' deshabilitada');
		            }
		        }           
		    }


		    var objDia;
		    var intDiferencia = 0;
		    //Se comprueba si las semanas del més sobrepasan el límite de 62 días a partir de fecha de mes cerrado
            //tanto si hay control de huecos como si no
		    for (var x = 0; x < numSemanasCal; x++) {
		        var y = 0;
		        if (listasSemanas[x][0] == "") {
		            for (y = 0; y < 6; y++) {
		                if (listasSemanas[x][y] != "") break;
		            }
		        }
		        objDia = new Date(annoSel, mesSel - 1, listasSemanas[x][y]);
		        intDiferencia = objDia.getTime() - dateUltimoMesCerrado.getTime();
		        if (intDiferencia > 5356800000) { //62 días en milisegundos (1 día 86400000).
		            contenedorSemana = $('#cal').find('[data-week= "' + (x + 1) + '"]');
		            $(contenedorSemana).addClass("diaCerrado");
		            $(contenedorSemana).attr('title', 'Semana ' + (x + 1) + ' deshabilitada');
		        }
		    }
		   

		    //IB.procesando.ocultar();
		    //TODO: llamar al método de obtenerTareasMes de Carlos
		    getConsumoMesTareas(IB.vars.codUsu, annoSel, mesSel, horasImputar, horasImputadas, mesCerrado);
		}

		function MesCerrado(mesSel, annoSel) {

		    var strUMC = IB.vars.UMC_IAP;
		    try {
		        if (strUMC != "") {
		            dateUltimoMesCerrado = new Date(strUMC.substring(0, 4), strUMC.substring(4, 6), 0);		            
		        } else {
		            dateUltimoMesCerrado = new Date(1999, 11, 31);
		        }
		        var strFechaActual = new Date(annoSel, mesSel - 1, 1);
		        return (strFechaActual < dateUltimoMesCerrado) ? true : false;

		    } catch (e) {
		        IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Error al comprobar si el mes está cerrado : " + e);
		    }
		}

		function getConsumoMesTareas(codUsu, sAnno, sMes, horasImputar, horasImputadas, mesCerrado) {
		    
		    var filtro = { codUsu: codUsu, sAnno: sAnno, sMes: sMes };

		    //IB.procesando.mostrar();
		    IB.DAL.post(null, "getConsumoMesTareas", filtro, null,
                function (data) {
                    SUPER.IAP30.Calendario.View.pintarTabla(data, horasImputar, horasImputadas, mesCerrado);
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    //hacer todo en caso de que falle
                    //...
                    //...
                    //IB.bserror.error$ajax(ex, status);
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al obtener los datos de las tareas del usuario.");
                }
            );		        
		}

		// Function to go back to the month view
		function viewToggleButton(){
		    if($('#'+uniqueId+' .monthly-event-list').is(":visible")) {
		        	$('#'+uniqueId+' .monthly-cal').remove();
		        	$('#'+uniqueId+' .monthly-header-title').prepend('<a href="#" class="monthly-cal" title="Volver a la vista mensual"><div></div></a>');
		        }
		}

	    //Cambio de mes desde el combo
		$(document.body).on('change', '#' + uniqueId + ' .selectMonth', function (e) {
		    var setMonth = $('#' + uniqueId + ' .selectMonth option:selected').val();
		    var setYear = $('#' + uniqueId + ' .selectYear option:selected').val();
		    //var setYear = $('#' + uniqueId).data('setYear');
		    obtenerCatalogoJornadas(setMonth, setYear);

		    viewToggleButton();
		    e.preventDefault();
		});

	    //Cambio de año desde el combo
		$(document.body).on('change', '#' + uniqueId + ' .selectYear', function (e) {
		    var setYear = $('#' + uniqueId + ' .selectYear option:selected').val();
		    var setMonth = $('#' + uniqueId + ' .selectMonth option:selected').val();
		    obtenerCatalogoJornadas(setMonth, setYear);

		    viewToggleButton();
		    e.preventDefault();
		});

		// Advance months
		$(document.body).on('click', '#'+uniqueId+' .monthly-next', function (e) {
			var setMonth = parseInt($('#' + uniqueId).data('setMonth')),
				setYear = $('#' + uniqueId).data('setYear');
			if (setMonth == 12) {
				var newMonth = 1,
					newYear = setYear + 1;
			    //setMonthly(newMonth, newYear);				
			} else {
				var newMonth = setMonth + 1,
					newYear = setYear;
			    //setMonthly(newMonth, newYear);				
			}
			obtenerCatalogoJornadas(newMonth, newYear);
			viewToggleButton();
			e.preventDefault();
		});

		// Go back in months
		$(document.body).on('click', '#'+uniqueId+' .monthly-prev', function (e) {
			var setMonth = $('#' + uniqueId).data('setMonth'),
				setYear = $('#' + uniqueId).data('setYear');
			if (setMonth == 1) {
				var newMonth = 12,
					newYear = setYear - 1;
				//setMonthly(newMonth, newYear);
			} else {
				var newMonth = setMonth - 1,
					newYear = setYear;
				//setMonthly(newMonth, newYear);
			}
			obtenerCatalogoJornadas(newMonth, newYear);
			viewToggleButton();
			e.preventDefault();
		});

		// Reset Month
		/*$(document.body).on('click', '#'+uniqueId+' .monthly-reset', function (e) {
		    //setMonthly(currentMonth, currentYear);
		    obtenerCatalogoJornadas(currentMonth, currentYear);
			viewToggleButton();
			e.preventDefault();
			e.stopPropagation();
		});*/

		// Back to month view
		$(document.body).on('click', '#'+uniqueId+' .monthly-cal', function (e) {
			$(this).remove();
				$('#' + uniqueId+' .monthly-event-list').css('transform','scale(0)').delay('800').hide();
			e.preventDefault();
		});	    


	    // Cada fila(tr) del calendario enlazará con la pantalla referenciada en cada fila
		$(document.body).on('keypress', 'tr[data-href]', function (e) {
		    if (!$(this).hasClass('diaCerrado') && (e.which == 13)) {
		        var opt = {
		            delay: 1
		        }
		        IB.procesando.opciones(opt);
		        IB.procesando.mostrar();
		        document.location = $(this).data('href');
		    }
		        
		});	 

		$(document.body).on('click', 'tr[data-href]', function (e) {
		    if (!$(this).hasClass('diaCerrado')) {
		        var opt = {
		            delay: 1
		        }
		        IB.procesando.opciones(opt);
		        IB.procesando.mostrar();
		        document.location = $(this).data('href');		        
		    }
		});

		
	  }

	}

    );
})(jQuery);
