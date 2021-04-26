function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("tblDatos") != null){
            actualizarLupas("tblTitulo", "tblDatos");
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
        
        
        var returnValue = oFila.id +"@#@"+ oFila.cells[1].innerText +" - "+ oFila.cells[2].innerText +"@#@"
                            + oFila.getAttribute("responsable") +"@#@"
                            + oFila.getAttribute("sexo_aprobador") +"@#@"
                            + oFila.getAttribute("aprobador") +"@#@"
                            + oFila.getAttribute("nodo") +"@#@"
                            + oFila.getAttribute("cliente");
        modalDialog.Close(window, returnValue);
        
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}
