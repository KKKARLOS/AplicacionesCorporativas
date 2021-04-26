function init(){
    try{
        ToolTipBotonera("procesar", "Paso de T445_INTERFACTSAP a datos económicos");

        ocultarProcesando();
        AccionBotonera("procesar", "D");
        if (bFechaFacIncorrecta){
            mmoff("War","Existen en T445_INTERFACTSAP líneas correspondientes a facturas de diferentes meses. No se permite procesar. ",420);
        }else if ($I("cldFacOK").innerText != "0"){
            AccionBotonera("procesar", "H");
        }
        if ($I("cldTotalFac").innerText != "0"){
            $I("lblIFS").className = "enlace";
            $I("lblIFS").onclick = function (){mostrarIFS()};
        }

        setExcelImg("imgExcel", "divErrores");
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
            case "procesar":
                AccionBotonera("procesar", "D");
                $I("divErrores").children[0].innerHTML = aResul[3];
                $I("cldTotalFac").innerText = aResul[4];
                var sMens="";
                var aP = aResul[2].split("##");
                for (i=0;i<aP.length;i++){
                    if (aP[i] != "")
                        sMens+=aP[i] + "\n";
                }
                if (sMens != ""){
                    sMens="Proyectos procesados en estado PRESUPUESTADO: \n\n"+ sMens;
                    mmoff("Inf", sMens, 400);
                }
                mmoff("Suc", "Proceso correcto", 160);
                break;
           case "mostrarIFS":
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
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function procesar(){
    try{
        if (nAnoMes == ""){
            mmoff("War","No hay datos a procesar", 200);
            return;
        }
        js_args = "procesar@#@";
        js_args+= nAnoMes + "@#@";
        js_args+= flGetCadenaDesglose();
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}
function flGetCadenaDesglose(){
/*Recorre la tabla de facturas erróneas para obtener una cadena que se pasará como parámetro
  al procedimiento de grabación. En la cadena figurarán los nº de linea del fichero que no son correctos
*/
    try{ 
        var sRes="";
        var aF=FilasDe("tblErrores");
        var sw=0;
        var sb = new StringBuilder;

        for (var i=0;i<aF.length;i++){
            //sRes += aF[i].id+"##";
            sb.Append(aF[i].id+"##");
            sw=1;
        }//for
        //if (sRes != "") sRes="##"+ sRes;
        if (sw == 1) sRes= "##"+ sb.ToString();
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function mostrarIFS(){
    try{
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("mostrarIFS@#@");
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar INTERFACTSAP", e.message);
    }
}

function excel(){
    try {
        var tblErrores = $I("tblErrores");
    
        if (tblErrores==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
		sb.Append("        <td style='width:auto'>ID</TD>");
		sb.Append("        <td style='width:auto'>Fecha</TD>");
		sb.Append("        <td style='width:auto'>Serie</TD>");
		sb.Append("        <td style='width:auto'>Factura</TD>");
		sb.Append("        <td style='width:auto'>Motivo</TD>");
		sb.Append("	</TR>");
		sb.Append("</TABLE>");
        
        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
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
