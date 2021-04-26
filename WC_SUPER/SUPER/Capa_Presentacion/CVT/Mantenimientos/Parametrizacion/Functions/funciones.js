if (!window.JSON) $.ajax({ type: "GET", url: "../../../../Javascript/json-2.4.js", dataType: "script" });
//***Directiva ajax
$.ajaxSetup({ cache: false });

function init() {
    try {
        $I("procesando").style.top = "420px";
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function grabar() {
    try {
        if (!comprobarDatos()) return;
        mostrarProcesando();
        var js_args = "";
        js_args += $I("txt_fproceso_act_masi").value + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_act_masi").value)) + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_envi_validar").value)) + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_validar_reg").value)) + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_cualifi_proy").value)) + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_alta_exp").value)) + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_peticion_bor").value)) + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_tar_ven_noven").value)) + "#sCad#";
        js_args += parseInt(dfn($I("txt_ndias_tar_ven_mieq").value)) + "#sCad#";
        js_args += $I("txt_fultenvio_tar_ven_noven").value + "#sCad#";
        js_args += $I("txt_fultenvio_tar_ven_mieq").value + "#sCad#";

        mostrarProcesando();

        $.ajax({
            url: "Default.aspx/grabar",
            //            data: JSON.stringify({ sDelete: $I("hdnDelete").value, sInsert: $I("hdnInsert").value, sUpdate: $I("hdnUpdate").value, objeto: o }),
            data: JSON.stringify({ sParametrizacion: js_args }),
            async: true,
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            //timeout: 10000,    // AJAX timeout. Se comentariza para debugar
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

                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                //limpiar();
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

function comprobarDatos() {
    var dFecha = new Date();
    if (($I("txt_fproceso_act_masi").value != "") && (cadenaAfecha($I("txt_fproceso_act_masi").value) < dFecha)) 
    {
        mmoff("War", "La fecha de actualización masiva no puede ser inferior a la fecha actual.", 420);
        $I("txt_fproceso_act_masi").focus();
        return false;
    }
    if ($I("txt_fultenvio_tar_ven_noven").value != "") 
    {
        if (cadenaAfecha($I("txt_fultenvio_tar_ven_noven").value).add("d", parseInt(dfn($I("txt_ndias_tar_ven_noven").value))) <= dFecha) {
            mmoff("War", "La fecha de último envío de las tareas vencidas y no vencidas más el número de días no puede ser inferior o igual a la fecha actual.", 380);
            $I("txt_fultenvio_tar_ven_noven").focus();
            return false;
        }
    }
    if ($I("txt_fultenvio_tar_ven_mieq").value != "") 
    {
        if (cadenaAfecha($I("txt_fultenvio_tar_ven_mieq").value).add("d", parseInt(dfn($I("txt_ndias_tar_ven_mieq").value))) <= dFecha) {
            mmoff("War", "La fecha de último envío de las tareas vencidas de mi equipo más el número de días no puede ser inferior o igual a la fecha actual.", 380);
            $I("txt_fultenvio_tar_ven_mieq").focus();
            return false;
        }
    }        
    if (parseInt(dfn($I("txt_ndias_act_masi").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_act_masi").focus();
        return false;
    }
    if (parseInt(dfn($I("txt_ndias_envi_validar").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_envi_validar").focus();
        return false;
    }
    if (parseInt(dfn($I("txt_ndias_validar_reg").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_validar_reg").focus();
        return false;
    }
    if (parseInt(dfn($I("txt_ndias_cualifi_proy").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_cualifi_proy").focus();
        return false;
    }
    if (parseInt(dfn($I("txt_ndias_alta_exp").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_alta_exp").focus();
        return false;
    }
    if (parseInt(dfn($I("txt_ndias_peticion_bor").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_peticion_bor").focus();
        return false;
    }
    if (parseInt(dfn($I("txt_ndias_tar_ven_noven").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_tar_ven_noven").focus();
        return false;
    }
    if (parseInt(dfn($I("txt_ndias_tar_ven_mieq").value)) < 0) {
        mmoff("War", "El valor del campo no puede ser negativo.", 300);
        $I("txt_ndias_tar_ven_mieq").focus();
        return false;
    }
                
    return true;
}
function aG() {
    try {
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
    }
}

function getCronologia() {
    try {
        var strEnlace = strServer + "Capa_Presentacion/CVT/Mantenimientos/Cronologia/default.aspx";
        modalDialog.Show(strEnlace, self, sSize(300, 340));
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de cronologías de actualizaciones masivas.", e.message);
    }
}