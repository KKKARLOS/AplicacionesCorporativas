
function init(){
	try{
        if (nReconectar=="1"){
            $I("tdInteresado").style.visibility = "visible";
            //$I("tdInteresado").setAttribute("style", "text-align:left;visibility:visible;background-image:url('../../../Images/imgFondoCal4G.gif'); background-repeat:no-repeat; width: 650px; height: 28px;");

/*            var sStyle = $I("tdInteresado").getAttribute("style");
            sStyle.setAttribute("visibility","hidden");
            $I("tdInteresado").setAttribute("style",sStyle);
            
            $I("tdInteresado").setAttribute("style",$I("tdInteresado").getAttribute("style").setAttribute("visibility","visible;"));
*/            if (sPerfil == "P" && reconectar_msg == 1) MostrarMensajeReconexion();
        }else{
            $I("tdInteresado").style.visibility = "hidden";            
            //$I("tdInteresado").setAttribute("style", "text-align:left;visibility:hidden;background-image:url('../../../Images/imgFondoCal4G.gif'); background-repeat:no-repeat; width: 650px; height: 28px;");
            //$I("tdInteresado").setAttribute("style",$I("tdInteresado").getAttribute("style").replace("visibility:visible;","visibility:hidden;"));
        }

	    for (var i=0; i<aSemLab.length; i++){
	        if (aSemLab[i] == "0"){
	            $I("tblLiterales").rows[0].cells[i].style.color = "red";
	            //$I("tblLiterales").rows[0].cells[i].setAttribute("style",$I("tblLiterales").getAttribute("style").setAttribute("color","red;"));
	            $I("tblLiterales").rows[0].cells[i].setAttribute("style", "color:red");
	            $I("tblLiterales").rows[0].cells[i].title = "Jornada no laborable";
	        }
	    }

        var oUMC = new Date(strUMC.substring(0,4),eval(strUMC.substring(4,6)-1),1).add("mo",1).add("d",-1);  
        if (oUltImputac < oUMC) oUltImputac = oUMC;
    
        setCurrentMonth();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
	}
}

function unload(){

}

function seleccionarSemana(intSemana,objImagen){
    try{
	    var intUltBarra = $I(objImagen.id).src.lastIndexOf("/");
	    var strImagen = $I(objImagen.id).src.substring((intUltBarra+1), $I(objImagen.id).src.length);
  	    if (strImagen!='icoAbiertoG.gif') return;

	    var intPrimerDiaSemana = aSemanas[intSemana][0];
	    var intUltimoDiaSemana = aSemanas[intSemana][aSemanas[intSemana].length-1];
	    var strRango = "Semana del "+ intPrimerDiaSemana +" al "+ intUltimoDiaSemana +" de "+ MonthNames[eval(nCurrentMonth-1)] +" - "+ nCurrentYear;
	    strDate = new Date(nCurrentYear, eval(nCurrentMonth-1),intPrimerDiaSemana);
	    intPrimerDia = strDate.getDay();
	    if (intPrimerDia == 0) intPrimerDia = 7;
	    intDiasEnSemana = eval(intUltimoDiaSemana - intPrimerDiaSemana +1 );
	    var strSemanaSeleccionada = new Date(nCurrentYear,eval(nCurrentMonth-1),intPrimerDiaSemana);
	    if (strSemanaSeleccionada < strUltimoMesCerrado) return;
    	
	    mostrarProcesando();
//	    var strParametros = "intPrimerDia="+ intPrimerDiaSemana;
//	    strParametros += "&intUltimoDia="+ intUltimoDiaSemana;
//	    strParametros += "&intPrimerDiaSemana="+ eval(intPrimerDia-1);   // De 0 a 6.
//	    strParametros += "&intDiasEnSemana="+ intDiasEnSemana;
//	    strParametros += "&intMes="+ eval(nCurrentMonth-1);
//	    strParametros += "&intAnno="+ nCurrentYear;
//	    strParametros += "&strRango="+ Utilidades.escape(strRango);

	    var strParametros = "ipd="+ codpar(intPrimerDiaSemana);
	    strParametros += "&iud="+ codpar(intUltimoDiaSemana);
	    strParametros += "&ipds="+ codpar(eval(intPrimerDia-1));   // De 0 a 6.
	    strParametros += "&ides="+ codpar(intDiasEnSemana);
	    strParametros += "&im="+ codpar(eval(nCurrentMonth-1));
	    strParametros += "&ia="+ codpar(nCurrentYear);
	    strParametros += "&sr="+ codpar(strRango);
	    
    	//alert(strParametros);
	    location.href = "../ImpDiaria/Default.aspx?"+ strParametros;
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la semana", e.message);
	}
}

