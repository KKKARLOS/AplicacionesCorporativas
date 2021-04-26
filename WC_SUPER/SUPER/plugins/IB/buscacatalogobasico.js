(function ($) {

    var pluginName = 'buscacatalogobasico';



    function Plugin(element, options) {

        var el = element;
        var $el = $(element);

        options = $.extend({}, $.fn[pluginName].defaults, options);

        function init() {

            initModal();
            if (options.autoShow) showModal();
            hook('onInit');

        }

        function initModal() {

            $el.append("<div class='modal fade' id='" + pluginName + "-modal' role='dialog' tabindex='-1'title='::: SUPER ::: - " + options.titulo + "'>" +
                        "<div class='modal-dialog' role='dialog'>" +
                            "<div class='modal-content'>" +

                                "<div class='modal-header bg-primary'>" +
                                    '<button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>' +
                                    "<h4 class='modal-title'>::: SUPER ::: - " + options.titulo + "</h4>" +
                                "</div>" +

                                "<div class='modal-body'>" +
                                    "<div class='row'>" +
                                        "<div id='" + pluginName + "-contenedor'>" +                                            
                                             "<div class='form-group'>" +
                                                "<div class='col-xs-12'>" +
                                                    "<div class='table-responsive'>" +
                                                        "<table id='" + pluginName + "-tabla' class='table table-hover header-fixed'>" +
                                                        "</table>" +
                                                    "</div>" +                                                    
                                                "</div>" +
                                            "</div>" +
                                        "</div>" +
                                    "</div>" +

                                "</div>" +

                                "<div class='modal-footer'>" +
                                    "<b>" +
                                        "<button id='" + pluginName + "-btnSeleccionar' class='btn btn-primary' disabled='disabled'>Aceptar</button></b>" +
                                    "<b>" +
                                        "<button id='" + pluginName + "-btnCancelar' class='btn btn-default'>Cancelar</button></b>" +
                                "</div>" +
                            "</div>" +
                       "</div>" +
                  "</div>");

            $("head").append("<style type='text/css'>" +
            "." + pluginName + "-linea:hover {cursor:pointer;}" +
            ".activa {background-color: #d9edf7;}" +
            ".activa:hover {background-color: #d9edf7 !important;}" +
            ".cebreada {background-color: #f9f9f9;}" +            
            "</style>");

            //Acción de click en cada línea de la tabla
            $el.on("keypress click", "." + pluginName + "-linea", function (e) {
                //Se marca ele lemento activo
                marcarLinea($(this));
            });

            //Acción de click en cada línea de la tabla
            $el.on("dblclick", "." + pluginName + "-linea", function (e) {
                marcarLinea($(this));
                $("#" + pluginName + "-btnSeleccionar").trigger("click");
            });

            //Botón aceptar de la ventana
            $el.find("#" + pluginName + "-btnSeleccionar").on('click', function (e) {
                //Se obtiene el elemento activo
                var $selected = $el.find("#" + pluginName + "-tabla").find("tr.activa");
                //Si no huebira ninguno se muestra el aviso
                if ($selected.length == 0) {
                    IB.bsalert.toastwarning("No se ha seleccionado ningún elemento");
                    return false;
                }

                var data = {
                    target: options.searchParams.target,
                    idElemento: $selected.attr("data-id"),
                    desElemento: $selected.attr("data-den")
                }
                hook("onSeleccionar", data);

                hideModal();
            });

            //Botón cancelar de la ventana
            $el.find("#" + pluginName + "-btnCancelar").on('click', function () {
                hideModal();
                hook("onCancelar");
            });

        }

        function showModal() {            
            $('#' + pluginName + '-modal').attr('title', '::: SUPER ::: - ' + options.searchParams.titulo);
            $('#' + pluginName + '-modal').find('.modal-title').html('::: SUPER ::: - ' + options.searchParams.titulo);

            //ocultar los campos de criterio de filtrado
            if (options.autoSearch) {
                $.when(search()).then(function () {
                    cebrear();
                    $el.find('.ocultable' + options.searchParams.nivelOcultable).attr('aria-hidden', 'true');
                    $el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                    $el.find(".modal").modal('show');

                    IB.procesando.ocultar();
                });

            }
            else { //dar el foco al primer campo del filtrado
                $("#" + pluginName + "-btnSeleccionar").attr('disabled', 'disabled');
                $el.find('.ocultable' + options.searchParams.nivelOcultable).attr('aria-hidden', 'true');
                $el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $el.find(".modal").modal('show');

                $el.find("#" + pluginName + "-modal").on('shown.bs.modal', function () {
                    $el.find("#" + pluginName + "-buscador").focus()
                });
                IB.procesando.ocultar();
            }

        }

        function hideModal() {
            $el.find(".modal").modal('hide');
            $el.find('.ocultable' + options.nivelOcultable).attr('aria-hidden', 'false');
            $el.find("input[type='text']").val("");
            $el.find("#" + pluginName + "-tabla").empty();
        }

        /**
        * Búsqueda en bloque
        */
        function search() {

            var defer = $.Deferred();

            $el.find("#" + pluginName + "-tabla").empty();

            var html = "";
            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + options.modulo + "/Services/BuscadorCatalogoBasico.asmx";

            var n = window.setTimeout(function () { $el.find("#" + pluginName + "-tabla").html("Buscando ...") }, 500);

            var payload = $.extend({}, options.searchParams)
            //var filter = {
            //    sTipo: options.searchParams.sTipo,
            //    t303_idnodo: options.searchParams.idNodo
            //}
            var filter;
            if (options.searchParams.tipoBusqueda == "PtBitacora") {
                filter = {
                    sTipo: options.searchParams.tipoBusqueda,
                    t305_idproyectosubnodo: options.searchParams.t305_idproyectosubnodo
                }
            }
            else {
                filter = {
                    sTipo: options.searchParams.sTipo,
                    t303_idnodo: options.searchParams.idNodo
                }
            }
            IB.DAL.post(serviceUrl, options.searchParams.tipoBusqueda, filter, null,
                function (data) {
                    var sHtml = new StringBuilder();
                    var id, denominacion;
                    if (data.length == 0) {
                        sHtml.append('No se han encontrado elementos.');
                        $el.find("#" + pluginName + "-tabla").html(sHtml.toString());
                        defer.resolve();
                    } else {

                        sHtml.append('<thead><th class="text-left bg-primary">Denominación</th></thead>');
                        sHtml.append('<tbody style="overflow-y: auto;max-height: 40vh;">');                                                

                        data.forEach(function (item) {
                            sHtml.append('<tr class="' + pluginName + '-linea"');
                            sHtml.append(' data-id="' + item.identificador + '"');
                            sHtml.append(' data-den="' + item.denominacion + '">');
                            sHtml.append('<td>');

                            sHtml.append(item.denominacion.replace(/"/g, "'"));
                            sHtml.append('</td></tr>');
                        });
                        sHtml.append('</tbody>');
                        $el.find("#" + pluginName + "-tabla").html(sHtml.toString());
                        $el.find(".fk_filter").removeClass("hide");

                        defer.resolve();
                    }


                    window.clearTimeout(n);

                }
            );

            return defer.promise();
        }


        function option(key, val) {

            if (typeof options[key] === "object") {
                if (val === undefined)
                    return options[key];
                else
                    $.extend(options[key], val)
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

        function destroy() {

            $el.each(function () {
                var el = this;
                var $el = $(this);

                hook('onDestroy');

                $el.removeData('plugin_' + pluginName);                                    

                $el.off("keypress click", "." + pluginName + "-linea");
                $el.off("dblclick", "." + pluginName + "-linea");
                $el.find("#" + pluginName + "-btnSeleccionar").off('click');
                $el.find("#" + pluginName + "-btnCancelar").off('click');
                $el.html("");

            });
        }

        function hook(hookName, data) {
            if (options[hookName] !== undefined) {
                options[hookName].call(el, data);
            }
        }

        init();


        return {
            option: option,
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
        titulo: "",
        modulo: "IAP30",
        autoSearch: true,
        autoShow: false,
        searchParams: {},
        onSeleccionar: function () { },
        onCancelar: function () { },
        onInit: function () { },
        onDestroy: function () { }
    };


    /****************************************************** FUNCIONES PROPIAS DEL CONTROL ************************

    /*Cebrea las líneas visualizadas*/
    function cebrear() {

        $("#" + pluginName + "-tabla tbody tr").filter(function () {
            return $(this).css('display') == 'block';
        }).removeClass("cebreada");

        $("#" + pluginName + "-tabla tbody tr").filter(function () {
            return $(this).css('display') == 'block';
        }).filter(":even").addClass('cebreada');
    }

    //Marcado de línea activa
    marcarLinea = function (thisObj) {
        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        $("#" + pluginName + "-tabla tr.activa").removeClass('activa');
        $(thisObj).addClass('activa');
        $("#" + pluginName + "-btnSeleccionar").removeAttr('disabled');
    }

    desmarcarLinea = function () {
        $("#" + pluginName + "-tabla tr.activa").removeClass('activa');
        $("#" + pluginName + "-btnSeleccionar").attr('disabled', 'disabled');
    }

})(jQuery);