$(document).ready(function () { SUPER.IAP30.ConsuImputaciones.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ConsuImputaciones = SUPER.IAP30.ConsuImputaciones || {}

SUPER.IAP30.ConsuImputaciones.app = (function (view) {

    var ultimaFecha;
    
    var init = function () {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }
              
        //Manejador de fechas
        moment.locale('es');

        //Inicializa formateo
        accounting.settings = {
            number: {
                precision: 2,
                thousand: ".",
                decimal: ","
            }
        }

    
        /*******************************************************************************************************/
        view.init();
        //Se atachan los eventos 

        //Acciones a realizar al modificar el valor de las fechas Desde y Hasta
        //view.attachEvents("change", view.dom.fechaDesde, obtenerProyectos);
        //view.attachEvents("change", view.dom.fechaHasta, obtenerProyectos);        
        view.attachEvents("change", view.dom.txtRango, comprobarObtenerProyectos);

        //Acciones a realizar al seleccionar un proyecto del combo
        view.attachEvents("change", view.dom.cboProyecto, ObtenerDatosTabla);        

        //Boton exportación a excel
        view.attachEvents("click", view.dom.btnExportExcel, exportarExcel);

        //Clicks de filas de la tabla
        view.attachLiveEvents("keypress click", view.selectores.sel_nombreLinea, clickLinea);

        //Marcado de filas de la tabla
        view.attachLiveEvents("keypress click", view.selectores.sel_lineasTabla, marcarLinea);

        //Botones de expasión/contracción de niveles 
        view.attachEvents("keypress click", view.dom.nivel1, mostrarNivel1);
        view.attachEvents("keypress click", view.dom.nivel2, mostrarNivel2);
        view.attachEvents("keypress click", view.dom.nivel3, mostrarNivel3);
        view.attachEvents("keypress click", view.dom.nivel4, mostrarNivel4);

        //Botón de bomba
        view.attachEvents("keypress click", view.dom.bomba, abrirBomba);

        //Redimensión de la pantalla
        view.attachEvents("resize", view.dom.ventana, view.redimensionPantalla);

        IB.procesando.mostrar();        

        $.when(obtenerProyectos()).then(function () {

            view.visualizarContenedor();            

        });
    }

    var comprobarObtenerProyectos = function () {

        if (view.dom.txtRango.val() != ultimaFecha) {
            IB.procesando.mostrar();
            obtenerProyectos();
        }

    }

    obtenerProyectos = function (e) {
        try {

            var defer = $.Deferred();

            //if ((view.dom.fechaDesde.val() == "") || (view.dom.fechaHasta.val() == "")) {
            //    IB.bsalert.fixedAlert("warning", "Debes indicar el periodo temporal.");
            //    return;
            //}
            //var fechaHasta = moment(view.dom.fechaHasta.val(), 'DD/MM/YYYY');
            //var fechaDesde = moment(view.dom.fechaDesde.val(), 'DD/MM/YYYY');
            var fechaDesde = view.dom.txtRango.data('daterangepicker').startDate;
            var fechaHasta = view.dom.txtRango.data('daterangepicker').endDate;
            //var days = fechaHasta.diff(fechaDesde, 'days'); //[days, years, months, seconds, ...]

            //if (days<0) {
            //    //IB.bsalert.fixedAlert("warning", "Error", "El rango temporal indicado es incorrecto.");
            //    if (this.id == "txtDesde") view.dom.fechaHasta.val(view.dom.fechaDesde.val());
            //    else view.dom.fechaDesde.val(view.dom.fechaHasta.val());
            //    return;
            //}


            //if (view.dom.fechaDesde.val().length == 10 && view.dom.fechaHasta.val().length == 10) {
            //    if (days < 0) {
            //        if (this.id == "txtDesde") view.dom.fechaHasta.val(view.dom.fechaDesde.val());
            //        else view.dom.fechaDesde.val(view.dom.fechaHasta.val());
            //    }

            //    var filtro = { sUsuario: IB.vars.codUsu, sFechaDesde: view.dom.fechaDesde.val(), sFechaHasta: view.dom.fechaHasta.val() };

            //    IB.procesando.mostrar();
            //    IB.DAL.post(null, "getProyectos", filtro, null,
            //        function (data) {
            //            view.rellenarComboProyectos(data);
            //        },
            //        function (ex, status) {
            //            IB.procesando.ocultar();
            //            IB.bsalert.fixedAlert("danger", "Error", "Al recuperar los proyectos con imputaciones en ese periodo.");
            //        }
            //    );

            ultimaFecha = view.dom.txtRango.val();

            var filtro = { sUsuario: IB.vars.codUsu, sFechaDesde: fechaDesde.format('DD/MM/YYYY'), sFechaHasta: fechaHasta.format('DD/MM/YYYY') };
            
            IB.DAL.post(null, "getProyectos", filtro, null,
                function (data) {
                    view.rellenarComboProyectos(data);
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar los proyectos con imputaciones en ese periodo.");
                    defer.reject();
                }
            );
            //}
            //else view.borrarCatalogo();
            return defer.promise();
        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al controlar el rango de fechas.");
            defer.reject();
        }
    }

    ObtenerDatosTabla = function (e) {
  //    view.borrarCatalogo();
        if (view.dom.cboProyecto.find('option:selected').val() != null) {
            var fechaDesde = view.dom.txtRango.data('daterangepicker').startDate;
            var fechaHasta = view.dom.txtRango.data('daterangepicker').endDate;
            // para seleccionar el texto del combo $(this).find('option:selected').text();
            //var filtro = { sUsuario: IB.vars.codUsu, sPSN: view.dom.cboProyecto.find('option:selected').val(), sFechaDesde: view.dom.fechaDesde.val(), sFechaHasta: view.dom.fechaHasta.val() };
            var filtro = { sUsuario: IB.vars.codUsu, sPSN: view.dom.cboProyecto.find('option:selected').val(), sFechaDesde: fechaDesde.format('DD/MM/YYYY'), sFechaHasta: fechaHasta.format('DD/MM/YYYY') };
            IB.procesando.mostrar();
            IB.DAL.post(null, "obtenerDatos", filtro, null,
                function (data) {
                    view.pintarTablaDatos(data);
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar los datos relacionados con el proyecto seleccionado.");
                }
            );
        }
    }

    var marcarLinea = function (e) {

        var srcobj = e.target ? e.target : e.srcElement;

        //Si el elemento no es el td se busca en la jerarquia superior
        if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td')

        ////Se marca la línea como seleccionada
        view.marcarLinea($(srcobj).parent());

    }

    var clickLinea = function (e) {

        var srcobj = e.target ? e.target : e.srcElement;

        //Si el elemento no es el td se busca en la jerarquia superior
        if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td')

        //Solo se buscan hijos si no es una tarea
        if ($(srcobj).parent().attr('data-tipo') != "C") {
            cargarLinea($(srcobj), 1);
        }

        ////Se marca la línea como seleccionada
        view.marcarLinea($(srcobj).parent());

    }
    
    //Muestra todos los proyectos técnicos de los proyectos económicos
    var mostrarNivel1 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);
    }
    //Muestra todos los elementos de nivel2
    var mostrarNivel2 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);

        IB.procesando.mostrar();
        view.mostrarNivel(view.niveles.N2(), 2);
        IB.procesando.ocultar();

    }
    //Muestra todos los elementos de nivel3
    var mostrarNivel3 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);

        IB.procesando.mostrar();
        view.mostrarNivel(view.niveles.N3(), 3);
        IB.procesando.ocultar();

    }
    //Muestra todos los elementos de nivel4
    var mostrarNivel4 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);

        IB.procesando.mostrar();
        view.mostrarNivel(view.niveles.N4(), 6);
        IB.procesando.ocultar();

    }

    ////Muestra todas las líneas que cuelgan de la línea activa de la tabla
    var abrirBomba = function (e) {

        var lineaActiva = view.lineas.lineaActiva();
        view.cerrarNivel(e);
        view.marcarLinea(lineaActiva);

        IB.procesando.mostrar();
        cargarLinea(view.lineas.lineaDesActiva(), 4);
        IB.procesando.ocultar();

    }
    //Tratamiento de la visualización de las líneas de la tabla
    var cargarLinea = function (thisObj, proceso) {

        //Proceso 1 -> Abrir línea a línea
        //Proceso 2 -> Abrir Proyectos técnicos
        //Proceso 3 -> Abrir todo
        //Proceso 4 -> Bomba(abrir descendencia de línea activa)

        switch (proceso) {
            case 1:
                if (view.abrir($(thisObj))) {
                    return view.abrirLinea($(thisObj), proceso);
                }
                else {
                    return view.cerrarLinea($(thisObj), proceso);
                }
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                return view.abrirLinea($(thisObj), proceso);
                break;
        }

    }
    function exportarExcel() {

        if ($(view.selectores.sel_lineasTabla).length == 0) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        //Esquema de la tabla en base a la cabecera de la misma
        var header_list = '[';

        header_list += ' {"data-text": "Tipo",';
        header_list += ' "data-type": "String"},';

        view.dom.tablaCabecera.find('thead tr th').each(function () {
            header_list += ' {"data-text": "' + $(this).text().trim() + '",';
            var type = $(this).attr('data-type');
            if (type) {
                header_list += ' "data-type": "' + type + '"},';            
            }
        });

        var schema = JSON.parse(header_list.slice(0, -1) + ']');

        //Se recorre la tabla para extraer las filas a exportar
        var datos = [];

        view.dom.tblDatos.find("tbody tr.linea:visible").each(function () {
            var fila = new Object()

            fila[0] = "";
            if ($(this).attr("data-tipo") != "C") fila[0] = $(this).attr("data-tipo");
            
            $.each(this.cells, function (colIdx) {                
                if ($(view.dom.tablaCabecera.find('thead tr th')[colIdx]).attr('data-type') == "Double") {
                    fila[colIdx + 1] = accounting.unformat($(this).text(), ",");
                } else {
                    var pre = "";
                    if ($(this).parent().attr("data-level") == "1") pre = "";
                    else if ($(this).parent().attr("data-level") == "2") pre = "  ";
                    else if ($(this).parent().attr("data-level") == "3") pre = "    ";
                    else if ($(this).parent().attr("data-level") == "4") pre = "      ";
                    else if ($(this).parent().attr("data-level") == "5") pre = "        ";
                    else if ($(this).parent().attr("data-level") == "6") pre = "          ";

                    if ($(this).parent().attr("data-tipo") != "C")
                        fila[colIdx + 1] = pre + $(this).children(colIdx).eq(2).text().trim();
                    else fila[colIdx + 1] = pre + $(this).eq(colIdx).text().trim();
                }
            });
            debugger;
            datos.push(fila);
        });

        //Se añade el pie
        var pie = new Object();

        pie[0] = "";
        pie[1] = "Total:";
        pie[2] = accounting.unformat($("#PieHoras").text(), ",");
        pie[3] = accounting.unformat($("#PieJornadas").text(), ",");

        datos.push(pie);

        IB.Exportaciones.exportarDatosExcel(schema, datos, "Imputaciones", "Consulta de imputaciones");

        //try {
        //    if ($(view.selectores.sel_lineasTabla).length == 0) {
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }
        //    var sb = new StringBuilder();
        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");

        //    // cabecera
        //    sb.append("	<tr align=center>");
        //    sb.append("        <td style='width:30px;background-color: #BCD4DF;'>Tipo</td>");
        //    sb.append("        <td style='width:500px;background-color: #BCD4DF;'>Estructura técnica / F. consumo / Comentarios</td>");
        //    sb.append("        <td style='width:60px;background-color: #BCD4DF;'>Horas</td>");
        //    sb.append("        <td style='width:60px;background-color: #BCD4DF;'>Jornadas</td>");
        //    sb.append("	</tr>");

        //    // contenido
        //    view.dom.bodyTabla.find("tr.linea:visible").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        sb.append("<td>");
        //        if ($(this).attr("data-tipo") != "C") sb.append($(this).attr("data-tipo"));
        //        sb.append("</td>");
        //        for (var x = 0; x <= 2; x++) {
        //            sb.append("<td>");

        //            if (x == 0) {
        //                if ($(this).attr("data-level") == "1") sb.append("");
        //                else if ($(this).attr("data-level") == "2") sb.append("&nbsp;&nbsp;");
        //                else if ($(this).attr("data-level") == "3") sb.append("&nbsp;&nbsp;&nbsp;&nbsp;");
        //                else if ($(this).attr("data-level") == "4") sb.append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        //                else if ($(this).attr("data-level") == "5") sb.append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        //                else if ($(this).attr("data-level") == "6") sb.append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                       
        //                if ($(this).attr("data-tipo") != "C")
        //                    sb.append($(this).children().children(x).eq(2).text());
        //                else sb.append($(this).children().eq(x).text());
        //            }
        //            else sb.append($(this).children().eq(x).text());
        //            sb.append("</td>");
        //        }
        //        sb.append("</tr>");
        //    });

        //    // pie
        //    sb.append("<tr>");
        //    sb.append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
        //    sb.append("<td style='background-color: #BCD4DF;'>Total</td>");
        //    sb.append("<td style='text-align:right;background-color: #BCD4DF;'>");
        //    sb.append($("#PieHoras").text());
        //    sb.append("</td>");
        //    sb.append("<td style='text-align:right;background-color: #BCD4DF;'>");
        //    sb.append($("#PieJornadas").text());
        //    sb.append("</td>");
        //    sb.append("</tr>");
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

})(SUPER.IAP30.ConsuImputaciones.View);