function seleccionarSemanaPlanif(intSemana, objImagen){
    try{
        if (objImagen.src.indexOf("imgSeparador") != -1) return;
        //alert("Planificación semana "+ intSemana);
	    var intUltimoDiaSemana = aSemanas[intSemana][aSemanas[intSemana].length-1];
	    
	    var dFechaAux = new Date(nCurrentYear,eval(nCurrentMonth-1),intUltimoDiaSemana);
	    var sFechaAux = fechaAcadena(dFechaAux);
	    var strParametros = "sFechaAux="+ sFechaAux;
    	//alert(strParametros);
	    location.href = "../Agenda/Consulta/Default.aspx?"+ strParametros;
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la semana", e.message);
	}
}

function MesCerrado(){
    try{
	    if (strUMC != ""){
	        strUltimoMesCerrado = new Date(strUMC.substring(0,4),eval(strUMC.substring(4,6)-1),1).add("mo", 1).add("d",-1);
	    }else{
	        strUltimoMesCerrado = new Date(1999,11,31);
	    }
	    var strFechaActual = new Date(nCurrentYear,nCurrentMonth-1, 1);
	    return (strFechaActual < strUltimoMesCerrado) ? true:false;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar el mes cerrado", e.message);
	}
}
	
function formateardiasAux(){
    try{
	    var strUltimoDiaBlanco = "";
	    aDias = $I("tblCalendario").getElementsByTagName("div");

	    intMesCalendario = eval(nCurrentMonth-1);
	    intAnnoCalendario = nCurrentYear.toString();

	    obtenerHorasImputadas(nCurrentMonth, nCurrentYear);
	}catch(e){
		mostrarErrorAplicacion("Error al formatear los días aux", e.message);
	}    	
}

