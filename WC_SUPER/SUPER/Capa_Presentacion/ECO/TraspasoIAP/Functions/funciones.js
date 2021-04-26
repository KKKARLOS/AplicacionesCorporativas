var aPSNtemp = new Array();
var nIndiceProy = 0;
var bgrabarproy = false;
var bgrabarproy2 = false;
var btraspgrabproy = false;
var bOcultar = true;
var nAnoMesActual, nAnoMesInicial;

function init(){
    try {
        mostrarProcesando();
        setOp($I("MesSig"), 30);
        //De las lista de proyectos creo un array solo con los que hay que tratar
        for (var i = 0; i < aPSN.length; i++) {
            //Si el proyecto tiene consumos o NO está marcado el check que muestra únicamente proyectos con consumos reportados en IAP
            if (aPSN[i].tiene_consumos == 1 || !$I("chkRPCCR").checked)
                aPSNtemp[aPSNtemp.length] = aPSN[i];
        }
        flActivarBtnsPSN(aPSNtemp.length, nIndiceProy);
        mostrarDatos(0);
        
        setOpcionGusano("1,0,2,3,4,5");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
//function goToResumenEco() {
//    try {
//        document.forms["aspnetForm"].method = "POST";
//        document.forms["aspnetForm"].action = "../ResumenEcoProy/Default.aspx";
//        document.forms["aspnetForm"].submit();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al ir a la pantalla de resumen económico.", e.message);
//    }
//}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        bOcultar = true;
        switch (aResul[0]){
            case "getPSN":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                setTotales();
                ocultarProcesando();
                break;

            case "grabar":
                aPSNtemp[nIndiceProy].traspasoIAP = 1;
                //if (!bgrabarproy) mmoff("Suc", "Grabación correcta.", 160);
                desActivarGrabar();

                if (NumOpcion == 0) {
                    if (bgrabarproy) {
                        if (nIndiceProy == aPSNtemp.length - 1) {
                            ocultarProcesando();
                            mmoff("Inf", "Último proyecto de la lista.", 250);
                        } else {
                            if (nIndiceProy < aPSNtemp.length) {
                                bOcultar = false;
                                setTimeout("mostrarDatos(3);", 20);

                                bgrabarproy = false;
                                if (btraspgrabproy)
                                    btraspgrabproy = false;
                            }
                        }
                    }
                    else {
                        if (bgrabarproy2) {
                            bgrabarproy2 = false;
                            setTimeout("LLamadaBuscar();", 20);
                        }
                        else
                            mmoff("Suc", "Grabación correcta.", 160);
                    }
                }
                else {
                    setTimeout("mostrarDatos(" + NumOpcion + ");", 20);
                    bgrabarproy = false;
                    if (btraspgrabproy)
                        btraspgrabproy = false;
                }
                break;        
                        
            case "traspglobal":
                for (var i=0; i < aPSN.length; i++){
                    aPSN[i].traspasoIAP = 1;
                }
                desActivarGrabar();
                setTimeout("obtenerDatosPSN();", 20);
                mmoff("Suc", "Trasaso global correcto. Todos tus proyectos han sido traspasados.", 500);
                break;
                
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.",250);
                break;
            case "getPreferencia":
                $I("chkSobreescribir").checked = (aResul[2]=="1")? true:false;
                $I("chkRPCCR").checked = (aResul[3]=="1")? true:false;
                $I("chkProfCon").checked = (aResul[4]=="1")? true:false;
                
                aPSNtemp.length = 0;
                for (var i=0; i < aPSN.length; i++){
                    if (aPSN[i].tiene_consumos == 1 || !$I("chkRPCCR").checked) aPSNtemp[aPSNtemp.length]= aPSN[i];
                }
                flActivarBtnsPSN(aPSNtemp.length, 0);
                setTimeout("mostrarDatos(0);", 20);
                
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultar) ocultarProcesando();
    }
}
function getPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    if (!grabar()) return;
                }
                bCambios = false;
                desActivarGrabar();
                LLamadagetPE();
            });
        }
        else LLamadagetPE();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}
