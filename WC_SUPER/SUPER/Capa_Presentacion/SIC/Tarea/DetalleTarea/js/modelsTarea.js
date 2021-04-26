var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};
SUPER.SIC.Models = SUPER.SIC.Models || {};

SUPER.SIC.Models.Tarea = (function () {

    'use strict';

    function Tarea(item) {

        this.ta204_idaccionpreventa = item.ta204_idaccionpreventa || 0;
        this.ta204_idaccionpreventa_encrypt = item.ta204_idaccionpreventa_encrypt || 0;
        this.ta207_idtareapreventa = item.ta207_idaccionpreventa || 0;
        this.ta207_idtareapreventa_encrypt = item.ta207_idaccionpreventa_encrypt || 0;
        this.ta207_denominacion = item.ta207_denominacion || "";
        this.ta207_descripcion = item.ta207_descripcion || "";
        this.ta207_comentarios = item.ta207_comentarios || "";
        this.ta207_fechafinreal = item.ta207_fechafinreal || new Date();
        this.ta207_fechafinprevista = item.ta207_fechafinprevista || new Date();
        this.ta207_estado = item.ta207_estado || "";
        this.ta207_motivoanulacion = item.ta207_motivoanulacion || "";

    }

    Accion.fromObject = function (item) {
        return new Tarea(item);
    };

    Accion.fromValues = function (ta204_idaccionpreventa, ta204_idaccionpreventa_encrypt, ta207_idtareapreventa, ta207_idtareapreventa_encrypt,
                                  ta207_denominacion, ta207_descripcion, ta207_comentarios, ta207_fechafinreal, ta207_fechafinprevista, ta207_estado, ta207_motivoanulacion) {

        var o = new Tarea({});

        o.ta204_idaccionpreventa = ta204_idaccionpreventa || 0;
        o.ta204_idaccionpreventa_encrypt = ta204_idaccionpreventa_encrypt || 0;

        o.ta207_idtareapreventa = ta207_idaccionpreventa || 0;
        o.ta207_idtareapreventa_encrypt = ta207_idaccionpreventa_encrypt || 0;


        o.ta207_denominacion = ta207_denominacion || "";
        o.ta207_descripcion = ta207_descripcion || "";
        o.ta207_comentarios = ta207_comentarios || "";
        o.ta207_fechafinreal = ta207_fechafinreal || new Date();
        o.ta207_fechafinprevista = ta207_fechafinprevista || new Date();
        o.ta207_estado = ta207_estado || "";
        o.ta207_motivoanulacion = ta207_motivoanulacion || "";



        return o;
    }

    return Accion;
})();