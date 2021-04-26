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
        
        mostrarProcesando();	    	
        modalDialog.Show(strEnlace, self, sSize(650,260))
            .then(function(ret) {
                actualizarDocumentos(nItem, sTipo, sOrigen);
            });
  	    window.focus();        	    
        ocultarProcesando();
		//alert("documentos.js -> ret: "+ ret);
		/* Con Chrome no se está obteniendo el valor devuelto */
	    /*if (ret != null){
            if (ret == "OK") actualizarDocumentos(nItem, sTipo);
	    }*/
	}catch(e){
		mostrarErrorAplicacion("Error al añadir documentos", e.message);
	}
}

function actualizarDocumentos(nItem, sTipo, sOrigen){
    try{
        var js_args = "documentos@#@";
        js_args += nItem +"@#@";
        js_args += sTipo +"@#@";
        js_args += gsDocModAcc + "@#@";
        js_args += gsDocEstPry + "@#@";
        js_args += sOrigen;
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
            mmoff("Inf","Debes seleccionar los archivos a eliminar",290);
            return;
        }
        jqConfirm("", "¡Atención!<br><br>Esta acción eliminará de base de datos los documentos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (!answer) return;
            else llamarEliminarDoc();
        });
    }catch(e){
        mostrarErrorAplicacion("Error al actualizar los documentos-1", e.message);
    }
}
function llamarEliminarDoc()
{
    try{
        var sTipo="";
        var js_args = "elimdocs@#@";
        var sClase="";
        
        var aFila = FilasDe("tblDocumentos");
        //Comprobación previa.
        for (var i=0;i<aFila.length;i++){
            if (ie) sClase = aFila[i].className;
            else sClase = aFila[i].getAttribute("class");
                    
            if (sClase== "FS"){
                //if (aFila[i].sAutor != sNumEmpleado && sPerfil != "A"){
                if (aFila[i].getAttribute("sAutor")  != sNumEmpleado && es_administrador == "" ){
                    mmoff("Inf","¡Denegado!\n\nHas seleccionado eliminar documentos que no son de tu propiedad.\n\nDicha acción no está permitida.",380);
                    return;
                }
            }
        }
        var sw=0;

        for (var i=0;i<aFila.length;i++){
            if (ie) sClase = aFila[i].className;
            else sClase = aFila[i].getAttribute("class");
            
            if (sClase == "FS"){
                //if (aFila[i].sAutor == sNumEmpleado || sPerfil == "A"){
                if (aFila[i].getAttribute("sAutor") == sNumEmpleado || es_administrador == "A" || es_administrador == "SA"){
                    sTipo = aFila[i].getAttribute("sTipo");
                    js_args += aFila[i].id +"##";
                    //(ie)? aFila[i].className = "FI" : aFila[i].setAttribute("class","FI");
                    aFila[i].className = "FI";
                    sw=1;
                }else{
                    mmoff("Inf","¡Denegado!\n\nHas seleccionado eliminar documentos que no son de tu propiedad.\n\nDicha acción no está permitida.",380);
                    return;
                }
            }
        }
        aFila = null;
        
        if (sw==1) js_args = js_args.substring(0, js_args.length-2);
        else{
            mmoff("Inf","Debes seleccionar los archivos a eliminar",290);
            return;
        }
        js_args += "@#@"+ sTipo
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los documentos-2", e.message);
	}
}

