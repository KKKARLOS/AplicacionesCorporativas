var bCrearNuevo = false;
var bHayCambios=false;

function init(){
    try{
        if (!mostrarErrores()) return;
        //alert(sIDDocuAux);
        swmMulti($I("txtObs"));
        ocultarProcesando();
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function Tramitar(){
    try
    {
        if (getOp($I("btnTramitar")) != 100) return;
        grabar();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Tramitar", e.message);	
	}              
}
function unload(){
}

function cerrarVentana(){
    var returnValue = (bHayCambios)? "OK":null;
    modalDialog.Close(window, returnValue);
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                bCambios = false;
                bHayCambios = true;
                $I("hdnIdSolicitud").value = aResul[2];
                mmoff("Inf", "Solicitud enviada correctamente", 250);
                setTimeout("cerrarVentana();", 2000);
                break;
            case "documentos":
                $I("txtNombreDocumento").value = aResul[2];
                break;
            case "borrarDoc":
                setTimeout("addDoc();", 100);
                break;
            //            case "elimdocs":  
//                var aFila = FilasDe("tblDocumentos");
//                for (var i=aFila.length-1;i>=0;i--){
//                    if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
//                }
//                aFila = null;
//                nfs = 0;
//                ocultarProcesando();
//                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDen").value == "") {
            $I("txtDen").focus();
            mmoff("War", "Debes indicar la denominación", 250);
            return false;
        }
        if ($I("txtObs").value == "" || $I("txtObs").className == "WaterMarkMulti") {
            $I("txtObs").focus();
            mmoff("War", "Debes indicar observaciones", 250);
            return false;
        }
        if ($I("txtNombreDocumento").value == "") {
            mmoff("War", "Debes adjuntar un documento", 250);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar(){
    try{
        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        var js_args = "grabar@#@" + $I("hdnTipo").value + "##" + Utilidades.escape($I("txtDen").value) + "##" + 
                                    Utilidades.escape($I("txtObs").value) + "##" + sIDDocuAux + "##" +
                                    $I("hdnIdCert").value + "##" + $I("hdnIdFicepiExamen").value;
        RealizarCallBack(js_args, "");
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al establecer los datos a grabar", e.message);
		return false;
    }
}
function addDoc(){
    if ($I("hdnIdSolicitud").value == "-1") {
        nuevoDoc("SC", sIDDocuAux);
    }
    else{
        nuevoDoc("SC", $I("hdnIdSolicitud").value);
    }
} 
//function delDoc(){
//    //if ($I("hdnModoAcceso").value=="R") return;
//    eliminarDoc();
//}
//function agregarDoc() {
//    try{
//        if ($I("txtNombreDocumento").value == "") {
//            addDoc();
//        }
//        else {//Si ya había un documento asociado, hay que borrarlo antes de añadir uno nuevo
//            borrarDoc();
//        }
//    }
//    catch (e) {
//        mostrarErrorAplicacion("Error al asociar documento a la petición de examen", e.message);
//        return false;
//    }
//}
//function borrarDoc() {
//    try {
//        mostrarProcesando();
//        var js_args = "borrarDoc@#@" + $I("hdnIdSolicitud").value + "##" + sIDDocuAux;
//        RealizarCallBack(js_args, "");
//    }
//    catch (e) {
//        mostrarErrorAplicacion("Error al intentar eliminar un documento de la petición de examen", e.message);
//        return false;
//    }
//}
//Set Water Mark
function swmMulti(oControl) {
    try {
        if (oControl.disabled) return;
        if (oControl.getAttribute("bSwm") == null) {
            if (oControl.getAttribute("watermarktext") == null) {
                if (typeof (mmoff) == "function") {
                    mmoff("War", "No se ha podido determinar la máscara", 270);
                    return;
                }
            }
            oControl.onfocus = function() {
                if (this.className == "WaterMarkMulti" && this.value == this.getAttribute("watermarktext")) {
                    this.value = "";
                    this.value = this.getAttribute("watermarktext");
                }
            };

            //oControl.onkeydown = fn_kd;
            oControl.onkeypress = function() {
                if (this.className == "WaterMarkMulti" && this.value == this.getAttribute("watermarktext")) {
                    this.className = "txtMultiM";
                    this.value = "";
                }
            };

            oControl.onkeyup = function(e) {
                if (!e) e = event;
                if (this.className == "WaterMarkMulti" && this.value != this.getAttribute("watermarktext") && e.ctrlKey) {
                    this.className = "txtMultiM";
                    this.value = this.value.replace(this.getAttribute("watermarktext"), "");
                }
            };

            oControl.onblur = function() {
                if (this.className == "txtMultiM" && this.value == "") {
                    this.className = "WaterMarkMulti";
                    this.value = this.getAttribute("watermarktext");
                }
            };

            oControl.setAttribute("bSwm", 1);
        }
        oControl.focus();
        oControl.blur();
    } catch (e) {
        try {
            mostrarErrorAplicacion("Error al establecer la marca de agua al control '" + oControl.id + "'", e.message);
        } catch (e) {//por si el control no tiene ID.
            mostrarErrorAplicacion("Error al establecer la marca de agua", e.message);
        }
    }
}
