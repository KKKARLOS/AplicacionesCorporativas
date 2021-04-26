$(document).ready(function () { SUPER.IAP30.Calendario.app.init(); })

var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Calendario = SUPER.IAP30.Calendario || {}

SUPER.IAP30.Calendario.app = (function (view) {

    
    var init = function () {

        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }
              
        //Inicializa formateo
        accounting.settings = {
            number: {
                precision: 2,
                thousand: ".",
                decimal: ","
            }
        }

        /****************************** CALCULO DE VARIABLES ****************************************************/
                
        var oUltImputac;
        var listasSemanas;
        var oUMC = new Date(IB.vars.UMC_IAP.substring(0, 4), eval(IB.vars.UMC_IAP.substring(4, 6) - 1), 1); //.add("mo", 1).add("d", -1);
        var aUltimoDia;
        oUltImputac = new Date(1900, 0, 1);

        if (IB.vars.FechaUltimaImputacion != "") {
            if (moment(IB.vars.anteriorprimerhueco, "DDMMYYYY").isBefore(moment(IB.vars.FechaUltimaImputacion, "DDMMYYYY"))) IB.vars.FechaUltimaImputacion = IB.vars.anteriorprimerhueco;
            aUltimoDia = IB.vars.FechaUltimaImputacion.split("/");
            oUltImputac = new Date(aUltimoDia[2], eval(aUltimoDia[1] - 1), aUltimoDia[0]);
        }

        if (oUltImputac < oUMC) oUltImputac = oUMC;


        /********************** Inicialización de los componenetes que se cargan a traves de los plugins *********************************************/
        
        //Se inicializa el control calendario (monthly.js)
        view.dom.objetoCal.monthly({
            'month': IB.vars.nCurrentMonth,
            'year': IB.vars.nCurrentYear
        });
        

        //Se inicializa el control BootSideMenu (BootSideMenu.js)
        view.dom.slideImp.BootSideMenu({ side: "right" });        
        /*******************************************************************************************************/

        view.init();

        //Se atachan los eventos 
        //Apertura de la modal del calendario anual desde el icono del calendario(pantalla grande/pequeña)
        view.attachEvents("click", view.dom.imgCalendarioAnual, abrirCalendarioAnual);        
        view.attachEvents("click", view.dom.btnAnual, abrirCalendarioAnual);

        //Se comprueba si el profesional tiene permisos de reconexión y si es así, se atacha el evento correspondiente
        if (IB.vars.nReconectar == 1) {
            view.habilitarReconexion();
            view.attachEvents("click", view.dom.icoProfesional, abrirBuscadorProfesional);
            view.attachEvents("click", view.dom.divBtnUser, abrirBuscadorProfesional);        
        }
        //Visualiza el icono de la agenda
        view.habilitarAgenda();
        //Redirección a la ventana de la agenda
        view.attachEvents("click", view.dom.iconoAgenda, redirigirAgenda);
        view.attachEvents("click", view.dom.divBtnAgenda, redirigirAgenda);

        //Redimensionamiento de la ventana
        view.attachEvents("resize", view.dom.ventana, view.redimensionPantalla);
        
        //Cierre de la ventana modal del calendario anual
        view.attachEvents("hidden.bs.modal", view.dom.modalAnual, view.cierreModal);

        //Clicks de filas de la tabla
        view.attachLiveEvents("keypress click", view.selectores.sel_nombreLinea, clickLinea);

        //Botones de expasión/contracción de niveles en Movil
        view.attachEvents("keypress click", $('#nivel1Mov'), mostrarNivel1);
        view.attachEvents("keypress click", $('#nivel2Mov'), mostrarNivel2);
        view.attachEvents("keypress click", $('#nivel3Mov'), mostrarNivel3);
        view.attachEvents("keypress click", $('#nivel4Mov'), mostrarNivel4);

        //Botón de bomba en Movil
        view.attachEvents("keypress click", $('#icoBombMov'), abrirBomba);
       
        //Botones de expasión/contracción de niveles 
        view.attachEvents("keypress click", view.dom.nivel1, mostrarNivel1);
        view.attachEvents("keypress click", view.dom.nivel2, mostrarNivel2);
        view.attachEvents("keypress click", view.dom.nivel3, mostrarNivel3);
        view.attachEvents("keypress click", view.dom.nivel4, mostrarNivel4);

        //Botón de bomba
        view.attachEvents("keypress click", view.dom.bomba, abrirBomba);              

        
    },

    abrirCalendarioAnual = function (e) {
        
        //TODO: Pensar si la inicialización del calendario debe ir en el onready o aqui
        //Se inicializa el control calendario anual (bic_calendar.js)
        var monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
        var dayNames = ["L", "M", "X", "J", "V", "S", "D"];
        var events = [
            {
                date: "",
                title: '',
                link: '',
                color: '',
                content: '',
                class: '',
            }
        ];

        view.dom.divCalendarioAnual.bic_calendar({
            //list of events in array
            events: events,
            //enable select
            enableSelect: true,
            //enable multi-select
            multiSelect: true,
            //set day names
            dayNames: dayNames,
            //set month names
            monthNames: monthNames,
            //show dayNames
            showDays: true,
        });

        view.aperturaModal(view.dom.modalAnual);        
    }    

    abrirBuscadorProfesional = function (e) {
        //Se inicializa el control buscadorUsuario (BuscadorUsuario.js)
        view.dom.divBuscadorPersonas.buscadorPersonas({
            modal: true, titulo: "Selección de profesional", tipoBusqueda: 'profesionales',
            aceptar: function (data) {
                //alert("profesional seleccionado: " + data.PROFESIONAL);
                /*view.dom.spProfesional.html(data.PROFESIONAL);
                view.dom.spProfesionalMov.html(data.PROFESIONAL);*/
                IB.vars.codUsu = data.t314_idusuario;
                var filtro = { sUsuario: IB.vars.codUsu };

                IB.procesando.mostrar();
                IB.DAL.post(null, "establecerUsuarioIAP", filtro, null,
                    function (data) {
                        IB.DAL.post(null, "obtenerPrimerHueco", null, null,
                            function (data2) {
                                view.pintarDatosDespuesReconexion(data, data2);
                            },
                            function (ex, status) {
                                IB.procesando.ocultar();
                                IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al acceder a los datos del usuario.");
                            }
                        );
                    },
                    function (ex, status) {
                        IB.procesando.ocultar();
                        IB.bsalert.fixedAlert("danger", "Error de aplicación", "Al acceder a los datos del usuario.");
                    }
                );

            },
            cancelar: function () {               
            }
        });
    }

    redirigirAgenda = function (e) {
        var date = $('.selectMonth').val() + $('.selectYear').val()
        var opciones = {
            delay: 1
        }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
        //var qs = IB.uri.encode("date=" + date);
        location.href = "../Agenda/Default.aspx?";

    }

    var clickLinea = function (event) {

        var srcobj = event.target ? event.target : event.srcElement;

        //Si el elemento no es el td se busca en la jerarquia superior
        if (!$(srcobj).is("td")) srcobj = $(srcobj).parent().closest('td')

        //Solo se buscan hijos si no es una tarea
        if ($(srcobj).parent().attr('data-tipo') != "C") {
            cargarLinea($(srcobj), 1);
        }

        ////Se marca la línea como seleccionada
        view.marcarLinea($(srcobj).parent());

    }

    //Muestra todos los proyectos técnicos de los proyectos económicos
    var mostrarNivel1 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);
    }
    //Muestra todos los elementos de nivel2
    var mostrarNivel2 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);

        IB.procesando.mostrar();
        view.mostrarNivel(view.niveles.N2(), 2);
        IB.procesando.ocultar();

    }
    //Muestra todos los elementos de nivel3
    var mostrarNivel3 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);

        IB.procesando.mostrar();
        view.mostrarNivel(view.niveles.N3(), 3);
        IB.procesando.ocultar();

    }
    //Muestra todos los elementos de nivel4
    var mostrarNivel4 = function (e) {
        view.colorearNiveles(e);
        view.cerrarNivel(e);

        IB.procesando.mostrar();
        view.mostrarNivel(view.niveles.N4(), 6);
        IB.procesando.ocultar();

    }

    ////Muestra todas las líneas que cuelgan de la línea activa de la tabla
    var abrirBomba = function (e) {

        var lineaActiva = view.lineas.lineaActiva();
        view.cerrarNivel(e);
        view.marcarLinea(lineaActiva);
        IB.procesando.mostrar();
        view.mostrarNivel(view.lineas.lineaDesActiva(), 6);
        view.abrirLinea(lineaActiva, 0);

        //cargarLinea(view.lineas.lineaDesActiva(), 4);
        IB.procesando.ocultar();

    }

    //Tratamiento de la visualización de las líneas de la tabla
    var cargarLinea = function (thisObj, proceso) {

        //Proceso 1 -> Abrir línea a línea
        //Proceso 2 -> Abrir Proyectos técnicos

        switch (proceso) {
            case 1:
                if (view.abrir($(thisObj))) {
                    return view.abrirLinea($(thisObj), proceso);
                }
                else {
                    return view.cerrarLinea($(thisObj), proceso);
                }
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                return view.abrirLinea($(thisObj), proceso);
                break;
        }

    }

    return {
        init: init
    }

})(SUPER.IAP30.Calendario.View);