function formateardias(){
    try{
        var aDia;
        var strUltimoDiaBlanco = "";
        var sStyle = "";

        aFestivos.length = 0;
        aDiaFes.length = 0;

        var bMesCerrado = MesCerrado(); 
        if (nCal == 0) bMesCerrado=true;
        /*******************************************************/
        //Unicamente queremos los días, por lo que no tenemos en cuenta los
        //15 primeros divs que son: main2,main,divCal,idSemana1,idSemana2
        //idSemana3,idSemana4,idSemana5,idSemana6,idPlanif1,idPlanif2,
        //idPlanif3,idPlanif4,idPlanif5,idPlanif6
        /*******************************************************/
        if (bMesCerrado){
            for (var i=15;i<aDias.length; i++){  
                aDias[i].style.backgroundImage = "";
	            //if (ie) 
	                    aDias[i].children[0].className = "diaCerrado";
	            //else    aDias[i].children[0].setAttribute("class","diaCerrado");
	            aDias[i].children[1].innerHTML = "<BR>";
            }
        }else{
            if (aDatos.length > 1){
                //strFecha = new Date();
                intHoy 		= oDiaActual.getDate();
                intMesHoy 	= oDiaActual.getMonth();
                intAnnoHoy 	= oDiaActual.getFullYear();
                var date1;
            
                for (var i=15;i<aDias.length; i++)     
                {
                    if (i-15 < aDatos.length){
                        aDias[i].style.backgroundImage = "";
                    //    sStyle= aDias[i].getAttribute("style");
                    //    aDias[i].setAttribute("style",sStyle+";backgroundImage:''");
                        
                        //var iDia = (ie)? aDias[i].children[0].innerText : aDias[i].children[0].textContent;
                        var iDia = aDias[i].children[0].innerText;
	                    if (parseInt(iDia, 10) == intHoy){
		                    if ((intMesHoy == intMesCalendario) && (intAnnoHoy == intAnnoCalendario)){
			                    aDias[i].style.backgroundImage = "url('../../../images/imgFondoDiaG.gif')";
			                    aDias[i].style.backgroundRepeat = "no-repeat";
			            //        var sStyle= aDias[i].getAttribute("style");
			            //        aDias[i].setAttribute("style",sStyle+";backgroundImage:url('../../../images/imgFondoDiaG.gif');backgroundRepeat:'no-repeat';");
		                    }
	                    }
                    
                            if (aDatos[i-15] != ""){
                            aDia = aDatos[i-15].split("//");
                            if (aDia[6] == "1") aDiaFes[aDiaFes.length] = aDia[0];
                            
                            if (aDia[1] == "1") {
                            	//if (ie) 
                            	aDias[i].children[0].className = "diaFestivo";
	                            //else aDias[i].children[0].setAttribute("class","diaFestivo");

                                aFestivos[aFestivos.length] = aDia[4];
                            } else if (aDia[2] != "0" && aDia[7] != "1") {
                            //} else if (aDia[2] != "0") {
                            	//if (ie) 
                            	aDias[i].children[0].className = "diaImputado";
	                            //else aDias[i].children[0].setAttribute("class","diaImputado");
                            
                            	strUltimoDiaBlanco = parseInt(aDias[i].children[0].innerText, 10) + 1;
                            }else if (aDia[7] == "1") {
                                aDias[i].children[0].className = "diaVacacion";
                                if (aDia[2] != "0")
                                    strUltimoDiaBlanco = parseInt(aDias[i].children[0].innerText, 10) + 1;
                            }else{
                                //if (ie)
	                                date1 = new Date(nCurrentYear,eval(nCurrentMonth-1),aDias[i].children[0].innerText);
	                            /*else
	                                date1 = new Date(nCurrentYear,eval(nCurrentMonth-1),aDias[i].children[0].textContent);
	                            */    
                                if (date1 <= oDiaActual) 
                                {                                
                            	    //if (ie) 
                            	    aDias[i].children[0].className = "diaNoImputado"; //verde  
	                                //else aDias[i].children[0].setAttribute("class","diaNoImputado");	                                
	                            }
	                            else
	                            {
                            	    //if (ie) 
                            	    aDias[i].children[0].className = "diaFuturo"; //verde  
	                                //else aDias[i].children[0].setAttribute("class","diaFuturo");	                                
	                            }
                            }
                            
                            if (aDia[5] == "0" && aDia[2] == "0" && aDia[3]== "0"){
                                aDias[i].children[1].innerHTML = "<BR>";
                            }else if (aDia[5] == "0"){
                                aDias[i].children[1].innerHTML = "<BR><font style='color:#83AFC3'>"+ aDia[2] +"</font> / <font style='color:black'>"+ aDia[3]+"</font>";
                            }else{
                                aDias[i].children[1].innerHTML = "<BR><font style='color:orange'>"+ aDia[5] +"</font> / <font style='color:#83AFC3'>"+ aDia[2] +"</font> / <font style='color:black'>"+ aDia[3]+"</font>";
                            }
                        }
                    }else{
             	        //if (ie) 
             	        aDias[i].children[0].className = "diaSinDesglose"; //verde  
                        //else aDias[i].children[0].setAttribute("class","diaSinDesglose");	                                
                       
                        aDias[i].children[1].innerHTML = "<BR>";
                    }
                }
            }else{
                for (var i=15;i<aDias.length; i++){
         	        //if (ie) 
         	        aDias[i].children[0].className = "diaSinDesglose"; //verde  
                    //else aDias[i].children[0].setAttribute("class","diaSinDesglose");	                                
                    
                    aDias[i].children[1].innerHTML = "<BR>";
                    aDias[i].style.backgroundImage = "";
                    //aDias[i].setAttribute("style",sStyle+";backgroundImage:''");
                }
            }
            
            var objDia, intDiferencia, bImpMesCerrado, intFestivos;
            var oDiaRef, oDiaPlanif, intAux, intUltimoDia, intDifDias;
            var bFestivo, bHuecoMesAnt;
            var intNuevaFecha, objNuevaFecha, intDiaSemana, intMesAux;
            var bImpMesCerrado = false;  // Para controlar el no poder imputar dos meses después del último mes cerrado 
            
            for (var i=0;i<aSemanas.length;i++){
    		    
    		    oDiaPlanif = new Date(intAnnoCalendario,intMesCalendario,aSemanas[i][aSemanas[i].length-1]);
    		    if (oDiaPlanif > oDiaActual) $I("idAccesoPlanif"+ (i+1)).src = "../../../images/imgPlanifON.gif";
    		    else $I("idAccesoPlanif"+ (i+1)).src = "../../../images/imgPlanifOFF.gif";
    		    
            }            
            
//            for (var i=0;i<aSemanas.length;i++){
//	            objDia = new Date(intAnnoCalendario,intMesCalendario,aSemanas[i][0]);
//	            intDiferencia = objDia.getTime() - strUltimoMesCerrado.getTime();
////	            if (intDiferencia > 5270400000){ //61 días en milisegundos (1 día 86400000).
//	            if (intDiferencia > 5356800000){ //62 días en milisegundos (1 día 86400000).
//		            bImpMesCerrado = true;
//	            }else bImpMesCerrado = false;

//	            for (var j=0;j<aSemanas[i].length;j++){
//		            intFestivos = 0;
//		            if ((aSemanas[i][j] <= strUltimoDiaBlanco) || (strAuxUltimoDia == "") || (!controlhuecos)){
//			            if (!bImpMesCerrado) $I("idAcceso"+ (i+1)).src = "../../../images/icoAbiertoG.gif";
//			            break;
//		            }
//		            else{
//			            oDiaRef = new Date(intAnnoCalendario,intMesCalendario,aSemanas[i][j]);
//			            intAux = oDiaRef.getTime() - oUltImputac.getTime();
//			            intUltimoDia = oUltImputac.getTime();
//			            intDifDias = Math.round(intAux / (1000 * 60 * 60 * 24));
//    					
//			            if (intDifDias > 30){
//				            if (isNaN(aSemanas[4][0])) $I("idAcceso5").src = "../../../images/imgSeparador.gif";
//				            if (isNaN(aSemanas[5][0])) $I("idAcceso6").src = "../../../images/imgSeparador.gif";
//				            if (isNaN(aSemanas[4][0])) $I("idAccesoPlanif5").src = "../../../images/imgSeparador.gif";
//				            if (isNaN(aSemanas[5][0])) $I("idAccesoPlanif6").src = "../../../images/imgSeparador.gif";
//				            ocultarProcesando();
//				            return;
//				            //Si la diferencia es superior a un mes, termina.
//			            }
//			            bFestivo = false;
//			            bHuecoMesAnt = false;
//			            /* Ahora se suman los sábados, domingos y festivos,
//			               para restárselos a la diferencia de días 
//			               Mikel  11/10/2011 En vez de sumar dias a través de milisegundos, uso las funciones de fecha
//			            */
//			            objNuevaFecha=oUltImputac;
//			            for (var y = 1; y <= intDifDias; y++){
//				            //intNuevaFecha = eval(intUltimoDia + (y * 86400000));
//				            //objNuevaFecha = new Date();
//				            //objNuevaFecha.setTime(intNuevaFecha);
//				            objNuevaFecha= objNuevaFecha.add("d",1);
//				            
//				            intDiaSemana = objNuevaFecha.getDay()
//				            intMesAux = objNuevaFecha.getMonth();
//    						
//                            if (intDiaSemana == 0) intDiaSemana = 6;
//                            else intDiaSemana--;
//    						
//				            if (aSemLab[intDiaSemana] == 0){
//					            intFestivos++;
//					            bFestivo = true;
//				            }else{
//					            for (var z=0;z<aFestivos.length;z++){
//						            aFestivoAux = aFestivos[z].split("/");
//						            strFestivoAux = new Date(aFestivoAux[2],eval(aFestivoAux[1]-1),aFestivoAux[0]);
//						            if (objNuevaFecha.getTime() == strFestivoAux.getTime()){
//							            intFestivos++;
//							            bFestivo = true;
//							            break;
//						            }else bFestivo = false;
//					            }
//				            }
//				            if ((aSemLab[intDiaSemana] == 1) && (!bFestivo)){
//					            if (parseInt(intMesAux, 10) < parseInt(intMesCalendario, 10)){
//						            bHuecoMesAnt = true;
//					            }
//				            }
//			            }
//                        intDifDias -= intFestivos;
//			            if (!bHuecoMesAnt){
//				            if ((intDifDias <= 1) && (!bFestivo)){
//					            if (!bImpMesCerrado) $I("idAcceso"+ (i+1)).src = "../../../images/icoAbiertoG.gif";
//					            break;
//				            }
//			            }
//		            }
//	            }
//            }
            //--------------------------------------------------
            var bContinuar=true, bHayImputacionEnMes=false, bUltImpEnMesAnt=false;
            var intUltimoDiaMes = aSemanas[aSemanas.length - 1][aSemanas[aSemanas.length - 1].length - 1];
            if (intUltimoDiaMes==null){
                intUltimoDiaMes = aSemanas[aSemanas.length - 2][aSemanas[aSemanas.length - 2].length - 1];
                if (intUltimoDiaMes==null)
                    intUltimoDiaMes = aSemanas[aSemanas.length - 3][aSemanas[aSemanas.length - 3].length - 1];
            }
            bHuecoMesAnt = false;
            //Miro si la última imputación es en el mes actual o en el mes anterior
            intMesAux = oUltImputac.getMonth();
            if ((parseInt(intMesAux, 10) == parseInt(intMesCalendario, 10)) && oUltImputac.getFullYear() == intAnnoCalendario)
                bHayImputacionEnMes=true;
            else{
                //Miro si el mes de la última imputación es justo el anterior al mes actual
                if (((parseInt(intMesAux, 10) + 1) == parseInt(intMesCalendario, 10) && oUltImputac.getFullYear() == intAnnoCalendario) ||
                    (parseInt(intMesAux, 10)==11 && parseInt(intMesCalendario, 10)==0 && oUltImputac.getFullYear() == intAnnoCalendario -1))
                    bUltImpEnMesAnt=true;
                else
                    bHuecoMesAnt=true;
            }
            if (!bHayImputacionEnMes && bUltImpEnMesAnt)
            {//En este caso todos los días entre la última imputación + 1 y el fín de mes han de ser festivos
                if (intMesAux==12) intMesAux=1;
                else intMesAux+=1;
                objFechaMesAnt = new Date(intAnnoCalendario, intMesCalendario , 1);
                objFechaMesAnt= objFechaMesAnt.add("d",-1);
                var intUltimoDiaMesAnt = objFechaMesAnt.getDate();
                intUltimoDia = oUltImputac.getDate();
                //Si el último día imputado es el último del mes anterior -> abro la primera semana del mes actual
                if (intUltimoDia < intUltimoDiaMesAnt){
                    objNuevaFecha = new Date(objFechaMesAnt.getFullYear(), objFechaMesAnt.getMonth(), intUltimoDia);
                    for (x=intUltimoDia; x<intUltimoDiaMesAnt; x++){
                        objNuevaFecha= objNuevaFecha.add("d",1);

                        for (var z=0;z<aFestivosG.length;z++){
	                        aFestivoAux = aFestivosG[z].split("/");
	                        strFestivoAux = new Date(aFestivoAux[2],eval(aFestivoAux[1]-1),aFestivoAux[0]);
	                        if (objNuevaFecha.getTime() == strFestivoAux.getTime()){
		                        bFestivo = true;
		                        break;
	                        }else bFestivo = false;
                        }
                        if (!bFestivo)
                            break;
                    }
                    if (!bFestivo)
                        bHuecoMesAnt=true;
                }
            }
            for (var i=0;i<aSemanas.length;i++){
	            objDia = new Date(intAnnoCalendario,intMesCalendario,aSemanas[i][0]);
	            intDiferencia = objDia.getTime() - strUltimoMesCerrado.getTime();
	            if (intDiferencia > 5356800000){ //62 días en milisegundos (1 día 86400000).
		            bImpMesCerrado = true;
	            }else bImpMesCerrado = false;

	            for (var j=0;j<aSemanas[i].length;j++){
		            intFestivos = 0;
		            if ((aSemanas[i][j] <= strUltimoDiaBlanco) || (strAuxUltimoDia == "") || (!controlhuecos)){
			            if (!bImpMesCerrado) 
			                $I("idAcceso"+ (i+1)).src = "../../../images/icoAbiertoG.gif";
			            break;
		            }
		            else{
			            oDiaRef = new Date(intAnnoCalendario,intMesCalendario,aSemanas[i][j]);
			            intAux = oDiaRef.getTime() - oUltImputac.getTime();
			            intUltimoDia = oUltImputac.getTime();
			            intDifDias = Math.round(intAux / (1000 * 60 * 60 * 24));
    					
			            if (intDifDias > 30){
				            if (isNaN(aSemanas[4][0])) $I("idAcceso5").src = "../../../images/imgSeparador.gif";
				            if (isNaN(aSemanas[5][0])) $I("idAcceso6").src = "../../../images/imgSeparador.gif";
				            if (isNaN(aSemanas[4][0])) $I("idAccesoPlanif5").src = "../../../images/imgSeparador.gif";
				            if (isNaN(aSemanas[5][0])) $I("idAccesoPlanif6").src = "../../../images/imgSeparador.gif";
				            ocultarProcesando();
				            return;
				            //Si la diferencia es superior a un mes, termina.
			            }
			            bFestivo = false;
			            
			            if (bContinuar){
		                    //recorro todos los festivos contiguos hasta encontrar un laborable o fin de mes
		                    //intUltimoDia = oUltImputac.getDate();
		                    if (bHayImputacionEnMes){
		                        intUltimoDia = oUltImputac.getDate();
		                        objNuevaFecha = new Date(intAnnoCalendario,intMesCalendario, intUltimoDia);     
		                        objNuevaFecha= objNuevaFecha.add("d",1);
		                    }
		                    else{
		                        intUltimoDia = aSemanas[i][j];
		                        objNuevaFecha = new Date(intAnnoCalendario,intMesCalendario, 1);
			                }
		                    for (x=intUltimoDia; x<intUltimoDiaMes; x++){
			                    bFestivo = false;
			                    for (var z=0;z<aFestivosG.length;z++){
				                    aFestivoAux = aFestivosG[z].split("/");
				                    strFestivoAux = new Date(aFestivoAux[2],eval(aFestivoAux[1]-1),aFestivoAux[0]);
				                    if (objNuevaFecha.getTime() == strFestivoAux.getTime()){
					                    bFestivo = true;
					                    break;
				                    }else bFestivo = false;
			                    }
			                    if (!bFestivo)
			                        break;
			                    else
			                        objNuevaFecha= objNuevaFecha.add("d",1);
			                }
			                //Si despues de recorrer los festivos contiguos llegamos a un día >= que el primero de la semana -> abrir
	                        if (aSemanas[i][j] <= objNuevaFecha.getDate()){
		                        if (!bImpMesCerrado && !bHuecoMesAnt) 
		                            $I("idAcceso"+ (i+1)).src = "../../../images/icoAbiertoG.gif";
		                        break;
	                        }
	                        bContinuar=false;
			            }//if (bContinuar)
			            else
			                break;
		            }
	            }//For Dias de la semana
            }//FOR SEMANAS
            //--------------------------------------------------
        }
        if (isNaN(aSemanas[4][0])) $I("idAcceso5").src = "../../../images/imgSeparador.gif";
        if (isNaN(aSemanas[5][0])) $I("idAcceso6").src = "../../../images/imgSeparador.gif";
        if (isNaN(aSemanas[4][0])) $I("idAccesoPlanif5").src = "../../../images/imgSeparador.gif";
        if (isNaN(aSemanas[5][0])) $I("idAccesoPlanif6").src = "../../../images/imgSeparador.gif";
        
    }catch(e){
		mostrarErrorAplicacion("Error al formatear los días", e.message);
	}    	
}

