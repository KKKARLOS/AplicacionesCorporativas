
$(document).ready(function () { SUPER.SIC.appCatAccion.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appCatAccion = (function (view) {


    function init() {
        
        console.log("origenMenu=" + IB.vars.origenMenu);

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        view.init();

        view.attachLiveEvents("click", view.selector.edicion_accion, mostrarDetalle);

        function mostrarDetalle() {

            var el = this;
            var filter = view.getFilterValuesWithDesc();

            //Guardar filtros en el historial y navegar al detalle
            var params = "filters=ejecutar:true";

            if (filter.estadoParticipacion != null) params += "|estadoParticipacion:" + filter.estadoParticipacion;
            if (filter.estado != null) params += "|estado:" + filter.estado;
            if (filter.itemorigen != null) params += "|itemorigen:" + filter.itemorigen;
            if (filter.iditemorigen != null) params += "|iditemorigen:" + filter.iditemorigen;            
            if (filter.ffinDesde != null) params += "|ffinDesde:" + filter.ffinDesde;
            if (filter.ffinHasta != null) params += "|ffinHasta:" + filter.ffinHasta;
            if (filter.promotor != null) params += "|promotor:" + filter.promotor;
            if (filter.comercial != null) params += "|comercial:" + filter.comercial;
            if (filter.lideres != null) params += "|lideres:" + filter.lideres.join(";");
            if (filter.clientes != null) params += "|clientes:" + filter.clientes.join(";");
            if (filter.acciones != null) params += "|acciones:" + filter.acciones.join(";");
            if (filter.unidades != null) params += "|unidades:" + filter.unidades.join(";");
            if (filter.areas != null) params += "|areas:" + filter.areas.join(";");
            if (filter.subareas != null) params += "|subareas:" + filter.subareas.join(";");

            params += "&origenmenu=" + IB.vars.origenMenu;
            params = IB.uri.encode(params);

            var newurl = location.href.substr(0, location.href.indexOf("#") != -1 ? location.href.indexOf("#") : location.href.length);
            newurl = newurl.substr(0, newurl.indexOf("?") != -1 ? newurl.indexOf("?") : newurl.length) + "?" + params;

            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
                function () {
                    var qs = IB.uri.encode("idAccion=" + $(el).attr("data-idaccion") + "&idTarea=" + $(el).attr("data-idtarea") + "&modoPantalla=E");
                    window.location.href = "../DetalleTarea/Default.aspx?" + qs;
                });

            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                //Quitamos la negrita 
                var payload = { idtarea: $(this).attr("data-idtarea") }
                IB.DAL.post(null, "quitarNegritasTareasPendientes", payload, null, function (data) {
                }
            );
            }

        }


    }



    return {
        init: init
    }

})(SUPER.SIC.viewCatAccion);