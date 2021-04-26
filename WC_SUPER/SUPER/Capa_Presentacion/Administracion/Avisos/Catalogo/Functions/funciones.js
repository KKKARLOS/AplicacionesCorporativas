var aFila;
//var oFilaAct;
function init(){
    try{
        ToolTipBotonera("eliminar","Elimina el aviso seleccionado");
        ToolTipBotonera("usuarios","Asigna todos los profesionales a los avisos seleccionados");
        LiteralBotonera("usuarios","Todos");
        ToolTipBotonera("desactivar","Elimina todos los profesionales asignados a los avisos seleccionados");
        LiteralBotonera("desactivar","Ninguno");
        actualizarLupas("tblTitulo", "tblDatos");
        //iFila = aFila.length - 1;
        aFila=FilasDe("tblDatos");
        oFilaAct=null;
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
            case "buscar":
                $I("divCatalogo").innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                aFila=FilasDe("tblDatos");
                break;
            case "grabar":
                mmoff("Suc", "Grabación correcta", 160);
                setTimeout("buscar()",100);
                break;
            case "eliminar":
                aFila = FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].className == "FS"){
                        $I("tblDatos").deleteRow(i);
                    }
                }
                actualizarLupas("tblTitulo", "tblDatos");
                break;
        }
        AccionBotonera("eliminar","D");
        AccionBotonera("usuarios","D");
        AccionBotonera("desactivar","D");
        ocultarProcesando();
    }
}
function mostrarDetalle(nIdAviso){
    mostrarProcesando();
    setTimeout("mDetalle('"+nIdAviso+"')",100);
}
function mDetalle(nIdAviso){
    try {
        //var sPantalla = "../Detalle/Default.aspx?nIdAviso=" + nIdAviso;
        var sPantalla = strServer + "Capa_Presentacion/Administracion/Avisos/Detalle/Default.aspx?nIdAviso=" + nIdAviso;
        modalDialog.Show(sPantalla, self, sSize(940, 650))
            .then(function(ret) {
                if (ret != null) {
                    var aNuevos = ret.split("@#@");
                    if (aNuevos[0] == "F") {//no ha habido cambios en la pantalla detalle
                        ocultarProcesando();
                        return;
                    }
                    //Nombre del aviso
                    if (aNuevos[1] == "") {//en este caso estamos volviendo de una pantalla con error 
                        ocultarProcesando();
                        return;
                    }
                    $I("txtTexto").value = "";
                    buscar();
                }
            });

        ocultarProcesando();
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el aviso", e.message);
    }
}
function nuevoAviso(){
    try{
        //location.href = "../Detalle/Default.aspx?nIdAviso=0";
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
//function desgloseAviso(){
//    try{
//        if (iFila == -1){
//            alert("Debe seleccionar algún aviso para poder acceder a su detalle");
//            return;
//        }
//        var nIdAviso = $I("tblDatos").rows[iFila].id;
//        
//        aFila = document.all("tblDatos").getElementsByTagName("TR");
//        location.href = "../Detalle/Default.aspx?nIdAviso="+ nIdAviso;
//    }
//    catch(e){
//		mostrarErrorAplicacion("Error al mostrar el detalle del aviso", e.message);
//    }
//}
function eliminarAviso(){
    try{
        var js_args = "eliminar@#@";
        //aFila = FilasDe("tblDatos");
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
        
        oFilaAct = oFila;
        AccionBotonera("eliminar","H");
        AccionBotonera("usuarios","H");
        AccionBotonera("desactivar","H");
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el texto del aviso", e.message);
    }
}
function grabarProfesionales(sTipo){
    try{
//        if (getOp($I("tblGrabar")) != 100) return;
//        if (!comprobarDatos()) return;
        var js_args = "grabar@#@" + sTipo + "@#@";
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                js_args += aFila[i].id +"##"
            }
        }
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar profesionales asignados al aviso", e.message);
    }
}

//function setTexto(){
//    try{
//        if (oFilaAct != null)
//            oFilaAct.texto = $I("txtTexto").value;
//        
//	}catch(e){
//		mostrarErrorAplicacion("Error al modificar el texto del aviso", e.message);
//    }
//}


