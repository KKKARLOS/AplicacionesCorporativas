var lista;

$(document).ready(function () {
    
    //Establecemos el código de pantalla para las ayudas
    $(document).find("body").attr('data-codigopantalla', $(document).find("body").attr('data-codigopantalla') + origen);

    //Modificamos la altura del contenedor en los IOS menores de versión 6
    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
        ver = iOSversion();
        if (ver[0] < 6) {
            $("li.parent_li").css("height", "350px !important");
        }
    }

    $('input#evaluador').attr('idficepi', idficepi.toString());
    $('input#evaluador').attr('data-sexo', sexo.toString());

    //Boton exportación a excel
    $('#btnExportExcel').on('click', exportarExcel)

    if (origen =="ADM") {

        $('input#evaluador').val("");
        $("#lblEvaluador").addClass("btn btn-link underline");

        $("#lblEvaluador").addClass("btn btn-link underline");
        $('#evaluador').attr("idficepi","");
        //***Modal selección de evaluadores (al hacer click sobre el link de Evaluador)
        $('#lblEvaluador').on('click', mostrarModal);
        
    }

    else {
        $('input#evaluador').val(nombre);
        obtenerLista();
        cargarDesgloseRol();
    }

    if (sexo == "V") $("#lblEvaluador").text("Evaluador")
    else $("#lblEvaluador").text("Evaluadora");
       
    //Selección simple de evaluador
    $('body').on('click', '#lisEvaluadores li', function (e) {
        $('#lisEvaluadores li').removeClass('active');
        $(this).addClass('active');
    });

    //Selección simple de evaluador
    $('body').on('click', '#lisEvaluadoresMiequipo li', function (e) {
        $('#lisEvaluadoresMiequipo li').removeClass('active');
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

            cargarDesgloseRol();

        } else {
            alertNew("warning", "Debes marcar un evaluador para luego seleccionarlo");
        }

    });


    //Botón seleccionar de evaluador
    $('#modal-evaluadoresMiequipo #btnSeleccionarMiequipo').on('click', function () {
        $evaluador = $('#lisEvaluadoresMiequipo li.active');
        if ($evaluador.length > 0) {
            
            $('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
            $('#modal-evaluadoresMiequipo').modal('hide');
                       
            cargarDesgloseRol();           

        } else {
            alertNew("warning", "Debes seleccionar un evaluador");
        }

    });

    //Botón cancelar de evaluador
    $('#modal-evaluadores #btnCancelar').on('click', function () {
        $('#modal-evaluadores').modal('hide');
    });

    //Botón cancelar de evaluador
    $('#modal-evaluadoresMiequipo #btnCancelarMiequipo').on('click', function () {
        $('#modal-evaluadoresMiequipo').modal('hide');
    });

})




