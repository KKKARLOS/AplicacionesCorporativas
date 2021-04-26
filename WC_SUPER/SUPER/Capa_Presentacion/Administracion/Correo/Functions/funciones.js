function init(){
    try{
        refrescar();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
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
                $I("hdnEstadoCorreo").value = aResul[2];
                refrescar();
                //mmoff("Suc","Grabación correcta", 160);
                break;
        }
        ocultarProcesando();
    }
}
function grabar(){
    try{
        var sEst = "A";
        if ($I("hdnEstadoCorreo").value == "A")
            sEst = "D";
        var js_args = "grabar@#@" + sEst;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al grabar", e.message);
    }
}
function refrescar(){
    try {
        if ($I("hdnEstadoCorreo").value == "A") {
            $I("imgEstadoCorreo").src = "../../../Images/imgCorreoOn.png";
            $I("lblAccion").innerHTML = "Desactivar";
            $I("lblEstado").innerHTML = "Activado";
        }
        else {
            $I("imgEstadoCorreo").src = "../../../Images/imgCorreoOff.png";
            $I("lblAccion").innerHTML = "&nbsp;&nbsp;&nbsp;Activar";
            $I("lblEstado").innerHTML = "Desactivado";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al refrescar la pantalla", e.message);
    }
}
