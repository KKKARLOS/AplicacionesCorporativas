var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};

SUPER.IAP30.Models = SUPER.IAP30.Models || {};

SUPER.IAP30.Models.Documento = (function () {

    'use strict';    
    function Documento(item) {
       
        this.idDocumento = item.idDocumento || 0;
        this.t2_iddocumento = item.t2_iddocumento || null;
        this.idElemento = item.idElemento || null;
        this.descripcion = item.descripcion || "";
        this.weblink = item.weblink || "";
        this.nombrearchivo = item.nombrearchivo || "";
        this.modolectura = item.modolectura || false;
        this.privado = item._privado || false;
        this.tipogestion = item.tipogestion || true;
        this.fecha = item.fecha || new Date();
        this.idusuario_autor = item.idusuario_autor || null;
        this.fechamodif = item.tfechamodif || new Date();  
        this.autor = item.autor || null;
        this.autormodif = item.autormodif || null;
        this.kbytes = item.kbytes || 0;
        this.fileupdated = item.fileupdated || false;
        this.editable = item.editable || false;
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
        return this.idElemento === other.idElemento;
    };

    return Documento;

})();