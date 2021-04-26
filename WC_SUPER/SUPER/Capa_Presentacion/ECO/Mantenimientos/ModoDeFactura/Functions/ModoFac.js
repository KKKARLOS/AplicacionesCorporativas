// JScript File

var oModoFacActivo;

function objModoFac(bd, idSN2, idModoFac, nombre, estado){
	this.bd	= bd;
	this.idSN2	= idSN2;
	this.idModoFac	= idModoFac;
	this.nombre	= nombre;
	this.estado	= estado;
}
function insertarModoFacEnArray(bd, idSN2, idModoFac, nombre, estado){
    try{
        oModoFac = new objModoFac(bd, idSN2, idModoFac, nombre, estado);
        aModoFac[aModoFac.length]= oModoFac;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un modo de facturación en el array.", e.message);
    }
}
function buscarModoFacEnArray(idModoFac){
    try{
        for (var nIndice=0; nIndice < aModoFac.length; nIndice++){
            if (aModoFac[nIndice].idModoFac == idModoFac){
                oModoFacActivo = aModoFac[nIndice];
                return aModoFac[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un modo de facturación  en el array.", e.message);
    }
}
function buscarIndiceModoFacEnArray(idModoFac){
    try{
        for (var nIndice=0; nIndice < aModoFac.length; nIndice++){
            if (aModoFac[nIndice].idModoFac == idModoFac)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un modo de facturación  en el array.", e.message);
    }
}
function borrarModoFacDeArray(idModoFac){
    try{
        aModoFac.splice(buscarIndiceModoFacEnArray(idModoFac), 1);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un modo de facturación del array.", e.message);
    }
}
function borrarSN2DeArray(idSN2){
    try{
        for (var nIndice=0; nIndice < aModoFac.length; nIndice++){
            if (aModoFac[nIndice].idSN2 == idSN2)
                aModoFac.splice(buscarIndiceModoFacEnArray(aModoFac[nIndice].idModoFac), 1);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un modo de facturación del array.", e.message);
    }
}
function mostrarDatosModoFac(idModoFac){
    try{
        for (var nIndice=0; nIndice < aModoFac.length; nIndice++){
            if (aModoFac[nIndice].idModoFac == idModoFac){
                var objModoFac = aModoFac[nIndice];
                var strMsg = "bd: "+ objModoFac.bd +"\nidSN2: "+ objModoFac.idSN2 +"\nidModoFac: "+ objModoFac.idModoFac +"\nnombre: "+ objModoFac.nombre +"\n";
                strMsg += " estado: "+ objModoFac.estado+"\n";
                mmoff("Inf", strMsg, 400);
                return;
             }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de un modo de facturación  del array.", e.message);
    }
}
function oModoFacActualizar(accion, clave, obj){
    try{
        //alert(accion+ "  "+ clave+ "  "+obj.outerHTML);
        if (oModoFacActivo == null) return;
        
        if (oModoFacActivo.bd != "I") oModoFacActivo.bd = accion;
        
        //alert(obj.type);
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oModoFacActivo."+ clave +" = 1");
            else eval("oModoFacActivo."+ clave +" = 0");
        }else{
            //alert("oModoFacActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            if (clave == "nombre")
                eval("oModoFacActivo."+ clave +" = '"+ obj.value +"';");
            else{
                if (obj.value != null)
                    eval("oModoFacActivo."+ clave +" = '"+ obj.value +"';");
                else
                    eval("oModoFacActivo."+ clave +" = '"+ obj +"';");
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la bd del modo de facturación activo.", e.message);
    }
}
