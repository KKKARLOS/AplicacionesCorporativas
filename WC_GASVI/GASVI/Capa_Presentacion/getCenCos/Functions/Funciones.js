var aFila;
function init(){
    try{
        if (!mostrarErrores()) return;
        aFila = FilasDe("tblCatCenCos");
        if (aFila != null) {
            iFila = aFila.length - 1;
            actualizarLupas("tblCabecera", "tblCatCenCos");
        }
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

function ac(oControl){
    try{
        var oFila;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }
//        mostrarProcesando();
//        window.returnValue = oFila.id + "@#@" + oFila.cells[1].innerText;
//        window.close();
        var returnValue = oFila.id + "@#@" + oFila.cells[1].innerText;
        modalDialog.Close(window, returnValue);        
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}