function LLamadagetPE() {
    try {
        mostrarProcesando();

        //var ret = window.showModalDialog("getProyTrasp.aspx", self, sSize(970, 650));
        var strEnlace = strServer + "Capa_Presentacion/ECO/TraspasoIAP/getProyTrasp.aspx";
        modalDialog.Show(strEnlace, self, sSize(970, 650))
	        .then(function (ret) {
	            if (ret != null) {
	                nIndiceProy = ret;
	                flActivarBtnsPSN(aPSNtemp.length, ret);
	                //mostrarDatos(ret);
	                obtenerDatosPSN();
	            }
	        });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}

var NumOpcion = 0;
function mostrarDatos(nOpcion){
    try{
        if (aPSNtemp.length == 0) return;
        switch (nOpcion) {
            case 0:
                $I("txtMes").value = AnoMesToMesAnoDescLong(aPSNtemp[nIndiceProy].annomes);
                nAnoMesActual = aPSNtemp[nIndiceProy].annomes;
                nAnoMesInicial = nAnoMesActual;
                break;
            case 1: if (getOp($I("btnPriRegOff")) != 100) return; break;
            case 2: if (getOp($I("btnAntRegOff")) != 100) return; break;
            case 3: if (getOp($I("btnSigRegOff")) != 100) return; break;
            case 4: if (getOp($I("btnUltRegOff")) != 100) return; break;
        }

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    NumOpcion = nOpcion;
                    grabarproy();
                    return;
                }
                else
                    desActivarGrabar();
                bCambios = false;
                LLamadaMostrarDatos(nOpcion);
            });
        }
        else LLamadaMostrarDatos(nOpcion);
    }catch(e){
        mostrarErrorAplicacion("Error al mostrar cambiar de proyecto-1.", e.message);
    }
}
function LLamadaMostrarDatos(nOpcion) {
    try {
        NumOpcion = 0;
        switch (nOpcion){
            case 1: nIndiceProy = 0; break;
            case 2: if (nIndiceProy > 0) nIndiceProy--; break;
            case 3: if (nIndiceProy < aPSNtemp.length-1) nIndiceProy++; break;
            case 4: nIndiceProy = aPSNtemp.length-1; break; 
        }
        if (nAnoMesInicial == nAnoMesActual)
            obtenerDatosPSN();
        else
            buscar();
        
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar cambiar de proyecto-2.", e.message);
    }
}

function obtenerDatosPSN(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        $I("txtTotHR").value = "0,00";
        $I("txtTotJR").value = "0,00";
        $I("txtTotJA").value = "0,00";
        $I("txtTotJE").value = "0,00";
        //alert(aPSNtemp.length);
        if (aPSNtemp.length == 0){
            mmoff("Inf","No hay proyectos que traspasar", 200);
            return;
        }
	    //$I("txtMes").value = AnoMesToMesAnoDescLong(aPSNtemp[nIndiceProy].annomes);//aMeses[parseInt(aPSNtemp[nIndiceProy].annomes.toString().substring(4, 6), 10)-1]+" "+aPSNtemp[nIndiceProy].annomes.toString().substring(0, 4);
	    switch (aPSNtemp[nIndiceProy].estadomes){
	        case "C":
	            $I("txtMes").style.backgroundColor = "#F45C5C";
	            $I("imgCopImp").style.visibility = "hidden";
	            if (bEs_superadministrador) {
	                AccionBotonera("traspgrabproy", "H");
	                AccionBotonera("traspglobal", "H");
	            }
	            else {
	                AccionBotonera("traspgrabproy", "D");
	                AccionBotonera("traspglobal", "D");
	            }
	            break;
	        case "A":
	            $I("txtMes").style.backgroundColor = "#00ff00";
	            $I("imgCopImp").style.visibility = "visible";
	            AccionBotonera("traspgrabproy", "H");
	            AccionBotonera("traspglobal", "H");
	            break;
	        default:
	            $I("txtMes").style.backgroundColor = "#ffffff";
	            $I("imgCopImp").style.visibility = "visible";
	            AccionBotonera("traspgrabproy", "H");
	            AccionBotonera("traspglobal", "H");
	            break;
	    }
	    
	    $I("txtNumPE").value = aPSNtemp[nIndiceProy].idProyecto.ToString("N", 9, 0);
	    $I("txtDesPE").value = Utilidades.unescape(aPSNtemp[nIndiceProy].denominacion);
	    
	    if (aPSNtemp[nIndiceProy].modelocoste == "J"){
	        $I("imgCopImp").style.width = "100px";
	        $I("imgCopImp").style.marginLeft = "580px";
	        $I("imgCopImp").title = "Traspasa a la columna de jornadas económicas la información de la columna de jornadas adaptadas";
	        //$I("lbjJA").style.visibility = "visible";
	        $I("lbjJA").innerHTML = "JA";
	        $I("lbjJA").title = "Jornadas adaptadas";
	        //$I("txtTotJA").style.visibility = "visible";
	        $I("lblHJE").innerHTML = "JE";
	        $I("lblHJE").title = "Jornadas económicas";
	    }else{
	        $I("imgCopImp").style.width = "100px"; //"240px";
	        $I("imgCopImp").style.marginLeft = "580px"; //"450px";
	        //$I("imgCopImp").title = "Traspasa a la columna de horas económicas la información de la columna de horas reportadas";
	        $I("imgCopImp").title = "Traspasa a la columna de horas económicas la información de la columna de horas adaptadas";
	        //$I("lbjJA").style.visibility = "hidden";
	        $I("lbjJA").innerHTML = "HA";
	        $I("lbjJA").title = "Horas adaptadas";
	        //$I("txtTotJA").style.visibility = "hidden";
	        $I("lblHJE").innerHTML = "HE";
	        $I("lblHJE").title = "Horas económicas";
        }
	    $I("imgCopImp").style.display = "block";
	    
        //Botones de navegación sobre proyectos económicos
        flActivarBtnsPSN(aPSNtemp.length,nIndiceProy);
	    
        var js_args = "getPSN@#@";
        js_args += aPSNtemp[nIndiceProy].idPSN +"@#@";
        js_args += aPSNtemp[nIndiceProy].annomes +"@#@";
        js_args += aPSNtemp[nIndiceProy].modelocoste +"@#@";
        js_args += ($I("chkProfCon").checked==true)? "1@#@" : "0@#@";
        js_args += aPSNtemp[nIndiceProy].estadomes + "@#@";
        js_args += "S";
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos del proyecto.", e.message);
    }
}

