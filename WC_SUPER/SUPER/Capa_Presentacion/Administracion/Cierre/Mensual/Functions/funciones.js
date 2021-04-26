var aF;
function init(){
    try{
        nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
        $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMesActual);

        $I("txtMesBaseECO").value = AnoMesToMesAnoDescLong(nAnoMesActualECO);
        $I("txtMesBaseIAP").value = AnoMesToMesAnoDescLong(nAnoMesActualIAP);
        
        ToolTipBotonera("cerrarmes", "Cierre mensual de nodos para IAP");
        LiteralBotonera("cerrarmes", " Cierre IAP");
        ToolTipBotonera("cerrar", "Cierre mensual de nodos para gestión económica");
        LiteralBotonera("cerrar", " Cierre ECO");
        
        ToolTipBotonera("icotras", "Realiza el traspaso IAP del mes siguiente al mes guía, para los proyectos de los nodos identificados en la columna 'Traspaso IAP Est.'");

        aF = FilasDe("tblDatos");
        colorearMeses();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultar = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
           case "grabarEco":
           //case "grabar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aF = FilasDe("tblDatos");
                colorearMeses();
                mmoff("Suc","Grabación correcta",230);
                break;
           case "getNodos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aF = FilasDe("tblDatos");
                colorearMeses();
                break;
           //case "setAutoTraspIAP":
           case "setUMC_ECO":
           case "setUMC_IAP":
               break;
           case "traspasoNodosIAP":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aF = FilasDe("tblDatos");
                colorearMeses();
                mmoff("Suc","El traspaso se ha realzado de forma correcta",350);
                break;

            case "cierreIAP":
                var tblDatos = $I("tblDatos");
                //tblDatos.rows[iFilaNodoANodo].setAttribute("uc", nAnoMesActual);
                //tblDatos.rows[iFilaNodoANodo].cells[5].style.backgroundColor = "yellow";
                //tblDatos.rows[iFilaNodoANodo].cells[5].innerText = AnoMesToMesAnoDescLong(nAnoMesActual);
                //seleccionar(tblDatos.rows[iFilaNodoANodo].cells[0].children[0]);

                //if (tblDatos.rows[iFilaNodoANodo].cells[7].children[0].checked && $I("chkTraspIAPAuto").checked)
                //    tblDatos.rows[iFilaNodoANodo].cells[9].innerText = AnoMesToMesAnoDescLong(nAnoMesActual);

                ////iFilaNodoANodo++;
                //$I("divCatalogo").scrollTop = 16 * iFilaNodoANodo;

                //if (iFilaNodoANodo < tblDatos.rows.length) {
                //    //bOcultar = false;
                //    setTimeout("cierreIAPNodoANodo(1)", 20);
                //}
                for (var i = 0; i < aF.length; i++) {
                    if (aF[i].cells[0].children[0].checked) {
                        tblDatos.rows[i].setAttribute("uc", nAnoMesActual);
                        tblDatos.rows[i].cells[5].style.backgroundColor = "yellow";
                        tblDatos.rows[i].cells[5].innerText = AnoMesToMesAnoDescLong(nAnoMesActual);
                        if (tblDatos.rows[i].cells[7].children[0].checked && $I("chkTraspIAPAuto").checked)
                            tblDatos.rows[i].cells[9].innerText = AnoMesToMesAnoDescLong(nAnoMesActual);
                        //$I("divCatalogo").scrollTop = 16 * i;
                    }
                }
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultar)
            ocultarProcesando();
    }
}
//function grabarIAP(){
//    try{
//        js_args = "grabar@#@";
//        js_args+=nAnoMesActual + "@#@";
//        js_args+=flGetCadenaDesglose() + "@#@";
//        js_args+=AddAnnomes(nAnoMesActual, 1) + "@#@";
        
