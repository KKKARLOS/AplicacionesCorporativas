    
$(document).ready(function () {    
    //Establecemos el código de pantalla para las ayudas
    $(document).find("body").attr('data-codigopantalla', $(document).find("body").attr('data-codigopantalla') + origen);

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $("#tablaEvaluado.header-fixed > tbody").css("max-height", "330px");
        }
    }

    //Manejador de fechas
    moment.locale('es'); 
    
    //Boton exportación a excel
    $('#btnExportExcel').on('click', exportarExcel)

   //Para administradores (filtro)
    if (origen =="ADM") {
        $("#divEvaluador").removeClass("hide").addClass("show");
        $('input#evaluador').val("");

        if ($("#evaluado").val() == "") $("#headerEvaluado").removeClass("hide").addClass("showInline");
        else $("#headerEvaluado").removeClass("showInline").addClass("hide");
        
    }
        
    $('input#evaluador').attr('idficepi', idficepi.toString());

    if (origen =="EVA")
    {
        $('input#evaluador').val(nombre);
        catalagoArbolDependencias();
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

         $("#headerEvaluado").removeClass("hide").addClass("showInline");
        

        $evaluador = $('#lisEvaluadores li.active');
        if ($evaluador.length > 0) {
            
            $('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
            $("#tblEvaluador tr").attr("idficepievaluador", $evaluador.attr('value'));

            $('#tblEvaluador tr').attr("data-sexo", $evaluador.attr('data-sexo'));
            $("#tblEvaluador tr").find("td[data-value=nomEvaluador]").text($evaluador.text());
            $('#modal-evaluadores').modal('hide');

            catalagoArbolDependencias();

        } else {
            alertNew("warning", "Debes seleccionar un evaluador");
        }

    });

    //Botón cancelar de evaluador
    $('#modal-evaluadores #btnCancelar').on('click', function () {
        $('#modal-evaluadores').modal('hide');
    });

    //Botón cancelar de Modal Detalle
    $("#modal-potencial #btnCancelarModalPotencial").on("click", function () {
        $('#modal-potencial').modal('hide');
    })

    $("#modal-YO #btnCancelarModalYO").on("click", function () {
        $('#modal-YO').modal('hide');
    });

})


