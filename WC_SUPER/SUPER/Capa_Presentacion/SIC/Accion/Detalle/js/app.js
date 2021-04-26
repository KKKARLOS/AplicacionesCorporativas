/// <reference path="../../../../../scripts/IB.js" />


$(document).ready(function () { SUPER.SIC.appAccion.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appAccion = (function (view) {

    var _modo = IB.vars.modo; //A=Alta; E=Edición; C=Consulta
    var _origenpantalla = IB.vars.origenpantalla; //CRM; SUPER
    var _id = IB.vars.ta204_idaccionpreventa;
    var _oAccion = IB.vars.oAccion;
    var _caller = IB.vars.caller;
    var _docAppLoaded = $.Deferred();
    var _oPerfilesEdicion = IB.vars.perfilesEdicion;
    var cmbEstructBloqueados;  //Indica el estado de los combos de estructura para acceder al ws de lista generérico o listaestructura
    var bcambios = false;
    var _t001_idficepi_lider_ant = null;
    var _liderActivo = false;
    var _ta206_estado = IB.vars.ta206_estado;

    var structToSelect = undefined; //para seleccionar valores en los combos de estructura en la carga de la pantalla en modo E

    //Controlar cuando finalizan de cargar los combos en modo "E" o ("A" con _origenpantalla="SUPER") para poner el bcambios = false
    var screenLoaded = {
        ready: false,
        areasLoaded: false,
        subareasLoaded: false,
        tiposAccionLoaded: false
    }
    var CFSL; //token del setInterval;

    function init() {

        console.log("modo=" + _modo);
        console.log("origenpantalla=" + _origenpantalla);
        console.log("id=" + _id);

        console.log("appAccion.init");

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            view.manejarcampos.desactivarcampostodos();
            view.manejarcampos.desactivarenlacestodos();
            view.manejarcampos.desactivarbotonestodos();
            view.attachEvents("click", view.dom.btnCerrar, btnCerrar_onClick);
            return;
        }

        window.onbeforeunload = function () {
            if (_modo != "C" && bcambios) return 'Hay cambios sin guardar. Si continúas perderás dichos cambios.';
        };

        //atachar eventos
        view.attachEvents("change", view.dom.cmbUnidad, cmbUnidad_onChange);
        view.attachEvents("change", view.dom.cmbArea, cmbArea_onChange);
        view.attachEvents("change", view.dom.cmbSubarea, cmbSubarea_onChange);
        view.attachEvents("change", view.dom.cmbTipoAccion, cmbTipoAccion_onChange);

        view.attachEvents("click", view.dom.btnTareas, btnTareas_onClick);
        view.attachEvents("click", view.dom.btnDocumentacion, btnDocumentacion_onClick);

        view.attachEvents("click", view.dom.btnGrabar, btnGrabar_onClick);
        view.attachEvents("click", view.dom.btnCerrar, btnCerrar_onClick);

        view.attachEvents("click", view.dom.btnFinalizarAccion, btnFinalizarAccion_onClick);
        view.attachEvents("click", view.dom.btnAnularAccion, btnAnularAccion_onClick);
        view.attachEvents("click", view.dom.btnAceptar_anularAccion, btnAceptar_anularAccion_onClick);
        view.attachEvents("click", view.dom.btnAceptar_finalizarAccion, btnAceptar_finalizarAccion_onClick);

        view.attachEvents("click", view.dom.btnReplicarAccion, btnReplicarAccion_onClick);
        view.attachEvents("change", view.dom.cmbTipoAccion_RA, cmbTipoAccion_RA_onChange);
        view.attachEvents("click", view.dom.btnAceptar_RA, btnAceptar_RA_onClick);
        view.attachEvents("click", view.dom.btnAutoAsignar, btnAutoAsignar_onClick);

        //gestion del bcambios
        var camposEditables = view.dom.divAccionContainer.find("select, textarea, input[type='text']");
        view.attachEvents("change", camposEditables, function () {
            console.log("onChange(" + $(this).prop("id") + ")");
            bcambios = true;
        });


        view.init(IB.vars.itemorigen, IB.vars.iditemorigen);

        if (IB.vars.itemorigen == "S")
            obtenerInfoSuper();
        else
            obtenerInfoCRM();


        //* MODULO DE DOCUMENTACION *//
        $.when(SUPER.SIC.appDocumentos.initAccion(_id, "C", "C", IB.vars.idficepi, false))
            .then(function () {
                $.when(SUPER.SIC.appDocumentos.count()).then(function (data) { view.setDocumentosCount(data) });
                _docAppLoaded.resolve();
            });
        SUPER.SIC.appDocumentos.onClose(function (data) { view.setDocumentosCount(data) });
        //* MODULO DE DOCUMENTACION *//

        var _dsa = $.Deferred();
        IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/SIC/Services/Listas.asmx", "ObtenerSubareas", null, null,
            function (data) {
                subareas.init(data); //inicializar array global de subareas
                _dsa.resolve();
            });


        $.when(_docAppLoaded, _dsa).then(gestionaPantalla);
    }


    function gestionaPantalla() {

        view.manejarcampos.desactivarcampostodos();
        view.manejarcampos.desactivarenlacestodos();
        view.manejarcampos.desactivarbotonestodos();

        switch (_modo) {
            case "A":
                console.log("gestionaPantalla en modo A");

                if (IB.vars.itemorigen == "S") {
                    view.manejarcampos.activarcampos(
                        [view.dom.cmbTipoAccion,
                         view.dom.cmbSubarea,
                         view.dom.txtDenominacion,
                         view.dom.txtObservaciones,
                         view.dom.dteFFE]
                    );
                }
                else {
                    view.manejarcampos.activarcampostodos();
                }

                view.manejarcampos.activarenlaces([view.dom.btnDocumentacion]);
                view.manejarcampos.activarbotones([view.dom.btnGrabar]);

                if (_origenpantalla == "SUPER") { //alta desde SUPER
                    view.manejarcampos.activarenlaces([view.dom.btnAyudaLider]);
                    view.manejarcampos.mostrarenlaces([view.dom.btnOtrosLideres]);
                    _liderActivo = true;
                }

                SUPER.SIC.appDocumentos.initAccion(_id, "E", "C", IB.vars.idficepi, false);

                _cmbEstructBloqueados = view.manejarcampos.getCmbEstructBloquedados();

                agregarAccion();

                break;

            case "E":
                console.log("gestionaPantalla en modo E");

                if (_origenpantalla == "CRM") {

                    view.manejarcampos.activarcampos(
                        [view.dom.txtDenominacion,
                         view.dom.txtObservaciones,
                         view.dom.dteFFE]
                    );

                    view.manejarcampos.activarenlaces(
                        [view.dom.btnDocumentacion]
                    );
                    if (_oAccion.t001_idficepi_lider != null && _oAccion.t001_idficepi_lider != 0) view.manejarcampos.activarenlaces([view.dom.btnTareas]);


                    view.manejarcampos.activarbotones(
                        [view.dom.btnGrabar,
                         view.dom.btnAnularAccion]
                    );

                    if (_oPerfilesEdicion.soyLider) {
                        view.manejarcampos.activarbotones([view.dom.btnFinalizarAccion]);
                        SUPER.SIC.appDocumentos.initAccion(_id, "E", "E", IB.vars.idficepi, true);
                    }
                    else {
                        SUPER.SIC.appDocumentos.initAccion(_id, "E", "C", IB.vars.idficepi, false);
                    }
                }
                else { //origenpantalla = "SUPER"

                    if (_caller == "autoasignacion") {

                        if (_oPerfilesEdicion.soyPosibleLider && _oAccion.t001_idficepi_lider == null) {

                            view.manejarcampos.activarenlaces([view.dom.btnDocumentacion]);

                            view.manejarcampos.activarbotones([view.dom.btnAutoAsignar]);

                            SUPER.SIC.appDocumentos.initAccion(_id, "C", "C", IB.vars.idficepi, false);

                        }
                        else if (!_oPerfilesEdicion.soyPosibleLider) {
                            $.when(IB.bsalert.fixedAlert("warning", "Autoasignación de lider", "No dispones del perfil necesario para autoasignarte lider en esta acción.")).then(regresar);
                        }
                        else if (_oAccion.t001_idficepi_lider != null) {
                            $.when(IB.bsalert.fixedAlert("warning", "Autoasignación de lider", "La acción ya tiene lider asignado.")).then(regresar);
                        }
                    }
                    else {
                        if (_oPerfilesEdicion.soyAdministrador) {

                            if (IB.vars.itemorigen != "S" && _oAccion.ta199_idunidadpreventa > 0) {
                                view.manejarcampos.activarcampos([view.dom.cmbUnidad, view.dom.cmbArea]);
                            }

                            view.manejarcampos.activarcampos(
                                [view.dom.txtDenominacion,
                                view.dom.txtObservaciones,
                                view.dom.dteFFE,
                                view.dom.cmbSubarea]
                            );

                            view.manejarcampos.activarenlaces(
                                [view.dom.btnDocumentacion,
                                 view.dom.btnAyudaLider]
                            );

                            _liderActivo = true;

                            if (_oAccion.t001_idficepi_lider != null && _oAccion.t001_idficepi_lider != 0) view.manejarcampos.activarenlaces([view.dom.btnTareas]);

                            view.manejarcampos.mostrarenlaces([view.dom.btnOtrosLideres]);

                            view.manejarcampos.activarbotones(
                                [view.dom.btnGrabar,
                                 view.dom.btnFinalizarAccion,
                                 view.dom.btnAnularAccion,
                                 view.dom.btnReplicarAccion]
                            );

                            SUPER.SIC.appDocumentos.initAccion(_id, "E", "E", IB.vars.idficepi, true);


                        }
                        else if (_oPerfilesEdicion.soyFiguraSubareaActual) {

                            if (IB.vars.itemorigen != "S" && _oAccion.ta199_idunidadpreventa > 0) {
                                view.manejarcampos.activarcampos([view.dom.cmbUnidad, view.dom.cmbArea]);
                            }


                            view.manejarcampos.activarcampos(
                                [view.dom.txtDenominacion,
                                view.dom.txtObservaciones,
                                view.dom.dteFFE,
                                view.dom.cmbSubarea]
                            );

                            view.manejarcampos.activarenlaces(
                                [view.dom.btnDocumentacion,
                                 view.dom.btnAyudaLider]
                            );
                            _liderActivo = true;

                            if (_oAccion.t001_idficepi_lider != null && _oAccion.t001_idficepi_lider != 0) view.manejarcampos.activarenlaces([view.dom.btnTareas]);

                            view.manejarcampos.mostrarenlaces([view.dom.btnOtrosLideres]);

                            view.manejarcampos.activarbotones(
                                [view.dom.btnGrabar,
                                 view.dom.btnFinalizarAccion,
                                 view.dom.btnAnularAccion,
                                 view.dom.btnReplicarAccion]
                            );

                            SUPER.SIC.appDocumentos.initAccion(_id, "E", "E", IB.vars.idficepi, true);

                        }
                        else if (_oPerfilesEdicion.soyLider) {

                            //view.manejarcampos.activarcampos(
                            //    [view.dom.txtDenominacion,
                            //    view.dom.txtObservaciones,
                            //    view.dom.dteFFE]
                            //);

                            view.manejarcampos.activarenlaces(
                                [view.dom.btnDocumentacion,
                                 view.dom.btnTareas,
                                 view.dom.btnAyudaLider]
                            );
                            _liderActivo = true;
                            view.manejarcampos.mostrarenlaces([view.dom.btnOtrosLideres]);

                            view.manejarcampos.activarbotones(
                                [view.dom.btnGrabar,
                                 view.dom.btnFinalizarAccion,
                                 view.dom.btnAnularAccion,
                                 view.dom.btnReplicarAccion]
                            );

                            SUPER.SIC.appDocumentos.initAccion(_id, "E", "E", IB.vars.idficepi, true);


                        }
                        else if (_oPerfilesEdicion.soyFiguraAreaActual) { //responsable, delegado o colaborador de area

                            view.manejarcampos.activarenlaces(
                                [view.dom.btnDocumentacion]
                            );
                            if (_oAccion.t001_idficepi_lider != null && _oAccion.t001_idficepi_lider != 0) view.manejarcampos.activarenlaces([view.dom.btnTareas]);


                            SUPER.SIC.appDocumentos.initAccion(_id, "C", "C", IB.vars.idficepi, false);

                        }
                        else {
                            //Esto no debería de ocurrir. La pantalla no contempla que un usuario sin permisos entre a una acción abierta. --> navegar a la anterior
                            console.log("Tu perfil de usuario no tiene acceso a la acción solicitada.");
                            regresar();

                        }
                    }


                }

                _cmbEstructBloqueados = view.manejarcampos.getCmbEstructBloquedados();

                editarAccion()

                break;

            case "C":
                console.log("gestionaPantalla en modo C");
                var msg = "";

                switch (_oAccion.ta204_estado) {
                    case "F":
                        break;
                    case "X":
                        msg = _oAccion.ta204_motivoanulacion;
                        break;
                    case "XS":
                        msg = "Por anulación de la solicitud";
                        break;
                    case "FS":
                        msg = "Por finalización de la solicitud";
                        break;
                }

                if (msg.length > 0) {
                    view.manejarcampos.mostrarMotivoAnulacion(msg);
                }

                SUPER.SIC.appDocumentos.initAccion(_id, "C", "C", IB.vars.idficepi, false);

                view.manejarcampos.activarenlaces(
                    [view.dom.btnDocumentacion,
                     view.dom.btnTareas]
                );

                if (_origenpantalla == "SUPER" && _ta206_estado == "A" && (_oPerfilesEdicion.soyAdministrador || _oPerfilesEdicion.soyFiguraSubareaActual || _oPerfilesEdicion.soyLider)) { //20180118 Permitir replicar acción si solicitud está abierta y soy admin, figura del subarea o el lider
                    view.manejarcampos.activarbotones([view.dom.btnReplicarAccion]);
                }

                _cmbEstructBloqueados = view.manejarcampos.getCmbEstructBloquedados();

                consultarAccion();

                break;
        }

    }

    function agregarAccion() {

        console.log("agregarAccion()");

        loadTiposAccion();

        if (IB.vars.itemorigen == "S") { //Establecer valor en unidad y area y bloquear combos
            //Para seleccionar los valores en los combos de estructura una vez cargados
            structToSelect = {
                ta199_idunidadpreventa: IB.vars.ta199_idunidadpreventa,
                ta200_idareapreventa: IB.vars.ta200_idareapreventa,
            }
            loadUnidades(structToSelect.ta199_idunidadpreventa);

            screenLoaded.ready = false;
            screenLoaded.areasLoaded = false;
            screenLoaded.tiposAccionLoaded = false;
            CFSL = window.setInterval(checkForScreenLoadedAreas, 500);
        }
        else { //Al venir del CRM --> libre elección en estructura
            screenLoaded.ready = true;
            loadUnidades();
        }

        view.renderAdd();
    }


    function editarAccion() {

        console.log("editarAccion");

        //Para seleccionar los valores en los combos de estructura una vez cargados
        structToSelect = {
            ta199_idunidadpreventa: _oAccion.ta199_idunidadpreventa,
            ta200_idareapreventa: _oAccion.ta200_idareapreventa,
            ta201_idsubareapreventa: _oAccion.ta201_idsubareapreventa
        }

        loadTiposAccion(_oAccion.ta205_idtipoaccionpreventa);
        loadUnidades(structToSelect.ta199_idunidadpreventa);

        if (structToSelect.ta199_idunidadpreventa <= 0) {
            console.log("Ocultar link selección de lider");
            view.manejarcampos.desactivarenlaces([view.dom.btnAyudaLider]);
            view.manejarcampos.desactivarenlaces2([view.dom.btnOtrosLideres]);
        }
        view.renderEdit(_oAccion);

        screenLoaded.ready = false;
        screenLoaded.subareasLoaded = false;
        screenLoaded.tiposAccionLoaded = false;
        CFSL = window.setInterval(checkForScreenLoadedSubareas, 500);
    }

    //Cuando se cargan los combos de subarea y tipos de accion --> ha finalizado la carga de la pantalla en modo "E" --> bcambios = false;
    function checkForScreenLoadedAreas() {
        if (screenLoaded.areasLoaded && screenLoaded.tiposAccionLoaded) {
            screenLoaded.ready = true;
            window.clearInterval(CFSL);
            console.log("screenLoadedAreas=true");
            bcambios = false;
        }
        else {
            console.log("screenLoadedAreas=false");
        }
    }

    //Cuando se cargan los combos de subarea y tipos de accion --> ha finalizado la carga de la pantalla en modo "E" --> bcambios = false;
    function checkForScreenLoadedSubareas() {
        if (screenLoaded.subareasLoaded && screenLoaded.tiposAccionLoaded) {
            screenLoaded.ready = true;
            window.clearInterval(CFSL);
            console.log("screenLoadedSubareas=true");
            bcambios = false;
        }
        else {
            console.log("screenLoadedSubareas=false");
        }
    }

    function consultarAccion() {

        console.log("consultarAccion");

        //Para seleccionar los valores en los combos de estructura una vez cargados
        structToSelect = {
            ta199_idunidadpreventa: _oAccion.ta199_idunidadpreventa,
            ta200_idareapreventa: _oAccion.ta200_idareapreventa,
            ta201_idsubareapreventa: _oAccion.ta201_idsubareapreventa
        }

        loadTiposAccion(_oAccion.ta205_idtipoaccionpreventa);
        loadUnidades(structToSelect.ta199_idunidadpreventa);

        view.renderEdit(_oAccion);

        screenLoaded.ready = false;
        screenLoaded.subareasLoaded = false;
        screenLoaded.tiposAccionLoaded = false;
        CFSL = window.setInterval(checkForScreenLoadedSubareas, 500);

    }

    function btnTareas_onClick() {

        if ($(this).hasClass("linkdisabled")) return; //está desactivado

        var urlTareas = "../../Tarea/CatalogoPorAccion/Default.aspx?" + IB.uri.encode("id=" + _oAccion.ta204_idaccionpreventa + "&origenpantalla=" + _origenpantalla);

        if (_modo != "C" && bcambios) {
            $.when(IB.bsconfirm.confirmCambios())
                .then(function () {
                    bcambios = false;
                    window.location.href = urlTareas;
                })
        }
        else {
            window.location.href = urlTareas;
        }


    }

    function btnDocumentacion_onClick() {

        if ($(this).hasClass("linkdisabled")) return; //está desactivado

        $.when(_docAppLoaded).then(SUPER.SIC.appDocumentos.show);
    }


    //*** BOTONERA INFERIOR ***//
    function btnGrabar_onClick() {

        var defer = $.Deferred();

        if (!view.requiredValidation(view.dom.divAccionContainer)) {
            return defer;
        }

        var oAccion = view.getViewValues();

        //Validaciones adicionales aqui...
        //La fechaFFE en modo alta debe ser igual o post
        if (_modo == "A") {
            oAccion.ta204_fechacreacion = new Date();
            oAccion.ta204_fechacreacion.setHours(0, 0, 0, 0);
            if (oAccion.ta204_fechafinestipulada < oAccion.ta204_fechacreacion) {
                IB.bsalert.toastdanger("La fecha de finalización requerida no puede ser anterior al día de hoy");
                view.dom.dteFFE.focus();
                return defer;
            }
        }
        else if (_modo == "E") { // En modo edición igual o posterior a la fecha de creación de la acción
            oAccion.ta204_fechacreacion = new Date(_oAccion.ta204_fechacreacion);
            oAccion.ta204_fechacreacion.setHours(0, 0, 0, 0);
            if (oAccion.ta204_fechafinestipulada < oAccion.ta204_fechacreacion) {
                IB.bsalert.toastdanger("La fecha de finalización requerida no puede ser anterior a la fecha de creación de la acción (" + moment(oAccion.ta204_fechacreacion).format('DD/MM/YYYY') + ")");
                view.dom.dteFFE.focus();
                return defer;
            }
        }

        var ta205_plazominreq = parseInt(view.getComboSelectedOptionAttr(view.dom.cmbTipoAccion, "ta205_plazominreq"));
        var d = new Date(oAccion.ta204_fechacreacion);
        d.setDate(d.getDate() + ta205_plazominreq);

        var d1 = $.Deferred();
        if (oAccion.ta204_fechafinestipulada < d) {
            var msg = "La acción preventa elegida tiene un plazo mínimo requerido que no se ha respetado. Si continúas con la grabación, deberás ponerte en contacto con " + subareas.getSubarea(oAccion.ta201_idsubareapreventa).responsable + ", responsable del subárea seleccionada, para explicarle el motivo de la petición.<br/><br/>¿Quieres continuar?";
            $.when(IB.bsconfirm.confirm("warning", "Grabar acción", msg)).then(function () {
                d1.resolve();
            })
        }
        else {
            d1.resolve();
        }

        $.when(d1).then(function () {
            var oSolicitud = {
                iditemorigen: IB.vars.iditemorigen,
                itemorigen: IB.vars.itemorigen
            }

            var payload;
            var methodName;


            if (_modo == "A") {
                payload = { oAccion: oAccion, oSolAux: oSolicitud, guidprovisional: _id }
                methodName = "GrabarAccionAlta";
                oAccion.ta204_estado = "A";
            }
            else {
                oAccion.ta204_idaccionpreventa = _id;
                oAccion.ta204_estado = _oAccion.ta204_estado;
                payload = { oAccion: oAccion };
                methodName = "GrabarAccionEdicion";
            }

            IB.procesando.mostrar();

            IB.DAL.post(null, methodName, payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        IB.bsalert.toast("Grabación correcta.");
                        bcambios = false;
                        if (_modo == "A") {
                            _id = data //al grabar el alta se obtiene el idaccion y se pone la pantalla en modo edición.
                            oAccion.ta204_idaccionpreventa = _id;

                            //reemplazar url en el historial
                            var params = IB.uri.encode("modo=E&id=" + _id + "&itemorigen=" + IB.vars.itemorigen + "&iditemorigen=" + +IB.vars.iditemorigen + "&origenpantalla=" + IB.vars.origenpantalla);
                            var newurl = location.href.substr(0, location.href.indexOf("?")) + "?" + params;
                            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl });

                        }
                        _oAccion = $.extend({}, oAccion);

                        //Recalcular los perfiles del usuario
                        $.when(recalcularPerfilesUsuario()).then(function () {
                            _modo = "E";
                            gestionaPantalla();
                            defer.resolve();
                        })
                    });
                }
            );
        });

        return defer.promise();
    }

    function btnCerrar_onClick() {

        regresar();
    }

    function btnFinalizarAccion_onClick() {

        if (!view.requiredValidation(view.dom.divAccionContainer)) return;

        view.initModal_finalizarAccion();
    }

    function btnAceptar_finalizarAccion_onClick() {
        var oAccion = view.getViewValues();
        oAccion.ta204_idaccionpreventa = _id;
        oAccion.ta204_estado = "F";
        payload = { oAccion: oAccion };

        IB.procesando.mostrar();

        IB.DAL.post(null, "GrabarAccionEdicion", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    IB.bsalert.toast("Grabación correcta.");
                    bcambios = false;
                    setTimeout(regresar, 500);
                });
            }

        );
    }

    function btnAnularAccion_onClick() {

        if (!view.requiredValidation(view.dom.divAccionContainer)) return;

        view.initModal_AnularAccion();
    }

    function btnAceptar_anularAccion_onClick() {

        if (!view.requiredValidation(view.dom.modal_anularAccion)) return;

        var motivo = view.getModalMotivoAnulacion();
        //if (motivo.length == 0) {
        //    IB.bsalert.toastdanger("Debes introducir el motivo de anulación.");
        //    return;
        //}

        var oAccion = view.getViewValues();
        oAccion.ta204_idaccionpreventa = _id;
        oAccion.ta204_estado = "X";
        oAccion.ta204_motivoanulacion = motivo;
        payload = { oAccion: oAccion };

        IB.procesando.mostrar();

        IB.DAL.post(null, "GrabarAccionEdicion", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    view.closeModal_AnularAccion();
                    IB.bsalert.toast("Grabación correcta.");
                    bcambios = false;
                    setTimeout(regresar, 500);
                });
            }

        );
    }

    function btnReplicarAccion_onClick() {

        if (!view.requiredValidation(view.dom.divAccionContainer)) return;

        if (bcambios) {
            $.when(IB.bsconfirm.confirm("warning", "Cambios sin guardar",
            "Para crear una acción hermana primero debes guardar los cambios de la acción actual.<br/><br/>¿Quieres guardar los cambios de la acción actual?"))
                .then(function () {
                    $.when(btnGrabar_onClick()).then(function () {
                        loadTiposAccion_RA();
                        view.initModal_ReplicarAccion();
                    });
                });
        }
        else {
            loadTiposAccion_RA();
            view.initModal_ReplicarAccion();
        }
    }

    function btnAceptar_RA_onClick() {

        if (!view.requiredValidation(view.dom.modal_replicarAccion)) return;

        var oAccion = view.getViewValues();
        var RAvalues = view.getModalReplicarAccionValues();
        console.log(RAvalues);

        //validaciones
        var hoy = new Date();
        hoy.setHours(0, 0, 0, 0);
        if (RAvalues.fecha < hoy) {
            IB.bsalert.toastdanger("La fecha fin estipulada no puede ser anterior al día de hoy");
            view.dom.dteFFE_RA.focus();
            return;
        }

        var ta205_plazominreq = parseInt(view.getComboSelectedOptionAttr(view.dom.cmbTipoAccion, "ta205_plazominreq"));
        var d = new Date()
        d.setDate(d.getDate() + ta205_plazominreq);

        var d1 = $.Deferred();
        if (RAvalues.fecha < d) {
            var msg = "La acción preventa elegida tiene un plazo mínimo requerido que no se ha respetado. Si continúas con la grabación, deberás ponerte en contacto con " + subareas.getSubarea(oAccion.ta201_idsubareapreventa).responsable + ", responsable del subárea seleccionada, para explicarle el motivo de la petición.<br/><br/>¿Quieres continuar?";
            $.when(IB.bsconfirm.confirm("warning", "Grabar acción hermana", msg)).then(function () {
                d1.resolve();
            })
        }
        else {
            d1.resolve();
        }


        $.when(d1).then(function () {
            //grabar
            var oSolicitud = {
                iditemorigen: IB.vars.iditemorigen,
                itemorigen: IB.vars.itemorigen
            }

            //Obtener los valores de la accion principal y sobreescribir con los de la hermana.

            oAccion.ta204_descripcion = RAvalues.descripcion;
            oAccion.ta204_observaciones = RAvalues.observaciones;
            oAccion.ta205_idtipoaccionpreventa = RAvalues.tipoaccion;
            oAccion.ta204_fechafinestipulada = RAvalues.fecha;

            var payload = { oAccion: oAccion, oSolAux: oSolicitud, guidprovisional: "" }

            IB.procesando.mostrar();

            IB.DAL.post(null, "GrabarAccionAlta", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        IB.bsalert.toast("Grabación correcta.");
                        view.closeModal_ReplicarAccion();
                    });
                }
            );
        })

    }

    function btnAutoAsignar_onClick() {

        IB.procesando.mostrar();

        var payload = { ta204_idaccionpreventa: _id };
        IB.DAL.post(null, "AutoasignarLider", payload, null,
            function () {
                $.when(IB.procesando.ocultar()).then(function () {
                    IB.bsalert.toast("Grabación correcta.");
                    bcambios = false;
                    _caller = "";

                    _oAccion.t001_idficepi_lider = IB.vars.idficepi;
                    _oAccion.lider = IB.vars.profesional;
                    view.setLider(_oAccion.t001_idficepi_lider, _oAccion.lider);

                    var params = IB.uri.encode("modo=E&id=" + _id + "&itemorigen=" + IB.vars.itemorigen + "&iditemorigen=" + +IB.vars.iditemorigen + "&origenpantalla=" + IB.vars.origenpantalla);
                    var newurl = location.href.substr(0, location.href.indexOf("?")) + "?" + params;
                    $.when(IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }))
                        .then(recalcularPerfilesUsuario)
                        .then(gestionaPantalla);
                });
            }
        );

    }

    function regresar() {

        var payload = { urlActual: location.href };

        if (_modo != "C" && bcambios) {
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
    //*** BOTONERA INFERIOR ***//

    function recalcularPerfilesUsuario() {

        var defer = $.Deferred();

        var oAccion = view.getViewValues();
        var t001_idficepi_lider = oAccion.t001_idficepi_lider != "" ? oAccion.t001_idficepi_lider : null;

        payload = { t001_idficepi_lider: t001_idficepi_lider, ta201_idsubareapreventa: oAccion.ta201_idsubareapreventa }

        IB.DAL.post(null, "obtenerPerfilesEdicionUsuario", payload, null,
            function (data) {
                _oPerfilesEdicion.soyLider = data.soyLider;
                _oPerfilesEdicion.soySuperEditor = data.soySuperEditor;
                _oPerfilesEdicion.soyFiguraSubareaActual = data.soyFiguraSubareaActual;
                _oPerfilesEdicion.soyFiguraAreaActual = data.soyFiguraAreaActual;
                _oPerfilesEdicion.soyPosibleLider = data.soyPosibleLider;
                defer.resolve();
            }
        );

        return defer.promise();
    }

    function obtenerInfoCRM() {

        var payload = { ta206_itemorigen: IB.vars.itemorigen, ta206_iditemorigen: IB.vars.iditemorigen }
        IB.DAL.post(IB.vars.strserver + "Capa_Presentacion/SIC/Services/CRM.asmx", "ObtenerDatosCRM", payload, null, view.pintaInfoCRM);
    }

    function obtenerInfoSuper() {

        var payload = { ta206_iditemorigen: IB.vars.iditemorigen }
        IB.DAL.post(null, "ObtenerDatosSUPER", payload, null, view.pintaInfoCRM);
    }

    //* COMBOS *//
    var loadUnidades = function (selectedId) {

        console.log("loadUnidades");

        var method = "ObtenerListaEstructura";
        if (_cmbEstructBloqueados.unidad || _oPerfilesEdicion.soyAdministrador || _oPerfilesEdicion.soyComercial) method = "ObtenerLista";
        if (_modo == "A" && IB.vars.itemorigen != "S") method = "ObtenerLista"; //20180118 correo doizalvi --> altas desde super para solicitudes tipo O/E/P -> ven todas las unidades excepto la comercial
        if (_modo == "E" && selectedId > 0) method = "ObtenerLista"; //20180118 correo doizalvi --> edicion si la unidad no es "comercial" -> cargar estructura completa (excepto comercial) para permitir reasignaciones

        if (method == "ObtenerListaEstructura" && !IB.vars.origenMenu) IB.vars.origenMenu = "SIC";
        var payload = { tipo: "unidad_preventa", filtrarPor: null, origenMenu: IB.vars.origenMenu }

        return IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/SIC/Services/Listas.asmx", method,
                    payload, null,
                    function (data) {

                        //if (_modo == "E" && IB.vars.itemorigen != "S" && selectedId > 0) { //Ediciones de acciones de origen distinto super, activa como unidad (en funcion gestionapantalla) pero no permite seleccionar "comercial"
                        if (_modo == "E" && selectedId > 0) { //20180118 correo doizalvi --> edicion si la unidad no es "comercial" -> cargar estructura completa (excepto comercial) para permitir reasignaciones
                            data = data.filter(function (obj) {
                                if (obj.Key <= 0)
                                    return false;
                                else
                                    return true;
                            });
                        }

                        if (_modo == "A" && IB.vars.origenpantalla == "SUPER") //Altas de acciones desde SUPER no pueden ser de la unidad comercial
                            data = data.filter(function (obj) {
                                if (obj.Key == 0)
                                    return false;
                                else
                                    return true;
                            });

                        view.renderCombo(data, view.dom.cmbUnidad, selectedId)
                    });
    };

    var loadAreas = function (selectedId) {

        console.log("loadAreas");

        var filtrarPor = view.getComboSelectedOption(view.dom.cmbUnidad);
        if (filtrarPor == null || filtrarPor == "") return;

        var method = "ObtenerListaEstructura";
        if (_cmbEstructBloqueados.area || _oPerfilesEdicion.soyAdministrador || _oPerfilesEdicion.soyComercial) method = "ObtenerLista";
        if (_modo == "A" && IB.vars.itemorigen != "S") method = "ObtenerLista"; //20180118 correo doizalvi --> altas desde super para solicitudes tipo O/E/P -> ven todas las unidades excepto la comercial
        if (_modo == "E" && filtrarPor > 0) method = "ObtenerLista"; //20180118 correo doizalvi --> edicion si la unidad no es "comercial" -> cargar estructura completa (excepto comercial) para permitir reasignaciones

        if (method == "ObtenerListaEstructura" && !IB.vars.origenMenu) IB.vars.origenMenu = "SIC";
        var payload = { tipo: "area_preventa", filtrarPor: filtrarPor, origenMenu: IB.vars.origenMenu }
        return IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/SIC/Services/Listas.asmx", method,
                    payload, null,
                    function (data) {
                        view.renderCombo(data, view.dom.cmbArea, selectedId)
                        screenLoaded.areasLoaded = true;
                    });
    };

    var loadSubareas = function (selectedId) {

        console.log("loadSubareas");

        var filtrarPor = view.getComboSelectedOption(view.dom.cmbArea);
        if (filtrarPor == null || filtrarPor == "") return;

        var method = "ObtenerListaEstructura";
        if (_cmbEstructBloqueados.subarea || _oPerfilesEdicion.soyAdministrador || _oPerfilesEdicion.soyComercial) method = "ObtenerLista";
        if (_modo == "A" && IB.vars.itemorigen != "S") method = "ObtenerLista"; //20180118 correo doizalvi --> altas desde super para solicitudes tipo O/E/P -> ven todas las unidades excepto la comercial
        if (_modo == "E" && filtrarPor > 0) method = "ObtenerLista"; //20180118 correo doizalvi --> edicion si la unidad no es "comercial" -> cargar estructura completa (excepto comercial) para permitir reasignaciones

        if (method == "ObtenerListaEstructura" && !IB.vars.origenMenu) IB.vars.origenMenu = "SIC";
        var payload = { tipo: "subarea_preventa", filtrarPor: filtrarPor, origenMenu: IB.vars.origenMenu }
        return IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/SIC/Services/Listas.asmx", method,
                    payload, null,
                    function (data) {
                        view.renderCombo(data, view.dom.cmbSubarea, selectedId)
                    });
    };

    var loadTiposAccion = function (selectedId) {

        console.log("loadTiposAccion");

        if (_modo == "A") {
            var payload = { itemorigen: IB.vars.itemorigen, ta206_iditemorigen: IB.vars.iditemorigen };
            var method = "ObtenerListaTipoAccionFiltrada";
        }
        else {
            var origen;
            switch (IB.vars.itemorigen) {
                case "O": origen = 0; break; //Oportunidad
                case "P": origen = 1; break; //Partida
                case "E": origen = 0; break; //Extension (Oportunidad)
                case "S": origen = 2; break; //SUPER
            }

            //En modo edición el combo de tipos de acción está bloqueado. Cargamos todos los tipos para evitar lío.
            origen = -1;

            //var payload = {filtrarPor: origen, ta204_idaccionpreventa: _id };
            //var payload = { tipo: "tipoaccion_preventa", filtrarPor: origen };
            var method = "ObtenerListaTipoAccion";
        }

        return IB.DAL.post(IB.vars.strserver + "Capa_Presentacion/SIC/Services/Listas.asmx", method, payload, null,
            function (data) {
                view.renderComboAccion(data, view.dom.cmbTipoAccion, selectedId);
                screenLoaded.tiposAccionLoaded = true;

            });
    };

    var loadTiposAccion_RA = function () {

        var payload = { itemorigen: IB.vars.itemorigen, ta206_iditemorigen: IB.vars.iditemorigen };
        var method = "ObtenerListaTipoAccionFiltrada";

        return IB.DAL.post(IB.vars.strserver + "Capa_Presentacion/SIC/Services/Listas.asmx", method, payload, null,
            function (data) { view.renderComboAccion(data, view.dom.cmbTipoAccion_RA) });

    };

    var cmbUnidad_onChange = function () {

        console.log("cmbUnidad_onChange");

        view.clearComboOptions(view.dom.cmbArea);
        view.clearComboOptions(view.dom.cmbSubarea);
        view.dom.txtRespSubarea.text("");

        if (_origenpantalla == "CRM") {
            //Si unidad = 0 --> ocultar area y subarea y establecer lider al usuario actual
            var ocultar;
            if (view.getViewValues().ta199_idunidadpreventa <= 0) {
                ocultar = true;
                if (_modo == "A")
                    view.setLider(IB.vars.idficepi, IB.vars.profesional);
            }
            else {
                ocultar = false;
                if (_modo == "A")
                    view.setLider("", "");
            }

            view.cmbUnidad_onChange_togglevisibility(ocultar);
        }
        else {
            if (view.getViewValues().ta199_idunidadpreventa <= 0) {
                view.cmbUnidad_onChange_togglevisibility(true);
            }
            if (screenLoaded.ready) { //Si no se esta en una carga inicial de pantalla
                view.setLider("", "");
            }
        }

        var selectedId = undefined;
        if (structToSelect && structToSelect.ta200_idareapreventa) selectedId = structToSelect.ta200_idareapreventa
        loadAreas(selectedId);
    }

    var cmbArea_onChange = function () {

        console.log("cmbArea_onChange");

        view.clearComboOptions(view.dom.cmbSubarea);
        view.dom.txtRespSubarea.text("");

        if (screenLoaded.ready && _modo != "A" && _origenpantalla != "CRM") { //Si no se esta en una carga inicial de pantalla
            view.setLider("", "");
        }

        var selectedId = undefined;
        if (structToSelect && structToSelect.ta201_idsubareapreventa) selectedId = structToSelect.ta201_idsubareapreventa
        loadSubareas(selectedId);
    }

    var cmbSubarea_onChange = function () {

        console.log("cmbSubarea_onChange");

        var oAccion = view.getViewValues();

        //Mostrar el responsable del subarea bajo el combo
        var sa = subareas.getSubarea(oAccion.ta201_idsubareapreventa)
        if (sa)
            view.dom.txtRespSubarea.text(sa.responsable);
        else
            view.dom.txtRespSubarea.text("");

        //Al seleccionar nuevo subarea, si el lider previamente seleccionado
        //no está como posible lider en el nuevo subarea --> borrar el lider.

        if (oAccion.ta201_idsubareapreventa != null) {
            if (oAccion.t001_idficepi_lider != 0 && oAccion.ta199_idunidadpreventa != 0) {
                var payload = { ta201_idsubareapreventa: oAccion.ta201_idsubareapreventa }
                IB.DAL.post(null, "ObtenerLideresSubarea", payload, null,
                    function (data) {
                        if (data.indexOf(parseInt(oAccion.t001_idficepi_lider)) == -1) {
                            //if (screenLoaded.ready && _modo != "A" && _origenpantalla != "CRM") { //Si no se esta en una carga inicial de pantalla
                            if (screenLoaded.ready && _origenpantalla != "CRM") {
                                view.setLider("", "");
                            }
                        }
                    });
            }
        }
        else {
            view.setLider("", "");
        }

        //Configuración de la ayuda de líderes: Si subarea comercial --> ayuda muestra todos los usuarios
        if (oAccion.ta201_idsubareapreventa == 0) {
            view.setTipoBusquedaAyudaLider("generalficepi");
        }
        else {
            view.setTipoBusquedaAyudaLider("lideres");
        }

        if (structToSelect) { structToSelect = undefined; }

        screenLoaded.subareasLoaded = true;

        if (_caller == "autoasignacion") return; //Cuando se accede para autoasignación no aplicar la siguiente casuistica.

        //Casuistica del subarea que obliga a introducir lider
        var ta201_obligalider = view.getAttrObligalider();

        if (ta201_obligalider === "true") {
            view.liderObligatorio();
        }
        else {
            if (IB.vars.itemorigen != "S") {  //Solicitud tipo oportunidad, extension, objetivo
                if (_modo == "A") {
                    if (_origenpantalla == "CRM") {
                        view.liderNoSeleccionable();
                    }
                    else {
                        view.liderOpcional();
                    }
                }
                else if (_modo == "E") {
                    if (_origenpantalla == "CRM") {
                        view.liderNoSeleccionable();
                    }
                    else {
                        if (oAccion.ta199_idunidadpreventa <= 0) {
                            view.liderNoSeleccionable();
                        }
                        else {
                            view.liderOpcional();
                        }
                    }
                }
            }
            else { //Solicitud de SUPER
                if (_modo == "A") {
                    view.liderOpcional();
                }
                else if (_modo == "E") {
                    view.liderOpcional();
                }
            }

        }
    }

    var cmbTipoAccion_onChange = function () {

        console.log("cmbTipoAccion_onChange");

        var ta205_plazominreq = parseInt(view.getComboSelectedOptionAttr(view.dom.cmbTipoAccion, "ta205_plazominreq"));
        if (ta205_plazominreq == -1) {
            view.dom.dteFFEM.text("");
            return;
        }

        if (ta205_plazominreq == 0) {
            view.dom.dteFFEM.text("No contemplado.")
        }
        else {
            view.dom.dteFFEM.text(ta205_plazominreq + " días naturales.")
        }
    }

    var cmbTipoAccion_RA_onChange = function () {

        console.log("cmbTipoAccion_onChange");

        var ta205_plazominreq = parseInt(view.getComboSelectedOptionAttr(view.dom.cmbTipoAccion_RA, "ta205_plazominreq"));
        if (ta205_plazominreq == -1) {
            view.dom.dteFFEM_RA.text("");
            return;
        }

        if (ta205_plazominreq == 0) {
            view.dom.dteFFEM_RA.text("No contemplado.")
        }
        else {
            view.dom.dteFFEM_RA.text(ta205_plazominreq + " días naturales.")
        }
    }
    //* COMBOS *//

    var subareas = (function () {

        var lst = []
        function init(data) {

            data.map(function (o) {
                var s = {};
                s.ta201_idsubareapreventa = o.ta201_idsubareapreventa;
                s.ta201_denominacion = o.ta201_denominacion;
                s.responsable = o.responsable;
                lst.push(s);
            });
        }

        function getSubarea(ta201_idsubareapreventa) {

            return lst.find(function (o) {
                if (o.ta201_idsubareapreventa === parseInt(ta201_idsubareapreventa)) return true;
            })

        }

        return {
            init: init,
            getSubarea: getSubarea
        }
    })();


    return {
        init: init
    }

})(SUPER.SIC.viewAccion);