//        if ($I("chkTraspIAPAuto").checked){
//            js_args+= flGetCadenaTraspIAP();
//        }else{
//            js_args+= "";
//        }
//        mostrarProcesando();
//        RealizarCallBack(js_args, "");  //con argumentos 
//	}catch(e){
//		mostrarErrorAplicacion("Error al grabar los datos", e.message);
//    }
//}
function flGetCadenaDesglose(){
    var sRes="";
    try{ 
        for (var i=0;i<aF.length;i++){
            if (aF[i].cells[0].children[0].checked==true)
                sRes += aF[i].getAttribute("id")+"##";
        }
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación de IAP", e.message);
    }
}
function grabarEco(){
    try{
        js_args = "grabarEco@#@";
        js_args+=nAnoMesActual + "@#@";
        js_args+=flGetCadenaDesgloseEco() + "@#@";
        js_args+=AddAnnomes(nAnoMesActual, 1)
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos 
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}
function flGetCadenaDesgloseEco(){
    var sRes="";
    try{ 
        for (var i=0;i<aF.length;i++){
            if (aF[i].cells[1].children[0].checked==true)
                sRes += aF[i].getAttribute("id")+"##";
        }
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación económica", e.message);
    }
}
function cambiarMes(sValor){
    try{
        switch (sValor){
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "A_ECO": if (getOp($I("imgAM_ECO")) != 100) return; break;
            case "A_IAP": if (getOp($I("imgAM_IAP")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
            case "S_ECO": if (getOp($I("imgSM_ECO")) != 100) return; break;
            case "S_IAP": if (getOp($I("imgSM_IAP")) != 100) return; break;
        }
        mostrarProcesando();
        switch (sValor){
            case "A":
                nAnoMesActual=AddAnnomes(nAnoMesActual, -1);
                break;
            case "A_ECO":
                nAnoMesActualECO = AddAnnomes(nAnoMesActualECO, -1);
                break;
            case "A_IAP":
                nAnoMesActualIAP = AddAnnomes(nAnoMesActualIAP, -1);
                break;
            case "S":
                nAnoMesActual=AddAnnomes(nAnoMesActual, 1);
                break;
            case "S_ECO":
                nAnoMesActualECO = AddAnnomes(nAnoMesActualECO, 1);
                break;
            case "S_IAP":
                nAnoMesActualIAP = AddAnnomes(nAnoMesActualIAP, 1);
                break;
        }
        switch (sValor){
            case "A":
            case "S":
                $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMesActual);
                js_args = "getNodos@#@";
                js_args += AddAnnomes(nAnoMesActual, 1)                
                break;
            case "A_ECO":
            case "S_ECO":
                $I("txtMesBaseECO").value = AnoMesToMesAnoDescLong(nAnoMesActualECO);
                js_args = "setUMC_ECO@#@";
                js_args += nAnoMesActualECO;
                break;
            case "A_IAP":
            case "S_IAP":
                $I("txtMesBaseIAP").value = AnoMesToMesAnoDescLong(nAnoMesActualIAP);
                js_args = "setUMC_IAP@#@";
                js_args += nAnoMesActualIAP;
                break;
        }

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}

function colorearMeses(){
    var iNodosA=0;iNodosC=0,iNodosEcoA=0;iNodosEcoC=0, iProcesarIAP=0, iProcesarECO=0;

    try{
        if (aF == null) return;
        for (var i=0;i<aF.length;i++){
        
            if (aF[i].getAttribute("uc") != (nAnoMesActual - 1)){
                aF[i].cells[5].style.backgroundColor = "yellow";
                aF[i].cells[0].children[0].checked=false;
            }
            else{
                aF[i].cells[5].style.backgroundColor = "";
                if (aF[i].getAttribute("m") == "1")//Si el nodo tiene cierre estandar
                    aF[i].cells[0].children[0].checked=true;
                else
                    aF[i].cells[0].children[0].checked=false; 
            }
            if (parseInt(aF[i].getAttribute("uc"), 10) >= nAnoMesActual) iNodosC++;
            else iNodosA++;
            
            if (aF[i].cells[0].children[0].checked) iProcesarIAP++;
            
            if (aF[i].getAttribute("uce") != (nAnoMesActual - 1)){
                aF[i].cells[6].style.backgroundColor = "yellow";
                aF[i].cells[1].children[0].checked=false;
            }
            else{
                aF[i].cells[6].style.backgroundColor = "";
                if (aF[i].getAttribute("me") == "1")//Si el nodo tiene cierre estandar
                    aF[i].cells[1].children[0].checked=true;
                else
                    aF[i].cells[1].children[0].checked=false;
            }
            if (parseInt(aF[i].getAttribute("uce"), 10) >= nAnoMesActual) iNodosEcoC++;
            else iNodosEcoA++;
            
            if (aF[i].cells[1].children[0].checked) iProcesarECO++;
        }
        //Cierre IAP
        $I("lblTotalNodoIAP").innerText=aF.length;
        $I("lblProcesoIAP").innerText=iProcesarIAP;
        $I("lblNumA").innerText=iNodosA;//$I("lblNodo").innerText + "s abiertos: " + iNodosA;
        $I("lblNumC").innerText=iNodosC;//$I("lblNodo").innerText + "s cerrados: " + iNodosC;
        
        //Cierre Economico
        $I("lblTotalNodoECO").innerText=aF.length;
        $I("lblProcesoECO").innerText=iProcesarECO;
        $I("lblNumEcoA").innerText=iNodosEcoA;//$I("lblNodo").innerText + "s abiertos: " + iNodosEcoA;
        $I("lblNumEcoC").innerText=iNodosEcoC;//$I("lblNodo").innerText + "s cerrados: " + iNodosEcoC;
        
        setNPMA();
	}catch(e){
		mostrarErrorAplicacion("Error al colorear", e.message);
    }
}

function mTabla(nCol){
    try{
        for (i=0;i<aF.length;i++){
            aF[i].cells[nCol].children[0].checked = true;
        }
        switch (nCol) {
            case 0:
                $I("lblProcesoIAP").innerText = aF.length;
                break;
            case 1:
                $I("lblProcesoECO").innerText = aF.length;
                break;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function dTabla(nCol) {
    try{
        for (i=0;i<aF.length;i++){
            aF[i].cells[nCol].children[0].checked = false;
        }
        switch (nCol) {
            case 0:
                $I("lblProcesoIAP").innerText = "0";
                break;
            case 1:
                $I("lblProcesoECO").innerText = "0";
                break;
        }
    } catch (e) {
		mostrarErrorAplicacion("Error al desmarcar", e.message);
    }
}

function setIAP(oChk){
    try{
        $I("lblProcesoIAP").innerText = (oChk.checked)? parseInt($I("lblProcesoIAP").innerText, 10)+1:parseInt($I("lblProcesoIAP").innerText, 10)-1;
	}catch(e){
		mostrarErrorAplicacion("Error al marcar cierre IAP", e.message);
    }
}
function setECO(oChk){
    try{
        $I("lblProcesoECO").innerText = (oChk.checked)? parseInt($I("lblProcesoECO").innerText, 10)+1:parseInt($I("lblProcesoECO").innerText, 10)-1;
        setNPMA();
	}catch(e){
		mostrarErrorAplicacion("Error al desmarcar cierre ECO", e.message);
    }
}
function setNPMA(){
    var nNodosConMesesAbiertos = 0;

    try{
        for (var i=0;i<aF.length;i++){
            if (aF[i].cells[1].children[0].checked){
                if (aF[i].getAttribute("npma") > 0) nNodosConMesesAbiertos++;
            }
        }
        if (nNodosConMesesAbiertos == 0){
            AccionBotonera("cerrar", "H");
            $I("imgCaution").style.display = "none";
        }else{
            AccionBotonera("cerrar", "D");
            $I("imgCaution").style.display = "block";
        }
    }catch(e){
		mostrarErrorAplicacion("Error al controlar los proyectos con meses abiertos", e.message);
    }
}

function getPMA(oFila){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Cierre/getProyectosMesA/Default.aspx?nNodo=" + oFila.parentNode.id + "&nAnoMesActual=" + nAnoMesActual;
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 550));

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

//function setAutoTraspIAP(oChk){
//    try{
//        js_args = "setAutoTraspIAP@#@";
//        js_args += oChk.parentNode.parentNode.id +"@#@";
//        js_args += (oChk.checked)? "1":"0";
        
//        RealizarCallBack(js_args, "");
//	}catch(e){
//		mostrarErrorAplicacion("Error al marcar el check de Traspaso IAP", e.message);
//    }
//}

//function flGetCadenaTraspIAP(){
//    var sRes="";
//    try{ 
//        for (var i=0;i<aF.length;i++){
//            if (aF[i].cells[0].children[0].checked==true
//                && aF[i].cells[7].children[0].checked==true)
//                sRes += aF[i].getAttribute("id")+",";
//        }
//        return sRes;
//    }
//    catch(e){
//	    mostrarErrorAplicacion("Error al obtener la cadena de traspaso de consumos de IAP", e.message);
//    }
//}

function traspasoIAP(){
    try{ 
        var sw=0;
        js_args = "cierreIAP@#@";
        js_args += AddAnnomes(nAnoMesActual, 1) + "@#@N@#@S@#@";

        for (var i=0;i<aF.length;i++){
            if (aF[i].cells[7].children[0].checked){
                js_args += aF[i].getAttribute("id") + ",S/";
                sw = 1;
            }
        }
        js_args += "@#@";

        if (sw==0){
            mmoff("War","No se ha seleccionado ningún "+ strEstructuraNodoLarga + " para realizar los traspasos.", 500);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener los "+ strEstructuraNodoLarga + " para realizar el traspaso IAP", e.message);
    }
}
function traspasoIAPExc(){
    try{ 
        var sw=0;
        js_args = "cierreIAP@#@";
        js_args += AddAnnomes(nAnoMesActual, 1) + "@#@N@#@S@#@";

        for (var i=0;i<aF.length;i++){
            if (aF[i].cells[8].children[0].checked){
                js_args += aF[i].getAttribute("id") + ",N/";
                sw = 1;
            }
        }
        if (sw==0){
            mmoff("War","No se ha seleccionado ningún "+ strEstructuraNodoLarga.toLowerCase() + " para realizar los traspasos.", 500, 2500);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener los "+ strEstructuraNodoLarga + " para realizar el traspaso IAP", e.message);
    }
}

//var iFilaNodoANodo = 0;
//function cierreIAPNodoANodo(iMens){
//    try {
//        var js_args = "cierreIAPNodoANodo@#@";
//        js_args += nAnoMesActual + "@#@";
//        var sw = 0;
//        for (var i=iFilaNodoANodo;i<aF.length;i++){
//            if (aF[i].cells[0].children[0].checked){
//                sw = 1;
//                js_args += aF[i].getAttribute("id")+"@#@";
//                if (aF[i].cells[7].children[0].checked && $I("chkTraspIAPAuto").checked)
//                    js_args += "1";
//                else
//                    js_args += "0";
//                iFilaNodoANodo = i;
//                mostrarProcesando();
//                RealizarCallBack(js_args, "");  //con argumentos 
//                break;                    
//            }
//        }
//        if (iMens == 0 && sw == 0) {
//            ocultarProcesando();
//            mmoff("War","No se ha marcado ninguna fila en la columna de cierre IAP.", 400);
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al ir a cerrar IAP nodo a nodo", e.message);
//    }
//}
function cierreIAPNodoANodo() {
    try {

        var js_args = "cierreIAP@#@";
        js_args += nAnoMesActual + "@#@S@#@";
        if ($I("chkTraspIAPAuto").checked)
            js_args += "S@#@";
        else
            js_args += "N@#@";

        var sw = 0;

        for (var i = 0; i < aF.length; i++) {
            if (aF[i].cells[0].children[0].checked) {
                sw = 1;
                js_args += aF[i].getAttribute("id") + ",";
                if (aF[i].cells[7].children[0].checked)
                    js_args += "S";
                else
                    js_args += "N";
                js_args += "/";
            }
        }

        if (sw == 0) {
            ocultarProcesando();
            mmoff("War", "No se ha marcado ninguna fila en la columna de cierre IAP.", 400);
        }
        else {
            mostrarProcesando();
            RealizarCallBack(js_args, "");  //con argumentos 
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a cerrar IAP nodo a nodo", e.message);
    }
}

