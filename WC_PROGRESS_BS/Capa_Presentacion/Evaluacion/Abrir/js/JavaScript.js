//Para saber en que punto del proceso voy
var etapa = 1;

$(document).ready(function () {
    comprobarerrores();
    try {
        //Inicializamos tooltip
        $("[data-toggle=tooltip]").tooltip();

        if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
            ver = iOSversion();
            if (ver[0] < 6) {
                $(".dual-list .list-group").css("max-height", "300px");
                $(".alturacontenedor").css("max-height", "400px");
                $("#capaPaso3 .panel-body").css("max-height", "300px");
                $("#lisImpEvaluados").css("max-height", "200px");                
            }
        }
        
        //Click en el botón siguiente y anterior
        $('#btnSiguienteEtapa1').on('click', function () { siguiente(1); });
        $('#btnAnteriorEtapa2').on('click', function () { anterior(2); });
        $('#btnSiguienteEtapa2').on('click', function () { siguiente(2); });
        $('#btnAnteriorEtapa3').on('click', function () { anterior(3); });
        $('#btnFinalizarEtapa3').on('click', function () { siguiente(3); });

        
        //Selección
        $('body').on('click', '#divMiEquipo .list-group .list-group-item', function (e) {
            lista = $(this).parent().children();
            if (e.shiftKey && lista.filter('.active').length > 0) {
                first = lista.filter('.active:first').index();//Primer seleccionado
                last = lista.filter('.active:last').index();//Último seleccionado
                $('#divMiEquipo .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                if ($(this).index() > first)
                    lista.slice(first, $(this).index() + 1).addClass('active');
                else
                    lista.slice($(this).index(), last + 1).addClass('active');
            }
            else if (e.ctrlKey) {
                $(this).toggleClass('active');
            } else {                
                $(this).toggleClass('active');
            }
        });

        //Click en los botones
        $('.list-arrows button').on('click', function () {
            var $button = $(this), actives = '';
            if ($button.hasClass('move-left')) {
                actives = $('.list-right ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('.list-left ul');
                    actives.each(function () { aEvaluar($(this).attr('value'), false); });
                    ordenar($('.list-left ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún profesional");
                }
            } else if ($button.hasClass('move-right')) {
                actives = $('.list-left ul li.active:not(.pend)');
                lisActNoPass = $('.list-left ul li.active.pend');

                if ((actives.length + lisActNoPass.length) > 0) {
                    actives.clone().appendTo('.list-right ul');
                    actives.each(function () { aEvaluar($(this).attr('value'), true); });
                    ordenar($('.list-right ul li'));
                    if (lisActNoPass.length > 0) {
                        alertNew("warning", "No puedes abrir una nueva evaluación a los profesionales que ya tienen otra evaluación abierta, en curso o están en trámite de salida del equipo.", null, 6000, null);
                    }
                } else {
                    alertNew("warning", "Tienes que seleccionar algún profesional");
                }
            }
            actives.remove();
            $('.list-group-item').removeClass('active');
            $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');

            $('[data-toggle="popover"]').popover({
                trigger: 'hover',
                html: true,

            });
            //grabar();
        });

        //Seleccionar todos o ninguno
        $('.dual-list .selector').on('click', function () {
            var $checkBox = $(this);
            if (!$checkBox.hasClass('selected')) {
                $checkBox.addClass('selected').closest('.well').find("li:not(.pend)").addClass("active");                               
                $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');                
            } else {
                $checkBox.removeClass('selected');
                $("#lisMiEquipo li.active").removeClass("active");
                $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');                
            }
        });

        //Buscar
        $('#capaPaso1 [name="SearchDualList"]').on('keyup', function (e) {
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

        //Seleccionar todos o ninguno
        $('th i').on('click', function () {
            var $checkBox = $(this);
            if (!$checkBox.hasClass('selected')) {
                $('#tbdProfesionales i.glyphicon').removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                $('#tbdProfesionales textarea').prop("disabled", false);
                $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');                
            } else {
                $('#tbdProfesionales i.glyphicon').removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                $('#tbdProfesionales textarea').prop("disabled", true);
                $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');                
            }
        });

        //Añado el evento al body pq son elementos q se generan dinámicamente
        $('body').on('click', '#tbdProfesionales i.glyphicon', function () {
            var $checkBox = $(this);
            if (!$checkBox.hasClass('selected')) {
                $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                $checkBox.closest('tr').find('textarea').prop("disabled", false);
            } else {
                $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                $checkBox.closest('tr').find('textarea').prop("disabled", true);
            }
        });

        //Seleccionar todos o ninguno
        $('#modal-imprimir .selector i').on('click', function () {
            var $checkBox = $(this);
            if (!$checkBox.hasClass('selected')) {
                $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                $('#lisImpEvaluados i').removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');                
            } else {
                $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                $('#lisImpEvaluados i').removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');                
            }
        });

        //Selección individual
        $('body').on('click', '#lisImpEvaluados i', function () {
            var $checkBox = $(this);
            if (!$checkBox.hasClass('selected')) {
                $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
            } else {
                $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
            }
        });



        $('[data-toggle="popover"]').popover({
            trigger: 'hover',
            html: true,

        });
      

    } catch (e) {
        alertNew("danger", "Ocurrió un error al iniciar la página.");
    }
});

function siguiente(etapa) {
    try {
        actualizarSession();
        if (etapa == 1) {
            seleccion = false;
            var $tblProf = $('#tbdProfesionales');
            $tblProf.children().remove();            
            for (i = 0; i < miequipo.length; i++) {
                if (miequipo[i].aevaluar) {
                    $("<tr value=" + miequipo[i].idficepi + "><td>" + miequipo[i].prof + "</td><td><i class='glyphicon glyphicon-" + ((miequipo[i].enviarcorreo) ? "check selected" : "unchecked") + "'/><td><textarea rows=4 " + ((!miequipo[i].enviarcorreo) ? "disabled" : "") + ">" + miequipo[i].textocorreo + "</textarea></td></tr>").appendTo($tblProf);
                    seleccion = true;
                }
            }
            //}
            if (seleccion) {
                $('#selEvaluados img').attr('src', '../../../imagenes/timeline.png').parent().find('p').css('color', 'grey');
                $('#confNotificaciones img').attr('src', '../../../imagenes/timelinecolor.png').parent().find('p').css('color', '#000');                                
                $('#capaPaso2').fadeIn('slow').removeClass('hide');
                $('#capaPaso1').addClass('hide');
                              
            } else {
                alertNew("warning", "Debes identificar como 'Profesionales a evaluar' al menos a un profesional", null, 3000, null);
            }
        } else if (etapa == 2) {
            var $tblResumen = $('#tbdResumen');
            $tblResumen.children().remove();
            $('#tbdProfesionales tr').each(function () {
                var $fila = $(this);
                var oProf = profesional($fila.attr("value"));
                oProf.enviarcorreo = $fila.find("td i").hasClass("selected");
                oProf.textocorreo = $fila.find("textarea").val();
                $("<tr data-profesional='"+ oProf.prof +"' value=" + oProf.idficepi + "><td>" + ((oProf.enviarcorreo) ? "<a data-toggle=popover textocorreo='" + oProf.textocorreo + "'><i class='fa fa-envelope fa-lg clickable'></i></a>" : "") + "</td><td>" + oProf.prof + "</td></tr>").appendTo($tblResumen);
            });
            $('#confNotificaciones img').attr('src', '../../../imagenes/timeline.png').parent().find('p').css('color', 'grey');
            $('#finProceso img').attr('src', '../../../imagenes/timelinecolor.png').parent().find('p').css('color', '#000');            
            $('#capaPaso3').fadeIn('slow').removeClass('hide');
            $('#capaPaso2').addClass('hide');
            //Popover Leyenda
            $('#tbdResumen a[data-toggle="popover"]').each(function () {
                var $a = $(this);
                $a.popover({
                    placement: 'auto',
                    trigger: 'hover',
                    html: true,
                    content: ContenidoPopover($a),
                    title: 'Texto a enviar' + CerrarPopover()
                });
            });

        } else if (etapa == 3) {
            
            //Llamada al Webmethod de inserción de evaluaciones            
            $.ajax({
                url: "Default.aspx/insertarEvaluaciones",   // Current Page, Method
                data: JSON.stringify({ listaProfesionales: $("#tbdResumen tr").map(function () { return $(this).attr('value'); }).get() }),  // parameter map as JSON
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {

                    var $lista = $('#lisImpEvaluados');
                    var $ListaInicial = $("#lisAEvaluar li").attr("value");                    
                    var $listaProfesionalesCorreo = $("#tbdResumen tr");
                    var $ListaresultadoApertura = result.d;

                    if ($("#lisAEvaluar li").length > $ListaresultadoApertura.length) {
                        $("#txtAvisoIncidente").css("display", "block");
                    }
                    else {
                        $("#txtAvisoIncidente").css("display", "none");
                    }

                    var $listaImprmir = [];                   
                    olistaImprimir = new Object();

                    for (i = 0; i < $ListaresultadoApertura.length; i++) {

                        olistaImprimir.t930_idvaloracion = $ListaresultadoApertura[i].t930_idvaloracion;
                        olistaImprimir.t001_idficepi = $ListaresultadoApertura[i].t001_idficepi;
                        olistaImprimir.t934_idmodeloformulario = $ListaresultadoApertura[i].t934_idmodeloformulario;
                        
                        
                        var elementoTR = $("#tbdResumen tr[value=" + $ListaresultadoApertura[i].t001_idficepi + "]")
                        olistaImprimir.nombre = $(elementoTR).attr("data-profesional");
                                              
                        if (typeof  $(elementoTR).find("td a").attr("textocorreo") != "undefined") {
                            olistaImprimir.textocorreo = $(elementoTR).find("td a").attr("textocorreo");
                            olistaImprimir.checkeado = true;
                        }
                        else {
                            olistaImprimir.textocorreo = "";
                            olistaImprimir.checkeado = false;
                        } 
                        
                        idficepiElemento = $ListaresultadoApertura[i].t001_idficepi
                        var $prof = getProfesional(idficepiElemento);
 
                        $listaImprmir.push(olistaImprimir);

                        $("<li data-t934_idmodeloformulario='" + $listaImprmir[i].t934_idmodeloformulario + "' data-textocorreo='" + $listaImprmir[i].textocorreo + "' data-value='" + $prof.Correo + "' data-enviarcorreo='" + $prof.enviarcorreo + "' data-sexo='" + $prof.Sexo + "' data-t001_idficepi='" + $listaImprmir[i].t001_idficepi + "'  data-t930_idvaloracion='" + $listaImprmir[i].t930_idvaloracion + "' data-profesional='" + $listaImprmir[i].nombre + "' class='list-group-item'><i class='glyphicon glyphicon-" + (($listaImprmir[i].checkeado) ? "check selected" : "unchecked") + "'></i><span>" + $listaImprmir[i].nombre + "</span></li>").appendTo($lista);

                    }

                    var $profesionalesEnvioCorreoEnviar = [];

                    $('#lisImpEvaluados li').each(function () {                        
                        oProfesional = new Object();
                        oProfesional.prof = $(this).attr("data-profesional");
                        oProfesional.Correo = $(this).attr("data-value");
                        oProfesional.Sexo = $(this).attr("data-sexo");
                        oProfesional.textocorreo = $(this).attr("data-textocorreo");
                        oProfesional.enviarcorreo = $(this).attr("data-enviarcorreo");
                        $profesionalesEnvioCorreoEnviar.push(oProfesional);
                        
                    });


                    //LLAMA A UN WEBMETHOD PARA ENVIAR LOS CORREOS. POR UN LADO HAY QUE ENVIAR LAS EVALUACIONES ABIERTAS, Y LOS QUE HAN SIDO NOTIFICADOS.                    
                    $.ajax({
                        url: "Default.aspx/EnvioCorreos",
                        "data": JSON.stringify({ listaProfesionalesCorreo: $profesionalesEnvioCorreoEnviar }),
                        type: "POST",
                        async: true,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        timeout: 20000,
                        success: function (result) {
                            
                        },
                        error: function (ex, status) {
                            alertNew("danger", "No se han podido enviar los correos");
                        }
                    });

                    //Llamada a la modal finalizar
                    $('#modal-imprimir').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                    $('#modal-imprimir').modal('show');

                },
                error: function (ex, status) {
                    alertNew("danger", "Error al intentar insertar evaluaciones");
                }
            });
            
        }
    } catch (e) {
        alertNew("danger", "Ocurrió un error al pulsar sobre el botón siguiente.");
    }
}


//Botón Imprimir
$("#btnImprimir").on("click", function () {
    Imprimir();
});

function Imprimir() {

    var idvaloracion = "";
    var resultado = "";
    var idValoracionModelo1 = "";
    var idValoracionModelo2 = "";

    $('#lisImpEvaluados li i').each(function () {
        if ($(this).hasClass("selected")) {
            
            if ($(this).parent().attr("data-t934_idmodeloformulario") == "1") {
                idValoracionModelo1 += $(this).parent().attr("data-t930_idvaloracion") + ",";
            }
            else {
                idValoracionModelo2 += $(this).parent().attr("data-t930_idvaloracion") + ",";
            }
        }
    })

    if (idValoracionModelo1 != "") idValoracionModelo1 = idValoracionModelo1.slice(0, -1);
    if (idValoracionModelo2 != "") idValoracionModelo2 = idValoracionModelo2.slice(0, -1);

    if (idValoracionModelo1 == "" && idValoracionModelo2 == "") {
        alertNew("danger", "No has seleccionado nada para imprimir.");
        return;
    }
    
    if (idValoracionModelo1 != "") {
        var strUrlPag = "../Formularios/Modelo1/Informe/Default.aspx?IdEvaluacion=" + codpar(idValoracionModelo1) + "&modelo=1";
        var bScroll = "no";
        var bMenu = "no";
        if (screen.width == 800) bScroll = "yes";
        bMenu = "yes";
        window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
    }

    if (idValoracionModelo2 != "") {
        var strUrlPag = "../Formularios/Modelo1/Informe/Default.aspx?IdEvaluacion=" + codpar(idValoracionModelo2) + "&modelo=2";
        var bScroll = "no";
        var bMenu = "no";
        if (screen.width == 800) bScroll = "yes";
        bMenu = "yes";
        window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=" + bScroll + ",menubar=" + bMenu + ",top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
    }
   
}

function anterior(etapa) {
    try{
    if (etapa == 2) {
        $('#tbdProfesionales tr').each(function () {
            var $fila = $(this);
            var oProf = profesional($fila.attr('value'));
            oProf.textocorreo = $fila.find("textarea").val();
            oProf.enviarcorreo = $fila.find("td i").hasClass("selected");
        });
        $('#selEvaluados img').attr('src', '../../../imagenes/timelinecolor.png').parent().find('p').css('color', '#000');
        $('#confNotificaciones img').attr('src', '../../../imagenes/timeline.png').parent().find('p').css('color', 'grey');               
        $('#capaPaso1').fadeIn('slow').removeClass('hide');
        $('#capaPaso2').addClass('hide');
        etapa = 1;
    } else if (etapa == 3) {
        $('#confNotificaciones img').attr('src', '../../../imagenes/timelinecolor.png').parent().find('p').css('color', '#000');
        $('#finProceso img').attr('src', '../../../imagenes/timeline.png').parent().find('p').css('color', 'grey');                                
        $('#capaPaso2').fadeIn('slow').removeClass('hide');
        $('#capaPaso3').addClass('hide');
        etapa = 2;
    }
    } catch (e) {
        alertNew("danger", "Ocurrió un error al pulsar sobre el botón anterior.");
    }
}

function aEvaluar(idficepi, bevaluar) {
    for (i = 0; i < miequipo.length; i++) {
        if(miequipo[i].idficepi==idficepi){
            miequipo[i].aevaluar = bevaluar;
            break;
        }
    }
}

function profesional(idficepi) {
    for (i = 0; i < miequipo.length; i++) {
        if (miequipo[i].idficepi == idficepi) {
            return miequipo[i];
        }
    }
    return null;
}


function ContenidoPopover($a) {
    return '<div><span>' + $a.attr('textocorreo').replace(/\r?\n/g, '<br />') + '</span></div>';
}

function CerrarPopover() {
    // Botón de cierre dentro del Popover
    return '<button onclick="$(this).closest(\'div.popover\').popover(\'hide\');" type="button" class="close" aria-hidden="true">&nbsp; &times;</button>';
}

$('#modal-finalizar .modal-footer button').on("click", function () {
    var $this = $(this);
    if ($this.hasClass("btn-primary"))
        location.href = strServer +"Capa_Presentacion/Evaluacion/CompletarAbierta/Default.aspx";
    else
        location.href = strServer +"Capa_Presentacion/Home/Home.aspx";
});


function getProfesional(idficepi) {
    for (j = 0; j < miequipo.length; j++) {
        if (miequipo[j].idficepi == idficepi) {
            return miequipo[j];
        }
    }
    return null;
}


$('body').on('click', '#lisMiEquipo li', function (e) {
    if (!$(this).hasClass("active")) {
        if ($(this).hasClass("pend")) {
            alertNew("warning", "No puedes abrir una nueva evaluación a los profesionales que ya tienen otra evaluación abierta, en curso o están en trámite de salida del equipo.", null, 6000, null);
            $(this).toggleClass('active');
            return;
        }
    }

});
