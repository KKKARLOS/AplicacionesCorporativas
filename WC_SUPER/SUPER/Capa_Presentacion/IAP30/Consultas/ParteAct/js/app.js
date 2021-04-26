$(document).ready(function () { SUPER.IAP30.ParteAct.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.ParteAct = SUPER.IAP30.ParteAct || {}

SUPER.IAP30.ParteAct.app = (function (view) {

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
        view.attachEvents("click keypress", view.dom.btnObtener, obtenerFiltrosPartes);        

        //Control de checkboxes
        view.attachEvents("click keypress", view.selectores.sel_chk, controlChk);
        

        //Apertura de modal de buscador de proyectos
        view.attachEvents("click keypress", view.dom.btnProyecto, abrirModalProyecto);

        //Borrado de proyectos en los criterios de la pantalla principal
        view.attachEvents("click keypress", view.dom.btnEliminarProyectos, eliminarProyectos);

        //Apertura de modal de buscador de clientes de la pantalla principal
        view.attachEvents("click keypress", view.dom.btnCliente, abrirModalClienteMulti);

        //Borrado de clientes en los criterios de la pantalla principal
        view.attachEvents("click keypress", view.dom.btnEliminarClientes, eliminarClientes);
        
        IB.procesando.ocultar();

        // Ayuda Clientes multi

        
        $(view.selectores.ayudaClienteMulti).buscaclientemulti({
            titulo: "Selección de clientes",
            tituloContIzda: "Clientes",
            tituloContDcha: "Clientes seleccionados",
            notFound: "No se han encontrado resultados.",
            modulo: "IAP30",
            tipoAyuda: "obtenerClientes",
            autoSearch: false,
            autoShow: false,
            eliminarExistentes: false,
            placeholderText: "Buscar cliente por denominación",
            filtro: "",
            onAceptar: function (data) {
                view.volcarClientesSeleccionados(data);
            }
        });




        //Ayuda Clientes
        var optionsClientes = {
            titulo: "Selección de cliente",
            modulo: "IAP30",
            autoSearch: false,
            autoShow: false,
            searchParams: {
            },
            onSeleccionar: function (data) {
                view.volcarTxtValue(data.target, data.desCliente, data.idCliente);
                if (data.target.selector == "#txtClienteContratos") {
                    view.vaciarTablaContratos();
                } else {
                    //vaciar tabla proyectos
                }
            },
            onCancelar: function () {
                console.log("cancelar");
            }
        };

        $(view.selectores.ayudaCliente).buscacliente(optionsClientes);

        //Ayuda Horizontal y cualificadores
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
         

    }

    //Funcionalidad pantalla principal

    //No se deja que estén los dos checkboxes sin checkear
    var controlChk = function (event) {

        var srcobj = event.target ? event.target : event.srcElement;

        var chkHermano = $(view.selectores.sel_chk).not(srcobj);

        if (!chkHermano.is(":checked")) chkHermano.prop('checked', true);

    }

    //Elimina los proyectos de los criterios de búsqueda
    var eliminarProyectos = function () {
        if (view.dom.listaProyectos.children().length > 0) {
            view.dom.listaProyectos.empty().change();
            view.dom.listaProyectos.removeClass('fk_lista').addClass('fk_lista');
        }        
    }

    //Elimina los clientes de los criterios de búsqueda
    var eliminarClientes = function () {
        if (view.dom.listaClientes.children().length > 0) {
            view.dom.listaClientes.empty().change();
            view.dom.listaClientes.removeClass('fk_lista').addClass('fk_lista');
        }
    }

    var obtenerFiltrosPartes = function () {

        var tipoTareas;
        var proyectos  = new StringBuilder();
        var clientes = new StringBuilder();

        IB.procesando.mostrar();

        //Si los dos están checkeados se envia null, si no true si son facturables o false si son no facturables
        if (view.dom.chkFacturables.is(":checked") && view.dom.chkNoFacturables.is(":checked")) {
            tipoTareas = null;
        } else if (view.dom.chkFacturables.is(":checked")) {
            tipoTareas = true;
        } else {
            tipoTareas = false;
        }

        //Se recorren las listas de proyectos y clientes para recoger sus identificadores
        $(view.selectores.sel_proyectos).each(function () {
            if (proyectos.strings.length != 1) proyectos.append(",");
            proyectos.append($(this).attr('data-t305_idproyectosubnodo'));
        });

        $(view.selectores.sel_clientes).each(function () {
            if (clientes.strings.length != 1) clientes.append(",");
            clientes.append($(this).attr('data-id'));
        });

        var desde = view.dom.txtRango.data('daterangepicker').startDate;
        var hasta = view.dom.txtRango.data('daterangepicker').endDate;

        obtenerPartes(clientes.toString(), proyectos.toString(), tipoTareas, desde, hasta);

    }    

    //Fin funcionalidad pantalla principal    

    //Funcionalidad modal proyectos

    //Atacha los eventos relativos al modal del buscador de proyectos y lo abre
    var abrirModalProyecto = function () {

        IB.procesando.mostrar();

        //Limpiar los filtros de la pantalla
        view.limpiarFiltrosProyectos();

        //Eliminar los links de cualificadores si estaban mostrandose por una entrada anterior al modal
        view.cambiarLinkCualif(false);

        //Cierre de modal de buscador de proyectos
        view.attachEvents("hidden.bs.modal", view.dom.modalProyecto, cerrarModalProyecto);

        //Aceptar de modal de buscador de proyectos
        view.attachEvents("click keypress", view.dom.btnAceptarProyecto, aceptarModalProyecto);

        //Obtención de datos en el modal de selección de proyectos
        view.attachEvents("click keypress", view.dom.btnObtenerProyectos, obtenerFiltrosProyectos);

        //Cambio en los campos de criterios
        view.attachLiveEvents("change", view.selectores.sel_filtros_proyectos, obtenerLimpiarProyectos);

        //Introducción en los campos de id y descripción de proyecto
        view.attachLiveEvents("keyup", view.selectores.sel_iddesc_proyectos, introProyectos);

        if (IB.vars.superEditor == "True") {

            //Apertura de modal de CR
            view.attachEvents("click keypress", view.dom.lblCRAdmin, abrirModalCR);

            //Eliminación de CR seleccionado
            view.attachEvents("click keypress", view.dom.btnEliminarCR, eliminarCR);            

        } else {

            //Apertura de modal de CR
            view.attachEvents("change", view.dom.txtCR, cambioCR);

        }                

        //Apertura de buscador de clientes
        view.attachEvents("click keypress", view.dom.lblClienteProyectos, buscarClienteProyectos);

        //Eliminación de cliente seleccionado
        view.attachEvents("click keypress", view.dom.btnEliminarClienteProyectos, eliminarClienteProyectos);

        //Apertura de modal de contrato
        view.attachEvents("click keypress", view.dom.lblContrato, abrirModalContrato);

        //Eliminación de cliente seleccionado
        view.attachEvents("click keypress", view.dom.btnEliminarContrato, eliminarContratoProyectos);

        //Apertura de buscador de responsable
        view.attachEvents("click keypress", view.dom.lblResp, abrirBuscadorResponsable);

        //Eliminación de responsable seleccionado
        view.attachEvents("click keypress", view.dom.btnEliminarResp, eliminarRespProyectos);

        //Apertura de buscador de horizontal
        view.attachEvents("click keypress", view.dom.lblHor, buscarHorizontales);

        //Eliminación de responsable seleccionado
        view.attachEvents("click keypress", view.dom.btnEliminarHor, eliminarHorProyectos);

        view.attachEventosListaDual();

        //Se visualiza el modal cuando se han cargado los combos
        $.when(obtenerModContra(), obtenerNodosCR()).then(function () {
            view.mostrarModalProyecto();
            IB.procesando.ocultar();
        });

    }

    //Desatacha los eventos relativos al modal del buscador de proyectos
    var cerrarModalProyecto = function () {

        view.deAttachLiveEvents("change", view.selectores.sel_filtros_proyectos, obtenerLimpiarProyectos);

        view.deAttachEvents(view.dom.modalProyecto);
        view.deAttachEvents(view.dom.btnAceptarProyecto);
        view.deAttachEvents(view.dom.btnObtenerProyectos);
        view.deAttachLiveEvents("change", view.selectores.sel_filtros_proyectos, obtenerLimpiarProyectos);
        view.deAttachLiveEvents("keyup", view.selectores.sel_iddesc_proyectos, introProyectos);
        if (IB.vars.superEditor == "True") {

            view.deAttachEvents(view.dom.lblCRAdmin);
            view.deAttachEvents(view.dom.btnEliminarCR);

        } else {

            view.deAttachEvents(view.dom.txtCR);

        }
        view.deAttachEvents(view.dom.lblClienteProyectos);
        view.deAttachEvents(view.dom.btnEliminarClienteProyectos);
        view.deAttachEvents(view.dom.lblContrato);
        view.deAttachEvents(view.dom.btnEliminarContrato);
        view.deAttachEvents(view.dom.lblResp);
        view.deAttachEvents(view.dom.btnEliminarResp);
        view.deAttachEvents(view.dom.lblHor);
        view.deAttachEvents(view.dom.btnEliminarHor);

        view.deAttachEventosListaDual();

        view.visualizarContenidoSR();

    }

    var obtenerLimpiarProyectos = function () {

        if (view.dom.checkAuto.is(":checked")) {
            obtenerFiltrosProyectos();
        }
        else {
            view.vaciarTablaProyectos();
        }

    }

    var introProyectos = function (e) {

        if (e.keyCode == 13) {
            obtenerFiltrosProyectos();
        } else {
            view.vaciarTablaProyectos();
        }        

    }

    var aceptarModalProyecto = function () {
        
        view.volcarProyectosSeleccionados();
    }    

    var obtenerFiltrosProyectos = function () {        

        var idCR = null;
        var idProyecto = null;
        var idResp = null;
        var idHoriz = null;
        var idCliente = null;
        var idContrato = null;
        var Qn = null;
        var Q1 = null;
        var Q2 = null;
        var Q3 = null;
        var Q4 = null;

        IB.procesando.mostrar();

        if (IB.vars.superEditor == "False") {
            if (view.dom.txtCR.val() != "") idCR = view.dom.txtCR.val();
        } else {
            if (view.dom.txtCR.attr('value') != "") idCR = view.dom.txtCR.attr('value');
        }
        
        if (view.dom.txtIdProyecto.val() != "") idProyecto = view.dom.txtIdProyecto.val();

        if (view.dom.txtResponsable.attr('value') != "") idResp = view.dom.txtResponsable.attr('value');

        if (view.dom.txtHorizontal.attr('value') != "") idHoriz = view.dom.txtHorizontal.attr('value');

        if (view.dom.txtClienteProyectos.attr('value') != "") idCliente = view.dom.txtClienteProyectos.attr('value');

        if (view.dom.txtIdContrato.val() != "") idContrato = view.dom.txtIdContrato.val();

        if (view.dom.txtQn.attr('value') != "") Qn = view.dom.txtQn.attr('value');
        if (view.dom.txtQ1.attr('value') != "") Q1 = view.dom.txtQ1.attr('value');
        if (view.dom.txtQ2.attr('value') != "") Q2 = view.dom.txtQ2.attr('value');
        if (view.dom.txtQ3.attr('value') != "") Q3 = view.dom.txtQ3.attr('value');
        if (view.dom.txtQ4.attr('value') != "") Q4 = view.dom.txtQ4.attr('value');


        //Negar la búsqueda cuando no haya filtros
        if (!idCR && !idProyecto && !idResp && !idHoriz && !idCliente && !idContrato && !Qn && !Q1 && !Q2 && !Q3 && !Q4 && view.dom.txtDesProyecto.val() == "") {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("Debes seleccionar más criterios");
            return false;
        }

        obtenerProyectos(idCR, view.dom.selEstado.val(), view.dom.selCategoria.val(), idCliente, idResp,
                        idProyecto, view.dom.txtDesProyecto.val(), view.dom.selBusquedaProyectos.val(), view.dom.selCualidad.val(), idContrato,
                        idHoriz, Qn, Q1, Q2, Q3, Q4, view.dom.selModCon.val());
        

    }

    var eliminarCR = function () {

        if (view.dom.txtCR.attr('value') != "") view.limpiarFiltro(view.dom.txtCR);
        view.cambiarLinkCualif(false);
    }

    var cambioCR = function () {

        if (view.dom.txtCR.val() == "") {

            view.cambiarLinkCualif(false);

        } else {

            view.cambiarLinkCualif(true);

        }
    }

    var eliminarClienteProyectos = function () {

        if (view.dom.txtClienteProyectos.attr('value') != "") view.limpiarFiltro(view.dom.txtClienteProyectos);

    }

    var eliminarContratoProyectos = function () {

        if (view.dom.txtIdContrato.val() != "" || view.dom.txtDesContrato.val() != "") {
            view.limpiarFiltro(view.dom.txtIdContrato);
            view.limpiarFiltro(view.dom.txtDesContrato);
        }

    }

    var eliminarRespProyectos = function () {

        if (view.dom.txtResponsable.attr('value') != "") view.limpiarFiltro(view.dom.txtResponsable);

    }

    var eliminarHorProyectos = function () {

        if (view.dom.txtHorizontal.attr('value') != "") view.limpiarFiltro(view.dom.txtHorizontal);

    }

    eliminarQnProyectos = function () {

        if (view.dom.txtQn.attr('value') != "") view.limpiarFiltro(view.dom.txtQn);

    }

    eliminarQ1Proyectos = function () {

        if (view.dom.txtQ1.attr('value') != "") view.limpiarFiltro(view.dom.txtQ1);

    }

    eliminarQ2Proyectos = function () {

        if (view.dom.txtQ2.attr('value') != "") view.limpiarFiltro(view.dom.txtQ2);

    }

    eliminarQ3Proyectos = function () {

        if (view.dom.txtQ3.attr('value') != "") view.limpiarFiltro(view.dom.txtQ3);

    }

    eliminarQ4Proyectos = function () {

        if (view.dom.txtQ4.attr('value') != "") view.limpiarFiltro(view.dom.txtQ4);

    }

    //Fin funcionalidad modal proyectos


    //Funcionalidad modal CR

    //Atacha los eventos relativos al modal del buscador de proyectos y lo abre
    var abrirModalCR = function () {

        IB.procesando.mostrar();

        //Cierre de modal de CR
        view.attachEvents("hidden.bs.modal", view.dom.modalCR, cerrarModalCR);

        view.attachEventosModalCR();

        obtenerNodosCRAdmin();

    }

    //Destacha los eventos relativos al modal del buscador de proyectos y lo cierra
    var cerrarModalCR = function () {

        view.deAttachEvents(view.dom.modalCR);

        view.deAttachEventosModalCR();

        view.visualizarContenidoSR2();

    }  

    //Fin funcionalidad modal CR

    //Funcionalidad buscador de clientes en proyectos

    var buscarClienteProyectos = function (e) {
        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();
        var o = {
            target: view.dom.txtClienteProyectos,
            tipoBusqueda: "obtenerClientes",
            nivelOcultable: 2,
            bSoloActivos: false,
            bInternos: false
        }
        $(view.selectores.ayudaCliente).buscacliente("option", "searchParams", o);
        $(view.selectores.ayudaCliente).buscacliente("show");

    }    

    //Fin funcionalidad buscador de clientes

    //Funcionalidad modal contratos

    var abrirModalContrato = function () {

        IB.procesando.mostrar();

        //Cierre de modal de contrato
        view.attachEvents("hidden.bs.modal", view.dom.modalContrato, cerrarModalContrato);

        //Con enter sobre el input de contrato se busca la denominación del contrato introducido y se lanza la búsqueda de contratos
        view.attachEvents("keypress", view.dom.idContratoM, obtenerDenominacionContrato);

        //Con enter sobre el input de descripción de contrato se lanza la búsqueda de contratos
        view.attachEvents("keypress", view.dom.desContratoM, obtenerContratosPorDenominacion);

        //Modificación de check de mostrar todos
        view.attachEvents("click keypress", view.dom.chkMostrarTodosContratos, obtenerFiltrosContratos);

        //Apertura de buscador de clientes
        view.attachEvents("click keypress", view.dom.lblClienteContratos, buscarClienteContratos);

        //Eliminación de cliente seleccionado
        view.attachEvents("click keypress", view.dom.btnEliminarClienteContratos, eliminarClienteContratos);

        //botón obtener contratos
        view.attachEvents("click keypress", view.dom.btnObtenerContratoProyectos, obtenerFiltrosContratos);

        view.attachEventosModalContrato();

        view.mostrarModalContrato();

    }

    var cerrarModalContrato = function () {

        view.deAttachEvents(view.dom.modalContrato);
        view.deAttachEvents(view.dom.idContratoM);
        view.deAttachEvents(view.dom.desContratoM);
        view.deAttachEvents(view.dom.chkMostrarTodosContratos);
        view.deAttachEvents(view.dom.lblClienteContratos);
        view.deAttachEvents(view.dom.btnEliminarClienteContratos);
        view.deAttachEvents(view.dom.btnObtenerContratoProyectos);

        view.deAttachEventosModalContrato();

        view.visualizarContenidoSR2();

    }

    var obtenerContratosPorDenominacion = function (e) {

        view.vaciarFiltrosContratos("desc");
        view.vaciarTablaContratos("desc");
        if (e.keyCode == 13) obtenerFiltrosContratos();

    }

    var eliminarClienteContratos = function () {

        if (view.dom.txtClienteContratos.attr('value') != "") view.limpiarFiltro(view.dom.txtClienteContratos);

    }

    var obtenerFiltrosContratos = function () {

        IB.procesando.mostrar();
        
        var bMostrarTodos = $('#chkMostrarTodosContratos').is(':checked');
        var t306_idcontrato = null;
        var t302_idcliente = null;

        if (view.dom.idContratoM.val() != "") t306_idcontrato = view.dom.idContratoM.val();

        if (view.dom.txtClienteContratos.attr('value') != "") t302_idcliente = view.dom.txtClienteContratos.attr('value');

        //Negar la búsqueda cuando no haya filtros
        if (!t306_idcontrato && !t302_idcliente && view.dom.desContratoM.val() == "") {
            IB.procesando.ocultar();
            IB.bsalert.toastdanger("Debes seleccionar más criterios");
            return false;
        }

        obtenerContratos(bMostrarTodos, t306_idcontrato, view.dom.desContratoM.val(), view.dom.selBusquedaContratos.val(), t302_idcliente);

    }

    //Fin funcionalidad modal contratos

    //Funcionalidad buscador de clientes en contratos

    var buscarClienteContratos = function (e) {
        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();
        var o = {
            target: view.dom.txtClienteContratos,
            tipoBusqueda: "obtenerClientes",
            nivelOcultable: 3,
            bSoloActivos: false,
            bInternos: false
        }
        $(view.selectores.ayudaCliente).buscacliente("option", "searchParams", o);
        $(view.selectores.ayudaCliente).buscacliente("show");

    }

    //Fin funcionalidad buscador de clientes

    //Funcionalidad buscador responsable

    var abrirBuscadorResponsable = function (e) {
        view.dom.divBuscadorResp.buscadorPersonas({
            modal: true, titulo: "Selección de responsable de proyecto", tipoBusqueda: 'RESPONSABLES_PROYECTO',
            aceptar: function (data) {
                view.volcarTxtValue(view.dom.txtResponsable, data.PROFESIONAL, data.t314_idusuario);
            },
            cancelar: function () {
            }
        });
    }

    //Fin funcionalidad buscador responsable

    //Funcionalidad buscador de horizontal

    var buscarHorizontales = function (e) {
        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();
        var o = {
            titulo: "Selección de Horizontal",
            target: view.dom.txtHorizontal,
            tipoBusqueda: "obtenerHorizontales",
            nivelOcultable: 2
        }
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("option", "searchParams", o);
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("show");

    }

    //Fin funcionalidad buscador de horizontal

    //Funcionalidad buscadores de cualificadores

    abrirBuscadorQn = function () {

        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();

        var idCR;
        if (IB.vars.superEditor == "False") {
            if (view.dom.txtCR.val() != "") idCR = view.dom.txtCR.val();
        } else {
            if (view.dom.txtCR.attr('value') != "") idCR = view.dom.txtCR.attr('value');
        }

        var o = {
            titulo: "Selección de Cualificador Qn",
            target: view.dom.txtQn,
            tipoBusqueda: "obtenerCualificadores",
            nivelOcultable: 2,
            sTipo: "Qn",
            idNodo: parseInt(idCR)
        }
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("option", "searchParams", o);
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("show");

    }

    abrirBuscadorQ1 = function () {

        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();

        var idCR;
        if (IB.vars.superEditor == "False") {
            if (view.dom.txtCR.val() != "") idCR = view.dom.txtCR.val();
        } else {
            if (view.dom.txtCR.attr('value') != "") idCR = view.dom.txtCR.attr('value');
        }

        var o = {
            titulo: "Selección de Cualificador Q1",
            target: view.dom.txtQ1,
            tipoBusqueda: "obtenerCualificadores",
            nivelOcultable: 2,
            sTipo: "Q1",
            idNodo: parseInt(idCR)
        }
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("option", "searchParams", o);
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("show");

    }

    abrirBuscadorQ2 = function () {

        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();

        var idCR;
        if (IB.vars.superEditor == "False") {
            if (view.dom.txtCR.val() != "") idCR = view.dom.txtCR.val();
        } else {
            if (view.dom.txtCR.attr('value') != "") idCR = view.dom.txtCR.attr('value');
        }

        var o = {
            titulo: "Selección de Cualificador Q2",
            target: view.dom.txtQ2,
            tipoBusqueda: "obtenerCualificadores",
            nivelOcultable: 2,
            sTipo: "Q2",
            idNodo: parseInt(idCR)
        }
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("option", "searchParams", o);
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("show");

    }

    abrirBuscadorQ3 = function () {

        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();

        var idCR;
        if (IB.vars.superEditor == "False") {
            if (view.dom.txtCR.val() != "") idCR = view.dom.txtCR.val();
        } else {
            if (view.dom.txtCR.attr('value') != "") idCR = view.dom.txtCR.attr('value');
        }

        var o = {
            titulo: "Selección de Cualificador Q3",
            target: view.dom.txtQ3,
            tipoBusqueda: "obtenerCualificadores",
            nivelOcultable: 2,
            sTipo: "Q3",
            idNodo: parseInt(idCR)
        }
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("option", "searchParams", o);
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("show");

    }

    abrirBuscadorQ4 = function () {

        var opt = {
            delay: 1,
            hide: 1
        }
        IB.procesando.opciones(opt);
        IB.procesando.mostrar();

        var idCR;
        if (IB.vars.superEditor == "False") {
            if (view.dom.txtCR.val() != "") idCR = view.dom.txtCR.val();
        } else {
            if (view.dom.txtCR.attr('value') != "") idCR = view.dom.txtCR.attr('value');
        }

        var o = {
            titulo: "Selección de Cualificador Q4",
            target: view.dom.txtQ4,
            tipoBusqueda: "obtenerCualificadores",
            nivelOcultable: 2,
            sTipo: "Q4",
            idNodo: parseInt(idCR)
        }
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("option", "searchParams", o);
        $(view.selectores.ayudaCatalogoBasico).buscacatalogobasico("show");

    }
    //Fin funcionalidad buscadores de cualificadores

    //Funcionalidad ayuda clientes multi

    var abrirModalClienteMulti = function () {

        
        //$(view.selectores.ayudaClienteMulti).ayudamulti("option", "lstSeleccionados", lstSeleccionados);
        //$(view.selectores.ayudaClienteMulti).ayudamulti("show")

        //var opt = {
        //    delay: 1,
        //    hide: 1
        //}
        //IB.procesando.opciones(opt);
        //IB.procesando.mostrar();

        var lstSeleccionados = view.volcarClientesCriterio();

        var o = {
            target: view.dom.txtClienteContratos,
            tipoBusqueda: "obtenerClientes",
            nivelOcultable: 3,
            bSoloActivos: false,
            bInternos: false,
            lstSeleccionados: lstSeleccionados
        }
        $(view.selectores.ayudaClienteMulti).buscaclientemulti("option", "searchParams", o);
        $(view.selectores.ayudaClienteMulti).buscaclientemulti("show");


    }

    //Fin funcionalidad ayuda clientes multi

    /*Llamadas de carga a webmethods*/
    //Llamadas pantalla principal

    //Obtiene los partes de actividad en base a los filtros
    var obtenerPartes = function (idClientes, idproyectosubnodos, facturable, fDesde, fHasta) {

        var defer = $.Deferred();

        var payload = { idClientes: idClientes, idproyectosubnodos: idproyectosubnodos, facturable: facturable, dDesde: fDesde, dHasta: fHasta };
        IB.DAL.post(null, "obtenerPartesActividad", payload, null,
            function (data) {
                view.pintarTablaPartes(data);
                $.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
            }
        );

        return defer.promise();

    }

    //Fin llamadas pantalla principal

    //Llamadas modal proyectos

    //Obtiene los proyectos de actividad en base a los filtros
    var obtenerProyectos = function (idNodo, sEstado, sCategoria, idCliente, idResponsable, numPE, sDesPE, sTipoBusqueda, sCualidad, nContrato, nHorizontal, Qn, Q1, Q2, Q3, Q4, nModeloContratacion) {

        var defer = $.Deferred();

        var payload = { idNodo: idNodo, sEstado: sEstado, sCategoria: sCategoria, idCliente: idCliente, idResponsable: idResponsable, numPE: numPE, sDesPE: sDesPE, sTipoBusqueda: sTipoBusqueda, sCualidad: sCualidad, nContrato: nContrato, nHorizontal: nHorizontal, nCNP: Qn, nCSN1P: Q1, nCSN2P: Q2, nCSN3P: Q3, nCSN4P: Q4, nModeloContratacion: nModeloContratacion };
        IB.DAL.post(null, "obtenerProyectos", payload, null,
            function (data) {
                view.vaciarTablaProyectos();
                view.pintarTablaProyectos(data);
                $.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
            }
        );

        return defer.promise();

    }

    //Obtiene los modelos de contratación
    var obtenerModContra = function () {

        var defer = $.Deferred();

        var payload = { t316_idmodalidad: null, t316_denominacion: "", bTodos: true, nOrden: 2, nAscDesc: 0 };
        IB.DAL.post(null, "obtenerModContra", payload, null,
            function (data) {
                view.llenarModContrat(data);
                //$.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
                defer.resolve();
            }
        );

        return defer.promise();

    }

    //Obtiene los modelos de contratación
    var obtenerNodosCR = function () {

        var defer = $.Deferred();

        if (IB.vars.superEditor != "False") return defer.resolve();        

        var payload = {  };
        IB.DAL.post(null, "obtenerNodosCR", payload, null,
            function (data) {
                view.llenarNodosCR(data);
                //$.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
                defer.resolve();
            }
        );

        return defer.promise();

    }

    var obtenerNodosCRAdmin = function () {

        var defer = $.Deferred();

        if (IB.vars.superEditor == "False") return defer.resolve();

        var payload = {};
        IB.DAL.post(null, "obtenerNodosCRAdmin", payload, null,
            function (data) {
                view.mostrarModalCR(data);
                defer.resolve();
            }
        );

        return defer.promise();

    }

    //Fin llamadas modal proyectos

    //Llamadas modal contratos

    var obtenerDenominacionContrato = function (e) {

        view.vaciarFiltrosContratos("id");
        view.vaciarTablaContratos();
        if (e.keyCode != 13) return;
        if (view.dom.idContratoM.val() == "") return;

        IB.procesando.mostrar();

        var defer = $.Deferred();

        var payload = { t306_idcontrato: view.dom.idContratoM.val() };
        IB.DAL.post(null, "obtenerDenominacionContrato", payload, null,
            function (data) {
                if (data != null) {
                    view.volcarTxt(view.dom.desContratoM, data.t377_denominacion);
                    $.when(obtenerFiltrosContratos(), IB.procesando.ocultar()).then(function () { defer.resolve(); });
                } else {
                    $.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
                }                
            }
        );

        return defer.promise();

    }

    var obtenerContratos = function (bMostrarTodos, t306_idcontrato, t377_denominacion, sTipoBusq, t302_idcliente) {

        var defer = $.Deferred();

        var payload = { bMostrarTodos: bMostrarTodos, t306_idcontrato: t306_idcontrato, t377_denominacion: t377_denominacion, sTipoBusq: sTipoBusq, t302_idcliente: t302_idcliente };
        IB.DAL.post(null, "obtenerContratos", payload, null,
            function (data) {
                view.pintarTablaContratos(data);
                $.when(IB.procesando.ocultar()).then(function () { defer.resolve(); });
            }
        );

        return defer.promise();

    }

    //Fin llamadas modal contratos    

    return {
        init: init
    }

})(SUPER.IAP30.ParteAct.View);