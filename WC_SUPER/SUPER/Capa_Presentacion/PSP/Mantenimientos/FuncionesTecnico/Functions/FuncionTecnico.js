// JScript File
var aTecnicos = new Array();
var oTecnicoActivo;

function objTecnico(opcionBD, idFuncion, idTecnico, nombre){
	this.opcionBD	= opcionBD;
	this.idFuncion	= idFuncion;
	this.idTecnico	= idTecnico;
	this.nombre		= nombre;
}
function insertarTecnicoEnArray(opcionBD, idFuncion, idTecnico, nombre){
    try{
        oTec = new objTecnico(opcionBD, idFuncion, idTecnico, nombre);
        aTecnicos[aTecnicos.length]= oTec;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un profesional en el array.", e.message);
    }
}
function buscarTecnicoEnArray(idTecnico){
    try{
        for (var nIndice=0; nIndice < aTecnicos.length; nIndice++){
            if (aTecnicos[nIndice].idTecnico == idTecnico){
                oTecnicoActivo = aTecnicos[nIndice];
                return aTecnicos[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un profesional en el array.", e.message);
    }
}
function buscarIndiceTecnicoEnArray(idTecnico){
    try{
        for (var nIndice=0; nIndice < aTecnicos.length; nIndice++){
            if (aTecnicos[nIndice].idTecnico == idTecnico)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un profesional en el array.", e.message);
    }
}
function borrarTecnicoDeArray(idTecnico){
    try{
        aTecnicos.splice(buscarIndiceTecnicoEnArray(idTecnico), 1);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un Tecnico del array.", e.message);
    }
}
function mostrarDatosTecnico(idTecnico){
    try{
        for (var nIndice=0; nIndice < aTecnicos.length; nIndice++){
            if (aTecnicos[nIndice].idTecnico == idTecnico){
                var objTec = aTecnicos[nIndice];
                var strMsg = "opcionBD: "+ objRec.opcionBD;
                strMsg += "\nidFuncion: "+ objRec.idFuncion;
                strMsg += "\nidTecnico: "+ objRec.idTecnico;
                strMsg += "\nnombre: "+ objRec.nombre +"\n";
                mmoff("Inf", strMsg, 400);
                return;
             }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de un profesional del array.", e.message);
    }
}
function oTecActualizar(accion, clave, obj){
    try{
        if (oTecnicoActivo == null) return;
        
        if (oTecnicoActivo.opcionBD != "I") oTecnicoActivo.opcionBD = accion;
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oTecnicoActivo."+ clave +" = 1");
            else eval("oTecnicoActivo."+ clave +" = 0");
        }else{
            if (clave == "indicaciones" || clave == "nombre")
                eval("oTecnicoActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            else
                eval("oTecnicoActivo."+ clave +" = '"+ obj.value +"';");
        }
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la opcionBD del Tecnico activo.", e.message);
    }
}