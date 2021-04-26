//Para controlar el acceso a la estructura sin haber grabado la pantalla
var bEstructura=false;
var bGrabable=true;
function init(){
//	Si la plantilla es Empresarial solo será modificable si el usuario conectado tiene perfil de Administrador
//	Si la plantilla es Departamental solo será modificable si el usuario conectado tiene perfil de Oficina Técnica o superior
//	Si la plantilla es Personal siempre es modificable (se supone que un usuario solo ve las plantillas personales que son suyas)
    var sModificable;
    try{
        sModificable=$I("txtModificable").value;
        if (sModificable=="T"){bGrabable=true;}
        else {bGrabable=false;}
        if (bGrabable){
            $I("cboAmbito").disabled=false;
             //El estado del combo de CR´s se trata en la función establecerTipo
            $I("txtDesPlantilla").disabled=false;
            $I("chkActivo").disabled=false;
            $I("txtObs").disabled=false;
       }
        else{
            $I("cboAmbito").disabled=true;
            //$I("CPHC_cboCR").disabled=true;
            $I("cboCR").disabled=true;
            $I("txtDesPlantilla").disabled=true;
            $I("chkActivo").disabled=true;
            $I("txtObs").disabled=true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al iniciar la página", e.message);
    }
}

function establecerTipo(){
// En función del tipo de plantilla al campo CR estará deshabilitado o será obligatorio
    var sTipo;
    try{
        if (bGrabable){
            sTipo=$I("cboAmbito").value;
            switch (sTipo){
                case "E"://empresarial
//                    document.getElementById('ctl00_CPHC_cboCR').disabled=true;
//                    document.getElementById('ctl00_CPHC_cboCR').value="";
                    $I("cboCR").disabled=true;
                    $I("cboCR").value="-1";
                    break;
                case "D"://departamental
//                    if(document.getElementById('ctl00_CPHC_cboCR').length==2){
//                        document.getElementById('ctl00_CPHC_cboCR').value=document.getElementById('ctl00_CPHC_hndCRActual').value;
//                        document.getElementById('ctl00_CPHC_cboCR').disabled=true;
//                    }
//                    else{
//                        document.getElementById('ctl00_CPHC_cboCR').disabled=false;
//                    }
                    $I("cboCR").disabled=false;
                    if($I("hndCRActual").value != "")
                        $I("cboCR").value=$I("hndCRActual").value;
                    break;
                case "P"://personal
//                    document.getElementById('ctl00_CPHC_cboCR').disabled=true;
//                    document.getElementById('ctl00_CPHC_cboCR').value="";
                    $I("cboCR").disabled=true;
                    $I("cboCR").value="-1";
                    break;
             activarGrabar();
            }
        }
        else{
            //document.getElementById('ctl00_CPHC_cboCR').disabled=true;
            $I("cboCR").disabled=true;
        }
    }catch(e){
		mostrarErrorAplicacion("Error al establecer el ámbito de la plantilla", e.message);
    }
    return;
}
function comprobarDatos(){
    try{
        if ($I("txtDesPlantilla").value == ""){
            mmoff("War", "Debes indicar el nombre de la plantilla",230);
            return false;
        }
        if ($I("cboAmbito").value == "-1"){
            mmoff("War", "Debes indicar a qué ambito pertenece la plantilla",290);
            $I("cboAmbito").focus();
            return false;
        }
        if (($I("cboAmbito").value == "D") && ($I("cboCR").value == "-1")){
            mmoff("War", "Para grabar una plantilla departamental\ndebes indicar el centro de responsabilidad",260);
            $I("cboAmbito").focus();
            return false;
        }
       
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function unload() {
    try {
        var bEnviar = true;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) bEnviar = grabar();
                return bEnviar;
            });
        }
        else return bEnviar;
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función unload", e.message);
    }
}

function grabar(){
    try{
        if (!bGrabable)return false;
        if (!comprobarDatos()) return false;

        var js_args = "grabar@#@";
        js_args += $I("hdnIDPlantilla").value +"@#@";
        js_args += Utilidades.escape($I("txtDesPlantilla").value) +"@#@";
        js_args += $I("cboCR").value +"@#@";
        if ($I("chkActivo").checked) js_args += "1@#@";
        else  js_args += "0@#@";
        js_args += $I("cboAmbito").value +"@#@";
        js_args += $I("txtTipo").value +"@#@";
        js_args += Utilidades.escape($I("txtObs").value);

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}
function desglosePlantilla(){
    try{
        if (bCambios) {
            jqConfirm("", "Se han producido cambios en la plantilla.<br>Para acceder a su estructura debes grabarlos.<br /><br />¿Deseas hacerlo?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bEstructura = true;
                    grabar();
                    return;
                } else LLamarDesglosePlantilla();

            });
        } else LLamarDesglosePlantilla();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el desglose de la plantilla-1", e.message);
    }
}
function LLamarDesglosePlantilla(){
    try{
        bEstructura=false;
        var nIDPlant = $I("hdnIDPlantilla").value;
        var sDesPlant = $I("txtDesPlantilla").value;
        
        if ((nIDPlant == "") || (nIDPlant == "0")){
            mmoff("Inf", "Debes seleccionar alguna plantilla para poder consultar su desglose",400);
            return;
        }
        location.href = "../Detalle/Default.aspx?nIDPlant="+ nIDPlant+"&sDesPlant="+sDesPlant+"&sAmbito="+$I("txtTipo").value;
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el desglose de la plantilla-2", e.message);
    }
}
//function eliminarPlantilla(){
//    try{
//        if (!bGrabable)return;
        
//        var nIDPlant = $I("hdnIDPlantilla").value;
//        var js_args = "eliminar@#@"+nIDPlant;
//        mostrarProcesando();
//        RealizarCallBack(js_args, "");  
//        location.href = "../Catalogo/Default.aspx";
//        desActivarGrabar();
//        //return; //El return detiene el location.href en Chrome.
//	}catch(e){
//		mostrarErrorAplicacion("Error al eliminar la plantilla", e.message);
//    }
//}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                $I("hdnIDPlantilla").value = aResul[2];
                desActivarGrabar();
                if (bEstructura) desglosePlantilla();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                break;
        }
    }
}


