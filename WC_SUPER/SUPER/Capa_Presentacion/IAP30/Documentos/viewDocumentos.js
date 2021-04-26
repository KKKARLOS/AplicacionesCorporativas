var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};

SUPER.IAP30.viewDocumentos = (function (models) {

    var _propietario; // detalletarea || ...
    var _idpropietario;
    var _modoAccion //Contenedor de documentos de la acción --> E=edicion || C=consulta
    var _modoTarea  //Contenedor de documentos de la tarea --> E=edicion || C=consulta
    var _modoEdicion; //A=Alta || E=Modificación
    var _esGUID;

    var _renderOptions;

    var _oUploadedFile = null; //utilizado por el upload plugin --> si tiene valor se ha actualizado el documento.
    var _uploading = false //para controlar si hay una subida en curso.

    var _oDocVisualizando; //Documento que se está visualizando en el detalle de la ventana principal

    if (typeof $.fn.uploadFile === "undefined") {
        $("<link/>", {
            rel: "stylesheet",
            type: "text/css",
            href: IB.vars.strserver + "plugins/jqueryuploadfile/uploadfile.css"
        }).appendTo("head");
        $.getScript(IB.vars.strserver + "plugins/jqueryuploadfile/jquery.uploadfile.js");
    }

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

        docCard: function (o) {

            var s = new StringBuilder();

            s.append("<div><a title='Ver detalle de " + o.nombrearchivo + "' class='list-group-item fk_documentoCard' style='display:block' href='#' data-idDocumento='" + o.idDocumento + "' data-ta210_destino=''>");
            s.append("	<div class='row'>");
            s.append("		<div class='col-sm-12'>");
            s.append(imagenExtension(o.nombrearchivo));
            if(!o.editable)
                s.append("<i class='fa fa-lock pull-right' aria-hidden='true'></i>");
            s.append("			<h4 class='list-group-item-heading'>" + o.nombrearchivo + "</h4>");
            s.append("			<span class='list-group-item-text'>" + moment(o.fechamodif).format("DD/MM/YYYY") + "</span>");
             s.append("		</div>");
            s.append("	</div>");
            s.append("	<br />");
            s.append("	<div class='row'>");
            s.append("		<div class='col-sm-12'>");
            s.append("			<p class='list-group-item-text'>" + o.descripcion.trunc(42) + "</p>");
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
            s.append(imagenExtension(o.nombrearchivo));
            if (!o.editable)
                s.append("<i class='fa fa-lock pull-right' aria-hidden='true'></i>");
            s.append("			<h4 class='list-group-item-heading'>" + o.nombrearchivo + "</h4>");
            s.append("			<span class='list-group-item-text'>" + moment(o.fechamodif).format("DD/MM/YYYY") + "</span>");
            s.append("		</div>");
            s.append("	</div>");
            s.append("	<br />");
            s.append("	<div class='row'>");
            s.append("		<div class='col-sm-12'>");
            s.append("			<p class='list-group-item-text'>" + o.descripcion.trunc(42) + "</p>");
            s.append("			<p class='list-group-item-text text-primary'>" + o.nombrearchivo.trunc(42) + "</p>");
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
                href: IB.vars.strserver + "Capa_Presentacion/IAP30/Documentos/StyleSheet.css"
            }).appendTo("head");

            //Cuando carca el html correspondiente al catálogo de documentos, iniciará la carga del html de la modal de edicion/alta de documentos
            $.when($.get(IB.vars.strserver + "Capa_Presentacion/IAP30/Documentos/Template.html", function (html) {

                $("#datosDoc").append(html);
            })).then(function () {

                $.get(IB.vars.strserver + "Capa_Presentacion/IAP30/Documentos/TemplateE.html", function (htmlE) {

                    $("body").append(htmlE);

                    var o = {
                        //modal catalogo
                        modalCatalogo: $("#modalDocumentacionCatalogo"),
                        litab1: $("#litab1"),
                        litab2: $("#litab2"),
                        divtab2: $("#divtab2"),
                        listaDocumentosAdd: $("#divListaDocumentosAdd"),
                        listaDocumentosRead: $("#divListaDocumentosRead"),
                        detalleDocumento: $("#divDetalleDocumento"),
                        tabTareaCount: $("#tabTareaCount"),


                        //Modal edicion/alta
                        modalEdicion: $("#modalDocumentacionEdicion"),
                        destino: $("input[name='mde-destino']"),
                        documento: $("#mde-documento"),
                        autor: $("#mde-autor"),
                        divautormodif: $("#mde-divautormodif"),
                        autormodif: $("#mde-autormodif"),
                        fileuploaderplugin: $("#mde-fileuploaderplugin"),
                        tipodocumento: $("#mde-tipodocumento"),
                        descripcion: $("#mde-descripcion"),
                        weblink: $("#mde-weblink"),
                        chkPrivado: $("#mde-chkPrivado"),
                        chkLectura: $("#mde-chkLectura"),
                        infoOcultable: $('#ocultable2')
                    }

                    $.extend(dom, o);

                    //inicializar el detalle de documento de la pantalla principal
                    detalleDocumento(models.Documento.create());

                    dom.detalleDocumento.on("click", ".fk_download", descargaDocumento)
                    dom.modalEdicion.on("click", ".fa-download", descargaDocumento)                    

                    initUploadPlugin();

                    defer.resolve();
                });
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
        dom.litab2.css("display", "block");        
        
        if (_esGUID) dom.litab1.css("display", "none");
       

        dom.divtab1.html("");
        dom.divtab2.html("");


        //Container de tarea
        if (typeof o.containerElemento !== "undefined") {
            htmlContainerContent = o.containerElemento.modo == "E" ? dom.listaDocumentosAdd.html() : dom.listaDocumentosRead.html();
            docCard = o.containerElemento.ext == true ? dom.docCardExt : dom.docCard;

            s = new StringBuilder();

            o.containerElemento.lstDocumentos.all().map(docCard)
                .forEach(function (card) {
                    s.append(card);
                })

            dom.divtab2.append(htmlContainerContent);
            dom.divtab2.find(".list-group").append(s.toString());
            dom.divtab2.find(".fk_title").text(o.containerElemento.titulo);
            dom.divtab2.find(".fk_btnAdjuntar").attr("data-container", "detalletarea");

            if (o.containerElemento.lstDocumentos.size() == 0)
                dom.divtab2.find(".fk_nodoc").css("display", "block");
            else {
                dom.divtab2.find(".fk_nodoc").css("display", "none");
                selectDocCatalogo(o.containerElemento.lstDocumentos._documentos[0].idDocumento);
            }
        }

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
        dom.modalCatalogo.modal("show");    }

    function addFichaDocumento(oDoc) {

        var o = $(dom.docCard(oDoc)).css("display", "none");
        dom.divtab2.find(".list-group").prepend(o);
        dom.divtab2.find("a.fk_documentoCard[data-idDocumento='" + oDoc.idDocumento + "']").parent().slideDown(function () { setDocumentosCount(); });
        dom.divtab2.find(".fk_nodoc").css("display", "none");
        setDocumentosCount();
    }

    function updateFichaDocumento(oDoc) {

        //Obtener la plantilla html con la que pintar las propiedades del documento.
        var ext = _renderOptions.containerElemento.ext;

        var docCard = ext == true ? dom.docCardExt : dom.docCard;
        var container = dom.divtab2.find(".fk_documentoCard[data-idDocumento=" + oDoc.idDocumento + "]").parent();
        $(container).html(docCard(oDoc)).css("display", "none").fadeIn("slow");
    }

    function eliminarDocumento(idDocumento) {

        dom.divtab2.find("a.fk_documentoCard[data-idDocumento='" + idDocumento + "']").parent().slideUp(function () { $(this).remove(); setDocumentosCount(); });
        detalleDocumento(models.Documento.create()); //limpiar el detalle

        setDocumentosCount();
    }

    function selectDocCatalogo(idDocumento) {

        var docCard = dom.divtab2.find(".fk_documentoCard[data-idDocumento=" + idDocumento + "]");
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

    function resaltarDocumento(idDocumento) {

        dom.divtab2.find("a.list-group-item").removeClass("active");
        dom.divtab2.find("a.list-group-item[data-idDocumento='" + idDocumento + "']").addClass("active");

    }

    //* CATALOGO DE DOCUMENTOS *//



    //*DETALLE DE DOCUMENTO EN VENTANA PRINCIPAL *//

    function detalleDocumento(oDoc) {

        $("#dd_nombre").text(oDoc.nombrearchivo);
        $("#dd_tamano").text(oDoc.kbytes.toLocaleString() + " KB");
        $("#dd_fecha").text(moment(oDoc.fechamodif).format("DD/MM/YYYY HH:mm"));
        $("#dd_weblink").text(oDoc.weblink);
        if (oDoc.weblink != "") $("#dd_weblink").attr("href", oDoc.weblink);
        $("#dd_autor").text(oDoc.autor);
        $("#dd_descripcion").text(oDoc.descripcion);       

        if (!oDoc.editable) {
            $(dd_selectores.btnEditar).attr("disabled", "disabled").addClass("disabled");
            $(dd_selectores.btnEliminar).attr("disabled", "disabled").addClass("disabled");
        }
        else {
            $(dd_selectores.btnEditar).removeAttr("disabled", "disabled").removeClass("disabled");
            $(dd_selectores.btnEliminar).removeAttr("disabled", "disabled").removeClass("disabled");
        }

        if (oDoc.idDocumento == 0) {
            $("#divDetalleDocumento").css("display", "none");
            _oDocVisualizando = null;
        }
        else {
            $("#divDetalleDocumento").css("display", "none").fadeIn();
            _oDocVisualizando = oDoc
        }        
    }

    function descargaDocumento() {

        var url = IB.vars.strserver + "Capa_Presentacion/IAP30/Documentos/FileDownload.ashx?" + IB.uri.encode("t2_iddocumento=" + _oDocVisualizando.t2_iddocumento);
        dom.ifrmDownloadDoc.prop("src", url).submit();
        
    }
    //*DETALLE DE DOCUMENTO EN VENTANA PRINCIPAL *//



    //* MODAL EDICION DE DOCUMENTO *//
    function initUploadPlugin() {

        console.log("initUploadPlugin()");
        dom.fileuploaderplugin.uploadFile({
            url: IB.vars.strserver + "Capa_Presentacion/IAP30/Documentos/FileUpload.ashx",
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
                 data.name = files[0];
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

    }

    function editarDocumento(o, modoEdicion) {

        _modoEdicion = modoEdicion


        _oUploadedFile = null;
        dom.fileuploaderplugin.reset();        

        if (modoEdicion == "E") {
            dom.documento.val(o.nombrearchivo);
            dom.autormodif.val(o.autormodif + " " + moment(o.fechamodif).format("DD/MM/YYYY HH:mm"));
            dom.autor.val(o.autor + " " + moment(o.fecha).format("DD/MM/YYYY HH:mm"));
            //dom.tipodocumento.val(o.ta211_idtipodocumento);
            dom.descripcion.val(o.descripcion);
            dom.weblink.val(o.weblink);
            if (o.modolectura) dom.chkLectura.prop("checked", true)
            else dom.chkLectura.prop('checked', false);
            if (o.privado) dom.chkPrivado.prop("checked", true)
            else dom.chkPrivado.prop('checked', false);
            dom.modalEdicion.find(".modal-title").text("::: SUPER ::: - Edición de ficha de documento");
            dom.divautormodif.css("display", "block");
        }
        else {
            dom.documento.val("");
            dom.autor.val(IB.vars.nombreUsuario);
            dom.autormodif.val("");
            dom.tipodocumento.val("");
            dom.descripcion.val("");
            dom.weblink.val("");
            dom.chkLectura.attr('checked', false);
            dom.chkPrivado.attr('checked', false);
            dom.modalEdicion.find(".modal-title").text("::: SUPER ::: - Adjuntar nuevo documento");
            dom.divautormodif.css("display", "none");

        }
        dom.modalEdicion.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        dom.modalEdicion.modal("show");
        dom.infoOcultable.attr('area-hidden', true);
    }

    function getModalEdicionValues() {

        var o = models.Documento.create();

        if (_oUploadedFile != null) {
            o.t2_iddocumento = _oUploadedFile.t2_iddocumento;
            o.nombrefichero = _oUploadedFile.name;
            o._kbytes = _oUploadedFile.size;
            o.nombrearchivo = _oUploadedFile.name;
        }

        o.idtipodocumento = dom.tipodocumento.val();
        o.descripcion = dom.descripcion.val();
        o.autor = dom.autor.val();
        o.weblink = dom.weblink.val();
        o.modolectura = dom.chkLectura.is(':checked');
        o.privado = dom.chkPrivado.is(':checked');
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
        if (o.descripcion.length == 0) valid = false;
        if (_modoEdicion == "A" && _oUploadedFile == null) filevalid = false;


        var msg = "";
        if (!valid) {
            msg = "Debes indicar la descripción del documento.";
        } else if (!filevalid) msg = "Debes seleccionar un documento."

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

})(SUPER.IAP30.Models);