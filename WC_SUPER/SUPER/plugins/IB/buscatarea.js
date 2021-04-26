
(function ($) {

    var pluginName = 'buscatarea';



    function Plugin(element, options) {

        var el = element;
        var $el = $(element);

        options = $.extend({}, $.fn[pluginName].defaults, options);

        var indicadores = {
            i_dispositivoTactil: false
        }


        function init() {

            initModal();
            if (options.autoShow) showModal();
            hook('onInit');

            if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
                indicadores.i_dispositivoTactil = true;
            }
            
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
                                            "<div class='btn-group btn-group-xs hide' style='padding-bottom: 5px;'" +
                                                "role='group' aria-label='Botones de apertura de niveles del arbol de imputaciones'>" +
                                                "<button id='nivel1' data-level='1' aria-label='nivel 1' title='Proyectos económicos' type='button' class='btn btn-default btn-blanco'>" +
                                                    "<span id='barra1' class='fa fa-square fa-square-2 nivelVerde'></span>" +
                                                "</button>" +
                                                "<button id='nivel2' data-level='2' aria-label='nivel 2' title='Proyectos técnicos' type='button' class='btn btn-default btn-blanco'>" +
                                                    "<span id='barra2' class='fa fa-square fa-square-3 nivelGris'></span>" +
                                                "</button>" +
                                                "<button id='nivel3' data-level='3' aria-label='nivel 3' title='Tareas' type='button' class='btn btn-default btn-blanco'>" +
                                                    "<span id='barra3' class='fa fa-square fa-square-4 nivelGris'></span>" +
                                                "</button>" +
                                             "</div>" +
                                             "<span id='icoBomb' data-level='3' role='button' aria-label='Bomba' class='fa fa-bomb fa-1-5x link  hide' tabindex='0'></span>" +
                                            "</div>" +
                                            "</div>" +
                                            "<div class='form-group'>" +
                                                "<div class='col-xs-12 fk_filter hide'>" +
                                                    "<input type='text' id='" + pluginName + "-buscaTarea' name='SearchDualList' class='form-control input-md' placeholder=' Buscar (Introduce la cadena de búsqueda y pulsa Intro)' />" +
                                                "</div>" +
                                             "</div>" +
                                             "<div class='form-group'>" +
                                                "<div class='col-xs-12'>" +
                                                    "<div class='table-responsive'>"+
                                                        "<table id='" + pluginName + "-tabla' class='table header-fixed'>"+

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

            $("head").append("<style type='text/css'>"+
            ".activa {background-color: #d9edf7;}"+
            ".activa:hover {background-color: #d9edf7 !important;}"+
            ".leyenda {font-size: 11.5px !important;width: 100%;margin-top: 5px;}"+
            ".spanLeyenda {margin: 5px;}"+
            "/*Estados de tareas*/"+
            ".finalizada{color: Purple;}"+
            ".cerrada, .anulada{color: DimGray;}"+
            ".paralizada{color: Red;}"+
            ".pendiente{color: Orange;}"+
            ".obligaest{color: blue;}"+
            ".fa-bomb {font-size: 24px; cursor: pointer; vertical-align: bottom;padding-left: 5px;padding-bottom: 5px;}"+
            ".glyphicon-minus:before, .glyphicon-plus:before { font-size: 2em; line-height: 0; vertical-align: middle; color: #a2a2a2;}"+
            ".glyphicon-minus span, .glyphicon-plus span {vertical-align: middle;}</style>");

                  

            //Búsqueda de elementos de la tabla
            $el.on("keyup", "#" + pluginName + "-buscaTarea", function (e) {
                //La búsqueda se realizará cuando se pulse la tecla INTRO
                
                if (e.which == 13) {
                    if (this.value != "") {
                        //Quitamos el crebreado
                        $("tr:visible").removeClass("cebreada");

                        //separar el contenido del input
                        var data = this.value;

                        //creamos el objeto línea
                        var linea = $("#" + pluginName + "-tabla").find("tr.linea");

                        //ocultamos todas las líneas y se modifica el icono a no expandido(+)
                        linea.hide();
                        $(linea).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
                        $(linea).find("span[aria-expanded]").attr('aria-expanded', 'false')

                        //Si el input está vacio mostramos solo las líneas de los proyectos económicos
                        if (this.value == "") {
                            $("[class*=PE]").show(function () {
                                cebrear();
                            });

                            return;
                        }


                        //Filtramos recursivamente las lineas para obtener el resultado    
                        linea.filter(function (i, v) {
                            var $t = $(this);
                            if (($t.text().normalize()).toUpperCase().indexOf((data.normalize()).toUpperCase()) > -1) {
                                return true;
                            } else if ($t.hasClass("PE")) {
                                if (($t.attr("id").indexOf((data.normalize()).toUpperCase()) > -1)) return true;
                            } else if ($t.hasClass("T")) {
                                if (($t.attr("id").indexOf((data.normalize()).toUpperCase()) > -1)) return true;
                            }
                            return false;
                        })
                        //Mostrar las lineas coincidentes.    
                        .show();

                        //Mostrar la jerarquia superior de las lineas coincidentes
                        var parent = "";
                        var aParent;
                        $("#" + pluginName + "-tabla").find("tr.linea:visible").each(function (index) {
                            parent = $(this).attr('data-parent');
                            aParent = parent.split(" ");
                            for (var x = 1; x < aParent.length - 1; x++) {
                                var elemento = $("#" + pluginName + "-tabla tr[data-id=' " + aParent[x] + " ']");
                                $(elemento).show();
                                $(elemento).find('.glyphicon-plus').addClass("glyphicon-minus").removeClass("glyphicon-plus");
                                $(elemento).find("span[aria-expanded]").attr('aria-expanded', 'true')

                            }
                        });

                        cebrear();
                    }
                    else {
                        //Si el input está vacio mostramos solo las líneas de los proyectos económicos
                        if (this.value == "") {
                            $("[class*=PE]").show();
                        }
                    }
                }
            });

            //Acción de click en cada línea de la tabla
            $el.on("keypress click", ".nombreLinea > .glyphicon-plus, .nombreLinea > .glyphicon-minus", function (e) {
                //Se expande o se contra según el caso
                controlarExpansion($(this).parent().parent().children());
            });
            
            //Acción de click en cada línea de la tabla
            $el.on("keypress click", ".linea", function (e) {
                //Se marca ele lemento activo
                marcarLinea($(this));
                //Si el elemento es una tarea se habilita el botón de "Seleccionar"
                if ($(this).hasClass("T")) $("#" + pluginName + "-btnSeleccionar").removeAttr('disabled');
                else $("#" + pluginName + "-btnSeleccionar").attr('disabled', 'disabled');
            });

            //Acción de click en cada línea de la tabla
            $el.on("dblclick", ".linea", function (e) {
                if ($(this).hasClass("T")) {
                    marcarLinea($(this));
                    $("#" + pluginName + "-btnSeleccionar").trigger("click");
                }
                  
            });

            //Botón aceptar de la ventana
            $el.find("#" + pluginName + "-btnSeleccionar").on('click', function (e) {
                //Se obtiene el elemento activo
                var $selected = $el.find("#" + pluginName + "-tabla").find("tr.activa");
                //Si no huebira ninguno se muestra el aviso
                if ($selected.length == 0) {
                    IB.bsalert.toastwarning("No se ha seleccionado ninguna tarea");
                    return false;
                }

                var data = {
                    idTarea: $selected.attr("id"),
                    desTarea: $selected.children().attr("title").substr(8)
                }
                hook("onSeleccionar", data);

                hideModal();
            });

            //Botón cancelar de la ventana
            $el.find("#" + pluginName + "-btnCancelar").on('click', function () {
                hideModal();
                hook("onCancelar");
            });

            //Botones de expansión
            $el.on("keypress click", ".btn-blanco", function (e) {
                colorearNiveles(e);
                var idNivel = $(this).attr("id");

                var numLineas = 0;

                //Se crea el objeto línea que contiene todos los elementos tr
                var linea = $("#" + pluginName + "-tabla").find("tr.linea");
                //Se ocultan todas las líneas y se les cambia el icono a no expandido(+)
                linea.hide();
                $(linea).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
                $(linea).find("span[aria-expanded]").attr('aria-expanded', 'false');                

                //Desmarcar línea si hubiera alguna marcada y deshabilitar el boton seleccionar
                desmarcarLinea();

                //Si el nivel de expansión es 1, se muestran los PE
                if (idNivel == "nivel1") {
                    $("[class*=PE]").show();                   
                } else if (idNivel == "nivel2") {//Si el nivel de expansión es 2, se muestran los PE y sus PT
                    $("[class*=PE]").show();
                    $("[class*=PT]").show();
                    $("[class*=PE]").find('.glyphicon-plus').addClass("glyphicon-minus").removeClass("glyphicon-plus");
                    $("[class*=PE]").find("span[aria-expanded]").attr('aria-expanded', 'true');

                } else if (idNivel == "nivel3") {//Si el nivel de expansión es 3, se muestran todas las líneas
                    linea.show();
                    $(linea).find('.glyphicon-plus').addClass("glyphicon-minus").removeClass("glyphicon-plus");
                    $(linea).find("span[aria-expanded]").attr('aria-expanded', 'false');
                }

                cebrear();
                
            });

            //Boton bomba de expansión
            $el.on("keypress click", "#icoBomb", function (e) {
                //Se obtiene el lemento seleccionado y se obtiene su data-id
                var $selected = $el.find("#" + pluginName + "-tabla").find("tr.activa");
                var ID = $selected.attr('data-id');

                //Comprobar que el elemnto no sea una tarea
                if (!$selected.hasClass("T")) {
                    //Se identifican cuales son sus descendientes (las líneas que contengan el ID en el atributo data-parent)
                    $("#" + pluginName + "-tabla tr[data-parent*='" + ID + "']").each(function (index) {
                        //Se visualizan los descendientes y se les modifica el icono a expandido
                        /*$(this).show('fast', function () {
                            cebrear();
                        });*/
                        $(this).show();
                        cebrear();
                        $(this).find('.glyphicon-plus').addClass("glyphicon-minus").removeClass("glyphicon-plus");
                        $(this).find("span[aria-expanded]").attr('aria-expanded', 'true')
                       
                    });

                    //Se modifica su propio icono a expandido (-)
                    $selected.find('.glyphicon-plus').addClass("glyphicon-minus").removeClass("glyphicon-plus");
                    $selected.find("span[aria-expanded]").attr('aria-expanded', 'true')
                  
                }
               
            });

        }

        function showModal() {
            colorearNivel("1");
            //ocultar los campos de criterio de filtrado
            if (options.autoSearch) {
                $el.find(".fk_filter").addClass("hide");
                $el.find(".btn-group").addClass("hide");
                $el.find("#icoBomb").addClass("hide");
                
                $.when(search()).then(function () {                    
                    cebrear();
                    $el.parent().find('.ocultable').attr('aria-hidden', 'true');
                    $el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                    $el.find(".modal").modal('show');
                    IB.procesando.ocultar();
                    
                });
                
            }
            else { //dar el foco al primer campo del filtrado
                $el.find(".fk_filter").removeClass("hide");
                $el.find("#" + pluginName + "-modal").on('shown.bs.modal', function () {
                    $el.find("#" + pluginName + "-txtApellido1").focus()
                })
            }           
            
        }

        function hideModal() {
            $el.parent().find('.ocultable').attr('aria-hidden', 'false');
            $el.find(".modal").modal('hide');
            $el.find("input[type='text']").val("");
            $el.find("#" + pluginName + "-tabla").empty();
        }

        function asignarEstilosPorNivel(elemento, nivel) {
            var estilo;
            switch (nivel) {
                case "1": estilo = "vertical-align: middle; "; break;
                case "2": estilo = "vertical-align: middle !important; padding-left: 15px !important;  "; break;
                case "3": $(elemento).hasClass("T") ? estilo = "vertical-align: middle !important; padding-left: 55px !important; " : estilo = "vertical-align: middle !important; padding-left:35px !important; "; break;
                case "4": estilo = "vertical-align: middle !important; padding-left: 55px !important;"; break;
                case "5": estilo = "vertical-align: middle !important; padding-left: 75px !important; "; break;

            }
            $(elemento).children().first().attr('style', estilo);
        }

        function estadosTarea(item) {
            var estadoTarea = "";

            switch (item.t332_estado)//Estado
            {
                case 0:
                    estadoTarea = " paralizada";
                    descEstado = " Tarea paralizada";
                    bEstadoLectura = true;
                    break;
                case 1://Activo
                    estadoTarea = "";
                    descEstado = "";
                    if (item.t331_obligaest == 1) {
                        estadoTarea = " obligaest";
                        descEstado = " Tarea de estimación obligatoria";
                    }
                    break;
                case 2:
                    estadoTarea = " pendiente";
                    descEstado = " Tarea pendiente";
                    bEstadoLectura = true;
                    break;
                case 3:
                    estadoTarea = " finalizada";
                    descEstado = " Tarea finalizada";
                    if (item.t332_impiap == 0) bEstadoLectura = true;  //si impiap = 0, lectura
                    break;
                case 4:
                    estadoTarea = " cerrada";
                    descEstado = " Tarea cerrada";
                    if (item.t332_impiap == 0) bEstadoLectura = true;
                    break;
                case 5:
                    estadoTarea = " anulada";
                    descEstado = " Tarea anulada";
                    break;
            }
            return estadoTarea;
        }
        
        /**
        * Búsqueda en bloque
        */
        function search() {
            var defer = $.Deferred();
            if (options.searchParams.tipoBusqueda == "tareasEnBloque" || options.searchParams.tipoBusqueda == "tareasAgendaEnBloque") $el.find("#" + pluginName + "-tabla").empty();

            var serviceUrl = IB.vars.strserver + "Capa_Presentacion/" + options.modulo + "/Services/BuscadorTareas.asmx";

            var n = window.setTimeout(function () { $el.find("#" + pluginName + "-tabla").html("Buscando ...") }, 500)

            var payload = $.extend({}, options.searchParams)            

            IB.DAL.post(serviceUrl, options.searchParams.tipoBusqueda, payload, null,
                function (data) {
                    var html = "";
                    if (data.length == 0) {
                        html = "No se han encontrado tareas.";
                        $el.find("#" + pluginName + "-tabla").html(html);
                        defer.resolve();
                    } else {

                        html = "<thead>" +
                                    "<th class='text-center bg-primary'>Denominación</th>" +
                                "</thead>" +
                                "<tbody  style='overflow-y: auto;max-height: 40vh;'>";

                        data.forEach(function (item) {
                            var tipo;
                            var dataParent;
                            var content;
                            if (item.Tipo == "PSN") {
                                tipo = "PE";
                                content = "<b>Proyecto:</b> " + item.denominacion + "<br /> <b>Responsable:</b> " + item.responsable + "<br /> <b>C.R.:</b> " + item.t303_denominacion + "<br /> <b>Cliente:</b> " + item.t302_denominacion;

                                html += "<tr class='linea PE' id='" + item.t301_idproyecto + "' data-id=' PE" + item.t305_idproyectosubnodo + " ' data-parent='' data-sPSN='" + item.t305_idproyectosubnodo + "' data-nivel='" + item.nivel + "'>" +
                                        "<td class='nombreLinea' style='vertical-align: middle;' tabindex='0' role='button' title='Proyecto Económico - " + item.t301_idproyecto + "'>" +
                                            "<span class='glyphicon-plus'>" +
                                                "<span aria-hidden='true' class='fa fa-diamond fa-diamond-verde'>" +
                                                "</span>" +
                                            "</span>" +
                                            "<span aria-hidden='true' data-placement='top' data-toggle='popover' data-content='" + content + "' title='<b>Información</b>'>"  + " " + accounting.formatNumber(item.t301_idproyecto, 0) + " - " + item.t305_seudonimo + "</span>" +
                                            "<span class='sr-only' role='button' aria-expanded='false'>Proyecto Económico - " + item.t305_seudonimo + " (Nivel 1)</span>" +
                                        "</td>" +
                                        "</tr>"

                            } else if (item.Tipo == "PT") {
                                tipo = "PT";
                                html += "<tr class='linea PT' id='" + item.t331_idpt + "' data-id=' PT" + item.t331_idpt + " ' data-parent=' PE" + item.t305_idproyectosubnodo + " ' data-idPSN ='" + item.t305_idproyectosubnodo + "' data-nivel='" + item.nivel + "'>" +
                                        "<td class='nombreLinea' style='vertical-align: middle;' tabindex='0' role='button' title='Proyecto Técnico - " + item.denominacion + "'>" +
                                            "&nbsp&nbsp" +
                                            "<span class='glyphicon-plus'>" +
                                                "<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'>" +
                                                    "<i class='fa fa-circle fa-stack-1x circuloLinea'></i>" +
                                                    "<i class='fa fa-stack-1x letraLinea'>P</i>" +
                                                "</span>" +
                                            "</span>" +
                                            "<span aria-hidden='true'>" + item.denominacion + "</span>" +
                                           " <span class='sr-only' role='button' aria-expanded='false'>Proyecto Técnico - " + item.denominacion + " (Nivel 2)</span>" +
                                          "</td>" +
                                        "</tr>"

                            } else if (item.Tipo == "A") {
                                tipo = "Actividad";
                                (item.t334_idfase != null && item.t334_idfase != 0) ? dataParent = "data-parent=' F" + item.t334_idfase + " PT" + item.t331_idpt + " PE" + item.t305_idproyectosubnodo + " '": dataParent = "data-parent=' PT" + + item.t331_idpt + " PE" + item.t305_idproyectosubnodo + " '";
                                html += "<tr class='linea A' id='" + item.t335_idactividad + "' data-id=' A" + item.t335_idactividad + " '" + dataParent + " data-nivel='" + item.nivel + "'>" +
                                            "<td class='nombreLinea A' role='button' tabindex='0' title='" + tipo + " - " + item.denominacion + "'>" +
                                                "<span class='glyphicon-plus'>" +
                                                    "<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'>" +
                                                        "<i class='fa fa-circle fa-stack-1x circuloLinea'></i>" +
                                                        "<i class='fa fa-stack-1x letraLinea'>A</i>" +
                                                    "</span>" +
                                                "</span>" +
                                                "<span aria-hidden='true'>" + item.denominacion + "</span>" +
                                                " <span class='sr-only' role='button' aria-expanded='false'>" + tipo + " - " + item.denominacion + " (Nivel 3)</span>" +
                                            "</td>" +
                                        "</tr>"

                            } else if (item.Tipo == "F") {
                                tipo = "Fase";
                                html += "<tr class='linea F' id='" + item.t334_idfase + "' data-id=' F" + item.t334_idfase + " ' data-parent=' PT" + item.t331_idpt + " PE" + item.t305_idproyectosubnodo + " ' data-nivel='" + item.nivel + "'>" +
                                           "<td class='nombreLinea F' role='button' tabindex='0' title='" + tipo + " - " + item.denominacion + "'>" +
                                            "<span class='glyphicon-plus'>" +
                                                "<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'>" +
                                                    "<i class='fa fa-circle fa-stack-1x circuloLinea'></i>" +
                                                    "<i class='fa fa-stack-1x letraLinea'>F</i>" +
                                                "</span>" +
                                            "</span>" +
                                            "<span aria-hidden='true'>" + item.denominacion + "</span>" +
                                            " <span class='sr-only' role='button' aria-expanded='false'>" + tipo + " - " + item.denominacion + " (Nivel 3)</span>" +
                                           "</td>" +
                                        "</tr>"
                            } else if (item.Tipo == "T") {

                                tipo = "Tarea";
                                if ((item.t334_idfase == null && item.t335_idactividad == null) || (item.t334_idfase == 0 && item.t335_idactividad == 0)) dataParent = "data-parent=' PT" +item.t331_idpt + " PE" +item.t305_idproyectosubnodo + " '";
                                else if ((item.t334_idfase != null && item.t335_idactividad == null) || (item.t334_idfase != 0 && item.t335_idactividad == 0)) dataParent = "data-parent=' F" +item.t334_idfase + " PT" +item.t331_idpt + " PE" +item.t305_idproyectosubnodo + " '";
                                else if ((item.t334_idfase == null && item.t335_idactividad != null) || (item.t334_idfase == 0 && item.t335_idactividad != 0)) dataParent = "data-parent=' A" +item.t335_idactividad + " PT" +item.t331_idpt + " PE" +item.t305_idproyectosubnodo + " '";
                                else dataParent = "data-parent=' A" + item.t335_idactividad + " F" + item.t334_idfase + " PT" + item.t331_idpt + " PE" + item.t305_idproyectosubnodo + " '";

                                html += "<tr class='linea T' id='" + item.t332_idtarea + "' data-id=' T" + item.t332_idtarea + " '" + dataParent + "' data-nivel='" + item.nivel + "'>" +
                                            "<td class='nombreLinea T' role='button' tabindex='0' title='" + tipo + " - " + item.denominacion + "'>" +
                                                "<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'>" +
                                                    "<i class='fa fa-circle fa-stack-1x circuloLinea'></i>" +
                                                    "<i class='fa fa-stack-1x letraLinea'>T</i>" +
                                                "</span>" +
                                                "<span class='" + estadosTarea(item) + "' aria-hidden='true'>" + accounting.formatNumber(item.t332_idtarea, 0) + " - " + item.denominacion + "</span>" +
                                                "<span class='sr-only' role='button' aria-expanded='false'>" + tipo + " - " + item.denominacion + " (Nivel 3)</span>" +
                                            "</td>" +
                                        "</tr>"

                            }
                            
                        });
                        html += "</tbody>";
                        //Para cuandos e necesite la leyenda
                        html += "<tfoot >" +
                                    "<tr>" +
                                        "<td id='leyendas' class='leyenda'>" +
                                            "<span class='fa fa-diamond fa-diamond-verde spanLeyenda'></span><span>Abierto </span>";
                                            if(options.searchParams.tipoBusqueda == "tareasAgendaEnBloque"){
                                                html += "<span class='pendiente spanLeyenda' style='float:right;'>Pendiente</span>" +
                                                        "<span class='paralizada spanLeyenda' style='float:right;'>Paralizada</span>"
                                            }
                                            html +=
                                            "<span class='finalizada spanLeyenda' style='float:right;'>Finalizada</span>" +
                                            "<span class='cerrada spanLeyenda' style='float:right;'>Cerrada</span>" +
                                            "<span class='obligaest spanLeyenda' style='float:right;'>Estimación obligatoria</span>" +
                                        "</td>" +
                                    "</tr> " +
                                "</tfoot>";
                        $el.find("#" + pluginName + "-tabla").html(html);
                        $el.find(".fk_filter").removeClass("hide");
                        $el.find(".btn-group").removeClass("hide");
                        $el.find("#icoBomb").removeClass("hide");

                        if (!indicadores.i_dispositivoTactil) {
                            $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true });
                        }
                        
                        //Se ocultan todas las líneas y se muestra los PE únicamente
                        var linea = $("#" + pluginName + "-tabla").find("tr.linea");                        
                        linea.hide();

                        //Se asignan los estilos a los diferentes niveles
                        var nivel;
                        for (var x = 0; x < linea.length; x++) {
                            nivel = $(linea[x]).attr('data-nivel');
                            asignarEstilosPorNivel(linea[x], nivel);
                        }
                        
                        $("[class*=PE]").show();
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

                $el.off("click", ".list-group-item");
                $el.find("#" + pluginName + "-btnSeleccionar").off('click');
                $el.find("#" + pluginName + "-btnCancelar").off('click');
                $el.find("#" + pluginName + "-btnObtener").off('click');
                $el.find(".fk_filter input").off('keyup');
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
        searchParams: { },
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
       
    // Colorear niveles
    var colorearNiveles = function (e) {

        var srcobj;
        srcobj = e.target ? e.target : e.srcElement;

        //El elemento target no es el mismo si se pulsa el el botón o el span que hay dentro del botón, por lo que se comprueba y se coge siempre el botón
        if (!$(srcobj).is(":button")) srcobj = $(srcobj).parent();

        var nivel = $(srcobj).attr("data-level");
        colorearNivel(nivel);
    }

    function colorearNivel(nivel) {
        switch (nivel) {
            case "1":
                $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra2").addClass("nivelGris").removeClass("nivelVerde");
                $("#barra3").addClass("nivelGris").removeClass("nivelVerde");
                break;
            case "2":
                $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra2").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra3").addClass("nivelGris").removeClass("nivelVerde");
                break;
            case "3":
                $("#barra1").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra2").addClass("nivelVerde").removeClass("nivelGris");
                $("#barra3").addClass("nivelVerde").removeClass("nivelGris");
                break;
        }
    }
    function cambiarIcono(elemento) {
        //Modifica el atributo de expandido de la línea pulsada
        $(elemento).find("span[aria-expanded]").attr('aria-expanded', 'true');

        //Cambia el glyphicon
        $(elemento).children().first().toggleClass("glyphicon-minus", true).removeClass("glyphicon-plus");
        
    }   

    function controlarExpansion(elemento) {
        //Cogemos el data-id de la línea pulsada
        var ID = $(elemento).parent().attr('data-id');

        if ($(elemento).children().hasClass('glyphicon-plus')) {

            //Abre los hijos del nodo pulsado
            $("#" + pluginName + "-tabla tr[data-parent^='" + ID + "']").show('fast', function () {
                cebrear();
            });
            
            //Modifica el atributo de expandido de la línea pulsada
            $(elemento).find("span[aria-expanded]").attr('aria-expanded', 'true');

            //Cambia el glyphicon
            $(elemento).children().first().toggleClass("glyphicon-minus", true).removeClass("glyphicon-plus");
           

        }
        else {

            //Cierra los descendientes del nodo pulsado
            $("#" + pluginName + "-tabla tr[data-parent*='" + ID + "']").hide('fast', function () {
                cebrear();
            });

            //Modifica el atributo de expandido de la línea pulsada
            $(elemento).find("span[aria-expanded]").attr('aria-expanded', 'false');
            //Modifica el atributo de expandido de los descendientes del nodo pulsado
            $("#" + pluginName + "-tabla tr[data-parent*='" + ID + "']").find("span[aria-expanded]").attr('aria-expanded', 'false');

            //Cambia el glyphicon de los descendientes del nodo pulsado y de sí mismo
            $("#" + pluginName + "-tabla tr[data-parent*='" + ID + "']").find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
            $(elemento).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
            
        }

    };

    
    //Marcado de línea activa
    marcarLinea = function (thisObj) {        
        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        $("#" + pluginName + "-tabla tr.activa").removeClass('activa');
        $(thisObj).addClass('activa');     


        //Eliminamos el texto ' - Seleccionado' del elemento seleccionado anterior. Las posiciones 0 y 1 contienen el tipo de linea y su descripción.
        $('span:contains("- Seleccionado")').each(function () {
            $(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1]);
        })

        //Añadimos el texo '- Seleccionado' al elemento seleccionado actualmente
        $(thisObj).children().children(":nth-child(3n)").text($(thisObj).children().children(":nth-child(3n)").text() + ' - Seleccionado');
       
    }

    desmarcarLinea = function (){
        $("#" + pluginName + "-tabla tr.activa").removeClass('activa');
        $("#" + pluginName + "-btnSeleccionar").attr('disabled','disabled');
    }

})(jQuery);