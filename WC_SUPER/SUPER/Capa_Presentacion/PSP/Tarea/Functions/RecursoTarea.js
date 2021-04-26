// JScript File
var aRecursos = new Array();
var oRecActivo;

function objRecurso(opcionBD, idTarea, idRecurso, nombre, ete, ffe, etp, fip, ffp, idTarifa, estado, comentario, indicaciones, pendiente, completado, acumulado, sPriCons, sUltCons, bNotifExceso){
	this.opcionBD	= opcionBD;
	this.idTarea	= idTarea;
	this.idRecurso	= idRecurso;
	this.nombre		= nombre;
	this.ete		= ete;
	this.ffe		= ffe;
	this.etp		= etp;
	this.fip		= fip;
	this.ffp		= ffp;
	this.idTarifa	= idTarifa;
	this.estado		= estado;
	this.comentario	= comentario;
	this.indicaciones= indicaciones;
	this.pendiente  = pendiente;
	this.completado = completado;
	this.acumulado  = acumulado;
	this.sPriCons   = sPriCons;
	this.sUltCons   = sUltCons;
	this.bNotifExceso= bNotifExceso;
}
function insertarRecursoEnArray(opcionBD, idTarea, idRecurso, nombre, ete, ffe, etp, fip, ffp, idTarifa, estado, 
                                comentario, indicaciones, pendiente, completado, acumulado, sPriCons, sUltCons, bNotifExceso){
    try{
        oRec = new objRecurso(opcionBD, idTarea, idRecurso, nombre, ete, ffe, etp, fip, ffp, idTarifa, estado, 
                              comentario, indicaciones, pendiente, completado, acumulado, sPriCons, sUltCons, bNotifExceso);
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
                var strMsg = "opcionBD: "+ objRec.opcionBD +"\nidTarea: "+ objRec.idTarea +"\nidRecurso: "+ objRec.idRecurso +"\nnombre: "+ objRec.nombre +"\n";
                strMsg += "ete: "+ objRec.ete +"\nffe: "+ objRec.ffe +"\netp: "+ objRec.etp +"\nffp: "+ objRec.ffp +"\nidTarifa: "+ objRec.idTarifa +"\n";
                strMsg += "estado: "+ objRec.estado +"\ncomentario: "+ objRec.comentario +"\nindicaciones: "+ objRec.indicaciones +"\npendiente: "+ objRec.pendiente +"\n";
                strMsg += " completado: "+ objRec.completado+"\nacumulado: "+ objRec.acumulado +"\nsPriCons: "+ objRec.sPriCons +"\nsUltCons: "+ objRec.sUltCons +"\n";
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
        //alert(accion+ "  "+ clave+ "  "+obj.outerHTML);
        if (oRecActivo == null) return;
        
        if (oRecActivo.opcionBD != "I") oRecActivo.opcionBD = accion;
        
        //alert(obj.type);
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oRecActivo."+ clave +" = 1");
            else eval("oRecActivo."+ clave +" = 0");
        }else{
            //alert("oRecActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            if (clave == "indicaciones" || clave == "nombre")
                eval("oRecActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            else{
                if (obj.value != null)
                    eval("oRecActivo."+ clave +" = '"+ obj.value +"';");
                else
                    eval("oRecActivo."+ clave +" = '"+ obj +"';");
            }
        }
        //Actualizo la fila del recurso a 'U' (updatear) si estaba '' (sin modificar)
        var aFila = FilasDe("tblAsignados");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].id == oRecActivo.idRecurso){
                if (aFila[i].getAttribute("bd") == ""){
                    aFila[i].setAttribute("bd","U");
                }
                break;
            }
        }
        aGProf(0);
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la opcionBD del profesional activo.", e.message);
    }
}