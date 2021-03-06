function init(){
    try{    
        setExcelImg("imgExcel", "divCatalogo", "excel");

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
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function excel(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        //Se pueden haber modificado datos, por lo que regenero aFilaT
        var aFila = FilasDe("tblDatos");
        if (aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sCad="";
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td colspan=7>Proyectos abiertos, contratantes en los que soy Responsable, Delegado o Colaborador</TD>");
		sb.Append("	</TR></TABLE>");
        
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td>Número</td>");
        sb.Append("        <td>Denominación</td>");
        sb.Append("        <td>Responsable de proyecto</td>");
        sb.Append("        <td>"+ strEstructuraNodoLarga +"</td>");
        sb.Append("        <td>Cliente</td>");
        sb.Append("        <td>Externalizado</td>");
        sb.Append("        <td>Factura USA</td>");
		sb.Append("	</TR></TABLE>");
        
        if (aFila != null){
            sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
	        for (var i=0;i < aFila.length; i++){           
    	        sb.Append("<tr>");
                sb.Append("<td>"+ aFila[i].cells[0].innerText + "</td>");
                sb.Append("<td>"+ aFila[i].cells[1].innerText + "</td>");
                sb.Append("<td>"+ Utilidades.unescape(aFila[i].getAttribute("responsable")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("nodo")) + "</td>");
                sb.Append("<td>"+ aFila[i].cells[2].innerText + "</td>");
                sb.Append("<td align='center'>");
                if (aFila[i].cells[3].innerHTML != "")sb.Append("√");
                sb.Append("</td>");
                sb.Append("<td align='center'>");
                if (aFila[i].cells[4].innerHTML != "")sb.Append("√");
                sb.Append("</td>");
    	        
    	        sb.Append("</tr>");
            }
	        sb.Append("</table>");
        }
        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
        sb.Append("<tr style='background-color: #BCD4DF;'>");
        sb.Append("<td colspan='7'></td>");
        sb.Append("</tr>");
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
