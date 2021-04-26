var bRegresar = false;

function init() {
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Oficinas de liquidación";
        ocultarProcesando();
        desActivarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
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
                } else
                    desActivarCombos();
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

function flGetOficinas() {
    /*Recorre la tblMotivos para obtener una cadena que se pasará como parámetro
    al procedimiento de grabación*/
    var sRes = "";
    try {
        aFila = FilasDe("tblOficinas")
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                sRes += aFila[i].id + "#sCad#";
                sRes += aFila[i].getAttribute("idOfLiq") + "#sFin#";
            }
        }
        if (sRes != "") sRes = sRes.substring(0, sRes.length - 6);
        return sRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la cadena de grabación.", e.message);
    }
}


function grabar(){
    try{     
        js_args = "grabar@#@";
        js_args += flGetOficinas();

        mostrarProcesando();
        desActivarGrabar();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos.", e.message);
		return false;
    }
}

function activarCombo(id) {
    try {
        var aFila = FilasDe("tblOficinas");
        for (var i = 0, nCount = aFila.length; i < nCount; i++) {
            if (aFila[i].id == id) {
                $I("cboOfiLiq" + (id)).style.visibility = "visible";
                $I("txtOfiLiq" + (id)).style.width = "0px";
                $I("cboOfiLiq" + (id)).style.width = "280px";
            }
            else {
                $I("cboOfiLiq" + (aFila[i].id)).style.visibility = "hidden";
                $I("cboOfiLiq" + (aFila[i].id)).style.width = "0px";
                $I("txtOfiLiq" + (aFila[i].id)).style.width = "210px";
                $I("txtOfiLiq" + (aFila[i].id)).style.visibility = "visible";
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el combo de oficinas liquidadoras.", e.message);
    }
}

function desActivarCombos() {
    try {
        var aFila = FilasDe("tblOficinas");
        for (var i = 0, nCount = aFila.length; i < nCount; i++) {
            $I("cboOfiLiq" + (aFila[i].id)).style.visibility = "hidden";
            $I("cboOfiLiq" + (aFila[i].id)).style.width = "0px";
            $I("txtOfiLiq" + (aFila[i].id)).style.width = "210px";
            $I("txtOfiLiq" + (aFila[i].id)).style.visibility = "visible";
            mfa(aFila[i], "N");
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error desactivar los combos de las oficinas liquidadoras.", e.message);
    }
}

function actualizarCampos(oFila, oElemento) {
    try {
        $I("txtOfiLiq" + oFila.id).value = oElemento[oElemento.selectedIndex].innerText;
        oFila.setAttribute("idOfLiq", oElemento.value);
        mfa(oFila, "U");
        return;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al actualizar el campo seleccionado.", e.message);
    }
}
