var aDiaSem = new Array("L", "M", "X", "J", "V", "S", "D");
var bSemLab = new Array(false, false, false, false, false, false, false);
var bSalir = false;
function init(){
    try{
        if ($I("hdnIDCalendario").value == "0"){
            desActivarGrabar();
        }
        for (i=0;i<7;i++){
            if ($I("chkSemLab"+ aDiaSem[i]).checked){
                bSemLab[i] = true;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
var objSeleccionado = "";
function selFecha(IDCelda){
    try{
        if ((objSeleccionado != "")&&(objSeleccionado != IDCelda)){
            $I(objSeleccionado).className = "TDCal";
        }
        objSeleccionado = IDCelda;
        if ($I(IDCelda).className != "TDCalSel") $I(IDCelda).className = "TDCalSel";
        else if (bSemLab[$I(IDCelda).cellIndex]) $I(IDCelda).className = "TDCal";
             else $I(IDCelda).className = "textoCalFinde";
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el día indicado", e.message);
	}
}
function weekDay(){
    try{
        var now = this.getDay();
        if (now == 0) now = 6;
        else now--;
        var names = new Array(7);
        names[0]="L";
        names[1]="M";
        names[2]="X";
        names[3]="J";
        names[4]="V";
        names[5]="S";
        names[6]="D";
        return(names[now]);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el día de la semana", e.message);
	}
}
Date.prototype.DayOfWeek = weekDay;

function letraDia(strDia){
    for (i=0;i<7;i++){
        if (aDiaSem[i] == strDia) return i;
    }
}

function grabar(){
    try{

        mostrarProcesando();
        var strHoras = "";

        if ($I("txtJV").value == "") {
            mmoff("War", "Debes indicar Jornadas vacaciones", 300);
            ocultarProcesando();
            return;
        }
        if ($I("txtHL").value == "") {
            mmoff("War", "Debes indicar Horas laborables", 300);
            ocultarProcesando();
            return;
        }
        var aSpan = document.getElementsByTagName("SPAN");
        for (j=0; j<aSpan.length; j++){
            if (aSpan[j].id.indexOf("ctl00_CPHC_hor_") != -1){
                var nHoras = aSpan[j].innerText;
                if (nHoras == " ") nHoras = "0";
                //Controlar que no haya días laborables sin horas indicadas.
                var id = aSpan[j].id;
                var strFecha = id.substring(15,17) +"/"+ id.substring(17,19) +"/"+ id.substring(19,23);
                var dFechaAux = new Date(id.substring(19,23),eval(id.substring(17,19)-1),id.substring(15,17));

                if ((nHoras == "0") && (bSemLab[letraDia(dFechaAux.DayOfWeek())])) {
                    mmoff("War", "Debes asignar horas a todos los días laborables (" + strFecha + ")", 420);
			        ocultarProcesando();
			        return false;
			    }
                if (aSpan[j].parentNode.children[0].className != "textoCalFes") var nFestivo = 0;
                else var nFestivo = 1;
                strHoras += aSpan[j].id + "//" + aSpan[j].innerText + "//"+ nFestivo + "##";
            }
        }

        strHoras = strHoras.substring(0, strHoras.length - 2);

        if ($I("lblTotalHoras").innerText == "0,00") {
            var sMsg = "¡ Atención !<br><br>";
            sMsg += "Vas a eliminar el desglose horario del calendario para el año seleccionado.<br><br>";
            sMsg += "¿Deseas continuar?";
            ocultarProcesando();
            jqConfirm("", sMsg, "", "", "war", 400).then(function (answer) {
                if (answer) {
                    borrar();
                }
                
                return false;
            })
        }
        else LLamarGrabar(strHoras);
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos", e.message);
        return false;
    }
}

function LLamarGrabar(strHoras) {
    try{
        var js_args = "grabar@#@";
        js_args += $I("hdnIDCalendario").value +"@#@";
        js_args += $I("txtAnno").value +"@#@";
        
        var sSemLab = "";
        for (x=0;x<7;x++){
            if ($I("chkSemLab"+ aDiaSem[x]).checked) sSemLab += "1//";
            else sSemLab += "0//";
        }
        sSemLab = sSemLab.substring(0, sSemLab.length-2);
        js_args += sSemLab + "@#@";
        js_args += strHoras + "@#@";
        js_args += $I("txtJV").value + "@#@";
        js_args += $I("txtHL").value;

        RealizarCallBack(js_args, ""); 
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}
function setJornDen() {
    $I("txtJD").value = $I("txtJH").value - $I("txtJV").value;
}
function asignar(){
    try{
        var bDiaSem = new Array(false, false, false, false, false, false, false);
        var sw = 0;
        for (i=0;i<7;i++){
            if ($I("chklstDias_"+i).checked){
                bDiaSem[i] = true;
                sw = 1;
            }
        }
        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "Debes seleccionar algún día de la semana", 320);
            return;
        }
        if ($I("txtFecIni").value == ""){
            ocultarProcesando();
            mmoff("War", "Debes seleccionar una fecha para asignarla como inicio del periodo", 420);
            return;
        }
        if ($I("txtFecFin").value == ""){
            ocultarProcesando();
            mmoff("War", "Debes seleccionar una fecha para asignarla como fin del periodo", 420);
            return;
        }
        if ($I("txtHoras").value == ""){
            ocultarProcesando();
            mmoff("War", "Debes indicar las horas a asignar", 300);
            $I("txtHoras").focus();
            return;
        }
        
        if ($I("txtHoras").value == "0,0"){
            var nHoras = " ";  //blanco para que no se desalinee.
        }else{
        var aHoras = $I("txtHoras").value.split(",");
            if (aHoras[1] == "0") var nHoras = aHoras[0];
            else var nHoras = $I("txtHoras").value;
        }
        
		var intDifDias = DiffDiasFechas($I("txtFecIni").value, $I("txtFecFin").value);
		if (intDifDias < 0) {
		    mmoff("War", "El fin del rango temporal debe ser posterior al inicio.", 420);
		    return;
		}

        var strDesde = $I("txtFecIni").value;
        var dDesde = new Date(strDesde.substring(6,10),parseInt(strDesde.substring(3,5),10)-1,strDesde.substring(0,2)).add("d",-1);
		var objNuevaFecha;
		for (y = 0; y <= intDifDias; y++){
            objNuevaFecha = dDesde.add("d", 1);
			if (bDiaSem[letraDia(objNuevaFecha.DayOfWeek())]){
			    //Hay que asignar este día
			    var objLabel = obtenerObjetoLabelDia(objNuevaFecha);
			    objLabel.innerText = nHoras;
			}
		}
        
        calcularTotales();
	}catch(e){
		mostrarErrorAplicacion("Error al asignar los horarios del calendario.", e.message);
	}
	ocultarProcesando();
	bCambios = true;
}

function establecerInicio(){
    try{
        if (objSeleccionado != ""){
            strFecha = objSeleccionado.substring(14,16)+"/"+objSeleccionado.substring(16,18)+"/"+objSeleccionado.substring(18,22);
            $I("txtFecIni").value = strFecha;
            $I(objSeleccionado).className = "TDCal";
            objSeleccionado = "";
        } else {
            mmoff("War", "Debes seleccionar una fecha para asignarla como inicio del periodo", 420);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la fecha de inicio", e.message);
	}
}

function establecerFinal(){
    try{
        if (objSeleccionado != ""){
            strFecha = objSeleccionado.substring(14,16)+"/"+objSeleccionado.substring(16,18)+"/"+objSeleccionado.substring(18,22);
            $I("txtFecFin").value = strFecha;
            $I(objSeleccionado).className = "TDCal";
            objSeleccionado = "";
        }else{
            mmoff("War", "Debes seleccionar una fecha para asignarla como fin del periodo", 420);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la fecha de fin", e.message);
	}
}

function establecerFestivo(){
    try{
        if (objSeleccionado != ""){
            //Actualizo el nº de jornadas hábiles
            var nJorHab = $I("txtJH").value;
            if ($I(objSeleccionado).children[0].className == "textoCal")
                nJorHab--;
            else {
                if ($I(objSeleccionado).children[0].className == "textoCalFes" && bSemLab[$I(objSeleccionado).cellIndex])
                    nJorHab++;
            }
            $I("txtJH").value = nJorHab;
            $I("txtJD").value = nJorHab - $I("txtJV").value;

            if ($I(objSeleccionado).children[0].className != "textoCalFes")
                $I(objSeleccionado).children[0].className = "textoCalFes";
            else if (bSemLab[$I(objSeleccionado).cellIndex])
                    $I(objSeleccionado).children[0].className = "textoCal";
                 else
                    $I(objSeleccionado).children[0].className = "textoCalFinde";
 
            //Para "desmarcar la celda"
            $I(objSeleccionado).className = "TDCal";
           
            objSeleccionado = "";
            activarGrabar();
        } else {
            mmoff("War", "Debes seleccionar una fecha para asignarla como festiva", 420);
        }
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el día festivo", e.message);
	}
}

function obtenerObjetoLabelDia(objFecha){
    try{
        var strID = "";
        var strFecha = "";
        var strDia = "";
        var strMes = "";
        var strAnno = "";
        
	    strDia = objFecha.getDate().toString();
	    if (strDia.length == 1) strDia = "0" + strDia;
	    strMes = eval(objFecha.getMonth()+1).toString();
	    if (strMes.length == 1) strMes = "0" + strMes;
	    strAnno= objFecha.getFullYear().toString();
	    if (strAnno.length == 2) strAnno = "20" + strAnno;
    	
	    strID = "ctl00_CPHC_hor_"+ strDia + strMes + strAnno;
	    var objLabel = $I(strID);
	    return objLabel;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el objeto label a partir de la fecha", e.message);
	}
}
function obtenerObjetoSpanDia(objFecha) {
    try {
        var strID = "";
        var strFecha = "";
        var strDia = "";
        var strMes = "";
        var strAnno = "";

        strDia = objFecha.getDate().toString();
        if (strDia.length == 1) strDia = "0" + strDia;
        strMes = eval(objFecha.getMonth() + 1).toString();
        if (strMes.length == 1) strMes = "0" + strMes;
        strAnno = objFecha.getFullYear().toString();
        if (strAnno.length == 2) strAnno = "20" + strAnno;

        strID = "ctl00_CPHC_fec_" + strDia + strMes + strAnno;
        var objSpan = $I(strID);
        return objSpan;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el objeto Span a partir de la fecha", e.message);
    }
}
function limpiar(){
    try{
        var nHoras = " ";  //blanco para que no se desalinee.
		
		var intDifDias = DiffDiasFechas("01/01/"+$I("txtAnno").value, "31/12/"+$I("txtAnno").value);
        var strDesde = "01/01/"+$I("txtAnno").value;
        var dDesde = new Date(strDesde.substring(6,10),parseInt(strDesde.substring(3,5),10)-1,strDesde.substring(0,2)).add("d",-1);
		var objNuevaFecha;
		for (y = 0; y <= intDifDias; y++){
            objNuevaFecha = dDesde.add("d", 1);
		    var objLabel = obtenerObjetoLabelDia(objNuevaFecha);
		    objLabel.innerText = nHoras;
		}
		
		$I("lblTotalHoras").innerText = "0,00";
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar los horarios del calendario", e.message);
	}
    ocultarProcesando();
}

function calcularTotales(){
    try{
        var nTotalHoras = 0;
        var nHoras = 0;
        
		var intDifDias = DiffDiasFechas("01/01/"+$I("txtAnno").value, "31/12/"+$I("txtAnno").value);
        var strDesde = "01/01/"+$I("txtAnno").value;
        var dDesde = new Date(strDesde.substring(6,10),parseInt(strDesde.substring(3,5),10)-1,strDesde.substring(0,2)).add("d",-1);
		var objNuevaFecha;
		for (y = 0; y <= intDifDias; y++){
            objNuevaFecha = dDesde.add("d", 1);
		    var objLabel = obtenerObjetoLabelDia(objNuevaFecha);
		    nHoras = objLabel.innerText;
		    if (nHoras != " "){
		        nTotalHoras = nTotalHoras + parseFloat(nHoras.replace(",","."));
            }
		}
		$I("lblTotalHoras").innerText = nTotalHoras.ToString("N");
        
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al calcular el número de horas totales", e.message);
	}
}

function borrar(){
    try {
        mostrarProcesando();
        var js_args = "borrar@#@";
        js_args += $I("hdnIDCalendario").value +"@#@";
        js_args += $I("txtAnno").value;

        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los datos", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) AccionBotonera("regresar", "P");
                break;
            case "cargarFestivos":
                var bCambios = false;
                var aDatos = aResul[2].split("///");
                var intDifDias = DiffDiasFechas("01/01/" + $I("txtAnno").value, "31/12/" + $I("txtAnno").value);
                var strDesde = "01/01/" + $I("txtAnno").value;
                var dDesde = new Date(strDesde.substring(6, 10), parseInt(strDesde.substring(3, 5), 10) - 1, strDesde.substring(0, 2)).add("d", -1);
                var objNuevaFecha;
                for (y = 0; y <= intDifDias; y++) {
                    objNuevaFecha = dDesde.add("d", 1);
                    var objSpan1 = obtenerObjetoSpanDia(objNuevaFecha);
                    if (objSpan1 == null) continue;
                    for (var i = 0; i < aDatos.length; i++) {
                        if (aDatos[i] == "") continue;
                        if (objSpan1.id.substring(0, 19) == 'ctl00_CPHC_fec_' + aDatos[i]) {
                            //Actualizo el nº de jornadas hábiles
                            var nJorHab = $I("txtJH").value;
                            if (objSpan1.className == "textoCal") nJorHab--;
                            $I("txtJH").value = nJorHab;
                            $I("txtJD").value = nJorHab - $I("txtJV").value;

                            objSpan1.className = "textoCalFes";
                            bCambios = true;
                            continue;
                        }
                    }
                }
                if (bCambios) activarGrabar();
                break;
            case "borrar":
                limpiar();
                desActivarGrabar();
                break;
        }
        ocultarProcesando();
        //popupWinespopup_winLoad();
    }
}

function comprobarCambios(sender, args)
{
/*
    if (bCambios){
		if (!confirm("Se han modificado los datos. ¿Deseas grabarlos?")){
			args.IsValid = true;
		}else{
			grabar();
            args.IsValid = true;
	    }
    }else{
        args.IsValid = true;
    }
*/    
}


function modificarSemLab(strIDchk){
    mostrarProcesando();
    setTimeout("modificarSemLabAux('" + strIDchk +"')", 50);

}
/*
            //Actualizo el nº de jornadas hábiles
            var nJorHab = $I("txtJH").value;
            if ($I(objSeleccionado).children[0].className == "textoCal")
                nJorHab--;
            else {
                if ($I(objSeleccionado).children[0].className == "textoCalFes" && bSemLab[$I(objSeleccionado).cellIndex])
                    nJorHab++;
            }
            $I("txtJH").value = nJorHab;
            $I("txtJD").value = nJorHab - $I("txtJV").value;

*/
function modificarSemLabAux(strIDchk){
    try{
        var bLab = $I(strIDchk).checked;
        var sLetra = strIDchk.substring(strIDchk.length-1,strIDchk.length);

        for (i=0;i<7;i++){
            if (sLetra == aDiaSem[i]){
                bSemLab[i] = bLab;
                break;
            }
        }
        //Actualizo el nº de jornadas hábiles
        var nJorHab = $I("txtJH").value;

		var intDifDias = DiffDiasFechas("01/01/"+$I("txtAnno").value, "31/12/"+$I("txtAnno").value);
        var strDesde = "01/01/"+$I("txtAnno").value;
        var dDesde = new Date(strDesde.substring(6,10),parseInt(strDesde.substring(3,5),10)-1,strDesde.substring(0,2)).add("d",-1);
		var objNuevaFecha;
		for (y = 0; y <= intDifDias; y++){
		    objNuevaFecha = dDesde.add("d", 1);

            //Calculo el indice del día en curso. Si es domingo le pongo indice 6 sino uno menos de lo que dé la función getDay
		    var nIndiceDia = objNuevaFecha.getDay();
		    if (nIndiceDia == 0) nIndiceDia = 6;
		    else nIndiceDia = nIndiceDia - 1;

		    var objLabel = obtenerObjetoLabelDia(objNuevaFecha);

		    if (i == nIndiceDia) {//(i == objLabel.parentNode.cellIndex)

		        if (objLabel.parentNode.children[0].className != "textoCalFes"){
		            if (bSemLab[nIndiceDia]) {//(bSemLab[objLabel.parentNode.cellIndex])
		                objLabel.parentNode.children[0].className = "textoCal";
		                nJorHab++;
		            }
		            else {
		                objLabel.parentNode.children[0].className = "textoCalFinde";
		                nJorHab--;
		            }
                }
            }		    
		}
		$I("txtJH").value = nJorHab;
		$I("txtJD").value = nJorHab - $I("txtJV").value;

		bCambios = true;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al modificar la semana laboral", e.message);
	}
}
function cargarFestivos(iCodProvincia) {
    try {
        if (iCodProvincia == "") {
            mmoff("War", "El calendario no tiene provincia de festivos asignada", 360);
            return;
        }
        mostrarProcesando();
        var js_args = "cargarFestivos@#@";
        js_args += iCodProvincia + "@#@";
        js_args += $I("txtAnno").value;
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el calendario festivo de la provincia vinculada a ese calendario", e.message);
    }
}