
(function ($) {

    var pluginName = 'ayudasubareasficepi';

    function Plugin(element, options) {

        var el = element;
        var $el = $(element);

        options = $.extend({}, $.fn[pluginName].defaults, options);

        function init() {

            initModal();
            $.when(loadComboAreas()).then(function(){
                if (options.autoShow) showModal();
            });

            hook('onInit');
        }

        function initModal() {
            $el.append("<style>padfilter{padding:0 15 0 15}</style>");
            $el.append("<div class='modal fade' id='" + pluginName + "-modal'>" +
                 "<div class='modal-dialog modal-lg'>" +
                     "<div class='modal-content'>" +
                         "<div class='modal-header btn-primary'>" +
                             "<h4 class='modal-title'>" + options.titulo + "</h4>" +
                         "</div>" +

                         "<div class='modal-body dual-list'>" +
                             "<div id='" + pluginName + "-divProfesionales'>" +
                                 "<div class='row'>" +
                                     "<!--Filtros de búsqueda-->" +
                                    "<div class='fk_filters'>" +
                                        "<div class='col-xs-2 col-sm-1 padfilter'>Área</div>" +
                                        "<div class='col-xs-10 col-sm-4 padfilter'>" +
                                            "<select id='" + pluginName + "-cmbSubarea' class='col-xs-12 col-sm-12' ></select>" +
                                        "</div>" +
                                    "</div>" +


                                     "<div class='clearfix'></div><br />" +

                                     "<!--Lista de profesionales-->" +
                                     "<div id='" + pluginName + "-lisProfesionales' class='text-left dual-list list-left col-xs-12 col-sm-5'>" +
                                         "<h4>" + options.tituloContIzda + "</h4>" +
                                         "<div class='well'>" +
                                             "<div class='row'>" +
                                                 "<div class='btn-group col-xs-2'>" +
                                                     "<a class='btn btn-default selector' data-toggle='popover' title='' data-content='Marcar/desmarcar todo' data-placement='top'>" +
                                                         "<i class='glyphicon glyphicon-unchecked'></i>" +
                                                     "</a>" +
                                                 "</div>" +
                                                   "<div class='col-xs-10 input-group'>" +
                                                     "<input type='text' name='SearchDualList' class='form-control' placeholder='Buscar en el contenedor' />" +
                                                 "</div>" +

                                             "</div>" +
                                             "<ul class='list-group table-hover' id='" + pluginName + "-UlProfesionales'>" +
                                             "</ul>" +
                                         "</div>" +
                                     "</div>" +
                                     "<div class='list-arrows btnacciones col-xs-12 col-sm-2 text-center MT100'>" +
                                         "<div class='row col-xs-3 col-xs-offset-3 col-sm-offset-0 col-sm-12'>" +
                                             "<button class='btn btn-default btn-sm move-right'>" +
                                                 "<span class='glyphicon glyphicon-chevron-right hidden-xs'></span>" +
                                                 "<span class='glyphicon glyphicon-chevron-down visible-xs'></span>" +
                                             "</button>" +
                                         "</div>" +
                                     "</div>" +

                                     "<!--Lista de seleccionados -->" +
                                     "<div id='" + pluginName + "-lisSeleccionados' class='dual-list list-right col-xs-12 col-sm-5'>" +
                                         "<h4>" + options.tituloContDcha + "</h4>" +
                                         "<div class='well'>" +
                                             "<div class='row'>" +
                                                 "<div class='btn-group col-xs-2'>" +
                                                     "<a class='btn btn-default selector' data-toggle='popover' title='' data-content='Marcar/desmarcar todo' data-placement='top'>" +
                                                         "<i class='glyphicon glyphicon-unchecked'></i>" +
                                                     "</a>" +
                                                 "</div>" +
                                                 "<div class='col-xs-10 input-group'>" +
                                                     "<input type='text' name='SearchDualList' class='form-control' placeholder='Buscar en el contenedor' />" +
                                                 "</div>" +
                                             "</div>" +
                                             "<ul class='list-group table-hover' id='" + pluginName + "-UlSeleccionados'>" +
                                             "</ul>" +


                                             "<div class='pull-left'>" +
                                                 "<button id='" + pluginName + "-btnDesmarcar' class='move-left btn-link underline'>Desmarcar eliminados</button>" +
                                             "</div>" +
                                             "<!--Eliminar Seleccionados-->" +
                                             "<div class='btnacciones pull-right'>" +
                                                 "<button id='" + pluginName + "-btnEliminarSeleccionados' class='move-left btn-link underline'>Eliminar</button>" +
                                             "</div>" +
                                         "</div>" +
                                     "</div>" +
                                 "</div>       " +
                             "</div>" +

                         "</div>" +
                         "<div class='modal-footer'>" +
                             "<b>" +
                                 "<button id='" + pluginName + "-btnAceptar' class='btn btn-primary'>Aceptar</button></b>" +
                             "<b>" +
                                 "<button id='" + pluginName + "-btnCancelar' class='btn btn-default' style='margin-left: 7px'>Cancelar</button></b>" +
                         "</div>" +
                     "</div>" +
                     "<!-- /.modal-content -->" +
                 "</div>" +
                 "<!-- /.modal-dialog -->" +
             "</div>");


            //Botón cancelar de la ventana      
            $el.find("#" + pluginName + "-btnCancelar").on('click', function () {
                clearValues();
                hideModal();
                hook("onCancelar");
            });

            //Botón aceptar de la ventana
            $el.find("#" + pluginName + "-btnAceptar").on('click', function (e) {

                var $selected = $el.find('#' + pluginName + '-UlSeleccionados li');

                var datos = [];

                $selected.each(function () {
                    oDatos = new Object();
                    oDatos.key = $(this).attr("data-key");
                    oDatos.value = $(this).html();
                    oDatos.estado = $(this).attr("data-estado");
                    datos.push(oDatos);
                })

                //Devolvemos a la pantalla llamante una lista de objetos
                hook("onAceptar", datos);

                hideModal();
            })

            //Selección de profesionales
            $el.on("click", "#" + pluginName + "-divProfesionales .list-group .list-group-item", function (e) {
                var lista = $(this).parent().children();
                if (e.shiftKey && lista.filter('.active').length > 0) {
                    var first = lista.filter('.active:first').index();//Primer seleccionado
                    var last = lista.filter('.active:last').index();//Último seleccionado
                    $el.find('#' + pluginName + '-divProfesionales .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                    if ($(this).index() > first)
                        lista.slice(first, $(this).index() + 1).addClass('active');
                    else
                        lista.slice($(this).index(), last + 1).addClass('active');
                }
                else if (e.ctrlKey) {
                    $(this).toggleClass('active');
                } else {
                    //SELECCIÓN MÚLTIPLE SIEMPRE
                    $(this).toggleClass('active');
                }
            });

            //Click en los botones (seleccionar o marcar para eliminar)
            $el.find('#' + pluginName + '-divProfesionales .btnacciones button').on('click', function () {
                var $button = $(this)

                if ($button.hasClass('move-left')) { //mover de dcha a izda (marcar para eliminar)

                    //Lista de profesionales seleccionados en la dcha
                    var lstActivos = $el.find('#' + pluginName + '-UlSeleccionados li.active');

                    //No se ha seleccionado ningún profesional
                    if (lstActivos.length == 0) {
                        IB.bsalert.toast("No has seleccionado ningún item", "warning");
                        return;
                    }

                    var avisoEliminarExistentes = false;

                    lstActivos.each(function (index, element) {

                        if ($(element).attr("data-estado") == "N") {
                            $(element).remove();
                        }
                        else if ($(element).attr("data-estado") == "E") {
                            if (options.eliminarExistentes) {
                                $(element).attr("data-estado", "X");
                                $(element).addClass("marcarEliminar");
                            }
                            else {
                                avisoEliminarExistentes = true;
                            }
                        }
                    })

                    if (avisoEliminarExistentes) {
                        IB.bsalert.toast("No se permite marcar para eliminar los items que ya estaban en la lista", "warning");
                    }


                } else if ($button.hasClass('move-right')) {  //mover de izda a dcha               

                    //Lista de profesionales seleccionados en la izda
                    var lstActivos = $el.find('#' + pluginName + '-UlProfesionales li.active');

                    //No se ha seleccionado ningún profesional
                    if (lstActivos.length == 0) {
                        IB.bsalert.toast("No has seleccionado ningún item", "warning");
                        return;
                    }

                    //Recorrer los seleccionados en la izda y comprobar si ya estan en la dcha
                    lstActivos.each(function (index, element) {

                        var $foundElement = $el.find("#" + pluginName + "-UlSeleccionados li[data-key='" + $(this).attr("data-key") + "']");

                        if ($foundElement.length == 0) { //No está en la dcha, pasarlo en estado N
                            var $newElement = $(element).clone();
                            $newElement.attr("data-estado", "N");
                            $el.find("#" + pluginName + "-UlSeleccionados").append($newElement);
                        }

                        else if ($foundElement.attr("data-estado") == "X") { //Está en la dcha pero marcado para eliminar --> desmarcar y poner estado E
                            $foundElement.attr("data-estado", "E");
                            $foundElement.removeClass("marcarEliminar");
                        }

                    })

                }

                $('.list-group-item').removeClass('active');
                $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
            });

            //Seleccionar todos o ninguno. 
            $el.find('#' + pluginName + '-divProfesionales .dual-list .selector').on('click', function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
                    $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
                } else {
                    $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
                    $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
                }
            });

            //Buscar en la lista
            $el.find("#" + pluginName + "-divProfesionales [name='SearchDualList']").on("keyup", function (e) {
                var code = e.keyCode || e.which;
                if (code == '9') return;
                if (code == '27') $(this).val(null);
                var $rows = $(this).closest('.dual-list').find('.list-group li');
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
                $rows.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return !~text.indexOf((val));
                }).hide();
            });

            //Desmarcar eliminados
            $el.find("#" + pluginName + "-btnDesmarcar").on('click', function () {

                //Lista de profesionales seleccionados en la dcha con estado X
                var lstActivos = $el.find("#" + pluginName + "-UlSeleccionados li.active[data-estado='X']");

                //Desmarcar eliminados                
                if (lstActivos.length > 0) {
                    lstActivos.removeClass("marcarEliminar");//.addClass("desmarcarEliminar");
                    lstActivos.attr("data-estado", "E");
                }
                else {
                    IB.bsalert.toast("No has seleccionado ningún item marcado para eliminar", "warning");
                }

                //Lista de profesionales seleccionados en la dcha
                var lstActivos = $el.find("#" + pluginName + "-UlSeleccionados li.active");
                lstActivos.removeClass("active");
            })


            //al seleccionar un area en el combo, buscar las subareas.
            $el.find("#" + pluginName + "-cmbSubarea").on("change", function (e) {
                $el.find("#" + pluginName + "-UlProfesionales").empty();
                search();
            });
        }

        function loadComboAreas() {

            var html = "";
            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + options.modulo + "/Services/AyudaSubareaFicepi.asmx";

            IB.DAL.post(serviceUrl, "ObtenerAreasDeSubareasDeFicepi", null, null,
                function (data) {
                    if (data.length > 1)                
                        data.splice(0, 0, { Key: "-1", Value: "Selecciona un área..." });

                    data.forEach(function (item) {
                        html += "<option value='" + item.Key  + "'>" + item.Value  + "</option>"
                    });

                    $el.find("#" + pluginName + "-cmbSubarea").append(html);

                    if (data.length == 1) {
                        $el.find("#" + pluginName + "-cmbSubarea option").attr("selected", "selected");
                        $el.find("#" + pluginName + "-cmbSubarea").trigger("change");
                    }

                    
                }
            );
        }

        function showModal() {

            $el.find(".modal-title").html(options.titulo)

            if (options.eliminarExistentes)
                $el.find("#" + pluginName + "-btnDesmarcar").css("display", "block");
            else
                $el.find("#" + pluginName + "-btnDesmarcar").css("display", "none");

            $el.find('.ocultable').attr('aria-hidden', 'true');
            $el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $el.find(".modal").modal('show');

            var html = "";
            var tachadoClass = "";
            //Pintamos los profesionales de la lista derecha (vienen como options)
            options.lstSeleccionados.forEach(function (item) {
                tachadoClass = "";
                if (!item.estado || item.estado == null || item.estado == "") item.estado = "E";
                if (item.estado == "X") tachadoClass = "marcarEliminar";

                html += "<li class='list-group-item " + tachadoClass + "' data-estado='" + item.estado + "' data-key='" + item.key + "'>" + item.value + "</li>"
            });

            $el.find("#" + pluginName + "-UlSeleccionados").html(html);

            //Ocultar los campos de criterio de filtrado
            if (options.autoSearch) {
                $el.find(".fk_filters").addClass("hide");
                search();
            }

            var $cmdArea = $el.find("#" + pluginName + "-cmbSubarea");

            if ($cmdArea.find("option").length == 1) {
                $el.find("#" + pluginName + "-cmbSubarea option").attr("selected", "selected");
                $el.find("#" + pluginName + "-cmbSubarea").trigger("change");
            }
            else if ($cmdArea.find("option").length > 1) {
                $el.find("#" + pluginName + "-cmbSubarea").val("-1");
            }

        }

        function hideModal() {
            $el.find(".modal").modal('hide');
            //Limpiamos valores al ocultar la modal
            clearValues();
        }

        function clearValues() {
            $el.find("input[type='text']").val("");
            $el.find("#" + pluginName + "-UlProfesionales").empty();
            $el.find("#" + pluginName + "-UlSeleccionados").html(options.lstParticipantes);
        }

        function search() {

            var ta200_idareapreventa = $el.find("#" + pluginName + "-cmbSubarea option:selected").val();
            if(ta200_idareapreventa == "-1") return;

            $el.find("#" + pluginName + "-listcontent").empty();

            var html = "";

            var n = window.setTimeout(function () { $el.find("#" + pluginName + "-listcontent").html("Buscando ...") }, 500)

            var payload = {
                ta200_idareapreventa: ta200_idareapreventa
            };

            console.log("Buscando: " + payload.filtro);

            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + options.modulo + "/Services/AyudaSubareaFicepi.asmx";

            IB.DAL.post(serviceUrl, "ObtenerSubareasPorAreaFicepi", payload, null,
                function (data) {
                    data.forEach(function (item) {
                        html += "<li class='list-group-item' data-key='" + item.Key + "'>" + item.Value + "</li>"
                    });

                    if (html.length == 0) html += options.notFound;

                    $el.find("#" + pluginName + "-UlProfesionales").html(html);
                    window.clearTimeout(n);
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

        function destroy() {

            $el.each(function () {
                var el = this;
                var $el = $(this);

                hook('onDestroy');

                $el.removeData('plugin_' + pluginName);

                $el.off("click", ".list-group-item");
                $el.find("#" + pluginName + "-btnAceptar").off('click');
                $el.find("#" + pluginName + "-btnCancelar").off('click');
                $el.find("#" + pluginName + "-btnObtener").off('click');
                $el.find(".fk_filters input").off('keyup');
                $el.html("");

            });
        }

        function hook(hookName, data) {
            if (options[hookName] !== undefined) {
                options[hookName].call(el, data);
            }
        }

        function ordenar(objsLis) {
            return objsLis.sort(function (a, b) {
                var A = $(a).text().toUpperCase();
                var B = $(b).text().toUpperCase();
                return (A < B) ? -1 : (A > B) ? 1 : 0;
            });
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
        titulo: "Selección de subáreas",
        tituloContIzda: "Subáreas",
        tituloContDcha: "Subáreas seleccionadas",
        notFound: "No se han encontrado datos",
        modulo: "SIC",
        tipoAyuda: "SubareasPreventa",
        autoSearch: false,
        autoShow: false,
        eliminarExistentes: false,
        lstSeleccionados: [], //{key, profesional, value = (N=nuevo; E=Existente; X=marcado para eliminar)}
        filtro: "",
        onAceptar: function () { },
        onCancelar: function () { },
        onInit: function () { },
        onDestroy: function () { }
    };

})(jQuery);