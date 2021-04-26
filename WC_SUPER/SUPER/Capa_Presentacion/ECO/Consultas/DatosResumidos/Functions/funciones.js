var oDivTitulo;
var oDivResultado;
var nOpcion = 0;
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nNivelIndentacion = 1;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var js_subnodos = new Array();
var bPeriodoModificado = false;
var bCargandoCriterios=false;
// Valores necesarios para la pestaña retractil 
var nIntervaloPX = 20;
var nAlturaPestana = 580;
var nTopPestana = 98;
// Fin de Valores necesarios para la pestaña retractil 

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

var iDesdeOld = 0;
var iHastaOld = 0;

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";
//var oImgSep = document.createElement("<img src='../../../../images/imgSeparador.gif'>");
var oImgSep = document.createElement("img");
oImgSep.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgSeparador.gif");

var oImgCli = document.createElement("img");
oImgCli.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgClienteICO.gif");
oImgCli.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;");

//var oImg8 = document.createElement("<img src='../../../../images/imgNaturaleza.gif' title='Naturalez' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>");
var oImgNat = document.createElement("img");
oImgNat.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgNaturaleza.gif");
oImgNat.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;");

var sIdCache = "";

function init(){
    try{
        if (!bRes1024) setResolucion1280();

        iDesdeOld = $I("hdnDesde").value;
        iHastaOld = $I("hdnHasta").value;

        oDivTitulo = $I("divTablaTitulo");
        oDivResultado = $I("divResultado");
        
        mostrarOcultarColumnas();

        setOperadorLogico(false);
        
        setLeyenda();
        if (bHayPreferencia){
            if ($I("tblDatos") != null)
                if ($I("tblDatos").rows.length == 0)
                    mostrarOcultarPestVertical();
                else
                    scrollTablaDR();
            else
                mostrarOcultarPestVertical();
        }
        else
            mostrarOcultarPestVertical();
        
        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }

        //setExcelImg("imgExcel", "divCatalogo");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]) {
            case "excel":
                if (aResul[2]) {
                    sIdCache = aResul[2];
                    crearExcelSimpleServerCache(sIdCache);
                }
                break;
            case "buscar":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];

                $I("totalV1").innerText = aResul[3].ToString("N"); //V1
                if (parseFloat(dfn(aResul[3])) < 0) $I("totalV1").style.color = "red";
                else $I("totalV1").style.color = "black";
                $I("totalV2").innerText = aResul[4].ToString("N"); //V2
                if (parseFloat(dfn(aResul[4])) < 0) $I("totalV2").style.color = "red";
                else $I("totalV2").style.color = "black";
                $I("totalV3").innerText = aResul[5].ToString("N"); //V3
                if (parseFloat(dfn(aResul[5])) < 0) $I("totalV3").style.color = "red";
                else $I("totalV3").style.color = "black";
                $I("totalV4").innerText = aResul[6].ToString("N"); //V4
                if (parseFloat(dfn(aResul[6])) < 0) $I("totalV4").style.color = "red";
                else $I("totalV4").style.color = "black";
                $I("totalV5").innerText = aResul[7].ToString("N"); //V5
                if (parseFloat(dfn(aResul[7])) < 0) $I("totalV5").style.color = "red";
                else $I("totalV5").style.color = "black";
                $I("totalV6").innerText = aResul[8].ToString("N") + " %"; //V6
                if (parseFloat(dfn(aResul[8])) < 0) $I("totalV6").style.color = "red";
                else $I("totalV6").style.color = "black";
                $I("totalV7").innerText = aResul[9].ToString("N"); //V7
                if (parseFloat(dfn(aResul[9])) < 0) $I("totalV7").style.color = "red";
                else $I("totalV7").style.color = "black";
                $I("totalV8").innerText = aResul[10].ToString("N"); //V8
                if (parseFloat(dfn(aResul[10])) < 0) $I("totalV8").style.color = "red";
                else $I("totalV8").style.color = "black";
                $I("totalV9").innerText = aResul[11].ToString("N"); //V9
                if (parseFloat(dfn(aResul[11])) < 0) $I("totalV9").style.color = "red";
                else $I("totalV9").style.color = "black";
                
                mostrarOcultarColumnas();
                scrollTablaDR();
                window.focus();

                if (bPeriodoModificado) {
                    bPeriodoModificado = false;
                    setTimeout("getTablaCriterios();", 20);
                }

                if (nIDFicepiEntrada == 1797
                    || nIDFicepiEntrada == 1321
                    || nIDFicepiEntrada == 1511
                        ) {
                    $I("divTiempos").innerHTML = "Respuesta de BD: " + aResul[12] + " ms.<br>Respuesta HTML: " + aResul[13] + " ms.";
                    $I("divTiempos").style.display = "block";
                }

                setExcelImg("imgExcel", "divCatalogo");
                break;
            case "buscar2":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila+1);
