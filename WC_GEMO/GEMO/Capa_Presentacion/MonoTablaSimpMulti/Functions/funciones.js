    function init(){
        try{
            if (!mostrarErrores()) return;
            actualizarLupas("tblTitulo", "tblDatos");
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
    function aceptar(){
        try{
            if (bProcesando()) return;
            var tblDatos = $I("tblDatos");
            strOpciones = "";
            if (iFila == -1){
                alert("Debe seleccionar alguna fila");
                return;
            }
            for (var i=0; i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].className == "FS"){          
                    if (tblDatos.rows[i].innerText=='') continue;          
                    strOpciones += tblDatos.rows[i].id + "@#@" + tblDatos.rows[i].innerText  + "///";
                }
            }
            strOpciones = strOpciones.substring(0,strOpciones.length-3);
    		
            //window.returnValue = strOpciones;
            //window.close();

            var returnValue = strOpciones;
            modalDialog.Close(window, returnValue);
            
        }catch(e){
            mostrarErrorAplicacion("Error al aceptar", e.message);
        }
    }
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;
            var tblDatos = $I("tblDatos");
            var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText + "///";
            modalDialog.Close(window, returnValue);
            
	        //window.returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText + "///";
	        //window.close();
        }catch(e){
            mostrarErrorAplicacion("Error seleccionar la fila", e.message);
        }
    }
	
    function cerrarVentana(){
	    try{
	        if (bProcesando()) return;
            
            var returnValue = null;
            modalDialog.Close(window, returnValue);
            
//	        window.returnValue = null;
//	        window.close();
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
