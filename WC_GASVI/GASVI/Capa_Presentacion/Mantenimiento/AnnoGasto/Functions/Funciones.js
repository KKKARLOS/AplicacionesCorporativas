var bRegresar = false;

function init() {
    try{
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Año gasto";
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
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                }
                else{
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") == "D") tblAnnoGasto.deleteRow(i);
                        else mfa(aFila[i], "N");
                    }
                }
                ot("tblAnnoGasto", 1, 0, "");
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

function comprobarFecha(strId){
    if (DiffDiasFechas($I("desde" + strId).value ,$I("hasta" + strId).value) < 0){
         mmoff("War", "La fecha desde tiene que ser anterior a la fecha hasta.",350,2000);
         $I("hasta" + strId).value = "";
    }
}

function comprobarDatos(){
    aFila = FilasDe("tblAnnoGasto");
    for (var i=0; i<aFila.length; i++){
        if (aFila[i].getAttribute("bd") != "D"){
            if ($I("anno" + aFila[i].id).value == ""){
                mmoff("War", "Debe indicar un año.",180);
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                aFila[i].cells[1].children[0].focus();
                return false;
            }            
            if ($I("desde" + aFila[i].id).value == ""){
                mmoff("War", "Debe indicar la fecha desde.",250);
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                aFila[i].cells[1].children[0].focus();
                return false;
            }
            if ($I("hasta" + aFila[i].id).value == ""){
                mmoff("War", "Debe indicar la fecha hasta.",250);
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                aFila[i].cells[1].children[0].focus();
                return false;
            }
        }
        
        var strDesc = $I("anno" + aFila[i].id).value;
        for (var x=0; x<aFila.length; x++){
            if (aFila[i].getAttribute("bd") != "D" && aFila[x].getAttribute("bd") != "D" && i != x){
                if (strDesc == aFila[x].cells[1].children[0].value){
                    mmoff("War", "No se permiten dos o más años con la misma descripción. (" + strDesc + ")",425,3000);
                    if (ie) aFila[x].click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        aFila[x].dispatchEvent(clickEvent);
                    }
                    return false;
                }
            }
        }
    }
    return true;    
}

