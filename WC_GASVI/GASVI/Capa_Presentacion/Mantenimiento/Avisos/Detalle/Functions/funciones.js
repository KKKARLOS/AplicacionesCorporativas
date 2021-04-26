var bHayCambios=false;
var bSaliendo=false;
var aFilaT;
function init(){
    try{
        if (!mostrarErrores()) return;
        
        bCambios=false;
        bHayCambios = false;
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);        
        $I("txtDen").focus();      
        ocultarProcesando();
        //alert($I("hdnIdAviso").value);
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function unload(){
    if (!bSaliendo)
        salir();
}
//function salir(){
//    bSalir=false;
//    bSaliendo=true;
//    if (bCambios && intSession > 1){
//        if (confirm("Datos modificados. ¿Desea grabarlos?")){
//            bEnviar = grabar();
//        }else bCambios=false;
//    }
//    var strRetorno="F";
//    if (bHayCambios)strRetorno ="T";
//    var returnValue = strRetorno;
//    modalDialog.Close(window, returnValue);
//}
function salir() {
    try {
        var strRetorno = "F";
        if (bHayCambios) strRetorno = "T";
        bSalir = false;
        bSaliendo = true;
        if (bCambios && intSession > 1) {
            jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    bEnviar = grabar();
                    return;
                }
                else bCambios = false;
                modalDialog.Close(window, strRetorno);
            });
        }
        else modalDialog.Close(window, strRetorno);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    var iCont=0;
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                bCambios = false;
                $I("hdnIdAviso").value = aResul[2];

                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);
                ocultarProcesando();  
                mmoff("Suc", "Grabación correcta", 230);
                
                if (bSalir) salir();
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
    }
}
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@" + $I("hdnIdAviso").value + "@#@";
        js_args += grabarP0();//datos generales
       
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del detalle de aviso", e.message);
    }
}
function grabarP0(){
    var js_args="";
    js_args += Utilidades.escape($I("txtDen").value) +"{sep}";  //0
    js_args += Utilidades.escape($I("txtTit").value) + "{sep}"; //1
    js_args += Utilidades.escape($I("txtDescripcion").value) +"{sep}"; //2
    
    if ($I("chkBorrable").checked) js_args += "1{sep}"; //3
    else js_args += "0{sep}"; //3
    js_args += $I("txtValIni").value+"{sep}"; //4
    js_args += $I("txtValFin").value; //5
    
    return js_args;
}

function comprobarDatos(){
    try{
        if ($I("txtDen").value == ""){
            alert("Debe indicar el nombre del aviso");
            return false;
        }
        if ($I("txtTit").value == ""){
            alert("Debe indicar el título del aviso");
            return false;
        }
        if ($I("txtDescripcion").value == ""){
            alert("Debe indicar el texto del aviso");
            return false;
        }
        //La fecha de fin de vigencia no puede ser anterior a la de inicio
        if (!fechasCongruentes($I("txtValIni").value, $I("txtValFin").value)){
            $I("txtValFin").select();
            alert("La fecha de fin de vigencia debe ser posterior a la de inicio o debe especificar la fecha de inicio");
            return false;
        }
        
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function aG(iPestana){//Sustituye a activarGrabar
    try{
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);   
        
        bCambios = true;
        bHayCambios=true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

