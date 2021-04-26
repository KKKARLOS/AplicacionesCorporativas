
$(document).ready(function () {
    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $(".header-fixed > tbody").css("max-height", "290px");
        }
    }

    //manejador de fechas
    moment.locale('es');

    //CatalogoCambioResponsable();
})

//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#divNombre input[type=text]').on('keyup', function (event) {

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#tblGestCambioResponsable").length > 0) {
        $("#tblGestCambioResponsable").html("");
    }

    if (event.keyCode == 13) {
        CatalogoCambioResponsable();

        $('#cboEstado').val("");
       
    }
});


$("#btnGestionar").on("click", function () {

    if ($("#tblGestCambioResponsable tr.active").length > 0) {

        $active = $("#tblGestCambioResponsable tr.active");

        //COMPROBAR SI ES APTO O NO
        actualizarSession();
        $.ajax({
            url: "Default.aspx/validaEvalProgress",   // Current Page, Method                
            data: JSON.stringify({ idficepi_interesado: $active.attr("data-idinteresado"), idrespdestino: $active.attr("data-idrespdestino") }),  // parameter map as JSON
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
               

                //TODO REVISAR ESTA PARTE... 
                if ($active.attr("data-idrespdestino") == 0) {
                    $("#txtInteresado").val($active.attr("interesado"));

                    if ($active.attr("resporigen") == "null") $("#txtOrigen").val("");
                    else $("#txtOrigen").val($active.attr("resporigen"));

                    $("#lblMotivoOrigen").removeClass("show").addClass("hide");
                    $("#txtMotivoOrigen").removeClass("show").addClass("hide");
                    $("#lblDestino").css("visibility", "hidden");
                    $("#txtDestino").removeClass("show").addClass("hide");
                    $("#lblFechaTramitacion").css("display", "none");
                    $("#txtFechatramitacion").css("display", "none");
                    $("#lblFechaSolMediacion").css("display", "none");
                    $("#txtFechaSolMediacion").css("display", "none");
                    $("#btnAnular").css("display", "none");
                    $("#btnSelEvaluador").text("Seleccionar evaluador");

                    $("#lblMotivoDestino").removeClass("show").addClass("hide");
                    $("#txtMotivoDestino").removeClass("show").addClass("hide");
                    $("#lblFechaRechazo").removeClass("show").addClass("hide");

                    $("#txtFecharechazo").css("display", "none");
                    
                    if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_interesado") == "V")
                        $("#lblInteresado").text("Interesado");
                    else
                        $("#lblInteresado").text("Interesada");

                    if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_resporigen") == "V")
                        $("#lblEvalActual").text("Evaluador actual");
                    else
                        $("#lblEvalActual").text("Evaluadora actual");

                }

                else {
                    //Sin estado
                    if ($active.attr("estado") == "null") {
                        $("#txtInteresado").val($active.attr("interesado"));
                        $("#txtOrigen").val($active.attr("resporigen"));
                        $("#txtDestino").removeClass("show").addClass("hide");
                        if (result.d == true) {
                            $("#lblDestino").css("visibility", "visible");
                            if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_respdestino") == "V")
                                $("#lblDestino").text("Evaluador propuesto");                            
                            else
                                $("#lblDestino").text("Evaluadora propuesta");
                            
                        } 

                        $("#lblMotivoOrigen").removeClass("show").addClass("hide");
                        $("#txtMotivoOrigen").removeClass("show").addClass("hide");
                        $("#lblMotivoDestino").removeClass("show").addClass("hide");
                        $("#txtMotivoDestino").removeClass("show").addClass("hide");
                        $("#lblFechaTramitacion").css("display", "none");
                        $("#txtFechatramitacion").css("display", "none");
                        $("#lblFechaRechazo").css("display", "none");
                        $("#txtFechaRechazo").css("display", "none");
                        //$("#lblFechaRechazo").css("display", "none");
                        $("#txtFecharechazo").css("display", "none");
                        $("#lblFechaSolMediacion").css("display", "none");
                        $("#txtFechaSolMediacion").css("display", "none");


                        $("#btnForzar").css("display", "none");
                        $("#btnAnular").css("display", "none");
                        $("#btnSelEvaluador").text("Seleccionar evaluador");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_interesado") == "V")
                            $("#lblInteresado").text("Interesado");
                        else
                            $("#lblInteresado").text("Interesada");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_resporigen") == "V")
                            $("#lblEvalActual").text("Evaluador actual");
                        else
                            $("#lblEvalActual").text("Evaluadora actual");

                    }


                    //Tramitadas
                    if ($active.attr("estado") == 1) {
                        $("#txtInteresado").val($active.attr("interesado"));
                        $("#txtOrigen").val($active.attr("resporigen"));
                        $("#txtDestino").val($active.attr("data-respdestino"));
                        if (result.d == true)
                        {
                            $("#lblDestino").css("visibility", "visible");
                            if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_respdestino") == "V")
                                $("#lblDestino").text("Evaluador propuesto");
                            else
                                $("#lblDestino").text("Evaluadora propuesta");

                        } 
                        //$("#lblDestino").css("visibility", "visible");
                        $("#txtDestino").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").text($active.attr("comenresporigen"));
                        $("#lblMotivoOrigen").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").removeClass("hide").addClass("show");
                        $("#lblMotivoDestino").removeClass("show").addClass("hide");
                        $("#txtMotivoDestino").removeClass("show").addClass("hide");

                        $("#txtFechatramitacion").text($active.find("td[data-fechaTramitacion]").text());
                        $("#lblFechaTramitacion").css("display", "inline-block");
                        $("#txtFechatramitacion").css("display", "inline-block");
                        $("#lblFechaRechazo").css("display", "none");
                        $("#txtFecharechazo").css("display", "none");
                        $("#lblFechaSolMediacion").css("display", "none");
                        $("#txtFechaSolMediacion").css("display", "none");


                        $("#btnForzar").css("display", "inline-block");
                        $("#btnAnular").css("display", "inline-block");
                        $("#btnSelEvaluador").text("Seleccionar otro evaluador");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_interesado") == "V")
                            $("#lblInteresado").text("Interesado");
                        else
                            $("#lblInteresado").text("Interesada");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_resporigen") == "V")
                            $("#lblEvalActual").text("Evaluador actual");
                        else
                            $("#lblEvalActual").text("Evaluadora actual");
                    }

                    //No aceptadas
                    if ($active.attr("estado") == 3) {
                        $("#txtInteresado").val($active.attr("interesado"));
                        $("#txtOrigen").val($active.attr("resporigen"));
                        $("#txtDestino").val($active.attr("data-respdestino"));
                        if (result.d == true) {
                            if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_respdestino") == "V")
                                $("#lblDestino").text("Evaluador propuesto");
                            else
                                $("#lblDestino").text("Evaluadora propuesta");
                        } 
                        //$("#lblDestino").css("visibility", "visible");
                        $("#txtDestino").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").text($active.attr("comenresporigen"));
                        $("#txtMotivoDestino").text($active.attr("comenresdestino"));

                        $("#lblMotivoOrigen").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").removeClass("hide").addClass("show");
                        $("#lblMotivoDestino").removeClass("hide").addClass("show");
                        $("#txtMotivoDestino").removeClass("hide").addClass("show");

                        $("#txtFechatramitacion").text($active.find("td[data-fechaTramitacion]").text());
                        $("#txtFecharechazo").text($active.find("td[data-fechaRechazo]").text());
                        $("#lblFechaTramitacion").css("display", "inline-block");
                        $("#txtFechatramitacion").css("display", "inline-block");
                        $("#lblFechaRechazo").removeClass("hide").css("display", "inline-block");
                        $("#lblFechaRechazo").css("display", "inline-block");
                        $("#txtFecharechazo").css("display", "inline-block");

                        $("#lblFechaSolMediacion").css("display", "none");
                        $("#txtFechaSolMediacion").css("display", "none");

                        $("#btnForzar").css("display", "inline-block");
                        $("#btnAnular").css("display", "inline-block");
                        $("#btnSelEvaluador").text("Seleccionar otro evaluador");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_interesado") == "V")
                            $("#lblInteresado").text("Interesado");
                        else
                            $("#lblInteresado").text("Interesada");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_resporigen") == "V")
                            $("#lblEvalActual").text("Evaluador actual");
                        else
                            $("#lblEvalActual").text("Evaluadora actual");
                    }


                    //Solicitadas mediación no rechazadas
                    if ($active.attr("estado") == 6 && $active.attr("data-fecharechazo") == "/Date(-62135596800000)/") {
                        $("#txtInteresado").val($active.attr("interesado"));
                        $("#txtOrigen").val($active.attr("resporigen"));
                        $("#txtDestino").val($active.attr("data-respdestino"));
                        if (result.d == 0) $("#lblDestino").css("visibility", "visible");
                        //$("#lblDestino").css("visibility", "visible");
                        $("#txtDestino").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").text($active.attr("comenresporigen"));
                        $("#lblMotivoOrigen").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").removeClass("hide").addClass("show");
                        $("#lblMotivoDestino").removeClass("show").addClass("hide");
                        $("#txtMotivoDestino").removeClass("show").addClass("hide");

                        $("#txtFechatramitacion").text($active.find("td[data-fechaTramitacion]").text());
                        $("#lblFechaTramitacion").css("display", "inline-block");
                        $("#txtFechatramitacion").css("display", "inline-block");


                        $("#lblFechaRechazo").css("display", "none");
                        $("#txtFecharechazo").css("display", "none");
                        $("#txtFechaSolMediacion").text($active.find("td[data-fechaCambioEstado]").text());
                        $("#lblFechaSolMediacion").css("display", "inline-block");
                        $("#txtFechaSolMediacion").css("display", "inline-block");

                        $("#btnForzar").css("display", "inline-block");
                        $("#btnAnular").css("display", "inline-block");
                        $("#btnSelEvaluador").text("Seleccionar otro evaluador");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_interesado") == "V")
                            $("#lblInteresado").text("Interesado");
                        else
                            $("#lblInteresado").text("Interesada");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_resporigen") == "V")
                            $("#lblEvalActual").text("Evaluador actual");
                        else
                            $("#lblEvalActual").text("Evaluadora actual");
                    }

                    //Solicitadas mediación rechazadas
                    if ($active.attr("estado") == 6 && $active.attr("data-fecharechazo") != "/Date(-62135596800000)/") {
                        $("#txtInteresado").val($active.attr("interesado"));
                        $("#txtOrigen").val($active.attr("resporigen"));
                        $("#txtDestino").val($active.attr("data-respdestino"));
                        if (result.d == 0) $("#lblDestino").css("visibility", "visible");
                        //$("#lblDestino").css("visibility", "visible");
                        $("#txtDestino").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").text($active.attr("comenresporigen"));
                        $("#txtMotivoDestino").text($active.attr("comenresdestino"));

                        $("#lblMotivoOrigen").removeClass("hide").addClass("show");
                        $("#txtMotivoOrigen").removeClass("hide").addClass("show");
                        $("#lblMotivoDestino").removeClass("hide").addClass("show");
                        $("#txtMotivoDestino").removeClass("hide").addClass("show");

                        $("#txtFechatramitacion").text($active.find("td[data-fechaTramitacion]").text());
                        $("#txtFecharechazo").text($active.find("td[data-fechaRechazo]").text());
                        $("#lblFechaTramitacion").css("display", "inline-block");
                        $("#txtFechatramitacion").css("display", "inline-block");
                        $("#lblFechaRechazo").css("display", "inline-block");
                        $("#txtFecharechazo").css("display", "inline-block");

                        $("#txtFechaSolMediacion").text($active.find("td[data-fechaCambioEstado]").text());
                        $("#lblFechaSolMediacion").css("display", "inline-block");
                        $("#txtFechaSolMediacion").css("display", "inline-block");

                        $("#btnForzar").css("display", "inline-block");
                        $("#btnAnular").css("display", "inline-block");
                        $("#btnSelEvaluador").text("Seleccionar otro evaluador");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_interesado") == "V")
                            $("#lblInteresado").text("Interesado");
                        else
                            $("#lblInteresado").text("Interesada");

                        if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_resporigen") == "V")
                            $("#lblEvalActual").text("Evaluador actual");
                        else
                            $("#lblEvalActual").text("Evaluadora actual");
                    }
                }

                //BOTONES DE LA MODAL. Si es válido mostramos el botón de asignación forzosa
                if (result.d == true && $active.attr("estado") != "null") {
                    $("#btnForzar").css("display", "inline-block");
                    $("#lblDestino").css("visibility", "visible");
                    if ($("#tblGestCambioResponsable tr.active").attr("data-sexo_respdestino") == "V")
                        $("#lblDestino").text("Evaluador propuesto");
                    else
                        $("#lblDestino").text("Evaluadora propuesta");
                    //$("#lblDestino").text("Evaluador propuesto");
                    $("#lblDestino").css("color", "black");
                }
                else {
                    $("#btnForzar").css("display", "none");
                    //$("#lblDestino").css("visibility", "visible");
                    $("#lblDestino").text("Evaluador propuesto (No Apto)");
                    $("#lblDestino").css("color", "rgb(155, 90, 90)");
                }
               
            },
            error: function (ex, status) {
                alertNew("danger", "No se ha podido validar al profesional");
            }
        });


        $('#modal-gestionar').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#modal-gestionar').modal('show');

        //Si el responsable destino está de baja deshabilitamos el botón forzar asignación.
        if ($active.attr("data-estadebaja_respdestino") == 1) {
            $("#btnForzar").prop("disabled", true);
        }

    }
    else {
        alertNew("warning", "Tienes que seleccinar algún profesional");
    }
    
})


