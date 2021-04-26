var $ulresultado = $("#resultadoVisualizados");
var $resultado = $("#lisVisualizados li");
var $resultadoVisualizados;
var $tablaGuardada;
var addVisualizador;
var bCambios;
$(document).ready(function () {
   
    catalogoVisionesAjenasArbol();

    //SELECCIÓN DE VISUALIZADOS
    //Selección de roles
    $('body').on('click', '#divVisualizados .list-group .list-group-item', function (e) {
        lista = $(this).parent().children();
        if (e.shiftKey && lista.filter('.active').length > 0) {
            first = lista.filter('.active:first').index();//Primer seleccionado
            last = lista.filter('.active:last').index();//Último seleccionado
            $('#divVisualizados .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
            if ($(this).index() > first)
                lista.slice(first, $(this).index() + 1).addClass('active');
            else
                lista.slice($(this).index(), last + 1).addClass('active');
        }
        else if (e.ctrlKey) {
            $(this).toggleClass('active');
        } else {
            //$('#divRoles .list-group .list-group-item').removeClass('active');
            //$(this).addClass('active');                 
            $(this).toggleClass('active');
        }

    });

    //Click en los botones
    $('#divVisualizados .list-arrows button').on('click', function () {
        var $button = $(this), actives = '';
        if ($button.hasClass('move-left')) {
            actives = $('#divlisVisualizados.list-right ul li.active');
            if (actives.length > 0) {                
                actives.clone().appendTo('#lisPersonas');
                //todo revisar ordenar.. no está funcionando correctamente en esta pantalla

                //ordenar($('#divlisPersonas.list-left ul li'));
                bCambios = true;
            } else {
                alertNew("warning", "Tienes que seleccionar algún profesional");
            }
        } else if ($button.hasClass('move-right')) {
            actives = $('#divlisPersonas.list-left ul li.active');
            if (actives.length > 0) {                
                actives.clone().appendTo('#lisVisualizados');
                //todo revisar ordenar, no está funcionando correctamente. 

                //ordenar($('#divlisVisualizados.list-right ul li'));
                bCambios = true;
            } else {
                alertNew("warning", "Tienes que seleccionar algún profesional");
            }
        }
        actives.remove();
        $('.list-group-item').removeClass('active');
        $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
        
    });

    //Seleccionar todos o ninguno
    $('.dual-list .selector').on('click', function () {
        var $checkBox = $(this);
        if (!$checkBox.hasClass('selected')) {
            $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
            $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
        } else {
            $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
            $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
        }
    });

    //Buscar
    $('[name="SearchDualList"]').on('keyup', function (e) {
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
    //FIN SELECCIÓN DE VISUALIZADOS

    

    $('[data-toggle="popover"]').popover({
        trigger: 'hover',
        html: true,

    });

})



function catalogoVisionesAjenasArbol() {
    actualizarSession();
    $.ajax({
        url: "Default.aspx/catalogoVisionesAjenasArbol",
        //"data": JSON.stringify(),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            pintarDatosPantalla(result.d);
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido obtener el catálogo de visiones ajenas al árbol de dependencias");
        }
    });
}



function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  
        var liVisionesAjeasArbol = "";
        liVisionesAjeasArbol += "<li>";
        var antTipo = "P";
        var ficepiPadre;

        for (var i = 0; i < datos.length; i++) {

            //Comprobamos si es Visualizador
            if (ficepiPadre != datos[i].Idficepi_visualizador) {
                if (antTipo == "H") { liVisionesAjeasArbol += "</ul>" }
                liVisionesAjeasArbol += "<i style='margin-right:4px; cursor:pointer;' class='glyphicon-plus fk-vision '></i><span data-sexo='" + datos[i].Sexo_Visualizador + "' id='" + datos[i].Idficepi_visualizador + "'>" + datos[i].Nombre_visualizador + "</span>";
                antTipo = "P";
            }

           
            if (antTipo == "P") { liVisionesAjeasArbol += "<ul>" }

            liVisionesAjeasArbol += "<li><span data-idficepivisualizado='" + datos[i].Idficepi_visualizado + "' style='margin-bottom: 10px; width:240px' class='profesional'>" + datos[i].Nombre_visualizado + "</span><span data-accion='" + datos[i].T949_accion + "'>" + getAccion(datos[i].T949_accion) + "</span> </li>";
            antTipo = "H";
            ficepiPadre = datos[i].Idficepi_visualizador;
           
            
            liVisionesAjeasArbol += "</li>";
        }
        //Inyectar html en la página
        $("#ulVisionesAjeasArbol").html(liVisionesAjeasArbol);


        //Expandir y contraer cada rama del árbol        
        $('.tree li:has(ul)').addClass('parent_li').find(' > i').attr('title', 'Desplegar');
       
        $(".fk-vision").next().on("click", function () {
            $(".fk-vision").next().removeClass("active");
            $(this).toggleClass('active');
        });


        $('.fk-vision').on('click', function (e) {

            $(this).addClass('parent_li').find(' > i').attr('title', 'Plegar');

            //Quitamos la clase active a todos los spanes 
            //$(".fk-vision").next().removeClass("active");

            //Sólo añadimos al elemento clickado
            //$(this).addClass("active");

            $(this).toggleClass('active');

            var children = $(this).next().next().find("li");

            if (children.is(":visible")) {
                children.hide('fast');
                $(this).attr('title', 'Desplegar').addClass('glyphicon glyphicon-plus').removeClass('glyphicon glyphicon-minus');

            } else {
                children.show('fast');
                $(this).attr('title', 'Plegar').addClass('glyphicon glyphicon-minus').removeClass('glyphicon glyphicon-plus');
            }

            putHeaders();
            e.stopPropagation();
        });


        //Expandir todo el árbol
        $("#imgExpandirTodo").on("click", function () {            
            var children = $('.tree li:has(ul)').find(">ul >li")
            $("#txtHeaderVisualizados").removeClass("hide").addClass("show");
            $("#txtHeaderAmbitoVision").removeClass("hide").addClass("show");
            children.show('fast');
            $('.tree li.parent_li > i').attr('title', 'Plegar').addClass('glyphicon glyphicon-minus').removeClass('glyphicon glyphicon-plus');
        })

        //Plegar todo el árbol
        $("#imgPlegarTodo").on("click", function () {            
            var children = $('.tree li:has(ul)').find(">ul >li")
            $("#txtHeaderVisualizados").removeClass("show").addClass("hide");
            $("#txtHeaderAmbitoVision").removeClass("show").addClass("hide");
            
            children.hide('fast');
            $('.tree li.parent_li > i').attr('title', 'Desplegar').addClass('glyphicon glyphicon-plus').removeClass('glyphicon glyphicon-minus');
        })

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}


function getAccion(accion) {
    switch (accion) {
        case "V":
            return "Profesional y sus dependientes";

        case "N":
            return "Sólo dependientes";

        case "C":
            return "Sólo profesional"

        default:
            return "";
    }
}


function putHeaders() {
    var encontrado = false;

    for (var i = 0; i < $('.fk-vision').length; i++) {
        if ($('.fk-vision')[i].title == "Plegar") {
            encontrado = true;
            break;
        }
    }

    if (encontrado) {
        $("#txtHeaderVisualizados").removeClass("hide").addClass("show");
        $("#txtHeaderAmbitoVision").removeClass("hide").addClass("show");

    }

    else {
        $("#txtHeaderVisualizados").removeClass("show").addClass("hide");
        $("#txtHeaderAmbitoVision").removeClass("show").addClass("hide");

    }

}



//Eliminar visualizador
$("#delVisualizador").on("click", function () {
    
    if ($("span.active").length > 0) {        
        $("#modal-confirmacion-eliminacion").modal("show");       
    }

    else {
        alertNew("warning", "No tienes ningún visualizador seleccionado.");
    }
})


$("#btnAceptarEliminacion").on("click", function () {
    $.ajax({
        url: "Default.aspx/deleteVisualizador",
        "data": JSON.stringify({ idficepiVisualizador: $("span.active").attr("id") }),
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $("span.active").prev().remove();
            $("span.active").next().remove();
            $("span.active").remove();
            $("#modal-confirmacion-eliminacion").modal("hide");
        },
        error: function (ex, status) {
            alertNew("danger", "No se ha podido borrar al visualizador");
        }
    });
})


