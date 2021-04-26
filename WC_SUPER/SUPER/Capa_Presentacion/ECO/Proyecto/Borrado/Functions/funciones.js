function init(){
    try{
        ToolTipBotonera("procesar","Elimina los proyectos seleccionados");
        if ($I("tblDatos") != null){
            scrollTablaProy();
            //actualizarLupas("tblTitulo", "tblDatos");
            AccionBotonera("procesar", "H");
        }
        ocultarProcesando();
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error al iniciar la página", e.message);
    }
}
function eliminarProyectos(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("eliminar@#@");
        var sw = 0;
        var tblDatos = $I("tblDatos");
        for (var i=0; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].cells[0].children[0].checked){
                sb.Append(tblDatos.rows[i].id + "," + tblDatos.rows[i].getAttribute("pry") + "," + tblDatos.rows[i].getAttribute("estado") + "##");
                sw = 1;
            }
        }
        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "No se han seleccionado proyectos a borrar.", 300);
            return;
        }

        jqConfirm("", "¿Deseas borrar los proyectos seleccionados?", "", "", "war", 300).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                RealizarCallBack(sb.ToString(), "");
            }
            else ocultarProcesando();
        });
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar proyectos", e.message);
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
            case "eliminar":
                //eliminarFilaDeTabla();
                bCambios = false;
                if (aResul[2] != ""){
                    mostrarError(aResul[2].replace(reg, "\n"));
                }
                else
                mmoff("Suc", "Proyectos eliminados.",180);
                setTimeout("getDatos()", 20);
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                //actualizarLupas("tblTitulo", "tblDatos");
                scrollTablaProy();
                bCambios = false;
                window.focus();
                break;
        }
        ocultarProcesando();
    }
}
//function eliminarFilaDeTabla(){
//    try{	
//        var aFila = $I("tblDatos").getElementsByTagName("tr");
//        var nFilas=aFila.length;
//	    for (i=nFilas-1;i>=0;i--){	        
//            if (aFila[i].className == "FS"){
//                $I("tblDatos").deleteRow(i);
//            }
//        }
//	    iFila == -1;
//	    //actualizarLupas("tblTitulo", "tblDatos");
//	}
//    catch(e){
//		mostrarErrorAplicacion("Error al eliminar línea de desglose", e.message);
//    }
//}
function getDatos(){
    try{
        var js_args = "buscar@#@";
        //js_args += nOrden +"@#@";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el catálogo", e.message);
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
        var tblDatos = $I("tblDatos"); 
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);
                
                //oFila.ondblclick = function(){aceptarClick(this.rowIndex)};

                if (oFila.getAttribute("categoria") == "P") oFila.cells[1].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[1].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[2].appendChild(oImgContratante.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[3].appendChild(oImgAbierto.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}
