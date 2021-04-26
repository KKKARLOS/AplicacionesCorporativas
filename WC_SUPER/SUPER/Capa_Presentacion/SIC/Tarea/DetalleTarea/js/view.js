var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view = (function () {

    var _data;

    //Obtenemos la fecha actual
    var fechaActual = new Date();
    var dd = fechaActual.getDate();
    var mm = fechaActual.getMonth() + 1; //Enero es 0!
    var yyyy = fechaActual.getYear();
    fechaActual.setHours(0, 0, 0, 0);

    var dom = {
        theForm: $("#frmTareas"),
        btnRegresar: $("#btnRegresar"),
        btnFinalizarParticipacion: $("#btnFinalizarParticipacion"),
        btnAnularParticipacion: $("#btnAnularParticipacion"),
        btnFinalizarTarea: $("#btnFinalizarTarea"),
        btnAnularTarea: $("#btnAnularTarea"),
        linkDocumentacion: $("#linkDocumentacion"),
        linkInformacionAdicional: $("#linkInformacionAdicional"),
        divInformacionAdicional: $("#divInformacionAdicional"),
        ta207_comentario: $("#ta207_comentario"),
        ta207_observaciones: $("#ta207_observaciones"),
        btnGrabar: $("#btnGrabar"),
        fk_ParticipacionProfesional: $(".fk_ParticipacionProfesional"),
        fk_ltrParticipacionProfesional: ".fk_ParticipacionProfesional",
        modalEstadoParticipantes: $("#modal-estadoParticipantes"),
        spanProfesional: $("#spanProfesional"),
        rdbParticipacionEnCurso: $("#rdbParticipacionEnCurso"),
        rdbParticipacionFinalizada: $("#rdbParticipacionFinalizada"),
        rdbParticipacionAnulada: $("#rdbParticipacionAnulada"),
        btnAceptarModalEstadoPartipacion: $("#btnAceptarModalEstadoPartipacion"),
        btnltrAceptarModalEstadoPartipacion: "#btnAceptarModalEstadoPartipacion",
        rdbNuevoEstadoParticipacion: $("input[name=rdbEstados]:checked").val(),
        ta207_denominacion: $("#ta207_denominacion"),
        selectDenominacion: $("#selectDenominacion"),
        ta207_fechafinprevista: $("#ta207_fechafinprevista"),
        ta207_descripcion: $("#ta207_descripcion"),
        lblParticipantes: $("#lblParticipantes"),
        textareaMotivoAnulacion: $("#txtMotivoAnulacion"),
        divtextareaMotivoAnulacion: $("#divtextareaMotivoAnulacion"),
        lblSello: $("#lblSello"),
        fk_camposObligatorios: $(".fk_camposObligatorios"),
        modal_AnularTarea: $("#modal-anularTarea"),
        btnAceptar_anularTarea: $("#btnAceptar_anularTarea"),
        btnCancelar_anularTarea: $("#btnCancelar_anularTarea"),
        filasTablaParticipantes: $("#tblParticipantes tbody tr td").not(".dataTables_empty").parent(),
        numCaracteres: $('#numCaracteres'),
        divayudapersonasmulti1: $("#divayudapersonasmulti1")

    }


    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function detachEvents() {
        $(selector).off(event, callback);
    }

    function detachLiveEvents(event, selector, callback) {
        $(document).off(event, selector, callback);
    }

    function getEstadoParticipacion_usuarioConectado() {
        return $("#tblParticipantes tr[data-t001_idficepi_participante = " + IB.vars.perfilesEdicion.idficepi + "]").attr("data-ta214_estado");
    }

    function init(ta206_itemorigen, ta206_iditemorigen) {

        _ta206_itemorigen = ta206_itemorigen;
        _ta206_iditemorigen = ta206_iditemorigen;

        //Si es una insert el mindate es 0. Si es update el mindate es la fecha de creación (visualización del calendario)
        var minDate;

        //Si el modo de pantalla es edición, la fecha fin prevista no puede ser antes que la fecha de creación
        if (IB.vars.perfilesEdicion.modoPantalla == "E") {
            array = IB.vars.fechaCreacion.split("/");
            dia = array[0];
            mes = array[1];
            anyo = array[2];
            minDate = new Date(anyo, mes - 1, dia);
        }
            //Si el modo de pantalla es Alta, la fecha fin prevista será mayor o igual al día actual.(visualización del calendario)
        else
            minDate = 0;


        //Obtenemos la fecha actual
        var fechaActual = new Date();
        var dd = fechaActual.getDate();
        var mm = fechaActual.getMonth() + 1; //Enero es 0!
        var yyyy = fechaActual.getYear();
        fechaActual.setHours(0, 0, 0, 0);

        //Calendario en fecha fin prevista
        dom.ta207_fechafinprevista.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            viewMode: "months",
            yearRange: new Date().getFullYear() + ":2050",
            minDate: minDate,
            onClose: function (dateText, inst) {
                var FFEdate;
                try {
                    FFEdate = $.datepicker.parseDate('dd/mm/yy', dateText);
                } catch (e) {
                    $.when(IB.bsalert.fixedAlert("danger", "Formato de fecha incorrecto", "La fecha introducida no es correcta.")).then(function () {
                        dom.ta207_fechafinprevista.val("");
                    });
                    return;
                }
            }
        });

        //Validación del Datepicker
        dom.ta207_fechafinprevista.datepicker().bind('change', function () {
            if ($(this).val() != '') {
                //Si el modo pantalla es alta, no permitimos que introduzcan una fecha fin prevista anterior a la fecha actual. (método establecer fecha de forma manual)
                if (IB.vars.modoPantalla == "A" && $.datepicker.parseDate('dd/mm/yy', dom.ta207_fechafinprevista.val()) < fechaActual) {
                    IB.bsalert.toastdanger("No puedes introducir una fecha anterior a la fecha actual");
                    $(this).val("").focus();
                    return;
                }

                    //Si el modo pantalla es edición, no permitimos que la fecha fin prevista sea anterior que la fecha de creación. (método establecer fecha de forma manual)
                else if (IB.vars.modoPantalla == "E" && $.datepicker.parseDate('dd/mm/yy', dom.ta207_fechafinprevista.val()) < $.datepicker.parseDate('dd/mm/yy', IB.vars.fechaCreacion)) {
                    IB.bsalert.toastdanger("No puedes introducir una fecha anterior a la fecha de creación de la tarea");
                    $(this).val("").focus();
                    return;
                }
            }

            else {
                IB.bsalert.toastdanger("Tienes que introducir una fecha");
                return;
            }
        });


        setSello(IB.vars.ta207_estado);
        dom.theForm.find(".form-control").on("keyup change", function () { IB.vars.bcambios = true; });

        /*plugin multiselección*/

        //Ayuda de profesionales
        if (IB.vars.perfilesEdicion.soyLider || IB.vars.perfilesEdicion.soyFiguraSubarea || IB.vars.perfilesEdicion.soyAdministrador) {
            $("#lblParticipantes").on("click", function () {
                var $filaVacia = $("#tblParticipantes tbody tr").find(".dataTables_empty").parent();
                var $participantes = $("#tblParticipantes tbody tr").not($filaVacia);

                var lstSeleccionados = []
                $participantes.each(function () {
                    lstSeleccionados.push({ t001_idficepi: $(this).attr("data-t001_idficepi_participante"), profesional: $(this).find("td:nth-child(1)").html(), estado: $(this).attr("data-estado") })
                })
                dom.divayudapersonasmulti1.buscaprofmulti("option", "lstSeleccionados", lstSeleccionados);
                dom.divayudapersonasmulti1.buscaprofmulti("show")
            });
        }
        dom.divayudapersonasmulti1.buscaprofmulti({
            titulo: "Selección de profesionales",
            tituloContIzda: "Profesionales",
            tituloContDcha: "Profesionales seleccionados",
            notFound: "No se han encontrado resultados.",
            modulo: "SIC",
            tipoAyuda: "GeneralFicepi",
            autoSearch: false,
            autoShow: false,
            eliminarExistentes: false,

            onAceptar: function (data) {
                var html = "";
                var tablaOriginal = $("#tblParticipantes tbody tr");
                var table = $('#tblParticipantes').DataTable();
                var lista = [];
                var estado = "";
                var ta214_estado = "";

                //Sólo añadimos los nuevos. (descartamos los repetidos)
                for (var i = 0; i < data.length; i++) {
                    var _encontrado = false;

                    for (var j = 0; j < tablaOriginal.length; j++) {
                        var _encontrado = false;
                        estado = "N";
                        ta214_estado = "A";

                        if (tablaOriginal[j].getAttribute("data-t001_idficepi_participante") == data[i].t001_idficepi) {
                            ta214_estado = tablaOriginal[j].getAttribute("data-ta214_estado");
                            if (tablaOriginal[j].getAttribute("data-estado") != data[i].estado) {
                                if (data[i].estado == "X") {
                                    tablaOriginal[j].setAttribute("data-estado", "X");
                                }
                                else {
                                    tablaOriginal[j].setAttribute("data-estado", "E");
                                    _profEncontrado = true;

                                }
                            }

                            else {
                                _profEncontrado = true;
                                estado = tablaOriginal[j].getAttribute("data-estado");
                            }
                            break;
                        }
                    }

                    
                    //Si no está repetido, agregamos filas al Datatable con los nuevos registros.
                    if (!_encontrado) {
                        var olista = new Object();
                        olista.t001_idficepi = data[i].t001_idficepi;
                        olista.participante = data[i].profesional;
                        olista.ta214_estado = ta214_estado;
                        olista.estado = estado;
                        lista.push(olista);
                    }

                }

                if (table != null)
                    table.destroy();

                refreshDatatable(lista);
                aplicarEstiloUnderline($(".fk_ParticipacionProfesional"));
                manejarObjetosDOM.habilitarbtnGrabar();
            }

        });
        /*fin plugin */
    }
    function refreshDatatable(lista) {

        $('#tblParticipantes').DataTable({
            "language": {
                "url": "../../../../plugins/datatables/Spanish.txt",
            },
            "language": {
                "emptyTable": "No hay participantes"
            },
            "procesing": true,
            "paginate": false,
            "responsive": false,
            "autoWidth": false,
            "order": [],
            "scrollY": "89px",
            "scrollCollapse": false,
            "info": false,
            "searching": false,
            "data": lista,
            "columns": [
                { "data": "participante" },
                { "data": "ta214_estado" }

            ],

            "columnDefs": [
       {
           "targets": 0
       },

       {
           //estado
           "targets": 1,
           "render": {
               "display": function (data, type, row, meta) {
                   return "<span class='fk_ParticipacionProfesional' data-estado='" + row.ta214_estado + "'>" + GetLiteralEstadoParticipacion(row.ta214_estado) + "</span>";
               }
           }
       },

            ],

            "createdRow": function (row, data, index) {
                $(row).attr("id", index++).attr("data-t001_idficepi_participante", data.t001_idficepi).attr("data-ta214_estado", data.ta214_estado).attr("data-estado", data.estado);

            }
        })
    }

    //No se está usando
    function desactivarControles() {
        dom.btnFinalizarParticipacion.removeClass("show").addClass("hide");
        dom.btnAnularParticipacion.removeClass("show").addClass("hide");
        dom.btnGrabar.removeClass("show").addClass("hide");
        dom.btnFinalizarTarea.removeClass("show").addClass("hide");
        dom.btnAnularTarea.removeClass("show").addClass("hide");

        dom.ta207_denominacion.attr("disabled", "disabled");
        dom.selectDenominacion.attr("disabled", "disabled");
        

        dom.ta207_fechafinprevista.attr("disabled", "disabled");
        dom.ta207_descripcion.attr("disabled", "disabled");
        dom.ta207_observaciones.attr("disabled", "disabled");
        dom.ta207_comentario.attr("disabled", "disabled");
        dom.divtextareaMotivoAnulacion.removeClass("show").addClass("hide");


        dom.linkDocumentacion.removeClass("underline").addClass("linkdisabled");
        dom.linkDocumentacion.off("click");
        dom.lblParticipantes.removeClass("underline").addClass("linkdisabled");
        dom.lblParticipantes.off("click");
        dom.fk_ParticipacionProfesional.removeClass("underline").addClass("linkdisabled");
        //dom.fk_ParticipacionProfesional.off("click");
        $(document, ".fk_ParticipacionProfesional").off("click");

        dom.fk_camposObligatorios.removeClass("show").addClass("hide");
    }

    function estilosMotivoAnulacion() {
        dom.divtextareaMotivoAnulacion.removeClass("hide").addClass("show");
    }

    function textoMotivoAnulacion(texto) {
        $("#textareaMotivoAnulacion").text(texto);
    }

    function btnCancelar_anularTarea_onClick() {
        clearMotivoAnulacion();
    }

    function setSello(estado) {
        dom.lblSello.addClass(setSelloEstado(estado));
        dom.lblSello.attr("data-after", GetLiteralEstadoTarea(estado));
    }
    var manejarObjetosDOM = (function () {

        function marcarEliminado() {
            $("#tblParticipantes tr[data-marcadoEliminar=true]").css("text-decoration", "line-through").css("color", "rgb(169, 68, 66)");
        }

        function habilitarDocumentacion() {
            //se habilita el enlace a la documentación
            dom.linkDocumentacion.removeClass("linkdisabled").addClass("underline");
            dom.linkDocumentacion.on("click");
        }

        function anuladasEstadoEdicion() {
            dom.divtextareaMotivoAnulacion.removeClass("hide").addClass("show");
            dom.lblSello.removeClass("blanco").addClass("rojo");
            dom.lblParticipantes.removeClass("underline").addClass("linkdisabled");
            dom.lblParticipantes.off("click");
        }

        function selloEstadoFinalizado() {
            dom.lblSello.removeClass("blanco").addClass("gris");
            dom.lblParticipantes.removeClass("underline").addClass("linkdisabled");
            dom.lblParticipantes.off("click");
        }

        function soyParticipante() {
            dom.ta207_comentario.removeAttr("disabled");
            dom.btnGrabar.removeClass("hide").addClass("show-inline");
            dom.btnAnularParticipacion.removeClass("hide").addClass("show-inline");
            dom.btnFinalizarParticipacion.removeClass("hide").addClass("show-inline");
            //dom.lblParticipantes.removeClass("underline").addClass("linkdisabled");
            //dom.lblParticipantes.off("click");
            dom.lblParticipantes.css("visibility", "hidden");
        }

        function quitarParticipante() {
            dom.ta207_comentario.attr("disabled", "disable");
            dom.btnAnularParticipacion.removeClass("show").addClass("hide");
            dom.btnFinalizarParticipacion.removeClass("show").addClass("hide");
        }

        function controlTotal() {
            dom.fk_camposObligatorios.removeClass("hide").addClass("show-inline");
            dom.btnGrabar.removeClass("hide").addClass("show-inline");
            dom.ta207_denominacion.removeAttr("disabled");

            //dom.selectDenominacion.removeAttr("disabled");
            
            dom.ta207_fechafinprevista.removeAttr("disabled");
            dom.ta207_descripcion.removeAttr("disabled");
            dom.ta207_observaciones.removeAttr("disabled");
            dom.lblParticipantes.removeClass("linkdisabled").addClass("underline");
            //$("#lblParticipantes").on("click");
            dom.lblParticipantes.css("visibility", "visible");
            aplicarEstiloUnderline($(".fk_ParticipacionProfesional"));
            attachLiveEvents("click", dom.fk_ltrParticipacionProfesional, fk_ParticipacionProfesional_onClick);
        }

        function controlTotalEdicion() {
            $("#divNumtarea").css("display", "block");
            $("#divFechaCreacion").css("display", "block");
            $("#divFechaFinReal").css("display", "block");
            dom.btnFinalizarTarea.removeClass("hide").addClass("show-inline");
            dom.btnAnularTarea.removeClass("hide").addClass("show-inline");
            dom.lblParticipantes.css("visibility", "visible");
            $("#ta207_denominacion").attr("disabled", "disabled");
            
        }

        function controlBotonesTarea() {
            dom.btnFinalizarTarea.removeClass("hide").addClass("show-inline");
            dom.btnAnularTarea.removeClass("hide").addClass("show-inline");
        }

        function controlTotalAlta() {
            //$("#divNumtarea").css("display", "none");            
            $("#divFechaFinReal").css("display", "none");
            dom.btnGrabar.removeClass("hide").addClass("show-inline");
            dom.lblParticipantes.css("visibility", "visible");
            $("#selectDenominacion").removeAttr("disabled");
        }

        function obtenerParticipantes() {
            var selector = $("#tblParticipantes tbody tr td").not(".dataTables_empty").parent();
            var lista = [];

            selector.each(function () {
                var oParticipantes = new Object();
                oParticipantes.t001_idficepi_participante = $(this).attr("data-t001_idficepi_participante");
                oParticipantes.ta214_estado = $(this).attr("data-ta214_estado");
                lista.push(oParticipantes);
            });
            return lista;
        }

        function deshabilitarbtnGrabar() {
            $("#btnGrabar").attr("disabled", "disabled");
        }

        function habilitarbtnGrabar() {
            $("#btnGrabar").removeAttr("disabled");
        }

        function pintaInfoCRM(data) {
            
            //todo poner los campos a mostrar
            switch (_ta206_itemorigen) {
                case "O":
                    $("#lblOportunidadSolic").text("Oportunidad");
                    $("#acc-txtOportunidadSolic").val(data.iditemorigen);
                    $("#acc-txtDenominacionSolic").val(data.denominacion);
                    $("#acc-txtClienteSolic").val(data.cuenta);


                    $("#txtIditemorigen_cab").val(data.iditemorigen);
                    $("#txtDenominacion_cab").val(data.denominacion);
                    $("#txtCuenta_cab").val(data.cuenta);
                    $("#div_txtCuenta_cab").css("display", "block");
                    $("#txtOportExt").val(data.num_oportunidad);
                    $("#txtdenOportExt_cab").val(data.den_oportunidad);
                    $("#txtComercial_cab").val(data.comercial);
                    $("#txtOrganizacionComercial_cab").val(data.organizacionComercial);
                    $("#txtGestorProduccion_cab").val(data.gestorProduccion_nombre);
                    $("#txtImporte_cab").val(data.importe);
                    $("#txtRentabilidad_cab").val(data.rentabilidad);
                    $("#txtProbabilidadExisto_cab").val(data.exito);
                    $("#txtAreaConTecno_cab").val(data.areaConTecnologico);
                    $("#txtAreaConSectorial_cab").val(data.areaConSectorial);
                    $("#txtDuracionProyecto_cab").val(data.duracionProyecto);
                    $("#txtFechaCierre_cab").val(data.fechaCierre);
                    $("#txtEtapaVentas_cab").val(data.etapaVentas);
                    $("#txtEstado_cab").val(data.estado);
                    $("#txtCentroResponsabilidad_cab").val(data.centroResponsabilidad)




                    break;
                case "E":
                    $("#lblOportunidadSolic").text("Extensión");
                    $("#acc-txtOportunidadSolic").val(data.iditemorigen);
                    $("#acc-txtDenominacionSolic").val(data.denominacion);
                    $("#acc-txtClienteSolic").val(data.cuenta);


                    $("#txtIditemorigen_cab").val(data.iditemorigen);
                    $("#txtDenominacion_cab").val(data.denominacion);
                    $("#txtCuenta_cab").val(data.cuenta);
                    $("#div_txtCuenta_cab").css("display", "block");
                    $("#txtOportExt").val(data.num_oportunidad);
                    $("#txtdenOportExt_cab").val(data.den_oportunidad);
                    $("#txtComercial_cab").val(data.comercial);
                    $("#txtOrganizacionComercial_cab").val(data.organizacionComercial);
                    $("#txtGestorProduccion_cab").val(data.gestorProduccion_nombre);
                    $("#txtImporte_cab").val(data.importe);
                    $("#txtRentabilidad_cab").val(data.rentabilidad);
                    $("#txtProbabilidadExisto_cab").val(data.exito);
                    $("#txtAreaConTecno_cab").val(data.areaConTecnologico);
                    $("#txtAreaConSectorial_cab").val(data.areaConSectorial);
                    $("#txtDuracionProyecto_cab").val(data.duracionProyecto);
                    $("#txtFechaCierre_cab").val(data.fechaCierre);
                    $("#txtEtapaVentas_cab").val(data.etapaVentas);
                    $("#txtEstado_cab").val(data.estado);
                    $("#txtCentroResponsabilidad_cab").val(data.centroResponsabilidad)



                    break;
                case "P":
                    $("#lblOportunidadSolic").text("Objetivo");
                    $("#acc-txtOportunidadSolic").val(data.iditemorigen);
                    $("#acc-txtDenominacionSolic").val(data.denominacion);
                    $("#acc-txtClienteSolic").val(data.cuenta);

                    $("#txtIditemorigen_cab").val(data.iditemorigen);
                    $("#txtDenominacion_cab").val(data.denominacion);

                    $("#txtOferta_cab").val(data.oferta);
                    $("#txtFechaInicio_cab").val(data.fechaInicio);
                    $("#txtContataPrevista_cab").val(data.contratacionPrevista);
                    $("#txtFechaFin_cab").val(data.fechaFin);
                    $("#txtCoste_cab").val(data.costePrevisto);
                    $("#txtResultado_cab").val(data.resultado);

                    $("#txtEstado_cabObj").val(data.estado);
                    $("#txtOrgComercial_Objetivo").val(data.organizacionComercial);
                    $("#txtComercial_cabObjetivo").val(data.comercial);
                    $("#txtDescObjetivo").val(data.desc_objetivo);

                    $("#txtTipoNegocio").val(data.tipo_negocio);

                    break;

                case "S":
                    $("#divInformacionAdicional").css("display", "none");
                    $("#acc-txtOportunidadSolic").val(data.ta206_idsolicitudpreventa);
                    $("#acc-txtDenominacionSolic").val(data.ta206_denominacion);
                    break;

            }

        }


        return {
            habilitarDocumentacion: habilitarDocumentacion,
            anuladasEstadoEdicion: anuladasEstadoEdicion,
            selloEstadoFinalizado: selloEstadoFinalizado,
            soyParticipante: soyParticipante,
            quitarParticipante: quitarParticipante,
            controlTotal: controlTotal,
            controlTotalEdicion: controlTotalEdicion,
            controlTotalAlta: controlTotalAlta,
            obtenerParticipantes: obtenerParticipantes,
            controlBotonesTarea: controlBotonesTarea,
            deshabilitarbtnGrabar: deshabilitarbtnGrabar,
            habilitarbtnGrabar: habilitarbtnGrabar,
            pintaInfoCRM: pintaInfoCRM
        }
    })();

    //Quitamos la clase "requerido" en cuanto se mete valor en el campo
    dom.theForm.find(":required").on("keyup change", function () {
        var obj = $(this);
        if (obj.val() != null && obj.val().length > 0) obj.removeClass("requerido");
    })



    //Validación requerida del formulario
    function requiredValidation() {

        var valid = true;

        dom.theForm.find(":required").each(function () {

            if ($(this).val() == null || $(this).val().length == 0) {
                $(this).addClass("requerido");
                valid = false;
            }

        });

        if (!valid)
            IB.bsalert.toastdanger("Debes cumplimentar los campos obligatorios. Están identificados con un asterisco.");

        return valid;
    }

    //Obtenemos los valores del formulario para ser accedidos desde app.js
    function getViewValues() {
        oTarea = new Object();

        oTarea.ta204_idaccionpreventa = IB.vars.ta204_idaccionpreventa;
        oTarea.ta207_idtareapreventa = IB.vars.ta207_idtareapreventa;
        oTarea.ta207_estado = IB.vars.ta207_estado;
        
        if ($("#selectDenominacion option:selected").val() === "-1") {
            oTarea.ta219_idtipotareapreventa = null;
            oTarea.ta207_denominacion = $("#ta207_denominacion").val();
        }
        else {
            oTarea.ta219_idtipotareapreventa = parseInt($("#selectDenominacion option:selected").val());
            oTarea.ta207_denominacion = "";
        }
                
        oTarea.ta207_observaciones = dom.ta207_observaciones.val();
        oTarea.ta207_fechaprevista = $.datepicker.parseDate('dd/mm/yy', dom.ta207_fechafinprevista.val());
        oTarea.ta207_descripcion = $("#ta207_descripcion").val();
        oTarea.ta207_comentario = $("#ta207_comentario").val();
        oTarea.ta207_motivoanulacion = $("#txtMotivoAnulacion").val();
        if (IB.vars.modoPantalla == "A") {
            oTarea.uidDocumento = IB.vars.uidDocumento;
        }



        return oTarea;
    }

    function setSelloEstado(estado) {

        var clase;
        switch (estado) {
            case "A":
                clase = "verde";
                break;
            case "F":
                clase = "gris";
                break;
            case "X":
            case "FS":
            case "XS":
                clase = "rojo";
                break;
        }

        return clase;
    }


    function setFechaCreacion() {
        $("#ta207_fechacreacion").val(moment(fechaActual).format('DD/MM/YYYY'));
    }

    function setIdTarea(idTarea) {
        $("#ta207_idtareapreventa").val(idTarea);
    }

    function plegar_desplegar() {
        if (dom.divInformacionAdicional.hasClass("in"))
            $(dom.linkInformacionAdicional).find("i").removeClass("fa-minus").addClass("fa-plus")
        else
            $(dom.linkInformacionAdicional).find("i").removeClass("fa-plus").addClass("fa-minus")
    }

    function aplicarEstiloUnderline(selector) {
        selector.addClass("underline");
    }

    function switchModoPantallaEdicion() {
        $("#divFechaCreacion").css("display", "block");
        $("#divFechaFinReal").css("display", "block");
    }

    function initDatatableParticipantes(idtarea) {

        var defer = $.Deferred();

        $('#tblParticipantes').DataTable({
            "language": {
                "url": "../../../../plugins/datatables/Spanish.txt",
            },
            "language": {
                "emptyTable": "No hay participantes"
            },
            "procesing": true,
            "paginate": false,
            "responsive": false,
            "autoWidth": false,
            "order": [],
            "scrollY": "89px",
            "scrollCollapse": false,
            "info": false,
            "searching": false,
            "destroy": true,
            "ajax": {
                "url": "Default.aspx/obtenerParticipantes",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ ta207_idtareapreventa: idtarea }); },
                "dataSrc": function (data) {
                    _data = JSON.parse(data.d);
                    return _data;
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
                }
            },

            "columns": [
                { "data": "participante" },
                { "data": "ta214_estado" }

            ],

            "columnDefs": [
                {
                    "targets": 0
                },

                {
                    //estado
                    "targets": 1,
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<span class='fk_ParticipacionProfesional' data-estado='" + row.ta214_estado + "'>" + GetLiteralEstadoParticipacion(row.ta214_estado) + "</span>";
                        }
                    }
                },

            ],

            "createdRow": function (row, data, index) {
                $(row).attr("id", index++).attr("data-t001_idficepi_participante", data.t001_idficepi_participante).attr("data-ta214_estado", data.ta214_estado).attr("data-estado", "E");

            },

            "initComplete": function (settings, json) {
                defer.resolve();
            }

        })

        return defer;
    }

    function initModal_AnularTarea() {
        dom.modal_AnularTarea.modal("show");
    }

    function fk_ParticipacionProfesional_onClick() {

        dom.modalEstadoParticipantes.modal("show");

        dom.rdbParticipacionEnCurso.next().text(GetLiteralEstadoParticipacion("A"));
        dom.rdbParticipacionFinalizada.next().text(GetLiteralEstadoParticipacion("F"));
        dom.rdbParticipacionAnulada.next().text(GetLiteralEstadoParticipacion("X"));

        dom.spanProfesional.text($(this).parent().prev().text());
        dom.spanProfesional.attr("data-id", $(this).parent().parent().attr("id"));

        switch ($(this).attr("data-estado")) {
            case "A":
                dom.rdbParticipacionEnCurso.prop("checked", true);
                break;

            case "F":
                dom.rdbParticipacionFinalizada.prop("checked", true);
                break;

            case "X":
                dom.rdbParticipacionAnulada.prop("checked", true);
                break;

            default:
                IB.bsalert.fixedAlert("danger", "Error de estado", "Estado de participación no contemplado")
                break;
        }

    }

    function lblParticipantes_onClick() {

    }

    function btnAceptarModalEstadoPartipacion_onClick() {

        var opcionSeleccionada = $("input[name=rdbEstados]:checked").val();
        var filaSeleccionada = $("#spanProfesional").attr("data-id");

        var table = $('#tblParticipantes').DataTable();
        var row = table.row(filaSeleccionada);
        var oData = row.data();

        oData.ta214_estado = opcionSeleccionada;
        //le pasamos false al método draw para mantenernos en la posición actual
        row.data(oData).draw(false);

        aplicarEstiloUnderline($(".fk_ParticipacionProfesional"));
        manejarObjetosDOM.habilitarbtnGrabar();

        //actualizo el data-estado de la fila (revisar)
        $("#tblParticipantes tr[id='" + filaSeleccionada + "']")
            .attr("data-ta214_estado", opcionSeleccionada);
    }

    function clearMotivoAnulacion() {
        $("#textareaMotivoAnulacion").val("").focus();
    }

    function actualizar_data_estado_participantes() {
        $("#tblParticipantes tr").attr("data-estado", "E");
    }

    function opcionesCombo() {
        if ($("#selectDenominacion option:selected").val() === "-1") {
            $("#divinputDenominacion").removeClass("hide").addClass("show");                        
            $("#asteriscoDenominacion").css("display", "initial");
            $("#ta207_denominacion").attr("required", "required");
        }
        else {
            $("#divinputDenominacion").removeClass("show").addClass("hide");                        
            $("#asteriscoDenominacion").css("display", "none");
            $("#ta207_denominacion").removeAttr("required");
        }
    }

    function grabarEstadoParticipantes() {
        $("#tblParticipantes tr").attr("data-estado", "E");
    }

    function GetLiteralEstadoParticipacion(estado) {
        switch (estado) {
            //Participación
            case "A": return "En curso";
            case "X": return "Anulada";
            case "F": return "Finalizada";

                //Herencia de solicitud
            case "FS": return "Cerrada";
            case "XS": return "Cerrada";

                //Herencia de acción
            case "FA": return "Cerrada";
            case "XA": return "Cerrada";

                //Herencia de tarea
            case "XT": return "Cerrada";
            case "FT": return "Cerrada";
            default: return "";
        }
    }

    //Obtiene el literal del estado pasado por parámetro
    function GetLiteralEstadoTarea(estado) {
        switch (estado) {
            case "A": return "Abierta";
            case "F": return "Finalizada";
            case "FS": return "Cerrada";
            case "FA": return "Cerrada";
            case "X": return "Anulada";
            case "XS": return "Cerrada";
            case "XA": return "Cerrada";
            default: return "";
        }
    }

    //Establece el contador de documentos
    function setContadorDocumentos(data) {
        $("#spanNumDocumentos").text(data);
    }

    function TiposTareaBloquear() {
        $("#selectDenominacion").attr("disabled", "disabled");
        $("#ta207_denominacion").attr("disabled", "disabled");
        
    }

    return {
        dom: dom,
        init: init,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        detachLiveEvents: detachLiveEvents,
        desactivarControles: desactivarControles,
        textoMotivoAnulacion: textoMotivoAnulacion,
        plegar_desplegar: plegar_desplegar,
        fk_ParticipacionProfesional_onClick: fk_ParticipacionProfesional_onClick,
        btnAceptarModalEstadoPartipacion_onClick: btnAceptarModalEstadoPartipacion_onClick,
        getViewValues: getViewValues,
        manejarObjetosDOM: manejarObjetosDOM,
        initDatatableParticipantes: initDatatableParticipantes,
        requiredValidation: requiredValidation,
        initModal_AnularTarea: initModal_AnularTarea,
        switchModoPantallaEdicion: switchModoPantallaEdicion,
        setContadorDocumentos: setContadorDocumentos,
        getEstadoParticipacion_usuarioConectado: getEstadoParticipacion_usuarioConectado,
        clearMotivoAnulacion: clearMotivoAnulacion,
        lblParticipantes_onClick: lblParticipantes_onClick,
        GetLiteralEstadoParticipacion: GetLiteralEstadoParticipacion,
        _data: _data,
        aplicarEstiloUnderline: aplicarEstiloUnderline,
        setFechaCreacion: setFechaCreacion,
        setSello: setSello,
        setIdTarea: setIdTarea,
        actualizar_data_estado_participantes: actualizar_data_estado_participantes,
        opcionesCombo: opcionesCombo,
        grabarEstadoParticipantes: grabarEstadoParticipantes,
        TiposTareaBloquear: TiposTareaBloquear
    }

})();


