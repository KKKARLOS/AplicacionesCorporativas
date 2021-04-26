var aFila;
function init(){
    try{
        aFila = FilasDe("tblCatNodo");
        iFila = aFila.length - 1;
        actualizarLupas("tblCabecera", "tblCatNodo");
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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

function aceptarClick(oControl){
    try{
        var oFila;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }
//        window.returnValue = oFila.id + "@#@" + oFila.cells[0].innerText;
//        window.close();
        var returnValue = oFila.id + "@#@" + oFila.cells[0].innerText;
        modalDialog.Close(window, returnValue);        
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}