//                insertarFilasEnTablaObject("tblDatos", aResul[2], iFila+1);
                $I("tblDatos").rows[iFila].cells[0].getElementsByTagName("IMG")[0].src = strServer +"images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado", "1");
                scrollTablaDR();
                break;
            case "getTablaCriterios":
                mmoff("hide");
                eval(aResul[2]);
                bCargandoCriterios=false;
                
                iDesdeOld = $I("hdnDesde").value;
                iHastaOld = $I("hdnHasta").value;

                if (nCriterioAVisualizar!=0) getCriterios(nCriterioAVisualizar);
                break;

            case "setResolucion":
                location.reload(true);
                break;
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.",200);
                break;
            case "getPreferencia":
                if (aResul[3] == "")
                    $I("cboConceptoEje").value = "";
                else
                    $I("cboConceptoEje").value = (parseInt(aResul[3], 10) >= 7) ? aResul[3] : "0";
                $I("cboCategoria").value = aResul[4];
                $I("cboCualidad").value = aResul[5];
                $I("chkCerrarAuto").checked = (aResul[6] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[7] == "1") ? true : false;
                nUtilidadPeriodo = parseInt(aResul[9], 10);
                $I("hdnDesde").value = aResul[10];
                $I("txtDesde").value = aResul[11];
                $I("hdnHasta").value = aResul[12];
                $I("txtHasta").value = aResul[13];

                iDesdeOld = $I("hdnDesde").value;
                iHastaOld = $I("hdnHasta").value;

                //aResul[14] //la opción se determinará al buscar
                js_subnodos.length = 0;
                js_subnodos = aResul[15].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[16], 0);
                $I("divAmbito").scrollTop = 0;
                //Mikel 20/02/2015 Al cambiar la preferencia hay que recargar la lista de elementos de la estructura
                if (js_subnodos != "") {
                    slValores = fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
                    js_ValSubnodos = slValores.split("///");
                }

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

                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[45], 0);
                $I("divProyecto").scrollTop = 0;

                var aMagnitudes = aResul[44].split("///");
                var aDatos;
                for (var x = 0; x < aMagnitudes.length; x++) {
                    aDatos = aMagnitudes[x].split("##");
                    $I(aDatos[0]).checked = (aDatos[1] == "1") ? true : false;
                }
                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[8] == "1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;

                setTodos();

                if ($I("chkActuAuto").checked) {
                    bOcultarProcesando = false;
                    setTimeout("buscar();", 20);
                }
                break;
            case "setResultadoOnline":
                bOcultarProcesando = false;
                setTimeout("buscar();", 20);
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
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
        //            borrarCatalogo();
        //            
        //            if ($I("chkActuAuto").checked){
        //                //buscar();
        //                buscarDiferido();
        //                //bPeriodoModificado = true;
        //            }else{
        //                if (iProc==0) ocultarProcesando();
        //                //getTablaCriterios();
        //            }

                    borrarCatalogo();
                    if ($I("chkActuAuto").checked){
                        buscar();
                    }else{
                        ocultarProcesando();
                    }

                }else ocultarProcesando();
	        }); 

	}catch(e){
		mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
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

function setCombo(){
    try{
        borrarCatalogo();
        if ($I("chkActuAuto").checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el "+ strEstructuraNodo, e.message);
    }
}

function buscar(){
    try{
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == ""){
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
            return;
        }
        
        if ($I("cboConceptoEje").value == ""){
            mmoff("War", "El concepto eje es obligatorio.", 220);
            return;
        }

        if (!$I("chkV1").checked
            && !$I("chkV2").checked
            && !$I("chkV3").checked
            && !$I("chkV4").checked
            && !$I("chkV5").checked
            && !$I("chkV6").checked
            && !$I("chkV7").checked
            && !$I("chkV8").checked
            && !$I("chkV9").checked){
            mmoff("War", "Es obligatorio seleccionar al menos un valor (magnitud económica).", 400);
            return;
        }

        nNivelEstructura = 0;
        nNivelSeleccionado = 0;
        nOpcion=0;
        
        if (parseInt($I("cboConceptoEje").value, 10) >= 7){
            nOpcion = parseInt($I("cboConceptoEje").value, 10);
        }else{
            nOpcion = 6;
            if ($I("tblAmbito").rows.length > 0 && $I("tblAmbito").rows[0].id != "-999"){
                for (var i=0; i < $I("tblAmbito").rows.length; i++){
                    if (parseInt($I("tblAmbito").rows[i].getAttribute("tipo"), 10) < nOpcion) 
                        nOpcion = parseInt($I("tblAmbito").rows[i].getAttribute("tipo"), 10);
                }
            }else nOpcion = nEstructuraMinima;
        }

        nNivelIndentacion = 1;
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append(nOpcion +"@#@");
        sb.Append($I("hdnDesde").value +"@#@");
        sb.Append($I("hdnHasta").value +"@#@");
//        sb.Append(nNivelEstructura+"@#@");
        sb.Append("7@#@");
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
        sb.Append(nNivelIndentacion+ "@#@");
        sb.Append(($I("chkV1").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV2").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV3").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV4").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV5").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV6").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV7").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV8").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkV9").checked)? "1@#@":"0@#@");
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
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function mostrar(oImg){
    try{
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);
        
        nIDItem = 0;        
        nNivelIndentacion = nNivel;
        
//            nNivelEstructura = 7;
//            nIDEstructura = js_subnodos.join(",");
//            A menos que esté tesplegando un elemento de la estructura organizativa mayor
//            que subnodo, entonces el nNivelEstructura es el que corresponda según la estructura y
//            el nIDEstructura es el ID del elemento seleccionado.
        nNivelEstructura = 7;
        nIDEstructura = js_subnodos.join(",");

        if (nDesplegado == 0){
            switch (oFila.getAttribute("tipo")){
//            	1 -> SN4
//                2 -> SN3
//                3 -> SN2
//                4 -> SN1
//                5 -> Nodo
//                6 -> Subnodo
//                7 -> Cliente
//                8 -> Naturaleza
//                9 -> Responsable de proyecto
//                10 -> Proyecto
//                11 -> Meses
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    nOpcion = parseInt(oFila.getAttribute("tipo"), 10)+1;
                    nNivelEstructura = nOpcion;
                    nIDEstructura = oFila.id;
                    break;
                case "6":
                    nOpcion = 10;
                    nIDEstructura = oFila.id;
                    break;  
                case "7":
                case "8":
                case "9":
                    nOpcion = 10;
                    nIDItem = oFila.id;
                    break;
                case "10":
                    nOpcion = parseInt(oFila.getAttribute("tipo"), 10)+1;
                    nIDItem = oFila.id;
                    break;
                case "11":
                    //alert("11");
                    nOpcion = parseInt(oFila.getAttribute("tipo"), 10)+1;
                    nIDItem = oFila.id;
                    break;
            }
            
            iFila=nIndexFila;

            if ($I("cboConceptoEje").value == ""){
                mmoff("War", "El concepto eje es obligatorio.", 220);
                return;
            }
            mostrarProcesando();
            
            var sb = new StringBuilder;
            sb.Append("buscar2@#@");
            sb.Append(nOpcion +"@#@");
            sb.Append($I("hdnDesde").value +"@#@");
            sb.Append($I("hdnHasta").value +"@#@");
            sb.Append(nNivelEstructura+"@#@");
//            sb.Append("7@#@");
            sb.Append($I("cboCategoria").value +"@#@");
            sb.Append($I("cboCualidad").value +"@#@");
            sb.Append((oFila.getAttribute("tipo")=="7")? nIDItem +"@#@":getDatosTabla(8)+ "@#@"); //Clientes
            sb.Append((oFila.getAttribute("tipo")=="9")? nIDItem +"@#@":getDatosTabla(2)+ "@#@"); //Responsable
            sb.Append((oFila.getAttribute("tipo")=="8")? nIDItem +"@#@":getDatosTabla(3)+ "@#@"); //Naturaleza
            sb.Append(getDatosTabla(5)+ "@#@"); //Horizontal
            sb.Append(getDatosTabla(4)+ "@#@"); //ModeloCon
            sb.Append(getDatosTabla(9)+ "@#@"); //Contrato
            sb.Append(nIDEstructura+"@#@"); //ids estructura ambito
//            sb.Append(js_subnodos.join(",")+ "@#@"); //ids estructura ambito
            sb.Append(getDatosTabla(6)+ "@#@"); //Sector
            sb.Append(getDatosTabla(7)+ "@#@"); //Segmento
            sb.Append(getRadioButtonSelectedValue("rdbOperador", false)+ "@#@"); //Operador lógico
            sb.Append(nNivelIndentacion+ "@#@");
            sb.Append(($I("chkV1").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV2").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV3").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV4").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV5").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV6").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV7").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV8").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkV9").checked)? "1@#@":"0@#@");
            sb.Append(getDatosTabla(10)+ "@#@"); //CNP
            sb.Append(getDatosTabla(11)+ "@#@"); //CSN1P
            sb.Append(getDatosTabla(12)+ "@#@"); //CSN2P
            sb.Append(getDatosTabla(13)+ "@#@"); //CSN3P
            sb.Append(getDatosTabla(14)+ "@#@"); //CSN4P
