
$(document).ready(function () { SUPER.SIC.appCatAccion.init(); });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.appCatAccion = (function (view, dal) {

    var _ta206_itemorigen = IB.vars.ta206_itemorigen;
    var _ta206_iditemorigen = IB.vars.ta206_iditemorigen;
    var _ta204_idaccionpreventa = null;

    function init() {

        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        view.init();


        //Eventos
        view.attachLiveEvents("click", view.selector.tabs, view.tabSeleccionado);
        
        view.attachLiveEvents("click", view.selector.edicion_tareaParticipante, function () {

            var el = this;
            
            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                //Quitamos la negrita 
                var payload = { idtarea: $(this).attr("data-idtarea") }
                dal.post(null, "quitarNegritasTareasPendientesParticipante", payload, null, function (data) {
                }
            );
            }
            var _idAccion = $(this).parent().parent().attr("data-idaccion");
            var _idTarea = $(this).attr("data-idtarea");
            var params = "filters=";
            params += "idaccion:" + _idAccion;
            params += "|radioSeleccionado:2";

            params = IB.uri.encode(params);
            var newurl = location.href.substr(0, location.href.indexOf("?") != -1 ? location.href.indexOf("?") : location.href.length) + "?" + params;

            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
                function () {
                    var qs = IB.uri.encode("idAccion=" + _idAccion + "&idTarea=" + _idTarea + "&modoPantalla=E");
                    window.location.href = "../../Tarea/DetalleTarea/Default.aspx?" + qs;
                });

        })

        view.attachLiveEvents("click", view.selector.edicion_tarea, function () {

            var el = this;

            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                //Quitamos la negrita 
                var payload = { idtarea: $(this).attr("data-idtarea") }
                dal.post(null, "quitarNegritasTareasPendientesParticipante", payload, null, function (data) {
                }
            );
            }


            var _idAccion = $(this).attr("data-idaccion");
            var _idTarea = $(this).attr("data-idtarea");

            var params = "filters=";
            params += "idaccion:" + _idAccion;

            params = IB.uri.encode(params);
            var newurl = location.href.substr(0, location.href.indexOf("?") != -1 ? location.href.indexOf("?") : location.href.length) + "?" + params;

            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
                function () {
                    var qs = IB.uri.encode("idAccion=" + _idAccion + "&idTarea=" + _idTarea + "&modoPantalla=E");
                    window.location.href = "../../Tarea/DetalleTarea/Default.aspx?" + qs;
                });

        });

        view.attachLiveEvents("click", view.selector.edicion_accion, function () {
            
            var el = this;

            var bNegrita = view.tieneNegrita($(this));
            if (bNegrita) {
                //Quitamos la negrita de la acción seleccionada
                var payload = { idaccion: $(this).attr("data-idaccion") }
                dal.post(null, "quitarNegritasTareasPendientesAccion", payload, null, function (data) {
                }
            );
            }


            //Guardar filtros en el historial y navegar al detalle
            var params = "filters=";
            params += "idaccion:" + $(this).attr("data-idaccion");
            params += "|radioSeleccionado:" + view.obtenerRadioSeleccionado();
            

            params = IB.uri.encode(params);
            var newurl = location.href.substr(0, location.href.indexOf("?") != -1 ? location.href.indexOf("?") : location.href.length) + "?" + params;

            IB.DAL.post(IB.vars.strserver + "Services/Historial.asmx", "Reemplazar", { newUrl: newurl }, null,
                function () {
                    var qs = IB.uri.encode("modo=E&id=" + $(el).attr("data-idaccion") + "&origenpantalla=SUPER&caller=accioneslider");
                    window.location.href = "../Detalle/Default.aspx?" + qs;
                });
            
        });
        //FIN Eventos


        
    }

    return {
        init: init
    }

})(SUPER.SIC.viewCatAccion, IB.DAL);