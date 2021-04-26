var bLectura=false;

function init(){
    try{
        if (!mostrarErrores()) return;

        window.focus();
        $I("txtDen").focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
//var idRetorno = "";
function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;
        var returnValue = $I("tblCertificados").rows[indexFila].id; ;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}
function aceptar() {
    var returnValue = "OK";
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
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                resaltarTexto();
                window.focus();
                break;
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

function buscar(){
    try{
        var js_args = "buscar@#@";
        js_args += $I("txtDen").value + "@#@";
        js_args += $I("cboEntCert").value + "@#@";
        js_args += $I("cboEntorno").value;
      
        //alert(js_args);     
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
function seleccionar() {
    try {
        var aFilas = FilasDe("tblCertificados");
        if (aFilas == null || aFilas.length == 0) {
            mmoff("War", "Debes seleccionar un certificado", 250);
            return;
        }
        var indFilaSeleccionada = -1;
        for (var x = aFilas.length - 1; x >= 0; x--) {
            if (aFilas[x].className == "FS") {
                indFilaSeleccionada = x;
                break;
            }
        }
        if (indFilaSeleccionada == -1) {
            mmoff("War", "Debes seleccionar un certificado", 250);
        }
        else{
            var returnValue = $I("tblCertificados").rows[indFilaSeleccionada].id;
            modalDialog.Close(window, returnValue);
        }
        
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar certificado", e.message);
    }
}
function solicitarCert(){
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/mantCertificado/Solicitud/Default.aspx?t=" + codpar("C") + "&f=" + codpar($I("hdnIdFicepiCert").value);
        modalDialog.Show(sPantalla, self, sSize(700, 600))
            .then(function(ret) {
                if (ret == "OK") {
                    aceptar();
                }
                else {
                    window.focus();
                }
            });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al solicitar nuevo certificado", e.message);
    }
}
function f_onkeypress(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    //alert((evt.keyCode) ? evt.keyCode : evt.which);
    if (((evt.keyCode) ? evt.keyCode : evt.which) == 13) {
        (ie) ? evt.keyCode = 0 : evt.preventDefault();
        buscar();
    } 
}
