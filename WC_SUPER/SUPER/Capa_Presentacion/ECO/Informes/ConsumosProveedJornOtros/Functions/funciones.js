var strAction = "";
var strTarget = "";
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nCriterioAVisualizar = 0;
var bCargandoCriterios=false;
var js_subnodos = new Array();

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

var iDesdeOld = 0;
var iHastaOld = 0;

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

function init(){
    try{
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target;   

        iDesdeOld = $I("hdnDesde").value;
        iHastaOld = $I("hdnHasta").value;
         
        setOperadorLogico();
        js_subnodos = sSubnodos.split(",");  
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
var sOLAnterior = "";
function setOperadorLogico(){
    try{
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        if (sOL == sOLAnterior) return;
        else sOLAnterior = sOL;
        
        setTodos();

	}catch(e){
		mostrarErrorAplicacion("Error al modificar el operador lógico.", e.message);
	}
}
function getPeriodo(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
	    modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
	            if (ret != null){
	                var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
        //            var iProc = 0;
        //            if  ($I("hdnDesde").value < iDesdeOld || $I("hdnHasta").value > iHastaOld) 
        //            {
        //                getTablaCriterios();
        //                iProc=1;
        //            }
        //            
        //            if (iProc==0) ocultarProcesando();
                }        
                ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}
function getTablaCriterios(){
    try{
        var js_args = "getTablaCriterios@#@";
        js_args += $I("hdnDesde").value +"@#@";
        js_args += $I("hdnHasta").value;
        bCargandoCriterios=true;
        RealizarCallBack(js_args, "");
        js_cri.length = 0;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los nuevos criterios", e.message);
    }
}
function getCriterios(nTipo){
    try{
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == ""){
            nCriterioAVisualizar = nTipo;
            mmoff("InfPer", "Actualizando valores de criterios... Espere, por favor", 350);
            return;
        }

        nCriterioAVisualizar = 0;
        mostrarProcesando();
        
        var nCC = 0; //ncount de criterios.
        var bExcede = false;
        for (var i=0; i<js_cri.length; i++)
        {
            if (js_cri[i].t > nTipo) break;
            if (js_cri[i].t < nTipo) continue;
            if (typeof(js_cri[i].excede)!="undefined"){
                bExcede = true;
                break;
            }
        }

        if (es_administrador != "" || bExcede) bCargarCriterios = false;
        else bCargarCriterios = true;

        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;            
        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(850, 400);
        
        var strEnlace = "";
        switch (nTipo){
            case 1:               
                if (bCargarCriterios){
                    for (var i=0; i<js_cri.length; i++)
                    {
                        if (js_cri[i].t > 1) break;
                        if (i==0) sSubnodos = js_cri[i].c;
                        else sSubnodos += ","+js_cri[i].c;
                    }
                }
                //strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sSnds=" + codpar(sSubnodos) + "&sExcede=" + ((bExcede) ? "T" : "F");
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sExcede=" + ((bExcede) ? "T" : "F");
                sTamano = sSize(950, 450);
                break;         
            case 16:  

                if (bCargarCriterios){
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioProyecto/Default.aspx?nTipo=" + nTipo + "&sMod=pge";
                    sTamano = sSize(1010, 570);
                }
                else{
                    strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pge";
                    sTamano = sSize(1010, 720);
                }
                break;
            default:
                if (bCargarCriterios) {
                    sTamano = sSize(850, 440);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterio/Default.aspx?nTipo=" + nTipo;
                }
                else {
                    sTamano = sSize(850, 420);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
                } 
                break;
        }   
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo){
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 5: oTabla = $I("tblHorizontal"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 10: oTabla = $I("tblQn"); break;
            case 11: oTabla = $I("tblQ1"); break;
            case 12: oTabla = $I("tblQ2"); break;
            case 13: oTabla = $I("tblQ3"); break;
            case 14: oTabla = $I("tblQ4"); break;
            case 16: oTabla = $I("tblProyecto"); break;
            case 17: oTabla = $I("tblProveedores"); break;
        }
        if (nTipo != 1){
            slValores=fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }
        //var ret = window.showModalDialog(strEnlace, self, sTamano);
        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function(ret) {
	            if (ret != null){
                    var aElementos = ret.split("///");
                    switch (nTipo)
                    {
                        case 1: 
                            nNivelEstructura = parseInt(aElementos[0], 10);
                            nNivelSeleccionado = parseInt(aElementos[0], 10);
                            BorrarFilasDe("tblAmbito");
                            //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
                            for (var i=1; i<aElementos.length; i++){
                                if (aElementos[i]=="") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblAmbito").insertRow(-1);
                                oNF.setAttribute("tipo", aDatos[0]);
                                var aID = aDatos[1].split("-");
                                switch(parseInt(oNF.getAttribute("tipo"), 10)){
                                    case 1:
                                        oNF.insertCell(-1).appendChild(oImgSN4.cloneNode(true), null);
                                        oNF.id = aID[0];
                                        break;
                                    case 2:
                                        oNF.insertCell(-1).appendChild(oImgSN3.cloneNode(true), null);
                                        oNF.id = aID[1];
                                        break;
                                    case 3:
                                        oNF.insertCell(-1).appendChild(oImgSN2.cloneNode(true), null);
                                        oNF.id = aID[2];
                                        break;
                                    case 4:
                                        oNF.insertCell(-1).appendChild(oImgSN1.cloneNode(true), null);
                                        oNF.id = aID[3];
                                        break;
                                    case 5:
                                        oNF.insertCell(-1).appendChild(oImgNodo.cloneNode(true), null);
                                        oNF.id = aID[4];
                                        break;
                                    case 6:
                                        oNF.insertCell(-1).appendChild(oImgSubNodo.cloneNode(true), null);
                                        oNF.id = aID[5];
                                        break;
                                }
        //                        oNF.cells[0].appendChild(document.createElement("<nobr class='NBR W230' onmouseover='TTip(event)'></nobr>"));
        //                        oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[1].style.width = "230px";
                                oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                                oNF.cells[0].children[1].attachEvent("onmouseover", TTip);                        
                            }

                            divAmbito.scrollTop=0;
                            break;
                        case 2: 
                            insertarTabla(aElementos,"tblResponsable");
                            break;
                        case 3: 
                            insertarTabla(aElementos,"tblNaturaleza");    
                            break;
                        case 4: 
                            insertarTabla(aElementos,"tblModeloCon");    
                            break;
                        case 5: 
                            insertarTabla(aElementos,"tblHorizontal");    
                            break;
                        case 6: 
                            insertarTabla(aElementos,"tblSector");    
                            break;
                        case 7: 
                            insertarTabla(aElementos,"tblSegmento");    
                            break;
                        case 8: 
                            insertarTabla(aElementos,"tblCliente");   
                            break;
                        case 9: 
                            insertarTabla(aElementos,"tblContrato");                   
                            break;
                        case 10: 
                            insertarTabla(aElementos,"tblQn");                                   
                            break;
                        case 11: 
                            insertarTabla(aElementos,"tblQ1");                                                   
                            break;
                        case 12: 
                            insertarTabla(aElementos,"tblQ2");                                                                   
                            break;
                        case 13: 
                            insertarTabla(aElementos,"tblQ3");                                                                                   
                            break;
                        case 14:
                            insertarTabla(aElementos,"tblQ4");                                                                                                    
                            break;
                        case 16: 
                            BorrarFilasDe("tblProyecto");
                            for (var i=0; i<aElementos.length; i++){
                                if (aElementos[i]=="") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProyecto").insertRow(-1);
                                oNF.id = aDatos[0]; //  nproy-subnodo
                                oNF.style.height = "16px";
                                oNF.setAttribute("categoria", aDatos[2]);
                                oNF.setAttribute("cualidad", aDatos[3]);
                                oNF.setAttribute("estado", aDatos[4]);
                                oNF.insertCell(-1);
                                
                                if (aDatos[2]=="P") oNF.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                                else oNF.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                                
                                switch (aDatos[3]){
                                    case "C": oNF.cells[0].appendChild(oImgContratante.cloneNode(true), null); break;
                                    case "J": oNF.cells[0].appendChild(oImgRepJor.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                                }

                                switch (aDatos[4]){
                                    case "A": oNF.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                                    case "C": oNF.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                                    case "H": oNF.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                                }
                                
        //                        oNF.cells[0].appendChild(document.createElement("<nobr class='NBR W190' style='margin-left:3px;' onmouseover='TTip(event)'></nobr>"));
        //                        oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[3].className = "NBR";
                                oNF.cells[0].children[3].setAttribute("style", "width:190px; margin-left:3px;");
                                oNF.cells[0].children[3].attachEvent("onmouseover", TTip);
                                oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
                            }
                            divProyecto.scrollTop=0;
                            break;  
                        case 17:
                            insertarTabla(aElementos,"tblProveedores");                                                                                                    
                            break;                                     
                    }	        
                    setTodos();            
                    ocultarProcesando();
                }else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}
function insertarTabla(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "16px";
            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
            oNF.cells[0].children[0].className = "NBR";
            oNF.cells[0].children[0].setAttribute("style", "width:260px;");
            oNF.cells[0].children[0].attachEvent("onmouseover", TTip);
            
            oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}
function setTodos() {
    try{
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        setFilaTodos("cboCategoria", (sOL=="1")?true:false, false);
        setFilaTodos("cboCualidad", (sOL=="1")?true:false, false);
        setFilaTodos("tblAmbito", (sOL=="1")?true:false, true);
        setFilaTodos("tblSector", (sOL=="1")?true:false, true);
        setFilaTodos("tblResponsable", (sOL=="1")?true:false, true);
        setFilaTodos("tblSegmento", (sOL=="1")?true:false, true);
        setFilaTodos("tblProyecto", (sOL=="1")?true:false, true);        
        setFilaTodos("tblNaturaleza", (sOL=="1")?true:false, false);
        setFilaTodos("tblCliente", (sOL=="1")?true:false, true);
        setFilaTodos("tblModeloCon", (sOL=="1")?true:false, true);
        setFilaTodos("tblContrato", (sOL=="1")?true:false, true);
        setFilaTodos("tblHorizontal", (sOL=="1")?true:false, true);
        setFilaTodos("tblQn", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ1", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ2", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ3", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ4", (sOL=="1")?true:false, true);
        setFilaTodos("tblProveedores", (sOL=="1")?true:false, true);
        
//        $I("rdbFormato_0").checked = true;
//        $I("rdbOrdenacion_0").checked = true;
//        $I("rdbConsumos_0").checked = true;
//                     
//        $I("chkPropios").checked =  true;
//        $I("chkOtros").checked = true;
//        $I("chkExternos").checked = true;        
        
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
	}
}
function delCriterios(nTipo){
    try{
        //alert(nTipo);
        mostrarProcesando();
        switch (nTipo)
        {
            case 1: 
                    nNivelEstructura=0;
                    nNivelSeleccionado = 0;
                    BorrarFilasDe("tblAmbito"); 
                    js_subnodos.length = 0;
                    js_ValSubnodos.length = 0;
                    break;
            case 2: BorrarFilasDe("tblResponsable"); break;
            case 3: BorrarFilasDe("tblNaturaleza"); break;
            case 4: BorrarFilasDe("tblModeloCon"); break;
            case 5: BorrarFilasDe("tblHorizontal"); break;
            case 6: BorrarFilasDe("tblSector"); break;
            case 7: BorrarFilasDe("tblSegmento"); break;
            case 8: BorrarFilasDe("tblCliente"); break;
            case 9: BorrarFilasDe("tblContrato"); break;
            case 10: BorrarFilasDe("tblQn"); break;
            case 11: BorrarFilasDe("tblQ1"); break;
            case 12: BorrarFilasDe("tblQ2"); break;
            case 13: BorrarFilasDe("tblQ3"); break;
            case 14: BorrarFilasDe("tblQ4"); break;
            case 16: BorrarFilasDe("tblProyecto"); break;
            case 17: BorrarFilasDe("tblProveedores"); break;
        }
	        
        //borrarCatalogo();
        setTodos();                    
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function setCombo(){
    try{
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el "+ strEstructuraNodo, e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
           case "generarExcel":
               if (aResul[2] == "cacheado") {
                   var xls;
                   try {
                       xls = new ActiveXObject("Excel.Application");
                       crearExcel(aResul[4]);
                   } catch (e) {
                       crearExcelSimpleServerCache(aResul[3]);
                   }
               }
               else
                   crearExcel(aResul[2]);
               break;
            case "getTablaCriterios":
                mmoff("hide");
                eval(aResul[2]);
                bCargandoCriterios=false;
                
                iDesdeOld = $I("hdnDesde").value;
                iHastaOld = $I("hdnHasta").value;

                if (nCriterioAVisualizar!=0) getCriterios(nCriterioAVisualizar);
                break;                          
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.",200);
                break;
            case "getPreferencia":
                $I("cboCategoria").value = aResul[4];
                $I("cboCualidad").value = aResul[5];
                nUtilidadPeriodo = parseInt(aResul[9], 10);
                $I("hdnDesde").value = aResul[10];
                $I("txtDesde").value = aResul[11];
                $I("hdnHasta").value = aResul[12];
                $I("txtHasta").value = aResul[13];
                
                iDesdeOld = $I("hdnDesde").value;
                iHastaOld = $I("hdnHasta").value;	
                                
                js_subnodos.length = 0;
                js_subnodos = aResul[15].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[16], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[18], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[20], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[22], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblHorizontal");
                insertarFilasEnTablaDOM("tblHorizontal", aResul[24], 0);
                $I("divHorizontal").scrollTop = 0;

                BorrarFilasDe("tblSector");
                insertarFilasEnTablaDOM("tblSector", aResul[26], 0);
                $I("divSector").scrollTop = 0;

                BorrarFilasDe("tblSegmento");
                insertarFilasEnTablaDOM("tblSegmento", aResul[28], 0);
                $I("divSegmento").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[30], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[32], 0);
                $I("divContrato").scrollTop = 0;

                BorrarFilasDe("tblQn");
                insertarFilasEnTablaDOM("tblQn", aResul[34], 0);
                $I("divQn").scrollTop = 0;

                BorrarFilasDe("tblQ1");
                insertarFilasEnTablaDOM("tblQ1", aResul[36], 0);
                $I("divQ1").scrollTop = 0;

                BorrarFilasDe("tblQ2");
                insertarFilasEnTablaDOM("tblQ2", aResul[38], 0);
                $I("divQ2").scrollTop = 0;

                BorrarFilasDe("tblQ3");
                insertarFilasEnTablaDOM("tblQ3", aResul[40], 0);
                $I("divQ3").scrollTop = 0;

                BorrarFilasDe("tblQ4");
                insertarFilasEnTablaDOM("tblQ4", aResul[42], 0);
                $I("divQ4").scrollTop = 0;

                BorrarFilasDe("tblProveedores");
                insertarFilasEnTablaDOM("tblProveedores", aResul[44], 0);
                $I("divProveedor").scrollTop = 0;
                
                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[46], 0);
                $I("divProyecto").scrollTop = 0;

                if (aResul[48]=="1") $I("rdbFormato_0").checked = true;
                else $I("rdbFormato_1").checked = true;   

                if (aResul[8]=="1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;   

                setTodos();
                
                //if ($I("chkActuAuto").checked)
                //    setTimeout("buscar();", 20);
              
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function Control(obj,op)
{
}
function Obtener(){
    try{
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == ""){
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
            return;
        }    
        $I('imgImpresora').src='../../../../Images/imgImpresora.gif';
        setTimeout("$I('imgImpresora').src='../../../../Images/imgImpresorastop.gif';", 10000);
        if ($I('rdbFormato_0').checked == true) 
            Exportar('PDF');
        else 
            if ($I('rdbFormato_1').checked==true) generarExcel();
	}catch(e){
		mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}
function generarExcel(){
    try{
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;

        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("generarExcel@#@");
        sb.Append($I("hdnDesde").value +"@#@"); //sDesde
        sb.Append($I("hdnHasta").value +"@#@"); //sHasta
//        sb.Append(nNivelEstructura+"@#@"); //sNivelEstructura 
        sb.Append("7@#@"); //Ahora siempre hay que pasar el 7 (nivel de subnodo), ya que al seleccionar
        //el ámbito, se seleccionan todos los subnodos de la estructura seleccionada.
        sb.Append($I("cboCategoria").value +"@#@"); //sCategoria
        sb.Append($I("cboCualidad").value +"@#@"); //sCualidad
		sb.Append(getDatosTabla(16)+ "@#@"); //Proyectos		
        sb.Append(getDatosTabla(8)+ "@#@"); //Clientes
        sb.Append(getDatosTabla(2)+ "@#@"); //Responsable
        sb.Append(getDatosTabla(3)+ "@#@"); //Naturaleza
        sb.Append(getDatosTabla(5)+ "@#@"); //Horizontal
        sb.Append(getDatosTabla(4)+ "@#@"); //ModeloCon
        sb.Append(getDatosTabla(9)+ "@#@"); //Contrato
        sb.Append(js_subnodos.join(",")+ "@#@"); //ids estructura ambito
        sb.Append(getDatosTabla(6)+ "@#@"); //Sector
        sb.Append(getDatosTabla(7)+ "@#@"); //Segmento
        sb.Append(getDatosTabla(17)+ "@#@"); //Proveedores
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false)+ "@#@"); //Operador lógico
        sb.Append(getDatosTabla(10)+ "@#@"); //CNP
        sb.Append(getDatosTabla(11)+ "@#@"); //CSN1P
        sb.Append(getDatosTabla(12)+ "@#@"); //CSN2P
        sb.Append(getDatosTabla(13)+ "@#@"); //CSN3P
        sb.Append(getDatosTabla(14)+ "@#@"); //CSN4P
        sb.Append(location.href.substring(0,location.href.length-13),+"@#@");//URL para imágenes
      
        RealizarCallBack(sb.ToString(), "");
        //borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}
function Exportar(strFormato){
    try{     
		$I("FORMATO").value = strFormato; 
//		$I('hdnNivelEstructura').value = nNivelEstructura;
		$I('hdnNivelEstructura').value = "7"; //Ahora siempre hay que pasar el 7 (nivel de subnodo), ya que al seleccionar
        //el ámbito, se seleccionan todos los subnodos de la estructura seleccionada.
		$I('hdnProyectos').value = getDatosTabla(16);
		$I('hdnClientes').value = getDatosTabla(8);
		$I('hdnResponsables').value = getDatosTabla(2);
		$I('hdnNaturalezas').value = getDatosTabla(3);
		$I('hdnHorizontales').value = getDatosTabla(5);
		$I('hdnModeloCons').value = getDatosTabla(4);
		$I('hdnContratos').value = getDatosTabla(9);
		$I('hdnEstrucAmbitos').value = js_subnodos.join(",");
		$I('hdnSectores').value = getDatosTabla(6);
		$I('hdnSegmentos').value = getDatosTabla(7);
		$I('hdnProveedores').value = getDatosTabla(17);
		
		$I('hdnCNP').value = getDatosTabla(10);
		$I('hdnCSN1P').value = getDatosTabla(11);
		$I('hdnCSN2P').value = getDatosTabla(12);
		$I('hdnCSN3P').value = getDatosTabla(13);
		$I('hdnCSN4P').value = getDatosTabla(14);

        //*SSRS
	     
		nDesde = ($I("hdnHasta").value).substring(0, 4) + "-" + ($I("hdnHasta").value).substring(4, 6) + "-01";

		var hastaDate = new Date(($I("hdnHasta").value).substring(0, 4), ($I("hdnHasta").value).substring(4, 6), 0);
		nHasta = hastaDate.getFullYear() + "-" + (hastaDate.getMonth() + 1) + "-" + hastaDate.getDate();

		if (getRadioButtonSelectedValue("rdbOperador") == 1) rdboperador = true;
		else rdboperador = false;

		var params = {
		    reportName: "/SUPER/sup_consuproveed",
		    tipo: "PDF",
		    NODO: NODO,
		    t314_idusuario: usuario,
		    nDesde: nDesde,
		    nHasta: nHasta,
		    FechaDesde: $I("txtDesde").value,
		    FechaHasta: $I("txtHasta").value,
		    nNivelEstructura: $I("hdnNivelEstructura").value,
		    t301_categoria: $I("cboCategoria").value,
		    t305_cualidad: $I("cboCualidad").value,
		    sProyectos: $I("hdnProyectos").value,
		    sClientes: $I("hdnClientes").value,
		    sResponsables: $I("hdnResponsables").value,
		    sNaturalezas: $I("hdnNaturalezas").value,
		    sHorizontal: $I("hdnHorizontales").value,
		    sModeloContrato: $I("hdnModeloCons").value,
		    sContrato: $I("hdnContratos").value,
		    sIDEstructura: $I("hdnEstrucAmbitos").value,
		    sSectores: $I("hdnSectores").value,
		    sSegmentos: $I("hdnSegmentos").value,
		    sProveedores: $I("hdnProveedores").value,
		    bComparacionLogica: rdboperador,
		    sCNP: $I("hdnCNP").value,
		    sCSN1P: $I("hdnCSN1P").value,
		    sCSN2P: $I("hdnCSN2P").value,
		    sCSN3P: $I("hdnCSN3P").value,
		    sCSN4P: $I("hdnCSN4P").value,
		    t422_idmoneda: t422_idmoneda,
		    ImportesEn: ImportesEn
		}

		PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR
        document.forms["aspnetForm"].action="Exportar/default.aspx";
		document.forms["aspnetForm"].target="_blank";
		document.forms["aspnetForm"].submit();
        //CR*/
		
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}               
function getDatosTabla(nTipo){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo)
        {
            case 1: oTabla = $I("tblAmbito"); break;
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 5: oTabla = $I("tblHorizontal"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 10: oTabla = $I("tblQn"); break;
            case 11: oTabla = $I("tblQ1"); break;
            case 12: oTabla = $I("tblQ2"); break;
            case 13: oTabla = $I("tblQ3"); break;
            case 14: oTabla = $I("tblQ4"); break;
            case 16: oTabla = $I("tblProyecto"); break;
            case 17: oTabla = $I("tblProveedores"); break;
        }
        
        for (var i=0; i<oTabla.rows.length;i++){
            if (oTabla.rows[i].id == "-999") continue;
            if (i>0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }
        
        if (sb.ToString().length > 8000)
        {
            ocultarProcesando();
            switch (nTipo)
            {
                //case 1: break;
                case 2: mmoff("Inf", "Has seleccionado un número excesivo de responsables de proyecto.", 500); break;
                case 3: mmoff("Inf", "Has seleccionado un número excesivo de naturalezas.", 450); break;
                case 4: mmoff("Inf", "Has seleccionado un número excesivo de modelos de contratación.", 500); break;
                case 5: mmoff("Inf", "Has seleccionado un número excesivo de horizontales.", 450); break;
                case 6: mmoff("Inf", "Has seleccionado un número excesivo de sectores.", 450); break;
                case 7: mmoff("Inf", "Has seleccionado un número excesivo de segmentos.", 450); break;
                case 8: mmoff("Inf", "Has seleccionado un número excesivo de clientes.", 450); break;
                case 9: mmoff("Inf", "Has seleccionado un número excesivo de contratos.", 450); break;
                case 10: mmoff("Inf", "Has seleccionado un número excesivo de Qn.", 400); break;
                case 11: mmoff("Inf", "Has seleccionado un número excesivo de Q1.", 400); break;
                case 12: mmoff("Inf", "Has seleccionado un número excesivo de Q2.", 400); break;
                case 13: mmoff("Inf", "Has seleccionado un número excesivo de Q3.", 400); break;
                case 14: mmoff("Inf", "Has seleccionado un número excesivo de Q4.", 400); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos.", 450); break;
                case 17: mmoff("Inf", "Has seleccionado un número excesivo de proveedores.", 450); break;
            }
            return;   
		}
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}
function setPreferencia(){
    try{
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;            
    
        mostrarProcesando();
        
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("setPreferencia@#@");
        sb.Append($I("cboCategoria").value +"@#@");
        sb.Append($I("cboCualidad").value +"@#@");
        sb.Append(nUtilidadPeriodo +"@#@");
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) +"@#@");
        sb.Append(getRadioButtonSelectedValue("rdbFormato", false) +"@#@");
        
        sb.Append(getValoresMultiples());
       
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function delPreferencia() {
    try {
        jqConfirm("", "Pulsa ACEPTAR para confirmar la eliminación de todas las preferencias de esta pantalla.", "", "", "war", 450).then(function (answer) {
            if (answer) {
                document.forms["aspnetForm"].action = strAction;
                document.forms["aspnetForm"].target = strTarget;

                mostrarProcesando();
                var js_args = "delPreferencia@#@";
                RealizarCallBack(js_args, "");
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a eliminar la preferencia", e.message);
    }
}
function getCatalogoPreferencias(){
    try{    
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;        
        mostrarProcesando();
        //var ret = window.showModalDialog("../../../getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
	        .then(function(ret) {
	            if (ret != null){
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    //borrarCatalogo();
	            }else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

function getValoresMultiples(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n=1; n<=19; n++){
            if (n==15) continue;
            switch (n)
            {
                case 1: oTabla = $I("tblAmbito"); break;
                case 2: oTabla = $I("tblResponsable"); break;
                case 3: oTabla = $I("tblNaturaleza"); break;
                case 4: oTabla = $I("tblModeloCon"); break;
                case 5: oTabla = $I("tblHorizontal"); break;
                case 6: oTabla = $I("tblSector"); break;
                case 7: oTabla = $I("tblSegmento"); break;
                case 8: oTabla = $I("tblCliente"); break;
                case 9: oTabla = $I("tblContrato"); break;
                case 10: oTabla = $I("tblQn"); break;
                case 11: oTabla = $I("tblQ1"); break;
                case 12: oTabla = $I("tblQ2"); break;
                case 13: oTabla = $I("tblQ3"); break;
                case 14: oTabla = $I("tblQ4"); break;
                case 16: oTabla = $I("tblProyecto"); break;
                case 19: oTabla = $I("tblProveedores"); break;
            }
        
            for (var i=0; i<oTabla.rows.length;i++){
                if (oTabla.rows[i].id == "-999") continue;
                if (n==1){
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n +"##"+ oTabla.rows[i].getAttribute("tipo")+"-"+oTabla.rows[i].id +"##"+ Utilidades.escape(oTabla.rows[i].innerText));
                }else{
                    if (n==16){
                        if (sb.buffer.length>0) sb.Append("///");
                        sb.Append(n +"##"+ oTabla.rows[i].id+"-"+oTabla.rows[i].getAttribute("categoria")+"-"+oTabla.rows[i].getAttribute("cualidad")+"-"+oTabla.rows[i].getAttribute("estado") +"##"+ Utilidades.escape(oTabla.rows[i].innerText));
                    }else{
                        if (sb.buffer.length>0) sb.Append("///");
                        sb.Append(n +"##"+ oTabla.rows[i].id +"##"+ Utilidades.escape(oTabla.rows[i].innerText));
                    }
                }
            }
        }
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}
function Limpiar()
{
    nNivelEstructura=0;
    nNivelSeleccionado = 0;
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;

    var aTable = $I('tblCriterios').getElementsByTagName("TABLE");       
    for (var i=0; i<aTable.length; i++){
        if (aTable[i].id.substring(0,3) != "tbl") continue;
        BorrarFilasDe(aTable[i].id);
    }

    $I("rdbOperador_0").checked=true;
    $I("cboCategoria").value="0";
    $I("cboCualidad").value="0"; 
    setTodos();
}

function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDC";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("lblMonedaImportes").innerText = aDatos[1];
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}