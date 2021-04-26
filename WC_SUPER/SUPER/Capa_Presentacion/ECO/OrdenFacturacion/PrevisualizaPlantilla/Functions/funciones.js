function init(){
    try{
        //if ($I("hdnErrores").value !=""){
        //    $I("imgcol1").style.visibility = "hidden";
        //    $I("imgcol2").style.visibility = "hidden";
        //    $I("imgcol3").style.visibility = "hidden";
        //    $I("imgcol4").style.visibility = "hidden";
        //    $I("imgcol5").style.visibility = "hidden";
        //    $I("btnSalir").style.visibility = "hidden";
        //}
        if (!mostrarErrores()){
            ocultarProcesando();
            cerrarVentana();
            return;
        }
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

    function cerrarVentana(){
	    try{
            if (bProcesando()) return;

            var returnValue = null;
            modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
