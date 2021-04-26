/// <reference path="../../../../../scripts/IB.js" />

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.viewCatAccion = (function () {

    var hoy = moment().format("MM-DD-YYYY");
    var _dtFilter = null;
    var _selectedRow = null;
    var _ta206_idsolicitudpreventa = -1;
    

    var selector = {
        btnAddAccion: "#btnAddAccion",
        btnAddSolicitud: "#btnAddSolicitud",
        fk_edicion_solicitud: ".fk_edicion_solicitud",
        edicion_accion: ".fk_edicion_accion",
        btnFinalizar: "#btnFinalizar",
        btnAnular: "#btnAnular",
        btnFinAnul: "#btnFinAnul",
        btnCancelFinAnul: "#btnCancelFinAnul",
        fk_lnkEstadoSolicitud: ".fk_lnkEstadoSolicitud"
    }

    var dom = {
        divAyudaItemOrigen: $("#divAyudaItemOrigen"),
        txtItemOrigen: $("#txtItemOrigen"),
        lnkAyudaItemOrigen: $("#lnkAyudaItemOrigen"),
        divAyudaPromotor: $("#divAyudaPromotor"),
        txtPromotor: $("#txtPromotor"),
        divAyudaComercial: $("#divAyudaComercial"),
        txtComercial: $("#txtComercial"),
        divAyudaLideres: $("#divAyudaLideres"),
        divAyudaClientes: $("#divAyudaClientes"),
        divAyudaAcciones: $("#divAyudaAcciones"),
        divAyudaUnidades: $("#divAyudaUnidades"),
        divAyudaAreas: $("#divAyudaAreas"),
        divAyudaSubareas: $("#divAyudaSubareas"),
        txtFFinDesde: $("#txtFFinDesde"),
        txtFFinHasta: $("#txtFFinHasta"),
        modal_AS: $("#modal_solicitud"),
        row_estructura_AS: $("#row_estructura_AS"),
        txtDenominacion_AS: $("#txtDenominacion_AS"),
        cmbUnidad_AS: $("#cmbUnidad_AS"),
        cmbArea_AS: $("#cmbArea_AS"),
        btnGrabar_AS: $("#btnGrabar_AS"),
        btnFinalizar: $("#btnFinalizar"),
        btnAnular: $("#btnAnular"),
        lnkAyudaPromotor: $("#lnkAyudaPromotor"),
        lblImporteDesde: $("#lblImporteDesde"),
        divImportes: $("#divImportes"),
        btnDelSolicitud: $("#btnDelSolicitud"),
        btnEliminarSolicitud: $("#btnEliminarSolicitud"),
        txtMotivo: $("#txtMotivo"),
        numCaracteres: $('#numCaracteres'),

        cmboption: function (data) {
            if (data.Key === "") {
                return "<option value='' selected hidden>" + data.Value; + "</option>";
            }
            else {
                return "<option value='" + data.Key + "'>" + data.Value; + "</option>";
            }
        }
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function init() {

        //formato numerico en los filtros de importes
        $('#txtImporteDesde').autoNumeric('init', { aSep: '.', aDec: ',', mDec: 0, anDefault: '0' });
        $('#txtImporteHasta').autoNumeric('init', { aSep: '.', aDec: ',', mDec: 0, anDefault: '0' });

        //Click en desplegar / replegar
        $("#linkFiltros").on("click", function () {
            if ($("#divFiltros").is(".in"))
                $("#linkFiltros").text("Desplegar filtros de búsqueda");
            else
                $("#linkFiltros").text("Replegar filtros de búsqueda");
        });

        //Click en limpiarfiltros
        $("#lnkLimpiarFiltros").on("click", limpiarFiltros)

        //Combo origen change --> actualizar la ayuda para el campo itemorigen
        $("#cmbOrigen").on("change", cmbOrigen_onChange);

        //Click en Consultar
        $("#btnConsultar").on("click", btnConsultar_onClick)

        //Click en lnkItemOrigen
        dom.lnkAyudaItemOrigen.on("click", lnkAyudaItemOrigen_onClick);
        $("#btnClearItemOrigen").on("click", function () {
            dom.txtItemOrigen.removeAttr("data-key");
            dom.txtItemOrigen.val("");
        });

        //Limpiar datatable al cambiar un filtro
        $(".fk_filter").on("change", function () { clearDatatable(); })

        if (IB.vars.filters === undefined) {
            _dtFilter = getFilterValues();
            $("#linkFiltros").trigger("click");
        }

        if (_ta206_idsolicitudpreventa === undefined)
            _ta206_idsolicitudpreventa = - 1;
        

        //Marcar fila seleccionada en datatable
        $(document).on("click", "#tblSolicitudes_wrapper tbody tr", function () {
            var id = $(this).attr("data-ta206_idsolicitudpreventa")
            seleccionarFilaDt(id);
            
            if (parseInt($("#tblSolicitudes tr.selectedRow").attr("data-numeroacciones")) === 0 && $("#tblSolicitudes tr.selectedRow").attr("data-ta206_itemorigen") === "S")
                dom.btnDelSolicitud.removeClass("hide").addClass("show-inline");
            else
                dom.btnDelSolicitud.removeClass("show-inline").addClass("hide");


            refreshDatatableAcciones(id);

        });

        dom.modal_AS.find(":required").on("keyup change", function () {
            var obj = $(this);
            try {
                if (obj.val() != null && obj.val().length > 0) obj.removeClass("requerido");
            }
            catch (e) {
                console.log(obj);
            }
        })
        
        //if (IB.vars.filters) {
        //    _ta206_idsolicitudpreventa = IB.vars.filters.idsolicitud == -1 ? -1 : IB.vars.filters.idsolicitud;

        //}
       
        initDatatable();
        initDatatableAcciones(_ta206_idsolicitudpreventa);
        initFilters();

        //Datepicker fechas desde-hasta
        dom.txtFFinDesde.datepicker({
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
                    IB.bsalert.toastdanger("La 'fecha fin estipula desde' introducida no es correcta.");
                    //dom.txtffindesde.val("");
                    dom.txtFFinDesde.focus();
                    return;
                }
            }

        });

        dom.txtFFinHasta.datepicker({
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
                    IB.bsalert.toastdanger("La 'fecha fin estipula hasta' introducida no es correcta.");
                    //dom.txtffinhasta.val("");
                    dom.txtFFinHasta.focus();
                    return;
                }
            }

        });

        //Ayuda de Promotor
        $("#lnkAyudaPromotor").on("click", function () { dom.divAyudaPromotor.buscaprof("show") });
        $("#btnClearPromotor").on("click", function () {
            dom.txtPromotor.removeAttr("data-idficepi");
            dom.txtPromotor.val("");
        });
        dom.divAyudaPromotor.buscaprof({
            titulo: "Selección de solicitante de acción preventa",
            modulo: "SIC",
            autoSearch: false,
            autoShow: false,
            tipoAyuda: "PromotoresAccionPreventa",
            notFound: "No se han encontrado resultados.",
            onSeleccionar: function (data) {
                dom.txtPromotor.attr("data-idficepi", data.idficepi);
                dom.txtPromotor.val(data.profesional);
                dom.txtPromotor.trigger("change");
            }
        });

        //Ayuda de Comercial
        $("#lnkAyudaComercial").on("click", function () { dom.divAyudaComercial.buscaprof("show") });
        $("#btnClearComercial").on("click", function () {
            dom.txtComercial.removeAttr("data-idficepi");
            dom.txtComercial.val("");
        });
        dom.divAyudaComercial.buscaprof({
            titulo: "Selección de comercial",
            modulo: "SIC",
            autoSearch: false,
            autoShow: false,
            tipoAyuda: "ComercialesPreventa",
            notFound: "No se han encontrado resultados.",
            onSeleccionar: function (data) {
                dom.txtComercial.attr("data-idficepi", data.idficepi);
                dom.txtComercial.val(data.profesional);
                dom.txtComercial.trigger("change");
            }
        });

        //Ayuda de lideres
        $("#lnkAyudaLider").on("click", function () {
            var lstSeleccionados = []
            $("#lstLideres li.list-group-item").each(function () {
                lstSeleccionados.push({ t001_idficepi: $(this).attr("data-idficepi"), profesional: $(this).html() })
            })
            dom.divAyudaLideres.buscaprofmulti("option", "lstSeleccionados", lstSeleccionados);
            dom.divAyudaLideres.buscaprofmulti("show")
        });
        dom.divAyudaLideres.buscaprofmulti({
            titulo: "Selección de líderes de acción preventa",
            tituloContIzda: "Profesionales",
            tituloContDcha: "Profesionales seleccionados",
            notFound: "No se han encontrado resultados.",
            modulo: "SIC",
            tipoAyuda: "LideresPreventaConAmbitoVision",
            
            searchParams: { proc: "SIC_GET_HLP_LIDERES_PH", admin: IB.vars.origenMenu == "ADM" ? true : false },
            autoSearch: true,
            autoShow: false,
            eliminarExistentes: true,
            onAceptar: function (data) {
                var html = "";
                data.forEach(function (item) {
                    if (item.estado != "X") {
                        html += "<li class='list-group-item' data-idficepi='" + item.t001_idficepi + "'>" + item.profesional + "</li>";
                    }
                });
                $("#lstLideres").html(html);
                $("#lstLideres").trigger("change");
            }
        });

        //Ayuda de clientes
        $("#lnkAyudaCliente").on("click", function () {
            var lstSeleccionados = []
            $("#lstClientes li.list-group-item").each(function () {
                lstSeleccionados.push({ key: $(this).attr("data-key"), value: $(this).html() })
            })
            dom.divAyudaClientes.ayudamulti("option", "lstSeleccionados", lstSeleccionados);
            dom.divAyudaClientes.ayudamulti("show")
        });
        dom.divAyudaClientes.ayudamulti({
            titulo: "Selección de clientes",
            tituloContIzda: "Clientes",
            tituloContDcha: "Clientes seleccionados",
            notFound: "No se han encontrado resultados.",
            modulo: "SIC",
            tipoAyuda: "CuentasCRM",
            autoSearch: false,
            autoShow: false,
            eliminarExistentes: true,
            placeholderText: "Introduce el comienzo de la denominación de la cuenta",
            filtro: "",
            onAceptar: function (data) {
                var html = "";
                data.forEach(function (item) {
                    if (item.estado != "X") {
                        html += "<li class='list-group-item' data-key='" + item.key + "'>" + item.value + "</li>";
                    }
                });
                $("#lstClientes").html(html);
                $("#lstClientes").trigger("change");
            }
        });

        //Ayuda de acciones
        $("#lnkAyudaAccion").on("click", function () {
            var lstSeleccionados = []
            $("#lstAcciones li.list-group-item").each(function () {
                lstSeleccionados.push({ key: $(this).attr("data-key"), value: $(this).html() })
            })
            dom.divAyudaAcciones.ayudamulti("option", "lstSeleccionados", lstSeleccionados);
            dom.divAyudaAcciones.ayudamulti("show")
        });
        dom.divAyudaAcciones.ayudamulti({
            titulo: "Selección de acciones preventa",
            tituloContIzda: "Acciones preventa",
            tituloContDcha: "Acciones seleccionadas",
            notFound: "No se han encontrado resultados.",
            modulo: "SIC",
            tipoAyuda: "AccionesPreventa",
            autoSearch: true,
            autoShow: false,
            eliminarExistentes: true,
            placeholderText: "Introduce el comienzo de la denominación de la acción",
            filtro: "",
            onAceptar: function (data) {
                var html = "";
                data.forEach(function (item) {
                    if (item.estado != "X") {
                        html += "<li class='list-group-item' data-key='" + item.key + "'>" + item.value + "</li>";
                    }
                });
                $("#lstAcciones").html(html);
                $("#lstAcciones").trigger("change");
            }
        });

        //Ayuda de unidades
        $("#lnkAyudaUnidad").on("click", function () {
            var lstSeleccionados = []
            $("#lstUnidades li.list-group-item").each(function () {
                lstSeleccionados.push({ key: $(this).attr("data-key"), value: $(this).html() })
            })
            dom.divAyudaUnidades.ayudamulti("option", "lstSeleccionados", lstSeleccionados);
            dom.divAyudaUnidades.ayudamulti("show")
        });
        dom.divAyudaUnidades.ayudamulti({
            titulo: "Selección de unidades preventa",
            tituloContIzda: "Unidades preventa",
            tituloContDcha: "Unidades seleccionadas",
            notFound: "No se han encontrado resultados.",
            modulo: "SIC",
            tipoAyuda: "SIC_AYUDA2UNIDADESPREVENTA_CAT",
            autoSearch: true,
            autoShow: false,
            eliminarExistentes: true,
            placeholderText: "Introduce el comienzo de la denominación de la unidad",
            filtro: "",
            onAceptar: function (data) {
                var html = "";
                data.forEach(function (item) {
                    if (item.estado != "X") {
                        html += "<li class='list-group-item' data-key='" + item.key + "'>" + item.value + "</li>";
                    }
                });
                $("#lstUnidades").html(html);
                $("#lstUnidades").trigger("change");

            }
        });

        //Ayuda de areas
        $("#lnkAyudaArea").on("click", function () {
            var lstSeleccionados = []
            $("#lstAreas li.list-group-item").each(function () {
                lstSeleccionados.push({ key: $(this).attr("data-key"), value: $(this).html() })
            })
            dom.divAyudaAreas.ayudamulti("option", "lstSeleccionados", lstSeleccionados);
            dom.divAyudaAreas.ayudamulti("show")
        });
        dom.divAyudaAreas.ayudamulti({
            titulo: "Selección de áreas preventa",
            tituloContIzda: "Áreas preventa",
            tituloContDcha: "Áreas seleccionadas",
            notFound: "No se han encontrado resultados.",
            modulo: "SIC",
            tipoAyuda: "SIC_AYUDA2AREASPREVENTA_CAT",
            admin: IB.vars.origenMenu == "ADM" ? true : false,
            autoSearch: true,
            autoShow: false,
            eliminarExistentes: true,
            placeholderText: "Introduce el comienzo de la denominación del área",
            filtro: "",
            onAceptar: function (data) {
                var html = "";
                data.forEach(function (item) {
                    if (item.estado != "X") {
                        html += "<li class='list-group-item' data-key='" + item.key + "'>" + item.value + "</li>";
                    }
                });
                $("#lstAreas").html(html);
                $("#lstAreas").trigger("change");
            }
        });

        //Ayuda de subareas
        $("#lnkAyudaSubarea").on("click", function () {
            var lstSeleccionados = []
            $("#lstSubareas li.list-group-item").each(function () {
                lstSeleccionados.push({ key: $(this).attr("data-key"), value: $(this).html() })
            })
            dom.divAyudaSubareas.ayudasubareasficepi("option", "lstSeleccionados", lstSeleccionados);
            dom.divAyudaSubareas.ayudasubareasficepi("show")
        });
        dom.divAyudaSubareas.ayudasubareasficepi({
            autoSearch: false,
            autoShow: false,
            tipoAyudaUnidades: "SIC_AYUDA2UNIDADESPREVENTA_CAT",
            tipoAyudaAreas: "SIC_AYUDA2AREASPREVENTA_CAT",
            tipoAyudaSubareas: "SIC_AYUDA2SUBAREASPREVENTA_CAT",
            admin: IB.vars.origenMenu == "ADM" ? true : false,
            eliminarExistentes: true,
            onAceptar: function (data) {
                var html = "";
                data.forEach(function (item) {
                    if (item.estado != "X") {
                        html += "<li class='list-group-item' data-key='" + item.key + "'>" + item.value + "</li>";
                    }
                });
                $("#lstSubareas").html(html);
                $("#lstSubareas").trigger("change");
            }
        });


    }

    function seleccionarFilaDt(id) {
        if (id) {
            $("#tblSolicitudes_wrapper tbody tr").removeClass("selectedRow");
            $("#tblSolicitudes_wrapper tbody tr[data-ta206_idsolicitudpreventa='" + id + "']").addClass("selectedRow");

            if (parseInt($("#tblSolicitudes tr.selectedRow").attr("data-numeroacciones")) === 0 && $("#tblSolicitudes tr.selectedRow").attr("data-ta206_itemorigen") === "S")
                dom.btnDelSolicitud.removeClass("hide").addClass("show-inline");
            else
                dom.btnDelSolicitud.removeClass("show-inline").addClass("hide");
        }
    }

    function initFilters() {


        if (IB.vars.filters) {

            var arr, arr1;
            var filter = IB.vars.filters;

            if (filter.estado) $("#cmbEstadoAccion option[value='" + filter.estado + "']").attr("selected", "selected");

            if (filter.estadoSolicitud === undefined) 
                $("#cmbEstadoSolicitud option[value='']").attr("selected", "selected");
            else
                $("#cmbEstadoSolicitud option[value='" + filter.estadoSolicitud + "']").attr("selected", "selected");
            
            if (filter.itemorigen) {
                $("#cmbOrigen option[value='" + filter.itemorigen + "']").attr("selected", "selected");
                $("#cmbOrigen").trigger("change");
            }
            if (filter.importeDesde) $("#txtImporteDesde").autoNumeric('set', filter.importeDesde);
            if (filter.importeHasta) $("#txtImporteHasta").autoNumeric('set', filter.importeHasta);
            if (filter.ffinDesde) $("#txtFFinDesde").val(filter.ffinDesde);
            if (filter.ffinHasta) $("#txtFFinHasta").val(filter.ffinHasta);
            if (filter.iditemorigen) {
                arr = filter.iditemorigen.split("#");
                dom.txtItemOrigen.attr("data-key", arr[0]);
                dom.txtItemOrigen.val(arr[1]);
            }
            if (filter.promotor) {
                arr = filter.promotor.split("#");
                dom.txtPromotor.attr("data-idficepi", arr[0]);
                dom.txtPromotor.val(arr[1]);
            }
            if (filter.comercial) {
                arr = filter.comercial.split("#");
                dom.txtComercial.attr("data-idficepi", arr[0]);
                dom.txtComercial.val(arr[1]);
            }
            if (filter.lideres) {
                var html = "";
                arr = filter.lideres.split(";")
                arr.forEach(function (item) {
                    arr1 = item.split("#");
                    html += "<li class='list-group-item' data-idficepi='" + arr1[0] + "'>" + arr1[1] + "</li>";
                });
                $("#lstLideres").html(html)
            }
            if (filter.clientes) {
                var html = "";
                arr = filter.clientes.split(";")
                arr.forEach(function (item) {
                    arr1 = item.split("#");
                    html += "<li class='list-group-item' data-key='" + arr1[0] + "'>" + arr1[1] + "</li>";
                });
                $("#lstClientes").html(html)
            }
            if (filter.acciones) {
                var html = "";
                arr = filter.acciones.split(";")
                arr.forEach(function (item) {
                    arr1 = item.split("#");
                    html += "<li class='list-group-item' data-key='" + arr1[0] + "'>" + arr1[1] + "</li>";
                });
                $("#lstAcciones").html(html)
            }
            if (filter.unidades) {
                var html = "";
                arr = filter.unidades.split(";")
                arr.forEach(function (item) {
                    arr1 = item.split("#");
                    html += "<li class='list-group-item' data-key='" + arr1[0] + "'>" + arr1[1] + "</li>";
                });
                $("#lstUnidades").html(html)
            }
            if (filter.areas) {
                var html = "";
                arr = filter.areas.split(";")
                arr.forEach(function (item) {
                    arr1 = item.split("#");
                    html += "<li class='list-group-item' data-key='" + arr1[0] + "'>" + arr1[1] + "</li>";
                });
                $("#lstAreas").html(html)
            }
            if (filter.subareas) {
                var html = "";
                arr = filter.subareas.split(";")
                arr.forEach(function (item) {
                    arr1 = item.split("#");
                    html += "<li class='list-group-item' data-key='" + arr1[0] + "'>" + arr1[1] + "</li>";
                });
                $("#lstSubareas").html(html)
            }

            //replegar los filtros
            if (filter.ejecutar) {
                $("#linkFiltros").trigger("click");
            }

            _dtFilter = getFilterValues();
        }

    }

    function limpiarFiltros() {
        $("#divFiltros").collapse("show");
        $("#cmbEstadoSolicitud").val("A");
        $("#cmbEstadoAccion").val("");
        $("#cmbOrigen").val("");
        $("#txtImporteDesde").val("");
        $("#txtImporteHasta").val("");
        $("#txtFFinDesde").val("");
        $("#txtFFinHasta").val("");
        dom.txtItemOrigen.removeAttr("data-key");
        dom.txtItemOrigen.val("");
        dom.txtPromotor.removeAttr("data-idficepi");
        dom.txtPromotor.val("");
        dom.txtComercial.removeAttr("data-idficepi");
        dom.txtComercial.val("");
        $("#lstLideres").html("");
        $("#lstClientes").html("");
        $("#lstAcciones").html("");
        $("#lstUnidades").html("");
        $("#lstAreas").html("");
        $("#lstSubareas").html("");
        
        refreshDatatable(null);
        refreshDatatableAcciones(-1);
    }

    function cmbOrigen_onChange() {

        var cmbOrigenValue = $("#cmbOrigen option:selected").val();
        if (cmbOrigenValue == "") {
            dom.lnkAyudaItemOrigen.text("ID Origen");
            dom.divImportes.css("visibility", "hidden");
            return;
        }

        var options = {
            modulo: "SIC",
            autoSearch: false,
            autoShow: false,
            filtro: "",
            admin: IB.vars.origenMenu == "ADM" ? true : false,
            notFound: "No se han encontrado resultados.",
            onCancelar: function () { },
            onInit: function () { },
            onDestroy: function () { }
        }


        switch (cmbOrigenValue) {
            case "O":
                options.titulo = "Ayuda de oportunidades";
                options.tipoAyuda = "SIC_AYUDA2TEMORIGEN_O_CAT";
                options.placeholderText = "Introduce el nº de oportunidad o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Oportunidad");
                dom.lnkAyudaPromotor.text("Comercial");
                dom.lblImporteDesde.text("Importe oportunidad desde (EUR)");
                dom.divImportes.css("visibility", "visible");
                
                break;
            case "E":
                options.titulo = "Ayuda de extensiones";
                options.tipoAyuda = "SIC_AYUDA2TEMORIGEN_E_CAT";
                options.placeholderText = "Introduce el nº de extensión o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Extensión");
                dom.lnkAyudaPromotor.text("Comercial");
                dom.lblImporteDesde.text("Importe extensión desde (EUR)");
                dom.divImportes.css("visibility", "visible");
                break;
            case "P":
                options.titulo = "Ayuda de objetivos";
                options.tipoAyuda = "SIC_AYUDA2TEMORIGEN_P_CAT";
                options.placeholderText = "Introduce el nº de objetivo o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Objetivo");
                dom.lnkAyudaPromotor.text("Comercial");
                dom.lblImporteDesde.text("Contratación objetivo desde (EUR)");
                dom.divImportes.css("visibility", "visible");
                break;
            case "S":
                options.titulo = "Ayuda de solicitudes de origen SUPER";
                options.tipoAyuda = "SIC_AYUDA2TEMORIGEN_S_CAT";
                options.placeholderText = "Introduce el nº de solicitud SUPER o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Solicitud");
                dom.lnkAyudaPromotor.text("Promotor");
                dom.divImportes.css("visibility", "hidden");
                break;
        }

        if (dom.divAyudaItemOrigen.html() == "") { //no existe plugin --> inicializar

            options.onSeleccionar = function (data) {
                dom.txtItemOrigen.attr("data-key", data.key)
                dom.txtItemOrigen.val(data.value);
                dom.txtItemOrigen.trigger("change");
            }

            dom.divAyudaItemOrigen.ayuda(options);
        }
        else
            dom.divAyudaItemOrigen.ayuda("options", options);

    }

    function lnkAyudaItemOrigen_onClick() {

        var cmbOrigenValue = $("#cmbOrigen option:selected").val();
        if (cmbOrigenValue == "") {
            IB.bsalert.toastwarning("Para utilizar esta ayuda primero debes seleccionar un tipo de origen de la lista.");
            return;
        }

        dom.divAyudaItemOrigen.ayuda("show");
    }

    function btnConsultar_onClick() {

        var filter = getFilterValues();
        var msgerr = "";
        var fdesde = null;
        var fhasta = null;

        //validar formato de campos de introducción manual
        if (filter.ffinDesde != null) {
            try {
                var fdesde = $.datepicker.parseDate('dd/mm/yy', filter.ffinDesde)
            }
            catch (e) {
                msgerr += "<li>La 'fecha fin estipulada desde' es incorrecta.</li>";
            }
        }
        if (filter.ffinHasta != null) {
            try {
                var fhasta = $.datepicker.parseDate('dd/mm/yy', filter.ffinHasta)
            }
            catch (e) {
                msgerr += "<li>La 'fecha fin estipulada hasta' es incorrecta.</li>";
            }
        }
        if (msgerr.length > 0) {
            IB.bsalert.toastdanger("<ul class='list-group text-left'>" + msgerr + "</ul>");
            return;
        }


        //validar rangos
        if (fdesde != null && fhasta != null) {
            if (fdesde > fhasta)
                msgerr += "<li'>Rango de fechas incorrecto.</li>";
        }
        if (filter.importeDesde != null && filter.importeHasta != null) {
            if (filter.importeDesde > filter.importeHasta)
                msgerr += "<li>Rango de importes incorrecto.</li>";
        }

        if (msgerr.length > 0) {
            IB.bsalert.toastdanger("<ul class='list-group text-left'>" + msgerr + "</ul>");
            return;
        }

        refreshDatatable(filter);

        $("#linkFiltros").trigger("click");
    }

    function getFilterValues() {

        var filter = {};

        filter.estado = $("#cmbEstadoAccion option:selected").val() != "" ? $("#cmbEstadoAccion option:selected").val() : null;
        filter.estadoSolicitud = $("#cmbEstadoSolicitud option:selected").val() != "" ? $("#cmbEstadoSolicitud option:selected").val() : null;
        filter.itemorigen = $("#cmbOrigen option:selected").val() != "" ? $("#cmbOrigen option:selected").val() : null;
        filter.iditemorigen = $("#txtItemOrigen").attr("data-key") ? $("#txtItemOrigen").attr("data-key") : null;
        filter.importeDesde = $("#txtImporteDesde").val() != "" ? $("#txtImporteDesde").val().replace(/\./g, "") : null;
        filter.importeHasta = $("#txtImporteHasta").val() != "" ? $("#txtImporteHasta").val().replace(/\./g, "") : null;
        filter.ffinDesde = $("#txtFFinDesde").val() != "" ? $("#txtFFinDesde").val() : null;
        filter.ffinHasta = $("#txtFFinHasta").val() != "" ? $("#txtFFinHasta").val() : null;
        filter.promotor = $("#txtPromotor").attr("data-idficepi") ? $("#txtPromotor").attr("data-idficepi") : null;
        filter.comercial = $("#txtComercial").attr("data-idficepi") ? $("#txtComercial").attr("data-idficepi") : null;
        filter.lideres = getAttributeFromListItem($("#lstLideres li"), "data-idficepi");
        filter.clientes = getAttributeFromListItem($("#lstClientes li"), "data-key");
        filter.acciones = getAttributeFromListItem($("#lstAcciones li"), "data-key");
        filter.unidades = getAttributeFromListItem($("#lstUnidades li"), "data-key");
        filter.areas = getAttributeFromListItem($("#lstAreas li"), "data-key");
        filter.subareas = getAttributeFromListItem($("#lstSubareas li"), "data-key");

        return filter;

    }

    function getFilterValuesWithDesc() {

        var filter = {};

        filter.estado = $("#cmbEstadoAccion option:selected").val() != "" ? $("#cmbEstadoAccion option:selected").val() : null;
        filter.estadoSolicitud = $("#cmbEstadoSolicitud option:selected").val() != "" ? $("#cmbEstadoSolicitud option:selected").val() : null;
        filter.itemorigen = $("#cmbOrigen option:selected").val() != "" ? $("#cmbOrigen option:selected").val() : null;
        filter.iditemorigen = $("#txtItemOrigen").attr("data-key") ? $("#txtItemOrigen").attr("data-key") + "#" + $("#txtItemOrigen").val() : null;
        filter.importeDesde = $("#txtImporteDesde").val() != "" ? $("#txtImporteDesde").val().replace(/\./g, "") : null;
        filter.importeHasta = $("#txtImporteHasta").val() != "" ? $("#txtImporteHasta").val().replace(/\./g, "") : null;
        filter.ffinDesde = $("#txtFFinDesde").val() != "" ? $("#txtFFinDesde").val() : null;
        filter.ffinHasta = $("#txtFFinHasta").val() != "" ? $("#txtFFinHasta").val() : null;
        filter.promotor = $("#txtPromotor").attr("data-idficepi") ? $("#txtPromotor").attr("data-idficepi") + "#" + $("#txtPromotor").val() : null;
        filter.comercial = $("#txtComercial").attr("data-idficepi") ? $("#txtComercial").attr("data-idficepi") + "#" + $("#txtComercial").val() : null;
        filter.lideres = getAttributeFromListItemWithDesc($("#lstLideres li"), "data-idficepi");
        filter.clientes = getAttributeFromListItemWithDesc($("#lstClientes li"), "data-key");
        filter.acciones = getAttributeFromListItemWithDesc($("#lstAcciones li"), "data-key");
        filter.unidades = getAttributeFromListItemWithDesc($("#lstUnidades li"), "data-key");
        filter.areas = getAttributeFromListItemWithDesc($("#lstAreas li"), "data-key");
        filter.subareas = getAttributeFromListItemWithDesc($("#lstSubareas li"), "data-key");

        return filter;

    }

    function getAttributeFromListItem($lst, attribute) {

        var arr = []

        $lst.each(function () {
            arr.push($(this).attr(attribute))
        });

        if (arr.length == 0) return null;

        return arr;
    }

    function getAttributeFromListItemWithDesc($lst, attribute) {
        var arr = []

        $lst.each(function () {
            arr.push($(this).attr(attribute) + "#" + $(this).html())
        });

        if (arr.length == 0) return null;

        return arr;

    }

    function refreshDatatable(filter, selectedRow) {
        _dtFilter = filter;
        _selectedRow = selectedRow

        var oDataTable = $('#tblSolicitudes').DataTable();
        //oDataTable.clear().draw();
        oDataTable.search('').draw();
        oDataTable.ajax.reload(null, true);
    }

    function refreshDatatableAcciones(filter, selectedRow) {
        _ta206_idsolicitudpreventa = filter;

        var oDataTable = $('#tblAcciones').DataTable();
        oDataTable.search('').draw();
        oDataTable.ajax.reload(null, true);
    }

    function clearDatatable() {
        var oDataTable = $('#tblSolicitudes').DataTable();
        oDataTable.clear().draw();

        var oDataTableAcciones = $('#tblAcciones').DataTable();
        oDataTableAcciones.clear().draw();
    }

    
    function initDatatable() {

        var dt = $('#tblSolicitudes').DataTable({
            "language": { "url": "../../plugins/datatables/literales-no-paginado.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,            
            "order": [], 
            "scrollY": "22vh",
            "scrollX": true,
            "scrollCollapse": false,
            "fixedColumns": {
                "leftColumns": 2
            },

            "ajax": {
                "url": "Default.aspx/Catalogo",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () {
                    return JSON.stringify({
                        origenMenu: IB.vars.origenMenu,
                        filter: _dtFilter
                    });
                },
                "dataSrc": function (data) {                    
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
                }
            },

            "columns": [
                { "data": "ta206_idsolicitudpreventa" },
                { "data": "ta206_denominacion" },

                { "data": "ta200_denominacion" },
                
                { "data": "promotor" },
                { "data": "den_cuenta" },


                { "data": "ta206_estado" },
                { "data": "ta206_fechacreacion" }                
            ],

            "columnDefs": [
                {
                    //ta206_idsolicitudpreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": function (data, type, row, meta) {
                        if (row.ta206_itemorigen == "S" && row.ta206_estado == "A")
                            return "<a class='underline fk_edicion_solicitud' href='#' data-idsolicitud='" + row.ta206_idsolicitudpreventa + "'>" + accounting.formatNumber(row.ta206_idsolicitudpreventa, 0, ".") + "</a>";
                        else
                            return accounting.formatNumber(row.ta206_idsolicitudpreventa, 0, ".");

                    }
                },
                {
                    //ta206_denominacion
                    "targets": 1,
                    "className": "text-left",
                    "render": function (data, type, row, meta) {
                        var s = "<nobr>"
                        switch (row.ta206_itemorigen) {
                            case "O": s += "ON"; break;
                            case "E": s += "EXT"; break;
                            case "P": s += "OBJ"; break;
                            case "S": s += "SUP"; break;
                        }
                        s += " " + accounting.formatNumber(row.ta206_iditemorigen, 0, ".") + " - " + row.ta206_denominacion;
                        s += "&nbsp;&nbsp;&nbsp;(" + row.numeroacciones + "/" + row.accionesabiertas + ")";
                        s += "</nobr>";
                        
                        return s;
                    }

                },
                {
                    //estado
                    "targets": 5,
                    "className": "text-center",
                    "render": function (data, type, row, meta) {
                        if (row.ta206_estado === "X" || row.ta206_estado === "N")
                            return "<a data-ta206_motivoanulacion='" + row.ta206_motivoanulacion + "' href='#' class='fk_lnkEstadoSolicitud underline'>" + getLiteralEstado(data) + "</a>"
                        else
                            return "<nobr>" + getLiteralEstado(data) + "</nobr>";
                    }

                },

                {
                    //ÁREA
                    "targets": 2,
                    "className": "text-left"

                },
             
                {
                    //PROMOTOR
                    "targets": 3,
                    "className": "text-left"                    
                },

                {
                    //CUENTA
                    "targets": 4,
                    "className": "text-left"                    
                },

                {
                    //ta206_fechacreacion
                    "targets": 6,
                    "type": "date",
                    "className": "text-center",
                    "render":{
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
                        },
                        "filter": function (data, type, row, meta) {
                                return moment(data).format('DD/MM/YYYY');
                        },
                    }
                },               
            ],



            "createdRow": function (row, data, index) {
                $(row).attr("id", "ta206_id_" + data.ta206_idsolicitudpreventa)
                $(row).attr("data-ta206_idsolicitudpreventa", data.ta206_idsolicitudpreventa) //id solicitud
                $(row).attr("data-ta206_itemorigen", data.ta206_itemorigen) 
                $(row).attr("data-ta206_iditemorigen", data.ta206_iditemorigen) 
                $(row).attr("data-ta206_estado", data.ta206_estado) //Estado
                $(row).attr("data-numeroacciones", data.numeroacciones) //Número acciones                
            },
            "drawCallback": function (settings) {
                console.log("drawCallback");
                //tras aplicar filtros --> marcar de nuevo la fila que estaba seleccionada
                if (IB.vars.filters && IB.vars.filters.ejecutar) { //marcar la fila que estaba seleccionada
                    //$("#tblSolicitudes_wrapper tbody tr[data-ta206_idsolicitudpreventa='" + IB.vars.filters.selected + "']").addClass("selectedRow");
                    $("#tblSolicitudes_wrapper tbody tr[data-ta206_idsolicitudpreventa='" + IB.vars.filters.idsolicitud + "']").addClass("selectedRow").trigger("click");
                    
                }
                
                if (_selectedRow) { //tras alta de solicitud --> marcar la fila
                    seleccionarFilaDt(_selectedRow);
                    refreshDatatableAcciones(-1);
                    //una vez seleccionada --> limpiar variable
                    if ($("#tblSolicitudes_wrapper tbody tr[data-ta206_idsolicitudpreventa='" + _selectedRow + "']").length > 0)
                        _selectedRow = null;
                }

            },

            "initComplete": function (row, data, index) {               
            }

        })

        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

    function initDatatableAcciones(ta206_idsolicitudpreventa) {

        var dt = $('#tblAcciones').DataTable({
            "language": { "url": "../../plugins/datatables/literales-no-paginado.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,            
            "order": [], 
            "scrollY": "21vh",
            "scrollX": true,
            "scrollCollapse": false,
            "fixedColumns": {
                "leftColumns": 2
            },

            "ajax": {
                "url": "Default.aspx/CatalogoAccionesBySolicitud",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () {                    
                    return JSON.stringify({                        
                        ta206_idsolicitudpreventa: _ta206_idsolicitudpreventa                        
                    });
                },
                "dataSrc": function (data) {                    
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
                }
            },

            "columns": [
                { "data": "ta204_idaccionpreventa" },
                { "data": "tipoAccion" },
                { "data": "subareaPreventa" },                
                { "data": "importe" },
                { "data": "promotor" },
                { "data": "den_unidadcomercial" },
                { "data": "lider" },
                { "data": "ta204_estado" },
                { "data": "ta204_fechacreacion" },
                { "data": "ta204_fechafinestipulada" },
                { "data": "ta204_fechafinreal" }
            ],

            "columnDefs": [
                {
                    //ta204_idaccionpreventa
                    "targets": 0,
                    "className": "text-center",
                    "render": function (data, type, row, meta) {
                        if (row.linkaccesoaccion)
                            return "<a class='underline fk_edicion_accion' href='#' data-idaccion='" + row.ta204_idaccionpreventa + "'>" + accounting.formatNumber(row.ta204_idaccionpreventa, 0, ".") + "</a>";
                        else
                            return accounting.formatNumber(row.ta204_idaccionpreventa, 0, ".");
                    }
                },
                {
                    //tipoAccion
                    "targets": 1,
                    "className": "text-left",
                    "render":{
                        "display" : function (data, type, row, meta) {
                            var style = "";
                            if (row.ta204_estado == "A") { //en rojo las que estando abiertas no cumplen el PMR
                                var fc = moment(row.ta204_fechacreacion).format("DD/MM/YYYY"); //quitar las horas
                                if (moment(row.ta204_fechafinestipulada).isBefore(moment(fc, "DD/MM/YYYY").add(row.ta205_plazominreq, "days")))
                                    style = "style='color:red;font-weight:bold'";
                            }

                            return "<span " + style + "><nobr>" + data + "</nobr></span>";
                        },
                        "filter": function (data, type, row, meta) {
                            return data;
                        }
                    }
                },
                {
                    //estado
                    "targets": 7,
                    "className": "text-center",
                    "render": function (data, type, row, meta) {
                        return "<nobr>" + getLiteralEstado(data) + "</nobr>";
                    }

                },

                 {
                     //Subárea
                     "targets": 2,
                     "className": "text-left"
                 },

                {
                    //importe
                    "targets": 3,
                    "className": "text-left",
                    "render": function (data, type, row, meta) {
                        return "<nobr>" + row.importe + " "+ row.moneda+" </nobr>";
                    }
                },

                {
                    //Solicitante
                    "targets": 4,
                    "className": "text-left"                   
                },

                {
                    //Org comercial
                    "targets": 5,
                    "className": "text-left"                    
                },

                {
                    //Líder
                    "targets": 6,
                    "className": "text-left"                    
                },




                {
                    //ta204_fechacreacion
                    "targets": 8,
                    "type": "date",
                    "className": "text-center",
                    "render":{
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00" && data != null)
                                return moment(data).format('DD/MM/YYYY');
                            else
                                return "";
                        },
                        "filter": function (data, type, row, meta) {
                                return moment(data).format('DD/MM/YYYY');
                        },
                    }
                },

                {
                    //ta206_fechafinrequerida
                    "targets": 9,
                    "type": "date",
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00")
                                if (moment(hoy).isAfter(data) && row.ta204_estado === "A")
                                    return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else if (moment(hoy).add(7, 'day').isAfter(data) && row.ta204_estado === "A")
                                    return "<span style='color:orange;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else
                                    return "<span>" + moment(data).format('DD/MM/YYYY') + "</span>";
                            else
                                return "";
                        },

                        "filter": function (data, type, row, meta) {
                            return moment(data).format('DD/MM/YYYY');
                        },
                    }
                },

        {
                    //ta204_fechafinreal
                    "targets": 10,
                    "type": "date",
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (data != "0001-01-01T00:00:00" && data !== null)
                                if (moment(data) > moment(row.ta204_fechafinestipulada))
                                    return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                else
                                    return "<span style='color:green;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";

                            else
                                return "";
                        },
                        "filter": function (data, type, row, meta) {
                                return moment(data).format('DD/MM/YYYY');
                        },
                    }
                },
            ],

            "createdRow": function (row, data, index) {
                $(row).attr("data-ta204_idaccion", data.ta204_idaccionpreventa) //Asignar id a la fila
            },
            "drawCallback": function (settings) {                

            }


        })

        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

    function getLiteralEstado(estado) {
        switch (estado) {
            case "A": return "Abierta";
            case "F": return "Finalizada";
            case "N": return "Anulada";
            case "X": return "Anulada";
            case "XS": return "Cerrada"; //este no se deberia de usar en la solicitud
            case "FS": return "Cerrada"; //este no se deberia de usar en la solicitud
            default: throw ("Estado no contemplado");
        }
    }

    function getSolicitudSelected() {

        var $tr = $("#tblSolicitudes tr.selectedRow")
        if ($tr.length == 0) return null;

        return {
            ta206_idsolicitudpreventa: $tr.attr("data-ta206_idsolicitudpreventa"),
            ta206_itemorigen: $tr.attr("data-ta206_itemorigen"),
            ta206_iditemorigen: $tr.attr("data-ta206_iditemorigen"),
            ta206_estado: $tr.attr("data-ta206_estado")
        };


    }

    var modalSolicitud = (function () {

        function init(modo) {

            if (modo == "A") { //Alta
                dom.modal_AS.find(".modal-title").html("Alta de solicitud");
                dom.row_estructura_AS.removeClass("hide").addClass("show");
                desbloquearEstructura()
            }
            else { //EDición
                dom.modal_AS.find(".modal-title").html("Edición de solicitud");
                dom.row_estructura_AS.removeClass("show").addClass("hide");
                bloquearEstructura();
            }

            dom.txtDenominacion_AS.val("");
            dom.txtDenominacion_AS.removeClass("requerido");
            dom.cmbUnidad_AS.removeClass("requerido");
            dom.cmbArea_AS.removeClass("requerido");
            clearComboOptions(dom.cmbUnidad_AS);
            clearComboOptions(dom.cmbArea_AS);
        }
        function mostrar() {
            dom.modal_AS.modal("show");

        }
        function ocultar() {
            dom.modal_AS.modal("hide");        
        }
        
        //Foco en la denominacion de la modal añadir solicitud
        dom.modal_AS.on("shown.bs.modal", function () {
            dom.txtDenominacion_AS.focus();
        })

        $("#modal-finAnul").on("shown.bs.modal", function () {
            dom.txtMotivo.focus();
        })

        function bloquearEstructura() {
            dom.cmbUnidad_AS.attr("disabled", "disabled");
            dom.cmbArea_AS.attr("disabled", "disabled");
            dom.cmbUnidad_AS.removeClass("show").addClass("hide");
            dom.cmbArea_AS.removeClass("show").addClass("hide");
        }
        function desbloquearEstructura() {
            dom.cmbUnidad_AS.removeClass("hide").addClass("show");
            dom.cmbArea_AS.removeClass("hide").addClass("show");
            dom.cmbUnidad_AS.removeAttr("disabled");
            dom.cmbArea_AS.removeAttr("disabled");

        }
        function estadoBloqueoEstructura() {

            return {
                unidad: dom.cmbUnidad_AS.attr("disabled") == "disabled" ? true : false,
                area: dom.cmbArea_AS.attr("disabled") == "disabled" ? true : false,
            }

        }
        function getValues() {

            return {
                denominacion: dom.txtDenominacion_AS.val(),                
                unidad: dom.cmbUnidad_AS.find("option:selected").val(),
                area: dom.cmbArea_AS.find("option:selected").val()
            }
        }
        function setValues(o) {
            dom.txtDenominacion_AS.val(o.ta206_denominacion);
        }
        function requiredValidation(oAmbito) {

            var valid = true;
            if (typeof oAmbito == "undefined" || oAmbito == null) oAmbito = dom.modal_AS;

            oAmbito.find(":required").each(function () {

                if (($(this).val() == null || $(this).val().length == 0) && !$(this).hasClass("hide")) {
                    $(this).addClass("requerido");
                    valid = false;
                }
            });

            if (!valid)
                IB.bsalert.toastdanger("Debes cumplimentar los campos obligatorios.");

            return valid;
        }

        return {
            init: init,
            mostrar: mostrar,
            ocultar: ocultar,
            estadoBloqueoEstructura: estadoBloqueoEstructura,
            getValues: getValues,
            setValues: setValues,
            requiredValidation: requiredValidation

        }

    })();

    // *** COMBOS *** //
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

    function clearComboOptions(el) {

        var antValue = el.val();

        el.find('option').remove();
        el.val("");
    }

    function getComboSelectedOption(el) {

        return el.val();
    }

    // *** COMBOS *** //

    function openFinalizarAnular(accion) {
        $("#modal-finAnul").modal("show");
        switch (accion) {
            case "F":
                
                $("#h4Title").text("Finalización de solicitud");
                $("#modal-finAnul #spanFinAnul").text("Esta acción finalizará la solicitud. Si estás conforme pulsa 'Aceptar', en caso contrario pulsa 'Cancelar'");
                $("#divMotivo").removeClass("show").addClass("hide");
                return "F";
                break;

            case "X":
                
                $("#h4Title").text("Anulación de solicitud");
                $("#modal-finAnul #spanFinAnul").text("Esta acción anulará la solicitud. Si estás conforme pulsa 'Aceptar', en caso contrario pulsa 'Cancelar'");
                $("#divMotivo").removeClass("hide").addClass("show");
                return "X";
                break;

        }        
    }

    function mostrarBotonesAccionSolicitud() {
        dom.btnFinalizar.removeClass("hide").addClass("show");
        dom.btnAnular.removeClass("hide").addClass("show");
    }

    function ocultarBotonesAccionSolicitud() {
        dom.btnFinalizar.removeClass("show").addClass("hide");
        dom.btnAnular.removeClass("show").addClass("hide");
    }

    function abrirModalEstadoSolicitud(row) {
        $("#modal-motivoAnulacionSolicitud").modal("show");        
        $("#txtMotivoAnulacionSolicitud").val($(row).find(".fk_lnkEstadoSolicitud").attr("data-ta206_motivoanulacion"));        
    }

    function Ocultar_modalfinAnul() {
        $("#modal-finAnul").modal("hide");
    }

    function showModalEliminarSolicitud(){
        $("#modal-EliminarSolicitud").modal("show");
    }

    function hideModalEliminarSolicitud() {
        $("#modal-EliminarSolicitud").modal("hide");
    }

    function getidSelectedRow() {
        return $("#tblSolicitudes tr.selectedRow").attr("data-ta206_idsolicitudpreventa");
    }

    return {
        selector: selector,
        dom: dom,
        init: init,
        getFilterValues: getFilterValues,
        getFilterValuesWithDesc: getFilterValuesWithDesc,
        getSolicitudSelected: getSolicitudSelected,
        modalSolicitud: modalSolicitud,
        attachLiveEvents: attachLiveEvents,
        attachEvents: attachEvents,
        renderCombo: renderCombo,
        getComboSelectedOption: getComboSelectedOption,
        clearComboOptions: clearComboOptions,
        refreshDatatable: refreshDatatable,
        openFinalizarAnular: openFinalizarAnular,
        mostrarBotonesAccionSolicitud: mostrarBotonesAccionSolicitud,
        ocultarBotonesAccionSolicitud:ocultarBotonesAccionSolicitud,
        Ocultar_modalfinAnul: Ocultar_modalfinAnul,
        getidSelectedRow: getidSelectedRow,
        showModalEliminarSolicitud:showModalEliminarSolicitud,
        hideModalEliminarSolicitud: hideModalEliminarSolicitud,
        abrirModalEstadoSolicitud: abrirModalEstadoSolicitud
    }

})();






