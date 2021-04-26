var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view = (function () {
    
    var dom = {
        divAyudaProfesionales: $("#divAyudaProfesionales"),
        listaPool: $("#ulPool"),
        listaPoolElementos: $("#ulPool li"),
        tblAreaSubarea: $("#tblAreaSubarea"),
        btnGrabar : $("#btnGrabar")
    }

    var selectores = {
        //Selectores     
        fk_check: ".fk_check",
        fk_selTodos: ".fk_selTodos"
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    //live events
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function detachEvents() {
        $(selector).off(event, callback);
    }

    //detach live events
    function detachLiveEvents(event, selector, callback) {
        $(document).off(event, selector, callback);
    }

    function init() {        
        initDatatable();        
    }

    //Eventos
    attachLiveEvents("click", selectores.fk_selTodos, fk_selTodos_onClick);


    //Ayuda de profesionales
    $("#spanSeleccionarProf").on("click", function () {
        var lstSeleccionados = []
        dom.listaPool.find("li").each(function () {
            lstSeleccionados.push({ t001_idficepi: $(this).attr("data-idficepi"), profesional: $(this).html(), estado: $(this).attr("data-estado") })
        })
        dom.divAyudaProfesionales.buscaprofmulti("option", "lstSeleccionados", lstSeleccionados);
        dom.divAyudaProfesionales.buscaprofmulti("show")
    });

    dom.divAyudaProfesionales.buscaprofmulti({
        titulo: "Selección de profesionales",
        tituloContIzda: "Profesionales",
        tituloContDcha: "Profesionales seleccionados",
        notFound: "No se han encontrado resultados.",
        modulo: "SIC",
        tipoAyuda: "GeneralFicepi",
        autoSearch: false,
        autoShow: false,
        eliminarExistentes: true,
        onAceptar: function (data) {
            var html = "";
            
            //Recorremos la lista de seleccionados del plugin
            data.forEach(function (item) {
                html += "<li class='list-group-item' data-estado='N' data-idficepi='" + item.t001_idficepi + "'>" + item.profesional + "</li>";
            });

            dom.listaPool.html(html);            
        }
    });
    
    

    function initDatatable() {

        $('#tblAreaSubarea').DataTable({
            "language": {
                "url": "../../../../plugins/datatables/Spanish.txt",
            },
            "language": {
                "emptyTable": "No hay resultados"
            },
            "procesing": true,
            "paginate": false,
            "responsive": false,
            "autoWidth": false,
            "order": [],
            "scrollY": "35vh",
            "scrollCollapse": false,
            "info": false,
            "searching": false,
            "destroy": true,
            "ajax": {
                "url": "Default.aspx/getareassubareasppl",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify() },
                "dataSrc": function (data) {
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
                }
            },

            "columns": [             
              { "data": "ta200_denominacion" },
              { "data": "ta201_denominacion" },
              { "data": null },
              { "data": "profesional" }
              
            ],

            "columnDefs": [
                
                 {
                     "targets": 3,                     
                     "orderable": false,
                     "render": {
                         "display": function (data, type, row, meta) {                             
                             var html = "";
                             var datos = "";
                             if (row.profesional != null) {
                                 var persona = row.profesional.split("|");
                                 for (var i = 0; i < persona.length - 1; i++) {
                                     datos = persona[i].split("@#@");
                                     html+= "<span data-original='true' data-t001_idficepi='" + datos[0] + "'>" + datos[1] + "</span></br>";
                                 }
                                 
                                 return html;
                                
                             }
                             else
                                 return "";
                         }
                     }
                 },

                {
                   "targets": 2,
                   "className": "text-center",
                   "orderable": false, 
                   "render": {
                       "display": function (data, type, row, meta) {                           
                            return "<input class='fk_check' type='checkbox' />";
                           
                       }
                   }
               }
            ],

            "createdRow": function (row, data, index) {
                //fila creada
                $(row).attr("data-ta201_idsubareapreventa", data.ta201_idsubareapreventa);

            }

        })
    }

    function renderprofesionales(data) {
        var datos = JSON.parse(data);
        var html = "";
        $(datos).each(function (index, element) {
            html += "<li class='list-group-item' data-estado='E'>" + element.profesional +"</li>";
        })

        dom.listaPool.html(html);
    }

    function fk_selTodos_onClick() {
        if ($(this).is(":checked")) {
            $(this).prop("checked", true);
            $(".fk_check").prop("checked", true);
        }
        else {
            $(this).prop("checked", false);
            $(".fk_check").prop("checked", false);
        }
    }
   
    $("#btnReemplazar").on("click", function () {        
        if ($(".fk_check:checked").length == 0) {
            IB.bsalert.toastdanger("No has seleccionado ninguna fila.");
            return false;
        }

        if ($("#ulPool li").length == 0) {
            IB.bsalert.toastdanger("No hay ningún profesional para añadir.");
            return false;
        }

        var html = "";
        $("#ulPool li").each(function (index, element) {
            html += "<span data-t001_idficepi='" + this.getAttribute("data-idficepi") + "'>" + this.innerText + "</span></br>";
        })

        //Quitamos el check de los elementos actualizados        
        $(".fk_check:checked").parent().next().html(html);
        $(".fk_check:checked").parent().parent().attr("data-cambios", true);
        $(".fk_check:checked").parent().prev().css("color", "blue");        
    })

    $("#btnAgregar").on("click", function () {
        if ($(".fk_check:checked").length == 0) {
            IB.bsalert.toastdanger("No has seleccionado ninguna fila.");
            return false;
        }

        if ($("#ulPool li").length == 0) {
            IB.bsalert.toastdanger("No hay ningún profesional para añadir.");
            return false;
        }

        var inputs = $(".fk_check:checked");
        var profesionales = "";
        var listaDerecha = $("#ulPool li");
        var html = "";
        var _encontrado = false;

        $(inputs).each(function () {
            profesionales = $(this).parent().next().find("span");

            if (profesionales.length == 0) {
                listaDerecha.each(function () {
                    html += "<span data-t001_idficepi='" + this.getAttribute("data-idficepi") + "'>" + this.innerText + "</span></br>";
                })
            }

            else {
                listaDerecha.each(function () {
                    for (var i = 0; i < profesionales.length; i++) {
                        if (profesionales[i].getAttribute("data-t001_idficepi") == this.getAttribute("data-idficepi"))
                        {
                            _encontrado = true;
                        }
                    }

                    if (!_encontrado) {
                        html += "<span data-t001_idficepi='" + this.getAttribute("data-idficepi") + "'>" + this.innerText + "</span></br>";
                    }
                })
            }
            
            $(this).parent().next().append(html)
            $(this).parent().parent().attr("data-cambios", true);
            $(this).parent().prev().css("color", "blue");
            html = "";
        })
    })
    
    $("#btnEliminar").on("click", function () {
        if ($(".fk_check:checked").length == 0) {
            IB.bsalert.toastdanger("No has seleccionado ninguna fila.");
            return false;
        }

        if ($("#ulPool li").length == 0) {
            IB.bsalert.toastdanger("No hay ningún profesional para añadir.");
            return false;
        }

        var inputs = $(".fk_check:checked");
        var profesionales = "";
        var listaDerecha = $("#ulPool li");
        var html = "";
        var _encontrado = false;

        $(inputs).each(function () {
            profesionales = $(this).parent().next().find("span");

            listaDerecha.each(function () {
                for (var i = 0; i < profesionales.length; i++) {
                    if (profesionales[i].getAttribute("data-t001_idficepi") == this.getAttribute("data-idficepi")) {
                        profesionales[i].setAttribute("data-eliminar", true);
                    }
                }

            })

            $("span[data-eliminar='true']").next().remove();
            $("span[data-eliminar='true']").remove();

            //Quitamos el check de los elementos actualizados
            $(".fk_check:checked").parent().parent().attr("data-cambios", true);
            $(".fk_check:checked").parent().prev().css("color", "blue");
        })


    })

    function refreshDatatable() {        
        var oDataTable = $('#tblAreaSubarea').DataTable();

        oDataTable.clear().draw();
        oDataTable.ajax.reload(null, true);
    }
   
    $("#spanCopiarProf").on("click", function () {
        $("#modal-copiarProf").modal("show");

        var filasConProfesionales = $("#tblAreaSubarea tr td span[data-original='true']").parent().parent();
        //Clonamos las filas
        var filasclonadas= filasConProfesionales.clone();

        $("#tbdCopiarProf").html(filasclonadas);

        //Ocultamos las columnas del input y los profesionales
        $(filasclonadas).find("td:nth-child(n + 3)").css("display", "none");
        
    })

    //Selección simple
    $('body').on('click', '#tbdCopiarProf tr', function (e) {
        $('#tbdCopiarProf tr').removeClass('active');
        $(this).addClass('active');
    });

    $("#btnAceptarModalCopiarProf").on("click", function () {
        var filaSeleccionada = $('#tbdCopiarProf tr.active');
        var prof = filaSeleccionada.find("span[data-original='true']");
        var htmlCopiarProf = "";

        $(prof).each(function (item) {
            htmlCopiarProf += "<li class='list-group-item' data-estado='N' data-idficepi='" + this.getAttribute("data-t001_idficepi") + "'>" + this.innerText+ "</li>";
        })
        
        dom.listaPool.html(htmlCopiarProf);

        $("#modal-copiarProf").modal("hide");

    })

    function obtenerDatosGrabar() {
        var elementos = $("[data-cambios='true']");
        var personas = $("[data-cambios='true']").find("td:nth-child(4) span");

        var lista = [];

        if (elementos.length == 0) {
            IB.bsalert.toastdanger("No hay nada para grabar.");
            return false;
        }

        elementos.each(function () {
            if ($(this).find(".fk_check:checked").length) {
                var prof = $(this).find("td:nth-child(4) span");
                var oLista = new Object();
                oLista.profesionales = "";
                oLista.ta201_idsubareapreventa = $(this).attr("data-ta201_idsubareapreventa");
                for (var i = 0; i < prof.length; i++) {
                    oLista.profesionales += prof[i].getAttribute("data-t001_idficepi") + "@#@";
                }

                lista.push(oLista);
            }
            
        });

        return lista;
    }

    function comprobarCambios() {
        if ($("#tblAreaSubarea tr[data-cambios='true']").length > 0)
            return true;
    }

    return {
        init: init,        
        dom: dom,        
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        renderprofesionales: renderprofesionales,
        obtenerDatosGrabar: obtenerDatosGrabar,
        refreshDatatable: refreshDatatable,
        comprobarCambios: comprobarCambios
    }

})();
