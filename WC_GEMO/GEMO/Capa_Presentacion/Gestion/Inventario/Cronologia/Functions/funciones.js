    function init(){
        try{
            if (!mostrarErrores()) return;
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
        }
    }
	
    function cerrarVentana(){
	    try{
            if (bProcesando()) return;
            var returnValue = null;
            modalDialog.Close(window, returnValue);	               
//	        window.returnValue = null;
//	        window.close();
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
