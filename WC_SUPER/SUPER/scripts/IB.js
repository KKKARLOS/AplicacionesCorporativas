               
var IB = (function () {

    // **** variables globales ********************************************************
    var _vars = [];
    // **** variables globales ********************************************************

    // **** Procesando ********************************************************
    var procesando = (function () {

        var _n = null;
        var _m = null;
        var _showStartTime = null;
        var _hidePromise = null;


        var opts = {
            delay: 1,
            hide: 1,
            spinner: true
        }

        var opciones = function (opciones) {

            opts.delay = opciones.delay || opts.delay;
            opts.hide = opciones.hide || opts.hide;
            opts.spinner = opciones.spinner || opts.spinner; //mostrar spinner true|false
        }


        var scrollProcesando = function () {
            var d = $("#divlock_procesando");
            if (d.css("display") == "block") {
                var span = d.find("span:first");
                var h = $(document).scrollTop() + ($(window).height() / 2);
                h = h - (72 / 2);

                $(span).css("top", h);
            }
        }

        var bloquear = function () {

            var $document = $(document);
            var $divlock = $("#divlock");

            if ($divlock.length == 0) {
                $("body").append("<div id='divlock' style='position:absolute; opacity: 0; z-index: 1099; top:0px; display:none'></div>");
                $divlock = $("#divlock");
            }

            $divlock.css("height", $document.height());
            $divlock.css("width", $document.width() - 20);

            $divlock.css("display", "block");
        }

        var desbloquear = function () {

            var $divlock = $("#divlock");
            $divlock.css("display", "none");
        }

        var mostrarProcesando = function () {

            var $window = $(window);
            var $document = $(document);

            var h = $document.scrollTop() + ($window.height() / 2);
            var w = $document.width() / 2;

            //tamaño de la imagen = 72*72
            h = h - (72 / 2);
            w = w - (72 / 2);

            var $d = $("#divlock_procesando");
            if ($d.length == 0) {
                $("body").append("<style>.fondogris {" +
                                 "background: url('" + IB.vars.strserver + "Capa_Presentacion/bsImages/ui-bg_flat_0_aaaaaa_40x100.png') 50% 50% repeat-x;}" +
                                 "</style>" +

                                 "<div id='divlock_procesando' style='position:absolute; z-index: 1100; top:0px; display:none'>" +
                                 "<div class='sk-fading-circle'><div class='sk-circle1 sk-circle'></div><div class='sk-circle2 sk-circle'></div><div class='sk-circle3 sk-circle'></div><div class='sk-circle4 sk-circle'></div><div class='sk-circle5 sk-circle'></div><div class='sk-circle6 sk-circle'></div><div class='sk-circle7 sk-circle'></div><div class='sk-circle8 sk-circle'></div><div class='sk-circle9 sk-circle'></div><div class='sk-circle10 sk-circle'></div><div class='sk-circle11 sk-circle'></div><div class='sk-circle12 sk-circle'></div></div>" +
                                 "</div>");

                $d = $("#divlock_procesando");
            }

            if (opts.spinner)
                $d.find(".spinner").css("display", "block");
            else
                $d.find(".spinner").css("display", "none");

            $d.css("height", $document.height());
            $d.css("width", $document.width());
            $d.css("display", "block");
            $d.find("span").css("top", h).css("left", w);

            $window.off("scroll", scrollProcesando).on("scroll", scrollProcesando);

            _showStartTime = new Date().getTime();
            _n = null;
        }

        var ocultarProcesando = function () {

            $("#divlock_procesando").css("display", "none");
            $(window).off("scroll", scrollProcesando);

            _n = null;
            _m = null;

            _hidePromise.resolve();

        }

        var mostrar = function () {

            bloquear();

            var $d = $("#divlock_procesando");

            //Si esta oculto y no hay timer de mostrado --> lanzar timer de mostrado
            if (($d.length == 0 || $d.css("display") == "none") && _n == null) {
                _n = window.setTimeout(mostrarProcesando, opts.delay);
            }

            //Cancelar timer de ocultado
            if (_m != null) {
                window.clearTimeout(_m);
                _m = null;
            }

        }

        var ocultar = function () {

            desbloquear();

            //Si hay timer para ocultar --> salir
            if (_m != null) return _hidePromise;

            var $d = $("#divlock_procesando");

            //Si está pdte de mostrar --> cancelar timer de mostrado
            if (($d.length == 0 || $d.css("display") == "none") && _n != null) {
                window.clearTimeout(_n);
                _n = null;
                return null;
            }

            var t = new Date().getTime();
            var delay = 0;

            //El tiempo minimo que debe estar visible el spinner es opts.hide
            if (t - _showStartTime < opts.hide) {
                delay = opts.hide - (t - _showStartTime); //retardo = tiempo que debe estar visible - lo que ya lleva visible
            }

            _m = window.setTimeout(ocultarProcesando, delay);

            _hidePromise = $.Deferred();

            return _hidePromise.promise();

        }


        return {
            mostrar: mostrar,
            ocultar: ocultar,
            opciones: opciones
        }

    })();
    // **** Procesando ********************************************************

    // **** bsalert ********************************************************
    var bsalert = (function () {

        //tipo = danger, info, warning
        var fixedAlert = function (tipo, strTitulo, strCuerpo) {

            var dfr = $.Deferred();

            var dlg = $("#dlgFixedAlert");
            if ($(dlg).length == 0) {

                var a = "<div class='modal fade' id='dlgFixedAlert'>";
                a += "<div class='modal-dialog'>";
                a += "<div class='modal-content'>";

                a += "<div class='modal-header'>";
                a += "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>";
                a += "<h4 class='modal-title'></h4>";
                a += "</div>";
                a += "<div class='modal-body' style='min-height:100px'></div>";

                a += "<div class='modal-footer clear'><button type='button' class='btn btn-default' data-dismiss='modal'>Cerrar</button></div>";

                a += "</div></div></div>";

                $("body").append(a);
                dlg = $("#dlgFixedAlert");
            }

            $(dlg).on("hidden.bs.modal", function () { dfr.resolve(); });

            $(dlg).find(".modal-header").removeClass("btn-danger btn-info btn-warning").addClass("btn-" + tipo);
            $(dlg).find(".modal-title").html(strTitulo);
            $(dlg).find(".modal-body").html(strCuerpo);
            $(dlg).modal({ keyboard: false, backdrop: "static", dismiss: "modal" }, "show");

            return dfr.promise();

        }

        //warning, success, info, danger 
        var toast = function (msg, type, fadin, delay, fadout) {

            if (!type) type = "success";
            if (!fadin) fadin = 300;//Por defecto
            if (!delay) delay = 2000;//Por defecto
            if (!fadout) fadout = 500;//Por defecto

            var divAlert = $('#dlgToast');
            if (divAlert.length > 0) {
                divAlert.finish();//Para terminar las animaciones lanzadas anteriormente
                divAlert.removeClass().addClass('bsToast col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-' + type);
                divAlert.text("");
                divAlert.children().not(".close").remove();
                divAlert.append(msg);
                divAlert.fadeIn(fadin).delay(delay).fadeOut(fadout);
            } else {
                var style = $("<style>.bsToast { position: fixed; top: 45%; text-align: center; z-index: 9999;} .bsToast .close {margin-left:10px;}</style>");
                style.appendTo($('body'));

                var content = $('<div id="dlgToast" class="bsToast col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-' + type + '" style="display: none;">');
                var close = $('<button type="button" class="close" data-dismiss="alert">&times</button>');
                content.append(close);
                content.append(msg);
                content.appendTo($('body')).fadeIn(fadin).delay(delay).fadeOut(fadout);
            }
        }

        var toastinfo = function (msg) {
            this.toast(msg, "info")
        }

        var toastwarning = function (msg) {
            this.toast(msg, "warning")
        }

        var toastdanger = function (msg) {
            this.toast(msg, "danger")
        }

        //warning, success, info, danger (default es warning)
        var fixedToast = function (msg, type, fadin) {

            if (!type) type = "warning";
            if (!fadin) fadin = 300;//Por defecto

            var divAlert = $('#dlgToast');
            if (divAlert.length > 0) {
                divAlert.finish();//Para terminar las animaciones lanzadas anteriormente
                divAlert.removeClass().addClass('bsToast col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-' + type);
                divAlert.contents().filter(function () { return this.nodeType != 1; }).remove();
                divAlert.append(msg);
                divAlert.fadeIn(fadin);
            } else {
                var style = $("<style>.bsToast { position: fixed; top: 45%; text-align: center; z-index: 9999;} .bsToast .close {margin-left:10px;}</style>");
                style.appendTo($('body'));

                var content = $('<div id="dlgToast" class="bsToast col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-' + type + '" style="display: none;">');
                var close = $('<button type="button" class="close" data-dismiss="alert">&times</button>');
                content.append(close);
                content.append(msg);
                content.appendTo($('body')).fadeIn(fadin);
            }
        }

        return {
            fixedAlert: fixedAlert,
            toast: toast,
            toastinfo: toastinfo,
            toastdanger: toastdanger,
            toastwarning: toastwarning,
            fixedToast: fixedToast
        }
    })();
    // **** bsalert ********************************************************

    // **** bserror ********************************************************
    var bserror = (function (bsAlert) {

        var error$ajax = function (ex, status) {

            IB.procesando.ocultar();

            if (status == "timeout") {
                mostrarAvisoTimeout();
                return;
            }

            var msg = "Se ha producido un error al realizar la operación.";
            var responseText;

            if (typeof ex.responseText !== "undefined") {
                try {
                    responseText = $.parseJSON(ex.responseText)

                    if (typeof responseText.Message !== "undefined") {
                        msg = responseText.Message;
                        if (typeof responseText.ExceptionMessage !== "undefined")
                            msg += "<br />" + responseText.ExceptionMessage;
                        if (typeof responseText.MessageDetail !== "undefined")
                            msg += "<br />" + responseText.MessageDetail

                    }

                    if (typeof responseText.ExceptionType !== "undefined" && responseText.ExceptionType.indexOf("ValidationException") != -1) {

                        mostrarAvisoAplicacion("Validación de datos.", msg);
                        return;
                    }
                }
                catch (err) { }
            }
            mostrarErrorAplicacion("Error de aplicación.", msg);
        }

        var mostrarAvisoTimeout = function () {

            IB.procesando.ocultar();

            strTitulo = "Timeout";
            strError = "Se ha sobrepasado el tiempo límite de espera para procesar la petición en servidor.";

            strError += "<div class='text-left' style='margin-top:40px;font-size:12px'>Inténtalo de nuevos en unos momentos y si persiste el problema ponte en contacto con el CAU.<br>Disculpa las molestias.</div>";

            bsAlert.fixedAlert("warning", strTitulo, strError)

        }

        var mostrarErrorAplicacion = function (strTitulo, strError) {

            IB.procesando.ocultar();

            strTitulo = decodeURIComponent(strTitulo);
            strError = decodeURIComponent(strError);

            strError += "<div class='text-left' style='margin-top:40px;font-size:12px'>Ponte en contacto con el CAU.<br>Disculpa las molestias.</div>";

            bsAlert.fixedAlert("danger", strTitulo, strError)

        }

        var mostrarAvisoAplicacion = function (strTitulo, strAviso) {

            IB.procesando.ocultar();

            strTitulo = decodeURIComponent(strTitulo);
            strAviso = decodeURIComponent(strAviso);

            bsAlert.fixedAlert("warning", strTitulo, strAviso)

        }


        return {
            error$ajax: error$ajax,
            mostrarErrorAplicacion: mostrarErrorAplicacion,
            mostrarAvisoAplicacion: mostrarAvisoAplicacion
        }

    })(bsalert);
    // **** bserror ********************************************************

    var bsconfirm = (function () {

        var confirm = function (tipo, strTitulo, strCuerpo) {

            var dfr = $.Deferred();

            var dlg = $("#bsconfirm");
            if ($(dlg).length == 0) {

                var a = "<div class='modal fade' id='bsconfirm'>";
                a += "<div class='modal-dialog'>";
                a += "<div class='modal-content'>";

                a += "<div class='modal-header btn-'" + tipo + ">";
                a += "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>";
                a += "<h4 class='modal-title'></h4>";
                a += "</div>";
                a += "<div class='modal-body' style='min-height:100px'></div>";

                a += "<div class='modal-footer clear'>";
                a += "<button type='button' class='btn btn-primary fk_btnsi' data-dismiss='modal'>Sí</button>";
                a += "<button type='button' class='btn btn-default fk_btnno' data-dismiss='modal'>No</button>";
                a += "</div>";

                a += "</div></div></div>";

                $("body").append(a);
                dlg = $("#bsconfirm");
            }

            $(dlg).find(".fk_btnno").off("click").on("click", function () { dfr.reject(); });
            $(dlg).find(".close").off("click").on("click", function () { dfr.reject(); });
            $(dlg).find(".fk_btnsi").off("click").on("click", function () { dfr.resolve(); });

            $(dlg).find(".modal-header").removeClass("btn-danger btn-info btn-warning").addClass("btn-" + tipo);
            $(dlg).find(".modal-title").html(strTitulo);
            $(dlg).find(".modal-body").html(strCuerpo);
            $(dlg).modal({ keyboard: false, backdrop: "static", dismiss: "modal" }, "show");


            return dfr.promise();
        }

        var confirmCambios = function () {
            return this.confirm("warning", "Cambios sin guardar", "Hay cambios sin guardar. Si continúas perderás dichos cambios.<br/><br/>¿Quieres continuar?");
        }


        return {
            confirm: confirm,
            confirmCambios: confirmCambios
        }
    })();


    var sesion = (function () {

        var _sessionMinutesTimeout; //constante --> timeout de servidor
        var minutosRestantes;
        var segundosRestantes;

        var m = null;
        var s = null;

        var init = function (t) {

            if (m != null) window.clearInterval(m);
            if (s != null) window.clearInterval(s);

            m = null;
            s = null;

            _sessionMinutesTimeout = t;
            minutosRestantes = _sessionMinutesTimeout

            m = window.setInterval(restaMinutos, 60000)

            pintaMensaje();

            $(document).off("ajaxSend").on("ajaxSend", function () {
                init(_sessionMinutesTimeout);
            });
        }

        var restaMinutos = function () {

            minutosRestantes--;
            pintaMensaje();

            if (minutosRestantes == 5) { //mostrar modal de restaurar tiempo de sesion

                var msg = "La sesión de SUPER va a caducar en breve.<br /><br />" +
                          "¿Desea reiniciar el tiempo de la sesión?<br />" +
                          "Si pulsas \"No\" podrías perder los datos pendientes de grabar.<br /><br />";

                $.when(IB.bsconfirm.confirm("primary", "Control de caducidad de sesión", msg))
                        .then(function () {
                            IB.DAL.post(IB.vars.strserver + "Services/Session.asmx", "Resetear", null, null,
                                function () {
                                    init(_sessionMinutesTimeout);
                                });
                        });
            }
            else if (minutosRestantes == 1) { //pintar segundos

                window.clearInterval(m);
                m = null;

                segundosRestantes = 59;
                s = window.setInterval(restaSegundos, 1000)
            }
        }

        var restaSegundos = function () {

            segundosRestantes--;
            pintaMensaje();

            if (segundosRestantes <= 0) { // se acabo el tiempo, redirigir a timeout
                window.clearInterval(s);
                s = null;
                window.location.href = IB.vars.strserver + "bsSesionCaducada.aspx";
            }

        }

        var pintaMensaje = function () {

            var $el = $("#lblSession");
            if (m != null) $el.html("La sesión caducará en " + minutosRestantes + " min.");
            if (s != null) $el.html("La sesión caducará en " + segundosRestantes + " seg.");
        }

        return {
            init: init
        }

    })();


    var base64 = (function () {

        var _PADCHAR = "=",
            _ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"

        function _getbyte64(s, i) {
            // This is oddly fast, except on Chrome/V8.
            // Minimal or no improvement in performance by using a
            // object with properties mapping chars to value (eg. 'A': 0)

            var idx = _ALPHA.indexOf(s.charAt(i));

            if (idx === -1) {
                throw "Cannot decode base64";
            }

            return idx;
        }

        function _decode(s) {
            var pads = 0,
              i,
              b10,
              imax = s.length,
              x = [];

            s = String(s);

            if (imax === 0) {
                return s;
            }

            if (imax % 4 !== 0) {
                throw "Cannot decode base64";
            }

            if (s.charAt(imax - 1) === _PADCHAR) {
                pads = 1;

                if (s.charAt(imax - 2) === _PADCHAR) {
                    pads = 2;
                }

                // either way, we want to ignore this last block
                imax -= 4;
            }

            for (i = 0; i < imax; i += 4) {
               b10 = (_getbyte64(s, i) << 18) | (_getbyte64(s, i + 1) << 12) | (_getbyte64(s, i + 2) << 6) | _getbyte64(s, i + 3);
                x.push(String.fromCharCode(b10 >> 16, (b10 >> 8) & 0xff, b10 & 0xff));
            }

            switch (pads) {
                case 1:
                    b10 = (_getbyte64(s, i) << 18) | (_getbyte64(s, i + 1) << 12) | (_getbyte64(s, i + 2) << 6);
                    x.push(String.fromCharCode(b10 >> 16, (b10 >> 8) & 0xff));
                    break;

                case 2:
                    b10 = (_getbyte64(s, i) << 18) | (_getbyte64(s, i + 1) << 12);
                    x.push(String.fromCharCode(b10 >> 16));
                    break;
            }

            return x.join("");
        }

        function _getbyte(s, i) {
            var x = s.charCodeAt(i);

            if (x > 255) {
                throw "INVALID_CHARACTER_ERR: DOM Exception 5";
            }

            return x;
        }

        function _encode(s) {
            if (arguments.length !== 1) {
                throw "SyntaxError: exactly one argument required";
            }

            s = String(s);

            var i,
              b10,
              x = [],
              imax = s.length - s.length % 3;

            if (s.length === 0) {
                return s;
            }

            for (i = 0; i < imax; i += 3) {
                b10 = (_getbyte(s, i) << 16) | (_getbyte(s, i + 1) << 8) | _getbyte(s, i + 2);
                x.push(_ALPHA.charAt(b10 >> 18));
                x.push(_ALPHA.charAt((b10 >> 12) & 0x3F));
                x.push(_ALPHA.charAt((b10 >> 6) & 0x3f));
                x.push(_ALPHA.charAt(b10 & 0x3f));
            }

            switch (s.length - imax) {
                case 1:
                    b10 = _getbyte(s, i) << 16;
                    x.push(_ALPHA.charAt(b10 >> 18) + _ALPHA.charAt((b10 >> 12) & 0x3F) + _PADCHAR + _PADCHAR);
                    break;

                case 2:
                    b10 = (_getbyte(s, i) << 16) | (_getbyte(s, i + 1) << 8);
                    x.push(_ALPHA.charAt(b10 >> 18) + _ALPHA.charAt((b10 >> 12) & 0x3F) + _ALPHA.charAt((b10 >> 6) & 0x3f) + _PADCHAR);
                    break;
            }

            return x.join("");
        }

        return {
            decode: _decode,
            encode: _encode
        };

    }());


    var uri = (function () {

        function _encode(s) {

            return IB.base64.encode(encodeURIComponent(s));
        }

        function _decode(s) {
            return decodeURIComponent(IB.base64.decode(s));
        }

        return {
            decode: _decode,
            encode: _encode
        };
    }());

    var logerror = (function () {

        $(window).on("error", log);

        function log(e) {

            try {
                var payload = {
                    file: e.originalEvent.filename,
                    line: e.originalEvent.lineno,
                    msg: e.originalEvent.message
                }

                IB.DAL.post(IB.vars.strserver + "Services/LogJS.asmx", "Log", payload, null, null, function () { })
            }
            catch (e) { }
        }


        return {
            log: log
        }

    })();


    var createGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }


    return {
        vars: _vars,
        procesando: procesando,
        bserror: bserror,
        bsalert: bsalert,
        bsconfirm: bsconfirm,
        sesion: sesion,
        base64: base64,
        uri: uri,
        createGuid: createGuid
    };

})();


