function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("tblDatos") != null){
            scrollTablaProy();
            actualizarLupas("tblTitulo", "tblDatos");
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

var idRetorno = "";
function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;
        if ($I("tblDatos").rows[indexFila].getAttribute("vision") == "0") {
            mmoff("Inf", "El proyecto está fuera de su ámbito de visión.", 300);
            return;
        }

        //idRetorno = $I("tblDatos").rows[indexFila].id + "///" + $I("tblDatos").rows[indexFila].cells[3].innerText + "///" + $I("tblDatos").rows[indexFila].cells[4].innerText + "///" + $I("tblDatos").rows[indexFila].getAttribute("estado") + "///" + $I("tblDatos").rows[indexFila].getAttribute("categoria");

        var js_args = "setPSN@#@";
        js_args += $I("tblDatos").rows[indexFila].id + "@#@";
        js_args += $I("tblDatos").rows[indexFila].getAttribute("ML") + "@#@";
        js_args += $I("tblDatos").rows[indexFila].getAttribute("moneda");
       
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function aceptarSalir(){
    try {
        var returnValue = "OK";
        modalDialog.Close(window, returnValue);		
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);	
}

function mdpsn(oNOBR){
    try{
        aceptarClick(oNOBR.parentNode.parentNode.rowIndex);
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle del proyectosubnodo", e.message);
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
            case "setPSN":
                aceptarSalir();
                break;
           
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}



var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProy){
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);
                                
                oFila.ondblclick = function(){aceptarClick(this.rowIndex)};

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}

