var aNODOS;
var oNODOActivo;

function objNODO(bd, idUser, idNODO, nombre){
	this.bd	= bd;
	this.idUser	= idUser;
	this.idNODO	= idNODO;
	this.nombre	= nombre;
}
function insertarNODOEnArray(bd, idUser, idNODO, nombre){
    try{
        oNODO = new objNODO(bd, idUser, idNODO, nombre);
        aNODOS[aNODOS.length]= oNODO;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un NODO en el array.", e.message);
    }
}
function buscarNODOEnArray(idUser, idNODO){
    try{
        for (var nIndice=0; nIndice < aNODOS.length; nIndice++){
            if (aNODOS[nIndice].idNODO == idNODO && aNODOS[nIndice].idUser == idUser){
                oNODOActivo = aNODOS[nIndice];
                return aNODOS[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un NODO en el array.", e.message);
    }
}
function buscarIndiceNODOEnArray(idUser, idNODO){
    try{
        for (var nIndice=0; nIndice < aNODOS.length; nIndice++){
            if (aNODOS[nIndice].idNODO == idNODO && aNODOS[nIndice].idUser == idUser)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un NODO en el array.", e.message);
    }
}
function borrarNODODeArray(idUser, idNODO){
    try{
        aNODOS.splice(buscarIndiceNODOEnArray(idUser, idNODO), 1);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un NODO del array.", e.message);
    }
}
function borrarUserDeArray(idUser){
    try{
        if (aNODOS != null){
            for (var nIndice=aNODOS.length -1; nIndice >=0 ; nIndice--){
                if (aNODOS[nIndice].idUser == idUser)
                    aNODOS.splice(buscarIndiceNODOEnArray(aNODOS[nIndice].idNODO), 1);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un usuario del array.", e.message);
    }
}
function oNODOActualizar(accion, clave, obj){
    try{
        if (oNODOActivo == null) return;
        
        if (oNODOActivo.bd != "I") oNODOActivo.bd = accion;
        
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oNODOActivo."+ clave +" = 1");
            else eval("oNODOActivo."+ clave +" = 0");
        }else{
            if (clave == "nombre")
                eval("oNODOActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            else{
                if (obj.value != null)
                    eval("oNODOActivo."+ clave +" = '"+ obj.value +"';");
                else
                    eval("oNODOActivo."+ clave +" = '"+ obj +"';");
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la bd del NODO activo.", e.message);
    }
}
