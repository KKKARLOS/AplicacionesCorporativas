//var nOpcion = 0;
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var bCargandoCriterios=false;
var js_subnodos = new Array();
var js_nodos = new Array();

var bPeriodoModificado = false;

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 

var js_Valores = new Array();
var js_ValSubnodos = new Array();
var js_ValNodos = new Array();

function init(){
    try{
        setOperadorLogico();
        
        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }

        js_nodos = sNodos.split(",");
        if (js_nodos != ""){
            slValores = fgGetCriteriosSeleccionados(18, $I("tblCR"));
            js_ValNodos = slValores.split("///");
        }
        
        //HabFigurasProf();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
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
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: " + aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War","La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 180);
                break;
            case "getPreferencia":
                $I("cboEstado").value = aResul[3];
                $I("cboCategoria").value = aResul[4];
                $I("cboCualidad").value = aResul[5];

                //if (aResul[9]=="1") $I("rdbConcepto_0").checked = true;
                //else $I("rdbConcepto_1").checked = true;
                
                js_subnodos.length = 0;
                js_subnodos = aResul[15].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[16], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblProfesionales");
                insertarFilasEnTablaDOM("tblProfesionales", aResul[18], 0);
                $I("divProfesionales").scrollTop = 0;

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
                
                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[45], 0);
                $I("divProyecto").scrollTop = 0;

                BorrarFilasDe("tblFiguras");
                insertarFilasEnTablaDOM("tblFiguras", aResul[47], 0);
                $I("divFiguras").scrollTop = 0;

                js_nodos.length = 0;
                js_nodos = aResul[49].split(",");

                BorrarFilasDe("tblCR");
                insertarFilasEnTablaDOM("tblCR", aResul[48], 0);                
                $I("divCR").scrollTop = 0;

                BorrarFilasDe("tblGF");
                insertarFilasEnTablaDOM("tblGF", aResul[50], 0);                
                $I("divGF").scrollTop = 0;
   
                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[8]=="1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;
                setTodos();
                //HabFigurasProf();
                
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function buscar(){
    try{
        if ($I("tblProfesionales").rows.length==0&&$I("tblFiguras").rows.length==0)
        {
            mmoff("Inf","Es obligatorio indicar algo en la tabla de figuras o en la de profesionales",400);
            return; 
        }
        else
        { 
            if ($I("rdbOperador_0").checked==true)  
            {              
                if ( ($I("tblProfesionales").rows[0].cells[0].innerText=='< Todos >' || $I("tblProfesionales").rows[0].cells[0].innerText=='< Todas >')
                     &&
                     ($I("tblFiguras").rows[0].cells[0].innerText=='< Todos >' || $I("tblFiguras").rows[0].cells[0].innerText=='< Todas >')
                    )
                {
                    mmoff("Inf", "Es obligatorio indicar algo en la tabla de figuras o en la de profesionales", 400);
                    return; 
                }
            }
            else
            {
                if ($I("tblProfesionales").rows[0]==null&&$I("tblFiguras").rows[0]==null)
                {
                    mmoff("Inf", "Es obligatorio indicar algo en la tabla de figuras o en la de profesionales", 400);
                    return; 
                }                
            }
        }               
        
        $I('imgImpresora').src='../../../../Images/imgImpresora.gif';
        setTimeout("$I('imgImpresora').src='../../../../Images/imgImpresorastop.gif';", 10000); 

        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("cboEstado").value +"@#@");    
        sb.Append($I("cboCategoria").value +"@#@");
        sb.Append($I("cboCualidad").value +"@#@");
        sb.Append(getDatosTabla(8)+ "@#@"); //Clientes

        sb.Append(getDatosTabla(20)+ "@#@"); //Figuras-item
        sb.Append(getDatosTabla(15)+ "@#@"); //Profesionales

        sb.Append(getDatosTabla(3)+ "@#@"); //Naturaleza
        sb.Append(getDatosTabla(5)+ "@#@"); //Horizontal
        sb.Append(getDatosTabla(4)+ "@#@"); //ModeloCon
        sb.Append(getDatosTabla(9)+ "@#@"); //Contrato
        sb.Append(js_subnodos.join(",")+ "@#@"); //ids Subnodos      
        sb.Append(getEstructura(5)+ "@#@"); //Nodos
        sb.Append(getEstructura(4)+ "@#@"); //Supernodos1
        sb.Append(getEstructura(3)+ "@#@"); //Supernodos2
        sb.Append(getEstructura(2)+ "@#@"); //Supernodos3
        sb.Append(getEstructura(1)+ "@#@"); //Supernodos4
        
        sb.Append(getDatosTabla(6)+ "@#@"); //Sector
        sb.Append(getDatosTabla(7)+ "@#@"); //Segmento
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false)+ "@#@"); //Operador lógico
        sb.Append(getDatosTabla(10)+ "@#@"); //CNP
        sb.Append(getDatosTabla(11)+ "@#@"); //CSN1P
        sb.Append(getDatosTabla(12)+ "@#@"); //CSN2P
        sb.Append(getDatosTabla(13)+ "@#@"); //CSN3P
        sb.Append(getDatosTabla(14)+ "@#@"); //CSN4P
        sb.Append(getDatosTabla(16)+ "@#@"); //ProyectoSubnodos
        sb.Append(js_nodos.join(",")+ "@#@"); //ids Nodos(Oficinas Técnicas) 
        sb.Append(getDatosTabla(22)+ "@#@"); //Grupos funcionales
        
        //sb.Append(getRadioButtonSelectedValue("rdbConcepto", false)+ "@#@"); //Concepto
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}
function getEstructura(tipo)
{
    try{
        var sb = ""; //sin paréntesis
        var sw = 0;
            
        for (var i=0; i<$I("tblAmbito").rows.length;i++){
            if ($I("tblAmbito").rows[i].id == "-999") continue;
            if ($I("tblAmbito").rows[i].getAttribute("tipo") != tipo) continue;         
            sb = sb + $I("tblAmbito").rows[i].id;
            sb = sb + ",";
        }  
        if (sb.length>0) sb = sb.substring(0,sb.length-1);          

        if (sb.length > 8000)
        {
            ocultarProcesando();
            mmoff("Inf", "Has seleccionado un número excesivo de elementos en Ámbito.", 450);
        }                
        return sb;                    
    }catch(e){
	    mostrarErrorAplicacion("Error al obtener los IDs de la estructura.", e.message);
    }       
}
function getEstructuraOld(tipo)
{
    try{
        var sb = new StringBuilder; //sin paréntesis
        var sw = 0;
            
        for (var i=0; i<$I("tblAmbito").rows.length;i++){
            if ($I("tblAmbito").rows[i].id == "-999") continue;
            if ($I("tblAmbito").rows[i].getAttribute("tipo") != tipo) continue;         
            sb.Append($I("tblAmbito").rows[i].id);
            sb.Append(",");
        }  
        if (sb.ToString().length>0) 
        {
           var a =sb.ToString().substring(0,sb.ToString().length-1);          
           sb.length = 0;
           sb.Append(a);
        }
        if (sb.ToString().length > 8000)
        {
            ocultarProcesando();
            mmoff("Inf", "Has seleccionado un número excesivo de elementos en Ámbito.", 450);
        }                
        return sb.ToString();                    
    }catch(e){
	    mostrarErrorAplicacion("Error al obtener los IDs de la estructura.", e.message);
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
            case 15: oTabla = $I("tblProfesionales"); break;
            case 16: oTabla = $I("tblProyecto"); break;
            case 18: oTabla = $I("tblCR"); break;            
            case 20: oTabla = $I("tblFiguras"); break;
            case 22: oTabla = $I("tblGF"); break;
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
                case 15: mmoff("Inf", "Has seleccionado un número excesivo de profesionales.", 450); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos-subnodos.", 500); break;
                case 18: mmoff("Inf", "Has seleccionado un número excesivo de oficinas técnicas.", 500); break;
                case 20: mmoff("Inf", "Has seleccionado un número excesivo de figuras.", 400); break;
                case 22: mmoff("Inf", "Has seleccionado un número excesivo de grupos funcionales.", 500); break;
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
            mmoff("Inf","Actualizando valores de criterios... Espere, por favor", 350);
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

        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(850, 400);
        
        sSubnodos = "";
        sNodos = "";

        var strEnlace = strServer + "Capa_Presentacion/";
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
                //strEnlace += "ECO/Consultas/getEstructuraSubnodos/Default.aspx?sSnds=" + codpar(sSubnodos) + "&sExcede=" + ((bExcede) ? "T" : "F");
                strEnlace += "ECO/Consultas/getEstructuraSubnodos/Default.aspx?sExcede=" + ((bExcede) ? "T" : "F");
                sTamano = sSize(950, 450);
                break;         
            case 15:
                strEnlace += "ECO/Consultas/Profesionales/Jerarquias/Profesionales/Default.aspx?nTipo="+nTipo+"&CR=";
                sTamano = sSize(1010, 520);  
                break;
            case 16:  

                if (bCargarCriterios){
                    strEnlace += "ECO/Consultas/getCriterioProyecto/Default.aspx?nTipo="+nTipo+"&sMod=pst";
                    sTamano = sSize(1010, 570);  
                }
                else{
                    strEnlace += "PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pst";
                    sTamano = sSize(1010, 720);  
                }
                break;
            case 18:               
                if (bCargarCriterios){
                    for (var i=0; i<js_cri.length; i++)
                    {
                        if (js_cri[i].t < nTipo) continue;
                        if (js_cri[i].t > nTipo) break;
                        if (i==0) sNodos = js_cri[i].c;
                        else sNodos += ","+js_cri[i].c;
                    }
                }
                strEnlace += "ECO/Consultas/getEstructuraNodos/Default.aspx?sNodos="+sNodos+"&sExcede="+((bExcede) ? "T" : "F");
                sTamano = sSize(950, 450);
                break;                    
            case 20:
                strEnlace += "Administracion/Consultas/FigurasUsuarios/Figuras/default.aspx";
                sTamano = sSize(850, 470);        
                break;            
            case 22:
	            strEnlace += "PSP/Informes/Conceptos/GrupoFuncional/Default.aspx?nTipo="+nTipo;
	            sTamano = sSize(850, 470);        
                break;

            default:
                if (bCargarCriterios) {
                    sTamano = sSize(850, 460);
                    strEnlace += "ECO/Consultas/getCriterio/Default.aspx?nTipo=" + nTipo;
                }
                else 
                {
                    sTamano = sSize(850, 420);
                    strEnlace += "ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
                }
                break;
        }   
        //Paso los elementos que ya tengo seleccionados
        
        switch (nTipo){
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
            case 15: oTabla = $I("tblProfesionales"); break;
            case 16: oTabla = $I("tblProyecto"); break;
            case 20: oTabla = $I("tblFiguras"); break;
            case 22: oTabla = $I("tblGF"); break;
        }
        if (nTipo != 1 && nTipo != 18){        
            slValores=fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }

        //window.focus();
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
	            if (ret != null){
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 1:
                            nNivelEstructura = parseInt(aElementos[0], 10);
                            nNivelSeleccionado = parseInt(aElementos[0], 10);
                            BorrarFilasDe("tblAmbito");
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

                                var oCtrl1 = document.createElement("div");
                                oCtrl1.className = "NBR W230";
                                oCtrl1.attachEvent('onmouseover', TTip);

                                oNF.cells[0].appendChild(oCtrl1);
                                oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                            }
                            divAmbito.scrollTop = 0;
                            break;
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
                        case 15:
                            BorrarFilasDe("tblProfesionales");
                            for (var i = 0; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProfesionales").insertRow(-1);
                                oNF.id = aDatos[0];
                                oNF.setAttribute("tipo", aDatos[3]);
                                oNF.setAttribute("sexo", aDatos[4]);
                                oNF.setAttribute("baja", aDatos[5]);
                                oNF.style.height = "16px";
                                var oCtrl1 = document.createElement("nobr");
                                oCtrl1.className = "NBR W255";
                                oNF.insertCell(-1).appendChild(oCtrl1);
                                oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[2]);
                            }
                            $I("tblProfesionales").scrollTop = 0;
                            break;
                        case 16:
                            BorrarFilasDe("tblProyecto");
                            for (var i = 0; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProyecto").insertRow(-1);
                                oNF.id = aDatos[0];
                                oNF.style.height = "18px";
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
                                var oCtrl1 = document.createElement("nobr");
                                oCtrl1.className = "NBR W190";
                                oCtrl1.setAttribute("style", 'margin-left:0px;');
                                oCtrl1.attachEvent('onmouseover', TTip);

                                oNF.cells[0].appendChild(oCtrl1);
                                oNF.cells[0].children[3].innerHTML = Utilidades.unescape(aDatos[1]);
                            }
                            divProyecto.scrollTop = 0;
                            break;
                        case 18:
                            //nNivelEstructura = parseInt(aElementos[0], 10) + 1;
                            //nNivelSeleccionado = parseInt(aElementos[0], 10);
                            BorrarFilasDe("tblCR");
                            //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
                            for (var i = 1; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblCR").insertRow(-1);
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
                                }
                                var oCtrl1 = document.createElement("nobr");
                                oCtrl1.className = "NBR W230";
                                oCtrl1.attachEvent('onmouseover', TTip);

                                oNF.cells[0].appendChild(oCtrl1);
                                oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                            }

                            divCR.scrollTop = 0;
                            break;
                        case 20:
                            insertarTabla(aElementos, "tblFiguras"); break;
                            break;
                        case 22:
                            insertarTabla(aElementos, "tblGF"); break;
                            break;

                    }
                    setTodos();
                }
            });
        ocultarProcesando();
	}
	catch(e){
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

			var oCtrl1 = document.createElement("div");
			oCtrl1.className = "NBR W255";
			oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));
			
			oNF.insertCell(-1).appendChild(oCtrl1);

			//oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
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
            case 15: BorrarFilasDe("tblProfesionales"); break;
            case 16: BorrarFilasDe("tblProyecto"); break;
            case 18: 
                    BorrarFilasDe("tblCR"); 
                    js_nodos.length = 0;
                    js_ValNodos.length = 0;
                    break;             
            case 20: BorrarFilasDe("tblFiguras"); break;
            case 22: BorrarFilasDe("tblGF"); break;
        }
	    setTodos();            
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
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
function setTodos(){
    try{
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        setFilaTodos("cboEstado", (sOL=="1")?true:false, false);        
        setFilaTodos("cboCategoria", (sOL=="1")?true:false, false);
        setFilaTodos("cboCualidad", (sOL=="1")?true:false, false);
        setFilaTodos("tblAmbito", (sOL=="1")?true:false, true);
        setFilaTodos("tblSector", (sOL=="1")?true:false, true);
       
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
        setFilaTodos("tblProfesionales", (sOL=="1")?true:false, true);
        setFilaTodos("tblFiguras", (sOL=="1")?true:false, true);
        setFilaTodos("tblCR", (sOL=="1")?true:false, true)
        setFilaTodos("tblGF", (sOL=="1")?true:false, true)
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
        sb.Append("0@#@");
        sb.Append("0@#@");

// sb.Append(getRadioButtonSelectedValue("rdbConcepto", false) +"@#@");

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
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
            .then(function(ret) {
	            if (ret != null){
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
	            }
            });
        
	    ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

function getValoresMultiples(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n=1; n<=22; n++){
            switch (n)
            {
                case 1: oTabla = $I("tblAmbito"); break;
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
                case 15: oTabla = $I("tblProfesionales"); break;
                case 16: oTabla = $I("tblProyecto"); break;
                case 18: oTabla = $I("tblCR"); break;
                case 20: oTabla = $I("tblFiguras"); break;
                case 22: oTabla = $I("tblGF"); break;
            }
        
            for (var i=0; i<oTabla.rows.length;i++){
                if (oTabla.rows[i].id == "-999") continue;
                if (n==1||n==18){
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n +"##"+ oTabla.rows[i].getAttribute("tipo")+"-"+oTabla.rows[i].id +"##"+ Utilidades.escape(oTabla.rows[i].innerText));
                }
                else{
                    if (n==16){
                        if (sb.buffer.length>0) sb.Append("///");
                        sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                    }else if(n==15)
                    {
                        if (sb.buffer.length>0) sb.Append("///");
                        sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].getAttribute("sexo") + "-" + oTabla.rows[i].getAttribute("baja") + "##" + Utilidades.escape(oTabla.rows[i].innerText));                                            
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

    js_nodos.length = 0;
    js_ValNodos.length = 0;

    var aTable = $I('tblCriterios').getElementsByTagName("TABLE");       
    for (var i=0; i<aTable.length; i++){
        if (aTable[i].id.substring(0,3) != "tbl") continue;
        BorrarFilasDe(aTable[i].id);
    }

    $I("rdbOperador_0").checked=true;
	$I("cboEstado").value="";
    $I("cboCategoria").value="0";
    $I("cboCualidad").value="0"; 
    
    setTodos();
}
/*
function HabFigurasProf()
{
    try{
        if ($I("rdbConcepto_0").checked) 
        {
            $I("lblProfesionales").className = "label";
            $I("Img21").style.visibility = "hidden"; 
            $I("lblFiguras").className = "enlace";
            $I("Img20").style.visibility = "visible";  
            BorrarFilasDe("tblProfesionales");                 
        }
        else
        {
            $I("lblFiguras").className = "label";
            $I("Img20").style.visibility = "hidden";
            $I("lblProfesionales").className = "enlace";
            $I("Img21").style.visibility = "visible"; 
            BorrarFilasDe("tblFiguras");       
        }
        setTodos();       
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}
*/