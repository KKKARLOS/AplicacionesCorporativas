(function ($) {

    var pluginName = 'buscaclientemulti';

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
                 "<div class='modal-dialog modal-lg'>" +
                     "<div class='modal-content'>" +
                         "<div class='modal-header btn-primary'>" +
                             "<h4 class='modal-title'>" + _options.titulo + "</h4>" +
                         "</div>" +

                         "<div class='modal-body dual-list'>" +
                             "<div id='" + pluginName + "-divProfesionales'>" +
                                 "<div class='row'>" +
                                     "<!--Filtros de búsqueda-->" +
                                    "<div class='fk_filters'>" +
                                        "<div>" +
                                            "<div class='col-xs-8 fk_filter'>" +
                                                "<input type='text' id='" + pluginName + "-txtFiltro' class='form-control input-md' placeholder='Buscar cliente por denominación' />" +
                                            "</div>" +
                                            "<div class='col-xs-4 fk_filter'>" +
                                                "<select id='" + pluginName + "-selBusqueda' name='" + pluginName + "-selBusqueda' class='form-control fk_filtro_proy'>" +
                                                    "<option value='I'>Inicia con</option>" +
                                                    "<option value='C' selected='selected'>Contiene</option>" +
                                                "</select>" +
                                            "</div>" +
                                        "</div>" +
                                    "</div>" +


                                     "<div class='clearfix'></div><br />" +

                                     "<!--Lista de profesionales-->" +
                                     "<div id='" + pluginName + "-lisProfesionales' class='text-left dual-list list-left col-xs-12 col-sm-5'>" +
                                         "<h4>" + _options.tituloContIzda + "</h4>" +
                                         "<div class='marco no-padding'>" +
                                             "<div class='row'>" +
                                                 "<div class='btn-group selector-table col-xs-2'>" +
                                                     "<a class='selector' data-toggle='popover' title='' data-content='Marcar/desmarcar todo' data-placement='top'>" +
                                                         "<i class='glyphicon glyphicon-unchecked'></i>" +
                                                     "</a>" +
                                                 "</div>" +
                                                   "<div class='col-xs-10 input-group fk_flex'>" +
                                                    "<span>Buscar: </span>" +
                                                     "<input type='text' class='form-control' />" +
                                                 "</div>" +

                                             "</div>" +
                                             "<div class='text-left bg-primary th-table'>Denominación</div>" +
                                             "<ul class='list-group table-hover ul-table' id='" + pluginName + "-UlProfesionales'>" +
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
                                         "<h4>" + _options.tituloContDcha + "</h4>" +
                                         "<div class='marco no-padding'>" +
                                             "<div class='row'>" +
                                                 "<div class='btn-group selector-table col-xs-2'>" +
                                                     "<a class='selector' data-toggle='popover' title='' data-content='Marcar/desmarcar todo' data-placement='top'>" +
                                                         "<i class='glyphicon glyphicon-unchecked'></i>" +
                                                     "</a>" +
                                                 "</div>" +
                                                 "<div class='col-xs-10 input-group fk_flex'>" +
                                                    "<span>Buscar: </span>" +
                                                     "<input type='text' class='form-control' />" +
                                                 "</div>" +
                                             "</div>" +
                                             "<div class='text-left bg-primary th-table'>Denominación</div>" +
                                             "<ul class='list-group table-hover ul-table' id='" + pluginName + "-UlSeleccionados'>" +
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
                             "<div class='col-xs-12'>" +                                                    
                                "<div class='" + pluginName + "-leyenda'>" +
                                    "<img alt='Icono de Matriz' border='0' src='" + IB.vars.strserver + "Images/imgM.gif' />" +
                                    "<span> Matriz</span>" +
                                    "&nbsp;&nbsp;&nbsp;&nbsp;" +
                                    "<img alt='Icono de Filial' border='0' src='" + IB.vars.strserver + "Images/imgF.gif' />" +
                                    "<span> Filial</span>" +
                                "</div>" +
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

            $("head").append("<style type='text/css'>" +
            "ul.list-group.table-hover.ul-table { border: solid 1px #e4e2e2; }" +
            ".selector-table {padding-bottom: 9px;}" +
            ".selector:hover {cursor: pointer;}" +
            ".th-table { padding-left: 4px !important; padding-top: 3px !important; padding-bottom: 3px !important;}" +
            ".ul-table { margin-top: 0 !important;}" +
            ".fk_flex { display: flex;}" +
            "." + pluginName + "-linea:hover {cursor:pointer;}" +
            ".activa {background-color: #d9edf7;}" +
            ".activa:hover {background-color: #d9edf7 !important;}" +
            ".cebreada {background-color: #f9f9f9;}" +
            "." + pluginName + "-leyenda {font-size: 11.5px !important;width: 100%;margin-top: 5px;}" +
            ".fk_filter {padding-bottom: 15px;}" +
            ".fk_filial {padding-left: 22px !important;}" +
            ".fk_gris {color: grey;}" +
            "img {vertical-align: initial;}" +
            "</style>");


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
                    //oDatos.value = $(this).html();
                    oDatos.value = $(this).attr("data-value");
                    oDatos.tipo = $(this).attr("data-tipo");
                    oDatos.estadoF = $(this).attr("data-estadoF");
                    datos.push(oDatos);
                })

                //Devolvemos a la pantalla llamante una lista de objetos
                hook("onAceptar", datos);

                hideModal();
            })

            //Selección de profesionales
            $el.on("click", "#" + pluginName + "-divProfesionales .list-group .list-group-item", function (e) {
                var lista = $(this).parent().children();
                if (e.shiftKey && lista.filter('.activa').length > 0) {
                    var first = lista.filter('.activa:first').index();//Primer seleccionado
                    var last = lista.filter('.activa:last').index();//Último seleccionado
                    $el.find('#' + pluginName + '-diProfesionales .list-group .list-group-item').removeClass('activa');//Borrar de las dos listas
                    if ($(this).index() > first)
                        lista.slice(first, $(this).index() + 1).addClass('activa');
                    else
                        lista.slice($(this).index(), last + 1).addClass('activa');
                }
                else if (e.ctrlKey) {
                    $(this).toggleClass('activa');
                } else {
                    //SELECCIÓN MÚLTIPLE SIEMPRE
                    $(this).toggleClass('activa');
                }
            });

            //Click en los botones (seleccionar o marcar para eliminar)
            $el.find('#' + pluginName + '-divProfesionales .btnacciones button').on('click', function () {
                var $button = $(this)

                if ($button.hasClass('move-left')) { //mover de dcha a izda (marcar para eliminar)

                    //Lista de profesionales seleccionados en la dcha
                    var lstActivos = $el.find('#' + pluginName + '-UlSeleccionados li.activa');

                    //No se ha seleccionado ningún profesional
                    if (lstActivos.length == 0) {
                        IB.bsalert.toast("No has seleccionado ningún cliente", "warning");
                        return;
                    }

                    var avisoEliminarExistentes = false;

                    lstActivos.each(function (index, element) {

                        if ($(element).attr("data-estado") == "N") {
                            $(element).remove();
                        }
                        else if ($(element).attr("data-estado") == "E") {
                            if (_options.eliminarExistentes) {
                                $(element).attr("data-estado", "X");
                                $(element).addClass("marcarEliminar");
                            }
                            else {
                                avisoEliminarExistentes = true;
                            }
                        }
                    })

                    if (avisoEliminarExistentes) {
                        IB.bsalert.toast("No se permite marcar para eliminar los clientes que ya estaban en la lista", "warning");
                    }

                    //Se deshabilita el botón aceptar si no hay clientes seleccionados
                    if ($el.find("#" + pluginName + "-UlSeleccionados > li").length == 0) {                        
                        $el.find("#" + pluginName + "-btnAceptar").attr('disabled', 'disabled');
                    }


                } else if ($button.hasClass('move-right')) {  //mover de izda a dcha               

                    //Lista de profesionales seleccionados en la izda
                    var lstActivos = $el.find('#' + pluginName + '-UlProfesionales li.activa');

                    //No se ha seleccionado ningún profesional
                    if (lstActivos.length == 0) {
                        IB.bsalert.toast("No has seleccionado ningún cliente", "warning");
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

                    //var listaordenada = ordenar($('#' + pluginName + '-UlSeleccionados li'));
                    //$('#' + pluginName + '-UlSeleccionados').html(listaordenada);

                    $el.find("#" + pluginName + "-btnAceptar").removeAttr('disabled');
                }

                $('.list-group-item').removeClass('activa');
                $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
            });

            //Seleccionar todos o ninguno. 
            $el.find('#' + pluginName + '-divProfesionales .dual-list .selector').on('click', function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $checkBox.addClass('selected').closest('.marco').find('ul li:not(.activa)').addClass('activa');
                    $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
                } else {
                    $checkBox.removeClass('selected').closest('.marco').find('ul li.activa').removeClass('activa');
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
                var lstActivos = $el.find("#" + pluginName + "-UlSeleccionados li.activa[data-estado='X']");

                //Desmarcar eliminados                
                if (lstActivos.length > 0) {
                    lstActivos.removeClass("marcarEliminar");//.addClass("desmarcarEliminar");
                    lstActivos.attr("data-estado", "E");
                }
                else {
                    IB.bsalert.toast("No has seleccionado ningún cliente marcado para eliminar", "warning");
                }

                //Lista de profesionales seleccionados en la dcha
                var lstActivos = $el.find("#" + pluginName + "-UlSeleccionados li.activa");
                lstActivos.removeClass("activa");
            })

            var sto = null;

            //Buscar al ir escribiendo en el filtro
            $el.find(".fk_filters input").on('keyup', function (e) {
                if (e.keyCode == 13) {
                    IB.procesando.mostrar();
                    $el.find("#" + pluginName + "-UlProfesionales").empty();//Vaciar el content al escribir algo en el filtro.                    
                    $.when(search()).then(function () {
                        cebrear();
                        IB.procesando.ocultar();
                    });
                }                

                //if (sto != null) window.clearTimeout(sto);
                //sto = window.setTimeout(search, 500);
            });

            //Buscar al modificar el tipo de búsqueda
            $el.find("#" + pluginName + "-selBusqueda").on('change', function (e) {
                if ($el.find("#" + pluginName + "-txtFiltro").val() != "") {
                    IB.procesando.mostrar();
                    $el.find("#" + pluginName + "-UlProfesionales").empty();//Vaciar el content al escribir algo en el filtro.                    
                    $.when(search()).then(function () {
                        cebrear();
                        IB.procesando.ocultar();
                    });
                }                    
            });
        }

        function showModal() {            

            $el.find(".modal-title").html(_options.titulo)

            if (_options.eliminarExistentes)
                $el.find("#" + pluginName + "-btnDesmarcar").css("display", "block");
            else
                $el.find("#" + pluginName + "-btnDesmarcar").css("display", "none");

            $el.find('.ocultable').attr('aria-hidden', 'true');
            $el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $el.find(".modal").modal('show');

            var html = "";
            var tachadoClass = "";
            //Pintamos los profesionales de la lista derecha (vienen como _options)
            _options.searchParams.lstSeleccionados.forEach(function (item) {
                tachadoClass = "";
                if (!item.estado || item.estado == null || item.estado == "") item.estado = "E";
                if (item.estado == "X") tachadoClass = "marcarEliminar";

                html += "<li class='list-group-item' data-estado='N' data-tipo='" + item.tipo + "' data-estadoF='" + item.estado + "' data-key='" + item.key + "' data-value='" + item.value.replace(/"/g, "'") + "'><span";

                if (item.tipo == "F") {
                    html += ' class="fk_filial">';
                    html += '<img alt="Filial" border="0" src="' + IB.vars.strserver + 'Images/imgF.gif" /></span>';
                } else {
                    html += '><img alt="Matriz" border="0" src="' + IB.vars.strserver + 'Images/imgM.gif" /></span>';
                }
                html += '<span';
                if (!item.estado) html += ' class="fk_gris"';
                html += '>  ' + item.value.replace(/"/g, "'") + '</span></li>';
            });

            $el.find("#" + pluginName + "-UlSeleccionados").html(html);

            //Se deshabilita el botón aceptar si no hay clientes seleccionados
            if ($el.find("#" + pluginName + "-UlSeleccionados > li").length > 0) {
                $el.find("#" + pluginName + "-btnAceptar").removeAttr('disabled');
            } else {
                $el.find("#" + pluginName + "-btnAceptar").attr('disabled', 'disabled');
            }            

            //Ocultar los campos de criterio de filtrado
            if (_options.autoSearch) {
                $el.find(".fk_filters").addClass("hide");
                search();
            }
            else { //dar el foco al primer campo del filtrado
                $el.find("#" + pluginName + "-txtFiltro").attr("placeholder", _options.placeholderText);
                $el.find(".fk_filters").removeClass("hide");
                $el.find("#" + pluginName + "-modal").on('shown.bs.modal', function () {
                    $el.find("#" + pluginName + "-txtApellido1").focus()
                })
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
            $el.find("#" + pluginName + "-UlSeleccionados").html(_options.lstParticipantes);
        }

        function search() {

            var defer = $.Deferred();

            $el.find("#" + pluginName + "-listcontent").empty();

            var html = "";

            var filtro = _options.autoSearch ? _options.filtro : $el.find("#" + pluginName + "-txtFiltro").val();
            if (filtro.length == 0 && !_options.autoSearch) return;

            var n = window.setTimeout(function () { $el.find("#" + pluginName + "-listcontent").html("Buscando ...") }, 500)

            var payload = {
                t302_denominacion: filtro,
                sTipoBusqueda: $el.find("#" + pluginName + "-selBusqueda").val(),
                bSoloActivos: _options.searchParams.bSoloActivos,
                bInternos: _options.searchParams.bInternos
            }

            console.log("Buscando: " + payload.filtro);

            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + _options.modulo + "/Services/BuscadorClientes.asmx";

            IB.DAL.post(serviceUrl, _options.searchParams.tipoBusqueda, payload, null,
                function (data) {
                    data.forEach(function (item) {
                        //html += "<li class='list-group-item' data-key='" + item.t302_idcliente + "'><span>" + item.t302_denominacion + "</span></li>"

                        html += "<li class='list-group-item' data-tipo='" + item.tipo + "' data-estadoF='" + item.t302_estado + "' data-key='" + item.t302_idcliente + "' data-value='" + item.t302_denominacion.replace(/"/g, "'") + "'><span";

                        if (item.tipo == "F") {
                            html += ' class="fk_filial">';
                            html += '<img alt="Filial" border="0" src="' + IB.vars.strserver + 'Images/imgF.gif" /></span>';
                        } else {
                            html += '><img alt="Matriz" border="0" src="' + IB.vars.strserver + 'Images/imgM.gif" /></span>';
                        }
                        html += '<span';
                        if (!item.t302_estado) html += ' class="fk_gris"';
                        html += '>  ' + item.t302_denominacion.replace(/"/g, "'") + '</span></li>';
                        

                    });

                    if (html.length == 0) html += _options.notFound;

                    $el.find("#" + pluginName + "-UlProfesionales").html(html);
                    //cebrear();
                    window.clearTimeout(n);
                    defer.resolve();
                }
            );
            return defer.promise();
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
                $el.find("#" + pluginName + "-btnAceptar").off('click');
                $el.find("#" + pluginName + "-btnCancelar").off('click');
                $el.find(".fk_filters input").off('keyup');
                $el.find("#" + pluginName + "-selBusqueda").off('change');
                $el.find('#' + pluginName + '-divProfesionales .btnacciones button').off('click');
                $el.find('#' + pluginName + '-divProfesionales .dual-list .selector').off('click');
                $el.find("#" + pluginName + "-divProfesionales [name='SearchDualList']").off("keyup");
                $el.find("#" + pluginName + "-btnDesmarcar").off('click');
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
        tituloContIzda: "Profesionales",
        tituloContDcha: "Profesionales seleccionados",
        notFound: "No se han encontrado datos",
        modulo: "SIC",
        tipoAyuda: "GeneralFicepi",
        autoSearch: false,
        autoShow: false,
        eliminarExistentes: false,
        lstSeleccionados: [], //{key, profesional, value = (N=nuevo; E=Existente; X=marcado para eliminar)}
        filtro: "",
        admin: false,
        placeholderText: "",
        onAceptar: function () { },
        onCancelar: function () { },
        onInit: function () { },
        onDestroy: function () { }
    };

    /*Cebrea las líneas visualizadas*/
    function cebrear() {

        $("#" + pluginName + "-modal ul li").removeClass("cebreada");
        $("#" + pluginName + "-modal ul li:even").addClass("cebreada");

    }

    function ordenar(objsLis) {
        return objsLis.sort(function (a, b) {
            var A = $(a).text().toUpperCase();
            var B = $(b).text().toUpperCase();
            return (A < B) ? -1 : (A > B) ? 1 : 0;
        });
    }


})(jQuery);