function cargarDesgloseRol() {
   
    if ($('#evaluador').attr("idficepi") == ""){
        return;
    }
    actualizarSession();
    $.ajax({
        url: "Default.aspx/catalogoDesgloseRol",
        "data": JSON.stringify({ idficepi: $("#evaluador").attr("idficepi"), parentesco: $("#cboProfundizacion").val() }),
        type: "POST",
        //async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {            
            pintarDatosPantalla(result.d);
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido obtener el desglose de roles");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  
        var liDesgloseRol = "";
        liDesgloseRol += "<li>";
        var antTipo = "";
        var sumaProfesionales = 0;

        for (var i = 0; i < datos.length; i++) {
            
            //Comprobamos si es Rol
            if (datos[i].Tipo == "R") {
                if (antTipo == "P") { liDesgloseRol += "</ul>" }
                liDesgloseRol += "<span idrol='" + datos[i].t004_idrol_actual + "' class='fk-rol'><i style='margin-right:4px' class='glyphicon-plus'></i>" + datos[i].DesRol + "</span> <span >(" + datos[i].Parentesco + ")</span>";
                antTipo = "R";
                sumaProfesionales += datos[i].Parentesco;

               
            }
            
            if (datos[i].Tipo == "P")
            {
                if (antTipo == "R") { liDesgloseRol += "<ul>" }
                
                liDesgloseRol += "<li><span style='margin-bottom: 10px;' class='profesional'>" + datos[i].Profesional + "</span> </li>";
                antTipo = "P";
            }
        };

        liDesgloseRol += "</li>";
                
        $("#spanSumaProfesionales").text(sumaProfesionales);

        //Inyectar html en la página
        $("#ulDesglose").html(liDesgloseRol);


        //Expandir y contraer cada rama del árbol
        $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Plegar');
        $('.fk-rol').on('click', function (e) {
            
            
            $(this).addClass('parent_li').find(' > span').attr('title', 'Plegar');
            
            var children = $(this).next().next().find("li");

            if (children.is(":visible")) {
                $("#txtHeaderProfesionales").removeClass("showInline").addClass("hide");
                children.hide('fast');
                $(this).attr('title', 'Desplegar').find(' > i').addClass('glyphicon glyphicon-plus').removeClass('glyphicon glyphicon-minus');
                
            } else {
                $("#txtHeaderProfesionales").removeClass("hide").addClass("showInline");
                children.show('fast');
                $(this).attr('title', 'Plegar').find(' > i').addClass('glyphicon glyphicon-minus').removeClass('glyphicon glyphicon-plus');
            }

            ponerProfesional();
            e.stopPropagation();
        });


        //Expandir todo el árbol
        $("#imgExpandirTodo").on("click", function () {
            $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Plegar');
            var children = $('.tree li:has(ul)').find(">ul >li")
            $("#txtHeaderProfesionales").removeClass("hide").addClass("showInline");

            children.show('fast');
            $('.tree li.parent_li > span').attr('title', 'Plegar').find(' > i').addClass('glyphicon glyphicon-minus').removeClass('glyphicon glyphicon-plus');
        })

        //Plegar todo el árbol
        $("#imgPlegarTodo").on("click", function () {
            $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Plegar');
            var children = $('.tree li:has(ul)').find(">ul >li")
            $("#txtHeaderProfesionales").removeClass("showInline").addClass("hide");
            children.hide('fast');
            $('.tree li.parent_li > span').attr('title', 'Plegar').find(' > i').addClass('glyphicon glyphicon-plus').removeClass('glyphicon glyphicon-minus');
        })

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}

function ponerProfesional() {
    var encontrado = false;

    for (var i = 0; i < $('.fk-rol').length; i++) {
        if ($('.fk-rol i')[i].className == "glyphicon-minus") {
            encontrado = true;
            break;
        }
    }

    if (encontrado) {
        $("#txtHeaderProfesionales").removeClass("hide").addClass("showInline");
    }

    else {
        $("#txtHeaderProfesionales").removeClass("showInline").addClass("hide");
    }

}

//Foco en pantalla de obtener posibles responsables destino
$('#modal-evaluadores').on('shown.bs.modal', function () {
    $('#inputApellido1').focus();
})


$("#btnCancelar").on("click", function () {
    limpiarFiltros();
    $("#btnObtener").removeClass("show").addClass("hide");
    $("#btnSeleccionar").css("display", "none");
})


function limpiarFiltros() {
    $("#inputApellido1").val("");
    $("#inputApellido2").val("");
    $("#inputNombre").val("");    
}


//Buscar cuando se pulsa intro sobre alguna de las cajas de texto
$('#modal-evaluadores :input[type=text]').on('keyup', function (event) {

    $("#btnSeleccionar").css("display", "none");

    if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "") {
        $("#btnObtener").removeClass("show").addClass("hide");
        return;
    }

    $("#btnObtener").removeClass("hide").addClass("show");

    //Vaciamos la tabla al introducir una nueva búsquda
    if ($("#lisEvaluadores").length > 0) {
        $("#lisEvaluadores").html("");
    }

    if (event.keyCode == 13) {
        
        if ($("#inputApellido1").val() == "" && $("#inputApellido2").val() == "" && $("#txtNombre").val() == "" && $("#tblgetFicepi li").length == 0) {
            alertNew("warning", "No se permite realizar búsquedas con los filtros vacíos");
            return;
        }

        if (origen == "ADM") {
            //Si es administrador puede buscar entre todos los evaluadores
            obtener();
        }
        
    }
});



$("#btnObtener").on("click", function () {
    obtener();
})

function obtener() {

    $lisEvaluadores = $('#lisEvaluadores');
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
                limpiarFiltros();
                $("#btnObtener").removeClass("show").addClass("hide");
                $("#btnSeleccionar").css("display","inline-block");
                $(result.d).each(function () { $("<li class='list-group-item' data-sexo='" + this.Sexo + "' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadores); });
                $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
                $('#modal-evaluadores').modal('show');

            } else {                
                alertNew("warning", "Ningún evaluador responde a la búsqueda por los filtros establecidos", null, 2000, null);
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluadores");
        }
    });
}


function obtenerLista() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/getEvaluadoresDescendientes",
        "data": JSON.stringify({ idficepi: idficepi }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {            
            lista = $(result.d);

            if ($(result.d).length > 1) {
                $("#lblEvaluador").addClass("btn btn-link underline");
                //***Modal selección de evaluadores (al hacer click sobre el link de Evaluador)
                $('#lblEvaluador').on('click', mostrarModal);
            }

            else {
                $("#lblEvaluador").removeClass("btn btn-link underline");
            }
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido obtener el desglose de roles");
        }
    });
}


function mostrarModal() {
    if (origen =="ADM") {
        $('#modal-evaluadores').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        $('#modal-evaluadores').modal('show');
        $('#lisEvaluadores').children().remove();
        $('#modal-evaluadores input[type=text]').val('');
    }
    else {
       
        $lisEvaluadoresMiquipo = $('#lisEvaluadoresMiequipo');
        $('#modal-evaluadoresMiequipo').modal({ keyboard: false, backdrop: "static", dismiss: "modal" });

        $('#lisEvaluadoresMiequipo').children().remove();

        $(lista).each(function () { $("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisEvaluadoresMiquipo); });

        $('#modal-evaluadoresMiequipo').modal('show');

    }
}

function exportarExcel() {

    //validaciones
    if ($("#ulDesglose").find("span.fk-rol").length == 0) {
        alertNew("warning", "No hay datos para exportar");
        return;
    }

    //Cargar en el iframe la página de exportación.
    var qs = "pantalla=desgloserol&idficepi=" + $("#evaluador").attr("idficepi") + "&idficepitext=" + $("#evaluador").val() + "&parentesco=" + $("#cboProfundizacion").val() + "&parentescotext=" + $("#cboProfundizacion option:selected").text() + "&numprofesionales=" + $("#ulDesglose ul li").length;
    $("#ifrmExportExcel").prop("src", strServer + "Capa_Presentacion/Utilidades/ExportarExcel.aspx?" + qs);

}