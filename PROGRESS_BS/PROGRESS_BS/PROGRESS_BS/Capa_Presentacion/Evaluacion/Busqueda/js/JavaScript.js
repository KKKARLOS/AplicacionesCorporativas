//RequestFilter: variable donde se almacenarán los filtros
var rf;

var $encurso = $('#tbdEval tr[estado=CUR]');
var $cerradas = $('#tbdEval tr[estado!=CUR]');
var $cboestado = $('#cboEstado');

var textEvaluado = "En caso de identificar un profesional, filtra las evaluaciones para obtener aquellas en las que conste como evaluado";
var textEvaluador = "En caso de identificar un profesional, filtra las evaluaciones para obtener aquellas en las que los evaluados sean profesionales dependientes en función del grado de profundización seleccionado.";



$(document).ready(function () {
    
    comprobarerrores();

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".list-group").css("max-height", "350px");
        }
    }

    //manejador de fechas
    moment.locale('es');

   
    if ($('input#evaluador').val() != "") {
        $("#imgEvaluador").css("display", "inline-block");
    }

    if ($('input#txtCR').val() != "") {
        $("#imgCR").css("display", "inline-block");
    }

    if ($('input#txtRol').val() != "") {
        $("#imgRol").css("display", "inline-block");
    }

    if ($("#divFiltros").hasClass("collapse in")) $("#linkFiltros").text("Replegar filtros de búsqueda");
    else {
        $("#linkFiltros").text("Desplegar filtros de búsqueda");        
    } 

    switch (entrada.toUpperCase()) {
        case "EVA":
            ActivarFormularios(entrada.toUpperCase(), JSON.parse(modeloFormularios));
            $('input#evaluador').attr('idficepi', idficepi.toString());
            $('input#evaluador').val(nombre);
            break;

        case "ADM":
            ActivarFormularios(entrada.toUpperCase(), null);
            break;
    }

    if (filtrosConsultas != "")
    {        
        MostrarEvaluaciones();
    }

    //$("#tblEvaluaciones").tablesorter();
    //setTimeout(function () { $('#tblEvaluaciones').tablesorter(); }, 10000);
    //$('#tblEvaluaciones').tablesorter();

    $(".fkInfo").text(textEvaluado);


    $('input[id=rdbEvaluado]').on("click", function () {
        $("#divNivel").css("display", "none");
        $(".fkInfo").text(textEvaluado);
    })

    
    $('#linkFormularioEstandar').on('click', function () {
        if ($(this).children().hasClass("glyphicon-plus"))
            $(this).children().removeClass("glyphicon-plus").addClass("glyphicon-minus");
        else
            $(this).children().removeClass("glyphicon-minus").addClass("glyphicon-plus");

    });


    $('#linkFormularioCAU').on('click', function () {
        if ($(this).children().hasClass("glyphicon-plus"))
            $(this).children().removeClass("glyphicon-plus").addClass("glyphicon-minus");
        else
            $(this).children().removeClass("glyphicon-minus").addClass("glyphicon-plus");

    });

    
});


$("#linkFiltros, #linkFiltros2").on("click", function () {
    if ($("#divFiltros").is(".in")) {
        $("#linkFiltros").text("Desplegar filtros de búsqueda");
        $("#divFiltros").css("height", "auto");
        //$("#divFiltros").hide().addClass("in").css("height", "auto").fadeIn("slow");
        //$("#divFiltros").fadeIn("slow");
        //QUITAMOS LA ANIMACIÓN
        $("#divFiltros").removeClass("animation");
          
    }

    else {
        $("#linkFiltros").text("Replegar filtros de búsqueda");
        //$("#divFiltros").hide().removeClass("in").css("display", "none");
        //$("#divFiltros").fadeOut("slow");        
    } 
})

$('input[id=rdbEvaluador]').on("click", function () {
    $("#divNivel").css("display", "block");
    $(".fkInfo").text("En caso de identificar un profesional, filtra las evaluaciones para obtener aquellas en las que los evaluados sean profesionales dependientes en función del grado de profundización seleccionado.");
})

$('input[id=rdbEvaluado]').on("click", function () {
    $("#divNivel").css("display", "none");
    $(".fkInfo").text("En caso de identificar un profesional, filtra las evaluaciones para obtener aquellas en las que conste como evaluado.");
})


$("#imgEvaluador").on("click", function () {
    $("#evaluador").val("");
    $("#evaluador").attr("idficepi", "");
    $("#imgEvaluador").css("display", "none");
});

$("#imgCR").on("click", function () {
    $("#txtCR").val("");
    $("#imgCR").css("display", "none");
});

$("#imgRol").on("click", function () {
    $("#txtRol").val("");
    $("#imgRol").css("display", "none");
});

$cboestado.on('change', function () {
    $this = $(this);
    if ($this.val() == '0') {//En curso
        $encurso.removeClass('hide');
        $cerradas.addClass('hide');
    } else if ($this.val() == '1') {//Cerradas
        $encurso.addClass('hide');
        $cerradas.removeClass('hide');
    }
});

//Selección de profesionales
$('body').on('click', '#tbdEval tr', function (e) {
    $('#tbdEval tr').removeClass('active');
    $(this).addClass('active');

    var $filaactiva = $("#tbdEval tr.active");

    if ($filaactiva.attr("data-permitircambioestado") == "true") {        
        $("#btnAlterarEstado").prop("disabled", false);
    }
    else {        
        $("#btnAlterarEstado").prop("disabled", true);
        
        
    } 
});



$('body').on('click', '.glyphicon.glyphicon-search', function (e) {

    $active = $(this).parent().parent().addClass("active");
    if ($active.length > 0) {
        //Si el estado es Abierta o en Curso y no es el evaluador, acceso no permitido
        if (($active.attr('codestado') == "ABI" || $active.attr('codestado') == "CUR") && idficepi != $active.attr('idevaluador')) {
            alertNew("warning", "Acceso no permitido. Para acceder a una evaluación no cerrada, debes ser evaluador/a de la misma", null, 4000, null);
            return;
        }
        switch ($active.attr('t934_idmodeloformulario')) {
            case "1":
                location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?acceso=consultas&pt=consultas&menu=' + entrada + '&idval=' + codpar($active.attr('idvaloracion'));
                break;
            case "2":
                location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?acceso=consultas&pt=consultas&menu=' + entrada + '&idval=' + codpar($active.attr('idvaloracion'));
                break;
        }
    } else
        alertNew("warning", "Tienes que seleccionar alguna evaluación");
});