$("#addVisualizador").on("click", function () {
    $("#modal-mantenimiento").modal("show");
    addVisualizador = true;
    $("#lblVisualizadorModal").css("display", "inline-block");
    $("#evaluador").val("");
    $("#spanVisualizador").css("display", "none");

})

//MODAL BÚSQUEDA DE EVALUADORES
//***Modal selección de evaluadores (al hacer click sobre el link de Visualizador)
$('#lblVisualizadorModal').on('click', function () {
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
    $.ajax({
        url: "Default.aspx/selVisualizados",   // Current Page, Method
        data: JSON.stringify({ t001_idficepi_visualizador: $evaluador.val()}),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {               
                
                //TODO PINTAR TABLA CON RESULTADO

                var select = $("<select><option value='V'>Profesional y sus dependientes</option><option value='N'>Sólo dependientes</option><option value='C'>Sólo profesional</option></select>");
                var tbl = "";

                tbl += "<table id='tabla' class='table table-striped'><thead class='bck-ibermatica'><tr><th>Visualizados/as</th><th>Ámbito de visión</th></tr>";
                tbl += "<tbody>";

                for (var i = 0; i < $result.length; i++) {
                    tbl += "<tr data-id='" + $result[i].Idficepi_visualizado + "'><td>" + $result[i].Nombre_visualizado + "</td>";
                    tbl += "<td><select id='selectAmbitoVision'>";
                    tbl += pintaOptionsCombo(select, $result[i].T949_accion);
                    tbl += "</td></tr>";
                }

                tbl += "</tbody>";

            }
            else {
                var tbl = "";

                tbl += "<table id='tabla' class='table table-striped'><thead class='bck-ibermatica'><tr><th>Visualizadores/as</th></tr>";
                tbl += "<tbody></tbody>";

            }

            $("#resultadoVisualizados").html(tbl);


            $('#evaluador').attr("data-sexo", $evaluador.attr('data-sexo'));
            if ($('#evaluador').attr("data-sexo") == "V") {
                $("#lblVisualizadorModal").text("Visualizador");
                $("#spanVisualizador").text("Visualizador");
            }
            else {
                $("#lblVisualizadorModal").text("Visualizadora");
                $("#spanVisualizador").text("Visualizadora");
            } 
            $('#modal-evaluadores').modal('hide');
            $('#evaluador').attr("idficepi", $evaluador.attr('value')).val($evaluador.text());
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los evaluadores");
        }
    });

});

