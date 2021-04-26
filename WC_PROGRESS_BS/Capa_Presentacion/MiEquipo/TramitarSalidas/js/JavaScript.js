
$(document).ready(function () {

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "320px");
        }
    }


    miEquipo();

    //Inicializamos tooltip
    $("a[data-toggle=tooltip]").tooltip();
   

    //Selección de equipo
    $('body').on('click', '#divMiEquipo #lisMiEquipo tr', function (e) {
        lista = $(this).parent().children();
        if (e.shiftKey && lista.filter('.active').length > 0) {
            first = lista.filter('.active:first').index();//Primer seleccionado
            last = lista.filter('.active:last').index();//Último seleccionado
            $('#divMiEquipo #lisMiEquipo tr').removeClass('active');//Borrar de las dos listas
            if ($(this).index() > first)
                lista.slice(first, $(this).index() + 1).addClass('active');
            else
                lista.slice($(this).index(), last + 1).addClass('active');
        }
        else if (e.ctrlKey) {
            $(this).toggleClass('active');
        } else {
            //$('#divMiEquipo #lisMiEquipo tr').removeClass('active');
            //$(this).addClass('active');
            $(this).toggleClass('active');
        }
    });


    //Seleccionar todos o ninguno
    $('#myteam .selector').on('click', function () {
        var $checkBox = $(this);
        if (!$checkBox.hasClass('selected')) {
            //$checkBox.addClass('selected').closest('#lisMiEquipo').find('tr:not(.active)').addClass('active');
            $checkBox.addClass('selected');
            
            var $itemsEvalabiertas = $("#lisMiEquipo tr:not(.fk-evalAbierta)");

            $($itemsEvalabiertas).not(".active").addClass("active");

            $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
        } else {
            //$checkBox.removeClass('selected').closest('#lisMiEquipo').find('tr.active').removeClass('active');
            $checkBox.removeClass('selected');
            $("#lisMiEquipo tr.active").removeClass("active");

            $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
        }
    });

    
    ////Contabiliza el número de caracteres escritos en un textarea
    $('#numCaracteres').text('750 caracteres disponibles');
    $('#numCaracteresAnulacion').text('750 caracteres disponibles');
    //$('#numCaracteresMediacion').text('750 caracteres disponibles');


    $('#txtComentario').keyup(function (e) {
        var max = 750;
        var len = $(this).val().length;
        if (len >= max) {
            $('#numCaracteres').text('Límite alcanzado');
            $('#numCaracteres').addClass('text-danger');

            /*Una vez alcanzado el límite, sólo permitimos el backspace*/
            if (e.keyCode !== 8) {
                e.preventDefault();                                
            }

        }
        else {
            var ch = max - len;
            $('#numCaracteres').text(ch + ' caracteres disponibles');
            $('#numCaracteres').removeClass('disabled');
            $('#numCaracteres').removeClass('text-danger');
        }
    });

    $('#txtComentarioAnulacion').keyup(function (e) {
        var max = 750;
        var len = $(this).val().length;
        if (len >= max) {
            $('#numCaracteresAnulacion').text('Límite alcanzado');
            $('#numCaracteresAnulacion').addClass('text-danger');

            /*Una vez alcanzado el límite, sólo permitimos el backspace*/
            if (e.keyCode !== 8) {
                e.preventDefault();
            }

        }
        else {
            var ch = max - len;
            $('#numCaracteresAnulacion').text(ch + ' caracteres disponibles');
            $('#numCaracteresAnulacion').removeClass('disabled');
            $('#numCaracteresAnulacion').removeClass('text-danger');
        }
    });


    $('#txtComentarioMediacion').keyup(function (e) {
        var max = 750;
        var len = $(this).val().length;
        if (len >= max) {
            $('#numCaracteresMediacion').text('Límite alcanzado');
            $('#numCaracteresMediacion').addClass('text-danger');

            /*Una vez alcanzado el límite, sólo permitimos el backspace*/
            if (e.keyCode !== 8) {
                e.preventDefault();
            }

        }
        else {
            var ch = max - len;
            $('#numCaracteresMediacion').text(ch + ' caracteres disponibles');
            $('#numCaracteresMediacion').removeClass('disabled');
            $('#numCaracteresMediacion').removeClass('text-danger');
        }
    });


    $('[data-toggle="popover"]').popover({        
        trigger: 'hover',
        html: true
        
    });

    
})