$('#btnAcceder').on('click', function () {
    $active = $('#tbdEval tr.active');
    if ($active.length > 0) {
        //Si el estado es Abierta o en Curso y no es el evaluador, acceso no permitido
        if (($active.attr('codestado') == "ABI" || $active.attr('codestado') == "CUR") && idficepi != $active.attr('idevaluador'))
        {
            alertNew("warning", "Acceso no permitido. Para acceder a una evaluación no cerrada, debes ser el evaluador de la misma", null, 4000, null);
            return;
        }
        switch ($active.attr('t934_idmodeloformulario')) {
            case "1":
                location.href = strServer +'Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?acceso=consultas&pt=consultas&menu=' + entrada + '&idval=' + codpar($active.attr('idvaloracion'));
                break;
            case "2":
                location.href = strServer +'Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?acceso=consultas&pt=consultas&menu=' + entrada + '&idval=' + codpar($active.attr('idvaloracion'));
                break;
        }
    } else
        alertNew("warning", "Tienes que seleccionar alguna evaluación");

});

$('#btnAlterarEstado').on('click', function () {
    $("#modal-alterarEstado").modal("show");
})



$("#btnCancelarGrabar").on("click", function () {
    $("#selectNuevoEstado").html("");
})


$('#btnGrabar').on('click', function () {
    $("#modal-confirmaciongrabar").modal("show");
})


$("#btnSi").on("click", function () {
    $active = $('#tbdEval tr.active');

    $.ajax({
        url: "Default.aspx/updateEstado",   // Current Page, Method
        data: JSON.stringify({ t930_idvaloracion: $active.attr("idvaloracion"), nuevoestado: $("#selectNuevoEstado option:selected").val(), correoevaluador: $active.attr("data-correoevaluador"), correoevaluado: $active.attr("data-correoevaluado"), nombreyapellidoevaluador: $active.attr("data-nombreyapellidoevaluador"), nombreyapellidoevaluado: $active.attr("data-nombreyapellidoevaluado"), nombrecortoevaluador: $active.attr("data-nombrecortoevaluador"), nombrecortoevaluado: $active.attr("data-nombrecortoevaluado"), fecapertura: $active.attr("data-fecapertura"), estadoantiguo: $active.attr("codestado") }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-confirmaciongrabar").modal("hide");
            $("#modal-alterarEstado").modal("hide");
            $("#selectNuevoEstado").html("");
            MostrarEvaluaciones();
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar actualizar el estado de la evaluación.");
        }
    });
})

//Poner el foco en la caja de texto del apellido1
$('#modal-alterarEstado').on('shown.bs.modal', function () {
    $tractive = $('#tbdEval tr.active');

    $("#txtEstadoActual").text($tractive.attr("data-estado"));

    if ($tractive.attr("codestado") =="CUR") {
        $("#selectNuevoEstado").append($("<option></option>").val("ABI").html("Abierta"));        
    }

    else if ($tractive.attr("codestado") == "CCF" || $tractive.attr("codestado") == "CSF") {
        $("#selectNuevoEstado").append($("<option></option>").val("ABI").html("Abierta"));
        $("#selectNuevoEstado").append($("<option></option>").val("CUR").html("En curso"));
    }


    
})




//Obtiene un objecto con los criterios de filtrado.
function getRequestFilter() {
    var rf = new doConsultas();

//    rf.desde = null;
    //    rf.hasta = null;

    rf.Origen = entrada;
    rf.Idficepi_usuario = idficepi;
    

    rf.desde = parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val());
    rf.hasta = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());

    //Si es evaluado
    if ($('input[name=rdbFigura]:checked').val() == "0") {
        rf.t001_idficepi = $('input#evaluador').attr('idficepi');
        rf.txtNombre = $('input#evaluador').val();
        rf.t001_idficepi_evaluador = null;
        rf.Profundidad = null;
    }

    //Evaluador
    else {
        rf.t001_idficepi = null;
        rf.t001_idficepi_evaluador = $('input#evaluador').attr('idficepi');
        rf.txtNombre = $('input#evaluador').val();
        rf.Profundidad = $("#cboProfundizacion").val();
    }

    rf.estado = $("#cboSituacion").val();;
    rf.t930_denominacionCR = $('#txtCR').val();
    rf.t930_denominacionROL = $('#txtRol').val();
    rf.t941_idcolectivo = $("#cboColectivo").val();
    rf.t930_puntuacion = $("#cboCalidad").val();



    var lAux = [];

    if ($('input[id=chkEstGesCli1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEstGesCli2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEstGesCli3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkEstGesCli4]').is(':checked'))
        lAux.push(4);

    rf.lestt930_gescli = lAux;


    lAux = [];

    if ($('input[id=chkEstLiderazgo1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEstLiderazgo2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEstLiderazgo3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkEstLiderazgo4]').is(':checked'))
        lAux.push(4);

    rf.lestt930_liderazgo = lAux;

    lAux = [];

    if ($('input[id=chkEstPlanOrga1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEstPlanOrga2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEstPlanOrga3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkEstPlanOrga4]').is(':checked'))
        lAux.push(4);

    rf.lestt930_planorga = lAux;


    lAux = [];

    if ($('input[id=chkEstExpTecnico1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEstExpTecnico2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEstExpTecnico3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkEstExpTecnico4]').is(':checked'))
        lAux.push(4);

    rf.lestt930_exptecnico = lAux;

    lAux = [];

    if ($('input[id=chkEstCooperacion1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEstCooperacion2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEstCooperacion3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkEstCooperacion4]').is(':checked'))
        lAux.push(4);

    rf.lestt930_cooperacion = lAux;

    lAux = [];

    if ($('input[id=chkEstIniciativa1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEstIniciativa2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEstIniciativa3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkEstIniciativa4]').is(':checked'))
        lAux.push(4);

    rf.lestt930_iniciativa = lAux;


    lAux = [];

    if ($('input[id=chkEstPerseverancia1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEstPerseverancia2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEstPerseverancia3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkEstPerseverancia4]').is(':checked'))
        lAux.push(4);

    rf.lestt930_perseverancia = lAux;

    

    rf.estaspectos = $("#cboEstReconocer").val();
    rf.estmejorar = $("#cboEstMejorar").val();


    if (entrada == "EVA") {
        rf.SelectMejorar = null;
        rf.SelectSuficiente = null;
        rf.SelectBueno = null;
        rf.SelectAlto = null;


        rf.SelectMejorarCAU = null;
        rf.SelectSuficienteCAU = null;
        rf.SelectBuenoCAU =null;
        rf.SelectAltoCAU = null;

    } else {
        rf.SelectMejorar = $("#selectMejorar").val();
        rf.SelectSuficiente = $("#selectSuficiente").val();
        rf.SelectBueno = $("#selectBueno").val();
        rf.SelectAlto = $("#selectAlto").val();


        rf.SelectMejorarCAU = $("#selectMejorarCAU").val();
        rf.SelectSuficienteCAU = $("#selectSuficienteCAU").val();
        rf.SelectBuenoCAU = $("#selectBuenoCAU").val();
        rf.SelectAltoCAU = $("#selectAltoCAU").val();
    }
    

   
    lAux = [];

    if ($('input[id=chkEvolucion1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=rdbEvolucion2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=rdbEvolucion3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=rdbEvolucion4]').is(':checked'))
        lAux.push(4);

    if ($('input[id=rdbEvolucion5]').is(':checked'))
        lAux.push(5);

    if ($('input[id=rdbEvolucion6]').is(':checked'))
        lAux.push(6);

    if ($('input[id= rdbEvolucion7]').is(':checked'))
        lAux.push(7);
    
    rf.lestt930_interesescar = lAux;
    
    
    
    lAux = [];

    if ($('input[id=chkCAUGesCli1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkCAUGesCli2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkCAUGesCli3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkCAUGesCli4]').is(':checked'))
        lAux.push(4);

    rf.lcaut930_gescli = lAux;

    lAux = [];

    if ($('input[id=chkCAULiderazgo1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkCAULiderazgo2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkCAULiderazgo3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkCAULiderazgo4]').is(':checked'))
        lAux.push(4);

    rf.lcaut930_liderazgo = lAux;

    lAux = [];

    if ($('input[id=chkCAUPlanOrga1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkCAUPlanOrga2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkCAUPlanOrga3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkCAUPlanOrga4]').is(':checked'))
        lAux.push(4);

    rf.lcaut930_planorga = lAux;

    lAux = [];

    if ($('input[id=chkEstCAUTecnico1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkCAUExpTecnico2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkCAUExpTecnico3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkCAUExpTecnico4]').is(':checked'))
        lAux.push(4);

    rf.lcaut930_exptecnico = lAux;

    lAux = [];

    if ($('input[id=chkCAUCooperacion1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkCAUCooperacion2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkCAUCooperacion3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkCAUCooperacion4]').is(':checked'))
        lAux.push(4);

    rf.lcaut930_cooperacion = lAux;

    lAux = [];

    if ($('input[id=chkCAUIniciativa1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkCAUIniciativa2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkCAUIniciativa3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkCAUIniciativa4]').is(':checked'))
        lAux.push(4);

    rf.lcaut930_iniciativa = lAux;

    lAux = [];

    if ($('input[id=chkCAUPerseverancia1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkCAUPerseverancia2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkCAUPerseverancia3]').is(':checked'))
        lAux.push(3);

    if ($('input[id=chkCAUPerseverancia4]').is(':checked'))
        lAux.push(4);

    rf.lcaut930_perseverancia = lAux;



    lAux = [];

    if ($('input[id=chkEvolucionCAU1]').is(':checked'))
        lAux.push(1);

    if ($('input[id=chkEvolucionCAU2]').is(':checked'))
        lAux.push(2);

    if ($('input[id=chkEvolucionCAU3]').is(':checked'))
        lAux.push(3);

    
    rf.lcaut930_interesescar = lAux;
    
    rf.caumejorar = $("#cboCAUMejorar").val();

    return rf;

}

