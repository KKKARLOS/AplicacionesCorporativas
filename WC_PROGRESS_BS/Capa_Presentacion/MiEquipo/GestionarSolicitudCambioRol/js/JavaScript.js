
$(document).ready(function () {
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "350px");
        }
    }

    //manejador de fechas
    moment.locale('es');

    solicitudCambioRolCat();
})


function solicitudCambioRolCat() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/solicitudCambioRolCat",
        //"data": JSON.stringify(),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {            
            //Hay que parsear el resultado JSON que devuelve el servidor
            pintarDatosPantalla(result.d);
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido obtener mis solicitudes de cambio de rol");
        }
    });
}


function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  
        
        var tblSolicitudesRol = "";
        //$idficepi = datos.idficepi;

        for (var i = 0; i < datos.length; i++) {
                       
            tblSolicitudesRol += "<tr data-correointeresado='" + datos[i].correointeresado + "' data-nombre_interesado='" + datos[i].nombre_interesado + "' data-nombreapellidos_promotor='"+ datos[i].nombreapellidos_promotor +"' data-nombreapellidos_interesado='" + datos[i].nombreapellidos_interesado + "' data-correoresporigen='" + datos[i].correoresporigen + "' data-nombre_promotor='" + datos[i].nombre_promotor + "' nombrecorto='" + datos[i].Nombrecorto + "' id=" + datos[i].t940_idtramitacambiorol + " idficepi= " + datos[i].t001_idficepi_interesado + " idrol=" + datos[i].t004_idrol_propuesto + ">";

            tblSolicitudesRol += "<td  data-value='nomProfesional'><span>" + datos[i].Profesional + "</span></td>";
            tblSolicitudesRol += "<td data-value='desrolActual'><span>" + datos[i].t940_desrolActual + "</span></td>";
            tblSolicitudesRol += "<td data-value='desrolPropuesto'><span>" + datos[i].t940_desrolPropuesto + "</span></td>";
            //tblSolicitudesRol += "<td data-value='t940_fechaproposicion'><span>" + moment(datos[i].t940_fechaproposicion).format('DD/MM/YYYY'); + "</span></td>";

            if (datos[i].t940_motivopropuesto != "") {
                tblSolicitudesRol += "<td><a data-value='motivo' data-placement='left' data-toggle='popover' title='' href='#' data-content='" + datos[i].t940_motivopropuesto + "'><i class='glyphicon glyphicon-comment text-primary'></i></a></td>";
            }
            else { tblSolicitudesRol += "<td style'width:16px'></td>"; }

            tblSolicitudesRol += "</tr>";
            
        };

        //Inyectar html en la página        
        $("#tblSolicitudesRol").html(tblSolicitudesRol);

        $("a[data-toggle=tooltip]").tooltip();

        if ($("#tblSolicitudesRol tr").length == 0) {
            $("#divpietablaSolicitudes").css("display", "none");
        }
        else {
            $("#divpietablaSolicitudes").css("display", "block");
        }


        $('[data-toggle="popover"]').popover({            
            trigger: 'hover',
            html: true
        });

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}


//Selección simple de profesionales en trámite
$('body').on('click', '#tblSolicitudesRol tr', function (e) {
    $('#tblSolicitudesRol tr').removeClass('active');
    $(this).addClass('active');
});


//Foco en pantalla (Motivo de rechazo)
$('#modal-NOaceptarPropuesta').on('shown.bs.modal', function () {
    $('#txtMotivo').focus();
})


$("#btnAceptarPropuesta").on("click", function () {

    $tblSolicitudesRol = $("#tblSolicitudesRol tr.active");

    if ($tblSolicitudesRol.length > 0) {
        $("#modal-aceptarPropuesta").modal("show");
        $("#txtAceptarPropuesta").text("Para aprobar el cambio de rol de "+ $tblSolicitudesRol.attr("data-nombreapellidos_interesado") + ", pulsa 'Confirmar aceptación'");

        $("#txtRolActual").text($tblSolicitudesRol.find("td[data-value=desrolActual]").text());
        $("#txtRolPropuesto").text($tblSolicitudesRol.find("td[data-value=desrolPropuesto]").text());
    }

    else {
        alertNew("warning", "Tienes que seleccionar algún profesional");
    }
            
})