function miEquipo() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/miEquipo",
        //"data": JSON.stringify(),
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
            mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
        }
    });
   
}



function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML   (MI EQUIPO)
        var lisMiEquipo = "";
        var icono = "";


        for (var i = 0; i < datos.profesionales.length; i++) {
            if (datos.profesionales[i].estado == 1 || datos.profesionales[i].estado == 3 || datos.profesionales[i].estado == 6) continue;
            //lisMiEquipo += "<li class='list-group-item' value=" + datos.profesionales[i].idficepi + ">" + datos.profesionales[i].prof + "</li>";            
            var clase = "";
            if (datos.profesionales[i].evaluacionEnCurso == true) {
                icono = "<i data-toggle='popover' data-placement='top' title='' data-content='Evaluación pendiente de la firma de "+ datos.profesionales[i].nombre+"'><i data-toggle='tooltip' data-placement='bottom' title='' class='glyphicon fa fa-file-text-o text-danger azul'></i>";                
            }


            //<a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Evalución pendiente de la firma del evaluador/a."><i class="fa fa-file-text-o verde"></i><span id="spanEvalAbierta">Evaluación abierta</span></a> 

            else if (datos.profesionales[i].evaluacionAbierta == true) {
                icono = "<i data-toggle='popover' data-placement='top' title='' data-content='Evaluación pendiente de tu firma' class='glyphicon fa fa-file-text-o text-danger fk-evalAbierta verde'></i>";
                clase = "fk-evalAbierta";
            }

            else { icono = ""; }


            if (datos.profesionales[i].estado == 1 || datos.profesionales[i].estado == 3 || datos.profesionales[i].estado == 6) {
                clase = "fk-salidaTramite";
            }

            lisMiEquipo += "<tr  data-nombre='" + datos.profesionales[i].nombre + "' data-nombreevaluadordestino='" + datos.profesionales[i].nombreevaluadordestino + "' data-nombreapellidosprofesional='" + datos.profesionales[i].nombreapellidosprofesional + "' id=" + datos.profesionales[i].idficepi + " class=" + clase + "><td>" + icono + "</td><td><span style='margin-left:4px'>" + datos.profesionales[i].prof + "</span></td></tr>";
        };

    
        //Inyectar html en la página
        $("#lisMiEquipo").html(lisMiEquipo);

        $("[data-toggle=tooltip]").tooltip();
        
        

        var tblSalidasEnTramite = "";

        for (var i = 0; i < datos.profesionales.length; i++) {
            
        
            //Estado de la petición (1-- Tramitada 3--Rechazada 6--Solicitada
            if (datos.profesionales[i].estado == 1 || datos.profesionales[i].estado == 3 || datos.profesionales[i].estado == 6)
            {
                tblSalidasEnTramite += "<tr data-idficepi='" + datos.profesionales[i].idficepievaluadordestino + "' data-correoevaluadordestino= '" + datos.profesionales[i].correoevaluadordestino + "'  data-nombreevaluadordestino='" + datos.profesionales[i].nombreevaluadordestino + "' data-nombreapellidosprofesional='" + datos.profesionales[i].nombreapellidosprofesional + "'  idpeticion=" + datos.profesionales[i].t937_idpetcambioresp + "  estado = " + datos.profesionales[i].estado + ">";
               
                if (datos.profesionales[i].estado == 1) tblSalidasEnTramite += "<td><span class='glyphicon glyphicon-new-window text-danger fk-salidaTramite'></span></td>";
                else if (datos.profesionales[i].estado == 3) tblSalidasEnTramite += "<td><span class='fa fa-compress text-danger fk-salidaTramite'></span></td>";
                else if (datos.profesionales[i].estado == 6) tblSalidasEnTramite += "<td><span class='glyphicon glyphicon-user text-danger fk-salidaTramite'" + datos.profesionales[i].t937_estadopeticion + "</span></td>";

                tblSalidasEnTramite += "<td data-value='nomProfesional'><span>" + datos.profesionales[i].prof + "</span></td>";
                tblSalidasEnTramite += "<td data-value='nomProfesionalDestino'><span>" + datos.profesionales[i].respdestino + "</span></td>";
                tblSalidasEnTramite += "<td><span style='margin-left:4px'>" + datos.profesionales[i].t937_fechainipeticion + "</span></td>";


                if (datos.profesionales[i].t937_comentario_resporigen != "") {
                    tblSalidasEnTramite += "<td><a class='fk_izquierda'  data-toggle='popover' title='' href='#' data-content='" + datos.profesionales[i].t937_comentario_resporigen + "'><i class='glyphicon glyphicon-comment text-primary'></i></a></td>";
                }
                else { tblSalidasEnTramite += "<td style'width:16px'></td>"; }
                
                if (datos.profesionales[i].t937_comentario_respdestino != "") {
                    tblSalidasEnTramite += "<td><a class='fk_izquierda' data-toggle='popover' title='' href='#' data-content='" + datos.profesionales[i].t937_comentario_respdestino + "'><i class='glyphicon glyphicon-comment text-danger'></i></a></td>";
                }
                else { tblSalidasEnTramite += "<td style'width:16px'></td>"; }

                tblSalidasEnTramite += "</tr>";

                //Pintamos el pie de la tabla
                $("#divpietablaSalidas").addClass("show");
                                
            } 
           
        };

        $("#tblSalidasEnTramite").html(tblSalidasEnTramite);

        //$("a.fk_izquierda[data-toggle=tooltip]").tooltip();

        $('a.fk_izquierda[data-toggle="popover"]').popover({
            placement:'left',
            trigger: 'hover',
            html: true
        });

        $('i[data-toggle="popover"]').popover({
            placement: 'top',
            trigger: 'hover',
            html: true
        });



        /*Controlamos si el contenedor tiene Scroll*/
        var div = document.getElementById('tblSalidasEnTramite');

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
        if (hasVerticalScrollbar) {
            $("#tablaSalidasEnTramite th:eq(1)").css("width", "34%");
            $("#tablaSalidasEnTramite th:eq(2)").css("width", "34.8%");

        }
        else {
            
            $("#tablaSalidasEnTramite th:eq(1)").css("width", "35%");
            $("#tablaSalidasEnTramite th:eq(2)").css("width", "35.5%");
        }
        /*FIN Controlamos si el contenedor tiene Scroll*/
       

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}


