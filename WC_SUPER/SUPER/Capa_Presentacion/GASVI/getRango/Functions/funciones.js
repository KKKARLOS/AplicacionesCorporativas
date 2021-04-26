function init(){
    try{
        if (!mostrarErrores()) return;
    
        var dHoy = new Date();
        CrearCalendarios(FechaAAnnomes(dHoy));
        
	    var reg = /\//g;
	    var sAnnomes = "";
	    if (sFechaDesde != ""){
	        nAnnomes = sFechaDesde.substring(6,10)+sFechaDesde.substring(3,5);
            generarTablaMes("tblMesIni1", AddAnnomes(nAnnomes, -1));
            generarTablaMes("tblMesIni2", nAnnomes);
	    }
	    if (sFechaHasta != ""){
	        nAnnomes = sFechaHasta.substring(6,10)+sFechaHasta.substring(3,5);
            generarTablaMes("tblMesFin1", AddAnnomes(nAnnomes, -1));
            generarTablaMes("tblMesFin2", nAnnomes);
	    }
        if (sFechaDesde != ""){
	        var dDesde = cadenaAfecha(sFechaDesde);
	        if ($I("tblMesIni2_td_" + dDesde.ToShortDateString().replace(reg, "")) != null)
                //$I("tblMesIni2_td_"+ dDesde.ToShortDateString().replace(reg,"")).click();
	            seleccionar($I("tblMesIni2_td_" + dDesde.ToShortDateString().replace(reg, "")));
        }
        if (sFechaHasta != ""){
	        var dHasta = cadenaAfecha(sFechaHasta);
	        if ($I("tblMesFin2_td_"+ dHasta.ToShortDateString().replace(reg,"")) != null)
	            //$I("tblMesFin2_td_" + dHasta.ToShortDateString().replace(reg, "")).click();
	            seleccionar($I("tblMesFin2_td_" + dHasta.ToShortDateString().replace(reg, "")));
        }
        if (sFechaDesde != "" && sFechaHasta != "")
            setOp($I("btnAceptar"), 30);
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function CrearCalendarios(nAnnomes){
    try{
//    var oF1 = new Date();  
    
        generarTablaMes("tblMesIni1", AddAnnomes(nAnnomes, -1));
        generarTablaMes("tblMesIni2", nAnnomes);
        generarTablaMes("tblMesFin1", AddAnnomes(nAnnomes, -1));
        generarTablaMes("tblMesFin2", nAnnomes);
//    var oF2 = new Date(); 
//    alert((oF2.getTime() - oF1.getTime())  + " ms.");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a crear calendarios", e.message);
    }
}


function setAntSig(sInicioFin, sOpcion){
    try{
        //alert(sInicioFin+" "+sOpcion);
        if (sInicioFin=="I"){
            var nAnnomesRef = $I("tblMesIni1").nAnnomes;
            if (sOpcion == "A"){
                generarTablaMes("tblMesIni1", AddAnnomes(nAnnomesRef, -2));
                generarTablaMes("tblMesIni2", AddAnnomes(nAnnomesRef, -1));
            }else{
                generarTablaMes("tblMesIni1", AddAnnomes(nAnnomesRef, 2));
                generarTablaMes("tblMesIni2", AddAnnomes(nAnnomesRef, 3));
            }
            
            if (oCeldaSelDesde != null){ //Si hubiera alguna fecha desde seleccionada
                if ($I(oCeldaSelDesde) != null){ //y existiera en los meses mostrados
                    var oCelda = $I(oCeldaSelDesde)
                    oCeldaSelDesde = null; //porque si no, la función de seleccionar no activa de nuevo la fecha
                    //oCelda.click(); //se selecciona
                    seleccionar(oCelda);
                }
            }
        }else{
            var nAnnomesRef = $I("tblMesFin1").nAnnomes;
            if (sOpcion == "A"){
                generarTablaMes("tblMesFin1", AddAnnomes(nAnnomesRef, -2));
                generarTablaMes("tblMesFin2", AddAnnomes(nAnnomesRef, -1));
            }else{
                generarTablaMes("tblMesFin1", AddAnnomes(nAnnomesRef, 2));
                generarTablaMes("tblMesFin2", AddAnnomes(nAnnomesRef, 3));
            }
            
            if (oCeldaSelHasta != null){ //Si hubiera alguna fecha desde seleccionada
                if ($I(oCeldaSelHasta) != null){ //y existiera en los meses mostrados
                    var oCelda = $I(oCeldaSelHasta)
                    oCeldaSelHasta = null; //porque si no, la función de seleccionar no activa de nuevo la fecha
                    seleccionar(oCelda);
                    //oCelda.click(); //se selecciona
                }
            }
        }
//    var oF1 = new Date();  
//        generarTablaMes("tblMesIni1", AddAnnomes(nAnnomes, -1));
//    var oF2 = new Date(); 
//    alert((oF2.getTime() - oF1.getTime())  + " ms.");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a cambiar de mes", e.message);
    }
}

function daysInMonth(iMonth, iYear)
{
	return 32 - new Date(iYear, iMonth, 32).getDate();
}

function generarTablaMes(sTabla, nAnnomes){
    try{
        var objFila;
        var objCelda;
        var nIndiceItemDia = 0;
	    var reg = /\//g;
        var objTablaMes = $I(sTabla);
        objTablaMes.nAnnomes = nAnnomes;
        //objTablaMes.style.border = "1";
        var bEntraMes = false;
        var nIndiceFila = 1;
        var nIndiceColumna = 1;
        
        for (var nFila=1; nFila<7; nFila++){
            for (var nCelda=0; nCelda<7; nCelda++){
                objTablaMes.rows[nFila].cells[nCelda].id = "";
                objTablaMes.rows[nFila].cells[nCelda].innerText = "";
                if (objTablaMes.rows[nFila].cells[nCelda].style.backgroundImage != ""){
                    objTablaMes.rows[nFila].cells[nCelda].style.backgroundImage = "";
                    objTablaMes.rows[nFila].cells[nCelda].style.backgroundRepeat = "";
                }
            }
        }
        
        $I("l"+sTabla.substring(1, sTabla.length)).innerText = AnoMesToMesAnoDescLong(nAnnomes);
        var dFecha = AnnomesAFecha(nAnnomes);
        var nDiasMes = daysInMonth(dFecha.getMonth(), dFecha.getFullYear());

        dFecha.add("d",-1);
        var objFecha = null;
        var sDiaSemana = "";
        for (var i = nIndiceItemDia; i < nDiasMes; i++)
        {
            objFecha = dFecha.add("d",1);
            sDiaSemana = objFecha.DayOfWeek();
            if (!bEntraMes)
            {
                bEntraMes = true;
               // #region Día de la semana del día uno de mes.
                switch (sDiaSemana)
                {
                    case "L": nIndiceColumna = 0; break;
                    case "M": nIndiceColumna = 1; break;
                    case "X": nIndiceColumna = 2; break;
                    case "J": nIndiceColumna = 3; break;
                    case "V": nIndiceColumna = 4; break;
                    case "S": nIndiceColumna = 5; break;
                    case "D": nIndiceColumna = 6; break;
                }
               // #endregion
            }

            objCelda = objTablaMes.rows[nIndiceFila].cells[nIndiceColumna];
            objCelda.onclick = function(){selFecha(this)};

            objCelda.id = sTabla +"_td_"+ objFecha.ToShortDateString().replace(reg,"");
            objCelda.innerText = objFecha.getDate();
            objCelda.style.cursor = "pointer";
                
            if (sDiaSemana == "S" || sDiaSemana == "D") 
                objCelda.style.color = "#F14E4E";
            else
                objCelda.style.color = "#315D6B";   

            if (nIndiceColumna == 6)
            {
                nIndiceFila++;
                nIndiceColumna = 0;
            }
            else
            {
                nIndiceColumna++;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la generación de la tabla.", e.message);
    }
}

function cerrarVentana(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

var oCeldaSelDesde = null; //id de la celda seleccionada
var oCeldaSelHasta = null; //id de la celda seleccionada
function selFecha(oCelda){
    try{
        var sFechaDesde = null;
        var oFechaDesde = null;
        var sFechaHasta = null;
        var oFechaHasta = null;
        var bExisteDesde = true;
        var bExisteHasta = true;

        //alert(oCelda.id +"  "+oCelda.parentNode.parentNode.parentNode.id);
        if (oCelda.parentNode.parentNode.parentNode.id.indexOf("Ini") > -1){ //Inicio
            if (oCeldaSelDesde == oCelda.id) return;

            if (oCeldaSelDesde != null && $I(oCeldaSelDesde) != null){
                $I(oCeldaSelDesde).style.backgroundImage = "";
                $I(oCeldaSelDesde).style.backgroundRepeat = "";
            }
            oCeldaSelDesde = oCelda.id;
            oCelda.style.backgroundImage = "url(imgFondoCalSel.gif)";
            oCelda.style.backgroundRepeat = "no-repeat";
            
            if (oCeldaSelHasta == null){
                bExisteHasta = false;
                var sCeldaSelHastaAux = oCeldaSelDesde.replace("Ini","Fin");
                if ($I(sCeldaSelHastaAux) != null) seleccionar($I(sCeldaSelHastaAux));
                    //$I(sCeldaSelHastaAux).click();
                oCeldaSelHasta = sCeldaSelHastaAux;
            }
            
            sFechaDesde = oCeldaSelDesde.split("_")[2];
            oFechaDesde = cadenaAfecha(sFechaDesde.substring(0,2)+"/"+sFechaDesde.substring(2,4)+"/"+sFechaDesde.substring(4,8));
            sFechaHasta = oCeldaSelHasta.split("_")[2];
            oFechaHasta = cadenaAfecha(sFechaHasta.substring(0,2)+"/"+sFechaHasta.substring(2,4)+"/"+sFechaHasta.substring(4,8));
           
            $I("divLiteralFechaInicio").innerText = oFechaDesde.ToLongDateString();
            if ($I("divLiteralFechaInicio").style.cursor != "pointer")
                $I("divLiteralFechaInicio").style.cursor = "pointer";
             
            if (!bExisteHasta){
                $I("divLiteralFechaFin").innerText = oFechaHasta.ToLongDateString();
                if ($I("divLiteralFechaFin").style.cursor != "pointer")
                    $I("divLiteralFechaFin").style.cursor = "pointer";
            }
            
           
            //alert(oFechaDesde +"\n"+ oFechaHasta); //.getTime()
            //if (oFechaHasta < oFechaDesde){
            if (oFechaHasta.getTime() < oFechaDesde.getTime()){
                //alert("hay que actualizar");
                var sCeldaSelHastaAux = oCeldaSelDesde.replace("Ini","Fin");
                if ($I(sCeldaSelHastaAux) != null){
                    //alert("hasta sí existe");
                    //$I(sCeldaSelHastaAux).click();
                    seleccionar($I(sCeldaSelHastaAux));
                }else{
                    //alert("hasta no existe");
                    //Actualizar Literal fecha hasta
                    $I("divLiteralFechaFin").innerText = $I("divLiteralFechaInicio").innerText;
                    //Borrar fecha hasta seleccionada
                    $I(oCeldaSelHasta).style.backgroundImage = "";
                    $I(oCeldaSelHasta).style.backgroundRepeat = "";
                    //Actualizar oCeldaSelHasta
                    oCeldaSelHasta = oCeldaSelDesde.replace("Ini","Fin");
                }
            }
        }else{ //Fin
            if (oCeldaSelHasta == oCelda.id) return;

            if (oCeldaSelHasta != null && $I(oCeldaSelHasta) != null){
                $I(oCeldaSelHasta).style.backgroundImage = "";
                $I(oCeldaSelHasta).style.backgroundRepeat = "";
            }
            oCeldaSelHasta = oCelda.id;
            oCelda.style.backgroundImage = "url(imgFondoCalSel.gif)";
            oCelda.style.backgroundRepeat = "no-repeat";
            if (oCeldaSelDesde == null){
                bExisteDesde = false;
                var sCeldaSelDesdeAux = oCeldaSelHasta.replace("Fin","Ini");
                if ($I(sCeldaSelDesdeAux) != null) seleccionar($I(sCeldaSelDesdeAux));
                    //$I(sCeldaSelDesdeAux).click();
                oCeldaSelDesde = sCeldaSelDesdeAux;
            }
            
            var sFechaDesde = oCeldaSelDesde.split("_")[2];
            var oFechaDesde = cadenaAfecha(sFechaDesde.substring(0,2)+"/"+sFechaDesde.substring(2,4)+"/"+sFechaDesde.substring(4,8));
            var sFechaHasta = oCeldaSelHasta.split("_")[2];
            var oFechaHasta = cadenaAfecha(sFechaHasta.substring(0,2)+"/"+sFechaHasta.substring(2,4)+"/"+sFechaHasta.substring(4,8));

            $I("divLiteralFechaFin").innerText = oFechaHasta.ToLongDateString();
            if ($I("divLiteralFechaFin").style.cursor != "pointer")
                $I("divLiteralFechaFin").style.cursor = "pointer";
            
            if (!bExisteDesde){
                $I("divLiteralFechaInicio").innerText = oFechaDesde.ToLongDateString();
                if ($I("divLiteralFechaInicio").style.cursor != "pointer")
                    $I("divLiteralFechaInicio").style.cursor = "pointer";
            }
            
            //alert(oFechaDesde +"\n"+ oFechaHasta);
            //if (oFechaHasta < oFechaDesde){
            if (oFechaHasta.getTime() < oFechaDesde.getTime()){
                //alert("hay que actualizar");
                var sCeldaSelDesdeAux = oCeldaSelHasta.replace("Fin","Ini");
                if ($I(sCeldaSelDesdeAux) != null){
                    //alert("desde sí existe");
                    //$I(sCeldaSelDesdeAux).click();
                    seleccionar($I(sCeldaSelDesdeAux));
                }else{
                    //alert("desde no existe");
                    //Actualizar Literal fecha desde
                    $I("divLiteralFechaInicio").innerText = $I("divLiteralFechaFin").innerText;
                    //Borrar fecha desde seleccionada
                    $I(oCeldaSelDesde).style.backgroundImage = "";
                    $I(oCeldaSelDesde).style.backgroundRepeat = "";
                    //Actualizar oCeldaSelDesde
                    oCeldaSelDesde = oCeldaSelHasta.replace("Fin","Ini");
                }
            }
        }
       
       //alert(oCeldaSelDesde +"  "+oCeldaSelHasta);
       if (getOp($I("btnAceptar")) != 100)
           setOp($I("btnAceptar"), 100);
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el día indicado", e.message);
    }
}

function setMesSeleccionado(sOpcion){
    try{
        //alert(sOpcion);
        if (sOpcion == "I"){
            if (oCeldaSelDesde == null) return;
            //alert(oCeldaSelDesde);
            var nAnnomes = oCeldaSelDesde.split("_")[2].substring(4,8)+oCeldaSelDesde.split("_")[2].substring(2,4);
            //alert("mesamostrar: "+ mesamostrar);
            generarTablaMes("tblMesIni1", AddAnnomes(nAnnomes, -1));
            generarTablaMes("tblMesIni2", nAnnomes);

            if ($I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]) != null){
                //$I("tblMesIni2_td_" + oCeldaSelDesde.split("_")[2]).click();
                seleccionar($I("tblMesIni2_td_" + oCeldaSelDesde.split("_")[2]));
                $I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]).style.backgroundImage = "url(imgFondoCalSel.gif)";
                $I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]).style.backgroundRepeat = "no-repeat";
            }
        }else{
            if (oCeldaSelHasta == null) return;
            //alert(oCeldaSelHasta);
            var nAnnomes = oCeldaSelHasta.split("_")[2].substring(4,8)+oCeldaSelHasta.split("_")[2].substring(2,4);
            //alert("mesamostrar: "+ mesamostrar);
            generarTablaMes("tblMesFin1", AddAnnomes(nAnnomes, -1));
            generarTablaMes("tblMesFin2", nAnnomes);
            //alert("Añomes tabla fin 2: "+ tblMesFin2.nAnnomes);
            
            if ($I("tblMesFin2_td_"+oCeldaSelHasta.split("_")[2]) != null){
                //$I("tblMesFin2_td_" + oCeldaSelHasta.split("_")[2]).click();
                seleccionar($I("tblMesFin2_td_" + oCeldaSelHasta.split("_")[2]));
                $I("tblMesFin2_td_"+oCeldaSelHasta.split("_")[2]).style.backgroundImage = "url(imgFondoCalSel.gif)";
                $I("tblMesFin2_td_"+oCeldaSelHasta.split("_")[2]).style.backgroundRepeat = "no-repeat";
            }
        }
    }catch(e){
        mostrarErrorAplicacion("Error al pulsar sobre el literal del día seleccionado.", e.message);
    }
}

function aceptar(){
    try{
        //alert("Aceptar: Desde: "+ oCeldaSelDesde +" Hasta: "+ oCeldaSelHasta);
        var sDesdeAux = "";
        var sHastaAux = "";
        if (oCeldaSelDesde != null){
            sDesdeAux = oCeldaSelDesde.split("_")[2];
            sDesdeAux = sDesdeAux.substring(0,2)+"/"+sDesdeAux.substring(2,4)+"/"+sDesdeAux.substring(4,8);
        }
        if (oCeldaSelHasta != null){
            sHastaAux = oCeldaSelHasta.split("_")[2];
            sHastaAux = sHastaAux.substring(0,2)+"/"+sHastaAux.substring(2,4)+"/"+sHastaAux.substring(4,8);
        }
        var returnValue = sDesdeAux +"@#@"+ sHastaAux;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar el rango de fechas.", e.message);
    }
}