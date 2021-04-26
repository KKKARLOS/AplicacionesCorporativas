function init(){
    try{
        if (!mostrarErrores()) return;
        
        if (bEs_superadministrador){
            $I("lblAudit").style.visibility="visible";
            $I("chkAudit").style.visibility="visible";
        }
        if ($I("candSUPER").src.indexOf("Cerrado") > -1){
            setOp($I("imgSUPER"), 30);
            $I("imgSUPER").style.cursor = "default";
        }
        if ($I("candPGE").src.indexOf("Cerrado") > -1){
            setOp($I("imgPGE"), 30);
            $I("imgPGE").style.cursor = "default";
        }
        if ($I("candPST").src.indexOf("Cerrado") > -1){
            setOp($I("imgPST"), 30);
            $I("imgPST").style.cursor = "default";
        }
        if ($I("candIAP").src.indexOf("Cerrado") > -1){
            setOp($I("imgIAP"), 30);
            $I("imgIAP").style.cursor = "default";
        }
        if ($I("candADP").src.indexOf("Cerrado") > -1){
            setOp($I("imgADP"), 30);
            $I("imgADP").style.cursor = "default";
        }
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function abrirCerrar(sAcceso){
    try{ 
        var nOp = getOp($I("img"+sAcceso));
        if (nOp == 100){
            $I("cand"+ sAcceso).src = "../../../images/icoCerradoG.gif";
            setOp($I("img"+sAcceso), 30);
        }else{
            $I("cand"+ sAcceso).src = "../../../images/icoAbiertoG.gif";
            setOp($I("img"+sAcceso), 100);
        }
        $I("img"+ sAcceso).style.cursor = "default";
    
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error en la función abrirCerrar.", e.message);
    }
}

function grabar(){
    try{
       
        mostrarProcesando();       
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        sb.Append(Utilidades.escape($I('txtMotivo').value) + "@#@");   
        sb.Append(($I("chkAudit").checked==true)? "1@#@" : "0@#@");      
        sb.Append(($I("candSUPER").src.indexOf("Abierto") > -1)? "1":"0");
        sb.Append("@#@");
        sb.Append(($I("candIAP").src.indexOf("Abierto") > -1)? "1":"0");
        sb.Append("@#@");
        sb.Append(($I("candPST").src.indexOf("Abierto") > -1)? "1":"0");
        sb.Append("@#@");
        sb.Append(($I("candPGE").src.indexOf("Abierto") > -1)? "1":"0");
        sb.Append("@#@");
        sb.Append(($I("candADP").src.indexOf("Abierto") > -1)? "1":"0");
       
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
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
                bCambios = false;
                desActivarGrabar();
                mmoff("Suc","Grabación correcta", 160);
                break;                
        }
        
        ocultarProcesando();
    }
}

