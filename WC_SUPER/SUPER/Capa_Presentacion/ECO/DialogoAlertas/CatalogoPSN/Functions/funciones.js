var bLectura = false;
var bHayCambios = false;

function init(){
    try {
        if (!bNuevo) {
            setOp($I("btnNuevo"), 30);
        }
        getDialogos();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir(){
    var returnValue = (bHayCambios) ? "1" : null;
    modalDialog.Close(window, returnValue);	
}

function mdpsn(oNOBR){
    try{
        aceptarClick(oNOBR.parentNode.parentNode.rowIndex);
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle del proyectosubnodo", e.message);
	}
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]){
            case "getDialogos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                window.focus();
                break;
//            case "setPSN":
//                aceptarSalir();
//                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function getDialogos(){
    try{
        mostrarProcesando();
        var js_args = "getDialogos@#@";
        js_args += (($I("chkSoloAbiertos").checked) ? "1" : "0") + "@#@";
        js_args += idPSN + "@#@";
        js_args += sRestringirOCyFACerrados;
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

function getDialogoAlerta(nIdDialogo) {
    try {
        mostrarProcesando();
        nDialogoActivo = nIdDialogo;
        var strEnlace = strServer + "Capa_Presentacion/ECO/DialogoAlertas/Detalle/Default.aspx?id=" + codpar(nIdDialogo);
        //var ret = window.showModalDialog(strEnlace, self, sSize(930, 680));
        modalDialog.Show(strEnlace, self, sSize(930, 680))
	        .then(function(ret) {
                getDialogos();
                bHayCambios = true;
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar el diálogo.", e.message);
    }
}

function addDialogo() {
    try {
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/DialogoAlertas/Creacion/Default.aspx?idpsn=" + codpar(idPSN);
        //var ret = window.showModalDialog(strEnlace, self, sSize(520, 270));
        modalDialog.Show(strEnlace, self, sSize(520, 270))
	        .then(function(ret) {
                if (ret == "OK") {
                    bHayCambios = true;
                    getDialogos();
                }else
                    ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear el diálogo.", e.message);
    }
}
