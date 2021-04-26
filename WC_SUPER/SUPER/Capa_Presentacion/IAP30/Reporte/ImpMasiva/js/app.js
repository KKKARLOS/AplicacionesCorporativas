$(document).ready(function () { SUPER.IAP30.ImpMasiva.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ImpMasiva = SUPER.IAP30.ImpMasiva || {}

SUPER.IAP30.ImpMasiva.app = (function (view) {

    var bControlhuecos;
    var num_empleado;
    var usuarioActual;
    var ultMesCerrado;
    var fechaUltImp;
    var fechaAlta;
    var numCal;
    var objDiaSigUDR;
    var objDiaAntUDR;
    var strAuxUltimoDia;
    var strDiaSigUDR;
    var primerDia;
    var objTarea;
    var aFestivos;

    function init() {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }
        //Inicializa formateo de importes
        accounting.settings = {
            number: {
                precision: 2, //es diferente por cada línea
                thousand: ".",
                decimal: ","
            }
        }

        /****************************** CALCULO DE VARIABLES ****************************************************/
        //debugger;
        bControlhuecos = IB.vars.bControlhuecos;
        num_empleado = IB.vars.num_empleado;
        usuarioActual = IB.vars.usuarioActual;
        ultMesCerrado = IB.vars.ultMesCerrado;
        fechaUltImp = IB.vars.fechaUltImp;
        fechaAlta = IB.vars.fechaAlta;
        numCal = IB.vars.numCal;
        aFestivos = IB.vars.aFestivos;
        //Se calcula cual es el siguiente día imputable al último día registrado
        var intUltimoDia = 0; var intDiaAnterior = 0;

        strAuxUltimoDia = fechaUltImp;
        if (strAuxUltimoDia == "") {
            fechaUltImp = new Date(2000, 0, 1);
            objDiaSigUDR = new Date(2000, 0, 2);
            objDiaAntUDR = new Date(1999, 11, 31);
            intUltimoDia = 0;
            intDiaAnterior = 0;
            primerDia = objDiaSigUDR;

            asignarValorFIni();            

        } else {

            calculoDeFechas();
        }
        
        

        //Ayuda tareas
        var options = {
            titulo: "Selección de tarea para imputación masiva",
            modulo: "IAP30",
            autoSearch: true,
            autoShow: false,
            searchParams: {               
            },
            onSeleccionar: function (data) {
                darValor(view.dom.tarea, data.idTarea);
                obtenerDatosTarea();
            },
            onCancelar: function () {
            }
        };

        view.dom.ayudaTarea.buscatarea(options);        

        /****************************** ************************ ****************************************************/

        //Se atachan los eventos 
        view.attachEvents("click", view.dom.title, view.collapse);
        view.attachEvents("click", view.dom.linkTarea, buscarTarea);
        view.attachEvents("click", view.dom.btnGuia, view.abrirGuia);
        view.attachEvents("change", view.dom.radioImputaciones, selTipoImputacion);
        /*view.attachEvents("focus", view.dom.txtFIni, abrirDatePicker);
        view.attachEvents("focus", view.dom.txtFFin, abrirDatePicker);
        view.attachEvents("focus", view.dom.txtFEst, abrirDatePicker);*/
        view.attachEvents("click", view.dom.btnGrabar, grabarDatos);
        view.attachEvents("keypress", view.dom.tarea, ejecutarAccionTecleo);
        view.attachEvents("focusout", view.dom.tarea, buscarTareaPorId);
        view.attachEvents("keypress", view.dom.txtHoras, validarCaracter);
        view.attachEvents("focusout", view.dom.txtHoras, formatearHoras);
        view.attachEvents("keypress", view.dom.txtTotEst, validarCaracter);
        view.attachEvents("focusout", view.dom.txtTotEst, formatearHoras);
        view.init();
    }

    calculoDeFechas = function (e) {
        var objUltImputac = cadenaAFecha(strAuxUltimoDia);
        
        view.darValor(view.dom.txtUltRep, strAuxUltimoDia);
        view.darValor(view.dom.txtFFin, '');

        //alert("strUltImputac: "+ strUltImputac);
        objDiaSigUDR = new Date();
        objDiaAntUDR = cadenaAFecha(strAuxUltimoDia).add("d", -1);

        var bDiaValido = false;
        var diaSigUltImputacion = cadenaAFecha(strAuxUltimoDia).add("d", 1);
        var primerDiaMesAbierto = moment(new Date(ultMesCerrado.substring(0, 4), eval(ultMesCerrado.substring(4, 6) - 1), 1)).add(1, 'months').toDate();       

        //Comprobamos si el día siguiente a la última imputación pertenece a un mes cerrado para empezar a buscar por el primer día imputable de un mes abierto
        (diaSigUltImputacion >= primerDiaMesAbierto) ? primerDia = diaSigUltImputacion : primerDia = primerDiaMesAbierto;

        //Comprobamos que el primer dia a validar contra el array de festivos no sea anterior a la fecha de alta del usuario
        if (cadenaAFecha(fechaAlta) > primerDia) primerDia = cadenaAFecha(fechaAlta);

        
        $.when(getFestivosRango(primerDia, moment(primerDia).add(15, "days").toDate())
            .then(function (lstFestivos) {
                aFestivos = lstFestivos;
                while (bDiaValido == false) {
                    var bFes = false;
                    for (var indice = 0; indice < aFestivos.length; indice++) {
                        if (primerDia.getTime() == new Date(moment(aFestivos[indice].t067_dia)).getTime()) {
                            bFes = true;
                            break;
                        }                        
                    }
                    if (!bFes) {
                        bDiaValido = true;
                    } else {
                        primerDia = moment(primerDia).add(1, "days").toDate();
                    }
                }

                asignarValorFIni();
            })     
         );
    }

    function getFechaUltImputacion() {

        var defer = $.Deferred();


        IB.DAL.post(null, "getFechaUltImputacion", null, null,
            function (data) {
                defer.resolve(data);
            },
            function (ex, status) {
                IB.bserror.error$ajax(ex, status);
                defer.resolve();
            }
        );

        return defer.promise();
    }

    function getFestivosRango(fecha_ini, fecha_fin) {

        var defer = $.Deferred();

        
        var payload = { fecha_ini: fecha_ini, fecha_fin: fecha_fin};
        IB.DAL.post(null, "getFestivosRango", payload, null,
            function (data) {
                defer.resolve(data);
            },
            function (ex, status) {		
                IB.bserror.error$ajax(ex, status);		                
                defer.resolve();
            }
        );
       
        return defer.promise();
    }

    //Validación de inputación manual en datepickers    
    $(document).on('focus', '.txtFecha:not(.hasDatepicker)', function (e) {
        $(this).datepicker({
            changeMonth: true,
            changeYear: true,
            //defaultDate: $('#L').attr('data-date'),

            beforeShow: function (input, inst) {
                $(this).removeClass('calendar-off').addClass('calendar-on');
            },
            onClose: function () {
                $(this).removeClass('calendar-on').addClass('calendar-off');
                if (moment($(this).val(), 'DD/MM/YYYY', 'es', true).isValid()) {
                    $(this).change();
                }
            }
        });
        $(this).change(function () {
            var input = $(this);
            var currentDatePickerId = e.currentTarget.id;
            var strFechaIni;
            var strFechaFin;

            if (currentDatePickerId == "txtFIni") {
                strFechaIni = $(this).val();
                strFechaFin = view.obtenerValor(view.dom.txtFFin);
            } else if (currentDatePickerId == "txtFFin") {
                strFechaIni = view.obtenerValor(view.dom.txtFIni);
                strFechaFin = $(this).val();
            }

            var result = validarFechas(strFechaIni, strFechaFin);

            if (result == "") {

                $(this).attr('value', $(this).val());
                if (view.dom.tarea.val() != "") {

                    if (strFechaIni == "" || strFechaFin == "") {
                        view.darValor(view.dom.tarea, "");
                        view.limpiarDatosTarea();
                       

                    } else {


                        var objFechaIniImp = new Date(moment(view.obtenerValor(view.dom.fechaInicioImpPermitida)));
                        var objFechaFinImp = new Date(moment(view.obtenerValor(view.dom.fechaFinImpPermitida)));
                        var aFecha1 = strFechaIni.split("/");
                        var aFecha2 = strFechaFin.split("/");
                        var objFechaIni = new Date(aFecha1[2], (aFecha1[1] - 1), aFecha1[0]);
                        var objFechaFin = new Date(aFecha2[2], (aFecha2[1] - 1), aFecha2[0]);

                        if (objFechaIni < objFechaIniImp || objFechaFin > objFechaFinImp) {
                            $.when(IB.bsconfirm.confirm("primary", "", "Atención, las fechas inicio y fin de imputación no cumplen con la vigencia de la tarea seleccionada. ¿Quieres continuar?</br></br>")).then(function () {
                                view.darValor(view.dom.tarea, "");
                                view.limpiarDatosTarea();
                            }, function () {
                                input.val(input.attr('value'));
                                input.focus();
                            });                           
                        }
                    }

                }
            } else {
                $.when(IB.bsalert.fixedAlert("warning", "Error de validación", result)).then(function () {
                    input.val(input.attr('value'));
                    input.focus();
                });              
                
                
            }
   
        });

        $(this).focusout(function () {
            var input = $(this);
            window.setTimeout(function () {
                if ((!moment(input.val(), 'DD/MM/YYYY', 'es', true).isValid()) && (input.val() != '')) {
                    IB.bsalert.toastdanger("Formato de fecha incorrecto: " + input.val());
                    input.val('');
                    input.focus();
                }

            }, 100);
            
        });
    });


            

    asignarValorFIni = function () {
        strDiaSigUDR = moment(primerDia).format("DD/MM/YYYY");
        view.darValor(view.dom.txtFIni, strDiaSigUDR);
        view.darValor(view.dom.txtFIniH, strDiaSigUDR);
    }

    

    obtenerDatosTarea = function (e){
        if(view.obtenerValor(view.dom.tarea) != ""){

            var fechaInicio = (view.obtenerValor(view.dom.txtFIni) == "") ? null : view.obtenerValor(view.dom.txtFIni).toDate();
            var fechaFin = (view.obtenerValor(view.dom.txtFFin) == "") ? null : view.obtenerValor(view.dom.txtFFin).toDate()
            var idTarea = view.obtenerValor(view.dom.tarea);
            var pos = idTarea.indexOf('.');
            if (pos != -1) idTarea = idTarea.substring(0, pos) + idTarea.substring(pos + 1, idTarea.length);
            var filtro = { idTarea: idTarea, fechaInicio: fechaInicio, fechaFin: fechaFin };
            IB.procesando.mostrar();
            IB.DAL.post(null, "obtenerDetalleTarea", filtro, null,
                function (data) {
                    if (data != null) {
                        objTarea = data;
                        view.pintarDatosTarea(objTarea);
                    } else {
                        $.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", "No puede imputar en esta tarea (" + view.obtenerValor(view.dom.tarea) + ")."))
                        .then(function () { view.asignarFoco(view.dom.tarea) });
                    }
                    IB.procesando.ocultar();
                },
                function (ex, status) {
                    $.when(IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error al obtener la tarea (" + view.obtenerValor(view.dom.tarea) + ")."))
                    .then(function () { view.asignarFoco(view.dom.tarea) });
                    IB.procesando.ocultar();
                }
            );
        }
    }

    buscarTarea = function (e) {
        if (!validarFechasAntesDeBuscarTarea(view.obtenerValor(view.dom.txtFIni), view.obtenerValor(view.dom.txtFFin))) return false;

        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();
        var o = {
            tipoBusqueda: "tareasEnBloque",
            fechaInicio: (view.obtenerValor(view.dom.txtFIni) == "") ? null : view.obtenerValor(view.dom.txtFIni).toDate(),
            fechaFin: (view.obtenerValor(view.dom.txtFFin) == "") ? null : view.obtenerValor(view.dom.txtFFin).toDate()
        }
        $(".fk_ayudaTarea").buscatarea("option", "searchParams", o);
        $(".fk_ayudaTarea").buscatarea("show");

    }   

    ejecutarAccionTecleo = function (e) {
        if (e.which == 13) {
            if (!validarFechasAntesDeBuscarTarea(view.obtenerValor(view.dom.txtFIni), view.obtenerValor(view.dom.txtFFin))) return false;
            obtenerDatosTarea(e);
        } else if (e.which != 13 && e.which != 37 && e.which != 38 && e.which != 39 && e.which != 40) {
            if (validarTeclaNumerica(e, false)) {
                view.limpiarDatosTarea();
            } else return false;
        }
    }

    buscarTareaPorId = function (e) {
        if (view.obtenerValor(view.dom.tarea) == "") view.limpiarDatosTarea();
        else {
            if (!validarFechasAntesDeBuscarTarea(view.obtenerValor(view.dom.txtFIni), view.obtenerValor(view.dom.txtFFin))) return false;
            obtenerDatosTarea(e);
        }
    }

    validarFechasAntesDeBuscarTarea = function (strFechaIni, strFechaFin) {
        if (strFechaIni == "" || strFechaFin == "") {
            IB.bsalert.toastdanger("Debes indicar las fechas inicio y fin de imputación para poder realizar la búsqueda de tareas");
            return false;
        }
        return true;
    }

    grabarDatos = function (e) {

        if (!validarDatos()) return false;

        var filtro = {
            idTarea: view.obtenerValor(view.dom.tarea), tipoImp: view.obtenerValor($('input:radio[id^="radio"]:checked')),
            ultDiaReport: view.obtenerValor(view.dom.txtUltRep),
            fechaDesde: view.obtenerValor(view.dom.txtFIni), fechaHasta: view.obtenerValor(view.dom.txtFFin),
            cmbModo: view.obtenerValor(view.dom.cmbModo),
            festivos: view.radioCheckeado(view.dom.chkFestivos) ? 1 : 0, finalizado: view.radioCheckeado(view.dom.chkFinalizado) ? 1 : 0,
            horas: (view.obtenerValor(view.dom.txtHoras) == "") ? 0 : view.obtenerValor(view.dom.txtHoras),
            obsImputacion: view.obtenerValor(view.dom.txtComent),
            obsTecnico: view.obtenerValor(view.dom.txtObsv),
            totalEst: (view.obtenerValor(view.dom.txtTotEst) == "") ? 0 : view.obtenerValor(view.dom.txtTotEst),
            fechaFinEst: view.obtenerValor(view.dom.txtFEst),
            obligaEst: objTarea.t331_obligaest, PSN: objTarea.t305_idproyectosubnodo
        };

        IB.procesando.mostrar();
        IB.DAL.post(null, "grabarImputacionMasiva", filtro, null,
            function (data) {
                IB.procesando.ocultar();
                $.when(getFechaUltImputacion())
                    .then(function (data) {
                        debugger;
                        strAuxUltimoDia = data;
                        calculoDeFechas();
                    });
                obtenerDatosTarea();
                IB.bsalert.toast(data);
            }
           
        );

    }

    validarCaracter = function (e) {
        if (!validarTeclaNumerica(e, true)) return false;
    }

    formatearHoras = function (e) {
        var horas = accounting.format(accounting.unformat(view.obtenerValor($(this)), ","));
        view.darValor($(this), horas);
    }

    validarFechas = function (strFecha1, strFecha2) {

        var msgError = "";
        var aFecha1 = strFecha1.split("/");
        var aFecha2 = strFecha2.split("/");
        var objFecha1 = new Date(aFecha1[2], (aFecha1[1] - 1), aFecha1[0]);
        var objFecha2 = new Date(aFecha2[2], (aFecha2[1] - 1), aFecha2[0]);
        if (objFecha2 < objFecha1) {
            //IB.bsalert.toastdanger("La fecha final de la imputación no puede ser anterior a la fecha de inicio", "danger", 300, 4000);
            //view.asignarFoco(view.dom.txtFFin);
            msgError = "La fecha final de la imputación no puede ser anterior a la fecha de inicio";
            return msgError;
        }

        /* La fecha de final no puede ser posterior a los 2 meses del último cierre */
        var objUMC = new Date(IB.vars.ultMesCerrado.substring(0, 4), IB.vars.ultMesCerrado.substring(4, 6), 0);
        var intDiferencia = objFecha2.getTime() - objUMC.getTime();
        if (intDiferencia > 5356800000) { //62 días en milisegundos (1 día 86400000).
            //IB.bsalert.toastdanger("La fecha final de la imputación debe ser, como máximo, dos meses posterior al último cierre (" + moment(objUMC).format("DD/MM/YYYY") + ")", "danger", 300, 4000);
            //view.asignarFoco(view.dom.txtFFin);           
            msgError = "La fecha final de la imputación debe ser, como máximo, dos meses posterior al último cierre (" + moment(objUMC).format("DD/MM/YYYY") + ")";
            return msgError;
        }

        /* La fecha de inicio debe ser posterior al último mes cierre */
        if (objFecha1.getTime() <= objUMC.getTime()) {
            //IB.bsalert.toast("La fecha de inicio de la imputación debe ser posterior al último mes cerrado (" + moment(objUMC).format("DD/MM/YYYY") + ")", "danger", 300, 4000);
            //view.asignarFoco(view.dom.txtFIni);            
            msgError = "La fecha de inicio de la imputación debe ser posterior al último mes cerrado (" + moment(objUMC).format("DD/MM/YYYY") + ")";
            return msgError;
        }

        /* Si se opta por la opción 2 o 3, la fecha de inicio debe ser:
               - Anterior al UDR:
               - Igual al UDR
               - Día siguiente laborable al UDR */
        if ((view.radioCheckeado(view.dom.radio2) || view.radioCheckeado(view.dom.radio3)) && (strAuxUltimoDia != "") && (bControlhuecos)) {
            if (objFecha1.getTime() > strDiaSigUDR.toDate().getTime()) {
                //IB.bsalert.toast("La fecha de inicio de imputación debe ser menor o igual al siguiente laborable al último día reportado", "danger", 300, 4000);
                //view.asignarFoco(view.dom.txtFIni);                
                msgError = "La fecha de inicio de imputación debe ser menor o igual al siguiente laborable al último día reportado";
                return msgError;
            }
        }

        return msgError;
    }

    //Validación de campos antes de grabar
    validarDatos = function (e) {
        try{
            if (view.dom.tarea.val() == "") {
                IB.bsalert.toastdanger("Debes seleccionar la tarea");
                view.asignarFoco(view.dom.tarea);
                return false;
            }

            if (view.dom.txtFIni.val() == "") {
                IB.bsalert.toastdanger("Debes introducir la fecha de inicio de la imputación");
                view.asignarFoco(view.dom.txtFIni);
                return false;
            }
            if (view.dom.txtFFin.val() == "") {
                IB.bsalert.toastdanger("Debes introducir la fecha final de la imputación");
                view.asignarFoco(view.dom.txtFFin);
                return false;
            }
            
            // Creo que esta validación sobra, ya que a la hora de seleccionar o modificar fechas también se hace
            var result = validarFechas(view.dom.txtFIni.val(), view.dom.txtFFin.val());
            if (result != "") {
                IB.bsalert.toastdanger(result, "danger", 300, 4000);
                return false;
            }          
           

            /* Si se opta por la opción 3, que se introduzcan horas (no superior a 24h) */
             if (view.radioCheckeado(view.dom.radio3)) {
                 if (view.dom.txtHoras.val() == "") {
                     IB.bsalert.toastdanger("Introduce las horas a imputar");
                     view.asignarFoco(view.dom.txtHoras);
                     return false;
                 }
                 /* Horas */
                 if (parseFloat(dfn(view.dom.txtHoras.val())) <= 0) {
                     IB.bsalert.toastdanger("El número de horas a imputar debe ser superior a 0 horas");
                     view.asignarFoco(view.dom.txtHoras);
                     return false;
                 }
             
                 if (parseFloat(dfn(view.dom.txtHoras.val())) > 24) {
                     IB.bsalert.toastdanger("El número de horas a imputar debe ser inferior a 24 horas");
                     view.asignarFoco(view.dom.txtHoras);
                     return false;
                 }

                 // Validaciones de proyecto
                 if (view.dom.ImputarFestivos.val() != "1" && view.radioCheckeado(view.dom.chkFestivos)) {
                     //IB.bsalert.toast("El proyecto económico seleccionado no permite imputar en festivos.", "warning");
                     $.when(IB.bsalert.fixedAlert("warning", "Error de validación", "El proyecto económico seleccionado no permite imputar en festivos."))
                     .then(function () { view.asignarFoco(view.dom.chkFestivos) });
                     return false;
                 }
                 //Si el proyecto no permite imputar a jornada no completa y el modo es dos, return false
                 if (view.dom.RegJorNoCompleta.val() != "1") {
                     //IB.bsalert.toast("El proyecto económico seleccionado obliga a imputar jornadas completas, por lo que hay que imputar con las opciones de horas estándar.", "warning", 300, 4000);
                     $.when(IB.bsalert.fixedAlert("warning", "Error de validación", "El proyecto económico seleccionado obliga a imputar jornadas completas, por lo que hay que imputar con las opciones de horas estándar."))
                     .then(function () { view.asignarFoco(view.dom.txtHoras) });
                     return false;
                 }
             }
            // Respecto a la tarea
            
             if (objTarea.t331_obligaest) {
                 if ((view.dom.txtTotEst.val() == "") || (view.dom.txtTotEst.val() == "0")) {
                     IB.bsalert.toastdanger("Introduce el esfuerzo total estimado.");
                     view.asignarFoco(view.dom.txtTotEst);
                     return false;
                 }
                 if (view.dom.txtFEst.val() == "") {
                     IB.bsalert.toastdanger("Introduce la fecha de finalización estimada.");
                     view.asignarFoco(view.dom.txtFEst);
                     return false;
                 }
             }

             return true;

        } catch (e) {
           IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Error al realizar las validaciones previas a grabar<br/><br/>" + e.message);

        }
    }

    function cadenaAFecha(cadena) {
        var aCadena = cadena.split("/");
        var objFecha = new Date(aCadena[2], (aCadena[1] - 1), aCadena[0]);
        return objFecha;
    }

    Date.prototype.add = function (sInterval, iNum) {
        var dTemp = this;
        if (!sInterval || iNum == 0) return dTemp;
        switch (sInterval.toLowerCase()) {
            case "ms": dTemp.setMilliseconds(dTemp.getMilliseconds() + iNum); break;
            case "s": dTemp.setSeconds(dTemp.getSeconds() + iNum); break;
            case "mi": dTemp.setMinutes(dTemp.getMinutes() + iNum); break;
            case "h": dTemp.setHours(dTemp.getHours() + iNum); break;
            case "d": dTemp.setDate(dTemp.getDate() + iNum); break;
            case "mo": dTemp.setMonth(dTemp.getMonth() + iNum); break;
            case "y": dTemp.setFullYear(dTemp.getFullYear() + iNum); break;
        }
        return dTemp;
    }

    /***********************************************
    Función: dfn (DesFormatear Numero)
    Inputs: sValor --> Número formateado a desformatear
    ************************************************/
    function dfn(sValor) {
        if (isNaN(sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), "."))
            || sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".") == ""
            ) return "0";
        else return sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".");

        //    return (isNaN(sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")))?"0":sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".");
    }



    return {
        init: init
    }

})(SUPER.IAP30.ImpMasiva.view);

