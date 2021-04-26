var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ImpDiaria = SUPER.IAP30.ImpDiaria || {}

SUPER.IAP30.ImpDiaria.appTarea = (function (view, viewPantalla) {
    var sEstado;
    var sImputacion;
    var bEstadoLectura = false;
    var _docAppLoaded = $.Deferred();

    //Booleano indicador de cambios en el detalle de tarea
    var bCambiosTarea = false;

    /*Lógica de carga y visualización de datos*/
    window.onbeforeunload = function () {
        if (bCambiosTarea) return "Si continúas perderás los cambios que no has guardado.";
    }

    function init() {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

        //Se atachan los eventos 
        view.attachEvents("click", view.dom.btnGrabar, grabarDatos);
        view.attachEvents("click", view.dom.btnCancelar, controlarCambios);
        view.attachEvents("click", view.dom.btnCierreModalTarea, controlarCambios);
        view.attachEvents("click", view.dom.chkFinalizado, view.modDatosFinalizacion);
        //Cierre de la ventana modal
        view.attachEvents("hidden.bs.modal", view.dom.modalDetalleTarea, view.cierreModal);

        view.attachEvents("shown.bs.modal", view.dom.modalDetalleTarea, view.ajustarCampos);
        //Carga de documentos en la pestaña de documentos
        view.attachEvents("click", view.dom.pestanaDocu, mostrarDocumentos);
        //Evento de cambios en los campos
        view.attachEvents("keydown", view.selectores.sel_textarea, hayCambiosTarea);
        view.attachLiveEvents("change", view.selectores.sel_inputs, hayCambiosTarea);
        view.attachLiveEvents("change", view.selectores.sel_textarea, hayCambiosTarea);
        view.attachLiveEvents("keydown", view.dom.txtTotEst, view.descheckear);
        view.attachEvents("keypress", view.dom.txtTotEst, validarCampoTipoHora);
        view.attachEvents("focusout", view.dom.txtTotEst, formatearHoras);
        //view.attachEvents("focusout", view.dom.txtTotEst, validarTotEst);
        view.init();



    }

    aperturaModalDetalleTarea = function (e) {
        $.when(accionesAperturaTarea()).then(function () {
            view.aperturaModal(view.dom.modalDetalleTarea);
            $("#tabs li.active a").attr("aria-expanded","false");
            $("#tabs li.active").removeClass("active");            
            $("#tabs li:first").addClass("active");
            $("#tabs li:first a").attr("aria-expanded", "true");
            $('#my-tab-content .active').removeClass('active');
            $('#my-tab-content :first').addClass('active').show();            
        });
    }
        

    accionesAperturaTarea = function (e) {
        var defer = $.Deferred();

        //Documentacion
        var _accesoDocumentacion;
        var _modoContainerDocsTarea;
        var _soySuperEditor = false;
        var _idDoc;

        //Obtención de variables
        sEstado = IB.vars.estadotarea;
        sImputacion = IB.vars.imputacion;

        bCambiosTarea = false;
        $.when(obtenerDatosTarea())
            .then(function () {
                controlarEdicionPantalla();
                initPluginDocumentacion();
                mostrarDocumentos();
                defer.resolve()
            });
        
        return defer.promise();
    }


    //Control de cambios en la pantalla antes de cierre
    controlarCambios = function (e) {
        IB.procesando.mostrar();
        var defer = $.Deferred();        

        if (bCambiosTarea) {
            IB.procesando.ocultar();
            $.when(IB.bsconfirm.confirmCambios())
                .then(function () {                    
                    defer.resolve();
                    bCambiosTarea = false;
                    view.dom.modalDetalleTarea.modal("hide");
                })

        } else {
            defer.resolve();
            view.dom.modalDetalleTarea.modal("hide");
            IB.procesando.ocultar();
        }

        return defer.promise();
    }

    //Identificación de cambios en la pantalla
    var hayCambiosTarea = function () {
        if (!bCambiosTarea) {
            bCambiosTarea = true;
            view.dom.btnGrabar.removeAttr("disabled");
        }
    }

    obtenerDatosTarea = function (e) {
        var defer = $.Deferred();
        if (IB.vars.idTarea != "") {

            var filtro = { idTarea: IB.vars.idTarea };
            IB.procesando.mostrar();
            IB.DAL.post(null, "obtenerDetalleTarea", filtro, null,
                function (data) {
                    if (data != null) {
                        objTarea = data;
                        view.pintarDatosTarea(objTarea);
                        defer.resolve();
                    } else IB.bsalert.fixedAlert("danger", "Error de aplicación", "No se han obtenido datos de la tarea (" + IB.vars.idTarea + ").");
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener la tarea (" + IB.vars.idTarea + ").");
                    IB.procesando.ocultar();
                    defer.reject();
                }
            );
        }
        return defer.promise();
    }


    grabarDatos = function (e) {

        if (!validarTotEst()) return false;

        var notasDisabled = $($("#tabs li")[2]).hasClass("disabled");

        var hayIndicaciones = false;

        var filtro = {
            idTarea: view.obtenerValor(view.dom.tarea), finalizado: view.radioCheckeado(view.dom.chkFinalizado) ? 1 : 0,
            totalEst: (view.obtenerValor(view.dom.txtTotEst) == "") ? 0 : view.obtenerValor(view.dom.txtTotEst),
            fechaFinEst: view.obtenerValor(view.dom.txtFEst),
            comentario: view.obtenerValor(view.dom.txtObsv),
            grabarNotas: (notasDisabled) ? 0 : 1,
            notas: (notasDisabled) ? null : [view.obtenerValor(view.dom.txtInv), view.obtenerValor(view.dom.txtAcc), view.obtenerValor(view.dom.txtPru), view.obtenerValor(view.dom.txtPaso)]

        };

        IB.procesando.mostrar();
        IB.DAL.post(null, "grabarTarea", filtro, null,
            function (data) {
                IB.procesando.ocultar();
                IB.bsalert.toast(data);
                if (view.obtenerValor(view.dom.txtParti) != "" || filtro.comentario != "" ||
                    view.obtenerValor(view.dom.txtColec) != "" || accounting.unformat(view.obtenerValor(view.dom.txtTotPrev), ",") != 0 ||
                    view.obtenerValor(view.dom.txtFFinPrev) != "") hayIndicaciones = true;
                viewPantalla.tareaModificada(filtro.idTarea, filtro.finalizado, filtro.totalEst, filtro.fechaFinEst, hayIndicaciones);
            }
        );
    }

    validarCampoTipoHora = function (e) {
        return validarTeclaNumerica(e, true);
    }

    validarTotEst = function (e) {
        if ((parseFloat(dfn(view.dom.txtTotEst.val())) != 0) && (parseFloat(dfn(view.dom.txtTotEst.val())) < parseFloat(dfn(view.dom.PConsumido.val())))) {
            IB.bsalert.toastdanger("La estimación total de horas no puede ser menor a las horas consumidas");
            view.asignarFoco(view.dom.txtTotEst);
            return false; 
        }
        return true;
    }

    /***********************************************
   Función: dfn (DesFormatear Numero)
   Inputs: sValor --> Número formateado a desformatear
   ************************************************/
    function dfn(sValor) {
        if (isNaN(sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), "."))
            || sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".") == ""
            ) return "0";
        else return sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".");

        //    return (isNaN(sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")))?"0":sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".");
    }

   
    formatearHoras = function (e) {
        var horas = accounting.format(accounting.unformat(view.obtenerValor($(this)), ","));
        view.darValor($(this), horas);
    }

    controlarEdicionPantalla = function (e) {
        bEstadoLectura = false;
        switch (sEstado)//Estado
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
        view.dom.btnGrabar.attr("disabled", "disabled");
        if (bEstadoLectura) view.pantallaDeLectura();
        else view.pantallaDeEscritura();
    }

    function initPluginDocumentacion() {

        //Si está en modo edición se le pasa al módulo documentos el idtarea
        /*if (IB.vars.perfilesEdicion.modoPantalla == "E")
            _idDoc = IB.vars.idTarea;
            //Si es modo Alta se le pasa un guid
        else
            _idDoc = IB.vars.uidDocumento*/

        _idTarea = parseInt(IB.vars.idTarea);
        _modoContainerDocsTarea = IB.vars.sModoContainer;
        _soySuperEditor = IB.vars.superEditor == "True" ? true : false;
        $.when(SUPER.IAP30.appDocumentos.initModuloDoc(_idTarea, "detalleTarea", _modoContainerDocsTarea, IB.vars.codUsu, _soySuperEditor))
           .then(function () {
               _docAppLoaded.resolve();
           });
    }

    function mostrarDocumentos() {
        $.when(_docAppLoaded).then(SUPER.IAP30.appDocumentos.show);
    }

    return {
        init: init,
        aperturaModalDetalleTarea: aperturaModalDetalleTarea
    }

})(SUPER.IAP30.ImpDiaria.viewTarea, SUPER.IAP30.ImpDiaria.View);
