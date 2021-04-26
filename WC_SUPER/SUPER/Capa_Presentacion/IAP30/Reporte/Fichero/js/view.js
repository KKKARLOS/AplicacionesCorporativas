var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Fichero = SUPER.IAP30.Fichero || {}

SUPER.IAP30.Fichero.View = (function (e) {
    var dom = {
        //inputFichero: $("#fileuploader"),
        btnFormato: $('#btnFormato'),
        btnAnalizar: $('#btnAnalizar'),
        btnVisualizar: $('#btnVisualizar'),
        btnGrabar: $('#btnGrabar'),
        modalFormato: $('#modal-Formato'),
        estructuraMasiva: $('#estructura_masiva'),
        estructuraDia: $('#estructura_dia'),
        tipoFichero: $('input:radio[id^="radio"]:checked'),
        nFilas: $('#nFilas'),
        nFilasC: $('#nFilasC'),
        nFilasE: $('#nFilasE'),
        bodyTabla: $('#bodyTabla'),
        radio: $('input:radio'),
        divTablaV: $('#divTablaV'),
        divTablaVM: $('#divTablaVM'),
        tablaV: $('#tablaV'),
        tablaVM: $('#tablaVM'),
        tablaG: $('#tablaG')        
        //pantalla: $('.ibox-title')
        //,uFichero: $('#uFichero')
};
    var selectores = {
        bodyTabla: "#bodyTabla",
        bodyTablaTR: "#bodyTabla tr",
        bodyTablaM: "#bodyTablaM",
        bodyTablaMTR: "#bodyTablaM tr",
        bodyTabla2: "#bodyTabla2",
        bodyTabla2TR: "#bodyTabla2 tr",
        container: ".container",
        btnExportExcel: ".btnExportExcel",
        btnExportExcel2: ".btnExportExcel2",
        btnExportExcel3: ".btnExportExcel3",
        radio: "input:radio",
        fichero: ".ajax-file-upload-filename",
        nombreFichero: "",
        form: "form"
    }
    var indicadores = {
        i_dispositivoTactil: false
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }
    //para elementos que no existen de inicio
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    $(window).on("load", mostrarPantalla);
    function mostrarPantalla() {
        //alert("Hola");
        //$('.ibox-title').show();
        //$('.ibox-title').removeAttr("style");
        $('.ibox-content').removeAttr("style");
    }
    var init = function () {
        
        inicializarPantalla();
        
    }
    function inicializarPantalla() {

        //Comprobación de si es un dispositivo móvil (en principio para no mostrar los popover)
        if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
            indicadores.i_dispositivoTactil = true;
        }
        //Ocultamos el texto de explicación del formato de la segunda estructura
        $('#estructura_masiva').hide();
        $('#btnGrabar').attr('disabled', 'disabled');
        $('input:radio').removeAttr("disabled");
        //$('.ajax-upload-dragdrop').attr("style", "vertical-align: top;width: 500px;")
        //$('.ajax-upload-dragdrop').attr("width", "500px !important")

        //Para visualizar el contenido se abre un Notepad mediante ActiveX por lo que solo funcionaba en IE
        //Además al usar el plugin para subir ficheros, no tenemos acceso al path completo del archivo por lo
        //que tampoco se abre en IE (si usáramos el input=file a pelo si tendríamos acceso al patah). 
        //Por lo tanto, quito el botón
        //if (navigator.userAgent.match(/msie/i) || navigator.userAgent.match(/trident/i)) {
        //    $('#btnVisualizar').show();
        //}
        verErroresValidacion(null);
        verErroresValidacionM(null);
        verErroresGrabacion(null);

        $("#fileuploader").uploadFile({
            multiple: false,
            maxFileCount: 1,
            showDelete: true,
            dragDropStr: "<span><b>Drag&amp;Drop Arrastra y suelta ficheros en este área</b></span>",
            uploadStr: "Seleccionar",
            abortStr: "Detener",
            cancelStr: "Cancelar",
            deletelStr: "Eliminar",
            doneStr: "Finalizado",
            downloadStr: "Descargar",
            multiDragErrorStr: "El Drag &amp; Drop de varios ficheros simultaneamente no está permitido.",
            extErrorStr: "no está permitido. Las extensiones permitidas son ",
            duplicateErrorStr: "no está permitido. El fichero ya existe.",
            sizeErrorStr: "no está permitido. El tamaño máximo permitido es ",
            uploadErrorStr: "Upload no está permitido",
            maxFileCountErrorStr: " no está permitido. El nº máximo de ficheros permitidos es ",
            //onLoad: function (obj) {alert("hola");},
            onSuccess: function (files, data, xhr, pd) {
                //$("#eventsmessage").html($("#eventsmessage").html() + "<br/>Success for: " + JSON.stringify(data));
                //alert(files);
                $('#btnGrabar').attr('disabled', 'disabled');
                $('input:radio').removeAttr("disabled");
                selectores.nombreFichero = files;
                //dom.fichero = data;
            }
        });
    }
    obtenerValor = function (elemento) {
        return elemento.val();
    }
    function cebrear() {
        $("tr:visible:not(.bg-info)").removeClass("cebreada");
        $('tr:visible:not(.bg-info):even').addClass('cebreada');
        //controlarScroll();
    }
    //Marcado de línea activa
    var marcarLinea = function (thisObj) {
        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        desmarcarLinea($(thisObj).parent());
        $(thisObj).addClass('activa');
    }
    //Desmarcado de línea activa
    var desmarcarLinea = function (tabla) {
        //$("tr.activa").removeClass('activa');
        $(tabla).find('tr.activa').removeClass('activa');
    }

    var verErroresValidacion = function (dataSource) {
        //var dataSource = [
        //    ["Pepe", "15/11/2016", "1234-denominacion tarea", "8,5", "Usuario no existente"]
        //];

        //var columnas = [
        //        { "data": "Usuario" },
        //        {
        //            "data": "Fecha",
        //            "type": "date ",
        //            "render": function (value) {
        //                if (value === null) return "";
        //                return moment(value).format('DD/MM/YYYY');
        //            }
        //        },
        //        { "data": "Tarea" },
        //        { "data": "Esfuerzo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
        //        { "data": "Error" }
        //];
        var columnas = [
                { "data": "Usuario" },
                {
                    "data": "Fecha",
                    "type": "date ",
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                { "data": "Tarea" },
                { "data": "Esfuerzo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "Error" }
        ];

        dom.tblVal = $("#tablaV").DataTable({
            destroy: true,
            "columns": columnas,
            data: dataSource,
            scrollY: "19vh",
            scrollX: true,
            "bScrollCollapse": true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExportExcel',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'EXCEL'
                }
            ]
        });
    }

    var verErroresValidacionM = function (dataSource) {
        //var columnas = [
        //        { "data": "Usuario" },
        //        {
        //            "data": "Fecha",
        //            "type": "date ",
        //            "render": function (value) {
        //                if (value === null) return "";
        //                return moment(value).format('DD/MM/YYYY');
        //            }
        //        },
        //        {
        //            "data": "FechaH",
        //            "type": "date ",
        //            "render": function (value) {
        //                if (value === null) return "";
        //                return moment(value).format('DD/MM/YYYY');
        //            }
        //        },
        //        { "data": "Tarea" },
        //        { "data": "Esfuerzo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
        //        { "data": "Festivos" },
        //        { "data": "Error" }
        //];
        var columnas = [
                { "data": "Usuario" },
                {
                    "data": "Fecha",
                    "type": "date ",
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                {
                    "data": "FechaH",
                    "type": "date ",
                    "render": function (value) {
                        if (value === null) return "";
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                { "data": "Tarea" },
                { "data": "Esfuerzo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "Festivos" },
                { "data": "Error" }
        ];

        dom.tblValM = $("#tablaVM").DataTable({
            destroy: true,
            "columns": columnas,
            data: dataSource,
            scrollY: "19vh",
            scrollX: true,
            "bScrollCollapse": true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExportExcel3',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'EXCEL'
                }
            ]
        });
    }

    var verErroresGrabacion = function (dataSource) {
        var columnas = [
                { "data": "Fila", render: $.fn.dataTable.render.number('.', ',', 0, '') },
                { "data": "Error" }
        ];

        dom.tblGrab = $("#tablaG").DataTable({
            destroy: true,
            "columns": columnas,
            data: dataSource,
            scrollY: "19vh",
            scrollX: true,
            "bScrollCollapse": true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            ordering: true,
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExportExcel2',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'EXCEL'
                }
            ]
        });
    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        indicadores: indicadores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        cebrear: cebrear,
        marcarLinea: marcarLinea,
        verErroresValidacion: verErroresValidacion,
        verErroresValidacionM: verErroresValidacionM,
        verErroresGrabacion: verErroresGrabacion,
        obtenerValor: obtenerValor
    }

}());

