$(document).ready(function () {
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "350px");
        }
    }

    //manejador de fechas
    moment.locale('es');

    solicitudesNoAprobadasCat();
})

function solicitudesNoAprobadasCat() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/solicitudesNoAprobadasCat",
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
            alertNew("danger", "No se ha podido obtener el catálogo de solicitudes no aprobadas");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  

        var tblSolicitudesRol = "";
        //$idficepi = datos.idficepi;

        for (var i = 0; i < datos.length; i++) {
            tblSolicitudesRol += "<tr data-motivoPropuesto='" + datos[i].t940_motivopropuesto + "' data-nomCortoInteresado='" + datos[i].nomCortoInteresado + "' data-idficepi_aprobador='" + datos[i].t001_idficepi_aprobador + "' data-nomCortoPromotor='" + datos[i].nomCortoPromotor + "' data-nomCortoAprobador='" + datos[i].nomCortoAprobador + "' data-t001_idficepi_promotor='" + datos[i].t001_idficepi_promotor + "' data-correointeresado='" + datos[i].correointeresado + "' data-nombre_interesado='" + datos[i].nombre_interesado + "' data-nombreapellidos_promotor='" + datos[i].nombre_promotor + "' data-correo_promotor='" + datos[i].CorreoPromotor + "' data-correoaprobador='" + datos[i].CorreoAprobador + "'   id=" + datos[i].t940_idtramitacambiorol + " idficepi= " + datos[i].t001_idficepi_interesado + " idrol=" + datos[i].t004_idrol_propuesto + ">";
            tblSolicitudesRol += "<td  data-value='nomProfesional'><span>" + datos[i].nombre_interesado + "</span></td>";
            tblSolicitudesRol += "<td  data-value='nomPromotor'><span>" + datos[i].nombre_promotor + "</span></td>";
            tblSolicitudesRol += "<td data-value='desrolActual'><span>" + datos[i].t940_desrolActual + "</span></td>";
            tblSolicitudesRol += "<td data-value='desrolPropuesto'><span>" + datos[i].t940_desrolPropuesto + "</span></td>";
            tblSolicitudesRol += "<td style='word-break: break-all;' data-value='motivoPropuesto'><span>" + datos[i].t940_motivopropuesto + "</span></td>";
            tblSolicitudesRol += "<td data-value='fechaProposicion'><span>" + moment(datos[i].t940_fechaproposicion).format('DD/MM/YYYY') + "</span></td>";
            tblSolicitudesRol += "<td  data-value='nomAprobador'><span>" + datos[i].aprobador + "</span></td>";
            tblSolicitudesRol += "<td style='word-break: break-all;' data-value='motivoRechazo'><span>" + datos[i].t940_motivorechazo + "</span></td>";
            tblSolicitudesRol += "<td data-value='fechaResolucion'><span>" + moment(datos[i].t940_fecharesolucion).format('DD/MM/YYYY') + "</span></td>";

            tblSolicitudesRol += "</tr>";
        };

        //Inyectar html en la página        
        $("#tblSolicitudesNoAprobadas").html(tblSolicitudesRol);

        $("#numSolicitudes").html(datos.length)

        $("#tablaSolicitudesNoAprobadas").trigger("destroy");

        //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
        $("#tablaSolicitudesNoAprobadas").tablesorter({
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
                    sorter: false
                },

                8: {
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
$('body').on('click', '#tablaSolicitudesNoAprobadas tr', function (e) {
    lista = $(this).parent().children();
    if (e.shiftKey && lista.filter('.active').length > 0) {
        first = lista.filter('.active:first').index();//Primer seleccionado
        last = lista.filter('.active:last').index();//Último seleccionado
        $('#tablaSolicitudesNoAprobadas tr').removeClass('active');//Borrar de las dos listas
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

$("#btnStandby").on("click", function () {
    if (!comprobarFilaSeleccionada()) {
        alertNew("danger", "No has seleccionado ninguna fila");
        return;
    }

    $("#modal-standby").modal("show");


    var $profesionales = $("#tblSolicitudesNoAprobadas tr.active").find("td[data-value='nomProfesional']");
    var html = "";

    $profesionales.each(function (index, element) {
        html += "<span>" + element.firstChild.innerText + "</span></br>"
    })

    $("#divProfesionalesStandby").html(html);

})

$("#btnAceptarNoAprobacion").on("click", function () {

    if (!comprobarFilaSeleccionada()) {
        alertNew("danger", "No has seleccionado ninguna fila");
        return;
    }

    $("#modal-aceptarNoAprobacion").modal("show");

    var $profesionales = $("#tblSolicitudesNoAprobadas tr.active").find("td[data-value='nomProfesional']");
    var html = "";

    $profesionales.each(function (index, element) {
        html += "<span>" + element.firstChild.innerText + "</span></br>"
    })

    $("#divProfesionalesAceptar").html(html);
})

function comprobarFilaSeleccionada() {
    var $filasSeleccionadas = $("#tblSolicitudesNoAprobadas tr.active");
    if ($filasSeleccionadas.length == 0)
        return false;

    return true;
}

$("#btnAceptarFinal").on("click", function () {
    aceptarNoAprobacion();
})

$("#btnStandbyFinal").on("click", function () {
    standby();
})


function standby() {
    actualizarSession();
    var $filasSeleccionadas = $("#tblSolicitudesNoAprobadas tr.active");
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
        oProfesional.t940_resolucion = "Z";
        oProfesional.t940_desrolActual = $(this).find("td[data-value='desrolActual']").text();
        oProfesional.t940_desrolPropuesto = $(this).find("td[data-value='desrolPropuesto']").text();
        oProfesional.nombre_interesado = $(this).find("td[data-value='nomProfesional']").text();
        oProfesional.t940_motivorechazo = $(this).find("td[data-value='motivoRechazo']").text();
        oProfesional.t940_motivopropuesto = $(this).attr("data-motivoPropuesto");
        

        array.push(oProfesional);
    });

    $.ajax({
        url: "Default.aspx/standby",
        "data": JSON.stringify({ oProfesional: array }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-standby").modal("hide");
            solicitudesNoAprobadasCat();
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido dejar la solicitud en Stand by");
        }
    });
}

function aceptarNoAprobacion() {
    actualizarSession();
    var $filasSeleccionadas = $("#tblSolicitudesNoAprobadas tr.active");
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
        oProfesional.t940_motivorechazo = $(this).find("td[data-value='motivoRechazo']").text();
        oProfesional.t940_motivopropuesto = $(this).attr("data-motivoPropuesto");
        oProfesional.t940_resolucion = "R";

        array.push(oProfesional);
    });

    $.ajax({
        url: "Default.aspx/aceptarNoAprobacion",
        "data": JSON.stringify({ oProfesional: array }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("#modal-aceptarNoAprobacion").modal("hide");
            solicitudesNoAprobadasCat();
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido aceptar la no aprobación");
        }
    });
}