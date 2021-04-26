$(document).ready(function () {
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "350px");
        }
    }

    //manejador de fechas
    moment.locale('es');

    solicitudesCat();
})


function solicitudesCat() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/solicitudesCat",
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
            alertNew("danger", "No se ha podido obtener el catálogo de solicitudes");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  

        var tblSolicitudesRol = "";
        //$idficepi = datos.idficepi;

        for (var i = 0; i < datos.length; i++) {
            tblSolicitudesRol += "<tr data-nomCortoInteresado='" + datos[i].nomCortoInteresado + "' data-idficepi_aprobador='" + datos[i].t001_idficepi_aprobador + "' data-nomCortoPromotor='" + datos[i].nomCortoPromotor + "' data-nomCortoAprobador='" + datos[i].nomCortoAprobador + "' data-t001_idficepi_promotor='" + datos[i].t001_idficepi_promotor + "' data-correointeresado='" + datos[i].correointeresado + "' data-nombre_interesado='" + datos[i].nombre_interesado + "' data-nombreapellidos_promotor='" + datos[i].nombre_promotor + "' data-correo_promotor='" + datos[i].CorreoPromotor + "' data-correoaprobador='" + datos[i].CorreoAprobador + "'   id=" + datos[i].t940_idtramitacambiorol + " idficepi= " + datos[i].t001_idficepi_interesado + " idrol=" + datos[i].t004_idrol_propuesto + ">";
            tblSolicitudesRol += "<td  data-value='nomProfesional'><span>" + datos[i].nombre_interesado + "</span></td>";
            tblSolicitudesRol += "<td  data-value='nomPromotor'><span>" + datos[i].nombre_promotor + "</span></td>";
            tblSolicitudesRol += "<td data-value='desrolActual'><span>" + datos[i].t940_desrolActual + "</span></td>";
            tblSolicitudesRol += "<td data-value='desrolPropuesto'><span>" + datos[i].t940_desrolPropuesto + "</span></td>";
            tblSolicitudesRol += "<td style='word-break: break-all;' data-value='motivoPropuesto'><span>" + datos[i].t940_motivopropuesto + "</span></td>";
            tblSolicitudesRol += "<td data-value='fechaProposicion'><span>" + moment(datos[i].t940_fechaproposicion).format('DD/MM/YYYY') + "</span></td>";
            tblSolicitudesRol += "<td  data-value='nomAprobador'><span>" + datos[i].aprobador + "</span></td>";
            tblSolicitudesRol += "<td data-value='fechaResolucion'><span>" + moment(datos[i].t940_fecharesolucion).format('DD/MM/YYYY') + "</span></td>";
            tblSolicitudesRol += "</tr>";
        };

        //Inyectar html en la página        
        $("#tblSolicitudes").html(tblSolicitudesRol);

        $("#numSolicitudes").html(datos.length)

        $("#tablaSolicitudes").trigger("destroy");

        //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
        $("#tablaSolicitudes").tablesorter({
            //dateFormat: "dd/mm/yy", // set the default date format
            // pass the headers argument and assing a object            
            headers: {
                // set "sorter : false" (no quotes) to disable the column
                0: {
                    sorter: "text"
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

                4: {
                    sorter: false
                },

                5: {
                    sorter: "shortDate", dateFormat: "ddmmyyyy"
                },

                6: {
                    sorter: "text"
                },

                7: {
                    sorter: "shortDate", dateFormat: "ddmmyyyy"
                }


            }
        });

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}

