function init(){
    try{
        ToolTipBotonera("procesar", "Procesa las filas correctas del fichero cargado");

        if ($I("hdnErrores").value != "") {
            AccionBotonera("procesar", "D");
            return;
        }

        ocultarProcesando();        
        if ($I("cldTotalLin").innerText =="0") {
            AccionBotonera("procesar", "D");
            mmoff("Inf","Tabla DATOECOTABLA vacía",170);
        }
        else if ($I("cldTotalLin").innerText == $I("cldLinOK").innerText && $I("cldTotalLin").innerText != "0")
        {
            AccionBotonera("procesar", "H");
        }
        else if ($I(tblErrores).rows.length > 0) 
        {
            AccionBotonera("procesar", "D");
            mmoff("Err", "Se han detectado errores en el proceso verificación", 340);
        }

        if ($I("cldTotalLin").innerText != "0"){
            $I("lblTabla").className = "enlace";
            $I("lblTabla").onclick = function (){mostrarTabla()};
        } 
       
        setExcelImg("imgExcel", "divErrores");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarTabla(){
    try{
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("mostrarTabla@#@");
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar T494_DATOECOTABLA", e.message);
    }
}


function procesar(){
    var js_args="";
    try{
        if ($I("tblErrores").rows.length != 0) return;
        js_args = "procesar@#@";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
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
            case "procesar":
                ocultarProcesando();
                AccionBotonera("procesar", "D");
                mmoff("SucPer", "Proceso finalizado correctamente", 210);
           case "mostrarTabla":
               if (aResul[2] == "cacheado") {
                   var xls;
                   try {
                       xls = new ActiveXObject("Excel.Application");
                       crearExcel(aResul[4]);
                   } catch (e) {
                       crearExcelSimpleServerCache(aResul[3]);
                   }
               }
               else
                   crearExcel(aResul[2]);
               break;
           default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function excel1(){
    try{
        mostrarProcesando();
        setTimeout("excel();", 20);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function excel(){
    try {
        var tblErrores = $I("tblErrores");
        if (tblErrores==null){
            ocultarProcesando();
            mmoff("War","No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
		sb.Append("        <td style='width:auto'>Nodo</TD>");
		sb.Append("        <td style='width:auto'>Proyecto</TD>");
		sb.Append("        <td style='width:auto'>Año/Mes</TD>");
		sb.Append("        <td style='width:auto'>Clase</TD>");
		sb.Append("        <td style='width:auto'>Importe</TD>");
		sb.Append("        <td style='width:auto'>N.destino</TD>");
		sb.Append("        <td style='width:auto'>Proveedor</TD>");
		sb.Append("        <td style='width:auto'>Motivo</TD>");
		sb.Append("        <td style='width:auto'>Error</TD>");
		sb.Append("	</TR>");		
	    for (var i=0;i < tblErrores.rows.length; i++){
	        sb.Append(tblErrores.rows[i].outerHTML);
        }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