function catalagoArbolDependencias() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/catArbolDependencias",
        "data": JSON.stringify({ idficepi: $("#evaluador").attr("idficepi") }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,

        success: function (result) {
            
            //Pintar Árbol

            pintarDatosPantalla(result.d);
            
            //$nomEvaluado = $(this).find("td[data-value=nomEvaluado]").text();

            //$("#tblEvaluador").find("td[data-value=nomEvaluador]").text()

        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido obtener el árbol de dependencias");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  
        var tblEvaluador = "";
        var tblEvaluado = "";
        var imgFlechaArriba = "";
        var imgFlechaAbajo = "";
        var imgPotencialEvaluador = "";       
        var imgRealizaPruebasEvaluador = "";
        var imgEvaluadorconvocadoapruebasnolasrealizo = "";
        var imgYoibEvaluador = "";

        var imgPotencialEvaluado = "";       
        var imgRealizaPruebasEvaluado = "";
        var imgEvaluadoconvocadoapruebasnolasrealizo = "";
        var imgYoibEvaluado = "";
        var imgColectivoevaluador = "";
        var imgColectivoevaluado = "";

        //Si tengo evalprogress y no soy yo mismo... pinto el icono de flecha arriba
        if (datos[0].Evaluadordelevaluador != null && idficepi != datos[0].idficepievaluador ) {
            imgFlechaArriba = "<i class='glyphicon glyphicon-arrow-up'></i>";
        }
      
        
        //Si vengo por la administración y tengo evalprogress, pinto la flecha de arriba
        else if (origen == "ADM" && datos[0].Evaluadordelevaluador != null) {
            imgFlechaArriba = "<i class='glyphicon glyphicon-arrow-up'></i>";
        }
        else { imgFlechaArriba = "<i class='glyphicon glyphicon-arrow-up transparent'></i>"; }


        if (datos[0].colectivoevaluador == "1") {
            imgColectivoevaluador = "<i class='fa fa-check'></i>";
        }
        else imgColectivoevaluador = "<i class='fa fa-check transparent'></i>";

       
        //REALIZA PRUEBAS DE IDENTIFICACIÓN
        if (datos[0].Evaluadorrealizapruebas || datos[0].Evaluadorconvocadoapruebas != 0) {
            imgRealizaPruebasEvaluador = "<i class='fa fa-pencil-square-o'></i>";
            //$("#leyPruebasIdentificacion").removeClass("hide").addClass("show");
        }
        else {
            imgRealizaPruebasEvaluador = "<i class='fa fa-pencil-square-o transparent'></i>";
        }


        //POTENCIAL
        if (datos[0].Evaluadorpotencial) {
            imgPotencialEvaluador = "<i class='fa fa-diamond'></i>";
            imgRealizaPruebasEvaluador = "<i class='fa fa-pencil-square-o transparent'></i>";
            //$("#leyPotencial").removeClass("hide").addClass("show");
        }
        else {
            imgPotencialEvaluador = "<i class='fa fa-diamond transparent'></i>";
        }


        //CONVOCADO A PRUEBAS
        //if (datos[0].Evaluadorconvocadoapruebasnolasrealizo) {
        //    imgEvaluadorconvocadoapruebasnolasrealizo = "<i class='fa fa-pencil-square-o text-danger'></i>";
        //    //$("#leyConvocadoPruebas").removeClass("hide").addClass("show");
        //}

        //else {
        //    imgEvaluadorconvocadoapruebasnolasrealizo = "<i class='fa fa-pencil-square-o text-danger transparent'></i>";
        //}

        //YO@IBERMATICA
        if (datos[0].Evaluadoryoenibermatica) {
            imgYoibEvaluador = "<i class='fa fa-at'></i>";
            //$("#leyYoIb").removeClass("hide").addClass("show");            
        }
        else {
            imgYoibEvaluador = "<i class='fa fa-at transparent'></i>";
        }

       

        tblEvaluador += "<tr data-evaluadorconvocadoapruebas='" + datos[0].Evaluadorconvocadoapruebas + "' data-colectivoevaluador='" + datos[0].colectivoevaluador + "'  data-sexo= '" + datos[0].Sexo + "' idevalprogress='" + datos[0].Evaluadordelevaluador + "' idficepievaluador='" + datos[0].idficepievaluador + "'><td style='width:4%'>" + imgFlechaArriba + "</td>";
        tblEvaluador += "<td style='width:4%'>" + imgColectivoevaluador + "</td>"
        tblEvaluador += "<td style='width: 45.5%;' data-value='nomEvaluador'>" + datos[0].Evaluador + "</td>";
        tblEvaluador += "<td style='width:4%'>" + imgPotencialEvaluador + "</td>";
        tblEvaluador += "<td style='width:4%'>" + imgYoibEvaluador + "</td>";
        tblEvaluador += "<td style='width:4%'>" + imgRealizaPruebasEvaluador + "</td>";
        //tblEvaluador += "<td style='width:4%'>" + imgEvaluadorconvocadoapruebasnolasrealizo + "</td>";
        tblEvaluador += "<td style='width:34.5%'>" + datos[0].Roldelevaluador + "</td>";
        tblEvaluador +=  "</tr>";
            
        //Inyectamos el html a la tabla evaluador
        $("#tblEvaluador").html(tblEvaluador);

        if (datos.length > 1) {
            if ($("#tblEvaluador tr").attr("data-sexo") == "V") $("#headerEvaluador").text("Evaluador");
            else $("#headerEvaluador").text("Evaluadora");
            
            //$("#headerEvaluado").text("Evaluados/as");
            
        }
        else {
            $("#headerEvaluador").text("Profesional");
            $("#headerEvaluado").text("");
        }

        $("#headerRol").text("Rol");

        //Tabla Evaluados
        for (var i = 1; i < datos.length; i++) {
            tblEvaluado += "<tr  data-evaluadorconvocadoapruebas='" + datos[i].Evaluadoconvocadoapruebas + "' data-colectivoevaluado='" + datos[i].colectivoevaluado + "' idficepievaluado='" + datos[i].idficepievaluado + "'>";
            
            //Miramos si el evaluado tiene hijos
            if (datos[i].Elevaluadotienehijos) {
                imgFlechaAbajo = "<i id='flechaAbajo' class='glyphicon glyphicon-arrow-down'></i>";                                                
            }
            else { imgFlechaAbajo = "<i id='flechaAbajo' class='glyphicon glyphicon-arrow-down transparent'></i>"; }


            //REALIZA PRUEBAS DE IDENTIFICACIÓN
            if (datos[i].Evaluadorealizapruebas || datos[i].Evaluadoconvocadoapruebas != 0) {
                imgRealizaPruebasEvaluado = "<i class='fa fa-pencil-square-o'></i>";
                //if ($("#leyPruebasIdentificacion").hasClass("hide")) {
                //    //$("#leyPruebasIdentificacion").removeClass("hide").addClass("show");
                //}
            }
            else { imgRealizaPruebasEvaluado = "<i class='fa fa-pencil-square-o transparent'></i>"; }
            
            //POTENCIAL
            if (datos[i].Evaluadopotencial) {
                imgPotencialEvaluado = "<i class='fa fa-diamond'></i>";
                imgRealizaPruebasEvaluado = "<i class='fa fa-pencil-square-o transparent'></i>";
                //if ($("#leyPotencial").hasClass("hide")) {
                //    //$("#leyPotencial").removeClass("hide").addClass("show");
                //}

            }
            else { imgPotencialEvaluado = "<i class='fa fa-diamond transparent'></i>"; }

            //CONVOCADO A PRUEBAS
            //if (datos[i].Evaluadoconvocadoapruebasnolasrealizo) {
            //    imgEvaluadoconvocadoapruebasnolasrealizo = "<i class='fa fa-pencil-square-o text-danger'></i>";
            //    //if ($("#leyConvocadoPruebas").hasClass("hide")) {
            //    //    //$("#leyConvocadoPruebas").removeClass("hide").addClass("show");
            //    //}
            //}

            //else {
            //    imgEvaluadoconvocadoapruebasnolasrealizo = "<i class='fa fa-pencil-square-o text-danger transparent'></i>";
            //}

            //YO@IBERMATICA
            if (datos[i].Evaluadoyoenibermatica) {
                imgYoibEvaluado = "<i class='fa fa-at'></i>";
                //if ($("#leyYoIb").hasClass("hide")) {
                //    //$("#leyYoIb").removeClass("hide").addClass("show");
                //}
            }
            else {
                imgYoibEvaluado = "<i class='fa fa-at transparent'></i>";
            }

            if (datos[i].colectivoevaluado == "1") {
                imgColectivoevaluado = "<i class='fa fa-check'></i>";
            }
            else {
                imgColectivoevaluado = "<i class='fa fa-check transparent'></i>";
            }
            

            tblEvaluado += "<td style='width: 15%; text-align:right'>"+imgFlechaAbajo+" ";                
            tblEvaluado += "</td>";

            tblEvaluado += "<td style='width:4%'>" + imgColectivoevaluado + "</td>";
            tblEvaluado += "<td style='width:34.5%' data-value='nomEvaluado'>" + datos[i].Evaluado + "</td>";

            //ICONOS
            tblEvaluado += "<td style='width:4%;'>" + imgPotencialEvaluado + "</td>";
            tblEvaluado += "<td style='width:4%;'>" + imgYoibEvaluado + "</i></td>";
            tblEvaluado += "<td style='width:4%;'>" + imgRealizaPruebasEvaluado + "</td>";            
            tblEvaluado += "<td style='width:34.5%;'>" + datos[i].Roldelevaluado + "</td></tr>"
            
        };
        
        //Inyectar html en la tabla evaluados
        $("#tblEvaluado").html(tblEvaluado);
        //$('#tblEvaluado').hide().html(tblEvaluado).fadeIn('fast');


        $("#spanNumero").text($("#tblEvaluado tr").length);

  
        //Click en FlechaAbajo
        $("#tblEvaluador .glyphicon.glyphicon-arrow-down, #tblEvaluado .glyphicon.glyphicon-arrow-down").on("click", function () {
            
            $nomEvaluador = $("#tblEvaluador").find("td[data-value=nomEvaluador]").text();
            $nomEvaluado = $(this).parent().parent().find("td[data-value=nomEvaluado]").text();

            $("#tblEvaluador").find("td[data-value=nomEvaluador]").text($nomEvaluado);

            //Le pasamos al procedimiento el idficepi de la fila        
            actualizarSession();
            $.ajax({
                url: "Default.aspx/catArbolDependencias",
                "data": JSON.stringify({ idficepi: $(this).parent().parent().attr("idficepievaluado") }),
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {
                    
                    pintarDatosPantalla(result.d)
                    //$nomEvaluado = $(this).find("td[data-value=nomEvaluado]").text();
                    //$("#tblEvaluador").find("td[data-value=nomEvaluador]").text()
                },
                error: function (ex, status) {
                    alertNew("danger", "No se ha podido obtener los datos de los evaluados");
                }
            });
        })

        

        //Click en FlechaArriba
        $("#tblEvaluador .glyphicon.glyphicon-arrow-up, #tblEvaluado .glyphicon.glyphicon-arrow-up").on("click", function () {
            
            if ($(this).hasClass("transparent")) return;

            //Le pasamos al procedimiento el idficepi de la fila        
            actualizarSession();
            $.ajax({
                url: "Default.aspx/catArbolDependencias",
                "data": JSON.stringify({ idficepi: $(this).parent().parent().attr("idevalprogress") }),
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {

                    pintarDatosPantalla(result.d)                    
                },
                error: function (ex, status) {
                    alertNew("danger", "No se ha podido obtener los datos de los evaluados");
                }
            });
        })

        //Click en potencial o pruebas
        $('#tblEvaluado .fa.fa-diamond:not(.transparent) , #tblEvaluado .fa.fa-pencil-square-o:not(.transparent), #tblEvaluador .fa.fa-diamond:not(.transparent) , #tblEvaluador .fa.fa-pencil-square-o:not(.transparent) ').on('click', function () {
            $('#modal-potencial').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#modal-potencial').modal('show');

            /*
            if ($(this).parent().parent().attr("idficepievaluado") != undefined) alert($(this).parent().parent().attr("idficepievaluado"));
            else alert($(this).parent().parent().attr("idficepievaluador"));*/
            var id = "";

            if ($(this).parent().parent().attr("idficepievaluado") != undefined) id= ($(this).parent().parent().attr("idficepievaluado"));
            else id= ($(this).parent().parent().attr("idficepievaluador"));

            actualizarSession();
            $.ajax({
                url: "Default.aspx/SelectPotencial",
                "data": JSON.stringify({ idficepi: id }),
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {

                    pintarDetalle(result.d)
                },
                error: function (ex, status) {
                    alertNew("danger", "No se ha podido obtener el detalle del potencial");
                }
            });

        });

        //Click en yo@enibermatica
        $('#tblEvaluado .fa.fa-at:not(.transparent)').on('click', function () {
            $('#modal-YO').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
            $('#modal-YO').modal('show');

            /*
            if ($(this).parent().parent().attr("idficepievaluado") != undefined) alert($(this).parent().parent().attr("idficepievaluado"));
            else alert($(this).parent().parent().attr("idficepievaluador"));*/
            var id = "";

            if ($(this).parent().parent().attr("idficepievaluado") != undefined) id = ($(this).parent().parent().attr("idficepievaluado"));
            else id = ($(this).parent().parent().attr("idficepievaluador"));

            actualizarSession();
            $.ajax({
                url: "Default.aspx/SelectYO",
                "data": JSON.stringify({ idficepi: id }),
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {

                    pintarDetalleYO(result.d)
                },
                error: function (ex, status) {
                    alertNew("danger", "No se ha podido obtener el detalle de yo@enibermatica");
                }
            });

        });


    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}



//Foco en pantalla 
$('#modal-evaluadores').on('shown.bs.modal', function () {
    $('#inputApellido1').focus();
})


//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluadores :input[type=text]').on('keyup', function (event) {

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#lisEvaluadores").length > 0) {
        $("#lisEvaluadores").html("");
    }

    if (event.keyCode == 13) {
        $lisEvaluadores = $('#lisEvaluadores');

        if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "" && $("#tblgetFicepi li").length == 0) {
            alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
            return;
        }


        if (origen ="ADM") {
            //Si es administrador puede buscar entre todos los evaluadores
            actualizarSession();
            $.ajax({
                url: "Default.aspx/getEvaluadores",   // Current Page, Method
                data: JSON.stringify({ t001_apellido1: $('#inputApellido1').val(), t001_apellido2: $('#inputApellido2').val(), t001_nombre: $('#txtNombre').val() }),  // parameter map as JSON
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
        //else {
        //    //Si es usuario buscará sólo en su equipo
        //    $.ajax({
        //        url: "Default.aspx/getEvaluadoresArbol",   // Current Page, Method
        //        data: JSON.stringify({ idficepi: idficepi, t001_apellido1: $('#inputApellido1').val(), t001_apellido2: $('#inputApellido2').val(), t001_nombre: $('#txtNombre').val() }),  // parameter map as JSON
        //        type: "POST",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        timeout: 20000,
        //        success: function (result) {
        //            $result = $(result.d);
        //            if ($result.length > 0) {
        //                $(result.d).each(function () { $("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });
        //                $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        //                $('#modal-evaluadores').modal('show');

        //            } else {
        //                alertNew("warning", "No existen evaluadores");
        //            }
        //        },
        //        error: function (ex, status) {
        //            alertNew("danger", "Error al intentar obtener los evaluadores");
        //        }
        //    });
        //}

    }
});




