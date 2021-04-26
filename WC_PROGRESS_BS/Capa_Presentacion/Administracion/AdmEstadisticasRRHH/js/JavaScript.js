//RequestFilter: variable donde se almacenarán los filtros
var rf;
var rfParamsRPT;

$(document).ready(function () {
    comprobarerrores();

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".list-group").css("max-height", "320px");
        }
    }

    //Se obtiene la fecha actual
    var currentDate = new Date()
    var day = currentDate.getDate();
    var month = currentDate.getMonth() + 1;
    var year = currentDate.getFullYear();

    //Fecha antigüedad (Mostramos días en el calendario)
    $("#txtFantiguedad, #txtFantiguedadProgress").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        yearRange: '1973:' + year,
        
        beforeShow: function (input, inst) {
            $(inst.dpDiv).removeClass('calendar-off');
        }
    });

  

    

    //Por defecto, la fecha de antigüedad es la fecha actual
    $("#txtFantiguedad, #txtFantiguedadProgress").datepicker({
        dateFormat: 'dd/mm/yy'
    }).datepicker('setDate', defectoAntiguedad)

    //Comento estas líneas, porque por defecto no debe aparecer ningún evaluador.
    //$('input#evaluador').attr('idficepi', idficepi.toString());
    //$('input#evaluador').attr('data-sexo', sexo.toString());
    //$('input#evaluador').val(nombre);

    ////Traducción al español del datepicker
    $(function ($) {
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '<Ant',
            nextText: 'Sig>',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
    });
    //FIN DATEPICKER

    //Llamada ajax para traer las estadísticas al cargar la página
    cargarEstadisticas();

    if ($('input#CR').val() != "") {
        $("#imgCREvaluacion").css("display", "inline-block");
    };

    if ($('input#evaluador').val() != "") {
        $("#imgEvaluadorColectivoProgress").css("display", "inline-block");
    };


    $('[data-toggle="popover"]').popover({
        placement: 'auto',
        trigger: 'hover',
        html: true
    });
})


//Obtiene las estadísticas con los filtros seleccionados
function cargarEstadisticas() {
    //Se limpian las tablas
    $("#tblProfesionales").html(null);
    $("#tblEvaluaciones").html(null);
    $("#tblEvaluadores").html(null);
    $("#tblProfesionalesEvaluadores").html(null);
    $("#tblColectivoProgress").html(null);
    
    //Se comprueba que los combos Desde y Hasta seleccionados tienen un periodo lógico
    if (!comprobarfechas()) return;
    if (!comprobarfechasColectivos()) return;

    //obtiene los criterios de filtrado y los deja en rf
    rf = getRequestFilter();
    rfParamsRPT = getRequestParamsRPT();

    //Se llama al servidor (cargarEstadisticas) con los filtros seleccionados
    actualizarSession();
    $.ajax({
        url: "Default.aspx/cargarEstadisticas",
        "data": JSON.stringify({ requestFilter: rf, requestParamsRPT: rfParamsRPT }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            //Hay que parsear el resultado JSON que devuelve el servidor
            pintarDatosPantalla(JSON.parse(result.d));
        },
        error: function (ex, status) {
            error$ajax("Ocurrió un error obteniendo las estadísticas", ex, status);
        }
    });
}

