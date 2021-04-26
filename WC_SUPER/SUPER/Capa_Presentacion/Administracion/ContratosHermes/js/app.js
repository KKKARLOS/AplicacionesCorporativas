$(document).ready(function () { SUPER.Administracion.ContratosHermes.app.init(); })

var SUPER = SUPER || {};
SUPER.Administracion = SUPER.Administracion || {};
SUPER.Administracion.ContratosHermes = SUPER.Administracion.ContratosHermes || {}

SUPER.Administracion.ContratosHermes.app = (function (view) {

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

        //Obtención de datos en la pantalla principal
        view.attachEvents("click keypress", view.dom.btnObtener, obtenerFiltros);
        //Salir de la pantalla 
        view.attachEvents("click", view.dom.btnRegresar, salir);
        //Generar contratos a partir de las oportunidades seleccionadas
        view.attachEvents("click", view.dom.btnGrabar, generarContratos);

        //Control de checkboxes
        //view.attachEvents("click keypress", view.selectores.sel_chk, controlChk);

        //Apertura de buscador de CRs
        view.attachEvents("click keypress", view.dom.lblCR, buscarCR);
        //Limpia CR
        view.attachEvents("click keypress", view.dom.btnEliminarCR, limpiarCR);
        //Apertura buscador profesioanles
        view.attachEvents("click", view.dom.icoProfesional, abrirBuscadorProfesional);


        //Plugin buscador genérico (en este caso para CRs)
        var options = {
            titulo: "",
            modulo: "IAP30",
            autoSearch: true,
            autoShow: false,
            searchParams: {
            },
            onSeleccionar: function (data) {
                view.volcarTxtValue(data.target, data.desElemento, data.idElemento);
                //if (data.target.selector == "#txtClienteContratos") {
                //    view.vaciarTablaContratos();
                //} else {
                //    //vaciar tabla proyectos
                //}
            },
            onCancelar: function () {
                console.log("cancelar");
            }
        };
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico(options);

        IB.procesando.ocultar();
    }

    //Funcionalidad pantalla principal

    var obtenerFiltros = function () {
        var nodo = view.dom.txtCR.attr('value');
        if (nodo=="") {
            IB.bsalert.toastdanger("Debes seleccionar un Centro de Responsabilidad");
            return false;
        }

        IB.procesando.mostrar();

        var nodo = view.dom.txtCR.attr('value');
        var desde = view.dom.txtRango.data('daterangepicker').startDate;
        var hasta = view.dom.txtRango.data('daterangepicker').endDate;

        obtenerOportunidades(nodo, desde, hasta);

    }

    //Fin funcionalidad pantalla principal    

    /*Llamadas de carga a webmethods*/
    //Llamadas pantalla principal

    var buscarCR = function (e) {
        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();
        var o = {
            titulo: "Selección de CR",
            target: view.dom.txtCR,
            tipoBusqueda: "obtenerCRsNoHermes",//obtenerCRsAdmin
            nivelOcultable: 2
        }
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("option", "searchParams", o);
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("show");

    }

    var limpiarCR = function () {
        if (view.dom.txtCR.attr('value') != "") view.limpiarFiltro(view.dom.txtCR);
    }

    //var limpiarCatalogo = function () {
    //    view.dom.vaciarTabla();
    //}

    //Obtiene los partes de actividad en base a los filtros
    var obtenerOportunidades = function (nodo, fDesde, fHasta) {

        var defer = $.Deferred();
        var payload = { idNodo: nodo, dDesde: fDesde, dHasta: fHasta };
        IB.DAL.post(null, "obtenerOportunidades", payload, null,
            function (data) {
                view.pintarTablaOportunidades(data);
                $.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
            },null,30000
        );
        return defer.promise();
    }

    var comprobarDatosGrabar = function () {
        var sError = "";
        var nError = 0;

        IB.procesando.mostrar();

        if (!view.dom.tabla.DataTable().rows().count()) {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("No hay oportunidades para generar contratos");
            return false;
        }

        if (!view.dom.tabla.DataTable().rows('.selected').count()) {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("No hay filas seleccionadas");
            return false;
        }

        if (view.dom.tabla.DataTable().rows('.selected').count() > 100) {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("El volumen de oportunidades seleccionadas excede el máximo permitido. \n\n Reduzca el número de oportunidades marcadas para la generación de contratos.");
            return false;
        }

        IB.procesando.ocultar();
        return true;
    }

    generarContratos = function (e) {
        if (comprobarDatosGrabar()) {

            var ListaOportunidades = [];
            var idNodo = view.dom.txtCR.attr('value');
            //dom.tabla.DataTable().rows('.selected').every(function () {
            //    sb.append(this.data().t314_idusuario + '/' + this.data().t332_idtarea + '/' + moment(this.data().t337_fecha).format("YYYYMMDD") + ',');
            //});

            $.each($(view.selectores.filasSeleccionadas), function () {
                //if ($(this).attr("data-bd") != "") {//Solo se actualizan filas modificadas // no porque sino luego no aparecen en la relación de notificación de correo
                oON = new Object();
                oON.t306_icontrato = $(this).attr("id");
                oON.t377_idextension = $(this).children().eq(2).text().trim();
                oON.t377_denominacion = $(this).children().eq(3).text().trim();

                oON.t303_idnodo = idNodo;
                oON.ta212_idorganizacioncomercial = $(this).attr("idOC");
                oON.t302_idcliente_contrato = $(this).attr("idCli");
                oON.t314_idusuario_comercialhermes = $(this).attr("idCo");
                oON.t314_idusuario_gestorprod = $(this).attr("idGe");
                oON.t314_idusuario_responsable = IB.vars.codResponsable;
                oON.duracion = $(this).attr("dur"); 
                oON.tipocontrato = $(this).attr("tip");
                oON.codred_gestor_produccion = $(this).attr("codredGe");

                oON.t422_idmoneda = $(this).attr("mon");
                //oON.t377_fechacontratacion = $(this).children().eq(8).text().trim();
                oON.t377_fechacontratacion = moment($(this).children().eq(8).text().trim(), 'DD/MM/YYYY');

                oON.t377_importeser = $(this).children().eq(9).text().trim().replace('.', '').replace('.', '').replace(',', '.');
                oON.t377_marpreser = $(this).children().eq(10).text().trim().replace('.', '').replace('.', '').replace(',', '.');
                oON.t377_importepro = $(this).children().eq(11).text().trim().replace('.', '').replace('.', '').replace(',', '.');                
                oON.t377_marprepro = $(this).children().eq(12).text().trim().replace('.', '').replace('.', '').replace(',', '.');

                //oON.T382_idasunto = parseInt(IB.vars.idAsunto);
                //oON.T388_notificar = ($(this).children().eq(1).children().eq(0).is(':checked') == true) ? true : false;
                //oON.mail = $(this).attr("data-mail");
                //oON.t001_sexo = $(this).attr("data-sexo");
                //oON.t303_idnodo = parseInt(0);
                //oON.baja = $(this).attr("data-baja");
                //oON.tipo = $(this).attr("data-tipo");
                //oON.accionBD = $(this).attr("data-bd");

                //Nueva Línea de Oferta
                oON.t195_idlineaoferta = $(this).attr("idNLO");

                ListaOportunidades.push(oON);
                //}
            });

            var payload = {Oportunidades: ListaOportunidades };

            IB.procesando.mostrar();
            IB.DAL.post(null, "generar", payload, null,
                function (data) {
                    $.each($(view.selectores.filasSeleccionadas), function (e) {
                        //if ($(this).attr("data-bd") != "") {
                        //    $(this).closest('tr').remove();
                        //}
                        $(this).remove();
                    });
                    view.vaciarTablaProyectos();
                    view.mostrarModalProyecto(data);
                    IB.procesando.ocultar();
                    IB.bsalert.toast("Generación correcta.", "success");
                },
                null,30000
            );
        }
    }

    abrirBuscadorProfesional = function (e) {
        view.dom.divBuscadorPersonas.buscadorPersonas({//SUP_PROF_VISIBLES_ADM
            modal: true, titulo: "Selección de profesional", tipoBusqueda: 'USUARIOS',
            aceptar: function (data) {
                view.dom.spProfesional.html(data.PROFESIONAL);
                IB.vars.codResponsable = data.t314_idusuario;
            },
            cancelar: function () {
            }
        });
    }

    var salir = function (e) {
        //$.when(controlarSalir(e)).then(function () {
        //    setTimeout(function () { location.href = "../../BitacoraPE/Default.aspx?" + IB.vars.qs; }, 500);
        //});
        location.href = "../Contratos/Catalogo/Default.aspx";
    }
    //Fin llamadas pantalla principal

    return {
        init: init
    }

})(SUPER.Administracion.ContratosHermes.View);