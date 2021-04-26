function init(){
    try{       
        $I("procesando").style.top = 120;
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

/* El resultado se env�a en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." � "ERROR@#@Descripci�n del error" */
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
                    mmoff("Suc", "Resoluci�n configurada.",200, null, null, null, 120);
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
                    mmoff("Suc", "Importaci�n configurada.",200, null, null, null, 120);
                    break;
                case "setPeriodificacion":
                    mmoff("Suc", "Periodificaci�n configurada.",200, null, null, null, 120);
                    break;
                case "setResultadoOnline":
                case "setCorreos":
                case "setBotCalendario":
                case "setMultiVentana": 
                case "setObtenerEstructura":
                case "setMonedaVDP":
                case "setMonedaVDC":
                    mmoff("Suc", "Configuraci�n modificada.", 200, null, null, null, 120);
                    break;
                    
                default:
                    ocultarProcesando();
                    mmoff("Err", "Opci�n de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                    
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
        mostrarErrorAplicacion("Error al seleccionar la configuraci�n de la obtenci�n de la informaci�n.", e.message);
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
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n de resoluci�n de pantalla.", e.message);
    }
}
function setMensaje(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setMensaje@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n del mensaje de bienvenida.", e.message);
    }
}

function setGASVI(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setGASVI@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n de la importaci�n de GASVI.", e.message);
    }
}
function setCorreos(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setCorreos@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n de los correos informativos.", e.message);
    }
}
function setPeriodificacion(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setPeriodificacion@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n de la periodificaci�n de proyectos.", e.message);
    }
}

function setMultiVentana(sValue){
    try{
        //alert(sValue);
        mostrarProcesando();
   	    var js_args = "setMultiVentana@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n de las m�ltiples instancias SUPER.", e.message);
    }
}
function setBotCalendario(sValue){
    try{
        mostrarProcesando();
   	    var js_args = "setBotCalendario@#@"+ sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n de acceso a calendario.", e.message);
    }
}

function setObtenerEstructura(sValue) {
    try {
        //alert(sValue);
        mostrarProcesando();
        var js_args = "setObtenerEstructura@#@" + sValue;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la configuraci�n de la obtenci�n de la estructura t�cnica.", e.message);
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
        //mostrarProcesando();  //27/03/2014: a indicaci�n de V�ctor.
        var strEnlace = strServer + "Capa_Presentacion/Archivo/Configuracion/Password/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 400))
            .then(function(ret) {
                if (ret != null) {
                    if (ret == "B")
                        mmoff("Inf", "Contrase�a eliminada correctamente", 250);
                    else {
                        if (ret == "G")
                            mmoff("Inf", "Contrase�a grabada correctamente", 250);
                    }
                }
            });
        window.focus();
        //ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la contrase�a", e.message);
    }
}