//Obtiene un objecto con los criterios de filtrado.
function getRequestFilter() {
    var rf = new doEstadisticas();

    //EVALUACIONES
    rf.Desde = parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val());
    rf.Hasta = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());
    
    var sfecha = $('input#txtFantiguedad').val();
    var sd = sfecha.substring(0, 2);
    var sm = sfecha.substring(3, 5);
    var sa = sfecha.substring(6, 10);

    var fAntiguedad = new Date(sa, sm - 1, sd);

    rf.t001_fecantigu = fAntiguedad;
    rf.t941_idcolectivo = $(cboColectivo).val();
    rf.estado = $("#cboEstado").val();

    //DENOMINACIÓN DEL CR
    rf.t930_denominacionCR = $('input#CR').val();

    //EVALUADORES
    rf.t303_idnodo_evaluadores = $("#CREvaluadores").attr("idnodo"); //int Nullable

    //COLECTIVOS
    rf.DesdeColectivos = parseInt($('#SelAnoIniProgress').val()) * 100 + parseInt($('#SelMesIniProgress').val());
    rf.HastaColectivos = parseInt($('#SelAnoFinProgress').val()) * 100 + parseInt($('#SelMesFinProgress').val());


    var sfechaColectivos = $('input#txtFantiguedadProgress').val();
    var sdColectivos = sfechaColectivos.substring(0, 2);
    var smColectivos = sfechaColectivos.substring(3, 5);
    var saColectivos = sfechaColectivos.substring(6, 10);

    var fAntiguedadColectivos = new Date(saColectivos, smColectivos - 1, sdColectivos);

    rf.t001_fecantiguColectivos = fAntiguedadColectivos;

    rf.t303_idnodo_colectivos = $("input#CRColectivos").attr("idnodo"); //int Nullable
    rf.t941_idcolectivo_colectivos = $(cboColectivoProgress).val();
    
    rf.t001_idficepi = $('input#evaluador').attr('idficepi');
    
    return rf;
}

function comprobarfechas() {
    try {
        $anoIni = $('#selAnoIni');
        $mesIni = $('#selMesIni');
        $anoFin = $('#selAnoFin');
        $mesFin = $('#selMesFin');

        fecIni = parseInt($anoIni.val()) * 100 + parseInt($mesIni.val());
        fecFin = parseInt($anoFin.val()) * 100 + parseInt($mesFin.val());

        if (fecIni > fecFin) {
            alertNew('warning', 'Periodo seleccionado no lógico.');
            return false;
        } else
            return true;

        


    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar las fechas.");
    }
}

function comprobarfechasColectivos() {
    try {

        //FECHAS COLECTIVOS
        $anoIniProgress = $('#SelAnoIniProgress');
        $mesIniProgress = $('#SelMesIniProgress ');
        $anoFinProgress = $('#SelAnoFinProgress');
        $mesFinProgress = $('#SelMesFinProgress ');

        fecIniProgress = parseInt($anoIniProgress.val()) * 100 + parseInt($mesIniProgress.val());
        fecFinProgress = parseInt($anoFinProgress.val()) * 100 + parseInt($mesFinProgress.val());

        if (fecIniProgress > fecFinProgress) {
            alertNew('warning', 'Periodo seleccionado no lógico.');
            return false;
        } else
            return true;

    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar las fechas.");
    }
}
function getRequestParamsRPT() {
    var rfParamsRPT = new doParametrosRPT();

    rfParamsRPT.txtSituacion = $('#cboSituacion').find(":selected").text();
    rfParamsRPT.sexo = $("#evaluador").attr("data-sexo");
    rfParamsRPT.txtEvaluador = $("#evaluador").val();
    rfParamsRPT.txtProfundizacion = $('#cboProfundizacion').find(":selected").text();
    rfParamsRPT.txtColectivo = $('#cboColectivo').find(":selected").text();
    rfParamsRPT.txtColectivoProgress = $('#cboColectivoProgress').find(":selected").text();    
    rfParamsRPT.txtCR_Evaluaciones = $("#CR").val();
    rfParamsRPT.txtCR_Evaluadores = $("#CREvaluadores").val();
    rfParamsRPT.txtCR_Profesionales = $("#CRColectivos").val();
    rfParamsRPT.txtEstado = $('#cboEstado').find(":selected").text();

    return rfParamsRPT;
}

