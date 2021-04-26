var aFilas;
function init(){
    try{
   
        $I("hdnDesde").value = dMSC;
        $I("hdnHasta").value = dMSC;
        $I("txtDesde").value = AnoMesToMesAnoDescLong(dMSC);
        $I("txtHasta").value = $I("txtDesde").value;

        setCabecera2($I("hdnModeloCoste").value);

        aFilas = FilasDe("tblDatos");
		scrollTabla();
		ocultarProcesando();
		
		if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
		}
		else sValorNodo = $I("cboCR").value;
        $I("lblInternos").title = "Muestra consumos reportados de empleados pertenecientes al " + $I("lblNodo").innerText + "seleccionado";
        $I("lblOtrosNodos").title = "Muestra consumos reportados de empleados pertenecientes a " + $I("lblNodo").innerText + "s diferentes al seleccionado";
        $I("lblExternos").title = "Muestra consumos reportados de colaboradores externos en proyectos del " + $I("lblNodo").innerText + " seleccionado";
        
        setExcelImg("imgExcel", "divCatalogo");

        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        $I("divCatalogo2").innerHTML = "";
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo2").innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
		        //setTimeout("MostrarIncompletosAux();", 100);
		        scrollTabla();
		        ocultarProcesando();
		        window.focus();
                break;
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                ocultarProcesando();
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.",250);
                ocultarProcesando();
                break;
            case "getPreferencia":
                sValorNodo = aResul[2]; 
                if (es_administrador == "A" || es_administrador == "SA") {
                    $I("hdnIdNodo").value = aResul[2];
                    $I("txtDesNodo").value = aResul[3];
                }else{
                    $I("cboCR").value = aResul[2];
                }        
                $I("chkDelNodo").checked = (aResul[4] == "1") ? true : false;
                $I("chkExternos").checked = (aResul[5] == "1") ? true : false;
                $I("chkOtroNodo").checked = (aResul[6] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[7] == "1") ? true : false;
                $I("chkRPA").checked = (aResul[8] == "1") ? true : false;
                if ($I("chkActuAuto").checked &&
                    ($I("chkDelNodo").checked || $I("chkExternos").checked || $I("chkOtroNodo").checked) &&
                    sValorNodo != "")
                {
                    setOp($I("btnObtener"), 30);
                    setTimeout("buscar()", 20);
                }else setOp($I("btnObtener"), 100);
                ocultarProcesando();
                break;
            
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}

