$(document).ready(function () { SUPER.IAP30.Bitacora.PE.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Bitacora = SUPER.IAP30.Bitacora || {}
SUPER.IAP30.Bitacora.PE = SUPER.IAP30.Bitacora.PE || {}

SUPER.IAP30.Bitacora.PE.app = (function (view) {

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

        //Ayuda tareas
        var options = {
            titulo: "Selección de tarea para consulta bitácora",
            modulo: "IAP30",
            autoSearch: true,
            autoShow: false,
            searchParams: {
            },
            onSeleccionar: function (data) {
                darValor(view.dom.tarea, data.idTarea);
                obtenerDatosTarea();
            },
            onCancelar: function () {
            }
        };

        view.dom.ayudaTarea.buscatarea(options);

        view.init();

        //Obtención de datos en la pantalla principal
        view.attachEvents("click keypress", view.dom.btnObtener, obtenerFiltrosBitacora);
        //view.attachEvents("click", view.selectores.sel_filtros, consultar);
        view.attachEvents("change", view.selectores.sel_filtros, consultar);
        view.attachEvents("click", view.dom.linkTarea, buscarTarea);
        view.attachEvents("keypress", view.dom.tarea, ejecutarAccionTecleo);
        view.attachEvents("focusout", view.dom.tarea, buscarTareaPorId);

        //Seleccionar fila
        view.attachLiveEvents("click", view.selectores.filas, setFila);

        //Ir al detalle del catálogo 
        view.attachEvents("click", view.dom.btnGoAsunto, mostrarDetalle);

        //Control de checkboxes
        //view.attachEvents("click keypress", view.selectores.sel_chk, controlChk);

        view.attachLiveEvents("dblclick", view.selectores.filaSel, mostrarDetalle);

        IB.procesando.ocultar();

        obtenerTipoAsunto();
        //Para cuando vuelvo de un detalle
        if (IB.vars.idTarea && IB.vars.idTarea != "")
            obtenerFiltrosBitacora();

        //Prueba
        //view.dom.txtNotif.data('daterangepicker').setStartDate('01/01/2017');
        //view.dom.txtNotif.data('daterangepicker').setEndDate('01/02/2017');
    }

    //Funcionalidad pantalla principal

    var consultar = function () {
        if (view.dom.chkAutomatica.is(":checked")) {
            obtenerFiltrosBitacora();
        }
    }
    var setFila = function (event) {
        view.marcarLinea(this);
    }

    var obtenerFiltrosBitacora = function () {

        var bConAcciones = false;
        var proyectos = new StringBuilder();

        if (view.dom.desTarea.val() == "") {
            IB.bsalert.fixedAlert("warning", "Error de validación", "La tarea es un dato obligatorio");
            return;
        }


        IB.procesando.mostrar();

        if (view.dom.chkConAcciones.is(":checked")) {
            bConAcciones = true;
        }

        var dNotif = null, hNotif = null;
        if (view.dom.txtNotif.data('daterangepicker').element[0].value != "") {
            dNotif = view.dom.txtNotif.data('daterangepicker').startDate;
            hNotif = view.dom.txtNotif.data('daterangepicker').endDate;
        }

        var dLimite = null, hLimite = null;
        if (view.dom.txtLimite.data('daterangepicker').element[0].value != "") {
            var dLimite = view.dom.txtLimite.data('daterangepicker').startDate;
            var hLimite = view.dom.txtLimite.data('daterangepicker').endDate;
        }

        var dFin = null, hFin = null;
        if (view.dom.txtFin.data('daterangepicker').element[0].value != "") {
            var dFin = view.dom.txtFin.data('daterangepicker').startDate;
            var hFin = view.dom.txtFin.data('daterangepicker').endDate;
        }

        var TipoAsunto = null, Estado = null, Severidad = null, Prioridad = null;
        if (view.dom.selTipoAsunto.val() != '0') TipoAsunto = view.dom.selTipoAsunto.val();
        if (view.dom.selEstado.val() != '0') Estado = view.dom.selEstado.val();
        if (view.dom.selSeveridad.val() != '0') Severidad = view.dom.selSeveridad.val();
        if (view.dom.selPrioridad.val() != '0') Prioridad = view.dom.selPrioridad.val();

        getElementos(view.dom.tarea.val(), bConAcciones, view.dom.txtDenominacion.val(),
                     TipoAsunto, Estado, Severidad, Prioridad,
                     dNotif, hNotif, dLimite, hLimite, dFin, hFin);

    }
    //Obtiene los valores para llenar el combo de tipos de asunto
    var obtenerTipoAsunto = function () {

        var defer = $.Deferred();

        var payload = {};
        IB.DAL.post(null, "obtenerTipoAsunto", payload, null,
            function (data) {
                view.llenarTipoAsunto(data);
                //$.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
                defer.resolve();
            }
        );

        return defer.promise();

    }

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
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar proyecto.");
                }
            );
        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al llamar a proyecto.");
        }
    }


    var filtrarAsuntosTipo = function () {
        IB.procesando.mostrar();
        var sTipoAsunto = $(view.selectores.sel_TipoAsunto).text();
        if (sTipoAsunto == "Todos") sTipoAsunto = "";
        view.dom.tabla.column(view.dom.colTipoAsunto).search(sTipoAsunto).draw();
        $(view.selectores.filaSel).removeClass('activa');
        IB.procesando.ocultar();
    }
    var filtrarAsuntosEstado = function () {
        IB.procesando.mostrar();
        var sEstado = $(view.selectores.sel_Estado).text();
        if (sEstado == "Todos") sEstado = "";
        view.dom.tabla.column(view.dom.colEstado).search(sEstado).draw();
        $(view.selectores.filaSel).removeClass('activa');
        IB.procesando.ocultar();
    }
    var mostrarDetalle = function (e) {
        IB.procesando.mostrar();
        var sId = $(view.selectores.filaSel).attr("id");
        var ASoACC = $(view.selectores.filaSel).attr("ASoACC");
        if (!sId) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar un elemento");
            return;
        }
        var sParam = "&ori=IAP&p=L" + "&idTarea=" + view.dom.tarea.val();
        if (view.dom.chkConAcciones.is(":checked")) {
            sParam += "&conACC=S";
        }
        else
            sParam += "&conACC=N";
        if (view.dom.chkAutomatica.is(":checked")) {
            sParam += "&auto=S";
        }
        else
            sParam += "&auto=N";

        sParam += "&tipo=" + view.dom.selTipoAsunto.val();
        sParam += "&estado=" + view.dom.selEstado.val();
        sParam += "&severidad=" + view.dom.selSeveridad.val();
        sParam += "&prio=" + view.dom.selPrioridad.val();

        if (view.dom.txtNotif.data('daterangepicker').element[0].value != "") {
            sParam += "&notifD=" + view.dom.txtNotif.data('daterangepicker').startDate.format('DD/MM/YYYY');
            sParam += "&notifH=" + view.dom.txtNotif.data('daterangepicker').endDate.format('DD/MM/YYYY');
        }
        if (view.dom.txtLimite.data('daterangepicker').element[0].value != "") {
            sParam += "&limiteD=" + view.dom.txtLimite.data('daterangepicker').startDate.format('DD/MM/YYYY');
            sParam += "&limiteH=" + view.dom.txtLimite.data('daterangepicker').endDate.format('DD/MM/YYYY');
        }
        if (view.dom.txtFin.data('daterangepicker').element[0].value != "") {
            sParam += "&finD=" + view.dom.txtFin.data('daterangepicker').startDate.format('DD/MM/YYYY');
            sParam += "&finH=" + view.dom.txtFin.data('daterangepicker').endDate.format('DD/MM/YYYY');
        }

        if (ASoACC == "AS") {
            var sAux = "";
            if (IB.vars.qs)
                sAux = mergeParam(IB.vars.qs, "idAsunto=" + sId + sParam);
            else
                sAux = "idAsunto=" + sId + sParam;
            qs = IB.uri.encode(sAux);
            location.href = IB.vars.strserver + "Capa_Presentacion/IAP30/Reporte/Bitacora/Asunto/AsuntoTarea/Default.aspx?" + qs;
        }
        else {
            if (IB.vars.qs)
                sAux = mergeParam(IB.vars.qs, "idAccion=" + sId + sParam);
            else
                sAux = "idAccion=" + sId + sParam;

            qs = IB.uri.encode(sAux);
            location.href = IB.vars.strserver + "Capa_Presentacion/IAP30/Reporte/Bitacora/Accion/AccionTarea/Default.aspx?" + qs;
        }

    }

    //Fin funcionalidad pantalla principal    

    obtenerDatosTarea = function (e) {
        if (view.obtenerValor(view.dom.tarea) != "") {

            var idTarea = view.obtenerValor(view.dom.tarea);
            var pos = idTarea.indexOf('.');
            if (pos != -1) idTarea = idTarea.substring(0, pos) + idTarea.substring(pos + 1, idTarea.length);
            var filtro = { idTarea: idTarea };
            IB.procesando.mostrar();
            IB.DAL.post(null, "obtenerDetalleTarea", filtro, null,
                function (data) {
                    if (data != null) {
                        objTarea = data;
                        view.pintarDatosTarea(objTarea);
                    } else {
                        $.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", "No puede obtener la tarea (" + view.obtenerValor(view.dom.tarea) + ")."))
                        .then(function () { view.asignarFoco(view.dom.tarea) });
                    }
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    $.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener la tarea (" + view.obtenerValor(view.dom.tarea) + ")."))
                    .then(function () { view.asignarFoco(view.dom.tarea) });
                    IB.procesando.ocultar();
                }
            );
        }
    }
    getDatosTarea = function (e) {
        if (view.obtenerValor(view.dom.tarea) != "") {

            var idTarea = view.obtenerValor(view.dom.tarea);
            var pos = idTarea.indexOf('.');
            if (pos != -1) idTarea = idTarea.substring(0, pos) + idTarea.substring(pos + 1, idTarea.length);
            var filtro = { idTarea: idTarea };
            IB.procesando.mostrar();
            IB.DAL.post(null, "getDetalleTarea", filtro, null,
                function (data) {
                    if (data != null) {
                        objTarea = data;
                        view.pintarDatosTarea(objTarea);
                    } else {
                        $.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", "No puede obtener la tarea (" + view.obtenerValor(view.dom.tarea) + ")."))
                        .then(function () { view.asignarFoco(view.dom.tarea) });
                    }
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    var sMens = "No se ha podido obtener el detalle de la tarea (" + view.obtenerValor(view.dom.tarea) + ")."
                    //$.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", sMens))
                    $.when(IB.bsalert.fixedAlert("warning", "Aviso", sMens))
                    .then(function () { view.asignarFoco(view.dom.tarea) });
                    IB.procesando.ocultar();
                }
            );
        }
    }

    buscarTarea = function (e) {
        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();
        var o = {
            tipoBusqueda: "tareasBitacoraIAP"
            //fechaInicio: (view.obtenerValor(view.dom.txtFIni) == "") ? null : view.obtenerValor(view.dom.txtFIni).toDate(),
            //fechaFin: (view.obtenerValor(view.dom.txtFFin) == "") ? null : view.obtenerValor(view.dom.txtFFin).toDate()
        }
        $(".fk_ayudaTarea").buscatarea("option", "searchParams", o);
        $(".fk_ayudaTarea").buscatarea("show");

    }

    ejecutarAccionTecleo = function (e) {
        if (e.which == 13) {
            getDatosTarea(e);
        } else if (e.which != 13 && e.which != 37 && e.which != 38 && e.which != 39 && e.which != 40) {
            if (validarTeclaNumerica(e, false)) {
                view.limpiarDatosTarea();
            } else return false;
        }
    }

    buscarTareaPorId = function (e) {
        if (view.obtenerValor(view.dom.tarea) == "") view.limpiarDatosTarea();
        else {
            //obtenerDatosTarea(e);
        }
    }

    /*Llamadas de carga a webmethods*/
    //Llamadas pantalla principal

    //Obtiene los partes de actividad en base a los filtros
    var getElementos = function (idTarea, bConAcciones, Denominacion,
                                 TipoAsunto, Estado, Severidad, Prioridad,
                                 dNotif, hNotif, dLimite, hLimite, dFin, hFin) {

        var defer = $.Deferred();

        var payload = {
            idTarea: idTarea, acciones: bConAcciones, Denominacion: Denominacion,
            TipoAsunto: TipoAsunto, Estado: Estado, Severidad: Severidad, Prioridad: Prioridad,
            dNotif: dNotif, hNotif: hNotif, dLimite: dLimite, hLimite: hLimite, dFin: dFin, hFin: hFin
        };
        IB.DAL.post(null, "getElementos", payload, null,
            function (data) {
                view.pintarTablaBitacora(data);
                $.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
            }
        );

        return defer.promise();

    }
    //Fin llamadas pantalla principal

    return {
        init: init
    }

})(SUPER.IAP30.Bitacora.PE.View);