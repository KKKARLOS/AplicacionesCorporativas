//RequestFilter: variable donde se almacenarán los filtros
var rf;
var rfParamsRPT;
var rfExcel;

$(document).ready(function () {
    comprobarerrores();

   // //Se obtiene la fecha actual
    var currentDate = new Date()
    var day = currentDate.getDate();
    var month = currentDate.getMonth() + 1;
    var year = currentDate.getFullYear();

   // //Fecha antigüedad (Mostramos días en el calendario)
    $("#txtFantiguedad").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        yearRange: '1973:' + year,

        beforeShow: function (input, inst) {
            $(inst.dpDiv).removeClass('calendar-off');            
        }
    });

    if (origen != "ADM") $("#divSituacion").css("visibility", "hidden");
    
    //Establecemos el código de pantalla para las ayudas
    $(document).find("body").attr('data-codigopantalla', $(document).find("body").attr('data-codigopantalla') + origen);
   
    $("#txtFantiguedad").datepicker({
        dateFormat: 'dd/mm/yy'
    }).datepicker('setDate', defectoAntiguedad)


   // //Si está visible el filtro del evaluador (ADM), se asigna el atributo idficepi y el val al textbox (con los valores de las vbles de sesión)
   // //$visible = $('.fk-ocultar').not('.hide');
   // //if ($visible.length > 1) {//ADM
   // //    $visible.find('input#evaluador').attr('idficepi', idficepi.toString());
   // //    $visible.find('input#evaluador').attr('data-sexo', sexo.toString());
   // //    $visible.find('input#evaluador').val(nombre);
   // //}

    $('input#evaluador').attr('idficepi', idficepi.toString());
    $('input#evaluador').attr('data-sexo', sexo.toString());
    $('input#evaluador').val(nombre);


    if (sexo == "V") $("#lblEvaluador").text("Evaluador")
    else $("#lblEvaluador").text("Evaluadora");
    
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
   // //FIN DATEPICKER

   // //Llamada ajax para traer las estadísticas al cargar la página
    cargarEstadisticas();
    
    $('[data-toggle="popover"]').popover({
        placement: 'auto',
        trigger: 'hover',
        html: true        
    });
})

//Obtiene las estadísticas con los filtros seleccionados
function cargarEstadisticas() {
    //Se limpian las tablas
    $("#tblProfesionales").html("");
    $("#tblEvaluaciones").html("");

    //Se comprueba que los combos Desde y Hasta seleccionados tienen un periodo lógico
    if (!comprobarfechas()) return;

    //obtiene los criterios de filtrado y los deja en rf
    rf = getRequestFilter();
    rfParamsRPT = getRequestParamsRPT();

    //Se llama al servidor (cargarEstadisticas) con los filtros seleccionados
    actualizarSession();
    $.ajax({
        url: "Default.aspx/cargarEstadisticas",
        "data": JSON.stringify({ requestFilter: rf, requestParamsRPT: rfParamsRPT }),
        type: "POST",
        async : true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            //Hay que parsear el resultado JSON que devuelve el servidor            
            pintarDatosPantalla(JSON.parse(result.d));
        },
        error: function (ex, status) {            
            //error$ajax("Ocurrió un error obteniendo las estadísticas", ex, status);
            alertNew("danger", "Ocurrió un error obteniendo las estadísticas");
        }
    });

}

//Inserta una foto pasándole como parámetros el autor y el nombre de la foto
function hacerFoto() {
    var nombreFoto = $("#txtNombreFoto").val();
    actualizarSession();
    $.ajax({
        url: "Default.aspx/hacerFoto",
        data: JSON.stringify({ idficepi: idficepi, t932_denominacion: nombreFoto}),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            if (result.d == -1) //Control de que no exista una foto con ese nombre
                alertNew('warning', 'Ya existe una foto con el nombre indicado');
            else {
                $('#modal-foto-i').modal('hide');
                MostrarFotos(); //Se muestra el catálogo de fotos

                var currentDate = new Date()
                var day = currentDate.getDate();
                var month = currentDate.getMonth() + 1;
                var year = currentDate.getFullYear();

                //Hacer append con el id de la foto y concatenarle la fecha. Insertarlo después de la foto actual                
                $('<option value="' + result.d + '">' + $("#txtNombreFoto").val() + " ____" + day + "/" + month + "/" + year + '</option>').insertAfter($(".fk_fotoActual"));
            }
        },
        error: function (ex, status) {
            //error$ajax("Ocurrió un error haciendo la foto", ex, status);
            alertNew("danger", "Ocurrió un error haciendo la foto");
        }
    });

}

