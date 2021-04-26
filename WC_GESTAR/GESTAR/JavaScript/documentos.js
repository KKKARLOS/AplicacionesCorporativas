<!--
function nuevoDoc(sTipo, nItem){
    try{
        if (nItem=="")
        {
            alert("No puedes anexar ficheros primero tienes grabar");
            return;
        }
  	    var strEnlace = strServer + "Capa_Presentacion/Documentos/Subir.aspx?";
	    strEnlace += "sTipo="+sTipo;
	    strEnlace += "&nItem="+nItem;
	    strEnlace += "&sAccion=I";
	    
//	    var ret = window.showModalDialog(strEnlace, self, "dialogWidth:650px; dialogHeight:260px; center:yes; status:NO; help:NO;");
//		//alert(ret);
//	    if (ret != null){
//            if (ret == "OK") actualizarDocumentos(nItem);
//	    }

        modalDialog.Show(strEnlace, self, sSize(650,260))
        .then(function(ret) {
	        if (ret != null){
                if (ret == "OK") actualizarDocumentos(nItem);
	        }
	        ocultarProcesando();
        }); 	    
	    
	}catch(e){
	    var strTitulo = "Error al añadir documentos";
		mostrarErrorAplicacion(strTitulo, e.message);
	}
}

function actualizarDocumentos(nItem){
    try{
        var js_args = "documentos@@";
        js_args += nItem;
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
	}catch(e){
	    var strTitulo = "Error al actualizar los documentos";
		mostrarErrorAplicacion(strTitulo, e.message);
	}
}

function eliminarDoc(){
    try{
        if (nfs == 0){
            alert("Debe seleccionar los archivos a eliminar");
            return;
        }
        jqConfirm("", "Esta acción eliminará de base de datos los documentos seleccionados.<br /><br />¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                eliminarDoc2();
            }
        });
    }catch(e){
	    var strTitulo = "Error al eliminar los documentos";
		mostrarErrorAplicacion(strTitulo, e.message);
	}
}
function eliminarDoc2(){
    try{
        var js_args = "elimdocs@@";

        var aFila = FilasDe("tblDocumentos");
        //Comprobación previa.
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("sAutor") != sNumEmpleado){
                    alert("¡Denegado!\n\nHa seleccionado eliminar documentos que no son de su propiedad.\n\nDicha acción no está permitida.");
                    return;
                }
            }
        }
        
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("sAutor") == sNumEmpleado){
                    js_args += aFila[i].id +"##";
                    aFila[i].className = "FI";
                }else{
                    mmoff("War", "¡Denegado!\n\nHa seleccionado eliminar documentos que no son de su propiedad.\n\nDicha acción no está permitida.", 420,2500);
                    return;
                }
            }
        }
        aFila = null;
        
        js_args = js_args.substring(0, js_args.length-2);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
    }catch(e){
        var strTitulo = "Error al eliminar los documentos";
        mostrarErrorAplicacion(strTitulo, e.message);
    }
}

function descargar(sTipo, nIDDoc){
    try{
        //alert("Descargar documento de tipo: "+ sTipo +", nº: "+ nIDDoc);
	    var strEnlace = strServer + "Capa_Presentacion/Documentos/Descargar.aspx?";
	    strEnlace += "sTipo="+sTipo;
	    strEnlace += "&nIDDOC="+nIDDoc;

        mostrarProcesando();
        $I("iFrmSubida").src = strEnlace;
        setTimeout("ocultarProcesando();",5000);
	}catch(e){
	    var strTitulo = "Error al descargar el documento";
		mostrarErrorAplicacion(strTitulo, e.message);
	}
}

function modificarDoc(sTipo, nIDDoc){
    try{
        //alert("Modificar documento de tipo: "+ sTipo +", nº: "+ nIDDoc);
  	    var strEnlace = strServer + "Capa_Presentacion/Documentos/Subir.aspx?";
	    strEnlace += "sTipo="+sTipo;
	    switch (sTipo){
	        case "A":
        	    strEnlace += "&nItem="+$I("hdnIDArea").value;
        	    break;
	        case "D":
        	    strEnlace += "&nItem="+$I("hdnIDDefi").value;
        	    break;
	    }
	    strEnlace += "&sAccion=U";
	    strEnlace += "&nIDDOC="+ nIDDoc;
	    
	    mostrarProcesando();
//	    var ret = window.showModalDialog(strEnlace, self, "dialogWidth:650px; dialogHeight:260px; center:yes; status:NO; help:NO;");
//		ocultarProcesando();
//	    if (ret != null){
//            if (ret == "OK"){
//	            switch (sTipo){
//	                case "A":
//        	            actualizarDocumentos($I("hdnIDArea").value);
//        	            break;
//	                case "D":
//        	            actualizarDocumentos($I("hdnIDDefi").value);
//        	            break;
//	            }
//                
//            }
//	    }
	    
        modalDialog.Show(strEnlace, self, sSize(650,260))
        .then(function(ret) {
	        if (ret != null){
                if (ret == "OK"){
	                switch (sTipo){
	                    case "A":
        	                actualizarDocumentos($I("hdnIDArea").value);
        	                break;
	                    case "D":
        	                actualizarDocumentos($I("hdnIDDefi").value);
        	                break;
	                }                
                }
	        }
            ocultarProcesando();
        }); 	    
	    
	}catch(e){
	    var strTitulo = "Error al modificar documentos";
		mostrarErrorAplicacion(strTitulo, e.message);
	}
}
-->