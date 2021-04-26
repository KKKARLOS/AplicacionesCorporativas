var nOpcion = 0;
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nNivelIndentacion = 1;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var bCargandoCriterios=false;
var js_subnodos = new Array();
//var bPeriodoModificado = false;

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 440;
var nTopPestana = 125;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

var iDesdeOld = 0;
var iHastaOld = 0;

function init(){
    try{
        mostrarProcesando();
        if (bRes1024) setResolucion1024();

        iDesdeOld = $I("hdnDesde").value;
        iHastaOld = $I("hdnHasta").value;

        $I("rdbResultadoCalculo_0").style.verticalAlign = "middle";
        $I("rdbResultadoCalculo_1").style.verticalAlign = "middle";
        
        setExcelImg("imgExcel", "divCatalogo");
        
        if (parseFloat(dfn($I("totMargen").innerText)) < 0) $I("totMargen").style.color = "red";
        else $I("totMargen").style.color = "black";
        if (parseFloat(dfn($I("totProdExt").innerText)) + parseFloat(dfn($I("totProdInt").innerText)) != 0){
            $I("totRentabilidad").innerText = (parseFloat(dfn($I("totMargen").innerText)) * 100 / (parseFloat(dfn($I("totProdExt").innerText)) + parseFloat(dfn($I("totProdInt").innerText)))).ToString("N") + " %";
        }else $I("totRentabilidad").innerText = "0,00 %";

        if ($I("tblDatos") != null){
            scrollTablaProy();
            actualizarLupas("tblTitulo", "tblDatos");
        }
        if (!bHayPreferencia){
            mostrarOcultarPestVertical();
        }
        setOperadorLogico(false);
        js_subnodos = sSubnodos.split(",");
//        var oF2 = new Date(); 
//        alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }
        ocultarProcesando();
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
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("totProdExt").innerText = aResul[3];
                if (parseFloat(dfn(aResul[3])) < 0) $I("totProdExt").style.color = "red";
                else $I("totProdExt").style.color = "black";
                $I("totProdInt").innerText = aResul[4];
                if (parseFloat(dfn(aResul[4])) < 0) $I("totProdInt").style.color = "red";
                else $I("totProdInt").style.color = "black";
                $I("totConsumo").innerText = aResul[5];
                if (parseFloat(dfn(aResul[5])) < 0) $I("totConsumo").style.color = "red";
                else $I("totConsumo").style.color = "black";
                $I("totIngNetos").innerText = aResul[7];
                if (parseFloat(dfn(aResul[7])) < 0) $I("totIngNetos").style.color = "red";
                else $I("totIngNetos").style.color = "black";
                $I("totMargen").innerText = aResul[6];
                if (parseFloat(dfn(aResul[6])) < 0) $I("totMargen").style.color = "red";
                else $I("totMargen").style.color = "black";
                
                if (parseFloat(dfn($I("totMargen").innerText)) < 0) $I("totMargen").style.color = "red";
                else $I("totMargen").style.color = "black";

                if (parseFloat(dfn($I("totProdExt").innerText)) + parseFloat(dfn($I("totProdInt").innerText)) != 0){
                    $I("totRentabilidad").innerText = (parseFloat(dfn($I("totMargen").innerText)) * 100 / (parseFloat(dfn($I("totProdExt").innerText)) + parseFloat(dfn($I("totProdInt").innerText)))).ToString("N") +" %";
                }else $I("totRentabilidad").innerText = "0,00 %";
                
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
                window.focus();
                swBuscarProy = "1";
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
                mmoff("Suc", "Preferencias eliminadas.",250);
                break;
            case "getPreferencia":
                $I("cboEstado").value = aResul[44];
                $I("cboCategoria").value = aResul[3];  //2  +1
                $I("cboCualidad").value = aResul[4];
                $I("chkCerrarAuto").checked = (aResul[5] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[6] == "1") ? true : false;
                nUtilidadPeriodo = parseInt(aResul[8], 10);
                $I("hdnDesde").value = aResul[9];
                $I("txtDesde").value = aResul[10];
                $I("hdnHasta").value = aResul[11];
                $I("txtHasta").value = aResul[12];

                iDesdeOld = $I("hdnDesde").value;
                iHastaOld = $I("hdnHasta").value;

                //aResul[14] //la opción se determinará al buscar
                js_subnodos.length = 0;
                js_subnodos = aResul[13].split(",");

                BorrarFilasDe("tblAmbito");
                //insertarFilasEnTablaDOM("tblAmbito", aResul[13], 0);
                insertarFilasEnTablaDOM("tblAmbito", aResul[14], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[16], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[18], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[20], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblHorizontal");
                insertarFilasEnTablaDOM("tblHorizontal", aResul[22], 0);
                $I("divHorizontal").scrollTop = 0;

                BorrarFilasDe("tblSector");
                insertarFilasEnTablaDOM("tblSector", aResul[24], 0);
                $I("divSector").scrollTop = 0;

                BorrarFilasDe("tblSegmento");
                insertarFilasEnTablaDOM("tblSegmento", aResul[26], 0);
                $I("divSegmento").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[28], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[30], 0);
                $I("divContrato").scrollTop = 0;

                BorrarFilasDe("tblQn");
                insertarFilasEnTablaDOM("tblQn", aResul[32], 0);
                $I("divQn").scrollTop = 0;

                BorrarFilasDe("tblQ1");
                insertarFilasEnTablaDOM("tblQ1", aResul[34], 0);
                $I("divQ1").scrollTop = 0;

                BorrarFilasDe("tblQ2");
                insertarFilasEnTablaDOM("tblQ2", aResul[36], 0);
                $I("divQ2").scrollTop = 0;

                BorrarFilasDe("tblQ3");
                insertarFilasEnTablaDOM("tblQ3", aResul[38], 0);
                $I("divQ3").scrollTop = 0;

                BorrarFilasDe("tblQ4");
                insertarFilasEnTablaDOM("tblQ4", aResul[40], 0);
                $I("divQ4").scrollTop = 0;

                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[42], 0);
                $I("divProyecto").scrollTop = 0;

                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[7] == "1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;
                setTodos();

                if ($I("chkActuAuto").checked) {
                    bOcultarProcesando = false;
                    setTimeout("buscar();", 20);
                }
                break;
            case "setResolucion":
                location.reload(true);
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
        if (bOcultarProcesando)
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
                    if ($I("chkActuAuto").checked) {
                        buscar();
                    } else {
                        ocultarProcesando();
                    }
                } else ocultarProcesando();
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
        mostrarProcesando();
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
                 
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("hdnDesde").value +"@#@");
        sb.Append($I("hdnHasta").value +"@#@");
        sb.Append("7@#@");  //nNivelEstructura //subnodos
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
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function getDatosTabla(nTipo){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo) {
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

function mdpsn(oFila){
    try{
        //alert(oFila);
        if (oFila.tagName != "TR") oFila = oFila.parentNode.parentNode;
        
        mostrarProcesando();
        iFila = oFila.rowIndex;
//	    var strEnlace = "../SegEco/Default.aspx?";
//	    strEnlace += "nPSN="+ oFila.id;
//	    strEnlace += "&ML="+ oFila.ML;
//	    strEnlace += "&origen=resumen";

        var sb = new StringBuilder;
        var tblDatos = $I("tblDatos");
	    for (var i=0;i < tblDatos.rows.length; i++){
	        if (i>0) sb.Append(",");
	        sb.Append(tblDatos.rows[i].id);
        }
//	    strEnlace += "&listaPSN="+ sb.ToString();

//	    location.href = strEnlace;
	    
	    $I("nPSN").value = oFila.id;
	    //$I("ML").value = oFila.getAttribute("ML");
	    $I("ListaPSN").value = sb.ToString();
	    $I("MonedaPSN").value = oFila.getAttribute("moneda_proyecto");
	    document.forms["aspnetForm"].method = "POST";
	    document.forms["aspnetForm"].action = "../SegEco/Default.aspx";
	    document.forms["aspnetForm"].submit();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del proyectosubnodo.", e.message);
	}
}

function excel(){
    try {
        var tblDatos = $I("tblDatos");
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Categoría</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cualidad</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Centro de responsabilidad</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Contrato</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Prod. Externa</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Prod. Interna</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Consumo</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Ing. netos</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Margen</TD>");
		sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Rentabilidad</TD>");
		sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i=0;i < tblDatos.rows.length; i++){
            sb.Append("<TR>");
	        for (var x=0;x<tblDatos.rows[i].cells.length; x++){
	            if (x==0){
					sb.Append("<td>");
					if (tblDatos.rows[i].getAttribute("categoria") == "P") sb.Append("Producto");
					else sb.Append("Servicio");
					sb.Append("</td><td>");
					switch (tblDatos.rows[i].getAttribute("cualidad")){
						case "C": sb.Append("Contratante"); break;
						case "J": sb.Append("Replicado sin gestión"); break;
						case "P": sb.Append("Replicado con gestión"); break;
					}	
					sb.Append("</td><td>");

					switch (tblDatos.rows[i].getAttribute("estado")) {
						case "A": sb.Append("Abierto"); break;
						case "C": sb.Append("Cerrado"); break;
						case "H": sb.Append("Histórico"); break;
						case "P": sb.Append("Presupuestado"); break;
					}	
					
	                sb.Append("</td>");
	            }else if (x>2){
	                switch (x){
	                    case 4:
	                        sb.Append(tblDatos.rows[i].cells[x].outerHTML);
	                        sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("desNodo")) + "</td>");
	                        break;
	                    case 5: /*En 1024, la columna de contrato está oculta.*/
	                    case 12:
	                        sb.Append("<td style='align:right;'>" + tblDatos.rows[i].cells[x].innerText + "</td>");
	                        break;
	                    default:
	                        sb.Append(tblDatos.rows[i].cells[x].outerHTML);
	                        break;
	                }
					//if (x == 12) sb.Append("<td style='align:right;'>"+ tblDatos.rows[i].cells[x].innerText +"</td>");
					//else sb.Append(tblDatos.rows[i].cells[x].outerHTML);				
				}				
            }
            sb.Append("</TR>");
        }		
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ $I("totProdExt").innerText +"</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ $I("totProdInt").innerText +"</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ $I("totConsumo").innerText +"</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ $I("totIngNetos").innerText +"</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ $I("totMargen").innerText +"</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ $I("totRentabilidad").innerText +"</TD>");
		sb.Append("	</TR>");

		//sb.Append("<tr><td colspan='" + tblDatos.rows[0].cells.length + "' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");
		sb.Append("<tr style='vertical-align:top;'>");
		sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");
		for (var j = 2; j <= tblDatos.rows[0].cells.length; j++) {
		    sb.Append("<td></td>");
		}
		sb.Append("</tr>");
		
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
/*
var oImgProducto = document.createElement("<img src='../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");
var oImgServicio = document.createElement("<img src='../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");

var oImgContratante = document.createElement("<img src='../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");
var oImgRepJor = document.createElement("<img src='../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");
var oImgRepPrecio = document.createElement("<img src='../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");

var oImgAbierto = document.createElement("<img src='../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");
var oImgCerrado = document.createElement("<img src='../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");
var oImgHistorico = document.createElement("<img src='../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");
var oImgPresup = document.createElement("<img src='../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;' />");
*/