//Borra la foto seleccionada
function borrarFoto() {
    
    var t932_idfoto = $('#tbdFoto tr.active').attr('idfoto');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/borrarFoto",
        data: JSON.stringify({ t932_idfoto: t932_idfoto }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            //Borrar de pantalla la fila seleccionada
            idfotoSeleccionado = $('#tbdFoto tr.active').attr("idfoto");
            $('#tbdFoto tr.active').remove();                        
            $("#cboSituacion option[value='" + idfotoSeleccionado + "']").remove();
        },
        error: function (ex, status) {
            //error$ajax("Ocurrió un error borrando la foto", ex, status);
            alertNew("danger", "Ocurrió un error borrando la foto");
        }
    });
}

//Obtiene un objecto con los criterios de filtrado.
function getRequestFilter() {
    var rf = new doEstadisticas();

    rf.Desde = parseInt($('#selAnoIni').val()) * 100 + parseInt($('#selMesIni').val());
    rf.Hasta = parseInt($('#selAnoFin').val()) * 100 + parseInt($('#selMesFin').val());

    $visible = $('.fk-ocultar').not('.hide');

    if (origen == "ADM" || origen =="EVA") {//ADM: El idficepi es el atributo del textbox, la fecha de antigüedad se obtiene del datepicker y la foto del combo
        rf.t001_idficepi = $visible.find('input#evaluador').attr('idficepi');

        var sfecha = $visible.find('input#txtFantiguedad').val();
        var sd = sfecha.substring(0, 2);
        var sm = sfecha.substring(3, 5);
        var sa = sfecha.substring(6, 10);

        var fAntiguedad = new Date(sa, sm - 1, sd);  

        rf.t001_fecantigu = fAntiguedad;

        rf.t932_idfoto = $(cboSituacion).val();
    }
    else {//EV: El idficepi es la vble de sesíón del ficepi conectado, la fecha de antigüedad se obtiene a partir del combo, y, como foto, la situación actual
        rf.t001_idficepi = idficepi;
        var fAntiguedad = new Date();
        fAntiguedad.setFullYear(fAntiguedad.getFullYear() - $(cboAntRef).val());
   
        rf.t001_fecantigu = fAntiguedad;
        rf.t932_idfoto = null;  
    }

    //Como nivel de profundización, el valor del combo
    rf.Profundidad = $(cboProfundizacion).val();
    rf.t941_idcolectivo = $(cboColectivo).val();

    return rf;
}


function getRequestParamsRPT() {
    var rfParamsRPT = new doParametrosRPT();

    rfParamsRPT.txtSituacion = $('#cboSituacion').find(":selected").text();
    rfParamsRPT.sexo = $("#evaluador").attr("data-sexo");
    rfParamsRPT.txtEvaluador = $("#evaluador").val();
    rfParamsRPT.txtProfundizacion = $('#cboProfundizacion').find(":selected").text();
    rfParamsRPT.txtColectivo = $('#cboColectivo').find(":selected").text();
    
    return rfParamsRPT;
}


