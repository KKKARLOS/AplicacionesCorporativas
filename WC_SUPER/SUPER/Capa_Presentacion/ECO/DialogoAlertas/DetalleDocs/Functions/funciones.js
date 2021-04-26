function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        gsDocModAcc=$I("hdnModoAcceso").value;
        //gsDocEstPry=$I("hdnEstado").value;
        setEstadoBotonesDoc(gsDocModAcc, "A"); //gsDocEstPry
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "documentos":
		        document.getElementById("divCatalogoDoc").children[0].innerHTML = aResul[2];
		        if (!document.all)
		            $I("divCatalogoDoc").children[0].style.backgroundImage = "url(../../../../Images/imgFT20.gif)";
		        setEstadoBotonesDoc(aResul[3], "A");//aResul[4]
                ocultarProcesando();
                nfs = 0;
                break;

            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                var tblDocumentos = $I("tblDocumentos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].className == "FI")
                        tblDocumentos.deleteRow(i);
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
    try {
        nuevoDoc("DI", $I("hdnIdDialogo").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir un documento.", e.message);
    }
} 
function eliminarDoc1(){
    if ($I("hdnModoAcceso").value=="R") return;
    eliminarDoc();
}


var tsPestanas = null;
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanasGen;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}

function cerrarVentana() {
    var returnValue = tblDocumentos.rows.length;
    modalDialog.Close(window, returnValue);	    
}
