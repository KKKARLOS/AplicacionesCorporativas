var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ImpDiaria = SUPER.IAP30.ImpDiaria || {}


SUPER.IAP30.ImpDiaria.View = (function (e) {

    //Guarda el elemento que contenía el foco antes de pulsar un botón
    //var preInput = "";    

    var dom = {
        btnTarea: $('#btnTarea'),
        btnTareaLite: $('#btnTareaLite'),
        btnJornada: $('#btnJornada'),
        btnJornadaLite: $('#btnJornadaLite'),
        btnSemana: $('#btnSemana'),
        btnSemanaLite: $('#btnSemanaLite'),
        btnGrabar: $('#btnGrabar'),
        btnGrabarLite: $('#btnGrabarLite'),
        btnGrabarSig: $('#btnGrabarSig'),
        btnGrabarRegresar: $('#btnGrabarRegresar'),
        btnGASVI: $('#btnGasvi'),
        btnGASVILite: $('#btnGasviLite'),
        btnBitacora: $('#btnBitacora'),
        btnComentario: $('#btnComentario'),
        btnSalir: $('#btnSalir'),
        btnSalirLite: $('#btnSalirLite'),
        semSig: $('#semSig'),
        semAnt: $('#semAnt'),
        nivel1: $('#nivel1'),
        nivel2: $('#nivel2'),
        nivel3: $('#nivel3'),
        bomba: $('#icoBomb'),
        btnAceptarComentario: $('#btnAceptarComentario'),
        txtComentario: $('#txtComentario'),
        btnBusqueda: $('#btnBusqueda'),
        txtSearch: $('#txtSearch'),
        btnBuscarSiguiente: $('#btnBuscarSiguiente'),
        contenedor: $('#contenedor')
        
    }

    var selectores = {

        container: '.container',
        sel_inputs: "td input:not(.chkHoras)",
        //sel_nombreLinea: "td.nombreLinea",
        sel_nombreLinea: "td.nombreLinea > .glyphicon-plus, td.nombreLinea > .glyphicon-minus",
        sel_totalTarea: "input.totalTarea",
        sel_FFE: "input.txtFecha",
        sel_checkFin: "input.chkHoras",
        sel_inputdiasSemana: "input.diasSemana",
        sel_totales: ".diasTotal",
        sel_lineaTarea: "td.nombreLinea",
        sel_preInput: ""
    }

    var indicadores = {
        i_offsetTabla: $('#tabla').offset().top,
        //i_dispositivoTactil: false,
        i_lineaPadre: ""
    }

    function deAttachEvents(selector) {
        $(selector).off();
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var lineas = {

        //Lineas de proyectos económicos
        PE: function () {

            //return $('#bodyTabla tr[data-level="1"]').find('span[aria-expanded="false"]').parent();
            return $('#bodyTabla tr[data-tipo="PSN"]');

        },

        //Lineas cargadas y contraidas
        LineasCargadasContraidas: function () {

            return $('#bodyTabla tr[data-loaded="1"]').find('span[aria-expanded="false"]').parent().parent();
        },

        //Proyectos económicos cargados y expandidos
        PECargadosExpandidos: function () {

            return $('#bodyTabla tr[data-tipo="PSN"][data-loaded="1"]').find('span[aria-expanded="true"]').parent().parent();
        },

        //Lineas de proyectos técnicos
        PT: function () {

            //return $('#bodyTabla tr[data-level="1"]').find('span[aria-expanded="false"]').parent();
            return $('#bodyTabla tr[data-tipo="PT"]');

        },

        //Lineas que no sean Tarea, pie de tabla o línea de otros consumos
        padresTLineas: function () {

            //return $('#bodyTabla tr:not([data-tipo="T"], [data-level="0"], [data-loaded="1"])').find('span[aria-expanded="false"]').parent();;
            return $('#bodyTabla tr:not([data-tipo="T"], [data-level="0"])');

        },

        //Lineas que no sean pie de tabla o línea de otros consumos
        TLineas: function () {

            //return $('#bodyTabla tr:not([data-tipo="T"], [data-level="0"], [data-loaded="1"])').find('span[aria-expanded="false"]').parent();;
            return $('#bodyTabla tr:not([data-level="0"])');

        },

        //Lineas descendientes de la línea activa
        lineaDesActiva: function () {

            //return $("#bodyTabla [data-parent*='" + $("#bodyTabla tr.activa").attr("id") + "']").children();
            return $("#bodyTabla [data-parent*='" + $("#bodyTabla tr.activa").attr("id") + "']");
            //return $("#bodyTabla tr.activa:not([data-tipo='T']").children();

        },        

        //Línea activa
        lineaActiva: function () {

            return $("#bodyTabla tr.activa");

        },

        //Línea de cualquier nivel de la estructura técnica que se pasa por parámetro
        linea: function (idLinea) {

            return $('#' + idLinea);

        },

        //Línea del Proyecto Económico que se pasa por parámetro
        lineaPSN: function (psn) {

            return $('#PSN' + psn);

        },

        //Línea del Proyecto Técnico que se pasa por parámetro
        lineaPT: function (pt) {

            return $('#PT' + pt);

        },

        //Línea de la tarea que se pasa por parámetro
        lineaT: function (t) {

            return $('#T' + t);

        },

        //Lineas modificadas de estimación obligatoria
        lineasEstModificadas: function () {

            return $('#bodyTabla > tr.linea[data-changed=1][data-obligaest=1]');

        },

        //Lineas de tareas modificadas
        lineasModificadas: function () {

            return $('#bodyTabla > tr.linea[data-changed=1]');

        },

        //Buscar lineas que visualicen el texto pasado por parámetro
        buscarLineas: function(texto) {
            return $('#bodyTabla > tr > td.nombreLinea > span.fk_busqueda:icontains("' + texto + '"):visible').parent().parent();
        },

        //Devuelve las líneas no expandidas de un proyectosubnodo
        PSNDespliegueCompleto: function(nPSN) {
            return $('#bodyTabla tr[data-psn="' + nPSN + '"][data-loaded="0"]:not([data-tipo="T"], [data-level="0"])');
        },        

        lineasVisiblesEnTablaSinEventos: function () {
            return a = $('#bodyTabla tr:within-viewport[data-event="0"]');
        },

        lineasTareasVisiblesEnTablaSinEventos: function () {
            return a = $('#bodyTabla tr:within-viewport[data-event="0"][data-tipo="T"]');
    }

    }

    //Función de obtención de descripción desde el elemento html correspondiente
    var obtenerDescripcion = function (linea) {
        return linea.find(">:first-child >:nth-child(2)").html();
    }

    //Función de obtención de cabecera de día para consultar sus atributos
    var obtenerPropDia = function (dia) {
        return $('#' + dia);
    }

    var init = function () {        

        $(window).resize(function () {
            controlarScroll();
        });    
        
        //Se modifica el viewport del plugin a la tabla y el ancho al contenedor para que considere las filas en el viewport aunque exista scroll horizontal.
        withinviewport.defaults.container = $('.table-responsive');
        withinviewport.defaults.right = - $('#contenedor').width();
        withinviewport.defaults.left = - $('#contenedor').width();

        //Migas de pan
        var enlace = '<span><a href="/"></a></span><span> &gt; </span><span><a title="Informe de actividad y progreso">IAP</a></span><span> &gt; </span><span><a>Reporte</a></span><span> &gt; </span><span><a>Calendario</a></span><span> &gt; </span><span>Imputación diaria</span>';
        $("#Menu_SiteMap1").html(enlace);


        $('.table-responsive').on('touchstart', function (e) { });

        //if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
        //    indicadores.i_dispositivoTactil = true;
        //}                              

        $('#modalComentario').on('hidden.bs.modal', function () {
            //Para que al cerrar la modal los elementos de la pantalla principal estén visibles al SR
            $('.ocultable').attr('aria-hidden', 'false');
            //Se pasa el foco al elemento que estaba seleccionado anteriormente
            $(selectores.sel_preInput).focus();
        });

        //Para que al cerrar la modal de confirmación los elementos de la pantalla principal estén visibles al SR     
        $('#bsconfirm1').on('hidden.bs.modal', function () {
            $('.ocultable').attr('aria-hidden', 'false');
        });

        //Recepción del foco en el textarea del modal de comentario
        $('#modalComentario').on('shown.bs.modal', function () {
            $('#txtComentario').focus();
        });

        //Recepción del foco en el textarea del modal de busqueda
        $('#modalBusqueda').on('shown.bs.modal', function () {
            dom.txtSearch.focus();
        });                                                                        
        
        //Inicialización y validación de datepickers              
        $(document).on('focus', '.txtFecha:not(.hasDatepicker)', function (e) {
            $(this).datepicker({
                changeMonth: true,
                changeYear: true,
                defaultDate: $('#L').attr('data-date'),

                beforeShow: function (input, inst) {
                    $(this).removeClass('calendar-off').addClass('calendar-on');
                },
                onClose: function () {
                    $(this).removeClass('calendar-on').addClass('calendar-off');
                    if (moment($(this).val(), 'DD/MM/YYYY', 'es', true).isValid() && $(this).val() != $(this).attr('data-originalvalue')) {
                        $(this).change();
                    }
                }
            });
            $(this).change(function (e) {
                var input = $(this);
                if ($(this).val() != '') {
                    if (moment($(this).val(), 'DD/MM/YYYY', 'es', true).isValid() && $(this).val() != $(this).attr('data-originalvalue') && $(this).val() != $(this).attr('value')) {
                        var ultiImp = $(this).parent().parent().attr('data-fultiimp');
                        if (ultiImp == "" || (moment($(this).val().toDate()).diff(moment(ultiImp.toDate())) >= 0)) {//No se permite que la nueva FFE sea menor que la fecha de la ultima imputación del usuario en la tarea
                            actualizarFFE($(this));
                            $(this).attr('value', $(this).val());
                        } else {
                            $.when(IB.bsalert.fixedAlert("warning", "Error de validación", "La nueva fecha de fin estimada debe ser mayor a la fecha de la última imputación registrada en la tarea.")).then(function () {
                                input.val(input.attr('value'));
                                input.focus();
                            });
                        }                        
                    }
                } else {
                    actualizarFFE($(this));
                }
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

    var atacharEventosFilasTareasView = function (linea) {

        //Marcar la línea al entrar a un input de una línea abierta(timeout para I.E.)
        linea.find('td input').on('focus', function (e) {
            marcarLinea($(this).parent().parent());
            $(this).select();
        });

        //Marcar la línea 
        linea.on('click', function (e) {
            marcarLinea($(this));
        });

        //Se habilita el botón de Jornada al entrar en un input semanal
        linea.find('input.diasSemana').on('focus', function (e) {
            selectores.sel_preInput = $(this);
            habilitarBtn(dom.btnJornada, 1, imputarJornada);
            habilitarBtn(dom.btnJornadaLite, 1, imputarJornada);
        });

        //Si el elemento es un input de Lunes a Domingo y el elemento por el que se abandona un input es el botón de comentario o los botones de Jornada, etc se habilitan sus clickeos
        // y recogermos el input en el que está situado si no se deshabilitan los botones y se vacia la variable que contenía el input
        linea.find('input.diasSemana').on('focusout', function (e) {

            window.setTimeout(function () {
                var srcobj = $(document.activeElement).closest(":focusable");

                if (!($("#txtComentario").is(srcobj) || $("input.diasSemana").is(srcobj) || $("#btnComentario").is(srcobj) || $("#btnJornada").is(srcobj) || $("#btnJornadaLite").is(srcobj))) {

                    //Para GASVI solo necesitamos mantener el input que estaba seleccionado
                    if (!($("#btnGasvi").is(srcobj) || $("#btnGasviLite").is(srcobj))) selectores.sel_preInput = "";

                    habilitarBtn(dom.btnComentario, 0);
                    habilitarBtn(dom.btnJornada, 0);
                    habilitarBtn(dom.btnJornadaLite, 0);
                }
            }, 0);

        });

        //validación de numéricos en horas
        //$(document).on('keypress', 'input.txtHoras', function (e) {
        linea.find('input.txtHoras').on('keypress', function (e) {
            if (e.keyCode == 13) {
                //Si se ha pulsado intro se fija el foco en el siguiente elemento
                $('input').eq([$('input').index(this) + 1]).focus();
            } else {
                return validarTeclaNumerica(e, true);
            }
        });

        //Formateo de inputs de horas 
        linea.find('input.diasSemana').on('focusout', function (e) {
            validarFormatearHoras24($(this));
        });

        //Formateo y validación de input de ETE 
        linea.find('input.totalTarea').on('focusout', function (e) {
            validarFormatearETE($(this));
        });

    }

    var atacharEventosFilasTareasValoresCambiadosView = function (linea) {

        //Marcar la línea al entrar a un input de una línea abierta(timeout para I.E.)
        linea.find('td input.diasSemana').on('focus', function (e) {            
            marcarLinea($(this).parent().parent());
            $(this).select();
        });

        //Se habilita el botón de Jornada al entrar en un input semanal
        linea.find('input.diasSemana').on('focus', function (e) {
            selectores.sel_preInput = $(this);
            habilitarBtn(dom.btnJornada, 1, imputarJornada);
            habilitarBtn(dom.btnJornadaLite, 1, imputarJornada);
        });

        //Si el elemento es un input de Lunes a Domingo y el elemento por el que se abandona un input es el botón de comentario o los botones de Jornada, etc se habilitan sus clickeos
        // y recogermos el input en el que está situado si no se deshabilitan los botones y se vacia la variable que contenía el input
        linea.find('input.diasSemana').on('focusout', function (e) {

            window.setTimeout(function () {
                var srcobj = $(document.activeElement).closest(":focusable");

                if (!($("#txtComentario").is(srcobj) || $("input.diasSemana").is(srcobj) || $("#btnComentario").is(srcobj) || $("#btnJornada").is(srcobj) || $("#btnJornadaLite").is(srcobj))) {

                    //Para GASVI solo necesitamos mantener el input que estaba seleccionado
                    if (!($("#btnGasvi").is(srcobj) || $("#btnGasviLite").is(srcobj))) selectores.sel_preInput = "";

                    habilitarBtn(dom.btnComentario, 0);
                    habilitarBtn(dom.btnJornada, 0);
                    habilitarBtn(dom.btnJornadaLite, 0);
                }
            }, 0);

        });

        //validación de numéricos en horas
        //$(document).on('keypress', 'input.txtHoras', function (e) {
        linea.find('input.diasSemana').on('keypress', function (e) {
            if (e.keyCode == 13) {
                //Si se ha pulsado intro se fija el foco en el siguiente elemento
                $('input').eq([$('input').index(this) + 1]).focus();
            } else {
                return validarTeclaNumerica(e, true);
            }
        });

        //Formateo de inputs de horas 
        linea.find('input.diasSemana').on('focusout', function (e) {
            validarFormatearHoras24($(this));
        });

        //Formateo y validación de input de ETE 
        linea.find('input.totalTarea').on('focusout', function (e) {
            validarFormatearETE($(this));
        });

    }

    var atacharEventosFilasPTPSNView = function (linea) {        

        //Marcar la línea de PE y PT
        linea.on('click', function (e) {
            marcarLinea($(this));
        });

    }

    //Función de cebreo de las filas de la tabla
    function cebrear() {

        $("#tabla > tbody > tr:not(.bg-info)").removeClass("cebreada");
        $('#tabla > tbody > tr:visible:not(.bg-info, activa):even').addClass('cebreada');
        controlarScroll();

    }

    function controlarScroll() {

        /*Controlamos si el contenedor tiene Scroll*/
        var div = document.getElementById('contenedor');

        var scrollWidth = $('#contenedor').width() - div.scrollWidth;

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;

        if (hasVerticalScrollbar) {

            $(".div-table").css("width", "calc( " + $('#contenedor').width() + "px - " + scrollWidth + "px )");
            $("#tablaCabecera").css("width", "calc( " + $('#contenedor').width() + "px - " + scrollWidth + "px )");
            $("#tablaPie").css("width", "calc( " + $('#contenedor').width() + "px - " + scrollWidth + "px )");
        }
        else {
            $(".div-table").css("width", "" + $('#contenedor').width() + "px")
            $("#tablaCabecera").css("width", "" + $('#contenedor').width() + "px")
            $("#tablaPie").css("width", "" + $('#contenedor').width() + "px")
        }        

    }

    var pintarCabeceraSemana = function() {

        //Pintado de semana actual
        $('.txtSemana').html(IB.vars.strRango);
        $('.txtSemanaMini').html(IB.vars.strRangoMini);

    }

    var visualizarContenedor = function () {

        $(selectores.container).css("visibility", "visible");

    }

    //Función que se llama si se vuelve de gasvi para posicionar el foco en la línea que lo contenia 
    //antes de abandonar la página y abrir la jerarquia superior de esta línea
    var posicionarFoco = function () {

        var linea = $('#' + IB.vars.filaSel);

        marcarLinea(linea);

        //var padres = linea.attr('data-parent').split(" ");

        //for (var i = 0; i < (padres.length) ; i++) { 
            
        //    abrirLinea($('#' + padres[i]).children(":first"), 1);

        //}

        //Si había una celda de fecha seleccionada al ir a gasvi se selecciona al volver
        if (IB.vars.fechaSel != "") {
            $('#' + IB.vars.filaSel + ' > td > input[data-date="' + IB.vars.fechaSel + '"]').focus();
        }
        
    }

    //Marcado de línea activa
    var marcarLinea = function (thisObj) {

        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        desmarcarLinea();
        $(thisObj).addClass('activa');

        //Eliminamos el texto ' - Seleccionado' del elemento seleccionado anterior. Las posiciones 0 y 1 contienen el tipo de linea y su descripción.
        $('span:contains("- Seleccionado")').each(function () {
            $(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1] + ' - ' + $(this).text().split(" - ")[2]);
        })

        //Añadimos el texo '- Seleccionado' al elemento seleccionado actualmente
        $(thisObj).children().children(":nth-child(3n)").text($(thisObj).children().children(":nth-child(3n)").text() + ' - Seleccionado');

        //Si la línea marcada es una tarea se abre el botón de tarea y de semana
        if ($(thisObj).attr('data-tipo') == "T") {
            habilitarBtn(dom.btnTarea, 1, redirigirTarea);
            habilitarBtn(dom.btnTareaLite, 1, redirigirTarea);
            habilitarBtn(dom.btnSemana, 1, imputarSemana);
            habilitarBtn(dom.btnSemanaLite, 1, imputarSemana);
        }
        if ($(thisObj).length > 0) setIconoBitacora($(thisObj).attr('data-tipo'), $(thisObj).attr('data-bitacora'));
    }

    //Desmarcado de línea activa
    var desmarcarLinea = function () {

        $("#bodyTabla tr.activa").removeClass('activa');
        habilitarBtn(dom.btnTarea, 0);
        habilitarBtn(dom.btnTareaLite, 0);
        habilitarBtn(dom.btnSemana, 0);
        habilitarBtn(dom.btnSemanaLite, 0);

        //No mostramos botón de acceso a bitácora
        setIconoBitacora("A", null);

    }

    //Asignación de totales al entity e inserción de información en la cabecera y el pie
    var pintarCabeceraPie = function (data, dias) {

        if (dias.lunes.diaSemana !== null) {
            dias.lunes.totalConsumo = data.tot_Lunes;
            $('#L').attr('data-date', moment(dias.lunes.fechaSemana).format("DD/MM/YYYY"));
            $('#L').attr('data-horasJornada', dias.lunes.horasJornada);
            $('#L').children().html('L. ' + dias.lunes.diaSemana).attr('title', 'Lunes ' + dias.lunes.diaSemana);
            $('#L').attr('data-laborable', dias.lunes.laborable);
            $('#L').attr('data-festivo', dias.lunes.festivo);
            if ((!dias.lunes.laborable) || dias.lunes.festivo) {
                $('#L').css('color', '#F58D8D');
            } else {
                $('#L').css('color', '#fff');
            }
            $('.PieL').children().html(accounting.formatNumber(dias.lunes.totalConsumo));
            $('.PieL').attr('data-originalValue', accounting.formatNumber(dias.lunes.totalConsumo));
            $('.PieL').attr('aria-label', 'Lunes ' + dias.lunes.diaSemana);

        }
        else {
            $('.PieL').children().addClass('fk-elementosvacios');
            $('#L').removeAttr('data-date');
            $('#L').removeAttr('data-horasJornada');
            $('#L').children().html('L. ').attr('title', 'L. ');
            $('#L').removeAttr('data-laborable');
            $('#L').removeAttr('data-festivo');
            $('#L').css('color', '#fff');
            $('.PieL').children().html("");
            $('.PieL').removeAttr('data-originalValue');
            $('.PieL').attr('aria-label', '');
        }

        if (dias.martes.diaSemana !== null) {
            dias.martes.totalConsumo = data.tot_Martes;
            $('#M').attr('data-date', moment(dias.martes.fechaSemana).format("DD/MM/YYYY"));
            $('#M').attr('data-horasJornada', dias.martes.horasJornada);
            $('#M').children().html('M. ' + dias.martes.diaSemana).attr('title', 'Martes ' + dias.martes.diaSemana);
            $('#M').attr('data-laborable', dias.martes.laborable);
            $('#M').attr('data-festivo', dias.martes.festivo);
            if ((!dias.martes.laborable) || dias.martes.festivo) {
                $('#M').css('color', '#F58D8D');
            } else {
                $('#M').css('color', '#fff');
            }
            $('.PieM').children().html(accounting.formatNumber(dias.martes.totalConsumo));
            $('.PieM').attr('data-originalValue', accounting.formatNumber(dias.martes.totalConsumo));
            $('.PieM').attr('aria-label', 'Martes ' + dias.martes.diaSemana);
        }
        else {
            $('.PieM').children().addClass('fk-elementosvacios');
            $('#M').removeAttr('data-date');
            $('#M').removeAttr('data-horasJornada');
            $('#M').children().html('M. ').attr('title', 'M. ');
            $('#M').removeAttr('data-laborable');
            $('#M').removeAttr('data-festivo');
            $('#M').css('color', '#fff');
            $('.PieM').children().html("");
            $('.PieM').removeAttr('data-originalValue');
            $('.PieM').attr('aria-label', '');
        }

        if (dias.miercoles.diaSemana !== null) {
            dias.miercoles.totalConsumo = data.tot_Miercoles;
            $('#X').attr('data-date', moment(dias.miercoles.fechaSemana).format("DD/MM/YYYY"));
            $('#X').attr('data-horasJornada', dias.miercoles.horasJornada);
            $('#X').children().html('X. ' + dias.miercoles.diaSemana).attr('title', 'Miércoles ' + dias.miercoles.diaSemana);
            $('#X').attr('data-laborable', dias.miercoles.laborable);
            $('#X').attr('data-festivo', dias.miercoles.festivo);
            if ((!dias.miercoles.laborable) || dias.miercoles.festivo) {
                $('#X').css('color', '#F58D8D');
            } else {
                $('#X').css('color', '#fff');
            }
            $('.PieX').children().html(accounting.formatNumber(dias.miercoles.totalConsumo));
            $('.PieX').attr('data-originalValue', accounting.formatNumber(dias.miercoles.totalConsumo));
            $('.PieX').attr('aria-label', 'Miércoles ' + dias.miercoles.diaSemana);
        }
        else {
            $('.PieX').children().addClass('fk-elementosvacios');
            $('#X').removeAttr('data-date');
            $('#X').removeAttr('data-horasJornada');
            $('#X').children().html('X. ').attr('title', 'X. ');
            $('#X').removeAttr('data-laborable');
            $('#X').removeAttr('data-festivo');
            $('#X').css('color', '#fff');
            $('.PieX').children().html("");
            $('.PieX').removeAttr('data-originalValue');
            $('.PieX').attr('aria-label', '');
        }

        if (dias.jueves.diaSemana !== null) {
            dias.jueves.totalConsumo = data.tot_Jueves;
            $('#J').attr('data-date', moment(dias.jueves.fechaSemana).format("DD/MM/YYYY"));
            $('#J').attr('data-horasJornada', dias.jueves.horasJornada);
            $('#J').children().html('J. ' + dias.jueves.diaSemana).attr('title', 'Jueves ' + dias.jueves.diaSemana);
            $('#J').attr('data-laborable', dias.jueves.laborable);
            $('#J').attr('data-festivo', dias.jueves.festivo);
            if ((!dias.jueves.laborable) || dias.jueves.festivo) {
                $('#J').css('color', '#F58D8D');
            } else {
                $('#J').css('color', '#fff');
            }
            $('.PieJ').children().html(accounting.formatNumber(dias.jueves.totalConsumo));
            $('.PieJ').attr('data-originalValue', accounting.formatNumber(dias.jueves.totalConsumo));
            $('.PieJ').attr('aria-label', 'Jueves ' + dias.jueves.diaSemana);
        }
        else {
            $('.PieJ').children().addClass('fk-elementosvacios');
            $('#J').removeAttr('data-date');
            $('#J').removeAttr('data-horasJornada');
            $('#J').children().html('J. ').attr('title', 'J. ');
            $('#J').removeAttr('data-laborable');
            $('#J').removeAttr('data-festivo');
            $('#J').css('color', '#fff');
            $('.PieJ').children().html("");
            $('.PieJ').removeAttr('data-originalValue');
            $('.PieJ').attr('aria-label', '');
        }

        if (dias.viernes.diaSemana !== null) {
            dias.viernes.totalConsumo = data.tot_Viernes;
            $('#V').attr('data-date', moment(dias.viernes.fechaSemana).format("DD/MM/YYYY"));
            $('#V').attr('data-horasJornada', dias.viernes.horasJornada);
            $('#V').children().html('V. ' + dias.viernes.diaSemana).attr('title', 'Viernes ' + dias.viernes.diaSemana);
            $('#V').attr('data-laborable', dias.viernes.laborable);
            $('#V').attr('data-festivo', dias.viernes.festivo);
            if ((!dias.viernes.laborable) || dias.viernes.festivo) {
                $('#V').css('color', '#F58D8D');
            } else {
                $('#V').css('color', '#fff');
            }
            $('.PieV').children().html(accounting.formatNumber(dias.viernes.totalConsumo));
            $('.PieV').attr('data-originalValue', accounting.formatNumber(dias.viernes.totalConsumo));
            $('.PieV').attr('aria-label', 'Viernes ' + dias.viernes.diaSemana);
        }
        else {
            $('.PieV').children().addClass('fk-elementosvacios');
            $('#V').removeAttr('data-date');
            $('#V').removeAttr('data-horasJornada');
            $('#V').children().html('V. ').attr('title', 'V. ');
            $('#V').removeAttr('data-laborable');
            $('#V').removeAttr('data-festivo');
            $('#V').css('color', '#fff');
            $('.PieV').children().html("");
            $('.PieV').removeAttr('data-originalValue');
            $('.PieV').attr('aria-label', 'V');
        }

        if (dias.sabado.diaSemana !== null) {
            dias.sabado.totalConsumo = data.tot_Sabado;
            $('#S').attr('data-date', moment(dias.sabado.fechaSemana).format("DD/MM/YYYY"));
            $('#S').attr('data-horasJornada', dias.sabado.horasJornada);
            $('#S').children().html('S. ' + dias.sabado.diaSemana).attr('title', 'Sábado ' + dias.sabado.diaSemana);
            $('#S').attr('data-laborable', dias.sabado.laborable);
            $('#S').attr('data-festivo', dias.sabado.festivo);
            if ((!dias.sabado.laborable) || dias.sabado.festivo) {
                $('#S').css('color', '#F58D8D');
            } else {
                $('#S').css('color', '#fff');
            }
            $('.PieS').children().html(accounting.formatNumber(dias.sabado.totalConsumo));
            $('.PieS').attr('data-originalValue', accounting.formatNumber(dias.sabado.totalConsumo));
            $('.PieS').attr('aria-label', 'Sábado ' + dias.sabado.diaSemana);
        }
        else {
            $('.PieS').children().addClass('fk-elementosvacios');
            $('#S').removeAttr('data-date');
            $('#S').removeAttr('data-horasJornada');
            $('#S').children().html('S. ').attr('title', 'S. ');
            $('#S').removeAttr('data-laborable');
            $('#S').removeAttr('data-festivo');
            $('#S').css('color', '#fff');
            $('.PieS').children().html("");
            $('.PieS').removeAttr('data-originalValue');
            $('.PieS').attr('aria-label', '');
        }

        if (dias.domingo.diaSemana !== null) {
            dias.domingo.totalConsumo = data.tot_Domingo;
            $('#D').attr('data-date', moment(dias.domingo.fechaSemana).format("DD/MM/YYYY"));
            $('#D').attr('data-horasJornada', dias.domingo.horasJornada);
            $('#D').children().html('D. ' + dias.domingo.diaSemana).attr('title', 'Domingo ' + dias.domingo.diaSemana);
            $('#D').attr('data-laborable', dias.domingo.laborable);
            $('#D').attr('data-festivo', dias.domingo.festivo);
            if ((!dias.domingo.laborable) || dias.domingo.festivo) {
                $('#D').css('color', '#F58D8D');
            } else {
                $('#D').css('color', '#fff');
            }
            $('.PieD').children().html(accounting.formatNumber(dias.domingo.totalConsumo));
            $('.PieD').attr('data-originalValue', accounting.formatNumber(dias.domingo.totalConsumo));
            $('.PieD').attr('aria-label', 'Domingo ' + dias.domingo.diaSemana);
        }
        else {
            $('.PieD').children().addClass('fk-elementosvacios');
            $('#D').removeAttr('data-date');
            $('#D').removeAttr('data-horasJornada');
            $('#D').children().html('D. ').attr('title', 'D. ');
            $('#D').removeAttr('data-laborable');
            $('#D').removeAttr('data-festivo');
            $('#D').css('color', '#fff');
            $('.PieD').children().html("");
            $('.PieD').removeAttr('data-originalValue');
            $('.PieD').attr('aria-label', '');
        }

    }        

    //Pintado de la tabla con los Proyectos Económicos
    var pintarTablaPSN = function (data, dias) {
        var inicio = Date.now();
        var sHtml = new StringBuilder();
        var content;

        for (var i = 0; i < data.length; i++) {

            if (dias.lunes.diaSemana !== null) dias.lunes.totalConsumoProys += data[i].esf_Lunes;
            if (dias.martes.diaSemana !== null) dias.martes.totalConsumoProys += data[i].esf_Martes;
            if (dias.miercoles.diaSemana !== null) dias.miercoles.totalConsumoProys += data[i].esf_Miercoles;
            if (dias.jueves.diaSemana !== null) dias.jueves.totalConsumoProys += data[i].esf_Jueves;
            if (dias.viernes.diaSemana !== null) dias.viernes.totalConsumoProys += data[i].esf_Viernes;
            if (dias.sabado.diaSemana !== null) dias.sabado.totalConsumoProys += data[i].esf_Sabado;
            if (dias.domingo.diaSemana !== null) dias.domingo.totalConsumoProys += data[i].esf_Domingo;

            content = '<b>Proyecto:</b> ' + data[i].denominacion.replace(/"/g, "'") + '<br /> <b>Responsable:</b> ' + data[i].Responsable + '<br /> <b>C.R.:</b> ' + data[i].t303_denominacion + '<br /> <b>Cliente:</b> ' + data[i].t302_denominacion;

            sHtml.append('<tr id="PSN' + data[i].t305_idproyectosubnodo + '" data-tipo="PSN" data-estado="' + data[i].t301_estado + '" class="linea" data-parent="" data-level="1" data-psn="' + data[i].t305_idproyectosubnodo + '" data-idnaturaleza="' + data[i].t323_idnaturaleza + '" data-bitacora="' + data[i].AccesoBitacora + '" data-gasvi="' + data[i].t305_imputablegasvi + '" data-loaded="0" data-event="0"');
            sHtml.append('data-sr="Proyecto Económico');
            data[i].t301_estado == "A" ? sHtml.append(' Abierto') : sHtml.append(' Cerrado');
            sHtml.append(' - ' + accounting.formatNumber(data[i].t301_idproyecto, 0) + ' - ' + data[i].denominacion.replace(/"/g, "'") + ' (Nivel 1)">');
            sHtml.append('<td headers="PSN' + data[i].t305_idproyectosubnodo + '" class="nombreLinea Nivel1"><span class="glyphicon-plus">');
            data[i].t301_estado == "A" ? sHtml.append('<span aria-hidden="true" class="fa fa-diamond fa-diamond-verde fk-elementosvacios" data-placement="top" data-toggle="popover" data-content="' + content + '" title="<b>Información</b>"></span>') : sHtml.append('<span aria-hidden="true" class="fa fa-diamond fa-diamond-rojo"></span>');
            sHtml.append('</span><span class="fk_busqueda" aria-hidden="true" >' + accounting.formatNumber(data[i].t301_idproyecto, 0) + ' - ' + data[i].denominacion.replace(/"/g, "'") + '</span>');            
            sHtml.append('</td>');

            sHtml.append('<td headers="L" class="celdaHoras"><span>');
            if (data[i].esf_Lunes > 0) sHtml.append(accounting.formatNumber(data[i].esf_Lunes));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="M" class="celdaHoras"><span>');
            if (data[i].esf_Martes > 0) sHtml.append(accounting.formatNumber(data[i].esf_Martes));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="X" class="celdaHoras"><span>');
            if (data[i].esf_Miercoles > 0) sHtml.append(accounting.formatNumber(data[i].esf_Miercoles));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="J" class="celdaHoras"><span>');
            if (data[i].esf_Jueves > 0) sHtml.append(accounting.formatNumber(data[i].esf_Jueves));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="V" class="celdaHoras"><span>');
            if (data[i].esf_Viernes > 0) sHtml.append(accounting.formatNumber(data[i].esf_Viernes));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="S" class="celdaHoras"><span>');
            if (data[i].esf_Sabado > 0) sHtml.append(accounting.formatNumber(data[i].esf_Sabado));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="D" class="celdaHoras"><span>');
            if (data[i].esf_Domingo > 0) sHtml.append(accounting.formatNumber(data[i].esf_Domingo));
            sHtml.append('</span></td>');

            //OTC
            sHtml.append('<td headers="OTC" class="celdaHoras">');
            if (data[i].t346_codpst !== null) {
                sHtml.append('<span class="txtOT" title="' + data[i].t346_codpst + '">' + data[i].t346_codpst + '</span>');
            } else {
                sHtml.append('<span></span>');
            }
            sHtml.append('</td>');

            //OTL
            sHtml.append('<td headers="OTL" class="celdaHoras">');
            if (data[i].t332_otl !== null) {
                sHtml.append('<span class="txtOT" title="' + data[i].t332_otl + '">' + data[i].t332_otl + '</span>');
            } else {
                sHtml.append('<span></span>');
            }
            sHtml.append('</td>');

            sHtml.append('<td headers="ETE" class="celdaHoras"><span>');
            if (data[i].TotalEstimado != null && data[i].TotalEstimado > 0) sHtml.append(accounting.formatNumber(data[i].TotalEstimado));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="FFE" class="celdaFecha"><span>');
            if (data[i].FinEstimado !== null) sHtml.append(moment(data[i].FinEstimado).format("DD/MM/YYYY"));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="F" class="celdaHoras"><span></span></td>');

            sHtml.append('<td headers="EAT" class="celdaHoras"><span>');
            if (data[i].EsfuerzoTotalAcumulado > 0) sHtml.append(accounting.formatNumber(data[i].EsfuerzoTotalAcumulado));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="EP" class="celdaHoras"><span>');
            //if (data[i].Pendiente > 0) sHtml.append(accounting.formatNumber(data[i].Pendiente));
            if (data[i].Pendiente != null && data[i].Pendiente > 0) sHtml.append(accounting.formatNumber(data[i].Pendiente));
            sHtml.append('</span></td>');

            sHtml.append('</tr>');

        }

        $('#bodyTabla').html(sHtml.toString());


        //if (!indicadores.i_dispositivoTactil) {
            $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true });
        //}

        //Se enmascaran los campos de importes        
        $('.diasSemana').mask('99,99');
        $('.totalTarea').mask('999999,99');

        //$("#tabla tbody tr td span:empty").addClass("fk-elementosvacios");

        cebrear();
        $('#ContadorRender').html(Date.now() - inicio + ' ms.');
    }

    //Pintado de la tabla con el desglose a primer nivel o a todos los niveles de un Proyecto Económico, Proyecto Técnico, una Fase o una Actividad
    var pintarTablaDesglose = function (data, idPadre, dias, matchear) {
        var inicio = Date.now();
        var sHtml = new StringBuilder();
        var sfechaDia;
        var content;

        for (var i = 0; i < data.length; i++) {            

            if (data[i].tipo != "T") { //Si es PSN, PT, A ó F


                switch (data[i].tipo) {

                    case "PT":

                        if (matchear && existeLinea("" + data[i].tipo + data[i].t331_idpt)) {                            

                            if (sHtml.strings.length > 1) {
                                renderizarLineasDesglose(sHtml.toString());
                                sHtml = new StringBuilder();
                            }                            

                            indicadores.i_lineaPadre = data[i].tipo + data[i].t331_idpt;
                            continue;
                        } 

                        sHtml.append('<tr id="PT' + data[i].t331_idpt + '" data-tipo="PT" class="linea" data-parent="PSN' + data[i].t305_idproyectosubnodo + '" data-level="' + data[i].nivel + '" data-psn="' + data[i].t305_idproyectosubnodo + '" data-pt="' + data[i].t331_idpt + '" data-bitacora="' + data[i].AccesoBitacora + '" data-gasvi="' + data[i].t305_imputablegasvi + '" data-loaded="0" data-event="0" ');
                        sHtml.append('data-sr="Proyecto Técnico - ' + data[i].t331_idpt + ' - ' + data[i].denominacion.replace(/"/g, "'") + ' (Nivel ' + data[i].nivel + ')">');
                        sHtml.append('<td headers="PT' + data[i].t331_idpt + '" class="nombreLinea Nivel' + data[i].nivel + '">');
                        sHtml.append('<span class="glyphicon-plus">');
                        sHtml.append('<span aria-hidden="true" class="fa-stack fa-lg fa-stack-linea"><i class="fa fa-circle fa-stack-1x circuloLinea"></i><i class="fa fa-stack-1x letraLinea">P</i></span>');
                        sHtml.append('</span><span class="fk_busqueda">' + data[i].denominacion + '</span>');                        
                        sHtml.append('</td>');

                        break;

                    case "F":

                        if (matchear && existeLinea("" + data[i].tipo + data[i].t334_idfase)) {

                            if (sHtml.strings.length > 1) {
                                renderizarLineasDesglose(sHtml.toString());
                                sHtml = new StringBuilder();
                            }

                            indicadores.i_lineaPadre = data[i].tipo + data[i].t334_idfase;
                            continue;
                        }

                        sHtml.append('<tr id="F' + data[i].t334_idfase + '" data-tipo="F" class="linea" data-parent= "' + obtenerPadre(data[i]) + '" data-level="' + data[i].nivel + '" data-psn="' + data[i].t305_idproyectosubnodo + '" data-pt="' + data[i].t331_idpt + '" data-f="' + data[i].t334_idfase + '" data-gasvi="' + data[i].t305_imputablegasvi + '" data-loaded="0" data-event="0" ');
                        sHtml.append('data-sr="Fase - ' + data[i].t334_idfase + ' - ' + data[i].denominacion.replace(/"/g, "'") + ' (Nivel ' + data[i].nivel + ')">');
                        sHtml.append('<td headers="F' + data[i].t334_idfase + '" class="nombreLinea Nivel' + data[i].nivel + '">');
                        sHtml.append('<span class="glyphicon-plus">');
                        sHtml.append('<span aria-hidden="true" class="fa-stack fa-lg fa-stack-linea"><i class="fa fa-circle fa-stack-1x circuloLinea"></i><i class="fa fa-stack-1x letraLinea">' + data[i].tipo + '</i></span>');
                        sHtml.append('</span><span class="fk_busqueda">' + data[i].denominacion + '</span>');                        
                        sHtml.append('</td>');

                        break;

                    case "A":

                        if (matchear && existeLinea("" + data[i].tipo + data[i].t335_idactividad)) {

                            if (sHtml.strings.length > 1) {
                                renderizarLineasDesglose(sHtml.toString());
                                sHtml = new StringBuilder();
                            }

                            indicadores.i_lineaPadre = data[i].tipo + data[i].t335_idactividad;
                            continue;
                        }

                        var nFase;
                        data[i].t334_idfase == null ? nFase = 0 : nFase = data[i].t334_idfase;

                        sHtml.append('<tr id="A' + data[i].t335_idactividad + '" data-tipo="A" class="linea" data-parent= "' + obtenerPadre(data[i]) + '" data-level="' + data[i].nivel + '" data-psn="' + data[i].t305_idproyectosubnodo + '" data-pt="' + data[i].t331_idpt + '" data-f="' + nFase + '" data-a="' + data[i].t335_idactividad + '" data-gasvi="' + data[i].t305_imputablegasvi + '" data-loaded="0" data-event="0"');
                        sHtml.append('data-sr="Actividad - ' + data[i].t335_idactividad + ' - ' + data[i].denominacion.replace(/"/g, "'") + ' (Nivel ' + data[i].nivel + ')">');
                        sHtml.append('<td headers="A' + data[i].t335_idactividad + '" class="nombreLinea Nivel' + data[i].nivel + '">');
                        sHtml.append('<span class="glyphicon-plus">');
                        sHtml.append('<span aria-hidden="true" class="fa-stack fa-lg fa-stack-linea"><i class="fa fa-circle fa-stack-1x circuloLinea"></i><i class="fa fa-stack-1x letraLinea">' + data[i].tipo + '</i></span>');
                        sHtml.append('</span><span class="fk_busqueda">' + data[i].denominacion + '</span>');                        
                        sHtml.append('</td>');

                        break;
                }

                sHtml.append('<td headers="L" class="celdaHoras"><span>');
                if (data[i].esf_Lunes > 0) sHtml.append(accounting.formatNumber(data[i].esf_Lunes));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="M" class="celdaHoras"><span>');
                if (data[i].esf_Martes > 0) sHtml.append(accounting.formatNumber(data[i].esf_Martes));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="X" class="celdaHoras"><span>');
                if (data[i].esf_Miercoles > 0) sHtml.append(accounting.formatNumber(data[i].esf_Miercoles));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="J" class="celdaHoras"><span>');
                if (data[i].esf_Jueves > 0) sHtml.append(accounting.formatNumber(data[i].esf_Jueves));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="V" class="celdaHoras"><span>');
                if (data[i].esf_Viernes > 0) sHtml.append(accounting.formatNumber(data[i].esf_Viernes));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="S" class="celdaHoras"><span>');
                if (data[i].esf_Sabado > 0) sHtml.append(accounting.formatNumber(data[i].esf_Sabado));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="D" class="celdaHoras"><span>');
                if (data[i].esf_Domingo > 0) sHtml.append(accounting.formatNumber(data[i].esf_Domingo));
                sHtml.append('</span></td>');

                //OTC
                sHtml.append('<td headers="OTC" class="celdaHoras">');
                if (data[i].t346_codpst !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t346_codpst + '">' + data[i].t346_codpst + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }
                sHtml.append('</td>');

                //OTL
                sHtml.append('<td headers="OTL" class="celdaHoras">');
                if (data[i].t332_otl !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t332_otl + '">' + data[i].t332_otl + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }
                sHtml.append('</td>');

                sHtml.append('<td headers="ETE" class="celdaHoras"><span>');
                if (data[i].TotalEstimado != null && data[i].TotalEstimado > 0) sHtml.append(accounting.formatNumber(data[i].TotalEstimado));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="FFE" class="celdaFecha"><span>');
                if (data[i].FinEstimado !== null) sHtml.append(moment(data[i].FinEstimado).format("DD/MM/YYYY"));
                sHtml.append('</span></td>');

                sHtml.append('<td headers="F" class="celdaHoras"><span></span></td>');

            }

            else {

                if (matchear && existeLinea("" + data[i].tipo + data[i].t332_idtarea)) {

                    if (sHtml.strings.length > 1) {
                        renderizarLineasDesglose(sHtml.toString());
                        sHtml = new StringBuilder();
                    }

                    indicadores.i_lineaPadre = data[i].tipo + data[i].t332_idtarea;
                    continue;
                }

                var bEstadoLectura = false;
                var bAnulada = false;
                var bDisabled;
                var estadoTarea, descEstado;
                var estadoDia = "";
                var fAlta = new Date(moment(data[i].t330_falta));
                var fBaja;

                data[i].t330_fbaja != null ? fBaja = new Date(moment(data[i].t330_fbaja)) : fBaja = null;

                switch (data[i].t332_estado)//Estado
                {
                    case 0:
                        estadoTarea = " paralizada";
                        descEstado = " Tarea paralizada.";
                        bEstadoLectura = true;
                        break;
                    case 1://Activo
                        estadoTarea = "";
                        descEstado = "";
                        if (data[i].t331_obligaest == 1) {
                            estadoTarea = " obligaest";
                            descEstado = " Tarea de estimación obligatoria.";
                        }
                        break;
                    case 2:
                        estadoTarea = " pendiente";
                        descEstado = " Tarea pendiente.";
                        bEstadoLectura = true;
                        break;
                    case 3:
                        estadoTarea = " finalizada";
                        descEstado = " Tarea finalizada.";
                        if (data[i].t332_impiap == 0) bEstadoLectura = true;  //si impiap = 0, lectura
                        break;
                    case 4:
                        estadoTarea = " cerrada";
                        descEstado = " Tarea cerrada.";
                        if (data[i].t332_impiap == 0) bEstadoLectura = true;
                        break;
                    case 5:
                        estadoTarea = " anulada";
                        descEstado = " Tarea anulada.";
                        bAnulada = true;
                        break;
                }

                if (data[i].t301_estado == "C") bEstadoLectura = true;

                var ultiImp;

                data[i].fultiimp != null ? ultiImp = moment(data[i].fultiimp).format("DD/MM/YYYY"): ultiImp = "" ;

                sHtml.append('<tr id="T' + data[i].t332_idtarea + '" data-tipo="' + data[i].tipo + '" data-estado="' + data[i].t332_estado + '" class="linea" data-parent= "' + obtenerPadre(data[i]) + '" data-level="' + data[i].nivel + '" data-psn="' + data[i].t305_idproyectosubnodo + '" data-pt="' + data[i].t331_idpt + '" data-f="' + data[i].t334_idfase + '" data-a="' + data[i].t335_idactividad + '" data-t="' + data[i].t332_idtarea + '" data-obligaest="' + data[i].t331_obligaest + '" data-regjornocompleta="' + data[i].t323_regjornocompleta + '" data-imp="' + data[i].t332_impiap + '" data-fin="' + data[i].t336_completado + '" data-bitacora="' + data[i].AccesoBitacora + '" data-fultiimp="' + ultiImp + '" data-gasvi="' + data[i].t305_imputablegasvi + '" data-fultiimp_temp="" data-changed="0" data-loaded="1" data-event="0" ');
                sHtml.append('data-sr="Tarea - ' + accounting.formatNumber(data[i].t332_idtarea, 0) + ' - ' + data[i].denominacion + ' (Nivel ' + data[i].nivel + ')' + descEstado + '">');
                sHtml.append('<td headers="T' + data[i].t332_idtarea + '" class="nombreLinea Nivel' + data[i].nivel + '">');

                sHtml.append('<span class="glyphicon">');

                sHtml.append('<span aria-hidden="true" class="fa-stack fa-lg fa-stack-linea"><i class="fa fa-circle fa-stack-1x circuloLinea"></i><i class="fa fa-stack-1x letraLinea">' + data[i].tipo + '</i></span>');
                sHtml.append('</span><span class="fk_busqueda' + estadoTarea + '">' + accounting.formatNumber(data[i].t332_idtarea, 0) + ' - ' + data[i].denominacion + '</span>');                
                sHtml.append('</td>');

                //LUNES//
                sfechaDia = dias.lunes.fechaSemana
                sHtml.append('<td headers="L" class="celdaHoras"');                
                if (sfechaDia != null) {
                    var claseInputLunes = "";
                    bDisabled = false;

                    if (data[i].out_Lunes == 1) { // Está fuera de proyecto
                        claseInputLunes += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Lunes == 0) { // Está fuera de vigencia
                            claseInputLunes += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Lunes == 0) { // Es no laborable
                                claseInputLunes += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Lunes == 1) { // Es vacación
                                    claseInputLunes += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Lunes == "1") claseInputLunes += " comentario";
                    var ariaLabelLunes = "Tarea " + data[i].denominacion + " - " + $('#L > abbr').attr('title');
                    if (estadoDia != "") ariaLabelLunes += " - " + estadoDia;
                    var dateLunes = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueLunes="", valueJornLunes="";
                    //data[i].esf_Lunes > 0 ? valueLunes = accounting.formatNumber(data[i].esf_Lunes) : valueLunes = "";
                    if (data[i].esf_Lunes > 0) {
                        valueLunes = accounting.formatNumber(data[i].esf_Lunes);
                        valueJornLunes = accounting.formatNumber(data[i].esfJorn_Lunes);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledLunes = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledLunes = 1;
                    sHtml.append(' data-input-class="' + claseInputLunes + '"');
                    sHtml.append(' data-input-date="' + dateLunes + '"');
                    sHtml.append(' data-input-value="' + valueLunes + '"');
                    sHtml.append(' data-input-jorn="' + valueJornLunes + '"');
                    sHtml.append(' data-input-disabled="' + disabledLunes + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelLunes + '"');
                }

                sHtml.append('></td>');

                //MARTES//
                estadoDia = "";
                sfechaDia = dias.martes.fechaSemana
                sHtml.append('<td headers="M" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputMartes = "";
                    bDisabled = false;

                    if (data[i].out_Martes == 1) { // Está fuera de proyecto
                        claseInputMartes += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Martes == 0) { // Está fuera de vigencia
                            claseInputMartes += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Martes == 0) { // Es no laborable
                                claseInputMartes += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Martes == 1) { // Es vacación
                                    claseInputMartes += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Martes == "1") claseInputMartes += " comentario";
                    var ariaLabelMartes = "Tarea " + data[i].denominacion + " - " + $('#M > abbr').attr('title');
                    if (estadoDia != "") ariaLabelMartes += " - " + estadoDia;
                    var dateMartes = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueMartes="", valueJornMartes="";
                    //data[i].esf_Martes > 0 ? valueMartes = accounting.formatNumber(data[i].esf_Martes) : valueMartes = "";
                    if (data[i].esf_Martes > 0) {
                        valueMartes = accounting.formatNumber(data[i].esf_Martes);
                        valueJornMartes = accounting.formatNumber(data[i].esfJorn_Martes);
                    }

                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledMartes = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledMartes = 1;
                    sHtml.append(' data-input-class="' + claseInputMartes + '"');
                    sHtml.append(' data-input-date="' + dateMartes + '"');
                    sHtml.append(' data-input-value="' + valueMartes + '"');
                    sHtml.append(' data-input-jorn="' + valueJornMartes + '"');
                    sHtml.append(' data-input-disabled="' + disabledMartes + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelMartes + '"');
                }
                sHtml.append('></td>');


                //MIERCOLES//
                estadoDia = "";
                //sfechaDia = $('#X').attr('data-date');
                sfechaDia = dias.miercoles.fechaSemana
                sHtml.append('<td headers="X" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputMiercoles = "";
                    bDisabled = false;

                    if (data[i].out_Miercoles == 1) { // Está fuera de proyecto
                        claseInputMiercoles += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Miercoles == 0) { // Está fuera de vigencia
                            claseInputMiercoles += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Miercoles == 0) { // Es no laborable
                                claseInputMiercoles += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Miercoles == 1) { // Es vacación
                                    claseInputMiercoles += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Miercoles == "1") claseInputMiercoles += " comentario";
                    var ariaLabelMiercoles = "Tarea " + data[i].denominacion + " - " + $('#X > abbr').attr('title');
                    if (estadoDia != "") ariaLabelMiercoles += " - " + estadoDia;
                    var dateMiercoles = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueMiercoles="", valueJornMiercoles="";
                    //data[i].esf_Miercoles > 0 ? valueMiercoles = accounting.formatNumber(data[i].esf_Miercoles) : valueMiercoles = "";
                    if (data[i].esf_Miercoles > 0) {
                        valueMiercoles = accounting.formatNumber(data[i].esf_Miercoles);
                        valueJornMiercoles = accounting.formatNumber(data[i].esfJorn_Miercoles);
                    }

                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledMiercoles = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledMiercoles = 1;
                    sHtml.append(' data-input-class="' + claseInputMiercoles + '"');
                    sHtml.append(' data-input-date="' + dateMiercoles + '"');
                    sHtml.append(' data-input-value="' + valueMiercoles + '"');
                    sHtml.append(' data-input-jorn="' + valueJornMiercoles + '"');
                    sHtml.append(' data-input-disabled="' + disabledMiercoles + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelMiercoles + '"');
                }
                sHtml.append('></td>');


                //JUEVES//
                estadoDia = "";
                //sfechaDia = $('#J').attr('data-date');
                sfechaDia = dias.jueves.fechaSemana
                sHtml.append('<td headers="J" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputJueves = "";
                    bDisabled = false;

                    if (data[i].out_Jueves == 1) { // Está fuera de proyecto
                        claseInputJueves += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Jueves == 0) { // Está fuera de vigencia
                            claseInputJueves += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Jueves == 0) { // Es no laborable
                                claseInputJueves += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Jueves == 1) { // Es vacación
                                    claseInputJueves += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Jueves == "1") claseInputJueves += " comentario";
                    var ariaLabelJueves = "Tarea " + data[i].denominacion + " - " + $('#J > abbr').attr('title');
                    if (estadoDia != "") ariaLabelJueves += " - " + estadoDia;
                    var dateJueves = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueJueves="", valueJornJueves="";
                    //data[i].esf_Jueves > 0 ? valueJueves = accounting.formatNumber(data[i].esf_Jueves) : valueJueves = "";
                    if (data[i].esf_Jueves > 0) {
                        valueJueves = accounting.formatNumber(data[i].esf_Jueves);
                        valueJornJueves = accounting.formatNumber(data[i].esfJorn_Jueves);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledJueves = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledJueves = 1;
                    sHtml.append(' data-input-class="' + claseInputJueves + '"');
                    sHtml.append(' data-input-date="' + dateJueves + '"');
                    sHtml.append(' data-input-value="' + valueJueves + '"');
                    sHtml.append(' data-input-jorn="' + valueJornJueves + '"');
                    sHtml.append(' data-input-disabled="' + disabledJueves + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelJueves + '"');
                }
                sHtml.append('></td>');


                //VIERNES//
                estadoDia = "";
                //sfechaDia = $('#V').attr('data-date');
                sfechaDia = dias.viernes.fechaSemana
                sHtml.append('<td headers="V" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputViernes = "";
                    bDisabled = false;

                    if (data[i].out_Viernes == 1) { // Está fuera de proyecto
                        claseInputViernes += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Viernes == 0) { // Está fuera de vigencia
                            claseInputViernes += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Viernes == 0) { // Es no laborable
                                claseInputViernes += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Viernes == 1) { // Es vacación
                                    claseInputViernes += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Viernes == "1") claseInputViernes += " comentario";
                    var ariaLabelViernes = "Tarea " + data[i].denominacion + " - " + $('#V > abbr').attr('title');
                    if (estadoDia != "") ariaLabelViernes += " - " + estadoDia;
                    var dateViernes = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueViernes="", valueJornViernes="";
                    //data[i].esf_Viernes > 0 ? valueViernes = accounting.formatNumber(data[i].esf_Viernes) : valueViernes = "";
                    if (data[i].esf_Viernes > 0) {
                        valueViernes = accounting.formatNumber(data[i].esf_Viernes);
                        valueJornViernes = accounting.formatNumber(data[i].esfJorn_Viernes);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledViernes = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledViernes = 1;
                    sHtml.append(' data-input-class="' + claseInputViernes + '"');
                    sHtml.append(' data-input-date="' + dateViernes + '"');
                    sHtml.append(' data-input-value="' + valueViernes + '"');
                    sHtml.append(' data-input-jorn="' + valueJornViernes + '"');
                    sHtml.append(' data-input-disabled="' + disabledViernes + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelViernes + '"');
                }
                sHtml.append('></td>');


                //SABADO//
                estadoDia = "";
                //sfechaDia = $('#S').attr('data-date');
                sfechaDia = dias.sabado.fechaSemana
                sHtml.append('<td headers="S" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputSabado = "";
                    bDisabled = false;

                    if (data[i].out_Sabado == 1) { // Está fuera de proyecto
                        claseInputSabado += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Sabado == 0) { // Está fuera de vigencia
                            claseInputSabado += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Sabado == 0) { // Es no laborable
                                claseInputSabado += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Sabado == 1) { // Es vacación
                                    claseInputSabado += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Sabado == "1") claseInputSabado += " comentario";
                    var ariaLabelSabado = "Tarea " + data[i].denominacion + " - " + $('#V > abbr').attr('title');
                    if (estadoDia != "") ariaLabelSabado += " - " + estadoDia;
                    var dateSabado = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueSabado="", valueJornSabado="";
                    //data[i].esf_Sabado > 0 ? valueSabado = accounting.formatNumber(data[i].esf_Sabado) : valueSabado = "";
                    if (data[i].esf_Sabado > 0) {
                        valueSabado = accounting.formatNumber(data[i].esf_Sabado);
                        valueJornSabado = accounting.formatNumber(data[i].esfJorn_Sabado);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledSabado = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledSabado = 1;
                    sHtml.append(' data-input-class="' + claseInputSabado + '"');
                    sHtml.append(' data-input-date="' + dateSabado + '"');
                    sHtml.append(' data-input-value="' + valueSabado + '"');
                    sHtml.append(' data-input-jorn="' + valueJornSabado + '"');
                    sHtml.append(' data-input-disabled="' + disabledSabado + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelSabado + '"');
                }
                sHtml.append('></td>');


                //DOMINGO//
                estadoDia = "";
                //sfechaDia = $('#D').attr('data-date');
                sfechaDia = dias.domingo.fechaSemana
                sHtml.append('<td headers="D" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputDomingo = "";
                    bDisabled = false;

                    if (data[i].out_Domingo == 1) { // Está fuera de proyecto
                        claseInputDomingo += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Domingo == 0) { // Está fuera de vigencia
                            claseInputDomingo += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Domingo == 0) { // Es no laborable
                                claseInputDomingo += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Domingo == 1) { // Es vacación
                                    claseInputDomingo += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Domingo == "1") claseInputDomingo += " comentario";
                    var ariaLabelDomingo = "Tarea " + data[i].denominacion + " - " + $('#V > abbr').attr('title');
                    if (estadoDia != "") ariaLabelDomingo += " - " + estadoDia;
                    var dateDomingo = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueDomingo="", valueJornDomingo="";
                    //data[i].esf_Domingo > 0 ? valueDomingo = accounting.formatNumber(data[i].esf_Domingo) : valueDomingo = "";
                    if (data[i].esf_Domingo > 0) {
                        valueDomingo = accounting.formatNumber(data[i].esf_Domingo);
                        valueJornDomingo = accounting.formatNumber(data[i].esfJorn_Domingo);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledDomingo = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledDomingo = 1;
                    sHtml.append(' data-input-class="' + claseInputDomingo + '"');
                    sHtml.append(' data-input-date="' + dateDomingo + '"');
                    sHtml.append(' data-input-value="' + valueDomingo + '"');
                    sHtml.append(' data-input-jorn="' + valueJornDomingo + '"');
                    sHtml.append(' data-input-disabled="' + disabledDomingo + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelDomingo + '"');
                }
                sHtml.append('></td>');

                //OTC
                sHtml.append('<td headers="OTC" class="celdaHoras">');
                if (data[i].t346_codpst !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t346_codpst + '">' + data[i].t346_codpst + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }                    
                sHtml.append('</td>');

                //OTL
                sHtml.append('<td headers="OTL" class="celdaHoras">');
                if (data[i].t332_otl !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t332_otl + '">' + data[i].t332_otl + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }
                sHtml.append('</td>');

                //ETE
                sHtml.append('<td headers="ETE" class="celdaHoras"');
                var claseInputETE = "";
                if (parseInt(data[i].HayIndicaciones) == 1) claseInputETE += " comentario";
                sHtml.append(' data-input-class="' + claseInputETE + '"');
                sHtml.append(' data-aria-label="Tarea ' + data[i].denominacion + " - " + $('#ETE > abbr').attr('title') + '"');                
                if (data[i].TotalEstimado != null && data[i].TotalEstimado > 0) {
                    sHtml.append(' data-input-value="' + accounting.formatNumber(data[i].TotalEstimado) + '"');
                } else {
                    sHtml.append(' data-input-value=""');
                }
                var disabledETE = 0;
                if (bAnulada || bEstadoLectura) disabledETE = 1;
                sHtml.append(' data-input-disabled="' + disabledETE + '"');
                sHtml.append('></td>');                

                //FFE                
                sHtml.append('<td headers="FFE" class="celdaFecha"');
                sHtml.append(' data-aria-label="Tarea ' + data[i].denominacion + " - " + $('#FFE > abbr').attr('title') + '"');
                if (data[i].FinEstimado !== null) {
                    sHtml.append(' data-input-value="' + moment(data[i].FinEstimado).format("DD/MM/YYYY") + '"');
                } else {
                    sHtml.append(' data-input-value=""');
                }
                var disabledFFE = 0;
                if (bAnulada || bEstadoLectura) disabledFFE = 1;
                sHtml.append(' data-input-disabled="' + disabledFFE + '"');
                sHtml.append('></td>');

                //Fin
                sHtml.append('<td headers="F" class="celdaHoras"');
                sHtml.append(' data-aria-label="Fin tarea ' + " - " + data[i].denominacion + '"');
                sHtml.append(' data-input-value="' + data[i].t336_completado + '"');
                var disabledFin = 0;
                if (bAnulada || bEstadoLectura) disabledFin = 1;
                sHtml.append(' data-input-disabled="' + disabledFin + '"');
                sHtml.append('></td>');

            }

            sHtml.append('<td headers="EAT" class="celdaHoras"><span>');
            if (data[i].EsfuerzoTotalAcumulado > 0) sHtml.append(accounting.formatNumber(data[i].EsfuerzoTotalAcumulado));
            sHtml.append('</span></td>');

            sHtml.append('<td headers="EP" class="celdaHoras"><span>');
            if (data[i].Pendiente != null && data[i].Pendiente > 0) sHtml.append(accounting.formatNumber(data[i].Pendiente));
            sHtml.append('</span></td>');

            sHtml.append('</tr>');

        }

        //Se añade el contenido tras la fila del padre y se actualizan sus atributos de apertura y carga
        if (indicadores.i_lineaPadre == "") indicadores.i_lineaPadre = idPadre;

        renderizarLineasDesglose(sHtml.toString());        

        //Se enmascaran los campos de importes
        $('.diasSemana').mask('99,99');
        $('.totalTarea').mask('999999,99');

        //$("#tabla tbody tr td span:empty").addClass("fk-elementosvacios");

        //cebrear();

        indicadores.i_lineaPadre = "";
        $('#ContadorRender').html(Date.now() - inicio + ' ms.');
    }

    //Cambio de valores en la tabla durante la navegación entre semanas del mismo mes
    var cambiarValoresTabla = function (data, dias) {
        var inicio = Date.now();

        for (var i = 0; i < data.length; i++) {

            if (data[i].tipo != "T") {

                switch (data[i].tipo) {

                    case "PSN":

                        if (dias.lunes.diaSemana !== null) dias.lunes.totalConsumoProys += data[i].esf_Lunes;
                        if (dias.martes.diaSemana !== null) dias.martes.totalConsumoProys += data[i].esf_Martes;
                        if (dias.miercoles.diaSemana !== null) dias.miercoles.totalConsumoProys += data[i].esf_Miercoles;
                        if (dias.jueves.diaSemana !== null) dias.jueves.totalConsumoProys += data[i].esf_Jueves;
                        if (dias.viernes.diaSemana !== null) dias.viernes.totalConsumoProys += data[i].esf_Viernes;
                        if (dias.sabado.diaSemana !== null) dias.sabado.totalConsumoProys += data[i].esf_Sabado;
                        if (dias.domingo.diaSemana !== null) dias.domingo.totalConsumoProys += data[i].esf_Domingo;

                        var linea = $('#PSN' + data[i].t305_idproyectosubnodo + '');

                        break;

                    case "PT":

                        var linea = $('#PT' + data[i].t331_idpt + '');

                        break;

                    case "F":

                        var linea = $('#F' + data[i].t334_idfase + '');

                        break;

                    case "A":

                        var linea = $('#A' + data[i].t335_idactividad + '');

                        break;

                }
                
                var valor;

                data[i].esf_Lunes > 0 ? valor = accounting.formatNumber(data[i].esf_Lunes) : valor = "";
                linea.find('td[headers="L"]').children().html(valor);

                data[i].esf_Martes > 0 ? valor = accounting.formatNumber(data[i].esf_Martes) : valor = "";
                linea.find('td[headers="M"]').children().html(valor);

                data[i].esf_Miercoles > 0 ? valor = accounting.formatNumber(data[i].esf_Miercoles) : valor = "";
                linea.find('td[headers="X"]').children().html(valor);

                data[i].esf_Jueves > 0 ? valor = accounting.formatNumber(data[i].esf_Jueves) : valor = "";
                linea.find('td[headers="J"]').children().html(valor);

                data[i].esf_Viernes > 0 ? valor = accounting.formatNumber(data[i].esf_Viernes) : valor = "";
                linea.find('td[headers="V"]').children().html(valor);

                data[i].esf_Sabado > 0 ? valor = accounting.formatNumber(data[i].esf_Sabado) : valor = "";
                linea.find('td[headers="S"]').children().html(valor);

                data[i].esf_Domingo > 0 ? valor = accounting.formatNumber(data[i].esf_Domingo) : valor = "";
                linea.find('td[headers="D"]').children().html(valor);

                //OTC
                var sHtml = new StringBuilder();
                sHtml.append('<td headers="OTC" class="celdaHoras">');
                if (data[i].t346_codpst !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t346_codpst + '">' + data[i].t346_codpst + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }
                sHtml.append('</td>');
                linea.find('td[headers="OTC"]').replaceWith(sHtml.toString());

                //OTL
                sHtml = new StringBuilder();
                sHtml.append('<td headers="OTL" class="celdaHoras">');
                if (data[i].t332_otl !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t332_otl + '">' + data[i].t332_otl + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }
                sHtml.append('</td>');
                linea.find('td[headers="OTL"]').replaceWith(sHtml.toString());

                data[i].TotalEstimado != null && data[i].TotalEstimado > 0 ? valor = accounting.formatNumber(data[i].TotalEstimado) : valor = "";
                linea.find('td[headers="ETE"]').children().html(valor);

                data[i].FinEstimado != null ? valor = moment(data[i].FinEstimado).format("DD/MM/YYYY") : valor = "";
                linea.find('td[headers="FFE"]').children().html(valor);

                data[i].EsfuerzoTotalAcumulado > 0 ? valor = accounting.formatNumber(data[i].EsfuerzoTotalAcumulado) : valor = "";
                linea.find('td[headers="EAT"]').children().html(valor);

                data[i].Pendiente != null && data[i].Pendiente > 0 ? valor = accounting.formatNumber(data[i].Pendiente) : valor = "";
                linea.find('td[headers="EP"]').children().html(valor);

            }

            else {

                var linea = $('#T' + data[i].t332_idtarea + '');

                var bEstadoLectura = false;
                var bAnulada = false;
                var bDisabled;
                var estadoTarea, descEstado;
                var estadoDia = "";
                var fAlta = new Date(moment(data[i].t330_falta));
                var fBaja;

                data[i].t330_fbaja != null ? fBaja = new Date(moment(data[i].t330_fbaja)) : fBaja = null;

                switch (data[i].t332_estado)//Estado
                {
                    case 0:
                        estadoTarea = " paralizada";
                        descEstado = " Tarea paralizada.";
                        bEstadoLectura = true;
                        break;
                    case 1://Activo
                        estadoTarea = "";
                        descEstado = "";
                        if (data[i].t331_obligaest == 1) {
                            estadoTarea = " obligaest";
                            descEstado = " Tarea de estimación obligatoria.";
                        }
                        break;
                    case 2:
                        estadoTarea = " pendiente";
                        descEstado = " Tarea pendiente.";
                        bEstadoLectura = true;
                        break;
                    case 3:
                        estadoTarea = " finalizada";
                        descEstado = " Tarea finalizada.";
                        if (data[i].t332_impiap == 0) bEstadoLectura = true;  //si impiap = 0, lectura
                        break;
                    case 4:
                        estadoTarea = " cerrada";
                        descEstado = " Tarea cerrada.";
                        if (data[i].t332_impiap == 0) bEstadoLectura = true;
                        break;
                    case 5:
                        estadoTarea = " anulada";
                        descEstado = " Tarea anulada.";
                        bAnulada = true;
                        break;
                }

                if (data[i].t301_estado == "C") bEstadoLectura = true;

                //LUNES//
                var sHtml = new StringBuilder();
                sfechaDia = dias.lunes.fechaSemana
                sHtml.append('<td headers="L" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputLunes = "";
                    bDisabled = false;

                    if (data[i].out_Lunes == 1) { // Está fuera de proyecto
                        claseInputLunes += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Lunes == 0) { // Está fuera de vigencia
                            claseInputLunes += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Lunes == 0) { // Es no laborable
                                claseInputLunes += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Lunes == 1) { // Es vacación
                                    claseInputLunes += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Lunes == "1") claseInputLunes += " comentario";
                    var ariaLabelLunes = "Tarea " + data[i].denominacion + " - " + $('#L > abbr').attr('title');
                    if (estadoDia != "") ariaLabelLunes += " - " + estadoDia;
                    var dateLunes = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueLunes = "", valueJornLunes = "";
                    //data[i].esf_Lunes > 0 ? valueLunes = accounting.formatNumber(data[i].esf_Lunes) : valueLunes = "";
                    if (data[i].esf_Lunes > 0) {
                        valueLunes = accounting.formatNumber(data[i].esf_Lunes);
                        valueJornLunes = accounting.formatNumber(data[i].esfJorn_Lunes);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledLunes = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledLunes = 1;
                    sHtml.append(' data-input-class="' + claseInputLunes + '"');
                    sHtml.append(' data-input-date="' + dateLunes + '"');
                    sHtml.append(' data-input-value="' + valueLunes + '"');
                    sHtml.append(' data-input-jorn="' + valueJornLunes + '"');
                    sHtml.append(' data-input-disabled="' + disabledLunes + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelLunes + '"');
                }

                sHtml.append('></td>');
                linea.find('td[headers="L"]').replaceWith(sHtml.toString());

                //MARTES//
                sHtml = new StringBuilder();
                estadoDia = "";
                sfechaDia = dias.martes.fechaSemana
                sHtml.append('<td headers="M" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputMartes = "";
                    bDisabled = false;

                    if (data[i].out_Martes == 1) { // Está fuera de proyecto
                        claseInputMartes += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Martes == 0) { // Está fuera de vigencia
                            claseInputMartes += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Martes == 0) { // Es no laborable
                                claseInputMartes += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Martes == 1) { // Es vacación
                                    claseInputMartes += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Martes == "1") claseInputMartes += " comentario";
                    var ariaLabelMartes = "Tarea " + data[i].denominacion + " - " + $('#M > abbr').attr('title');
                    if (estadoDia != "") ariaLabelMartes += " - " + estadoDia;
                    var dateMartes = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueMartes = "", valueJornMartes = "";
                    //data[i].esf_Martes > 0 ? valueMartes = accounting.formatNumber(data[i].esf_Martes) : valueMartes = "";
                    if (data[i].esf_Martes > 0) {
                        valueMartes = accounting.formatNumber(data[i].esf_Martes);
                        valueJornMartes = accounting.formatNumber(data[i].esfJorn_Martes);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledMartes = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledMartes = 1;
                    sHtml.append(' data-input-class="' + claseInputMartes + '"');
                    sHtml.append(' data-input-date="' + dateMartes + '"');
                    sHtml.append(' data-input-value="' + valueMartes + '"');
                    sHtml.append(' data-input-jorn="' + valueJornMartes + '"');
                    sHtml.append(' data-input-disabled="' + disabledMartes + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelMartes + '"');
                }
                sHtml.append('></td>');
                linea.find('td[headers="M"]').replaceWith(sHtml.toString());


                //MIERCOLES//
                sHtml = new StringBuilder();
                estadoDia = "";
                //sfechaDia = $('#X').attr('data-date');
                sfechaDia = dias.miercoles.fechaSemana
                sHtml.append('<td headers="X" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputMiercoles = "";
                    bDisabled = false;

                    if (data[i].out_Miercoles == 1) { // Está fuera de proyecto
                        claseInputMiercoles += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Miercoles == 0) { // Está fuera de vigencia
                            claseInputMiercoles += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Miercoles == 0) { // Es no laborable
                                claseInputMiercoles += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Miercoles == 1) { // Es vacación
                                    claseInputMiercoles += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Miercoles == "1") claseInputMiercoles += " comentario";
                    var ariaLabelMiercoles = "Tarea " + data[i].denominacion + " - " + $('#X > abbr').attr('title');
                    if (estadoDia != "") ariaLabelMiercoles += " - " + estadoDia;
                    var dateMiercoles = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueMiercoles = "", valueJornMiercoles = "";
                    //data[i].esf_Miercoles > 0 ? valueMiercoles = accounting.formatNumber(data[i].esf_Miercoles) : valueMiercoles = "";
                    if (data[i].esf_Miercoles > 0) {
                        valueMiercoles = accounting.formatNumber(data[i].esf_Miercoles);
                        valueJornMiercoles = accounting.formatNumber(data[i].esfJorn_Miercoles);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledMiercoles = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledMiercoles = 1;
                    sHtml.append(' data-input-class="' + claseInputMiercoles + '"');
                    sHtml.append(' data-input-date="' + dateMiercoles + '"');
                    sHtml.append(' data-input-value="' + valueMiercoles + '"');
                    sHtml.append(' data-input-jorn="' + valueJornMiercoles + '"');
                    sHtml.append(' data-input-disabled="' + disabledMiercoles + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelMiercoles + '"');
                }
                sHtml.append('></td>');
                linea.find('td[headers="X"]').replaceWith(sHtml.toString());


                //JUEVES//
                sHtml = new StringBuilder();
                estadoDia = "";
                //sfechaDia = $('#J').attr('data-date');
                sfechaDia = dias.jueves.fechaSemana
                sHtml.append('<td headers="J" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputJueves = "";
                    bDisabled = false;

                    if (data[i].out_Jueves == 1) { // Está fuera de proyecto
                        claseInputJueves += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Jueves == 0) { // Está fuera de vigencia
                            claseInputJueves += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Jueves == 0) { // Es no laborable
                                claseInputJueves += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Jueves == 1) { // Es vacación
                                    claseInputJueves += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Jueves == "1") claseInputJueves += " comentario";
                    var ariaLabelJueves = "Tarea " + data[i].denominacion + " - " + $('#J > abbr').attr('title');
                    if (estadoDia != "") ariaLabelJueves += " - " + estadoDia;
                    var dateJueves = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueJueves = "", valueJornJueves = "";
                    //data[i].esf_Jueves > 0 ? valueJueves = accounting.formatNumber(data[i].esf_Jueves) : valueJueves = "";
                    if (data[i].esf_Jueves > 0) {
                        valueJueves = accounting.formatNumber(data[i].esf_Jueves);
                        valueJornJueves = accounting.formatNumber(data[i].esfJorn_Jueves);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledJueves = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledJueves = 1;
                    sHtml.append(' data-input-class="' + claseInputJueves + '"');
                    sHtml.append(' data-input-date="' + dateJueves + '"');
                    sHtml.append(' data-input-value="' + valueJueves + '"');
                    sHtml.append(' data-input-jorn="' + valueJornJueves + '"');
                    sHtml.append(' data-input-disabled="' + disabledJueves + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelJueves + '"');
                }
                sHtml.append('></td>');
                linea.find('td[headers="J"]').replaceWith(sHtml.toString());


                //VIERNES//
                sHtml = new StringBuilder();
                estadoDia = "";
                //sfechaDia = $('#V').attr('data-date');
                sfechaDia = dias.viernes.fechaSemana
                sHtml.append('<td headers="V" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputViernes = "";
                    bDisabled = false;

                    if (data[i].out_Viernes == 1) { // Está fuera de proyecto
                        claseInputViernes += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Viernes == 0) { // Está fuera de vigencia
                            claseInputViernes += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Viernes == 0) { // Es no laborable
                                claseInputViernes += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Viernes == 1) { // Es vacación
                                    claseInputViernes += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Viernes == "1") claseInputViernes += " comentario";
                    var ariaLabelViernes = "Tarea " + data[i].denominacion + " - " + $('#V > abbr').attr('title');
                    if (estadoDia != "") ariaLabelViernes += " - " + estadoDia;
                    var dateViernes = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueViernes = "", valueJornViernes = "";
                    //data[i].esf_Viernes > 0 ? valueViernes = accounting.formatNumber(data[i].esf_Viernes) : valueViernes = "";
                    if (data[i].esf_Viernes > 0) {
                        valueViernes = accounting.formatNumber(data[i].esf_Viernes);
                        valueJornViernes = accounting.formatNumber(data[i].esfJorn_Viernes);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledViernes = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledViernes = 1;
                    sHtml.append(' data-input-class="' + claseInputViernes + '"');
                    sHtml.append(' data-input-date="' + dateViernes + '"');
                    sHtml.append(' data-input-value="' + valueViernes + '"');
                    sHtml.append(' data-input-jorn="' + valueJornViernes + '"');
                    sHtml.append(' data-input-disabled="' + disabledViernes + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelViernes + '"');
                }
                sHtml.append('></td>');
                linea.find('td[headers="V"]').replaceWith(sHtml.toString());


                //SABADO//
                sHtml = new StringBuilder();
                estadoDia = "";
                //sfechaDia = $('#S').attr('data-date');
                sfechaDia = dias.sabado.fechaSemana
                sHtml.append('<td headers="S" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputSabado = "";
                    bDisabled = false;

                    if (data[i].out_Sabado == 1) { // Está fuera de proyecto
                        claseInputSabado += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Sabado == 0) { // Está fuera de vigencia
                            claseInputSabado += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Sabado == 0) { // Es no laborable
                                claseInputSabado += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Sabado == 1) { // Es vacación
                                    claseInputSabado += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Sabado == "1") claseInputSabado += " comentario";
                    var ariaLabelSabado = "Tarea " + data[i].denominacion + " - " + $('#V > abbr').attr('title');
                    if (estadoDia != "") ariaLabelSabado += " - " + estadoDia;
                    var dateSabado = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueSabado = "", valueJornSabado = "";
                    //data[i].esf_Sabado > 0 ? valueSabado = accounting.formatNumber(data[i].esf_Sabado) : valueSabado = "";
                    if (data[i].esf_Sabado > 0) {
                        valueSabado = accounting.formatNumber(data[i].esf_Sabado);
                        valueJornSabado = accounting.formatNumber(data[i].esfJorn_Sabado);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledSabado = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledSabado = 1;
                    sHtml.append(' data-input-class="' + claseInputSabado + '"');
                    sHtml.append(' data-input-date="' + dateSabado + '"');
                    sHtml.append(' data-input-value="' + valueSabado + '"');
                    sHtml.append(' data-input-jorn="' + valueJornSabado + '"');
                    sHtml.append(' data-input-disabled="' + disabledSabado + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelSabado + '"');
                }
                sHtml.append('></td>');
                linea.find('td[headers="S"]').replaceWith(sHtml.toString());


                //DOMINGO//
                sHtml = new StringBuilder();
                estadoDia = "";
                //sfechaDia = $('#D').attr('data-date');
                sfechaDia = dias.domingo.fechaSemana
                sHtml.append('<td headers="D" class="celdaHoras"');
                if (sfechaDia != null) {
                    var claseInputDomingo = "";
                    bDisabled = false;

                    if (data[i].out_Domingo == 1) { // Está fuera de proyecto
                        claseInputDomingo += "fueraProyecto";
                        estadoDia = "Fuera de proyecto.";
                        bDisabled = true;
                    } else {
                        if (data[i].vig_Domingo == 0) { // Está fuera de vigencia
                            claseInputDomingo += "fueraVigencia";
                            estadoDia = "Fuera de vigencia.";
                            bDisabled = true;
                        } else {
                            if (data[i].lab_Domingo == 0) { // Es no laborable
                                claseInputDomingo += "noLaborable";
                                estadoDia = "Festivo.";
                                if (data[i].t323_regfes == 0) bDisabled = true; // No puede imputar en no laborables
                            } else {
                                if (data[i].vac_Domingo == 1) { // Es vacación
                                    claseInputDomingo += "vacaciones";
                                    estadoDia = "Vacaciones.";
                                } else {
                                }
                            }
                        }
                    }

                    //Pintado de tarea/día con comentario
                    if (data[i].com_Domingo == "1") claseInputDomingo += " comentario";
                    var ariaLabelDomingo = "Tarea " + data[i].denominacion + " - " + $('#V > abbr').attr('title');
                    if (estadoDia != "") ariaLabelDomingo += " - " + estadoDia;
                    var dateDomingo = moment(sfechaDia).format("DD/MM/YYYY");
                    var valueDomingo = "", valueJornDomingo = "";
                    //data[i].esf_Domingo > 0 ? valueDomingo = accounting.formatNumber(data[i].esf_Domingo) : valueDomingo = "";
                    if (data[i].esf_Domingo > 0) {
                        valueDomingo = accounting.formatNumber(data[i].esf_Domingo);
                        valueJornDomingo = accounting.formatNumber(data[i].esfJorn_Domingo);
                    }
                    //Si se ha etiquetado como de solo lectura a nivel de día o si es de solo lectura a nivel de tarea y la fecha del día es posterior a la fecha de alta del usuario en el proyecto e inferior a la fecha de baja.                     
                    var disabledDomingo = 0;
                    if (bAnulada || bDisabled || (bEstadoLectura && !(sfechaDia >= fAlta && (fBaja == null || sfechaDia <= fBaja)))) disabledDomingo = 1;
                    sHtml.append(' data-input-class="' + claseInputDomingo + '"');
                    sHtml.append(' data-input-date="' + dateDomingo + '"');
                    sHtml.append(' data-input-value="' + valueDomingo + '"');
                    sHtml.append(' data-input-jorn="' + valueJornDomingo + '"');
                    sHtml.append(' data-input-disabled="' + disabledDomingo + '"');
                    sHtml.append(' data-aria-label="' + ariaLabelDomingo + '"');
                }
                sHtml.append('></td>');
                linea.find('td[headers="D"]').replaceWith(sHtml.toString());

                //OTC
                sHtml = new StringBuilder();
                sHtml.append('<td headers="OTC" class="celdaHoras">');
                if (data[i].t346_codpst !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t346_codpst + '">' + data[i].t346_codpst + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }
                sHtml.append('</td>');
                linea.find('td[headers="OTC"]').replaceWith(sHtml.toString());

                //OTL
                sHtml = new StringBuilder();
                sHtml.append('<td headers="OTL" class="celdaHoras">');
                if (data[i].t332_otl !== null) {
                    sHtml.append('<span class="txtOT" title="' + data[i].t332_otl + '">' + data[i].t332_otl + '</span>');
                } else {
                    sHtml.append('<span></span>');
                }
                sHtml.append('</td>');
                linea.find('td[headers="OTL"]').replaceWith(sHtml.toString());

                //ETE
                sHtml = new StringBuilder();
                sHtml.append('<td headers="ETE" class="celdaHoras"');
                var claseInputETE = "";
                if (parseInt(data[i].HayIndicaciones) == 1) claseInputETE += " comentario";
                sHtml.append(' data-input-class="' + claseInputETE + '"');
                sHtml.append(' data-aria-label="Tarea ' + data[i].denominacion + " - " + $('#ETE > abbr').attr('title') + '"');
                if (data[i].TotalEstimado != null && data[i].TotalEstimado > 0) {
                    sHtml.append(' data-input-value="' + accounting.formatNumber(data[i].TotalEstimado) + '"');
                } else {
                    sHtml.append(' data-input-value=""');
                }
                var disabledETE = 0;
                if (bAnulada || bEstadoLectura) disabledETE = 1;
                sHtml.append(' data-input-disabled="' + disabledETE + '"');
                sHtml.append('></td>');
                linea.find('td[headers="ETE"]').replaceWith(sHtml.toString());

                //FFE      
                sHtml = new StringBuilder();
                sHtml.append('<td headers="FFE" class="celdaFecha"');
                sHtml.append(' data-aria-label="Tarea ' + data[i].denominacion + " - " + $('#FFE > abbr').attr('title') + '"');
                if (data[i].FinEstimado !== null) {
                    sHtml.append(' data-input-value="' + moment(data[i].FinEstimado).format("DD/MM/YYYY") + '"');
                } else {
                    sHtml.append(' data-input-value=""');
                }
                var disabledFFE = 0;
                if (bAnulada || bEstadoLectura) disabledFFE = 1;
                sHtml.append(' data-input-disabled="' + disabledFFE + '"');
                sHtml.append('></td>');
                linea.find('td[headers="FFE"]').replaceWith(sHtml.toString());

                //Fin
                sHtml = new StringBuilder();
                sHtml.append('<td headers="F" class="celdaHoras"');
                sHtml.append(' data-aria-label="Fin tarea ' + " - " + data[i].denominacion + '"');
                sHtml.append(' data-input-value="' + data[i].t336_completado + '"');
                var disabledFin = 0;
                if (bAnulada || bEstadoLectura) disabledFin = 1;
                sHtml.append(' data-input-disabled="' + disabledFin + '"');
                sHtml.append('></td>');
                linea.find('td[headers="F"]').replaceWith(sHtml.toString());

                linea.attr('data-event', '0');

            }

        }

        //Se enmascaran los campos de importes        
        //$('.diasSemana').mask('99,99');
        //$('.totalTarea').mask('999999,99');

        $('#ContadorRender').html(Date.now() - inicio + ' ms.');
    }

    var existeLinea = function (idElemento) {
        if (lineas.linea(idElemento).length > 0) return true;
        return false;
    }

    var renderizarLineasDesglose = function (html) {

        var lineaPadre = $('#' + indicadores.i_lineaPadre);
        lineaPadre.after(html);                

    }

    var eliminarOtrosConsumos = function () {

        $('#Otros').remove();
    }

    //Pintado de la línea de otros consumos
    var pintarOtrosConsumos = function (dias) {

        var sHtml = new StringBuilder();
        var consLunes = 0; var consMartes = 0; var consMiercoles = 0; var consJueves = 0; var consViernes = 0; var consSabado = 0; var consDomingo = 0;

        sHtml.append('<tr id="Otros" data-loaded="1" class="linea" data-parent="" data-level="0">');
        sHtml.append('<td headers="" class="PE"><span>');
        sHtml.append('</span><span class="fk_busqueda" aria-hidden="true">Otros consumos imputados</span>');
        sHtml.append('<span class="sr-only" role="button" aria-expanded="false">Otros consumos imputados (Nivel 1)</span>');
        sHtml.append('</td>');

        sHtml.append('<td headers="L" class="celdaHoras"><span>');
        consLunes = dias.lunes.totalConsumo - dias.lunes.totalConsumoProys;
        if (consLunes > 0) sHtml.append(accounting.formatNumber(consLunes));
        sHtml.append('</span></td>');
        sHtml.append('<td headers="M" class="celdaHoras"><span>');
        consMartes = dias.martes.totalConsumo - dias.martes.totalConsumoProys;
        if (consMartes > 0) sHtml.append(accounting.formatNumber(consMartes));
        sHtml.append('</span></td>');
        sHtml.append('<td headers="X" class="celdaHoras"><span>');
        consMiercoles = dias.miercoles.totalConsumo - dias.miercoles.totalConsumoProys;
        if (consMiercoles > 0) sHtml.append(accounting.formatNumber(consMiercoles));
        sHtml.append('</span></td>');
        sHtml.append('<td headers="J" class="celdaHoras"><span>');
        consJueves = dias.jueves.totalConsumo - dias.jueves.totalConsumoProys;
        if (consJueves > 0) sHtml.append(accounting.formatNumber(consJueves));
        sHtml.append('</span></td>');
        sHtml.append('<td headers="V" class="celdaHoras"><span>');
        consViernes = dias.viernes.totalConsumo - dias.viernes.totalConsumoProys;
        if (consViernes > 0) sHtml.append(accounting.formatNumber(consViernes));
        sHtml.append('</span></td>');
        sHtml.append('<td headers="S" class="celdaHoras"><span>');
        consSabado = dias.sabado.totalConsumo - dias.sabado.totalConsumoProys;
        if (consSabado > 0) sHtml.append(accounting.formatNumber(consSabado));
        sHtml.append('</span></td>');
        sHtml.append('<td headers="D" class="celdaHoras"><span>');
        consDomingo = dias.domingo.totalConsumo - dias.domingo.totalConsumoProys;
        if (consDomingo > 0) sHtml.append(accounting.formatNumber(consDomingo));
        sHtml.append('</span></td>');
        sHtml.append('<td headers="OTC" class="celdaHoras"><span></span></td>');
        sHtml.append('<td headers="OTL" class="celdaHoras"><span></span></td>');


        sHtml.append('<td headers="ETE" class="celdaHoras"><span>');
        sHtml.append('</span></td>');


        sHtml.append('<td headers="FFE" class="celdaFecha"><span>');
        sHtml.append('</span></td>');

        sHtml.append('<td headers="F" class="celdaHoras"><span></span></td>');

        sHtml.append('<td headers="EAT" class="celdaHoras"><span>');
        sHtml.append('</span></td>');

        sHtml.append('<td headers="EP" class="celdaHoras"><span>');
        sHtml.append('</span></td>');

        sHtml.append('</tr>');


        $('#bodyTabla').append(sHtml.toString());

        //$("#tabla tbody tr td span:empty").addClass("fk-elementosvacios");        

        //cebrear();
        

    }

    //Función de obtención del data-parent de un fila
    var obtenerPadre = function (fila) {

        var padre;

        switch (fila.tipo)
        {
            //case "PT":
            //    padre = "PSN" + fila.t301_idproyecto;
            //    break;
            case "F"://Activo
                padre = "PT" + fila.t331_idpt + " PSN" + fila.t305_idproyectosubnodo;
                break;
            case "A":
                if (fila.t334_idfase == null) {
                    padre = "PT" + fila.t331_idpt + " PSN" + fila.t305_idproyectosubnodo;
                } else {
                    padre = "F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t305_idproyectosubnodo;
                }
                break;
            case "T":
                if (fila.t335_idactividad == null) {
                    padre = "PT" + fila.t331_idpt + " PSN" + fila.t305_idproyectosubnodo;
                } else {
                    if (fila.t334_idfase == null) {
                        padre = "A" + fila.t335_idactividad + " PT" + fila.t331_idpt + " PSN" + fila.t305_idproyectosubnodo;
                    } else {
                        padre = "A" + fila.t335_idactividad + " F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t305_idproyectosubnodo;
                    }
                }
                break;
        }
        
        return padre;

    }

    //Obtiene la naturaleza de un PSN
    var obtenerNaturaleza = function (nPSN) {
        return $('#PSN' + nPSN).attr('data-idnaturaleza');
    }

    //Obtiene si un PSN es imputablea Gasvi
    var obtenerImputableGasvi = function (nPSN) {
        return $('#PSN' + nPSN).attr('data-gasvi');
    }

    var deshabilitarLink = function (link) {

        $(link).attr("role", "");        
        $(link).attr("title", "");
        $(link).attr("tabindex", "");
        $(link).removeClass('link');
        $(link).css({ opacity: 0.1 });

        deAttachEvents(link);
            

    }

    var habilitarLink = function (link) {

        $(link).attr("role", "link");
        $(link).attr("tabindex", "0");
        $(link).addClass('link');
        $(link).css({ opacity: 1 });

        if (link.selector == "#semSig") {
            $(link).attr("title", "Ir a la semana siguiente");
        }
        else {
            $(link).attr("title", "Ir a la semana anterior");
        }

    }


    //Comprobación de apertura o cierre de linea. True para abrir y False para cerrar
    var abrir = function (thisObj) {

        if ($(thisObj).children().hasClass('glyphicon-plus')) return true;
        return false;

    }

    //Comprobación de carga de BBDD de hijos de una línea
    var lineaCargada = function (thisObj) {

        if ($(thisObj).parent().attr('data-loaded') == "1") return true;
        return false;

    }

    //Apertura de lineas
    var abrirLinea = function (thisObj, proceso) {

        switch (proceso) {
            case 1:
                var id = $(thisObj).attr('id')
                //Abre los hijos del nodo pulsado
                $("#bodyTabla tr[data-parent^='" + id + "']").show();
                actualizarLineaAbierta(thisObj);
                break;
            case 2:
                $(lineas.PT()).show();
                actualizarLineaAbierta(lineas.PE());
                break;
            case 3:
                $(lineas.TLineas()).show();
                actualizarLineaAbierta(lineas.padresTLineas());
                break;
            case 4:
                var id = $(thisObj).attr('id');
                var filas = $("#bodyTabla tr[data-parent*='" + id + "'], #bodyTabla tr[id='" + id + "']");
                filas.show();                
                actualizarLineaAbierta(filas);
                break;
        }

    }

    //Actualiza los valores de la línea recién abierta
    var actualizarLineaAbierta = function (thisObj) {

        //Modifica el atributo de expandido de la línea pulsada
        $(thisObj).find("span[aria-expanded]").attr('aria-expanded', 'true');

        //Cambia el glyphicon
        $(thisObj).find(".glyphicon-plus").toggleClass("glyphicon-minus", true).removeClass("glyphicon-plus");

        //Marca la fila como cargada de BBDD
        $(thisObj).attr('data-loaded', "1");

        //cebrear();
    }

    //Cierre de lineas del arbol de la tabla
    var cerrarLinea = function (thisObj, proceso) {

        var id = $(thisObj).parent().attr('id')

        //Cierra los descendientes del nodo pulsado
        $("#bodyTabla tr[data-parent*='" + id + "']").hide();

        //Modifica el atributo de expandido de la línea pulsada
        $(thisObj).find("span[aria-expanded]").attr('aria-expanded', 'false');

        //Modifica el atributo de expandido de los descendientes del nodo pulsado
        $("#bodyTabla tr[data-parent*='" + id + "']").find("span[aria-expanded]").attr('aria-expanded', 'false');

        //Cambia el glyphicon de los descendientes del nodo pulsado y de sí mismo
        $("#bodyTabla tr[data-parent*='" + id + "']").find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
        $(thisObj).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");

        //cebrear();
    }

    // Colorear niveles
    var colorearNiveles = function (e) {

        var srcobj;
        srcobj = e.target ? e.target : e.srcElement;

        //El elemento target no es el mismo si se pulsa el el botón o el span que hay dentro del botón, por lo que se comprueba y se coge siempre el botón
        if (!$(srcobj).is(":button")) srcobj = $(srcobj).parent();

        var nivel = $(srcobj).attr("data-level");
        if ($("#bodyTabla tr").length == 0) nivel = 1;
        colorearNivel(nivel);
    }

    function colorearNivel(nivel) {
        switch (nivel) {
            case "1":
                $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra2").addClass("nivelGris").removeClass("nivelVerde");
                $("#barra3").addClass("nivelGris").removeClass("nivelVerde");
                break;
            case "2":
                $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra2").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra3").addClass("nivelGris").removeClass("nivelVerde");
                break;
            case "3":
                $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra2").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra3").addClass("nivelVerde").removeClass("nivelGris");
                break;
        }
    }

    //Cierre de todas las líneas
    var cerrarNivel = function (event) {

        var srcobj = event.target ? event.target : event.srcElement;

        //El elemento target no es el mismo si se pulsa el el botón o el span que hay dentro del botón, por lo que se comprueba y se coge siempre el botón
        if (!$(srcobj).is(":button")) srcobj = $(srcobj).parent();

        var nivel = $(srcobj).attr("data-level");

        $('#bodyTabla tr').filter(function () {
            return $(this).attr("data-level") > nivel;
        }).hide();

        //Modifica el atributo de expandido de los nodos cerrados
        $('#bodyTabla tr').filter(function () {
            return $(this).attr("data-level") >= nivel;
        }).find('span[aria-expanded]').attr('aria-expanded', 'false');

        //Cambia el glyphicon de los nodos cerrados
        $('#bodyTabla tr').filter(function () {
            return $(this).attr("data-level") >= nivel;
        }).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");

        desmarcarLinea();

        //Eliminamos el texto ' - Seleccionado' del elemento seleccionado anterior. Las posiciones 0 y 1 contienen el tipo de linea y su descripción.
        $('span:contains("- Seleccionado")').each(function () {
            $(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1] + ' - ' + $(this).text().split(" - ")[2]);
        });

        //cebrear();
    }

    //Función de habilitación/deshabilitación de los botones de la botonera superior e inferior xs-sm
    var habilitarBtn = function (boton, valor, callback) {

        if (valor) {
            attachEvents("keypress click", boton, callback);
            $(boton).removeClass('botonDeshabilitado');
            $(boton).addClass('boton');
            $(boton).attr("tabindex", "0");
            $(boton).attr("role", "link");
            if ($(boton).hasClass('fa-stack')) {
                $(boton).next().removeClass('botonDeshabilitado');
            } 
        }
        else {
            deAttachEvents(boton);
            $(boton).removeClass('boton');
            $(boton).addClass('botonDeshabilitado');
            $(boton).removeAttr("tabindex");
            $(boton).removeAttr("role");
            if ($(boton).hasClass('fa-stack')) {
                $(boton).next().addClass('botonDeshabilitado');
            }
        }
    }

    var validarFormatearETE = function (inputETE) {

        var horas = accounting.unformat(inputETE.val(), ",");
        var horasAnt = accounting.unformat(inputETE.attr('value'), ",")

        //Si el valor no ha cambiado no se hará nada
        if (horasAnt != horas || (horas == 0 && inputETE.val() != "")) {

            var finalizada = parseInt(inputETE.parent().parent().attr('data-fin'));

            //Se actualiza EP
            var EAT = inputETE.parent().siblings('[headers="EAT"]').children();
            var EATval = accounting.unformat(EAT.html(), ",");
            var EP = inputETE.parent().siblings('[headers="EP"]').children();

            if (horas < EATval) {

                IB.bsalert.fixedAlert("warning", "Error de validación", "El esfuerzo total estimado no puede ser menor que el esfuerzo acumulado total.");
                inputETE.val(inputETE.attr('value'));
                inputETE.focus();
                return false;
            }

            horas != 0 ? EP.html(accounting.formatNumber(horas - EATval)) : EP.html("");

            var difHoras = horas - horasAnt;

            //Se actualizan los valores de los padres de la tarea
            var padres = inputETE.parent().parent().attr('data-parent').split(" ");

            for (var i = 0; i < (padres.length) ; i++) { //El último valor de padres es el separador de espacio por lo que se obvia

                //De la propia celda
                var padre = $('#' + padres[i] + ' > td[headers=ETE] > span');
                var padreval = accounting.unformat($('#' + padres[i] + ' > td[headers=ETE] > span').html(), ",");
                padre.html(accounting.format(padreval + difHoras));
                if (accounting.unformat(padre.html(), ",") == 0) padre.html("");

                //De EP
                var padreEPval = accounting.unformat($('#' + padres[i] + ' > td[headers=EP] > span').html(), ",");
                if (padreEPval != "") {
                    var padreEP = $('#' + padres[i] + ' > td[headers=EP] > span');
                    padreEP.html(accounting.format(padreEPval + difHoras));
                    if (accounting.unformat(padreEP.html(), ",") == 0 || accounting.unformat(padreEP.html(), ",") < 0) padreEP.html("");
                }                

            }

            //Si horas es 0 el input no contendrá nada
            horas != 0 ? horas = accounting.formatNumber(horas) : horas = "";

            inputETE.attr('value', horas);              
            inputETE.val(horas);
            
            inputETE.attr('data-changed', 1);
            inputETE.parent().parent().attr('data-changed', 1);

            //Si la tarea está finalizada se cambia a no finalizada
            if (finalizada) {
                inputETE.parent().siblings('[headers="F"]').children().prop("checked", false);
                inputETE.parent().parent().attr('data-fin', 0);
            }

        }

    }

    var validarFormatearHoras24 = function (inputHoras) {

        var horas = accounting.unformat(inputHoras.val(), ",");
        var horasAnt = accounting.unformat(inputHoras.attr('value'), ",")

        inputHoras.attr('data-changed', 1);
        inputHoras.parent().parent().attr('data-changed', 1);


        //Si el valor no ha cambiado no se hará nada
        if (horasAnt != horas || (horas == 0 && inputHoras.val() != "")) {

            var finalizada = parseInt(inputHoras.parent().parent().attr('data-fin'));
            var padres = inputHoras.parent().parent().attr('data-parent').split(" ");

            //Se suma la cantidad de horas insertada al total de la Jornada (menos las horas que contenía anteriormente el input) para comrpobar que no se superan las 24h/día
            var dia = inputHoras.parent().attr('headers');
            //var totalJornada = horas + accounting.unformat($('.Pie' + dia).children().html(), ",")
            var totalJornada = horas + (accounting.unformat($('.Pie' + dia).children().html(), ",") - accounting.unformat($(inputHoras).attr('value'), ","));

            if (totalJornada > 24) {
                IB.bsalert.fixedAlert("warning", "Error de validación", "La imputación realizada el " + $('#headTabla > th#' + dia +' > abbr').attr('title') + " superaría el máximo de 24h. Imputación no permitida.");
                inputHoras.val(inputHoras.attr('value'));
                inputHoras.focus();
                return false;
            }

            //Se comprueba si el proyecto exige que se impute a jornada completa (0 obligatorio jornada completa) y que las horas introducidas no sean 0
            var regjornocompleta = parseInt(inputHoras.parent().parent().attr('data-regjornocompleta'));

            if ((!regjornocompleta) && (horas != 0)) {

                if ($('#' + dia).attr('data-horasJornada') != horas) {
                    IB.bsalert.fixedAlert("warning", "Error de validación", "Es obligatorio imputar este proyecto a jornada completa");
                    inputHoras.val(inputHoras.attr('value'));
                    inputHoras.focus();
                    return false;
                }

            }

            //Se actualizan los totales
            var difHoras = horas - horasAnt;

            //Total diario
            var totalDia = accounting.unformat($('.Pie' + dia).children().html(), ",");
            $('.Pie' + dia).children().html(accounting.formatNumber(totalDia + difHoras));            

            //EAT
            var EAT = inputHoras.parent().siblings('[headers="EAT"]').children();
            var EATval = accounting.unformat(EAT.html(), ",");

            EATval != 0 ? EAT.html(accounting.formatNumber(EATval + difHoras)) : EAT.html(accounting.formatNumber(difHoras));
            if (accounting.unformat(EAT.html(), ",") == 0) EAT.html("");
           
            var ETE = inputHoras.parent().siblings('[headers="ETE"]').children();
            var ETEval = accounting.unformat(ETE.val(), ",");
            var EP = inputHoras.parent().siblings('[headers="EP"]').children();
            EATval = accounting.unformat(EAT.html(), ",");

            if (!finalizada) {
                //EP                                
                if (ETEval != 0) EP.html(accounting.formatNumber(ETEval - EATval));
            } else {
                //Si está finalizada
                //ETE   
                var difHorasETE = EATval - ETEval;
                ETE.val(accounting.formatNumber(EATval));
                ETE.attr('value', accounting.formatNumber(EATval));
                ETE.attr('data-changed', 1);
                //Se actualiza la fecha fin estimada                
                var FFE = inputHoras.parent().siblings('[headers="FFE"]').children();
                if (moment(inputHoras.attr('data-date').toDate()).diff(FFE.val().toDate()) > 0) {
                    FFE.val(inputHoras.attr('data-date'));
                    FFE.attr('value', inputHoras.attr('data-date'));
                }
                EP.html("0,00");
            }

            //Se actualizan los valores de los padres de la tarea            
            for (var i = 0; i < padres.length ; i++) { 

                //De la propia celda
                var padre = $('#' + padres[i] + ' > td[headers=' + dia + '] > span');
                var padreval = accounting.unformat($('#' + padres[i] + ' > td[headers=' + dia + '] > span').html(), ",");;
                padre.html(accounting.format(padreval + difHoras));
                if (accounting.unformat(padre.html(), ",") == 0) padre.html("");

                //EAT
                var padreEAT = $('#' + padres[i] + ' > td[headers=EAT] > span');
                var padreEATval = accounting.unformat($('#' + padres[i] + ' > td[headers=EAT] > span').html(), ",");
                padreEAT.html(accounting.format(padreEATval + difHoras));
                if (accounting.unformat(padreEAT.html(), ",") == 0) padreEAT.html("");

                //Si está finalizada se actualizan también FFE y ETE
                if (finalizada) {
                    var fechaPadre = $('#' + padres[i] + ' > td[headers=FFE] > span').html().toDate();
                    if (moment(inputHoras.attr('data-date').toDate()).diff(fechaPadre) > 0) {
                        $('#' + padres[i] + ' > td[headers=FFE] > span').html(inputHoras.attr('data-date'));
                    }
                    var padreETEval = accounting.unformat($('#' + padres[i] + ' > td[headers=ETE] > span').html(), ",");
                    $('#' + padres[i] + ' > td[headers=ETE] > span').html(accounting.formatNumber(padreETEval + difHorasETE));
                }

                //EP
                var padreEPval = accounting.unformat($('#' + padres[i] + ' > td[headers=EP] > span').html(), ",");
                if (padreEPval != "") {
                    var padreEP = $('#' + padres[i] + ' > td[headers=EP] > span');
                    var padreETEval = accounting.unformat($('#' + padres[i] + ' > td[headers=ETE] > span').html(), ",");
                    padreEATval = accounting.unformat($('#' + padres[i] + ' > td[headers=EAT] > span').html(), ",");;
                    padreEP.html(accounting.format(padreETEval - padreEATval));
                    //if (accounting.unformat(padreEP.html(), ",") == 0) padreEP.html("");
                }

                //Si la fecha fin estimada existe y es menor a la fecha de imputación, se actualiza FFE y se avisa al usuario.
                //actualizarFFE
                //var FFEval = inputHoras.parent().siblings('[headers="FFE"]').children().val();
                //if (FFEval != "" && (moment(inputHoras.attr('data-date').toDate()).diff(moment(inputHoras.parent().siblings('[headers="FFE"]').children().val().toDate())) > 0)) {
                //    IB.bsalert.toastinfo("La fecha de fin estimada de la tarea se actualiza a " + inputHoras.attr('data-date'));
                //    inputHoras.parent().siblings('[headers="FFE"]').children().val(inputHoras.attr('data-date'));
                //    actualizarFFE(inputHoras.parent().siblings('[headers="FFE"]').children());
                //}
                
            }

            //inputHoras.attr('data-changed', 1);
            //inputHoras.parent().parent().attr('data-changed', 1);
            cambioTotales();
        }

        //Si horas es 0 el input no contendrá nada
        horas != 0 ? horas = accounting.format(horas) : horas = "";

        inputHoras.attr('value', horas);
        inputHoras.val(horas);
        //cambioTotales();
        return true;
    }

    //Función de actualización de valores de los campos FFE
    var actualizarFFE = function (input) {

        var idPsn = input.parent().parent().attr('data-psn');
        var filas = $("#bodyTabla tr[data-psn='" + idPsn + "']:not([data-tipo='T'])");

        filas.each(function () {
            var fila = $(this);
            var id = fila.attr('id');            
            //var fechaOrig = fila.find("td[headers='FFE'] > span").html();
            var fechaFinal = fechaOrig = "";
            $("#bodyTabla tr[data-parent*='" + id + "'][data-tipo='T']").each(function () {
                var fechaInput;
                if ($(this).attr('data-event') == 0) {
                    fechaInput = $(this).find("td[headers='FFE']").attr("data-input-value");
                } else {
                    fechaInput = $(this).find("td[headers='FFE'] > input").val();
                }                

                if (fechaInput != "") {
                    if (fechaFinal != "") {
                        if (fechaInput.toDate() > fechaFinal.toDate()) {
                            fechaFinal = fechaInput;
                        }
                    } else {
                        fechaFinal = fechaInput;
                    }
                }
            })
            fila.find("td[headers='FFE'] > span").html(fechaFinal);
        })

        input.attr('data-changed', 1);
        input.parent().parent().attr('data-changed', 1);

    }

    //Función de cierre de tarea desde el proceso de grabación.
    var clickFinalizarTarea = function (idTarea) {
        lineas.lineaT(idTarea).find(selectores.sel_checkFin).trigger('click');        
    }    

    var desFinalizarTarea = function (idTarea) {

        //$('tr[data-t="' + idTarea + '"]').attr('data-fin') == "0" ? $('tr[data-t="' + idTarea + '"]').attr('data-fin', 1) : $('tr[data-t="' + idTarea + '"]').attr('data-fin', 0);
        $('tr[data-t="' + idTarea + '"]').attr('data-fin', 0);

        $('tr[data-t="' + idTarea + '"] > td > input.chkHoras').attr('data-changed', 1);
        $('tr[data-t="' + idTarea + '"]').attr('data-changed', 1);

    }

    //Modificaciones de pantalla de finalización de tarea
    var finalizarTarea = function (data, idTarea) {

        var fUltimoConsumo = "";
        var fUltimoConsumoSemana = "";
        var ETEPadre;
        var ETETarea;
        var padres = $('tr[data-t="' + idTarea + '"]').attr('data-parent').split(" ");

        if (data != null) {
            if (data.UltimoConsumo != null) fUltimoConsumo = moment(data.UltimoConsumo).format("DD/MM/YYYY");
        }

        //Se recorre los inputs de días de la tarea para recoger el día de la última imputación por si hay
        // última imputación de la semana actual sin grabar
        $('tr[data-t="' + idTarea + '"] input.diasSemana').each(function () {
            if ($(this).val() != "") fUltimoConsumoSemana = $(this).attr('data-date');
        });

        //Se vuelca el valor en F.F.E y se actualizan los F.F.E. de los padres
        if ((fUltimoConsumoSemana.toDate() > fUltimoConsumo.toDate()) || fUltimoConsumo == "") fUltimoConsumo = fUltimoConsumoSemana;
        $('tr[data-t="' + idTarea + '"] input.txtFecha').val(fUltimoConsumo);
        $('tr[data-t="' + idTarea + '"] input.txtFecha').attr('value', fUltimoConsumo);
        if ($('tr[data-t="' + idTarea + '"] > td[headers=FFE] > input').attr('data-autochange') == "1") $('tr[data-t="' + idTarea + '"] input.txtFecha').attr('data-originalvalue', fUltimoConsumo);
        actualizarFFE($('tr[data-t="' + idTarea + '"] input.txtFecha'));

        //Se pone como total estimado el esfuerzo acumulado total y se actualizan los ETE de los padres de la tarea
        $('tr[data-t="' + idTarea + '"] input.totalTarea').val($('tr[data-t="' + idTarea + '"] > td[headers="EAT"]').children().html());
        validarFormatearETE($('tr[data-t="' + idTarea + '"] input.totalTarea'));        

        //Se pone el pendiente estimado a cero
        $('tr[data-t="' + idTarea + '"] > td[headers="EP"]').children().html('0,00')        

        //Se actualiza el atributo de finalizada de la fila
        //$('tr[data-t="' + idTarea + '"]').attr('data-fin') == "0" ? $('tr[data-t="' + idTarea + '"]').attr('data-fin', 1) : $('tr[data-t="' + idTarea + '"]').attr('data-fin', 0);
        $('tr[data-t="' + idTarea + '"]').attr('data-fin', 1);

        //Se actualiza el valor de modificado
        $('tr[data-t="' + idTarea + '"] > td > input.chkHoras').attr('data-changed', 1);
        $('tr[data-t="' + idTarea + '"] input.txtFecha').attr('data-changed', 1);
        $('tr[data-t="' + idTarea + '"] input.totalTarea').attr('data-changed', 1);
        $('tr[data-t="' + idTarea + '"]').attr('data-changed', 1);
    }

    function abrirModalComentario(comentario, bEstadoLectura) {

        var input = selectores.sel_preInput;

        if (input != "") {

            //Se oculta el contenido de la pantalla al SR y se insertan la fecha del día y la información de la tarea al título del modal
            $('.ocultable').attr('aria-hidden', 'true');
            $('#modalComentario #tituloModalComentario').html('::: SUPER ::: - ' + $(input).attr('data-date') + ' - ' + $(input).parent().parent().children('td:first').children('span:nth-child(2)').html())

            //Según el estado de la tarea se habilita o no el textArea
            bEstadoLectura ? $('#txtComentario').prop("disabled", true) : $('#txtComentario').prop("disabled", false);

            //Si llega comentario se inserta el la propiedad comentario del input y se visualiza en el textArea
            if (comentario != "") $(input).attr('data-comentario', comentario);
            $('#txtComentario').val(comentario);

            $('#modalComentario').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#modalComentario').modal('show');
        }

    }

    var abrirModalConfirmacion = function () {

        var dfr = $.Deferred();

        var dlg = $("#bsconfirm1");

        //Se oculta el contenido de la pantalla al SR
        $('.ocultable').attr('aria-hidden', 'true');

        $(dlg).find(".fk_btnno").off("click").on("click", function () { dfr.reject(); });
        $(dlg).find(".close").off("click").on("click", function () { dfr.reject(); });
        $(dlg).find(".fk_btnsi").off("click").on("click", function () { dfr.resolve(); });

        $('#bsconfirm1').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#bsconfirm1').modal('show');

        return dfr.promise();

    }

    //Función de actualización de comentario en el input y de actualización del marcado visual de comentario
    var guardarComentario = function () {

        var valor = accounting.unformat(selectores.sel_preInput.attr('value'), ",");

        selectores.sel_preInput.attr('data-comentario', dom.txtComentario.val());

        //Si el input no tiene valor no lo registramos como cambio en el input ya que sin imputación no se grabará
        if (valor != 0) {
            selectores.sel_preInput.attr('data-changed', 1);            
        }

        selectores.sel_preInput.parent().parent().attr('data-changed', 1);

        if (dom.txtComentario.val() != "") {
            $(selectores.sel_preInput).addClass('comentario');
        } else {
            $(selectores.sel_preInput).removeClass('comentario');
        }

    }

    //Función de borrado de comentario
    var borrarComentario = function (input) {

        $(input).removeClass('comentario');
        $(input).attr('data-comentario', "");        

    }

    //Función de imputación de jornada através del botón correspondiente
    var imputarJornada = function () {

        var input = selectores.sel_preInput;
        var dia = input.parent().attr('headers'); //Se coge el día correspondiente al input
        var horasJornada = accounting.format($('#' + dia).attr('data-horasjornada')); //Se cogen las horas por jornada correspondientes a ese día

        if (horasJornada == "0,00") {

            //IB.bsalert.toastinfo("No hay datos sobre las horas correspondientes a la jornada del " + $('#' + dia + ' > abbr').attr('title'));
            IB.bsalert.fixedAlert("info", "Información", "No hay datos sobre las horas correspondientes a la jornada del " + $('#' + dia + ' > abbr').attr('title'));

        } else {

            input.val(horasJornada).change();

            validarFormatearHoras24(input);

        }

        input.focus();
            
    }

    //Función de imputación de jornada através del botón correspondiente
    var imputarSemana = function () {        

        var linea = lineas.lineaActiva();
        var dia;
        var horasJornada;

        //Solo se imputará en dias laborables no inhabilitados
        $(linea).find('input.diasSemana:not(.noLaborable):enabled').each(function () {

            dia = $(this).parent().attr('headers'); //Se coge el día correspondiente al input            
            horasJornada = accounting.format($('#' + dia).attr('data-horasjornada')); //Se cogen las horas por jornada correspondientes a ese día

            $(this).val(horasJornada).change();

            return validarFormatearHoras24($(this));
        });
        

    }   

    //Función de acciones posteriores a la grabación
    var grabacionCorrecta = function () {

        habilitarBtn(dom.btnGrabar, 0);
        habilitarBtn(dom.btnGrabarLite, 0);
        habilitarBtn(dom.btnGrabarSig, 0);
        habilitarBtn(dom.btnGrabarRegresar, 0);

        //Se ponen los valores como valores originales y se cambia el data-changed de los inputs
        $('[data-changed=1]:not(tr):not(.chkHoras)').each(function () {
            $(this).attr('data-originalvalue', $(this).attr('value'));
            $(this).attr('data-changed', 0);
        });

        //Se ponen los valores como valores originales y se cambia el data-changed de los checkboxes
        $('[data-changed=1].chkHoras').each(function () {
            $(this).is(':checked') ? $(this).attr('data-originalvalue', 1) : $(this).attr('data-originalvalue', 0);
            $(this).attr('data-changed', 0);
        });

        //Se vuelcan las ultimas fechas de imputación temporales
        $('[data-changed=1][data-tipo="T"]tr').each(function () {
            if ($(this).attr('data-fultiimp_temp') != "") {
                $(this).attr('data-fultiimp', $(this).attr('data-fultiimp_temp'));
                $(this).attr('data-fultiimp_temp', "");
            }
        });
            
        //Se cambia el data-changed de las líneas
        $('[data-changed=1]tr').attr('data-changed', 0);

        //Se actualizan los valores originales de los totales
        $('#pieTabla td.diasTotal').each(function () {
            $(this).attr('data-originalvalue', $(this).children().html())
        })

    }

    //Se actualizan los valores modificados en el detalle de tarea
    var tareaModificada = function (idTarea, finalizado, totalEst, fechaFinEst, hayIndicaciones) {

        var lineaTarea = $('tr[data-t="' + idTarea + '"]');
        var datosCambiados = false;       

        //ETE
        var inputETE = $('tr[data-t="' + idTarea + '"] > td[headers=ETE] > input');

        if (totalEst != inputETE.val()) {
            inputETE.attr('data-autochange', '1');
            inputETE.val(totalEst);
            validarFormatearETE(inputETE);
            datosCambiados = true;            
        } 

        //FFE
        var inputFFE = $('tr[data-t="' + idTarea + '"] > td[headers=FFE] > input');

        if (fechaFinEst != inputFFE.val()) {
            inputFFE.attr('data-autochange', '1');
            inputFFE.val(fechaFinEst);
            actualizarFFE(inputFFE);
            inputFFE.attr('value', fechaFinEst);
            inputFFE.attr('data-originalvalue', fechaFinEst);
            datosCambiados = true;            
        }

        //Fin de tarea
        if (finalizado != parseInt(lineaTarea.attr('data-fin'))) {

            var chkFinalizado = $('tr[data-t="' + idTarea + '"] > td[headers=F] > input');
            chkFinalizado.attr('data-autochange', '1');

            clickFinalizarTarea(idTarea);
            datosCambiados = true;            

        }

        //Comentario
        if (hayIndicaciones != inputETE.hasClass('comentario')) {

            if (hayIndicaciones) {
                inputETE.addClass('comentario');
                datosCambiados = true;
            } else {
                inputETE.removeClass('comentario');
                datosCambiados = true;
            }

        }
        
        if (datosCambiados) grabacionCorrecta();        

    }
    var setIconoBitacora = function (sTipoElemento, sModoAccesoBitacora) {
        switch (sTipoElemento) {
            case "PSN":
                sTipoElemento = "PE";
                break;
            case "F":
            case "A":
                $('#btnBitacora').attr('src', IB.vars["strserver"] + "images/imgSeparador.gif");
                return;
        }
        switch (sModoAccesoBitacora) {
            case "E":
                sModoAccesoBitacora = "W";
                break;
            case "L":
                sModoAccesoBitacora = "R";
                break;
            case "X":
                sModoAccesoBitacora = "N";
                break;
        }
        var sImagen = IB.vars["strserver"] + "images/imgBT" + sTipoElemento + sModoAccesoBitacora + ".gif";
        $('#btnBitacora').attr('src', sImagen);
    }

    var abrirBuscador = function () {

        ocultarMas();
        dom.txtSearch.val('');

        //Se oculta el contenido de la pantalla al SR
        $('.ocultable').attr('aria-hidden', 'true');                

        $('#modalBusqueda').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#modalBusqueda').modal('show');
        
    }

    var cerrarBuscador = function () {
        $('#modalBusqueda').modal('hide');
    }

    var mostrarMas = function () {
        dom.btnBuscarSiguiente.show();
    }

    var ocultarMas = function () {
        dom.btnBuscarSiguiente.hide();
    }

    var scrollearALinea = function (linea) {
        var x = ($(linea).offset().top - indicadores.i_offsetTabla) + dom.contenedor.scrollTop();
        dom.contenedor.animate({ scrollTop: x });
    }

    var marcarEventosAttachados = function (linea) {
        linea.removeAttr('data-event');
    }

    var anadirControles = function (linea, dias, valoresCambiados) {

        if (dias.lunes.diaSemana !== null) anadirControlesDia(linea, "L");
        if (dias.martes.diaSemana !== null) anadirControlesDia(linea, "M");
        if (dias.miercoles.diaSemana !== null) anadirControlesDia(linea, "X");
        if (dias.jueves.diaSemana !== null) anadirControlesDia(linea, "J");
        if (dias.viernes.diaSemana !== null) anadirControlesDia(linea, "V");
        if (dias.sabado.diaSemana !== null) anadirControlesDia(linea, "S");
        if (dias.domingo.diaSemana !== null) anadirControlesDia(linea, "D");

        //ETE
        var sHtml = new StringBuilder();
        var celda = linea.find("td[headers='ETE']");

        sHtml.append('<input class="form-control txtHoras totalTarea ' + celda.attr('data-input-class') + '" type="text" ');
        sHtml.append('aria-label="' + celda.attr('data-aria-label') + '" ');
        sHtml.append('value="' + celda.attr('data-input-value') + '" ');
        celda.attr('data-input-value') == "" ? sHtml.append(' data-originalValue="" ') : sHtml.append(' data-originalValue="' + celda.attr('data-input-value') + '" ');
        if (celda.attr('data-input-disabled') == "1") sHtml.append('disabled ');
        sHtml.append('data-changed="0">');

        celda.html(sHtml.toString());

        //Se enmascaran los campos de importes        
        celda.find('.totalTarea').mask('999999,99');

        //Se eliminan los atributos temporales de la celda
        celda.removeAttr('data-input-class');
        celda.removeAttr('data-aria-label');
        celda.removeAttr('data-input-value');
        celda.removeAttr('data-input-disabled');

        //FFE
        sHtml = new StringBuilder();
        celda = linea.find("td[headers='FFE']");

        sHtml.append('<input class="form-control txtFecha calendar-off" type="text" ');
        sHtml.append('aria-label="' + celda.attr('data-aria-label') + '" ');
        //sHtml.append('value="' + celda.attr('data-input-value') + '" ');
        typeof celda.attr('data-input-value') !== "undefined" ? sHtml.append('value="' + celda.attr('data-input-value') + '" ') : sHtml.append('value="' + celda.attr('data-input-value') + '" ');
        celda.attr('data-input-value') == "" ? sHtml.append(' data-originalValue="" ') : sHtml.append(' data-originalValue="' + celda.attr('data-input-value') + '" ');
        celda.attr('data-input-disabled') == "1" ? sHtml.append(' disabled') : sHtml.append(' placeholder = "dd/mm/aaaa"');
        sHtml.append('data-changed="0">');

        celda.html(sHtml.toString());

        //Se eliminan los atributos temporales de la celda
        celda.removeAttr('data-aria-label');
        celda.removeAttr('data-input-value');
        celda.removeAttr('data-input-disabled');

        //Fin

        sHtml = new StringBuilder();
        celda = linea.find("td[headers='F']");

        sHtml.append('<input class="form-control chkHoras" type="checkbox" ');
        sHtml.append('aria-label="' + celda.attr('data-aria-label') + '" ');
        sHtml.append('data-originalValue="' + celda.attr('data-input-value') + '" ');
        if (celda.attr('data-input-disabled') == "1") sHtml.append('disabled ');
        if (celda.attr('data-input-value') == "1") sHtml.append(' checked');
        sHtml.append(' data-changed="0">');

        celda.html(sHtml.toString());

        //Se eliminan los atributos temporales de la celda
        celda.removeAttr('data-aria-label');
        celda.removeAttr('data-input-value');
        celda.removeAttr('data-input-disabled');

    }

    var anadirControlesDia = function (linea, dia) {
        var sHtml = new StringBuilder();

        var celda = linea.find("td[headers='" + dia + "']");

        var diaSemana = celda.attr('headers');

        sHtml.append('<input class="form-control txtHoras diasSemana ' + celda.attr('data-input-class') + '" type="text" ');
        sHtml.append('aria-label="' + celda.attr('data-aria-label') + '" ');
        sHtml.append('data-date="' + celda.attr('data-input-date') + '" ');
        sHtml.append('data-jorn="' + celda.attr('data-input-jorn') + '" ');
        sHtml.append('value="' + celda.attr('data-input-value') + '" ');
        celda.attr('data-input-value') == "" ? sHtml.append(' data-originalValue="' + accounting.formatNumber(0) + '" ') : sHtml.append(' data-originalValue="' + celda.attr('data-input-value') + '" ');
        if (celda.attr('data-input-disabled') == "1") sHtml.append('disabled ');
        //Si han cambiado las horas de una jornada marco la casilla para que sea grabada
        var horasJornada = $('#' + diaSemana).attr('data-horasJornada');
        if (horasJornada != 0) {
            var horas = accounting.unformat(celda.attr('data-input-value'), ",");
            var esfJornOriginal = accounting.unformat(celda.attr('data-input-jorn'), ",");
            var esfJornActual = horas / horasJornada;
            if (esfJornActual != esfJornOriginal) {
                sHtml.append('jorn-changed="1" ');
                linea.attr('data-changed', 1);
            }
            //else
            //    sHtml.append('jorn-changed="0" ');
        }
        sHtml.append('data-changed="0">');

        celda.html(sHtml.toString());

        //Se enmascaran los campos de importes
        celda.find('.diasSemana').mask('99,99');

        //Se eliminan los atributos temporales de la celda
        celda.removeAttr('data-input-class');
        celda.removeAttr('data-aria-label');
        celda.removeAttr('data-input-date');
        celda.removeAttr('data-input-value');
        celda.removeAttr('data-input-jorn');
        celda.removeAttr('data-input-disabled');

    }

    var incluirAccesibilidad = function (linea) {

        linea.attr("role", "row");

        linea.children().attr("role", "gridcell");

        linea.children().children().eq(1).attr("aria-hidden", "true");

        linea.children().children().eq(1).after('<span class="sr-only" role="button" aria-expanded="true">' + linea.attr('data-sr') + '</span>');

        linea.removeAttr('data-sr');

    }

    var incluirAccesibilidadTareas = function (linea) {

        linea.attr("role", "row");

        linea.children().attr("role", "gridcell");

        linea.children().children().eq(1).attr("aria-hidden", "true");

        linea.children().children().eq(1).after('<span class="sr-only" role="button">' + linea.attr('data-sr') + '</span>');

        linea.removeAttr('data-sr');

    }

    //Se eliminan las filas hijas de los PSNs cargados de BBDD que estén contraidos en la navegación entre semanas dentro de un mismo mes
    var eliminarFilasContraidas = function () {
        lineas.LineasCargadasContraidas().each(function () {
            $("#bodyTabla tr[data-parent*='" + $(this).attr('id') + "']").remove();
            $(this).attr('data-loaded', "0");
        });
    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        indicadores: indicadores,
        deAttachEvents: deAttachEvents,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        lineas: lineas,
        atacharEventosFilasTareasView: atacharEventosFilasTareasView,
        atacharEventosFilasTareasValoresCambiadosView: atacharEventosFilasTareasValoresCambiadosView,
        atacharEventosFilasPTPSNView: atacharEventosFilasPTPSNView,
        obtenerDescripcion: obtenerDescripcion,
        obtenerPropDia: obtenerPropDia,
        pintarCabeceraPie: pintarCabeceraPie,
        pintarTablaPSN: pintarTablaPSN,
        pintarTablaDesglose: pintarTablaDesglose,
        cambiarValoresTabla: cambiarValoresTabla,
        pintarOtrosConsumos: pintarOtrosConsumos,
        eliminarOtrosConsumos: eliminarOtrosConsumos,
        obtenerNaturaleza: obtenerNaturaleza,
        obtenerImputableGasvi: obtenerImputableGasvi,
        deshabilitarLink: deshabilitarLink,
        habilitarLink: habilitarLink,
        abrir: abrir,
        lineaCargada: lineaCargada,
        pintarCabeceraSemana: pintarCabeceraSemana,
        visualizarContenedor: visualizarContenedor,
        posicionarFoco: posicionarFoco,
        marcarLinea: marcarLinea,
        desmarcarLinea: desmarcarLinea,
        abrirLinea: abrirLinea,
        actualizarLineaAbierta: actualizarLineaAbierta,
        cerrarLinea: cerrarLinea,
        cebrear: cebrear,
        cerrarNivel: cerrarNivel,
        colorearNiveles: colorearNiveles,
        colorearNivel: colorearNivel,
        habilitarBtn: habilitarBtn,
        actualizarFFE: actualizarFFE,
        clickFinalizarTarea: clickFinalizarTarea,
        finalizarTarea: finalizarTarea,
        desFinalizarTarea: desFinalizarTarea,
        abrirModalComentario: abrirModalComentario,
        abrirModalConfirmacion: abrirModalConfirmacion,
        guardarComentario: guardarComentario,
        borrarComentario: borrarComentario,
        grabacionCorrecta: grabacionCorrecta,
        tareaModificada: tareaModificada,
        setIconoBitacora: setIconoBitacora,
        abrirBuscador: abrirBuscador,
        cerrarBuscador: cerrarBuscador,
        mostrarMas: mostrarMas,
        ocultarMas: ocultarMas,
        scrollearALinea: scrollearALinea,
        marcarEventosAttachados: marcarEventosAttachados,
        anadirControles: anadirControles,
        incluirAccesibilidad: incluirAccesibilidad,
        incluirAccesibilidadTareas: incluirAccesibilidadTareas,
        eliminarFilasContraidas: eliminarFilasContraidas
    }
})();