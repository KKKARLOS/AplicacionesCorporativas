
var oTarea = {"nPSN":0,
            "nPT":0,
            "nT":0,
            "desT":"",
            "ImpFes":false,
            "NOJC":false,
            "Obligaest":false,
            "ete":0,
            "ffe":"",
            "Comentario":""};

function init(){
    try{       
        if (nCal == 0){
	        $I("rdbImpMas_0").disabled = true;
	        $I("rdbImpMas_1").disabled = true;
	        $I("rdbImpMas_2").disabled = true;
	        //(ie)? $I("lblTarea").className = "texto" : $I("lblTarea").setAttribute("class","texto");
	        $I("lblTarea").className = "texto";
	        $I("lblTarea").onclick = null;
	        $I("cbovModo").disabled = true;
	        $I("chkFestivos").disabled = true;
	        $I("chkFinalizado").disabled = true;
	        if (btnCal == "I"){
	            $I("txtDesde").onclick = null;
	            $I("txtHasta").onclick = null;
	        }
	        else{
	            $I("txtDesde").onmousedown = null;
	            $I("txtDesde").onfocus = null;
	            $I("txtHasta").onmousedown = null;
	            $I("txtHasta").onfocus = null;
	        }
	        $I("txtvHoras").disabled = true;
	        $I("txtvComentario").disabled = true;
	        return;
        }
    	//seleccionar($I("rdbImpMas_0"));
        seleccionarOpcion();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
		if (aResul[0]=="getTarea2")
		    $I("txtIDTarea").select();
    }else{
        switch (aResul[0]){
            case "grabar":
                $I("txtvUDR").value = aResul[3];
                
                actualizarFechas(aResul[3]);
                
                if (aResul[2] != "") mmoff("War", aResul[2], 400); // Msg de imputaciones fuera de asociación al proyecto.

                if (oTarea.Obligaest) actualizarEstimacion(aResul[4], aResul[5]);
                setTimeout("getDatosIAP()", 20);
                desActivarGrabar();
                mmoff("Suc","Grabación correcta", 160); 

                break;
            case "getTarea":
                $I("chkFacturable").checked = (aResul[2]=="1")? true:false;
                if (ie) $I("lblModo").innerText = aResul[3];
                else $I("lblModo").textContent = aResul[3];
                $I("txtPriCon").value = aResul[4];
                $I("txtUltCon").value = aResul[5];
                $I("txtConHor").value = aResul[6];
                $I("txtConJor").value = aResul[7];
                $I("txtPteEst").value = aResul[8];
                $I("txtAvanEst").value = aResul[9];
                $I("txtTotPre").value = aResul[10];
                $I("txtFinPre").value = aResul[11];
                $I("txtIndicaciones").value = Utilidades.unescape(aResul[12]);
                $I("txtColectivas").value = Utilidades.unescape(aResul[13]);
                $I("txtvETE").value = aResul[14];
                $I("txtvFFE").value = aResul[15];
                $I("txtvObservaciones").value = Utilidades.unescape(aResul[16]);
                $I("chkFinalizada").checked = (aResul[17]=="1")? true:false;
                $I("txtProyecto").value = aResul[18];
                $I("txtPT").value = aResul[19];
                $I("txtFase").value = aResul[20];
                $I("txtActividad").value = aResul[21];
                break;
            case "getTarea2":
                obtenerTarea2(aResul[2]);
                break;
        }
        ocultarProcesando();
    }
}