function pintarDetalle(datos) {
    try {
        //PINTAR HTML DETALLE        
        $("#txtPruebasRealizadas").text("");
        $("#fecha").val("");
        $("#origen").text("");
        $("#tblDocPotencial").html("");


        if (datos[0].esPotencial == true) $("#esPotencial").text("Sí");
        else $("#esPotencial").text("No");

        if (datos[0].Realizapruebas == true)
            $("#txtPruebasRealizadas").text("Sí");
        else {
            if (datos[0].Convocadoapruebas == 0) $("#txtPruebasRealizadas").text("No");
            else if (datos[0].Convocadoapruebas == 1) {
                $("#txtPruebasRealizadas").text("Convocado/a a pruebas, no las realizó");
            }
            else {
                $("#txtPruebasRealizadas").text("Convocado/a a pruebas, sí las realizó");
            }
        } 

        if (datos[0].Realizapruebas == true || datos[0].Convocadoapruebas != 0) {
            $("#fecha").val(moment(datos[0].Potfecha).format('DD/MM/YYYY'));
            $("#origen").val(datos[0].Potorigen);
        }
        
        //Tabla documentos
        var tblDocPotencial = "";

        for (var i = 1; i < datos.length; i++) {
            tblDocPotencial += "<tr t2='" + datos[i].T2_iddocumento + "'><td class='fk-documentoPotencial'>" + datos[i].T153_nombre + "</td></tr>";
        }
        
        //Inyectar html en la tabla documentos potencial
        $("#tblDocPotencial").html(tblDocPotencial);
        //$('#tblEvaluado').hide().html(tblEvaluado).fadeIn('fast');

       
        //Descarga documentos
        $(".fk-documentoPotencial").on('click', function () {           
            descargarDoc_onclick($(this).parents('tr').attr('t2'));
        });


    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}

function pintarDetalleYO(datos) {
    try {
        //PINTAR HTML  DETALLE YO                
        //Tabla documentos
        var tblDocYO = "";

        for (var i = 0; i < datos.length; i++) {
            tblDocYO += "<tr t2='" + datos[i].T2_iddocumento + "'>><td class='fk-documentoYO'>" + datos[i].T153_nombre + "</td></tr>";
        }

        //Inyectar html en la tabla documentos potencial
        $("#tblDocYO").html(tblDocYO);
        
        //Descarga documentos
        $(".fk-documentoYO").on('click', function () {
            descargarDoc_onclick($(this).parents('tr').attr('t2'));
        });


    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}




//Descarga documento
function descargarDoc_onclick(t2_iddocumento) {

    var url = "../../../../FileDownload.aspx?" + $.uri.encode("d=" + t2_iddocumento);

    //Submitear el iframe de descarga de documentos
    $("#ifrmDownloadDoc").prop("src", url).submit();

}


function exportarExcel() {

    //validaciones
    if ($("#tblEvaluado").length == 0) {
        alertNew("warning", "No hay datos para exportar");
        return;
    }

    //Cargar en el iframe la página de exportación.
    var qs = "pantalla=arboldependencias&idficepi=" + $("#evaluador").attr("idficepi") + "&idficepitext=" + $("#evaluador").val() + "&idficepievaluador=" + $("#tblEvaluador tr").attr("idficepievaluador") + "&evaluadortext=" + $("#tblEvaluador tr").find("td[data-value=nomEvaluador]").text() + "&rolevaluador=" + $("#tblEvaluador tr").find("td:last").text() + "&numevaluados=" + $("#tblEvaluado tr").length;

    $("#ifrmExportExcel").prop("src", strServer + "Capa_Presentacion/Utilidades/ExportarExcel.aspx?" + qs);

}


function exportarExcelALL() {

    //validaciones
    if ($("#tblEvaluado").length == 0) {
        alertNew("warning", "No hay datos para exportar");
        return;
    }

    //Cargar en el iframe la página de exportación.
    var qs = "pantalla=arboldependenciasALL&idficepi=" + $("#evaluador").attr("idficepi") + "&idficepitext=" + $("#evaluador").val() + "&idficepievaluador=" + $("#tblEvaluador tr").attr("idficepievaluador") + "&evaluadortext=" + $("#tblEvaluador tr").find("td[data-value=nomEvaluador]").text() + "&rolevaluador=" + $("#tblEvaluador tr").find("td:last").text() + "&numevaluados=" + $("#tblEvaluado tr").length;

    $("#ifrmExportExcel").prop("src", strServer + "Capa_Presentacion/Utilidades/ExportarExcel.aspx?" + qs);

}

$("#btnExportExcelALL").on("click", exportarExcelALL);



//Selección de fila activa
//$('body').on('click', '#tblEvaluado tr', function (e) {
//    $('#tblEvaluado tr').removeClass('active');
//    $(this).addClass('active');
//});
