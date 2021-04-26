var bLectura=false;

function init(){
    try {
        //window.focus();
        if (bGestor) {
            $I("txtFLR").style.cursor = "pointer";
            $I("txtFLR").onclick = function() { mc(this); };
            $I("lblFLR").style.visibility = "visible";
            $I("txtFLR").style.visibility = "visible";
        }
        
        $I("txtMensaje").focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);	
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]){
            case "addDialogo":
                var returnValue = "OK";
                modalDialog.Close(window, returnValue);		
                return;
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getMes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getUnMes.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sSize(270, 215));
        modalDialog.Show(strEnlace, self, sSize(270, 215))
	        .then(function(ret) {
                if (ret != null) {
                    $I("hdnMes").value = ret;
                    $I("txtMes").value = AnoMesToMesAnoDescLong(ret);
                    $I("imgGoma").style.visibility = "visible";
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el mes.", e.message);
    }
}

function delMes() {
    try {
        $I("hdnMes").value = "";
        $I("txtMes").value = "";
        $I("imgGoma").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el mes.", e.message);
    }
}

function setFLR() {
    try {
        $I("imgGomaFLR").style.visibility = ($I("txtFLR").value == "") ? "hidden" : "visible";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la fecha límite de respuesta.", e.message);
    }
}

function delFLR() {
    try {
        $I("txtFLR").value = "";
        $I("imgGomaFLR").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el mes.", e.message);
    }
}


function comprobarDatos() {
    try {
        if ($I("cboAsunto").value == "") {
            ocultarProcesando();
            mmoff("War", "El asunto es dato obligatorio", 250);
            return false;
        }
        if ($I("txtMensaje").value.Trim() == "") {
            ocultarProcesando();
            mmoff("War", "El mensaje es dato obligatorio", 250);
            return false;
        }
        if ($I("txtFLR").value != "" && cadenaAfecha(sToday).getTime() > cadenaAfecha($I("txtFLR").value).getTime()) {
            ocultarProcesando();
            mmoff("War", "La fecha límite de respuesta no puede ser anterior a hoy.", 380);
            $I("txtFLR").value = $I("txtFLR").getAttribute("oValue");
            return false;
        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function tramitar(){
    try {
        mostrarProcesando();

        if (!comprobarDatos()) return;

        var js_args = "addDialogo@#@";
        js_args += $I("cboAsunto").value + "@#@";
        js_args += $I("hdnMes").value + "@#@";
        js_args += Utilidades.escape($I("txtMensaje").value) + "@#@";
        js_args += idPSN + "@#@";
        js_args += ((bInterlocutor) ? "1" : "0") + "@#@";
        js_args += $I("txtFLR").value;
        
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}