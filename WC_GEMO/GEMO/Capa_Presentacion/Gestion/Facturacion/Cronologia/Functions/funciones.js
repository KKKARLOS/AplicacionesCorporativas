    function init(){
        try{
            if (!mostrarErrores()) return;
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function cerrarVentana(){
	    try{
            if (bProcesando()) return;
            
//	        window.returnValue = null;
//	        window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);		
	        
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