$('#btnbuscar').on('click', function () {
    MostrarEvaluaciones();
    //initDataTable();
});


function MostrarEvaluaciones() {
    //$('#tbdFoto').children().remove();
    //$tbdFoto = $('#tbdFoto');

    if (filtrosConsultas != "") {
        setFiltrosConsultas();
    }
    
    rf = getRequestFilter();
    actualizarSession();
    $.ajax({
        url: "Default.aspx/ListaEvaluaciones",   // Current Page, Method
        "data": JSON.stringify({ requestFilter: rf }),
        //"data": JSON.stringify(),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            
            //pintarDatosPantalla(JSON.parse(result.d));

            $result = $(result.d);
            if ($result.length > 0) {
                //initDataTable();
            

                $("#divFiltros").removeClass("collapse in").addClass("collapse");
                $("#linkFiltros").text("Desplegar filtros de búsqueda");
                //cambiar a desplegar
                //$("#divFiltros").collapse();
                $("#tblResultados").css("display", "block");
                $("#btnAcceder").css("display", "block");
                $("#btnAlterarEstado").css("display", "block");
                $("#btnEliminar").css("display", "block");
                $("#btnAlterarEstado").prop("disabled", true);

                tblEvaluaciones = "";
                $(result.d).each(function () {                    
                    if (this.FechaCierre == null) this.FechaCierre = "";
                    else this.FechaCierre = moment(this.FechaCierre).format("DD/MM/YYYY");

                    tblEvaluaciones += "<tr data-feccierre='" + this.FechaCierre +"'  data-fecapertura='" + moment(this.fecha).format("DD/MM/YYYY") + "' data-estado='" + this.estado + "' class='data' data-permitircambioestado='" + this.Permitircambioestado + "' idevaluado='" + this.idevaluado + "' data-correoevaluador='" + this.Correoevaluador + "' data-correoevaluado='" + this.Correoevaluado + "' data-nombreyapellidoevaluador='" + this.evaluador + "' data-nombreyapellidoevaluado='" + this.evaluado + "' data-nombrecortoevaluado='" + this.Nombrecortoevaluado + "' data-nombrecortoevaluador='" + this.Nombrecortoevaluador + "'   idevaluador='" + this.idevaluador + "' codestado= '" + this.codestado + "' idvaloracion='" + this.idvaloracion + "' t934_idmodeloformulario='" + this.idformulario + "' ><td><i class='glyphicon glyphicon-search'></i></td><td>" + this.Nombrecompletoevaluador + "</td><td>" + this.Nombrecompletoevaluado + "</td><td>" + this.estado + "</td><td>" + moment(this.fecha).format("DD/MM/YYYY") + "</td>" + "<td>" + this.FechaCierre + "</td></tr>";
                });
                

                //Si hay demasiados resultados 
                if (result.d.length >= 5000) {
                    alertNew("warning", "El volumen del resultado obtenido excede el límite máximo permitido. Acota más la búsqueda y vuelve a intentarlo", null, 6000, null);
                    return;
                }


                $("#tbdEval").html(tblEvaluaciones);
                
                /*Controlamos si el contenedor tiene Scroll*/
                var div = document.getElementById('tbdEval');

                var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
                if (hasVerticalScrollbar) {
                    $("thead").css("width", "calc( 100% - 1em )")
                }
                else { $("thead").css("width", "100%") }
                /*FIN Controlamos si el contenedor tiene Scroll*/

                //Ordenación de columnas                
                //setTimeout(function () { $('#tblEvaluaciones').tablesorter(); }, 10000);
                //$('#tblEvaluaciones').tablesorter();


                $("#tblEvaluaciones").trigger("destroy");

                //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
                $("#tblEvaluaciones").tablesorter({
                    //dateFormat: "dd/mm/yy", // set the default date format
                    // pass the headers argument and assing a object            
                    headers: {
                        // set "sorter : false" (no quotes) to disable the column
                        0: {
                            sorter: false
                        },
                        1: {
                            sorter: "text"
                        },
                        2: {
                            sorter: "text"
                        },
                        3: {
                            sorter: "text"
                        },
                        4:
                        {
                            sorter: "shortDate", dateFormat: "ddmmyyyy"
                        },
                        5:
                       {
                           sorter: "shortDate", dateFormat: "ddmmyyyy"
                       }

                    }
                });

               
               //PAGINACIÓN (COMENTADO POR PROBLEMAS DE RENDIMIENTO)
                load = function () {
                    window.tp = new Pagination('#tablePaging', {
                        itemsCount: $result.length,
                        onPageSizeChange: function (ps) {
                            console.log('changed to ' + ps);
                        },
                        onPageChange: function (paging) {
                            //custom paging logic here
                            console.log(paging);
                            var start = paging.pageSize * (paging.currentPage - 1),
                                end = start + paging.pageSize,
                                $rows = $('#tblEvaluaciones').find('.data');

                            $rows.hide();

                            for (var i = start; i < end; i++) {
                                $rows.eq(i).show();
                            }
                        }
                    });
                }

                load();

                $("#tablePaging").css("display", "block");
                //FIN PAGINACIÓN


            } else {
                $("#tbdEval").html("");
                $("#tblResultados").css("display", "none");
                $("#btnAcceder").css("display", "none");
                $("#btnAlterarEstado").css("display", "none");
                $("#btnEliminar").css("display", "none");
                $("#tablePaging").css("display", "none");
                alertNew("warning", "No se encuentran evaluaciones que respondan a los filtros establecidos", null, 2000, null);
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener las evaluaciones");
        }
    });
}

