function init(){
    try{
        if (!mostrarErrores()) return;

        ocultarProcesando();
        //setOp($I("btnCancelar"), 30);
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
//            case "tareasPlant":
//                var nIdPlant=insertarPlantilla(aResul[2]);
//                //con el codigo de plantilla accedo al array y genero el Html de la tabla de estructura
//                $I("divTareas").children[0].innerHTML=generarHtmlPlantilla(nIdPlant, gsTipo);
//                insertarPlantillaHito(aResul[3]);
//                $I("divHitos").children[0].innerHTML=generarHtmlPlantillaHito(nIdPlant, gsTipo);
//                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

//function mostrarPlantilla(nIdPlant){
//    var sHtml="";
//    try{
//        //Si la plantilla está cargada la leo del array
//        //Sino cargo el array y la leo del array
//        var objRec = buscarPlantillaEnArray(nIdPlant);
//        if (objRec == null){ //No existe en el array
//            var js_args = "tareasPlant@#@"+nIdPlant +"@#@"+gsTipo +"@#@";//genera una cadena para guardar en el array de plantillas
//            mostrarProcesando();
//            RealizarCallBack(js_args, "");  //con argumentos
//        }
//        else{
//            //con el codigo de plantilla accedo al array y genero el Html de la tabla de estructura
//            sHtml=generarHtmlPlantilla(nIdPlant, gsTipo);
//            $I("divTareas").children[0].innerHTML=sHtml;
//            sHtml=generarHtmlPlantillaHito(nIdPlant, gsTipo);
//            $I("divHitos").children[0].innerHTML=sHtml;
//        }
//        return;
//	}catch(e){
//		mostrarErrorAplicacion("Error al obtener la estructura de la plantilla", e.message);
//    }
//}



function addEscenario() {
    try {
        //alert("addEscenario");
        mostrarProcesando();
        $I("hdnIdProyectoSubNodo").value = "9337";
        var strEnlace = strServer + "Capa_Presentacion/ECO/Escenarios/Cabecera/Default.aspx?ip=" + codpar($I("hdnIdProyectoSubNodo").value) + "&ie=" + codpar("-1");
        //var ret = window.showModalDialog(strEnlace, self, sSize(950, 650));
        modalDialog.Show(strEnlace, self, sSize(950, 650))
	        .then(function(ret) {
                //alert(ret);
                if (ret != null) {
                    //setNodo(ret, true);
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un nuevo escenario.", e.message);
    }
}
function cloneEscenario() {
    try {
        alert("cloneEscenario");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a clonar un escenario.", e.message);
    }
}
function delEscenario() {
    try {
        alert("delEscenario");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a eliminar un escenario.", e.message);
    }
}

