$(document).ready(function () {

    //var d = SUPER.SIC.appDocumentos.initTarea(8, "E", "E", 7583, false);
    var d = SUPER.SIC.appDocumentos.initAccion(4, "E", "E", 7583, false);
    //var d = SUPER.SIC.appDocumentos.initTarea("8746E89A-CA5E-4774-9C4F-6A3808F8098C", "C", "E", 7583, true);
    //var d = SUPER.SIC.appDocumentos.initAccion("2E3AF78D-2191-4224-A071-C988E3FA3C70", "E", "C", 7583, true);
    $.when(d).then(SUPER.SIC.appDocumentos.show);


    $("#btn1").on("click", function () {
        var d = SUPER.SIC.appDocumentos.initTarea(8, "C", "E", 7583, true);
        $.when(d).then(SUPER.SIC.appDocumentos.show);
    })

    $("#btn2").on("click", function () {
        var d = SUPER.SIC.appDocumentos.initTarea(8, "C", "C", 7583, true);
        $.when(d).then(SUPER.SIC.appDocumentos.show);
    })

    $("#btn3").on("click", function () {
        var d = SUPER.SIC.appDocumentos.initAccion(4, "E", "C", 7583, true);
        $.when(d).then(SUPER.SIC.appDocumentos.show);
    })


    $("#btn4").on("click", function () {
        var d = SUPER.SIC.appDocumentos.initAccion(4, "C", "C", 7583, true);
        $.when(d).then(SUPER.SIC.appDocumentos.show);
    })


    $("#btn11").on("click", function () {
        SUPER.SIC.appDocumentos.initTarea(8, "C", "C", 7583, true);
        SUPER.SIC.appDocumentos.count().then(function (data) { alert(data); })
    })

    $("#btn13").on("click", function () {
        var d = SUPER.SIC.appDocumentos.initAccion(4, "C", "C", 7583, true);
        SUPER.SIC.appDocumentos.count().then(function (data) { alert(data); })
    })


    $("#btn21").on("click", function () {
        var d = SUPER.SIC.appDocumentos.initTarea("8746E89A-CA5E-4774-9C4F-6A3808F8098C", "C", "E", 7583, true);
        $.when(d).then(SUPER.SIC.appDocumentos.show);
    })

    $("#btn22").on("click", function () {
        var d = SUPER.SIC.appDocumentos.initAccion("2E3AF78D-2191-4224-A071-C988E3FA3C70", "E", "C", 7583, true);
        $.when(d).then(SUPER.SIC.appDocumentos.show);
    })


    SUPER.SIC.appDocumentos.onClose(function (data) {
        alert(data);
    })
    
    //SUPER.SIC.app.init("tareapreventa");
});

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app = (function () {


   

    //    $(".fa-pencil").on("click", function () {

    //        $("#modalEditarDocumento").modal("show");

    //    });

    var fileProp = function () {
        this.tipodocumento = null;
        this.t2_iddocumento = null;
        this.name = null;
        this.size = null;
        this.summary = null;
    }


    var t2_iddocumento = null;
    

    function init(tipodocumento) {

        //tipodocumento = tareapreventa || accionpreventa
        fileProp.tipodocumento = tipodocumento;

        $("#fileuploaderplugin").uploadFile({
            url: "FileUpload.ashx",
            dynamicFormData: function () { return fileProp; },
            autoSubmit: true,
            multiple: false,
            maxFileCount: 1,
            dragDrop: true,
            fileName: "myfile",
            returnType: "json",
            statusBarWidth: "100%"  ,
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

                t2_iddocumento = data.t2_iddocumento;
                //pd.progressDiv.hide();
                //$(pd.filename).attr("data-t2_iddocumento", data.t2_iddocumento);

            },

            deleteCallback: function (data) {

                t2_iddocumento = null;

                //var url = "./FileDelete.ashx?" + IB.uri.encode("t2_iddocumento=" + data.t2_iddocumento);
                //$("#fileuploader-ifrmDownloadDoc").prop("src", url).submit();
                //pd.statusbar.hide(); //You choice.

            },

            downloadCallback: function (data) {
                var url = "./FileDownload.ashx?" + IB.uri.encode("t2_iddocumento=" + data.t2_iddocumento);
                $("#fileuploader-ifrmDownloadDoc").prop("src", url).submit();
            },

            onSubmit: function (files) {
                fileProp.filename = "alsdjlasdkjas";
                fileProp.tipo = ".pdf";
            },

            onSelect: function (files) {
                return true;
            }

        });


    }
  
    


    return {
        init: init
    }

})()