var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProy){
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        $I("totProdExt").innerText = "0,00";
        $I("totProdInt").innerText = "0,00";
        $I("totConsumo").innerText = "0,00";
        $I("totIngNetos").innerText = "0,00";
        $I("totMargen").innerText = "0,00";
        $I("totRentabilidad").innerText = "0,00";

        $I("totProdExt").style.color = "black";
        $I("totProdInt").style.color = "black";
        $I("totConsumo").style.color = "black";
        $I("totIngNetos").style.color = "black";
        $I("totMargen").style.color = "black";
        $I("totRentabilidad").style.color = "black";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
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
                if (bCargarCriterios) 
                {
                    sTamano = sSize(850, 460);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterio/Default.aspx?nTipo=" + nTipo;
                }
                else {
                    sTamano = sSize(850, 420);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
                }                
                break;
        }
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo) {
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
                                oNF.style.height = "18px";
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
                                var oCtrl1 = document.createElement("div");
                                oCtrl1.className = "NBR W230";
                                oCtrl1.attachEvent('onmouseover', TTip);
                                oNF.cells[0].appendChild(oCtrl1);
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
                                oCtrl1.className = "NBR W180";
                                oCtrl1.setAttribute("style", 'margin-left:0px;');
                                oCtrl1.attachEvent('onmouseover', TTip);

                                oNF.cells[0].appendChild(oCtrl1);
                                oNF.cells[0].children[3].innerHTML = Utilidades.unescape(aDatos[1]);                           
                                
                                
                            }
                            divProyecto.scrollTop=0;
                            break;                    
                    }
                    setTodos();            
                    if ($I("chkActuAuto").checked) setTimeout("buscar();", 20);
                    else ocultarProcesando();
                }else ocultarProcesando();
	        });      	    
	    //var ret = window.open(strEnlace,'','');
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

            var oCtrl1 = document.createElement("div");
            oCtrl1.className = "NBR W255";
            oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));

            oNF.insertCell(-1).appendChild(oCtrl1);

            //oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
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
	        
        borrarCatalogo();
        setTodos();            
        
        if ($I("chkActuAuto").checked){
            buscar();
        }else ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
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
    try {
        $I("lblContrato").innerHTML = "Cont."
        //$I("lblCliente").innerHTML = "Cl."
        $I("tblGeneral").style.width = "1000px";
        $I("tblTitulo").style.width = "980px";
        oColgroup = $I("tblTitulo").children[0];
        //oColgroup.children[1].style.width = "260px";
        oColgroup.children[1].setAttribute("style", "width:210px;");
        oColgroup.children[2].setAttribute("style", "width:50px;");
        //oColgroup.children[2].style.display = "none";
        oColgroup.children[3].setAttribute("style", "width:60px;");

        var oFila = $I("tblTitulo").rows[0];
        //oFila.cells[1].style.width = "260px";
        //oFila.cells[2].style.width = "0px";
        //oFila.cells[2].style.display = "none";
        //oFila.cells[3].style.width = "65px"; //-40
        oFila.cells[1].setAttribute("style", "width:210px;");
        oFila.cells[2].setAttribute("style", "width:50px;");
        oFila.cells[3].setAttribute("style", "width:60px;");

        $I("divCatalogo").style.width = "996px";
        $I("divCatalogo").style.height = "430px";
        $I("divCatalogo").children[0].style.width = "980px";

        $I("tblTotales").style.width = "980px";
        oColgroup = $I("tblTotales").children[0];
        //oColgroup.children[2].style.width = "0px";
        //oColgroup.children[2].style.display = "none";
        //oColgroup.children[3].style.width = "65px"; //-40
        //oColgroup.children[4].style.width = "90px"; //-40
        //oColgroup.children[8].style.width = "90px"; //-40
        //oColgroup.children[9].style.width = "88px"; //-40        
        oColgroup.children[1].setAttribute("style", "width:220px;");
        oColgroup.children[2].setAttribute("style", "width:40px;");
        oColgroup.children[3].setAttribute("style", "width:60px;");

        oFila = $I("tblTotales").rows[0];
        //oFila.cells[2].style.width = "0px";
        //oFila.cells[2].style.display = "none";
        //oFila.cells[3].style.width = "68px"; //-40
        //oFila.cells[4].style.width = "90px"; //-40
        //oFila.cells[8].style.width = "90px"; //-40
        //oFila.cells[9].style.width = "88px"; //-40
        oFila.cells[1].setAttribute("style", "width:220px;");
        oFila.cells[2].setAttribute("style", "width:40px;");
        oFila.cells[3].setAttribute("style", "width:60px;");

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

function getValoresMultiples(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n=1; n<=16; n++){
            if (n==15) continue;
            switch (n) {
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
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }else if (n==16){
                if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }else{
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n +"##"+ oTabla.rows[i].id +"##"+ Utilidades.escape(oTabla.rows[i].innerText));
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

    var aTable= $I('divPestRetr').getElementsByTagName("TABLE");       
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

function setResultadoOnline() {
    try {
        mostrarProcesando();
        var js_args = "setResultadoOnline@#@";
        js_args += getRadioButtonSelectedValue("rdbResultadoCalculo", false);

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
                    buscar();
                } else
                    ocultarProcesando();
	        });             
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}
