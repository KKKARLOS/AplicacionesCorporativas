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
            alertNew("danger", "No se ha podido obtener el catálogo de colectivo/formulario");
        }
    });
}

function pintarDatosPantalla(datos) {
    try {
        //PINTAR HTML  

        var tblColectivoFormulario = "";

        var $listaColectivos = datos.Select1;

        for (var i = 0; i < datos.Select2.length; i++) {

            tblColectivoFormulario += "<tr data-idcolectivo='" + datos.Select2[i].t941_idcolectivo + "'>";
            tblColectivoFormulario += "<td>" + datos.Select2[i].t941_denominacion + "</td>";
            tblColectivoFormulario += "<td><select>" + pintaOptionsCombo($listaColectivos, datos.Select2[i].t934_idmodeloformulario) + "</select></td>";
            tblColectivoFormulario += "</tr>";
        };

        //Inyectar html en la página
        $("#tbdColectivoFormulario").html(tblColectivoFormulario);



        $("select").on("change", function () {
            $.ajax({
                url: "Default.aspx/update",
                "data": JSON.stringify({ t941_idcolectivo: $(this).parent().parent().attr("data-idcolectivo"), t934_idmodeloformulario: $(this).find(":selected").val() }),
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 20000,
                success: function (result) {                    
                },
                error: function (ex, status) {
                    alertNew("danger", "No se ha podido actualizar categoría/colectivo");
                }
            });
        })


    }
    catch (e) {
        mostrarErrorAplicacion("Ocurrió un error al pintar la tabla.", e.message)
    }

}

//Agrega las options al control 
function pintaOptionsCombo(listaColectivos, idformulario) {

    var s = "";
    //Si el colectivo viene null, le pintamos opción en blanco. Si ya tiene valor, no pintamos opción en blanco
    if (idformulario == null)
        s += "<option value=''></option>";
    for (var i = 0; i < listaColectivos.length; i++) {
        s += "<option value=" + listaColectivos[i].T934_idmodeloformulario;
        if (listaColectivos[i].T934_idmodeloformulario == idformulario) {
            s += " selected= selected";
        }

        s += ">" + listaColectivos[i].T934_denominacion + "</option>";
    }
    return s;
}