//            sb.Append(getDatosTabla(16)+ "@#@"); //ProyectoSubnodos
            sb.Append((oFila.getAttribute("tipo") == "10" ||
                       oFila.getAttribute("tipo")=="11")? nIDItem +"@#@": getDatosTabla(16)+ "@#@"); //ProyectoSubnodos
           
            if ($I("chkCerrarAuto").checked){
                bPestRetrMostrada = true;
                mostrarOcultarPestVertical();
            }
           
            RealizarCallBack(sb.ToString(), "");
            return;
        }
        
        //alert("nIndexFila: "+ nIndexFila);
        for (var i=nIndexFila+1; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].getAttribute("nivel") > nNivel){
                if (opcion == "O")
                {
                    tblDatos.rows[i].style.display = "none";
                    if (tblDatos.rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
                else if (tblDatos.rows[i].getAttribute("nivel") - 1 == nNivel) {
                    //tblDatos.rows[i].style.display = "block";
                    tblDatos.rows[i].style.display = "table-row";
                }
            }else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../../images/plus.gif";
        else oImg.src = "../../../../images/minus.gif"; 

        var sKK = tblDatos.outerHTML;

        ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir/contraer", e.message);
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
		mostrarErrorAplicacion("Error al obtener los datos de las tablas.", e.message);
	}
}