function flActivarBtnsPSN(iNumPSN,iIndAct){
    //Establece la visibilidad de los botones de navegación de Proyectossubnodos 
    try{
	    if (iNumPSN>1){
            if (iIndAct==0){
                //$I("btnPriRegOff").filters.alpha.opacity = 30;
                setOp($I("btnPriRegOff"), 30);
                setOp($I("btnAntRegOff"), 30);
                setOp($I("btnSigRegOff"), 100);
                setOp($I("btnUltRegOff"), 100);
            }
            else{
                if (iIndAct==iNumPSN-1){
                    setOp($I("btnPriRegOff"), 100);
                    setOp($I("btnAntRegOff"), 100);
                    setOp($I("btnSigRegOff"), 30);
                    setOp($I("btnUltRegOff"), 30);
                }
                else{
                    setOp($I("btnPriRegOff"), 100);
                    setOp($I("btnAntRegOff"), 100);
                    setOp($I("btnSigRegOff"), 100);
                    setOp($I("btnUltRegOff"), 100);
                }
            }
	    }
	    else{
            $I("lblProy").className = "texto";
            $I("lblProy").onclick = null;
            setOp($I("btnPriRegOff"), 30);
            setOp($I("btnAntRegOff"), 30);
            setOp($I("btnSigRegOff"), 30);
            setOp($I("btnUltRegOff"), 30);
	    }
	}
	catch(e){
		mostrarErrorAplicacion("Error al establecer la visibilidad de los botones de navegación de Proyectos económicos.", e.message);
	}
}

function setTotales(){
    try{
        var nTotHR = 0, nTotJR = 0, nTotJA = 0, nTotJE = 0;
		var oFila;
		var tblDatos = $I("tblDatos");
		
        for (var i=tblDatos.rows.length-1; i>=0; i--){
            //Probar a Crear y pasar oFila en lugar de tblDatos.rows[i], para comprobar rendimiento.
            //Efectivamente, es más rápido.
			oFila = tblDatos.rows[i];
			nTotHR += parseFloat(dfn(getCelda(oFila, 2)));
			nTotJR += parseFloat(dfn(getCelda(oFila, 3)));
			nTotJA += parseFloat(dfn(getCelda(oFila, 4)));
			nTotJE += parseFloat(dfn(getCelda(oFila, 5)));
        }

       //Totales
		$I("txtTotHR").value = nTotHR.ToString("N");
		$I("txtTotJR").value = nTotJR.ToString("N");
		$I("txtTotJA").value = nTotJA.ToString("N");
		$I("txtTotJE").value = nTotJE.ToString("N");

	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales.");
    }
}