function comprobarFestivos(){
    try{
        if (!oTarea.ImpFes){
		    $I("chkFestivos").checked = false;
		    $I("chkFestivos").disabled = true;
        }else if($I("hdnOpcion").value == "2"){
			$I("chkFestivos").disabled = false;
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar si se pueden imputar los días festivos", e.message);
    }
}

function seleccionarOpcion(){
    try{
        var nOp = getRadioButtonSelectedValue("rdbImpMas", false);
        //alert(nOp);
        switch (nOp){
            case "1":
                imputarOpcion1();
                break;
            case "2":
                imputarOpcion2();
                break;
            case "3":
                imputarOpcion3();
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar alguna opción", e.message);
    }
}
//function prueba()
//{
//    alert('prueba');
//}
function imputarOpcion1(){
    try{
	    //alert("Opcion1");
	    $I("cbovModo").value = 1;
	    $I("cbovModo").disabled = true;
	    $I("chkFestivos").checked = false;
	    $I("chkFestivos").disabled = true;
	    $I("txtvHoras").value = "";
	    $I("txtvHoras").disabled = true;
	    $I("txtvHoras").style.backgroundColor = "#CACACA";
	    $I("txtDesde").value = strDiaSigUDR;
	    //if (btnCal == "I")
        //{
	        $I("txtDesde").readOnly = true;
	        $I("txtDesde").onclick = null; //function () { prueba(); };
	        $I("txtDesde").onmousedown = null;
	        $I("txtDesde").onfocus = null;
	        //$I("txtDesde").detachEvent("onfocus", focoFecha);
	    //}
	    $I("hdnOpcion").value = "1";	
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la opción 1", e.message);
    }
}

function imputarOpcion2(){
    try{
	    //alert("Opcion2");
	    $I("cbovModo").disabled = false;
	    $I("chkFestivos").disabled = false;
	    $I("txtvHoras").readonly = false;
	    $I("txtvHoras").style.backgroundColor = "#FFFFFF";
	    $I("txtvHoras").disabled = false;
	    $I("txtDesde").value = strDiaSigUDR;
	    if (btnCal == "I") {
	        $I("txtDesde").readOnly = true;
	        $I("txtDesde").onclick = function () { mc(this); };
	    }
	    else {
	        $I("txtDesde").readOnly = false;
	        $I("txtDesde").onmousedown = function () { mc1(this); };
	        $I("txtDesde").onfocus = function (e) { focoFecha(e); };
	        //$I("txtDesde").attachEvent("onfocus", focoFecha);
	    }
	    $I("hdnOpcion").value = "2";	
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la opción 2", e.message);
    }
}

function imputarOpcion3(){
    try{
	    //alert("Opcion3");
	    $I("cbovModo").value = 1;
	    $I("cbovModo").disabled = true;
	    $I("chkFestivos").checked = false;
	    $I("chkFestivos").disabled = true;
	    $I("txtvHoras").value = "";
	    $I("txtvHoras").disabled = true;
	    $I("txtvHoras").style.backgroundColor = "#CACACA";
	    $I("txtDesde").value = strDiaSigUDR;
	    if (btnCal == "I") {
	        $I("txtDesde").readOnly = true;
	        $I("txtDesde").onclick = function () { mc(this); };
	    }
	    else {
	        $I("txtDesde").readOnly = false;
	        $I("txtDesde").onmousedown = function () { mc1(this); };
	        $I("txtDesde").onfocus = function (e) { focoFecha(e); };
	        //$I("txtDesde").attachEvent("onfocus", focoFecha);
        }
	    $I("hdnOpcion").value = "3";	
	}catch(e){
	    var strTitulo = "Error al seleccionar la opción 3";
		mostrarErrorAplicacion(strTitulo, e.message);
    }
}

function actualizarEstimacion(strETE, strFFE){
    try{
	    oTarea.ete = parseFloat(dfn($I("txtvETE").value));
	    oTarea.ffe = $I("txtvFFE").value;
	    oTarea.Comentario = Utilidades.escape($I("txtvObservaciones").value);
	    
        ocultarProcesando();

        if (strETE != ""){
	        oTarea.ete = parseFloat(dfn(strETE));
    	    $I("txtvETE").value = strETE.ToString("N");
	        mmoff("Inf","Se han imputado más horas de las estimadas, por lo que se ha actualizado dicha estimación.",400);
	    }
	    if (strFFE != ""){
	        oTarea.ffe = strFFE;
	        $I("txtvFFE").value = strFFE;
    		mmoff("Inf","Se ha realizado alguna imputación en una fecha posterior a la estimada,\npor lo que se ha actualizado dicha estimación.",400);
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar las horas estimadas", e.message);
    }
}

function grabar(){
    try{
        if (!comprobarDatos()) return;
        
        mostrarProcesando();
        
        var sb = new StringBuilder;
        
        sb.Append("grabar@#@");
        sb.Append($I("hdnOpcion").value +"##"); //0
        sb.Append(oTarea.nT +"##"); //1
        sb.Append($I("txtvUDR").value +"##"); //2
        sb.Append($I("txtDesde").value +"##"); //3
        sb.Append($I("txtHasta").value +"##"); //4
        sb.Append($I("cbovModo").value +"##"); //5
        sb.Append(($I("chkFestivos").checked)? "1##":"0##"); //6
        sb.Append(($I("chkFinalizada").checked)? "1##":"0##"); //7
        sb.Append(($I("txtvHoras").value == "")? "0##": $I("txtvHoras").value+"##"); //8
        sb.Append(Utilidades.escape($I("txtvComentario").value) +"##"); //9
        sb.Append(($I("txtvETE").value=="")? "0##":$I("txtvETE").value +"##"); //10
        sb.Append($I("txtvFFE").value +"##"); //11
        sb.Append(Utilidades.escape($I("txtvObservaciones").value)+"##"); //12
        sb.Append((oTarea.Obligaest)? "1##":"0##"); //13
        sb.Append(oTarea.nPSN); //14
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos.", e.message);
    }
}

function comprobarDatos(){
    try{
        if (oTarea.nT == 0){
		    mmoff("War","Debes seleccionar la tarea",210);
		    return false;
        }
	    /* Que las dos fechas existan y la segunda sea posterior */
	    if ($I("txtDesde").value == ""){
		    mmoff("War","Debes introducir la fecha de inicio de la imputación",360);
		    return false;
	    }
	    if ($I("txtHasta").value == ""){
		    mmoff("War","Debes introducir la fecha final de la imputación",340);
		    return false;
	    }
    	
	    var aFecha1 = $I("txtDesde").value.split("/");
	    var aFecha2 = $I("txtHasta").value.split("/");
	    var objFecha1 = new Date(aFecha1[2],eval(aFecha1[1]-1),aFecha1[0]);
	    var objFecha2 = new Date(aFecha2[2],eval(aFecha2[1]-1),aFecha2[0]);
	    if (objFecha2 < objFecha1){
		    mmoff("War","La fecha final de la imputación no puede ser anterior a la fecha de inicio",340);
		    return false;
	    }
    	
	    /* La fecha de final no puede ser posterior a los 2 meses del último cierre */
	    var intDiferencia = objFecha2.getTime() - objUMC.getTime();
	    if (intDiferencia > 5356800000){ //62 días en milisegundos (1 día 86400000).
		    mmoff("War","La fecha final de la imputación debe ser, como máximo, dos meses posterior al último cierre ("+ objUMC.ToShortDateString() +")",340);
		    return false;
	    }
    	
	    /* La fecha de inicio debe ser posterior al último mes cierre */
	    var intDiferencia = objFecha2.getTime() - objUMC.getTime();
	    if (objFecha1.getTime() <= objUMC.getTime()){ 
		    mmoff("War","La fecha de inicio de la imputación debe ser posterior al último mes cerrado ("+ objUMC.ToShortDateString() +")",400);
		    return false;
	    }
    	
	    /* Si se opta por la opción 2, la fecha de inicio debe ser:
	       - Anterior al UDR:
	       - Igual al UDR
	       - Día siguiente laborable al UDR */  
	    if ((($I("rdbImpMas_1").checked) || ($I("rdbImpMas_2").checked)) && (strAuxUltimoDia != "") && (bControlhuecos)){
		    if (objFecha1.getTime() > objDiaSigUDR.getTime()){
			    mmoff("War","La fecha de inicio de imputación debe ser:\n\n- Anterior al último día reportado\n- Igual al último día reportado\n- Día siguiente laborable al último día reportado ",400);
			    return false;
		    }
	    }

	    /* Si se opta por la opción 2, que se introduzcan horas (no superior a 24h) */
	    if ($I("rdbImpMas_2").checked){
		    if ($I("txtvHoras").value == ""){
			    mmoff("War","Introduce las horas a imputar",190);
			    return false;
		    }
		    /* Horas */
		    if (parseFloat(dfn($I("txtvHoras").value)) <= 0){
			    mmoff("War","El número de horas a imputar debe ser superior a 0 horas",370);
			    $I("txtvHoras").focus();
			    $I("txtvHoras").select();
			    return false;
		    }
		    // ya no ?
//		    if (parseFloat(sUNParseNumero(intNuevoValor)) % 0.25 != 0){
//			    alert("El número de horas a imputar debe ser múltiplo de 0,25 horas");
//			    $I("txtvHoras").focus();
//			    $I("txtvHoras").select();
//			    return false;
//		    }
		    if (parseFloat(dfn($I("txtvHoras").value)) > 24){
			    mmoff("War","El número de horas a imputar debe ser inferior a 24 horas",370);
			    $I("txtvHoras").focus();
			    $I("txtvHoras").select();
			    return false;
		    }
    	    
            if (!oTarea.ImpFes && $I("chkFestivos").checked == true){
	            mmoff("War","El proyecto económico seleccionado no permite imputar en festivos.",380);
	            return false;
            }
            //Si el proyecto no permite imputar a jornada no completa y el modo es dos, return false
            if (!oTarea.NOJC){
	            mmoff("War","El proyecto económico seleccionado obliga a imputar jornadas completas, por lo que hay que imputar con las opciones de horas estándar.",400);
	            return false;
            }
	    }

	    if (oTarea.Obligaest){
		    if (($I("txtvETE").value == "")||($I("txtvETE").value == "0,00")){
			    mmoff("War","Introduce el esfuerzo total estimado",280);
			    $I("txtvETE").focus();
			    $I("txtvETE").select();
			    return false;
		    }
		    if ($I("txtvFFE").value == ""){
			    mmoff("War","Introduce la fecha de finalización estimada",300);
			    return false;
		    }
	    }
	    	    
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function actualizarFechas(strAuxUltimoDia){
    try{
        strUltImputac = cadenaAfecha(strAuxUltimoDia);
        objDiaSigUDR = cadenaAfecha(strAuxUltimoDia);

	    objDiaAntUDR = cadenaAfecha(strAuxUltimoDia).add("d", -1);
	    
	    var sw = 0;
	    var sw1 = 0;
	    var bFes = false;
        for (var y = 1; y <= 7; y++){
            var objNuevaFecha = cadenaAfecha(strAuxUltimoDia).add("d", y);
		    bFes = false;
		    for (var indice=0;indice<aFestivos.length;indice++){
			    var strNuevaFecha = objNuevaFecha.ToShortDateString();
			    //Comparo las fechas, porque si lo hago con los .setTime,
			    //inexplicablemente, el 28/03/2005 me da las 01:00:00 en lugar de 00:00:00
			    if (strNuevaFecha == aFestivos[indice]){
				    bFes = true;
				    break;
			    }
		    }
		    //Añadir, y que el día no sea festivo.
            //if ((intDiaSemana == 0) || (intDiaSemana == 6) || bFes){ // el array de festivos incluye no laborables
            if (bFes){
                continue;
            }else{
			    if (sw1 == 0){
				    var intNuevaFecha2 = objNuevaFecha.getTime();
				    sw1 = 1;
			    }
                for (var z=0;z<aFestivos.length;z++){
                    var strFechaAux = cadenaAfecha(aFestivos[z]);
                    if (strFechaAux <= strUltImputac){
                        continue;
                    }
                    /* Al final del año, como todos los festivos son
                    anteriores al último día reportado, nunca se llega hasta
                    aquí, por lo que sw queda a 0 */
                    if (strUltImputac.getTime() != strFechaAux.getTime()){
                        objDiaSigUDR.setTime(objNuevaFecha);
					    sw = 1;
					    break;
                    }
                }
            }
		    if (sw == 1) break;
        }
	    //alert(sw);        
	    if (sw == 0){
		    objNuevaFecha = new Date();
		    objNuevaFecha.setTime(intNuevaFecha2);
		    objDiaSigUDR.setTime(objNuevaFecha);
	    }
    	
	    $I("txtvUDR").value = strAuxUltimoDia;
	    $I("txtDesde").value = objDiaSigUDR.ToShortDateString();
	    $I("txtHasta").value = "";
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar las fechas después de grabar.", e.message);
    }
}

function getTarea(){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/IAP/ImpMasiva/getTareaMasiva/Default.aspx?idUsuario=" + UsuarioActual + "&txtDesde=" + $I("txtDesde").value + "&txtHasta=" + $I("txtHasta").value, self, sSize(650, 670))
            .then(function(ret) {
	            if (ret != null){
                    var aDatos = ret.split("///");
                    with (oTarea){
                        nPSN = aDatos[0];
                        nPT = aDatos[1];
                        nT = aDatos[2];
                        desT = aDatos[3];
                        ImpFes = (aDatos[4]=="1")? true:false;
                        NOJC = (aDatos[5]=="1")? true:false;
                        Obligaest = (aDatos[6]=="1")? true:false;
                    }
                    $I("txtvETE").disabled = false;
                    if (btnCal == "I")
                        $I("txtvFFE").onclick = function (){mc(this);};
                    else{
                        $I("txtvFFE").onmousedown = function (){mc1(this);};
                        //$I("txtvFFE").onfocus = function (){focoFecha(this);};
                        $I("txtvFFE").attachEvent("onfocus", focoFecha);
                    }
                    $I("chkFinalizada").disabled = false;
                    $I("txtvObservaciones").disabled = false;
                    
                    $I("txtIDTarea").value = oTarea.nT.ToString("N", 9, 0);
                    $I("txtDesTarea").value = oTarea.desT;
                    getDatosIAP();
                    comprobarFestivos();
                    activarGrabar();
                }
            });
        window.focus();	                            	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las tareas", e.message);
    }
}

function getDatosIAP(){
    try{
        var js_args = "getTarea@#@"+oTarea.nT;  
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos IAP de la tarea.", e.message);
    }
}
function obtenerTarea(){
    try{
        if ($I("txtIDTarea").value != ""){
            var js_args = "getTarea2@#@" + $I("txtIDTarea").value;  
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al obtener tarea.", e.message);
    }
}
function obtenerTarea2(strDatos){
    try{
        var aDatos = strDatos.split("///");
        with (oTarea){
            nPSN = aDatos[0];
            nPT = aDatos[1];
            nT = aDatos[2];
            desT = aDatos[3];
            ImpFes = (aDatos[4]=="1")? true:false;
            NOJC = (aDatos[5]=="1")? true:false;
            Obligaest = (aDatos[6]=="1")? true:false;
        }
        $I("txtvETE").disabled = false;
        if (btnCal == "I")
            $I("txtvFFE").onclick = function (){mc};
        else{
            $I("txtvFFE").onmousedown = function (){mc1};
            $I("txtvFFE").onfocus = function (){focoFecha};
        }
        $I("chkFinalizada").disabled = false;
        $I("txtvObservaciones").disabled = false;
        
        $I("txtIDTarea").value = oTarea.nT.ToString("N", 9, 0);
        $I("txtDesTarea").value = oTarea.desT;

        $I("chkFacturable").checked = (aDatos[7]=="1")? true:false;
        if (ie)
            $I("lblModo").innerText = aDatos[8];
        else
            $I("lblModo").innerText = aDatos[8];
        $I("txtPriCon").value = aDatos[9];
        $I("txtUltCon").value = aDatos[10];
        $I("txtConHor").value = aDatos[11];
        $I("txtConJor").value = aDatos[12];
        $I("txtPteEst").value = aDatos[13];
        $I("txtAvanEst").value = aDatos[14];
        $I("txtTotPre").value = aDatos[15];
        $I("txtFinPre").value = aDatos[16];
        $I("txtIndicaciones").value = Utilidades.unescape(aDatos[17]);
        $I("txtColectivas").value = Utilidades.unescape(aDatos[18]);
        $I("txtvETE").value = aDatos[19];
        $I("txtvFFE").value = aDatos[20];
        $I("txtvObservaciones").value = Utilidades.unescape(aDatos[21]);
        $I("chkFinalizada").checked = (aDatos[22]=="1")? true:false;
        $I("txtProyecto").value = aDatos[23];
        $I("txtPT").value = aDatos[24];
        $I("txtFase").value = aDatos[25];
        $I("txtActividad").value = aDatos[26];

        comprobarFestivos();
        activarGrabar();
    }
	catch(e){
		mostrarErrorAplicacion("Error al cargar la tarea.", e.message);
    }
}
function quitarTarea() {
    try {
        with (oTarea) {
            nPSN = 0;
            nPT = 0;
            nT = 0;
            desT = "";
            ImpFes = false;
            NOJC = false;
            Obligaest = false;
            ete = 0;
            ffe = "";
            Comentario = "";
        }
        $I("txtvETE").value = "";
        $I("txtvObservaciones").value = "";
        $I("chkFinalizada").checked = false;
        $I("txtIDTarea").value = "";
        $I("txtDesTarea").value = "";
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar la tarea.", e.message);
    }
}
function limpiar(event){
    try{//Si pulso una tecla que no es intro o flecha izda, dcha, arriba o abajo entonces limpio la pantalla
        if(event.keyCode!=13 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40){
            oTarea.nT = 0;
            $I("txtDesTarea").value = "";
            $I("chkFacturable").checked = false;
            if (ie)
                $I("lblModo").innerText = "";
            else
                $I("lblModo").innerText = "";
            $I("txtPriCon").value = "";
            $I("txtUltCon").value = "";
            $I("txtConHor").value = "";
            $I("txtConJor").value = "";
            $I("txtPteEst").value = "";
            $I("txtAvanEst").value = "";
            $I("txtTotPre").value = "";
            $I("txtFinPre").value = "";
            $I("txtIndicaciones").value = "";
            $I("txtColectivas").value = "";
            $I("txtvETE").value = "";
            $I("txtvFFE").value = "";
            $I("txtvObservaciones").value = "";
            $I("chkFinalizada").checked = false;
            $I("txtProyecto").value = "";
            $I("txtPT").value = "";
            $I("txtFase").value = "";
            $I("txtActividad").value = "";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar.", e.message);
    }
}