//Selección filas
$('body').on('click', '#tablaSolicitudes tr', function (e) {
    lista = $(this).parent().children();
    if (e.shiftKey && lista.filter('.active').length > 0) {
        first = lista.filter('.active:first').index();//Primer seleccionado
        last = lista.filter('.active:last').index();//Último seleccionado
        $('#tablaSolicitudes tr').removeClass('active');//Borrar de las dos listas
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


function comprobarFilaSeleccionada() {
    var $filasSeleccionadas = $("#tblSolicitudes tr.active");
    if ($filasSeleccionadas.length == 0)
        return false;

    return true;
}

$("#btnAceptar").on("click", function () {
    if (!comprobarFilaSeleccionada()) {
        alertNew("danger", "No has seleccionado ninguna fila");
        return;
    }

    $("#modal-aceptarPropuesta").modal("show");


    var $profesionales = $("#tblSolicitudes tr.active").find("td[data-value='nomProfesional']");
    var html = "";

    $profesionales.each(function (index, element) {
        html += "<span>" + element.firstChild.innerText + "</span></br>"
    })

    $("#divProfesionalesAceptados").html(html);

})

$("#btnRechazar").on("click", function () {
    if (!comprobarFilaSeleccionada()) {
        alertNew("danger", "No has seleccionado ninguna fila");
        return;
    }

    $("#modal-rechazarPropuesta").modal("show");

    var $profesionales = $("#tblSolicitudes tr.active").find("td[data-value='nomProfesional']");
    var html = "";

    $profesionales.each(function (index, element) {
        html += "<span>" + element.firstChild.innerText + "</span></br>"
    })

    $("#divProfesionalesRechazados").html(html);
})

$("#btnAceptarRechazo").on("click", function () {
    AprobacionNoAceptada();
})

$("#btnAceptarSolicitud").on("click", function () {
    aceptarSolicitud();
})

function aceptarSolicitud() {
    actualizarSession();
    var $filasSeleccionadas = $("#tblSolicitudes tr.active");
    var array = [];

    //Creamos una lista de objetos 
    $filasSeleccionadas.each(function (index) {
        oProfesional = new Object();
        oProfesional.t940_idtramitacambiorol = $(this).attr("id");
        oProfesional.t001_idficepi_interesado = $(this).attr("idficepi");
        oProfesional.nomCortoInteresado = $(this).attr("data-nomCortoInteresado");
        oProfesional.correointeresado = $(this).attr("data-correointeresado");
        oProfesional.nombre_interesado = $(this).find("td[data-value='nomProfesional']").text();
        oProfesional.t004_idrol_propuesto = $(this).attr("idrol");
        oProfesional.t940_desrolActual = $(this).find("td[data-value='desrolActual']").text();
        oProfesional.t940_desrolPropuesto = $(this).find("td[data-value='desrolPropuesto']").text();
        oProfesional.CorreoPromotor = $(this).attr("data-correo_promotor");
        oProfesional.t001_idficepi_aprobador = $(this).attr("data-idficepi_aprobador");
        oProfesional.CorreoAprobador = $(this).attr("data-correoaprobador");
        oProfesional.nombre_promotor = $(this).find("td:nth-child(2)").text();
        oProfesional.aprobador = $(this).find("td:nth-child(7)").text();
        oProfesional.nomCortoPromotor = $(this).attr("data-nomCortoPromotor");
        oProfesional.nomCortoAprobador = $(this).attr("data-nomCortoAprobador");
        oProfesional.t001_idficepi_promotor = $(this).attr("data-t001_idficepi_promotor");

        array.push(oProfesional);
    });

    $.ajax({
        url: "Default.aspx/AceptarPropuesta",
        "data": JSON.stringify({ oProfesional: array }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-aceptarPropuesta").modal("hide");
            solicitudesCat();
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido aceptar la propuesta");
        }
    });
}

function AprobacionNoAceptada() {
    actualizarSession();
    var $filasSeleccionadas = $("#tblSolicitudes tr.active");
    var array = [];

    //Creamos una lista de objetos 
    $filasSeleccionadas.each(function (index) {
        oProfesional = new Object();
        oProfesional.t940_idtramitacambiorol = $(this).attr("id");
        oProfesional.correoPromotor = $(this).attr("data-correo_promotor");
        oProfesional.correoAprobador = $(this).attr("data-correoaprobador");
        oProfesional.nombre_promotor = $(this).find("td:nth-child(2)").text();
        oProfesional.aprobador = $(this).find("td:nth-child(7)").text();
        oProfesional.nomCortoPromotor = $(this).attr("data-nomCortoPromotor");
        oProfesional.nomCortoAprobador = $(this).attr("data-nomCortoAprobador");
        oProfesional.nombre_interesado = $(this).find("td[data-value='nomProfesional']").text();
        oProfesional.t001_idficepi_promotor = $(this).attr("data-t001_idficepi_promotor");
        oProfesional.t940_desrolActual = $(this).find("td[data-value='desrolActual']").text();
        oProfesional.t940_desrolPropuesto = $(this).find("td[data-value='desrolPropuesto']").text();
        oProfesional.t940_resolucion = "R";

        array.push(oProfesional);
    });

    $.ajax({
        url: "Default.aspx/AprobacionNoAceptada",
        "data": JSON.stringify({ oProfesional: array }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-rechazarPropuesta").modal("hide");
            solicitudesCat();
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido dejar la solicitud en Stand by");
        }
    });
}

