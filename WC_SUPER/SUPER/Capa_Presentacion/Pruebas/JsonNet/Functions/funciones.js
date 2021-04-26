var pfj = "ctl00_CPHC_";
$(document).ready(function () {

    $("#" + pfj + "btnWriteFile").bind("click", PruebaEscribirFichero);
    $("#" + pfj + "btnTestConfirm").bind("click", PruebaConfirmAnidado);
    $("#" + pfj + "btnTestAj").bind("click", PruebaJson);
    $("#" + pfj + "btnTestIberdok").bind("click", PruebaIberdok);
    $("#" + pfj + "btnTestIberdok2").bind("click", PruebaIberdok2);
    $("#" + pfj + "btnCarpetaIberdok").bind("click", CarpetaIberdok);
    $("#" + pfj + "btnQTCNDrupal").bind("click", PruebaQTCNDrupal);
    $("#" + pfj + "btnQTCNDrupalGET").bind("click", PruebaQTCNDrupalGET);
    $("#" + pfj + "btnQTCNDrupalGET2").bind("click", GetPaisExpl);
    $("#" + pfj + "btnQTCNDrupalPOST").bind("click", PruebaQTCNDrupalPOST);
    $("#" + pfj + "btnLlamarModal").bind("click", PruebaModal);
    $("#" + pfj + "btnSAP").bind("click", PruebaSAP);
    $("#" + pfj + "btnSAP2").bind("click", PruebaSAP2);
});

