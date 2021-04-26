function init(){
    try{
        if (!mostrarErrores()) return;
    
        var dHoy = new Date();
        CrearCalendarios(FechaAAnnomes(dHoy));
        
	    var reg = /\//g;
	    var sAnnomes = "";
	    if (sFecha == "")
	        sFecha = dHoy.ToShortDateString();
	        
        nAnnomes = sFecha.substring(6,10)+sFecha.substring(3,5);
        generarTablaMes("tblMesIni1", AddAnnomes(nAnnomes, -1));
        generarTablaMes("tblMesIni2", nAnnomes);
        var dDesde = cadenaAfecha(sFecha);
        if ($I("tblMesIni2_td_"+ dDesde.ToShortDateString().replace(reg,"")) != null){
            if (ie) $I("tblMesIni2_td_"+ dDesde.ToShortDateString().replace(reg,"")).click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                $I("tblMesIni2_td_"+ dDesde.ToShortDateString().replace(reg,"")).dispatchEvent(clickEvent);
            }
        }
        
        setOp($I("btnAceptar"), 100);
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function CrearCalendarios(nAnnomes){
    try{
        generarTablaMes("tblMesIni1", AddAnnomes(nAnnomes, -1));
        generarTablaMes("tblMesIni2", nAnnomes);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a crear calendarios", e.message);
    }
}


function setAntSig(sOpcion){
    try{
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
                if (ie) oCelda.click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    oCelda.dispatchEvent(clickEvent);
                }
            }
        }
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
                objTablaMes.rows[nFila].cells[nCelda].onclick = null;
                objTablaMes.rows[nFila].cells[nCelda].style.cursor = "default";
                if (objTablaMes.rows[nFila].cells[nCelda].style.backgroundImage != "") {
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
            objCelda.ondblclick = function(){aceptar()};

            objCelda.id = sTabla +"_td_"+ objFecha.ToShortDateString().replace(reg,"");
            objCelda.innerText = objFecha.getDate();
            //objCelda.style.cursor = "pointer";
            objCelda.style.cursor = "url(../../../images/imgManoAzul2.cur)";
                
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
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana(){
    //        window.returnValue = null;
    //        window.close();
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

var oCeldaSelDesde = null; //id de la celda seleccionada
var oCeldaSelHasta = null; //id de la celda seleccionada
function selFecha(oCelda){
    try{
        var sFechaDesde = null;
        var oFechaDesde = null;
        var bExisteDesde = true;

        //alert(oCelda.id +"  "+oCelda.parentNode.parentNode.parentNode.id);
        if (oCeldaSelDesde == oCelda.id) return;

        if (oCeldaSelDesde != null && $I(oCeldaSelDesde) != null){
            $I(oCeldaSelDesde).style.backgroundImage = "";
            $I(oCeldaSelDesde).style.backgroundRepeat = "";
        }
        oCeldaSelDesde = oCelda.id;
        oCelda.style.backgroundImage = "url(imgFondoCalSel.gif)";
        oCelda.style.backgroundRepeat = "no-repeat";
        
        sFechaDesde = oCeldaSelDesde.split("_")[2];
        oFechaDesde = cadenaAfecha(sFechaDesde.substring(0,2)+"/"+sFechaDesde.substring(2,4)+"/"+sFechaDesde.substring(4,8));
       
        $I("divLiteralFechaInicio").innerText = oFechaDesde.ToLongDateString();
        if ($I("divLiteralFechaInicio").style.cursor != "pointer")
            $I("divLiteralFechaInicio").style.cursor = "pointer";
         
       //alert(oCeldaSelDesde +"  "+oCeldaSelHasta);
       if (getOp($I("btnAceptar")) != 100)
           setOp($I("btnAceptar"), 100);
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el día indicado", e.message);
    }
}

function setMesSeleccionado(){
    try{
        //alert(sOpcion);
        if (oCeldaSelDesde == null) return;
        //alert(oCeldaSelDesde);
        var nAnnomes = oCeldaSelDesde.split("_")[2].substring(4,8)+oCeldaSelDesde.split("_")[2].substring(2,4);
        //alert("mesamostrar: "+ mesamostrar);
        generarTablaMes("tblMesIni1", AddAnnomes(nAnnomes, -1));
        generarTablaMes("tblMesIni2", nAnnomes);

        if ($I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]) != null){
            if (ie) $I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]).click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                $I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]).dispatchEvent(clickEvent);
            }
            $I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]).style.backgroundImage = "url(imgFondoCalSel.gif)";
            $I("tblMesIni2_td_"+oCeldaSelDesde.split("_")[2]).style.backgroundRepeat = "no-repeat";
        }
    }catch(e){
        mostrarErrorAplicacion("Error al pulsar sobre el literal del día seleccionado.", e.message);
    }
}

function aceptar(){
    try{
        //alert("Aceptar: Desde: "+ oCeldaSelDesde +" Hasta: "+ oCeldaSelHasta);
        var sDesdeAux = "";
        if (oCeldaSelDesde != null){
            sDesdeAux = oCeldaSelDesde.split("_")[2];
            sDesdeAux = sDesdeAux.substring(0,2)+"/"+sDesdeAux.substring(2,4)+"/"+sDesdeAux.substring(4,8);
        }

//        window.returnValue = sDesdeAux;
//        window.close();
        var returnValue = sDesdeAux;
        modalDialog.Close(window, returnValue);        
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar la fecha.", e.message);
    }
}