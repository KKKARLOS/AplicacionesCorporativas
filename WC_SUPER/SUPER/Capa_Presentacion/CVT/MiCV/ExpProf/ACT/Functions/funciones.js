function init(){
    try{
        if (!mostrarErrores()) return;
        ocultarProcesando();
        actualizarLupas("tblTitulo", "tblDatos"); 
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(indexFila) {
    try {
        var returnValue = $I("tblDatos").rows[indexFila].getAttribute("id") + "@#@"
                        + Utilidades.escape($I("tblDatos").rows[indexFila].cells[0].innerText);
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}
function aceptarAux() {
    //if (bProcesando()) return;
    //mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar() {
    try {
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        var tblDatos = $I("tblDatos");
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].className == "FS" || tblDatos.rows[i].getAttribute("class") == "FS") {
                sb.Append(tblDatos.rows[i].getAttribute("id") + "@#@");
                sb.Append(Utilidades.escape(tblDatos.rows[i].cells[0].innerText));
                sb.Append("///");
                sw = 1;
            }
        }

        if (sw == 0) {
            //ocultarProcesando();
            mmoff("War", "Debes seleccionar algún item", 230, 2000);
            return;
        }
        var returnValue = sb.ToString().substring(0, sb.ToString().length - 3);
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana(){
    try{
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
