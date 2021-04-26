function init(){
    try{       
        $I("procesando").style.top = 120;
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/* El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error" */
function RespuestaCallBack(strResultado, context){
    try{
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            mostrarErrorSQL(aResul[3], aResul[2]);
        }else{
            switch (aResul[0])
            {
                case "setResPantalla":
                    mmoff("Suc", "Resolución configurada.",200, null, null, null, 120);
                    break;
                case "setMensaje":
                    switch ($I("cboMensaje").value){
                        case "0":
                            mmoff("Suc", "Mensaje de bienvenida desactivado.",300, 2000, null, null, 120);
                            break;
                        case "1":
                            mmoff("Suc", "Mensaje de bienvenida activado y configurado a 1 segundo.",400, 2000, null, null, 120);
                            break;
                        default:
                            mmoff("Suc", "Mensaje de bienvenida activado y configurado a " + $I("cboMensaje").value + " segundos.",400, 2000, null, null, 120);
                            break;
                    }
                    break;
                case "setGASVI":
                    mmoff("Suc", "Importación configurada.",200, null, null, null, 120);
                    break;
                case "setPeriodificacion":
                    mmoff("Suc", "Periodificación configurada.",200, null, null, null, 120);
                    break;
                case "setResultadoOnline":
                case "setCorreos":
                case "setBotCalendario":
                case "setMultiVentana": 
                case "setObtenerEstructura":
                case "setMonedaVDP":
                case "setMonedaVDC":
                    mmoff("Suc", "Configuración modificada.", 200, null, null, null, 120);
                    break;
                    
                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                    
            }
            ocultarProcesando();
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la respuesta del callback.", e.message);
    }
    window.focus();
}

function setResultadoOnline(sValue) {
    try {
        //alert(sValue);
        mostrarProcesando();
        var js_args = "setResultadoOnline@#@" + sValue;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la configuración de la obtención de la información.", e.message);
    }
}

function setResPantalla(sPantalla, sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setResPantalla@#@"+ sPantalla +"@#@"+ sValue;
        RealizarCallBack(js_args,"");
        if (sPantalla == "TODAS"){
            var aPantallas = tblResolucion.getElementsByTagName("SELECT");
            for (var i=0; i<aPantallas.length; i++){
                aPantallas[i].value = sValue;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración de resolución de pantalla.", e.message);
    }
}
function setMensaje(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setMensaje@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración del mensaje de bienvenida.", e.message);
    }
}

function setGASVI(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setGASVI@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración de la importación de GASVI.", e.message);
    }
}
function setCorreos(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setCorreos@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración de los correos informativos.", e.message);
    }
}
function setPeriodificacion(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setPeriodificacion@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración de la periodificación de proyectos.", e.message);
    }
}

function setMultiVentana(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setMultiVentana@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración de las múltiples instancias SUPER.", e.message);
    }
}
function setBotCalendario(sValue){
    try{
        mostrarProcesando();
   	    var js_args = "setBotCalendario@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración de acceso a calendario.", e.message);
    }
}

function setObtenerEstructura(sValue) {
    try {
        //alert(sValue);
        mostrarProcesando();
        var js_args = "setObtenerEstructura@#@" + sValue;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la configuración de la obtención de la estructura técnica.", e.message);
    }
}

function setMonedaVDP(sValue) {
    try {
        mostrarProcesando();
        var js_args = "setMonedaVDP@#@" + sValue;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la moneda por defecto para datos de proyecto.", e.message);
    }
}

function setMonedaVDC(sValue) {
    try {
        mostrarProcesando();
        var js_args = "setMonedaVDC@#@" + sValue;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la moneda por defecto para datos consolidados.", e.message);
    }
}
function setPassw() {
    try {
        //mostrarProcesando();  //27/03/2014: a indicación de Víctor.
        var strEnlace = strServer + "Capa_Presentacion/Archivo/Configuracion/Password/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 400))
            .then(function(ret) {
                if (ret != null) {
                    if (ret == "B")
                        mmoff("Inf", "Contraseña eliminada correctamente", 250);
                    else {
                        if (ret == "G")
                            mmoff("Inf", "Contraseña grabada correctamente", 250);
                    }
                }
            });
        window.focus();
        //ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la contraseña", e.message);
    }
}
