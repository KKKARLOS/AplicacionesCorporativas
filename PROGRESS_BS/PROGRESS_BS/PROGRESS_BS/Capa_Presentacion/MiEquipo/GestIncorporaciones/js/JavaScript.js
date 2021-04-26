
$(document).ready(function () {
    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "320px");
        }
    }

    miEquipo();    
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
            alertNew("danger", "No se ha podido obtener mi equipo");
        }
    });

    
}


function limpiar() {
    $("#txtMotivo").val("");
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML   (MI EQUIPO)
        var lisMiEquipo = "";
        var tblGestIncorporaciones = "";
        $idficepi = datos.idficepi;



        for (var i = 0; i < datos.profesionales.length; i++) {
            lisMiEquipo += "<tr id=" + datos.profesionales[i].idficepi + "><td><span style='margin-left:4px'>" + datos.profesionales[i].prof + "</span></td></tr>";
            //Sólo mi equipo (excluímos a las nuevas incorporaciones
           
        };

        for (var i = 0; i < datos.profesionalesEnTramite.length; i++) {
            tblGestIncorporaciones += "<tr data-Idficepievaluadordestino='" + datos.profesionalesEnTramite[i].Idficepievaluadordestino + "' data-correointeresado= '" + datos.profesionalesEnTramite[i].correointeresado + "' data-correoresporigen='" + datos.profesionalesEnTramite[i].correoresporigen + "' data-nombreresporigen='" + datos.profesionalesEnTramite[i].nombreresporigen + "' data-nombreinteresado='" + datos.profesionalesEnTramite[i].nombreinteresado + "'  data-nombreapellidosinteresado='" + datos.profesionalesEnTramite[i].nombreapellidosinteresado + "' idficepi= " + datos.profesionalesEnTramite[i].idficepi + " idpeticion=" + datos.profesionalesEnTramite[i].t937_idpetcambioresp + ">";
            tblGestIncorporaciones += "<td data-value='nomProfesional'><span style='margin-left:4px'>" + datos.profesionalesEnTramite[i].prof + "</span></td>";

            tblGestIncorporaciones += "<td><span style='margin-left:4px'>" + datos.profesionalesEnTramite[i].resporigen + "</span></td>";
            tblGestIncorporaciones += "<td><span style='margin-left:4px'>" + datos.profesionalesEnTramite[i].t937_fechainipeticion + "</span></td>";

            if (datos.profesionalesEnTramite[i].t937_comentario_resporigen != "") {
                tblGestIncorporaciones += "<td><a data-placement='left' data-toggle='popover' title='' href='#' data-content='" + datos.profesionalesEnTramite[i].t937_comentario_resporigen + "'><i class='glyphicon glyphicon-comment text-primary'></i></a></td>";
            }
            else { tblGestIncorporaciones += "<td style'width:16px'></td>"; }

            tblGestIncorporaciones += "</tr>";
        }


      
        //Inyectar html en la página
        $("#lisMiEquipo").html(lisMiEquipo);
        $("#tblIncorpEnTramite").html(tblGestIncorporaciones);

        //$("a[data-toggle=tooltip]").tooltip();
        $('[data-toggle="popover"]').popover({            
            trigger: 'hover',
            html: true
        });

        if ( $("#tblIncorpEnTramite tr").length == 0) {
            $("#divpietablaIncorporaciones").css("display", "none");
        }
        else
        {
            $("#divpietablaIncorporaciones").css("display", "block");
        } 

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}


    //Selección simple de profesionales en trámite
    $('body').on('click', '#tblIncorpEnTramite tr', function (e) {
        //$('#tblIncorpEnTramite tr').removeClass('active');
        //$(this).addClass('active');
        $(this).toggleClass('active');
    });

    $("#btnAceptarIncorporacion").on("click", function () {

        $tblIncorporaciones = $("#tblIncorpEnTramite tr.active");

        if ($tblIncorporaciones.length > 0) {
            $("#modal-AceptarIncorporacion").modal("show");

            var lisAceptados = "";
            
            $tblIncorporaciones.each(function () {
                lisAceptados += "<tr><td>" + this.children[0].innerText + "</td></tr>";
            })
            $("#tblModalAceptarIncorporacion").html(lisAceptados);

            //$("#txtAceptarIncorporacion").text("Confirma para que " + $tblIncorporaciones.find("td[data-value=nomProfesional]").text() + " pase a formar parte de tu equipo");
        }
        else {
            alertNew("warning", "Tienes que seleccionar algún profesional para aceptar su incorporación", null, 4000, null)
        }
    
    })


    $("#btnRechazarIncorporacion").on("click", function () {
        $tblIncorporaciones = $("#tblIncorpEnTramite tr.active");

        if ($tblIncorporaciones.length > 0) {
            $("#modal-RechazarIncorporacion").modal("show");
            //$("#txtRechazarIncorporacion").text("Tras cumplimentar el motivo por el cual no aceptas a  " + $tblIncorporaciones.find("td[data-value=nomProfesional]").text() + " en tu equipo, pulsa el botón <<Confirmar la no aceptación>> para finalizar el trámite.");


            $("#txtRechazarIncorporacion").text("Indica el motivo por el que rechazas a: ");

            var lisRechazados = "";

            $tblIncorporaciones.each(function () {
                lisRechazados += "<tr><td>" + this.children[0].innerText + "</td></tr>";
            })
            $("#tblModalRechazarIncorporacion").html(lisRechazados);

        }
        else {
            alertNew("warning", "Tienes que seleccionar algún profesional para rechazar su incorporación", null, 4000, null)
        }
    })


    $("#btnConfirmIncorporacion").on("click", function () {
        //TODO el profesional pasa al otro lado y confirma el equipo
    })


    //Foco en pantalla (Motivo de rechazo)
    $('#modal-RechazarIncorporacion').on('shown.bs.modal', function () {
        $('#txtMotivo').focus();
    })


    $("#btnConfirmRechazo").on("click", function () {
        

        if ($("#txtMotivo").val().length ==0) {
            alertNew("warning", "Tienes que indicar un motivo")
            $("#txtMotivo").focus();
            return;
        }

        if ($("#txtMotivo").val().length < 10) {
            alertNew("warning", "Tienes que ampliar la descripción del motivo", null, 4000, null);
            $("#txtMotivo").focus();
            return;
        }

      
        $incorporaciones = $("#tblIncorpEnTramite tr.active");

        var listapeticiones = $tblIncorporaciones.map(function () {
            return this.getAttribute("idpeticion")
        }).get();


        var array = [];

        $incorporaciones.each(function (index) {
            oProfesional = new Object();
            oProfesional.Idficepievaluadordestino = $(this).attr("data-Idficepievaluadordestino");                
            oProfesional.nombreresporigen = $(this).attr("data-nombreresporigen");                
            oProfesional.nombreapellidosinteresado = $(this).attr("data-nombreapellidosinteresado");
            oProfesional.correoresporigen = $(this).attr("data-correoresporigen");
                
            array.push(oProfesional);
        });


        actualizarSession();
        $.ajax({
            url: "Default.aspx/RechazarIncorporacion",   // Current Page, Method                
            data: JSON.stringify({ listapeticiones: listapeticiones, motivoRechazo: $("#txtMotivo").val(), oProfesional: array }),  // parameter map as JSON
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                limpiar();
                $('#modal-RechazarIncorporacion').modal('hide');
                pintarDatosPantalla(JSON.parse(result.d));
            },
            error: function (ex, status) {
                alertNew("danger", "No se ha podido rechazar la incorporación");
            }
        });
      

   
    })

    $("#btnConfirmIncorporacion").on("click", function () {
        actualizarSession();

        var $incorporaciones = $("#tablaIncorporaciones tr.active");

        var listapeticiones = $incorporaciones.map(function () {
            return this.getAttribute("idpeticion")
        }).get();

        var arr = [];
        
        $incorporaciones.each(function (index) {
            oProfesional = new Object();
            oProfesional.Idficepievaluadordestino = $(this).attr("data-Idficepievaluadordestino");
            oProfesional.idficepi_Interesado = $(this).attr("idficepi");
            oProfesional.idficepi_fin = $idficepi;
            oProfesional.nombreresporigen = $(this).attr("data-nombreresporigen");
            oProfesional.nombreinteresado = $(this).attr("data-nombreinteresado");
            oProfesional.nombreapellidosinteresado = $(this).attr("data-nombreapellidosinteresado");
            oProfesional.correointeresado = $(this).attr("data-correointeresado");
            oProfesional.correoresporigen = $(this).attr("data-correoresporigen");
            
            arr.push(oProfesional);
        });


        $.ajax({
            url: "Default.aspx/AceptarIncorporacion",   // Current Page, Method                
            data: JSON.stringify({ listapeticiones: listapeticiones, oProfesional: arr }),  // parameter map as JSON
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                
                $('#modal-AceptarIncorporacion').modal('hide');
                if (result.d == "KO") {
                    alertNew("warning", "Usuario no válido para este evaluador");
                    return;
                }
                else {
                    pintarDatosPantalla(JSON.parse(result.d));                
                }

            
            },
            error: function (ex, status) {
                alertNew("danger", "No se ha podido aceptar la incorporación");
            }
        });
    
    })

