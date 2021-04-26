
var $encurso = $('#tbdEval tr[estado=CUR]');
var $cerradas = $('#tbdEval tr[estado!=CUR]');
var $forzadas = $('#tbdEval tr[motivo=F]');
var $arbol = $('#tbdEval tr[motivo=A]');
var primeravez = true;
var datosGuardados;
var checked = false;
var DesdeManiobra;
var HastaManiobra;
var ProfundidadManiobra;
var Situacion = "";
//var Desde;
//var Hasta;

$(document).ready(function () {

    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {            
            $(".header-fixed > tbody").css("max-height", "240px");
        }
    }

    //manejador de fechas
    moment.locale('es');
   
    ValMiEquipo();
})
  

$("#imgEvaluado").on("click", function (evento) {
    $("#inputEvaluado").val("");
    $("#inputEvaluado").attr("idficepi", null);
    $("#imgEvaluado").css("display", "none");
    logicaBusqueda(evento.target.id);
});

$("#imgEvaluador").on("click", function (evento) {
    $("#inputEvaluador").val("");
    $("#inputEvaluador").attr("idficepi", null);
    $("#imgEvaluador").css("display", "none");
    logicaBusqueda(evento.target.id);
});


function ValMiEquipo() {

    if (typeof $('#inputEvaluado').val() == "undefined" || $('#inputEvaluado').val() == "") {
        $("#imgEvaluado").css("display", "none");
    }
    else {
        $("#imgEvaluado").css("display", "inline-block");
    }

    if (typeof $('#inputEvaluador').val() == "undefined" || $('#inputEvaluador').val() == "") {
        $("#imgEvaluador").css("display", "none");
    }
    else {
        $("#imgEvaluador").css("display", "inline-block");
    }

    //Se comprueba que los combos Desde y Hasta seleccionados tienen un periodo lógico
    if (!comprobarfechas()) return;

    

    actualizarSession();

    if (filtrosDeMiEquipo != "") {
        var $filtrosDeMiEquipo = JSON.parse(filtrosDeMiEquipo);

        primeravez = false;
        //FECHAS        
        Desde = parseInt($filtrosDeMiEquipo[0].toString().substring(0, 4)) * 100 + parseInt($filtrosDeMiEquipo[0].toString().substring(4, 6));
        Hasta = parseInt($filtrosDeMiEquipo[1].toString().substring(0, 4)) * 100 + parseInt($filtrosDeMiEquipo[1].toString().substring(4, 6));
       
        $('#selAnoIni').val($filtrosDeMiEquipo[0].toString().substring(0, 4));
        $('#selMesIni').val($filtrosDeMiEquipo[0].toString().substring(4, 6));

        $('#selAnoFin').val($filtrosDeMiEquipo[1].toString().substring(0, 4));
        $('#selMesFin').val($filtrosDeMiEquipo[1].toString().substring(4, 6));

        Profundidad = $filtrosDeMiEquipo[2].toString();
        Situacion = $filtrosDeMiEquipo[3].toString();

        $("#cboProfundizacion").val($filtrosDeMiEquipo[2].toString());
        $("#cboEstado").val($filtrosDeMiEquipo[3].toString());

        if (Boolean($filtrosDeMiEquipo[4])) {
            //$("#checkDesignacion").prop("checked", true);            
        }

        $('#inputEvaluado').attr("idficepi", $filtrosDeMiEquipo[5]);
        $('#inputEvaluado').val($filtrosDeMiEquipo[6]);

        if (Boolean($filtrosDeMiEquipo[7])) {
            $("#chkBuscarPersona").prop("checked", true);
            $("#divNivel").css("visibility", "hidden");
            $("#fldEvaluadorEvaluado").removeClass("hide").addClass("show");
        }

        //CheckPeriodo (Para que se tenga en cuenta el período en las búsquedas por personas)
        if (Boolean($filtrosDeMiEquipo[8])) {
            $("#chkPeriodo").prop("checked", true);            
        }
        else {

            if ($("#chkBuscarPersona").prop("checked") == true) {
                $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').prop("disabled", true);
                $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').css("background-color", "#DADADA");
            }
            
        }

        $('#inputEvaluador').attr("idficepi", $filtrosDeMiEquipo[9]);
        $('#inputEvaluador').val($filtrosDeMiEquipo[10]);

        $("#selectDesignacion").val($filtrosDeMiEquipo[11]);
       

        if (typeof $('#inputEvaluado').val() != "undefined") {
            $("#imgEvaluado").css("display", "inline-block");
        }
        else {
            $("#imgEvaluado").css("display", "none");
        }


        if (typeof $('#inputEvaluador').val() != "undefined") {
            $("#imgEvaluador").css("display", "inline-block");
        }
        else {
            $("#imgEvaluador").css("display", "none");
        }

        filtrosDeMiEquipo = "";
    }


    if (typeof $('#inputEvaluado').val() == "undefined" || $('#inputEvaluado').val() == "") {
        $("#imgEvaluado").css("display", "none");
    }
    else {
        $("#imgEvaluado").css("display", "inline-block");
    }

    if (typeof $('#inputEvaluador').val() == "undefined" || $('#inputEvaluador').val() == "") {
        $("#imgEvaluador").css("display", "none");
    }
    else {
        $("#imgEvaluador").css("display", "inline-block");
    }

    //Vacíamos tabla
    $("#tbdEval").html("");
   
    logicaBusqueda("inicio");

  
}