function mostrarProyectos(){
    try{
        aPSNtemp.length = 0;
        nIndiceProy = 0;
        for (var i=0; i < aPSN.length; i++){
            //mostrarDatosPSN(aPSN[i].idPSN);
            if ($I("chkRPCCR").checked == false || ($I("chkRPCCR").checked == true && aPSN[i].tiene_consumos == 1)) 
                aPSNtemp[aPSNtemp.length]= aPSN[i];
        }

        flActivarBtnsPSN(aPSNtemp.length,nIndiceProy);
        mostrarDatos(0);
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los proyectos");
    }
}
var bMostrarMsg = false;
function copiarImportes(nOpcion){
    try {
        var iCaso = 0;
		var oFila;
		var tblDatos = $I("tblDatos");
		bMostrarMsg = false;
		if ($I("chkSobreescribir").checked == false) {
            for (var i=tblDatos.rows.length-1; i>=0; i--){
			    oFila = tblDatos.rows[i];
		        if (aPSNtemp[nIndiceProy].modelocoste == "J"){
		            if (parseFloat(dfn(getCelda(oFila, 4))) != 0 && parseFloat(dfn(getCelda(oFila, 5))) != 0 &&
                        parseFloat(dfn(getCelda(oFila, 4))) != parseFloat(dfn(getCelda(oFila, 5)))) {
		                iCaso = 1;
		                //if (confirm("Existen valores diferentes de cero en la columna de unidades económicas (UE), que van a ser modificados mediante esta acción por otros valores diferentes de cero de la columna de jornadas adaptadas (JA).\n\n¿Deseas continuar?")){
		                //    break;
		                //}else return false;
		            }
		            if (parseFloat(dfn(getCelda(oFila, 4))) == 0 && parseFloat(dfn(getCelda(oFila, 5))) != 0){
		                bMostrarMsg = true;
		            }
		        }else{
		            if (parseFloat(dfn(getCelda(oFila, 4))) != 0 && parseFloat(dfn(getCelda(oFila, 5))) != 0 &&
                        parseFloat(dfn(getCelda(oFila, 4))) != parseFloat(dfn(getCelda(oFila, 5)))) {
		                iCaso = 2;
		                //if (confirm("Existen valores diferentes de cero en la columna de unidades económicas (UE), que van a ser modificados mediante esta acción por otros valores diferentes de cero de la columna de horas adaptadas (HA).\n\n¿Deseas continuar?")) {
		                //    break;
		                //} else return false;
		            }
		            if (parseFloat(dfn(getCelda(oFila, 4))) == 0 && parseFloat(dfn(getCelda(oFila, 5))) != 0) {
		                bMostrarMsg = true;
		            }
		        }
            }
		} 
		if (bMostrarMsg){

		    var strMsg = "";
		    if (iCaso == 1)  strMsg += "Existen valores diferentes de cero en la columna de unidades económicas (UE), que van a ser modificados mediante esta acción por otros valores diferentes de cero de la columna de jornadas adaptadas (JA).<br><br>";
		    if (iCaso == 2)  strMsg += "Existen valores diferentes de cero en la columna de unidades económicas (UE), que van a ser modificados mediante esta acción por otros valores diferentes de cero de la columna de horas adaptadas (HA).<br><br>";
            
		    if (nOpcion == 1)
		    {
		        strMsg += "Para poner a cero valores de unidades económicas (UE) mediante esta acción, es necesario forzar sobreescritura.";
		        mmoff("Inf", strMsg ,400);
		    }
		    else if (nOpcion == 2){
		        strMsg += "Para poner a cero valores de unidades económicas (UE) mediante esta acción, es necesario forzar sobreescritura.<br><br>¿Deseas continuar?"
		        jqConfirm("", strMsg, "", "", "war", 450).then(function (answer) {
		            if (answer) return LLamarcopiarImportes();
		            else return false;
		        });
		    }
		}
		else return (LLamarcopiarImportes());
    }catch(e){
        mostrarErrorAplicacion("Error al copiar los importes.");
    }
}
function LLamarcopiarImportes() {
	try {
		var sw=0;
		for (var i=tblDatos.rows.length-1; i>=0; i--){
		    //Probar a Crear y pasar oFila en lugar de tblDatos.rows[i], para comprobar rendimiento.
		    //Efectivamente, es más rápido.
		    oFila = tblDatos.rows[i];
		    if (aPSNtemp[nIndiceProy].modelocoste == "J"){
		        if ($I("chkSobreescribir").checked == false && parseFloat(dfn(getCelda(oFila, 4))) == 0 && parseFloat(dfn(getCelda(oFila, 5))) != 0) continue;
		        setCelda(oFila, 5, getCelda(oFila, 4));
		        sw=1;
		    }else{
		        if ($I("chkSobreescribir").checked == false && parseFloat(dfn(getCelda(oFila, 2))) == 0 && parseFloat(dfn(getCelda(oFila, 5))) != 0) continue;
		        //setCelda(oFila, 5, getCelda(oFila, 2));
		        setCelda(oFila, 5, getCelda(oFila, 4));
		        sw = 1;
		    }
		}
		if (sw==1) activarGrabar();
		setTotales();
		return true;
    }catch(e){
        mostrarErrorAplicacion("Error al copiar los importes.");
    }
}
function activarGrabar(){
    try {
        if (nAnoMesActual == nAnoMesInicial) {
            AccionBotonera("Grabar", "H");
            AccionBotonera("GrabarProy", "H");
            bCambios = true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón 'Grabar'", e.message);
	}
}

function desActivarGrabar(){
    try{
	    AccionBotonera("Grabar", "D");
	    AccionBotonera("GrabarProy", "D");
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón 'Grabar'", e.message);
	}
}

function grabar(){
    try{
        var sb = new StringBuilder;
        var oFila;
        var nHayDatos = "0";
        var tblDatos = $I("tblDatos");
        sb.Append("grabar@#@");
        sb.Append(aPSNtemp[nIndiceProy].idPSN +"@#@");
        sb.Append(aPSNtemp[nIndiceProy].annomes +"@#@");
        
        for (var i=0; i<tblDatos.rows.length;i++){
            oFila = tblDatos.rows[i];
            if (parseFloat(dfn(getCelda(oFila, 5))) == 0) continue;
            sb.Append(oFila.id +"//"); //idUsuario
            sb.Append(oFila.getAttribute("costecon") +"//"); 
            sb.Append(oFila.getAttribute("costerep") +"//"); 
            sb.Append(oFila.getAttribute("nodo_usuario") +"//");
            sb.Append(oFila.getAttribute("empresa_nodo") + "//"); 
            sb.Append(getCelda(oFila, 5) +"##"); 
            nHayDatos = "1";
        }

        sb.Append("@#@"+nHayDatos);
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");  
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
	}
}

function grabarproy() {
    try {
        //alert("Graba y calcula el siguiente proyecto");
        bgrabarproy = true;
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar y calcular el siguiente proyecto.", e.message);
    }
}
function grabarproy2() {
    try {
        bgrabarproy2 = true;
        grabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}

function traspgrabproy(){
    try{
	    //alert("Traspasa los consumos, graba y calcula el siguiente proyecto");
	    btraspgrabproy = true;
	    var bTermina = copiarImportes(2);
	    if (bTermina) grabarproy();
	}catch(e){
		mostrarErrorAplicacion("Error al traspasar los consumos, grabar y calcular el siguiente proyecto.", e.message);
	}
}

function traspglobal(){
    try{
	    //alert("Traspasa y graba los consumos de todos los proyetos bajo su responsabilidad");
	    var strMsg1 = "La opción de forzar sobreescritura está activada. Si continuas, se eliminarán las Unidades Económicas que estuvieran ya registradas, traspasando los consumos que se hayan identificado en IAP con los ajustes de adaptación estándares.";
	    var strMsg2 = "La opción de forzar sobreescritura está desactivada. Si continuas, no se traspasarán aquellos consumos para los profesionales que ya tuvieran alguna Unidad Económica registrada."; 
	    var strMsg = ($I("chkSobreescribir").checked==true)? strMsg1:strMsg2;
	        
	    jqConfirm("", "¡¡ Atención !!<br><br>" + strMsg + "<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                $I("chkProfCon").checked = true;
                $I("chkRPCCR").checked = true;

                mostrarProcesando();

                var js_args = "traspglobal@#@";
                js_args += ($I("chkSobreescribir").checked == true) ? "1" : "0";
                RealizarCallBack(js_args, "");
            }
        });
	}catch(e){
		mostrarErrorAplicacion("Error al traspasar y grabar los consumos de todos los proyectos bajo su responsabilidad.", e.message);
	}
}

