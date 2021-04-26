var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.Models = SUPER.SIC.Models || {};

SUPER.SIC.Models.Documento = (function () {

    'use strict';

    function Documento(item) {

        this.ta210_iddocupreventa = item.ta210_iddocupreventa || 0;
        this.t2_iddocumento = item.t2_iddocumento || null;
        this.ta210_destino = item.ta210_destino || "";
        this.ta210_descripcion = item.ta210_descripcion || "";
        this.ta210_nombrefichero = item.ta210_nombrefichero || "";
        this.ta210_kbytes = item.ta210_kbytes || 0;
        this.ta210_fechamod = item.ta210_fechamod || new Date();
        this.ta204_idaccionpreventa = item.ta204_idaccionpreventa || null;
        this.ta207_idtareapreventa = item.ta207_idtareapreventa || null;
        this.ta207_denominacion = item.ta207_denominacion || "";
        this.t001_idficepi_autor = item.t001_idficepi_autor || 0;
        this.ta211_idtipodocumento = item.ta211_idtipodocumento || 0;
        this.ta211_denominacion = item.ta211_denominacion || "";
        this.ta210_guidprovisional = item.ta210_guidprovisional || null;
        this.autor = item.autor || "";
        this.fileupdated = item.fileupdated || false;
        this.editable = item.editable || false;
        this.container = item.container || "";
        this.origenEdicion = item.origenEdicion || "";
        this.estado = item.estado || "";
    }

    Documento.fromJson = function (item) {
        return new Documento(item);
    };

    Documento.create = function () {
        return new Documento({});
    }

    Documento.prototype.equals = function (other) {
        return this.ta210_iddocupreventa === other.ta210_iddocupreventa;
    };

    return Documento;
})();