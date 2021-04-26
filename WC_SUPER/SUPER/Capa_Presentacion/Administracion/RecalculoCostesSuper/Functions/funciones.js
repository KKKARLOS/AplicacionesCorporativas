function init(){
    try{
        ToolTipBotonera("procesar", "Procesa las filas correctas del fichero cargado");
        AccionBotonera("procesar", "D");
        
        //alert(nAnoMesActual);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);

        AccionBotonera("procesar", "D");
        
        if ($I("hdnErrores").value!= "")
        {
            ocultarProcesando();
            mmoff("Err",$I("hdnErrores").value,400);
            return;
        }
        
        if ($I("cldTotalLin").innerText == "0")
        {
            ocultarProcesando();
            mmoff("War", "Tabla T609_RECALCULOCOSTESSUPER vacía", 320);
        }
        else if ($I("cldTotalLin").innerText == $I("cldLinOK").innerText && $I("cldTotalLin").innerText != "0")
        {
            AccionBotonera("procesar", "H");
        }
        else if ($I(tblErrores).rows.length > 0)
        {
            ocultarProcesando();
            mmoff("Err", "Se han detectado errores en el proceso verificación", 340);
        }

        if ($I("cldTotalLin").innerText != "0"){
            $I("lblTabla").className = "enlace";
            $I("lblTabla").onclick = function (){mostrarTabla()};
        } 
       
        setExcelImg("imgExcel", "divCatalogo");
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
	    mostrarErrorAplicacion("Error al ir a mostrar T609_RECALCULOCOSTESSUPER", e.message);
    }
}


function procesar(){
    try{
        if ($I("tblErrores").rows.length!=0) return;

        var nCaso = 0;
        if (document.getElementsByName("rdbVariante")[0].checked)
            nCaso = 1;
        else if (document.getElementsByName("rdbVariante")[1].checked)
            nCaso = 2;
        else if (document.getElementsByName("rdbSistema")[1].checked)
            nCaso = 3;
        
//        alert(nCaso);
        if (nCaso == 0){
            mmoff("War","Es necesario indicar el sistema de recálculo a utilizar.",400);
            return;
        }

        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("procesar@#@");
        sb.Append(nAnoMesActual +"@#@");
        sb.Append(nCaso);

        RealizarCallBack(sb.ToString(), "");
	}
	catch(e){
		mostrarErrorAplicacion("Error al ir a procesar", e.message);
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
                mmoff("SucPer", "Proceso finalizado correctamente", 240);
                break;   
           case "mostrarTabla":
                crearExcel(aResul[2]);
                break;   
           case "cargar":
                location.reload(true);
                break;   
            case "getUsuarios":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("cldTotalLin").innerText = aResul[3];
                $I("cldLinOK").innerText = aResul[4];
                $I("cldLinErr").innerText = aResul[5];
                ocultarProcesando();
                break;
            default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function excel(){
    try {
        var tblErrores = $I("tblErrores");    
        if (tblErrores==null || tblErrores.rows.length==0){
            ocultarProcesando();
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold; width:auto;'>Cód. SUPER</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold; width:auto;'>Profesional</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold; width:auto;'>Error</td>");
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

function cambiarMes(nMes){
    try{
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        getUsuarios(nAnoMesActual);
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}
function getUsuarios(nMes) {
    try {
        mostrarProcesando();
        RealizarCallBack("getUsuarios@#@" + nMes, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener usuarios", e.message);
    }
}

function setSistema(oInput){
    try{
        if (oInput.id == "rdbSistema"){
            if (oInput.value == "0"){
                document.getElementsByName("rdbVariante")[0].checked = false;
                document.getElementsByName("rdbVariante")[1].checked = false;
                document.getElementsByName("rdbVariante")[0].disabled = false;
                document.getElementsByName("rdbVariante")[1].disabled = false;
            }else{
                document.getElementsByName("rdbVariante")[0].checked = false;
                document.getElementsByName("rdbVariante")[1].checked = false;
                document.getElementsByName("rdbVariante")[0].disabled = true;
                document.getElementsByName("rdbVariante")[1].disabled = true;
            }
        }
        //alert(oInput.id +" "+ oInput.value);
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer el sistema de recálculo", e.message);
    }
}

function cargar(){
    try {
//        var nCaso = 0;
//        if (document.getElementsByName("rdbVariante")[0].checked)
//            nCaso = 1;
//        else if (document.getElementsByName("rdbVariante")[1].checked)
//            nCaso = 2;
//        else if (document.getElementsByName("rdbSistema")[1].checked)
//            nCaso = 3;

//        //        alert(nCaso);
//        if (nCaso == 0) {
//            mmoff("War", "Es necesario indicar el sistema de recálculo a utilizar.", 400);
//            return;
//        }    
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/RecalculoCostesSuper/Importar.aspx", self, sSize(400, 400))
            .then(function(ret) {
                if (ret != null) {
                    var sb = new StringBuilder;
                    //var aLineas = ret.split(getSaltoLinea());

                    //var regTab = /\t/g;
                    //var regSalto = /\r\n/g;
                    var regTab = new RegExp(/\t/g);
                    //var regSalto = new RegExp(/\r\n/g);
                    var regSalto = new RegExp(/\n/g); /* 11/02/2014: En Chrome, no interpreta el retorno de carro y no realiza el reemplazo. Lo dejamos en el salto de línea */

                    //var sResultado = ret.replace(regTab,"#tab#").replace(regSalto,"#sal#");
                    //alert(sResultado);
                    sb.Append("cargar@#@");
                    sb.Append(ret.replace(regTab, "#tab#").replace(regSalto, "#sal#"));

                    RealizarCallBack(sb.ToString(), "");
                }
            });

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a cargar la lista.", e.message);
    }
}