function flGetAnnoGasto(){
    /*Recorre la tabla de Anno Gasto para obtener una cadena que se pasará como parámetro
      al procedimiento de grabación
    */
    var sRes = "", sIU = "", sD = "";
    var bGrabar = false, bActivo = false;
    try{
        aFila = FilasDe("tblAnnoGasto");
        for (i=0; i<aFila.length; i++){
            switch (aFila[i].getAttribute("bd")) {
                case "D":
                    sD = aFila[i].getAttribute("bd") + "#sCad#" + tblAnnoGasto.rows[i].id + "#sFin#";
                case "I":
                case "U":
                    sIU += aFila[i].getAttribute("bd") + "#sCad#" + tblAnnoGasto.rows[i].id;
                    sIU += "#sCad#" + $I("anno" + tblAnnoGasto.rows[i].id).value + "#sCad#";
                    sIU += $I("desde" + tblAnnoGasto.rows[i].id).value + "#sCad#" + $I("hasta" + tblAnnoGasto.rows[i].id).value + "#sFin#";
            }
        }
        sRes = sD + sIU;
        if (sRes != "") sRes = sRes.substring(0, sRes.length - 6);
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}

function grabar(){
    try{
        if (iFila >= 0) modoControles(tblAnnoGasto.rows[iFila], false);
        if (!comprobarDatos()) return;
       
        js_args = "grabar@#@";
        js_args += flGetAnnoGasto();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

var nNuevaFila = 30000;
function nuevo(){
    try {
        aFila = FilasDe("tblAnnoGasto");
        if (iFila >= 0) modoControles(tblAnnoGasto.rows[iFila], false);
        oNF = $I("tblAnnoGasto").insertRow(-1);
        nNuevaFila++;
        var anno = 0;
        if (aFila.length-1 == 0) {
            var oDiaActual = new Date();
            anno = oDiaActual.getFullYear();           
        }
        else {
            var maxFila = -1;
            for (var i = 0, nCount = aFila.length - 1; i < nCount; i++) {
                if (maxFila < parseInt(aFila[i].children[1].children[0].value, 10)) maxFila = parseInt(aFila[i].children[1].children[0].value, 10);
            }
            anno = maxFila + 1;
        }
        
        var desde = "01/01/" + anno;
        var hasta = "31/12/" + anno;
        oNF.id = nNuevaFila; 
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:20px;");
        oNF.onclick = function (e){mm(e);};

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        
        var oCtrl = document.createElement("input");
        oCtrl.id = "anno" + oNF.id;
        oCtrl.type = "text";
        oCtrl.className = "txtNumL";
        oCtrl.setAttribute("style", "width:35px; text-align:left");
        oCtrl.onchange = function() {activarGrabar(); };
        oCtrl.onkeypress = function() {vtn2(event); };
        oCtrl.value = anno;
        oCtrl.setAttribute("maxLength", "4");
        oNF.insertCell(-1).appendChild(oCtrl);
        
        var oCtr2 = document.createElement("input");
        oCtr2.id = "desde" + oNF.id;
        oCtr2.type = "text";
        oCtr2.className = "txtNumL";
        oCtr2.setAttribute("style", "width:60px;");
        oCtr2.setAttribute("Calendar","oCal");
        oCtr2.setAttribute("goma","0");
        oCtr2.onclick = function() {mc(this); };
        oCtr2.onchange = function() {comprobarFecha(oNF.id); activarGrabar(); };
        oCtr2.value = desde;
        oCtr2.setAttribute("ReadOnly", true);
        oNF.insertCell(-1).appendChild(oCtr2);
        
        
	    var oCtr3 = document.createElement("input");
        oCtr3.id = "hasta" + oNF.id;
        oCtr3.type = "text";
        oCtr3.className = "txtNumL";
        oCtr3.setAttribute("style", "width:60px;");
        oCtr3.setAttribute("Calendar","oCal");
        oCtr3.setAttribute("goma","0");
        oCtr3.onclick = function() {mc(this); };
        oCtr3.onchange = function() {comprobarFecha(oNF.id); activarGrabar(); };
        oCtr3.value = hasta;
        oCtr3.setAttribute("ReadOnly", true);
        oNF.insertCell(-1).appendChild(oCtr3);
	    
	    oNF.cells[1].children[0].focus();
        activarGrabar();
        if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
	 
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}

function eliminar(){
    try{
        var sw = 0;
        var sw2 = 0;
        aFila = FilasDe("tblAnnoGasto");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw2 = 1;
                if (aFila[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    $I("tblAnnoGasto").deleteRow(i);
                }    
                else{
                    mfa(aFila[i],"D");
                    sw = 1;
                }
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("War", "Debe seleccionar la fila a eliminar",250);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}

function excelAnnoGasto(){
    try{
        if ($I("tblAnnoGasto") == null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
	    sb.Append("	<tr style='text-align:center'>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Año</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Desde</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Hasta</td>");
	    sb.Append("	</tr>");
        for (var i=0; i<tblAnnoGasto.rows.length; i++){
            sb.Append("<tr>");
            sb.Append("<td style='align:right;'>" + $I("anno" + tblAnnoGasto.rows[i].id ).value + "</td>");
            sb.Append("<td style='align:right;'>" + $I("desde" + tblAnnoGasto.rows[i].id ).value + "</td>");
            sb.Append("<td style='align:right;'>" + $I("hasta" + tblAnnoGasto.rows[i].id ).value + "</td>");
            sb.Append("</tr>");
        }	
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
	    sb.Append("	</tr>");
        sb.Append("</table>");
        crearExcel(sb.ToString());
        var sb = null;
    }catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos para generar el archivo excel con los años de gastos", e.message);
    }
}

