var aFilas;
var sValorNodo = "";
var sValorGF = "";
function init(){
    try{
   
        $I("hdnAnomes").value = dMSC;
        $I("txtMesCierre").value = AnoMesToMesAnoDescLong(dMSC);
        aFilas = FilasDe("tblDatos");
		scrollTabla();
		ocultarProcesando();
		if ((es_rgf == "S") || (es_administrador == "A")) $I("rdbAmbito").style.display = "block";
		else $I("rdbAmbito").style.display = "none";
		
		$I("lblNodo").style.display = "none";
		$I("lblGF").style.display = "none";
		$I("ambCR").style.display = "none";
		$I("ambGF").style.display = "none";
	    $I("gomaNodo").style.display = "none";
		
	    if (es_administrador == "A" || es_administrador == "SA") {
		    $I("gomaGF").style.display = "none";
		}

	    if (es_administrador == "A" || es_administrador == "SA") {
		    $I("lblNodo").className = "enlace";
		    $I("lblNodo").onclick = function() { getNodo() };
		    $I("lblGF").className = "enlace";
		    $I("lblGF").onclick = function() { obtenerGF() };

		    if ($I("hdnIdNodo").value != "") {
		        $I("lblNodo").style.display = "block";
		        $I("ambCR").style.display = "block";
		        $I("gomaNodo").style.display = "block";

		        sValorNodo = $I("hdnIdNodo").value;
		        $I("rdbAmbito_0").checked = true;
		    }
		    else {
		        $I("lblGF").style.display = "block";
		        $I("ambGF").style.display = "block";
		        $I("gomaGF").style.display = "block";

		        sValorGF = $I("hdnIdGF").value;
		        $I("rdbAmbito_1").checked = true;
		    }
		} else {
		    $I("lblNodo").style.display = "none";
		    $I("ambCR").style.display = "none";
		    
		    $I("lblGF").style.display = "none";
		    $I("ambGF").style.display = "none";

		    if ($I("hdnIdNodo").value != "") {
		        $I("lblNodo").style.display = "block";
		        $I("ambCR").style.display = "block";
		        sValorNodo = $I("cboCR").value;
		        if (es_rgf == "S") $I("rdbAmbito_0").checked = true;
		    }
		    else 
		    {
		        if (es_rgf == "S") {
		            $I("lblGF").style.display = "block";
		            $I("ambGF").style.display = "block";
		            sValorGF = $I("cboGF").value;
		            $I("rdbAmbito_1").checked = true;
		        }
		        else {
		            $I("lblNodo").style.display = "block";
		            $I("ambCR").style.display = "block";		        
		        }
		    }
		}
        setEstadoChecks();

        $I("lblOtrosNodos").title = "Muestra consumos reportados de empleados internos pertenecientes a " + $I("lblNodo").innerText + "s diferentes al seleccionado";
        $I("lblExternos").title = "Muestra consumos reportados de colaboradores externos en proyectos del " + $I("lblNodo").innerText + " seleccionado";
        $I("lblForaneos").title = "Muestra consumos reportados de foráneos en proyectos del " + $I("lblNodo").innerText + " seleccionado";
        
        setExcelImg("imgExcel", "divCatalogo");

        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
var sAmb = "";
function seleccionAmbito(strRblist) {
    try {
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else {
            borrarCatalogo();

            $I("chkExternos").checked = false;
            $I("chkOtroNodo").checked = false;
            $I("chkForaneos").checked = false;
            
            sValorNodo = "";
            sValorGF = "";
            
            $I("hdnIdNodo").value = "";
            $I("hdnIdGF").value = "";

            if (es_administrador == "A" || es_administrador == "SA") {
                $I("txtDesNodo").value = "";
                $I("txtGF").value = "";
                $I("gomaNodo").style.display = "none";
                $I("gomaGF").style.display = "none";
            }
                                               
            $I("lblNodo").style.display = "none";
            $I("lblGF").style.display = "none";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";

            switch (sOp) {
                case "C":
                    $I("lblNodo").style.display = "block";
                    $I("ambCR").style.display = "block";
                    if (es_administrador == "A" || es_administrador == "SA")
                        $I("gomaNodo").style.display = "block";
                    else
                        $I("cboCR").value = "";
                    break;
                case "G":                    
                    $I("lblGF").style.display = "block";
                    $I("ambGF").style.display = "block";
                    if (es_administrador == "A" || es_administrador == "SA")
                        $I("gomaGF").style.display = "block";
                    else $I("cboGF").value = "";
                    break;
            }
            setEstadoChecks();
            //if ($I("chkActuAuto").checked) buscar();
            sAmb = sOp;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
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
                if (aResul[2] != "0") mmoff("Suc","Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("Inf", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                ocultarProcesando();
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 180);
                ocultarProcesando();
                break;
            case "getPreferencia":
                sValorNodo = "";
                sValorGF = "";
                $I("lblNodo").style.display = "none";
                $I("lblGF").style.display = "none";
                $I("ambCR").style.display = "none";
                $I("ambGF").style.display = "none";
  
                if (es_administrador == "A" || es_administrador == "SA") {
                    $I("gomaNodo").style.display = "none";
                    $I("gomaGF").style.display = "none";
                    if (aResul[2] != "")
                    {
                        $I("lblNodo").style.display = "block";
                        $I("ambCR").style.display = "block";
                        $I("gomaNodo").style.display = "block";

                        sValorNodo = aResul[2];                 
                        $I("hdnIdNodo").value = aResul[2];
                        $I("txtDesNodo").value = aResul[3];
                        $I("rdbAmbito_0").checked = true;
                    }
                    else 
                    {
                        $I("lblGF").style.display = "block";
                        $I("ambGF").style.display = "block";
                        $I("gomaGF").style.display = "block";

                        sValorGF = aResul[10];                     
                        $I("hdnIdGF").value = aResul[10];
                        $I("txtGF").value = aResul[11];
                        $I("rdbAmbito_1").checked = true;                        
                    }
                }
		        else {
		            $I("lblNodo").style.display = "none";
		            $I("ambCR").style.display = "none";
        		    
		            $I("lblGF").style.display = "none";
		            $I("ambGF").style.display = "none";

		            if (aResul[2] != "") {
		                $I("lblNodo").style.display = "block";
		                $I("ambCR").style.display = "block";
		                $I("cboCR").value = aResul[2];
		                sValorNodo = $I("cboCR").value;
		                if (es_rgf == "S") $I("rdbAmbito_0").checked = true;
		            }
		            else 
		            {
		                if (es_rgf == "S") {
		                    $I("lblGF").style.display = "block";
		                    $I("ambGF").style.display = "block";
		                    $I("cboGF").value = aResul[10];
		                    sValorGF = $I("cboGF").value;
		                    $I("rdbAmbito_1").checked = true;
		                }
		            }
		        }
		
                if (aResul[4] == "0") {
                    $I("rdbIncompletos_0").checked = true;
                    $I("cboIncompletos").disabled = true;
                }
                else {
                    $I("rdbIncompletos_1").checked = true;
                    $I("cboIncompletos").value = aResul[4];
                    $I("cboIncompletos").disabled = false;
                }
                $I("chkExternos").checked = (aResul[5] == "1") ? true : false;
                $I("chkOtroNodo").checked = (aResul[6] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[7] == "1") ? true : false;
                $I("chkRPA").checked = (aResul[8] == "1") ? true : false;
                $I("chkForaneos").checked = (aResul[9] == "1") ? true : false;
                setEstadoChecks();
                if ($I("chkActuAuto").checked) setTimeout("buscar()", 20);
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
        if ($I("chkActuAuto").checked) buscar();
        else{
            borrarCatalogo();
            ocultarProcesando();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar todos/incompletos", e.message);
    }
}

var NumMes = 0;
function cambiarMes(nMes){
    try{
        NumMes = 0;
        var iAno, iMes
        iAno=$I("hdnAnomes").value.substring(0,4);
        iMes=$I("hdnAnomes").value.substring(4,6);
        if (nMes == -1){
            if (iMes == "01"){
                iMes = "12";
                iAno = parseInt(iAno,10)-1;
            }else{
                iMes = parseInt(iMes,10)-1;
                if (parseInt(iMes,10) < 10) iMes = "0" + iMes;
            }
        }else{
            if (iMes == "12"){
                iMes = "01";
                iAno = parseInt(iAno,10)+1;
            }else{
                iMes = parseInt(iMes,10)+1;
                if (parseInt(iMes,10) < 10) iMes = "0" + iMes;
            }
        }
        $I("hdnAnomes").value =  iAno + iMes;
        $I("txtMesCierre").value =  AnoMesToMesAnoDescLong($I("hdnAnomes").value);
        
        if ($I("chkActuAuto").checked) buscar();
        else{
            borrarCatalogo();
            ocultarProcesando();
        }
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes-2", e.message);
	}
}

function excel(){
    try{
        //if (tblDatos==null || aFila==null || aFila.length==0){
        var tblDatos = $I("tblDatos");
        if (tblDatos==null || tblDatos.rows.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        aFilas = FilasDe("tblDatos");
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Tipo</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Profesional</TD>"); ;
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Centro de responsabilidad</TD>"); ;
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>UDR</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Calendario</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>DLC</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>DLR</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>HLC</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>HR</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>JR</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>HE</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>JE</TD>");
		sb.Append("	</TR>");

	    for (var i=0;i < aFilas.length; i++){
	        sb.Append("<tr>");
	        for (var x=0;x < 11; x++){
	            if (x==0){
	                if (aFilas[i].getAttribute("tipo") == "P")
	                    sb.Append("<td>Interno</td>");
	                else {
	                    if (aFilas[i].getAttribute("tipo") == "F")
	                        sb.Append("<td>Foráneo</td>");
	                    else
	                        sb.Append("<td>Externo</td>");
	                }
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
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    setEstadoChecks();
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener el nodo.", e.message);
    }
}
function obtenerGF() {
    try {
        var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/GrupoFuncional/obtenerGF_SS.aspx";
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorGF = aDatos[0];
                    $I("hdnIdGF").value = aDatos[0];
                    $I("txtGF").value = aDatos[1];
                    setEstadoChecks();
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el grupo funcional", e.message);
    }
}
function borrarCatalogo(){
    try{
        $I("divCatalogo2").innerHTML = "";           
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}
function borrarGF(){
    try {
        //mostrarProcesando();
        $I("hdnIdGF").value = "";
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("txtGF").value = "";
        }
        else {
            $I("cboGF").value = "";
        }
        sValorGF = "";
        setEstadoChecks();
        borrarCatalogo();
        //if ($I("chkActuAuto").checked) buscar();
        //else ocultarProcesando();            
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el GF.", e.message);
    }
}    
function borrarNodo(){
    try{
        //mostrarProcesando();
        $I("hdnIdNodo").value = "";        
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("txtDesNodo").value = "";
        }
        else {
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        setEstadoChecks();
        borrarCatalogo();
        //if ($I("chkActuAuto").checked) buscar();
        //else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el Nodo.", e.message);
    }
}
function setCombo(){
    try{
        setEstadoChecks();
        borrarCatalogo();
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function setEstadoChecks(){
    try{
       $I("chkExternos").disabled = true;
       $I("chkOtroNodo").disabled = true;
       $I("chkForaneos").disabled = true;
       if (sValorNodo!="")
       {
            $I("chkExternos").disabled = false;
            $I("chkOtroNodo").disabled = false;
            $I("chkForaneos").disabled = false;
       }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el estado de las casillas de verificación.", e.message);
    }
}
function buscar(){
    try {
        if (sValorNodo == "" && sValorGF == "" && (es_rgf == "S" || es_administrador == "A" || es_administrador == "SA")) {
            mmoff("Inf", "Debes indicar un CR o un Grupo funcional.", 300, 3500);
            return;
        }
        if (sValorNodo == "" && sValorGF == "" && (es_rgf != "S" && es_administrador == "")) {
            mmoff("Inf", "Debes indicar un CR.", 300, 3500);
            return;
        }        
        var iAno=$I("hdnAnomes").value.substring(0,4);
        var iMes=$I("hdnAnomes").value.substring(4,6);

        var js_args = "buscar@#@";
        js_args += iMes + "/" + iAno + "@#@";
        js_args += sValorNodo +"@#@";
        js_args += sValorGF + "@#@";
        if (getRadioButtonSelectedValue("rdbIncompletos", true) == "0")
            js_args += "0@#@";
        else
            js_args += $I("cboIncompletos").value +"@#@";

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
            
        if ($I("chkForaneos").checked)
            js_args += "1@#@";
        else
            js_args += "0@#@";
            
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla(){
    try{
        if ($I("divCatalogo2").innerHTML == "") return;
        if ($I("divCatalogo").scrollTop != nTopScroll){
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScroll/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){                  
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {                    
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                switch (oFila.getAttribute("tipo")) {   
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
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
    try {
        if (sValorNodo == "" && sValorGF == "" && (es_rgf == "S" || es_administrador == "A" || es_administrador == "SA")) {
            mmoff("Inf", "Debes indicar un CR o un Grupo funcional.", 300, 3500);
            return;
        }
        if (sValorNodo == "" && sValorGF == "" && (es_rgf != "S" && es_administrador == "")) {
            mmoff("Inf", "Debes indicar un CR.", 300, 3500);
            return;
        }    
        mostrarProcesando();
        var js_args = "setPreferencia@#@";
        if (getRadioButtonSelectedValue("rdbIncompletos", true) == "0")
            js_args += "0@#@";
        else
            js_args += $I("cboIncompletos").value +"@#@";
        js_args += ($I("chkActuAuto").checked)? "1@#@":"0@#@";
        js_args += sValorNodo+"@#@";
        js_args += ($I("chkExternos").checked)? "1@#@":"0@#@";
        js_args += ($I("chkOtroNodo").checked)? "1@#@":"0@#@";
        js_args += ($I("chkRPA").checked) ? "1@#@" : "0@#@";
        js_args += ($I("chkForaneos").checked) ? "1@#@" : "0@#@";
        js_args += sValorGF + "@#@";    
            
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function getCatalogoPreferencias(){
    try{
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia);
        modalDialog.Show(sPantalla, self, sSize(450, 470))
            .then(function(ret) {
                if (ret != null) {
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
                }
            });
        window.focus();
        
	    ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

function setIncompletos(){
    try{
        if (getRadioButtonSelectedValue("rdbIncompletos", false) == "0"){
            $I("cboIncompletos").disabled = true;
        }else{
            $I("cboIncompletos").disabled = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el filtro.", e.message);
    }
}
///////////////////////