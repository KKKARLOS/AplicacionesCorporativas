var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ParteAct = SUPER.IAP30.ParteAct || {}


SUPER.IAP30.ParteAct.View = (function (e) {

    var dom = {

        txtDesde: $('#txtDesde'),
        txtHasta: $('#txtHasta'),
        btnObtener: $('#btnObtener'),
        btnExportar: $('#btnExportar'),
        tabla: $('#tablaResultados'),
        chkFacturables: $('#chkFacturables'),
        chkNoFacturables: $('#chkNoFacturables'),
        btnProyecto: $('#btnProyecto'),
        btnCliente: $('#btnCliente'),
        txtRango: $('#txtRango'),
        listaProyectos: $('#listaProyectos'),
        btnEliminarProyectos: $('#btnEliminarProyectos'),
        listaClientes: $('#listaClientes'),
        btnEliminarClientes: $('#btnEliminarClientes'),
        //Modal de selección de proyectos
        modalProyecto: $('#modal-Proyecto'),
        btnObtenerProyectos: $('#btnObtenerProyectos'),        
        selEstado: $('#selEstado'),
        selCategoria: $('#selCategoria'),        
        lblCRAdmin: $('#lblCRAdmin'),
        CRAdmin: $('#CRAdmin'),
        btnEliminarCR: $('#btnEliminarCR'),
        lblClienteProyectos: $('#lblClienteProyectos'),
        txtClienteProyectos: $('#txtClienteProyectos'),
        btnEliminarClienteProyectos: $('#btnEliminarClienteProyectos'),
        lblContrato: $('#lblContrato'),
        btnEliminarContrato: $('#btnEliminarContrato'),
        lblResp: $('#lblResp'),
        divBuscadorResp: $('.buscadorResp'),
        btnEliminarResp: $('#btnEliminarResp'),
        txtResponsable: $('#txtResponsable'),
        txtIdProyecto: $('#txtIdProyecto'),
        txtDesProyecto: $('#txtDesProyecto'),
        selBusquedaProyectos: $('#selBusquedaProyectos'),
        selCualidad: $('#selCualidad'),
        txtIdContrato: $('#txtIdContrato'),
        txtDesContrato: $('#txtDesContrato'),
        lblHor: $('#lblHor'),
        btnEliminarHor: $('#btnEliminarHor'),
        txtHorizontal: $('#txtHorizontal'),
        lblQn: $('#lblQn'),
        btnEliminarQn: $('#btnEliminarQn'),
        txtQn: $('#txtQn'),
        lblQ1: $('#lblQ1'),
        btnEliminarQ1: $('#btnEliminarQ1'),
        txtQ1: $('#txtQ1'),
        lblQ2: $('#lblQ2'),
        btnEliminarQ2: $('#btnEliminarQ2'),
        txtQ2: $('#txtQ2'),
        lblQ3: $('#lblQ3'),
        btnEliminarQ3: $('#btnEliminarQ3'),
        txtQ3: $('#txtQ3'),
        lblQ4: $('#lblQ4'),
        btnEliminarQ4: $('#btnEliminarQ4'),
        txtQ4: $('#txtQ4'),
        selModCon: $('#selModCon'),
        txtCR: $('#txtCR'),        
        tablaProyectosModal: $('#tablaProyectos'),
        tablaProyectosSelModal: $('#tablaProyectosSel'),
        checkAuto: $('#chkAuto'),
        btnAceptarProyecto: $('#btnAceptarProyecto'),
        //Modal de CR
        modalCR: $('#modal-CR'),
        tablaCR: $('#tablaCR'),
        btnAceptarCR: $('#btnAceptarCR'),
        //Modal de contrato
        modalContrato: $('#modal-Contrato'),
        idContratoM: $('#idContratoM'),
        desContratoM: $('#desContratoM'),
        selBusquedaContratos: $('#selBusquedaContratos'),
        chkMostrarTodosContratos: $('#chkMostrarTodosContratos'),
        txtClienteContratos: $('#txtClienteContratos'),
        lblClienteContratos: $('#lblClienteContratos'),
        btnEliminarClienteContratos: $('#btnEliminarClienteContratos'),
        btnObtenerContratoProyectos: $('#btnObtenerContratoProyectos'),
        tablaContratos: $('#tablaContratos'),
        btnAceptarContrato: $('#btnAceptarContrato')
    }

    var selectores = {

        contenedor: ".container",
        modeloParte: "input[name=optradio]:checked",
        sel_filtros: ".fk_filtro",
        sel_chk: ".fk_chk",        
        sel_proyectos: "#listaProyectos > li",
        sel_clientes: "#listaClientes > li",
        //Modal de selección de proyectos
        sel_filtros_proyectos: ".fk_filtro_proy",
        sel_iddesc_proyectos: ".fk_filtro_proy_proy",
        sel_filtros_proyectos_input: ".fk_filtro_proy_input",
        sel_filtros_proyectos_sel: ".fk_filtro_proy_sel",
        ayudaCliente: ".fk_ayudaCliente",
        ayudaClienteMulti: ".fk_ayudaClienteMulti",
        ayudaCatalogoBasico: ".fk_ayudaCatalogoBasico",
        sel_cualif: ".fk_Cualif"
        
    }

    var indicadores = {
        //i_dispositivoTactil: false
    }

    function deAttachEvents(selector) {
        $(selector).off();
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function deAttachLiveEvents(event, selector, callback) {
        $(document).off(event, selector, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {

        //if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
        //    indicadores.i_dispositivoTactil = true;
        //}

        inicializarTabla();

        inicializarDatepickers();

        //Vaciado de tabla cuando se produce un cambio en los filtros
        $(selectores.sel_filtros).on("change", vaciarTabla);

        //validación de campos numéricos
        $(document).on('keypress', 'input.txtNum', function (e) {
            return validarTeclaNumerica(e, false);
        });

        //Exportación a PDF
        attachEvents("click keypress", dom.btnExportar, exportarPDF);
                
    }

    //Funcionalidad pantalla principal

    var exportarPDF = function () {

        IB.procesando.mostrar()

        if (!dom.tabla.DataTable().rows().count()) {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("No hay datos a exportar");
            return false;
        }

        if (!dom.tabla.DataTable().rows('.selected').count()) {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("No hay filas seleccionadas");
            return false;
        }

        if (dom.tabla.DataTable().rows('.selected').count() > 8000) {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("El volumen de imputaciones a exportar excede el máximo permitido. \n\n Acote el periodo temporal de la consulta o reduzca el número de tareas marcadas para la exportación.");
            return false;
        }

        var sb = new StringBuilder();

        $("#FORMATO").val("PDF");

        dom.tabla.DataTable().rows('.selected').every(function () {
            sb.append(this.data().t314_idusuario + '/' + this.data().t332_idtarea + '/' + moment(this.data().t337_fecha).format("YYYYMMDD") + ',');
        });

        $("#hdnConsumos").val(sb.toString());

        //*SSRS

        if ($(selectores.modeloParte).val() == "0") {
            reportname = "/SUPER/sup_parteactividad_a";
        } else {
            reportname = "/SUPER/sup_parteactividad_b";
        }

        var params = {
            reportName: reportname,
            tipo: "PDF",
            sConsumos: sb.toString()
        }

        PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR

        if ($(selectores.modeloParte).val() == "0") {
            document.forms["frmDatos"].action = IB.vars["strserver"] + "Capa_Presentacion/IAP/ParteAct/ExportarModA/default.aspx";
        } else {
            document.forms["frmDatos"].action = IB.vars["strserver"] + "Capa_Presentacion/IAP/ParteAct/ExportarModB/default.aspx";
        }  

        document.forms["frmDatos"].target = "_blank";
        document.forms["frmDatos"].submit();

        //CR*/
        IB.procesando.ocultar();

    }

    var inicializarTabla = function () {

        var dt = dom.tabla.DataTable({
            columns: [
                        { "data": null },
                        { "data": "t332_idtarea" }, // - t332_destarea .replace(/"/g, "'")                        
                        { "data": "Profesional" },
                        { "data": "t337_fecha" },
                        { "data": "t337_esfuerzo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "t302_denominacion" },
                        { "data": "t332_facturable" },
                        { "data": "t324_denominacion" }
            ],
            data: [],
            'columnDefs': [
                {
                    'targets': 0,
                    'checkboxes': {
                        'selectRow': true
                    }
                },
                { //t332_idtarea
                    "targets": 1,
                    "render": function (data, type, row) {
                        return accounting.formatNumber(data,0) + ' - ' + row.t332_destarea.replace(/"/g, "'");
                    }
                },
                { //t337_fecha
                    "targets": 3,
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
                        }
                    }
                },
                { //t332_facturable
                    "targets": 6,
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data == true)
                                return '<i class="fa fa-check"></i>';
                            else
                                return "";
                        }
                    }
                }
            ],
            'select': {
                'style': 'multi'
            },
            'order': [[3, 'asc']],
            bFilter: false,
            paging: false,
            bInfo: false,
            procesing: true,
            scrollCollapse: true,
            scrollY: "50vh",
            scrollX: true,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            //"createdRow": function (row, data, rowIndex) {                
            //    $(row).attr('data-tarea', data.t332_idtarea);
            //},            
            "initComplete": function (settings, json) {
                visualizarContenido();
            }
        });

    }

    var inicializarDatepickers = function () {

        var hoy = moment();

        $('#txtRango').daterangepicker({
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aceptar',
                cancelLabel: 'Cancelar',
            },
            startDate: hoy,
            endDate: hoy,
            opens: "left",
            linkedCalendars: false,
            disableHoverDate: true
        });

    }

    var pintarTablaPartes = function (data) {

        dom.tabla.DataTable().clear().draw();
        dom.tabla.DataTable().rows.add(data).draw();
        dom.tabla.DataTable().draw();

    }

    var vaciarTabla = function () {

        dom.tabla.DataTable().clear().draw();

    }

    var visualizarContenido = function () {

        $(selectores.contenedor).css('visibility', 'visible');

    }

    var visualizarContenidoSR = function () {

        //Para que al cerrar la modal los elementos de la pantalla principal estén visibles al SR
        $('.ocultable').attr('aria-hidden', 'false');

    }

    //Fin funcionalidad pantalla principal

    //Funcionalidad modal proyectos

    var llenarModContrat = function (data) {

        dom.selModCon.empty();
        dom.selModCon.append(
                $("<option></option>")
                    .text("Todos")
                    .val("")
            );

        $(data).each(function (index, item) {
            dom.selModCon.append(
                $("<option></option>")
                    .text(item.t316_denominacion)
                    .val(item.t316_idmodalidad)
            );
        });

    }

    var llenarNodosCR = function (data) {

        dom.txtCR.empty();
        dom.txtCR.append(
                $("<option></option>")
                    .text("Todos")
                    .val("")
            );

        $(data).each(function (index, item) {
            dom.txtCR.append(
                $("<option></option>")
                    .text(item.DENOMINACION)
                    .val(item.IDENTIFICADOR)
            );
        });

    }

    var mostrarModalProyecto = function () {

        //Se inicializan las datatables si no han sido inicializadas antes
        if (!$.fn.DataTable.isDataTable('#tablaProyectos')) {
            inicializarDatatablesProyectos();
        }
        else {

            //Se vacia la lista de proyectos
            dom.tablaProyectosModal.DataTable().clear().draw();
            dom.tablaProyectosSelModal.DataTable().clear().draw();

            //Si hay proyectos en criterios se pasan a la tabla de seleccionados
            if (dom.listaProyectos.children().length != 0) {
                volcarProyectosCriterio();
            }

        }                
        
        $('#divProyectos .marco .selector').children().children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');

        comprobarAceptarProyectos();

        //Se oculta el contenido inferior al lector de pantallas
        $('.ocultable').attr('aria-hidden', 'true');

        dom.modalProyecto.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        dom.modalProyecto.modal('show');

    }

    //Comprueba si existen proyectos seleccionados para habilitar o no el botón aceptar del modal
    var comprobarAceptarProyectos = function () {
        //Se deshabilita el botón aceptar si no hay proyectos seleccionados
        if (dom.tablaProyectosSelModal.DataTable().data().length == 0) {
            dom.btnAceptarProyecto.attr('disabled', 'disabled');
        } else {
            dom.btnAceptarProyecto.removeAttr('disabled');
        }
    }

    var limpiarFiltrosProyectos = function () {
        //Se dejan los filtros en su situación original
        dom.checkAuto.prop('checked', false);
        limpiarFiltro($(selectores.sel_filtros_proyectos_input));
        $(selectores.sel_filtros_proyectos_sel).each(function () { $(this).val($(this).find("option:first").val()); });
        dom.selEstado.val(dom.selEstado.find("option:first").next().val());
    }    

    var inicializarDatatablesProyectos = function () {

        var dt = dom.tablaProyectosModal.DataTable({
            columns: [                        
                        { "data": null, "width": "13%" },
                        {
                            "data": "t301_idproyecto", "width": "10%",
                            render: function (data) {
                                return accounting.formatNumber(data,0);
                            }
                        },
                        { "data": "t301_denominacion", "width": "77%" }
            ],
            data: [],
            'columnDefs': [
                { //Atributos del proyecto
                    "targets": 0,
                    "orderable": false,
                    "render": function (data, type, row) {
                        var atributos = "";

                        row.t301_categoria == "P" ? atributos += '<i class="fa fa-ticket fa-rotate-180" aria-hidden="true"></i>' : atributos += '<i class="fa fa-user" aria-hidden="true"></i>';

                        switch (row.t305_cualidad) {
                            case "C":
                                atributos += ' <i class="fa fa-copyright fk_verde" aria-hidden="true"></i>'
                                break;
                            case "J"://Activo
                                atributos += ' <i class="fa fa-registered" aria-hidden="true"></i>'
                                break;
                            case "P":
                                atributos += ' <i class="fa fa-registered fk_verde" aria-hidden="true"></i>'
                                break;
                        }

                        switch (row.t301_estado) {
                            case "A":
                                atributos += ' <i class="fa fa-diamond fa-diamond-verde" aria-hidden="true"></i>'
                                break;
                            case "C"://Activo
                                atributos += ' <i class="fa fa-diamond fa-diamond-rojo" aria-hidden="true"></i>'
                                break;
                            case "H":
                                atributos += ' <i class="fa fa-diamond fa-diamond-gris" aria-hidden="true"></i>'
                                break;
                            case "P":
                                atributos += ' <i class="fa fa-diamond fa-diamond-verde" aria-hidden="true"></i>'
                                break;
                        }
                        return atributos;
                    }
                }
            ],            
            'order': [[1, 'desc']],
            bFilter: false,
            paging: false,
            bInfo: false,
            procesing: true,
            //scrollCollapse: true,
            scrollY: "25vh",
            scrollX: true,
            searching: true,
            dom: 'Bft',
            buttons: [
                {
                    className: 'selector',
                    text: '<i class="glyphicon glyphicon-unchecked"></i>',
                    titleAttr: 'Marcar todas las líneas'
                }
            ],
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            "createdRow": function (row, data, rowIndex) {
                var content = '<b>Proyecto:</b> ' + data.t301_denominacion.replace(/"/g, "'") + '<br /> <b>Responsable:</b> ' + data.responsable + '<br /> <b>C.R.:</b> ' + data.t303_denominacion + '<br /> <b>Cliente:</b> ' + data.t302_denominacion;
                
                $(row).find('td:first-child').attr('data-placement', 'top');
                $(row).find('td:first-child').attr('data-toggle', 'popover');
                $(row).find('td:first-child').attr('data-content', content);
                $(row).find('td:first-child').attr('title', '<b>Información</b>');
                $(row).attr('data-t305_idproyectosubnodo', data.t305_idproyectosubnodo);
                $(row).attr('data-t301_idproyecto', accounting.formatNumber(data.t301_idproyecto,0));
                $(row).attr('data-t301_denominacion', data.t301_denominacion);
                $(row).attr('data-t301_categoria', data.t301_categoria);
                $(row).attr('data-t305_cualidad', data.t305_cualidad);
                $(row).attr('data-t301_estado', data.t301_estado);
            },
            "drawCallback": function( settings ) {
                //if (!indicadores.i_dispositivoTactil) {
                    $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true });
                //}
            }
        });

        var dt2 = dom.tablaProyectosSelModal.DataTable({
            columns: [
                        { "data": null, "width": "13%" },
                        {
                            "data": "t301_idproyecto", "width": "10%",
                            render: function (data) {
                                return accounting.formatNumber(data, 0);
                            }
                        },
                        { "data": "t301_denominacion", "width": "77%" }
            ],
            data: [],
            'columnDefs': [
                { //Atributos del proyecto
                    "targets": 0,
                    "orderable": false,
                    "render": function (data, type, row) {
                        var atributos = "";

                        row.t301_categoria == "P" ? atributos += '<i class="fa fa-ticket fa-rotate-180" aria-hidden="true"></i>' : atributos += '<i class="fa fa-user" aria-hidden="true"></i>';

                        switch (row.t305_cualidad) {
                            case "C":
                                atributos += ' <i class="fa fa-copyright fk_verde" aria-hidden="true"></i>'
                                break;
                            case "J"://Activo
                                atributos += ' <i class="fa fa-registered" aria-hidden="true"></i>'
                                break;
                            case "P":
                                atributos += ' <i class="fa fa-registered fk_verde" aria-hidden="true"></i>'
                                break;
                        }

                        switch (row.t301_estado) {
                            case "A":
                                atributos += ' <i class="fa fa-diamond fa-diamond-verde" aria-hidden="true"></i>'
                                break;
                            case "C"://Activo
                                atributos += ' <i class="fa fa-diamond fa-diamond-rojo" aria-hidden="true"></i>'
                                break;
                            case "H":
                                atributos += ' <i class="fa fa-diamond fa-diamond-gris" aria-hidden="true"></i>'
                                break;
                            case "P":
                                atributos += ' <i class="fa fa-diamond fa-diamond-verde" aria-hidden="true"></i>'
                                break;
                        }
                        return atributos;
                    }
                }
            ],
            'order': [[1, 'desc']],
            bFilter: false,
            paging: false,
            bInfo: false,
            procesing: true,
            //scrollCollapse: true,
            scrollY: "25vh",
            scrollX: true,
            searching: true,
            dom: 'Bft',
            buttons: [
                {
                    className: 'selector',
                    text: '<i class="glyphicon glyphicon-unchecked"></i>',
                    titleAttr: 'Marcar todas las líneas'
                }
            ],
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            "createdRow": function (row, data, rowIndex) {
                $(row).attr('data-t305_idproyectosubnodo', data.t305_idproyectosubnodo);
                $(row).attr('data-t301_idproyecto', accounting.formatNumber(data.t301_idproyecto,0));
                $(row).attr('data-t301_denominacion', data.t301_denominacion);
                $(row).attr('data-t301_categoria', data.t301_categoria);
                $(row).attr('data-t305_cualidad', data.t305_cualidad);
                $(row).attr('data-t301_estado', data.t301_estado);
            }
        });        

    }

    var vaciarTablaProyectos = function (data) {

        dom.tablaProyectosModal.DataTable().clear().draw();

    }

    var pintarTablaProyectos = function (data) {        

        dom.tablaProyectosModal.DataTable().rows.add(data).draw();      

    }    

    var attachEventosListaDual = function () {
        //Cualificadores Q3 y Q4 ocultos
        $('#grpQ3').css('display', 'none');
        $('#grpQ4').css('display', 'none');


        //FUNCIONALIDAD LISTA DUAL PROYECTOS//

        //Selección de proyectos
        $('body').on('click', '#bodyTablaProyectos tr, #bodyTablaProyectosSel tr', function (e) {
            if ($(this).children().hasClass('dataTables_empty')) return;
            var lista = $(this).parent().children();
            if (e.shiftKey && lista.filter('.activa').length > 0) {
                var first = lista.filter('.activa:first').index();//Primer seleccionado
                var last = lista.filter('.activa:last').index();//Último seleccionado
                $('#bodyTablaProyectos tr').removeClass('active');//Borrar de las dos listas
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
            $(this).closest(".dataTables_wrapper").siblings().find('.selector').children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
        });

        //Click en los botones
        $('#divProyectos .btnacciones button').on('click', function () {
            var $button = $(this), actives = '', vacia = 0;
            if ($button.hasClass('move-left')) {

                ////Botón eliminar
                var actives = $('#tablaProyectosSel').dataTable().find(".activa:visible");

                if (actives.length == 0) {
                    IB.bsalert.toast("No has seleccionado ningún proyecto", "danger");
                    return;
                }
                //Se eliminan las filas
                $('#tablaProyectosSel').dataTable().fnDeleteRow(actives, false);
                //se quitan las clases activa y se deja el selector de todas las filas en situación de no seleccionado
                $('#tablaProyectosSel tbody tr.activa').removeClass('activa');
                $('#proyectos-lisSeleccionados .selector').removeClass('selected')
                $('#proyectos-lisSeleccionados .selector').find('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');

            } else if ($button.hasClass('move-right')) {

                var actives = $('#tablaProyectos').dataTable().find(".activa:visible");

                if (actives.length == 0 ) {
                    IB.bsalert.toast("No has seleccionado ningún proyecto", "danger");
                    return;
                }

                var contenedorDerecha = $('#tablaProyectosSel tbody tr');

                for (var i = 0; i < actives.length; i++) {
                    var repetida = 0;
                    contenedorDerecha.each(function (index, element) {
                        if (actives[i].getAttribute("data-t305_idproyectosubnodo") == $(this).attr("data-t305_idproyectosubnodo")) {
                            repetida = 1;
                        }                        
                    })
                    if (!repetida) {
                        //actives[i].setAttribute("data-t301_idproyecto", accounting.formatNumber(actives[i].getAttribute("data-t301_idproyecto"),0));
                        addRow = $('#tablaProyectos').dataTable().fnGetData(actives[i]);
                        $('#tablaProyectosSel').dataTable().fnAddData(addRow);
                    }
                }
                //se quitan las clases activa y se deja el selector de todas las filas en situación de no seleccionado
                $('tbody tr.activa').removeClass('activa');
                $('#proyectos-lisProyectos .selector, #proyectos-lisSeleccionados .selector').removeClass('selected')
                $('#proyectos-lisProyectos .selector, #proyectos-lisSeleccionados .selector').find('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
                
            }
            comprobarAceptarProyectos();
        });

        //Seleccionar todos o ninguno. 
        $('body').on('click', '#proyectos-lisProyectos .selector, #proyectos-lisSeleccionados .selector', function (e) {
            var $checkBox = $(this);
            if ($checkBox.closest('.dataTables_wrapper').find('.dataTable').DataTable().data().length == 0) return;
            if (!$checkBox.hasClass('selected')) {
                $checkBox.addClass('selected').closest('.marco').find('table tbody tr:not(.activa)').find('td:not(.dataTables_empty)').parent().addClass('activa');
                $checkBox.children().children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
            } else {
                $checkBox.removeClass('selected').closest('.marco').find('table tbody tr.activa').removeClass('activa');
                $checkBox.children().children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
            }
        });        

        //FIN FUNCIONALIDAD LISTA DUAL PROYECTOS//

        //Ajuste de columnas de datatables de proyectos
        $(document).on('shown.bs.modal', '#modal-Proyecto', function () {
            dom.tablaProyectosModal.DataTable().columns.adjust();
            dom.tablaProyectosSelModal.DataTable().columns.adjust();
        });
    }

    var deAttachEventosListaDual = function () {        

        //Selección de proyectos
        $('body').off('click', '#bodyTablaProyectos tr, #bodyTablaProyectosSel tr');

        //Click en los botones
        $('#divProyectos .btnacciones button').off();

        //Seleccionar todos o ninguno. 
        $('body').off('click', '#proyectos-lisProyectos .selector, #proyectos-lisSeleccionados .selector');

        //Ajuste de columnas de datatables de proyectos
        $(document).off('shown.bs.modal', '#modal-Proyecto');

    }

    var cambiarLinkCualif = function (mostrar) {

        if (mostrar) {

            $(selectores.sel_cualif).addClass('underline btn-link');
            $(selectores.sel_cualif).attr('role', 'link');
            attachEvents("click keypress", dom.lblQn, abrirBuscadorQn);
            attachEvents("click keypress", dom.lblQ1, abrirBuscadorQ1);
            attachEvents("click keypress", dom.lblQ2, abrirBuscadorQ2);
            attachEvents("click keypress", dom.lblQ3, abrirBuscadorQ3);
            attachEvents("click keypress", dom.lblQ4, abrirBuscadorQ4);
            attachEvents("click keypress", dom.btnEliminarQn, eliminarQnProyectos);
            attachEvents("click keypress", dom.btnEliminarQ1, eliminarQ1Proyectos);
            attachEvents("click keypress", dom.btnEliminarQ2, eliminarQ2Proyectos);
            attachEvents("click keypress", dom.btnEliminarQ3, eliminarQ3Proyectos);
            attachEvents("click keypress", dom.btnEliminarQ4, eliminarQ4Proyectos);


        } else {

            $(selectores.sel_cualif).removeClass('underline').removeClass('btn-link');
            $(selectores.sel_cualif).removeAttr('role');
            deAttachEvents(dom.lblQn);
            deAttachEvents(dom.lblQ1);
            deAttachEvents(dom.lblQ2);
            deAttachEvents(dom.lblQ3);
            deAttachEvents(dom.lblQ4);
            attachEvents(dom.btnEliminarQn);
            attachEvents(dom.btnEliminarQ1);
            attachEvents(dom.btnEliminarQ2);
            attachEvents(dom.btnEliminarQ3);
            attachEvents(dom.btnEliminarQ4);

        }

    }

    var volcarProyectosSeleccionados = function () {

        dom.listaProyectos.empty();

        if (dom.tablaProyectosSelModal.DataTable().data().length != 0) {
            $('#tablaProyectosSel').find('tbody tr').each(function () {
                var li = $("<li class='list-group-item' data-t305_idproyectosubnodo='" + $(this).attr('data-t305_idproyectosubnodo')
                    + "' data-t301_idproyecto='" + $(this).attr('data-t301_idproyecto')
                    + "' data-t301_denominacion='" + $(this).attr('data-t301_denominacion')
                    + "' data-t301_categoria='" + $(this).attr('data-t301_categoria')
                    + "' data-t305_cualidad='" + $(this).attr('data-t305_cualidad')
                    + "' data-t301_estado='" + $(this).attr('data-t301_estado') + "'>")
                $("th, td", this).each(function () {
                    li.append(this.innerHTML + ' ');
                });
                dom.listaProyectos.removeClass('fk_lista');
                dom.listaProyectos.append(li);
            })
        } else {
            dom.listaProyectos.removeClass('fk_lista').addClass('fk_lista');
        }

        dom.listaProyectos.change();
    }

    var volcarProyectosCriterio = function () {

        var contenido = new StringBuilder();
        var coma = "";

        contenido.append('[');
        $(listaProyectos).find('li').each(function (index, element) {
            contenido.append(coma);
            contenido.append(' { "t305_idproyectosubnodo":' + parseInt($(this).attr('data-t305_idproyectosubnodo')) + '');
            contenido.append(' , "t301_idproyecto":"' + accounting.unformat($(this).attr('data-t301_idproyecto'),",") + '"');
            contenido.append(' , "t301_denominacion":"' + $(this).attr('data-t301_denominacion') + '"');
            contenido.append(' , "t301_categoria":"' + $(this).attr('data-t301_categoria') + '"');
            contenido.append(' , "t305_cualidad":"' + $(this).attr('data-t305_cualidad') + '"');
            contenido.append(' , "t301_estado":"' + $(this).attr('data-t301_estado') + '" }');
            coma = ",";
        });

        contenido.append(' ]');

        var data = JSON.parse(contenido.toString());

        dom.tablaProyectosSelModal.DataTable().rows.add(data).draw();
        dom.tablaProyectosSelModal.DataTable().draw();

    }

    var volcarClientesCriterio = function () {

        var lstSeleccionados = []
        $(listaClientes).find("li.list-group-item").each(function () {
            lstSeleccionados.push({
                key: $(this).attr("data-id"),
                value: $(this).html(),
                tipo: $(this).attr("data-tipo"),
                estadoF: $(this).attr("data-estadoF")
            })
        })
    
        return lstSeleccionados;

    }

    var visualizarContenidoSR2 = function () {

        //Para que al cerrar la modal los elementos de la pantalla principal estén visibles al SR
        $('.ocultable2').attr('aria-hidden', 'false');

    }

    //Fin funcionalidad modal proyectos   

    //Funcionalidad modal CR

    var mostrarModalCR = function (data) {        

        //Se oculta el contenido inferior al lector de pantallas
        $('.ocultable2').attr('aria-hidden', 'true');

        //Se deshabilita el botón aceptar
        dom.btnAceptarCR.attr('disabled', 'disabled');

        if (!$.fn.DataTable.isDataTable('#tablaCR')) {
            initDatatableCR(data);
        }
        else {

            var oDataTable = $('#tablaCR').DataTable();

            oDataTable.clear().draw();
            oDataTable.rows.add(data).draw();            

            IB.procesando.ocultar();

            dom.modalCR.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            dom.modalCR.modal('show');

            setTimeout("$('#tablaCR').DataTable().columns.adjust().draw();", 300);

        }        

    }

    var attachEventosModalCR = function () {

        $('body').on('click', '#bodyTablaCR tr', function (e) {
            if ($(this).hasClass('activa')) {
                $(this).removeClass('activa');
                dom.btnAceptarCR.attr('disabled', 'disabled');

            } else {
                $('#bodyTablaCR tr.activa').removeClass('activa');
                $(this).addClass('activa');
                dom.btnAceptarCR.removeAttr('disabled');
            }
            
        });

        $('body').on('dblclick', '#bodyTablaCR tr', function (e) {            
            $('#bodyTablaCR tr.activa').removeClass('activa');
            $(this).addClass('activa');
            dom.btnAceptarCR.removeAttr('disabled');
            $("#btnAceptarCR").trigger("click");
        });

        $('#btnAceptarCR').on('click keypress', function () {
            dom.txtCR.attr('value', $('#bodyTablaCR tr.activa').attr('data-t303_idnodo'));
            dom.txtCR.val($('#bodyTablaCR tr.activa').attr('data-t303_denominacion')).change();
            cambiarLinkCualif(true);
        });

    }

    var deAttachEventosModalCR = function () {

        $('body').off('click', '#bodyTablaCR tr');

        $('body').off('dblclick', '#bodyTablaCR tr');

        $('#btnAceptarCR').off();
    }

    var initDatatableCR = function (data) {

        var dt = $('#tablaCR').DataTable({
            columns: [
                        { "data": "t303_idnodo" },
                        { "data": "t303_denominacion" }
            ],
            searching: true,
            data: data,
            'order': [[1, 'asc']],
            rowCallback: function (row, data) { },
            bFilter: false,
            paging: false,
            bInfo: false,
            procesing: true,
            scrollCollapse: true,
            scrollY: "40vh",
            scrollX: true,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            "initComplete": function (settings, json) {
                IB.procesando.ocultar();
                dom.modalCR.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                dom.modalCR.modal('show');
                setTimeout("$('#tablaCR').DataTable().columns.adjust().draw();", 300);     
            },
            "createdRow": function (row, data, rowIndex) {
                $(row).attr('data-t303_idnodo', data.t303_idnodo);
                $(row).attr('data-t303_denominacion', data.t303_denominacion);

            }
                
        });

    }

    //Fin funcionalidad modal CR 

    //Funcionalidad modal contratos

    var attachEventosModalContrato = function () {

        $(document).on('click keypress', 'td.fk_extendible', function (e) {
            var span = $(this).children(":first");
            var id = $(this).parent().attr('id');

            if (span.hasClass('glyphicon-plus')) {
                span.removeClass('glyphicon-plus').addClass('glyphicon-minus');
                $("#bodyTablaContratos tr[data-parent='" + id + "']").show();
            } else {
                span.removeClass('glyphicon-minus').addClass('glyphicon-plus');
                $("#bodyTablaContratos tr[data-parent='" + id + "']").hide();
            }

            cebreartablaContratos();
        });

        $(document).on('click keypress', '#bodyTablaContratos tr.fk_seleccionable', function (e) {

            $("#bodyTablaContratos tr.activa").removeClass('activa');
            $(this).addClass('activa');

            dom.btnAceptarContrato.removeAttr('disabled');

        });

        $('body').on('dblclick', '#bodyTablaContratos tr.fk_seleccionable', function (e) {
            $('#bodyTablaContratos tr.activa').removeClass('activa');
            $(this).addClass('activa');
            dom.btnAceptarContrato.removeAttr('disabled');
            $("#btnAceptarContrato").trigger("click");
        });

        $('#btnAceptarContrato').on('click keypress', function () {
            dom.txtIdContrato.val($('#bodyTablaContratos tr.activa').attr('id'));
            dom.txtDesContrato.val($('#bodyTablaContratos tr.activa').attr('data-t377_denominacion')).change();
        });

    }

    var deAttachEventosModalContrato = function () {

        $(document).off('click keypress', 'td.fk_extendible');

        $(document).off('click keypress', '#bodyTablaContratos tr.fk_seleccionable');

        $(document).off('dblclick', '#bodyTablaContratos tr.fk_seleccionable');

        $('#btnAceptarContrato').off();

    }

    var mostrarModalContrato = function () {

        //Se oculta el contenido inferior al lector de pantallas
        $('.ocultable2').attr('aria-hidden', 'true');

        //Se deshabilita el botón aceptar
        dom.btnAceptarContrato.attr('disabled', 'disabled');

        //Se limpian los filtros
        vaciarFiltrosContratos();
        limpiarFiltro(dom.txtClienteContratos);

        if (!$.fn.DataTable.isDataTable('#tablaContratos')) {
            initDatatableContrato();
        }
        else {

            $('#tablaContratos').DataTable().clear().draw();

            IB.procesando.ocultar();

            dom.modalContrato.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            dom.modalContrato.modal('show');

            setTimeout("$('#tablaContratos').DataTable().columns.adjust().draw();", 300);

        }        

    }

    var initDatatableContrato = function () {

        var dt = $('#tablaContratos').DataTable({
            columns: [
                        { "data": "t377_denominacion" },
                        { "data": "t306_idcontrato", "sClass": "text-right", render: $.fn.dataTable.render.number('.', ',', 0, '') },
                        { "data": "t377_idextension" },
                        { "data": "importe_producto", "sClass": "text-right", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "pendiente_producto", "sClass": "text-right", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "importe_servicio", "sClass": "text-right", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "pendiente_servicio", "sClass": "text-right", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                        { "data": "t302_denominacion" }
            ],
            "columnDefs": [
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            }],
            order: false,
            ordering: false,
            searching: true,
            data: [],            
            bFilter: false,
            paging: false,
            bInfo: false,
            procesing: true,
            scrollCollapse: true,
            scrollY: "40vh",
            scrollX: true,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },            
            rowCallback: function (row, data) {
                $('row').removeClass('odd').removeClass('even');
            },
            "initComplete": function (settings, json) {
                IB.procesando.ocultar();
                dom.modalContrato.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                dom.modalContrato.modal('show');
                setTimeout("$('#tablaContratos').DataTable().columns.adjust().draw();", 300);
            },
            "createdRow": function (row, data, rowIndex) {
                //Se añade el icono de expansión y se ocultan las filas de expansiones
                var icon = '<span class="fk_sin_extensiones"> </span>';
                var td = $(row).find('td:first-child');                                

                if (data.t377_idextension != 0) {
                    var texto = td.html();
                    $(row).html('<td colspan="8">' + texto + '</td>');
                    $(row).attr('data-parent', data.t306_idcontrato);
                    td = $(row).find('td:first-child');
                    icon = '<span class="fk_extension"></span>';
                    $(row).hide();
                } else {
                    if (data.con_extensiones) {
                        icon = '<span class="glyphicon-plus fk_con_extensiones"></span>';
                        td.addClass('fk_extendible');
                    } 
                    $(row).addClass('fk_seleccionable');
                    $(row).attr('id', data.t306_idcontrato);
                    $(row).attr('data-t377_denominacion', data.t377_denominacion);
                }
                
                td.prepend(icon);

            }

        });

    }

    var pintarTablaContratos = function (data) {

        //Se buscan oportunidades con extensión 
        var extensiones = $.grep(data, function (e) { return e.t377_idextension != 0; });

        for (var i = 0; i < data.length; i++) {
            if (data[i].t377_idextension == 0) {
                if ($.grep(extensiones, function (e) {
                return (e.t302_idcliente_contrato == data[i].t302_idcliente_contrato
                && e.t306_idcontrato == data[i].t306_idcontrato);
                }).length > 0) {
                    data[i].con_extensiones = true;
                }
            }
        }        

        dom.tablaContratos.DataTable().clear().draw();
        dom.tablaContratos.DataTable().rows.add(data).draw();

        cebreartablaContratos();

    }

    var cebreartablaContratos = function() {

        $('#bodyTablaContratos > tr:visible:odd').removeClass('odd').removeClass('even').addClass('odd');
        $('#bodyTablaContratos > tr:visible:even').removeClass('odd').removeClass('even').addClass('even');

    }

    var vaciarFiltrosContratos = function (excp) {
        //Si excp es distinto de null es para que no se vacie ese campo

        if (excp != "id") dom.idContratoM.val('');
        if (excp != "desc") dom.desContratoM.val('');

    }

    var vaciarTablaContratos = function () {

        dom.tablaContratos.DataTable().clear().draw();

    }

    //Fin funcionalidad modal contratos

    //Funcionalidad ayuda clientes multiple

    var volcarClientesSeleccionados = function (data) {

        dom.listaClientes.empty();

        if (data.length != 0) {

            dom.listaClientes.removeClass('fk_lista');

            data.forEach(function (item) {
                var li = "<li class='list-group-item' data-tipo='" + item.tipo + "' data-estadoF='" + item.estadoF + "' data-id='" + item.key + "'>" + item.value + "</li>";
                dom.listaClientes.append(li);
            });
            
        } else {
            dom.listaClientes.removeClass('fk_lista').addClass('fk_lista');
        }

        dom.listaClientes.change();
    }

    //Funcionalidad ayuda clientes multiple

    //Funcionalidades generales pantalla

    var limpiarFiltro = function (filtro) {

        filtro.attr('value', "");
        filtro.val("").change();
    }

    var volcarTxt = function (target, valor) {

        target.val(valor).change();

    }

    var volcarTxtValue = function (target, valor, id) {

        target.attr('value', id);
        target.val(valor).change();

    }

    //Fin funcionalidades generales pantalla

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        deAttachEvents: deAttachEvents,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        deAttachLiveEvents: deAttachLiveEvents,
        pintarTablaPartes: pintarTablaPartes,
        llenarModContrat: llenarModContrat,
        llenarNodosCR: llenarNodosCR,
        limpiarFiltrosProyectos: limpiarFiltrosProyectos,
        mostrarModalProyecto: mostrarModalProyecto,
        vaciarTablaProyectos: vaciarTablaProyectos,
        pintarTablaProyectos: pintarTablaProyectos,
        visualizarContenido: visualizarContenido,
        visualizarContenidoSR: visualizarContenidoSR,
        attachEventosListaDual: attachEventosListaDual,
        deAttachEventosListaDual: deAttachEventosListaDual,
        cambiarLinkCualif: cambiarLinkCualif,
        volcarProyectosSeleccionados: volcarProyectosSeleccionados,
        volcarClientesCriterio: volcarClientesCriterio,
        visualizarContenidoSR2: visualizarContenidoSR2,
        mostrarModalCR: mostrarModalCR,
        attachEventosModalCR: attachEventosModalCR,
        deAttachEventosModalCR: deAttachEventosModalCR,
        attachEventosModalContrato: attachEventosModalContrato,
        deAttachEventosModalContrato: deAttachEventosModalContrato,
        mostrarModalContrato: mostrarModalContrato,
        pintarTablaContratos: pintarTablaContratos,
        vaciarFiltrosContratos: vaciarFiltrosContratos,
        vaciarTablaContratos: vaciarTablaContratos,
        volcarClientesSeleccionados: volcarClientesSeleccionados,
        limpiarFiltro: limpiarFiltro,
        volcarTxt: volcarTxt,
        volcarTxtValue: volcarTxtValue
    }
})();