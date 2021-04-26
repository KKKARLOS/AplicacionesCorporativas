//$(document).on('ready', function () {
//No usamos el evento ready del documento ya que en él todavia no están registrados los web components
//y no podemos capturar sus elementos del DOM en la vista. En su lugar usamos el evento WebComponentsReady de la ventana

window.addEventListener('WebComponentsReady', function (e) {
    $('#tablaDocu').stacktable();

    IB.app.init();

});

var IB = IB || {};
IB.app = (function () {

    var Dal = null;
    var View = tarea.View(document);
    //Asociar eventos de la vista
    View.attachEvents("keypress click", View.dom.anadir, abrirModalSubir);  //agregar / eliminar favorito 
    View.attachEvents("keypress click", View.dom.lineaTabla, resaltarLinea);
    View.attachEvents("dblclick", View.dom.lineaTabla, abrirModalSubir);
    View.attachEvents("hidden.bs.modal", View.dom.modalSubir, cerrarModalSubir);

    function abrirModalSubir () {

        //calcular el logaritmo neperiano de no se que
        //inicializar las variables tal y cual
        //mostrar modal
        View.abrirModalSubir();

    }

    function cerrarModalSubir() {

        //calcular el logaritmo neperiano de no se que
        //inicializar las variables tal y cual
        //mostrar modal
        View.cerrarModalSubir();

    }

    function resaltarLinea() {
        View.resaltarLinea();
    }

    var init = function () {
        Dal = tarea.Dal;
    }

    return {
        init: init
    }
})();