//***Modal selección de profesionales (al hacer click sobre el link de Profesional)
$('#lblEvaluado').on('click', function () {
    $('#modal-evaluados').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-evaluados').modal('show');
    $('#lisEvaluados').children().remove();
    $('#modal-evaluados input[type=text]').val('');

});


//Selección simple de evaluados
$('body').on('click', '#lisEvaluados li', function (e) {
    $('#lisEvaluados li').removeClass('active');
    $(this).addClass('active');
});


//***Modal selección de profesionales (al hacer click sobre el link de Profesional)
$('#lblEvaluador').on('click', function () {
    $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-evaluadores').modal('show');
    $('#lisEvaluadores').children().remove();
    $('#modal-evaluadores input[type=text]').val('');

});

//Botón seleccionar de evaluado
$('#modal-evaluados #btnSeleccionarEvaluados').on('click', function () {
    $evaluado = $('#lisEvaluados li.active');
    if ($evaluado.length > 0) {
        //$('#sProyecto').val($proyecto.text());
        $('#evaluado').attr("idficepi", $evaluado.attr('value')).val($evaluado.text());

        $('#modal-evaluados').modal('hide');

    } else {
        alertNew("warning", "Debes seleccionar un profesional");
    }

});

//Botón cancelar de evaluado
$('#modal-evaluados #btnCancelarEvaluados').on('click', function () {
    $('#modal-evaluados').modal('hide');
});



//Obtiene los evaluados (se llama al hacer intro sobre los filtros (ap1, ap2 o nombre) o al pulsar sobre Obtener)
function ObtenerEvaluados() {
    $('#lisEvaluados').children().remove();
    $lisEvaluados = $('#lisEvaluados');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getFicepi",   // Current Page, Method
        data: JSON.stringify({ t001_apellido1: $('#txtApellido1Evaluados').val(), t001_apellido2: $('#txtApellido2Evaluados').val(), t001_nombre: $('#txtNombreEvaluados').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluados); });
                $('#modal-evaluados').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluados').modal('show');

            } else {
                alertNew("warning", "No se encuentran evaluados que respondan al filtro establecido");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluados");
        }
    });
}



//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluados :input[type=text]').on('keyup', function (event) {

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#lisEvaluadores li").length > 0) {
        $("#lisEvaluadores").html("");
    }

    if (event.keyCode == 13) {
        ObtenerEvaluados();
    }
});

//Al pulsar sobre el botón Obtener
$('#btnObtenerEvaluados').on('click', function () {
    ObtenerEvaluados();
});

//Poner el foco en la caja de texto del apellido1
$('#modal-evaluados').on('shown.bs.modal', function () {
    $('#txtApellido1Evaluados').focus();
})


//Selección simple de evaluador
$('body').on('click', '#lisEvaluadores li', function (e) {
    $('#lisEvaluadores li').removeClass('active');
    $(this).addClass('active');
});

//Botón seleccionar de evaluador
$('#modal-evaluadores #btnSeleccionar').on('click', function () {
    $evaluador = $('#lisEvaluadores li.active');
    if ($evaluador.length > 0) {        
        $('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
        $('#modal-evaluadores').modal('hide');
        $("#imgEvaluador").css("display", "inline-block");

    } else {
        alertNew("warning", "Debes seleccionar un profesional");
    }

});

//Botón cancelar de evaluador
$('#modal-evaluadores #btnCancelar').on('click', function () {
    $('#modal-evaluadores').modal('hide');
});

//Obtiene los evaluadores (se llama al hacer intro sobre los filtros (ap1, ap2 o nombre) o al pulsar sobre Obtener)
function ObtenerEvaluadores() {
    $('#lisEvaluadores').children().remove();
    $lisEvaluadores = $('#lisEvaluadores');
    actualizarSession();

    //Pasar el valor del radio button figura ($('input[name=rdbFigura]:checked').val())

    $.ajax({
        url: "Default.aspx/getDescendientes",   // Current Page, Method
        data: JSON.stringify({ t001_idficepi: idficepi, perfilApl: entrada, t001_apellido1: $('#txtApellido1').val(), t001_apellido2: $('#txtApellido2').val(), t001_nombre: $('#txtNombre').val(), figura: $('input[name=rdbFigura]:checked').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });
                $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluadores').modal('show');

            } else {

                if ($('input[name=rdbFigura]:checked').val() == "0")
                    alertNew("warning", "No se encuentran evaluados bajo su ámbito de visión que, respondiendo al filtro establecido, tengan evaluaciones realizadas.", null, 5000, null);
                else
                    alertNew("warning", "No se encuentran evaluadores bajo su ámbito de visión que, respondiendo al filtro establecido, tengan evaluaciones realizadas.", null, 5000, null);
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluadores");
        }
    });
}



//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluadores :input[type=text]').on('keyup', function (event) {
    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#lisEvaluadores li").length > 0) {
        $("#lisEvaluadores").html("");
    }
    if (event.keyCode == 13) {
        ObtenerEvaluadores();
    }
});

//Al pulsar sobre el botón Obtener
$('#btnObtener').on('click', function () {
    ObtenerEvaluadores();
});

