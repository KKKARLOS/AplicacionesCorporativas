function init(){
    try{
        if (!mostrarErrores()) return;

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

function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;
        
        var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].cells[1].innerText;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