//Selección simple
$('body').on('click', '#tblGestCambioResponsable tr', function (e) {
    $('#tblGestCambioResponsable tr').removeClass('active');
    $(this).addClass('active');
});

function CatalogoCambioResponsable() {

    $estado = $("#cboEstado").val();
    if ($estado == "") $estado = null;

    if ($("#txtApellido1").val() != "" || $("#txtApellido2").val() != "" || $("#txtNombre").val() != "") {
        $estado = null;
    }
    else { $estado = $("#cboEstado").val(); }
    
    actualizarSession();
    $.ajax({
        url: "Default.aspx/CatalogoCambioResponsable",
        "data": JSON.stringify({ estado: $estado, apellido1: $("#txtApellido1").val(), apellido2: $("#txtApellido2").val(), nombre: $("#txtNombre").val() }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            
            pintarDatosPantalla(result.d);
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido obtener el catálogo de cambio de responsable");
        }
    });
}


function pintarDatosPantalla(datos) {
    try {
        
        var tblGestCambioResponsable = "";
      
        //Mostramos botón si hay resultados
        if (datos.length > 0) {
            $("#btnGestionar").removeClass("hide").addClass("show");
            $("#txtApellido1").val("");
            $("#txtApellido2").val("");
            $("#txtNombre").val("");
        }
        else {
            $("#btnGestionar").removeClass("show").addClass("hide");
        }

        for (var i = 0; i < datos.length; i++) {

            //PINTAR HTML
            tblGestCambioResponsable += "<tr data-idresp-origen='" + datos[i].T001_idficepi_resporigen + "' data-estadebaja_respdestino='" + datos[i].estadebaja_respdestino + "' data-nombreapellidos_respdestino='" + datos[i].nombreapellidos_respdestino + "' data-sexo_respdestino='" + datos[i].sexo_respdestino + "' data-sexo_interesado='" + datos[i].sexo_interesado + "' data-sexo_resporigen='" + datos[i].sexo_resporigen + "' data-correo_respdestino='" + datos[i].correo_respdestino + "' data-nombre_respdestino='" + datos[i].nombre_respdestino + "' data-nombre_resporigen='" + datos[i].nombre_resporigen + "' data-correo_resporigen='" + datos[i].correo_resporigen + "' data-nombreapellidos_interesado='" + datos[i].nombreapellidos_interesado + "' data-correointeresado='" + datos[i].correo_interesado + "' data-nombreprofesional='" + datos[i].nombreprofesional + "' data-fechaRechazo='" + datos[i].T937_fecharechazo + "' data-idinteresado='" + datos[i].T001_idficepi_interesado + "' data-idrespdestino='" + datos[i].T001_idficepi_respdestino + "' estado='" + datos[i].T937_estadopeticion + "' interesado='" + datos[i].Interesado + "' comenresporigen='" + datos[i].T937_comentario_resporigen + "' resporigen='" + datos[i].Resporigen + "' comenresdestino='" + datos[i].T937_comentario_respdestino + "' data-respdestino='" + datos[i].Respdestino + "' idpeticion='" + datos[i].T937_idpetcambioresp + "'>";
            tblGestCambioResponsable += "<td>" + datos[i].Interesado + "</td>";
            tblGestCambioResponsable += "<td data-estado='" + datos[i].T937_estadopeticion + "'>" + getEstado(datos[i].T937_estadopeticion) + " </td>";

            //Tramitadas
            if (datos[i].T937_estadopeticion == 1) {
                tblGestCambioResponsable += "<td data-fechaTramitacion='" + datos[i].T937_fechacambioestado + "'>" + moment(datos[i].T937_fechacambioestado).format('DD/MM/YYYY') + "</td>";
                tblGestCambioResponsable += "<td></td>";
                tblGestCambioResponsable += "<td></td>";
            }

            //Rechazadas
            else if (datos[i].T937_estadopeticion == 3) {
                
                tblGestCambioResponsable += "<td data-fechaTramitacion='" + datos[i].T937_fechainipeticion + "'>" + moment(datos[i].T937_fechainipeticion).format('DD/MM/YYYY') + "</td>";
                //Valor null
                if (datos[i].T937_fecharechazo != "/Date(-62135596800000)/") {
                    tblGestCambioResponsable += "<td data-fechaRechazo='" + datos[i].T937_fecharechazo + "'>" + moment(datos[i].T937_fecharechazo).format('DD/MM/YYYY') + "</td>";
                }
                else {
                    tblGestCambioResponsable += "<td></td>";
                }
                
                tblGestCambioResponsable += "<td></td>";
            }

            //Solicitada mediación no rechazadas
            //else if (datos[i].T937_estadopeticion == 6 && datos[i].T937_fecharechazo == "/Date(-62135596800000)/") {
            //    tblGestCambioResponsable += "<td data-fechaTramitacion='" + datos[i].T937_fechainipeticion + "'>" + moment(datos[i].T937_fechainipeticion).format('DD/MM/YYYY') + "</td>";
            //    tblGestCambioResponsable += "<td></td>";
            //    tblGestCambioResponsable += "<td data-fechaCambioEstado='" + datos[i].T937_fechacambioestado + "'>" + moment(datos[i].T937_fechacambioestado).format('DD/MM/YYYY') + "</td>";
            //}
            
            ////Solicitada mediación rechazada
            //else if (datos[i].T937_estadopeticion == 6 && datos[i].T937_fecharechazo != "/Date(-62135596800000)/") {
            //    tblGestCambioResponsable += "<td data-fechaTramitacion='" + datos[i].T937_fechainipeticion + "'>" + moment(datos[i].T937_fechainipeticion).format('DD/MM/YYYY') + "</td>";
            //    tblGestCambioResponsable += "<td data-fechaRechazo='" + datos[i].T937_fecharechazo + "'>" + moment(datos[i].T937_fecharechazo).format('DD/MM/YYYY') + "</td>";
            //    tblGestCambioResponsable += "<td data-fechaCambioEstado='" + datos[i].T937_fechacambioestado + "'>" + moment(datos[i].T937_fechacambioestado).format('DD/MM/YYYY') + "</td>";
                
            //}

            //Sin estado
            else if (datos[i].T937_estadopeticion == null) {
                tblGestCambioResponsable += "<td></td>";
                tblGestCambioResponsable += "<td></td>";
                tblGestCambioResponsable += "<td></td>";
            }

            tblGestCambioResponsable += "</tr>";

        };

        //Inyectar html en la página
        $("#tblGestCambioResponsable").html(tblGestCambioResponsable);

        /*Controlamos si el contenedor tiene Scroll*/
        var div = document.getElementById('tblGestCambioResponsable');

        var hasVerticalScrollbar = div.scrollHeight > div.clientHeight;
        if (hasVerticalScrollbar) {
            $("thead").css("width", "calc( 100% - 1em )")
        }
        else { $("thead").css("width", "100%") }
        /*FIN Controlamos si el contenedor tiene Scroll*/


        $("#tablaGestCambioResponsable").trigger("destroy");

        //Ordenación de columnas
        //$("#tblEvalMiEquipo").tablesorter();


        //ORDENAMOS LAS COLUMNAS (TABLESORTER FORK)
        $("#tablaGestCambioResponsable").tablesorter({
            //dateFormat: "dd/mm/yy", // set the default date format
            // pass the headers argument and assing a object            
            headers: {
                // set "sorter : false" (no quotes) to disable the column
                0: {
                    sorter: "text"
                },
                1: {
                    sorter: false
                },
                2: {
                    sorter: "shortDate", dateFormat: "ddmmyyyy"
                },
                3:
                {
                    sorter: "shortDate", dateFormat: "ddmmyyyy"
                },
                4:
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

//***Modal selección de evaluadores (al hacer click sobre el link de Evaluador)
$('#btnSelEvaluador').on('click', function () {
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
        var $active = $("#tblGestCambioResponsable tr.active");
        $.ajax({
            url: "Default.aspx/validaEvalProgress",
            "data": JSON.stringify({ idficepi_interesado: $active.attr("data-idinteresado"), idrespdestino: $evaluador.val() }),
            type: "POST",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                if (result.d == true) {
                    $('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
                    $('#modal-evaluadores').modal('hide');

                    $('#modal-ConfirmCambio').modal('show');
                    $("#txtProfCambio").html("Has seleccionado a " + $("#lisEvaluadores li.active").text() + ".</br></br> ¿Confirmas el cambio de evaluador?");
                }

                else {
                    alertNew("danger", "El evaluador destino no es válido para el profesional seleccionado.", null, 5000, null);
                    return;
                }
            },
            error: function (ex, status) {
                alertNew("danger", "No se ha podido gestionar la asignación");
            }
        });


    } else {
        alertNew("warning", "Debes seleccionar un evaluador");
    }

});


$("#btnConfirmarCambio").on("click", function () {
    //LLamamos al Webmethod que hará la update
    actualizarSession();
    $.ajax({
        url: "Default.aspx/otroEvaluador",
        "data": JSON.stringify({ idpeticion: $("#tblGestCambioResponsable tr.active").attr("idpeticion"), estado: $("#tblGestCambioResponsable tr.active").find("td[data-estado]").attr("data-estado"), idficepi_interesado: $("#tblGestCambioResponsable tr.active").attr("data-idinteresado"), idficepi_destino: $evaluador.attr('value'), nombreapellidosprofesional: $("#lisEvaluadores li.active").attr("data-nombreapellidosprofesional"), nombreinteresado: $("#tblGestCambioResponsable tr.active").attr("data-nombreprofesional"), sexo: $("#lisEvaluadores li.active").attr("data-sexo"), correointeresado: $("#tblGestCambioResponsable tr.active").attr("data-correointeresado"), nombre_resporigen: $("#tblGestCambioResponsable tr.active").attr("data-nombre_resporigen"), correo_resporigen: $("#tblGestCambioResponsable tr.active").attr("data-correo_resporigen"), nombreapellidos_interesado: $("#tblGestCambioResponsable tr.active").attr("data-nombreapellidos_interesado"), nombreprofesional: $("#lisEvaluadores li.active").attr("data-nombreprofesional"), correo_profesional: $("#lisEvaluadores li.active").attr("data-correo_profesional"), nombre_respdestino: $active.attr("data-nombre_respdestino"), correo_respdestino: $active.attr("data-correo_respdestino") }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            if (result.d == "no") {
                alertNew("danger", "El evaluador destino no es válido para el profesional seleccionado.", null, 5000, null);
                $('#modal-ConfirmCambio').modal('hide');
                return;
            }
            $('#modal-gestionar').modal('hide');
            $('#modal-ConfirmCambio').modal('hide');

            //$("#tblGestCambioResponsable tr.active").remove();

            if ($("#tblGestCambioResponsable tr.active").attr("estado") == "null")
                $("#tblGestCambioResponsable tr.active").attr("resporigen", $("#lisEvaluadores li.active").text());

            else {
                $("#tblGestCambioResponsable tr.active").remove();
            }

        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido gestionar el cambio de responsable");
        }
    });
})

$("#cboEstado").on('change', function () {
    $("#txtApellido1").val("");
    $("#txtApellido2").val("");
    $("#txtNombre").val("");
    if ($(this).val() == "")
    {
        $("#tblGestCambioResponsable").html("");
        return;
    }

    CatalogoCambioResponsable();
});

//FORZAR ASIGNACIÓN
$("#btnForzar").on("click", function () {

    $('#modal-Forzar').modal('show');
    $("#txtProfForzar").html("¿Confirmas la asignación?");
});

$("#btnConfirmarForzar").on("click", function () {
    var $active= $("#tblGestCambioResponsable tr.active");
    actualizarSession();
    $.ajax({
        url: "Default.aspx/GestionAsignacion",
        "data": JSON.stringify({ idpeticion: $active.attr("idpeticion"), estado: $active.find("td[data-estado]").attr("data-estado"), nombreapellidos_interesado: $active.attr("data-nombreapellidos_interesado"), nombre_resporigen: $active.attr("data-nombre_resporigen"), correo_resporigen: $active.attr("data-correo_resporigen"), nombre_respdestino: $active.attr("data-nombre_respdestino"), correo_respdestino: $active.attr("data-correo_respdestino"), sexo_respdestino: $active.attr("data-sexo_respdestino"), nombreprofesional: $active.attr("data-nombreprofesional"), nombreapellidos_respdestino: $active.attr("data-nombreapellidos_respdestino"), correointeresado: $active.attr("data-correointeresado"), idrespdestino: $active.attr("data-idrespdestino"), idinteresado:$active.attr("data-idinteresado") }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {            
            $('#modal-gestionar').modal('hide');
            $('#modal-Forzar').modal('hide');
            $("#tblGestCambioResponsable tr.active").remove();
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido gestionar la asignación");
        }
    });
})


//ANULAR 
$("#btnAnular").on("click", function () {
    $('#modal-Anular').modal('show');
    $("#txtProfAnular").html("¿Confirmas la anulación?");
});


$("#btnConfirmarAnular").on("click", function () {
    $("#tblGestCambioResponsable tr.active");

    actualizarSession();
    $.ajax({
        url: "Default.aspx/GestionAnulacion",
        "data": JSON.stringify({ idpeticion: $("#tblGestCambioResponsable tr.active").attr("idpeticion"), estado: $("#tblGestCambioResponsable tr.active").find("td[data-estado]").attr("data-estado"), nombre_resporigen: $("#tblGestCambioResponsable tr.active").attr("data-nombre_resporigen"), nombreapellidos_interesado: $("#tblGestCambioResponsable tr.active").attr("data-nombreapellidos_interesado"), correo_resporigen: $("#tblGestCambioResponsable tr.active").attr("data-correo_resporigen"), nombre_respdestino: $("#tblGestCambioResponsable tr.active").attr("data-nombre_respdestino"), correo_respdestino: $("#tblGestCambioResponsable tr.active").attr("data-correo_respdestino") }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {            
            $('#modal-gestionar').modal('hide');
            $('#modal-Anular').modal('hide');
            $("#tblGestCambioResponsable tr.active").remove();

        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido gestionar la anulación");
        }
    });
})


//Botón cancelar de evaluador
$('#modal-evaluadores #btnCancelar').on('click', function () {
    $('#modal-evaluadores').modal('hide');
});

//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluadores :input[type=text]').on('keyup', function (event) {
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
    $('#txtApellido1Modal').focus();
})
//***Fin Modal selección de evaluadores

//Obtiene los evaluadores (se llama al hacer intro sobre los filtros (ap1, ap2 o nombre) o al pulsar sobre Obtener)
function ObtenerEvaluadores() {
    $('#lisEvaluadores').children().remove();
    $lisEvaluadores = $('#lisEvaluadores');


    $tablaCatalogo = $("#tblGestCambioResponsable tr.active");

    actualizarSession();
    $.ajax({
        url: "Default.aspx/getSeleccionarEvaluador",   // Current Page, Method
        data: JSON.stringify({ t001_evaluado_actual: $tablaCatalogo.attr("data-idinteresado"), t001_evaluador_actual: $tablaCatalogo.attr("data-idresp-origen"), t001_apellido1: $('#txtApellido1Modal').val(), t001_apellido2: $('#txtApellido2Modal').val(), t001_nombre: $('#txtNombreModal').val() }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                $(result.d).each(function () { $("<li data-nombreprofesional='" + this.nombreprofesional + "' data-correo_profesional='" + this.correo_profesional + "'  data-sexo='" + this.Sexo + "' data-nombreapellidosprofesional='" + this.nombreapellidosprofesional + "' class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });
                $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluadores').modal('show');

            } else {
                alertNew("warning", "No existen profesionales válidos que respondan al filtro establecido.");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluadores");
        }
    });
}


function getEstado(estado) {
    switch (estado) {
        case 1:
            return "Tramitada";

        case 3:
            return "No aceptada";

        case 6:
            return "Solicitada mediación"

        default:
            return "";
    }
}
