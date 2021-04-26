function init(){
    try{
        if (!mostrarErrores()) return;
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function cerrarVentana() {
    try {
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function aceptar() {
    try {
        mostrarProcesando();
        var returnValue = ($I("chkLineaBase").checked) ? "S" : "N";
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
    

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        var reg = /\\n/g;
        ocultarProcesando();
        if (aResul[1] != "AVISO") {
            mostrarError(aResul[2].replace(reg, "\n"));
        }
        else {
            var sMens = "Existen tareas cuya fecha de fin planificada es superior a la prevista.<br /><br />¿Deseas continuar?";
            jqConfirm("", sMens, "", "", "war", 400).then(function (answer) {
                if (answer) 
                {
                    mostrarProcesando();
                    var strEnlace = strServer + "Capa_Presentacion/PSP/proyecto/Exportar/ListaTareas.aspx?p=" + $I("hdnPSN").value + "&RTPT=" + $I("hdnRTPT").value;
                    modalDialog.Show(strEnlace, self, sSize(850, 470))
                        .then(function (ret) {
                            aceptar();
                        });
                    window.focus();
                    ocultarProcesando();
                }
                else cerrarVentana();
            });
        }
    }else{
        switch (aResul[0]){
           case "procesar":
                bCambios = false;
                ocultarProcesando();
                aceptar();
                break;                
                                               
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function procesar(){
    try{
        //if (bProcesando()) return;
        
        var js_args = "procesar@#@";
        js_args += $I("hdnPSN").value + "@#@";
        js_args += ($I("chkLineaBase").checked) ? "1@#@" : "0@#@";
        js_args += $I("hdnRTPT").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
                
	}catch(e){
		mostrarErrorAplicacion("Error en procesar", e.message);
    }            
}
