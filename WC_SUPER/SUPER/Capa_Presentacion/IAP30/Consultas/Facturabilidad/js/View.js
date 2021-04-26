var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Facturabilidad = SUPER.IAP30.Facturabilidad || {}


SUPER.IAP30.Facturabilidad.View = (function (e) {
    var myPieChart;
    var dom = {
        container: $('.container'),
        icoProfesional: $('#divProf'),
        divBuscadorPersonas: $('.buscadorUsuario'),
        imagenProf: $('#imagenProf'),
        spProfesional: $('#spProfesional'),
        btnTarea: $('#btnTarea'),
        txtRango: $('#txtRango'),
        tabla: $("#tabla"),
        tabla2: $("#tabla2")
    }
    var selectores = {
        btnExportExcel: ".btnExportExcel",
        btnExportExcel2: ".btnExportExcel2",        
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }
    //para elementos que no existen de inicio
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    //Función de cebreo de las filas de la tabla
    function cebrear(sBody) {

        //$("tr:not(.bg-info)").removeClass("cebreada");
        //$('tr:visible:not(.bg-info, activa):even').addClass('cebreada');
        if (!sBody) sBody = "#bodyTabla";
        //$("#bodyTabla > tr:visible").removeClass("cebreada");
        //$('#bodyTabla > tr:visible:even').addClass('cebreada');
        $(sBody+" > tr:visible").removeClass("cebreada");
        $(sBody+' > tr:visible:even').addClass('cebreada');
        //controlarScroll();

    }


    function inicializarPantalla() {                

        ////Inicialización de datepickers  
        inicializarCalendarios();

    }

    var inicializarCalendarios = function () {

        var hoy;

        if (IB.vars.UMC_IAP != "") {
            hoy = moment(IB.vars.UMC_IAP + "01", 'YYYY/MM/DD').add(1, 'month');
        } else {
            hoy = moment();
        }

        $('#txtRango').daterangepicker({
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aceptar',
                cancelLabel: 'Cancelar',
            },
            startDate: hoy.startOf('month').format('L'),
            endDate: hoy.startOf('month').add(1, 'months').subtract(1, "days").format('L'),
            linkedCalendars: false,
            disableHoverDate: true
        });       
       
    }

    //Pintado de la tabla en la carga
    var pintarTablaCarga = function (dataSource) {


        
        var columnas = [
                { "data": "Proyecto" },
                { "data": "Tarea" },
                {
                  "orderSequence": ["desc"],
                  "data": "Facturable", render: function (data) {
                      return data ? '<img alt="Icono de tarea facturable" border="0" src="../../../../Images/imgIcoMonedas.gif" />' :
                                    '<img alt="Icono de tarea facturable" border="0" src="../../../../Images/imgIcoMonedasOff.gif" />';
                  }
                },
                { "data": "t332_etpl", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "t336_etp", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_planificadas_periodo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_tecnico_periodo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_otros_periodo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_total_periodo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_planificadas_finperiodo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_tecnico_finperiodo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_otros_finperiodo", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horas_total_finperiodo", render: $.fn.dataTable.render.number('.', ',', 2, '') }
        ];
        
        try{
            var table = $("#tabla")
                .DataTable({
                    destroy: true,
                    "columns": columnas,
                    data:dataSource,
                    scrollY: "38vh",
                    scrollX: true,
                    "bScrollCollapse": true,
                    paging: false,
                    language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
                    bInfo: false,
                    "footerCallback": function ( row, data, start, end, display ) {
                        var api = this.api(), data;
                        // Total over all pages
                        aTotal = new Array(10);
                        for (i = 0; i < 11; i++) {
                            //Calculo la suma de cada columna aplicando el filtro por si el usuario ha hecho una búsqueda
                            aTotal[i] = api.column(i + 2, { "filter": "applied" }).data().reduce(function (a, b) { return a + b; }, 0);
                        }
                        for (i = 4; i < 14; i++) {
                            //Actualizo el total
                            $(api.column(i-1).footer()).html(accounting.formatNumber(aTotal[i-3]));
                        }
                        pintarTipologias(data, $("#tabla_filter input").val().toUpperCase());
                    },
                    /*, "initComplete": function (settings, json) {
                        $('#tabla_filter input').unbind();
                        $('#tabla_filter input').bind('keyup', function (e) {
                            if (e.keyCode == 13) {
                                table.search(this.value).draw();                              
                            }
                        });
                        //$('#tabla_filter').after("<div class='pull-right btnExportExcel'><span tabindex='0' role='link' title='Exportar a excel el contenido de la tabla' aria-label='Exportar a excel el contenido de la tabla' class='fa fa-file-excel-o fa-1-5x'></span></div>");
                    },*/
                    dom: 'f<"pull-right"B>t',
                    buttons: [
                        {
                            className: 'btnExportExcel',
                            text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                            titleAttr: 'Exportar a EXCEL'
                        }
                    ]
                });            	            
        }
        catch (e) {
            IB.bsalert.fixedAlert("warning", "Error de aplicación", e.message);
            //alert("Error: " + e.message);
            if (e.message == "No se puede obtener la propiedad 'style' de referencia nula o sin definir")
                pintarTablaCarga(dataSource);
        }
    }
    function buscarNaturaleza(array, key, value) {
        for (var i = 0; i < array.length; i++) {
            if (array[i][key] === value) {
                return array[i];
            }
        }
        return null;
    }

    var calcularTotalesProyectos = function() {
        $('#cabProy > th').each(function (i) {
            if (i>2)
                calcularColumna(i);
        });
    }
    var calcularColumna = function (index) {
        var total = 0;
        $('#tabla > tbody > tr').each(function ()
        {
            var value = parseFloat($('td', this).eq(index).text().replace('.', '').replace(',', '.'));
            if (!isNaN(value))
            {
                total += value;
            }
        });
        $('#tabla > tfoot td').eq(index).text(accounting.formatNumber(total));
    }

    var pintarTipologias = function (data, filtro) {
        var dSi = 0, dNo = 0;
        var sHtml = new StringBuilder();
        //Array para pintar la tabla por naturalezas
        var aNat = new Array();

        for (var i = 0; i < data.length; i++) {
            //console.log("Proy=" + data[i].Proyecto + " Tarea=" + data[i].Tarea + "HF=" + data[i].horasF + "HNF=" + data[i].horasNF);
            if (filtro == "" || data[i].Proyecto.toUpperCase().indexOf(filtro) > -1
                             || data[i].Tarea.toUpperCase().indexOf(filtro) > -1) {
                var oNat = new Object();
                oNat.idT = data[i].t320_idtipologiaproy;
                oNat.denT = data[i].t320_denominacion;
                oNat.idN = data[i].t323_idnaturaleza;
                oNat.denN = data[i].t323_denominacion;
                if (data[i].Facturable) {
                    oNat.horasF = data[i].horas_tecnico_periodo;
                    oNat.horasNF = 0;
                }
                else {
                    oNat.horasNF = data[i].horas_tecnico_periodo;
                    oNat.horasF = 0;
                }
                var miItem = buscarNaturaleza(aNat, 'idN', oNat.idN);
                if (miItem != null) {
                    miItem.horasF += oNat.horasF;
                    miItem.horasNF += oNat.horasNF;
                }
                else {
                    aNat.push(oNat);
                }
            }
        }
        for (var i = 0; i < aNat.length; i++) {

            //Horas facturables
            dSi += aNat[i].horasF;

            //Horas no facturables
            dNo += aNat[i].horasNF;
        }        

        var columnas = [
                { "data": "denT" },
                { "data": "denN" },
                { "data": "horasF", render: $.fn.dataTable.render.number('.', ',', 2, '') },
                { "data": "horasNF", render: $.fn.dataTable.render.number('.', ',', 2, '') }
        ];

        var table = $("#tabla2").DataTable({
            destroy: true,
            "columns": columnas,
            data: aNat,
            searching: false,
            scrollY: "15vh",
            scrollX: true,
            "bScrollCollapse": true,
            paging: false,
            language: { "decimal": ",", "thousands": ".", "url": IB.vars["strserver"] + 'plugins/datatables/Spanish.txt' },
            bInfo: false,
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api(), data;
                for (i = 2; i < 4; i++) {
                    $(api.column(i).footer()).html(accounting.formatNumber(api.column(i).data().reduce(function (a, b) { return a + b; }, 0)));
                }                
            },
            dom: 'f<"pull-right"B>t',
            buttons: [
                {
                    className: 'btnExportExcel2',
                    text: 'EXCEL',
                    text: '<i class="fa fa-file-excel-o"></i> EXCEL',
                    titleAttr: 'Exportar a EXCEL'
                }
            ]
        });

        pintarGrafico(dSi.toFixed(2), dNo.toFixed(2));

        if(comprobarVisibilidad(dom.container)) visualizarContenido();

    }

    var pintarGrafico = function (dSi, dNo) {


        //Gráfico
        var data = {
            labels: [
                "Facturable",
                "No facturable"
            ],
            datasets: [
                {
                    data: [dSi, dNo],
                    backgroundColor: [
                        "#F8D14C",
                        "#D5D5D5"
                    ],
                    hoverBackgroundColor: [
                        "#F8D14C",
                        "#D5D5D5"
                    ]
                }]
        };
        var options = {
            legend: {
                display: true,
                position: 'bottom',
                onClick: function (event, legendItem) { }//Para que en el click sobre la leyenda no haga nada
            }
            //,responsive: true,
            //,maintainAspectRatio: false,
            //,responsiveAnimationDuration:500
        };

       
        var ctx = document.getElementById("myChart");
        if (myPieChart != undefined) myPieChart.destroy();
        myPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
        //myPieChart.render();
        //setTimeout(function () { myPieChart.update(); }, 500);
        myPieChart.update(200, 500);
    }
    var deshabilitarLink = function (link) {

        $(link).attr("role", "");
        $(link).attr("title", "");
        $(link).attr("tabindex", "");
        $(link).removeClass('link');
        $(link).css({ opacity: 0.1 });

    }

    function pintarDatosDespuesReconexion(data) {
        //IB.vars.aFestivos = data.aFestivos;
        //IB.vars.codCal = data.IdCalendario;
        //dom.spDesCalendario.html(data.desCalendario);
        //dom.spDesCalendarioMov.html(data.desCalendario);

        //if (data.fUltImputacion == null) data.fUltImputacion = "";
        //else data.fUltImputacion = moment(data.fUltImputacion).format("DD/MM/YYYY");

        //dom.lblFUI.html(data.fUltImputacion);
        //dom.lblUMC.html(AnoMesToMesAnoDescLong(data.t303_ultcierreIAP));
        //dom.imagenProf.attr('src', IB.vars["strserver"] + "images/imgUSU" + data.tipo + data.t001_sexo + ".gif");

        //IB.vars.UMC_IAP = data.t303_ultcierreIAP.toString();
        //IB.vars.FechaUltimaImputacion = data.fUltImputacion;
        //IB.vars.controlhuecos = (data.t314_controlhuecos) ? "true" : "false";
        IB.vars.idficepi = data.t001_IDFICEPI;
        //dom.spProfesional.html(data.PROFESIONAL);

        //if (IB.vars.FechaUltimaImputacion != "") {
        //    aUltimoDia = IB.vars.FechaUltimaImputacion.split("/");
        //    oUltImputac = new Date(aUltimoDia[2], eval(aUltimoDia[1] - 1), aUltimoDia[0]);

        //    var oUMC = new Date(IB.vars.UMC_IAP.substring(0, 4), eval(IB.vars.UMC_IAP.substring(4, 6) - 1), 1).add("mo", 1).add("d", -1);
        //    if (oUltImputac < oUMC) oUltImputac = oUMC;

        //} else oUltImputac = null;

        ////Se resetea el objeto calendario con los datos del profesional reconectado
        //dom.objetoCal.monthly({
        //    'reset': true
        //});

        IB.procesando.ocultar();
    }
    function deshabilitarReconexion() {
        dom.icoProfesional.css('display', 'none');
        //dom.btnUser.css('display', 'none');
        //dom.divIconoUser.css('display', 'none');
    }

    var visualizarContenido = function () {
        $('.ocultaProcesando').css("visibility", "visible");
    }

    var ocultarContenido = function () {
        $('.ocultaProcesando').css("visibility", "hidden");
    }

    var comprobarVisibilidad = function (elemento) {
        return(elemento.css("visibility") == "visible");
    }

    var init = function () {
        inicializarPantalla();
        $(window).on("orientationchange", function () {            
            var dSi = accounting.unformat($('#tabla2 tfoot tr td:nth-child(3)').html(), ",");
            var dNo = accounting.unformat($('#tabla2 tfoot tr td:nth-child(4)').html(), ",");
            pintarGrafico(dSi, dNo);   
        });

    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        pintarTablaCarga: pintarTablaCarga,
        pintarTipologias: pintarTipologias,
        pintarDatosDespuesReconexion: pintarDatosDespuesReconexion,
        deshabilitarReconexion: deshabilitarReconexion,
        ocultarContenido: ocultarContenido,
        comprobarVisibilidad: comprobarVisibilidad
    }
})();