function setPreferencia(){
    try{
        mostrarProcesando();
        
        var js_args = "setPreferencia@#@";
        js_args += ($I("chkSobreescribir").checked)? "1@#@":"0@#@";
        js_args += ($I("chkRPCCR").checked)? "1@#@":"0@#@";
        js_args += ($I("chkProfCon").checked)? "1@#@":"0@#@";
       
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function getCatalogoPreferencias(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog("../../getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
	        .then(function(ret) {
	            if (ret != null){
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
	            }else ocultarProcesando();
	        });                     
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        $I("txtTotHR").innerText = "0,00";
        $I("txtTotJR").innerText = "0,00";
        $I("txtTotJA").innerText = "0,00";
        $I("txtTotJE").innerText = "0,00";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}
function cambiarMes(nMes) {
    try {
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        if (nAnoMesActual == nAnoMesInicial) {
            //$("MesSig").style.visibility = "hidden";
            setOp($I("MesSig"), 30);
        }
        else {
            
            if (nAnoMesActual > nAnoMesInicial) {
                mmoff("War", "No puedes consultar meses posteriores a " + AnoMesToMesAnoDescLong(nAnoMesInicial), 380, 3000);
                nAnoMesActual = nAnoMesInicial;
                return;
            }
            else {
                //$("MesSig").style.visibility = "visible";
                setOp($I("MesSig"), 100);
            }
        }
        $I("txtMes").value = AnoMesToMesAnoDescLong(nAnoMesActual);

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    grabarproy2();
                    return;
                }
                else
                    desActivarGrabar();
                bCambios = false;
                LLamadaBuscar();
            });
        }
        else LLamadaBuscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar de mes", e.message);
    }
}
function LLamadaBuscar() {
    try {
        if (nAnoMesInicial == nAnoMesActual)
            obtenerDatosPSN();
        else
            buscar();

    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar cambiar de proyecto-2.", e.message);
    }
}