//pinta los datos de la pantalla en base a la estructura JS
function pintarDatosPantalla(datos) {
    try {

        //EVALUACIONES
        var tblEvaluaciones = "<tbody><tr>";

        tblEvaluaciones += "";
        tblEvaluaciones += "<td style='width:100px' class='text-right'><span>" + datos.EvaluacionesABI + "</span>";        
        if (datos.EvaluacionesABI != 0) tblEvaluaciones += " <i style='visibility:visible; margin-left:0' class='fa fa-file-pdf-o' onclick='informes(1)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o' ></i>"; }
        tblEvaluaciones += "</td><td style='width:100px'></td><td style='width:70px'></td>";
        
        tblEvaluaciones += "<td style='width:100px'><span>" + datos.EvaluacionesABIEvaluador + "</span>";
        
        if (datos.EvaluacionesABIEvaluador != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(2)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
        tblEvaluaciones += "</td></tr>";
        
        tblEvaluaciones += "<tr>";
        tblEvaluaciones += "<td style='width:100px' class='text-right'><span>" + datos.EvaluacionesCUR + "</span>";
        
        if (datos.EvaluacionesCUR != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(3)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px'></td><td style='width:100px'></td><td style='width:100px' class='text-right'><span>" + datos.EvaluacionesCUREvaluador + "</span>";
        if (datos.EvaluacionesCUREvaluador != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(4)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
        tblEvaluaciones += "</td></tr>";


        tblEvaluaciones += "<tr>";
        tblEvaluaciones += "<td style='width:100px'>" + datos.Totalevaluacionescerradas + "";
        if (datos.Totalevaluacionescerradas != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(5)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px'>" + datos.Totalevaluacionesvalidas + "";
        if (datos.Totalevaluacionesvalidas != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(6)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px' class='text-right'><span>" + datos.Totalevaluacionesnovalidas + "</span>";
        if (datos.Totalevaluacionesnovalidas != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(7)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px' class='text-right'><span>" + datos.Totalevaluacionescerradasevaluador + "</span>";

        if (datos.Totalevaluacionescerradasevaluador != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(8)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td></tr>";

        tblEvaluaciones += "<tr>";

        tblEvaluaciones += "<td style='width:100px'>" + datos.EvaluacionesCCF + "";
        if (datos.EvaluacionesCCF != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(9)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td>" + datos.EvaluacionesCCFValida + "";
        if (datos.EvaluacionesCCFValida != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(10)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px' class='text-right'><span>" + datos.EvaluacionesCCFNoValida + "</span>";
        if (datos.EvaluacionesCCFNoValida != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(11)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px' class='text-right'><span>" + datos.EvaluacionesCCFEvaluador + "</span>";
        if (datos.EvaluacionesCCFEvaluador != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(12)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td></tr>";

        tblEvaluaciones += "<tr>";
        tblEvaluaciones += "<td style='width:70px'>" + datos.EvaluacionesCSF + "";
            
        if (datos.EvaluacionesCSF != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(13)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
               
        tblEvaluaciones += "</td><td style='width:100px'>" + datos.EvaluacionesCSFValida + "";
        if (datos.EvaluacionesCSFValida != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(14)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px' class='text-right'><span>" + datos.EvaluacionesCSFNoValida + "</span>";
        
        if (datos.EvaluacionesCSFNoValida != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(15)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

            tblEvaluaciones += "</td><td style='width:100px' class='text-right'><span>" + datos.EvaluacionesCSFEvaluador + "</span>";

            if (datos.EvaluacionesCSFEvaluador != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(16)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td></tr>";

        tblEvaluaciones += "<tr>";
        tblEvaluaciones += "<td style='width:100px'>" + datos.Totalevaluaciones + "";

        if (datos.Totalevaluaciones != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(17)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px'>" + datos.Totalevaluacionesvalidas + "";
           
        if (datos.Totalevaluacionesvalidas != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(18)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td>";

        tblEvaluaciones += "<td style='width:100px' class='text-right'><span>" + datos.Totalevaluacionesnovalidas + "</span>";

        if (datos.Totalevaluacionesnovalidas != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(19)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td><td style='width:100px' class='text-right'><span>" + datos.Totalevaluacionesevaluador + "</span>";
        if (datos.Totalevaluacionesevaluador != 0) tblEvaluaciones += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(20)'></i>";
            else { tblEvaluaciones += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluaciones += "</td></tr>";
        
        tblEvaluaciones += "</tbody>";


        //EVALUADORES
        var tblEvaluadores = "<tbody><tr>";
        tblEvaluadores += "<td class='text-right'>" + datos.Evaluadoressinconfirmarequipo + "";
        if (datos.Evaluadoressinconfirmarequipo != 0) tblEvaluadores += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(21)'></i>";
            else { tblEvaluadores += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluadores += "</td></tr>";

        tblEvaluadores += "<tr>";
        tblEvaluadores += "<td class='text-right'>" + datos.Evaluadoresequipoconfirmado + "";
        if (datos.Evaluadoresequipoconfirmado != 0) tblEvaluadores += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(22)'></i>";
            else { tblEvaluadores += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
            
        tblEvaluadores += "</td></tr>";

        tblEvaluadores += "<tr><td>" + datos.Totalevaluadoresevaluadores + "";
        if (datos.Totalevaluadoresevaluadores != 0) tblEvaluadores += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(23)'></i>";
            else { tblEvaluadores += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluadores += "</td></tr>";
        
        tblEvaluadores += "<tr>";
        tblEvaluadores += "<td class='text-right'>" + datos.Profesionalessinevaluador + "";
        if (datos.Profesionalessinevaluador != 0) tblEvaluadores += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(24)'></i>";
        else { tblEvaluadores += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblEvaluadores += "</td></tr></tbody>";    
       
        //COLECTIVO PROGRESS
        var tblColectivoProgress = "<tbody><tr>";
        tblColectivoProgress += "<td class='text-right'>" + datos.ProfesionalesABI + "";
        if (datos.ProfesionalesABI != 0) tblColectivoProgress += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(25)'></i>";
            else { tblColectivoProgress += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
            
        tblColectivoProgress += "</td></tr>";
      
        tblColectivoProgress += "<tr>";
        tblColectivoProgress += "<td class='text-right'>" + datos.ProfesionalesCUR + "";
        if (datos.ProfesionalesCUR != 0) tblColectivoProgress += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(26)'></i>";
            else { tblColectivoProgress += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblColectivoProgress += "</td></tr>";

        tblColectivoProgress += "<tr>";
        tblColectivoProgress += "<td class='text-right'>" + datos.ProfesionalesCER + "";
        if (datos.ProfesionalesCER != 0) tblColectivoProgress += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(27)'></i>";
            else { tblColectivoProgress += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
        tblColectivoProgress += "</td></tr>";

        tblColectivoProgress += "<tr>";
        tblColectivoProgress += "<td class='text-right'>" + datos.ProfesionalesCCF + "";
        if (datos.ProfesionalesCCF != 0) tblColectivoProgress += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(28)'></i>";
            else { tblColectivoProgress += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
        tblColectivoProgress += "</td></tr>";

        tblColectivoProgress += "<tr>";
        tblColectivoProgress += "<td class='text-right'>" + datos.ProfesionalesCSF + ""
        if (datos.ProfesionalesCSF != 0) tblColectivoProgress += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(29)'></i>";
            else { tblColectivoProgress += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblColectivoProgress += "</td></tr>";
        
        tblColectivoProgress += "<tr>";
        tblColectivoProgress += "<td class='text-right'>" + datos.Totalprofesionales + "";
        if (datos.Totalprofesionales != 0) tblColectivoProgress += "<i style='visibility:visible' class='fa fa-file-pdf-o' onclick='informes(30)'></i>";
        else { tblColectivoProgress += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }
        tblColectivoProgress += "</td></tr>";


        tblColectivoProgress += "<tr>";
        tblColectivoProgress += "<td class='text-right'>" + datos.Profesionalessinevaluacion + "";
        if (datos.Profesionalessinevaluacion != 0) tblColectivoProgress += "<i style='visibility:visible' onclick='informes(31)' class='fa fa-file-pdf-o'></i>";
            else { tblColectivoProgress += " <i style='visibility:hidden; margin-left:0' class='fa fa-file-pdf-o'></i>"; }

        tblColectivoProgress += "</td></tr>";

       


        tblColectivoProgress += "</tbody>";
            

        ////Inyectar html en la página        

        setTimeout(function () {
            $("#tblEvaluaciones").html(tblEvaluaciones);
        }, 30);


        setTimeout(function () {
            $("#tblProfesionalesEvaluadores").html(tblEvaluadores);
        }, 80);

        setTimeout(function () {
            $("#tblColectivoProgress").html(tblColectivoProgress);
        }, 120);

        //$("#tblEvaluaciones").html(tblEvaluaciones);

        //$("#tblProfesionalesEvaluadores").html(tblEvaluadores);

        //$("#tblColectivoProgress").html(tblColectivoProgress);


        
    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }
}


$("#selMesIni, #selAnoIni, #selMesFin, #selAnoFin, #cboEstado, #txtFantiguedad, #cboColectivo ,#SelMesIniProgress, #SelAnoIniProgress, #SelMesFinProgress, #SelAnoFinProgress, #txtFantiguedadProgress, #cboColectivoProgress"  
    ).on("change", function () {
    cargarEstadisticas();
    })



//***Modal selección de CR (al hacer click sobre el link de CR)
$('#lblCR').on('click', function () {
    $('#modal-CR').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-CR').modal('show');
    $('#lisCR').children().remove();
    $('#modal-CR input[type=text]').val('');
    ObtenerCR();
});


$('#lblCREvaluadores').on('click', function () {
    $('#modal-CREvaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-CREvaluadores').modal('show');
    $('#lisCREvaluadores').children().remove();
    
    ObtenerCREvaluadores();
});

$('#lblCRColectivos').on('click', function () {
    $('#modal-CRColectivos').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-CRColectivos').modal('show');
    $('#lisCRColectivos').children().remove();
    
    ObtenerCRColectivos();
});


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

//Selección simple de CR
$('body').on('click', '#lisCR li', function (e) {
    $('#lisCR li').removeClass('active');
    $(this).addClass('active');
});


$('body').on('click', '#lisCREvaluadores li', function (e) {
    $('#lisCREvaluadores li').removeClass('active');
    $(this).addClass('active');
});

$('body').on('click', '#lisCRColectivos li', function (e) {
    $('#lisCRColectivos li').removeClass('active');
    $(this).addClass('active');
});

//Botón seleccionar de CR
$('#modal-CR #btnSeleccionarCR').on('click', function () {
    $CR = $('#lisCR li.active');
    if ($CR.length > 0) {
        $('#CR').val($CR.text());
        $('#modal-CR').modal('hide');
        $("#imgCREvaluacion").css("display", "inline-block");
        cargarEstadisticas();
    } else {
        alertNew("warning", "Debes seleccionar un CR");
    }

});


$('#modal-CREvaluadores #btnSeleccionarCREvaluadores').on('click', function () {
    $CREvaluadores = $('#lisCREvaluadores li.active');
    if ($CREvaluadores.length > 0) {
        $('#CREvaluadores').val($CREvaluadores.text());
        $('#CREvaluadores').attr("idnodo", $CREvaluadores.attr('value')).val($CREvaluadores.text());
        $('#modal-CREvaluadores').modal('hide');
        $("#imgCREvaluadores").css("display", "inline-block");
        cargarEstadisticas();
    } else {
        alertNew("warning", "Debes seleccionar un CR");
    }

});


$('#modal-CRColectivos #btnSeleccionarCRColectivos').on('click', function () {
    $CRColectivos = $('#lisCRColectivos li.active');
    if ($CRColectivos.length > 0) {
        $('#CRColectivos').val($CRColectivos.text());
        $('#CRColectivos').attr("idnodo", $CRColectivos.attr('value')).val($CRColectivos.text());
        $('#modal-CRColectivos').modal('hide');
        $("#imgCRColectivos").css("display", "inline-block");
        cargarEstadisticas();
    } else {
        alertNew("warning", "Debes seleccionar un CR");
    }

});

//Botón cancelar de CR
$('#modal-CR #btnCancelarCR').on('click', function () {
    $('#modal-CR').modal('hide');
});

$('#modal-CREvaluadores #btnCancelarCREvaluadores').on('click', function () {
    $('#modal-CREvaluadores').modal('hide');
});

$('#modal-CRColectivos #btnCancelarCRColectivos').on('click', function () {
    $('#modal-CRColectivos').modal('hide');
});

//Obtiene los CR (se llama al hacer intro sobre el filtro (nombre) o al pulsar sobre Obtener)
function ObtenerCR() {
    $('#lisCR').children().remove();
    $lisCR = $('#lisCR');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getCR",   // Current Page, Method
        //data: JSON.stringify(),  // parameter map as JSON
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


//Obtiene los CR (se llama al hacer intro sobre el filtro (nombre) o al pulsar sobre Obtener)
function ObtenerCREvaluadores() {
    $('#lisCREvaluadores').children().remove();
    $lisCREvaluadores = $('#lisCREvaluadores');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getCRActivos",   // Current Page, Method
        //data: JSON.stringify(),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item' value='" + this.T303_idnodo + "'>" + this.T303_denominacion + "</li>").appendTo($lisCREvaluadores); });
                $('#modal-CREvaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-CREvaluadores').modal('show');

            } else {
                alertNew("warning", "No existen CR");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los CR");
        }
    });
}

function ObtenerCRColectivos() {
    $('#lisCRColectivos').children().remove();
    $lisCRColectivos = $('#lisCRColectivos');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getCRActivos",   // Current Page, Method
        //data: JSON.stringify(),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item' value='" + this.T303_idnodo + "'>" + this.T303_denominacion + "</li>").appendTo($lisCRColectivos); });
                $('#modal-CRColectivos').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-CRColectivos').modal('show');

            } else {
                alertNew("warning", "No existen CR");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los CR");
        }
    });
}



$("#imgCREvaluacion").on("click", function () {
    $("#CR").val("");    
    $("#imgCREvaluacion").css("display", "none");
    cargarEstadisticas();
});

$("#imgCREvaluadores").on("click", function () {
    $("#CREvaluadores").val("");
    $("#CREvaluadores").attr("idnodo", "");
    $("#imgCREvaluadores").css("display", "none");
    cargarEstadisticas();
});


$("#imgCRColectivos").on("click", function () {
    $("#CRColectivos").val("");
    $("#CRColectivos").attr("idnodo", "");
    $("#imgCRColectivos").css("display", "none");
    cargarEstadisticas();
});


$("#imgEvaluadorColectivoProgress").on("click", function () {
    $("#evaluador").val("");
    $("#evaluador").attr("idficepi", "");
    $("#imgEvaluadorColectivoProgress").css("display", "none");
    cargarEstadisticas();
});


//***Modal selección de evaluadores (al hacer click sobre el link de Evaluador)

$('#lblEvaluador').on('click', function () {
    $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-evaluadores').modal('show');
    $('#lisEvaluadores').children().remove();
    $('#modal-evaluadores input[type=text]').val('');

});


//Selección simple de evaluador
$('body').on('click', '#lisEvaluadores li', function (e) {
    $('#lisEvaluadores li').removeClass('active');
    $(this).addClass('active');
});

//Botón seleccionar de evaluador
$('#modal-evaluadores #btnSeleccionar').on('click', function () {
    $evaluador = $('#lisEvaluadores li.active');
    if ($evaluador.length > 0) {
        //$('#sProyecto').val($proyecto.text());
        $('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
        $('#evaluador').attr("data-sexo", $evaluador.attr('data-sexo'));
        $('#modal-evaluadores').modal('hide');
        $("#imgEvaluadorColectivoProgress").css("display", "inline-block");
        cargarEstadisticas();

    } else {
        alertNew("warning", "Debes seleccionar un evaluador");
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
    $.ajax({
        url: "Default.aspx/getEvaluadores",   // Current Page, Method
        data: JSON.stringify({ t001_apellido1: $('#txtApellido1').val(), t001_apellido2: $('#txtApellido2').val(), t001_nombre: $('#txtNombre').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item' data-sexo='" + this.Sexo + "' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });
                $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluadores').modal('show');

            } else {
                alertNew("warning", "No existen evaluadores");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluadores");
        }
    });
}

//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluadores :input[type=text]').on('keyup', function (event) {
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
//***Fin Modal selección de evaluadores


function informes(opcion) {
    //alert(opcion);
    //var strUrlPag = "Informe/Default.aspx?opcion=" + opcion + "&pantalla=1";

    //if (opcion == 1 || opcion == 3 || opcion == 5 || opcion == 6 || opcion == 7 || opcion == 9 || opcion == 10 || opcion == 11 || opcion == 13 || opcion == 14 || opcion == 15 || opcion == 17 || opcion == 18 || opcion == 19)
        var strUrlPag = "../Estadisticas/Informe/Default.aspx?opcion=" + opcion + "&pantalla=2";

    var bScroll = "no";
    var bMenu = "no";
    if (screen.width == 800) bScroll = "yes";
    bMenu = "yes";
    
    window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));

    //inf = window.open(strUrlPag, "inf", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
    //setTimeout("inf.close();", 15000);
}