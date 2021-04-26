///#source 1 1 /Capa_Presentacion/SIC/Documentos/modelsDocumento.js
var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.Models = SUPER.SIC.Models || {};

SUPER.SIC.Models.Documento = (function () {

    'use strict';

    function Documento(item) {

        this.ta210_iddocupreventa = item.ta210_iddocupreventa || 0;
        this.t2_iddocumento = item.t2_iddocumento || null;
        this.ta210_destino = item.ta210_destino || "";
        this.ta210_descripcion = item.ta210_descripcion || "";
        this.ta210_nombrefichero = item.ta210_nombrefichero || "";
        this.ta210_kbytes = item.ta210_kbytes || 0;
        this.ta210_fechamod = item.ta210_fechamod || new Date();
        this.ta204_idaccionpreventa = item.ta204_idaccionpreventa || null;
        this.ta207_idtareapreventa = item.ta207_idtareapreventa || null;
        this.ta207_denominacion = item.ta207_denominacion || "";
        this.t001_idficepi_autor = item.t001_idficepi_autor || 0;
        this.ta211_idtipodocumento = item.ta211_idtipodocumento || 0;
        this.ta211_denominacion = item.ta211_denominacion || "";
        this.ta210_guidprovisional = item.ta210_guidprovisional || null;
        this.autor = item.autor || "";
        this.fileupdated = item.fileupdated || false;
        this.editable = item.editable || false;
        this.container = item.container || "";
        this.origenEdicion = item.origenEdicion || "";
        this.estado = item.estado || "";
    }

    Documento.fromJson = function (item) {
        return new Documento(item);
    };

    Documento.create = function () {
        return new Documento({});
    }

    Documento.prototype.equals = function (other) {
        return this.ta210_iddocupreventa === other.ta210_iddocupreventa;
    };

    return Documento;
})();
///#source 1 1 /Capa_Presentacion/SIC/Documentos/modelsDocumentoList.js
var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.Models = SUPER.SIC.Models || {};

