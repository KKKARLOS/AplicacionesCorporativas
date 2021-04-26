function init(){
    try{
        ToolTipBotonera("procesar", "Procesa las filas correctas del fichero cargado");

        if ($I("hdnErrores").value != "") {
            AccionBotonera("procesar", "D");
            return;
        }
        
//        if ($I("cldTotalLin").innerText == "0")
//        {
//            ocultarProcesando();
//            alert("Tabla T495_CONSUCONTACORO vacía");
//        }
//        else if ($I("cldTotalLin").innerText == $I("cldLinOK").innerText && $I("cldTotalLin").innerText != "0")
//        {
//           AccionBotonera("procesar", "H");
//        }
//        else if ($I(tblErrores).rows.length > 0)
//        {
//            ocultarProcesando();
        //            mmoff("Err", "Se han detectado errores en el proceso verificación"), 340);
//        }

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
		mostrarErrorAplicacion("Error al ir a mostrar T495_CONSUCONTACORO", e.message);
    }
}


function procesar(){
    try{
        if ($I("tblErrores").rows.length != 0) {
            ocultarProcesando();
            jqConfirm("", "Las filas erróneas no serán tratadas.<br><br>¿Deseas continuar?", "", "", "war", 300).then(function (answer) {
                if (!answer) {
                    return;
                }
                LLamadaProcesar();
                return;
            });
        }
        else {
            LLamadaProcesar();
            return;
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}
function LLamadaProcesar() {
    try {
        AccionBotonera("procesar", "D");

        var js_args = "procesar@#@";
        js_args += $I("txtAnnoVisible").value;

        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaProcesar", e.message);
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
                mmoff("SucPer", "Proceso finalizado correctamente", 300);
                break;   
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
           case "getConsuContaCoro":
                $I("divB").innerHTML = aResul[2];
                $I("cldTotalLin").innerText = aResul[3];
                $I("cldLinOK").innerText = aResul[4];
                $I("cldLinErr").innerText = aResul[5];
                
                if ($I("cldTotalLin").innerText == "0")
                {
                    ocultarProcesando();
                    mmoff("War","Tabla T495_CONSUCONTACORO vacía",200);
                    AccionBotonera("procesar", "D");
                }
                else if ($I("cldTotalLin").innerText == $I("cldLinOK").innerText && $I("cldTotalLin").innerText != "0")
                {
                    AccionBotonera("procesar", "H");
                }
                else if ($I(tblErrores).rows.length > 0)
                {
                    AccionBotonera("procesar", "H");
                    ocultarProcesando();
                    mmoff("Err", "Se han detectado errores en el proceso verificación", 340);
                }

                if ($I("cldTotalLin").innerText != "0"){
                    $I("lblTabla").className = "enlace";
                    $I("lblTabla").onclick = function (){mostrarTabla()};
                }else{
                    $I("lblTabla").className = "texto";
                    $I("lblTabla").onclick = null;
                }
               
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
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>"+ strEstructuraNodoCorta +"</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proyecto</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Año/Mes</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Clase</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proveedor</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Descripción</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Error</td>");
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

function cambiarAnno(nAnno){
    try{
        mostrarProcesando();
        $I("txtAnnoVisible").value = parseInt($I("txtAnnoVisible").value, 10) + nAnno;

        setTimeout("borrarCatalogo()", 20);
       
//        var js_args = "getConsuContaCoro@#@";
//        js_args += $I("txtAnnoVisible").value;

//        RealizarCallBack(js_args, "");
    }catch(e){
		mostrarErrorAplicacion("Error al cambiar de año", e.message);
	}
}

function borrarCatalogo() {
    try {
        $I("divB").innerHTML = "";
        $I("cldTotalLin").innerText = "0";
        $I("cldLinOK").innerText = "0";
        $I("cldLinErr").innerText = "0";

        $I("lblTabla").className = "texto";
        $I("lblTabla").onclick = null;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar de año", e.message);
    }
}

function obtener() {
    try {
        mostrarProcesando();

        var js_args = "getConsuContaCoro@#@";
        js_args += $I("txtAnnoVisible").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar de año", e.message);
    }
}