function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Parametrización";
        ocultarProcesando();
        window.focus();
        cargarCombos();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    try{
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            mostrarErrorSQL(aResul[3], aResul[2]);
        }else{
            switch (aResul[0])
            {
                case "grabar":
                    mmoff("Suc", "Grabación correcta.", 200);
                    bCambios = false;
                    desActivarGrabar();
                    if (bRegresar) {
                        AccionBotonera("regresar", "P");
                    }
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

function comprobarDatos() {
    if(parseInt($I("txtVigencia").value, 10) <= 1){
        mmoff("War", "El numero de días de vigencia para solicitudes aparcadas debe ser mayor de uno.", 480, 3000);
        return false;
    }
    if (parseInt($I("txtEliminacion").value, 10) < 1) {
        mmoff("War", "El numero de días de aviso de eliminación de solicitudes aparcadas debe ser mayor que cero.", 550, 3000);
        return false;
    } 
    if (parseInt($I("txtEliminacion").value, 10) >= parseInt($I("txtVigencia").value, 10)) {
        mmoff("War", "El numero de días de vigencia para solicitudes aparcadas debe ser mayor que el numero de días de aviso de eliminación de solicitudes aparcadas.", 430, 6000, 45);
        return false;
    }    
    return true;
}

function grabar(sValue) {
    try{
        if (!comprobarDatos()) return;
        mostrarProcesando(); 
        var js_args = "grabar@#@";
        var sDatos = $I("cboAnioAnt").value + "#sCad#" + $I("cboMesAnt").value + "#sCad#" + $I("cboSemana").value;
        sDatos += "#sCad#" + $I("txtVigencia").value + "#sCad#" + $I("txtEliminacion").value;
        js_args += sDatos;
        RealizarCallBack(js_args,"");
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los cambios.", e.message);
    }
}

function cargarCombos(){
    var existe = false;
    var dias = new Array("Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
    var newOpt = document.createElement("OPTION");
    for (var i = 1; i <= 31; i++) {
        if(i != 1) newOpt = document.createElement("OPTION");
        newOpt.text = i;
        newOpt.value = i;
        if ($I("hdnAnioAnt").value == i) newOpt.selected = true;
        $I("cboAnioAnt").options.add(newOpt, i);
    }
    newOpt = document.createElement("OPTION");
    for (var i = 1; i <= 31; i++) {
        if (i != 1) newOpt = document.createElement("OPTION");
        newOpt.text = i;
        newOpt.value = i;
        if ($I("hdnMesAnt").value == i) newOpt.selected = true;
        $I("cboMesAnt").options.add(newOpt, i);
    }
    newOpt = document.createElement("OPTION");
    for (var i = 0; i <7; i++) {
        if (i != 0) newOpt = document.createElement("OPTION");
        newOpt.text = dias[i];
        newOpt.value = i + 1;
        if ($I("hdnSemana").value == i+1) newOpt.selected = true;
        $I("cboSemana").options.add(newOpt, i);
    }
}
