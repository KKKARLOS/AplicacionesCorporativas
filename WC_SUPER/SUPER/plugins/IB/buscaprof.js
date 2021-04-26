
(function ($) {

    var pluginName = 'buscaprof';

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
                                        "<div class='col-xs-3'>" +
                                            "<label for='" + pluginName + "-txtApellido1'>Apellido 1º</label>" +
                                            "<input id='" + pluginName + "-txtApellido1' type='text' />" +
                                        "</div>" +
                                        "<div class='col-xs-3' style='margin-left: 8px'>" +
                                            "<label for='" + pluginName + "-txtApellido2'>Apellido 2º</label>" +
                                            "<input id='" + pluginName + "-txtApellido2' type='text' />" +
                                        "</div>" +
                                        "<div class='col-xs-3' style='margin-left: 8px'>" +
                                            "<label for='" + pluginName + "-txtNombre'>Nombre</label>" +
                                            "<input id='" + pluginName + "-txtNombre' type='text' />" +
                                        "</div>" +
                                        "<div class='col-xs-1 col-xs-offset-1'>" +
                                            "<button type='button' style='margin-top: 22px' id='" + pluginName + "-btnObtener' class='btn btn-primary btn-xs'>Obtener</button>" +
                                        "</div>" +
                                    "</div>" +

                                    "<br />" +


                                    "<div class='well hide' id='" + pluginName + "-searchInListContainer'>" +
                                             "<div class='row'>" +
                                                 "<div class='col-xs-12 input-group'>" +
                                                     "<input type='text' id='" + pluginName + "-txtSearchInList' name='SearchDualList' class='form-control' placeholder='Buscar en el contenedor' />" +
                                                 "</div>" +
                                             "</div>" +
                                    "</div>" +



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
                    IB.bsalert.toastwarning("No has seleccionado ningún profesional");
                    return false;
                }

                var data = {
                    idficepi: $selected.attr("data-idficepi"),
                    profesional: $selected.html()
                }
                hook("onSeleccionar", data);

                hideModal();
            });

            //Botón cancelar de la ventana
            $el.find("#" + pluginName + "-btnCancelar").on('click', function () {                
                hideModal();
                hook("onCancelar");
            });

            //Boton buscar
            $el.find("#" + pluginName + "-btnObtener").on('click', search);

            //Buscar al presionar ENTER
            $el.find(".fk_filters input").on('keyup', function (e) {
                if (e.keyCode == 13) search();
            });

            //Vaciar el content al escribir algo en los filtros.
            $el.find(".fk_filters input").on('keyup', function (e) {
                $el.find("#" + pluginName + "-listcontent").empty();
            });

            //Buscar en la lista
            $el.find("#" + pluginName + "-txtSearchInList").on("keyup", function (e) {
                var code = e.keyCode || e.which;
                if (code == '9') return;
                if (code == '27') $(this).val(null);
                var $rows = $el.find("#" + pluginName + "-listcontent li");
                //var $rows = $(this).closest('.dual-list').find('.list-group li');
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
                $rows.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return !~text.indexOf((val));
                }).hide();
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
                $el.find(".fk_filters").removeClass("hide");
                $el.find("#" + pluginName + "-modal").on('shown.bs.modal', function () {
                    $el.find("#" + pluginName + "-txtApellido1").focus()
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

            var n = window.setTimeout(function () { $el.find("#" + pluginName + "-listcontent").html("Buscando ...") }, 500)

            var payload = $.extend({}, _options.searchParams)
            if (!_options.autoSearch) {
                var filter = {
                    t001_nombre: $el.find("#" + pluginName + "-txtNombre").val(),
                    t001_apellido1: $el.find("#" + pluginName + "-txtApellido1").val(),
                    t001_apellido2: $el.find("#" + pluginName + "-txtApellido2").val()
                }

                $.extend(payload, filter);
            }

            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + _options.modulo + "/Services/Profesionales.asmx";

            IB.DAL.post(serviceUrl, _options.tipoAyuda, payload, null,
                function (data) {
                    if (_options.autoSearch && data.length >= 10) {
                        $el.find("#" + pluginName + "-searchInListContainer").removeClass("hide").addClass("show");
                    }
                    data.forEach(function (item) {
                        html += "<li class='list-group-item' data-idficepi='" + item.t001_idficepi + "'>" + item.profesional + "</li>"
                    });
                    if (html.length == 0) html += _options.notFound;

                    $el.find("#" + pluginName + "-listcontent").html(html);

                    window.clearTimeout(n);
                }
            );
        }

        function option(key, val) {

            if (typeof _options[key] === "object") {
                if (val === undefined) {
                    return _options[key];
                }
                else if (Array.isArray(_options[key])) {
                    _options[key] = val;
                }
                else {
                    $.extend(_options[key], val);
                }
            }
            else {
                if (val === undefined) {
                    return _options[key];
                }
                else {
                    _options[key] = val;
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
                $el.find("#" + pluginName + "-btnObtener").off('click');
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
        titulo: "Selección de profesionales",
        notFound: "No se han encontrado datos",
        modulo: "SIC",
        tipoAyuda: "GeneralFicepi",
        autoSearch: true,
        autoShow: false,
        searchParams: {},
        onSeleccionar: function () { },
        onCancelar: function () { },
        onInit: function () { },
        onDestroy: function () { }
    };

})(jQuery);