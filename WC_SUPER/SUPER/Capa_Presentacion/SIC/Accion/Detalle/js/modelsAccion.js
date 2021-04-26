var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};
SUPER.SIC.Models = SUPER.SIC.Models || {};

SUPER.SIC.Models.Accion = (function () {

    'use strict';

    function Accion(item) {

        this.ta204_idaccionpreventa = item.ta204_idaccionpreventa || 0;
        this.ta204_idaccionpreventa_encrypt = item.ta204_idaccionpreventa_encrypt || 0;
        this.ta204_fechacreacion = item.ta204_fechacreacion || new Date();
        this.ta204_fechafinestipulada = item.ta204_fechafinestipulada || new Date();
        this.ta204_descripcion = item.ta204_descripcion || "";
        this.ta204_observaciones = item.ta204_observaciones || "";
        this.ta199_idunidadpreventa = item.ta199_idunidadpreventa || "";
        this.ta200_idareapreventa = item.ta200_idareapreventa || "";
        this.ta201_idsubareapreventa = item.ta201_idsubareapreventa || "";
        this.ta205_idtipoaccionpreventa = item.ta205_idtipoaccionpreventa || "";
        this.t001_idficepi_lider = item.t001_idficepi_lider || 0;
        this.lider = item.lider || "";
        this.ta206_idsolicitudpreventa = item.ta206_idsolicitudpreventa || 0;
        this.ta204_motivoanulacion = item.ta204_motivoanulacion || "";
        this.ta204_estado = item.ta204_ta204_estado || "";
        
        
    }

    Accion.fromObject = function (item) {
        return new Accion(item);
    };

    Accion.fromValues = function (ta204_idaccionpreventa, ta204_idaccionpreventa_encrypt, ta204_fechafinestipulada,
                                  ta204_descripcion, ta204_observaciones, ta199_idunidadpreventa,
                                  ta200_idareapreventa, ta201_idsubareapreventa, ta205_idtipoaccionpreventa,
                                  t001_idficepi_lider, lider) {

        var o = new Accion({});

        o.ta204_idaccionpreventa = ta204_idaccionpreventa || 0;
        o.ta204_idaccionpreventa_encrypt = ta204_idaccionpreventa_encrypt || 0;
        o.ta204_fechafinestipulada = ta204_fechafinestipulada || new Date();
        o.ta204_descripcion = ta204_descripcion;
        o.ta204_observaciones = ta204_observaciones;
        o.ta199_idunidadpreventa = ta199_idunidadpreventa;
        o.ta200_idareapreventa = ta200_idareapreventa;
        o.ta201_idsubareapreventa = ta201_idsubareapreventa;
        o.ta205_idtipoaccionpreventa = ta205_idtipoaccionpreventa;
        o.t001_idficepi_lider = t001_idficepi_lider || null;
        o.lider = lider

        return o;
    }

    return Accion;
})();