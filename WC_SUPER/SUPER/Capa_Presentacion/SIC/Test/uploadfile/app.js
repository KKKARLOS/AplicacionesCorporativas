$(document).ready(function () {

    IB.app.init();
});

var IB = IB || {}
IB.app = (function () {

    function init(){

        var options = {
            url: "/file/post",
            createImageThumbnails: false,
            parallelUploads: 1,
            maxFilesize: 50,
            acceptedFiles: ".doc,.txt,.pdf,.zip,.rar",
            dictInvalidFileType: "Formato de fichero no permitido. Los ficheros admitidos son doc, txt, pdf",
            dictFileTooBig: "El tamaño máximo de fichero permitido es {{maxFilesize}} MB.",

            //init: function () {
            //    this.on("addedfile", function (file) { alert("Added file."); });
            //    this.on("success", function (file) { alert("success file."); });
            //    this.on("error", function (file) { alert("error file."); });
            //},

            //previewsContainer: ".dropzone-previews",

        }
        $("#dropzone").dropzone({ url: "/file/post" });
    }


    return {
        init: init
    }


})()