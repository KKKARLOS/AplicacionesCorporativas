var bRegresar = false;

function init() {
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Monedas";
        actualizarLupas("tblTitulo", "tblMonedas");
        desActivarGrabar();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--) mfa(aFila[i],"N");
                desActivarGrabar();
                actualizarLupas("tblTitulo", "tblMonedas");
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                }
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function flGetMonedas(){
    /*Recorre la tblMonedas para obtener una cadena que se pasará como parámetro
      al procedimiento de grabación
    */
    var sRes = "", sCodigo, sTipoOperacion;
    var bGrabar = false, bActivo = false;
    try{
        //aFila = tblMonedas.getElementsByTagName("tr");
        aFila = FilasDe("tblMonedas")
        for (var i=0; i<aFila.length; i++){
            sTipoOperacion = aFila[i].getAttribute("bd");
            if (sTipoOperacion != ""){
                sRes += sTipoOperacion + "#sCad#";
                sRes += aFila[i].id + "#sCad#";
                var sActiva = (aFila[i].cells[2].children[0].checked)? "1":"0";
                sRes += sActiva + "#sFin#";
            }
        }
        if(sRes != "") sRes = sRes.substring(0, sRes.length-6);  
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}

function grabar(){
    try{
        if (iFila >= 0) modoControles(tblMonedas.rows[iFila], false);       
        var js_args = "grabar@#@";
        js_args += flGetMonedas();
//        var defecto = $I("cboMoneda").options[$I("cboMoneda").selectedIndex].value;
//        if (defecto != $I("hdnDefectoOld").value) 
//            defecto += "#sCad#" + $I("hdnDefectoOld").value;
//        else 
//            defecto = "";
//        js_args += "@#@" + defecto;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

function excelMonedas(){
    try{
        if ($I("tblMonedas") == null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        aFila = FilasDe("tblMonedas")
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
	    sb.Append("	<tr style='text-align:center'>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Identificador</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Denominacion</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Activa</td>");
	    sb.Append("	</tr>");
        for (var i=0; i<aFila.length; i++){
            sb.Append("<tr>");
            sb.Append("<td style='align:right;'>" + aFila[i].id + "</td>");
            sb.Append("<td style='align:right;'>" + aFila[i].innerText + "</td>");
            var sActiva = (aFila[i].cells[2].children[0].checked)? "Sí":"No";
            sb.Append("<td style='align:right;'>" + sActiva + "</td>");
            sb.Append("</tr>");
        }	
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
	    sb.Append("	</tr>");
        sb.Append("</table>");
        crearExcel(sb.ToString());
        //crearExcelServidor(sb.ToString(), "Monedas");
        var sb = null;
    }catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos para generar el archivo excel con las monedas", e.message);
    }
}

