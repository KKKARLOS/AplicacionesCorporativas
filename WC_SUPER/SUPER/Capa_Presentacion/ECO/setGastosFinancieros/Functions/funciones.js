var bSalir = false;
var bHayCambios = false;
var aSegMesProy = new Array();
var nIndiceM2 = null;
var nIndiceM1 = null;
var nIndice0 = null;
var nIndiceP1 = null;
var nIndiceP2 = null;
var nIndiceIA = null;
var nPrimerMesInsertado = 0;

function init(){
    try{
        if (!mostrarErrores()) return;
        
        if (bLecturaInsMes){
            setOp($I("btnInsertarMes"), 30);
        }
        if (bLectura){
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
        }else $I("txtGastosFinancieros").readOnly = false;
        
        aSegMesProy = opener.aSegMesProy;
        nIndiceM2 = opener.nIndiceM2;
        nIndiceM1 = opener.nIndiceM1;
        nIndice0 = opener.nIndice0;
        nIndiceP1 = opener.nIndiceP1;
        nIndiceP2 = opener.nIndiceP2;
        nIndiceIA = opener.nIndiceIA;
        
        if (opener.nColumnaCarrusel != nIndice0){
            nIndice0 = opener.nColumnaCarrusel;
            if (nIndice0 > 0) nIndiceM1 = nIndice0 -1;
            else nIndiceM1 = null;
            if (nIndice0 > 1) nIndiceM2 = nIndice0 -2;
            else nIndiceM2 = null;
            if (nIndice0 < aSegMesProy.length-1) nIndiceP1 = nIndice0 + 1;
            else nIndiceP1 = null;
            if (nIndice0 < aSegMesProy.length-2) nIndiceP2 = nIndice0 + 2;
            else nIndiceP2 = null;
        }
        colorearMeses();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function grabarsalir(){
    try{
        if (bProcesando()) return;
        bSalir=true;
        grabar();
    }catch(e){
        mostrarErrorAplicacion("Error al pulsar \"Grabar y salir\"", e.message);
    }
}
function salir() {
    bSaliendo = true;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                salirCerrarVentana();
            }
        });
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
    var returnValue = null;
    if (bHayCambios) {
        opener.sUltCierreEcoNodo = sUltCierreEcoNodo;
        opener.aSegMesProy = aSegMesProy;
        opener.nIndiceM2 = nIndiceM2;
        opener.nIndiceM1 = nIndiceM1;
        opener.nIndice0 = nIndice0;
        opener.nIndiceP1 = nIndiceP1;
        opener.nIndiceP2 = nIndiceP2;
        opener.nIndiceIA = nIndiceIA;

        returnValue = "OK";
    }

    modalDialog.Close(window, returnValue);
}
function activarGrabar() {
    try {
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activarGrabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        bCambios = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al desActivarGrabar", e.message);
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
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar la clase económica '" + aResul[3] + "',\n ya que existen elementos con los que está relacionada.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160); 
                //popupWinespopup_winLoad();
                bHayCambios = true;

                if (bSalir) setTimeout("salir();", 50);
                else{
                    if (bCambiarMes){
                        bCambiarMes = false;
                        setTimeout("cambiarMes('"+ sCambiarMes +"')", 50);
                    }
                    if (bGetDatosGF){
                        bGetDatosGF = false;
                        setTimeout("getDatosAvance()", 20);
                    }
                    if (bInsertarMes){
                        bInsertarMes =  false;
                        setTimeout("insertarmes()", 50);
                    }
                    if (bMonedaImportes) {
                        bMonedaImportes = false;
                        setTimeout("getMonedaImportes()", 50);
                    }
                }

                break;
            case "getDatosGF":
                nPrimerMesInsertado = 0;
                //alert(aResul[2]);
                $I("txtGastosFinancieros").value = aResul[2];
                
                if (aResul[3]=="true") $I("txtGastosFinancieros").readOnly = true;
                else $I("txtGastosFinancieros").readOnly = false;

                if (aResul[3]=="true"){
                    bLectura = true;
                    setOp($I("btnGrabar"), 30);
                    setOp($I("btnGrabarSalir"), 30);
                }else{
                    bLectura = false;
                    setOp($I("btnGrabar"), 100);
                    setOp($I("btnGrabarSalir"), 100);
                }

                $I("lblMonedaImportes").innerText = aResul[4];
                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;
                break;
            case "getMesesProy":
                var aDatos = aResul[2].split("///");
                aSegMesProy.length = 0;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    aSegMesProy[i] = new Array(aValor[0],aValor[1],aValor[2]); //id, anomes, estado
                }
                //sAnoMesActual
                if (nPrimerMesInsertado != 0){
                    for (var i=0; i<aSegMesProy.length; i++){
                        if (nPrimerMesInsertado == aSegMesProy[i][1]){
                            if (i > 1) nIndiceM2 = i-2;
                            else nIndiceM2 = null;
                            if (i > 0) nIndiceM1 = i-1;
                            else nIndiceM1 = null;
                            nIndice0 = i;
                            if (i < aSegMesProy.length-1) nIndiceP1 = i+1;
                            else nIndiceP1 = null;
                            if (i < aSegMesProy.length-2) nIndiceP2 = i+2;
                            else nIndiceP2 = null;
                            
                            break;
                        }
                    }
                }else{
                    var sw = 0;
                    for (var i=0; i<aSegMesProy.length; i++){
                        if (aSegMesProy[i][2] == "A"){//estado abierto
                            
                            if (i > 1) nIndiceM2 = i-2;
                            else nIndiceM2 = null;
                            if (i > 0) nIndiceM1 = i-1;
                            else nIndiceM1 = null;
                            nIndice0 = i;
                            if (i < aSegMesProy.length-1) nIndiceP1 = i+1;
                            else nIndiceP1 = null;
                            if (i < aSegMesProy.length-2) nIndiceP2 = i+2;
                            else nIndiceP2 = null;
                            
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0 && aSegMesProy.length > 0){
                        if (aSegMesProy.length > 2) nIndiceM2 = aSegMesProy.length-3;
                        if (aSegMesProy.length > 1) nIndiceM1 = aSegMesProy.length-2;
                        nIndice0 = aSegMesProy.length-1;
                        nIndiceP1 = null;
                        nIndiceP2 = null;
                    }
                }
                colorearMeses();
                setTimeout("getDatosAvance();", 20);
                setTimeout("opener.getCierre();", 20);
                break;
                
            case "addMesesProy":
                nPrimerMesInsertado = parseInt(aResul[2] ,10);
                setTimeout("getSegMesProy();", 20);
                bHayCambios = true;
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

var nIDTimeGM = 0;
var nCountGM = 0;
function getSegMesProy(){
    try{
        if (nCountGM > 3){
            clearTimeout(nIDTimeGM);
            nCountGM = 0;
            mmoff("Inf", "No se ha podido determinar el proyecto para la obtención de los meses", 400);
            return;
        }
        if (opener.$I("hdnIdProyectoSubNodo").value == ""){
            nCountGM++;
            clearTimeout(nIDTimeGM);
            nIDTimeProy = setTimeout("getSegMesProy()", 100);
            return;
        }
        nCountGM = 0;

        var js_args = "getMesesProy@#@";
            js_args += opener.$I("hdnIdProyectoSubNodo").value;

        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir a buscar los meses", e.message);
    }
}

function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;

        mostrarProcesando();

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        sb.Append(aSegMesProy[nIndice0][0] +"@#@");
        sb.Append(($I("txtGastosFinancieros").value=="")? 0:$I("txtGastosFinancieros").value);

        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
var bGetDatosGF = false;
function getDatosGF(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetDatosGF = true;
                    grabar();
                    return;
                } else {
                    bCambios = false;
                    LLamarGetDatosProf();
                }
            });
        } else LLamarGetDatosProf();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los gastos financieros-1", e.message);
    }
}
function LLamarGetDatosProf() {
    try {
        var js_args = "getDatosGF@#@";
        js_args += aSegMesProy[nIndice0][0] +"@#@";
        js_args += aSegMesProy[nIndice0][2] +"@#@";
        js_args += sEstado + "@#@";
        js_args += sMonedaProyecto + "@#@";
        js_args += sMonedaImportes;
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los gastos financieros-2", e.message);
    }
}