$("#btnCancelarAnulacion").on("click", function () {
    $("#divMotivo").css("display", "none");
    $("#txtComentarioAnulacion").val("");
    $('#numCaracteresAnulacion').text('750 caracteres disponibles');


})

function tramitar() {
   
    $itemsActivos = $("#lisMiEquipo tr.active");

    //$evalCurso =  $("#lisMiEquipo tr.active").find("i");

    if ($itemsActivos.length > 0) {
  
        var lisMiEquipo = "";
        
        $itemsActivos.each(function(){
            $this = $(this);
            if ($this.hasClass("fk-salidaTramite")) {
                alertNew("warning", "No está permitido tramitar salidas de profesionales de tu equipo que ya se encuentren en trámite de salida", null, 5000, null);
            }
      
            else if ($this.hasClass("fk-evalAbierta"))
            {
                alertNew("warning", "No está permitido tramitar salidas de profesionales de tu equipo que tengan evaluaciones en trámite", null, 5000, null);
            }

            else {
                $('#modal-tramitar').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-tramitar').modal('show');
                lisMiEquipo += "<tr id='" + this.id + "' ><td>" + this.children[1].innerText + "</td></tr>";
            }
        });
        //Inyectar html en la página
        $("#tblModalTramitar").html(lisMiEquipo);
    }

    else {
        alertNew("warning", "Tienes que seleccionar algún profesional");
    }

}


function comprobarCambios()
{
       $("#txtDestinatario").val("");
       $("#txtComentario").val("");
       $("#tblgetFicepi").html("");
       $("#inputApellido1").val("");
       $("#inputApellido2").val("");
       $("#txtNombre").val("");      
}



