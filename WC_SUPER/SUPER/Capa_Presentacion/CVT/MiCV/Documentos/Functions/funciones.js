function init(){
    try{
        if (!mostrarErrores()) return;
        $I("divCatalogoDoc").children[0].children[0].style.backgroundImage = "url(../../../../Images/imgFT20.gif)";   
        ocultarProcesando(); 
          
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}


function unload(){

}
function aceptar() {
    var returnValue = "ok";
    modalDialog.Close(window, returnValue);
}

function cerrarVentana() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);
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
            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                $I("divCatalogoDoc").children[0].children[0].style.backgroundImage = "url(../../../../Images/imgFT20.gif)";   
                $I("divCatalogoDoc").scrollTop = 0;
                break;
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
                }
                aFila = null;
                nfs = 0;
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}


function addDoc(){
     try {

        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/Documentos/Subir/Default.aspx?ID=" + codpar($I("hdnID").value) + "&sAccion=" + codpar("I");
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(620, 170))
            .then(function(ret) {
                if (ret != null) {
                    RealizarCallBack("documentos@#@" + $I("hdnID").value, "");
                    return;
                }
            });
        window.focus();

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al añadir documentos", e.message);
    }
}

function modificarDoc(sId) {
    try {

        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/Documentos/Subir/Default.aspx?ID=" + codpar(sId) + "&sAccion=" + codpar("U");
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(620, 170))
            .then(function(ret) {
                if (ret != null) {
                    RealizarCallBack("documentos@#@" + $I("hdnID").value, "");
                    return;
                }
            });
        window.focus();
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al modificar documentos", e.message);
    }
}
function CancelarDoc() {
    try {
        $I("divDocumentos").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar la selección de documento.", e.message);
    }
}

function delDoc() {
    try {
        if (nfs == 0) {
            mmoff("Inf", "Debes seleccionar los archivos a eliminar", 290);
            return;
        }

        jqConfirm("", "Esta acción eliminará de base de datos los documentos seleccionados.<br><br>¿Deseas continuar?", "", "", "war", 350).then(function (answer) {
            if (answer) {
                var js_args = "elimdocs@#@";
                var aFila = FilasDe("tblDocumentos");

                var sw = 0;
                for (var i = 0; i < aFila.length; i++) {
                    if (aFila[i].className == "FS") {
                        js_args += aFila[i].id + "##";
                        aFila[i].className = "FI";
                        sw = 1;
                    }
                }

                aFila = null;

                if (sw == 1) js_args = js_args.substring(0, js_args.length - 2);
                else {
                    mmoff("Inf", "Debes seleccionar los archivos a eliminar", 290);
                    return;
                }
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return;
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los documentos", e.message);
    }
}

function descargar(nIDDoc) {
    try {
        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/Documentos/Descargar/Default.aspx?";
        strEnlace += "nIDDOC=" + nIDDoc;
        mostrarProcesando();
        $I("iFrmSubida").src = strEnlace;
        setTimeout("ocultarProcesando();", 5000);
    } catch (e) {
        mostrarErrorAplicacion("Error al descargar el documento", e.message);
    }
}
//function descargarAux(nIDDoc) {
//    try {
//        descargar("CV", nIDDoc)
//    } catch (e) {
//        mostrarErrorAplicacion("Error al descargar el documento", e.message);
//    }
//}