//Poner el foco en la caja de texto del apellido1
$('#modal-evaluadores').on('shown.bs.modal', function () {
    $('#txtApellido1').focus();
})
//***Fin Modal selección de profesionales

//***Modal selección de CR (al hacer click sobre el link de CR)
$('#lblCR').on('click', function () {
    $('#modal-CR').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-CR').modal('show');
    $('#lisCR').children().remove();
    $('#modal-CR input[type=text]').val('');
    ObtenerCR();

});

//Selección simple de CR
$('body').on('click', '#lisCR li', function (e) {
    $('#lisCR li').removeClass('active');
    $(this).addClass('active');
});

//$('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
//Botón seleccionar de CR
$('#modal-CR #btnSeleccionarCR').on('click', function () {
    $CR = $('#lisCR li.active');
    if ($CR.length > 0) {        
        $('#txtCR').val($CR.text());
        $('#modal-CR').modal('hide');
        $("#imgCR").css("display", "inline-block");
    } else {
        alertNew("warning", "Debes seleccionar un CR");
    }

});

//Botón cancelar de CR
$('#modal-CR #btnCancelarCR').on('click', function () {
    $('#modal-CR').modal('hide');
});

//Obtiene los CR (se llama al hacer intro sobre el filtro (nombre) o al pulsar sobre Obtener)
function ObtenerCR() {
    $('#lisCR').children().remove();
    $lisCR = $('#lisCR');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getCR",   // Current Page, Method
        data: JSON.stringify(),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item'>" + this + "</li>").appendTo($lisCR); });
                $('#modal-CR').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-CR').modal('show');

            } else {
                alertNew("warning", "No existen CR");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los CR");
        }
    });
}

//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-CR :input[type=text]').on('keyup', function (event) {
    if (event.keyCode == 13) {
        ObtenerCR();
    }
});

//Al pulsar sobre el botón Obtener
$('#btnObtenerCR').on('click', function () {
    ObtenerCR();
});

//Poner el foco en la caja de texto del nombre
$('#modal-CR').on('shown.bs.modal', function () {
    $('#txtNombreCR').focus();
})
//***Fin Modal selección de CR


//***Modal selección de Rol (al hacer click sobre el link de Rol)
$('#lblRol').on('click', function () {
    $('#modal-Rol').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-Rol').modal('show');
    $('#lisRol').children().remove();
    $('#modal-Rol input[type=text]').val('');
    ObtenerRol();
});

//Selección simple de CR
$('body').on('click', '#lisRol li', function (e) {
    $('#lisRol li').removeClass('active');
    $(this).addClass('active');
});

//$('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
//Botón seleccionar de CR
$('#modal-Rol #btnSeleccionarRol').on('click', function () {
    $Rol = $('#lisRol li.active');
    if ($Rol.length > 0) {        
        $('#txtRol').val($Rol.text());
        $('#modal-Rol').modal('hide');
        $("#imgRol").css("display", "inline-block");

    } else {
        alertNew("warning", "Debes seleccionar un rol");
    }

});

//Botón cancelar de CR
$('#modal-Rol #btnCancelarRol').on('click', function () {
    $('#modal-Rol').modal('hide');
});

//Obtiene los CR (se llama al hacer intro sobre el filtro (nombre) o al pulsar sobre Obtener)
function ObtenerRol() {
    $('#lisRol').children().remove();
    $lisRol = $('#lisRol');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getRol",   // Current Page, Method
        data: JSON.stringify(),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () {
                    $("<li class='list-group-item'>" + this+ "</li>").appendTo($lisRol);
                });
                $('#modal-Rol').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-Rol').modal('show');

            } else {
                alertNew("warning", "No existen roles");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los roles");
        }
    });
}


//Buscar
$('[name="SearchDualList"]').on('keyup', function (e) {
    var code = e.keyCode || e.which;
    if (code == '9') return;
    if (code == '27') $(this).val(null);
    var $rows = $(this).closest('.dual-list').find('.list-group li');
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
    $rows.show().filter(function () {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
});

//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-Rol :input[type=text]').on('keyup', function (event) {
    if (event.keyCode == 13) {
        ObtenerRol();
    }
});

//Al pulsar sobre el botón Obtener
$('#btnObtenerRol').on('click', function () {
    ObtenerRol();
});

//Poner el foco en la caja de texto del nombre
$('#modal-Rol').on('shown.bs.modal', function () {
    $('#txtNombreRol').focus();
})
//***Fin Modal selección de CR



/*LIMPIAR FILTROS*/
$("#limpiarFiltros, #limpiarFiltros2").on("click", function () {
    limpiarFiltros();
    //REVISAR ESTA Línea
    //$("#divFiltros").hide().css("height","auto").addClass("in").fadeIn("slow");
    $("#divFiltros").addClass("in").addClass("animation");
    $("#linkFiltros").text("Replegar filtros de búsqueda");
    //setTimeout(function () {
    //    $('#divFiltros').addClass('in');
    //}, 600);
    
    //$("#divFiltros").fadeIn("slow");
    $("#divFiltros").css("height", "auto");
    $("#tablePaging").css("display", "none");
})




