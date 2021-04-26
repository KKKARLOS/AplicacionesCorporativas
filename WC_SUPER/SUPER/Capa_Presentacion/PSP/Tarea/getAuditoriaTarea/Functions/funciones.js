var bLectura=false;

function init(){
    try{
        if (!mostrarErrores()) return;

        setExcelImg("imgExcel", "divCatalogo");

        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

function excel(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='1' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td colspan='2'></td>");
        sb.Append("        <td colspan='2' style='width:auto;background-color: #E4EFF3;'>FFPR</TD>");
        sb.Append("        <td colspan='2' style='width:auto;background-color: #E4EFF3;'>ETPR</TD>");
        sb.Append("	</TR>");
        sb.Append("	<TR align=center>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Fecha</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Profesional</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Antes</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Después</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Antes</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Después</TD>");
		sb.Append("	</TR>");

        sb.Append($I("tblDatos").innerHTML);

	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}