function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  
        
        var tbdEvalMiEquipo = "";
        var feccierre = "";
        //$valorCombo = $("#selectDesignacion").val();

        for (var i = 0; i < datos.length; i++) {

            tbdEvalMiEquipo += "<tr data-idficepievaluador='" + datos[i].Idficepi_evaluador + "'  motivo='" + datos[i].Motivo + "'  data-estado='" + datos[i].estadooriginal + "' idformulario=" + datos[i].idformulario + " idvaloracion='" + datos[i].idvaloracion + "'>";

            //if (datos[i].Motivo == "F" && $("#checkDesignacion").prop("checked") == false) continue;
            //if (datos[i].Motivo == "F" && $valorCombo == "1") continue;
            //if (datos[i].Motivo == "A" && $valorCombo == "2") continue;
            
            ////No forzadas
            //if (datos[i].Motivo == "A" && ($valorCombo == "0" || $valorCombo == "1")) {
            //    tbdEvalMiEquipo += "<td><i class='glyphicon glyphicon-search'</td>";
            //}

            ////Forzadas
            //if (datos[i].Motivo == "F" && ($valorCombo == "0" || $valorCombo == "2")) {
            //    tbdEvalMiEquipo += "<td><i class='glyphicon glyphicon-search rojo'</td>";
            //}


            //if (datos[i].Motivo == "F" && $valorCombo == "F") continue;
            //if (datos[i].Motivo == "A" && $valorCombo == "T") continue;

            //No forzadas
            if (datos[i].Motivo == "A") {
                tbdEvalMiEquipo += "<td><i class='glyphicon glyphicon-search'</td>";
            }

            //Forzadas
            if (datos[i].Motivo == "F") {
                tbdEvalMiEquipo += "<td><i class='glyphicon glyphicon-search rojo'</td>";
            }

          
            tbdEvalMiEquipo += "<td>" + datos[i].evaluado + "</td>";
            tbdEvalMiEquipo += "<td>" + datos[i].evaluador + "</td>";
            tbdEvalMiEquipo += "<td>" + datos[i].estado + "</td>";
            tbdEvalMiEquipo += "<td>" + moment(datos[i].fecapertura).format('DD/MM/YYYY') + "</td>";

            if (datos[i].feccierre == null) feccierre = "";
            else feccierre = moment(datos[i].feccierre).format('DD/MM/YYYY');

            tbdEvalMiEquipo += "<td>" + feccierre + "</td>";
            tbdEvalMiEquipo += "</tr>";
        };

        //Inyectar html en la página
        $("#tbdEval").html(tbdEvalMiEquipo);
        
        
        $("#spanNumeroEvaluaciones").text($("#tbdEval tr").length)

        if ($("#tbdEval tr").length == 1) $("#spanTextoResultado").text("resultado");
        else $("#spanTextoResultado").text("resultados");

        //Mostramos leyenda si hay alguna Evaluación visible por designación
        if ($(".glyphicon.glyphicon-search.rojo").length > 1) {
            $("#leyendaForzadas").css("visibility", "visible");
        }
        else {
            $("#leyendaForzadas").css("visibility", "hidden");
        }
       
      
        primeravez = false;

        /*Controlamos si el contenedor tiene Scroll*/
        var div = document.getElementById('tbdEval');

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
        if (hasVerticalScrollbar) {
            $("thead").css("width", "calc( 100% - 1em )")
        }
        else { $("thead").css("width", "100%") }        
        /*FIN Controlamos si el contenedor tiene Scroll*/

        $("#tblEvalMiEquipo").trigger("destroy");

       
       
        //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
        $("#tblEvalMiEquipo").tablesorter({
            //dateFormat: "dd/mm/yy", // set the default date format
            // pass the headers argument and assing a object            
            headers: {
                // set "sorter : false" (no quotes) to disable the column
                0: {
                    sorter : false
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

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}


function comprobarfechas() {
    try {
        $anoIni = $('#selAnoIni'); $mesIni = $('#selMesIni'); $anoFin = $('#selAnoFin'); $mesFin = $('#selMesFin');
        fecIni = parseInt($anoIni.val()) * 100 + parseInt($mesIni.val());
        fecFin = parseInt($anoFin.val()) * 100 + parseInt($mesFin.val());

        if (fecIni > fecFin) {
            alertNew('warning', 'Periodo seleccionado no lógico.');
            $("#tbdEval").html("");
            return false;
        } else
            return true;

    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar las fechas.");
    }
}


//Selección de profesionales
$('body').on('click', '#tbdEval tr', function (e) {
    $('#tbdEval tr').removeClass('active');
    $(this).addClass('active');
});


$('body').on('click', '.glyphicon.glyphicon-search', function (e) {
    
    $active = $(this).parent().parent().addClass("active");

    if (!comprobarAccesoEvaluacion($active.attr("data-estado"), idficepi, $active.attr("data-idficepievaluador")))
    {
        alertNew("warning", "Para acceder a evaluaciones abiertas tienes que ser evaluador de las mismas. En este caso, al no cumplirse la condición, no puedes acceder.", null, 5000, null);
        return;
    }
    //Webmethod que almacena filtros de búsqueda
    llamadaGuardarFiltros();
    
    switch ($active.attr('idformulario')) {
        case "1":
            location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=demiequipo";
            break;
        case "2":
            location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=demiequipo";
            break;
    }
    
});


$("#selectDesignacion").on('change', function () {    
    $("#tbdEval").html("");    
    //$("#selectDesignacion option[selected]").val();
    //pintarDatosPantalla(datosGuardados);
    //ValMiEquipo();
    logicaBusqueda("selectDesignacion");
});


$('div.container div.row button.btn-primary').on('click', function () {

    //Webmethod que almacena filtros de búsqueda
    llamadaGuardarFiltros();

    $active = $('#tbdEval tr.active');
    if ($active.length > 0) {
        switch ($active.attr('idformulario')) {
            case "1":
                location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=demiequipo";
                break;
            case "2":
                location.href = strServer + 'Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=' + codpar($active.attr('idvaloracion')) + "&acceso=demiequipo";
                break;
        }
    } else
        alertNew("warning", "Tienes que seleccionar alguna evaluación");

});


function llamadaGuardarFiltros() {

    //Valores filtros
    var Desde = parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val());
    var Hasta = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());
    var Profundidad = $("#cboProfundizacion").val();
    var Situacion = $("#cboEstado").val()
    var checkbox = $("#checkDesignacion").prop("checked");
    var optionSelected = $("#selectDesignacion").val();
    var chkBuscarPersona = $("#chkBuscarPersona").prop("checked");
    var chkPeriodo = $("#chkPeriodo").prop("checked");


    
    var idficepi_evaluado;
    if (typeof $("#inputEvaluado").attr("idficepi") == "undefined") idficepi_evaluado = null;
    else idficepi_evaluado = $("#inputEvaluado").attr("idficepi");
      
    var nombreEvaluado = $("#inputEvaluado").val();

    var idficepi_evaluador;
    if (typeof $("#inputEvaluador").attr("idficepi") == "undefined") idficepi_evaluador = null;
    else idficepi_evaluador = $("#inputEvaluador").attr("idficepi");

    var nombreEvaluador = $("#inputEvaluador").val();

    $.ajax({
        url: "Default.aspx/GuardarFiltros",
        "data": JSON.stringify({ desde: Desde, hasta: Hasta, profundidad: Profundidad, estado: Situacion, checkbox: checkbox, idficepi_evaluado: idficepi_evaluado, nombreEvaluado: nombreEvaluado, chkBuscarPersona: chkBuscarPersona, chkPeriodo: chkPeriodo, idficepi_evaluador: idficepi_evaluador, nombreEvaluador: nombreEvaluador, optionSelected: optionSelected }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {

        },
        error: function (ex, status) {
            alertNew("danger", "No se han podido guardar los filtros de búsqueda");
        }
    });
}

$('body').on('keydown', function (e) {
    var code = (e.keyCode ? e.keyCode : e.which);
    if (code == 116) {
        e.preventDefault();
        location.href = "Default.aspx";
    }

});


//MODAL BÚSQUEDA DE EVALUADOS
//***Modal selección de evaluados
$('#lblEvaluado').on('click', function () {
    $('#modal-evaluados').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-evaluados').modal('show');
    $('#lisEvaluados').children().remove();
    $('#modal-evaluados input[type=text]').val('');

});


//Selección simple de evaluado
$('body').on('click', '#lisEvaluados li', function (e) {
    $('#lisEvaluados li').removeClass('active');
    $(this).addClass('active');

});

//Botón seleccionar de evaluado
$('#modal-evaluados #btnSeleccionar').on('click', function (evento) {

    $evaluado = $('#lisEvaluados li.active');
    
    if ($evaluado.length > 0) {       
        $('#inputEvaluado').attr("idficepi", $evaluado.attr('value')).val($evaluado.text());
        $('#modal-evaluados').modal('hide');
        $("#imgEvaluado").css("display", "inline-block");
        logicaBusqueda(evento.target.id)
    } else {
        alertNew("warning", "No has seleccionado ningún profesional");
    }

});

//Poner el foco en la caja de texto del apellido1
$('#modal-evaluados').on('shown.bs.modal', function () {
    $('#inputApellido1').focus();
})

//Botón cancelar de evaluado
$('#modal-evaluados #btnCancelar').on('click', function () {
    $('#modal-evaluados').modal('hide');
});


//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluados :input[type=text]').on('keyup', function (event) {

    $("#btnSeleccionar").css("display", "none");

    if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "") {
        $("#btnObtener").removeClass("show").addClass("hide");
        return;
    }

    $("#btnObtener").removeClass("hide").addClass("show");

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#lisEvaluados").length > 0) {
        $("#lisEvaluados").html("");
    }

    if (event.keyCode == 13) {

        if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "" && $("#tblgetFicepi li").length == 0) {
            alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
            return;
        }

        obtener();

    }
});