//Poner el foco en la caja de texto del apellido1
$('#modal-evaluadores').on('shown.bs.modal', function () {
    $('#inputApellido1').focus();
})

//Botón cancelar de evaluador
$('#modal-evaluadores #btnCancelar').on('click', function () {
    $('#modal-evaluadores').modal('hide');
});


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

        obtener();
       
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
                $("#btnSeleccionar").css("display", "inline-block");
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



//Poner el foco en la caja de texto del apellido1
$('#modal-evaluadores').on('shown.bs.modal', function () {
    $('#txtApellido1').focus();
})
//***Fin Modal selección de evaluadores

function limpiarFiltros() {
    $("#inputApellido1").val("");
    $("#inputApellido2").val("");
    $("#inputNombre").val("");
}


$("#modal-mantenimiento").on('shown.bs.modal', function (){

    if (addVisualizador) {
        var tbl = "";
        tbl += "<table id='tabla' class='table table-striped'><thead class='bck-ibermatica'><tr><th>Visualizados/as</th><th>Ámbito de visión</th></tr>";
        tbl += "<tbody></tbody>";

        $("#resultadoVisualizados").html(tbl);
    }
   
})



$('#modal-seleccionVisualizados').on('shown.bs.modal', function () {

    $("#Headermodal-seleccionVisualizados").text("Selección de visualizados/as de " + $("#evaluador").val());
    
    //$("#lisVisualizados").html($resultadoVisualizados);
    
    $lisPersonas = $('#lisPersonas');

    //$($("#lisVisualizados li")).each(function () {
    //    this.remove();
    //});
    $("#lisVisualizados").html("");

    $($("#tabla tbody tr")).each(function () {
        $("<li class='list-group-item' value='" + this.getAttribute("data-id") + "'>" + this.firstChild.innerText + "</li>").appendTo($("#lisVisualizados"));
    });

    //$($("#lisPersonas li")).each(function () {
    //    this.remove();
    //});
   
    //todo llamada ajax para traer a los profesionales de alta
    $.ajax({
        url: "Default.aspx/getProfesionales",   // Current Page, Method
        data: JSON.stringify({ t001_idficepi_visualizador: $("#evaluador").attr("idficepi") }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            $result = $(result.d);
            if ($result.length > 0) {
                var strb = new StringBuilder()
                console.log(Date.now());
                $(result.d).each(function () {
                    strb.Append("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>");
                    //$("<li class='list-group-item' value='" + this.t001_idficepi + "'>" + this.nombre + "</li>").appendTo($lisPersonas);
                });
                $lisPersonas.append(strb.ToString());
                //strb.ToString().appendTo($lisPersonas);
                console.log(Date.now());
            } else {
                alertNew("warning", "No se han obtenido resultados", null, 2000, null);
            }
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar obtener los profesionales");
        }
    });


})


