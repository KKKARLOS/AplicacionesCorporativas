/*var IAP = IAP || {};
IAP.calendario = IAP.calendario || {}
IAP.calendario.view = (function () {

});

		
$(document).ready(function () {

    $('#cal').monthly({
    });

    var monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
    var dayNames = ["L", "M", "X", "J", "V", "S", "D"];

    var events = [
        {
            date: "",
            title: '',
            link: '',
            color: '',
            content: '',
            class: '',
        }
    ];

    $('#calendari_lateral1').bic_calendar({
        //list of events in array
        events: events,
        //enable select
        enableSelect: true,
        //enable multi-select
        multiSelect: true,
        //set day names
        dayNames: dayNames,
        //set month names
        monthNames: monthNames,
        //show dayNames
        showDays: true,
    });

    $('#slideImp').BootSideMenu({ side: "right" });
    if ($('#indicator').is(':visible')) {
        $("#L").html('L');
        $("#M").html('M');
        $("#X").html('X');
        $("#J").html('J');
        $("#V").html('V');
        $("#S").html('S');
        $("#D").html('D');

    } else {
        $("#L").html('Lunes');
        $("#M").html('Martes');
        $("#X").html('Miércoles');
        $("#J").html('Jueves');
        $("#V").html('Viernes');
        $("#S").html('Sábado');
        $("#D").html('Domingo');
    }
});

$('#anual').on('click', function (event) {
    $('#calendarioAnual').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#calendarioAnual').modal('show');
});

$(window).resize(function () {
    if ($('#slideImp').find(".toggler").parent().attr('data-status') == "closed") {
       $('#test2').css('right', '-95%');
    }


    if ($('#indicator').is(':visible')) {
        $("#L").html('L');
        $("#M").html('M');
        $("#X").html('X');
        $("#J").html('J');
        $("#V").html('V');
        $("#S").html('S');
        $("#D").html('D');
    } else {
        $("#L").html('Lunes');
        $("#M").html('Martes');
        $("#X").html('Miércoles');
        $("#J").html('Jueves');
        $("#V").html('Viernes');
        $("#S").html('Sábado');
        $("#D").html('Domingo');
    }
});

$('.monthly-day-pick').on('click', function () {
    location.href = '../ImpDiaria/Default.aspx';
});

*/
function AnnomesAFecha(nAnnoMes) {
    if (ValidarAnnomes(nAnnoMes))
        return new Date(nAnnoMes.toString().substring(0, 4), eval(nAnnoMes.toString().substring(4, 6) - 1), 1);
    else
        return new Date(1900, 0, 1);
}

function FechaAAnnomes(dFecha) {
    return (dFecha.getFullYear() * 100 + dFecha.getMonth() + 1);
}

function ValidarAnnomes(nAnnoMes) {
    if (nAnnoMes.toString().length != 6) {
        mostrarErrorAplicacion("La longitud del AnnoMes no es de seis dígitos", "");
        return false;
    }
    if (nAnnoMes % 100 < 1 || nAnnoMes % 100 > 12) {
        mostrarErrorAplicacion("El mes no es coherente. Menor de 1 o mayor de 12.", "");
        return false;
    }
    if (nAnnoMes / 100 < 1900 || nAnnoMes / 100 > 2078) {
        mostrarErrorAplicacion("El año no es coherente. Menor de 1900 o mayor de 2078.", "");
        return false;
    }

    return true;
}
Date.prototype.add = function (sInterval, iNum) {
    var dTemp = this;
    if (!sInterval || iNum == 0) return dTemp;
    switch (sInterval.toLowerCase()) {
        case "ms": dTemp.setMilliseconds(dTemp.getMilliseconds() + iNum); break;
        case "s": dTemp.setSeconds(dTemp.getSeconds() + iNum); break;
        case "mi": dTemp.setMinutes(dTemp.getMinutes() + iNum); break;
        case "h": dTemp.setHours(dTemp.getHours() + iNum); break;
        case "d": dTemp.setDate(dTemp.getDate() + iNum); break;
        case "mo": dTemp.setMonth(dTemp.getMonth() + iNum); break;
        case "y": dTemp.setFullYear(dTemp.getFullYear() + iNum); break;
    }
    return dTemp;
}
/*
//ejemplos
var d = new Date();
var d2 = d.add("d", 3); //+3days
var d3 = d.add("h", -3); //-3hours
alert(d2);
alert(d3);
*/

Date.prototype.ToShortDateString = function () {
    try {
        //var dTemp = this;
        var sDia = this.getDate().toString();
        var sMes = (this.getMonth() + 1).toString();
        var sAnno = this.getFullYear().toString();
        if (sDia.length == 1) sDia = "0" + sDia;
        if (sMes.length == 1) sMes = "0" + sMes;

        return sDia + "/" + sMes + "/" + sAnno;
    } catch (e) {
        mostrarErrorAplicacion("Error al pasar una fecha a ToShortDateString() ", e.message);
        return false
    }
}
var aMeses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
function AnoMesToMesAnoDescLong(nAnoMes) {
    try {
        if (nAnoMes == "") {
            mmoff("Inf", "Dato de año/mes no válido", 170);
            return;
        }
        return aMeses[parseInt(nAnoMes.toString().substring(4, 6), 10) - 1] + " " + nAnoMes.toString().substring(0, 4);
    } catch (e) {
        mostrarErrorAplicacion("Error al convertir el annomes a mes-año desc", e.message);
    }
}