//$("#modal-ficepi :input[input[type=text]]").on("keyup", function () {
    
//})

$('#modal-ficepi :input[type=text]').on('keyup', function (event) {

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#tblgetFicepi").length > 0)
    {
        $("#tblgetFicepi").html("");
    }


    if (event.keyCode == 13) {
                
        $filtros = $("#txtBusqueda input");
        $profesionales = $('#tblgetFicepi li.active');

        var $equipoOrigen = $("#lisMiEquipo tr.active").attr("id");
        
        if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "" && $("#tblgetFicepi li").length == 0) {
            alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
            return;
        }
        actualizarSession();
        $.ajax({
            url: "Default.aspx/getFicepi",   // Current Page, Method
            //        data: JSON.stringify({ t001_apellido1: $('#txtApellido1'), t001_apellido2: $('#txtApellido2'), t001_nombre: $('#txtNombre') }),  // parameter map as JSON
            data: JSON.stringify({idficepiinteresado: $equipoOrigen,  t001_apellido1: $filtros[0].value, t001_apellido2: $filtros[1].value, t001_nombre: $filtros[2].value }),  // parameter map as JSON
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                $result = $(result.d);
                if ($result.length > 0) {
                                        
                    pintarDatosFicepi($result);

                    $('#modal-ficepi').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                    $('#modal-ficepi').modal('show');
                } else {
                    alertNew("warning", "No existen profesionales");
                }
            },
            error: function (ex, status) {
                alertNew("danger", "Error al intentar obtener los profesionales");
            }
        });

    }
});


//Foco en pantalla de obtener posibles responsables destino
$('#modal-ficepi').on('shown.bs.modal', function () {
    $('#inputApellido1').focus();
})

$('#btnObtener').on('click', function (event) {
    
    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#tblgetFicepi").length > 0) {
        $("#tblgetFicepi").html("");
    }


    $filtros = $("#txtBusqueda input");
    $profesionales = $('#tblgetFicepi li.active');

    var $equipoOrigen = $("#lisMiEquipo tr.active").attr("id");

    actualizarSession();

    if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "" && $("#tblgetFicepi li").length == 0) {
        alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
        return;
    }

    $.ajax({
        url: "Default.aspx/getFicepi",   // Current Page, Method
        //        data: JSON.stringify({ t001_apellido1: $('#txtApellido1'), t001_apellido2: $('#txtApellido2'), t001_nombre: $('#txtNombre') }),  // parameter map as JSON
        //data: JSON.stringify({ t001_apellido1: $filtros[0].value, t001_apellido2: $filtros[1].value, t001_nombre: $filtros[2].value }),  // parameter map as JSON
        data: JSON.stringify({ idficepiinteresado: $equipoOrigen, t001_apellido1: $filtros[0].value, t001_apellido2: $filtros[1].value, t001_nombre: $filtros[2].value }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $('#modal-ficepi').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-ficepi').modal('show');
                pintarDatosFicepi($result);
                
            } else {
                alertNew("warning", "No existen profesionales");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los profesionales");
        }
    });

    
});


function pintarDatosFicepi(datos)
{
    try {
        //PINTAR HTML   (FICEPIS)
        var profDestino = "";
        $filtros = $("#txtBusqueda input");

        for (var i = 0; i < datos.length; i++) {
            
            profDestino += "<li class='list-group-item' value='" + datos[i].t001_idficepi + "'>" + datos[i].nombre + "</li>";
        };


        //Inyectar html en la página
        $("#tblgetFicepi").html(profDestino);
        
        //Vaciamos los filtros
        $filtros.each(function () {
            this.value = "";            
        })

        $("a[data-toggle=tooltip]").tooltip();
    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }
}


//Selección simple de responsable destino
$('body').on('click', '#tblgetFicepi li', function (e) {
    $('#tblgetFicepi li').removeClass('active');
    $(this).addClass('active');
});

//Selección simple de profesionales en trámite
$('body').on('click', '#tblSalidasEnTramite tr', function (e) {
    //$('#tblSalidasEnTramite tr').removeClass('active');
    //$(this).addClass('active');
    $(this).toggleClass('active');
});