function descargar(sTipo, nIDDoc){
    try{
        token = new Date().getTime();   //use the current timestamp as the token value
        //alert("Descargar documento de tipo: "+ sTipo +", nº: "+ nIDDoc);
	    var strEnlace = strServer + "Capa_Presentacion/Documentos/Descargar.aspx?";
	    strEnlace += "descargaToken="+ token;
	    strEnlace += "&sTipo="+sTipo;
	    strEnlace += "&nIDDOC="+nIDDoc;

        mostrarProcesando();
        initDownload();
        $I("iFrmSubida").src = strEnlace;
        //setTimeout("ocultarProcesando();",5000);
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
        	    strEnlace += "&nItem="+document.getElementById("txtIdAsunto").value;
        	    break;
	        case "AC":
	        case "AC_PE":
	        case "AC_PT":
	        case "AC_T":
        	    strEnlace += "&nItem="+document.getElementById("txtIdAccion").value;
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
	        case "PEF":
        	    strEnlace += "&nItem="+dfn($I("hdnPE").value);
        	    break;
            case "HT": //Hito lineal
            case "HM": //Hito discontinuo
	        case "HF": //Hito de fecha
        	    strEnlace += "&nItem="+$I("txtIdHito").value.replace('.','');
        	    break;
	        case "OF":
	            if ($I("hdnIdOrden").value == "0"){
	                if (sIDDocuAux != "")
	                    strEnlace += "&nItem="+sIDDocuAux;
	                else
	                    strEnlace += "&nItem="+$I("hdnIdOrden").value;
	            }
	            else
        	        strEnlace += "&nItem="+$I("hdnIdOrden").value;
        	    break;
	        case "PL_OF":
        	    strEnlace += "&nItem="+$I("hdnIdPlantilla").value;
        	    break;
	        case "EC":
        	    strEnlace += "&nItem="+$I("hdnID").value;
        	    break;
	        case "DI":
        	    strEnlace += "&nItem="+$I("hdnIdDialogo").value;
        	    break;
	        case "SC":
        	    strEnlace += "&nItem="+$I("hdnIdSolicitud").value;
        	    break;
	        case "EXAM":
        	    strEnlace += "&nItem="+$I("hdnIdExamen").value;
        	    //strEnlace += "&nFicepi="+$I("hdnIdFicepi").value;
        	    break;
	        case "CERT":
        	    strEnlace += "&nItem="+$I("hdnCVTCert").value;
        	    //strEnlace += "&nFicepi="+$I("hdnIdFicepi").value;
        	    break;
	        case "CURSOR"://Curso recibido
        	    strEnlace += "&nItem="+$I("hdnIdCurso").value;
        	    break;
	        case "CURSOI"://Curso impartido
        	    strEnlace += "&nItem="+$I("hdnIdCurMonitor").value;
        	    break;
	        case "TIF"://Titulo idioma
        	    strEnlace += "&nItem="+$I("hdnIdTitulo").value;
        	    break;
	        case "TAD"://Titulo academico
        	    strEnlace += "&nItem="+$I("hdnIdTituloficepi").value;
        	    break;
	        case "TAE"://Expediente de Titulo academico
        	    strEnlace += "&nItem="+$I("hdnIdTituloficepi").value;
        	    break;
	    }
	    strEnlace += "&sAccion=U";
	    strEnlace += "&nIDDOC="+ nIDDoc;
	    strEnlace += "&sOrigen="+ sOrigen;
	    
	    mostrarProcesando();
	    //alert(strEnlace);
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
	                        case "PEF":
        	                    actualizarDocumentos(dfn($I("hdnPE").value), sTipo);
        	                    break;
                            case "HT": //Hito lineal 
                            case "HM": //Hito discontinuo
	                        case "HF": //Hito de fecha
        	                    actualizarDocumentos($I("txtIdHito").value.replace('.',''), sTipo);
        	                    break;
	                        case "OF":
        	                    //actualizarDocumentos($I("hdnIdOrden").value, sTipo);
	                            if ($I("hdnIdOrden").value == "0"){
	                                if (sIDDocuAux != "")
	                                    actualizarDocumentos(sIDDocuAux, sTipo);
	                                else
	                                    actualizarDocumentos($I("hdnIdOrden").value, sTipo);
	                            }
	                            else
        	                        actualizarDocumentos($I("hdnIdOrden").value, sTipo);
        	                    break;
	                        case "PL_OF":
        	                    actualizarDocumentos($I("hdnIdPlantilla").value, sTipo);
        	                    break;
	                        case "EC":
        	                    actualizarDocumentos($I("hdnID").value, sTipo);
        	                    break;
	                        case "DI":
        	                    actualizarDocumentos($I("hdnIdDialogo").value, sTipo);
        	                    break;
	                        case "SC":
        	                    actualizarDocumentos($I("hdnIdSolicitud").value, sTipo);
        	                    break;
	                        case "EXAM":
        	                    actualizarDocumentos($I("hdnIdExamen").value, sTipo, sOrigen);
        	                    break;
	                        case "CERT":
        	                    actualizarDocumentos($I("hdnCVTCert").value, sTipo, sOrigen);
        	                    break;
	                        case "CURSOR":
        	                    actualizarDocumentos($I("hdnIdCurso").value, sTipo, sOrigen);
        	                    break;
	                        case "CURSOI":
        	                    actualizarDocumentos($I("hdnIdCurMonitor").value, sTipo, sOrigen);
        	                    break;
	                        case "TIF":
        	                    actualizarDocumentos($I("hdnIdTitulo").value, sTipo, sOrigen);
        	                    break;
	                        case "TAD":
        	                    actualizarDocumentos($I("hdnIdTituloficepi").value, sTipo, sOrigen);
        	                    break;
	                        case "TAE":
        	                    actualizarDocumentos($I("hdnIdTituloficepi").value, sTipo, sOrigen);
        	                    break;
	                    }
                        
                    }
	            }
            });
   	    window.focus();        	    
		//alert(ret);
		ocultarProcesando();
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
                //setOp($I("Button1"), 30);
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
