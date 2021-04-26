$(document).ready(function() {
    $("#ctl00_CPHC_btnEjecutar").bind("click", Ejecutar);
    $("#tblProcedimientos").live("click", setPA);
    obtenerProcedimientos();
});

//Obtenemos el Catálogo de los procedimientos almacenados
function obtenerProcedimientos() {
    try {
        mmoff("InfPer", "Obteniendo los procedimientos almacenados...", 330);
        $.ajax({
            url: "Default.aspx/ObtenerProcedimientos",   // Current Page, Method
            data: JSON.stringify({}),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 10000,    // AJAX timeout
            success: function(result) {
                //$("#divCatalogoPA div:eq(0)").html(result.d);
                $("#divCatalogoPA div").first().html(result.d);
            },
            error: function(ex, status) {
                try { mostrarErrorAplicacion("Ocurrió un error obteniendo los procedimientos almacenados.", $.parseJSON(ex.responseText).Message); }
                catch (e) { mostrarErrorAplicacion("Ocurrió un error obteniendo los procedimientos almacenados.", e.name + ": " + e.message); }
            },
            complete: function(jXHR, status) {
                //console.log("Completed: " + status);
                actualizarSession();
                mmoff("hide");
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los procedimientos almacenados", e.message);
    }
}

//Obtenemos el Catálogo de parámetros de un procedimiento almacenado
function obtenerParametros() {
    try {
        mmoff("InfPer", "Obteniendo los parámetros...", 250);
        $.ajax({
        url: "Default.aspx/ObtenerParametros",   // Current Page, Method
            data: JSON.stringify({ sPA: $("#tblProcedimientos .FS td:eq(0)").text() }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 10000,    // AJAX timeout
            success: function(result) {
                //$("#divCatalogoPA div:eq(0)").html(result.d);
                $("#divCatalogoParam div").first().html(result.d);
            },
            error: function(ex, status) {
            try { mostrarErrorAplicacion("Ocurrió un error obteniendo los parámetros.", $.parseJSON(ex.responseText).Message); }
            catch (e) { mostrarErrorAplicacion("Ocurrió un error obteniendo los parámetros.", e.name + ": " + e.message); }
            },
            complete: function(jXHR, status) {
                //console.log("Completed: " + status);
                actualizarSession();
                mmoff("hide");
            }
        });
    } catch (e) {
    mostrarErrorAplicacion("Error al obtener los parámetros", e.message);
    }
}

function setPA(e) {
    try {
        var oFila = getFilaFromEvent(e);
        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila del procedimiento", 300);
            return;
        }
        ms_class(oFila);
        //alert($("#tblProcedimientos .FS").attr("id") + " - " + $("#tblProcedimientos .FS td:eq(0)").text());
        obtenerParametros();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los procedimientos almacenados", e.message);
    }
}

function getFilaFromEvent(e) {
    try {
        var oFila = null;
        var oControl = e.srcElement ? e.srcElement : e.target;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }
        return oFila;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la fila desde un evento", e.message);
    }
}

function Ejecutar() {
    try {
        if (typeof ($("#tblProcedimientos .FS").attr("id")) == "undefined") {
            mmoff("War", "Debes seleccionar el procedimiento a ejecutar.", 320);
            return;
        }
        //alert($("#tblProcedimientos .FS").attr("id"));
        var sb = new StringBuilder();
        var sProc = $("#tblProcedimientos .FS td:eq(0)").text();
        sb.Append(sProc + "{sepdatos}");
        $("#tblParametros tr").each(function(index) {
            sb.Append($(this).find("td:eq(1) input").val() + "{sep}");
        });

        //alert(sb.ToString());
        if (sb.ToString().length == 0) {
            mmoff("Err", "No hay datos a ejecutar.");
            return;
        }
        
        mmoff("InfPer", "Ejecutando procedimiento...", 250);
        $.ajax({
            url: "Default.aspx/EjecutarProcedimiento",   // Current Page, Method
            data: JSON.stringify({ sDatos: sb.ToString() }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 300000,    // AJAX timeout
            success: function(result) {
                //mmoff("Suc", "Procedimiento ejecutado correctamente", 250);
                tratarResultado(sProc);
            },
            error: function(ex, status) {
                mmoff("hide");
                try { mostrarErrorAplicacion("Ocurrió un error al ejecutar el procedimiento.", $.parseJSON(ex.responseText).Message); }
                catch (e) { mostrarErrorAplicacion("Ocurrió un error al ejecutar el procedimiento.", e.name + ": " + e.message); }
            },
            complete: function(jXHR, status) {
                //console.log("Completed: " + status);
                actualizarSession();
            }
        });
        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function tratarResultado(sProc) {

    switch (sProc) {
        case "SUP_ADM_ALTAGES_NPROC":
            escribirFicheroAltaGestion();
            break;
        default:
            mmoff("WarPer", "El procedimiento " + sProc + " no está contemplado.", 250);
            break;
    }
}
function escribirFicheroAltaGestion() {
    try {

        var datos = { solicitante: "EDA" };
        $.ajax({
            url: "Default.aspx/EscribirFicheroAltaGestion",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                //alert(result.d);
                mmoff("SucPer", "Procedimiento ejecutado correctamente.\r\n" + result.d, 400);
            },
            error: function (ex, status) {
                //ocultarProcesando()
                //try {alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message);}
                //catch (e) {alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message);}
                mmoff("hide");
                try { mostrarErrorAplicacion("Ocurrió un error al ejecutar el procedimiento.", $.parseJSON(ex.responseText).Message); }
                catch (e) { mostrarErrorAplicacion("Ocurrió un error al ejecutar el procedimiento.", e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}

function escribirFicheroInversiones() {
    try {

        var datos = { solicitante: "EDA" };
        $.ajax({
            url: "Default.aspx/EscribirFicheroAltaGestion",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                //alert(result.d);
                mmoff("SucPer", "Procedimiento ejecutado correctamente.\r\n" + result.d, 400);
            },
            error: function (ex, status) {
                //ocultarProcesando()
                //try {alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message);}
                //catch (e) {alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message);}
                mmoff("hide");
                try { mostrarErrorAplicacion("Ocurrió un error al ejecutar el procedimiento.", $.parseJSON(ex.responseText).Message); }
                catch (e) { mostrarErrorAplicacion("Ocurrió un error al ejecutar el procedimiento.", e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