function excel(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var strCadena = "";
/*        
        var nPos = location.href.indexOf("Capa_Presentacion");
        var strUrlImg = location.href.substring(0, nPos)+ "images";
        oImgSep.src = strUrlImg +"/imgSeparador.gif";
        for (var i = 0; i < $I("tblDatos").rows.length; i++){
            if ($I("tblDatos").rows[i].style.display == "none"){
                continue;
            }
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("tipo") != "11") oFila.cells[0].children[0].style.marginRight = 0;
                
                switch (oFila.getAttribute("tipo")) {
                    case "1": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN4.cloneNode(true)); break;
                    case "2": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN3.cloneNode(true)); break;
                    case "3": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN2.cloneNode(true)); break;
                    case "4": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN1.cloneNode(true)); break;
                    case "5": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgNodo.cloneNode(true)); break;
                    case "6": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSubNodo.cloneNode(true)); break;
                    case "7": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgCli.cloneNode(true)); break;
                    case "8": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgNat.cloneNode(true)); break;
                    case "9": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", eval("oImg" + oFila.getAttribute("tiporecurso") + oFila.getAttribute("sexo")).cloneNode(true)); break;
                    case "10":
                        //oFila.cells[0].children[0].insertAdjacentElement("afterEnd", eval("oImg10" + oFila.estado).cloneNode(true));
                        switch (oFila.getAttribute("estado")) {
                            case "A": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgAbierto.cloneNode(true)); break;
                            case "C": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgCerrado.cloneNode(true)); break;
                            case "H": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgHistorico.cloneNode(true)); break;
                            case "P": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgPresup.cloneNode(true)); break;
                        }
                        //oFila.cells[0].children[0].insertAdjacentElement("afterEnd", eval("oImgCualidad" + oFila.cualidad).cloneNode(true));
                        switch (oFila.getAttribute("cualidad")) {
                            case "C": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgContratante.cloneNode(true)); break;
                            case "J": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgRepJor.cloneNode(true)); break;
                            case "P": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgRepPrecio.cloneNode(true)); break;
                        }
                        //oFila.cells[0].children[0].insertAdjacentElement("afterEnd", eval("oImgCategoria" + oFila.categoria).cloneNode(true));
                        switch (oFila.getAttribute("categoria")) {
                            case "P": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgProducto.cloneNode(true)); break;
                            case "S": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgServicio.cloneNode(true)); break;
                        }
                        break;
                }
            }
        }
               
        $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;
*/        

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        switch ($I("cboConceptoEje").value) {
            case "0":
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estructura</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Categoría</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cualidad</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable</TD>");
                break;
            case "7":
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Categoría</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cualidad</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable</TD>");
                break;
            case "8":
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Categoría</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cualidad</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable</TD>");
                break;
            case "9":
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Categoría</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cualidad</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable</TD>");
                break;
            case "10":
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Categoría</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cualidad</TD>");
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable</TD>");
                break;
        }


        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Denominación</TD>");


        if ($I("chkV1").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV1").innerText + "</TD>");
        if ($I("chkV2").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV2").innerText + "</TD>");
        if ($I("chkV3").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV3").innerText + "</TD>");
        if ($I("chkV4").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV4").innerText + "</TD>");
        if ($I("chkV5").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV5").innerText + "</TD>");
        if ($I("chkV6").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV6").innerText + "</TD>");
        if ($I("chkV7").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV7").innerText + "</TD>");
        if ($I("chkV8").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV8").innerText + "</TD>");
        if ($I("chkV9").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + $I("tituloV9").innerText + "</TD>");

        if ($I("cboConceptoEje").value == 10) {
            sb.Append("        <td style='width:300px; background-color: #BCD4DF;'>" + strEstructuraNodo + "</TD>");
            //sb.Append("        <td style='width:300px; background-color: #BCD4DF;'>Responsable del proyecto</TD>");
            sb.Append("        <td style='width:600px; background-color: #BCD4DF;'>Cliente del proyecto</TD>");
        }
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].style.display == "none") {
                continue;
            }
            oFila = tblDatos.rows[i];
            sb.Append("<TR style='height:18px;vertical-align:middle;'>");
            for (var x = 0; x < oFila.cells.length; x++) {
                if (x == 0) {
                    switch ($I("cboConceptoEje").value) {
                        case "0":
                            sb.Append("<td style='width:auto;'>");
                            switch (oFila.getAttribute("tipo")) {
                                case "1":
                                    sb.Append(imgLeySN4.innerText);
                                    break;
                                case "2":
                                    sb.Append(imgLeySN3.innerText);
                                    break;
                                case "3":
                                    sb.Append(imgLeySN2.innerText);
                                    break;
                                case "4":
                                    sb.Append(imgLeySN1.innerText);
                                    break;
                                case "5":
                                    sb.Append(imgLeyNodo.innerText);
                                    break;
                                case "6":
                                    sb.Append(imgLeySubNodo.innerText);
                                    break;
                            }
                            sb.Append("</td>");
                            break;
                        case "7":
                            break;
                        case "8":
                            break;
                        case "9":
                            break;
                    }

                    strCadena = "";
                    switch (oFila.getAttribute("categoria")) {
                        case "P": strCadena = "Producto"; break;
                        case "S": strCadena = "Servicio"; break;
                    }

                    sb.Append("<td style='width:auto;'>" + strCadena + "</TD>");

                    strCadena = "";
                    switch (oFila.getAttribute("estado")) {
                        case "A":; strCadena = "Activo"; break;
                        case "C":; strCadena = "Cerrado"; break;
                        case "H":; strCadena = "Historico"; break;
                        case "P":; strCadena = "Presupuestado"; break;
                    }
                    sb.Append("<td style='width:auto;'>" + strCadena + "</TD>");

                    strCadena = "";
                    switch (oFila.getAttribute("cualidad")) {
                        case "C": strCadena = "Contratante"; break;
                        case "J": strCadena = "Replicado sin gestión"; break;
                        case "P": strCadena = "Replicado con gestión"; break;
                    }
                    sb.Append("<td style='width:auto;'>" + strCadena + "</TD>");

                    sb.Append("<td style='width:auto;'>" + oFila.getAttribute("responsable") + "</TD>");

                    sb.Append("<td style='width:auto;'>" + oFila.cells[x].innerText + "</TD>");
                }
                else {
                    if (oFila.cells[x].className.indexOf("textoR") > -1)
                        sb.Append("<td style='color:Red;align:right;width:auto;'>");
                    else
                        sb.Append("<td style='align:right;width:auto;'>");

                    sb.Append(oFila.cells[x].innerText + "</td>");
                }
            }

            if ($I("cboConceptoEje").value == 10) {
                sb.Append("<td>" + oFila.getAttribute("cr") + "</td>");
                //sb.Append("<td>" + oFila.getAttribute("responsable") + "</td>");
                sb.Append("<td>" + oFila.getAttribute("clienteProy") + "</td>");
            }

            sb.Append("</TR>");
        }
        sb.Append("	<TR>");

        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        if ($I("cboConceptoEje").value == "0") {
            sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        }
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        if ($I("chkV1").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[1].style.color + "'>" + $I("totalV1").innerText + "</TD>");
        if ($I("chkV2").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[2].style.color + "'>" + $I("totalV2").innerText + "</TD>");
        if ($I("chkV3").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[3].style.color + "'>" + $I("totalV3").innerText + "</TD>");
        if ($I("chkV4").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[4].style.color + "'>" + $I("totalV4").innerText + "</TD>");
        if ($I("chkV5").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[5].style.color + "'>" + $I("totalV5").innerText + "</TD>");
        if ($I("chkV6").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[6].style.color + "'>" + $I("totalV6").innerText + "</TD>");
        if ($I("chkV7").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[7].style.color + "'>" + $I("totalV7").innerText + "</TD>");
        if ($I("chkV8").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[8].style.color + "'>" + $I("totalV8").innerText + "</TD>");
        if ($I("chkV9").checked) sb.Append("        <td style='background-color: #BCD4DF;color:" + tblTotales.rows[0].cells[9].style.color + "'>" + $I("totalV9").innerText + "</TD>");
        if ($I("cboConceptoEje").value == 10) {
            sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
            //sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
            sb.Append("<td style='background-color: #BCD4DF;'>&nbsp;</td>");
        }
        sb.Append("	</TR>");

        //sb.Append("<tr><td colspan='" + oFila.cells.length + "' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");
        sb.Append("<tr style='vertical-align:top;'>");
        sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");
        for (var j = 2; j <= oFila.cells.length; j++) {
            sb.Append("<td></td>");
        }
        sb.Append("</tr>");
        sb.Append("</table>");

        //if (nIDFicepiEntrada == 777 || nIDFicepiEntrada == 1797) {
        //    crearExcelConCache(sb.ToString());
        //    //crearExcelSimpleServerCache(sIdCache);
        //}
        //else {
        //    crearExcel(sb.ToString());
        //}
        crearExcelConCache(sb.ToString());

        var sb = null;
    }
    catch (e) {
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function crearExcelConCache(sHTML) {
    try {
        mostrarProcesando();
        var js_args = "excel@#@" + sHTML;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en crearExcelConCache.", e.message);
    }
}

var nTopScrollDR = 0;
var nIDTimeDR = 0;
function scrollTablaDR(){
    try{
        oDivTitulo.scrollLeft = $I("divCatalogo").scrollLeft;
        oDivResultado.scrollLeft = $I("divCatalogo").scrollLeft;
    
        if ($I("divCatalogo").scrollTop != nTopScrollDR){
            nTopScrollDR = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeDR);
            nIDTimeDR = setTimeout("scrollTablaDR()", 50);
            return;
        }
        clearTimeout(nIDTimeDR);
        var nNumFilas = $I("divCatalogo").offsetHeight/20 +1;
        var nFilaVisible = Math.floor(nTopScrollDR/20);
        //var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var nContador = 0;
        var oFila;
        //for (var i = nFilaVisible; i < nUltFila; i++){
        for (var i = 0; i < $I("tblDatos").rows.length; i++){
            if ($I("tblDatos").rows[i].style.display == "none"){
                nContador--;
                continue;
            }
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("tipo") != "11") oFila.cells[0].children[0].style.marginRight = 0;
                
                switch (oFila.getAttribute("tipo")){
                    case "1": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN4.cloneNode(true)); break;
                    case "2": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN3.cloneNode(true)); break;
                    case "3": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN2.cloneNode(true)); break;
                    case "4": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSN1.cloneNode(true)); break;
                    case "5": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgNodo.cloneNode(true)); break;
                    case "6": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgSubNodo.cloneNode(true)); break;
                    case "7": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgCli.cloneNode(true)); break;
                    case "8": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgNat.cloneNode(true)); break;
                    case "9": oFila.cells[0].children[0].insertAdjacentElement("afterEnd",eval("oImg"+ oFila.getAttribute("tiporecurso")+oFila.getAttribute("sexo")).cloneNode(true)); break;
                    case "10":
                        //oFila.cells[0].children[0].insertAdjacentElement("afterEnd", eval("oImg10" + oFila.estado).cloneNode(true));
                        switch (oFila.getAttribute("estado")) {
                            case "A": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgAbierto.cloneNode(true)); break;
                            case "C": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgCerrado.cloneNode(true)); break;
                            case "H": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgHistorico.cloneNode(true)); break;
                            case "P": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgPresup.cloneNode(true)); break;
                        }
                        //oFila.cells[0].children[0].insertAdjacentElement("afterEnd", eval("oImgCualidad" + oFila.cualidad).cloneNode(true));
                        switch (oFila.getAttribute("cualidad")) {
                            case "C": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgContratante.cloneNode(true)); break;
                            case "J": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgRepJor.cloneNode(true)); break;
                            case "P": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgRepPrecio.cloneNode(true)); break;
                        }
                        //oFila.cells[0].children[0].insertAdjacentElement("afterEnd", eval("oImgCategoria" + oFila.categoria).cloneNode(true));
                        switch (oFila.getAttribute("categoria")) {
                            case "P": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgProducto.cloneNode(true)); break;
                            case "S": oFila.cells[0].children[0].insertAdjacentElement("afterEnd", oImgServicio.cloneNode(true)); break;
                        }
                        break;
                }
            }
            if (i > nFilaVisible) nContador++;
            //if (nContador > $I("divCatalogo").offsetHeight/20 +1){
            if (nContador > nNumFilas) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}

