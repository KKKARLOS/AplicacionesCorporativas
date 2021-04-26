$(document).ready(function () { SUPER.IAP30.GASVI.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.GASVI = SUPER.IAP30.GASVI || {}

SUPER.IAP30.GASVI.app = (function (view) {

    //Booleano indicador de cambios
    var bCambios = 0;
    window.onbeforeunload = function () {
        if (bCambios) return "Si continúas perderás los cambios que no has guardado.";
    }

    /*Lógica de carga y visualización de datos*/

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
        if (IB.vars.nPSN != "") IdProyectoSubnodo = IB.vars.nPSN;
        /********************** Inicialización de los componentes que se cargan a traves de los plugins *********************************************/

        var options = {
            titulo: "::: SUPER ::: - Selección de proyecto",
            modulo: "IAP30",
            autoSearch: true,
            autoShow: false,
            searchParams: {
                /*tipoBusqueda: "proyectosEconomicos",
                fechaInicio: (view.obtenerValor(view.dom.txtFIni) == "") ? null : view.obtenerValor(view.dom.txtFIni),
                fechaFin: (view.obtenerValor(view.dom.txtFFin) == "") ? null : view.obtenerValor(view.dom.txtFFin)*/
            },
            onSeleccionar: function (data) {
                darValor(view.dom.txtPro, data.filaSeleccionada.t301_idproyecto + ' - ' + data.filaSeleccionada.t301_denominacion);
                darValor(view.dom.IdProyectoSubnodo, data.filaSeleccionada.t305_idproyectosubnodo);
            },
            onCancelar: function () {
                //console.log("cancelar");
                //view.pintarDatosTarea(objTarea);
            }
        };

        view.dom.ayudaProyecto.buscaproyecto(options);

        view.init();

        IB.procesando.mostrar();
        //Creación de variables globales
        IB.vars.bCambios = false;
        IB.vars.prevFocus = null;
        IB.vars.returnElement = null;

        $(document).on('hidden.bs.modal', '.modal', function () {
            view.dom.ocultable.attr('aria-hidden', 'false');
        });

        //Asignación de botones a elementos estáticos
        //view.attachEvents("change", view.dom.txtDesde, consultar);
        //view.attachEvents("change", view.dom.txtHasta, consultar);
        //view.attachEvents("change", view.dom.buscador, consultar);

        //Evento de cambios en los campos
        //view.attachLiveEvents("change", view.selectores.sel_inputs, hayCambios);

        //Tramitar la nota de gasto
        view.attachEvents("click", view.dom.btnTramitar, tramitar);
        //Salir de la pantalla 
        view.attachEvents("click", view.dom.btnVolver, salir);
        //Enlace a buscador de proyectos
        view.attachEvents("click", view.dom.linkProy, buscarProyecto);
        //Boton exportación a excel
        view.attachEvents("click", view.dom.btnCalc, calculadora);
        //Botón Anotaciones personales
        view.attachEvents("click", view.dom.btnAnot, anotacion);// pruebaComentario

        //Acciones a realizar al coger el foco las fechas (inicializar)
        //view.attachLiveEvents('focus', view.selectores.sel_Fecha, abrir);

        //view.attachEvents("click", view.dom.lblProy, proyecto);
        view.attachEvents("click", view.dom.btnLlevarValor, getValorCalculadora);
        //Para cambiar los iconos al hacer collapse 
        view.attachEvents("click", view.dom.iconosColapsables, view.alternarClaseIconos);
        //Para calcular el total del gasto pagado por la empresa 
        view.attachEvents("change", view.selectores.sel_Gpe, setGastoPagado);
        //Calculo del total cuando cambian los anticipos
        view.attachEvents("change", view.selectores.sel_Anticipo, ponerTotales);
        //Damos funcionalidad al enter en el modal de calculadora con el input central enfocado
        view.attachEvents("keyup", view.selectores.idCalculadoratxtResultado, evaluar);
        //cambio de icono de FA
        view.attachEvents("change", view.dom.ficheroAdjunto, iconoFichero);
        //Una vez mostrado el modal de calculadora enviamos el foco al input de resultado
        view.attachEvents("shown.bs.modal", view.dom.calculadora, ponerFocoInput);
        //Una vez mostrado el modal de comentario enviamos el foco al input siguiente en la linea (la primera dieta)
        view.attachEvents("hidden.bs.modal", view.dom.comentarioGasto, ponerFocoDieta);

        //Para seleccionar un desplazamiento o cancelar

        view.attachEvents("click", view.dom.btnSeleccionarDesplazamiento, pulsarBtnSeleccionarDesplazamiento);
        view.attachEvents("click", view.dom.btnCancelarDesplazamiento, cancelarDesplazamiento);

        view.attachEvents("dblclick", view.dom.lineaTablaProy, getProyecto);

        view.attachEvents("click", view.dom.NFilaGasto, ponerLineaGasto);
        view.attachEvents("click", view.dom.EFilaGasto, quitarLineaGasto);
        view.attachEvents("click", view.dom.DFilaGasto, duplicarLineaGasto);

        //Boton mostrar distancias kilométricas
        view.attachEvents("click", view.dom.btnDistancias, mostrarDistancias);

        //Boton anotaciones personales 
        view.attachEvents("click", view.dom.btnAceptarAnot, setAnotacion);
        view.attachEvents("click", view.dom.btnCancelarAnot, cancelarAnotacion);
        view.attachEvents("click", view.dom.closeAnota, cancelarAnotacion);

        //Boton comentario a la fila de gasto 
        view.attachEvents("click", view.dom.btnAceptarComent, setComentario);

        //Unos pocos usuario pertenecen a mas de una empresa, con lo que pueden seleccionarla de un combo
        view.attachEvents("change", view.dom.cboEmpresa, setEmpresa);

        view.attachEvents("change", view.dom.cboMoneda, setMoneda);


        //Para marcar/desmarcar filas del catálogo de gastos
        view.attachLiveEvents("click keypress", view.selectores.filasGasto, clickLinea);
        //Para calcular los totales de la fila cuando cambian las dietas
        view.attachLiveEvents("change", view.selectores.sel_Dieta, calcularDietas);
        //Para establecer el campo que tenía el foco antes de ir a la calculadora 
        view.attachLiveEvents("focus", view.selectores.sel_Justi, setCampoCalculadora);
        //Para calcular los totales de la fila cuando cambian los gastos justificados
        view.attachLiveEvents("change", view.selectores.sel_Justi, calcularJustificados);
        //Para formatear como numericos los campos al salir
        view.attachLiveEvents("change", view.selectores.sel_Anticipo, formatFloats);
        view.attachLiveEvents("change", view.selectores.sel_Gpe, formatFloats);

        //Para calcular los el importe de los KM
        view.attachLiveEvents("change", view.selectores.sel_Km, calcularKm);
        //Click en las imagenes del cochecito de ECO
        view.attachLiveEvents("click", view.selectores.sel_ImgECO, getECO2);
        //Click en la imagen del comentario de cada fila de gasto
        view.attachLiveEvents("click", view.selectores.sel_Coment, mostrarComentario);
        //Mostrar la capa modal pulsando INTRO
        view.attachLiveEvents("keypress", view.selectores.sel_LinkComent, mostrarComentarioTecla);
        //Mostrar la capa modal pulsando INTRO
        view.attachLiveEvents("keypress", view.selectores.sel_LinkECO, getECOTecla);
        //Control de cambios
        view.attachLiveEvents("keypress", view.selectores.sel_inputs, hayCambios);

        //Se comprueba si el profesional tiene permisos de reconexión y si es así, se atacha el evento correspondiente
        //if (IB.vars.nReconectar == 1) {
        //    view.attachEvents("click", view.dom.icoProfesional, abrirBuscadorProfesional);
        //    view.attachEvents("click", view.dom.btnUser, abrirBuscadorProfesional);
        //}
        //else {//En caso de que no pueda reconectarse se deshabilita la opción
        //    view.deshabilitarReconexion();
        //}


        /*Comprobamos si cuando se reduce el tamaño la calculadora está visualizandose para ocultarla*/
        $(window).resize(function () {
            var width = $(window).width();
            if (width < 992 && (view.dom.calculadora.data('bs.modal') || {}).isShown) {
                view.dom.calculadora.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                view.dom.calculadora.modal('hide');
            }
        });

        //consultar();
        //obtenerMotivos();
        $.when(obtenerMonedas()).then(function () {
            view.visualizarContenedor();
        });
        //Como es una nota nueva nunca va a tener Historial
        //obtenerHistorial();

        //Pongo una línea en el catálogo de gastos
        ponerLineaGasto();

        //Prueba selector fechas
        //$(view.selectores.sel_Fecha).val('01/09/2016')

        setEstadoJustificante(0);
    }

    var tramitar = function () {
        IB.procesando.mostrar();
        var sDesde = "", sHasta = "";
        if (comprobarDatosTramitar()) {
            var cabecera = {
                t431_idestado: "T",
                t420_concepto: view.dom.txtConcepto.val(),
                t001_idficepi_solicitante: parseInt(IB.vars.idficepi),
                t314_idusuario_interesado: parseInt(IB.vars.idInteresado),
                //t423_idmotivo: parseInt(view.dom.cboMotivo.val()),
                t423_idmotivo: parseInt(IB.vars.idMotivo),
                t420_justificantes: ($(view.selectores.rdbJustificantes).val() == '1') ? true : false,
                t305_idproyectosubnodo: parseInt(view.dom.IdProyectoSubnodo.val()),
                t422_idmoneda: IB.vars.idMoneda,
                t420_comentarionota: view.dom.observaciones.text(),
                t420_anotaciones: view.dom.txtDesAnotaciones.text(),                
                t420_importeanticipo: accounting.unformat(view.dom.txtImpAnticipo.val(), ","),
                t420_fanticipo: view.dom.fecAnticipo.val().toDate(),
                t420_lugaranticipo: view.dom.lugarAnticipo.val(),
                t420_importedevolucion: accounting.unformat(view.dom.txtImpDevolucion.val(), ","),
                t420_fdevolucion: view.dom.FDevolucion.val().toDate(),
                t420_lugardevolucion: view.dom.OfiDevolucion.val(),
                t420_aclaracionesanticipo: view.dom.txtAclaraciones.text(),
                t420_pagadotransport: accounting.unformat(view.dom.txtPagadoTransporte.val(), ","),
                t420_pagadohotel: accounting.unformat(view.dom.txtPagadoHotel.val(), ","),
                t420_pagadootros: accounting.unformat(view.dom.txtPagadoOtros.val(), ","),
                t420_aclaracionepagado: view.dom.txtAclaracionesPagados.text(),
                t313_idempresa: parseInt(IB.vars.sIdEmpresa),
                t007_idterrfis: parseInt(IB.vars.sIdTerritorio),
                t420_impdico: accounting.unformat(IB.vars.cldDCCO, ","),
                t420_impmdco: accounting.unformat(IB.vars.cldMDCO, ","),
                t420_impalco: accounting.unformat(IB.vars.cldDACO, ","),
                t420_impkmco: accounting.unformat(IB.vars.cldKMCO, ","),
                t420_impdeco: accounting.unformat(IB.vars.cldDECO, ","),
                t420_impdiex: accounting.unformat(IB.vars.cldDCEX, ","),
                t420_impmdex: accounting.unformat(IB.vars.cldMDEX, ","),
                t420_impalex: accounting.unformat(IB.vars.cldDAEX, ","),
                t420_impkmex: accounting.unformat(IB.vars.cldKMEX, ","),
                t420_impdeex: accounting.unformat(IB.vars.cldDEEX, ","),
                t010_idoficina: parseInt(IB.vars.sIdOficinaLiq),
                t420_idreferencia_lote: null,
                autoResponsable: (IB.vars.bAutorresponsable == '1') ? true : false
            };


            var aLineas = [];
            $(view.dom.tablaGastos).find('tbody > tr').each(function () {
                var sComentario = "";
                var codECO = null;
                if ($(this).attr('comentario')) sComentario = $(this).attr('comentario');
                if ($(this).attr('eco')) codECO = getInt($(this).attr('eco'));

                sDesde = $(this).find(view.selectores.sel_Fecha).eq(0).val();
                if (!sDesde) sDesde = $(this).find('td').eq(0).text();
                sHasta = $(this).find(view.selectores.sel_Fecha).eq(1).val();
                if (!sHasta) sHasta = $(this).find('td').eq(1).text();

                var linea = {
                    idCabecera: null,
                    idLinea: null,
                    desde: sDesde.toDate(),
                    hasta: sHasta.toDate(),
                    destino: $(this).find(view.selectores.sel_Dest).eq(0).val(),
                    anotacion: sComentario,
                    dietaCompleta: getInt($(this).find(view.selectores.sel_Dieta).eq(0).val()),
                    mediaDieta: getInt($(this).find(view.selectores.sel_Dieta).eq(1).val()),
                    dietaEspecial: getInt($(this).find(view.selectores.sel_Dieta).eq(2).val()),
                    dietaAlojamiento: getInt($(this).find(view.selectores.sel_Dieta).eq(3).val()),
                    numKm: getInt($(this).find(view.selectores.sel_Km).eq(0).val()),
                    idECO: codECO,
                    peaje: accounting.unformat($(this).find(view.selectores.sel_Justi).eq(0).val(), ","),
                    comida: accounting.unformat($(this).find(view.selectores.sel_Justi).eq(1).val(), ","),
                    transporte: accounting.unformat($(this).find(view.selectores.sel_Justi).eq(2).val(), ","),
                    hotel: accounting.unformat($(this).find(view.selectores.sel_Justi).eq(3).val(), ",")
                };
                aLineas.push(linea);
            });

            grabar(cabecera, aLineas);
        }
    }
    var grabar = function (cabecera, lineas) {

        var payload = { cabecera: cabecera, lineas: lineas };
        IB.DAL.post(null, "grabar", payload, null,
            function (data) {
                IB.procesando.ocultar();
                grabacionCorrecta();
                imprimir(data);
            }
        );
    }
    var grabacionCorrecta = function () {
        IB.bsalert.toast("Grabación correcta.", "info");
        desactivarGrabar();
    }
    var comprobarDatosTramitar = function () {
        var sErrorT = "", sError = "", sDesde = "", sHasta = "", sDestino = "";
        var targetError = "";
        var nTotalLinea = 0, nDietaAlojamiento = 0, nTotalDietas = 0, nKm = 0, nTotalDietasNota = 0, nAuxDieta = 0, nTotalDietasAlojamientoNota = 0;
        //Array donde acumulo los intervalos de fechas para saber cuantos días comprende la nota
        var js_Dias = new Array();

        IB.procesando.mostrar();
        //Compruebo datos de la cabecera
        if (view.dom.txtConcepto.val() == "") {
            sErrorT = "El concepto es un dato obligatorio";
            targetError = view.dom.txtConcepto;
        }
        if (sError == "" && sErrorT == "") {
            //if (view.dom.IdProyectoSubnodo.val() == "" && view.dom.cboMotivo.val() == "1")
            if (view.dom.IdProyectoSubnodo.val() == "" && IB.vars.idMotivo == "1") {
                sErrorT = "El proyecto es un dato obligatorio";
                targetError = view.dom.txtPro;
            }
        }
        if (sError == "" && sErrorT == "") {
            var sOpcion = $(view.selectores.rdbJustificantes).val();
            if (sOpcion != "0" && sOpcion != "1") {
                sErrorT = "Debes indicar si existen justificantes";
                targetError = view.dom.ficheroAdjunto;
            }
        }
        if (sError == "" && sErrorT == "") {
            if (accounting.unformat(view.dom.txtGSTOTAL.text(), ",") == 0) {
                sErrorT = "No se permiten tramitar solicitudes de liquidación de importe cero.";
                targetError = $(view.selectores.filaActivaError);
            }
        }
        if (sError == "" && sErrorT == "") {
            //Compruebo datos de las lineas
            $(view.dom.tablaGastos).find('tbody > tr').each(function () {
                if (sError == "" && sErrorT == "") {
                    sDesde = $(this).find(view.selectores.sel_Fecha).eq(0).val();
                    if (!sDesde) sDesde = $(this).find('td').eq(0).text();
                    sHasta = $(this).find(view.selectores.sel_Fecha).eq(1).val();
                    if (!sHasta) sHasta = $(this).find('td').eq(1).text();

                    sDestino = $(this).find(view.selectores.sel_Dest).eq(0).val();
                    if (sDesde != "" && sHasta != "") {
                        var fFechaDesde = sDesde.toDate();
                        var fFechaHasta = sHasta.toDate();
                        var iDif = moment(fFechaHasta).diff(moment(fFechaDesde), 'days');
                        if (iDif < 0) {
                            sErrorT = "Las fechas no son congruentes (" + sDesde + " - " + sHasta + ")";
                            targetError = $(this).find(view.selectores.primeraFecha);
                        }                            
                    }
                    if (sError == "" && sErrorT == "") {
                        if (sDesde != "" && sHasta != "") {
                            var oFechaDesde = new Date(fFechaDesde);
                            var oFechaHasta = new Date(fFechaHasta);
                            do {
                                //if (js_Dias.isInArray(oFechaDesde.ToShortDateString()) == null)
                                //    js_Dias[js_Dias.length] = oFechaDesde.ToShortDateString();
                                if ($.inArray(oFechaDesde.toString(), js_Dias) == -1) {
                                    //js_Dias[js_Dias.length] = sDesde;
                                    js_Dias.push(oFechaDesde.toString());
                                }

                                //oFechaDesde = oFechaDesde.add("d", 1);
                                oFechaDesde.setDate(oFechaDesde.getDate() + 1)
                            } while (oFechaDesde <= oFechaHasta);
                        }

                        nTotalLinea = accounting.unformat($(this).find(view.selectores.sel_Total).eq(0).val(), ",");
                        nDietaAlojamiento = 0;
                        nTotalDietas = 0;
                        if (sDesde == "" || sHasta == "" || sDestino == "" || nTotalLinea == 0) {
                            sError = "Se han detectado filas que teniendo algún dato no cumplen con el mínimo exigido (fecha, destino y algún importe).";
                        }
                    }
                    if (sError == "" && sErrorT == "") {
                        $(this).find(view.selectores.sel_Dieta).each(function () {
                            nAuxDieta = getInt($(this).val())
                            nTotalDietasNota += nAuxDieta;
                            nTotalDietas += nAuxDieta;
                            if (nAuxDieta < 0) {
                                sErrorT = "No se permite indicar números negativos en las dietas";
                                targetError = $(this).find(view.selectores.peajes);
                            }                                
                        });
                    }
                    if (sError == "" && sErrorT == "") {
                        nKm = $(this).find(view.selectores.sel_Km).eq(0).val();
                        if (nKm < 0) {
                            sErrorT = "No se permite indicar un número negativo de kilómetros";
                            targetError = $(this).find(view.selectores.km);
                        }
                            
                    }
                    if (sError == "" && sErrorT == "") {
                        nDietaAlojamiento = getInt($(this).find(view.selectores.sel_Dieta).eq(3).val());
                        nTotalDietasAlojamientoNota += nDietaAlojamiento;
                        if ((nTotalDietas - nDietaAlojamiento) > moment(fFechaHasta).diff(moment(fFechaDesde), 'days') + 1) {
                            sError = "El número de dietas (completa, media, especial) no puede superar el número de días entre dos fechas.";
                            targetError = $(this).find(view.selectores.primeraDieta);
                        }                            
                    }
                    if (sError == "" && sErrorT == "") {
                        if (nDietaAlojamiento > moment(fFechaHasta).diff(moment(fFechaDesde), 'days') + 1) {
                            sError = "El número de dietas de alojamiento no puede superar el número de días entre dos fechas.";
                            targetError = $(this).find(view.selectores.alojamiento);
                        }
                            
                    }
                    if (sError == "" && sErrorT == "") {
                        if (IB.vars.idMoneda != "EUR") {
                            if (nTotalDietas > 0 || nKm > 0)
                                sError = "Las solicitudes con moneda diferente al Euro no permiten dietas ni kilometraje.";
                                targetError = $(this).find(view.selectores.primeraDieta);
                        }
                    }
                }
            });
            //Una vez comprobada cada linea individual, hago comprobaciones sobre los totales
            if (sError == "" && sErrorT == "") {
                if ((nTotalDietasNota - nTotalDietasAlojamientoNota) > js_Dias.length) {
                    sError = "El número total de dietas (completa, media, especial) no puede superar el número de días contemplados en la solicitud.";
                }
            }
            if (sError == "" && sErrorT == "") {
                if (nTotalDietasAlojamientoNota > js_Dias.length) {
                    sError = "El número total de dietas de alojamiento no puede superar el número de días contemplados en la solicitud.";
                }
            }
        }
        if (sErrorT != "") {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger(sErrorT);
            if (targetError != "") targetError.focus();
            return false;
        }
        if (sError != "") {
            IB.procesando.ocultar();
            $.when(IB.bsalert.fixedAlert("warning", "Error de validación", sError)).then(function () {
                if (targetError != "") targetError.focus();                
            });
            return false;
        }
        return true;
    }

    var setAnotacion = function (e) {
        //view.dom.hdnAnotacionesPersonales.val(view.dom.txtDesAnotaciones.val());
        if (view.dom.txtDesAnotaciones.val() != "") {
            view.dom.icoAnot.removeClass('fa-file-o').addClass('fa-file-text-o');
        }
        else {
            view.dom.icoAnot.removeClass('fa-file-text-o').addClass('fa-file-o');
        }
    }
    var cancelarAnotacion = function (e) {
        view.dom.txtDesAnotaciones.val(view.dom.hdnAnotacionesPersonales.val());
    }

    var mostrarComentarioTecla = function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            mostrarComentario(e);
        }
    }
    var getECOTecla = function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            var srcobj = event.target ? event.target : event.srcElement;
            if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td');
            if ($('img', srcobj).length > 0) {
                // image exist
                getECO($(srcobj).parent());
            }

        }
    }
    var mostrarComentario = function (e) {
        var srcobj = event.target ? event.target : event.srcElement;
        //Si el elemento no es el td se busca en la jerarquia superior
        if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('tr');
        //view.dom.txtDesComentario.val($(srcobj).attr('comentario'));
        var sComent = "";
        //if (view.dom.bodyGastos.find('tr.activa').attr('comentario'))
        //    sComent = view.dom.bodyGastos.find('tr.activa').attr('comentario');
        if ($(srcobj).attr('comentario'))
            sComent = $(srcobj).attr('comentario');
        view.dom.txtDesComentario.html(sComent);
        view.dom.comentarioGasto.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        view.dom.comentarioGasto.modal('show');
        view.dom.ocultable.attr('aria-hidden', 'true');
    }
    var setComentario = function (e) {
        var sComent = view.dom.txtDesComentario.val();
        var oFila = view.dom.bodyGastos.find(view.selectores.filaActiva);
        oFila.attr('comentario', sComent);
        oFila.find(view.selectores.sel_Coment).eq(0).attr("data-content", sComent);
        if (sComent != "") {
            oFila.find(view.selectores.sel_Coment).eq(0).removeClass('fa-comment-o').addClass('fa-commenting-o');
        }
        else {
            oFila.find(view.selectores.sel_Coment).eq(0).removeClass('fa-commenting-o').addClass('fa-comment-o');
        }
    }

    var ponerLineaGasto = function (e) {
        view.ponerLineaGasto();
        var oNewFila = view.dom.bodyGastos.find('tr:last');
        if (IB.vars.sFechaIAP != "") {
            //oNewFila.find('td').eq(0).text(IB.vars.sFechaIAP);
            //oNewFila.find('td').eq(1).text(IB.vars.sFechaIAP);
            oNewFila.find('td').eq(0).find('input').val(IB.vars.sFechaIAP);
            oNewFila.find('td').eq(1).find('input').val(IB.vars.sFechaIAP);
            IB.vars.sFechaIAP = "";
        }
        ponerCalendariosLinea(oNewFila.find('td').eq(0).find('input'));
        ponerCalendariosLinea(oNewFila.find('td').eq(1).find('input'));
        ponerPopover(oNewFila.find('td').eq(3).find('i'));
        //$(".txtFecha").attr('maxlength', 10);
        //$(".txtDieta").attr('maxlength', 3);
        //$(".txtKm").attr('maxlength', 4);
        //$(".txtJusti").attr('maxlength', 6);
        oNewFila.find('.txtDieta').attr('maxlength', 3);
        oNewFila.find('.txtKm').attr('maxlength', 4);
        oNewFila.find('.txtJusti').attr('maxlength', 6);
    }
    var ponerCalendariosLinea = function (oInput) {
        oInput.attr('maxlength', 10);
        oInput.datepicker({
            changeMonth: true,
            changeYear: true,

            beforeShow: function (input, inst) {
                $(this).removeClass('calendar-off').addClass('calendar-on');
            },
            onClose: function () {
                $(this).removeClass('calendar-on').addClass('calendar-off');
            }
        });
        //Validación de inputación manual en datepickers
        oInput.on('focusout', function (e) {
            if ($(this).val() != '') {
                var input = $(this);
                window.setTimeout(function () {
                    if ((input.val() != '') && (!moment(input.val(), 'DD/MM/YYYY', 'es', true).isValid())) {
                        IB.bsalert.toastdanger("Formato de fecha incorrecto: " + input.val());
                        input.val('');
                        input.focus();
                    }
                }, 100);
            }
        });
        oInput.on('change', function (e) {
            if ($(this).val() != '') {
                //if (!moment($(this).val(), 'DD/MM/YYYY', 'es', true).isValid()) {
                //    IB.bsalert.toastdanger("Formato de fecha incorrecto");
                //    $(this).focus();
                //}
                //Compruebo si las fechas son congruentes
                var oFila = $(this).parent().parent();
                var sDesde = oFila.find(view.selectores.sel_Fecha).eq(0).val();
                var sHasta = oFila.find(view.selectores.sel_Fecha).eq(1).val();
                if (sDesde != "" && sHasta != "") {
                    var iDif = moment(sHasta.toDate()).diff(moment(sDesde.toDate()), 'days');
                    if (iDif < 0)
                        IB.bsalert.toastdanger("Las fechas no son congruentes");
                }
            }
        });
    }

    var ponerPopover = function (oElem) {
        // $('[data-toggle="popover"]').popover({ trigger: "hover" });
        //oElem.popover({ trigger: "hover" });
        if (!view.indicadores.i_dispositivoTactil) {
            oElem.popover({ trigger: "hover" });
        }

    }
    var quitarLineaGasto = function (e) {
        if ($(view.selectores.filaActiva).length == 0) {
            IB.bsalert.toastdanger("Debes seleccionar la fila a eliminar.");
            return false;
        }
        view.dom.bodyGastos.find(view.selectores.filaActiva).remove();
        calcularPie();
    }
    var duplicarLineaGasto = function (e) {

        if ($(view.selectores.filaActiva).length == 0) {
            IB.bsalert.toastdanger("Debes seleccionar la fila a duplicar.");
            return false;
        }
        //No puedo clonar la fila porque el clonado de Datepickers no funciona bien
        //view.dom.bodyGastos.find(view.selectores.filaActiva).clone(true).insertAfter(view.dom.bodyGastos.find('tr:last'));

        var oFilaOrigen = view.dom.bodyGastos.find(view.selectores.filaActiva);
        //Dietas completas.
        var dc = oFilaOrigen.find(view.selectores.sel_Dieta).eq(0).val();
        //Media Dietas
        var md = oFilaOrigen.find(view.selectores.sel_Dieta).eq(1).val();
        //Dieta especial
        var de = oFilaOrigen.find(view.selectores.sel_Dieta).eq(2).val();
        //Dieta alojamiento
        var da = oFilaOrigen.find(view.selectores.sel_Dieta).eq(3).val();
        //Total de dietas 
        var td = oFilaOrigen.find(view.selectores.sel_ImpDieta).eq(0).val();

        var peaje = oFilaOrigen.find(view.selectores.sel_Justi).eq(0).val();
        var comida = oFilaOrigen.find(view.selectores.sel_Justi).eq(1).val();
        var transporte = oFilaOrigen.find(view.selectores.sel_Justi).eq(2).val();
        var hotel = oFilaOrigen.find(view.selectores.sel_Justi).eq(3).val();

        ponerLineaGasto();
        var oFilaDestino = view.dom.bodyGastos.find('tr:last');
        //Copio los valores de la fila origen a la actual
        oFilaDestino.find(view.selectores.sel_Dest).eq(0).val(oFilaOrigen.find(view.selectores.sel_Dest).eq(0).val());

        oFilaDestino.find(view.selectores.sel_Dieta).eq(0).val(dc);
        oFilaDestino.find(view.selectores.sel_Dieta).eq(1).val(md);
        oFilaDestino.find(view.selectores.sel_Dieta).eq(2).val(de);
        oFilaDestino.find(view.selectores.sel_Dieta).eq(3).val(da);
        oFilaDestino.find(view.selectores.sel_ImpDieta).eq(0).val(td);

        oFilaDestino.find(view.selectores.sel_Km).eq(0).val(oFilaOrigen.find(view.selectores.sel_Km).eq(0).val());
        oFilaDestino.find(view.selectores.sel_ImpKm).eq(0).val(oFilaOrigen.find(view.selectores.sel_ImpKm).eq(0).val());
        oFilaDestino.find(view.selectores.sel_CabEco).html(oFilaOrigen.find(view.selectores.sel_CabEco).html());

        oFilaDestino.find(view.selectores.sel_Justi).eq(0).val(peaje);
        oFilaDestino.find(view.selectores.sel_Justi).eq(1).val(comida);
        oFilaDestino.find(view.selectores.sel_Justi).eq(2).val(transporte);
        oFilaDestino.find(view.selectores.sel_Justi).eq(3).val(hotel);

        var sComentario = oFilaOrigen.attr('comentario');
        var oComent = oFilaDestino.find(view.selectores.sel_Coment).eq(0);
        if (sComentario && sComentario != "") {
            oFilaDestino.attr('comentario', sComentario);
            oComent.attr("data-content", sComentario);
            oComent.removeClass('fa-comment-o').addClass('fa-commenting-o');
        }
        else
            oComent.removeClass('fa-commenting-o').addClass('fa-comment-o');
        //calcularLineaTotal(oFilaDestino);
        oFilaDestino.find(view.selectores.sel_Total).eq(0).val(oFilaOrigen.find(view.selectores.sel_Total).eq(0).val());

        calcularPie();
        view.marcarLinea(oFilaDestino);
    }

    var setCampoCalculadora = function (e) {
        var srcobj = event.target ? event.target : event.srcElement;
        IB.vars.prevFocus = srcobj;
    }
    var calculadora = function (e) {
        IB.vars.returnElement = null;
        //view.dom.idCalculadoratxtResultado.val('');
        $(view.selectores.idCalculadoratxtResultado).eq(0).val('');

        view.dom.btnLlevarValor.prop('disabled', true);
        if ($(IB.vars.prevFocus).hasClass('txtJusti')) {
            IB.vars.returnElement = IB.vars.prevFocus;
            view.dom.btnLlevarValor.prop('disabled', false);
        }

        view.dom.calculadora.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        view.dom.calculadora.modal('show');
        view.dom.ocultable.attr('aria-hidden', 'true');
    }
    //obtenerMotivos = function (e) {
    //    try {

    //        //var filtro = { sUsuario: IB.vars.codUsu, sFechaDesde: dom.fechaDesde.val(), sFechaHasta: dom.fechaHasta.val() };
    //        var filtro = {};

    //        IB.procesando.mostrar();
    //        IB.DAL.post(null, "getMotivos", filtro, null,
    //            function (data) {
    //                view.rellenarComboMotivos(data);
    //            },
    //            function (ex, status) {
    //                IB.procesando.ocultar();
    //                IB.bsalert.fixedAlert("danger", "Error", "Al recuperar los motivos.");
    //            }
    //        );
    //    } catch (e) {
    //        IB.bsalert.fixedAlert("danger", "Error", "Error al llamar a motivos.");
    //    }
    //}
    obtenerMonedas = function (e) {
        try {

            var defer = $.Deferred();
            //var filtro = { sUsuario: IB.vars.codUsu, sFechaDesde: dom.fechaDesde.val(), sFechaHasta: dom.fechaHasta.val() };
            var filtro = {};

            IB.procesando.mostrar();
            IB.DAL.post(null, "getMonedas", filtro, null,
                function (data) {
                    view.rellenarComboMonedas(data);
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar las monedas.");
                    defer.reject();
                }
            );
            return defer.promise();
        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al llamar a monedas.");
            defer.reject();
        }
    }
    var setEmpresa = function (e) {
        IB.vars.sIdEmpresa = $(this).val();
        IB.vars.sIdTerritorio = $(view.selectores.empresa).attr('idterritorio')
    }
    var setMoneda = function (e) {
        IB.vars.idMoneda = $(this).val();
        if (IB.vars.idMoneda == "EUR") {
            $(view.dom.tablaGastos).find('tbody > tr').each(function () {
                $(this).find(view.selectores.sel_Dieta).attr('readonly', false);
            });
        }
        else {
            $(view.dom.tablaGastos).find('tbody > tr').each(function () {
                $(this).find(view.selectores.sel_Dieta).val(0);
                $(this).find(view.selectores.sel_Dieta).attr('readonly', true);
                $(this).find(view.selectores.sel_ImpDieta).val(0);
                //Quito el icono del coche
                $(this).find(view.selectores.sel_CabEco).html("");
                //Pongo el importe por KMs a cero
                //$(this).find('td[headers="hImpoProp"]').html('');
                $(this).find(view.selectores.sel_ImpKm).val('');
                $(this).find(view.selectores.sel_Km).val('');
                $(this).find(view.selectores.sel_Km).attr('readonly', true);
                //Oculto el icono de información de distancia kilométricas
                view.dom.btnDistancias.hide();
                calcularLineaTotal($(this));
            });
        }
    }
    //obtenerHistorial = function (e) {
    //    try {

    //        //var filtro = { sUsuario: IB.vars.codUsu, sFechaDesde: dom.fechaDesde.val(), sFechaHasta: dom.fechaHasta.val() };
    //        var filtro = {};

    //        IB.procesando.mostrar();
    //        IB.DAL.post(null, "getHistorial", filtro, null,
    //            function (data) {
    //                view.rellenarTablaHistorial(data);
    //            },
    //            function (ex, status) {
    //                IB.procesando.ocultar();
    //                IB.bsalert.fixedAlert("danger", "Error", "Al recuperar el historial.");
    //            }
    //        );
    //    } catch (e) {
    //        IB.bsalert.fixedAlert("danger", "Error", "Error al llamar a historial.");
    //    }
    //}

    obtenerDenomProyecto = function (e) {
        try {

            //var filtro = { sUsuario: IB.vars.codUsu, sFechaDesde: dom.fechaDesde.val(), sFechaHasta: dom.fechaHasta.val() };
            var filtro = {};

            IB.procesando.mostrar();
            IB.DAL.post(null, "getDenomProyecto", filtro, null,
                function (data) {
                    //view.rellenarComboMonedas(data);
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar las monedas.");
                }
            );
        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al llamar a monedas.");
        }
    }
    buscarProyecto = function (e) {
        var o = {
            tipoBusqueda: "SeleccionarUnProyecto_IAP_GASVI"
        }
        $(view.selectores.sel_Proy).buscaproyecto("option", "searchParams", o);
        $(view.selectores.sel_Proy).buscaproyecto("show");
    }
    //var anotacion = function (e) {
    //    view.dom.anotaciones.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    //    view.dom.anotaciones.modal('show');
    //    view.dom.ocultable.attr('aria-hidden', 'true');
    //}
    var anotacion = function (e) {
        view.dom.hdnAnotacionesPersonales.val(view.dom.txtDesAnotaciones.val());
        view.dom.anotaciones.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        view.dom.anotaciones.modal('show');
        view.dom.ocultable.attr('aria-hidden', 'true');
        //setTimeout("view.dom.txtDesAnotaciones.focus();", 500);
        //setTimeout("$('#txtDesAnotaciones').focus();", 800);

    }

    var mostrarDistancias = function (e) {
        view.dom.distancias.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        view.dom.distancias.modal('show');
        view.dom.ocultable.attr('aria-hidden', 'true');
    }
    //var proyecto = function (e) {
    //    view.cebrear();
    //    //Limpiamos los filtros antes de volver a mostrar el buscador
    //    view.dom.buscaProyectos.val('');
    //    view.dom.tbodyProyectos.find("tr").show();

    //    view.dom.proyecto.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    //    view.dom.proyecto.modal('show');
    //    view.dom.ocultable.attr('aria-hidden', 'true');
    //}
    var getValorCalculadora = function (e) {
        var vVal = eval($(view.selectores.idCalculadoratxtResultado).eq(0).val());
        //$(IB.vars.returnElement).val(view.dom.idCalculadoratxtResultado.val());
        $(IB.vars.returnElement).val(accounting.formatNumber(vVal));
        view.dom.calculadora.modal('hide');
        calcularLineaTotal($(IB.vars.returnElement).closest('tr'));
    }
    var evaluar = function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            //view.dom.idCalculadoratxtResultado.val(eval(view.dom.idCalculadoratxtResultado.val()));
            var sResul = eval($(view.selectores.idCalculadoratxtResultado).eq(0).val());
            //view.dom.idCalculadoratxtResultado.val(sResul);
            $(view.selectores.idCalculadoratxtResultado).val(sResul);
        }
    }    
    var iconoFichero = function (e) {
        if (this.value == "0") {//El usuario ha marcado SIN justificante
            view.dom.icoJustificante.removeClass("fa-question-circle").removeClass("fa-check-circle").removeClass("text-success").addClass("fa-times-circle text-warning");
            view.dom.icoJustificante.attr('title', "No existen justificantes");
            calcularPie();
        }
        else {//El usuario ha marcado CON justificante
            view.dom.icoJustificante.removeClass("fa-question-circle").removeClass("fa-times-circle").removeClass("text-warning").removeClass("blink_me").addClass("fa-check-circle text-success");
            view.dom.icoJustificante.attr('title', "Existen gastos que requieren justificantes");
        }
    }
    var ponerFocoInput = function (e) {
        // view.dom.idCalculadoratxtResultado.focus();
        $(view.selectores.idCalculadoratxtResultado).eq(0).focus();
    }
    var ponerFocoDieta = function (e) {
        view.dom.tablaGastos.find(view.selectores.filaActiva).find(view.selectores.sel_Dieta).eq(0).focus();
    }
    var getProyecto = function (e) {
        view.dom.txtPro.val()
        view.dom.proyecto.modal('hide');
    }

    var clickLinea = function (event) {
        var srcobj = event.target ? event.target : event.srcElement;
        //Si el elemento no es el td se busca en la jerarquia superior
        if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td')
        ////Se marca la línea como seleccionada
        if (srcobj.length != 0)
            view.marcarLinea($(srcobj).parent());
    }
    var calcularDietas = function (event) {
        if ($(this).val() > 255)
            $(this).val(255);

        var nImporteAux = 0;
        //var oFila = $(this).closest('tr');
        var oFila = $(this).parent().parent();
        //Dietas completas.
        var sValorAux = oFila.find(view.selectores.sel_Dieta).eq(0).val();
        nImporteAux += accounting.unformat(sValorAux, ",") * accounting.unformat(IB.vars.cldDCCO, ",");
        //Media Dietas
        sValorAux = oFila.find(view.selectores.sel_Dieta).eq(1).val();
        nImporteAux += accounting.unformat(sValorAux, ",") * accounting.unformat(IB.vars.cldMDCO, ",");
        //Dieta especial
        sValorAux = oFila.find(view.selectores.sel_Dieta).eq(2).val();
        nImporteAux += accounting.unformat(sValorAux, ",") * accounting.unformat(IB.vars.cldDECO, ",");
        //Dieta alojamiento
        sValorAux = oFila.find(view.selectores.sel_Dieta).eq(3).val();
        nImporteAux += accounting.unformat(sValorAux, ",") * accounting.unformat(IB.vars.cldDACO, ",");

        //Establezco el total de dietas 
        oFila.find(view.selectores.sel_ImpDieta).eq(0).val(accounting.formatNumber(nImporteAux));

        //Establezco el total de la linea
        calcularLineaTotal(oFila);
    }
    var calcularJustificados = function (event) {
        $(this).val(accounting.formatNumber(accounting.unformat($(this).val(), ","), 2));
        calcularLineaTotal($(this).closest('tr'));
    }
    var formatFloats = function (event) {
        $(this).val(accounting.formatNumber(accounting.unformat($(this).val(), ","), 2));
    }
    var getECO2 = function (event) {
        var oFila = $(this).closest('tr');
        getECO(oFila);
    }
    var getECO = function (oFila) {
        //var oFila = $(this).closest('tr');
        oFila.addClass('activa');

        var sFecIni = oFila.find(view.selectores.sel_Fecha).eq(0).val();
        if (!sFecIni) sFecIni = oFila.find('td').eq(0).text();
        var sFecFin = oFila.find(view.selectores.sel_Fecha).eq(1).val();
        if (!sFecFin) sFecFin = oFila.find('td').eq(1).text();

        if (sFecIni == "" || sFecFin == "") {
            IB.bsalert.toastdanger("Para seleccionar una referencia ECO, es necesario indicar las fechas.");
        }
        else {
            view.dom.desplazamiento.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            view.dom.desplazamiento.modal('show');
            view.dom.ocultable.attr('aria-hidden', 'true');

            //IB.procesando.mostrar();
            view.dom.tbodyDesplaza.html('');

            var filtro = { sInteresado: IB.vars.codUsu, sDesde: sFecIni, sHasta: sFecFin, sReferencia: "0" };
            //var filtro = { sInteresado: IB.vars.codUsu, sDesde: '01/04/2016', sHasta: '30/04/2016', sReferencia: '0' };

            IB.DAL.post(null, "ObtenerDesplazamientos", filtro, null,
                function (data) {
                    view.pintartblDesplaza(data);
                    view.attachLiveEvents("click", view.selectores.filasDespla, marcarEco);
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar los datos de la tabla de desplazamientos en el servidor.");
                }
            );
        }
    }
    var marcarEco = function (event) {
        var srcobj = event.target ? event.target : event.srcElement;
        //Si el elemento no es el td se busca en la jerarquia superior
        if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td')
        ////Se marca la línea como seleccionada
        if (srcobj.length != 0) view.marcarLineaECO($(srcobj).parent());
    }
    /*
    var oECOReq = document.createElement("img");
    oECOReq.setAttribute("src", "../../../images/imgECOReq.gif");
    oECOReq.setAttribute("style", "cursor:url(../../../images/imgManoAzul2.cur),pointer;margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
    */
    var calcularKm = function (event) {
        var oKm = $(this);
        var oFila = oKm.closest('tr');
        var nKm = accounting.formatNumber(oKm.val().replace(',', '.'), 0);
        if (nKm <= 0) oKm.val(0);
        //if (oKm.val() > 0) oKm.val(accounting.formatNumber(parseInt(oKm.val()), 0));
        if (nKm > 0) oKm.val(nKm);

        if (IB.vars.sIdOficinaBase != "") {
            if (nKm > 0) {
                if (nKm > IB.vars.nMinimoKmsECO) {
                    //Pongo el icono del coche
                    oFila.find(view.selectores.sel_CabEco).html("<img class='imgECO' src='" + IB.vars.strServer + "images/imgECOReq.gif'/>");
                } else {
                    oFila.find(view.selectores.sel_CabEco).html("");
                    oFila.removeAttr('eco');
                }
                //var sValorAux = oFila.find(view.selectores.sel_Km).eq(0).val();
                //var nImporteAux = accounting.unformat(sValorAux, ",") * accounting.unformat(IB.vars.cldKMCO, ",");
                var nImporteAux = nKm * accounting.unformat(IB.vars.cldKMCO, ",");
                oFila.find(view.selectores.sel_ImpKm).val(accounting.formatNumber(nImporteAux));
                //Muestro el icono de información de distancia kilométricas imgKMSEstandares
                view.dom.btnDistancias.show();
            }
            else {
                //Quito el icono del coche
                oFila.find(view.selectores.sel_CabEco).html("");
                oFila.removeAttr('eco');
                //Pongo el importe por KMs a cero
                //oFila.find('td[headers="hImpoProp"]').html('');
                oFila.find(view.selectores.sel_ImpKm).val('');
                //Oculto el icono de información de distancia kilométricas
                view.dom.btnDistancias.hide();
            }
        }
        calcularLineaTotal(oFila);
    }
    var calcularLineaTotal = function (oFila) {
        var nImporteAux = 0;
        //Acumulado de dietas
        var sValorAux = oFila.find(view.selectores.sel_ImpDieta).eq(0).val();
        nImporteAux += accounting.unformat(sValorAux, ",");
        //Kms
        //sValorAux = oFila.find('td[headers="hImpoProp"]').html();
        sValorAux = oFila.find(view.selectores.sel_ImpKm).val();
        nImporteAux += accounting.unformat(sValorAux, ",");
        //Peajes, Comidas, Transportes y Hoteles
        oFila.find(view.selectores.sel_Justi).each(function () {
            nImporteAux += accounting.unformat($(this).val(), ",");
        });
        //Establezco el total de la fila 
        oFila.find(view.selectores.sel_Total).eq(0).val(accounting.formatNumber(nImporteAux));

        //Actualizo el pié del catálogo con los totales por columna
        calcularPie();
    }

    var calcularPie = function () {
        var nT = 0;
        var nAux = 0;
        $(view.dom.tablaGastos).find('tfoot > tr td').each(function (i) {
            nAux = 0;
            if (i > 3 && i != 11) {
                nAux = calcularTotalColumna(i);
                switch (i) {
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        nT += nAux;
                        break;
                }
            }
        });
        setEstadoJustificante(nT);
        ponerTotales();
    }
    var setEstadoJustificante = function (nImp) {
        var bExisteGastoConJustificante = false;
        if (nImp != 0) {
            bExisteGastoConJustificante = true;
        }
        switch ($(view.selectores.rdbJustificantes).val()) {
            case "1":
                view.dom.icoJustificante.attr('title', "");
                view.dom.icoJustificante.removeClass("blink_me");
                break;
            case "0":
                if (bExisteGastoConJustificante) {
                    view.dom.icoJustificante.attr('title', "Existen gastos que requieren justificantes");
                    view.dom.icoJustificante.addClass("blink_me");
                } else {
                    view.dom.icoJustificante.attr('title', "No existen justificantes");
                    view.dom.icoJustificante.removeClass("blink_me");
                }
                break;
            default:  //No se ha seleccionado todavía
                if (bExisteGastoConJustificante) {
                    view.dom.icoJustificante.attr('title', "Existen gastos que requieren justificantes");
                    view.dom.icoJustificante.addClass("blink_me");
                } else {
                    view.dom.icoJustificante.attr('title', "¿Existen justificantes?");
                }
                break;
        }
    }
    var calcularTotalColumna = function (index) {
        var total = 0;
        var sImporte = 0;
        $(view.dom.tablaGastos).find('tbody > tr').each(function () {
            sImporte = $('td', this).eq(index).find('input').val();
            var value = parseFloat(sImporte.replace('.', '').replace(',', '.'));
            if (!isNaN(value)) total += value;
        });
        switch (index) {
            case 4:
            case 5:
            case 6:
            case 7:
                $(view.dom.tablaGastos).find('tfoot td').eq(index).text(accounting.formatNumber(parseInt(total), 0));
                break;
            case 9:
                $(view.dom.tablaGastos).find('tfoot td').eq(index).find('span').text(accounting.formatNumber(parseInt(total), 0));
                break;
            default:
                $(view.dom.tablaGastos).find('tfoot td').eq(index).find('span').text(accounting.formatNumber(total));
                break;
        }
        return total;
    }
    var ponerTotales = function () {
        //Los datos básicos calculados son:
        var nTotalGastos = 0; //Total de la tabla/grid de gastos
        var nACobrarEnNomina = 0; //Casilla "En nómina"
        var nACobrarDevolver = 0; //Casilla "Sin retención"
        var nPagadoEmpresa = 0;
        var nTotalViaje = 0;

        var nImpKMCO = accounting.unformat(IB.vars.cldKMCO, ",");
        var nImpKMEX = accounting.unformat(IB.vars.cldKMEX, ",");

        var nImpDCCO = accounting.unformat(IB.vars.cldDCCO, ",");
        var nImpDCEX = accounting.unformat(IB.vars.cldDCEX, ",");

        var nImpMDCO = accounting.unformat(IB.vars.cldMDCO, ",");
        var nImpMDEX = accounting.unformat(IB.vars.cldMDEX, ",");

        var nImpDECO = accounting.unformat(IB.vars.cldDECO, ",");
        var nImpDEEX = accounting.unformat(IB.vars.cldDEEX, ",");

        var nImpDACO = accounting.unformat(IB.vars.cldDACO, ",");
        var nImpDAEX = accounting.unformat(IB.vars.cldDAEX, ",");

        var nTotalDietaCompleta = $(view.dom.tablaGastos).find('tfoot td').eq(4).text();
        var nTotalMediaDieta = $(view.dom.tablaGastos).find('tfoot td').eq(5).text();
        var nTotalDietaEspecial = $(view.dom.tablaGastos).find('tfoot td').eq(6).text();
        var nTotalDietaAlojamiento = $(view.dom.tablaGastos).find('tfoot td').eq(7).text();
        var nTotalKms = $(view.dom.tablaGastos).find('tfoot td').eq(9).text();
        var nTotalgastos = $(view.dom.tablaGastos).find('tfoot td').eq(16).text();

        //Total pagado por la empresa
        //$I("txtPagadoTotal").value = getFloat($I("").value)
        nPagadoEmpresa = accounting.unformat(view.dom.totGas.val(), ",");

        //Total a cobrar "En nómina"
        if (nImpKMCO - nImpKMEX > 0)
            nACobrarEnNomina += accounting.unformat(nTotalKms, ",") * (nImpKMCO - nImpKMEX);
        if (nImpDCCO - nImpDCEX > 0)
            nACobrarEnNomina += nTotalDietaCompleta * (nImpDCCO - nImpDCEX);
        if (nImpMDCO - nImpMDEX > 0)
            nACobrarEnNomina += nTotalMediaDieta * (nImpMDCO - nImpMDEX);
        if (nImpDECO - nImpDEEX > 0)
            nACobrarEnNomina += nTotalDietaEspecial * (nImpDECO - nImpDEEX);
        if (nImpDACO - nImpDAEX > 0)
            nACobrarEnNomina += nTotalDietaAlojamiento * (nImpDACO - nImpDAEX);

        //$I("txtNomina").value = nACobrarEnNomina.ToString("N");
        view.dom.txtNomina.val(accounting.formatNumber(nACobrarEnNomina));

        //Total a cobrar "Sin retención"
        //nTotalGastos = getFloat(nTotalgastos);
        nTotalGastos = accounting.unformat(nTotalgastos, ",");
        nACobrarDevolver = nTotalGastos - nACobrarEnNomina - accounting.unformat(view.dom.txtImpAnticipo.val(), ",") + accounting.unformat(view.dom.txtImpDevolucion.val(), ",");
        view.dom.txtSinRet.val(accounting.formatNumber(nACobrarDevolver));
        //$I("txtACobrarDevolver").style.color = (nACobrarDevolver < 0) ? "red" : "black";
        if (nACobrarDevolver < 0) {
            view.dom.txtSinRet.removeClass('negro')
            view.dom.txtSinRet.addClass('rojo')
        }
        else {
            view.dom.txtSinRet.removeClass('rojo')
            view.dom.txtSinRet.addClass('negro')
        }

        nTotalViaje = nTotalGastos + nPagadoEmpresa;
        view.dom.txtTotalViaje.val(accounting.formatNumber(nTotalViaje));

        //setImagenJustificante();

    }
    var setGastoPagado = function (e) {
        //var nT = getFloat(view.dom.txtPagadoTransporte.val());
        //nT += getFloat(view.dom.txtPagadoHotel.val());
        //nT += getFloat(view.dom.txtPagadoOtros.val());
        var nT = 0;
        view.dom.tablaBilletes.find(view.selectores.sel_Gpe).each(function () {
            nT += accounting.unformat($(this).val(), ",");
        });

        view.dom.totGas.val(accounting.formatNumber(nT));

        ponerTotales();
    }
    var pulsarBtnSeleccionarDesplazamiento = function (e) {
        var $selected = view.dom.tablaDesplaza.find(view.selectores.filaActiva);
        //Si no hubiera ninguno se muestra el aviso
        if ($selected.length == 0) {
            IB.bsalert.toastwarning("No se ha seleccionado ningún proyecto");
            return false;
        }

        var $selected2 = view.dom.tablaGastos.find(view.selectores.filaActiva);
        $selected2.removeAttr('eco');
        $selected2.attr('eco', $selected.children().eq(0).text());
        view.dom.desplazamiento.modal('hide');
    }
    var cancelarDesplazamiento = function (e) {
        view.dom.desplazamiento.modal('hide');
    }
    var imprimir = function (iReferencia) {
        if (!iReferencia || iReferencia == "") return;
        //IB.bsalert.toastwarning("Tramitación correcta");
        //IB.vars.bCambios = false;
        if ($(view.selectores.rdbJustificantes).val() == "1") {
            var strMsg = "Recuerda que tienes que enviar los justificantes por valija, ";
            strMsg += "junto a una copia impresa de la solicitud, ";
            strMsg += "a la atención GASVI a la oficina \"" + $("#txtOfi").val() + "\".<br /><br />";
            strMsg += "Si deseas imprimir ahora la solicitud, elije \"Sí\".<br />";
            strMsg += "En caso contrario, pulsa \"No\".";
            IB.procesando.ocultar();
            $.when(IB.bsconfirm.confirm("primary", "Atención", strMsg)).then(function () {

                //*SSRS

                var params = {
                    reportName: "/SUPER/gsv_Estandar",
                    tipo: "PDF",
                    sReferencia: iReferencia
                }

                //PostSSRS(params, servidorSSRS);

                x = window.open("")

                a = $(x.document.createElement("form")).attr({ "method": "post", "action": servidorSSRS });
                $.each(params, function (key, value) {
                    $.each(value instanceof Array ? value : [value],
                        function (i, val) {
                            $(x.document.createElement("input")).attr({ "type": "hidden", "name": key, "value": Utilidades.escape(val) }).appendTo(a);
                        })
                });
                a.appendTo(x.document.body).submit();

                //SSRS*/
                /*CR
                 var strUrlPag = "../../../GASVI/Informes/Estandar/default.aspx?ref=" + codpar(iReferencia);
                if (screen.width == 800)
                    window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=yes,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
                else
                    window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=no,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
                //CR*/

                salir();
            }, function () {
                salir();
            });
        }
        else salir();
    }
    var salir = function (e) {
        IB.procesando.mostrar();
        $.when(controlarSalir(e)).then(function () {
            setTimeout(function () { location.href = "../ImpDiaria/Default.aspx?" + IB.vars.qs; }, 500);
        });
    }
    //Identificación de cambios en la pantalla
    var hayCambios = function () {
        if (bCambios != 1) {
            bCambios = 1;
            //view.habilitarBtn(view.dom.btnTramitar, 1, grabar);
        }
    }
    //Control de cambios en salida (si dice NO, te mantiene en pantalla)
    var controlarSalir = function (e) {
        var defer = $.Deferred();
        if (bCambios) {
            IB.procesando.ocultar();
            $.when(IB.bsconfirm.confirmCambios()).then(function () {
                IB.procesando.mostrar();
                desactivarGrabar();
                defer.resolve();
            },
                function () {
                    defer.reject();
                });
        } else {
            defer.resolve();
        }
        return defer.promise();
    }
    var desactivarGrabar = function () {
        bCambios = 0;
    }

    return {
        init: init
    }

})(SUPER.IAP30.GASVI.View);