function init(){
    try{
        if (!mostrarErrores()) return;
        window.focus();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function setAno(sControl, sOpcion){
    try{
        var nDesde, nHasta;
        if (sControl == "D"){
            if (sOpcion == "A"){
                $I("txtDesde").value = parseInt($I("txtDesde").value, 10) - 1;
            }else{
                $I("txtDesde").value = parseInt($I("txtDesde").value, 10) + 1;
            }
            nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
            nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
            if (nDesde > nHasta){
                $I("cboHasta").value = parseInt(nDesde.toString().substring(4, 6), 10);
                $I("txtHasta").value = nDesde.toString().substring(0, 4);
	        }
        }else{
            if (sOpcion == "A"){
                $I("txtHasta").value = parseInt($I("txtHasta").value, 10) - 1;
            }else{
                $I("txtHasta").value = parseInt($I("txtHasta").value, 10) + 1;
            }
            nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
            nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
            if (nDesde > nHasta){
                $I("cboDesde").value = parseInt(nHasta.toString().substring(4, 6), 10);
                $I("txtDesde").value = nHasta.toString().substring(0, 4);
	        }
        }
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el año", e.message);
    }
}
function setMes(sControl){
    try{
        var nDesde, nHasta;
        nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
        nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
        if (sControl == "D"){
            if (nDesde > nHasta){
                $I("cboHasta").value = $I("cboDesde").value;
	        }
        }else{
            if (nDesde > nHasta){
                $I("cboDesde").value = $I("cboHasta").value;
	        }
        }
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el mes", e.message);
    }
}

function aceptar(){
    try{
        if (parseInt($I("txtDesde").value, 10) < 1990 || parseInt($I("txtHasta").value, 10) > 2078){
            alert("El rango temporal no puede debe estar comprendido entre los años 1990 y 2078.");
            return;
        }
        var nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
        var nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
        //alert("nDesde: "+ nDesde +"\nnHasta: "+ nHasta);

//        window.returnValue = nDesde + "@#@" + nHasta;
//        window.close();
        var returnValue = nDesde + "@#@" + nHasta;
        modalDialog.Close(window, returnValue);           
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana(){
    try{
        //        window.returnValue = null;
        //        window.close();
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
