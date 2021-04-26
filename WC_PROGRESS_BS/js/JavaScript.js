//Provisional
function mostrarErrorAplicacion(msg,msg2)
{
    alertNew("danger", msg + "<br />" + msg2);
}

//warning, success, info, danger (Pasar a genérico)
function alertNew(type, msg, fadin, delay, fadout) {
    if (!fadin) fadin = 300;//Por defecto
    if (!delay) delay = 2000;//Por defecto
    if (!fadout) fadout = 500;//Por defecto
    var divAlert = $('div.bsAlert');
    if (divAlert.length > 0) {
        divAlert.finish();//Para terminar las animaciones lanzadas anteriormente
        divAlert.removeClass().addClass('bsAlert col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-' + type);
        divAlert.contents().filter(function () { return this.nodeType != 1; }).remove();
        divAlert.append(msg);
        divAlert.fadeIn(fadin).delay(delay).fadeOut(fadout);
    } else {
        var message = $('<div class="bsAlert col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-' + type + '" style="display: none;">');
        var close = $('<button type="button" class="close" data-dismiss="alert">&times</button>');
        message.append(close); // adding the close button to the message
        message.append(msg); // adding the error response to the message
        message.appendTo($('body')).fadeIn(fadin).delay(delay).fadeOut(fadout);
    }
}

function comprobarerrores() {
    if (msgerr.length > 0) {
        alertNew("danger", msgerr);
        return;
    }
}

