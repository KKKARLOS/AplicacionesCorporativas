<!--
var gsDocModAcc="";//Modo de accceso al proyecto R->lectura W->escritura
var gsDocEstPry="";//Estado del proyecto C->cerrado, H->historico, A->abierto, P->presupuestado
var token = null;
var fileDownloadCheckTimer;

function nuevoDoc(sTipo, nItem, sOr){
    try{
        var sOrigen = (sOr) ? sOr : "";
  	    var strEnlace = strServer + "Capa_Presentacion/Documentos/Subir.aspx?";
	    strEnlace += "sTipo="+ sTipo;
	    strEnlace += "&nItem="+ nItem;
	    strEnlace += "&sAccion=I";
	    strEnlace += "&sOrigen="+ sOrigen;
	    
//	    var ret = window.showModalDialog(strEnlace, self, sSize(650, 260));
//	    window.focus();
//		//alert(ret);
//	    if (ret != null){
//            if (ret == "OK") actualizarDocumentos(nItem, sTipo);
//	    }
	    
        modalDialog.Show(strEnlace, self, sSize(650, 260))
            .then(function(ret) {
	            if (ret != null){
                    if (ret == "OK") actualizarDocumentos(nItem, sTipo);
	            }
            }); 	    
	    
	}catch(e){
		mostrarErrorAplicacion("Error al añadir documentos", e.message);
	}
}

function actualizarDocumentos(nItem, sTipo){
    try{
        var js_args = "documentos@#@";
        js_args += nItem +"@#@";
        js_args += sTipo +"@#@";
        js_args += gsDocModAcc +"@#@";
        js_args += gsDocEstPry;
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los documentos", e.message);
	}
}

function eliminarDoc(){
    try{
        if (nfs == 0){
            alert("Debe seleccionar los archivos a eliminar");
            return;
        }
        
        if (!confirm("¡Atención!\n\nEsta acción eliminará de base de datos los documentos seleccionados.\n\n¿Desea continuar?")) return;
        
        var sTipo="";
        var js_args = "elimdocs@#@";

        var aFila = FilasDe("tblDocumentos");
        //Comprobación previa.
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                //if (aFila[i].sAutor != sNumEmpleado && sPerfil != "A"){
                if (aFila[i].sAutor != sNumEmpleado && es_administrador != "A" && es_administrador != "SA"){
                    alert("¡Denegado!\n\nHa seleccionado eliminar documentos que no son de su propiedad.\n\nDicha acción no está permitida.");
                    return;
                }
            }
        }
        var sw=0;
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                //if (aFila[i].sAutor == sNumEmpleado || sPerfil == "A"){
                if (aFila[i].sAutor == sNumEmpleado || es_administrador == "A" || es_administrador == "SA"){
                    sTipo = aFila[i].sTipo;
                    js_args += aFila[i].id +"##";
                    aFila[i].className = "FI";
                    sw=1;
                }else{
                    alert("¡Denegado!\n\nHa seleccionado eliminar documentos que no son de su propiedad.\n\nDicha acción no está permitida.");
                    return;
                }
            }
        }
        aFila = null;
        
        if (sw==1) js_args = js_args.substring(0, js_args.length-2);
        js_args += "@#@"+ sTipo
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los documentos", e.message);
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
		mostrarErrorAplicacion("Error al descargar el documento", e.message);
	}
}

function initDownload() {
    fileDownloadCheckTimer = window.setInterval(function () {
          var cookieValue = getCookie('fileDownloadToken');
          if (cookieValue == token)
            finishDownload();
        }, 1000);
}
  
function finishDownload() {
     window.clearInterval(fileDownloadCheckTimer);
     expireCookie('fileDownloadToken');
     ocultarProcesando();
}  