SUPER.SIC.Models.DocumentoList = (function () {

    'use strict';

    function DocumentoList(documentos) {
        this._documentos = documentos;
    }

    DocumentoList.create = function (documentos) {
        return new DocumentoList(documentos);
    };

    DocumentoList.prototype.all = function () {
        return this._documentos;
    };

    DocumentoList.prototype.size = function () {
        return this._documentos.length;
    };

    DocumentoList.prototype.add = function (documento) {
        this._documentos.push(documento);
    }

    DocumentoList.prototype.update = function (documento) {
        $.extend(this.get(documento.ta210_iddocupreventa), documento);
    }

    DocumentoList.prototype.delete = function (item) {
        var index = this._documentos.indexOf(item);
        return this._documentos.splice(index, 1);
    }

    DocumentoList.prototype.get = function (id) {

        var arr = this._documentos.filter(function (o) {
            return o.ta210_iddocupreventa == id
        })

        if (arr.length == 0)
            return null;
        else if (arr.length == 1)
            return arr[0];
        else
            throw ("Multiple items with same id.");
    }

    return DocumentoList;
})();
///#source 1 1 /Capa_Presentacion/SIC/Documentos/viewDocumentos.js
var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.viewDocumentos = (function (models) {

    var _propietario; // tareapreventa || accionpreventa
    var _idpropietario;
    var _modoAccion //Contenedor de documentos de la acción --> E=edicion || C=consulta
    var _modoTarea  //Contenedor de documentos de la tarea --> E=edicion || C=consulta
    var _modoEdicion; //A=Alta || E=Modificación
    var _esGUID;

    var _renderOptions;

    var _oUploadedFile = null; //utilizado por el upload plugin --> si tiene valor se ha actualizado el documento.
    var _uploading = false //para controlar si hay una subida en curso.

    var _oDocVisualizando; //Documento que se está visualizando en el detalle de la ventana principal



    var dom = {

        modalCatalogo: $("#modalDocumentacionCatalogo"),
        modalEdicion: $("#modalDocumentacionEdicion"),
        detalleDocumento: $("#divDetalleDocumento"),
        litab1: $("#litab1"),
        litab2: $("#litab2"),
        divtab1: $("#divtab1"),
        divtab2: $("#divtab2"),
        listaDocumentosAdd: $("#divListaDocumentosAdd"),
        listaDocumentosRead: $("#divListaDocumentosRead"),
        ifrmDownloadDoc: $("#ifrmDownloadDoc"),
        tabAccionCount: $("#tabAccionCount"),
        tabTareaCount: $("#tabTareaCount"),
        chkMostrarOperativos: $("#chkMostrarOperativos"),
        chkMostrarResultado: $("#chkMostrarResultado"),

        docCard: function (o) {

            var s = new StringBuilder();

            s.append("<div><a class='list-group-item fk_documentoCard' style='display:block' href='#' data-ta210_iddocupreventa='" + o.ta210_iddocupreventa + "' data-ta210_destino='" + o.ta210_destino + "'>");
            s.append("	<div class='row'>");
            s.append("		<div class='col-sm-12'>");
            s.append(imagenExtension(o.ta210_nombrefichero));
            if (!o.editable)
                s.append("<i class='fa fa-lock pull-right' aria-hidden='true'></i>");
            s.append("			<h4 class='list-group-item-heading'>" + o.ta210_nombrefichero + "</h4>");
            s.append("			<span class='list-group-item-text'>" + moment(o.ta210_fechamod).format("DD/MM/YYYY") + "</span>");
            s.append("			|");
            s.append("			<span>" + getTextoDestino(o.ta210_destino) + "</span>");
            s.append("		</div>");
            s.append("	</div>");
            s.append("	<br />");
            s.append("	<div class='row'>");
            s.append("		<div class='col-sm-12'>");
            s.append("			<p class='list-group-item-text'>" + o.ta210_descripcion.trunc(42) + "</p>");
            s.append("		</div>");
            s.append("	</div>");
            s.append("</a></div>");

            return s.toString();
        },

        docCardExt: function (o) {

            var s = new StringBuilder();

            s.append("<div><a class='list-group-item fk_documentoCard' style='display:block' href='#' data-ta210_iddocupreventa='" + o.ta210_iddocupreventa + "' data-ta210_destino='" + o.ta210_destino + "'>");
            s.append("	<div class='row'>");
            s.append("		<div class='col-sm-12'>");
            s.append(imagenExtension(o.ta210_nombrefichero));
            if (!o.editable)
                s.append("<i class='fa fa-lock pull-right' aria-hidden='true'></i>");
            s.append("			<h4 class='list-group-item-heading'>" + o.ta210_nombrefichero + "</h4>");
            s.append("			<span class='list-group-item-text'>" + moment(o.ta210_fechamod).format("DD/MM/YYYY") + "</span>");
            s.append("			|");
            s.append("			<span>" + getTextoDestino(o.ta210_destino) + "</span>");
            s.append("		</div>");
            s.append("	</div>");
            s.append("	<br />");
            s.append("	<div class='row'>");
            s.append("		<div class='col-sm-12'>");
            s.append("			<p class='list-group-item-text'>" + o.ta210_descripcion.trunc(42) + "</p>");
            s.append("			<p class='list-group-item-text text-primary'>" + o.ta207_denominacion.trunc(42) + "</p>");
            s.append("		</div>");
            s.append("	</div>");
            s.append("</a></div>");

            return s.toString();
        },

        cmboption: function (data) {
            if (data.Key === "") {
                return "<option value='' selected hidden>" + data.Value; + "</option>";
            }
            else {
                return "<option value='" + data.Key + "'>" + data.Value; + "</option>";
            }
        },

        destino: undefined,
        documento: undefined, //se inicializan tras cargar la modal de edicion
        autor: undefined,
        fileuploaderplugin: undefined,
        tipodocumento: undefined,
        descripcion: undefined,
    }



    var selectores = {
        //modal principal
        modalCatalogo: "#modalDocumentacionCatalogo",


        //tarjeta de documento en el catalogo
        documentoCard: ".fk_documentoCard",
        nuevo: ".fk_btnAdjuntar", //Boton "nuevo documento" en el contenedor superior del catalogo
        grabar: ".fk_mde-btnGrabar" //boton grabar en la ventana modal edición de documento

        //descarga:   ".fa-download",
        //edicion:    ".fa-pencil",
        //borrado:    ".fa-trash",

    }


    var dd_selectores = {
        btnDescargar: "#dd_btnDescargar",
        btnEditar: "#dd_btnEditar",
        btnEliminar: "#dd_btnEliminar"
    }
    //Detalle de documento en ventana principal


    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function setParams(propietario, esGUID) {

        _propietario = propietario;
        _esGUID = esGUID;
    }

    function init(lstTipoDocumento) {

        var defer = $.Deferred();

        if (dom.modalCatalogo.length == 0) {


            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: IB.vars.strserver + "Capa_Presentacion/SIC/Documentos/StyleSheet.css"
            }).appendTo("head");

            $.get(IB.vars.strserver + "Capa_Presentacion/SIC/Documentos/Template.html?20171128_02", function (html) {

                $("body").append(html);

                var o = {
                    //modal catalogo
                    modalCatalogo: $("#modalDocumentacionCatalogo"),
                    litab1: $("#litab1"),
                    litab2: $("#litab2"),
                    divtab1: $("#divtab1"),
                    divtab2: $("#divtab2"),
                    listaDocumentosAdd: $("#divListaDocumentosAdd"),
                    listaDocumentosRead: $("#divListaDocumentosRead"),
                    detalleDocumento: $("#divDetalleDocumento"),
                    tabAccionCount: $("#tabAccionCount"),
                    tabTareaCount: $("#tabTareaCount"),
                    chkMostrarOperativos: $("#chkMostrarOperativos"),
                    chkMostrarResultado: $("#chkMostrarResultado"),


                    //Modal edicion/alta
                    modalEdicion: $("#modalDocumentacionEdicion"),
                    destino: $("input[name='mde-destino']"),
                    documento: $("#mde-documento"),
                    autor: $("#mde-autor"),
                    fileuploaderplugin: $("#mde-fileuploaderplugin"),
                    tipodocumento: $("#mde-tipodocumento"),
                    descripcion: $("#mde-descripcion")
                }

                $.extend(dom, o);

                //inicializar el detalle de documento de la pantalla principal
                detalleDocumento(models.Documento.create());

                dom.detalleDocumento.on("click", ".fk_download", descargaDocumento)
                dom.modalEdicion.on("click", ".fa-download", descargaDocumento)
                dom.chkMostrarOperativos.on("change", function () { setFiltro(); setDocumentosCount(); });
                dom.chkMostrarResultado.on("change", function () { setFiltro(); setDocumentosCount(); });

                dom.modalEdicion.find(":required").on("keyup change", function () {
                    var obj = $(this);
                    try {
                        if (obj.val() != null && obj.val().length > 0) obj.removeClass("requerido");
                    }
                    catch (e) {
                        console.log(obj);
                    }
                })


                //cargar combo de tipos de documento
                renderCombo(lstTipoDocumento, dom.tipodocumento);

                initUploadPlugin();

                defer.resolve();

            });

            $("body").append("<iframe id='ifrmDownloadDoc' scrolling='no' marginheight='0' marginwidth='0' frameborder='0' style='display: none;'></iframe>");
            dom.ifrmDownloadDoc = $("#ifrmDownloadDoc");
        }
        else {
            defer.resolve();
        }

        return defer.promise();


    }


    //* CATALOGO DE DOCUMENTOS *//

    //renderCatalogo(o)
    // o = {
    //  containerAccion: {
    //    lstDocumentos: lista de documentos a pintar,
    //    titulo: "Documentación asociada",
    //    modo: E=Edicion C=Consulta,
    //    ext: pinta documentos con información extendida (docCardExt) true/false
    //  },
    //  containerTarea: {
    //    lstDocumentos: lista de documentos a pintar,
    //    titulo: "Documentación asociada",
    //    modo: E=Edicion C=Consulta,
    //    ext: pinta documentos con información extendida (docCardExt) true/false
    //  }
    //}
    function renderCatalogo(o) {

        var htmlContainerContent;
        var docCard;
        var s;

        _renderOptions = o;


        //Seleccionar pestaña por defecto.
        dom.litab1.css("display", "block");
        dom.litab2.css("display", "block");
        dom.litab1.removeClass("active");
        dom.litab2.removeClass("active");
        dom.divtab1.removeClass("active");
        dom.divtab2.removeClass("active");

        if (_propietario == "accionpreventa") {
            dom.litab1.addClass("active");
            dom.divtab1.addClass("active");
            if (_esGUID) dom.litab2.css("display", "none");
        }
        else {
            dom.litab2.addClass("active");
            dom.divtab2.addClass("active");
            if (_esGUID) dom.litab1.css("display", "none");
        }

        dom.divtab1.html("");
        dom.divtab2.html("");

        //Container de acciones
        if (typeof o.containerAccion !== "undefined") {
            htmlContainerContent = o.containerAccion.modo == "E" ? dom.listaDocumentosAdd.html() : dom.listaDocumentosRead.html();
            docCard = o.containerAccion.ext == true ? dom.docCardExt : dom.docCard;

            s = new StringBuilder();

            o.containerAccion.lstDocumentos.all().map(docCard)
                .forEach(function (card) {
                    s.append(card);
                })

            dom.divtab1.append(htmlContainerContent);
            dom.divtab1.find(".list-group").append(s.toString());
            dom.divtab1.find(".fk_title").text(o.containerAccion.titulo);
            dom.divtab1.find(".fk_btnAdjuntar").attr("data-container", "accionpreventa");

            if (o.containerAccion.lstDocumentos.size() == 0)
                dom.divtab1.find(".fk_nodoc").css("display", "block");
            else
                dom.divtab1.find(".fk_nodoc").css("display", "none");
        }


        //Container de tarea
        if (typeof o.containerTarea !== "undefined") {
            htmlContainerContent = o.containerTarea.modo == "E" ? dom.listaDocumentosAdd.html() : dom.listaDocumentosRead.html();
            docCard = o.containerTarea.ext == true ? dom.docCardExt : dom.docCard;

            s = new StringBuilder();

            o.containerTarea.lstDocumentos.all().map(docCard)
                .forEach(function (card) {
                    s.append(card);
                })

            dom.divtab2.append(htmlContainerContent);
            dom.divtab2.find(".list-group").append(s.toString());
            dom.divtab2.find(".fk_title").text(o.containerTarea.titulo);
            dom.divtab2.find(".fk_btnAdjuntar").attr("data-container", "tareapreventa");

            if (o.containerTarea.lstDocumentos.size() == 0)
                dom.divtab2.find(".fk_nodoc").css("display", "block");
            else
                dom.divtab2.find(".fk_nodoc").css("display", "none");
        }

        setFiltro();
        setDocumentosCount();


    }

    function setFiltro() {

        if (dom.chkMostrarOperativos.is(":checked")) {
            dom.divtab1.find("a.list-group-item[data-ta210_destino='O']").css("display", "block");
            dom.divtab2.find("a.list-group-item[data-ta210_destino='O']").css("display", "block");
        }
        else {
            dom.divtab1.find("a.list-group-item[data-ta210_destino='O']").css("display", "none");
            dom.divtab2.find("a.list-group-item[data-ta210_destino='O']").css("display", "none");
        }

        if (dom.chkMostrarResultado.is(":checked")) {
            dom.divtab1.find("a.list-group-item[data-ta210_destino='R']").css("display", "block");
            dom.divtab2.find("a.list-group-item[data-ta210_destino='R']").css("display", "block");
        }
        else {
            dom.divtab1.find("a.list-group-item[data-ta210_destino='R']").css("display", "none");
            dom.divtab2.find("a.list-group-item[data-ta210_destino='R']").css("display", "none");
        }


    }

    function showModalCatalogo() {

        dom.modalCatalogo.modal("show");
    }

    function addFichaDocumento(oDoc, container) {

        var o = $(dom.docCard(oDoc)).css("display", "none");
        if (container == "accionpreventa") {
            dom.divtab1.find(".list-group").prepend(o);
        }
        else if (container == "tareapreventa") {
            dom.divtab2.find(".list-group").prepend(o);
        }
        dom.modalCatalogo.find("a.fk_documentoCard[data-ta210_iddocupreventa='" + oDoc.ta210_iddocupreventa + "']").parent().slideDown(function () { setDocumentosCount(); });

        setDocumentosCount();
    }

    function updateFichaDocumento(oDoc) {

        //Obtener la plantilla html con la que pintar las propiedades del documento.
        var ext;
        if (oDoc.container == "accionpreventa")
            ext = _renderOptions.containerAccion.ext;
        else if (oDoc.container == "tareapreventa")
            ext = _renderOptions.containerTarea.ext;

        var docCard = ext == true ? dom.docCardExt : dom.docCard;
        var container = dom.modalCatalogo.find(".fk_documentoCard[data-ta210_iddocupreventa=" + oDoc.ta210_iddocupreventa + "]").parent();
        $(container).html(docCard(oDoc)).css("display", "none").fadeIn("slow");
    }

    function eliminarDocumento(ta210_iddocupreventa) {

        dom.modalCatalogo.find("a.fk_documentoCard[data-ta210_iddocupreventa='" + ta210_iddocupreventa + "']").parent().slideUp(function () { $(this).remove(); setDocumentosCount(); });
        detalleDocumento(models.Documento.create()); //limpiar el detalle

        setDocumentosCount();
    }

    function selectDocCatalogo(ta210_iddocupreventa) {

        var docCard = dom.modalCatalogo.find(".fk_documentoCard[data-ta210_iddocupreventa=" + ta210_iddocupreventa + "]");
        docCard.trigger("click");
    }

    function getTextoDestino(s) {

        switch (s) {
            case "O": return "Operativo"; break;
            case "R": return "Resultado"; break;
        }

    }

    function imagenExtension(filename) {

        var extension = filename.substr(filename.lastIndexOf('.') + 1);
        var htmlFontAwsome = "";

        switch (extension) {
            case "pdf":
                htmlFontAwsome = "<i class='fa fa-3x fa-file-pdf-o text-danger pull-left' aria-hidden='true'></i>";
                break;

            case "xls":
            case "xlsx":
                htmlFontAwsome = "<i class='fa fa-3x fa-file-excel-o text-success pull-left' aria-hidden='true'></i>";
                break;

            case "doc":
            case "docx":
                htmlFontAwsome = "<i class='fa fa-3x fa-file-word-o text-primary pull-left' aria-hidden='true'></i>";
                break;

            case "ppt":
            case "pptx":
                htmlFontAwsome = "<i class='fa fa-3x fa-file-powerpoint-o text-primary pull-left' aria-hidden='true'></i>";
                break;

            case "zip":
            case "rar":
                htmlFontAwsome = "<i class='fa fa-3x fa-file-archive-o text-primary pull-left' aria-hidden='true'></i>";
                break;

            default:
                htmlFontAwsome = "<i class='fa fa-3x fa-file-o text-primary pull-left' aria-hidden='true'></i>";
                break;
        }

        return htmlFontAwsome;
    }

    function setDocumentosCount() {

        dom.tabAccionCount.text(dom.divtab1.find("a.list-group-item").filter(function () {
            return this.style && this.style.display === 'block'
        }).length);
        dom.tabTareaCount.text(dom.divtab2.find("a.list-group-item").filter(function () {
            return this.style && this.style.display === 'block'
        }).length);
    }

    function resaltarDocumento(ta210_iddocupreventa) {

        dom.modalCatalogo.find("a.list-group-item").removeClass("active");
        dom.modalCatalogo.find("a.list-group-item[data-ta210_iddocupreventa='" + ta210_iddocupreventa + "']").addClass("active");

    }

    //* CATALOGO DE DOCUMENTOS *//



    //*DETALLE DE DOCUMENTO EN VENTANA PRINCIPAL *//

    function detalleDocumento(oDoc) {

        $("#dd_nombre").text(oDoc.ta210_nombrefichero);
        $("#dd_tamano").text(oDoc.ta210_kbytes.toLocaleString() + " KB");
        $("#dd_fecha").text(moment(oDoc.ta210_fechamod).format("DD/MM/YYYY HH:mm"));
        $("#dd_autor").text(oDoc.autor);
        $("#dd_destino").text(oDoc.ta210_destino == "O" ? "Operativo" : "Resultado");
        $("#dd_tipo").text(oDoc.ta211_denominacion);
        $("#dd_descripcion").text(oDoc.ta210_descripcion);

        if (!oDoc.editable) {
            $(dd_selectores.btnEditar).attr("disabled", "disabled").addClass("disabled");
            $(dd_selectores.btnEliminar).attr("disabled", "disabled").addClass("disabled");
        }
        else {
            $(dd_selectores.btnEditar).removeAttr("disabled", "disabled").removeClass("disabled");
            $(dd_selectores.btnEliminar).removeAttr("disabled", "disabled").removeClass("disabled");
        }

        if (oDoc.ta210_iddocupreventa == 0) {
            $("#divDetalleDocumento").css("display", "none");
            _oDocVisualizando = null;
        }
        else {
            $("#divDetalleDocumento").css("display", "none").fadeIn();
            _oDocVisualizando = oDoc
        }

    }

    function descargaDocumento() {

        var url = IB.vars.strserver + "Capa_Presentacion/SIC/Documentos/FileDownload.ashx?" + IB.uri.encode("t2_iddocumento=" + _oDocVisualizando.t2_iddocumento);
        dom.ifrmDownloadDoc.prop("src", url).submit();
    }
    //*DETALLE DE DOCUMENTO EN VENTANA PRINCIPAL *//



    //* MODAL EDICION DE DOCUMENTO *//
    function initUploadPlugin() {

        var d1 = $.Deferred();

        if (typeof $.fn.uploadFile === "undefined") {
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: IB.vars.strserver + "plugins/jqueryuploadfile/uploadfile.css"
            }).appendTo("head");
            $.getScript(IB.vars.strserver + "plugins/jqueryuploadfile/jquery.uploadfile.js", function () { d1.resolve(); console.log("jquery.uploadfile loaded") });
        }
        else {
            d1.resolve();
        }

        $.when(d1).then(function () {
            console.log("initUploadPlugin()");
            dom.fileuploaderplugin.uploadFile({
                url: IB.vars.strserver + "Capa_Presentacion/SIC/Documentos/FileUpload.ashx",
                //dynamicFormData: function () { return fileProp; },
                autoSubmit: true,
                multiple: false,
                maxFileCount: 1,
                dragDrop: true,
                fileName: "myfile",
                returnType: "json",
                statusBarWidth: "100%",
                dragdropWidth: "100%",
                maxFileSize: 50 * 1024 * 1024, //50MB
                showPreview: false,
                showStatusAfterSuccess: true,
                showStatusAfterError: false,
                showFileCounter: false,
                showProgress: true,
                showDownload: false,
                showDelete: true,
                customErrorKeyStr: "jquery-upload-file-error",

                onSuccess: function (files, data, xhr, pd) {
                    _oUploadedFile = data;
                    _uploading = false;
                },

                deleteCallback: function (data) {
                    _oUploadedFile = null;
                },

                onSubmit: function (files) {
                    dom.fileuploaderplugin.removeClass("requerido")
                    _uploading = true;
                },
                onError: function (files, status, message, pd) {
                    IB.bserror.mostrarErrorAplicacion("Documentación", "Ha ocurrido un error subiendo el documento al servidor");
                    _uploading = false;
                },

            });
        })

    }

    function editarDocumento(o, modoEdicion) {

        _modoEdicion = modoEdicion


        _oUploadedFile = null;
        dom.fileuploaderplugin.reset();

        dom.tipodocumento.removeClass("requerido");
        dom.descripcion.removeClass("requerido");

        if (modoEdicion == "E") {
            dom.destino.filter("[value='" + o.ta210_destino + "']").prop('checked', true);
            dom.documento.val(o.ta210_nombrefichero + " (" + o.ta210_kbytes.toLocaleString() + " KB)");
            dom.autor.val(o.autor + " " + moment(o.ta210_fechamod).format("DD/MM/YYYY HH:mm"));
            dom.tipodocumento.val(o.ta211_idtipodocumento);
            dom.descripcion.val(o.ta210_descripcion);

            dom.modalEdicion.find(".modal-title").text("Edición de ficha de documento");
        }
        else {
            dom.destino.filter("[value='O']").prop('checked', true);
            dom.documento.val("");
            dom.autor.val("");
            if(dom.tipodocumento.find("option").length == 1)
                dom.tipodocumento.val(dom.tipodocumento.find("option:first").val());
            else
                dom.tipodocumento.val("");
            dom.descripcion.val("");

            dom.modalEdicion.find(".modal-title").text("Adjuntar nuevo documento");
        }

        dom.modalEdicion.modal("show");
    }

    function getModalEdicionValues() {

        var o = models.Documento.create();

        if (_oUploadedFile != null) {
            o.t2_iddocumento = _oUploadedFile.t2_iddocumento;
            o.ta210_nombrefichero = _oUploadedFile.name;
            o.ta210_kbytes = _oUploadedFile.size;
        }

        o.ta210_destino = dom.destino.filter(":checked").val();
        o.ta211_idtipodocumento = dom.tipodocumento.val();
        o.ta210_descripcion = dom.descripcion.val();

        return o;
    }

    function validarEdicion() {


        if (_uploading) {
            IB.bsalert.toastdanger("Espera a que finalice la subida del documento al servidor.");
            return false;
        }

        var valid = true;
        var filevalid = true;

        var o = getModalEdicionValues();
        if (typeof o.ta210_destino === "undefined") valid = false;
        if (o.ta211_idtipodocumento.length == 0) {
            dom.tipodocumento.addClass("requerido")
            valid = false;
        } 
        if (_modoEdicion == "A" && _oUploadedFile == null)
        {
            dom.fileuploaderplugin.addClass("requerido");
            filevalid = false;
        }


        var msg = "";
        if (!valid && !filevalid) msg = "Debes cumplimentar los campos obligatorios y seleccionar un documento.";
        else if (!valid) msg = "Debes cumplimentar los campos obligatorios.";
        else if (!filevalid) msg = "Debes seleccionar un documento."

        if (msg.length > 0) IB.bsalert.toastdanger(msg);

        return valid && filevalid;
    }

    function closeModalEdicion() {

        dom.modalEdicion.modal("hide");
    }
    //* MODAL EDICION DE DOCUMENTO *//



    //* COMBOS *//
    function renderCombo(lstOptions, el, selectedId) {

        console.log("renderCombo " + el.prop("id"));

        if (lstOptions.length > 1)
            lstOptions.splice(0, 0, { Key: "", Value: "Selecciona..." });

        clearComboOptions(el);
        lstOptions
            .map(dom.cmboption)
            .forEach(function (option) {
                el.append(option);
            });

        if (selectedId != undefined && selectedId != null) {
            el.val(selectedId);
            el.trigger("change");
        }
        else if (lstOptions.length == 1) {
            el.find("option").attr("selected", "selected");
            el.trigger("change");
        }

    }

    function clearComboOptions(el) {

        var antValue = el.val();

        el.find('option').remove();
        el.val("");

        if (antValue != null && antValue != "") el.trigger("change");
    }
    //* COMBOS *//


    return {
        selectores: selectores,
        dd_selectores: dd_selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        setParams: setParams,
        init: init,
        renderCatalogo: renderCatalogo,
        showModalCatalogo: showModalCatalogo,
        closeModalEdicion: closeModalEdicion,
        detalleDocumento: detalleDocumento,
        editarDocumento: editarDocumento,
        eliminarDocumento: eliminarDocumento,
        getModalEdicionValues: getModalEdicionValues,
        validarEdicion: validarEdicion,
        addFichaDocumento: addFichaDocumento,
        updateFichaDocumento: updateFichaDocumento,
        selectDocCatalogo: selectDocCatalogo,
        resaltarDocumento: resaltarDocumento
    }

})(SUPER.SIC.Models);
///#source 1 1 /Capa_Presentacion/SIC/Documentos/appDocumentos.js
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
