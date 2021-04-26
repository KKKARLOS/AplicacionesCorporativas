//Se deshabilita el click derecho a nivel de IAP 3.0
document.oncontextmenu = function () { return false };

//selector icontains es igual que el contains de jquery pero insensitive
jQuery.expr[':'].icontains = function (a, i, m) {
    return jQuery(a).text().toUpperCase()
        .indexOf(m[3].toUpperCase()) >= 0;
};

var tamanoModal = function (modal) {

    var doc = $(window).height();
    var head = $(modal).find(".modal-header").height(); // modal header
    if (head < 0) head = Math.abs(head);
    var footer = $(modal).find(".modal-footer").height(); // modal footer
    if (footer < 0) footer = Math.abs(footer);
    if (head == 0 || footer == 0) head = footer = 31; //IE no obtiene el header ni el footer
    var margin = 2 * parseInt($(modal).find('.modal-dialog').css('margin-top'));
    var modheight = doc - head - footer - margin - 65;

    //Si tiene pestañas el scroll se hará sobre el contenedor bajo las pestañas
    var target = $(modal).find('.tab-content');

    if ($(target).length != 0) {
        modheight = modheight - 70;
    } else {
        target = $(modal).find('.modal-body');
    }

    $(target).css('overflow-y', 'auto');
    $(target).css('max-height', modheight);

}

$(document).on('show.bs.modal', '.modal', function (event) {    
    tamanoModal(this);
});

$(window).resize(function () {
    if ($('body').hasClass("modal-open")) {

        $('.modal.in').each(function () {
            
            tamanoModal($(this));

        });
        
    }

    if ($(".hasDatepicker").length > 0) $(".hasDatepicker.calendar-on").datepicker('hide').datepicker('show');
});



$(document).on('hidden.bs.modal', '.modal', function (event) {
    $(this).removeClass('fv-modal-stack');
    $('body').data('fv_open_modals', $('body').data('fv_open_modals') - 1);

    $('.fv-modal-stack').not('.modal-backdrop').css('overflow-x', 'hidden');
    $('.fv-modal-stack').not('.modal-backdrop').css('overflow-y', 'auto');

    //Corrección para no generar scroll sobre el body cuando se cierra un modal y queda alguno abierto
    if ($(document).find('.fv-modal-stack').length > 0) $('body').addClass('modal-open');
});

$(document).on('shown.bs.modal', '.modal', function (event) {
    // keep track of the number of open modals

    if (typeof ($('body').data('fv_open_modals')) == 'undefined') {
        $('body').data('fv_open_modals', 0);
    }


    // if the z-index of this modal has been set, ignore.

    if ($(this).hasClass('fv-modal-stack')) {
        return;
    }

    $(this).addClass('fv-modal-stack');

    $('body').data('fv_open_modals', $('body').data('fv_open_modals') + 1);

    $(this).css('z-index', 1040 + (10 * $('body').data('fv_open_modals')));

    $('.modal-backdrop').not('.fv-modal-stack')
            .css('z-index', 1039 + (10 * $('body').data('fv_open_modals')));

    $('.modal-backdrop').not('fv-modal-stack')
            .addClass('fv-modal-stack');

});

$(function ($) {
    if ($.datepicker != undefined) {
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: 'Mes anterior',
            nextText: 'Mes siguiente',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
    }
});