var bCambiarMes = false;
var sCambiarMes = null;
function cambiarMes(sValor) {
    try {
        if (aSegMesProy.length == 0) return;
        switch (sValor) {
            case "P": if (getOp($I("imgPM")) != 100) return; break;
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
            case "U": if (getOp($I("imgUM")) != 100) return; break;
        }


        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCambiarMes = true;
                    sCambiarMes = sValor;
                    grabar();
                    return;
                }
                else {
                    bCambios = false;
                    LLamarcambiarMes(sValor);
                }
            });
        } else LLamarcambiarMes(sValor);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes-1", e.message);
    }
}
function LLamarcambiarMes(sValor) {
    try {
        mostrarProcesando();
        bHayCambios = true;
        switch (sValor){
            case "A":
                if (nIndice0 > 0) nIndice0--;
                else{
                    ocultarProcesando();
                    return;
                }

                if (nIndiceM1 != null && nIndiceM1 > 0){
                    nIndiceM1--;
                }else nIndiceM1 = null;
                if (nIndiceM2 != null && nIndiceM2 > 0){
                    nIndiceM2--;
                }else nIndiceM2 = null;
                
                if (nIndice0 < aSegMesProy.length-1){
                    if (nIndiceP1 == null) nIndiceP1=aSegMesProy.length-1;
                    else nIndiceP1--;
                }else nIndiceP1 = null;
                if (nIndice0 < aSegMesProy.length-2){
                    if (nIndiceP2 == null) nIndiceP2=aSegMesProy.length-1;
                    else nIndiceP2--;
                }else nIndiceP2 = null;
                break; 
                
            case "S":
                if (nIndice0 < aSegMesProy.length-1) nIndice0++;
                else{
                    ocultarProcesando();
                    return;
                }
                
                if (nIndiceM1 < aSegMesProy.length-1){
                    if (nIndiceM1==null) nIndiceM1 = 0;
                    else nIndiceM1++;
                }
                if (nIndiceM2 < aSegMesProy.length-2){
                    if (nIndiceM1==0) nIndiceM2 = null;
                    else if (nIndiceM2==null) nIndiceM2 = 0;
                    else nIndiceM2++;
                }
                
                if (nIndiceP1 != null && nIndiceP1 < aSegMesProy.length-1){
                    nIndiceP1++;
                }else nIndiceP1 = null;
                if (nIndiceP2 != null && nIndiceP2 < aSegMesProy.length-1){
                    nIndiceP2++;
                }else nIndiceP2 = null;
                break;
                
            case "P":
                nIndiceM2 = null;
                nIndiceM1 = null;
                nIndice0 = 0;
                if (aSegMesProy.length > 0) nIndiceP1 = 1;
                else nIndiceP1 = null;
                if (aSegMesProy.length > 1) nIndiceP2 = 2;
                else nIndiceP2 = null;
                break;
                
            case "U":
                if (aSegMesProy.length > 2) nIndiceM2 = aSegMesProy.length - 3;
                else nIndiceM2 = null;
                if (aSegMesProy.length > 1) nIndiceM1 = aSegMesProy.length - 2;
                else nIndiceM1 = null;
            
                nIndice0 = aSegMesProy.length-1;
                nIndiceP1 = null;
                nIndiceP2 = null;
                break;
        }
        
        colorearMeses();
        getDatosGF();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar el mes-2", e.message);
    }
}

