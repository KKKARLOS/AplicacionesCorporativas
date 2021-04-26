$(document).ready(function () { SUPER.IAP30.Facturabilidad.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Facturabilidad = SUPER.IAP30.Facturabilidad || {}

SUPER.IAP30.Facturabilidad.app = (function (view) {

    /*Lógica de carga y visualización de datos*/
    var ultimaFecha;

    var init = function () {

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

        view.init();        

        //Asignación de botones a elementos estáticos
        //view.attachEvents("change", view.dom.txtDesde, consultar);
        //view.attachEvents("change", view.dom.txtHasta, consultar);
        view.attachEvents("change", view.dom.txtRango, comprobarConsultar);

        //Boton exportación a excel
        view.attachLiveEvents("click", view.selectores.btnExportExcel, exportarExcel);
        view.attachLiveEvents("click", view.selectores.btnExportExcel2, exportarExcel2);
        //Se comprueba si el profesional tiene permisos de reconexión y si es así, se atacha el evento correspondiente
        if (IB.vars.nReconectar == 1) {
            view.attachEvents("click", view.dom.icoProfesional, abrirBuscadorProfesional);
            view.attachEvents("click", view.dom.btnUser, abrirBuscadorProfesional);
        }
        else {//En caso de que no pueda reconectarse se deshabilita la opción
            view.deshabilitarReconexion();
        }

        consultar();
        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

    }

    abrirBuscadorProfesional = function (e) {
        //Se inicializa el control buscadorUsuario (BuscadorUsuario.js)
        view.dom.divBuscadorPersonas.buscadorPersonas({
            modal: true, titulo: "Selección de profesional", tipoBusqueda: 'profesionales',
            aceptar: function (data) {
                view.dom.spProfesional.html(data.PROFESIONAL);

                IB.vars.codUsu = data.t314_idusuario;
                var filtro = { sUsuario: IB.vars.codUsu };

                IB.procesando.mostrar();
                IB.DAL.post(null, "establecerUsuarioIAP", filtro, null,
                    function (data) {
                        consultar();
                    },
                    function (ex, status) {
                        IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al acceder a los datos del usuario.");
                    }
                );

            },
            cancelar: function () {
            }
        });
    }

    var comprobarConsultar = function (e) {

        if (view.dom.txtRango.val() != ultimaFecha) {
            consultar();
        }

    }

    var consultar = function () {
        //if (!moment(view.dom.txtDesde.val(), 'DD/MM/YYYY', 'es', true).isValid()) return;
        //if (!moment(view.dom.txtHasta.val(), 'DD/MM/YYYY', 'es', true).isValid()) return;

        //var desde = moment(toDate(view.dom.txtDesde.val()));
        //var hasta = moment(toDate(view.dom.txtHasta.val()));
        IB.procesando.mostrar();
        if (view.comprobarVisibilidad(view.dom.container)) view.ocultarContenido();

        var desde = view.dom.txtRango.data('daterangepicker').startDate;
        var hasta = view.dom.txtRango.data('daterangepicker').endDate;

        if (hasta.diff(desde) < 0) {
            IB.bsalert.toastdanger("La fecha Desde debe ser inferior o igual a la fecha Hasta");
            return;
        }

        ultimaFecha = view.dom.txtRango.val();
        $.when(obtenerProyectos(desde.format('DD/MM/YYYY'), hasta.format('DD/MM/YYYY')).then(function () {
            view.dom.container.css("visibility", "visible");
        }));


    }

    var buscar = function () {

    }

    var toDate = function (str, splitter) {

        if (typeof splitter === "undefined") splitter = "/";
        var aDate = str.split(splitter);
        return new Date(aDate[2], (aDate[1] - 1), aDate[0]);

    }

    function exportarExcel() {

        if (!view.dom.tabla.DataTable().rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tabla, "Facturabilidad1", "Consulta de facturabilidad", true);

        //try {
        //    if ($("#bodyTabla tr").length == 0 || $("#bodyTabla tr").text() == 'Ningún dato disponible en esta tabla') {
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }
        //    var sb = new StringBuilder();
        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");

        //    // cabecera
        //    sb.append("	<tr align=center>");
        //    sb.append("        <td colspan='3'>&nbsp;</td>");
        //    sb.append("        <td colspan='2' style='background-color: #E4EFF3;'>Planificación</td>");
        //    sb.append("        <td colspan='4' style='background-color: #E4EFF3;'>Periodo</td>");
        //    sb.append("        <td colspan='4' style='background-color: #E4EFF3;'>Inicio proyecto -> Fin periodo</td>");
        //    sb.append("	</tr>");
        //    sb.append("	<tr align=center>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Proyecto económico</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Tarea</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:80px; text-align:center;'>Facturable</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas planificadas para la tarea</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas previstas para el profesional en la tarea</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas planificadas en la agenda para el profesional en la tarea</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas imputadas por el profesional a la tarea dentro del periodo</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas imputadas por otros profesionales a la tarea dentro del periodo</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Total de horas imputadas a la tarea dentro del periodo</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas planificadas en la agenda por el profesional para la tarea desde el inicio del proyecto hasta el fin del periodo</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas imputadas por el profesional a la tarea desde el inicio del proyecto hasta el fin del periodo</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Horas imputadas por otros profesionales a la tarea desde el inicio del proyecto hasta el fin del periodo</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:200px;'>Total de horas imputadas a la tarea desde el inicio del proyecto hasta el fin del periodo</td>");

        //    sb.append("	</tr>");

        //    // contenido
        //    $("#bodyTabla").find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        for (var x = 0; x < 13; x++) {
        //            if (x == 2) {
        //                sb.append("<td style='text-align:center;'>");
        //                if ($(this).children().eq(x).html().indexOf('MonedasOff') >= 0)
        //                    sb.append('NO');
        //                else
        //                    sb.append('SI');
        //            }
        //            else {
        //                sb.append("<td>");
        //                sb.append($(this).children().eq(x).text());
        //            }
        //            sb.append("</td>");
        //        }
        //        sb.append("</tr>");
        //    });

        //    // pie
        //    sb.append("<tr>");
        //    $('.dataTables_scrollFootInner > table > tfoot > tr > td').each(function () {
        //        sb.append("<td style='background-color: #E4EFF3;'>");
        //        sb.append($(this).text());
        //        sb.append("</td>");
        //    });
        //    sb.append("</tr>");
        //    sb.append("</table>");

        //    crearExcelIAP(sb.toString());
        //    var sb = null;

        //} catch (e) {
        //    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos para generar el archivo Excel");
        //}
    }

    function exportarExcel2() {

        if (!view.dom.tabla2.DataTable().rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tabla2, "Facturabilidad2", "Consulta de facturabilidad", true);

        //try {
        //    if ($("#bodyTabla2 tr").length == 0) {
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }
        //    var sb = new StringBuilder();
        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");

        //    // cabecera
        //    sb.append("	<tr align=center>");
        //    sb.append("        <td colspan='2'>&nbsp;</td>");
        //    sb.append("        <td colspan='2' style='background-color: #E4EFF3;'>Imputaciones registradas</td>");
        //    sb.append("	</tr>");
        //    sb.append("	<tr align=center>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:300px;'>Tipología de proyecto</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:300px;'>Naturaleza de producción</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:100px;'>Facturable</td>");
        //    sb.append("        <td style='background-color: #BCD4DF; width:100px;'>No facturable</td>");
        //    sb.append("	</tr>");

        //    // contenido
        //    $("#bodyTabla2").find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        for (var x = 0; x < 4; x++) {
        //            sb.append("<td>");
        //            sb.append($(this).children().eq(x).text());
        //            sb.append("</td>");
        //        }
        //        sb.append("</tr>");
        //    });

        //    // pie
        //    sb.append("<tr>");
        //    $('#tabla2 > tfoot > tr > td').each(function () {
        //        sb.append("<td style='background-color: #E4EFF3;'>");
        //        sb.append($(this).html());
        //        sb.append("</td>");
        //    });
        //    sb.append("</tr>");
        //    sb.append("</table>");

        //    crearExcelIAP(sb.toString());
        //    var sb = null;

        //} catch (e) {
        //    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos para generar el archivo Excel");
        //}
    }

    /*Llamadas de carga a webmethods*/

    var obtenerProyectos = function (fDesde, fHasta) {
        var defer = $.Deferred();

        var payload = { sDesde: fDesde, sHasta: fHasta };
        IB.DAL.post(null, "getFacturabilidadProyectos", payload, null,
            function (data) {
                view.pintarTablaCarga(data);
                $.when(IB.procesando.ocultar()).then(function () {
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }   

    return {
        init: init
    }

})(SUPER.IAP30.Facturabilidad.View);