//Botón seleccionar
$('#modal-ficepi #btnSeleccionar').on('click', function () {
    $profesionales = $('#tblgetFicepi li.active');
    $filtros = $("#txtBusqueda input");

    for (var i = 0; i < $filtros.length; i++) {

        if ($profesionales.length == 0 && $filtros[i].value == "")
        {
            alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
            return;
        }

        if ($filtros[i].value != "") {
            alertNew("warning", "Tienes que pulsar Intro para obtener los profresionales en base al filtro establecido");
            return;
        }
    }
    
    if ($profesionales.length > 0) {
        idficepi = $("#tblModalTramitar tr").map(function () { return this.id }).get();
        actualizarSession();
        $.ajax({
            url: "Default.aspx/validaEvalProgress",   // Current Page, Method                
                data: JSON.stringify({ listaid: idficepi, t001_idevalprogress: $profesionales.attr('value') }),  // parameter map as JSON
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {
                    $result = $(result.d);

                    if ($result.length > 0)
                    {                       
                        alertNew("danger", "El evaluador destino no es válido para alguno de los profesionales seleccionados", null, 5000);
                        //$("#tblModalTramitar tr[id='" + result + "']").addClass("text-danger");
                        $result.each(function () {
                            $("#tblModalTramitar tr[id='" + this + "']").addClass("tacharUsuario");
                        })
                    }

                    //Vaciamos la tabla
                    $("#tblgetFicepi").html("");
 
                },
                error: function (ex, status) {
                    alertNew("danger", "No se ha podido validar al profesional");
                }
            });
        
        
        
        $('#txtDestinatario').attr("idficepi", $profesionales.attr('value')).val($profesionales.text());
        $("#txtDestinatario").prop("disabled", true);
        $('#modal-ficepi').modal('hide');
        $("#txtComentario").parent().focus();
    }
    
    
    else {
        alertNew("warning", "Debes seleccionar un profesional");
    }

});


$('#modal-tramitar #btnConfirmarSalida').on('click', function () {


    if ($("#txtDestinatario").val() == "") {
        alertNew("danger", "Para confirmar la salida debes seleccionar evaluador/a destino");
        return;
    }

    if ($("#txtComentario").val().length == 0) {
        alertNew("warning", "Tienes que indicar un comentario", null, 4000, null);
        $("#txtComentario").focus();
        return;
    }

    if ($("#txtComentario").val().length < 10) {
        alertNew("warning", "Tienes que ampliar la descripción del comentario", null, 4000, null);
        $("#txtComentario").focus();
        return;
    }

    idficepi = $("#tblModalTramitar tr").not(".tacharUsuario").map(function () { return this.id }).get();
    var listadocorreo = $("#tblModalTramitar tr td").not(".tacharUsuario").map(function () { return this.innerText }).get();

    if (idficepi.length > 0 && $("#txtDestinatario").val() != "" ) {
        actualizarSession();
        $.ajax({
            url: "Default.aspx/insert",   // Current Page, Method                
            data: JSON.stringify({ listaProfesionales: idficepi, t001_idficepi_respdestino: $('#txtDestinatario').attr("idficepi"), t937_comentario_resporigen: $('#txtComentario').val(), listadocorreo: listadocorreo }),  // parameter map as JSON
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                pintarDatosPantalla(JSON.parse(result.d));
                //Limpiamos el campo
                $('#txtDestinatario').val("");
                $('#txtComentario').val("");
                
            },
            error: function (ex, status) {
                alertNew("danger", "No se ha podido insertar al profesional");
            }
        });

        $('#modal-tramitar').modal('hide');
        //$("#txtDestinatario").val("");
        
    }

})