function limpiarFiltros() {
    
    /*SECCIÓN PROFESIONALES*/
    filtrosConsultas = "";

    $("#tbdEval").html("");
    $("#tblResultados").css("display", "none");
    $("#btnAcceder").css("display", "none");
    $("#btnAlterarEstado").css("display", "none");
    $("#btnEliminar").css("display", "none");
    $("#tablePaging").css("display", "none");

    //$("#rdbEvaluado").prop("checked", true);
    $("#rdbEvaluado").prop("checked", true);
    $("#divNivel").css("display", "none");
    $("#cboProfundizacion").val("");


    $('input#evaluado').val("");


    switch (entrada.toUpperCase()) {
        case "EVA":           
            $('input#evaluador').attr('idficepi', idficepi.toString());
            $('input#evaluador').val(nombre);
            break;

        case "ADM":
            $('input#evaluador').attr('idficepi', "");
            $('input#evaluador').val("");
            break;
    }

    //$('input#evaluador').attr('idficepi', idficepi.toString());
    //$('input#evaluador').val(nombre);
       
    $(cboProfundizacion).val("1");   
    /*FIN SECCIÓN PROFESIONALES*/


    /*SECCIÓN EVALUACIONES*/
    //Se obtiene la fecha actual
    var currentDate = new Date()
    var day = currentDate.getDate();
    var month = currentDate.getMonth() + 1;
    var year = currentDate.getFullYear();

    

    $("#selMesIni").val("01");
    $("#selAnoIni").val(anoMin);

    if (month.toString().length == 1) {
        $("#selMesFin").val("0" + month);
    }
    else {
        $("#selMesFin").val(month);
    } 
    
    $("#selAnoFin").val(year);
    
    $('#txtCR').val("");
    $('#txtRol').val("");
    $(cboColectivo).val("");
    $(cboCalidad).val("");
    $(cboSituacion).val("");
    /*FIN SECCIÓN EVALUACIONES*/


    /*SECCIÓN FORMULARIO ESTÁNDAR*/
    $("#cboEstReconocer").val("");
    $("#cboEstMejorar").val("");
       
    /*Gestión de relación de clientes*/
    $("#chkEstGesCli1").prop('checked', false);
    $("#chkEstGesCli2").prop('checked', false);
    $("#chkEstGesCli3").prop('checked', false);
    $("#chkEstGesCli4").prop('checked', false);


    /*Liderazgo/Gestión de equipos*/
    $("#chkEstLiderazgo1").prop('checked', false);
    $("#chkEstLiderazgo2").prop('checked', false);
    $("#chkEstLiderazgo3").prop('checked', false);
    $("#chkEstLiderazgo4").prop('checked', false);
    
    /*Planificación/Organización*/
    $("#chkEstPlanOrga1").prop('checked', false);
    $("#chkEstPlanOrga2").prop('checked', false);
    $("#chkEstPlanOrga3").prop('checked', false);
    $("#chkEstPlanOrga4").prop('checked', false);

    /*Expertise/Técnico*/
    $("#chkEstExpTecnico1").prop('checked', false);
    $("#chkEstExpTecnico2").prop('checked', false);
    $("#chkEstExpTecnico3").prop('checked', false);
    $("#chkEstExpTecnico4").prop('checked', false);

    /*Cooperación*/
    $("#chkEstCooperacion1").prop('checked', false);
    $("#chkEstCooperacion2").prop('checked', false);
    $("#chkEstCooperacion3").prop('checked', false);
    $("#chkEstCooperacion4").prop('checked', false);

    /*Iniciativa*/
    $("#chkEstIniciativa1").prop('checked', false);
    $("#chkEstIniciativa2").prop('checked', false);
    $("#chkEstIniciativa3").prop('checked', false);
    $("#chkEstIniciativa4").prop('checked', false);

    /*Perseverancia*/
    $("#chkEstPerseverancia1").prop('checked', false);
    $("#chkEstPerseverancia2").prop('checked', false);
    $("#chkEstPerseverancia3").prop('checked', false);
    $("#chkEstPerseverancia4").prop('checked', false);

    /*Más de X aspectos (a mejorar)*/
    $("#selectMejorar").val("");

    /*Más de X aspectos (Suficiente)*/
    $("#selectSuficiente").val("");
    
    /*Más de X aspectos (Bueno)*/
    $("#selectBueno").val("");
    
    /*Más de X aspectos (Alto)*/
    $("#selectAlto").val("");

    
    /*Intereses de carrera (Seguir la línea actual. Refuerzo del rol actual)*/    
    $("#chkEvolucion1").prop('checked', false);

    /*Intereses de carrera (Evolucionar hacia función)*/
    $("#rdbEvolucion2").prop('checked', false);
    $("#rdbEvolucion3").prop('checked', false);
    $("#rdbEvolucion4").prop('checked', false);
    $("#rdbEvolucion5").prop('checked', false);

    /*Intereses de carrera (Interesado en trayectoria internacional)*/
    $("#rdbEvolucion7").prop('checked', false);
    
    /*Intereses de carrera (Otros intereses profesionales (cambio de proyecto, geografía, tecnología, etc.))*/
    $("#rdbEvolucion6").prop('checked', false);    
    /*FIN Intereses de carrera*/

    /*FIN SECCIÓN FORMULARIO ESTÁNDAR*/


    /*SECCIÓN FORMULARIO CAU*/

    /*Aspectos a mejorar*/
    $("#cboCAUMejorar").val("");
    
    /*Orientación al cliente*/
    $("#chkCAUGesCli1").prop('checked', false);
    $("#chkCAUGesCli2").prop('checked', false);
    $("#chkCAUGesCli3").prop('checked', false);
    $("#chkCAUGesCli4").prop('checked', false);
    
    /*Orientación a resultados*/
    $("#chkCAULiderazgo1").prop('checked', false);
    $("#chkCAULiderazgo2").prop('checked', false);
    $("#chkCAULiderazgo3").prop('checked', false);
    $("#chkCAULiderazgo4").prop('checked', false);

    /*Comunicación*/
    $("#chkCAUPlanOrga1").prop('checked', false);
    $("#chkCAUPlanOrga2").prop('checked', false);
    $("#chkCAUPlanOrga3").prop('checked', false);
    $("#chkCAUPlanOrga4").prop('checked', false);

    /*Compromiso*/
    $("#chkCAUExpTecnico1").prop('checked', false);
    $("#chkCAUExpTecnico2").prop('checked', false);
    $("#chkCAUExpTecnico3").prop('checked', false);
    $("#chkCAUExpTecnico4").prop('checked', false);

    /*Iniciativa*/
    $("chkCAUIniciativa1").prop('checked', false);
    $("chkCAUIniciativa2").prop('checked', false);
    $("chkCAUIniciativa3").prop('checked', false);
    $("chkCAUIniciativa4").prop('checked', false);

    /*Perseverancia*/
    $("chkCAUPerseverancia1").prop('checked', false);
    $("chkCAUPerseverancia2").prop('checked', false);
    $("chkCAUPerseverancia3").prop('checked', false);
    $("chkCAUPerseverancia4").prop('checked', false);

    /*Más de X aspectos (a mejorar)*/
    $("#selectMejorarCAU").val("");

    /*Más de X aspectos (a mejorar)*/
    $("#selectSuficienteCAU").val("");
    


    /*FIN SECCIÓN FORMULARIO CAU*/

}


function ActivarFormularios(origen, listaformularios) {

    if (origen == "ADM") {
        $("#tblFiltros3, #tblFiltros4").css("display", "block");
        return;
    }

    for (var i = 0; i < listaformularios.length; i++) {
        switch (listaformularios[i]) {
            case 1:
                $("#tblFiltros3").css("display", "block");
                break;

            case 2:
                $("#tblFiltros4").css("display", "block");
                break;
        }
    }

    return;

}


