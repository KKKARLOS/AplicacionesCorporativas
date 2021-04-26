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

//        window.returnValue = oFila.id + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.cells[3].innerText + "@#@" //idBono + Denominacion bono + Importe bono
//                            + oFila.getAttribute("idProyecto") + "@#@"
//                            + oFila.getAttribute("idUsuario") + "@#@"
//                            + oFila.getAttribute("idMoneda") + "@#@"
//                            + oFila.getAttribute("desMoneda") + "@#@"
//                            + oFila.cells[4].children[0].innerText;
//        window.close();
        var returnValue = oFila.id + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.cells[3].innerText + "@#@" //idBono + Denominacion bono + Importe bono
                            + oFila.getAttribute("idProyecto") + "@#@"
                            + oFila.getAttribute("idUsuario") + "@#@"
                            + oFila.getAttribute("idMoneda") + "@#@"
                            + oFila.getAttribute("desMoneda") + "@#@"
                            + oFila.cells[4].children[0].innerText;
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