bMail = false;
$("#btnAnularSalida").on("click", function () {
    $tblTramitados = $("#tblSalidasEnTramite tr.active");
    
    //var idpeticiones = $tblTramitados.map(function () { return this.getAttribute("idpeticion") }).get();

    if ($tblTramitados.attr("estado") == "6") {
        alertNew("warning", "No puedes anular la petición si se ha solicitado mediación a RRHH previamente")
    }

    
    if ($tblTramitados.length > 0) {

        $tblTramitados.map(function () { return this.getAttribute("estado") }).get();

        $tblTramitados.each(function () {
            if ($(this).attr("estado") == "1") {
                $("#divMotivo").css("display", "block");
            }           
        })

        //Se pueden anular las rechazadas (3) y las tramitadas (1).. nunca las solicitadas mediación (6)
        //if ($tblTramitados.attr("estado") == "1") {
        //    $("#divMotivo").css("display", "block");
        //    bMail = true;
        //}
        //else if ($tblTramitados.attr("estado") == "3") {
            
        //    $("#divMotivo").css("display", "none");
        //    bMail = false;
        //}

        $("#modal-AnularSalida").modal("show");
        var lisAnulados = "";
        $('#txtComentario').val("");
        $tblTramitados.each(function () {
            lisAnulados += "<tr><td>" + this.children[1].innerText + "</td></tr>";
        })
        $("#tblModalAnularSalida").html(lisAnulados);

        //if ($tblTramitados.attr("estado") == "3" && $tblTramitados.attr("estado") != "6") {
        //    $("#modal-AnularSalida").modal("show");
        //    $('#txtComentario').val("");
        //    $("#txtProfAnulacion").text("Pulsa <<Confirmar anulación>> para anular la tramitación de salida de  " + $tblTramitados.find("td[data-value=nomProfesional]").text());
           
        //    bMail = false;
        //}
    }

    else {
        alertNew("warning", "Si quieres anular una salida debes seleccionar alguna fila")
    }
    

})


$("#btnConfirmarAnulacion").on("click", function () {


    if ($("#txtComentarioAnulacion").val().length == 0 && $("#divMotivo").css("display") == "block") {
        alertNew("warning", "Tienes que indicar un motivo")
        $("#txtComentarioAnulacion").focus();
        return;
    }


    if ($("#txtComentarioAnulacion").val().length < 10 && $("#divMotivo").css("display") == "block") {
        alertNew("warning", "Tienes que ampliar la descripción del motivo", null, 4000, null);
        $("#txtComentarioAnulacion").focus();
        return;
    }

    $tblTramitados = $("#tblSalidasEnTramite tr.active");
    var idpeticiones = $tblTramitados.map(function () { return this.getAttribute("idpeticion") }).get();

    //anularSalida(idpeticiones, bMail);
    anularSalidaMasiva(idpeticiones);
})


$("#btnConfirmarMediacion").on("click", function () {
    solicitarMediacion($tblTramitados.attr("idpeticion"));
})




$("#btnSolicitarMediacion").on("click", function () {

    $tblTramitados = $("#tblSalidasEnTramite tr.active");


    if ($tblTramitados.attr("estado") == "6") {
        alertNew("warning", "Ya se ha solicitado mediación a RRHH para esta salida en trámite")
    }


    if ($tblTramitados.length > 0) {
        if ($tblTramitados.attr("estado") != "6") {
            $("#modal-mediacion").modal("show");
            $("#txtProfMediacion").text("Si pulsas <<Confirmar mediación>>, se enviará una notificación a RRHH para que adopte una decisión sobre la salida en trámite de " + $tblTramitados.find("td[data-value=nomProfesional]").text());

        }
    }
    else {
        alertNew("warning", "Si quieres solicitar mediación debes seleccionar alguna fila")
    }
    
    
})


