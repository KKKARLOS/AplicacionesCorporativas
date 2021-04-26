var js_meses = new Array();
var myChart1;// = new FusionCharts("../../../Graficos/FCF_MSLine.swf", "myChartId", "1000", "350");
var myChart2;
var myChart3;
var myChart4;
var myChartD1;
var myChartD2;
var myChartD3;
var myChartD4;
var myChartD5;
var myChartD6;
var myChartD7;
var myChartD8;

var nValorMinimoGraf1 = 0;
var nValorMaximoGraf1 = 0;
var nValorMinimoGraf2 = 0;
var nValorMaximoGraf2 = 0;
var nValorMinimoGraf3 = 0;
var nValorMaximoGraf3 = 0;
var nValorMinimoGraf4 = 0;
var nValorMaximoGraf4 = 0;

var nCriterioAVisualizar = 0;
var js_subnodos = new Array();
var bPeriodoModificado = false;
var bCargandoCriterios=false;
/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 500;
var nTopPestana = 98;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

var iAno = "";

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

function init(){
    try{
//        $I("procesando").style.top = 110;
//        $I("procesando").style.left = 410;
        if (nName != "chrome" && nName != "ie") {
            //08/01/2013 Los gráficos en Flash solo funcionan en IE y Chrome
            //Hasta nueva orden los que hacemos es mostrar página de obras si no es ninguno de esos navegadores
            location.href = strServer + "Capa_Presentacion/Ayuda/Obras/Default.aspx";
            return;
        }
        $I("imgPestHorizontalAux").style.visibility = "visible";
        $I("divPestRetr").style.visibility = "visible";
        $I("tblGeneral").style.visibility = "visible";
        
        iAno = $I("txtAnno").value;
        setOperadorLogico(false);

        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, tblAmbito);
            js_ValSubnodos = slValores.split("///");
        }
        
        resetDatos();

        if (bHayPreferencia){
            buscar();
            setTimeout("mostrarProcesando();", 50);
        }else mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function mostrarOcultarPestVertical(){
    if (!bPestRetrMostrada){
        $I("tblGeneral").style.visibility = "hidden";
        mostrarCriterios();
    }else{
        ocultarCriterios();
        $I("tblGeneral").style.visibility = "visible";
    }
    bPestRetrMostrada = !bPestRetrMostrada;
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultar = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                bOcultar = false;
                setDatos(aResul[2]);
                break;
            case "getTablaCriterios":
                mmoff("hide");
                eval(aResul[2]);
                bCargandoCriterios=false;
                
                iAno = $I("txtAnno").value;
                
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
                $I("cboEstado").value = aResul[3];
                $I("cboCategoria").value = aResul[4];
                $I("cboCualidad").value = aResul[5];
                $I("chkCerrarAuto").checked = (aResul[6]=="1")? true:false;
                $I("chkActuAuto").checked = (aResul[7]=="1")? true:false;
                js_subnodos.length = 0;
                js_subnodos = aResul[9].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[10], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[12], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[14], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[16], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblHorizontal");
                insertarFilasEnTablaDOM("tblHorizontal", aResul[18], 0);
                $I("divHorizontal").scrollTop = 0;

                BorrarFilasDe("tblSector");
                insertarFilasEnTablaDOM("tblSector", aResul[20], 0);
                $I("divSector").scrollTop = 0;

                BorrarFilasDe("tblSegmento");
                insertarFilasEnTablaDOM("tblSegmento", aResul[22], 0);
                $I("divSegmento").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[24], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[26], 0);
                $I("divContrato").scrollTop = 0;

                BorrarFilasDe("tblQn");
                insertarFilasEnTablaDOM("tblQn", aResul[28], 0);
                $I("divQn").scrollTop = 0;

                BorrarFilasDe("tblQ1");
                insertarFilasEnTablaDOM("tblQ1", aResul[30], 0);
                $I("divQ1").scrollTop = 0;

                BorrarFilasDe("tblQ2");
                insertarFilasEnTablaDOM("tblQ2", aResul[32], 0);
                $I("divQ2").scrollTop = 0;

                BorrarFilasDe("tblQ3");
                insertarFilasEnTablaDOM("tblQ3", aResul[34], 0);
                $I("divQ3").scrollTop = 0;

                BorrarFilasDe("tblQ4");
                insertarFilasEnTablaDOM("tblQ4", aResul[36], 0);
                $I("divQ4").scrollTop = 0;
                
                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[38], 0);
                $I("divProyecto").scrollTop = 0;
                
                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[8]=="1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;

                setTodos();

                if ($I("chkActuAuto").checked){
                    bOcultar = false;
                    setTimeout("buscar();", 20);
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultar) ocultarProcesando();
    }
}

function getTablaCriterios(){
    try{
        mostrarProcesando();
        var js_args = "getTablaCriterios@#@";
        js_args += $I("txtAnno").value;
        bCargandoCriterios=true;
        RealizarCallBack(js_args, "");
        js_cri.length = 0;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los nuevos criterios", e.message);
    }
}

