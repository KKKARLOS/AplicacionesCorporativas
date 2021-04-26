//var aDiaSem = new Array("L", "M", "X", "J", "V", "S", "D");
//var bSemLab = new Array(false, false, false, false, false, false, false);
var bSemLab = new Array(true, true, true, true, true, true, true);
var bSalir = false;
function init(){
    try{
        desActivarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
var objSeleccionado = "";
function selFecha(IDCelda){
    try{
        if ((objSeleccionado != "")&&(objSeleccionado != IDCelda)) $I(objSeleccionado).className = "TDCal";
        objSeleccionado = IDCelda;
        if ($I(IDCelda).className != "TDCalSel") $I(IDCelda).className = "TDCalSel";
        else if (bSemLab[$I(IDCelda).cellIndex]) $I(IDCelda).className = "TDCal";
             else $I(IDCelda).className = "textoCalFinde";
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el día indicado", e.message);
	}
}


function grabar(){
    try{
        if (sProvOld == "")
        {
            mmoff("War", "Debes seleccionar alguna provincia", 280);
            return false;
        }
        mostrarProcesando();
        var strDias = "";
        var aSpan = document.getElementsByTagName("SPAN");
        for (j=0; j<aSpan.length; j++){
            if (aSpan[j].id.indexOf("ctl00_CPHC_fec_") != -1){
                var id = aSpan[j].id;
                var strFecha = id.substring(15,17) +"/"+ id.substring(17,19) +"/"+ id.substring(19,23);
                var dFechaAux = new Date(id.substring(19,23),eval(id.substring(17,19)-1),id.substring(15,17));

                if (aSpan[j].parentNode.children[0].className != "textoCalFes") var nFestivo = 0;
                else var nFestivo = 1;
                if (nFestivo==1) strDias += strFecha + "##";
            }
        }

        var js_args = "grabar@#@";
        //js_args += $I("cboProvincia").value + "@#@";
        js_args += sProvOld + "@#@";
        js_args += strDias + "@#@";
        js_args += $I("txtAnno").value;
        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos", e.message);
        return false;
    }
}

function establecerFestivo(){
    try{
        if (objSeleccionado != ""){      
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
    	
	    strID = "ctl00_CPHC_hor_" + strDia + strMes + strAnno;
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
        var intDifDias = DiffDiasFechas("01/01/" + $I("txtAnno").value, "31/12/" + $I("txtAnno").value);
		var strDesde = "01/01/" + $I("txtAnno").value;
        var dDesde = new Date(strDesde.substring(6,10),parseInt(strDesde.substring(3,5),10)-1,strDesde.substring(0,2)).add("d",-1);
		var objNuevaFecha;
		for (y = 0; y <= intDifDias; y++){
            objNuevaFecha = dDesde.add("d", 1);
            var objSpan = obtenerObjetoSpanDia(objNuevaFecha);
            if (objSpan.className != "textoCalFinde") objSpan.className = "textoCal";
		}
		if ($I("cboProvincia").value=="") AccionBotonera("festivo", "D");
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar los horarios del calendario", e.message);
	}
    ocultarProcesando();
}
function cargarFestivos(iCodProvincia) {
    try {
        if (bCambios) {
            jqConfirm("", "Se han modificado los datos. ¿Deseas grabarlos?", "", "", "war", 360).then(function (answer) {
                if (answer) grabar();
                bCambios = false;
                LLamarCargarFestivos(iCodProvincia);
            });
        }
        else LLamarCargarFestivos(iCodProvincia);
    }catch(e){
        mostrarErrorAplicacion("Error al cargar el calendario festivo de una provincia", e.message);
    }
}
function LLamarCargarFestivos(iCodProvincia) {
    try {
        if (iCodProvincia == "") {
            limpiar();
            return;
        }
        mostrarProcesando();
        var js_args = "cargarFestivos@#@";
        js_args += iCodProvincia + "@#@";
        js_args += $I("txtAnno").value;
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al cargar el calendario festivo de una provincia", e.message);
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
                if ($I("cboProvincia").value=="") limpiar();
                break;
            case "cargarFestivos":
                limpiar();
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
                        if (objSpan1.id.substring(0,19) == 'ctl00_CPHC_fec_' + aDatos[i])
                        {
                            objSpan1.className = "textoCalFes";
                            continue;
                        }
                    }
                }
                sProvOld = $I("cboProvincia").value;
                AccionBotonera("festivo", "H");
                break;
            case "provinciasPais":
                var aDatos = aResul[2].split("///");
                var j = 1;
                $I("cboProvincia").length = 0;

                var opcion = new Option("", "");
                $I("cboProvincia").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboProvincia").options[j] = opcion;
                    j++;
                }
                //sPaisOld = $I("cboPais").value;
                break;
        }
        ocultarProcesando();
    }
}
function obtenerProvinciasPais(sPais) {
    try {
        if (sPais == "") {
            $I("cboProvincia").length = 1;
            return;
        }
        if (bCambios) {
            jqConfirm("", "Se han modificado los datos. ¿Deseas grabarlos?", "", "", "war", 360).then(function (answer) {
                if (answer) grabar();
                bCambios = false;
                LLamarProvinciasPais(sPais);
            });
        }
        else LLamarProvinciasPais(sPais);
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenerProvinciasPais ", e.message);
    }
}
function LLamarProvinciasPais(sPais) {
    try {
        limpiar();
        sProvOld = "";
        var js_args = "provinciasPais@#@";
        js_args += sPais;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función LLamarProvinciasPais ", e.message);
    }
}
function comprobarCambios(sender, args) {
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