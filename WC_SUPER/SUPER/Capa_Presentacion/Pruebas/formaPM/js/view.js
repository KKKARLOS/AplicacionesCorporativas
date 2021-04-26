var SUPER = SUPER || {};
SUPER.formaPM = SUPER.formaPM || {}


SUPER.formaPM.view = (function () {


    var selectores = {
        nombre: ".fk_nombre",
        apellido: ".fk_apellido",
        input: "#contenido input"
    }

    var dom = {
        btnBuscar: $("#btnBuscar"),
        filtro: $("#txtfiltro"),
        contenido: $("#contenido"),
        btnLimpiar: $("#btnLimpiar"),
        row: function (persona) {

            return "<tr data-idficepi='" + persona.idficepi + "' >" +
                   "<td><input class='fk_nombre' type='text' value='" + persona.nombre + "' /></td>" +
                   "<td><input class='fk_apellido' type='text' value='" + persona.apellido + "' /></td>" + 
                   "<td><span>" + persona.concatenado + "</span></td></tr>";
        },
        
    }

    var atributos = {
        idficepi: "data-idficepi"

    }
  
    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {

        console.log("initview");
        dom.btnLimpiar.on("click", limpiar);
    }

    var obtenerfiltro = function(){

        return dom.filtro.val();

    }

    var limpiar = function () {
        console.log("limpiar");
        dom.filtro.val("");
        dom.contenido.html("");
    }

    function pintarTabla(filas) {

        var h = "";

        filas.map(dom.row).forEach(function (oRow) { h += oRow });

        dom.contenido.html(h);
    }

    var obtenerInputModificado = function (el) {

        var datos = new Object();

        datos.idficepi = $(el).parent().parent().attr(atributos.idficepi);

        var contenedor = dom.contenido.find("[" + atributos.idficepi + " = '" + datos.idficepi + "']")

        datos.nombre = $(contenedor).find(selectores.nombre).val();
        datos.apellido = $(contenedor).find(selectores.apellido).val();

        return datos;
    }

    

    return {
        init: init,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        dom: dom,
        selectores: selectores,
        obtenerfiltro: obtenerfiltro,
        pintarTabla: pintarTabla,
        obtenerInputModificado: obtenerInputModificado
    }

})();