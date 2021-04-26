function init(){
    try{
        if ($I("hdnIdConsulta").value != "")
            $I("txtDescripcion").value = Utilidades.unescape(opener.$I("tblConsultas").rows[opener.iFila].getAttribute("titulo"));
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    try {
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK") {
            mostrarErrorSQL(aResul[3], aResul[2]);
        } else {
            switch (aResul[0]) {
                case "grabar":
                    mmoff("Suc","Datos modificados.", 200);
                    setTimeout("aceptar()", 20);
                    break;
                default:
                    ocultarProcesando();
                    alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");

            }
            ocultarProcesando();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la respuesta del callback.", e.message);
    }
    window.focus();
}

function comprobarDatos(){
    if($I("txtDenominacion").value == ""){
        mmoff("War","Debe introducir la denominación de la consulta.", 350);
        return false;
    }    
    return true;
}

function grabar() {
    try {
        mostrarProcesando();
        var js_args = "grabar@#@";
        js_args += $I("hdnIdConsulta").value + "@#@";
        js_args += Utilidades.escape($I("txtDenominacion").value) + "@#@";
        js_args += getRadioButtonSelectedValue("rdbEstado", true) + "@#@";
        js_args += Utilidades.escape($I("txtDescripcion").value);
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos modificados.", e.message);
    } 
}

function aceptar(){
    try{
        if(!comprobarDatos()) return;
        var valorRetorno = $I("hdnIdConsulta").value + "@#@";
        valorRetorno += Utilidades.escape($I("txtDenominacion").value) + "@#@";
        valorRetorno += getRadioButtonSelectedValue("rdbEstado", true) + "@#@";
        valorRetorno += Utilidades.escape($I("txtDescripcion").value);
        
//        window.returnValue = valorRetorno;
//        window.close();
        var returnValue = valorRetorno;
        modalDialog.Close(window, returnValue);
                
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;
        
//        window.returnValue = null;
//        window.close();
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}





