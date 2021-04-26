function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        gsDocModAcc=$I("hdnModoAcceso").value;
        gsDocEstPry=$I("hdnEstProy").value;
        setEstadoBotonesDoc(gsDocModAcc, gsDocEstPry);
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function salir() {
    modalDialog.Close(window, null);
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
            case "documentos":
		        $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                setEstadoBotonesDoc(aResul[3], aResul[4]);
                ocultarProcesando();
                nfs = 0;
                break;
                
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
                }
                aFila = null;
                nfs = 0;
                ocultarProcesando();
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function nuevoDoc1(){
    var sIdPSN=$I('hdnIdProyectoSubNodo').value;
    
    if ((sIdPSN=="")||(sIdPSN=="0")){
        mmoff("Inf", "El proyecto económico debe estar grabado para poder asociarle documentación", 380);
    }
    else{
        nuevoDoc("PSN", sIdPSN);
    }
} 
function eliminarDoc1(){
    if ($I("hdnModoAcceso").value=="R")return;
    eliminarDoc();
} 
