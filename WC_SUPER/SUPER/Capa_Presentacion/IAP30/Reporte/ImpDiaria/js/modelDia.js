var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ImpDiaria = SUPER.IAP30.ImpDiaria || {};
SUPER.IAP30.ImpDiaria.Models = SUPER.IAP30.ImpDiaria.Models || {};

SUPER.IAP30.ImpDiaria.Models.Dia = (function () {

    'use strict';

    function Dia(item) {

        this.diaSemana = item.diaSemana || null;
        this.fechaSemana = item.fechaSemana || null;
        this.horasJornada = item.horasJornada || null;
        this.totalConsumo = item.totalConsumo || null;
        this.totalConsumoProys = item.totalConsumoProys || null;
        this.laborable = item.laborable || null;
        this.festivo = item.festivo || null;
    }

    Dia.noValues = function () {

        return new Dia({});;
    }

    Dia.fromObject = function (item) {
        return new Dia(item);
    };

    Dia.fromValues = function (diaSemana, fechaSemana, horasJornada, festivo) {

        var o = new Dia({});

        o.diaSemana = diaSemana || 0;
        o.fechaSemana = fechaSemana || new Date();
        o.horasJornada = horasJornada || 0;
        o.totalConsumo = totalConsumo || 0;
        o.totalConsumoProys = totalConsumoProys || 0;
        o.laborable = laborable || 0;
        o.festivo = festivo || 0;

        return o;
    }

    return Dia;
})();