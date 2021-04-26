var SUPER = SUPER || {};
SUPER.formaPM = SUPER.formaPM || {}
SUPER.formaPM.models = SUPER.formaPM.models || {}

SUPER.formaPM.models.Persona = (function () {

    function Persona(o) {
        this.idficepi = o.idficepi;
        this.nombre = o.nombre;
        this.apellido = o.apellido;
        this.concatenado = this.nombre + " " + this.apellido;
    }

    Persona.fromNet = function(o) {
        return new Persona(o);
    }

    return Persona;


})();