function setCombo(){
    try{
        if ($I("chkActuAuto").checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el criterio.", e.message);
    }
}

function buscar(){
    try{
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == ""){
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
            return;
        }
                
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("txtAnno").value +"@#@");
        sb.Append($I("cboEstado").value +"@#@");
        sb.Append($I("cboCategoria").value +"@#@");
        sb.Append($I("cboCualidad").value +"@#@");
        sb.Append(getDatosTabla(8)+ "@#@"); //Clientes
        sb.Append(getDatosTabla(2)+ "@#@"); //Responsable
        sb.Append(getDatosTabla(3)+ "@#@"); //Naturaleza
        sb.Append(getDatosTabla(5)+ "@#@"); //Horizontal
        sb.Append(getDatosTabla(4)+ "@#@"); //ModeloCon
        sb.Append(getDatosTabla(9)+ "@#@"); //Contrato
        sb.Append(js_subnodos.join(",")+ "@#@"); //ids estructura ambito
        sb.Append(getDatosTabla(6)+ "@#@"); //Sector
        sb.Append(getDatosTabla(7)+ "@#@"); //Segmento
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false)+ "@#@"); //Operador lógico
        sb.Append(getDatosTabla(10)+ "@#@"); //CNP
        sb.Append(getDatosTabla(11)+ "@#@"); //CSN1P
        sb.Append(getDatosTabla(12)+ "@#@"); //CSN2P
        sb.Append(getDatosTabla(13)+ "@#@"); //CSN3P
        sb.Append(getDatosTabla(14)+ "@#@"); //CSN4P
        sb.Append(getDatosTabla(16)+ "@#@"); //ProyectoSubnodos
       
        if ($I("chkCerrarAuto").checked){
            bPestRetrMostrada = true;
            mostrarOcultarPestVertical();
        }
       
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
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
            }
            return;   
		}
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
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
        var slValores="";
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

        mostrarProcesando();
        var oTabla;
        var strEnlace = "";
        var sTamano = sSize(850, 430);
        
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
                sTamano = "dialogwidth:950px; dialogheight:450px; center:yes; status:NO; help:NO;";
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
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[1].style.width = "230px";
                                oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                            }
                            divAmbito.scrollTop=0;
                            break;
                        case 2: insertarTabla(aElementos,"tblResponsable"); break;
                        case 3: insertarTabla(aElementos,"tblNaturaleza"); break;
                        case 4: insertarTabla(aElementos,"tblModeloCon"); break;
                        case 5: insertarTabla(aElementos,"tblHorizontal"); break;
                        case 6: insertarTabla(aElementos,"tblSector"); break;
                        case 7: insertarTabla(aElementos,"tblSegmento"); break;
                        case 8: insertarTabla(aElementos,"tblCliente"); break;
                        case 9: insertarTabla(aElementos,"tblContrato"); break;
                        case 10: insertarTabla(aElementos,"tblQn"); break;
                        case 11: insertarTabla(aElementos,"tblQ1"); break;
                        case 12: insertarTabla(aElementos,"tblQ2"); break;
                        case 13: insertarTabla(aElementos,"tblQ3"); break;
                        case 14: insertarTabla(aElementos,"tblQ4"); break;
                        case 16: 
                            BorrarFilasDe("tblProyecto");
                            for (var i=0; i<aElementos.length; i++){
                                if (aElementos[i]=="") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProyecto").insertRow(-1);
                                oNF.id = aDatos[0];
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
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[3].className = "NBR";
                                oNF.cells[0].children[3].setAttribute("style", "width:190px; margin-left:3px;");
                                oNF.cells[0].children[3].attachEvent("onmouseover", TTip);
                                oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
                            }
                            divProyecto.scrollTop=0;
                            break;                    
                    }	        
                    setTodos();            
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
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
        }
	        
        setTodos();            
        
        if ($I("chkActuAuto").checked){
            buscar();
        }else ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

var sOLAnterior = "";
function setOperadorLogico(bBuscar){
    try{
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        if (sOL == sOLAnterior) return;
        else sOLAnterior = sOL;
        
        setTodos();

        if ($I("chkActuAuto").checked){
            if (bBuscar) buscar();
        }
        
    }catch(e){
		mostrarErrorAplicacion("Error al modificar el operador lógico.", e.message);
	}
}
function setTodos(){
    try{
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        setFilaTodos("cboEstado", (sOL=="1")?true:false, true);
        setFilaTodos("cboCategoria", (sOL=="1")?true:false, false);
        setFilaTodos("cboCualidad", (sOL=="1")?true:false, false);
        setFilaTodos("tblAmbito", (sOL=="1")?true:false, true);
        setFilaTodos("tblSector", (sOL=="1")?true:false, true);
        setFilaTodos("tblResponsable", (sOL=="1")?true:false, true);
        setFilaTodos("tblSegmento", (sOL=="1")?true:false, true);
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
        setFilaTodos("tblProyecto", (sOL=="1")?true:false, true);
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
	}
}

