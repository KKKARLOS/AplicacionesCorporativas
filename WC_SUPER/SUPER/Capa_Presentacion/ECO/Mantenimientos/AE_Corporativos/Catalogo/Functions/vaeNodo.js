// JScript File
//var aVAEsNodo = new Array();
var aVAEsNodo;
var oVAENodoActivo;

//function objVAENodo(bd, idAE, idVAE, idNodo, nombre){
function objVAENodo(bd, idAE, idVAE, nombre){
	this.bd	= bd;
	this.idAE	= idAE;
	this.idVAE	= idVAE;
	//this.idNodo	= idNodo;
	this.nombre	= nombre;
}
//function insertarVAENodoEnArray(bd, idAE, idVAE, idNodo, nombre){
function insertarVAENodoEnArray(bd, idAE, idVAE, nombre){
    try{
        //oVAENodoActivo = new objVAENodo(bd, idAE, idVAE, idNodo, nombre);
        oVAENodoActivo = new objVAENodo(bd, idAE, idVAE, nombre);
        aVAEsNodo[aVAEsNodo.length]= oVAENodoActivo;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un VAENodo en el array.", e.message);
    }
}
//function buscarVAENodoEnArray(idVAE, idNodo){
function buscarVAENodoEnArray(idVAE){
    try{
        for (var nIndice=0; nIndice < aVAEsNodo.length; nIndice++){
            //if (aVAEsNodo[nIndice].idVAE == idVAE && aVAEsNodo[nIndice].idNodo == idNodo){
            if (aVAEsNodo[nIndice].idVAE == idVAE){
                oVAENodoActivo = aVAEsNodo[nIndice];
                return aVAEsNodo[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un VAENodo en el array.", e.message);
    }
}
//function buscarIndiceVAENodoEnArray(idVAE, idNodo){
function buscarIndiceVAENodoEnArray(idVAE){
    try{
        for (var nIndice=0; nIndice < aVAEsNodo.length; nIndice++){
            //if (aVAEsNodo[nIndice].idVAE == idVAE && aVAEsNodo[nIndice].idNodo == idNodo)
            if (aVAEsNodo[nIndice].idVAE == idVAE)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un VAENodo en el array.", e.message);
    }
}
//function borrarVAENodoDeArray(idVAE, idNodo){
function borrarVAENodoDeArray(idVAE){
    try{
        //aVAEsNodo.splice(buscarIndiceVAENodoEnArray(idVAE, idNodo), 1);
        aVAEsNodo.splice(buscarIndiceVAENodoEnArray(idVAE), 1);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un VAENodo del array.", e.message);
    }
}
//function borrarAENodoDeArray(idAE, idNodo){
function borrarAENodoDeArray(idAE){
    try{
        for (var nIndice=aVAEsNodo.length -1; nIndice >=0 ; nIndice--){
            if (aVAEsNodo[nIndice].idAE == idAE){
                //aVAEsNodo.splice(buscarIndiceVAENodoEnArray(aVAEsNodo[nIndice].idVAE, idNodo), 1);
                aVAEsNodo.splice(buscarIndiceVAENodoEnArray(aVAEsNodo[nIndice].idVAE), 1);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un VAENodo del array.", e.message);
    }
}
//function marcarBorradoAENodoDeArray(idAE, idNodo){
function marcarBorradoAENodoDeArray(idAE){
    try{
        for (var nIndice=aVAEsNodo.length -1; nIndice >=0 ; nIndice--){
            if (aVAEsNodo[nIndice].idAE == idAE){
                aVAEsNodo[nIndice].bd="D";
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un VAENodo del array.", e.message);
    }
}
