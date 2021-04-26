
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
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Clientes/Detalle/Default.aspx?ID=" + codpar(oFila.id) +
	            "&ORIGEN=" + codpar("MantFiguras");
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 545));
        modalDialog.Show(strEnlace, self, sSize(1010, 545))
	        .then(function(ret) {
                ocultarProcesando();
	        }); 
	    //location.href=location.href;  ??? para qu� ?
	}catch(e){
		mostrarErrorAplicacion("Error en la funci�n Detalle", e.message);
    }
}

