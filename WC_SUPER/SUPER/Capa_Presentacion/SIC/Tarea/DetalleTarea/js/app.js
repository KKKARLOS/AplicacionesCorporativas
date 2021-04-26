$(document).ready(function () { });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app = (function (view) {

    var _estado = IB.vars.ta207_estado;
    var _accesoDocumentacion;
    var _modoContainerDocsAccion;
    var _modoContainerDocsTarea;
    var _soySuperEditor = false;
    var _idDoc;
    var _docAppLoaded = $.Deferred();
    var bcambios = false;
    var _oPerfilesEdicion = IB.vars.perfilesEdicion;
    var _ta206_itemorigen = IB.vars.ta206_itemorigen;
    var _ta206_iditemorigen = IB.vars.ta206_iditemorigen;
    
    if (IB.vars.error) {
        IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
        return;
    }

    if (IB.vars.aviso) {
        switch (IB.vars.aviso) {
            case 1:
                IB.bserror.mostrarAvisoAplicacion("Detalle de tarea", "No se ha podido obtener los datos de la tarea para su edición");
                view.desactivarControles();
                break;
        }
        return;
    }

    window.onbeforeunload = function () {
        if (bcambios) return 'Hay cambios sin guardar. Si continúas perderás dichos cambios.';
    };

    //gestion del bcambios
    var camposEditables = view.dom.theForm.find("textarea, input[type='text'], select");
    view.attachEvents("change keyup", camposEditables, function () {
        bcambios = true;
        view.manejarObjetosDOM.habilitarbtnGrabar();
    });

    view.init(_ta206_itemorigen, _ta206_iditemorigen);

    //Obtiene datos del CRM   
    if (_ta206_itemorigen === "S")
        obtenerInfoSuper();
    else
        obtenerInfoCRM();

    if (IB.vars.modoPantalla == "E") {
        //el Datatable participantes devuelve una promesa. Los controles se activarán cuando se resuelva la promesa.
        var promesaDTParticipantes = view.initDatatableParticipantes(IB.vars.ta207_idtareapreventa);
    }
    else {
        var promesaDTParticipantes = view.initDatatableParticipantes(-1);
    }

    $.when(promesaDTParticipantes).then(activarControles);

    //Activación de controles de pantalla
    function activarControles() {
        //Se habilita el enlace a la documentación
        view.manejarObjetosDOM.habilitarDocumentacion();
        //_accesoDocumentacion = "X";            

        //Se desatacha el evento así, porque es un Live Event (document, selector)
        view.detachLiveEvents("click", view.dom.fk_ltrParticipacionProfesional, view.fk_ParticipacionProfesional_onClick);

        //Casuística de pantalla tareas
        if (IB.vars.ta207_estado != "A" && IB.vars.modoPantalla == "E") {
            if (IB.vars.ta207_estado == "XS" || IB.vars.ta207_estado == "FS" || IB.vars.ta207_estado == "XA" || IB.vars.ta207_estado == "FA" || IB.vars.ta207_estado == "X") {
                view.manejarObjetosDOM.anuladasEstadoEdicion();

                //Tarea anulada
                if (IB.vars.ta207_estado == "XS")
                    view.textoMotivoAnulacion("Por anulación de la solicitud");
                else if (IB.vars.ta207_estado == "FS")
                    view.textoMotivoAnulacion("Por finalización de la solicitud");
                else if (IB.vars.ta207_estado == "XA")
                    view.textoMotivoAnulacion("Por anulación de la acción");
                else if (IB.vars.ta207_estado == "FA")
                    view.textoMotivoAnulacion("Por finalización de la acción");
                else
                    view.dom.textareaMotivoAnulacion.text();
            }
            else if (IB.vars.ta204_estado == "X" || IB.vars.ta204_estado == "F" || IB.vars.ta204_estado == "FS" || IB.vars.ta204_estado == "XS") {
                view.manejarObjetosDOM.anuladasEstadoEdicion();
            }

            //Tarea Finalizada
            else {
                view.manejarObjetosDOM.selloEstadoFinalizado();
            }
        }

        else {
            //Estado de la tarea Abierta
            if (IB.vars.modoPantalla == "E") {
                //Soy participante
                if (IB.vars.estadoParticipacion == "A") {
                    //soy participante y el estado es "en curso"
                    //activamos el campo comentarios del participante, el botón grabar, anular y finalizar participación.
                    view.manejarObjetosDOM.soyParticipante();
                    _modoContainerDocsAccion = "C";
                    _modoContainerDocsTarea = "E";
                }

                else if (IB.vars.estadoParticipacion == "F" || IB.vars.estadoParticipacion == "X" || IB.vars.estadoParticipacion == "XS" || IB.vars.estadoParticipacion == "FS" || IB.vars.estadoParticipacion == "FA" || IB.vars.estadoParticipacion == "XA" || IB.vars.estadoParticipacion == "XT" || IB.vars.estadoParticipacion == "FT") {
                    //soy ejecutor de una participación finalizada o anulada de una tarea abierta                        
                    _modoContainerDocsAccion = "C";
                    _modoContainerDocsTarea = "C";
                }

            }

            if (_oPerfilesEdicion.soyFiguraArea && IB.vars.modoPantalla == "E") {
                _modoContainerDocsAccion = "C";
                _modoContainerDocsTarea = "C";
            }

            if (_oPerfilesEdicion.soyLider || _oPerfilesEdicion.soyFiguraSubareaActual || _oPerfilesEdicion.soyAdministrador) {
                view.manejarObjetosDOM.controlTotal();

                if (IB.vars.modoPantalla == "A")
                    view.manejarObjetosDOM.controlTotalAlta();
                else if (IB.vars.modoPantalla == "E")
                    view.manejarObjetosDOM.controlTotalEdicion();

                _modoContainerDocsAccion = "E";
                _modoContainerDocsTarea = "E";                
            }

        }
        //Llamamos al módulo documentación
        initDocumentacion();
    }

    //Eventos
    view.attachEvents("click", view.dom.btnRegresar, btnRegresar_onClick);
    view.attachLiveEvents("click", view.dom.fk_ltrParticipacionProfesional, view.fk_ParticipacionProfesional_onClick);
    view.attachEvents("click", view.dom.btnFinalizarParticipacion, btnFinalizarParticipacion_onClick);
    view.attachEvents("click", view.dom.btnAnularParticipacion, btnAnularParticipacion_onClick);
    view.attachEvents("click", view.dom.btnFinalizarTarea, btnFinalizarTarea_onClick);
    view.attachEvents("click", view.dom.btnAnularTarea, btnAnularTarea_onClick);
    view.attachEvents("click", view.dom.btnAceptar_anularTarea, btnAceptar_anularTarea_onClick);
    view.attachEvents("click", view.dom.linkDocumentacion, linkDocumentacion_onClick);
    view.attachEvents("click", view.dom.linkInformacionAdicional, linkInformacionAdicional_onClick);
    view.attachLiveEvents("click", view.dom.btnltrAceptarModalEstadoPartipacion, view.btnAceptarModalEstadoPartipacion_onClick);
    view.attachEvents("click", view.dom.lblParticipantes, view.lblParticipantes_onClick);
    view.attachEvents("click", view.dom.btnGrabar, btnGrabar_onClick);
    view.attachEvents("keyup", view.dom.textareaMotivoAnulacion, contabilizarCaracteresMotivoAnulacion_onKeyUp);
    view.attachEvents("click", view.dom.btnCancelar_anularTarea, btnCancelar_anularTarea_onClick);

    view.dom.selectDenominacion.on("change", function () {
        view.opcionesCombo();
        view.dom.ta207_denominacion.val("");
    })
    //FIN Eventos

    function btnRegresar_onClick() {        
        regresar();
    }

    //Finalizar la participación en la tarea
    function btnFinalizarParticipacion_onClick() {

        //bcambios = false;

        //validaciones de campos obligatorios
        if (!view.requiredValidation()) return;

        $.when(IB.bsconfirm.confirm("primary", "Finalización de participación", "Has pulsado el botón 'Finalizar mi participación'. ¿Estás conforme?")).then(function () {
            actualizarParticipacion("F");
            location.href = "../catalogoParticipante/default.aspx";
        })
    }

    //Anular la participación en la tarea
    function btnAnularParticipacion_onClick() {
        
        //bcambios = false;

        //validaciones de campos obligatorios
        if (!view.requiredValidation()) return;

        $.when(IB.bsconfirm.confirm("primary", "Anulación de participación", "Has pulsado el botón 'Anular mi participación'. ¿Estás conforme?")).then(function () {
            actualizarParticipacion("X");
            location.href = "../catalogoParticipante/default.aspx";
        })
    }

    //Finalizar tarea
    function btnFinalizarTarea_onClick() {

        //bcambios = false;

        //validaciones de campos obligatorios
        if (!view.requiredValidation()) return;

        $.when(IB.bsconfirm.confirm("primary", "Finalización de tarea", "Has pulsado el botón 'Finalizar tarea'. ¿Estás conforme?")).then(function () {
            _estado = "F";
            actualizarEstadoTarea("F");
            //regresar();
        })
    }

    //Anular tarea 
    //Inicializa la modal anular tarea
    function btnAnularTarea_onClick() {

        //bcambios = false;

        //validaciones de campos obligatorios
        if (!view.requiredValidation()) return;

        view.initModal_AnularTarea();
    }

    function btnCancelar_anularTarea_onClick() {
        view.clearMotivoAnulacion();
    }

    //Anula la tarea
    function btnAceptar_anularTarea_onClick() {        
        if (view.dom.textareaMotivoAnulacion.val() == "") {
            IB.bsalert.toast("Debes introducir un motivo de anulación.", "warning");
            return false;
        }

        _estado = "X";
        actualizarEstadoTarea("X");
        
        regresar();
    }

    function obtenerInfoSuper() {

        var payload = { ta206_iditemorigen: _ta206_iditemorigen }
        IB.DAL.post(null, "ObtenerDatosSUPER", payload, null, view.manejarObjetosDOM.pintaInfoCRM);
    }

    function obtenerInfoCRM() {_ta206_iditemorigen
        var payload = { ta206_itemorigen: _ta206_itemorigen, ta206_iditemorigen: _ta206_iditemorigen }
        IB.DAL.post(IB.vars.strserver + "Capa_Presentacion/SIC/Services/CRM.asmx", "ObtenerDatosCRM", payload, null, view.manejarObjetosDOM.pintaInfoCRM);
    }

    function initDocumentacion() {

        //Si está en modo edición se le pasa al módulo documentos el idtarea
        if (IB.vars.modoPantalla == "E")
            _idDoc = IB.vars.ta207_idtareapreventa;
            //Si es modo Alta se le pasa un guid
        else
            _idDoc = IB.vars.uidDocumento

        $.when(SUPER.SIC.appDocumentos.initTarea(_idDoc, _modoContainerDocsAccion, _modoContainerDocsTarea, IB.vars.perfilesEdicion.idficepi, _oPerfilesEdicion.soySuperEditor))
           .then(function () {
               _docAppLoaded.resolve();
               $.when(SUPER.SIC.appDocumentos.count()).then(function (data) { view.setContadorDocumentos(data); });
               SUPER.SIC.appDocumentos.onClose(function (data) { view.setContadorDocumentos(data) });
           });
    }

    //Grabar
    function btnGrabar_onClick() {

        //validaciones de campos obligatorios
        if (!view.requiredValidation()) return;

        //Devuelve un objeto con los valores de los campos de pantalla
        oTarea = view.getViewValues();

        //Obtenemos los participantes del Datatable
        var lista = view.manejarObjetosDOM.obtenerParticipantes();

        IB.procesando.mostrar();
        oTarea.ta207_estado = _estado;
        var payload = { oTarea: oTarea, listaParticipantes: lista, modoPantalla: IB.vars.modoPantalla, estadoParticipacion: IB.vars.estadoParticipacion, oPerfilesEdicion: _oPerfilesEdicion }

        IB.DAL.post(null, "grabarTarea", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    bcambios = false;
                    switch (_estado) {
                        case "F":
                            IB.bsalert.toast("Grabación correcta.");
                            regresar();                            
                            return;
                            break;

                        case "X":
                            IB.bsalert.toast("Grabación correcta.");
                            regresar();
                            return;
                            break;

                        default:
                            IB.bsalert.toast("Grabación correcta.");
                            view.manejarObjetosDOM.deshabilitarbtnGrabar();

                            //Los participantes se graban a estado "E"
                            view.grabarEstadoParticipantes();

                            //si es una alta... debemos cambiar al modo edición. Visualizar campos que están ocultos.
                            if (IB.vars.modoPantalla == "A") {
                                IB.vars.modoPantalla = "E";
                                IB.vars.ta207_idtareapreventa = data;
                                view.setFechaCreacion();
                                view.actualizar_data_estado_participantes();


                                //Quitar campos requeridos
                                $(".requerido").removeClass("requerido");

                                if (_oPerfilesEdicion.soyLider || _oPerfilesEdicion.soyFiguraSubareaActual || _oPerfilesEdicion.soyAdministrador) {
                                    view.setIdTarea(data)
                                    view.manejarObjetosDOM.controlTotalEdicion();
                                    //view.manejarObjetosDOM.controlBotonesTarea();
                                }
                                else {
                                    view.switchModoPantallaEdicion();
                                }

                                //view.switchModoPantallaEdicion();

                                initDocumentacion();
                                _estado = "A";
                                view.setSello("A");
                                $("#ta207_fechafinreal").css("visibility", "hidden");
                            }
                            //si el usuario conectado finaliza o anula una participación de el mismo, deshabilitar los campos del participante
                            var _estadoActual = view.getEstadoParticipacion_usuarioConectado();
                            if (_estadoActual == "F" || _estadoActual == "X" || typeof (_estadoActual) == "undefined") {
                                view.manejarObjetosDOM.quitarParticipante();
                                view.manejarObjetosDOM.controlBotonesTarea();
                            }

                            //si la pone en curso, habilitamos los campos del participante
                            if (_estadoActual == "A")
                                view.manejarObjetosDOM.soyParticipante();
                            //Si soy líder, figura subárea o administrador, control total
                            if (_oPerfilesEdicion.soyLider || _oPerfilesEdicion.soyFiguraSubareaActual || _oPerfilesEdicion.soyAdministrador)
                                view.manejarObjetosDOM.controlTotal();


                            if (IB.vars.modoPantalla === "E") {
                                //Bloquear combo tipos de tarea y denominación
                                view.TiposTareaBloquear();
                            }

                            break;
                    }

                })
            }
        );

    }

    //Contabiliza el número de caracteres escritos en el textarea motivo de anulación de tarea
    function contabilizarCaracteresMotivoAnulacion_onKeyUp() {
        var _max = 250;
        view.dom.numCaracteres.text(_max + ' caracteres disponibles');

        var len = $(this).val().length;
        if (len >= _max) {
            view.dom.numCaracteres.text('Límite alcanzado');
            view.dom.numCaracteres.addClass('text-danger');

            /*Una vez alcanzado el límite, sólo permitimos el backspace*/
            if (e.keyCode !== 8) {
                e.preventDefault();
            }

        }
        else {
            var ch = _max - len;
            view.dom.numCaracteres.text(ch + ' caracteres disponibles');
            view.dom.numCaracteres.removeClass('disabled');
            view.dom.numCaracteres.removeClass('text-danger');
        }
    }

    //Actualiza la participación en la tarea
    function actualizarParticipacion(estado) {

        IB.procesando.mostrar();
        bcambios = false;

        //Redirigir según origen
        var payload = { ta207_idtareapreventa: IB.vars.ta207_idtareapreventa, estado: estado }
        IB.DAL.post(null, "actualizarParticipacion", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {                    
                    regresar();
                })
            }
        );
    }

    //Actualiza el estado de la tarea
    function actualizarEstadoTarea(estado) {
        bcambios = false;
        _estado = estado;
        btnGrabar_onClick();
    }

    function linkDocumentacion_onClick() {
        $.when(_docAppLoaded).then(SUPER.SIC.appDocumentos.show);
    }

    function linkInformacionAdicional_onClick() {
        view.plegar_desplegar();
    }


    function regresar() {

        var payload = {
            urlActual: location.href
        }

        if (bcambios) {
            $.when(IB.bsconfirm.confirmCambios())
                .then(function () {
                    bcambios = false;
                    IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Leer", payload, null,
                        function (data) {
                            if (data != "") window.location.href = data;
                        });
                })
        }
        else {
            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Leer", payload, null,
                function (data) {
                    if (data != "") window.location.href = data;
                });
        }


    }

})(SUPER.SIC.view);