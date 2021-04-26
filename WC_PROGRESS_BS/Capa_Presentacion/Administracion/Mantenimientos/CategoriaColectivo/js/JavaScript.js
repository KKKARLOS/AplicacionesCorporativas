
$(document).ready(function () {

    catalogo();

})

function catalogo() {

    actualizarSession();
    $.ajax({
        url: "Default.aspx/catalogo",
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
            alertNew("danger", "No se ha podido obtener el catálogo de categoría/colectivo");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  

        var tblCategoriaColectivo = "";

        var $listaColectivos = datos.Select1;

        for (var i = 0; i < datos.Select2.length; i++) {

            tblCategoriaColectivo += "<tr data-idcategoriaprofesional='" + datos.Select2[i].T935_idcategoriaprofesional + "'>";
            tblCategoriaColectivo += "<td>" + datos.Select2[i].T935_denominacion + "</td>";
            tblCategoriaColectivo += "<td><select>" + pintaOptionsCombo($listaColectivos, datos.Select2[i].T941_idcolectivoColectivo) + "</select></td>";
            tblCategoriaColectivo += "</tr>";
        };

        //Inyectar html en la página
        $("#tbdCategoriaColectivo").html(tblCategoriaColectivo);

      
        $("select").on("change", function () {            
            $.ajax({
                url: "Default.aspx/update",                
                "data": JSON.stringify({ t935_idcategoriaprofesional: $(this).parent().parent().attr("data-idcategoriaprofesional"), t941_idcolectivo: $(this).find(":selected").val() }),
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
               
                error: function (ex, status) {
                    alertNew("danger", "No se ha podido actualizar categoría/colectivo");
                }
            });
        })

        //$("#tableEval").trigger("destroy");

        // add parser through the tablesorter addParser method
        //$.tablesorter.addParser({
        //    // set a unique id
        //    id: 'grades',
        //    is: function () {
        //        // return false so this parser is not auto detected
        //        return false;
        //    },
        //    format: function (s) {
        //        return s.toLowerCase()
        //          .replace(/good/, 2)
        //          .replace(/medium/, 1)
        //          .replace(/bad/, 0);
        //    },
        //    type: 'numeric'
        //});


        ////Ordenación de columnas
        //$("#tableEval").tablesorter({
        //    //dateFormat: "dd/mm/yy", // set the default date format
        //    // pass the headers argument and assing a object            
        //    headers: {
        //        // set "sorter : false" (no quotes) to disable the column
        //        0: {
        //            sorter: "text"
        //        },
        //        1: {
        //            sorter: "select"
        //        }
                
        //    }
        //});

    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}

//Agrega las options al control 
function pintaOptionsCombo(listaColectivos, idcolectivo) {

    var s = "";
    //Si el colectivo viene null, le pintamos opción en blanco. Si ya tiene valor, no pintamos opción en blanco
    if (idcolectivo == null) 
        s += "<option value=''></option>";
    for (var i = 0; i < listaColectivos.length; i++) {               
        s += "<option value=" + listaColectivos[i].t941_idcolectivo;
        if (listaColectivos[i].t941_idcolectivo == idcolectivo)
        {
            s += " selected= selected";
        }

        s += ">" + listaColectivos[i].t941_denominacion + "</option>";        
    }
    return s;
}



//$(function () {
//    $("table").tablesorter({
//        theme: 'blue'
//        // "extractor-select" and "sorter-grades" are added as a class name in the HTML
//        // or un-comment out the option below
//        // ,headers: { 6: { extractor: 'select', sorter: 'grades' } }
//    });

//});