function setFiltrosConsultas() {
    var $filtrosConsultas = JSON.parse(filtrosConsultas);

    //SECCIÓN PROFESIONALES
    
    //Selección Evaluado / Evaluador
    //Caso Evaluado
    
    //TODO REVISAR CAMBIO DE FORMULARIO TAU
    if ($filtrosConsultas.t001_idficepi != null) {
        $("#rdbEvaluado").prop("checked", true);
        $('input#evaluador').attr('idficepi', $filtrosConsultas.t001_idficepi);

    }
   
    //Caso Evaluador
    if ($filtrosConsultas.t001_idficepi_evaluador != null) {
        $("#rdbEvaluador").prop("checked", true);
        $("#divNivel").css("display", "block");
        $("#cboProfundizacion").val($filtrosConsultas.profundidad);
        $('input#evaluador').attr('idficepi', $filtrosConsultas.t001_idficepi_evaluador);
    }
   


    $("#evaluador").val($filtrosConsultas.txtNombre);
    //FIN SECCIÓN PROFESIONALES    


    //SECCIÓN EVALUACIONES

    //FECHAS
    var selAnoIni = $filtrosConsultas.desde.toString().substring(0, 4);
    var selMesIni = $filtrosConsultas.desde.toString().substring(4, 6);

    $('#selAnoIni').val(selAnoIni);
    $('#selMesIni').val(selMesIni);

    var selAnoFin = $filtrosConsultas.hasta.toString().substring(0, 4);
    var selMesFin = $filtrosConsultas.hasta.toString().substring(4, 6);

    $('#selAnoFin').val(selAnoFin);
    $('#selMesFin').val(selMesFin);

    
    //ESTADO
    $("#cboSituacion").val($filtrosConsultas.estado);
    $("#cboColectivo").val($filtrosConsultas.t941_idcolectivo);
    $("#cboCalidad").val($filtrosConsultas.t930_puntuacion);

    //CR
    $("#txtCR").val($filtrosConsultas.t930_denominacionCR);

    //ROL
    $("#txtRol").val($filtrosConsultas.t930_denominacionROL);
    
    
    //FIN SECCIÓN EVALUACIONES


    //FORMULARIO ESTANDAR
    //Gestión de relación de clientes
    for (var i = 0; i < $filtrosConsultas.lestt930_gescli.length; i++) {

        switch ($filtrosConsultas.lestt930_gescli[i]) {
            case 1:
                $("#chkEstGesCli1").prop("checked", true);
                break;
            case 2:
                $("#chkEstGesCli2").prop("checked", true);
                break;
            case 3:
                $("#chkEstGesCli3").prop("checked", true);
                break;
            case 4:
                $("#chkEstGesCli4").prop("checked", true);
                break;
        }

    }
    

    //Liderazgo/Gestión de equipos
    for (var i = 0; i < $filtrosConsultas.lestt930_liderazgo.length; i++) {

        switch ($filtrosConsultas.lestt930_liderazgo[i]) {
            case 1:
                $("#chkEstLiderazgo1").prop("checked", true);
                break;
            case 2:
                $("#chkEstLiderazgo2").prop("checked", true);
                break;
            case 3:
                $("#chkEstLiderazgo3").prop("checked", true);
                break;
            case 4:
                $("#chkEstLiderazgo4").prop("checked", true);
                break;
        }

    }

    //Planificación/Organización
    for (var i = 0; i < $filtrosConsultas.lestt930_planorga.length; i++) {

        switch ($filtrosConsultas.lestt930_planorga[i]) {
            case 1:
                $("#chkEstPlanOrga1").prop("checked", true);
                break;
            case 2:
                $("#chkEstPlanOrga2").prop("checked", true);
                break;
            case 3:
                $("#chkEstPlanOrga3").prop("checked", true);
                break;
            case 4:
                $("#chkEstPlanOrga4").prop("checked", true);
                break;
        }

    }

    //Expertise/Técnico
    for (var i = 0; i < $filtrosConsultas.lestt930_exptecnico.length; i++) {

        switch ($filtrosConsultas.lestt930_exptecnico[i]) {
            case 1:
                $("#chkEstExpTecnico1").prop("checked", true);
                break;
            case 2:
                $("#chkEstExpTecnico2").prop("checked", true);
                break;
            case 3:
                $("#chkEstExpTecnico3").prop("checked", true);
                break;
            case 4:
                $("#chkEstExpTecnico4").prop("checked", true);
                break;
        }

    }

    //Cooperación
    for (var i = 0; i < $filtrosConsultas.lestt930_cooperacion.length; i++) {

        switch ($filtrosConsultas.lestt930_cooperacion[i]) {
            case 1:
                $("#chkEstCooperacion1").prop("checked", true);
                break;
            case 2:
                $("#chkEstCooperacion2").prop("checked", true);
                break;
            case 3:
                $("#chkEstCooperacion3").prop("checked", true);
                break;
            case 4:
                $("#chkEstCooperacion4").prop("checked", true);
                break;
        }

    }
  
    //Iniciativa
    for (var i = 0; i < $filtrosConsultas.lestt930_iniciativa.length; i++) {

        switch ($filtrosConsultas.lestt930_iniciativa[i]) {
            case 1:
                $("#chkEstIniciativa1").prop("checked", true);
                break;
            case 2:
                $("#chkEstIniciativa2").prop("checked", true);
                break;
            case 3:
                $("#chkEstIniciativa3").prop("checked", true);
                break;
            case 4:
                $("#chkEstIniciativa4").prop("checked", true);
                break;
        }

    }
   
    //Perseverancia
    for (var i = 0; i < $filtrosConsultas.lestt930_perseverancia.length; i++) {

        switch ($filtrosConsultas.lestt930_perseverancia[i]) {
            case 1:
                $("#chkEstPerseverancia1").prop("checked", true);
                break;
            case 2:
                $("#chkEstPerseverancia2").prop("checked", true);
                break;
            case 3:
                $("#chkEstPerseverancia3").prop("checked", true);
                break;
            case 4:
                $("#chkEstPerseverancia4").prop("checked", true);
                break;
        }

    }

 
    $("#cboEstReconocer").val($filtrosConsultas.estaspectos);
    $("#cboEstMejorar").val($filtrosConsultas.estmejorar);
   

    if (entrada == "ADM") {
        $("#selectMejorar").val($filtrosConsultas.SelectMejorar);
        $("#selectSuficiente").val($filtrosConsultas.SelectSuficiente);
        $("#selectBueno").val($filtrosConsultas.SelectBueno);
        $("#selectAlto").val($filtrosConsultas.SelectAlto);

        $("#selectMejorarCAU").val($filtrosConsultas.SelectMejorarCAU);
        $("#selectSuficienteCAU").val($filtrosConsultas.SelectSuficienteCAU);
        $("#selectBuenoCAU").val($filtrosConsultas.SelectBuenoCAU);
        $("#selectAltoCAU").val($filtrosConsultas.SelectAltoCAU);

    }
   
    //Intereses de carrera


    for (var i = 0; i < $filtrosConsultas.lestt930_interesescar.length; i++) {

        switch ($filtrosConsultas.lestt930_interesescar[i]) {
            case 1:
                $("#chkEvolucion1").prop("checked", true);
                break;
            case 2:
                $("#rdbEvolucion2").prop("checked", true);
                break;
            case 3:
                $("#rdbEvolucion3").prop("checked", true);
                break;
            case 4:
                $("#rdbEvolucion4").prop("checked", true);
                break;
            case 5:
                $("#rdbEvolucion5").prop("checked", true);
                break;
            case 6:
                $("#rdbEvolucion6").prop("checked", true);
                break;
            case 7:
                $("#rdbEvolucion7").prop("checked", true);
                break;
        }

    }


    //FORMULARIO CAU


    //Orientación al cliente
    for (var i = 0; i < $filtrosConsultas.lcaut930_gescli.length; i++) {

        switch ($filtrosConsultas.lcaut930_gescli[i]) {
            case 1:
                $("#chkCAUGesCli1").prop("checked", true);
                break;
            case 2:
                $("#chkCAUGesCli2").prop("checked", true);
                break;
            case 3:
                $("#chkCAUGesCli3").prop("checked", true);
                break;
            case 4:
                $("#chkCAUGesCli4").prop("checked", true);
                break;
        }

    }


    //Orientación a resultados
    for (var i = 0; i < $filtrosConsultas.lcaut930_liderazgo.length; i++) {

        switch ($filtrosConsultas.lcaut930_liderazgo[i]) {
            case 1:
                $("#chkCAULiderazgo1").prop("checked", true);
                break;
            case 2:
                $("#chkCAULiderazgo2").prop("checked", true);
                break;
            case 3:
                $("#chkCAULiderazgo3").prop("checked", true);
                break;
            case 4:
                $("#chkCAULiderazgo4").prop("checked", true);
                break;
        }

    }

    //Comunicación
    for (var i = 0; i < $filtrosConsultas.lcaut930_planorga.length; i++) {

        switch ($filtrosConsultas.lcaut930_planorga[i]) {
            case 1:
                $("#chkCAUPlanOrga1").prop("checked", true);
                break;
            case 2:
                $("#chkCAUPlanOrga2").prop("checked", true);
                break;
            case 3:
                $("#chkCAUPlanOrga3").prop("checked", true);
                break;
            case 4:
                $("#chkCAUPlanOrga4").prop("checked", true);
                break;
        }

    }

    //Comunicación
    for (var i = 0; i < $filtrosConsultas.lcaut930_exptecnico.length; i++) {

        switch ($filtrosConsultas.lcaut930_exptecnico[i]) {
            case 1:
                $("#chkEstCAUTecnico1").prop("checked", true);
                break;
            case 2:
                $("#chkEstCAUTecnico2").prop("checked", true);
                break;
            case 3:
                $("#chkEstCAUTecnico3").prop("checked", true);
                break;
            case 4:
                $("#chkEstCAUTecnico4").prop("checked", true);
                break;
        }

    }

    //Compromiso
    for (var i = 0; i < $filtrosConsultas.lcaut930_cooperacion.length; i++) {

        switch ($filtrosConsultas.lcaut930_cooperacion[i]) {
            case 1:
                $("#chkCAUCooperacion1").prop("checked", true);
                break;
            case 2:
                $("#chkCAUCooperacion2").prop("checked", true);
                break;
            case 3:
                $("#chkCAUCooperacion3").prop("checked", true);
                break;
            case 4:
                $("#chkCAUCooperacion4").prop("checked", true);
                break;
        }

    }

    //Cooperación
    for (var i = 0; i < $filtrosConsultas.lcaut930_iniciativa.length; i++) {

        switch ($filtrosConsultas.lcaut930_iniciativa[i]) {
            case 1:
                $("#chkCAUIniciativa1").prop("checked", true);
                break;
            case 2:
                $("#chkCAUIniciativa2").prop("checked", true);
                break;
            case 3:
                $("#chkCAUIniciativa3").prop("checked", true);
                break;
            case 4:
                $("#chkCAUIniciativa3").prop("checked", true);
                break;
        }

    }
   
    for (var i = 0; i < $filtrosConsultas.lcaut930_perseverancia.length; i++) {

        switch ($filtrosConsultas.lcaut930_perseverancia[i]) {
            case 1:
                $("#chkCAUPerseverancia1").prop("checked", true);
                break;
            case 2:
                $("#chkCAUPerseverancia2").prop("checked", true);
                break;
            case 3:
                $("#chkCAUPerseverancia3").prop("checked", true);
                break;
            case 4:
                $("#chkCAUPerseverancia4").prop("checked", true);
                break;
        }

    }
   
    for (var i = 0; i < $filtrosConsultas.lcaut930_interesescar.length; i++) {

        switch ($filtrosConsultas.lcaut930_interesescar[i]) {
            case 1:
                $("#chkEvolucionCAU1").prop("checked", true);
                break;
            case 2:
                $("#chkEvolucionCAU2").prop("checked", true);
                break;
            case 3:
                $("#chkEvolucionCAU3").prop("checked", true);
                break;
           
        }

    }

    $("#cboCAUMejorar").val($filtrosConsultas.caumejorar);

}