$("#btnNoAceptarPropuesta").on("click", function () {
    $tblSolicitudesRol = $("#tblSolicitudesRol tr.active");

    if ($tblSolicitudesRol.length > 0) {
        $("#modal-NOaceptarPropuesta").modal("show");
        $("#txtNoAceptarPropuesta").text("Rechaza el cambio de rol propuesto indicando un motivo.");
    }

    else {
        alertNew("warning", "Tienes que seleccionar algún profesional")
    }
    
})


function limpiar() {
    $("#txtMotivo").val("");
}


$("#btnNOaceptar").on("click", function () {

    if ($("#txtMotivo").val().length == 0) {
        alertNew("warning", "Tienes que indicar un motivo")
        $("#txtMotivo").focus();
        return;
    }

    if ($("#txtMotivo").val().length < 10) {
        alertNew("warning", "Tienes que ampliar la descripción del motivo", null, 4000, null);
        $("#txtMotivo").focus();
        return;
    }


    $tblSolicitudesRol = $("#tblSolicitudesRol tr.active");
    
    actualizarSession();
    $.ajax({
        url: "Default.aspx/RechazarPropuesta",   // Current Page, Method                            
        data: JSON.stringify({ t940_idtramitacambiorol: $tblSolicitudesRol.attr("id"), motivorechazo: $("#txtMotivo").val(), nombre_promotor: $tblSolicitudesRol.attr("data-nombreapellidos_promotor"), nombreapellidosinteresado: $tblSolicitudesRol.attr("data-nombreapellidos_interesado"), rolantiguo: $tblSolicitudesRol.find("td[data-value=desrolActual]").text(), rolnuevo: $tblSolicitudesRol.find("td[data-value=desrolPropuesto]").text(), correoresporigen: $tblSolicitudesRol.attr("data-correoresporigen"), motivo: $tblSolicitudesRol.find("td:last a").attr("data-content") }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $('#modal-NOaceptarPropuesta').modal('hide');
                
            //Borramos la fila seleccionada
            $tblSolicitudesRol.remove();
            limpiar();

            if ($("#tblSolicitudesRol tr").length == 0) {
                $("#divpietablaSolicitudes").css("display", "none");
            }
            else {
                $("#divpietablaSolicitudes").css("display", "block");
            }
                
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido No aceptar la propuesta");
        }
    });



})


$("#btnAceptar").on("click", function () {

    $tblSolicitudesRol = $("#tblSolicitudesRol tr.active");


    //Falta pasar el correo del interesado al Webmethod
    actualizarSession();
    $.ajax({
        url: "Default.aspx/AceptarPropuesta",   // Current Page, Method                
        data: JSON.stringify({ t940_idtramitacambiorol: $tblSolicitudesRol.attr("id"), idficepi_interesado: $tblSolicitudesRol.attr("idficepi"), t004_idrol_propuesto: $tblSolicitudesRol.attr("idrol"), nombre_promotor: $tblSolicitudesRol.attr("data-nombre_promotor"), nombreapellidosinteresado: $tblSolicitudesRol.attr("data-nombreapellidos_interesado"), rolantiguo: $tblSolicitudesRol.find("td[data-value=desrolActual]").text(), rolnuevo: $tblSolicitudesRol.find("td[data-value=desrolPropuesto]").text(), correoresporigen: $tblSolicitudesRol.attr("data-correoresporigen"), nombre_interesado: $tblSolicitudesRol.attr("data-nombre_interesado"), nombreapellidos_promotor: $tblSolicitudesRol.attr("data-nombreapellidos_promotor"), correointeresado: $tblSolicitudesRol.attr("data-correointeresado"), motivo: $tblSolicitudesRol.find("td:last a").attr("data-content") }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $('#modal-aceptarPropuesta').modal('hide');
            $tblSolicitudesRol.remove();
            limpiar();
            
            if ($("#tblSolicitudesRol tr").length == 0) {
                $("#divpietablaSolicitudes").css("display", "none");
            }
            else {
                $("#divpietablaSolicitudes").css("display", "block");
            }
            
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido No aceptar la propuesta");
        }
    });
})
