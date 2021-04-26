
function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function Detalle(oFila)
{
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Formulas/getFormula.aspx?id=" + oFila.id + "&nombre=" + codpar(oFila.cells[1].innerText);        
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(780, 600));
        
        ocultarProcesando();
	    //location.href=location.href;  ??? para qué ?
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}

