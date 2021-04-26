var nTotEst = "";
var sFinEst = "";

function init(){
    try{
    
        if (!mostrarErrores()) return;
        
        nTotEst = $I("txtTotEst").value;
        sFinEst = $I("txtFinEst").value;
        if ($I("chkFinalizada").checked == true){
            bFinalizada = "1";
            bFinalizadaAntes = "1";
        }
        else{
            bFinalizada = "0";
            bFinalizadaAntes = "0";
        }
        if ($I("txtComentario").value != "" || $I("txtColectivas").value != "" || $I("txtIndicaciones").value != "" || $I("txtTotPre").value != "" || $I("txtFinPre").value != "") sIndiComen = "S";
        else sIndiComen = "N";
        nPendiente = $I("txtPteEst").value;
        
        if (bEstadoLectura){
            setOp($I("btnNuevo"), 30);
            setOp($I("btnEliminar"), 30);
            $I("txtFinEst").onclick = null;
        }
        
        sETEOriginal = nTotEst;
        sFFEOriginal = sFinEst;
        sComentarioOriginal = Utilidades.escape($I("txtComentario").value);
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptar(){
    var strRetorno="";
    strRetorno += nTotEst +"@#@";
    strRetorno += sFinEst +"@#@";
    strRetorno += bFinalizada +"@#@";
    strRetorno += sIndiComen +"@#@";
    strRetorno += nPendiente;
   
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}

function salir() {
    var strRetorno="";
    strRetorno += nTotEst +"@#@";
    strRetorno += sFinEst +"@#@";
    strRetorno += bFinalizada +"@#@";
    strRetorno += sIndiComen +"@#@";
    strRetorno += nPendiente;
   
    var returnValue = strRetorno;
    var sMsg="Datos modificados. ¿Deseas grabarlos?";
    if (bCambios) {
        if ($I("txtPteEst").value == "0,00" && $I("chkFinalizada").checked == false){
            sMsg="La tarea tiene un esfuerzo pendiente de 0 horas.<br>¿Deseas marcarla como finalizada y grabar?";
        }
        jqConfirm("", sMsg, "", "", "war",370).then(function (answer) {
            if (answer) {
                if ($I("txtPteEst").value == "0,00" && $I("chkFinalizada").checked == false)
                    $I("chkFinalizada").checked = true;
                grabarsalir();
            }
            else{
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
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
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                bCambios = false;
                setOp($I("btnGrabarSalir"), 30);
                ocultarProcesando();
                mmoff("Suc","Grabación correcta", 160); 

                if (bSalir) setTimeout("aceptar();",50);
                break;
                
            case "documentos":
		        $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                ocultarProcesando();
                nfs = 0;
                break;
                
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].className == "FI" || aFila[i].getAttribute("class") == "FI" ) $I("tblDocumentos").deleteRow(i);
                }
                aFila = null;
                recolorearTabla("tblDocumentos");
                nfs = 0;
                ocultarProcesando();
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}

function grabarsalir(){
    bSalir = true;
    grabar();
}

function grabarAux(){
    bSalir = false;
    grabar();
}

function grabar(){
    try{
       	if (getOp($I("btnGrabarSalir")) != 100) return;
        
       	if (!comprobarDatos()) return;
        
       	llamarGrabar();

    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function llamarGrabar() {
    try {
        nTotEst = $I("txtTotEst").value;
        sFinEst = $I("txtFinEst").value;
        if ($I("chkFinalizada").checked == true) bFinalizada = "1";
        else bFinalizada = "0";
        if ($I("txtComentario").value != "" || $I("txtIndicaciones").value != "" || $I("txtTotPre").value != "" || $I("txtFinPre").value != "") sIndiComen = "S";
        else sIndiComen = "N";
        nPendiente = $I("txtPteEst").value;

        var js_args = "grabar@#@";
        js_args += $I("hdnIdTarea").value +"##"; //0
        js_args += nTotEst +"##"; //1
        js_args += sFinEst +"##"; //2
        js_args += Utilidades.escape($I("txtComentario").value) +"##"; //3
        if ($I("chkFinalizada").checked) js_args += "1##"; //4
        else js_args += "0##";
        js_args += bFinalizadaAntes +"##"; //5 Antes (finalizada o no, al inicio)
        js_args += nPT +"##";  //6
        js_args += Utilidades.escape(sDesTarea) +"##"; //7
        js_args += sNotas +"##"; //8
        js_args += Utilidades.escape($I("txtNotas1").value) +"##"; //9
        js_args += Utilidades.escape($I("txtNotas2").value) +"##"; //10
        js_args += Utilidades.escape($I("txtNotas3").value) +"##"; //11
        js_args += Utilidades.escape($I("txtNotas4").value) +"##"; //12
        js_args += sComentarioOriginal +"##"; //13
        js_args += sETEOriginal +"##"; //14
        js_args += sFFEOriginal +"##"; //15
        
        //alert(js_args);//return;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la tarea", e.message);
    }
}

function comprobarDatos(){
    try{
        if (nObligaest == "1"){
            if ($I("txtTotEst").value != "" && $I("chkFinalizada").checked == false){
                if (isNaN(parseFloat(desformatNumero($I("txtTotEst").value)))){
                    mmoff("War","El esfuerzo total estimado debe ser un valor numérico",390);
                    $I("txtTotEst").select();
                    return false;
                }
            }
            if ($I("txtTotEst").value == "0,00" && $I("chkFinalizada").checked == false){
                mmoff("War","Debes indicar el esfuerzo total estimado",350);
                $I("txtTotEst").select();
                return false;
            }

            if ($I("txtFinEst").value == "" && $I("chkFinalizada").checked == false){
                mmoff("War","Debes indicar la fecha de finalización estimada",370);
                return false;
            }
            if (parseFloat(desformatNumero($I("txtPteEst").value)) < 0 && $I("chkFinalizada").checked == false){
			    mmoff("War","No puedes dejar un esfuerzo pendiente negativo",350);
			    return false;
            }
        }

        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar(){
    try{
        if (bEstadoLectura) return;
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

function controlTareaFin(){
    try{
        if ($I("chkFinalizada").checked == true){
            if ($I("txtUltCon").value!="")
            {
//                if ($I("txtFinEst").value=="") $I("txtFinEst").value = $I("txtUltCon").value;
//                var dFinEst = cadenaAfecha($I("txtFinEst").value);
//                var dUltCon = cadenaAfecha($I("txtUltCon").value);
//                if (dUltCon>dFinEst) 
                    $I("txtFinEst").value = $I("txtUltCon").value;
            }       
        
            $I("txtTotEst").value = $I("txtConHor").value;
            $I("txtPteEst").value = "0,00";
            
            var nAvanTeo = 0;
            if (parseFloat(dfn($I("txtTotEst").value)) != 0){
                nAvanTeo = parseFloat(dfn($I("txtConHor").value)) * 100 / parseFloat(dfn($I("txtTotEst").value));
            }
            $I("txtAvanEst").value = nAvanTeo.ToString("N");
        }
        activarGrabar();
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al controlar la tarea finalizada", e.message);
	}
}

function controlHorasPtes(){
    try{
        var nPte = parseFloat(dfn($I("txtTotEst").value)) - parseFloat(dfn($I("txtConHor").value));
        $I("txtPteEst").value = nPte.ToString("N");

        var nAvanTeo = 0;
        if (parseFloat(dfn($I("txtTotEst").value)) != 0){
            nAvanTeo = parseFloat(dfn($I("txtConHor").value)) * 100 / parseFloat(dfn($I("txtTotEst").value));
        }
        $I("txtAvanEst").value = nAvanTeo.ToString("N");

        activarGrabar();
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al controlar la tarea finalizada", e.message);
	}
}

//function descargar(sTipo, nIDDoc){
//    try{
//        var strEnlace = "../Documentos/Descargar.aspx?";
//	    strEnlace += "sTipo="+sTipo;
//	    strEnlace += "&nIDDOC="+nIDDoc;
//        
//        mostrarProcesando();
//        $I("iFrmSubida").src = strEnlace;
//        setTimeout("ocultarProcesando();",5000);
//	}catch(e){
//		mostrarErrorAplicacion("Error al descargar el documento", e.message);
//	}
//}
var tsPestanas = null;
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
/*    
        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aeh.aad) {  //ID
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
*/        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}
