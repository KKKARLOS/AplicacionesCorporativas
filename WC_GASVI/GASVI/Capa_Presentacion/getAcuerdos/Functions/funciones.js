function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("tblCatAcuerdos") != null){
            actualizarLupas("tblTitulo", "tblCatAcuerdos");
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(oControl){
    try{
        if (bProcesando()) return;
        
        var oFila;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }        
        
//        window.returnValue = oFila.id + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.getAttribute("idMoneda");
//        window.close();
        var returnValue = oFila.id + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.getAttribute("idMoneda");
        modalDialog.Close(window, returnValue);        
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir(){
    //        window.returnValue = null;
    //        window.close();
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}
