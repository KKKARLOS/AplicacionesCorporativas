/// <reference path="../../../../../scripts/IB.js" />

$(document).ready(function () { SUPER.SIC.appCatAccion.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appCatAccion = (function (view) {


    function init() {

        console.log("appCatAccion.init");

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        view.init();

        view.attachLiveEvents("click", view.selector.edicion_accion, mostrarDetalle);
        view.attachLiveEvents("click", ".btnExportExcel", exportarExcel);

        $("#btnTest").on("click", mostrarDetalle);

        function mostrarDetalle() {

            var el = this;
            var filter = view.getFilterValuesWithDesc();

            //Guardar filtros en el historial y navegar al detalle
            var params = "ejecutar:true";

            if (filter.estado != null) params += "|estado:" + filter.estado;
            if (filter.itemorigen != null) params += "|itemorigen:" + filter.itemorigen;
            if (filter.iditemorigen != null) params += "|iditemorigen:" + filter.iditemorigen;
            if (filter.importeDesde != null) params += "|importeDesde:" + filter.importeDesde;
            if (filter.importeHasta != null) params += "|importeHasta:" + filter.importeHasta;
            if (filter.ffinDesde != null) params += "|ffinDesde:" + filter.ffinDesde;
            if (filter.ffinHasta != null) params += "|ffinHasta:" + filter.ffinHasta;
            if (filter.promotor != null) params += "|promotor:" + filter.promotor;
            if (filter.lideres != null) params += "|lideres:" + filter.lideres.join(";");
            if (filter.clientes != null) params += "|clientes:" + filter.clientes.join(";");
            if (filter.acciones != null) params += "|acciones:" + filter.acciones.join(";");
            if (filter.unidades != null) params += "|unidades:" + filter.unidades.join(";");
            if (filter.areas != null) params += "|areas:" + filter.areas.join(";");
            if (filter.subareas != null) params += "|subareas:" + filter.subareas.join(";");

            //if (params.length > 0) {
            params = IB.uri.encode("filters=" + params);
            var newurl = location.href.substr(0, location.href.indexOf("#") != -1 ? location.href.indexOf("#") : location.href.length);
            newurl = newurl.substr(0, newurl.indexOf("?") != -1 ? newurl.indexOf("?") : newurl.length) + "?" + params;

            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
                function () {
                    var qs = IB.uri.encode("modo=E&id=" + $(el).attr("data-idaccion") + "&origenpantalla=CRM&caller=catalogofigareasubarea");
                    window.location.href = "../Detalle/Default.aspx?" + qs;
                });

        }

    }

    //Exporta a Excel los datos de pantalla
    function exportarExcel() {

        if (!$("#tblAcciones").DataTable().rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }

        IB.Exportaciones.exportarDataTableExcel($("#tblAcciones"), "AccionesSolicitadas", "Catálogo de acciones preventa solicitadas", false);        
    }

    return {
        init: init
    }

})(SUPER.SIC.viewCatAccion);