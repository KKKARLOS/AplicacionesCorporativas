if (!window.JSON) $.ajax({ type: "GET", url: "../../../Javascript/json-2.4.js", dataType: "script" });
//***Directiva ajax
$.ajaxSetup({ cache: false });
var sTable = "";
var sCargarProvin = false;
var sPaisSel = "";

function init(){
    try {
        sTable = $I("divCatalogo").children[0].innerHTML;
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function asignar() {
    try {
        if ($I("tblDatos") == null) return;
        
        if ($I("cboZona").value == "") {
            mmoff("Inf", "Debes indicar alguna zona.", 230);
            return;
        }

        var aFila = FilasDe("tblDatos");
        var sZona = $I("cboZona").options[$I("cboZona").selectedIndex].text;
        
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                aFila[i].setAttribute("zona", $I("cboZona").value);
                aFila[i].cells[2].innerText = sZona;
                aFila[i].className = "";
                //aFila[i].style.color = "Fuchsia";
                //aFila[i].setAttribute("bd", "U");
                mfa(aFila[i], "U");
                //activarGrabar();
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar la zona.", e.message);
    }
}
function grabar(){
    try {
        if ($I("tblDatos") == null) return;

        var sb = new StringBuilder;
        var aFila = FilasDe("tblDatos");
        
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") == "U") {
                sb.Append(aFila[i].id + "##");      //1 id provincia
                sb.Append(aFila[i].getAttribute("zona") + "##");    //2 id zona
                sb.Append("///");   
            }
        }
        var js_args = sb.ToString();

        mostrarProcesando();

        $.ajax({
            url: "Default.aspx/grabar",
            //            data: JSON.stringify({ sDelete: $I("hdnDelete").value, sInsert: $I("hdnInsert").value, sUpdate: $I("hdnUpdate").value, objeto: o }),
            data: JSON.stringify({ sProvincias: js_args }),
            async: true,
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 10000,    // AJAX timeout. Se comentariza para debugar
            success: function(result) {
                //$("#divresultado").html(result.d.nombre + " " + result.d.apellido + " " + result.d.dni);
                //$("#divresultado").html(result.d);
                var aResul = result.d.split("@#@");
                if (aResul[0] != "OK") {
                    ocultarProcesando();
                    desActivarGrabar();
                    arrayDelete = [];
                    bCambios = false;

                    if (aResul[2] == "547") //error de integridad referencial, no se ha podido eliminar//error de integridad referencial, no se ha podido eliminar
                        mmoff("Err", "No se ha podido realizar el borrado porque existen datos relacionados con el elemento.", 540);
                    else
                        mmoff("Err", aResul[1], 540);
                    return;
                }
                
                var aFila = FilasDe("tblDatos");
                for (var i = 0; i < aFila.length; i++) {
                    if (aFila[i].getAttribute("bd") == "U") {
                        mfa(aFila[i], "N");
                    }
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                //limpiar();
                if (sCargarProvin == true) {
                    sCargarProvin = false;
                    obtenerProvinciasGesPais(sPaisSel);
                }
                ocultarProcesando();
            },
            error: function(ex, status) {
                error$ajax("Ocurrió un error en la aplicación", ex, status)
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}
function limpiar() {
    $I("cboPaisGes").value = "";
    $I("cboAmbito").value = "";
    $I("cboZona").length = 0;
        
    $I("divCatalogo").children[0].innerHTML = sTable;
    $I("divCatalogo").scrollTop = 0;
    actualizarLupas("tblTitulo", "tblDatos");		
}
function obtenerZonasAmbito(sAmbito) {
    try {
        if (sAmbito == "") {
            $I("cboZona").length = 1;
            return;
        }

        mostrarProcesando();

        $.ajax({
            url: "Default.aspx/zonasAmbito",
            //            data: JSON.stringify({ sDelete: $I("hdnDelete").value, sInsert: $I("hdnInsert").value, sUpdate: $I("hdnUpdate").value, objeto: o }),
            data: JSON.stringify({ sID: sAmbito }),
            async: true,
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 10000,    // AJAX timeout. Se comentariza para debugar
            success: function(result) {
                //$("#divresultado").html(result.d.nombre + " " + result.d.apellido + " " + result.d.dni);
                //$("#divresultado").html(result.d);
                var aResul = result.d.split("@#@");
                if (aResul[0] != "OK") {
                    ocultarProcesando();
                    mmoff("Err", aResul[1], 540);
                }

                var aDatos = aResul[1].split("///");
                var j = 1;
                $I("cboZona").length = 0;

                var opcion = new Option("", "");
                $I("cboZona").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboZona").options[j] = opcion;
                    j++;
                }
                ocultarProcesando();
            },
            error: function(ex, status) {
                error$ajax("Ocurrió un error en la aplicación", ex, status)         
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}
function obtenerProvinciasGesPais(sPais) {
    try {
        sPaisSel = sPais;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    sCargarProvin = true;
                    if (!grabar()) return;
                }
                bCambios = false;
                desActivarGrabar();
                LLamadaProvGton(sPais);
            });
        }
        else LLamadaProvGton(sPais);
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}
function LLamadaProvGton(sPais)
{
    try
    {
        sCargarProvin = false;

        if (sPais == "") {
            $I("divCatalogo").children[0].innerHTML = sTable;
            $I("divCatalogo").scrollTop = 0;
            actualizarLupas("tblTitulo", "tblDatos");
            return;
        }

        mostrarProcesando();

        $.ajax({
            url: "Default.aspx/provinciasGtonPais",
            data: JSON.stringify({ sID: sPais }),
            async: true,
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 10000,    // AJAX timeout. Se comentariza para debugar
            success: function (result) {
                //$("#divresultado").html(result.d.nombre + " " + result.d.apellido + " " + result.d.dni);
                //$("#divresultado").html(result.d);
                var aResul = result.d.split("@#@");
                if (aResul[0] != "OK") {
                    ocultarProcesando();
                    mmoff("Err", aResul[1], 540);
                }

                $I("divCatalogo").children[0].innerHTML = aResul[1];
                $I("divCatalogo").scrollTop = 0;
                actualizarLupas("tblTitulo", "tblDatos");
                ocultarProcesando();
            },
            error: function (ex, status) {
                error$ajax("Ocurrió un error en la aplicación", ex, status)
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaProvGton.", e.message);
    }
}

function error$ajax(msg, ex, status) {
    ocultarProcesando();
    if (status == "timeout") {
        mmoff("Err", "Se ha sobrepasado el tiempo límite de espera para procesar la petición en servidor.\n\nVuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU.\n\nDisculpa las molestias.",400);
        return;
    }
    bCambios = false;
    var desc = "";
    var reg = /\\n/g;
    if (ex.responseText != "undefined") {
        desc = Utilidades.unescape($.parseJSON(ex.responseText).Message);
        desc = desc.replace(reg, "\n");
    }
    mostrarError(msg + "\n\n" + desc);
}