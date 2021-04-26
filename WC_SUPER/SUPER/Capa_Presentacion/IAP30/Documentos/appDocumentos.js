//********************************************************************
// - Cada contenedor se presenta en modo lectura o escritura.
// - El usuario sólo puede editar sus documentos siempre que el contenedor esté en modo escritura.
// - El lider o admin puede editar los documentos de cualquier usuario siempre que el contenedor esté en modo escritura.
//********************************************************************

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};

SUPER.IAP30.appDocumentos = (function (view, models) {

    var _initialized = false;
    var _urlWebServiceDocs = IB.vars.strserver + "Capa_Presentacion/IAP30/Services/Documentos.asmx";

    var _origenEdicion; // detalleTarea || detalleAsuntoBitacora  || detalleAccionBitacora , .....
    var _origenEdicionEstado; // estado de la accion / tarea
    var _idorigenEdicion; //ta2107_idtareapreventa; ta204_idaccionpreventa; GUID
    var _modoContainerDocs;  //Contenedor de documentos --> E=edicion || C=consulta
    var _idficepi; //usuario conectado
    var _superEditor; // true/false. El usuario puede editar o no los documentos que no son suyos.
    var _esGUID = false;

    var _modoEdicion; //A=alta || E=edición (en la modal de edición del documento)   
    var _lstDocsElemento = [];

    var _oDocEditando; //entity Documento actualmente en edición;
    var _oDocVisualizando; //entity Documento actualmente visualizando en el detalle de la ventana principal;



    //***** PUBLICOS *****//   
   
    function initModuloDoc(_idElemento, origenEdicion, modoContainerDocs, idficepi, superEditor) {

        _origenEdicion = origenEdicion;
        _idorigenEdicion = _idElemento;
        _modoContainerDocs = modoContainerDocs;
        _idficepi = parseInt(idficepi);
        _superEditor = superEditor;
        _esGUID = false;
        
        return init();
    }

    function getOrigen() {
        return _origenEdicion;
    }

    //Cada vez que se muestra la pantalla se comprueba el estado de la tarea/acción por si hubiera que presentar la pantalla en modo consulta.
    function show() {
        //$.when(obtenerOrigenEdicionEstado()).then(obtenerDatos).then(pintarCatalogo);
        $.when(obtenerDatos()).then(pintarCatalogo);
    }

    function count() {

        var defer = $.Deferred();

        $.when(obtenerDatos()).then(function (lst1) {

            var total = lst1.size() ;
            defer.resolve(total);
        });

        return defer.promise();
    }
    //***** PUBLICOS *****//



    //***** PRIVADOS *****//
    function init() {

        var defer = $.Deferred();

        if (!_initialized) {
            
            view.init().then(function () {
                    defer.resolve()
            });

            if (_modoContainerDocs == "E") {
                view.attachLiveEvents("click", view.dd_selectores.btnEliminar, eliminarDocumento); //Eliminar desde detalle de doc en ventana principal
                view.attachLiveEvents("click", view.dd_selectores.btnEditar, editarDocumento); //Editar desde detalle de doc en ventana principal
                view.attachLiveEvents("click", view.selectores.nuevo, nuevoDocumento); //Añadir documento desde catalogo contenedor superior
                view.attachLiveEvents("click", view.selectores.grabar, grabarDocumento); //Grabar nuevo doc desde modal edición de documento
                //view.attachLiveEvents("hidden.bs.modal", view.selectores.modalCatalogo, function () { raiseEvent("onClose", _lstDocsElemento.size())  });
            }

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
        
        var payload = { origenEdicion: _origenEdicion, idorigenEdicion: _idorigenEdicion };
        return IB.DAL.post(_urlWebServiceDocs, "obtenerOrigenEdicionEstado", payload, null, function (data) {
            _origenEdicionEstado = data;
            defer.resolve(data);
        });

        return defer.promise();

    }

    function obtenerDatos() {

        var defer = $.Deferred();

        var payload1 = { origenEdicion: _origenEdicion, idUsuAutorizado: _idficepi, idElemento: _idorigenEdicion };
        var promise1 = IB.DAL.post(_urlWebServiceDocs, "Catalogo", payload1, models.Documento.fromJson);

        $.when(promise1).then(function (lst1) {

            lst1.map(function (o) {
                setExtraData(o);
            })
                       
            _lstDocsElemento = models.DocumentoList.create(lst1);

            defer.resolve(_lstDocsElemento);
        });

        return defer.promise();
    }

    function setExtraData(o) {

        o.origenEdicion = _origenEdicion;
        o.editable = false;

        //Permisos de edición:
        // - Si el container está en modo edición y el documento es de sólo lectura, el doc será editable si:
        //      - Soy el autor del doc
        //      - Soy supereditor (lideres y admin)
        if (_modoContainerDocs == "E") {            
            if (o.modolectura) {
                if (o.idusuario_autor == _idficepi || _superEditor) o.editable = true;
            } else o.editable = true;

        } 
    }


    //* CATALOGO DE DOCUMENTOS *//
    function pintarCatalogo() {

        var o;

       
        switch (_origenEdicion) {
               
            case "detalleTarea":

                o = {

                    containerElemento: {
                        lstDocumentos: _lstDocsElemento,
                        titulo: "Documentación asociada a la tarea",
                        modo: _modoContainerDocs,
                        ext: false
                    }
                };

                break;
            case "detalleAsuntoPE":

                o = {

                    containerElemento: {
                        lstDocumentos: _lstDocsElemento,
                        titulo: "Documentación asociada al asunto del proyecto económico",
                        modo: _modoContainerDocs,
                        ext: false
                    }
                };

                break;

            case "detalleAccionPE":

                o = {

                    containerElemento: {
                        lstDocumentos: _lstDocsElemento,
                        titulo: "Documentación asociada a la acción del proyecto económico",
                        modo: _modoContainerDocs,
                        ext: false
                    }
                }
                break;
            case "detalleAsuntoPT":

                o = {

                    containerElemento: {
                        lstDocumentos: _lstDocsElemento,
                        titulo: "Documentación asociada al asunto del proyecto técnico",
                        modo: _modoContainerDocs,
                        ext: false
                    }
                };

                break;

            case "detalleAccionPT":

                o = {

                    containerElemento: {
                        lstDocumentos: _lstDocsElemento,
                        titulo: "Documentación asociada a la acción del proyecto técnico",
                        modo: _modoContainerDocs,
                        ext: false
                    }
                };
                break;
            case "detalleAsuntoTA":

                o = {

                    containerElemento: {
                        lstDocumentos: _lstDocsElemento,
                        titulo: "Documentación asociada al asunto de la tarea",
                        modo: _modoContainerDocs,
                        ext: false
                    }
                };

                break;

            case "detalleAccionTA":

                o = {

                    containerElemento: {
                        lstDocumentos: _lstDocsElemento,
                        titulo: "Documentación asociada a la acción de la tarea",
                        modo: _modoContainerDocs,
                        ext: false
                    }
                };
                break;
        }
        

        view.detalleDocumento(models.Documento.create()); //inicializar el detalle de documento de la pantalla principal
        view.renderCatalogo(o);
    }

    //Al hacer click en un documento de la lista --> mostrar el detalle del documento en el container de la derecha
    function documentoCard_onclick() {

        _oDocVisualizando = null;
        var idDocumento = $(this).attr("data-idDocumento");

        var oDoc = _lstDocsElemento.get(idDocumento);

        if (oDoc == null) {
            IB.bserror.mostrarAvisoAplicacion("Documentación", "Ha ocurrido un error al obtener la información del documento.<div class='text-left' style='margin-top:40px;font-size:12px'>Vuelve a cargar la pantalla y si el problema persiste pónte en contacto con el CAU.<br>Disculpa las molestias.</div>");
            return;
        }

        _oDocVisualizando = oDoc;

        view.resaltarDocumento(idDocumento);
        view.detalleDocumento(oDoc);

    }
    //* CATALOGO DE DOCUMENTOS *//



    //*DETALLE DE DOCUMENTO EN VENTANA PRINCIPAL *//
    function eliminarDocumento() {

        if (_oDocVisualizando == null) return;

        $.when(IB.bsconfirm.confirm("primary", "::: SUPER ::: - Eliminar documento", "¿Estás seguro que quieres eliminar el documento?")).then(function () {

            IB.procesando.mostrar();

            var payload = { origenEdicion: _origenEdicion, idDocumento: _oDocVisualizando.idDocumento };
            IB.DAL.post(_urlWebServiceDocs, "Delete", payload, null, function () {
                $.when(IB.procesando.ocultar()).then(function () {
                    /*if (_oDocVisualizando.container == "detalletarea")
                        _lstDocsElemento.delete(_oDocVisualizando);*/

                    view.eliminarDocumento(_oDocVisualizando.idDocumento);
                });
            });
        });
    }

    function editarDocumento() {

        if (_oDocVisualizando == null) return;

        _modoEdicion = "E";

        $.when(getDocumentoFromBBDD(_oDocVisualizando.idDocumento)).then(function (data) {
            _oDocEditando = data;
            view.editarDocumento(data, _modoEdicion);
        });

    }

    function nuevoDocumento() {

        _modoEdicion = "A";
        _oDocEditando = models.Documento.create();
       
        view.editarDocumento(null, _modoEdicion);
    }

    function getDocumentoFromBBDD(idDocumento) {

        var $defer = $.Deferred();

        IB.procesando.mostrar();

        var payload = { origenEdicion: _origenEdicion, idDocumento: idDocumento };
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

        if (!view.validarEdicion()) {
            if ($('#mde-descripcion').html() == "") $('#mde-descripcion').focus();
            return;
        }
        var oDoc = view.getModalEdicionValues();

        oDoc.idDocumento = _oDocEditando.idDocumento;
        if (oDoc.t2_iddocumento == null) {
            oDoc.t2_iddocumento = _oDocEditando.t2_iddocumento;
            oDoc.nombrearchivo = _oDocEditando.nombrearchivo;
            oDoc.idusuario_autor = _oDocEditando.idusuario_autor;
            oDoc.idusuario_modif = _oDocEditando.idusuario_modif;
            oDoc.autor = _oDocEditando.autor;
            oDoc.autormodif = _oDocEditando.autormodif;
        }
        else {
            oDoc.fileupdated = true;
        }
        oDoc.origenEdicion = _origenEdicion;

        if (_modoEdicion == "A") {
            oDoc.idElemento = _idorigenEdicion;
            oDoc.modolectura = false;
            oDoc.idusuario_autor = _idficepi;
            oDoc.idusuario_modif = _idficepi;
        }
        else {
            oDoc.idElemento = _oDocEditando.idElemento;
            oDoc.idusuario_autor = _idficepi;
            //oDoc.t = _idficepi;
        }

        IB.procesando.mostrar();

        var payload = { origenEdicion: _origenEdicion, oDoc: oDoc };
        methodName = _modoEdicion == "A" ? "Insert" : "Update";

        IB.DAL.post(_urlWebServiceDocs, methodName, payload, null, function (data) {
            if (_modoEdicion == "A") {
                $.extend(oDoc, data);
                // ojo con el resto de casos
                oDoc.idDocumento = data.idDocumento;
                oDoc.editable = true;
            }
            $.when(IB.procesando.ocultar()).then(function () {

                if (_modoEdicion == "A") { //Agregar el nuevo doc a la lista js                   
                    _lstDocsElemento.add(oDoc);
                }
                else { //Actualizar doc en la lista js
                    $.extend(_oDocVisualizando, data);
                }

                view.closeModalEdicion();

                if (_modoEdicion == "A") {
                    view.addFichaDocumento(oDoc)
                }
                else {
                    view.updateFichaDocumento(_oDocVisualizando);
                }

                view.selectDocCatalogo(oDoc.idDocumento);

            });
        });
    }
    //* MODAL EDICION DE DOCUMENTO *//

    //* EVENTOS *//
    var _events = {};

    function onClose(callback) {

        _events.onClose = callback;
    }

    function raiseEvent(event, data)
    {
        if(typeof _events[event] !== "undefined")
            _events[event].call(this, data);
    }
    //* EVENTOS *//

    return {
        initModuloDoc: initModuloDoc,
        show: show,
        count: count,
        onClose: onClose
    }

})(SUPER.IAP30.viewDocumentos, SUPER.IAP30.Models);