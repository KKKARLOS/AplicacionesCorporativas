var aFila = null;
var strAction = "";
var strTarget = "";

function init(){
    try{
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target;
    
        setExcelImg("imgExcel", "divCatalogo");
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
            case "proyecto":
		        $I("divCatalogo").innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        nNE=1;
                setNE(nNE);
                aFila = FilasDe("tblDatos");
		        recolorear();
                calcularAcumulados();
                break;
            case "buscarPE":
                if (aResul[2]==""){
                    $I('hdnT305IdProy').value="";
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##");
                        $I("hdnT305IdProy").value = aDatos[0];
                        if (aDatos[2] == "1")
                            $I("hdnEsSoloRtpt").value = "S";
                        else
                            $I("hdnEsSoloRtpt").value = "N";

                        setTimeout("recuperarDatosPSN();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "recuperarPSN":
                if (aResul[4]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            //sEstadoProy = aResul[2];
	            $I("hdnEstadoProy").value = aResul[2]; 
                switch (aResul[2])
                {
                    case "A": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }
    	        
	            $I("txtCodProy").value = aResul[4];
                $I("hdnT305IdProy").value = aResul[5];               
                $I("divPry").innerHTML = "<INPUT class=txtM id=txtNomProy name=txtNomProy Text='' style='WIDTH:360px' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Responsable:</label>" + aResul[7] + "<br><label style='width:70px'>"+$I("hdnNodo").value+":</label>"+aResul[10]+"<br><label style='width:70px'>Cliente:</label>" + aResul[9] + "] hideselects=[off]\" />";
	            $I("txtNomProy").value = aResul[3];
                bLimpiarDatos = true;
	            setTimeout("buscar();", 20);
                break;              
        }
        ocultarProcesando();
    }
}
function VerFecha(strM){
    try {
	    if ($I('txtValIni').value.length==10 && $I('txtValFin').value.length==10){
	        var fecha_desde = new Date();
	        fecha_desde.setFullYear($I('txtValIni').value.substr(6,4),$I('txtValIni').value.substr(3,2)-1,$I('txtValIni').value.substr(0,2));
	        var fecha_hasta = new Date();
	        fecha_hasta.setFullYear($I('txtValFin').value.substr(6,4),$I('txtValFin').value.substr(3,2)-1,$I('txtValFin').value.substr(0,2));
            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtValFin').value = $I('txtValIni').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtValIni').value = $I('txtValFin').value;

            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}
function buscar()
{
    try{
        borrarCatalogo();
        if ($I("chkActuAuto").checked == true && $I("txtValIni").value != '' && $I("txtValFin").value != '' && $I("txtNomProy").value != "") setTimeout("obtenerDatos();", 50);
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda del proyecto", e.message);
    }
}
function obtenerDatos(){
   try{	 

        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;
           
     	if (($I('txtValIni').value=="") || ($I('txtValFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
     	if ($I('hdnT305IdProy').value=="")
  	    {
  	        mmoff("War", "Debes indicar el proyecto.", 210);
  	        return;
  	    }
   
        var js_args = "proyecto@#@";
        js_args += $I("hdnT305IdProy").value +"@#@";  //
        js_args += $I("txtValIni").value + "@#@";  //fecha desde
        js_args += $I("txtValFin").value + "@#@";  //fecha hasta
        js_args += $I("hdnEsSoloRtpt").value;  //Es solo RTPT

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la consulta", e.message);
    }
}
function obtenerProyectos(){
    try{
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;
        
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///"); 
	                $I("hdnT305IdProy").value = aDatos[0];
	                if (aDatos[2]=="1")
	                    $I("hdnEsSoloRtpt").value = "S";
	                else
	                    $I("hdnEsSoloRtpt").value = "N";
	                recuperarDatosPSN();
	                //buscar();
	            }
	        });
	    window.focus();	    	            
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function recolorear(){
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos.rows.length == 0) return;
        
        var nIndice1 = 0;
        var nIndice2 = 0;
        var nIndice3 = 0;
       
        for (var i=0; i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].style.display == "none")  continue;

            if (tblDatos.rows[i].getAttribute("tipo") == "P" || tblDatos.rows[i].getAttribute("tipo") == "PT" || tblDatos.rows[i].getAttribute("tipo") == "F" || tblDatos.rows[i].getAttribute("tipo") == "A") {
            // proy.técnico, fase, actividad , profesional
                if (nIndice1 % 2 == 0){
                    tblDatos.rows[i].className = "FA";
                }
                else{
                    tblDatos.rows[i].className = "FB";
                }
                if (tblDatos.rows[i].getAttribute("tipo") == "P")
                {
                    if (tblDatos.rows[i].getAttribute("baja") == "1")
                        setOp(tblDatos.rows[i].cells[0].children[1], 20);
                }
                nIndice1++;
            }

            if (tblDatos.rows[i].getAttribute("tipo") == "T") {
            // tareas
                if (nIndice2 % 2 == 0){
                    tblDatos.rows[i].className = "FAM1";
                }
                else{
                    tblDatos.rows[i].className = "FAM2";
                }
                nIndice2++;
            }
            else if (tblDatos.rows[i].getAttribute("tipo") != "C") nIndice2 = 0;

            // consumos
            if (tblDatos.rows[i].getAttribute("tipo") == "C") {
                if (nIndice3 % 2 == 0){
                    tblDatos.rows[i].className = "FAM3";
                }
                else{
                    tblDatos.rows[i].className = "FAM4";
                }
                nIndice3++;
            }else nIndice3 = 0;            
        }
	}catch(e){
		mostrarErrorAplicacion("Error al recolorear las filas de la tabla", e.message);
    }
}
var oImgAux;
function mostrar2(oImg)
{
    oImgAux=oImg;
    mostrarProcesando();
    setTimeout("mostrar(),20");
}
function mostrar(){
    try {                     
        var oImg =oImgAux;
        var nIndexFila = oImg.parentNode.parentNode.rowIndex;
        var nNivel = oImg.parentNode.parentNode.getAttribute("nivel");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        
        var tblDatos = $I("tblDatos");
        for (var i=nIndexFila+1; i<tblDatos.rows.length; i++){

            if (tblDatos.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O")
                {
                    tblDatos.rows[i].style.display = "none";
                    if (tblDatos.rows[i].getAttribute("N") != null)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
                else{ 
                    if (tblDatos.rows[i].getAttribute("nivel")-1 == nNivel) 
                        tblDatos.rows[i].style.display = "table-row";
                }
            }
            else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../../images/plus.gif";
        else oImg.src = "../../../../images/minus.gif"; 
        recolorear(); 
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(obj){
    try {
        mostrarProcesando();
        if ($I("divCatalogo").innerHTML == ""){
            ocultarProcesando();
            return;
        }
        var tblDatos = $I("tblDatos");
        
        if (tblDatos.rows.length == 0){
            ocultarProcesando();
            return;
        }
        if (obj == 0){ //Contraer
            var j = 0;
            for (var i=0; i<tblDatos.rows.length;i++){

                if (tblDatos.rows[i].getAttribute("nivel") > 1)
                {
                    if (tblDatos.rows[i].getAttribute("N") != null)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    tblDatos.rows[i].style.display = "none";
                }
                else 
                {
                    tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    if (j % 2 == 0)
                        tblDatos.rows[i].className = "FA";
                    else
                        tblDatos.rows[i].className = "FB";
					j++;
                }                
            }
        }else{ //Expandir
            for (var i=0; i<tblDatos.rows.length;i++){
                //alert("Fila: " +i + " tipo: " + tblDatos.rows[i].tipo);
                if (bNivelPedido(nNE, tblDatos.rows[i].getAttribute("tipo"))) {
                    if (tblDatos.rows[i].getAttribute("N") != null)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    tblDatos.rows[i].style.display = "table-row";
                }
                else 
                {
                    if (tblDatos.rows[i].getAttribute("N") != null) {
                        if (bNivelInferior(nNE, tblDatos.rows[i].getAttribute("tipo"))) {
                            tblDatos.rows[i].cells[0].children[0].src = "../../../../images/minus.gif";
                            tblDatos.rows[i].style.display = "table-row";
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
                //if (sTipo=="PE" || sTipo=="PT") bRes=true;
                if (sTipo=="PT") bRes=true;
                break;
            case 2:
                //if (sTipo=="PE" || sTipo=="PT" || sTipo=="F" ||sTipo=="A" || sTipo=="T") bRes=true;
                if (sTipo=="T") bRes=true;
                break;
            case 3:
                if (sTipo=="P") bRes=true;
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
            case 1:
                if (sTipo=="PT") bRes=true;
                break;
            case 2:
                if (sTipo=="PT" || sTipo=="F" || sTipo=="A" || sTipo=="T") bRes=true;
                break;
            case 3:
                if (sTipo=="PT" || sTipo=="F" || sTipo=="A" || sTipo=="T" || sTipo=="P") bRes=true;
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
		$I("txtHorasReportadas").value = "0,00";
		$I("txtJornadasReportadas").value = "0,00";
		//$I("txtJornadasEconomicas").value = "0,00";
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
        var nPH= 0, nPJ= 0;
        var nTotH= 0;
        var nTotJ= 0;
        //var nTotE= 0;
        var nH=0, nJ=0;
        
        for (var i=aFila.length-1; i>=0; i--)
        {
            //alert(aFila[i].innerHTML);
            if (aFila[i].getAttribute("tipo") == "C"){//Consumo
                nH = parseFloat(dfn(aFila[i].getAttribute("nH")));
                nJ = parseFloat(dfn(aFila[i].getAttribute("nJ")));
//                nTH += parseFloat(dfn(aFila[i].cells[1].innerText));
//                nTJ += parseFloat(dfn(aFila[i].cells[2].innerText));
//                nAH += parseFloat(dfn(aFila[i].cells[1].innerText));
//                nAJ += parseFloat(dfn(aFila[i].cells[2].innerText));
//                nFH += parseFloat(dfn(aFila[i].cells[1].innerText));
//                nFJ += parseFloat(dfn(aFila[i].cells[2].innerText));
//                nPTH += parseFloat(dfn(aFila[i].cells[1].innerText));
//                nPTJ += parseFloat(dfn(aFila[i].cells[2].innerText));
//                nPH += parseFloat(dfn(aFila[i].cells[1].innerText));
//                nPJ += parseFloat(dfn(aFila[i].cells[2].innerText));
//                nTotH += parseFloat(dfn(aFila[i].cells[1].innerText));
//                nTotJ += parseFloat(dfn(aFila[i].cells[2].innerText));
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
                nPH += nH;
                nPJ += nJ;
                nTotH += nH;
                nTotJ += nJ;
            } else if (aFila[i].getAttribute("tipo") == "P") {//Profesional
                aFila[i].cells[1].innerText = nPH.ToString("N");
                aFila[i].cells[2].innerText = nPJ.ToString("N");
                nPH = 0; nPJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "T") {//Tarea
                aFila[i].cells[1].innerText = nTH.ToString("N");
                aFila[i].cells[2].innerText = nTJ.ToString("N");
                nPH = 0; nPJ = 0;
                nTH = 0; nTJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "A") {//ACTIVIDAD
                aFila[i].cells[1].innerText = nAH.ToString("N");
                aFila[i].cells[2].innerText = nAJ.ToString("N");
                nPH = 0; nPJ = 0;
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "F") {//FASE
                aFila[i].cells[1].innerText = nFH.ToString("N");
                aFila[i].cells[2].innerText = nFJ.ToString("N");
                nPH = 0; nPJ = 0;
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "PT") {//PT
                aFila[i].cells[1].innerText = nPTH.ToString("N");
                aFila[i].cells[2].innerText = nPTJ.ToString("N");
                nPH = 0; nPJ = 0;
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
                nPTH = 0; nPTJ = 0;
            }            
        }     
		$I("txtHorasReportadas").innerText = nTotH.ToString("N");
		$I("txtJornadasReportadas").innerText = nTotJ.ToString("N");
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los acumulados", e.message);
    }
}
function excel(stropcion){
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td>Tipo</td>");
        sb.Append("        <td>Estructura técnica / Profesional / F. consumo / Comentarios</td>");
        sb.Append("        <td>Horas</td>");
        sb.Append("        <td>Jornadas</td>");
		sb.Append("	</tr>");
		sb.Append("</table>");
        
        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
	    for (var i=0;i < aFila.length; i++){
		    if (aFila[i].style.display == "none"&&stropcion==null) continue;
		    
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
            
                sb.Append(aFila[i].cells[x].innerText);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }
	    sb.Append("</table>");

        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
	    var aFilaRes = FilasDe("tblResultado");
	    for (var i=0;i < aFilaRes.length; i++){
	        sb.Append("<tr style='background-color: #BCD4DF;'>");
	        sb.Append("<td>&nbsp;</td>");
	        sb.Append("<td>Total proyecto económico</td>");
            for (var x=1; x<=2;x++){
                if (x>0)
                    sb.Append("<td style='text-align:right'>");
                else
                    sb.Append("<td>");

                sb.Append(aFilaRes[i].cells[x].innerText);
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
        MostrarOcultar(0);
        if (nNE > 1) MostrarOcultar(1);

	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function colorearNE(){
    try{
        switch(nNE){
            case 1:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../../images/imgNE4off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../../images/imgNE4off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../../images/imgNE4off.gif";
                break;
            case 4:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../../images/imgNE4on.gif";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function buscarPE(){
    try{
        $I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtCodProy").value);
        setNumPE();
        //alert(js_args);     
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
            $I("imgEstProy").title = "";
            $I("divPry").innerHTML = "<input type='text' class='txtM' id='txtNomProy' value='' style='width:360px;' readonly='true' />";
            //$I("divCatalogo").children[0].innerHTML = "";
            borrarCatalogo();
	            
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}
function getPEByNum(){
    try{ 
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&nPE=" + dfn($I("txtCodProy").value);

        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    $I("hdnEsSoloRtpt").value = aDatos[2];
                    
                    recuperarDatosPSN();
                }
                else {
                    ocultarProcesando();
                }
            });
        window.focus();	    	            
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}
function recuperarDatosPSN(){
    try{
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        js_args += $I("hdnT305IdProy").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
function Exportar(){
    try{  
        if ($I("txtCodProy").value=="") return;
		
		var sScroll = "no";
		if (screen.width == 800) sScroll = "yes";

        //*SSRS

		dtFechaDesde = ($I("txtValIni").value).substring(6, 10) + "-" + ($I("txtValIni").value).substring(3, 5) + "-" + ($I("txtValIni").value).substring(0, 2);
		dtFechaHasta = ($I("txtValFin").value).substring(6, 10) + "-" + ($I("txtValFin").value).substring(3, 5) + "-" + ($I("txtValFin").value).substring(0, 2);

		var params = {
		    reportName: "/SUPER/sup_consumosproyecto",
		    tipo: "PDF",
		    t314_idusuario: t314_idusuario,
		    nPSN: $I("hdnT305IdProy").value,
		    dtFechaDesde: dtFechaDesde,
		    dtFechaHasta: dtFechaHasta,
		    FechaDesde: $I("txtValIni").value,
		    FechaHasta: $I("txtValFin").value,
		    num_proyecto: $I("txtCodProy").value,
		    nom_proyecto: $I("txtNomProy").value,
		    estado: $I("hdnEstadoProy").value,
		    bEsSoloRtpt: $I("hdnEsSoloRtpt").value == "S"
		}

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
       

