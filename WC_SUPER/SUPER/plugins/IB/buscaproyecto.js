var datable;
(function ($) {

    var pluginName = 'buscaproyecto';

    if (typeof $.fn[pluginName] === "undefined") {
        $("<link/>", {
            rel: "stylesheet",
            type: "text/css",
            href: IB.vars.strserver + "plugins/datatables/jquery.dataTables.min.css"
        }).appendTo("head");
        $("<link/>", {
            rel: "stylesheet",
            type: "text/css",
            href: IB.vars.strserver + "Capa_Presentacion/IAP30/css/IAP30.css"
        }).appendTo("head");
        if (!getScript(IB.vars.strserver + "plugins/datatables/jquery.dataTables.min.js")) $.getScript(IB.vars.strserver + "plugins/datatables/jquery.dataTables.min.js"); 
    }

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
            $el.append("<div class='modal fade' id='" + pluginName + "-modal'>" +
                       "<div class='modal-dialog' role='dialog'>" +
                            "<div class='modal-content'>" +

                                "<div class='modal-header btn-primary'>" +
                                "<button type='button' class='close' data-dismiss='modal' aria-label='Cerrar'><span aria-hidden='true'>&times;</span></button>" +
                                    "<h4 class='modal-title'>::: SUPER ::: - " + options.titulo + "</h4>" +
                                "</div>" +
                                "<div class='modal-body'>" +
                                    "<div class='row'>" +
                                        "<div class='col-xs-12' id='" + pluginName + "-contenedor'>" +
                                                "<table id='" + pluginName + "-tabla' class='display' cellspacing='0' width='100%'>" +
                                                    "<thead>" +
                                                        "<tr>" +
                                                            "<th id='t301_estado' class='bg-primary'></th>" +
                                                            "<th id='t301_idproyecto' class='bg-primary'> Nº</th>" +
                                                            "<th id='t301_denominacion' class='bg-primary'> Proyecto</th>" +
                                                            "<th id='t302_denominacion' class='bg-primary'> Cliente</th>" +
                                                        "</tr>" +
                                                    "</thead>" +
                                                    "<tbody>" +
                                                    "</tbody>" +
                                                "</table>" +
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

            //Botón aceptar de la ventana
            $el.find("#" + pluginName + "-btnSeleccionar").on('click', function (e) {
                //Se obtiene el elemento activo
                var $selected = $el.find("#" + pluginName + "-tabla").find("tr.selected");
                //Si no hubiera ninguno se muestra el aviso
                if ($selected.length == 0) {
                    IB.bsalert.toastwarning("No se ha seleccionado ningún proyecto");
                    return false;
                }

                var data = {
                    filaSeleccionada: datable.rows('.selected').data()[0]
                    //IdProyectoSubnodo: $selected.children().eq(4).text(),
                    //DenomProyecto: $selected.children().eq(2).text()
                };
                hook("onSeleccionar", data);
                hideModal();
            });

            //Botón cancelar de la ventana
            $el.find("#" + pluginName + "-btnCancelar").on('click', function () {
                hideModal();
                hook("onCancelar");
            });

        }
        //$(document).on(event, selector, callback);

        $(document).on('dblclick', '#buscaproyecto-tabla > tbody > tr', function (e) {     
            var data = {
                filaSeleccionada: datable.rows(datable.row(this).index()).data()[0]
            };
            hook("onSeleccionar", data);
            hideModal();
        });

        function showModal() {

            $el.find(".modal-title").html(options.titulo)
            $el.find('.ocultable').attr('aria-hidden', 'true');
            $el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" }, "show");
    
            //ocultar los campos de criterio de filtrado
            if (options.autoSearch) {
                $el.find(".fk_filter").addClass("hide");
                search();
                //$.when(search()).then(function () {
                //    datable.columns.adjust().draw();
                //});
            }
        }

        function hideModal() {
            $('.popover').hide();
            $el.find(".modal").modal('hide');
            $el.find("input[type='text']").val("");
            $el.find("#" + pluginName + "-tabla").empty();
        }

        /**
        * Búsqueda en bloque
        */
        function search() {
            //var defer = $.Deferred();
            // $el.find("#" + pluginName + "-tabla").empty();

            var indicadores = {
                i_dispositivoTactil: false
            }

            if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
                indicadores.i_dispositivoTactil = true;
            }

            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + options.modulo + "/Services/BuscadorProyectos.asmx/" + options.searchParams.tipoBusqueda;
            if (typeof datable != "undefined") datable.destroy();
            datable = $("#" + pluginName + "-tabla").DataTable({
                "procesing": true,
                "retrieve": true,
                "destroy": true,
                "paging": false,
                "scrollY": "410px",
                "scrollCollapse": true,
                "language": { "url": IB.vars.strserver + 'plugins/datatables/Spanish.txt' },
                "info": false,
                "searching": true,
                "aaSorting": [],
                "ajax": {
                    "url": serviceUrl,
                    "type": "POST",
                    "contentType": "application/json; charset=utf-8",
                    //"data": function () { return JSON.stringify(); },
                    "dataSrc": function (json) {
                        return json.d;
                    },
                    "error": function (ex, status) {
                        IB.bserror.error$ajax(ex, status);
                    }
                },

                "columns": [
                        { "data": "t301_estado" },
                        { "data": "t301_idproyecto" },
                        { "data": "t301_denominacion" },
                        { "data": "t302_denominacion" },
                ],
                "columnDefs":
                            [
                                {
                                    "targets": 0,
                                    "bSortable": false,
                                    "columns": {className: ""},
                                    "render": {
                                        "display": function (data, type, row, meta) {
                                            //return row.t301_estado + " hola";
                                            var sColor = "";
                                            switch (row.t301_estado) {
                                                case "A":
                                                    sColor = "verde";
                                                    break;
                                                case "H":
                                                    sColor = "gris";
                                                    break;
                                                case "C":
                                                    sColor = "rojo";
                                                    break;
                                                case "P":
                                                    sColor = "naranja";
                                                    break;
                                            }
                                            //var sContent = "<b>Proyecto:</b> " + row.t301_denominacion.replace(/"/g, "'") + "<br /> <b>Responsable: </b> " + row.Responsable_Proyecto.replace(/"/g, "'") + "<br />";
                                            //sContent += "<b>" + ((row.Sexo_Aprobador == "V") ? "Aprobador" : "Aprobadora") + ": </b>" + row.Aprobador.replace(/"/g, "'") + "<br />";
                                            //sContent += "<b>C.R.: </b>" + row.t303_denominacion.replace(/"/g, "'");
                                            //return '<span aria-hidden="true" class="fa fa-diamond fa-diamond-' + sColor + '" data-placement="top" data-toggle="popover" data-content="' + sContent + '" title="<b>Información</b>"></span>';
                                            return "<span aria-hidden='true' class='fa fa-diamond fa-diamond-" + sColor + "'></span>";
                                        }
                                    }
                                },
                                {
                                    "targets": 2,
                                     "render": {
                                         "display": function (data, type, row, meta) {
                                             var sContent = "<b>Proyecto:</b> " + row.t301_denominacion.replace(/"/g, "'") + "<br /> <b>Responsable: </b> " + row.Responsable_Proyecto.replace(/"/g, "'") + "<br />";
                                             if (row.Sexo_Aprobador != null) {
                                                 //Si venimos de una pantalla que no es GASVI no tiene sentido poner el aprobador de notas GASVI
                                                 sContent += "<b>" + ((row.Sexo_Aprobador == "V") ? "Aprobador" : "Aprobadora") + ": </b>" + row.Aprobador.replace(/"/g, "'") + "<br />";
                                             }
                                             sContent += "<b>C.R.: </b>" + row.t303_denominacion.replace(/"/g, "'");
                                             return '<span data-placement="top" data-toggle="popover" data-content="' + sContent + '" title="<b>Información</b>">' + row.t301_denominacion.replace(/"/g, "'") + '</span>';
                                         }
                                     }

                                }
                            ],
                        "fnInitComplete": function (oSettings, json) {
                            setTimeout("datable.columns.adjust().draw();", 300);                            
                            if (!indicadores.i_dispositivoTactil) {
                                $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true });
                            }
                            $(".dataTables_scrollHeadInner").css("padding-left", "0px");
                            $("#buscaproyecto-tabla > tbody > tr").css("cursor", "pointer");
                            setTimeout('$("input[type=search]").focus();',400);
                    }
            });
           
            //return defer.promise();
        };

        $(document).on('click', '#' + pluginName + '-tabla tbody tr', function (e) {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
                $('#' + pluginName + '-btnSeleccionar').attr('disabled', 'disabled');
            }
            else {
                $('tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('#' + pluginName + '-btnSeleccionar').removeAttr('disabled');
            }
        });

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

                //$el.off("click", ".list-group-item");
                $el.find("#" + pluginName + "-btnSeleccionar").off('click');
                $el.find("#" + pluginName + "-btnCancelar").off('click');
                $el.find("#" + pluginName + "-btnObtener").off('click');
                //$el.find(".fk_filter input").off('keyup');
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


})(jQuery);