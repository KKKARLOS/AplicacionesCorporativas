var sRecargar = 'N';
var bHayCambios = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload(){

}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}

function salir() {
    var returnValue = (sRecargar == 'S') ? sRecargar : null;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                returnValue = "S";
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }

        });
    }
    else modalDialog.Close(window, returnValue);
}
function aG(){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            bCambios = true;
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
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
                sRecargar='S';
                bCambios = false;
                bNueva = 'false';
                bHayCambios = true;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);                
                $I("hdnID").value = aResul[2];
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) setTimeout("salir();", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("txtConsumos").value=="")
        {
            $I("txtConsumos").focus();
            mmoff("War","Se debe indicar la descripcion del consumo",340);            
            return false;
        }    
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        //if (!comprobarDatos()) return;

        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        
  		if (bNueva=='false') sb.Append("0@#@");
    	else sb.Append("1@#@");                                   //0 0=Update 1=Insert      
        sb.Append($I("hdnID").value +"@#@");                       //1 ID Agenda     
       
        sb.Append(Utilidades.escape($I("txtConsumos").value) +"@#@");         //2 Consumos
        sb.Append(Utilidades.escape($I("txtProduccion").value) +"@#@");       //3 Producción
        sb.Append(Utilidades.escape($I("txtFacturacion").value) +"@#@");      //4 Facturación
        sb.Append(Utilidades.escape($I("txtOtros").value) +"@#@");            //5 Otros        
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), ""); 
    }
    catch (e) {
		mostrarErrorAplicacion("Error al grabar los datos de la agenda", e.message);
    }
}

function detalle(sId){
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/EspacioComunicacion/Detalle/Default.aspx?ID=" + sId + "&bNueva=false&nProy=" + dfn(fOpener().$I("txtNumPE").value);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(745, 475));
	    modalDialog.Show(strEnlace, self, sSize(745, 475))
	        .then(function(ret) {
                if (ret != null) ObtenerDatos();
                try { window.event.cancelBubble = true; } catch (e) { };
                ocultarProcesando(); 
	        });                    
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}