function setPreferencia(){
    try{
        mostrarProcesando();
        
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("setPreferencia@#@");
        sb.Append($I("cboEstado").value +"@#@");
        sb.Append($I("cboCategoria").value +"@#@");
        sb.Append($I("cboCualidad").value +"@#@");
        sb.Append(($I("chkCerrarAuto").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkActuAuto").checked)? "1@#@":"0@#@");
        //sb.Append(nUtilidadPeriodo +"@#@");
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) +"@#@");
        sb.Append(getValoresMultiples());
       
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function getCatalogoPreferencias(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
	        .then(function(ret) {
	            if (ret != null){
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
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
        for (var n=1; n<=16; n++){
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

function setAnno(sOpcion){
    try{
        if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
        else if (sOpcion == "S") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;

        
        var iProc = 0;
        if  (iAno != $I("txtAnno").value) 
        {
            getTablaCriterios();
            iProc=1;
        }
        
        if ($I("chkActuAuto").checked){
            //buscar();
            buscarDiferido();
            //bPeriodoModificado = true;
        }else{
            if (iProc==0) ocultarProcesando();
            //getTablaCriterios();
        }        
    }catch(e){
        mostrarErrorAplicacion("Error al establecer el año", e.message);
    }
}
var nIDTimeBuscar = null;
function buscarDiferido(){
    try{
        if (bCargandoCriterios){
            nIDTimeBuscar = setTimeout("buscarDiferido()", 20);
        }
        else{
            clearTimeout(nIDTimeBuscar);
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al buscar comprobando los criterios", e.message);
    }
}

function setDatos(sResul){
    try{
        //alert(sResul);
        resetDatos();
        var aDatos = sResul.split("///");
        for (var i=0; i<aDatos.length-1; i++){
            var aDatosMes = aDatos[i].split("##");
            var nMes = parseInt(aDatosMes[0].substring(4,6), 10);
            js_meses[nMes-1] = {"anomes":aDatosMes[0],
            
                            "Total_Usuarios_Propios":aDatosMes[1],
                            "Usuarios_Propios":aDatosMes[2],
                            "Horas_Propios":aDatosMes[3],
                            "Coste_Horas_Propios":aDatosMes[4],
                            "Jornadas_Propios":aDatosMes[5],
                            "Coste_Jornadas_Propios":aDatosMes[6],
                           
                            "Total_Usuarios_Otros_nodos":aDatosMes[7],
                            "Usuarios_Otros_nodos":aDatosMes[8],
                            "Horas_Otros_nodos":aDatosMes[9],
                            "Coste_Horas_Otros_nodos":aDatosMes[10],
                            "Jornadas_Otros_nodos":aDatosMes[11],
                            "Coste_Jornadas_Otros_nodos":aDatosMes[12],
                            
                            "Total_Usuarios_Externos":aDatosMes[13],
                            "Usuarios_Externos":aDatosMes[14],
                            "Horas_Externos":aDatosMes[15],
                            "Coste_Horas_Externos":aDatosMes[16],
                            "Jornadas_Externos":aDatosMes[17],
                            "Coste_Jornadas_Externos":aDatosMes[18],
                            "Total_Consumo":aDatosMes[19]};

            if (parseFloat(aDatosMes[2]) < nValorMinimoGraf1) nValorMinimoGraf1 = parseFloat(aDatosMes[2]);
            if (parseFloat(aDatosMes[2]) > nValorMaximoGraf1) nValorMaximoGraf1 = parseFloat(aDatosMes[2]);
            if (parseFloat(aDatosMes[8]) < nValorMinimoGraf1) nValorMinimoGraf1 = parseFloat(aDatosMes[8]);
            if (parseFloat(aDatosMes[8]) > nValorMaximoGraf1) nValorMaximoGraf1 = parseFloat(aDatosMes[8]);
            if (parseFloat(aDatosMes[14]) < nValorMinimoGraf1) nValorMinimoGraf1 = parseFloat(aDatosMes[14]);
            if (parseFloat(aDatosMes[14]) > nValorMaximoGraf1) nValorMaximoGraf1 = parseFloat(aDatosMes[14]);
                
            if (parseFloat(aDatosMes[6]) < nValorMinimoGraf2) nValorMinimoGraf2 = parseFloat(aDatosMes[6]);
            if (parseFloat(aDatosMes[6]) > nValorMaximoGraf2) nValorMaximoGraf2 = parseFloat(aDatosMes[6]);
            if (parseFloat(aDatosMes[12]) < nValorMinimoGraf2) nValorMinimoGraf2 = parseFloat(aDatosMes[12]);
            if (parseFloat(aDatosMes[12]) > nValorMaximoGraf2) nValorMaximoGraf2 = parseFloat(aDatosMes[12]);
            if (parseFloat(aDatosMes[18]) < nValorMinimoGraf2) nValorMinimoGraf2 = parseFloat(aDatosMes[18]);
            if (parseFloat(aDatosMes[18]) > nValorMaximoGraf2) nValorMaximoGraf2 = parseFloat(aDatosMes[18]);

            if (parseFloat(aDatosMes[6]) < nValorMinimoGraf3) nValorMinimoGraf3 = parseFloat(aDatosMes[6]);
            if (parseFloat(aDatosMes[6]) > nValorMaximoGraf3) nValorMaximoGraf3 = parseFloat(aDatosMes[6]);
            if (parseFloat(aDatosMes[12]) < nValorMinimoGraf3) nValorMinimoGraf3 = parseFloat(aDatosMes[12]);
            if (parseFloat(aDatosMes[12]) > nValorMaximoGraf3) nValorMaximoGraf3 = parseFloat(aDatosMes[12]);
            if (parseFloat(aDatosMes[18]) < nValorMinimoGraf3) nValorMinimoGraf3 = parseFloat(aDatosMes[18]);
            if (parseFloat(aDatosMes[18]) > nValorMaximoGraf3) nValorMaximoGraf3 = parseFloat(aDatosMes[18]);

            if (parseFloat(aDatosMes[4]) < nValorMinimoGraf4) nValorMinimoGraf4 = parseFloat(aDatosMes[4]);
            if (parseFloat(aDatosMes[4]) > nValorMaximoGraf4) nValorMaximoGraf4 = parseFloat(aDatosMes[4]);
            if (parseFloat(aDatosMes[10]) < nValorMinimoGraf4) nValorMinimoGraf4 = parseFloat(aDatosMes[10]);
            if (parseFloat(aDatosMes[10]) > nValorMaximoGraf4) nValorMaximoGraf4 = parseFloat(aDatosMes[10]);
            if (parseFloat(aDatosMes[16]) < nValorMinimoGraf4) nValorMinimoGraf4 = parseFloat(aDatosMes[16]);
            if (parseFloat(aDatosMes[16]) > nValorMaximoGraf4) nValorMaximoGraf4 = parseFloat(aDatosMes[16]);
        }
        generarGraficos();
	}catch(e){
		mostrarErrorAplicacion("Error al recibir los datos", e.message);
    }
}

function resetDatos(){
    try{
        nValorMinimoGraf1 = 0;
        nValorMaximoGraf1 = 0;
        nValorMinimoGraf2 = 0;
        nValorMaximoGraf2 = 0;
        nValorMinimoGraf3 = 0;
        nValorMaximoGraf3 = 0;
        nValorMinimoGraf4 = 0;
        nValorMaximoGraf4 = 0;

        for (var i=0; i<12; i++){
            js_meses[i] = {"anomes":0,
                            "Total_Usuarios_Propios":"0",
                            "Usuarios_Propios":"0",
                            "Horas_Propios":"0",
                            "Coste_Horas_Propios":"0",
                            "Jornadas_Propios":"0",
                            "Coste_Jornadas_Propios":"0",
                            "Total_Usuarios_Otros_nodos":"0",
                            "Usuarios_Otros_nodos":"0",
                            "Horas_Otros_nodos":"0",
                            "Coste_Horas_Otros_nodos":"0",
                            "Jornadas_Otros_nodos":"0",
                            "Coste_Jornadas_Otros_nodos":"0",
                            "Total_Usuarios_Externos":"0",
                            "Usuarios_Externos":"0",
                            "Horas_Externos":"0",
                            "Coste_Jornadas_Externos":"0",
                            "Jornadas_Externos":"0",
                            "Coste_Jornadas_Externos":"0",
                            "Total_Consumo":"0"};
        }
	}catch(e){
		mostrarErrorAplicacion("Error al resetear los datos", e.message);
    }
}

function generarGraficos(){
    try{
        generarGraficoD1();
        generarGraficoD2();
        generarGrafico1();
        
        generarGraficoD3();
        generarGraficoD4();
        generarGrafico2();

        generarGraficoD5();
        generarGraficoD6();
        generarGrafico3();

        generarGraficoD7();
        //generarGraficoD8();
        generarGrafico4();

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a generar los gráficos", e.message);
    }
}

function generarGraficoD1(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;

        sb.Append("<graph ");//caption='Resumen económico' ");
        sb.Append("     caption='Anual'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     nameTBDistance='-20'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");

        var nInternos = parseInt(js_meses[0].Total_Usuarios_Propios, 10) + parseInt(js_meses[0].Total_Usuarios_Otros_nodos, 10);
        var nExternos = parseInt(js_meses[0].Total_Usuarios_Externos, 10);

        if (nInternos == 0 && nExternos == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Internos' value='"+ nInternos +"' color='3f7091' />");
            sb.Append("<set name='Externos' value='"+ nExternos +"' color='7c6c44' />");
        }

        sb.Append("</graph>");

        if (bHayDatos){
            myChartD1 = new FusionCharts("../../../Graficos/FCF_Doughnut2D.swf", "myChartIdD1", "100", "115");
            myChartD1.setDataXML(sb.ToString());
            myChartD1.render("chartdivD1");
        }else{
            $I("chartdivD1").className = "texto";
            $I("chartdivD1").innerText = "No hay datos";
        }        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D1", e.message);
    }
}
function generarGraficoD2(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;

        sb.Append("<graph ");
        sb.Append("     caption='Anual'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     nameTBDistance='-20'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");

        var nInternos = parseInt(js_meses[0].Total_Usuarios_Propios, 10);
        var nOtrosNodos = parseInt(js_meses[0].Total_Usuarios_Otros_nodos, 10);
        var nExternos = parseInt(js_meses[0].Total_Usuarios_Externos, 10);


        if (nInternos == 0 && nOtrosNodos == 0 && nExternos == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Propios' value='"+ nInternos +"' color='3f7091' />");
            sb.Append("<set name='Otro "+ strEstructuraNodo +"' value='"+ nOtrosNodos +"' color='f3cc0b' />");
            sb.Append("<set name='Externos' value='"+ nExternos +"' color='7c6c44' />");
        }


        sb.Append("</graph>");

        if (bHayDatos){
            myChartD2 = new FusionCharts("../../../Graficos/FCF_Doughnut2D.swf", "myChartIdD2", "100", "115");
            myChartD2.setDataXML(sb.ToString());
            myChartD2.render("chartdivD2");
        }else{
            $I("chartdivD2").className = "texto";
            $I("chartdivD2").innerText = "No hay datos";
        }        
        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D1", e.message);
    }
}
function generarGrafico1(){
    try{
        var sb = new StringBuilder;
        //if (nValorMinimoGraf1==0) nValorMinimoGraf1 = -110;
        if (nValorMaximoGraf1==0) nValorMaximoGraf1 = 90;

        sb.Append("<graph ");
        sb.Append("     caption='Profesionales con unidades económicas consumidas (Número)' ");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     showvalues='0'");
        sb.Append("     numdivlines='3'");
        sb.Append("     numVdivlines='0'");
        sb.Append("     formatNumber='1'");
        sb.Append("     decimalSeparator='.'");
        sb.Append("     thousandSeparator=','");
        sb.Append("     rotateNames='1'");
        sb.Append("     yaxisminvalue='0'");
        //sb.Append("     yaxisminvalue='"+ parseInt(nValorMinimoGraf1 - nValorMinimoGraf1 / 10, 10) +"'");
        sb.Append("     yaxismaxvalue='"+ parseInt(nValorMaximoGraf1 + nValorMaximoGraf1 / 10, 10) +"'");
        sb.Append("     bgColor='ecf0ee'>");

        sb.Append("<categories>");
        for (var i=0; i<12; i++) sb.Append("<category name='"+ aMes[i] +"' />");
        sb.Append("</categories>");

        sb.Append("<dataset seriesName='Propios' color='3f7091' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Usuarios_Propios +"' />");
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Otro  "+ strEstructuraNodo +"' color='f3cc0b' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Usuarios_Otros_nodos +"' />");
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Externos' color='7c6c44' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Usuarios_Externos +"' />");
        sb.Append("</dataset>");

        sb.Append("</graph>");

        myChart1 = new FusionCharts("../../../Graficos/FCF_MSColumn2D.swf", "myChartId1", "370", "230");
        myChart1.setDataXML(sb.ToString());
        myChart1.render("chartdiv1");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 1", e.message);
    }
}

function generarGraficoD3(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;
        var nTBDistance = -45;

        var nInternos = 0;
        var nExternos = 0;

        for (var i=0; i<js_meses.length; i++){
            nInternos += parseFloat(js_meses[i].Jornadas_Propios) + parseFloat(js_meses[i].Horas_Propios) + parseFloat(js_meses[i].Jornadas_Otros_nodos) + parseFloat(js_meses[i].Horas_Otros_nodos);
            nExternos += parseFloat(js_meses[i].Jornadas_Externos) + parseFloat(js_meses[i].Horas_Externos);
        }
        if (nInternos > 9999999 || nExternos > 9999999)
            nTBDistance = -60; 

        sb.Append("<graph ");
        sb.Append("     caption='U.E.A.'");
        sb.Append("     hovercapbg='E0E6EA' ");
        sb.Append("     hovercapborder='3F7091' ");
        sb.Append("     nameTBDistance='"+ nTBDistance +"'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");
        
        if (nInternos == 0 && nExternos == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Internos' value='"+ nInternos +"' color='3f7091' />");
            sb.Append("<set name='Externos' value='"+ nExternos +"' color='7c6c44' />");
        }
        sb.Append("</graph>");

        if (bHayDatos){
            myChartD3 = new FusionCharts("../../../Graficos/FCF_Doughnut2D.swf", "myChartIdD3", "100", "115");
            myChartD3.setDataXML(sb.ToString());
            myChartD3.render("chartdivD3");
        }else{
            $I("chartdivD3").className = "texto";
            $I("chartdivD3").innerText = "No hay datos";
        }        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D3", e.message);
    }
}

function generarGraficoD4(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;
        var nTBDistance = -45;
        var nInternos = 0;
        var nExternos = 0;

        for (var i=0; i<js_meses.length; i++){
            nInternos += parseFloat(js_meses[i].Coste_Jornadas_Propios) + parseFloat(js_meses[i].Coste_Horas_Propios) + parseFloat(js_meses[i].Coste_Jornadas_Otros_nodos) + parseFloat(js_meses[i].Coste_Horas_Otros_nodos);
            nExternos += parseFloat(js_meses[i].Coste_Jornadas_Externos) + parseFloat(js_meses[i].Coste_Horas_Externos);
        }
        if (nInternos > 9999999 || nExternos > 9999999)
            nTBDistance = -60; 
        
        
        sb.Append("<graph ");
        sb.Append("     caption='Coste anual'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     nameTBDistance='"+ nTBDistance +"'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");

        if (nInternos == 0 && nExternos == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Internos' value='"+ nInternos +"' color='3f7091' />");
            sb.Append("<set name='Externos' value='"+ nExternos +"' color='7c6c44' />");
        }
        sb.Append("</graph>");

        if (bHayDatos){
            myChartD4 = new FusionCharts("../../../Graficos/FCF_Doughnut2D.swf", "myChartIdD4", "100", "115");
            myChartD4.setDataXML(sb.ToString());
            myChartD4.render("chartdivD4");
        }else{
            $I("chartdivD4").className = "texto";
            $I("chartdivD4").innerText = "No hay datos";
        }        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D4", e.message);
    }
}

function generarGrafico2(){
    try{
        var sb = new StringBuilder;
        //if (nValorMinimoGraf2==0) nValorMinimoGraf2 = -110;
        if (nValorMaximoGraf2==0) nValorMaximoGraf2 = 90;

        sb.Append("<graph ");
        sb.Append("     caption='Coste de unidades económicas'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     hovercapbg='E0E6EA' ");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     showvalues='0'");
        sb.Append("     numdivlines='3'");
        sb.Append("     numVdivlines='0'");
        sb.Append("     formatNumber='1'");
        sb.Append("     decimalSeparator='.'");
        sb.Append("     thousandSeparator=','");
        sb.Append("     rotateNames='1'");
        sb.Append("     yaxisminvalue='0'");
        //sb.Append("     yaxisminvalue='"+ parseInt(nValorMinimoGraf2 - nValorMinimoGraf2 / 10, 10) +"'");
        sb.Append("     yaxismaxvalue='"+ parseInt(nValorMaximoGraf2 + nValorMaximoGraf2 / 10, 10) +"'");
        sb.Append("     bgColor='ecf0ee'>");

        sb.Append("<categories>");
        for (var i=0; i<12; i++) sb.Append("<category name='"+ aMes[i] +"' />");
        sb.Append("</categories>");

        sb.Append("<dataset seriesName='Internos' color='3f7091' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++){
            var nInternos = parseInt(js_meses[i].Coste_Jornadas_Propios, 10) + parseInt(js_meses[i].Coste_Horas_Propios, 10) + parseInt(js_meses[i].Coste_Jornadas_Otros_nodos, 10) + parseInt(js_meses[i].Coste_Horas_Otros_nodos, 10);
            sb.Append("<set value='"+ nInternos +"' />");
        }
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Externos' color='7c6c44' anchorBorderColor='ff3399' anchorBgColor='ff3399'>");
        for (var i=0; i<12; i++){
            var nExternos = parseInt(js_meses[i].Coste_Jornadas_Externos, 10) + parseInt(js_meses[i].Coste_Horas_Externos, 10);
            sb.Append("<set value='"+ nExternos +"' />");
        }
        sb.Append("</dataset>");

        sb.Append("</graph>");

        myChart2 = new FusionCharts("../../../Graficos/FCF_MSColumn2D.swf", "myChartId2", "370", "230");
        myChart2.setDataXML(sb.ToString());
        myChart2.render("chartdiv2");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 2", e.message);
    }
}

function generarGraficoD5(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;
        var nTBDistance = -45;
        var nInternos = 0;
        var nOtrosNodos = 0;
        var nExternos = 0;

        for (var i=0; i<js_meses.length; i++){
            nInternos += parseFloat(js_meses[i].Jornadas_Propios) + parseFloat(js_meses[i].Horas_Propios);
            nOtrosNodos += parseFloat(js_meses[i].Jornadas_Otros_nodos) + parseFloat(js_meses[i].Horas_Otros_nodos);
            nExternos += parseFloat(js_meses[i].Jornadas_Externos) + parseFloat(js_meses[i].Horas_Externos);
        }
        
        if (nInternos > 9999999 || nOtrosNodos > 9999999|| nExternos > 9999999)
            nTBDistance = -60; 
        
        sb.Append("<graph ");
        sb.Append("     caption='U.E.A.'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     nameTBDistance='"+ nTBDistance +"'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");

        if (nInternos == 0 && nOtrosNodos == 0 && nExternos == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Propios' value='"+ nInternos +"' color='3f7091' />");
            sb.Append("<set name='Otro "+ strEstructuraNodo +"' value='"+ nOtrosNodos +"' color='f3cc0b' />");
            sb.Append("<set name='Externos' value='"+ nExternos +"' color='7c6c44' />");
        }
        
        sb.Append("</graph>");

        if (bHayDatos){
            myChartD5 = new FusionCharts("../../../Graficos/FCF_Doughnut2D.swf", "myChartIdD5", "100", "115");
            myChartD5.setDataXML(sb.ToString());
            myChartD5.render("chartdivD5");
        }else{
            $I("chartdivD5").className = "texto";
            $I("chartdivD5").innerText = "No hay datos";
        }        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D5", e.message);
    }
}
function generarGraficoD6(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;
        var nTBDistance = -45;
        var nInternos = 0;
        var nOtrosNodos = 0;
        var nExternos = 0;

        for (var i=0; i<js_meses.length; i++){
            nInternos += parseFloat(js_meses[i].Coste_Jornadas_Propios) + parseFloat(js_meses[i].Coste_Horas_Propios);
            nOtrosNodos += parseFloat(js_meses[i].Coste_Jornadas_Otros_nodos) + parseFloat(js_meses[i].Coste_Horas_Otros_nodos);
            nExternos += parseFloat(js_meses[i].Coste_Jornadas_Externos) + parseFloat(js_meses[i].Coste_Horas_Externos);
        }
        
        if (nInternos > 9999999 || nOtrosNodos > 9999999|| nExternos > 9999999)
            nTBDistance = -60; 

        sb.Append("<graph ");
        sb.Append("     caption='Coste anual'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091' ");
        sb.Append("     nameTBDistance='"+ nTBDistance +"'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");

        if (nInternos == 0 && nOtrosNodos == 0 && nExternos == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Propios' value='"+ nInternos +"' color='3f7091' />");
            sb.Append("<set name='Otro "+ strEstructuraNodo +"' value='"+ nOtrosNodos +"' color='f3cc0b' />");
            sb.Append("<set name='Externos' value='"+ nExternos +"' color='7c6c44' />");
        }
        
        sb.Append("</graph>");

        if (bHayDatos){
            myChartD6 = new FusionCharts("../../../Graficos/FCF_Doughnut2D.swf", "myChartIdD6", "100", "115");
            myChartD6.setDataXML(sb.ToString());
            myChartD6.render("chartdivD6");
        }else{
            $I("chartdivD6").className = "texto";
            $I("chartdivD6").innerText = "No hay datos";
        }        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D6", e.message);
    }
}

function generarGrafico3(){
    try{
        var sb = new StringBuilder;
        //if (nValorMinimoGraf3==0) nValorMinimoGraf3 = -110;
        if (nValorMaximoGraf3==0) nValorMaximoGraf3 = 90;

        sb.Append("<graph ");
        sb.Append("     caption='Coste de unidades económicas'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     showvalues='0'");
        sb.Append("     numdivlines='3'");
        sb.Append("     numVdivlines='0'");
        sb.Append("     formatNumber='1'");
        sb.Append("     decimalSeparator='.'");
        sb.Append("     thousandSeparator=','");
        sb.Append("     rotateNames='1'");
        sb.Append("     yaxisminvalue='0'");
        //sb.Append("     yaxisminvalue='"+ parseInt(nValorMinimoGraf3 - nValorMinimoGraf3 / 10, 10) +"'");
        sb.Append("     yaxismaxvalue='"+ parseInt(nValorMaximoGraf3 + nValorMaximoGraf3 / 10, 10) +"'");
        sb.Append("     bgColor='ecf0ee'>");

        sb.Append("<categories>");
        for (var i=0; i<12; i++) sb.Append("<category name='"+ aMes[i] +"' />");
        sb.Append("</categories>");

        sb.Append("<dataset seriesName='Propios' color='3f7091' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++){
            var nInternos = parseFloat(js_meses[i].Coste_Jornadas_Propios) + parseFloat(js_meses[i].Coste_Horas_Propios);
            sb.Append("<set value='"+ nInternos +"' />");
        }
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Otro  "+ strEstructuraNodo +"' color='f3cc0b' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++){
            var nOtrosNodos = parseFloat(js_meses[i].Coste_Jornadas_Otros_nodos) + parseFloat(js_meses[i].Coste_Horas_Otros_nodos);
            sb.Append("<set value='"+ nOtrosNodos +"' />");
        }
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Externos' color='7c6c44' anchorBorderColor='ff3399' anchorBgColor='ff3399'>");
        for (var i=0; i<12; i++){
            var nExternos = parseFloat(js_meses[i].Coste_Jornadas_Externos) + parseFloat(js_meses[i].Coste_Horas_Externos);
            sb.Append("<set value='"+ nExternos +"' />");
        }
        sb.Append("</dataset>");

        sb.Append("</graph>");

        myChart3 = new FusionCharts("../../../Graficos/FCF_MSColumn2D.swf", "myChartId3", "370", "230");
        myChart3.setDataXML(sb.ToString());
        myChart3.render("chartdiv3");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 3", e.message);
    }
}

function generarGraficoD7(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;
        var nTBDistance = -45;
        var nProfesionales = 0;
        var nResto = 0;

        for (var i=0; i<js_meses.length; i++){
            var nProfesionalesAux = parseFloat(js_meses[i].Coste_Horas_Propios)
                            + parseFloat(js_meses[i].Coste_Jornadas_Propios)
                            + parseFloat(js_meses[i].Coste_Horas_Otros_nodos)
                            + parseFloat(js_meses[i].Coste_Jornadas_Otros_nodos)
                            + parseFloat(js_meses[i].Coste_Horas_Externos)
                            + parseFloat(js_meses[i].Coste_Jornadas_Externos);
            nProfesionales += nProfesionalesAux;
            nResto += parseFloat(js_meses[i].Total_Consumo) - nProfesionalesAux;
        }
        
        if (nProfesionales > 9999999 || nResto > 9999999)
            nTBDistance = -60; 

        sb.Append("<graph ");
        sb.Append("     caption='Coste anual'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     nameTBDistance='"+ nTBDistance +"'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");

        if (nProfesionales == 0 && nResto == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Profesionales' value='"+ nProfesionales +"' color='fd261c' isSliced='1' />");
            sb.Append("<set name='Otros' value='"+ nResto +"' color='1c8314' />");
        }
        
        sb.Append("</graph>");

        if (bHayDatos){
            myChartD7 = new FusionCharts("../../../Graficos/FCF_Pie2D.swf", "myChartIdD7", "150", "150");
            myChartD7.setDataXML(sb.ToString());
            myChartD7.render("chartdivD7");
        }else{
            $I("chartdivD7").className = "texto";
            $I("chartdivD7").innerText = "No hay datos.";
        }        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D7", e.message);
    }
}
function generarGraficoD8(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;
        var nTBDistance = -45;
        var nInternos = 0;
        var nOtrosNodos = 0;
        var nExternos = 0;

        for (var i=0; i<js_meses.length; i++){
            nInternos += parseFloat(js_meses[i].Coste_Horas_Propios);
            nOtrosNodos += parseFloat(js_meses[i].Coste_Horas_Otros_nodos);
            nExternos += parseFloat(js_meses[i].Coste_Horas_Externos);
        }
        
        
        if (nInternos > 9999999 || nOtrosNodos > 9999999|| nExternos > 9999999)
            nTBDistance = -60; 

        sb.Append("<graph ");
        sb.Append("     caption='Coste horas'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     nameTBDistance='"+ nTBDistance +"'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     bgColor='ecf0ee'>");

        if (nInternos == 0 && nOtrosNodos == 0 && nExternos == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Propios' value='"+ nInternos +"' color='3f7091' />");
            sb.Append("<set name='Otro "+ strEstructuraNodo +"' value='"+ nOtrosNodos +"' color='f3cc0b' />");
            sb.Append("<set name='Externos' value='"+ nExternos +"' color='7c6c44' />");
        }
        
        sb.Append("</graph>");

        if (bHayDatos){
            myChartD8 = new FusionCharts("../../../Graficos/FCF_Doughnut2D.swf", "myChartIdD8", "100", "115");
            myChartD8.setDataXML(sb.ToString());
            myChartD8.render("chartdivD8");
        }else{
            $I("chartdivD8").className = "texto";
            $I("chartdivD8").innerText = "No hay datos sobre costes por hora.";
        }        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico D8", e.message);
    }
}

