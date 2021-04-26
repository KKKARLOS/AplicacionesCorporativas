var aFila = null;
var strAction = "";
var strTarget = "";

function init(){
    try{
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target;
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);

	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function cambiarMes(nMes){
    try{
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);;
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0])
        {
            case "getDatosMes":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                break;
            case "ExportarExcel":
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
    }
    ocultarProcesando();
}

function Exportar(){
    try{   	    		
		var sScroll = "no";
		if (screen.width == 800) sScroll = "yes";
		
        var sb = new StringBuilder;
        var tblDatos = $I("tblDatos");
        
        for (var i=0;i < tblDatos.rows.length; i++){
            if (tblDatos.rows[i].className == "FS") sb.Append(tblDatos.rows[i].id + ",");                
        }
 		$I("hdnIDS").value = sb.ToString();       
		if ($I("hdnIDS").value == ""){
		    mmoff("War", "No hay registros seleccionados a exportar", 300);
		    return;
		}
		$I("hdnIDS").value = $I("hdnIDS").value.substr(0,$I("hdnIDS").value.length-1);
		$I("FORMATO").value = "PDF"; 
		
		$I("hdnAnoMes").value = nAnoMesActual;
                
        //*SSRS

		var params = {
		    reportName: "/SUPER/sup_proyectoUsa",
		    tipo: "PDF",
		    Codigo: $I("hdnIDS").value,
		    t641_anomes: $I("hdnAnoMes").value,
		    AnoMesTexto: $I("txtMesVisible").value,
		    iFecha: $I("hdnAnoMes").value
		};

		PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR
        document.forms["aspnetForm"].action="Exportar/default.aspx";
		document.forms["aspnetForm"].target="_blank";
		document.forms["aspnetForm"].submit();
        //CR*/
		
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}

function ExportarExcel(){
    try{
    //alert(nAnoMesActual);return;
        mostrarProcesando();
        
        var sb = new StringBuilder;
        var tblDatos = $I("tblDatos");
        for (var i=0;i < tblDatos.rows.length; i++){
            if (tblDatos.rows[i].className == "FS") sb.Append(tblDatos.rows[i].id + ",");                
        }
        
		if (sb.ToString() == ""){
		    ocultarProcesando();
		    mmoff("War", "No hay proyectos seleccionados a exportar", 300);
		    return;
		}

        var js_args = "ExportarExcel@#@";
        js_args += sb.ToString() + "@#@";
        js_args += nAnoMesActual;
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a exportar a Excel.", e.message);
    }
}