$("#btnAceptarVisualizados").on("click", function () {

    if (bCambios) {
        $("#modal-seleccionVisualizados").modal("hide");
        $ulresultado.html($("#lisVisualizados li"));

        $resultadoVisualizados = $("#resultadoVisualizados li");

        var select = $("<select id='selectAmbitoVision'><option value='V'>Profesional y sus dependientes</option><option value='N'>Sólo dependientes</option><option value='C'>Sólo profesional</option></select>");

        var tbl = "";

        tbl += "<table id='tabla' class='table table-striped'><thead class='bck-ibermatica'><tr><th>Visualizados/as</th><th>Ámbito de visión</th></tr>";
        tbl += "<tbody>";

        for (var i = 0; i < $ulresultado.find("li").length ; i++) {
            tbl += "<tr data-id='" + $ulresultado.find("li")[i].value + "'><td>" + $ulresultado.find("li")[i].innerText + "</td>";

            tbl += "<td><select>";

            for (var j = 0; j < $tablaGuardada.find("tbody tr").length; j++) {
                if ($ulresultado.find("li")[i].value == $tablaGuardada.find("tbody tr")[j].getAttribute("data-id")) {
                    tbl += pintaOptionsCombo(select, $tablaGuardada.find("tbody tr")[j].children[1].firstChild.value);
                    tbl += "</select></td>";
                    break;

                }
                //else {
                //    tbl += pintaOptionsCombo(select, "V");
                //    //break;
                //}
            }

            tbl += "<option value='V'>Profesional y sus dependientes</option><option value='N'>Sólo dependientes</option><option value='C'>Sólo profesional</option>";
            //tbl += pintaOptionsCombo(select, "");

            //if (addVisualizador) {
            //    tbl += pintaOptionsCombo(select, "");
            //}
            tbl += "</select></td></tr>";

        }

        tbl += "</tbody>";

        $("#resultadoVisualizados").html(tbl);

        $("#resultadoVisualizados").children("option").remove();

        $("#lisPersonas").html("");

        //ul2table($ulresultado);
    }

    else {

        alertNew("warning", "No has añadido ningún profesional.");
        return;
        //$($("#lisPersonas li")).each(function () {
        //    this.remove();
        //});
        //$("#modal-seleccionVisualizados").modal("hide");

    }

    
    

})


function ul2table(ul) {
    var tbl = $('<table id="tabla" class="table table-striped"><thead><th>Profesional</th><th>Ámbito de visión</th>');
    var select = "<select id='selectAmbitoVision'><option value='V'>Profesional y sus dependientes</option><option value='N'>Sólo dependientes</option><option value='C'>Sólo profesional</option></select>";

    var selectObjeto = $("<select id='selectAmbitoVision'><option value='V'>Profesional y sus dependientes</option><option value='N'>Sólo dependientes</option><option value='C'>Sólo profesional</option></select>");

    ul.find('li').each(function () {
        tbl.append($('<tr data-id="' + $(this).val() + '"/>')
            .append($('<td>').html($(this).html()))
            .append($('<td>').html(select)));
    });

    //$ulresultado.replaceWith(tbl);

    ul.html(tbl);
    
}


function ordenar(objsLis) {
    try {
        objsLis.sort(function (a, b) {
            var A = $(a).text().toUpperCase();
            var B = $(b).text().toUpperCase();
            return (A < B) ? -1 : (A > B) ? 1 : 0;
        });
        objsLis.parent().html(objsLis);
    } catch (e) {
        alertNew("danger", "Error al intentar ordenar la lista.");
    }
}



$('body').on('change', 'select', function (e) {
    $(this).val();
})



$("#btnGrabar").on("click", function () {

    if ($("#evaluador").val() == "") 
    {
        alertNew("warning", "Para grabar, tienes que tener un/a visualizador/a seleccionado/a");      
        return;
    }
    
    else if($("#tabla tbody tr").length == 0)
    {
        if (addVisualizador) alertNew("warning", "Para grabar, tienes que tener por lo menos un/a visualizado/a seleccionado/a");
        else alertNew("warning", "Para grabar, tienes que tener por lo menos un/a visualizado/a seleccionado/a. Si lo que quieres es eliminar al visualizador/a, pulsa 'Cancelar' y haz la eliminación desde la pantalla que se te muestra.", null, 5000, null);
        return;
    }
    grabarDatos();    
})

