var aFila;

function init(){
    try{
        //document.body.style.cursor = 'pointer';
        //getMes($I("hdnMesAct").value);
        getMes("INI");
    } catch (e) {
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
		    case "getmes":
		        nPropias = parseInt(aResul[2]);
		        $I("divCatalogo").children[0].innerHTML = aResul[3];
		        nAjenas = parseInt(aResul[4]);
		        $I("divCatalogo2").children[0].innerHTML = aResul[5];
		        setCorazones();
		        break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function setCorazones(){
   try{	 
        if (nPropias > nAjenas){
            $I("imgCorP").src = strServer + "Images/imgCorazonR2.gif";
            $I("imgCorA").src = strServer + "Images/imgCorazonA1.gif";
        }else if (nPropias == nAjenas){
            $I("imgCorP").src = strServer + "Images/imgCorazonR1.gif";
            $I("imgCorA").src = strServer + "Images/imgCorazonA1.gif";
        }else{
            $I("imgCorP").src = strServer + "Images/imgCorazonR1.gif";
            $I("imgCorA").src = strServer + "Images/imgCorazonA2.gif";
        }
        if (nPropias == 0) $I("imgCorP").src = strServer + "Images/imgCorazonR0.gif";
        if (nAjenas == 0) $I("imgCorA").src = strServer + "Images/imgCorazonA0.gif";
	}catch(e){
		mostrarErrorAplicacion("Error al establecer los corazones", e.message);
    }
}


function getMes(sAnoMes) {
    try {
        if (sAnoMes == $I("hdnMesAct").value)
            return;
        if (sAnoMes == "INI")
            sAnoMes = $I("hdnMesAct").value;
        $I("hdnMesAct").value = sAnoMes;
//        $I("lblPropias").innerText = "Detalle " + AnoMesToMesAnoDescLong(nAnoMes);
//        $I("lblAjenas").innerText = "Detalle " + AnoMesToMesAnoDescLong(nAnoMes);
        $I("lblPropias").innerText = "Detalle " + sAnoMes;
        $I("lblAjenas").innerText = "Detalle " + sAnoMes;

        var js_args = "getmes@#@" + sAnoMes;
        mostrarProcesando();
        RealizarCallBack(js_args, "");

        setCorazones();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del mes", e.message);
    }
}
function ponerMano() {
    //alert("hola");
    document.body.style.cursor = 'pointer';
} 
function quitarMano() {
    document.body.style.cursor = 'default';
    //$I("Chart1").style.cursor = "default";
}