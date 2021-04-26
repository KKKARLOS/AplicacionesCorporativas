function init(){
    try {
        ocultarProcesando();
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/* El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error" */
function RespuestaCallBack(strResultado, context){
    try{
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            mostrarErrorSQL(aResul[3], aResul[2]);
        }else{
            switch (aResul[0])
            {
                case "setMoneda": 
                case "setAviso":
                case "setMotivo":
                case "setEmpresa":
                    mmoff("Suc", "Configuración modificada.", 200);
                    break;
                default:
                    ocultarProcesando();
                    alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                    
            }
            ocultarProcesando();
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la respuesta del callback.", e.message);
    }
    window.focus();
}

function setMoneda(sValue) {
    try{
        mostrarProcesando();
        var js_args = "setMoneda@#@" + sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración de la moneda por defecto.", e.message);
    }
}
function setAviso(sValue) {
    try{
        mostrarProcesando();
        var js_args = "setAviso@#@" + sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuración del aviso de cambio de estado.", e.message);
    }
}

function setMotivo(sValue) {
    try {
        mostrarProcesando();
        var js_args = "setMotivo@#@" + sValue;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la configuración del motivo por defecto.", e.message);
    }
}
function setEmpresa(sValue) {
    try {
        mostrarProcesando();
        var js_args = "setEmpresa@#@" + $I("cboEmpresa").value;
        js_args += "@#@" + $I("cboEmpresa").options[$I("cboEmpresa").selectedIndex].innerText;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la configuración de la empresa por defecto.", e.message);
    }
}
