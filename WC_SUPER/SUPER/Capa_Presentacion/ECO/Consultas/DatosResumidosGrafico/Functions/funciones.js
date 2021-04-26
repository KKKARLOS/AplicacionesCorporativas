var js_meses = new Array();
//var myChart1;// = new FusionCharts("../../../Graficos/FCF_MSLine.swf", "myChartId", "1000", "350");
//var myChart2;
//var myChart3;
//var myChart4;
//var nValorMinimoGraf1 = 0;
//var nValorMaximoGraf1 = 0;
//var nValorMinimoGraf2 = 0;
//var nValorMaximoGraf2 = 0;
//var nValorMinimoGraf3 = 0;
//var nValorMaximoGraf3 = 0;
//var nValorMinimoGraf4 = 0;
//var nValorMaximoGraf4 = 0;

var nCriterioAVisualizar = 0;
var js_subnodos = new Array();
var bPeriodoModificado = false;
var bCargandoCriterios=false;
/* Valores necesarios para la pesta�a retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 500;
var nTopPestana = 98;
/* Fin de Valores necesarios para la pesta�a retractil */
var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

//Lista de par�metros seleccionados para pasar a la pantalla de selecci�n de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

function init(){
    try{
//        $I("procesando").style.top = 110;
//        $I("procesando").style.left = 410;
        
        setOperadorLogico(false);

        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }
        //resetDatos();
        if (bHayPreferencia && $I("hdnInicio").value == "T" && $I("chkActuAuto").checked) {
            buscar();
            setTimeout("mostrarProcesando();", 50);
        }
        else {
            //Si estoy cargando porque se ha hecho submit no muestro la pesta�a retr�ctil y recargo los criterios
            if ($I("hdnInicio").value == "F") {
                bPestRetrMostrada = false;
                //setTimeout("mostrarProcesando();", 50);
                reponerCriterios($I("hdnReponerCri").value);
                $I("tblGeneral").style.visibility = "visible";
            }
            else {
                ocultarProcesando();
                mostrarOcultarPestVertical();
            } 
        }
        //ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

function mostrarOcultarPestVertical(){
    if (!bPestRetrMostrada){
        //$I("tblGeneral").style.visibility = "hidden";
        mostrarCriterios();
    }else{
        ocultarCriterios();
        //$I("tblGeneral").style.visibility = "visible";
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
//            case "buscar":
//                bOcultar = false;
//                setDatos(aResul[2]);
//                break;
            case "getTablaCriterios":
                mmoff("hide");
                bOcultar = false;
                eval(aResul[2]);
                bCargandoCriterios = false;
                if (nCriterioAVisualizar != 0) getCriterios(nCriterioAVisualizar);
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
                
                //el operador al final, para que muestre "< Todos >" o no, en funci�n de las tablas
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
                mmoff("Err", "Opci�n de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultar) 
            ocultarProcesando();
    }
}

function getTablaCriterios(){
    try{
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
        $I("tblGeneral").style.visibility = "hidden";
        
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
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false)+ "@#@"); //Operador l�gico
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

        //RealizarCallBack(sb.ToString(), "");
        $I("hdnInicio").value = "F";
        $I("hdnCriterios").value = sb.ToString();
        //Guardo los criterios para recargarlos despu�s del submit
        guardarCriterios();
        setTimeout("submitir();", 50);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}
function submitir() {
    document.forms["aspnetForm"].submit();
}
function getDatosTabla(nTipo){
    try{
        var sb = new StringBuilder; //sin par�ntesis
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
                case 2: mmoff("Inf", "Has seleccionado un n�mero excesivo de responsables de proyecto.", 500); break;
                case 3: mmoff("Inf", "Has seleccionado un n�mero excesivo de naturalezas.", 450); break;
                case 4: mmoff("Inf", "Has seleccionado un n�mero excesivo de modelos de contrataci�n.", 500); break;
                case 5: mmoff("Inf", "Has seleccionado un n�mero excesivo de horizontales.", 450); break;
                case 6: mmoff("Inf", "Has seleccionado un n�mero excesivo de sectores.", 450); break;
                case 7: mmoff("Inf", "Has seleccionado un n�mero excesivo de segmentos.", 450); break;
                case 8: mmoff("Inf", "Has seleccionado un n�mero excesivo de clientes.", 450); break;
                case 9: mmoff("Inf", "Has seleccionado un n�mero excesivo de contratos.", 450); break;
                case 10: mmoff("Inf", "Has seleccionado un n�mero excesivo de Qn.", 400); break;
                case 11: mmoff("Inf", "Has seleccionado un n�mero excesivo de Q1.", 400); break;
                case 12: mmoff("Inf", "Has seleccionado un n�mero excesivo de Q2.", 400); break;
                case 13: mmoff("Inf", "Has seleccionado un n�mero excesivo de Q3.", 400); break;
                case 14: mmoff("Inf", "Has seleccionado un n�mero excesivo de Q4.", 400); break;
                case 16: mmoff("Inf", "Has seleccionado un n�mero excesivo de proyectos.", 450); break;
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
        var sTamano = sSize(850, 400);
        
        var strEnlace = "";
        switch (nTipo){
            case 1:
                if (bCargarCriterios) {
                    for (var i = 0; i < js_cri.length; i++) {
                        if (js_cri[i].t > 1) break;
                        if (i == 0) sSubnodos = js_cri[i].c;
                        else sSubnodos += "," + js_cri[i].c;
                    }
                }
                //strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sSnds=" + codpar(sSubnodos) + "&sExcede=" + ((bExcede) ? "T" : "F");
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sExcede=" + ((bExcede) ? "T" : "F");
                sTamano = sSize(950, 450);
                break;
            case 16:
                if (bCargarCriterios) {
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioProyecto/Default.aspx?nTipo=" + nTipo + "&sMod=pge";
                    sTamano = sSize(1010, 570);
                }
                else {
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
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 1:
                            nNivelEstructura = parseInt(aElementos[0], 10);
                            nNivelSeleccionado = parseInt(aElementos[0], 10);
                            BorrarFilasDe("tblAmbito");
                            //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
                            for (var i = 1; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblAmbito").insertRow(-1);
                                oNF.setAttribute("tipo", aDatos[0]);
                                var aID = aDatos[1].split("-");
                                switch (parseInt(oNF.getAttribute("tipo"), 10)) {
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
                            divAmbito.scrollTop = 0;
                            break;
                        case 2: insertarTabla(aElementos, "tblResponsable"); break;
                        case 3: insertarTabla(aElementos, "tblNaturaleza"); break;
                        case 4: insertarTabla(aElementos, "tblModeloCon"); break;
                        case 5: insertarTabla(aElementos, "tblHorizontal"); break;
                        case 6: insertarTabla(aElementos, "tblSector"); break;
                        case 7: insertarTabla(aElementos, "tblSegmento"); break;
                        case 8: insertarTabla(aElementos, "tblCliente"); break;
                        case 9: insertarTabla(aElementos, "tblContrato"); break;
                        case 10: insertarTabla(aElementos, "tblQn"); break;
                        case 11: insertarTabla(aElementos, "tblQ1"); break;
                        case 12: insertarTabla(aElementos, "tblQ2"); break;
                        case 13: insertarTabla(aElementos, "tblQ3"); break;
                        case 14: insertarTabla(aElementos, "tblQ4"); break;
                        case 16:
                            BorrarFilasDe("tblProyecto");
                            for (var i = 0; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProyecto").insertRow(-1);
                                oNF.id = aDatos[0];
                                oNF.style.height = "16px";
                                oNF.setAttribute("categoria", aDatos[2]);
                                oNF.setAttribute("cualidad", aDatos[3]);
                                oNF.setAttribute("estado", aDatos[4]);
                                oNF.insertCell(-1);

                                if (aDatos[2] == "P") oNF.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                                else oNF.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                                switch (aDatos[3]) {
                                    case "C": oNF.cells[0].appendChild(oImgContratante.cloneNode(true), null); break;
                                    case "J": oNF.cells[0].appendChild(oImgRepJor.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                                }

                                switch (aDatos[4]) {
                                    case "A": oNF.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                                    case "C": oNF.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                                    case "H": oNF.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                                }

                                //oNF.cells[0].appendChild(document.createElement("<nobr class='NBR W190' style='margin-left:3px;' onmouseover='TTip(event)'></nobr>"));
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[3].className = "NBR";
                                oNF.cells[0].children[3].setAttribute("style", "width:190px; margin-left:3px;");
                                oNF.cells[0].children[3].attachEvent("onmouseover", TTip);
                                oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
                            }
                            divProyecto.scrollTop = 0;
                            break;
                    }
                    setTodos();
                    if ($I("chkActuAuto").checked) buscar();
                    else
                        ocultarProcesando();
                } else
                    ocultarProcesando();
	        }); 

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}
function insertarTabla(aElementos,strName){
    try{    
		BorrarFilasDe(strName);
		for (var i=0; i<aElementos.length; i++){
			if (aElementos[i]=="") continue;
			var aDatos = aElementos[i].split("@#@");
			var oNF = $I(strName).insertRow(-1);
			oNF.id = aDatos[0];
			oNF.style.height = "16px";
			//oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W260'></nobr>"));
			oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
			oNF.cells[0].children[0].className = "NBR";
			oNF.cells[0].children[0].setAttribute("style", "width:260px;");
			oNF.cells[0].children[0].attachEvent("onmouseover", TTip);

			oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
		}
		$I(strName).scrollTop=0;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar las filas en la tabla "+strName, e.message);
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
        } else 
            ocultarProcesando();

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
		mostrarErrorAplicacion("Error al modificar el operador l�gico.", e.message);
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
        
        var sb = new StringBuilder; //sin par�ntesis
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
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(745, 475))
	        .then(function(ret) {
                if (ret != null) {
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                } else 
	                ocultarProcesando();
	        }); 

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

function getValoresMultiples(){
    try{
        var sb = new StringBuilder; //sin par�ntesis
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

            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].id == "-999") continue;
                if (n == 1) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else {
                    if (n == 16) {
                        if (sb.buffer.length > 0) sb.Append("///");
                        sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                    } else {
                        if (sb.buffer.length > 0) sb.Append("///");
                        sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
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

        if ($I("chkActuAuto").checked){
            buscar();
        }else{
            //ocultarProcesando();
            getTablaCriterios();
        }
    }catch(e){
        mostrarErrorAplicacion("Error al establecer el a�o 2", e.message);
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
function guardarCriterios() {
    try {
        var sb = new StringBuilder;

        sb.Append($I("txtAnno").value + "@#@");//0
        sb.Append($I("cboEstado").value + "@#@");//1
        sb.Append($I("cboCategoria").value + "@#@");//2
        sb.Append($I("cboCualidad").value + "@#@");//3
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) + "@#@"); //Operador l�gico 4
        sb.Append(($I("chkCerrarAuto").checked) ? "1@#@" : "0@#@");//5
        sb.Append(($I("chkActuAuto").checked) ? "1@#@" : "0@#@");//6
        sb.Append(js_subnodos.join(",") + "@#@"); //ids estructura ambito 7

        sb.Append($I("tblAmbito").innerHTML + "@#@"); //8
        sb.Append($I("tblResponsable").innerHTML + "@#@"); //9
        sb.Append($I("tblNaturaleza").innerHTML + "@#@"); //10
        sb.Append($I("tblModeloCon").innerHTML + "@#@"); //11
        sb.Append($I("tblHorizontal").innerHTML + "@#@"); //12
        sb.Append($I("tblSector").innerHTML + "@#@"); //13
        sb.Append($I("tblSegmento").innerHTML + "@#@"); //14
        sb.Append($I("tblCliente").innerHTML + "@#@"); //15
        sb.Append($I("tblContrato").innerHTML + "@#@"); //16
        sb.Append($I("tblQn").innerHTML + "@#@"); //17
        sb.Append($I("tblQ1").innerHTML + "@#@"); //18
        sb.Append($I("tblQ2").innerHTML + "@#@"); //19
        sb.Append($I("tblQ3").innerHTML + "@#@"); //20
        sb.Append($I("tblQ4").innerHTML + "@#@"); //21
        sb.Append($I("tblProyecto").innerHTML + "@#@"); //22

        $I("hdnReponerCri").value = sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al guardar criterios.", e.message);
    }
}
function reponerCriterios(strCriterios) {
    if (strCriterios == "") return;
    var aResul = strCriterios.split("@#@");

    $I("txtAnno").value = aResul[0];
    $I("cboEstado").value = aResul[1];
    $I("cboCategoria").value = aResul[2];
    $I("cboCualidad").value = aResul[3];
    $I("chkCerrarAuto").checked = (aResul[5] == "1") ? true : false;
    $I("chkActuAuto").checked = (aResul[6] == "1") ? true : false;
    js_subnodos.length = 0;
    js_subnodos = aResul[7].split(",");

    BorrarFilasDe("tblAmbito");
    insertarFilasEnTablaDOM("tblAmbito", aResul[8], 0);
    $I("divAmbito").scrollTop = 0;

    BorrarFilasDe("tblResponsable");
    insertarFilasEnTablaDOM("tblResponsable", aResul[9], 0);
    $I("divResponsable").scrollTop = 0;

    BorrarFilasDe("tblNaturaleza");
    insertarFilasEnTablaDOM("tblNaturaleza", aResul[10], 0);
    $I("divNaturaleza").scrollTop = 0;

    BorrarFilasDe("tblModeloCon");
    insertarFilasEnTablaDOM("tblModeloCon", aResul[11], 0);
    $I("divModeloCon").scrollTop = 0;

    BorrarFilasDe("tblHorizontal");
    insertarFilasEnTablaDOM("tblHorizontal", aResul[12], 0);
    $I("divHorizontal").scrollTop = 0;

    BorrarFilasDe("tblSector");
    insertarFilasEnTablaDOM("tblSector", aResul[13], 0);
    $I("divSector").scrollTop = 0;

    BorrarFilasDe("tblSegmento");
    insertarFilasEnTablaDOM("tblSegmento", aResul[14], 0);
    $I("divSegmento").scrollTop = 0;

    BorrarFilasDe("tblCliente");
    insertarFilasEnTablaDOM("tblCliente", aResul[15], 0);
    $I("divCliente").scrollTop = 0;

    BorrarFilasDe("tblContrato");
    insertarFilasEnTablaDOM("tblContrato", aResul[16], 0);
    $I("divContrato").scrollTop = 0;

    BorrarFilasDe("tblQn");
    insertarFilasEnTablaDOM("tblQn", aResul[17], 0);
    $I("divQn").scrollTop = 0;

    BorrarFilasDe("tblQ1");
    insertarFilasEnTablaDOM("tblQ1", aResul[18], 0);
    $I("divQ1").scrollTop = 0;

    BorrarFilasDe("tblQ2");
    insertarFilasEnTablaDOM("tblQ2", aResul[19], 0);
    $I("divQ2").scrollTop = 0;

    BorrarFilasDe("tblQ3");
    insertarFilasEnTablaDOM("tblQ3", aResul[20], 0);
    $I("divQ3").scrollTop = 0;

    BorrarFilasDe("tblQ4");
    insertarFilasEnTablaDOM("tblQ4", aResul[21], 0);
    $I("divQ4").scrollTop = 0;

    BorrarFilasDe("tblProyecto");
    insertarFilasEnTablaDOM("tblProyecto", aResul[22], 0);
    $I("divProyecto").scrollTop = 0;

    //el operador al final, para que muestre "< Todos >" o no, en funci�n de las tablas
    if (aResul[4] == "1") $I("rdbOperador_0").checked = true;
    else $I("rdbOperador_1").checked = true;

    setTodos();

//    if ($I("chkActuAuto").checked) {
//        bOcultar = false;
//        setTimeout("buscar();", 20);
    //    }
    setTimeout("ocultarProcesando();", 1000);
}