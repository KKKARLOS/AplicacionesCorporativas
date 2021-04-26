/// <reference path="../../../../../scripts/IB.js" />

var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view = (function () {



    var dom = {
        divAyudaLideres: $("#divAyudaLideres"),
        divAyudaUnidades: $("#divAyudaUnidades"),
        divAyudaAreas: $("#divAyudaAreas"),
        divAyudaSubareas: $("#divAyudaSubareas"),
        txtFFinDesde: $("#txtFFinDesde"),
        txtFFinHasta: $("#txtFFinHasta"),
        txtFFinDesde_tarea: $("#txtFFinDesde_tarea"),
        txtFFinHasta_tarea: $("#txtFFinHasta_tarea")
    }

    $("#btnExportar").on("click", btnExportar_onClick);



    function init() {

        //Click en limpiarfiltros
        $("#lnkLimpiarFiltros").on("click", limpiarFiltros)


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
                    IB.bsalert.toastdanger("La 'fecha fin estipulada desde' introducida no es correcta.");
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
                    IB.bsalert.toastdanger("La 'fecha fin estipulada hasta' introducida no es correcta.");
                    //dom.txtffinhasta.val("");
                    dom.txtFFinHasta.focus();
                    return;
                }
            }

        });

        //Datepicker fechas desde-hasta
        dom.txtFFinDesde_tarea.datepicker({
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
                    IB.bsalert.toastdanger("La 'fecha fin prevista desde' introducida no es correcta.");
                    //dom.txtffindesde.val("");
                    dom.txtFFinDesde_tarea.focus();
                    return;
                }
            }

        });

        dom.txtFFinHasta_tarea.datepicker({
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
                    IB.bsalert.toastdanger("La 'fecha fin prevista hasta' introducida no es correcta.");
                    //dom.txtffinhasta.val("");
                    dom.txtFFinHasta_tarea.focus();
                    return;
                }
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
        $("#cmbEstado_tarea").val("");
        $("#txtFFinDesde").val("");
        $("#txtFFinHasta").val("");
        $("#txtFFinDesde_tarea").val("");
        $("#txtFFinHasta_tarea").val("");
        $("#lstLideres").html("");
        $("#lstUnidades").html("");
        $("#lstAreas").html("");
        $("#lstSubareas").html("");

    }
    

    function validarFiltros() {

        var filter = getFilterValues();
        var msgerr = "";
        var fdesde = null;
        var fhasta = null;
        var fdesde_tarea = null;
        var fhasta_tarea = null;

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

        //validar formato de campos de introducción manual
        if (filter.ffinDesde_tarea != null) {
            try {
                var fdesde_tarea = $.datepicker.parseDate('dd/mm/yy', filter.ffinDesde_tarea)
            }
            catch (e) {
                msgerr += "<li>La 'fecha fin prevista desde' es incorrecta.</li>";
            }
        }
        if (filter.ffinHasta_tarea != null) {
            try {
                var fhasta_tarea = $.datepicker.parseDate('dd/mm/yy', filter.ffinHasta_tarea)
            }
            catch (e) {
                msgerr += "<li>La 'fecha fin prevista hasta' es incorrecta.</li>";
            }
        }
        if (msgerr.length > 0) {
            IB.bsalert.toastdanger("<ul class='list-group text-left'>" + msgerr + "</ul>");
            return false;
        }


        //validar rangos
        if (fdesde != null && fhasta != null) {
            if (fdesde > fhasta)
                msgerr += "<li'>Rango de fecha fin estipulada incorrecto.</li>";
        }
        if (fdesde_tarea != null && fhasta_tarea != null) {
            if (fdesde_tarea > fhasta_tarea)
                msgerr += "<li'>Rango de fecha fin prevista incorrecto.</li>";
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
        filter.estado_tarea = $("#cmbEstado_tarea option:selected").val() != "" ? $("#cmbEstado_tarea option:selected").val() : null;
        filter.ffinDesde = $("#txtFFinDesde").val() != "" ? $("#txtFFinDesde").val() : null;
        filter.ffinHasta = $("#txtFFinHasta").val() != "" ? $("#txtFFinHasta").val() : null;
        filter.ffinDesde_tarea = $("#txtFFinDesde_tarea").val() != "" ? $("#txtFFinDesde_tarea").val() : null;
        filter.ffinHasta_tarea = $("#txtFFinHasta_tarea").val() != "" ? $("#txtFFinHasta_tarea").val() : null;
        filter.lideres = getAttributeFromListItem($("#lstLideres li"), "data-idficepi");
        filter.unidades = getAttributeFromListItem($("#lstUnidades li"), "data-key");
        filter.areas = getAttributeFromListItem($("#lstAreas li"), "data-key");
        filter.subareas = getAttributeFromListItem($("#lstSubareas li"), "data-key");

        return filter;

    }

    function getFilterValuesWithDesc() {

        var filter = {};

        filter.estado = $("#cmbEstado option:selected").val() != "" ? $("#cmbEstado option:selected").val() : null;
        filter.estado_tarea = $("#cmbEstado_tarea option:selected").val() != "" ? $("#cmbEstado_tarea option:selected").val() : null;
        filter.ffinDesde = $("#txtFFinDesde").val() != "" ? $("#txtFFinDesde").val() : null;
        filter.ffinHasta = $("#txtFFinHasta").val() != "" ? $("#txtFFinHasta").val() : null;
        filter.ffinDesde_tarea = $("#txtFFinDesde_tarea").val() != "" ? $("#txtFFinDesde_tarea").val() : null;
        filter.ffinHasta_tarea = $("#txtFFinHasta_tarea").val() != "" ? $("#txtFFinHasta_tarea").val() : null;
        filter.lideres = getAttributeFromListItemWithDesc($("#lstLideres li"), "data-idficepi");
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
        if (filter.estado_tarea != null) params += "|estado_tarea:" + filter.estado_tarea;
        if (filter.ffinDesde != null) params += "|ffinDesde:" + filter.ffinDesde;
        if (filter.ffinHasta != null) params += "|ffinHasta:" + filter.ffinHasta;
        if (filter.ffinDesde_tarea != null) params += "|ffinDesde_tarea:" + filter.ffinDesde_tarea;
        if (filter.ffinHasta_tarea != null) params += "|ffinHasta_tarea:" + filter.ffinHasta_tarea;
        if (filter.lideres != null) params += "|lideres:" + filter.lideres.join(";");
        if (filter.unidades != null) params += "|unidades:" + filter.unidades.join(";");
        if (filter.areas != null) params += "|areas:" + filter.areas.join(";");
        if (filter.subareas != null) params += "|subareas:" + filter.subareas.join(";");

        //Quitar la pipe del primer filtro
        if (params.length > 0 && params.substr(0, 1) == "|") params = params.substr(1);

        //IB.procesando.mostrar();

        var url = "../exportar.aspx?" + IB.uri.encode("origenmenu=" + IB.vars.origenMenu + "&exportid=cargatrabajo&filters=" + params);
        $("#iframeExportar").attr("src", url);


    }


    return {
        init: init
    }

})();

