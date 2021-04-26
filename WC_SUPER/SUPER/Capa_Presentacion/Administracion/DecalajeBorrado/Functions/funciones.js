function init(){
    try{
        $I("cldEstNodo").innerText = $I("tblNodos").rows.length;
        actualizarLupas("tblTituloNodo", "tblNodos");

        //alert(nAnoMesActual);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        setExcelImg("imgExcel", "divCatalogo");

        AccionBotonera("procesar", "H");

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
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("cldEstOrdenes").innerText = aResul[3];
                $I("cldEstOrdenesSel").innerText = $I("tblDatos").rows.length.ToString("N", 9, 0);
                actualizarLupas("tblTitulo", "tblDatos");
                break;
            case "procesar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];

                mmoff("Suc", "Proceso correcto", 160);
                setTimeout("buscar();", 50);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function cambiarMes(nMes){
    try{
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        borrarEstadistica();
        borrarCatalogo();
        if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}

function marcardesmarcarNodo(nOpcion){
    try{
        for (var i=0; i<tblNodos.rows.length; i++){
            tblNodos.rows[i].cells[2].children[0].checked = (nOpcion==1)?true:false;
        }
        borrarEstadistica();
        borrarCatalogo();
        setNodosCount();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}

function marcardesmarcarBorrado(nOpcion){
    try{
        if ($I("tblDatos")==null) return;
        mostrarProcesando();
        setTimeout("marcardesmarcarBorrado2("+nOpcion+")", 20);
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar borrado (1).", e.message);
	}
}

function marcardesmarcarBorrado2(nOpcion){
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos==null) return;
        
        for (var i=0; i<tblDatos.rows.length; i++){
            tblDatos.rows[i].cells[4].children[0].checked = (nOpcion==1)?true:false;
            if (nOpcion==1) tblDatos.rows[i].cells[5].children[0].checked = false;
        }
        countBorDec();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar borrado (2).", e.message);
	}
}

function marcardesmarcarDecalaje(nOpcion){
    try{
        if ($I("tblDatos")==null) return;
        mostrarProcesando();
        setTimeout("marcardesmarcarDecalaje2("+nOpcion+")", 20);
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar decalaje (1).", e.message);
	}
}

function marcardesmarcarDecalaje2(nOpcion){
    try{
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        
        for (var i=0; i<tblDatos.rows.length; i++){
            tblDatos.rows[i].cells[5].children[0].checked = (nOpcion==1)?true:false;
            if (nOpcion==1) tblDatos.rows[i].cells[4].children[0].checked = false;
        }
        countBorDec();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar decalaje (2).", e.message);
	}
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}

function buscar(){
    try{
        mostrarProcesando();
        var sw = 0;
        var sb = new StringBuilder;

        sb.Append("buscar@#@");
        sb.Append(nAnoMesActual +"@#@");
        var tblNodos = $I("tblNodos");
        
        for (var i=0; i<tblNodos.rows.length; i++){
            if (tblNodos.rows[i].cells[2].children[0].checked){
                if (sw == 1) sb.Append(",");
                sb.Append(tblNodos.rows[i].id);
                sw = 1;
            }
        }
        
        if (sw == 0){
            ocultarProcesando();
            mmoff("Inf", "Debes seleccionar algún " + strEstructuraNodo, 370);
            return;
        }
               
        RealizarCallBack(sb.ToString(), "");
        borrarEstadistica();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function procesar(){
    try{
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        
        mostrarProcesando();
        var sw = 0;
        var sb = new StringBuilder;
        
        sb.Append("procesar@#@");
        sb.Append(nAnoMesActual +"@#@");
        
        for (var i=0; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].cells[4].children[0].checked
                || tblDatos.rows[i].cells[5].children[0].checked){
                
                sb.Append(tblDatos.rows[i].getAttribute("idDE")+"##");
                sb.Append(tblDatos.rows[i].getAttribute("idSM")+"##");
                sb.Append(tblDatos.rows[i].getAttribute("idPSN")+"##");
                if (tblDatos.rows[i].cells[4].children[0].checked) sb.Append("B///");
                else sb.Append("D///");
                sw = 1;
            }
        }
        
        if (sw == 0){
            ocultarProcesando();
            mmoff("Inf", "No hay ninguna órden de facturación seleccionada para procesar.",400);
            return;
        }
               
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function setNodosCount(){
    try{
        var nCount = 0;
        var tblNodos = $I("tblNodos");
        
        for (var i=0; i<tblNodos.rows.length; i++){
            if (tblNodos.rows[i].cells[2].children[0].checked) nCount++;
        }
        $I("cldEstNodoSel").innerText = nCount.ToString("N", 9, 0);
        $I("cldEstOrdenesSel").innerText = 0;
        $I("cldEstBorrar").innerText = 0;
        $I("cldEstDecalar").innerText = 0;
        if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}

function countBorDec(){
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null){
            $I("cldEstBorrar").innerText = 0;
            $I("cldEstDecalar").innerText = 0;
            return;
        }
        
        var nBorrado = 0;
        var nDecalaje = 0;
        for (var i=0; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].cells[4].children[0].checked) nBorrado++;
            if (tblDatos.rows[i].cells[5].children[0].checked) nDecalaje++;
        }
        $I("cldEstBorrar").innerText = nBorrado.ToString("N", 9, 0);
        $I("cldEstDecalar").innerText = nDecalaje.ToString("N", 9, 0);
	}catch(e){
		mostrarErrorAplicacion("Error al contar las órdenes a borrar/decalar.", e.message);
	}
}

function borrarEstadistica(){
    try{
        $I("cldEstOrdenes").innerText = 0;
        $I("cldEstOrdenesSel").innerText = 0;
        $I("cldEstBorrar").innerText = 0;
        $I("cldEstDecalar").innerText = 0;
	}catch(e){
		mostrarErrorAplicacion("Error al borrar las estadísticas.", e.message);
	}
}

//function setActuAuto(){
//    try{
//        if ($I("chkActuAuto").checked){
//            borrarEstadistica();
//            borrarCatalogo();
//            buscar();
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al establecer el modo automático.", e.message);
//	}
//}
function excel() {
    try {
        var aFila = FilasDe("tblDatos");
        if (aFila == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:auto'>Proyecto</TD>");
        sb.Append("        <td style='width:auto'>Clase económica</TD>");
        sb.Append("        <td style='width:auto'>Motivo</TD>");
        sb.Append("        <td style='width:auto'>Importe</TD>");
        sb.Append("        <td style='width:auto'>Borrado</TD>");
        sb.Append("        <td style='width:auto'>Decalaje</TD>");

        sb.Append("        <td style='width:auto'>" + strEstructuraNodo + "</TD>");
        sb.Append("        <td style='width:auto'>" + strLinea + "</TD>");
        sb.Append("        <td style='width:auto'>Responsable</TD>");
        sb.Append("        <td style='width:auto'>Cliente</TD>");
        sb.Append("        <td style='width:auto'>Moneda</TD>");
        sb.Append("        <td style='width:auto'>Destinatario Factura</TD>");
        sb.Append("        <td style='width:auto'>Tramitador</TD>");

        sb.Append("	</TR>");

        for (var i = 0; i < aFila.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>" + aFila[i].cells[0].innerText + "</td>");
            sb.Append("<td>" + aFila[i].cells[1].innerText + "</td>");
            sb.Append("<td>" + aFila[i].cells[2].innerText + "</td>");
            sb.Append("<td>" + aFila[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + ((aFila[i].cells[4].children[0].checked) ? "Sí" : "No") + "</td>");
            sb.Append("<td>" + ((aFila[i].cells[5].children[0].checked) ? "Sí" : "No") + "</td>");

            sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("linea")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("cr")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("responsable")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("cliente")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("moneda")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("desfactura")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("tramitador")) + "</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        //var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
