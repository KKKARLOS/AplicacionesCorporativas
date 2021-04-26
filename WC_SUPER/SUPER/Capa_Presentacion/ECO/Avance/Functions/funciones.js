//var oDivTitulo;
//var oDivOnLine;
//var oDivMonedaImportes;

var oDivBodyFijo = null;
var oDivBodyMovil = null;
var oDivTituloMovil = null;
//var mousewheelevt = (document.attachEvent) ? "mousewheel" : "DOMMouseScroll"  //FF doesn't recognize mousewheel as of FF3.x
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x  

//var sValorNodo = "";
var nOpcion = 0;
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nNivelIndentacion = 1;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var bCargandoCriterios=false;
var js_subnodos = new Array();
var bPeriodoModificado = false;

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 440;
var nTopPestana = 125;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

function init(){
    try{
        if (bRes1024) setResolucion1024();
        
        $I("rdbResultadoCalculo_0").style.verticalAlign = "middle";
        $I("rdbResultadoCalculo_1").style.verticalAlign = "middle";

        setExcelImg("imgExcel", "divBodyMovil");
        
        if ($I("tblBodyFijo").rows.length > 0){
            scrollTablaProy();
            actualizarLupas("tblTitulo", "tblBodyFijo");
        }
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);

        setOperadorLogico(false);
        
        if (!bHayPreferencia) mostrarOcultarPestVertical();
        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }

        //oDivTitulo = $I("divTablaTitulo");
        //oDivOnLine = $I("divOnline");
        //oDivMonedaImportes = $I("divMonedaImportes");

        oDivBodyFijo = $I("divBodyFijo");
        oDivBodyMovil = $I("divBodyMovil");
        oDivTituloMovil = $I("divTituloMovil");

        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false) 

	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
