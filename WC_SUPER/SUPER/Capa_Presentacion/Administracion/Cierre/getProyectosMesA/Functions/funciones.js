var bLectura=false;

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

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);
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
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);
                
                if (oFila.getAttribute("categoria")=="P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                
                switch (oFila.getAttribute("cualidad")){
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