function PruebaEscribirFichero(e) {
    try {

        e.preventDefault();

        var datos = { solicitante: "EDA" };
        $.ajax({
            url: "Default.aspx/EscribirFichero",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try {
                    alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message);
                }
                catch (e) {
                    alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message);
                }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}

function PruebaJson(e) {
    try {
        //if (!e) e = event;

        e.preventDefault();
        //alert("Botón");
        //mostrarProcesando();

        var datos = { annomes: 201501, aCentros: [8, 25, 33], aEvaluadores: [1568, 1321], aPSN: [{ id: 1, des: "des1", resp: 1568 }, { id: 2, des: "des2", resp: 1321 }] };
        datos.aTareas = [{ idT: 1, desT: "Tarea 1" }, { idT: 2, desT: "Tarea 2" }, { idT: 3, desT: "Tarea 3" }];
        //alert(datos.annomes + "\n" + datos.sCentros[0] + "\n" + datos.sEvaluadores[1] + "\n" + datos.sPSN.length);
        //var sDatos = JSON.stringify({ x: datos }, null, 4);
        alert(JSON.stringify(datos, null, 4));
        $.ajax({
            url: "Default.aspx/Test",   // Current Page, Method
            //data: JSON.stringify(datos),  // parameter map as JSON
            data: JSON.stringify(datos),  // parameter map as JSON
            //data: JSON.stringify({ json: datos }),  // parameter map as JSON
            //data: { json: sDatos },  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 10000,    // AJAX timeout
            success: function (result) {
                //$("#divCatalogo div").first().html(result.d);
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                //console.log("Completed: " + status);
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function PruebaIberdok(e) {
    try {

        e.preventDefault();

        var datos = { solicitante: "IBERDOK", password: "94659DD2-FC18-4D51-8423-B43EDB960D2E", pedido: "BE364AE0-B4A7-4F4D-ABB4-BDA8FAD01929", code: 0 };
        //var sDatos = JSON.stringify({ x: datos }, null, 4);
        //alert(JSON.stringify(datos, null, 4));
        $.ajax({
            url: "Default.aspx/TestIberdok",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                //$("#divCatalogo div").first().html(result.d);
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function PruebaIberdok2(e) {
    try {

        e.preventDefault();

        var datos = { solicitante: "IBERDOK", password: "94659DD2-FC18-4D51-8423-B43EDB960D2E", pedido: "BE364AE0-B4A7-4F4D-ABB4-BDA8FAD01929", code: 0 };
        //var sDatos = JSON.stringify({ x: datos }, null, 4);
        //alert(JSON.stringify(datos, null, 4));
        $.ajax({
            url: "Default.aspx/TestIberdok2",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                //$("#divCatalogo div").first().html(result.d);
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function CarpetaIberdok(e) {
    try {

        e.preventDefault();

        var datos = { dominio: "IBERMATICA", usuario: "clienteDIS", password: "clienteDIS", pedido: "BE364AE0-B4A7-4F4D-ABB4-BDA8FAD01929" };
        $.ajax({
            url: "Default.aspx/CarpetaIberdok",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function PruebaQTCNDrupal(e) {
    try {

        e.preventDefault();

        var datos = { solicitante: "EDA", password: "EA348F14-90B6-42e2-9739-6925C9378B33", t513_idlinea: "8770" };
        $.ajax({
            url: "Default.aspx/TestQTCNDrupal",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                //$("#divCatalogo div").first().html(result.d);
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try {
                    alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message);
                }
                catch (e) {
                    alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message);
                }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function PruebaQTCNDrupalGET(e) {
    try {

        e.preventDefault();

        var datos = { solicitante: "EDA", password: "EA348F14-90B6-42e2-9739-6925C9378B33", t513_idlinea: "8770" };
        $.ajax({
            url: "Default.aspx/TestQTCNDrupalGET",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try {
                    alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message);
                }
                catch (e) {
                    alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message);
                }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function PruebaQTCNDrupalPOST(e) {
    try {

        e.preventDefault();

        var datos = { solicitante: "EDA", password: "EA348F14-90B6-42e2-9739-6925C9378B33", t513_idlinea: "8770" };
        $.ajax({
            url: "Default.aspx/TestQTCNDrupalPOST",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try {
                    alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message);
                }
                catch (e) {
                    alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message);
                }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}

function PruebaModal() {
    var strEnlaceNodo = strServer + "Capa_Presentacion/Administracion/DetalleNodo/Default.aspx?SN4=" + codpar('1');
    strEnlaceNodo += "&SN3=" + codpar('13');
    strEnlaceNodo += "&SN2=" + codpar('9');
    strEnlaceNodo += "&SN1=" + codpar('43');
    strEnlaceNodo += "&ID=" + codpar('65');
    modalDialog.Show(strEnlaceNodo, self, sSize(990, 605)).then(function (ret) {
        if (ret != null) {
            alert(ret);
        }
    });
}
//Explotación
function GetPaisExpl(e) {
    try {

        e.preventDefault();

        var datos = { solicitante: "EDA", password: "EA348F14-90B6-42e2-9739-6925C9378B33" };
        $.ajax({
            url: "Default.aspx/GetPaisExpl",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "GET", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try {
                    alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message);
                }
                catch (e) {
                    alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message);
                }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}

function PruebaConfirmAnidado() {
    jqConfirm("", "Primer confirm. ¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
        if (answer)
            alert("Has pulsado Aceptar");
        else
            alert("Has pulsado Cancelar");
    });
}


function PruebaSAP(e) {
    try {

        e.preventDefault();

        var datos = { I_FECHA: "2017-05-30" };
        $.ajax({
            url: "Default.aspx/TestSAP",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                //$("#divCatalogo div").first().html(result.d);
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}
function PruebaSAP2(e) {
    try {

        e.preventDefault();

        var datos = { I_FECHA: "2017-06-14" };
        //var datos = { I_FECHA: "2005-12-31" };
        $.ajax({
            url: "Default.aspx/TestSAP2",   // Current Page, Method
            data: JSON.stringify(datos),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 1000000,    // AJAX timeout
            success: function (result) {
                //$("#divCatalogo div").first().html(result.d);
                alert(result.d);
            },
            error: function (ex, status) {
                ocultarProcesando()
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}




