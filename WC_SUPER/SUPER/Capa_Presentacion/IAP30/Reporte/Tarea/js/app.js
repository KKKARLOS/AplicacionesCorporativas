$(document).ready(function () { SUPER.IAP30.Tarea.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Tarea = SUPER.IAP30.Tarea || {}

SUPER.IAP30.Tarea.app = (function (view) {
    var sEstado;
    var sImputacion;
    var bEstadoLectura = false;
    var _docAppLoaded = $.Deferred();

    function init() {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

        //Documentacion
        var _accesoDocumentacion;
        var _modoContainerDocsTarea;
        var _soySuperEditor = false;
        var _idDoc;
        var _docAppLoaded = $.Deferred();

        //Obtención de variables
        sEstado = IB.vars.estadotarea;
        sImputacion = IB.vars.imputacion;

        obtenerDatosTarea();
        controlarEdicionPantalla();        

        //Se atachan los eventos 
        view.attachEvents("click", view.dom.btnGrabar, grabarDatos);
        view.attachEvents("click", view.dom.chkFinalizado, view.modDatosFinalizacion);
        initPluginDocumentacion();
        mostrarDocumentos();
        view.init();
    }

    

    obtenerDatosTarea = function (e) {
        if (IB.vars.idTarea != "") {

            var filtro = { idTarea: IB.vars.idTarea };
            IB.procesando.mostrar();
            IB.DAL.post(null, "obtenerDetalleTarea", filtro, null,
                function (data) {
                    if (data != null) {
                        objTarea = data;
                        view.pintarDatosTarea(objTarea);
                    } else IB.bsalert.fixedAlert("danger", "Error", "No se han obtenido datos de la tarea (" + IB.vars.idTarea + ").");
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    IB.bsalert.fixedAlert("danger", "Error", "Error al obtener la tarea (" + IB.vars.idTarea + ").");
                    IB.procesando.ocultar();
                }
            );
        }
    }

    /*obtenerDocumentosTarea = function (e) {
        if (IB.vars.idTarea != "") {

            var filtro = { idTarea: IB.vars.idTarea };
            IB.procesando.mostrar();
            IB.DAL.post(null, "obtenerDocumentos", filtro, null,
                function (data) {
                    if (data != null) {
                        objTarea = data;
                        //view.pintarDocumentosTarea(objTarea);
                    } else IB.bsalert.fixedAlert("danger", "Error", "No se han podido obtneer documento de la tarea (" + IB.vars.idTarea + ").");
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    IB.bsalert.fixedAlert("danger", "Error", "Error al obtener documentos de la tarea (" + IB.vars.idTarea + ").");
                    IB.procesando.ocultar();
                }
            );
        }
    }*/


    grabarDatos = function (e) {

        var notasDisabled = $($("#tabs li")[2]).hasClass("disabled");

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
                //IB.bsalert.toast(data, "info");
                IB.bsalert.toast(data);
            }
        );
    }

    controlarEdicionPantalla = function (e) {
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

        if (bEstadoLectura) view.pantallaDeLectura();
    }

    function initPluginDocumentacion() {

        //Si está en modo edición se le pasa al módulo documentos el idtarea
        /*if (IB.vars.perfilesEdicion.modoPantalla == "E")
            _idDoc = IB.vars.idTarea;
            //Si es modo Alta se le pasa un guid
        else
            _idDoc = IB.vars.uidDocumento*/

        _idDoc = IB.vars.idTarea;
        _modoContainerDocsTarea = IB.vars.sModoContainer; 
        _soySuperEditor = IB.vars.superEditor == "True" ? true : false;
        $.when(SUPER.IAP30.appDocumentos.initTarea(_idDoc, _modoContainerDocsTarea, IB.vars.idficepi, _soySuperEditor))
           .then(function () {
               _docAppLoaded.resolve();
               /*$.when(SUPER.IAP30.appDocumentos.count()).then(function (data) { view.setContadorDocumentos(data); });
               SUPER.IAP30.appDocumentos.onClose(function (data) { view.setContadorDocumentos(data) });*/
           });
    }

    /*function pestañaDocumentacion_onClick() {
       SUPER.IAP30.appDocumentos.show();
    }*/

    function mostrarDocumentos() {
        $.when(_docAppLoaded).then(SUPER.IAP30.appDocumentos.show);        
    }

    return {
        init: init
    }

})(SUPER.IAP30.Tarea.View);