function grabarDatos() {

    var arr = [];
    //Seleccionamos la fila editada y pasamos el Array
    $("#tabla tbody tr").each(function (index) {
        oVisualizados = new Object();        
        oVisualizados.Idficepi_visualizador = $("#evaluador").attr("idficepi");
        oVisualizados.Idficepi_visualizado = $(this).attr("data-id");
        oVisualizados.T949_accion = $(this).find("select").val();
        
        arr.push(oVisualizados);
    });

    actualizarSession();

    $.ajax({
        url: "Default.aspx/GrabarDatos",   // Current Page, Method
        data: JSON.stringify({ oVisualizadores: arr }),  // parameter map as JSON                
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 30000,    // AJAX timeout
        success: function (result) {
            $("#modal-mantenimiento").modal("hide");
            $("#tabla").html("");
            $("#evaluador").val("");
            $("input[name=SearchDualList]").val("");
            catalogoVisionesAjenasArbol();            
        },
        error: function (ex, status) {
            ocultarProcesando();
            error$ajax("Ha habido un error grabando los datos.", ex, status)
        }
    });
}

$("#mntVisualizados").on("click", function () {
    if ($("#evaluador").val() == "") {
        alertNew("warning", "Para seleccionar visualizados/as, tienes que tener seleccionado algún visualizador/a.");        
        return;
    }
    else {
        $tablaGuardada = $("#tabla");
        $("#modal-seleccionVisualizados").modal("show");        
    }
})


$("#editVisualizador").on("click", function () {

    addVisualizador = false;
    if ($(".fk-vision + span.active").length == 0) {
        alertNew("warning", "Para modificar, tienes que tener seleccionado algún visualizador/a.");
        return;
    }
        
    $("#modal-mantenimiento").modal("show");
    $("#evaluador").val($(".fk-vision + span.active").text());
    $("#evaluador").attr("idficepi", $(".fk-vision + span.active").attr("id"));
    $("#evaluador").attr("data-sexo", $(".fk-vision + span.active").attr("data-sexo"));

    $("#lblVisualizadorModal").css("display", "none");
    $("#spanVisualizador").css("display", "inline-block");
    
    
    
    var select = $("<select id='selectAmbitoVision'><option value='V'>Profesional y sus dependientes</option><option value='N'>Sólo dependientes</option><option value='C'>Sólo profesional</option></select>");
    var tbl = "";

    tbl += "<table id='tabla' class='table table-striped'><thead class='bck-ibermatica'><tr><th>Visualizados/as</th><th>Ámbito de visión</th></tr>";
    tbl += "<tbody>";

    for (var i = 0; i < $(".fk-vision + span.active").next().children().length; i++) {
        tbl += "<tr data-id='" + $(".fk-vision + span.active").next().children()[i].firstChild.getAttribute("data-idficepivisualizado") + "'><td>" + $(".fk-vision + span.active").next().children()[i].firstChild.innerText + "</td>";
        tbl += "<td><select id='selectAmbitoVision'>";
        tbl += pintaOptionsCombo(select, $(".fk-vision + span.active").next().children()[i].children[1].getAttribute("data-accion"));
        tbl += "</td></tr>";
    }

    tbl += "</tbody>";

    $("#resultadoVisualizados").html(tbl);

    if ($('#evaluador').attr("data-sexo") == "V") {
        $("#lblVisualizadorModal").text("Visualizador");
        $("#spanVisualizador").text("Visualizador");
    }
    else {
        $("#lblVisualizadorModal").text("Visualizadora");
        $("#spanVisualizador").text("Visualizadora");
    }

})

$("#btnCancelarVisualizados").on("click", function () {
    //$($("#lisVisualizados li")).each(function () {
    //    $(this).remove();
    //});
    $("#lisVisualizados").html("");

    //$($("#lisPersonas li")).each(function () {
    //    this.remove();
    //});
    $("#lisPersonas").html("");
    $("input[name=SearchDualList]").val("");
})

//Agrega las options al control 
function pintaOptionsCombo(listaAcciones, accion) {

    var s = "";  
    for (var i = 0; i < listaAcciones.find("option").length; i++) {
        s += "<option value=" + listaAcciones.find("option")[i].value;
        if (listaAcciones.find("option")[i].value == accion) {
            s += " selected= selected";
        }

        s += ">" + listaAcciones.find("option")[i].innerText + "</option>";
    }
    return s;
}

$("#btnCancelarGrabar").on("click", function () {
    $("#tabla").html("");
    $("#lblVisualizadorModal").text("Visualizador");
    $("#spanVisualizador").text("Visualizador");   
})