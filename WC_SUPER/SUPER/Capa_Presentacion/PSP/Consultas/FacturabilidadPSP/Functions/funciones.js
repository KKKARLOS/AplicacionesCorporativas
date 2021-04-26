<!--
function init(){
    try{
        actualizarLupas("tblTitulo", "tblDatos");
        setExcelImg("imgExcel", "divCatalogo");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
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
		        actualizarLupas("tblTitulo", "tblDatos");
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }

        ocultarProcesando();
    }
}

function buscar(){
    try{
        if ($I("txtDesde").value == ""){
            Borrar();
            mmoff("Inf", "Para mostrar valores, es necesario un periodo de fechas válido.",400);
            return;
        }
        if ($I("txtHasta").value == ""){
            Borrar();
            mmoff("Inf", "Para mostrar valores, es necesario un periodo de fechas válido.",400);
            return;
        }

        var js_args = "buscar##";
        js_args += $I("txtDesde").value+ "##";
        js_args += $I("txtHasta").value;

        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar", e.message);
    }
}

function Borrar(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        $I("txtHorasFac").value = "0,00";
        $I("txtHorasNoFac").value = "0,00";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los datos", e.message);
    }
}

function VerFecha(strM){
    try {
		if ($I('txtDesde').value.length==10 && $I('txtHasta').value.length==10){
		    aa = $I('txtDesde').value;
		    bb = $I('txtHasta').value;
		    if (aa == "") aa = "01/01/1900";
		    if (bb == "") bb = "01/01/1900";
		    fecha_desde = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
		    fecha_hasta = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);

            if (strM=='D' && $I('txtDesde').value == "") return;
            if (strM=='H' && $I('txtHasta').value == "") return;

            if ($I('txtDesde').value.length < 10 || $I('txtHasta').value.length < 10) return;
    		
            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtHasta').value = $I('txtDesde').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtDesde').value = $I('txtHasta').value;
                
            buscar();
		}
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}


function excel(){
    try{
        var tblDatos = $I("tblDatos");
        if (tblDatos==null){
            ocultarProcesando();
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }
        $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td colspan='3'>&nbsp;Desde: "+$I("txtDesde").value+"&nbsp;&nbsp;&nbsp;Hasta: "+$I("txtHasta").value+"</td>");
        sb.Append("        <td width='238px' colspan='4' style='background-color: #E4EFF3;'>Horas agenda</TD>");
        sb.Append("        <td width='180px' colspan='3' style='background-color: #E4EFF3;'>Horas reales</TD>");
        sb.Append("        <td width='60px'>&nbsp;</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td>Profesional</td>");
        sb.Append("        <td>H.T.C.</td>");
        sb.Append("        <td>H.T.D.</td>");
        sb.Append("        <td>T.F.</td>");
        sb.Append("        <td>T.N.F.</td>");
        sb.Append("        <td>H.S.T.</td>");
        sb.Append("        <td>Total</td>");
        sb.Append("        <td>T.F.</td>");
        sb.Append("        <td>T.N.F.</td>");
        sb.Append("        <td>Total</td>");
        sb.Append("        <td>F.T.</td>");
		sb.Append("	</TR>");

	    for (var i=0;i < tblDatos.rows.length; i++){
	        sb.Append(tblDatos.rows[i].outerHTML);
        }
		
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
-->
