function init(){
    try{
        ToolTipBotonera("excel", "Genera hoja Excel con el proyecto y la instantánea");
        if (bRes1024)
            setResolucion1024();

        $I("txtMesCierre").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        if (id_proyectosubnodo_actual != ""){// cargarProyecto();
            if ($I("txtEstado").value != null){
                switch ($I("txtEstado").value)
                {
                    case "A": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }
	        }
            var nom_proyecto=$I("txtNomProy").value;
            $I("divPry").innerHTML = "<input class='txtM' id='txtNomProy' Text='' style='width:500px' readonly='true' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>" + $I("lblNodo").innerText + ":</label>" + $I("txtDesCR").value + "<br><label style='width:70px'>Cliente:</label>" + $I("txtNomCliente").value + "<br><label style='width:70px'>Responsable:</label>" + $I("txtNomResp").value + "] hideselects=[off]\" />";
            $I("txtNomProy").value = nom_proyecto;
            setTimeout("mostrarProcesando();", 100);

            obtenerDatosSeguimiento2();
        }
        $I("txtCodProy").focus();
        
        setExcelImg("imgExcel", "divCatalogo", "excel");
        $I("imgExcel_exp").style.top = "180px";
        setExcelImg("imgExcel2", "divCatalogo2", "excel2");
        if (bRes1024)//pantalla pequeña
            $I("imgExcel2_exp").style.top = "457px";
        else
            $I("imgExcel2_exp").style.top = "558px";

        $I("imgExcel_exp").style.zIndex = 0;
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        //$I("divCatalogo").children[0].innerHTML = "";
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        bLimpiarDatos = true;
        switch (aResul[0]){
            case "getDatosProy":
                $I("txtNomCliente").value = aResul[3];
                $I("txtNomResp").value = aResul[4];
                $I("txtEstado").value = aResul[2];
                $I("txtNivelPresupuesto").value = aResul[5];
                switch ($I("txtEstado").value) {
                    case "A":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif";
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif";
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif";
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif";
                        $I("imgEstProy").title = "Proyecto histórico";
                }

                borrarCatalogo1();
                borrarCatalogo2();

                setTimeout("obtenerDatosSeguimiento();", 20);
                break;
            case "getPT":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").style.visibility = "visible";
                $I("txtPL").value = aResul[3];
                $I("txtInicioPL").value = aResul[4];
                $I("txtFinPL").value = aResul[5];
                $I("txtPrePL").value = aResul[6];
                $I("txtMes").value = aResul[7];
                $I("txtAcu").value = aResul[8];
                $I("txtPen").value = aResul[9];
                $I("txtEst").value = aResul[10];
                $I("txtFinEst").value = aResul[11];
                $I("txtTotPR").value = aResul[12];
                $I("txtPdtePR").value = aResul[13];
                $I("txtFinPR").value = aResul[14]; //13
                $I("txtAV").value = aResul[15];
                $I("txtAVPR").value = aResul[16];
                $I("txtPro").value = aResul[17];
                $I("txtIndiCon").value = aResul[18];
                $I("txtIndiDes").value = aResul[19];
                $I("txtIndiDesPlazo").value = aResul[20]; //19

                if (sMONEDA_VDP == "") {
                    sMONEDA_VDP = aResul[21]; //t422_idmoneda_proyecto
                    $I("lblMonedaImportes").innerText = aResul[22]; //t422_denominacionimportes
                }

                //if (es_DIS || sMOSTRAR_SOLODIS == "0")
                    $I("divMonedaImportes").style.visibility = "visible";
                //else
                    //$I("divMonedaImportes").style.visibility = "hidden";

                var fDesviacion = 0;
                var bHayDatos = false;
                var fTotPlanificado = dfn($I("txtPL").value);
                var fTotPrevisto = dfn($I("txtTotPR").value);
                if (fTotPlanificado != 0 && fTotPrevisto != 0) {
                    bHayDatos = true;
                    fDesviacion = (parseFloat(fTotPrevisto) * 100 / parseFloat(fTotPlanificado)) - 100;
                }

                if (bHayDatos) {
                    $I("txtIndiDes").value = fDesviacion.ToString("N");
                    if (fDesviacion <= 5) $I("txtIndiDes").className = "txtNumL SV";
                    else if (fDesviacion > 5 && fDesviacion <= 20) $I("txtIndiDes").className = "txtNumL SA";
                    else if (fDesviacion > 20) $I("txtIndiDes").className = "txtNumL SR";
                } else {
                    $I("txtIndiDes").value = "0";
                    $I("txtIndiDes").className = "txtNumL ST";
                }

                //Desviación de plazos
                if ($I("txtIndiDesPlazo").value != "") {
                    fDesviacion = dfn($I("txtIndiDesPlazo").value);
                    if (fDesviacion <= 5)
                        $I("txtIndiDesPlazo").className = "txtNumL SV";
                    else if (fDesviacion > 5 && fDesviacion <= 20)
                        $I("txtIndiDesPlazo").className = "txtNumL SA";
                    else if (fDesviacion > 20)
                        $I("txtIndiDesPlazo").className = "txtNumL SR";
                } else {
                    $I("txtIndiDesPlazo").value = "0";
                    $I("txtIndiDesPlazo").className = "txtNumL ST";
                }

                nNE = 1;
                colorearNE(1);
                nFilaSeg = -1;
                //ocultarProcesando();
                //Hay que obtener la lista de instantáneas y mostrar la más reciente.
                setTimeout("obtenerListaFotos();", 20);
                break;
                
            case "getTarea":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila+1);
                $I("tblDatos").rows[iFila].cells[0].children[0].src = strServer +"images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado", "1");
                nTablaGlobal=1;
                if (bMostrar) setTimeout("MostrarTodo();", 20);
                else ocultarProcesando();
                break;
                
            case "getFotoTarea":
                insertarFilasEnTablaDOM("tblDatos2", aResul[2], iFila+1);
                $I("tblDatos2").rows[iFila].cells[0].children[0].src = strServer +"images/minus.gif";
                $I("tblDatos2").rows[iFila].setAttribute("desplegado", "1");
                nTablaGlobal=2;
                if (bMostrar) setTimeout("MostrarTodo();", 20);
                else ocultarProcesando();
                break;

            case "getFotoPT":
		        $I("divCatalogo2").children[0].innerHTML = aResul[2];
		        $I("divCatalogo2").scrollTop = 0;
                //$I("divCatalogo2").style.visibility = "visible";
		        $I("txtPL2").value     = aResul[3];
		        $I("txtInicioPL2").value= aResul[4];
		        $I("txtFinPL2").value  = aResul[5];
		        $I("txtPrePL2").value  = aResul[6];
		        $I("txtMes2").value    = aResul[7];
		        $I("txtAcu2").value    = aResul[8];
		        $I("txtPen2").value    = aResul[9];
		        $I("txtEst2").value    = aResul[10];
		        $I("txtFinEst2").value = aResul[11];
		        $I("txtTotPR2").value  = aResul[12];
		        $I("txtPdtePR2").value  = aResul[13];
		        $I("txtFinPR2").value  = aResul[14];//13
		        $I("txtAV2").value     = aResul[15];
		        $I("txtAVPR2").value   = aResul[16];
		        $I("txtPro2").value    = aResul[17];
		        $I("txtIndiCon2").value   = aResul[18];
		        $I("txtIndiDes2").value   = aResul[19];
		        $I("txtIndiDesplazo2").value = aResul[20]; //19
		        
		        var fDesviacion = 0;
                var bHayDatos = false;
                var fTotPlanificado = dfn($I("txtPL2").value);
                var fTotPrevisto = dfn($I("txtTotPR2").value);
                if (fTotPlanificado != 0 && fTotPrevisto != 0){
                    bHayDatos = true;
                    fDesviacion = (parseFloat(fTotPrevisto) * 100 / parseFloat(fTotPlanificado)) - 100;    
                }
                if (bHayDatos){
                    $I("txtIndiDes2").value = fDesviacion.ToString("N");
                    if (fDesviacion <= 5) $I("txtIndiDes2").className = "txtNumL SV";
                    else if (fDesviacion > 5 && fDesviacion <= 20) $I("txtIndiDes2").className = "txtNumL SA";
                    else if (fDesviacion > 20) $I("txtIndiDes2").className = "txtNumL SR";
                }else{ 
                    $I("txtIndiDes2").value = "0";
                    $I("txtIndiDes2").className = "txtNumL ST";
                }
                //Desviación de plazos
                if ($I("txtIndiDesplazo2").value != "") {
                    fDesviacion = dfn($I("txtIndiDesplazo2").value);
                    if (fDesviacion <= 5) 
                        $I("txtIndiDesplazo2").className = "txtNumL SV";
                    else if (fDesviacion > 5 && fDesviacion <= 20) 
                        $I("txtIndiDesplazo2").className = "txtNumL SA";
                    else if (fDesviacion > 20) 
                        $I("txtIndiDesplazo2").className = "txtNumL SR";
                }else{ 
                    $I("txtIndiDesplazo2").value = "0";
                    $I("txtIndiDesplazo2").className = "txtNumL ST";
                }
                
                nNE = 1;colorearNE(2);
	            nFilaSeg = -1;
                ocultarProcesando();

                break;
                
             case "ListaFotos":
		        var aOpciones = aResul[2].split("##");
		        aFoto.length = 0;
		        nIndiceFoto = 0;
		        var sw=0;
		        for (var i=0;i<aOpciones.length-1;i++){//último elemento vacío
		            var aDatos = aOpciones[i].split("///");
		            //aFoto[i] = new Array(aDatos[0], aDatos[1].substring(0, aDatos[1].length - 3)); // id /// fecha
		            aFoto[i] = new Array(aDatos[0], aDatos[1].substring(0, aDatos[1].length - 3), aDatos[2]); // id /// fecha /// fecha consumoIAP
		            sw = 1;
		        }

                if (sw == 1){
                    setTimeout("obtenerDatosFoto();", 50);
                }else
                    ocultarProcesando();
                break;
               
            case "recuperarPSN":
                if (aResul[3]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            $I("txtUne").value = aResul[5];
		        $I("txtDesCR").value = aResul[6];
	            $I("txtCodProy").value = aResul[3].ToString("N", 9, 0);
	            $I("txtNomProy").value = aResul[4];
	            $I("t305IdProyectoSubnodo").value = aResul[2];
	            $I("MonedaPSN").value = aResul[7];
	            if (sMONEDA_VDP == "") {
	                sMONEDA_VDP = aResul[7]; //t422_idmoneda_proyecto
	            }
	            
	            setTimeout("getDatosProyecto();", 20);
                break;
            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                }else{
                    var aProy = aResul[2].split("///");
                    //alert(aProy.length);
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##")
                        $I("t305IdProyectoSubnodo").value = aDatos[0];
                        if (sMONEDA_VDP == "") {
                            sMONEDA_VDP = aDatos[3]; //t422_idmoneda_proyecto
                        }
                        //                        if (aDatos[1] == "1"){
//                            bLectura = true;
//                        }else{
//                            bLectura = false;
//                        }
//                        if (es_administrador == "SA" || es_administrador == "A"){
//                            bRTPT = false;
//                        }
//                        else{
//                            if (aDatos[2] == "1"){
//                                bRTPT = true;
//                            }else{
//                                bRTPT = false;
//                            }
//                        }
                        setTimeout("recuperarDatosPSN();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "setResolucion":
                location.reload(true);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
    }
}
function recuperarDatosPSN(){
    try{
        var js_args = "recuperarPSN@#@";
        js_args += $I("t305IdProyectoSubnodo").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
function getPEByNum(){
    try{    
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&nPE=" + dfn($I("txtCodProy").value);
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("t305IdProyectoSubnodo").value = aDatos[0];
                    recuperarDatosPSN();
                }
            });
        ocultarProcesando();
        window.focus();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

function obtenerProyectos(){
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                borrarDatosFoto();
	                aDatos = ret.split("///");
	                id_proyectosubnodo_actual = aDatos[0];

	                recuperarPSN();
	            }
	        });
	    window.focus();	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function recuperarPSN(){
    try{
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        js_args += id_proyectosubnodo_actual;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function getDatosProyecto(){
    try{
        var js_args = "getDatosProy@#@" + $I("t305IdProyectoSubnodo").value;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function obtenerDatosSeguimiento(){
    try {
        borrarCatalogo1();

        var nom_proyecto=$I("txtNomProy").value;
        $I("divPry").innerHTML = "<input class='txtM' id='txtNomProy' Text='' style='width:500px' readonly='true' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>" + $I("lblNodo").innerText + ":</label>" + $I("txtDesCR").value + "<br><label style='width:70px'>Cliente:</label>" + $I("txtNomCliente").value + "<br><label style='width:70px'>Responsable:</label>" + $I("txtNomResp").value + "] hideselects=[off]\" />";
        $I("txtNomProy").value=nom_proyecto;
	    
        var js_args = "getPT@#@";
        js_args += num_empleado +"@#@";
        js_args += $I("t305IdProyectoSubnodo").value +"@#@";  
        js_args += $I("txtMesCierre").value+"@#@";
        js_args += $I("txtEstado").value + "@#@";
        js_args += $I("MonedaPSN").value + "@#@";
        js_args += $I("txtNivelPresupuesto").value;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de seguimiento", e.message);
    }
}
function obtenerDatosSeguimiento2(){
    try{
        mostrarProcesando();
        borrarCatalogo1();

        $I("txtCodProy").value = $I("txtCodProy").value.ToString("N", 6, 0);

        var js_args = "getPT@#@";
        js_args += num_empleado +"@#@";
        js_args += $I("t305IdProyectoSubnodo").value +"@#@";  
        js_args += $I("txtMesCierre").value+"@#@";
        js_args += $I("txtEstado").value + "@#@";
        js_args += $I("MonedaPSN").value + "@#@";
        js_args += $I("txtNivelPresupuesto").value;

        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de seguimiento(2)", e.message);
    }
}
function obtenerFotos(){
    try{
	    var aOpciones;
        //if (aProy.length == 0) return;
        if ($I("t305IdProyectoSubnodo").value == "") return;
        var nPE = $I("t305IdProyectoSubnodo").value;//aProy[nIndiceProy][4];
        
        borrarDatosFoto();
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/PSP/Consultas/SeguimientoHist/obtenerFotos.aspx?p=" + codpar(nPE);
	    modalDialog.Show(strEnlace, self, sSize(550, 450))
            .then(function(ret) {
	        if (ret != null) {
	            aOpciones = ret.split("@#@");
	            aFoto.length = 0;
	            nIndiceFoto = aOpciones[0];
	            for (var i = 1; i < aOpciones.length; i++) {
	                var aDatos = aOpciones[i].split("///");
	                //aFoto[i - 1] = new Array(aDatos[0], aDatos[1], aDatos[2], aDatos[3], aDatos[4], aDatos[5], aDatos[6], aDatos[7], aDatos[8]);
	                aFoto[i - 1] = new Array(aDatos[0], aDatos[1], aDatos[2]);
	            }

	            obtenerDatosFoto();
	        }
	    });
	    window.focus();	    
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las fotos", e.message);
    }
}

function obtenerListaFotos(){
    try{
        var js_args = "ListaFotos@#@";
        js_args += $I("t305IdProyectoSubnodo").value.replace(".","");

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;

	}catch(e){
		mostrarErrorAplicacion("Error al obtener las fotos", e.message);
    }
}
function mostrarFoto(nOpcion){
    try{
        if (aFoto.length == 0) return;
        switch (nOpcion){
            case 1:{
                if (getOp($I("btnPriRegOff2")) == 30) return;
                break;
            }
            case 2:{
                if (getOp($I("btnAntRegOff2")) == 30) return;
                break;
            }
            case 3:{
                if (getOp($I("btnSigRegOff2")) == 30) return;
                break;
            }
            case 4:{
                if (getOp($I("btnUltRegOff2")) == 30) return;
                break;
            }
        }

        switch (nOpcion){
            case 4:{
                nIndiceFoto = 0;
                break;
            }
            case 3:{
                if (nIndiceFoto > 0) nIndiceFoto--;
                break;
            }
            case 2:{
                if (nIndiceFoto < aFoto.length-1) nIndiceFoto++;
                break;
            }
            case 1:{
                nIndiceFoto = aFoto.length-1;
                break;
            }
        }

        obtenerDatosFoto();
        
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de seguimiento", e.message);
    }
}

function obtenerDatosFoto(){
    try {
        borrarCatalogo2();
        $I("txtFechaFoto").value = aFoto[nIndiceFoto][1];
        $I("lblConsumos").innerHTML = "Consumos IAP  a fecha " + aFoto[nIndiceFoto][2];
        //Botones de navegación sobre instantaneas
        flActivarBtnsPE2(aFoto.length,nIndiceFoto);
	    
        var js_args = "getFotoPT@#@";
        js_args += aFoto[nIndiceFoto][0];  //idFoto

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	    
	}catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos de la instantánea (obtenerDatosFoto)", e.message);
    }
}

function borrarDatosFoto(){
    try{
        borrarCatalogo2();
            
        aFoto.length = 0;
        $I("txtFechaFoto").value = "";
        //Botones de navegación sobre instantaneas
        flActivarBtnsPE2(aFoto.length, nIndiceFoto);
	    
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los datos de la foto", e.message);
    }
}

/* Función para establecer el nivel de expansión */
var nNE = 1;
function setNE(nValor, nTabla){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }
        nTablaGlobal=nTabla;
        mostrarProcesando();
        nNE = nValor;
        colorearNE(nTabla);
//        MostrarOcultar(0, nTabla);
//        if (nNE > 1) MostrarOcultar(1, nTabla);
        setTimeout("setNE2()", 100);
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function setNE2(){
    try{
        MostrarOcultar(0, nTablaGlobal);
        if (nNE > 1) MostrarOcultar(1, nTablaGlobal);
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function colorearNE(nTabla){
    try{
        if (nTabla==1){
            switch(nNE){
                case 1:
                    $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                    $I("imgNE2").src = "../../../../images/imgNE2off.gif";
                    break;
                case 2:
                    $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                    $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                    break;
            }
        }
        else {
            switch(nNE){
                case 1:
                    $I("imgFotoNE1").src = "../../../../images/imgNE1on.gif";
                    $I("imgFotoNE2").src = "../../../../images/imgNE2off.gif";
                    break;
                case 2:
                    $I("imgFotoNE1").src = "../../../../images/imgNE1on.gif";
                    $I("imgFotoNE2").src = "../../../../images/imgNE2on.gif";
                    break;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al colorear el nivel de expansión", e.message);
    }
}

function MostrarOcultar(nMostrar, nTabla){
    try{
        if (nTabla == 1 && $I("tblDatos").rows.length == 0) return;
        if (nTabla == 2 && $I("tblDatos2").rows.length == 0) return;

        nTablaGlobal = nTabla;
        if (nMostrar == 0){//Contraer
            if (nTabla == 1){
                for (var i=0; i<$I("tblDatos").rows.length;i++){
                    //if (tblDatos.rows[i].nivel > 1)
                    if ($I("tblDatos").rows[i].getAttribute("exp") > 0)
                    {
                        //if (tblDatos.rows[i].nivel == 2)
                        if ($I("tblDatos").rows[i].getAttribute("T") == "0")
                            $I("tblDatos").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                        $I("tblDatos").rows[i].style.display = "none";
                    }
                    else 
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
            }else{
                for (var i=0; i<$I("tblDatos2").rows.length;i++){
                    //if (tblDatos2.rows[i].nivel > 1)
                    if ($I("tblDatos2").rows[i].getAttribute("exp") > 0)
                    {
                        if ($I("tblDatos2").rows[i].getAttribute("T") == "0")
                            $I("tblDatos2").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                        $I("tblDatos2").rows[i].style.display = "none";
                    }
                    else 
                    {
                        $I("tblDatos2").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    }                             
                }
            }
            ocultarProcesando();
        }else{ //Expandir
            MostrarTodo();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar=false;
var nIndiceTodo = -1;
var nTablaGlobal = -1;
function MostrarTodo(){
    try
    {
        if (nTablaGlobal == 1 && $I("tblDatos").rows.length == 0) return;
        if (nTablaGlobal == 2 && $I("tblDatos2").rows.length == 0) return;
        
        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        if (nTablaGlobal == 1){
            for (var i=nIndiceAux; i<$I("tblDatos").rows.length;i++){
                if ($I("tblDatos").rows[i].getAttribute("exp") < nNE){ 
                    if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                        bMostrar=true;
                        nIndiceTodo = i;
                        mostrar($I("tblDatos").rows[i].cells[0].children[0]);
                        return;
                    }
                }
            }
        }else{
            for (var i=nIndiceAux; i< $I("tblDatos2").rows.length;i++){
                if ($I("tblDatos2").rows[i].getAttribute("exp") < nNE){ 
                    if ($I("tblDatos2").rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                        bMostrar=true;
                        nIndiceTodo = i;
                        mostrar($I("tblDatos2").rows[i].cells[0].children[0]);
                        return;
                    }
                }
            }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}
function mostrar(oImg){
    try{
        var bFoto = false; //Indica si se va a desplegar una rama de la tabla superior (actual) o de la inferior (foto)
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        
        if (oFila.getAttribute("idFotoPT") != null) bFoto = true;
        js_args="";
        if (nDesplegado == "0"){
            switch (nNivel){
                case "1": //Fases + Actividades + Tareas
                    if (!bFoto){ //Desplegar PT actual
                        var js_args = "getTarea@#@";
                        js_args += num_empleado +"@#@";
                        js_args += oFila.getAttribute("PE") +"@#@";
                        js_args += oFila.getAttribute("PT") +"@#@"; 
                        js_args += $I("txtMesCierre").value +"@#@";
                        js_args += oFila.getAttribute("estado") + "@#@";
                        js_args += oFila.getAttribute("cualidad") + "@#@";
                        js_args += $I("txtNivelPresupuesto").value;
                    }else{ //Desplegar PT foto
                        var js_args = "getFotoTarea@#@";
                        js_args += oFila.getAttribute("idFotoPT");
                    }
                    break;
            }
            iFila=nIndexFila;
            if (js_args != ""){
                mostrarProcesando();
                RealizarCallBack(js_args, ""); 
            }
            return;
        }
        if (!bFoto){
            for (var i=nIndexFila+1; i<$I("tblDatos").rows.length; i++){
                if ($I("tblDatos").rows[i].getAttribute("nivel") > nNivel){
                    if (opcion == "O")
                    {
                        $I("tblDatos").rows[i].style.display = "none";
                            //if (tblDatos.rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                            if ($I("tblDatos").rows[i].getAttribute("T") == 0)
                                $I("tblDatos").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    }
                    else if ($I("tblDatos").rows[i].getAttribute("nivel") - 1 == nNivel) $I("tblDatos").rows[i].style.display = "table-row";
                }else{
                    break;
                }
            }
            if (opcion == "O" && $I("tblDatos").rows[nIndexFila].getAttribute("T") == 0) oImg.src = "../../../../images/plus.gif";
            else oImg.src = "../../../../images/minus.gif"; 
        }
        else{
            for (var i=nIndexFila+1; i<$I("tblDatos2").rows.length; i++){
                if ($I("tblDatos2").rows[i].getAttribute("nivel") > nNivel){
                    if (opcion == "O")
                    {
                        $I("tblDatos2").rows[i].style.display = "none";
                        if ($I("tblDatos2").rows[i].getAttribute("T") == 0)
                            $I("tblDatos2").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    }
                    else if ($I("tblDatos2").rows[i].getAttribute("nivel") - 1 == nNivel) $I("tblDatos2").rows[i].style.display = "table-row";
                }else{
                    break;
                }
            }
            if (opcion == "O" && $I("tblDatos2").rows[nIndexFila].getAttribute("T") == 0) oImg.src = "../../../../images/plus.gif";
            else oImg.src = "../../../../images/minus.gif"; 
        }
        

//        if (nNivel < 3 && !bMostrar)
//        {
//            if (!bFoto) recolorearTablaSeguimiento("tblDatos");
//            else recolorearTablaSeguimiento("tblDatos2");
//        }     
        if (bMostrar) MostrarTodo(); 
        ocultarProcesando();
        
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function flActivarBtnsPE2(iNumPE, iIndAct){
    //Establece la visibilidad de los botones de navegación entre las fotos 
    try{
	    if (iNumPE>1){
            if (iIndAct==0){
                setOp($I("btnPriRegOff2"), 100);
                setOp($I("btnAntRegOff2"), 100);
                setOp($I("btnSigRegOff2"), 30);
                setOp($I("btnUltRegOff2"), 30);
            }
            else{
                if (iIndAct==iNumPE-1){
                    setOp($I("btnPriRegOff2"), 30);
                    setOp($I("btnAntRegOff2"), 30);
                    setOp($I("btnSigRegOff2"), 100);
                    setOp($I("btnUltRegOff2"), 100);
                }
                else{
                    setOp($I("btnPriRegOff2"), 100);
                    setOp($I("btnAntRegOff2"), 100);
                    setOp($I("btnSigRegOff2"), 100);
                    setOp($I("btnUltRegOff2"), 100);
                }
            }
	    }
	    else{
            setOp($I("btnPriRegOff2"), 30);
            setOp($I("btnAntRegOff2"), 30);
            setOp($I("btnSigRegOff2"), 30);
            setOp($I("btnUltRegOff2"), 30);
	    }
	}
	catch(e){
		mostrarErrorAplicacion("Error al establecer la visibilidad de los botones de navegación entre las fotos.", e.message);
	}
}

function excel() {
    try {
        if ($I("tblDatos")==null || $I("tblDatos").rows.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        crearExcel(cabecera() + cadExcel1());
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function cadExcel1() {
    try {
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td>"+$I("txtMesCierre").value+"</td>");
        sb.Append("        <td colspan='4' style='background-color: #E4EFF3;'>Planificado</TD>");
        sb.Append("        <td colspan='5' style='background-color: #E4EFF3;'>IAP</TD>");
        sb.Append("        <td colspan='4' style='background-color: #E4EFF3;'>Previsto</TD>");
        sb.Append("        <td colspan='2' style='background-color: #E4EFF3;'>Avance</TD>");
        sb.Append("        <td colspan='3' style='background-color: #E4EFF3;'>Indicadores</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='background-color: #BCD4DF;'>P. técnico / Tarea / Profesional</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Inicio</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Fin</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Importe presupuestado</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Mes</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Acumul.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Pend. Est.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total Est.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Fin Est.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Pendiente</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Fin</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>%</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>%</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Importe producido</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>% Con.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>% DE.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>% DP.</TD>");
		sb.Append("	</TR>");

        var aFila = FilasDe("tblDatos");
	    for (var i=0;i < aFila.length; i++){
		    if (aFila[i].style.display == "none") continue;
		    
	        sb.Append("<tr>");
	        
            for (var x=0; x<=18;x++){
                var aInput = aFila[i].cells[x].getElementsByTagName("INPUT");
                
                if (aFila[i].cells[x].style.backgroundColor.toUpperCase() == "#F58D8D")
                    sb.Append("<td style='background-color:#F58D8D;text-align:right'>");
                else if (aFila[i].cells[x].className.toUpperCase() == "SV")
                    sb.Append("<td style='background-color:#00ff00;text-align:right'>");
                else if (aFila[i].cells[x].className.toUpperCase() == "SA")
                    sb.Append("<td style='background-color:yellow;text-align:right'>");
                else if (aFila[i].cells[x].className.toUpperCase() == "SR")
                    sb.Append("<td style='background-color:#F45C5C;text-align:right'>");
                else if (x>0)
                    sb.Append("<td style='text-align:right'>");
                     else
                    sb.Append("<td>");
                
                if (x==0){
                    switch(aFila[i].getAttribute("nivel")){
                        case "1": sb.Append(""); break;
                        case "2": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                        case "3": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                        case "4": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                    }
                }//else sb.Append("&nbsp;";  //Para que no se pierdan los decimales 0,00--> 0  o  9,60 --> 9,6 
            
                if (aInput.length==0) sb.Append(aFila[i].cells[x].innerText);
                else sb.Append(aInput[0].value);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }

	    var aFilaRes = FilasDe("tblResultado");
	    for (var i=0;i < aFilaRes.length; i++){
	        sb.Append("<tr>");
	        sb.Append("<td style='background-color: #BCD4DF;'>Total proyecto económico</td>");
            for (var x=1; x<=18;x++){
                var aInput = aFilaRes[i].cells[x].getElementsByTagName("INPUT");
                if (x > 16){
                    if (aFilaRes[i].cells[x].children[0].className.toUpperCase() == "TXTNUML SV")
                        sb.Append("<td style='background-color:#00ff00;text-align:right'>");
                    else if (aFilaRes[i].cells[x].children[0].className.toUpperCase() == "TXTNUML SA")
                        sb.Append("<td style='background-color:yellow;'text-align:right>");
                    else if (aFilaRes[i].cells[x].children[0].className.toUpperCase() == "TXTNUML SR")
                        sb.Append("<td style='background-color:#F45C5C;text-align:right'>");
                    else
                        sb.Append("<td style='background-color: #BCD4DF;text-align:right'>");
                }else if (x>0)
                    sb.Append("<td style='background-color: #BCD4DF;text-align:right'>");
                     else
                    sb.Append("<td style='background-color: #BCD4DF;'>");

                //sb.Append("&nbsp;";
                if (aInput.length==0) sb.Append(aFilaRes[i].cells[x].innerText);
                else sb.Append(aInput[0].value);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }
	    //sb.Append("<tr><td colspan='24' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");	    
	    sb.Append("</table>");

        return sb.ToString();
        //var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function cabecera() {
    var sb = new StringBuilder;
    sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
    //sb.Append("<tr><td colspan='19' style='font-weight:bold;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr><tr><td colspan='19'></td></tr>");
    sb.Append("<tr style='vertical-align:top;'>");
    sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");

    for (var j = 2; j <= 19; j++) {
        sb.Append("<td></td>");
    }
    sb.Append("</tr>");  

    sb.Append("</table>");
    return sb.ToString();
}
function excel2() {
    try {
        if ($I("tblDatos2") == null || $I("tblDatos2").rows.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        crearExcel(cabecera() + cadExcel2());
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function cadExcel2() {
    try{
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td>"+$I("txtFechaFoto").value+"</td>");
        sb.Append("        <td colspan='4' style='background-color: #E4EFF3;'>Planificado</TD>");
        sb.Append("        <td colspan='5' style='background-color: #E4EFF3;'>IAP</TD>");
        sb.Append("        <td colspan='4' style='background-color: #E4EFF3;'>Previsto</TD>");
        sb.Append("        <td colspan='2' style='background-color: #E4EFF3;'>Avance</TD>");
        sb.Append("        <td colspan='3' style='background-color: #E4EFF3;'>Indicadores</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='background-color: #BCD4DF;'>P. técnico / Tarea / Profesional</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Inicio</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Fin</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Importe presupuestado</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Mes</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Acumul.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Pend. Est.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total Est.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Fin Est.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Pendiente</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Fin</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>%</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>%</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Importe producido</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>% Con.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>% DE.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>% DP.</TD>");
		sb.Append("	</TR>");
        
        if ($I("tblDatos2") != null){
            var aFilaF = FilasDe("tblDatos2");
	        for (var i=0;i < aFilaF.length; i++){
		        if (aFilaF[i].style.display == "none") continue;
    		    
	            sb.Append("<tr>");
    	        
                for (var x=0; x<=18;x++){
                    var aInput = aFilaF[i].cells[x].getElementsByTagName("INPUT");
                    
                    if (aFilaF[i].cells[x].style.backgroundColor.toUpperCase() == "#F58D8D")
                        sb.Append("<td style='background-color:#F58D8D;text-align:right'>");
                    else if (aFilaF[i].cells[x].className.toUpperCase() == "SV")
                        sb.Append("<td style='background-color:#00ff00;text-align:right'>");
                    else if (aFilaF[i].cells[x].className.toUpperCase() == "SA")
                        sb.Append("<td style='background-color:yellow;text-align:right'>");
                    else if (aFilaF[i].cells[x].className.toUpperCase() == "SR")
                        sb.Append("<td style='background-color:#F45C5C;text-align:right'>");
                    else if (x>0)
                        sb.Append("<td style='text-align:right'>");
                         else
                        sb.Append("<td>");
                    
                    if (x==0){
                        switch(aFilaF[i].getAttribute("nivel")){
                            case "1": sb.Append(""); break;
                            case "2": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                            case "3": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                            case "4": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
                        }
                    }//else sb.Append("&nbsp;";  //Para que no se pierdan los decimales 0,00--> 0  o  9,60 --> 9,6 
                
                    if (aInput.length==0) sb.Append(aFilaF[i].cells[x].innerText);
                    else sb.Append(aInput[0].value);
                    sb.Append("</td>");
                }
	            sb.Append("</tr>");
	        }
        }

	    var aFilaRes2 = FilasDe("tblResultado2");
	    for (var i=0;i < aFilaRes2.length; i++){
	        sb.Append("<tr style='background-color: #BCD4DF;'>");
	        sb.Append("<td>Total proyecto económico</td>");
            for (var x=1; x<=18;x++){
                var aInput = aFilaRes2[i].cells[x].getElementsByTagName("INPUT");
                if (x > 16){
                    if (aFilaRes2[i].cells[x].children[0].className.toUpperCase() == "TXTNUML SV")
                        sb.Append("<td style='background-color:#00ff00;text-align:right'>");
                    else if (aFilaRes2[i].cells[x].children[0].className.toUpperCase() == "TXTNUML SA")
                        sb.Append("<td style='background-color:yellow;'text-align:right>");
                    else if (aFilaRes2[i].cells[x].children[0].className.toUpperCase() == "TXTNUML SR")
                        sb.Append("<td style='background-color:#F45C5C;text-align:right'>");
                    else
                        sb.Append("<td style='text-align:right'>");
                }else if (x>0)
                    sb.Append("<td style='text-align:right'>");
                     else
                    sb.Append("<td>");

                //sb.Append("&nbsp;";
                if (aInput.length==0) sb.Append(aFilaRes2[i].cells[x].innerText);
                else sb.Append(aInput[0].value);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        return sb.ToString();
        //var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function excelTotal() {
    var sCad1, sCad2;
    try {
        if ($I("tblDatos") == null || $I("tblDatos").rows.length == 0)
            sCad1 = "";
        else
            sCad1 = cadExcel1();
        if ($I("tblDatos2") == null || $I("tblDatos2").rows.length == 0)
            sCad2 = "";
        else
            sCad2 = cadExcel2();
        if (sCad1 == "" && sCad2 == "") {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
        }
        else {
            crearExcel(cabecera() + sCad1 + "<table><tr><td></td></tr><tr><td></td></tr></table>" + sCad2);
        }
    }
    catch (e) {
        ocultarProcesando();
        mmoff("Inf", "No hay información en pantalla para exportar.", 300);
    }
}
function buscarPE(){
    try{
        $I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtCodProy").value);
        setNumPE();
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
            $I("imgEstProy").title = "";
            $I("divPry").innerHTML = "<input type='text' class='txtM' id='txtNomProy' value='' style='width:500px;' readonly='true' />";
            $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
            $I("divCatalogo2").children[0].innerHTML = "<table id='tblDatos2'></table>";
	            
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function setResolucionPantalla(){
    try{
        mostrarProcesando();
        var js_args = "setResolucion@#@";
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}

function setResolucion1024(){
    try{
        $I("divCatalogo").style.height = "180px";
        $I("divCatalogo2").style.height = "160px";
        $I("divGral").style.height = "550px";
        $I("divGral").style.width = "1000px";
    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}

function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDP";
        modalDialog.Show(strEnlace, self, sSize(350, 300))
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    sMONEDA_VDP = aDatos[0];
                    $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];

                    borrarCatalogo1();
                    borrarCatalogo2();
                    getDatosProyecto();
                }
            });
        window.focus();	    

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}

function borrarCatalogo1() {
    try {
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
        $I("txtPL").value = "0,00";
        $I("txtInicioPL").value = "";
        $I("txtFinPL").value = "";
        $I("txtPrePL").value = "0,00";
        $I("txtMes").value = "0,00";
        $I("txtAcu").value = "0,00";
        $I("txtPen").value = "0,00";
        $I("txtEst").value = "0,00";
        $I("txtFinEst").value = "";
        $I("txtTotPR").value = "0,00";
        $I("txtPdtePR").value = "0,00";
        $I("txtFinPR").value = "";
        $I("txtAV").value = "0";
        $I("txtAVPR").value = "0";
        $I("txtPro").value = "0,00";
        $I("txtIndiCon").value = "0";
        $I("txtIndiDes").value = "0";
        $I("txtIndiDes").className = "txtNumL ST";
        $I("txtIndiDesPlazo").value = "0";
        $I("txtIndiDesPlazo").className = "txtNumL ST";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo vigente.", e.message);
    }
}
function borrarCatalogo2() {
    try {
        $I("divCatalogo2").children[0].innerHTML = "<table id='tblDatos2' class='texto' style='width:1240px;'></table>";
        $I("txtPL2").value = "0,00";
        $I("txtInicioPL2").value = "";
        $I("txtFinPL2").value = "";
        $I("txtPrePL2").value = "0,00";
        $I("txtMes2").value = "0,00";
        $I("txtAcu2").value = "0,00";
        $I("txtPen2").value = "0,00";
        $I("txtEst2").value = "0,00";
        $I("txtFinEst2").value = "";
        $I("txtTotPR2").value = "0,00";
        $I("txtPdtePR2").value = "0,00";
        $I("txtFinPR2").value = "";
        $I("txtAV2").value = "0";
        $I("txtAVPR2").value = "0";
        $I("txtPro2").value = "0,00";
        $I("txtIndiCon2").value = "0";
        $I("txtIndiDes2").value = "0";
        $I("txtIndiDes2").className = "txtNumL ST";
        $I("txtIndiDesplazo2").value = "0";
        $I("txtIndiDesplazo2").className = "txtNumL ST";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo de instantáneas.", e.message);
    }
}

function setPosImgExcel() {
    try {
        //alert($I("divGral").scrollLeft + "\n" + $I("imgExcel2_exp").style.posLeft);
        var nLeft = 1317 - $I("divGral").scrollLeft;
        $I("imgExcel_exp").style.left = nLeft + "px";
        $I("imgExcel2_exp").style.left = nLeft + "px";
        //$I("imgExcel_exp").style.left = "1250px";
        //alert("$I('imgExcel_exp').style.left=" + $I("imgExcel_exp").style.left);
    } catch (e) {
        mostrarErrorAplicacion("Error al posicionar las imágenes de exportación de excel.", e.message);
    }
}
