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
            if(!o.editable)
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

            $.get(IB.vars.strserver + "Capa_Presentacion/SIC/Documentos/Template.html", function (html) {

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
                showStatusAfterError: true,
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
                    _uploading = true;
                },
                onError: function (files, status, message, pd) {
                    _uploading = false;
                },

            });
        })

    }

    function editarDocumento(o, modoEdicion) {

        _modoEdicion = modoEdicion


        _oUploadedFile = null;
        dom.fileuploaderplugin.reset();

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
        if (o.ta211_idtipodocumento.length == 0) valid = false;
        if (o.ta210_descripcion.length == 0) valid = false;
        if (_modoEdicion == "A" && _oUploadedFile == null) filevalid = false;


        var msg = "";
        if (!valid && !filevalid) msg = "Debes cumplimentar todos los campos y seleccionar un documento.";
        else if (!valid) msg = "Debes cumplimentar todos los campos.";
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