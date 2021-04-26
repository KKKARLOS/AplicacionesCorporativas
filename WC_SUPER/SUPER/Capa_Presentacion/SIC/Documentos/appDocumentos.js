//********************************************************************editarodocumento
// - Cada contenedor se presenta en modo lectura o escritura.
// - El usuario sólo puede editar sus documentos siempre que el contenedor esté en modo escritura.
// - El lider o admin puede editar los documentos de cualquier usuario siempre que el contenedor esté en modo escritura.
//********************************************************************

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appDocumentos = (function (view, models) {

    var _initialized = false;
    var _urlWebServiceDocs = IB.vars.strserver + "Capa_Presentacion/SIC/Services/Documentos.asmx";
    var _urlWebServiceListas = IB.vars.strserver + "Capa_Presentacion/SIC/Services/Listas.asmx";

    var _origenEdicion; // tareapreventa || accionpreventa 
    var _origenEdicionEstado; // estado de la accion / tarea
    var _idorigenEdicion; //ta2107_idtareapreventa; ta204_idaccionpreventa; GUID
    var _modoContainerDocsAccion; //Contenedor de documentos de la acción --> E=edicion || C=consulta
    var _modoContainerDocsTarea;  //Contenedor de documentos de la tarea --> E=edicion || C=consulta
    var _idficepi; //usuario conectado
    var _superEditor; // true/false. El usuario puede editar o no los documentos que no son suyos.
    var _esGUID;

    var _modoEdicion; //A=alta || E=edición (en la modal de edición del documento)
    var _modoEdicionContainer; //accionpreventa | tareapreventa. Al añadir nuevo doc, para saber en que contenedor hay que pintarlo.

    var _lstDocsAccion = [];
    var _lstDocsTarea = [];

    var _oDocEditando; //entity Documento actualmente en edición;
    var _oDocVisualizando; //entity Documento actualmente visualizando en el detalle de la ventana principal;



    //***** PUBLICOS *****//
    function initAccion(ta204_idaccionpreventa, modoContainerDocsAccion, modoContainerDocsTarea, idficepi, superEditor) {

        _origenEdicion = "accionpreventa";
        _idorigenEdicion = ta204_idaccionpreventa;
        _modoContainerDocsAccion = modoContainerDocsAccion;
        _modoContainerDocsTarea = modoContainerDocsTarea;
        _idficepi = idficepi;
        _superEditor = superEditor;
        _esGUID = false;

        if (isNaN(ta204_idaccionpreventa)) _esGUID = true;

        return init();
    }

    function initTarea(ta207_idtareapreventa, modoContainerDocsAccion, modoContainerDocsTarea, idficepi, superEditor) {

        _origenEdicion = "tareapreventa";
        _idorigenEdicion = ta207_idtareapreventa;
        _modoContainerDocsAccion = modoContainerDocsAccion;
        _modoContainerDocsTarea = modoContainerDocsTarea;
        _idficepi = idficepi;
        _superEditor = superEditor;
        _esGUID = false;

        if (isNaN(ta207_idtareapreventa)) _esGUID = true;

        return init();
    }

    //Cada vez que se muestra la pantalla se comprueba el estado de la tarea/acción por si hubiera que presentar la pantalla en modo consulta.
    function show() {

        $.when(obtenerOrigenEdicionEstado()).then(obtenerDatos).then(pintarCatalogo);
    }

    function count() {

        var defer = $.Deferred();

        $.when(obtenerDatos()).then(function (lst1, lst2) {

            var total = lst1.size() + lst2.size();
            defer.resolve(total);
        });

        return defer.promise();
    }
    //***** PUBLICOS *****//



    //***** PRIVADOS *****//
    function init() {

        var defer = $.Deferred();

        if (!_initialized) {

            //Cargar lista de tipos de documento
            var payload = { tipo: "tipodocumento_preventa", filtrarPor: null }
            IB.DAL.post(_urlWebServiceListas, "ObtenerLista", payload, null,
                        function (lstTipoDocumento) {
                            view.setParams(_origenEdicion, _esGUID);
                            view.init(lstTipoDocumento)
                                .then(function () {
                                    defer.resolve()
                                });
                        })

            view.attachLiveEvents("click", view.dd_selectores.btnEliminar, eliminarDocumento); //Eliminar desde detalle de doc en ventana principal
            view.attachLiveEvents("click", view.dd_selectores.btnEditar, editarDocumento); //Editar desde detalle de doc en ventana principal
            view.attachLiveEvents("click", view.selectores.nuevo, nuevoDocumento); //Añadir documento desde catalogo contenedor superior
            view.attachLiveEvents("click", view.selectores.grabar, grabarDocumento); //Grabar nuevo doc desde modal edición de documento
            view.attachLiveEvents("hidden.bs.modal", view.selectores.modalCatalogo, function () { raiseEvent("onClose", _lstDocsAccion.size() + _lstDocsTarea.size()) });

            //Mostrar detalle documento en la ventana principal
            view.attachLiveEvents("click", view.selectores.documentoCard, documentoCard_onclick);

            _initialized = true;
        }
        else {
            view.setParams(_origenEdicion, _esGUID);
            defer.resolve();
        }

        return defer.promise();

    }

    //Obtiene el estado de la accion / tarea que invoca a este modulo
    function obtenerOrigenEdicionEstado() {

        var defer = $.Deferred();

        if (_esGUID) {
            _origenEdicionEstado = "A";
            defer.resolve("A");
        }
        else {
            var payload = { origenEdicion: _origenEdicion, idorigenEdicion: _idorigenEdicion };
            return IB.DAL.post(_urlWebServiceDocs, "obtenerOrigenEdicionEstado", payload, null, function (data) {
                _origenEdicionEstado = data;
                defer.resolve(data);
            });
        }

        return defer.promise();

    }

    function obtenerDatos() {

        var defer = $.Deferred();

        if (_esGUID) {

            _lstDocsAccion = models.DocumentoList.create([]);
            _lstDocsTarea = models.DocumentoList.create([]);

            var payload = { GUID: _idorigenEdicion };
            IB.DAL.post(_urlWebServiceDocs, "CatalogoGUID", payload, models.Documento.fromJson, function (lst) {

                lst.map(function (o) {
                    setExtraData(o, _origenEdicion);
                })

                if (_origenEdicion == "accionpreventa") {
                    _lstDocsAccion = models.DocumentoList.create(lst);
                }

                if (_origenEdicion == "tareapreventa") {
                    _lstDocsTarea = models.DocumentoList.create(lst);
                }

                defer.resolve(_lstDocsAccion, _lstDocsTarea);
            });

        }
        else {
            switch (_origenEdicion) {

                case "accionpreventa":
                    //tab1 --> documentos de la acción
                    //tab2 --> documentos de las tareas de la acción

                    var payload1 = { origenEdicion: "accionpreventa", idorigenEdicion: _idorigenEdicion };
                    var promise1 = IB.DAL.post(_urlWebServiceDocs, "Catalogo", payload1, models.Documento.fromJson);

                    var payload2 = { origenEdicion: "tareasaccionpreventa", idorigenEdicion: _idorigenEdicion };
                    var promise2 = IB.DAL.post(_urlWebServiceDocs, "Catalogo", payload2, models.Documento.fromJson);

                    $.when(promise1, promise2).then(function (lst1, lst2) {

                        lst1.map(function (o) {
                            setExtraData(o, "accionpreventa");
                        })
                        lst2.map(function (o) {
                            setExtraData(o, "tareapreventa");
                        })

                        _lstDocsAccion = models.DocumentoList.create(lst1);
                        _lstDocsTarea = models.DocumentoList.create(lst2);

                        defer.resolve(_lstDocsAccion, _lstDocsTarea);
                    });

                    break;

                case "tareapreventa":
                    //tab1 --> documentos de la acción de la tarea
                    //tab2 --> documentos de la tarea

                    var payload1 = { origenEdicion: "acciontareapreventa", idorigenEdicion: _idorigenEdicion };
                    var promise1 = IB.DAL.post(_urlWebServiceDocs, "Catalogo", payload1, models.Documento.fromJson);

                    var payload2 = { origenEdicion: "tareapreventa", idorigenEdicion: _idorigenEdicion };
                    var promise2 = IB.DAL.post(_urlWebServiceDocs, "Catalogo", payload2, models.Documento.fromJson);

                    $.when(promise1, promise2).then(function (lst1, lst2) {

                        lst1.map(function (o) {
                            setExtraData(o, "accionpreventa");
                        })
                        lst2.map(function (o) {
                            setExtraData(o, "tareapreventa");
                        })

                        _lstDocsAccion = models.DocumentoList.create(lst1);
                        _lstDocsTarea = models.DocumentoList.create(lst2);

                        defer.resolve(_lstDocsAccion, _lstDocsTarea);
                    });

                    break;
            }
        }

        return defer.promise();
    }

    function setExtraData(o, container) {

        o.origenEdicion = _origenEdicion;
        o.container = container;
        o.editable = false;

        //Permisos de edición:
        // - Si el container está en modo edición, el doc será editable si:
        //      - Soy el autor del doc
        //      - Soy supereditor (lideres y admin)
        if (container == "accionpreventa" && _modoContainerDocsAccion == "E") {
            if (o.t001_idficepi_autor == _idficepi || _superEditor) {
                o.editable = true;
            }
        }
        else if (container == "tareapreventa" && _modoContainerDocsTarea == "E") {
            if (o.t001_idficepi_autor == _idficepi || _superEditor) {
                o.editable = true;
            }
        }

        //Protección concurrencia. El campo o.estado indica el estado de la tarea o accion a la que pertenece el documento

        //Excepción: Si origenEdicion = Accion y Accion = Abierta y usuario es supereditor y o es de tarea  --> o.editable = true
        //(Cuando se entra desde una accion abierta y el usuario es supereditor debe poder modificar los documentos de tarea independientemente de su estado)
        if (_origenEdicion == "accionpreventa" && _origenEdicionEstado == "A" && _superEditor == true && container == "tareapreventa" && _modoContainerDocsTarea == "E") {
            o.editable = true;
        }
        else {
            if (o.estado != "A") o.editable = false; //Resto de casos --> en función de su estado.
        }
    }


    //* CATALOGO DE DOCUMENTOS *//
    function pintarCatalogo() {

        var o;

        if (_esGUID) {

            switch (_origenEdicion) {

                case "accionpreventa":
                    o = {
                        containerAccion: {
                            lstDocumentos: _lstDocsAccion,
                            titulo: "Documentación asociada a la acción",
                            modo: _modoContainerDocsAccion,
                            ext: false
                        }
                    };
                    break;

                case "tareapreventa":
                    o = {
                        containerTarea: {
                            lstDocumentos: _lstDocsTarea,
                            titulo: "Documentación asociada a la tarea",
                            modo: _modoContainerDocsTarea,
                            ext: false
                        }
                    };
                    break;

            }
        }

        else {
            switch (_origenEdicion) {

                case "accionpreventa":

                    o = {
                        containerAccion: {
                            lstDocumentos: _lstDocsAccion,
                            titulo: "Documentación asociada a la acción",
                            modo: _modoContainerDocsAccion,
                            ext: false
                        }
                        ,
                        containerTarea: {
                            lstDocumentos: _lstDocsTarea,
                            titulo: "Documentación asociada a las tareas de la acción",
                            modo: "C", //Se pone "C" a pelo para ocultar el botón "añadir" --> No se pueden añadir docs de tarea a una acción
                            ext: true
                        }
                    };

                    break;

                case "tareapreventa":

                    o = {
                        containerAccion: {
                            lstDocumentos: _lstDocsAccion,
                            titulo: "Documentación asociada a la acción de la tarea",
                            modo: "C", //Se pone "C" a pelo para ocultar el botón "añadir" --> No se pueden añadir docs de acción a una tarea
                            ext: false
                        }
                        ,
                        containerTarea: {
                            lstDocumentos: _lstDocsTarea,
                            titulo: "Documentación asociada a la tarea",
                            modo: _modoContainerDocsTarea,
                            ext: false
                        }
                    };

                    break;
            }
        }

        view.detalleDocumento(models.Documento.create()); //inicializar el detalle de documento de la pantalla principal
        view.renderCatalogo(o);
        view.showModalCatalogo();

    }

    //Al hacer click en un documento de la lista --> mostrar el detalle del documento en el container de la derecha
    function documentoCard_onclick() {

        _oDocVisualizando = null;
        var ta210_iddocupreventa = $(this).attr("data-ta210_iddocupreventa");

        var oDoc = _lstDocsAccion.get(ta210_iddocupreventa);
        if (oDoc == null) oDoc = _lstDocsTarea.get(ta210_iddocupreventa);

        if (oDoc == null) {
            IB.bserror.mostrarAvisoAplicacion("Documentación", "Ha ocurrido un error al obtener la información del documento.<div class='text-left' style='margin-top:40px;font-size:12px'>Vuelve a cargar la pantalla y si el problema persiste pónte en contacto con el CAU.<br>Disculpa las molestias.</div>");
            return;
        }

        _oDocVisualizando = oDoc;

        view.resaltarDocumento(ta210_iddocupreventa);
        view.detalleDocumento(oDoc);

    }
    //* CATALOGO DE DOCUMENTOS *//



    //*DETALLE DE DOCUMENTO EN VENTANA PRINCIPAL *//
    function eliminarDocumento() {

        if (_oDocVisualizando == null) return;

        $.when(IB.bsconfirm.confirm("primary", "Eliminar documento", "¿Estás seguro que quieres eliminar el documento?")).then(function () {

            IB.procesando.mostrar();

            var payload = { ta210_iddocupreventa: _oDocVisualizando.ta210_iddocupreventa };
            IB.DAL.post(_urlWebServiceDocs, "Delete", payload, null, function () {
                $.when(IB.procesando.ocultar()).then(function () {
                    if (_oDocVisualizando.container == "accionpreventa")
                        _lstDocsAccion.delete(_oDocVisualizando);
                    else if (_oDocVisualizando.container == "tareapreventa")
                        _lstDocsTarea.delete(_oDocVisualizando);

                    view.eliminarDocumento(_oDocVisualizando.ta210_iddocupreventa);
                });
            });
        });
    }

    function editarDocumento() {

        if (_oDocVisualizando == null) return;

        _modoEdicion = "E";
        _modoEdicionContainer = _oDocVisualizando.container;

        $.when(getDocumentoFromBBDD(_oDocVisualizando.ta210_iddocupreventa)).then(function (data) {
            _oDocEditando = data;
            view.editarDocumento(data, _modoEdicion);
        });

    }

    function nuevoDocumento() {

        _modoEdicion = "A";
        _oDocEditando = models.Documento.create();

        _modoEdicionContainer = $(this).attr("data-container");

        view.editarDocumento(null, _modoEdicion, _modoEdicionContainer);
    }

    function getDocumentoFromBBDD(ta210_iddocupreventa) {

        var $defer = $.Deferred();

        IB.procesando.mostrar();

        var payload = { ta210_iddocupreventa: ta210_iddocupreventa };
        IB.DAL.post(_urlWebServiceDocs, "Select", payload, null,
            function (data) {
                $.when(IB.procesando.ocultar()).then(function () {
                    $defer.resolve(data);
                });
            }
        );

        return $defer.promise();
    }
    //*DETALLE DE DOCUMENTO EN VENTANA PRINCIPAL *//



    //* MODAL EDICION DE DOCUMENTO *//
    function grabarDocumento() {

        if (!view.validarEdicion()) return;

        var oDoc = view.getModalEdicionValues();

        oDoc.ta210_iddocupreventa = _oDocEditando.ta210_iddocupreventa;
        if (oDoc.t2_iddocumento == null) {
            oDoc.t2_iddocumento = _oDocEditando.t2_iddocumento;
            oDoc.ta210_nombrefichero = _oDocEditando.ta210_nombrefichero;
            oDoc.ta210_kbytes = _oDocEditando.ta210_kbytes;
            oDoc.t001_idficepi_autor = _oDocEditando.t001_idficepi_autor
        }
        else {
            oDoc.fileupdated = true;
        }
        oDoc.origenEdicion = _origenEdicion;

        if (_modoEdicion == "A") {
            if (_esGUID) {
                oDoc.ta210_guidprovisional = _idorigenEdicion;
            }
            else {
                if (_modoEdicionContainer == "accionpreventa") oDoc.ta204_idaccionpreventa = _idorigenEdicion;
                if (_modoEdicionContainer == "tareapreventa") oDoc.ta207_idtareapreventa = _idorigenEdicion;
            }
        }
        else {
            oDoc.ta204_idaccionpreventa = _oDocEditando.ta204_idaccionpreventa;
            oDoc.ta207_idtareapreventa = _oDocEditando.ta207_idtareapreventa;
            oDoc.ta210_guidprovisional = _oDocEditando.ta210_guidprovisional;
        }

        IB.procesando.mostrar();

        var payload = { oDoc: oDoc };
        methodName = _modoEdicion == "A" ? "Insert" : "Update";

        IB.DAL.post(_urlWebServiceDocs, methodName, payload, null, function (data) {
            if (_modoEdicion == "A") {
                $.extend(oDoc, data);
                oDoc.ta210_iddocupreventa = data.ta210_iddocupreventa;
                oDoc.editable = true;
            }
            $.when(IB.procesando.ocultar()).then(function () {

                if (_modoEdicion == "A") { //Agregar el nuevo doc a la lista js
                    if (_modoEdicionContainer == "accionpreventa") {
                        _lstDocsAccion.add(oDoc);
                    }
                    else if (_modoEdicionContainer == "tareapreventa") {
                        _lstDocsTarea.add(oDoc);
                    }
                }
                else { //Actualizar doc en la lista js
                    $.extend(_oDocVisualizando, data);
                }

                view.closeModalEdicion();

                if (_modoEdicion == "A") {
                    view.addFichaDocumento(oDoc, _modoEdicionContainer)
                }
                else {
                    view.updateFichaDocumento(_oDocVisualizando);
                }

                view.selectDocCatalogo(oDoc.ta210_iddocupreventa);

            });
        });
    }
    //* MODAL EDICION DE DOCUMENTO *//

    //* EVENTOS *//
    var _events = {};

    function onClose(callback) {

        _events.onClose = callback;
    }

    function raiseEvent(event, data) {
        if (typeof _events[event] !== "undefined")
            _events[event].call(this, data);
    }
    //* EVENTOS *//

    return {
        initAccion: initAccion,
        initTarea: initTarea,
        show: show,
        count: count,
        onClose: onClose
    }

})(SUPER.SIC.viewDocumentos, SUPER.SIC.Models);