var bRegresar = false;

function init() {
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Textos de ayuda";
        $I("txtDescripcion").focus();
        ocultarProcesando();
        desActivarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
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
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                }
                setTimeout("timeverTooltip(" + $I("cboTooltips").value + ")", 500);
                break;
            case "verTooltip":
                $I("txtDescripcion").value = aResul[2];
                $I("hdnOrigen").value = $I("cboTooltips").value;
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function grabar(){
    try{     
        js_args = "grabar@#@";
        js_args += Utilidades.escape($I("txtDescripcion").value) + "@#@";
        js_args += $I("hdnOrigen").value;

        mostrarProcesando();
        desActivarGrabar();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

function verTooltip(sValue) {
    try {
        
        if (bCambios) {
            //if (confirm("Datos modificados. ¿Desea grabarlos?")) {
            //    grabar();                
            //    return;
            //}
            //else timeverTooltip(sValue);
            jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    mostrarProcesando();
                    grabar();
                }
                else {
                    bCambios = false;
                    timeverTooltip(sValue);
                }
            });
        }
        else timeverTooltip(sValue);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos.", e.message);
    }
}

function timeverTooltip(sValue) {
    try {
        var js_args = "verTooltip@#@" + sValue;
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos.", e.message);
    }
}
