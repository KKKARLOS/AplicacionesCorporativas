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

        var sRetorno = "";
        sRetorno = oFila.id + "@#@" + oFila.cells[1].innerText + " - " + oFila.cells[2].innerText + "@#@";
        sRetorno += (typeof (oFila.getAttribute("responsable")) != "undefined") ? oFila.getAttribute("responsable") + "@#@" : "@#@";
        sRetorno += (typeof (oFila.getAttribute("sexo_aprobador")) != "undefined") ? oFila.getAttribute("sexo_aprobador") + "@#@" : "@#@";
        sRetorno += (typeof (oFila.getAttribute("aprobador")) != "undefined") ? oFila.getAttribute("aprobador") + "@#@" : "@#@";
        sRetorno += oFila.getAttribute("nodo") + "@#@";
        sRetorno += oFila.getAttribute("cliente");
//        window.returnValue = sRetorno;
//        window.close();

        var returnValue = sRetorno;
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
