/// <reference path="../../../../../scripts/IB.js" />

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.viewCatAccion = (function () {

    var hoy = moment().format("MM-DD-YYYY");
    var _dtFilter = null;

    var selector = {
        edicion_accion: ".fk_edicion_accion",
        btnExportExcel: ".btnExportExcel"
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
        btnConsultar: $("#btnConsultar"),
        btnExportarExcel: $("#btnExportarExcel"),
        tabla: $('#tblAcciones')
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

        $(".fk_filter").on("change", function () { refreshDatatable(null); })

        initDatatable();
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
            tipoAyuda: "LideresPreventaAmbitoVision",
            searchParams: {
                admin: IB.vars.origenMenu == "ADM" ? true : false
            },
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
            tipoAyuda: "SIC_AYUDA1UNIDADESPREVENTA_CAT",
            admin: IB.vars.origenMenu == "ADM" ? true : false,
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
            tipoAyuda: "SIC_AYUDA1AREASPREVENTA_CAT",
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
            tipoAyudaUnidades: "SIC_AYUDA1UNIDADESPREVENTA_CAT",
            tipoAyudaAreas: "SIC_AYUDA1AREASPREVENTA_CAT",
            tipoAyudaSubareas: "SIC_AYUDA1SUBAREASPREVENTA_CAT",
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

    function initFilters() {

        //Al regresar de otra pantalla
        if (IB.vars.filters) {

            var arr, arr1;
            var filter = IB.vars.filters;

            //if (filter.estado) {
                if (filter.estado === undefined) {
                    $("#cmbEstado option[value='']").attr("selected", "selected");
                }
                else
                    $("#cmbEstado option[value='" + filter.estado + "']").attr("selected", "selected"); 
            //} 
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
        //Al entrar en la pantalla 
        else {
            $("#linkFiltros").trigger("click");
            _dtFilter = getFilterValues();
        }

    }

    function limpiarFiltros() {        
        $("#divFiltros").collapse("show");
        $("#cmbEstado").val("A");
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

    }

    function cmbOrigen_onChange() {

        var cmbOrigenValue = $("#cmbOrigen option:selected").val();
        if (cmbOrigenValue == "") {
            dom.lnkAyudaItemOrigen.text("ID Origen");
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
                options.tipoAyuda = "SIC_AYUDA1TEMORIGEN_O_CAT";
                options.placeholderText = "Introduce el nº de oportunidad o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Oportunidad");
                break;
            case "E":
                options.titulo = "Ayuda de extensiones";
                options.tipoAyuda = "SIC_AYUDA1TEMORIGEN_E_CAT";
                options.placeholderText = "Introduce el nº de extensión o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Extensión");
                break;
            case "P":
                options.titulo = "Ayuda de objetivos";
                options.tipoAyuda = "SIC_AYUDA1TEMORIGEN_P_CAT";
                options.placeholderText = "Introduce el nº de objetivo o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Objetivo");
                break;
            case "S":
                options.titulo = "Ayuda de solicitudes de origen SUPER";
                options.tipoAyuda = "SIC_AYUDA1TEMORIGEN_S_CAT";
                options.placeholderText = "Introduce el nº de solicitud SUPER o contenido de la denominación";
                dom.lnkAyudaItemOrigen.text("Solicitud");
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

        filter.estado = $("#cmbEstado option:selected").val() != "" ? $("#cmbEstado option:selected").val() : null;
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

        filter.estado = $("#cmbEstado option:selected").val() != "" ? $("#cmbEstado option:selected").val() : null;
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

    function refreshDatatable(filter) {
        
        _dtFilter = filter;

        var oDataTable = $('#tblAcciones').DataTable();
        oDataTable.clear().draw();
        oDataTable.search('').draw();
        oDataTable.ajax.reload(null, true);
    }

    function initDatatable() {

        var dt = $('#tblAcciones').DataTable({
            dom: 'f<"pull-left"B>t',
            buttons: [
                {
                    className: 'btnExportExcel',
                    text: 'EXCEL',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'Exportar a EXCEL'
                  
                }
            ],
            "language": { "url": "../../plugins/datatables/literales-no-paginado.txt" },
            "procesing": true,
            "paginate": false,
            "responsive": true,
            "autoWidth": false,
            "fixedHeader": false,            
            "order": [], 
            "scrollY": "55vh",
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
                { "data": "ta204_idaccionpreventa" },
                { "data": "tipoAccion" },
                { "data": "ta206_itemorigen" },
                { "data": "importe" },
                { "data": "den_cuenta" },
                { "data": "promotor" },
                { "data": "comercial" },
                { "data": "den_unidadcomercial" },
                { "data": "areaPreventa" },
                { "data": "subareaPreventa" },
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
                    "render": {
                        "display": function (data, type, row, meta) {
                            return "<a class='underline fk_edicion_accion' href='#' data-idaccion='" + row.ta204_idaccionpreventa + "'>" + accounting.formatNumber(row.ta204_idaccionpreventa, 0, ".") + "</a>";
                        },
                        "filter": function (data, type, row, meta) {
                            return row.ta204_idaccionpreventa + " " + accounting.formatNumber(row.ta204_idaccionpreventa, 0, ".");
                        }
                    }
                },
                {
                    //tipoAccion
                    "targets": 1,
                    "className": "text-left",
                    "render": {
                        "display": function (data, type, row, meta) {
                            var style = ""
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
                    //itemOrigen
                    "targets": 2,
                    "className": "text-left",
                    "render": function (data, type, row, meta) {
                        var s = "<nobr>"
                        switch (data) {
                            case "O": s += "ON"; break;
                            case "E": s += "EXT"; break;
                            case "P": s += "OBJ"; break;
                            case "S": s += "SUP"; break;
                        }
                        s += " " + accounting.formatNumber(row.ta206_iditemorigen, 0, ".") + "</nobr><br />"
                        if (data == "S" && row.ta206_denominacion != null)
                            s += row.ta206_denominacion;
                        else if (row.den_item != null)
                            s += row.den_item;

                        return s;


                    }

                },

                {
                    //importe
                    "targets": 3,
                    "className": "text-right",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (row.moneda == "") return "";
                            return "<nobr>" + accounting.formatNumber(data, 2, ".", ",") + " " + row.moneda + "</nobr>";
                        },
                        "filter": function (data, type, row, meta) {
                            if (row.moneda == "") return "";
                            return data + " " + accounting.formatNumber(data, 2, ".", ",") + " " + row.moneda;
                        }
                    }

                },
                {
                    //areaPreventa
                    "targets": 8,
                    "className": "text-left",
                },
                {
                    //subareaPreventa
                    "targets": 9,
                    "className": "text-left",
                },

                {
                    //Unidad comercial
                    "targets": 7,
                    "className": "text-left",
                },

                {
                    //estado
                    "targets": 11,
                    "className": "text-left",
                    //"render": function (data, type, row, meta) {
                    //          return "<nobr>" + getLiteralEstado(data) + "</nobr>";
                    //}

                    "render": function (data, type, row, meta) {
                        return "<nobr>" + getLiteralEstado(data) + "</nobr>";
                    }


                },
                {
                    //cliente
                    "targets": 4,
                    "className": "text-left",
                   
                },
                {
                    //comercial
                    "targets": 6,
                    "className": "text-left",
                   
                },
                {
                    //promotor
                    "targets": 5,
                    "className": "text-left",
                   
                },
                {
                    //lider
                    "targets": 10,
                    "className": "text-left",
                    
                },
                {
                    //ta204_fechacreacion
                    "targets": 12,
                    "type": "date",
                    "className": "text-center",
                    "render": {
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
                {
                    //ta204_fechafinestipulada
                    "targets": 13,
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
                        }
                    }
                },

                 {
                     //ta204_fechafinreal
                     "targets": 14,
                     "type": "date",
                     "className": "text-center",
                     "render": {
                         "display": function (data, type, row, meta) {
                             if (data != "0001-01-01T00:00:00")
                                 if (moment(data) > moment(row.ta204_fechafinestipulada))
                                     return "<span style='color:red;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";
                                 else
                                     return "<span style='color:green;font-weight:bold'>" + moment(data).format('DD/MM/YYYY') + "</span>";

                             else
                                 return "";
                         },
                         "filter": function (data, type, row, meta) {
                             return moment(data).format('DD/MM/YYYY');
                         }
                     }
                 }

            ],

            "createdRow": function (row, data, index) {
                if (data.ta208_negrita)
                    $(row).css("font-weight", "bold").attr("data-negrita", "true");
                else
                    $(row).css("font-weight", "normal");
                //$(row).attr("data-idaccion", data.ta204_idaccionpreventa) //Asignar id a la fila
            },
            "drawCallback": function (settings) {
                //$("#dtCatalogoPedidos a[data-toggle='tooltip']").tooltip();
            }

        })
        dt.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //set to 0, which means "initial draw" which with a clear table will show "loading..." again.
            $(this).dataTable().api().draw();
        });
    }

    function tieneNegrita(row) {
        if ($(row).parent().parent().attr("data-negrita") == "true") return true;
        else return false;
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

    return {
        selector: selector,
        init: init,
        getFilterValuesWithDesc: getFilterValuesWithDesc,
        attachLiveEvents: attachLiveEvents,
        attachEvents: attachEvents,
        tieneNegrita: tieneNegrita        
    }

})();






