
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
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Contratos/Detalle/Default.aspx?ID=" + codpar(oFila.id);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 545));
	    modalDialog.Show(strEnlace, self, sSize(1010, 545))
	        .then(function(ret) {
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}

