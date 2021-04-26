var IDActivo = "0-0-0-0-0-0";

function init(){
    try{

	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function mdn(oFila){
    try
    {
        //alert("Nivel: " + oFila.nivel +", ID completo: " + oFila.id +", ID nodo: " + oFila.id.split("-")[oFila.nivel-1]);
        var aDatos = oFila.id.split("-");
        mostrarProcesando();
        
        var ret = null;
        //if (oFila.nivel < 5){
        switch (parseInt(oFila.getAttribute("nivel"), 10)){
            case 1:
            case 2:
            case 3:
            case 4:
                var strEnlaceSN = strServer + "Capa_Presentacion/Administracion/DetalleSN/Default.aspx?nNivel=" + codpar(oFila.getAttribute("nivel"));
                strEnlaceSN += "&SN4="+ codpar(aDatos[0]);
                strEnlaceSN += "&SN3="+ codpar(aDatos[1]);
                strEnlaceSN += "&SN2="+ codpar(aDatos[2]);
                strEnlaceSN += "&SN1="+ codpar(aDatos[3]);
                strEnlaceSN += "&Nodo="+ codpar(aDatos[4]);
                strEnlaceSN += "&ID="+ codpar(aDatos[oFila.getAttribute("nivel")-1]);
                strEnlaceSN += "&origen=" + codpar("MantFiguras");
                //ret = window.showModalDialog(strEnlaceSN, self, sSize(990, 550));
                modalDialog.Show(strEnlaceSN, self, sSize(990, 550))
	                .then(function(ret) {
                        ocultarProcesando();
	                }); 
                break;
            case 5:
                //Victor dice 10/04/2015 no enlazar como estaba hasta ahora
                location.href = strServer + "Capa_Presentacion/Ayuda/Obras/Default.aspx";
                return;

                var strEnlaceNodo = strServer + "Capa_Presentacion/Administracion/DetalleNodo/Default.aspx?SN4=" + codpar(aDatos[0]);
                strEnlaceNodo += "&SN3="+ codpar(aDatos[1]);
                strEnlaceNodo += "&SN2="+ codpar(aDatos[2]);
                strEnlaceNodo += "&SN1="+ codpar(aDatos[3]);
                strEnlaceNodo += "&ID="+ codpar(aDatos[oFila.getAttribute("nivel")-1]);
                strEnlaceNodo += "&origen=" + codpar("MantFiguras");
                //ret = window.showModalDialog(strEnlaceNodo, self, sSize(990, 605));
                modalDialog.Show(strEnlaceNodo, self, sSize(990, 605))
	                .then(function(ret) {
                        ocultarProcesando();
	                });                 
                break;
            case 6:
                var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/DetalleSubNodo/Default.aspx?SN4=" + codpar(aDatos[0]);
                strEnlaceSubNodo += "&SN3="+ codpar(aDatos[1]);
                strEnlaceSubNodo += "&SN2="+ codpar(aDatos[2]);
                strEnlaceSubNodo += "&SN1="+ codpar(aDatos[3]);
                strEnlaceSubNodo += "&Nodo="+ codpar(aDatos[4]);
                strEnlaceSubNodo += "&ID="+ codpar(aDatos[oFila.getAttribute("nivel")-1]);
                strEnlaceSubNodo += "&origen=" + codpar("MantFiguras");
                //ret = window.showModalDialog(strEnlaceSubNodo, self, sSize(990, 550));
                modalDialog.Show(strEnlaceSubNodo, self, sSize(990, 550))
	                .then(function(ret) {
                        ocultarProcesando();
	                });                 
                break;
        }
	    //alert(ret);
        
	}catch(e){
		mostrarErrorAplicacion("Error ", e.message);
    }
}


/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar el item seleccionado,\nya que existen elementos con los que está relacionado.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getEstructura":
                $I("divCatalogo").innerHTML = aResul[2];
                IDActivo = "0-0-0-0-0-0";
                AccionBotonera("eliminar", "D");
                break;
            case "eliminar":
                setTimeout("MostrarInactivos();", 50);
                break;        
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

