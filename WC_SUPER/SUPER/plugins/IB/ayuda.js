
(function ($) {

    var pluginName = 'ayuda';

    function Plugin(element, _options) {

        var el = element;
        var $el = $(element);

        _options = $.extend({}, $.fn[pluginName].defaults, _options);

        function init() {

            initModal();
            if (_options.autoShow) showModal();

            hook('onInit');
        }

        function initModal() {

            $el.append("<div class='modal fade' id='" + pluginName + "-modal'>" +
                        "<div class='modal-dialog' role='dialog'>" +
                            "<div class='modal-content'>" +

                                "<div class='modal-header btn-primary'>" +
                                    "<h4 class='modal-title'>" + _options.titulo + "</h4>" +
                                "</div>" +

                                "<div class='modal-body'>" +
                                    "<div class='row fk_filters'>" +
                                        "<div class='col-xs-12'>" +
                                            "<input id='" + pluginName + "-txtFiltro' type='text' class='col-xs-12' placeholder='" + _options.placeholderText + "' />" +
                                        "</div>" +
                                    "</div>" +

                                    "<br />" +

                                    "<div class='row'>" +
                                        "<div class='col-xs-12'>" +
                                            "<ul class='list-group table-hover' id='" + pluginName + "-listcontent'>" +
                                            "</ul>" +
                                        "</div>" +
                                    "</div>" +
                                "</div>" +

                                "<div class='modal-footer'>" +
                                    "<b>" +
                                        "<button type='button' id='" + pluginName + "-btnSeleccionar' class='btn btn-primary'>Seleccionar</button></b>" +
                                    "<b>" +
                                        "<button type='button' id='" + pluginName + "-btnCancelar' class='btn btn-default'>Cancelar</button></b>" +
                                "</div>" +
                            "</div>" +
                       "</div>" +
                  "</div>");


            //Elemento activo de la lista
            $el.on("click", ".list-group-item", function (e) {
                $el.find(".list-group-item").removeClass('active');
                $(this).addClass('active');
            });

            //Botón aceptar de la ventana
            $el.find("#" + pluginName + "-btnSeleccionar").on('click', function (e) {

                var $selected = $el.find("#" + pluginName + "-listcontent").find("li.active");

                if ($selected.length == 0) {
                    IB.bsalert.toastwarning("No has seleccionado ningún item de la lista");
                    return false;
                }

                var data = {
                    key: $selected.attr("data-key"),
                    value: $selected.text()
                }
                hook("onSeleccionar", data);

                hideModal();
            });

            //Botón cancelar de la ventana
            $el.find("#" + pluginName + "-btnCancelar").on('click', function () {
                hideModal();
                hook("onCancelar");
            });

            var sto = null;

            //Buscar al ir escribiendo en el filtro
            $el.find(".fk_filters input").on('keyup', function (e) {
                $el.find("#" + pluginName + "-listcontent").empty(); //Vaciar el content al escribir algo en el filtro.

                if (sto != null) window.clearTimeout(sto);
                sto = window.setTimeout(search, 200);
            });



        }

        function showModal() {

            $el.find(".modal-title").html(_options.titulo)

            $el.find('.ocultable').attr('aria-hidden', 'true');
            $el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $el.find(".modal").modal('show');

            //ocultar los campos de criterio de filtrado
            if (_options.autoSearch) {
                $el.find(".fk_filters").addClass("hide");
                search();
            }
            else { //dar el foco al primer campo del filtrado
                $el.find("#" + pluginName + "-txtFiltro").attr("placeholder", _options.placeholderText);
                $el.find(".fk_filters").removeClass("hide");
                $el.find("#" + pluginName + "-modal").on('shown.bs.modal', function () {
                    $el.find("#" + pluginName + "-txtFiltro").focus()
                })
            }

        }

        function hideModal() {

            $el.find(".modal").modal('hide');
            $el.find("input[type='text']").val("");
            $el.find("#" + pluginName + "-listcontent").empty();

        }

        function search() {


            $el.find("#" + pluginName + "-listcontent").empty();

            var html = "";

            var filtro = _options.autoSearch ? _options.filtro : $el.find("#" + pluginName + "-txtFiltro").val();
            if (filtro.length == 0) return;

            $el.find("#" + pluginName + "-listcontent").html("Buscando ...");
            //var n = window.setTimeout(function () { $el.find("#" +pluginName + "-listcontent").html("Buscando ...") }, 500)

            var payload = {
                tipo: _options.tipoAyuda,
                filtro: filtro,
                admin: _options.admin
            }

            console.log("Buscando: " + payload.filtro);

            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + _options.modulo + "/Services/Ayudas.asmx";

            IB.DAL.post(serviceUrl, "Buscar", payload, null,
                function (data) {
                    data.forEach(function (item) {
                        html += "<li class='list-group-item' data-key='" + item.Key + "'>" + item.Value + "</li>"
                    });
                    if (html.length == 0) html += _options.notFound;

                    $el.find("#" + pluginName + "-listcontent").html(html);
                    //window.clearTimeout(n);
                }
            );
        }

        function option(key, val) {

            if (typeof options[key] === "object") {
                if (val === undefined) {
                    return options[key];
                }
                else if (Array.isArray(options[key])) {
                    options[key] = val;
                }
                else {
                    $.extend(options[key], val);
                }
            }
            else {
                if (val === undefined) {
                    return options[key];
                }
                else {
                    options[key] = val;
                }
            }
        }

        function options(opts) {
            $.extend(_options, opts);
        }

        function destroy() {

            $el.each(function () {
                var el = this;
                var $el = $(this);

                hook('onDestroy');

                $el.removeData('plugin_' + pluginName);

                $el.off("click", ".list-group-item");
                $el.find("#" + pluginName + "-btnSeleccionar").off('click');
                $el.find("#" + pluginName + "-btnCancelar").off('click');
                $el.find(".fk_filters input").off('keyup');
                $el.html("");

            });
        }

        function hook(hookName, data) {
            if (_options[hookName] !== undefined) {
                _options[hookName].call(el, data);
            }
        }

        init();

        return {
            option: option,
            options: options,
            destroy: destroy,
            show: showModal,
            hide: hideModal
        };
    }

    $.fn[pluginName] = function (options) {

        if (typeof arguments[0] === 'string') {
            var methodName = arguments[0];
            var args = Array.prototype.slice.call(arguments, 1);
            var returnVal;
            this.each(function () {
                // Check that the element has a plugin instance, and that
                // the requested public method exists.
                if ($.data(this, 'plugin_' + pluginName) && typeof $.data(this, 'plugin_' + pluginName)[methodName] === 'function') {
                    // Call the method of the Plugin instance, and Pass it
                    // the supplied arguments.
                    returnVal = $.data(this, 'plugin_' + pluginName)[methodName].apply(this, args);
                } else {
                    throw new Error('Method ' + methodName + ' does not exist on jQuery.' + pluginName);
                }
            });
            if (returnVal !== undefined) {
                // If the method returned a value, return the value.
                return returnVal;
            } else {
                // Otherwise, returning 'this' preserves chainability.
                return this;
            }
            // If the first parameter is an object (options), or was omitted,
            // instantiate a new instance of the plugin.
        } else if (typeof options === "object" || !options) {
            return this.each(function () {
                // Only allow the plugin to be instantiated once.
                if (!$.data(this, 'plugin_' + pluginName)) {
                    // Pass options to Plugin constructor, and store Plugin
                    // instance in the elements jQuery data object.
                    $.data(this, 'plugin_' + pluginName, new Plugin(this, options));
                }
            });
        }
    };

    $.fn[pluginName].defaults = {
        titulo: "Ayuda",
        modulo: "SIC",
        tipoAyuda: "",
        autoSearch: true,
        autoShow: false,
        filtro: "",
        admin: false,
        placeholderText: "",
        notFound: "No se han encontrado resultados.",
        onSeleccionar: function () { },
        onCancelar: function () { },
        onInit: function () { },
        onDestroy: function () { }
    };

})(jQuery);