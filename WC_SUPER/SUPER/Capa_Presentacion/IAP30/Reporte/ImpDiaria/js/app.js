$(document).ready(function () { SUPER.IAP30.ImpDiaria.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ImpDiaria = SUPER.IAP30.ImpDiaria || {}

SUPER.IAP30.ImpDiaria.app = (function (view, Dia, appTarea) {

    //Variables de Ib.vars
    var primerDiaSemana;
    var ultimoDiaSemana;
    var primerDia;
    var diasEnSemana;
    var mes;
    var anno;
    var controlHuecos;
    var fAlta;
    var strUltimoMesCerrado;
    var primerDiaSemanaSiguiente, ultimoDiaSemanaAnterior;

    //Array declarativo de entities
    var dias;

    var fDesde, fHasta;

    //Booleano indicador de cambios
    var bCambios;

    //Booleano indicador apertura de todos los niveles através de la barra de nivel
    var bTodosNiveles;

    //Booleano indicador de navegación a semana siguiente
    var bSiguiente;

    //Mensajes a mostrar junto a la grabación correcta
    var sMensajes = "";

    /*Lógica de carga y visualización de datos*/
    window.onbeforeunload = function () {
        if (bCambios) return "Si continúas perderás los cambios que no has guardado.";
    }

    var init = function () {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

        //Manejador de fechas
        moment.locale('es');

        //Inicializa formateo
        accounting.settings = {
            number: {
                precision: 2,
                thousand: ".",
                decimal: ","
            }
        }

        cargarVariablesInicio();

        view.init();
        appTarea.init();

        asociarEventosInicio();

        cargarDatosInicio(false);

    }

    var cargarVariablesInicio = function () {

        dias = {};
        bCambios = false;
        bSiguiente = false;
        bTodosNiveles = false;

        //Recogemos en variables locales las variables más utilizadas de IB.vars        
        primerDiaSemana = parseInt(IB.vars.primerDiaSemana);
        ultimoDiaSemana = parseInt(IB.vars.ultimoDiaSemana);

        //Primer día imputable de la semana del 1 al 7 (Lunes a Domingo)
        primerDia = parseInt(IB.vars.primerDia) + 1;

        //Cantidad de días imputables en una semana
        diasEnSemana = parseInt(IB.vars.diasEnSemana);

        mes = parseInt(IB.vars.mes);
        anno = parseInt(IB.vars.anno);

        //Control de huecos, fecha de alta del recurso y fecha de la última imputación del recurso
        //controlHuecos = Boolean(IB.vars.controlHuecos);
        controlHuecos = (IB.vars.controlHuecos === 'True');
        fAlta = IB.vars.fAlta.toDate();

        //Declaración de la semana
        dias.lunes = Dia.noValues();
        dias.martes = Dia.noValues();
        dias.miercoles = Dia.noValues();
        dias.jueves = Dia.noValues();
        dias.viernes = Dia.noValues();
        dias.sabado = Dia.noValues();
        dias.domingo = Dia.noValues();

        fDesde = new Date(anno, mes, primerDiaSemana);
        fHasta = new Date(anno, mes, ultimoDiaSemana);

        //var a = fDesde.getFullYear();
        //var m = fDesde.getMonth();
        //fInicioMes = new Date(a, m, 1);
        //fFinMes = new Date(a, m + 1, 0);

    }

    var asociarEventosInicio = function () {

        //Control de scroll para atachar eventos y añadir controles a las filas
        view.attachEvents("scrollstop", view.dom.contenedor, atacharEventosFilas);

        //Redirecciones de botones
        view.attachEvents("keypress click", view.dom.btnGASVI, redirigirGASVI);
        view.attachEvents("keypress click", view.dom.btnGASVILite, redirigirGASVI);
        view.attachEvents("keypress click", view.dom.btnSalir, redirigirCalendario);
        view.attachEvents("keypress click", view.dom.btnSalirLite, redirigirCalendario);
        view.attachEvents("keypress click", view.dom.btnBitacora, goBitacora);                

        //Botones de expasión/contracción de niveles   
        view.attachEvents("keypress click", view.dom.nivel1, abrirNivel0);
        view.attachEvents("keypress click", view.dom.nivel2, abrirNivel1);
        //view.attachEvents("keypress click", view.dom.nivel3, preguntarAbrirTodosNiveles);
        view.attachEvents("keypress click", view.dom.nivel3, abrirTodosNiveles);

        //Botón de bomba
        view.attachEvents("keypress click", view.dom.bomba, abrirBomba);

        //Botón de búsqueda
        view.attachEvents("keypress click", view.dom.btnBusqueda, view.abrirBuscador);

        //Búsqueda
        view.attachEvents("keyup", view.dom.txtSearch, buscarLineas);

        //Ir siguiente en búsqueda btnBuscarSiguiente
        view.attachEvents("keypress click", view.dom.btnBuscarSiguiente, buscarLineaSiguiente);       

        //Aceptar del modal de comentario
        view.attachEvents("keypress click", view.dom.btnAceptarComentario, aceptarComentario);

        //Control de caracteres en el modal de comentario
        view.attachEvents("keyup", view.dom.txtComentario, controlarCaracteresComentario);

    }

    var cargarDatosInicio = function (mismoMes) {

        //Si la navegación corresponde dentro del mismo mes se eliminan las filas hijas de los PSNs cargados de BBDD que estén contraidos
        if (mismoMes) {
            view.eliminarFilasContraidas();            
        } else { //Si se cambia de mes se reinicia el estado de las barras de expansión
            view.colorearNivel("1");
        }

        $.when(obtenerDatosIAP(mismoMes)).then(function () {
            view.visualizarContenedor();
            view.pintarCabeceraSemana();
            atacharEventosFilas();
            //si se vuelve de gasvi para posicionar el foco en la línea que lo contenia
            if (IB.vars.filaSel != "" && IB.vars.filaSel != "undefined") {
                //Hay que cargar de BBDD las líneas a visualizar
                if (view.lineas.linea(IB.vars.filaSel).length == 0) {

                    var padres = IB.vars.filaSelParents.split(" ");
                    pedirHijos(padres, padres.length - 1);

                } else {
                    view.posicionarFoco();
                    //Para navegar con la tarea seleccionada abierta comentar estas dos lineas y refrescar estas variables en los clicks
                    IB.vars.filaSel = "";
                    IB.vars.fechaSel = "";
                }

            } else {
                view.desmarcarLinea();
            }
            IB.procesando.ocultar();
        });

    }    

    //función recursiva que va abriendo todos los hijos del array de padres esperando a la promesa de cada uno.
    var pedirHijos = function (padres, i) {
        IB.procesando.mostrar();
        $.when(cargarLinea(view.lineas.linea(padres[i]).children(":first"), 1, false)).then(function () {
            if (i > 0) { //Quedan hijos por abrir
                pedirHijos(padres, (i - 1));
            } else { //No hay más hijos
                view.posicionarFoco();
                IB.procesando.ocultar();
                //Para navegar con la tarea seleccionada abierta comentar estas dos lineas y refrescar estas variables en los clicks
                IB.vars.filaSel = "";
                IB.vars.fechaSel = "";
            }
        });
    }

    //Cálculo de festivos y horas de jornadas
    var obtenerHorasDefecto = function (data) {

        //Primer día imputable de la semana 
        var diaImputable = primerDiaSemana;

        ////El bucle recorre solo la cantidad de días imputables de la semana
        for (var i = 0; i < diasEnSemana ; i++) {

            //Objeto intermedio sobre el que volcar los datos
            var diaAGrabar = {};

            // i + primerDia nos situa en el número de día en la semana del 1 al 7
            switch (i + primerDia) {
                case 1:
                    diaAGrabar = dias.lunes;
                    break;
                case 2:
                    diaAGrabar = dias.martes;
                    break;
                case 3:
                    diaAGrabar = dias.miercoles;
                    break;
                case 4:
                    diaAGrabar = dias.jueves;
                    break;
                case 5:
                    diaAGrabar = dias.viernes;
                    break;
                case 6:
                    diaAGrabar = dias.sabado;
                    break;
                default:
                    diaAGrabar = dias.domingo;
            }

            // i + (primerDia - 1) nos situa en el número de día en la semana del 0 al 6 para el array de laborables
            diaAGrabar.laborable = parseInt(IB.vars.aSemLab[i + (primerDia - 1)]);

            diaAGrabar.diaSemana = diaImputable;
            diaAGrabar.fechaSemana = new Date(anno, mes, diaImputable);

            //Comprobamos horarios especiales(Si tiene jornada reducida y si la fecha actual se encuentra entre las fechas de comienzo de jornada reducida y final de jornada reducida)

            var fecDesRed = IB.vars['fecDesRed'].toDate();

            var fechasRed = IB.vars['fechasRed'].toDate();

            if ((IB.vars['jornadaReducida']) && (new Date(diaAGrabar.fechaSemana) >= fecDesRed) && (new Date(diaAGrabar.fechaSemana) <= fechasRed)) {

                diaAGrabar.horasJornada = parseFloat(IB.vars['nHorasRed'].replace(/,/g, '.')).toFixed(2);

            }
            else {

                diaAGrabar.horasJornada = parseFloat(data[i].t067_horas).toFixed(2);

            }

            diaAGrabar.festivo = data[i].t067_festivo;

            diaImputable++;
        }

    }

    //Controlamos la habilitación de los botones de navegación semanal
    var controlSemAntSig = function () {

        var hueco = limiteAvance = 0;
        var intDiferencia;

        //Control de la navegación a la semana anterior, solo se permite retroceder si el mes de la semana anterior no está cerrado y el último día de la semana anterior es mayor o igual a la fecha de alta del recurso. 
        //Para comprobarlo se utiliza el día anterior al primer día de la actual semana
        ultimoDiaSemanaAnterior = new Date(fDesde);

        ultimoDiaSemanaAnterior.setDate(ultimoDiaSemanaAnterior.getDate() - 1);
        if (mesCerrado(ultimoDiaSemanaAnterior.getMonth() + 1, ultimoDiaSemanaAnterior.getFullYear()) || (ultimoDiaSemanaAnterior < fAlta)) {
            view.deshabilitarLink(view.dom.semAnt);
            view.deAttachEvents(view.dom.semAnt);
        }
        else {
            view.habilitarLink(view.dom.semAnt);
            view.deAttachEvents(view.dom.semAnt);
            view.attachEvents("keypress click", view.dom.semAnt, redirigirSemanaAnterior);
        }

        //Control de navegación a la semana siguiente. Para navegar a la semana siguiente han de estar imputados(algo imputado) todos los días laborables no festivos de la semana si existe el control de huecos. Tampoco se puede navegar a 
        //la semana siguiente si se sobrepasa el límite de 62 días desde el siguiente día al último mes cerrado.
        primerDiaSemanaSiguiente = new Date(fHasta);

        primerDiaSemanaSiguiente.setDate(primerDiaSemanaSiguiente.getDate() + 1);

        if (controlHuecos) {

            $.each(dias, function () {
                if (this.fechaSemana >= fAlta) {//Solo se pueden considerar huecos días en los que estaba dado de alta el recurso
                    if (this.laborable && !this.festivo && this.horasJornada != "0.00" && (this.totalConsumo == 0 || this.totalConsumo === null)) hueco = 1;
                }
            });

        }

        //Se comprueba si la semana siguiente sobrepasa el límite de 62 días a partir de fecha de mes cerrado
        intDiferencia = primerDiaSemanaSiguiente.getTime() - strUltimoMesCerrado.getTime();

        //62 días en milisegundos (1 día 86400000).
        if (intDiferencia > 5356800000) limiteAvance = 1

        if (hueco || limiteAvance) {

            view.deshabilitarLink(view.dom.semSig);
            view.deAttachEvents(view.dom.semSig);
            bSiguiente = false;

        }

        else {

            view.habilitarLink(view.dom.semSig);
            view.deAttachEvents(view.dom.semSig);
            view.attachEvents("keypress click", view.dom.semSig, redirigirSemanaSiguiente);
            bSiguiente = true;

        }

    }

    //Se controla la habilitación del botón Grabar e ir a la siguiente semana en cada imputación diaria
    var controlGrabarSig = function () {

        var hueco = limiteAvance = 0;
        var intDiferencia;

        if (controlHuecos) {

            $.each(dias, function () {
                if (this.fechaSemana >= fAlta) {//Solo se pueden considerar huecos días en los que estaba dado de alta el recurso
                    if (this.laborable && !this.festivo && this.horasJornada != "0.00" && (this.totalConsumo == 0 || this.totalConsumo === null)) hueco = 1;
                }
            });

        }

        //Se comprueba si la semana siguiente sobrepasa el límite de 62 días a partir de fecha de mes cerrado
        intDiferencia = primerDiaSemanaSiguiente.getTime() - strUltimoMesCerrado.getTime();

        //62 días en milisegundos (1 día 86400000).
        if (intDiferencia > 5356800000) limiteAvance = 1

        if (hueco || limiteAvance) {

            bSiguiente = false;
            view.habilitarBtn(view.dom.btnGrabarSig);

        }

        else {

            bSiguiente = true;
            view.habilitarBtn(view.dom.btnGrabarSig);
            view.habilitarBtn(view.dom.btnGrabarSig, 1, grabarSig);

        }

    }

    cambioTotales = function () {

        actualizarTotales();
        controlGrabarSig();

    }

    var mesCerrado = function (mesSel, annoSel) {
        var strUMC = IB.vars.UMC_IAP;
        try {
            if (strUMC != "") {
                strUltimoMesCerrado = new Date(strUMC.substring(0, 4), eval(strUMC.substring(4, 6)), 0);
            } else {
                strUltimoMesCerrado = new Date(1999, 11, 31);
            }
            var strFechaActual = new Date(annoSel, mesSel - 1, 1);
            return (strFechaActual < strUltimoMesCerrado) ? true : false;

        } catch (e) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Error al comprobar si el mes está cerrado : " + e);
        }
    }

    //Si la suma de las líneas de los consumos no coincide con el total es que existen otros consumos
    var otrosConsumos = function () {

        var hayOtrosConsumos = 0;

        $.each(dias, function () {
            //if (this.totalConsumo != this.totalConsumoProys) { hayOtrosConsumos = 1; } hayOtrosConsumos = 1;
            if (this.totalConsumo != this.totalConsumoProys && this.totalConsumoProys != null) {
                hayOtrosConsumos = 1;
            }
        });

        if (hayOtrosConsumos) view.pintarOtrosConsumos(dias);

    }

    //Función de redirección al detalle de tarea
    redirigirTarea = function (e) {

        IB.procesando.mostrar();

        var lineaTarea = view.lineas.lineaActiva();

        var tarea = lineaTarea.attr('data-t');
        var obligaest = lineaTarea.attr('data-obligaest');
        var PT = lineaTarea.attr('data-pt');
        var desTarea = view.obtenerDescripcion(lineaTarea);

        //A petición de Miren se sustituye como parámetro el estado de la tarea por el estado del proyecto
        var estado = lineaTarea.attr('data-estado');
        var estadopsn = view.lineas.lineaPSN(lineaTarea.attr('data-psn')).attr('data-estado');

        var imputacion = lineaTarea.attr('data-imp');

        //Variables que necesita la modal de detalle de tarea
        var sModoContainer = "E";

        $.when(controlarSalir(e)).then(function () {

            setTimeout(function () {
                //Si el proyecto está cerrado o es histórico, se accede en modo consulta
                if (estadopsn == "C" || estadopsn == "H") sModoContainer = "C";
                IB.vars.idTarea = tarea;
                IB.vars.estadotarea = estado;
                IB.vars.imputacion = imputacion;
                IB.vars.estadopsn = estadopsn;
                IB.vars.sModoContainer = sModoContainer;
                appTarea.aperturaModalDetalleTarea();
            }, 500);

        });
    }

    var redirigirGASVI = function (e) {

        IB.procesando.mostrar();

        var nPSN = "";
        var sF = "";
        var nN = "";

        var linea = view.lineas.lineaActiva();
        var inputDia = view.selectores.sel_preInput;
        var fSel = linea.attr('id')
        var fSelParents = linea.attr('data-parent')

        if (linea.length > 0 && (view.obtenerImputableGasvi(linea.attr('data-psn')) === 'false')) {
            IB.procesando.ocultar();
            IB.bsalert.toast("El proyecto seleccionado no permite la tramitación de notas de GASVI.", "info");
        } else {
            $.when(controlarCambios(e)).then(function () {

                setTimeout(function () {
                    if (linea.length > 0) {

                        nPSN = linea.attr('data-psn');
                        nN = view.obtenerNaturaleza(nPSN);

                        if (inputDia != "") sF = inputDia.attr('data-date');
                    }

                    qs = IB.uri.encode("fSel=" + fSel + "&fSelParents=" + fSelParents + "&nPSN=" + nPSN + "&sF=" + sF + "&nN=" + nN
                        + "&ipd=" + IB.vars.primerDiaSemana + "&iud=" + IB.vars.ultimoDiaSemana + "&ipds=" + IB.vars.primerDia
                        + "&ides=" + IB.vars.diasEnSemana + "&im=" + IB.vars.mes + "&ia=" + IB.vars.anno + "&sr=" + IB.vars.strRango
                        + "&srm=" + IB.vars.strRangoMini
                        );
                    location.href = "../GASVI/Default.aspx?" + qs;
                }, 500);

            });
        }
    }

    goBitacora = function (e) {

        IB.procesando.mostrar();

        //$.when(controlarCambios(e)).then(function () { setTimeout(function () { location.href = "../Bitacora/BitacoraPE/Default.aspx"; }, 500); });        
        var nPSN = "";
        var sF = "";
        var nN = "";
        var sTipo = "";
        var sAcceso = "";

        var linea = view.lineas.lineaActiva();
        var inputDia = view.selectores.sel_preInput;
        var fSel = linea.attr('id')

        $.when(controlarCambios(e)).then(function () {

            setTimeout(function () {
                if (linea.length > 0) {

                    nPSN = linea.attr('data-psn');
                    //nN = view.obtenerNaturaleza(nPSN);
                    if (inputDia != "") sF = inputDia.attr('data-date');
                    sTipo = linea.attr('data-tipo');
                    sAcceso = linea.attr('data-bitacora');
                    if (sAcceso == "X") {
                        IB.procesando.ocultar();
                        IB.bsalert.toast("Acceso a bitácora desde IAP no permitido.", "info");
                    }
                    else {
                        switch (sTipo) {
                            case "PSN":
                                qs = IB.uri.encode("ori=reporteIAP&fSel=" + fSel + "&nPSN=" + nPSN + "&sF=" + sF + "&ipd=" + IB.vars.primerDiaSemana
                                    + "&iud=" + IB.vars.ultimoDiaSemana + "&ipds=" + IB.vars.primerDia + "&ides=" + IB.vars.diasEnSemana
                                    + "&im=" + IB.vars.mes + "&ia=" + IB.vars.anno + "&sr=" + IB.vars.strRango + "&srm=" + IB.vars.strRangoMini
                                    );
                                location.href = "../Bitacora/BitacoraPE/Default.aspx?" + qs;
                                break;
                            case "PT":
                                qs = IB.uri.encode("ori=reporteIAP&fSel=" + fSel + "&nPSN=" + nPSN + "&sF=" + sF + "&ipd=" + IB.vars.primerDiaSemana
                                    + "&iud=" + IB.vars.ultimoDiaSemana + "&ipds=" + IB.vars.primerDia + "&ides=" + IB.vars.diasEnSemana
                                    + "&im=" + IB.vars.mes + "&ia=" + IB.vars.anno + "&sr=" + IB.vars.strRango + "&srm=" + IB.vars.strRangoMini
                                    + "&nPT=" + linea.attr('data-pt')
                                    );
                                location.href = "../Bitacora/BitacoraPT/Default.aspx?" + qs;
                                break;
                            case "T":
                                qs = IB.uri.encode("ori=reporteIAP&fSel=" + fSel + "&nPSN=" + nPSN + "&sF=" + sF + "&ipd=" + IB.vars.primerDiaSemana
                                    + "&iud=" + IB.vars.ultimoDiaSemana + "&ipds=" + IB.vars.primerDia + "&ides=" + IB.vars.diasEnSemana
                                    + "&im=" + IB.vars.mes + "&ia=" + IB.vars.anno + "&sr=" + IB.vars.strRango + "&srm=" + IB.vars.strRangoMini
                                    + "&nPT=" + linea.attr('data-pt') + "&nT=" + linea.attr('data-t')
                                    );
                                location.href = "../Bitacora/BitacoraTarea/Default.aspx?" + qs;
                                break;
                        }
                    }
                }
            }, 500);

        });
    }

    var redirigirCalendario = function (e) {

        IB.procesando.mostrar();

        var qs = IB.uri.encode("im=" + mes + "&ia=" + anno);

        $.when(controlarCambios(e)).then(function () {
            setTimeout(function () { location.href = "../Calendario/Default.aspx?" + qs; }, 500);
        });

    }

    var redirigirSemanaAnterior = function (e) {

        IB.procesando.mostrar();

        var qs;
        var fprimerDiaSemanaAnt; var fultimoDiaSemanaAnt;
        var primerDiaSemanaAnt; var ultimoDiaSemanaAnt; var primerDiaAnt; var diasEnSemanaAnt; var mesAnt; var mAnt; var yAnt; var strRango; var strRangoMini;

        //Se usa moment.js para sacar el primer día de la semana anterior.
        var fprimerDiaSemanaAnt = new Date(moment(ultimoDiaSemanaAnterior).startOf('week'));

        $.when(controlarCambios(e)).then(function () {

            //setTimeout(function () { //Si hay cambio de mes se recoge como primer día de la semana anterior el primer día del mes
            if (!moment(fprimerDiaSemanaAnt).isSame(ultimoDiaSemanaAnterior, 'month')) fprimerDiaSemanaAnt = new Date(moment(ultimoDiaSemanaAnterior).startOf('month'));

            //Se saca la diferencia entre los mismos. Para incluir el primer día se suma 1
            diasEnSemanaAnt = moment(ultimoDiaSemanaAnterior).diff(moment(fprimerDiaSemanaAnt), 'days') + 1;

            //Se pasa el nº de día a String
            ultimoDiaSemanaAnt = ultimoDiaSemanaAnterior.getDate().toString();
            primerDiaSemanaAnt = fprimerDiaSemanaAnt.getDate().toString();

            primerDiaAnt = fprimerDiaSemanaAnt.getDay() - 1
            mesAnt = moment.months(ultimoDiaSemanaAnterior.getMonth()).capitalize();
            mAnt = ultimoDiaSemanaAnterior.getMonth();
            yAnt = ultimoDiaSemanaAnterior.getFullYear().toString();

            strRango = "Semana del " + primerDiaSemanaAnt + " al " + ultimoDiaSemanaAnt + " de " + mesAnt + " - " + yAnt;

            strRangoMini = primerDiaSemanaAnt + " - " + ultimoDiaSemanaAnt + " " + mesAnt + " - " + yAnt;

            cambiarSemana(strRango, strRangoMini, primerDiaSemanaAnt, ultimoDiaSemanaAnt, primerDiaAnt, diasEnSemanaAnt, mAnt, yAnt)
            //}, 500);                        

        });

    }

    var redirigirSemanaSiguiente = function (e) {

        IB.procesando.mostrar();

        var qs;
        var fprimerDiaSemanaSig; var fultimoDiaSemanaSig;
        var primerDiaSemanaSig; var ultimoDiaSemanaSig; var primerDiaSig; var diasEnSemanaSig; var mesSig; var mSig; var ySig; var strRango;

        //Se usa moment.js para sacar el último día de la semana siguiente.
        var fultimoDiaSemanaSig = new Date(moment(primerDiaSemanaSiguiente).endOf('week'));

        $.when(controlarCambios(e)).then(function () {

            //setTimeout(function () { //Si hay cambio de mes se recoge como último día de la semana siguiente el último día del mes
            if (!moment(fultimoDiaSemanaSig).isSame(primerDiaSemanaSiguiente, 'month')) fultimoDiaSemanaSig = new Date(moment(ultimoDiaSemanaAnterior).endOf('month'));

            //Se saca la diferencia entre los mismos. Para incluir el primer día se suma 1
            diasEnSemanaSig = moment(fultimoDiaSemanaSig).diff(moment(primerDiaSemanaSiguiente), 'days') + 1;

            //Se pasa el nº de día a String
            ultimoDiaSemanaSig = fultimoDiaSemanaSig.getDate().toString();
            primerDiaSemanaSig = primerDiaSemanaSiguiente.getDate().toString();

            primerDiaSig = primerDiaSemanaSiguiente.getDay() - 1
            mesSig = moment.months(primerDiaSemanaSiguiente.getMonth()).capitalize();
            mSig = primerDiaSemanaSiguiente.getMonth();
            ySig = primerDiaSemanaSiguiente.getFullYear().toString();

            strRango = "Semana del " + primerDiaSemanaSig + " al " + ultimoDiaSemanaSig + " de " + mesSig + " - " + ySig;

            strRangoMini = primerDiaSemanaSig + " - " + ultimoDiaSemanaSig + " " + mesSig + " - " + ySig;

            cambiarSemana(strRango, strRangoMini, primerDiaSemanaSig, ultimoDiaSemanaSig, primerDiaSig, diasEnSemanaSig, mSig, ySig)

            //}, 500);                        

        });

    }

    var cambiarSemana = function (strRango, strRangoMini, primerDiaSemanaNueva, ultimoDiaSemanaNueva, primerDiaNueva, diasEnSemanaNueva, mNueva, yNueva) {

        //Comprobación de cambio de mes
        var mismoMes = moment(fDesde).isSame(new Date(yNueva, mNueva, primerDiaSemanaNueva), 'month');

        IB.vars.primerDiaSemana = primerDiaSemanaNueva;
        IB.vars.ultimoDiaSemana = ultimoDiaSemanaNueva;
        IB.vars.primerDia = primerDiaNueva;
        IB.vars.diasEnSemana = diasEnSemanaNueva;
        IB.vars.mes = mNueva;
        IB.vars.anno = yNueva;
        IB.vars.strRango = strRango;
        IB.vars.strRangoMini = strRangoMini;
        //IB.vars.filaSel = '" + filaSel + "';";
        //IB.vars.fechaSel = '" + fechaSel + "';";
        
        cargarVariablesInicio();
        cargarDatosInicio(mismoMes);
        view.eliminarOtrosConsumos();

    }

    var clickLinea = function (event) {

        var srcobj = event.target ? event.target : event.srcElement;

        //Si el elemento no es el td se busca en la jerarquia superior
        //if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td');
        srcobj = $(srcobj).parent().closest('td');

        //Solo se buscan hijos si no es una tarea
        if ($(srcobj).parent().attr('data-tipo') != "T") {
            cargarLinea($(srcobj), 1, true);
        }

        //Se establece el icono de bitácora que le corresponda
        //if ($(thisObj).length > 0) view.setIconoBitacora($(srcobj).parent().attr('data-tipo'), $(srcobj).parent().attr('data-bitacora'));

    }

    var abrirNivel0 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);
        view.cebrear();
    }

    //Muestra todos los proyectos técnicos de los proyectos económicos
    var abrirNivel1 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);

        cargarLinea(view.lineas.PE(), 2, false);

    }

    //var preguntarAbrirTodosNiveles = function (e) {        

    //    if (!bTodosNiveles) {
    //        //Puede que se hayan cargado todos los niveles sin usar las barras
    //        var lineasSinCargar = view.lineas.padresTLineas().map(function () { if ($(this).attr('data-loaded') == "0") return this; });
    //        if (lineasSinCargar.length == 0) {
    //            bTodosNiveles = true;
    //            abrirTodosNiveles(e);
    //        } else {
    //            $.when(IB.bsconfirm.confirm("primary", "", "Atención, en función del número de tareas que tengas disponibles, la opción de despliegue masivo puede llevar bastante tiempo. ¿Quieres continuar?</br></br>")).then(function () {
    //                bTodosNiveles = true;
    //                abrirTodosNiveles(e);
    //            });
    //        }                        
    //    } else {
    //        abrirTodosNiveles(e);
    //    }

    //}

    //Muestra todas las fases, actividades y tareas de los proyectos técnicos
    var abrirTodosNiveles = function (e) {
        view.colorearNiveles(e);
        //view.cerrarNivel(e);

        cargarLinea(view.lineas.PE(), 3, false);

    }

    var abrirBomba = function (e) {

        var lineaActiva = view.lineas.lineaActiva();
        //view.cerrarNivel(e);
		if (lineaActiva.length > 0 && lineaActiva.attr("data-tipo") != "T") {
				view.marcarLinea(lineaActiva);
				cargarLinea(lineaActiva, 4, false);
		}        

    }

    //Tratamiento de la visualización de las líneas de la tabla
    var cargarLinea = function (thisObj, proceso, procesando) {        

        //Proceso 1 -> Abrir línea a línea
        //Proceso 2 -> Abrir Proyectos técnicos
        //Proceso 3 -> Abrir todo
        //Proceso 4 -> Bomba(abrir descendencia de línea activa)
        switch (proceso) {
            case 1:                
                if (view.abrir($(thisObj))) {
                    if (view.lineaCargada($(thisObj))) {                        
                        view.abrirLinea($(thisObj).parent(), proceso);
                        return view.cebrear();
                    } else {

                        var defer = $.Deferred();

                        var tipo = $(thisObj).parent().attr('data-tipo');
                        var idPadre = $(thisObj).parent().attr('id');

                        var procedimiento;

                        switch (tipo) {
                            case "PSN":
                                var lPSN = [];
                                lPSN.push(parseInt($(thisObj).parent().attr('data-psn')));
                                procedimiento = ObtenerConsumosIAPSemanaPSN_D(lPSN, 1, procesando);
                                break;
                            case "PT":
                                var nPT = parseInt($(thisObj).parent().attr('data-pt'));
                                procedimiento = ObtenerConsumosIAPSemanaPT_D(nPT, 1, procesando);
                                break;
                            case "F":
                                var nF = parseInt($(thisObj).parent().attr('data-f'));
                                procedimiento = ObtenerConsumosIAPSemanaF(nF, 1, procesando);
                                break;
                            case "A":
                                var nActividad = parseInt($(thisObj).parent().attr('data-a'));
                                procedimiento = ObtenerConsumosIAPSemanaA(nActividad, procesando);
                                break;
                        }
                        $.when(procedimiento).then(
                                    function (data) {
                                        view.pintarTablaDesglose(data, idPadre, dias, false);
                                        view.actualizarLineaAbierta(view.lineas.linea(idPadre));
                                        atacharEventosFilas();
                                        view.cebrear();
                                        defer.resolve();
                                    }
                                );
                        return defer.promise();

                    }
                }
                else {
                    view.cerrarLinea($(thisObj), proceso);
                    return view.cebrear();
                }
                break;
            case 2:
                var lineasSinCargar;

                lineasSinCargar = $(thisObj).map(function () { if ($(this).attr('data-loaded') == "0") return this; });

                //Si hay lineas sin cargar de BBDD se piden a esta
                if (lineasSinCargar.length > 0) {

                    IB.procesando.mostrar();                    

                    var lPSN = [];                    

                    lineasSinCargar.each(function () {                        
                        lPSN.push(parseInt($(this).attr('data-psn')));
                    });

                    $.when(ObtenerConsumosIAPSemanaPSN_D(lPSN, 1, procesando)).then(
                                    function (data) {
                                        var dataPSN = [];
                                        var tmpPSN = 0;

                                        var inicio = Date.now();
                                        for (var i = 0; i <= data.length; i++) {
                                            if (i == data.length || (tmpPSN != 0 && tmpPSN != data[i].t305_idproyectosubnodo)) {
                                                view.pintarTablaDesglose(dataPSN, 'PSN' + tmpPSN, dias, true);
                                                dataPSN = [];
                                            }
                                            if (i === (data.length)) break;
                                            dataPSN.push(data[i]);
                                            tmpPSN = data[i].t305_idproyectosubnodo
                                        }
                                        $('#ContadorRender').html(Date.now() - inicio + ' ms.');
                                        view.abrirLinea($(thisObj), proceso);
                                        atacharEventosFilas();
                                        view.cebrear();
                                        IB.procesando.ocultar();
                                    }
                                );
                    

                } else {
                    view.abrirLinea($(thisObj), proceso);
                    return view.cebrear();

                }

                break;
            case 3:

                var lineasSinCargar;

                //Se cogen los proyectos que no están desplegados completamente
                lineasSinCargar = $(thisObj).map(function () { if (view.lineas.PSNDespliegueCompleto($(this).attr('data-psn')).length != 0) return this; });

                if (lineasSinCargar.length > 0) {

                    IB.procesando.mostrar();

                    var lPSN = [];

                    lineasSinCargar.each(function () {
                        lPSN.push(parseInt($(this).attr('data-psn')));
                    });

                    $.when(ObtenerConsumosIAPSemanaPSN_D(lPSN, 0, procesando)).then(
                                   function (data) {
                                       var dataPSN = [];
                                       var tmpPSN = 0;
                                       var inicio = Date.now();
                                       for (var i = 0; i <= data.length; i++) {
                                           if (i == data.length || (tmpPSN != 0 && tmpPSN != data[i].t305_idproyectosubnodo)) {
                                               view.pintarTablaDesglose(dataPSN, 'PSN' + tmpPSN, dias, true);
                                               dataPSN = [];
                                           }
                                           if (i === (data.length)) break;
                                           dataPSN.push(data[i]);
                                           tmpPSN = data[i].t305_idproyectosubnodo
                                       }
                                       $('#ContadorRender').html(Date.now() - inicio + ' ms.');
                                       view.abrirLinea($(thisObj), proceso);
                                       atacharEventosFilas();
                                       view.cebrear();
                                       IB.procesando.ocultar();
                                   }
                               );                    

                } else {
                    view.abrirLinea($(thisObj), proceso);
                    return view.cebrear();
                }
                break;
            case 4:
                //Se desatacha el evento para evitar ejecuciones indeseadas
                view.deAttachEvents(view.dom.bomba);

                var lineasSinCargar = view.lineas.lineaDesActiva().map(function () { if ($(this).attr('data-loaded') == "0") return this; });

                //Si algún elemento del desglose del elemento seleccionado no ha sido traido de BBDD se trae
                if ($(thisObj).attr('data-loaded') == "0" || lineasSinCargar.length > 0) {

                    IB.procesando.mostrar();

                    var tipo = $(thisObj).attr('data-tipo');
                    var idPadre = $(thisObj).attr('id');
                    var procedimiento;

                    switch (tipo) {
                        case "PSN":
                            var lPSN = [];
                            lPSN.push(parseInt($(thisObj).attr('data-psn')));
                            procedimiento = ObtenerConsumosIAPSemanaPSN_D(lPSN, 0, procesando);
                            break;
                        case "PT":
                            var nPT = parseInt($(thisObj).attr('data-pt'));
                            procedimiento = ObtenerConsumosIAPSemanaPT_D(nPT, 0, procesando);                            
                            break;
                        case "F":
                            var nF = parseInt($(thisObj).attr('data-f'));
                            procedimiento = ObtenerConsumosIAPSemanaF(nF, 0, procesando);                            
                            break;
                        case "A":
                            var nActividad = parseInt($(thisObj).attr('data-a'));
                            procedimiento = ObtenerConsumosIAPSemanaA(nActividad, procesando);                            
                            break;
                    }
                    $.when(procedimiento).then(
                                function (data) {
                                    var inicio = Date.now();
                                    view.pintarTablaDesglose(data, idPadre, dias, true);
                                    $('#ContadorRender').html(Date.now() - inicio + ' ms.');
                                    view.attachEvents("keypress click", view.dom.bomba, abrirBomba);
                                    view.abrirLinea($(thisObj), proceso);
                                    atacharEventosFilas();
                                    view.cebrear();
                                    IB.procesando.ocultar();
                                }
                            );

                } else {

                    view.attachEvents("keypress click", view.dom.bomba, abrirBomba);
                    view.abrirLinea($(thisObj), proceso);
                    return view.cebrear();
                }

                break;
        }        
    }

    //Attacheo de eventos sobre las nuevas filas insertadas al DOM
    var atacharEventosFilas = function () {

        view.lineas.lineasVisiblesEnTablaSinEventos().each(function () {

            if ($(this).attr('data-tipo') != "T") {

                //Clicks sobre el más o el menos
                view.attachEvents("keypress click", $(this).find(view.selectores.sel_nombreLinea), clickLinea);

                if ($(this).attr('data-tipo') == "PSN" || $(this).attr('data-tipo') == "PT") {
                    view.atacharEventosFilasPTPSNView($(this));
                }

                view.incluirAccesibilidad($(this))

            } else {

                view.incluirAccesibilidadTareas($(this))

                view.anadirControles($(this), dias, false);

                //Evento de cambios en los campos
                view.attachEvents("change", $(this).find(view.selectores.sel_inputs), hayCambios);
                view.attachEvents("keydown", $(this).find(view.selectores.sel_inputs), validarCambio);

                //Evento de cierre de tarea
                view.attachEvents("change", $(this).find(view.selectores.sel_checkFin), finTareaRecur);

                //Doble click sobre tarea
                view.attachEvents("dblclick", $(this).find(view.selectores.sel_lineaTarea), redirigirTarea);

                //Click derecho para comentario
                view.attachEvents("contextmenu", $(this).find(view.selectores.sel_inputdiasSemana), abrirComentario);

                //Se habilita el botón de comentario al entrar en un input semanal
                view.attachEvents("focusin", $(this).find(view.selectores.sel_inputdiasSemana), cargarComentario);

                view.atacharEventosFilasTareasView($(this));
            }

            view.marcarEventosAttachados($(this));

        });
    }

    //Attacheo de eventos sobre las tareas a las que se les ha cambiado valor en la navegación entre semanas de un mismo mes
    var atacharEventosFilasValoresCambiados = function () {

        view.lineas.lineasTareasVisiblesEnTablaSinEventos().each(function () {

            view.anadirControles($(this), dias, true);

            //Evento de cambios en los campos
            view.attachEvents("change", $(this).find(view.selectores.sel_inputs), hayCambios);
            view.attachEvents("keydown", $(this).find(view.selectores.sel_inputs), validarCambio);

            //Evento de cierre de tarea
            view.attachEvents("change", $(this).find(view.selectores.sel_checkFin), finTareaRecur);

            //Click derecho para comentario
            view.attachEvents("contextmenu", $(this).find(view.selectores.sel_inputdiasSemana), abrirComentario);

            //Se habilita el botón de comentario al entrar en un input semanal
            view.attachEvents("focusin", $(this).find(view.selectores.sel_inputdiasSemana), cargarComentario);

            view.atacharEventosFilasTareasValoresCambiadosView($(this));

            view.marcarEventosAttachados($(this));

        });
    }

    //Finalización de tarea
    var finTareaRecur = function (event) {

        clickLinea(event);

        var srcobj = event.target ? event.target : event.srcElement;

        //Si el elemento no es el td se busca en la jerarquia superior
        //if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td')        

        var idTarea = parseInt($(srcobj).parent().parent().attr('data-t'));

        if ($(srcobj).parent().parent().attr('data-fin') == "0") {

            ObtenerTareaRecurso(idTarea);

        } else {
            view.desFinalizarTarea(idTarea);
        }

        hayCambios(event);

    }

    //Se habilita el botón de comentario
    var cargarComentario = function () {
        view.habilitarBtn(view.dom.btnComentario, 0);
        view.habilitarBtn(view.dom.btnComentario, 1, abrirComentario);
    }

    //Lógica del modal de comentario
    var abrirComentario = function () {

        var input = view.selectores.sel_preInput;
        var fila = $(input).parent().parent();
        var comentario = "";
        var bEstadoLectura = false;

        var sImputacion = $(fila).attr('data-imp');

        switch ($(fila).attr('data-estado'))//Estado
        {
            case "0"://Paralizada
                bEstadoLectura = true;
                break;
            case "1"://Activo
                break;
            case "2"://Pendiente
                bEstadoLectura = true;
                break;
            case "3"://Finalizada
                if (sImputacion == "0") bEstadoLectura = true;
                break;
            case "4"://Cerrada
                if (sImputacion == "0") bEstadoLectura = true;
                break;
        }

        //Si tiene comentario y no se ha traido anteriormente se trae de BBDD y se muestra en el modal
        if ($(input).hasClass('comentario') && !$(input)[0].hasAttribute('data-comentario')) {

            var idtarea = $(fila).attr('data-t');
            var fecha = $(input).attr('data-date').toDate();

            ObtenerComentario(idtarea, fecha, bEstadoLectura);

        }
        else {
            //Si tiene comentario cargado o modificado se pasa al modal para mostrarlo
            comentario = $(input).attr('data-comentario');

            view.abrirModalComentario(comentario, bEstadoLectura);
        }

    }

    //Pulsado del botón de aceptar del modal de comentario
    var aceptarComentario = function (event) {

        var input = view.selectores.sel_preInput;
        var comentarioAnterior = "";

        //Si el comentario está habilitado
        if (!view.dom.txtComentario.prop('disabled')) {

            //Si existe comentario anterior
            if (typeof $(input).attr('data-comentario') !== "undefined") comentarioAnterior = $(input).attr('data-comentario');

            //Si ha habido cambios en el comentario
            if (view.dom.txtComentario.val() != comentarioAnterior) {
                view.guardarComentario();
                hayCambios(event);
            }

        }
    }

    //Control de caracteres en el comentario(max 7500)
    var controlarCaracteresComentario = function () {
        var maxlong = 7500;

        if (view.dom.txtComentario.val().length > maxlong) {
            view.dom.txtComentario.val(view.dom.txtComentario.val().substring(0, maxlong));
            IB.bsalert.toast("La longitud máxima del comentario es de " + maxlong + " caracteres.", "danger");
        }
    }

    //Se actualizan los valores modificados en el detalle de tarea
    var tareaModificada = function (idTarea, finalizado, totalEst, fechaFinEst, hayIndicaciones) {

        //Si es true es que se han hecho cambios en la pantalla
        if (view.tareaModificada(idTarea, finalizado, totalEst, fechaFinEst, hayIndicaciones)) {
            actualizarVbles();
        };

    }

    /*Lógica de grabación*/

    //Validación de cambio en pulsado de tecla en inputs(mayús. tabulador etc. no deben generar cambio)
    var validarCambio = function (e) {

        var srcobj = e.target ? e.target : e.srcElement;

        e = (e) ? e : window.event;
        var charCode = (e.which) ? e.which : e.keyCode;

        if (!((charCode < 48 || charCode > 57) && (charCode < 96 || charCode > 105))) {
            hayCambios(e);
        }

    }

    //Identificación de cambios en la pantalla
    var hayCambios = function (event) {

        var srcobj = event.target ? event.target : event.srcElement;

        var cambioAutomatico = ($(srcobj).attr('data-autochange') == "1");

        if (cambioAutomatico) $(srcobj).attr('data-autochange', '0');

        if (bCambios == false && (!cambioAutomatico)) {

            bCambios = true;

            view.habilitarBtn(view.dom.btnGrabar, 1, grabar);
            view.habilitarBtn(view.dom.btnGrabarLite, 1, grabar);
            if (bSiguiente) view.habilitarBtn(view.dom.btnGrabarSig, 1, grabarSig);
            view.habilitarBtn(view.dom.btnGrabarRegresar, 1, grabarSalir);

        }

    }

    //Control de cambios en salida controlada
    var controlarCambios = function (e) {

        var defer = $.Deferred();

        if (bCambios) {

            IB.procesando.ocultar();

            $.when(IB.bsconfirm.confirm("warning", "Cambios sin guardar", "Existen cambios sin guardar. <br/> ¿Deseas guardarlos?")).then(function () {
                IB.procesando.mostrar();
                $.when(grabar(false)).then(function () {
                    defer.resolve();
                });
            }, function () {
                IB.procesando.mostrar();
                view.grabacionCorrecta();
                $.when(actualizarVbles()).then(defer.resolve());
            });

        } else {
            defer.resolve();
        }

        return defer.promise();
    }

    //Control de cambios en salida a Tarea (si dice de no, te mantiene en pantalla)
    var controlarSalir = function (e) {

        var defer = $.Deferred();

        if (bCambios) {

            IB.procesando.ocultar();

            //$.when(IB.bsconfirm.confirmCambios()).then(function () {
            $.when(IB.bsconfirm.confirm("warning", "Cambios sin guardar", "Para acceder al detalle de la tarea es necesario grabar los cambios realizados. <br/> ¿Deseas grabarlos?")).then(function () {
                $.when(grabar(false)).then(function () {
                    defer.resolve();
                });
            }, function () {
                defer.reject();
            });

        } else {
            defer.resolve();
        }

        return defer.promise();
    }

    var buscarLineas = function (e) {

        view.desmarcarLinea();

        if (e.keyCode == 13) {

            view.cerrarBuscador();

            var lineasEncontradas = view.lineas.buscarLineas(view.dom.txtSearch.val());

            if (lineasEncontradas.length != 0) {

                //Si hay más de una coincidencia se muestra el siguiente
                if (lineasEncontradas.length > 1) view.mostrarMas();

                view.marcarLinea(lineasEncontradas[0]);
                view.scrollearALinea(lineasEncontradas[0]);

            } else {
                view.ocultarMas();
                IB.bsalert.toast("Cadena no encontrada.", "info");
            }

        } else {
            view.ocultarMas();
        }

    }

    var buscarLineaSiguiente = function () {

        var lineasEncontradas = view.lineas.buscarLineas(view.dom.txtSearch.val());

        if (lineasEncontradas.length != 0) {

            //Si solo hay una coincidencia se oculta el siguiente
            if (lineasEncontradas.length == 1) view.ocultarMas();

            //Se coge la línea anterior y se desmarca
            var lineaAnterior = view.lineas.lineaActiva();
            view.desmarcarLinea();

            var i = 0;

            lineasEncontradas.each(function () {
                //Si se encuentra la línea se va a la siguiente y se sale del bucle
                if ($(this).attr('id') == lineaAnterior.attr('id')) {
                    i++;
                    return false
                }
                i++;
            });

            //Si se ha llegado a la última se vuelve a la primera
            if (i >= lineasEncontradas.length) i = 0;

            view.marcarLinea(lineasEncontradas[i]);
            view.scrollearALinea(lineasEncontradas[i]);

        } else {
            view.ocultarMas();
        }

    }

    //Grabación y navegación a la semana siguiente
    var grabarSig = function (e) {

        IB.procesando.mostrar();

        $.when(grabar(true)).then(function () {
            //setTimeout(function () { redirigirSemanaSiguiente(); }, 500);
            redirigirSemanaSiguiente();
        });

    }

    var grabarSalir = function (e) {

        IB.procesando.mostrar();

        $.when(grabar(false)).then(function () {
            setTimeout(function () { redirigirCalendario(); }, 500);
        });

    }

    var grabar = function (irSiguiente) {

        //grabar también puede llegar desde un evento atachado por attachevents por lo que 
        //irSiguiente sería un evento así que se pone el parámetro a false
        if (typeof irSiguiente.target != "undefined") irSiguiente = false;

        return obtenerFUltImputac(irSiguiente);

    }

    var validacionGrabar = function (fUltImputac, irSiguiente) {

        IB.procesando.mostrar();

        var dfr = $.Deferred();

        var error = false;
        var sErrores = new StringBuilder();
        var sPSN, sPT, sTarea;
        var finalizar, EAT, ETE, FFE, chkF, EP;
        var finalizables = [];

        //Validación de líneas modificadas con estimación obligatoria
        view.lineas.lineasEstModificadas().each(function () {

            //Se recogen las descripciones de PSN, PT y Tarea
            sPSN = view.obtenerDescripcion(view.lineas.lineaPSN($(this).attr('data-psn')));
            sPT = view.obtenerDescripcion(view.lineas.lineaPT($(this).attr('data-pt')));
            sTarea = view.obtenerDescripcion($(this));

            finalizar = $(this).find('td[headers="F"]').children();
            EAT = accounting.unformat($(this).find('td[headers="EAT"]').children().html(), ",")
            ETE = accounting.unformat($(this).find(view.selectores.sel_totalTarea).val(), ",")
            FFE = $(this).find(view.selectores.sel_FFE).val();
            EP = accounting.unformat($(this).find('td[headers="EP"]').children().html(), ",")

            if (EAT > 0) {

                if (finalizar.is(':checked')) {

                    $(this).find('td[headers="EP"]').children().html("");

                }
                else {

                    if (ETE == 0) {
                        sErrores.append("Debes introducir el esfuerzo total estimado de la siguiente tarea:</br>Proyecto Económico: " + sPSN + "</br>Proyecto Técnico: " + sPT + "</br>Tarea: " + sTarea + "</br></br>");
                        //IB.bsalert.fixedAlert("warning", "Error", "Debes introducir el esfuerzo total estimado de la siguiente tarea:</br>Proyecto Económico: " + sPSN + "</br>Proyecto Técnico: " + sPT + "</br>Tarea: " + sTarea + "");
                        error = true;
                        //return false;
                    }

                    if (FFE == "") {
                        sErrores.append("Debes introducir la fecha de fin estimada de la siguiente tarea:</br>Proyecto Económico: " + sPSN + "</br>Proyecto Técnico: " + sPT + "</br>Tarea: " + sTarea + "</br></br>");
                        //IB.bsalert.fixedAlert("warning", "Error", "Debes introducir la fecha de fin estimada de la siguiente tarea:</br>Proyecto Económico: " + sPSN + "</br>Proyecto Técnico: " + sPT + "</br>Tarea: " + sTarea + "");
                        error = true;
                        //return false;
                    }

                    if (EP < 0) {
                        sErrores.append("No puedes dejar un esfuerzo pendiente negativo en la siguiente tarea::</br>Proyecto Económico: " + sPSN + "</br>Proyecto Técnico: " + sPT + "</br>Tarea: " + sTarea + "</br></br>");
                        //IB.bsalert.fixedAlert("warning", "Error", "No puedes dejar un esfuerzo pendiente negativo en la siguiente tarea::</br>Proyecto Económico: " + sPSN + "</br>Proyecto Técnico: " + sPT + "</br>Tarea: " + sTarea + "");
                        error = true;
                        //return false;
                    }
                    if (EP == 0) {//Si la tarea es de estimación obligatoria, no está finalizada y el esfuerzo pendiente es 0 esta se podría finalizar y se guarda en un array para preguntar por ella más tarde
                        //finalizables[finalizables.length] = $(this);
                        finalizables.push($(this));
                    }

                }
            }
        });

        //if (error) return false;

        //Control huecos
        var imputacionesPosteriores = false;
        var diaImputable;
        var hueco = false;
        var sdiaHueco = "";
        var diaHueco;

        if (controlHuecos) {

            //Si el último día imputado es posterior al último día imputable de la semana es que existen imputaciones en semanas posteriores
            if (fUltImputac > fHasta) imputacionesPosteriores = true;

            $(view.selectores.sel_totales).each(function () {
                if ($(this).children().html() != "") { //Solo se validan los días pertenecientes a la semana/mes

                    //Se coge la cabecera del día correspondiente para acceder a sus propiedades
                    diaImputable = view.obtenerPropDia($(this).attr('headers'));

                    //Solo se puede considerar hueco los días laborables, no festivos, con horas por defecto diferentes de 0 y de fecha igual o posterior a la de alta del recurso
                    if (diaImputable.attr('data-laborable') == "1" && diaImputable.attr('data-festivo') != "1" && diaImputable.attr('data-horasjornada') != "0.00" && diaImputable.attr('data-date').toDate() >= fAlta) {

                        if ($(this).children().html() == "0,00") {

                            hueco = true;
                            diaHueco = diaImputable.attr('data-date').toDate();
                            sdiaHueco = diaImputable.children().attr('title');

                            if ($(this).attr('data-originalValue') != $(this).children().html()) { //No se permite dejar un día sin imputaciones cuando antes si las tenía

                                sErrores.append("No se permite dejar días laborables sin imputaciones, cuando antes sí las tenían.<br><br>");
                                sErrores.append("Si quiere eliminar la imputación de esfuerzo a un proyecto/tarea del día " + sdiaHueco);
                                sErrores.append(", deberá borrar dicha imputación y asignar los esfuerzos a otro proyecto/tarea antes de grabar ");
                                sErrores.append("los cambios.</br></br>");

                                //var strMensaje = "No se permite dejar días laborables sin imputaciones, cuando antes sí las tenían.<br><br>";
                                //strMensaje += "Si quiere eliminar la imputación de esfuerzo a un proyecto/tarea del día " + sdiaHueco
                                //strMensaje += ", deberá borrar dicha imputación y asignar los esfuerzos a otro proyecto/tarea antes de grabar ";
                                //strMensaje += "los cambios.";

                                //IB.bsalert.fixedAlert("warning", "Error", strMensaje);
                                error = true;
                                return false;
                            }

                            if (irSiguiente) { //Si se ha pulsado grabar e ir a la semana siguiente

                                sErrores.append("Denegado. Detectado hueco el " + sdiaHueco + ". No se puede acceder a la siguiente semana<br>si hay días sin imputar en la actual.</br></br>");
                                //IB.bsalert.fixedAlert("warning", "Error", "Denegado. Detectado hueco el " + sdiaHueco + ". No se puede acceder a la siguiente semana<br>si hay días sin imputar en la actual.");
                                error = true;
                                return false;

                            }

                            if (imputacionesPosteriores) { //No se permiten huecos en la semana cuando hay imputaciones en semanas posteriores

                                sErrores.append("Denegado. Detectado hueco el " + sdiaHueco + " debido a que existen imputaciones posteriores a la semana actual.</br></br>");
                                //IB.bsalert.fixedAlert("warning", "Error", "Denegado. Detectado hueco el " + sdiaHueco + " debido a que existen imputaciones posteriores a la semana actual.");
                                error = true;
                                return false;
                            }

                        }
                        else {

                            if (hueco) { //Si hay huecos en días anteriores al imputado

                                sErrores.append("Denegado. Detectado hueco el " + sdiaHueco + ".</br></br>");
                                //IB.bsalert.fixedAlert("warning", "Error", "Denegado. Detectado hueco el " + sdiaHueco + ".");
                                error = true;
                                return false;

                            }

                        }
                    //DOAIOSMI: Se añade esta validación para no permitir imputar un día no laborable sin haber imputado los días laborables previos
                    } else if (diaImputable.attr('data-laborable') == "0" && $(this).children().html() != "0,00" && hueco) {
                        sErrores.append("Denegado. Detectado hueco el " + sdiaHueco + ".</br></br>");
                        error = true;
                        return false;
                    }

                }
            });

        }

        //if (error) return false;
        //Si existen se muestran todos los errores a la vez
        if (error) {
            IB.procesando.ocultar();
            IB.bsalert.fixedAlert("warning", "Error de validación", sErrores.toString());
            return dfr.reject();
        }

        //Grabación de filas        
        var consumos = [], txtComentariosSinEsfuerzo = [], inputComentariosSinEsfuerzo = []; //Los consumos de tipo I se corresponden con imputaciones diarias y los de tipo E con estimaciones a nivel de tarea
        var imputarDiaVacaProyNoVaca = false;
        var ultimaFecha;

        view.lineas.lineasModificadas().each(function () {

            ultimaFecha = "";

            //Se recorren los inputs de imputación diaria que estén habilitados
            $(this).find(view.selectores.sel_inputdiasSemana + ':enabled').each(function () {

                var valorOriginal = accounting.unformat($(this).attr('data-originalvalue'), ",");
                var valor = accounting.unformat($(this).attr('value'), ",");

                //Si el valor es 0 y tiene comentario, este se vacía
                if (valor == 0 && $(this).hasClass('comentario')) {
                    //$(this).attr('aria-label')
                    txtComentariosSinEsfuerzo.push('<br>' + $(this).attr('aria-label'));
                    inputComentariosSinEsfuerzo.push($(this));
                    //view.borrarComentario($(this));
                }

                //Se comprueba si en días de vacaciones se está imputando a un proyecto distinto al de vacaciones
                if (($(this).val() != "") && $(this).hasClass('vacaciones') && (view.obtenerNaturaleza($(this).parent().parent().attr('data-psn')) != "27")) {
                    imputarDiaVacaProyNoVaca = true;
                }

                //Se comprueba si el input ha sido modificado para grabarlo
                //Tambien puede ocurrir que aunque no hayan cambiado las horas, haya que grabar porque se ha cambiado las horas por jornada para ese usuario en esa fecha
                if (($(this).attr('data-changed') == "1" && !(valor == 0 && valorOriginal == 0)) || $(this).attr('jorn-changed') == "1") {
                    var accion, comentario = "";

                    if (valorOriginal == 0) {
                        accion = "I";
                        comentario = typeof $(this).attr('data-comentario') === "undefined" ? "" : $(this).attr('data-comentario');
                    } else if (valor == 0) {
                        accion = "D";
                        comentario = typeof $(this).attr('data-comentario') === "undefined" ? "" : $(this).attr('data-comentario');
                    } else {
                        accion = "U";
                        comentario = typeof $(this).attr('data-comentario') === "undefined" ? "@" : $(this).attr('data-comentario');
                    }

                    var imputacion = {
                        tipo: "T",
                        accion: accion,
                        t332_idtarea: parseInt($(this).parent().parent().attr('data-t')),
                        t337_fecha: $(this).attr('data-date').toDate(),
                        t337_esfuerzo: parseFloat(valor),
                        t337_comentario: comentario,
                        t337_esfuerzoenjor: parseFloat(view.obtenerPropDia($(this).parent().attr('headers')).attr('data-horasJornada')) == 0 ? 1 : parseFloat(valor) / parseFloat(view.obtenerPropDia($(this).parent().attr('headers')).attr('data-horasJornada')),
                        idpt: parseInt($(this).parent().parent().attr('data-pt'))
                    };
                    consumos.push(imputacion);
                    ultimaFecha = $(this).attr('data-date');
                }

            });

            ETE = $(this).find(view.selectores.sel_totalTarea);
            FFE = $(this).find(view.selectores.sel_FFE);
            chkF = $(this).find(view.selectores.sel_checkFin);

            //Si la fecha fin estimada existe y es menor que la fecha de la última imputación de la semana, se actualiza FFE y se avisa al usuario.
            if (FFE.val() != "" && (moment(ultimaFecha.toDate()).diff(moment(FFE.attr('data-originalvalue').toDate())) > 0)) {
                FFE.val(ultimaFecha);
                view.actualizarFFE(FFE);
                sMensajes += '<br/>Se han actualizado fechas de fin de estimación.';
            }

            //Si la fecha de la última imputación de la tarea es posterior a la que contempla la fila se actualiza el atributo temporal(no se actualiza directamente por si debido a validaciones cambia)
            if (moment(ultimaFecha.toDate()).diff(moment($(this).attr('data-fultiimp').toDate())) > 0) {
                $(this).attr('data-fultiimp_temp', ultimaFecha);
            } else {
                $(this).attr('data-fultiimp_temp', "");
            }


            //Se comprueba si ETE, FFE o Finalizar tarea han cambiado                
            if ((ETE.attr('data-changed') == "1") || (FFE.attr('data-changed') == "1") || (chkF.attr('data-changed') == "1")) {
                var estimacion = {
                    tipo: "E",
                    accion: "U",
                    t332_idtarea: parseInt($(this).attr('data-t')),
                    ffe: FFE.val() == "" ? null : FFE.val().toDate(),
                    //ete: accounting.unformat(ETE.val(), ","),
                    ete: ETE.val() == "" ? null : accounting.unformat(ETE.val(), ","),
                    fin: chkF.is(':checked'),
                    finOrig: chkF.attr('data-originalvalue') == "0" ? false : true,
                    idpt: parseInt($(this).attr('data-pt')),
                    ffeOrig: FFE.attr('data-originalvalue') == "" ? null : FFE.attr('data-originalvalue').toDate(),
                    //eteOrig: accounting.unformat(ETE.attr('data-originalvalue'), ","),
                    eteOrig: ETE.attr('data-originalvalue') == "" ? null : accounting.unformat(ETE.attr('data-originalvalue'), ","),

                };
                consumos.push(estimacion);
            }


        });

        if (txtComentariosSinEsfuerzo.length == 0) {
            if (imputarDiaVacaProyNoVaca || (finalizables.length > 0)) {
                if (imputarDiaVacaProyNoVaca && !(finalizables.length > 0)) { //Si se ha imputado en días de vacaciones a proyectos que no son de vacaciones
                    IB.procesando.ocultar();
                    $.when(view.abrirModalConfirmacion()).then(function () {
                        IB.procesando.mostrar();
                        $.when(grabarConsumo(consumos)).then(function () {
                            return dfr.resolve();
                        });

                    }, function () {
                        return dfr.reject();
                    });
                }
                else {
                    if (!imputarDiaVacaProyNoVaca && (finalizables.length > 0)) {//Si hay tareas finalizables(EP=0 y no finalizadas)

                        $.when(preguntarFinalizar(finalizables, consumos, event)).then(function () {
                            return dfr.resolve();
                        });
                    }
                    else { //Si se ha imputado en días de vacaciones a proyectos que no son de vacaciones y hay tareas finalizables(EP=0 y no finalizadas)

                        IB.procesando.ocultar();
                        $.when(view.abrirModalConfirmacion()).then(function () {
                            $.when(preguntarFinalizar(finalizables, consumos, event)).then(function () {
                                return dfr.resolve();
                            });
                        }, function () {
                            return dfr.reject();
                        });

                    }
                }
            }
            else {
                $.when(grabarConsumo(consumos)).then(function () {
                    return dfr.resolve();
                });
            }
        } else { //Se han introducido comentarios en tareas sin esfuerzo
            IB.procesando.ocultar();
            $.when(IB.bsconfirm.confirm("primary", "", "Se han introducido comentarios en tareas sin esfuerzo en los siguientes días:<br>" + txtComentariosSinEsfuerzo.toString() + " <br><br>Pulsa \"Sí\" si deseas descartar estos comentarios y continuar con la grabación. En caso contrario, pulsa \"No\" e introduce un esfuerzo para las tareas indicadas.</br></br>")).then(function () {
                IB.procesando.mostrar();
                $.each(inputComentariosSinEsfuerzo, function () {
                    view.borrarComentario($(this));
                });
                return grabarConsumo(consumos);
            }, function () {
                return dfr.reject();
            });
        }

        return dfr.promise();
    }

    var preguntarFinalizar = function (finalizables, consumos, e) {

        var tareasFinalizables = new StringBuilder();
        for (var i = 0; i < finalizables.length; i++) {
            tareasFinalizables.append(view.obtenerDescripcion(finalizables[i]) + "</br>");
        }
        IB.procesando.ocultar();
        $.when(IB.bsconfirm.confirm("primary", "", "La o las siguientes tareas tienen un esfuerzo pendiente de 0 horas.<br><br>Pulsa \"Sí\" si deseas guardarlas como finalizadas. En caso contrario, pulsa \"No\" para guardarlas su estado actual.</br></br>" + tareasFinalizables.toString())).then(function () {
            //Poner las tareas como finalizadas en pantalla
            for (var i = 0; i < finalizables.length; i++) {
                var tarea = finalizables[i].attr('data-t');
                view.clickFinalizarTarea(tarea);
                $.each(consumos, function (val) {
                    if (this.t332_idtarea == tarea && this.tipo == "E") this.fin = true;
                });
            }
            IB.procesando.mostrar();
            return grabarConsumo(consumos);
        }, function () {
            IB.procesando.mostrar();
            return grabarConsumo(consumos);
        });
    }

    //se actualizan variables y el entity dias con los nuevos totales
    var actualizarVbles = function () {

        var defer = $.Deferred();

        bCambios = false;
        sMensajes = "";
        //IB.vars.fechaUltImp = sUltImputac;
        //fUltImputac = sUltImputac.toDate();

        actualizarTotales();

        return defer.resolve();

    }

    var actualizarTotales = function () {

        $(view.selectores.sel_totales).each(function () {
            switch ($(this).attr('headers')) {
                case "L":
                    dias.lunes.totalConsumo = accounting.unformat($(this).children().html(), ",");
                    break;
                case "M":
                    dias.martes.totalConsumo = accounting.unformat($(this).children().html(), ",");
                    break;
                case "X":
                    dias.miercoles.totalConsumo = accounting.unformat($(this).children().html(), ",");
                    break;
                case "J":
                    dias.jueves.totalConsumo = accounting.unformat($(this).children().html(), ",");
                    break;
                case "V":
                    dias.viernes.totalConsumo = accounting.unformat($(this).children().html(), ",");
                    break;
                case "S":
                    dias.sabado.totalConsumo = accounting.unformat($(this).children().html(), ",");
                    break;
                case "D":
                    dias.domingo.totalConsumo = accounting.unformat($(this).children().html(), ",");
                    break;
            }

        });
    }

    var grabacionCorrecta = function () {

        var defer = $.Deferred();

        IB.bsalert.toast("Grabación correcta." + sMensajes, "success");

        view.grabacionCorrecta();
        $.when(actualizarVbles()).then(defer.resolve());
        controlSemAntSig();

        return defer.promise();

    }

    /*Llamadas de carga a webmethods*/

    //Obtiene los datos de carga de la pantalla
    var obtenerDatosIAP = function (mismoMes) {
        IB.procesando.mostrar();

        var defer = $.Deferred();

        var payload = { dDesde: fDesde, dHasta: fHasta };
        IB.DAL.post(null, "obtenerHorasDefecto", payload, null,
            function (data) {
                obtenerHorasDefecto(data);
                var a = ObtenerConsumosIAPSemanaPSN(mismoMes);
                var b = ObtenerPSNTotales();
                
                $.when(a, b).then(function (data1, data2) {

                    view.pintarCabeceraPie(data2, dias);
                    if (mismoMes) {
                        view.cambiarValoresTabla(data1, dias);
                        atacharEventosFilasValoresCambiados();
                    } else {
                        view.pintarTablaPSN(data1, dias);
                    }                    
                    controlSemAntSig();
                    var c = otrosConsumos();

                    $.when(c).then(
                        defer.resolve()
                        );
                });

            }
        );

        return defer.promise();

    }

    //Obtiene los consumos totales de la semana
    var ObtenerPSNTotales = function () {
        var defer = $.Deferred();

        var payload = { dDesde: fDesde, dHasta: fHasta };
        IB.DAL.post(null, "ObtenerConsumosTotalesSemanaIAP", payload, null,
            function (data) {
                defer.resolve(data);
            }, null, 120000
        );

        return defer.promise();

    }

    //Obtiene los proyectos económicos en los que puede imputar el usuario conectado
    var ObtenerConsumosIAPSemanaPSN = function (mismoMes) {
        //IB.procesando.mostrar();

        var defer = $.Deferred();        
        var payload;
        var webmethod;

        //Si se navega a una semana de un mismo mes se pasan los PSNs cargados y expandidos anteriormente
        if (mismoMes) {
            var lPSN = [];

            view.lineas.PECargadosExpandidos().each(function () {
                lPSN.push(parseInt($(this).attr('data-psn')));
            });

            payload = { lPSN: lPSN, dDesde: fDesde, dHasta: fHasta };
            webmethod = "ObtenerConsumosIAPSemanaCompleto";
        } else {
            payload = { dDesde: fDesde, dHasta: fHasta };
            webmethod = "ObtenerConsumosIAPSemanaPSN";
        }
        
        var inicio = Date.now();
        IB.DAL.post(null, webmethod, payload, null,
            function (data) {
                $('#ContadorServ').html(Date.now() - inicio + ' ms.');
                defer.resolve(data);
            }, null, 100000
        );

        return defer.promise();

    }

    //Obtiene todos los Proyectos Técnicos de un proyectosubnodo
    var ObtenerConsumosIAPSemanaPSN_D = function (lPSN, soloPrimerNivel, procesando) {
        if (procesando) IB.procesando.mostrar();

        var defer = $.Deferred();

        var payload = { lPSN: lPSN, dDesde: fDesde, dHasta: fHasta, soloPrimerNivel: soloPrimerNivel };
        var inicio = Date.now();
        IB.DAL.post(null, "ObtenerConsumosIAPSemanaPSN_D", payload, null,
            function (data) {
                $('#ContadorServ').html(Date.now() - inicio + ' ms.');
                defer.resolve(data);
                if (procesando) IB.procesando.ocultar();
            }, null, 100000
        );

        return defer.promise();

    }

    //Obtiene el desglose de un Proyecto  Técnico
    var ObtenerConsumosIAPSemanaPT_D = function (nPT, soloPrimerNivel, procesando) {

        if (procesando) IB.procesando.mostrar();

        var defer = $.Deferred();

        var payload = { nPT: nPT, dDesde: fDesde, dHasta: fHasta, soloPrimerNivel: soloPrimerNivel };
        var inicio = Date.now();
        IB.DAL.post(null, "ObtenerConsumosIAPSemanaPT_D", payload, null,
            function (data) {
                $('#ContadorServ').html(Date.now() - inicio + ' ms.');
                defer.resolve(data);
                if (procesando) IB.procesando.ocultar();
            }, null, 100000
        );

        return defer.promise();

    }

    //Obtiene el desglose de una fase
    var ObtenerConsumosIAPSemanaF = function (nFase, soloPrimerNivel, procesando) {

        if (procesando) IB.procesando.mostrar();

        var defer = $.Deferred();

        var payload = { nFase: nFase, dDesde: fDesde, dHasta: fHasta, soloPrimerNivel: soloPrimerNivel };
        var inicio = Date.now();
        IB.DAL.post(null, "ObtenerConsumosIAPSemanaF", payload, null,
            function (data) {
                $('#ContadorServ').html(Date.now() - inicio + ' ms.');
                defer.resolve(data);
                if (procesando) IB.procesando.ocultar();
            }, null, 100000
        );

        return defer.promise();

    }

    //Obtiene el desglose de una actividad
    var ObtenerConsumosIAPSemanaA = function (nActividad, procesando) {

        if (procesando) IB.procesando.mostrar();

        var defer = $.Deferred();

        var payload = { nActividad: nActividad, dDesde: fDesde, dHasta: fHasta};
        var inicio = Date.now();
        IB.DAL.post(null, "ObtenerConsumosIAPSemanaA", payload, null,
            function (data) {
                $('#ContadorServ').html(Date.now() - inicio + ' ms.');
                defer.resolve(data);
                if (procesando) IB.procesando.ocultar();
            }, null, 100000
        );

        return defer.promise();

    }

    //Obtiene la estructura del elemento abierto mediante el botón de bomba en los que puede imputar esfuerzos un usuario en IAP, en una semana.
    var ObtenerConsumosIAPSemanaDesglose = function (nPSN, nPT, nFase, nActividad) {

        var defer = $.Deferred();

        var payload = { nPSN: nPSN, nPT: nPT, nFase: nFase, nActividad: nActividad, dDesde: fDesde, dHasta: fHasta, dLunes: dias.lunes.fechaSemana, dMartes: dias.martes.fechaSemana, dMiercoles: dias.miercoles.fechaSemana, dJueves: dias.jueves.fechaSemana, dViernes: dias.viernes.fechaSemana, dSabado: dias.sabado.fechaSemana, dDomingo: dias.domingo.fechaSemana };
        var inicio = Date.now();
        IB.DAL.post(null, "ObtenerConsumosIAPSemanaDesglose", payload, null,
            function (data) {
                $('#ContadorServ').html(Date.now() - inicio + ' ms.');
                defer.resolve(data);
            }, null, 500000
        );

        return defer.promise();

    }

    //Obtiene los datos generales de una relación tarea/recurso, correspondientes a la tabla t336_TAREAPSPRECURSO
    var ObtenerTareaRecurso = function (idTarea) {

        var defer = $.Deferred();

        var payload = { idTarea: idTarea };
        IB.DAL.post(null, "ObtenerTareaRecurso", payload, null,
            function (data) {
                view.finalizarTarea(data, idTarea);
                defer.resolve();
            }, null, 120000
        );

        return defer.promise();

    }

    //Si existe comentario para una tarea de un usuario en una fecha lo obtiene
    var ObtenerComentario = function (idtarea, fecha, bEstadoLectura) {

        var defer = $.Deferred();
        var comentario = "";

        var payload = { idtarea: idtarea, fecha: fecha };
        IB.DAL.post(null, "ObtenerComentario", payload, null,
            function (data) {

                //Si existe comentario se muestra
                if (data != null) comentario = data.t337_comentario;

                view.abrirModalComentario(comentario, bEstadoLectura);
                defer.resolve();
            }, null, 120000
        );

        return defer.promise();

    }

    //Obtiene la fecha de última imputación del recurso
    var obtenerFUltImputac = function (irSiguiente) {

        var defer = $.Deferred();

        var payload = {};
        IB.DAL.post(null, "obtenerFUltImputac", payload, null,
            function (data) {
                $.when(validacionGrabar(data.toDate(), irSiguiente)).then(function () {
                    defer.resolve();
                }).fail(function () {
                    sMensajes = "";
                    defer.reject();
                });
            }, null, 120000
        );

        return defer.promise();

    }

    /*Llamadas de grabación a webmethods*/

    //Grabación de consumos
    var grabarConsumo = function (consumos) {

        var defer = $.Deferred();

        var payload = { consumos: consumos };
        IB.DAL.post(null, "grabarConsumo", payload, null,
            function (data) {
                IB.procesando.ocultar();
                $.when(grabacionCorrecta()).then(function () {
                    defer.resolve();
                });
            }, null, 120000
        );

        return defer.promise();

    }

    return {
        init: init
    }

})(SUPER.IAP30.ImpDiaria.View, SUPER.IAP30.ImpDiaria.Models.Dia, SUPER.IAP30.ImpDiaria.appTarea);