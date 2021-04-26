var SUPER = SUPER || {};
SUPER.IAP30 = SUPER.IAP30 || {};
SUPER.IAP30.Calendario = SUPER.IAP30.Calendario || {}


SUPER.IAP30.Calendario.View = (function () {

    var dom = {
        imgCalendarioAnual: $('#anual'),
        icoProfesional: $('#divProf'),
        iconoProf: $("#icoProf"),
        iconoAgenda: $('#agenda'),
        icoAgenda: $('#icoAgenda'),
        btnAnual: $('#btnAnual'),
        divBtnAgenda: $('#divBtnAgenda'),
        divBtnUser: $('#divBtnUser'),
        diasCalendario: $('.monthly-day-pick'),
        modalAnual: $('#calendarioAnual'),
        modalProfesional: $('#buscProfesional'),
        spProfesional: $('#spProfesional'),
        spProfesionalMov: $('#spProfesionalMov'),
        //imagenProf: $('.imagenProf'),
        spDesCalendario: $('.spDesCalendario'),
        divBuscadorPersonas: $('.buscadorUsuario'),
        ventana: $(window),
        indicadorRedimension: $('#indicator'),
        estadoSlide: $('#slideImp').find(".toggler").parent().attr('data-status'),
        slideImp: $('#slideImp'),
        tareasMensuales: $('#tareasMensuales'),
        infoOcultable: $('.ocultable'),
        lblFUI: $('#lblFUI'),
        lblFUIMov: $('#lblFUIMov'),
        lblUMC: $('#lblUMC'),
        lblUMCMov: $('#lblUMCMov'),
        objetoCal: $('#cal'),
        divCalendarioAnual: $('#calendari_lateral1'),
        diaL: $("#L"),
        diaM: $("#M"),
        diaX: $("#X"),
        diaJ: $("#J"),
        diaV: $("#V"),
        diaS: $("#S"),
        diaD: $("#D"),
        divIconoUser: $('.divIconoUser'),
        nivel1: $('#nivel1'),
        nivel2: $('#nivel2'),
        nivel3: $('#nivel3'),
        nivel4: $('#nivel4'),
        bomba: $('#icoBomb'),
        nivel1Mov: $('#nivel1Mov'),
        nivel2Mov: $('#nivel2Mov'),
        nivel3Mov: $('#nivel3Mov'),
        nivel4Mov: $('#nivel4Mov'),
        bombaMov: $('#icoBombMov')
    }
    var indicadores = {
        i_dispositivoTactil: false
    }


    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }


    function habilitarReconexion() {
        dom.icoProfesional.css('display', 'block');
        dom.divBtnUser.css('display', 'block');
        dom.divIconoUser.css('display', 'block');
    }

    function habilitarAgenda() {

        dom.iconoAgenda.css('display', 'block');
        dom.divBtnAgenda.css('display', 'block');
    }

    function aperturaModal(element) {
        element.modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        element.modal('show');
        dom.infoOcultable.attr('area-hidden', true);
    }

    function cierreModal() {
        dom.divCalendarioAnual.html("");
        dom.infoOcultable.attr('aria-hidden', 'false');
    }


    function pintarDatosDespuesReconexion(data, anteriorprimerhueco) {
        //IB.vars.aFestivos = data.aFestivos;
        var nombreReconectado = data.NOMBRE + " " + data.APELLIDO1 + " " + data.APELLIDO2;
        IB.vars.codCal = data.IdCalendario;
        dom.spDesCalendario.html(data.desCalendario);
        dom.spProfesional.html(nombreReconectado);
        dom.spProfesionalMov.html(nombreReconectado);
        if (data.fUltImputacion == null) data.fUltImputacion = "";
        else data.fUltImputacion = moment(data.fUltImputacion).format("DD/MM/YYYY");

        dom.lblFUI.html(data.fUltImputacion);
        $('#lblFUIMov').html(data.fUltImputacion);
        dom.lblUMC.html(AnoMesToMesAnoDescLong(data.t303_ultcierreIAP));
        $('#lblUMCMov').html(AnoMesToMesAnoDescLong(data.t303_ultcierreIAP));
        //dom.imagenProf.attr('src', IB.vars["strserver"] + "images/imgUSU" + data.tipo + data.t001_sexo + ".gif");

        IB.vars.UMC_IAP = data.t303_ultcierreIAP.toString();
        IB.vars.FechaUltimaImputacion = data.fUltImputacion;
        IB.vars.controlhuecos = (data.t314_controlhuecos) ? "True" : "False";
        IB.vars.idficepi = data.t001_IDFICEPI;

        if (IB.vars.FechaUltimaImputacion != "") {
            if (moment(anteriorprimerhueco, "DDMMYYYY").isBefore(moment(IB.vars.FechaUltimaImputacion, "DDMMYYYY"))) IB.vars.FechaUltimaImputacion = anteriorprimerhueco;
            aUltimoDia = IB.vars.FechaUltimaImputacion.split("/");
            oUltImputac = new Date(aUltimoDia[2], eval(aUltimoDia[1] - 1), aUltimoDia[0]);

            var oUMC = new Date(IB.vars.UMC_IAP.substring(0, 4), eval(IB.vars.UMC_IAP.substring(4, 6) - 1), 1).add("mo", 1).add("d", -1);
            if (oUltImputac < oUMC) oUltImputac = oUMC;

        } else oUltImputac = null;        

        //Se resetea el objeto calendario con los datos del profesional reconectado 
        dom.objetoCal.monthly({
            'reset': true
        });       
    }

    function redimensionPantalla() {
        if ($('#slideImp').find(".toggler").parent().attr('data-status') == "closed") {
            $('#slideImp').css('right', '-95%');
        }
        if (dom.indicadorRedimension.is(':visible')) {
            $("#L").html('L');
            $("#M").html('M');
            $("#X").html('X');
            $("#J").html('J');
            $("#V").html('V');
            $("#S").html('S');
            $("#D").html('D');
            $('#slideImp').css('display', 'block');
            $('#lblFUIMov').html($('#lblFUI').html());
            $('#tareasMensuales').css('display', 'none');

        } else {
            $("#L").html('Lunes');
            $("#M").html('Martes');
            $("#X").html('Miércoles');
            $("#J").html('Jueves');
            $("#V").html('Viernes');
            $("#S").html('Sábado');
            $("#D").html('Domingo');
            $('#slideImp').css('display', 'none');
            $('#tareasMensuales').css('display', 'block');
        }
        
    }

    function tratamientoTareasMensuales() {
        if (dom.indicadorRedimension.is(':visible')) {
            dom.slideImp.css('display', 'block');
            dom.tareasMensuales.css('display', 'none');
        } else {
            dom.slideImp.css('display', 'none');
            dom.tareasMensuales.css('display', 'block');
        }        
    }

    /* INICIO TABLA DE TAREAS MENSUALES */

    var selectores = {
        sel_nombreLinea: "td.nombreLinea"
    }

    //Pintado de la tabla
    var pintarTabla = function (data, horasImputar, horasImputadas, mesCerrado) {

        var sHTML = new StringBuilder();
        var sb = new StringBuilder();
        var sfechaDia;
        var content;

        var nPE = 0, nPT = 0, nF = 0, nA = 0, nT = 0, nNivel = 0;
        var nFactual = 0, nAactual = 0;

        if (data.length > 0) {

            $.each(data, function (index, item) {
                sHTML = "";
                nFactual = item.t334_idfase;
                nAactual = item.t335_idactividad;

                if (nPE != item.t301_idproyecto) {
                    //Crear PE, PT, F, A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    nPE = item.t301_idproyecto;
                    sHTML = CrearProyEco(item);
                }
                else if (nPT != item.t331_idpt) {
                    //Crear PT, F, A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    sHTML = CrearProyTec(item);
                }
                else if ((nF != nFactual) && (nFactual != null)) {
                    //Crear F, A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    sHTML = CrearFase(item);
                }
                else if ((nA != nAactual) && (nAactual != null)) {
                    //Crear A, T y consumo
                    nT = item.t332_idtarea;
                    nA = item.t335_idactividad;
                    nF = item.t334_idfase;
                    nPT = item.t331_idpt;
                    if (nFactual == null)
                        nNivel = 3;
                    else nNivel = 4;
                    sHTML = CrearActividad(item, nNivel);
                }
                else if (nT != item.t332_idtarea) {
                    //Crear T y consumo
                    if (nFactual == null) {
                        if (nAactual == null) nNivel = 3;
                        else nNivel = 4;
                    }
                    else {
                        nNivel = 5;
                    }
                    nT = item.t332_idtarea;
                    sHTML = CrearTarea(item, nNivel);
                }
                else {
                    //Crear consumo
                    if (nFactual == null) {
                        if (nAactual == null) nNivel = 4;
                        else nNivel = 5;
                    }
                    else {
                        nNivel = 6;
                    }
                    sHTML = CrearConsumo(item, nNivel);
                }

                sb += sHTML.toString();
            });
        } else {
            sb = '<span style="padding-left:10px;padding-top:10px;">No hay consumos registrados</span>';
        }

        $('.bodyTabla').html(sb.toString());

        if (!indicadores.i_dispositivoTactil) {
            $('[data-toggle="popover"]').popover({ trigger: "hover", container: 'body', html: true });
        }
        
        $("#tabla tbody tr td span:empty").addClass("fk-elementosvacios");

        $(".bodyTabla tr[data-tipo!='PSN']").hide();

        $(".PieHorasImputadas").attr("title", horasImputadas);
        $(".PieHorasImputadas").text(accounting.formatNumber(horasImputadas));
        $(".PieHorasAImputar").attr("title", horasImputar);
        $(".PieHorasAImputar").text(accounting.formatNumber(horasImputar));

        if (mesCerrado) $('.PieFilaHorasAImputar').addClass("hidden");
        else $('.PieFilaHorasAImputar').removeClass("hidden");

        calcularAcumulados();
        inicializarTabla();
        colorearNivel("1");
        IB.procesando.ocultar();        
    }

    function borrarCatalogo() {
        dom.tablaContenido.html("");

        dom.totalHorasReportadas.attr('title', '');
        dom.totalHorasReportadas.val("0,00");
    }

    function inicializarTabla() {

        //Contraer todas las ramas menos los proyectos económicos
        //Ocultamos todas las filas que tuvieran asignado un padre

        $(".bodyTabla tr[data-parent!='']").hide();
        cebrear();
        $(window).resize(function () {
            cebrear();
        });        
    }
    
    function CrearFila(fila, nNivel, sTipo)
    {
        var sb = new StringBuilder();
        var sColor = "";
        var sContent = "";
        sb.append( "<tr ");
        switch (sTipo)
        {
            case "PE":
                sb.append("id='PSN" + fila.t301_idproyecto + "' data-tipo='PSN' class='linea' data-parent='' data-level='1' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                sb.append("<td headers='PSN" + fila.t301_idproyecto + "' class='nombreLinea Nivel1 columna1'>");
                sb.append("<span class='glyphicon-plus'></span>");
                switch (fila.t301_estado)
                {
                    case "A":
                        sColor = "verde";
                        break;
                    case "H":
                        sColor = "gris";
                        break;
                    case "C":
                        sColor = "rojo";
                        break;
                    case "P":
                        sColor = "naranja";
                        break;
                }

                sb.append("<span aria-hidden='true' class='fa fa-diamond fa-diamond-"+sColor+"'></span>");
                    
                sContent = "<b>Proyecto:</b>  " + fila.t301_denominacion.replace(/"/g, "").replace(/'/g, "") + "<br /> <b>Responsable:</b> " + fila.Responsable.replace(/"/g, "").replace(/'/g, "") + "<br /> <b>Cualidad:</b> " + fila.Cualidad.replace(/"/g, "").replace(/'/g, "") + "<br /> <b>Cliente:</b> " + fila.t302_denominacion.replace(/"/g, "").replace(/'/g, "") + "<br /> <b>C.R. :</b> " + fila.t303_idnodo + " - " + fila.t303_denominacion.replace(/"/g, "").replace(/'/g, "");
                sb.append("<span aria-hidden='true' style='padding-left: 5px;' data-placement='top' data-toggle='popover' data-content='" + sContent + "' title='<b>Información</b>'>" + fila.t301_idproyecto + " - " + fila.t301_denominacion.replace(/"/g, "'") + "</span>");

                sb.append("<span class='sr-only' role='button' aria-expanded='false'>Proyecto Económico - " + fila.t301_idproyecto + " (Nivel 1)</span>");
                sb.append("</td>");
                break;

            case "PT":
                sb.append("id='PT" + fila.t331_idpt + "' data-tipo='PT' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                sb.append("<td headers='PT" + fila.t331_idpt + "' class='nombreLinea Nivel" + nNivel + " columna1'>");
                sb.append("<span class='glyphicon-plus'></span>");
                sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>P</i></span>");
                sb.append("<span aria-hidden='true'>" + fila.T331_despt + "</span>");
                sb.append("<span class='sr-only' role='button' aria-expanded='false'>Proyecto Técnico - " + fila.t331_idpt + fila.T331_despt + " (Nivel  " + nNivel + "</span>");
                sb.append("</td>");
                break;

            case "F":
                sb.append("id='F" + fila.t334_idfase + "' data-tipo='F' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                sb.append("<td headers='F" + fila.t334_idfase + "' class='nombreLinea Nivel" + nNivel + " columna1'>");
                sb.append("<span class='glyphicon-plus'></span>");
                sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>" + sTipo + "</i></span>");
                sb.append("<span aria-hidden='true'>" + fila.t334_desfase + "</span>");
                sb.append("<span class='sr-only' role='button' aria-expanded='false'>Fase - " + fila.t334_idfase + fila.t334_desfase + " (Nivel  " + nNivel + "</span>");
                sb.append("</td>");
                break;

            case "A":
                sb.append("id='A" + fila.t335_idactividad + "' data-tipo='A' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                sb.append("<td headers='A" + fila.t335_idactividad + "' class='nombreLinea Nivel" + nNivel + " columna1'>");
                sb.append("<span class='glyphicon-plus'></span>");
                sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>" + sTipo + "</i></span>");
                sb.append("<span aria-hidden='true'>" + fila.t335_desactividad + "</span>");
                sb.append("<span class='sr-only' role='button' aria-expanded='false'>Actividad - " + fila.t335_idactividad + fila.t335_desactividad + " (Nivel  " + nNivel + "</span>");
                sb.append("</td>");
                break;

            case "T":
                sb.append("id='T" + fila.t332_idtarea + "' data-tipo='T' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "'>");
                sb.append("<td headers='T" + fila.t332_idtarea + "' class='nombreLinea Nivel" + nNivel + " columna1'>");
                sb.append("<span class='glyphicon-plus'></span>");
                sb.append("<span aria-hidden='true' class='fa-stack fa-lg fa-stack-linea'><i class='fa fa-circle fa-stack-1x circuloLinea'></i><i class='fa fa-stack-1x letraLinea'>" + sTipo + "</i></span>");
                sb.append("<span aria-hidden='true'>" + fila.t332_idtarea + " - " + fila.t332_destarea + "</span>");
                sb.append("<span class='sr-only' role='button' aria-expanded='false'>Tarea - " + fila.t332_idtarea + fila.t332_destarea + " (Nivel  " + nNivel + "</span>");
                sb.append("</td>");
                break;

            case "C":
                sb.append("id='C" + fila.t332_idtarea + "-" + moment(fila.t337_fecha).format("DD/MM/YYYY") + "' data-tipo='C' class='linea' data-parent='" + obtenerPadre(sTipo, fila) + "' data-level='" + nNivel + "' data-psn='" + fila.t301_idproyecto + "' data-pt='" + fila.t331_idpt + "' data-f='" + fila.t334_idfase + "' data-a='" + fila.t334_idfase + "' data-t='" + fila.t332_idtarea + "' ");
                if (fila.TotalHorasReportadas != null)
                    sb.append("nH='" + accounting.formatNumber(fila.TotalHorasReportadas) + "' ");
                else
                    sb.append("nH='0' ");
                sb.append(">");
                sb.append("<td class='FC-C' headers='C" + fila.t332_idtarea + "' class='nombreLinea Nivel" + nNivel + " columna1'>");
                var sFecha = fila.t337_fecha;
                if (sFecha != "") sFecha = moment(fila.t337_fecha).format("DD/MM/YYYY");
                sb.append("<span>" + sFecha + "</span>");  //Fechas de Consumo y Comentarios
                sb.append("</td>");
                break;
        }

        if (sTipo == "C")
        {
            var nHR = 0;
            if (fila.TotalHorasReportadas != null)
            {
                nHR = accounting.formatNumber(fila.TotalHorasReportadas);
            }
            sb.append("<td style='text-align:right;' class='columna2'>");
            sb.append(nHR);// TotalHorasReportadas
            sb.append("</td>");            
        }
        else
            sb.append("<td style='text-align:right;' class='columna2'></td>");

        sb.append("</tr>");

        return sb;

    }

        

    function CrearProyEco(fila){
        var sResul = CrearFila(fila, 1, "PE");
        sResul += CrearFila(fila, 2, "PT");
        if (fila.t334_idfase != null){
            sResul += CrearFila(fila, 3, "F");
            if (fila.t335_idactividad != null){
                sResul += CrearFila(fila, 4, "A");
                sResul += CrearFila(fila, 5, "T");
                sResul += CrearFila(fila, 6, "C");
            }
        }
        else
        {
            if (fila.t335_idactividad != null)
            {
                sResul += CrearFila(fila, 3, "A");
                sResul += CrearFila(fila, 4, "T");
                sResul += CrearFila(fila, 5, "C");
            }
            else
            {
                sResul += CrearFila(fila, 3, "T");
                sResul += CrearFila(fila, 4, "C");
            }
        }
        return sResul;
    }
    
    
    function CrearProyTec(fila){
        var sResul = CrearFila(fila, 2, "PT");
        if (fila.t334_idfase != null)
        {
            sResul += CrearFila(fila, 3, "F");
            if (fila.t335_idactividad != null)
            {
                sResul += CrearFila(fila, 4, "A");
                sResul += CrearFila(fila, 5, "T");
                sResul += CrearFila(fila, 6, "C");
            }
        }
        else
        {
            if (fila.t335_idactividad != null)
            {
                sResul += CrearFila(fila, 3, "A");
                sResul += CrearFila(fila, 4, "T");
                sResul += CrearFila(fila, 5, "C");
            }
            else
            {
                sResul += CrearFila(fila, 3, "T");
                sResul += CrearFila(fila, 4, "C");
            }
        }
        return sResul;
    }


    function CrearFase(fila){
        var sResul = CrearFila(fila, 3, "F");
        sResul += CrearFila(fila, 4, "A");
        sResul += CrearFila(fila, 5, "T");
        sResul += CrearFila(fila, 6, "C");
        return sResul;
    }

    function CrearActividad(fila, nNivel){
        var sResul = CrearFila(fila, nNivel, "A");
        sResul += CrearFila(fila, nNivel + 1, "T");
        sResul += CrearFila(fila, nNivel + 2, "C");
        return sResul;
    }

    function CrearTarea(fila, nNivel){
        var sResul = CrearFila(fila, nNivel, "T");
        sResul += CrearFila(fila, nNivel + 1, "C");
        return sResul;
    }

    function CrearConsumo(fila, nNivel){
        var sResul = CrearFila(fila, nNivel, "C");
        return sResul;
    }

    //Función de obtención del data-parent de un fila
    var obtenerPadre = function (tipo, fila) {

        var padre;

        switch (tipo) {
            case "PT":
                padre = "PSN" + fila.t301_idproyecto;
                break;
            case "F"://Activo
                padre = "PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                break;
            case "A":
                if (fila.t334_idfase == null) {
                    padre = "PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                } else {
                    padre = "F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                }
                break;
            case "T":
                if (fila.t335_idactividad == null) {
                    padre = "PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                } else {
                    if (fila.t334_idfase == null) {
                        padre = "A" + fila.t335_idactividad + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    } else {
                        padre = "A" + fila.t335_idactividad + " F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    }
                }
                break;

            case "C":
                if (fila.t335_idactividad == null) {
                    padre = "T" + fila.t332_idtarea + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                } else {
                    if (fila.t334_idfase == null) {
                        padre = "T" + fila.t332_idtarea + " A" + fila.t335_idactividad + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    } else {
                        padre = "T" + fila.t332_idtarea + " A" + fila.t335_idactividad + " F" + fila.t334_idfase + " PT" + fila.t331_idpt + " PSN" + fila.t301_idproyecto;
                    }
                }
                break;
        }

        return padre;

    }

    var lineas = {

        //Lineas de proyectos económicos
        PE: function () {
            return $('.bodyTabla tr[data-tipo="PSN"]').children();
        },

        //Lineas de proyectos técnicos
        PT: function () {
            return $('.bodyTabla tr[data-tipo="PT"]').children();

        },
        //Lineas que no sean Tarea, pie de tabla o línea de otros consumos
        padresTLineas: function () {

            return $('.bodyTabla tr:not([data-tipo="T"], [data-level="0"])').children();

        },

        //Lineas que no sean pie de tabla o línea de otros consumos
        TLineas: function () {

            return $('.bodyTabla tr:not([data-level="0"])').children();

        },

        //Linea activa y sus lineas descendientes que no sean tareas
        padresDesLineaActiva: function () {
            return $(".bodyTabla tr.activa, .bodyTabla [data-parent*='" + $(".bodyTabla tr.activa").attr("id") + "']:not([data-tipo='T'])").children();
        },

        //Lineas descendientes de la línea activa
        lineaDesActiva: function () {

            return $(".bodyTabla [data-parent*='" + $(".bodyTabla tr.activa").attr("id") + "']").children();

        },

        //Línea activa
        lineaActiva: function () {

            return $(".bodyTabla tr.activa");

        }

    }
    var niveles = {
        
        //Lineas de proyectos técnicos
        N2: function () {
            return $('.bodyTabla tr').filter(function () {
                var tipo = $(this).attr("data-tipo");
                if (tipo == "PSN" || tipo == "PT") return $(this);
            }).children();
        },
        N3: function () {
            return $('.bodyTabla tr').filter(function () {

                var tipo = $(this).attr("data-tipo");
                if (tipo == "PSN" || tipo == "PT" || tipo == "F" || tipo == "A" || tipo == "T") return $(this);
            }).children();
        },
        N4: function () {
           
            return $('.bodyTabla tr').children();
        }
    }

    // Colorear niveles
    var colorearNiveles = function (e) {

        var srcobj;
        srcobj = e.target ? e.target : e.srcElement;

        //El elemento target no es el mismo si se pulsa el el botón o el span que hay dentro del botón, por lo que se comprueba y se coge siempre el botón
        if (!$(srcobj).is(":button")) srcobj = $(srcobj).parent();

        var nivel = $(srcobj).attr("data-level");
        if ($(".bodyTabla tr").length == 0) nivel = 1;
        colorearNivel(nivel);
    }

    function colorearNivel(nivel) {
        switch (nivel) {
            case "1":
                $(".barra1").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra2").addClass("nivelGris").removeClass("nivelVerde");
                $(".barra3").addClass("nivelGris").removeClass("nivelVerde");
                $(".barra4").addClass("nivelGris").removeClass("nivelVerde");
                break;
            case "2":
                $(".barra1").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra2").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra3").addClass("nivelGris").removeClass("nivelVerde");
                $(".barra4").addClass("nivelGris").removeClass("nivelVerde");
                break;
            case "3":
                $(".barra1").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra2").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra3").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra4").addClass("nivelGris").removeClass("nivelVerde");
                break;
            case "4":
                $(".barra1").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra2").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra3").addClass("nivelVerde").removeClass("nivelGris");
                $(".barra4").addClass("nivelVerde").removeClass("nivelGris");
                break;
        }
    }
    //Marcado de línea activa
    var marcarLinea = function (thisObj) {

        //Eliminamos la clase activa de la fila anteriormente pulsada y se la asignamos a la que se ha pulsado
        desmarcarLinea();
        $(thisObj).addClass('activa');

        //Eliminamos el texto ' - Seleccionado' del elemento seleccionado anterior. Las posiciones 0 y 1 contienen el tipo de linea y su descripción.
        $('span:contains("- Seleccionado")').each(function () {
            //$(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1]);
            if ($(this).text().split(" - ")[1] == "Seleccionado") $(this).text($(this).text().split(" - ")[0]);
            else $(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1]);
        })

        //Añadimos el texo '- Seleccionado' al elemento seleccionado actualmente

        $(thisObj).children().children(":nth-child(4n)").text($(thisObj).children().children(":nth-child(4n)").text() + ' - Seleccionado');
    }

    //Desmarcado de línea activa
    var desmarcarLinea = function () {

        $(".bodyTabla tr.activa").removeClass('activa');
    }




    //Apertura de niveles
    var mostrarNivel = function (thisObj, nivel) {

        switch (nivel) {
            case 2:
                $(thisObj.parent()).show();
                break;
            case 3:
                $(thisObj.parent()).show();
                break;
            case 4:
                $(thisObj.parent()).show();
                break;
            case 5:
                $(thisObj.parent()).show();
                break;
            case 6:
                $(thisObj.parent()).show();
                break;
        }
        actualizarNivelExp(thisObj.parent(), nivel);
    }
    //Actualiza los valores del nivel

    var actualizarNivelExp = function (thisObj, nivel) {
        $(thisObj).each(function () {
            if ($(this).attr('data-level') < nivel || ((nivel == "3" && $(this).attr('data-tipo')!="T" && $(this).attr('data-tipo')!="C")  && ($(this).attr('data-level') == "3" || $(this).attr('data-level') == "4"))) {
                $(this).find("span[aria-expanded]").attr('aria-expanded', 'true');
                $(this).children().find(".glyphicon-plus").addClass("glyphicon-minus").removeClass("glyphicon-plus");
            }
        })
        cebrear();  
    }

    //Cierre del nivel (de momento igual)

    var cerrarNivel = function (e) {

        var srcobj;
        srcobj = e.target ? e.target : e.srcElement;

        //El elemento target no es el mismo si se pulsa el el botón o el span que hay dentro del botón, por lo que se comprueba y se coge siempre el botón
        if (!$(srcobj).is(":button")) srcobj = $(srcobj).parent();

        var nivel = $(srcobj).attr("data-level");

        $('.bodyTabla tr').filter(function () {
            return $(this).attr("data-level") > nivel;
        }).hide();

        //Modifica el atributo de expandido de los nodos cerrados
        $('.bodyTabla tr').filter(function () {
            return $(this).attr("data-level") >= nivel;
        }).find('span[aria-expanded]').attr('aria-expanded', 'false');

        //Cambia el glyphicon de los nodos cerrados
        $('.bodyTabla tr').filter(function () {
            return $(this).attr("data-level") >= nivel;
        }).find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");

        desmarcarLinea();

        //Eliminamos el texto ' - Seleccionado' del elemento seleccionado anterior. Las posiciones 0 y 1 contienen el tipo de linea y su descripción.
        $('span:contains("- Seleccionado")').each(function () {
            $(this).text($(this).text().split(" - ")[0] + ' - ' + $(this).text().split(" - ")[1]);
        });

        cebrear();

    }

    //Comprobación de apertura o cierre de linea. True para abrir y False para cerrar
    var abrir = function (thisObj) {

        if ($(thisObj).children().hasClass('glyphicon-plus')) return true;
        return false;

    }

    //Apertura de lineas
    var abrirLinea = function (thisObj, proceso) {

        switch (proceso) {
            case 0:
                var id = $(thisObj).attr('id')
                //Abre los hijos del nodo pulsado
                thisObj.show();
                actualizarLineaAbierta(id)
                break;
            case 1:
                var id = $(thisObj).parent().attr('id')
                //Abre los hijos del nodo pulsado
                $(".bodyTabla tr[data-parent^='" + id + "']").show();
                actualizarLineaAbierta(id)
                break;
            case 2:
                $(thisObj.parent()).show();
                actualizarLineaAbierta(lineas.PT());
                break;
            case 3:
                $(thisObj.parent()).show();
                actualizarLineaAbierta(lineas.padresTLineas())
                break;
            case 4:
                $(thisObj.parent()).show();
                actualizarLineaAbierta(lineas.padresDesLineaActiva())
                break;
            case 5:
                $(thisObj.parent()).show();
                actualizarLineaAbierta(lineas.padresDesLineaActiva())
                break;
        }

    }

    //Actualiza los valores de la línea recién abierta
    var actualizarLineaAbierta = function (id) {

        //Modifica el atributo de expandido de la línea pulsada
        $(".bodyTabla tr[id='" + id + "']").find("span[aria-expanded]").attr('aria-expanded', 'true');

        //Cambia el glyphicon
        $(".bodyTabla tr[id='" + id + "']").find(".glyphicon-plus").toggleClass("glyphicon-minus", true).removeClass("glyphicon-plus");
        cebrear();

    }    


    //Cierre de lineas del arbol de la tabla
    var cerrarLinea = function (thisObj, proceso) {

        var id = $(thisObj).parent().attr('id')

        //Cierra los descendientes del nodo pulsado
        $(".bodyTabla tr[data-parent*='" + id + "']").hide();

        //Modifica el atributo de expandido de la línea pulsada
        $(".bodyTabla tr[id='" + id + "']").find("span[aria-expanded]").attr('aria-expanded', 'false');

        //Modifica el atributo de expandido de los descendientes del nodo pulsado
        $(".bodyTabla tr[data-parent*='" + id + "']").find("span[aria-expanded]").attr('aria-expanded', 'false');

        //Cambia el glyphicon de los descendientes del nodo pulsado y de sí mismo
        $(".bodyTabla tr[data-parent*='" + id + "']").find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");
        $(".bodyTabla tr[id='" + id + "']").find('.glyphicon-minus').addClass("glyphicon-plus").removeClass("glyphicon-minus");

        cebrear();
    }
   
    function calcularAcumulados() {
        try {
            var nTH = 0;
            var nAH = 0;
            var nFH = 0;
            var nPTH = 0;
            var nPEH = 0;
            var nTotH = 0;
            var nH = 0;

            $($('.bodyTabla tr').get().reverse()).each(function () {
                if ($(this).attr('data-tipo') == "C") { // Consumo

                    nH = parseFloat(accounting.unformat($(this).attr('nH'), ","));                    
                    nTH += nH;
                    if ($(this).attr('data-level') >= 5) {
                        nAH += nH;
                    }
                    if ($(this).attr('data-level') == 6) {
                        nFH += nH;
                    }
                    nPTH += nH;
                    nPEH += nH;
                    nTotH += nH;
                }

                else if ($(this).attr('data-tipo') == "T") {//Tarea
                    $(this).children().eq(1).attr("title", nTH);

                    $(this).children().eq(1).text(accounting.formatNumber(nTH));

                    nTH = 0; 

                } else if ($(this).attr('data-tipo') == "A") {//ACTIVIDAD
                    $(this).children().eq(1).attr("title", nAH);

                    $(this).children().eq(1).text(accounting.formatNumber(nAH));

                    nTH = 0;
                    nAH = 0;

                } else if ($(this).attr('data-tipo') == "F") {//FASE
                    $(this).children().eq(1).text(nFH);

                    $(this).children().eq(1).text(accounting.formatNumber(nFH));

                    nTH = 0;
                    nAH = 0;
                    nFH = 0;

                } else if ($(this).attr('data-tipo') == "PT") {//PT
                    $(this).children().eq(1).attr("title", nPTH);

                    $(this).children().eq(1).text(accounting.formatNumber(nPTH));

                    nTH = 0;
                    nAH = 0;
                    nFH = 0;
                    nPTH = 0;

                } else if ($(this).attr('data-tipo') == "PSN") {//PE
                    $(this).children().eq(1).attr("title", nPEH);

                    $(this).children().eq(1).text(accounting.formatNumber(nPEH));

                    nTH = 0;
                    nAH = 0;
                    nFH = 0;
                    nPTH = 0;
                    nPEH = 0;
                }
            });

            
        } catch (e) {
            IB.bsalert.fixedAlert("danger", "Error de aplicación", "Error calculando acumulados");
        }
    }


    //Función de cebreo de las filas de la tabla
    function cebrear() {

        $(".bodyTabla tr:visible:not(.bg-info)").removeClass("cebreada");
        $('.bodyTabla tr:visible:not(.bg-info):even').addClass('cebreada');
        controlarScroll();

    }

    function controlarScroll() {
       
        /*Controlamos si el contenedor tiene Scroll*/
        var div = document.getElementById('contenedor');
        var divMov = document.getElementById('contenedorMov');

        var scrollWidth = $('#contenedor').width() - div.scrollWidth;
        var scrollWidthMov = $('#contenedorMov').width() - divMov.scrollWidth;

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
        var hasVerticalScrollbarMov = divMov.scrollHeight > divMov.clientHeight;

        if (hasVerticalScrollbar) {

            $(".div-table").css("width", "calc( " + $('#contenedor').width() + "px - " + scrollWidth + "px )");
            $("#tablaCabecera").css("width", "calc( " + $('#contenedor').width() + "px - " + scrollWidth + "px )");
            $("#tablaPie").css("width", "calc( " + $('#contenedor').width() + "px - " + scrollWidth + "px )");
        }

        else {
            $(".div-table").css("width", "" + $('#contenedor').width() + "px");
            $("#tablaCabecera").css("width", "" + $('#contenedor').width() + "px");
            $("#tablaPie").css("width", "" + $('#contenedor').width() + "px");            
        }

        if (hasVerticalScrollbarMov) {

            $(".div-tableMov").css("width", "calc( " + $('#contenedorMov').width() + "px - " + scrollWidthMov + "px )");
            $("#tablaCabeceraMov").css("width", "calc( " + $('#contenedorMov').width() + "px - " + scrollWidthMov + "px )");
            $("#tablaPieMov").css("width", "calc( " + $('#contenedorMov').width() + "px - " + scrollWidthMov + "px )");
        }
        else {
            $(".div-tableMov").css("width", "" + $('#contenedorMov').width() + "px");
            $("#tablaCabeceraMov").css("width", "" + $('#contenedorMov').width() + "px");
            $("#tablaPieMov").css("width", "" + $('#contenedorMov').width() + "px");
        }

    }

    /* FINN ARBOL DE TAREAS MENSUALES*/

    var init = function () {

        //Migas de pan(Si se vuelve de imputación diaria y por funcionamiento del menu.ascx se pierden)
        if ($('#Menu_SiteMap1 > span > a:contains("IAP")').length == 0) {
            var enlace = '<span><a href="/"></a></span><span> &gt; </span><span><a title="Informe de actividad y progreso">IAP</a></span><span> &gt; </span><span><a>Reporte</a></span><span> &gt; </span><span><a>Calendario</a></span>';
            $("#Menu_SiteMap1").html(enlace);
        }

        //Se controla la dimensión de la pantalla para escribir la cabecera del calendario (días largos o cortos) y 
        //mostrar el resumen mensual de tareas de una manera o de otra
        redimensionPantalla();
        //tratamientoTareasMensuales();
        //dom.imagenProf.attr('src', IB.vars["strserver"] + IB.vars.imageUrl);
        dom.lblFUI.html((dom.lblFUI.html().split(" "))[0]);

        if (('ontouchstart' in window) || (navigator.maxTouchPoints > 0) || (navigator.msMaxTouchPoints > 0)) {
            indicadores.i_dispositivoTactil = true;
        }

    }

    return {
        init: init,
        dom: dom,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        habilitarReconexion: habilitarReconexion,
        habilitarAgenda: habilitarAgenda,
        aperturaModal: aperturaModal,
        pintarDatosDespuesReconexion: pintarDatosDespuesReconexion,
        cierreModal: cierreModal,
        redimensionPantalla: redimensionPantalla,
        tratamientoTareasMensuales: tratamientoTareasMensuales,
        pintarTabla: pintarTabla,
        selectores: selectores,
        lineas: lineas,
        abrir: abrir,
        marcarLinea: marcarLinea,
        desmarcarLinea: desmarcarLinea,
        abrirLinea: abrirLinea,
        cerrarLinea: cerrarLinea,
        borrarCatalogo: borrarCatalogo,
        colorearNiveles: colorearNiveles,
        cerrarNivel: cerrarNivel,
        mostrarNivel: mostrarNivel,
        niveles: niveles
    }
})();