
function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
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
	    //location.href=location.href;  ??? para qu� ?
	}catch(e){
		mostrarErrorAplicacion("Error en la funci�n Detalle", e.message);
    }
}