//Validación de numericos en inputs
var validarTeclaNumerica = function (e, coma) {

    e = (e) ? e : window.event;
    var srcobj = e.target ? e.target : e.srcElement;
    
    var charCode = (e.which) ? e.which : e.keyCode;

    //Si se admite la coma, se inserta coma o punto y no existe en el input anteriormente se inserta una coma
    if (coma && (charCode == 44 || charCode == 46) && srcobj.value.match(/\,/g) == null) {
        srcobj.value = srcobj.value += ",";
        e.preventDefault();
        return true;
    }

    if (charCode > 31 && charCode != 37 && charCode != 39 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;

}
/***********************************************
Función: getFloat (devuelve un float)
Inputs: oValor --> String o Número
************************************************/
function getFloat(oValor) {
    try {
        var regPunto = new RegExp(/\./g);
        var regComa = new RegExp(/\,/g);
        if (typeof (oValor) == "number") {
            return parseFloat(oValor);
        } else {
            if (isNaN(oValor.replace(regPunto, "").replace(regComa, "."))
                || oValor.replace(regPunto, "").replace(regComa, ".") == ""
                ) return 0;
            else return parseFloat(oValor.replace(regPunto, "").replace(regComa, "."));
        }
    } catch (e) {
        alert("Error al obtener el número " + e.message);
    }
}
function getInt(oValor) {
    if (isNaN(oValor)) return 0;
    if (oValor == "") return 0;
    else return parseInt(oValor);
}

//Permitir solo dígitos
var soloDigitos = function (e) {
    var srcobj = e.target ? e.target : e.srcElement;
    e = (e) ? e : window.event;
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode < 48 || charCode > 57) {
        return false;
    }
    return true;
}

function crearExcelIAP(sHTML) {
    try {
        var strAction = document.forms[0].action;
        var strTarget = document.forms[0].target;

        if (document.getElementById('hdnInputExcel') == null) {
            var $oInputExcel = $("<textarea />").attr({ id: 'hdnInputExcel', name: 'ctl00$hdnInputExcel', rows: 1, cols: 1, style: 'display:none' });
            $('#frmDatos').append($oInputExcel);
        }

        $("#hdnInputExcel").val(sHTML);

        var patt = /{{septabla}}/g;
        if (sHTML.match(patt) == null || sHTML.match(patt).length == 0)
            document.forms[0].action = IB.vars["strserver"] + "Capa_Presentacion/Documentos/GenExcel1Hoja.aspx";
        else
            document.forms[0].action = IB.vars["strserver"] + "Capa_Presentacion/Documentos/getExcel.aspx";

        document.forms[0].target = "_blank";
        document.forms[0].submit();
        document.forms[0].action = strAction;
        document.forms[0].target = (strTarget == "") ? "_self" : strTarget;

        IB.procesando.ocultar();
    } catch (e) {
        IB.procesando.ocultar();
        IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al ir a crear el fichero Excel de servidor.");
    }
}

//Apertura de las guías en las pantallas de IAP 3.0
$(".guia").on("click", function () {
    obtenerDocumentoAyuda($(document).find("body").attr("data-codigopantalla"));
})

//Apertura por tecla F10
$("body").on("keydown", function (event) {
    if (event.keyCode == 121) {
        obtenerDocumentoAyuda($(document).find("body").attr("data-codigopantalla"));
    }
})


function obtenerDocumentoAyuda(pantalla) {
    switch (pantalla) {
        case "100":
            window.open(IB.vars.strserver + "Capa_Presentacion/Ayuda/Guia/bsPDF/ImputacionMasiva.pdf", '_blank');
            break;
        case "110":
            window.open(IB.vars.strserver + "Capa_Presentacion/Ayuda/Guia/bsPDF/ConsultaFacturabilidad.pdf", '_blank');
            break;
        case "120":
            window.open(IB.vars.strserver + "Capa_Presentacion/Ayuda/Guia/bsPDF/ImputacionCalendario.pdf", '_blank');
            break;
        case "130":
            window.open(IB.vars.strserver + "Capa_Presentacion/Ayuda/Guia/bsPDF/ConsultaImputaciones.pdf", '_blank');
            break;
        case "140":
            window.open(IB.vars.strserver + "Capa_Presentacion/Ayuda/Guia/bsPDF/Agenda.pdf", '_blank');
            break;
        default:
            IB.bsalert.toastinfo("Lo sentimos, la página actual no dispone de guía de ayuda.");

    }
}

