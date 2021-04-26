var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.GASVI = SUPER.IAP30.GASVI || {}


SUPER.IAP30.GASVI.View = (function (e) {
    //Guarda el elemento que contenía el foco antes de pulsar un botón
    var prevFocus;
    var returnElement;

    var dom = {
        divCalculadora: $('#idCalculadora'),
        ocultable: $('.ocultable'),
        //imagenProf: $('#imagenProf'),
        //spProfesional: $('#spProfesional'),
        calculadora: $('#calculadora'),
        btnLlevarValor: $('#btnLlevarValor'),
        btnAnot: $('#btnAnot'),
        icoAnot: $('#icoAnot'),
        closeAnota: $('#closeAnota'),
        //idCalculadoratxtResultado: $('#idCalculadoratxtResultado'),
        btnCalc: $('#btnCalc'),
        //cboMotivo: $('#cboMotivo'),
        cboMoneda: $('#cboMoneda'),
        cboEmpresa: $('#cboEmpresa'),
        observaciones: $('#txtObser'),
        anotaciones: $('#anotaciones'),
        celdaComentario: $('.celdaComentario'),
        comentarioGasto: $('#comentarioGasto'),
        //lblProy: $('#lblProy'),
        buscaProyectos: $('#buscaProyectos'),
        tbodyProyectos: $('#tbodyProyectos'),
        proyecto: $('#proyecto'),
        iconosColapsables: $('.ibox-title_toggleable'),
        ficheroAdjunto: $('input:radio[id^="radio"]'),
        icoJustificante: $("#icoJustificante"),
        lineaTablaProy: $(".lineaTablaProy"),
        txtConcepto : $('#txtConcepto'),
        txtPro: $("#txtPro"),
        proyecto: $("#proyecto"),
        NFilaGasto: $("#NFilaGasto"),
        EFilaGasto: $("#EFilaGasto"),
        DFilaGasto: $("#DFilaGasto"),
        tablaGastos: $("#tablaGastos"),
        bodyGastos: $("#bodyGastos"),
        linkProy: $('#lblProy'),
        ayudaProyecto: $(".fk_ayudaProyecto"),
        IdProyectoSubnodo: $('#hdnIdProyectoSubNodo'),
        txtPagadoTransporte: $('#TransGas'),
        txtPagadoHotel: $('#HGas'),
        txtPagadoOtros: $('#OGas'),
        totGas: $('#totGas'),
        txtGSTKM: $('#txtGSTKM'),
        txtGSTDC: $('#txtGSTDC'),
        txtGSTMD: $('#txtGSTMD'),
        txtGSTDE: $('#txtGSTDE'),
        txtGSTAL: $('#txtGSTAL'),
        txtGSTOTAL: $('#txtGSTOTAL'),
        txtNomina: $('#txtNomina'),
        txtImpAnticipo: $('#ImpoAnti'),
        fecAnticipo: $('#FAnti'),
        lugarAnticipo: $('#OfiAnti'),
        txtAclaraciones: $('#txtAclaraciones'),
        txtAclaracionesPagados: $('#txtAclaraciones2'),
        txtImpDevolucion: $('#ImpoDev'),
        FDevolucion: $('#FDev'),
        OfiDevolucion : $('#OfiDev'),
        txtSinRet: $('#txtSinRet'),
        txtTotalViaje: $('#TViaje'),
        btnDistancias: $('#imgKMSEstandares'),
        distancias: $('#distancias'),
        tablaBilletes: $('#tablaBilletes'),
        btnAceptarAnot: $('#btnAceptarAnot'),
        btnCancelarAnot: $('#btnCancelarAnot'),
        hdnAnotacionesPersonales: $('#hdnAnotacionesPersonales'),
        txtDesAnotaciones: $('#txtDesAnotaciones'),
        txtDesComentario: $('#txtDesComentario'),
        btnAceptarComent: $('#btnAceptarComent'),
        desplazamiento: $('#desplazamiento'),
        tbodyDesplaza: $('#tbodyDesplaza'),
        tablaDesplaza: $('#tblDesplaza'),
        btnSeleccionarDesplazamiento: $('#btnSeleccionarDesplazamiento'),
        btnCancelarDesplazamiento: $('#btnCancelarDesplazamiento'),
        btnTramitar: $('#btnTramitar'),
        btnVolver: $('#btnVolver')
    }
    var selectores = {
        contenedor: ".container",
        sel_inputs: "input",
        filasGasto: "tr.filaGasto",
        filaActiva: "tr.activa",
        filasDespla: "tr.filaDesplaza",
        sel_Fecha: "input.txtFecha",
        sel_Dest: "input.txtDest",
        sel_Coment: "i.txtComent",
        sel_Dieta: "input.txtDieta",
        sel_ImpDieta: "input.txtImpDieta",
        sel_Justi: "input.txtJusti",
        sel_Km: "input.txtKm",
        sel_ImpKm: "input.txtImpKms",
        sel_Total: "input.txtTotal",
        sel_CabEco : "td[headers='hECO']",
        sel_ImgECO: "img.imgECO",
        sel_Anticipo: "input.anticipo",
        sel_Gpe: "input.gpe",
        rdbJustificantes: 'input:radio[name=optradio]:checked',
        empresa: "#cboEmpresa option:selected",
        idCalculadoratxtResultado: "#idCalculadoratxtResultado",
        sel_Proy: ".fk_ayudaProyecto",
        sel_LinkComent: ".txtLinkComent",
        sel_LinkECO: ".txtLinkECO",
        filaActivaError: 'tr.activa td[headers="hIni"] input',
        primeraDieta: 'td[headers="hCD"] input',
        alojamiento: 'td[headers="hA"] input',
        primeraFecha: 'td[headers="hIni"] input',
        peajes: 'td[headers="hPeajes"] input',
        km: 'td[headers="hKm"] input'
    }
    var indicadores = {
        i_dispositivoTactil: false
    }
    //Evento de búsqueda en la tabla
    //var eventFired = function (type) {alert('Hola ' + type);}

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }
    //para elementos que no existen de inicio
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {

        var enlace = '<span><a href="/"></a></span><span> &gt; </span><span><a title="Informe de actividad y progreso">IAP</a></span><span> &gt; </span><span><a>Reporte</a></span><span> &gt; </span><span><a>Calendario</a></span><span> &gt; </span><span>Imputación diaria</span><span> &gt; </span><span>GASVI</span>';
        $("#Menu_SiteMap1").html(enlace);

        inicializarPantalla();
        //asignarControlDatepicker();
        inicializarCalendarios();
    }
    function inicializarPantalla() {
        //var hoy = moment();
        //$("#txtDesde").val(hoy.startOf('month').format('L'));
        //$("#txtHasta").val(hoy.startOf('month').add(1, 'months').subtract(1, "days").format('L'));
        $('#hdnIdProyectoSubNodo').val(IB.vars.nPSN);
        //dom.imagenProf.attr('src', IB.vars["strserver"] + IB.vars.imageUrl);
        $("#txtConcepto").attr('maxlength', 50);
        $("#OfiAnti").attr('maxlength', 50);
        $("#OfiDev").attr('maxlength', 50);
        $("#FAnti").attr('maxlength', 10);
        $("#FDev").attr('maxlength', 10);
        $("#ImpoAnti").attr('maxlength', 7);
        $("#ImpoDev").attr('maxlength', 7);
        $(".gpe").attr('maxlength', 7);
        $("#totGas").attr('maxlength', 8);

        ////Inicialización de datepickers  
        //inicializarCalendarios();
        //Creación de la calculadora
        $("#idCalculadora").Calculadora({
            ClaseBtns1: 'primary', /* Color Numbers*/
            ClaseBtns2: 'primary', /* Color Operators*/
            ClaseBtns3: 'primary', /* Color Clear*/
            Botones: ["+", "-", "*", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "="]
        });

        //recogemos el último input por el que ha pasado el foco
        $("input").focus(function () {
            if (typeof prevFocus !== "undefined") {
                $("#prev").html(prevFocus.val());
            }
            prevFocus = $(this);
        });
        //Marcar la línea al entrar a un input de una línea del catálogo de gastos
        $(document).on('focusin', 'input.gasto', function (e) {
            marcarLinea($(this).parent().parent());
        });

        //Recepción del foco en el textarea del modal de comentario
        $(document).on('shown.bs.modal', '#comentarioGasto', function () {
            $('#txtDesComentario').focus();
        });
        //Recepción del foco en el textarea del modal de anotaciones
        $(document).on('shown.bs.modal', '#anotaciones', function () {
            $('#txtDesAnotaciones').focus();
        });
        
        //validación de dietas 
        $(document).on('keypress', 'input.txtDieta', function (e) {
            return soloDigitos(e);
        });
        //validación de kms 
        $(document).on('keypress', 'input.txtKm', function (e) {
            //return soloDigitos(e);
            return validarTeclaNumerica(e, true);
        });
        //validación de justificantes 
        $(document).on('keypress', 'input.txtJusti', function (e) {
            return validarTeclaNumerica(e, true);
        }); 
        //validación de anticipos
        $(document).on('keypress', 'input.anticipo', function (e) {
            return validarTeclaNumerica(e, true);
        });
        //validación de gastos pagados por la empresa 
        $(document).on('keypress', 'input.gpe', function (e) {
            return validarTeclaNumerica(e, true);
        });

        //Comprobación de si es un dispositivo móvil (en principio para no mostrar los popover)
        if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
            indicadores.i_dispositivoTactil = true;
        }

        $('#txtConcepto').focus();
    }
    var inicializarCalendarios = function () {
        $('#FDev').datepicker({
            changeMonth: true,
            changeYear: true,

            beforeShow: function (input, inst) {
                $(this).removeClass('calendar-off').addClass('calendar-on');
            },
            onClose: function () {
                $(this).removeClass('calendar-on').addClass('calendar-off');
            }
        });

        //Validación de inputación manual en datepickers
        $(document).on('focusout', '#FDev.hasDatepicker', function (e) {
            var input = $(this);
            window.setTimeout(function () {
                if ((!moment(input.val(), 'DD/MM/YYYY', 'es', true).isValid()) && (input.val() != '')) {
                    IB.bsalert.toastdanger("Formato de fecha incorrecto: " + input.val());
                    input.val('');
                    input.focus();
                }
            }, 100);
        });

        $('#FAnti').datepicker({
            changeMonth: true,
            changeYear: true,

            beforeShow: function (input, inst) {
                $(this).removeClass('calendar-off').addClass('calendar-on');
            },
            onClose: function () {
                $(this).removeClass('calendar-on').addClass('calendar-off');
            }
        });

        $(document).on('focusout', '#FAnti.hasDatepicker', function (e) {
            var input = $(this);
            window.setTimeout(function () {
                if ((!moment(input.val(), 'DD/MM/YYYY', 'es', true).isValid()) && (input.val() != '')) {
                    IB.bsalert.toastdanger("Formato de fecha incorrecto: " + input.val());
                    input.val('');
                    input.focus();
                }
            }, 100);
        });
    }

    //Función de habilitación/deshabilitación de los botones de la botonera superior e inferior xs-sm
    var habilitarBtn = function (boton, valor, callback) {

        if (valor) {
            attachEvents("keypress click", boton, callback);
            $(boton).removeClass('botonDeshabilitado');
            $(boton).addClass('boton');
            $(boton).attr("tabindex", "0");
            $(boton).attr("role", "link");
        }
        else {
            deAttachEvents(boton);
            $(boton).removeClass('boton');
            $(boton).addClass('botonDeshabilitado');
            $(boton).removeAttr("tabindex");
            $(boton).removeAttr("role");
        }
    }

    function cebrear() {
        $("tr:visible:not(.bg-info)").removeClass("cebreada");
        $('tr:visible:not(.bg-info):even').addClass('cebreada');
        controlarScroll();
        //$("#tablaProyectos tr").removeClass("cebreada");
        //$('#tablaProyectos tr:even').addClass('cebreada');
    }
    obtenerValor = function (elemento) {
        return elemento.val();
    }

    darValor = function (elemento, value) {
        elemento.val(value);
    }
    function controlarScroll() {
        /*Controlamos si el contenedor tiene Scroll*/

        var div = document.getElementById('bodyGastos');

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
        if (hasVerticalScrollbar) $("#tablaGastos thead").css("width", "calc( 100% - 1em )")
        else { $("#tablaGastos thead").css("width", "100%") }

        /*FIN Controlamos si el contenedor tiene Scroll*/
    }

    var visualizarContenedor = function () {

        $(selectores.contenedor).css("visibility", "visible");

    }

    function rellenarComboMonedas(data) {
        //Alimentar el combo de proyectos para ese rango temporal
        IB.procesando.ocultar();
        dom.cboMoneda.empty();
        $.each(data, function (index, item) {
            //if (item.t422_idmoneda == IB.vars.idMoneda)
            //    dom.cboMoneda.append($('<option></option>').val(item.t422_idmoneda).html(item.t422_denominacion));
            //else
                dom.cboMoneda.append($('<option></option>').val(item.t422_idmoneda).html(item.t422_denominacion));
        });
        //dom.cboMoneda.val(dom.cboMoneda.find('option:first').val());
        dom.cboMoneda.val(IB.vars.idMoneda);
    }

    function ponerLineaGasto() {
        var sb = new StringBuilder();
        sb.append("<tr class='filaGasto'>");
        sb.append("<td headers='hIni'><input class='txtFecha form-control form-control-fila input-md gasto date-picker calendar-off' type='text' aria-label='Fecha Inicio' placeholder='dd/mm/aaaa' title='Formato de fecha:dd/mm/aaaa' /></td>");
        sb.append("<td headers='hFin'><input class='txtFecha form-control form-control-fila input-md gasto date-picker calendar-off' type='text' aria-label='Fecha Fin' placeholder='dd/mm/aaaa' title='Formato de fecha:dd/mm/aaaa' /></td>");

        sb.append("<td headers='hDest'><input type='text' class='txtDest form-control form-control-fila input-md gasto' aria-labelledby='hC' /></td>");
        sb.append("<td headers='hC' tabindex='0' class='icoComent txtLinkComent' role='button' aria-label='Comentario al gasto'>");
        //sb.append("<i class='fa fa-file-o text-info'></i></td>");
        sb.append("<i class='txtComent fa fa-comment-o' aria-hidden='true' data-toggle='popover' title='Comentario' data-trigger='focus'></i></td>");

        sb.append("<td headers='hCD'><input class='txtDieta form-control form-control-fila input-md gasto' type='text' aria-labelledby='hCD' /></td>");
        sb.append("<td headers='hM'><input class='txtDieta form-control form-control-fila input-md gasto' type='text' aria-labelledby='hM' /></td>");
        sb.append("<td headers='hE'><input class='txtDieta form-control form-control-fila input-md gasto' type='text' aria-labelledby='hE' /></td>");
        sb.append("<td headers='hA'><input class='txtDieta form-control form-control-fila input-md gasto' type='text' aria-labelledby='hA' /></td>");
        sb.append("<td headers='hImpo'><input class='txtImpDieta form-control form-control-fila input-md gasto' type='text' readonly='readonly' aria-labelledby='hImpo' placeholder='0,00' title='Formato de importe: 0,00' /></td>");

        sb.append("<td headers='hKm'><input class='txtKm form-control form-control-fila input-md gasto' type='text' aria-labelledby='hKm' /></td>");
        sb.append("<td headers='hImpoProp'><input class='txtImpKms form-control form-control-fila input-md gasto' type='text' aria-labelledby='hImpoProp' readonly='readonly' aria-readonly='true' /></td>");
        sb.append("<td headers='hECO' tabindex='0' class='txtLinkECO' title='Selección de desplazamiento ECO' role='button' aria-label='Desplazamiento ECO relacionado'></td>");

        sb.append("<td headers='hPeajes'><input class='txtJusti form-control form-control-fila input-md gasto' type='text' aria-labelledby='hPeajes' placeholder='0,00' title='Formato de importe: 0,00' /></td>");
        sb.append("<td headers='hComidas'><input class='txtJusti form-control form-control-fila input-md gasto' type='text' aria-labelledby='hComidas' placeholder='0,00' title='Formato de importe: 0,00' /></td>");
        sb.append("<td headers='hTransp'><input class='txtJusti form-control form-control-fila input-md gasto' type='text' aria-labelledby='hTransp' placeholder='0,00' title='Formato de importe: 0,00' /></td>");
        sb.append("<td headers='hHotel'><input class='txtJusti form-control form-control-fila input-md gasto' type='text' aria-labelledby='hHotel' placeholder='0,00' title='Formato de importe: 0,00' /></td>");

        sb.append("<td headers='hTotal'><input class='txtTotal form-control form-control-fila input-md gasto' type='text' aria-labelledby='hTotal' readonly='readonly' aria-readonly='true' /></td>");
        sb.append("</tr>");
        
        //$('#tablaGastos tr:last').after(sb.toString());
        $('#bodyGastos').append(sb.toString());
        marcarLinea($('#tablaGastos tr.filaGasto:last'));
    }
    //Marcado de línea activa
    var marcarLinea = function (thisObj) {
        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        desmarcarLinea();
        $(thisObj).addClass('activa');
    }

    //Desmarcado de línea activa
    var desmarcarLinea = function () {
        $("#bodyGastos tr.activa").removeClass('activa');
    }

    var marcarLineaECO = function (thisObj) {
        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        desmarcarLineaECO();
        $(thisObj).addClass('activa');
        $('#btnSeleccionarDesplazamiento').removeAttr('disabled');
    }

    //Desmarcado de línea activa
    var desmarcarLineaECO = function () {
        $("#tbodyDesplaza tr.activa").removeClass('activa');
        $('#btnSeleccionarDesplazamiento').attr('disabled', 'disabled');
    }

    var alternarClaseIconos = function (e) {
        //$(this).toggleClass('fa-chevron-down fa-chevron-up');
        $(this).find('.fa').toggleClass('fa-chevron-down fa-chevron-up');
    }

    function pintartblDesplaza(data) {
        var tblDesplaza = "";
        var sNumero = "";

        $.each(data, function (index, item) {
            tblDesplaza = "<tr class='filaDesplaza'>";
            //sNumero = (item.t615_iddesplazamiento == "0") ? "" : accounting.formatNumber(item.t615_iddesplazamiento);
            tblDesplaza += "<td class='text-right'>" + item.t615_iddesplazamiento + "</td>";
            tblDesplaza += "<td class='text-left'>" + item.t615_destino + "</td>";
            tblDesplaza += "<td class='text-left'>" + item.t615_observaciones + "</td>";
            tblDesplaza += "<td class='text-left'>" + moment(item.t615_fechoraida).format('DD/MM/YYYY HH:mm') + "</td>";
            tblDesplaza += "<td class='text-left'>" + moment(item.t615_fechoravuelta).format('DD/MM/YYYY HH:mm') + "</td>";
            sNumero = (item.numero_usos == "0") ? "" : accounting.formatNumber(item.numero_usos);
            tblDesplaza += "<td class='text-right'>" + sNumero + "</td>";
            tblDesplaza += "</tr>";
            dom.tbodyDesplaza.append(tblDesplaza);
        });

        //Inyectar html en la página
        //dom.tablaDetalle.html(tblDetalle);
        cebrear();
    }

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        indicadores: indicadores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        //pintarTablaCarga: pintarTablaCarga,
        //pintarTipologias: pintarTipologias,
        //pintarDatosDespuesReconexion: pintarDatosDespuesReconexion,
        //deshabilitarReconexion: deshabilitarReconexion
        //rellenarComboMotivos: rellenarComboMotivos,
        rellenarComboMonedas: rellenarComboMonedas,
        ponerLineaGasto: ponerLineaGasto,
        cebrear: cebrear,
        marcarLinea: marcarLinea,
        marcarLineaECO: marcarLineaECO,
        alternarClaseIconos: alternarClaseIconos,
        visualizarContenedor: visualizarContenedor,
        obtenerValor: obtenerValor,
        darValor: darValor,
        pintartblDesplaza: pintartblDesplaza,
        habilitarBtn: habilitarBtn
    }
})();