$("#btnObtener").on("click", function () {
    obtener();
})

function obtener() {

    $lisEvaluados = $('#lisEvaluados');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getEvaluadosDeMiEquipo",   // Current Page, Method
        data: JSON.stringify({ idficepi: idficepi, t001_apellido1: $('#inputApellido1').val(), t001_apellido2: $('#inputApellido2').val(), t001_nombre: $('#txtNombre').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                limpiarFiltros();
                $("#btnObtener").removeClass("show").addClass("hide");
                $("#btnSeleccionar").css("display", "inline-block");
                $(result.d).each(function () { $("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluados); });
                $('#modal-evaluados').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluados').modal('show');

            } else {
                alertNew("warning", "Ningún evaluado/a bajo tu ámbito de visión responde a la búsqueda por los filtros establecidos", null, 2000, null);
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluados");
        }
    });
}

function limpiarFiltros() {
    $("#inputApellido1").val("");
    $("#inputApellido2").val("");
    $("#inputNombre").val("");
}

//***Fin Modal selección de evaluados



/////////////////////////////////////////MODAL EVALUADORES/////////////////////////////////////

$('#lblEvaluador').on('click', function () {
    $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#modal-evaluadores').modal('show');
    $('#lisEvaluadores').children().remove();
    $('#modal-evaluadores input[type=text]').val('');

});



$('body').on('click', '#lisEvaluadores li', function (e) {
    $('#lisEvaluadores li').removeClass('active');
    $(this).addClass('active');

});


$('#modal-evaluadores #btnSeleccionarEvaluador').on('click', function (evento) {

    $evaluador = $('#lisEvaluadores li.active');

    if ($evaluador.length > 0) {
        $('#inputEvaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
        $('#modal-evaluadores').modal('hide');
        $("#imgEvaluador").css("display", "inline-block");
        logicaBusqueda(evento.target.id);
    } else {
        alertNew("warning", "No has seleccionado ningún profesional");
    }

});

//Poner el foco en la caja de texto del apellido1
$('#modal-evaluadores').on('shown.bs.modal', function () {
    $('#inputApellido1Evaluador').focus();
})


$('#modal-evaluadores #btnCancelarEvaluador').on('click', function () {
    $('#modal-evaluadores').modal('hide');
});


//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluadores :input[type=text]').on('keyup', function (event) {

    $("#btnSeleccionarEvaluador").css("display", "none");

    if ($("#inputApellido1Evaluador").val() == "" && $("#inputApellido2Evaluador").val() == "" && $("#txtNombreEvaluador").val() == "") {
        $("#btnObtenerEvaluador").removeClass("show").addClass("hide");
        return;
    }

    $("#btnObtenerEvaluador").removeClass("hide").addClass("show");

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#lisEvaluadores").length > 0) {
        $("#lisEvaluadores").html("");
    }

    if (event.keyCode == 13) {

        if ($("#inputApellido1Evaluador").val() == "" && $("#inputApellido2Evaluador").val() == "" && $("#txtNombreEvaluador").val() == "" && $("#tblgetFicepi li").length == 0) {
            alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
            return;
        }

        obtenerEvaluadores();

    }
});