//Limpiamos tabla resultados cuando tocamos cualquier control
$("#divFiltros input, #divFiltros select").on("change", function () {    
    $("#tbdEval").html("");
    $("#tblResultados").css("display", "none");
    $("#btnAcceder").css("display", "none");
    $("#btnAlterarEstado").css("display", "none");
    $("#btnEliminar").css("display", "none");

    filtrosConsultas = "";
    $("#tablePaging").css("display", "none");
});

//$(".glyphicon.glyphicon-repeat").on("click", function () {
//    $("#tbdEval").html("");
//    $("#tblResultados").css("display", "none");
//    $("#btnAcceder").css("display", "none");
//    filtrosConsultas = "";
//    $("#tablePaging").css("display", "none");
//})


$("#btnEliminar").on("click", function () {

    $active = $('#tbdEval tr.active');
    if ($active.length == 0) {
        alertNew("warning", "Tienes que seleccionar alguna evaluación");
        return;
    }

    $('#modal-eliminar').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $("#modal-eliminar").modal("show");
})


$("#btnSiEliminacion").on("click", function () {
    $active = $('#tbdEval tr.active');

    $.ajax({
        url: "Default.aspx/eliminar",   // Current Page, Method
        data: JSON.stringify({ t930_idvaloracion: $active.attr("idvaloracion"), estado: $active.attr("codestado"), correoevaluador: $active.attr("data-correoevaluador"), correoevaluado: $active.attr("data-correoevaluado"), nombreyapellidoevaluador: $active.attr("data-nombreyapellidoevaluador"), nombreyapellidoevaluado: $active.attr("data-nombreyapellidoevaluado"), nombrecortoevaluador: $active.attr("data-nombrecortoevaluador"), nombrecortoevaluado: $active.attr("data-nombrecortoevaluado"), fecapertura: $active.attr("data-fecapertura"), feccierre: $active.attr("data-feccierre") }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-eliminar").modal("hide");                        
            MostrarEvaluaciones();
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar eliminar la evaluación.");
        }
    });
})

$("#btnNoEliminacion").on("click", function () {
    $("#modal-eliminar").modal("hide");
})

