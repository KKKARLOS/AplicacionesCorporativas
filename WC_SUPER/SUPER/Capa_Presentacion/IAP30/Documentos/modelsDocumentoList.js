var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};

SUPER.IAP30.Models = SUPER.IAP30.Models || {};

SUPER.IAP30.Models.DocumentoList = (function () {

    'use strict';

    function DocumentoList(documentos) {
        this._documentos = documentos;
    }

    DocumentoList.create = function (documentos) {
        return new DocumentoList(documentos);
    };

    DocumentoList.prototype.all = function () {
        return this._documentos;
    };

    DocumentoList.prototype.size = function () {
        return this._documentos.length;
    };

    DocumentoList.prototype.add = function (documento) {
        this._documentos.push(documento);
    }

    DocumentoList.prototype.update = function (documento) {
        $.extend(this.get(documento.idDocumento), documento);
    }

    DocumentoList.prototype.delete = function (item) {
        var index = this._documentos.indexOf(item);
        return this._documentos.splice(index, 1);
    }

    DocumentoList.prototype.get = function (id) {

        var arr = this._documentos.filter(function (o) {
            return o.idDocumento == id
        })

        if (arr.length == 0)
            return null;
        else if (arr.length == 1)
            return arr[0];
        else
            throw ("Multiple items with same id.");
    }

    return DocumentoList;
})();