function obtenerHorasImputadas(nMes, nAnno){
    try{
        //alert("Obtener horas imputadas de: \nUsuario: "+ num_empleado +"\nCalendario: "+ nCal +"\nMes: "+ nMes +"\nAño: "+ nAnno);
        if (nCal == 0) return;
        
        try{mostrarProcesando();}catch(e1){}
        var js_args = "horas@#@";
        js_args += num_empleado +"@#@";
        js_args += idFicepi +"@#@";
        js_args += nCal +"@#@";
        js_args += nMes +"@#@";
        js_args += nAnno;
        //alert(js_args);
        RealizarCallBack(js_args, "");
       
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las horas estándar e imputadas.", e.message);
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
            case "horas":
                aDatos = aResul[2].split("##");
                formateardias();
                break;
            case "festivos":
                eval(aResul[2]);
                setTimeout("formateardiasAux()", 50);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function reconectar(){
	var aDatosRec;  
        modalDialog.Show(strServer + "Capa_Presentacion/IAP/getProfesionalIAP.aspx", self, sSize(450,420))
            .then(function(ret) {
	            if (ret != null){
		            aDatosRec = ret.split("///");
                    nCal = aDatosRec[2];
                    $I("lblUsuario").innerText = Utilidades.unescape(aDatosRec[1]);
                    $I("lblCalendario").innerText = Utilidades.unescape(aDatosRec[3]);
                    $I("lblFUI").innerText = aDatosRec[5];
                    $I("lblUMC").innerText = AnoMesToMesAnoDescLong(aDatosRec[6]);
                    num_empleado = aDatosRec[0];

                    controlhuecos = (aDatosRec[4]=="1")? true:false;
                    strUMC = aDatosRec[6];
                    strAuxUltimoDia = aDatosRec[5];
                    
                    //$I("lblUMC").innerText = AnnomesAFecha(aDatosRec[6]).add("mo",1).add("d",-1).ToShortDateString();
                    if (aDatosRec[6]==""){
                        aDatosRec[6] = FechaAAnnomes(new Date().add("mo",-1)).ToString();
                    }
                    if (strAuxUltimoDia != ""){
                        aUltimoDia = strAuxUltimoDia.split("/");
                        oUltImputac = new Date(aUltimoDia[2],eval(aUltimoDia[1]-1),aUltimoDia[0]);  
                        
                        var oUMC = new Date(strUMC.substring(0,4),eval(strUMC.substring(4,6)-1),1).add("mo",1).add("d",-1);  
                        if (oUltImputac < oUMC) oUltImputac = oUMC;
                        
                    } else oUltImputac = null;

                    aSemLab	= aDatosRec[7].split(",");
                    idFicepi = aDatosRec[8];
	                for (var i=0; i<aSemLab.length; i++){
	                    if (aSemLab[i] == "0"){
	                        $I("tblLiterales").rows[0].cells[i].style.color = "red";
	                        $I("tblLiterales").rows[0].cells[i].setAttribute("style", "color:red");
	                        $I("tblLiterales").rows[0].cells[i].title = "Jornada no laborable";
	                    }
	                }
            	    
                    $I("imgProfesional").src = strServer+ "images/imgUSU" + aDatosRec[9] + aDatosRec[10] + ".gif";
                    //Cierro los candados. El formateradias ya se ocupará de abrir los pertinentes
	                for (i=1;i<7;i++){
		                $I("idAcceso"+ i).src = "../../../images/icoCerradoG.gif";
	                }
            	    
		            //Cargo los festivos de ese profesional
                    var js_args = "festivos@#@";
                    RealizarCallBack(js_args, "");
	            }
            });
    window.focus();
	
}

function mostrarHorario(){
    try{
        if (nCal == 0) return;
        var strEnlace = strServer + "Capa_Presentacion/IAP/Calendario/Horario.aspx?nCalendario="+ codpar(nCal) +"&nAnno="+ codpar(nCurrentYear);
        modalDialog.Show(strEnlace, self, sSize(810,700));
        window.focus();
	    
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle horario", e.message);
	}
}