function colorearMeses(){
    try{
        $I("txtMesBase").value = opener.AnoMesToMesAnoDescLong(aSegMesProy[nIndice0][1]);
        if (nIndice0==null) $I("txtMesBase").style.backgroundColor = "#ffffff";
        else if (aSegMesProy[nIndice0][2] == "A") $I("txtMesBase").style.backgroundColor = "#00ff00";
        else $I("txtMesBase").style.backgroundColor = "#F58D8D";
        
        if (nIndice0 == 0 && aSegMesProy.length == 1){
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        }else if (nIndice0 == 0){
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        }else if (nIndice0 == aSegMesProy.length-1){
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        }else{
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el estado de los meses", e.message);
    }
}

var bInsertarMes = false;
function insertarmes() {
    try {
        if (getOp($I("btnInsertarMes")) != 100) return;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bInsertarMes = true;
                    grabar();
                    return;
                }
                else {
                    bCambios = false;
                    LLamarInsertarmes();
                }
            });
        } else LLamarInsertarmes();
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-1", e.message);
    }
}

function LLamarInsertarmes() {
    try {
        bInsertarMes = false;

        var nMesACrear = opener.getPMACrear();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getPeriodo.aspx?sDesde=" + nMesACrear + "&sHasta=" + nMesACrear;
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                var js_args = "addMesesProy@#@";
	                js_args += opener.$I("hdnIdProyectoSubNodo").value + "@#@";
	                js_args += aDatos[0] + "@#@";
	                js_args += aDatos[1];

	                RealizarCallBack(js_args, "");
	            }
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-2", e.message);
    }
}

var bMonedaImportes = false;
function getMonedaImportes() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bMonedaImportes = true;
                    grabar();
                    return;
                } else {
                    bCambios = false;
                    LLamarGetMonedaImportes();
                }
            });
        } else LLamarGetMonedaImportes();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-1.", e.message);
    }
}
function LLamarGetMonedaImportes() {
    try {
        bMonedaImportes = false;

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDP";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function (ret) {
	            if (ret != null) {
	                //alert(ret);
	                var aDatos = ret.split("@#@");
	                sMonedaImportes = (aDatos[0] == "") ? sMonedaProyecto : aDatos[0];;
	                $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];
	                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;
	                bHayCambios = true;
	                getDatosGF();
	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-2.", e.message);
    }
}