var IB = IB || {}

IB.Exportaciones = (function () {


    //Se añaden al body el iframe y el form
    $("body").append("<iframe id='ifrmExportar' scrolling='no' marginheight='0' marginwidth='0' frameborder='0' style='display: none;'></iframe><form runat='server' id='ifrmExportarForm' style='display: none; target='ifrmExportar' method='post'><input type='text' id='ifrmExportarFilename' name='ifrmExportarFilename' /><input type='text' id='ifrmExportarTitulo' name='ifrmExportarTitulo' /><textarea id='ifrmExportarSchema' name='ifrmExportarSchema'></textarea><textarea id='ifrmExportarDatos' name='ifrmExportarDatos'></textarea></form>");


    //Exportación de Datatable a Excel por medio de IBSERVIOFFICE
    var exportarDataTableExcel = function (datatable, nombreFichero, tituloFichero, pie) {

        //Esquema de la tabla en base a la cabecera de la misma
        var header_list = '[';

        datatable.find('thead tr th').each(function () {
            header_list += ' {"data-text": "' + $(this).text().trim() + '",';
            var type = $(this).attr('data-type');
            if (type) {
                header_list += ' "data-type": "' + type + '"},';
            } else {
                IB.bserror.mostrarErrorAplicacion("Error de exportación", "Una o más columnas de la tabla a exportar no indican su tipo de dato.<br/><br/>");
                return false;
            }
        });

        var schema = JSON.parse(header_list.slice(0, -1) + ']');

        //Se recorre la datatable para extraer las filas a exportar
        var datos = [];
        datatable.DataTable().rows().every(function (rowIdx) {
            datos.push($.extend({}, this.cells(rowIdx, ':visible').data().toArray()));
        });

        //Si se pide exportar el pide de la tabla se incluye en los datos como una fila más
        if (pie) {

            var pie = new Object();
            datatable.DataTable().columns().every(function (colIdx) {
                if ($(datatable.find('thead tr th')[colIdx]).attr('data-type') == "Double") {
                    pie[colIdx] = accounting.unformat($(this.footer()).html(), ",");
                } else {
                    pie[colIdx] = $(this.footer()).html();
                }
            });

            datos.push(pie);

        }

        //Se alimentan los campos del formulario y se postea        
        $('#ifrmExportarFilename').val(nombreFichero);
        $('#ifrmExportarTitulo').val(tituloFichero);
        $('#ifrmExportarSchema').val(JSON.stringify(schema));
        $('#ifrmExportarDatos').val(JSON.stringify(datos));
        $('#ifrmExportarForm').attr("action", IB.vars.strserver + "Capa_Presentacion/APP/Exportaciones/ExportarExcel.aspx");
        $('#ifrmExportarForm').submit();

    }


    //Exportación tabla a Excel por medio de IBSERVIOFFICE
    var exportarTablaExcel = function (tablaCabecera, tablaCuerpo, tablaPie, nombreFichero, tituloFichero) {

        //Esquema de la tabla en base a la cabecera de la misma
        var header_list = '[';

        tablaCabecera.find('thead tr th').each(function () {
            header_list += ' {"data-text": "' + $(this).text().trim() + '",';
            var type = $(this).attr('data-type');
            if (type) {
                header_list += ' "data-type": "' + type + '"},';
            } else {
                IB.bserror.mostrarErrorAplicacion("Error de exportación", "Una o más columnas de la tabla a exportar no indican su tipo de dato.<br/><br/>");
                return false;
            }
        });

        var schema = JSON.parse(header_list.slice(0, -1) + ']');

        //Se recorre la tabla para extraer las filas a exportar
        var datos = [];

        tablaCuerpo.find("tbody tr.linea:visible").each(function () {
            var fila = new Object()
            $.each(this.cells, function (colIdx) {
                if ($(tablaCabecera.find('thead tr th')[colIdx]).attr('data-type') == "Double") {
                    fila[colIdx] = accounting.unformat($(this).text(), ",");
                } else {
                    fila[colIdx] = $(this).text();
                }                
            });
            datos.push(fila);
        });

        //Si se pide exportar el pide de la tabla se incluye en los datos como una fila más
        if (tablaCuerpo != tablaPie) {

            var pie = new Object();
            tablaPie.find("tfoot tr:visible td").each(function (colIdx) {
                if ($(tablaCabecera.find('thead tr th')[colIdx]).attr('data-type') == "Double") {
                    pie[colIdx] = accounting.unformat($(this).text(), ",");
                } else {
                    pie[colIdx] = $(this).text();
                }
            });

            datos.push(pie);

        }

        //Se alimentan los campos del formulario y se postea        
        $('#ifrmExportarFilename').val(nombreFichero);
        $('#ifrmExportarTitulo').val(tituloFichero);
        $('#ifrmExportarSchema').val(JSON.stringify(schema));
        $('#ifrmExportarDatos').val(JSON.stringify(datos));
        $('#ifrmExportarForm').attr("action", IB.vars.strserver + "Capa_Presentacion/APP/Exportaciones/ExportarExcel.aspx");
        $('#ifrmExportarForm').submit();

    }


    //Exportación del esquema y los datos de una tabla a Excel por medio de IBSERVIOFFICE
    var exportarDatosExcel = function (schema, datos, nombreFichero, tituloFichero) {

        //Se alimentan los campos del formulario y se postea        
        $('#ifrmExportarFilename').val(nombreFichero);
        $('#ifrmExportarTitulo').val(tituloFichero);
        $('#ifrmExportarSchema').val(JSON.stringify(schema));
        $('#ifrmExportarDatos').val(JSON.stringify(datos));
        $('#ifrmExportarForm').attr("action", IB.vars.strserver + "Capa_Presentacion/APP/Exportaciones/ExportarExcel.aspx");
        $('#ifrmExportarForm').submit();

    }

    return {
        exportarDataTableExcel: exportarDataTableExcel,
        exportarTablaExcel: exportarTablaExcel,
        exportarDatosExcel: exportarDatosExcel
    }

})();