function generarGrafico4(){
    try{
        var sb = new StringBuilder;

        //if (nValorMinimoGraf4==0) nValorMinimoGraf4 = -110;
        if (nValorMaximoGraf4==0) nValorMaximoGraf4 = 90;

        sb.Append("<graph ");
        sb.Append("     caption='Coste'");
//        sb.Append("     subcaption='Proyectos gestionados por horas'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     hovercapbg='E0E6EA' ");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     areaBorderColor='100d99'");
        sb.Append("     areaAlpha='70'");
        sb.Append("     numVDivLines='10'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='0'");
        sb.Append("     showvalues='0'");
        sb.Append("     numdivlines='3'");
        sb.Append("     numVdivlines='0'");
        sb.Append("     formatNumber='1'");
        sb.Append("     decimalSeparator='.'");
        sb.Append("     thousandSeparator=','");
        sb.Append("     rotateNames='1'");
        sb.Append("     yaxisminvalue='0'");
        //sb.Append("     yaxisminvalue='"+ parseInt(nValorMinimoGraf4 - nValorMinimoGraf4 / 10, 10) +"'");
        sb.Append("     yaxismaxvalue='"+ parseInt(nValorMaximoGraf4 + nValorMaximoGraf4 / 10, 10) +"'");
        sb.Append("     bgColor='ecf0ee'>");

        sb.Append("<categories>");
        for (var i=0; i<12; i++) sb.Append("<category name='"+ aMes[i] +"' />");
        sb.Append("</categories>");

        sb.Append("<dataset seriesName='Profesionales' color='fd261c' anchorBorderColor='fd261c' anchorBgColor='fd261c'>");
        for (var i=0; i<12; i++){
            var nProfesionalesAux = parseFloat(js_meses[i].Coste_Horas_Propios)
                            + parseFloat(js_meses[i].Coste_Jornadas_Propios)
                            + parseFloat(js_meses[i].Coste_Horas_Otros_nodos)
                            + parseFloat(js_meses[i].Coste_Jornadas_Otros_nodos)
                            + parseFloat(js_meses[i].Coste_Horas_Externos)
                            + parseFloat(js_meses[i].Coste_Jornadas_Externos);
            sb.Append("<set value='"+ nProfesionalesAux +"' />");
        }
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Otros' color='1c8314' anchorBorderColor='1c8314' anchorBgColor='1c8314'>");
        for (var i=0; i<12; i++){
            var nProfesionalesAux = parseFloat(js_meses[i].Coste_Horas_Propios)
                            + parseFloat(js_meses[i].Coste_Jornadas_Propios)
                            + parseFloat(js_meses[i].Coste_Horas_Otros_nodos)
                            + parseFloat(js_meses[i].Coste_Jornadas_Otros_nodos)
                            + parseFloat(js_meses[i].Coste_Horas_Externos)
                            + parseFloat(js_meses[i].Coste_Jornadas_Externos);
            var nResto = parseFloat(js_meses[i].Total_Consumo) - nProfesionalesAux;
            sb.Append("<set value='"+ nResto +"' />");
        }
        sb.Append("</dataset>");

        sb.Append("</graph>");

        myChart4 = new FusionCharts("../../../Graficos/FCF_MSArea2D.swf", "myChartId4", "320", "230");
        myChart4.setDataXML(sb.ToString());
        myChart4.render("chartdiv4");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 4", e.message);
    }
}
function Limpiar()
{
    nNivelEstructura=0;
    nNivelSeleccionado = 0;
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;

    var aTable = $I('divPestRetr').getElementsByTagName("TABLE");       
    for (var i=0; i<aTable.length; i++){
        if (aTable[i].id.substring(0,3) != "tbl") continue;
        BorrarFilasDe(aTable[i].id);
    }

    $I("rdbOperador_0").checked=true;
	$I("cboEstado").value="";
    $I("cboCategoria").value="0";
    $I("cboCualidad").value="0"; 
    $I("chkCerrarAuto").checked=true;
    $I("chkActuAuto").checked=false;
    
    setTodos();
}