/*
function moverScroll(e) {
    try {
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;

        oDivTitulo.scrollLeft = oElement.scrollLeft;
        //oDivMonedaImportes.style.left = (415 - oElement.scrollLeft) + "px";
        oDivOnLine.style.left = (205 - oElement.scrollLeft) + "px";
        oDivMonedaImportes.style.left = 395 - oElement.scrollLeft + "px";        
    } catch (e) {
        mostrarErrorAplicacion("Error al mover el scroll del título", e.message);
    }
}
*/
function setScroll() {
    try {   
        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}
function setFilaFija(oFila) {
    try {
        ms(oFila);
        ms($I("tblBodyMovil").rows[oFila.rowIndex]);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila fija", e.message);
    }
}

function setFilaMovil(oFila) {
    try {
        ms($I("tblBodyFijo").rows[oFila.rowIndex]);
        ms(oFila);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila móvil", e.message);
    }
}
function setBuscarDescriFija() {
    if (intFilaSeleccionada != -1) 
    {
        ms($I("tblBodyMovil").rows[intFilaSeleccionada]);
        $I("divBodyMovil").scrollTop = $I("tblBodyMovil").rows[intFilaSeleccionada].offsetTop - 16;
        return;
    };
    
    var aFilaBus = FilasDe("tblBodyMovil");
    if (aFilaBus.length == 0) return;
	for (var i = 0; i < aFilaBus.length; i++) {
	    if (aFilaBus[i].style.display == "none") continue;
	    if (aFilaBus[i].className != "") aFilaBus[i].className = "";
	}
	intFilaSeleccionada = -1;
	$I("divBodyMovil").scrollTop = 0;
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
                $I("divBodyFijo").children[0].innerHTML = aResul[2];
                $I("divBodyMovil").children[0].innerHTML = aResul[3];
                
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblBodyFijo");
                window.focus();

                if (nIDFicepiEntrada == 1568
                    || nIDFicepiEntrada == 1321
                    || nIDFicepiEntrada == 1511
                        ) {
                    $I("divTiempos").innerHTML = "Respuesta de BD: " + aResul[4] + " ms.<br>Respuesta HTML: " + aResul[5] + " ms.";
                    $I("divTiempos").style.display = "block";
                }
                break;
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.",190);
                break;
            case "getPreferencia":
                $I("cboEstado").value = aResul[44];  
                $I("cboCategoria").value = aResul[3];  //2  +1
                $I("cboCualidad").value = aResul[4];
                $I("chkCerrarAuto").checked = (aResul[5]=="1")? true:false;
                $I("chkActuAuto").checked = (aResul[6]=="1")? true:false;
                nUtilidadPeriodo = parseInt(aResul[8], 10);
                $I("hdnDesde").value = aResul[9];
                $I("txtDesde").value = aResul[10];
                $I("hdnHasta").value = aResul[11];
                $I("txtHasta").value = aResul[12];
                //aResul[14] //la opción se determinará al buscar
                js_subnodos.length = 0;
                js_subnodos = aResul[13].split(",");

                BorrarFilasDe("tblAmbito");
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
                if (aResul[7]=="1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;
                                   
                setTodos();
                
                if ($I("chkActuAuto").checked)
                    setTimeout("buscar();", 20);
                  
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
        ocultarProcesando();
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


function borrarCatalogo(){
    try {
        $I("divBodyFijo").children[0].innerHTML = "";
        $I("divBodyMovil").children[0].innerHTML = "";

        $I("divTiempos").innerHTML = "";
        $I("divTiempos").style.display = "none";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar(){
    try{
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append(nAnoMesActual +"@#@");
        sb.Append(nAnoMesActual +"@#@");
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
        borrarCatalogo();       
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}

function cambiarMes(nMes){
    try{
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        
        //if ($I("chkActuAuto").checked) 
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}

function mdpsn(oFila){
    try{
        //alert(oFila);
        if (oFila.tagName != "TR") oFila = oFila.parentNode.parentNode;

        iFila = oFila.rowIndex;
        var strEnlace = strServer + "Capa_Presentacion/ECO/AvanceDetalle/Default.aspx?";

        strEnlace += "nPSN=" + oFila.id; 
        strEnlace += "&nPE=" + oFila.getAttribute("PE");
        //strEnlace += "&sPE=" + codpar(oFila.cells[4].innerText);
        strEnlace += "&sPE=" + codpar(oFila.getAttribute("despe"));
        strEnlace += "&ML=" + oFila.getAttribute("ML");
        strEnlace += "&idNodo=" + oFila.getAttribute("idNodo");
        strEnlace += "&sAnoMes=" + nAnoMesActual;
        strEnlace += "&estado=" + oFila.getAttribute("estado");
	    
	    strEnlace += "&origen=E";
	    strEnlace += "&moneda_proyecto=" + oFila.getAttribute("moneda_proyecto");

	    var sTamano;
	    if (bRes1024) sTamano = sSize(1030, 735);
	    else sTamano = sSize(1280, 990);
        mostrarProcesando();

	    //var ret = window.showModalDialog(strEnlace, self, sTamano);
	    modalDialog.Show(strEnlace, self, sTamano)
	        .then(function(ret) {
	            if (ret != null) {
	                //alert(ret);
	                var aDatos = ret.split("@#@");
	                if (aDatos[0] != nAnoMesActual || aDatos[1] == "T") {//si se ha cambiado de mes o datos del mes.
	                    nAnoMesActual = aDatos[0];
	                    $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
	                    buscar();
	                } else ocultarProcesando();
	            } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del proyectosubnodo", e.message);
	}
}

function excel(){
    try {
        var tblBodyMovil = $I("tblBodyMovil");
        
        if (tblBodyMovil == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td colspan='5'>&nbsp;"+$I("txtMesVisible").value+"</td>");
        sb.Append("        <td colspan='4' style='width:auto; background-color: #E4EFF3;'>Planificado</TD>");
        sb.Append("        <td colspan='5' style='width:auto; background-color: #E4EFF3;'>IAP</TD>");
        sb.Append("        <td colspan='4' style='width:auto; background-color: #E4EFF3;'>Previsto</TD>");
        sb.Append("        <td colspan='2' style='width:auto; background-color: #E4EFF3;'>Avance</TD>");
        sb.Append("        <td colspan='3' style='width:auto; background-color: #E4EFF3;'>Económico</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width=:auto; background-color: #BCD4DF;'>Categoría</TD>");
        sb.Append("        <td style='width=:auto; background-color: #BCD4DF;'>Cualidad</TD>");
        sb.Append("        <td style='width=:auto; background-color: #BCD4DF;'>Estado</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Nº</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Proyecto</TD>");
        
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
        
        sb.Append("        <td style='background-color: #BCD4DF;'>Prod. mes</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Tot. proy.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>%</TD>");
        
		sb.Append("	</TR>");

		//        sb.Append(tblDatos.innerHTML);
		var tblBodyFijo = $I("tblBodyFijo");
		
		for (var i = 0; i < tblBodyMovil.rows.length; i++) {
	        sb.Append("<tr>");
	        for (var x = 0; x < tblBodyFijo.rows[i].cells.length; x++) {
	            if (x==0){
					sb.Append("<td>");
					if (tblBodyFijo.rows[i].getAttribute("categoria") == "P") sb.Append("Producto");
					else sb.Append("Servicio");
					sb.Append("</td><td>");
					switch (tblBodyFijo.rows[i].getAttribute("cualidad")) {
						case "C": sb.Append("Contratante"); break;
						case "J": sb.Append("Replicado sin gestión"); break;
						case "P": sb.Append("Replicado con gestión"); break;
					}	
					sb.Append("</td><td>");

					switch (tblBodyFijo.rows[i].getAttribute("estado")) {
						case "A": sb.Append("Abierto"); break;
						case "C": sb.Append("Cerrado"); break;
						case "H": sb.Append("Histórico"); break;
						case "P": sb.Append("Presupuestado"); break;
					}						
	                sb.Append("</td>");
	            } else if (x > 2) sb.Append(tblBodyFijo.rows[i].cells[x].outerHTML);
	        }

	        for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
                sb.Append(tblBodyMovil.rows[i].cells[x].outerHTML);
	        }	        

	        sb.Append("</tr>");
	    }
	    
	    //sb.Append("<tr><td colspan='23' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");
	    sb.Append("<tr style='vertical-align:top;'>");
	    sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");

	    for (var j = 2; j <= 23; j++) {
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

var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy(){
    try{
        if ($I("divBodyMovil").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divBodyMovil").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }

        var tblBodyFijo = $I("tblBodyFijo");
        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divBodyMovil").offsetHeight / 20 + 1, tblBodyFijo.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblBodyFijo.rows[i].getAttribute("sw")) {
                oFila = tblBodyFijo.rows[i];
                oFila.setAttribute("sw", 1);

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
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

//function setActuAuto(){
//    try{
//        if ($I("chkActuAuto").checked){
//            setOp($I("btnObtener"), 30);
//            buscar();
//        }else{
//            setOp($I("btnObtener"), 100);
//        }

//	}catch(e){
//		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
//	}
//}

function setCombo(){
    try{
        borrarCatalogo();
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
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
function getCriterios(nTipo){
    try{
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == ""){
            nCriterioAVisualizar = nTipo;
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
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

                //if (es_administrador == "") 
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
                                oNF.style.height = "18px";
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
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470));
        var strEnlace = strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia);
        modalDialog.Show(strEnlace, self, sSize(450, 470))
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
    try{
        $I("tblGeneral").style.width = "1001px";
        $I("tblGeneral").style.marginLeft="1px"; 
//        oColgroup = $I("tblGeneral").children[0];
//        oColgroup.children[1].style.width = "626px";

//        var oFila = $I("tblTituloMovil").rows[0];
//        oFila.cells[2].style.width = "0px";
                 
        $I("divBodyFijo").style.height = "400px";
        $I("divBodyMovil").style.height = "416px";
        $I("divTituloMovil").style.width = "606px";
        $I("divBodyMovil").style.width = "621px";
        //$I("tblResultado").style.width = "980px";
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
                } else if (n == 16) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
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
