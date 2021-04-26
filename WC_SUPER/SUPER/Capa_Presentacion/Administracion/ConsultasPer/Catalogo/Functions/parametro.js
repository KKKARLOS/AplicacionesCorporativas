// JScript File
var aParams;
var oParamActivo;

function objParam(bd, idCons, idParam, nombre, texto, tipo, comentario, defecto, visible, orden, opcional){
	this.bd	= bd;
	this.idCons	= idCons;
	this.idParam = idParam;
	this.nombre = nombre;
	this.texto = texto;
	this.tipo	= tipo;
	this.comentario	= comentario;
	this.defecto	= defecto;
	this.visible	= visible;
	this.orden	= orden;
	this.opcional = opcional;
}
function insertarParamEnArray(bd, idCons, idParam, nombre, texto, tipo, comentario, defecto, visible, orden, opcional){
    try{
        oParam = new objParam(bd, idCons, idParam, nombre, texto, tipo, comentario, defecto, visible, orden, opcional);
        aParams[aParams.length]= oParam;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un Param en el array.", e.message);
    }
}
function buscarParamEnArray(idCons, idParam){
    try{
        for (var nIndice=0; nIndice < aParams.length; nIndice++){
            if (aParams[nIndice].idCons == idCons && aParams[nIndice].idParam == idParam){
                oParamActivo = aParams[nIndice];
                return aParams[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un Param en el array.", e.message);
    }
}
function buscarIndiceParamEnArray(idCons, idParam){
    try{
        for (var nIndice=0; nIndice < aParams.length; nIndice++){
            if (aParams[nIndice].idCons == idCons && aParams[nIndice].idParam == idParam)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de un Param en el array.", e.message);
    }
}
function borrarParamDeArray(idCons, idParam){
    try{
        aParams.splice(buscarIndiceParamEnArray(idCons, idParam), 1);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un Param del array.", e.message);
    }
}
function borrarConsDeArray(idCons){
    try{
        //for (var nIndice=0; nIndice < aParams.length; nIndice++){
        for (var nIndice=aParams.length -1; nIndice >=0 ; nIndice--){
            if (aParams[nIndice].idCons == idCons)
                aParams.splice(buscarIndiceParamEnArray(idCons, aParams[nIndice].idParam), 1);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un Param del array.", e.message);
    }
}
function marcarBorradoParams(idCons){
    try{
        for (var nIndice=aParams.length -1; nIndice >=0 ; nIndice--){
            if (aParams[nIndice].idCons == idCons){
                aParams[nIndice].bd="D";
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar un parámetro del array.", e.message);
    }
}
//function mostrarDatosParam(idParam){
//    try{
//        for (var nIndice=0; nIndice < aParams.length; nIndice++){
//            if (aParams[nIndice].idParam == idParam){
//                var objParam = aParams[nIndice];
//                var strMsg = "bd: "+ objParam.bd +"\nidCons: "+ objParam.idCons +"\nidParam: "+ objParam.idParam +"\nnombre: "+ objParam.nombre +"\n";
//                strMsg += " estado: "+ objParam.estado+"\norden: "+ objParam.orden +"\n";
//                alert(strMsg);
//                return;
//             }
//        }
//        return null;
//	}catch(e){
//		mostrarErrorAplicacion("Error al mostrar los datos de un Param del array.", e.message);
//    }
//}
function oParamActualizar(accion, clave, obj){
    try{
        //alert(accion+ "  "+ clave+ "  "+obj.outerHTML);
        if (oParamActivo == null) return;
        
        if (oParamActivo.bd != "I") oParamActivo.bd = accion;
        
        //alert(obj.type);
        if (obj.type == "checkbox"){
            if (obj.checked) eval("oParamActivo."+ clave +" = 1");
            else eval("oParamActivo."+ clave +" = 0");
        }else{
            //alert("oParamActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            if (clave == "texto" || clave == "comentario" || clave == "defecto" || clave == "nombre")
                eval("oParamActivo."+ clave +" = '"+ Utilidades.escape(obj.value) +"';");
            else{
                if (obj.value != null)
                    eval("oParamActivo."+ clave +" = '"+ obj.value +"';");
                else
                    eval("oParamActivo."+ clave +" = '"+ obj +"';");
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la bd del Param activo.", e.message);
    }
}
//Copia local de funcion global en draganddrop.js porque hay que actualizar el orden en el array que soporta los Param´s
function startDragIMGParam(e) 
{ 
    //if (bLectura) return;   
    if (!e) e = event; 
    var row = e.srcElement ? e.srcElement : e.target; 
    if (e.srcElement.nodeName != 'IMG') return;
    if (e.srcElement.src.indexOf("imgMoveRow")==-1) return;
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
        
        var aFilasParam=FilasDe("tblDatosParam");
        var nIndParam;
        for (var i=0; i<aFilasParam.length; i++){
            nIndParam=buscarIndiceParamEnArray(aFilasParam[i].getAttribute("idCons"), aFilasParam[i].id);
            if (aParams[nIndParam].bd != 'D'){
                if (aParams[nIndParam].orden != i){
                    aParams[nIndParam].orden = i;
                    if (aParams[nIndParam].bd != 'I')
                        aParams[nIndParam].bd = 'U';
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
