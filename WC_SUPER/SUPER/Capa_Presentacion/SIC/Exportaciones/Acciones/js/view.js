/// <reference path="../../../../../scripts/IB.js" />

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view = (function () {



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
        txtFFinHasta: $("#txtFFinHasta")
    }

    $("#btnExportar").on("click", btnExportar_onClick);

  

    function init() {

        //formato numerico en los filtros de importes
        $('#txtImporteDesde').autoNumeric('init', { aSep: '.', aDec: ',', mDec: 0, anDefault: '0' });
        $('#txtImporteHasta').autoNumeric('init', { aSep: '.', aDec: ',', mDec: 0, anDefault: '0' });

        //Click en limpiarfiltros
        $("#lnkLimpiarFiltros").on("click", limpiarFiltros)

        //Combo origen change --> actualizar la ayuda para el campo itemorigen
        $("#cmbOrigen").on("change", cmbOrigen_onChange);

        //Click en lnkItemOrigen
        dom.lnkAyudaItemOrigen.on("click", lnkAyudaItemOrigen_onClick);
        $("#btnClearItemOrigen").on("click", function () {
            dom.txtItemOrigen.removeAttr("data-key");
            dom.txtItemOrigen.val("");
        });

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
            titulo: "Selección de promotor de acción preventa",
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
        
    function limpiarFiltros() {
        
        $("#cmbEstado").val("");
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

    function validarFiltros() {

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
            return false;
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
            return false;
        }

        return true;

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

    function btnExportar_onClick() {


        //validarf filtros (rangos de fechas e importes)
        if (!validarFiltros()) return;

        var filter = getFilterValuesWithDesc();

        var params = "";

        if (filter.estado != null) params += "|estado:" + filter.estado;
        if (filter.itemorigen != null) params += "|itemorigen:" + filter.itemorigen;
        if (filter.iditemorigen != null) params += "|iditemorigen:" + filter.iditemorigen;
        if (filter.importeDesde != null) params += "|importeDesde:" + filter.importeDesde;
        if (filter.importeHasta != null) params += "|importeHasta:" + filter.importeHasta;
        if (filter.ffinDesde != null) params += "|ffinDesde:" + filter.ffinDesde;
        if (filter.ffinHasta != null) params += "|ffinHasta:" + filter.ffinHasta;
        if (filter.promotor != null) params += "|promotor:" + filter.promotor;
        if (filter.comercial != null) params += "|comercial:" + filter.comercial;
        if (filter.lideres != null) params += "|lideres:" + filter.lideres.join(";");
        if (filter.clientes != null) params += "|clientes:" + filter.clientes.join(";");
        if (filter.acciones != null) params += "|acciones:" + filter.acciones.join(";");
        if (filter.unidades != null) params += "|unidades:" + filter.unidades.join(";");
        if (filter.areas != null) params += "|areas:" + filter.areas.join(";");
        if (filter.subareas != null) params += "|subareas:" + filter.subareas.join(";");

        //Quitar la pipe del primer filtro
        if (params.length > 0 && params.substr(0, 1) == "|") params = params.substr(1);
        
        var url = "../exportar.aspx?" + IB.uri.encode("origenmenu=" + IB.vars.origenMenu + "&exportid=acciones&filters=" + params);
        $("#iframeExportar").attr("src", url);

    }
   

    return {
        init: init
    }

})();

