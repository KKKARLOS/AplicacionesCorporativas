$(document).ready(function () {
    SUPER.IAP30.BitacoraPE.app.init();
})

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.BitacoraPE = SUPER.IAP30.BitacoraPE || {}

SUPER.IAP30.BitacoraPE.app = (function (view) {

    /*Lógica de carga y visualización de datos*/
    var init = function () {
        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

        //IB.vars.nPSN = "37096";
        view.init();

        //$(document).on('hidden.bs.modal', '.modal', function () {
        //    view.dom.ocultable.attr('aria-hidden', 'false');
        //});

        //Salir de la pantalla 
        view.attachEvents("click", view.dom.btnRegresar, salir); 
        //Añadir al catálogo de asuntos 
        view.attachEvents("click", view.dom.icoNuevoAsunto, addAsunto);
        //Añadir al catálogo de acciones 
        view.attachEvents("click", view.dom.icoNuevaAccion, addAccion);
        //Eliminar fila seleccionada del catálogo de asuntos 
        view.attachEvents("click", view.dom.btnDelAsunto, eliminarAsunto);
        //Eliminar fila seleccionada del catálogo de acciones 
        view.attachEvents("click", view.dom.btnDelAccion, eliminarAccion);
        //Acceso a detalle de asunto 
        view.attachEvents("click", view.dom.btnGoAsunto, goAsunto);
        //Acceso a detalle de acción
        view.attachEvents("click", view.dom.btnGoAccion, goAccion);
        //Acceso a bitácora de PT 
        view.attachEvents("click", view.dom.btnAccesoPT, goPT);
        //Filtro de tipo de asunto
        view.attachEvents("change", view.dom.cboTipoAsunto, filtrarAsuntosTipo);
        //Filtro de estado
        view.attachEvents("change", view.dom.cboEstado, filtrarAsuntosEstado);
        //Botones exportación a excel
        view.attachLiveEvents("click", view.selectores.btnExcelAsunto, excelAsunto);
        view.attachLiveEvents("click", view.selectores.btnExcelAccion, excelAccion);
        view.attachLiveEvents("click", view.selectores.btnExcelPT, excelPT);


        //Acceso al detalle de asunto
        //view.attachEvents("dblclick", view.dom.lineaTablaProy, getProyecto);

        //Carga de las acciones del asunto seleccionado
        view.attachLiveEvents("click", view.selectores.filasAsunto, getAcciones);
        //Carga de las acciones del asunto seleccionado
        view.attachLiveEvents("click", view.selectores.filasAccion, setAccion);
        //Marcado de fila seleccionada en catálogo de bitácoras de PT
        view.attachLiveEvents("click", view.selectores.filasPT, setBitacoraPT);
        //Acceso al detalle de asunto. Lo comento porque en el click va a BBDD para traer acciones y no le da tiempo a hacer el doble-click
        //view.attachLiveEvents("dblclick", view.selectores.filasAsunto, goAsunto);
        //Acceso al detalle de accion
        view.attachLiveEvents("dblclick", view.selectores.filaAccionSel, goAccion);
        //Acceso a la bitácora de PT seleccionada
        view.attachLiveEvents("dblclick", view.selectores.filaPTSel, goPT);

        //Establece la visibilidad de los botones para modificar la bitacora
        setBotones();

        getTiposAsunto();

        consultar();
        getBitacoraPT(IB.vars.nPSN);

    }
    var setBotones = function () {
        if (IB.vars.hdnAcceso != "E")
            $(view.selectores.btnModif).hide();
    }
    var consultar = function () {
        if (IB.vars.nPSN == "")
            obtenerAsuntos(-1, "-1", "-1");
        else
            obtenerAsuntos(IB.vars.nPSN, "-1", "-1");
        if (IB.vars.idAsunto != "" && IB.vars.idAsunto != undefined) {
            obtenerAcciones(IB.vars.idAsunto);
        }
        else
            obtenerAcciones(-1);
    }

    var obtenerAsuntos = function (nPSN, tipoAsunto, estado) {
        var defer = $.Deferred();

        var payload = { nPSN: nPSN, tipoAsunto: tipoAsunto, estado: estado };
        IB.DAL.post(null, "getAsuntos", payload, null,
            function (data) {
                //if (data.length != 0) {
                view.crearAsuntos(data);
                if (IB.vars.idAsunto != "" && IB.vars.idAsunto != undefined) {
                    obtenerAcciones(IB.vars.idAsunto);
                }
                //}
                $.when(IB.procesando.ocultar()).then(function () {
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }
    var obtenerAcciones = function (nAsunto) {
        var defer = $.Deferred();

        var payload = { nAsunto: nAsunto };
        IB.DAL.post(null, "getAcciones", payload, null,
            function (data) {
                //if (data.length != 0) {
                    view.crearAcciones(data);
                //}
                $.when(IB.procesando.ocultar()).then(function () {
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }
    var getBitacoraPT = function (nPSN) {
        if (nPSN == "") nPSN=-1;
        var defer = $.Deferred();

        var payload = { nPSN: nPSN };
        IB.DAL.post(null, "getBitacoraPT", payload, null,
            function (data) {
                //if (data.length != 0) {
                    view.crearBitacoraPT(data);
                //}

                $.when(IB.procesando.ocultar()).then(function () {
                    view.visualizarContenedor();
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }

    var getTiposAsunto = function (e) {
        try {
            var filtro = {};

            IB.procesando.mostrar();
            IB.DAL.post(null, "getTiposAsunto", filtro, null,
                function (data) {
                    view.rellenarComboTipoAsunto(data);
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error", "Al recuperar los tipos de asunto.");
                }
            );
        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error", "Error al llamar a tipos de asunto.");
        }
    }

    var filtrarAsuntosTipo = function () {
        IB.procesando.mostrar();
        var sTipoAsunto = $(view.selectores.sel_TipoAsunto).text();
        if (sTipoAsunto == "TODOS") sTipoAsunto = "";
        view.dom.tblAsunto.column(view.dom.colTipoAsunto).search(sTipoAsunto).draw();
        obtenerAcciones(-1);
        $(view.selectores.filaAsuntoSel).removeClass('activa');
        IB.procesando.ocultar();
    }
    var filtrarAsuntosEstado = function () {
        IB.procesando.mostrar();
        var sEstado = $(view.selectores.sel_Estado).text();
        if (sEstado == "TODOS") sEstado = "";
        view.dom.tblAsunto.column(view.dom.colEstado).search(sEstado).draw();
        obtenerAcciones(-1);
        $(view.selectores.filaAsuntoSel).removeClass('activa');
        IB.procesando.ocultar();
    }
    var getAcciones_Old = function (event) {
        var srcobj = event.target ? event.target : event.srcElement;
        //Si el elemento no es el td se busca en la jerarquia superior
        if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td')
        ////Se marca la línea como seleccionada
        if (srcobj.length != 0) {
            var oFila = $(srcobj).parent();
            view.marcarLinea(oFila);
            //obtenerAcciones(oFila.find(view.selectores.sel_IdAsunto).text());
            var id = view.dom.tblAsunto.row(oFila).id();
            if (oFila.id)
                obtenerAcciones(oFila.id);
            else
                IB.bsalert.toastwarning("No se ha podido determinar el código de asunto");
        }
    }

    var getAcciones = function (event) {
        var thisTimeout = this;
        setTimeout(function () {
            IB.procesando.mostrar();
            view.marcarLinea(thisTimeout);
            if (thisTimeout.getAttribute("id"))
                obtenerAcciones(thisTimeout.getAttribute("id"));
            else {
                IB.procesando.ocultar();
                IB.bsalert.toastwarning("No se ha podido determinar el código de asunto");
            }
        }, 200);
    }

    var setAccion = function (event) {
        view.marcarLinea(this);
    }

    var salir = function (e) {
        IB.procesando.mostrar();
        //$.when(controlarSalir(e)).then(function () {
        //setTimeout(function () {
        if (IB.vars.origen=="reporteIAP")
            location.href = "../../ImpDiaria/Default.aspx?" + IB.uri.encode(IB.vars.qs);
        else
            IB.bsalert.toastwarning("No se ha indicado pantalla de regreso");
        //}, 500);
        //});
    }

    var setBitacoraPT = function (event) {
        view.marcarLinea(this);
    }
    var eliminarAsunto = function (event) {
        if (IB.vars.hdnAcceso != "E") {
            IB.bsalert.toastwarning("Bitácora no modificable");
            return;
        }
        IB.procesando.ocultar();
        $.when(IB.bsconfirm.confirm("warning", "Borrado de asunto", "El borrado de un asunto desencadena el borrado de todas sus acciones.<br><br>¿Deseas borrar el asunto?.</br></br>")).then(function () {
            var aLineas = [];
            var sIdAsunto = $(view.selectores.filaAsuntoSel).attr("id");
            var linea = { idAsunto: sIdAsunto };
            aLineas.push(linea);
            borrarAsuntos(aLineas);
        });
    }
    var borrarAsuntos = function (lineas) {
        IB.procesando.mostrar();
        var payload = { lineas: lineas };
        IB.DAL.post(null, "borrarAsuntos", payload, null,
            function (data) {
                view.dom.tblAsunto.row('.activa').remove().draw(false);
                obtenerAcciones(-1);
                IB.procesando.ocultar();
                grabacionCorrecta();
            }
        );
    }
    var eliminarAccion = function (event) {
        if (IB.vars.hdnAcceso != "E") {
            IB.bsalert.toastwarning("Bitácora no modificable");
            return;
        }
        IB.procesando.ocultar();
        $.when(IB.bsconfirm.confirm("warning", "Borrado de acción", "¿Deseas borrar la acción?.</br></br>")).then(function () {
            var aLineas = [];
            var sIdAccion = $(view.selectores.filaAccionSel).attr("id");
            var linea = { t383_idaccion: sIdAccion };
            aLineas.push(linea);
            borrarAcciones(aLineas);
        });
    }
    var borrarAcciones = function (lineas) {
        IB.procesando.mostrar();
        var payload = { lineas: lineas };
        IB.DAL.post(null, "borrarAcciones", payload, null,
            function (data) {
                view.dom.tblAccion.row('.activa').remove().draw(false);
                IB.procesando.ocultar();
                grabacionCorrecta();
            }
        );
    }
    var grabacionCorrecta = function () {
        IB.bsalert.toast("Grabación correcta.", "info");
        //desactivarGrabar();
    }

    //Acceso a otras pantallas
    var addAsunto = function (e) {
        //alert("Pendiente acceso al detalle de asunto de PSN " + IB.vars.nPSN);
        if (IB.vars.hdnAcceso != "E") {
            IB.bsalert.toastwarning("Bitácora no modificable");
            return;
        }
        IB.procesando.mostrar();
        var sAux = mergeParam(IB.vars.qs, "idAsunto=-1&p=" + IB.vars.hdnAcceso + "&nPE=" + accounting.unformat(view.dom.idProyecto.val(), ",") + "&desPE=" + view.dom.desProyecto.val() + "&nPSN=" + IB.vars.nPSN);
        qs = IB.uri.encode(sAux);
        location.href = IB.vars.strserver + "Capa_Presentacion/IAP30/Reporte/Bitacora/Asunto/AsuntoPE/Default.aspx?" + qs;
    }
    var goAsunto = function (e) {
        IB.procesando.mostrar();
        var sIdAsunto = $(view.selectores.filaAsuntoSel).attr("id");
        if (!sIdAsunto) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar un asunto");
            return;
        }
        //alert("Pendiente acceso al detalle de asunto de PSN " + IB.vars.nPSN + " y asunto " + sIdAsunto);
        
        //qs = IB.uri.encode(IB.vars.qs + "&as=" + sIdAsunto + "&p=" + IB.vars.hdnAcceso + "&nPE=" + accounting.unformat(view.dom.idProyecto.val(), ",") + "&desPE=" + view.dom.desProyecto.val() + "&nPSN=" + IB.vars.nPSN);
        var sAux = mergeParam(IB.vars.qs, "idAsunto=" + sIdAsunto + "&p=" + IB.vars.hdnAcceso + "&nPE=" + accounting.unformat(view.dom.idProyecto.val(), ",") + "&desPE=" + view.dom.desProyecto.val() + "&nPSN=" + IB.vars.nPSN);
        qs = IB.uri.encode(sAux);
        location.href = IB.vars.strserver + "Capa_Presentacion/IAP30/Reporte/Bitacora/Asunto/AsuntoPE/Default.aspx?" + qs;

    }
    var addAccion = function (e) {
        if (IB.vars.hdnAcceso != "E") {
            IB.bsalert.toastwarning("Bitácora no modificable");
            return;
        }
        IB.procesando.mostrar();
        var sIdAsunto = $(view.selectores.filaAsuntoSel).attr("id");
        var sIdResponsable = $(view.selectores.filaAsuntoSel).attr("idR");
        //alert("Pendiente acceso al detalle de accion de PSN " + IB.vars.nPSN + " para el asunto " + sIdAsunto);

        if ((typeof sIdAsunto === "undefined") || (typeof sIdResponsable === "undefined")) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar un asunto");
            return;
        }

        var sAux = mergeParam(IB.vars.qs, "idAccion=-1&r=" + sIdResponsable + "&idAsunto=" + sIdAsunto + "&p=" + IB.vars.hdnAcceso + "&nPE=" + accounting.unformat(view.dom.idProyecto.val(), ",") + "&desPE=" + view.dom.desProyecto.val());
        qs = IB.uri.encode(sAux);
        location.href = IB.vars.strserver + "Capa_Presentacion/IAP30/Reporte/Bitacora/Accion/AccionPE/Default.aspx?" + qs;
    }
    var goAccion = function (e) {
        IB.procesando.mostrar();
        var sIdAsunto = $(view.selectores.filaAsuntoSel).attr("id");
        var sIdResponsable = $(view.selectores.filaAsuntoSel).attr("idR");
        var sIdAccion = $(view.selectores.filaAccionSel).attr("id");
        if (!sIdAccion) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar una acción");
            return;
        }
        //alert("Pendiente acceso al detalle de accion de PSN " + IB.vars.nPSN + " para la acción " + sIdAccion + " del asunto " + sIdAsunto);
        
        var sAux = mergeParam(IB.vars.qs, "idAccion=" + sIdAccion + "&r=" + sIdResponsable + "&idAsunto=" + sIdAsunto + "&p=" + IB.vars.hdnAcceso + "&nPE=" + accounting.unformat(view.dom.idProyecto.val(), ",") + "&desPE=" + view.dom.desProyecto.val());
        qs = IB.uri.encode(sAux);
        location.href = IB.vars.strserver + "Capa_Presentacion/IAP30/Reporte/Bitacora/Accion/AccionPE/Default.aspx?" + qs;
    }
    var goPT = function (e) {
        IB.procesando.mostrar();
        var sIdPT = $(view.selectores.filaPTSel).attr("id");
        var sAcceso = $(view.selectores.filaPTSel).attr("acceso");
        if (!sIdPT) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar un proyecto técnico");
            return;
        }
        if (sAcceso=="X") {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("El proyecto técnico no permite acceso a su bitácora desde IAP");
            return;
        }
        if (IB.vars.origen == "reporteIAP") {
            if (IB.vars.nPSN != "") {
                //qs = IB.uri.encode(IB.vars.qs + "&ori2=bitacora&nPT=" + sIdPT);
                var sAux = mergeParam(IB.vars.qs, "ori2=bitacora&nPT=" + sIdPT);
                qs = IB.uri.encode(sAux);
            }
            else {
                    //qs = IB.uri.encode(IB.vars.qs + "&ori2=bitacora&nPSN=" + IB.vars.nPSN + "&nPT=" + sIdPT);
                    var sAux = mergeParam(IB.vars.qs, "ori2=bitacora&nPSN=" +IB.vars.nPSN + "&nPT=" +sIdPT);
                    qs = IB.uri.encode(sAux);
                }
        }
        else{
            //qs = IB.uri.encode("&nPSN=" +IB.vars.nPSN + "&nPT=" +sIdPT);
            var sAux = mergeParam(IB.vars.qs, "nPSN=" +IB.vars.nPSN + "&nPT=" + sIdPT);
            qs = IB.uri.encode(sAux);
        }
        location.href = "../BitacoraPT/Default.aspx?" + qs;
    }
    function excelAsunto() {

        if (!view.dom.tblAsunto.rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tablaAsunto, "Asuntos", "Bitacora de Proyecto Económico - Asunto", false);

        //try {
        //    IB.procesando.mostrar();
        //    if (!view.dom.tblAsunto.rows().count()) {
        //        IB.procesando.ocultar();
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }

        //    var sb = new StringBuilder();
        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        //    sb.append("	<tr align=center style='background-color: #BCD4DF;'>");
        //    sb.append("     <td style='width:400px'>Denominación</TD>");
        //    sb.append("     <td style='width:100px'>Tipo</TD>");
        //    sb.append("     <td style='width:100px'>Severidad</TD>");
        //    sb.append("     <td style='width:100px'>Prioridad</TD>");
        //    sb.append("     <td style='width:100px'>Límite</TD>");
        //    sb.append("     <td style='width:100px'>Notificación</TD>");
        //    sb.append("     <td style='width:100px'>Estado</TD>");
        //    sb.append(" </tr>");

        //    view.dom.bodyAsunto.find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        for (var x = 0; x < 7; x++) {
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
    function excelAccion() {

        if (!view.dom.tblAccion.rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tablaAccion, "Acciones", "Bitacora de Proyecto Económico - Acción", false);


        //IB.procesando.mostrar();
        //try {
        //    if (!view.dom.tblAccion.rows().count()) {
        //        IB.procesando.ocultar();
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }

        //    var sb = new StringBuilder();

        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        //    sb.append("	<tr align=center style='background-color: #BCD4DF;'>");
        //    sb.append("     <td style='width:400px'>Denominación</TD>");
        //    sb.append("     <td style='width:80px'>Límite</TD>");
        //    sb.append("     <td style='width:80px'>Avance</TD>");
        //    sb.append("     <td style='width:80px'>Finalización</TD>");
        //    sb.append(" </tr>");

        //    //$(view.selectores.bodyAccion).find("tr").each(function () {
        //    view.dom.bodyAccion.find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'>");
        //        for (var x = 0; x < 4; x++) {
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
    function excelPT() {

        if (!view.dom.tblPT.rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel(view.dom.tablaPT, "ProyectosTecnicos", "Bitacora de Proyecto Económico - Proyectos técnicos", false);

        //IB.procesando.mostrar();
        //try {
        //    if (!view.dom.tblPT.rows().count()) {
        //        IB.procesando.ocultar();
        //        IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
        //        return;
        //    }

        //    var sb = new StringBuilder();
        //    sb.append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        //    sb.append("	<tr align=center style='background-color: #BCD4DF;'>");
        //    sb.append("     <td style='width:500px'>Denominación</TD>");
        //    sb.append(" </tr>");

        //    view.dom.bodyPT.find("tr").each(function () {
        //        sb.append("<tr style='vertical-align:top;'><td>");
        //        sb.append($(this).children().eq(0).text());
        //        sb.append("</td></tr>");
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

})(SUPER.IAP30.BitacoraPE.View);