function borrarCatalogo(){
    try{
        if ($I("divCatalogo").children[0].innerHTML != ""){
            $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
            $I("totalV1").innerText = "0,00";
            $I("totalV1").style.color = "black";
            $I("totalV2").innerText = "0,00";
            $I("totalV2").style.color = "black";
            $I("totalV3").innerText = "0,00";
            $I("totalV3").style.color = "black";
            $I("totalV4").innerText = "0,00";
            $I("totalV4").style.color = "black";
            $I("totalV5").innerText = "0,00";
            $I("totalV5").style.color = "black";
            $I("totalV6").innerText = "0,00";
            $I("totalV6").style.color = "black";
            $I("totalV7").innerText = "0,00";
            $I("totalV7").style.color = "black";
            $I("totalV8").innerText = "0,00";
            $I("totalV8").style.color = "black";
            $I("totalV9").innerText = "0,00";
            $I("totalV9").style.color = "black";
        }

        $I("divTiempos").innerHTML = "";
        $I("divTiempos").style.display = "none";

    } catch (e) {
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}

function setLeyenda(){
    try{
        delLeyenda();
        if ($I("cboConceptoEje").value == "") return;
        switch($I("cboConceptoEje").value){
            case "0":
                var nValor = nNivelEstructura-1;
                if (nValor<=0) nValor = nEstructuraMinima;
                switch(nValor){
                    case 1:
                        $I("imgLeySN4").style.display = "";
                    case 2:
                        $I("imgLeySN3").style.display = "";
                    case 3:
                        $I("imgLeySN2").style.display = "";
                    case 4:
                        $I("imgLeySN1").style.display = "";
                    case 5:
                        $I("imgLeyNodo").style.display = "";
                    case 6:
                        $I("imgLeySubNodo").style.display = "";
                        break;
                }

                $I("imgLeyProy").style.display = "";
                break;
            case "7":
                var nValor = nNivelEstructura-1;
                switch(nValor){
                    case 1:
                        $I("imgLeySN4").style.display = "";
                        break;
                    case 2:
                        $I("imgLeySN3").style.display = "";
                        break;
                    case 3:
                        $I("imgLeySN2").style.display = "";
                        break;
                    case 4:
                        $I("imgLeySN1").style.display = "";
                        break;
                    case 5:
                        $I("imgLeyNodo").style.display = "";
                        break;
                    case 6:
                        $I("imgLeySubNodo").style.display = "";
                        break;
                }
                $I("imgLeyCli").style.display = "";
                $I("imgLeyProy").style.display = "";
                break;
            case "8":
                var nValor = nNivelEstructura-1;
                switch(nValor){
                    case 1:
                        $I("imgLeySN4").style.display = "";
                        break;
                    case 2:
                        $I("imgLeySN3").style.display = "";
                        break;
                    case 3:
                        $I("imgLeySN2").style.display = "";
                        break;
                    case 4:
                        $I("imgLeySN1").style.display = "";
                        break;
                    case 5:
                        $I("imgLeyNodo").style.display = "";
                        break;
                    case 6:
                        $I("imgLeySubNodo").style.display = "";
                        break;
                }
                $I("imgLeyNat").style.display = "";
                $I("imgLeyProy").style.display = "";
                break;
            case "9":
                var nValor = nNivelEstructura-1;
                switch(nValor){
                    case 1:
                        $I("imgLeySN4").style.display = "";
                        break;
                    case 2:
                        $I("imgLeySN3").style.display = "";
                        break;
                    case 3:
                        $I("imgLeySN2").style.display = "";
                        break;
                    case 4:
                        $I("imgLeySN1").style.display = "";
                        break;
                    case 5:
                        $I("imgLeyNodo").style.display = "";
                        break;
                    case 6:
                        $I("imgLeySubNodo").style.display = "";
                        break;
                }
                $I("imgLeyRes").style.display = "";
                $I("imgLeyProy").style.display = "";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la leyenda", e.message);
    }
}

function delLeyenda(){
    try{
        $I("imgLeySN4").style.display = "none";
        $I("imgLeySN3").style.display = "none";
        $I("imgLeySN2").style.display = "none";
        $I("imgLeySN1").style.display = "none";
        $I("imgLeyNodo").style.display = "none";
        $I("imgLeySubNodo").style.display = "none";
        $I("imgLeyCli").style.display = "none";
        $I("imgLeyRes").style.display = "none";
        $I("imgLeyNat").style.display = "none";
        $I("imgLeyProy").style.display = "none";
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la leyenda", e.message);
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


        //bCargarCriterios = false; 
        mostrarProcesando();
        var oTabla;
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
                else 
                {
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
                                        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../images/imgSN4.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"));
                                        oNF.insertCell(-1).appendChild(oImgSN4.cloneNode(true), null);
                                        oNF.id = aID[0];
                                        break;
                                    case 2:
                                        oNF.insertCell(-1).appendChild(oImgSN3.cloneNode(true), null);
                                        oNF.id = aID[1];
                                        break;
                                    case 3:
                                        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../images/imgSN2.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"));
                                        oNF.insertCell(-1).appendChild(oImgSN2.cloneNode(true), null);
                                        oNF.id = aID[2];
                                        break;
                                    case 4:
                                        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../images/imgSN1.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"));
                                        oNF.insertCell(-1).appendChild(oImgSN1.cloneNode(true), null);
                                        oNF.id = aID[3];
                                        break;
                                    case 5:
                                        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../images/imgNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"));
                                        oNF.insertCell(-1).appendChild(oImgNodo.cloneNode(true), null);
                                        oNF.id = aID[4];
                                        break;
                                    case 6:
                                        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../images/imgSubNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"));
                                        oNF.insertCell(-1).appendChild(oImgSubNodo.cloneNode(true), null);
                                        oNF.id = aID[5];
                                        break;
                                }
                                //oNF.cells[0].appendChild(document.createElement("<nobr class='NBR W230'></nobr>"));
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
                    else ocultarProcesando();
                } else ocultarProcesando();
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
	        
        borrarCatalogo();
        setTodos();            
        
        if ($I("chkActuAuto").checked){
            buscar();
        }else ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function mostrarOcultarColumnas(){
    try{
        $I("tblTitulo").style.width = "1270px";
        oColgroup = $I("tblTitulo").children[0];
        var oFila = $I("tblTitulo").rows[0];
        for (var i = 1; i <= 9; i++) {
            //oColgroup.children[i].style.width = "100px";
            //oColgroup.children[i].style.display = "";
            if (!$I("chkV" + i).checked) {
                oColgroup.children[i].style.width = "0px";
                oColgroup.children[i].style.display = "none";
                oFila.cells[i].style.width = "0px";
                oFila.cells[i].style.display = "none";
                //oColgroup.children[i].setAttribute("style", "width:0px; display:none;");
                $I("tblTitulo").style.width = ($I("tblTitulo").offsetWidth - 100) + "px";
            }
            else {
                oColgroup.children[i].style.width = "100px";
                oColgroup.children[i].style.display = "";
                oFila.cells[i].style.width = "100px";
                oFila.cells[i].style.display = "";
            }
        }

        if ($I("tblTitulo").offsetWidth >= 1200) 
        {
            if (!bRes1024) 
            {
                $I("tblTitulo").style.width = "1200px";
                $I("divCatalogo").children[0].style.width = $I("divCatalogo").children[0].children[0].style.width;
                $I("divCatalogo").style.width = "1216px";
            }            
        }

        if ($I("tblTitulo").offsetWidth >= 970) {
            if (bRes1024) {
                $I("tblTitulo").style.width = "970px";
                $I("divCatalogo").children[0].style.width = $I("divCatalogo").children[0].children[0].style.width;
                $I("divCatalogo").style.width = "986px";
            }
        }
        if ($I("divCatalogo").children[0].children[0].style.width == "") $I("divCatalogo").children[0].children[0].style.width = "0px";
        if ($I("divCatalogo").children[0].style.width > $I("divCatalogo").children[0].children[0].style.width)
            $I("divCatalogo").children[0].style.width = $I("divCatalogo").children[0].children[0].style.width;
                

        $I("divTablaTitulo").style.width = $I("tblTitulo").style.width;

        if ($I("tblTitulo").offsetWidth < 1200)
        {
            if (!bRes1024) 
            {
                $I("divCatalogo").children[0].style.width = $I("tblTitulo").style.width;
                $I("divCatalogo").style.width = ($I("tblTitulo").offsetWidth + 16) + "px";
            }
        }

        if ($I("tblTitulo").offsetWidth < 970) {
            if (bRes1024) {
                $I("divCatalogo").children[0].style.width = $I("tblTitulo").style.width;
                $I("divCatalogo").style.width = ($I("tblTitulo").offsetWidth + 16) + "px";
            }
        }       
        
        $I("tblTotales").style.width = "1270px";
        oColgroup = $I("tblTotales").children[0];
        oFila = $I("tblTotales").rows[0];
        for (var i = 1; i <= 9; i++) {

            //oColgroup.children[i].style.width = "100px";
            //oColgroup.children[i].style.display = "";

            if (!$I("chkV" + i).checked) {
                oColgroup.children[i].style.width = "0px";
                oColgroup.children[i].style.display = "none";
                oFila.cells[i].style.width = "0px";
                oFila.cells[i].style.display = "none";
                $I("tblTotales").style.width = ($I("tblTotales").offsetWidth - 100) + "px";                
            }
            else {
                oColgroup.children[i].style.width = "100px";
                oColgroup.children[i].style.display = "";
                oFila.cells[i].style.width = "100px";
                oFila.cells[i].style.display = "";
            }
        }

        if ($I("tblTotales").offsetWidth > 1200 && (!bRes1024)) $I("tblTotales").style.width = "1200px";
        if ($I("tblTotales").offsetWidth > 970 && (bRes1024)) $I("tblTotales").style.width = "970px";
        $I("divResultado").style.width = $I("tblTotales").style.width;
        
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar/ocultar las columnas", e.message);
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

function setResolucion1280(){
    try{
        $I("divTablaTitulo").style.width = "1200px";
        $I("divCatalogo").style.width = "1216px";
        $I("divCatalogo").style.height = "700px";
        $I("divResultado").style.width = "1200px";
    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
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
        sb.Append($I("cboConceptoEje").value +"@#@");
        sb.Append($I("cboCategoria").value +"@#@");
        sb.Append($I("cboCualidad").value +"@#@");
        sb.Append(($I("chkCerrarAuto").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkActuAuto").checked)? "1@#@":"0@#@");
        sb.Append(nUtilidadPeriodo +"@#@");
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
        var strEnlace = strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia);
        modalDialog.Show(strEnlace, self, sSize(450, 470))
	        .then(function(ret) {
                if (ret != null) {
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
                } else ocultarProcesando();
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

        for (var i = 0; i < $I("tblValores").rows.length; i++) {
            if (sb.buffer.length>0) sb.Append("///");
            sb.Append("15##chkV"+ (i+1).toString() +"##");
            sb.Append(($I("chkV"+ (i+1).toString()).checked)? "1":"0");
        }

        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs múltiples de los criterios.", e.message);
	}
}


function Limpiar()
{
    nNivelEstructura=0;
    nNivelSeleccionado = 0;
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;

    var aTable= $I('divPestRetr').getElementsByTagName("TABLE");       
    for (var i=0; i<aTable.length; i++){
        if (aTable[i].id.substring(0,3) != "tbl") continue;
		if (aTable[i].id == "tblValores") continue;
        BorrarFilasDe(aTable[i].id);
    }

    for (var i = 1; i < 10; i++) {
        if (i<7) $I("chkV"+i).checked=true;
        else $I("chkV"+i).checked=false;            
    }

    $I("cboConceptoEje").value="";
    $I("rdbOperador_0").checked=true;
    $I("cboCategoria").value="0";
    $I("cboCualidad").value="0"; 
    $I("chkCerrarAuto").checked=true;
    $I("chkActuAuto").checked=false;
    setTodos();
}

function setResultadoOnline(nOpcion) {
    try {
        mostrarProcesando();
        var sOpcion = (nOpcion == 1) ? "rdbResultadoCalculo" : "rdbResultadoCalculo2";
        var sValor = getRadioButtonSelectedValue(sOpcion, false);
        if (sOpcion == "rdbResultadoCalculo") {
            $I("rdbResultadoCalculo2_0").checked = (sValor == "1") ? true : false;
            $I("rdbResultadoCalculo2_1").checked = (sValor == "0") ? true : false;
        } else {
            $I("rdbResultadoCalculo_0").checked = (sValor == "1") ? true : false;
            $I("rdbResultadoCalculo_1").checked = (sValor == "0") ? true : false;
        }
        var js_args = "setResultadoOnline@#@";
        js_args += sValor;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a establecer la configuración del resultado.", e.message);
    }
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
                    $I("lblMonedaImportes2").innerText = aDatos[1];
                    buscar();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}