function getCookie( name ) {
    var parts = document.cookie.split(name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}

function expireCookie( cName ) {
    document.cookie = encodeURIComponent( cName ) + "=deleted; expires=" + new Date( 0 ).toUTCString();
}

function modificarDoc(sTipo, nIDDoc, sOr){
    try{
        if (bLectura) return;
        //alert("Modificar documento de tipo: "+ sTipo +", nº: "+ nIDDoc);
        var sOrigen = (sOr) ? sOr : "";
  	    var strEnlace = strServer + "Capa_Presentacion/Documentos/Subir.aspx?";
	    strEnlace += "sTipo="+sTipo;
	    switch (sTipo){
	        case "AS":
	        case "AS_PE":
	        case "AS_PT":
	        case "AS_T":
        	    strEnlace += "&nItem="+$I("txtIdAsunto").value;
        	    break;
	        case "AC":
	        case "AC_PE":
	        case "AC_PT":
	        case "AC_T":
        	    strEnlace += "&nItem="+$I("txtIdAccion").value;
        	    break;
	        case "IAP_T":
	        case "T":
        	    strEnlace += "&nItem="+$I("hdnIdTarea").value;
        	    break;
	        case "A":
        	    strEnlace += "&nItem="+$I("hdnIDActividad").value;
        	    break;
	        case "F":
        	    strEnlace += "&nItem="+$I("hdnIDFase").value;
        	    break;
	        case "PT":
        	    strEnlace += "&nItem="+$I("hdnIDPT").value;
        	    break;
//	        case "PE":
//        	    strEnlace += "&nItem="+$I("hdnIDPE").value+"/"+$I("hdnIDCR").value;
//        	    break;
	        case "PE":
	        case "PSN":
        	    strEnlace += "&nItem="+$I("hdnIdProyectoSubNodo").value;
        	    break;
            case "HT": //Hito lineal
            case "HM": //Hito discontinuo
	        case "HF": //Hito de fecha
        	    strEnlace += "&nItem="+$I("txtIdHito").value.replace('.','');
        	    break;
	        case "OF":
        	    strEnlace += "&nItem="+$I("hdnIdOrden").value;
        	    break;
	    }
	    strEnlace += "&sAccion=U";
	    strEnlace += "&nIDDOC="+ nIDDoc;
	    strEnlace += "&sOrigen="+ sOrigen;
	    
	    mostrarProcesando();
	    //alert(strEnlace);
//	    var ret = window.showModalDialog(strEnlace, self, sSize(650, 260)); 
//	    window.focus();
//		//alert(ret);
        modalDialog.Show(strEnlace, self, sSize(650, 260))
            .then(function(ret) {
	            if (ret != null){
                    if (ret == "OK"){
                        //alert(sTipo);
	                    switch (sTipo){
	                        case "AS":
	                        case "AS_PE":
	                        case "AS_PT":
	                        case "AS_T":
        	                    actualizarDocumentos($I("txtIdAsunto").value, sTipo);
        	                    break;
	                        case "AC":
	                        case "AC_PE":
	                        case "AC_PT":
	                        case "AC_T":
        	                    actualizarDocumentos($I("txtIdAccion").value, sTipo);
        	                    break;
	                        case "T":
        	                    actualizarDocumentos($I("hdnIdTarea").value, sTipo);
        	                    break;
	                        case "A":
        	                    actualizarDocumentos($I("hdnIDActividad").value, sTipo);
        	                    break;
	                        case "F":
        	                    actualizarDocumentos($I("hdnIDFase").value, sTipo);
        	                    break;
	                        case "PT":
        	                    actualizarDocumentos($I("hdnIDPT").value, sTipo);
        	                    break;
        //	                case "PE":
        //        	            actualizarDocumentos($I("hdnIDPE").value+"/"+$I("hdnIDCR").value, sTipo);
        //        	            break;
	                        case "PE":
	                        case "PSN":
        	                    actualizarDocumentos($I("hdnIdProyectoSubNodo").value, sTipo);
        	                    break;
                            case "HT": //Hito lineal
                            case "HM": //Hito discontinuo
	                        case "HF": //Hito de fecha
        	                    actualizarDocumentos($I("txtIdHito").value.replace('.',''), sTipo);
        	                    break;
	                        case "OF":
        	                    actualizarDocumentos($I("hdnIdOrden").value, sTipo);
        	                    break;
	                    }
                    }
       		        ocultarProcesando();
       		   }    //then
            });     //modalDialog.Show
	   // }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar documentos", e.message);
	}
}

function setEstadoBotonesDoc(sModoAcceso, sEstadoProyecto){
    try{
        if (sModoAcceso == "R"){
            setOp($I("Button1"), 30);
            setOp($I("Button2"), 30);
        }else{
            if (sEstadoProyecto=="C" || sEstadoProyecto=="H"){
                setOp($I("Button1"), 100);
                setOp($I("Button2"), 30);
            }
            else{
                setOp($I("Button1"), 100);
                setOp($I("Button2"), 100);
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al establecer el estado de los botones de acceso a documentos", e.message);
    }
}

-->