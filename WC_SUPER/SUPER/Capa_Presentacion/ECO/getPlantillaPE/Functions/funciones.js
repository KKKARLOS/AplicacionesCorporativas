function init(){
    try{
        if (!mostrarErrores()) return;
        
        if (opener.$I("hdnIDPlantilla") != null){
            if (opener.$I("hdnIDPlantilla").value != ""){
                //$I(opener.$I("hdnIDPlantilla").value).click();
                seleccionar($I(opener.$I("hdnIDPlantilla").value));
            }
        }
        ocultarProcesando();
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
            case "tareasPlant":
                var nIdPlant=insertarPlantilla(aResul[2]);
                //con el codigo de plantilla accedo al array y genero el Html de la tabla de estructura
                $I("divTareas").children[0].innerHTML=generarHtmlPlantilla(nIdPlant, gsTipo);
                insertarPlantillaHito(aResul[3]);
                $I("divHitos").children[0].innerHTML=generarHtmlPlantillaHito(nIdPlant, gsTipo);
                break;
        }
        ocultarProcesando();
    }
}
function limpiar(){
    try{
        $I("divTareas").children[0].innerHTML="";
        $I("divHitos").children[0].innerHTML="";
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar", e.message);
    }
}
function mostrarPlantilla(nIdPlant){
    var sHtml="";
    try{
        //Si la plantilla está cargada la leo del array
        //Sino cargo el array y la leo del array
        var objRec = buscarPlantillaEnArray(nIdPlant);
        if (objRec == null){ //No existe en el array
            var js_args = "tareasPlant@#@"+nIdPlant +"@#@"+gsTipo +"@#@";//genera una cadena para guardar en el array de plantillas
            mostrarProcesando();
            RealizarCallBack(js_args, "");  //con argumentos
        }
        else{
            //con el codigo de plantilla accedo al array y genero el Html de la tabla de estructura
            sHtml=generarHtmlPlantilla(nIdPlant, gsTipo);
            $I("divTareas").children[0].innerHTML=sHtml;
            sHtml=generarHtmlPlantillaHito(nIdPlant, gsTipo);
            $I("divHitos").children[0].innerHTML=sHtml;
        }
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la estructura de la plantilla", e.message);
    }
}
function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;
        
        var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].cells[1].innerText;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}
function cargarPlantilla(){
    var sTareas="";

    var iNumTareas=0;
    var aTareas = FilasDe("tblTareas");
    iNumTareas=aTareas.length;
    if (iNumTareas==0){
        mmoff("Inf", "Acción no permitida.\nLa plantilla no contiene estructura.",290);
        return;
    }
    else{
        mostrarProcesando();
        for (var i = 0; i < aTareas.length; i++){
            sTareas += aTareas[i].getAttribute("tipo") + "@#@" + aTareas[i].cells[1].innerText + "@#@" +
                        aTareas[i].getAttribute("margen") + "@#@" + aTareas[i].id + "@#@" + aTareas[i].getAttribute("sFact") + "##";
        }
    }
    //Añado los hitos especiales
    var aHitos = FilasDe("tblHitos");
    iNumTareas=aHitos.length;
    if (iNumTareas>0){
        for (var i = 0; i < aHitos.length; i++){
            sTareas += "HC@#@" + aHitos[i].cells[1].innerText + "@#@" + "0@#@" + aHitos[i].id + "##";
        }
    }
    
    var sMsg = "";
    if (gsTipo == "T")
        sMsg = "Si pulsas \"Aceptar\" se sustituirá toda la estructura que hubiera dependiente del proyecto técnico seleccionado, por la estructura de la plantilla seleccionada sin posibilidad de vuelta atrás.";
    else
        sMsg = "Si pulsas \"Aceptar\" se sustituirá toda la estructura que hubiera dependiente del proyecto seleccionado, por la estructura de la plantilla seleccionada sin posibilidad de vuelta atrás.";
    ocultarProcesando();
    jqConfirm("", sMsg, "", "", "war", 450).then(function (answer) {
        if (answer)
        {
            var returnValue = sTareas; //Devuelvo la lista de tipo y descripción de elementos
            modalDialog.Close(window, returnValue);
        }
        else ocultarProcesando();
    });
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
