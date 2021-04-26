function init(){
    try{
        if ($I("hdnIdBono").value != "")
            $I("txtDescripcion").value = Utilidades.unescape(opener.$I("tblBonos").rows[opener.iFila].getAttribute("titulo"));
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
    }
}

function comprobarDatos(){
    if($I("txtDenominacion").value == ""){
        mmoff("War", "Debe introducir la denominación del bono.", 350);
        return false;
    }    
    return true;        
}

function aceptar(){
    try{
        if(!comprobarDatos()) return;
        var valorRetorno = $I("hdnIdBono").value + "@#@" + $I("txtDenominacion").value + "@#@";
        valorRetorno += getRadioButtonSelectedValue("rdbEstado", true) + "@#@" + $I("txtDescripcion").value + "@#@" + $I("cboMoneda").value;
//        window.returnValue = valorRetorno;
        //        window.close();
        var returnValue = strRetorno;
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
        mostrarErrorAplicacion("Error al cerrar la ventana.", e.message);
    }
}





