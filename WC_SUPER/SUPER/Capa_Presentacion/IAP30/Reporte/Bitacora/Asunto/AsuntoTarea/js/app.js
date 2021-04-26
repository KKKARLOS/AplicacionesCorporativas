$(document).ready(function () { SUPER.IAP30.BitacoraAsuntoTA.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.BitacoraAsuntoTA = SUPER.IAP30.BitacoraAsuntoTA || {}

SUPER.IAP30.BitacoraAsuntoTA.app = (function (view) {

    // Tratamiento de documentos
    var _docAppLoaded = $.Deferred();

    window.onbeforeunload = function () {
        if (IB.vars.bCambios) return "Si continúas perderás los cambios que no has guardado.";
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

        /****************************** CALCULO DE VARIABLES ****************************************************/
        /*        
        */
        /********************** Inicialización de los componentes que se cargan a traves de los plugins *********************************************/


        /*******************************************************************************************************/

        //Se atachan los eventos segun si estoy en modo grabacion o lectura

        if (IB.vars.permiso == "E") // Si estoy en modo edición atacho los eventos
        {
            //Control de cambios
            view.attachLiveEvents("keypress", view.selectores.sel_inputs, hayCambios);
            view.attachLiveEvents("change", view.selectores.sel_inputs, hayCambios);
            view.attachLiveEvents("change", view.selectores.sel_selects, hayCambios);

            //Profesionales notificables
            view.attachLiveEvents("change", view.selectores.sel_notificables, view.modificarBDFila);

            //Profesionales notificables
            view.attachLiveEvents("change", view.selectores.sel_notificables, view.modificarBDFila);

            //Validar dato numérico
            view.attachLiveEvents("keypress", view.selectores.sel_numeroDecimal, view.validarNumerico);

            //Asignar Responsable
            view.attachEvents("click", view.dom.lblResponsable, abrirBuscadorProfesional);
        
            //Se atachan los eventos 
            view.attachEvents("click", view.dom.btnGrabar, grabarDatos);

            //Enlace eliminar seleccionados
            view.attachEvents("click", view.dom.btnEliminarSeleccionados, view.eliminarFilaProfesionalesSel);

            //Desmarcar eliminados
            view.attachEvents("click", view.dom.btnDesmarcarEliminados, view.desmarcarFilaProfesionalesEliminados);
        
            //Marcar fila profesionales seleccionados
            view.attachLiveEvents('click', view.selectores.filasProfesionalesSel, view.marcarFila);

            //Marcar fila profesionales a seleccionar
            view.attachLiveEvents('click', view.selectores.filasProfesionales, view.marcarFila);

            //Marcar-Desmarcar todos los elementos Origen
            view.attachEvents("click", view.dom.btnMarcarDesmarcarOrigen, view.marcarDesmarcarOrigen);

            //Marcar-Desmarcar todos los elementos Destino
            view.attachEvents("click", view.dom.btnMarcarDesmarcarDestino, view.marcarDesmarcarDestino);

            //Buscar profesionales por criterios de pantalla
            view.attachLiveEvents('keypress', view.selectores.buscarProfesionales, buscarProfesionales);

            //Buscar profesionales asignados
            view.attachEvents('keyup', view.dom.buscAsignados, view.buscarProfesionalesAsignados);
        
            //Añadir elementos seleccionados a través de botón
            view.attachEvents("click", view.dom.btnSeleccionar, view.anadirProfesionalesBtn);
            
            //Añadir elementos seleccionados a través de doble click
            view.attachLiveEvents("dblclick", view.selectores.filasProfesionales, view.anadirProfesionalesDblClick);

        }

        // Mostrar / ocultar bloque de información
        view.attachEvents("click", view.dom.title, view.collapse);

        //Control de carga de pestañas
        view.attachLiveEvents("click", view.selectores.tab, controlCargaSolapas);

        //Salir de la pantalla 
        view.attachEvents("click", view.dom.btnRegresar, salir);

        a = obtenerTiposAsuntos();
        b = $.when(a).then(function () { obtenerDatosAsunto(); });
        c = obtenerPSN_OPD();
        d = obtenerProfesionalesAsunto();

        $.when(a, b , c, d).then(function () {
            IB.procesando.ocultar();
            view.visualizarContenedor();
            view.init();
        });

    }

    //Para evitar el submit del formulario que por defecto se hace al dar intro dentro un html input

    //$('input[type="text"]').keydown(function (event) {
    //    if (event.keyCode == 13) {
    //        event.preventDefault();
    //        return false;
    //    }
    //});

    abrirBuscadorProfesional = function (e) {
        //Se inicializa el control buscadorUsuario (BuscadorUsuario.js)
        view.dom.divBuscadorPersonas.buscadorPersonas({
            modal: true, titulo: "Selección de profesional", tipoBusqueda: 'RESPONSABLES_PROYECTO',
            aceptar: function (data) {
                //alert("profesional seleccionado: " + data.PROFESIONAL);
                view.dom.responsable.val(data.PROFESIONAL);
                IB.vars.idResponsable = data.t314_idusuario;
            },
            cancelar: function () {
            }
        });
    }

    obtenerTiposAsuntos = function (e) {
        var defer = $.Deferred();
        var filtro = {};
        IB.DAL.post(null, "getAsuntos", filtro, null,
            function (data) {
                view.rellenarComboTiposAsunto(data);
                defer.resolve();
            },
            function (ex, status) {
                IB.procesando.ocultar();
                IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar los tipos de asunto.");
                defer.fail();
            }
        );
        return defer.promise();
    }

    obtenerDatosAsunto = function (e) {
        var defer = $.Deferred();
        if (IB.vars.idAsunto != "-1") {
            var filtro = { idAsunto: IB.vars.idAsunto };
            IB.DAL.post(null, "obtenerDetalleAsunto", filtro, null,
                function (data) {
                    view.pintarDatosAsunto(data);
                    defer.resolve();
                 },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos del asunto (" + IB.vars.idAsunto + ").");
                    defer.fail();
                }
            );
        }
        else
        {
            view.pintarDatosAltaAsunto();
            defer.resolve();
        }
        view.dom.desAsunto.focus();
        return defer.promise();
    }

    obtenerPSN_OPD = function (e) {
        var defer = $.Deferred();
        var filtro = { nPSN: IB.vars.nPSN };
        IB.DAL.post(null, "obtenerPSN_OPD", filtro, null,
            function (data) {
                view.pintarDatosPSN_OPD(data);
                defer.resolve();
            },
            function (ex, status) {
                IB.procesando.ocultar();
                IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos del proyecto subnodo (" + IB.vars.nPSN + ").");
                defer.fail();
            }
        );
        return defer.promise();
    }

    obtenerProfesionalesAsunto = function (e) {
        var defer = $.Deferred();
        if (IB.vars.idAsunto != "-1") {
            var filtro = { idAsunto: IB.vars.idAsunto };
            IB.DAL.post(null, "obtenerProfesionalesAsunto", filtro, null,
                function (data) {
                    view.pintarProfesionalesAsunto(data);
                    IB.procesando.ocultar();
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos de profesionales ligados al asunto (" + IB.vars.idAsunto + ").");
                    defer.fail();
                }
            );
        } else defer.resolve();
        return defer.promise();
    }

    buscarProfesionales = function (event) {
        
        if (event.keyCode != 13) {return;}
        if (view.dom.Apellido1.val() == "" && view.dom.Apellido2.val() == "" && view.dom.Nombre.val() == "") {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No se permite realizar búsquedas con los filtros vacíos");
            return false;
        }
        //event.stopPropagation();

        var defer = $.Deferred();

	    var oTecnicos = {
	        Apellido1: view.dom.Apellido1.val(),
	        Apellido2: view.dom.Apellido2.val(),
	        Nombre: view.dom.Nombre.val(),
	        t303_idnodo: IB.vars.idNodo
        };
        var filtro = { oTecnicos: oTecnicos };


        IB.DAL.post(null, "obtenerProfesionales", filtro, null,
            function (data) {
                view.pintarProfesionales(data);
                IB.procesando.ocultar();
                defer.resolve();
            },
            function (ex, status) {
                IB.procesando.ocultar();
                IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos de profesionales ligados al asunto (" + IB.vars.idAsunto + ").");
                defer.fail();
            }
        );
        return defer.promise();
    }
   
    grabarDatos = function (e) {
        if (view.comprobarDatosGrabar()) {
            if (parseInt(IB.vars.idAsunto) == -1 && view.dom.estado.find('option:selected').val() == 0) view.dom.estado.val("1");

            var DatosGenerales = {
                t303_idnodo: null,
                t301_idproyecto: parseInt(IB.vars.nPE),
                t305_idproyectosubnodo: parseInt(IB.vars.nPSN),
                T600_alerta: view.dom.alerta.val(),
                T600_desasunto: view.dom.desAsunto.val(),
                T600_desasuntolong: view.dom.descripcion.val(),
                T600_dpto: view.dom.departamento.val(),
                T600_estado: view.dom.estado.val(),
                T600_etp: parseFloat(getFloat(view.dom.esfuerzoPlanificado.val())),
                T600_etr: parseFloat(getFloat(view.dom.esfuerzoReal.val())),
                T600_fcreacion: cadenaAfecha(view.dom.fechaCreacion.val()),
                T600_ffin: cadenaAfecha(view.dom.fechaFinalizacion.val()),
                T600_flimite: cadenaAfecha(view.dom.fechaLimite.val()),
                T600_fnotificacion: cadenaAfecha(view.dom.fechaNotificacion.val()),
                T600_idasunto: parseInt(IB.vars.idAsunto),
                T600_notificador: view.dom.notificador.val(),
                T600_obs: view.dom.observaciones.val(),
                T600_prioridad: view.dom.prioridad.val(),
                T600_refexterna: view.dom.refExterma.val(),
                T600_registrador: parseInt(IB.vars.codUsu),
                T600_responsable: parseInt(IB.vars.idResponsable),
                T600_severidad: view.dom.severidad.val(),
                T600_sistema: view.dom.sistema.val(),
                T384_idtipo: parseInt(view.dom.tipo.val()),
                T384_destipo: view.dom.tipo.find('option:selected').text(),
                Registrador: view.dom.registrador.val(),
                Responsable: view.dom.responsable.val(),
                T600_estado_anterior: IB.vars.coEstadoAnterior,
                DesPE: IB.vars.desPE,
                DesEstado: view.dom.estado.find('option:selected').text(),
                DesSeveridad: view.dom.severidad.find('option:selected').text(),
                DesPrioridad: view.dom.prioridad.find('option:selected').text(),
                t331_idpt: IB.vars.nPT,
                t331_despt: IB.vars.desPT,
                t332_idtarea: IB.vars.idTarea,
                t332_destarea: IB.vars.desTarea,
                t334_desfase: IB.vars.fase,
                t335_desactividad: IB.vars.actividad
            };

            var ListaIntegrantes = [];

            $.each($(view.selectores.filasProfesionalesSel), function () {
                //if ($(this).attr("data-bd") != "") {//Solo se actualizan filas modificadas // no porque sino luego no aparecen en la relación de notificación de correo
                    oIntegrante = new Object();
                    oIntegrante.t314_idusuario = $(this).attr("data-id");
                    oIntegrante.nomRecurso = $(this).children().eq(0).text().trim();
                    oIntegrante.T600_idasunto = parseInt(IB.vars.idAsunto);
                    oIntegrante.t604_notificar = ($(this).children().eq(1).children().eq(0).is(':checked') == true) ? true : false;
                    oIntegrante.mail = $(this).attr("data-mail");
                    oIntegrante.t001_sexo = $(this).attr("data-sexo");
                    oIntegrante.t303_idnodo = parseInt(0);
                    oIntegrante.baja = $(this).attr("data-baja");
                    oIntegrante.tipo = $(this).attr("data-tipo");
                    oIntegrante.accionBD = $(this).attr("data-bd");

                    ListaIntegrantes.push(oIntegrante);
                //}
            });

            var payload = { DatosGenerales: DatosGenerales, Integrantes: ListaIntegrantes };

            IB.procesando.mostrar();
            IB.DAL.post(null, "grabar", payload, null,
                function (data) {
                    IB.vars.idAsunto = parseInt(data);
                    view.dom.idAsunto.val(accounting.formatNumber(IB.vars.idAsunto, 0));
                    if (IB.vars.coEstadoAnterior != view.dom.estado.val())
                    {
                        view.marcarNoLeidoCrono();
                    }
                    $.each($(view.selectores.filasProfesionalesSel), function (e) {
                        if ($(this).attr("data-bd") == "D") {
                            $(this).closest('tr').remove();
                        }
                        else if ($(this).attr("data-bd") == "I") $(this).attr("data-bd", "");

                    });
                    IB.procesando.ocultar();
                    IB.bsalert.toast("Grabación correcta.", "success");
                    desactivarGrabar();
                }
            );
        }
    }
    var salir = function (e) {        
        $.when(controlarSalir(e)).then(function () {
            //setTimeout(function () { location.href = "../../BitacoraTarea/Default.aspx?" + IB.vars.qs; }, 500);
            if (IB.vars.origen == "IAP")
                setTimeout(function () { location.href = "../../../../Consultas/Bitacora/Tarea/Default.aspx?" + IB.vars.qs; }, 500);
            else
                setTimeout(function () { location.href = "../../BitacoraTarea/Default.aspx?" + IB.vars.qs; }, 500);

        });
    }

    //var comprobarDatosGrabar = function () {
    //    var sError = "";
    //    var nError = 0;

    //    IB.procesando.mostrar();
    //    //Compruebo datos de la cabecera

    //    if (view.dom.desAsunto.val() == "") {
    //        sError = "Debes indicar el nombre del asunto";
    //        nError = 1;
    //    }


    //    if (sError == "") {
    //        if (view.dom.fechaNotificacion.val() == "") {
    //            sError = "Debes indicar la fecha de notificación";
    //            nError = 2;
    //        }
    //    }
    //    if (sError == "") {
    //        if (parseInt(IB.vars.idAsunto) != -1) {
    //            if (view.dom.estado.find('option:selected').val() == 0) {
    //                sError = "Debes indicar el estado del asunto";
    //                nError = 3;
    //            }
    //        }
    //    }

    //    if (sError == "") {
    //        if (IB.vars.idResponsable == "0") {
    //            sError = "Debes indicar el responsable del asunto";
    //            nError = 4;
    //        }
    //    }

    //    //Alertas por e-mail
    //    if (view.dom.alerta.val() != "") {
    //        $.each($(view.dom.alerta).val().split(";"), function (i, item) {
    //            if (item != "") //IB.bsalert.fixedAlert("warning", "¡Atención!", "Hay información para validar");
    //            {
    //                if (!validarEmail(item.trim())) {
    //                    sError = "La dirección de correo indicada en el campo 'Notificar a' no es válida (" + item.trim() + ")";
    //                    nError = 5;
    //                }
    //            }
    //        });
    //    }


    //    if (sError != "") {
    //        IB.procesando.ocultar();
    //        a = IB.bsalert.fixedAlert("warning", "Error de validación", sError);
    //        $.when(a).then(function () {
    //            $("#pestana1").trigger("click");
    //            switch (nError) {
    //                case 1: view.dom.desAsunto.focus(); break;
    //                case 2: view.dom.fechaNotificacion.focus(); break;
    //                case 3: view.dom.estado.focus(); break;
    //                case 5: view.dom.alerta.focus(); break;
    //            }
    //        });
    //        return false;
    //    }
    //    return true;
    //}

   var controlCargaSolapas = function (e) {
        var oElement = e.srcElement ? e.srcElement : e.target;
        oElement = $(oElement);

        if (oElement.parent().hasClass("disabled")) return false;

        //if (oElement.attr("href") == "#general" && oElement.hasClass("noLeido")) oElement.removeClass("noLeido").addClass("leido");
       //la solapa 1 se lee en la carga
        
        if (oElement.attr("href") == "#general") view.controlarScroll();
        else if (oElement.attr("href") == "#crono" && oElement.hasClass("noLeido") && parseInt(IB.vars.idAsunto) != -1) obtenerDatosCronologia(oElement);
        else if (oElement.attr("href") == "#docu" && oElement.hasClass("noLeido") && parseInt(IB.vars.idAsunto)!=-1) {
            initPluginDocumentacion();
            mostrarDocumentos();
            //oElement.removeClass("noLeido").addClass("leido");
            view.marcarLeido(oElement);
        }
    }

    obtenerDatosCronologia = function (e) {
        if (IB.vars.idAsunto != "") {
            view.dom.tablaCrono.html("");
            var filtro = { idAsunto: IB.vars.idAsunto };
            IB.procesando.mostrar();
            IB.DAL.post(null, "obtenerDetalleCronologia", filtro, null,
                function (data) {
                    if (data != null) {
                        view.pintarTablaCronologia(data);
                        //e.removeClass("noLeido").addClass("leido");
                        view.marcarLeido(e);
                    } else IB.bsalert.fixedAlert("danger", "Error de aplicación", "No se han obtenido datos de cronología del asunto (" + IB.vars.idAsunto + ").");
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos d cronología del asunto (" + IB.vars.idAsunto + ").");
                    IB.procesando.ocultar();
                }
            );
        };
    }
    //Identificación de cambios en la pantalla
    var hayCambios = function () {
        if (IB.vars.bCambios != 1) {
            IB.vars.bCambios = 1;
        }
    }
    //Control de cambios en salida (si dice NO, te mantiene en pantalla)
    var controlarSalir = function (e) {
        var defer = $.Deferred();
        if (IB.vars.bCambios) {
            $.when(IB.bsconfirm.confirmCambios()).then(function () {
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
        IB.vars.bCambios = 0;
    }
    //Documentos
    function initPluginDocumentacion() {

        _idDoc = IB.vars.idAsunto;
        _modoContainerDocs = IB.vars.sModoContainer;
        _soySuperEditor = IB.vars.superEditor == "True" ? true : false;

        $.when(SUPER.IAP30.appDocumentos.initModuloDoc(_idDoc, "detalleAsuntoTA", _modoContainerDocs, IB.vars.codUsu, _soySuperEditor))
           .then(function () {
               _docAppLoaded.resolve();
               /*$.when(SUPER.IAP30.appDocumentos.count()).then(function (data) { view.setContadorDocumentos(data); });
               SUPER.IAP30.appDocumentos.onClose(function (data) { view.setContadorDocumentos(data) });*/
           });
    }
    function mostrarDocumentos() {
        $.when(_docAppLoaded).then(SUPER.IAP30.appDocumentos.show);
    }

    return {
        init: init
    }

})(SUPER.IAP30.BitacoraAsuntoTA.View);