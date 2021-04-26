$(document).ready(function () { });

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.app = (function (view, dal) {

    var _ta200_idareapreventa = null;
    var _ta201_idsubareapreventa = null;
    var bcambios = false;

    if (IB.vars.error) {
        IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
        return;
    }

    window.onbeforeunload = function () {
        if (view.comprobarCambios()) return 'Hay cambios sin guardar. Si continúas perderás dichos cambios.';
    };

    
    view.init();
    
    //Eventos
    view.attachEvents("click", view.dom.btnGrabar, grabar);

    function getpplporsubareaparaficepi() {
        
        dal.post(null, "getpplporsubareaparaficepi", null, null,
               function (data) {
                   if (data.length > 0) {
                       //render lista de profesionales
                       view.renderprofesionales(data);
                   }
               }
           );
    }

    function grabar() {
        var lista = view.obtenerDatosGrabar();
        var payload = { lstSubareas: lista}

        if (lista != false) {
            dal.post(null, "grabar", payload, null,
             function (data) {
                 bcambios = false;
                 view.refreshDatatable();
                 IB.bsalert.toast("Grabación correcta.");
             }
         );
        }
       
    }
    
  
})(SUPER.SIC.view, IB.DAL);