$(document).ready(function () { SUPER.IAP30.BitacoraAccionTarea.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.BitacoraAccionTarea = SUPER.IAP30.BitacoraAccionTarea || {}

SUPER.IAP30.BitacoraAccionTarea.app = (function (view) {

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

        //Se atachan los eventos según si estoy en modo grabacion o lectura

        if (IB.vars.permiso == "E") // Si estoy en modo edición atacho los eventos
        {
            //Control de cambios
            view.attachLiveEvents("keypress", view.selectores.sel_inputs, hayCambios);
            view.attachLiveEvents("change", view.selectores.sel_inputs, hayCambios);
            view.attachLiveEvents("change", view.selectores.sel_selects, hayCambios);
            //Profesionales notificables
            view.attachLiveEvents("change", view.selectores.sel_notificables, view.modificarBDFila);

            //Validar dato numérico
            view.attachLiveEvents("keypress", view.selectores.sel_numeroDecimal, view.validarTeclaNumerica);

            //Formatear al perder el foco
            view.attachLiveEvents("focusout", view.selectores.sel_numeroDecimal, view.validarFormatoDecimal);

            //Botón grabar
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


        a = obtenerDatosAsunto();
        b = obtenerDatosAccion();
        c = obtenerProfesionalesAccion();

        $.when(a, b, c).then(function () {
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

    obtenerDatosAsunto = function (e) {
        var defer = $.Deferred();
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
        return defer.promise();
    }

    obtenerDatosAccion = function (e) {
        var defer = $.Deferred();
        if (IB.vars.idAccion != "-1") {
            var filtro = { idAccion: IB.vars.idAccion };
            IB.DAL.post(null, "obtenerDetalleAccion", filtro, null,
                function (data) {
                    view.pintarDatosAccion(data);
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos de la acción (" + IB.vars.idAccion + ").");
                    defer.fail();
                }
            );
        } else defer.resolve();
        return defer.promise();
    }

    obtenerProfesionalesAccion = function (e) {
        var defer = $.Deferred();
        if (IB.vars.idAccion != "-1") {
            var filtro = { idAccion: IB.vars.idAccion };
            IB.DAL.post(null, "obtenerProfesionalesAccion", filtro, null,
                function (data) {
                    view.pintarProfesionalesAccion(data);
                    IB.procesando.ocultar();
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos de profesionales ligados a la acción (" + IB.vars.idAccion + ").");
                    defer.fail();
                }
            );
        } else defer.resolve();
        return defer.promise();
    }

    buscarProfesionales = function (event) {
        if (event.keyCode != 13) return;
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
                IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos de profesionales ligados a la acción (" + IB.vars.idAccion + ").");
                defer.fail();
            }
        );
        return defer.promise();
    }

    grabarDatos = function (e) {
        if (comprobarDatosGrabar()) {

            var DatosGenerales = {
                T601_idaccion: parseInt(IB.vars.idAccion),
                T601_desaccion: view.dom.desAccion.val(),
                T600_idasunto: parseInt(IB.vars.idAsunto),
                T600_desAsunto: view.dom.desAsunto.val(),
                T601_desaccionlong: view.dom.descripcion.val(),
                T601_avance: view.dom.avance.find('option:selected').text(),
                T601_ffin: cadenaAfecha(view.dom.fechaFinalizacion.val()),
                T601_flimite: cadenaAfecha(view.dom.fechaLimite.val()),
                T601_obs: view.dom.observaciones.val(),
                T601_dpto: view.dom.departamento.val(),
                T601_alerta: view.dom.alerta.val(),
                t314_idusuario_responsable: IB.vars.idResponsable,
                t301_idproyecto: IB.vars.nPE,
                t301_denominacion: IB.vars.desPE,
                t331_idpt: IB.vars.nPT,
                t331_despt: IB.vars.desPT,
                t332_idtarea: IB.vars.idTarea,
                t332_destarea: IB.vars.desTarea,
                t334_desfase: IB.vars.fase,
                t335_desactividad: IB.vars.actividad
            };

            var ListaIntegrantes = [];

            $.each($(view.selectores.filasProfesionalesSel), function () {
                //if ($(this).attr("data-bd") != "") {//Solo se actualizan filas modificadas
                oIntegrante = new Object();
                oIntegrante.t314_idusuario = $(this).attr("data-id");
                oIntegrante.nomRecurso = $(this).children().eq(0).text().trim();
                oIntegrante.T601_idaccion = parseInt(IB.vars.idAccion);
                oIntegrante.t605_notificar = ($(this).children().eq(1).children().eq(0).is(':checked') == true) ? true : false;
                oIntegrante.MAIL = $(this).attr("data-mail");
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
                    IB.vars.idAccion = parseInt(data);
                    view.dom.idAccion.val(accounting.formatNumber(IB.vars.idAccion, 0));
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
        //$.when(controlarSalir(e)).then(function () {
        //    setTimeout(function () { location.href = "../../BitacoraTarea/Default.aspx?" + IB.vars.qs; }, 500);
        //});
        $.when(controlarSalir(e)).then(function () {
            if (IB.vars.origen == "IAP")
                setTimeout(function () { location.href = "../../../../Consultas/Bitacora/Tarea/Default.aspx?" + IB.vars.qs; }, 500);
            else
                setTimeout(function () { location.href = "../../BitacoraTarea/Default.aspx?" + IB.vars.qs; }, 500);

        });
    }

    var comprobarDatosGrabar = function () {
        var sError = "";
        var nError = 0;

        IB.procesando.mostrar();
        //Compruebo datos de la cabecera

        if (view.dom.desAccion.val() == "") {
            sError = "Debes indicar el nombre de la acción";
            nError = 1;
        }
        //Alertas por e-mail
        if (view.dom.alerta.val() != "") {
            $.each($(view.dom.alerta).val().split(";"), function (i, item) {
                if (item != "") //IB.bsalert.fixedAlert("warning", "¡Atención!", "Hay información para validar");
                {
                    if (!validarEmail(item.trim())) {
                        sError = "La dirección de correo indicada en el campo 'Notificar a' no es válida (" + item.trim() + ")";
                        nError = 2;
                    }
                }
            });
        }

        if (sError != "") {
            IB.procesando.ocultar();
            a = IB.bsalert.fixedAlert("warning", "Error de validación", sError);
            $.when(a).then(function () {
                $("#pestana1").trigger("click");
                switch (nError) {
                    case 1: view.dom.desAccion.focus(); break;
                    case 2: view.dom.alerta.focus(); break;
                }
            });
            return false;
        }
        return true;
    }

    var controlCargaSolapas = function (e) {
        var oElement = e.srcElement ? e.srcElement : e.target;
        oElement = $(oElement);

        if (oElement.parent().hasClass("disabled")) return false;

        //if (oElement.attr("href") == "#general" && oElement.hasClass("noLeido")) oElement.removeClass("noLeido").addClass("leido");
        //la solapa 1 se lee en la carga
        view.controlarScroll();
        //if (oElement.attr("href") == "#general") view.controlarScroll();
        if (oElement.attr("href") == "#tareas" && oElement.hasClass("noLeido") && parseInt(IB.vars.idAccion) != -1) obtenerDatosTareas(oElement);
        else if (oElement.attr("href") == "#docu" && oElement.hasClass("noLeido") && parseInt(IB.vars.idAccion) != -1) {
            initPluginDocumentacion();
            mostrarDocumentos();
            view.marcarLeido(oElement);
        }
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

        _idDoc = IB.vars.idAccion;
        _modoContainerDocs = IB.vars.sModoContainer;
        _soySuperEditor = IB.vars.superEditor == "True" ? true : false;

        $.when(SUPER.IAP30.appDocumentos.initModuloDoc(_idDoc, "detalleAccionTA", _modoContainerDocs, IB.vars.codUsu, _soySuperEditor))
           .then(function () {
               _docAppLoaded.resolve();
           });
    }
    function mostrarDocumentos() {
        $.when(_docAppLoaded).then(SUPER.IAP30.appDocumentos.show);
    }

    var validarFormatoDecimal = function (event) {
        $(this).val(accounting.formatNumber(getFloat($(this).val())));
    }

    return {
        init: init
    }

})(SUPER.IAP30.BitacoraAccionTarea.View);