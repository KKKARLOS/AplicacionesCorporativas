function init()
{
    var aFilas = FilasDe("tblDatos");
    for (var i = 0; i < aFilas.length; i++) {
        if (aFilas[i].id == $I("hdnFecAnnoMesActual").value) 
        {
            $I("divCatalogo").scrollTop = aFilas[i].rowIndex * 20;
            break;
        }
    }    
    
    //$I("divCatalogo").scrollTop = iFilaNueva * 16;      
	actualizarLupas("tblAsignados", "tblDatos");
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                bCambios = false;
                aFila = FilasDe("tblDatos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "U") {
                        mfa(aFila[i], "N");
                        continue;
                    } 
                }
                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc","Grabación correcta", 160);
                break;
                 
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar(){
    try{
        var js_args = "grabar@#@";

        var sb = new StringBuilder;
        for (var i=0; i<$I("tblDatos").rows.length;i++){
            if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                sb.Append($I("tblDatos").rows[i].getAttribute("bd") + "##"); //0
                sb.Append($I("tblDatos").rows[i].id + "##"); //1
                sb.Append($I("tblDatos").rows[i].cells[1].children[0].value); //2
                sb.Append("///");
            }
        }
        js_args += sb.ToString();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la fecha límite de respuesta", e.message);
    }
}
function aG(obj) {
    try {
        mfa(obj.parentNode.parentNode, "U");
        activarGrabar();
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la fecha límite de respuesta", e.message);
    }
}