//pinta los datos de la pantalla en base a la estructura JS
function pintarDatosPantalla(datos) {
    try {

        
        //PINTAR HTML   (PROFESIONALES)
        var tblProfesionales = "<tbody><tr>";

        tblProfesionales += " <td class='w100'>" + datos.profevh + "</td>";
        tblProfesionales += " <td class='w100'>" + datos.profevm + "</td>";

        
        tblProfesionales += " <td class='w100'> <span>" + datos.profevt + "</span> </td>";

        tblProfesionales += "<td class='pl'>";

        if (datos.profevt != 0) {
            tblProfesionales += " <i class='fa fa-file-pdf-o fkOpcion1' onclick='informes(1)'></i>";
        }

        else {
            tblProfesionales += " <i class='fa fa-file-pdf-o transparent fkOpcion1'></i>";
        }

        tblProfesionales += "</td>";

        tblProfesionales += " <td class='w100'>" + datos.profevhant + "</td>";
        tblProfesionales += " <td class='w100'>" + datos.profevmant + "</td>";
        tblProfesionales += " <td class='w100'><span>" + datos.profevantt + "</span> </td>";


        tblProfesionales += "<td class='pl'>";
        if (datos.profevt != 0) {
            tblProfesionales += " <i class='fa fa-file-pdf-o' onclick='informes(2)'></i>";
        }

        else {
            tblProfesionales += " <i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblProfesionales += "</td>";

        
        tblProfesionales += "</tr>";
        tblProfesionales += " <tr>";
        tblProfesionales += "<td>" + datos.profnoevh + "</td><td>" + datos.profnoevm + "</td><td><span>" + datos.profnoevt + "</span></td>";

        tblProfesionales += "<td class='pl'>";

        if (datos.profnoevt != 0) {
            tblProfesionales += "<i class='fa fa-file-pdf-o' onclick='informes(3)'></i>";
        }
        else {
            tblProfesionales += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblProfesionales += "</td>";
        
        tblProfesionales += "<td>" + datos.profnoevhant + "</td><td>" + datos.profnoevmant + "</td>";
        tblProfesionales += "<td><span>" + datos.profnoevantt + "</span></td>";


        tblProfesionales += "<td class='pl'>";
        if (datos.profnoevantt != 0) {
            tblProfesionales += "<i class='fa fa-file-pdf-o' onclick='informes(4)'></i>";
        }
        else {
            tblProfesionales += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblProfesionales += "</td>";

        tblProfesionales += "</tr>";


        tblProfesionales += "<tr></td> <td>" + datos.profht + "</td> <td>" + datos.profmt + "</td><td> <span>" + datos.proft + "</span></td>";


        tblProfesionales += "<td class='pl'>";
        if (datos.proft != 0) {
            tblProfesionales += "<i class='fa fa-file-pdf-o' onclick='informes(5)'></i>";
        }
        else {
            tblProfesionales += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblProfesionales += "</td>";
           
        
        tblProfesionales += "<td>" + datos.profhantt + "</td> <td>" + datos.profmantt + "</td><td><span>" + datos.profantt + "</span> </td>";
        
        
        tblProfesionales += "<td class='pl'>";
        if (datos.profantt != 0) {
            tblProfesionales += "<i class='fa fa-file-pdf-o' onclick='informes(6)'></i>";
        }

        else {
            tblProfesionales += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblProfesionales += "</td>";

        tblProfesionales += "</tr>";                    
        tblProfesionales += "</tbody></table></div>";
        
        //PINTAR HTML (EVALUACIONES)
        tblEvaluaciones = "<tbody><tr>";
        tblEvaluaciones += "<td class='w100'>" + datos.evabiertah + "</td> <td class='w100'>" + datos.evabiertam + "</td><td class='w100'><span>" + datos.evabiertat + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evabiertat != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(7)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";

        //tblEvaluaciones += "<td class='pl'><i class='fa fa-file-pdf-o'></i></td>";
        tblEvaluaciones += " <td class='w100'>" + datos.evabiertahant + "</td> <td class='w100'>" + datos.evabiertamant + "</td><td class='w100'<span>" + datos.evabiertaantt + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evabiertaantt != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(8)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";

        tblEvaluaciones += "</tr>";

        tblEvaluaciones += "<tr>";
        tblEvaluaciones += "<td class='w100'>" + datos.evcursoh + "</td> <td class='w100'>" + datos.evcursom + "</td><td class='w100'><span>" + datos.evcursot + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evcursot != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(9)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";

        
        tblEvaluaciones += " <td class='w100'>" + datos.evcursohant + "</td> <td class='w100'>" + datos.evcursomant + "</td><td class='w100'<span>" + datos.evcursoantt + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evcursoantt != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(10)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";


        tblEvaluaciones+= "</tr>";
        
        tblEvaluaciones += "<tr><td>" + datos.evcerradah + "</td><td>" + datos.evcerradam + "</td><td> <span>" + datos.evcerradat + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evcerradat != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(11)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";


        tblEvaluaciones += "<td>" + datos.evcerradahant + "</td><td>" + datos.evcerradamant + "</td><td><span>" + datos.evcerradaantt + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evcerradaantt != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(12)'></i>";
        }

        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";


        tblEvaluaciones += "</tr>";


        tblEvaluaciones += "<tr><td>" + datos.evfirmadah + "</td> <td>" + datos.evfirmadam + "</td><td><span>" + datos.evfirmadat + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evfirmadat != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(13)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";

        
        tblEvaluaciones += "<td>" + datos.evfirmadahant + "</td><td>" + datos.evfirmadamant + "</td> <td> <span>" + datos.evfirmadaantt + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evfirmadaantt != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(14)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";

        tblEvaluaciones += "</tr>";


        tblEvaluaciones += "<tr><td>" + datos.evautomaticah + "</td><td>" + datos.evautomaticam + "</td><td> <span>" + datos.evautomaticat + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evautomaticat != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(15)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";

        tblEvaluaciones += "<td>" + datos.evautomaticahant + "</td><td>" + datos.evautomaticamant + "</td><td><span>" + datos.evautomaticaantt + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evautomaticaantt != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(16)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";

        tblEvaluaciones += "</tr>";

        tblEvaluaciones += "<tr><td>" + datos.evht + "</td> <td>" + datos.evmt + "</td><td><span>" + datos.evt + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evt != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(17)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";
        
        tblEvaluaciones += "<td>" + datos.evhantt + "</td><td>" + datos.evmantt + "</td><td> <span>" + datos.evantt + "</span></td>";

        tblEvaluaciones += "<td class='pl'>";
        if (datos.evantt != 0) {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o' onclick='informes(18)'></i>";
        }
        else {
            tblEvaluaciones += "<i class='fa fa-file-pdf-o transparent'></i>";
        }
        tblEvaluaciones += "</td>";
        
        tblEvaluaciones+= "</tr></tbody>";

        
        //Inyectar html en la página
        $("#tblProfesionales").html(tblProfesionales);
                
        setTimeout(function () {
            $("#tblEvaluaciones").html(tblEvaluaciones);
        }, 20);

        //$("#tblEvaluaciones").html(tblEvaluaciones)
        
    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }
}

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

        if ($('#evaluador').attr("data-sexo") == "V") $("#lblEvaluador").text("Evaluador")
        else $("#lblEvaluador").text("Evaluadora")

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
function ObtenerEvaluadores()
{

    if ($("#txtApellido1").val() == "" && $("#txtApellido2").val() == "" && $("#txtNombre").val() == "") {
        alertNew("warning", "No se permiten hacer búsquedas con filtros vacíos.", null, 4000, null);
        return;
    }

    $('#lisEvaluadores').children().remove();
    $lisEvaluadores = $('#lisEvaluadores');
    actualizarSession();
   
    $.ajax({
        url: "Default.aspx/getEvaluadores",   // Current Page, Method
        data: JSON.stringify({ t001_idficepi: idficepi, perfilApl: origen, t001_apellido1: $('#txtApellido1').val(), t001_apellido2: $('#txtApellido2').val(), t001_nombre: $('#txtNombre').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 30000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li class='list-group-item' data-sexo='" + this.Sexo + "' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });
                $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluadores').modal('show');

            } else {
                alertNew("warning", "No se encuentran evaluadores/as bajo tu ámbito de visión que respondan al filtro establecido.", null, 5000, null);
                return;
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

//****Modal Catálogo de fotos
//Obtiene el Catálogo de fotos
function MostrarFotos()
{
    $('#tbdFoto').children().remove();
    $tbdFoto = $('#tbdFoto');
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getFotos",   // Current Page, Method
        data: JSON.stringify(),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () {
                    var fecha = new Date(parseInt(this.t932_fechafoto.substr(6)));
                    var d = fecha.getDate();
                    if (d < 10) d = "0" + d;
                    var m = fecha.getMonth();
                    m += 1;
                    if (m < 10) m = "0" + m;
                    var y = fecha.getFullYear();

                    $("<tr idfoto='" + this.t932_idfoto + "'><td>" + this.t932_denominacion + "</td><td>" + d + "/" + m + "/" + y + "</td><td>" + this.nombre + "</td></tr>").appendTo($tbdFoto);
                });

                $('#modal-foto').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-foto').modal('show');

            } else {
                alertNew("warning", "No existen fotos");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener las fotos");
        }
    });
}


$("#btnExcel").on("click", function () {
    excel();
})

//Botón Catálogo de Fotos (al hacer click sobre el botón de Fotos)
$('#btnFoto').on('click', function () {
    MostrarFotos();
});

//Botón Salir del Catálogo de fotos
$('#btnSalir').on('click', function () {    
    $('#modal-foto').modal('hide');    
});

//Selección de fotos
$('body').on('click', '#tbdFoto tr', function (e) {
    $('#tbdFoto tr').removeClass('active');
    $(this).addClass('active');
});

//Borrar foto
$('#btnBorrarFoto').on('click', function () {

    if ($('#tbdFoto tr.active').length > 0) {
        $("#modal-EliminarFoto").modal("show");
        $("#txtConfirmacionEliminacion").html("Pulsa 'Confirmar eliminación' para borrar la foto:</br></br><strong> " + $('#tbdFoto tr.active').find('td:eq(0)').text() + "</strong>");
    }
    else {
        alertNew("warning", "Debes seleccionar la foto que quieres eliminar")
    }
    
   
});

$("#btnConfirmacionEliminacion").on('click', function () {    
    borrarFoto();
    $("#modal-EliminarFoto").modal("hide");
});

//****Fin Modal Catálogo de fotos

//****Modal Nueva foto
//Botón insertar foto (desde el catálogo de fotos)
$('#btnInsertarFoto').on('click', function () {
    $('#modal-foto-i').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    $('#txtNombreFoto').val('');

    $('#modal-foto-i').modal('show');
});

//Poner el foco en la caja de texto
$('#modal-foto-i').on('shown.bs.modal', function () {
    $('#txtNombreFoto').focus();
})

//Botón aceptar
$('#modal-foto-i #btnAceptarFoto').on('click', function () {
    //Se introduce algún caracter en la caja
    if (!comprobarFoto()) return;   

    hacerFoto();
    
    
});




//Botón cancelar
$('#modal-foto-i #btnCancelarFoto').on('click', function () {
    $('#modal-foto-i').modal('hide');
});
//Fin Modal Nueva Foto

function comprobarfechas() {
    try {
        $anoIni = $('#selAnoIni'); $mesIni = $('#selMesIni'); $anoFin = $('#selAnoFin'); $mesFin = $('#selMesFin');
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

function comprobarFoto() {
    try {
        if ($('#txtNombreFoto').val().trim().length == 0) {
            alertNew('warning', 'Debes introducir una denominación para la nueva foto', null, 2000, null);
            return false;
        } else {
            return true;
        }

    } catch (e) {
        alertNew("danger", "Ocurrió un error al comprobar la foto.");
    }
}

function informes(opcion) {
    //alert(opcion);
    var strUrlPag = "Informe/Default.aspx?opcion=" + opcion+"&pantalla=1";
    var bScroll = "no";
    var bMenu = "no";
    if (screen.width == 800) bScroll = "yes";
    bMenu = "yes";
    window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
}


/*EXCEL*/
function excel() {
    try {
        
        alert("En construcción");
        rfExcel = getRequestFilter();

        var sb = new StringBuilder;
        sb.Append("&FILTROS=");
        sb.Append("t932_idfoto:" + rfExcel.t932_idfoto + ":Situación:" + $('#cboSituacion').find(":selected").text() + "|");

        sb.Append("Desde:" + rfExcel.Desde + ":Desde:" + rfExcel.Desde + "|");
        sb.Append("Hasta:" + rfExcel.Hasta + ":Hasta:" + rfExcel.Hasta + "|");
        sb.Append("t001_fecantigu:" + rfExcel.t001_fecantigu + ":Fecha de antigüedad :" + rfExcel.t001_fecantigu + "|");

        sb.Append("Profundidad:" + rfExcel.Profundidad + ":Profundidad :" + $('#cboProfundizacion').find(":selected").text() + "|");
        sb.Append("t001_idficepi:" + rfExcel.t001_idficepi + ":Evaluador :" + $("#evaluador").val() + "|");

        sb.Append("t941_idcolectivo:" + rfExcel.t941_idcolectivo + ":Colectivo :" + $('#cboColectivo').find(":selected").text());
        
        //HASTA AQUÍ

        //sb.Append("T513_CODLINEA:" + $I("txtreferencia").value + ":Referencia:" + $I("txtreferencia").value + "|");
        //sb.Append("T512_FIRME:" + $("#" + $I("rdbfirme").id + " input:checked").val() + ":Tipo pedido:" + $("#" + $I("rdbfirme").id + " input:checked").next().text() + "|");
        //sb.Append("T513_ESTADO:" + $("#" + $I("rdbestado").id + " input:checked").val() + ":Estado:" + $("#" + $I("rdbestado").id + " input:checked").next().text() + "|");
        //sb.Append("T392_IDSUPERNODO2:" + $I("cbounidad").value + ":Unidad:" + $("#" + $I("cbounidad").id + " option:selected").text() + "|");
        //sb.Append("T303_IDNODO:" + $I("cboarea").value + ":Area:" + $("#" + $I("cboarea").id + " option:selected").text() + "|");
        //sb.Append("T001_IDFICEPISOL:" + $I("hdnsolicitante").value + ":Solicitante:" + $I("txtsolicitante").value + "|");
        //sb.Append("stridfase:" + fase.substring(0, fase.length - 1) + ":Fase:" + sFase.substring(0, sFase.length - 1) + "|");
        //sb.Append("T035_IDCODPERFIL:" + $I("cboPerfil").value + ":Perfil:" + $("#" + $I("cboPerfil").id + " option:selected").text() + "|");
        //sb.Append("stridconocimiento:" + cono.substring(0, cono.length - 1) + ":Conocimiento Principal:" + sCono.substring(0, sCono.length - 1) + "|");
        //sb.Append("T001_IDFICEPIGES:" + $I("cbogestor").value + ":Gestor:" + $("#" + $I("cbogestor").id + " option:selected").text() + "|");
        //sb.Append("T172_IDPAIS:" + $I("cbopais").value + ":País:" + $("#" + $I("cbopais").id + " option:selected").text() + "|");
        //sb.Append("T173_IDPROVINCIA:" + $I("cboprovincia").value + ":Provincia:" + $("#" + $I("cboprovincia").id + " option:selected").text() + "|");
        //sb.Append("cliente:" + $I("hdncliente").value + ":Cliente:" + $I("txtcliente").value + "|");
        //sb.Append("T512_FECHASOLD:" + $I("txtdesde").value + ":Desde:" + $I("txtdesde").value + "|");
        //sb.Append("T512_FECHASOLH:" + $I("txthasta").value + ":Hasta:" + $I("txthasta").value + "|");
        //sb.Append("T516_MOSTRAR:" + $("#" + $I("rdbenvio").id + " input:checked").val() + ":Envío de currículums:" + $("#" + $I("rdbenvio").id + " input:checked").next().text() + "|");
        //sb.Append("T512_OBJETIVO:" + $("#" + $I("rdbfinalidad").id + " input:checked").val() + ":Finalidad:" + $("#" + $I("rdbfinalidad").id + " input:checked").next().text() + "|");
        //sb.Append("nOrden:" + $I("hdnOrden").value + "|");
        //sb.Append("nAscDesc:" + $I("hdnAscDesc").value);

        exportar("../../../../Documentos/ExportExcel.aspx?EXP=ESTADISTICAS1" + sb.ToString());
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}


function exportar(url) {
    //var ifrExportacion = $("ifrExportacion");
    
        ifrExportacion = document.createElement("iframe");
        ifrExportacion.id = "ifrExportacion";
        ifrExportacion.style.display = "none";
        ifrExportacion.src = url;
        document.body.appendChild(ifrExportacion);
    
        ifrExportacion.src = url;
    
}
$("#btnExportarPDF").on("click",function(){
    var sOrigen="";
    if (origen != "ADM") sOrigen="1";
    else sOrigen="2";

    var strUrlPag = "Informe/Filtros.aspx?origen=" + sOrigen;
    var bScroll = "no";
    var bMenu = "no";
    if (screen.width == 800) bScroll = "yes";
    bMenu = "yes";
    window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
})


$("#btnRestablecer").on("click", function () {
    restablecerFiltros();
})

function restablecerFiltros() {
    location.href = "Default.aspx?origen="+ origen+"";
}
