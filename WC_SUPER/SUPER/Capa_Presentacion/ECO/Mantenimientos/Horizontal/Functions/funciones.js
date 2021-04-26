
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
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Horizontal/Detalle/Default.aspx?ID=" + oFila.id + "&ORIGEN=MantFiguras";
  	    //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 545));
  	    modalDialog.Show(strEnlace, self, sSize(1010, 545))
	        .then(function(ret) {
  	            ocultarProcesando();
	        }); 
	    //location.href=location.href;  ??? para qué ?
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}