var Base64 = {
    // private property
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

    // public method for encoding
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        input = Base64._utf8_encode(input);

        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
			this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
			this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

        }

        return output;
    },

    // public method for decoding
    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {

            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }

        }

        output = Base64._utf8_decode(output);

        return output;

    },

    // private method for UTF-8 encoding
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }

        }

        return string;
    }
}
//Codificar parámetro
function codpar(sCadena) {
    try {
        return Base64.encode(Utilidades.escape(sCadena));
    } catch (e) {
        alert("Error al codificar el parámetro", e.message);
    }
}
function Utilidades() { }
Utilidades.escape = function (sTexto) {
    try {
        return encodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"escape\" de un texto. " + e.message);
    }
}
Utilidades.unescape = function (sTexto) {
    try {
        return decodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"unescape\" de un texto. " + e.message);
    }
}

//Comprueba si la pantalla ya tiene incluida la referencia a un fichero js
function getScript(src) {
    if ($('script[src="' + src + '"]').length > 0) {
        return true;
    } else {
        return false;
    }
}
function cadenaAfecha(sCadena) {
    try {
        if (!sCadena || sCadena == "") return null;
        var aDatos = sCadena.split(" ");
        var aFecha = aDatos[0].split("/"); //Fecha

        if (aDatos[0].toString().length != 10) {
            mostrarErrorAplicacion("La longitud del dato de la fecha no es de diez dígitos (dd/mm/aaaa)", "");
            return false;
        }

        var aHora;
        if (aDatos.length > 1) {//Es que hay dato horario
            aHora = aDatos[1].split(":"); //Hora
            if (aHora[0]) aHora[0] = ((aHora[0].length == 1) ? "0" : "") + aHora[0];
            else aHora[0] = "00";
            if (aHora[1]) aHora[1] = ((aHora[1].length == 1) ? "0" : "") + aHora[1];
            else aHora[1] = "00";
            if (aHora[2]) aHora[2] = ((aHora[2].length == 1) ? "0" : "") + aHora[2];
            else aHora[2] = "00";

            return new Date(aFecha[2], eval(aFecha[1] - 1), aFecha[0], aHora[0], aHora[1], aHora[2]);
        } else {
            return new Date(aFecha[2], eval(aFecha[1] - 1), aFecha[0]);
        }

        //        var aFecha = sCadena.split("/");
        //        return new Date(aFecha[2],eval(aFecha[1]-1),aFecha[0]);
    } catch (e) {
        //mostrarErrorAplicacion("Error al convertir una cadena a fecha", e.message);
        alert("Error al convertir una cadena a fecha", e.message);
    }
}
function validarEmail(email) {
    var re = /^[^\s()<>@,;:\/]+@\w[\w\.-]+\.[a-z]{2,}$/i
    return re.test(email);
}

//Para manejar Querystrings. Inserta los elementos de la primera lista en la segunda siempre que no existan
function mergeParam(sListaOrigen, sListaDestino) {
    try {
        var aO = sListaOrigen.split("&");
        var aD = sListaDestino.split("&");
        var sListaParamDest = [];
        //Obtengo lista auxiliar con los nombres de los parámetros existentes en la lista destino
        for (var i = 0; i < aD.length; i++) {
            if (aD[i] != "") {
                var aElem = aD[i].split("=");
                if (aElem[0] != "")
                    sListaParamDest.push(aElem[0]);
            }
        }
        //Recorro la lista origen y para cada parametro que no esté en destino lo añado
        for (var i = 0; i < aO.length; i++) {
            if (aO[i] != "") {
                var aElem = aO[i].split("=");
                if (aElem[0] != "") {
                    index = sListaParamDest.indexOf(aElem[0]);
                    if (index < 0) {
                        aD.push(aO[i]);
                    }
                }
            }
        }
        return aD.join("&");
    } catch (e) {
        alert("Error al realizar el merge de parámetros. " + e.message);
    }
}
