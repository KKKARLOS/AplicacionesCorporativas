// JScript File
var aNodoReplica= new Array();
var oNodoActivo;

function objNodo(idProyecto, idNodo, desNodo, tiporeplica, propuestafirme, idGestor, nombreGestor){
	this.idProyecto	    = idProyecto;
	this.idNodo	        = idNodo;
	this.desNodo	    = desNodo;
	this.tiporeplica	= tiporeplica;
	this.propuestafirme	= propuestafirme;
	this.idGestor		= idGestor;
	this.nombreGestor	= nombreGestor;
}

function insertarNodoEnArray(idProyecto, idNodo, desNodo, tiporeplica, propuestafirme, idGestor, nombreGestor){
    try{
        oNodo = new objNodo(idProyecto, idNodo, desNodo, tiporeplica, propuestafirme, idGestor, nombreGestor);
        aNodoReplica[aNodoReplica.length]= oNodo;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un nodo en el array.", e.message);
    }
}


function buscarNodoEnArray(idNodo){
    try{
        for (var nIndice=0; nIndice < aNodoReplica.length; nIndice++){
            if (aNodoReplica[nIndice].idNodo == idNodo){
                oNodoActivo = aNodoReplica[nIndice];
                return aNodoReplica[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un nodo en el array.", e.message);
    }
}

function setNodoActivo(nProyecto, nNodo){
    try{
        for (var nIndice=0; nIndice < aNodoReplica.length; nIndice++){
            if (aNodoReplica[nIndice].idProyecto == nProyecto && aNodoReplica[nIndice].idNodo == nNodo){
                oNodoActivo = aNodoReplica[nIndice];
                return;
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un nodo en el array.", e.message);
    }
}

function buscarIndiceNodoEnArray(idNodo){
    try{
        for (var nIndice=0; nIndice < aNodoReplica.length; nIndice++){
            if (aNodoReplica[nIndice].idNodo == idNodo)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un nodo en el array.", e.message);
    }
}

function mostrarDatosNodo(idNodo){
    try{
        for (var nIndice=0; nIndice < aNodoReplica.length; nIndice++){
            if (aNodoReplica[nIndice].idNodo == idNodo){
                var objNodo = aNodoReplica[nIndice];
                var strMsg = "idProyecto: "+ objNodo.idProyecto +"\nidNodo: "+ objNodo.idNodo +"\ndesNodo: "+ objNodo.desNodo +"\n";
                strMsg += "tiporeplica: "+ objNodo.tiporeplica +"\npropuestafirme: "+ objNodo.propuestafirme +"\nidGestor: "+ objNodo.idGestor +"\nnombreGestor: "+ objNodo.nombreGestor;
                mmoff("Inf", strMsg, 400);
                return;
             }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de un profesional del array.", e.message);
    }
}

function mostrarDatosNodo2(objNodo){
    try{
        var strMsg = "idProyecto: "+ objNodo.idProyecto +"\nidNodo: "+ objNodo.idNodo +"\ndesNodo: "+ objNodo.desNodo +"\n";
        strMsg += "tiporeplica: "+ objNodo.tiporeplica +"\npropuestafirme: "+ objNodo.propuestafirme +"\nidGestor: "+ objNodo.idGestor +"\nnombreGestor: "+ objNodo.nombreGestor;
        mmoff("Inf", strMsg, 400);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de un nodo del array.", e.message);
    }
}

function oNodoActualizar(accion, clave, obj){
    try{
        //alert(accion+ "  "+ clave+ "  "+obj.outerHTML);
        if (oNodoActivo == null) return;
        
        //alert(obj.type);
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oNodoActivo."+ clave +" = 1");
            else eval("oNodoActivo."+ clave +" = 0");
        }else{
            //alert("oNodoActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            eval("oNodoActivo."+ clave +" = '"+ obj.value +"';");
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los datos del nodo activo.", e.message);
    }
}