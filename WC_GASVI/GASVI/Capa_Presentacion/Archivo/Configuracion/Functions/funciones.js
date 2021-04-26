function init(){
    try {
        ocultarProcesando();
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

/* El resultado se env�a en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." � "ERROR@#@Descripci�n del error" */
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
                    mmoff("Suc", "Configuraci�n modificada.", 200);
                    break;
                default:
                    ocultarProcesando();
                    alert("Opci�n de RespuestaCallBack no contemplada ("+aResul[0]+")");
                    
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
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n de la moneda por defecto.", e.message);
    }
}
function setAviso(sValue) {
    try{
        mostrarProcesando();
        var js_args = "setAviso@#@" + sValue;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la configuraci�n del aviso de cambio de estado.", e.message);
    }
}

function setMotivo(sValue) {
    try {
        mostrarProcesando();
        var js_args = "setMotivo@#@" + sValue;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la configuraci�n del motivo por defecto.", e.message);
    }
}
function setEmpresa(sValue) {
    try {
        mostrarProcesando();
        var js_args = "setEmpresa@#@" + $I("cboEmpresa").value;
        js_args += "@#@" + $I("cboEmpresa").options[$I("cboEmpresa").selectedIndex].innerText;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la configuraci�n de la empresa por defecto.", e.message);
    }
}
