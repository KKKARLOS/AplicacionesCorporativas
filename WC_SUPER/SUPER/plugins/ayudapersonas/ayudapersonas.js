var table;

(function ($) {


    $.ayudaPersonas = function(element, options) {


            var settings = $.extend({
                modal: true,          //abrir modal con el contenido true/false
                titulo: '',           //Título a mostrar en caso de que se vaya a abrir una modal
                autoSearch: false,    //Ejecutar la búsqueda al mostrar el plugin (y esconder los campos de filtrado)
                autoShow: false,
                aceptar: function () { },
                cancelar: function () { }
            }, options);


            //Se obtiene el identificador de la capa buscadorUsuario para volcar el html
            var uniqueId = $(element).attr("id");

            //Si el parámetro de entrada es (modal==true), se genera el código html de una modal
            if (options.modal) {
                pintarModal(element);
                //pintarContenido(idBuscador);
                if (options.autoShow) mostrarModal(element);

            }
            else
                pintarContenido(uniqueId);

            $.fn.ayudaPersonas.open = function () {
                mostrarModal(element);
            }

            function pintarModal(that) {
                var $div = $(that).append('<div class="modal fade" role="dialog" tabindex="-1" title="' + settings.titulo + '">' +
                                             '<div class="modal-dialog" role="dialog">' +
                                                '<div class="modal-content">' +
                                                    ' <div class="modal-header">' +
                                                        '<button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>' +
                                                        '<span class="modal-title">' + settings.titulo + '</span>' +
                                                    '</div>' +
                                                    '<div class="modal-body">' +
                                                    '</div>' +
                                                    '<div class="modal-footer">' +
                                                        '<b><button class="btn btn-primary fk_btnAceptar">Aceptar</button></b>  ' +
                                                        '<b><button class="btn btn-default fk_btnCancelar">Cancelar</button></b>' +
                                                    '</div>' +
                                                '</div>' +
                                            '</div>' +
                                        '</div>');

                //Botón aceptar de la ventana
                $div.find(".fk_btnAceptar").on('click', function (e) {
                    if ($("#listaProfesionales tr.selected").length == 0) {
                        IB.bsalert.toastwarning("No has seleccionado ningún profesional");
                        return false;
                    }
                    if (settings.modal) {
                        $div.find(".modal").modal('hide');
                    }
                    settings.aceptar.call(table.rows('.selected').data()[0]);
                });

                //Botón cancelar de la ventana
                $div.find(".fk_btnCancelar").on('click', function () {
                    if (settings.modal) {
                        $div.find(".modal").modal('hide');
                    }
                    //options.cancelar('acción de cancelar');
                    settings.cancelar.call();
                });


            };

            // Visualiza la ventana modal
            function mostrarModal(that) {
                $('.ocultable').attr('aria-hidden', 'true');

                $(that).find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $(that).find(".modal").modal('show');
            }

            //Función que pinta el formulario de buscador de personas
            function pintarContenido(uniqueId) {
                $('#' + uniqueId).html('');
                $('#' + uniqueId).append('<div id="txtBusqueda" class="row">' +
                                            '<div class="col-xs-12  col-md-3">' +
                                                '<label for="txtAp1Prof" id="lblAp1Prof" class="col-xs-12">Apellido 1º</label>' +
                                                '<div class="col-xs-12">' +
                                                    '<input id="txtAp1Prof" name="" type="text" class="input-md" value="" />' +
                                                '</div>' +
                                            '</div>' +
                                            '<div class="col-xs-12  col-md-3">' +
                                                '<label for="txtAp2Prof" id="lblAp2Prof" class="col-xs-12">Apellido 2º</label>' +
                                                '<div class="col-xs-12">' +
                                                    '<input id="txtAp2Prof" name="" type="text" class="input-md" value="" />' +
                                                '</div>' +
                                            '</div>' +
                                            '<div class="col-xs-12 col-md-3">' +
                                                '<label for="txtNomProf" id="lblNomProf" class="col-xs-12">Nombre</label>' +
                                                '<div class="col-xs-12">' +
                                                    '<input id="txtNomProf" name="" type="text" class="input-md" value="" />' +
                                                '</div>' +
                                            '</div>' +
                                            '<div class="col-xs-12 col-md-offset-1 col-md-1">' +
                                                '<button style="margin-top: 20px" class="btn btn-primary btn-xs obtenerPersonas">Obtener</button>' +
                                            '</div>' +

                                            '<div class="col-xs-12 col-md-3 checkbox"/>' +
                                           '</div>' +
                                           '<div class="row">' +
                                                '<div id="divProfesionales" class="text-left col-xs-12">' +
                                                '</div>' +
                                            '</div>');


                if (options.tipoCheck == 'T') {
                    //TODO: Si el perfil de usuario es GR se habilita el check
                    //Obtener el perfil del array de datos de sesion
                    $('#' + uniqueId + ' .checkbox').append('<label for="chkTodos">' +
                                                            '<input type="checkbox" disabled/>Mostrar todos' +
                                                           '</label>');
                } else if (options.tipoCheck == 'B') {
                    $('#' + uniqueId + ' .checkbox').append('<input type="checkbox">Mostrar bajas');

                }

                if (!options.modal) {
                    $('#' + uniqueId).append('<div class="row">' +
                                               '<button id="btnAceptar" class="btn btn-primary">Aceptar</button>' +
                                             '</div>');

                }
            }

            // Obtener profesionales
            $('#' + uniqueId + ' .obtenerPersonas').on('click', function (e) {
                obtenerDatos();
            });

            function obtenerDatos() {
                var nombre = $('#txtNomProf').val();
                var apellido1 = $('#txtAp1Prof').val();
                var apellido2 = $('#txtAp2Prof').val();

                IB.procesando.mostrar();
                var d1 = $.Deferred();
                var d2 = $.Deferred();
                var payload = { tipo: 'buscador', codUsu: 65050, idficepi: 4969, perfil: 'A', apellido1: apellido1, apellido2: apellido2, nombre: nombre, foraneo: true };

                try {

                    console.log("loadUnidades");

                    //var payload = { tipo: "unidad_preventa", filtrarPor: null }
                    //IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Services/BuscadorPersonas.asmx", "ObtenerProfesionales",
                    //datos de cabecera
                    //IB.DAL.post(null, "getProfesionales", payload, null,
                    IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Services/BuscadorPersonas.asmx", "ObtenerProfesionales", payload, null,
                        function (data) {
                            console.log(data);
                            cargarDatosResultado(data);
                            d1.resolve();
                        },
                        function (ex, status) {
                            //hacer todo en caso de que falle
                            //...
                            //...
                            IB.bserror.error$ajax(ex, status)
                            d1.reject();
                        }
                    );
                } catch (e) {
                    IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
                }

            }

            function cargarDatosResultado(data) {
                $('#' + uniqueId + ' #divProfesionales').empty();
                $('#' + uniqueId + ' #divProfesionales').append('<hr /><table id="listaProfesionales" class="display compact margin-bottom">' +
                                                                '<thead>' +
                                                                        '<tr>' +
                                                                            '<th>Tipo</th>' +
                                                                            '<th>Profesional</th>' +
                                                                        '</tr>' +
                                                                '</thead>' +
                                                            '</table>' +
                                                            '<div id="divLeyenda"/>' +
                                                            '</div>');

                //Se inicializa el DataTable de profesionales
                if (typeof table != "undefined") table.destroy();
                initDataTable(data, options.tipoLeyenda);
                pintarDivLeyenda(options.tipoLeyenda);
                IB.procesando.ocultar();

            }

            function pintarDivLeyenda(tipoLeyenda) {
                /* $('#divIconoUser').append('<span class="fa fa-user azul"></span><span>Empleado interno&nbsp;&nbsp;</span>'+
               '<span class="fa fa-user externo"></span><span>Colaborador externo</span>');*/
                $('#divLeyenda').append('<img alt="Empleado interno" id="logoUsuInterno" src="/Images/imgUsuIVM.gif" class="img-responsive"><span>&nbsp;Interno&nbsp;&nbsp;</span>' +
                                            '<img alt="Empleado externo" id="logoUsuExterno" src="/Images/imgUsuEVM.gif" class="img-responsive"><span>&nbsp;Externo&nbsp;&nbsp;</span>');

                if (tipoLeyenda == "F") {
                    $('#divLeyenda').append('<img alt="Empleado foráneo" id="logoUsuExterno" src="/Images/imgUsuFVM.gif" class="img-responsive"><span>Foráneo&nbsp;&nbsp;</span>');

                }

            }

            //para que al cerrar la modal los elementos de la pantalla principal estén visibles
            $(document).on('hidden.bs.modal', '#buscProfesional', function () {
                $('.ocultable').attr('aria-hidden', 'false');
                $('#' + uniqueId).html('');
            });

            //Inicializa el datatable
            function initDataTable(dataSource, tipoLeyenda) {
                table = $("#listaProfesionales").DataTable({

                    data: dataSource,

                    columns: [
                             { "data": 'tipo', "width": "10px", "title": "Tipo" },
                             { "data": 'PROFESIONAL' },
                    ],
                    columnDefs: [
                        {
                            "targets": 0,
                            "className": "text-left",
                            "render": {
                                "display": function (data, type, row, meta) {
                                    if (tipoLeyenda == 'F') {
                                        if (row.t001_sexo == 'M') {
                                            if (row.tipo == 'P' || row.tipo == 'N') {
                                                return '<img alt="Empleado interno" src="/Images/imgUsuIM.gif" class="img-responsive">';
                                            } else if (row.tipo == 'F') {
                                                return '<img alt="Empleado foráneo"  src="/Images/imgUsuFM.gif" class="img-responsive">';

                                            } else return '<img alt="Empleado externo" src="/Images/imgUsuEM.gif" class="img-responsive">';
                                        } else {
                                            if (row.tipo == 'P' || row.tipo == 'N') {
                                                return '<img alt="Empleado interno" src="/Images/imgUsuIV.gif" class="img-responsive">';
                                            } else if (row.tipo == 'F') {
                                                return '<img alt="Empleado foráneo"  src="/Images/imgUsuFV.gif" class="img-responsive">';

                                            } else return '<img alt="Empleado externo" src="/Images/imgUsuEV.gif" class="img-responsive">';
                                        }
                                    } else if (tipoLeyenda == 'CR') {//TODO
                                    } else {
                                        if (row.t001_sexo == 'M') {
                                            if (row.tipo == 'P' || row.tipo == 'N') {
                                                return '<img alt="Empleado interno" src="/Images/imgUsuIM.gif" class="img-responsive">';
                                            } else return '<img alt="Empleado externo" src="/Images/imgUsuEM.gif" class="img-responsive">';
                                        } else {
                                            if (row.tipo == 'P' || row.tipo == 'N') {
                                                return '<img alt="Empleado interno" src="/Images/imgUsuIV.gif" class="img-responsive">';
                                            } else return '<img alt="Empleado interno" src="/Images/imgUsuEV.gif" class="img-responsive">';
                                        }
                                    }
                                }
                            }
                        },

                         {
                             "targets": 1
                         }
                    ],

                    scrollY: "200px",
                    scrollCollapse: true,
                    paging: false,
                    language: { "url": 'plugins/datatables/Spanish.txt' },
                    bInfo: false
                });

            }
        }

    $.fn.ayudaPersonas = function (options) {

        return this.each(function () {
            if (undefined == $(this).data('ayudaPersonas')) {
                var plugin = new $.ayudaPersonas(this, options);
                $(this).data('ayudaPersonas', plugin);
            }
        });

    }
    

    $(document).on('click', '#listaProfesionales tbody tr', function (e) {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });



    function ProfesionalFiltro(nombre, apellido1, apellido2) {
        this.nombre = nombre;
        this.apellido1 = apellido1;
        this.apellido2 = apellido2;
    }

})(jQuery);
