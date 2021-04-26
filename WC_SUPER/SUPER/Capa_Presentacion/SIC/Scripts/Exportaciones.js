var IB = IB || {}

IB.Exportaciones = (function () {


    //Se añaden al body el iframe y el form    
    $("body").append("<iframe id='ifrmExportar' scrolling='no' marginheight='0' marginwidth='0' frameborder='0' style='display: none;'></iframe><form runat='server' id='ifrmExportarForm' style='display: none;' method='post'><input type='text' id='ifrmExportarFilename' name='ifrmExportarFilename' /><input type='text' id='ifrmExportarTitulo' name='ifrmExportarTitulo' /><textarea id='ifrmExportarSchema' name='ifrmExportarSchema'></textarea><textarea id='ifrmExportarDatos' name='ifrmExportarDatos'></textarea></form>");
    
    
    var exportarDataTableExcel = function (tablaCuerpo, nombreFichero, tituloFichero) {

        //Esquema de la tabla en base a la cabecera de la misma
        var header_list = '[';

        tablaCuerpo.find('thead tr th').each(function () {
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

        //Recorrremos la tabla para extraer las filas a exportar
        var datos = [];

        tablaCuerpo.find("tbody tr:visible").each(function () {
            var fila = new Object()
            $.each(this.cells, function (colIdx) {
                if ($(tablaCuerpo.find('thead tr th')[colIdx]).attr('data-type') == "Double") {
                    fila[colIdx] = accounting.unformat($(this).text(), ",");
                }
                else {
                    if ($(this).text().length == 0)
                        fila[colIdx] = null;
                    else
                        fila[colIdx] = $(this).text();
                }
            });
            datos.push(fila);
        });

        //Se alimentan los campos del formulario y se postea        
        $('#ifrmExportarFilename').val(nombreFichero);
        $('#ifrmExportarTitulo').val(tituloFichero);
        $('#ifrmExportarSchema').val(JSON.stringify(schema));
        $('#ifrmExportarDatos').val(JSON.stringify(datos));
        $('#ifrmExportarForm').attr("action", IB.vars.strserver + "Capa_Presentacion/SIC/Exportaciones/ExportarExcelDatatableCliente.aspx");
        $('#ifrmExportarForm').submit();

    }

   

    return {
        exportarDataTableExcel: exportarDataTableExcel        
    }

})();