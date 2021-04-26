var nProyecto = "";

function init(){
    try{
        nNE=1;
        setNE(nNE);
        aFila = FilasDe("tblDatos");
        recolorear();
        calcularAcumulados();

        //setExcelImg("imgExcel", "divCatalogo");
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
        switch (aResul[0])
        {
            case "getProyectos":
                var aDatos = aResul[2].split("///");
                var j=1;
                $I("cboProyecto").length = 1;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1],aValor[0]); 
                    $I("cboProyecto").options[j] = opcion;
			        j++;
                }
                $I("cboProyecto").value = nProyecto;
                nProyecto = $I("cboProyecto").value;
                setTimeout("buscar();", 20);
                break;
            case "buscar":
                $I("divCatalogo").innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        nNE=1;
                setNE(nNE);
                aFila = FilasDe("tblDatos");
		        recolorear();
                calcularAcumulados();
                break;
        }
        ocultarProcesando();
    }
}

function buscar(){
   try{	 
        borrarCatalogo();
        
     	if (($I('txtDesde').value=="") || ($I('txtHasta').value=="")) 
  	    {
  	        mmoff("Inf","Debes indicar el periodo temporal.",340);
  	        return;
  	    }
        nProyecto = $I("cboProyecto").value;
        var js_args = "buscar@#@";
        js_args += nProyecto + "@#@";
        js_args += $I("txtDesde").value + "@#@";
        js_args += $I("txtHasta").value;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos de la consulta", e.message);
    }
}

function recolorear(){
    try{
        if ($I("tblDatos").rows.length == 0) return;
        
        var nIndice1 = 0;
        var nIndice2 = 0;
        var nIndice3 = 0;
       
        for (var i=0; i<$I("tblDatos").rows.length;i++){
            if ($I("tblDatos").rows[i].style.display == "none") continue;

            if ($I("tblDatos").rows[i].getAttribute("tipo") == "PE" || $I("tblDatos").rows[i].getAttribute("tipo") == "PT" || $I("tblDatos").rows[i].getAttribute("tipo") == "F" || $I("tblDatos").rows[i].getAttribute("tipo") == "A") {
            // PE, proy.técnico, fase, actividad
                if (nIndice1 % 2 == 0) {
                    //(ie) ? $I("tblDatos").rows[i].className = "FA" : $I("tblDatos").rows[i].setAttribute("class", "FA");
                    $I("tblDatos").rows[i].className = "FA";
                }
                else {
                    //(ie) ? $I("tblDatos").rows[i].className = "FB" : $I("tblDatos").rows[i].setAttribute("class", "FB");
                    $I("tblDatos").rows[i].className = "FB";
                }
                nIndice1++;
            }

            if ($I("tblDatos").rows[i].getAttribute("tipo") == "T") {
               // tareas
               if (nIndice2 % 2 == 0) {
                   //(ie) ? $I("tblDatos").rows[i].className = "FAM1" : $I("tblDatos").rows[i].setAttribute("class", "FAM1");
                   $I("tblDatos").rows[i].className = "FAM1";
               }
               else {
                   //(ie) ? $I("tblDatos").rows[i].className = "FAM2" : $I("tblDatos").rows[i].setAttribute("class", "FAM2");
                   $I("tblDatos").rows[i].className = "FAM2";
               }            
               nIndice2++;
            }
            else if ($I("tblDatos").rows[i].getAttribute("tipo") != "C") nIndice2 = 0;

            // consumos
            if ($I("tblDatos").rows[i].getAttribute("tipo") == "C") {
                if (nIndice3 % 2 == 0) {
                    //(ie) ? $I("tblDatos").rows[i].className = "FAM3" : $I("tblDatos").rows[i].setAttribute("class", "FAM3");
                    $I("tblDatos").rows[i].className = "FAM3";
                }
                else {
                    //(ie) ? $I("tblDatos").rows[i].className = "FAM4" : $I("tblDatos").rows[i].setAttribute("class", "FAM4");
                    $I("tblDatos").rows[i].className = "FAM4";
                }              
                nIndice3++;
            }else nIndice3 = 0;            
        }
	}catch(e){
		mostrarErrorAplicacion("Error al recolorear las filas de la tabla", e.message);
    }
}