$("#btnObtenerEvaluador").on("click", function () {
    obtenerEvaluadores();
})

function obtenerEvaluadores() {

    $lisEvaluadores = $('#lisEvaluadores');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getEvaluadoresDeMiEquipo",   // Current Page, Method
        data: JSON.stringify({ idficepi: idficepi, t001_apellido1: $('#inputApellido1Evaluador').val(), t001_apellido2: $('#inputApellido2Evaluador').val(), t001_nombre: $('#txtNombreEvaluador').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                limpiarFiltrosEvaluador();
                $("#btnObtenerEvaluador").removeClass("show").addClass("hide");
                $("#btnSeleccionarEvaluador").css("display", "inline-block");
                $(result.d).each(function () { $("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });
                $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluadores').modal('show');

            } else {
                alertNew("warning", "Ningún evaluador bajo tu ámbito de visión responde a la búsqueda por los filtros establecidos", null, 2000, null);
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluadores");
        }
    });
}

function limpiarFiltrosEvaluador() {
    $("#inputApellido1Evaluador").val("");
    $("#inputApellido2Evaluador").val("");
    $("#inputNombreEvaluador").val("");
}


$("#chkBuscarPersona").on("click", function (evento) {
    $("#spanNumeroEvaluaciones").text("0");
    $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').prop("disabled", false);
    $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').css("background-color", "#FFFFFF");

    if ($(this).prop("checked") == true) {
        $("#divNivel").css("visibility", "hidden");
        $("#fldEvaluadorEvaluado").removeClass("hide").addClass("show");
        if (screen.height == 768){
            $("#tbdEval").height("174px");
        }
        
        if ($("#chkPeriodo").prop("checked") == false) {
            $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').prop("disabled", true);
            $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').css("background-color", "#DADADA");            
        }
    }

    else {
        $("#divNivel").css("visibility", "visible");
        $("#fldEvaluadorEvaluado").removeClass("show").addClass("hide");
        if (screen.height == 768) {
            $("#tbdEval").height("240px");
        }
    }

    //Evento (click filtrar por persona)
    logicaBusqueda(evento.target.id);
})

$("#chkPeriodo").on('click', function (evento) {
   
    if ($(this).prop("checked") == true) {
        $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').prop("disabled", false);
        $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').css("background-color", "#FFFFFF");
    }
    else {
        $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').prop("disabled", true);
        $('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin').css("background-color", "#DADADA");
        
    }


    logicaBusqueda(evento.target.id);
});

$('#selAnoIni, #selMesIni, #selAnoFin, #selMesFin, #cboProfundizacion, #cboEstado').on("change", function (evento) {
    $("#spanNumeroEvaluaciones").text("0");
    //Evento (on change de los combos de fechas)
    logicaBusqueda(evento.target.id)
})


$("#btnRestablecer").on("click", function () {
    restablecerFiltros();
})


function restablecerFiltros() {    
    location.href = "Default.aspx";
}


function comprobarAccesoEvaluacion(estadoEvaluacion, usuarioLogado, evaluadorEvaluacion ) {
    if (usuarioLogado == evaluadorEvaluacion )return true;
    if (estadoEvaluacion != "ABI") return true;
    return false;
}

function logicaBusqueda(disparadorOrigen) {

    DesdeManiobra = parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val());
    HastaManiobra = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());
    
    
    if ($("#chkBuscarPersona").prop("checked") == true && $("#chkPeriodo").prop("checked") == false) {
        DesdeManiobra = anyomesmin;
        HastaManiobra = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());
    }

    if ($("#chkBuscarPersona").prop("checked") == true) {

        $("#tbdEval").html("");
        if ($("#inputEvaluado").val() == "" && $("#inputEvaluador").val() == "") return;
        ProfundidadManiobra = 3;

        if (typeof $("#inputEvaluado").attr("idficepi") == "undefined") $lblEvaluadoManiobra = null;
        else $lblEvaluadoManiobra = $("#inputEvaluado").attr("idficepi");


        if (typeof $("#inputEvaluador").attr("idficepi") == "undefined") $lblEvaluadorManiobra = null;
        else $lblEvaluadorManiobra = $("#inputEvaluador").attr("idficepi");
        
    }
    else {
        ProfundidadManiobra = $("#cboProfundizacion").val();
        $lblEvaluadoManiobra =  null;
        $lblEvaluadorManiobra = null;
    }

    
    Situacion = $("#cboEstado").val();
    

    switch (disparadorOrigen) {
        case "inicio":
            if(origen != "Formularios") Situacion = "";
            break;
        case "chkBuscarPersona":
            if ($("#chkBuscarPersona").prop("checked") == true && $("#inputEvaluado").val() == "" && $("#inputEvaluador").val() == "") return;

            break;

        case "chkPeriodo":
            if ($("#inputEvaluado").val() == "" && $("#inputEvaluador").val() == "") return;

            break;

        case "selectDesignacion":
            
            break;



        default:
            break;

    }
   
    if ($("#cboEstado").val() == "ABI") $combo = "A";
    else $combo = $("#selectDesignacion").val();
   
    procesando.mostrar();

    $.ajax({
        url: "Default.aspx/ValMiEquipo",
        "data": JSON.stringify({ desde: DesdeManiobra, hasta: HastaManiobra, profundidad: ProfundidadManiobra, estado: Situacion, idficepi_evaluado: $lblEvaluadoManiobra, idficepi_evaluador: $lblEvaluadorManiobra, alcance: $combo }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            procesando.ocultar();
            //var arr = result.d.split('@@||@@');

            //Si hay demasiados resultados 
            if (JSON.parse(result.d).length > 1000) {
                
                alertNew("warning", "El volumen del resultado obtenido excede el límite permitido (" + JSON.parse(result.d).length + " frente a 1000 máximo). Acota más la búsqueda y vuelve a intentarlo", null, 6000, null);
                $("#tbdEval").html("");                
                $("#spanNumeroEvaluaciones").text(JSON.parse(result.d).length);
                $("#spanTextoResultado").text(" resultados. Excede límite");
                return;
            }


            if (JSON.parse(result.d).length == 0) {
                alertNew("warning", "No tienes evaluaciones que respondan a los filtros establecidos");
                $("#tbdEval").html("");
                $("#checkMostrarEvalDesignacion").css("visibility", "hidden");
                $("#btnAcceder").css("visibility", "hidden");                
            }

            else {
                $("#btnAcceder").css("visibility", "visible");                
            }

            if (primeravez) {
                if (JSON.parse(result.d).length > 0) {
                    if (JSON.parse(result.d)[0].estadooriginal == "CUR") {
                        $("#cboEstado").val("CUR");
                    }

                    else if (JSON.parse(result.d)[0].estadooriginal == "ABI") {
                        $("#cboEstado").val("ABI");
                    }

                    else {
                        $("#cboEstado").val("CER");

                    }

                }
                else $("#cboEstado").val("CER");
            }

            
            if (JSON.parse(result.d)[0].Mostrarcombo) $("#checkMostrarEvalDesignacion").css("visibility", "visible");
            else $("#checkMostrarEvalDesignacion").css("visibility", "hidden");

            if ($("#cboEstado").val() == "ABI") $("#checkMostrarEvalDesignacion").css("visibility", "hidden");

            //if ($("#cboEstado").val() == "ABI") $("#checkMostrarEvalDesignacion").css("visibility", "hidden");
            //Hay que parsear el resultado JSON que devuelve el servidor
            //datosGuardados = JSON.parse(result.d);

            pintarDatosPantalla(JSON.parse(result.d));



        },
        error: function (ex, status) {
            procesando.ocultar();
            alertNew("danger", "No se han podido obtener las evaluaciones de mi equipo");
        }
    });
}



 