function buscar() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
        $I("txtTotHR").value = "0,00";
        $I("txtTotJR").value = "0,00";
        $I("txtTotJA").value = "0,00";
        $I("txtTotJE").value = "0,00";
        //alert(aPSNtemp.length);
        if (aPSNtemp.length == 0) {
            mmoff("Inf", "No hay proyectos", 150);
            return;
        }
        if (nAnoMesInicial == nAnoMesActual) {
            switch (aPSNtemp[nIndiceProy].estadomes) {
                case "C":
                    $I("txtMes").style.backgroundColor = "#F45C5C";
                    $I("imgCopImp").style.visibility = "hidden";
                    if (!bEs_superadministrador)
                        AccionBotonera("traspgrabproy", "D");
                    break;
                case "A":
                    $I("txtMes").style.backgroundColor = "#00ff00";
                    $I("imgCopImp").style.visibility = "visible";
                    AccionBotonera("traspgrabproy", "H");
                    break;
                default:
                    $I("txtMes").style.backgroundColor = "#ffffff";
                    $I("imgCopImp").style.visibility = "visible";
                    AccionBotonera("traspgrabproy", "H");
                    break;
            }
        }
        else {
            $I("txtMes").style.backgroundColor = "#F45C5C";
            $I("imgCopImp").style.visibility = "hidden"; 
            AccionBotonera("traspgrabproy", "D");
            AccionBotonera("traspglobal", "D");
        }
        $I("txtNumPE").value = aPSNtemp[nIndiceProy].idProyecto.ToString("N", 9, 0);
        $I("txtDesPE").value = Utilidades.unescape(aPSNtemp[nIndiceProy].denominacion);

        if (aPSNtemp[nIndiceProy].modelocoste == "J") {
            $I("lbjJA").innerHTML = "JA";
            $I("lbjJA").title = "Jornadas adaptadas";
            $I("lblHJE").innerHTML = "JE";
            $I("lblHJE").title = "Jornadas económicas";
        } else {
            $I("lbjJA").innerHTML = "HA";
            $I("lbjJA").title = "Horas adaptadas";
            $I("lblHJE").innerHTML = "HE";
            $I("lblHJE").title = "Horas económicas";
        }

        //Botones de navegación sobre proyectos económicos
        flActivarBtnsPSN(aPSNtemp.length, nIndiceProy);

        var js_args = "getPSN@#@"; 
        js_args += aPSNtemp[nIndiceProy].idPSN + "@#@";
        //js_args += aPSNtemp[nIndiceProy].annomes + "@#@";
        js_args += nAnoMesActual + "@#@";
        js_args += aPSNtemp[nIndiceProy].modelocoste + "@#@";
        js_args += ($I("chkProfCon").checked == true) ? "1@#@" : "0@#@";
        js_args += "C@#@";
        js_args += "N";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos del proyecto.", e.message);
    }
}
