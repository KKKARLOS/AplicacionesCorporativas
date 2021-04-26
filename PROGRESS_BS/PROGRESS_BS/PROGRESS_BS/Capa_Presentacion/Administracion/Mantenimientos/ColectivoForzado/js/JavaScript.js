var $listaColectivos;

$(document).ready(function () {

   ColectivoForzado();
})


function ColectivoForzado() {
    
    $.ajax({
        url: "Default.aspx/ColectivoForzado",
        type: "POST",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
          
            pintarDatosPantalla(result.d);

        },
        error: function (ex, status) {            
            alertNew("danger", "No se ha podido obtener el catálogo del colectivo forzado");
        }
    });
}


function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML PROFESIONALES
        var tblColectivo = "";

        for (var i = 0; i < datos.SelectForzadas1.length; i++) {

            tblColectivo += "<tr class='list-group-item' data-idficepi='" + datos.SelectForzadas1[i].t001_idficepi + "'>";
            tblColectivo += "<td  style='width:350px'>" + datos.SelectForzadas1[i].profesional + "</td>";
            
            tblColectivo += "</tr>";
        };

        //Inyectar html en la página
        $("#tblColectivo").html(tblColectivo);


        //PINTAR HTML PROFESIONALES FORZADOS
        var tblColectivoForzado = "";
        $listaColectivos = datos.SelectForzadas3;

        for (var i = 0; i < datos.SelectForzadas2.length; i++) {

            tblColectivoForzado += "<tr class='list-group-item' data-idficepi='" + datos.SelectForzadas2[i].t001_idficepi + "'>";
            tblColectivoForzado += "<td style='width:350px'>" + datos.SelectForzadas2[i].profesional + "</td>";
            tblColectivoForzado += "<td class='fk_select'><select>" + pintaOptionsCombo($listaColectivos, datos.SelectForzadas2[i].t941_idcolectivo) + "</select></td>";

            tblColectivoForzado += "</tr>";
        };

        //Inyectar html en la página
        $("#tblColectivoForzados").html(tblColectivoForzado);

        $("select").on("change", function () {
            grabar();
        })

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}


