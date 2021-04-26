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
            case "tecnicos":
		        $I("divCatalogo").innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        nNE=1;
                setNE(nNE);
                aFila = FilasDe("tblDatos");
		        recolorear();
                calcularAcumulados();
                
                $I("lblTotalHorasImputadas").innerText = aResul[3].ToString("N");
                $I("lblTotalJornadasImputadas").title = aResul[4].ToString();
                $I("lblTotalJornadasImputadas").innerText = aResul[4].ToString("N");
                
                $I("lblTotalHorasImputadasProf").innerText = aResul[5].ToString("N");
                $I("lblTotalJornadasImputadasProf").title = aResul[6].ToString();
                $I("lblTotalJornadasImputadasProf").innerText = aResul[6].ToString("N");
                
                $I("lblTotalHorasCalendario").innerText = aResul[7].ToString("N");
                $I("lblTotalJornadasCalendario").title = aResul[8].ToString();
                $I("lblTotalJornadasCalendario").innerText = aResul[8].ToString("N");
                
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
        else
            borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}
function buscar()
{
    try{
        borrarCatalogo();
        if ($I("chkActuAuto").checked == true && $I("txtValIni").value != '' && $I("txtValFin").value != '' && $I("txtNombreTecnico").value != "")
            setTimeout("obtenerDatos();",50);
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda del proyecto", e.message);
    }
}
function obtenerDatos(){
   try{	 
     	if (($I('txtValIni').value=="") || ($I('txtValFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
     	if ($I('txtCodTecnico').value=="")
  	    {
  	        mmoff("Inf", "Debes indicar el profesional.",210);
  	        return;
  	    }
	    //formatearNumero($I("txtCodTecnico"), 0, true)

        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;
   
        var js_args = "tecnicos@#@";
        js_args += $I("txtCodTecnico").value +"@#@";  //txtCodTecnico
        js_args += $I("txtValIni").value + "@#@";  //fecha desde
        js_args += $I("txtValFin").value + "@#@";  //fecha hasta

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la consulta", e.message);
    }
}
function obtenerTecnicos(){
    try{
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;
    
	    mostrarProcesando();

	    var strEnlace = strServer + "Capa_Presentacion/PSP/getProfesionalVision.aspx";
	    var sTamano = sSize(680, 620);
	    modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
	            if (ret != null) {
	                var aOpciones = ret.split("@#@");

	                $I("txtCodTecnico").value = aOpciones[0];
	                $I("txtTipoRecurso").value = aOpciones[1];
	                $I("txtNombreTecnico").value = aOpciones[2];
	                buscar();
	            }
	        });
	    window.focus();
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
    }
}
//function setActuAuto(){
//    try{
//        if ($I("chkActuAuto").checked){
//            setOp($I("btnObtener"), 30);
//            obtenerDatos();
//        }else{
//            setOp($I("btnObtener"), 100);
//        }

//	}catch(e){
//		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
//	}
//}

function recolorear(){
    try{
        var tblDatos = $I("tblDatos");
        if (tblDatos.rows.length == 0) return;
        
        var nIndice1 = 0;
        var nIndice2 = 0;
        var nIndice3 = 0;
        
        for (var i=0; i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].style.display == "none")  continue;

            if (tblDatos.rows[i].getAttribute("tipo") == "PE" || tblDatos.rows[i].getAttribute("tipo") == "PT" || tblDatos.rows[i].getAttribute("tipo") == "F" || tblDatos.rows[i].getAttribute("tipo") == "A") {
            // PE, proy.técnico, fase, actividad 
                if (nIndice1 % 2 == 0){
                    tblDatos.rows[i].className = "FA";
                }
                else{
                    tblDatos.rows[i].className = "FB";
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
    try{
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
                    if (tblDatos.rows[i].getAttribute("nivel") - 1 == nNivel) 
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
		$I("lblHorasReportadas").value = "0,00";
		$I("lblJornadasReportadas").value = "0,00";
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
        
        for (var i=aFila.length-1; i>=0; i--)
        {
            if (aFila[i].getAttribute("tipo") == "C") {//Consumo
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
//                nPEH += parseFloat(dfn(aFila[i].cells[1].innerText));
//                nPEJ += parseFloat(dfn(aFila[i].cells[2].innerText));
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
                nPEH += nH;
                nPEJ += nJ;
                nTotH += nH;
                nTotJ += nJ;
            } else if (aFila[i].getAttribute("tipo") == "T") {//Tarea
                aFila[i].cells[1].innerText = nTH.ToString("N");
                aFila[i].cells[2].innerText = nTJ.ToString("N");
                nTH = 0; nTJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "A") {//ACTIVIDAD
                aFila[i].cells[1].innerText = nAH.ToString("N");
                aFila[i].cells[2].innerText = nAJ.ToString("N");
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "F") {//FASE
                aFila[i].cells[1].innerText = nFH.ToString("N");
                aFila[i].cells[2].innerText = nFJ.ToString("N");
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "PT") {//PT
                aFila[i].cells[1].innerText = nPTH.ToString("N");
                aFila[i].cells[2].innerText = nPTJ.ToString("N");
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
                nPTH = 0; nPTJ = 0;
            } else if (aFila[i].getAttribute("tipo") == "PE") {//PE
                aFila[i].cells[1].innerText = nPEH.ToString("N");
                aFila[i].cells[2].innerText = nPEJ.ToString("N");
                nTH = 0; nTJ = 0;
                nAH = 0; nAJ = 0;
                nFH = 0; nFJ = 0;
                nPTH = 0; nPTJ = 0;
                nPEH = 0; nPEJ = 0;
                //nTotE += parseFloat(desformatNumero(aFila[i].cells[3].innerText));
            }            
        }     

		$I("lblHorasReportadas").innerText = nTotH.ToString("N");
		$I("lblJornadasReportadas").innerText = nTotJ.ToString("N");
		//$I("txtJornadasEconomicas").innerText = formatearFloat(nTotE, 2, false);

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
		sb.Append("	<tr align=center>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Tipo</TD>");
        sb.Append("        <td style='background-color: #BCD4DF; width: 400px;'>Estructura técnica / F. consumo / Comentarios</TD>");
        sb.Append("        <td style='background-color: #BCD4DF; width: 70px;'>Horas</TD>");
        sb.Append("        <td style='background-color: #BCD4DF; width: 70px;'>Jornadas</TD>");
		sb.Append("	</tr>");
//		sb.Append("</TABLE>");
//        
//        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
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
                    switch (aFila[i].getAttribute("nivel")) {
                        case "1": sb.Append(""); break;
                        case "2": sb.Append("&nbsp;&nbsp;"); break;
                        case "3": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                        case "4": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                        case "5": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                        case "6": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                    }
                }
            
                sb.Append(aFila[i].cells[x].innerText);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }
//	    sb.Append("</table>");

//        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
        sb.Append("<tr>");
        sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
        sb.Append("<td style='background-color: #BCD4DF;'>Total imputado a proyectos según ámbito de visión</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblHorasReportadas").innerText +"</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblJornadasReportadas").innerText +"</td>");
        sb.Append("</tr>");
	    
        sb.Append("<tr>");
        sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
        sb.Append("<td style='background-color: #BCD4DF;'>Total imputado usuario</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblTotalHorasImputadas").innerText +"</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblTotalJornadasImputadas").innerText +"</td>");
        sb.Append("</tr>");
	    
        sb.Append("<tr>");
        sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
        sb.Append("<td style='background-color: #BCD4DF;'>Total imputado profesional</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblTotalHorasImputadasProf").innerText +"</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblTotalJornadasImputadasProf").innerText +"</td>");
        sb.Append("</tr>");
	    
        sb.Append("<tr>");
        sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
        sb.Append("<td style='background-color: #BCD4DF;'>Calendario actual</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblTotalHorasCalendario").innerText +"</td>");
        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>"+ $I("lblTotalJornadasCalendario").innerText +"</td>");
        sb.Append("</tr>");
	    
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
function Exportar(){
    try{  
     	if ($I('txtCodTecnico').value=="")
  	    {
  	        mmoff("Inf", "Debes indicar el profesional.", 210);
  	        return;
  	    }
  	    		
		var sScroll = "no";
		if (screen.width == 800) sScroll = "yes";

        //*SSRS

		fDesde = ($I("txtValIni").value).substring(6, 10) + "-" + ($I("txtValIni").value).substring(3, 5) + "-" + ($I("txtValIni").value).substring(0, 2);
		fHasta = ($I("txtValFin").value).substring(6, 10) + "-" + ($I("txtValFin").value).substring(3, 5) + "-" + ($I("txtValFin").value).substring(0, 2);

		var params = {
		    reportName: "/SUPER/sup_consumosprofesional",
		    tipo: "PDF",
		    t314_idusuario: t314_idusuario,
		    Tecnico: $I("txtCodTecnico").value,
		    fDesde: fDesde,
		    fHasta: fHasta,
		    FechaDesde: $I("txtValIni").value,
		    FechaHasta: $I("txtValFin").value,
		    profesional: $I("txtNombreTecnico").value
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