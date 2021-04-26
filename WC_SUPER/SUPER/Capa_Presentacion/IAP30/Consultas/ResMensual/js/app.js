$(document).ready(function () { SUPER.IAP30.ResMensual.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ResMensual = SUPER.IAP30.ResMensual || {}

SUPER.IAP30.ResMensual.app = (function (view) {

    
    var init = function () {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }
              
        //Manejador de fechas
        moment.locale('es');

        //Inicializa formateo
        accounting.settings = {
            number: {
                precision: 2,
                thousand: ".",
                decimal: ","
            }
        }

        view.init();

        /****************************** CALCULO DE VARIABLES ****************************************************/
        /*        
        */
        
        //Se atachan los eventos 
 
        //Redimensionamiento de la ventana
        //view.attachEvents("resize", view.dom.ventana, view.redimensionPantalla);

        //Acciones a realizar al clickar el combo de opciones
        view.attachEvents("change", view.dom.opcionSel, getPeriodo);
        //Acciones a realizar al clickar las flechas izda y derecha

        //Mikel
        //view.attachEvents("click", view.dom.flechaIzda, setPeriodo);
        //view.attachEvents("click", view.dom.flechaDcha, setPeriodo);

        //A revisar pendiente de conseguir que funcione el evento onchange

        view.attachLiveEvents('change', view.selectores.sel_datepickers, obtenerDatos);

        //Acciones al pulsar el ícono imprimir
        view.attachEvents("click", view.dom.Imprimir, Imprimir);

        //view.dom.divFlechas.hide();
        view.dom.fechaDesde.attr("readonly", "readonly");
        view.dom.fechaHasta.attr("readonly", "readonly");
        view.dom.ano.attr("readonly", "readonly");
        view.dom.anoMes.attr("readonly", "readonly");
        view.dom.mes.attr("readonly", "readonly");

        obtenerDatos();
        
    }

    var obtenerDatos = function () {

        validarFechas();
        view.ocultarContainer();
        IB.procesando.mostrar();

        var a = obtenerDatosMensuales();
        var b = obtenerDatosDetalle();

        $.when(a, b).then(function () {
            IB.procesando.ocultar()
            view.visualizarContainer();
        });

    }
    var validarFechas = function () {
        var sFecha1 = "";
        var iMonth1 = 0;
        var sYear1 = "";
        var selDate1 = view.dom.fechaDesde.val();
        sYear1 = selDate1.substring(selDate1.length - 4, selDate1.length);
        iMonth1 = jQuery.inArray(selDate1.substring(0, selDate1.length - 5),
                            view.dom.fechaDesde.datepicker('option', 'monthNames'));

        iMonth1 = parseInt(iMonth1, 10) + 1;

        if (iMonth1 < 10) sFecha1 = sYear1 + "0" + iMonth1.toString();
        else sFecha1 = sYear1 + iMonth1.toString();

        var sFecha2 = "";
        var iMonth2 = 0;
        var sYear2 = "";
        var selDate2 = view.dom.fechaHasta.val();
        sYear2 = selDate2.substring(selDate2.length - 4, selDate2.length);
        iMonth2 = jQuery.inArray(selDate2.substring(0, selDate2.length - 5),
                        view.dom.fechaHasta.datepicker('option', 'monthNames'));

        iMonth2 = parseInt(iMonth2, 10) + 1;

        if (iMonth2 < 10) sFecha2 = sYear2 + "0" + iMonth2.toString();
        else sFecha2 = sYear2 + iMonth2.toString();

        if (parseInt(sFecha1, 10) > parseInt(sFecha2, 10) && IB.vars.fechaInicioOld != selDate1) {
            view.dom.fechaHasta.datepicker('setDate', new Date(sYear1, iMonth1-1, 1));
            IB.vars.fechaHasta = sFecha1;
            IB.vars.fechaInicioOld = selDate1;
            IB.vars.fechaFinOld = selDate1;
        }
        else if (parseInt(sFecha2, 10) < parseInt(sFecha1, 10) && IB.vars.fechaFinOld != selDate2)
        {
            view.dom.fechaDesde.datepicker('setDate', new Date(sYear2, iMonth2-1, 1));
            IB.vars.fechaDesde = sFecha2;
            IB.vars.fechaInicioOld = selDate2;
            IB.vars.fechaFinOld = selDate2;
        }

    }
    obtenerDatosMensuales = function (e) {
        try {
            var defer = $.Deferred();

            view.dom.tablaDetalle.html("");
            view.dom.tablaTotal.html("");

            var filtro = { sProfesionales: IB.vars.codUsu, sDesde: IB.vars.fechaDesde, sHasta: IB.vars.fechaHasta, sTipo: 'M' };

            IB.DAL.post(null, "getDatos", filtro, null,
                function (data) {
                    view.pintarTablaDatos(data);
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar los datos de la tabla mensual en el servidor.");
                    defer.fail();
                }
            );
            return defer.promise();

        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al controlar el rango de fechas.");
        }
    }

    obtenerDatosDetalle = function (e) {
        try {
            var defer = $.Deferred();

            var filtro = { sProfesionales: IB.vars.codUsu, sDesde: IB.vars.fechaDesde, sHasta: IB.vars.fechaHasta, sTipo: 'D' };

            //IB.procesando.mostrar();
            IB.DAL.post(null, "getDatos", filtro, null,
                function (data) {
                    view.pintarTablaDatosDetalle(data);
                    defer.resolve();
                },
                function (ex, status) {
                    IB.procesando.ocultar();
                    IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al recuperar los datos de la tabla diaria en el servidor.");
                    defer.fail();
                }
            );
            return defer.promise();


        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener los datos diarios.");
        }
    }
    
    getPeriodo = function (e) {
        try {
            var oFecha = new Date();
            var sMes = "";
            var iMes = 0;
            //switch (parseInt(dom.opcionSel.find('option:selected').val(), 10)) {

            //view.dom.divFlechas.show();
            //if (parseInt($(this).val(), 10) != 0) asignarControlDatepicker();
            //else {
            //    view.dom.fechaDesde.datepicker("destroy");
            //    view.dom.fechaHasta.datepicker("destroy");
            //}
            asignarControlDatepicker();
            view.dom.fechaDesde.datepicker('disable');
            view.dom.fechaHasta.datepicker('disable');

            switch (parseInt($(this).val(), 10)) {
                case 0:
                    view.dom.fechaDesde.datepicker('enable');
                    view.dom.fechaHasta.datepicker('enable');

                    var iMonth1 = 0;
                    var iYear1 = 0;
                    //var monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];

                    var selDate1 = view.dom.fechaDesde.val();
                    iYear1 = parseInt(selDate1.substring(selDate1.length - 4, selDate1.length), 10);
                    iMonth1 = parseInt(jQuery.inArray(selDate1.substring(0, selDate1.length - 5),
                                    view.dom.fechaDesde.datepicker('option', 'monthNames')), 10);
                                       //monthNames), 10);

                    iMonth1 = parseInt(iMonth1, 10) + 1;

                    iMes = iMonth1 + 1;
                    if (iMes < 10) sMes = "0" + iMes.toString();
                    else sMes = iMes.toString();

                    IB.vars.fechaDesde = iYear1.toString() + sMes;
                    view.dom.fechaDesde.datepicker('setDate', new Date(iYear1.toString(), iMes - 1, 1));
                    view.dom.fechaHasta.datepicker('setDate', new Date(iYear1.toString(), iMes - 1, 1));
                    IB.vars.fechaHasta = IB.vars.fechaDesde;

                    //var fDesde = view.dom.fechaDesde.datepicker("getDate");
                    //var fhasta = view.dom.fechaDesde.datepicker("getDate");
                    break;
                case 1:

                    view.dom.fechaDesde.datepicker('setDate', new Date(oFecha.getFullYear(), 0, 1));
                    IB.vars.fechaDesde = oFecha.getFullYear().toString() + "01";
                    view.dom.fechaHasta.datepicker('setDate', new Date(oFecha.getFullYear(), 11, 1));
                    IB.vars.fechaHasta = oFecha.getFullYear().toString() + "12";
                    break;
                case 2:


                    iMes = oFecha.getMonth() + 1;
                    if (iMes < 10) sMes = "0" + iMes.toString();
                    else sMes = iMes.toString();

                    IB.vars.fechaDesde = oFecha.getFullYear().toString() + sMes;
                    view.dom.fechaDesde.datepicker('setDate', new Date(oFecha.getFullYear(), iMes - 1, 1));
                    view.dom.fechaHasta.datepicker('setDate', new Date(oFecha.getFullYear(), iMes - 1, 1));
                    IB.vars.fechaHasta = IB.vars.fechaDesde;
                    break;
                case 3:
                    view.dom.fechaHasta.datepicker('enable');

                    view.dom.fechaDesde.datepicker('setDate', new Date(oFecha.getFullYear(), 0, 1));
                    IB.vars.fechaDesde = oFecha.getFullYear().toString() + "01";

                    iMes = oFecha.getMonth() + 1;
                    if (iMes < 10) sMes = "0" + iMes.toString();
                    else sMes = iMes.toString();

                    IB.vars.fechaHasta = oFecha.getFullYear().toString() + sMes;
                    view.dom.fechaHasta.datepicker('setDate', new Date(oFecha.getFullYear(), iMes - 1, 1));
                    break;
                case 4:
                    view.dom.fechaDesde.datepicker('setDate', new Date(1990, 0, 1));
                    IB.vars.fechaDesde = "199001";
                    iMes = oFecha.getMonth() + 1;
                    if (iMes < 10) sMes = "0" + iMes.toString();
                    else sMes = iMes.toString();
                    IB.vars.fechaHasta = oFecha.getFullYear().toString() + sMes;
                    view.dom.fechaHasta.datepicker('setDate', new Date(oFecha.getFullYear(), iMes - 1, 1));
                    break;
                case 5:
                    view.dom.fechaDesde.datepicker('setDate', new Date(1990, 0, 1));
                    IB.vars.fechaDesde = "199001";
                    view.dom.fechaHasta.datepicker('setDate', new Date(2078, 11, 1));
                    IB.vars.fechaHasta = "207812";
                    break;
            }
            obtenerDatos();
        } catch (e) {
            IB.bsalert.toastdanger(e.message);
        }
    }

    //setPeriodo = function (e) {
    //    try {
    //        var sOpcion = parseInt(view.dom.opcionSel.find('option:selected').val(), 10);
    //        //if (sOpcion == 0) {
    //        //    IB.bsalert.fixedAlert("warning", "Validación", "No hay ninguna opción seleccionada");
    //        //    return;
    //        //}
    //        if (sOpcion == 5) {
    //            IB.bsalert.fixedAlert("warning", "Error de validación", "Este rango temporal no permite navegación");
    //            return;
    //        }

    //        var oFecha = new Date();
    //        var sMes = "";
    //        var iMes = 0;
    //        var iAno = 0;

    //        var iMonth1 = 0;
    //        var iYear1 = 0;
    //        //var monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
    //        var selDate1 = view.dom.fechaDesde.val();
    //        iYear1 = parseInt(selDate1.substring(selDate1.length - 4, selDate1.length), 10);
    //        iMonth1 = parseInt(jQuery.inArray(selDate1.substring(0, selDate1.length - 5),
    //                            view.dom.fechaDesde.datepicker('option', 'monthNames')), 10);
    //        //                   monthNames), 10);

    //        iMonth1 = parseInt(iMonth1, 10) + 1;

    //        var iMonth2 = 0;
    //        var iYear2 = 0;

    //        var selDate2 = view.dom.fechaHasta.val();
    //        iYear2 = parseInt(selDate2.substring(selDate2.length - 4, selDate2.length), 10);
    //        iMonth2 = parseInt(jQuery.inArray(selDate2.substring(0, selDate2.length - 5),
    //                        view.dom.fechaHasta.datepicker('option', 'monthNames')), 10);
    //        //                 monthNames), 10);

    //        iMonth2 = parseInt(iMonth2, 10) + 1;


    //        switch (sOpcion) {
    //            case 0:
    //                if ($(this.id).selector == "flechaIzda") {
    //                    if (iMonth1 == 1) {
    //                        iMonth1 = 12;
    //                        iYear1 = iYear1 - 1;
    //                    }
    //                    else iMonth1 = iMonth1 - 1;
    //                }
    //                else {
    //                    if (iMonth2 == 12) {
    //                        iMonth2 = 1;
    //                        iYear2 = iYear2 + 1;
    //                    }
    //                    else iMonth2 = iMonth2 + 1;
    //                }
    //                iMes = iMonth1;
    //                if (iMes < 10) sMes = "0" + iMes.toString();
    //                else sMes = iMes.toString();
    //                IB.vars.fechaDesde = iYear1.toString() + sMes;
    //                view.dom.fechaDesde.datepicker('setDate', new Date(iYear1, iMonth1 - 1, 1));
    //                iMes = iMonth2;
    //                if (iMes < 10) sMes = "0" + iMes.toString();
    //                else sMes = iMes.toString();
    //                IB.vars.fechaHasta = iYear2.toString() + sMes;
    //                view.dom.fechaHasta.datepicker('setDate', new Date(iYear2, iMonth2 - 1, 1));
    //                break;

    //            case 1:

    //                if ($(this.id).selector == "flechaIzda") iYear1 = iYear1 - 1;
    //                else iYear1 = iYear1 + 1;

    //                view.dom.fechaDesde.datepicker('setDate', new Date(iYear1, 0, 1));
    //                view.dom.fechaHasta.datepicker('setDate', new Date(iYear1, 11, 1));

    //                IB.vars.fechaDesde = iYear1.toString() + "01";
    //                IB.vars.fechaHasta = iYear1.toString() + "12";
    //                break;
    //            case 2:
    //                if ($(this.id).selector == "flechaIzda") {
    //                    if (iMonth1 == 1) {
    //                        iMonth1 = 12;
    //                        iYear1 = iYear1 - 1;
    //                    }
    //                    else iMonth1 = iMonth1 - 1;
    //                }
    //                else {
    //                    if (iMonth1 == 12) {
    //                        iMonth1 = 1;
    //                        iYear1 = iYear1 + 1;
    //                    }
    //                    else iMonth1 = iMonth1 + 1;
    //                }
    //                iMes = iMonth1;
    //                if (iMes < 10) sMes = "0" + iMes.toString();
    //                else sMes = iMes.toString();
    //                IB.vars.fechaDesde = iYear1.toString() + sMes;
    //                view.dom.fechaDesde.datepicker('setDate', new Date(iYear1, iMonth1 - 1, 1));
    //                view.dom.fechaHasta.datepicker('setDate', new Date(iYear1, iMonth1 - 1, 1));
    //                IB.vars.fechaHasta = IB.vars.fechaDesde;
    //                break;
    //            case 3:
    //                if ($(this.id).selector == "flechaIzda") {
    //                    if (iMonth2 == 1) {
    //                        iMonth2 = 12;
    //                        iYear2 = iYear2 - 1;
    //                    }
    //                    else iMonth2 = iMonth2 - 1;
    //                }
    //                else {
    //                    if (iMonth2 == 12) {
    //                        iMonth2 = 1;
    //                        iYear2 = iYear2 + 1;
    //                    }
    //                    else iMonth2 = iMonth2 + 1;
    //                }
    //                iMes = iMonth2;
    //                if (iMes < 10) sMes = "0" + iMes.toString();
    //                else sMes = iMes.toString();
    //                IB.vars.fechaHasta = iYear2.toString() + sMes;
    //                view.dom.fechaHasta.datepicker('setDate', new Date(iYear2, iMonth2 - 1, 1));
    //                break;
    //            case 4:
    //                if ($(this.id).selector == "flechaIzda") {
    //                    if (iMonth2 == 1) {
    //                        iMonth2 = 12;
    //                        iYear2 = iYear2 - 1;
    //                    }
    //                    else iMonth2 = iMonth2 - 1;
    //                }
    //                else {
    //                    if (iMonth2 == 12) {
    //                        iMonth2 = 1;
    //                        iYear2 = iYear2 + 1;
    //                    }
    //                    else iMonth2 = iMonth2 + 1;
    //                }
    //                iMes = iMonth2;
    //                if (iMes < 10) sMes = "0" + iMes.toString();
    //                else sMes = iMes.toString();
    //                IB.vars.fechaHasta = iYear2.toString() + sMes;
    //                view.dom.fechaHasta.datepicker('setDate', new Date(iYear2, iMonth2 - 1, 1));
    //                break;
    //        }
    //        obtenerDatos();

    //    } catch (e) {
    //        mostrarErrorAplicacion("Error", e.message);
    //    }
    //}

    return {
        init: init
    }

})(SUPER.IAP30.ResMensual.View);