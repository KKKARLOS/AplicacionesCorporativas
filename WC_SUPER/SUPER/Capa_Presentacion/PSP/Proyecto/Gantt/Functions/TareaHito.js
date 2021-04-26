// JScript File
var aHCM = new Array();
var oHCMActivo;

function oTareaHito(idHito, idTarea, ffpr){
	this.idHito	= idHito;
	this.idTarea= idTarea;
	this.ffpr	= ffpr;
}

function oHCM_I(idHito, idTarea, ffpr){// insertarTareaHitoEnArray
    try{
        oHCM = new oTareaHito(idHito, idTarea, ffpr);
        aHCM[aHCM.length]= oHCM;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una tarea de HCM en el array.", e.message);
    }
}
