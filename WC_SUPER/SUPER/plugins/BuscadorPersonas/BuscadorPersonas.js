var table_buscaPersonas;

(function ($) {
    $.fn.extend({
        buscadorPersonas: function (options) {
            // Parámetros de entrada que puede recibir el buscador
            var defaults = {
                modal: '', //true:para que el plugin cargue el contenido en una modal / false:para que el contenido se cargue en la propia pantalla
                titulo: '', //Título a mostrar en caso de que se vaya a abrir una modal
                tipoBusqueda: '', //PROFESIONALES:mostrar todos; RESPONSABLES_PROYECTO:mostrar bajas;...
                aceptar: '', //Función que se ejecutará al darle al botón "aceptar"
                cancelar: '' //Función que se ejecutará al darle al botón "cancelar"
            }
                        
            var tipoProfesionales = "PROFESIONALES";
            var tipoResponsablesProyecto = "RESPONSABLES_PROYECTO";
            var tipoUsuarios="USUARIOS"
            var idBuscador = "buscPer";
            var indicadores = {
                i_dispositivoTactil: false
            }

            var tipoBusquedaParam = tipoProfesionales;
            if (options.tipoBusqueda != null) {
                if (options.tipoBusqueda.toUpperCase() != tipoProfesionales &&
                    options.tipoBusqueda.toUpperCase() != tipoResponsablesProyecto &&
                    options.tipoBusqueda.toUpperCase() != tipoUsuarios) {
                    IB.bsalert.fixedAlert("danger", "Error", "No se ha definido una búsqueda válida");
                    return;
                }
                else tipoBusquedaParam = options.tipoBusqueda.toUpperCase();
            }

            //Se obtiene el identificador de la capa buscadorUsuario para volcar el html
            var uniqueId = $(this).attr('id');
            
            //Si el parámetro de entrada es (modal==true), se genera el código html de una modal
            if (options.modal) {
                pintarModal(uniqueId);
                pintarContenido(idBuscador);
                mostrarModal();

            } else pintarContenido(uniqueId);            

            function pintarModal(uniqueId) {
                $('#' + uniqueId).append('<div class="modal fade" id="buscProfesional" role="dialog" tabindex="-1" title="::: SUPER ::: - ' + options.titulo + '">' +
                                             '<div class="modal-dialog" role="dialog">' +
                                                '<div class="modal-content">' +
                                                    ' <div class="modal-header bg-primary">' +
                                                        '<button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>' +
                                                        '<h1 class="modal-title">::: SUPER ::: - ' + options.titulo + '</h1>' +
                                                    '</div>' +
                                                    '<div class="modal-body" id="' + idBuscador +'">' +
                                                    '</div>' +
                                                    '<div class="modal-footer">' +
                                                        '<b><button id="btnAceptar" class="btn btn-primary">Aceptar</button></b>  ' +
                                                        '<b><button id="btnCancelar" class="btn btn-default">Cancelar</button></b>' +
                                                    '</div>' +
                                                '</div>' +
                                            '</div>' +
                                        '</div>');
            };

           
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: IB.vars.strserver + "plugins/datatables/jquery.dataTables.min.css"
            }).appendTo("head");
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: IB.vars.strserver + "plugins/BuscadorPersonas/BuscadorPersonas.css"
            }).appendTo("head");

            if (!getScript(IB.vars.strserver + "plugins/datatables/jquery.dataTables.min.js")) $.getScript(IB.vars.strserver + "plugins/datatables/jquery.dataTables.min.js");
           

            //Función que pinta el formulario de buscador de personas
            function pintarContenido(uniqueId) {
                $('#' + uniqueId).html('');
                $('#' + uniqueId).append('<div id="txtBusqueda" class="form-group">' +
                                            '<fieldset class="fieldset">' +
                                            '<h3 class="sr-only">Datos del profesional</h3>' +
                                            '<legend class="fieldset">'+
                                                'Datos del profesional' +
                                            '</legend>' +  
                                                '<div class="form-group" >' +
                                                    '<div class="col-xs-12 col-sm-4 col-md-4">' +
                                                        '<div class="col-xs-12">' +
                                                            '<label for="txtAp1Prof" id="lblAp1Prof">Apellido 1º</label>' +
                                                        '</div>' +
                                                        '<div class="col-xs-12">' +
                                                            '<input id="txtAp1Prof" name="" type="text" class="input-md" value="" />' +
                                                        '</div>' +
                                                    '</div>' +
                                                    '<div class="col-xs-12 col-sm-4 col-md-4">' +
                                                        '<div class="col-xs-12">' +
                                                            '<label for="txtAp2Prof" id="lblAp2Prof">Apellido 2º</label>' +
                                                        '</div>' +
                                                        '<div class="col-xs-12">' +
                                                            '<input id="txtAp2Prof" name="" type="text" class="input-md" value="" />' +
                                                        '</div>' +
                                                    '</div>' +
                                                    '<div class="col-xs-12 col-sm-4 col-md-4">' +
                                                        '<div class="col-xs-12">' +
                                                            '<label for="txtNomProf" id="lblNomProf">Nombre</label>' +
                                                         '</div>' +
                                                         '<div class="col-xs-12">' +
                                                            '<input id="txtNomProf" name="" type="text" class="input-md" value="" />' +
                                                         '</div>' +
                                                    '</div>' +                                                
                                               '</div>' +
                                               '<div class="form-group">' +
                                                     '<div class="col-xs-6">' +
                                                        '<div class="col-xs-12 checkbox"/>' +
                                                    '</div>' +
                                                    '<div class="col-xs-6 text-right no-padding-right">' +
                                                        '<button title="Obtener personas" class="btn btn-primary btn-md obtenerPersonas" style="margin-top:10px;">Obtener</button>' +
                                                    '</div>' +
                                               '</div>' +
                                             '</fieldset>' +
                                           
                                            '<div class="row">' +
                                                
                                            '</div>' +
                                            
                                        '</div>' +
                                        '<div class="row">'+
                                            '<div id="divProfesionales" class="text-left col-xs-12">' +                                              
                                            '</div>' +
                                        '</div>');


                if (tipoBusquedaParam == tipoProfesionales) {
                    //TODO: Si el perfil de usuario es RG se habilita el check
                    //Obtener el perfil del array de datos de sesion
                    $('#' + uniqueId + ' .checkbox').append('<label for="chkTodos" class="control-label fk-label">');
                    if (IB.vars.perfil == 'RG') $('#' + uniqueId + ' .checkbox').append('<input type="checkbox" id="chkTodos"/>Mostrar de tus G.Funcionales</label>');
                    else $('#' + uniqueId + ' .checkbox').append('<input type="checkbox" id="chkTodos" disabled/>Mostrar de tus G.Funcionales</label>');
                                                            
                } else if (tipoBusquedaParam == tipoResponsablesProyecto) {
                    $('#' + uniqueId + ' .checkbox').append('<label for="chkBajas" class="control-label fk-label"><input id="chkBajas" type="checkbox">Mostrar bajas</label>');
                                         
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

            //Buscar cuando se pulsa intro sobre alguna de las cajas de texto
            $('#txtBusqueda :input[type=text]').on('keyup', function (event) {

                if ($("#listaProfesionales tr").length > 0) {
                    $("#listaProfesionales").html("");
                }
                if (event.keyCode == 13) {
                    obtenerDatos();
                }
            });

            function obtenerDatos() {
                var nombre = $('#txtNomProf').val();
                var apellido1 = $('#txtAp1Prof').val();
                var apellido2 = $('#txtAp2Prof').val();
                var bajas = false;
                var nodo = "";

                if (tipoBusquedaParam == tipoProfesionales) {
                    if ($("#chkTodos").is(':checked')) {
                        nombre = "";
                        apellido1 = "";
                        apellido2 = "";
                    } else if (nombre == "" && apellido1 == "" && apellido2 == "") {
                        IB.bsalert.toast("No se ha definido ningún criterio de búsqueda","warning");
                        return false;
                    }
                } else if (tipoBusquedaParam == tipoResponsablesProyecto) {
                    bajas = $("#chkBajas").is(':checked') ? true : false;
                }

                IB.procesando.mostrar();
                var payload = { tipoBusqueda: tipoBusquedaParam, apellido1: apellido1, apellido2: apellido2, nombre: nombre, bajas: bajas, nodo: nodo };

                try {
                    IB.DAL.post(IB.vars["strserver"] + "Capa_Presentacion/IAP30/Services/BuscadorPersonas.asmx", "ObtenerProfesionales", payload, null,
                     function (data) {
                         cargarDatosResultado(data);
                     },
                     function (ex, status) {
                         //hacer todo en caso de que falle
                         //...
                         //...
                         IB.bserror.error$ajax(ex, status)
                     }
                 );
                } catch (e) {
                    IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
                }               
                
            }


            function cargarDatosResultado(data) {
                
                $('#' + uniqueId + ' #divProfesionales').empty();
                $('#' + uniqueId + ' #divProfesionales').append('<table id="listaProfesionales" class="display compact margin-bottom">' +
                                                                '<thead>' +
                                                                        '<tr>' +
                                                                            '<th class="bg-primary">Tipo</th>' +
                                                                            '<th class="bg-primary">Profesional</th>' +
                                                                        '</tr>' +
                                                                '</thead>' +
                                                            '</table>' +
                                                            '<div id="divLeyenda"/>' +
                                                            '</div>');

                //Se inicializa el DataTable de profesionales
                if (typeof table_buscaPersonas != "undefined" && table_buscaPersonas != null) table_buscaPersonas.destroy();
                initDataTable(data, tipoBusquedaParam);
                pintarDivLeyenda(tipoBusquedaParam);
                IB.procesando.ocultar();
            }            
            
            function pintarDivLeyenda(tipoBusquedaParam) {
                /* $('#divIconoUser').append('<span class="fa fa-user azul"></span><span>Empleado interno&nbsp;&nbsp;</span>'+
               '<span class="fa fa-user externo"></span><span>Colaborador externo</span>');*/
                if (tipoBusquedaParam == tipoProfesionales)
                    $('#divLeyenda').append('<img alt="Empleado interno" id="logoUsuInterno" src="' + IB.vars["strserver"] + 'Images/imgUsuIVM.gif"><span>&nbsp;Interno&nbsp;&nbsp;</span>' +
                                                '<img alt="Empleado externo" id="logoUsuExterno" src="' + IB.vars["strserver"] + 'Images/imgUsuEVM.gif"><span>&nbsp;Externo&nbsp;&nbsp;</span>' +
                                                '<img alt="Empleado foráneo" id="logoUsuExterno" src="' + IB.vars["strserver"] + 'Images/imgUsuFVM.gif"><span>Foráneo&nbsp;&nbsp;</span>');

                else if (tipoBusquedaParam == tipoResponsablesProyecto) {
                    $('#divLeyenda').append('<img alt="responsable" src="' + IB.vars["strserver"] + 'Images/imgResponsable.gif"><span>Responsable&nbsp;&nbsp;</span>' +
                                            '<img alt="responsable" src="' + IB.vars["strserver"] + 'Images/imgResponsableCRP.gif"><span>No responsable&nbsp;&nbsp;</span>');

                }
        
            }       
       
            // Visualiza la ventana modal
            function mostrarModal() {
                $('.ocultable').attr('aria-hidden', 'true');
                $('#buscProfesional').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#buscProfesional').modal('show');
                
                if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
                    indicadores.i_dispositivoTactil = true;
                }
            }

            //Recepción del foco en el textarea del modal de comentario
            $(document).on('shown.bs.modal', '#buscProfesional', function () {
                $('#txtAp1Prof').focus();
            });           

            //Botón aceptar de la ventana
            $('#' + uniqueId + ' #btnAceptar').on('click', function (e) {
                if ($("#listaProfesionales tr.selected").length == 0) {
                    IB.bsalert.fixedAlert("warning", "Error", "No se ha seleccionado ningún profesional");
                    //e.preventDefault();
                    return false;
                }

                if (options.modal) {                    
                    $("#buscProfesional").modal('hide');                    
                }
                options.aceptar(table_buscaPersonas.rows('.selected').data()[0]);

            });

            //Acción de doble click en cada línea de la tabla
            $(document).off("dblclick", '#listaProfesionales tbody tr').on("dblclick", '#listaProfesionales tbody tr', function (e) {
                table_buscaPersonas.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('.popover').hide();
                $('#' + uniqueId + ' #btnAceptar').trigger("click");
            });

            $(document).off('click', '#listaProfesionales tbody tr').on('click', '#listaProfesionales tbody tr', function (e) {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    table_buscaPersonas.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });


            //Botón cancelar de la ventana
            $('#' + uniqueId + ' #btnCancelar').on('click', function () {
                if (options.modal) {
                    $("#buscProfesional").modal('hide');                    
                }
                //options.cancelar('acción de cancelar');
                eval("options.cancelar()");
            });

            //para que al cerrar la modal los elementos de la pantalla principal estén visibles
            $(document).on('hidden.bs.modal', '#buscProfesional', function () {
                $('.ocultable').attr('aria-hidden', 'false');
                table_buscaPersonas = null;
                $('#' + uniqueId).html('');
            });


            //Inicializa el datatable
            function initDataTable(dataSource) {
                var campo = 'tipo';

                if (tipoBusquedaParam == tipoResponsablesProyecto) campo = 'es_responsable';

                table_buscaPersonas = $("#listaProfesionales").DataTable({
                    
                    data: dataSource,

                    
                    columns: [
                             { "data": campo , "width": "10px", "title": "Tipo" },
                             { "data": 'PROFESIONAL' },
                    ],
                    
                    /*columns: [
                             { "data": 'PROFESIONAL' },
                    ],*/
                    columnDefs: [                       
                        {
                            "targets": 0,
                            "className": "text-center",
                            "render": {
                                "display": function (data, type, row, meta) {
                                    if (tipoBusquedaParam == tipoProfesionales || tipoBusquedaParam == tipoUsuarios) {
                                        if (row.t001_sexo == 'M') {
                                            if (row.tipo == 'P' || row.tipo == 'N') {
                                                return '<img alt="Empleado interno" src="' + IB.vars["strserver"] + 'Images/imgUsuIM.gif">';
                                            } else if (row.tipo == 'F') {
                                                return '<img alt="Empleado foráneo"  src="' + IB.vars["strserver"] + 'Images/imgUsuFM.gif">';

                                            } else return '<img alt="Empleado externo" src="' + IB.vars["strserver"] + 'Images/imgUsuEM.gif">';
                                        } else {
                                            if (row.tipo == 'P' || row.tipo == 'N') {
                                                return '<img alt="Empleado interno" src="' + IB.vars["strserver"] + 'Images/imgUsuIV.gif">';
                                            } else if (row.tipo == 'F') {
                                                return '<img alt="Empleado foráneo"  src="' + IB.vars["strserver"] + 'Images/imgUsuFV.gif">';

                                            } else return '<img alt="Empleado externo" src="' + IB.vars["strserver"] + 'Images/imgUsuEV.gif">';
                                        }
                                    } else if (tipoBusquedaParam == tipoResponsablesProyecto) {
                                        if (row.es_responsable > 0) {
                                            return '<img alt="Responsable" src="' + IB.vars["strserver"] + 'Images/imgResponsable.gif">';
                                        } else return '<img alt="No responsable"  src="' + IB.vars["strserver"] + 'Images/imgResponsableCRP.gif">';

                                    } //TODO CR
                                }
                            }
                        },

                         {
                             "targets": 1,
                             "render": {
                                 "display": function (data, type, row, meta) {
                                     var content = "<b>Profesional:</b> " + row.t314_idusuario + " - " + row.PROFESIONAL + "<br /> <b>C.R.:</b> " + row.t303_denominacion ;
                                     return '<span data-placement="top" data-toggle="popover" data-content="' + content + '" title="<b>Información</b>">' + row.PROFESIONAL + '</span>';
                                 }
                             }

                         }
                    ],

                    scrollY: "20vh",
                    scrollCollapse: true,
                    paging: false,
                    language: { "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
                    info: false,
                    procesing: true,                   
                    responsive: false,
                    "order": [1, "asc"],
                    fnInitComplete: function (oSettings, json) {
                        if (!indicadores.i_dispositivoTactil) {
                            $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true });
                        }
                       
                    }                    
                });

            }
        }

    });   

    function ProfesionalFiltro(nombre, apellido1, apellido2) {
        this.nombre = nombre;
        this.apellido1 = apellido1;
        this.apellido2 = apellido2;
    }

})(jQuery);