//Ordenar las listas (Pasar a genérico)
function ordenar(objsLis) {
    try {
        objsLis.sort(function (a, b) {
            var A = $(a).text().toUpperCase();
            var B = $(b).text().toUpperCase();
            return (A < B) ? -1 : (A > B) ? 1 : 0;
        });
        objsLis.parent().html(objsLis);
    } catch (e) {
        alertNew("danger", "Error al intentar ordenar la lista.");
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
        mostrarErrorAplicacion("Error al codificar el parámetro", e.message);
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
HTMLElement.prototype.__defineGetter__("innerText", function () {
    return (this.textContent);
});
HTMLElement.prototype.__defineSetter__("innerText", function (txt) {
    this.textContent = txt;
});

function StringBuilder() {
    this.buffer = [];
}
StringBuilder.prototype.Append = function (str) {
    this.buffer[this.buffer.length] = str;
}
StringBuilder.prototype.ToString = function () {
    return this.buffer.join('');
}


//normalizamos la búsqueda (sin tildes ni acentos)
var normalize = (function () {
    var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
        to = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuunncc",
        mapping = {};

    for (var i = 0, j = from.length; i < j; i++)
        mapping[from.charAt(i)] = to.charAt(i);

    return function (str) {
        var ret = [];
        for (var i = 0, j = str.length; i < j; i++) {
            var c = str.charAt(i);
            if (mapping.hasOwnProperty(str.charAt(i)))
                ret.push(mapping[c]);
            else
                ret.push(c);
        }
        return ret.join('');
    }

})();


//Detecta versión IOS
function iOSversion() {
    if (/iP(hone|od|ad)/.test(navigator.platform)) {
        var v = (navigator.appVersion).match(/OS (\d+)_(\d+)_?(\d+)?/);
        return [parseInt(v[1], 10), parseInt(v[2], 10), parseInt(v[3] || 0, 10)];
    }
}


function obtenerDocumentoAyuda(pantalla) {
    switch (pantalla) {
        case "100":            
            window.open(strServer + "Docs/Ayudas/Evaluaciones/MisEvaluaciones.pdf", '_blank');
            break;

        case "101":
            window.open(strServer + "Docs/Ayudas/Evaluaciones/EvaluacionesDeMiEquipo.pdf", '_blank');
            break;

        case "102":
            window.open(strServer + "Docs/Ayudas/Evaluaciones/AbirNuevasEvaluaciones.pdf", '_blank');
            break;

        case "103":
            window.open(strServer + "Docs/Ayudas/Evaluaciones/CompletarEvaluacionesAbiertas.pdf", '_blank');
            break;

        case "104EVA":
            window.open(strServer + "Docs/Ayudas/Evaluaciones/Estadisticas.pdf", '_blank');
            break;

        case "104ADM":
            window.open(strServer + "Docs/Ayudas/Administracion/EstadisticasPorEvaluador.pdf", '_blank');
            break;



        case "200":
            window.open(strServer + "Docs/Ayudas/Equipo/ConfirmarMiEquipo.pdf", '_blank');
            break;

        case "201":
            window.open(strServer + "Docs/Ayudas/Equipo/TramitarSalidasDeMiEquipo.pdf", '_blank');
            break;

        case "202":
            window.open(strServer + "Docs/Ayudas/Equipo/GestionarEntradasAMiEquipo.pdf", '_blank');
            break;

        case "203EVA":
            window.open(strServer + "Docs/Ayudas/Equipo/ArbolDeDependencias.pdf", '_blank');
            break;

        case "203ADM":
            window.open(strServer + "Docs/Ayudas/Administracion/ArbolDeDependencias-adm.pdf", '_blank');
            break;




        case "300":
            window.open(strServer + "Docs/Ayudas/Roles/SolicitarCambioDeRol.pdf", '_blank');
            break;

        case "301":
            window.open(strServer + "Docs/Ayudas/Roles/GestionarSolicitudesDeCambioDeRol.pdf", '_blank');
            break;

        case "302EVA":
            window.open(strServer + "Docs/Ayudas/Roles/ProfesionalesPorRol.pdf", '_blank');
            break;

        case "302ADM":
            window.open(strServer + "Docs/Ayudas/Administracion/ProfesionalesPorRol-adm.pdf", '_blank');
            break;

        
        case "400":
            window.open(strServer + "Docs/Ayudas/Administracion/CambiarEvaluador.pdf", '_blank');
            break;

        case "401":
            window.open(strServer + "Docs/Ayudas/Administracion/BuscarEvaluaciones.pdf", '_blank');
            break;

        case "402":
            window.open(strServer + "Docs/Ayudas/Administracion/EstadisticasGlobales.pdf", '_blank');
            break;


        case "404":
            window.open(strServer + "Docs/Ayudas/Administracion/HistoricoDeRoles.pdf", '_blank');
            break;

      
        case "406":
            window.open(strServer + "Docs/Ayudas/Administracion/FormacionDemandada.pdf", '_blank');
            break;

        case "407":
            window.open(strServer + "Docs/Ayudas/Administracion/Perfiles.pdf", '_blank');
            break;

        case "408":
            window.open(strServer + "Docs/Ayudas/Administracion/ComunidadProgress.pdf", '_blank');
            break;

        case "409":
            window.open(strServer + "Docs/Ayudas/Administracion/RolesAprobadores.pdf", '_blank');
            break;

        case "411":
            window.open(strServer + "Docs/Ayudas/Administracion/AntiguedadDeReferencia.pdf", '_blank');
            break;


        case "412":
            window.open(strServer + "Docs/Ayudas/Administracion/ColectivoForzado.pdf", '_blank');
            break;


        case "413":
            window.open(strServer + "Docs/Ayudas/Administracion/CategoriaProfesional-colectivo.pdf", '_blank');
            break;

        case "414":
            window.open(strServer + "Docs/Ayudas/Administracion/Colectivo-modeloDeFormulario.pdf", '_blank');
            break;

        case "415":
            window.open(strServer + "Docs/Ayudas/Administracion/BulaDeAvisos.pdf", '_blank');
            break;


        case "416":
            window.open(strServer + "Docs/Ayudas/Administracion/VisualizacionForzadaDirecta.pdf", '_blank');
            break;

        case "417":
            window.open(strServer + "Docs/Ayudas/Administracion/VisualizacionForzadaAgregada.pdf", '_blank');
            break;

        case "418":
            window.open(strServer + "Docs/Ayudas/Administracion/TemporadaProgress.pdf", '_blank');
            break;



        default:            
            alertNew("info", "Lo sentimos, la página actual no dispone de guía de ayuda.");

    }
}