function MostrarIncompletos(){
    try{
        borrarCatalogo();
        if ($I("chkActuAuto").checked) buscar();
        else{
            //borrarCatalogo();
            ocultarProcesando();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar todos/incompletos", e.message);
    }
}

function excel(){
    try{
        //if (tblDatos==null || aFila==null || aFila.length==0){
        if ($I("tblDatos")==null || $I("tblDatos").rows.length==0){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sModeloCoste = getRadioButtonSelectedValue("rdbCoste", false);
        aFilas = FilasDe("tblDatos");
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Tipo</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Profesional</TD>"); ;
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Centro de responsabilidad</TD>"); ;
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Calendario</TD>");
        if (sModeloCoste == "H") {
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Horas laborables</TD>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Horas bajo mi ámbito de visión</TD>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Total Horas</TD>");
        }
        else {
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Días laborables</TD>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Jornadas bajo mi ámbito de visión</TD>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Total jornadas</TD>");
        }
		sb.Append("	</TR>");

	    for (var i=0;i < aFilas.length; i++){
	        sb.Append("<tr>");
	        for (var x=0;x < 6; x++){
	            if (x == 0) {
	                switch (aFilas[i].getAttribute("tipo")) {
	                    case "P":
	                        sb.Append("<td>Del " + $I("lblNodo").innerText + "</td>");
	                        break;
	                    case "N":
	                        sb.Append("<td>De otro " + $I("lblNodo").innerText + "</td>");
	                        break;
	                    case "E":
	                        sb.Append("<td>Externo</td>");
	                        break;
	                    case "F":
	                        sb.Append("<td>Foráneo</td>");
	                        break;
	                }
//	                if (aFilas[i].getAttribute("tipo")=="P")
//	                    sb.Append("<td>Del "+$I("lblNodo").innerText+"</td>");
//	                else{
//	                    if (aFilas[i].getAttribute("tipo")=="N")
//    	                    sb.Append("<td>De otro "+$I("lblNodo").innerText+"</td>");
//	                    else
//	                        sb.Append("<td>Externo</td>");
//	                }
	            }else{
                    sb.Append(aFilas[i].cells[x].outerHTML);
                    if (x==1) sb.Append("<td>"+ Utilidades.unescape(aFilas[i].getAttribute("nodo")) +"</td>");
	            }
	        }
	        sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function getNodo(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
	        .then(function(ret) {
                if (ret != null) {
		            var aDatos = ret.split("@#@");
		            sValorNodo = aDatos[0];
		            $I("hdnIdNodo").value = aDatos[0];
		            $I("txtDesNodo").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
	            }else ocultarProcesando();
            }); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener elemento.", e.message);
    }
}
function borrarCatalogo(){
    try{
        $I("divCatalogo2").innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}

function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
        }else{
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        borrarCatalogo();
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar al limpiar.", e.message);
    }
}
//function setActuAuto(){
//    try{
//        if ($I("chkActuAuto").checked){
//            setOp($I("btnObtener"), 30);
//            buscar();
//        }else{
//            setOp($I("btnObtener"), 100);
//        }

//	}catch(e){
//		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
//	}
//}

function setObtener(){
    try{
        if ($I("chkActuAuto").checked){
            //setOp($I("btnObtener"), 30);
        }else{
            setOp($I("btnObtener"), 100);
        }

	}catch(e){
		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
	}
}

function setCombo(){
    try{
        borrarCatalogo();
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function buscar(){
    try{
        //if (sValorNodo=="")
        //{
        //    mmoff("War", "Debes indicar algún " + $I("lblNodo").innerText + " para realizar la búsqueda.", 400);
        //    return;
        //}
        if ($I("chkDelNodo").checked || $I("chkExternos").checked || $I("chkOtroNodo").checked){
            var js_args = "buscar@#@";
            js_args += $I("hdnDesde").value +"@#@";
            js_args += $I("hdnHasta").value +"@#@";
            
            js_args += sValorNodo +"@#@";
            
            if ($I("chkDelNodo").checked)
                js_args += "1@#@";
            else
                js_args += "0@#@";
            if ($I("chkExternos").checked)
                js_args += "1@#@";
            else
                js_args += "0@#@";
            if ($I("chkOtroNodo").checked)
                js_args += "1@#@";
            else
                js_args += "0@#@";
            if ($I("chkRPA").checked)
                js_args += "1@#@";
            else
                js_args += "0@#@";
                
            js_args += getRadioButtonSelectedValue("rdbCoste", false);

            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        else
            mmoff("War", "Debes indicar algún tipo de profesional para realizar la búsqueda.", 400);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScroll){
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScroll/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 +1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                
                if (oFila.getAttribute("baja")=="1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
////PREFERENCIAS///////
function setPreferencia(){
    try{
        mostrarProcesando();
        var js_args = "setPreferencia@#@";
        js_args += ($I("chkActuAuto").checked)? "1@#@":"0@#@";
        js_args += sValorNodo+"@#@";
        js_args += ($I("chkDelNodo").checked)? "1@#@":"0@#@";
        js_args += ($I("chkExternos").checked)? "1@#@":"0@#@";
        js_args += ($I("chkOtroNodo").checked)? "1@#@":"0@#@";
        js_args += ($I("chkRPA").checked) ? "1@#@" : "0@#@";
        js_args += getRadioButtonSelectedValue("rdbCoste", false);
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function getCatalogoPreferencias(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
	        .then(function(ret) {
	            if (ret != null) {
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
	            }else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

///////////////////////
function getPeriodo(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
	    modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
	            if (ret != null){
	                var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                    MostrarIncompletos();
                    ocultarProcesando();
                }else ocultarProcesando();  
	        }); 	        
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}
function setCabecera(sModeloCoste) {
    borrarCatalogo();
    setCabecera2(sModeloCoste);
}
function setCabecera2(sModeloCoste) {
    if (sModeloCoste == "H") {
        $I("cabCal").innerHTML = "HLC";
        $I("cabCal").title = "Horas laborables del calendario";
        $I("cabMias").innerHTML = "Horas ámbito";
        $I("cabMias").title = "Horas económicas en proyectos de mi ámbito de visión";
        $I("cabTodas").innerHTML = "Total Horas";
        $I("cabTodas").title = "Horas económicas en todos los proyectos";
    }
    else {
        $I("cabCal").innerHTML = "JLC";
        $I("cabCal").title = "Jornadas laborables del calendario";
        $I("cabMias").innerHTML = "Jornadas ámbito";
        $I("cabMias").title = "Jornadas económicas en proyectos de mi ámbito de visión";
        $I("cabTodas").innerHTML = "Total Jornadas";
        $I("cabTodas").title = "Jornadas económicas en todos los proyectos";
    }
}