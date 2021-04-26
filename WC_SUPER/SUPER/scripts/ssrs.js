//Sql-Server Reporting Services
function PostSSRS(params, servidorSSRS) {
    if (typeof mostrarProcesando === "function") mostrarProcesando();
    var form = $(document.createElement("form")).attr({ "method": "post", "action": servidorSSRS });
    form.css("display", "none");
    $.each(params, function (key, value) {
        $.each(value instanceof Array ? value : [value],
            function (i, val) {
                $(document.createElement("input")).attr({ "type": "hidden", "name": key, "value": Utilidades.escape(val) }).appendTo(form);
            })
    });
    form.appendTo(document.body).submit();
    if (typeof mostrarProcesando === "function") setTimeout(ocultarProcesando, 5000);
}