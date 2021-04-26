var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.viewAccion = (function (oAccion) {

    var _ta206_itemorigen;
    var _ta206_iditemorigen;

    var dom = {
        txtIditemorigen_cab: $("#txtIditemorigen_cab"),
        txtDenominacion_cab: $("#txtDenominacion_cab"),
        txtCuenta_cab: $("#txtCuenta_cab"),
        div_txtCuenta_cab: $("#div_txtCuenta_cab"),
        txtOportExt: $("#txtOportExt"),
        txtdenOportExt_cab: $("#txtdenOportExt_cab"),

        linkInformacionAdicional: $("#linkInformacionAdicional"),
        divInformacionAdicional: $("#divInformacionAdicional"),

        txtComercial_cabObjetivo: $("#txtComercial_cabObjetivo"),
        txtEstado_cabObj: $("#txtEstado_cabObj"),
        txtDescObjetivo: $("#txtDescObjetivo"),
        txtOrgComercial_Objetivo: $("#txtOrgComercial_Objetivo"),
        txtTipoNegocio: $("#txtTipoNegocio"),

        txtComercial_cab: $("#txtComercial_cab"),
        txtOrganizacionComercial_cab: $("#txtOrganizacionComercial_cab"),
        txtGestorProduccion_cab: $("#txtGestorProduccion_cab"),
        txtImporte_cab: $("#txtImporte_cab"),
        txtRentabilidad_cab: $("#txtRentabilidad_cab"),
        txtProbabilidadExisto_cab: $("#txtProbabilidadExisto_cab"),
        txtAreaConTecno_cab: $("#txtAreaConTecno_cab"),
        txtAreaConSectorial_cab: $("#txtAreaConSectorial_cab"),
        txtDuracionProyecto_cab: $("#txtDuracionProyecto_cab"),
        txtFechaCierre_cab: $("#txtFechaCierre_cab"),
        txtFechaLimitePresentacion_cab: $("#txtFechaLimitePresentacion_cab"),
        txtEtapaVentas_cab: $("#txtEtapaVentas_cab"),
        txtEstado_cab: $("#txtEstado_cab"),
        txtCentroResponsabilidad_cab: $("#txtCentroResponsabilidad_cab"),

        txtOferta_cab: $("#txtOferta_cab"),
        txtFechaInicio_cab: $("#txtFechaInicio_cab"),
        txtContataPrevista_cab: $("#txtContataPrevista_cab"),
        txtFechaFin_cab: $("#txtFechaFin_cab"),
        txtCoste_cab: $("#txtCoste_cab"),
        txtResultado_cab: $("#txtResultado_cab"),

        divAccionContainer: $("#divAccionContainer"),

        lblArea: $("#lblArea"),
        lblSubarea: $("#lblSubarea"),
        cmbUnidad: $("#cmbUnidad"),
        cmbArea: $("#cmbArea"),
        cmbSubarea: $("#cmbSubarea"),
        cmbTipoAccion: $("#cmbTipoAccion"),
        txtRespSubarea: $("#txtRespSubarea"),
        lblRespSubarea: $("#lblRespSubarea"),

        txtLider: $("#txtLider"),
        btnAyudaLider: $("#btnAyudaLider"),
        divAyudaLider: $("#divAyudaLider"),
        btnOtrosLideres: $("#btnOtrosLideres"),
        txtDenominacion: $("#txtDenominacion"),
        txtObservaciones: $("#txtObservaciones"),
        dteFFE: $("#dteFFE"),
        dteFFEM: $("#dteFFEM"),
        dteFC: $("#dteFC"),
        lbldteFFR: $("#lbldteFFR"),
        dteFFR: $("#dteFFR"),
        estado: $("#estado"),
        estadomin: $("#estadomin"),

        btnTareas: $("#btnTareas"),
        lblTareasCount: $("#lblTareasCount"),
        btnDocumentacion: $("#btnDocumentacion"),
        lblDocumentosCount: $("#lblDocumentosCount"),

        theForm: $("#frmAcciones"),
        btnGrabar: $("#btnGrabar"),
        btnCerrar: $("#btnCerrar"),
        btnFinalizarAccion: $("#btnFinalizarAccion"),
        btnAnularAccion: $("#btnAnularAccion"),
        btnReplicarAccion: $("#btnReplicarAccion"),
        btnAutoAsignar: $("#btnAutoAsignar"),
        btnCrearAccionHermana: $("#btnCrearAccionHermana"),

        modal_anularAccion: $("#modal_anularAccion"),
        btnAceptar_anularAccion: $("#btnAceptar_anularAccion"),
        modal_txtMotivoAnulacion: $("#modal_txtMotivoAnulacion"),
        numCaracteres_anularAccion: $('#numCaracteres_anularAccion'),

        modal_replicarAccion: $("#modal_replicarAccion"),
        cmbTipoAccion_RA: $("#cmbTipoAccion_RA"),
        txtDenominacion_RA: $("#txtDenominacion_RA"),
        txtObservaciones_RA: $("#txtObservaciones_RA"),
        dteFFE_RA: $("#dteFFE_RA"),
        dteFFEM_RA: $("#dteFFEM_RA"),
        btnAceptar_RA: $("#btnAceptar_RA"),

        divtextareaMotivoAnulacion: $("#divtextareaMotivoAnulacion"),
        textareaMotivoAnulacion: $("#textareaMotivoAnulacion"),

        modal_finalizarAccion: $("#modal_finalizarAccion"),
        btnAceptar_finalizarAccion: $("#btnAceptar_finalizarAccion"),

        //ta201_obligalider --> Esta mierda es de D.Velazquez. Ahora todos los combos de la pantalla van con la propiedad ta201_obligalider, cuando solo hace falta para el combo de subareas.
        //Además, la llamada al servicio Services/Listas.asmx devuelve siempre la propiedad.
        //Ir dejando morir el servicio, no usarlo en ningun sitio más. Vamos a intentar que esta porqueria no salpique al resto del proyecto.
        cmboption: function (data) {
            if (data.Key === "") {
                return "<option ta201_obligalider=" + data.ta201_obligalider + " value='' selected hidden>" + data.Value; + "</option>";
            }
            else {
                return "<option ta201_obligalider=" + data.ta201_obligalider + " value='" + data.Key + "'>" + data.Value; + "</option>";
            }
        },

        cmboptionAccion: function (data) {
            if (data.ta205_idtipoaccionpreventa === "") {
                return "<option ta205_plazominreq=" + data.ta205_plazominreq + " value='' selected hidden>" + data.ta205_denominacion; + "</option>";
            }
            else {
                return "<option ta205_plazominreq=" + data.ta205_plazominreq + " value='" + data.ta205_idtipoaccionpreventa + "'>" + data.ta205_denominacion; + "</option>";
            }
        }

    }


    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function init(ta206_itemorigen, ta206_iditemorigen) {

        _ta206_itemorigen = ta206_itemorigen;
        _ta206_iditemorigen = ta206_iditemorigen;


        //Calendario en fecha fin estipulada
        dom.dteFFE.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            yearRange: new Date().getFullYear() + ":2050",
            //minDate: 0,

            beforeShow: function (input, inst) {
                //$(inst.dpDiv).removeClass('calendar-off');
            },
            onClose: function (dateText, inst) {
                var FFEdate;
                try {
                    FFEdate = $.datepicker.parseDate('dd/mm/yy', dateText);
                } catch (e) {
                    $.when(IB.bsalert.fixedAlert("danger", "Formato de fecha incorrecto", "La fecha introducida no es correcta.")).then(function () {
                        dom.dteFFE.val("");
                    });
                    return;
                }
            }

        });

        //Calendario en fecha fin estipulada de modal de Replicar Acción
        dom.dteFFE_RA.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            yearRange: new Date().getFullYear() + ":2050",
            //minDate: 0,

            beforeShow: function (input, inst) {
                //$(inst.dpDiv).removeClass('calendar-off');
            },
            onClose: function (dateText, inst) {
                var FFEdate;
                try {
                    FFEdate = $.datepicker.parseDate('dd/mm/yy', dateText);
                } catch (e) {
                    $.when(IB.bsalert.fixedAlert("danger", "Formato de fecha incorrecto", "La fecha introducida no es correcta.")).then(function () {
                        dom.dteFFE_RA.val("");
                    });
                    return;
                }
            }

        });

        //Ayuda de lider
        var options = {
            titulo: "Selección de líder",
            modulo: "SIC",
            autoSearch: true,
            autoShow: false,
            extensibleSearch: false,
            tipoAyuda: "lideressubarea",
            notFound: "No se han encontrado líderes para el subárea seleccionado.",
            onSeleccionar: function (data) {
                setLider(data.idficepi, data.profesional);
                dom.txtLider.trigger("change");
            }
        };

        dom.divAyudaLider.buscaprof(options);
        dom.btnAyudaLider.on("click", function () {

            if ($(this).hasClass("linkdisabled")) return; //está desactivado

            var ta201_idsubareapreventa = getComboSelectedOption(dom.cmbSubarea);

            if (ta201_idsubareapreventa == null || ta201_idsubareapreventa == "") {
                IB.bsalert.toastwarning("Para asignar lider primero debes seleccionar un subárea preventa");
                return;
            }

            var extensibleSearch = !(getAttrObligalider() === 'true');
            var o = {
                ta201_idsubareapreventa: ta201_idsubareapreventa
            }
            dom.divAyudaLider.buscaprof("option", "searchParams", o);
            dom.divAyudaLider.buscaprof("option", "extensibleSearch", extensibleSearch);
            dom.divAyudaLider.buscaprof("option", "tipoAyuda", "lideressubarea");
            dom.divAyudaLider.buscaprof("show");
        });

        dom.btnOtrosLideres.on("click", function () {

            IB.procesando.mostrar();

            $("#modal_otrosLideres tbody").html("");
            var ta201_idsubareapreventa = getComboSelectedOption(dom.cmbSubarea);

            if (ta201_idsubareapreventa == null || ta201_idsubareapreventa == "") {
                IB.bsalert.toastwarning("Para mostrar la ventana 'Otros líderes de la solicitud' primero debes seleccionar un subárea preventa");
                IB.procesando.ocultar();
                return;
            }

            var payload = {
                ta206_iditemorigen: IB.vars.iditemorigen,
                ta206_itemorigen: IB.vars.itemorigen,
                ta204_idaccionpreventa: IB.vars.ta204_idaccionpreventa,
                ta201_idsubareapreventa: ta201_idsubareapreventa
            }
            if (typeof payload.ta204_idaccionpreventa == "string") payload.ta204_idaccionpreventa = null;

            //Obtener los datos y show modal
            IB.DAL.post(null, "GetLideresSolicitud", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        var html = "";

                        data.forEach(function (item) {
                            html += "<tr><td>" + item.ta205_denominacion + "</td>" +
                                         "<td>" + item.areaPreventa + "</td>" +
                                         "<td>" + item.subareaPreventa + "</td>";
                            if (item.posibleLider)
                                html += "<td><a class='underline' data-idficepi='" + item.t001_idficepi_lider + "'>" + item.profesional + "</a></td>";
                            else
                                html += "<td>" + item.profesional + "</td>";
                            html += "</tr>";
                        })
                        $("#modal_otrosLideres tbody").html(html);
                        $("#modal_otrosLideres").modal("show");
                    });
                }

            );


        });
        $("#modal_otrosLideres tbody").on("click", "a", function () {

            setLider($(this).attr("data-idficepi"), $(this).html());
            $("#modal_otrosLideres").modal("hide");
            dom.txtLider.trigger("change");
        })


        $(".chevron_toggleable").on("click", function () { $(this).toggleClass("fa-chevron-down fa-chevron-up") });

        dom.theForm.find(".form-control").on("keyup change", function () { IB.vars.bcambios = true; })
        $(document).on("keyup change", "select[required], input[required], textarea[required]", function () {
            var obj = $(this);
            if (obj.val() != null && obj.val().length > 0) obj.removeClass("requerido");
        });


        dom.modal_txtMotivoAnulacion.on("keyup", modal_txtMotivoAnulacion_onKeyUp);

        dom.linkInformacionAdicional.on("click", plegar_desplegar);

    }

    function plegar_desplegar() {
        if (dom.divInformacionAdicional.hasClass("in"))
            $(dom.linkInformacionAdicional).find("i").removeClass("fa-minus").addClass("fa-plus")
        else
            $(dom.linkInformacionAdicional).find("i").removeClass("fa-plus").addClass("fa-minus")
    }

    function liderObligatorio() {
        $("#asteriscoLider").removeClass("hide").addClass("inline");
        $("#btnAyudaLider").removeClass("linkdisabled");
        $("#txtLider").attr("required", "required");
    }

    function liderNoSeleccionable() {
        $("#asteriscoLider").removeClass("show").addClass("hide");
        $("#btnAyudaLider").addClass("linkdisabled");
        $("#txtLider").removeAttr("required");
        $("#txtLider").removeClass("requerido");
    }
    function liderOpcional() {
        $("#asteriscoLider").removeClass("show").addClass("hide");
        $("#btnAyudaLider").removeClass("linkdisabled");
        $("#txtLider").removeAttr("required");
        $("#txtLider").removeClass("requerido");
    }

    function pintaInfoCRM(data) {

        switch (_ta206_itemorigen) {
            case "O":
            case "E":
                dom.txtIditemorigen_cab.val(data.iditemorigen);
                dom.txtDenominacion_cab.val(data.denominacion);
                dom.txtCuenta_cab.val(data.cuenta);
                dom.div_txtCuenta_cab.css("display", "block");
                dom.txtOportExt.val(data.num_oportunidad);
                dom.txtdenOportExt_cab.val(data.den_oportunidad);
                dom.txtComercial_cab.val(data.comercial);
                dom.txtOrganizacionComercial_cab.val(data.organizacionComercial);
                dom.txtGestorProduccion_cab.val(data.gestorProduccion_nombre);
                dom.txtImporte_cab.val(data.importe);
                dom.txtRentabilidad_cab.val(data.rentabilidad);
                dom.txtProbabilidadExisto_cab.val(data.exito);
                dom.txtAreaConTecno_cab.val(data.areaConTecnologico);
                dom.txtAreaConSectorial_cab.val(data.areaConSectorial);
                dom.txtDuracionProyecto_cab.val(data.duracionProyecto);
                dom.txtFechaCierre_cab.val(data.fechaCierre);
                dom.txtFechaLimitePresentacion_cab.val(data.fechaLimitePresentacion);
                dom.txtEtapaVentas_cab.val(data.etapaVentas);
                dom.txtEstado_cab.val(data.estado);
                dom.txtCentroResponsabilidad_cab.val(data.centroResponsabilidad)
                break;

            case "P":
                dom.txtIditemorigen_cab.val(data.iditemorigen);
                dom.txtDenominacion_cab.val(data.denominacion);

                dom.txtCuenta_cab.val(data.cuenta);
                dom.div_txtCuenta_cab.css("display", "block");

                dom.txtEstado_cabObj.val(data.estado);
                dom.txtOrgComercial_Objetivo.val(data.organizacionComercial);
                dom.txtComercial_cabObjetivo.val(data.comercial);
                dom.txtDescObjetivo.val(data.desc_objetivo);

                dom.txtTipoNegocio.val(data.tipo_negocio);

                dom.txtOferta_cab.val(data.oferta);
                dom.txtFechaInicio_cab.val(data.fechaInicio);
                dom.txtContataPrevista_cab.val(data.contratacionPrevista);
                dom.txtFechaFin_cab.val(data.fechaFin);
                dom.txtCoste_cab.val(data.costePrevisto);
                //dom.txtResultado_cab.val(data.resultado);
                break;

            case "S":
                dom.txtIditemorigen_cab.val(data.ta206_idsolicitudpreventa);
                dom.txtDenominacion_cab.val(data.ta206_denominacion);
                break;
        }

    }

    function setTipoBusquedaAyudaLider(tipoAyuda) {

        var o = {
            tipoAyuda: tipoAyuda
        }
        dom.divAyudaLider.buscaprof("option", "searchParams", o);

        if (tipoAyuda == "lideres")
            dom.divAyudaLider.buscaprof("option", "autoSearch", true);
        else if (tipoAyuda == "generalficepi")
            dom.divAyudaLider.buscaprof("option", "autoSearch", false);
    }

    function setEstado(estado) {

        var clase;
        switch (estado) {
            case "A":
                clase = "verde";
                break;
            case "F":
                clase = "gris";
                dom.lbldteFFR.text("Fecha de finalización");
                break;
            case "X":
                clase = "rojo";
                dom.lbldteFFR.text("Fecha de anulación");
                break;
            case "FS":
            case "XS":
                clase = "rojo";
                dom.lbldteFFR.text("Fecha de cierre");
                break;

        }

        dom.estado.attr("data-after", getLiteralEstado(estado));
        dom.estado.addClass(clase);
        dom.estadomin.text(getLiteralEstado(estado));
    }

    function renderAdd() {

        setEstado("A");
        dom.lblTareasCount.text("0");
    }

    function renderEdit(o) {

        console.log("renderEdit " + o);

        dom.txtDenominacion.val(o.ta204_descripcion);
        dom.txtObservaciones.val(o.ta204_observaciones);
        dom.dteFFE.val(moment(o.ta204_fechafinestipulada).format("DD/MM/YYYY"));
        dom.dteFC.val(moment(o.ta204_fechacreacion).format("DD/MM/YYYY"));

        if (typeof o.ta204_fechafinreal !== "undefined")
            if (o.ta204_fechafinreal !== null) {
                dom.dteFFR.val(moment(o.ta204_fechafinreal).format("DD/MM/YYYY"));
                dom.dteFFR.css("visibility", "visible");
                dom.lbldteFFR.css("visibility", "visible");
            }
            else {
                dom.dteFFR.css("visibility", "hidden");
                dom.lbldteFFR.css("visibility", "hidden");
            }

        dom.txtLider.val(o.lider);
        dom.txtLider.attr("data-idficepi", o.t001_idficepi_lider);
        dom.lblTareasCount.text(o.tareasCount);

        setEstado(o.ta204_estado);

    }

    function getViewValues() {

        return new oAccion.fromValues(
            null,
            null,
            $.datepicker.parseDate('dd/mm/yy', dom.dteFFE.val()),
            dom.txtDenominacion.val(),
            dom.txtObservaciones.val(),
            dom.cmbUnidad.val(),
            dom.cmbArea.val(),
            dom.cmbSubarea.val(),
            dom.cmbTipoAccion.val(),
            dom.txtLider.attr("data-idficepi"),
            dom.txtLider.val()
        );
    }

    //Validación requerida del formulario
    function requiredValidation(oAmbito) {

        var valid = true;
        if (typeof oAmbito == "undefined" || oAmbito == null) oAmbito = dom.theForm;

        oAmbito.find(":required").each(function () {

            if ($(this).val() == null || $(this).val().length == 0) {
                $(this).addClass("requerido");
                valid = false;
            }
        });

        if (!valid)
            IB.bsalert.toastdanger("Debes cumplimentar los campos obligatorios.");

        return valid;
    }

    function setDocumentosCount(n) {
        dom.lblDocumentosCount.text(n)
    }

    function getLiteralEstado(estado) {
        switch (estado) {
            case "A": return "Abierta";
            case "F": return "Finalizada";
            case "X": return "Anulada";

                //heredados de la solicitud
            case "FS": return "Cerrada"; //anulada por finalización de solicitud
            case "XS": return "Cerrada"; //anulada por anulación de solicitud

            default: throw ("Estado no contemplado");
        }
    }

    //*** ANULACION DE ACCION ***//
    function initModal_AnularAccion() {

        dom.numCaracteres_anularAccion.removeClass('text-danger');
        dom.modal_txtMotivoAnulacion.removeClass("requerido");
        dom.numCaracteres_anularAccion.text("250 caracteres disponibles");
        dom.modal_txtMotivoAnulacion.val("").focus();
        dom.modal_anularAccion.modal("show");
    }

    function initModal_finalizarAccion() {
        dom.modal_finalizarAccion.modal("show");
    }

    function modal_txtMotivoAnulacion_onKeyUp(e) {
        var _max = 250;
        dom.numCaracteres_anularAccion.text(_max + ' caracteres disponibles');

        var len = $(this).val().length;
        if (len >= _max) {
            dom.numCaracteres_anularAccion.text('Límite alcanzado');
            dom.numCaracteres_anularAccion.addClass('text-danger');

            /*Una vez alcanzado el límite, sólo permitimos el backspace*/
            if (e.keyCode !== 8) {
                e.preventDefault();
            }

        }
        else {
            var ch = _max - len;
            dom.numCaracteres_anularAccion.text(ch + ' caracteres disponibles');
            dom.numCaracteres_anularAccion.removeClass('disabled');
            dom.numCaracteres_anularAccion.removeClass('text-danger');
        }
    }

    function getModalMotivoAnulacion() {

        return dom.modal_txtMotivoAnulacion.val();
    }

    function closeModal_AnularAccion() {

        dom.modal_anularAccion.modal("hide");
    }

    //*** ANULACION DE ACCION ***//

    //*** MODAL REPLICAR ACCION ***//
    function initModal_ReplicarAccion() {

        dom.txtDenominacion_RA.removeClass("requerido");
        dom.dteFFE_RA.removeClass("requerido");
        dom.txtDenominacion_RA.val("").focus();
        dom.txtObservaciones_RA.val("");
        dom.dteFFE_RA.val("");
        dom.dteFFEM_RA.text("");

        dom.modal_replicarAccion.modal("show");
    }

    function getModalReplicarAccionValues() {

        return {
            tipoaccion: dom.cmbTipoAccion_RA.val(),
            descripcion: dom.txtDenominacion_RA.val(),
            observaciones: dom.txtDenominacion_RA.val(),
            fecha: $.datepicker.parseDate('dd/mm/yy', dom.dteFFE_RA.val())
        }
    }

    function closeModal_ReplicarAccion() {

        dom.modal_replicarAccion.modal("hide");
    }
    //*** MODAL REPLICAR ACCION ***//


    function renderCombo(lstOptions, el, selectedId) {

        console.log("renderCombo " + el.prop("id"));

        if (lstOptions.length > 1)
            lstOptions.splice(0, 0, { Key: "", Value: "Selecciona..." });

        clearComboOptions(el);
        lstOptions
            .map(dom.cmboption)
            .forEach(function (option) {
                el.append(option);
            });

        if (selectedId != undefined && selectedId != null) {
            el.val(selectedId);
            el.trigger("change");
        }
        else if (lstOptions.length == 1) {
            el.find("option").attr("selected", "selected");
            el.trigger("change");
        }



    }

    function renderComboAccion(lstOptions, el, selectedId) {

        console.log("renderComboAccion " + el.prop("id"));

        if (lstOptions.length > 1)
            lstOptions.splice(0, 0, {
                ta205_idtipoaccionpreventa: "", ta205_denominacion: "Selecciona...", ta205_plazominreq: -1
            });

        clearComboOptions(el);
        lstOptions
                .map(dom.cmboptionAccion)
                .forEach(function (option) {
                    el.append(option);
                });

        if (selectedId != undefined && selectedId != null) {
            el.val(selectedId);
            el.trigger("change");
        }
        else if (lstOptions.length == 1) {
            el.find("option").attr("selected", "selected");
            el.trigger("change");
        }
    }

    function getAttrObligalider() {
        if ($("#cmbSubarea").css("visibility") !== "hidden")
            return $("#cmbSubarea:visible option:selected").attr("ta201_obligalider");
    }

    function clearComboOptions(el) {

        var antValue = el.val();

        el.find('option').remove();
        el.val("");

        //if (antValue != null && antValue != "") el.trigger("change");
    }

    function getComboSelectedOption(el) {

        return el.val();
    }

    //Esta es bastante mas fina que la funcion "getAttrObligalider", origen de toda la porqueria del atributo ta201_obligalider
    function getComboSelectedOptionAttr(el, attr) {
        return el.find("option:selected").attr(attr);
    }

    function cmbUnidad_onChange_togglevisibility(ocultar) {

        //Si unidad = 0 --> ocultar area y subarea
        var cssProp = "visibility";
        var cssValue = ocultar ? "hidden" : "visible";

        dom.lblArea.css(cssProp, cssValue);
        dom.lblSubarea.css(cssProp, cssValue);
        dom.cmbArea.css(cssProp, cssValue);
        dom.cmbSubarea.css(cssProp, cssValue);
        dom.lblRespSubarea.css(cssProp, cssValue);
        dom.txtRespSubarea.css(cssProp, cssValue);
    }

    var manejarcampos = (function () {

        function desactivarcampostodos() {
            dom.cmbTipoAccion.attr("disabled", "disabled");
            dom.cmbUnidad.attr("disabled", "disabled");
            dom.cmbArea.attr("disabled", "disabled");
            dom.cmbSubarea.attr("disabled", "disabled");
            dom.txtDenominacion.attr("disabled", "disabled");
            dom.txtObservaciones.attr("disabled", "disabled");
            dom.dteFFE.attr("disabled", "disabled");

            dom.dteFFR.css("visibility", "hidden");
            dom.lbldteFFR.css("visibility", "hidden");

        }
        function activarcampostodos() {
            //campos
            dom.cmbTipoAccion.removeAttr("disabled");
            dom.cmbUnidad.removeAttr("disabled");
            dom.cmbArea.removeAttr("disabled");
            dom.cmbSubarea.removeAttr("disabled");
            dom.txtDenominacion.removeAttr("disabled");
            dom.txtObservaciones.removeAttr("disabled");
            dom.dteFFE.removeAttr("disabled");
        }
        function desactivarbotonestodos() {
            dom.btnGrabar.css("display", "none");
            dom.btnFinalizarAccion.css("display", "none");
            dom.btnAnularAccion.css("display", "none");
            dom.btnReplicarAccion.css("display", "none");
            dom.btnAutoAsignar.css("display", "none");
            dom.btnCrearAccionHermana.css("display", "none");
        }
        function activarbotonestodos() {
            dom.btnGrabar.css("display", "inline-block");
            dom.btnFinalizarAccion.css("display", "inline-block");
            dom.btnAnularAccion.css("display", "inline-block");
            dom.btnReplicarAccion.css("display", "inline-block");
            dom.btnAutoAsignar.css("display", "inline-block");
            dom.btnCrearAccionHermana.css("display", "inline-block");
        }
        function desactivarenlacestodos() {
            dom.btnAyudaLider.addClass("linkdisabled");
            dom.btnOtrosLideres.addClass("hide");
            dom.btnTareas.addClass("linkdisabled");
            dom.btnDocumentacion.addClass("linkdisabled");
        }
        function activarenlacestodos() {
            dom.btnAyudaLider.removeClass("linkdisabled");
            dom.btnTareas.removeClass("linkdisabled");
            dom.btnDocumentacion.removeClass("linkdisabled");
        }
        function desactivarcampos(arr) {
            arr.forEach(function (campo) {
                campo.attr("disabled", "disabled");
            })
        }
        function activarcampos(arr) {
            arr.forEach(function (campo) {
                campo.removeAttr("disabled", "disabled");
            })
        }
        function desactivarenlaces(arr) {
            arr.forEach(function (enlace) {
                enlace.addClass("linkdisabled");
            })
        }
        function desactivarenlaces2(arr) {
            arr.forEach(function (enlace) {
                enlace.addClass("hide");
            })
        }
        function activarenlaces(arr) {
            arr.forEach(function (enlace) {
                enlace.removeClass("linkdisabled");
            })
        }
        function activarenlaces2(arr) {
            arr.forEach(function (enlace) {
                enlace.removeClass("hide");
            })
        }
        function mostrarenlaces(arr) {
            arr.forEach(function (enlace) {
                enlace.removeClass("hide");
            })
        }
        function desactivarbotones(arr) {
            arr.forEach(function (enlace) {
                enlace.css("display", "none");
            })
        }
        function activarbotones(arr) {
            arr.forEach(function (enlace) {
                enlace.css("display", "inline-block");
            })
        }
        function mostrarMotivoAnulacion(msg) {
            dom.textareaMotivoAnulacion.text(msg);
            dom.divtextareaMotivoAnulacion.removeClass("hide");
        }
        function getCmbEstructBloquedados() {

            return {
                unidad: dom.cmbUnidad.attr("disabled") == "disabled" ? true : false,
                area: dom.cmbArea.attr("disabled") == "disabled" ? true : false,
                subarea: dom.cmbSubarea.attr("disabled") == "disabled" ? true : false
            }
        }




        return {
            desactivarcampostodos: desactivarcampostodos,
            activarcampostodos: activarcampostodos,
            desactivarbotonestodos: desactivarbotonestodos,
            activarbotonestodos: activarbotonestodos,
            desactivarenlacestodos: desactivarenlacestodos,
            activarenlacestodos: activarenlacestodos,
            desactivarcampos: desactivarcampos,
            activarcampos: activarcampos,
            desactivarenlaces: desactivarenlaces,
            activarenlaces: activarenlaces,
            desactivarenlaces2: desactivarenlaces2,
            activarenlaces2: activarenlaces2,
            mostrarenlaces: mostrarenlaces,
            desactivarbotones: desactivarbotones,
            activarbotones: activarbotones,
            mostrarMotivoAnulacion: mostrarMotivoAnulacion,
            getCmbEstructBloquedados: getCmbEstructBloquedados
        }


    })();

    function setLider(idficepi, profesional) {
        dom.txtLider.attr("data-idficepi", idficepi);
        dom.txtLider.val(profesional);
    }

    return {
        dom: dom,
        init: init,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        getViewValues: getViewValues,
        renderAdd: renderAdd,
        renderEdit: renderEdit,
        requiredValidation: requiredValidation,
        renderCombo: renderCombo,
        renderComboAccion: renderComboAccion,
        clearComboOptions: clearComboOptions,
        getComboSelectedOption: getComboSelectedOption,
        getComboSelectedOptionAttr: getComboSelectedOptionAttr,
        cmbUnidad_onChange_togglevisibility: cmbUnidad_onChange_togglevisibility,
        setDocumentosCount: setDocumentosCount,
        manejarcampos: manejarcampos,
        setLider: setLider,
        setTipoBusquedaAyudaLider: setTipoBusquedaAyudaLider,
        initModal_AnularAccion: initModal_AnularAccion,
        initModal_finalizarAccion: initModal_finalizarAccion,
        closeModal_AnularAccion: closeModal_AnularAccion,
        getModalMotivoAnulacion: getModalMotivoAnulacion,
        initModal_ReplicarAccion: initModal_ReplicarAccion,
        getModalReplicarAccionValues: getModalReplicarAccionValues,
        closeModal_ReplicarAccion: closeModal_ReplicarAccion,
        pintaInfoCRM: pintaInfoCRM,
        getAttrObligalider: getAttrObligalider,
        liderObligatorio: liderObligatorio,
        liderNoSeleccionable: liderNoSeleccionable,
        liderOpcional: liderOpcional

    }

})(SUPER.SIC.Models.Accion);