function mostrar(oImg){
    try{
        var nIndexFila = oImg.parentNode.parentNode.rowIndex;
        var nNivel = oImg.parentNode.parentNode.getAttribute("nivel");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar

        for (var i = nIndexFila + 1; i < $I("tblDatos").rows.length; i++) {

            if ($I("tblDatos").rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O")
                {
                    $I("tblDatos").rows[i].style.display = "none";
                    if ($I("tblDatos").rows[i].getAttribute("N") != null)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }
                else{
                    if ($I("tblDatos").rows[i].getAttribute("nivel") - 1 == nNivel)
                        $I("tblDatos").rows[i].style.display = "table-row";
                }
            }
            else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../images/plus.gif";
        else oImg.src = "../../../images/minus.gif"; 
        recolorear();    
        
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(obj){
    try{
        if ($I("divCatalogo").innerHTML == ""){
            ocultarProcesando();
            return;
        }
        if ($I("tblDatos").rows.length == 0) {
            ocultarProcesando();
            return;
        }
        if (obj == 0){ //Contraer
            var j = 0;
            for (var i = 0; i < $I("tblDatos").rows.length; i++) {

                if ($I("tblDatos").rows[i].getAttribute("nivel") > 1)
                {
                    if ($I("tblDatos").rows[i].getAttribute("N") != null)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                    $I("tblDatos").rows[i].style.display = "none";
                }
                else 
                {
                    $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                    if (j % 2 == 0)
                        //(ie) ? $I("tblDatos").rows[i].className = "FA" : $I("tblDatos").rows[i].setAttribute("class", "FA");
                        $I("tblDatos").rows[i].className = "FA";
                    else
                    //(ie) ? $I("tblDatos").rows[i].className = "FB" : $I("tblDatos").rows[i].setAttribute("class", "FB");
                        $I("tblDatos").rows[i].className = "FB";                    
					j++;
                }                
            }
        }else{ //Expandir
        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
                //alert("Fila: " +i + " tipo: " + tblDatos.rows[i].tipo);
            if (bNivelPedido(nNE, $I("tblDatos").rows[i].getAttribute("tipo"))) {
                if ($I("tblDatos").rows[i].getAttribute("N") != null)
                    $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                $I("tblDatos").rows[i].style.display = "table-row";
                }
                else 
                {
                    if ($I("tblDatos").rows[i].getAttribute("N") != null) {
                        if (bNivelInferior(nNE, $I("tblDatos").rows[i].getAttribute("tipo"))) {
                            $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/minus.gif";
                            $I("tblDatos").rows[i].style.display = "table-row";
                        }
                    }
                }
            }
            recolorear();
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer global", e.message);
    }
}
function bNivelPedido(nNivelPedido, sTipo){
    var bRes=false;
    try{
        switch (nNivelPedido){
            case 1:
                if (sTipo=="PE") bRes=true;
                break;
            case 2:
                //if (sTipo=="PE" || sTipo=="PT") bRes=true;
                if (sTipo=="PT") bRes=true;
                break;
            case 3:
                //if (sTipo=="PE" || sTipo=="PT" || sTipo=="F" ||sTipo=="A" || sTipo=="T") bRes=true;
                if (sTipo=="T") bRes=true;
                break;
            case 4:
                if (sTipo=="C") bRes=true;
                break;
        }
        return bRes;
    }
	catch(e){
		mostrarErrorAplicacion("Error al determinar nivel mostrable", e.message);
    }
}
function bNivelInferior(nNivelPedido, sTipo){
    var bRes=false;
    try{
        switch (nNivelPedido){
            case 2:
                if (sTipo=="PE") bRes=true;
                break;
            case 3:
                if (sTipo=="PE" || sTipo=="PT" || sTipo=="F" || sTipo=="A") bRes=true;
                break;
            case 4:
                bRes=true;
                break;
        }
        return bRes;
    }
	catch(e){
		mostrarErrorAplicacion("Error al determinar nivel mostrable(2)", e.message);
    }
}
function borrarCatalogo(){
    try{
        $I("divCatalogo").innerHTML = "";
		$I("txtHorasReportadas").title = "";

		//if (ie) 
		    $I("txtHorasReportadas").innerText = "0,00";
		//else $I("txtHorasReportadas").textContent = "0,00"; 

		$I("txtJornadasReportadas").title = "";

		//if (ie) 
		    $I("txtJornadasReportadas").innerText = "0,00";
		//else $I("txtJornadasReportadas").textContent = "0,00"; 

	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function calcularAcumulados(){
    try{
        var nTH = 0, nTJ = 0;
        var nAH = 0, nAJ = 0;
        var nFH = 0, nFJ = 0;
        var nPTH= 0, nPTJ= 0;
        var nPEH= 0, nPEJ= 0;
        var nTotH= 0;
        var nTotJ= 0;
        //var nTotE= 0;
        var nH=0, nJ=0;
        
        for (var i=aFila.length-1; i>=0; i--) {
            if (aFila[i].getAttribute("tipo") == "C") {//Consumo
                nH = parseFloat(dfn(aFila[i].getAttribute("nH")));
                nJ = parseFloat(dfn(aFila[i].getAttribute("nJ")));
                nTH += nH;
                nTJ += nJ;
                if (aFila[i].getAttribute("nivel") >= 5) {
                    nAH += nH;
                    nAJ += nJ;
                }
                if (aFila[i].getAttribute("nivel") == 6) {
                    nFH += nH;
                    nFJ += nJ;
                }
                nPTH += nH;
                nPTJ += nJ;
                nPEH += nH;
                nPEJ += nJ;
                nTotH += nH;
                nTotJ += nJ;
            } else if (aFila[i].getAttribute("tipo") == "T") {//Tarea
                aFila[i].cells[1].title = nTH.ToString();
                aFila[i].cells[2].title = nTJ.ToString();

                //if (ie) 
                aFila[i].cells[1].innerText = nTH.ToString("N");
                //else aFila[i].cells[1].textContent = nTH.ToString("N");

                //if (ie) 
                aFila[i].cells[2].innerText = nTJ.ToString("N");
                //else aFila[i].cells[2].textContent = nTJ.ToString("N");
                
                nTH = 0; nTJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "A") {//ACTIVIDAD
                aFila[i].cells[1].title = nAH.ToString();
                aFila[i].cells[2].title = nAJ.ToString();

                //if (ie) 
                aFila[i].cells[1].innerText = nAH.ToString("N");
                //else aFila[i].cells[1].textContent = nAH.ToString("N");

                //if (ie) 
                aFila[i].cells[2].innerText = nAJ.ToString("N");
                //else aFila[i].cells[2].textContent = nAJ.ToString("N");
                                
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "F") {//FASE
                aFila[i].cells[1].title = nFH.ToString();
                aFila[i].cells[2].title = nFJ.ToString();

                //if (ie) 
                aFila[i].cells[1].innerText = nFH.ToString("N");
                //else aFila[i].cells[1].textContent = nFH.ToString("N");

                //if (ie) 
                aFila[i].cells[2].innerText = nFJ.ToString("N");
                //else aFila[i].cells[2].textContent = nFJ.ToString("N");
                                
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "PT") {//PT
                aFila[i].cells[1].title = nPTH.ToString();
                aFila[i].cells[2].title = nPTJ.ToString();

                //if (ie) 
                aFila[i].cells[1].innerText = nPTH.ToString("N");
                //else aFila[i].cells[1].textContent = nPTH.ToString("N");

                //if (ie) 
                aFila[i].cells[2].innerText = nPTJ.ToString("N");
                //else aFila[i].cells[2].textContent = nPTJ.ToString("N");
                                
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
                nPTH = 0; nPTJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "PE") {//PE
                aFila[i].cells[1].title = nPEH.ToString();
                aFila[i].cells[2].title = nPEJ.ToString();

                //if (ie) 
                aFila[i].cells[1].innerText = nPEH.ToString("N");
                //else aFila[i].cells[1].textContent = nPEH.ToString("N");

                //if (ie) 
                aFila[i].cells[2].innerText = nPEJ.ToString("N");
                //else aFila[i].cells[2].textContent = nPEJ.ToString("N");
                                
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
                nPTH = 0; nPTJ = 0;
                nPEH = 0; nPEJ = 0;
            }            
        }     

		$I("txtHorasReportadas").title = nTotH.ToString();

		//if (ie) 
		$I("txtHorasReportadas").innerText = nTotH.ToString("N");
		//else $I("txtHorasReportadas").textContent = nTotH.ToString("N");

		//if (ie) 
		$I("txtJornadasReportadas").innerText = nTotJ.ToString("N");
		//else $I("txtJornadasReportadas").textContent = nTotJ.ToString("N"); 

		
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los acumulados", e.message);
    }
}
function excel(){
    try{
        if ($I("tblDatos")==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='width:30px;background-color: #BCD4DF;'>Tipo</TD>");
        sb.Append("        <td style='width:500px;background-color: #BCD4DF;'>Estructura técnica / F. consumo / Comentarios</TD>");
        sb.Append("        <td style='width:60px;background-color: #BCD4DF;'>Horas</TD>");
        sb.Append("        <td style='width:60px;background-color: #BCD4DF;'>Jornadas</TD>");
		sb.Append("	</TR>");

	    for (var i=0;i < aFila.length; i++){
		    if (aFila[i].style.display == "none") continue;
		    
	        sb.Append("<tr style='vertical-align:top;'>");
	        sb.Append("<td>");
	        if (aFila[i].getAttribute("tipo") != "C")
	            sb.Append(aFila[i].getAttribute("tipo"));
	        sb.Append("</td>");
            for (var x=0; x<=2;x++){
                sb.Append("<td>");
                
                if (x==0){
                    if (aFila[i].getAttribute("nivel") == "1") sb.Append("");
                    else if (aFila[i].getAttribute("nivel") == "2") sb.Append("&nbsp;&nbsp;");
                    else if (aFila[i].getAttribute("nivel") == "3") sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    else if (aFila[i].getAttribute("nivel") == "4") sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    else if (aFila[i].getAttribute("nivel") == "5") sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    else if (aFila[i].getAttribute("nivel") == "6") sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                }

                //if (ie) 
                sb.Append(aFila[i].cells[x].innerText);
                //else sb.Append(aFila[i].cells[x].textContent);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }

	    var aFilaRes = FilasDe("tblResultado");
	    for (var i=0;i < aFilaRes.length; i++){
	        sb.Append("<tr>");
	        sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
	        sb.Append("<td style='background-color: #BCD4DF;'>Total</td>");
            for (var x=1; x<=2;x++){
                if (x>0)
                    sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>");
                else
                    sb.Append("<td style='background-color: #BCD4DF;'>");

                //if (ie) 
                sb.Append(aFilaRes[i].cells[x].innerText);
                //else sb.Append(aFilaRes[i].cells[x].textContent); 
                               
                sb.Append("</td>");
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
var nNE = 1;
function setNE(nValor){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        nNE = nValor;
        colorearNE();
        setTimeout("MostrarOcultar(0);", 20);
        if (nNE > 1) setTimeout("MostrarOcultar(1);", 20);

	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function colorearNE(){
    try{
        switch(nNE){
            case 1:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 4:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function fControlFechas(sOp){
    try{
        if ($I('txtDesde').value.length==10 && $I('txtHasta').value.length==10){
            if (DiffDiasFechas($I("txtDesde").value, $I("txtHasta").value) < 0){
                if (sOp == "D") $I("txtHasta").value = $I("txtDesde").value;
                else $I("txtDesde").value = $I("txtHasta").value;
            }
            getProyectos();
        }
        else
            borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al controlar el rango de fechas.", e.message);
    }
}

function getProyectos(){
    try{
     	if (($I('txtDesde').value=="") || ($I('txtHasta').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 340);
  	        return;
  	    }
        var js_args = "getProyectos@#@";
        js_args += $I("txtDesde").value + "@#@";  //fecha desde
        js_args += $I("txtHasta").value;  //fecha hasta

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los proyectos.", e.message);
    }
}