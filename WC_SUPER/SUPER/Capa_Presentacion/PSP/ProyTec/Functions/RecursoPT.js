var aRecursos = new Array();
var oRecActivo;
function objRecurso(opcionBD, idPT, idRecurso, nombre,fip, ffe, idTarifa, estado, indicaciones, bNotifExceso){
	this.opcionBD	= opcionBD;
	this.idPT       = idPT;
	this.idRecurso	= idRecurso;
	this.nombre		= nombre;
	this.ffe		= ffe;
	this.fip		= fip;
	this.idTarifa	= idTarifa;
	this.estado		= estado;
	this.indicaciones= indicaciones;
	this.bNotifExceso= bNotifExceso;
}
function insertarRecursoEnArray(opcionBD, idPT, idRecurso, nombre,fip, ffe, idTarifa, estado, indicaciones, bNotifExceso){
    try{
        oRec = new objRecurso(opcionBD, idPT, idRecurso, nombre, fip, ffe, idTarifa, estado, indicaciones, bNotifExceso);
        aRecursos[aRecursos.length]= oRec;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un profesional en el array.", e.message);
    }
}
function buscarRecursoEnArray(idRecurso){
    try{
        for (var nIndice=0; nIndice < aRecursos.length; nIndice++){
            if (aRecursos[nIndice].idRecurso == idRecurso){
                oRecActivo = aRecursos[nIndice];
                return aRecursos[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un profesional en el array.", e.message);
    }
}
function buscarIndiceRecursoEnArray(idRecurso){
    try{
        for (var nIndice=0; nIndice < aRecursos.length; nIndice++){
            if (aRecursos[nIndice].idRecurso == idRecurso)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un profesional en el array.", e.message);
    }
}
function borrarRecursoDeArray(idRecurso){
    try{
        aRecursos.splice(buscarIndiceRecursoEnArray(idRecurso), 1);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un profesional del array.", e.message);
    }
}
function mostrarDatosRecurso(idRecurso){
    try{
        for (var nIndice=0; nIndice < aRecursos.length; nIndice++){
            if (aRecursos[nIndice].idRecurso == idRecurso){
                var objRec = aRecursos[nIndice];
                var strMsg = "opcionBD: "+ objRec.opcionBD +"\nidPT: "+ objRec.idPT +"\nidRecurso: "+ objRec.idRecurso +"\nnombre: "+ objRec.nombre +"\n";
                strMsg += "perfil: "+ objRec.perfil +"\nffe: "+ objRec.ffe +"\nidTarifa: "+ objRec.idTarifa +"\n";
                strMsg += "estado: "+ objRec.estado +"\nindicaciones: "+ objRec.indicaciones;
                mmoff("Inf", strMsg, 400);
                return;
             }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de un profesional del array.", e.message);
    }
}
function oRecActualizar(accion, clave, obj){
    try{
        if (oRecActivo == null) return;
        if (oRecActivo.opcionBD != "I") oRecActivo.opcionBD = accion;
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oRecActivo."+ clave +" = 1");
            else eval("oRecActivo."+ clave +" = 0");
        }else{
            if (clave == "indicaciones" || clave == "nombre")
                eval("oRecActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            else
                eval("oRecActivo."+ clave +" = '"+ obj.value +"';");
        }
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la opcionBD del profesional activo.", e.message);
    }
}