function anularSalida(idpeticiones, bMail) {
    actualizarSession();

    var nombresEvaluadoresDestino = $tblTramitados.map(function () { return this.getAttribute("data-nombreevaluadordestino") }).get();

    var nombreapellidosprofesional = $tblTramitados.map(function () { return this.getAttribute("data-nombreapellidosprofesional") }).get();

    var correoevaluadordestino = $tblTramitados.map(function () { return this.getAttribute("data-correoevaluadordestino") }).get();

    var estados = $tblTramitados.map(function () { return this.getAttribute("estado") }).get();

    $.ajax({
            url: "Default.aspx/anularSalida",   // Current Page, Method                
            data: JSON.stringify({ idpeticiones: idpeticiones, nombreevaluadordestino: nombresEvaluadoresDestino, nombreapellidosprofesional: nombreapellidosprofesional, motivo: $("#txtComentarioAnulacion").val(), correovaluadordestino: correoevaluadordestino, estados: estados }),  // parameter map as JSON
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                
                $('#modal-AnularSalida').modal('hide');
                //Limpiamos el campo
                $('#txtDestinatario').val("");
                $("#txtComentarioAnulacion").val("");
                pintarDatosPantalla(JSON.parse(result.d));
                
                if ($("#tblSalidasEnTramite tr").length == 0) {
                    $("#divpietablaSalidas").removeClass("show").addClass("hide");
                }
                
                
            },
            error: function (ex, status) {
                alertNew("danger", "No se ha podido anular la salida en trámite");
            }
        });
}



function anularSalidaMasiva(idpeticiones) {
    actualizarSession();

    var $salidas = $("#tblSalidasEnTramite tr.active");

    var arr = [];

    $salidas.each(function (index) {
        oProfesional = new Object();
        oProfesional.idpeticion = $(this).attr("idpeticion");
        oProfesional.idficepievaluadordestino = $(this).attr("data-idficepi");
        oProfesional.nombreevaluadordestino = $(this).attr("data-nombreevaluadordestino");;
        oProfesional.nombreapellidosprofesional = $(this).attr("data-nombreapellidosprofesional");
        oProfesional.motivo = $("#txtComentarioAnulacion").val();
        oProfesional.correoevaluadordestino = $(this).attr("data-correoevaluadordestino");
        oProfesional.estado = $(this).attr("estado");
        arr.push(oProfesional);
    });


    //Mapea los correoevaluadordestino y excluye los repetidos.
    //var Unicos = $.unique($salidas.map(function (d) {
    //    return $(this).attr("data-correoevaluadordestino");
    //}).get());

   
    //var unique = $salidas.filter(function (itm, i, a) {
    //    return i == $salidas.indexOf(itm);
    //});


    $.ajax({
        url: "Default.aspx/anularSalidaMasiva",   // Current Page, Method                
        data: JSON.stringify({ idpeticiones: idpeticiones, oProfesional: arr }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {

            $('#modal-AnularSalida').modal('hide');
            //Limpiamos el campo
            $('#txtDestinatario').val("");
            $("#txtComentarioAnulacion").val("");
            pintarDatosPantalla(JSON.parse(result.d));

            if ($("#tblSalidasEnTramite tr").length == 0) {
                $("#divpietablaSalidas").removeClass("show").addClass("hide");
            }


        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido anular la salida en trámite");
        }
    });
}

function solicitarMediacion(idpeticion)
{
    //TODO ENVIAR CORREO y actualizar estado a (6)
    actualizarSession();
    $.ajax({
        url: "Default.aspx/solicitarMediacion",   // Current Page, Method                
        data: JSON.stringify({ idpeticion: idpeticion, nombreapellidosprofesional: $("#tblSalidasEnTramite tr.active").attr("data-nombreapellidosprofesional"), motivo: $("#txtComentarioMediacion").val(), estado: $("#tblSalidasEnTramite tr.active").attr("estado"), correovaluadordestino: $("#tblSalidasEnTramite tr.active").attr("data-correoevaluadordestino") }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-mediacion").modal("hide");
            pintarDatosPantalla(JSON.parse(result.d));
            alertNew("success", "Mediación solicitada");

        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido solicitar mediación a RRHH");
        }
    });
}

//Ponemos foco a campos de la modal
$(window).on('shown.bs.modal', function () {
    $("#txtComentarioAnulacion").focus();
    $("#txtComentarioMediacion").focus();
    
});


$('body').on('click', '#lisMiEquipo tr', function (e) {
    if (!$(this).hasClass("active")) {
        if ($(this).hasClass("fk-evalAbierta")) {
            alertNew("warning", "No está permitido tramitar salidas de profesionales de tu equipo que tengan evaluaciones abiertas", null, 5000, null);
            $(this).toggleClass('active');
            return;
        }
    }
    
});