function grabar() {
    var lista = [];
   
    $('#tblColectivoForzados tr').each(function () {
        var oForzados = new Object();
        oForzados.t001_idficepi = $(this).attr("data-idficepi");        
        oForzados.t941_idcolectivo = $(this).find("option:selected").val();
        lista.push(oForzados);
    });
    
    actualizarSession();
    $.ajax({
        url: "Default.aspx/update",   // Current Page, Method
        data: JSON.stringify({ lista: lista }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        success: function (result) {
            ColectivoForzado();
        },
        error: function (ex, status) {
            alertNew("danger", "Error al intentar actualizar.");
        }

    });
}

//Agrega las options al control 
function pintaOptionsCombo(listaColectivos, idcolectivo) {

    var s = "";
    //Si el colectivo viene null, le pintamos opción en blanco. Si ya tiene valor, no pintamos opción en blanco
    if (idcolectivo == null)
        s += "<option value=''></option>";
    for (var i = 0; i < listaColectivos.length; i++) {
        s += "<option value=" + listaColectivos[i].t941_idcolectivo;
        if (listaColectivos[i].t941_idcolectivo == idcolectivo) {
            s += " selected= selected";
        }

        s += ">" + listaColectivos[i].t941_denominacion + "</option>";
    }
    return s;
}


/*PROFESIONALES IZQUIERDA*/
//Selección de profesionales
$('body').on('click', '#tblColectivo tr', function (e) {
    lista = $(this).parent().children();
    if (e.shiftKey && lista.filter('.active').length > 0) {
        first = lista.filter('.active:first').index();//Primer seleccionado
        last = lista.filter('.active:last').index();//Último seleccionado
        $('#tblColectivo tr').removeClass('active');//Borrar de las dos listas
        if ($(this).index() > first)
            lista.slice(first, $(this).index() + 1).addClass('active');
        else
            lista.slice($(this).index(), last + 1).addClass('active');
    }
    else if (e.ctrlKey) {
        $(this).toggleClass('active');
    } else {
        //SÓLO DEJA HACER SELECCIÓN MÚLTIPLE CON SHIFT Y CONTROL (VERSIÓN VIEJA)
        //$('#divProfesionales .list-group .list-group-item').removeClass('active');
        //$(this).addClass('active');

        //SELECCIÓN MÚLTIPLE SIEMPRE
        $(this).toggleClass('active');
    }
});

//Seleccionar todos o ninguno
$('#divProfesionales .dual-list .selector').on('click', function () {
    var $checkBox = $(this);
    if (!$checkBox.hasClass('selected')) {
        $checkBox.addClass('selected').closest('.well').find('tr:not(.active)').addClass('active');
        $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
    } else {
        $checkBox.removeClass('selected').closest('.well').find('tr.active').removeClass('active');
        $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
    }
});

//Buscar
$('#divProfesionales [name="SearchDualList"]').on('keyup', function (e) {
    var code = e.keyCode || e.which;
    if (code == '9') return;
    if (code == '27') $(this).val(null);
    var $rows = $(this).closest('.dual-list').find('.list-group tr');
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
    $rows.show().filter(function () {
        var text = normalize($(this).text().replace(/\s+/g, ' ').toLowerCase());
        return !~text.indexOf(normalize(val));
    }).hide();
});


//Click en los botones
$('#divProfesionales .list-arrows button').on('click', function () {
    var $button = $(this), actives = '';
    if ($button.hasClass('move-left')) {
        actives = $('.list-right tr.active');
        if (actives.length > 0) {
            //Borramos la celda con el select
            actives.find("td.fk_select").remove();            
            actives.clone().appendTo('#tblColectivo');            
            ordenar($('.list-left tr'));
            actives.remove();
           
        } else {
            alertNew("warning", "Tienes que seleccionar algún profesional");
        }
    } else if ($button.hasClass('move-right')) {
        actives = $('.list-left tr.active');
        if (actives.length > 0) {
            //Creamos un td con el select
            var lst = actives;            
            lst.each(function (index, element) {                
                var h = $(this).html();
                h += "<td><select>" + pintaOptionsCombo($listaColectivos, 1) + "</select></td>";
                $(this).html(h);
            })
            
            lst.appendTo("#tblColectivoForzados");
            
            ordenar($('.list-right tr'));
        } else {
            alertNew("warning", "Tienes que seleccionar algún profesional");
        }
    }
    //actives.remove();
    $('.list-group-item').removeClass('active');
    $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
    grabar();
});


/*FIN PROFESIONALES IZQUIERDA*/



/*PROFESIONALES DERECHA*/
$('body').on('click', '#tblColectivoForzados tr', function (e) {
    lista = $(this).parent().children();
    if (e.shiftKey && lista.filter('.active').length > 0) {
        first = lista.filter('.active:first').index();//Primer seleccionado
        last = lista.filter('.active:last').index();//Último seleccionado
        $('#tblColectivoForzados tr').removeClass('active');//Borrar de las dos listas
        if ($(this).index() > first)
            lista.slice(first, $(this).index() + 1).addClass('active');
        else
            lista.slice($(this).index(), last + 1).addClass('active');
    }
    else if (e.ctrlKey) {
        $(this).toggleClass('active');
    } else {
        //SÓLO DEJA HACER SELECCIÓN MÚLTIPLE CON SHIFT Y CONTROL (VERSIÓN VIEJA)
        //$('#divProfesionales .list-group .list-group-item').removeClass('active');
        //$(this).addClass('active');

        //SELECCIÓN MÚLTIPLE SIEMPRE
        $(this).toggleClass('active');
    }
});




/*FIN PROFESIONALES DERECHA*/