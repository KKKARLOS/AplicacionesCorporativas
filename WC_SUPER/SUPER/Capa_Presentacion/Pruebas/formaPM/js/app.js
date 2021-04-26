$(document).ready(function () { SUPER.formaPM.app.init(); })

var SUPER = SUPER || {};
SUPER.formaPM = SUPER.formaPM || {}



SUPER.formaPM.app = (function (view, oPersona) {

    var init = function () {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }

        view.init();

        view.attachEvents("click", view.dom.btnBuscar, buscar);
        view.attachLiveEvents("change", view.selectores.input, actualizar);

        console.log(IB.vars.strserver);
        console.log(IB.vars.codred);
        console.log(IB.vars.idpersona);


    }

    function buscar() {

        var filtro = { filtro: view.obtenerfiltro() };

        IB.procesando.mostrar();


        IB.DAL.post(null, "buscar", filtro, oPersona.fromNet,
            function (data) {
                view.pintarTabla(data);
                IB.procesando.ocultar();
            },
            function (ex, status) {
                //hacer todo en caso de que falle
                //...
                //...
                //IB.bserror.error$ajax(ex, status);
                IB.bsalert.fixedAlert("danger", "titulooooo", "textooooo del error.");
            }
        );
    }

    function actualizar() {

        var datos = { datos: view.obtenerInputModificado(this) };

        IB.DAL.post(null, "actualizar", datos, null,
            function () {
                //IB.bsalert.fixedAlert("success", "grabación correcta", "los datos se han grabado correctamente")
                IB.bsalert.toast("grabación correcta")
            })
    }


    return {
        init: init
    }

})(SUPER.formaPM.view, SUPER.formaPM.models.Persona);