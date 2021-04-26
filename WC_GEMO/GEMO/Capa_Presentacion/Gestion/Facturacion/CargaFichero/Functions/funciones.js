function init(){
    try{
        
        ocultarProcesando();
        setOp($I("btnCronologia"), 100);
                       
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function HabCarga(){
    try{
        mmoff("Inf","Se necesita pulsar el botón cargar para enviar el fichero al servidor.", 600);
        //$I("panel").style.visibility='visible';
        setOp($I("btnSubir"), 100);
    }
	catch(e){
		mostrarErrorAplicacion("Error al habilitar los botones", e.message);
    }
}

function getCronologia()
{
    try{
		var strEnlace ="../Cronologia/default.aspx"; 
//	    window.showModalDialog(strEnlace, self, "dialogwidth:650px; dialogheight:480px; center:yes; status:NO; help:NO;");
		modalDialog.Show(strEnlace, self, sSize(650, 480))
	        .then(function(ret) {
	        });  

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la pantalla de cronologías de ficheros de facturación.", e.message);
    }
}


function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "procesar":
                AccionBotonera("procesar", "D");

                ocultarProcesando();
                
                alert("Proceso finalizado correctamente");
                
                break;
           default:
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
        }
        ocultarProcesando();
    }
}
function LeerFichero(){
    try{
        mostrarProcesando();
        document.forms[0].submit();
	}catch(e){
		mostrarErrorAplicacion("Error al validar el fichero de carga", e.message);
    }
}
