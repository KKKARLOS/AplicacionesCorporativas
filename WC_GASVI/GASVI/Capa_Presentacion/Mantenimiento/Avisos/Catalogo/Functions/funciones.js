var aFila;
var oFilaAct;
function init(){
    try{
        ToolTipBotonera("eliminar","Elimina el aviso seleccionado");
        aFila=FilasDe("tblDatos");
        oFilaAct=null;
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Comunicados de administración";
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
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                aFila=FilasDe("tblDatos");
                break;

            case "eliminar":
                aFila = FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].className == "FS"){
                        $I("tblDatos").deleteRow(i);
                    }
                }
                $I("txtTexto").value = "";
                actualizarLupas("tblTitulo", "tblDatos");
                break;
        }
        AccionBotonera("eliminar","D");
        ocultarProcesando();
    }
}
function mostrarDetalle(nIdAviso){
    mostrarProcesando();
    setTimeout("mDetalle('"+nIdAviso+"')",100);
}
function mDetalle(nIdAviso){
    try{
//        var sPantalla="../Detalle/Default.aspx?nIdAviso=" + nIdAviso;
//        var sTamanio= sSize(940, 650);
//        
//        var ret = window.showModalDialog(sPantalla, self, sTamanio);
//        window.focus();

        var strEnlace = "../Detalle/Default.aspx?nIdAviso=" + nIdAviso;
        modalDialog.Show(strEnlace, self, sSize(940, 650))
            .then(function(ret) {
                if (ret != null) {
                    var aNuevos = ret.split("@#@");
                    if (aNuevos[0] == "F") {//no ha habido cambios en la pantalla detalle
                        ocultarProcesando();
                        return;
                    }
                    $I("txtTexto").value = "";
                    buscar();
                }
                ocultarProcesando();
            });
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el aviso", e.message);
    }
}
function nuevoAviso(){
    try{
        mostrarDetalle(0);   
	}
	catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo aviso", e.message);
    }
}
function buscar(){
    try{
        mostrarProcesando();
        RealizarCallBack("buscar", "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordernar el catálogo", e.message);
    }
}

function eliminarAviso(){
    try{
        var js_args = "eliminar@#@";
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                js_args += aFila[i].id +"##"
            }
        }
        mostrarProcesando();
        RealizarCallBack(js_args, "");        
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el aviso", e.message);
    }
}

function mostrarTexto(oFila){
    try{
        $I("txtTexto").value = Utilidades.unescape(oFila.getAttribute("texto"));

//        var reg = /\+/g;
//        var strCom = $I(oFila).texto;
//        strCom = strCom.replace(reg, "%2B");
//        reg = /\$/g;
//        strCom = strCom.replace(reg, "%24");
        
//        $I("txtTexto").value = Utilidades.unescape(strCom);
//        //$I("txtTexto").value = unescape(strCom);              
        
        oFilaAct = oFila;
        AccionBotonera("eliminar","H");

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el texto del aviso", e.message);
    }
}


