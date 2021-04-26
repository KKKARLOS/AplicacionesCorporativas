$(document).ready(function () {
    SUPER.IAP30.Fichero.app.init();
})

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Fichero = SUPER.IAP30.Fichero || {}

SUPER.IAP30.Fichero.app = (function (view) {

    var submit = false;

    var init = function () {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

        view.init(); 

        //Al seleccionar un fichero 
        //view.attachEvents("change", view.dom.inputFichero, selFichero);
        //Visualizar modal con el formato que debe tener el fichero 
        view.attachEvents("click", view.dom.btnFormato, verFormato);
        //Analizar el fichero 
        view.attachEvents("click", view.dom.btnAnalizar, analizar3);
        //Grabar las imputaciones
        view.attachEvents("click", view.dom.btnGrabar, grabar);
        //Abre un notepad para visualizar el contenido del archivo de imputaciones
        //view.attachEvents("click", view.dom.btnVisualizar, visualizar);
        //BotonES exportación a excel
        view.attachLiveEvents("click", view.selectores.btnExportExcel, excel);
        view.attachLiveEvents("click", view.selectores.btnExportExcel2, excel2);
        view.attachLiveEvents("click", view.selectores.btnExportExcel3, excel3);

        //Carga de las acciones del asunto seleccionado
        //view.attachLiveEvents("change", view.selectores.tipoFichero, setTipoFichero);
        view.attachLiveEvents("change", view.selectores.radio, setTipoFichero);
        //view.attachLiveEvents("change", view.selectores.fichero, selFichero);

        //setTimeout("view.dom.pantalla.show();", 50);
        //view.dom.pantalla.removeAttr("style");

        //Para evitar llamar a limpiar caché cuando se submitea el formulario de las exportaciones a excel
        view.attachLiveEvents("submit", view.selectores.form, formSubmit);

        IB.procesando.ocultar();
    }
    //var selFichero = function () {
    //    view.dom.btnGrabar.attr('disabled', 'disabled');
    //    view.dom.radio.removeAttr("disabled");
    //    //view.dom.uFichero.val($("#inputFichero").val());
    //}
    window.onbeforeunload = function () {
        if (!submit) {
            IB.procesando.mostrar();
            IB.DAL.post(null, "limpiarCache", null, null, null);
        }
        submit = false;
    }

    var formSubmit = function () {
        if (typeof $(this).attr('id') !== "undefined") submit = true;
    }

    var verFormato = function () {
        view.dom.modalFormato.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        view.dom.modalFormato.modal('show');
        $('.ocultable').attr('aria-hidden', 'true');
    }
    var setTipoFichero = function (event) {
        if (this.value == "D") {
            $(view.dom.estructuraMasiva).hide();
            $(view.dom.estructuraDia).show();
            $(view.dom.divTablaVM).hide();
            $(view.dom.divTablaV).show();
            $(view.dom.tablaV).DataTable().columns.adjust().draw();
        }
        else {
            $(view.dom.estructuraDia).hide();
            $(view.dom.estructuraMasiva).show();
            $(view.dom.divTablaV).hide();
            $(view.dom.divTablaVM).show();
            $(view.dom.tablaVM).DataTable().columns.adjust().draw();
        }
    }
    /*
    var analizar = function () {
        if (view.dom.inputFichero.val() == "") {
            IB.bsalert.toastwarning("Debes seleccionar un fichero.");
            return;
        }
        //Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {  
            IB.procesando.mostrar();

            var fileUpload = $("#inputFichero").get(0);

            if (view.dom.tblVal) view.dom.tblVal.clear().draw();
            if (view.dom.tblValM) view.dom.tblValM.clear().draw();
            if (view.dom.tblGrab) view.dom.tblGrab.clear().draw();

            var files = fileUpload.files;  
              
            // Create FormData object  
            var fileData = new FormData();  
  
            // Looping over all files and add it to FormData object  
            //for (var i = 0; i < files.length; i++) {  
            //    fileData.append(files[i].name, files[i]);  
            //}  

            fileData.append('FicheroSubido', files[0]);

            // Adding one more key to FormData object  
            //fileData.append('tipoFichero', $(view.selectores.tipoFichero).val());
  
            $.ajax({  
                url: 'Default.aspx',
                type: "POST",  
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,  
                success: function (result) {  
                    //IB.bsalert.toastinfo(result);  
                    analizar2();
                },  
                error: function (err) {  
                    IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la validación del fichero.<br/><br/>" + err.statusText);

                }  
            });  
        } else {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga del fichero.<br/><br/>FormData no está soportado.");
        }  
    };
    
    var analizar2 = function () {
        IB.procesando.mostrar();
        var defer = $.Deferred();
        var sTipoFichero = view.obtenerValor($('input:radio[id^="radio"]:checked'));
        var payload = { tipoFichero: sTipoFichero };
        IB.DAL.post(null, "getErroresValidacion", payload, null,
            function (result) {
                view.dom.nFilas.val(result.nFilas);
                view.dom.nFilasC.val(result.nFilasC);
                view.dom.nFilasE.val(result.nFilasE);
                if (result.nFilasE != 0) {
                    if (sTipoFichero=="D") view.verErroresValidacion(result.Errores);
                    else view.verErroresValidacionM(result.Errores);
                }
                else {
                    if (sTipoFichero == "D") view.verErroresValidacion(null);
                    else view.verErroresValidacionM(null);
                    view.dom.radio.attr('disabled', 'disabled');
                    view.dom.btnGrabar.removeAttr("disabled");
                    IB.bsalert.toastinfo("Información del fichero de entrada correcta.<br />Pulsa el botón grabar.");
                }

                $.when(IB.procesando.ocultar()).then(function () {
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }
    */
    var analizar3 = function () {
        IB.procesando.mostrar();
        if (view.selectores.nombreFichero.length != 1) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar un fichero.");
            return;
        }
        var defer = $.Deferred();
        var sTipoFichero = view.obtenerValor($('input:radio[id^="radio"]:checked'));
        var payload = { tipoFichero: sTipoFichero };
        IB.DAL.post(null, "getErroresValidacion", payload, null,
            function (result) {
                view.dom.nFilas.val(result.nFilas);
                view.dom.nFilasC.val(result.nFilasC);
                view.dom.nFilasE.val(result.nFilasE);
                if (result.nFilasE != 0) {
                    if (sTipoFichero == "D") view.verErroresValidacion(result.Errores);
                    else view.verErroresValidacionM(result.Errores);
                }
                else {
                    if (sTipoFichero == "D") view.verErroresValidacion(null);
                    else view.verErroresValidacionM(null);
                    view.dom.radio.attr('disabled', 'disabled');
                    view.dom.btnGrabar.removeAttr("disabled");
                    IB.bsalert.toastinfo("Información del fichero de entrada correcta.<br />Pulsa el botón grabar.");
                }

                $.when(IB.procesando.ocultar()).then(function () {
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }

    var grabar = function () {
        IB.procesando.mostrar();
        view.dom.btnGrabar.attr('disabled', 'disabled');
        var defer = $.Deferred();
        var sTipoFichero = view.obtenerValor($('input:radio[id^="radio"]:checked'));
        var payload = { tipoFichero: sTipoFichero };
        IB.DAL.post(null, "Procesar", payload, null,
            function (result) {
                view.dom.nFilas.val(result.nFilas);
                view.dom.nFilasC.val(result.nFilasC);
                view.dom.nFilasE.val(result.nFilasE);
                if (result.nFilasE != 0) {
                    view.verErroresGrabacion(result.Errores);
                }
                else {
                    //view.dom.radio.attr('disabled', 'disabled');
                    view.verErroresGrabacion(null);
                    IB.bsalert.fixedToast("Fichero procesado con éxito.", "info");
                }

                $.when(IB.procesando.ocultar()).then(function () {
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }
    //Visualizar con input=file a pelo
    //var visualizar = function() {
    //    try {
    //        if (view.dom.inputFichero.val() != "") {
    //            var shell = new ActiveXObject("WScript.Shell");
    //            shell.run("notepad.exe " + view.dom.inputFichero.val(), 1, false);
    //        }
    //        else
    //            IB.bsalert.toastwarning("Debes seleccionar un fichero.");
    //    }
    //    catch (e) {
    //        IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido al visualizar el fichero<br/><br/>" + e.message);
    //    }
    //}

    //Visualizar con plugin para subir ficheros
    //var visualizar = function () {
    //    try {
    //        if (view.dom.nombreFichero[0] != "") {
    //            var shell = new ActiveXObject("WScript.Shell");
    //            shell.run("notepad.exe " + view.dom.nombreFichero[0], 1, false);
    //        }
    //        else
    //            IB.bsalert.toastwarning("Debes seleccionar un fichero.");
    //    }
    //    catch (e) {
    //        IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido al visualizar el fichero<br/><br/>" + e.message);
    //    }
    //}

    function excel() {

        if (!view.dom.tablaV.DataTable().rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tablaV, "ErroresValidacion", "Relación de filas erróneas en el proceso de validación", false);

        //try {
        //    if ($(view.selectores.bodyTablaTR).length == 0 || $(view.selectores.bodyTablaTR).text() == 'Ningún dato disponible en esta tabla') {
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }

        //    var sb = new StringBuilder();
        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        //    sb.append("	<tr align=center style='background-color: #BCD4DF;'>");
        //    sb.append("     <td style='width:235px'>Usuario</TD>");
        //    sb.append("     <td style='width:90px'>Fecha</TD>");
        //    sb.append("     <td style='width:235px'>Tarea</TD>");
        //    sb.append("     <td style='width:90px'>Esfuerzo</TD>");
        //    sb.append("     <td style='width:280px'>Error</TD>");
        //    sb.append(" </tr>");

        //    $(view.selectores.bodyTabla).find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        for (var x = 0; x < 13; x++) {
        //            sb.append("<td>");
        //            sb.append($(this).children().eq(x).text());
        //            sb.append("</td>");
        //        }
        //        sb.append("</tr>");
        //    });
        //    sb.append("</table>");
        //    crearExcelIAP(sb.toString());
        //    var sb = null;
        //} catch (e) {
        //    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos para generar el archivo Excel");
        //}
    }
    function excel2() {

        if (!view.dom.tablaG.DataTable().rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tablaG, "ErroresGrabacion", "Relación de filas erróneas en el proceso de grabación", false);


        //try {
        //    if ($(view.selectores.bodyTabla2TR).length == 0 || $(view.selectores.bodyTabla2TR).text() == 'Ningún dato disponible en esta tabla') {
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }

        //    var sb = new StringBuilder();

        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        //    sb.append("	<tr align=center style='background-color: #BCD4DF;'>");
        //    sb.append("     <td style='width:70px'>Nº Línea</TD>");
        //    sb.append("     <td style='width:860px'>Error</TD>");
        //    sb.append(" </tr>");

        //    $(view.selectores.bodyTabla2).find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        for (var x = 0; x < 13; x++) {
        //            sb.append("<td>");
        //            sb.append($(this).children().eq(x).text());
        //            sb.append("</td>");
        //        }
        //        sb.append("</tr>");
        //    });
        //    sb.append("</table>");

        //    crearExcelIAP(sb.toString());
        //    var sb = null;
        //} catch (e) {
        //    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos para generar el archivo Excel");
        //}
    }
    function excel3() {

        if (!view.dom.tablaVM.DataTable().rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tablaVM, "ErroresValidacion", "Relación de filas erróneas en el proceso de validación", false);

        //try {
        //    if ($(view.selectores.bodyTablaMTR).length == 0 || $(view.selectores.bodyTablaMTR).text() == 'Ningún dato disponible en esta tabla') {
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }

        //    var sb = new StringBuilder();
        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        //    sb.append("	<tr align=center style='background-color: #BCD4DF;'>");
        //    sb.append("     <td style='width:200px'>Usuario</TD>");
        //    sb.append("     <td style='width:100px'>F.Desde</TD>");
        //    sb.append("     <td style='width:100px'>F.Hasta</TD>");
        //    sb.append("     <td style='width:200px'>Tarea</TD>");
        //    sb.append("     <td style='width:90px'>Esfuerzo</TD>");
        //    sb.append("     <td style='width:30px'>Fes.</TD>");
        //    sb.append("     <td style='width:280px'>Error</TD>");
        //    sb.append(" </tr>");

        //    $(view.selectores.bodyTablaM).find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        for (var x = 0; x < 13; x++) {
        //            sb.append("<td>");
        //            sb.append($(this).children().eq(x).text());
        //            sb.append("</td>");
        //        }
        //        sb.append("</tr>");
        //    });
        //    sb.append("</table>");
        //    crearExcelIAP(sb.toString());
        //    var sb = null;
        //} catch (e) {
        //    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos para generar el archivo Excel");
        //}
    }

    return {
        init: init
    }

})(SUPER.IAP30.Fichero.View);
