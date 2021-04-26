var aFila;

function init(){
    try{
        aFila = FilasDe("tblDatos");
        aFila[1].cells[0].style.backgroundColor="#83afc3";
        if (parseInt(aFila[1].getAttribute("iPropias"), 10) > parseInt(aFila[1].getAttribute("iAjenas"), 10)) {
            $I("imgCorP").src = "../../../Images/imgCorazonR2.gif";
            $I("imgCorA").src = "../../../Images/imgCorazonA1.gif";
        } else if (parseInt(aFila[1].getAttribute("iPropias"), 10) == parseInt(aFila[1].getAttribute("iAjenas"), 10)) {
            $I("imgCorP").src = "../../../Images/imgCorazonR1.gif";
            $I("imgCorA").src = "../../../Images/imgCorazonA1.gif";
        }else{
            $I("imgCorP").src = "../../../Images/imgCorazonR1.gif";
            $I("imgCorA").src = "../../../Images/imgCorazonA2.gif";
        }
        if (parseInt(aFila[1].getAttribute("iPropias"), 10) == 0) $I("imgCorP").src = "../../../Images/imgCorazonR0.gif";
        if (parseInt(aFila[1].getAttribute("iAjenas"), 10) == 0) $I("imgCorA").src = "../../../Images/imgCorazonA0.gif";
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
            case "buscar":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo2").children[0].innerHTML = aResul[3];
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function buscar(nAnoMes){
   var js_args="";
   try{	 
        var js_args = "buscar@#@"+nAnoMes;  
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        
        for (var i=0;i<aFila.length;i++){
	         if (aFila[i].id == nAnoMes){
                aFila[i].cells[0].style.backgroundColor="#83afc3";
                if (parseInt(aFila[i].getAttribute("iPropias"), 10) > parseInt(aFila[i].getAttribute("iAjenas"), 10)) {
                    $I("imgCorP").src = "../../../Images/imgCorazonR2.gif";
                    $I("imgCorA").src = "../../../Images/imgCorazonA1.gif";
                } else if (parseInt(aFila[i].getAttribute("iPropias"), 10) == parseInt(aFila[i].getAttribute("iAjenas"), 10)) {
                    $I("imgCorP").src = "../../../Images/imgCorazonR1.gif";
                    $I("imgCorA").src = "../../../Images/imgCorazonA1.gif";
                }else{
                    $I("imgCorP").src = "../../../Images/imgCorazonR1.gif";
                    $I("imgCorA").src = "../../../Images/imgCorazonA2.gif";
                }
                if (parseInt(aFila[i].getAttribute("iPropias"), 10) == 0) $I("imgCorP").src = "../../../Images/imgCorazonR0.gif";
                if (parseInt(aFila[i].getAttribute("iAjenas"), 10) == 0) $I("imgCorA").src = "../../../Images/imgCorazonA0.gif";
             }else
                aFila[i].cells[0].style.backgroundColor="";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos", e.message);
    }
}
