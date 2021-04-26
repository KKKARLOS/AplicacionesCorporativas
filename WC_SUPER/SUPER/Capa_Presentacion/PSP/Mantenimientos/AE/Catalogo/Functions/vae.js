// JScript File
//var aVAES = new Array();
var aVAES;
var oVAEActivo;

function objVAE(bd, idAE, idVAE, nombre, estado, orden){
	this.bd	= bd;
	this.idAE	= idAE;
	this.idVAE	= idVAE;
	this.nombre	= nombre;
	this.estado	= estado;
	this.orden	= orden;
}
function insertarVAEEnArray(bd, idAE, idVAE, nombre, estado, orden){
    try{
        oVAE = new objVAE(bd, idAE, idVAE, nombre, estado, orden);
        aVAES[aVAES.length]= oVAE;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un VAE en el array.", e.message);
    }
}
function buscarVAEEnArray(idVAE){
    try{
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].idVAE == idVAE){
                oVAEActivo = aVAES[nIndice];
                return aVAES[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un VAE en el array.", e.message);
    }
}
function buscarIndiceVAEEnArray(idVAE){
    try{
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].idVAE == idVAE)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un VAE en el array.", e.message);
    }
}
function borrarVAEDeArray(idVAE){
    try{
        aVAES.splice(buscarIndiceVAEEnArray(idVAE), 1);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un VAE del array.", e.message);
    }
}
function borrarAEDeArray(idAE){
    try{
        //for (var nIndice=0; nIndice < aVAES.length; nIndice++){
        for (var nIndice=aVAES.length -1; nIndice >=0 ; nIndice--){
            if (aVAES[nIndice].idAE == idAE)
                aVAES.splice(buscarIndiceVAEEnArray(aVAES[nIndice].idVAE), 1);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un VAE del array.", e.message);
    }
}
function mostrarDatosVAE(idVAE){
    try{
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].idVAE == idVAE){
                var objVAE = aVAES[nIndice];
                var strMsg = "bd: "+ objVAE.bd +"\nidAE: "+ objVAE.idAE +"\nidVAE: "+ objVAE.idVAE +"\nnombre: "+ objVAE.nombre +"\n";
                strMsg += " estado: "+ objVAE.estado+"\norden: "+ objVAE.orden +"\n";
                mmoff("Inf", strMsg, 400);
                return;
             }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de un VAE del array.", e.message);
    }
}
function oVAEActualizar(accion, clave, obj){
    try{
        //alert(accion+ "  "+ clave+ "  "+obj.outerHTML);
        if (oVAEActivo == null) return;
        
        if (oVAEActivo.bd != "I") oVAEActivo.bd = accion;
        
        //alert(obj.type);
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oVAEActivo."+ clave +" = 1");
            else eval("oVAEActivo."+ clave +" = 0");
        }else{
            //alert("oVAEActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            if (clave == "nombre")
                eval("oVAEActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            else{
                if (obj.value != null)
                    eval("oVAEActivo."+ clave +" = '"+ obj.value +"';");
                else
                    eval("oVAEActivo."+ clave +" = '"+ obj +"';");
            }
        }
        //Actualizo la fila del VAE a 'U' (updatear) si estaba '' (sin modificar)
//        var aFila = FilasDe("tblDatosVAE");
//        for (var i=aFila.length-1; i>=0; i--){
//            if (aFila[i].id == oVAEActivo.idVAE){
//                if (aFila[i].bd == ""){
//                    aFila[i].bd="U";
//                }
//                break;
//            }
//        }
//        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la bd del VAE activo.", e.message);
    }
}
//Copia local de funcion global en draganddrop.js porque hay que actualizar el orden en el array que soporta los VAE´s
function startDragIMGvae(e) 
{ 
    //if (bLectura) return;   
    if (!e) e = event; 
    var row = e.srcElement ? e.srcElement : e.target;
    if (row.nodeName != 'IMG') return;
    if (row.src.indexOf("imgMoveRow") == -1) return;
    while (row && row.nodeName != 'TR') row = row.parentNode; 
    if (!row) return false;             
    
    var tbody = this; 
    tbody.activeRow = row; 
    nFilaDesde = row.rowIndex;
    tbody.onmousemove = doDrag; 
    document.onmouseup = function () 
    { 
        //document.body.style.cursor = "default";
        tbody.activeRow.style.backgroundColor="";
        tbody.onmousemove = null; 
        nFilaHasta = tbody.activeRow.rowIndex;
        
        var aFilasVAE=FilasDe("tblDatosVAE");
        var nIndVAE;
        for (var i=0; i<aFilasVAE.length; i++){
            nIndVAE=buscarIndiceVAEEnArray(aFilasVAE[i].id);
            if (aVAES[nIndVAE].bd != 'D'){
                if (aVAES[nIndVAE].orden != i){
                    aVAES[nIndVAE].bd = 'U';
                    aVAES[nIndVAE].orden = i;
                }
            }
        }
        
        document.onmouseup = null; 
        //fm(row);
        try{
            tbody.activeRow.className = "FS"
        }catch(e){}
        tbody.activeRow = null; 
        try{
            if ($I("ctl00_CPHB_Botonera") != null || $I("tblGrabar") != null){
                if (nFilaDesde != nFilaHasta)
                    activarGrabar();    
            }else{ //pantalla modal
                try{ activarGrabar(); }catch(e){}
            }    
        }catch(e){}
    } 
} 
